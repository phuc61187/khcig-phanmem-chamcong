using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ChamCong_v03;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;

namespace ChamCong_v03.DAO {
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
			const string query = @"	SELECT	ShiftID,ShiftCode,Onduty,Offduty,DayCount,OnTimeIn,OnTimeOut,CutIn,CutOut
										,OnLunch,OffLunch, DATEDIFF(MINUTE, 0, Shifts.Onduty) as OndutyMinute, DATEDIFF(MINUTE, 0, Shifts.Offduty) as OffdutyMinute
										,WorkingTime,Workingday,LateGrace,EarlyGrace,AfterOT,ShowPosition,KyHieuCC
								FROM	Shifts 
								group by Onduty,Offduty,ShiftID,ShiftCode,DayCount,OnTimeIn,OnTimeOut,CutIn,CutOut
										,OnLunch,OffLunch
										,WorkingTime,Workingday,LateGrace,EarlyGrace,AfterOT,ShowPosition,KyHieuCC";
			var kq = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return kq;
		}
		public static DataTable LayDSLichTrinh() {
			const string query = @"	SELECT		Schedule.SchID, Schedule.SchName, ShiftSch.T1
								FROM		Schedule, ShiftSch
								where		Schedule.SchID = ShiftSch.SchID 
								group by    Schedule.SchID, Schedule.SchName, ShiftSch.T1";
			var kq = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return kq;
		}
		public static DataTable LogIn(string tempUsername, string passEncrypt) {
			const string query = @"	select	UserAccount, UserID from NewUserAccount 
								where	UserAccount = @UserAccount and Password = @Password ";
			DataTable dt = SqlDataAccessHelper.ExecuteQueryString(query
				, new[] { "@UserAccount", "@Password" }, new object[] { tempUsername, passEncrypt });
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

		public static DataTable LayDSNV(int[] arrIDPhongBan) {
			#region
			var query = @"   select  UserEnrollNumber,UserFullName,UserFullCode
	                                    ,HeSoLuongCB,HeSoLuongSP,u.SchID,SchName,HSBHCongThem
	                                    ,UserIDTitle, TinhLuongCongNhat
                                        ,   case when u.UserIDTitle = 0 then N'Chưa SX'
	                                        else TitleName
	                                        end as TitleName
	                                    ,d1.ID as IDD_1, d1.Description as Description_1, d1.TinhPC50, d1.ViTri
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

			var table = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return table;
		}
		public static DataTable LayDSNV_ThayDoiThongTin(int[] arrIDPhongBan) {
			#region
			var query = @"   select  UserEnrollNumber,UserFullName,UserFullCode
	                                    ,HeSoLuongCB,HeSoLuongSP,u.SchID,SchName,HSBHCongThem
	                                    ,UserIDTitle, TinhLuongCongNhat, UserEnabled
                                        ,   case when u.UserIDTitle = 0 then N'Chưa SX'
	                                        else TitleName
	                                        end as TitleName
	                                    ,d1.ID as IDD_1, d1.Description as Description_1, d1.TinhPC50, d1.ViTri
	                                    ,d2.ID as IDD_2, d2.Description as Description_2
	                                    --,d3.ID as IDD_3, d3.Description as Description_3
                                from    UserInfo u left join Title on u.UserIDTitle = Title.IDT
	                                    left join RelationDept d1 on u.UserIDD = d1.ID
	                                    left join RelationDept d2 on d1.RelationID = d2.ID
	                                    --left join RelationDept d3 on d2.RelationID = d3.ID
                                        left join Schedule on u.SchID = Schedule.SchID
                                where   UserIDD > 0
                                        and ( UserIDD = {0} )
                                order by UserEnrollNumber asc ";
			query = String.Format(query, String.Join(" or UserIDD = ", arrIDPhongBan));
			#endregion

			var table = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return table;
		}

		public static DataTable LayDSNV_TruCongNhat(int[] arrIDPhongBan) {
			#region
			var query = @"   select  UserEnrollNumber,UserFullName,UserFullCode
	                                    ,HeSoLuongCB,HeSoLuongSP,u.SchID,SchName,HSBHCongThem
	                                    ,UserIDTitle, TinhLuongCongNhat
                                        ,   case when u.UserIDTitle = 0 then N'Chưa SX'
	                                        else TitleName
	                                        end as TitleName
	                                    ,d1.ID as IDD_1, d1.Description as Description_1, d1.TinhPC50, d1.ViTri
	                                    ,d2.ID as IDD_2, d2.Description as Description_2
	                                    --,d3.ID as IDD_3, d3.Description as Description_3
                                from    UserInfo u left join Title on u.UserIDTitle = Title.IDT
	                                    left join RelationDept d1 on u.UserIDD = d1.ID
	                                    left join RelationDept d2 on d1.RelationID = d2.ID
	                                    --left join RelationDept d3 on d2.RelationID = d3.ID
                                        left join Schedule on u.SchID = Schedule.SchID
                                where   UserEnabled = 1 and UserIDD > 0 and (TinhLuongCongNhat is null or TinhLuongCongNhat = 0)
                                        and ( UserIDD = {0} )
                                order by UserEnrollNumber asc ";
			query = String.Format(query, String.Join(" or UserIDD = ", arrIDPhongBan));
			#endregion

			var table = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return table;
		}

		public static DataTable LayDSTatCaNV() {
			#region
			const string query = @"   select  UserEnrollNumber,UserFullName,UserFullCode
	                                    ,HeSoLuongCB,HeSoLuongSP,u.SchID,SchName,HSBHCongThem
	                                    ,UserIDTitle, TinhLuongCongNhat
                                        ,   case when u.UserIDTitle = 0 then N'Chưa SX'
	                                        else TitleName
	                                        end as TitleName
	                                    ,d1.ID as IDD_1, d1.Description as Description_1, d1.TinhPC50, d1.ViTri
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

			var table = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return table;
		}

		public static DataTable LayDSTatCaNV_TruCongNhat() {
			#region
			const string query = @"   select  UserEnrollNumber,UserFullName,UserFullCode
	                                    ,HeSoLuongCB,HeSoLuongSP,u.SchID,SchName,HSBHCongThem
	                                    ,UserIDTitle, TinhLuongCongNhat
                                        ,   case when u.UserIDTitle = 0 then N'Chưa SX'
	                                        else TitleName
	                                        end as TitleName
	                                    ,d1.ID as IDD_1, d1.Description as Description_1, d1.TinhPC50, d1.ViTri
	                                    ,d2.ID as IDD_2, d2.Description as Description_2
	                                    --,d3.ID as IDD_3, d3.Description as Description_3
                                from    UserInfo u left join Title on u.UserIDTitle = Title.IDT
	                                    left join RelationDept d1 on u.UserIDD = d1.ID
	                                    left join RelationDept d2 on d1.RelationID = d2.ID
	                                    --left join RelationDept d3 on d2.RelationID = d3.ID
                                        left join Schedule on u.SchID = Schedule.SchID
                                where   UserEnabled = 1 and UserIDD > 0 and (TinhLuongCongNhat is null or TinhLuongCongNhat = 0)                                      
                                order by UserEnrollNumber asc ";
			#endregion

			var table = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return table;
		}

		public static DataTable LayDSPhong(int userID) {
			#region query
			const string query = @"   SELECT  r1.ID as ID,r1.RelationID as RelationID,r1.Description as Description, r1.ViTri
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

			var kq = SqlDataAccessHelper.ExecuteQueryString(query, new[] { "@UserID" }, new object[] { userID });

			return kq;

		}

		public static DataTable LayDSTatCaPhongBan() {
			#region query
			const string query = @"   SELECT  r1.ID as ID,r1.RelationID as RelationID,r1.Description as Description, r1.ViTri                                        
                                FROM    RelationDept r1                                         ";
			#endregion

			var kq = SqlDataAccessHelper.ExecuteQueryString(query, null, null);

			return kq;

		}

		public static DataTable LayTableCIO_A(int[] ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT) {
			#region OriginType
/*
			or (TimeStr between @BDRaa and @KTRaa and MachineNo % 2 = 0) )
		                                and (Loai is null or Loai = 0)
										and (Xoa is null or Xoa = 0)
*/
			#endregion 
			#region query
			var query = @"   select distinct	CheckInOut.UserEnrollNumber, TimeStr, MachineNo, Source, Them, IDGioGoc, Xoa
                                from	CheckInOut, UserInfo
                                where	( (TimeStr between @BDVao and @KTVao and MachineNo % 2 = 1)
		                                or (TimeStr between @BDRaa and @KTRaa and MachineNo % 2 = 0) )
										and (Xoa is null or Xoa = 0)
										and IDXacNhanCaVaLamThem is null 
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

		public static DataTable LayTableCIO_A_SuaHangLoat(int[] ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT) {
			#region origin
			/*or (TimeStr between @BDRaa and @KTRaa and MachineNo % 2 = 0) )
													and (Loai is null or Loai = 0)
													and (Xoa is null or Xoa = 0)*/
			#endregion 
			#region query
			var query = @"   select distinct	CheckInOut.UserEnrollNumber, UserInfo.UserFullName, UserInfo.UserFullCode, TimeStr, MachineNo, Source, Them, IDGioGoc, Xoa
                                from	CheckInOut, UserInfo
                                where	( (TimeStr between @BDVao and @KTVao and MachineNo % 2 = 1)
		                                or (TimeStr between @BDRaa and @KTRaa and MachineNo % 2 = 0) )
										and (Xoa is null or Xoa = 0)
										and IDXacNhanCaVaLamThem is null 
										and UserInfo.UserEnrollNumber = CheckInOut.UserEnrollNumber
                                        and ( CheckInOut.UserEnrollNumber = {0} )
                                order by CheckInOut.UserEnrollNumber asc, TimeStr asc ";
			query = String.Format(query, String.Join(" or CheckInOut.UserEnrollNumber = ", ArrDSMaCC_Checked));
			#endregion

			var tableCIO_A = SqlDataAccessHelper.ExecuteQueryString(query
				, new[] { "@BDVao", "@KTVao", "@BDRaa", "@KTRaa" }
				, new object[] { ngayBD, ngayKT, ngayBD, ngayKT });
			return tableCIO_A;
		}

		public static DataTable LayTableCIO_A(int UserEnrollNumber, DateTime ngayBD, DateTime ngayKT) {
			var arr = new int[] { UserEnrollNumber };
			return LayTableCIO_A(arr, ngayBD, ngayKT);
		}

		public static DataTable LayTableCIO_V(int[] ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT) {
			#region oringin
/*
			                                FROM	XacNhanCaVaLamThem XN, CheckInOut CIO
                                where	(Loai is null or Loai = 0)
										and (Xoa is null or Xoa = 0)
*/

			#endregion 
			var query = @"   SELECT distinct	CIO.UserEnrollNumber
                                        ,CIO.TimeStr, CIO.Source, CIO.MachineNo, TreSomTinhCV
                                        ,XN.ID,ShiftID,ShiftCode,Onduty,Offduty
                                        ,LateGrace,EarlyGrace,AfterOT
                                        ,DayCount,WorkingTime,Workingday
                                        ,OTMin,Note, CIO.Them, CIO.IDGioGoc, CIO.Xoa
                                FROM	XacNhanCaVaLamThem XN, CheckInOut CIO
                                where	(Xoa is null or Xoa = 0)
										and	CIO.IDXacNhanCaVaLamThem = XN.ID
                                        and ( (TimeStr between @BDVao and @KTVao and MachineNo % 2 = 1)
	                                        or (TimeStr between @BDRaa and @KTRaa and MachineNo % 2 = 0) )
                                        and ( CIO.UserEnrollNumber = {0} )
								group by CIO.UserEnrollNumber,XN.ID
										,CIO.TimeStr,CIO.MachineNo,CIO.Source 
										,ShiftID,ShiftCode,Onduty,Offduty
										,LateGrace,EarlyGrace,AfterOT
										,DayCount,WorkingTime,Workingday
										,OTMin,Note, CIO.Them, CIO.IDGioGoc, CIO.Xoa , TreSomTinhCV
                                order by CIO.UserEnrollNumber asc, ID asc , CIO.TimeStr asc";
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

		public static DataTable LayTableVang(int[] ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT) {
			var query = @"   SELECT      ID, UserInfo.UserEnrollNumber, UserInfo.UserFullCode, UserInfo.UserFullName
                                            , TimeDate, LoaiVang.AbsentCode, LoaiVang.AbsentSymbol,LoaiVang.AbsentDescription
                                            , Thang, Nam, Absent.Workingday, Absent.WorkingTime 
                                FROM        Absent, LoaiVang , UserInfo 
                                WHERE      (Absent.AbsentCode = LoaiVang.AbsentCode)
                                        and (UserInfo.UserEnrollNumber = Absent.UserEnrollNumber)
                                        and (TimeDate between @NgayBD and @NgayKT)
                                        and ( UserInfo.UserEnrollNumber = {0} )
                                ORDER BY    UserInfo.UserEnrollNumber ASC,Nam DESC, Thang DESC, TimeDate ASC ";
			query = String.Format(query, String.Join(" or UserInfo.UserEnrollNumber = ", ArrDSMaCC_Checked));
			var tableVang = SqlDataAccessHelper.ExecuteQueryString(query
																		 , new string[] { "@NgayBD", "@NgayKT" }
																		 , new object[] { ngayBD, ngayKT });
			return tableVang;
		}

		public static DataTable LayTableVang(int UserEnrollNumber, DateTime ngayBD, DateTime ngayKT) {
			var arr = new int[] { UserEnrollNumber };
			return LayTableVang(arr, ngayBD, ngayKT);
		}

		public static DataTable LayTableXacNhanPC50(int[] Arr_MaCC, DateTime ngayBD, DateTime ngayKT) {
			var query = @"		SELECT      UserEnrollNumber, Ngay, TinhPC50
                                FROM        XacNhanPC50
                                WHERE       Ngay between @NgayBD and @NgayKT and ( UserEnrollNumber = {0} )
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
                                WHERE       Duyet = @Duyet and Ngay between @NgayBD and @NgayKT and ( UserEnrollNumber = {0} )
                                ORDER BY    UserEnrollNumber ASC, Ngay ASC ";
			query = String.Format(query, String.Join(" or UserEnrollNumber = ", Arr_MaCC));
			var table = SqlDataAccessHelper.ExecuteQueryString(query
																		 , new string[] { "@NgayBD", "@NgayKT", "@Duyet" }
																		 , new object[] { ngayBD, ngayKT, iDuyet });
			return table;
		}


		public static DataTable DocNgayLe(DateTime ngayBD, DateTime ngayKT) {
			const string query = @"	select * from Holiday where HDate between @NgayBD and @NgayKT";
			var table = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@NgayBD", "@NgayKT" }, new object[] { ngayBD, ngayKT });
			return table;
		}


		public static int UpdateSetting(int id, string value)
		{

			var query = @"	update Setting set Value = @Value where ID = @ID ";
			var kq = SqlDataAccessHelper.ExecNoneQueryString(query, new string[] {"@ID", "@Value" }, new object[] { id, value});
			return kq;
		}


		public static bool LoaiGioLienQuan(List<cChk> kq1) {
			var query = string.Empty;
			var param = new string[kq1.Count];
			var obj = new object[kq1.Count];
			var dem = 0;
			foreach (cChk o in kq1) {
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

		public static bool ThemGio_ra3_vao1(List<cChk> kq2) {
			var query = string.Empty;
			var param = new string[6 * kq2.Count];
			var obj = new object[6 * kq2.Count];
			var dem = 0;
			cChkInn_A checkInn;
			cChkOut_A checkOut;
			foreach (cChk o in kq2) {
				if (o is cChkInn_A) {
					checkInn = o as cChkInn_A;
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

					query += @"	insert into CheckInOut (UserEnrollNumber, TimeDate, TimeStr, OriginType, MachineNo, Source, WorkCode, Them) values ( ";
					query += "@UserEnrollNumber" + dem + ", @TimeDate" + dem + ", @TimeStr" + dem + ", @OriginType" + dem + ", @MachineNo" + dem + ", @Source" + dem + ", 0, 1) ";

				}
				else {
					checkOut = o as cChkOut_A;
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

					query += @"	insert into CheckInOut (UserEnrollNumber, TimeDate, TimeStr, OriginType, MachineNo, Source, WorkCode, Them) values ( ";
					query += "@UserEnrollNumber" + dem + ", @TimeDate" + dem + ", @TimeStr" + dem + ", @OriginType" + dem + ", @MachineNo" + dem + ", @Source" + dem + ", 0, 1) ";

				}
				dem++;
			}
			if (param.Length == 0) return true;
			if (SqlDataAccessHelper.ExecNoneQueryString(query, param, obj) == 0)
				return false;
			return true;

		}

		public static bool ThemGioChoNV(int UserEnrollNumber, DateTime GioMoi, string pKieuGioMoi, int MachineNoNew, int pUserID, string pLydo, string pGhichu) {
			var kq = 0;
			var OriginTypeNew = pKieuGioMoi;
			const string query = @"   INSERT INTO CheckInOut (UserEnrollNumber, TimeStr, TimeDate, Source, MachineNo, OriginType, WorkCode, Them)
                                VALUES     (@UserEnrollNumber, @TimeStrNew, @TimeDateNew, @SourceNew, @MachineNoNew, @OriginTypeNew, 0, 1)

                                INSERT INTO LichSuSuaGioVaoRa (UserEnrollNumber
                                            , TimeStrOld, TimeStrNew, SourceOld, SourceNew, MachineNoOld, MachineNoNew
                                            , UserID, Explain, Note, CommandType, ExecuteTime ) 
								VALUES      (@UserEnrollNumber
                                            ,null,@TimeStrNew,null,@SourceNew,null,@MachineNoNew
                                            ,@UserID,@Explain,@Note,@CommandType,GetDate() ) ";

			kq = SqlDataAccessHelper.ExecNoneQueryString(query
					, new string[] { "@UserEnrollNumber", "@TimeStrNew", "@TimeDateNew", "@SourceNew", "@MachineNoNew", "@OriginTypeNew"
                                    ,"@UserID","@Explain","@Note","@CommandType"}
					, new object[] { UserEnrollNumber, GioMoi, GioMoi.Date, "PC", MachineNoNew, OriginTypeNew
                                    , pUserID, pLydo, pGhichu, 1});
			if (kq == 0) return false;
			return true;
		}

		public static bool XoaGioChoNV(int pUserEnrollNumber, DateTime pGioCu, string pSourceOld, int pMachineNoOld, int pUserID, string pLydo, string pGhichu) {
			var kq = 0;
			const string query = @"   UPDATE  CheckInOut 
									SET Xoa = 1
                                WHERE   UserEnrollNumber = @UserEnrollNumber
                                        and TimeStr = @TimeStrOld
                                        and (MachineNo % 2 = @MachineNoOld % 2)

                                INSERT INTO LichSuSuaGioVaoRa (UserEnrollNumber
                                            , TimeStrOld, TimeStrNew, SourceOld, SourceNew, MachineNoOld, MachineNoNew
                                            , UserID, Explain, Note, CommandType, ExecuteTime ) 
								VALUES      (@UserEnrollNumber
                                            ,@TimeStrOld,null,@SourceOld,null,@MachineNoOld,null
                                            ,@UserID,@Explain,@Note,@CommandType,GetDate() ) ";

			kq = SqlDataAccessHelper.ExecNoneQueryString(query
				, new string[] { "@UserEnrollNumber", "@TimeStrOld", "@SourceOld", "@MachineNoOld"
                                ,"@UserID","@Explain","@Note","@CommandType"}
				, new object[] { pUserEnrollNumber, pGioCu, pSourceOld, pMachineNoOld
                                , pUserID, pLydo, pGhichu, -1});
			if (kq == 0) return false;
			return true;

		}

		public static bool SuaGioChoNV(int UserEnrollNumber, DateTime GioCuu, DateTime GioMoi, string SourceOld, string SourceNew, int MachineNoOld, int MachineNoNew, int idgiogoc, int UserID, string Lydo, string Ghichu) {
			var kq = 0;

			var query = string.Empty;
			if (idgiogoc != -1) {
				query = @"   update  CheckInOut 
                                set     TimeStr = @TimeStrNew, TimeDate = @TimeDateNew, Source = @SourceNew, MachineNo = @MachineNoNew
					            where   UserEnrollNumber = @UserEnrollNumber 
                                        and TimeStr = @TimeStrOld
                                        and (MachineNo % 2 = @MachineNoOld % 2)
                                INSERT INTO LichSuSuaGioVaoRa (UserEnrollNumber
                                            , TimeStrOld, TimeStrNew, SourceOld, SourceNew, MachineNoOld, MachineNoNew
                                            , UserID, Explain, Note, CommandType, ExecuteTime ) 
								VALUES      (@UserEnrollNumber
                                            ,@TimeStrOld,@TimeStrNew,@SourceOld,@SourceNew,@MachineNoOld,@MachineNoNew
                                            ,@UserID,@Explain,@Note,@CommandType,GetDate() ) ";
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
                                        and (MachineNo % 2 = @MachineNoOld % 2)
                                INSERT INTO LichSuSuaGioVaoRa (UserEnrollNumber
                                            , TimeStrOld, TimeStrNew, SourceOld, SourceNew, MachineNoOld, MachineNoNew
                                            , UserID, Explain, Note, CommandType, ExecuteTime ) 
								VALUES      (@UserEnrollNumber
                                            ,@TimeStrOld,@TimeStrNew,@SourceOld,@SourceNew,@MachineNoOld,@MachineNoNew
                                            ,@UserID,@Explain,@Note,@CommandType,GetDate() )";


			}
			kq = SqlDataAccessHelper.ExecNoneQueryString(query
					, new string[] { "@UserEnrollNumber", "@TimeStrOld", "@TimeStrNew", "@TimeDateNew"
                                    , "@SourceOld", "@SourceNew", "@MachineNoOld", "@MachineNoNew"
                                    ,"@UserID","@Explain","@Note","@CommandType"}
					, new object[] { UserEnrollNumber, GioCuu, GioMoi, GioMoi.Date
                                    , SourceOld, SourceNew, MachineNoOld, MachineNoNew
                                    , UserID, Lydo, Ghichu, 0});
			if (kq == 0) return false;
			return true;

		}


		public static DataTable XacNhanGio_A(int iUserEnrollNumber, int ShiftID, string ShiftCode
			, string onnduty, string offduty, int lategrace, int earlygrace, int afterot, int daycount
			, float wktime, float wkdayy, int sophutOT, bool TreSomTinhCV //,bool chophepTinhPC150//[140615_2]
			, DateTime timestrInn, DateTime timestrOut, string SourceInn, string SourceOut, int MachineNoInn, int MachineNoOut) {

			#region oringin 
/*
						var query = @"	declare   @ID int ; 
						INSERT INTO XacNhanCaVaLamThem
						( UserEnrollNumber,  ShiftID,  ShiftCode,
						  Onduty,  Offduty,  LateGrace,  EarlyGrace,  AfterOT,
						  DayCount,  WorkingTime,  Workingday,
						  TimeStrIn,  TimeStrOut,
						  OTMin,  Note ) 
						VALUES 
						( @UserEnrollNumber,  @ShiftID,  @ShiftCode,
						  @Onduty,  @Offduty,  @LateGrace,  @EarlyGrace,  @AfterOT,
						  @DayCount,  @WorkingTime,  @Workingday,
						  @TimeStrIn,  @TimeStrOut,
						  @OTMin,  @Note )

						select @ID = SCOPE_IDENTITY()  
  						select SCOPE_IDENTITY() 

						UPDATE	CheckInOut
						SET		IDXacNhanCaVaLamThem = @ID
						WHERE	(Loai is NULL or Loai = 0)
							AND		UserEnrollNumber = @UserEnrollNumber
							AND	(MachineNo % 2 = @MachineNoIn % 2)
							AND TimeStr = @TimeStrIn

						UPDATE	CheckInOut
						SET		IDXacNhanCaVaLamThem = @ID
						WHERE	(Loai is NULL or Loai = 0)
							AND	UserEnrollNumber = @UserEnrollNumber
							AND	(MachineNo % 2 = @MachineNoOut%2)
							AND TimeStr = @TimeStrOut

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
*/
			#endregion

			#region query, bie561
			var query = @"	declare   @ID int ; 
						INSERT INTO XacNhanCaVaLamThem
						( UserEnrollNumber,  ShiftID,  ShiftCode, TreSomTinhCV,
						  Onduty,  Offduty,  LateGrace,  EarlyGrace,  AfterOT,
						  DayCount,  WorkingTime,  Workingday,
						  TimeStrIn,  TimeStrOut,
						  OTMin,  Note ) 
						VALUES 
						( @UserEnrollNumber,  @ShiftID,  @ShiftCode, @TreSomTinhCV,
						  @Onduty,  @Offduty,  @LateGrace,  @EarlyGrace,  @AfterOT,
						  @DayCount,  @WorkingTime,  @Workingday,
						  @TimeStrIn,  @TimeStrOut,
						  @OTMin,  @Note )

						select @ID = SCOPE_IDENTITY()  
  						select SCOPE_IDENTITY() 

						UPDATE	CheckInOut
						SET		IDXacNhanCaVaLamThem = @ID
						WHERE	UserEnrollNumber = @UserEnrollNumber
							AND	(MachineNo % 2 = @MachineNoIn % 2)
							AND TimeStr = @TimeStrIn

						UPDATE	CheckInOut
						SET		IDXacNhanCaVaLamThem = @ID
						WHERE	UserEnrollNumber = @UserEnrollNumber
							AND	(MachineNo % 2 = @MachineNoOut%2)
							AND TimeStr = @TimeStrOut

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
			
			return SqlDataAccessHelper.ExecuteQueryString(query
									   , new string[] {"@UserEnrollNumber", "@ShiftID", "@ShiftCode", "@TreSomTinhCV",
																					"@Onduty", "@Offduty", "@LateGrace", "@EarlyGrace", "@AfterOT", 
																					"@DayCount", "@WorkingTime", "@Workingday", 
																					"@OTMin", //"@PCTangCuong", 
																					"@TimeStrIn", "@TimeStrOut",
																					"@SourceInn", "@SourceOut", "@MachineNoIn", "@MachineNoOut",
																					"@Note", "@Explain", "@UserID"}
									   , new object[] {iUserEnrollNumber, ShiftID, ShiftCode, TreSomTinhCV,
																					onnduty, offduty, lategrace, earlygrace, afterot,
																					daycount, wktime, wkdayy,
																					sophutOT, //PCTangCuong,//[140615_2]
																					timestrInn, timestrOut,
																					SourceInn, SourceOut, MachineNoInn, MachineNoOut,
																					"Xác nhận giờ chấm công", "", XL2.currUserID
																	});



		}

		public static int CapNhatXacNhanGio_V(int iUserEnrollNumber, int ID, int shiftId, string shiftCode, string OnDutyString, string OffDutyString, int lateGrace_Minute, int earlyGrace_Minute, int afterOT_Minute, int dayCount, float workingTime_float, float workingday_float, int SoPhutLamThem, bool TreSomTinhCV, DateTime OldTimeStrVao, DateTime dOldTimeStrRaa, string sourceInn, string sourceOut, int machineNoInn, int machineNoOut) {
			#region query
			var query = @"	
						UPDATE XacNhanCaVaLamThem
						SET ShiftID = @ShiftID, ShiftCode = @ShiftCode,
						  Onduty = @Onduty,  Offduty = @Offduty, LateGrace = @LateGrace,  EarlyGrace = @EarlyGrace,  AfterOT = @AfterOT,
						  DayCount = @DayCount,  WorkingTime = @WorkingTime,  Workingday =  @Workingday,
						  OTMin = @OTMin,  TimeStrIn = @TimeStrIn,  TimeStrOut = @TimeStrOut, Note = @Note 
						WHERE ID = @ID
  
						INSERT INTO LichSuSuaGioVaoRa
						( UserEnrollNumber,  TimeStrOld,  TimeStrNew,  
						  SourceOld,  SourceNew, MachineNoOld,  MachineNoNew,
						  UserID,  Explain,  Note,
						  ExecuteTime,  CommandType
						) 
						VALUES 
						( @UserEnrollNumber,  @TimeStrIn,  @TimeStrIn,
						  @SourceInn, N'PC', @MachineNoIn, 21, 
						  @UserID,  @Explain,  @Note,
						  GetDate(),  0)

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
						  GetDate(),  0)
						 ";
			#endregion

			return SqlDataAccessHelper.ExecNoneQueryString(query
									   , new string[] {"@UserEnrollNumber", "@ID", "@ShiftID", "@ShiftCode", "@TreSomTinhCV",
																					"@Onduty", "@Offduty", "@LateGrace", "@EarlyGrace", "@AfterOT", 
																					"@DayCount", "@WorkingTime", "@Workingday", 
																					"@OTMin", "@TimeStrIn", "@TimeStrOut", 
																					"@SourceInn", "@SourceOut", "@MachineNoIn", "@MachineNoOut",
																					"@Note", "@Explain", "@UserID"}
									   , new object[] { iUserEnrollNumber, ID, shiftId, shiftCode, TreSomTinhCV,
																					OnDutyString, OffDutyString, lateGrace_Minute, earlyGrace_Minute, afterOT_Minute,
																					dayCount, workingTime_float, workingday_float,
																					SoPhutLamThem,
																					OldTimeStrVao, dOldTimeStrRaa, 
																					sourceInn, sourceOut, machineNoInn, machineNoOut,
																					"Xác nhận lại giờ chấm công", "", XL2.currUserID
																	});


		}


		public static bool ThemNgayVang(int[] DSMaCC_MaNV_Checked, List<DateTime> DSNgayCheck, double workingDay, double workingTime, string absentCode, int currentUserID) {
			#region query
			var query = @"	INSERT INTO Absent (UserEnrollNumber,   TimeDate,  AbsentCode, Thang, Nam, Workingday, WorkingTime, UserID) 
                                VALUES		  (@UserEnrollNumber,  @TimeDate, @AbsentCode,@Thang,@Nam,@Workingday,@WorkingTime,@UserID)";
			#endregion

			var flagError = false;
			foreach (var maCC in DSMaCC_MaNV_Checked) {
				if (DSNgayCheck.Select(ngay => SqlDataAccessHelper.ExecNoneQueryString(
												query,
												new string[] { "@UserEnrollNumber", "@TimeDate", "@AbsentCode", "@Thang", "@Nam", "@Workingday", "@WorkingTime", "@UserID" },
												new object[] { maCC, ngay.Date, absentCode, ngay.Month, ngay.Year, workingDay, workingTime, currentUserID })).Any(kqThaotac => kqThaotac == 0)) {
					flagError = true;
				}
				if (flagError) break;
			}
			if (flagError) return false;
			return true;
		}

		public static bool XoaNgayVangNV(int[] arrRecord) {
			var kqThaotac = 0;
			var flagError = false;
			var query = @"DELETE FROM Absent WHERE ID = {0}";
			query = String.Format(query, String.Join(" or ID = ", arrRecord));

			kqThaotac = SqlDataAccessHelper.ExecNoneQueryString(query, null, null);
			if (kqThaotac == 0) {
				flagError = true;
			}
			if (flagError) return false;
			return true;
		}

		public static DataTable LietKeNgayVangChoNV(int[] arrDSNVCheck, DateTime ngayBD, DateTime ngayKT) {
			var query = @"	SELECT      ID, UserInfo.UserEnrollNumber, UserInfo.UserFullCode, UserInfo.UserFullName, TimeDate, 
										LoaiVang.AbsentCode, LoaiVang.AbsentSymbol,LoaiVang.AbsentDescription, Thang, Nam, Absent.Workingday, Absent.WorkingTime 
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

		public static bool GhiNhanDieuChinhLuong(int iMaCC, DateTime thang, decimal luong) {
			int kqThaotac = 0;
			bool flagError = false;
			#region query
			var query = @"   UPDATE DieuChinhLuongThangTruoc SET LuongDieuChinh = @LuongDieuChinh
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

		public static bool CapNhatHeSoLuong(int iMaCC, Single hslCB, Single hslSP, Single hsBHcongthem, bool UserEnabled, bool TinhLuongCongNhat) {
			int kqThaotac = 0;
			bool flagError = false;
			#region query
			var query = @"   UPDATE UserInfo SET HeSoLuongCB = @HeSoLuongCB, HeSoLuongSP = @HeSoLuongSP, HSBHCongThem=@HSBHCongThem, 
												UserEnabled = @UserEnabled, TinhLuongCongNhat = @TinhLuongCongNhat
								WHERE UserEnrollNumber = @UserEnrollNumber";
			#endregion
			kqThaotac = SqlDataAccessHelper.ExecNoneQueryString(query
				, new string[] { "@UserEnrollNumber", "@HeSoLuongCB", "@HeSoLuongSP", "@HSBHCongThem", "@UserEnabled", "@TinhLuongCongNhat" }
				, new object[] { iMaCC, hslCB, hslSP, hsBHcongthem, UserEnabled, TinhLuongCongNhat });
			if (kqThaotac == 0) flagError = true;
			if (flagError == true) return false;
			return true;
		}



		public static void CheDoTest(List<cUserInfo> dsnv , DateTime ngayBD, DateTime ngayKT)
		{
			var Arr_MaCC = (from nv in dsnv select nv.MaCC).ToArray();
			#region query
			var query = @"		update	CheckInOut set Loai = null
                                where	( (TimeStr between @BDVao and @KTVao and MachineNo % 2 = 1)
		                                or (TimeStr between @BDRaa and @KTRaa and MachineNo % 2 = 0) )
                                        and ( UserEnrollNumber = {0} ) ";
			query = String.Format(query, String.Join(" or UserEnrollNumber = ", Arr_MaCC));
			#endregion

			SqlDataAccessHelper.ExecNoneQueryString(query
				, new[] { "@BDVao", "@KTVao", "@BDRaa", "@KTRaa" }
				, new object[] { ngayBD.Add(XL2._18h00), ngayKT.Add(XL2._04h30), ngayBD.AddDays(1d).Add(XL2._07h00), ngayKT.Add(XL2._18h00) });
			//AutoClosingMessageBox.Show("complete", "thong bao",3000);
		}

		public static DateTime LayDuLieuTGCuoi() {
			const string query = @"	SELECT	MAX(CheckInOut.TimeStr) FROM CheckInOut WHERE OriginType != N'PC' and MachineNo != 21 and MachineNo != 22";
			var kq = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			var time = (kq.Rows.Count != 0) ? (DateTime)kq.Rows[0][0] : DateTime.MinValue;
			return time;
		}


		public static int CheckTinhPC50(int macc, DateTime ngay, bool giatri) {
			var tenbien = "TinhPC50";

			var query = @"	update		XacNhanPC50 set " + tenbien + " = @giatri " +
						"	where		Ngay = @Ngay and UserEnrollNumber = @UserEnrollNumber " +
						"	if @@ROWCOUNT=0 " +
						"	insert into XacNhanPC50	(UserEnrollNumber, Ngay," + tenbien + ")" +
						"	values (@UserEnrollNumber, @Ngay,@giatri)";
			var n = SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@giatri", "@Ngay", "@UserEnrollNumber" },
															new object[] { giatri, ngay, macc });
			return n;
		}
		public static int SetMacDinhTinhPC50(int macc, DateTime ngay) {
			var query = @"	delete		XacNhanPC50 where	Ngay = @Ngay and UserEnrollNumber = @UserEnrollNumber ";
			var n = SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@Ngay", "@UserEnrollNumber" }, new object[] { ngay, macc });
			return n;
		}
		public static int UpdateOrInsert_TinhPCDB(int macc, DateTime ngay, int loai, int pcngay, int pcdem) {
			var query = @"	update		XacNhanPC set LoaiPC = @loai, Duyet = 1 " +
						"	where		Ngay = @Ngay and UserEnrollNumber = @UserEnrollNumber " +
						"	if @@ROWCOUNT=0 " +
						"	insert into XacNhanPC	(UserEnrollNumber, Ngay,LoaiPC,PCNgay, PCDem,Duyet)" +
						"	values (@UserEnrollNumber, @Ngay,@loai,@PCNgay, @PCDem,1)";
			var n = SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@loai", "@Ngay", "@UserEnrollNumber", "@PCNgay", "@PCDem" },
															new object[] { loai, ngay, macc, pcngay, pcdem });
			return n;
		}
		public static int DeleteTinhPCDB(int macc, DateTime ngay) {

			var query = @"	delete	XacNhanPC where Ngay = @Ngay and UserEnrollNumber = @UserEnrollNumber";
			var n = SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@Ngay", "@UserEnrollNumber" }, new object[] { ngay, macc });
			return n;
		}

		public static object DeleteTinhPC50(int macc, DateTime ngay) {
			var query = @"	delete	XacNhanPC50 where Ngay = @Ngay and UserEnrollNumber = @UserEnrollNumber";
			var n = SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@Ngay", "@UserEnrollNumber" }, new object[] { ngay, macc });
			return n;
		}

		public static int	CapNhatLuongDieuChinh_TamUng_ThuChiKhac(int UserEnrollNumber, int thang, int nam, double luongdieuchinh, double tamung, double thuchikhac, Single MucDongBHXH) {
			var query = string.Empty;
			query = @"	update DieuChinhLuongThangTruoc 
						set LuongDieuChinh = @LuongDieuChinh , TamUng = @TamUng, ThuChiKhac = @ThuChiKhac, MucDongBHXH=@MucDongBHXH
						where	UserEnrollNumber = @UserEnrollNumber	and Thang = @Thang and Nam=@Nam 
						if @@RowCount = 0
						insert into DieuChinhLuongThangTruoc(UserEnrollNumber,Thang,Nam,LuongDieuChinh,TamUng,ThuChiKhac,MucDongBHXH) 
						values (@UserEnrollNumber,@Thang,@Nam,@LuongDieuChinh,@TamUng,@ThuChiKhac,@MucDongBHXH)	";
			var n = SqlDataAccessHelper.ExecNoneQueryString(query,
															new string[] { "@UserEnrollNumber", "@Thang", "@Nam", "@LuongDieuChinh", "@TamUng", "@ThuChiKhac", "@MucDongBHXH" },
															new object[] { UserEnrollNumber, thang, nam, luongdieuchinh, tamung, thuchikhac, MucDongBHXH });
			return n;
		}

		public static DataTable LayBangDieuChinhLuong(DateTime thang) {
			var query = string.Empty;
			query = @"	select UserInfo.UserEnrollNumber, Thang, Nam, LuongDieuChinh,TamUng,ThuChiKhac,MucDongBHXH, UserFullCode, UserFullName
						from DieuChinhLuongThangTruoc, UserInfo
						where Thang = @Thang and Nam=@Nam
							and UserInfo.UserEnrollNumber =  DieuChinhLuongThangTruoc.UserEnrollNumber";
			var kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@Thang", "@Nam" }, new object[] { thang.Month, thang.Year });
			return kq;
		}

		public static DataTable LayBangLuongCongNhat(int thang, int nam) {
			var query = string.Empty;
			query = @"	select	ID, Thang, Nam, Ten, ChucVu, DonGiaLuong, NgayCong, ThanhTien, TamUng, ThucLanh
						from	LuongCongNhat where Thang=@Thang and Nam = @Nam";
			var kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@Thang", "@Nam" }, new object[] { thang, nam });
			return kq;
		}

		public static int ThemLuongCongNhat(int thang, int nam, string hoten, string chucdanh, double dongialuong, double ngayCong, double dthantien, double dtamung, double dthuclanh) {
			var query =
				@"	insert into LuongCongNhat (Thang, Nam, Ten, ChucVu, DonGiaLuong, NgayCong, ThanhTien, TamUng, ThucLanh)
							values (@Thang, @Nam, @Ten, @ChucVu, @DonGiaLuong, @NgayCong, @ThanhTien, @TamUng, @ThucLanh)";
			var kq = SqlDataAccessHelper.ExecNoneQueryString(query,
															 new string[]
				                                                 {
					                                                 "@Thang", "@Nam", "@Ten", "@ChucVu", "@DonGiaLuong", "@NgayCong",
					                                                 "@ThanhTien", "@TamUng", "@ThucLanh"
				                                                 },
															 new object[]
				                                                 {
					                                                 thang, nam, hoten, chucdanh, dongialuong, ngayCong, dthantien, dtamung,
					                                                 dthuclanh
				                                                 });
			return kq;
		}

		public static int CapNhatLuongCongNhat(int id, int thang, int nam, string hoten, string chucdanh, double dongialuong, double ngayCong, double dthantien, double dtamung, double dthuclanh) {
			var query = @"	update LuongCongNhat 
								set Thang= @Thang,  Nam= @Nam, Ten= @Ten, ChucVu= @ChucVu,  DonGiaLuong= @DonGiaLuong, 
									NgayCong=  @NgayCong,  ThanhTien= @ThanhTien,  TamUng=  @TamUng,  ThucLanh=@ThucLanh
							where ID = @ID";
			var kq = SqlDataAccessHelper.ExecNoneQueryString(query,
															 new string[]
				                                                 {
					                                                 "@ID", "@Thang", "@Nam", "@Ten", "@ChucVu", "@DonGiaLuong", "@NgayCong",
					                                                 "@ThanhTien", "@TamUng", "@ThucLanh"
				                                                 },
															 new object[]
				                                                 {
					                                                 id, thang, nam, hoten, chucdanh, dongialuong, ngayCong, dthantien, dtamung,
					                                                 dthuclanh
				                                                 });
			return kq;
		}

		public static int XoaLuongCongNhat(int id) {
			var query = @"	delete from LuongCongNhat where ID = @ID";
			var kq = SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@ID" }, new object[] { id });
			return kq;
		}



		public static DataTable SelAllDSTaikhoan()
		{
			var query = @"	select UserID, UserAccount from NewUserAccount";
			var kq = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return kq;
		}

		public static DataTable LayBang_PH_Them(int[] arrMaCC, DateTime ngayBD, DateTime ngayKT, DataTable Bang_PH_Them)
        {
            var query = @"  SELECT    CheckInOut.UserEnrollNumber,UserFullCode,UserFullName,
                                      case when MachineNo%2 = 1 then N'Vào'
                                      else N'Ra' end as Type,
                                      TimeStr,MachineNo,Source,                                      
                                      Them
                                    FROM    CheckInOut, UserInfo
                                    WHERE (CheckInOut.TimeStr between @NgayBD and @NgayKT)
										and Them = 1 and (IDXacNhanCaVaLamThem is null)
                                        and CheckInOut.UserEnrollNumber = UserInfo.UserEnrollNumber
                                        and ( CheckInOut.UserEnrollNumber = {0} )";
			var temp = string.Join(" or CheckInOut.UserEnrollNumber = ", arrMaCC);
            query = string.Format(query, temp);

            var kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] {"@NgayBD", "@NgayKT"},
                                                            new object[] {ngayBD, ngayKT});
            return kq;
        }

		public static DataTable LayBang_PH_Xoa(int[] arrMaCC, DateTime ngayBD, DateTime ngayKT, DataTable m_Bang_PH_Xoaa) {
			var query = @"  SELECT    CheckInOut.UserEnrollNumber,UserFullCode,UserFullName,
                                      TimeStr,MachineNo,Source,
									  case when MachineNo%2 = 1 then N'Vào'
                                      else N'Ra' end as Type,
                                      Xoa
                                    FROM    CheckInOut, UserInfo
                                    WHERE (CheckInOut.TimeStr between @NgayBD and @NgayKT)
										and Xoa = 1 and (Them is null or Them = 0)
                                        and CheckInOut.UserEnrollNumber = UserInfo.UserEnrollNumber
                                        and ( CheckInOut.UserEnrollNumber = {0} )";
			var temp = string.Join(" or CheckInOut.UserEnrollNumber = ", arrMaCC);
            query = string.Format(query, temp);

            var kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] {"@NgayBD", "@NgayKT"},
                                                            new object[] {ngayBD, ngayKT});
            return kq;
		}

		public static DataTable LayBang_PH_GioGoc(int[] arrMaCC, DateTime ngayBD, DateTime ngayKT, DataTable m_Bang_PH_GioGoc) {
			var query = @"  SELECT		CheckInOut.UserEnrollNumber,UserFullCode,UserFullName,
										case when CheckInOut.MachineNo%2 = 1 then N'Vào'
										else N'Ra' end as CurrType,
										CheckInOut.TimeStr as CurrTimeStr,CheckInOut.MachineNo as CurrMachineNo,CheckInOut.Source as CurrSource,
										case when GioGoc.MachineNo%2 = 1 then N'Vào'
										else N'Ra' end as OrinType,
										GioGoc.TimeStr as OrinTimeStr, GioGoc.MachineNo as OrinMachineNo, GioGoc.Source as OrinSource,
										Them, GioGoc.IDGioGoc
			
							FROM	    CheckInOut, UserInfo, GioGoc
							WHERE		(CheckInOut.TimeStr between @NgayBD and @NgayKT)
										and (Xoa is null or Xoa = 0) and (IDXacNhanCaVaLamThem is null)
										and (CheckInOut.IDGioGoc = GioGoc.IDGioGoc)
										and CheckInOut.UserEnrollNumber = UserInfo.UserEnrollNumber                                        
                                        and ( CheckInOut.UserEnrollNumber = {0} )
							order by	IDGioGoc ";


			var temp = string.Join(" or CheckInOut.UserEnrollNumber = ", arrMaCC);
			query = string.Format(query, temp);

			var kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@NgayBD", "@NgayKT" },
															new object[] { ngayBD, ngayKT });
			return kq;
		}

		public static int PhucHoiGioThem(int UserEnrollNumber, int MachineNo, DateTime TimeStr)
		{
			var query = @"	delete from		CheckInOut 
								where		UserEnrollNumber = @UserEnrollNumber and MachineNo = @MachineNo and TimeStr = @TimeStr";
			var kq = SqlDataAccessHelper.ExecNoneQueryString(query,
			                                                 new string[] {"@UserEnrollNumber","@MachineNo", "@TimeStr"},
			                                                 new object[] {UserEnrollNumber, MachineNo, TimeStr});
			return kq;
		}

		public static int PhucHoiGioXoa(int UserEnrollNumber, int MachineNo, DateTime TimeStr) {
			var query = @"	update CheckInOut set Xoa = null 
								where		UserEnrollNumber = @UserEnrollNumber and MachineNo = @MachineNo and TimeStr = @TimeStr";
			var kq = SqlDataAccessHelper.ExecNoneQueryString(query,
			                                                 new string[] {"@UserEnrollNumber","@MachineNo", "@TimeStr"},
			                                                 new object[] {UserEnrollNumber, MachineNo, TimeStr});
			return kq;
		}



		public static int PhucHoiGioGoc(int UserEnrollNumber, int CurrMachineNo, DateTime CurrTimeStr, string CurrSource, int OrinMachineNo, DateTime OrinTimeStr, string OrinSource, int IDGioGoc)
		{
			var query = @"	update	CheckInOut
							set		MachineNo = @OrinMachineNo, TimeStr = @OrinTimeStr, Source = @OrinSource, IDGioGoc = null
							where UserEnrollNumber = @UserEnrollNumber and MachineNo = @CurrMachineNo and TimeStr = @CurrTimeStr and Source = @CurrSource";
			var kq = SqlDataAccessHelper.ExecNoneQueryString(query,
			                                                 new string[] {"@UserEnrollNumber", "@CurrMachineNo", "@CurrTimeStr", "@CurrSource", "@OrinMachineNo", "@OrinTimeStr", "@OrinSource", "@IDGioGoc"},
			                                                 new object[] {UserEnrollNumber, CurrMachineNo, CurrTimeStr, CurrSource, OrinMachineNo, OrinTimeStr, OrinSource, IDGioGoc});
			return kq;
		}

		internal static DataTable LayBang_GioDaXN(int[] arrMaCC, DateTime ngayBD, DateTime ngayKT, DataTable m_Bang_GioDaXN)
		{
			var query = @"	select	CheckInOut.UserEnrollNumber, TimeDate, TimeStr, MachineNo, IDXacNhanCaVaLamThem 
							from	CheckInOut	
							where	IDXacNhanCaVaLamThem is not null
								and MachineNo %2 = 1 and TimeStr between  @NgayBD and @NgayKT
								and (CheckInOut.UserEnrollNumber = {0})
								";
			query = string.Format(query, string.Join(" or CheckInOut.UserEnrollNumber = ", arrMaCC));
			var table1 = SqlDataAccessHelper.ExecuteQueryString(query, new string[] {"@NgayBD", "@NgayKT"},
			                                                    new object[] {ngayBD, ngayKT});
			return table1;
		}
		internal static DataTable LayBang_GioDaXN2(int[] arrMaCC, DateTime ngayBD, DateTime ngayKT, DataTable m_Bang_GioDaXN) {
			var query = @"	select	UserFullCode, UserFullName, CheckInOut.UserEnrollNumber, TimeStr, IDXacNhanCaVaLamThem 
							from	CheckInOut	, UserInfo
							where	IDXacNhanCaVaLamThem is not null
								and MachineNo %2 = 1 and TimeStr between  @NgayBD and @NgayKT
								and UserInfo.UserEnrollNumber = CheckInOut.UserEnrollNumber
								and (CheckInOut.UserEnrollNumber = {0})
								";
			query = string.Format(query, string.Join(" or CheckInOut.UserEnrollNumber = ", arrMaCC));
			var table1 = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@NgayBD", "@NgayKT" },
																new object[] { ngayBD, ngayKT });
			return table1;
		}

		internal static DataTable LayBang_GioDaXN3(int[] arrMaCC, DataTable m_Bang_GioDaXN)
		{
			var query = @"	select CheckInOut.UserEnrollNumber, TimeStr, IDXacNhanCaVaLamThem ,ShiftID, ShiftCode, OTMin 
							from CheckInOut	, XacNhanCaVaLamThem
							where MachineNo %2 = 0 and IDXacNhanCaVaLamThem = ID and (IDXacNhanCaVaLamThem = {0})
							";
			query = string.Format(query, string.Join(" or IDXacNhanCaVaLamThem = ", arrMaCC));
			var table1 = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return table1;
		}

		internal static void HuyXN_GioChamCong(int ID)
		{
			var query = @"	update CheckInOut set IDXacNhanCaVaLamThem = null where IDXacNhanCaVaLamThem = @IDXacNhanCaVaLamThem";
			SqlDataAccessHelper.ExecNoneQueryString(query, new string[] {"@IDXacNhanCaVaLamThem"}, new object[] {ID});
		}

		internal static DataTable LayDSLoaiVang()
		{
			var query = @"	select * from LoaiVang";
			return SqlDataAccessHelper.ExecuteQueryString(query, null, null);
		}
	}
}
