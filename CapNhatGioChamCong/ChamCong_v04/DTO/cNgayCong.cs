using System;
using System.Collections.Generic;
using System.Linq;
using ChamCong_v04.Helper;

namespace ChamCong_v04.DTO {
	public class cNgayCong {
		public DateTime Ngay;
		public List<cCheckInOut> DSVaoRa = new List<cCheckInOut>();
		public List<cLoaiVang> DSVang = new List<cLoaiVang>();
		public cNgayCong prev;
		public cNgayCong next;

		public ThoiGian TG = new ThoiGian();

		//public float TongCong;
		//public float TongNgayLV;//ver4.0.0.1

		#region //ver 4.0.0.8

		public float TongCong_4008;//ver 4.0.0.8
		public float CongTichLuy;//ver 4.0.0.8
		public float CongDinhMucDuoi8Tieng;//ver 4.0.0.8

		public float TruCongTreBu;//ver 4.0.0.8
		public float TruCongSomBu;//ver 4.0.0.8
		public float TruCongTreVR;//ver 4.0.0.8
		public float TruCongSomVR;//ver 4.0.0.8
		public float CongBuPhepTre;//ver 4.0.0.8
		public float CongBuPhepSom;//ver 4.0.0.8

		#endregion

		public bool QuaDem;
		public bool TinhPC50;
		public bool TinhPCDB;
		public int LoaiPCDB;
		public PhuCap PhuCaps;

		public TrangThaiDiemDanh TrangThaiDiemDanh;

		public override string ToString() {
			var temp = Ngay.ToString("d/M") + "; " + "; " + "; TongLam: " + TG.GioLamViec.TotalHours.ToString("##.##") + "; LamDem" +
				   TG.LamBanDem.TotalHours.ToString("#0.##") + "; Cong: " + TongCong_4008.ToString("#0.##") + "; PC: " + PhuCaps._TongPC.ToString("#0.##") + "\n";
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
				temp += (temp == string.Empty) ? CIO.ThuocCa.KyHieuCC + CIO.KyHieuBuPhepBuGio() : ", " + CIO.ThuocCa.KyHieuCC + CIO.KyHieuBuPhepBuGio();
			}
			foreach (cLoaiVang vang in DSVang)
				temp = temp + ((temp == string.Empty) ? vang.LayKyHieu() : ", " + vang.LayKyHieu());

			//có đi làm (tức có ký hiệu chấm công mới có phụ cấp nên luôn là dấu , trước
			if (PhuCaps._100_LVNN_Ngay > 0f) temp += ", (x2)";
			if (PhuCaps._150_LVNN_Dem > 0f) temp += ", (x2đ)";
			if (PhuCaps._200_LeTet_Ngay > 0f) temp += ", (x3)";
			if (PhuCaps._250_LeTet_Dem > 0f) temp += ", (x3đ)";
			if (PhuCaps._Cus > 0f) temp += ", (x)";
			if (PhuCaps._50_TC > 0f) temp += ", (tc)";
			if (PhuCaps._100_TCC3 > 0f) temp += ", (t3)";
		}
		public void XuatChuoiKyHieuPhuCap_Vang(ref string temp) {
			temp = string.Empty;
			if (DSVang != null && DSVang.Count == 0)
				foreach (cLoaiVang vang in DSVang)
					temp = temp + ((temp == string.Empty) ? vang.LayKyHieu() : ", " + vang.LayKyHieu());
			if (PhuCaps._30_dem > 0f) temp += "; (c3)";
			if (TinhPCDB) {
				if (PhuCaps._100_LVNN_Ngay > 0f) temp += "; (x2)";
				if (PhuCaps._150_LVNN_Dem > 0f) temp += "; (x2đ)";
				if (PhuCaps._200_LeTet_Ngay > 0f) temp += "; (x3)";
				if (PhuCaps._250_LeTet_Dem > 0f) temp += "; (x3đ)";
				if (PhuCaps._Cus > 0f) temp += "; (x)";
			}
			else if (TinhPC50) {
				if (PhuCaps._50_TC > 0f) temp += "; (tc)";
				if (PhuCaps._100_TCC3 > 0f) temp += "; (t3)";
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
			else kq = (CIOs_Code + "; " + AbsentsCode);
			kq = (chuoiTruoc == null) ? kq : chuoiTruoc + "; " + kq;

			if (TinhPCDB) {
				if (PhuCaps._100_LVNN_Ngay > 0f) kq += "; (x2)";
				if (PhuCaps._150_LVNN_Dem > 0f) kq += "; (x2đ)";
				if (PhuCaps._200_LeTet_Ngay > 0f) kq += "; (x3)";
				if (PhuCaps._250_LeTet_Dem > 0f) kq += "; (x3đ)";
				if (PhuCaps._Cus > 0f) kq += "; (x)";
			}
			else if (TinhPC50) {
				if (PhuCaps._50_TC > 0f) kq += "; (tc)";
				if (PhuCaps._100_TCC3 > 0f) kq += "; (t3)";
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
			kq = (chuoiTruoc == null) ? kq : chuoiTruoc + "; " + kq;
			if (TinhPC50 && PhuCaps._50_TC > 0f) kq += "; (tc)";
			if (TinhPCDB) {
				if (PhuCaps._100_LVNN_Ngay > 0f) kq += "; (x2)";
				if (PhuCaps._150_LVNN_Dem > 0f) kq += "; (x2đ)";
				if (PhuCaps._200_LeTet_Ngay > 0f) kq += "; (x3)";
				if (PhuCaps._250_LeTet_Dem > 0f) kq += "; (x3đ)";
				if (PhuCaps._Cus > 0f) kq += "; (x)";
			}
			else if (TinhPC50) {
				if (PhuCaps._50_TC > 0f) kq += "; (tc)";
				if (PhuCaps._100_TCC3 > 0f) kq += "; (t3)";
			}
			return kq;
		}
		public cNgayCong() { }

	}


	public class cLoaiVang {
		public string MaLV_Code;
		public float WorkingDay;
		public float PhuCap;
		public string MoTa;
		public DateTime Ngay;
		public string LayKyHieu() {
			var kq = string.Empty;
			if (Math.Abs(WorkingDay - 0f) < 0.01f) kq += string.Empty;
			else if (Math.Abs(WorkingDay - 0.25f) < 0.01f) kq += "2t" + MaLV_Code;
			else if (Math.Abs(WorkingDay - 0.5f) < 0.01f) kq += "n" + MaLV_Code;
			else if (Math.Abs(WorkingDay - 1f) < 0.01f) kq += MaLV_Code;
			return kq;
		}

		public cLoaiVang()
		{
			
		}
	}

	public struct structPCDB {
		public DateTime Ngay;
		public int LoaiPC;
		public bool Duyet;
		public int PCNgay;
		public int PCDem;
		public override string ToString() {
			return LoaiPC + "; " + PCNgay.ToString("##0.0#") + "; " + Ngay.ToString("d/M") + Duyet;
		}
	}

	public struct structPCTC {
		public DateTime Ngay;
		public bool TinhPC50;
		public override string ToString() {
			return TinhPC50 + "; " + Ngay.ToString("d/M");
		}

	}
}
