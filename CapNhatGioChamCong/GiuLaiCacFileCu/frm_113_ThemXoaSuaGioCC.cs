using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GiuLaiCacFileCu {
	public partial class frm_113_ThemXoaSuaGioCC : Form {
		public frm_113_ThemXoaSuaGioCC() {
		}

/*
		public bool IsReload;
		public DataRow selectedRow; // row đang chọn từ form xem công lưu giữ thông tin ngày công của nhân viên đang chọn
		public cUserInfo nhanvien_goc;// nhân viên đang chọn, để update lại thông tin các ds check , cio , ngày công sau khi thêm giờ
		public cNgayCong NgayCong_goc;// ngày công đang chọn, để update lại thông tin các danh sách và công, phụ cấp
		public List<cCaAbs> dsCa; // danh sách các Ca thuộc lịch trình của nhân viên, hàm load sẽ thêm item "--" ở vị trí 0
		public DataTable m_Bang_ChiTiet;
		public DataTable TaoTable_ChiTiet() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "cUserInfo", "cNgayCong", "cChkInOut", "cChk", "TimeStrThu", "TimeStrNgay", "TimeStr", "Loai", "MachineNo", "Source", "ShiftCode", "IsEdited", "IsEnableEdit" },
				new[] { typeof(cUserInfo), typeof(cNgayCong), typeof(cChkInOut), typeof(cChk), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(string), typeof(int), typeof(string), typeof(string), typeof(bool), typeof(bool) }
				);
			return kq;
		}

		public frm_113_ThemXoaSuaGioCC() {
			InitializeComponent();

			//tạo table chi tiết để gán dataSource
			m_Bang_ChiTiet = TaoTable_ChiTiet();
			// ko cho autogenerate các cột
			dgrdGioKDQD.AutoGenerateColumns = false;
			dgrdGioKDQD.DataSource = m_Bang_ChiTiet;
			// gán trước các datamember, value member cho các comboBox
			cbCa_Them.ValueMember = cbCa_Suaa.ValueMember = "ShiftID";
			cbCa_Them.DisplayMember = cbCa_Suaa.DisplayMember = "ShiftCode";
		}

		private void frm_ThongTinThemGio_Load(object sender, EventArgs e) {

			// 1. lấy thông tin nhân viên, fill thông tin vào 2 tb mã và tên nv
			nhanvien_goc = selectedRow["cUserInfo"] as cUserInfo;
			NgayCong_goc = selectedRow["cNgayCong"] as cNgayCong;
			tbMaCC.Text = nhanvien_goc.UserFullCode;
			tbTenNV.Text = nhanvien_goc.UserFullName;

			//tạo dsCa gốc cho toàn form, combobox nào muốn sử dụng lại thì tạo mới từ cái gốc này và thay đổi tùy ý để giữ cái này làm gốc ko bị thay đổi
			dsCa = new List<cCaAbs>(nhanvien_goc.LichTrinhLV.DSCa); ;
			dsCa.Insert(0, new cCaChuan { ShiftID = -1, ShiftCode = "--" });
			// 
			// .triển khai load datagrid, trong khi load datagrid cũng đồng thời triển khai load dataSource cho các combobox
			loadGrid();
		}

		public void loadGrid() {
			m_Bang_ChiTiet.Rows.Clear();
			//load datagrid từ dataTable
			// kiểm  tra nếu ngày trước bị edited hoặc ngày sau bị edited thì load các giờ của ngày hôm trước, ngày hôm sau
			if (NgayCong_goc.prev != null) {
				loadRow(NgayCong_goc.prev, false);
			}
			loadRow(NgayCong_goc, true);
			if (NgayCong_goc.next != null) {
				loadRow(NgayCong_goc.next, false);
			}


			VeLaiCacGioCoThayDoi();
			//dgrdGioKDQD.DataSource = m_Bang_ChiTiet;
			// reset lại các control trong Thêm, Xóa Sửa
		}

		private void loadRow(cNgayCong ngayCong, bool isEnableEdit) {
			if (ngayCong.DSVaoRa.Count == 0) {
				var row1 = m_Bang_ChiTiet.NewRow();
				var row2 = m_Bang_ChiTiet.NewRow();
				row1["cNgayCong"] = row2["cNgayCong"] = ngayCong;
				row1["cUserInfo"] = row2["cUserInfo"] = nhanvien_goc;
				row1["TimeStrThu"] = row1["TimeStrNgay"] = ngayCong.Ngay.Date;
				row2["TimeStrThu"] = row2["TimeStrNgay"] = ngayCong.Ngay.Date;
				row1["Loai"] = "Vào";
				row2["Loai"] = "Ra";
				if (ngayCong.IsEdited > 0) {
					row1["IsEdited"] = true;
					row2["IsEdited"] = true;
				}
				row1["IsEnableEdit"] = isEnableEdit;
				row2["IsEnableEdit"] = isEnableEdit;

				m_Bang_ChiTiet.Rows.Add(row1);
				m_Bang_ChiTiet.Rows.Add(row2);
			}

			foreach (var CIO in ngayCong.DSVaoRa) {
				var row1 = m_Bang_ChiTiet.NewRow();
				var row2 = m_Bang_ChiTiet.NewRow();
				row1["Loai"] = "Vào";
				row2["Loai"] = "Ra";
				row1["cNgayCong"] = row2["cNgayCong"] = ngayCong;
				row1["cUserInfo"] = row2["cUserInfo"] = nhanvien_goc;
				row1["TimeStrThu"] = row1["TimeStrNgay"] = ngayCong.Ngay.Date;
				row2["TimeStrThu"] = row2["TimeStrNgay"] = ngayCong.Ngay.Date;
				var temp = string.Empty;
				CIO.LayChuoiThuocCa_01(ref temp);
				if (CIO.HaveINOUT == -1) {
					row1["cChk"] = CIO.Vao;
					row1["TimeStr"] = CIO.Vao.TimeStr;
					row1["MachineNo"] = CIO.Vao.MachineNo;
					row1["Source"] = CIO.Vao.Source;
					row1["ShiftCode"] = temp;
				}
				else if (CIO.HaveINOUT == -2) {
					row2["cChk"] = CIO.Raa;
					row2["TimeStr"] = CIO.Raa.TimeStr;
					row2["MachineNo"] = CIO.Raa.MachineNo;
					row2["Source"] = CIO.Raa.Source;
					row2["ShiftCode"] = temp;
				}
				else {
					row1["cChk"] = CIO.Vao;
					row2["cChk"] = CIO.Raa;
					row1["TimeStr"] = CIO.Vao.TimeStr;
					row2["TimeStr"] = CIO.Raa.TimeStr;
					row1["MachineNo"] = CIO.Vao.MachineNo;
					row2["MachineNo"] = CIO.Raa.MachineNo;
					row1["Source"] = CIO.Vao.Source;
					row2["Source"] = CIO.Raa.Source;
					row1["ShiftCode"] = row2["ShiftCode"] = temp;
				}

				row1["IsEdited"] = (CIO.IsEdited > 0);
				row2["IsEdited"] = (CIO.IsEdited > 0);

				row1["IsEnableEdit"] = isEnableEdit;
				row2["IsEnableEdit"] = isEnableEdit;

				m_Bang_ChiTiet.Rows.Add(row1);
				m_Bang_ChiTiet.Rows.Add(row2);
			}
		}

		private void VeLaiCacGioCoThayDoi() {
			foreach (DataGridViewRow dataGridViewRow in dgrdGioKDQD.Rows) {
				var row = (dataGridViewRow.DataBoundItem as DataRowView).Row;
				if (row["IsEnableEdit"] == DBNull.Value || (bool)row["IsEnableEdit"] == false) {
					dataGridViewRow.DefaultCellStyle.BackColor = Color.LightGray;
				}
				if (row["IsEdited"] != DBNull.Value && (bool)row["IsEdited"]) {
					dataGridViewRow.DefaultCellStyle.BackColor = Color.LightGreen;
				}
			}
		}

		private void dgrdGioKDQD_SelectionChanged(object sender, EventArgs e) {
			// mỗi lần chọn 1 row thì load dữ liệu vào các group thêm xóa sửa
			if (dgrdGioKDQD.SelectedRows.Count == 0) { // trường hợp này xảy ra sau khi gán dataSource tức load lần đầu hay reload
				//reset từng  group
				chkGioVao.Checked = false;
				chkGioRaa.Checked = false;
				tbGioCu_Suaa.Text = tbGioCu_Xoaa.Text = string.Empty;

				//không row nào được chọn thì disable button, ko cho thực hiện thao tác
				btnThem.Enabled = false;
				btnSuaa.Enabled = false;
				btnXoaa.Enabled = false;
				btnChuyenDoi.Enabled = false;
				return;
			}
			//chuyển từ dataGridViewRow sang DataRowView sang DataRow
			var row = ((DataRowView)(dgrdGioKDQD.SelectedRows[0]).DataBoundItem).Row;

			// lấy dữ liệu từ row đang chọn: 1. nếu đang chọn ngày trước hoặc sau thì disable các button và thoát; 2. lấy dữ liệu và load group
			var IsEnableEdit = (row["IsEnableEdit"] != DBNull.Value) ? (bool)row["IsEnableEdit"] : false;
			if (IsEnableEdit == false) {
				chkGioVao.Checked = false;
				chkGioRaa.Checked = false;
				tbGioCu_Suaa.Text = tbGioCu_Xoaa.Text = string.Empty;

				btnThem.Enabled = false;
				btnSuaa.Enabled = false;
				btnXoaa.Enabled = false;
				btnChuyenDoi.Enabled = false;
				return;
			}

			//2. lấy dữ liệu và load group
			var ngayCong = (row["cNgayCong"] != DBNull.Value) ? (cNgayCong)row["cNgayCong"] : null;
			var chk = (row["cChk"] != DBNull.Value) ? (cChk)row["cChk"] : null;

			LoadGroup_Them(ngayCong.Ngay);
			LoadGroup_Suaa(ngayCong, chk);
			LoadGroup_Xoaa(chk);
		}

		private void LoadGroup_Xoaa(cChk check) {
			if (check == null) // check null thì reset group
			{
				tbGioCu_Xoaa.Text = string.Empty;
				cbLyDo_Xoaa.SelectedIndex = 0;
				tbGhiChu_Xoaa.Text = string.Empty;
				btnXoaa.Enabled = false;
			}
			else {
				var kieugio = (check.MachineNo % 2 == 1) ? "Vào" : "Ra";
				tbGioCu_Xoaa.Text = kieugio + " " + check.TimeStr.ToString("H:mm ddd d/M", Application.CurrentCulture);
				btnXoaa.Enabled = true;
			}
		}

		private void LoadGroup_Suaa(cNgayCong ngayCong, cChk check) {
			if (check == null) {
				btnSuaa.Enabled = false; // ko tồn tại giờ cũ nên ko cho sửa
				btnChuyenDoi.Enabled = false;// ko tồn tại giờ cũ nên ko cho sửa
				tbGioCu_Suaa.Text = string.Empty;
				radVao.Enabled = radRaa.Enabled = false;
				cbCa_Suaa.DataSource = null;
				dtpGioMoi_Sua.Enabled = false;
				cbLyDo_Suaa.Enabled = false;
				tbGhiChu_Suaa.Text = string.Empty;
				tbGhiChu_Suaa.Enabled = false;
				//tbd còn các control khác??

			}
			else {
				// xem xét check là CIO_V hay CIO_A, CIO_A cho sửa, CIO_V ko cho sửa, trong CIO_A coi có phải là check đệm giữa ca ko
				tbGioCu_Suaa.Text = ((check.MachineNo % 2 == 1) ? "Vào" : "Ra") + " " + check.TimeStr.ToString("H:mm ddd d/M");
				radVao.Checked = (check.MachineNo % 2 == 1);
				radRaa.Checked = (check.MachineNo % 2 == 0);

				var dataSource_DSCa = new List<cCaAbs>(dsCa);
				if (cbCa_Suaa.DataSource == null) {
					cbCa_Suaa.ValueMember = "ShiftID";
					cbCa_Suaa.DisplayMember = "ShiftCode";
					cbCa_Suaa.DataSource = dataSource_DSCa;
				}

				dtpGioMoi_Sua.Value = new DateTime(ngayCong.Ngay.Year, ngayCong.Ngay.Month, ngayCong.Ngay.Day, 0, 0, 1);
				cbLyDo_Suaa.SelectedIndex = 0;
				tbGhiChu_Suaa.Text = string.Empty;

				//tồn tại giờ cũ nên cho phép sửa
				btnSuaa.Enabled = true;
				btnChuyenDoi.Enabled = true;
				radVao.Enabled = radRaa.Enabled = true;
				dtpGioMoi_Sua.Enabled = true;
				cbLyDo_Suaa.Enabled = true;
				tbGhiChu_Suaa.Enabled = true;
			}
		}

		private void LoadGroup_Them(DateTime ngay) {

			var dataSource_DSCa = cbCa_Them.DataSource as List<cCaAbs>;
			if (dataSource_DSCa == null) dataSource_DSCa = new List<cCaAbs>(dsCa); // trường hợp này xảy ra khi loadgrid lần đầu, các lần sau dataSource ko còn bị null
			else dataSource_DSCa = dsCa;
			cbCa_Them.DataSource = dataSource_DSCa;

			cbCa_Them.SelectedIndex = 0;
			chkGioVao.Checked = false;
			chkGioRaa.Checked = false;
			dtpVao_Them.Value = new DateTime(ngay.Year, ngay.Month, ngay.Day, 0, 0, 1);
			dtpRaa_Them.Value = new DateTime(ngay.Year, ngay.Month, ngay.Day, 0, 0, 1);
			cbLyDo_Them.SelectedIndex = 0;
			tbGhichu_Them.Text = string.Empty;
			btnThem.Enabled = true;//vì thêm giờ ko cần xác định giờ cũ, chỉ insert data nên mặc định cho phép enable

		}


		private void cbCa_Them_SelectionChangeCommitted(object sender, EventArgs e) {
			//nếu dataSource null hoặc là index đầu tiên hoặc chưa có dòng nào được chọn thì ko làm gì cả
			if (cbCa_Them.DataSource == null || cbCa_Them.SelectedIndex == 0 || dgrdGioKDQD.SelectedRows.Count == 0) {
				return;
			}

			// tồn tại dataSource , xem item nào đang được chọn, nếu item đầu thì return như ở trên, các item khác thì fill
			var item = cbCa_Them.SelectedItem as cCaAbs;
			var row = ((DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem)).Row; // convert về datarow
			var ngayCong = (DateTime)row["TimeStrNgay"];
			// xác định được ca, ngày công, fill vào 2 datetimepicker
			dtpVao_Them.ValueChanged -= dtpVao_Them_ValueChanged;
			dtpRaa_Them.ValueChanged -= dtpRaa_Them_ValueChanged;
			dtpVao_Them.Value = ngayCong.Add(item.OnnDutyTS);
			dtpRaa_Them.Value = ngayCong.Add(item.OffDutyTS);
			dtpVao_Them.ValueChanged += dtpVao_Them_ValueChanged;
			dtpRaa_Them.ValueChanged += dtpRaa_Them_ValueChanged;

		}

		private void dtpVao_Them_ValueChanged(object sender, EventArgs e) {
			// nếu ko có dataSource hay chọn item đầu thì thoát
			if (cbCa_Them.DataSource == null || cbCa_Them.SelectedIndex == 0 || dgrdGioKDQD.SelectedRows.Count == 0) return;

			var row = ((DataRowView)dgrdGioKDQD.SelectedRows[0].DataBoundItem).Row;
			var ngayCong = (DateTime)row["TimeStrNgay"];
			var ca = cbCa_Them.SelectedItem as cCaAbs;
			var hieuvaoca = ngayCong.Add(ca.OnTimeInTS);
			var hieuraaca = ngayCong.Add(ca.CutInTS);
			var time = dtpVao_Them.Value;

			if (time < hieuvaoca || time > hieuraaca) {
				cbCa_Them.SelectionChangeCommitted -= cbCa_Them_SelectionChangeCommitted;
				cbCa_Them.SelectedIndex = 0;
				cbCa_Them.SelectionChangeCommitted += cbCa_Them_SelectionChangeCommitted;
			}

		}

		private void dtpRaa_Them_ValueChanged(object sender, EventArgs e) {
			// nếu ko có dataSource hay chọn item đầu thì thoát
			if (cbCa_Them.DataSource == null || cbCa_Them.SelectedIndex == 0 || dgrdGioKDQD.SelectedRows.Count == 0) return;

			var row = ((DataRowView)dgrdGioKDQD.SelectedRows[0].DataBoundItem).Row;
			var ngayCong = (DateTime)row["TimeStrNgay"];
			var ca = cbCa_Them.SelectedItem as cCaAbs;
			var hieuvaoca = ngayCong.Add(ca.OnTimeInTS);
			var hieuraaca = ngayCong.Add(ca.CutInTS);
			var time = dtpRaa_Them.Value;

			if (time < hieuvaoca || time > hieuraaca) {
				cbCa_Them.SelectionChangeCommitted -= cbCa_Them_SelectionChangeCommitted;
				cbCa_Them.SelectedIndex = 0;
				cbCa_Them.SelectionChangeCommitted += cbCa_Them_SelectionChangeCommitted;
			}

		}

		private void radVao_CheckedChanged(object sender, EventArgs e) {
			lbGioMoi.Text = "Giờ " + ((RadioButton)sender).Text + " mới";
			//lấy ngày công, ca
			if (cbCa_Suaa.DataSource == null || cbCa_Suaa.SelectedIndex == 0 || dgrdGioKDQD.SelectedRows.Count == 0) {
				return;
			}

			// tồn tại dataSource , xem item nào đang được chọn, nếu item đầu thì return như ở trên, các item khác thì fill
			var item = cbCa_Suaa.SelectedItem as cCaAbs;
			var row = ((DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem)).Row; // convert về datarow
			var ngayCong = (DateTime)row["TimeStrNgay"];

			if (radVao.Checked) {
				dtpGioMoi_Sua.ValueChanged -= dtpGioMoiSua_OnValueChanged;
				dtpGioMoi_Sua.Value = ngayCong.Add(item.OnnDutyTS);
				dtpGioMoi_Sua.ValueChanged += dtpGioMoiSua_OnValueChanged;
			}
			else {
				dtpGioMoi_Sua.ValueChanged -= dtpGioMoiSua_OnValueChanged;
				dtpGioMoi_Sua.Value = ngayCong.Add(item.OffDutyTS);
				dtpGioMoi_Sua.ValueChanged += dtpGioMoiSua_OnValueChanged;

			}
		}

		private void dtpGioMoiSua_OnValueChanged(object sender, EventArgs eventArgs) {
			//lấy ngày công, ca
			if (cbCa_Suaa.DataSource == null || cbCa_Suaa.SelectedIndex == 0 || dgrdGioKDQD.SelectedRows.Count == 0) {
				return;
			}

			// tồn tại dataSource , xem item nào đang được chọn, nếu item đầu thì return như ở trên, các item khác thì fill
			var item = cbCa_Suaa.SelectedItem as cCaAbs;
			var row = ((DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem)).Row; // convert về datarow
			var ngayCong = (DateTime)row["TimeStrNgay"];
			if (radVao.Checked) {
				if (dtpGioMoi_Sua.Value < ngayCong.Add(item.OnTimeInTS) || dtpGioMoi_Sua.Value > ngayCong.Add(item.CutInTS)) {
					cbCa_Suaa.SelectionChangeCommitted -= cbCaSuaa_OnSelectionChangeCommitted;
					cbCa_Suaa.SelectedIndex = 0;
					cbCa_Suaa.SelectionChangeCommitted += cbCaSuaa_OnSelectionChangeCommitted;
				}
			}
			else {
				if (dtpGioMoi_Sua.Value < ngayCong.Add(item.OnTimeOutTS) || dtpGioMoi_Sua.Value > ngayCong.Add(item.CutOutTS)) {
					cbCa_Suaa.SelectionChangeCommitted -= cbCaSuaa_OnSelectionChangeCommitted;
					cbCa_Suaa.SelectedIndex = 0;
					cbCa_Suaa.SelectionChangeCommitted += cbCaSuaa_OnSelectionChangeCommitted;
				}
			}
		}

		private void cbCaSuaa_OnSelectionChangeCommitted(object sender, EventArgs eventArgs) {
			//nếu dataSource null hoặc là index đầu tiên hoặc chưa có dòng nào được chọn thì ko làm gì cả
			if (cbCa_Suaa.DataSource == null || cbCa_Suaa.SelectedIndex == 0 || dgrdGioKDQD.SelectedRows.Count == 0) {
				return;
			}

			// tồn tại dataSource , xem item nào đang được chọn, nếu item đầu thì return như ở trên, các item khác thì fill
			var item = cbCa_Suaa.SelectedItem as cCaAbs;
			var row = ((DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem)).Row; // convert về datarow
			var ngayCong = (DateTime)row["TimeStrNgay"];
			// xác định được ca, ngày công, fill vào 2 datetimepicker

			if (radVao.Checked) {
				dtpGioMoi_Sua.ValueChanged -= dtpGioMoiSua_OnValueChanged;
				dtpGioMoi_Sua.Value = ngayCong.Add(item.OnnDutyTS);
				dtpGioMoi_Sua.ValueChanged += dtpGioMoiSua_OnValueChanged;
			}
			else {
				dtpGioMoi_Sua.ValueChanged -= dtpGioMoiSua_OnValueChanged;
				dtpGioMoi_Sua.Value = ngayCong.Add(item.OffDutyTS);
				dtpGioMoi_Sua.ValueChanged += dtpGioMoiSua_OnValueChanged;

			}
		}

		private void btnThem_Click(object sender, EventArgs e) {
			IsReload = true;
			// lấy thông tin
			var giovao = dtpVao_Them.Value.Add(ThamSo._01giay);
			var gioraa = dtpRaa_Them.Value;
			var lydo = cbLyDo_Them.SelectedText;
			var ghichu = tbGhichu_Them.Text;
			var UserEnrollNumber = nhanvien_goc.UserEnrollNumber;
			var ngayBD = nhanvien_goc.DSNgayCong[0].Ngay;//[tbd]
			var ngayKT = nhanvien_goc.DSNgayCong[nhanvien_goc.DSNgayCong.Count - 1].Ngay;//[tbd]


			if (chkGioVao.Checked) {
				var kq = false;
				var kq1 = 0;
				var gioAnhHuong = new List<cChkInOut>();
				var chuoi = string.Empty;

				XL.Kiemtra_GioRaaNhoHonGioVao(giovao, NgayCong_goc.DSVaoRa, ref kq1, ref gioAnhHuong, ref chuoi); //tbd NgayCong_goc hay lấy từ row đang chọn?
				if (kq1 == 1) {
					var giovaoTruoc1Ngay = giovao - ThamSo._1ngay;
					var tempstring = "Bạn muốn thêm giờ vào cho" + chuoi +
									 "?\nBấm Yes nếu đồng ý thêm giờ vào " + giovaoTruoc1Ngay.ToString("H:mm dddd d/M")
									 + ".\nBấm No nếu bạn muốn thêm giờ vào " + giovao.ToString("H:mm dddd d/M")
									 + ".\nBấm Cancel để hủy bỏ và xem lại thời gian nhập cho ngày này.";
					switch (MessageBox.Show(tempstring, "Thông báo", MessageBoxButtons.YesNoCancel)) {
						case DialogResult.Yes:
							giovao = giovaoTruoc1Ngay;
							break;
						case DialogResult.Cancel:
							return;
					}
				}
				else if (kq1 == 2) {
					var giovaoSau1Ngay = giovao + ThamSo._1ngay;
					var tempstring = "Bạn muốn thêm giờ vào cho" + chuoi +
									 "?\nBấm Yes nếu đồng ý thêm giờ vào " + giovaoSau1Ngay.ToString("H:mm dddd d/M")
									 + ".\nBấm No nếu bạn vẫn muốn thêm giờ vào " + giovao.ToString("H:mm dddd d/M")
									 + ".\nBấm Cancel để hủy bỏ và xem lại thời gian nhập cho ngày này.";
					switch (MessageBox.Show(tempstring, "Thông báo", MessageBoxButtons.YesNoCancel)) {
						case DialogResult.Yes:
							giovao = giovaoSau1Ngay;
							break;
						case DialogResult.Cancel:
							return;
					}
				}
				gioAnhHuong.Clear();
				chuoi = string.Empty;
				KiemTra_TruocKhiThemGio(giovao, true, nhanvien_goc.DSVaoRa, ref kq, ref gioAnhHuong, ref chuoi);
				if (kq) {
					MessageBox.Show("Không thể thêm giờ vào " + giovao.ToString("H:mm d/M") + ".\nVì giờ cần thêm gần hoặc giữa cặp giờ" + chuoi, "Thông báo");
					return;
				}

				
				if (DAL.ThemGioChoNV(UserEnrollNumber, giovao, true, 21, ThamSo.currUserID, lydo, ghichu)) {
					var checkinn = new cChkInn_A() { IsEdited = 1, Type = "I", MachineNo = 21, Source = "PC", UserEnrollNumber = UserEnrollNumber, TimeStr = giovao, PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false } };
					nhanvien_goc.DS_CheckInn_A.Add(checkinn);
					nhanvien_goc.DS_CheckInn_A.Sort(new cChkComparer());
					nhanvien_goc.DS_Check_A.Add(checkinn);
				}
			}

			if (chkGioRaa.Checked) {

				var kq = false;
				var gioAnhHuong = new List<cChkInOut>();
				var chuoi = string.Empty;
				KiemTra_TruocKhiThemGio(gioraa, false, nhanvien_goc.DSVaoRa, ref kq, ref gioAnhHuong, ref chuoi);
				if (kq) {
					MessageBox.Show("Không thể thêm giờ ra " + gioraa.ToString("H:mm d/M") + ".\nVì giờ cần thêm gần hoặc giữa cặp giờ" + chuoi, "Thông báo");
					return;
				}
				if (DAL.ThemGioChoNV(UserEnrollNumber, gioraa, false, 22, ThamSo.currUserID, lydo, ghichu)) {
					var checkout = new cChkOut_A() { IsEdited = 1, Type = "O", MachineNo = 22, Source = "PC", UserEnrollNumber = UserEnrollNumber, TimeStr = gioraa, PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false } };
					nhanvien_goc.DS_CheckOut_A.Add(checkout);
					nhanvien_goc.DS_CheckOut_A.Sort(new cChkComparer());
					nhanvien_goc.DS_Check_A.Add(checkout);
				}
			}

			nhanvien_goc.DS_Check_A.Sort(new cChkComparer());
			XL.LoaiBoCheckCungLoaiTrong30phut(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CheckInn_A, nhanvien_goc.DS_CheckOut_A, nhanvien_goc.DS_Check_KoHopLe);
			XL.GhepCIO_A(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CIO_A);
			XL.LoaiBoCIOKoHopLe(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CheckInn_A, nhanvien_goc.DS_CheckOut_A, nhanvien_goc.DS_Check_KoHopLe);
			XL.XetCa_CIO_A(nhanvien_goc.DS_CIO_A, nhanvien_goc.LichTrinhLV.DSCa, nhanvien_goc.MacDinhTinhPC150, nhanvien_goc.ds_check_ra3_vao1);
			XL.TronDS_CIO_A_V(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_CIO_V, nhanvien_goc.DSVaoRa);
			XL.TinhCongTheoNgay(nhanvien_goc.DSVaoRa, ngayBD, ngayKT, nhanvien_goc.DSVang, nhanvien_goc.DSNgayCong);
			NgayCong_goc = nhanvien_goc.DSNgayCong.Find(o => o.Ngay == NgayCong_goc.Ngay);

			loadGrid();

		}

		private void KiemTra_TruocKhiThemGio(DateTime gio, bool kieugio, List<cChkInOut> dsVaoRaa, ref bool kq, ref List<cChkInOut> gioAnhHuong, ref string chuoi) {
			if (kieugio) // kiểu giờ vào
			{
				var vao2 = gio;
				gioAnhHuong = (from CIO in dsVaoRaa
							   where ((CIO.HaveINOUT == -1 || CIO.HaveINOUT == 0) && (vao2 < CIO.Vao.TimeStr) && (CIO.Vao.TimeStr - vao2 < ThamSo._30phut))
								   || ((CIO.HaveINOUT == -1) && (CIO.Vao.TimeStr < vao2) && (vao2 - CIO.Vao.TimeStr < ThamSo._30phut))
								   || (CIO.HaveINOUT == 0 && CIO.Vao.TimeStr < vao2 && vao2 < CIO.Raa.TimeStr) // I2 nam giua IO
								   || ((CIO.HaveINOUT == -2) && (vao2 < CIO.Raa.TimeStr) && (CIO.Raa.TimeStr - vao2 < ThamSo._30phut)) // IO30
							   select CIO).ToList();
			}
			else {
				var raa2 = gio;
				gioAnhHuong = (from CIO in dsVaoRaa
							   where ((CIO.HaveINOUT == -2 || CIO.HaveINOUT == 0) && (CIO.Raa.TimeStr < raa2) && (raa2 - CIO.Raa.TimeStr < ThamSo._30phut))
								   || ((CIO.HaveINOUT == -2) && (raa2 < CIO.Raa.TimeStr) && (CIO.Raa.TimeStr - raa2 < ThamSo._30phut))
								   || (CIO.HaveINOUT == 0 && CIO.Vao.TimeStr < raa2 && raa2 < CIO.Raa.TimeStr) // O2 nam giua IO
								   || ((CIO.HaveINOUT == -1) && (CIO.Vao.TimeStr < raa2) && (raa2 - CIO.Vao.TimeStr < ThamSo._30phut)) // IO30
							   select CIO).ToList();
			}
			if (gioAnhHuong.Count > 0) {
				kq = true;
				var tempCIO = gioAnhHuong[0];
				chuoi += (tempCIO.Vao != null) ? " Vào " + tempCIO.Vao.TimeStr.ToString("H:mm d/M") : "";
				chuoi += (tempCIO.Raa != null) ? " Ra " + tempCIO.Raa.TimeStr.ToString("H:mm d/M") : "";
			}

		}

		private void btnXoaa_Click(object sender, EventArgs e) {
			IsReload = true;
			var row = ((DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem)).Row;
			var check = (cChk)row["cChk"];
			var lydo = cbLyDo_Xoaa.SelectedText;
			var ghichu = tbGhiChu_Xoaa.Text;
			var UserEnrollNumber = nhanvien_goc.UserEnrollNumber;
			var ngayBD = nhanvien_goc.DSNgayCong[0].Ngay;//[tbd]
			var ngayKT = nhanvien_goc.DSNgayCong[nhanvien_goc.DSNgayCong.Count - 1].Ngay;//[tbd]

			if (check is cChkInn_V || check is cChkOut_V) {
				MessageBox.Show("Giờ chấm công đã được xác nhận, không thể thay đổi.", "Thông báo");
				return;
			}

			if (DAL.XoaGioChoNV(UserEnrollNumber, check.TimeStr, check.Source, check.MachineNo, ThamSo.currUserID, lydo, ghichu)) {
				var indexCheck = nhanvien_goc.DS_Check_A.FindIndex(0, nhanvien_goc.DS_Check_A.Count, o => o.TimeStr == check.TimeStr && o.MachineNo == check.MachineNo && o.Source == check.Source);
				if (indexCheck < 0) return;
				var prevCheck = (indexCheck == 0) ? null : nhanvien_goc.DS_Check_A[indexCheck - 1];
				var nextCheck = (indexCheck == nhanvien_goc.DS_Check_A.Count - 1) ? null : nhanvien_goc.DS_Check_A[indexCheck + 1];
				if (prevCheck != null) prevCheck.IsEdited += 1;
				if (nextCheck != null) nextCheck.IsEdited += 1;
				nhanvien_goc.DS_Check_A.Remove(check);
				if (check is cChkInn_A) nhanvien_goc.DS_CheckInn_A.Remove(check);
				else nhanvien_goc.DS_CheckOut_A.Remove(check);
				XL.LoaiBoCheckCungLoaiTrong30phut(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CheckInn_A, nhanvien_goc.DS_CheckOut_A, nhanvien_goc.DS_Check_KoHopLe);
				XL.GhepCIO_A(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CIO_A);
				XL.LoaiBoCIOKoHopLe(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CheckInn_A, nhanvien_goc.DS_CheckOut_A, nhanvien_goc.DS_Check_KoHopLe);
				XL.XetCa_CIO_A(nhanvien_goc.DS_CIO_A, nhanvien_goc.LichTrinhLV.DSCa, nhanvien_goc.MacDinhTinhPC150, nhanvien_goc.ds_check_ra3_vao1);
				XL.TronDS_CIO_A_V(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_CIO_V, nhanvien_goc.DSVaoRa);
				XL.TinhCongTheoNgay(nhanvien_goc.DSVaoRa, ngayBD, ngayKT, nhanvien_goc.DSVang, nhanvien_goc.DSNgayCong);
				NgayCong_goc = nhanvien_goc.DSNgayCong.Find(o => o.Ngay == NgayCong_goc.Ngay);

			}
			// update lại table
			m_Bang_ChiTiet.Rows.Clear();
			loadGrid();

		}

		private void btnSuaa_Click(object sender, EventArgs e) {
			IsReload = true;
			var row = ((DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem)).Row;
			var check = (cChk)row["cChk"];
			var loaiMoi = radVao.Checked;
			var gioMoi = dtpGioMoi_Sua.Value;
			var lydo = cbLyDo_Suaa.SelectedText;
			var ghichu = tbGhiChu_Suaa.Text;
			var ngayBD = nhanvien_goc.DSNgayCong[0].Ngay;//[tbd]
			var ngayKT = nhanvien_goc.DSNgayCong[nhanvien_goc.DSNgayCong.Count - 1].Ngay;//[tbd]

			if (check is cChkInn_V || check is cChkOut_V) {
				MessageBox.Show("Giờ chấm công đã được xác nhận, không thể thay đổi.", "Thông báo");
				return;
			}
			// kiểm tra trước khi thêm giờ
			var kq = false;
			var gioAnhHuong = new List<cChkInOut>();
			var loai = string.Empty;
			KiemTra_TruocKhiSuaGio(check, gioMoi, loaiMoi, nhanvien_goc.DSVaoRa, ref kq, ref gioAnhHuong, ref loai);
			if (kq) {
				MessageBox.Show("Không thể sửa giờ" + ((check is cChkInn_A) ? " vào " : " ra ") + check.TimeStr.ToString("H:mm d/M") + "sang giờ" + ((loaiMoi) ? " vào " : " ra ") + gioMoi.ToString("H:mm d/M") + ".\nVì giờ mới gần hoặc nằm giữa cặp giờ" + loai, "Thông báo");
				return;
			}

			if (DAL.SuaGioChoNV(nhanvien_goc.UserEnrollNumber, check.TimeStr, gioMoi, loaiMoi, check.Source, check.MachineNo, check.PhucHoi.IDGioGoc, ThamSo.currUserID, lydo, ghichu)) {
				nhanvien_goc.DS_Check_A.Remove(check);
				if (check.GetType() == typeof(cChkInn_A)) nhanvien_goc.DS_CheckInn_A.Remove(check);
				else nhanvien_goc.DS_CheckOut_A.Remove(check);
				if (loaiMoi) {
					check = new cChkInn_A { Type = "I", UserEnrollNumber = nhanvien_goc.UserEnrollNumber, IsEdited = 1, TimeStr = gioMoi, Source = "PC", MachineNo = 21, PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = check.PhucHoi.Them, Xoaa = check.PhucHoi.Xoaa } }; // tbd trạng thái?? tạm thời lấy int.maxvalue tức sửa > 0
					nhanvien_goc.DS_CheckInn_A.Add(check);
					nhanvien_goc.DS_CheckInn_A.Sort(new cChkComparer());
				}
				else {
					check = new cChkOut_A { Type = "O", UserEnrollNumber = nhanvien_goc.UserEnrollNumber, IsEdited = 1, TimeStr = gioMoi, Source = "PC", MachineNo = 22, PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = check.PhucHoi.Them, Xoaa = check.PhucHoi.Xoaa } }; // tbd trạng thái?? tạm thời lấy int.maxvalue tức sửa > 0
					nhanvien_goc.DS_CheckOut_A.Add(check);
					nhanvien_goc.DS_CheckOut_A.Sort(new cChkComparer());
				}
				nhanvien_goc.DS_Check_A.Add(check);
				nhanvien_goc.DS_Check_A.Sort(new cChkComparer());

				XL.LoaiBoCheckCungLoaiTrong30phut(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CheckInn_A, nhanvien_goc.DS_CheckOut_A, nhanvien_goc.DS_Check_KoHopLe);
				XL.GhepCIO_A(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CIO_A);
				XL.LoaiBoCIOKoHopLe(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CheckInn_A, nhanvien_goc.DS_CheckOut_A, nhanvien_goc.DS_Check_KoHopLe);
				XL.XetCa_CIO_A(nhanvien_goc.DS_CIO_A, nhanvien_goc.LichTrinhLV.DSCa, nhanvien_goc.MacDinhTinhPC150, nhanvien_goc.ds_check_ra3_vao1);
				XL.TronDS_CIO_A_V(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_CIO_V, nhanvien_goc.DSVaoRa);
				XL.TinhCongTheoNgay(nhanvien_goc.DSVaoRa, ngayBD, ngayKT, nhanvien_goc.DSVang, nhanvien_goc.DSNgayCong);
				NgayCong_goc = nhanvien_goc.DSNgayCong.Find(o => o.Ngay == NgayCong_goc.Ngay);
			}

			m_Bang_ChiTiet.Rows.Clear();
			loadGrid();

		}

		private void KiemTra_TruocKhiSuaGio(cChk checkOld, DateTime gioMoi, bool loaiMoi, List<cChkInOut> dsVaoRaa, ref bool kq, ref List<cChkInOut> gioAnhHuong, ref string chuoi) {
			if (loaiMoi) {
				var vao2 = gioMoi;
				gioAnhHuong = (from CIO in dsVaoRaa
							   where ((CIO.Vao != null && CIO.Vao != checkOld) && (CIO.Raa != null && CIO.Raa != checkOld)) &&
							   (((CIO.HaveINOUT == -1 || CIO.HaveINOUT == 0) && (vao2 < CIO.Vao.TimeStr) && (CIO.Vao.TimeStr - vao2 < ThamSo._30phut))
								   || ((CIO.HaveINOUT == -1) && (CIO.Vao.TimeStr < vao2) && (vao2 - CIO.Vao.TimeStr < ThamSo._30phut))
								   || (CIO.HaveINOUT == 0 && CIO.Vao.TimeStr < vao2 && vao2 < CIO.Raa.TimeStr) // I2 nam giua IO
								   || ((CIO.HaveINOUT == -2) && (vao2 < CIO.Raa.TimeStr) && (CIO.Raa.TimeStr - vao2 < ThamSo._30phut))) // IO30
							   select CIO).ToList();
			}
			else {
				var raa2 = gioMoi;
				gioAnhHuong = (from CIO in dsVaoRaa
							   where ((CIO.Vao != null && CIO.Vao != checkOld) && (CIO.Raa != null && CIO.Raa != checkOld))
							   && (((CIO.HaveINOUT == -2 || CIO.HaveINOUT == 0) && (CIO.Raa.TimeStr < raa2) && (raa2 - CIO.Raa.TimeStr < ThamSo._30phut))
								   || ((CIO.HaveINOUT == -2) && (raa2 < CIO.Raa.TimeStr) && (CIO.Raa.TimeStr - raa2 < ThamSo._30phut))
								   || (CIO.HaveINOUT == 0 && CIO.Vao.TimeStr < raa2 && raa2 < CIO.Raa.TimeStr) // O2 nam giua IO
								   || ((CIO.HaveINOUT == -1) && (raa2 < CIO.Vao.TimeStr) && (CIO.Vao.TimeStr - raa2 < ThamSo._30phut))) // IO30
							   select CIO).ToList();

			}
			if (gioAnhHuong.Count > 0) {
				kq = true;
				var tempCIO = gioAnhHuong[0];
				chuoi += (tempCIO.Vao != null) ? " Vào " + tempCIO.Vao.TimeStr.ToString("H:mm d/M") : "";
				chuoi += (tempCIO.Raa != null) ? " Ra " + tempCIO.Raa.TimeStr.ToString("H:mm d/M") : "";
			}


		}

		private void btnChuyenDoi_Click(object sender, EventArgs e) {
			IsReload = true;
			var row = ((DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem)).Row;
			var check = (cChk)row["cChk"];
			var loaiCuu = (check.GetType() == typeof(cChkInn_A));
			var loaiMoi = !loaiCuu;
			var giocuu = check.TimeStr;
			var lydo = cbLyDo_Suaa.SelectedText;
			var ghichu = tbGhiChu_Suaa.Text;
			var ngayBD = nhanvien_goc.DSNgayCong[0].Ngay;//[tbd]
			var ngayKT = nhanvien_goc.DSNgayCong[nhanvien_goc.DSNgayCong.Count - 1].Ngay;//[tbd]

			if (check is cChkInn_V || check is cChkOut_V) {
				MessageBox.Show("Giờ chấm công đã được xác nhận, không thể thay đổi.", "Thông báo");
				return;
			}

			if (DAL.SuaGioChoNV(nhanvien_goc.UserEnrollNumber, giocuu, giocuu, loaiMoi, check.Source, check.MachineNo, check.PhucHoi.IDGioGoc, ThamSo.currUserID, lydo, ghichu)) {
				nhanvien_goc.DS_Check_A.Remove(check);
				if (check.GetType() == typeof(cChkInn_A)) nhanvien_goc.DS_CheckInn_A.Remove(check);
				else nhanvien_goc.DS_CheckOut_A.Remove(check);
				if (loaiMoi) {
					check = new cChkInn_A { Type = "I", UserEnrollNumber = nhanvien_goc.UserEnrollNumber, IsEdited = 1, TimeStr = giocuu, Source = "PC", MachineNo = 21, PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = check.PhucHoi.Them, Xoaa = check.PhucHoi.Xoaa } };// tbd trạng thái?? tạm thời lấy int.maxvalue tức sửa > 0
					nhanvien_goc.DS_CheckInn_A.Add(check);
					nhanvien_goc.DS_CheckInn_A.Sort(new cChkComparer());
				}
				else {
					check = new cChkOut_A { Type = "O", UserEnrollNumber = nhanvien_goc.UserEnrollNumber, IsEdited = 1, TimeStr = giocuu, Source = "PC", MachineNo = 22, PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = check.PhucHoi.Them, Xoaa = check.PhucHoi.Xoaa } };// tbd trạng thái?? tạm thời lấy int.maxvalue tức sửa > 0
					nhanvien_goc.DS_CheckOut_A.Add(check);
					nhanvien_goc.DS_CheckOut_A.Sort(new cChkComparer());
				}
				nhanvien_goc.DS_Check_A.Add(check);
				nhanvien_goc.DS_Check_A.Sort(new cChkComparer());

				XL.LoaiBoCheckCungLoaiTrong30phut(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CheckInn_A, nhanvien_goc.DS_CheckOut_A, nhanvien_goc.DS_Check_KoHopLe);
				XL.GhepCIO_A(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CIO_A);
				XL.LoaiBoCIOKoHopLe(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CheckInn_A, nhanvien_goc.DS_CheckOut_A, nhanvien_goc.DS_Check_KoHopLe);
				XL.XetCa_CIO_A(nhanvien_goc.DS_CIO_A, nhanvien_goc.LichTrinhLV.DSCa, nhanvien_goc.MacDinhTinhPC150, nhanvien_goc.ds_check_ra3_vao1);
				XL.TronDS_CIO_A_V(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_CIO_V, nhanvien_goc.DSVaoRa);
				XL.TinhCongTheoNgay(nhanvien_goc.DSVaoRa, ngayBD, ngayKT, nhanvien_goc.DSVang, nhanvien_goc.DSNgayCong);
				NgayCong_goc = nhanvien_goc.DSNgayCong.Find(o => o.Ngay == NgayCong_goc.Ngay);
			}
			m_Bang_ChiTiet.Rows.Clear();
			loadGrid();


		}


		#region thực hiện sau


		#endregion 
*/
	}
}
