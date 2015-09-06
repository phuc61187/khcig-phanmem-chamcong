using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ChamCong_v06.DTO;
using ChamCong_v06.Helper;

namespace ChamCong_v06.BUS {
	public class BUS_LichTrinh_Ca {

		internal void LayTatCaLichTrinhVaCa(ref List<cNhomCa> DSNhomCa) {
			//1. lập ds các ca đang enable trước, sau đó duyệt trong bảng phân bố để lọc các ca theo ID Lịch trình 
			List<cCa> DSTatCaCa;
			LapDSTatCaCa_ChamCong(out DSTatCaCa);
			DataTable tableNhomCa = SqlDataAccessHelper.ExecSPQuery(SPName6.Schedule_DocLichTrinhV6.ToString());
			DataTable tablePhanBoCaTheoNhom = SqlDataAccessHelper.ExecSPQuery(SPName6.ShiftSch_DocDSCaV6.ToString());
			DSNhomCa.Clear();
			//2. lập danh sách các lịch trình, sau đó mới duyệt bảng phân bố ca để đưa ds các ca vào
			foreach (DataRow dataRow in tableNhomCa.Rows) {
				cNhomCa nhomCa = new cNhomCa { 
					IDDescription = new ID_Description { ID = (int)dataRow["SchID"], Description = dataRow["SchName"].ToString()},
					DSCa = new List<cCa>()
				};
				DSNhomCa.Add(nhomCa);
			}
			// 3. dựa vào bảng phân bố xác định danh sách các ca 
			foreach (cNhomCa nhomCa in DSNhomCa)
			{
				// xác định các record của 1 lịch trình
				DataRow[] dataRows = tablePhanBoCaTheoNhom.Select("SchID=" + nhomCa.IDDescription.ID);
				// xác định ID của ca để add vào danh sách
				foreach (DataRow row in dataRows)
				{
					int shiftID = (int) row["T1"];
					cCa ca = (from cCa item in DSTatCaCa where item.ID == shiftID select item).SingleOrDefault();
					if (ca != null) nhomCa.DSCa.Add(ca);
				}
			}
		}

		public void LapDSTatCaCa_ChamCong(out List<cCa> DSCa)// lập danh sách tất cả các ca để chấm công (chỉ có enable )
		{
			DSCa = new List<cCa>();
			DataTable tableCa = SqlDataAccessHelper.ExecSPQuery(SPName6.Shift_DocTatCaShiftV6.ToString(), 
				new SqlParameter("@Enable", true));
			foreach (DataRow row in tableCa.Rows)
			{
				#region nạp dữ liệu

				cCa ca = new cCa();
				ca.ID = (int) row["ShiftID"];
				ca.Code = row["ShiftCode"].ToString();
				if (TimeSpan.TryParse(row["Onduty"].ToString(), out ca.Duty.From) == false)
				{
					ACMessageBox.Show("Không xử lý được dữ liệu Ca.", Resources.Caption_Loi, 2000);
					break;
				}
				if (TimeSpan.TryParse(row["Offduty"].ToString(), out ca.Duty.To) == false)
				{
					ACMessageBox.Show("Không xử lý được dữ liệu Ca.", Resources.Caption_Loi, 2000);
					break;
				}
				ca.DayCount = (int) row["DayCount"];
				ca.QuaDem = (ca.DayCount == 1);
				ca.Duty.To = ca.Duty.To.Add(new TimeSpan(ca.DayCount, 0, 0, 0));
				ca.NightTime = new FromToTimeSpan {From = GlobalVariables._22h00, To = GlobalVariables._6gHomSau};
				ca.OnTimeInMin = (int) row["OnTimeIn"];
				ca.CutInMin = (int) row["CutIn"];
				ca.OnTimeOutMin = (int) row["OnTimeOut"];
				ca.CutOutMin = (int) row["CutOut"];
				//nhận diện vào, nhận diện ra tự động tính toán bên kia
				ca.AfterOTMin = new TimeSpan(0, (int) row["AfterOT"], 0);
				ca.LateeMin = new TimeSpan(0, (int) row["LateGrace"], 0);
				ca.EarlyMin = new TimeSpan(0, (int) row["EarlyGrace"], 0);
				//tính timeOfDay ChoPhépTrễ, Sớm, AfterOT tính toán bên class

				var tOnLunch = TimeSpan.Zero;
				var tOffLunch = TimeSpan.Zero;
				if (row["OnLunch"] != DBNull.Value && row["OffLunch"] != DBNull.Value)
				{
					TimeSpan.TryParse(row["OnLunch"].ToString(), out tOnLunch);
					TimeSpan.TryParse(row["OffLunch"].ToString(), out tOffLunch);
				}
				ca.LunchMin = tOffLunch - tOnLunch;

				ca.WorkingTimeTS = new TimeSpan(0, int.Parse(row["WorkingTime"].ToString()), 0);
				ca.Workingday = (Single) row["Workingday"];
				ca.KyHieuCC = row["KyHieuCC"].ToString();
				ca.MoTa = row["Description"].ToString();
				ca.Is_CaTuDo = false;

				#endregion

				DSCa.Add(ca);
			}
		}
	}
}
