using System;
using System.Collections.Generic;
using ChamCong_v05.Helper;

namespace ChamCong_v05.DTO {
	public class cShiftSchedule {
		public int SchID;
		public string TenLichTrinh;
		public List<List<cCa>> DSCaThu = new List<List<cCa>>(7);
		public List<List<cCa>> DSCaMRThu = new List<List<cCa>>(7);
		public TGLamDemTheoQuyDinh TGLamDemTheoQuyDinh = TGLamDemTheoQuyDinh._22h00;
		public cShiftSchedule() {
		}

	}
	/*
			public override string ToString() {

				return "\t" + Code + " \t" + ID + ";\ton:" + OnnTS.ToString() + "\toff:" + OffTS.ToString() + "\tOnIN:" + OnnInnTS + "\tCutIN:" + CutInnTS
					+ "\tOnOUT" + OnnOutTS + "\tCutOUT" + CutOutTS + "\tTRE" + chophepTreTS + "\tSOM" + chophepSomTS + "\tOT" + TOD_batdaulamthem;

				//return MyUtility.GetAllValueOfObject(this);
			}
	*/



	public class cCa {
		//public TimeSpan chophepTreTS { get; set; }
		//public TimeSpan chophepSomTS { get; set; }
		public int ID { get; set; }
		public string Code;
		public TS TOD_Duty;
		//public TS TOD_NhanDienVao;
		//public TS TOD_NhanDienRaa;
		public int PhutOnnInn;
		public TimeSpan TS_PhutOnnInn { get { return new TimeSpan(0, PhutOnnInn, 0); } }
		public int PhutCutInn;
		public TimeSpan TS_PhutCutInn { get { return new TimeSpan(0, PhutCutInn, 0); } }
		public int PhutOnnOut;
		public TimeSpan TS_PhutOnnOut { get { return new TimeSpan(0, PhutOnnOut, 0); } }
		public int PhutCutOut;
		public TimeSpan TS_PhutCutOut { get { return new TimeSpan(0, PhutCutOut, 0); } }
		public int PhutChoTre;
		public TimeSpan TS_PhutChoTre {
			get {
				return new TimeSpan(0, PhutChoTre, 0);
			}
		}
		public int PhutChoSom;
		public TimeSpan TS_PhutChoSom {
			get {
				return new TimeSpan(0, PhutChoSom, 0);
			}
		}
		public int PhutToiThieuTinhOT;
		public TimeSpan TS_PhutAfterOT {
			get {
				return new TimeSpan(0, PhutToiThieuTinhOT, 0);
			}
		}
		public int PhutNghiTrua;
		public TimeSpan TS_PhutNghiTrua {
			get {
				return new TimeSpan(0, PhutNghiTrua, 0);
			}
		}

		public TimeSpan WorkingTimeTS;
		public float Workingday;
		public bool QuaDem {
			get { return (TOD_Duty.Off.Days > 0); }
		}
		public TS TOD_NightTime;

		public string MoTa;
		public string KyHieuCC;
		public bool IsExtended;
		public bool TachCaDem;
		public int idCaTruoc;
		public int idCaSauuu;
		public cCa catruoc;
		public cCa casauuu;

		public override string ToString() {
			var temp = "Code:{0}; Onn:{1} Off:{2} [{3} - {4}] [{5} - {6}]";
			return string.Format(temp, Code, TOD_Duty.Onn.ToString(@"d\ hh\:mm"), TOD_Duty.Off.ToString(@"d\ hh\:mm")/*,
				TOD_NhanDienVao.Onn.ToString(@"d\ hh\:mm"), TOD_NhanDienVao.Off.ToString(@"d\ hh\:mm"),
				TOD_NhanDienRaa.Onn.ToString(@"d\ hh\:mm"), TOD_NhanDienRaa.Off.ToString(@"d\ hh\:mm")*/);
		}
		public cCa() { }

		public DateTime ThoiDiemTre(DateTime Ngay) {
			return Ngay.Add(this.TOD_Duty.Onn.Add(this.TS_PhutChoTre));
		}
		public DateTime ThoiDiemSom(DateTime Ngay) {
			return Ngay.Add(this.TOD_Duty.Off.Subtract(this.TS_PhutChoSom));
		}
		public DateTime ThoiDiemTinhOT(DateTime Ngay) {
			return Ngay.Add(this.TOD_Duty.Off.Add(this.TS_PhutAfterOT));
		}
		public DateTime ThoiDiemOnnInn(DateTime Ngay) {
			return Ngay.Add(this.TOD_Duty.Onn.Subtract(this.TS_PhutOnnInn));
		}
		public DateTime ThoiDiemCutInn(DateTime Ngay) {
			return Ngay.Add(this.TOD_Duty.Onn.Add(this.TS_PhutCutInn));
		}
		public DateTime ThoiDiemOnnOut(DateTime Ngay) {
			return Ngay.Add(this.TOD_Duty.Off.Subtract(this.TS_PhutOnnOut));
		}
		public DateTime ThoiDiemCutOut(DateTime Ngay) {
			return Ngay.Add(this.TOD_Duty.Off.Add(this.TS_PhutCutOut));
		}
	}


}
