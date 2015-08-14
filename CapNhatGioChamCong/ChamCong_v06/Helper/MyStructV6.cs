using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamCong_v06.Helper {
	public struct ID_Description
	{
		public int ID;
		public string Description;
	}
	public struct StartEnd
	{
		public DateTime Start;
		public DateTime End;
	}

	#region UserInfo
	public struct HeSo {
		public float LuongCB;
		public float LuongCV;
		//public float BHXH_YT_TN;
		public float BHCongThem_ChoGD_PGD;

		//private float baoHiemXaHoi_YTe_ThatNghiep;
		public float BHXH_YT_TN {
			get { return LuongCB + BHCongThem_ChoGD_PGD; }
		}
	}

	#endregion
}
