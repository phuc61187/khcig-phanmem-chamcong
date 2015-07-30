using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamCong_v06.Helper {
	#region ENUM xuat bao bieu
	public enum CC {
		STT = 6,
		ten = 24,
		manv = 6,
		kyhieuchamcong = 8,
		congtrongngay = 5,
		pctrongngay = 5,
		kyhieuvang = 5,
		cong = 9,
		Le = 8,
		Phep = 8,
		cv = 9,
		bh = 8,
		hoc = 8,
		tongcong = 9,
		ca130 = 9,
		ca150 = 9,
		tcc3_100 = 8,
		LVNN = 8,
		pcCa3LVNN = 8,
		pcle_tet = 8,
		pcCa3_le = 8,
		pckhac = 8,
		nghiRo = 7,
		ptdt = 7,
	}

	public enum DD {
		STT = 6,
		TEN = 24,
		MANV = 6,
		TRANGTHAI = 25,
		LOAIVANG = 12,
		CA = 20,
		VAO1 = 12,
		VAO2 = 12,
		VAO3 = 12,
		RAA1 = 12,
		RAA2 = 12,
		RAA3 = 12,

	}
	public enum TRS {
		TEN = 24,
		MANV = 6,
		NGAY = 12,
		CA = 24,
		VAO1 = 12,
		VAO2 = 12,
		VAO3 = 12,
		RAA1 = 12,
		RAA2 = 12,
		RAA3 = 12,

	}

	public enum L {
		STT = 6,
		MANV = 6,
		HOTEN = 24,
		HSLUONGCB = 9,
		HSLUONGSP = 9,
		BANNGAY = 9,
		PHEP = 9,
		HOCHOPLE = 9,
		PTDOANTHE = 9,
		VIECRIENG = 9,
		QUADEM = 8,
		CV = 9,
		PC30 = 9,
		PC50 = 9,
		PCTCC3 = 9,
		PC100 = 9,
		PC160 = 9,
		PC200 = 9,
		PC290 = 9,
		TONGCONG = 9,
		TONGPC = 9,
		LUONGCB = 14,
		LUONGSP = 14,
		DIEUCHINH = 12,
		TONGLUONG = 14,
		PCLUONGCB = 14,
		PCLUONGSP = 14,
		BOIDUONGCA3 = 12,
		TONGPCLUONG = 14,
		TONGLUONGPC = 14,
		TAMUNG = 14,
		BHXH = 14,
		THUCHIKHAC = 14,
		TIENCOMTRUA = 14,
		THUCLANH = 14,
		KYNHAN = 5

	}
	public enum U {
		TEN = 24,
		MANV = 8,
		NGAY = 12,
		CA = 18,
		VAOTRE = 8,
		RAASOM = 8,
		VAO1 = 6,
		VAO2 = 6,
		VAO3 = 6,
		RAA1 = 6,
		RAA2 = 6,
		RAA3 = 6,
		STT = 6,
		TONGTRE = 14,
		TONGSOM = 14,
		TONGTRESOM = 14
	}
	public enum X {
		STT = 5,
		TEN = 25,
		CHUCVU = 25,
		DONGIALUONG = 15,
		NGAYCONG = 10,
		THANHTIEN = 15,
		TAMUNG = 15,
		THUCLANH = 15,
		KYNHAN = 10

	}
	#endregion

	public enum LoaiCongNhat {
		NVChinhThuc,
		NVCongNhat,
		NVCongNhatVaChinhThuc
	}
	public enum TrangThaiDiemDanh {
		VANG_NGHI = 0,
		VANG_LYDO = 3,
		DANGLAMVIEC = 1,
		DARAVE = 2,
		DARAVE_THIEUCC = -1
	}

	public enum Quyen {
		XemCong = 10011,// MoTa = "Xem Công" });
		ThemXoaSuaGioCC = 10012,// MoTa = "Thêm xoá sửa giờ chấm công" });
		XNCa_LamThem = 10033,// MoTa = "Xác nhận ca và làm thêm" });
		XN_PCTC = 10014,// MoTa = "Xác nhận phụ cấp tăng cường" });
		XN_PCDB = 10015,// MoTa = "Xác nhận phụ cấp làm việc ngày nghỉ, trực lễ, tết" });
		KetCongThang = 10016,// MoTa = "Kết công tháng" });
		DiemDanh = 10021,// MoTa = "Điểm danh Nhân viên" });
		ChamCongQL = 10031,// MoTa = "Chấm công tay cho Quản lý" });

		KhaiBaoVang = 20011,// MoTa = "Khai báo vắng cho Nhân viên" });

		SuaGioHangLoat = 30011,// MoTa = "Sửa giờ hàng loạt" });
		XemNhatKyThaoTac = 30012,// MoTa = "Xem lịch sử thao tác" });
		QuanLyNhiemVuNhanVien = 30013,
		XemTKeCongVaPCTheoNhiemVu = 30014,
		XemDSNhiemVu = 30015,

		QuanLyNV = 40011,// MoTa = "Quản lý Nhân viên" });

		KetLuong = 50011,// MoTa = "Kết lương và huỷ kết lương tháng" });

		//= 60011,// MoTa = "Đổi mật khẩu tài khoản" });// xem [2703_1]
		PhanQuyen = 70011,// MoTa = "Phân quyền" });
		CaiDatThongSo = 70012,// MoTa = "Cài đặt thông số" });
		TaoTKDangNhap = 70013,// MoTa = "Tạo tài khoản đăng nhập" });

	}
	public enum TGLamDemTheoQuyDinh {
		_22h00 = 1,
		_21h45 = 0,
	}

	public enum YesNoCancel {
		Yes = 1,
		Cancel = 0,
		No = -1,
	}

	public enum LoaiPhuCap
	{
		/// <summary>
		/// sử dụng HSPCTangCuongNgay, HSPCDem, HSPCTangCuongDem
		/// </summary>
		NgayThuong = 0,
		/// <summary>
		/// sử dụng HSPCNgay và HSPCDem
		/// </summary>
		NgayNghi = 1, 
		/// <summary>
		/// sử dụng HSPCNgay và HSPCDem
		/// </summary>
		NgayLe = 2,
		/// <summary>
		/// sử dụng HSPCNgay và HSPCDem
		/// </summary>
		TuyChinhNgayDem = 3,
		/// <summary>
		/// sử dụng HSPCNgay, HSPCTangCuongNgay, HSPCDem, HSPCTangCuongDem
		/// </summary>
		TuyChinhTatCa = 4,
	}

	public enum MayCheck
	{
		I = 21, O = 22,
	}

	public enum SettingName {
		PC30, PC100, PC200, PC160, PC50, PC290, PCTCC3,

		TGLamDemToiThieu,
		ChoPhepTre,
		ChoPhepSom,
		LamThemAfterOT,

		HSPCDem_NgayThuong,
		HSPCTangCuongNgay_NgayThuong,
		HSPCTangCuongDem_NgayThuong,
		HSPCNgay_NgayNghi,
		HSPCDem_NgayNghi,
		HSPCNgay_NgayLe,
		HSPCDem_NgayLe,
	}

	#region store procedure name
	public enum SPName {
		#region CheckInOut
		/// <summary>
		/// @ArrayMaCC IntArray readonly,	@BDVao datetime, @KTVao datetime,	@BDRaa datetime,	@KTRaa datetime
		/// </summary>
		CheckInOut_DocCheckVanTay, // đọc các check vân tay chưa qua xác nhận
		/// <summary>
		/// 	@ArrayMaCC IntArray readonly,	@BDVao datetime,	@KTVao datetime,	@BDRaa datetime,	@KTRaa datetime
		/// </summary>
		CheckInOut_DocCheckDaCoXN, // đọc các check vân tay chưa qua xác nhận
		/// <summary>
		/// @UserEnrollNumber, @TimeDate, @TimeStr, @OriginType, @MachineNo, @Source, workcode in sp =0, them in sp = 1
		/// </summary>
		CheckInOut_Ins,
		/// <summary>
		/// @UserEnrollNumber, @TimeDate, @TimeStr, @MachineNoOld, @SourceOld, @MachineNoNew , @SourceNew
		/// </summary>
		CheckInOut_InvertKieuChamCong,
		/// <summary>
		/// @UserEnrollNumber, @TimeStrOld, @MachineNoOld, @SourceOld, @OriginTypeNew, @TimeDateNew, @MachineNoNew, @SourceNew
		/// </summary>
		CheckInOut_Update,
		/// <summary>
		/// 
		/// </summary>
		CheckInOut_Delete,
		#endregion
		#region Absent
		/// <summary>
		/// @ArrayMaCC IntArray readonly,	@NgayBD datetime,	@NgayKT datetime
		/// </summary>
		Absent_DocNVVang,
		#endregion
		#region XacNhanPC50   va XacNhanPC
		/// <summary>
		/// @ArrayMaCC IntArray readonly,	@NgayBD datetime,	@NgayKT datetime
		/// </summary>
		XacNhanPC50_DocXNPC50,
		XacNhanPC_DocXNPC,
		XacNhanPhuCap5_DocBang,
		#endregion
		#region TableNhiemVu

		NhiemVu_DocBang,
		NhiemVu_InsUpd,
		NhiemVu_Del,

		#endregion
		#region RelationDept
		RelationDept_DocTatCaPhongBan,
		RelationDept_DocDSPhongDuocThaoTac,
		#endregion

		#region userinfo
		UserInfo_DocDSNVThaoTac,
		UserInfo_DocDSNVDkyNhiemVu,
		UserInfo_DocNhanVienNhanNhiemVu,
		UserInfo_DocDSNVThongKeCongVaPC,
		#endregion

		#region NhiemVu_NhanVien
		NhiemVu_NhanVien_INS,
		NhiemVu_NhanVien_DEL,
		#endregion

		#region other sp
		KiemTraKetLuongThang,
		ThongKeCongVaPhuCap,
		UserInfo_ChangePass,
		#endregion
	}
	#endregion

}
