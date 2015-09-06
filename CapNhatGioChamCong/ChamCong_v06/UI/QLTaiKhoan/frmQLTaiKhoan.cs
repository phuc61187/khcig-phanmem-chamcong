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
			// làm sạch danh sách node trước
			tvDSPhongBan.Nodes.Clear();
			// load những phòng ban được Enable, sắp xếp theo vị trí
			DataTable tableDSPhong = SqlDataAccessHelper.ExecSPQuery(SPName6.RelationDept_DocPhongBanV6.ToString(),
				new SqlParameter("@Enable", true));// ko load những phòng ban bị disable
			var rowsPhong = (from DataRow row in tableDSPhong.Rows select row).OrderBy(s => (int)s["ViTri"]);
			// xác định root node là Node luôn có RelationID = 0(IDCha = 0 tức là gốc ko có cha nữa)
			// nếu ko tìm được node root này thì thoát form
			var relationID_0 = rowsPhong.Where(o => (int)o["RelationID"] == 0).ToList().OrderBy(s => (int)s["ViTri"]);
			if (!relationID_0.Any()) return null; // return null cho biết ko load được tree

			// sau khi xác định root thì lần lượt load từng subNode vào và gán tag là dataRow phòng
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
				var string2 = phong["Description"].ToString();
				TreeNode child = new TreeNode { Text = string2, Tag = phong };
				ParentNode.Nodes.Add(child);
				loadTreeSubNode(ref child, (int)phong["ID"], dsphongban);
			}
		}

		#endregion

		private void frmQLTaiKhoan_Load(object sender, EventArgs e) {
			// load tree phòng ban, nếu tree null thì báo và đóng form
			treePhongBan = loadTreePhgBan(treePhongBan);
			if (treePhongBan == null)
			{
				ACMessageBox.Show(Properties.Resources.Text_ChuaDuocPhanQuyenPhongBan, Resources.Caption_ThongBao, 2000);	
				Close();
			}
			//Load danh sách phân quyền
			LoadDSPhanQuyen();
			this.gridViewTaiKhoan.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewTaiKhoan_FocusedRowChanged);
			// load danh sách tất cả tài khoản (kể cả disable)
			LoadDSTaiKhoan();
		}

		private void LoadDSPhanQuyen() {
			DataTable tableChucNang = SqlDataAccessHelper.ExecSPQuery(SPName6.Function_DocChucNangV6.ToString());
			checkedListChucNang.DataSource = tableChucNang;
			checkedListChucNang.ValueMember = "ID";
			checkedListChucNang.DisplayMember = "Description";
		}

		private void LoadDSTaiKhoan() {
			DataTable tableTaiKhoan = SqlDataAccessHelper.ExecSPQuery(SPName6.NewUserAccount_DocTatCaTaiKhoanV6.ToString());
			gridControlTaiKhoan.DataSource = tableTaiKhoan;
		}

		private DataTable LoadDataTable_PhanQuyenPhongBan(int UserID) {
			// đọc lên danh sách phòng được enable và user được phép thao tác
			DataTable tablePhongBanThaoTac = SqlDataAccessHelper.ExecSPQuery(SPName6.DeptPrivilege_DocPhongBanThaoTacV6.ToString(),
				new SqlParameter("@UserID", UserID),
				new SqlParameter("@ChoPhepThaoTac", true),
				new SqlParameter("@RelationDeptEnable", true));
			// đọc lên UserID, ID phòng được thao tác [IDD]
			return tablePhongBanThaoTac;
		}

		private void PopulateData_ToTreePhong(DataTable tablePhanQuyenPhongBan) {
			//logic: thực hiện load trạng thái ban đầu (uncheck toàn bộ): đưa về root và thực hiện từ trên xuống
			TreeNode root = GeneralBUS.ReturnRootNode(treePhongBan.TopNode);
			SetCheckStatus_AllTreeNode(root, false);
			treePhongBan.Invalidate();
			// duyệt từ root và check những phòng được thao tác, uncheck và phòng ko được thao tác
			root = GeneralBUS.ReturnRootNode(root);
			SetCheckNode_PhongBanThaoTac(root, tablePhanQuyenPhongBan);
		}

		private DataTable LoadDataTable_PhanQuyenChucNang(int UserID) {
			// có chức năng trong csdl là được phân quyề, ngược lại thì ko
			DataTable kq = SqlDataAccessHelper.ExecSPQuery(SPName6.FunctionPrivilege_DocPhanQuyenChucNangV6.ToString(),
														   new SqlParameter("@UserID", UserID));
			return kq;
		}

		private void PopulateData_ToCheckList(DataTable tablePhanQuyenChucNang) {
			// ghi chú trước khi fill dữ liệu vào check list thì cần phải uncheckAll trước
			checkedListChucNang.UnCheckAll();
			foreach (DataRow dataRow in tablePhanQuyenChucNang.Rows) {
				int idChucNangDuocPhanQuyen = (int)dataRow["FunctionID"];
				bool enable = (bool)dataRow["Enable"];

				for (int index = 0; index < checkedListChucNang.ItemCount; index++) // ghi chú ở đây ko dùng items.Count vì nó = 0, chưa hiểu, sử dụng ItemsCount và hàm GetItem
				{
					DataRowView dataRowView = (DataRowView)checkedListChucNang.GetItem(index);
					int idChucNang = (int)dataRowView["ID"];
					if (idChucNang == idChucNangDuocPhanQuyen) {
						checkedListChucNang.SetItemChecked(index, enable);
						break;
					}
				}
			}
		}

		private void gridViewTaiKhoan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
			// nếu ko có focus vào dòng nào thì thoát form, nếu có thì xử lý load phòng ban thao tác và phân quyền
			if (e.FocusedRowHandle == GridControl.InvalidRowHandle) {
				ACMessageBox.Show("Không có dòng dữ liệu nào được chọn. Form tự động đóng.", Resources.Caption_ThongBao, 2000);
				Close();
			}
			if (e.FocusedRowHandle < 0) { return; }
			//có focus, load phòng và phân quyền theo tài khoản này
			//xác định userID 
			DataRow dataRow = gridViewTaiKhoan.GetDataRow(e.FocusedRowHandle);
			if (dataRow == null) { return; }
			int userID = (int)dataRow["UserID"];
			// uncheck all treeNode Phòng ban
			TreeNode root = GeneralBUS.ReturnRootNode(treePhongBan.TopNode);
			SetCheckStatus_AllTreeNode(root, false);
			treePhongBan.Refresh();
			// load dữ liệu phân quyền phòng ban và thực hiện check
			treePhongBan.AfterCheck -= treePhongBan_OnAfterCheck;
			DataTable tablePhanQuyenPhongBan = LoadDataTable_PhanQuyenPhongBan(userID);
			PopulateData_ToTreePhong(tablePhanQuyenPhongBan);
			treePhongBan.ExpandAll();
			treePhongBan.AfterCheck += treePhongBan_OnAfterCheck;
			//load dữ liệu phân quyền chức năng và thực hiện check
			DataTable tablePhanQuyenChucNang = LoadDataTable_PhanQuyenChucNang(userID);
			PopulateData_ToCheckList(tablePhanQuyenChucNang);
		}

		private void treePhongBan_OnAfterCheck(object sender, TreeViewEventArgs treeViewEventArgs) {
			// xác định trạng thái node hiện hành để set trạng thái cho node con
			TreeNode root = treeViewEventArgs.Node;
			DataRow dataRow = (DataRow)root.Tag;
			var checkStatus = root.Checked;
			int idPhong = (int)dataRow["ID"];// giữ lại id của node hiện tại để sau khi check xong thì chọn lại node này
			// thực hiện set trạng thái cho các node con
			treePhongBan.AfterCheck -= treePhongBan_OnAfterCheck;
			SetCheckStatus_AllTreeNode(root, checkStatus);
			treePhongBan.AfterCheck += treePhongBan_OnAfterCheck;
			// sau khi set xong thì focus lại node hiện tại 
			root = GeneralBUS.ReturnRootNode(root);
			root = FindNode(idPhong, root);
		}

		private void SetCheckNode_PhongBanThaoTac(TreeNode root, DataTable tablePhanQuyenPhongBan) {
			foreach (DataRow dataRow in tablePhanQuyenPhongBan.Rows) {
				root = GeneralBUS.ReturnRootNode(root);
				int idPhong = (int)dataRow["IDDepartment"];
				TreeNode node = FindNode(idPhong, root);
				if (node == null) continue; // ko tìm thấy  node thì đi tiếp
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
			for (int i = 0; i < root.Nodes.Count; i++) {
				SetCheckStatus_AllTreeNode(root.Nodes[i], Status);
			}
		}

		#region xóa tài khoản
		private void btnXoa_Click(object sender, EventArgs e) {
			// xác định thông tin tài khoản đang chọn để thực hiện thao tác
			DataRow dataRow = XacDinh_DataRowTaiKhoan_DangChon();
			if (dataRow == null) return;
			int userID = (int)dataRow["UserID"];
			string userAccount = dataRow["UserAccount"].ToString();
			string template = "Bạn muốn xóa tài khoản [{0}]?";
			if (MessageBox.Show(string.Format(template, userAccount), Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}
			// thực hiện và reload lại danh sách tài khoản
			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.NewUserAccount_XoaTaiKhoanV6.ToString(),
														 new SqlParameter("@UserID", userID));
			if (kq == 0) {
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
				return;
			}
			//reload lại danh sách tài khoản
			LoadDSTaiKhoan();
		}
		#endregion

		private void btnDisable_Click(object sender, EventArgs e) {
			// xác định thông tin tài khoản đang chọn để thực hiện thao tác
			DataRow dataRow = XacDinh_DataRowTaiKhoan_DangChon();
			if (dataRow == null) return;
			int userID = (int)dataRow["UserID"];
			bool Status = (bool)dataRow["Enable"];
			bool Status1 = !Status;
			string userAccount = dataRow["UserAccount"].ToString();
			string template = "Bạn muốn đổi trạng thái tài khoản [{0}]?";
			if (MessageBox.Show(string.Format(template, userAccount), Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}
			// thực hiện và reload lại danh sách tài khoản
			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.NewUserAccount_DisableTaiKhoanV6.ToString(),
														 new SqlParameter("@UserID", userID), new SqlParameter("@Enable", Status1));
			if (kq == 0) {
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
				return;
			}
			//reload lại danh sách tài khoản
			LoadDSTaiKhoan();
		}

		private void btnThem_Click(object sender, EventArgs e) {

			frmNhapTTTaiKhoan frm = new frmNhapTTTaiKhoan();
			frm.m_Mode = ModeType.Them;
			frm.ShowDialog();
			if (frm.m_Mode == ModeType.Cancel) return;

			//kiểm tra kết nối csdl
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			string encryptPass = MyUtility.Mahoa(frm.m_Password);
			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.NewUserAccount_ThemTaiKhoanV6.ToString(),
														 new SqlParameter("@UserAccount", frm.m_TenTaiKhoan),
														 new SqlParameter("@Password", encryptPass),
														 new SqlParameter("@Enable", frm.m_Enable));
			if (kq == 0) {
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
				return;
			}
			LoadDSTaiKhoan();
		}

		private void btnSua_Click(object sender, EventArgs e) {
			// xác định thông tin tài khoản đang chọn để thực hiện thao tác
			DataRow dataRow = XacDinh_DataRowTaiKhoan_DangChon();
			int userID = (int)dataRow["UserID"];

			frmNhapTTTaiKhoan frm = new frmNhapTTTaiKhoan();
			frm.m_Mode = ModeType.Sua;
			frm.m_TenTaiKhoan = dataRow["UserAccount"].ToString();
			frm.m_Enable = (bool)dataRow["Enable"];
			frm.ShowDialog();

			if (frm.m_Mode == ModeType.Cancel) return;

			//kiểm tra kết nối csdl
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.NewUserAccount_CapNhatTaiKhoanV6.ToString(),
														 new SqlParameter("@UserID", userID),
														 new SqlParameter("@UserAccount", frm.m_TenTaiKhoan),
														 new SqlParameter("@Enable", frm.m_Enable));
			if (kq == 0) {
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
				return;
			}
			LoadDSTaiKhoan();

		}

		private void btnLuuChucNang_Click(object sender, EventArgs e) {
			int focusRowHandle = gridViewTaiKhoan.FocusedRowHandle;
			// kiểm tra focusRowHandle hợp lệ
			if (focusRowHandle == GridControl.InvalidRowHandle || focusRowHandle < 0) {
				ACMessageBox.Show("Bạn chưa chọn tài khoản để thao tác.", Resources.Caption_ThongBao, 2000);
				return;
			}

			//kiểm tra kết nối csdl 
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			DataRow selectedDataRow_TaiKhoan = gridViewTaiKhoan.GetDataRow(focusRowHandle);
			int userID = (int)selectedDataRow_TaiKhoan["UserID"];
			List<int> dsChucNang_DuocPhanQuyen = new List<int>();
			List<int> dsChucNang_KoDuocPhanQuyen = new List<int>();
			for (int i = 0; i < checkedListChucNang.ItemCount; i++) {
				DataRowView dataRowView = (DataRowView)checkedListChucNang.GetItem(i);
				int functionID = (int)dataRowView["ID"];
				bool checkStatus = checkedListChucNang.GetItemChecked(i);
				if (checkStatus) dsChucNang_DuocPhanQuyen.Add(functionID);
				else dsChucNang_KoDuocPhanQuyen.Add(functionID);
			}
			LuuPhanQuyenChucNang(userID, dsChucNang_DuocPhanQuyen, true);
			LuuPhanQuyenChucNang(userID, dsChucNang_KoDuocPhanQuyen, false);
		}

		private void LuuPhanQuyenChucNang(int UserID, List<int> DS_ChucNang, bool Enable) {
			foreach (int functionID in DS_ChucNang) {
				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.FunctionPrivilege_InsUpd_PhanQuyenV6.ToString(),
															 new SqlParameter("@UserID", UserID),
															 new SqlParameter("@FunctionID", functionID),
															 new SqlParameter("@Enable", Enable));
				if (kq == 0) {
					ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
					return;
				}
			}
		}

		private void btnLuuPhong_Click(object sender, EventArgs e) {
			int focusRowHandle = gridViewTaiKhoan.FocusedRowHandle;
			// kiểm tra focusRowHandle hợp lệ
			if (focusRowHandle == GridControl.InvalidRowHandle || focusRowHandle < 0) {
				ACMessageBox.Show("Bạn chưa chọn tài khoản để thao tác.", Resources.Caption_ThongBao, 2000);
				return;
			}

			//kiểm tra kết nối csdl 
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			DataRow selectedDataRow_TaiKhoan = gridViewTaiKhoan.GetDataRow(focusRowHandle);
			int userID = (int)selectedDataRow_TaiKhoan["UserID"];

			List<int> dsPhong_DuocThaoTac = new List<int>();
			List<int> dsPhong_KoDuocThaoTac = new List<int>();
			TreeNode root = GeneralBUS.ReturnRootNode(treePhongBan.TopNode);
			LayDSPhongBanDuocPhanQuyen(root, dsPhong_DuocThaoTac, dsPhong_KoDuocThaoTac);
			LuuPhanQuyenPhongBan(userID, dsPhong_DuocThaoTac, true);
			LuuPhanQuyenPhongBan(userID, dsPhong_KoDuocThaoTac, false);
		}

		private void LuuPhanQuyenPhongBan(int UserID, List<int> dsPhong_DuocThaoTac, bool Enable) {
			foreach (int id in dsPhong_DuocThaoTac) {
				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.DeptPrivilege_InsUpdPhanQuyenV6.ToString(),
															 new SqlParameter("@UserID", UserID),
															 new SqlParameter("@IDDepartment", id),
															 new SqlParameter("@Enable", Enable));
				if (kq == 0) {
					ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
					return;
				}
			}
		}

		private void LayDSPhongBanDuocPhanQuyen(TreeNode root, List<int> dsPhong_DuocThaoTac, List<int> dsPhong_KoDuocThaoTac) {
			if (root == null) return;
			bool checkStatus = root.Checked;
			DataRow dataRow = (DataRow)root.Tag;
			int idPhong = (int)dataRow["ID"];
			if (checkStatus) dsPhong_DuocThaoTac.Add(idPhong);
			else dsPhong_KoDuocThaoTac.Add(idPhong);
			for (int i = 0; i < root.Nodes.Count; i++) {
				LayDSPhongBanDuocPhanQuyen(root.Nodes[i], dsPhong_DuocThaoTac, dsPhong_KoDuocThaoTac);
			}
		}

		private void btnResetPassword_Click(object sender, EventArgs e) {
			DataRow dataRow = XacDinh_DataRowTaiKhoan_DangChon();
			int userID = (int)dataRow["UserID"];

			frmNhapTTTaiKhoan frm = new frmNhapTTTaiKhoan();
			frm.m_Mode = ModeType.Other;
			frm.m_TenTaiKhoan = dataRow["UserAccount"].ToString();
			frm.m_Enable = (bool)dataRow["Enable"];
			frm.ShowDialog();

			if (frm.m_Mode == ModeType.Cancel) return;
			string encryptPassword = MyUtility.Mahoa(frm.m_Password);
			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.NewUserAccount_ChangePassV6.ToString(),
														 new SqlParameter("@UserID", userID),
														 new SqlParameter("@NewEncryptPassword", encryptPassword));
			if (kq == 0) {
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
				return;
			}

		}

		private DataRow XacDinh_DataRowTaiKhoan_DangChon() {
			#region kiểm tra đang chọn dòng nào

			if (gridViewTaiKhoan.FocusedRowHandle == GridControl.InvalidRowHandle) {
				ACMessageBox.Show("Không có tài khoản.", Resources.Caption_ThongBao, 2000);
				return null;
			}
			if (gridViewTaiKhoan.FocusedRowHandle < 0) {
				ACMessageBox.Show("Vui lòng chọn tài khoản.", Resources.Caption_ThongBao, 2000);
				return null;
			}

			#endregion

			//xác định tài khoản sẽ xóa
			int focusRowHandle = gridViewTaiKhoan.FocusedRowHandle;
			DataRow dataRow = gridViewTaiKhoan.GetDataRow(focusRowHandle);
			return dataRow;
		}

	}
}
