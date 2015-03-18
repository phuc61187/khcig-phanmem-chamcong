using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CapNhatGioChamCong.DAO;
using DocumentFormat.OpenXml.Bibliography;
using log4net;

namespace CapNhatGioChamCong {
	public partial class frm_PhanQuyen : Form {
		public class cChucNang {
			public int ID { get; set; }
			public string MoTa { get; set; }
			public bool IsYes { get; set; }
		}

		public readonly ILog log = LogManager.GetLogger("frm_PhanQuyen");
		public frm_PhanQuyen() {
			log4net.Config.XmlConfigurator.Configure();
			InitializeComponent();
			SqlDataAccessHelper.ConnectionString = @"Data Source=.\sqlexpress; Initial Catalog=WiseEyeV5Express; Integrated Security=true;";
			ThamSo.currUserID = 21;
			ThamSo.currUserAccount = "dainghia";
		}

		#region load treeview
		private DataTable m_TablePhongban;
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
				TreeNode child = new TreeNode { Text = dRow["Description"].ToString(), Tag = (int)dRow["ID"], ToolTipText = dRow["Description"].ToString(), Checked = (bool)dRow["IsYes"] };
				ParentNode.Nodes.Add(child);
				//Recursion Call
				loadTreeSubNode(ref child, (int)dRow["ID"], dtMenu);
			}
		}

		#endregion

		#region DAL
		private DataTable LayDSPhongban(int UserID) {
			string query = string.Empty;
			query += @"select RelationDept.ID, RelationDept.Description, RelationDept.RelationID, DeptPrivilege.IsYes 
						from RelationDept, DeptPrivilege
						where DeptPrivilege.IDD = RelationDept.ID
						and DeptPrivilege.IsYes = 1
						and DeptPrivilege.UserID = @UserID";
			DataTable kq;
			kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@UserID" }, new object[] { UserID });
			return kq;
		}
		private DataTable LayDSCacPhongban(int UserID) {
			string query = string.Empty;
			query += @"select RelationDept.ID, RelationDept.Description, RelationDept.RelationID, DeptPrivilege.IsYes 
						from RelationDept, DeptPrivilege
						where DeptPrivilege.IDD = RelationDept.ID						
						and DeptPrivilege.UserID = @UserID";
			DataTable kq;
			kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@UserID" }, new object[] { UserID });
			return kq;
		}
		private DataTable LayDSTaikhoan() {
			string query = string.Empty;
			#region query
			query += @"		SELECT	NewUserAccount.UserAccount, NewUserAccount.UserID
							FROM	NewUserAccount
							where	NewUserAccount.UserID <> @UserID  ";
			#endregion
			DataTable kq;
			kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@UserID" }, new object[] { ThamSo.currUserID });
			return kq;
		}
		private DataTable LayDSChucNang(int UserID) {
			string query = string.Empty;
			#region query

			query += @"select distinct  MenuPrivilege.UserID, MenuPrivilege.MenuID, MenuPrivilege.IsYes from MenuPrivilege
					where MenuPrivilege.UserID = @UserID";

			#endregion

			DataTable kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@UserID" }, new object[] { UserID });
			return kq;
		}

		private List<cChucNang> TaoChucNang() {
			List<cChucNang> lstChucNang = new List<cChucNang>();
			lstChucNang.Add(new cChucNang() { ID = 10000, MoTa = "Kết nối CSDL" });
			lstChucNang.Add(new cChucNang() { ID = 20001, MoTa = "Xem Công, sửa giờ và xuất báo biểu" });
			lstChucNang.Add(new cChucNang() { ID = 20002, MoTa = "Điểm danh Nhân viên" });
			lstChucNang.Add(new cChucNang() { ID = 20003, MoTa = "Khai báo vắng cho Nhân viên" });
			lstChucNang.Add(new cChucNang() { ID = 20004, MoTa = "Khai báo làm việc vào ngày nghỉ cho Nhân viên" });
			lstChucNang.Add(new cChucNang() { ID = 20005, MoTa = "Chấm công tay cho Nhân viên" });
			lstChucNang.Add(new cChucNang() { ID = 30001, MoTa = "Sửa giờ hàng loạt" });
			lstChucNang.Add(new cChucNang() { ID = 30002, MoTa = "Xem lịch sử sửa giờ chấm công" });
			lstChucNang.Add(new cChucNang() { ID = 30003, MoTa = "Tính lương cho Nhân viên" });
			return lstChucNang;

		}

		#endregion

		private void frm_PhanQuyen_Load(object sender, EventArgs e) {
			// load danh sách chức năng trước
			List<cChucNang> lstChucnang = TaoChucNang();
			checkQuyenThaotac.DataSource = lstChucnang;
			checkQuyenThaotac.ValueMember = "ID";
			checkQuyenThaotac.DisplayMember = "MoTa";

			// load tree view
			treePhongban.Nodes.Clear();
			m_TablePhongban = LayDSPhongban(ThamSo.currUserID);
			loadTreePhgBan(treePhongban, m_TablePhongban);

			// load danh sách tài khoản 
			lstTaikhoan.DataSource = LayDSTaikhoan();
			lstTaikhoan.DisplayMember = "UserAccount";

			#region // trường hợp đặc biệt ko có tài khoản nào
			if (lstTaikhoan.Items.Count == 0) {
				AutoClosingMessageBox.Show("Không có tài khoản để phân quyền.", "Thông báo", 2000);
				this.Close();
			}
			#endregion

			// load danh sách phòng ban
			lstTaikhoan.SelectedIndex = 0;
		}

		private void lstTaikhoan_SelectedValueChanged(object sender, EventArgs e) {
			// lấy userid để load tree view, update check chức năng
			DataRowView rowView = lstTaikhoan.SelectedItem as DataRowView;
			int ID_Dangchon = (int)rowView["UserID"];

			//treePhongban.Nodes.Clear();
			//CheckTreeViewNode(treePhongban.TopNode, false);
			duyetnode(treePhongban.TopNode, false);
			DataTable table1 = LayDSCacPhongban(ID_Dangchon);
			treePhongban.AfterCheck -= treePhongban_AfterCheck;
			for (int i = 0; i < table1.Rows.Count; i++) {
				DataRow row = table1.Rows[i];
			int IDDept = (int)row["ID"];
				bool isyes = (bool)row["IsYes"];
				TreeNode node = FindNode(treePhongban.TopNode, IDDept);
				if (node != null) node.Checked = isyes;
			}
			
			treePhongban.AfterCheck += treePhongban_AfterCheck;

			// update check từng chức năng
			DataTable chucnang = LayDSChucNang(ID_Dangchon);
			UpdateQuyenThaotac(chucnang);
		}

		private void treePhongban_AfterCheck(object sender, TreeViewEventArgs e) {
			CheckTreeViewNode(e.Node, e.Node.Checked);
		}

		private void CheckTreeViewNode(TreeNode node, Boolean isChecked) {
			foreach (TreeNode item in node.Nodes) {
				item.Checked = isChecked;
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			log4net.ILog a = LogManager.GetLogger("");
			// lấy ID tài khoản đang chọn
			DataRowView rowView = lstTaikhoan.SelectedItem as DataRowView;
			int userid = (int)rowView["UserID"];
			// update phòng ban thao tác
			List<int> id = new List<int>();
			List<int> status = new List<int>();
			DuyetLayID_Status(treePhongban.TopNode, id, status);

			for (int i = 0; i < id.Count; i++) {
				int n = SqlDataAccessHelper.ExecNoneQueryString(
					" update DeptPrivilege set IsYes = @IsYes where IDD = @IDD and UserID = @UserID" +
					" IF @@ROWCOUNT=0 INSERT INTO DeptPrivilege (UserID,IDD,IsYes) VALUES (@UserID,@IDD,@IsYes) "
					, new string[] { "@UserID", "@IsYes", "@IDD" }
					, new object[] { userid, status[i], id[i] });
				Debug.WriteLine(n);
			}

			List<cChucNang> lstchucnang = checkQuyenThaotac.DataSource as List<cChucNang>;
			for (int i = 0; i < checkQuyenThaotac.Items.Count; i++) {
				cChucNang item = checkQuyenThaotac.Items[i] as cChucNang;
				bool check = item.IsYes;
				int menuid = item.ID;
				int n = SqlDataAccessHelper.ExecNoneQueryString(
					" update MenuPrivilege set IsYes = @IsYes where UserID = @UserID and MenuID = @MenuID " +
					" IF @@ROWCOUNT=0 INSERT INTO MenuPrivilege (UserID,MenuID,IsYes) VALUES (@UserID,@MenuID,@IsYes) "
					, new string[] { "@UserID", "@IsYes", "@MenuID" }
					, new object[] { userid, check, menuid });
				Debug.WriteLine(n);
			}

			lstTaikhoan_SelectedValueChanged(lstTaikhoan, null);

		}

		void DuyetLayID_Status(TreeNode root, List<int> id, List<int> status) {
			if (root == null) return;
			id.Add((int)root.Tag);
			status.Add(root.Checked ? 1 : 0);
			if (root.Nodes.Count > 0)
				foreach (TreeNode nodecon in root.Nodes) {
					DuyetLayID_Status(nodecon, id, status);
				}

		}

		private void UpdatePhongban(DataTable tablePB_check) {
			if (tablePB_check == null || tablePB_check.Rows.Count == 0) {
				//kiểm tra trước treeview có bị null ko
				if (treePhongban.Nodes.Count == 0)
					return;
				CheckTreeViewNode(treePhongban.TopNode, false);
			}
			else {

			}
		}

		private void UpdateQuyenThaotac(DataTable tableChucnang) {
			// lấy dataSource danh sách chức năng, set default false nếu ko có row nào
			if (tableChucnang == null || tableChucnang.Rows.Count == 0) {
				List<cChucNang> dschucnang = checkQuyenThaotac.DataSource as List<cChucNang>;
				foreach (cChucNang item in dschucnang) {
					item.IsYes = false;
				}

				foreach (object obj in checkQuyenThaotac.Items) {
					
				}
			}
			else {
				List<cChucNang> dschucnang = checkQuyenThaotac.DataSource as List<cChucNang>;
				//duyệt từng item chức năng,, tìm trong table chức năng đó bật hay tắt
				foreach (cChucNang item in dschucnang) {
					int id = item.ID;
					DataRow[] arrRows = tableChucnang.Select("MenuID = " + id, "MenuID asc");// tìm trong table chức năng đó bật hay tắt, 
					// ko có dòng nào tức ko có menu id đó --> tắt
					bool isyes = false;
					if (arrRows.Length != 0)
						isyes = (bool)arrRows[0]["IsYes"];
					item.IsYes = isyes;
				}
			}
			
			checkQuyenThaotac.Update();
		}


		void duyetnode(TreeNode root, bool ischeck) {
			if (root == null) return;
			root.Checked = ischeck;
			if (root.Nodes.Count > 0)
				foreach (TreeNode node in root.Nodes) {
					duyetnode(node, ischeck);

				}
		}

		TreeNode FindNode(TreeNode root, int ID) {
			if (root != null) {
				if ((int)root.Tag == ID) 
					return root;
				if (root.Nodes.Count > 0)
/*					foreach (TreeNode treeNode in root.Nodes) {
						TreeNode kq = FindNode(treeNode, ID);
						if (kq != null)
					}*/
				for (int i = 0; i < root.Nodes.Count; i++) {
					TreeNode kq = FindNode(root.Nodes[i], ID);
					if (kq != null) return kq;
					if (kq == null && i == root.Nodes.Count -1) return null;
				}
			}
			return null;


		}

		private void checkQuyenThaotac_ItemCheck(object sender, ItemCheckEventArgs e) {
			cChucNang item = checkQuyenThaotac.Items[e.Index] as cChucNang;
			if (e.NewValue == CheckState.Checked) item.IsYes = true;
			else if (e.NewValue == CheckState.Unchecked) item.IsYes = false;
		}
	}
}
