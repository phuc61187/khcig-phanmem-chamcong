using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DAL;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;
using log4net;

namespace ChamCong_v04.UI.KhaiBao {
	public partial class frm_KBVang : Form {
		#region log tooltip và hàm ko quan trọng
		public readonly ILog lg = LogManager.GetLogger("frm_KBVang");

		private void toolTipHint_Draw(object sender, DrawToolTipEventArgs e) {
			Font f = new Font("Arial", 10.0f);
			e.DrawBackground();
			e.DrawBorder();
			e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, new PointF(2f, 2f));
		}

		private void toolTipHint_Popup(object sender, PopupEventArgs e) {
			Size temp = TextRenderer.MeasureText(toolTipHint.GetToolTip(e.AssociatedControl), new Font("Arial", 10.0f));
			temp.Width += 4;
			temp.Height += 4;
			e.ToolTipSize = temp;
		}
		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}


		#endregion

		public struct Working {
			public float cong;
			public float hour;
			public float Cong { get { return cong; } }
			public float Hour { get { return hour; } }
		}
		public List<cUserInfo> m_DSNV = new List<cUserInfo>(); // danh sách các nhân viên thuộc phòng đang chọn
		public List<cPhongBan> m_DSPhg = new List<cPhongBan>();
		public List<int> m_listIDPhongBan;
		public DataTable m_Bang_DSNV;
		public DataTable TaoBang_DSNV() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "check", "cUserInfo", "UserFullCode", "UserEnrollNumber", "UserFullName", "SchID", "SchName", "ChucVu", "MaPhong", "TenPhong", "HeSoLuongCB", "HeSoLuongSP", "HSBHCongThem" },
				new[] { typeof(bool), typeof(cUserInfo), typeof(string), typeof(int), typeof(string), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(float), typeof(float), typeof(float) });
			return kq;
		}

		public DateTime currMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
		#region biến checkbox check all: 1 của DSNV cần xem công. 2 DS Công của NV
		readonly CheckBox checkAll_GridDSNV = new CheckBox();
		void checkAll_CheckedChanged(object sender, EventArgs e) {
			//1. xác định datagrid nào đang check all
			DataGridView tempGrid = dgrdDSNVTrgPhg;

			// 2. kiểm tra nếu ko có dòng dữ liệu nào thì thoát
			if (tempGrid.Rows.Count == 0) return;

			//3. có dữ liệu, duyệt qua hết và gán giá trị check (or uncheck), sau đó endEDIT lại lưới để dòng đầu tiên có giá trị
			bool tmpCheckAll = ((CheckBox)sender).Checked;

			tempGrid.BeginEdit(true);
			var dt = tempGrid.DataSource as DataView;

			if (dt != null && dt.Count != 0) {
				foreach (DataRowView row in dt) {
					row["check"] = tmpCheckAll;
				}
			}

			tempGrid.EndEdit();
			tempGrid.Update();
			tempGrid.RefreshEdit();
		}
		#endregion


		// hàm xử lý -----------------------------------------------------------------------------

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			#region get id node hiện tại và các node con

			m_listIDPhongBan.Clear();
			if (e.Node.FirstNode != null) XL.GetIDNodeAndChildNode(e.Node, ref m_listIDPhongBan);
			else {
				var temp = ((cPhongBan)e.Node.Tag);
				if (temp.ChoPhep) m_listIDPhongBan.Add(temp.ID);
			}
			e.Node.Expand();

			#endregion

			#region mất kết nối csdl thì thoát form

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false)
			{
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}

			#endregion

			XL.KhoiTaoDSNV_ChamCong(m_DSNV, m_listIDPhongBan, m_DSPhg);
			XL.TaoTableDSNV(m_DSNV, m_Bang_DSNV);

			#region set datasource for autocomplete

			var Source = new AutoCompleteStringCollection();
			Source.AddRange((from nv in m_DSNV select nv.TenNV.ToUpperInvariant()).ToArray());
			tbSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tbSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			tbSearch.AutoCompleteCustomSource = Source;

			#endregion

			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = string.Empty;

			checkAll_GridDSNV.Checked = false;
		}

		public frm_KBVang() {
			InitializeComponent();
			log4net.Config.XmlConfigurator.Configure();

			m_listIDPhongBan = new List<int>();
			m_DSNV = new List<cUserInfo>();
			m_Bang_DSNV = TaoBang_DSNV();

			dgrdDSNVTrgPhg.AutoGenerateColumns = dgrdNgayVang.AutoGenerateColumns = false;
			DataView dataView = new DataView(m_Bang_DSNV);
			dgrdDSNVTrgPhg.DataSource = dataView;

			XL2.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));
		}

		private void frm_KhaiBaoVang_Load(object sender, EventArgs e)
		{
			#region kiểm tra kết nối csdl, mất kết nối thì thoát

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false)
			{
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}

			#endregion

			#region //2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan ,
			// trường hợp ko có phòng ban nào được phép thao tác thì báo và thoát form

			XL.KhoiTaoDSPhongBan(m_DSPhg, XL2.currUserID);
			if (m_DSPhg.Count == 0)
			{
				ACMessageBox.Show(Resources.Text_ChuaCapQuyenPhongBanThaoTac, Resources.Caption_ThongBao, 5000);
				Close();
				return;
			}
			XL.loadTreePhgBan(treePhongBan, XL2.TatcaPhongban, m_DSPhg);

			#endregion

			// đăng ký sự kiện cho tree và chọn topNode
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;
			treePhongBan.SelectedNode = treePhongBan.TopNode;


			// lấy tháng hiện tại cho dtpThang
			dtpThang.Value = currMonth;

			var tableLV = SqlDataAccessHelper.ExecuteQueryString("Select * from LoaiVang");
			cbLoaiVang.ValueMember = "AbsentCode";
			cbLoaiVang.DisplayMember = "AbsentDescription";
			cbLoaiVang.DataSource = tableLV;
			cbLoaiVang.SelectedIndex = 0;

		}


		private void dtpThang_ValueChanged(object sender, EventArgs e)
		{
			chk2AllWKDays.Checked = false;
			chk2ExceptSat.Checked = false;
			chk2ExceptSun.Checked = false;
			checklistNgay.Items.Clear();
			currMonth = dtpThang.Value;
			var startDay = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);
			var endddDay = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, DateTime.DaysInMonth(dtpThang.Value.Year, dtpThang.Value.Month));

			var indexDay = startDay;
			var lstNgay = new List<DateTime>();
			while (indexDay <= endddDay) {
				lstNgay.Add(indexDay);
				indexDay = indexDay.AddDays(1d);
			}
			checklistNgay.FormatString = "d - ddd";
			for (int index = 0; index < lstNgay.Count; index++)
			{
				DateTime t = lstNgay[index];
				checklistNgay.Items.Add(t);
			}
		}

		private void btnThem_Click(object sender, EventArgs e)
		{
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			// lấy ngày check
			List<DateTime> DSNgayCheck = (from object item in checklistNgay.CheckedItems select (DateTime)item).ToList();
			if (DSNgayCheck.Count == 0) {
				ACMessageBox.Show("Bạn chưa chọn ngày vắng", "Thông báo", 2000);
				return;
			}

			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(DSNgayCheck[0], DSNgayCheck[DSNgayCheck.Count-1])) {
				MessageBox.Show(String.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "khai báo vắng", "khai báo vắng", "khai báo vắng"), 
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}

			#endregion


			#region hỏi lại trước khi thực hiện
			if (MessageBox.Show(Resources.Text_XacNhanThemKhaiVang, Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}
			#endregion
			//1. lấy dữ liệu từ form
			dtpThang.Update();
			currMonth = dtpThang.Value;

			dgrdDSNVTrgPhg.EndEdit();
			dgrdDSNVTrgPhg.Update();

			BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();
			//2. lấy danh sách nhân viên check, nếu chưa có nv nào check thì thông báo
			var listMaCC_NV = (from DataGridViewRow dataGridViewRow  in dgrdDSNVTrgPhg.Rows
						  		let row = (DataRowView)dataGridViewRow.DataBoundItem
								where (row["check"] != DBNull.Value && (bool)row["check"])
								select ((cUserInfo)row["cUserInfo"]).MaCC)
								.ToList();

			if (listMaCC_NV.Count == 0) {
				ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000);
				return;
			}

			// lấy loại vắng
			if (cbLoaiVang.SelectedItem == null) {
				ACMessageBox.Show("Bạn chưa chọn loại vắng", "Thông báo", 2000);
				return;
			}
			var rowLV = cbLoaiVang.SelectedItem as DataRowView;

			var absentCode = rowLV["AbsentCode"].ToString();
			float workingDay = 0f;
			if (rad2Gio.Checked) workingDay = 0.25f;
			else if (radNuaNgay.Checked) workingDay = 0.5f;
			else if (rad1ngay.Checked) workingDay = 1f;
			else workingDay = 0f;
			var workingTime = 0f;
			var phuCapString = maskedTextBox1.Text;
			var phuCapInt = 0;
			var phuCapFloat = 0f;
			if (int.TryParse(phuCapString, out phuCapInt)==false || phuCapString.Length < 3) { ACMessageBox.Show("Nhập phụ cấp chưa đúng định dạng.", Resources.Caption_Loi, 2000); return;}
			phuCapFloat = Convert.ToSingle(phuCapInt)/100f;

			#region set working time tùy theo workingDay

			if (Math.Abs(workingDay - 0f) < 0.01f) workingTime = 0f;
			else if (Math.Abs(workingDay - 0.25f) < 0.01f) workingTime = 2f;
			else if (Math.Abs(workingDay - 0.5f) < 0.01f) workingTime = 4f;
			else if (Math.Abs(workingDay - 1f) < 0.01f) workingTime = 8f;

			#endregion

			if (Math.Abs(workingDay-0.25f)<0.01f)
			{
				if  (!(absentCode.ToLower()=="p"||absentCode.ToLower()=="ro"))
				{
					ACMessageBox.Show("Chưa hỗ trợ vắng 2 tiếng ngoài phép và việc riêng.", "Chức năng chưa được hỗ trợ", 3000);
					return;
				}
			}

			var formatString = "[{0}] đã xin phép vắng [{1}] [{2}] ngày ngày [{3}]";
			var tableVang = DAO.LietKeNgayVangChoNV(listMaCC_NV, DSNgayCheck.Min(), DSNgayCheck.Max());
			if (tableVang.Rows.Count > 0) {
				List<Warning> listWarning = new List<Warning>();
				foreach (var nv in listMaCC_NV) {//duyệt từng nhân viên
					foreach (var ngay in DSNgayCheck) {// duyệt từng ngày check vắng của nhân viên
						DateTime ngay1 = ngay;
						int nv1 = nv;
						var result = (from DataRow item in tableVang.Rows
									  where (int)item["UserEnrollNumber"] == nv1 && (DateTime)item["TimeDate"] == ngay1
									  select item).ToList(); // lấy danh sách các xin phép vắng trong ngày xác định
						if (result.Any()) {
							// nếu có xin phép vắng thì ghi lại chuỗi các xin phép vắng đó
							var userfullname = result[0]["UserFullName"].ToString();
							listWarning.AddRange(result.Select(row123 => new Warning() {
								CB = "Đã có xin phép vắng",
								ND = string.Format(formatString, userfullname, row123["AbsentCode"],
														((float)row123["Workingday"]).ToString("0.0#"),
														((DateTime)row123["TimeDate"]).ToString("dd/MM/yyyy"))
							}));
						}
					}
				}

				// hiện form cảnh báo, nếu xác nhận tiếp tục thì thực hiện , ko thì dừng
				frmWarning frm = new frmWarning { StartPosition = FormStartPosition.CenterParent};
				frm.listWarning = listWarning;
				frm.ShowDialog();
				if (frm.TiepTuc == false) return;
			}

			IEnumerable<dynamic> tempList = (from macc in listMaCC_NV
			                          from ngay in DSNgayCheck
			                          select new {MaCC = macc, NgayVang = ngay});
			DAO.ThemNgayVang(tempList, workingDay, workingTime, phuCapFloat, absentCode);


			// sau khi thao tác xong thì clear check các ngày liệt kê lại
			chk2ExceptSat.Checked = false;
			chk2ExceptSun.Checked = false;
			for (int i = 0; i < checklistNgay.Items.Count; i++) {
				checklistNgay.SetItemChecked(i, false);
			}

			Thread.Sleep(20);
			btnLietKe.PerformClick();
		}


		private void btnXoa_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1), new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, DateTime.DaysInMonth(dtpThang.Value.Year, dtpThang.Value.Month)))) {
				MessageBox.Show(string.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "khai báo vắng", "xoá khai báo vắng", ""),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}


			#endregion


			#region hỏi lại trước khi thực hiện

			if (MessageBox.Show(Resources.Text_XacNhanXoaKhaiBaoVang, Resources.Caption_XacNhan, MessageBoxButtons.YesNo) ==DialogResult.No) {
				return;
			}

			#endregion

			dgrdNgayVang.EndEdit();
			dgrdNgayVang.Update();

			var arrRecord = (from DataGridViewRow dataGridViewRow in dgrdNgayVang.SelectedRows
							 select ((DataRowView)dataGridViewRow.DataBoundItem)).ToList();
			if (arrRecord.Count == 0) {
				return;
			}

			var kqThaotac = DAO.XoaNgayVangNV(arrRecord);
			if (kqThaotac == false)
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			GC.Collect();

			Thread.Sleep(20);
			btnLietKe.PerformClick();
		}

		private void btnLietKe_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			//1. lấy dữ liệu từ form
			dtpThang.Update();
			currMonth = dtpThang.Value;
			var ngayBD = DateTime.MinValue;
			var ngayKT = DateTime.MinValue;
				ngayBD = new DateTime(currMonth.Year, currMonth.Month, 1);
				ngayKT = new DateTime(currMonth.Year, currMonth.Month, DateTime.DaysInMonth(currMonth.Year, currMonth.Month));
			//-----------INFO bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

			dgrdDSNVTrgPhg.EndEdit();
			dgrdDSNVTrgPhg.Update();

			//2. lấy danh sách nhân viên check, nếu chưa có nv nào check thì thông báo
			BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();
			var listNV = (from DataGridViewRow dataGridViewRow in dgrdDSNVTrgPhg.Rows
						  let row = (DataRowView) dataGridViewRow.DataBoundItem
						  where (row["check"] != DBNull.Value && (bool)row["check"])
						  select ((cUserInfo)row["cUserInfo"]).MaCC).ToList();

			if (listNV.Count == 0) {
				ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000);
				return;
			}

			var table = DAO.LietKeNgayVangChoNV(listNV, ngayBD, ngayKT);
			dgrdNgayVang.DataSource = table;
			if (table.Rows.Count == 0) 
				ACMessageBox.Show("Các nhân viên đang chọn không vắng ngày nào trong tháng.", "Thông báo", 1500);
			GC.Collect();
		}


		private void btnTim_Click(object sender, EventArgs e) {
			if (dgrdDSNVTrgPhg.DataSource == null) return;

			var searchStr1 = tbSearch.Text;
			var searchStr = string.Format("UserFullName like '%{0}%' or UserFullCode like '%{0}%'", searchStr1);
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = searchStr;
		}

		private void linkHienThiTatCaNV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = string.Empty;
		}


		private void chk2AllDays_CheckedChanged(object sender, EventArgs e) {
			bool checkT7 = chk2ExceptSat.Checked;
			bool checkCN = chk2ExceptSun.Checked;
			bool checkAll = chk2AllWKDays.Checked;
			for (int i = 0; i < checklistNgay.Items.Count; i++) {
				var obj = checklistNgay.Items[i];
				DateTime ngay = (DateTime)obj;
				checklistNgay.SetItemChecked(i, checkAll);
				if (ngay.DayOfWeek != DayOfWeek.Saturday && ngay.DayOfWeek != DayOfWeek.Sunday) checklistNgay.SetItemChecked(i, checkAll);
				if (ngay.DayOfWeek == DayOfWeek.Saturday) checklistNgay.SetItemChecked(i, checkT7);
				if (ngay.DayOfWeek == DayOfWeek.Sunday) checklistNgay.SetItemChecked(i, checkCN);
			}

		}

		private void btnKBVangDaiHan_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			dgrdDSNVTrgPhg.EndEdit();
			dgrdDSNVTrgPhg.Update();

			BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();
			//2. lấy danh sách nhân viên check, nếu chưa có nv nào check thì thông báo
			var listMaCC_NV = (from DataGridViewRow dataGridViewRow in dgrdDSNVTrgPhg.Rows
							   let row = (DataRowView)dataGridViewRow.DataBoundItem
							   where (row["check"] != DBNull.Value && (bool)row["check"])
							   select ((cUserInfo)row["cUserInfo"]).MaCC)
								.ToList();

			if (listMaCC_NV.Count == 0) {
				ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000);
				return;
			}

			frmKBVang_DaiHan frm = new frmKBVang_DaiHan {listMaCC_NV = listMaCC_NV, WindowState = FormWindowState.Normal, StartPosition = FormStartPosition.CenterParent};
			frm.ShowDialog();

			// sau khi thao tác xong thì liệt kê lại
			Thread.Sleep(20);
			btnLietKe.PerformClick();

		}

		private void tbSearch_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter)
				btnTim.PerformClick();

		}



	}
}
