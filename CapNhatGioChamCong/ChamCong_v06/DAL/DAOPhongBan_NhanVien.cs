using System;
using System.Data;
using System.Data.SqlClient;
using ChamCong_v06.Helper;

namespace ChamCong_v06.DAL {
	public static partial class DAO5 {
		//ok ver 8
		public static DataTable LayDSNV(int[] arrIDPhongBan = null) {
			#region
			var query = @"  select	UserFullCode, UserFullName, UserEnrollNumber, UserLastName, UserEnrollName,
									UserIDD as MaPhong, 
									case when (UserIDD is null or UserIDD = 0) then N'--' else RelationDept.Description end as TenPhong, RelationDept.TinhPC50, RelationDept.ViTri,
 
									UserIDTitle as IDChucVu, case when (UserInfo.UserIDTitle is null or UserInfo.UserIDTitle = 0)  then N'Chưa SX' else TitleName end as ChucVu, 

									UserInfo.SchID, case when (UserInfo.SchID is null or UserInfo.SchID = 0)  then N'--' else SchName end as SchName,   

									HeSoLuongCB, HeSoLuongSP, 
									UserCardNo, HSBHCongThem, 
									case when ( UserSex = 0 ) then N'Nam' else N'Nữ' end as UserSex, 
									UserBirthDay, UserEnabled, UserHireDay, UserPrivilege 

							FROM	UserInfo left join Title on UserInfo.UserIDTitle = Title.IDT
									left join RelationDept on UserInfo.UserIDD = RelationDept.ID
									left join Schedule on UserInfo.SchID = Schedule.SchID
							{0}
							order by UserFullCode "; //{0} là điều kiện where nếu lấy theo phòng ban, ngược lại là chuỗi rỗng
			#endregion
			if (arrIDPhongBan == null)// lấy hết
			{
				query = String.Format(query, String.Empty);
			}
			else// lấy theo phòng ban
			{
				string temp2 = " UserIDD > 0 and ( UserIDD = {0} )";
				temp2 = String.Format(temp2, String.Join(" or UserIDD = ", arrIDPhongBan)); // --> [ useridd > 0 and (useridd = 1 or useridd = 2) ]
				query = String.Format(query, " where " + temp2);// --> where [ useridd > 0 and (useridd = 1 or useridd = 2) ]

			}

			var table = SqlDataAccessHelper.ExecuteQueryString(query, null, null);
			return table;
		}

		//ok ver 8
		public static DataTable LayDSPhong(int userID) {
			#region query
			const string query = @"   SELECT  RelationDept.ID as ID, RelationID,Description, ViTri, IsYes
									  FROM    RelationDept, DeptPrivilege
										WHERE   UserID = @UserID AND IsYes = 1 AND DeptPrivilege.IDD = RelationDept.ID  
										order by ViTri asc ";
			#endregion

			var kq = SqlDataAccessHelper.ExecuteQueryString(query, new[] { "@UserID" }, new object[] { userID });

			return kq;

		}

		//ok ver 8
		public static DataTable LayDSTatCaPhongBan() {
			#region query
			const string query = @"   SELECT  ID,RelationID,Description,ViTri                                        
									  FROM    RelationDept  
									  order by	ViTri asc";
			#endregion

			var kq = SqlDataAccessHelper.ExecuteQueryString(query, null, null);

			return kq;

		}


		public static DataTable LoadDataSourceChucVu(bool loadDefaultRow) {
			var tableChucVu = SqlDataAccessHelper.ExecuteQueryString("select IDT as IDChucVu, TitleName as ChucVu from Title");
			if (loadDefaultRow) {
				DataRow defaultRowChucVu = tableChucVu.NewRow();
				defaultRowChucVu["IDChucVu"] = 0;
				defaultRowChucVu["ChucVu"] = "Chưa Sắp xếp";
				tableChucVu.Rows.InsertAt(defaultRowChucVu, 0);
			}

			return tableChucVu;
		}

		public static DataTable LoadDataSourcePhongBan(bool loadDefaultRow) {
			var tablePhongBan = LayDSTatCaPhongBan();
			if (loadDefaultRow) {
				DataRow defaultRowPhong = tablePhongBan.NewRow();
				defaultRowPhong["ID"] = 0;
				defaultRowPhong["Description"] = "Chưa Sắp xếp";
				tablePhongBan.Rows.InsertAt(defaultRowPhong, 0);
			}
			return tablePhongBan;
		}

		public static bool ChangePassword(string OldPass, string NewPass, string Account)//v6
		{
			int kq1 = SqlDataAccessHelper.ExecSPNoneQuery(SPName.UserInfo_ChangePass.ToString(), //todo viết store
				new SqlParameter("OldPassword", OldPass), 
				new SqlParameter("NewPassword", NewPass), 
				new SqlParameter("UserAccount", Account));
			if (kq1 > 0) return true;
			return false;
		}

		public static DataTable LoadDataSourceShift()
		{
			DataTable tableShift = SqlDataAccessHelper.ExecSPQuery(SPName6.Shift_DocTatCaShift.ToString());
			DataTable kq = new DataTable();
			kq.Columns.Add(new DataColumn("ShiftCode", typeof (string)));
			kq.Columns.Add(new DataColumn("ShiftID", typeof (int)));
			kq.Columns.Add(new DataColumn("Enable", typeof (bool)));
			kq.Columns.Add(new DataColumn("OnDuty", typeof (TimeSpan)));
			kq.Columns.Add(new DataColumn("OffDuty", typeof (TimeSpan)));
			kq.Columns.Add(new DataColumn("WorkingTime", typeof (TimeSpan)));
			kq.Columns.Add(new DataColumn("Workingday", typeof (float)));
			kq.Columns.Add(new DataColumn("LateGrace", typeof (int)));
			kq.Columns.Add(new DataColumn("EarlyGrace", typeof (int)));
			kq.Columns.Add(new DataColumn("AfterOT", typeof (int)));
			kq.Columns.Add(new DataColumn("OnTimeIn", typeof (TimeSpan)));
			kq.Columns.Add(new DataColumn("CutIn", typeof (TimeSpan)));
			kq.Columns.Add(new DataColumn("OnTimeOut", typeof (TimeSpan)));
			kq.Columns.Add(new DataColumn("CutOut", typeof (TimeSpan)));
			kq.Columns.Add(new DataColumn("KyHieuCC", typeof (string)));
			
			foreach (DataRow row in tableShift.Rows)
			{
				DataRow newRow = kq.NewRow();
				bool disableCSDL = (row["Disable"] != DBNull.Value) ? (bool) row["Disable"] : false;
				bool enableColumn = !disableCSDL; // row[""] ToString()
				string shiftcode = row["ShiftCode"].ToString();
				int shiftID = (int)row["ShiftID"];
				TimeSpan onDuty = TimeSpan.Parse(row["OnDuty"].ToString());
				int dayCount = (int) row["DayCount"];
				TimeSpan offDuty = TimeSpan.Parse(row["OffDuty"].ToString());
				offDuty = offDuty.Add(new TimeSpan(dayCount, 0, 0, 0));
				TimeSpan tongGioLam = new TimeSpan(0, 0, Convert.ToInt32(row["WorkingTime"]), 0);
				float chamCong = (float) (row["Workingday"]);
				int trePhut = (int) row["LateGrace"];
				int somPhut = (int) row["EarlyGrace"];
				int lamThemToiThieu = (int) row["AfterOT"];
				int OnTimeInMinute = (int) row["OnTimeIn"];
				int CutInMinute = (int) row["CutIn"];
				int OnTimeOutMinute = (int) row["OnTimeOut"];
				int CutOutMinute = (int)row["CutOut"];
				TimeSpan OnTimeIn = onDuty.Subtract(new TimeSpan(0, OnTimeInMinute, 0));
				TimeSpan CutIn = onDuty.Add(new TimeSpan(0, CutInMinute, 0));
				TimeSpan OnTimeOut = offDuty.Subtract(new TimeSpan(0, OnTimeOutMinute, 0));
				TimeSpan CutOut = offDuty.Add(new TimeSpan(0, CutOutMinute, 0));

				string kyHieuCC = row["KyHieuCC"].ToString();

				newRow["ShiftCode"] = shiftcode;
				newRow["ShiftID"] = shiftID;
				newRow["Enable"] = enableColumn;
				newRow["OnDuty"] = onDuty;
				newRow["OffDuty"] = offDuty;
				newRow["WorkingTime"] = tongGioLam;
				newRow["Workingday"] = chamCong;
				newRow["LateGrace"] = trePhut;
				newRow["EarlyGrace"] = somPhut;
				newRow["AfterOT"] = lamThemToiThieu;
				newRow["OnTimeIn"] = OnTimeIn;
				newRow["CutIn"] = CutIn;
				newRow["OnTimeOut"] = OnTimeOut;
				newRow["CutOut"] = CutOut;
				newRow["KyHieuCC"] = kyHieuCC;
				kq.Rows.Add(newRow);
			}
			return kq;
		}
	}
}
