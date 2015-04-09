using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.Helper;
using ChamCong_v05.Properties;
using log4net;

namespace ChamCong_v05.UI.XepLich {
	public partial class fmDSNVNhanNhiemVu : Form {
		#region log tooltip và hàm ko quan trọng

		public readonly ILog lg = LogManager.GetLogger("frm_XemCong");

		private void toolTipHint_Draw(object sender, DrawToolTipEventArgs e) {
			Font f = new Font("Arial", 10.0f);
			e.DrawBackground();
			e.DrawBorder();
			e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, new PointF(2f, 2f));
		}

		private void toolTipHint_Popup(object sender, PopupEventArgs e) {
			Size temp = TextRenderer.MeasureText(toolTipHint.GetToolTip(e.AssociatedControl), new Font("Arial", 10.0f));
			temp.Width += 4;
			temp.Height += 4;
			e.ToolTipSize = temp;
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			Close();
		}

		#endregion

		public List<int> m_listIDPhongBan = new List<int>();

		#region biến checkbox check all: 1 của DSNV cần xem công

		private readonly CheckBox checkAll_GridDSNVNhanNhiemVu = new CheckBox();

		private void checkAll_CheckedChanged(object sender, EventArgs e) {
			//1. xác định datagrid nào đang check all
			DataGridView tempGrid = dgrdDSNVNhanNhiemVu;

			// 2. kiểm tra nếu ko có dòng dữ liệu nào thì thoát
			if (tempGrid.Rows.Count == 0) return;

			//3. có dữ liệu, duyệt qua hết và gán giá trị check (or uncheck), sau đó endEDIT lại lưới để dòng đầu tiên có giá trị
			var tmpCheckAll = ((CheckBox)sender).Checked;

			tempGrid.BeginEdit(true);
			var dt = tempGrid.DataSource as DataView;

			if (dt != null && dt.Count != 0) {
				foreach (DataRowView row in dt) {
					row["check"] = tmpCheckAll;
				}
			}

			tempGrid.EndEdit();
			tempGrid.Update();
			tempGrid.RefreshEdit();
		}

		#endregion


		public fmDSNVNhanNhiemVu() {
			InitializeComponent();

			log4net.Config.XmlConfigurator.Configure();
			//không cho autogen các column khi bind dữ liệu
			dgrdDSNVNhanNhiemVu.AutoGenerateColumns = false;

			//3. vẽ 3 checkbox checkall cho DSNV trong phòng
			XL2.VeCheckBox_CheckAll(dgrdDSNVNhanNhiemVu, checkAll_GridDSNVNhanNhiemVu, checkAll_CheckedChanged, new Point(7, 3));
		}

		private void fmDSNVNhanNhiemVu_Load(object sender, EventArgs e) {
			XL.loadTreePhgBan(treePhongBan);
			// đăng ký sự kiện cho tree và chọn topNode
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;
			treePhongBan.SelectedNode = XL.TopNode(treePhongBan.TopNode);

			#region 			//load danh sách nhiệm vụ, có add thêm cột check

			DataTable tableNhiemVu = SqlDataAccessHelper.ExecSPQuery(SPName.NhiemVu_DocBang.ToString());
			DataColumn columnCheck = tableNhiemVu.Columns.Add("check", typeof(bool));
			columnCheck.DefaultValue = false;
			checkListNhiemVu.DataSource = tableNhiemVu;
			checkListNhiemVu.ValueMember = "MaNhiemVu";
			checkListNhiemVu.DisplayMember = "TenNhiemVu";
			checkListNhiemVu.ClearSelected();

			#endregion


		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			#region mỗi lần chọn node thì lấy ID node hiện tại và tất cả node con

			m_listIDPhongBan.Clear();
			e.Node.Expand();
			TreeNode topnode = XL.TopNode(e.Node); //đưa về root để thực hiện từ trên xuống
			if (topnode != null) XL.GetIDNodeAndChildNode1(e.Node, ref m_listIDPhongBan); // chỉ lấy các phòng ban được phép, 
			else {
				var temp = ((DataRow)e.Node.Tag);
				if ((int)temp["IsYes"] == 1) m_listIDPhongBan.Add((int)temp["ID"]);
			}

			#endregion

			#region kiểm tra kết nối csdl , nếu mất kết nối thì đóng

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}

			#endregion

			LoadGridNhanVienNhanNhiemVu();
		}

		private void checkListNhiemVu_ItemCheck(object sender, ItemCheckEventArgs e) {

			List<int> listMaNhiemVu = (from DataRowView rowView in checkListNhiemVu.CheckedItems select (int)rowView["MaNhiemVu"]).ToList();
			// vì sau khi check xong thì hàm này mới đc gọi (checked) nên chỉ cần xét giá trị hiện tại là biết đc giá trị sắp tới
			if (e.CurrentValue == CheckState.Unchecked) listMaNhiemVu.Add((int)((DataRowView)checkListNhiemVu.SelectedItem)["MaNhiemVu"]);
			else if (e.CurrentValue == CheckState.Checked) listMaNhiemVu.Remove((int)((DataRowView)checkListNhiemVu.SelectedItem)["MaNhiemVu"]);
			DataTable tableDSNVCoNhiemVu = this.tableNhanVienNhanNhiemVu(m_listIDPhongBan, listMaNhiemVu);
			dgrdDSNVNhanNhiemVu.DataSource = new DataView(tableDSNVCoNhiemVu);
		}

		private void btnDangKyNhiemVu_Click(object sender, EventArgs e) {
			//tbd test
			frmDangKyNhiemVuChoNV frm = new frmDangKyNhiemVuChoNV();
			frm.m_listIDPhongBan = this.m_listIDPhongBan;
			frm.ShowDialog();

			//sau khi đăng ký xong thì load lại GUI datagrid
			LoadGridNhanVienNhanNhiemVu();
		}

		private void btnHuyNhiemVu_Click(object sender, EventArgs e) {
			#region logic
			/* 1. lấy các dòng của nhiệm vụ chính được chọn, nhiệm vụ phụ được chọn
			 * 2. kiểm tra ko chọn 1 dòng nào thì thoát ko thực hiện gì hết
			 */
			#endregion

			bool haveOtherError = false;
			string otherError = "Không huỷ được nhiệm vụ sau của nhân viên: \n";

			var listNhiemVuCuaNhanVien = (from DataGridViewRow gridViewRow in dgrdDSNVNhanNhiemVu.Rows
										   let rowView = (DataRowView)gridViewRow.DataBoundItem
										   where rowView["check"] != DBNull.Value && (bool)rowView["check"]
										   select rowView).ToList();
			if (listNhiemVuCuaNhanVien.Count == 0) return;
			this.HuyNhiemVuCuaNhanVien(listNhiemVuCuaNhanVien, ref haveOtherError, ref otherError);
			if (haveOtherError) {
				MessageBox.Show(otherError, Resources.Caption_Loi);
			}
			ACMessageBox.Show(Resources.Text_DaThucHienXong, Resources.Caption_ThongBao, 2000);
			//reload gui
			LoadGridNhanVienNhanNhiemVu();
		}

		private void LoadGridNhanVienNhanNhiemVu() {
			//1. lấy thông tin mảng mã phòng được thao tác, mảng mã nhiệm vụ chọn
			//2. query với mảng mã phòng, mảng mã nhiệm vụ, có kết quả thì lọc ra theo 2 danh sách chính và phụ

			List<int> listMaNhiemVu = (from DataRowView rowView in checkListNhiemVu.CheckedItems select (int)rowView["MaNhiemVu"]).ToList();

			DataTable tableDSNVCoNhiemVu = this.tableNhanVienNhanNhiemVu(m_listIDPhongBan, listMaNhiemVu);
			dgrdDSNVNhanNhiemVu.DataSource = new DataView(tableDSNVCoNhiemVu);
		}

		/// <summary>
		/// Lấy các nhân viên thuộc phòng ban được thao tác và có nhiệm vụ tương ứng nhiệm vụ được check
		/// </summary>
		/// <param name="listMaPhongBan"></param>
		/// <param name="listMaNhiemVu"></param>
		/// <returns></returns>
		public DataTable tableNhanVienNhanNhiemVu(List<int> listMaPhongBan, List<int> listMaNhiemVu) {
			DataTable tableMaPhongBan = MyUtility.Array_To_DataTable("tableMaPhongBan", listMaPhongBan);
			SqlParameter sqlParamArrUserIDD = new SqlParameter("@ArrUserIDD", SqlDbType.Structured);
			sqlParamArrUserIDD.Value = tableMaPhongBan;
			DataTable tableMaNhiemVu = MyUtility.Array_To_DataTable("tableMaNhiemVu", listMaNhiemVu);
			SqlParameter sqlParamArrMaNhiemVu = new SqlParameter("@ArrMaNhiemVu", SqlDbType.Structured);
			sqlParamArrMaNhiemVu.Value = tableMaNhiemVu;
			DataTable kq = SqlDataAccessHelper.ExecSPQuery(SPName.UserInfo_DocNhanVienNhanNhiemVu.ToString(),
				sqlParamArrUserIDD, sqlParamArrMaNhiemVu);
			kq.Columns.Add(new DataColumn("check", typeof(bool)) { DefaultValue= false});

			return kq;
		}

		private void HuyNhiemVuCuaNhanVien(List<DataRowView> listNhiemVuCuaNhanVien, ref bool haveOtherError, ref string otherError) {
			string templateStr = "Nhiệm vụ [{0}] của nhân viên [{1}], mã NV [{2}].\n";
			foreach (var rowView in listNhiemVuCuaNhanVien) {
				var uen = (int)rowView["UserEnrollNumber"];
				var tenNhanVien = rowView["UserFullName"].ToString();
				var maNhanVien = rowView["UserFullCode"].ToString();
				var maNhiemVu = (int)rowView["MaNhiemVu"];
				var tenNhiemVu = rowView["TenNhiemVu"].ToString();
				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName.NhiemVu_NhanVien_DEL.ToString(),
															 new SqlParameter("@UserEnrollNumber", uen),
															 new SqlParameter("@MaNhiemVu", maNhiemVu));
				if (kq == 0) {
					haveOtherError = true;
					otherError += string.Format(templateStr, tenNhiemVu, tenNhanVien, maNhanVien);
				}
			}
		}

	}
}
