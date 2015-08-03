using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamCong_v06.Helper
{
	public enum ModeType
	{
		Them = 1,
		Sua = 0,
		Xoa = -1,
		Cancel = -9,
	}

	public enum SPName6 {
		#region RelationDept
		RelationDept_ThemPhong,
		RelationDept_SuaPhong,
		RelationDept_XoaPhong,
		#endregion
		#region
		Shift_DocTatCaShift,
		#endregion
	}

}