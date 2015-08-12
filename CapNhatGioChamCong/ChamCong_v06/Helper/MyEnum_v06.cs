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
		#region Shift
		Shift_DocTatCaShiftV6,
		Shift_DeleteShiftV6,
		Shift_InsertNewShiftV6,
		Shift_DocDSCaEnableV6,
		#endregion

		#region Schedule

		Schedule_ThemLichTrinhV6,
		Schedule_DocLichTrinhV6,

		#endregion

		#region ShiftSch

		ShiftSch_DocDSCaV6,
		ShiftSch_ThemDSCaVaoLichTrinhV6,
		ShiftSch_XoaDSCaKhoiLichTrinhV6,


		#endregion
		#region NewAccount
		NewUserAccount_DocTatCaTaiKhoanV6,
		NewUserAccount_XoaTaiKhoanV6,
		#endregion

		#region DeptPrivilege
		DeptPrivilege_DocPhongBanThaoTacV6,
		#endregion

		#region 	UserInfo	
		NewUserAccount_ChangePassV6,
		#endregion
		#region làm nhiều thao tác, bảng kết hợp
		Other_XoaLichTrinhV6,
		#endregion
	}

}