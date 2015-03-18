using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong_v03.DTO {
	public class cPhucHoi {
		public bool Them;
		public int IDGioGoc;
		public bool Xoaa;
	}

	public class ThoiGian {
		public TimeSpan GioThuc = TimeSpan.Zero;
		public TimeSpan GioLamTrongNgay = TimeSpan.Zero;
		public TimeSpan LamDemTrongNgay = TimeSpan.Zero;
		public TimeSpan LamThemTrongNgay = TimeSpan.Zero;
		//public TimeSpan Tinh100 = TimeSpan.Zero;
		public TimeSpan Tinh130 = TimeSpan.Zero;
		public TimeSpan Tinh150 = TimeSpan.Zero;
		public TimeSpan TinhTCC3 = TimeSpan.Zero;
		public TimeSpan Tinh200 = TimeSpan.Zero;
		public TimeSpan Tinh260 = TimeSpan.Zero;
		public TimeSpan Tinh300 = TimeSpan.Zero;
		public TimeSpan Tinh390 = TimeSpan.Zero;
		public TimeSpan TinhPCCus = TimeSpan.Zero;

		public TimeSpan VaoTre = TimeSpan.Zero;
		public TimeSpan RaaSom = TimeSpan.Zero;
		public TimeSpan OLai = TimeSpan.Zero;
		public TimeSpan OTCa = TimeSpan.Zero;
	}

	public class ThoiDiem
	{
		public DateTime VaoLam;// vào làm ca
		public DateTime RaaLam;// 
		public DateTime RaLam_ChuaOT;
		public DateTime RaLam_DaCoOT;
	}

	[Serializable]
	public abstract class cChk {
		#region Public Properties

		public abstract ushort IsEdited { get; set; }
		public abstract int MaCC { get; set; }
		public abstract DateTime Time { get; set; }
		public abstract string Source { get; set; }
		public abstract int MachineNo { get; set; }
		public abstract string Type { get; set; }
		public abstract cPhucHoi PhucHoi { get; set; }

		public override string ToString() {
			return " TimeStr: " + Time.ToString("d/M H:mm") + "\n";
		}
		#endregion


	}

	[Serializable]
	public class cChkInn_V : cChk {
		public int ID;

		public override ushort IsEdited { get; set; }
		public override int MaCC { get; set; }
		public override DateTime Time { get; set; }
		public override string Source { get; set; }
		public override int MachineNo { get; set; }
		public override string Type { get; set; }
		public override cPhucHoi PhucHoi { get; set; }

		public override string ToString() {
			return "cChkInn_V " + Time.ToString("d/M H:mm:ss") + "\t";
		}

		public cChkInn_V() {
		}
	}
	[Serializable]
	public class cChkOut_V : cChk {
		public int ID;
		public override ushort IsEdited { get; set; }
		public override int MaCC { get; set; }
		public override DateTime Time { get; set; }
		public override string Source { get; set; }
		public override int MachineNo { get; set; }
		public override string Type { get; set; }
		public override cPhucHoi PhucHoi { get; set; }

		public override string ToString() {
			return "cChkOut_V " + Time.ToString("d/M H:mm:ss") + "\t";
		}
		public cChkOut_V() {
		}
	}
	[Serializable]
	public class cChkInn_A : cChk {
		public override ushort IsEdited { get; set; }
		public override int MaCC { get; set; }
		public override DateTime Time { get; set; }
		public override string Source { get; set; }
		public override int MachineNo { get; set; }
		public override string Type { get; set; }
		public override cPhucHoi PhucHoi { get; set; }

		public override string ToString() {
			return "cChkInn_A " + Time.ToString("d/M H:mm:ss") + "\t";
		}
		public cChkInn_A() {
		}
	}
	[Serializable]
	public class cChkOut_A : cChk {
		public override ushort IsEdited { get; set; }
		public override int MaCC { get; set; }
		public override DateTime Time { get; set; }
		public override string Source { get; set; }
		public override int MachineNo { get; set; }
		public override string Type { get; set; }
		public override cPhucHoi PhucHoi { get; set; }

		public override string ToString() {
			return "cChkOut_A " + Time.ToString("d/M H:mm:ss") + "\t";
		}
		public cChkOut_A() {
		}
	}

	[Serializable]
	public abstract class cChkInOut {
		public abstract ushort IsEdited { get; set; }
		public abstract DateTime TimeDaiDien { get; set; }

		public abstract cChk Vao { get; set; }
		public abstract cChk Raa { get; set; }

		public abstract cCaAbs ThuocCa { get; set; }
		public abstract ThoiGian TG { get; set; }
		public abstract ThoiDiem TD { get; set; }
		public abstract bool QuaDem { get; set; }
		public abstract DateTime ThuocNgayCong { get; set; }

		public abstract Double Cong { get; set; }

		public abstract int HaveINOUT { get; set; }
		public abstract bool TreSomTinhCV { get; set; }

		public virtual string CIOCodeComp(string chuoiTruoc = null) {
			return base.ToString();
		}
		public virtual string CIOCodeFull(string chuoiTruoc = null) {
			return base.ToString();
		}
		public override string ToString() {
			string temp = String.Empty;
			temp += GetType() == typeof(cChkInOut_A) ? "A;" : "V;";
			if (Vao != null) temp += "Vao: " + Vao.Time.ToString("d/M H:mm:ss");
			if (Raa != null) temp += "\t Raa: " + Raa.Time.ToString("d/M H:mm:ss");
			if (HaveINOUT == 0) temp += "INN_OUT;";
			else if (HaveINOUT == -1) temp += "INN_---;";
			else if (HaveINOUT == -2) temp += "---_OUT;";
			else if (HaveINOUT == 30) temp += "INOUT30;";
			if (ThuocCa != null) temp += "\t Caa:" + ThuocCa.Code + ";";
			temp += "ThuocNgay:" + ThuocNgayCong.ToString("d/M");
			return temp;
		}

	}

	[Serializable]
	public class cChkInOut_V : cChkInOut {
		public override ushort IsEdited { get; set; }
		public override DateTime TimeDaiDien { get; set; }
		public override cChk Vao { get; set; }
		public override cChk Raa { get; set; }
		public override cCaAbs ThuocCa { get; set; }
		public override ThoiGian TG { get; set; }
		public override ThoiDiem TD {get;set;}

		public override bool QuaDem { get; set; }
		public override DateTime ThuocNgayCong { get; set; }
		public override Double Cong { get; set; }
		public override int HaveINOUT { get; set; }
		public override bool TreSomTinhCV { get; set; }
		//public override bool TinhPC150 { get; set; }//[140615_2]



		public override string CIOCodeComp(string chuoiTruoc = null) {
			return (chuoiTruoc == null)
				? "XN-" + ThuocCa.Code
				: chuoiTruoc + ";XN-" + ThuocCa.Code;
		}

		public override string CIOCodeFull(string chuoiTruoc = null) {
			return CIOCodeComp(chuoiTruoc);
		}


		public int ID;
		public cChkInOut_V() {
		}

	}

	[Serializable]
	public class cChkInOut_A : cChkInOut {
		public override ushort IsEdited { get; set; }
		public override DateTime TimeDaiDien { get; set; }
		public override cChk Vao { get; set; }
		public override cChk Raa { get; set; }
		public override cCaAbs ThuocCa { get; set; }
		public override ThoiGian TG { get; set; }
		public override ThoiDiem TD {get;set;}
		public override bool QuaDem { get; set; }
		public override DateTime ThuocNgayCong { get; set; }
		public override Double Cong { get; set; }
		public override int HaveINOUT { get; set; }
		public override bool TreSomTinhCV { get; set; }
		//public override bool TinhPC150 { get; set; }//[140615_2]

		public override string CIOCodeComp(string chuoiTruoc = null) {
			var kq = string.Empty;
			if (HaveINOUT == -2) kq = "KV";
			else if (HaveINOUT == -1) kq = "KR";
			else kq = ThuocCa.Code;
			return (chuoiTruoc == null)
					   ? kq
					   : chuoiTruoc + ";" + kq;
		}

		public override string CIOCodeFull(string chuoiTruoc = null) {
			var kq = string.Empty;
			if (HaveINOUT == -2) {
				kq += "KV(";
				if (DSCa != null) {
					var result = string.Empty;
					foreach (var abs in DSCa)
						if (result == string.Empty)
							result += abs.Code;
						else result += ";" + abs.Code;
					kq += result;
				}
				kq += ")";
			}
			else if (HaveINOUT == -1) {
				kq += "KR(";
				if (DSCa != null) {
					var result = string.Empty;
					foreach (var abs in DSCa)
						if (result == string.Empty)
							result += abs.Code;
						else result += ";" + abs.Code;
					kq += result;
				}
				kq += ")";
			}
			else {
				kq += ThuocCa.Code;
			}
			return (chuoiTruoc == null)
								   ? kq
								   : chuoiTruoc + ";" + kq;

		}

		public List<cCaAbs> DSCa;

		public cChkInOut_A() {
		}

	}


}
