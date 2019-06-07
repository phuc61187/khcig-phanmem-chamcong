using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ChamCong_v04.DAL;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using log4net;
using log4net.Config;
using mySetting = ChamCong_v04.Properties.Settings;

namespace ChamCong_v04.BUS {
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
			return chkinn.TimeOfDay < XL2._03gio ? chkinn.Date.AddDays(-1d) : chkinn.Date;
		}

		public static void Vao(DateTime timeinn, DateTime onnduty, DateTime chopheptre, bool DuyetChoPhepVaoTre, bool VaoTreTinhCV, out DateTime td_batdau_lv, out TimeSpan treVR, out TimeSpan vaoSauCaKoTruVR) {
			treVR = TimeSpan.Zero;
			vaoSauCaKoTruVR = TimeSpan.Zero;
			if (DuyetChoPhepVaoTre) {
				td_batdau_lv = onnduty;
			}
			else
			{
				var timeinn0s = new DateTime(timeinn.Year, timeinn.Month, timeinn.Day, timeinn.Hour, timeinn.Minute, 0);
				if (VaoTreTinhCV)
				{
					if (chopheptre < timeinn0s)
					{
						td_batdau_lv = timeinn0s; 
						vaoSauCaKoTruVR = timeinn0s - onnduty;
					}
					else
					{
						td_batdau_lv = onnduty; 
					}
				}
				else if (chopheptre < timeinn0s)
				{
					td_batdau_lv = timeinn0s;
					treVR = timeinn0s - onnduty;
				}
				else {
					td_batdau_lv = onnduty;
				}
			}
		}

		public static void XetBuGioTre(bool BuGioTre, DateTime onnduty, ref DateTime td_batdau_lv, ref TimeSpan tre)
		{
			if (tre == TimeSpan.Zero) return;

			if (BuGioTre == false) return;

			if (tre <= XL2._02gio) //todo setting
			{
				td_batdau_lv = onnduty + XL2._02gio; //todo setting
				tre = XL2._02gio;//todo setting
			}
		}

		public static void XetBuPhepTre (bool BuPhepTre, float CongPhep, DateTime onnduty, ref float CongPhepCongDon, ref DateTime td_batdau_lv, ref TimeSpan tre)
		{
			if (tre == TimeSpan.Zero || BuPhepTre == false)
			{
				CongPhepCongDon = 0f;
				return;
			}

			var temp = onnduty + new TimeSpan(0, Convert.ToInt32((CongPhep * XL2._08gio.TotalMinutes)), 0);
			CongPhepCongDon = 0f + CongPhep;
			if (td_batdau_lv > temp)
			{
				tre = td_batdau_lv - temp;
			}
			else
			{
				td_batdau_lv = temp;
				tre = TimeSpan.Zero;
			}

		}


		public static void Raa(DateTime timeout, DateTime offduty, DateTime chophepsom, bool DuyetChoPhepRaSom, bool RaaSomTinhCV, out DateTime td_ketthuc_lv_chuaOT, out TimeSpan som, out TimeSpan raTruocCaKoTruVR) {
			som = TimeSpan.Zero;
			raTruocCaKoTruVR = TimeSpan.Zero;
			if (DuyetChoPhepRaSom) {
				td_ketthuc_lv_chuaOT = offduty;
			}
			else
			{
				var timeout0s = new DateTime(timeout.Year, timeout.Month, timeout.Day, timeout.Hour, timeout.Minute, 0);
				if (RaaSomTinhCV)
				{
					if (timeout0s < chophepsom)
					{
						td_ketthuc_lv_chuaOT = timeout0s;
						raTruocCaKoTruVR = offduty - timeout0s;
					}
					else
					{
						td_ketthuc_lv_chuaOT = offduty;
					}
				}
				else if (timeout0s < chophepsom) {
					td_ketthuc_lv_chuaOT = timeout0s;
					som = offduty - timeout0s;
				}
				else {
					td_ketthuc_lv_chuaOT = offduty;
				}

			}
		}

		public static void XetBuGioSom(bool BuGioSom, DateTime offduty, ref DateTime td_ketthuc_lv_chuaOT, ref TimeSpan som) {
			if (som == TimeSpan.Zero) return;

			if (BuGioSom == false) return;

			if (som <= XL2._02gio) //todo setting
			{
				td_ketthuc_lv_chuaOT = offduty - XL2._02gio; //todo setting
				som = XL2._02gio;//todo setting
			}
		}

		public static void XetBuPhepSom(bool BuPhepSom, float CongPhep, DateTime offduty, ref float CongPhepCongDon, ref DateTime td_ketthuc_lv_chuaOT, ref TimeSpan som) {
			if (som == TimeSpan.Zero || BuPhepSom == false) {
				CongPhepCongDon = 0f;
				return;
			}

			var temp = offduty - new TimeSpan(0, Convert.ToInt32((CongPhep * XL2._08gio.TotalMinutes)), 0);
			CongPhepCongDon = 0f + CongPhep;
			if (td_ketthuc_lv_chuaOT < temp) {
				som = temp - td_ketthuc_lv_chuaOT;
			}
			else {
				td_ketthuc_lv_chuaOT = temp;
				som = TimeSpan.Zero;
			}

		}

		public static void OLai(DateTime timeout, DateTime offduty, DateTime batdaulamthem, out TimeSpan olai) {
			var timeout0s = new DateTime(timeout.Year, timeout.Month, timeout.Day, timeout.Hour, timeout.Minute, 0);
			if (batdaulamthem < timeout0s) {
				olai = timeout0s - offduty;
			}
			else {
				olai = TimeSpan.Zero;
			}
		}

		public static void LamThem(DateTime td_ketthuc_lv_chuaOT, TimeSpan OTmin, out DateTime td_ketthuc_lc_DaCoOT) {
			td_ketthuc_lc_DaCoOT = td_ketthuc_lv_chuaOT + OTmin;
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
			else  // 1 ca đêm nhưng 0.5 ca ko tính tăng cường, nửa ca tính tăng cường đêm
			{
				TGLamTC_Dem = TGLamTC;
				TGLamDem_koTC = TGLamDem - TGLamTC;
			}
		}

		public static void Tinh_PCTC(bool TinhPC50, bool QuaDem, TimeSpan SoGioLamDemmm, TimeSpan SoGioLamThem, bool nvNhanKiet,
			out TimeSpan tgTinh130, out TimeSpan tgTinh150, out TimeSpan tgTinhTCC3,
			out float PhuCap30, out float PhuCapTC, out float PhuCapTCC3, out float TongPhuCap) {

			var heso_pctc = Convert.ToSingle(XL2.PC50) / 100f;
			var heso_pcdem = Convert.ToSingle(XL2.PC30) / 100f; //tbd
			var heso_pctcc3 = (Convert.ToSingle(XL2.PCTCC3)) / 100f;
            var heso_pctcc3NhanKiet = (Convert.ToSingle(XL2.PCTCC3NK)) / 100f;

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
                    if (nvNhanKiet) { PhuCapTCC3 = Convert.ToSingle(Math.Round((tgTinhTCC3.TotalHours / 8d) * heso_pctcc3NhanKiet, 2, MidpointRounding.ToEven)); }
                    else { PhuCapTCC3 = Convert.ToSingle(Math.Round((tgTinhTCC3.TotalHours / 8d) * heso_pctcc3, 2, MidpointRounding.ToEven)); }
					PhuCapTC = Convert.ToSingle(Math.Round((tgTinh150.TotalHours / 8d) * heso_pctc, 2, MidpointRounding.ToEven));
					PhuCap30 = Convert.ToSingle(Math.Round((tgTinh130.TotalHours / 8d) * heso_pcdem, 2, MidpointRounding.ToEven));
					TongPhuCap = PhuCap30 + PhuCapTC + PhuCapTCC3;

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
			if (CheckInTime.TimeOfDay < XL2._03gio) temp = Ca.Duty.Onn.Add(XL2._1ngay); //ca 3 , ca 3 va 1 vẫn giữ nguyên vì 21h > 4h//tbd xem lại ngày công
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
			if (CheckInTime.TimeOfDay < XL2._03gio) temp = Ca.Duty.Onn.Add(XL2._1ngay); //ca 3 , ca 3 va 1 vẫn giữ nguyên vì 21h > 4h//tbd xem lại ngày công
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
			dsnv.Clear();
			// lấy nv theo phòng ban, bỏ qua các nv ko có lịch trình, ko có phòng ban, ko enable
			// bao gồm cả nv công nhật
			if (m_listIDPhongBan.Count == 0) return dsnv;
			var table = XL.LayDSNV(true, m_listIDPhongBan.ToArray()); // lấy theo phòng ban các nv Enabled, bao gồm cả công nhật
			if (table.Rows.Count == 0) return dsnv;

			// chấm công thì bỏ qua các NV ko được phép, vẫn giữ công nhật, 
			dsnv.AddRange(from DataRow row in table.Rows
						  //where row["UserEnabled"] != DBNull.Value && (bool)row["UserEnabled"]
						  select
						  KhoiTaoNV((int)row["UserEnrollNumber"], (string)row["UserFullName"], (string)row["UserFullCode"],
						  null, null, null, null, null, null, null,
						  dsphong,
						  (row["SchID"] != DBNull.Value ? (int)row["SchID"] : -1),
						  (row["IDChucVu"] != DBNull.Value) ? (int)row["IDChucVu"] : -1,
						  (row["ChucVu"] != DBNull.Value) ? (string)row["ChucVu"] : null,
						  (row["MaPhong"] == DBNull.Value || (int)row["MaPhong"] == 0 || (int)row["MaPhong"] == -1) ? -1 : (int)row["MaPhong"], //info nếu mã phòng null hoặc 0 là chưa sắp xếp
                          (row["NVNhanKiet"] != DBNull.Value ? (bool)row["NVNhanKiet"] : false))); 
			return dsnv;
		}
		public static List<cUserInfo> KhoiTaoDSNV_TinhLuong(List<cUserInfo> dsnv, List<cPhongBan> dsphong) {
			dsnv.Clear();
			// lấy danh sách tất cả nhân viên trừ các nhân viên công nhật ko tính lương
            // note: bỏ qua nhân viên Nhân Kiệt
			DataTable tableDSNV = XL.LayDSNV(UserEnabled: true); //[20140510_7]
			if (tableDSNV.Rows.Count == 0) return dsnv;
			foreach (var nhanvien in from DataRow row in tableDSNV.Rows
									 select KhoiTaoNV((int)row["UserEnrollNumber"], (string)row["UserFullName"], (string)row["UserFullCode"],
														(row["HeSoLuongCB"] != DBNull.Value) ? (float)row["HeSoLuongCB"] : 0f,
														(row["HeSoLuongSP"] != DBNull.Value) ? (float)row["HeSoLuongSP"] : 0f,
														(row["HSBHCongThem"] != DBNull.Value) ? (float)row["HSBHCongThem"] : 0f,
                                                        (row["HSLCBTT17"] != DBNull.Value) ? (float)row["HSLCBTT17"] : 0f,
                                                        (row["HSPCTN"] != DBNull.Value) ? (float)row["HSPCTN"] : 0f,
                                                        (row["HSPCDH"] != DBNull.Value) ? (float)row["HSPCDH"] : 0f,
                                                        (row["HSPCCV"] != DBNull.Value) ? (float)row["HSPCCV"] : 0f,
                                                        dsphong,
														(row["SchID"] != DBNull.Value ? (int)row["SchID"] : (int?)null),
														(row["IDChucVu"] != DBNull.Value) ? (int)row["IDChucVu"] : (int?)null,
														(row["ChucVu"] != DBNull.Value) ? (string)row["ChucVu"] : null,
														(row["MaPhong"] == DBNull.Value || (int)row["MaPhong"] == 0 || (int)row["MaPhong"] == -1)
															? -1
															: (int)row["MaPhong"],//info nếu mã phòng null hoặc 0 là chưa sắp xếp
                                                        (row["NVNhanKiet"] != DBNull.Value) ? (bool)row["NVNhanKiet"] : false)) 
			{
				nhanvien.chiTietLuong = new ChiTietLuong();
				nhanvien.ThongKeThang = new ThongKeCong_PC();

				// tbd lưu ý chưa xét lương công nhật
                if (nhanvien.NVNhanKiet == false) dsnv.Add(nhanvien); // trừ nhân viên Nhân Kiệt không được add vào bảng Lương, các nhân viên còn lại add bình thường
                
			}
			return dsnv;
		}

		public static cUserInfo KhoiTaoNV(int uen, string ten, string manv,
			float? hslCB, float? hslCV, float? hsBHcongthem, float? hslCBTT17, float? hsPCTNTT17, float? hsPCDHTT17, float? hsPCCVTT17,
            List<cPhongBan> phongBans,
			int? schID = null, int? idChucVu = null, string ChucVu = null,
			int? maphong = null, bool? nvNhanKiet = null) {
			cUserInfo nhanvien = new cUserInfo {
				MaCC = uen, TenNV = ten, MaNV = manv,
				ChucVu = ChucVu,
				HeSo = new HeSo(),
			};
			if (hslCB != null) nhanvien.HeSo.LuongCB = (float)hslCB;
			if (hslCV != null) nhanvien.HeSo.LuongCV = (float)hslCV;
            nhanvien.HeSo.LCBTT17 = hslCBTT17 != null ? (float)hslCBTT17 : 0f;
            nhanvien.HeSo.PCTNTT17 = hsPCTNTT17 != null ? (float)hsPCTNTT17 : 0f;
            nhanvien.HeSo.PCDHTT17 = hsPCDHTT17 != null ? (float)hsPCDHTT17 : 0f;
            nhanvien.HeSo.PCCVTT17 = hsPCCVTT17 != null ? (float)hsPCCVTT17 : 0f;
			if (hsBHcongthem != null && hslCB != null) nhanvien.HeSo.BHCongThem_ChoGD_PGD = (float)hsBHcongthem;
			if (idChucVu == null) nhanvien.IDChucVu = 0;
            nhanvien.NVNhanKiet = (nvNhanKiet != null) ? (bool)nvNhanKiet : false;
			#region xét lịch trình cho nv
			// chưa có lịch trình thì tạo lịch trình ko có ca
			if (schID == null) {
				nhanvien.LichTrinhLV = new cShiftSchedule {
					SchID = int.MinValue,
					TenLichTrinh = "Chưa SX",
					DSCaThu = new List<List<cCa>>(7),
					DSCaMRThu = new List<List<cCa>>(7)
				};
				for (int i = 0; i < 7; i++) {
					nhanvien.LichTrinhLV.DSCaThu.Add(new List<cCa>());
					nhanvien.LichTrinhLV.DSCaMRThu.Add(new List<cCa>());
				}
				nhanvien.LichTrinhLV.TGLamDemTheoQuyDinh = TGLamDemTheoQuyDinh._22h00;//ver 4.0.0.4
			}
			else {
				// tìm lịch trình có ID, nếu ko tìm thấy thì mặc định là chưa sắp xếp
				nhanvien.LichTrinhLV = DSLichTrinh.FirstOrDefault(o => o.SchID == schID);
				if (nhanvien.LichTrinhLV == null) {
					nhanvien.LichTrinhLV = new cShiftSchedule {
						SchID = int.MinValue,
						TenLichTrinh = "Chưa SX",
						DSCaThu = new List<List<cCa>>(7),
						DSCaMRThu = new List<List<cCa>>(7)
					};
					for (int i = 0; i < 7; i++) {
						nhanvien.LichTrinhLV.DSCaThu.Add(new List<cCa>());
						nhanvien.LichTrinhLV.DSCaMRThu.Add(new List<cCa>());
					}
					nhanvien.LichTrinhLV.TGLamDemTheoQuyDinh = TGLamDemTheoQuyDinh._22h00;//ver 4.0.0.4
				}
			}
			if (nhanvien.LichTrinhLV.TGLamDemTheoQuyDinh == TGLamDemTheoQuyDinh._22h00) { nhanvien.StartNT = XL2._22h00; nhanvien.EndddNT = XL2._06h00; }
			else { nhanvien.StartNT = XL2._21h45; nhanvien.EndddNT = XL2._05h45; }

			#endregion
			#region xét phòng ban

			nhanvien.PhongBan = (maphong == -1) //info phòng có id -1 là chưa sắp xếp, parent null, idParent mặc định là 0 giống Nhà máy(relationID = 0)
								  ? new cPhongBan { ID = -1, idParent = 0, parent = null, Ten = "Chưa sắp xếp", ViTri = int.MaxValue } //info vị trí max thì nằm ở cuối, 1 là nằm ở trên
								  : phongBans.FirstOrDefault(item => item.ID == maphong);

			#endregion

			return nhanvien;
		}


		public static void DiemDanh_v08(List<cUserInfo> dsnv, DateTime ngayBD_Bef2D, DateTime ngayKT_Aft2D, DateTime ngaydiemdanh, DateTime currentTime) {
			if (dsnv.Count == 0) return;

			var Arr_MaCC = (from nv in dsnv select nv.MaCC).ToArray();
			var tableCheck_A = DAO.LayTableCIO_A(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableCheck_V = DAO.LayTableCIO_V(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableVang = DAO.LayTableXPVang(ngayBD_Bef2D, ngayKT_Aft2D, Arr_MaCC);
			var tableXacNhanPC50 = DAO.LayTableXacNhanPC50(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableXacNhanPCDB = DAO.LayTableXacNhanPCDB(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D, Duyet: true);
			var tableNgayLe = DAO.DocNgayLe(ngayBD_Bef2D, ngayKT_Aft2D);

			var DS_Check_KoHopLe_AllNV = new List<cCheck>();

			var ds_raa3_vao1 = new List<cCheck>();
			foreach (var nv in dsnv) {
				var tempMaCC = nv.MaCC;
				nv.NgayCongBD_Bef2D = ngayBD_Bef2D;
				nv.NgayCongKT_Aft2D = ngayKT_Aft2D;
				LoadDSCheck_A(tempMaCC, tableCheck_A, nv.DS_Check_A);
				LoadDSCIO_V(tempMaCC, tableCheck_V, nv.DS_CIO_V);
				LoadDSXPVang_Le(tempMaCC, tableVang, tableNgayLe, nv.DSVang/*, nv.NVNhanKiet*/);
				LoadDSXNPC50(tempMaCC, tableXacNhanPC50, nv.DSXNPhuCap50);
				LoadDSXNPCDB(tempMaCC, tableXacNhanPCDB, nv.DSXNPhuCapDB);
				KhoiTaoDSNgayCong(nv.DSNgayCong, nv.NgayCongBD_Bef2D, nv.NgayCongKT_Aft2D);

			}

			foreach (var nv in dsnv) {
				var DS_Check_KoHopLe_1NV = new List<cCheck>();
				LoaiBoCheckKoHopLe1(nv.DS_Check_A, ref DS_Check_KoHopLe_1NV);// loại bỏ check cùng loại trong 30ph, IO 30 phút
				DS_Check_KoHopLe_AllNV.AddRange(DS_Check_KoHopLe_1NV);// add vào ds ko hợp lệ của nhiều nhân viên để xóa sau
				GhepCIO_A2(nv.DS_Check_A, nv.DS_CIO_A);

				XetCa_ListCIO_A3(nv.DS_CIO_A, nv.LichTrinhLV, ds_raa3_vao1, nv.DS_Check_A);// xét thuộc ca, update thuộc ngày công, tách ca 3&1 nếu có, rồi tính công//nv.MacDinhTinhPC50, //[140615_4]
				XetCa_ListCIO_V4(nv.DS_CIO_V, nv.LichTrinhLV);
				TronDS_CIO_A_V5(nv.DS_CIO_A, nv.DS_CIO_V, nv.DSVaoRa);
				PhanPhoi_DSVaoRa6(nv.DSVaoRa, nv.DSNgayCong);
				PhanPhoi_DSVang7(nv.DSVang, nv.DSNgayCong);
				DiemDanhTheoNgay(nv.DSNgayCong, ngaydiemdanh, currentTime);

			}
			if (DS_Check_KoHopLe_AllNV.Count > 0) DAO.LoaiGioLienQuan(DS_Check_KoHopLe_AllNV);
			if (ds_raa3_vao1.Count > 0) DAO.ThemGio_ra3_vao1(ds_raa3_vao1);
		}

		public static void DiemDanhTheoNgay(List<cNgayCong> dsNgayCong, DateTime dateDiemDanh, DateTime currentTime) {

			var ngayDiemDanh = dsNgayCong.FirstOrDefault(item => item.Ngay == dateDiemDanh);
			if (ngayDiemDanh == null) return;
			if (ngayDiemDanh.DSVaoRa.Count == 0) // ko chấm công -> vắng --> xac dinh vang nghi hay vang co ly do
			{
				ngayDiemDanh.TrangThaiDiemDanh = (ngayDiemDanh.DSVang.Count == 0)
					? TrangThaiDiemDanh.VANG_NGHI // vang nghi neu ko co xin phep
					: TrangThaiDiemDanh.VANG_LYDO; // vang xin phep
			}
			else // có chấm công 
			{
				var LastCIO = ngayDiemDanh.DSVaoRa[ngayDiemDanh.DSVaoRa.Count - 1];
				if (LastCIO.HaveINOUT == -2) // có ra thiếu vào
				{
					ngayDiemDanh.TrangThaiDiemDanh = TrangThaiDiemDanh.DARAVE_THIEUCC; // có chấm công ra --> đã ra về
				}
				else if (LastCIO.HaveINOUT == -1) // có vào thiếu ra
				{
					if (ngayDiemDanh.next != null && ngayDiemDanh.next.DSVaoRa != null && ngayDiemDanh.next.DSVaoRa.Count > 0) {
						ngayDiemDanh.TrangThaiDiemDanh = TrangThaiDiemDanh.DARAVE_THIEUCC; // có vào ko ra, ngày hôm sau tồn tại và có chấm công thì nghĩa là hôm trước về rồi quên chấm
					}
					else {
						ngayDiemDanh.TrangThaiDiemDanh = (currentTime - LastCIO.TimeDaiDien >= XL2._24h00)
							? TrangThaiDiemDanh.DARAVE_THIEUCC // có vào ko ra mà nếu quá 24h(1 ngày) kể từ hiện tại nghĩa là đã về mà thiếu chấm công
							: TrangThaiDiemDanh.DANGLAMVIEC; // ko quá 24h có nghĩa vẫn đang làm
					}
				}
				else if (LastCIO.HaveINOUT == 0) // đủ vào ra
				{
					ngayDiemDanh.TrangThaiDiemDanh = ngayDiemDanh.DSVaoRa.Exists(item => item.HaveINOUT < 0)
						? TrangThaiDiemDanh.DARAVE_THIEUCC // lần cuối đủ vào ra thì những lần trước là nếu thiếu thì nghĩa là ngày đó thiếu chấm công
						: TrangThaiDiemDanh.DARAVE;
				}
			}

		}

		public static void XemCong_v08(List<cUserInfo> dsnv, DateTime ngayBD_Bef2D, DateTime ngayKT_Aft2D) {
			XmlConfigurator.Configure();
			if (dsnv.Count == 0) return;
			#region nạp dữ liệu từ database
			var Arr_MaCC = (from nv in dsnv select nv.MaCC).ToArray(); // tạo mảng danh sách mã chấm công để viết chuỗi query : or.. or
			var tableCheck_A = DAO.LayTableCIO_A(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableCheck_V = DAO.LayTableCIO_V(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableXPVang = DAO.LayTableXPVang(ngayBD_Bef2D, ngayKT_Aft2D, Arr_MaCC);
			var tableXN_PCTC = DAO.LayTableXacNhanPC50(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableXN_PCDB = DAO.LayTableXacNhanPCDB(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D, Duyet: true);
			var tableNgayLe = DAO.DocNgayLe(ngayBD_Bef2D, ngayKT_Aft2D);
            #endregion

            #region transfer dữ liệu sang object
            foreach (var nv in dsnv) {
                var tempMaCC = nv.MaCC;
                nv.NgayCongBD_Bef2D = ngayBD_Bef2D;
                nv.NgayCongKT_Aft2D = ngayKT_Aft2D;
                LoadDSCheck_A(tempMaCC, tableCheck_A, nv.DS_Check_A);
                LoadDSCIO_V(tempMaCC, tableCheck_V, nv.DS_CIO_V);
                LoadDSXPVang_Le(tempMaCC, tableXPVang, tableNgayLe, nv.DSVang/*, nv.NVNhanKiet*/);
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
				var DS_Check_KoHopLe_1NV = new List<cCheck>();
				LoaiBoCheckKoHopLe1(nv.DS_Check_A, ref DS_Check_KoHopLe_1NV);// loại bỏ check cùng loại trong 30ph, IO 30 phút
				DS_Check_KoHopLe_AllNV.AddRange(DS_Check_KoHopLe_1NV);// add vào ds ko hợp lệ của nhiều nhân viên để xóa sau
				GhepCIO_A2(nv.DS_Check_A, nv.DS_CIO_A);
				XetCa_ListCIO_A3(nv.DS_CIO_A, nv.LichTrinhLV, ds_raa3_vao1, nv.DS_Check_A);// xét thuộc ca, update thuộc ngày công, tách ca 3&1 nếu có, rồi tính công//nv.MacDinhTinhPC50, //[140615_4]
				XetCa_ListCIO_V4(nv.DS_CIO_V, nv.LichTrinhLV);
				TronDS_CIO_A_V5(nv.DS_CIO_A, nv.DS_CIO_V, nv.DSVaoRa);
				PhanPhoi_DSVaoRa6(nv.DSVaoRa, nv.DSNgayCong);
				PhanPhoi_DSVang7(nv.DSVang, nv.DSNgayCong);
				TinhCong_ListNgayCong8(nv.DSNgayCong, nv.StartNT, nv.EndddNT);//ver 4.0.0.4
				TinhPCTC_TrongListXNPCTC9(nv.DSXNPhuCap50, nv.DSNgayCong, nv.NVNhanKiet);
				TinhPCDB_TrongListXNPCDB10(nv.DSXNPhuCapDB, nv.DSNgayCong);
				TinhPCNgayVang(nv.DSVang, nv.DSNgayCong);
			}
			if (DS_Check_KoHopLe_AllNV.Count > 0) DAO.LoaiGioLienQuan(DS_Check_KoHopLe_AllNV);
			if (ds_raa3_vao1.Count > 0) DAO.ThemGio_ra3_vao1(ds_raa3_vao1);
			#endregion
		}

		public static void TinhPCNgayVang(List<cLoaiVang> dsVang, List<cNgayCong> dsNgayCong)
		{
			if (dsVang == null || dsVang.Count == 0) return;
			var listDsVangCoPhuCap = dsVang.Where(item => (Math.Abs(item.PhuCap - 0f) > 0.0001f)).ToList();
			if (listDsVangCoPhuCap.Any() == false) return;
			foreach (var ngayCong in dsNgayCong)
			{
				TinhPCNgayVang(ngayCong);
			}
		}

		public static void TinhPCNgayVang(cNgayCong ngayCong)
		{
			if (ngayCong.DSVang == null || ngayCong.DSVang.Count == 0) return;
			List<cLoaiVang> listDSVangCoPhuCap = ngayCong.DSVang.Where(item => Math.Abs(item.PhuCap - 0f) > 0.0001f).ToList();
			if (listDSVangCoPhuCap.Any() == false) return;
			
			foreach (var loaiVang in listDSVangCoPhuCap)
			{
				ngayCong.PhuCaps._Cus += loaiVang.PhuCap;//todo tongPC????
				ngayCong.PhuCaps._TongPC += loaiVang.PhuCap;
			}

		}

		public static void LoadDSCheck_A(int tempMaCC, DataTable tableCheck_A, List<cCheck> ds_Check_A) {
			var arrRows = tableCheck_A.Select("UserEnrollNumber = " + tempMaCC, "TimeStr asc");
			//reset lại các danh sách
			ds_Check_A.Clear();

			if (arrRows.Length == 0) return;

			foreach (var row in arrRows) {
				var UserEnrollNumber = (int)row["UserEnrollNumber"];
				var MachineNo = (int)row["MachineNo"];
				var Source = (string)row["Source"];
				var TimeStr = (DateTime)row["TimeStr"];
				var checkThem = (row["Them"] != DBNull.Value) && (bool)row["Them"];
				var IDGioGoc = (row["IDGioGoc"] != DBNull.Value) ? (int)row["IDGioGoc"] : -1;
				var checkXoaa = (row["Xoa"] != DBNull.Value) && (bool)row["Xoa"];

				cCheck check;
				if (MachineNo % 2 == 1) {
					var checkInn = new cCheck { ID = int.MinValue, MaCC = UserEnrollNumber, Time = TimeStr, Source = Source, MachineNo = MachineNo, Type = "I", PhucHoi = new cPhucHoi { Them = checkThem, IDGioGoc = IDGioGoc, Xoaa = checkXoaa } };
					check = checkInn;
				}
				else {
					var checkOut = new cCheck { ID = int.MinValue, MaCC = UserEnrollNumber, Time = TimeStr, Source = Source, MachineNo = MachineNo, Type = "O", PhucHoi = new cPhucHoi { Them = checkThem, IDGioGoc = IDGioGoc, Xoaa = checkXoaa } };
					check = checkOut;
				}
				ds_Check_A.Add(check);
			}

		}
		public static void LoadDSCIO_V(int tempMaCC, DataTable tableCheck_V, List<cCheckInOut> DS_CIO_V) {
			var arrRows = tableCheck_V.Select("UserEnrollNumber = " + tempMaCC, "ID asc, TimeStr asc");
			DS_CIO_V.Clear();

			if (arrRows.Length == 0 || arrRows.Length == 1) return;
			for (int x = 0, y = 1; x < arrRows.Length; ) {
				//i là index rowInn, j là index row out

				var rowInn = arrRows[x];
				if (y >= arrRows.Length) break; // ko có ph?n t? k? ti?p d? xét
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
				var vaoTreTinhCV = (rowInn["VaoTreLaCV"] != DBNull.Value) && (bool)rowInn["VaoTreLaCV"];//ver 4.0.0.4	
				var raaSomTinhCV = (rowInn["RaSomLaCV"] != DBNull.Value) && (bool)rowInn["RaSomLaCV"];//ver 4.0.0.4	
				var bBuGioTre = (rowInn["BuGioTre"] != DBNull.Value) && (bool)rowInn["BuGioTre"];//ver 4.0.0.8
				var bBuGioSom = (rowInn["BuGioSom"] != DBNull.Value) && (bool)rowInn["BuGioSom"];//ver 4.0.0.8
				var bBuPhepTre = (rowInn["BuPhepTre"] != DBNull.Value) && (bool)rowInn["BuPhepTre"];//ver 4.0.0.8
				var bBuPhepSom = (rowInn["BuPhepSom"] != DBNull.Value) && (bool)rowInn["BuPhepSom"];//ver 4.0.0.8
				var fCongPhepTreCongDon = (rowInn["CongBuPhepTre"] != DBNull.Value) ? (float)rowInn["CongBuPhepTre"] : 0f;//ver 4.0.0.8
				var fCongPhepSomCongDon = (rowInn["CongBuPhepSom"] != DBNull.Value) ? (float)rowInn["CongBuPhepSom"] : 0f;//ver 4.0.0.8
				var pOTMin = (int)rowInn["OTMin"];

				if (IDinn != IDout) // b? m?t c?p -> b? qua d?n c?p k? ti?p, ph?i tang x, y v́ x+1=y ==> y+1
				{
					x = x + 1;
					y = y + 1;
					continue;
				}
				// cùng id --> xét có g?n nhau ko
				if (MachineNoInn % 2 == MachineNoOut % 2) {
					y = y + 1;
					continue;
				}
				// d? di?u ki?n ghép  c?p

				#region ghép

				var chkInnV = new cCheck { ID = IDinn, MachineNo = MachineNoInn, Source = SourceInn, Time = timeInn, Type = "I", PhucHoi = new cPhucHoi { Them = checkThemInn, IDGioGoc = IDGioGoc_Inn, Xoaa = checkXoaaInn }, MaCC = UserEnrollNumber };
				var chkOutV = new cCheck { ID = IDout, MachineNo = MachineNoOut, Source = SourceOut, Time = timeOut, Type = "O", PhucHoi = new cPhucHoi { Them = checkThemOut, IDGioGoc = IDGioGoc_Out, Xoaa = checkXoaaOut }, MaCC = UserEnrollNumber };
				var chkInOutV = new cCheckInOut {
					/*ID = IDinn,*/
					DaXN = true, Vao = chkInnV, Raa = chkOutV, TimeDaiDien = chkInnV.Time,//HaveINOUT = 0, m?c d?nh = 0
					ShiftID = shiftID,
					DuyetChoPhepVaoTre = DuyetChoPhepVaoTre, DuyetChoPhepRaSom = DuyetChoPhepRaSom,
					VaoTreTinhCV = vaoTreTinhCV, RaaSomTinhCV = raaSomTinhCV,//ver 4.0.0.4	
					ChoBuGioTre = bBuGioTre, ChoBuGioSom = bBuGioSom,//ver 4.0.0.8
					ChoBuPhepTre = bBuPhepTre, ChoBuPhepSom = bBuPhepSom, //ver 4.0.0.8
					BuCongPhepTre = fCongPhepTreCongDon, BuCongPhepSom = fCongPhepSomCongDon,//ver 4.0.0.8
					BuCongPhepTreCongDon = fCongPhepTreCongDon, BuCongPhepSomCongDon = fCongPhepSomCongDon,//ver 4.0.0.8
					ThuocNgayCong = ThuocNgayCong(chkInnV.Time),TG = new ThoiGian { OTCa = new TimeSpan(0, pOTMin, 0) },
					TD = new ThoiDiem()
				};
				DS_CIO_V.Add(chkInOutV);
				#endregion

				// sau khi th?c hi?n xong th́ tang 
				x = x + 2;
				y = y + 2;
			}

		}
		public static void LoadDSXPVang_Le(int tempMaCC, DataTable tableVang, DataTable tableNgayLe, List<cLoaiVang> dsVangs/*, bool nvNhanKiet*/) {
			dsVangs.Clear();
			var arrVangg = tableVang.Select("UserEnrollNumber = " + tempMaCC, "TimeDate asc");
			//if (arrVangg.Length == 0) return;
			dsVangs.AddRange(from DataRow row in arrVangg
							 let TimeDate = (DateTime)row["TimeDate"]
							 let absentCode = (string)row["AbsentCode"]
							 let absentDesc = (string)row["AbsentDescription"]
							 let wkdayy = (Single)row["Workingday"]
							 let phuCap = (row["PhuCap"] == DBNull.Value) ? 0f : (Single)(row["PhuCap"])
							 select new cLoaiVang { WorkingDay = wkdayy, MaLV_Code = absentCode, MoTa = absentDesc, Ngay = TimeDate, PhuCap = phuCap});
			if (tableNgayLe.Rows.Count == 0) goto sort;
			var dsNgayLe = (from DataRow row in tableNgayLe.Select(string.Empty, "HDate ASC")
							let ngayle = (DateTime)row["HDate"]
							let mota = row["Holiday"].ToString()
							select new cLoaiVang { WorkingDay = 1f, MaLV_Code = "L", MoTa = mota, Ngay = ngayle, PhuCap = 0f}).ToList();

            //if (nvNhanKiet == true) dsNgayLe.Clear();
			dsVangs.AddRange(dsNgayLe);

		sort: dsVangs.Sort(new cLoaiVangComparer());


		}
		public static void LoadDSXNPC50(int tempMaCC, DataTable tableXN_PCTC, List<structPCTC> dsXacNhanPC) {
			var arrRows = tableXN_PCTC.Select("UserEnrollNumber = " + tempMaCC, "Ngay asc");
			dsXacNhanPC.Clear();
			if (arrRows.Length == 0) return;
			dsXacNhanPC.AddRange(from row in arrRows
								 let UserEnrollNumber = (int)row["UserEnrollNumber"]
								 let ngay = (DateTime)row["Ngay"]
								 let tinhpc50 = (row["TinhPC50"] != DBNull.Value) && (bool)row["TinhPC50"]
								 select new structPCTC { Ngay = ngay, TinhPC50 = tinhpc50, });
		}
		public static void LoadDSXNPCDB(int tempMaCC, DataTable tableXN_PCDB, List<structPCDB> dsXacNhanPC) {
			var arrRows = tableXN_PCDB.Select("UserEnrollNumber = " + tempMaCC, "Ngay asc");
			dsXacNhanPC.Clear();
			if (arrRows.Length == 0) return;
			dsXacNhanPC.AddRange(from row in arrRows
								 let UserEnrollNumber = (int)row["UserEnrollNumber"]
								 let ngay = (DateTime)row["Ngay"]
								 let LoaiPC = (int)row["LoaiPC"]
								 let PCNgay = (row["PCNgay"] != DBNull.Value) ? (int)row["PCNgay"] : 0
								 let PCDem = (row["PCDem"] != DBNull.Value) ? (int)row["PCDem"] : 0
								 select new structPCDB { Ngay = ngay, LoaiPC = LoaiPC, PCNgay = PCNgay, PCDem = PCDem });
		}
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


		#region các bước để tính toán công và phụ cấp các loại


		public static void LoaiBoCheckKoHopLe1(List<cCheck> ds_Check_A, ref List<cCheck> ds_Check_Trong30ph) {
			//clear ds_Check_Trong30ph trước vì nó còn giữ các check ko hl của nv trước
			ds_Check_Trong30ph.Clear();
			// lọc này phải dảm bảo sort trước
			if (ds_Check_A == null || ds_Check_A.Count == 0 || ds_Check_A.Count == 1) return;
			var i = 0;
			while (i + 1 < ds_Check_A.Count) {
				var before = ds_Check_A[i];
				var afterr = ds_Check_A[i + 1];
				if ((afterr.Time - before.Time) < XL2._10phut) {
					//info ver 4.0.0.8 // giữ giờ check cuối cùng trừ giờ từ PC
					if (before.MachineNo > 20 && afterr.MachineNo > 20 && (afterr.Time - before.Time) < XL2._01phut)
						i++;
					else
					{
						ds_Check_Trong30ph.Add(before);
						ds_Check_A.Remove(before);
					}
				}
				else i++;
			}
		}

		public static void GhepCIO_A2(List<cCheck> ds_Check_A, List<cCheckInOut> ds_CIO_A) {
			ds_CIO_A.Clear();
			var x = 0;
			while (x + 1 < ds_Check_A.Count) {
				var chk_1 = ds_Check_A[x];
				var chk_2 = ds_Check_A[x + 1];
				if (chk_1.Type == "O") {
					// đầu ds là checkOut --> ra ko vào
					var CIO = new cCheckInOut { Vao = null, Raa = chk_1, HaveINOUT = -2, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
					ds_CIO_A.Add(CIO);
					x++;
				}
				else {
					//đầu ds là checkInn-> kiểm tra kế nếu cũng là check In thì checkInn trước là vào ko ra
					if (chk_2.Type == "I") {
						var CIO = new cCheckInOut { Vao = chk_1, Raa = null, HaveINOUT = -1, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
						ds_CIO_A.Add(CIO);
						x++;
					}
					else {
						// kế là checkOut --> kiểm tra nằm trong khoảng >30ph và dưới 21h45 thì ghép, ngược lại thì giờ vào ko ra, ra ko vào
						var duration = chk_2.Time - chk_1.Time;
						if (duration > XL2._22h00) {//ver 4.0.0.4	old:(duration > XL2._21h45)
							var CIO1 = new cCheckInOut { Vao = chk_1, Raa = null, HaveINOUT = -1, TimeDaiDien = chk_1.Time, TD = new ThoiDiem(), }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
							var CIO2 = new cCheckInOut { Vao = null, Raa = chk_2, HaveINOUT = -2, TimeDaiDien = chk_2.Time, TD = new ThoiDiem(), }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
							ds_CIO_A.Add(CIO1);
							x++;
							ds_CIO_A.Add(CIO2);
							x++;
						}
						else {
							var CIO = new cCheckInOut { Vao = chk_1, Raa = chk_2, HaveINOUT = 0, TimeDaiDien = chk_1.Time, TG = new ThoiGian(), TD = new ThoiDiem(), };
							ds_CIO_A.Add(CIO);
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
					ds_CIO_A.Add(CIO1);
				}
				else {
					var CIO2 = new cCheckInOut { Vao = null, Raa = chk_1, HaveINOUT = -2, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
					ds_CIO_A.Add(CIO2);
				}
			}
		}

		public static void XetCa_ListCIO_A3(List<cCheckInOut> ds_CIO_A, cShiftSchedule lichtrinh, List<cCheck> ds_raa3_vao1, List<cCheck> ds_check_A) {
			//bool macdinh_tinhPC50, //[140615_4]
			try {
				var i = 0;
				while (i < ds_CIO_A.Count) {
					var CIO = ds_CIO_A[i];

					#region nếu giờ quên check thì chỉ kiểm tra khoảng hiểu ca

					int index;
					if (CIO.HaveINOUT < 0) {
						CIO.ThuocNgayCong = CIO.TimeDaiDien.Date;
						index = GetIndex(CIO.ThuocNgayCong);
						CIO.DSCa = Tim_DSCa_NhanDienDuoc(CIO.TimeDaiDien, CIO.ThuocNgayCong, CIO.HaveINOUT, lichtrinh.DSCaThu[index]);
						i++;
						continue;
					}

					#endregion

					var ngay = ThuocNgayCong(CIO.TimeDaiDien);
					CIO.ThuocNgayCong = ngay;
					index = GetIndex(CIO.ThuocNgayCong);
					var ca = KiemtraThuocCa(CIO.Vao.Time, CIO.Raa.Time, CIO.ThuocNgayCong, lichtrinh.DSCaThu[index]);

					#region nếu thuộc khoảng hiểu ca thì set ca

					if (ca != null) {
						if (ca.TachCaDem) {
							//tbd thêm điều kiện ca.QuaDem
							var ca3 = ca.catruoc;
							var ca1 = ca.casauuu;

							#region check inn, check out vao 3 ra 3, vao 1 ra 1

							var vaoca3 = CIO.Vao;
							var raaca3 = new cCheck {
								ID = int.MinValue, Type = "O", MachineNo = 22, Source = "PC", Time = ngay.Add(ca3.Duty.Off), MaCC = CIO.Vao.MaCC,
								PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false },
							}; // đáng lẽ IDgiờgốc là -1 vì giờ này mới thêm chưa bị sửa nhưng do đây là giờ đệm ko có trong csdl nên để max, ko cho update
							var vaoca1 = new cCheck {
								ID = int.MinValue, Type = "I", MachineNo = 21, Source = "PC", Time = ngay.AddDays(1d).Date.Add(ca1.Duty.Onn).Add(XL2._01giay), MaCC = CIO.Raa.MaCC,
								PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false },
							}; // đáng lẽ IDgiờgốc là -1 vì giờ này mới thêm chưa bị sửa nhưng do đây là giờ đệm ko có trong csdl nên để max, ko cho update
							var raaca1 = CIO.Raa;

							#endregion

							ds_raa3_vao1.Add(raaca3);
							ds_raa3_vao1.Add(vaoca1);
							ds_check_A.Add(raaca3);
							ds_check_A.Add(vaoca1);
							ds_check_A.Sort(new cCheckComparer());
							ds_CIO_A[i] = new cCheckInOut { TG = new ThoiGian(), TD = new ThoiDiem(), ThuocCa = ca3, Vao = vaoca3, Raa = raaca3, ThuocNgayCong = ngay, TimeDaiDien = vaoca3.Time, }; //TinhPC150 = macdinh_tinhPC50, //[140615_2]

							var newCIO = new cCheckInOut { TG = new ThoiGian(), TD = new ThoiDiem(), ThuocCa = ca1, Vao = vaoca1, Raa = raaca1, ThuocNgayCong = ngay.AddDays(1d), TimeDaiDien = vaoca1.Time, }; //TinhPC150 = macdinh_tinhPC50, //[140615_2]

							// vì hàm insert ko cho phép chèn ở vị trí > số lượng phần tử
							// => nên nếu i là phần tử cuối thì add vào cuối danh sách, ngược lại thì insert vào vị trí i+1
							if (i == (ds_CIO_A.Count - 1)) ds_CIO_A.Add(newCIO);
							else ds_CIO_A.Insert(i + 1, newCIO);
							i = i + 2; // +2 vì i là ca3, i+1 là ca 1
						}
						else {
							CIO.ThuocCa = ca;
							i++;
						}
					}
					#endregion
					#region nếu ko thuộc khoảng hiểu thì đó tạo ca tự do, tính công theo ca tự do này

					else {
						var catudo = new cCa { ID = Int32.MinValue, Code = Properties.Settings.Default.shiftCodeCa8h, Is_CaTuDo = true };
						//TaoCaTuDo(catudo, CIO.Vao.Time, XL2._08gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, "8");
						TaoCaTuDo(catudo, CIO.Vao.Time);//ver 4.0.0.4	
						CIO.ThuocCa = catudo;

						i++;
					}

					#endregion
				}

			}
			catch (Exception e) {
				lg.Error(string.Format("[{0}]_[{1}]\n", "XLChamCong", System.Reflection.MethodBase.GetCurrentMethod().Name), e);
			}
		}

		public static void XetCa_1_CIO_A_KoTachCa(cCheckInOut CIO, cShiftSchedule lichtrinh) {
			int index;
			#region nếu giờ quên check thì chỉ kiểm tra khoảng hiểu ca

			if (CIO.HaveINOUT < 0) {
				CIO.ThuocNgayCong = CIO.TimeDaiDien.Date;
				index = XL.GetIndex(CIO.ThuocNgayCong);
				CIO.DSCa = Tim_DSCa_NhanDienDuoc(CIO.TimeDaiDien, CIO.ThuocNgayCong, CIO.HaveINOUT, lichtrinh.DSCaThu[index]);
			}

			#endregion

			// ca có đủ vào ra
			var ngay = ThuocNgayCong(CIO.TimeDaiDien);
			CIO.ThuocNgayCong = ngay;
			index = XL.GetIndex(CIO.ThuocNgayCong);
			var ca = KiemtraThuocCa(CIO.Vao.Time, CIO.Raa.Time, CIO.ThuocNgayCong, lichtrinh.DSCaThu[index]);
			if (ca != null) { //nếu thuộc khoảng hiểu ca thì tính công
				CIO.ThuocCa = ca;
			}
			else { //nếu ko thuộc khoảng hiểu thì đó tạo ca tự do, tính công theo ca tự do này
				var catudo = new cCa { ID = Int32.MinValue, Code = Properties.Settings.Default.shiftCodeCa8h, Is_CaTuDo = true };
				//TaoCaTuDo(catudo, CIO.Vao.Time, XL2._08gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, "8");
				TaoCaTuDo(catudo, CIO.Vao.Time);
				CIO.ThuocCa = catudo;
			}
		}

		public static int GetIndex(DateTime dateTime) {
			switch (dateTime.DayOfWeek) {
				case DayOfWeek.Sunday:
					return 0;
				case DayOfWeek.Monday:
					return 1;
				case DayOfWeek.Tuesday:
					return 2;
				case DayOfWeek.Wednesday:
					return 3;
				case DayOfWeek.Thursday:
					return 4;
				case DayOfWeek.Friday:
					return 5;
				case DayOfWeek.Saturday:
					return 6;

			}
			return 0;
		}

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



		public static void XetCa_ListCIO_V4(List<cCheckInOut> ds_CIO_V, cShiftSchedule lichtrinh) {
			foreach (cCheckInOut chkInOutV in ds_CIO_V) {
				XetCa_1_CIO_V(chkInOutV, lichtrinh);
			}

		}

		public static void XetCa_1_CIO_V(cCheckInOut chkInOutV, cShiftSchedule lichtrinh) {
			cCa thuocCa = new cCa();
			int index = GetIndex(chkInOutV.ThuocNgayCong);
			var shiftID = chkInOutV.ShiftID;
			if (shiftID > 0) {
				// tìm trong ds ca chuẩn có trong lịch trình trước
				thuocCa = ((lichtrinh.DSCaThu[index].FirstOrDefault(item => item.ID == shiftID)
					?? lichtrinh.DSCaMRThu[index].FirstOrDefault(item => item.ID == shiftID))
					?? XL.DSCa.FirstOrDefault(item => item.ID == shiftID))
					?? XL.DSCaMoRong.FirstOrDefault(item => item.ID == shiftID);
			}
			else if (shiftID < Int32.MinValue + 100) // ca tự do (8 tiếng, ca dài 12 tiếng)
			{
				thuocCa = new cCa { ID = shiftID, Is_CaTuDo = true };
				TaoCaTuDo(thuocCa, chkInOutV.Vao.Time);
			}
			chkInOutV.ThuocCa = thuocCa;
		}

		public static void TronDS_CIO_A_V5(List<cCheckInOut> dsCIO_A, List<cCheckInOut> dsCIO_V, List<cCheckInOut> kq) {
			kq.Clear();
			kq.AddRange(dsCIO_A);
			kq.AddRange(dsCIO_V);
			kq.Sort(new cCheckInOutComparer());
		}

		public static void TinhTG_LV_LVCa3_LamThem1Ca(cCheckInOut CIO, cCa ca, TimeSpan startNT, TimeSpan endddNT) {
			bool tempQuaDem;
			TinhTG_LV_LVCa3_LamThem1Ca(CIO.ThuocNgayCong, CIO.HaveINOUT, CIO.DaXN, CIO.DuyetChoPhepVaoTre, CIO.DuyetChoPhepRaSom,
				CIO.VaoTreTinhCV, CIO.RaaSomTinhCV,//ver 4.0.0.4	
				CIO.ChoBuGioTre, CIO.ChoBuGioSom,//ver 4.0.0.8	
				CIO.ChoBuPhepTre, CIO.BuCongPhepTre, ref CIO.BuCongPhepTreCongDon, CIO.ChoBuPhepSom, CIO.BuCongPhepSom, ref CIO.BuCongPhepSomCongDon,//ver 4.0.0.8	
				CIO.Vao.Time, CIO.Raa.Time, ca.Duty.Onn, ca.Duty.Off, ca.chophepTreTS, ca.chophepSomTS, ca.batdaulamthemTS, ca.LunchMin, CIO.TG.OTCa,
				startNT, endddNT,//ver 4.0.0.4
				out CIO.TD.BD_LV, out CIO.TD.KT_LV, out CIO.TD.KT_LV_ChuaOT, out CIO.TD.KT_LV_DaCoOT, out CIO.TD.BD_LV_Ca3, out CIO.TD.KT_LV_Ca3,
				out CIO.TG.GioThuc, out CIO.TG.GioLamViec, out CIO.TG.VaoTre, out CIO.TG.RaaSom,
				out CIO.TG.VaoSauCaKoTruCong, out CIO.TG.RaTruocCaKoTruCong, //ver 4.0.0.8
				out CIO.TG.GioLVTrongCa,//ver 4.0.0.4	
				out CIO.TG.OLai, out CIO.TG.LamThem, out tempQuaDem, out CIO.TG.LamBanDem);
			CIO.QuaDem = tempQuaDem;// ko cho phép out CIO.QuaDem nên fix tạm bằng cách dùng biến trung gian cục bộ và gán lại
		}

		public static void TinhTG_LV_LVCa3_LamThem1Ca(DateTime ThuocNgayCong, int HaveINOUT, Boolean DaXN,
			bool KoTruVaoTre, bool KoTruRaaSom,
			bool VaotreTinhCV, bool RaaSomTinhCV, //ver 4.0.0.4	
			bool ChoBuGioVaoTre, bool ChoBuGioRaaSom, //ver 4.0.0.8	
			bool ChoBuPhepVaoTre, float CongPhepTre,ref float CongBuPhepTre, bool ChoBuPhepRaaSom, float CongPhepSom,ref float CongBuPhepSom, //ver 4.0.0.8	
			DateTime Vao, DateTime Raa,
			TimeSpan DutyOnn, TimeSpan DutyOff, TimeSpan chophepTreTS, TimeSpan chophepSomTS, TimeSpan batdaulamthemTS,
			TimeSpan LunchMin, TimeSpan OTCa,
			TimeSpan startNT, TimeSpan endddNT, //ver 4.0.0.4
			out DateTime TD_BD_LV, out DateTime TD_KT_LV, out DateTime TD_KT_LV_TrongCa, out DateTime TD_KT_LV_NgoaiCaCoOT,
			out DateTime TD_BD_LV_Ca3, out DateTime TD_KT_LV_Ca3,
			out TimeSpan TGThucTe, out TimeSpan TGGioLamViec, out TimeSpan TGVaoTre, out TimeSpan TGRaaSom,
			out TimeSpan TGVaoTreKoTruVR, out TimeSpan TGRaaSomKoTruVR,
			out TimeSpan TGGioLamViecTrongCa, //ver 4.0.0.4	
			out TimeSpan TGOLai, out TimeSpan TGLamThem, out bool QuaDem, out TimeSpan TGLamBanDem) {

			TD_BD_LV = DateTime.MinValue;
			TD_KT_LV_TrongCa = DateTime.MinValue;
			TD_KT_LV_NgoaiCaCoOT = DateTime.MinValue;
			TD_KT_LV = DateTime.MinValue;
			TD_BD_LV_Ca3 = DateTime.MinValue;
			TD_KT_LV_Ca3 = DateTime.MinValue;
			TGThucTe = TimeSpan.Zero;
			TGVaoTre = TimeSpan.Zero;
			TGRaaSom = TimeSpan.Zero;
			TGVaoTreKoTruVR = TimeSpan.Zero;
			TGRaaSomKoTruVR = TimeSpan.Zero;
			TGOLai = TimeSpan.Zero;
			TGGioLamViec = TimeSpan.Zero;
			TGGioLamViecTrongCa = TimeSpan.Zero;//ver 4.0.0.4	
			TGLamThem = TimeSpan.Zero;
			TGLamBanDem = TimeSpan.Zero;
			QuaDem = false;

			if (HaveINOUT < 0) return;

			var TD_BD_Ca = ThuocNgayCong.Add(DutyOnn);
			var TD_KT_Ca = ThuocNgayCong.Add(DutyOff);//off duty này đã bao gồm daycount được công bên trong
			var thoidiem_BD_tinhtre = ThuocNgayCong.Add(chophepTreTS);
			var thoidiem_BD_tinhsom = ThuocNgayCong.Add(chophepSomTS);
			var thoidiem_BD_tinhOLai = ThuocNgayCong.Add(batdaulamthemTS);
			//var tmpBDLamDem = ThuocNgayCong.Add(XL2._21h45);
			//var tmpKTLamDem = ThuocNgayCong.AddDays(1d).Add(XL2._05h45);
			var tmpBDLamDem = ThuocNgayCong.Add(startNT);//ver 4.0.0.4
			var tmpKTLamDem = ThuocNgayCong.AddDays(1d).Add(endddNT);//ver 4.0.0.4

			bool quadem;

			TGThucTe = Raa - Vao;
			// kiểm tra giờ ra ko được nhỏ hơn vào ca, giờ vào ko được nhỏ hơn ra ca
			if (Raa < TD_BD_Ca || Vao > TD_KT_Ca) {
				return;
			}
			//var temp = 0f;
			XL.Vao(Vao, TD_BD_Ca, thoidiem_BD_tinhtre, KoTruVaoTre, VaotreTinhCV, out TD_BD_LV, out TGVaoTre, out TGVaoTreKoTruVR);//ver 4.0.0.8
			XL.XetBuGioTre(ChoBuGioVaoTre, TD_BD_Ca, ref TD_BD_LV, ref TGVaoTre);
			XL.XetBuPhepTre(ChoBuPhepVaoTre, CongPhepTre, TD_BD_Ca, ref CongBuPhepTre, ref TD_BD_LV, ref TGVaoTre);
			XL.Raa(Raa, TD_KT_Ca, thoidiem_BD_tinhsom, KoTruRaaSom, RaaSomTinhCV, out TD_KT_LV_TrongCa, out TGRaaSom, out TGRaaSomKoTruVR);//ver 4.0.0.8
			XL.XetBuGioSom(ChoBuGioRaaSom, TD_KT_Ca, ref TD_KT_LV_TrongCa, ref TGRaaSom);
			XL.XetBuPhepSom(ChoBuPhepRaaSom, CongPhepSom, TD_KT_Ca, ref CongBuPhepSom, ref TD_KT_LV_TrongCa, ref TGRaaSom);
			OLai(Raa, TD_KT_Ca, thoidiem_BD_tinhOLai, out TGOLai);
			Tinh_TGLamViecTrongCa(TD_BD_LV, TD_KT_LV_TrongCa, LunchMin, out TGGioLamViecTrongCa);
			TD_KT_LV = TD_KT_LV_TrongCa; // giờ chưa qua xác nhận thì kt làm việc là chưa tính OT
			if (DaXN) {
				LamThem(TD_KT_LV_TrongCa, OTCa, out TD_KT_LV_NgoaiCaCoOT);
				TD_KT_LV = TD_KT_LV_NgoaiCaCoOT;
			}
			//Tinh_TGLamViec(TD_BD_LV, TD_KT_LV, LunchMin, out TGGioLamViec);// lúc này TD_KT_LV là kết thúc ca nếu chưa XN, nếu đã XN thì = (TD_KT_LV + khoảng OT)
			TGGioLamViec = TGGioLamViecTrongCa + OTCa;//ver 4.0.0.4	
			Tinh_TGLamViec_Ca3(TD_BD_LV, TD_KT_LV, tmpBDLamDem, tmpKTLamDem, out TD_BD_LV_Ca3, out  TD_KT_LV_Ca3, out TGLamBanDem, out quadem);
			TGLamThem = Tinh_TGLamThem(TGGioLamViec);//(TGGioLamViec - XL2._08gio) >= XL2._01phut ? (TGGioLamViec - XL2._08gio) : TimeSpan.Zero;
			QuaDem = quadem;


		}



		public static void TinhCong_ListNgayCong8(List<cNgayCong> dsNgayCong, TimeSpan startNT, TimeSpan endddNT) {
			foreach (var ngayCong in dsNgayCong) {
				TinhCong_HangNgay2(ngayCong, startNT, endddNT);
			}
		}

		public static void TinhCong_HangNgay(cNgayCong ngayCong, TimeSpan startNT, TimeSpan endddNT) {
			/*
						ngayCong.TG = new ThoiGian();
						ngayCong.PhuCaps = new PhuCap();
						ngayCong.TongCong = 0f;
						ngayCong.TongNgayLV = 0f; //ver4.0.0.1
						ngayCong.QuaDem = false;
						ngayCong.TinhPC50 = false;
						ngayCong.TinhPCDB = false;
						ngayCong.LoaiPCDB = 0;
						ngayCong.TrangThaiDiemDanh = TrangThaiDiemDanh.VANG_NGHI;
						// tính công của từng ca làm việc, sau đó tổng hợp Công làm việc của 1 ngày
						if (ngayCong.DSVaoRa.Count == 0) return;
						foreach (var CIO in ngayCong.DSVaoRa) {
							if (CIO.HaveINOUT < 0) {
								continue;
							}
							TinhTG_LV_LVCa3_LamThem1Ca(CIO, CIO.ThuocCa, startNT, endddNT);
							if (CIO.QuaDem) ngayCong.QuaDem = true; // set qua đêm nếu có
							//CIO.Cong = Convert.ToSingle(Math.Round((CIO.TG.GioLamViec.TotalHours / CIO.ThuocCa.WorkingTimeTS.TotalHours) * CIO.ThuocCa.Workingday, 2));// làm tròn 2 số thập phân
							//CIO.Cong = Convert.ToSingle(Math.Round(((CIO.TG.GioLVTrongCa.TotalHours / CIO.ThuocCa.WorkingTimeTS.TotalHours) * CIO.ThuocCa.Workingday)	+ (CIO.TG.OTCa.TotalHours / 8f), 2));
							float cong_trong_ca = Convert.ToSingle(Math.Round(((CIO.TG.GioLVTrongCa.TotalHours / CIO.ThuocCa.WorkingTimeTS.TotalHours) * CIO.ThuocCa.Workingday) , 2));
							float cong_ngoai_ca = Convert.ToSingle(Math.Round((CIO.TG.OTCa.TotalHours / 8f), 2));
							CIO.Cong = cong_trong_ca+cong_ngoai_ca;
							ngayCong.TG.GioThuc += CIO.TG.GioThuc;
							ngayCong.TG.GioLamViec += CIO.TG.GioLamViec;
							ngayCong.TG.LamBanDem += CIO.TG.LamBanDem;
							ngayCong.TG.VaoTre += CIO.TG.VaoTre;
							ngayCong.TG.RaaSom += CIO.TG.RaaSom;
							ngayCong.TongCong += CIO.Cong; //công đã được làm tròn 2 số thập phân ở trên
							//if (CIO.DuyetChoPhepVaoTre && CIO.DuyetChoPhepRaSom)//ver 4.0.0.4	
							//{
							//	ngayCong.TongNgayLV += Convert.ToSingle((CIO.ThuocCa.Workingday + (CIO.TG.OTCa.TotalHours / 8f)));
							//}
							//else {
							//	var temp = CIO.TG.GioLVTrongCa;
							//	if (CIO.VaoTreTinhCV == false) temp += CIO.TG.VaoTre;
							//	if (CIO.RaaSomTinhCV == false) temp += CIO.TG.RaaSom;
							//	ngayCong.TongNgayLV += Convert.ToSingle(((temp.TotalHours / CIO.ThuocCa.WorkingTimeTS.TotalHours) * CIO.ThuocCa.Workingday)
							//													   + (CIO.TG.OTCa.TotalHours / 8f));
							//}
							if ((CIO.DuyetChoPhepVaoTre && CIO.DuyetChoPhepRaSom)
								|| (CIO.DuyetChoPhepVaoTre == false && CIO.DuyetChoPhepRaSom == false && CIO.VaoTreTinhCV == false && CIO.RaaSomTinhCV == false))
							{
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
						ngayCong.TG.LamThem = Tinh_TGLamThem(ngayCong.TG.GioLamViec);// (ngayCong.TG.GioLamViec - XL2._08gio > XL2._01phut) ? ngayCong.TG.GioLamViec - XL2._08gio : TimeSpan.Zero;			
						ngayCong.PhuCaps._30_dem = Convert.ToSingle(Math.Round((ngayCong.TG.LamBanDem.TotalHours / 8d) * (XL2.PC30 / 100f), 2, MidpointRounding.ToEven));
						ngayCong.PhuCaps._TongPC = ngayCong.PhuCaps._30_dem;
			*/
		}

		public static void TinhCong_HangNgay2(cNgayCong ngayCong, TimeSpan startNT, TimeSpan endddNT) {
			ngayCong.TG = new ThoiGian();
			ngayCong.PhuCaps = new PhuCap();
			ngayCong.TongCong_4008 = 0f;
			//ngayCong.TongNgayLV = 0f; //ver4.0.0.1
			ngayCong.CongDinhMucDuoi8Tieng = 0f;//ver 4.0.0.8
			ngayCong.CongTichLuy = 0f;//ver 4.0.0.8
			ngayCong.TruCongTreVR = 0f;//ver 4.0.0.8
			ngayCong.TruCongSomVR = 0f;//ver 4.0.0.8
			ngayCong.TruCongTreBu = 0f;//ver 4.0.0.8
			ngayCong.TruCongSomBu = 0f;//ver 4.0.0.8
			ngayCong.CongBuPhepTre = 0f;//ver 4.0.0.8
			ngayCong.CongBuPhepSom = 0f;//ver 4.0.0.8

			ngayCong.QuaDem = false;
			ngayCong.TinhPC50 = false;
			ngayCong.TinhPCDB = false;
			ngayCong.LoaiPCDB = 0;
			ngayCong.TrangThaiDiemDanh = TrangThaiDiemDanh.VANG_NGHI;
			// tính công của từng ca làm việc, sau đó tổng hợp Công làm việc của 1 ngày
			if (ngayCong.DSVaoRa.Count == 0) return;
			foreach (var CIO in ngayCong.DSVaoRa) {
				if (CIO.HaveINOUT < 0) {
					continue;
				}
				//XacDinhThoiDiemLamViec(CIO);
				TinhTG_LV_LVCa3_LamThem1Ca(CIO, CIO.ThuocCa, startNT, endddNT);
				if (CIO.QuaDem) ngayCong.QuaDem = true; // set qua đêm nếu có
				#region //ver 4.0.0.8

				float truCongTre = Convert.ToSingle(((CIO.TG.VaoTre.TotalHours / CIO.ThuocCa.WorkingTimeTS.TotalHours) * CIO.ThuocCa.Workingday)).Truncate(2); //ver 4.0.0.8
				float truCongSom = Convert.ToSingle(((CIO.TG.RaaSom.TotalHours / CIO.ThuocCa.WorkingTimeTS.TotalHours) * CIO.ThuocCa.Workingday)).Truncate(2); //ver 4.0.0.8
				float fVaoSauCaKoTruCong = Convert.ToSingle(((CIO.TG.VaoSauCaKoTruCong.TotalHours / CIO.ThuocCa.WorkingTimeTS.TotalHours) * CIO.ThuocCa.Workingday)).Truncate(2); //ver 4.0.0.8
				float fRaTruocCaKoTruCong = Convert.ToSingle(((CIO.TG.RaTruocCaKoTruCong.TotalHours / CIO.ThuocCa.WorkingTimeTS.TotalHours) * CIO.ThuocCa.Workingday)).Truncate(2); //ver 4.0.0.8
				CIO.TruCongTre = truCongTre;
				CIO.TruCongSom = truCongSom;

				float cong_trong_ca = CIO.ThuocCa.Workingday - CIO.TruCongTre - CIO.TruCongSom - CIO.BuCongPhepTreCongDon - CIO.BuCongPhepSomCongDon - fVaoSauCaKoTruCong - fRaTruocCaKoTruCong;
				float cong_ngoai_ca = Convert.ToSingle(Math.Round((CIO.TG.OTCa.TotalHours / 8f), 2));
				CIO.CongTrongCa = cong_trong_ca;
				CIO.CongNgoaiCa = cong_ngoai_ca;

				CIO.Cong = CIO.CongTrongCa + CIO.CongNgoaiCa;

				CIO.TruCongTreVR = CIO.TruCongTre;
				CIO.TruCongSomVR = CIO.TruCongSom;
				
				if (CIO.ChoBuGioTre) {
					CIO.TruCongTreVR = 0f;
					CIO.TruCongTreBu = CIO.TruCongTre;
				}
				if (CIO.ChoBuGioSom) {
					CIO.TruCongSomVR = 0f;
					CIO.TruCongSomBu = CIO.TruCongSom;
				}

				ngayCong.TongCong_4008 += (CIO.CongTrongCa + CIO.CongNgoaiCa);//ver 4.0.0.8
				ngayCong.TruCongTreVR += CIO.TruCongTreVR;
				ngayCong.TruCongSomVR += CIO.TruCongSomVR;
				ngayCong.TruCongTreBu += CIO.TruCongTreBu;
				ngayCong.TruCongSomBu += CIO.TruCongSomBu;
				ngayCong.CongBuPhepTre += CIO.BuCongPhepTreCongDon;
				ngayCong.CongBuPhepSom += CIO.BuCongPhepSomCongDon;
				#endregion

				ngayCong.TG.GioThuc += CIO.TG.GioThuc;
				ngayCong.TG.GioLamViec += CIO.TG.GioLamViec;
				ngayCong.TG.LamBanDem += CIO.TG.LamBanDem;
				ngayCong.TG.VaoTre += CIO.TG.VaoTre;
				ngayCong.TG.RaaSom += CIO.TG.RaaSom;
				//ngayCong.TongCong += CIO.Cong; //công đã được làm tròn 2 số thập phân ở trên
				//ngayCong.TongCong_4008 += (CIO.CongTrongCa + CIO.CongNgoaiCa);//ver 4.0.0.8

			}
			ngayCong.TG.LamThem = Tinh_TGLamThem(ngayCong.TG.GioLamViec);
			ngayCong.PhuCaps._30_dem = Convert.ToSingle(Math.Round((ngayCong.TG.LamBanDem.TotalHours / 8d) * (XL2.PC30 / 100f), 2, MidpointRounding.ToEven));
			ngayCong.PhuCaps._TongPC = ngayCong.PhuCaps._30_dem;

			#region //ver 4.0.0.8

			if (ngayCong.TongCong_4008 <= 1f) {
				ngayCong.CongDinhMucDuoi8Tieng = ngayCong.TongCong_4008;
				ngayCong.CongTichLuy = 0f;
			}
			else {
				ngayCong.CongDinhMucDuoi8Tieng = 1f;
				ngayCong.CongTichLuy = ngayCong.TongCong_4008 - 1f;
			}

			#endregion
		}


		public static void TinhPCTC_TrongListXNPCTC9(List<structPCTC> dsXacNhanPC, List<cNgayCong> dsNgayCong, bool nvNhanKiet) {
			foreach (var item in dsXacNhanPC) {
				var ngayCong = dsNgayCong.Find(o => o.Ngay == item.Ngay);
				TinhPCTC_CuaNgay(ngayCong, item.TinhPC50, nvNhanKiet);
			}

		}

		public static void TinhPCTC_CuaNgay(cNgayCong ngayCong, bool choPhepTinhTC, bool nvNhanKiet) {
			ngayCong.TinhPC50 = choPhepTinhTC;
			Tinh_PCTC(choPhepTinhTC, ngayCong.QuaDem, ngayCong.TG.LamBanDem, ngayCong.TG.LamThem, nvNhanKiet,
							   out ngayCong.TG.Tinh130, out ngayCong.TG.Tinh150, out ngayCong.TG.TinhTCC3,
							   out ngayCong.PhuCaps._30_dem, out ngayCong.PhuCaps._50_TC, out ngayCong.PhuCaps._100_TCC3, out ngayCong.PhuCaps._TongPC);
		}

		public static void TinhPCTC_CuaNgay(cNgayCong ngayCong, List<structPCTC> DSXNPhuCap50, bool nvNhanKiet) {
			var IndexngayTinhLaiPCTC1 = DSXNPhuCap50.FindIndex(item => item.Ngay == ngayCong.Ngay);
			var kq1 = (IndexngayTinhLaiPCTC1 >= 0) && (DSXNPhuCap50[IndexngayTinhLaiPCTC1].TinhPC50);
			TinhPCTC_CuaNgay(ngayCong, kq1, nvNhanKiet);
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
			TinhPCDB(ngayCong.TinhPCDB, ngayCong.TG.GioLamViec, ngayCong.TG.LamBanDem, ngayCong.QuaDem, ngayCong.LoaiPCDB, PCNgay, PCDem, //tbd
		 out ngayCong.TG.Tinh200, out ngayCong.TG.Tinh260, out ngayCong.TG.Tinh300, out ngayCong.TG.Tinh390, out ngayCong.TG.TinhPCCus,
		 out ngayCong.PhuCaps._100_LVNN_Ngay, out ngayCong.PhuCaps._150_LVNN_Dem, out ngayCong.PhuCaps._200_LeTet_Ngay, out ngayCong.PhuCaps._250_LeTet_Dem, out ngayCong.PhuCaps._Cus, ref ngayCong.PhuCaps._TongPC);
			//TinhPCNgayVang(ngayCong);
		}

		public static void TinhPCDB_CuaNgay(cNgayCong ngayCong, List<structPCDB> DSXNPCDB) {
			var ngayTinhLaiPCDB = DSXNPCDB.FindIndex(item => item.Ngay == ngayCong.Ngay);
			if (ngayTinhLaiPCDB >= 0)
				TinhPCDB_CuaNgay(ngayCong, DSXNPCDB[ngayTinhLaiPCDB].LoaiPC, DSXNPCDB[ngayTinhLaiPCDB].PCNgay, DSXNPCDB[ngayTinhLaiPCDB].PCDem);
		}

		#endregion

		public static void ThemGioChoNV(int MaCC, cCheck check, List<cCheck> DS_Check_A, string pLydo, string pGhichu) {
			//if (check.Time.Date <= XL2.NgayCuoiThangKetCong && XL2.NgayCuoiThangKetCong != DateTime.MinValue) return; //tbd temp patch

			if (DAO.ThemGioChoNV(MaCC, check.Time, check.Type, check.MachineNo, pLydo, pGhichu)) {
				DS_Check_A.Add(check);
				DS_Check_A.Sort(new cCheckComparer());
			}
		}

		public static bool XoaGioChoNV(int MaCC, cCheck check, List<cCheck> DS_Check_A, string lydo, string ghichu) {
			//if (check.Time.Date <= XL2.NgayCuoiThangKetCong && XL2.NgayCuoiThangKetCong != DateTime.MinValue) return false; //tbd temp patch

			if (DAO.XoaGioChoNV(MaCC, check.Time, check.Source, check.MachineNo, lydo, ghichu)) {
				var indexCheck = DS_Check_A.FindIndex(o => o.Time == check.Time && o.MachineNo == check.MachineNo && o.Source == check.Source);

				if (indexCheck < 0) return false;
				var prevCheck = (indexCheck == 0) ? null : DS_Check_A[indexCheck - 1];
				var nextCheck = (indexCheck == DS_Check_A.Count - 1) ? null : DS_Check_A[indexCheck + 1];

				DS_Check_A.Remove(check);

			}
			return true;

		}

		public static void SuaGioChoNV(int MaCC, cCheck checkold, cCheck checknew, List<cCheck> DS_Check_A, string lydo, string ghichu) {

			if (DAO.SuaGioChoNV(MaCC, checkold.Time, checknew.Time, checkold.Source, checknew.Source, checkold.MachineNo, checknew.MachineNo, checkold.PhucHoi.IDGioGoc, lydo, ghichu)) {
				DS_Check_A.Remove(checkold);
				DS_Check_A.Add(checknew);
				DS_Check_A.Sort(new cCheckComparer());
			}
		}

		public static void ThemNgayVang(IEnumerable<dynamic> list, float workingDay, float workingTime, float PhuCapVang, string absentCode, List<Error> listError) {
			foreach (dynamic obj in list) {
				bool kqKiemTraMauThuan = XL.KiemtraCoMauThuanLoaiVang(obj, absentCode, obj.NgayVang, listError);
				if (kqKiemTraMauThuan) continue;// nếu bị mâu thuẫn loại vắng thì chuyển qua cái kế tiếp
				var ngayVang = (DateTime)obj.NgayVang;
				//BH dài ngày vào chủ nhật, Ro dài ngày vào chủ nhật thì set workingday về 0
				if ((absentCode == "BD" || absentCode == "TS") && (ngayVang).DayOfWeek == DayOfWeek.Sunday) {
					var NewWKDay = 0f;
					DAO.ThemNgayVang(obj.MaCC, ngayVang, NewWKDay, workingTime, PhuCapVang, absentCode);
				}
				else {
					DAO.ThemNgayVang(obj.MaCC, ngayVang, workingDay, workingTime, PhuCapVang, absentCode);
				}
			}
		}

		private static bool KiemtraCoMauThuanLoaiVang(dynamic obj, string absentCode, DateTime ngayVang, List<Error> listError) {
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
					if (DAO.KiemtraTonTaiLoaiVang("TS", ngayVang, (int)obj.MaCC)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang vắng thai sản", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					break;
				case "BD": // bh ko di chung TS
					if (DAO.KiemtraTonTaiLoaiVang("TS", ngayVang, (int)obj.MaCC)) {
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
					if (DAO.KiemtraTonTaiLoaiVang("TS", ngayVang, (int)obj.MaCC)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang vắng thai sản", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					break;

				#endregion

				#region ts ko đi chung với tất cả các loại khác

				case "TS": //ts  ko cho phép chủ nhật
					if (DAO.KiemtraTonTaiLoaiVang("TS", ngayVang, (int)obj.MaCC)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang vắng thai sản ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO.KiemtraTonTaiLoaiVang("BH", ngayVang, (int)obj.MaCC)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang vắng bảo hiểm ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO.KiemtraTonTaiLoaiVang("BD", ngayVang, (int)obj.MaCC)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang vắng bảo hiểm ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO.KiemtraTonTaiLoaiVang("RO", ngayVang, (int)obj.MaCC)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang vắng việc riêng ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO.KiemtraTonTaiLoaiVang("H", ngayVang, (int)obj.MaCC)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang họp, học ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO.KiemtraTonTaiLoaiVang("CT", ngayVang, (int)obj.MaCC)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} đang công tác ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO.KiemtraTonTaiLoaiVang("PT", ngayVang, (int)obj.MaCC)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} phong trào ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO.KiemtraTonTaiLoaiVang("PD", ngayVang, (int)obj.MaCC)) {
						kq = true;
						listError.Add(new Error { L = string.Format("Không thể báo vắng {0}", absentCode), ND = string.Format("Không thể báo vắng {0} vào ngày {1} phong trào ", absentCode, ngayVang.ToString("dd/MM/yyyy")) });
					}
					if (DAO.KiemtraTonTaiLoaiVang("P", ngayVang, (int)obj.MaCC)) {
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
			Bang_PH_Them = DAO.LayBang_PH_Them(arrMaCC, ngayBD, ngayKT, Bang_PH_Them);
		}

		internal static void LayBang_PH_Xoaa(List<cUserInfo> listNV, DateTime ngayBD, DateTime ngayKT, ref DataTable m_Bang_PH_Xoaa) {
			var arrMaCC = (from nv in listNV select nv.MaCC).ToArray();
			m_Bang_PH_Xoaa = DAO.LayBang_PH_Xoa(arrMaCC, ngayBD, ngayKT, m_Bang_PH_Xoaa);
		}

		internal static void LayBang_PH_GioGoc(List<cUserInfo> listNV, DateTime ngayBD, DateTime ngayKT, ref DataTable m_Bang_PH_GioGoc) {
			var arrMaCC = (from nv in listNV select nv.MaCC).ToArray();
			m_Bang_PH_GioGoc = DAO.LayBang_PH_GioGoc(arrMaCC, ngayBD, ngayKT, m_Bang_PH_GioGoc);
		}

		internal static void LayBang_GioDaXN(List<cUserInfo> listNV, DateTime ngayBD, DateTime ngayKT, ref DataTable m_Bang_GioDaXN) {
			m_Bang_GioDaXN.Rows.Clear();
			var arrMaCC = (from nv in listNV select nv.MaCC).ToArray();
			var table01 = DAO.LayBang_GioDaXN2(arrMaCC, ngayBD, ngayKT, m_Bang_GioDaXN);
			if (table01.Rows.Count == 0) { return; }
			var arrID = (from row in table01.Rows.Cast<DataRow>() let id = (int)row["IDXNCa_LamThem"] select id).ToArray();
			var table02 = DAO.LayBang_GioDaXN3(arrID, m_Bang_GioDaXN);
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
			DAO.HuyXN_GioChamCong(ID);
		}

	}

}
