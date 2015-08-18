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
		Other = 2,
	}

	public enum SPName6 {
		#region RelationDept
		RelationDept_ThemPhongV6,
		RelationDept_SuaPhongV6,
		RelationDept_XoaPhongV6,
		RelationDept_DocPhongBanV6,
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
		#region NewUserAccount
		NewUserAccount_DocTatCaTaiKhoanV6,
		NewUserAccount_XoaTaiKhoanV6,
		NewUserAccount_DisableTaiKhoanV6,
		NewUserAccount_ChangePassV6,
		NewUserAccount_ThemTaiKhoanV6,
		NewUserAccount_CapNhatTaiKhoanV6,

		#endregion

		#region DeptPrivilege
		DeptPrivilege_DocPhongBanThaoTacV6,
		DeptPrivilege_InsUpdPhanQuyenV6,
		#endregion
		#region FunctionPrivilege
		FunctionPrivilege_DocPhanQuyenChucNangV6,
		FunctionPrivilege_InsUpd_PhanQuyenV6,

		#endregion

		#region 	UserInfo
		UserInfo_DocNhanVienChamCongV6,
		#endregion

		#region Function
		Function_DocChucNangV6,

		#endregion


		#region làm nhiều thao tác, bảng kết hợp
		Other_XoaLichTrinhV6,
		CheckInOut_DocCheckChuaXL,
		#endregion
	}

	public enum Field {
		UserEnrollNumber,
		UserFullName,
		UserFullCode,
		IDChucVu,
		ChucVu,
		SchID,
		SchName,
		UserIDDepartment,
		IDDepartment,

	}
	public enum FunctionListV6
	{
		Admin_QLLichTrinh = 1,
		Admin_QLPhong = 2,
		Admin_QLTaiKhoan = 3,
	}

}