using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.BUS;
using ChamCong_v06.Helper;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;

namespace ChamCong_v06.UI.QLTaiKhoan {
	#region LOGIC
	/*	1. LOAD FORM LẦN ĐÂÙ
	 *		1.1 load toàn bộ danh sách phòng ban cho cây phân quyền ( trừ những phòng bị Disable )
	 *		1.2 load toàn bộ phân quyền
	 *		1.3 load danh sách tài khoản
	 *	2. TÙY THEO TỪNG TÀI KHOẢN được chọn
	 *		2.1 load DataTable phòng ban được thao tác -> thực hiện check
	 */
	#endregion
	public partial class frmQLTaiKhoan : Form {
		#region các hàm ko quan trọng
		public frmQLTaiKhoan() {
			InitializeComponent();
		}

		#endregion

		#region cách làm có store procedure
		public static TreeView loadTreePhgBan(TreeView tvDSPhongBan) {
			tvDSPhongBan.Nodes.Clear();
			DataTable tableDSPhong = SqlDataAccessHelper.ExecSPQuery(SPName.RelationDept_DocTatCaPhongBan.ToString(),
				new SqlParameter("@Enable", true));// ko load những phòng ban bị disable

			var rowsPhong = (from DataRow row in tableDSPhong.Rows select row).OrderBy(s => (int)s["ViTri"]);
			var relationID_0 = rowsPhong.Where(o => (int)o["RelationID"] == 0).ToList().OrderBy(s => (int)s["ViTri"]);

			foreach (var dataRowView in relationID_0) {
				//var enable = (bool)(dataRowView["Enable"]);
				//if (enable == false) continue; 
				var string2 = dataRowView["Description"].ToString();
				TreeNode parentNode = new TreeNode { Text = string2, Tag = dataRowView };
				tvDSPhongBan.Nodes.Add(parentNode);
				loadTreeSubNode(ref parentNode, (int)dataRowView["ID"], rowsPhong/*TatcaPhongban*/);
			}


			return tvDSPhongBan;
		}

		public static void loadTreeSubNode(ref TreeNode ParentNode, int idPhongBanTrucThuoc, IOrderedEnumerable<DataRow> dsphongban) {
			IOrderedEnumerable<DataRow> childs = dsphongban.Where(item => (int)item["RelationID"] == idPhongBanTrucThuoc).ToList().OrderBy(item => (int)item["ViTri"]);
			foreach (DataRow phong in childs) {
				//var enable = (bool)(phong["Enable"]);
				//if (enable == false) continue; // ko load những phòng ban bị disable
				var string2 = phong["Description"].ToString();
				TreeNode child = new TreeNode { Text = string2, Tag = phong };
				ParentNode.Nodes.Add(child);
				loadTreeSubNode(ref child, (int)phong["ID"], dsphongban);
			}
		}

		#endregion

		private void frmQLTaiKhoan_Load(object sender, EventArgs e)
		{
			loadTreePhgBan(treePhongBan);
			this.gridViewTaiKhoan.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewTaiKhoan_FocusedRowChanged);
			LoadDSTaiKhoan();
			//this.gridViewTaiKhoan.Focus();
			//LoadDSPhanQuyen();
		}

		private void LoadDSTaiKhoan()
		{
			DataTable tableTaiKhoan = SqlDataAccessHelper.ExecSPQuery(SPName6.NewUserAccount_DocTatCaTaiKhoanV6.ToString());
			gridControlTaiKhoan.DataSource = tableTaiKhoan;
		}

		private DataTable LoadDataTable_PhanQuyenPhongBan(int UserID)
		{
			DataTable tablePhongBanThaoTac = SqlDataAccessHelper.ExecSPQuery(SPName6.DeptPrivilege_DocPhongBanThaoTacV6.ToString(),
				new SqlParameter("@UserID", UserID),
				new SqlParameter("@IsYes", true),
				new SqlParameter("@RelationDeptEnable", true));
			// đọc lên UserID, ID phòng được thao tác [IDD]
			return tablePhongBanThaoTac;
		}

		private void gridViewTaiKhoan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
			// nếu ko có focus vào dòng nào thì thoát form, nếu có thì xử lý load phòng ban thao tác và phân quyền
			if (e.FocusedRowHandle == GridControl.InvalidRowHandle)
			{
				ACMessageBox.Show("Không có dòng dữ liệu nào được chọn. Form tự động đóng.", Resources.Caption_ThongBao, 2000);
				Close();
			}
			if (e.FocusedRowHandle < 0) { return;}
			//có focus, load phòng và phân quyền theo tài khoản này
			//xác định userID 
			DataRow dataRow = gridViewTaiKhoan.GetDataRow(e.FocusedRowHandle);
			if (dataRow == null) {ACMessageBox.Show("DataRow Null", Resources.Caption_Loi, 2000); return;}
			int userID = (int) dataRow["UserID"];
			// load dữ liệu phân quyền phòng ban và thực hiện check
			treePhongBan.AfterCheck -= treePhongBan_OnAfterCheck;
			DataTable tablePhanQuyenPhongBan = LoadDataTable_PhanQuyenPhongBan(userID);
			PopulateData_ToTreePhong(tablePhanQuyenPhongBan);
			treePhongBan.AfterCheck += treePhongBan_OnAfterCheck;
		}

		private void treePhongBan_OnAfterCheck(object sender, TreeViewEventArgs treeViewEventArgs)
		{
			TreeNode root = treeViewEventArgs.Node;
			DataRow dataRow = (DataRow) root.Tag;
			var checkStatus = root.Checked;
			int idPhong = (int) dataRow["ID"];
			treePhongBan.AfterCheck -= treePhongBan_OnAfterCheck;
			SetCheckStatus_AllTreeNode(root, checkStatus);
			treePhongBan.AfterCheck += treePhongBan_OnAfterCheck;
			root = XL.ReturnRootNode(root);
			root = FindNode(idPhong, root);
		}

		private void PopulateData_ToTreePhong(DataTable tablePhanQuyenPhongBan) {
			//logic: thực hiện xóa check toàn bộ 
			TreeNode root = XL.ReturnRootNode(treePhongBan.TopNode);
			SetCheckStatus_AllTreeNode(root, false);
			treePhongBan.Invalidate();
			root = XL.ReturnRootNode(root);
			SetCheckNode_PhongBanThaoTac(root, tablePhanQuyenPhongBan);
		}

		private void SetCheckNode_PhongBanThaoTac(TreeNode root, DataTable tablePhanQuyenPhongBan)
		{
			foreach (DataRow dataRow in tablePhanQuyenPhongBan.Rows)
			{
				root = XL.ReturnRootNode(root);
				int idPhong = (int) dataRow["IDD"];
				TreeNode node = FindNode(idPhong, root);
				if (node== null) continue; // ko tìm thấy  node thì đi tiếp
				node.Checked = true;
			}

		}

		private TreeNode FindNode(int IDPhong, TreeNode root) {
			DataRow row = (DataRow)root.Tag; // mỗi dòng sẽ có ID, RelationID (phòng cha)
			if (IDPhong == (int)row["ID"]) return root;
			if (root.Nodes.Count > 0) {
				for (int i = 0; i < root.Nodes.Count; i++) {
					TreeNode node = FindNode(IDPhong, root.Nodes[i]);
					if (node != null) return node;
				}
			}
			return null;
		}


		private void SetCheckStatus_AllTreeNode(TreeNode root, bool Status) {
			if (root == null) return;
			root.Checked = Status;
			for (int i = 0; i < root.Nodes.Count; i++)
			{
				SetCheckStatus_AllTreeNode(root.Nodes[i], Status);
			}
		}

		#region xóa tài khoản
		private void btnXoa_Click(object sender, EventArgs e) {
			#region kiểm tra đang chọn dòng nào

			if (gridViewTaiKhoan.FocusedRowHandle == GridControl.InvalidRowHandle)
			{
				ACMessageBox.Show("Không có tài khoản.", Resources.Caption_ThongBao, 2000);
				return;
			}
			if (gridViewTaiKhoan.FocusedRowHandle < 0)
			{
				ACMessageBox.Show("Vui lòng chọn tài khoản.", Resources.Caption_ThongBao, 2000);
				return;
			}

			#endregion

			//xác định tài khoản sẽ xóa
			int focusRowHandle = gridViewTaiKhoan.FocusedRowHandle;
			DataRow dataRow = gridViewTaiKhoan.GetDataRow(focusRowHandle);
			if (dataRow == null) return;
			int userID = (int) dataRow["UserID"];
			string userAccount = dataRow["UserAccount"].ToString();
			string template = "Bạn muốn xóa tài khoản [{0}]?";
			if (MessageBox.Show(string.Format(template, userAccount), Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No)
			{
				return;
			}
			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.NewUserAccount_XoaTaiKhoanV6.ToString(),
			                                             new SqlParameter("@UserID", userID));
			if (kq == 0)
			{
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
				return;
			}
		}
		#endregion


	}
}
