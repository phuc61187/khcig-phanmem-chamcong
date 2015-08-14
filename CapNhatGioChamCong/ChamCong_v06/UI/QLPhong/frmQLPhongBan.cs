using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v06.Helper;

namespace ChamCong_v06.UI.QLPhong {
	public partial class frmQLPhongBan : Form {
		public frmQLPhongBan() {
			InitializeComponent();
		}

		private void frmQLPhongBan_Load(object sender, EventArgs e) {
			//1. load tree Phòng ban , đăng ký sự kiện afterselect tại đây
			loadTreePhgBan(treePhongBan);
			treePhongBan.ExpandAll();
		}


		#region cách làm có store procedure
		public static TreeView loadTreePhgBan(TreeView tvDSPhongBan) {
			tvDSPhongBan.Nodes.Clear();
			DataTable tableDSPhong = SqlDataAccessHelper.ExecSPQuery(SPName6.RelationDept_DocPhongBanV6.ToString());

			var rowsPhong = (from DataRow row in tableDSPhong.Rows select row).OrderBy(s => (int)s["ViTri"]);
			var relationID_0 = rowsPhong.Where(o => (int)o["RelationID"] == 0).ToList().OrderBy(s => (int)s["ViTri"]);

			foreach (var dataRowView in relationID_0) {
				var enable = (bool)(dataRowView["Enable"]);
				var string1 = enable == false ? "[Disable]" : string.Empty;
				var string2 = dataRowView["Description"].ToString();
				TreeNode parentNode = new TreeNode { Text = string1 + string2, Tag = dataRowView };
				tvDSPhongBan.Nodes.Add(parentNode);
				loadTreeSubNode(ref parentNode, (int)dataRowView["ID"], rowsPhong/*TatcaPhongban*/);
			}


			return tvDSPhongBan;
		}

		public static void loadTreeSubNode(ref TreeNode ParentNode, int idPhongBanTrucThuoc, IOrderedEnumerable<DataRow> dsphongban /*List<cPhongBan> dsphongban*/) {
			IOrderedEnumerable<DataRow> childs = dsphongban.Where(item => (int)item["RelationID"] == idPhongBanTrucThuoc).ToList().OrderBy(item => (int)item["ViTri"]);
			foreach (DataRow phong in childs) {
				var enable = (bool)(phong["Enable"]);
				var string1 = enable == false ? "[Disable]" : string.Empty;
				var string2 = phong["Description"].ToString();

				TreeNode child = new TreeNode { Text = string1+string2, Tag = phong };
				ParentNode.Nodes.Add(child);
				loadTreeSubNode(ref child, (int)phong["ID"], dsphongban);
			}
		}

		#endregion

		private void btnThem_Click(object sender, EventArgs e) {
			//hiển thị form và nhận kết quả trả về
			frmNhapThongTinPhong frm = new frmNhapThongTinPhong();
			frm.m_CurrentDataRow = (DataRow)treePhongBan.SelectedNode.Tag;
			frm.m_Mode = ModeType.Them;
			frm.ShowDialog();
			if (frm.m_Mode == ModeType.Cancel) return;

			//sử dụng kết quả trả về thêm vào CSDL
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}
			#region test
			ACMessageBox.Show(string.Format("{0};RID {1};LID {2};VT {3};E {4}",
				frm.m_TenPhong,
				(int)frm.m_ParentDataRow["RelationID"],
				 (int)frm.m_ParentDataRow["LevelID"] + 1,
					frm.m_VitriPhong, frm.m_Enable), "", 5000);
			loadTreePhgBan(treePhongBan);
			//return;
			#endregion

			if (SqlDataAccessHelper.ExecSPNoneQuery(SPName6.RelationDept_ThemPhongV6.ToString(),
				new SqlParameter("@Description", frm.m_TenPhong),
				new SqlParameter("@RelationID", (int)frm.m_ParentDataRow["ID"]),
				new SqlParameter("@LevelID", (int)frm.m_ParentDataRow["LevelID"] + 1),//+1 vì là con của phòng trực thuộc
				new SqlParameter("@ViTri", frm.m_VitriPhong),
				new SqlParameter("@Enable", frm.m_Enable)) == 0) 
			{
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
				return;
			}
			else //thêm thành công load lại tree
			{
				loadTreePhgBan(treePhongBan);
				treePhongBan.ExpandAll();
			}
		}

		private void btnSua_Click(object sender, EventArgs e) {
			frmNhapThongTinPhong frm = new frmNhapThongTinPhong();
			frm.m_Mode = ModeType.Sua;
			frm.m_CurrentDataRow = (DataRow)treePhongBan.SelectedNode.Tag;
			frm.ShowDialog();
			if (frm.m_Mode == ModeType.Cancel) return;

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}
			#region test
			ACMessageBox.Show(string.Format("{0};RID {1};LID {2};VT {3};E {4}, ID:{5}",
				frm.m_TenPhong,
				(int)frm.m_ParentDataRow["RelationID"],
				 (int)frm.m_ParentDataRow["LevelID"] + 1,
					frm.m_VitriPhong,
			frm.m_Enable, (int)frm.m_CurrentDataRow["ID"]), "", 5000);
			loadTreePhgBan(treePhongBan);
			//return;
			#endregion

			if (SqlDataAccessHelper.ExecSPNoneQuery(SPName6.RelationDept_SuaPhongV6.ToString(),
				new SqlParameter("@ID", (int)frm.m_CurrentDataRow["ID"]),
				new SqlParameter("@Description", frm.m_TenPhong),
				new SqlParameter("@RelationID", (int)frm.m_ParentDataRow["ID"]),
				new SqlParameter("@LevelID", (int)frm.m_ParentDataRow["LevelID"] + 1),//+1 vì là con của phòng trực thuộc
				new SqlParameter("@ViTri", (int)frm.m_CurrentDataRow["ViTri"]),
				new SqlParameter("@Enable", frm.m_Enable)) == 0) {
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
				return;
			}
			else //thêm thành công load lại tree
			{
				loadTreePhgBan(treePhongBan);
				treePhongBan.ExpandAll();
			}

		}

		private void btnXoa_Click(object sender, EventArgs e) {
			//1. hỏi trước khi xóa
			//2. thực hiện xóa //todo nâng cấp lên chức năng disable
			if (MessageBox.Show("Xóa phòng ban sẽ mất toàn bộ thông tin lịch sử liên quan đến phòng ban này!" +
								"Cân nhắc sử dụng chức năng cập nhật tình trạng về disable." +
								"Bạn muốn TIẾP TỤC XÓA phòng ban này?", Resources.Caption_XacNhan, MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning) == DialogResult.No) {
				return;
			}

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			DataRow CurrentDataRow = (DataRow) treePhongBan.SelectedNode.Tag;
			if (SqlDataAccessHelper.ExecSPNoneQuery(SPName6.RelationDept_XoaPhongV6.ToString(),
				new SqlParameter("@ID", (int)CurrentDataRow["ID"])) == 0) {
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
				return;
			}
			else //thêm thành công load lại tree
			{
				loadTreePhgBan(treePhongBan);
				treePhongBan.ExpandAll();
			}
		}


		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			e.Node.Expand();
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			Close();
		}

	}
}
