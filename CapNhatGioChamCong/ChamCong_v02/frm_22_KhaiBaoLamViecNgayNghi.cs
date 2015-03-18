using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ChamCong_v02.BUS;
using ChamCong_v02.DAO;
using log4net;

namespace ChamCong_v02 {
	public partial class frm_22_KhaiBaoLamViecNgayNghi : Form {
		public readonly ILog lg = LogManager.GetLogger("frm_22_KhaiBaoLamViecNgayNghi");

		#region local variable

		public List<int> m_listIDPhongBan { get; set; }
		public DateTime NgayKhaiBao = DateTime.Today;
		#region 3 biến checkbox check all: 1 của DSNV cần xem công. 2 DS Công của NV
		readonly CheckBox checkAll_GridDSNV = new CheckBox();
		readonly CheckBox checkAll_GridNgayVang = new CheckBox();
		#endregion


		#endregion

		// hàm xử lý -----------------------------------------------------------------------------
		#region load treeview new
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
			checkAll_GridDSNV.Checked = false;
		}

		#endregion

		#region vẽ checkBox check all và xử lý sự kiện check

		void checkAll_CheckedChanged(object sender, EventArgs e) {
			//1. xác định datagrid nào đang check all
			DataGridView tempGrid;
			if (sender == checkAll_GridDSNV) tempGrid = dgrdDSNVTrgPhg;
			else tempGrid = dgrdNgayNghi;

			// 2. kiểm tra nếu ko có dòng dữ liệu nào thì thoát
			if (tempGrid.Rows.Count == 0) return;

			//3. có dữ liệu, duyệt qua hết và gán giá trị check (or uncheck), sau đó endEDIT lại lưới để dòng đầu tiên có giá trị
			bool tmpCheckAll = ((CheckBox)sender).Checked;

			tempGrid.BeginEdit(true);
			DataTable dt = tempGrid.DataSource as DataTable;

			if (dt != null && dt.Rows.Count != 0) {
				foreach (DataRow row in dt.Rows)
					row["check"] = tmpCheckAll;
			}

			tempGrid.EndEdit();
			tempGrid.Update();
		}
		#endregion


		public frm_22_KhaiBaoLamViecNgayNghi() {
			InitializeComponent();
			dgrdDSNVTrgPhg.AutoGenerateColumns = dgrdNgayNghi.AutoGenerateColumns = false;
			ThamSo.VeCheckBox_CheckAll(dgrdNgayNghi, checkAll_GridNgayVang, checkAll_CheckedChanged, new Point(7, 3));
			ThamSo.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));
			DateTime today = DateTime.Today;
			dtpThang.Value = new DateTime(today.Year, today.Month, today.Day);
		}

		private void frm_KhaiBaoVang_Load(object sender, EventArgs e) {
			// 1. khởi tạo các biến cục bộ
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

			// lấy tháng hiện tại cho dtpThang
			dtpThang.Value = NgayKhaiBao;

		}

		private void btnThem_Click(object sender, EventArgs e) {
			//1. lấy dữ liệu từ form
			dtpThang.Update();
			NgayKhaiBao = dtpThang.Value.Date;
			//-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

			if (dgrdDSNVTrgPhg.DataSource != null)
				BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();
			//2. lấy danh sách nhân viên check, nếu chưa có nv nào check thì thông báo
			List<int> dsMaCC_Checked = LayDSMaCC_Checked(dgrdDSNVTrgPhg);
			int[] ArrDSMaCC_Checked = dsMaCC_Checked.ToArray();
			if (dsMaCC_Checked.Count == 0) {
				AutoClosingMessageBox.Show("Chưa chọn Nhân viên", "Thông báo", 2000);
				return;
			}

			Single PCThem = 1f;// tương đương 100% công làm
			Single PCDem = 1f;// tương đương 100% công làm ca đêm

			// tính phụ cấp thêm 100% công : tổng cộng 1 công , 100%phụ cấp

			bool kqThaotac = DAL.KhaiBaoLVNgayNghiChoNV(ArrDSMaCC_Checked, NgayKhaiBao, PCThem, PCDem, ThamSo.currUserID);// mặc định bên dưới là chưa được duyệt
			if (kqThaotac == false)
				MessageBox.Show("Có lỗi trong quá trình thực hiện. Vui lòng kiểm tra lại dữ liệu.", "Lỗi");

			// sau khi thao tác xong thì liệt kê lại
			btnLietKe.PerformClick();
		}



		private void btnXoa_Click(object sender, EventArgs e) {

			if (dgrdDSNVTrgPhg.DataSource != null)
				BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();
			dgrdNgayNghi.Update();

			//2. lấy danh sách row cần xóa
			DataTable table = dgrdNgayNghi.DataSource as DataTable;
			if (table == null) return;

			DataRow[] arrRecord = table.Select("check = true and Duyet = false", "UserEnrollNumber asc");
			if (arrRecord.Length == 0) return;

			if (MessageBox.Show("Xoá khai báo ngày làm việc tính PC 100% chưa duyệt?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) {
				bool kqThaotac = DAL.XoaLamViecNgayNghiDaKhaiBao(arrRecord);
				if (kqThaotac == false)
					MessageBox.Show("Có lỗi trong quá trình thực hiện. Vui lòng kiểm tra lại dữ liệu.", "Lỗi");
				btnLietKe_Click(btnLietKe, null);
			}
		}

		private void btnLietKe_Click(object sender, EventArgs e) {
			//1. lấy dữ liệu từ form
			dtpThang.Update();
			NgayKhaiBao = dtpThang.Value;
			DateTime ngayBD = new DateTime(NgayKhaiBao.Year, NgayKhaiBao.Month, 1);
			DateTime ngayKT = new DateTime(NgayKhaiBao.Year, NgayKhaiBao.Month, DateTime.DaysInMonth(NgayKhaiBao.Year, NgayKhaiBao.Month));
			//-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

			dgrdDSNVTrgPhg.EndEdit();
			dgrdDSNVTrgPhg.Update();

			//2. lấy danh sách nhân viên check
			if (dgrdDSNVTrgPhg.DataSource != null)
				BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();
			//2. lấy danh sách nhân viên check, nếu chưa có nv nào check thì thông báo
			List<int> dsMaCC_Checked = LayDSMaCC_Checked(dgrdDSNVTrgPhg);
			int[] ArrDSMaCC_Checked = dsMaCC_Checked.ToArray();
			if (dsMaCC_Checked.Count == 0) {
				AutoClosingMessageBox.Show("Chưa chọn Nhân viên", "Thông báo", 2000);
				return;
			}

			DataTable dataTable = dgrdNgayNghi.DataSource as DataTable;
			if (dataTable != null) dataTable.Rows.Clear();
			dataTable = DAL.LietKeLamViecNgayNghiDaKhaiBao(ArrDSMaCC_Checked, ngayBD, ngayKT);

			dataTable.Columns.Add("check", typeof(bool));
			dgrdNgayNghi.DataSource = dataTable;

			GC.Collect();
		}

		private void btnLietKe_MouseHover(object sender, EventArgs e) {
			toolTip1.Show("Liệt kê các khai báo đi làm vào ngày nghỉ được tính PC 100% lương.", btnLietKe, 3000);
		}

		private List<int> LayDSMaCC_Checked(DataGridView dataGridView) {
			List<int> kq = new List<int>();
			if (dataGridView.Rows.Count == 0) return kq;
			foreach (DataGridViewRow row in dataGridView.Rows) {
				DataRowView rowView = row.DataBoundItem as DataRowView;
				if (rowView == null) continue;
				bool IsChecked = (rowView["check"] != DBNull.Value) ? (bool)rowView["check"] : false;
				if (IsChecked) kq.Add((int)rowView["UserEnrollNumber"]);
			}
			return kq;
		}

	}
}
