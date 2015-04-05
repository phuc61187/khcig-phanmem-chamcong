using System;
using System.Collections.Generic;
using System.Data;
using ChamCong_v05.BUS;
using ChamCong_v05.DTO;
using ChamCong_v05.Helper;

namespace ChamCong_v05.DAL {
	public static partial class DAO5 {
		#region hàm sẽ bỏ, giữ lại để ko bị lỗi
		public static DataTable LayTableCIO_A(int[] Arr_MaCC, DateTime startTime, DateTime endTime) {
			return new DataTable();
		}
		public static DataTable LayTableCIO_V5(int[] tableArrayMaCC, DateTime ngayBD, DateTime ngayKT, bool Duyet = true) {
			return new DataTable();
		}
		public static DataTable LayTableXacNhanPCDB(int[] tableArrayMaCC, DateTime ngayBD, DateTime ngayKT,bool Duyet= true){
			return new DataTable();
	}
		public static DataTable LayTableXacNhanPC50(int[] tableArrayMaCC, DateTime ngayBD, DateTime ngayKT, bool Duyet = true) {
			return new DataTable();
		}
		public static DataTable LayTableXPVang(DateTime ngayBD_Bef2D, DateTime ngayKT_Aft2D, int[] Arr_MaCC, bool Duyet = true) {
			return new DataTable();
		}
		public static DataTable LayTableXPVang(DateTime ngaydauthang, DateTime ngaycuoithang) {
			return new DataTable();
		}
		#endregion

		public static DataTable LayDSCa() {
			const string query = @"	SELECT	ShiftID,ShiftCode,Onduty,Offduty,DayCount,OnTimeIn,OnTimeOut,CutIn,CutOut
										,OnLunch,OffLunch, DATEDIFF(MINUTE, 0, Shifts.Onduty) as OndutyMinute, DATEDIFF(MINUTE, 0, Shifts.Offduty) as OffdutyMinute
										,WorkingTime,Workingday,LateGrace,EarlyGrace,AfterOT,ShowPosition,KyHieuCC
										,Description, IsEnabled, IsExtended, IsSplited, ShiftID1, ShiftID2
										,StartNT,EndNT
								FROM	Shifts 
								group by Onduty,Offduty,ShiftID,ShiftCode,DayCount,OnTimeIn,OnTimeOut,CutIn,CutOut
										,OnLunch,OffLunch
										,WorkingTime,Workingday,LateGrace,EarlyGrace,AfterOT,ShowPosition,KyHieuCC
										,Description, IsEnabled, IsExtended, IsSplited, ShiftID1, ShiftID2
										,StartNT,EndNT"; //ver 4.0.0.4	StartNT,EndNT
			var kq = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return kq;
		}

		public static DataTable LayDSCa_FillCaTruocCaSau() {
			// điều kiện 1. đang sử dụng 2. ko phải ca mở rộng 3. công < 2
			const string query = @"	SELECT	ShiftID,ShiftCode
								FROM	Shifts 
								where IsEnabled = 1 and (IsExtended is null OR IsExtended = 0) and Workingday < 2";
			var kq = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return kq;
		}
		public static DataTable LayDSLichTrinh() {
			const string query = @"	SELECT		Schedule.SchID, Schedule.SchName, ShiftSch.T1, ShiftSch.T2, ShiftSch.T3, ShiftSch.T4, ShiftSch.T5, ShiftSch.T6, ShiftSch.T7
								FROM		Schedule, ShiftSch
								where		Schedule.SchID = ShiftSch.SchID 
								group by    Schedule.SchID, Schedule.SchName, ShiftSch.T1, ShiftSch.T2, ShiftSch.T3, ShiftSch.T4, ShiftSch.T5, ShiftSch.T6, ShiftSch.T7";
			var kq = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return kq;
		}
		public static DataTable LogIn(string tempUsername, string passEncrypt) {
			const string query = @"	select	UserAccount, UserID from NewUserAccount 
								where	UserAccount = @UserAccount and Password = @Password ";
			DataTable dt = SqlDataAccessHelper.ExecuteQueryString(query
				, new[] { "@UserAccount", "@Password" }
				, new object[] { tempUsername, passEncrypt }, 
				CanLog:false);
			return dt;
		}

		public static DataTable PhanQuyenMenu(int userid) {
			#region query
			const string query = @" select * from MenuPrivilege where MenuPrivilege.UserID = @UserID

							order by MenuID ASC, IsYes ASC";
			#endregion
			var table = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@UserID" }, new object[] { userid });
			return table;
		}



		public static int UpdSetting(int id, string value) {

			var query = @"	update Setting set Value = @Value where ID = @ID ";
			var kq = SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@ID", "@Value" }, new object[] { id, value });
			return kq;
		}





		public static void UpdInsKetCongBoPhan(DateTime thang, List<cPhongBan> dsPhongKetCong) {
			var query = @"
INSERT INTO NhatKyThaoTac(Loai,NoiDung,UserID,ThoiDiem,MaPhong)
VALUES (@Loai,@NoiDung,@UserID,GetDate(),@IDPHONG)

IF NOT EXISTS (SELECT * FROM KETCONGBOPHAN WHERE THANG = @THANG AND IDPHONG = @IDPHONG)
BEGIN
	INSERT INTO KETCONGBOPHAN (THANG, IDPHONG) VALUES (@THANG, @IDPHONG) 
END
";
			foreach (cPhongBan phong in dsPhongKetCong) {
				string noidung = string.Format("Kết công tháng [{0}] cho bộ phận [{1}] (mã phòng=[{2}]) ", thang.ToString("MM/yyyy"), phong.Ten, phong.ID);
				SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@THANG", "@IDPHONG", "@Loai","@NoiDung","@UserID", },
																 new object[] { thang, phong.ID, "Kết công bộ phận", noidung, XL2.currUserID, });
			}
		}

		public static DataTable LayDSPhongDaKetcong(DateTime ngaydauthang) {
			var query = @"
select KetCongBoPhan.IDPhong , RelationDept.ID, Description, ViTri, RelationID
from KetCongBoPhan, RelationDept
where Thang = @Thang and KetCongBoPhan.IDPhong = RelationDept.ID
order by ViTri desc
";
			return SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@Thang" }, new object[] { ngaydauthang });
		}

		public static DataTable KiemtraTinhtrangKetcongThang(DateTime ngaydauthang, int UserEnrollNumber) {
			var ngaycuoithang = new DateTime(ngaydauthang.Year, ngaydauthang.Month, DateTime.DaysInMonth(ngaydauthang.Year, ngaydauthang.Month));
			var query = @"	select * from KetCongNgay where 
							Ngay >= @NgayBD and Ngay <= @NgayKT and UserEnrollNumber = @UserEnrollNumber";
			return SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@NgayBD", "@NgayKT", "@UserEnrollNumber" },
				new object[] { ngaydauthang, ngaycuoithang, UserEnrollNumber });
		}

		public static DataTable LayKhoangThoigianCongnhat(DateTime ngaydauthang, int mUserEnrollNumber) {
			var ngaycuoithang = new DateTime(ngaydauthang.Year, ngaydauthang.Month, DateTime.DaysInMonth(ngaydauthang.Year, ngaydauthang.Month));
			var query = @"	select * from DSNVChiCongNhatThang 
							where	Thang=@Thang and UserEnrollNumber = @UserEnrollNumber ";
			return SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@Thang", "@UserEnrollNumber" }, new object[] { ngaydauthang, mUserEnrollNumber });
		}

		public static int InsKetLuongThang(DateTime Thang, cUserInfo nv) {
			//info đã thực hiện log. log do hàm gọi thực hiện
			#region query
			//DANGLAM
			var query = @"
	INSERT INTO KetLuongThang
	(Thang, UserEnrollNumber, HSLCB, HSLCV, HSBHCongThem,
	IDPhong, TenPhong, ViTriPhong, LevelDept, RelationIDDept,
	IDChucVu, ChucVu,
	TongCong, TongLe, TongPhep, TongH_CT_PT, TongQuaDem, TongCongCV, TongBHXH, TongRo, TongPTDT,
	TongPCDem, TongPCTC, TongPCTC_dem, TongPC200, TongPC260, TongPC300, TongPC390, TongPCCus, TongPC,
	LuongCB_TheoCongThucTe, LuongCB_TheoCheDoNghi, LuongCB_TheoCongCV, PCLuongCB, 
	LuongSP_TheoCongThucTe, LuongSP_TheoCheDoNghi, PCLuongSP, 
	LuongDieuChinh, BoiDuongCa3, 
	TamUng, MucDongBHXH, KhauTruBHXH, ThuChiKhac, TienComTrua, ThucLanh  ) 
	VALUES (
	@Thang, @UserEnrollNumber, @HSLCB, @HSLCV, @HSBHCongThem,
	@IDPhong, @TenPhong, @ViTriPhong, @LevelDept, @RelationIDDept,
	@IDChucVu, @ChucVu,
	@TongCong, @TongLe, @TongPhep, @TongH_CT_PT, @TongQuaDem, @TongCongCV, @TongBHXH, @TongRo, @TongPTDT,
	@TongPCDem, @TongPCTC, @TongPCTC_dem, @TongPC200, @TongPC260, @TongPC300, @TongPC390, @TongPCCus, @TongPC,
	@LuongCB_TheoCongThucTe, @LuongCB_TheoCheDoNghi, @LuongCB_TheoCongCV, @PCLuongCB, 
	@LuongSP_TheoCongThucTe, @LuongSP_TheoCheDoNghi, @PCLuongSP, 
	@LuongDieuChinh, @BoiDuongCa3, 
	@TamUng, @MucDongBHXH, @KhauTruBHXH, @ThuChiKhac, @TienComTrua, @ThucLanh  ) ";

			#endregion

			return SqlDataAccessHelper.ExecNoneQueryString(query,
														   new string[]//DANGLAM
															   {
"@Thang", "@UserEnrollNumber", "@HSLCB", "@HSLCV", "@HSBHCongThem",
"@IDPhong", "@TenPhong", "@ViTriPhong", "@LevelDept", "@RelationIDDept",
"@IDChucVu", "@ChucVu",
"@TongCong", "@TongLe", "@TongPhep", "@TongH_CT_PT", "@TongQuaDem", "@TongCongCV", "@TongBHXH", "@TongRo", "@TongPTDT",
"@TongPCDem", "@TongPCTC", "@TongPCTC_dem", "@TongPC200", "@TongPC260", "@TongPC300", "@TongPC390", "@TongPCCus", "@TongPC",
"@LuongCB_TheoCongThucTe", "@LuongCB_TheoCheDoNghi", "@LuongCB_TheoCongCV", "@PCLuongCB", 
"@LuongSP_TheoCongThucTe", "@LuongSP_TheoCheDoNghi", "@PCLuongSP", 
"@BoiDuongCa3", "@LuongDieuChinh", 
"@TamUng", "@MucDongBHXH", "@KhauTruBHXH", "@ThuChiKhac", "@TienComTrua" , "@ThucLanh"
															   },
														   new object[]
															   {
  Thang,nv.MaCC,nv.HeSo.LuongCB,nv.HeSo.LuongCV,nv.HeSo.BHCongThem_ChoGD_PGD,
  nv.PhongBan.ID,nv.PhongBan.Ten, nv.PhongBan.ViTri, 0/*leveldept*/, nv.PhongBan.idParent, //info xem lại nếu phòng ban null thì id và tên ntn--> đã giải quyết
																							//info id parent nếu null --> đã giải quyết
  nv.IDChucVu, nv.ChucVu,
  nv.ThongKeThang.Cong, nv.ThongKeThang.Le, nv.ThongKeThang.Phep, nv.ThongKeThang.H_CT_PT, nv.ThongKeThang.NgayQuaDem, nv.ThongKeThang.CongCV,nv.ThongKeThang.BHXH, nv.ThongKeThang.NghiRo, nv.ThongKeThang.PTDT,//DANGLAM
  nv.ThongKeThang.PhuCaps._30_dem,nv.ThongKeThang.PhuCaps._50_TC, nv.ThongKeThang.PhuCaps._100_TCC3, 
  nv.ThongKeThang.PhuCaps._100_LVNN_Ngay, nv.ThongKeThang.PhuCaps._150_LVNN_Dem, nv.ThongKeThang.PhuCaps._200_LeTet_Ngay, nv.ThongKeThang.PhuCaps._250_LeTet_Dem, nv.ThongKeThang.PhuCaps._Cus, nv.ThongKeThang.PhuCaps._TongPC,
  nv.chiTietLuong.LCB_Theo.CongThucTe, nv.chiTietLuong.LCB_Theo.CheDoNghi, nv.chiTietLuong.LCB_Theo.CongCV, nv.chiTietLuong.LCB_Theo.PhuCap,
  nv.chiTietLuong.LSP_Theo.CongThucTe, nv.chiTietLuong.LSP_Theo.CheDoNghi, nv.chiTietLuong.LSP_Theo.PhuCap, 
  nv.chiTietLuong.BoiDuongQuaDem, nv.chiTietLuong.LuongDieuChinh,
  nv.chiTietLuong.KhauTru.TamUng, nv.chiTietLuong.MucDongBHXH, nv.chiTietLuong.KhauTru.BHXH, nv.chiTietLuong.KhauTru.ThuChiKhac, nv.chiTietLuong.TienComTrua, nv.chiTietLuong.ThucLanh,

															   });

		}

		public static DataTable LayKetLuongThang(DateTime m_Thang) {
			var query = @"select KetLuongThang.*, UserInfo.UserFullName, UserInfo.UserFullCode
from KetLuongThang, UserInfo 
where Thang=@Thang and UserInfo.UserEnrollNumber = KetLuongThang.UserEnrollNumber";
			return SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@Thang" }, new object[] { m_Thang });
		}

		public static void GhiNhatKyThaotac(string loai, string noidung, int? maCC = null, int? maPhong = null)
		{
			var query =
				@"	INSERT INTO NhatKyThaoTac(Loai, NoiDung, UserID, ThoiDiem, UserEnrollNumber, MaPhong) VALUES (@Loai, @NoiDung, @UserID, GetDate(), @UserEnrollNumber, @MaPhong)"; //ver4.0.0.1 thêm mã phòng, mã NV
			SqlDataAccessHelper.ExecNoneQueryString(query, new string[] {"@Loai", "@NoiDung", "@UserID", /*ThoiDiem=GetDate,*/ "@UserEnrollNumber", "@MaPhong"},
				new object[] {loai, noidung, XL2.currUserID, maCC == null ? (object)DBNull.Value : maCC, maPhong == null ? (object)DBNull.Value : maPhong});
		}


	}
}
