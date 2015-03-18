using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ChamCong_v02.DTO;

namespace ChamCong_v02 {
    public class ThamSo {

        public static int currUserID = int.MinValue;
        public static string currUserAccount = string.Empty;

        public static readonly TimeSpan _0gio = TimeSpan.Zero;
        public static readonly TimeSpan _05phut = new TimeSpan(0, 5, 0);
        public static readonly TimeSpan _10phut = new TimeSpan(0, 10, 0);
        public static readonly TimeSpan _30phut = new TimeSpan(0, 30, 0);
        public static readonly TimeSpan _4gio = new TimeSpan(4, 0, 0);
        public static readonly TimeSpan _7gio45ph = new TimeSpan(7, 45, 0);
        public static readonly TimeSpan _8gio = new TimeSpan(8, 0, 0);
        public static readonly TimeSpan _8gio1giay = new TimeSpan(8, 0, 1);
        public static readonly TimeSpan _16gio = new TimeSpan(16, 0, 0);
        public static readonly TimeSpan _4h30 = new TimeSpan(4, 30, 0);
        public static readonly TimeSpan _05h45 = new TimeSpan(5, 45, 0);
        public static readonly TimeSpan _13h45 = new TimeSpan(13, 45, 0);
        public static readonly TimeSpan _20h00 = new TimeSpan(20, 0, 0);
        public static readonly TimeSpan _21h45 = new TimeSpan(21, 45, 0);
        public static readonly TimeSpan _1ngay = new TimeSpan(1, 0, 0, 0);
        public static readonly TimeSpan _12gio = new TimeSpan(12, 0, 0);
        public static readonly TimeSpan _24h00 = new TimeSpan(24, 0, 0);
        public static readonly TimeSpan _02gio = new TimeSpan(2, 0, 0);


        public const string nameVao = "TimeStrVao";
        public const string nameRa = "TimeStrRa";
        public const string nameNgay = "TimeStrNgay";
        public const string nameThu = "TimeStrThu";


        public static List<cShift> DSCa { get; set; }
        public static List<cShiftSchedule> DSLichTrinh { get; set; }
		

        public static void VeCheckBox_CheckAll(DataGridView grid, CheckBox checkBox, EventHandler checkAll_CheckedChanged, Point location) {
            Rectangle rect = grid.GetCellDisplayRectangle(0, -1, true);
            checkBox.Size = new Size(18, 18); checkBox.Location = new Point(rect.Location.X + location.X, rect.Location.Y + location.Y);
            checkBox.CheckedChanged += checkAll_CheckedChanged;
            grid.Controls.Add(checkBox);
        }

        /// <summary>
        /// Tạo DS Ca mở rộng. Đã bao gồm item Khác(int.MinValue)
        /// </summary>
        /// <param name="tmpDSCa"></param>
        /// <returns></returns>
        internal static List<cShift> TaoDSCaMoRong(List<cShift> tmpDSCa) {
			List<cShift> kq = new List<cShift>(tmpDSCa);
            List<cShift> DSCa1cong = tmpDSCa.FindAll(item => item.Workingday == 1f);
            List<cShift> DSCaNuaCong = tmpDSCa.FindAll(item => item.Workingday == 0.5f);
			if (DSCaNuaCong.Count == 0 || DSCa1cong.Count == 0) return new List<cShift>(tmpDSCa);

            foreach (cShift ca1Cong in DSCa1cong) {
                foreach (cShift caNuaCong in DSCaNuaCong) {
                    if (ca1Cong.OffDutyTS == caNuaCong.OnnDutyTS
                        || (ca1Cong.DayCount == 1 && ca1Cong.OffDutyTS.Add(new TimeSpan(-ca1Cong.DayCount, 0, 0, 0)) == caNuaCong.OnnDutyTS)) {
                        #region collapse
                        cShift tempShift = new cShift() {
                            LoaiCa = 0,
                            ShiftID = -ca1Cong.ShiftID,
                            ShiftCode = ca1Cong.ShiftCode + " & " + caNuaCong.ShiftCode,
                            DayCount = ca1Cong.DayCount + caNuaCong.DayCount, QuaDem = ((ca1Cong.DayCount + caNuaCong.DayCount) == 1),
                            OnnDutyTS = ca1Cong.OnnDutyTS,
                            OffDutyTS = caNuaCong.OffDutyTS,
                            OnTimeInTS = ca1Cong.OnTimeInTS,
                            CutInTS = ca1Cong.CutInTS,
                            OnTimeOutTS = caNuaCong.OnTimeOutTS,
                            CutOutTS = caNuaCong.CutOutTS,
                            AfterOTTS = caNuaCong.AfterOTTS,
                            LateGraceTS = ca1Cong.LateGraceTS,
                            EarlyGraceTS = caNuaCong.EarlyGraceTS,
                            Workingday = ca1Cong.Workingday + caNuaCong.Workingday,
                            ShowPosition = ca1Cong.ShowPosition,
                            WorkingTimeTS = ca1Cong.WorkingTimeTS + caNuaCong.WorkingTimeTS,
                            chophepvaotreTS = ca1Cong.OnnDutyTS + ca1Cong.LateGraceTS,
                            chopheprasomTS = caNuaCong.OffDutyTS - caNuaCong.EarlyGraceTS,
                            batdaulamthemTS = caNuaCong.OffDutyTS + caNuaCong.AfterOTTS,
                            LunchMinute = ca1Cong.LunchMinute + caNuaCong.LunchMinute,
                        };
                        #endregion
                        kq.Add(tempShift);
                    }
                    else if (caNuaCong.OffDutyTS == ca1Cong.OnnDutyTS) {
                        #region collapse
                        cShift tempShift = new cShift() {
                            LoaiCa = 0,
                            ShiftID = -caNuaCong.ShiftID,
                            ShiftCode = caNuaCong.ShiftCode + " & " + ca1Cong.ShiftCode,
                            DayCount = caNuaCong.DayCount + ca1Cong.DayCount, QuaDem = ((ca1Cong.DayCount + caNuaCong.DayCount) == 1),
                            OnnDutyTS = caNuaCong.OnnDutyTS,
                            OffDutyTS = ca1Cong.OffDutyTS,
                            OnTimeInTS = caNuaCong.OnTimeInTS,
                            CutInTS = caNuaCong.CutInTS,
                            OnTimeOutTS = ca1Cong.OnTimeOutTS,
                            CutOutTS = ca1Cong.CutOutTS,
                            AfterOTTS = ca1Cong.AfterOTTS,
                            LateGraceTS = caNuaCong.LateGraceTS,
                            EarlyGraceTS = ca1Cong.EarlyGraceTS,
                            Workingday = caNuaCong.Workingday + ca1Cong.Workingday,
                            ShowPosition = caNuaCong.ShowPosition,
                            WorkingTimeTS = caNuaCong.WorkingTimeTS + ca1Cong.WorkingTimeTS,
                            chophepvaotreTS = caNuaCong.OnnDutyTS + caNuaCong.LateGraceTS,
                            chopheprasomTS = ca1Cong.OffDutyTS - ca1Cong.EarlyGraceTS,
                            batdaulamthemTS = ca1Cong.OffDutyTS + ca1Cong.AfterOTTS,
                            LunchMinute = ca1Cong.LunchMinute + caNuaCong.LunchMinute,
                        };
                        #endregion
                        kq.Add(tempShift);
                    }

                    else continue;
                }
            }
            return kq;
        }

        internal static DateTime GetDate(DateTime tmpTimeStrVao) {
            return tmpTimeStrVao.TimeOfDay > ThamSo._4h30 ? tmpTimeStrVao.Date : tmpTimeStrVao.Date.Subtract(ThamSo._1ngay);
        }






    }
}
