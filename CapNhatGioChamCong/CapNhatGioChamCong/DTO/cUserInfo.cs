using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CapNhatGioChamCong.BUS;
using CapNhatGioChamCong.DAO;

namespace CapNhatGioChamCong.DTO {
	public class cPhongBan {
		public int ID;
		public string TenPhongBan;
	}
	public class cUserInfo {
		#region Public Properties

		public string UserFullCode { get; set; }
		public string UserFullName { get; set; }
		public int UserEnrollNumber { get; set; }
		public cPhongBan BoPhan { get; set; }
		//public int UserIDTitle { get; set; }
		//public int UserPrivilege { get; set; }
		//public bool UserEnabled { get; set; }
		//public int UserIDC { get; set; }
		//public int UserIDD { get; set; }
		//public int SchID { get; set; }
		//public int UserGroup { get; set; }

		#endregion

		#region khai báo biến

		public bool MacDinhTinhPC150 { get; set; }
		public cShiftSchedule LichTrinhLV { get; set; }
		public List<cShift> DSCa { get; set; }
		public List<cShift> DSCaMoRong { get; set; }
		public List<cChk> ds_CheckAuto { get; set; }
		public List<cChk> ds_Check_ID { get; set; }
		public List<cChkInOut> DSVaoRa { get; set; }
		public List<cNgayCong> DSNgayCong { get; set; }
		#endregion

		public void ClearAll() {
			if (ds_CheckAuto == null) ds_CheckAuto = new List<cChk>();
			else ds_CheckAuto.Clear();
			if (ds_Check_ID == null) ds_Check_ID = new List<cChk>();
			else ds_Check_ID.Clear();
			if (DSVaoRa == null) DSVaoRa = new List<cChkInOut>();
			else DSVaoRa.Clear();
			if (DSNgayCong == null) DSNgayCong = new List<cNgayCong>();
			else DSNgayCong.Clear();

		}

		internal void Check_GioChuaXN(DateTime tempTimeStr, string pSource, int tempMachineNo) {
			if (tempMachineNo % 2 == 1) {
				cChkIn tChkIn = new cChkIn() { TimeStr = tempTimeStr, Source = pSource, MachineNo = tempMachineNo };
				ds_CheckAuto.Add(tChkIn);
			}
			else {
				cChkOut tChkOut = new cChkOut() { TimeStr = tempTimeStr, Source = pSource, MachineNo = tempMachineNo };
				ds_CheckAuto.Add(tChkOut);
			}
		}

		internal void Check_GioDaXN(DateTime pTimeStr, string pSource, int tempMachineNo
			, int pIDXacnhan, int pShiftID, string pShiftCode,
			TimeSpan pOnduty, TimeSpan pOffduty, TimeSpan pLateGrace, TimeSpan pEarlyGrace, TimeSpan pAfterOT, bool pTinhPC150,
			int pDayCount, TimeSpan pWorkingTime, Single pWorkingday
			, DateTime pTimeStrIn, DateTime pTimeStrOut, int pOTMin, string pNote) {

			if (tempMachineNo % 2 == 1) {
				cChkIn tChkIn = new cChkIn() { TimeStr = pTimeStr, Source = pSource, MachineNo = tempMachineNo };
				ds_Check_ID.Add(tChkIn);
			}
			else {
				cChkOut tChkOut = new cChkOut() { TimeStr = pTimeStr, Source = pSource, MachineNo = tempMachineNo };
				ds_Check_ID.Add(tChkOut);

				if (ds_Check_ID.Count > 1 && ds_Check_ID[ds_Check_ID.Count - 2].GetType() == typeof(cChkIn)) {
					cChkIn tmpChkIn = (cChkIn)ds_Check_ID[ds_Check_ID.Count - 2];
					cChkOut tmpChkOut = (cChkOut)ds_Check_ID[ds_Check_ID.Count - 1];
					cChkInOut tmpChkINOUT = new cChkInOut() {
						Vao = tmpChkIn, Raa = tmpChkOut, HaveINOUT = 1,
                        TongGioThuc = tmpChkOut.TimeStr - tmpChkIn.TimeStr,
						DaXN = true, IsOT = true, TinhPC150 = pTinhPC150,
						LamThem = new TimeSpan(0, pOTMin, 0),
						ThuocNgayCong = ThamSo.GetDate(tmpChkIn.TimeStr), // [TBD] xem lại thuộc ngày công
					};
					cShift tmpThuocCa;
					if (pShiftID > 0) { // ca trong ds ca
						tmpThuocCa = DSCa.Find(item => item.ShiftID == pShiftID); 
					}
					else if (pShiftID > int.MinValue + 100 && pShiftID < 0) // ca tách và ca kết hợp  [Chú ý] + 100 vì chừa khoảng này cho các loại khác
						tmpThuocCa = new cShift() {
							ShiftID = pShiftID, ShiftCode = pShiftCode,
							OnnDutyTS = pOnduty, OffDutyTS = pOffduty, chophepvaotreTS = pLateGrace, chopheprasomTS = pEarlyGrace, batdaulamthemTS = pAfterOT,
							WorkingTimeTS = pWorkingTime, Workingday = pWorkingday, DayCount = pDayCount, QuaDem = (pDayCount == 1), LunchMinute = ThamSo._0gio, LoaiCa = 0
						};//[TBD]
					else if (pShiftID < int.MinValue + 100)
						tmpThuocCa = new cShift() {
							ShiftID = pShiftID, ShiftCode = pShiftCode,
							OnnDutyTS = pOnduty, OffDutyTS = pOffduty, chophepvaotreTS = pLateGrace, chopheprasomTS = pEarlyGrace, batdaulamthemTS = pAfterOT,
							WorkingTimeTS = pWorkingTime, Workingday = pWorkingday, DayCount = pDayCount, QuaDem = (pDayCount == 1), LunchMinute = ThamSo._0gio, LoaiCa = 1
						};
					else {
						tmpThuocCa = new cShift() {
							ShiftID = pShiftID, ShiftCode = "XacNhan8h",
							OnnDutyTS = pOnduty, OffDutyTS = pOffduty, chophepvaotreTS = pLateGrace, chopheprasomTS = pEarlyGrace, batdaulamthemTS = pAfterOT,
							WorkingTimeTS = pWorkingTime, Workingday = pWorkingday, DayCount = pDayCount, QuaDem = (pDayCount == 1), LunchMinute = ThamSo._0gio, LoaiCa = 1
						}; //[TBD]
					}
					if (tmpThuocCa == null) {
						log4net.ILog log = log4net.LogManager.GetLogger("ERROR function Check_GioDaXN ");
						log.Fatal("ERROR function Check_GioDaXN");
					}

					tmpChkINOUT.ThuocCa = tmpThuocCa;
					tmpChkINOUT.QuaDem = tmpThuocCa.QuaDem;
					XL.TinhCongTheoCa(tmpChkINOUT, tmpChkINOUT.ThuocCa);
					DSVaoRa.Add(tmpChkINOUT);
				}
			}
		}
		public override string ToString() {
			return " UEN=" + UserEnrollNumber + "; Ten=" + UserFullName + "__\n";
		}


		#region sử dụng để tính công

		public Single HeSoLuongCB;
		public Single HeSoLuongSP;

		#endregion

		// [BackupFunction06]

		public cUserInfo() {
		}


		public Double TongCongThang { get; set; }

		public int TongNgayQuaDem { get; set; }

		public Double TongCongPhep { get; set; }
		public Double TongCongCV { get; set; }

		public Double TongCongBH { get; set; }

		public Double TongCongH_CT_PT { get; set; }

		public Double TongCongRo { get; set; }

		public Double TongPCapThang { get; set; }

		public Double TienLuong { get; set; }

		public cLuongThang Luong { get; set; }

	}

	public class cLuongThang {
		public Double LuongCB { get; set; }
		public Double LuongSP { get; set; }
		public Double BoiDuongQuaDem { get; set; }
		public Double HSLuongSPQuyDoi { get; set; }
		public Double LuongThangTruoc { get; set; }
		public Double TongLuong { get; set; }
		public cLuongThang() {
			LuongCB = 0d; LuongSP = 0d;
			BoiDuongQuaDem = 0d;
			HSLuongSPQuyDoi = 0d; LuongThangTruoc = 0d; TongLuong = 0d;
		}
	}
}
