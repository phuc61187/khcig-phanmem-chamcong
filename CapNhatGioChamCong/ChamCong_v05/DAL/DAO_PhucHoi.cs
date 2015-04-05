using System;
using System.Data;
using ChamCong_v05.Helper;

namespace ChamCong_v05.DAL {
	public static partial class DAO5 {
		public static DataTable LayBang_PH_Them(int[] arrMaCC, DateTime ngayBD, DateTime ngayKT, DataTable Bang_PH_Them) {
			var query = @"  SELECT    CheckInOut.UserEnrollNumber,UserFullCode,UserFullName,
									  case when MachineNo%2 = 1 then N'Vào'
									  else N'Ra' end as Type,
									  TimeStr,MachineNo,Source,                                      
									  Them
									FROM    CheckInOut, UserInfo
									WHERE (CheckInOut.TimeStr between @NgayBD and @NgayKT)
										and Them = 1 and (IDXNCa_LamThem is null)
										and CheckInOut.UserEnrollNumber = UserInfo.UserEnrollNumber
										and ( CheckInOut.UserEnrollNumber = {0} )";
			var temp = String.Join(" or CheckInOut.UserEnrollNumber = ", arrMaCC);
			query = String.Format(query, temp);

			var kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@NgayBD", "@NgayKT" },
															new object[] { ngayBD, ngayKT });
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
			var temp = String.Join(" or CheckInOut.UserEnrollNumber = ", arrMaCC);
			query = String.Format(query, temp);

			var kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@NgayBD", "@NgayKT" },
															new object[] { ngayBD, ngayKT });
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
										and (Xoa is null or Xoa = 0) and (IDXNCa_LamThem is null)
										and (CheckInOut.IDGioGoc = GioGoc.IDGioGoc)
										and CheckInOut.UserEnrollNumber = UserInfo.UserEnrollNumber                                        
										and ( CheckInOut.UserEnrollNumber = {0} )
							order by	IDGioGoc ";


			var temp = String.Join(" or CheckInOut.UserEnrollNumber = ", arrMaCC);
			query = String.Format(query, temp);

			var kq = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@NgayBD", "@NgayKT" },
															new object[] { ngayBD, ngayKT });
			return kq;
		}

		public static int PhucHoiGioThem(int UserEnrollNumber, int MachineNo, DateTime TimeStr) {
			var query = @"	delete from		CheckInOut 
								where		UserEnrollNumber = @UserEnrollNumber and MachineNo = @MachineNo and TimeStr = @TimeStr";
			var kq = SqlDataAccessHelper.ExecNoneQueryString(query,
															 new string[] { "@UserEnrollNumber", "@MachineNo", "@TimeStr" },
															 new object[] { UserEnrollNumber, MachineNo, TimeStr });
			return kq;
		}

		public static int PhucHoiGioXoa(int UserEnrollNumber, int MachineNo, DateTime TimeStr) {
			var query = @"	update CheckInOut set Xoa = null 
								where		UserEnrollNumber = @UserEnrollNumber and MachineNo = @MachineNo and TimeStr = @TimeStr";
			var kq = SqlDataAccessHelper.ExecNoneQueryString(query,
															 new string[] { "@UserEnrollNumber", "@MachineNo", "@TimeStr" },
															 new object[] { UserEnrollNumber, MachineNo, TimeStr });
			return kq;
		}

		public static int PhucHoiGioGoc(int UserEnrollNumber, int CurrMachineNo, DateTime CurrTimeStr, string CurrSource, int OrinMachineNo, DateTime OrinTimeStr, string OrinSource, int IDGioGoc) {
			var query = @"	update	CheckInOut
							set		MachineNo = @OrinMachineNo, TimeStr = @OrinTimeStr, Source = @OrinSource, IDGioGoc = null
							where UserEnrollNumber = @UserEnrollNumber and MachineNo = @CurrMachineNo and TimeStr = @CurrTimeStr and Source = @CurrSource";
			var kq = SqlDataAccessHelper.ExecNoneQueryString(query,
															 new string[] { "@UserEnrollNumber", "@CurrMachineNo", "@CurrTimeStr", "@CurrSource", "@OrinMachineNo", "@OrinTimeStr", "@OrinSource", "@IDGioGoc" },
															 new object[] { UserEnrollNumber, CurrMachineNo, CurrTimeStr, CurrSource, OrinMachineNo, OrinTimeStr, OrinSource, IDGioGoc });
			return kq;
		}

		public static DataTable LayBang_GioDaXN(int[] arrMaCC, DateTime ngayBD, DateTime ngayKT, DataTable m_Bang_GioDaXN) {
			var query = @"	select	CheckInOut.UserEnrollNumber, TimeDate, TimeStr, MachineNo, IDXNCa_LamThem 
							from	CheckInOut	
							where	IDXNCa_LamThem is not null
								and MachineNo %2 = 1 and TimeStr between  @NgayBD and @NgayKT
								and (CheckInOut.UserEnrollNumber = {0})
								";
			query = String.Format(query, String.Join(" or CheckInOut.UserEnrollNumber = ", arrMaCC));
			var table1 = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@NgayBD", "@NgayKT" },
																new object[] { ngayBD, ngayKT });
			return table1;
		}
		public static DataTable LayBang_GioDaXN2(int[] arrMaCC, DateTime ngayBD, DateTime ngayKT, DataTable m_Bang_GioDaXN) {
			var query = @"	select	UserFullCode, UserFullName, CheckInOut.UserEnrollNumber, TimeStr, IDXNCa_LamThem 
							from	CheckInOut	, UserInfo
							where	IDXNCa_LamThem is not null
								and MachineNo %2 = 1 and TimeStr between  @NgayBD and @NgayKT
								and UserInfo.UserEnrollNumber = CheckInOut.UserEnrollNumber
								and (CheckInOut.UserEnrollNumber = {0})
								";
			query = String.Format(query, String.Join(" or CheckInOut.UserEnrollNumber = ", arrMaCC));
			var table1 = SqlDataAccessHelper.ExecuteQueryString(query, new string[] { "@NgayBD", "@NgayKT" },
																new object[] { ngayBD, ngayKT });
			return table1;
		}


		internal static DataTable LayBang_GioDaXN3(int[] arrMaCC, DataTable m_Bang_GioDaXN) {
			var query = @"	select CheckInOut.UserEnrollNumber, TimeStr, IDXNCa_LamThem ,ShiftID, ShiftCode, OTMin ,DuyetChoPhepVaoTre, DuyetChoPhepRaSom
							from CheckInOut	, XNCa_LamThem
							where MachineNo %2 = 0 and IDXNCa_LamThem = ID and (IDXNCa_LamThem = {0})
							";
			query = string.Format(query, string.Join(" or IDXNCa_LamThem = ", arrMaCC));
			var table1 = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return table1;
		}


		public static void HuyXN_GioChamCong(int ID) {
			var query = @"	update CheckInOut set IDXNCa_LamThem = null where IDXNCa_LamThem = @IDXNCa_LamThem 
							delete XNCa_LamThem where ID = @IDXNCa_LamThem ";
			SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@IDXNCa_LamThem" }, new object[] { ID });
		}

	}
}
