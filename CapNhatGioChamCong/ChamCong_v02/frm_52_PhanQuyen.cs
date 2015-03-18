using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ChamCong_v02.BUS;
using ChamCong_v02.DAO;
using log4net;

namespace ChamCong_v02 {
	public partial class frm_52_PhanQuyen : Form {
		// các vấn đề gặp trong form này.
		// logic: 
		// 1. load vào treeview danh sách các phòng ban mà tài khoản login được phép thao tác.
		//2. load vào danh sách tài khoản các tài khoản trừ tài khoản hiện tại đang login. 
		// trường hợp là tài khoản root thì do UserID khác tất cả => lấy được hết tất cả tài khoản
		//		VD: 10 phòng trong đó có 2 phòng 1-2 mà tài khoản		
		//--> lấy lên danh sách tất cả phòng ban mà tài khoản đang chọn được phép thao tác
		// duyệt qua và đối chiếu với treePhòng ban, cái nào có thì gán giá trị check/uncheck, trường hợp ko có thì mặc định là false
		// load mặc định các chức năng vào ds check quyền thao tác
		// duyệt từng quyền thao tác trong CSDL và đối chiếu , cái nào có thì gán giá trị, cái nào ko có thì gán giá trị mặc định là false
		// 

		public class cChucNang {
			public int ID { get; set; }
			public string MoTa { get; set; }
			public bool IsYes { get; set; }
			public override string ToString() {
				return "ID= " + ID + " IsYes= " + IsYes + "\t" + MoTa;
			}
		}

		public List<int> dspb1 = new List<int>();

		public readonly ILog lg = LogManager.GetLogger("frm_52_PhanQuyen");
		public frm_52_PhanQuyen() {
			log4net.Config.XmlConfigurator.Configure();
			InitializeComponent();
		}

		#region load treeview
		private DataTable m_TablePhongban;
		public TreeView loadTreePhgBan(TreeView tvDSPhongBan, DataTable pDataTable) {
			if (pDataTable.Rows.Count > 0) {
				foreach (DataRow dataRow in pDataTable.Select("RelationID = 0")) {
					// load root node vì chỉ có 1 mình root node trỏ đến chính nó, relation id = 0
					TreeNode ParentNode = new TreeNode { Text = dataRow["Description"].ToString(), Tag = (int)dataRow["ID"] };
					tvDSPhongBan.Nodes.Add(ParentNode);
					// thêm vào danh sách id node
					dspb1.Add((int)dataRow["ID"]);
					// sau khi load root node thì load các node con dưới nó 1 cấp , các node con này tự nó se load con cho nó
					loadTreeSubNode(ref ParentNode, (int)(dataRow["ID"]), pDataTable);
				}
			}
			return tvDSPhongBan;
		}

		private void loadTreeSubNode(ref TreeNode ParentNode, int ParentId, DataTable dtMenu) {
			// lọc ra các node anh em có cùng id của node cha để gắn vào
			DataRow[] childs = dtMenu.Select("RelationID = " + ParentId);
			// duyệt qua từng node anh em để load các con của mình
			foreach (DataRow dRow in childs) {
				TreeNode child = new TreeNode { Text = dRow["Description"].ToString(), Tag = (int)dRow["ID"], ToolTipText = dRow["Description"].ToString(), Checked = (bool)dRow["IsYes"] };
				ParentNode.Nodes.Add(child);
				// sau khi tạo thì thêm vào danh sách id
				dspb1.Add((int)dRow["ID"]);
				//Recursion Call : load các con của nó
				loadTreeSubNode(ref child, (int)dRow["ID"], dtMenu);
			}
		}

		#endregion

		#region DAL
		private DataTable LayDSPhongban(int UserID) {
			string query = string.Empty;
			query += @"	select RelationDept.ID, RelationDept.Description, RelationDept.RelationID, DeptPrivilege.IsYes 
						from RelationDept, DeptPrivilege
						where DeptPrivilege.IDD = RelationDept.ID
						and DeptPrivilege.IsYes = 1
						and DeptPrivilege.UserID = @UserID";
			DataTable kq;
			kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@UserID" }, new object[] { UserID });
			return kq;
		}

		/// <summary>
		/// áp dụng cho trường hợp tài khoản root , ko thể kết 2 bảng DeptPrivilege vì root ko có trong danh sách tài khoản 
		/// nên chỉ cần lấy danh sách tất cả các phòng ban, khỏi kết 2 bảng
		/// </summary>
		/// <returns></returns>
		private DataTable LayDSTatCaPhongban() {
			string query = string.Empty;
			query += @"select RelationDept.ID, RelationDept.Description, RelationDept.RelationID, cast (1 as bit) as IsYes
						from RelationDept";
			DataTable kq;
			kq = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return kq;
		}

		/// <summary>
		/// lấy danh sách tài khoản mà UserID hiện tại được phép thao tác nằm trong giới hạn tài khoản login được phép thao tác
		/// </summary>
		/// <param name="UserID"></param>
		/// <param name="currUserID"></param>
		/// <returns></returns>
		private DataTable LayDSCacPhongban(int UserID, int currUserID) {
			string query = string.Empty;
			query += @"	select RelationDept.ID, RelationDept.Description, RelationDept.RelationID, DeptPrivilege.IsYes 
						from RelationDept, DeptPrivilege
						where DeptPrivilege.IDD = RelationDept.ID						
						and DeptPrivilege.UserID = @UserID
                        and RelationDept.ID IN      ( select RelationDept.ID
						                            from RelationDept, DeptPrivilege
						                            where DeptPrivilege.IDD = RelationDept.ID						
						                            and DeptPrivilege.UserID = @UserID_LoggedIn )";
			DataTable kq;
			kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@UserID", "@UserID_LoggedIn" }, new object[] { UserID, currUserID });
			return kq;
		}

		/// <summary>
		/// trường hợp đang là tài khoản root, lấy danh sách  các phòng ban được thao tác trên tất cả phòng ban vì root lấy hết tất cả phòng ban
		/// </summary>
		/// <param name="UserID"></param>
		/// <returns></returns>
		private DataTable LayDSCacPhongban(int UserID) {
			string query = string.Empty;
			query += @"select RelationDept.ID, RelationDept.Description, RelationDept.RelationID, DeptPrivilege.IsYes 
						from RelationDept, DeptPrivilege
						where DeptPrivilege.IDD = RelationDept.ID						
						and DeptPrivilege.UserID = @UserID
                        and RelationDept.ID IN      ( select RelationDept.ID from RelationDept )";
			DataTable kq;
			kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@UserID" }, new object[] { UserID });
			return kq;
		}

		/// <summary>
		/// trường hợp tài khoản bình thường lấy các tài khoản khác tài khoản đang login
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// trường hợp root lấy hết danh sách tát cả các tài khoản
		/// </summary>
		/// <returns></returns>
		private DataTable LayDSTatCaTaikhoan() {
			string query = string.Empty;
			#region query
			query += @"		SELECT	NewUserAccount.UserAccount, NewUserAccount.UserID
							FROM	NewUserAccount";
			#endregion
			DataTable kq;
			kq = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return kq;
		}

		/// <summary>
		/// lấy danh sách các chức năng mà tài khoản đang chọn được phép thao tác.
		/// </summary>
		/// <param name="UserID"></param>
		/// <returns></returns>
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
			//lstChucNang.Add(new cChucNang() { ID = 10000, MoTa = "Kết nối CSDL" });
			lstChucNang.Add(new cChucNang() { ID = 10001, MoTa = "Xem Công, sửa giờ và xuất báo biểu" });
			lstChucNang.Add(new cChucNang() { ID = 10002, MoTa = "Điểm danh Nhân viên" });
			lstChucNang.Add(new cChucNang() { ID = 20001, MoTa = "Khai báo vắng cho Nhân viên" });
			lstChucNang.Add(new cChucNang() { ID = 20002, MoTa = "Khai báo ngày làm việc tính PC 100% lương" });
			lstChucNang.Add(new cChucNang() { ID = 20003, MoTa = "Chấm công tay cho Nhân viên" });
			lstChucNang.Add(new cChucNang() { ID = 30001, MoTa = "Sửa giờ hàng loạt" });
			lstChucNang.Add(new cChucNang() { ID = 30002, MoTa = "Xem lịch sử sửa giờ chấm công" });
			lstChucNang.Add(new cChucNang() { ID = 30003, MoTa = "Duyệt các ngày làm việc tính PC 100% lương" });
			lstChucNang.Add(new cChucNang() { ID = 40001, MoTa = "Tính lương cho Nhân viên" });
			lstChucNang.Add(new cChucNang() { ID = 40002, MoTa = "Điều chỉnh lương tháng trước cho Nhân viên" });
			lstChucNang.Add(new cChucNang() { ID = 40003, MoTa = "Thay đổi hệ số lương cho Nhân viên" });
			//lstChucNang.Add(new cChucNang() { ID = 50001, MoTa = "Đổi mật khẩu tài khoản" });// xem [2703_1]
			lstChucNang.Add(new cChucNang() { ID = 50002, MoTa = "Phân quyền" });
			lstChucNang.Add(new cChucNang() { ID = 50003, MoTa = "Tạo tài khoản đăng nhập" });
			lstChucNang.Add(new cChucNang() { ID = 60001, MoTa = "Cài đặt thông số" });

			return lstChucNang;

		}

		#endregion

		private void frm_PhanQuyen_Load(object sender, EventArgs e) {
			// load danh sách chức năng trước tất cả các chức năng mà tài khoản này được phép thao tác
			List<cChucNang> lstChucnang = TaoChucNang();
			checkQuyenThaotac.DataSource = lstChucnang;
			checkQuyenThaotac.ValueMember = "ID";
			checkQuyenThaotac.DisplayMember = "MoTa";

			// load tree view danh sách phòng ban mà tài khoản đang login được phép thao tác, nếu tài khoản login là root thì load hết danh sách phòng ban
			treePhongban.Nodes.Clear();
			if (ThamSo.currUserID != int.MaxValue) m_TablePhongban = LayDSPhongban(ThamSo.currUserID);
			else m_TablePhongban = LayDSTatCaPhongban();

			loadTreePhgBan(treePhongban, m_TablePhongban); // sau khi load xong sẽ có được list dspb1 chứa ds id các phòng ban mà currID hiện tại được phép thao tác
			// việc check các item quyền sẽ để cho sự kiện chọn tài khoản  thực hiện

			// load tất cả danh sách tài khoản trừ tài khoản đang login
			lstTaikhoan.DisplayMember = "UserAccount";
			if (ThamSo.currUserID != int.MaxValue) lstTaikhoan.DataSource = LayDSTaikhoan();
			else lstTaikhoan.DataSource = LayDSTatCaTaikhoan();

			#region // trường hợp đặc biệt ko có tài khoản nào
			if (lstTaikhoan.Items.Count == 0) {
				AutoClosingMessageBox.Show("Không có tài khoản để phân quyền.", "Thông báo", 2000);
				this.Close();
			}
			#endregion

			// chọn mặc định tài khoản đầu tiên, lúc này phát sự kiện index change sẽ check các item quyền
			// chọn tài khoản đồng thời sẽ check , uncheck các phòng ban được thao tác
			// đồng thời check , uncheck các chức năng được tài khoản được chọn được phép thao tác
			lstTaikhoan.SelectedIndex = 0;
		}

		private void lstTaikhoan_SelectedValueChanged(object sender, EventArgs e) {
			// lấy userid để load tree view, update check phòng ban chức năng
			DataRowView rowView = lstTaikhoan.SelectedItem as DataRowView;
			int Selected_UserID = (int)rowView["UserID"];

			// check, uncheck quyền cho các phòng ban trước, ds thao tác sau
			// VD: tk login duoc phan quyen 1_21; 2_21; 3_21; 7_21; 9_21 (format <idquyen>_<id_tk> thì lấy hết dspb này cho tk được phân quyền, 
			// id nào có check thì phân quyền true, ko có thì phân quyền false. 
			// VD: lấy lên chỉ được 1_25; 9_25 thì set true, còn lại 2_21; 3_21; 7_21; ko có trong kq lấy lên thì set false
			DataTable table1 = (ThamSo.currUserID != int.MaxValue) ? LayDSCacPhongban(Selected_UserID, ThamSo.currUserID) : LayDSCacPhongban(Selected_UserID);
			treePhongban.AfterCheck -= treePhongban_AfterCheck;

			SetQuyenPhongBan(treePhongban.TopNode, table1);

			treePhongban.AfterCheck += treePhongban_AfterCheck;

			// check, uncheck quyền ds thao tác
			DataTable chucnang = LayDSChucNang(Selected_UserID);
			#region debug

/*			for (int i = 0; i < chucnang.Rows.Count; i++) {
				DataRow r = chucnang.Rows[i];
				Debug.WriteLine("Selected_UserID=" + r["UserID"].ToString() + "\tMenuID=" + r["MenuID"].ToString() + "\tIsYes=" + r["IsYes"].ToString());
			}*/
			#endregion
			checkQuyenThaotac.ItemCheck -= checkQuyenThaotac_ItemCheck;
			UpdateQuyenThaotac(chucnang);
			checkQuyenThaotac.ItemCheck += checkQuyenThaotac_ItemCheck;
		}

		private void SetQuyenPhongBan(TreeNode treeNode, DataTable table1) {
			if (treeNode == null) return; // node gốc null
			// set quyền cho node gốc trước
			DataRow[] row = table1.Select("ID = " + (int)treeNode.Tag);
			if (row.Length == 0) { // ko tìm thấy phân quyền cho ID phòng ban này --> mặc định set false
				treeNode.Checked = false;
			}
			else { // tìm thấy phân quyền cho phòng ban này thì set true hoặc false tuỳ theo dữ liệu
				bool ischeck = (bool)row[0]["IsYes"];// mỗi phòng ban có id duy nhất nên kết quả chỉ lấy row0
				treeNode.Checked = ischeck;
			}
			// nếu tồn tại node con thì set quyền cho các node con
			if (treeNode.Nodes.Count > 0) {
				for (int i = 0; i < treeNode.Nodes.Count; i++) {
					SetQuyenPhongBan(treeNode.Nodes[i], table1);
				}
			}
		}

		private void treePhongban_AfterCheck(object sender, TreeViewEventArgs e) {
			CheckTreeViewNode(e.Node, e.Node.Checked);

		}

		private void CheckTreeViewNode(TreeNode node, Boolean isChecked) {
			foreach (TreeNode item in node.Nodes) {
				item.Checked = isChecked;
			}
		}

		private void btnCapNhat_Click(object sender, EventArgs e) {
			// lấy ID tài khoản đang chọn
			DataRowView rowView = lstTaikhoan.SelectedItem as DataRowView;
			int userid = (int)rowView["UserID"];
			// update phòng ban thao tác và status cho phép hay ko
			List<int> id = new List<int>();
			List<int> status = new List<int>();
			DuyetLayID_Status(treePhongban.TopNode, id, status);

		    bool flag = false;
		    int n;
			// duyệt qua từng phòng ban và update lại phòng ban đó được cho phép hay ko
			for (int i = 0; i < id.Count; i++) {
				n = SqlDataAccessHelper.ExecNoneQueryString(
					" update DeptPrivilege set IsYes = @IsYes where IDD = @IDD and UserID = @UserID" +
					" IF @@ROWCOUNT=0 INSERT INTO DeptPrivilege (UserID,IDD,IsYes) VALUES (@UserID,@IDD,@IsYes) "
					, new string[] { "@UserID", "@IsYes", "@IDD" }
					, new object[] { userid, status[i], id[i] });
                if (n == 0) { // nếu xảy ra lỗi thì thoát khỏi vòng lặp
                    flag = true;
                    break;
                }
			}
            if (flag) {
                MessageBox.Show("Xảy ra lỗi trong quá trình thực hiện. Vui lòng thử lại." , "Lỗi");
                goto point1;
            }

			//duyệt từng chức năng và update lại cho phép hay ko cho phép chức năng đó
			List<cChucNang> lstchucnang = checkQuyenThaotac.DataSource as List<cChucNang>;
			for (int i = 0; i < checkQuyenThaotac.Items.Count; i++) {
				cChucNang item = checkQuyenThaotac.Items[i] as cChucNang;
				bool check = item.IsYes;
				int menuid = item.ID;
				n = SqlDataAccessHelper.ExecNoneQueryString(
					" update MenuPrivilege set IsYes = @IsYes where UserID = @UserID and MenuID = @MenuID " +
					" IF @@ROWCOUNT=0 INSERT INTO MenuPrivilege (UserID,MenuID,IsYes) VALUES (@UserID,@MenuID,@IsYes) "
					, new string[] { "@UserID", "@IsYes", "@MenuID" }
					, new object[] { userid, check, menuid });
                if (n == 0) { // nếu xảy ra lỗi thì thoát khỏi vòng lặp
                    flag = true;
                    break;
                }
            }
            if (flag) {
                MessageBox.Show("Xảy ra lỗi trong quá trình thực hiện. Vui lòng thử lại.", "Lỗi");
                goto point1;
            }

            AutoClosingMessageBox.Show("Cập nhật phân quyền thành công.", "Thông báo", 2000);

            point1:
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


		private void UpdateQuyenThaotac(DataTable tableChucnang) {
			// lấy dataSource danh sách chức năng, set default false nếu ko có row nào
			if (tableChucnang == null || tableChucnang.Rows.Count == 0) {
				for (int i = 0; i < checkQuyenThaotac.Items.Count; i++) {
					cChucNang chucnang = checkQuyenThaotac.Items[i] as cChucNang;
					if (chucnang == null) continue;
					checkQuyenThaotac.SetItemCheckState(i, CheckState.Unchecked);
					chucnang.IsYes = false;
				}
			}
			else {// tồn tại chức năng
				for (int i = 0; i < checkQuyenThaotac.Items.Count; i++) {
					//checkQuyenThaotac.g  checkQuyenThaotac.GetItemCheckState(i);
					cChucNang chucnang = checkQuyenThaotac.Items[i] as cChucNang;
					if (chucnang == null) continue;
					int id = chucnang.ID;
					DataRow[] arrRows = tableChucnang.Select("MenuID = " + id, "MenuID asc");// tìm trong table chức năng đó bật hay tắt, 
					if (arrRows.Length == 0) {
						checkQuyenThaotac.SetItemCheckState(i, CheckState.Unchecked);
						chucnang.IsYes = false;
					}
					else {
						bool temp = (bool)arrRows[0]["IsYes"];
						checkQuyenThaotac.SetItemCheckState(i, (temp == true) ? CheckState.Checked : CheckState.Unchecked);
						chucnang.IsYes = temp;
					}
				}
			}

			checkQuyenThaotac.Update();
		}




		public TreeNode FindNode(TreeNode root, int ID) {
			if (root != null) {
				if ((int)root.Tag == ID) // xét node hiện tại trước, node hiện tại có thì return node hiện tại khỏi tìm trong các node con, còn node hiện tại ko có thì tìm ở node con của nó
					return root;
				if (root.Nodes.Count > 0) { //tồn tại node con thì tìm trong các node con
					for (int i = 0; i < root.Nodes.Count; i++) {
						TreeNode kq = FindNode(root.Nodes[i], ID);
						if (kq != null) return kq; // tìm được tại 1 node con nào đó
						else { // kq == null : ko tìm được trong node hiện tại (vì điều kiện root.Tag == ID ở trên tức là tìm ở node hiện tại) lẫn các node con của nó 
							if (i == root.Nodes.Count - 1)  //node hiện tại là node cuối cùng trong danh sách các node con mà vẫn tìm ko thấy thì kết thúc tìm kiếm, return null
								return null;
						}
					}
				}
				// ko tồn tại node con, thoát xuống dưới return null
			}
			return null;


		}

		private void checkQuyenThaotac_ItemCheck(object sender, ItemCheckEventArgs e) {
			cChucNang item = checkQuyenThaotac.Items[e.Index] as cChucNang;
			if (e.NewValue == CheckState.Checked) item.IsYes = true;
			else if (e.NewValue == CheckState.Unchecked) item.IsYes = false;
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			GC.Collect();
			this.Close();
		}
	}
}
