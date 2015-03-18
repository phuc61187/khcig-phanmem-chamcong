using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiuLaiCacFileCu.DTO {
	[Serializable]
	public class cChkInOut {
		#region properties
		private TimeSpan _tongGioThuc;
		private DateTime _thuocNgayCong;
		private DateTime _timeStrDaiDien;
		private int _haveInout;
		#endregion 
		public DateTime TimeStrDaiDien {
			get {
				return Vao != null ? Vao.TimeStr : Raa.TimeStr;
			}
			set { _timeStrDaiDien = value; }
		}

		public bool IsOT { get; set; }
		public bool DaXN { get; set; }
		public cChk Vao { get; set; }
		public cChk Raa { get; set; }
		public TimeSpan TongGioThuc {
			get {
				if (_tongGioThuc != TimeSpan.MinValue) return _tongGioThuc;
				return (HaveINOUT > 0) ? Raa.TimeStr.Subtract(Vao.TimeStr) : ThamSo._0gio;
			}
			set { _tongGioThuc = value; }
		}

		public cShift ThuocCa { get; set; }

		/// <summary>
		/// NULL if chấm thủ công
		/// </summary>
		public DateTime VaoLam { get; set; }
		public DateTime RaaLam { get; set; }
		public TimeSpan TGLamTinhCong { get; set; }

		public TimeSpan VaoTre { get; set; }
		public TimeSpan RaaSom { get; set; }
		public TimeSpan OLaiThem { get; set; }
		public TimeSpan LamThem { get; set; }
		public bool QuaDem { get; set; }
		public DateTime BDLamDem { get; set; }
		public DateTime KTLamDem { get; set; }
		public TimeSpan TGLamDem { get; set; }
		public DateTime ThuocNgayCong {
			get {
				return (Vao != null) ? ThamSo.GetDate(Vao.TimeStr) : ThamSo.GetDate(Raa.TimeStr);
			}
			set { _thuocNgayCong = value; }
		}

		public float Cong { get; set; }
		public float PhuCapDem { get; set; }

		/// <summary>
		/// 1 : if đủ check in, check out và làm việc trong giới hạn 21 tiếng 45phut
		/// -1: ko có vào
		/// -2: ko có ra
		/// 2 : tổng giờ vào ra > 21 tiếng 45ph
		/// 0 : mới khởi tạo hoặc trường hợp khác
		/// </summary>
		public int HaveINOUT {
			get {
				if (_haveInout != 0) return _haveInout;
				if (Vao != null && Raa != null) {
					if ((Raa.TimeStr - Vao.TimeStr) > ThamSo._21h45)
						return 2;
					return 1;
				}
				if (Vao == null && Raa != null) return -1;
				if (Raa == null && Vao != null) return -2;
				log4net.ILog log = log4net.LogManager.GetLogger("ERROR HAVEINOUT = 0");
				return 0;
			}
			set { _haveInout = value; }
		}

		public bool TinhPC150 { get; set; }
		//tạm thời ko sử dụng PhuCapTC này, sử dụng PC bên ngày công
		public bool TinhPC200 { get; set; }

		public override string ToString() {
			string temp = String.Empty;
			if (Vao != null) temp += "Vao: " + Vao.TimeStr.ToString("d/M H:mm:ss");
			if (Raa != null) temp += "\t Raa: " + Raa.TimeStr.ToString("d/M H:mm:ss");
			if (ThuocCa != null) temp += "\t Caa:" + ThuocCa.ShiftCode;
			temp += "\nVL:" + VaoLam.ToString("d/M H:mm") + "\tRL:" + RaaLam.ToString("d/M H:mm");
			temp += "\tTongLAM:" + TGLamTinhCong.TotalHours.ToString("#0.##");
			return temp;
		}

	}
	public class cChkInOutComparer : IComparer<cChkInOut> {
		public int Compare(cChkInOut x, cChkInOut y) {
			return x.TimeStrDaiDien.CompareTo(y.TimeStrDaiDien);
			//return 1;
		}
	}

	[Serializable]
	public abstract class cChk {
		#region protected Properties

		protected int userEnrollNumber { get; set; }
		protected DateTime timeStr { get; set; }
		protected string source { get; set; }
		protected int machineNo { get; set; }
		protected List<cChk> pGioLienQuan { get; set; }
		#endregion
		#region Public Properties

		public abstract DateTime TimeStr { get; set; }
		public abstract string Source { get; set; }
		public abstract int MachineNo { get; set; }
		public abstract List<cChk> GioLienQuan { get; set; }

		public override string ToString() {
			return " TimeStr: " + TimeStr.ToString("dd/MM H:mm") + "\n";
		}
		#endregion


	}

	[Serializable]
	public class cChkIn : cChk {
		public override DateTime TimeStr { get { return timeStr; } set { timeStr = value; } }
		public override string Source { get { return source; } set { source = value; } }
		public override int MachineNo { get { return machineNo; } set { machineNo = value; } }
		public override List<cChk> GioLienQuan { get { return pGioLienQuan; } set { pGioLienQuan = value; } }
		public override string ToString() {
			return "ChkInn " + TimeStr.ToString("d/M H:mm:ss") + "\t";
		}
	}

	[Serializable]
	public class cChkOut : cChk {
		public override DateTime TimeStr { get { return timeStr; } set { timeStr = value; } }
		public override string Source { get { return source; } set { source = value; } }
		public override int MachineNo { get { return machineNo; } set { machineNo = value; } }
		public override List<cChk> GioLienQuan { get { return pGioLienQuan; } set { pGioLienQuan = value; } }
		public override string ToString() {
			return "ChkOut " + TimeStr.ToString("d/M H:mm:ss") + "\t";
		}
	}

	[Serializable]
	public class cChkInn_V : cChk {
		public int ID;
		public override DateTime TimeStr { get { return timeStr; } set { timeStr = value; } }
		public override string Source { get { return source; } set { source = value; } }
		public override int MachineNo { get { return machineNo; } set { machineNo = value; } }
		public override List<cChk> GioLienQuan { get { return pGioLienQuan; } set { pGioLienQuan = value; } }
		public override string ToString() {
			return "cChkInn_V " + TimeStr.ToString("d/M H:mm:ss") + "\t";
		}
	}
	[Serializable]
	public class cChkOut_V : cChk {
		public int ID;
		public override DateTime TimeStr { get { return timeStr; } set { timeStr = value; } }
		public override string Source { get { return source; } set { source = value; } }
		public override int MachineNo { get { return machineNo; } set { machineNo = value; } }
		public override List<cChk> GioLienQuan { get { return pGioLienQuan; } set { pGioLienQuan = value; } }
		public override string ToString() {
			return "cChkOut_V " + TimeStr.ToString("d/M H:mm:ss") + "\t";
		}
	}
	[Serializable]
	public class cChkInn_A : cChk {
		public override DateTime TimeStr { get { return timeStr; } set { timeStr = value; } }
		public override string Source { get { return source; } set { source = value; } }
		public override int MachineNo { get { return machineNo; } set { machineNo = value; } }
		public override List<cChk> GioLienQuan { get { return pGioLienQuan; } set { pGioLienQuan = value; } }
		public override string ToString() {
			return "cChkInn_A " + TimeStr.ToString("d/M H:mm:ss") + "\t";
		}
	}
	[Serializable]
	public class cChkOut_A : cChk {
		public override DateTime TimeStr { get { return timeStr; } set { timeStr = value; } }
		public override string Source { get { return source; } set { source = value; } }
		public override int MachineNo { get { return machineNo; } set { machineNo = value; } }
		public override List<cChk> GioLienQuan { get { return pGioLienQuan; } set { pGioLienQuan = value; } }
		public override string ToString() {
			return "cChkOut_A " + TimeStr.ToString("d/M H:mm:ss") + "\t";
		}
	}

	public class cChkInOut_A {
		#region properties
		private DateTime _thuocNgayCong;
		private DateTime _timeStrDaiDien;
		private int _haveInout;
		#endregion

		public DateTime TimeStrDaiDien;

		public bool IsOT { get; set; }
		public bool DaXN { get; set; }
		public cChk Vao { get; set; }
		public cChk Raa { get; set; }
		public TimeSpan TongGioThuc;

		public cShift ThuocCa { get; set; }

		/// <summary>
		/// NULL if chấm thủ công
		/// </summary>
		public DateTime VaoLam { get; set; }
		public DateTime RaaLam { get; set; }
		public TimeSpan TGLamTinhCong { get; set; }

		public TimeSpan VaoTre { get; set; }
		public TimeSpan RaaSom { get; set; }
		public TimeSpan OLaiThem { get; set; }
		public TimeSpan LamThem { get; set; }
		public bool QuaDem { get; set; }
		public DateTime BDLamDem { get; set; }
		public DateTime KTLamDem { get; set; }
		public TimeSpan TGLamDem { get; set; }
		public DateTime ThuocNgayCong {
			get {
				return (Vao != null) ? ThamSo.GetDate(Vao.TimeStr) : ThamSo.GetDate(Raa.TimeStr);
			}
			set { _thuocNgayCong = value; }
		}

		public float Cong { get; set; }
		public float PhuCapDem { get; set; }

		/// <summary>
		/// 1 : if đủ check in, check out và làm việc trong giới hạn 21 tiếng 45phut
		/// -1: ko có vào
		/// -2: ko có ra
		/// 2 : tổng giờ vào ra > 21 tiếng 45ph
		/// 0 : mới khởi tạo hoặc trường hợp khác
		/// </summary>
		public int HaveINOUT {
			get {
				if (_haveInout != 0) return _haveInout;
				if (Vao != null && Raa != null) {
					if ((Raa.TimeStr - Vao.TimeStr) > ThamSo._21h45)
						return 2;
					return 1;
				}
				if (Vao == null && Raa != null) return -1;
				if (Raa == null && Vao != null) return -2;
				log4net.ILog log = log4net.LogManager.GetLogger("ERROR HAVEINOUT = 0");
				return 0;
			}
			set { _haveInout = value; }
		}

		public bool TinhPC150 { get; set; }
		//tạm thời ko sử dụng PhuCapTC này, sử dụng PC bên ngày công
		public bool TinhPC200 { get; set; }

		public override string ToString() {
			string temp = String.Empty;
			if (Vao != null) temp += "Vao: " + Vao.TimeStr.ToString("d/M H:mm:ss");
			if (Raa != null) temp += "\t Raa: " + Raa.TimeStr.ToString("d/M H:mm:ss");
			if (ThuocCa != null) temp += "\t Caa:" + ThuocCa.ShiftCode;
			temp += "\nVL:" + VaoLam.ToString("d/M H:mm") + "\tRL:" + RaaLam.ToString("d/M H:mm");
			temp += "\tTongLAM:" + TGLamTinhCong.TotalHours.ToString("#0.##");
			return temp;
		}

	}

}
