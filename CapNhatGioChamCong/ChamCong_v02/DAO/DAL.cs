using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ChamCong_v02.DAO {
	public static class DAL {
		/// <summary>
		/// nhớ thêm điều kiện trước chuỗi này AND, OR ....CheckInOut.UserEnrollNumber = {0}
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="tenThuoctinh">chú ý nhớ sử dụng tên tiền tố phía trước CheckInOut.UserEnrollNumber hay UserInfo.UserEnrollNumber</param>
		/// <returns></returns>
		public static string TaoChuoiOR(List<int> ds, string tenThuoctinh) {
			string kq = String.Empty;
			kq += @"( ";
			kq += tenThuoctinh + @" = {0}";
			kq = String.Format(kq, String.Join(" or " + tenThuoctinh + " = ", ds.ToArray()));
			kq += @" )";
			return kq;

		}

		public static DataTable LayDSCa() {
			string query = @"	SELECT	ShiftID,ShiftCode,Onduty,Offduty,DayCount,OnTimeIn,OnTimeOut,CutIn,CutOut
										,OnLunch,OffLunch, DATEDIFF(MINUTE, 0, Shifts.Onduty) as OndutyMinute, DATEDIFF(MINUTE, 0, Shifts.Offduty) as OffdutyMinute
										,WorkingTime,Workingday,LateGrace,EarlyGrace,AfterOT,ShowPosition
								FROM	Shifts 
								group by Onduty,Offduty,ShiftID,ShiftCode,DayCount,OnTimeIn,OnTimeOut,CutIn,CutOut
										,OnLunch,OffLunch
										,WorkingTime,Workingday,LateGrace,EarlyGrace,AfterOT,ShowPosition";
			DataTable kq = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return kq;
		}
		public static DataTable LayDSLichTrinh() {
			string query = @"	SELECT	ID,SchID,    T1,T2,T3,T4,T5,T6,T7
								FROM	ShiftSch
								group by    SchID,             T1,T2,T3,T4,T5,T6,T7,    ID";
			DataTable kq = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return kq;
		}
		public static DataTable LogIn(string tempUsername, string passEncrypt) {
			string query = @"	select	UserAccount, UserID from NewUserAccount 
								where	UserAccount = @UserAccount and Password = @Password ";
			DataTable dt = SqlDataAccessHelper.ExecuteQueryString(query
				, new[] { "@UserAccount", "@Password" }, new object[] { tempUsername, passEncrypt });
			return dt;
		}

		public static DataTable PhanQuyenMenu(int userid) {
			#region query
			string query = @" select * from MenuPrivilege where MenuPrivilege.UserID = @UserID

                            order by MenuID ASC, IsYes ASC";
			#endregion
			DataTable table = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@UserID" }, new object[] { userid });
			return table;
		}

		public static DataTable LayDSNV(int[] arrIDPhongBan) {
			//note: lấy ds nhân viên đang enable và UserIDD > 0 vì UserIDD = 0 là chưa được phân vào phòng ban nào hết
			#region
			string query = @"   select  UserEnrollNumber,UserFullName,UserFullCode
	                                    ,HeSoLuongCB,HeSoLuongSP,u.SchID,SchName,HSBHCongThem
	                                    ,UserIDTitle
                                        ,   case when u.UserIDTitle = 0 then N'Chưa SX'
	                                        else TitleName
	                                        end as TitleName
	                                    ,d1.ID as IDD_1, d1.Description as Description_1, d1.TinhPC150, d1.TinhPCNgayLe
	                                    ,d2.ID as IDD_2, d2.Description as Description_2
	                                    --,d3.ID as IDD_3, d3.Description as Description_3
                                from    UserInfo u left join Title on u.UserIDTitle = Title.IDT
	                                    left join RelationDept d1 on u.UserIDD = d1.ID
	                                    left join RelationDept d2 on d1.RelationID = d2.ID
	                                    --left join RelationDept d3 on d2.RelationID = d3.ID
                                        left join Schedule on u.SchID = Schedule.SchID
                                where   UserEnabled = 1 and UserIDD > 0
                                        and ( UserIDD = {0} )
                                order by UserEnrollNumber asc ";
			query = String.Format(query, String.Join(" or UserIDD = ", arrIDPhongBan));
			#endregion

			DataTable table = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return table;
		}

		public static DataTable LayDSTatCaNV() {
			//note: lấy ds nhân viên đang enable và UserIDD > 0 vì UserIDD = 0 là chưa được phân vào phòng ban nào hết
			#region
			string query = @"   select  UserEnrollNumber,UserFullName,UserFullCode
	                                    ,HeSoLuongCB,HeSoLuongSP,u.SchID,SchName
	                                    ,UserIDTitle
                                        ,   case when u.UserIDTitle = 0 then N'Chưa SX'
	                                        else TitleName
	                                        end as TitleName
	                                    ,d1.ID as IDD_1, d1.Description as Description_1, d1.TinhPC150, d1.TinhPCNgayLe
	                                    ,d2.ID as IDD_2, d2.Description as Description_2
	                                    --,d3.ID as IDD_3, d3.Description as Description_3
                                from    UserInfo u left join Title on u.UserIDTitle = Title.IDT
	                                    left join RelationDept d1 on u.UserIDD = d1.ID
	                                    left join RelationDept d2 on d1.RelationID = d2.ID
	                                    --left join RelationDept d3 on d2.RelationID = d3.ID
                                        left join Schedule on u.SchID = Schedule.SchID
                                where   UserEnabled = 1 and UserIDD > 0                                        
                                order by UserEnrollNumber asc ";
			#endregion

			DataTable table = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return table;
		}

		public static DataTable LayDSPhong(int userID) {
			#region query

			string query = @"   SELECT  r1.ID as ID,r1.RelationID as RelationID,r1.Description as Description
                                        ,r2.ID as ID_2,r2.Description as Description_2
                                        ,r3.ID as ID_3,r3.Description as Description_3
                                FROM    RelationDept r1
                                        LEFT OUTER JOIN RelationDept r2 ON (r1.RelationID = r2.ID)
                                        LEFT OUTER JOIN RelationDept r3 ON (r2.RelationID = r3.ID)
                                        ,DeptPrivilege
                                WHERE   DeptPrivilege.UserID = @UserID
                                        AND DeptPrivilege.IsYes = 1
                                        AND DeptPrivilege.IDD = r1.ID  ";
			#endregion

			DataTable kq = SqlDataAccessHelper.ExecuteQueryString(query, new[] { "@UserID" }, new object[] { userID });

			return kq;

		}

		public static DataTable LayTableCIO_A(int[] ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT) {
			#region query
			string query = @"   select distinct	CheckInOut.UserEnrollNumber, TimeStr, MachineNo, Source, UserFullCode, UserFullName
                                from	CheckInOut, UserInfo
                                where	( (TimeStr between @BDVao and @KTVao and MachineNo % 2 = 1)
		                                or (TimeStr between @BDRaa and @KTRaa and MachineNo % 2 = 0) )
		                                and IDXacNhanCaVaLamThem is null 
										and UserInfo.UserEnrollNumber = CheckInOut.UserEnrollNumber
                                        and ( CheckInOut.UserEnrollNumber = {0} )
                                order by CheckInOut.UserEnrollNumber asc, TimeStr asc ";
			query = String.Format(query, String.Join(" or CheckInOut.UserEnrollNumber = ", ArrDSMaCC_Checked));
			#endregion

			DataTable tableCIO_A = SqlDataAccessHelper.ExecuteQueryString(query
				, new[] { "@BDVao", "@KTVao", "@BDRaa", "@KTRaa" }
				, new object[] { ngayBD, ngayKT, ngayBD, ngayKT });
			return tableCIO_A;
		}

		public static DataTable LayTableCIO_A(int UserEnrollNumber, DateTime ngayBD, DateTime ngayKT) {
			int[] arr = new int[] { UserEnrollNumber };
			return LayTableCIO_A(arr, ngayBD, ngayKT);
		}

		public static DataTable LayTableCIO_V(int[] ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT) {
			string query = @"   SELECT distinct	CIO.UserEnrollNumber
                                        ,CIO.TimeStr, CIO.Source, CIO.MachineNo
                                        ,XN.ID,ShiftID,ShiftCode,Onduty,Offduty
                                        ,LateGrace,EarlyGrace,AfterOT
                                        ,DayCount,WorkingTime,Workingday
                                        ,OTMin,TinhPC150,Note
                                FROM	XacNhanCaVaLamThem XN, CheckInOut CIO
                                where	CIO.IDXacNhanCaVaLamThem = XN.ID
                                        and ( (TimeStr between @BDVao and @KTVao and MachineNo % 2 = 1)
	                                        or (TimeStr between @BDRaa and @KTRaa and MachineNo % 2 = 0) )
                                        and ( CIO.UserEnrollNumber = {0} )
								group by CIO.UserEnrollNumber,XN.ID
										,CIO.TimeStr,CIO.MachineNo,CIO.Source
										,ShiftID,ShiftCode,Onduty,Offduty
										,LateGrace,EarlyGrace,AfterOT
										,DayCount,WorkingTime,Workingday
										,OTMin,TinhPC150,Note
                                order by CIO.UserEnrollNumber asc, ID asc , CIO.TimeStr asc";
			query = String.Format(query, String.Join(" or CIO.UserEnrollNumber = ", ArrDSMaCC_Checked));
			DataTable tableCIO_V = SqlDataAccessHelper.ExecuteQueryString(query
				, new[] { "@BDVao", "@KTVao", "@BDRaa", "@KTRaa" }
				, new object[] { ngayBD, ngayKT, ngayBD, ngayKT });
			return tableCIO_V;
		}

		public static DataTable LayTableCIO_V(int UserEnrollNumber, DateTime ngayBD, DateTime ngayKT) {
			int[] arr = new int[] { UserEnrollNumber };
			return LayTableCIO_V(arr, ngayBD, ngayKT);
		}

		public static DataTable LayTableVang(int[] ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT) {
			string query = @"   SELECT      ID, UserInfo.UserEnrollNumber, UserInfo.UserFullCode, UserInfo.UserFullName
                                            , TimeDate, LoaiVang.AbsentCode, LoaiVang.AbsentSymbol,LoaiVang.AbsentDescription
                                            , Thang, Nam, Absent.Workingday, Absent.WorkingTime 
                                FROM        Absent, LoaiVang , UserInfo 
                                WHERE      (Absent.AbsentCode = LoaiVang.AbsentCode)
                                        and (UserInfo.UserEnrollNumber = Absent.UserEnrollNumber)
                                        and (TimeDate between @NgayBD and @NgayKT)
                                        and ( UserInfo.UserEnrollNumber = {0} )
                                ORDER BY    UserInfo.UserEnrollNumber ASC,Nam DESC, Thang DESC, TimeDate ASC ";
			query = String.Format(query, String.Join(" or UserInfo.UserEnrollNumber = ", ArrDSMaCC_Checked));
			DataTable tableVang = SqlDataAccessHelper.ExecuteQueryString(query
																		 , new string[] { "@NgayBD", "@NgayKT" }
																		 , new object[] { ngayBD, ngayKT });
			return tableVang;
		}

		public static DataTable LayTableVang(int UserEnrollNumber, DateTime ngayBD, DateTime ngayKT) {
			int[] arr = new int[] { UserEnrollNumber };
			return LayTableVang(arr, ngayBD, ngayKT);
		}

		public static DataTable LayTableLamViecNgayNghi(int[] ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT) {
			// lấy danh sách ngày nghỉ đã được duyệt để tính công
			string query = @"   SELECT		ID, LamViecNgayNghi.UserEnrollNumber, UserFullName, Ngay, PCThem, PCDem, Duyet
					             FROM		LamViecNgayNghi, UserInfo
					             WHERE		( Ngay between @NgayBD and @NgayKT )  and ( LamViecNgayNghi.UserEnrollNumber = {0} )
							            and UserInfo.UserEnrollNumber =  LamViecNgayNghi.UserEnrollNumber
										and Duyet = 1
					             order by	LamViecNgayNghi.UserEnrollNumber asc, Ngay asc";
			query = String.Format(query, String.Join(" or LamViecNgayNghi.UserEnrollNumber = ", ArrDSMaCC_Checked));
			DataTable tableLamViecNgayNghi = SqlDataAccessHelper.ExecuteQueryString(query
				, new string[] { "@NgayBD", "@NgayKT" }
				, new object[] { ngayBD, ngayKT });
			return tableLamViecNgayNghi;
		}

		public static DataTable LayTableLamViecNgayNghi(int UserEnrollNumber, DateTime ngayBD, DateTime ngayKT) {
			int[] arr = new int[] { UserEnrollNumber };
			return LayTableLamViecNgayNghi(arr, ngayBD, ngayKT);
		}

		public static bool SuaGioChoNV(int pUserEnrollNumber, DateTime pGioCu, DateTime pGioMoi, bool pKieuGioMoi, string pSourceOld, int pMachineNoOld, int pUserID, string pLydo, string pGhichu) {
			int kq = 0;
			int pMachineNoNew = (pKieuGioMoi) ? 21 : 22;
			string query = @"   update  CheckInOut 
                                set     TimeStr = @TimeStrNew, TimeDate = @TimeDateNew, MachineNo = @MachineNoNew
					            where   UserEnrollNumber = @UserEnrollNumber 
                                        and TimeStr = @TimeStrOld
                                        and (MachineNo % 2 = @MachineNoOld % 2)
                                INSERT INTO LichSuSuaGioVaoRa (UserEnrollNumber
                                            , TimeStrOld, TimeStrNew, SourceOld, SourceNew, MachineNoOld, MachineNoNew
                                            , UserID, Explain, Note, CommandType, ExecuteTime ) 
								VALUES      (@UserEnrollNumber
                                            ,@TimeStrOld,@TimeStrNew,@SourceOld,@SourceOld,@MachineNoOld,@MachineNoNew
                                            ,@UserID,@Explain,@Note,@CommandType,GetDate() ) ";

			kq = SqlDataAccessHelper.ExecNoneQueryString(query
					, new string[] { "@UserEnrollNumber", "@TimeStrOld", "@TimeStrNew", "@TimeDateNew"
                                    , "@SourceOld", "@MachineNoOld", "@MachineNoNew"
                                    ,"@UserID","@Explain","@Note","@CommandType"}
					, new object[] { pUserEnrollNumber, pGioCu, pGioMoi, pGioMoi.Date
                                    , pSourceOld, pMachineNoOld, pMachineNoNew
                                    , pUserID, pLydo, pGhichu, 0});
			if (kq == 0) return false;
			return true;

		}

		public static bool ThemGioChoNV(int pUserEnrollNumber, DateTime pGioMoi, bool pKieuGioMoi, int pMachineNoNew, int pUserID, string pLydo, string pGhichu) {
			int kq = 0;
			string OriginTypeNew = (pKieuGioMoi) ? "I" : "O";
			string query = @"   INSERT INTO CheckInOut (UserEnrollNumber, TimeStr, TimeDate, Source, MachineNo, OriginType, WorkCode)
                                VALUES     (@UserEnrollNumber, @TimeStrNew, @TimeDateNew, @SourceNew, @MachineNoNew, @OriginTypeNew, 0)

                                INSERT INTO LichSuSuaGioVaoRa (UserEnrollNumber
                                            , TimeStrOld, TimeStrNew, SourceOld, SourceNew, MachineNoOld, MachineNoNew
                                            , UserID, Explain, Note, CommandType, ExecuteTime ) 
								VALUES      (@UserEnrollNumber
                                            ,@TimeStrNew,@TimeStrNew,@SourceNew,@SourceNew,@MachineNoNew,@MachineNoNew
                                            ,@UserID,@Explain,@Note,@CommandType,GetDate() ) ";

			kq = SqlDataAccessHelper.ExecNoneQueryString(query
					, new string[] { "@UserEnrollNumber", "@TimeStrNew", "@TimeDateNew", "@SourceNew", "@MachineNoNew", "@OriginTypeNew"
                                    ,"@UserID","@Explain","@Note","@CommandType"}
					, new object[] { pUserEnrollNumber, pGioMoi, pGioMoi.Date, "PC", pMachineNoNew, OriginTypeNew
                                    , pUserID, pLydo, pGhichu, 1});
			if (kq == 0) return false;
			return true;
		}

		public static bool XoaGioChoNV(int pUserEnrollNumber, DateTime pGioCu, string pSourceOld, int pMachineNoOld, int pUserID, string pLydo, string pGhichu) {
			int kq = 0;
			string query = @"   DELETE  CheckInOut 
                                WHERE   UserEnrollNumber = @UserEnrollNumber
                                        and TimeStr = @TimeStrOld
                                        and (MachineNo % 2 = @MachineNoOld % 2)

                                INSERT INTO LichSuSuaGioVaoRa (UserEnrollNumber
                                            , TimeStrOld, TimeStrNew, SourceOld, SourceNew, MachineNoOld, MachineNoNew
                                            , UserID, Explain, Note, CommandType, ExecuteTime ) 
								VALUES      (@UserEnrollNumber
                                            ,@TimeStrOld,@TimeStrOld,@SourceOld,@SourceOld,@MachineNoOld,@MachineNoOld
                                            ,@UserID,@Explain,@Note,@CommandType,GetDate() ) ";

			kq = SqlDataAccessHelper.ExecNoneQueryString(query
				, new string[] { "@UserEnrollNumber", "@TimeStrOld", "@SourceOld", "@MachineNoOld"
                                ,"@UserID","@Explain","@Note","@CommandType"}
				, new object[] { pUserEnrollNumber, pGioCu, pSourceOld, pMachineNoOld
                                , pUserID, pLydo, pGhichu, -1});
			if (kq == 0) return false;
			return true;

		}

		public static int ThemChamCongTay(int iUserEnrollNumber, DateTime timeBD, DateTime timeKT, int ShiftID, string ShiftCode, string onnduty, string offduty, int lategrace, int earlygrace, int afterot, int daycount, float wktime, float wkdayy, int sophutOT, bool tinhPC150, string lydo, string ghichu) {
			#region query
			string query = @"	declare   @ID int ; 
						INSERT INTO XacNhanCaVaLamThem
						( UserEnrollNumber,  ShiftID,  ShiftCode,
						  Onduty,  Offduty,  LateGrace,  EarlyGrace,  AfterOT,
						  DayCount,  WorkingTime,  Workingday,
						  TimeStrIn,  TimeStrOut,
						  OTMin,  TinhPC150,  Note ) 
						VALUES 
						( @UserEnrollNumber,  @ShiftID,  @ShiftCode,
						  @Onduty,  @Offduty,  @LateGrace,  @EarlyGrace,  @AfterOT,
						  @DayCount,  @WorkingTime,  @Workingday,
						  @TimeStrIn,  @TimeStrOut,
						  @OTMin,  @TinhPC150,  N'Chấm công tay' )

						select @ID = @@Identity 
  
						INSERT INTO CheckInOut
						( UserEnrollNumber,  TimeDate,  TimeStr,
						  OriginType, Source, MachineNo,  WorkCode,  IDXacNhanCaVaLamThem ) 
						VALUES 
						( @UserEnrollNumber,  @TimeDateIn,  @TimeStrIn,
						  N'I',  N'PC',  21, 0,  @ID)

						INSERT INTO CheckInOut
						( UserEnrollNumber,  TimeDate,  TimeStr,
						  OriginType, Source, MachineNo,  WorkCode,  IDXacNhanCaVaLamThem ) 
						VALUES 
						( @UserEnrollNumber,  @TimeDateOut,  @TimeStrOut,
						  N'O',  N'PC',  22, 0,  @ID)

						INSERT INTO LichSuSuaGioVaoRa
						( UserEnrollNumber,  TimeStrOld,  TimeStrNew,  
						  SourceOld,  SourceNew, MachineNoOld,  MachineNoNew,
						  UserID,  Explain,  Note,
						  ExecuteTime,  CommandType
						) 
						VALUES 
						( @UserEnrollNumber,  @TimeStrIn,  @TimeStrIn,
						  N'PC', N'PC', 21,21, 
						  @UserID,  @Explain,  @Note,
						  GetDate(),  1)

						INSERT INTO LichSuSuaGioVaoRa
						( UserEnrollNumber,  TimeStrOld,  TimeStrNew,  
						  SourceOld,  SourceNew, MachineNoOld,  MachineNoNew,
						  UserID,  Explain,  Note,
						  ExecuteTime,  CommandType
						) 
						VALUES 
						( @UserEnrollNumber,  @TimeStrOut,  @TimeStrOut,
						  N'PC', N'PC', 22,22, 
						  @UserID,  @Explain,  @Note,
						  GetDate(),  1)
						 ";
			#endregion
			return SqlDataAccessHelper.ExecNoneQueryString(query
									   , new string[] {"@UserEnrollNumber", "@ShiftID", "@ShiftCode", 
																					"@Onduty", "@Offduty", "@LateGrace", "@EarlyGrace", "@AfterOT", 
																					"@DayCount", "@WorkingTime", "@Workingday", 
																					"@OTMin", "@TinhPC150", 
																					"@TimeStrIn", "@TimeStrOut", "@TimeDateIn", "@TimeDateOut", 
																					"@Note", "@Explain", "@UserID"}
									   , new object[] {iUserEnrollNumber, ShiftID, ShiftCode,
																					onnduty, offduty, lategrace, earlygrace, afterot,
																					daycount, wktime, wkdayy,
																					sophutOT, tinhPC150,
																					timeBD, timeKT, timeBD.Date, timeKT.Date,
																					ghichu, lydo, ThamSo.currUserID
																	});



		}

		public static int XacNhanGio(int iUserEnrollNumber, int ShiftID, string ShiftCode
			, string onnduty, string offduty, int lategrace, int earlygrace, int afterot, int daycount
			, float wktime, float wkdayy, int sophutOT, bool tinhPC150
			, DateTime timestrInn, DateTime timestrOut, string SourceInn, string SourceOut, int MachineNoInn, int MachineNoOut) {
			#region query
			string query = @"	declare   @ID int ; 
						INSERT INTO XacNhanCaVaLamThem
						( UserEnrollNumber,  ShiftID,  ShiftCode,
						  Onduty,  Offduty,  LateGrace,  EarlyGrace,  AfterOT,
						  DayCount,  WorkingTime,  Workingday,
						  TimeStrIn,  TimeStrOut,
						  OTMin,  TinhPC150,  Note ) 
						VALUES 
						( @UserEnrollNumber,  @ShiftID,  @ShiftCode,
						  @Onduty,  @Offduty,  @LateGrace,  @EarlyGrace,  @AfterOT,
						  @DayCount,  @WorkingTime,  @Workingday,
						  @TimeStrIn,  @TimeStrOut,
						  @OTMin,  @TinhPC150,  @Note )

						select @ID = @@Identity 
  
						UPDATE	CheckInOut
						SET		IDXacNhanCaVaLamThem = @ID
						WHERE	UserEnrollNumber = @UserEnrollNumber
							AND	(MachineNo % 2 = @MachineNoIn % 2)
							AND	(TimeStr between @TimeStrIn and @TimeStrInn30)

						UPDATE	CheckInOut
						SET		IDXacNhanCaVaLamThem = @ID
						WHERE	UserEnrollNumber = @UserEnrollNumber
							AND (MachineNo % 2 = @MachineNoOut%2)
							AND (TimeStr between @TimeStrOut and @TimeStrOut30)

						INSERT INTO LichSuSuaGioVaoRa
						( UserEnrollNumber,  TimeStrOld,  TimeStrNew,  
						  SourceOld,  SourceNew, MachineNoOld,  MachineNoNew,
						  UserID,  Explain,  Note,
						  ExecuteTime,  CommandType
						) 
						VALUES 
						( @UserEnrollNumber,  @TimeStrIn,  @TimeStrIn,
						  @SourceInn, N'PC', @MachineNoIn,21, 
						  @UserID,  @Explain,  @Note,
						  GetDate(),  1)

						INSERT INTO LichSuSuaGioVaoRa
						( UserEnrollNumber,  TimeStrOld,  TimeStrNew,  
						  SourceOld,  SourceNew, MachineNoOld,  MachineNoNew,
						  UserID,  Explain,  Note,
						  ExecuteTime,  CommandType
						) 
						VALUES 
						( @UserEnrollNumber,  @TimeStrOut,  @TimeStrOut,
						  @SourceOut, N'PC', @MachineNoOut,22, 
						  @UserID,  @Explain,  @Note,
						  GetDate(),  1)
						 ";
			#endregion

			DateTime TimeStrInn30 = timestrInn.Add(ThamSo._30phut);
			DateTime TimeStrOut30 = timestrOut.Add(ThamSo._30phut);
			return SqlDataAccessHelper.ExecNoneQueryString(query
									   , new string[] {"@UserEnrollNumber", "@ShiftID", "@ShiftCode", 
																					"@Onduty", "@Offduty", "@LateGrace", "@EarlyGrace", "@AfterOT", 
																					"@DayCount", "@WorkingTime", "@Workingday", 
																					"@OTMin", "@TinhPC150", 
																					"@TimeStrIn", "@TimeStrOut", "@TimeStrInn30", "@TimeStrOut30", 
																					"SourceInn", "SourceOut", "@MachineNoIn", "@MachineNoOut",
																					"@Note", "@Explain", "@UserID"}
									   , new object[] {iUserEnrollNumber, ShiftID, ShiftCode,
																					onnduty, offduty, lategrace, earlygrace, afterot,
																					daycount, wktime, wkdayy,
																					sophutOT, tinhPC150,
																					timestrInn, timestrOut, TimeStrInn30, TimeStrOut30,
																					SourceInn, SourceOut, MachineNoInn, MachineNoOut,
																					"Xác nhận giờ chấm công", "", ThamSo.currUserID
																	});



		}

		public static bool KhaiBaoLVNgayNghiChoNV(int[] arrDSNVCheck, DateTime NgayKhaiBao, float PCThem, float PCDem, int currentUserID) {
			string query = @"	INSERT INTO LamViecNgayNghi (UserEnrollNumber, Ngay, PCThem, PCDem, UserID, Duyet)
								VALUES (@UserEnrollNumber, @Ngay, @PCThem, @PCDem,@UserID,     0    )";// mặc định số 0 là khai báo thì chưa được duyệt, chờ được duyệt
			for (int i = 0; i < arrDSNVCheck.Length; i++) {

				int UserEnrollNumber = arrDSNVCheck[i];
				int kqThuchien = SqlDataAccessHelper.ExecNoneQueryString(query,
																		 new string[] { "@UserEnrollNumber", "@Ngay", "@PCThem", "@PCDem", "@UserID" },
																		 new object[] { UserEnrollNumber, NgayKhaiBao, PCThem, PCDem, currentUserID });
				if (kqThuchien == 0)
					return false;
			}

			return true;
		}


		public static DataTable LietKeLamViecNgayNghiDaKhaiBao(int[] arrDSNVCheck, DateTime ngayBD, DateTime ngayKT) {
			#region query
			string query = @"SELECT		ID, LamViecNgayNghi.UserEnrollNumber, UserFullName, Ngay, PCThem, PCDem, Duyet, UserFullCode
							 FROM		LamViecNgayNghi, UserInfo
							 WHERE		( Ngay between @NgayBD and @NgayKT )  and ( LamViecNgayNghi.UserEnrollNumber = {0} )
									and UserInfo.UserEnrollNumber =  LamViecNgayNghi.UserEnrollNumber
							 order by	LamViecNgayNghi.UserEnrollNumber asc, Ngay asc";

			query = String.Format(query, String.Join(" or LamViecNgayNghi.UserEnrollNumber = ", arrDSNVCheck));
			#endregion
			DataTable table = SqlDataAccessHelper.ExecuteQueryString(query
				, new string[] { "@NgayBD", "@NgayKT" }
				, new object[] { ngayBD, ngayKT });
			return table;
		}

		public static bool XoaLamViecNgayNghiDaKhaiBao(DataRow[] arrRecord) {
			int kqThaotac = 0;
			bool flagError = false;
			string query = @"	DELETE FROM LamViecNgayNghi WHERE       ID = @ID";
			//duyet qua cac record, neu ngay nao da
			foreach (DataRow row in arrRecord) {
				int ID = (int)row["ID"];

				kqThaotac = SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@ID" }, new object[] { ID });
				if (kqThaotac == 0) {
					flagError = true;
					break;
				}
			}
			if (flagError == true) return false;
			return true;
		}

		internal static int DuyetLamViecNgayNghi(int id) {
			string query = "	update LamViecNgayNghi set Duyet = 1 where ID = @ID ";
			int kq = SqlDataAccessHelper.ExecNoneQueryString(query, new[] { "@ID" }, new object[] { id });
			return kq;
		}

		public static bool KhaiBaoNgayVangChoNV(int[] DSMaCC_MaNV_Checked, List<DateTime> DSNgayCheck, DataRowView row, int currentUserID) {
			#region query
			string query = @"	INSERT INTO Absent (UserEnrollNumber,   TimeDate,  AbsentCode, Thang, Nam, Workingday, WorkingTime, UserID) 
                                VALUES			  (@UserEnrollNumber,  @TimeDate, @AbsentCode,@Thang,@Nam,@Workingday,@WorkingTime,@UserID)";
			#endregion
			int kqThaotac = 0;
			bool flagError = false;
			float cong = (float)row["WorkingDay"];
			float workingtime = (float)row["WorkingTime"];
			for (int i = 0; i < DSMaCC_MaNV_Checked.Length; i++) {
				int maCC = DSMaCC_MaNV_Checked[i];
				foreach (DateTime ngay in DSNgayCheck) {
					kqThaotac = SqlDataAccessHelper.ExecNoneQueryString(query
						, new string[] { "@UserEnrollNumber", "@TimeDate", "@AbsentCode", "@Thang", "@Nam", "@Workingday", "@WorkingTime", "@UserID" }
						, new object[] { maCC, ngay.Date, row["AbsentCode"].ToString(), ngay.Month, ngay.Year, cong, workingtime, currentUserID });
					if (kqThaotac == 0) {
						flagError = true;
						break;
					}
				}
				if (flagError) break;
			}
			if (flagError) return false;
			return true;
		}

		public static bool XoaNgayVangNV(DataRow[] arrRecord) {
			int kqThaotac = 0;
			bool flagError = false;
			string query = @"DELETE FROM Absent WHERE       ID = @ID";
			foreach (DataRow row in arrRecord) {
				int ID = (int)row["ID"];
				kqThaotac = SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@ID" }, new object[] { ID });
				if (kqThaotac == 0) {
					flagError = true;
					break;
				}
			}
			if (flagError == true) return false;
			return true;
		}

		public static DataTable LietKeNgayVangChoNV(object[] arrDSNVCheck, DateTime ngayBD, DateTime ngayKT) {
			string query = @"SELECT      ID, UserInfo.UserEnrollNumber, UserInfo.UserFullCode, UserInfo.UserFullName, TimeDate, LoaiVang.AbsentCode, LoaiVang.AbsentSymbol,LoaiVang.AbsentDescription, Thang, Nam, Absent.Workingday, Absent.WorkingTime 
							 FROM        Absent, LoaiVang , UserInfo 
							 WHERE      (Absent.AbsentCode = LoaiVang.AbsentCode)
									and (UserInfo.UserEnrollNumber = Absent.UserEnrollNumber)
									and (TimeDate between @NgayBD and @NgayKT)
									and (UserInfo.UserEnrollNumber = {0} )
							 ORDER BY    UserInfo.UserEnrollNumber ASC,Nam DESC, Thang DESC, TimeDate ASC ";
			query = String.Format(query, String.Join(" or UserInfo.UserEnrollNumber = ", arrDSNVCheck));
			DataTable table = SqlDataAccessHelper.ExecuteQueryString(query
				, new string[] { "@NgayBD", "@NgayKT" }
				, new object[] { ngayBD, ngayKT });
			return table;
		}


		internal static bool GhiNhanDieuChinhLuong(int iMaCC, DateTime thang, decimal luong) {
			int kqThaotac = 0;
			bool flagError = false;
			#region query
			string query = @"   UPDATE DieuChinhLuongThangTruoc SET LuongDieuChinh = @LuongDieuChinh
								WHERE UserEnrollNumber = @UserEnrollNumber and Thang = @Thang and Nam=@Nam

								IF @@ROWCOUNT=0
									INSERT INTO DieuChinhLuongThangTruoc(UserEnrollNumber,Thang,Nam,LuongDieuChinh) 
									VALUES (@UserEnrollNumber,@Thang,@Nam,@LuongDieuChinh) ";
			#endregion
			kqThaotac = SqlDataAccessHelper.ExecNoneQueryString(query
				, new string[] { "@UserEnrollNumber", "@Thang", "@Nam", "@LuongDieuChinh" }
				, new object[] { iMaCC, thang.Month, thang.Year, luong });
			if (kqThaotac == 0) flagError = true;
			if (flagError == true) return false;
			return true;
		}

		internal static bool CapNhatHeSoLuong(int iMaCC, Single hslCB, Single hslSP, Single hsBHcongthem) {
			int kqThaotac = 0;
			bool flagError = false;
			#region query
			string query = @"   UPDATE UserInfo SET HeSoLuongCB = @HeSoLuongCB, HeSoLuongSP = @HeSoLuongSP, HSBHCongThem=@HSBHCongThem
								WHERE UserEnrollNumber = @UserEnrollNumber";
			#endregion
			kqThaotac = SqlDataAccessHelper.ExecNoneQueryString(query
				, new string[] { "@UserEnrollNumber", "@HeSoLuongCB", "@HeSoLuongSP", "@HSBHCongThem" }
				, new object[] { iMaCC, hslCB, hslSP, hsBHcongthem });
			if (kqThaotac == 0) flagError = true;
			if (flagError == true) return false;
			return true;
		}

		internal static DataTable DocNgayLe(DateTime ngayBD, DateTime ngayKT) {
			string query = @"	select * from Holiday where HDate between @NgayBD and @NgayKT";
			DataTable table = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@NgayBD", "@NgayKT" }, new object[] { ngayBD, ngayKT });
			return table;
		}

		internal static DataTable DocHSBHCongThem() {
			string query = @"	select UserEnrollNumber, UserFullCode, HSBHCongThem 
								from UserInfo 
								where HSBHCongThem > 0";
			DataTable table = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return table;
		}

		internal static DataTable LayDSLamViecNgayNghiChoDuyet() {
			string query = @"	select	ID , UserInfo.UserEnrollNumber, UserInfo.UserFullCode, UserInfo.UserFullName, Ngay, PCThem, PCDem, UserID 
								from	UserInfo, LamViecNgayNghi
								where	UserInfo.UserEnrollNumber = LamViecNgayNghi.UserEnrollNumber
										and Duyet = 0";
			DataTable table = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return table;
		}

		internal static DataTable SelLichSuSuaGioChamCong(int userID) {
			string query = @"	SELECT   UserInfo.UserEnrollNumber,UserInfo.UserFullName,UserFullCode
										 TimeStrOld,TimeStrNew,
										 SourceOld,SourceNew,MachineNoOld,MachineNoNew,
										 NewUserAccount.UserID,NewUserAccount.UserAccount, 
										 Explain,Note,ExecuteTime,CommandType
								 FROM    LichSuSuaGioVaoRa, UserInfo, NewUserAccount 
								 where   UserInfo.UserEnrollNumber = LichSuSuaGioVaoRa.UserEnrollNumber
									  and LichSuSuaGioVaoRa.UserID = NewUserAccount.UserID
									  and NewUserAccount.UserID = @UserID
								 order by ExecuteTime desc";
			DataTable table = SqlDataAccessHelper.ExecuteQueryString(query, new[] { "@UserID" }, new object[] { userID });
			return table;
		}

	}
}
