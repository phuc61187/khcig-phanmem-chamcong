using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CapNhatGioChamCong.DAO
{
    public static class DAL
    {
        /// <summary>
        /// nhớ thêm điều kiện trước chuỗi này AND, OR ....CheckInOut.UserEnrollNumber = {0}
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="tenThuoctinh">chú ý nhớ sử dụng tên tiền tố phía trước CheckInOut.UserEnrollNumber hay UserInfo.UserEnrollNumber</param>
        /// <returns></returns>
        public static string TaoChuoiOR(List<int> ds, string tenThuoctinh)
        {
            string kq = String.Empty;
            kq += @"( ";
            kq += tenThuoctinh + @" = {0}";
            kq = String.Format(kq, String.Join(" or " + tenThuoctinh + " = ", ds.ToArray()));
            kq += @" )";
            return kq;

        }


        public static DataTable LayDSNV(int[] arrIDPhongBan)
        {
            #region
            string query = @"   select  UserEnrollNumber,UserFullName,UserFullCode
	                                    ,HeSoLuongCB,HeSoLuongSP,u.SchID,SchName
	                                    ,UserIDTitle
                                        ,   case when u.UserIDTitle = 0 then N'Chưa SX'
	                                        else TitleName
	                                        end as TitleName
	                                    ,d1.ID as IDD_1, d1.Description as Description_1, d1.TinhPC150
	                                    ,d2.ID as IDD_2, d2.Description as Description_2
	                                    --,d3.ID as IDD_3, d3.Description as Description_3
                                from    UserInfo u left join Title on u.UserIDTitle = Title.IDT
	                                    left join RelationDept d1 on u.UserIDD = d1.ID
	                                    left join RelationDept d2 on d1.RelationID = d2.ID
	                                    --left join RelationDept d3 on d2.RelationID = d3.ID
                                        left join Schedule on u.SchID = Schedule.SchID
                                where   UserEnabled = 1 
                                        and ( UserIDD = {0} )
                                order by UserEnrollNumber asc ";
            query = String.Format(query, String.Join(" or UserIDD = ", arrIDPhongBan));
            #endregion

            DataTable table = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
            return table;
        }

        public static DataTable LayDSPhong(int userID)
        {
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

        public static DataTable LayTableCIO_A(int[] ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT)
        {
            #region query
            string query = @"   select	UserEnrollNumber, TimeStr, MachineNo, Source
                                from	CheckInOut
                                where	( (TimeStr between @BDVao and @KTVao and MachineNo % 2 = 1)
		                                or (TimeStr between @BDRaa and @KTRaa and MachineNo % 2 = 0) )
		                                and IDXacNhanCaVaLamThem is null 
                                        and ( UserEnrollNumber = {0} )
                                order by UserEnrollNumber asc, TimeStr asc ";
            query = String.Format(query, String.Join(" or UserEnrollNumber = ", ArrDSMaCC_Checked));
            #endregion

            DataTable tableCIO_A = SqlDataAccessHelper.ExecuteQueryString(query
                , new[] { "@BDVao", "@KTVao", "@BDRaa", "@KTRaa" }
                , new object[] { ngayBD, ngayKT, ngayBD, ngayKT });
            return tableCIO_A;
        }

        public static DataTable LayTableCIO_V(int[] ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT)
        {
            string query = @"   SELECT	CIO.UserEnrollNumber
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
                                order by CIO.UserEnrollNumber asc, CIO.TimeStr asc ";
            query = String.Format(query, String.Join(" or CIO.UserEnrollNumber = ", ArrDSMaCC_Checked));
            DataTable tableCIO_V = SqlDataAccessHelper.ExecuteQueryString(query
                , new[] { "@BDVao", "@KTVao", "@BDRaa", "@KTRaa" }
                , new object[] { ngayBD, ngayKT, ngayBD, ngayKT });
            return tableCIO_V;
        }

        public static DataTable LayTableVang(int[] ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT)
        {
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

        public static DataTable LayTableLamViecNgayNghi(int[] ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT)
        {
            string query = @"   SELECT		ID, LamViecNgayNghi.UserEnrollNumber, UserFullName, Ngay, HeSoPC, 
								            case when HeSoPC = 1 then N'100% Lương' 
									             when HeSoPC = 2 then N'200% Lương'
									             when HeSoPC = 0.5 then N'50% Lương'
								            end as PhanTramHeSo 
					             FROM		LamViecNgayNghi, UserInfo
					             WHERE		( Ngay between @NgayBD and @NgayKT )  and ( LamViecNgayNghi.UserEnrollNumber = {0} )
							            and UserInfo.UserEnrollNumber =  LamViecNgayNghi.UserEnrollNumber
					             order by	LamViecNgayNghi.UserEnrollNumber asc, Ngay asc";
            query = String.Format(query, String.Join(" or LamViecNgayNghi.UserEnrollNumber = ", ArrDSMaCC_Checked));
            DataTable tableLamViecNgayNghi = SqlDataAccessHelper.ExecuteQueryString(query
                , new string[] { "@NgayBD", "@NgayKT" }
                , new object[] { ngayBD, ngayKT });
            return tableLamViecNgayNghi;
        }

		public static int ThemChamCongTay(int iUserEnrollNumber, DateTime timeBD, DateTime timeKT, int ShiftID, string ShiftCode, string onnduty, string offduty, int lategrace, int earlygrace, int afterot, int daycount, float wktime, float wkdayy, int sophutOT, bool tinhPC150, string lydo, string ghichu) {
			return SqlDataAccessHelper.ExecNoneQueryString(ThamSo.InsStrChamCongTay()
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
    }
}
