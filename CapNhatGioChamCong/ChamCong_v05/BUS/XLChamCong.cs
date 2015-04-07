using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ChamCong_v05.DAL;
using ChamCong_v05.DTO;
using ChamCong_v05.Helper;
using log4net;
using log4net.Config;
using mySetting = ChamCong_v05.Properties.Settings;

namespace ChamCong_v05.BUS {
	public static partial class XL {
		#region ko còn sử dụng, chỉ để tham khảo
		/*
		public static void ArrayRowsToDS_CIO_V(DataRow[] arrRows, cShiftSchedule lichtrinh, List<cCheckInOut> DS_CIO_V) {
			// cấu trúc là cấu trúc ghép 2 bảng checkinout và xác nhận
			DS_CIO_V.Clear();

			if (arrRows.Length == 0 || arrRows.Length == 1) return;
			for (int x = 0, y = 1; x < arrRows.Length; ) {
				//i là index rowInn, j là index row out

				var rowInn = arrRows[x];
				if (y >= arrRows.Length) break; // ko có phần tử kế tiếp để xét
				var rowOut = arrRows[y];
				var UserEnrollNumber = (int)rowInn["UserEnrollNumber"];
				var IDinn = (int)rowInn["ID"];
				var IDout = (int)rowOut["ID"];
				var SourceInn = (string)rowInn["Source"];
				var SourceOut = (string)rowOut["Source"];
				var MachineNoInn = (int)rowInn["MachineNo"];
				var MachineNoOut = (int)rowOut["MachineNo"];
				var timeInn = (DateTime)rowInn["TimeStr"];
				var timeOut = (DateTime)rowOut["TimeStr"];
				var shiftID = (int)rowInn["ShiftID"];
				var checkThemInn = (rowInn["Them"] != DBNull.Value) && (bool)rowInn["Them"];
				var checkXoaaInn = (rowInn["Xoa"] != DBNull.Value) && (bool)rowInn["Xoa"];
				var IDGioGoc_Inn = (rowInn["IDGioGoc"] != DBNull.Value) ? (int)rowInn["IDGioGoc"] : -1;
				var checkThemOut = (rowOut["Them"] != DBNull.Value) && (bool)rowOut["Them"];
				var checkXoaaOut = (rowOut["Xoa"] != DBNull.Value) && (bool)rowOut["Xoa"];
				var IDGioGoc_Out = (rowOut["IDGioGoc"] != DBNull.Value) ? (int)rowOut["IDGioGoc"] : -1;
				var DuyetChoPhepVaoTre = (rowInn["DuyetChoPhepVaoTre"] != DBNull.Value) && (bool)rowInn["DuyetChoPhepVaoTre"];
				var DuyetChoPhepRaSom = (rowInn["DuyetChoPhepRaSom"] != DBNull.Value) && (bool)rowInn["DuyetChoPhepRaSom"];
				var pOTMin = (int)rowInn["OTMin"];

				if (IDinn != IDout) // bị mất cặp -> bỏ qua đến cặp kế tiếp, phải tăng x, y vì x+1=y ==> y+1
				{
					x = x + 1;
					y = y + 1;
					continue;
				}
				// cùng id --> xét có gần nhau ko
				if (MachineNoInn % 2 == MachineNoOut % 2) {
					y = y + 1;
					continue;
				}
				// đủ điều kiện ghép  cặp

				#region ghép

				var chkInnV = new cCheck { ID = IDinn, MachineNo = MachineNoInn, Source = SourceInn, Time = timeInn, Type = "I", PhucHoi = new cPhucHoi { Them = checkThemInn, IDGioGoc = IDGioGoc_Inn, Xoaa = checkXoaaInn }, MaCC = UserEnrollNumber };
				var chkOutV = new cCheck { ID = IDout, MachineNo = MachineNoOut, Source = SourceOut, Time = timeOut, Type = "O", PhucHoi = new cPhucHoi { Them = checkThemOut, IDGioGoc = IDGioGoc_Out, Xoaa = checkXoaaOut }, MaCC = UserEnrollNumber };
				var chkInOutV = new cCheckInOut {
					ID = IDinn, Vao = chkInnV, Raa = chkOutV, TimeDaiDien = chkInnV.Time,
					ShiftID = shiftID,
					DaXN = true,//HaveINOUT = 0, mặc định = 0
					DuyetChoPhepVaoTre = DuyetChoPhepVaoTre,
					DuyetChoPhepRaSom = DuyetChoPhepRaSom,
					ThuocNgayCong = ThuocNgayCong(chkInnV.Time),
					TG = new ThoiGian { OTCa = new TimeSpan(0, pOTMin, 0) },
					TD = new ThoiDiem { Vao = timeInn, Raa = timeOut }
				};
				DS_CIO_V.Add(chkInOutV);
				#endregion

				// sau khi thực hiện xong thì tăng 
				x = x + 2;
				y = y + 2;
			}


		}
*/

		#endregion
		private static readonly ILog log = LogManager.GetLogger("XL");
		public static List<cCa> DSCa = new List<cCa>();
		public static List<cCa> DSCaMoRong = new List<cCa>();
		public static List<cShiftSchedule> DSLichTrinh = new List<cShiftSchedule>();

		public static DateTime ThuocNgayCong(DateTime chkinn)// lưu ý không dùng cho các giờ ra, giờ ra thiếu vào, giờ vào thiếu ra
		{
			return chkinn.TimeOfDay < XL2._03gio ? chkinn.Date.AddDays(-1) : chkinn.Date;
		}

		public static void Vao(DateTime timeinn, DateTime onnduty, DateTime chopheptre, bool DuyetChoPhepVaoTre, out DateTime td_batdau_lv, out TimeSpan tre) {
			if (DuyetChoPhepVaoTre) {
				td_batdau_lv = onnduty;
				tre = TimeSpan.Zero;
			}
			else {
				var timeInn_0second = new DateTime(timeinn.Year, timeinn.Month, timeinn.Day, timeinn.Hour, timeinn.Minute, 0);
				if (chopheptre < timeInn_0second) {
					td_batdau_lv = timeInn_0second;
					tre = timeInn_0second - onnduty;
				}
				else {
					td_batdau_lv = onnduty;
					tre = TimeSpan.Zero;
				}
			}
		}

		public static void Raa(DateTime timeout, DateTime offduty, DateTime chophepsom, bool DuyetChoPhepRaSom, out DateTime td_ketthuc_lv_chuaOT, out TimeSpan som) {
			if (DuyetChoPhepRaSom) {
				td_ketthuc_lv_chuaOT = offduty;
				som = TimeSpan.Zero;
			}
			else {
				var timeOut_0second = new DateTime(timeout.Year, timeout.Month, timeout.Day, timeout.Hour, timeout.Minute, 0);
				if (timeOut_0second < chophepsom) {
					td_ketthuc_lv_chuaOT = timeOut_0second;
					som = offduty - timeOut_0second;
				}
				else {
					td_ketthuc_lv_chuaOT = offduty;
					som = TimeSpan.Zero;
				}
			}
		}

		public static void OLai(DateTime timeout, DateTime offduty, DateTime batdaulamthem, out TimeSpan olai) {
			var timeOut_0second = new DateTime(timeout.Year, timeout.Month, timeout.Day, timeout.Hour, timeout.Minute, 0);
			if (batdaulamthem < timeOut_0second) {
				olai = timeOut_0second - offduty;
			}
			else {
				olai = TimeSpan.Zero;
			}
		}

		public static void Tinh_TGLamViec(DateTime thoidiem_batdau_lamviec, DateTime thoidiem_ketthuc_lamviec_DaCoOT, TimeSpan LunchMin, out TimeSpan TGLamTinhCong) {
			TGLamTinhCong = (thoidiem_ketthuc_lamviec_DaCoOT - thoidiem_batdau_lamviec).Subtract(LunchMin);
		}

		public static void Tinh_TGLamViecTrongCa(DateTime tdBdLv, DateTime tdKtLvTrongCa, TimeSpan lunchMin, out TimeSpan tgGioLamViecTrongCa) {//ver 4.0.0.4	
			tgGioLamViecTrongCa = (tdKtLvTrongCa - tdBdLv).Subtract(lunchMin);
		}

		public static void Tinh_TGLamViec_Ca3(DateTime bd_lv, DateTime kt_lv_DaCoOT, DateTime startNT, DateTime endddNT,
			out DateTime bd_lv_ca3, out DateTime kt_lv_ca3, out TimeSpan TGLamDem, out bool QuaDem) {
			TimeSpan tempTGLamDem;
			var BDLamDem = DateTime.MinValue;
			var KTLamDem = DateTime.MinValue;
			if (kt_lv_DaCoOT > startNT) {
				BDLamDem = bd_lv > (startNT + XL2.ChoPhepTre) ? bd_lv : startNT;
				KTLamDem = kt_lv_DaCoOT < (endddNT - XL2.ChoPhepSom) ? kt_lv_DaCoOT : endddNT;
				tempTGLamDem = KTLamDem - BDLamDem;
			}
			else tempTGLamDem = TimeSpan.Zero;
			if (tempTGLamDem < XL2.TGLamDemToiThieu) {
				bd_lv_ca3 = DateTime.MinValue;
				kt_lv_ca3 = DateTime.MinValue;
				TGLamDem = TimeSpan.Zero;
				QuaDem = false;
			}
			else {
				bd_lv_ca3 = BDLamDem;
				kt_lv_ca3 = KTLamDem;
				TGLamDem = tempTGLamDem;
				QuaDem = true;
			}
		}

		public static TimeSpan Tinh_TGLamThem(TimeSpan TGGioLamViec) {
			return (TGGioLamViec - XL2._08gio) >= XL2._01phut ? (TGGioLamViec - XL2._08gio) : TimeSpan.Zero;
		}

		public static void Tinh_TGLamTCC3(TimeSpan TGLamTC, TimeSpan TGLamDem, out TimeSpan TGLamTC_Ngay, out TimeSpan TGLamDem_koTC, out TimeSpan TGLamTC_Dem) {
			TGLamTC_Ngay = TimeSpan.Zero;
			TGLamDem_koTC = TimeSpan.Zero;
			if (TGLamTC >= TGLamDem) // trọn qua đêm là tăng cường đêm, còn lại là tăng cường ngày
			{
				TGLamTC_Dem = TGLamDem;
				TGLamTC_Ngay = TGLamTC - TGLamDem; // số giờ tính pctc
			}
			else  // 1 ThuocCa đêm nhưng 0.5 ThuocCa ko tính tăng cường, nửa ThuocCa tính tăng cường đêm
			{
				TGLamTC_Dem = TGLamTC;
				TGLamDem_koTC = TGLamDem - TGLamTC;
			}
		}

		public static void Tinh_PCTC(bool TinhPC50, bool QuaDem, TimeSpan SoGioLamDemmm, TimeSpan SoGioLamThem,
			out TimeSpan tgTinh130, out TimeSpan tgTinh150, out TimeSpan tgTinhTCC3,
			out float PhuCap30, out float PhuCapTC, out float PhuCapTCC3, out float TongPhuCap) {

			var heso_pctc = Convert.ToSingle(XL2.PC50) / 100f;
			var heso_pcdem = Convert.ToSingle(XL2.PC30) / 100f; //tbd
			var heso_pctcc3 = (Convert.ToSingle(XL2.PCTCC3)) / 100f;

			PhuCap30 = 0f;
			PhuCapTC = 0f;
			PhuCapTCC3 = 0f;
			TongPhuCap = 0f;
			tgTinh130 = TimeSpan.Zero;
			tgTinh150 = TimeSpan.Zero;
			tgTinhTCC3 = TimeSpan.Zero;
			if (QuaDem == false) // ko qua đêm, tính pctc 50 % bình thường
			{
				if (TinhPC50 && (SoGioLamThem > XL2._01phut)) {
					tgTinh150 = SoGioLamThem;
					PhuCapTC = Convert.ToSingle(Math.Round((SoGioLamThem.TotalHours / 8d) * heso_pctc, 2, MidpointRounding.ToEven));
					TongPhuCap = PhuCapTC;
				}
			}
			else // có thể có qua đêm, có thể tính pctcc3 100%
			{
				if (TinhPC50 == false || (SoGioLamThem <= XL2._01phut))	  // ko có tăng cường thì chỉ tính pc đêm
				{
					PhuCap30 = (Convert.ToSingle(SoGioLamDemmm.TotalHours / 8d)) * heso_pcdem;
					TongPhuCap = PhuCap30;
				}
				else {
					Tinh_TGLamTCC3(SoGioLamThem, SoGioLamDemmm, out tgTinh150, out tgTinh130, out tgTinhTCC3);
					PhuCapTCC3 = Convert.ToSingle(Math.Round((tgTinhTCC3.TotalHours / 8d) * heso_pctcc3, 2, MidpointRounding.ToEven));
					PhuCapTC = Convert.ToSingle(Math.Round((tgTinh150.TotalHours / 8d) * heso_pctc, 2, MidpointRounding.ToEven));
					PhuCap30 = Convert.ToSingle(Math.Round((tgTinh130.TotalHours / 8d) * heso_pcdem, 2, MidpointRounding.ToEven));
					TongPhuCap = PhuCap30 + PhuCapTC + PhuCapTCC3;

					/*
										if (SoGioLamThem >= SoGioLamDemmm) // trọn qua đêm là tăng cường đêm, còn lại là tăng cường ngày
										{
											tgTinhTCC3 = SoGioLamDemmm;
											tgTinh150 = SoGioLamThem - SoGioLamDemmm; // số giờ tính pctc
											PhuCapTCC3 = (tgTinhTCC3.TotalHours / 8d) * heso_pctcc3;
											PhuCapTC = (tgTinh150.TotalHours / 8d) * heso_pctc;
											TongPhuCap = PhuCapTC + PhuCapTCC3;
										}
										else  // 1 ThuocCa đêm nhưng 0.5 ThuocCa ko tính tăng cường, nửa ThuocCa tính tăng cường đêm
										{
											tgTinhTCC3 = SoGioLamThem;
											tgTinh130 = SoGioLamDemmm - SoGioLamThem;
											PhuCapTCC3 = (tgTinhTCC3.TotalHours / 8d) * heso_pctcc3;
											PhuCap30 = (tgTinh130.TotalHours / 8d) * heso_pcdem;
											TongPhuCap = PhuCap30 + PhuCapTCC3;
										}
					*/
				}
			}

		}


		public static void TinhPCDB(bool TinhPCDB, TimeSpan SoGioTinhCong, TimeSpan SoGioLamDemmm, bool QuaDem,
			int loaiPC, float heso_pc_banNgay, float heso_pc_banDem,
			out TimeSpan tgTinh200, out TimeSpan tgTinh260, out TimeSpan tgTinh300, out TimeSpan tgTinh390, out TimeSpan tgTinhCus,
			out float PhuCap100, out float PhuCap160, out float PhuCap200, out float PhuCap290, out float PhuCapCus, ref float TongPhuCap) {

			PhuCap100 = 0f;
			PhuCap160 = 0f;
			PhuCap200 = 0f;
			PhuCap290 = 0f;
			PhuCapCus = 0f;
			tgTinh200 = TimeSpan.Zero;
			tgTinh260 = TimeSpan.Zero;
			tgTinh300 = TimeSpan.Zero;
			tgTinh390 = TimeSpan.Zero;
			tgTinhCus = TimeSpan.Zero;
			if (TinhPCDB) {
				switch (loaiPC) {
					case 200:
						tgTinh200 = SoGioTinhCong - SoGioLamDemmm; // số giờ làm việc ban ngày
						tgTinh260 = SoGioLamDemmm; // số giờ làm việc ban đêm
						PhuCap100 = Convert.ToSingle(Math.Round((tgTinh200.TotalHours / 8d) * (heso_pc_banNgay / 100f), 2, MidpointRounding.ToEven)); // phụ cấp 100% số giờ làm việc ban ngày
						PhuCap160 = Convert.ToSingle(Math.Round((tgTinh260.TotalHours / 8d) * (heso_pc_banDem / 100f), 2, MidpointRounding.ToEven)); // phụ cấp 150% số giờ làm việc ban đêm
						TongPhuCap = PhuCap100 + PhuCap160;
						break;
					case 300:
						tgTinh300 = SoGioTinhCong - SoGioLamDemmm;
						tgTinh390 = SoGioLamDemmm;
						PhuCap200 = Convert.ToSingle(Math.Round((tgTinh300.TotalHours / 8d) * (heso_pc_banNgay / 100f), 2, MidpointRounding.ToEven)); // phụ cấp 200% số giờ làm việc ban ngày
						PhuCap290 = Convert.ToSingle(Math.Round((tgTinh390.TotalHours / 8d) * (heso_pc_banDem / 100f), 2, MidpointRounding.ToEven)); // phụ cấp 250% số giờ làm việc ban đêm
						TongPhuCap = PhuCap200 + PhuCap290;
						break;
					case 1:
						tgTinhCus = Tinh_TGLamThem(SoGioTinhCong);//((SoGioTinhCong - XL2._08gio > XL2._01phut) ? (SoGioTinhCong - XL2._08gio) : TimeSpan.Zero);
						PhuCapCus = Convert.ToSingle(Math.Round((tgTinhCus.TotalHours / 8d) * (heso_pc_banNgay / 100f), 2, MidpointRounding.ToEven));
						TongPhuCap = PhuCapCus;
						break;
					case 2:
						tgTinhCus = SoGioTinhCong;
						PhuCapCus = Convert.ToSingle(Math.Round((tgTinhCus.TotalHours / 8d) * (heso_pc_banNgay / 100f), 2, MidpointRounding.ToEven));
						TongPhuCap = PhuCapCus;
						break;
				}
			}
		}


		public static void TaoCaTuDo(cCa Ca, DateTime CheckInTime, TimeSpan WorkingTime, TimeSpan SoPhutChoPhepTre, TimeSpan SoPhutChoPhepSomTS, TimeSpan SoPhutAfterOT, float WorkingDay, string kyhieu, bool add1Day = true) {
			//var temp = CheckInTime.TimeOfDay;//ver 4.0.0.0//tbd xem lại ngày công
			var temp = new TimeSpan(CheckInTime.TimeOfDay.Hours, CheckInTime.TimeOfDay.Minutes, 0);//ver 4.0.0.1 bỏ phần giây, chỉ giữ phần giờ, phút
			if (CheckInTime.TimeOfDay < XL2._03gio) temp = Ca.Duty.Onn.Add(XL2._1ngay); //ThuocCa 3 , ThuocCa 3 va 1 vẫn giữ nguyên vì 21h > 4h//tbd xem lại ngày công
			Ca.Duty = new TS { Onn = temp, Off = temp.Add(WorkingTime) };
			Ca.LateeMin = SoPhutChoPhepTre;
			Ca.EarlyMin = SoPhutChoPhepSomTS;
			Ca.AfterOTMin = SoPhutAfterOT;
			Ca.chophepTreTS = Ca.Duty.Onn.Add(SoPhutChoPhepTre);
			Ca.chophepSomTS = Ca.Duty.Off.Subtract(SoPhutChoPhepSomTS);
			Ca.batdaulamthemTS = Ca.Duty.Off.Add(SoPhutAfterOT);
			Ca.DayCount = Ca.Duty.Off.Days;
			Ca.QuaDem = (Ca.Duty.Off.Days == 1);
			Ca.WorkingTimeTS = WorkingTime;
			Ca.Workingday = WorkingDay;
			Ca.LunchMin = XL2._0gio;
			Ca.KyHieuCC = kyhieu;
		}
		public static void TaoCaTuDo(cCa Ca, DateTime CheckInTime) {
			//var temp = CheckInTime.TimeOfDay;//ver 4.0.0.0//tbd xem lại ngày công
			var temp = new TimeSpan(CheckInTime.TimeOfDay.Hours, CheckInTime.TimeOfDay.Minutes, 0);//ver 4.0.0.1 bỏ phần giây, chỉ giữ phần giờ, phút
			if (CheckInTime.TimeOfDay < XL2._03gio) temp = Ca.Duty.Onn.Add(XL2._1ngay); //ThuocCa 3 , ThuocCa 3 va 1 vẫn giữ nguyên vì 21h > 4h//tbd xem lại ngày công
			if (Ca.ID == int.MinValue + 0) {
				Ca.Duty = new TS { Onn = temp, Off = temp.Add(XL2._08gio) };
				Ca.WorkingTimeTS = XL2._08gio;
				Ca.Workingday = 1f;
				Ca.Code = mySetting.Default.shiftCodeCa8h;
				Ca.MoTa = string.Format(mySetting.Default.MoTaCaTuDo, 8);
				Ca.KyHieuCC = mySetting.Default.kyHieuCCCa8h;
			}
			else if (Ca.ID == int.MinValue + 1) {
				Ca.Duty = new TS { Onn = temp, Off = temp.Add(XL2._12gio) };
				Ca.WorkingTimeTS = XL2._12gio;
				Ca.Workingday = 1.5f;
				Ca.Code = mySetting.Default.shiftCodeCa12h;
				Ca.MoTa = string.Format(mySetting.Default.MoTaCaTuDo, 12);
				Ca.KyHieuCC = mySetting.Default.kyHieuCCCa12h;
			}
			else if (Ca.ID == int.MinValue + 2) {
				Ca.Duty = new TS { Onn = temp, Off = temp.Add(XL2._04gio) };
				Ca.WorkingTimeTS = XL2._04gio;
				Ca.Workingday = 0.5f;
				Ca.Code = mySetting.Default.shiftCodeCa4h;
				Ca.MoTa = string.Format(mySetting.Default.MoTaCaTuDo, 4);
				Ca.KyHieuCC = mySetting.Default.kyHieuCCCa4h;
			}
			else if (Ca.ID == int.MinValue + 3) {
				Ca.Duty = new TS { Onn = temp, Off = temp.Add(XL2._16gio) };
				Ca.WorkingTimeTS = XL2._16gio;
				Ca.Workingday = 2f;
				Ca.Code = mySetting.Default.shiftCodeCa16h;
				Ca.MoTa = string.Format(mySetting.Default.MoTaCaTuDo, 16);
				Ca.KyHieuCC = mySetting.Default.kyHieuCCCa16h;
			}

			Ca.LateeMin = XL2.ChoPhepTre;
			Ca.EarlyMin = XL2.ChoPhepSom;
			Ca.AfterOTMin = XL2.LamThemAfterOT;
			Ca.chophepTreTS = Ca.Duty.Onn.Add(XL2.ChoPhepTre);
			Ca.chophepSomTS = Ca.Duty.Off.Subtract(XL2.ChoPhepSom);
			Ca.batdaulamthemTS = Ca.Duty.Off.Add(XL2.LamThemAfterOT);
			Ca.DayCount = Ca.Duty.Off.Days;
			Ca.QuaDem = (Ca.Duty.Off.Days == 1);
			Ca.LunchMin = XL2._0gio;
		}


		//------------------------------------------------------------------------------------------------------------------------------------------
		public static List<cUserInfo> KhoiTaoDSNV_ChamCong(List<cUserInfo> dsnv, List<int> m_listIDPhongBan, List<cPhongBan> dsphong) {
			return new List<cUserInfo>();
		}
		public static List<cUserInfo> KhoiTaoDSNV_TinhLuong(List<cUserInfo> dsnv, List<cPhongBan> dsphong) {
			return new List<cUserInfo>();
		}

		public static cUserInfo KhoiTaoNV(int uen, string ten, string manv,
			float? hslCB, float? hslCV, float? hsBHcongthem, List<cPhongBan> phongBans,
			int? schID = null, int? idChucVu = null, string ChucVu = null,
			int? maphong = null) {
			return new cUserInfo();
		}


		public static void DiemDanh_v08(List<cUserInfo> dsnv, DateTime ngayBD_Bef2D, DateTime ngayKT_Aft2D, DateTime ngaydiemdanh, DateTime currentTime) {
		}

		public static void DiemDanhTheoNgay(List<cNgayCong> dsNgayCong, DateTime dateDiemDanh, DateTime currentTime) {
		}

		public static void XemCong_v08(List<cUserInfo> dsnv, DateTime ngayBD_Bef2D, DateTime ngayKT_Aft2D) {
			XmlConfigurator.Configure();
			if (dsnv.Count == 0) return;
			#region nạp dữ liệu từ database
			var Arr_MaCC = (from nv in dsnv select nv.MaCC).ToArray(); // tạo mảng danh sách mã chấm công để viết chuỗi query : or.. or
			var tableCheck_A = DAO5.LayTableCIO_A(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableCheck_V = DAO5.LayTableCIO_V5(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableXPVang = DAO5.LayTableXPVang(ngayBD_Bef2D, ngayKT_Aft2D, Arr_MaCC);
			var tableXN_PCTC = DAO5.LayTableXacNhanPC50(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableXN_PCDB = DAO5.LayTableXacNhanPCDB(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D, Duyet: true);
			var tableNgayLe = DAO5.DocNgayLe(ngayBD_Bef2D, ngayKT_Aft2D);
			#endregion

			#region transfer dữ liệu sang object
			foreach (var nv in dsnv) {
				var tempMaCC = nv.MaCC;
				nv.NgayCongBD_Bef2D = ngayBD_Bef2D;
				nv.NgayCongKT_Aft2D = ngayKT_Aft2D;
				LoadDSCheck_A5(tempMaCC, tableCheck_A, out nv.DS_Check_A);
				LoadDSCIO_V5(tempMaCC, tableCheck_V, out nv.DS_CIO_V);
				LoadDSXPVang_Le(tempMaCC, tableXPVang, tableNgayLe, nv.DSVang);
				LoadDSXNPC50(tempMaCC, tableXN_PCTC, nv.DSXNPhuCap50);
				LoadDSXNPCDB(tempMaCC, tableXN_PCDB, nv.DSXNPhuCapDB);
				// khởi tạo danh sách ngày công
				KhoiTaoDSNgayCong(nv.DSNgayCong, ngayBD_Bef2D, ngayKT_Aft2D);

			}

			#endregion
			#region xử lý
			var DS_Check_KoHopLe_AllNV = new List<cCheck>();
			var ds_raa3_vao1 = new List<cCheck>();
			foreach (var nv in dsnv) {
				List<cCheck> DS_Check_KoHopLe_1NV;
				LoaiBoCheckKoHopLe15(nv.DS_Check_A, out DS_Check_KoHopLe_1NV);// loại bỏ check cùng loại trong 30ph, IO 30 phút
				DS_Check_KoHopLe_AllNV.AddRange(DS_Check_KoHopLe_1NV);// add vào ds ko hợp lệ của nhiều nhân viên để xóa sau
				GhepCIO_A2(nv.DS_Check_A, out nv.DS_CIO_A);
				XetCa_ListCIO_A3(nv.DS_CIO_A, nv.LichTrinhLV, ds_raa3_vao1, nv.DS_Check_A);// xét thuộc ThuocCa, update thuộc ngày công, tách ThuocCa 3&1 nếu có, rồi tính công//nv.MacDinhTinhPC50, //[140615_4]
				XetCa_ListCIO_V4_5(nv.DS_CIO_V);
				TronDS_CIO_A_V5(nv.DS_CIO_A, nv.DS_CIO_V, out nv.DSVaoRa);
				PhanPhoi_DSVaoRa6(nv.DSVaoRa, nv.DSNgayCong);
				PhanPhoi_DSVang7(nv.DSVang, nv.DSNgayCong);
				TinhCong_ListNgayCong8(nv.DSNgayCong, nv.StartNT, nv.EndddNT);//ver 4.0.0.4
				TinhPCTC_TrongListXNPCTC9(nv.DSXNPhuCap50, nv.DSNgayCong);
				TinhPCDB_TrongListXNPCDB10(nv.DSXNPhuCapDB, nv.DSNgayCong);
			}
			if (DS_Check_KoHopLe_AllNV.Count > 0) DAO5.LoaiGioLienQuan(DS_Check_KoHopLe_AllNV);
			if (ds_raa3_vao1.Count > 0) DAO5.ThemGio_ra3_vao1(ds_raa3_vao1);
			#endregion
		}

		public static void LoadDSCheck_A5(int MaCC, DataTable tableCheck_A, out List<cCheck> ds_Check_A) {
			var arrRows = tableCheck_A.Select("UserEnrollNumber = " + MaCC, "TimeStr asc");
			ds_Check_A = new List<cCheck>();

			if (arrRows.Length == 0) return;

			foreach (var row in arrRows) {
				var userEnrollNumber = (int)row["UserEnrollNumber"];
				var machineNo = (int)row["MachineNo"];
				var source = (string)row["Source"];
				var timeStr = (DateTime)row["TimeStr"];

				cCheck check;
				if (machineNo % 2 == 1) {
					var checkInn = new cCheck { ID = int.MinValue, MaCC = userEnrollNumber, Time = timeStr, Source = source, MachineNo = machineNo, Type = "I" };
					check = checkInn;
				}
				else {
					var checkOut = new cCheck { ID = int.MinValue, MaCC = userEnrollNumber, Time = timeStr, Source = source, MachineNo = machineNo, Type = "O" };
					check = checkOut;
				}
				ds_Check_A.Add(check);
			}

		}
		public static void LoadDSCIO_V5(int maCC, DataTable tableCheck_V, out List<cCheckInOut> DS_CIO_V) {
			var arrRows = tableCheck_V.Select("UserEnrollNumber = " + maCC, "ID asc, TimeStr asc");
			DS_CIO_V = new List<cCheckInOut>();

			if (arrRows.Length == 0 || arrRows.Length == 1) return;
			for (int x = 0, y = 1; x < arrRows.Length; ) {
				//i là index rowInn, j là index row out

				var rowInn = arrRows[x];
				if (y >= arrRows.Length) break; // ko có ph?n t? k? ti?p d? xét
				var rowOut = arrRows[y];
				var userEnrollNumber = (int)rowInn["UserEnrollNumber"];
				var idInn = (int)rowInn["ID"];
				var idOut = (int)rowOut["ID"];
				var sourceInn = (string)rowInn["Source"];
				var sourceOut = (string)rowOut["Source"];
				var machineNoInn = (int)rowInn["MachineNo"];
				var machineNoOut = (int)rowOut["MachineNo"];
				var timeInn = (DateTime)rowInn["TimeStr"];
				var timeOut = (DateTime)rowOut["TimeStr"];
				var shiftID = (int)rowInn["ShiftID"];

				var duyetChoPhepVaoTre = (rowInn["DuyetChoPhepVaoTre"] != DBNull.Value) && (bool)rowInn["DuyetChoPhepVaoTre"];
				var duyetChoPhepRaSom = (rowInn["DuyetChoPhepRaSom"] != DBNull.Value) && (bool)rowInn["DuyetChoPhepRaSom"];
				var vaoTreTinhCV = (rowInn["VaoTreLaCV"] != DBNull.Value) && (bool)rowInn["VaoTreLaCV"];//ver 4.0.0.4	
				var raaSomTinhCV = (rowInn["RaSomLaCV"] != DBNull.Value) && (bool)rowInn["RaSomLaCV"];//ver 4.0.0.4	
				var otMin = (int)rowInn["OTMin"];

				if (idInn != idOut) // b? m?t c?p -> b? qua d?n c?p k? ti?p, ph?i tang x, y v́ x+1=y ==> y+1
				{
					x = x + 1;
					y = y + 1;
					continue;
				}
				// cùng id --> xét có g?n nhau ko
				if (machineNoInn % 2 == machineNoOut % 2) {
					y = y + 1;
					continue;
				}
				// d? di?u ki?n ghép  c?p

				#region ghép

				var chkInnV = new cCheck { ID = idInn, MachineNo = machineNoInn, Source = sourceInn, Time = timeInn, Type = "I", MaCC = userEnrollNumber };
				var chkOutV = new cCheck { ID = idOut, MachineNo = machineNoOut, Source = sourceOut, Time = timeOut, Type = "O", MaCC = userEnrollNumber };
				var chkInOutV = new cCheckInOut {
					/*ID = IDinn,*/
					DaXN = true, Vao = chkInnV, Raa = chkOutV, TimeDaiDien = chkInnV.Time,//HaveINOUT = 0, m?c d?nh = 0
					ShiftID = shiftID,
					DuyetChoPhepVaoTre = duyetChoPhepVaoTre, DuyetChoPhepRaSom = duyetChoPhepRaSom,
					VaoTreTinhCV = vaoTreTinhCV, RaaSomTinhCV = raaSomTinhCV,//ver 4.0.0.4	
					ThuocNgayCong = ThuocNgayCong(chkInnV.Time),
					TG = new ThoiGian { SoPhutLamThem5 = new TimeSpan(0, otMin, 0) },
					TD = new ThoiDiem()
				};
				DS_CIO_V.Add(chkInOutV);
				#endregion

				// sau khi th?c hi?n xong th́ tang 
				x = x + 2;
				y = y + 2;
			}

		}
		public static void LoadDSXPVang_Le(int tempMaCC, DataTable tableVang, DataTable tableNgayLe, List<cLoaiVang> dsVangs) { }
		public static void LoadDSXNPC50(int tempMaCC, DataTable tableXN_PCTC, List<structPCTC> dsXacNhanPC) { }
		public static void LoadDSXNPCDB(int tempMaCC, DataTable tableXN_PCDB, List<structPCDB> dsXacNhanPC) { }
		public static void KhoiTaoDSNgayCong(List<cNgayCong> DSNgayCong, DateTime ngayBD_Bef2D, DateTime ngayKT_Aft2D) {
			DSNgayCong.Clear();
			var indexNgay = ngayBD_Bef2D.Date; // ngayBD_Bef2D là trước 2 ngày vd: xem ngày 3 thì lấy giờ từ ngày 1, nhưng khi tạo ngày công thì lấy từ ngày 2
			DSNgayCong.Add(new cNgayCong { Ngay = indexNgay, DSVaoRa = new List<cCheckInOut>(), prev = null, next = null });
			indexNgay = indexNgay.AddDays(1d);

			for (; indexNgay <= ngayKT_Aft2D.Date; indexNgay = indexNgay.AddDays(1d)) {
				var ngayCong = new cNgayCong { Ngay = indexNgay, DSVaoRa = new List<cCheckInOut>(), prev = DSNgayCong[DSNgayCong.Count - 1], next = null };
				DSNgayCong[DSNgayCong.Count - 1].next = ngayCong;
				DSNgayCong.Add(ngayCong);
			}
		}
		public static void PhanPhoi_DSVang7(List<cLoaiVang> dsVang, List<cNgayCong> dsNgayCong) {
			var skipElement1 = 0;
			dsVang.Sort(new cLoaiVangComparer());
			foreach (var ngayCong in dsNgayCong) {
				ngayCong.DSVang = (from vang in dsVang.Skip(skipElement1)
								   where ngayCong.Ngay == vang.Ngay
								   select vang).ToList();
				skipElement1 += ngayCong.DSVang.Count; //tbd test skip element
			}
		}
		public static void PhanPhoi_DSVaoRa6(List<cCheckInOut> dsVaoRa, List<cNgayCong> dsNgayCong) {
			var skipElement2 = 0;
			foreach (var ngayCong in dsNgayCong) {
				ngayCong.DSVaoRa = (from vaoraa in dsVaoRa.Skip(skipElement2)
									where vaoraa.ThuocNgayCong == ngayCong.Ngay
									select vaoraa).ToList();
				skipElement2 += ngayCong.DSVaoRa.Count;
			}
		}


		public static void LoaiBoCheckKoHopLe15(List<cCheck> ds_Check_A, out List<cCheck> ds_Check_Trong30ph) {
			/* 1. lọc này phải dảm bảo sort trước khi vào hàm này xử lý
			 * 2. cùng I hoặc cùng O của máy chấm công tron 30ph thì loại, check thêm từ PC thì giữ nguyên
			 */
			ds_Check_Trong30ph = new List<cCheck>();
			if (ds_Check_A == null || ds_Check_A.Count == 0 || ds_Check_A.Count == 1) return; //1 phần tử thì khỏi xử lý
			var i = 0;
			while (i + 1 < ds_Check_A.Count) {
				var before = ds_Check_A[i];
				var afterr = ds_Check_A[i + 1];
				if (before.Source == "PC") i++;// bỏ qua before luôn
				else if (afterr.Source == "PC") i = i + 2;// bỏ qua after luôn
				else if ((before.Type == afterr.Type) && ((afterr.Time - before.Time) < XL2._10phut)) {
					// cùng loại trong 30ph
					//info ver 4.0.0.1 // giữ vào đầu tiên, ra cuối cùng
					if (before.Type == "I") {
						before.IsEdited += afterr.IsEdited;
						ds_Check_Trong30ph.Add(afterr);
						ds_Check_A.Remove(afterr);
					}
					else //out
					{
						afterr.IsEdited += before.IsEdited;
						ds_Check_Trong30ph.Add(before);
						ds_Check_A.Remove(before);
					}
				}
				else if (before.Type == "I" && afterr.Type == "O"
					&& (afterr.Time - before.Time) < XL2._10phut) {
					//IO trong 30 phút thì chỉ giữ O
					afterr.IsEdited += before.IsEdited;
					ds_Check_Trong30ph.Add(before);
					ds_Check_A.Remove(before);
				}
				else i++;
			}
		}

		public static void GhepCIO_A2(List<cCheck> ds_Check_A, out List<cCheckInOut> DS_CIO_A) {
			DS_CIO_A = new List<cCheckInOut>();
			var x = 0;
			while (x + 1 < ds_Check_A.Count) {
				var chk_1 = ds_Check_A[x];
				var chk_2 = ds_Check_A[x + 1];
				if (chk_1.Type == "O") {
					// đầu ds là checkOut --> ra ko vào
					var CIO = new cCheckInOut { Vao = null, Raa = chk_1, HaveINOUT = -2, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
					DS_CIO_A.Add(CIO);
					x++;
				}
				else {
					//đầu ds là checkInn-> kiểm tra kế nếu cũng là check In thì checkInn trước là vào ko ra
					if (chk_2.Type == "I") {
						var CIO = new cCheckInOut { Vao = chk_1, Raa = null, HaveINOUT = -1, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
						DS_CIO_A.Add(CIO);
						x++;
					}
					else {
						// kế là checkOut --> kiểm tra nằm trong khoảng >30ph và dưới 21h45 thì ghép, ngược lại thì giờ vào ko ra, ra ko vào
						var duration = chk_2.Time - chk_1.Time;
						if (duration > XL2._22h00) {//ver 4.0.0.4	old:(duration > XL2._21h45)
							var CIO1 = new cCheckInOut { Vao = chk_1, Raa = null, HaveINOUT = -1, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
							var CIO2 = new cCheckInOut { Vao = null, Raa = chk_2, HaveINOUT = -2, TimeDaiDien = chk_2.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
							DS_CIO_A.Add(CIO1);
							x++;
							DS_CIO_A.Add(CIO2);
							x++;
						}
						else {
							var CIO = new cCheckInOut { Vao = chk_1, Raa = chk_2, HaveINOUT = 0, TimeDaiDien = chk_1.Time, };
							DS_CIO_A.Add(CIO);
							x++;
							x++;
						}
					}
				}
			}
			// xảy ra 2 TH, 1 là hết ds--> ko làm gì hết, 2 là còn lại 1 pt-> them vào
			if (x < ds_Check_A.Count) {
				var chk_1 = ds_Check_A[x];
				if (chk_1.Type == "I") {
					var CIO1 = new cCheckInOut { Vao = chk_1, Raa = null, HaveINOUT = -1, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
					DS_CIO_A.Add(CIO1);
				}
				else {
					var CIO2 = new cCheckInOut { Vao = null, Raa = chk_1, HaveINOUT = -2, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
					DS_CIO_A.Add(CIO2);
				}
			}
		}

		public static void XetCa_ListCIO_A3(List<cCheckInOut> ds_CIO_A, cShiftSchedule lichtrinh, List<cCheck> ds_raa3_vao1, List<cCheck> ds_check_A) { }

		public static void XetCa_ListCIO_A3_5(List<cCheckInOut> ds_CIO_A, cShiftSchedule lichtrinh, List<cCheck> ds_raa3_vao1, List<cCheck> ds_check_A) {
			var i = 0;
			while (i < ds_CIO_A.Count) {
				var CIO = ds_CIO_A[i];
				bool isTachCaDem = false;
				cCheck checkRaa3, checkVao1;
				cCheckInOut cio_ca3, cio_ca1;
				cCa thuocCa_Ca3, thuocCa_Ca1;
				XetCa_1_CIO_A5(CIO, lichtrinh, out CIO.ThuocNgayCong, out CIO.ThuocCa, out isTachCaDem, out CIO.DSCa,
					out checkRaa3, out checkVao1, out cio_ca3, out cio_ca1, out thuocCa_Ca3, out thuocCa_Ca1);

				if (isTachCaDem) {
					ds_raa3_vao1.Add(checkRaa3);
					ds_raa3_vao1.Add(checkVao1);//vaoca1
					ds_check_A.Add(checkRaa3);//raaca3
					ds_check_A.Add(checkVao1);
					ds_check_A.Sort(new cCheckComparer());

					cio_ca3.ThuocCa = thuocCa_Ca3;
					cio_ca1.ThuocCa = thuocCa_Ca1;
					cio_ca3.ThuocNgayCong = new DateTime(CIO.ThuocNgayCong.Year, CIO.ThuocNgayCong.Month, CIO.ThuocNgayCong.Day);
					cio_ca1.ThuocNgayCong = CIO.ThuocNgayCong.AddDays(1d);
					ds_CIO_A[i] = cio_ca3; //TinhPC150 = macdinh_tinhPC50, //[140615_2]

					// vì hàm insert ko cho phép chèn ở vị trí > số lượng phần tử
					// => nên nếu i là phần tử cuối thì add vào cuối danh sách, ngược lại thì insert vào vị trí i+1
					if (i == (ds_CIO_A.Count - 1)) ds_CIO_A.Add(cio_ca1);
					else ds_CIO_A.Insert(i + 1, cio_ca1);
					i = i + 2; // +2 vì i là ThuocCa_Ca3, i+1 là ThuocCa 1
				}
				else i++;
			}
		}

		public static void XetCa_1_CIO_A5(cCheckInOut CIO, cShiftSchedule lichtrinh,
			out DateTime ThuocNgayCong, out cCa ThuocCa, out bool IsTachCaDem, out List<cCa> DSCa_KV_KR,
			out cCheck CheckRaa3, out cCheck CheckVao1, out cCheckInOut CIO_Ca3, out cCheckInOut CIO_Ca1, out cCa ThuocCa_Ca3, out cCa ThuocCa_Ca1) {
			IsTachCaDem = false;
			DSCa_KV_KR = null;
			CheckRaa3 = null; CheckVao1 = null;
			CIO_Ca3 = null; CIO_Ca1 = null;
			ThuocCa = null; ThuocCa_Ca3 = null; ThuocCa_Ca1 = null;
			#region nếu giờ quên check thì chỉ kiểm tra khoảng hiểu ThuocCa

			int index;
			ThuocNgayCong = new DateTime(CIO.TimeDaiDien.Year, CIO.TimeDaiDien.Month, CIO.TimeDaiDien.Day);
			index = MyUtility.GetIndexOf_DayInWeek(ThuocNgayCong);
			if (CIO.HaveINOUT < 0) {
				DSCa_KV_KR = Tim_DSCa_NhanDienDuoc(CIO.TimeDaiDien, ThuocNgayCong, CIO.HaveINOUT, lichtrinh.DSCaThu[index]); // nếu ko tìm đc thì bằng null
				return;
			}

			#endregion
			ThuocCa = KiemtraThuocCa(CIO.Vao.Time, CIO.Raa.Time, ThuocNgayCong, lichtrinh.DSCaThu[index]);

			#region nếu thuộc khoảng hiểu ThuocCa thì set ThuocCa

			if (ThuocCa != null) {
				if (ThuocCa.TachCaDem) {
					//tbd thêm điều kiện ThuocCa.QuaDem
					ThuocCa_Ca3 = ThuocCa.catruoc;
					ThuocCa_Ca1 = ThuocCa.casauuu;

					#region check inn, check out vao 3 ra 3, vao 1 ra 1

					var vaoca3 = CIO.Vao;
					CheckRaa3 = new cCheck {
						ID = int.MinValue, Type = "O", MachineNo = 22, Source = "PC", Time = ThuocNgayCong.Add(ThuocCa_Ca3.Duty.Off), MaCC = CIO.Vao.MaCC,
					}; // đáng lẽ IDgiờgốc là -1 vì giờ này mới thêm chưa bị sửa nhưng do đây là giờ đệm ko có trong csdl nên để max, ko cho update
					CheckVao1 = new cCheck {
						ID = int.MinValue, Type = "I", MachineNo = 21, Source = "PC", Time = ThuocNgayCong.AddDays(1d).Date.Add(ThuocCa_Ca1.Duty.Onn).Add(XL2._01giay), MaCC = CIO.Raa.MaCC,
					}; // đáng lẽ IDgiờgốc là -1 vì giờ này mới thêm chưa bị sửa nhưng do đây là giờ đệm ko có trong csdl nên để max, ko cho update
					var raaca1 = CIO.Raa;

					#endregion

					CIO_Ca3 = new cCheckInOut { Vao = vaoca3, Raa = CheckRaa3, TimeDaiDien = vaoca3.Time, }; //TinhPC150 = macdinh_tinhPC50, //[140615_2]
					CIO_Ca1 = new cCheckInOut { Vao = CheckVao1, Raa = raaca1, TimeDaiDien = CheckVao1.Time, }; //TinhPC150 = macdinh_tinhPC50, //[140615_2]
				}
			}
			#endregion
			else TaoCaTuDo(CIO.Vao.Time, int.MinValue, out ThuocCa);

		}

		public static void XetCa_1_CIO_A_KoTachCa(cCheckInOut CIO, cShiftSchedule lichtrinh) { }


		public static cCa KiemtraThuocCa(DateTime t_vao, DateTime t_raa, DateTime ngay, List<cCa> dsCa) {
			return dsCa.FirstOrDefault(ca => t_vao >= ngay.Add(ca.NhanDienVao.Onn) && t_vao <= ngay.Add(ca.NhanDienVao.Off)
											 && t_raa >= ngay.Add(ca.NhanDienRaa.Onn) && t_raa <= ngay.Add(ca.NhanDienRaa.Off));
		}

		public static bool KiemtraThuocCa(DateTime t_vao, DateTime t_raa, DateTime ngay, cCa ca) {
			return (t_vao >= ngay.Add(ca.NhanDienVao.Onn) && t_vao <= ngay.Add(ca.NhanDienVao.Off)
											 && t_raa >= ngay.Add(ca.NhanDienRaa.Onn) && t_raa <= ngay.Add(ca.NhanDienRaa.Off));
		}

		public static List<cCa> Tim_DSCa_NhanDienDuoc(DateTime time, DateTime ngay, int HaveINOUT, List<cCa> dsCa) {
			var kq = (HaveINOUT == -1)
						 ? dsCa.FindAll(ca => time >= ngay.Add(ca.NhanDienVao.Onn) && time <= ngay.Add(ca.NhanDienVao.Off))
						 : dsCa.FindAll(ca => time >= ngay.Add(ca.NhanDienRaa.Onn) && time <= ngay.Add(ca.NhanDienRaa.Off));
			return kq;
		}



		public static void XetCa_ListCIO_V4_5(List<cCheckInOut> ds_CIO_V) {
			foreach (cCheckInOut chkInOutV in ds_CIO_V) {
				XetCa_1_CIO_V_5(chkInOutV, out chkInOutV.ThuocNgayCong, out chkInOutV.ThuocCa);
			}

		}

		public static void XetCa_1_CIO_V(cCheckInOut chkInOutV, cShiftSchedule lichtrinh) { }

		public static void XetCa_1_CIO_V_5(cCheckInOut chkInOutV,
			out DateTime ThuocNgayCong, out cCa ThuocCa) {
			ThuocCa = new cCa();
			ThuocNgayCong = new DateTime(chkInOutV.TimeDaiDien.Year, chkInOutV.TimeDaiDien.Month, chkInOutV.TimeDaiDien.Day);
			var shiftID = chkInOutV.ShiftID;
			if (shiftID > 0) {
				// tìm trong ds ThuocCa chuẩn có trong lịch trình trước
				ThuocCa = (XL.DSCa.FirstOrDefault(item => item.ID == shiftID))
					?? XL.DSCaMoRong.FirstOrDefault(item => item.ID == shiftID);
			}
			else if (shiftID < Int32.MinValue + 100) // ThuocCa tự do (8 tiếng, ThuocCa dài 12 tiếng)
			{
				ThuocCa = new cCa { ID = shiftID, Is_CaTuDo = true };
				TaoCaTuDo(ThuocCa, chkInOutV.Vao.Time);
			}
		}

		public static void TronDS_CIO_A_V5(List<cCheckInOut> dsCIO_A, List<cCheckInOut> dsCIO_V, out List<cCheckInOut> kq) {
			kq = new List<cCheckInOut>();
			kq.AddRange(dsCIO_A);
			kq.AddRange(dsCIO_V);
			kq.Sort(new cCheckInOutComparer());
		}

		public static void TinhTG_LV_LVCa3_LamThem1Ca(DateTime ThuocNgayCong, int HaveINOUT, Boolean DaXN,
			bool KoTruVaoTre, bool KoTruRaaSom,
			//bool VaotreTinhCV, bool RaaSomTinhCV, //ver 4.0.0.4	
			DateTime Vao, DateTime Raa,
			TimeSpan DutyOnn, TimeSpan DutyOff, TimeSpan chophepTreTS, TimeSpan chophepSomTS, TimeSpan batdaulamthemTS,
			TimeSpan LunchMin, TimeSpan SoPhutLamThem,
			TimeSpan startNT, TimeSpan endddNT, //ver 4.0.0.4
			out DateTime TD_BD_LV, out DateTime TD_KT_LV, out DateTime TD_KT_LV_TrongCa,
			out DateTime TD_BD_LV_Ca3, out DateTime TD_KT_LV_Ca3,
			out TimeSpan TGThucTe, out TimeSpan TGGioLamViec, out TimeSpan TGVaoTre, out TimeSpan TGRaaSom,
			out TimeSpan TGGioLamViecTrongCa, //ver 4.0.0.4	
			out TimeSpan TGOLai, out TimeSpan TGLamThem, out bool QuaDem, out TimeSpan TGLamBanDem) {

			#region khởi tạo biến

			TD_BD_LV = DateTime.MinValue;
			TD_KT_LV_TrongCa = DateTime.MinValue; // chưa cộng OT
			TD_KT_LV = DateTime.MinValue; // đã cộng OT
			TD_BD_LV_Ca3 = DateTime.MinValue;
			TD_KT_LV_Ca3 = DateTime.MinValue;
			TGThucTe = TimeSpan.Zero;
			TGVaoTre = TimeSpan.Zero;
			TGRaaSom = TimeSpan.Zero;
			TGOLai = TimeSpan.Zero;
			TGGioLamViec = TimeSpan.Zero; // tổng thời gian làm việc đã gồm OT
			TGGioLamViecTrongCa = TimeSpan.Zero; //ver 4.0.0.4	
			TGLamThem = TimeSpan.Zero;
			TGLamBanDem = TimeSpan.Zero;
			QuaDem = false;

			#endregion

			if (HaveINOUT < 0) return;

			var TD_BD_Ca = ThuocNgayCong.Add(DutyOnn);
			var TD_KT_Ca = ThuocNgayCong.Add(DutyOff);//off duty này đã bao gồm daycount được công bên trong
			var thoidiem_BD_tinhtre = ThuocNgayCong.Add(chophepTreTS);
			var thoidiem_BD_tinhsom = ThuocNgayCong.Add(chophepSomTS);
			var thoidiem_BD_tinhOLai = ThuocNgayCong.Add(batdaulamthemTS);
			var tmpBDLamDem = ThuocNgayCong.Add(startNT);//ver 4.0.0.4
			var tmpKTLamDem = ThuocNgayCong.AddDays(1d).Add(endddNT);//ver 4.0.0.4

			bool quadem;

			TGThucTe = Raa - Vao;
			// kiểm tra giờ ra ko được nhỏ hơn vào ThuocCa, giờ vào ko được nhỏ hơn ra ThuocCa
			if (Raa < TD_BD_Ca || Vao > TD_KT_Ca) {
				return;
			}
			XL.Vao(Vao, TD_BD_Ca, thoidiem_BD_tinhtre, KoTruVaoTre, out TD_BD_LV, out TGVaoTre);
			XL.Raa(Raa, TD_KT_Ca, thoidiem_BD_tinhsom, KoTruRaaSom, out TD_KT_LV_TrongCa, out TGRaaSom);
			XL.OLai(Raa, TD_KT_Ca, thoidiem_BD_tinhOLai, out TGOLai);
			Tinh_TGLamViecTrongCa(TD_BD_LV, TD_KT_LV_TrongCa, LunchMin, out TGGioLamViecTrongCa);
			TD_KT_LV = TD_KT_LV_TrongCa; // giờ chưa qua xác nhận thì kt làm việc là chưa tính OT
			if (DaXN) TD_KT_LV = TD_KT_LV_TrongCa.Add(SoPhutLamThem);

			Tinh_TGLamViec(TD_BD_LV, TD_KT_LV, LunchMin, out TGGioLamViec);// lúc này TD_KT_LV là kết thúc ThuocCa nếu chưa XN, nếu đã XN thì = (TD_KT_LV + khoảng OT)
			TGGioLamViec = TGGioLamViecTrongCa + SoPhutLamThem;//ver 4.0.0.4	
			Tinh_TGLamViec_Ca3(TD_BD_LV, TD_KT_LV, tmpBDLamDem, tmpKTLamDem, out TD_BD_LV_Ca3, out  TD_KT_LV_Ca3, out TGLamBanDem, out quadem);
			TGLamThem = Tinh_TGLamThem(TGGioLamViec);//(TGGioLamViec - XL2._08gio) >= XL2._01phut ? (TGGioLamViec - XL2._08gio) : TimeSpan.Zero;
			QuaDem = quadem;
		}



		public static void TinhCong_ListNgayCong8(List<cNgayCong> dsNgayCong, TimeSpan startNT, TimeSpan endddNT) {
			foreach (var ngayCong in dsNgayCong) {
				TinhCong_HangNgay(ngayCong, startNT, endddNT, out ngayCong.TG, out ngayCong.PhuCaps, out ngayCong.TongCong, out ngayCong.TongNgayLV, out ngayCong.QuaDem);
			}
		}

		public static void TinhCong_HangNgay(cNgayCong ngayCong, TimeSpan startNT, TimeSpan endddNT,
			out ThoiGian TG, out PhuCap PhuCaps, out float TongCong, out float TongNgayLV, out bool QuaDem) {
			TG = new ThoiGian();
			PhuCaps = new PhuCap();
			TongCong = 0f;
			TongNgayLV = 0f; //ver4.0.0.1
			QuaDem = false;
			ngayCong.TrangThaiDiemDanh = TrangThaiDiemDanh.VANG_NGHI;
			// tính công của từng ThuocCa làm việc, sau đó tổng hợp Công làm việc của 1 ngày
			if (ngayCong.DSVaoRa.Count == 0) return;
			foreach (var CIO in ngayCong.DSVaoRa) {
				CIO.TD = new ThoiDiem();
				CIO.TG = new ThoiGian();
				TinhTG_LV_LVCa3_LamThem1Ca(CIO.ThuocNgayCong, CIO.HaveINOUT, CIO.DaXN, CIO.DuyetChoPhepVaoTre, CIO.DuyetChoPhepRaSom, //CIO.VaoTreTinhCV, CIO.RaaSomTinhCV, 
					CIO.Vao.Time, CIO.Raa.Time, CIO.ThuocCa.Duty.Onn, CIO.ThuocCa.Duty.Off, CIO.ThuocCa.chophepTreTS, CIO.ThuocCa.chophepSomTS,
					CIO.ThuocCa.batdaulamthemTS, CIO.ThuocCa.LunchMin, new TimeSpan(0, CIO.OTMin, 0), startNT, endddNT,
					out CIO.TD.BD_LV, out CIO.TD.KT_LV, out CIO.TD.KT_LV_ChuaOT, out CIO.TD.BD_LV_Ca3, out CIO.TD.KT_LV_Ca3,
				out CIO.TG.GioThucTe5, out CIO.TG.GioLamViec5, out CIO.TG.VaoTre, out CIO.TG.RaaSom,
				out CIO.TG.GioLVTrongCa5,//ver 4.0.0.4	
				out CIO.TG.OLai, out CIO.TG.LamTangCuong, out CIO.QuaDem, out CIO.TG.LamBanDem);
				//if (CIO.QuaDem) QuaDem = true; // set qua đêm nếu có
				float cong_trong_ca = Convert.ToSingle(Math.Round(((CIO.TG.GioLVTrongCa5.TotalHours / CIO.ThuocCa.WorkingTimeTS.TotalHours) * CIO.ThuocCa.Workingday), 2));
				float cong_bi_tru_TreSom = CIO.ThuocCa.Workingday - cong_trong_ca;
				float cong_ngoai_ca = Convert.ToSingle(Math.Round((CIO.TG.SoPhutLamThem5.TotalHours / 8f), 2));// tương đương giờ làm việc ngoài ThuocCa, làm ngoài ThuocCa chưa chắc OT ví dụ nửa ThuocCa
				CIO.Cong = cong_trong_ca + cong_ngoai_ca;
				TG.GioThucTe5 += CIO.TG.GioThucTe5;
				TG.GioLamViec5 += CIO.TG.GioLamViec5;
				TG.GioLVTrongCa5 += CIO.TG.GioLVTrongCa5;
				TG.LamBanDem += CIO.TG.LamBanDem;
				TG.VaoTre += CIO.TG.VaoTre;
				TG.RaaSom += CIO.TG.RaaSom;
				TongCong += CIO.Cong; //công đã được làm tròn 2 số thập phân ở trên
				if ((CIO.DuyetChoPhepVaoTre && CIO.DuyetChoPhepRaSom)
					|| (CIO.DuyetChoPhepVaoTre == false && CIO.DuyetChoPhepRaSom == false && CIO.VaoTreTinhCV == false && CIO.RaaSomTinhCV == false)) {
					ngayCong.TongNgayLV += CIO.ThuocCa.Workingday;
				}
				else {
					if (CIO.DuyetChoPhepVaoTre == false && CIO.VaoTreTinhCV)
						ngayCong.TongNgayLV += cong_trong_ca;
					else if (CIO.DuyetChoPhepRaSom == false && CIO.RaaSomTinhCV)
						ngayCong.TongNgayLV += cong_trong_ca;
					else ngayCong.TongNgayLV += CIO.ThuocCa.Workingday;
				}
				ngayCong.TongNgayLV += cong_ngoai_ca;
				//ngayCong.TongNgayLV += (CIO.Cong > CIO.ThuocCa.Workingday) ? CIO.Cong : CIO.ThuocCa.Workingday;//ver4.0.0.1
			}
			ngayCong.TG.LamTangCuong = Tinh_TGLamThem(ngayCong.TG.GioLamViec5);// (ngayCong.TG.GioLamViec - XL2._08gio > XL2._01phut) ? ngayCong.TG.GioLamViec - XL2._08gio : TimeSpan.Zero;			
			ngayCong.PhuCaps._30_dem = Convert.ToSingle(Math.Round((ngayCong.TG.LamBanDem.TotalHours / 8d) * (XL2.PC30 / 100f), 2, MidpointRounding.ToEven));
			ngayCong.PhuCaps._TongPC = ngayCong.PhuCaps._30_dem;
		}

		public static void TinhCong_1Ngay5(cNgayCong ngayCong, TimeSpan startNT, TimeSpan endddNT,

			out ThoiGian TG, out PhuCap PhuCaps, out float TongCong, out float TongNgayLV, out bool QuaDem) {
			TG = new ThoiGian();
			PhuCaps = new PhuCap();
			TongCong = 0f;
			TongNgayLV = 0f; //ver4.0.0.1
			QuaDem = false;
			ngayCong.TrangThaiDiemDanh = TrangThaiDiemDanh.VANG_NGHI;
			// tính công của từng ThuocCa làm việc, sau đó tổng hợp Công làm việc của 1 ngày
			if (ngayCong.DSVaoRa.Count == 0) return;
			foreach (var CIO in ngayCong.DSVaoRa) {
				CIO.TD = new ThoiDiem();
				CIO.TG = new ThoiGian();
				TinhTG_LV_LVCa3_LamThem1Ca(CIO.ThuocNgayCong, CIO.HaveINOUT, CIO.DaXN, CIO.DuyetChoPhepVaoTre, CIO.DuyetChoPhepRaSom, //CIO.VaoTreTinhCV, CIO.RaaSomTinhCV, 
					CIO.Vao.Time, CIO.Raa.Time, CIO.ThuocCa.Duty.Onn, CIO.ThuocCa.Duty.Off, CIO.ThuocCa.chophepTreTS, CIO.ThuocCa.chophepSomTS,
					CIO.ThuocCa.batdaulamthemTS, CIO.ThuocCa.LunchMin, new TimeSpan(0, CIO.OTMin, 0), startNT, endddNT,
					out CIO.TD.BD_LV, out CIO.TD.KT_LV, out CIO.TD.KT_LV_ChuaOT, out CIO.TD.BD_LV_Ca3, out CIO.TD.KT_LV_Ca3,
					out CIO.TG.GioThucTe5, out CIO.TG.GioLamViec5, out CIO.TG.VaoTre, out CIO.TG.RaaSom,
					out CIO.TG.GioLVTrongCa5,//ver 4.0.0.4	
					out CIO.TG.OLai, out CIO.TG.LamTangCuong, out CIO.QuaDem, out CIO.TG.LamBanDem);

				TinhCong_1_CIO_5(CIO.ThuocCa.Workingday, CIO.ThuocCa.WorkingTimeTS,CIO.TG.VaoTre, CIO.TG.RaaSom, 
					CIO.VaoTreTinhCV, CIO.RaaSomTinhCV,CIO.TG.SoPhutLamThem5,
								 out CIO.Cong5.CaQuyDinh, out CIO.Cong5.TTCongTre, out CIO.Cong5.TTCongSom, 
								 out CIO.Cong5.TTTrongCa, out CIO.Cong5.TTNgoaiCa,out CIO.Cong5.ThucTe, 
								 out CIO.Cong5.TongCongBu, out CIO.Cong5.TongCongTru, out CIO.Cong5.DinhMuc);

				TG.GioThucTe5 += CIO.TG.GioThucTe5;
				TG.GioLamViec5 += CIO.TG.GioLamViec5;
				TG.GioLVTrongCa5 += CIO.TG.GioLVTrongCa5;
				TG.LamBanDem += CIO.TG.LamBanDem;
				TG.VaoTre += CIO.TG.VaoTre;
				TG.RaaSom += CIO.TG.RaaSom;
				TongCong += CIO.Cong; //công đã được làm tròn 2 số thập phân ở trên

			}
		}

		public static float TinhCongTre5(TimeSpan GioLam_CaQuyDinh, TimeSpan GioVaoTre, int LamTronConSo) {
			return ((GioLam_CaQuyDinh.TotalHours - GioVaoTre.TotalHours) / GioLam_CaQuyDinh.TotalHours).Truncate(LamTronConSo); // làm tròn xuống 2 con số
		}
		public static float TinhCongSom5(TimeSpan GioLam_CaQuyDinh, TimeSpan GioRaaSom, int LamTronConSo) {
			return ((GioLam_CaQuyDinh.TotalHours - GioRaaSom.TotalHours) / GioLam_CaQuyDinh.TotalHours).Truncate(LamTronConSo); // làm tròn xuống 2 con số
		}
		public static float TinhCongThucTeTrongCa(float CongCaQuyDinh, float CongTre, float CongSom) {
			return CongCaQuyDinh - CongTre - CongSom; // làm tròn xuống 2 con số
		}
		public static void TinhCong_1_CIO_5(float Cong_DoCaQuyDinh,TimeSpan GioLam_CaQuyDinh, TimeSpan GioVaoTre, TimeSpan GioRaaSom,
			bool VaoTreTinhCV, bool RaaSomTinhCV,TimeSpan SoPhutLamThem5,
			out float CongCaQuyDinh, out float CongTre, out float CongSom, 
			out float CongThucTeTrongCa, out float CongThucTeNgoaiCa,out float CongThucTe, 
			out float TongCongBu, out float TongCongTru, out float DinhMucCong) {
			CongCaQuyDinh = Cong_DoCaQuyDinh;
			CongTre = XL.TinhCongTre5(GioLam_CaQuyDinh, GioVaoTre, 2);// làm tròn xuống 2 con số
			CongSom = XL.TinhCongSom5(GioLam_CaQuyDinh, GioRaaSom, 2);// làm tròn xuống 2 con số
			CongThucTeTrongCa = XL.TinhCongThucTeTrongCa(CongCaQuyDinh, CongTre, CongSom);
			CongThucTeNgoaiCa = Convert.ToSingle(Math.Round((SoPhutLamThem5.TotalHours / 8f), 2));// tương đương giờ làm việc ngoài ThuocCa, làm ngoài ThuocCa chưa chắc OT ví dụ nửa ThuocCa
			CongThucTe = CongThucTeTrongCa + CongThucTeNgoaiCa;
			DinhMucCong = CongThucTe;
			//nếu đã duyệt cho phép vào trễ rồi thì giờ vào trễ = 0, công vào trễ =0
			TongCongBu = 0f;
			TongCongTru = 0f;
			if (VaoTreTinhCV) TongCongBu += CongTre;
			else TongCongTru += CongTre;

			if (RaaSomTinhCV) TongCongBu += CongSom;
			else TongCongTru += CongSom;


			
			if (!VaoTreTinhCV) {
				CongBu += CongTre;
				DinhMucCong += CongTre;
			}
			if (!RaaSomTinhCV) {
				CongBu += CongSom;
				DinhMucCong += CongSom;
			}

		}

		public static void TinhPCTC_TrongListXNPCTC9(List<structPCTC> dsXacNhanPC, List<cNgayCong> dsNgayCong) {
			foreach (var item in dsXacNhanPC) {
				var ngayCong = dsNgayCong.Find(o => o.Ngay == item.Ngay);
				TinhPCTC_CuaNgay(ngayCong, item.TinhPC50);
			}

		}

		public static void TinhPCTC_CuaNgay(cNgayCong ngayCong, bool choPhepTinhTC) {
			ngayCong.TinhPC50 = choPhepTinhTC;
			Tinh_PCTC(choPhepTinhTC, ngayCong.QuaDem, ngayCong.TG.LamBanDem, ngayCong.TG.LamTangCuong,
							   out ngayCong.TG.Tinh130, out ngayCong.TG.Tinh150, out ngayCong.TG.TinhTCC3,
							   out ngayCong.PhuCaps._30_dem, out ngayCong.PhuCaps._50_TC, out ngayCong.PhuCaps._100_TCC3, out ngayCong.PhuCaps._TongPC);
		}

		public static void TinhPCTC_CuaNgay(cNgayCong ngayCong, List<structPCTC> DSXNPhuCap50) {
			var IndexngayTinhLaiPCTC1 = DSXNPhuCap50.FindIndex(item => item.Ngay == ngayCong.Ngay);
			var kq1 = (IndexngayTinhLaiPCTC1 >= 0) && (DSXNPhuCap50[IndexngayTinhLaiPCTC1].TinhPC50);
			TinhPCTC_CuaNgay(ngayCong, kq1);
		}

		public static void TinhPCDB_TrongListXNPCDB10(List<structPCDB> dsXacNhanPC, List<cNgayCong> dsNgayCong) {
			foreach (var item in dsXacNhanPC) {
				var ngayCong = dsNgayCong.Find(o => o.Ngay == item.Ngay);

				ngayCong.TinhPCDB = true;
				ngayCong.TG.Tinh130 = TimeSpan.Zero;
				ngayCong.TG.Tinh150 = TimeSpan.Zero;
				ngayCong.TG.TinhTCC3 = TimeSpan.Zero;
				ngayCong.PhuCaps._30_dem = 0f;
				ngayCong.PhuCaps._50_TC = 0f;
				ngayCong.PhuCaps._100_TCC3 = 0f;
				TinhPCDB_CuaNgay(ngayCong, item.LoaiPC, item.PCNgay, item.PCDem);
			}
		}

		public static void TinhPCDB_CuaNgay(cNgayCong ngayCong, int loaiPCDB, float PCNgay, float PCDem) {
			ngayCong.TinhPCDB = true;
			ngayCong.LoaiPCDB = loaiPCDB;
			TinhPCDB(ngayCong.TinhPCDB, ngayCong.TG.GioLamViec5, ngayCong.TG.LamBanDem, ngayCong.QuaDem, ngayCong.LoaiPCDB, PCNgay, PCDem, //tbd
		 out ngayCong.TG.Tinh200, out ngayCong.TG.Tinh260, out ngayCong.TG.Tinh300, out ngayCong.TG.Tinh390, out ngayCong.TG.TinhPCCus,
		 out ngayCong.PhuCaps._100_LVNN_Ngay, out ngayCong.PhuCaps._150_LVNN_Dem, out ngayCong.PhuCaps._200_LeTet_Ngay, out ngayCong.PhuCaps._250_LeTet_Dem, out ngayCong.PhuCaps._Cus, ref ngayCong.PhuCaps._TongPC);

		}

		public static void TinhPCDB_CuaNgay(cNgayCong ngayCong, List<structPCDB> DSXNPCDB) {
			var ngayTinhLaiPCDB = DSXNPCDB.FindIndex(item => item.Ngay == ngayCong.Ngay);
			if (ngayTinhLaiPCDB >= 0)
				TinhPCDB_CuaNgay(ngayCong, DSXNPCDB[ngayTinhLaiPCDB].LoaiPC, DSXNPCDB[ngayTinhLaiPCDB].PCNgay, DSXNPCDB[ngayTinhLaiPCDB].PCDem);
		}


		public static void ThemGioChoNV(int MaCC, cCheck check, List<cCheck> DS_Check_A, string pLydo, string pGhichu) {
			//if (check.Time.Date <= XL2.NgayCuoiThangKetCong && XL2.NgayCuoiThangKetCong != DateTime.MinValue) return; //tbd temp patch

			if (DAO5.ThemGioChoNV(MaCC, check.Time, check.Type, check.MachineNo, pLydo, pGhichu)) {
				DS_Check_A.Add(check);
				DS_Check_A.Sort(new cCheckComparer());
			}
		}

		public static bool XoaGioChoNV(int MaCC, cCheck check, List<cCheck> DS_Check_A, string lydo, string ghichu) {
			//if (check.Time.Date <= XL2.NgayCuoiThangKetCong && XL2.NgayCuoiThangKetCong != DateTime.MinValue) return false; //tbd temp patch

			if (DAO5.XoaGioChoNV(MaCC, check.Time, check.Source, check.MachineNo, lydo, ghichu)) {
				var indexCheck = DS_Check_A.FindIndex(o => o.Time == check.Time && o.MachineNo == check.MachineNo && o.Source == check.Source);

				if (indexCheck < 0) return false;
				var prevCheck = (indexCheck == 0) ? null : DS_Check_A[indexCheck - 1];
				var nextCheck = (indexCheck == DS_Check_A.Count - 1) ? null : DS_Check_A[indexCheck + 1];
				if (prevCheck != null && (check.Time - prevCheck.Time).Duration() < XL2._1ngay) prevCheck.IsEdited += 1;
				if (nextCheck != null && (nextCheck.Time - check.Time).Duration() < XL2._1ngay) nextCheck.IsEdited += 1;

				DS_Check_A.Remove(check);

			}
			return true;

		}

		public static void SuaGioChoNV(int MaCC, cCheck checkold, cCheck checknew, List<cCheck> DS_Check_A, string lydo, string ghichu) {

			if (DAO5.SuaGioChoNV(MaCC, checkold.Time, checknew.Time, checkold.Source, checknew.Source, checkold.MachineNo, checknew.MachineNo, checkold.PhucHoi.IDGioGoc, lydo, ghichu)) {
				DS_Check_A.Remove(checkold);
				DS_Check_A.Add(checknew);
				DS_Check_A.Sort(new cCheckComparer());
			}
		}

		public static void ThemNgayVang(IEnumerable<dynamic> list, float workingDay, float workingTime, string absentCode, List<Error> listError) {
			foreach (dynamic obj in list) {
				bool kqKiemTraMauThuan = XL.KiemtraCoMauThuanLoaiVang(absentCode, obj.NgayVang, listError);
				if (kqKiemTraMauThuan) continue;// nếu bị mâu thuẫn loại vắng thì chuyển qua cái kế tiếp
				var ngayVang = (DateTime)obj.NgayVang;
				//BH dài ngày vào chủ nhật, Ro dài ngày vào chủ nhật thì set workingday về 0
				if ((absentCode == "BD" || absentCode == "TS") && (ngayVang).DayOfWeek == DayOfWeek.Sunday) {
					var NewWKDay = 0f;
					DAO5.ThemNgayVang(obj.MaCC, ngayVang, NewWKDay, workingTime, absentCode);
				}
				else {
					DAO5.ThemNgayVang(obj.MaCC, ngayVang, workingDay, workingTime, absentCode);
				}
			}
		}

		private static bool KiemtraCoMauThuanLoaiVang(string absentCode, DateTime ngayVang, List<Error> listError) {
			bool kq = false;
			switch (absentCode) {
				#region bh

				case "BH":
					//chủ nhật ko tính BH ngắn ngày 
					if (ngayVang.DayOfWeek == DayOfWeek.Sunday) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày chủ nhật {1}", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					// bh ko di chung TS
					if (DAO5.KiemtraTonTaiLoaiVang("TS", ngayVang)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang vắng thai sản", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					break;
				case "BD": // bh ko di chung TS
					if (DAO5.KiemtraTonTaiLoaiVang("TS", ngayVang)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang vắng thai sản", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					break;

				#endregion

				#region ro H CT PT PD P   ko di chung TS

				case "RO":
				case "H":
				case "CT":
				case "PT":
				case "PD":
				case "P": // ro H CT PT PD P   ko di chung TS
					if (DAO5.KiemtraTonTaiLoaiVang("TS", ngayVang)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang vắng thai sản", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					break;

				#endregion

				#region ts ko đi chung với tất cả các loại khác

				case "TS": //ts  ko cho phép chủ nhật
					if (DAO5.KiemtraTonTaiLoaiVang("TS", ngayVang)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang vắng thai sản ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO5.KiemtraTonTaiLoaiVang("BH", ngayVang)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang vắng bảo hiểm ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO5.KiemtraTonTaiLoaiVang("BD", ngayVang)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang vắng bảo hiểm ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO5.KiemtraTonTaiLoaiVang("RO", ngayVang)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang vắng việc riêng ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO5.KiemtraTonTaiLoaiVang("H", ngayVang)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang họp, học ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO5.KiemtraTonTaiLoaiVang("CT", ngayVang)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang công tác ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO5.KiemtraTonTaiLoaiVang("PT", ngayVang)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} phong trào ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO5.KiemtraTonTaiLoaiVang("PD", ngayVang)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} phong trào ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO5.KiemtraTonTaiLoaiVang("P", ngayVang)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang vắng phép ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					break;

				#endregion
			}
			return kq;
		}




		internal static void LayBang_PH_Them(List<cUserInfo> listNV, DateTime ngayBD, DateTime ngayKT, ref DataTable Bang_PH_Them) {

			var arrMaCC = (from nv in listNV select nv.MaCC).ToArray();
			Bang_PH_Them = DAO5.LayBang_PH_Them(arrMaCC, ngayBD, ngayKT, Bang_PH_Them);
		}

		internal static void LayBang_PH_Xoaa(List<cUserInfo> listNV, DateTime ngayBD, DateTime ngayKT, ref DataTable m_Bang_PH_Xoaa) {
			var arrMaCC = (from nv in listNV select nv.MaCC).ToArray();
			m_Bang_PH_Xoaa = DAO5.LayBang_PH_Xoa(arrMaCC, ngayBD, ngayKT, m_Bang_PH_Xoaa);
		}

		internal static void LayBang_PH_GioGoc(List<cUserInfo> listNV, DateTime ngayBD, DateTime ngayKT, ref DataTable m_Bang_PH_GioGoc) {
			var arrMaCC = (from nv in listNV select nv.MaCC).ToArray();
			m_Bang_PH_GioGoc = DAO5.LayBang_PH_GioGoc(arrMaCC, ngayBD, ngayKT, m_Bang_PH_GioGoc);
		}

		internal static void LayBang_GioDaXN(List<cUserInfo> listNV, DateTime ngayBD, DateTime ngayKT, ref DataTable m_Bang_GioDaXN) {
			m_Bang_GioDaXN.Rows.Clear();
			var arrMaCC = (from nv in listNV select nv.MaCC).ToArray();
			var table01 = DAO5.LayBang_GioDaXN2(arrMaCC, ngayBD, ngayKT, m_Bang_GioDaXN);
			if (table01.Rows.Count == 0) { return; }
			var arrID = (from row in table01.Rows.Cast<DataRow>() let id = (int)row["IDXNCa_LamThem"] select id).ToArray();
			var table02 = DAO5.LayBang_GioDaXN3(arrID, m_Bang_GioDaXN);
			if (table02.Rows.Count == 0) { return; }
			foreach (DataRow rowInn in table01.Rows) {
				var UserFullName = rowInn["UserFullName"].ToString();
				var UserFullCode = rowInn["UserFullCode"].ToString();
				var UserEnrollNumber = (int)rowInn["UserEnrollNumber"];
				var TimeStrInn = (DateTime)rowInn["TimeStr"];
				var ID = (int)rowInn["IDXNCa_LamThem"];
				var arrRowOut = table02.Select("IDXNCa_LamThem=" + ID);
				if (arrRowOut.Length == 0) continue;
				var rowOut = arrRowOut[0];
				var TimeStrOut = (DateTime)rowOut["TimeStr"];
				var ShiftCode = rowOut["ShiftCode"] != DBNull.Value ? rowOut["ShiftCode"].ToString() : string.Empty;
				var ShiftID = (int)rowOut["ShiftID"];
				var OTMin = (int)rowOut["OTMin"];
				var duyetChoPhepVaoTre = (bool)rowOut["DuyetChoPhepVaoTre"];
				var duyetChoPhepRaSom = (bool)rowOut["DuyetChoPhepRaSom"];
				DataRow row = m_Bang_GioDaXN.NewRow();
				row["UserFullName"] = UserFullName;
				row["UserFullCode"] = UserFullCode;
				row["UserEnrollNumber"] = UserEnrollNumber;
				row["IDXNCa_LamThem"] = ID;
				row["TimeStrInn"] = TimeStrInn;
				row["TimeStrOut"] = TimeStrOut;
				row["ShiftCode"] = ShiftCode;
				row["ShiftID"] = ShiftID;
				row["OTMin"] = OTMin;
				row["DuyetChoPhepVaoTre"] = duyetChoPhepVaoTre;
				row["DuyetChoPhepRaSom"] = duyetChoPhepRaSom;
				m_Bang_GioDaXN.Rows.Add(row);
			}
		}

		internal static void HuyXN_GioChamCong(int ID) {
			DAO5.HuyXN_GioChamCong(ID);
		}
	}

}
