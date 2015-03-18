using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong_v02.DTO
{
	public class ThoiGian {
		public TimeSpan LamTinhCong;
		public TimeSpan LamTinhPC30;
		public TimeSpan LamTinhPC50;
	}

    [Serializable]
    public abstract class cChk
    {
        #region Public Properties

        public abstract DateTime TimeStr { get; set; }
        public abstract string Source { get; set; }
        public abstract int MachineNo { get; set; }
        public abstract List<cChk> GioLienQuan { get; set; }

        public override string ToString()
        {
            return " TimeStr: " + TimeStr.ToString("dd/MM H:mm") + "\n";
        }
        #endregion


    }

    [Serializable]
    public class cChkInn_V : cChk
    {
        public int ID;

        public override DateTime TimeStr { get; set; }
        public override string Source { get; set; }
        public override int MachineNo { get; set; }
        public override List<cChk> GioLienQuan { get; set; }

        public override string ToString()
        {
            return "cChkInn_V " + TimeStr.ToString("d/M H:mm:ss") + "\t";
        }
    }
    [Serializable]
    public class cChkOut_V : cChk
    {
        public int ID;
        public override DateTime TimeStr { get; set; }
        public override string Source { get; set; }
        public override int MachineNo { get; set; }
        public override List<cChk> GioLienQuan { get; set; }
        public override string ToString()
        {
            return "cChkOut_V " + TimeStr.ToString("d/M H:mm:ss") + "\t";
        }
    }
    [Serializable]
    public class cChkInn_A : cChk
    {
        public override DateTime TimeStr { get; set; }
        public override string Source { get; set; }
        public override int MachineNo { get; set; }
        public override List<cChk> GioLienQuan { get; set; }
        public override string ToString()
        {
            return "cChkInn_A " + TimeStr.ToString("d/M H:mm:ss") + "\t";
        }
    }
    [Serializable]
    public class cChkOut_A : cChk
    {
        public override DateTime TimeStr { get; set; }
        public override string Source { get; set; }
        public override int MachineNo { get; set; }
        public override List<cChk> GioLienQuan { get; set; }
        public override string ToString()
        {
            return "cChkOut_A " + TimeStr.ToString("d/M H:mm:ss") + "\t";
        }
    }

    [Serializable]
    public abstract class cChkInOut
    {
        public abstract DateTime TimeStrDaiDien { get; set; }

        public abstract cChk Vao { get; set; }
        public abstract cChk Raa { get; set; }
        public abstract TimeSpan TongGioThuc { get; set; }

        public abstract cShift ThuocCa { get; set; }
        public abstract DateTime VaoLam { get; set; }
        public abstract DateTime RaaLam { get; set; }
		public abstract ThoiGian TG { get; set; } 

        public abstract TimeSpan VaoTre { get; set; }
        public abstract TimeSpan RaaSom { get; set; }
        public abstract TimeSpan OLaiThem { get; set; }
        public abstract TimeSpan LamThem { get; set; }
        public abstract bool QuaDem { get; set; }
        public abstract DateTime ThuocNgayCong { get; set; }

        public abstract Double Cong { get; set; }
        public abstract Double PhuCapDem { get; set; }
		public abstract TinhTron LamTron { get; set; }

        public abstract int HaveINOUT { get; set; }

        public abstract bool TinhPC150 { get; set; }

        public override string ToString() {
			string temp = String.Empty;
            if (GetType() == typeof(cChkInOut_A)) temp += "Auto;";
            else temp += "Veri;";
			if (Vao != null) temp += "Vao: " + Vao.TimeStr.ToString("d/M H:mm:ss");
			if (Raa != null) temp += "\t Raa: " + Raa.TimeStr.ToString("d/M H:mm:ss");
            if (HaveINOUT == 1) temp += "INN_OUT;"; 
            else if (HaveINOUT == -1) temp += "INN_---;";
            else if (HaveINOUT == -2) temp += "---_OUT;";
			if (ThuocCa != null) temp += "\t Caa:"+ ThuocCa.ShiftID+"_" + ThuocCa.ShiftCode + ";";
			//temp += "\nVL:" + VaoLam.ToString("d/M H:mm") + "\tRL:" + RaaLam.ToString("d/M H:mm");
			//temp += "\tTongLAM:" + TGLamTinhCong.TotalHours.ToString("#0.##");
		    temp += "ThuocNgay:" + ThuocNgayCong.ToString("d/M");
			return temp;
		}

    }

	[Serializable]
    public class cChkInOut_V : cChkInOut
    {
        public override DateTime TimeStrDaiDien { get; set; }
        public override cChk Vao { get; set; }
        public override cChk Raa { get; set; }
        public override TimeSpan TongGioThuc { get; set; }
        public override cShift ThuocCa { get; set; }
        public override DateTime VaoLam { get; set; }
        public override DateTime RaaLam { get; set; }
		public override ThoiGian TG { get; set; }
		public override TimeSpan VaoTre { get; set; }
        public override TimeSpan RaaSom { get; set; }
        public override TimeSpan OLaiThem { get; set; }
        public override TimeSpan LamThem { get; set; }
        public override bool QuaDem { get; set; }
        public override DateTime ThuocNgayCong { get; set; }
        public override Double Cong { get; set; }
		public override Double PhuCapDem { get; set; }
		public override TinhTron LamTron { get; set; }
		public override int HaveINOUT { get; set; }
        public override bool TinhPC150 { get; set; }
		public cChkInOut_V() {
			TG = new ThoiGian();
			LamTron = new TinhTron();
		}
    }

	[Serializable]
    public class cChkInOut_A : cChkInOut
    {
        public override DateTime TimeStrDaiDien { get; set; }
        public override cChk Vao { get; set; }
        public override cChk Raa { get; set; }
        public override TimeSpan TongGioThuc { get; set; }
        public override cShift ThuocCa { get; set; }
        public override DateTime VaoLam { get; set; }
        public override DateTime RaaLam { get; set; }
		public override ThoiGian TG { get; set; }
		public override TimeSpan VaoTre { get; set; }
        public override TimeSpan RaaSom { get; set; }
        public override TimeSpan OLaiThem { get; set; }
        public override TimeSpan LamThem { get; set; }
        public override bool QuaDem { get; set; }
        public override DateTime ThuocNgayCong { get; set; }
        public override Double Cong { get; set; }
		public override Double PhuCapDem { get; set; }
		public override TinhTron LamTron { get; set; }
		public override int HaveINOUT { get; set; }
        public override bool TinhPC150 { get; set; }
		public cChkInOut_A() {
			TG = new ThoiGian();
			LamTron = new TinhTron();
		}
    }



}
