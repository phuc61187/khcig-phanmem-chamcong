using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;

namespace ChamCong_v03.DieuChinhLuong {
	public partial class frmThemDieuChinhLuong : Form {
		public DateTime m_thang;
		public bool IsReload = false;
		public List<int> m_listIDPhongBan = new List<int>();
		public List<cUserInfo> m_DSNV = new List<cUserInfo>();
		public DataTable m_Bang_DSNV;

		public DataTable TaoBang_DSNV() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "check", "cUserInfo", "UserEnrollNumber", "UserFullCode", "UserFullName", "SchID", "SchName", "TitleName", "IDD_1", "Description_1", "HeSoLuongCB", "HeSoLuongSP", "HSBHCongThem" },
				new[] { typeof(bool), typeof(cUserInfo), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(double), typeof(double), typeof(double) });
			return kq;
		}

		public frmThemDieuChinhLuong() {
			InitializeComponent();

			m_listIDPhongBan = new List<int>();
			m_DSNV = new List<cUserInfo>();
			m_Bang_DSNV = TaoBang_DSNV();

			//1. không cho autogen các column khi bind dữ liệu: 4 cái
			dgrdDSNVTrgPhg.AutoGenerateColumns = false;
			DataView dataView = new DataView(m_Bang_DSNV);
			dgrdDSNVTrgPhg.DataSource = dataView;


			//3. vẽ 3 checkbox checkall cho DSNV trong phòng
			XL2.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));

		}

		#region load treeview new

		public TreeView loadTreePhgBan(TreeView tvDSPhongBan, DataTable pDataTable) {
			if (pDataTable.Rows.Count > 0) {
				foreach (var dataRow in pDataTable.Select("RelationID = 0", "ViTri asc")) {
					var ParentNode = new TreeNode { Text = dataRow["Description"].ToString(), Tag = (int)dataRow["ID"] };
					tvDSPhongBan.Nodes.Add(ParentNode);
					loadTreeSubNode(ref ParentNode, (int)(dataRow["ID"]), pDataTable);
				}
			}
			return tvDSPhongBan;
		}

		private void loadTreeSubNode(ref TreeNode ParentNode, int ParentId, DataTable dtMenu) {
			var childs = dtMenu.Select("RelationID = " + ParentId, "ViTri asc");
			foreach (var dRow in childs) {
				var child = new TreeNode { Text = dRow["Description"].ToString(), Tag = (int)dRow["ID"], ToolTipText = dRow["Description"].ToString() };
				ParentNode.Nodes.Add(child);
				//Recursion Call
				loadTreeSubNode(ref child, (int)dRow["ID"], dtMenu);
			}
		}

		private void GetIDLeafNodeExceptParent(TreeNode root, List<int> listID) {
			if (root == null) return;

			listID.Add((int)root.Tag);

			if (root.Nodes.Count > 0) {
				foreach (TreeNode node in root.Nodes) {
					GetIDLeafNodeExceptParent(node, listID);
				}
			}
			// xuốn đến đây tương đương root.Nodes.Count== 0; return
		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {

			m_listIDPhongBan.Clear();
			if (e.Node.FirstNode != null) GetIDLeafNodeExceptParent(e.Node, m_listIDPhongBan);
			else m_listIDPhongBan.Add((int)e.Node.Tag);
			e.Node.Expand();

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				this.Close();
				return;
			}

			var table = DAL.LayDSNV_TruCongNhat(m_listIDPhongBan.ToArray());
			if (table.Rows.Count == 0) return;
			m_DSNV.Clear();
			XL.KhoiTaoDSNV(m_DSNV, table);
			m_Bang_DSNV.Rows.Clear();
			XL.TaoTableDSNV(m_DSNV, m_Bang_DSNV);
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			var Source = new AutoCompleteStringCollection();
			Source.AddRange((from nv in m_DSNV select nv.TenNV.ToUpperInvariant()).ToArray());
			tbSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tbSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			tbSearch.AutoCompleteCustomSource = Source;
			dataView.RowFilter = string.Empty;
			checkAll_GridDSNV.Checked = false;
		}

		#endregion
		#region 3 biến checkbox check all: 1 của DSNV cần xem công. 2 DS Công của NV

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

		private void Form1_Load(object sender, EventArgs e)
		{
			IsReload = false;
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				this.Close();
				return;
			}

			dtpThang.Value = m_thang;

			var tablePhong = DAL.LayDSTatCaPhongBan();
			if (tablePhong.Rows.Count == 0) {
				AutoClosingMessageBox.Show("Bạn chưa được phân quyền thao tác.", "Thông báo", 2000);
				return;
			}
			//2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan : xoá dữ liệu trước và load
			treePhongBan.Nodes.Clear();
			loadTreePhgBan(treePhongBan, tablePhong);

			// đăng ký sự kiện cho tree và chọn topNode
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;
			treePhongBan.SelectedNode = treePhongBan.TopNode;
			XL2.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));

		}

		private void btnThucHien_Click(object sender, EventArgs e)
		{
			IsReload = true;
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			//lấy các nv được chọn
			IEnumerable<DataGridViewRow> lstGridViewRow = dgrdDSNVTrgPhg.Rows.Cast<DataGridViewRow>();
			var listNV = (from row in (lstGridViewRow)
						  where ((DataRowView)row.DataBoundItem)["check"] != DBNull.Value && (bool)((DataRowView)row.DataBoundItem)["check"]
						  select (cUserInfo)((DataRowView)row.DataBoundItem).Row["cUserInfo"]).ToList();

			if (listNV.Count == 0) {
				return;
			}
			// lấy thông tin đã nhập
			var luongdieuchinh = (Double)numLuongDieuChinh.Value;
			var tamung = (Double)numTamUngThang.Value;
			var thuchikhac = (Double)numThuChiKhac.Value;
			var thang = dtpThang.Value;
			var Mucdongbh = 0f;
			if (Single.TryParse(tbMucDongBH.Text, out Mucdongbh) == false)
			{
				AutoClosingMessageBox.Show("Mức đóng BHXH không hợp lệ. Vui lòng nhập lại.", "Thông báo", 4000);
				return;
			}

			// thực hiện
			var flag = false;
			foreach (var nv in listNV) {
				var UserEnrollNumber = nv.MaCC;
				var kq = DAL.CapNhatLuongDieuChinh_TamUng_ThuChiKhac(UserEnrollNumber, thang.Month, thang.Year, luongdieuchinh, tamung, thuchikhac, Mucdongbh);
				if (kq == 0) {
					flag = true;
					MessageBox.Show("Xảy ra lỗi trong quá trình thực hiện. Vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK);
					return;
				}
			}
			if (flag == false) {
				AutoClosingMessageBox.Show("Thực hiện thành công.", "Thông báo", 2000);
			}
		}

		private void btnTim_Click(object sender, EventArgs e) {
			if (dgrdDSNVTrgPhg.DataSource == null) return;

			var searchStr1 = tbSearch.Text;
			var searchStr = string.Format("UserFullName like '%{0}%' or UserFullCode like '%{0}%'", searchStr1);
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			dataView.RowFilter = searchStr;

		}

		private void btnThoat_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void linkHienThiTatCaNV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			dataView.RowFilter = string.Empty;
		}

	}
}
