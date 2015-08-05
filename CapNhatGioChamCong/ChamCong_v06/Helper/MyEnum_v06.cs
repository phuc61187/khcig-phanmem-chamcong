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
		Shift_DeleteShift,
		Shift_InsertNewShift,
		#endregion

		#region

		Schedule_ThemLichTrinh,
		Schedule_DocLichTrinh,

		#endregion

		#region ShiftSch

		ShiftSch_DocDSCa,


		#endregion

		#region làm nhiều thao tác, bảng kết hợp
		Other_XoaLichTrinh,
		#endregion
	}

}