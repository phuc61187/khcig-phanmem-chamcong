using System;
using ChamCong_v06.Helper;

namespace ChamCong_v06.DTO {
	public class cCa
	{
		public TimeSpan chophepTreTS;
		public TimeSpan chophepSomTS;
		public TimeSpan batdaulamthemTS;
		public int ID;
		public string Code;
		public bool TachCaDem;
		public FromToTimeSpan Duty;
		public FromToTimeSpan NhanDienVao;
		public FromToTimeSpan NhanDienRaa;
		public TimeSpan LateeMin;
		public TimeSpan EarlyMin;
		public TimeSpan AfterOTMin;
		public TimeSpan LunchMin;
		public TimeSpan WorkingTimeTS;
		public float Workingday;
		public int ShowPosition;
		public int DayCount;
		public bool QuaDem;
		public FromToTimeSpan NightTime;
		//public TimeSpan StartNT;//ver 4.0.0.4	start night time
		//public TimeSpan EndddNT;//ver 4.0.0.4	enddd night time

		public string MoTa;
		public string KyHieuCC;
		public bool Is_CaTuDo;

		public int OnTimeInMin;
		public int CutInMin;
		public int OnTimeOutMin;
		public int CutOutMin;
		

		public override string ToString() {
			var temp = "Code:{0}; Onn:{1} Off:{2} [{3} - {4}] [{5} - {6}]";
			return string.Format(temp, Code, Duty.From.ToString(@"d\ hh\:mm"), Duty.To.ToString(@"d\ hh\:mm"),
				NhanDienVao.From.ToString(@"d\ hh\:mm"), NhanDienVao.To.ToString(@"d\ hh\:mm"),
				NhanDienRaa.From.ToString(@"d\ hh\:mm"), NhanDienRaa.To.ToString(@"d\ hh\:mm"));
		}
		public cCa() { }
	}
}
