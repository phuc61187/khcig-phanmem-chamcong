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

	public enum SPName6
	{
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

		#region CheckInCheckOut
		CheckInOut_DocCheckChuaXuLyV6,
		CheckInOut_LoaiCheck_KoHopLeV6,

		#endregion
		#region CIO
		CIO_ThemCIOChuaChamCongV6,
		#endregion

		#region làm nhiều thao tác, bảng kết hợp
		Other_XoaLichTrinhV6,
		#endregion
	}

	public enum Field
	{
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

	public enum LoaiTK
	{
		User = 1,
		LocalRoot = 0,
		None = -1,
	}
	public enum SettingName
	{
		PC30, PC100, PC200, PC160, PC50, PC290, PCTCC3,

		TGLamDemToiThieu,
		ChoPhepTre,
		ChoPhepSom,
		LamThemAfterOT,

		//todo bỏ
		//HSPCDem_NgayThuong,
		//HSPCTangCuongNgay_NgayThuong,
		//HSPCTangCuongDem_NgayThuong,
		//HSPCNgay_NgayNghi,
		//HSPCDem_NgayNghi,
		//HSPCNgay_NgayLe,
		//HSPCDem_NgayLe,

		HSPCDem,
		HSPCTangCuong,
		HSPCThem_NgayThuong,
		HSPCNgayNghi,
		//public static int HSPCDem,
		HSPCThem_NgayNghi,
		HSPCNgayLe,
		//public static int HSPCDem,
		HSPCThem_NgayLe,
	}
	public enum TrangThaiCheck
	{
		ThieuRa = -1,
		ThieuVao = -2,
		CheckDayDu = 0,
	}
}