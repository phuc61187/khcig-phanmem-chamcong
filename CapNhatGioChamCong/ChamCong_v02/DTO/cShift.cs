using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong_v02.DTO {
	public class cShiftSchedule {

		public int SchID { get; set; }

		public List<cShift> ListT1 { get; set; }

		public List<cShift> ListT2 { get; set; }

		public List<cShift> ListT3 { get; set; }

		public List<cShift> ListT4 { get; set; }

		public List<cShift> ListT5 { get; set; }

		public List<cShift> ListT6 { get; set; }

		public List<cShift> ListT7 { get; set; }

	}

	[Serializable]
	public class cShift : cShiftAbstract {
		#region Public Properties
		public cShift(){}
		/// <summary>
		/// giờ. VD: onduty 7h30 => cho phép vào trễ: 7h40
		/// </summary>
        public override TimeSpan chophepvaotreTS { get; set; }
		/// <summary>
		/// giờ. VD: offduty 4h => cho phép ra sớm: 3h50
		/// </summary>
        public override TimeSpan chopheprasomTS { get; set; }
		/// <summary>
		/// giờ. VD: onduty 4h => bd làm thêm: 4h30
		/// </summary>
        public override TimeSpan batdaulamthemTS { get; set; }
		public override string ToString() {
			
						return "\t" + ShiftCode + " \t" + ShiftID + ";Loai:" + LoaiCa  + "\ton:" + OnnDutyTS.ToString() + "\toff:" + OffDutyTS.ToString() + "\tOnIN:" + OnTimeInTS + "\tCutIN:" + CutInTS 
                            + "\tOnOUT" + OnTimeOutTS + "\tCutOUT" + CutOutTS + "\tbdTRE" + chophepvaotreTS+ "\tbdSOM" +  chopheprasomTS+ "\tbdOT" + batdaulamthemTS;
			
			//return MyUtility.GetAllValueOfObject(this);
		}

		/// <summary>
		/// 0: ca chuẩn; 1: ca tự do tính công dựa trên 8 tiếng sau giờ vào
		/// </summary>
        public override int LoaiCa { get; set; }

        public override int ShiftID { get; set; }

        public override string ShiftCode { get; set; }
		/// <summary>
		/// timespan of day
		/// </summary>
        public override TimeSpan OnnDutyTS { get; set; }
		/// <summary>
		/// timespan of day
		/// </summary>
        public override TimeSpan OffDutyTS { get; set; }
		/// <summary>
		/// = 1 nếu ca qua đêm, = 0 nếu ca ngày
		/// </summary>
        public override int DayCount { get; set; }

        public override bool QuaDem { get; set; }

		/// <summary>
		/// số phút
		/// </summary>
        public override TimeSpan OnTimeInTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
        public override TimeSpan OnTimeOutTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
        public override TimeSpan CutInTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
        public override TimeSpan CutOutTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
        public override TimeSpan AfterOTTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
        public override TimeSpan LateGraceTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
        public override TimeSpan EarlyGraceTS { get; set; }
		/// <summary>
		/// timespan
		/// </summary>
        public override TimeSpan WorkingTimeTS { get; set; }
		/// <summary>
		/// Công: 0.5, 1, 2
		/// </summary>
        public override Single Workingday { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
        public override TimeSpan LunchMinute { get; set; }

        public override int ShowPosition { get; set; }


		#endregion

	}



	[Serializable]
	public abstract class cShiftAbstract {
		#region Public Properties

		/// <summary>
		/// giờ. VD: onduty 7h30 => cho phép vào trễ: 7h40
		/// </summary>
		public abstract TimeSpan chophepvaotreTS { get; set; }
		/// <summary>
		/// giờ. VD: offduty 4h => cho phép ra sớm: 3h50
		/// </summary>
        public abstract TimeSpan chopheprasomTS { get; set; }
		/// <summary>
		/// giờ. VD: onduty 4h => bd làm thêm: 4h30
		/// </summary>
        public abstract TimeSpan batdaulamthemTS { get; set; }
		public override string ToString() {
			
						return "\t" + ShiftCode + " \t" + ShiftID + ";Loai:" + LoaiCa  + "\ton:" + OnnDutyTS.ToString() + "\toff:" + OffDutyTS.ToString() + "\tOnIN:" + OnTimeInTS + "\tCutIN:" + CutInTS 
                            + "\tOnOUT" + OnTimeOutTS + "\tCutOUT" + CutOutTS + "\tbdTRE" + chophepvaotreTS+ "\tbdSOM" +  chopheprasomTS+ "\tbdOT" + batdaulamthemTS;
			
			//return MyUtility.GetAllValueOfObject(this);
		}

		/// <summary>
		/// 0: ca chuẩn; 1: ca tự do tính công dựa trên 8 tiếng sau giờ vào
		/// </summary>
        public abstract int LoaiCa { get; set; }

        public abstract int ShiftID { get; set; }

        public abstract string ShiftCode { get; set; }
		/// <summary>
		/// timespan of day
		/// </summary>
        public abstract TimeSpan OnnDutyTS { get; set; }
		/// <summary>
		/// timespan of day
		/// </summary>
        public abstract TimeSpan OffDutyTS { get; set; }
		/// <summary>
		/// = 1 nếu ca qua đêm, = 0 nếu ca ngày
		/// </summary>
        public abstract int DayCount { get; set; }

        public abstract bool QuaDem { get; set; }

		/// <summary>
		/// số phút
		/// </summary>
        public abstract TimeSpan OnTimeInTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
        public abstract TimeSpan OnTimeOutTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
        public abstract TimeSpan CutInTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
        public abstract TimeSpan CutOutTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
        public abstract TimeSpan AfterOTTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
        public abstract TimeSpan LateGraceTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
        public abstract TimeSpan EarlyGraceTS { get; set; }
		/// <summary>
		/// timespan
		/// </summary>
        public abstract TimeSpan WorkingTimeTS { get; set; }
		/// <summary>
		/// Công: 0.5, 1, 2
		/// </summary>
        public abstract Single Workingday { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
        public abstract TimeSpan LunchMinute { get; set; }

        public abstract int ShowPosition { get; set; }


		#endregion
	}
}
