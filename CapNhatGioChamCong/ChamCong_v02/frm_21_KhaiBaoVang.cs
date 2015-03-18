using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ChamCong_v02.BUS;
using ChamCong_v02.DAO;
using log4net;

namespace ChamCong_v02 {
	public partial class frm_21_KhaiBaoVang : Form {
		public readonly ILog lg = LogManager.GetLogger("frm_21_KhaiBaoVang");

		#region local variable
		public List<int> m_listIDPhongBan { get; set; }

		public DateTime currMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
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
			else tempGrid = dgrdNgayVang;

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


		public frm_21_KhaiBaoVang() {
			InitializeComponent();
			dgrdDSNVTrgPhg.AutoGenerateColumns = dgrdNgayVang.AutoGenerateColumns = false;
			ThamSo.VeCheckBox_CheckAll(dgrdNgayVang, checkAll_GridNgayVang, checkAll_CheckedChanged, new Point(7, 3));
			ThamSo.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));
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
			dtpThang.Value = currMonth;

			DataTable tableLV = SqlDataAccessHelper.ExecuteQueryString("Select * from LoaiVang");
			LoadComboLoaiVang(tableLV);

		}

		private void LoadComboLoaiVang(DataTable tableLV) {
			cbLoaiVang.ValueMember = "AbsentCode";
			cbLoaiVang.DisplayMember = "AbsentDescription";
			cbLoaiVang.DataSource = tableLV;
			cbLoaiVang.SelectedIndex = 0;
		}

		private void dtpThang_ValueChanged(object sender, EventArgs e) {
			currMonth = dtpThang.Value;
			DateTime startDay = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);
			DateTime endddDay = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, DateTime.DaysInMonth(dtpThang.Value.Year, dtpThang.Value.Month));

			DateTime indexDay = startDay;
			List<DateTime> lstNgay = new List<DateTime>();
			while (indexDay <= endddDay) {
				lstNgay.Add(indexDay);
				indexDay = indexDay.AddDays(1d);
			}
			checklistNgay.FormatString = "d - ddd";

			checklistNgay.DataSource = lstNgay;
		}

		private void btnThem_Click(object sender, EventArgs e) {
			//1. lấy dữ liệu từ form
			dtpThang.Update();
			currMonth = dtpThang.Value;

			dgrdDSNVTrgPhg.EndEdit();
			dgrdDSNVTrgPhg.Update();

			List<int> dsMaCC_Checked = LayDSMaCC_Checked(dgrdDSNVTrgPhg);
			int[] ArrDSMaCC_Checked = dsMaCC_Checked.ToArray();
			if (dsMaCC_Checked.Count == 0) {
				AutoClosingMessageBox.Show("Chưa chọn Nhân viên", "Thông báo", 2000);
				return;
			}


			// lấy ngày check
			List<DateTime> DSNgayCheck;
			if (rdChonNgayTrongThang.Checked) {
				DSNgayCheck = LayNgayCheck(checklistNgay);
			}
			else {
				DSNgayCheck = LayNgayCheck(dtpNgayBD.Value.Date, dtpNgayKT.Value.Date);
			}

			// lấy loại vắng
			DataRowView row = cbLoaiVang.SelectedItem as DataRowView;

			bool kqThaotac = DAL.KhaiBaoNgayVangChoNV(ArrDSMaCC_Checked, DSNgayCheck, row, ThamSo.currUserID);
			if (kqThaotac == false)
				MessageBox.Show("Có lỗi trong quá trình thực hiện. Vui lòng kiểm tra lại dữ liệu.", "Lỗi");

			// sau khi thao tác xong thì liệt kê lại
			btnLietKe_Click(btnLietKe, null);
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


		public List<DateTime> LayNgayCheck(CheckedListBox checkedList) {
			List<DateTime> kq = new List<DateTime>();
			if (checkedList.CheckedItems.Count == 0) return kq;
			foreach (object item in checkedList.CheckedItems) {
				kq.Add((DateTime)item);
			}
			return kq;
		}

		private List<DateTime> LayNgayCheck(DateTime dateTime1, DateTime dateTime2) {
			// truyền vào ngày dtpNgayBD.Date
			List<DateTime> kq = new List<DateTime>();
			for (DateTime indexNgay = dateTime1.Date; indexNgay <= dateTime2; indexNgay = indexNgay.AddDays(1d)) {
				kq.Add(indexNgay.Date);
			}
			return kq;
		}

		private void btnXoa_Click(object sender, EventArgs e) {
			//-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

			dgrdNgayVang.EndEdit();
			dgrdNgayVang.Update();

			//2. lấy danh sách row cần xóa
			DataTable table = dgrdNgayVang.DataSource as DataTable;
			if (table == null) return;

			DataRow[] arrRecord = table.Select("check = true", "UserEnrollNumber asc");
			if (arrRecord.Length == 0) return;

			bool kqThaotac = DAL.XoaNgayVangNV(arrRecord);
			if (kqThaotac == false)
				MessageBox.Show("Có lỗi trong quá trình thực hiện. Vui lòng kiểm tra lại dữ liệu.", "Lỗi");
			btnLietKe_Click(btnLietKe, null);
		}

		private void btnLietKe_Click(object sender, EventArgs e) {
			//1. lấy dữ liệu từ form
			dtpThang.Update();
			currMonth = dtpThang.Value;
			DateTime ngayBD = DateTime.MinValue;
			DateTime ngayKT = DateTime.MinValue;
			if (rdChonNgayTrongKhoang.Checked) {
				ngayBD = dtpNgayBD.Value.Date;
				ngayKT = dtpNgayKT.Value.Date;
			}
			else {
				ngayBD = new DateTime(currMonth.Year, currMonth.Month, 1);
				ngayKT = new DateTime(currMonth.Year, currMonth.Month, DateTime.DaysInMonth(currMonth.Year, currMonth.Month));
			}
			//-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

			dgrdDSNVTrgPhg.EndEdit();
			dgrdDSNVTrgPhg.Update();

			//2. lấy danh sách nhân viên check
			DataTable table = dgrdDSNVTrgPhg.DataSource as DataTable;
			if (table == null) return;
			DataRow[] arrRecord = table.Select("check = true", "UserEnrollNumber asc", DataViewRowState.CurrentRows);
			if (arrRecord.Length == 0) {
				AutoClosingMessageBox.Show("Chưa chọn Nhân viên", "Thông báo", 2000);
				return;
			}
			object[] arrDSNVCheck = new object[arrRecord.Length];

			for (int i = 0; i < arrRecord.Length; i++) {
				DataRow row = arrRecord[i];
				arrDSNVCheck[i] = ((int)row["UserEnrollNumber"]);
			}

			DataTable dataTable = dgrdNgayVang.DataSource as DataTable;
			if (dataTable != null) dataTable.Rows.Clear();
			dataTable = DAL.LietKeNgayVangChoNV(arrDSNVCheck, ngayBD, ngayKT);
			dataTable.Columns.Add("check", typeof(bool));
			dgrdNgayVang.DataSource = dataTable;

			GC.Collect();
		}

		private void rdChonNgayTrongKhoang_CheckedChanged(object sender, EventArgs e) {
			if (rdChonNgayTrongKhoang.Checked) {
				dtpNgayBD.Enabled = dtpNgayKT.Enabled = true;
				dtpThang.Enabled = checklistNgay.Enabled = false;
				if (checklistNgay.Items.Count > 0) {
					for (int i = 0; i < checklistNgay.Items.Count; i++) {
						checklistNgay.SetItemChecked(i, false);
					}
				}
			}
			else {
				dtpNgayBD.Enabled = dtpNgayKT.Enabled = false;
				dtpThang.Enabled = checklistNgay.Enabled = true;
			}
		}
	}
}
