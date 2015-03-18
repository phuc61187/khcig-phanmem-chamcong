using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ChamCong_v02.BUS;
using ChamCong_v02.DAO;
using ChamCong_v02.DTO;
using log4net;

namespace ChamCong_v02 {
	public partial class v03_frm_SuaGio : Form {
		private readonly ILog lg = LogManager.GetLogger("v03_frm_SuaGio");

		public DataRow[] DSNgayCongChked { get; set; }
		public bool IsReload { get; set; }
		readonly CheckBox checkAllGridTH = new CheckBox();
		void checkAll_CheckedChanged(object sender, EventArgs e) {
			//1. xác định datagrid nào đang check all
			DataGridView tempGrid;
			tempGrid = dgrdGioKDQD;

			// 2. kiểm tra nếu ko có dòng dữ liệu nào thì thoát
			if (tempGrid.Rows.Count == 0) return;

			//3. có dữ liệu, duyệt qua hết và gán giá trị check (or uncheck), sau đó endEDIT lại lưới để dòng đầu tiên có giá trị
			bool tmpCheckAll = ((CheckBox)sender).Checked;

			tempGrid.BeginEdit(true);
			DataTable dt = tempGrid.DataSource as DataTable;

			if (dt != null && dt.Rows.Count != 0) {
				foreach (DataRow row in dt.Rows) {
					row["check"] = tmpCheckAll;
				}
			}

			tempGrid.EndEdit();
			tempGrid.Update();
			//tempGrid.Refresh();
		}

		public v03_frm_SuaGio() {
			InitializeComponent();
			dgrdGioKDQD.AutoGenerateColumns = false;
			log4net.Config.XmlConfigurator.Configure();
			ThamSo.VeCheckBox_CheckAll(dgrdGioKDQD, checkAllGridTH, checkAll_CheckedChanged, new Point(7, 3));

		}

		private void frm_SuaGio_Load(object sender, EventArgs e) {
			// mặc định ko reload lại xem công nếu ko thực hiện thao tác
			IsReload = false;

			//1 load 2 combobox danh sach ca
			List<cShift> dataCa_Vao = new List<cShift>(ThamSo.DSCa);
			dataCa_Vao.Insert(0, new cShift { ShiftID = 0, ShiftCode = "Tuỳ chỉnh", LoaiCa = 1 });
			cbCa_Vao.DisplayMember = "ShiftCode";
			cbCa_Vao.DataSource = dataCa_Vao;
			// gio đe mac dinh 0:00
			dtpGioMoi.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);

			// tạo cấu trúc table load datagrid
			DataTable tableChiTiet = XL.TaoCauTrucDataTable(
				new string[] { "check", "UserEnrollNumber", "UserFullCode", "UserFullName", "TimeStrNgay", "Kieu", "TimeStr", "MachineNo", "Source", "objCIO", "objCheck" }
				, new Type[] { typeof(bool), typeof(int), typeof(string), typeof(string), typeof(DateTime), typeof(string), typeof(DateTime), typeof(int), typeof(string), typeof(cChkInOut), typeof(cChk) });
			//load datagrid
			// duyệt qua từng ngày công đã chọn
			foreach (DataRow row in DSNgayCongChked) {
				bool check = false;
				int iUserEnrollNumber = (int)row["UserEnrollNumber"];
				string manv = row["UserFullCode"].ToString();
				string name = row["UserFullName"].ToString();
				DateTime ngay = (DateTime)row["TimeStrNgay"];
				cNgayCong ngayCong = (cNgayCong)row["obj"];
				// duyệt qua các giờ vào-ra, bỏ qua các CIO_V
				if (ngayCong.DSVaoRa != null && ngayCong.DSVaoRa.Count > 0) {
					foreach (cChkInOut CIO in ngayCong.DSVaoRa) {
						if (CIO is cChkInOut_V) continue; //bỏ qua các CIO_V
						if (CIO.Vao != null) {
							#region gán giá trị cho các row
							DataRow row1 = tableChiTiet.NewRow();
							row1["check"] = false;
							row1["UserEnrollNumber"] = iUserEnrollNumber;
							row1["UserFullCode"] = manv;
							row1["UserFullName"] = name;
							row1["TimeStrNgay"] = ngay;
							row1["Kieu"] = (CIO.Vao.MachineNo % 2 == 1) ? "Vào" : "Ra";
							row1["TimeStr"] = CIO.Vao.TimeStr;
							row1["MachineNo"] = CIO.Vao.MachineNo;
							row1["Source"] = CIO.Vao.Source;
							row1["objCIO"] = CIO;
							row1["objCheck"] = CIO.Vao;
							tableChiTiet.Rows.Add(row1);
							#endregion
						}
						if (CIO.Raa != null) {
							#region gán giá trị cho các row
							DataRow row2 = tableChiTiet.NewRow();
							row2["check"] = false;
							row2["UserEnrollNumber"] = iUserEnrollNumber;
							row2["UserFullCode"] = manv;
							row2["UserFullName"] = name;
							row2["TimeStrNgay"] = ngay;
							row2["Kieu"] = (CIO.Raa.MachineNo % 2 == 1) ? "Vào" : "Ra";
							row2["TimeStr"] = CIO.Raa.TimeStr;
							row2["MachineNo"] = CIO.Raa.MachineNo;
							row2["Source"] = CIO.Raa.Source;
							row2["objCIO"] = CIO;
							row2["objCheck"] = CIO.Raa;
							tableChiTiet.Rows.Add(row2);
							#endregion
						}
					}
				}
			}
			dgrdGioKDQD.DataSource = tableChiTiet;

			// dang ky su kien check vao ra, combo ca , datetime gio
			cbCa_Vao.SelectionChangeCommitted += cbCa_SelectionChangeCommitted;
			dtpGioMoi.ValueChanged += DtpGioMoiValueChanged;

		}

		void DtpGioMoiValueChanged(object sender, EventArgs e) {
			if (cbCa_Vao.SelectedIndex == 0) return; // neu chon tuỳ chinh thi thoat ko lam gi het

			cShift ca = cbCa_Vao.SelectedItem as cShift; // lay ca duoc chon và kiem tra xem gio vao có nằm trong khoảng hiểu ca ko.
			// neu ko hieu ca chuyen combo ve tùy chỉnh

			if (ca == null) return;

			bool kieugio = radVao.Checked;
			DateTime x = dtpGioMoi.Value;
			if (kieugio) {// nếu chọn kiểu vào thì so sánh với ontimein, cutin
				if (x.TimeOfDay >= ca.OnTimeInTS.Subtract(new TimeSpan(ca.DayCount, 0, 0, 0))
				  && x.TimeOfDay <= ca.CutInTS.Subtract(new TimeSpan(ca.DayCount, 0, 0, 0))) {
					// nằm trong khoảng hiểu thì ko làm gì hết
				}
				else {// nằm ngoài khoảng hiểu, chuyển về tùy chọn tùy chỉnh
					cbCa_Vao.SelectedIndex = 0;
				}
			}
			else { // nếu chọn kiểu ra thì so sánh với ontimeout, cutout
				if (x.TimeOfDay >= ca.OnTimeOutTS.Subtract(new TimeSpan(ca.DayCount, 0, 0, 0))
					&& x.TimeOfDay <= ca.CutOutTS.Subtract(new TimeSpan(ca.DayCount, 0, 0, 0))) {

				}
				else {
					cbCa_Vao.SelectedIndex = 0;
				}
			}
		}


		private void cbCa_SelectionChangeCommitted(object sender, EventArgs e) {
			if (cbCa_Vao.SelectedIndex == 0) return; // neu chon tuỳ chinh thi thoat ko lam gi het

			cShift ca = cbCa_Vao.SelectedItem as cShift; // lay ca chon
			bool kieugio = radVao.Checked;

			if (ca == null) return;
			// gán giờ theo ca, huỷ rồi bật lại sự kiện datetimepicker change
			dtpGioMoi.ValueChanged -= DtpGioMoiValueChanged;
			DateTime x = DateTime.MinValue;
			x = (kieugio) ? DateTime.Today.Add(ca.OnnDutyTS) : DateTime.Today.Add(ca.OffDutyTS.Subtract(new TimeSpan(ca.DayCount, 0, 0, 0)));  //

			dtpGioMoi.Value = x;
			dtpGioMoi.Update();
			dtpGioMoi.ValueChanged += DtpGioMoiValueChanged;

		}

		#region enable/disable datetimepicker ngày
		private void checkNgayVao_CheckedChanged(object sender, EventArgs e) {
			dtpNgay_Vao.Enabled = checkNgay_Vao.Checked;
		}

		#endregion

		private void btnSuaGio_Click(object sender, EventArgs e) {
			// lấy thông tin 1. ca, 2. gio vao, 3. ngay co dinh if check, ly do, ghi chu
			TimeSpan giomoi = dtpGioMoi.Value.TimeOfDay;
			string lydo = (cbLyDo.SelectedItem != null) ? cbLyDo.SelectedItem.ToString() : cbLyDo.Text;
			string ghichu = tbGhiChu.Text;

			// nếu thực hiện thì bật yêu cầu reload lại form xem công
			IsReload = true;

			// biến kq thực hiện thành công hay thất bại
			bool flag = true;
			int iUserEnrollNumber = -1;
			string name = string.Empty;
			DateTime ngay = DateTime.MinValue;

			//update datagrid 
			this.BindingContext[dgrdGioKDQD.DataSource].EndCurrentEdit();

			DataTable table = dgrdGioKDQD.DataSource as DataTable; // lấy datatable của datagrid để lọc ra các dòng check = true
			if (table == null) return;
			DataRow[] arrRows = table.Select("check = true"); // lấy danh sách record check, kiểm tra nếu = 0 thì thoát
			if (arrRows.Length == 0) return;

			DateTime TimeStr = DateTime.MinValue;
			// duyệt qua từng ngày để sửa giờ, trong quá trình sửa nếu xảy ra lỗi thì thoát ra.
			for (int i = 0; i < arrRows.Length; i++) {
				DataRow row = arrRows[i];
				cChk check = row["objCheck"] as cChk;
				if (check == null) continue;

				iUserEnrollNumber = (int)row["UserEnrollNumber"];
				ngay = (checkNgay_Vao.Checked) ? dtpNgay_Vao.Value.Date : (DateTime)row["TimeStrNgay"];
				ngay = ngay.Add(giomoi);
				TimeStr = check.TimeStr;
				bool kq = DAL.SuaGioChoNV(iUserEnrollNumber, check.TimeStr, ngay, (check.MachineNo % 2 == 1) ? true : false, check.Source, check.MachineNo, ThamSo.currUserID, lydo, ghichu);

				if (kq == false) {
					name = row["UserFullName"].ToString();
					flag = false;
					break;
				}
			}
			// nếu xảy ra lỗi thì báo, ko thì thông báo thành công
			if (flag == false) {
				MessageBox.Show("Xảy ra lỗi trong quá trình sửa giờ kể từ Nhân viên: " + name + " vào giờ " + TimeStr.ToString(), "Lỗi");
			}
			else {
				AutoClosingMessageBox.Show("Thực hiện thành công.", "Thông báo", 2000);
				this.Close();
			}
		}


		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void rad_CheckedChanged(object sender, EventArgs e) {
			if (cbCa_Vao.SelectedIndex == 0) return; // neu chon tuỳ chinh thi thoat ko lam gi het

			cShift ca = cbCa_Vao.SelectedItem as cShift; // lay ca duoc chon và kiem tra xem gio vao có nằm trong khoảng hiểu ca ko.
			// neu ko hieu ca chuyen combo ve tùy chỉnh

			if (ca == null) return;

			bool kieugio = radVao.Checked;

			//kiểm tra coi thuộc ca nào, kiểu gì để thay đổi combox
			dtpGioMoi.ValueChanged -= DtpGioMoiValueChanged;
			DateTime x = DateTime.MinValue;
			x = (kieugio) ? DateTime.Today.Add(ca.OnnDutyTS) : DateTime.Today.Add(ca.OffDutyTS.Subtract(new TimeSpan(ca.DayCount, 0, 0, 0)));  //

			dtpGioMoi.Value = x;
			dtpGioMoi.Update();

			dtpGioMoi.ValueChanged += DtpGioMoiValueChanged;

		}

		private void btnDaoNguoc_Click(object sender, EventArgs e) {
			// lấy thông tin 1. ca, 2. gio vao, 3. ngay co dinh if check, ly do, ghi chu
			string lydo = (cbLyDo.SelectedItem != null) ? cbLyDo.SelectedItem.ToString() : cbLyDo.Text;
			string ghichu = tbGhiChu.Text;

			// nếu thực hiện thì bật yêu cầu reload lại form xem công
			IsReload = true;

			// biến kq thực hiện thành công hay thất bại
			bool flag = true;
			int iUserEnrollNumber = -1;
			string name = string.Empty;
			DateTime ngay = DateTime.MinValue;

			//update datagrid 
			this.BindingContext[dgrdGioKDQD.DataSource].EndCurrentEdit();

			DataTable table = dgrdGioKDQD.DataSource as DataTable; // lấy datatable của datagrid để lọc ra các dòng check = true
			if (table == null) return;
			DataRow[] arrRows = table.Select("check = true"); // lấy danh sách record check, kiểm tra nếu = 0 thì thoát
			if (arrRows.Length == 0) return;

			DateTime TimeStr = DateTime.MinValue;
			// duyệt qua từng ngày để sửa giờ, trong quá trình sửa nếu xảy ra lỗi thì thoát ra.
			for (int i = 0; i < arrRows.Length; i++) {
				DataRow row = arrRows[i];
				cChk check = row["objCheck"] as cChk;
				if (check == null) continue;

				iUserEnrollNumber = (int)row["UserEnrollNumber"];

				TimeStr = check.TimeStr;
				// do đảo ngược giờ nên (check.MachineNo % 2 == 1): kiểu giờ mới bị ngược, vào thành ra, ra thành vào
				bool kq = DAL.SuaGioChoNV(iUserEnrollNumber, check.TimeStr, check.TimeStr, (check.MachineNo % 2 == 1) ? false : true, check.Source, check.MachineNo, ThamSo.currUserID, lydo, ghichu);

				if (kq == false) {
					name = row["UserFullName"].ToString();
					flag = false;
					break;
				}
			}
			// nếu xảy ra lỗi thì báo, ko thì thông báo thành công
			if (flag == false) {
				MessageBox.Show("Xảy ra lỗi trong quá trình đảo ngược giờ kể từ Nhân viên: " + name + " vào giờ " + TimeStr.ToString(), "Lỗi");
			}
			else {
				AutoClosingMessageBox.Show("Thực hiện thành công.", "Thông báo", 2000);
				this.Close();
			}
		}
	}
}
