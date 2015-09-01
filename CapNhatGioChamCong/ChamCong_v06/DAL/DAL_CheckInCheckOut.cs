using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ChamCong_v06.DTO;
using ChamCong_v06.Helper;

namespace ChamCong_v06.DAL {
	public partial class DAL_CheckInCheckOut {
		public void GetCheckInCheckOutData(FromToDateTime KhoangThoiGian, List<int> ArrayUEN, out List<cCheck> ResultListCheck)
		{
			DataTable tableArrayUEN = MyUtility.Array_To_DataTable("tableName", ArrayUEN);
			DataTable tableCheck = SqlDataAccessHelper.ExecSPQuery(SPName6.CheckInOut_DocCheckChuaXuLyV6.ToString(),
			                                                       new SqlParameter("@From", KhoangThoiGian.From),
			                                                       new SqlParameter("@To", KhoangThoiGian.To),
			                                                       new SqlParameter{ ParameterName = "@ArrayUserEnrollNumber", SqlDbType = SqlDbType.Structured, Value = tableArrayUEN},
																   new SqlParameter("@DaXuLy", false),
																   new SqlParameter("@Loai", false));
			ResultListCheck = new List<cCheck>();
			foreach (DataRow dataRow in tableCheck.Rows)
			{
				cCheck check = new cCheck
					{
						MaCC = (int) dataRow["UserEnrollNumber"],
						Source = dataRow["Source"].ToString(),
						Time = (DateTime) dataRow["TimeStr"],
						MachineNo = (int) dataRow["MachineNo"],
						TypeColumn = dataRow["Type"].ToString()
					};
				check.Type = (check.MachineNo%2 == 1) ? "I" : "O";
				ResultListCheck.Add(check);
			}

		}

		internal void LoaiCheckTrong30ph(List<cCheck> DSCheck_BiLoai_All) {
			int kq = 0;
			bool flag = false;
			foreach (cCheck check in DSCheck_BiLoai_All)
			{
				kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.CheckInOut_UpdCheckV6.ToString(),
				                                         new SqlParameter("@UserEnrollNumber", check.MaCC),
				                                         new SqlParameter("@TimeStr", check.Time),
				                                         new SqlParameter("@MachineNo", check.MachineNo),
				                                         new SqlParameter("@Loai", true));
				if (kq == 0)
				{
					flag = true;
				}
			}
			if (flag)
			{
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
			}
		}
	}
}
