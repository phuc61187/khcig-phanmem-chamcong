using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;
using ChamCong_v03.Properties;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using log4net;
using log4net.Config;

namespace ChamCong_v03.BUS {
	public static class XL {
		private static readonly ILog log = LogManager.GetLogger("XL");
		public static List<cCaChuan> DSCa = new List<cCaChuan>();
		public static List<cCaAbs> DSCaMoRong = new List<cCaAbs>();
		public static List<cShiftSchedule> DSLichTrinh = new List<cShiftSchedule>();
		#region tao cau truc datatable
		public static DataTable TaoCauTrucDataTable(string[] colName, Type[] colType) {
			var kq = new DataTable();
			for (var i = 0; i < colName.Length; i++) {
				kq.Columns.Add(colName[i], colType[i]);
			}
			return kq;
		}

		#endregion
		#region tạo DSCa mở rộng
		/// <summary>
		/// Tạo DS Ca mở rộng. Đã bao gồm item Khác(int.MinValue)
		/// </summary>
		/// <param name="tmpDSCa"></param>
		/// <returns></returns>
		public static List<cCaAbs> TaoDSCaMoRong(List<cCaAbs> tmpDSCa) {
			var kq = new List<cCaAbs>(tmpDSCa);
			List<cCaAbs> DSCa1cong = tmpDSCa.FindAll(item => item.Workingday == 1f);
			List<cCaAbs> DSCaNuaCong = tmpDSCa.FindAll(item => item.Workingday == 0.5f);
			if (DSCaNuaCong.Count == 0 || DSCa1cong.Count == 0) return new List<cCaAbs>(tmpDSCa);

			foreach (cCaChuan ca1Cong in DSCa1cong) {
				foreach (cCaChuan caNuaCong in DSCaNuaCong) {
					if (ca1Cong.OffTS == caNuaCong.OnnTS
						|| (ca1Cong.DayCount == 1 && ca1Cong.OffTS.Add(new TimeSpan(-ca1Cong.DayCount, 0, 0, 0)) == caNuaCong.OnnTS)) {
						#region collapse
						var tempShift = new cCaChuan {
							ID = -ca1Cong.ID,
							Code = ca1Cong.Code + " & " + caNuaCong.Code,
							DayCount = ca1Cong.DayCount + caNuaCong.DayCount, QuaDem = ((ca1Cong.DayCount + caNuaCong.DayCount) == 1),
							OnnTS = ca1Cong.OnnTS,
							OnnInnTS = ca1Cong.OnnInnTS,
							CutInnTS = ca1Cong.CutInnTS,
							OnnOutTS = caNuaCong.OnnOutTS.Add(new TimeSpan(ca1Cong.DayCount, 0, 0, 0)),
							CutOutTS = caNuaCong.CutOutTS.Add(new TimeSpan(ca1Cong.DayCount, 0, 0, 0)),
							AfterOTMin = caNuaCong.AfterOTMin,
							LateeMin = ca1Cong.LateeMin,
							EarlyMin = caNuaCong.EarlyMin,
							Workingday = ca1Cong.Workingday + caNuaCong.Workingday,
							ShowPosition = ca1Cong.ShowPosition,
							WorkingTimeTS = ca1Cong.WorkingTimeTS + caNuaCong.WorkingTimeTS,
							LunchMin = ca1Cong.LunchMin + caNuaCong.LunchMin,
							KyHieuCC = ca1Cong.KyHieuCC + "&" + caNuaCong.KyHieuCC,
						};
						tempShift.OffTS = ca1Cong.OnnTS.Add(ca1Cong.WorkingTimeTS).Add(caNuaCong.WorkingTimeTS);
						tempShift.chophepTreTS = tempShift.OnnTS + tempShift.LateeMin;
						tempShift.chophepSomTS = tempShift.OffTS - tempShift.EarlyMin;
						tempShift.batdaulamthemTS = tempShift.OffTS + tempShift.AfterOTMin;

						tempShift.catruoc = new cCaChuan(); tempShift.catruoc = ca1Cong;
						tempShift.casauuu = new cCaChuan(); tempShift.casauuu = caNuaCong;
						#endregion
						if (tempShift.DayCount == 1 && tempShift.OnnTS > XL2._20h00 && tempShift.Workingday > 1f)
							tempShift.TachCa = true;

						kq.Add(tempShift);
					}
					else if (caNuaCong.OffTS == ca1Cong.OnnTS) {
						#region collapse
						var tempShift = new cCaChuan {
							ID = -caNuaCong.ID,
							Code = caNuaCong.Code + " & " + ca1Cong.Code,
							DayCount = caNuaCong.DayCount + ca1Cong.DayCount, QuaDem = ((ca1Cong.DayCount + caNuaCong.DayCount) == 1),
							OnnTS = caNuaCong.OnnTS,
							OnnInnTS = caNuaCong.OnnInnTS,
							CutInnTS = caNuaCong.CutInnTS,
							OnnOutTS = ca1Cong.OnnOutTS.Add(new TimeSpan(caNuaCong.DayCount, 0, 0, 0)),
							CutOutTS = ca1Cong.CutOutTS.Add(new TimeSpan(caNuaCong.DayCount, 0, 0, 0)),
							AfterOTMin = ca1Cong.AfterOTMin,
							LateeMin = caNuaCong.LateeMin,
							EarlyMin = ca1Cong.EarlyMin,
							Workingday = caNuaCong.Workingday + ca1Cong.Workingday,
							ShowPosition = caNuaCong.ShowPosition,
							WorkingTimeTS = caNuaCong.WorkingTimeTS + ca1Cong.WorkingTimeTS,
							LunchMin = ca1Cong.LunchMin + caNuaCong.LunchMin,
							KyHieuCC = caNuaCong.KyHieuCC + "&" + ca1Cong.KyHieuCC,
						};
						tempShift.OffTS = caNuaCong.OnnTS.Add(caNuaCong.WorkingTimeTS).Add(ca1Cong.WorkingTimeTS);
						tempShift.chophepTreTS = tempShift.OnnTS + tempShift.LateeMin;
						tempShift.chophepSomTS = tempShift.OffTS - tempShift.EarlyMin;
						tempShift.batdaulamthemTS = tempShift.OffTS + tempShift.AfterOTMin;

						tempShift.catruoc = new cCaChuan(); tempShift.catruoc = caNuaCong;
						tempShift.casauuu = new cCaChuan(); tempShift.casauuu = ca1Cong;
						#endregion
						if (tempShift.DayCount == 1 && tempShift.OnnTS > XL2._20h00 && tempShift.Workingday > 1f)
							tempShift.TachCa = true;
						kq.Add(tempShift);
					}

				}
			}
			return kq;
		}


		#endregion

		public static DateTime ThuocNgayCong(DateTime chkinn)// lưu ý không dùng cho các giờ ra, giờ ra thiếu vào, giờ vào thiếu ra
		{
			return chkinn.TimeOfDay < XL2._04h30 ? chkinn.Date.AddDays(-1) : chkinn.Date;
		}

		public static void Vao(DateTime timeinn, DateTime onnduty, DateTime chopheptre, out DateTime vaolam, out TimeSpan tre) {
			if (chopheptre < timeinn && timeinn - chopheptre > XL2._01phut) {
				vaolam = timeinn;
				tre = timeinn - onnduty;
				tre = new TimeSpan(tre.Days, tre.Hours, tre.Minutes, 0);
			}
			else {
				vaolam = onnduty;
				tre = TimeSpan.Zero;
			}
		}

		public static void Raa(DateTime timeout, DateTime offduty, DateTime chophepsom, out DateTime raalam_ChuaOT, out TimeSpan som) {
			if (timeout < chophepsom && chophepsom - timeout > XL2._01phut) {
				raalam_ChuaOT = timeout;
				som = offduty - timeout;
				som = new TimeSpan(som.Days, som.Hours, som.Minutes, 0);
			}
			else {
				raalam_ChuaOT = offduty;
				som = TimeSpan.Zero;
			}
		}

		public static void OLai(DateTime timeout, DateTime offduty, DateTime batdaulamthem, out TimeSpan olai) {
			if (batdaulamthem < timeout && timeout - batdaulamthem > XL2._01phut) {
				olai = timeout - offduty;
				olai = new TimeSpan(olai.Days, olai.Hours, olai.Minutes, 0);
			}
			else {
				olai = TimeSpan.Zero;
			}
		}

		public static void LamThem(DateTime raalam_ChuaOT, TimeSpan OTmin, out DateTime RaLam_CoOT) {
			RaLam_CoOT = raalam_ChuaOT + OTmin;
		}

		public static void TGLamTinhCong(DateTime vaolam, DateTime raalam, TimeSpan LunchMin, out TimeSpan TGLamTinhCong) {
			TGLamTinhCong = (raalam - vaolam).Subtract(LunchMin);
		}

		public static void LamDem(DateTime VaoLam, DateTime RaaLam, DateTime _21h45, DateTime _05h45, out DateTime vaolamdem, out DateTime raalamdem, out TimeSpan TGLamDem, out bool QuaDem) {
			var tempTGLamDem = TimeSpan.Zero;
			var BDLamDem = DateTime.MinValue;
			var KTLamDem = DateTime.MinValue;
			if (RaaLam > _21h45) {
				BDLamDem = VaoLam > (_21h45 + XL2.ChoPhepTre + XL2._01phut) ? VaoLam : _21h45;
				KTLamDem = RaaLam < (_05h45 - XL2.ChoPhepSom - XL2._01phut) ? RaaLam : _05h45;
				tempTGLamDem = KTLamDem - BDLamDem;
			}
			else tempTGLamDem = TimeSpan.Zero;
			if (tempTGLamDem < XL2.TGLamDemToiThieu) {
				vaolamdem = DateTime.MinValue;
				raalamdem = DateTime.MinValue;
				TGLamDem = TimeSpan.Zero;
				QuaDem = false;
			}
			else {
				vaolamdem = BDLamDem;
				raalamdem = KTLamDem;
				TGLamDem = tempTGLamDem;
				QuaDem = true;
			}
		}

		public static void TinhPCDem_PCTC(TimeSpan SoGioTinhCong, TimeSpan SoGioLamDemmm, bool TinhPC50, bool QuaDem, TimeSpan SoGioLamThem,
			out TimeSpan tgTinh130, out TimeSpan tgTinh150, out TimeSpan tgTinhTCC3,
			out double PhuCap30, out double PhuCapTC, out double PhuCapTCC3, out double TongPhuCap) {

			var pctc = Convert.ToSingle(XL2.PC50) / 100f;
			var pcdem = Convert.ToSingle(XL2.PC30) / 100f; //tbd
			var pctcc3 = (Convert.ToSingle(XL2.PCTCC3)) / 100f;

			PhuCap30 = 0d;
			PhuCapTC = 0d;
			PhuCapTCC3 = 0d;
			TongPhuCap = 0d;
			tgTinh130 = TimeSpan.Zero;
			tgTinh150 = TimeSpan.Zero;
			tgTinhTCC3 = TimeSpan.Zero;
			if (QuaDem == false) {
				if (TinhPC50 && (SoGioLamThem > XL2._01phut)) {
					tgTinh150 = SoGioLamThem;
					PhuCapTC = ((SoGioLamThem.TotalHours / 8d) * pctc);
					TongPhuCap = PhuCapTC;
				}
			}
			else {
				if (TinhPC50 == false || (SoGioLamThem <= XL2._01phut)) {
					PhuCap30 = (SoGioLamDemmm.TotalHours / 8d) * pcdem;
					TongPhuCap = PhuCap30;
				}
				else {
					if (SoGioLamThem >= SoGioLamDemmm) // trọn qua đêm là tăng cường đêm, còn lại là tăng cường ngày
					{
						tgTinhTCC3 = SoGioLamDemmm;
						tgTinh150 = SoGioLamThem - SoGioLamDemmm; // số giờ tính pctc
						PhuCapTCC3 = (tgTinhTCC3.TotalHours / 8d) * pctcc3;
						PhuCapTC = (tgTinh150.TotalHours / 8d) * pctc;
						TongPhuCap = PhuCapTC + PhuCapTCC3;
					}
					else {
						tgTinhTCC3 = SoGioLamThem;
						tgTinh130 = SoGioLamDemmm - SoGioLamThem;
						PhuCapTCC3 = (tgTinhTCC3.TotalHours / 8d) * pctcc3;
						PhuCap30 = (tgTinh130.TotalHours / 8d) * pcdem;
						TongPhuCap = PhuCap30 + PhuCapTCC3;
					}
				}
			}

		}


		public static void TinhPCDB(TimeSpan SoGioTinhCong, TimeSpan SoGioLamDemmm, bool QuaDem, bool TinhPCDB,
			int loaiPC, float pcngay, float pcdem,
			out TimeSpan tgTinh200, out TimeSpan tgTinh260, out TimeSpan tgTinh300, out TimeSpan tgTinh390, out TimeSpan tgTinhCus,
			out double PhuCap100, out double PhuCap160, out double PhuCap200, out double PhuCap290, out double PhuCapCus, ref double TongPhuCap) {

			PhuCap100 = 0d;
			PhuCap160 = 0d;
			PhuCap200 = 0d;
			PhuCap290 = 0d;
			PhuCapCus = 0d;
			tgTinh200 = TimeSpan.Zero;
			tgTinh260 = TimeSpan.Zero;
			tgTinh300 = TimeSpan.Zero;
			tgTinh390 = TimeSpan.Zero;
			tgTinhCus = TimeSpan.Zero;
			if (TinhPCDB == false) {
				return;
			}
			else {
				switch (loaiPC) {
					case 200:
						tgTinh200 = SoGioTinhCong - SoGioLamDemmm;
						tgTinh260 = SoGioLamDemmm;
						PhuCap100 = (tgTinh200.TotalHours / 8d) * (pcngay / 100d);
						PhuCap160 = (tgTinh260.TotalHours / 8d) * (pcdem / 100d);
						TongPhuCap = PhuCap100 + PhuCap160;
						break;
					case 300:
						tgTinh300 = SoGioTinhCong - SoGioLamDemmm;
						tgTinh390 = SoGioLamDemmm;
						PhuCap200 = (tgTinh300.TotalHours / 8d) * (pcngay / 100d);
						PhuCap290 = (tgTinh390.TotalHours / 8d) * (pcdem / 100d);
						TongPhuCap = PhuCap200 + PhuCap290;
						break;
					case 1:
						tgTinhCus = ((SoGioTinhCong - XL2._08gio > XL2._01phut) ? (SoGioTinhCong - XL2._08gio) : TimeSpan.Zero);
						PhuCapCus = (tgTinhCus.TotalHours / 8d) * (pcngay / 100d);
						TongPhuCap = PhuCapCus;
						break;
					case 2:
						tgTinhCus = SoGioTinhCong;
						PhuCapCus = (tgTinhCus.TotalHours / 8d) * (pcngay / 100d);
						TongPhuCap = PhuCapCus;
						break;
				}
			}
		}


		public static void TaoCaTuDo(cCaTuDo Ca, DateTime CheckInTime, TimeSpan WorkingTime, TimeSpan SoPhutChoPhepTre, TimeSpan SoPhutChoPhepSomTS, TimeSpan SoPhutAfterOT, float WorkingDay, string kyhieu) {
			Ca.OnnTS = CheckInTime.TimeOfDay;//tbd xem lại ngày công
			if (CheckInTime.TimeOfDay < XL2._04h30) Ca.OnnTS = Ca.OnnTS.Add(XL2._1ngay); //ca 3 , ca 3 va 1 vẫn giữ nguyên vì 21h > 4h//tbd xem lại ngày công
			Ca.OffTS = Ca.OnnTS.Add(WorkingTime);
			Ca.LateeMin = SoPhutChoPhepTre;
			Ca.EarlyMin = SoPhutChoPhepSomTS;
			Ca.AfterOTMin = SoPhutAfterOT;
			Ca.chophepTreTS = Ca.OnnTS.Add(SoPhutChoPhepTre);
			Ca.chophepSomTS = Ca.OffTS.Subtract(SoPhutChoPhepSomTS);
			Ca.batdaulamthemTS = Ca.OffTS.Add(SoPhutAfterOT);
			Ca.DayCount = Ca.OffTS.Days;
			Ca.QuaDem = (Ca.OffTS.Days == 1);
			Ca.WorkingTimeTS = WorkingTime;
			Ca.Workingday = WorkingDay;
			Ca.LunchMin = XL2._0gio;
			Ca.KyHieuCC = kyhieu;
		}





		//------------------------------------------------------------------------------------------------------------------------------------------

		public static List<cUserInfo> KhoiTaoDSNV(List<cUserInfo> dsnv, DataTable tableDSNV) {
			if (tableDSNV.Rows.Count == 0) return dsnv;
			foreach (DataRow row in tableDSNV.Rows) {
				var nhanvien = new cUserInfo();
				nhanvien.MaCC = (int)row["UserEnrollNumber"];
				nhanvien.TenNV = (string)row["UserFullName"];
				nhanvien.MaNV = (string)row["UserFullCode"];
				nhanvien.UserIDTitle = (row["UserIDTitle"] != DBNull.Value) ? (int)row["UserIDTitle"] : 0;
				nhanvien.TitleName = (string)row["TitleName"];
				nhanvien.HeSo.LuongCB = (row["HeSoLuongCB"] != DBNull.Value) ? (Single)row["HeSoLuongCB"] : 0f;
				nhanvien.HeSo.LuongCV = (row["HeSoLuongSP"] != DBNull.Value) ? (Single)row["HeSoLuongSP"] : 0f;
				var hesocongthem = (row["HSBHCongThem"] != DBNull.Value) ? (Single)row["HSBHCongThem"] : 0f;
				nhanvien.HeSo.BHXH_YT_TN = nhanvien.HeSo.LuongCB + hesocongthem;// hệ số bảo hiểm, bình thường = hệ số lương cb. riêng gd, pgd thì cộng thêm HSBHCộngThêm + 0.5, 0.6
				nhanvien.Luong = new cChiTietLuong();
				nhanvien.MacDinhTinhPC50 = (row["TinhPC50"] != DBNull.Value) ? (bool)row["TinhPC50"] : false; //trong query là d1.TinhPC150 [140615_1]
				if (row["SchID"] == DBNull.Value) continue;
				var schid = (int)row["SchID"];
				nhanvien.LichTrinhLV = DSLichTrinh.Find(o => o.SchID == schid);
				if (row["IDD_1"] != DBNull.Value)
					nhanvien.PBCap1 = new cPhongBan { ID = (int)row["IDD_1"], TenPhongBan = row["Description_1"].ToString(), ViTri = (int)row["ViTri"] };
				if (row["IDD_2"] != DBNull.Value)
					nhanvien.PBCap2 = new cPhongBan { ID = (int)row["IDD_2"], TenPhongBan = row["Description_2"].ToString(), ViTri = 0 };//chỉ có phòng ban cấp 1 có vị trí

				dsnv.Add(nhanvien);
			}
			return dsnv;
		}
		public static List<cUserInfo> KhoiTaoDSNV_ThayDoiThongTin(List<cUserInfo> dsnv, DataTable tableDSNV) {
			if (tableDSNV.Rows.Count == 0) return dsnv;
			foreach (DataRow row in tableDSNV.Rows) {
				var nhanvien = new cUserInfo();
				nhanvien.MaCC = (int)row["UserEnrollNumber"];
				nhanvien.TenNV = (string)row["UserFullName"];
				nhanvien.MaNV = (string)row["UserFullCode"];
				nhanvien.UserIDTitle = (row["UserIDTitle"] != DBNull.Value) ? (int)row["UserIDTitle"] : 0;
				nhanvien.TitleName = (string)row["TitleName"];
				nhanvien.IsUserEnabled = (row["UserEnabled"] != DBNull.Value) ? (bool)row["UserEnabled"] : false;
				nhanvien.IsDailySalary = (row["TinhLuongCongNhat"] != DBNull.Value) ? (bool)row["TinhLuongCongNhat"] : false;
				nhanvien.HeSo.LuongCB = (row["HeSoLuongCB"] != DBNull.Value) ? (Single)row["HeSoLuongCB"] : 0f;
				nhanvien.HeSo.LuongCV = (row["HeSoLuongSP"] != DBNull.Value) ? (Single)row["HeSoLuongSP"] : 0f;
				var hesocongthem = (row["HSBHCongThem"] != DBNull.Value) ? (Single)row["HSBHCongThem"] : 0f;
				nhanvien.HeSo.BHXH_YT_TN = nhanvien.HeSo.LuongCB + hesocongthem;// hệ số bảo hiểm, bình thường = hệ số lương cb. riêng gd, pgd thì cộng thêm HSBHCộngThêm + 0.5, 0.6
				nhanvien.Luong = new cChiTietLuong();
				nhanvien.MacDinhTinhPC50 = (row["TinhPC50"] != DBNull.Value) ? (bool)row["TinhPC50"] : false; //trong query là d1.TinhPC150 [140615_1]
				if (row["SchID"] == DBNull.Value) continue;
				var schid = (int)row["SchID"];
				nhanvien.LichTrinhLV = DSLichTrinh.Find(o => o.SchID == schid);
				if (row["IDD_1"] != DBNull.Value)
					nhanvien.PBCap1 = new cPhongBan { ID = (int)row["IDD_1"], TenPhongBan = row["Description_1"].ToString(), ViTri = (int)row["ViTri"] };
				if (row["IDD_2"] != DBNull.Value)
					nhanvien.PBCap2 = new cPhongBan { ID = (int)row["IDD_2"], TenPhongBan = row["Description_2"].ToString(), ViTri = 0 };//chỉ có phòng ban cấp 1 có vị trí

				dsnv.Add(nhanvien);
			}
			return dsnv;
		}

		public static void DiemDanh(List<cUserInfo> dsnv, DateTime ngayBD_Bef2D, DateTime ngayKT_Aft2D) {
			var Arr_MaCC = (from nv in dsnv select nv.MaCC).ToArray();
			var tableCheck_A = DAL.LayTableCIO_A(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableCheck_V = DAL.LayTableCIO_V(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableVang = DAL.LayTableVang(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableXacNhanPC50 = DAL.LayTableXacNhanPC50(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableXacNhanPCDB = DAL.LayTableXacNhanPCDB(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D, Duyet: true);
			var tableNgayLe = DAL.DocNgayLe(ngayBD_Bef2D, ngayKT_Aft2D);
			var HaveHoliday = (tableNgayLe.Rows.Count > 0);

			var DS_Check_KoHopLe = new List<cChk>();
			var ds_raa3_vao1 = new List<cChk>();
			foreach (var nv in dsnv) {
				var tempMaCC = nv.MaCC;
				nv.NgayCongBD_Bef2D = ngayBD_Bef2D;
				nv.NgayCongKT_Aft2D = ngayKT_Aft2D;
				var arrCheck_A = tableCheck_A.Select("UserEnrollNumber = " + tempMaCC, "TimeStr asc");
				var arrCheck_V = tableCheck_V.Select("UserEnrollNumber = " + tempMaCC, "ID asc, TimeStr asc");
				var arrVangg = tableVang.Select("UserEnrollNumber = " + tempMaCC, "TimeDate asc");
				var arrXacNhanPC50 = tableXacNhanPC50.Select("UserEnrollNumber = " + tempMaCC, "Ngay asc");
				var arrXacNhanPCDB = tableXacNhanPCDB.Select("UserEnrollNumber = " + tempMaCC, "Ngay asc");

				ArrayRowsToDSVang(arrVangg, nv.DSVang);
				if (HaveHoliday) { DocNgayLeVaoDSVang(tableNgayLe, nv.DSVang); }
				ArrayRowsToDSXNPC50(arrXacNhanPC50, nv.DSXNPhuCap50);
				ArrayRowsToDSXNPCDB(arrXacNhanPCDB, nv.DSXNPhuCapDB);
				ArrayRowsToDSCheck_A(arrCheck_A, nv.DS_Check_A);
				LoaiBoCheckCungLoaiTrong30phut(nv.DS_Check_A, nv.DS_Check_KoHopLe);
				GhepCIO_A(nv.DS_Check_A, nv.DS_CIO_A);
				LoaiBoCIOKoHopLe(nv.DS_CIO_A, nv.DS_Check_A, nv.DS_Check_KoHopLe);
				if (nv.DS_Check_KoHopLe.Count > 0) {
					DS_Check_KoHopLe.AddRange(nv.DS_Check_KoHopLe);
					nv.DS_Check_KoHopLe.Clear();//sau khi đưa vào danh sách check ko hợp lệ để thực hiện update status Loại thì cũng đồng thời clear danh sách check ko hợp lệ
				}
				XetCa_CIO_A(nv.DS_CIO_A, nv.LichTrinhLV.DSCa, ds_raa3_vao1, nv.DS_Check_A);// xét thuộc ca, update thuộc ngày công, tách ca 3&1 nếu có, rồi tính công//nv.MacDinhTinhPC50, //[140615_4]

				ArrayRowsToDS_CIO_V(arrCheck_V, nv.LichTrinhLV.DSCaMoRong, nv.DS_CIO_V);
				TronDS_CIO_A_V(nv.DS_CIO_A, nv.DS_CIO_V, nv.DSVaoRa);
				TinhCongTheoNgay(nv.DSVaoRa, nv.NgayCongBD_Bef2D, nv.NgayCongKT_Aft2D, nv.DSVang, nv.DSNgayCong, nv.MacDinhTinhPC50);
				TinhLaiPhuCapTC(nv.DSXNPhuCap50, nv.DSNgayCong);
				TinhLaiPhuCapDB(nv.DSXNPhuCapDB, nv.DSNgayCong);

			}
			if (DS_Check_KoHopLe.Count > 0) DAL.LoaiGioLienQuan(DS_Check_KoHopLe);
			if (ds_raa3_vao1.Count > 0) DAL.ThemGio_ra3_vao1(ds_raa3_vao1);
		}

		public static void XemCong(List<cUserInfo> dsnv, DateTime ngayBD_Bef2D, DateTime ngayKT_Aft2D) {
			XmlConfigurator.Configure();
			var Arr_MaCC = (from nv in dsnv select nv.MaCC).ToArray(); // tạo mảng danh sách mã chấm công để viết chuỗi query : or.. or
			var tableCheck_A = DAL.LayTableCIO_A(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableCheck_V = DAL.LayTableCIO_V(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableVang = DAL.LayTableVang(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableXN_PCTC = DAL.LayTableXacNhanPC50(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableXN_PCDB = DAL.LayTableXacNhanPCDB(Arr_MaCC, ngayBD_Bef2D, ngayKT_Aft2D, Duyet: true);
			var tableNgayLe = DAL.DocNgayLe(ngayBD_Bef2D, ngayKT_Aft2D);
			var HaveHoliday = (tableNgayLe.Rows.Count > 0);

			var DS_Check_KoHopLe = new List<cChk>();
			var ds_raa3_vao1 = new List<cChk>();
			foreach (var nv in dsnv) {
				var tempMaCC = nv.MaCC;
				nv.NgayCongBD_Bef2D = ngayBD_Bef2D;
				nv.NgayCongKT_Aft2D = ngayKT_Aft2D;
				var arrCheck_A = tableCheck_A.Select("UserEnrollNumber = " + tempMaCC, "TimeStr asc");
				var arrCheck_V = tableCheck_V.Select("UserEnrollNumber = " + tempMaCC, "ID asc, TimeStr asc");
				var arrVangg = tableVang.Select("UserEnrollNumber = " + tempMaCC, "TimeDate asc");
				var arrXacNhanPC50 = tableXN_PCTC.Select("UserEnrollNumber = " + tempMaCC, "Ngay asc");
				var arrXacNhanPCDB = tableXN_PCDB.Select("UserEnrollNumber = " + tempMaCC, "Ngay asc");

				ArrayRowsToDSVang(arrVangg, nv.DSVang);
				if (HaveHoliday) { DocNgayLeVaoDSVang(tableNgayLe, nv.DSVang); }
				ArrayRowsToDSXNPC50(arrXacNhanPC50, nv.DSXNPhuCap50);
				ArrayRowsToDSXNPCDB(arrXacNhanPCDB, nv.DSXNPhuCapDB);
				ArrayRowsToDSCheck_A(arrCheck_A, nv.DS_Check_A);
				LoaiBoCheckCungLoaiTrong30phut(nv.DS_Check_A, nv.DS_Check_KoHopLe);
				GhepCIO_A(nv.DS_Check_A, nv.DS_CIO_A);
				LoaiBoCIOKoHopLe(nv.DS_CIO_A, nv.DS_Check_A, nv.DS_Check_KoHopLe);
				if (nv.DS_Check_KoHopLe.Count > 0) {
					DS_Check_KoHopLe.AddRange(nv.DS_Check_KoHopLe);
					nv.DS_Check_KoHopLe.Clear();//sau khi đưa vào danh sách check ko hợp lệ để thực hiện update status Loại thì cũng đồng thời clear danh sách check ko hợp lệ
				}
				XetCa_CIO_A(nv.DS_CIO_A, nv.LichTrinhLV.DSCa, ds_raa3_vao1, nv.DS_Check_A);// xét thuộc ca, update thuộc ngày công, tách ca 3&1 nếu có, rồi tính công//nv.MacDinhTinhPC50, //[140615_4]

				ArrayRowsToDS_CIO_V(arrCheck_V, nv.LichTrinhLV.DSCaMoRong, nv.DS_CIO_V);
				TronDS_CIO_A_V(nv.DS_CIO_A, nv.DS_CIO_V, nv.DSVaoRa);
				TinhCongTheoNgay(nv.DSVaoRa, nv.NgayCongBD_Bef2D, nv.NgayCongKT_Aft2D, nv.DSVang, nv.DSNgayCong, nv.MacDinhTinhPC50);
				TinhLaiPhuCapTC(nv.DSXNPhuCap50, nv.DSNgayCong);
				TinhLaiPhuCapDB(nv.DSXNPhuCapDB, nv.DSNgayCong);
			}
			if (DS_Check_KoHopLe.Count > 0) DAL.LoaiGioLienQuan(DS_Check_KoHopLe);
			if (ds_raa3_vao1.Count > 0) DAL.ThemGio_ra3_vao1(ds_raa3_vao1);

		}

		#region các bước để tính toán công và phụ cấp các loại

		public static void ArrayRowsToDSVang(DataRow[] arrRow, List<cLoaiVang> dsVang) {
			dsVang.Clear();
			if (arrRow.Length == 0) return;
			dsVang.AddRange(from row in arrRow
							let TimeDate = (DateTime)row["TimeDate"]
							let absentCode = (string)row["AbsentCode"]
							let absentsymbol = (string)row["AbsentSymbol"]
							let absentDesc = (string)row["AbsentDescription"]
							let wkdayy = (Single)row["Workingday"]
							select new cLoaiVang { KyHieu = absentsymbol, Cong = wkdayy, MaLV = absentCode, MoTa = absentDesc, Ngay = TimeDate });
		}

		public static void DocNgayLeVaoDSVang(DataTable tableNgayLe, List<cLoaiVang> dsVangs) {
			if (tableNgayLe.Rows.Count == 0) return;
			dsVangs.AddRange(from DataRow row in tableNgayLe.Select(string.Empty, "HDate ASC")
							 let ngayle = (DateTime)row["HDate"]
							 let mota = row["Holiday"].ToString()
							 select new cLoaiVang { Cong = 1f, KyHieu = "L", MaLV = "L", MoTa = mota, Ngay = ngayle });
		}

		public static void ArrayRowsToDSXNPC50(DataRow[] arrRows, List<cTemp1> dsXacNhanPC) {
			dsXacNhanPC.Clear();
			if (arrRows.Length == 0) return;
			foreach (var row in arrRows) {
				var UserEnrollNumber = (int)row["UserEnrollNumber"];
				var ngay = (DateTime)row["Ngay"];
				var tinhpc50 = (row["TinhPC50"] != DBNull.Value) ? (bool)row["TinhPC50"] : false;
				var xacnhanpc = new cTemp1 { Ngay = ngay, UserEnrollNumber = UserEnrollNumber, TinhPC50 = tinhpc50, };
				dsXacNhanPC.Add(xacnhanpc);
			}
		}

		public static void ArrayRowsToDSXNPCDB(DataRow[] arrRows, List<cTemp> dsXacNhanPC) {
			dsXacNhanPC.Clear();
			if (arrRows.Length == 0) return;
			foreach (var row in arrRows) {
				//var duyet = (row["Duyet"] != DBNull.Value) ? (bool)row["Duyet"] : false;
				var UserEnrollNumber = (int)row["UserEnrollNumber"];
				var ngay = (DateTime)row["Ngay"];
				var LoaiPC = (int)row["LoaiPC"];
				var PCNgay = (row["PCNgay"] != DBNull.Value) ? (int)row["PCNgay"] : 0;
				var PCDem = (row["PCDem"] != DBNull.Value) ? (int)row["PCDem"] : 0;
				var xacnhanpc = new cTemp { Ngay = ngay, UserEnrollNumber = UserEnrollNumber, LoaiPC = LoaiPC, PCNgay = PCNgay, PCDem = PCDem };
				dsXacNhanPC.Add(xacnhanpc);
			}
		}

		public static void ArrayRowsToDSCheck_A(DataRow[] arrRows, List<cChk> ds_Check_A) {
			//cấu trúc datatble là cấu trúc bảng checkinout

			#region reset lại các danh sách

			ds_Check_A.Clear();

			#endregion

			if (arrRows.Length == 0) return;

			foreach (var row in arrRows) {
				var UserEnrollNumber = (int)row["UserEnrollNumber"];
				var MachineNo = (int)row["MachineNo"];
				var Source = (string)row["Source"];
				var TimeStr = (DateTime)row["TimeStr"];
				var checkThem = (row["Them"] != DBNull.Value) ? (bool)row["Them"] : false;
				var IDGioGoc = (row["IDGioGoc"] != DBNull.Value) ? (int)row["IDGioGoc"] : -1;
				var checkXoaa = (row["Xoa"] != DBNull.Value) ? (bool)row["Xoa"] : false;

				cChk check;
				if (MachineNo % 2 == 1) {
					var checkInn = new cChkInn_A { MaCC = UserEnrollNumber, Time = TimeStr, Source = Source, MachineNo = MachineNo, Type = "I", PhucHoi = new cPhucHoi { Them = checkThem, IDGioGoc = IDGioGoc, Xoaa = checkXoaa } };
					check = checkInn;
				}
				else {
					var checkOut = new cChkOut_A { MaCC = UserEnrollNumber, Time = TimeStr, Source = Source, MachineNo = MachineNo, Type = "O", PhucHoi = new cPhucHoi { Them = checkThem, IDGioGoc = IDGioGoc, Xoaa = checkXoaa } };
					check = checkOut;
				}
				ds_Check_A.Add(check);
			}
		}

		public static void LoaiBoCheckCungLoaiTrong30phut(List<cChk> ds_Check_A, List<cChk> ds_Check_Trong30ph) {
			// lọc này phải dảm bảo sort trước
			if (ds_Check_A == null || ds_Check_A.Count == 0 || ds_Check_A.Count == 1) return;
			var i = 0;
			while (i + 1 < ds_Check_A.Count) {
				var before = ds_Check_A[i];
				var afterr = ds_Check_A[i + 1];
				if ((before.GetType() == afterr.GetType()) && ((afterr.Time - before.Time) < XL2._30phut)) {
					// cùng loại trong 30ph
					before.IsEdited += afterr.IsEdited;
					ds_Check_Trong30ph.Add(afterr);
					ds_Check_A.Remove(afterr);
				}
				else i++;
			}
		}

		public static void GhepCIO_A(List<cChk> ds_Check_A, List<cChkInOut_A> ds_CIO_A) {
			ds_CIO_A.Clear();
			var x = 0;
			while (x + 1 < ds_Check_A.Count) {
				var chk_1 = ds_Check_A[x];
				var chk_2 = ds_Check_A[x + 1];
				if (chk_1.GetType() == typeof(cChkOut_A)) {
					// đầu ds là checkOut --> ra ko vào
					var CIO = new cChkInOut_A { IsEdited = chk_1.IsEdited, Vao = null, Raa = chk_1, HaveINOUT = -2, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
					ds_CIO_A.Add(CIO);
					x++;
				}
				else {
					//đầu ds là checkInn-> kiểm tra kế nếu cũng là check In thì checkInn trước là vào ko ra
					if (chk_2.GetType() == typeof(cChkInn_A)) {
						var CIO = new cChkInOut_A { IsEdited = chk_1.IsEdited, Vao = chk_1, Raa = null, HaveINOUT = -1, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
						ds_CIO_A.Add(CIO);
						x++;
					}
					else {
						// kế là checkOut --> kiểm tra nằm trong khoảng >30ph và dưới 21h45 thì ghép, ngược lại thì giờ vào ko ra, ra ko vào
						var duration = chk_2.Time - chk_1.Time;
						if (duration > XL2._21h45) {
							var CIO1 = new cChkInOut_A { IsEdited = chk_1.IsEdited, Vao = chk_1, Raa = null, HaveINOUT = -1, TimeDaiDien = chk_1.Time, TD = new ThoiDiem(), }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
							var CIO2 = new cChkInOut_A { IsEdited = chk_2.IsEdited, Vao = null, Raa = chk_2, HaveINOUT = -2, TimeDaiDien = chk_2.Time, TD = new ThoiDiem(), }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
							ds_CIO_A.Add(CIO1);
							x++;
							ds_CIO_A.Add(CIO2);
							x++;
						}
						else if (duration < XL2._30phut) {
							var CIO = new cChkInOut_A { Vao = chk_1, Raa = chk_2, HaveINOUT = 30, TimeDaiDien = chk_1.Time, TG = new ThoiGian(), TD = new ThoiDiem(), };
							CIO.IsEdited += chk_1.IsEdited;
							CIO.IsEdited += chk_2.IsEdited;
							ds_CIO_A.Add(CIO);
							x++;
							x++;
						}
						else {
							var CIO = new cChkInOut_A { Vao = chk_1, Raa = chk_2, HaveINOUT = 0, TimeDaiDien = chk_1.Time, TG = new ThoiGian(), TD = new ThoiDiem(), };
							CIO.IsEdited += chk_1.IsEdited;
							CIO.IsEdited += chk_2.IsEdited;
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
				if (chk_1.GetType() == typeof(cChkInn_A)) {
					var CIO1 = new cChkInOut_A { IsEdited = chk_1.IsEdited, Vao = chk_1, Raa = null, HaveINOUT = -1, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
					ds_CIO_A.Add(CIO1);
				}
				else {
					var CIO2 = new cChkInOut_A { IsEdited = chk_1.IsEdited, Vao = null, Raa = chk_1, HaveINOUT = -2, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
					ds_CIO_A.Add(CIO2);
				}
			}
		}

		public static void LoaiBoCIOKoHopLe(List<cChkInOut_A> ds_CIO_A, List<cChk> ds_Check_A, List<cChk> dsCheck_KoHopLe) {
			if (ds_CIO_A.Count == 0 || ds_CIO_A.Count == 1) return;
			var x = 0;
			var temp = String.Empty; //để in chuỗi debug
			while (x + 1 < ds_CIO_A.Count) {
				var CIOx = ds_CIO_A[x];
				var CIOy = ds_CIO_A[x + 1];
				if (CIOx.HaveINOUT == -1) {
					// 1. sau I_KR : I_KR hay O_KV(I-O > 30ph) hay (IOca) thì bỏ qua, I đó vẫn là I_KR
					// 2. nhưng sau sau I_KR là IO30 thì có đó là check lộn: muốn ra nhưng lại check vào trước rồi check ra sau
					if (CIOy.HaveINOUT == -1 || CIOy.HaveINOUT == -2 || (CIOy.HaveINOUT == 0)) {
						//1. Ikr > 30ph, 2. Okv (>21h), 3. IOca
						x++;
					}
					else {
						//4.IO30, tìm cách ghép I1 với O  ( trường hợp I1------I2-O ) --> nếu I1-O trong 1 ngày thì xóa I2 đi, chuyển I1 sang I2 rồi xóa CIO1 đi
						var duration = CIOy.Raa.Time - CIOx.Vao.Time;
						if (duration < XL2._21h45) {
							#region debug

							temp = "I   : " + CIOx.Vao.Time.ToString("H:mm:ss d/M");
							temp += "\t\tIO30: " + CIOy.Vao.Time.ToString("H:mm:ss d/M") + "-" + CIOy.Raa.Time.ToString("H:mm:ss d/M");
							temp += "\nLoai : I: " + CIOy.Vao.Time.ToString("H:mm:ss d/M");
							//Debug.WriteLine(temp);
							//Debug.WriteLine("CIO trước khi bỏ: " + CIO1.Vao.TimeStr.ToString("H:mm:ss d/M"));

							#endregion

							// đưa vào ds ko hợp lệ và loại bỏ khỏi ds_check auto và checkin auto
							dsCheck_KoHopLe.Add(CIOy.Vao);
							ds_Check_A.Remove(CIOy.Vao);

							CIOy.IsEdited += CIOx.Vao.IsEdited; // is edited là tổng hợp isedited của cả 2 CIO
							CIOy.Vao = CIOx.Vao; //CIO2.Raa giữ nguyên
							CIOy.HaveINOUT = 0;
							CIOy.TimeDaiDien = CIOx.Vao.Time;
							CIOy.TG = new ThoiGian();
							ds_CIO_A.RemoveAt(x); //  sau khi loại bỏ  thì CIO1 là ghép của I trước với O sau và < 21 tiếng, tăng x để bỏ qua CIO hợp lệ này
							x++;
						}
						else {
							// trên 21 tiếng thì bỏ qua , tăng x để xử lý IO-30ph
							x++;
						}
					}
				}
				else if (CIOx.HaveINOUT == -2) {
					// sau O_KV : là O_KV thì bỏ qua
					// nhưng sau O_KV là I_KV hoặc O2_KV hoặc IOca hoặc IO30 mà cách < 30 ph thì bỏ O_KV này
					if (CIOy.HaveINOUT == 30 && (CIOy.Vao.Time - CIOx.Raa.Time < XL2._30phut)) {
						dsCheck_KoHopLe.Add(CIOy.Vao);
						dsCheck_KoHopLe.Add(CIOy.Raa);
						//trước khi loại bỏ thì chuyển Isedited của IO30 sang cho O
						CIOx.Raa.IsEdited += CIOy.IsEdited;
						CIOx.IsEdited += CIOx.Raa.IsEdited;

						ds_Check_A.Remove(CIOy.Vao);
						ds_Check_A.Remove(CIOy.Raa);
						ds_CIO_A.Remove(CIOy); // ko tăng x vì lúc này vẫn là O kr xét tiếp với các CIO tiếp theo
					}
					else {
						x++;
					}
				}
				else if (CIOx.HaveINOUT == 30) {
					//IO 30ph --> muốn ra bỏ I đi

					#region debug

					temp = "IO30: " + CIOx.Vao.Time.ToString("H:mm:ss d/M") + "  -  " + CIOx.Raa.Time.ToString("H:mm:ss d/M");
					temp += "\nLoai: I   : " + CIOx.Vao.Time.ToString("H:mm:ss d/M");
					//Debug.WriteLine(temp + "\n");

					#endregion

					// đưa vào ds ko hợp lệ và loại bỏ khỏi ds_check auto và check in auto
					dsCheck_KoHopLe.Add(CIOx.Vao);
					ds_Check_A.Remove(CIOx.Vao);

					// khi bỏ đi I thì chuyển edited sang cho Out 
					CIOx.Raa.IsEdited += CIOx.Vao.IsEdited;
					CIOx.IsEdited += CIOx.Raa.IsEdited;
					CIOx.Vao = null; // CIO1.raa giữ nguyên
					CIOx.HaveINOUT = -2;
					CIOx.TimeDaiDien = CIOx.Raa.Time;
					CIOx.TG = null;
					// ko tăng x vì IO_30ph này sau khi bỏ I thì trở thành O ko vào, xét luôn nó
				}
				else {
					//(CIOx.HaveINOUT == 0) IOca
					x++;
				}
			}
		}

		public static void XetCa_CIO_A(List<cChkInOut_A> ds_CIO_A, List<cCaAbs> dsca, List<cChk> ds_raa3_vao1, List<cChk> ds_check_A) {
			//bool macdinh_tinhPC50, //[140615_4]
			var i = 0;
			while (i < ds_CIO_A.Count) {
				var CIO = ds_CIO_A[i];

				#region nếu giờ quen check thì chỉ kiểm tra khoảng hiểu ca

				if (CIO.HaveINOUT < 0) {
					CIO.ThuocNgayCong = CIO.TimeDaiDien.Date;
					CIO.DSCa = KiemtraKhoangHieuCa(CIO, dsca);

					i++;
					continue;
				}

				#endregion

				var ngay = ThuocNgayCong(CIO.TimeDaiDien);
				CIO.ThuocNgayCong = ngay;

				var ca = KiemtraThuocCa(CIO, dsca);

				#region nếu thuộc khoảng hiểu ca thì tính công

				if (ca != null) {
					if (ca.TachCa) {
						//tbd thêm điều kiện ca.QuaDem
						var ca3 = ca.catruoc;
						var ca1 = ca.casauuu;

						#region check inn, check out vao 3 ra 3, vao 1 ra 1

						var vaoca3 = CIO.Vao;
						var raaca3 = new cChkOut_A {
							Type = "O",
							MachineNo = 22,
							Source = "PC",
							Time = ngay.Add(ca3.OffTS),
							MaCC = CIO.Vao.MaCC,
							PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false },
						}; // đáng lẽ IDgiờgốc là -1 vì giờ này mới thêm chưa bị sửa nhưng do đây là giờ đệm ko có trong csdl nên để max, ko cho update
						var vaoca1 = new cChkInn_A {
							Type = "I",
							MachineNo = 21,
							Source = "PC",
							Time = CIO.Raa.Time.Date.Add(ca1.OnnTS).Add(XL2._01giay),
							MaCC = CIO.Raa.MaCC,
							PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false },
						}; // đáng lẽ IDgiờgốc là -1 vì giờ này mới thêm chưa bị sửa nhưng do đây là giờ đệm ko có trong csdl nên để max, ko cho update
						var raaca1 = CIO.Raa;

						#endregion

						ds_raa3_vao1.Add(raaca3);
						ds_raa3_vao1.Add(vaoca1);
						ds_check_A.Add(raaca3);
						ds_check_A.Add(vaoca1);
						ds_check_A.Sort(new cChkComparer());
						// do tách ra thành 2 CIO mới nên phải gán lại IsEdited cho từng cái
						ds_CIO_A[i] = new cChkInOut_A { IsEdited = vaoca3.IsEdited, TG = new ThoiGian(), TD = new ThoiDiem(), ThuocCa = ca3, Vao = vaoca3, Raa = raaca3, ThuocNgayCong = ngay, TimeDaiDien = vaoca3.Time, }; //TinhPC150 = macdinh_tinhPC50, //[140615_2]
						TinhTGLam_TGQuaDem(ds_CIO_A[i], ds_CIO_A[i].ThuocCa);

						var newCIO = new cChkInOut_A { IsEdited = raaca1.IsEdited, TG = new ThoiGian(), TD = new ThoiDiem(), ThuocCa = ca1, Vao = vaoca1, Raa = raaca1, ThuocNgayCong = ngay.AddDays(1d), TimeDaiDien = vaoca1.Time, }; //TinhPC150 = macdinh_tinhPC50, //[140615_2]
						TinhTGLam_TGQuaDem(newCIO, newCIO.ThuocCa);

						// vì hàm insert ko cho phép chèn ở vị trí > số lượng phần tử
						// => nên nếu i là phần tử cuối thì add vào cuối danh sách, ngược lại thì insert vào vị trí i+1
						if (i == (ds_CIO_A.Count - 1)) ds_CIO_A.Add(newCIO);
						else ds_CIO_A.Insert(i + 1, newCIO);
						i = i + 2; // +2 vì i là ca3, i+1 là ca 1
					}
					else {
						//CIO.TG.GioThuc đã update info trong hàm ghép_CIO_A, IsEdited đã có trong lúc ghép
						CIO.ThuocCa = ca;
						//CIO.TinhPC150 = macdinh_tinhPC50;//[140615_2]
						TinhTGLam_TGQuaDem(CIO, CIO.ThuocCa); // cập nhật CIO.QuaĐêm
						i++;
					}
				}
				#endregion
				#region nếu ko thuộc khoảng hiểu thì đó tạo ca tự do, tính công theo ca tự do này

				else {
					var catudo = new cCaTuDo { ID = Int32.MinValue, Code = "Ca8h" };
					TaoCaTuDo(catudo, CIO.Vao.Time, XL2._08gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, "8");
					CIO.ThuocCa = catudo;
					//CIO.TinhPC150 = macdinh_tinhPC50;//[140615_2]
					TinhTGLam_TGQuaDem(CIO, CIO.ThuocCa);

					i++;
				}

				#endregion
			}

		}

		public static cCaChuan KiemtraThuocCa(cChkInOut_A CIO, List<cCaAbs> dsCa) {
			var t_vao = CIO.Vao.Time;
			var ngay = CIO.ThuocNgayCong;
			var t_raa = CIO.Raa.Time;
			return dsCa.FirstOrDefault(ca => t_vao >= ngay.Add(ca.OnnInnTS) && t_vao <= ngay.Add(ca.CutInnTS)
											 && t_raa >= ngay.Add(ca.OnnOutTS) && t_raa <= ngay.Add(ca.CutOutTS)) as cCaChuan;
		}

		public static List<cCaAbs> KiemtraKhoangHieuCa(cChkInOut_A CIO, List<cCaAbs> dsCa) {
			var time = CIO.TimeDaiDien;
			var ngay = CIO.ThuocNgayCong;
			var kq = (CIO.HaveINOUT == -1)
						 ? dsCa.FindAll(ca => time >= ngay.Add(ca.OnnInnTS) && time <= ngay.Add(ca.CutInnTS))
						 : dsCa.FindAll(ca => time >= ngay.Add(ca.OnnOutTS) && time <= ngay.Add(ca.CutOutTS));
			return kq;
		}

		public static void ArrayRowsToDS_CIO_V(DataRow[] arrRows, List<cCaAbs> dsCa, List<cChkInOut_V> DS_CIO_V) {
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
				var checkThemInn = (rowInn["Them"] != DBNull.Value) ? (bool)rowInn["Them"] : false;
				var checkXoaaInn = (rowInn["Xoa"] != DBNull.Value) ? (bool)rowInn["Xoa"] : false;
				var IDGioGoc_Inn = (rowInn["IDGioGoc"] != DBNull.Value) ? (int)rowInn["IDGioGoc"] : -1;
				var checkThemOut = (rowOut["Them"] != DBNull.Value) ? (bool)rowOut["Them"] : false;
				var checkXoaaOut = (rowOut["Xoa"] != DBNull.Value) ? (bool)rowOut["Xoa"] : false;
				var IDGioGoc_Out = (rowOut["IDGioGoc"] != DBNull.Value) ? (int)rowOut["IDGioGoc"] : -1;
				var TreSomTinhCV = (rowInn["TreSomTinhCV"] != DBNull.Value) ? (bool)rowInn["TreSomTinhCV"] : false;

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

				#region lấy thông tin

				var shiftID = (int)rowInn["ShiftID"];
				var shiftCode = rowInn["ShiftCode"].ToString();
				var dayCount = (int)rowInn["DayCount"];
				var workingday = (Single)rowInn["Workingday"];
				TimeSpan onnDutyTs, offDutyTs;
				TimeSpan.TryParse(rowInn["Onduty"].ToString(), out onnDutyTs);
				TimeSpan.TryParse(rowInn["Offduty"].ToString(), out offDutyTs);

				var afterOTTs = new TimeSpan(0, (int)rowInn["AfterOT"], 0);
				var lateGraceTSS = new TimeSpan(0, (int)rowInn["LateGrace"], 0);
				var earlyGraceTS = new TimeSpan(0, (int)rowInn["EarlyGrace"], 0);

				lateGraceTSS = onnDutyTs.Add(lateGraceTSS);
				// add thêm 1 ngày daycount nếu có
				offDutyTs = offDutyTs.Add(new TimeSpan(dayCount, 0, 0, 0));
				earlyGraceTS = offDutyTs.Subtract(earlyGraceTS);
				afterOTTs = offDutyTs.Add(afterOTTs);
				var pOTMin = (int)rowInn["OTMin"];


				var wkt = Int32.Parse(rowInn["WorkingTime"].ToString());
				var pWorkingTime = new TimeSpan(0, wkt, 0);

				#endregion

				var chkInnV = new cChkInn_V { ID = IDinn, MachineNo = MachineNoInn, Source = SourceInn, Time = timeInn, Type = "I", PhucHoi = new cPhucHoi { Them = checkThemInn, IDGioGoc = IDGioGoc_Inn, Xoaa = checkXoaaInn }, MaCC = UserEnrollNumber };
				var chkOutV = new cChkOut_V { ID = IDout, MachineNo = MachineNoOut, Source = SourceOut, Time = timeOut, Type = "O", PhucHoi = new cPhucHoi { Them = checkThemOut, IDGioGoc = IDGioGoc_Out, Xoaa = checkXoaaOut }, MaCC = UserEnrollNumber };
				var chkInOutV = new cChkInOut_V {
					ID = IDinn,
					Vao = chkInnV,
					Raa = chkOutV,
					//HaveINOUT = 0, mặc định = 0
					TimeDaiDien = chkInnV.Time,
					TreSomTinhCV = TreSomTinhCV,
					//TinhPC150 = TinhPC150,//[140615_2]
					ThuocNgayCong = ThuocNgayCong(chkInnV.Time),
					TG = new ThoiGian { OTCa = new TimeSpan(0, pOTMin, 0) },
					TD = new ThoiDiem()
				};

				cCaAbs thuocCa;
				if (shiftID > 0) {
					thuocCa = (from ca in dsCa where ca.ID == shiftID select ca).FirstOrDefault();
					if (thuocCa == null) {
						thuocCa = (from ca in DSCa where ca.ID == shiftID select ca).FirstOrDefault();
						if (thuocCa == null) {
							thuocCa = new cCaTuDo {
								ID = shiftID, Code = shiftCode,
								OnnTS = onnDutyTs, OffTS = offDutyTs,
								chophepTreTS = lateGraceTSS, chophepSomTS = earlyGraceTS, batdaulamthemTS = afterOTTs,
								WorkingTimeTS = pWorkingTime, Workingday = workingday,
								DayCount = dayCount, QuaDem = (dayCount == 1),
								LunchMin = XL2._0gio,
							}; // tbd chưa thêm lunch minute dưới csdl để thêm nên tạm thời để 0g
						}
					}
				}
				else if (shiftID > Int32.MinValue + 100 && shiftID < 0) // ca tách và ca kết hợp  [Chú ý] + 100 vì chừa khoảng này cho các loại khác
					thuocCa = (from ca in dsCa where ca.ID == shiftID select ca).FirstOrDefault();
				else if (shiftID < Int32.MinValue + 100) // ca tự do (8 tiếng, ca dài 12 tiếng)
					thuocCa = new cCaTuDo {
						ID = shiftID, Code = shiftCode,
						OnnTS = onnDutyTs, OffTS = offDutyTs,
						chophepTreTS = lateGraceTSS, chophepSomTS = earlyGraceTS, batdaulamthemTS = afterOTTs,
						WorkingTimeTS = pWorkingTime, Workingday = workingday,
						DayCount = dayCount, QuaDem = (dayCount == 1),
						LunchMin = XL2._0gio,
					}; // tbd chưa thêm lunch minute dưới csdl để thêm nên tạm thời để 0g
				else
					thuocCa = new cCaTuDo {
						ID = shiftID, Code = "XN8h",
						OnnTS = onnDutyTs, OffTS = offDutyTs,
						chophepTreTS = lateGraceTSS, chophepSomTS = earlyGraceTS, batdaulamthemTS = afterOTTs,
						WorkingTimeTS = pWorkingTime, Workingday = workingday,
						DayCount = dayCount, QuaDem = (dayCount == 1),
						LunchMin = XL2._0gio,
					}; //[TBD]

				if (thuocCa == null) {
					log.Fatal("ERROR function Check_GioDaXN");
				}

				if (thuocCa is cCaChuan && thuocCa.TachCa) {
					var ca3_va1_hoac1A = (cCaChuan)thuocCa;
					var ca3 = ca3_va1_hoac1A.catruoc;
					var ca1_1A = ca3_va1_hoac1A.casauuu;
					var ngay = chkInOutV.ThuocNgayCong;
					var vaocatruoc = chkInnV;
					var raacatruoc = new cChkOut_V {
						Time = ngay.Add(ca3.OffTS), ID = IDinn, Type = "O", MachineNo = 22, Source = "PC", MaCC = UserEnrollNumber,
						PhucHoi = new cPhucHoi { IDGioGoc = Int32.MaxValue, Them = true, Xoaa = false },
					}; // tbd TimeStr?? //tbd trạng thái????
					var vaocasauuu = new cChkInn_V {
						Time = ngay.AddDays(1d).Add(ca1_1A.OnnTS).Add(XL2._01giay), ID = IDout, Type = "I", MachineNo = 21, Source = "PC", MaCC = UserEnrollNumber,
						PhucHoi = new cPhucHoi { IDGioGoc = Int32.MaxValue, Them = true, Xoaa = false },
					}; //tbd trạng thái????
					var raacasauuu = chkOutV;
					var CIO_V_truoc = new cChkInOut_V { ID = IDinn, Vao = vaocatruoc, Raa = raacatruoc, TimeDaiDien = vaocatruoc.Time, ThuocNgayCong = ngay, TreSomTinhCV = TreSomTinhCV}; //tbd xem [140514_1] //TinhPC150 = TinhPC150,//[140615_2]
					var CIO_V_sauuu = new cChkInOut_V { ID = IDout, Vao = vaocasauuu, Raa = raacasauuu, TimeDaiDien = vaocasauuu.Time, ThuocNgayCong = ngay.AddDays(1d), TreSomTinhCV = TreSomTinhCV}; //làm thêm là của ca sau, ca trước ko có làm thêm //TinhPC150 = TinhPC150, //[140615_2]
					CIO_V_truoc.TG = new ThoiGian { OTCa = TimeSpan.Zero, OLai = TimeSpan.Zero };
					CIO_V_truoc.TD = new ThoiDiem();
					CIO_V_truoc.QuaDem = true;
					CIO_V_truoc.ThuocCa = ca3;

					CIO_V_sauuu.TG = new ThoiGian { OTCa = new TimeSpan(0, pOTMin, 0), OLai = TimeSpan.Zero };
					CIO_V_sauuu.TD = new ThoiDiem();
					CIO_V_sauuu.QuaDem = false;
					CIO_V_sauuu.ThuocCa = ca1_1A;

					TinhTGLam_TGQuaDem(CIO_V_truoc, CIO_V_truoc.ThuocCa);
					TinhTGLam_TGQuaDem(CIO_V_sauuu, CIO_V_sauuu.ThuocCa);

					DS_CIO_V.Add(CIO_V_truoc);
					DS_CIO_V.Add(CIO_V_sauuu);
				}
				else {
					// các trường hợp còn lại, ca chuẩn ko tách và ca tự do
					chkInOutV.ThuocCa = thuocCa;

					Debug.Assert(thuocCa != null, "thuocCa != null");
					chkInOutV.QuaDem = thuocCa.QuaDem;
					TinhTGLam_TGQuaDem(chkInOutV, chkInOutV.ThuocCa);
					DS_CIO_V.Add(chkInOutV);
				}

				// sau khi thực hiện xong thì tăng 
				x = x + 2;
				y = y + 2;
			}
		}

		public static void TronDS_CIO_A_V(List<cChkInOut_A> dsCIO_A, List<cChkInOut_V> dsCIO_V, List<cChkInOut> kq) {
			kq.Clear();
			kq.AddRange(dsCIO_A);
			kq.AddRange(dsCIO_V);
			kq.Sort(new cChkInOutComparer());
		}

		public static void TinhTGLam_TGQuaDem(cChkInOut CIO, cCaAbs ca) {
			try {
				if (CIO.HaveINOUT < 0) return;

				var vaoCa = CIO.ThuocNgayCong.Add(ca.OnnTS);
				var raaCa = CIO.ThuocNgayCong.Add(ca.OffTS);//off duty này đã bao gồm daycount được công bên trong
				var chophepvaotre = CIO.ThuocNgayCong.Add(ca.chophepTreTS);
				var chophepraasom = CIO.ThuocNgayCong.Add(ca.chophepSomTS);
				var batdaulamthem = CIO.ThuocNgayCong.Add(ca.batdaulamthemTS);
				var tmpBDLamDem = CIO.ThuocNgayCong.Add(XL2._21h45);
				var tmpKTLamDem = CIO.ThuocNgayCong.AddDays(1d).Add(XL2._05h45);
				var temp1 = new DateTime();
				var temp2 = new DateTime();
				var quadem = false;

				CIO.TG.GioThuc = CIO.Raa.Time - CIO.Vao.Time;
				Vao(CIO.Vao.Time, vaoCa, chophepvaotre, out CIO.TD.VaoLam, out CIO.TG.VaoTre);
				Raa(CIO.Raa.Time, raaCa, chophepraasom, out CIO.TD.RaLam_ChuaOT, out CIO.TG.RaaSom);
				if (CIO.GetType() == typeof(cChkInOut_A)) {
					OLai(CIO.Raa.Time, raaCa, batdaulamthem, out CIO.TG.OLai);
					CIO.TD.RaaLam = CIO.TD.RaLam_ChuaOT;
				}
				else if (CIO.GetType() == typeof(cChkInOut_V)) {
					LamThem(CIO.TD.RaLam_ChuaOT, CIO.TG.OTCa, out  CIO.TD.RaLam_DaCoOT);
					CIO.TD.RaaLam = CIO.TD.RaLam_DaCoOT;
				}
				TGLamTinhCong(CIO.TD.VaoLam, CIO.TD.RaaLam, ca.LunchMin, out CIO.TG.GioLamTrongNgay);
				LamDem(CIO.TD.VaoLam, CIO.TD.RaaLam, tmpBDLamDem, tmpKTLamDem, out temp1, out  temp2, out CIO.TG.LamDemTrongNgay, out quadem);
				CIO.TG.LamThemTrongNgay = (CIO.TG.GioLamTrongNgay - XL2._08gio) >= XL2._01phut ? (CIO.TG.GioLamTrongNgay - XL2._08gio) : TimeSpan.Zero;
				CIO.QuaDem = quadem;

			} catch (Exception exception) {
				XmlConfigurator.Configure();
				log.Error("TinhCongTheoCa \nStackTrace:" + exception.StackTrace + "--end StartTrace", exception);
				throw;
			}

		}

		public static void TinhCongTheoNgay(List<cChkInOut> dsVaoRa, DateTime ngayBD_Bef2D, DateTime ngayKT_Aft2D, List<cLoaiVang> dsVang, List<cNgayCong> dsNgayCong, bool macdinhtinhpc50) {
			dsNgayCong.Clear();
			var indexNgay = ngayBD_Bef2D.Date; // ngayBD_Bef2D là trước 2 ngày vd: xem ngày 3 thì lấy giờ từ ngày 1, nhưng khi tạo ngày công thì lấy từ ngày 2
			dsNgayCong.Add(new cNgayCong { Ngay = indexNgay, TinhPC50 = macdinhtinhpc50, HasCheck = false, DSVaoRa = new List<cChkInOut>(), prev = null, next = null });
			indexNgay = indexNgay.AddDays(1d);
			for (; indexNgay <= ngayKT_Aft2D.Date; indexNgay = indexNgay.AddDays(1d)) {
				var ngayCong = new cNgayCong { Ngay = indexNgay, TinhPC50 = macdinhtinhpc50, HasCheck = false, DSVaoRa = new List<cChkInOut>(), prev = dsNgayCong[dsNgayCong.Count - 1], next = null };
				dsNgayCong[dsNgayCong.Count - 1].next = ngayCong;
				dsNgayCong.Add(ngayCong);
			}
			var skipElement1 = 0;
			var skipElement2 = 0;
			dsVang.Sort(new cLoaiVangComparer());
			foreach (var ngayCong in dsNgayCong) {
				// lấy trước các danh sách vắng
				ngayCong.DSVang = (from vang in dsVang.Skip(skipElement1)
								   where ngayCong.Ngay == vang.Ngay
								   select vang).ToList();
				skipElement1 += ngayCong.DSVang.Count; //tbd test skip element
				// lấy các giờ vào ra, nếu có thì tính công, ko thì tiếp tục
				ngayCong.DSVaoRa = (from vaoraa in dsVaoRa.Skip(skipElement2)
									where vaoraa.ThuocNgayCong == ngayCong.Ngay
									select vaoraa).ToList();
				skipElement2 += ngayCong.DSVaoRa.Count;

				#region có vào ra, tính công

				if (ngayCong.DSVaoRa.Count > 0) {
					ngayCong.HasCheck = true;
					foreach (var CIO in ngayCong.DSVaoRa) {
						if (CIO.HaveINOUT < 0) {
							ngayCong.IsEdited += CIO.IsEdited;
							ngayCong.DSThieu_VaoHoacRa.Add(CIO);
							continue;
						}
						//if (CIO.TinhPC150) ngayCong.TG.LamTinhPC50 += CIO.TG.LamTinhCong;//[140615_2]
						if (CIO.QuaDem) ngayCong.QuaDem = true; // set qua đêm nếu có
						CIO.Cong = (CIO.TG.GioLamTrongNgay.TotalHours / CIO.ThuocCa.WorkingTimeTS.TotalHours) * CIO.ThuocCa.Workingday;
						ngayCong.IsEdited += CIO.IsEdited;
						ngayCong.TG.GioThuc += CIO.TG.GioThuc;
						ngayCong.TG.GioLamTrongNgay += CIO.TG.GioLamTrongNgay;
						ngayCong.TG.LamDemTrongNgay += CIO.TG.LamDemTrongNgay;
						ngayCong.TG.VaoTre += CIO.TG.VaoTre;
						ngayCong.TG.RaaSom += CIO.TG.RaaSom;
						ngayCong.TongCong += Math.Round(CIO.Cong, 2);
                        if (CIO.Cong < CIO.ThuocCa.Workingday && CIO.TreSomTinhCV == false)
                        {
                            ngayCong.TruCongCV += (CIO.ThuocCa.Workingday - CIO.Cong);
                        }
					}
					ngayCong.TG.LamThemTrongNgay = (ngayCong.TG.GioLamTrongNgay - XL2._08gio > XL2._01phut) ? ngayCong.TG.GioLamTrongNgay - XL2._08gio : TimeSpan.Zero;

					TinhPCDem_PCTC(ngayCong.TG.GioLamTrongNgay, ngayCong.TG.LamDemTrongNgay, ngayCong.TinhPC50, ngayCong.QuaDem, ngayCong.TG.LamThemTrongNgay,
								   out ngayCong.TG.Tinh130, out ngayCong.TG.Tinh150, out ngayCong.TG.TinhTCC3,
								   out ngayCong.PhuCap30, out ngayCong.PhuCap50, out ngayCong.PhuCapTCC3, out ngayCong.TongPhuCap);
				}

				#endregion
			}

		}

		public static void TinhLaiPhuCapTC(List<cTemp1> dsXacNhanPC, List<cNgayCong> dsNgayCong) {
			foreach (var item in dsXacNhanPC) {
				var ngayCong = dsNgayCong.Find(o => o.Ngay == item.Ngay);
				ngayCong.TinhPC50 = (item.TinhPC50);

				TinhPCDem_PCTC(ngayCong.TG.GioLamTrongNgay, ngayCong.TG.LamDemTrongNgay, ngayCong.TinhPC50, ngayCong.QuaDem, ngayCong.TG.LamThemTrongNgay,
							   out ngayCong.TG.Tinh130, out ngayCong.TG.Tinh150, out ngayCong.TG.TinhTCC3,
							   out ngayCong.PhuCap30, out ngayCong.PhuCap50, out ngayCong.PhuCapTCC3, out ngayCong.TongPhuCap);
			}

		}

		public static void TinhLaiPhuCapDB(List<cTemp> dsXacNhanPC, List<cNgayCong> dsNgayCong) {
			foreach (var item in dsXacNhanPC) {
				var ngayCong = dsNgayCong.Find(o => o.Ngay == item.Ngay);

				ngayCong.TinhPCDB = true;
				ngayCong.LoaiPCDB = item.LoaiPC;
				ngayCong.TG.Tinh130 = TimeSpan.Zero;
				ngayCong.TG.Tinh150 = TimeSpan.Zero;
				ngayCong.TG.TinhTCC3 = TimeSpan.Zero;
				ngayCong.PhuCap30 = 0d;
				ngayCong.PhuCap50 = 0d;
				ngayCong.PhuCapTCC3 = 0d;
				  TinhPCDB(ngayCong.TG.GioLamTrongNgay, ngayCong.TG.LamDemTrongNgay, ngayCong.QuaDem, ngayCong.TinhPCDB, ngayCong.LoaiPCDB, item.PCNgay, item.PCDem, //tbd
						 out ngayCong.TG.Tinh200, out ngayCong.TG.Tinh260, out ngayCong.TG.Tinh300, out ngayCong.TG.Tinh390, out ngayCong.TG.TinhPCCus,
						 out ngayCong.PhuCap100, out ngayCong.PhuCap160, out ngayCong.PhuCap200, out ngayCong.PhuCap290, out ngayCong.PhuCapCus, ref ngayCong.TongPhuCap);

			}
		}

		#endregion

		#region thêm, xoá, sửa giờ, thêm giờ cho NV Qlý

		public static void ThemGioChoNVQL(cChk check, cUserInfo nv, int pUserID, string pLydo, string pGhichu) {
			//if (check.Time.Date <= XL2.ThangKetCong && XL2.ThangKetCong != DateTime.MinValue) return ; //tbd temp patch // đã ko cho thực hiện từ bên trên

			DAL.ThemGioChoNV(nv.MaCC, check.Time, check.Type, check.MachineNo, pUserID, pLydo, pGhichu);
		}

		public static void ThemGioChoNV(cChk check, cUserInfo nv, int pUserID, string pLydo, string pGhichu) {
			if (check.Time.Date <= XL2.ThangKetCong && XL2.ThangKetCong != DateTime.MinValue) return; //tbd temp patch

			if (DAL.ThemGioChoNV(nv.MaCC, check.Time, check.Type, check.MachineNo, pUserID, pLydo, pGhichu)) {
				nv.DS_Check_A.Add(check);
			}
		}

		public static bool XoaGioChoNV(cChk check, cUserInfo nhanvien, int currUserID, string lydo, string ghichu) {
			if (check.Time.Date <= XL2.ThangKetCong && XL2.ThangKetCong != DateTime.MinValue) return false; //tbd temp patch

			if (DAL.XoaGioChoNV(nhanvien.MaCC, check.Time, check.Source, check.MachineNo, currUserID, lydo, ghichu)) {
				var indexCheck = nhanvien.DS_Check_A.FindIndex(0, nhanvien.DS_Check_A.Count, o => o.Time == check.Time && o.MachineNo == check.MachineNo && o.Source == check.Source);

				if (indexCheck < 0) return false;
				var prevCheck = (indexCheck == 0) ? null : nhanvien.DS_Check_A[indexCheck - 1];
				var nextCheck = (indexCheck == nhanvien.DS_Check_A.Count - 1) ? null : nhanvien.DS_Check_A[indexCheck + 1];
				if (prevCheck != null && (check.Time - prevCheck.Time).Duration() < XL2._1ngay) prevCheck.IsEdited += 1;
				if (nextCheck != null && (nextCheck.Time - check.Time).Duration() < XL2._1ngay) nextCheck.IsEdited += 1;

				nhanvien.DS_Check_A.Remove(check);

			}
			return true;

		}

		public static void SuaGioChoNV(cChk checkold, cChk checknew, cUserInfo nhanvien, int currUserID, string lydo, string ghichu) {
			if (checkold.Time.Date <= XL2.ThangKetCong && XL2.ThangKetCong != DateTime.MinValue) return; //tbd temp patch

			if (DAL.SuaGioChoNV(nhanvien.MaCC, checkold.Time, checknew.Time, checkold.Source, checknew.Source, checkold.MachineNo, checknew.MachineNo, checkold.PhucHoi.IDGioGoc, currUserID, lydo, ghichu)) {
				nhanvien.DS_Check_A.Remove(checkold);
				nhanvien.DS_Check_A.Add(checknew);
			}
		}

		#endregion

		#region phần xác nhận ca: tách ca, ko tách ca, xác nhận lại

		public static void XacNhanKoTachCa(cUserInfo nhanvien, cChkInOut CIO, cCaAbs caMoi, int sophutlamthem, bool choPhepTinhPC50, bool TreSomTinhCV) {
			//[140615_2]
			if (CIO.ThuocNgayCong <= XL2.ThangKetCong && XL2.ThangKetCong != DateTime.MinValue) return;
			// 1 da tinh cong day du,11 xac nhan lai ca(2 trg hop: ca vua xac nhan: ko biet id, ca), xac nhan ca moi
			var n = DAL.XacNhanGio_A(nhanvien.MaCC, caMoi.ID, caMoi.Code, caMoi.OnnTS.ToString(@"hh\:mm"), caMoi.OffTS.ToString(@"hh\:mm")
									 , caMoi.LateeMin.Minutes, caMoi.EarlyMin.Minutes, caMoi.AfterOTMin.Minutes, caMoi.DayCount, (int)caMoi.WorkingTimeTS.TotalMinutes, caMoi.Workingday, sophutlamthem, TreSomTinhCV //choPhepTinhPC50//[140615_2]
									 , CIO.Vao.Time, CIO.Raa.Time, CIO.Vao.Source, CIO.Raa.Source, CIO.Vao.MachineNo, CIO.Raa.MachineNo);
			var id = Int32.Parse(n.Rows[0][0].ToString());
			// từ CIO_A thành CIO_V => 1. xoá khỏi CIO_A, tạo mới bên CIO_V
			nhanvien.DS_Check_A.Remove(CIO.Vao);
			nhanvien.DS_Check_A.Remove(CIO.Raa);
			nhanvien.DS_CIO_A.Remove((cChkInOut_A)CIO);
			var checkInn_V = new cChkInn_V { ID = id, MaCC = nhanvien.MaCC, Type = "I", Time = CIO.Vao.Time, Source = CIO.Vao.Source, MachineNo = CIO.Vao.MachineNo, IsEdited = 1, PhucHoi = new cPhucHoi { IDGioGoc = CIO.Vao.PhucHoi.IDGioGoc, Them = CIO.Vao.PhucHoi.Them, Xoaa = CIO.Vao.PhucHoi.Xoaa } };
			var checkOut_V = new cChkOut_V { ID = id, MaCC = nhanvien.MaCC, Type = "O", Time = CIO.Raa.Time, Source = CIO.Raa.Source, MachineNo = CIO.Raa.MachineNo, IsEdited = 1, PhucHoi = new cPhucHoi { IDGioGoc = CIO.Raa.PhucHoi.IDGioGoc, Them = CIO.Raa.PhucHoi.Them, Xoaa = CIO.Raa.PhucHoi.Xoaa } };
			var CIO_V = new cChkInOut_V { TG = new ThoiGian { OTCa = new TimeSpan(0, sophutlamthem, 0), OLai = TimeSpan.Zero }, TD = new ThoiDiem(), ID = id, IsEdited = 1, ThuocCa = caMoi, ThuocNgayCong = ThuocNgayCong(checkInn_V.Time), Vao = checkInn_V, Raa = checkOut_V, TimeDaiDien = checkInn_V.Time, TreSomTinhCV = TreSomTinhCV}; //, TinhPC150 = choPhepTinhPC50//[140615_2]
			CheckTinhPC50(nhanvien, CIO_V.ThuocNgayCong, choPhepTinhPC50);
			TinhTGLam_TGQuaDem(CIO_V, CIO_V.ThuocCa);
			nhanvien.DS_CIO_V.Add(CIO_V);
			TronDS_CIO_A_V(nhanvien.DS_CIO_A, nhanvien.DS_CIO_V, nhanvien.DSVaoRa);
			TinhCongTheoNgay(nhanvien.DSVaoRa, nhanvien.NgayCongBD_Bef2D, nhanvien.NgayCongKT_Aft2D, nhanvien.DSVang, nhanvien.DSNgayCong, nhanvien.MacDinhTinhPC50);
			TinhLaiPhuCapTC(nhanvien.DSXNPhuCap50, nhanvien.DSNgayCong);
			TinhLaiPhuCapDB(nhanvien.DSXNPhuCapDB, nhanvien.DSNgayCong);
		}

		public static void XacNhanKoTachCaChoQL(cUserInfo nhanvien, cChkInOut CIO, cCaAbs caMoi, int sophutlamthem, bool choPhepTinhPC50, bool TreSomTinhCV) {
			//[140615_2]
			if (CIO.ThuocNgayCong <= XL2.ThangKetCong && XL2.ThangKetCong != DateTime.MinValue) return;

			// 1 da tinh cong day du,11 xac nhan lai ca(2 trg hop: ca vua xac nhan: ko biet id, ca), xac nhan ca moi
			var n = DAL.XacNhanGio_A(nhanvien.MaCC, caMoi.ID, caMoi.Code, caMoi.OnnTS.ToString(@"hh\:mm"), caMoi.OffTS.ToString(@"hh\:mm")
									 , caMoi.LateeMin.Minutes, caMoi.EarlyMin.Minutes, caMoi.AfterOTMin.Minutes, caMoi.DayCount, (int)caMoi.WorkingTimeTS.TotalMinutes, caMoi.Workingday, sophutlamthem, TreSomTinhCV //choPhepTinhPC50//[140615_2]
									 , CIO.Vao.Time, CIO.Raa.Time, CIO.Vao.Source, CIO.Raa.Source, CIO.Vao.MachineNo, CIO.Raa.MachineNo);
			if (choPhepTinhPC50) {
				CheckTinhPC50(nhanvien, CIO.ThuocNgayCong, choPhepTinhPC50);
			}
		}

		public static void XacNhanCoTachCa(cUserInfo nhanvien, cChkInOut CIO, cCaAbs caMoi, int sophutlamthem, bool choPhepTinhPC50, bool TreSomTinhCV) {
			////[140615_2]
			// 1 da tinh cong day du,11 xac nhan lai ca(2 trg hop: ca vua xac nhan: ko biet id, ca), xac nhan ca moi
			var ca3_va1_hoac1A = (cCaChuan)caMoi;
			var ca3 = ca3_va1_hoac1A.catruoc;
			var ca1_1A = ca3_va1_hoac1A.casauuu;
			var ngaycongCa3 = (CIO.Vao.Time.TimeOfDay < XL2._04h30) ? CIO.Vao.Time.Date.AddDays(-1d) : CIO.Vao.Time.Date;
			var ngaycongCa1 = ngaycongCa3.AddDays(1d);
			var timeOut05h45 = ngaycongCa3.Add(ca3.OffTS);
			var timeInn05h45 = ngaycongCa1.Add(ca1_1A.OnnTS).Add(XL2._01giay);

			if (ngaycongCa3 <= XL2.ThangKetCong && XL2.ThangKetCong != DateTime.MinValue) return;

			var kq1 = DAL.ThemGioChoNV(nhanvien.MaCC, timeOut05h45, "O", 22, XL2.currUserID, "", "tách giờ ca 3&1, ca 3&1A");
			var kq2 = DAL.ThemGioChoNV(nhanvien.MaCC, timeInn05h45, "I", 21, XL2.currUserID, "", "tách giờ ca 3&1, ca 3&1A");

			var n1 = DAL.XacNhanGio_A(nhanvien.MaCC, ca3.ID, ca3.Code, ca3.OnnTS.ToString(@"hh\:mm"), ca3.OffTS.ToString(@"hh\:mm")
									  , ca3.LateeMin.Minutes, ca3.EarlyMin.Minutes, ca3.AfterOTMin.Minutes, ca3.DayCount, (int)ca3.WorkingTimeTS.TotalMinutes, ca3.Workingday, 0, TreSomTinhCV //, choPhepTinhPC50//[140615_2]
									  , CIO.Vao.Time, timeOut05h45, CIO.Vao.Source, "PC", CIO.Vao.MachineNo, 22);
			var id1 = Int32.Parse(n1.Rows[0][0].ToString());

			var n2 = DAL.XacNhanGio_A(nhanvien.MaCC, ca1_1A.ID, ca1_1A.Code, ca1_1A.OnnTS.ToString(@"hh\:mm"), ca1_1A.OffTS.ToString(@"hh\:mm")
									  , ca1_1A.LateeMin.Minutes, ca1_1A.EarlyMin.Minutes, ca1_1A.AfterOTMin.Minutes, ca1_1A.DayCount, (int)ca1_1A.WorkingTimeTS.TotalMinutes, ca1_1A.Workingday, sophutlamthem, TreSomTinhCV //, choPhepTinhPC50//[140615_2]
									  , timeInn05h45, CIO.Raa.Time, "PC", CIO.Raa.Source, 21, CIO.Raa.MachineNo);
			var id2 = Int32.Parse(n2.Rows[0][0].ToString());


			var checkInnCa3_V = new cChkInn_V { ID = id1, MaCC = nhanvien.MaCC, Type = "I", Time = CIO.Vao.Time, Source = CIO.Vao.Source, MachineNo = CIO.Vao.MachineNo, IsEdited = 1, PhucHoi = new cPhucHoi { IDGioGoc = CIO.Vao.PhucHoi.IDGioGoc, Them = CIO.Vao.PhucHoi.Them, Xoaa = CIO.Vao.PhucHoi.Xoaa } };
			var checkOutCa3_V = new cChkOut_V { ID = id1, MaCC = nhanvien.MaCC, Type = "O", Time = timeOut05h45, Source = "PC", MachineNo = 22, IsEdited = 1, PhucHoi = new cPhucHoi { IDGioGoc = -1, Them = true, Xoaa = false }, };
			var checkInnCa1_V = new cChkInn_V { ID = id2, MaCC = nhanvien.MaCC, Type = "I", Time = timeInn05h45, Source = "PC", MachineNo = 21, IsEdited = 1, PhucHoi = new cPhucHoi { IDGioGoc = -1, Them = true, Xoaa = false }, };
			var checkOutCa1_V = new cChkOut_V { ID = id2, MaCC = nhanvien.MaCC, Type = "O", Time = CIO.Raa.Time, Source = CIO.Raa.Source, MachineNo = CIO.Raa.MachineNo, IsEdited = 1, PhucHoi = new cPhucHoi { IDGioGoc = CIO.Raa.PhucHoi.IDGioGoc, Them = CIO.Raa.PhucHoi.Them, Xoaa = CIO.Raa.PhucHoi.Xoaa } };


			// từ CIO_A thành CIO_V => 1. xoá khỏi CIO_A, tạo mới bên CIO_V
			nhanvien.DS_Check_A.Remove(CIO.Vao);
			nhanvien.DS_Check_A.Remove(CIO.Raa);
			nhanvien.DS_CIO_A.Remove((cChkInOut_A)CIO);
			var CIO_Ca3_V = new cChkInOut_V { ID = id1, TG = new ThoiGian { OTCa = TimeSpan.Zero, OLai = TimeSpan.Zero }, TD = new ThoiDiem { }, IsEdited = 1, ThuocCa = ca3, ThuocNgayCong = ngaycongCa3, Vao = checkInnCa3_V, Raa = checkOutCa3_V, TimeDaiDien = checkInnCa3_V.Time, TreSomTinhCV = TreSomTinhCV}; //TinhPC150 = choPhepTinhPC50,//[140615_2]
			var CIO_Ca1_V = new cChkInOut_V { ID = id2, TG = new ThoiGian { OTCa = new TimeSpan(0, sophutlamthem, 0), OLai = TimeSpan.Zero }, TD = new ThoiDiem { }, IsEdited = 1, ThuocCa = ca1_1A, ThuocNgayCong = ngaycongCa1, Vao = checkInnCa1_V, Raa = checkOutCa1_V, TimeDaiDien = checkInnCa1_V.Time, TreSomTinhCV = TreSomTinhCV}; //TinhPC150 = choPhepTinhPC50,//[140615_2]
			CheckTinhPC50(nhanvien, CIO_Ca3_V.ThuocNgayCong, choPhepTinhPC50);
			CheckTinhPC50(nhanvien, CIO_Ca1_V.ThuocNgayCong, choPhepTinhPC50);
			TinhTGLam_TGQuaDem(CIO_Ca3_V, ca3);
			TinhTGLam_TGQuaDem(CIO_Ca1_V, ca1_1A);
			nhanvien.DS_CIO_V.Add(CIO_Ca3_V);
			nhanvien.DS_CIO_V.Add(CIO_Ca1_V);
			TronDS_CIO_A_V(nhanvien.DS_CIO_A, nhanvien.DS_CIO_V, nhanvien.DSVaoRa);
			TinhCongTheoNgay(nhanvien.DSVaoRa, nhanvien.NgayCongBD_Bef2D, nhanvien.NgayCongKT_Aft2D, nhanvien.DSVang, nhanvien.DSNgayCong, nhanvien.MacDinhTinhPC50);
			TinhLaiPhuCapTC(nhanvien.DSXNPhuCap50, nhanvien.DSNgayCong);
			TinhLaiPhuCapDB(nhanvien.DSXNPhuCapDB, nhanvien.DSNgayCong);

		}

		public static void XacNhanCoTachCaChoQL(cUserInfo nhanvien, cChkInOut CIO, cCaAbs caMoi, int sophutlamthem, bool choPhepTinhPC50, bool TreSomTinhCV) {
			////[140615_2]
			// 1 da tinh cong day du,11 xac nhan lai ca(2 trg hop: ca vua xac nhan: ko biet id, ca), xac nhan ca moi
			var ca3_va1_hoac1A = (cCaChuan)caMoi;
			var ca3 = ca3_va1_hoac1A.catruoc;
			var ca1_1A = ca3_va1_hoac1A.casauuu;
			var ngaycongCa3 = (CIO.Vao.Time.TimeOfDay < XL2._04h30) ? CIO.Vao.Time.Date.AddDays(-1d) : CIO.Vao.Time.Date;
			var ngaycongCa1 = ngaycongCa3.AddDays(1d);
			var timeOut05h45 = ngaycongCa3.Add(ca3.OffTS);
			var timeInn05h45 = ngaycongCa1.Add(ca1_1A.OnnTS).Add(XL2._01giay);

			if (ngaycongCa3 <= XL2.ThangKetCong && XL2.ThangKetCong != DateTime.MinValue) return;

			var kq1 = DAL.ThemGioChoNV(nhanvien.MaCC, timeOut05h45, "O", 22, XL2.currUserID, "", "tách giờ ca 3&1, ca 3&1A");
			var kq2 = DAL.ThemGioChoNV(nhanvien.MaCC, timeInn05h45, "I", 21, XL2.currUserID, "", "tách giờ ca 3&1, ca 3&1A");

			var n1 = DAL.XacNhanGio_A(nhanvien.MaCC, ca3.ID, ca3.Code, ca3.OnnTS.ToString(@"hh\:mm"), ca3.OffTS.ToString(@"hh\:mm")
									  , ca3.LateeMin.Minutes, ca3.EarlyMin.Minutes, ca3.AfterOTMin.Minutes, ca3.DayCount, (int)ca3.WorkingTimeTS.TotalMinutes, ca3.Workingday, 0, TreSomTinhCV //, choPhepTinhPC50//[140615_2]
									  , CIO.Vao.Time, timeOut05h45, CIO.Vao.Source, "PC", CIO.Vao.MachineNo, 22);

			var n2 = DAL.XacNhanGio_A(nhanvien.MaCC, ca1_1A.ID, ca1_1A.Code, ca1_1A.OnnTS.ToString(@"hh\:mm"), ca1_1A.OffTS.ToString(@"hh\:mm")
									  , ca1_1A.LateeMin.Minutes, ca1_1A.EarlyMin.Minutes, ca1_1A.AfterOTMin.Minutes, ca1_1A.DayCount, (int)ca1_1A.WorkingTimeTS.TotalMinutes, ca1_1A.Workingday, sophutlamthem, TreSomTinhCV //, choPhepTinhPC50//[140615_2]
									  , timeInn05h45, CIO.Raa.Time, "PC", CIO.Raa.Source, 21, CIO.Raa.MachineNo);

			CheckTinhPC50(nhanvien, ngaycongCa3, choPhepTinhPC50);
			CheckTinhPC50(nhanvien, ngaycongCa1, choPhepTinhPC50);
		}

		public static void XacNhanLaiKoTachCa(cUserInfo nhanvien, ref cChkInOut CIO, cCaAbs caMoi, int sophutlamthem, bool choPhepTinhPC50, bool TreSomTinhCV) {
			////[140615_2]
			if (CIO.ThuocNgayCong <= XL2.ThangKetCong && XL2.ThangKetCong != DateTime.MinValue) return;

			var n = DAL.CapNhatXacNhanGio_V(nhanvien.MaCC, ((cChkInn_V)CIO.Vao).ID, caMoi.ID, caMoi.Code, caMoi.OnnTS.ToString(@"hh\:mm"), caMoi.OffTS.ToString(@"hh\:mm")
											, caMoi.LateeMin.Minutes, caMoi.EarlyMin.Minutes, caMoi.AfterOTMin.Minutes, caMoi.DayCount, (int)caMoi.WorkingTimeTS.TotalMinutes, caMoi.Workingday, sophutlamthem, TreSomTinhCV //, choPhepTinhPC50//[140615_2]
											, CIO.Vao.Time, CIO.Raa.Time, CIO.Vao.Source, CIO.Raa.Source, CIO.Vao.MachineNo, CIO.Raa.MachineNo);
			//var id = Int32.Parse(n.Rows[0][0].ToString());
			CheckTinhPC50(nhanvien, CIO.ThuocNgayCong, choPhepTinhPC50);
			// update lại cio_V
			CIO.Vao.IsEdited += 1;
			CIO.Raa.IsEdited += 1;
			CIO.IsEdited += 1;
			CIO.ThuocCa = caMoi;
			//CIO.TinhPC150 = choPhepTinhPC50;//[140615_2]
			CIO.TG.OTCa = new TimeSpan(0, sophutlamthem, 0);
		    CIO.TreSomTinhCV = TreSomTinhCV;
			TinhTGLam_TGQuaDem(CIO, CIO.ThuocCa);
			//XL.TronDS_CIO_A_V(nhanvien.DS_CIO_A, nhanvien.DS_CIO_V, nhanvien.DSVaoRa);
			TinhCongTheoNgay(nhanvien.DSVaoRa, nhanvien.NgayCongBD_Bef2D, nhanvien.NgayCongKT_Aft2D, nhanvien.DSVang, nhanvien.DSNgayCong, nhanvien.MacDinhTinhPC50);
			TinhLaiPhuCapTC(nhanvien.DSXNPhuCap50, nhanvien.DSNgayCong);
			TinhLaiPhuCapDB(nhanvien.DSXNPhuCapDB, nhanvien.DSNgayCong);

		}

		#endregion

		#region tạo table dsnv, table xem công, điểm danh, giờ thiếu chấm công, giờ ko nhận diện được, xác nhận ca và tăng ca

		public static void TaoTableDSNV(List<cUserInfo> m_DSNV, DataTable m_tableDSNV) {
			if (m_DSNV == null || m_DSNV.Count == 0) return;

			foreach (cUserInfo nhanvien in m_DSNV) {
				var row = m_tableDSNV.NewRow();
				row["check"] = false;
				row["cUserInfo"] = nhanvien;
				row["UserEnrollNumber"] = nhanvien.MaCC;
				row["UserFullCode"] = nhanvien.MaNV;
				row["UserFullName"] = nhanvien.TenNV;
				row["SchID"] = nhanvien.LichTrinhLV.SchID;
				row["SchName"] = nhanvien.LichTrinhLV.TenLichTrinh;
				row["TitleName"] = nhanvien.TitleName;
				row["IDD_1"] = nhanvien.PBCap1.ID;
				row["Description_1"] = nhanvien.PBCap1.TenPhongBan;
				row["HeSoLuongCB"] = nhanvien.HeSo.LuongCB;
				row["HeSoLuongSP"] = nhanvien.HeSo.LuongCV;
				row["HSBHCongThem"] = nhanvien.HeSo.BHXH_YT_TN - nhanvien.HeSo.LuongCB;
				m_tableDSNV.Rows.Add(row);
			}
		}

		public static void TaoTableDSNV_ThayDoiThongTin(List<cUserInfo> m_DSNV, DataTable m_tableDSNV) {
			if (m_DSNV == null || m_DSNV.Count == 0) return;

			foreach (cUserInfo nhanvien in m_DSNV) {
				var row = m_tableDSNV.NewRow();
				row["check"] = false;
				row["cUserInfo"] = nhanvien;
				row["UserEnrollNumber"] = nhanvien.MaCC;
				row["UserFullCode"] = nhanvien.MaNV;
				row["UserFullName"] = nhanvien.TenNV;
				row["SchID"] = nhanvien.LichTrinhLV.SchID;
				row["SchName"] = nhanvien.LichTrinhLV.TenLichTrinh;
				row["TitleName"] = nhanvien.TitleName;
				row["IDD_1"] = nhanvien.PBCap1.ID;
				row["Description_1"] = nhanvien.PBCap1.TenPhongBan;
				row["HeSoLuongCB"] = nhanvien.HeSo.LuongCB;
				row["HeSoLuongSP"] = nhanvien.HeSo.LuongCV;
				row["HSBHCongThem"] = nhanvien.HeSo.BHXH_YT_TN - nhanvien.HeSo.LuongCB;
				row["UserEnabled"] = nhanvien.IsUserEnabled;
				row["TinhLuongCongNhat"] = nhanvien.IsDailySalary;
				m_tableDSNV.Rows.Add(row);
			}
		}

		public static void TaoTableDSNV_PhucHoi(List<cUserInfo> m_DSNV, DataTable m_tableDSNV) {
			if (m_DSNV == null || m_DSNV.Count == 0) return;

			foreach (cUserInfo nhanvien in m_DSNV) {
				var row = m_tableDSNV.NewRow();
				row["check"] = false;
				row["cUserInfo"] = nhanvien;
				row["UserEnrollNumber"] = nhanvien.MaCC;
				row["UserFullCode"] = nhanvien.MaNV;
				row["UserFullName"] = nhanvien.TenNV;
				row["TitleName"] = nhanvien.TitleName;
				row["IDD_1"] = nhanvien.PBCap1.ID;
				row["Description_1"] = nhanvien.PBCap1.TenPhongBan;
				row["HeSoLuongCB"] = nhanvien.HeSo.LuongCB;
				row["HeSoLuongSP"] = nhanvien.HeSo.LuongCV;
				row["HSBHCongThem"] = nhanvien.HeSo.BHXH_YT_TN - nhanvien.HeSo.LuongCB;
				m_tableDSNV.Rows.Add(row);
			}
		}

		public static void TaoTableDiemDanh(List<cUserInfo> dsnv, DataTable kq, out int SoNVDangLamViec, out int SoNVDaRaVe, out int SoNVVang) {
			SoNVDangLamViec = 0;
			SoNVDaRaVe = 0;
			SoNVVang = 0;

			foreach (var nhanvien in dsnv) {
				var row = kq.NewRow();
				row["UserEnrollNumber"] = nhanvien.MaCC;
				row["UserFullCode"] = nhanvien.MaNV;
				row["UserFullName"] = nhanvien.TenNV;
				var ngayCong = nhanvien.DSNgayCong[2];
				//nếu có check thì khỏi ghi vắng
				var ChuoiCa = String.Empty;
				var ChuoiTrangThai = String.Empty;
				if (ngayCong.HasCheck) {
					for (var i = 0; i < ngayCong.DSVaoRa.Count; i++) {
						if (i >= 3) break;
						var CIO = ngayCong.DSVaoRa[i];
						row["TimeStrVao" + (i + 1)] = (CIO.Vao != null) ? CIO.Vao.Time : (object)DBNull.Value;
						row["TimeStrRaa" + (i + 1)] = (CIO.Raa != null) ? CIO.Raa.Time : (object)DBNull.Value;
						/*CIO.LayChuoiThuocCa_01(ref ChuoiCa);*/

					}
					var lastCIO1 = ngayCong.DSVaoRa[ngayCong.DSVaoRa.Count - 1];
					// xét vào ra cuối để ghi trạng thái
					if (lastCIO1.HaveINOUT == -1) {
						ChuoiTrangThai = "Đang làm việc; ";
						SoNVDangLamViec++;
					}
					else if (lastCIO1.HaveINOUT >= 0 || lastCIO1.HaveINOUT == -2) {
						ChuoiTrangThai = "Đã ra về; ";
						SoNVDaRaVe++;
					}

				}
				else {
					// không có check, kiểm tra có khai báo vắng ko, nếu có thì ghi
					SoNVVang++;
					/*ChuoiCa = String.Empty;*/
				}
				row["Ca"] = ngayCong.CIOs_Absents_Code_Full();
				row["TrangThai"] = ngayCong.Absents_Code() + ";" + ChuoiTrangThai;
				kq.Rows.Add(row);
			}
		}

		public static void TaoTableXemCong(List<cUserInfo> dsnv, DataTable kq) {

			foreach (var nhanvien in dsnv) {
				//duyệt qua từng ngày công của nhân viên để fill datarow
				for (var x = 2; x < nhanvien.DSNgayCong.Count - 2; x++) {
					//[CHÚ Ý] ở đây chỉ chạy từ 1-> kế cuối
					var ngayCong = nhanvien.DSNgayCong[x];
					var row = kq.NewRow();
					row["cNgayCong"] = ngayCong;
					row["cUserInfo"] = nhanvien;
					row["UserEnrollNumber"] = nhanvien.MaCC;
					row["UserFullName"] = nhanvien.TenNV;
					row["UserFullCode"] = nhanvien.MaNV;
					row["TimeStrNgay"] = row["TimeStrThu"] = ngayCong.Ngay;
					row["TongTre"] = Math.Floor(ngayCong.TG.VaoTre.TotalMinutes);
					row["TongSom"] = Math.Floor(ngayCong.TG.RaaSom.TotalMinutes);
					row["TongGioLam"] = ngayCong.TG.GioLamTrongNgay.TotalHours;
					row["TongGioThuc"] = ngayCong.TG.GioThuc.TotalHours;
					row["TongCong"] = ngayCong.TongCong;
					row["TongPhuCap"] = ngayCong.TongPhuCap;
					row["IsEdited"] = (ngayCong.IsEdited > 0);
					row["TinhPCTC"] = ngayCong.TinhPC50;
					if (ngayCong.HasCheck == false && ngayCong.DSVang.Count == 0)
						row["ShiftCode"] = "--";
					else {
						if (ngayCong.HasCheck) {
							var i = 1;
							foreach (var CIO in ngayCong.DSVaoRa) {
								if (i > 3) break;
								row["cChkInOut" + i] = CIO;
								switch (CIO.HaveINOUT) {
									case -1:
										row["TimeStrVao" + i] = CIO.Vao.Time;
										break;
									case -2:
										row["TimeStrRaa" + i] = CIO.Raa.Time;
										break;
									case 0:
									case 30:
										row["TimeStrVao" + i] = CIO.Vao.Time;
										row["TimeStrRaa" + i] = CIO.Raa.Time;
										break;
								}
								i++;
							}
						}
						row["ShiftCode"] = ngayCong.CIOs_Absents_Code_Comp();
					}
					kq.Rows.Add(row);
				}
			}
		}

		public static void TaoTableGioKDQD(List<cUserInfo> dsnvDiemDanh, DataTable kq) {
			foreach (cUserInfo nhanvien in dsnvDiemDanh) {
				//duyệt qua từng ngày công của nhân viên để fill datarow
				for (var x = 2; x < nhanvien.DSNgayCong.Count - 2; x++) {
					//[CHÚ Ý] ở đây chỉ chạy từ 1-> kế cuối
					var ngayCong = nhanvien.DSNgayCong[x];
					if (ngayCong.DSVaoRa.Exists(o => o.HaveINOUT == 0 && o.GetType() == typeof(cChkInOut_A) && o.ThuocCa.ID < 0)) {
						var row = kq.NewRow();
						row["cNgayCong"] = ngayCong;
						row["cUserInfo"] = nhanvien;
						row["UserEnrollNumber"] = nhanvien.MaCC;
						row["UserFullName"] = nhanvien.TenNV;
						row["UserFullCode"] = nhanvien.MaNV;
						row["TimeStrNgay"] = row["TimeStrThu"] = ngayCong.Ngay;
						row["TongGioLam"] = ngayCong.TG.GioLamTrongNgay.TotalHours;
						row["TongGioThuc"] = ngayCong.TG.GioThuc.TotalHours;
						row["TongCong"] = ngayCong.TongCong;
						row["TongPhuCap"] = ngayCong.TongPhuCap;
						row["IsEdited"] = (ngayCong.IsEdited > 0);
						var i = 1;
						foreach (var CIO in ngayCong.DSVaoRa) {
							if (i > 3) break;
							row["cChkInOut" + i] = CIO;
							switch (CIO.HaveINOUT) {
								case -1:
									row["TimeStrVao" + i] = CIO.Vao.Time;
									break;
								case -2:
									row["TimeStrRaa" + i] = CIO.Raa.Time;
									break;
								case 0:
								case 30:
									row["TimeStrVao" + i] = CIO.Vao.Time;
									row["TimeStrRaa" + i] = CIO.Raa.Time;
									break;
							}
							i++;
						}
						row["ShiftCode"] = ngayCong.CIOs_Absents_Code_Comp();
						kq.Rows.Add(row);
					}
				}
			}
		}

		public static void TaoTableGioThieuCheck(List<cUserInfo> dsnvDiemDanh, DataTable kq) {
			foreach (cUserInfo nhanvien in dsnvDiemDanh) {
				//duyệt qua từng ngày công của nhân viên để fill datarow
				for (var x = 2; x < nhanvien.DSNgayCong.Count - 2; x++) {
					//[CHÚ Ý] ở đây chỉ chạy từ 1-> kế cuối
					var ngayCong = nhanvien.DSNgayCong[x];
					if (ngayCong.DSThieu_VaoHoacRa.Count == 0) continue; // nếu ko có giờ check KDQD thì bỏ qua
					// tồn tại 
					var row = kq.NewRow();
					row["cNgayCong"] = ngayCong;
					row["cUserInfo"] = nhanvien;
					row["UserEnrollNumber"] = nhanvien.MaCC;
					row["UserFullName"] = nhanvien.TenNV;
					row["UserFullCode"] = nhanvien.MaNV;
					row["TimeStrNgay"] = row["TimeStrThu"] = ngayCong.Ngay;
					row["TongGioLam"] = ngayCong.TG.GioLamTrongNgay.TotalHours;
					row["TongGioThuc"] = ngayCong.TG.GioThuc.TotalHours;
					row["TongCong"] = ngayCong.TongCong;
					row["TongPhuCap"] = ngayCong.TongPhuCap;
					row["IsEdited"] = (ngayCong.IsEdited > 0);
					var i = 1;
					foreach (var CIO in ngayCong.DSVaoRa) {
						if (i > 3) break;
						row["cChkInOut" + i] = CIO;
						switch (CIO.HaveINOUT) {
							case -1:
								row["TimeStrVao" + i] = CIO.Vao.Time;
								break;
							case -2:
								row["TimeStrRaa" + i] = CIO.Raa.Time;
								break;
							case 0:
							case 30:
								row["TimeStrVao" + i] = CIO.Vao.Time;
								row["TimeStrRaa" + i] = CIO.Raa.Time;
								break;
						}
						i++;
					}
					row["ShiftCode"] = ngayCong.CIOs_Absents_Code_Comp();
					kq.Rows.Add(row);
				}
			}

		}

		public static void TaoTableThK_TreSom(List<cUserInfo> dsnvDiemDanh, DataTable kq) {
			foreach (cUserInfo nhanvien in dsnvDiemDanh) {
				//duyệt qua từng ngày công của nhân viên để fill datarow
				for (var x = 2; x < nhanvien.DSNgayCong.Count - 2; x++) {
					//[CHÚ Ý] ở đây chỉ chạy từ 1-> kế cuối
					var ngayCong = nhanvien.DSNgayCong[x];
					if (ngayCong.TG.VaoTre.TotalMinutes < 1f && ngayCong.TG.RaaSom.TotalMinutes < 1f) continue; // nếu ko có giờ check KDQD thì bỏ qua
					// tồn tại 
					var row = kq.NewRow();
					row["cNgayCong"] = ngayCong;
					row["cUserInfo"] = nhanvien;
					row["UserEnrollNumber"] = nhanvien.MaCC;
					row["UserFullName"] = nhanvien.TenNV;
					row["UserFullCode"] = nhanvien.MaNV;
					row["TimeStrNgay"] = row["TimeStrThu"] = ngayCong.Ngay;
					row["TongGioLam"] = ngayCong.TG.GioLamTrongNgay.TotalHours;
					row["TongGioThuc"] = ngayCong.TG.GioThuc.TotalHours;
					row["TongCong"] = ngayCong.TongCong;
					row["TongPhuCap"] = ngayCong.TongPhuCap;
					row["TongTre"] = ngayCong.TG.VaoTre.TotalMinutes;
					row["TongSom"] = ngayCong.TG.RaaSom.TotalMinutes;
					row["IsEdited"] = (ngayCong.IsEdited > 0);
					var i = 1;
					var flag_IsShow = false;
					foreach (var CIO in ngayCong.DSVaoRa) {
						if (i > 3) break;
						if (CIO.HaveINOUT >= 0)
							flag_IsShow = true;
						row["cChkInOut" + i] = CIO;
						row["TreSomTinhCV" + i] = CIO.TreSomTinhCV;
						switch (CIO.HaveINOUT) {
							case -1:
								row["TimeStrVao" + i] = CIO.Vao.Time;
								break;
							case -2:
								row["TimeStrRaa" + i] = CIO.Raa.Time;
								break;
							case 0:
							case 30:
								row["TimeStrVao" + i] = CIO.Vao.Time;
								row["TimeStrRaa" + i] = CIO.Raa.Time;
								break;
						}
						i++;
					}
					row["ShiftCode"] = ngayCong.CIOs_Absents_Code_Comp();
					
					if (flag_IsShow)
						kq.Rows.Add(row);
				}
			}

		}

		public static void TaoTableXacNhanTangCa(List<cUserInfo> dsnv, DataTable table) {
			foreach (var nhanvien in dsnv) {
				foreach (var ngayCong in nhanvien.DSNgayCong) {

					foreach (var CIO in ngayCong.DSVaoRa) {
						var row = table.NewRow();
						if (CIO.HaveINOUT < 0) continue;
						row["tieuchi3"] = (ngayCong.TongCong > 1f);
						row["tieuchi2"] = (CIO.GetType() == typeof(cChkInOut_V));
						row["tieuchi1"] = (CIO.GetType() == typeof(cChkInOut_A) && (CIO.TG.OLai > XL2._0gio));

						#region fill datarow

						row["UserEnrollNumber"] = nhanvien.MaCC;
						row["UserFullCode"] = nhanvien.MaNV;
						row["UserFullName"] = nhanvien.TenNV;
						row["TimeStrNgay"] = ngayCong.Ngay;
						row["TimeStrVao"] = CIO.Vao.Time;
						row["TimeStrRaa"] = CIO.Raa.Time;
						row["ShiftID"] = CIO.ThuocCa.ID;
						row["ShiftCode"] += CIO.CIOCodeComp();
						row["Cong"] = CIO.Cong;
						row["TongGioLam"] = CIO.TG.GioLamTrongNgay.TotalHours;
						row["TongGioThuc"] = CIO.TG.GioThuc.TotalHours;
						row["cNgayCong"] = ngayCong;
						row["cUserInfo"] = nhanvien;
						row["cChkInOut"] = CIO;
						row["IsEdited"] = (CIO.IsEdited > 0);

						#endregion

						table.Rows.Add(row);
					}
				}
			}
		}

		#endregion


		public static bool KiemtraDulieuCapnhatTuServer(DateTime now) {
			var gioSauCung = DAL.LayDuLieuTGCuoi();
			return (gioSauCung != DateTime.MinValue && now - gioSauCung <= XL2._07h00);
		}

		#region tính lương

		public static void ThongKe(cUserInfo nhanvien) {
			for (var i = 2; i < nhanvien.DSNgayCong.Count - 2; i++) {
				var ngayCong = nhanvien.DSNgayCong[i];
				nhanvien.Tong.CongThang += ngayCong.TongCong;
				nhanvien.Tong.PCapThang += ngayCong.TongPhuCap;
			    nhanvien.Tong.CongCVTreSom += ngayCong.TruCongCV;
				if (ngayCong.TinhPCDB) {
					nhanvien.Tong.PC100 += ngayCong.PhuCap100;
					nhanvien.Tong.PC160 += ngayCong.PhuCap160;
					nhanvien.Tong.PC200 += ngayCong.PhuCap200;
					nhanvien.Tong.PC290 += ngayCong.PhuCap290;
				}
				else {
					if (ngayCong.QuaDem) nhanvien.Tong.PC30 += ngayCong.PhuCap30;
					if (ngayCong.TinhPC50) nhanvien.Tong.PC50 += ngayCong.PhuCap50;
					if (ngayCong.TinhPCTCC3) nhanvien.Tong.PCTCC3 += ngayCong.PhuCapTCC3;
				}
				if (ngayCong.QuaDem) nhanvien.Tong.NgayQuaDem++;

				if (ngayCong.DSVang != null) {
					foreach (var loaiVang in ngayCong.DSVang) {
						switch (loaiVang.KyHieu) {
							case "P":
								nhanvien.Tong.Phep = nhanvien.Tong.Phep + loaiVang.Cong;
								break;
							case "CV":
								nhanvien.Tong.CongCV = nhanvien.Tong.CongCV + loaiVang.Cong;
								break;
							case "BH":
							case "TS":
								nhanvien.Tong.BHXH = nhanvien.Tong.BHXH + loaiVang.Cong;
								break;
							case "PT":
							case "H":
							case "CT":
								nhanvien.Tong.H_CT_PT = nhanvien.Tong.H_CT_PT + loaiVang.Cong;
								break;
							case "RO":
								nhanvien.Tong.NghiRo = nhanvien.Tong.NghiRo + loaiVang.Cong;
								break;
							case "L":
								nhanvien.Tong.Le = nhanvien.Tong.Le + loaiVang.Cong;
								break;
						}
					}
				}

			}

			// làm tròn lên 2 con số của tổng công
			nhanvien.Tong.CongThang = Math.Round(nhanvien.Tong.CongThang, 2);
		}


		public static void TinhCongChoViec(DateTime ngayDauThang, double tongcong, double tongphep, double tongle, double tongH, double tongbhxh, double tongRO, double tongCV_Khaibao, double treCongCV, out double CongCV) {
			var soNgayCN = DemSoNgayCN(ngayDauThang);
			var CongChuan = Convert.ToDouble((DateTime.DaysInMonth(ngayDauThang.Year, ngayDauThang.Month) - soNgayCN));
			var tong = treCongCV + tongcong + tongphep + tongH + tongle + tongCV_Khaibao + tongbhxh + tongRO;
			if (tong < CongChuan)
				CongCV = tongCV_Khaibao + (CongChuan - tong);
			else
				CongCV = tongCV_Khaibao;
		}

		public static int DemSoNgayCN(DateTime ngayDauThang) {
			var ngayBD = ngayDauThang;
			var ngayKT = new DateTime(ngayDauThang.Year, ngayDauThang.Month, DateTime.DaysInMonth(ngayDauThang.Year, ngayDauThang.Month));
			int kq = 0;
			for (var ngaydem = ngayBD; ngaydem.Date <= ngayKT.Date; ngaydem = ngaydem.AddDays(1d)) {
				if (ngaydem.DayOfWeek != DayOfWeek.Sunday) continue;
				kq++;
			}
			return kq;
		}



		public static void TinhSPLamRa_CongVaPC_B102(double HSLCV, double congthang, double PCapThang, double phep, double H_CT_PT, double Le,
			out double LuongSP_LamRa_TheoCong, out double LuongSP_LamRa_TheoPCap, out double spLamRa) {
			var b11 = HSLCV * (congthang + phep + H_CT_PT + Le);// ko có chờ việc
			LuongSP_LamRa_TheoCong = b11;
			var b12 = PCapThang * HSLCV;
			LuongSP_LamRa_TheoPCap = b12;
			spLamRa = b11 + b12;
		}

		public static void TinhBoiDuongQuaDemA512(int NgayQuaDem, double boiduongca3, out double boiduong) {
			boiduong = (NgayQuaDem * boiduongca3);
		}

		public static void TinhLuongCoBan_CongVaPC_A202(cUserInfo nv, double luongtoithieu, out double luongcb1nv) {
			double a21_1 = (luongtoithieu * nv.HeSo.LuongCB) / 26d;
			double a21_2 = a21_1 * (nv.Tong.CongThang + nv.Tong.Phep + nv.Tong.H_CT_PT + nv.Tong.Le);
			double a21_3 = a21_1 * (nv.Tong.CongCV);
			nv.Luong.LCB_TheoCongVaCV = a21_2 + a21_3;
			double a21_4 = a21_1 * (nv.Tong.PCapThang);
			nv.Luong.LCB_TheoPCap = a21_4;
			double kq = a21_2 + a21_3 + a21_4;
			luongcb1nv = kq;
		}
		public static void TinhLuongCoBan_CongVaPC_A202(double HSLCB, double luongtoithieu,
			double CongThang, double PCapThang, Double Phep, double H_CT_PT, double Le, double CongCV,
			out double LCB_TheoCongVaCV, out double LCB_TheoPCap, out double luongcb1nv) {
			double a21_1 = (luongtoithieu * HSLCB) / 26d;
			double a21_2 = a21_1 * (CongThang + Phep + H_CT_PT + Le);
			double a21_3 = a21_1 * (CongCV);
			LCB_TheoCongVaCV = a21_2 + a21_3;
			double a21_4 = a21_1 * (PCapThang);
			LCB_TheoPCap = a21_4;
			double kq = a21_2 + a21_3 + a21_4;
			luongcb1nv = kq;
		}



		public static void TinhKhauTruBHChoNV(List<cUserInfo> dsnv, Double mucluongtoithieu) {
			foreach (var nhanvien in dsnv) {
				nhanvien.Luong.KhauTruBH = nhanvien.HeSo.BHXH_YT_TN * mucluongtoithieu * (nhanvien.Luong.MucDongBHXH / 100d);
			}
		}
		public static void DocLuongDieuChinh(DateTime thang, List<cUserInfo> dsnv, out double dTongLuongDieuChinh, out double dThuChiKhac) {
			var dieuchinhluong = SqlDataAccessHelper.ExecuteQueryString("select * from DieuChinhLuongThangTruoc where Thang=@Thang and Nam=@Nam order by UserEnrollNumber"
				, new string[] { "@Thang", "@Nam" }, new object[] { thang.Month, thang.Year });
			if (dieuchinhluong.Rows.Count == 0) {
				dTongLuongDieuChinh = 0d;
				dThuChiKhac = 0d;
				return;
			}
			dTongLuongDieuChinh = 0d;
			dThuChiKhac = 0d;
			
			foreach (DataRow row in dieuchinhluong.Rows) {
				var iUserEnrollNumber = (int)row["UserEnrollNumber"];
				var nv = dsnv.Find(o => o.MaCC == iUserEnrollNumber);
				if (nv == null) continue;
				var luong = (row["LuongDieuChinh"] != DBNull.Value) ? Convert.ToDouble(row["LuongDieuChinh"]) : 0d;
				var tamung = (row["TamUng"] != DBNull.Value) ? Convert.ToDouble(row["TamUng"]) : 0d;
				var thuchikhac = (row["ThuChiKhac"] != DBNull.Value) ? Convert.ToDouble(row["ThuChiKhac"]) : 0d;
				var mucdongbh = (row["MucDongBHXH"] != DBNull.Value) ? Convert.ToSingle(row["MucDongBHXH"]) : 0f;
				nv.Luong.LuongThangTruoc = luong;
				nv.Luong.ThuChiKhac = thuchikhac;
				nv.Luong.TamUng = tamung;
				nv.Luong.MucDongBHXH = mucdongbh;
				dTongLuongDieuChinh += luong;
				dThuChiKhac += dThuChiKhac;
			}
		}


		#endregion

		#region format cell in excel

		public static void FormatCells(ExcelRange cell, object value, bool wraptext = false, bool merge = false,
									   bool bold = false, bool italic = false,
									   int size = 12, bool VeBorder = true, int color=0,
									   ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin,
									   ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Center,
									   ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center,
									   string numberFormat = null, int textRotation = 0) {
			cell.Value = value;
			cell.Style.WrapText = wraptext;
			cell.Style.Font.Size = size;
			if (merge) cell.Merge = true;
			if (numberFormat != null) cell.Style.Numberformat.Format = numberFormat;
			if (textRotation != 0) cell.Style.TextRotation = textRotation;
			if (bold) cell.Style.Font.Bold = true;
			if (italic) cell.Style.Font.Italic = true;
			cell.Style.HorizontalAlignment = hAlign;
			cell.Style.VerticalAlignment = vAlign;
			if (VeBorder) cell.Style.Border.BorderAround(viendamnhat, Color.Black);
			switch(color)
			{
				case 7:
					cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
					cell.Style.Fill.BackgroundColor.SetColor(Color.DarkGray); 
					break;
				case 8: 
					cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
					cell.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
					break;					
			}
		}
		public static void FormatOneCell(ExcelWorksheet ws, ref int row, ref int col, object value, bool wraptext = false, bool merge = false,
									   int increaseCol = 0, bool bold = false, bool Italic = false,
									   int size = 12, bool VeBorder = true,
									   ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin,
									   ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Center,
									   ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center,
									   string numberFormat = null, int textRotation = 0) {
			ws.Cells[row, col].Value = value;
			ws.Cells[row, col].Style.WrapText = wraptext;
			ws.Cells[row, col].Style.Font.Size = size;
			if (merge) ws.Cells[row, col].Merge = true;
			if (numberFormat != null) ws.Cells[row, col].Style.Numberformat.Format = numberFormat;
			if (textRotation != 0) ws.Cells[row, col].Style.TextRotation = textRotation;
			if (bold) ws.Cells[row, col].Style.Font.Bold = true;
			if (Italic) ws.Cells[row, col].Style.Font.Italic = true;
			ws.Cells[row, col].Style.HorizontalAlignment = hAlign;
			ws.Cells[row, col].Style.VerticalAlignment = vAlign;
			if (VeBorder) ws.Cells[row, col].Style.Border.BorderAround(viendamnhat, Color.Black);
			if (increaseCol > 0) col += increaseCol;
		}
	
		public static void FormatCell(ExcelWorksheet ws, ref int row, ref int col, object value, 
			bool wraptext = false, bool merge = false,
			int increaseRow = 0, int increaseCol = 0, bool Bold = false, bool Italic = false,
			int size = 12, bool VeBorder = true,
			ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin,
			ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Center,
			ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center,
			string numberFormat = null) {
			ws.Cells[row, col].Value = value;
			ws.Cells[row, col].Style.WrapText = wraptext;
			ws.Cells[row, col].Style.Font.Size = size;
			ws.Cells[row, col].Merge = merge;
			if (numberFormat != null) ws.Cells[row, col].Style.Numberformat.Format = numberFormat;
			if (Bold) ws.Cells[row, col].Style.Font.Bold = true;
			if (Italic) ws.Cells[row, col].Style.Font.Italic = true;
			ws.Cells[row, col].Style.HorizontalAlignment = hAlign;
			ws.Cells[row, col].Style.VerticalAlignment = vAlign;
			if (VeBorder) ws.Cells[row, col].Style.Border.BorderAround(viendamnhat, Color.Black);
			if (increaseCol > 0) col += increaseCol;
			if (increaseRow > 0) row += increaseRow;
		}

		public static void FCong(ExcelRange cell1, ExcelRange cell2, ExcelRange cell3, ExcelRange cell123,
								 object value1, object value2, object value3,
								 ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thick,
								 ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center,
								 ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Right,
								 string numberFormat = "#0.0#") {
			cell1.Value = value1;
			cell2.Value = value2;
			cell3.Value = value3;
			cell1.Style.Numberformat.Format = numberFormat;
			cell2.Style.Numberformat.Format = numberFormat;
			cell1.Style.HorizontalAlignment = hAlign;
			cell1.Style.VerticalAlignment = vAlign;
			cell2.Style.Border.BorderAround(ExcelBorderStyle.Dotted);
			cell123.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

		}

		public static void FormatCells3(ExcelRange cell, object value, bool wrap = false, bool Merge = false,
							   bool Bold = false, bool Italic = false,
							   int size = 12, bool VeBorder = true,
							   ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin,
							   ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Center,
							   ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center,
							   string numberFormat = null, int textRotation = 0) {
			cell.Value = value;
			cell.Style.WrapText = wrap;
			cell.Style.Font.Size = size;
			cell.Merge = Merge;
			if (numberFormat != null) cell.Style.Numberformat.Format = numberFormat;
			cell.Style.Font.Bold = Bold;
			cell.Style.Font.Italic = Italic;
			cell.Style.HorizontalAlignment = hAlign;
			cell.Style.VerticalAlignment = vAlign;
			if (VeBorder) cell.Style.Border.BorderAround(viendamnhat, Color.Black);
		}
		public static void FormatCellsTitle3(ExcelRange cell, object value, bool wrap = false, bool Merge = false,
							   bool Bold = true, bool Italic = false,
							   int size = 12, bool VeBorder = true,
							   ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin,
							   ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Center,
							   ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center,
							   string numberFormat = null, int textRotation = 0) {
			cell.Value = value;
			cell.Style.WrapText = wrap;
			cell.Style.Font.Size = size;
			cell.Merge = Merge;
			if (numberFormat != null) cell.Style.Numberformat.Format = numberFormat;
			cell.Style.Font.Bold = Bold;
			cell.Style.Font.Italic = Italic;
			cell.Style.HorizontalAlignment = hAlign;
			cell.Style.VerticalAlignment = vAlign;
			if (VeBorder) cell.Style.Border.BorderAround(viendamnhat, Color.Black);
		}


		#endregion

		//---------------------------------------------------------------------------------------------------------------------------------------------------------



		public static void KiemtraTontaiGioQuaDem(cNgayCong ngayCongGoc, DateTime giovao) {
			// kiểm tra tồn tại giờ ra ko vào ko?
			var kq = ngayCongGoc.DSVaoRa.Any(item => item.HaveINOUT == -2 && giovao < item.Raa.Time);
		}


		public static void SapXepDS_Check(List<cChk>[] list) {
			foreach (List<cChk> listCheck in list) {
				listCheck.Sort(new cChkComparer());
			}
		}

		public static void CheckTinhPC50(cUserInfo nhanvien, DateTime ngay, bool giatri) {
			var n1 = DAL.CheckTinhPC50(nhanvien.MaCC, ngay, giatri);
			var ngayXN_PCTC = nhanvien.DSXNPhuCap50.Find(o => o.Ngay == ngay);
			if (ngayXN_PCTC == null) { // chưa có --> tạo mới
				ngayXN_PCTC = new cTemp1 { Ngay = ngay, UserEnrollNumber = nhanvien.MaCC, TinhPC50 = giatri };
				nhanvien.DSXNPhuCap50.Add(ngayXN_PCTC);
			}
			else { // đã có --> cập nhật
				ngayXN_PCTC.TinhPC50 = giatri;
			}
		}
		public static void TinhPCDB(cUserInfo nhanvien, DateTime ngay, int loai, int pcBanNgay, int pcBanDem) {
			var n1 = DAL.UpdateOrInsert_TinhPCDB(nhanvien.MaCC, ngay, loai, pcBanNgay, pcBanDem);
			var ngayTinhPCDB = nhanvien.DSXNPhuCapDB.Find(o => o.Ngay == ngay);
			var ngayCong = nhanvien.DSNgayCong.Find(o => o.Ngay == ngay);
			if (ngayTinhPCDB == null) {
				ngayTinhPCDB = new cTemp { LoaiPC = loai, PCNgay = pcBanNgay, PCDem = pcBanDem, UserEnrollNumber = nhanvien.MaCC, Ngay = ngay, Duyet = true };
				nhanvien.DSXNPhuCapDB.Add(ngayTinhPCDB);
			}
			else {
				ngayTinhPCDB.LoaiPC = loai;
				ngayTinhPCDB.PCNgay = pcBanNgay;
				ngayTinhPCDB.PCDem = pcBanDem;
			}
			ngayCong.TinhPCDB = true;
			ngayCong.LoaiPCDB = loai;
			TinhPCDB(ngayCong.TG.GioLamTrongNgay, ngayCong.TG.LamDemTrongNgay, ngayCong.QuaDem, ngayCong.TinhPCDB, ngayCong.LoaiPCDB, ngayTinhPCDB.PCNgay, ngayTinhPCDB.PCDem,
				out ngayCong.TG.Tinh200, out ngayCong.TG.Tinh260, out ngayCong.TG.Tinh300, out ngayCong.TG.Tinh390, out ngayCong.TG.TinhPCCus,
				out ngayCong.PhuCap100, out ngayCong.PhuCap160, out ngayCong.PhuCap200, out ngayCong.PhuCap290, out ngayCong.PhuCapCus, ref ngayCong.TongPhuCap);

		}

		public static void HuyBo_TinhPCDB(cUserInfo nhanvien, DateTime ngay) {
			var n1 = DAL.DeleteTinhPCDB(nhanvien.MaCC, ngay);
			var ngayTinhPCDB = nhanvien.DSXNPhuCapDB.Find(o => o.Ngay == ngay);
			if (ngayTinhPCDB != null) {
				nhanvien.DSXNPhuCapDB.Remove(ngayTinhPCDB);
			}
			var ngayCong = nhanvien.DSNgayCong.Find(o => o.Ngay == ngay);
			ngayCong.TinhPCDB = false;
			ngayCong.LoaiPCDB = 0;
			ngayCong.PhuCap100 = 0d;
			ngayCong.PhuCap160 = 0d;
			ngayCong.PhuCap200 = 0d;
			ngayCong.PhuCap290 = 0d;
			ngayCong.PhuCapCus = 0d;
			ngayCong.TongPhuCap = 0d;
			ngayCong.TG.Tinh150 = TimeSpan.Zero;
			ngayCong.TG.Tinh200 = TimeSpan.Zero;
			ngayCong.TG.Tinh260 = TimeSpan.Zero;
			ngayCong.TG.Tinh300 = TimeSpan.Zero;
			ngayCong.TG.Tinh390 = TimeSpan.Zero;
			ngayCong.TG.TinhPCCus = TimeSpan.Zero;

			var ngayTinhPCTC = nhanvien.DSXNPhuCap50.Find(o => o.Ngay == ngay);// lỗi null ngayTinhPCTC
			if (ngayTinhPCTC != null)
				ngayCong.TinhPC50 = ngayTinhPCTC.TinhPC50;// lỗi null ngayTinhPCTC
			TinhPCDem_PCTC(ngayCong.TG.GioLamTrongNgay, ngayCong.TG.LamDemTrongNgay, ngayCong.TinhPC50, ngayCong.QuaDem, ngayCong.TG.LamThemTrongNgay,
				out ngayCong.TG.Tinh130, out ngayCong.TG.Tinh150, out ngayCong.TG.TinhTCC3,
				out ngayCong.PhuCap30, out ngayCong.PhuCap50, out ngayCong.PhuCapTCC3, out ngayCong.TongPhuCap);
			//TinhLaiPhuCapTC(nhanvien.DSXNPhuCap50, nhanvien.DSNgayCong);
			TinhPCDB(ngayCong.TG.GioLamTrongNgay, ngayCong.TG.LamDemTrongNgay, ngayCong.QuaDem, ngayCong.TinhPCDB, ngayCong.LoaiPCDB, 0f, 0f,
				out ngayCong.TG.Tinh200, out ngayCong.TG.Tinh260, out ngayCong.TG.Tinh300, out ngayCong.TG.Tinh390, out ngayCong.TG.TinhPCCus,
				out ngayCong.PhuCap100, out ngayCong.PhuCap160, out ngayCong.PhuCap200, out ngayCong.PhuCap290, out ngayCong.PhuCapCus, ref ngayCong.TongPhuCap);

			//TinhLaiPhuCapDB(nhanvien.DSXNPhuCapDB, nhanvien.DSNgayCong);
		}


		public static void CapNhatLuongDieuChinh_TamUng_ThuChiKhac(DataTable table, DateTime ngaydauthang) {
			foreach (DataRow row in table.Rows) {
				var userfullcode = row["UserFullCode"].ToString();
				var s1 = userfullcode.Replace(" ", string.Empty);
				s1 = s1.Replace("K", "9");
				int UserEnrollNumber;
				if (int.TryParse(s1, out UserEnrollNumber) == false) continue;

				var thang = ngaydauthang.Month;
				var nam = ngaydauthang.Year;
				var luongdieuchinh = (double)row["LuongDieuChinh"];
				var tamung = (double)row["TamUng"];
				var thuchikhac = (double)row["ThuChiKhac"];
				var mucdongbhxh = (Single) row["MucDongBHXH"];
				var kq = DAL.CapNhatLuongDieuChinh_TamUng_ThuChiKhac(UserEnrollNumber, thang, nam, luongdieuchinh, tamung, thuchikhac, mucdongbhxh);
			}
		}

		public static void DocSetting() {
			var table = SqlDataAccessHelper.ExecuteQueryString("select * from Setting", null, null);
			for (int i = 0; i < table.Rows.Count; i++) {
				var row = table.Rows[i];
				var id = (int)row["ID"];
				var code = row["Code"].ToString();
				var value = row["Value"].ToString();
				switch (code) {
					#region phụ cấp

					case "PC30":
						var val30 = 0;
						if (int.TryParse(value, out val30)) {
							XL2.PC30 = val30;
						}
						break;
					case "PC50":
						var val50 = 0;
						if (int.TryParse(value, out val50)) {
							XL2.PC50 = val50;
						}
						break;
					case "PCTCC3":
						var valTCC3 = 0;
						if (int.TryParse(value, out valTCC3)) {
							XL2.PCTCC3 = valTCC3;
						}
						break;
					case "PC100":
						var val100 = 0;
						if (int.TryParse(value, out val100)) {
							XL2.PC100 = val100;
						}
						break;
					case "PC160":
						var val160 = 0;
						if (int.TryParse(value, out val160)) {
							XL2.PC160 = val160;
						}
						break;
					case "PC200":
						var val200 = 0;
						if (int.TryParse(value, out val200)) {
							XL2.PC200 = val200;
						}
						break;
					case "PC290":
						var val290 = 0;
						if (int.TryParse(value, out val290)) {
							XL2.PC290 = val290;
						}
						break;

					#endregion
					case "TGLamDemToiThieu":
						var valTGLamDemToiThieu = new TimeSpan(0, 0, 0);
						if (TimeSpan.TryParse(value, out valTGLamDemToiThieu)) {
							XL2.TGLamDemToiThieu = valTGLamDemToiThieu;
						}
						break;

					#region số phút cho phép trễ sớm afterot ca tự do

					case "ChoPhepTre":
						var valChoPhepTre = 0;
						if (int.TryParse(value, out valChoPhepTre)) {
							XL2.ChoPhepTre = new TimeSpan(0, valChoPhepTre, 0);
						}
						break;
					case "ChoPhepSom":
						var valChoPhepSom = 0;
						if (int.TryParse(value, out valChoPhepSom)) {
							XL2.ChoPhepSom = new TimeSpan(0, valChoPhepSom, 0);
						}
						break;
					case "LamThemAfterOT":
						var valLamThemAfterOT = 0;
						if (int.TryParse(value, out valLamThemAfterOT)) {
							XL2.LamThemAfterOT = new TimeSpan(0, valLamThemAfterOT, 0);
						}
						break;

					#endregion
				}
			}

		}

		public static DateTime DocThangKetCong() {
			var query = " select Max(Thang) from ThangKetCong where Khoa = 1 ";
			var table = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			if (table.Rows.Count == 0 || table.Rows[0][0] == DBNull.Value) return DateTime.MinValue;
			return (DateTime)table.Rows[0][0];
		}

		internal static void LayBang_PH_Them(List<cUserInfo> listNV, DateTime ngayBD, DateTime ngayKT, ref DataTable Bang_PH_Them) {

			var arrMaCC = (from nv in listNV select nv.MaCC).ToArray();
			Bang_PH_Them = DAL.LayBang_PH_Them(arrMaCC, ngayBD, ngayKT, Bang_PH_Them);
		}

		internal static void LayBang_PH_Xoaa(List<cUserInfo> listNV, DateTime ngayBD, DateTime ngayKT, ref DataTable m_Bang_PH_Xoaa) {
			var arrMaCC = (from nv in listNV select nv.MaCC).ToArray();
			m_Bang_PH_Xoaa = DAL.LayBang_PH_Xoa(arrMaCC, ngayBD, ngayKT, m_Bang_PH_Xoaa);
		}

		internal static void LayBang_PH_GioGoc(List<cUserInfo> listNV, DateTime ngayBD, DateTime ngayKT, ref DataTable m_Bang_PH_GioGoc) {
			var arrMaCC = (from nv in listNV select nv.MaCC).ToArray();
			m_Bang_PH_GioGoc = DAL.LayBang_PH_GioGoc(arrMaCC, ngayBD, ngayKT, m_Bang_PH_GioGoc);
		}

		internal static void LayBang_GioDaXN(List<cUserInfo> listNV, DateTime ngayBD, DateTime ngayKT, ref DataTable m_Bang_GioDaXN) {
			m_Bang_GioDaXN.Rows.Clear();
			var arrMaCC = (from nv in listNV select nv.MaCC).ToArray();
			var table01 = DAL.LayBang_GioDaXN2(arrMaCC, ngayBD, ngayKT, m_Bang_GioDaXN);
			if (table01.Rows.Count == 0) { return; }
			var arrID = (from row in table01.Rows.Cast<DataRow>() let id = (int)row["IDXacNhanCaVaLamThem"] select id).ToArray();
			var table02 = DAL.LayBang_GioDaXN3(arrID, m_Bang_GioDaXN);
			if (table02.Rows.Count == 0) { return; }
			foreach (DataRow rowInn in table01.Rows) {
				var UserFullName = rowInn["UserFullName"].ToString();
				var UserFullCode = rowInn["UserFullCode"].ToString();
				var UserEnrollNumber = (int)rowInn["UserEnrollNumber"];
				var TimeStrInn = (DateTime)rowInn["TimeStr"];
				var ID = (int)rowInn["IDXacNhanCaVaLamThem"];
				var arrRowOut = table02.Select("IDXacNhanCaVaLamThem=" + ID);
				if (arrRowOut.Length == 0) continue;
				var rowOut = arrRowOut[0];
				var TimeStrOut = (DateTime)rowOut["TimeStr"];
				var ShiftCode = rowOut["ShiftCode"].ToString();
				var ShiftID = (int)rowOut["ShiftID"];
				var OTMin = (int)rowOut["OTMin"];
				DataRow row = m_Bang_GioDaXN.NewRow();
				row["UserFullName"] = UserFullName;
				row["UserFullCode"] = UserFullCode;
				row["UserEnrollNumber"] = UserEnrollNumber;
				row["IDXacNhanCaVaLamThem"] = ID;
				row["TimeStrInn"] = TimeStrInn;
				row["TimeStrOut"] = TimeStrOut;
				row["ShiftCode"] = ShiftCode;
				row["ShiftID"] = ShiftID;
				row["OTMin"] = OTMin;
				m_Bang_GioDaXN.Rows.Add(row);
			}
		}

		internal static void HuyXN_GioChamCong(int ID) {
			DAL.HuyXN_GioChamCong(ID);
		}

	}

}
