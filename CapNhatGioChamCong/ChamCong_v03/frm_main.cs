using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;
using System.Linq;
using System.Linq.Expressions;
using ChamCong_v03.LuongCongNhat;
using log4net;
using System.Drawing;

namespace ChamCong_v03 {
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
				ChuanBiDSLichTrinhVaCa();
				PhanQuyenMenu(1);
				#region  tbd khu vực test 
/*
                frm_PhucHoiGioChamCong frm = new frm_PhucHoiGioChamCong();
                frm.Show();
*/
				#endregion 
			}
			else if (frmDangnhap.LoggedInResult == int.MaxValue) { // login thành công bằng tài khoản root
				PhanQuyenMenu(int.MaxValue);
				frm_01_Admin frm01 = new frm_01_Admin();
				frm01.ShowDialog();
			}
		}

		#region chuẩn bị dữ liệu : DS tất cả các Ca làm việc, tất cả các Lịch trình
		private void ChuanBiDSLichTrinhVaCa() {
			XL.DSLichTrinh.Clear();
			XL.DSCa.Clear();
			XL.DSCaMoRong.Clear();
			
			// lấy danh sách lịch trình, mỗi dòng là 1 ca thuộc lịch trình => để duyệt từng lịch trình thì phải lấy dicstint
			var tableLichTrinh = DAL.LayDSLichTrinh();
			var tableDSCa = DAL.LayDSCa();

			foreach (DataRow row in tableDSCa.Rows) {
				#region transfer dữ liệu từ row sang đối tượng
				var iShiftID = (int)row["ShiftID"];
				var sShiftCode = row["ShiftCode"].ToString();

				TimeSpan tsOnDuty;
				TimeSpan.TryParse(row["Onduty"].ToString(), out tsOnDuty);
				TimeSpan tOffDuty;
				TimeSpan.TryParse(row["Offduty"].ToString(), out tOffDuty);
				var iDayCount = (int)row["DayCount"];
				tOffDuty = tOffDuty.Add(new TimeSpan(iDayCount, 0, 0, 0));

				var tOnTimeIn = tsOnDuty.Subtract(new TimeSpan(0, (int)row["OnTimeIn"], 0));
				var tCutIn = tsOnDuty.Add(new TimeSpan(0, (int)row["CutIn"], 0));

				// phải add thêm 1 ngày daycount vì trong dữ liệu chỉ có chuỗi giờ thô : 05:45 không có ngày
				var tOnTimeOut = tOffDuty.Subtract(new TimeSpan(0, (int)row["OnTimeOut"], 0));
				var tCutOut = tOffDuty.Add(new TimeSpan(0, (int)row["CutOut"], 0));

				var tAfterOT = new TimeSpan(0, (int)row["AfterOT"], 0);
				var tLateGrace = new TimeSpan(0, (int)row["LateGrace"], 0);
				var tEarlyGrace = new TimeSpan(0, (int)row["EarlyGrace"], 0);

				var tOnLunch = XL2._0gio;
				var tOffLunch = XL2._0gio;
				if (row["OnLunch"] != DBNull.Value && row["OffLunch"] != DBNull.Value) {
					TimeSpan.TryParse(row["OnLunch"].ToString(), out tOnLunch);
					TimeSpan.TryParse(row["OffLunch"].ToString(), out tOffLunch);
				}

				var iShowPosition = (int)row["ShowPosition"];
				var tempWorkingTime = int.Parse(row["WorkingTime"].ToString());
				var kyhieucc = row["KyHieuCC"].ToString();
				var tempShift = new cCaChuan {
					ID = iShiftID,
					Code = sShiftCode,
					DayCount = iDayCount,
					QuaDem = (iDayCount == 1),
					OnnTS = tsOnDuty,
					OffTS = tOffDuty,
					OnnInnTS = tOnTimeIn,
					CutInnTS = tCutIn,
					OnnOutTS = tOnTimeOut,
					CutOutTS = tCutOut,
					AfterOTMin = tAfterOT,
					LateeMin = tLateGrace,
					EarlyMin = tEarlyGrace,
					Workingday = (Single)row["Workingday"],
					ShowPosition = iShowPosition,
					WorkingTimeTS = new TimeSpan(0, tempWorkingTime, 0),
					chophepTreTS = tsOnDuty.Add(tLateGrace),
					chophepSomTS = tOffDuty - tEarlyGrace,
					batdaulamthemTS = tOffDuty + tAfterOT,//[TBD] thay tOffDuty + XL2._30phut thành tempOffDuty + tempAfterOT --> đã thay
					LunchMin = tOffLunch.Subtract(tOnLunch),
					KyHieuCC = kyhieucc,
				};
				#endregion

				XL.DSCa.Add(tempShift);

			}
			var ca3va1 = XL.DSCa.Find(o => o.OnnTS == XL2._21h45 && o.Workingday == 2f);
			if (ca3va1 != null) {
				ca3va1.TachCa = true;
				var ca3 = XL.DSCa.Find(o => o.OnnTS == XL2._21h45 && o.Workingday == 1f);
				var ca1 = XL.DSCa.Find(o => o.OnnTS == XL2._05h45 && o.Workingday == 1f);
				((cCaChuan)ca3va1).catruoc = ca3 as cCaChuan;
				((cCaChuan)ca3va1).casauuu = ca1 as cCaChuan;
			}


			IEnumerable<DataRow> arrRows = tableLichTrinh.Rows.Cast<DataRow>();
			var arrRows_Distinct = (from row in arrRows select new { a = (int)(row["SchID"]), b = row["SchName"].ToString() }).Distinct();
			foreach (var row in arrRows_Distinct) {
				var arrRows1 = tableLichTrinh.Select("SchID=" + row.a, "");
				var lichtrinh = new cShiftSchedule { SchID = row.a, TenLichTrinh = row.b, DSCa = new List<cCaAbs>(), DSCaMoRong = new List<cCaAbs>() };
				foreach (var row1 in arrRows1) {
					var shiftid = (int)row1["T1"];
					var ca = XL.DSCa.Find(o => o.ID == shiftid);
					if (ca != null) lichtrinh.DSCa.Add(ca);
				}
				lichtrinh.DSCaMoRong = XL.TaoDSCaMoRong(lichtrinh.DSCa);
				XL.DSLichTrinh.Add(lichtrinh);
			}


			if (XL.DSLichTrinh.Count == 0)
				throw new NullReferenceException("Không có ds lịch trình");

		}



		#endregion


		//[v2] lam lai
		private void PhanQuyenMenu(int pAccountType) {

			// account thường
			if (pAccountType == int.MaxValue) {
				MenuTaiKhoan.Enabled = false;
				SubMenu_DoiMK.Enabled = false;
			}
			else {
				MenuTaiKhoan.Enabled = true;
				SubMenu_DoiMK.Enabled = true;// nếu login thành công thì cho phép đổi mật khẩu của mình
				DataTable dt = DAL.PhanQuyenMenu(XL2.currUserID);
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
						#region menu khai báo 1. vắng 2.cham cong tay 3.chấm công tay cho QL
						case 20001: // khai bao vang
							if (enable) MenuKhaiBao.Enabled = enable;
							SubMenu_KhaibaoVang.Enabled = enable;
							break;
						case 20002: // cham cong tay
							if (enable) MenuKhaiBao.Enabled = enable;
							SubMenu_ChamcongTay.Enabled = enable;
							break;
						case 20003: // cham cong tay cho QL
							if (enable) MenuKhaiBao.Enabled = enable;
							SubMenu_ChamcongTayQL.Enabled = enable;
							break;

						#endregion
						#region menu hoat động khác 1. sua hang loat 2. xem lich su 
						case 30001: // sua gio hang loat
							if (enable) MenuHoatDong.Enabled = enable;
							SubMenu_SuaGioHangLoat.Enabled = enable;
							break;
						case 30002: // xem lich su
							if (enable) MenuHoatDong.Enabled = enable;
							SubMenu_XemHistory.Enabled = enable;
							break;
						#endregion
						#region menu tính lương 1. tính lương 2. điều chỉnh lương tháng trước 3. thay đổi hệ số lương
						case 40001: // tinh luong
							if (enable) MenuTinhLuong.Enabled = enable;
							SubMenu_TinhLuong.Enabled = enable;
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
				frm1.Location = new Point((int)((this.Size.Width - frm1.Size.Width) / 2f),
										  (int)((this.Size.Height - frm1.Size.Height) / 2f));
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
		private void SubMenu_ChamcongTayQL_Click(object sender, EventArgs e) {
			frm_23_ChamCongQL frm1 = new frm_23_ChamCongQL();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_23_ChamCongQL;
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
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Maximized;
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
		private void MenuThoat_Click(object sender, EventArgs e) {
			if (ActiveMdiChild == null) {
				this.Close();
			}
			while (ActiveMdiChild != null) {
				ActiveMdiChild.Close();
			}
		}

	}
}
