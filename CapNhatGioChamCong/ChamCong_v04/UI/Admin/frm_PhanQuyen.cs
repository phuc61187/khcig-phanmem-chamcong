using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DAL;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using log4net;
using log4net.Config;

namespace ChamCong_v04.UI.Admin {
	public partial class frm_PhanQuyen : Form {
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


		#region các hàm ko quan trọng

		public readonly ILog lg = LogManager.GetLogger("frm_52_PhanQuyen");

		public frm_PhanQuyen() {
			XmlConfigurator.Configure();
			InitializeComponent();
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			GC.Collect();
			this.Close();
		}

		#endregion

		#region DAL


		private DataTable LayDSTaikhoan(int currUserID) {
			string query = string.Empty;
			//logic trường hợp tài khoản bình thường lấy các tài khoản khác tài khoản đang login
			#region query
			query += @"		SELECT	NewUserAccount.UserAccount, NewUserAccount.UserID
							FROM	NewUserAccount
							where	NewUserAccount.UserID <> @UserID  ";
			#endregion
			DataTable kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@UserID" }, new object[] { currUserID });
			return kq;
		}

		private DataTable LayDSTatCaTaikhoan() {
			string query = string.Empty;
				//logic trường hợp root lấy hết danh sách tát cả các tài khoản
			#region query
			query += @"		SELECT	NewUserAccount.UserAccount, NewUserAccount.UserID
							FROM	NewUserAccount";
			#endregion
			DataTable kq = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return kq;
		}

		private DataTable LayDSChucNang(int UserID) {
			string query = string.Empty;
			//logic lấy danh sách các chức năng mà tài khoản đang chọn được phép thao tác.

			#region query

			query += @"select distinct  MenuPrivilege.UserID, MenuPrivilege.MenuID, MenuPrivilege.IsYes from MenuPrivilege
					where MenuPrivilege.UserID = @UserID";

			#endregion

			DataTable kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@UserID" }, new object[] { UserID });
			return kq;
		}


		#endregion

		private void frm_PhanQuyen_Load(object sender, EventArgs e) {
			// load danh sách chức năng trước tất cả các chức năng mà tài khoản này được phép thao tác
			List<XL2.cChucNang> lstChucnang = XL2.TaoChucNang();
			checkQuyenThaotac.DataSource = lstChucnang;
			checkQuyenThaotac.ValueMember = "ID";
			checkQuyenThaotac.DisplayMember = "MoTa";

			// load tree view danh sách phòng ban mà tài khoản đang login được phép thao tác, nếu tài khoản login là root thì load hết danh sách phòng ban
			List<cPhongBan> dsphongThaotac = new List<cPhongBan>();
			if (XL2.currUserID != int.MaxValue) XL.KhoiTaoDSPhongBan(dsphongThaotac, XL2.currUserID);
			else XL.KhoiTaoDSPhongBan(dsphongThaotac);

			XL.loadTreePhgBan(treePhongBan, XL2.TatcaPhongban, dsphongThaotac); // sau khi load xong sẽ có được list dspb1 chứa ds id các phòng ban mà currID hiện tại được phép thao tác
			// việc check các item quyền sẽ để cho sự kiện chọn tài khoản  thực hiện

			// load tất cả danh sách tài khoản trừ tài khoản đang login
			lstTaikhoan.DisplayMember = "UserAccount";
			if (XL2.currUserID != int.MaxValue) lstTaikhoan.DataSource = LayDSTaikhoan(XL2.currUserID);
			else lstTaikhoan.DataSource = LayDSTatCaTaikhoan();

			#region // trường hợp đặc biệt ko có tài khoản nào
			if (lstTaikhoan.Items.Count == 0) {
				ACMessageBox.Show("Không có tài khoản để phân quyền.", "Thông báo", 2000);
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
			DataTable table1 = DAO.LayDSPhong(Selected_UserID);
			treePhongBan.AfterCheck -= treePhongBan_AfterCheck;
			//set check ko đệ quy
			// đưa về root node trước khi thực hiện
			var root = treePhongBan.TopNode;
			GetTopLevelNode(ref root);// mỗi lần duyệt node sẽ làm root node chuyển về parent của node cuối nên phải trả về node gốc để duyệt từ đầu
			while (root.PrevNode != null)
				root = root.PrevNode;

			SetQuyenPhongBan(root, table1);

			treePhongBan.AfterCheck += treePhongBan_AfterCheck;

			// check, uncheck quyền ds thao tác
			DataTable chucnang = LayDSChucNang(Selected_UserID);
			#region debug

			/*			for (int i = 0; i < chucnang.Rows.Count; i++) {
				DataRow r = chucnang.Rows[i];
				Debug.WriteLine("Selected_UserID=" + r["UserID"].ToString() + "\tMenuID=" + r["MenuID"].ToString() + "\tIsYes=" + r["IsYes"].ToString());
			}*/
			#endregion
			checkQuyenThaotac.ItemCheck -= checkQuyenThaotac_ItemCheck;
			SetQuyenThaotac(chucnang);
			checkQuyenThaotac.ItemCheck += checkQuyenThaotac_ItemCheck;
		}
		public void GetTopLevelNode(ref TreeNode currentNode) {
/*
			if (currentNode.Parent != null) {
				currentNode = currentNode.Parent;
				GetTopLevelNode(ref currentNode);
			}
*/

			while (currentNode.Parent != null) {
				currentNode = currentNode.Parent;
			}
		}


		private void SetQuyenPhongBan(TreeNode treeNode, DataTable table1) {
			if (treeNode == null) return; // node gốc null
			// set quyền cho node gốc trước
			DataRow[] row = table1.Select("ID = " + ((cPhongBan)treeNode.Tag).ID);
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

		private void treePhongBan_AfterCheck(object sender, TreeViewEventArgs e) {
			TreeNode root = e.Node;
			treePhongBan.AfterCheck -= treePhongBan_AfterCheck;
			var @checked = root.Checked;
			setCheckedNode(root, @checked);
			treePhongBan.AfterCheck += treePhongBan_AfterCheck;

		}

		private void setCheckedNode(TreeNode root, bool @checked) {
			// set check đệ quy trạng thái tương ứng cho toàn bộ node con
			if (root == null) return;
			root.Checked = @checked;
			root.Expand();

			if (root.Nodes.Count > 0)
				for (int i = 0; i < root.Nodes.Count; i++) {
					TreeNode node = root.Nodes[i];
					setCheckedNode(node, @checked);
				}
		}


		private void btnCapNhat_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			// lấy ID tài khoản đang chọn
			DataRowView rowView = lstTaikhoan.SelectedItem as DataRowView;
			int userid = (int)rowView["UserID"];
			// update phòng ban thao tác và status cho phép hay ko
			#region lấy ds phòng ban 1. được thao tác, 2.check kết công
			List<cPhongBan> dsphongbanChecked = new List<cPhongBan>();
			List<cPhongBan> dsphongbanUnCheck = new List<cPhongBan>();
			// đưa về root node trước khi thực hiện
			var root = treePhongBan.TopNode;
			GetTopLevelNode(ref root);// mỗi lần duyệt node sẽ làm root node chuyển về parent của node cuối nên phải trả về node gốc để duyệt từ đầu
			while (root.PrevNode != null)
				root = root.PrevNode;
			GetNode_DuocThaotac_CheckKetcong(root, dsphongbanChecked, dsphongbanUnCheck);
			#endregion


			// duyệt qua từng phòng ban và update lại phòng ban đó được cho phép hay ko
			for (int i = 0; i < dsphongbanChecked.Count; i++) {
				SqlDataAccessHelper.ExecNoneQueryString(
						@" update DeptPrivilege set IsYes = @IsYes where IDD = @IDD and UserID = @UserID
						IF @@ROWCOUNT=0 INSERT INTO DeptPrivilege (UserID,IDD,IsYes) VALUES (@UserID,@IDD,@IsYes) "
					, new string[] { "@UserID", "@IsYes", "@IDD" }
					, new object[] { userid, 1, dsphongbanChecked[i].ID });
			}
			for (int i = 0; i < dsphongbanUnCheck.Count; i++) {
				SqlDataAccessHelper.ExecNoneQueryString(
					@" update DeptPrivilege set IsYes = @IsYes where IDD = @IDD and UserID = @UserID
						IF @@ROWCOUNT=0 INSERT INTO DeptPrivilege (UserID,IDD,IsYes) VALUES (@UserID,@IDD,@IsYes) "
					, new string[] { "@UserID", "@IsYes", "@IDD" }
					, new object[] { userid, 0, dsphongbanUnCheck[i].ID });
			}

			//duyệt từng chức năng và update lại cho phép hay ko cho phép chức năng đó
			List<XL2.cChucNang> lstchucnang = checkQuyenThaotac.DataSource as List<XL2.cChucNang>;
			for (int i = 0; i < checkQuyenThaotac.Items.Count; i++) {
				XL2.cChucNang item = checkQuyenThaotac.Items[i] as XL2.cChucNang;
				bool check = item.IsYes;
				int menuid = item.ID;
				SqlDataAccessHelper.ExecNoneQueryString(
					@" update MenuPrivilege set IsYes = @IsYes where UserID = @UserID and MenuID = @MenuID
						IF @@ROWCOUNT=0 INSERT INTO MenuPrivilege (UserID,MenuID,IsYes) VALUES (@UserID,@MenuID,@IsYes) "
					, new string[] { "@UserID", "@IsYes", "@MenuID" }
					, new object[] { userid, check, menuid });
			}

			ACMessageBox.Show("Cập nhật phân quyền thành công.", "Thông báo", 2000);

		point1:
			lstTaikhoan_SelectedValueChanged(lstTaikhoan, null);

		}

		private void GetNode_DuocThaotac_CheckKetcong(TreeNode root, List<cPhongBan> dsphongbanChecked, List<cPhongBan> dsPhongBanUncheck) {
			if (root == null) return;
			var phong = (cPhongBan)root.Tag;
			if (phong.ChoPhep) {
				if (root.Checked)
					dsphongbanChecked.Add(phong);
				else dsPhongBanUncheck.Add(phong);
			}
			if (root.Nodes.Count > 0)
				for (int index = 0; index < root.Nodes.Count; index++) {
					TreeNode node = root.Nodes[index];
					GetNode_DuocThaotac_CheckKetcong(node, dsphongbanChecked, dsPhongBanUncheck);
				}
		}

		private void SetQuyenThaotac(DataTable tableChucnang) {
			// lấy dataSource danh sách chức năng, set default false nếu ko có row nào
			if (tableChucnang == null || tableChucnang.Rows.Count == 0) {
				for (int i = 0; i < checkQuyenThaotac.Items.Count; i++) {
					XL2.cChucNang chucnang = checkQuyenThaotac.Items[i] as XL2.cChucNang;
					if (chucnang == null) continue;
					checkQuyenThaotac.SetItemCheckState(i, CheckState.Unchecked);
					chucnang.IsYes = false;
				}
			}
			else {// tồn tại chức năng
				//duyệt từng chức năng trong danh sách chức năng, kiểm tra xem chức năng đó có trong csdl table ko? nếu có thì check, ko thì uncheck
				for (int i = 0; i < checkQuyenThaotac.Items.Count; i++) {
					XL2.cChucNang chucnang = checkQuyenThaotac.Items[i] as XL2.cChucNang;
					if (chucnang == null) continue;
					int id = chucnang.ID;
					DataRow[] arrRows = tableChucnang.Select("MenuID = " + id, "MenuID asc");// tìm trong table chức năng đó bật hay tắt, 
					if (arrRows.Length == 0) { // ko tồn tại phân quyền --> uncheck
						checkQuyenThaotac.SetItemCheckState(i, CheckState.Unchecked);
						chucnang.IsYes = false;
					}
					else {// tồn tại phân quyền -->? kiểm tra xem nó có  enable ko
						bool isYes = (bool)arrRows[0]["IsYes"];
						checkQuyenThaotac.SetItemCheckState(i, (isYes) ? CheckState.Checked : CheckState.Unchecked);
						chucnang.IsYes = isYes;
					}
				}
			}

			checkQuyenThaotac.Update();
		}




		private void checkQuyenThaotac_ItemCheck(object sender, ItemCheckEventArgs e) {
			XL2.cChucNang item = checkQuyenThaotac.Items[e.Index] as XL2.cChucNang;
			if (e.NewValue == CheckState.Checked) item.IsYes = true;
			else if (e.NewValue == CheckState.Unchecked) item.IsYes = false;
		}
	}
}
