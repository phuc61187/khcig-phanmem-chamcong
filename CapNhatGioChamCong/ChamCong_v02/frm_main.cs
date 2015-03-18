using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ChamCong_v02.DAO;
using ChamCong_v02.DTO;
using log4net;

namespace ChamCong_v02 {
	public partial class frm_main : Form {
		public readonly ILog lg = LogManager.GetLogger("frm_main");

		public frm_main() {
			InitializeComponent();

			//config log
			log4net.Config.XmlConfigurator.Configure();

		}


		private void frm_main_Load(object sender, EventArgs e) {

			frmDangNhap frmDangnhap = new frmDangNhap();
			frmDangnhap.ShowDialog();
			if (frmDangnhap.LoggedInResult == 0) {// login ko thành công, click thoát
				this.Close();
				Application.Exit();
			}
			else if (frmDangnhap.LoggedInResult == 1) { // login thành công bằng tài khoản thường
				ChuanBiDuLieu();
				PhanQuyenMenu(1);
			}
			else if (frmDangnhap.LoggedInResult == int.MaxValue) { // login thành công bằng tài khoản root
				PhanQuyenMenu(int.MaxValue);
				frm_01_Admin frm01 = new frm_01_Admin();
				frm01.ShowDialog();
			}
		}

		#region chuẩn bị dữ liệu : DS tất cả các Ca làm việc, tất cả các Lịch trình
		private void ChuanBiDuLieu() {
			ChuanBiDSCaLamViec();
			ChuanBiDSLichTrinh();
		}

		//[v2] ok giữ
		private void ChuanBiDSCaLamViec() {
			if (ThamSo.DSCa == null) ThamSo.DSCa = new List<cShift>();
			else ThamSo.DSCa.Clear();

			//ko cần try catch ở đây
			DataTable dt = DAL.LayDSCa();
			if (dt.Rows.Count == 0) return;
			foreach (DataRow row in dt.Rows) {
				int iShiftID = (int)row["ShiftID"];
				string sShiftCode = row["ShiftCode"].ToString();

				TimeSpan tsOnDuty;
				TimeSpan.TryParse(row["Onduty"].ToString(), out tsOnDuty);
				TimeSpan tOnTimeIn = tsOnDuty.Subtract(new TimeSpan(0, (int)row["OnTimeIn"], 0));
				TimeSpan tCutIn = tsOnDuty.Add(new TimeSpan(0, (int)row["CutIn"], 0));

				int iDayCount = (int)row["DayCount"];
				int iShowPosition = (int)row["ShowPosition"];
				TimeSpan tOffDuty;
				TimeSpan.TryParse(row["Offduty"].ToString(), out tOffDuty);
				// phải add thêm 1 ngày daycount vì trong dữ liệu chỉ có chuỗi giờ thô : 05:45 không có ngày
				tOffDuty = tOffDuty.Add(new TimeSpan(iDayCount, 0, 0, 0));
				TimeSpan tOnTimeOut = tOffDuty.Subtract(new TimeSpan(0, (int)row["OnTimeOut"], 0));
				TimeSpan tCutOut = tOffDuty.Add(new TimeSpan(0, (int)row["CutOut"], 0));

				TimeSpan tAfterOT = new TimeSpan(0, (int)row["AfterOT"], 0);
				TimeSpan tLateGrace = new TimeSpan(0, (int)row["LateGrace"], 0);
				TimeSpan tEarlyGrace = new TimeSpan(0, (int)row["EarlyGrace"], 0);

				TimeSpan tOnLunch = ThamSo._0gio, tOffLunch = ThamSo._0gio;
				if (row["OnLunch"] != DBNull.Value && row["OffLunch"] != DBNull.Value) {
					TimeSpan.TryParse(row["OnLunch"].ToString(), out tOnLunch);
					TimeSpan.TryParse(row["OffLunch"].ToString(), out tOffLunch);
				}

				int tempWorkingTime = int.Parse(row["WorkingTime"].ToString());
				cShift tempShift = new cShift() {
					LoaiCa = 0,
					ShiftID = iShiftID, ShiftCode = sShiftCode,
					DayCount = iDayCount, QuaDem = (iDayCount == 1),
					OnnDutyTS = tsOnDuty, OffDutyTS = tOffDuty,
					OnTimeInTS = tOnTimeIn, CutInTS = tCutIn, OnTimeOutTS = tOnTimeOut, CutOutTS = tCutOut,
					AfterOTTS = tAfterOT,
					LateGraceTS = tLateGrace, EarlyGraceTS = tEarlyGrace,
					Workingday = (Single)row["Workingday"],
					ShowPosition = iShowPosition,
					WorkingTimeTS = new TimeSpan(0, tempWorkingTime, 0),
					chophepvaotreTS = tsOnDuty.Add(tLateGrace),
					chopheprasomTS = tOffDuty - tEarlyGrace,
                    batdaulamthemTS = tOffDuty + tAfterOT,//[TBD] thay tOffDuty + ThamSo._30phut thành tempOffDuty + tempAfterOT --> đã thay
					LunchMinute = tOffLunch.Subtract(tOnLunch)
				};
				ThamSo.DSCa.Add(tempShift);
			}

		}

		//[v2] ok giữ
		private void ChuanBiDSLichTrinh() {
			if (ThamSo.DSLichTrinh == null) ThamSo.DSLichTrinh = new List<cShiftSchedule>();
			else ThamSo.DSLichTrinh.Clear();
			List<cShift> tmpDSCa = ThamSo.DSCa;

			DataTable table = DAL.LayDSLichTrinh();
			int tempScheduleID = -1;
			foreach (DataRow dataRow in table.Rows) {
				if (tempScheduleID == -1)
					tempScheduleID = (int)dataRow["SchID"];
				else {
					if (tempScheduleID == (int)dataRow["SchID"]) continue;
					else tempScheduleID = (int)dataRow["SchID"];
				}

				//= (int)dataRow["SchID"]; // tempT1, tempT2, tempT3, tempT4, tempT5, tempT6, tempT7;
				DataRow[] arrSubRecord = table.Select("SchID = " + tempScheduleID, string.Empty, DataViewRowState.CurrentRows);
				cShiftSchedule tmpLichTrinh = new cShiftSchedule { SchID = tempScheduleID };
				tmpLichTrinh.ListT1 = new List<cShift>();
				tmpLichTrinh.ListT2 = new List<cShift>();
				tmpLichTrinh.ListT3 = new List<cShift>();
				tmpLichTrinh.ListT4 = new List<cShift>();
				tmpLichTrinh.ListT5 = new List<cShift>();
				tmpLichTrinh.ListT6 = new List<cShift>();
				tmpLichTrinh.ListT7 = new List<cShift>();
				foreach (DataRow subRecord in arrSubRecord) {
					int tempTChungCho1Hang = (int)subRecord["T1"]; // tempT1, tempT2, tempT3, tempT4, tempT5, tempT6, tempT7;
					cShift tempShift = tmpDSCa.Find(item => item.ShiftID == tempTChungCho1Hang);

					tmpLichTrinh.ListT1.Add(tempShift);
					tmpLichTrinh.ListT2.Add(tempShift);
					tmpLichTrinh.ListT3.Add(tempShift);
					tmpLichTrinh.ListT4.Add(tempShift);
					tmpLichTrinh.ListT5.Add(tempShift);
					tmpLichTrinh.ListT6.Add(tempShift);
					tmpLichTrinh.ListT7.Add(tempShift);

				}
				ThamSo.DSLichTrinh.Add(tmpLichTrinh);
			}
		}



		#endregion


		//[v2] lam lai
		private void PhanQuyenMenu(int pAccountType) {

			// account thường
			//[TBD] [2703_1]chưa làm: ko cho phép đổi mật khẩu nếu là tài khoản admin -->ok
			if (pAccountType == int.MaxValue) {
				MenuTaiKhoan.Enabled = false;
				SubMenu_DoiMK.Enabled = false;
			}
			else {
				MenuTaiKhoan.Enabled = true;
				SubMenu_DoiMK.Enabled = true;// nếu login thành công thì cho phép đổi mật khẩu của mình
				DataTable dt = DAL.PhanQuyenMenu(ThamSo.currUserID);
				for (int i = 0; i < dt.Rows.Count; i++) {
					bool enable = (bool)dt.Rows[i]["IsYes"];
					switch ((int)dt.Rows[i]["MenuID"]) {
						#region Menu chấm công --> 1.xem công  2. điểm danh
						case 10001:// xem công
							if (enable) MenuChamCong.Enabled = enable;
							SubMenu_XemCongNV.Enabled = enable;
							break;
						case 10002: // điểm danh
							if (enable) MenuChamCong.Enabled = enable;
							SubMenu_DiemDanh.Enabled = enable;
							break;
						#endregion
						#region menu khai báo 1. vắng 2.làm việc ngày nghỉ 3.chấm công tay
						case 20001: // khai bao vang
							if (enable) MenuKhaiBao.Enabled = enable;
							SubMenu_KhaibaoVang.Enabled = enable;
							break;
						case 20002: // khai bao lam viec ngay nghi
							if (enable) MenuKhaiBao.Enabled = enable;
							SubMenu_KhaibaoLVNN.Enabled = enable;
							break;
						case 20003: // cham cong tay
							if (enable) MenuKhaiBao.Enabled = enable;
							SubMenu_ChamcongTay.Enabled = enable;
							break;

						#endregion
						#region menu hoat động khác 1. sua hang loat 2. xem lich su 3. duyệt các ngày làm việc tính PC 100% lương
						case 30001: // sua gio hang loat
							if (enable) MenuHoatDong.Enabled = enable;
							SubMenu_SuaGioHangLoat.Enabled = enable;
							break;
						case 30002: // xem lich su
							if (enable) MenuHoatDong.Enabled = enable;
							SubMenu_XemHistory.Enabled = enable;
							break;
						case 30003:
							if (enable) MenuHoatDong.Enabled = enable;
							SubMenu_XacNhanNgayLVTinhPC100.Enabled = enable;
							break;
						#endregion
						#region menu tính lương 1. tính lương 2. điều chỉnh lương tháng trước 3. thay đổi hệ số lương
						case 40001: // tinh luong
							if (enable) MenuTinhLuong.Enabled = enable;
							SubMenu_TinhLuong.Enabled = enable;
							break;
						case 40002: // dieu chinh luong thang truoc
							if (enable) MenuTinhLuong.Enabled = enable;
							SubMenu_DieuChinhLuong.Enabled = enable;
							break;
						case 40003: // thay doi he so luong
							if (enable) MenuTinhLuong.Enabled = enable;
							SubMenu_ThayDoiHSL.Enabled = enable;
							break;
							
						#endregion
						#region menu tài khoản 
						case 50002: //phân quyền
							if (enable) MenuTaiKhoan.Enabled = enable;
							SubMenu_PhanQuyen.Enabled = enable;
							break;
						case 50003: //tạo tài khoản đăng nhập
							if (enable) MenuTaiKhoan.Enabled = enable;
							SubMenu_TaoTK.Enabled = enable;
							break;
						#endregion
						#region menu setting
						case 60001: // tinh luong --> được tính lương thì được điều chỉnh, chỉ áp dụng cho phòng tổ chức
							if (enable) MenuSetting.Enabled = enable;
							break;
						#endregion
						default:
							break;
					}
				}
			}


		}

		int LayVitriForm(Form mainForm, Type childType) {
			if (mainForm.MdiChildren.Length == 0) return -1;
			for (int i = 0; i < mainForm.MdiChildren.Length; i++) {
				if (mainForm.MdiChildren[i].GetType() == childType)
					return i;
			}
			return -1;
		}

		private void SubMenu_XemCongNV_Click(object sender, EventArgs e) {
			frm_11_XemCong frm1 = new frm_11_XemCong();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_11_XemCong;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Maximized;
				frm1.Show();
			}
		}

		private void SubMenu_DiemDanh_Click(object sender, EventArgs e) {
			frm_12_DiemDanhNV frm1 = new frm_12_DiemDanhNV();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_12_DiemDanhNV;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Maximized;
				frm1.Show();
			}
		}

		private void SubMenu_KhaiBaoVang_Click(object sender, EventArgs e) {
			frm_21_KhaiBaoVang frm1 = new frm_21_KhaiBaoVang();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_21_KhaiBaoVang;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}
		}

		private void SubMenu_KhaiBaoLVNN_Click(object sender, EventArgs e) {
			frm_22_KhaiBaoLamViecNgayNghi frm1 = new frm_22_KhaiBaoLamViecNgayNghi();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_22_KhaiBaoLamViecNgayNghi;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}
		}

		private void SubMenu_ChamCongTay_Click(object sender, EventArgs e) {
			frm_23_ChamCongTay frm1 = new frm_23_ChamCongTay();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_23_ChamCongTay;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}
		}

		private void SubMenu_SuaGioHangLoat_Click(object sender, EventArgs e) {
			frm_31_SuaGioHangLoat frm1 = new frm_31_SuaGioHangLoat();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_31_SuaGioHangLoat;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Maximized;
				frm1.Show();
			}
		}

		private void SubMenu_xemHistory_Click(object sender, EventArgs e) {
			frm_32_XemLichSu frm1 = new frm_32_XemLichSu();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_32_XemLichSu;
				frm1.Close();
				frm1 = new frm_32_XemLichSu();
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Maximized;
				frm1.Show();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Maximized;
				frm1.Show();
			}
		}

		private void SubMenu_XacNhanNgayLVTinhPC100_Click(object sender, EventArgs e) {
			frm_XacNhanNgayLVTinhPC100 frm1 = new frm_XacNhanNgayLVTinhPC100();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_XacNhanNgayLVTinhPC100;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}
		}

		private void SubMenu_TinhLuong_Click(object sender, EventArgs e) {
			frm_41_TinhLuongNV frm1 = new frm_41_TinhLuongNV();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_41_TinhLuongNV;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}
		}

		private void SubMenu_DieuChinhLuong_Click(object sender, EventArgs e) {
			frm_42_DieuChinhLuongThangTruoc frm1 = new frm_42_DieuChinhLuongThangTruoc();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_42_DieuChinhLuongThangTruoc;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
                frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}
		}

		private void SubMenu_ThayDoiHSL_Click(object sender, EventArgs e) {
			frm_43_ThayDoiHeSoLuong_PC frm1 = new frm_43_ThayDoiHeSoLuong_PC();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_43_ThayDoiHeSoLuong_PC;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}
		}

		private void SubMenu_DoiMK_Click(object sender, EventArgs e) {
			frm_51_DoiMatKhau frm1 = new frm_51_DoiMatKhau();
			frm1.ShowDialog();
		}

		private void MenuSetting_Click(object sender, EventArgs e) {
			frm_61_Setting frm1 = new frm_61_Setting();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_61_Setting;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}
		}

		private void SubMenu_PhanQuyen_Click(object sender, EventArgs e) {
			frm_52_PhanQuyen frm1 = new frm_52_PhanQuyen();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_52_PhanQuyen;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}
		}

		private void MenuThoat_Click(object sender, EventArgs e) {
			if (ActiveMdiChild == null) {
				this.Close();
			}
			while (ActiveMdiChild != null) {
				ActiveMdiChild.Close();
			}
		}

		private void SubMenu_TaoTK_Click(object sender, EventArgs e) {
			frm_02_TaoTaiKhoan frm1 = new frm_02_TaoTaiKhoan();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_02_TaoTaiKhoan;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}
		}


	}
}
