﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ChamCong_v05.DAL;
using ChamCong_v05.DTO;
using ChamCong_v05.Helper;
using log4net.Config;
using mySetting = ChamCong_v05.Properties.Settings;

namespace ChamCong_v05.BUS
{
	//new function for ver 4.0.0.5
	public static partial class XL
	{
		public static void GetLichTrinhNV(cUserInfo nhanvien, int? schID) {
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
				nhanvien.LichTrinhLV = XL.DSLichTrinh.FirstOrDefault(o => o.SchID == schID);
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

		}

		public static void TaoCaTuDo(DateTime CheckInTime, int LoaiCaTuDo, out cCa Ca)
		{
			//var temp = CheckInTime.TimeOfDay;//ver 4.0.0.0//tbd xem lại ngày công
			Ca = new cCa { ID = LoaiCaTuDo, };
			var temp = new TimeSpan(CheckInTime.TimeOfDay.Hours, CheckInTime.TimeOfDay.Minutes, 0);//ver 4.0.0.1 bỏ phần giây, chỉ giữ phần giờ, phút
			if (CheckInTime.TimeOfDay < XL2._03gio) temp = Ca.Duty.Onn.Add(XL2._1ngay); //ca 3 , ca 3 va 1 vẫn giữ nguyên vì 21h > 4h//tbd xem lại ngày công
			if (LoaiCaTuDo == int.MinValue + 0)
			{
				Ca.Duty = new TS { Onn = temp, Off = temp.Add(XL2._08gio) };
				Ca.WorkingTimeTS = XL2._08gio;
				Ca.Workingday = 1f;
				Ca.Code = mySetting.Default.shiftCodeCa8h;
				Ca.MoTa = string.Format(mySetting.Default.MoTaCaTuDo, 8);
				Ca.KyHieuCC = mySetting.Default.kyHieuCCCa8h;
			}
			else if (LoaiCaTuDo == int.MinValue + 1)
			{
				Ca.Duty = new TS { Onn = temp, Off = temp.Add(XL2._12gio) };
				Ca.WorkingTimeTS = XL2._12gio;
				Ca.Workingday = 1.5f;
				Ca.Code = mySetting.Default.shiftCodeCa12h;
				Ca.MoTa = string.Format(mySetting.Default.MoTaCaTuDo, 12);
				Ca.KyHieuCC = mySetting.Default.kyHieuCCCa12h;
			}
			else if (LoaiCaTuDo == int.MinValue + 2)
			{
				Ca.Duty = new TS { Onn = temp, Off = temp.Add(XL2._04gio) };
				Ca.WorkingTimeTS = XL2._04gio;
				Ca.Workingday = 0.5f;
				Ca.Code = mySetting.Default.shiftCodeCa4h;
				Ca.MoTa = string.Format(mySetting.Default.MoTaCaTuDo, 4);
				Ca.KyHieuCC = mySetting.Default.kyHieuCCCa4h;
			}
			else if (LoaiCaTuDo == int.MinValue + 3)
			{
				Ca.Duty = new TS { Onn = temp, Off = temp.Add(XL2._16gio) };
				Ca.WorkingTimeTS = XL2._16gio;
				Ca.Workingday = 2f;
				Ca.Code = mySetting.Default.shiftCodeCa16h;
				Ca.MoTa = string.Format(mySetting.Default.MoTaCaTuDo, 16);
				Ca.KyHieuCC = mySetting.Default.kyHieuCCCa16h;
			}
		}

		public static void XemCong_v08_2(List<cUserInfo> dsnv, DateTime ngayBD_Bef2D, DateTime ngayKT_Aft2D) {
			if (dsnv.Count == 0) return;
			#region nạp dữ liệu từ database
			var Arr_MaCC = (from nv in dsnv select nv.MaCC).ToList(); // tạo mảng danh sách mã nv chấm công
			DataTable tableArrayMaCC = MyUtility.Array_To_DataTable("tableArrayMaCC", Arr_MaCC);
			var tableCheck_A = DAO5.LayTableCIO_A5(tableArrayMaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableCheck_V = DAO5.LayTableCIO_V5(tableArrayMaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableXPVang = DAO5.LayTableXPVang5(tableArrayMaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableXN_PCTC = DAO5.LayTableXacNhanPC50_5(tableArrayMaCC, ngayBD_Bef2D, ngayKT_Aft2D);
			var tableXN_PCDB = DAO5.LayTableXacNhanPCDB_5(tableArrayMaCC, ngayBD_Bef2D, ngayKT_Aft2D, Duyet: true);
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
			//var DS_Check_KoHopLe_AllNV = new List<cCheck>();
			//var ds_raa3_vao1 = new List<cCheck>();
			//foreach (var nv in dsnv) {
			//	var DS_Check_KoHopLe_1NV = new List<cCheck>();
			//	LoaiBoCheckKoHopLe1(nv.DS_Check_A, ref DS_Check_KoHopLe_1NV);// loại bỏ check cùng loại trong 30ph, IO 30 phút
			//	DS_Check_KoHopLe_AllNV.AddRange(DS_Check_KoHopLe_1NV);// add vào ds ko hợp lệ của nhiều nhân viên để xóa sau
			//	GhepCIO_A2(nv.DS_Check_A, nv.DS_CIO_A);
			//	XetCa_ListCIO_A3(nv.DS_CIO_A, nv.LichTrinhLV, ds_raa3_vao1, nv.DS_Check_A);// xét thuộc ca, update thuộc ngày công, tách ca 3&1 nếu có, rồi tính công//nv.MacDinhTinhPC50, //[140615_4]
			//	XetCa_ListCIO_V4_5(nv.DS_CIO_V, nv.LichTrinhLV);
			//	TronDS_CIO_A_V5(nv.DS_CIO_A, nv.DS_CIO_V, nv.DSVaoRa);
			//	PhanPhoi_DSVaoRa6(nv.DSVaoRa, nv.DSNgayCong);
			//	PhanPhoi_DSVang7(nv.DSVang, nv.DSNgayCong);
			//	TinhCong_ListNgayCong8(nv.DSNgayCong, nv.StartNT, nv.EndddNT);//ver 4.0.0.4
			//	TinhPCTC_TrongListXNPCTC9(nv.DSXNPhuCap50, nv.DSNgayCong);
			//	TinhPCDB_TrongListXNPCDB10(nv.DSXNPhuCapDB, nv.DSNgayCong);
			//}
			//if (DS_Check_KoHopLe_AllNV.Count > 0) DAO.LoaiGioLienQuan(DS_Check_KoHopLe_AllNV);
			//if (ds_raa3_vao1.Count > 0) DAO.ThemGio_ra3_vao1(ds_raa3_vao1);
			#endregion
		}

		public static bool KiemtraKetluongThang45(DateTime ngayDauThang) {
			var query = " select Thang from ThongSoKetLuongThang where Thang >= @NgayDauThang ";
			var table = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@NgayDauThang"}, new object[] { ngayDauThang });
			if (table.Rows.Count == 0) return false;
			return true;
		}
		
		public static void XemCongThangDaKetLuong45(DataTable tableArrMaCC, DateTime ngayDauThang) {
			/* Lưu ý: trong dsnv sẽ có các nhân viên mới vào làm, những ng này công sẽ = 0 và công cv cũng =0
			 * 1. đọc lên kết công ngày, kết công ca và lọc ra của từng nhân viên
			 */

		}

		public static void XemCongThangChuaKetLuong45(DataTable tableArrMaCC, DateTime ngayDauThang) {
			throw new NotImplementedException();
		}

		public static void XemCongThoiGianChuaKetLuong(List<cUserInfo> DSNV, DateTime ngayBD, DateTime ngayKT) {
			XmlConfigurator.Configure();
			if (DSNV.Count == 0) return;
			#region nạp dữ liệu từ database
			var Arr_MaCC = (from nv in DSNV select nv.MaCC).ToList(); // tạo mảng danh sách mã chấm công để viết chuỗi query : or.. or
			DataTable tableArrayMaCC = MyUtility.Array_To_DataTable("tableArrayMaCC", Arr_MaCC);
			var tableCheck_A = DAO5.LayTableCIO_A5(tableArrayMaCC, ngayBD, ngayKT);
			var tableCheck_V = DAO5.LayTableCIO_V5(tableArrayMaCC, ngayBD, ngayKT);
			var tableXPVang = DAO5.LayTableXPVang5(tableArrayMaCC, ngayBD, ngayKT);
			var tableXN_PCTC = DAO5.LayTableXacNhanPC50_5(tableArrayMaCC, ngayBD, ngayKT);
			var tableXN_PCDB = DAO5.LayTableXacNhanPCDB_5(tableArrayMaCC, ngayBD, ngayKT, Duyet: true);
			var tableNgayLe = DAO5.DocNgayLe(ngayBD, ngayKT);
			#endregion

			#region transfer dữ liệu sang object
			foreach (var nv in DSNV) {
				var tempMaCC = nv.MaCC;
				nv.NgayCongBD_Bef2D = ngayBD;
				nv.NgayCongKT_Aft2D = ngayKT;
				LoadDSCheck_A5(tempMaCC, tableCheck_A, out nv.DS_Check_A);
				LoadDSCIO_V5(tempMaCC, tableCheck_V, out nv.DS_CIO_V);
				LoadDSXPVang_Le(tempMaCC, tableXPVang, tableNgayLe, nv.DSVang);
				LoadDSXNPC50(tempMaCC, tableXN_PCTC, nv.DSXNPhuCap50);
				LoadDSXNPCDB(tempMaCC, tableXN_PCDB, nv.DSXNPhuCapDB);
				// khởi tạo danh sách ngày công
				KhoiTaoDSNgayCong(nv.DSNgayCong, ngayBD, ngayKT);

			}

			#endregion
			#region xử lý
			var DS_Check_KoHopLe_AllNV = new List<cCheck>();
			var ds_raa3_vao1 = new List<cCheck>();
			foreach (var nv in DSNV) {List<cCheck> DS_Check_KoHopLe_1NV;
				LoaiBoCheckKoHopLe15(nv.DS_Check_A, out DS_Check_KoHopLe_1NV);// loại bỏ check cùng loại trong 30ph, IO 30 phút
				DS_Check_KoHopLe_AllNV.AddRange(DS_Check_KoHopLe_1NV);// add vào ds ko hợp lệ của nhiều nhân viên để xóa sau
				GhepCIO_A2(nv.DS_Check_A, out nv.DS_CIO_A);
				XetCa_ListCIO_A3_5(nv.DS_CIO_A, nv.LichTrinhLV, ds_raa3_vao1, nv.DS_Check_A);// xét thuộc ca, update thuộc ngày công, tách ca 3&1 nếu có, rồi tính công//nv.MacDinhTinhPC50, //[140615_4]
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

	}
}