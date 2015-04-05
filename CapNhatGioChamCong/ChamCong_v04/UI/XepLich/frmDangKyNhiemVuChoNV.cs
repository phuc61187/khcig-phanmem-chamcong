using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;
using log4net;

namespace ChamCong_v04.UI.XepLich {
	public partial class frmDangKyNhiemVuChoNV : Form {
		#region log tooltip và hàm ko quan trọng

		public readonly ILog lg = LogManager.GetLogger("frmDangKyNhiemVuChoNV");

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

		private readonly CheckBox checkAll_GridDSNV = new CheckBox();

		private void checkAll_CheckedChanged(object sender, EventArgs e) {
			//1. xác định datagrid nào đang check all
			DataGridView tempGrid = dgrdDSNVTrgPhg;

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

		public frmDangKyNhiemVuChoNV() {
			InitializeComponent();

			log4net.Config.XmlConfigurator.Configure();
			// không cho autogen các column khi bind dữ liệu
			dgrdDSNVTrgPhg.AutoGenerateColumns = false;

			//3. vẽ checkbox checkall cho DSNV trong phòng
			XL2.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));
		}

		private void frmDangKyNhiemVuChoNV_Load(object sender, EventArgs e) {
			//lấy danh sách tất cả nhân viên trong phòng được thao tác và load vào datagrid
			DataTable tableMaPhongBan = MyUtility.Array_To_DataTable("tableMaPhongBan", m_listIDPhongBan);
			SqlParameter sqlParamArrUserIDD = new SqlParameter("@ArrUserIDD", SqlDbType.Structured) { Value = tableMaPhongBan };
			DataTable tableNhanVien = SqlDataAccessHelper.ExecSPQuery(SPName.sp_UserInfo_DocDSNVThaoTac.ToString(), sqlParamArrUserIDD);
			DataView viewNhanVien = new DataView(tableNhanVien);
			dgrdDSNVTrgPhg.DataSource = viewNhanVien;

			//load AutoComplete cho tb tìm kiếm
			var Source = new AutoCompleteStringCollection();
			Source.AddRange((from DataRow row in tableNhanVien.Rows select row["UserLastName"].ToString().ToUpperInvariant()).ToArray());
			tbSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tbSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			tbSearch.AutoCompleteCustomSource = Source;

			//load nhiệm vụ cho checkList
			DataTable tableNhiemVu = SqlDataAccessHelper.ExecSPQuery(SPName.sp_NhiemVu_DocBang.ToString());
			checkListNhiemVu.DataSource = tableNhiemVu;
			checkListNhiemVu.ValueMember = "MaNhiemVu";
			checkListNhiemVu.DisplayMember = "TenNhiemVu";
		}


		private void btnTim_Click(object sender, EventArgs e) {
			if (dgrdDSNVTrgPhg.DataSource == null) return;

			var searchStr1 = tbSearch.Text;
			var searchStr = string.Format("UserFullName like '%{0}%' or UserFullCode like '%{0}%'", searchStr1);
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = searchStr;
		}

		private void linkHienThiTatCaNV_Click(object sender, EventArgs e) {
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = string.Empty;
		}


		private void DangKyNhiemVu(DataRow NhanVien, List<DataRow> SelectedNhiemVu, ref bool haveOtherError, ref string otherError ) {
			string templateFormatStr = "Nhiệm vụ [{0}] cho [{1}] có mã NV [{2}].\n";

			foreach (var rowViewNhiemVu in SelectedNhiemVu) {
				var maNhiemVu = (int)rowViewNhiemVu["MaNhiemVu"];
				var tenNhiemVu = rowViewNhiemVu["TenNhiemVu"].ToString();
				var maChamCong = (int)NhanVien["UserEnrollNumber"];
				var tenNhanVien = NhanVien["UserLastName"].ToString();
				var maNhanVien = NhanVien["UserFullCode"].ToString();
				SqlParameter outPutParamKetQua = new SqlParameter("@KetQua", SqlDbType.Int) { Direction = ParameterDirection.Output, };
				var rowAffect = SqlDataAccessHelper.ExecSPNoneQuery(SPName.sp_NhiemVu_NhanVien_INS.ToString(),
															 new SqlParameter("@MaNhiemVu", maNhiemVu),
															 new SqlParameter("@UserEnrollNumber", maChamCong),
															 outPutParamKetQua);
				int ketqua = (int)outPutParamKetQua.Value;//ketqua == 0) không thêm vì đã tồn tại 
				if (ketqua == 1) {// thêm được--> kiểm tra số rowAffect
					if (rowAffect == 0) {
						haveOtherError = true;
						var fillValueTemplate = string.Format(templateFormatStr, tenNhiemVu, tenNhanVien, maNhanVien);
						otherError += fillValueTemplate;
						continue;
					}
				}
			}
		}


		private void btnDangKy_Click(object sender, EventArgs e) {
			#region //1. lấy danh sách nhân viên check, nếu chưa có nv nào check thì thông báo

			var listNV = (from DataGridViewRow dataGridViewRow in dgrdDSNVTrgPhg.Rows
						  let rowView = dataGridViewRow.DataBoundItem as DataRowView
						  where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
						  select rowView.Row)
						  .ToList();

			if (listNV.Count == 0) {
				ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000); 
				GC.Collect();
				return;
			}

			#endregion

			checkListNhiemVu.EndUpdate();
			List<DataRow> listSelectedNhiemVuChinh = (from DataRowView checkedItem in checkListNhiemVu.CheckedItems select checkedItem.Row).ToList();

			if (listSelectedNhiemVuChinh.Count == 0) {
				ACMessageBox.Show(string.Format(Resources.Text_ChuaChonXXX, "nhiệm vụ"), Resources.Caption_ThongBao, 2000);
				return;
			}
			else {
				string otherError = "Không đăng ký được các nhiệm vụ sau của Nhân viên (lỗi dữ liệu):\n";
				bool haveOtherError = false;

				for (int i = 0; i < listNV.Count; i++) {
					this.DangKyNhiemVu(listNV[i], listSelectedNhiemVuChinh, ref haveOtherError, ref otherError);
				}
				if (haveOtherError) {
					MessageBox.Show(otherError, Resources.Caption_Loi);
				}
				ACMessageBox.Show("Đã thực hiện xong.", Resources.Caption_ThongBao, 2000);
				//reload GUI : clear check list nhiệm vụ và danh sách nhân viên
				for (int i = 0; i < checkListNhiemVu.Items.Count; i++)
					checkListNhiemVu.SetItemChecked(i, false);
				checkAll_GridDSNV.Checked = false;
			}
		}

	}

}
