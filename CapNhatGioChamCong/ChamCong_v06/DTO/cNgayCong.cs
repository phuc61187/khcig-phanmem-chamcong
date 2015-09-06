using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamCong_v06.Helper;

namespace ChamCong_v06.DTO
{
    public class cNgayCong
    {
        public DateTime Ngay;
        public List<cCheckInOut> DSVaoRa = new List<cCheckInOut>();
        //public List<cLoaiVang> DSVang = new List<cLoaiVang>();
        public cNgayCong prev;
        public cNgayCong next;
	    public StructTGNgay TGNgay;
	    public StructCongCa TongCongCa;

        //public ThoiGian TG = new ThoiGian();

        public bool QuaDem;
        //public PhuCap PhuCaps;

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

    }
}
