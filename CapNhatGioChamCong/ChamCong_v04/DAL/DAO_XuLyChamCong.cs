﻿using System;
using System.Collections.Generic;
using System.Data;
using ChamCong_v04.BUS;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;

namespace ChamCong_v04.DAL {
	public static partial class DAO {
		public static DataTable LayTableCIO_A(int[] ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT) {
			#region query
			var query = @"   select distinct	CheckInOut.UserEnrollNumber, TimeStr, MachineNo, Source, Them, IDGioGoc, Xoa, UserInfo.UserFullCode, UserInfo.UserFullName
								from	CheckInOut, UserInfo
								where	( (TimeStr between @BDVao and @KTVao and MachineNo % 2 = 1)
										or (TimeStr between @BDRaa and @KTRaa and MachineNo % 2 = 0) )
										and (Xoa is null or Xoa = 0)
										and ( IDXNCa_LamThem is null)
										and UserInfo.UserEnrollNumber = CheckInOut.UserEnrollNumber
										and ( CheckInOut.UserEnrollNumber = {0} )
								order by CheckInOut.UserEnrollNumber asc, TimeStr asc ";
			query = String.Format(query, String.Join(" or CheckInOut.UserEnrollNumber = ", ArrDSMaCC_Checked));
			#endregion

			var tableCIO_A = SqlDataAccessHelper.ExecuteQueryString(query
				, new[] { "@BDVao", "@KTVao", "@BDRaa", "@KTRaa" }
				, new object[] { ngayBD.Add(XL2._18h00), ngayKT.Add(XL2._04h30), ngayBD.AddDays(1d).Add(XL2._07h00), ngayKT.Add(XL2._18h00) });
			return tableCIO_A;
		}

		public static DataTable LayTableCIO_A(int UserEnrollNumber, DateTime ngayBD, DateTime ngayKT) {
			var arr = new int[] { UserEnrollNumber };
			return LayTableCIO_A(arr, ngayBD, ngayKT);
		}

		public static DataTable LayTableCIO_V(int[] ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT) {
			#region oringin
			/*
											FROM	XNCa_LamThem XN, CheckInOut CIO
								where	(Loai is null or Loai = 0)
										and (Xoa is null or Xoa = 0)
*/

			#endregion
			var query = @"   SELECT distinct	CIO.UserEnrollNumber
										,CIO.TimeStr, CIO.Source, CIO.MachineNo
										,XN.ID,ShiftID,DuyetChoPhepVaoTre, DuyetChoPhepRaSom,OTMin, Explain, Note
										,VaoTreLaCV, RaSomLaCV
										,BuGioTre, BuGioSom, BuPhepTre, CongBuPhepTre, BuPhepSom, CongBuPhepSom
										,CIO.Them, CIO.IDGioGoc, CIO.Xoa
								FROM	XNCa_LamThem XN, CheckInOut CIO
								where	(Xoa is null or Xoa = 0)
										and	CIO.IDXNCa_LamThem = XN.ID
										and ( (TimeStr between @BDVao and @KTVao and MachineNo % 2 = 1)
											or (TimeStr between @BDRaa and @KTRaa and MachineNo % 2 = 0) )
										and ( CIO.UserEnrollNumber = {0} )
								group by CIO.UserEnrollNumber
										,CIO.TimeStr, CIO.Source, CIO.MachineNo 
										,XN.ID,ShiftID,DuyetChoPhepVaoTre, DuyetChoPhepRaSom,OTMin, Explain, Note
										,VaoTreLaCV, RaSomLaCV
										,BuGioTre, BuGioSom, BuPhepTre, CongBuPhepTre, BuPhepSom, CongBuPhepSom
										,CIO.Them, CIO.IDGioGoc, CIO.Xoa
								order by CIO.UserEnrollNumber asc, ID asc , CIO.TimeStr asc";//ver 4.0.0.4	VaoTreLaCV, RaSomLaCV
			//ver 4.0.0.8	BuGioTre, BuGioSom, BuPhepTre, CongBuPhepTre, BuPhepSom, CongBuPhepSom			
			query = String.Format(query, String.Join(" or CIO.UserEnrollNumber = ", ArrDSMaCC_Checked));
			var tableCIO_V = SqlDataAccessHelper.ExecuteQueryString(query
				, new[] { "@BDVao", "@KTVao", "@BDRaa", "@KTRaa" }
				, new object[] { ngayBD.Add(XL2._18h00), ngayKT.Add(XL2._04h30), ngayBD.AddDays(1d).Add(XL2._07h00), ngayKT.Add(XL2._18h00) });
			return tableCIO_V;
		}

		public static DataTable LayTableCIO_V(int UserEnrollNumber, DateTime ngayBD, DateTime ngayKT) {
			var arr = new int[] { UserEnrollNumber };
			return LayTableCIO_V(arr, ngayBD, ngayKT);
		}

		public static DataTable LayTableXPVang(DateTime ngayBD, DateTime ngayKT, int[] ArrDSMaCC_Checked) {
			var query = @"   SELECT      ID, UserInfo.UserEnrollNumber, UserInfo.UserFullCode, UserInfo.UserFullName
											, TimeDate, LoaiVang.AbsentCode, LoaiVang.AbsentDescription
											, Thang, Nam, Absent.Workingday, Absent.WorkingTime 
								FROM        Absent, LoaiVang , UserInfo 
								WHERE      (Absent.AbsentCode = LoaiVang.AbsentCode)
										and (UserInfo.UserEnrollNumber = Absent.UserEnrollNumber)
										and (TimeDate >= @NgayBD and TimeDate <= @NgayKT)
										and ( UserInfo.UserEnrollNumber = {0} )
								ORDER BY    UserInfo.UserEnrollNumber ASC,Nam DESC, Thang DESC, TimeDate ASC ";
			query = String.Format(query, String.Join(" or UserInfo.UserEnrollNumber = ", ArrDSMaCC_Checked));
			var tableVang = SqlDataAccessHelper.ExecuteQueryString(query
																		 , new string[] { "@NgayBD", "@NgayKT" }
																		 , new object[] { ngayBD, ngayKT });
			return tableVang;
		}
		public static DataTable LayTableXPVang(DateTime ngayBD, DateTime ngayKT) {
			var query = @"   SELECT      ID, UserInfo.UserEnrollNumber, UserInfo.UserFullCode, UserInfo.UserFullName
											, TimeDate, LoaiVang.AbsentCode, LoaiVang.AbsentDescription
											, Thang, Nam, Absent.Workingday, Absent.WorkingTime 
								FROM        Absent, LoaiVang , UserInfo 
								WHERE      (Absent.AbsentCode = LoaiVang.AbsentCode)
										and (UserInfo.UserEnrollNumber = Absent.UserEnrollNumber)
										and (TimeDate >= @NgayBD and TimeDate <= @NgayKT)
								ORDER BY    UserInfo.UserEnrollNumber ASC,Nam DESC, Thang DESC, TimeDate ASC ";
			var tableVang = SqlDataAccessHelper.ExecuteQueryString(query
																		 , new string[] { "@NgayBD", "@NgayKT" }
																		 , new object[] { ngayBD, ngayKT });
			return tableVang;
		}

		public static DataTable LayTableXacNhanPC50(int[] Arr_MaCC, DateTime ngayBD, DateTime ngayKT) {
			var query = @"		SELECT      UserEnrollNumber, Ngay, TinhPC50
								FROM        XacNhanPC50
								WHERE       Ngay >= @NgayBD and Ngay <= @NgayKT and ( UserEnrollNumber = {0} )
								ORDER BY    UserEnrollNumber ASC, Ngay ASC ";
			query = String.Format(query, String.Join(" or UserEnrollNumber = ", Arr_MaCC));
			var table = SqlDataAccessHelper.ExecuteQueryString(query
																		 , new string[] { "@NgayBD", "@NgayKT" }
																		 , new object[] { ngayBD, ngayKT });
			return table;
		}

		public static DataTable LayTableXacNhanPCDB(int[] Arr_MaCC, DateTime ngayBD, DateTime ngayKT, bool Duyet) {
			var iDuyet = (Duyet) ? 1 : 0;
			var query = @"		SELECT      ID, UserEnrollNumber, Ngay, LoaiPC, PCNgay, PCDem, Duyet
								FROM        XacNhanPC
								WHERE       Duyet = @Duyet and Ngay >= @NgayBD and Ngay <= @NgayKT and ( UserEnrollNumber = {0} )
								ORDER BY    UserEnrollNumber ASC, Ngay ASC ";
			query = String.Format(query, String.Join(" or UserEnrollNumber = ", Arr_MaCC));
			var table = SqlDataAccessHelper.ExecuteQueryString(query
																		 , new string[] { "@NgayBD", "@NgayKT", "@Duyet" }
																		 , new object[] { ngayBD, ngayKT, iDuyet });
			return table;
		}

		public static DataTable DocNgayLe(DateTime ngayBD, DateTime ngayKT) {
			const string query = @"	select * from Holiday where HDate >= @NgayBD and  HDate <= @NgayKT";
			var table = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@NgayBD", "@NgayKT" }, new object[] { ngayBD, ngayKT });
			return table;
		}

		public static bool LoaiGioLienQuan(List<cCheck> kq1) {
			var query = String.Empty;
			var param = new string[kq1.Count];
			var obj = new object[kq1.Count];
			var dem = 0;
			foreach (cCheck o in kq1) {
				var UserEnrollNumber = o.MaCC;

				param[dem] = @"@time" + dem;
				obj[dem] = o.Time;
				query += @" update CheckInOut set Xoa = 1 where UserEnrollNumber = " + UserEnrollNumber;
				query += @" and TimeStr = @time" + dem;
				query += @" and MachineNo % 2 = " + (o.MachineNo % 2) + "  ";
				dem++;
			}
			if (param.Length == 0) return true;
			if (SqlDataAccessHelper.ExecNoneQueryString(query, param, obj) == 0)
				return false;
			return true;
		}

		public static bool ThemGio_ra3_vao1(List<cCheck> kq2) {
			var query = String.Empty;
			var param = new string[6 * kq2.Count];
			var obj = new object[6 * kq2.Count];
			var dem = 0;
			foreach (cCheck o in kq2) {
				if (o.Type == "I") {
					cCheck checkInn = o;
					param[6 * dem] = "@UserEnrollNumber" + dem;
					param[6 * dem + 1] = "@TimeDate" + dem;
					param[6 * dem + 2] = "@TimeStr" + dem;
					param[6 * dem + 3] = "@OriginType" + dem;
					param[6 * dem + 4] = "@MachineNo" + dem;
					param[6 * dem + 5] = "@Source" + dem;

					obj[6 * dem] = checkInn.MaCC;
					obj[6 * dem + 1] = checkInn.Time.Date;
					obj[6 * dem + 2] = checkInn.Time;
					obj[6 * dem + 3] = "I";
					obj[6 * dem + 4] = 21;
					obj[6 * dem + 5] = "PC";

					query += @"	insert into CheckInOut (UserEnrollNumber, TimeDate, TimeStr, OriginType, MachineNo, Source, WorkCode, Them)";
					var temp = string.Format(" values ( @UserEnrollNumber{0},  @TimeDate{0},  @TimeStr{0},  @OriginType{0},  @MachineNo{0},  @Source{0},  0, 1) ", dem);
					query += temp;
				}
				else {
					cCheck checkOut = o;
					param[6 * dem] = "@UserEnrollNumber" + dem;
					param[6 * dem + 1] = "@TimeDate" + dem;
					param[6 * dem + 2] = "@TimeStr" + dem;
					param[6 * dem + 3] = "@OriginType" + dem;
					param[6 * dem + 4] = "@MachineNo" + dem;
					param[6 * dem + 5] = "@Source" + dem;

					obj[6 * dem] = checkOut.MaCC;
					obj[6 * dem + 1] = checkOut.Time.Date;
					obj[6 * dem + 2] = checkOut.Time;
					obj[6 * dem + 3] = "O";
					obj[6 * dem + 4] = 22;
					obj[6 * dem + 5] = "PC";

					query += @"	insert into CheckInOut (UserEnrollNumber, TimeDate, TimeStr, OriginType, MachineNo, Source, WorkCode, Them) ";
					var temp = string.Format(" values ( @UserEnrollNumber{0},  @TimeDate{0},  @TimeStr{0},  @OriginType{0},  @MachineNo{0},  @Source{0},  0, 1) ", dem);
					query += temp;

				}
				dem++;
			}
			if (param.Length == 0) return true;
			if (SqlDataAccessHelper.ExecNoneQueryString(query, param, obj) == 0)
				return false;
			return true;

		}

		public static bool ThemGioChoNV(int UserEnrollNumber, DateTime GioMoi, string pKieuGioMoi, int MachineNoNew, string pLydo, string pGhichu) {
			var OriginTypeNew = pKieuGioMoi;
			const string query = @"   INSERT INTO CheckInOut (UserEnrollNumber, TimeStr, TimeDate, Source, MachineNo, OriginType, WorkCode, Them)
								VALUES     (@UserEnrollNumber, @TimeStrNew, @TimeDateNew, @SourceNew, @MachineNoNew, @OriginTypeNew, 0, 1) ";

			int kq = SqlDataAccessHelper.ExecNoneQueryString(query
															 , new string[] { "@UserEnrollNumber", "@TimeStrNew", "@TimeDateNew", "@SourceNew", "@MachineNoNew", "@OriginTypeNew"
																 ,"@Explain","@Note","@CommandType"}
															 , new object[] { UserEnrollNumber, GioMoi, GioMoi.Date, "PC", MachineNoNew, OriginTypeNew
																 , pLydo, pGhichu, 1});
			string noidung = "Thêm giờ [{0}] cho NV có mã chấm công [{1}] : giờ [{2}], lý do [{3}], ghi chú [{4}]";
			noidung = string.Format(noidung, ((pKieuGioMoi == "I") ? "VÀO" : "RA"), UserEnrollNumber, GioMoi.ToString("H:mm:ss dd/MM/yyyy"), pLydo, pGhichu);
			DAO.GhiNhatKyThaotac("Thêm giờ chấm công", noidung, maCC:UserEnrollNumber);
			if (kq == 0) return false;
			return true;
		}

		public static bool XoaGioChoNV(int pUserEnrollNumber, DateTime pGioCu, string pSourceOld, int pMachineNoOld, string pLydo, string pGhichu) {
			const string query = @"   UPDATE  CheckInOut 
									SET Xoa = 1
								WHERE   UserEnrollNumber = @UserEnrollNumber
										and TimeStr = @TimeStrOld
										and (MachineNo % 2 = @MachineNoOld % 2) ";
			int kq = SqlDataAccessHelper.ExecNoneQueryString(query
															 , new string[] { "@UserEnrollNumber", "@TimeStrOld", "@SourceOld", "@MachineNoOld",
																 "@Explain","@Note","@CommandType"}
															 , new object[] { pUserEnrollNumber, pGioCu, pSourceOld, pMachineNoOld,
																 pLydo, pGhichu, -1});

			string noidung = "Xóa giờ [{0}] của NV có mã chấm công [{1}] : giờ [{2}], máy số [{3}], nguồn [{4}], lý do [{5}], ghi chú [{6}]";
			noidung = string.Format(noidung, ((pMachineNoOld %2 ==1) ? "VÀO" : "RA"), pUserEnrollNumber, pGioCu.ToString("H:mm:ss dd/MM/yyyy"), pMachineNoOld, pSourceOld, pLydo, pGhichu);
			DAO.GhiNhatKyThaotac("Xóa giờ chấm công", noidung, maCC:pUserEnrollNumber);

			if (kq == 0) return false;
			return true;

		}

		public static bool SuaGioChoNV(int UserEnrollNumber, DateTime GioCuu, DateTime GioMoi, string SourceOld, string SourceNew, int MachineNoOld, int MachineNoNew, int idgiogoc, string Lydo, string Ghichu) {
			var kq = 0;

			var query = String.Empty;
			if (idgiogoc != -1) {
				query = @"   update  CheckInOut 
								set     TimeStr = @TimeStrNew, TimeDate = @TimeDateNew, Source = @SourceNew, MachineNo = @MachineNoNew
								where   UserEnrollNumber = @UserEnrollNumber 
										and TimeStr = @TimeStrOld
										and (MachineNo % 2 = @MachineNoOld % 2) ";
			}
			else {
				query = @"	declare @ID int
								insert into GioGoc (TimeStr, MachineNo, Source) 
								values				(@TimeStrOld, @MachineNoOld, @SourceOld)

								select @ID = @@Identity

								update  CheckInOut 
								set     TimeStr = @TimeStrNew, TimeDate = @TimeDateNew, Source = @SourceNew, MachineNo = @MachineNoNew, IDGioGoc = @ID
								where   UserEnrollNumber = @UserEnrollNumber 
										and TimeStr = @TimeStrOld
										and (MachineNo % 2 = @MachineNoOld % 2)								";


			}
			kq = SqlDataAccessHelper.ExecNoneQueryString(query
					, new string[] { "@UserEnrollNumber", "@TimeStrOld", "@TimeStrNew", "@TimeDateNew"
									, "@SourceOld", "@SourceNew", "@MachineNoOld", "@MachineNoNew"
									,"@Explain","@Note","@CommandType"}
					, new object[] { UserEnrollNumber, GioCuu, GioMoi, GioMoi.Date
									, SourceOld, SourceNew, MachineNoOld, MachineNoNew
									, Lydo, Ghichu, 0});
			string kieugiogoc = MachineNoOld%2 == 1 ? "VÀO" : "RA";
			string kieugiomoi = MachineNoNew%2 == 1 ? "VÀO" : "RA";
			string noidung =
				@"Sửa giờ [{0}] [{2}] sang giờ [{10}] [{3}] của NV có mã chấm công [{1}], nguồn gốc [{4}], nguồn mới [{5}], máy gốc [{6}], máy mới [{7}], lý do [{8}], ghi chú [{9}]";
			noidung = string.Format(noidung, kieugiogoc, UserEnrollNumber, GioCuu.ToString("H:mm:ss dd/MM/yyyy"), GioMoi.ToString("H:mm:ss dd/MM/yyyy"), SourceOld, SourceNew, MachineNoOld,
			                        MachineNoNew, Lydo, Ghichu, kieugiomoi);
			DAO.GhiNhatKyThaotac("Sửa giờ chấm công", noidung, maCC:UserEnrollNumber);
			if (kq == 0) return false;
			return true;

		}



		public static DataTable LayDSLoaiVang() {
			var query = @"	select * from LoaiVang";
			return SqlDataAccessHelper.ExecuteQueryString(query, null, null);
		}

		public static bool ThemNgayVang(IEnumerable<dynamic> list, float workingDay, float workingTime, string absentCode) {
			#region query
			var queryIns = @"	
IF EXISTS (	SELECT ID FROM Absent 
			WHERE AbsentCode=@AbsentCode and TimeDate=@TimeDate	and WorkingTime = 4 and WorkingTime = @WorkingTime )
	UPDATE Absent SET Workingday = @NewWorkingday, WorkingTime = @NewWorkingTime
	WHERE AbsentCode=@AbsentCode and TimeDate=@TimeDate 
			and WorkingTime = 4  and WorkingTime = @WorkingTime 
ELSE
		INSERT INTO Absent (UserEnrollNumber,   TimeDate,  AbsentCode, Thang, Nam, Workingday, WorkingTime) 
		VALUES		  (@UserEnrollNumber,  @TimeDate, @AbsentCode,@Thang,@Nam,@Workingday,@WorkingTime) 
";
			#endregion

			string noidung = "Thêm xin phép vắng [{0}] [{1}] ngày [{2}] cho NV có mã chấm công [{3}]";
			//info ghép 2 cái nửa thành 1
			foreach (dynamic obj in list) {
				int kq = SqlDataAccessHelper.ExecNoneQueryString(
					queryIns,
					new string[] { "@UserEnrollNumber", "@TimeDate", "@AbsentCode", "@Thang", "@Nam", 
						"@Workingday", "@WorkingTime", "@NewWorkingday", "@NewWorkingTime",  },
					new object[] { obj.MaCC, obj.NgayVang, absentCode, obj.NgayVang.Month, obj.NgayVang.Year, 
						workingDay, workingTime, 1f, 8f });
				DAO.GhiNhatKyThaotac("Thêm xin phép vắng", 
					string.Format(noidung, absentCode, workingDay.ToString("0.0"), ((DateTime)obj.NgayVang).ToString("dd/MM/yyyy"), obj.MaCC), maCC:(int)obj.MaCC);
			}

			return true;
		}
		public static bool ThemNgayVang(int MaCC, DateTime ngayVang, float workingDay, float workingTime, string absentCode) {
			#region query
			var queryIns = @"	
IF EXISTS (	SELECT ID FROM Absent 
			WHERE UserEnrollNumber = @UserEnrollNumber and AbsentCode=@AbsentCode and TimeDate=@TimeDate	
					and WorkingTime = 4 and WorkingTime = @WorkingTime )
	UPDATE Absent SET Workingday = @NewWorkingday, WorkingTime = @NewWorkingTime
	WHERE UserEnrollNumber = @UserEnrollNumber and AbsentCode=@AbsentCode and TimeDate=@TimeDate 
			and WorkingTime = 4  and WorkingTime = @WorkingTime 
ELSE
   INSERT INTO Absent (UserEnrollNumber,   TimeDate,  AbsentCode, Thang, Nam, Workingday, WorkingTime) 
		VALUES		  (@UserEnrollNumber,  @TimeDate, @AbsentCode,@Thang,@Nam,@Workingday,@WorkingTime) 
";
			#endregion

			//info ghép 2 cái nửa thành 1
			int kq = SqlDataAccessHelper.ExecNoneQueryString(
					queryIns,
					new string[] { "@UserEnrollNumber", "@TimeDate", "@AbsentCode", "@Thang", "@Nam", 
						"@Workingday", "@WorkingTime", "@NewWorkingday", "@NewWorkingTime",  },
					new object[] { MaCC, ngayVang, absentCode, ngayVang.Month, ngayVang.Year, 
						workingDay, workingTime, 1f, 8f });
			string noidung = "Thêm xin phép vắng [{0}] [{1}] ngày [{2}] cho NV có mã chấm công [{3}]";
			
			DAO.GhiNhatKyThaotac("Thêm xin phép vắng",
					string.Format(noidung, absentCode, workingDay.ToString("0.0"), (ngayVang).ToString("dd/MM/yyyy"), MaCC), maCC:MaCC);
			return true;
		}

		public static bool XoaNgayVangNV(List<DataRowView> arrRecord) { 
			var query = @"DELETE FROM Absent WHERE ID = {0}";
			var formatStr2 = "Xóa khai báo vắng [{0}] [{1}] ngày ngày [{2}] của NV có mã chấm công [{3}]";
			//query = String.Format(query, String.Join(" or ID = ", arrRecord));
			foreach (var rowView in arrRecord)
			{
				var id = (int) rowView["ID"];
				var absentCode = rowView["AbsentCode"].ToString();
				var wkday = (float) rowView["Workingday"];
				var ngay = (DateTime) rowView["TimeDate"];
				var macc = (int) rowView["UserEnrollNumber"];
				SqlDataAccessHelper.ExecNoneQueryString(string.Format(query, id) , null, null);
				DAO.GhiNhatKyThaotac("Xóa khai báo vắng", noidung: string.Format(formatStr2, absentCode, wkday.ToString("0.0"), ngay.ToString("H:mm:ss dd/MM/yyyy"), macc), maCC:macc);
			}

			return true;
		}



		public static DataTable LietKeNgayVangChoNV(List<int> arrDSNVCheck, DateTime ngayBD, DateTime ngayKT) {
			var query = @"	SELECT      ID, UserInfo.UserEnrollNumber, UserInfo.UserFullCode, UserInfo.UserFullName, TimeDate, 
										LoaiVang.AbsentCode, LoaiVang.AbsentDescription, Thang, Nam, Absent.Workingday, Absent.WorkingTime 
							 FROM        Absent, LoaiVang , UserInfo 
							 WHERE      (Absent.AbsentCode = LoaiVang.AbsentCode)
									and (UserInfo.UserEnrollNumber = Absent.UserEnrollNumber)
									and (TimeDate >= @NgayBD and TimeDate <= @NgayKT)
									and (UserInfo.UserEnrollNumber = {0} )
							 ORDER BY    UserInfo.UserEnrollNumber ASC,Nam DESC, Thang DESC, TimeDate ASC ";
			query = String.Format(query, String.Join(" or UserInfo.UserEnrollNumber = ", arrDSNVCheck.ToArray()));
			DataTable table = SqlDataAccessHelper.ExecuteQueryString(query
				, new string[] { "@NgayBD", "@NgayKT" }
				, new object[] { ngayBD, ngayKT });
			return table;
		}


		public static int CheckTinhPC50(int macc, DateTime ngay, bool giatri) {

			var query = @"	update		XacNhanPC50 set TinhPC50 = @giatri 
							where		Ngay = @Ngay and UserEnrollNumber = @UserEnrollNumber 
							if @@ROWCOUNT=0 
							insert into XacNhanPC50	(UserEnrollNumber, Ngay,TinhPC50)
							values (@UserEnrollNumber, @Ngay,@giatri)";
			var n = SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@giatri", "@Ngay", "@UserEnrollNumber" },
															new object[] { giatri, ngay, macc });
			string noidung = "Xác nhận [{0}] tính phụ cấp tăng cường ngày [{1}] cho NV có mã chấm công [{2}]";
			noidung = string.Format(noidung, giatri ? "CÓ" : "KHÔNG", ngay.ToString("dd/MM/yyyy"), macc);
			DAO.GhiNhatKyThaotac("Xác nhận phụ cấp tăng cường", noidung, maCC:macc);
			return n;
		}
		public static int UpdIns_TinhPCDB(int macc, DateTime ngay, int loai, int pcngay, int pcdem, string noidungLog) {
			var query = @"	
update		XacNhanPC set LoaiPC = @loai, Duyet = 1 
where		Ngay = @Ngay and UserEnrollNumber = @UserEnrollNumber 
if @@ROWCOUNT=0 
insert into XacNhanPC	(UserEnrollNumber, Ngay,LoaiPC,PCNgay, PCDem,Duyet)
values (@UserEnrollNumber, @Ngay,@loai,@PCNgay, @PCDem,1)";
			var n = SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@loai", "@Ngay", "@UserEnrollNumber", "@PCNgay", "@PCDem" },
															new object[] { loai, ngay, macc, pcngay, pcdem });

			string noidung = "Xác nhận [{0}] tính phụ cấp {1} ngày [{2}] cho NV có mã chấm công [{3}]";
			noidung = string.Format(noidung, "CÓ", noidungLog, ngay.ToString("dd/MM/yyyy"), macc);
			DAO.GhiNhatKyThaotac("Xác nhận phụ cấp làm việc ngày nghỉ, trực lễ, tết", noidung, maCC:macc);

			return n;
		}
		public static int DeleteTinhPCDB(int macc, DateTime ngay) {

			var query = @"	delete from	XacNhanPC where Ngay = @Ngay and UserEnrollNumber = @UserEnrollNumber";
			var n = SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@Ngay", "@UserEnrollNumber" }, new object[] { ngay, macc });

			string noidung = "Xác nhận [{0}] tính phụ cấp làm việc ngày nghỉ, trực lễ, tết ngày [{1}] cho NV có mã chấm công [{2}]";
			noidung = string.Format(noidung, "HUỶ", ngay.ToString("dd/MM/yyyy"), macc.ToString());
			DAO.GhiNhatKyThaotac("Xác nhận phụ cấp làm việc ngày nghỉ, trực lễ, tết", noidung, maCC: macc);
			return n;
		}


		public static int InsCa(string shiftcode, TimeSpan OnnDuty, TimeSpan OffDuty, int DayCount, int wktime, float wkday,
			int iOnnInn, int iCutInn, int iOnnOut, int iCutOut, int cptre, int cpsom, int afterot,
			string mota, string kyhieucc, TimeSpan OnnLun, TimeSpan OffLun, bool tachcadem, int shiftid1, int shiftid2, bool isenable, bool isextended) {
			var query = @"	insert into Shifts(ShiftCode, Onduty, Offduty, DayCount, WorkingTime, Workingday, 
												OnTimeIn, CutIn, OnTimeOut, CutOut, LateGrace, EarlyGrace, AfterOT,
												Description, KyHieuCC, OnLunch, OffLunch, 
												IsSplited, ShiftID1, ShiftID2, IsEnabled, IsExtended,

												  IsNightTime,  StartNT,  EndNT,  NoOutWT,  NoInWT,
												  IsLateGrace,  IsEarlyGrace,  IsOT,  OTlevel,  IsSunOT,  SunOTLevel,
												  IsBeforeOT,  BeforeOT,  IsAfterOT,  AfterOTTotal,  AfterOTDeduce,
												  BeforeOTTotal,  BeforeOTDeduce,  LevelOT1,  LevelOT2,  IsOTPoint,  OTPoint,
												  IsHolidayOT,  HolidayOTLevel,  IsLate,  IsEarly,  WKOTLevel,  IsWKOTLevel,  ShowPosition)
 
							values				(@shiftcode, @OnnDuty, @OffDuty, @DayCount, @wktime, @wkday, 
												@iOnnInn, @iCutInn, @iOnnOut, @iCutOut, @cptre, @cpsom, @afterot,
												@mota, @kyhieucc, @OnnLun, @OffLun, @tachcadem, @shiftid1, @shiftid2, @isenable, @isextended,
												0, N'22:00',N'06:00',0,0,
												1,1,0,1,0,1, 
												0 , 30 , 0 , 0 , 0, 
												0 , 0 , 0 , 0 , 0 , 0, 
												1 , 1 , 1 , 1 , 1 , 1, 1) ";
			return SqlDataAccessHelper.ExecNoneQueryString(query,
				new string[] {"@shiftcode", "@OnnDuty", "@OffDuty", "@DayCount", "@wktime", "@wkday", "@iOnnInn", "@iCutInn", "@iOnnOut", "@iCutOut", "@cptre", "@cpsom", "@afterot",
				"@mota", "@kyhieucc", "@OnnLun", "@OffLun", "@tachcadem", "@shiftid1", "@shiftid2", "@isenable", "@isextended"},
				new object[]
				{
					shiftcode, OnnDuty.ToString(@"hh\:mm"), OffDuty.ToString(@"hh\:mm"), DayCount, wktime, wkday, iOnnInn, iCutInn, iOnnOut, iCutOut, cptre, cpsom, afterot,
						   mota, kyhieucc, OnnLun.ToString(@"hh\:mm"), OffLun.ToString(@"hh\:mm"), 
						   tachcadem, (shiftid1 == -1) ? (object)DBNull.Value : shiftid1, (shiftid2 == -1) ? (object)DBNull.Value : shiftid2, isenable, isextended
				});


		}

		public static int UpdCa(int shiftid, string shiftcode, TimeSpan OnnDuty, TimeSpan OffDuty, int DayCount, int wktime, float wkday, int iOnnInn, int iCutInn, int iOnnOut, int iCutOut, int cptre, int cpsom, int afterot, string mota, string kyhieucc, TimeSpan OnnLun, TimeSpan OffLun, bool tachcadem, int shiftid1, int shiftid2, bool isenable, bool isextended) {
			var query = @"	update Shifts
							set ShiftCode= @shiftcode   , Onduty=  @OnnDuty  , Offduty=  @OffDuty  , DayCount= @DayCount , WorkingTime=  @wktime  , Workingday=  @wkday  , 
							OnTimeIn= @iOnnInn  , CutIn=  @iCutInn  , OnTimeOut= @iOnnOut , CutOut= @iCutOut  , LateGrace=  @cptre  , EarlyGrace=  @cpsom  , AfterOT=  @afterot ,
							Description=  @mota  , KyHieuCC=  @kyhieucc  , OnLunch=  @OnnLun , OffLunch= @OffLun  , 
							IsSplited=  @tachcadem  , ShiftID1=  @shiftid1  , ShiftID2=  @shiftid2  , IsEnabled=@isenable   , IsExtended= @isextended 
							where ShiftID = @ShiftID";
			return SqlDataAccessHelper.ExecNoneQueryString(query,
				new string[] {"@ShiftID", "@shiftcode", "@OnnDuty", "@OffDuty", "@DayCount", "@wktime", "@wkday", "@iOnnInn", "@iCutInn", "@iOnnOut", "@iCutOut", "@cptre", "@cpsom", "@afterot",
				"@mota", "@kyhieucc", "@OnnLun", "@OffLun", "@tachcadem", "@shiftid1", "@shiftid2", "@isenable", "@isextended"},
				new object[]
				{
					shiftid, shiftcode, OnnDuty.ToString(@"hh\:mm"), OffDuty.ToString(@"hh\:mm"), DayCount, wktime, wkday, iOnnInn, iCutInn, iOnnOut, iCutOut, cptre, cpsom, afterot,
						   mota, kyhieucc, OnnLun.ToString(@"hh\:mm"), OffLun.ToString(@"hh\:mm"), 
						   tachcadem, (shiftid1 == -1) ? (object)DBNull.Value : shiftid1, (shiftid2 == -1) ? (object)DBNull.Value : shiftid2, isenable, isextended
				});

		}

		public static int DelCa(int shiftid) {
			var query = @"	delete from Shifts where ShiftID = @ShiftID ";
			return SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@ShiftID" }, new object[] { shiftid });
		}

		public static void XacNhanCa(int maCc, DateTime timevao, int machineNoInn, string sourceInn,
			DateTime timeraa, int machineNoOut, string sourceOut,
			int ShiftID, string shiftCode, bool bDuyetCpTre, bool bDuyetCpSom, int soPhutLamThem, string lydo, string ghichu, out int idXacNhan, 
			bool bBuGioTre, bool bBuGioSom, bool bBuPhepTre, float fCongBuPhepTreCongDon, bool bBuPhepSom, float fCongBuPhepSomCongDon,//ver 4.0.0.8
			bool vaoTreLaCV, bool raaSomLaCV) {
			// insert vào bảng xác nhận trước để lấy id
			var queryInsXNCa = @"	INSERT INTO XNCa_LamThem(ShiftID,ShiftCode,DuyetChoPhepVaoTre, DuyetChoPhepRaSom,  OTMin,  Explain,  Note,
															BuGioTre, BuGioSom, BuPhepTre, CongBuPhepTre, BuPhepSom, CongBuPhepSom,
															VaoTreLaCV, RaSomLaCV) 
									VALUES (@ShiftID,@ShiftCode,@DuyetChoPhepVaoTre,@DuyetChoPhepRaSom,@OTMin,@Explain,@Note,
											@BuGioTre, @BuGioSom, @BuPhepTre, @CongBuPhepTre, @BuPhepSom, @CongBuPhepSom,
											@VaoTreLaCV, @RaSomLaCV)
									select ID = @@Identity";//ver 4.0.0.4	VaoTreLaCV, RaSomLaCV
			//ver 4.0.0.8		"@BuGioTre", "@BuGioSom", "@BuPhepTre", "@CongBuPhepTre", "@BuPhepSom", "@CongBuPhepSom",
			DataTable tableKQ1 = SqlDataAccessHelper.ExecuteQueryString(
				queryInsXNCa,
				new string[] { "@ShiftID", "@ShiftCode", "@DuyetChoPhepVaoTre", "@DuyetChoPhepRaSom", "@OTMin", "@Explain", "@Note", 
					"@BuGioTre", "@BuGioSom", "@BuPhepTre", "@CongBuPhepTre", "@BuPhepSom", "@CongBuPhepSom",//ver 4.0.0.8
					"@VaoTreLaCV", "@RaSomLaCV" },//ver 4.0.0.4	
				new object[] { ShiftID, shiftCode, bDuyetCpTre, bDuyetCpSom, soPhutLamThem, lydo, ghichu, 
					bBuGioTre, bBuGioSom, bBuPhepTre, fCongBuPhepTreCongDon, bBuPhepSom, fCongBuPhepSomCongDon,//ver 4.0.0.8
					vaoTreLaCV, raaSomLaCV });//ver 4.0.0.4	
			idXacNhan = Int32.Parse(tableKQ1.Rows[0][0].ToString());
			var queryUpd_CheckInOut = @"	UPDATE	CheckInOut  
											SET		IDXNCa_LamThem = @IDXNCa_LamThem
											WHERE	UserEnrollNumber = @UserEnrollNumber
												and	( (TimeStr = @TimeStrInn and Source = @SourceInn and MachineNo = @MachineNoInn)
												or	  (TimeStr = @TimeStrOut and Source = @SourceOut and MachineNo = @MachineNoOut) )";
			int kq = SqlDataAccessHelper.ExecNoneQueryString(
				queryUpd_CheckInOut,
				new string[] { "@IDXNCa_LamThem", "@UserEnrollNumber", "@TimeStrInn", "@SourceInn", "@MachineNoInn", "@TimeStrOut", "@SourceOut", "@MachineNoOut" },
				new object[] { idXacNhan, maCc, timevao, sourceInn, machineNoInn, timeraa, sourceOut, machineNoOut });

			//string noidung = @"Xác nhận ca [{0}] cho NV có mã chấm công [{1}], giờ vào [{2}], giờ ra [{3}], [{4}] cho phép vào trễ, [{5}] cho phép ra sớm, làm thêm [{6}] phút, [{9}] tính vào trễ, [{10}] tính ra sớm là chờ việc(nếu có), lý do: [{7}], ghi chú: [{8}]";//ver 4.0.0.4	
			string noidung = @"Xác nhận ca [{0}] cho NV có mã chấm công [{1}], giờ vào [{2}], giờ ra [{3}], [{4}] cho phép vào trễ, [{5}] cho phép ra sớm, làm thêm [{6}] phút, [{7}] tính vào trễ, [{8}] tính ra sớm là chờ việc(nếu có), [{9}] bù giờ vào trễ, [{10}] bù giờ ra sớm, [{11}] bù phép vào trễ [{12}], [{13}] bù phép ra sớm [{14}], lý do: [{15}], ghi chú: [{16}]";//ver 4.0.0.4	
			noidung = string.Format(noidung, shiftCode, maCc, timevao.ToString("H:mm:ss dd/MM/yyyy"), timeraa.ToString("H:mm:ss dd/MM/yyyy"), // 0 1 2 3 
				bDuyetCpTre ? "CÓ" : "KHÔNG", bDuyetCpSom ? "CÓ" : "KHÔNG", soPhutLamThem, vaoTreLaCV ? "CÓ" : "KHÔNG", raaSomLaCV ? "CÓ" : "KHÔNG",// 4 5 6 7 8
				bBuGioTre ? "CÓ" : "KHÔNG", bBuGioSom ? "CÓ" : "KHÔNG", //ver 4.0.0.8 //9 10
				bBuPhepTre ? "CÓ" : "KHÔNG", XL.XacDinhTGBuPhep(fCongBuPhepTreCongDon), //ver 4.0.0.8 //11 12
				bBuPhepSom ? "CÓ" : "KHÔNG", XL.XacDinhTGBuPhep(fCongBuPhepSomCongDon),//ver 4.0.0.8 //13 14
				lydo, ghichu);//ver 4.0.0.4	VaoTreLaCV, RaSomLaCV //15 16
			DAO.GhiNhatKyThaotac("Xác nhận ca", noidung, maCC:maCc);
		}

		public static bool KiemtraTonTaiLoaiVang(string absentCode, DateTime ngay) {
			var querySel = @"	select ID from Absent where AbsentCode = @AbsentCode and TimeDate = @TimeDate ";
			DataTable table = SqlDataAccessHelper.ExecuteQueryString(querySel,
																	 new string[] { "@AbsentCode", "@TimeDate" },
																	 new object[] { absentCode, ngay });
			return (table.Rows.Count > 0);
		}






	}
}
