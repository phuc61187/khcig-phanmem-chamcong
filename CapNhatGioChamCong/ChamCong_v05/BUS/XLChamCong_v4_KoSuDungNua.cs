using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ChamCong_v05.DAL;
using ChamCong_v05.DTO;
using log4net.Config;

namespace ChamCong_v05.BUS {
	public static partial class XL {
		public static void Tinh_PCTC5(bool TinhPC50, bool QuaDem, TimeSpan SoGioLamDemmm, TimeSpan SoGioLamThem,
	TimeSpan tgTinh130, TimeSpan tgTinh150, TimeSpan tgTinhTCC3,
	out float PhuCap30, out float PhuCapTC, out float PhuCapTCC3, out float TongPhuCap) {
			var heso_pctc = Convert.ToSingle(XL2.PC50) / 100f;
			var heso_pcdem = Convert.ToSingle(XL2.PC30) / 100f; //tbd
			var heso_pctcc3 = (Convert.ToSingle(XL2.PCTCC3)) / 100f;

			PhuCap30 = 0f;
			PhuCapTC = 0f;
			PhuCapTCC3 = 0f;
			TongPhuCap = 0f;
			if (QuaDem == false) {// ko qua đêm, tính pctc 50 % bình thường
				if (TinhPC50 && (SoGioLamThem > XL2._01phut)) {
					PhuCapTC = Convert.ToSingle(Math.Round((SoGioLamThem.TotalHours / 8d) * heso_pctc, 2, MidpointRounding.ToEven));
					TongPhuCap = PhuCapTC;
				}
			}
			else {// có thể có qua đêm, có thể tính pctcc3 100%
				if (TinhPC50 == false || (SoGioLamThem <= XL2._01phut)) {  // ko có tăng cường thì chỉ tính pc đêm
					PhuCap30 = (Convert.ToSingle(SoGioLamDemmm.TotalHours / 8d)) * heso_pcdem;
					TongPhuCap = PhuCap30;
				}
				else {
					PhuCapTCC3 = Convert.ToSingle(Math.Round((tgTinhTCC3.TotalHours / 8d) * heso_pctcc3, 2, MidpointRounding.ToEven));
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

			tgTinh200 = tgTinh260 = tgTinh300 = tgTinh390 = tgTinhCus = TimeSpan.Zero;
			PhuCap100 = 0f;
			PhuCap160 = 0f;
			PhuCap200 = 0f;
			PhuCap290 = 0f;
			PhuCapCus = 0f;
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
						tgTinhCus = Tinh_TGLamTangCuong(SoGioTinhCong);//((SoGioTinhCong - XL2._08gio > XL2._01phut) ? (SoGioTinhCong - XL2._08gio) : TimeSpan.Zero);
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


		public static void XemCong_v08(List<cUserInfo> dsnv, DateTime ngayBD_Bef2D, DateTime ngayKT_Aft2D) {
/*
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
				XetCa_ListCIO_A3_5(nv.DS_CIO_A, nv.LichTrinhLV, ds_raa3_vao1, nv.DS_Check_A);// xét thuộc ThuocCa, update thuộc ngày công, tách ThuocCa 3&1 nếu có, rồi tính công//nv.MacDinhTinhPC50, //[140615_4]
				XetCa_ListCIO_V4_5(nv.DS_CIO_V);
				TronDS_CIO_A_V5(nv.DS_CIO_A, nv.DS_CIO_V, out nv.DSVaoRa);
				PhanPhoi_DSVaoRa6(nv.DSVaoRa, nv.DSNgayCong);
				PhanPhoi_DSVang7(nv.DSVang, nv.DSNgayCong);
				TinhCong_ListNgayCong8_5(nv.DSNgayCong);//ver 4.0.0.4
				//TinhPhuCap_ListNgayCong9_5(nv.DSNgayCong, nv.)
				TinhPCTC_TrongListXNPCTC9(nv.DSXNPhuCap50, nv.DSNgayCong);
				TinhPCDB_TrongListXNPCDB10(nv.DSXNPhuCapDB, nv.DSNgayCong);
			}
			if (DS_Check_KoHopLe_AllNV.Count > 0) DAO5.LoaiGioLienQuan(DS_Check_KoHopLe_AllNV);
			if (ds_raa3_vao1.Count > 0) DAO5.ThemGio_ra3_vao1(ds_raa3_vao1);
			#endregion
*/
		}

		public static void LoadDSXPVang_Le(int tempMaCC, DataTable tableVang, DataTable tableNgayLe, List<cLoaiVang> dsVangs) { }
		public static void LoadDSXNPC50(int tempMaCC, DataTable tableXN_PCTC, List<structPCTC> dsXacNhanPC) { }
		public static void LoadDSXNPCDB(int tempMaCC, DataTable tableXN_PCDB, List<structPCDB> dsXacNhanPC) { }
		public static void TinhPCTC_TrongListXNPCTC9(List<structPCTC> dsXacNhanPC, List<cNgayCong> dsNgayCong) {
			foreach (var item in dsXacNhanPC) {
				var ngayCong = dsNgayCong.Find(o => o.Ngay == item.Ngay);
				TinhPCTC_CuaNgay(ngayCong, item.TinhPC50);
			}
		}

		public static void TinhPCTC_CuaNgay(cNgayCong ngayCong, List<structPCTC> DSXNPhuCap50) {
			var IndexngayTinhLaiPCTC1 = DSXNPhuCap50.FindIndex(item => item.Ngay == ngayCong.Ngay);
			var kq1 = (IndexngayTinhLaiPCTC1 >= 0) && (DSXNPhuCap50[IndexngayTinhLaiPCTC1].TinhPC50);
			TinhPCTC_CuaNgay(ngayCong, kq1);
		}

		public static void TinhPCTC_CuaNgay(cNgayCong ngayCong, bool choPhepTinhTC) {
			ngayCong.TinhPC50 = choPhepTinhTC;
			Tinh_PCTC5(choPhepTinhTC, ngayCong.QuaDem, ngayCong.TG5.TongGioLamDem, ngayCong.TG5.TongGioTangCuong,
							   ngayCong.TG5.HuongPC_Dem, ngayCong.TG5.HuongPC_TangCuongNgay, ngayCong.TG5.HuongPC_TangCuongDem,
							   out ngayCong.PhuCaps._30_dem, out ngayCong.PhuCaps._50_TC, out ngayCong.PhuCaps._100_TCC3, out ngayCong.PhuCaps._TongPC);
		}

		public static void TinhPCDB_TrongListXNPCDB10(List<structPCDB> dsXacNhanPC, List<cNgayCong> dsNgayCong) {
			foreach (var item in dsXacNhanPC) {
				var ngayCong = dsNgayCong.Find(o => o.Ngay == item.Ngay);

				ngayCong.TinhPCDB = true;
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

	}
}
