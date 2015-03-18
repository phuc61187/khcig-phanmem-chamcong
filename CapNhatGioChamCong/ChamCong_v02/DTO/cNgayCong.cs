using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong_v02.DTO {
	public class TinhTron {
		public Double TongCong;
		public Double TongPC30;
		public Double TongPC50;
		public Double TongPC100;
	}

	public class cNgayCong {
	    public DateTime NgayCong;
        public List<cChkInOut> DSVaoRa { get; set; }
        public List<cLoaiVang> DSVang { get; set; }

	    /// <summary>
	    /// false nếu vắng mặt, true nếu có mặt, chỉ cần có check xem như có mặt
	    /// </summary>
	    public bool HasCheck;
		public bool QuaDem;
		public ThoiGian TG = new ThoiGian();
        public TimeSpan TongGioThuc { get; set; }
        public Double	TongCong { get; set; } // tổng công = tổng công check vân tay + tổng công chấm tay, không tính các công khai báo
		public TinhTron TinhTron = new TinhTron();
        public TimeSpan TongTre { get; set; }
        public TimeSpan TongSom { get; set; }
        public Double TongPhuCap { get; set; }
        public Double PhuCap30 { get; set; }
        public Double PhuCap50 { get; set; }
		public bool TinhPC150 { get; set; }
/*        public override string ToString() {
            string temp = NgayCong.ToString("d/M") + "; " + ((HasCheck) ? "HasCheck" : "--") + "; " + "; TongLam: " + TG.LamTinhCong.TotalHours.ToString("##.##") + "; Thuc: " + TongGioThuc.TotalHours.ToString("##.##") + "; LamDem" +
                   TG.LamTinhPC30.TotalHours.ToString("#0.##") + "; Cong: " + TongCong.ToString("#0.##") + "; PC: " + TongPhuCap.ToString("#0.##") + "; Tre: " + TongTre.TotalMinutes.ToString("####") + "; Som: " + TongSom.TotalMinutes.ToString("####") +
                   "\n";
            if (DSVaoRa != null) {
	            foreach (cChkInOut @out in DSVaoRa) 
					temp = temp + "; I:" + ((@out.Vao != null) ? @out.Vao.TimeStr.ToString("H:mm") : "----" )
						+ "; O:" + ((@out.Raa != null) ? @out.Raa.TimeStr.ToString("H:mm") : "----" ) + "\n";
            }
            return temp;
        }*/

		internal void them(cChkInOut tmpVaoRa1) {
			if (DSVaoRa == null) DSVaoRa = new List<cChkInOut>();
			DSVaoRa.Add(tmpVaoRa1);

		}

		public string XuatChuoiVang() {
			if (DSVang == null || DSVang.Count == 0) return string.Empty;
			string kq = string.Empty;
			foreach (cLoaiVang loaiVang in DSVang) {
				kq += loaiVang.KyHieu + ";";
			}
			return kq;
		}
        public cNgayCong(){}
    }


    public class cLoaiVang {
        public string MaLV { get; set; }
        public float Cong { get; set; }
        public string MoTa { get; set; }
        public string KyHieu { get; set; }
        public DateTime Ngay { get; set; }
    }
}
