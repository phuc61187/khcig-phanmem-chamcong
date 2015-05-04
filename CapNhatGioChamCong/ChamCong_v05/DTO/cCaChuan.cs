﻿using System;
using System.Collections.Generic;
using ChamCong_v05.Helper;

namespace ChamCong_v05.DTO {
	public class cShiftSchedule {
		public int SchID;
		public string TenLichTrinh;
		public List<List<cCa>> DSCaThu = new List<List<cCa>>(7);
		public List<List<cCa>> DSCaMRThu = new List<List<cCa>>(7);
		public TGLamDemTheoQuyDinh TGLamDemTheoQuyDinh = TGLamDemTheoQuyDinh._22h00;
		public cShiftSchedule()
		{
		}

	}
/*
		public override string ToString() {

			return "\t" + Code + " \t" + ID + ";\ton:" + OnnTS.ToString() + "\toff:" + OffTS.ToString() + "\tOnIN:" + OnnInnTS + "\tCutIN:" + CutInnTS
				+ "\tOnOUT" + OnnOutTS + "\tCutOUT" + CutOutTS + "\tTRE" + chophepTreTS + "\tSOM" + chophepSomTS + "\tOT" + TOD_batdaulamthem;

			//return MyUtility.GetAllValueOfObject(this);
		}
*/



	public class cCa
	{
		//public TimeSpan chophepTreTS { get; set; }
		//public TimeSpan chophepSomTS { get; set; }
		public TS TOD_ChoPhepTreSom;
		public TimeSpan TOD_batdaulamthem { get; set; }
		public int ID { get; set; }
		public string Code { get; set; }
		public TS TOD_Duty { get; set; }
		public TS TOD_NhanDienVao { get; set; }
		public TS TOD_NhanDienRaa { get; set; }
		public TimeSpan LateeMin { get; set; }
		public TimeSpan EarlyMin { get; set; }
		public TimeSpan AfterOTMin { get; set; }
		public TimeSpan LunchMin { get; set; }
		public TimeSpan WorkingTimeTS { get; set; }
		public float Workingday { get; set; }
		public int ShowPosition { get; set; }
		public int DayCount { get; set; }
		public bool QuaDem { get; set; }
		//public TimeSpan StartNT;//ver 4.0.0.4	start night time
		//public TimeSpan EndddNT;//ver 4.0.0.4	enddd night time
		public TS TOD_NightTime;

		public string MoTa;
		public string KyHieuCC;
		public bool IsExtended;
		public bool TachCaDem;
		public int idCaTruoc;
		public int idCaSauuu;
		public cCa catruoc;
		public cCa casauuu;

		public bool Is_CaTuDo;
		
		public override string ToString()
		{
			var temp = "Code:{0}; Onn:{1} Off:{2} [{3} - {4}] [{5} - {6}]";
			return string.Format(temp,Code, TOD_Duty.Onn.ToString(@"d\ hh\:mm"), TOD_Duty.Off.ToString(@"d\ hh\:mm"), 
				TOD_NhanDienVao.Onn.ToString(@"d\ hh\:mm"), TOD_NhanDienVao.Off.ToString(@"d\ hh\:mm"),
				TOD_NhanDienRaa.Onn.ToString(@"d\ hh\:mm"), TOD_NhanDienRaa.Off.ToString(@"d\ hh\:mm"));
		}
		public cCa(){}
	}


}
