using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong_v03.DTO {
	public class cShiftSchedule {
		public int SchID;
		public string TenLichTrinh;
		public List<cCaAbs> DSCa = new List<cCaAbs>();
		public List<cCaAbs> DSCaMoRong = new List<cCaAbs>();
	}

	[Serializable]
	public class cCaChuan : cCaAbs {
		public cCaChuan() { }


		public override TimeSpan chophepTreTS { get; set; }
		public override TimeSpan chophepSomTS { get; set; }
		public override TimeSpan batdaulamthemTS { get; set; }
		public override int ID { get; set; }
		public override string Code { get; set; }
		public override TimeSpan OnnTS { get; set; }
		public override TimeSpan OffTS { get; set; }
		public override TimeSpan OnnInnTS { get; set; }
		public override TimeSpan OnnOutTS { get; set; }
		public override TimeSpan CutInnTS { get; set; }
		public override TimeSpan CutOutTS { get; set; }
		public override TimeSpan LateeMin { get; set; }
		public override TimeSpan EarlyMin { get; set; }
		public override TimeSpan AfterOTMin { get; set; }
		public override TimeSpan LunchMin { get; set; }
		public override TimeSpan WorkingTimeTS { get; set; }
		public override float Workingday { get; set; }
		public override int ShowPosition { get; set; }
		public override string KyHieuCC { get; set; }
		public override int DayCount { get; set; }
		public override bool QuaDem { get; set; }
		public override bool TachCa { get; set; }
		
/*
		public override string ToString() {

			return "\t" + Code + " \t" + ID + ";\ton:" + OnnTS.ToString() + "\toff:" + OffTS.ToString() + "\tOnIN:" + OnnInnTS + "\tCutIN:" + CutInnTS
				+ "\tOnOUT" + OnnOutTS + "\tCutOUT" + CutOutTS + "\tTRE" + chophepTreTS + "\tSOM" + chophepSomTS + "\tOT" + batdaulamthemTS;

			//return MyUtility.GetAllValueOfObject(this);
		}
*/


		public cCaChuan catruoc;
		public cCaChuan casauuu;


	}

	[Serializable]
	public class cCaTuDo : cCaAbs {

		#region Overrides of cShiftAbstract

		public override TimeSpan chophepTreTS { get; set; }
		public override TimeSpan chophepSomTS { get; set; }
		public override TimeSpan batdaulamthemTS { get; set; }
		public override int ID { get; set; }
		public override string Code { get; set; }
		public override TimeSpan OnnTS { get; set; }
		public override TimeSpan OffTS { get; set; }
		public override TimeSpan OnnInnTS { get; set; }
		public override TimeSpan OnnOutTS { get; set; }
		public override TimeSpan CutInnTS { get; set; }
		public override TimeSpan CutOutTS { get; set; }
		public override TimeSpan LateeMin { get; set; }
		public override TimeSpan EarlyMin { get; set; }
		public override TimeSpan AfterOTMin { get; set; }
		public override TimeSpan LunchMin { get; set; }
		public override TimeSpan WorkingTimeTS { get; set; }
		public override float Workingday { get; set; }
		public override int ShowPosition { get; set; }
		public override string KyHieuCC { get; set; }

		public override int DayCount { get; set; }
		public override bool QuaDem { get; set; }
		public override bool TachCa { get; set; }

		#endregion
	}

	[Serializable]
	public abstract class cCaAbs {
		#region Public Properties

		/// <summary>
		/// giờ. VD: onduty 7h30 => cho phép vào trễ: 7h40
		/// </summary>
		public abstract TimeSpan chophepTreTS { get; set; }
		/// <summary>
		/// giờ. VD: offduty 4h => cho phép ra sớm: 3h50
		/// </summary>
		public abstract TimeSpan chophepSomTS { get; set; }
		/// <summary>
		/// giờ. VD: onduty 4h => bd làm thêm: 4h30
		/// </summary>
		public abstract TimeSpan batdaulamthemTS { get; set; }

		public abstract int ID { get; set; }
		public abstract string Code { get; set; }

		public abstract TimeSpan OnnTS { get; set; }
		public abstract TimeSpan OffTS { get; set; }
		public abstract TimeSpan OnnInnTS { get; set; }
		public abstract TimeSpan OnnOutTS { get; set; }
		public abstract TimeSpan CutInnTS { get; set; }
		public abstract TimeSpan CutOutTS { get; set; }
		public abstract TimeSpan LateeMin { get; set; }
		public abstract TimeSpan EarlyMin { get; set; }
		public abstract TimeSpan AfterOTMin { get; set; }
		public abstract TimeSpan LunchMin { get; set; }
		public abstract TimeSpan WorkingTimeTS { get; set; }
		public abstract Single Workingday { get; set; }
		public abstract int ShowPosition { get; set; }
		public abstract string KyHieuCC { get; set; }
		public abstract int DayCount { get; set; }

		public abstract bool QuaDem { get; set; }



		public abstract bool TachCa { get; set; }

/*
		public override string ToString() {

			return "\t" + Code + " \t" + ID + ";\ton:" + OnnTS.ToString() + "\toff:" + OffTS.ToString() + "\tOnIN:" + OnnInnTS + "\tCutIN:" + CutInnTS
				+ "\tOnOUT" + OnnOutTS + "\tCutOUT" + CutOutTS + "\tTRE" + chophepTreTS + "\tSOM" + chophepSomTS + "\tOT" + batdaulamthemTS;

		}
*/


		#endregion
	}
}
