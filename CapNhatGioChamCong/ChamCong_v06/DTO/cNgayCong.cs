using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamCong_v06.BUS;
using ChamCong_v06.Helper;

namespace ChamCong_v06.DTO {
	public class cNgayCong {
		public DateTime Ngay;
		public int MaCC;
		public List<cCheckInOut_DaCC> DSVaoRa = new List<cCheckInOut_DaCC>();
		public List<cKhaiBaoVang> DSVang = new List<cKhaiBaoVang>();
		public cXacNhanPhuCapNgay XN_PCNgay = new cXacNhanPhuCapNgay();
		public bool IsHoliday = false;

		public bool QuaDem;

		public TimeSpan Tre;
		public TimeSpan Som;
		public TimeSpan VaoSauCa;
		public TimeSpan RaTruocCa;

		public TimeSpan HienDien;
		public TimeSpan LamDem;
		public TimeSpan LamViec;
		public TimeSpan LamThem {
			get {
				return
				   LamViec > GlobalVariables._08gio
				   ? LamViec - GlobalVariables._08gio
				   : TimeSpan.Zero;
			}
		}

		public TimeSpan LamTangCuongDem {
			get { return (LamThem > TimeSpan.Zero && LamDem > TimeSpan.Zero) ? LamThem : TimeSpan.Zero; }
		}

		//public TimeSpan LamNgay { get { return LamViec - LamDem; } }

		/*
				public TrangThaiDiemDanh TrangThaiDiemDanh;

				public override string ToString() {
					var temp = Ngay.ToString("d/M") + "; " + "; " + "; TongLam: " + TG.GioLamViec.TotalHours.ToString("##.##") + "; LamDem" +
						   TG.LamBanDem.TotalHours.ToString("#0.##") + "; Cong: " + TongCong.ToString("#0.##") + "; PC: " + PhuCaps._TongPC.ToString("#0.##") + "\n";
					if (DSVaoRa != null) {
						temp = DSVaoRa.Aggregate(temp, (current, @out) => current + "; I:" + ((@out.Vao != null) ? @out.Vao.Time.ToString("H:mm") : "----") + "; O:" + ((@out.Raa != null) ? @out.Raa.Time.ToString("H:mm") : "----") + "\n");
					}
					return temp;
				}


				public void XuatChuoiVang(ref string temp) {
					temp += DSVang.Aggregate(string.Empty, (current, loaiVang) => current + (loaiVang.LayKyHieu() + ";"));
				}
				public void XuatChuoiKyHieuChamCong(ref string temp) {
					temp = string.Empty;
					//xuất theo format <chuỗi ký hiệu ca> <chuỗi ký hiệu vắng> <chuỗi ký hiệu phụ cấp>
					foreach (var CIO in DSVaoRa) {
						if (CIO.HaveINOUT < 0) continue;
						temp += (temp == string.Empty) ? CIO.ThuocCa.KyHieuCC : ", " + CIO.ThuocCa.KyHieuCC;
					}
					foreach (cLoaiVang vang in DSVang)
						temp = temp + ((temp == string.Empty) ? vang.LayKyHieu() : ", " + vang.LayKyHieu());

					//có đi làm (tức có ký hiệu chấm công mới có phụ cấp nên luôn là dấu , trước
					if (PhuCaps._100_LVNN_Ngay > 0f) temp += ", (x2)";
					if (PhuCaps._150_LVNN_Dem > 0f) temp += ", (x2đ)";
					if (PhuCaps._200_LeTet_Ngay > 0f) temp += ", (x3)";
					if (PhuCaps._250_LeTet_Dem > 0f) temp += ", (x3đ)";
					if (PhuCaps._Cus > 0f) temp += ", (xYC)";
					if (PhuCaps._50_TC > 0f) temp += ", (TC)";
					if (PhuCaps._100_TCC3 > 0f) temp += ", (T3)";
				}
				public void XuatChuoiKyHieuPhuCap_Vang(ref string temp) {
					temp = string.Empty;
					if (DSVang != null && DSVang.Count == 0)
						foreach (cLoaiVang vang in DSVang)
							temp = temp + ((temp == string.Empty) ? vang.LayKyHieu() : ", " + vang.LayKyHieu());
					if (PhuCaps._30_dem > 0f) temp += "; (C3)";
					if (TinhPCDB) {
						if (PhuCaps._100_LVNN_Ngay > 0f) temp += "; (x2)";
						if (PhuCaps._150_LVNN_Dem > 0f) temp += "; (x2đ)";
						if (PhuCaps._200_LeTet_Ngay > 0f) temp += "; (x3)";
						if (PhuCaps._250_LeTet_Dem > 0f) temp += "; (x3đ)";
						if (PhuCaps._Cus > 0f) temp += "; (xYC)";
					}
					else if (TinhPC50) {
						if (PhuCaps._50_TC > 0f) temp += "; (TC)";
						if (PhuCaps._100_TCC3 > 0f) temp += "; (T3)";
					}
				}

				public string CIOs_CodeComp(string chuoiTruoc = null) {
					var kq = DSVaoRa.Aggregate(string.Empty, (current, CIO) => current + ((current == string.Empty) ? CIO.CIOCodeComp() : "; " + CIO.CIOCodeComp()));
					kq = (chuoiTruoc == null) ? kq : chuoiTruoc + "; " + kq;
					return kq;
				}
				public string CIOs_CodeFull(string chuoiTruoc = null) {
					var kq = DSVaoRa.Aggregate(string.Empty, (current, CIO) => current + ((current == string.Empty) ? CIO.CIOCodeFull() : "; " + CIO.CIOCodeFull()));
					kq = (chuoiTruoc == null) ? kq : chuoiTruoc + "; " + kq;
					return kq;
				}

				public string Absents_Code(string chuoiTruoc = null) {
					// xuất chuỗi ký hiệu vắng
					var kq = DSVang.Aggregate(string.Empty, (current, loaiVang) => current + ((current == string.Empty) ? loaiVang.LayKyHieu() : "; " + loaiVang.LayKyHieu()));
					kq = (chuoiTruoc == null) ? kq : chuoiTruoc + "; " + kq;
					return kq;
				}

				public string CIOs_Absents_Code_Comp(string chuoiTruoc = null) {
					// lấy chuỗi CIO Code và absent code
					var CIOs_Code = string.Empty;
					CIOs_Code += CIOs_CodeComp();
					var AbsentsCode = string.Empty;
					AbsentsCode += Absents_Code();
					var kq = string.Empty;

					// kết hợp theo format <cio code> <absentcode> <phu cap 50, 100>
					if (string.IsNullOrEmpty(CIOs_Code)) kq += AbsentsCode;
					else if (string.IsNullOrEmpty(AbsentsCode)) kq += CIOs_Code;
					else kq = (CIOs_Code + ";" + AbsentsCode);
					kq = (chuoiTruoc == null) ? kq : chuoiTruoc + ";" + kq;

					if (TinhPCDB) {
						if (PhuCaps._100_LVNN_Ngay > 0f) kq += ";(x2)";
						if (PhuCaps._150_LVNN_Dem > 0f) kq += ";(x2đ)";
						if (PhuCaps._200_LeTet_Ngay > 0f) kq += ";(x3)";
						if (PhuCaps._250_LeTet_Dem > 0f) kq += ";(x3đ)";
						if (PhuCaps._Cus > 0f) kq += ";(xYC)";
					}
					else if (TinhPC50) {
						if (PhuCaps._50_TC > 0f) kq += ";(TC)";
						if (PhuCaps._100_TCC3 > 0f) kq += ";(T3)";
					}
					return kq;
				}
				public string CIOs_Absents_Code_Full(string chuoiTruoc = null) {
					// lấy chuỗi CIO Code và absent code
					var CIOs_Code = string.Empty;
					CIOs_Code += CIOs_CodeFull();
					var AbsentsCode = string.Empty;
					AbsentsCode += Absents_Code();
					var kq = string.Empty;
					// kết hợp theo format <cio code> <absentcode> <phu cap 50, 100>
					if (string.IsNullOrEmpty(CIOs_Code)) kq += AbsentsCode;
					else if (string.IsNullOrEmpty(AbsentsCode)) kq += CIOs_Code;
					else kq = (CIOs_Code + ";" + AbsentsCode);
					kq = (chuoiTruoc == null) ? kq : chuoiTruoc + ";" + kq;
					if (TinhPC50 && PhuCaps._50_TC > 0f) kq += ";(TC)";
					if (TinhPCDB) {
						if (PhuCaps._100_LVNN_Ngay > 0f) kq += ";(x2)";
						if (PhuCaps._150_LVNN_Dem > 0f) kq += ";(x2đ)";
						if (PhuCaps._200_LeTet_Ngay > 0f) kq += ";(x3)";
						if (PhuCaps._250_LeTet_Dem > 0f) kq += ";(x3đ)";
						if (PhuCaps._Cus > 0f) kq += ";(xYC)";
					}
					else if (TinhPC50) {
						if (PhuCaps._50_TC > 0f) kq += ";(TC)";
						if (PhuCaps._100_TCC3 > 0f) kq += ";(T3)";
					}
					return kq;
				}
				public cNgayCong() { }
		*/

		public float TruCongTre;
		public float TruCongSom;
		public float CongTrongGio;
		public float CongNgoaiGio;
		public float ChamCongTay;

		public float DinhMucCong;
		public float TongCong;

		public bool DuocTinhPCTC;
		public bool DuocTinhPCNgayNghi;
		public bool DuocTinhPCNgayLe;
		public bool DuocTinhPCTay;
		public float PhuCapTay;

		public float PhuCapDem;
		public float PhuCapTangCuong;
		public float PhuCapThemNgayThuong;
		public float PhuCapNgayNghi;
		public float PhuCapThemNgayNghi;
		public float PhuCapNgayLe;
		public float PhuCapThemNgayLe;
		public float TongPhuCap;

		public string KyHieuCa_1Ngay {
			get {
				if (DSVaoRa == null || DSVaoRa.Count == 0) return string.Empty;
				return DSVaoRa.Aggregate(string.Empty, (current, item) => current + item.KyHieuCa + ";");
			}
		}

		public string KyHieuVang_1Ngay {
			get {
				string kq = string.Empty;
				if (IsHoliday) kq = "L;";
				if (DSVang == null || DSVang.Count == 0) return kq;
				kq += DSVang.Aggregate(string.Empty, (current, item) => current + item.KyHieuVang + ";");
				return kq;
			}
		}

		public void Them_CheckInOut_DaCC(cCheckInOut_DaCC item) {
			if (item.CheckVT != TrangThaiCheck.CheckDayDu) {
				//todo
			}
			else //item.CheckVân tay đầy đủ
			{
				Tre += item.Tre;
				Som += item.Som;
				VaoSauCa += item.VaoSauCa;
				RaTruocCa += item.RaTruocCa;

				HienDien += (item.RaaLamTron - item.VaoLamTron);
				LamViec += item.LamViec;
				LamDem += item.LamDem;

				TruCongTre += item.TruCongTre;
				TruCongSom += item.TruCongSom;
				CongTrongGio += item.CongTrongGio;
				CongNgoaiGio += item.CongNgoaiGio;
				ChamCongTay += item.ChamCongTay;
				DinhMucCong += item.DinhMuc;
				TongCong += item.Tong;

				if (item.QuaDem) QuaDem = true;
			}
			this.DSVaoRa.Add(item);
		}

		public void Xoa_CheckInOut_DaCC(cCheckInOut_DaCC item) {
			if (item.CheckVT != TrangThaiCheck.CheckDayDu) {
				//todo
			}
			else { //item.CheckVân tay đầy đủ
				Tre -= item.Tre;
				Som -= item.Som;
				VaoSauCa -= item.VaoSauCa;
				RaTruocCa -= item.RaTruocCa;

				HienDien -= (item.RaaLamTron - item.VaoLamTron);
				LamViec -= item.LamViec;
				LamDem -= item.LamDem;

				TruCongTre -= item.TruCongTre;
				TruCongSom -= item.TruCongSom;
				CongTrongGio -= item.CongTrongGio;
				CongNgoaiGio -= item.CongNgoaiGio;
				ChamCongTay -= item.ChamCongTay;
				DinhMucCong -= item.DinhMuc;
				TongCong -= item.Tong;

				if (DSVaoRa.Any(itemQuaDem => item.QuaDem)) QuaDem = true;
			}
			this.DSVaoRa.Remove(item);
		}

		public void SetXacNhanPhuCap(bool DuocTinhPCTC, bool DuocTinhPCNgayNghi, bool DuocTinhPCNgayLe, bool DuocTinhPCTay, float PhuCapTay = 0f) {
			this.DuocTinhPCTC = DuocTinhPCTC;
			this.DuocTinhPCNgayNghi = DuocTinhPCNgayNghi;
			this.DuocTinhPCNgayLe = DuocTinhPCNgayLe;
			this.DuocTinhPCTay = DuocTinhPCTay;
			this.PhuCapTay = PhuCapTay;
		}

		public void TinhLaiPhuCap() {
			if (QuaDem) PhuCapDem = MyUtility.TinhPhuCap(LamDem, GlobalVariables.HSPCDem);

			if (DuocTinhPCTC) {
				//ngayCong.PhuCapDem = TinhPhuCap(ngayCong.LamDem, GlobalVariables.HSPCDem);
				PhuCapTangCuong = MyUtility.TinhPhuCap(LamThem, GlobalVariables.HSPCTangCuong);
				PhuCapThemNgayThuong = MyUtility.TinhPhuCap(LamTangCuongDem, GlobalVariables.HSPCThem_NgayThuong);
				PhuCapNgayNghi = 0f;
				PhuCapThemNgayNghi = 0f;
				PhuCapNgayLe = 0f;
				PhuCapThemNgayLe = 0f;
			}
			if (DuocTinhPCNgayNghi) {
				//ngayCong.PhuCapDem = TinhPhuCap(ngayCong.LamDem, GlobalVariables.HSPCDem);
				PhuCapTangCuong = 0f;
				PhuCapThemNgayThuong = 0f;
				PhuCapNgayNghi = MyUtility.TinhPhuCap(LamViec, GlobalVariables.HSPCNgayNghi);
				PhuCapThemNgayNghi = MyUtility.TinhPhuCap(LamDem, GlobalVariables.HSPCThem_NgayNghi);
				PhuCapNgayLe = 0f;
				PhuCapThemNgayLe = 0f;
			}
			if (DuocTinhPCNgayLe) {
				//ngayCong.PhuCapDem = TinhPhuCap(ngayCong.LamDem, GlobalVariables.HSPCDem);
				PhuCapTangCuong = 0f;
				PhuCapThemNgayThuong = 0f;
				PhuCapNgayNghi = 0f;
				PhuCapThemNgayNghi = 0f;
				PhuCapNgayLe = MyUtility.TinhPhuCap(LamViec, GlobalVariables.HSPCNgayLe);
				PhuCapThemNgayLe = MyUtility.TinhPhuCap(LamDem, GlobalVariables.HSPCThem_NgayLe);
			}
			if (DuocTinhPCTay) {
				PhuCapDem = 0f;
				PhuCapTangCuong = 0f;
				PhuCapThemNgayThuong = 0f;
				PhuCapNgayNghi = 0f;
				PhuCapThemNgayNghi = 0f;
				PhuCapNgayLe = 0f;
				PhuCapThemNgayLe = 0f;
			}
			TongPhuCap = PhuCapDem + PhuCapTangCuong + PhuCapThemNgayThuong + PhuCapNgayNghi + PhuCapThemNgayNghi + PhuCapNgayLe + PhuCapThemNgayLe + PhuCapTay;
		}

	}
}
