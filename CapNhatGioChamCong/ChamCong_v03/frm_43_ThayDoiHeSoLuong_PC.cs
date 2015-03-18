using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;
using log4net;

namespace ChamCong_v03 {
	public partial class frm_43_ThayDoiHeSoLuong_PC : Form {

		public readonly ILog lg = LogManager.GetLogger("frm_43_ThayDoiHeSoLuong_PC");

		public List<int> m_listIDPhongBan = new List<int>();
		public List<cUserInfo> m_DSNV = new List<cUserInfo>();
		public DataTable m_Bang_DSNV;
		public DataTable TaoBang_DSNV_ThayDoiThongTin() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "check", "cUserInfo", "UserEnrollNumber", "UserFullCode", "UserFullName", "SchID", "SchName", "TitleName", "IDD_1", "Description_1", "HeSoLuongCB", "HeSoLuongSP", "HSBHCongThem", "UserEnabled", "TinhLuongCongNhat" },
				new[] { typeof(bool), typeof(cUserInfo), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(double) , typeof(double) , typeof(double), typeof(bool), typeof(bool) });
			return kq;
		}


		public frm_43_ThayDoiHeSoLuong_PC() {
			InitializeComponent();
			log4net.Config.XmlConfigurator.Configure();
			m_listIDPhongBan = new List<int>();
			m_DSNV = new List<cUserInfo>();
			m_Bang_DSNV = TaoBang_DSNV_ThayDoiThongTin();

			//1. không cho autogen các column khi bind dữ liệu: 4 cái
			dgrdDSNVTrgPhg.AutoGenerateColumns = false;
			DataView dataView = new DataView(m_Bang_DSNV);
			dgrdDSNVTrgPhg.DataSource = dataView;


		}

		#region load treeview
		public TreeView loadTreePhgBan(TreeView tvDSPhongBan, DataTable pDataTable) {
			if (pDataTable.Rows.Count > 0) {
				foreach (DataRow dataRow in pDataTable.Select("RelationID = 0", "ViTri asc")) {
					TreeNode ParentNode = new TreeNode { Text = dataRow["Description"].ToString(), Tag = (int)dataRow["ID"] };
					tvDSPhongBan.Nodes.Add(ParentNode);
					loadTreeSubNode(ref ParentNode, (int)(dataRow["ID"]), pDataTable);
				}
			}
			return tvDSPhongBan;
		}

		private void loadTreeSubNode(ref TreeNode ParentNode, int ParentId, DataTable dtMenu) {
			DataRow[] childs = dtMenu.Select("RelationID = " + ParentId, "ViTri asc");
			foreach (DataRow dRow in childs) {
				TreeNode child = new TreeNode { Text = dRow["Description"].ToString(), Tag = (int)dRow["ID"], ToolTipText = dRow["Description"].ToString() };
				ParentNode.Nodes.Add(child);
				//Recursion Call
				loadTreeSubNode(ref child, (int)dRow["ID"], dtMenu);
			}
		}

		void GetIDLeafNodeExceptParent(TreeNode root, List<int> listID) {
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
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 4000);
				this.Close();
				return;
			}

			DataTable table = DAL.LayDSNV_ThayDoiThongTin(m_listIDPhongBan.ToArray());
			if (table.Rows.Count == 0) return;
			m_DSNV.Clear();
			XL.KhoiTaoDSNV_ThayDoiThongTin(m_DSNV, table);
			m_Bang_DSNV.Rows.Clear();
			XL.TaoTableDSNV_ThayDoiThongTin(m_DSNV, m_Bang_DSNV);
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			var Source = new AutoCompleteStringCollection();
			Source.AddRange((from nv in m_DSNV select nv.TenNV.ToUpperInvariant()).ToArray());
			tbSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tbSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			tbSearch.AutoCompleteCustomSource = Source;
			dataView.RowFilter = string.Empty;
		}

		#endregion


		private void frm_DieuChinhLuongThangTruoc_Load(object sender, EventArgs e) {


			DataTable tablePhong = DAL.LayDSPhong(XL2.currUserID);
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

		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void btnThucHien_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			int iMaCC = (int)tbMaCC.Tag;
			if (int.TryParse(iMaCC.ToString(), out iMaCC) == false) {
				AutoClosingMessageBox.Show("Mã Nhân viên chưa đúng, vui lòng điền lại.", "Lỗi", 3000);
				return;
			}
			tbHSLCB.Update();
			tbHSLCV.Update();
			Single hslcb = Single.Parse(tbHSLCB.Text);
            Single hslsp = Single.Parse(tbHSLCV.Text);
            Single hsBHCongThem = Single.Parse(tbHSBHCongThem.Text);
			bool UserEnabled = checkUserEnabled.Checked;
			bool TinhLuongCongNhat = checkTinhLuongCongNhat.Checked;


			bool kq = DAL.CapNhatHeSoLuong(iMaCC, hslcb, hslsp, hsBHCongThem, UserEnabled, TinhLuongCongNhat);
			if (kq == false) MessageBox.Show("Mất kết nối với máy chủ. Vui lòng thử lại.");
			else {
				tbHSLCB.Text = "0.00";
			    tbHSLCV.Text = "0.00";
				tbHSBHCongThem.Text = "0.00";
				MessageBox.Show("Thực hiện thành công.");
/*
				DataTable table = DAL.LayDSNV_ThayDoiThongTin(m_listIDPhongBan.ToArray());
				table.Columns.Add("check", typeof(bool));
				table.Columns["check"].DefaultValue = false;

				dgrdDSNVTrgPhg.DataSource = table;
*/


				DataTable table = DAL.LayDSNV_ThayDoiThongTin(m_listIDPhongBan.ToArray());
				if (table.Rows.Count == 0) return;
				m_DSNV.Clear();
				XL.KhoiTaoDSNV_ThayDoiThongTin(m_DSNV, table);
				m_Bang_DSNV.Rows.Clear();
				XL.TaoTableDSNV_ThayDoiThongTin(m_DSNV, m_Bang_DSNV);
				var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
				var Source = new AutoCompleteStringCollection();
				Source.AddRange((from nv in m_DSNV select nv.TenNV.ToUpperInvariant()).ToArray());
				tbSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
				tbSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
				tbSearch.AutoCompleteCustomSource = Source;
				dataView.RowFilter = string.Empty;

			}
		}



		private void btnTim_Click(object sender, EventArgs e) {
			if (dgrdDSNVTrgPhg.DataSource == null) return;

			var searchStr1 = tbSearch.Text;
			var searchStr = string.Format("UserFullName like '%{0}%' or UserFullCode like '%{0}%'", searchStr1);
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			dataView.RowFilter = searchStr;

		}

		private void dgrdDSNVTrgPhg_SelectionChanged(object sender, EventArgs e) {
			if (dgrdDSNVTrgPhg.SelectedRows.Count == 0) return;

			DataRowView rowView = dgrdDSNVTrgPhg.SelectedRows[0].DataBoundItem as DataRowView;
			if (rowView == null) return;
			DataRow row = rowView.Row;
			tbMaCC.Tag = (int)row["UserEnrollNumber"];
			tbMaCC.Text = row["UserFullCode"].ToString();
			tbTenNV.Text = (string)row["UserFullName"];
			tbHSLCB.Text = row["HeSoLuongCB"] != DBNull.Value ? Convert.ToDouble(row["HeSoLuongCB"]).ToString("0.00") : "0.00";
            tbHSLCV.Text = row["HeSoLuongSP"] != DBNull.Value ? Convert.ToDouble(row["HeSoLuongSP"]).ToString("0.00") : "0.00";
            tbHSBHCongThem.Text = row["HSBHCongThem"] != DBNull.Value ? Convert.ToDouble(row["HSBHCongThem"]).ToString("0.00") : "0.00";
			checkTinhLuongCongNhat.Checked = row["TinhLuongCongNhat"] != DBNull.Value ? Convert.ToBoolean(row["TinhLuongCongNhat"]) : false;
			checkUserEnabled.Checked = row["UserEnabled"] != DBNull.Value ? Convert.ToBoolean(row["UserEnabled"]) : false;
		}

		private void linkHienThiTatCaNV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			dataView.RowFilter = string.Empty;

		}


	}
}
