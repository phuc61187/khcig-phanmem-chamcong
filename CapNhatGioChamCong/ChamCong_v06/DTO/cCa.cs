using System;
using ChamCong_v06.Helper;

namespace ChamCong_v06.DTO {
    public class cCa {
        public int ID;
        public string Code;
        public bool TachCaDem;
        public FromToTimeSpan Duty;

        public int OnTimeInMin;
        public int CutInMin;
        public int OnTimeOutMin;
        public int CutOutMin;
        public FromToTimeSpan NhanDienVao {
            get {
                FromToTimeSpan kq = new FromToTimeSpan {
                    From = Duty.From.Subtract(new TimeSpan(0, OnTimeInMin, 0)),
                    To = Duty.From.Add(new TimeSpan(0, CutInMin, 0))
                };
                return kq;
            }
        }
        public FromToTimeSpan NhanDienRaa {
            get {
                FromToTimeSpan kq = new FromToTimeSpan {
                    From = Duty.To.Subtract(new TimeSpan(0, OnTimeOutMin, 0)),
                    To = Duty.To.Add(new TimeSpan(0, CutOutMin, 0))
                };
                return kq;
            }
        }
        public TimeSpan LateeMin;
        public TimeSpan EarlyMin;
        public TimeSpan AfterOTMin;
        public TimeSpan ChoPhepTre_TimeOfDay { get { return Duty.From + LateeMin; } }
        public TimeSpan ChophepSom_TimeOfDay { get { return Duty.To - EarlyMin; } }
        public TimeSpan BatdauOT_TimeOfDay { get { return Duty.To + AfterOTMin; } }

        public TimeSpan LunchMin;
        public TimeSpan WorkingTimeTS;
        public float Workingday;
        public int DayCount;
        public bool QuaDem;
        public FromToTimeSpan NightTime;
        //public TimeSpan StartNT;//ver 4.0.0.4	start night time
        //public TimeSpan EndddNT;//ver 4.0.0.4	enddd night time

        public string MoTa;
        public string KyHieuCC;
        public bool Is_CaTuDo;


        public override string ToString() {
            var temp = "Code:{0}; Onn:{1} Off:{2} [{3} - {4}] [{5} - {6}]";
            return string.Format(temp, Code, Duty.From.ToString(@"d\ hh\:mm"), Duty.To.ToString(@"d\ hh\:mm"),
                NhanDienVao.From.ToString(@"d\ hh\:mm"), NhanDienVao.To.ToString(@"d\ hh\:mm"),
                NhanDienRaa.From.ToString(@"d\ hh\:mm"), NhanDienRaa.To.ToString(@"d\ hh\:mm"));
        }
        public cCa() { }
    }
}
