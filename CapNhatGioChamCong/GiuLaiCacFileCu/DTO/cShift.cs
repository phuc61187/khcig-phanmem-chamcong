using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiuLaiCacFileCu.DTO {
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
	public class cShift {
		#region Public Properties
		public cShift(){}
		/// <summary>
		/// giờ. VD: onduty 7h30 => cho phép vào trễ: 7h40
		/// </summary>
		public TimeSpan chophepvaotreTS { get; set; }
		/// <summary>
		/// giờ. VD: offduty 4h => cho phép ra sớm: 3h50
		/// </summary>
		public TimeSpan chopheprasomTS { get; set; }
		/// <summary>
		/// giờ. VD: onduty 4h => bd làm thêm: 4h30
		/// </summary>
		public TimeSpan batdaulamthemTS { get; set; }
		public override string ToString() {
			
						return "\t" + ShiftCode + " \t" + ShiftID + ";Loai:" + LoaiCa  + "\ton:" + OnnDutyTS.ToString() + "\toff:" + OffDutyTS.ToString() + "\tOnIN:" + OnTimeInTS + "\tCutIN:" + CutInTS 
                            + "\tOnOUT" + OnTimeOutTS + "\tCutOUT" + CutOutTS + "\tbdTRE" + chophepvaotreTS+ "\tbdSOM" +  chopheprasomTS+ "\tbdOT" + batdaulamthemTS;
			
			//return MyUtility.GetAllValueOfObject(this);
		}

		/// <summary>
		/// 0: ca chuẩn; 1: ca tự do tính công dựa trên 8 tiếng sau giờ vào
		/// </summary>
		public int LoaiCa { get; set; }

		public int ShiftID { get; set; }

		public string ShiftCode { get; set; }
		/// <summary>
		/// timespan of day
		/// </summary>
		public TimeSpan OnnDutyTS { get; set; }
		/// <summary>
		/// timespan of day
		/// </summary>
		public TimeSpan OffDutyTS { get; set; }
		/// <summary>
		/// = 1 nếu ca qua đêm, = 0 nếu ca ngày
		/// </summary>
		public int DayCount { get; set; }

		public bool QuaDem { get; set; }

		/// <summary>
		/// số phút
		/// </summary>
		public TimeSpan OnTimeInTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
		public TimeSpan OnTimeOutTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
		public TimeSpan CutInTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
		public TimeSpan CutOutTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
		public TimeSpan AfterOTTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
		public TimeSpan LateGraceTS { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
		public TimeSpan EarlyGraceTS { get; set; }
		/// <summary>
		/// timespan
		/// </summary>
		public TimeSpan WorkingTimeTS { get; set; }
		/// <summary>
		/// Công: 0.5, 1, 2
		/// </summary>
		public float Workingday { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
		public TimeSpan LunchMinute { get; set; }

		public int ShowPosition { get; set; }


		#endregion

	}
}
