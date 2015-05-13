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

		#endregion
		private static readonly ILog log = LogManager.GetLogger("XL");
		public static List<cCa> DSCa = new List<cCa>();
		public static List<cCa> DSCaMoRong = new List<cCa>();
		public static List<cShiftSchedule> DSLichTrinh = new List<cShiftSchedule>();

		public static DateTime ThuocNgayCong(DateTime chkinn)// lưu ý không dùng cho các giờ ra, giờ ra thiếu vào, giờ vào thiếu ra
		{
			return chkinn.TimeOfDay < XL2._03gio ? chkinn.Date.AddDays(-1) : chkinn.Date;
		}

		public static void Vao(DateTime timeinn, DateTime onnduty, DateTime chopheptre, out DateTime td_batdau_lv, out TimeSpan tre) {
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

		public static void Raa(DateTime timeout, DateTime offduty, DateTime chophepsom, out DateTime td_ketthuc_lv_chuaOT, out TimeSpan som) {
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
				BDLamDem = bd_lv > startNT ? bd_lv : startNT;
				KTLamDem = kt_lv_DaCoOT < endddNT ? kt_lv_DaCoOT : endddNT;
				tempTGLamDem = KTLamDem - BDLamDem;
			}
			else tempTGLamDem = TimeSpan.Zero;
			if (tempTGLamDem < XL2.TS_Default_LamDemToiThieu) {
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

		public static TimeSpan Tinh_TGLamTangCuong(TimeSpan TGGioLamViec) {
			return (TGGioLamViec - XL2._08gio) >= XL2._01phut ? (TGGioLamViec - XL2._08gio) : TimeSpan.Zero;
		}

		public static void Tinh_TGLamTCC3_5(TimeSpan TongGioLamViec, TimeSpan TongGioTangCuong, TimeSpan TongGioLamDem,
			out TimeSpan TongGioLamNgay, out TimeSpan KoHuongPC_TangCuongNgay, out TimeSpan HuongPC_TangCuongNgay, out TimeSpan HuongPC_Dem, out TimeSpan HuongPC_TangCuongDem) {
			KoHuongPC_TangCuongNgay = TimeSpan.Zero;
			HuongPC_TangCuongNgay = TimeSpan.Zero;
			TongGioLamNgay = TimeSpan.Zero;
			HuongPC_Dem = TimeSpan.Zero;
			HuongPC_TangCuongDem = TimeSpan.Zero;
			if (TongGioLamViec <= TimeSpan.Zero) return;

			if (TongGioTangCuong == TimeSpan.Zero) {
				if (TongGioLamDem == TimeSpan.Zero) {
					KoHuongPC_TangCuongNgay = TongGioLamViec;
					//HuongPC_TangCuongNgay = 0;
					TongGioLamNgay = TongGioLamViec;
					//HuongPC_Dem = 0;
					//HuongPC_TangCuongDem = 0;
				}
				else {
					KoHuongPC_TangCuongNgay = TongGioLamViec - TongGioLamDem;
					//HuongPC_TangCuongNgay = 0;
					TongGioLamNgay = KoHuongPC_TangCuongNgay;
					HuongPC_Dem = TongGioLamDem;
					//HuongPC_TangCuongDem = 0;
				}
			}
			else {
				if (TongGioLamDem == TimeSpan.Zero) {
					KoHuongPC_TangCuongNgay = XL2._08gio;
					HuongPC_TangCuongNgay = TongGioLamViec - XL2._08gio;
					TongGioLamNgay = TongGioLamViec;
					//HuongPC_Dem = 0;
					//HuongPC_TangCuongDem = 0;
				}
				else {
					TongGioLamNgay = TongGioLamViec - TongGioLamDem;
					if (TongGioLamNgay > XL2._08gio) {
						KoHuongPC_TangCuongNgay = XL2._08gio;
						HuongPC_TangCuongNgay = TongGioLamNgay - XL2._08gio;
						//TongGioLamNgay = TongGioLamViec - TongGioLamDem;
						//HuongPC_Dem = 0;
						HuongPC_TangCuongDem = TongGioLamDem;
					}
					else {
						KoHuongPC_TangCuongNgay = TongGioLamNgay;
						//HuongPC_TangCuongNgay = 0;
						//TongGioLamNgay = TongGioLamViec - TongGioLamDem;
						HuongPC_TangCuongDem = TongGioLamViec - XL2._08gio;
						HuongPC_Dem = TongGioLamDem - HuongPC_TangCuongDem;
					}
				}
			}
		}


		public static void TaoCaTuDo(cCa Ca, DateTime CheckInTime) {
			//var temp = CheckInTime.TimeOfDay;//ver 4.0.0.0//tbd xem lại ngày công
			var temp = new TimeSpan(CheckInTime.TimeOfDay.Hours, CheckInTime.TimeOfDay.Minutes, 0);//ver 4.0.0.1 bỏ phần giây, chỉ giữ phần giờ, phút
			if (CheckInTime.TimeOfDay < XL2._03gio) temp = Ca.TOD_Duty.Onn.Add(XL2._1ngay); //ThuocCa 3 , ThuocCa 3 va 1 vẫn giữ nguyên vì 21h > 4h//tbd xem lại ngày công
			if (Ca.ID == int.MinValue + 0) {
				Ca.TOD_Duty = new TS { Onn = temp, Off = temp.Add(XL2._08gio) };
				Ca.WorkingTimeTS = XL2._08gio;
				Ca.Workingday = 1f;
				Ca.Code = mySetting.Default.shiftCodeCa8h;
				Ca.MoTa = string.Format(mySetting.Default.MoTaCaTuDo, 8);
				Ca.KyHieuCC = mySetting.Default.kyHieuCCCa8h;
			}
			else if (Ca.ID == int.MinValue + 1) {
				Ca.TOD_Duty = new TS { Onn = temp, Off = temp.Add(XL2._12gio) };
				Ca.WorkingTimeTS = XL2._12gio;
				Ca.Workingday = 1.5f;
				Ca.Code = mySetting.Default.shiftCodeCa12h;
				Ca.MoTa = string.Format(mySetting.Default.MoTaCaTuDo, 12);
				Ca.KyHieuCC = mySetting.Default.kyHieuCCCa12h;
			}
			else if (Ca.ID == int.MinValue + 2) {
				Ca.TOD_Duty = new TS { Onn = temp, Off = temp.Add(XL2._04gio) };
				Ca.WorkingTimeTS = XL2._04gio;
				Ca.Workingday = 0.5f;
				Ca.Code = mySetting.Default.shiftCodeCa4h;
				Ca.MoTa = string.Format(mySetting.Default.MoTaCaTuDo, 4);
				Ca.KyHieuCC = mySetting.Default.kyHieuCCCa4h;
			}
			else if (Ca.ID == int.MinValue + 3) {
				Ca.TOD_Duty = new TS { Onn = temp, Off = temp.Add(XL2._16gio) };
				Ca.WorkingTimeTS = XL2._16gio;
				Ca.Workingday = 2f;
				Ca.Code = mySetting.Default.shiftCodeCa16h;
				Ca.MoTa = string.Format(mySetting.Default.MoTaCaTuDo, 16);
				Ca.KyHieuCC = mySetting.Default.kyHieuCCCa16h;
			}

			//Ca.AfterOTMin = XL2.LamThemAfterOT;
			Ca.PhutToiThieuTinhOT = XL2.default_PhutAfterOTMin;
			Ca.PhutChoTre = XL2.default_PhutChoTre;
			Ca.PhutChoSom = XL2.default_PhutChoSom;
			Ca.PhutNghiTrua = 0;
			Ca.TOD_NightTime = new TS { Onn = XL2._22h00, Off = XL2._06h00 };
			Ca.TOD_NhanDienVao = new TS { Onn = Ca.TOD_Duty.Onn.Subtract(XL2._10phut), Off = Ca.TOD_Duty.Onn.Add(XL2._10phut) };
			Ca.TOD_NhanDienRaa = new TS { Onn = Ca.TOD_Duty.Off.Subtract(XL2._10phut), Off = Ca.TOD_Duty.Off.Add(XL2._10phut) };
			Ca.idCaTruoc = -1;
			Ca.idCaSauuu = -1;
			//??? tbd Ca.DayCount
			//??? tbd	QuaDem = (iDayCount == 1),
			//??? tbd	TachCaDem = tachcadem,

		}


		//------------------------------------------------------------------------------------------------------------------------------------------
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
					DaXN = true,
					Vao = chkInnV,
					Raa = chkOutV,
					TimeDaiDien = chkInnV.Time,//HaveINOUT = 0, m?c d?nh = 0
					ShiftID = shiftID,
					DuyetChoPhepVaoTre = duyetChoPhepVaoTre,
					DuyetChoPhepRaSom = duyetChoPhepRaSom,
					VaoTreTinhCV = vaoTreTinhCV,
					RaaSomTinhCV = raaSomTinhCV,//ver 4.0.0.4	
					ThuocNgayCong = ThuocNgayCong(chkInnV.Time),
					OTMin = otMin
				};
				DS_CIO_V.Add(chkInOutV);
				#endregion

				// sau khi th?c hi?n xong th́ tang 
				x = x + 2;
				y = y + 2;
			}

		}
		public static void LoadDSXNPC5(int tempMaCC, DataTable tableXN_PCTC, out List<DataRow> dsXacNhanPC) {
			dsXacNhanPC = new List<DataRow>();
			dsXacNhanPC.AddRange(tableXN_PCTC.Select("UserEnrollNumber=" + tempMaCC));
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

		public static void XetCa_ListCIO_A3_5(List<cCheckInOut> ds_CIO_A, cShiftSchedule lichtrinh, List<cCheck> ds_raa3_vao1, List<cCheck> ds_check_A) {
			var i = 0;
			while (i < ds_CIO_A.Count) {
				var CIO = ds_CIO_A[i];
				bool isTachCaDem;
				cCheck checkRaa3, checkVao1;
				cCheckInOut cio_ca3, cio_ca1;
				cCa thuocCa_Ca3, thuocCa_Ca1;
				/* Xét ca 1 CIO , gặp trường hợp ca 3 ca 1 thì lưu lại danh sách check đệm giờ giao ca để thực hiện thêm dưới csdl*/
				XetCa_1_CIO_A5(CIO, lichtrinh, out CIO.ThuocNgayCong, out CIO.ThuocCa, out isTachCaDem, out CIO.DSCa,
					out checkRaa3, out checkVao1, out cio_ca3, out cio_ca1, out thuocCa_Ca3, out thuocCa_Ca1);

				#region xử lý trường hợp tách ca 3 ca 1

				if (isTachCaDem) {
					ds_raa3_vao1.Add(checkRaa3);
					ds_raa3_vao1.Add(checkVao1); //vaoca1
					ds_check_A.Add(checkRaa3); //raaca3
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

				#endregion
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
						ID = int.MinValue,
						Type = "O",
						MachineNo = 22,
						Source = "PC",
						Time = ThuocNgayCong.Add(ThuocCa_Ca3.TOD_Duty.Off),
						MaCC = CIO.Vao.MaCC,
					}; // đáng lẽ IDgiờgốc là -1 vì giờ này mới thêm chưa bị sửa nhưng do đây là giờ đệm ko có trong csdl nên để max, ko cho update
					CheckVao1 = new cCheck {
						ID = int.MinValue,
						Type = "I",
						MachineNo = 21,
						Source = "PC",
						Time = ThuocNgayCong.AddDays(1d).Date.Add(ThuocCa_Ca1.TOD_Duty.Onn).Add(XL2._01giay),
						MaCC = CIO.Raa.MaCC,
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


		public static cCa KiemtraThuocCa(DateTime t_vao, DateTime t_raa, DateTime ngay, List<cCa> dsCa) {
			return dsCa.FirstOrDefault(ca => t_vao >= ngay.Add(ca.TOD_NhanDienVao.Onn) && t_vao <= ngay.Add(ca.TOD_NhanDienVao.Off)
											 && t_raa >= ngay.Add(ca.TOD_NhanDienRaa.Onn) && t_raa <= ngay.Add(ca.TOD_NhanDienRaa.Off));
		}

		public static bool KiemtraThuocCa(DateTime t_vao, DateTime t_raa, DateTime ngay, cCa ca) {
			return (t_vao >= ngay.Add(ca.TOD_NhanDienVao.Onn) && t_vao <= ngay.Add(ca.TOD_NhanDienVao.Off)
											 && t_raa >= ngay.Add(ca.TOD_NhanDienRaa.Onn) && t_raa <= ngay.Add(ca.TOD_NhanDienRaa.Off));
		}

		public static List<cCa> Tim_DSCa_NhanDienDuoc(DateTime time, DateTime ngay, int HaveINOUT, List<cCa> dsCa) {
			var kq = (HaveINOUT == -1)
						 ? dsCa.FindAll(ca => time >= ngay.Add(ca.TOD_NhanDienVao.Onn) && time <= ngay.Add(ca.TOD_NhanDienVao.Off))
						 : dsCa.FindAll(ca => time >= ngay.Add(ca.TOD_NhanDienRaa.Onn) && time <= ngay.Add(ca.TOD_NhanDienRaa.Off));
			return kq;
		}



		public static void XetCa_ListCIO_V4_5(List<cCheckInOut> ds_CIO_V) {
			foreach (cCheckInOut chkInOutV in ds_CIO_V) {
				XetCa_1_CIO_V_5(chkInOutV, out chkInOutV.ThuocNgayCong, out chkInOutV.ThuocCa);
			}

		}

		public static void XetCa_1_CIO_V_5(cCheckInOut chkInOutV, out DateTime ThuocNgayCong, out cCa ThuocCa) {
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
				ThuocCa = new cCa { ID = shiftID };
				TaoCaTuDo(ThuocCa, chkInOutV.Vao.Time);
			}
		}

		public static void TronDS_CIO_A_V5(List<cCheckInOut> dsCIO_A, List<cCheckInOut> dsCIO_V, out List<cCheckInOut> kq) {
			kq = new List<cCheckInOut>();
			kq.AddRange(dsCIO_A);
			kq.AddRange(dsCIO_V);
			kq.Sort(new cCheckInOutComparer());
		}


		public static void TinhTG_LV_LVCa3_LamThem_1CIO5(DateTime ThuocNgayCong, int HaveINOUT, Boolean DaXN,
			bool KoTruVaoTre, bool KoTruRaaSom,//bool VaotreTinhCV, bool RaaSomTinhCV, //ver 4.0.0.4	
			DateTime Vao, DateTime Raa,
			TimeSpan TOD_DutyOnn, TimeSpan TOD_DutyOff, TimeSpan TS_PhutChoTre, TimeSpan TS_PhutChoSom, TimeSpan TS_BatDauOT,
			TimeSpan TS_LunchMin, TimeSpan TS_SoPhutLamThemDaXN,
			TS TOD_NightTime, //ver 4.0.0.4
			out DateTime TD_BD_LV, out DateTime TD_KT_LV, out DateTime TD_KT_LV_TrongCa,
			out DateTime TD_BD_LV_Ca3, out DateTime TD_KT_LV_Ca3,
			out TimeSpan TGThucTe, out TimeSpan TGGioLamViec, out TimeSpan TGVaoTre, out TimeSpan TGRaaSom,
			out TimeSpan TGGioLamViecTrongCa, //ver 4.0.0.4	
			out TimeSpan TGOLai, out bool QuaDem, out TimeSpan TGLamBanDem) {

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
			TGLamBanDem = TimeSpan.Zero;
			QuaDem = false;

			#endregion

			if (HaveINOUT < 0) return;

			var TD_BD_Ca = ThuocNgayCong.Add(TOD_DutyOnn);
			var TD_KT_Ca = ThuocNgayCong.Add(TOD_DutyOff);//off duty này đã bao gồm daycount được công bên trong
			var thoidiem_BD_tinhtre = TD_BD_Ca.Add(TS_PhutChoTre);
			var thoidiem_BD_tinhsom = TD_KT_Ca.Add(TS_PhutChoSom);
			var thoidiem_BD_tinhOLai = TD_KT_Ca.Add(TS_BatDauOT);
			var tmpBDLamDem = ThuocNgayCong.Add(TOD_NightTime.Onn);//ver 4.0.0.4
			var tmpKTLamDem = ThuocNgayCong.AddDays(1d).Add(TOD_NightTime.Off);//ver 4.0.0.4

			bool quadem;

			TGThucTe = Raa - Vao;
			// kiểm tra giờ ra ko được nhỏ hơn vào ThuocCa, giờ vào ko được nhỏ hơn ra ThuocCa
			if (Raa < TD_BD_Ca || Vao > TD_KT_Ca) {
				return;
			}
			XL.Vao(Vao, TD_BD_Ca, thoidiem_BD_tinhtre, out TD_BD_LV, out TGVaoTre);
			if (KoTruVaoTre) {
				TD_BD_LV = TD_BD_Ca;
				TGVaoTre = TimeSpan.Zero;
			}

			XL.Raa(Raa, TD_KT_Ca, thoidiem_BD_tinhsom, out TD_KT_LV_TrongCa, out TGRaaSom);
			if (KoTruRaaSom) {
				TD_KT_LV_TrongCa = TD_KT_Ca;
				TGRaaSom = TimeSpan.Zero;
			}

			XL.OLai(Raa, TD_KT_Ca, thoidiem_BD_tinhOLai, out TGOLai);
			Tinh_TGLamViecTrongCa(TD_BD_LV, TD_KT_LV_TrongCa, TS_LunchMin, out TGGioLamViecTrongCa);
			TD_KT_LV = TD_KT_LV_TrongCa; // giờ chưa qua xác nhận thì kt làm việc là chưa tính OT
			if (DaXN) TD_KT_LV = TD_KT_LV_TrongCa.Add(TS_SoPhutLamThemDaXN);
			Tinh_TGLamViec(TD_BD_LV, TD_KT_LV, TS_LunchMin, out TGGioLamViec);// lúc này TD_KT_LV là kết thúc ThuocCa nếu chưa XN, nếu đã XN thì = (TD_KT_LV + khoảng OT)
			Tinh_TGLamViec_Ca3(TD_BD_LV, TD_KT_LV, tmpBDLamDem, tmpKTLamDem, out TD_BD_LV_Ca3, out  TD_KT_LV_Ca3, out TGLamBanDem, out quadem);
			//TongTGLamTangCuong = Tinh_TGLamTangCuong(TGGioLamViec);//(TGGioLamViec - XL2._08gio) >= XL2._01phut ? (TGGioLamViec - XL2._08gio) : TimeSpan.Zero;
			QuaDem = quadem;
		}



		public static void TinhCong_ListNgayCong8_5(List<cNgayCong> dsNgayCong) {
			foreach (var ngayCong in dsNgayCong) {
				TinhCong_1Ngay5(ngayCong, out ngayCong.TG5, out ngayCong.QuaDem);
				if (ngayCong.QuaDem) {
					TinhPhuCap_1NgayQuaDem5(ngayCong.TG5.TongGioLamDem, XL2.HSPCDem_NgayThuong, out ngayCong.PhuCaps.PCDem5);
					ngayCong.PhuCaps._TongPC = ngayCong.PhuCaps.PCDem5;
				}
			}
		}

		public static void TinhCong_1Ngay5(cNgayCong ngayCong, out structThoiGianTheoNgayCong TG, out bool QuaDem) {
			TG = new structThoiGianTheoNgayCong();
			QuaDem = false;
			ngayCong.TrangThaiDiemDanh = TrangThaiDiemDanh.VANG_NGHI;
			// tính công của từng ThuocCa làm việc, sau đó tổng hợp Công làm việc của 1 ngày
			if (ngayCong.DSVaoRa.Count == 0) return;
			foreach (var CIO in ngayCong.DSVaoRa) {
				CIO.TD5 = new structThoiDiem();
				CIO.TG5 = new structThoiGianTheoCIO();
				if (CIO.HaveINOUT < 0) continue;
				if (CIO.DaXN) CIO.TG5.SoPhutLamThem5 = new TimeSpan(0, CIO.OTMin, 0);
				TinhTG_LV_LVCa3_LamThem_1CIO5(CIO.ThuocNgayCong, CIO.HaveINOUT, CIO.DaXN, CIO.DuyetChoPhepVaoTre, CIO.DuyetChoPhepRaSom,
					CIO.Vao.Time, CIO.Raa.Time, CIO.ThuocCa.TOD_Duty.Onn, CIO.ThuocCa.TOD_Duty.Off, CIO.ThuocCa.TS_PhutChoTre, CIO.ThuocCa.TS_PhutChoSom,
					CIO.ThuocCa.TS_PhutAfterOT, CIO.ThuocCa.TS_PhutNghiTrua, CIO.TG5.SoPhutLamThem5, CIO.ThuocCa.TOD_NightTime,
					out CIO.TD5.BD_LV, out CIO.TD5.KT_LV, out CIO.TD5.KT_LV_ChuaOT, out CIO.TD5.BD_LV_Ca3, out CIO.TD5.KT_LV_Ca3,
					out CIO.TG5.GioThucTe5, out CIO.TG5.TongGioLamViec5, out CIO.TG5.VaoTre, out CIO.TG5.RaaSom,
					out CIO.TG5.GioLVTrongCa5,//ver 4.0.0.4	
					out CIO.TG5.OLai, out CIO.QuaDem, out CIO.TG5.TongGioLamDem);
				if (CIO.QuaDem) QuaDem = true;
				TinhCong_1_CIO_5(CIO.ThuocCa.Workingday, CIO.ThuocCa.WorkingTimeTS, CIO.TG5.VaoTre, CIO.TG5.RaaSom,
					CIO.VaoTreTinhCV, CIO.RaaSomTinhCV, CIO.TG5.SoPhutLamThem5,
								 out CIO.Cong5.CaQuyDinh, out CIO.Cong5.TTCongTre, out CIO.Cong5.TTCongSom,
								 out CIO.Cong5.TTTrongCa, out CIO.Cong5.TTNgoaiCa, out CIO.Cong5.ThucTe,
								 out CIO.Cong5.TongCongBu, out CIO.Cong5.TongCongTru, out CIO.Cong5.DinhMuc);

				CongDonThoiGian_1NgayCong(ref ngayCong.TG5, ref CIO.TG5);
				CongDonCong_1NgayCong(ref ngayCong.Cong5, ref CIO.Cong5);
			}

			ngayCong.TG5.TongGioTangCuong = Tinh_TGLamTangCuong(ngayCong.TG5.TongGioLamViec);
			Tinh_TGLamTCC3_5(ngayCong.TG5.TongGioLamViec, ngayCong.TG5.TongGioTangCuong, ngayCong.TG5.TongGioLamDem,
				out ngayCong.TG5.TongGioLamNgay, out ngayCong.TG5.GioLamNgay_KoTC,
				out ngayCong.TG5.HuongPC_TangCuongNgay, out ngayCong.TG5.HuongPC_Dem, out ngayCong.TG5.HuongPC_TangCuongDem);
		}

		public static void CongDonThoiGian_1NgayCong(ref structThoiGianTheoNgayCong ngayCong, ref structThoiGianTheoCIO CIO) {
			ngayCong.TongGioLamViec += CIO.TongGioLamViec5;
			//ngayCong.GioLVTrongCa5 += CIO.GioLVTrongCa5;
			ngayCong.TongGioThucTe += CIO.GioThucTe5;
			ngayCong.TongGioLamDem += CIO.TongGioLamDem;
			//ngayCong.TongGioTangCuong += CIO.TongGioTangCuong;
			//ngayCong.OLai += CIO.OLai;
			ngayCong.VaoTre += CIO.VaoTre;
			ngayCong.RaaSom += CIO.RaaSom;
		}
		public static void CongDonCong_1NgayCong(ref structCong NgayCong, ref structCong CIO) {
			NgayCong.CaQuyDinh += CIO.CaQuyDinh;
			NgayCong.DinhMuc += CIO.DinhMuc;
			NgayCong.ThucTe += CIO.ThucTe;
			NgayCong.TongCongBu += CIO.TongCongBu;
			NgayCong.TongCongTru += CIO.TongCongTru;
			NgayCong.TTCongSom += CIO.TTCongSom;
			NgayCong.TTCongTre += CIO.TTCongTre;
			NgayCong.TTNgoaiCa += CIO.TTNgoaiCa;
			NgayCong.TTTrongCa += CIO.TTTrongCa;
		}
		public static float TinhCongTre5(TimeSpan GioVaoTre, TimeSpan GioLam_CaQuyDinh, float Cong_DoCaQuyDinh, int LamTronConSo) {
			return ((GioVaoTre.TotalHours / GioLam_CaQuyDinh.TotalHours) * Cong_DoCaQuyDinh).Truncate(LamTronConSo); // làm tròn xuống 2 con số
		}
		public static float TinhCongSom5(TimeSpan GioRaaSom, TimeSpan GioLam_CaQuyDinh, float Cong_DoCaQuyDinh, int LamTronConSo) {
			return ((GioRaaSom.TotalHours / GioLam_CaQuyDinh.TotalHours) * Cong_DoCaQuyDinh).Truncate(LamTronConSo); // làm tròn xuống 2 con số
		}
		public static float TinhCongThucTeTrongCa(float Cong_DoCaQuyDinh, float CongTre, float CongSom) {
			return Cong_DoCaQuyDinh - CongTre - CongSom; // làm tròn xuống 2 con số
		}
		public static void TinhCong_1_CIO_5(float Cong_DoCaQuyDinh, TimeSpan GioLam_CaQuyDinh, TimeSpan GioVaoTre, TimeSpan GioRaaSom,
			bool VaoTreTinhCV, bool RaaSomTinhCV, TimeSpan SoPhutLamThem5,
			out float CongCaQuyDinh, out float CongTre, out float CongSom,
			out float CongThucTeTrongCa, out float CongThucTeNgoaiCa, out float CongThucTe,
			out float TongCongBu, out float TongCongTru, out float DinhMucCong) {
			CongCaQuyDinh = Cong_DoCaQuyDinh;
			CongTre = XL.TinhCongTre5(GioVaoTre, GioLam_CaQuyDinh, Cong_DoCaQuyDinh, 2);// làm tròn xuống 2 con số
			CongSom = XL.TinhCongSom5(GioRaaSom, GioLam_CaQuyDinh, Cong_DoCaQuyDinh, 2);// làm tròn xuống 2 con số
			CongThucTeTrongCa = XL.TinhCongThucTeTrongCa(Cong_DoCaQuyDinh, CongTre, CongSom);
			CongThucTeNgoaiCa = Convert.ToSingle(Math.Round((SoPhutLamThem5.TotalHours / 8d), 2));// tương đương giờ làm việc ngoài ThuocCa, làm ngoài ThuocCa chưa chắc OT ví dụ nửa ThuocCa
			CongThucTe = CongThucTeTrongCa + CongThucTeNgoaiCa;
			DinhMucCong = Cong_DoCaQuyDinh + CongThucTeNgoaiCa;
			//nếu đã duyệt cho phép vào trễ rồi thì giờ vào trễ = 0, công vào trễ =0
			TongCongBu = 0f;
			TongCongTru = 0f;
			if (VaoTreTinhCV) {
				TongCongBu += CongTre;
				DinhMucCong -= CongTre;
			}
			else TongCongTru += CongTre;

			if (RaaSomTinhCV) {
				TongCongBu += CongSom;
				DinhMucCong -= CongSom;
			}
			else TongCongTru += CongSom;
		}

		public static void TinhPhuCap_1NgayQuaDem5(TimeSpan TongQuaDem, int HSPCDem_NgayThuong, out float phucapDem) {
			phucapDem = Convert.ToSingle(Math.Round((TongQuaDem.TotalHours / 8d) * (HSPCDem_NgayThuong / 100f), 2));
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
