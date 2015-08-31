using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ChamCong_v06.Helper;
using ChamCong_v06.DTO;

namespace ChamCong_v06.DAL {
	public partial class DAO_LTR_Ca {
		public void Doc(ref List<cLichTrinh> DSLichTrinh) { 
			DataTable table = SqlDataAccessHelper.ExecSPQuery(SPName6.Schedule_DocLichTrinhV6.ToString());
			DataTable tableCa = SqlDataAccessHelper.ExecSPQuery(SPName6.Shift_DocTatCaShiftV6.ToString(), new SqlParameter("@Enable", true));
			//DataTable tableCaThuocLichTrinh = SqlDataAccessHelper.ExecSPQuery(SPName6.ShiftSch_DocDSCaV6.ToString())
			DSLichTrinh.Clear();
			foreach (DataRow dataRow in table.Rows)
			{
				cLichTrinh lichTrinh = new cLichTrinh { IDDescription = new ID_Description { ID = (int)dataRow["SchID"], Description = dataRow["SchName"].ToString() } };

			}
		}
	}
}
