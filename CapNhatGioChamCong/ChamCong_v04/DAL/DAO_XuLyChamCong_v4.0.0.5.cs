using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamCong_v04.Helper;
using System.Data.SqlClient;
using System.Data;
using ChamCong_v04.BUS;

namespace ChamCong_v04.DAL
{
	public static partial class DAO
	{
		public static DataTable GetTableCheckAuto(List<int> ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT) {
			DataTable tableArrMaCC = MyUtility.Array_To_DataTable("TableMaCC", ArrDSMaCC_Checked);
			SqlParameter paramArrMaCC = new SqlParameter("@ArrayMaCC", SqlDbType.Structured) { Value = tableArrMaCC };

			var startVao = ngayBD.Add(XL2._18h00);
			var endddVao = ngayKT.Add(XL2._04h30);
			var startRaa = ngayBD.AddDays(1d).Add(XL2._07h00);
			var endddRaa = ngayKT.Add(XL2._18h00) ;
			DataTable result = SqlDataAccessHelper.ExecSPQuery(SPName.sp_CheckInOut_DocCheckVanTay.ToString(),
				paramArrMaCC,
				new SqlParameter("@BDVao", startVao),
				new SqlParameter("@KTVao", endddVao),
				new SqlParameter("@BDRaa", startRaa),
				new SqlParameter("@KTRaa", endddRaa)
				);
			
			return result;
		}

		public static DataTable GetTableCheckDaXN(List<int> ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT) {
			DataTable tableArrMaCC = MyUtility.Array_To_DataTable("TableMaCC", ArrDSMaCC_Checked);
			SqlParameter paramArrMaCC = new SqlParameter("@ArrayMaCC", SqlDbType.Structured) { Value = tableArrMaCC };

			var startVao = ngayBD.Add(XL2._18h00);
			var endddVao = ngayKT.Add(XL2._04h30);
			var startRaa = ngayBD.AddDays(1d).Add(XL2._07h00);
			var endddRaa = ngayKT.Add(XL2._18h00);
			DataTable result = SqlDataAccessHelper.ExecSPQuery(SPName.sp_CheckInOut_DocCheckDaCoXN.ToString(),
				paramArrMaCC,
				new SqlParameter("@BDVao", startVao),
				new SqlParameter("@KTVao", endddVao),
				new SqlParameter("@BDRaa", startRaa),
				new SqlParameter("@KTRaa", endddRaa)
				);
			return result;
		}

		public static DataTable GetTableXPVang(DateTime ngayBD, DateTime ngayKT, List<int> ArrDSMaCC_Checked) {
			DataTable tableArrMaCC = MyUtility.Array_To_DataTable("TableMaCC", ArrDSMaCC_Checked);
			SqlParameter paramArrMaCC = new SqlParameter("@ArrayMaCC", SqlDbType.Structured) { Value = tableArrMaCC };

			DataTable result = SqlDataAccessHelper.ExecSPQuery(SPName.sp_Absent_DocNVVang.ToString(),
				paramArrMaCC,
				new SqlParameter("@NgayBD", ngayBD),
				new SqlParameter("@NgayKT", ngayKT)
				);
			return result;
		}

		public static DataTable GetTableXNPC50(DateTime ngayBD, DateTime ngayKT, List<int> ArrDSMaCC_Checked) {
			DataTable tableArrMaCC = MyUtility.Array_To_DataTable("TableMaCC", ArrDSMaCC_Checked);
			SqlParameter paramArrMaCC = new SqlParameter("@ArrayMaCC", SqlDbType.Structured) { Value = tableArrMaCC };

			DataTable result = SqlDataAccessHelper.ExecSPQuery(SPName.XacNhanPC50_DocXNPC50.ToString(),
				paramArrMaCC,
				new SqlParameter("@NgayBD", ngayBD),
				new SqlParameter("@NgayKT", ngayKT)
				);
			return result;
		}
		public static DataTable GetTableXNPCDB(DateTime ngayBD, DateTime ngayKT, List<int> ArrDSMaCC_Checked, bool Duyet= true) {
			DataTable tableArrMaCC = MyUtility.Array_To_DataTable("TableMaCC", ArrDSMaCC_Checked);
			SqlParameter paramArrMaCC = new SqlParameter("@ArrayMaCC", SqlDbType.Structured) { Value = tableArrMaCC };

			DataTable result = SqlDataAccessHelper.ExecSPQuery(SPName.XacNhanPC_DocXNPC.ToString(),
				paramArrMaCC,
				new SqlParameter("@NgayBD", ngayBD),
				new SqlParameter("@NgayKT", ngayKT)
				);
			return result;
		}	
	}
}
