using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ChamCong_v05.DTO;
using ChamCong_v05.Helper;

namespace ChamCong_v05.BUS {
	public static partial class XL {

		public static int DemSoNgayNghiChunhat(DateTime ngayDauThang, bool demCN, bool demT7) {
			var ngayBD = ngayDauThang;
			var ngayKT = new DateTime(ngayDauThang.Year, ngayDauThang.Month, DateTime.DaysInMonth(ngayDauThang.Year, ngayDauThang.Month));
			int kq = 0;
			for (DateTime ngaydem = ngayBD; ngaydem.Date <= ngayKT.Date; ngaydem = ngaydem.AddDays(1d)) {
				if (demT7 && ngaydem.DayOfWeek == DayOfWeek.Saturday) kq++;
				if (demCN && ngaydem.DayOfWeek == DayOfWeek.Sunday) kq++;
			}
			return kq;
		}



		public static void TinhSPLamRa_CongVaPC_B102(float HSLCV, float congthang, float PCapThang, float phep, float H_CT_PT, float PTDT, float Le,
			out double SP_LamRa_TheoCong, out double SP_LamRa_TheoCheDoNghi, out double SP_LamRa_TheoPCap) { //DANGLAM PD
			SP_LamRa_TheoCong = HSLCV * congthang;// sản phẩm làm ra theo công// ko có chờ việc
			SP_LamRa_TheoCheDoNghi = HSLCV * (phep + H_CT_PT + Le); // công chờ việc ko có vì chờ việc ko làm ra sản phẩm
			SP_LamRa_TheoPCap =HSLCV * PCapThang;// sản phẩm hưởng thêm theo phụ cấp
		}

		public static void TinhBoiDuongQuaDemA512(int NgayQuaDem, int boiduongca3, out double boiduong) {
			boiduong = Convert.ToDouble(NgayQuaDem * boiduongca3);
		}

		public static void TinhLuongCoBan_CongVaPC_A202(float HSLCB, int luongtoithieu,
			float CongThang, float PCapThang, float Phep, float H_CT_PT, float PTDT, float Le, float CongCV,
			out double LCB_TheoCong, out double LCB_TheoCheDoNghi, out double LCB_TheoCongCV, out double LCB_TheoPCap) {
			double a21_1 = (luongtoithieu * HSLCB) / 26d; // đơn giá lương cơ bản của 1 ngày làm việc theo HSLCB

			LCB_TheoCong = a21_1 * CongThang; 
			LCB_TheoCheDoNghi = a21_1 * (Phep + H_CT_PT + Le);
			LCB_TheoCongCV =a21_1 * CongCV;

			//tbd tương lai : a21_3 = a21_1*donGiaNgayCongCV(130k);
			LCB_TheoPCap = a21_1 * PCapThang; // hưởng thêm lương(lương CB) theo phụ cấp tính
		}

		public static void LoadNgayLe(List<cNgayCong> dsNgayCong, DataTable tableNgayLe) {
			foreach (DataRow row in tableNgayLe.Rows.Cast<DataRow>()) {
				cLoaiVang vangLe = new cLoaiVang {
					MaLV_Code = "L",
					MoTa = "Lễ, tết",
					Ngay = (DateTime)row["HDate"],
					WorkingDay = 1f
				};
				cNgayCong ngayCong = dsNgayCong.Find(o => o.Ngay == vangLe.Ngay);
				ngayCong.DSVang.Add(vangLe);
			}
		}

		public static void LoadNgayCong(int macc, List<cNgayCong> dsNgayCong, DateTime indexNgay,
			DataTable tableKetcongNgay, DataTable tableKetcongCa) {
			cNgayCong ngayCong = new cNgayCong {
				DSVang = new List<cLoaiVang>(),
				DSVaoRa = new List<cCheckInOut>(),
				Ngay = indexNgay,
				PhuCaps = new PhuCap(),
				TG = new ThoiGian()
			};

			//load ket cong ngay
			foreach (DataRow row in tableKetcongNgay.Rows.Cast<DataRow>()
				.Where(o => ((int)o["UserEnrollNumber"]) == macc && ((DateTime)o["Ngay"]) == indexNgay)) {
				//v9 xem lai them cac truong khac cua ngaycong
				//1.isEdit, next, prev, trangthaidiemdanh ko su dung
				//info ko can loaipcdb, tinhpc50,tinhpcdb, vi đã tính rồi
				ngayCong.Ngay = (DateTime)row["Ngay"];
				ngayCong.TongCong = (float)row["TongCong"];
				ngayCong.TongNgayLV = (row["TongNgayLV"] != DBNull.Value) ? (float)row["TongNgayLV"] : 0f;//ver4.0.0.1
				ngayCong.PhuCaps = new PhuCap {
					_TongPC = (float)row["TongPC"],
					_30_dem = (float)row["PCDem"],
					_50_TC = (float)row["PCTangCuong"],
					_100_TCC3 = (float)row["PCTangCuong_Dem"],
					_100_LVNN_Ngay = (float)row["PC200"],
					_150_LVNN_Dem = (float)row["PC260"],
					_200_LeTet_Ngay = (float)row["PC300"],
					_250_LeTet_Dem = (float)row["PC390"],
					_Cus = (float)row["PCCus"]
				};
				ngayCong.QuaDem = (bool)row["IsOverNight"];
				ngayCong.TG = new ThoiGian {
					GioLamViec5 = (TimeSpan)row["TGLamViec"],
					LamTangCuong = (TimeSpan)row["TGLamThem"],
					LamBanDem = (TimeSpan)row["TGLamDem"],
					GioThuc5 = (TimeSpan)row["TGThuc"],//info  xem lai cac truong khac cua ngayCong.TG, vào trễ? ra sớm?--> do CIO lưu
				};
				ngayCong.DSVang = new List<cLoaiVang>(); //info danh sách vắng load ở hàm xử lý sau
				ngayCong.DSVaoRa = new List<cCheckInOut>();
				LoadDSVaoRa(macc, indexNgay, ngayCong.DSVaoRa, tableKetcongCa);
			}
			dsNgayCong.Add(ngayCong);

		}

		public static void LoadThongtinLamViecCongNhat(int MaCC, ref DateTime NgayBDCongnhat, ref DateTime NgayKTCongnhat, ref LoaiCongNhat NVCongnhat, List<cNgayCong> DSNgayCong, DataTable tableDSNVChiCongnhatThang) { //info cái này chỉ dành cho nhân viên chính thức chưa áp dụng cho nhân viên công nhật
			// tìm các nhân viên vừa làm công nhật vừa làm chính thức
			var rowsCongNhat = (from DataRow tempRow in tableDSNVChiCongnhatThang.Rows
								where (int)tempRow["UserEnrollNumber"] == MaCC//  && (tempRow["NVChinhThuc"] != DBNull.Value && (bool)tempRow["NVChinhThuc"] == true
								select tempRow).SingleOrDefault();
			// với các nhân viên này thì chỉ tính công khoảng thời gian làm công nhật, các khoảng khác phải loại bỏ
			if (rowsCongNhat != null) {
				NgayBDCongnhat = (DateTime)rowsCongNhat["NgayBatDau"];
				NgayKTCongnhat = (DateTime)rowsCongNhat["NgayKetThuc"];
				NVCongnhat =  ((bool)rowsCongNhat["NVChinhThuc"]) ? LoaiCongNhat.NVCongNhatVaChinhThuc : LoaiCongNhat.NVCongNhat;
				// loại bỏ ds vắng, ds ngày lễ trong khoảng thời gian này 
				DateTime ngayBDCongnhat = NgayBDCongnhat, ngayKTCongnhat = NgayKTCongnhat; // gán biến vì anonymousMethod ko chấp nhận ref, out
				foreach (var ngaycongtemp in DSNgayCong.Where(item => item.Ngay >= ngayBDCongnhat && item.Ngay <= ngayKTCongnhat)) {
					ngaycongtemp.DSVang.Clear();
					ngaycongtemp.PhuCaps = new PhuCap();
					ngaycongtemp.QuaDem = false;
				}
			}
			else
			{
				NgayBDCongnhat = DateTime.MinValue;
				NgayKTCongnhat = DateTime.MinValue;
				NVCongnhat = LoaiCongNhat.NVChinhThuc;
			}
		}

		public static void LoadDSXPVang(int macc, List<cNgayCong> dsNgayCong, DataTable tableXpVang) {
			foreach (DataRow row in tableXpVang.Rows.Cast<DataRow>()
				.Where(o => ((int)o["UserEnrollNumber"] == macc))
					) {
				cLoaiVang xpVang = new cLoaiVang {
					MaLV_Code = row["AbsentCode"].ToString(),
					MoTa = row["AbsentDescription"].ToString(),
					Ngay = (DateTime)row["TimeDate"],
					WorkingDay = (float)row["Workingday"],
				};
				cNgayCong ngayCong = dsNgayCong.Find(o => o.Ngay == xpVang.Ngay);
				if (ngayCong != null) ngayCong.DSVang.Add(xpVang);
			}
		}

		public static void LoadDSVaoRa(int macc, DateTime indexNgay, List<cCheckInOut> dsVaoRa, DataTable tableKetcongCa) {
			dsVaoRa.AddRange(from row in tableKetcongCa.Rows.Cast<DataRow>().Where(o => ((int)o["UserEnrollNumber"] == macc && (DateTime)o["Ngay"] == indexNgay))
							 //let dayCount = (row["DayCount"] != DBNull.Value && (bool)row["DayCount"]) ? 1d : 0d
							 let haveINOUT = (int)row["HaveINOUT"]
							 select new cCheckInOut {
								 Cong = row["Cong"] != DBNull.Value ? (float)row["Cong"] : 0f,
								 DaXN = row["CoXN"] != DBNull.Value && (bool)row["CoXN"],
								 DuyetChoPhepVaoTre = row["DuyetCPVaoTre"] != DBNull.Value && (bool)row["DuyetCPVaoTre"],
								 DuyetChoPhepRaSom = row["DuyetCPRaSom"] != DBNull.Value && (bool)row["DuyetCPRaSom"],
								 VaoTreTinhCV = row["VaoTreLaCV"] != DBNull.Value && (bool)row["VaoTreLaCV"],//ver 4.0.0.4	
								 RaaSomTinhCV = row["RaSomLaCV"] != DBNull.Value && (bool)row["RaSomLaCV"],//ver 4.0.0.4	
								 HaveINOUT = haveINOUT,
								 //ID = 0,//info id của xác nhận giờ, xem xét có lưu ko  suggest ko lưu
								 //IsEdited = 0,//info ko lưu
								 OTMin = row["OTMin"] != DBNull.Value ? (int)row["OTMin"] : 0,
								 QuaDem = row["QuaDem"] != DBNull.Value && (bool)row["QuaDem"], //info xem lại lúc update phần công nhật có update qua đêm này về 0 hay ko?, --> có

								 Vao = (row["TimeIn"] != DBNull.Value) ? new cCheck {//ID = 0,IsEdited = 0,MaCC = MachineNo = PhucHoi = Source = //info ko sử dụng
									 Time = (DateTime)row["TimeIn"], Type = "I",
								 } : null,
								 Raa = (row["TimeOut"] != DBNull.Value) ? new cCheck {//ID = 0,IsEdited = 0,MaCC = MachineNo = PhucHoi = Source = //info ko sử dụng
									 Time = (DateTime)row["TimeOut"], Type = "O",
								 } : null,
								 //ShiftID = (int)row["ShiftID"], //info shift id này của giờ đã xác nhận, ko sử dụng đến, sử dụng thuocCa như bên dưới

								 TD = (haveINOUT < 0) ? null : new ThoiDiem {
									 BD_LV = (DateTime)row["TD_BD_LV"],
									 KT_LV = (DateTime)row["TD_KT_LV_CoOT"], 
									 //BD_LV_Ca3 = KT_LV_Ca3 = KT_LV_ChuaOT = KT_LV_DaCoOT = info chưa có dưới csdl, xem xét có lưu ko--> ko lưu
								 },
								 TG = (haveINOUT < 0) ? null : new ThoiGian
									 {
										 GioThuc5 = (TimeSpan)row["TGThuc"],
										 GioLamViec5 = (TimeSpan)row["TGLamViec"],
										 LamBanDem = (TimeSpan)row["TGLamDem"],
										 LamTangCuong = (TimeSpan)row["TGLamThem"],
										 VaoTre = (TimeSpan)row["TGVaoTre"],
										 RaaSom = (TimeSpan)row["TGRaSom"],
									 },
								 ThuocCa = haveINOUT < 0 ? null : new cCa {
									 ID = (int)row["ShiftID"],
									 Code = row["ShiftCode"].ToString(),
									 KyHieuCC = row["KyHieuCC"].ToString()
								 },//v9 chưa làm, xem xét parse các trường thông tin từ shift param sang

								 ThuocNgayCong = (DateTime)row["Ngay"], // tương đương indexNgay
								 TimeDaiDien = ((int)row["HaveINOUT"] == 0 || (int)row["HaveINOUT"] == -1) ? (DateTime)row["TimeIn"] : (DateTime)row["TimeOut"]
								 //DSCa = new List<cCa>(),//info chưa có dưới csdl, xem xét có lưu ko? suggest bỏ
							 });
		}


		public static void ThongKeThang(ref ThongKeCong_PC thongKeThang, List<cNgayCong> dsNgayCong,
			DateTime ngayBDCongnhat, DateTime ngayKTCongnhat, LoaiCongNhat nvChinhThuc) {
			if (nvChinhThuc == LoaiCongNhat.NVCongNhat) // chỉ làm công nhật, chỉ tính công công nhật trong khoảng gian từ bắt đầu đến kết thúc
			{
				thongKeThang.Cong_Congnhat = (dsNgayCong
												.Where(item => item.Ngay >= ngayBDCongnhat && item.Ngay <= ngayKTCongnhat)
												.Sum(item => item.TongCong));
			}
			else //2 trường hợp, chính thức và vừa chính thức vừa công nhật
			{
				// trường hợp chỉ làm chính thức
				if (nvChinhThuc == LoaiCongNhat.NVChinhThuc)
					foreach (var ngayCong in dsNgayCong)
						XL.ThongKeNgay(ref thongKeThang, ngayCong);
				else // vừa làm công nhật vừa làm chính thức
				{
					thongKeThang.Cong_Congnhat = (dsNgayCong
								.Where(item => item.Ngay >= ngayBDCongnhat && item.Ngay <= ngayKTCongnhat)
								.Sum(item => item.TongCong));

					foreach (var ngayCong in dsNgayCong.Where(item => item.Ngay < ngayBDCongnhat || item.Ngay > ngayKTCongnhat))
						XL.ThongKeNgay(ref thongKeThang, ngayCong);
				}
			}

		}

		private static void ThongKeNgay(ref ThongKeCong_PC thongKeThang, cNgayCong ngayCong) {
			thongKeThang.Cong += ngayCong.TongCong;
			thongKeThang.TongNgayLV += ngayCong.TongNgayLV;//ver4.0.0.1
			thongKeThang.PhuCaps._TongPC += ngayCong.PhuCaps._TongPC;
			thongKeThang.PhuCaps._30_dem += ngayCong.PhuCaps._30_dem;
			thongKeThang.PhuCaps._50_TC += ngayCong.PhuCaps._50_TC;
			thongKeThang.PhuCaps._100_TCC3 += ngayCong.PhuCaps._100_TCC3;
			thongKeThang.PhuCaps._100_LVNN_Ngay += ngayCong.PhuCaps._100_LVNN_Ngay;
			thongKeThang.PhuCaps._150_LVNN_Dem += ngayCong.PhuCaps._150_LVNN_Dem;
			thongKeThang.PhuCaps._200_LeTet_Ngay += ngayCong.PhuCaps._200_LeTet_Ngay;
			thongKeThang.PhuCaps._250_LeTet_Dem += ngayCong.PhuCaps._250_LeTet_Dem;
			thongKeThang.PhuCaps._Cus += ngayCong.PhuCaps._Cus;
			thongKeThang.NgayQuaDem += (ngayCong.QuaDem) ? 1 : 0;
			foreach (var xpVang in ngayCong.DSVang) {
				var WorkingDay = xpVang.WorkingDay;
				switch (xpVang.MaLV_Code) {
					case "L":
						thongKeThang.Le = thongKeThang.Le + WorkingDay;
						break;
					case "P":
						thongKeThang.Phep = thongKeThang.Phep + WorkingDay;
						break;
					case "CV":
						thongKeThang.CongCV_KB = thongKeThang.CongCV_KB + WorkingDay; // tính trước công chờ việc khai báo, tính công chờ việc tự động sau
						break;
					case "BH":
					case "TS":
					case "BD": //info new version
						thongKeThang.BHXH = thongKeThang.BHXH + WorkingDay;
						break;
					case "H":
					case "CT":
					case "PT":
						thongKeThang.H_CT_PT = thongKeThang.H_CT_PT + WorkingDay;
						break;
					case "PD":
						thongKeThang.PTDT = thongKeThang.PTDT + WorkingDay;//DANGLAM
						break;
					case "RO":
						thongKeThang.NghiRo = thongKeThang.NghiRo + WorkingDay;
						break;
				}

			}

		}
	}

}
