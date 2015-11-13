using System;
using System.Collections.Generic;
using System.Data;
using ChamCong_v04.Helper;

namespace ChamCong_v04.DAL {
	public static partial class DAO {

		public static int UpdIns_ThuChiNVCongNhat(DateTime thang, int userEnrollNumber, int idPhong, string tenPhong, DateTime ngayBd, DateTime ngayKt, int donGiaLuong, double tamUng, bool LaNVChinhThuc)
		{
			string noidung = "Lưu ngày làm việc công nhật của NV có mã chấm công [{0}] bắt đầu làm từ ngày [{1}] đến hết ngày [{2}], loại NV [{3}]";
			noidung = string.Format(noidung, userEnrollNumber, ngayBd.ToString("dd/MM/yyyy"), ngayKt.ToString("dd/MM/yyyy"), LaNVChinhThuc ? "vừa tính lương chính thức vừa tính lương công nhật" : "làm việc công nhật");
			DAO.GhiNhatKyThaotac("Lưu thông tin làm việc công nhật", noidung, maCC: userEnrollNumber);
			var query = @"	
update DSNVChiCongNhatThang 
set IDPhong=@IDPhong, TenPhong=@TenPhong, NgayBatDau=@NgayBatDau, NgayKetThuc=@NgayKetThuc,DonGiaLuong= @DonGiaLuong, TamUng = @TamUng, NVChinhThuc = @NVChinhThuc
where UserEnrollNumber = @UserEnrollNumber and Thang=@Thang

if @@RowCount = 0
insert into DSNVChiCongNhatThang(Thang, UserEnrollNumber, IDPhong, TenPhong, NgayBatDau, NgayKetThuc, SoNgayCong, DonGiaLuong, TamUng, NVChinhThuc)
values (@Thang,  @UserEnrollNumber,  @IDPhong,  @TenPhong,  @NgayBatDau,  @NgayKetThuc,  @SoNgayCong,  @DonGiaLuong,  @TamUng, @NVChinhThuc) ";
			return SqlDataAccessHelper.ExecNoneQueryString(query,
														   new string[] { "@Thang", "@UserEnrollNumber", "@IDPhong", "@TenPhong", "@NgayBatDau", "@NgayKetThuc", "@SoNgayCong", "@DonGiaLuong", "@TamUng", "@NVChinhThuc" },
														   new object[] { thang, userEnrollNumber, idPhong, tenPhong, ngayBd, ngayKt, 0, donGiaLuong, tamUng, LaNVChinhThuc }); 
		}

		public static int UpdInsThongsoKetluongThang(DateTime ngayDauThang,
			int HSPCDem, int HSPCTangCuong, int HSPCTangCuong_Dem, int HSPC200, int HSPC260, int HSPC300, int HSPC390,
			float d01sanluong, float d02dongia, int perTrichQuyluong,
			int SanluongGiacongNoibo, int DongiaGiacongNoibo, int SanluongGiacongNgoai, int DongiaGiacongNgoai, int dluongtoithieu, /*int dDonGiaBD_ca3,*/ int dDinhMucComtrua,
			double QuyLuongCV, double QuyLuongNghiDinhCP, double ChiKhacTuQuyLuong, double QuyLuongTheoHeSoSanPham, double TienLuong1HeSoSP) {
//info thuộc log kết lương tháng
			#region QUERY

			string query = @" 
UPDATE ThongSoKetLuongThang  
SET 
  HSPCDem = @HSPCDem ,
  HSPCTangCuong = @HSPCTangCuong ,
  HSPCTangCuong_Dem = @HSPCTangCuong_Dem ,
  HSPC200 = @HSPC200 ,
  HSPC260 = @HSPC260 ,
  HSPC300 = @HSPC300 ,
  HSPC390 = @HSPC390 ,
  SanLuong = @SanLuong,
  DonGia = @DonGia,
  TrichQuyLuong = @TrichQuyLuong,
  SanLuongGiaCongNoiBo = @SanLuongGiaCongNoiBo,
  DonGiaGiaCongNoiBo = @DonGiaGiaCongNoiBo,
  SanLuongGiaCongNgoai = @SanLuongGiaCongNgoai,
  DonGiaGiaCongNgoai = @DonGiaGiaCongNgoai,
  MucLuongToiThieu = @MucLuongToiThieu,  
  DinhMucComTrua = @DinhMucComTrua,
  QuyLuongCV = @QuyLuongCV,
  QuyLuongNghiDinhCP = @QuyLuongNghiDinhCP,
  ChiKhacTuQuyLuong = @ChiKhacTuQuyLuong,
  QuyLuongTheoHeSoSanPham = @QuyLuongTheoHeSoSanPham,
  TienLuong1HeSoSP = @TienLuong1HeSoSP 
WHERE Thang = @Thang

if @@ROWCOUNT = 0
INSERT INTO ThongSoKetLuongThang
(
	Thang,
	HSPCDem,
	HSPCTangCuong,
	HSPCTangCuong_Dem,
	HSPC200,
	HSPC260,
	HSPC300,
	HSPC390,
	SanLuong,
	DonGia,
	TrichQuyLuong,
	SanLuongGiaCongNoiBo,
	DonGiaGiaCongNoiBo,
	SanLuongGiaCongNgoai,
	DonGiaGiaCongNgoai,
	MucLuongToiThieu,	
	DinhMucComTrua,
	QuyLuongCV,
	QuyLuongNghiDinhCP,
	ChiKhacTuQuyLuong,
	QuyLuongTheoHeSoSanPham,
	TienLuong1HeSoSP )
VALUES (
	@Thang,
	@HSPCDem,
	@HSPCTangCuong,
	@HSPCTangCuong_Dem,
	@HSPC200,
	@HSPC260,
	@HSPC300,
	@HSPC390,
	@SanLuong,
	@DonGia,
	@TrichQuyLuong,
	@SanLuongGiaCongNoiBo,
	@DonGiaGiaCongNoiBo,
	@SanLuongGiaCongNgoai,
	@DonGiaGiaCongNgoai,
	@MucLuongToiThieu,	
	@DinhMucComTrua,
	@QuyLuongCV,
	@QuyLuongNghiDinhCP,
	@ChiKhacTuQuyLuong,
	@QuyLuongTheoHeSoSanPham,
	@TienLuong1HeSoSP

)";
			#endregion

			return SqlDataAccessHelper.ExecNoneQueryString(query,
				new string[]{
					"@Thang",
					"@HSPCDem", "@HSPCTangCuong", "@HSPCTangCuong_Dem", "@HSPC200", "@HSPC260", "@HSPC300", "@HSPC390",
					"@SanLuong","@DonGia","@TrichQuyLuong",
					"@SanLuongGiaCongNoiBo","@DonGiaGiaCongNoiBo","@SanLuongGiaCongNgoai","@DonGiaGiaCongNgoai",
					"@MucLuongToiThieu","@DinhMucComTrua", 
					"@QuyLuongCV","@QuyLuongNghiDinhCP","@ChiKhacTuQuyLuong","@QuyLuongTheoHeSoSanPham","@TienLuong1HeSoSP"
				}, new object[]{
						ngayDauThang, 
						HSPCDem, HSPCTangCuong, HSPCTangCuong_Dem, HSPC200, HSPC260, HSPC300, HSPC390,
						d01sanluong, d02dongia, perTrichQuyluong, 
						SanluongGiacongNoibo, DongiaGiacongNoibo,SanluongGiacongNgoai,  DongiaGiacongNgoai,  
						dluongtoithieu, dDinhMucComtrua,
						QuyLuongCV,QuyLuongNghiDinhCP,ChiKhacTuQuyLuong,QuyLuongTheoHeSoSanPham,TienLuong1HeSoSP 
				});

		}

		public static DataTable LayThongsoKetluongThang(DateTime ngayDauThang) {
			return SqlDataAccessHelper.ExecuteQueryString(" select * from ThongSoKetLuongThang where Thang=@Thang ",
															new[] { "@Thang" }, new object[] { ngayDauThang });

		}

		public static DataTable LayTableCongNhat(DateTime ngayDauThang, int[] DSNV = null) {
			var query = @"	select	cn.*,
									nv.UserEnrollNumber, nv.UserFullName, nv.UserFullCode,
									case when (nv.UserIDD is null or nv.UserIDD = 0) then N'--' else d.Description end as TenPhong, 
 
									nv.UserIDTitle as IDChucVu, case when (nv.UserIDTitle is null or nv.UserIDTitle = 0)  then N'Chưa SX' else TitleName end as ChucVu
									
							FROM	DSNVChiCongNhatThang cn,
									UserInfo nv left join Title t on nv.UserIDTitle = t.IDT
									left join RelationDept d on nv.UserIDD = d.ID

							where	nv.UserEnrollNumber = cn.UserEnrollNumber 
								and Thang = @Thang 
								{0}  ";
			
			if (DSNV != null) {
				string joinString = string.Join(",", DSNV);
				query = string.Format(query, " AND cn.UserEnrollNumber in (" + joinString + ")");
			}
			else
			{
				query = string.Format(query, string.Empty);
			}

			return SqlDataAccessHelper.ExecuteQueryString(query, new[] { "@Thang" }, new object[] { ngayDauThang });
		}

		public static int InsKetCongCa(int UserEnrollNumber, DateTime Ngay, DateTime? TimeIn, DateTime? TimeOut,
			int? ShiftID, string ShiftCode, bool? DaXN, bool? DuyetCPVaoTre, bool? DuyetCPRaSom,
			bool? vaoTreLaCV, bool? raaSomLaCV,//ver 4.0.0.4	
			bool? BuGioTre, bool? BuGioSom, bool? BuPhepTre, float? CongBuPhepTre, bool? BuPhepSom, float? CongBuPhepSom,//ver 4.0.0.8
			float? CongTrongCa, float? CongNgoaiCa, float? TruCongTreVR, float? TruCongSomVR, float? TruCongTreBu, float? TruCongSomBu,
			int? OTMin, bool? QuaDem, string KyHieuCC,
			TimeSpan? gioLV, TimeSpan? tGLamthem, TimeSpan? tGLamdem, TimeSpan? tGthuc, TimeSpan? vaotre, TimeSpan? raasom,
			DateTime? tD_BD_LV, DateTime? tD_KT_LV_coOT, string thongtinCa, string shiftParams, int? haveINOUT, float? cong
			/*,string MoTaCa, TimeSpan OnDuty, TimeSpan OffDuty, int? DayCount, float WKTime, float WKDay, int LateMin, int EarlyMin, int AfterOT, int LunchMin*/) {
			//info thuộc log kết công bộ phận
			#region query

			var query = @"
INSERT INTO KetCongCa
( UserEnrollNumber,  Ngay,  TimeIn,  TimeOut,  ShiftID,  ShiftCode,  
CoXN,  DuyetCPVaoTre,  DuyetCPRaSom,  OTMin,  QuaDem,  KyHieuCC,  
VaoTreLaCV, RaSomLaCV,
BuGioTre, BuGioSom, BuPhepTre, CongBuPhepTre, BuPhepSom, CongBuPhepSom,
CongTrongCa, CongNgoaiCa, TruCongTreVR, TruCongSomVR, TruCongTreBu, TruCongSomBu,
TGLamViec,  TGLamThem,  TGLamDem,  TGThuc,  TGVaoTre,  TGRaSom,  
TD_BD_LV,  TD_KT_LV_CoOT,  ThongTinCa,  ShiftParams,  HaveINOUT,  Cong)
	 VALUES
(@UserEnrollNumber, @Ngay, @TimeIn, @TimeOut, @ShiftID, @ShiftCode, 
@CoXN, @DuyetCPVaoTre, @DuyetCPRaSom, @OTMin, @QuaDem, @KyHieuCC, 
@VaoTreLaCV, @RaSomLaCV,
@BuGioTre, @BuGioSom, @BuPhepTre, @CongBuPhepTre, @BuPhepSom, @CongBuPhepSom,
@CongTrongCa, @CongNgoaiCa, @TruCongTreVR, @TruCongSomVR, @TruCongTreBu, @TruCongSomBu,
@TGLamViec, @TGLamThem, @TGLamDem, @TGThuc, @TGVaoTre, @TGRaSom, 
@TD_BD_LV, @TD_KT_LV_CoOT, @ThongTinCa, @ShiftParams, @HaveINOUT, @Cong)
";//ver 4.0.0.4	VaoTreLaCV, RaSomLaCV
//ver 4.0.0.8			BuGioTre,  BuGioSom,  BuPhepTre,  CongBuPhepTre,  BuPhepSom,  CongBuPhepSom,
//ver 4.0.0.8			TruCongTreVR, TruCongSomVR, TruCongTreBu, TruCongSomBu,
			#endregion
			return SqlDataAccessHelper.ExecNoneQueryString(query,
														   new string[]
															   { 
"@UserEnrollNumber", "@Ngay", "@TimeIn", "@TimeOut", "@ShiftID", "@ShiftCode", 
"@CoXN", "@DuyetCPVaoTre", "@DuyetCPRaSom", "@OTMin", "@QuaDem", "@KyHieuCC", 
"@VaoTreLaCV", "@RaSomLaCV",//ver 4.0.0.4	
"@BuGioTre", "@BuGioSom", "@BuPhepTre", "@CongBuPhepTre", "@BuPhepSom", "@CongBuPhepSom",//ver 4.0.0.8
"@CongTrongCa", "@CongNgoaiCa", "@TruCongTreVR", "@TruCongSomVR", "@TruCongTreBu", "@TruCongSomBu",//ver 4.0.0.8
"@TGLamViec", "@TGLamThem", "@TGLamDem", "@TGThuc", "@TGVaoTre", "@TGRaSom", 
"@TD_BD_LV", "@TD_KT_LV_CoOT", "@ThongTinCa", "@ShiftParams", "@HaveINOUT", "@Cong"
															   },
															   new object[] { 
		UserEnrollNumber, Ngay, 
		TimeIn != null ? (object) TimeIn : DBNull.Value,TimeOut != null ? (object)TimeOut : DBNull.Value,
		ShiftID != null ? (object) ShiftID : DBNull.Value, ShiftCode, 
		DaXN != null ? (object) DaXN : DBNull.Value, 
		DuyetCPVaoTre != null ? (object) DuyetCPVaoTre : DBNull.Value, 
		DuyetCPRaSom != null ? (object) DuyetCPRaSom : DBNull.Value,
		OTMin != null ? (object) OTMin : DBNull.Value,
		QuaDem != null ? (object) QuaDem : DBNull.Value,
		KyHieuCC != null ? (object) KyHieuCC : DBNull.Value,
		vaoTreLaCV != null ? (object) vaoTreLaCV : DBNull.Value, //ver 4.0.0.4
		raaSomLaCV != null ? (object) raaSomLaCV : DBNull.Value,//ver 4.0.0.4
		BuGioTre != null ? (object) BuGioTre : DBNull.Value, //ver 4.0.0.8
		BuGioSom != null ? (object) BuGioSom : DBNull.Value, //ver 4.0.0.8 
		BuPhepTre != null ? (object) BuPhepTre : DBNull.Value, //ver 4.0.0.8 
		CongBuPhepTre != null ? (object) CongBuPhepTre : DBNull.Value, //ver 4.0.0.8 
		BuPhepSom != null ? (object) BuPhepSom : DBNull.Value, //ver 4.0.0.8 
		CongBuPhepSom != null ? (object) CongBuPhepSom : DBNull.Value, //ver 4.0.0.8
		CongTrongCa != null ? (object) CongTrongCa : DBNull.Value, //ver 4.0.0.8
		CongNgoaiCa != null ? (object) CongNgoaiCa : DBNull.Value, //ver 4.0.0.8
		TruCongTreVR != null ? (object) TruCongTreVR : DBNull.Value, //ver 4.0.0.8
		TruCongSomVR != null ? (object) TruCongSomVR : DBNull.Value, //ver 4.0.0.8
		TruCongTreBu != null ? (object) TruCongTreBu : DBNull.Value, //ver 4.0.0.8
		TruCongSomBu != null ? (object) TruCongSomBu : DBNull.Value, //ver 4.0.0.8
		gioLV != null ? (object) gioLV : DBNull.Value,
		tGLamthem != null ? (object) tGLamthem : DBNull.Value,
		tGLamdem != null ? (object) tGLamdem : DBNull.Value,
		tGthuc != null ? (object) tGthuc : DBNull.Value,
		vaotre != null ? (object) vaotre : DBNull.Value,
		raasom != null ? (object) raasom : DBNull.Value,
		tD_BD_LV != null ? (object) tD_BD_LV : DBNull.Value,
		tD_KT_LV_coOT != null ? (object) tD_KT_LV_coOT : DBNull.Value,
		thongtinCa != null ? (object) thongtinCa : DBNull.Value,
		shiftParams != null ? (object) shiftParams : DBNull.Value,
		haveINOUT != null ? (object) haveINOUT : DBNull.Value,
		cong != null ? (object) cong : DBNull.Value});
		}


		public static int DelKetCongCa_Ngay(DateTime ngaydauthang, DateTime ngaycuoithang, List<int> ds_macc) {
			//info log do kết công bộ phận thực hiện ( trước khi kết thì huỷ kết công)
			var query = @" 
delete from KetCongCa where Ngay >= @NgayDauThang and Ngay <= @NgayCuoiThang and UserEnrollNumber = @UserEnrollNumber
delete from KetCongNgay where Ngay >= @NgayDauThang and Ngay <= @NgayCuoiThang and UserEnrollNumber = @UserEnrollNumber ";
			int kq = 1;
			foreach (int UserEnrollNumber in ds_macc) {
				int result = SqlDataAccessHelper.ExecNoneQueryString(query,
																	 new string[] { "@NgayDauThang", "@NgayCuoiThang", "@UserEnrollNumber" },
																	 new object[] { ngaydauthang, ngaycuoithang, UserEnrollNumber });
			}
			return kq;
		}

		public static DataTable LayDSThuchiThang(DateTime mThang) {
			var query = @"
select	Thang, UserFullCode, UserFullName, UserInfo.UserEnrollNumber,
		TamUng, LuongDieuChinh, MucDongBHXH, ThuChiKhac
from	UserInfo, DSThuChiThang
where	UserInfo.UserEnrollNumber = DSThuChiThang.UserEnrollNumber
		and Thang = @Thang";
			return SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@Thang" }, new object[] { mThang });
		}

		public static int CapnhatThuchiThang(int UserEnrollNumber, DateTime thang, double luongdieuchinh, double tamung, double thuchikhac, float mucdongbhxh) {
//info thuộc log kết lương tháng
			var query = @"
update DSThuChiThang
set TamUng=@TamUng,
LuongDieuChinh = @LuongDieuChinh,
ThuChiKhac = @ThuChiKhac,
MucDongBHXH = @MucDongBHXH
where UserEnrollNumber = @UserEnrollNumber
and Thang = @Thang

if @@ROWCOUNT = 0
insert into DSThuChiThang( Thang, UserEnrollNumber, TamUng, LuongDieuChinh, ThuChiKhac, MucDongBHXH)
values ( @Thang, @UserEnrollNumber, @TamUng, @LuongDieuChinh, @ThuChiKhac, @MucDongBHXH) 
";
			return SqlDataAccessHelper.ExecNoneQueryString(query,
				new string[] { "@Thang", "@UserEnrollNumber", "@TamUng", "@LuongDieuChinh", "@ThuChiKhac", "@MucDongBHXH" },
				new object[] { thang, UserEnrollNumber, tamung, luongdieuchinh, thuchikhac, mucdongbhxh }); 
		}

		public static int InsKetCongNgay(int maCc, DateTime ngay, float tongCong, float tongNgayLV/*ver4.0.0.1*/, float tongPc, float pc30, float pc50, float pctcc3, float pc200, float pc260, float pc300, float pc390, float pccus, TimeSpan gioLamViec, TimeSpan lamThem, TimeSpan lamBanDem, TimeSpan gioThuc, bool QuaDem,
			float CongDinhMucDuoi8Tieng, float CongTichLuy, float TruCongTreVR,float TruCongSomVR, float TruCongTreBu,float TruCongSomBu,float CongBuPhepTre,float CongBuPhepSom//ver 4.0.0.8
			) {
//info thuộc log kết công bộ phận
			//ver4.0.0.1 có thêm TongNgayLV
			var query = @"
INSERT INTO KetCongNgay
(  UserEnrollNumber, Ngay, TongCong, TongNgayLV, TongPC, PCDem, PCTangCuong, PCTangCuong_Dem, 
PC200, PC260, PC300, PC390, PCCus, TGLamViec, TGLamThem, TGLamDem, TGThuc, IsOverNight,
CongDinhMucDuoi8Tieng, CongTichLuy, TruCongTreVR, TruCongSomVR, TruCongTreBu, TruCongSomBu, CongBuPhepTre, CongBuPhepSom) 
VALUES (
  @UserEnrollNumber, @Ngay, @TongCong, @TongNgayLV, @TongPC, @PCDem, @PCTangCuong, @PCTangCuong_Dem, 
@PC200, @PC260, @PC300, @PC390, @PCCus, @TGLamViec, @TGLamThem, @TGLamDem, @TGThuc, @IsOverNight,
@CongDinhMucDuoi8Tieng, @CongTichLuy, @TruCongTreVR, @TruCongSomVR, @TruCongTreBu, @TruCongSomBu, @CongBuPhepTre, @CongBuPhepSom)";
			//ver4.0.0.1 có thêm TongNgayLV
			//ver 4.0.0.8 CongDinhMucDuoi8Tieng, CongTichLuy, TruCongTreVR,TruCongSomVR, TruCongTreBu,TruCongSomBu,CongBuPhepTre,CongBuPhepSom
			return SqlDataAccessHelper.ExecNoneQueryString(query, new string[]
				{
  "@UserEnrollNumber", "@Ngay", "@TongCong", "@TongNgayLV", "@TongPC", "@PCDem", "@PCTangCuong", "@PCTangCuong_Dem", //ver4.0.0.1 có thêm TongNgayLV
  "@PC200", "@PC260", "@PC300", "@PC390", "@PCCus", "@TGLamViec", "@TGLamThem", "@TGLamDem", "@TGThuc", "@IsOverNight",
  "@CongDinhMucDuoi8Tieng", "@CongTichLuy", "@TruCongTreVR", "@TruCongSomVR", "@TruCongTreBu", "@TruCongSomBu", "@CongBuPhepTre", "@CongBuPhepSom"
				}, new object[]
					{
						maCc, ngay, tongCong, tongNgayLV, tongPc, pc30, pc50, pctcc3, //ver4.0.0.1 có thêm TongNgayLV
						pc200, pc260, pc300, pc390, pccus, gioLamViec, lamThem, lamBanDem, gioThuc, QuaDem,
						CongDinhMucDuoi8Tieng, CongTichLuy, TruCongTreVR, TruCongSomVR, TruCongTreBu, TruCongSomBu, CongBuPhepTre, CongBuPhepSom//ver 4.0.0.8
					});
		}

		public static DataTable LayKetcongNgay(DateTime ngaydauthang, DateTime ngaycuoithang, int[] DSNV = null) {
			var query = @"
SELECT	*
FROM	KetCongNgay
WHERE	Ngay >= @NgayDauThang and Ngay <= @NgayCuoiThang 
		{0}
order by UserEnrollNumber asc, Ngay asc
		";
			if (DSNV != null)
			{
				string joinString = string.Join(",", DSNV);
				query = string.Format(query, " AND UserEnrollNumber in (" + joinString + ")");
			}
			else {
				query = string.Format(query, string.Empty);
			}
			return SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@NgayDauThang", "@NgayCuoiThang" },
														  new object[] { ngaydauthang, ngaycuoithang });
		}
		public static DataTable LayKetcongCa(DateTime ngaydauthang, DateTime ngaycuoithang, int[] DSNV = null) {
			var query = @"
SELECT	*
FROM	KetCongCa
WHERE	Ngay >= @NgayDauThang and Ngay <= @NgayCuoiThang 
		{0}
order by UserEnrollNumber asc, TimeIn ";
			if (DSNV != null)
			{
				string joinString = string.Join(",", DSNV);
				query = string.Format(query, " AND UserEnrollNumber in (" + joinString + ")");
			}
			else {
				query = string.Format(query, string.Empty);
			}
			return SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@NgayDauThang", "@NgayCuoiThang" },
														  new object[] { ngaydauthang, ngaycuoithang });
		}


		public static DataTable LayDSNVKetluongThang(DateTime ngaydauthang, DateTime ngaycuoithang) {
			var query = @" 
select KetLuongThang.* , UserInfo.UserFullCode, UserInfo.UserFullName
from KetLuongThang , UserInfo
where Thang=@Thang 
and KetLuongThang.UserEnrollNumber = UserInfo.UserEnrollNumber
";
			return SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@Thang" }, new object[] { ngaydauthang });
		}


	}
}
