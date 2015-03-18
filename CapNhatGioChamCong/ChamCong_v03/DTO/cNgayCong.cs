using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong_v03.DTO {
	public class cNgayCong {
		public DateTime Ngay;
		public List<cChkInOut> DSVaoRa = new List<cChkInOut>();
		public List<cChkInOut> DSThieu_VaoHoacRa = new List<cChkInOut>();
		public List<cLoaiVang> DSVang = new List<cLoaiVang>();
		public cNgayCong prev;
		public cNgayCong next;

		public ushort IsEdited = 0;
		/// <summary>
		/// false nếu vắng mặt, true nếu có mặt, chỉ cần có check xem như có mặt
		/// </summary>
		public bool HasCheck;
		public ThoiGian TG = new ThoiGian();

		public Double TongCong; 
		public Double TongPhuCap;
		public bool QuaDem;
		public Double PhuCap30;
		public bool TinhPC50;
		public Double PhuCap50;
		public bool TinhPCTCC3;
		public double PhuCapTCC3;
		public bool TinhPCDB;
		public int LoaiPCDB;
		public Double PhuCap100;
		public Double PhuCap160;
		public Double PhuCap200;
		public Double PhuCap290;
		//public Double PhuCap
		public Double PhuCapCus;
	    public Double TruCongCV;

		public override string ToString() {
			var temp = Ngay.ToString("d/M") + "; " + ((HasCheck) ? "HasCheck" : "-") + "; " + "; TongLam: " + TG.GioLamTrongNgay.TotalHours.ToString("##.##") + "; LamDem" +
				   TG.LamDemTrongNgay.TotalHours.ToString("#0.##") + "; Cong: " + TongCong.ToString("#0.##") + "; PC: " + TongPhuCap.ToString("#0.##") + "\n";
			if (DSVaoRa != null) {
				temp = DSVaoRa.Aggregate(temp, (current, @out) => current + "; I:" + ((@out.Vao != null) ? @out.Vao.Time.ToString("H:mm") : "----") + "; O:" + ((@out.Raa != null) ? @out.Raa.Time.ToString("H:mm") : "----") + "\n");
			}
			return temp;
		}

		public void ResetPC()
		{
			PhuCap30 = 0d;
			PhuCap50 = 0d;
			PhuCapTCC3 = 0d;
			PhuCap100 = 0d;
			PhuCap160 = 0d;
			PhuCap200 = 0d;
			PhuCap290 = 0d;
			TongPhuCap = 0d;
		}

		public void XuatChuoiVang(ref string temp) {
			temp += DSVang.Aggregate(string.Empty, (current, loaiVang) => current + (loaiVang.LayKyHieu() + ";"));
		}
		public void XuatChuoiKyHieuChamCong(ref string temp) {
			temp = string.Empty;
			foreach (var CIO in DSVaoRa) {
				if (CIO.HaveINOUT < 0) continue;
				temp += (temp == string.Empty) ? CIO.ThuocCa.KyHieuCC : "," + CIO.ThuocCa.KyHieuCC;
			}
			foreach (cLoaiVang vang in DSVang)
				temp = temp + ((temp == string.Empty) ? vang.LayKyHieu() : "," + vang.LayKyHieu());
			if (TinhPCDB) {
				if (PhuCap100 > 0f) temp += ";(X2N)";
				if (PhuCap160 > 0f) temp += ";(X2D)";
				if (PhuCap200 > 0f) temp += ";(X3N)";
				if (PhuCap290 > 0f) temp += ";(X3D)";
				if (PhuCapCus > 0f) temp += ";(XTC)";
			}
			else if (TinhPC50) {
				if (PhuCap50 > 0f) temp += ";(+)";
				if (PhuCapTCC3 > 0f) temp += ";(+1)";
			}
		}

		public string CIOs_CodeComp(string chuoiTruoc = null) {
			var kq = DSVaoRa.Aggregate(string.Empty, (current, CIO) => current + ((current == string.Empty) ? CIO.CIOCodeComp() : ";" + CIO.CIOCodeComp()));
			kq = (chuoiTruoc == null) ? kq : chuoiTruoc + ";" + kq;
			return kq;
		}
		public string CIOs_CodeFull(string chuoiTruoc = null) {
			var kq = DSVaoRa.Aggregate(string.Empty, (current, CIO) => current + ((current == string.Empty) ? CIO.CIOCodeFull() : ";" + CIO.CIOCodeFull()));
			kq = (chuoiTruoc == null) ? kq : chuoiTruoc + ";" + kq;
			return kq;
		}

		public string Absents_Code(string chuoiTruoc = null) {
			var kq = DSVang.Aggregate(string.Empty, (current, loaiVang) => current + ((current == string.Empty) ? loaiVang.LayKyHieu() : ";" + loaiVang.LayKyHieu()));
			kq = (chuoiTruoc == null) ? kq : chuoiTruoc + ";" + kq;
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
			
			if (TinhPCDB)
			{
				if (PhuCap100 > 0f) kq += ";(X2N)";
				if (PhuCap160 > 0f) kq += ";(X2D)";
				if (PhuCap200 > 0f) kq += ";(X3N)";
				if (PhuCap290 > 0f) kq += ";(X3D)";
				if (PhuCapCus > 0f) kq += ";(XTC)";
			}
			else if (TinhPC50)
			{
				if (PhuCap50 > 0f) kq += ";(+)";
				if (PhuCapTCC3 > 0f) kq += ";(+1)";
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
			if (TinhPCDB) {
				if (PhuCap100 > 0f) kq += ";(X2N)";
				if (PhuCap160 > 0f) kq += ";(X2D)";
				if (PhuCap200 > 0f) kq += ";(X3N)";
				if (PhuCap290 > 0f) kq += ";(X3D)";
				if (PhuCapCus > 0f) kq += ";(XTC)";
			}
			else if (TinhPC50) {
				if (PhuCap50 > 0f) kq += ";(+)";
				if (PhuCapTCC3 > 0f) kq += ";(+1)";
			}
			return kq;
		}
		public cNgayCong() { }
	}


	public class cLoaiVang {
		public string MaLV;
		public float Cong;
		public string MoTa;
		public string KyHieu;
		public DateTime Ngay;
		public string LayKyHieu()
		{
			var kq=string.Empty;
			if (Cong == 0.5f) kq += "N" + KyHieu;
			else if (Cong == 1f) kq += KyHieu;
			else if (Cong == 2f) kq+="2"+KyHieu;
			return kq;
		}
	}

	public class cTemp {
		public int UserEnrollNumber;
		public DateTime Ngay;
		public int LoaiPC;
		public bool Duyet;
		public int PCNgay;
		public int PCDem;
		public override string ToString() {
			return UserEnrollNumber + "; " + LoaiPC + "; " + PCNgay.ToString("##0.0#") + "; " + Ngay.ToString("d/M") + Duyet;
		}
	}

	public class cTemp1 {
		public int UserEnrollNumber;
		public DateTime Ngay;
		public bool TinhPC50;
		public override string ToString() {
			return UserEnrollNumber + "; " + TinhPC50 + "; " + Ngay.ToString("d/M");
		}

	}
}
