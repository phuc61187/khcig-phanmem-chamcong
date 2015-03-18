using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ChamCong_v02.BUS;
using ChamCong_v02.DAO;
using log4net;

namespace ChamCong_v02 {
	public partial class frm_43_ThayDoiHeSoLuong_PC : Form {

		public readonly ILog lg = LogManager.GetLogger("frm_43_ThayDoiHeSoLuong_PC");

		public List<int> m_listIDPhongBan { get; set; }


		public frm_43_ThayDoiHeSoLuong_PC() {
			InitializeComponent();

			log4net.Config.XmlConfigurator.Configure();

			//1. không cho autogen các column khi bind dữ liệu: 
			dgrdDSNVTrgPhg.AutoGenerateColumns = false;

		}

		#region load treeview
		public TreeView loadTreePhgBan(TreeView tvDSPhongBan, DataTable pDataTable) {
			if (pDataTable.Rows.Count > 0) {
				foreach (DataRow dataRow in pDataTable.Select("RelationID = 0")) {
					TreeNode ParentNode = new TreeNode { Text = dataRow["Description"].ToString(), Tag = (int)dataRow["ID"] };
					tvDSPhongBan.Nodes.Add(ParentNode);
					loadTreeSubNode(ref ParentNode, (int)(dataRow["ID"]), pDataTable);
				}
			}
			return tvDSPhongBan;
		}

		private void loadTreeSubNode(ref TreeNode ParentNode, int ParentId, DataTable dtMenu) {
			DataRow[] childs = dtMenu.Select("RelationID = " + ParentId);
			foreach (DataRow dRow in childs) {
				TreeNode child = new TreeNode { Text = dRow["Description"].ToString(), Tag = (int)dRow["ID"], ToolTipText = dRow["Description"].ToString() };
				ParentNode.Nodes.Add(child);
				//Recursion Call
				loadTreeSubNode(ref child, (int)dRow["ID"], dtMenu);
			}
		}

		void GetIDLeafNodeExceptParent(TreeNode root, List<int> listID) {
			if (root == null) return;
			if (root.FirstNode == null) {
				listID.Add((int)root.Tag);
				//lg.Debug(root.Tag + " " + root.Text);
				GetIDLeafNodeExceptParent(root.NextNode, listID);
			}
			if (root.FirstNode != null) {
				foreach (TreeNode node in root.Nodes) {
					GetIDLeafNodeExceptParent(node, listID);
				}
			}
		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {

			if (m_listIDPhongBan == null) m_listIDPhongBan = new List<int>();
			else m_listIDPhongBan.Clear();
			if (e.Node.FirstNode != null) GetIDLeafNodeExceptParent(e.Node, m_listIDPhongBan);
			else m_listIDPhongBan.Add((int)e.Node.Tag);
			e.Node.Expand();

			DataTable table = DAL.LayDSNV(m_listIDPhongBan.ToArray());
			table.Columns.Add("check", typeof(bool));
			table.Columns["check"].DefaultValue = false;

			dgrdDSNVTrgPhg.DataSource = table;
		}

		#endregion


		private void frm_DieuChinhLuongThangTruoc_Load(object sender, EventArgs e) {


			m_listIDPhongBan = new List<int>();
			DataTable tablePhong = DAL.LayDSPhong(ThamSo.currUserID);
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

			cbTieuChi.SelectedIndex = 0;
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void btnThucHien_Click(object sender, EventArgs e) {

			int iMaCC = (int)tbMaCC.Tag;
			if (int.TryParse(iMaCC.ToString(), out iMaCC) == false) {
				AutoClosingMessageBox.Show("Mã Nhân viên chưa đúng, vui lòng điền lại.", "Lỗi", 3000);
				return;
			}

			tbHSLCB.Update();
			tbHSLSP.Update();
			Single hslcb = Convert.ToSingle(Math.Round((tbHSLCB.Value), 2));
			Single hslsp = Convert.ToSingle(Math.Round((tbHSLSP.Value), 2));
			Single hsBHCongThem = Convert.ToSingle(tbHSBHCongThem.Text);
			//if (hslcb>)


			bool kq = DAL.CapNhatHeSoLuong(iMaCC, hslcb, hslsp, hsBHCongThem);
			if (kq == false) MessageBox.Show("Mất kết nối với máy chủ. Vui lòng thử lại.");
			else {
				tbHSLCB.Value = 0d;
				tbHSLSP.Value = 0d;
				tbHSBHCongThem.Text = "00";
				MessageBox.Show("Thực hiện thành công.");
				DataTable table = DAL.LayDSNV(m_listIDPhongBan.ToArray());
				table.Columns.Add("check", typeof(bool));
				table.Columns["check"].DefaultValue = false;

				dgrdDSNVTrgPhg.DataSource = table;

			}
		}



		private void btnTim_Click(object sender, EventArgs e) {
			int tieuchi = cbTieuChi.SelectedIndex;
			DataTable table;

			DataView view = dgrdDSNVTrgPhg.DataSource as DataView;
			if (view != null) {
				table = view.Table;
			}
			else {
				table = dgrdDSNVTrgPhg.DataSource as DataTable;
			}
			if (table == null || table.Rows.Count == 0) return;

			if (tieuchi == 0) { // tìm theo mã cc
				string manv = tbSearch.Text;
				if (string.IsNullOrWhiteSpace(manv)) {
					return;
				}
				else {
					DataView newview = new DataView(table, "UserFullCode like '%" + manv + "%'", "UserFullCode asc", DataViewRowState.CurrentRows);

					if (newview.Count == 0) {
						AutoClosingMessageBox.Show("Không tìm được nhân viên này.", "Thông báo", 2000);
						return;
					}
					else {
						dgrdDSNVTrgPhg.DataSource = newview;
					}
				}
			}
			else { // tìm theo tên nv
				string tennv = tbSearch.Text;
				if (string.IsNullOrWhiteSpace(tennv)) {
					return;
				}
				else {
					DataView newview = new DataView(table, "UserFullName like '%" + tennv + "%'", "UserEnrollNumber asc", DataViewRowState.CurrentRows);

					if (newview.Count == 0) {
						AutoClosingMessageBox.Show("Không tìm được nhân viên này.", "Thông báo", 2000);
						return;
					}
					else {
						dgrdDSNVTrgPhg.DataSource = newview;
					}
				}
			}
		}

		private void dgrdDSNVTrgPhg_SelectionChanged(object sender, EventArgs e) {
			if (dgrdDSNVTrgPhg.SelectedRows.Count == 0) return;

			DataRowView rowView = dgrdDSNVTrgPhg.SelectedRows[0].DataBoundItem as DataRowView;
			if (rowView == null) return;
			DataRow row = rowView.Row;
			tbMaCC.Tag = (int)row["UserEnrollNumber"];
			tbMaCC.Text = row["UserFullCode"].ToString();
			tbTenNV.Text = (string)row["UserFullName"];
			tbHSLCB.Value = row["HeSoLuongCB"] != DBNull.Value ? Convert.ToDouble(row["HeSoLuongCB"]) : 0d;
			tbHSLSP.Value = row["HeSoLuongSP"] != DBNull.Value ? Convert.ToDouble(row["HeSoLuongSP"]) : 0d;
			tbHSBHCongThem.Text = row["HSBHCongThem"] != DBNull.Value ? Convert.ToString(row["HSBHCongThem"]) : "00";
			
		}

		private void tbHSBHCongThem_MouseHover(object sender, EventArgs e) {
			toolTip1.Show("Hệ số bảo hiểm cộng thêm cho chức danh Giám đốc và Phó Giám đốc.", tbHSBHCongThem, 5000);
		}

	}
}
