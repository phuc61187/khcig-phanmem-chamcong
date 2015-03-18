using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace GiuLaiCacFileCu {

/*

	public class clsNgayCong_v71 {

		public DateTime NgayCong { get; set; }
		public List<cChk> DSVaoHL_v71 { get; set; }
		public List<cChk> DSRaHL_v71 { get; set; }
		public List<cChkInOut> DSVaoRaHL_v11 { get; set; }

		public List<cChk> DSVaoKhac_ChuaXL_v72 { get; set; }
		public List<cChk> DSRaKhac_ChuaXL_v72 { get; set; }
		public List<cChkInOut> DSVaoRaKhac_v11 { get; set; }

		public List<cChk> DSVaoKhac_DaXL_v10 { get; set; }
		public List<cChk> DSRaKhac_DaXL_v10 { get; set; }
		public List<cChkInOut> DSVaoRaKhac_DaXL_v11 { get; set; }

		public List<cChkInOut> DSVaoRaTH_v11;

		public bool Tren2Ca { get; set; }
		public TimeSpan TongGioLam { get; set; }
		public TimeSpan TongGioThuc { get; set; }
		public float TongCongNgay { get; set; }
		public TimeSpan TongTre { get; set; }
		public TimeSpan TongSom { get; set; }
		public float TongPhuCap { get; set; }
	}

	public class cQuyGioTichLuy {
		public DateTime Thang { get; set; }
		public int SumMinutes { get; set; }
		public int Used { get; set; }
	}

	public class cUserInfo {
		public static readonly TimeSpan _0gio = TimeSpan.Zero;
		public static readonly TimeSpan _10phut = new TimeSpan(0, 10, 0);
		public static readonly TimeSpan _30phut = new TimeSpan(0, 30, 0);
		public static readonly TimeSpan _4gio = new TimeSpan(4, 0, 0);
		public static readonly TimeSpan _8gio = new TimeSpan(8, 0, 0);
		public static readonly TimeSpan _16gio = new TimeSpan(16, 0, 0);
		public static readonly TimeSpan _05h45 = new TimeSpan(5, 45, 0);
		public static readonly TimeSpan _13h45 = new TimeSpan(13, 45, 0);
		public static readonly TimeSpan _21h45 = new TimeSpan(21, 45, 0);

		#region Public Properties

		public string UserFullCode { get; set; }

		public string UserFullName { get; set; }

		public int UserEnrollNumber { get; set; }

		public int UserIDTitle { get; set; }

		public string TenLichTrinh { get; set; }

		public string TenPhongBan { get; set; }

		public int UserPrivilege { get; set; }

		public bool UserEnabled { get; set; }

		public int UserIDC { get; set; }

		public int UserIDD { get; set; }

		public int SchID { get; set; }

		public int UserGroup { get; set; }

		//public string UserLastName { get; set; }

		//public string UserEnrollName { get; set; }

		//public string UserCardNo { get; set; }

		//public DateTime UserHireDay { get; set; }

		//public int UserSex { get; set; }

		//public string UserBirthDay { get; set; }

		//public int UserBirthPlace { get; set; }

		//public Image UserPhoto { get; set; }

		//public string UserNoted { get; set; }

		//public string UserPW { get; set; }

		//public string UserTZ { get; set; }

		//public string UserPIN1 { get; set; }

		//public string PushCardID { get; set; }

		#endregion

		#region khai báo biến

		public cQuyGioTichLuy QuyGioTL { get; set; }

		public cShiftSchedule LichTrinhLV { get; set; }
		public List<cShift> DSCa { get; set; }

		public List<cChk> DSVao { get; set; }
		public List<cChk> DSRa { get; set; }

		public List<cChk> DSGVaoKhac_ChoXL1_v72;
		public List<cChk> DSGRaKhac_ChoXL1_v72;

		public List<cChk> DSVaoKhacChuaXL2_v72;
		public List<cChk> DSRaKhacChuaXL2_v72;

		public List<cChk> DSVaoHopLe;
		public List<cChk> DSRaHopLe;

		public List<cChkInOut> DSVaoRaHopLe;
		public List<cChkInOut> DSVaoRaKhacChuaXL;

		public List<cChk> DSGVaoBatThuong;
		public List<cChk> DSGRaBatThuong;

		public List<cChk> DSGioRaSai_v4;
		public List<cChk> DSGioVaoSai_v4;
		public List<clsNgayCong_v71> DSNgayCong_v71;

		public List<cChk> DSG_ChuaXL_v10;
		public List<cChk> DSG_DaXL_v10 { get; set; }

		#endregion

		#region method

		public void Them_GioChuaXL_v10(cChk pGio) {
			if (DSG_ChuaXL_v10 == null) DSG_ChuaXL_v10 = new List<cChk>();
			if (DSG_ChuaXL_v10.Count == 0) {
				XetThuocCa_v3(pGio, LichTrinhLV);
				DSG_ChuaXL_v10.Add(pGio);
				return;
			}
			// dsgio.Count > 0
			if (((pGio.MachineNo % 2) == (DSG_ChuaXL_v10[DSG_ChuaXL_v10.Count - 1].MachineNo % 2))
				&& pGio.TimeStr - DSG_ChuaXL_v10[DSG_ChuaXL_v10.Count - 1].TimeStr < _30phut) {
				if (DSG_ChuaXL_v10[DSG_ChuaXL_v10.Count - 1].ListGioLQ_v3 == null) DSG_ChuaXL_v10[DSG_ChuaXL_v10.Count - 1].ListGioLQ_v3 = new List<cChk>();
				DSG_ChuaXL_v10[DSG_ChuaXL_v10.Count - 1].ListGioLQ_v3.Add(pGio);
			}
			else {
				XetThuocCa_v3(pGio, LichTrinhLV);
				DSG_ChuaXL_v10.Add(pGio);
			}

		}
		public void Them_GioDaXL_v10(cChk pGio) {
			if (DSG_DaXL_v10 == null) DSG_DaXL_v10 = new List<cChk>();

			if (DSG_DaXL_v10.Count == 0) {
				XetThuocCa_v3(pGio, LichTrinhLV);
				DSG_DaXL_v10.Add(pGio); return;
			}
			if (pGio.TimeStr - DSG_DaXL_v10[DSG_DaXL_v10.Count - 1].TimeStr < _30phut
				&& pGio.Type == DSG_DaXL_v10[DSG_DaXL_v10.Count - 1].Type) {
				if (DSG_DaXL_v10[DSG_DaXL_v10.Count - 1].ListGioLQ_v3 == null)
					DSG_DaXL_v10[DSG_DaXL_v10.Count - 1].ListGioLQ_v3 = new List<cChk>();
				DSG_DaXL_v10[DSG_DaXL_v10.Count - 1].ListGioLQ_v3.Add(pGio);
			}
			else {
				XetThuocCa_v3(pGio, LichTrinhLV);
				DSG_DaXL_v10.Add(pGio);
			}
		}
		public cChk XetThuocCa_v3(cChk pGio, cShiftSchedule pLichTrinh) {
			cChk kq = pGio;
			TimeSpan temptre = _0gio;
			TimeSpan tempsom = _0gio;
			int i;
			if (kq.MachineNo % 2 == 1) {
				//check in
				for (i = 0; i < pLichTrinh.ListT1.Count; i++) {
					if (kq.TimeStr.TimeOfDay < pLichTrinh.ListT1[i].OnTimeIn || kq.TimeStr.TimeOfDay > pLichTrinh.ListT1[i].CutIn) continue; // ko thuộc ca, skip

					// thuộc ca => tính đi trễ
					if (kq.TimeStr.TimeOfDay > pLichTrinh.ListT1[i].Onduty + pLichTrinh.ListT1[i].LateGrace)
						temptre = kq.TimeStr.TimeOfDay - pLichTrinh.ListT1[i].Onduty;
					pLichTrinh.ListT1[i].tre = temptre;
					pLichTrinh.ListT1[i].som = _0gio;
					if (kq.ThuocCa == null || kq.ThuocCa.Count == 0) kq.ThuocCa = new List<cShift>() { pLichTrinh.ListT1[i] };
					else kq.ThuocCa.Add(pLichTrinh.ListT1[i]);
					temptre = tempsom = _0gio;
				}
			}
			else {
				//check out
				for (i = 0; i < pLichTrinh.ListT1.Count; i++) {
					TimeSpan tempTimeStr = kq.TimeStr.TimeOfDay;
					// nếu lúc getLịch trình có add thêm 1 day timespan thì ở đây phải trừ lại 1 day, còn ko lấy thì ko trừ
					//tempTimeStr = tempTimeStr + new TimeSpan(pLichTrinh.ListT1[i].DayCount , 0 , 0 , 0);
					if (tempTimeStr < pLichTrinh.ListT1[i].OnTimeOut || tempTimeStr > pLichTrinh.ListT1[i].CutOut) continue; // ko thuộc ca, skip

					// thuộc ca => tính về sớm
					if (kq.TimeStr.TimeOfDay < pLichTrinh.ListT1[i].Offduty - pLichTrinh.ListT1[i].EarlyGrace)
						tempsom = pLichTrinh.ListT1[i].Offduty - kq.TimeStr.TimeOfDay;
					pLichTrinh.ListT1[i].som = tempsom;
					pLichTrinh.ListT1[i].tre = _0gio;
					if (kq.ThuocCa == null || kq.ThuocCa.Count == 0) kq.ThuocCa = new List<cShift>() { pLichTrinh.ListT1[i] };
					else kq.ThuocCa.Add(pLichTrinh.ListT1[i]);
					tempsom = temptre = _0gio;
				}
			}
			return kq;
		}
		public void XetGioHopLe_v10() {
			if (DSVaoRaHopLe == null) DSVaoRaHopLe = new List<cChkInOut>();//[TBD] xem lại khởi tạo ở chỗ nào cho hợp lý
			if (DSGVaoBatThuong == null) DSGVaoBatThuong = new List<cChk>();
			if (DSGRaBatThuong == null) DSGRaBatThuong = new List<cChk>();
			if (DSGVaoKhac_ChoXL1_v72 == null) DSGVaoKhac_ChoXL1_v72 = new List<cChk>();
			if (DSGRaKhac_ChoXL1_v72 == null) DSGRaKhac_ChoXL1_v72 = new List<cChk>();
			int i = 0, tempCountRa = 0;
			//            Debug.WriteLine("\nnhan vien: "+ this.UserEnrollNumber + " tên "+ this.UserFullName);
			while (DSG_ChuaXL_v10 != null && DSG_ChuaXL_v10.Count != 0) {
				if (DSG_ChuaXL_v10[0].MachineNo % 2 == 0) { // đầu ds là Raa thì add bất thường
					DSGRaBatThuong.Add(DSG_ChuaXL_v10[0]);
					DSG_ChuaXL_v10.RemoveAt(0);
					continue;
				}
				else {// đầu ds chắc chắn là vào, xét xem đó có phải phần tử cuối ds không? đi đến 
					if (DSG_ChuaXL_v10.Count >= 2) { // chưa phải cuối ds. xét xem cặp với nó có phải giờ ra ko
						if (DSG_ChuaXL_v10[1].MachineNo % 2 == 1) { // vào - vào => add cái vào đầu tiên vào bất thường và loại bỏ cái đầu tiên đó và tiếp túc vòng lặp
							if (DSG_ChuaXL_v10.Count >= 3 && DSG_ChuaXL_v10[2].MachineNo % 2 == 0 && DSG_ChuaXL_v10[2].TimeStr - DSG_ChuaXL_v10[1].TimeStr < new TimeSpan(0, 10, 0)) { // vào  -> (vào ra)
								//                                Debug.WriteLine("IN   IN-OUT---- In 0: "+ DSGio_v10[0].TimeStr + " In 1(BT): " + DSGio_v10[1].TimeStr + " ra 2: "+ DSGio_v10[2].TimeStr);
								DSGVaoBatThuong.Add(DSG_ChuaXL_v10[1]);
								DSG_ChuaXL_v10.RemoveAt(1);
								continue;
							}
							else { // vào vào
								#region debug

								/*
                                                                if (DSGio_v10.Count >= 3)
                                                                Debug.WriteLine("IN   IN---- IN 0(BT): " + DSGio_v10[0].TimeStr + " Bonus: IN 1: " + DSGio_v10[1].TimeStr + " DSGio_v10[2]: " + DSGio_v10[2].TimeStr + " máy: " + DSGio_v10[2].MachineNo);
                                                                else if (DSGio_v10.Count >= 2)
                                                                    Debug.WriteLine("IN   IN---- IN 0(BT): " + DSGio_v10[0].TimeStr + " Bonus: IN 1: " + DSGio_v10[1].TimeStr + " hết ds ko có 2");
                                #1#

								#endregion
								DSGVaoBatThuong.Add(DSG_ChuaXL_v10[0]);
								DSG_ChuaXL_v10.RemoveAt(0);
								continue;
							}
						}
						else { // cặp với giờ vào là giờ ra. xét xem có nằm trong 24 tiếng ko, nếu trong 24 tiếng thì mới xét ca được
							if (DSG_ChuaXL_v10[1].TimeStr - DSG_ChuaXL_v10[0].TimeStr > new TimeSpan(23, 0, 0)) {
								DSGVaoBatThuong.Add(DSG_ChuaXL_v10[0]);
								DSG_ChuaXL_v10.RemoveAt(0);
								continue;
							}
							else {
								#region so khớp ca
								tempCountRa = DSG_ChuaXL_v10[1].ThuocCa.Count;
								for (i = 0; i < tempCountRa; i++) {
									// nếu so khớp được ca của giờ vào với ca của giờ ra
									cShift ca = DSG_ChuaXL_v10[0].ThuocCa.Find(item => item.ShiftID == DSG_ChuaXL_v10[1].ThuocCa[i].ShiftID);
									if (ca != null) {
										DSG_ChuaXL_v10[0].ThuocCa = new List<cShift>() { ca };
										DSG_ChuaXL_v10[1].ThuocCa = new List<cShift>() { ca };

										#region xét vào sớm ra trễ và tính công

										// xét giờ đó vào trễ ra sớm  
										TimeSpan temp = DSG_ChuaXL_v10[0].ThuocCa[0].Onduty + DSG_ChuaXL_v10[0].ThuocCa[0].LateGrace;
										TimeSpan tempVaoTre = new TimeSpan();
										if (DSG_ChuaXL_v10[0].TimeStr.TimeOfDay > temp)
											DSG_ChuaXL_v10[0].VaoTre = tempVaoTre = DSG_ChuaXL_v10[0].TimeStr.TimeOfDay - DSG_ChuaXL_v10[0].ThuocCa[0].Onduty;

										TimeSpan temp2 = DSG_ChuaXL_v10[1].ThuocCa[0].Offduty - DSG_ChuaXL_v10[1].ThuocCa[0].EarlyGrace;
										TimeSpan tempRaSom = new TimeSpan();
										if (DSG_ChuaXL_v10[1].TimeStr.TimeOfDay < temp2)
											DSG_ChuaXL_v10[1].RaaSom = tempRaSom = DSG_ChuaXL_v10[1].ThuocCa[0].Offduty - DSG_ChuaXL_v10[1].TimeStr.TimeOfDay;

										// tính giờ làm thêm cho nhân viên
										TimeSpan temp3 = DSG_ChuaXL_v10[1].ThuocCa[0].Offduty + DSG_ChuaXL_v10[1].ThuocCa[0].AfterOT;
										if (DSG_ChuaXL_v10[1].TimeStr.TimeOfDay > temp3)
											DSG_ChuaXL_v10[1].GioLamThem = DSG_ChuaXL_v10[1].TimeStr.TimeOfDay - DSG_ChuaXL_v10[1].ThuocCa[0].Offduty;

										//TimeSpan temp1 = (ca.Offduty + new TimeSpan(ca.DayCount, 0, 0, 0) - ca.Onduty);
										TimeSpan tempGioLamThuc = ca.WorkingTime - (DSG_ChuaXL_v10[0].VaoTre + DSG_ChuaXL_v10[1].RaaSom);
										// phần tính công để qua đưa vào ngày công tính
										DSG_ChuaXL_v10[1].Cong = (float)(tempGioLamThuc.TotalHours / ca.WorkingTime.TotalHours) * ca.Workingday;

										if (DSG_ChuaXL_v10[1].ThuocCa[0].ShiftID == 29) {
											cChk tempChkOut3 = new cChk() {
												UserEnrollNumber = this.UserEnrollNumber,
												TimeDate = DSG_ChuaXL_v10[0].TimeDate, TimeStr = DSG_ChuaXL_v10[0].TimeDate + DSG_ChuaXL_v10[0].ThuocCa[0].Onduty + _8gio, Gio = DateTime.MinValue,
												OriginType = "O", NewType = null, Source = "PC", MachineNo = 2, WorkCode = -1,
												ListGioLQ_v3 = new List<cChk>(), ThuocCa = new List<cShift>() { LichTrinhLV.ListT1.Find(item => item.ShiftID == 8) },
												VaoTre = _0gio, RaaSom = _0gio,
												GioLamThem = _0gio, Cong = 0f, PhuCap = 0f,
												KieuChamTay = false, Type = 0
											};
											cChk tempChkIn1 = new cChk() {
												UserEnrollNumber = this.UserEnrollNumber,
												TimeDate = DSG_ChuaXL_v10[0].TimeDate.AddDays(1.0d), TimeStr = DSG_ChuaXL_v10[0].TimeDate + DSG_ChuaXL_v10[0].ThuocCa[0].Onduty + new TimeSpan(8, 0, 1), Gio = DateTime.MinValue,
												OriginType = "I", NewType = null, Source = "PC", MachineNo = 1, WorkCode = -1,
												ListGioLQ_v3 = new List<cChk>(), ThuocCa = new List<cShift>() { LichTrinhLV.ListT1.Find(item => item.ShiftID == 6) },
												VaoTre = _0gio, RaaSom = _0gio,
												GioLamThem = _0gio, Cong = 0f, PhuCap = 0f,
												KieuChamTay = false, Type = 0
											};
											cChkInOut temp1 = new cChkInOut() {
												Loai = 1, Vao = DSG_ChuaXL_v10[0], Raa = tempChkOut3, ThuocCa = ca,
												VaoTre = tempVaoTre, RaaSom = _0gio
											};
											cChkInOut temp4 = new cChkInOut() {
												Loai = 1, Vao = tempChkIn1, Raa = DSG_ChuaXL_v10[1], ThuocCa = ca,
												VaoTre = _0gio, RaaSom = tempRaSom
											};
											DSVaoRaHopLe.Add(temp1);
											DSVaoRaHopLe.Add(temp4);

											DSVaoHopLe.Add(DSG_ChuaXL_v10[0]);
											DSRaHopLe.Add(tempChkOut3);
											DSVaoHopLe.Add(tempChkIn1);
											DSRaHopLe.Add(DSG_ChuaXL_v10[1]);
										}
										else {
											cChkInOut temp1 = new cChkInOut() {
												Loai = 1, Vao = DSG_ChuaXL_v10[0], Raa = DSG_ChuaXL_v10[1], ThuocCa = ca,
												VaoTre = tempVaoTre, RaaSom = tempRaSom
											};
											DSVaoRaHopLe.Add(temp1);

											DSVaoHopLe.Add(DSG_ChuaXL_v10[0]);
											DSRaHopLe.Add(DSG_ChuaXL_v10[1]);
										}

										DSG_ChuaXL_v10.RemoveRange(0, 2);
										#endregion
										break; // break xét ca
									}
								}
								#endregion

								if (i < tempCountRa) { continue; } // tìm được 2 ca khớp nhau. tiếp tục vòng lặp
								DSGVaoKhac_ChoXL1_v72.Add(DSG_ChuaXL_v10[0]);// không tìm được 2 ca khớp nhau. đưa vào danh sách vào khác ra khác
								DSGRaKhac_ChoXL1_v72.Add(DSG_ChuaXL_v10[1]);
								DSG_ChuaXL_v10.RemoveRange(0, 2);
							}
						}
					}// cuối danh sách
					else {
						DSGVaoBatThuong.Add(DSG_ChuaXL_v10[0]);
						DSG_ChuaXL_v10.RemoveAt(0);
					}
				}
			}
			GC.Collect();
			/*
						Debug.WriteLine("nhan vien: " + UserEnrollNumber + " ten : " + UserFullName);
						Debug.WriteLine("HOP LE: ");
						for (i = 0; i < DSVaoHopLe.Count; i++)
							Debug.WriteLine("Ngay: " + DSVaoHopLe[i].TimeDate.ToString("d/M") + " " + DSVaoHopLe[i].TimeStr.TimeOfDay + " -> " + DSRaHopLe[i].TimeStr.TimeOfDay);

						Debug.WriteLine("KHAC: ");
						for (i = 0; i < DSVaoKhac_v72.Count; i++)
			
			 * 
			 * 
			 * 
			 * 
			 * 
			 * Debug.WriteLine("Ngay: " + DSVaoKhac_v72[i].TimeDate.ToString("d/M") + " " + DSVaoKhac_v72[i].TimeStr.TimeOfDay + " -> " + DSRaKhac_v72[i].TimeStr.TimeOfDay);

						Debug.WriteLine("VAO BAT THUONG: ");
						for (i = 0; i < DSVaoBatThuong.Count; i++)
							Debug.WriteLine("Ngay: " + DSVaoBatThuong[i].TimeDate.ToString("d/M") + " " + DSVaoBatThuong[i].TimeStr.TimeOfDay);

						Debug.WriteLine("RA BAT THUONG: ");
						if (DSRaBatThuong != null)
						for (i = 0; i < DSRaBatThuong.Count; i++)
							Debug.WriteLine("Ngay: " + DSRaBatThuong[i].TimeDate.ToString("d/M") + " " + DSRaBatThuong[i].TimeStr.TimeOfDay);
			#1#
		}

		public void XetGioHopLe_v11() {
			if (DSVaoRaHopLe == null) DSVaoRaHopLe = new List<cChkInOut>();//[TBD] xem lại khởi tạo ở chỗ nào cho hợp lý
			if (DSGVaoBatThuong == null) DSGVaoBatThuong = new List<cChk>();
			if (DSGRaBatThuong == null) DSGRaBatThuong = new List<cChk>();
			if (DSGVaoKhac_ChoXL1_v72 == null) DSGVaoKhac_ChoXL1_v72 = new List<cChk>();
			if (DSGRaKhac_ChoXL1_v72 == null) DSGRaKhac_ChoXL1_v72 = new List<cChk>();
			int i = 0, tempCountRa = 0;
			//            Debug.WriteLine("\nnhan vien: "+ this.UserEnrollNumber + " tên "+ this.UserFullName);
			while (DSG_ChuaXL_v10 != null && DSG_ChuaXL_v10.Count != 0) {
				if (DSG_ChuaXL_v10[0].MachineNo % 2 == 0) { // đầu ds là Raa thì add bất thường
					DSGRaBatThuong.Add(DSG_ChuaXL_v10[0]);
					DSG_ChuaXL_v10.RemoveAt(0);
					continue;
				}
				else {// đầu ds chắc chắn là vào, xét xem đó có phải phần tử cuối ds không? đi đến 
					if (DSG_ChuaXL_v10.Count >= 2) { // chưa phải cuối ds. xét xem cặp với nó có phải giờ ra ko
						if (DSG_ChuaXL_v10[1].MachineNo % 2 == 1) { // vào - vào => add cái vào đầu tiên vào bất thường và loại bỏ cái đầu tiên đó và tiếp tục vòng lặp
							if (DSG_ChuaXL_v10.Count >= 3 && DSG_ChuaXL_v10[2].MachineNo % 2 == 0 && DSG_ChuaXL_v10[2].TimeStr - DSG_ChuaXL_v10[1].TimeStr < _10phut) { // vào  -> (vào ra)
								//                                Debug.WriteLine("IN   IN-OUT---- In 0: "+ DSGio_v10[0].TimeStr + " In 1(BT): " + DSGio_v10[1].TimeStr + " ra 2: "+ DSGio_v10[2].TimeStr);
								DSGVaoBatThuong.Add(DSG_ChuaXL_v10[1]);
								DSG_ChuaXL_v10.RemoveAt(1);
								continue;
							}
							else { // vào vào
								#region debug

								/*
                                                                if (DSGio_v10.Count >= 3)
                                                                Debug.WriteLine("IN   IN---- IN 0(BT): " + DSGio_v10[0].TimeStr + " Bonus: IN 1: " + DSGio_v10[1].TimeStr + " DSGio_v10[2]: " + DSGio_v10[2].TimeStr + " máy: " + DSGio_v10[2].MachineNo);
                                                                else if (DSGio_v10.Count >= 2)
                                                                    Debug.WriteLine("IN   IN---- IN 0(BT): " + DSGio_v10[0].TimeStr + " Bonus: IN 1: " + DSGio_v10[1].TimeStr + " hết ds ko có 2");
                                #1#

								#endregion
								DSGVaoBatThuong.Add(DSG_ChuaXL_v10[0]);
								DSG_ChuaXL_v10.RemoveAt(0);
								continue;
							}
						}
						else { // cặp với giờ vào là giờ ra. xét xem có nằm trong 24 tiếng ko, nếu trong 24 tiếng thì mới xét ca được
							if (DSG_ChuaXL_v10[1].TimeStr - DSG_ChuaXL_v10[0].TimeStr > _21h45) {
								DSGVaoBatThuong.Add(DSG_ChuaXL_v10[0]);
								DSG_ChuaXL_v10.RemoveAt(0);
								continue;
							}
							else {
								#region so khớp ca
								tempCountRa = DSG_ChuaXL_v10[1].ThuocCa.Count;
								for (i = 0; i < tempCountRa; i++) {
									// nếu so khớp được ca của giờ vào với ca của giờ ra
									cChk tempVao = DSG_ChuaXL_v10[0]; cChk tempRa = DSG_ChuaXL_v10[1];
									cShift ca = tempVao.ThuocCa.Find(item => item.ShiftID == tempRa.ThuocCa[i].ShiftID);
									if (ca != null) {
										#region xét vào sớm ra trễ và tính công

										// xét giờ đó vào trễ ra sớm  
										TimeSpan tempVaoTre = TinhVaoTre(tempVao, ca);
										TimeSpan tempRaSom = TinhRaSom(tempRa, ca);

										// tính giờ làm thêm cho nhân viên
										TimeSpan tempLamThem = TinhLamThemSauRa(tempRa, ca);

										float tempHeSoPC = 0f;
										TimeSpan tempTGTinhPC = _0gio;
										TimeSpan tempSoGioLamTinhCong = ca.WorkingTime - tempVaoTre - tempRaSom;

										// ca qua đêm ( ca2&3, ca 3, 3A, 3&1)
										if (tempVao.TimeStr.TimeOfDay > tempRa.TimeStr.TimeOfDay) {
											if (ca.Workingday == 0.5f || ca.Workingday == 1f) {
												tempHeSoPC = 0.3f;
												tempTGTinhPC = tempSoGioLamTinhCong;
											}
											else if (ca.Workingday == 2f) {
												if (ca.Onduty > new TimeSpan(21, 0, 0)) {
													// ca 3&1
													tempHeSoPC = 0.3f;
													tempTGTinhPC = _8gio - tempVaoTre;
													tempSoGioLamTinhCong = tempTGTinhPC;
													cShift tempCa3 = tempVao.ThuocCa.Find(shifts => shifts.Onduty == _21h45 && shifts.Offduty == _05h45);
													cChkInOut tempVaoRaCa3 = new cChkInOut() {
														Loai = 1, Vao = tempVao, Raa = tempRa, ThuocCa = tempCa3, ThuocNgayCong = tempVao.TimeDate,
														VaoTre = tempVaoTre, RaaSom = _0gio,
														TGLamTinhCong = tempSoGioLamTinhCong,
														TongGioThuc = tempTGTinhPC,// (= 8 giờ - vào trễ)
														Cong = (float)((tempSoGioLamTinhCong.TotalMinutes * ca.Workingday) / ca.WorkingTime.TotalMinutes),
														HeSoPC = tempHeSoPC, TGTinhPC = tempTGTinhPC,
														PhuCap = (float)((tempTGTinhPC.TotalHours / 8.0f) * tempHeSoPC)
													};
													cShift tempCa1 = tempVao.ThuocCa.Find(shifts => shifts.Onduty == _05h45 && shifts.Offduty == _13h45);
													//tempVaoRaCa3.Raa = new cChk(){Gio = tempVao.TimeDate, TimeStr = new DateTime(ca.on)};
													tempSoGioLamTinhCong = _8gio - tempRaSom;
													cChkInOut tempVaoRaCa1 = new cChkInOut() {
														Loai = 1, Vao = tempVao, Raa = tempRa, ThuocCa = tempCa1, ThuocNgayCong = tempRa.TimeDate,
														VaoTre = _0gio, RaaSom = tempRaSom,
														TGLamTinhCong = tempSoGioLamTinhCong,
														TongGioThuc = tempSoGioLamTinhCong + tempLamThem,
														Cong = (float)((tempSoGioLamTinhCong.TotalMinutes * ca.Workingday) / ca.WorkingTime.TotalMinutes),
														HeSoPC = 0f, TGTinhPC = _0gio,
														PhuCap = 0f
													};
													DSVaoRaHopLe.Add(tempVaoRaCa3);
													DSVaoRaHopLe.Add(tempVaoRaCa1);
													DSG_ChuaXL_v10.RemoveRange(0, 2);
													break;
												}
												else {
													// ca 2&3
													tempHeSoPC = 0.6f;
													tempTGTinhPC = _8gio - tempRaSom;
												}
											}
										}
										// 2 ca bình thường ko qua đêm,
										else {
											// ca 1&2
											if (ca.Workingday == 2f) {
												tempHeSoPC = 0.5f;
												tempTGTinhPC = _8gio - tempRaSom;
											}
										}
										// 1 ca và nửa ca bình thường 
										cChkInOut tempVaoRa = new cChkInOut() {
											Loai = 1, Vao = tempVao, Raa = tempRa, ThuocCa = ca, ThuocNgayCong = tempVao.TimeDate,
											VaoTre = tempVaoTre, RaaSom = tempRaSom,
											TGLamTinhCong = tempSoGioLamTinhCong,
											TongGioThuc = tempSoGioLamTinhCong + tempLamThem,
											Cong = (float)((tempSoGioLamTinhCong.TotalMinutes * ca.Workingday) / ca.WorkingTime.TotalMinutes),
											HeSoPC = tempHeSoPC, TGTinhPC = tempTGTinhPC,
											PhuCap = (float)((tempTGTinhPC.TotalHours / 8.0f) * tempHeSoPC)
										};

										DSVaoRaHopLe.Add(tempVaoRa);

										DSG_ChuaXL_v10.RemoveRange(0, 2);
										#endregion
										break; // break xét ca
									}
								}
								#endregion

								if (i < tempCountRa) { continue; } // tìm được 2 ca khớp nhau. tiếp tục vòng lặp
								DSGVaoKhac_ChoXL1_v72.Add(DSG_ChuaXL_v10[0]);// không tìm được 2 ca khớp nhau. đưa vào danh sách vào khác ra khác
								DSGRaKhac_ChoXL1_v72.Add(DSG_ChuaXL_v10[1]);
								DSG_ChuaXL_v10.RemoveRange(0, 2);
							}
						}
					}// cuối danh sách
					else {
						DSGVaoBatThuong.Add(DSG_ChuaXL_v10[0]);
						DSG_ChuaXL_v10.RemoveAt(0);
					}
				}
			}
			//GC.Collect();
			#region debug
			/*
						Debug.WriteLine("nhan vien: " + UserEnrollNumber + " ten : " + UserFullName);
						Debug.WriteLine("HOP LE: ");
						for (i = 0; i < DSVaoHopLe.Count; i++)
							Debug.WriteLine("Ngay: " + DSVaoHopLe[i].TimeDate.ToString("d/M") + " " + DSVaoHopLe[i].TimeStr.TimeOfDay + " -> " + DSRaHopLe[i].TimeStr.TimeOfDay);

						Debug.WriteLine("KHAC: ");
						for (i = 0; i < DSVaoKhac_v72.Count; i++)
			
			 * 
			 * 
			 * 
			 * 
			 * 
			 * Debug.WriteLine("Ngay: " + DSVaoKhac_v72[i].TimeDate.ToString("d/M") + " " + DSVaoKhac_v72[i].TimeStr.TimeOfDay + " -> " + DSRaKhac_v72[i].TimeStr.TimeOfDay);

						Debug.WriteLine("VAO BAT THUONG: ");
						for (i = 0; i < DSVaoBatThuong.Count; i++)
							Debug.WriteLine("Ngay: " + DSVaoBatThuong[i].TimeDate.ToString("d/M") + " " + DSVaoBatThuong[i].TimeStr.TimeOfDay);

						Debug.WriteLine("RA BAT THUONG: ");
						if (DSRaBatThuong != null)
						for (i = 0; i < DSRaBatThuong.Count; i++)
							Debug.WriteLine("Ngay: " + DSRaBatThuong[i].TimeDate.ToString("d/M") + " " + DSRaBatThuong[i].TimeStr.TimeOfDay);
			#1#
			#endregion
		}


		private TimeSpan TinhLamThemSauRa(cChk tempRa, cShift ca) {
			TimeSpan tempBDLamThem = ca.Offduty + ca.AfterOT;
			TimeSpan tempLamThem = _0gio;
			if (tempRa.TimeStr.TimeOfDay > tempBDLamThem)
				tempLamThem = tempRa.TimeStr.TimeOfDay - ca.Offduty;
			return tempLamThem;
		}

		private TimeSpan TinhRaSom(cChk tempRa, cShift ca) {
			TimeSpan chopheprasom = ca.Offduty - ca.EarlyGrace;
			TimeSpan tempRaSom = _0gio;
			if (tempRa.TimeStr.TimeOfDay < chopheprasom)
				tempRaSom = ca.Offduty - tempRa.TimeStr.TimeOfDay;
			return tempRaSom;
		}

		private TimeSpan TinhVaoTre(cChk tempVao, cShift ca) {
			TimeSpan chophepvaotre = ca.Onduty + ca.LateGrace;
			TimeSpan tempVaoTre = _0gio;
			if (tempVao.TimeStr.TimeOfDay > chophepvaotre)
				tempVaoTre = tempVao.TimeStr.TimeOfDay - ca.Onduty;
			return tempVaoTre;
		}

		public void XetGioCaDai_v72() {
			if ((DSGVaoKhac_ChoXL1_v72 == null || DSGVaoKhac_ChoXL1_v72.Count == 0)
				|| (DSGRaKhac_ChoXL1_v72 == null || DSGRaKhac_ChoXL1_v72.Count == 0)) return;

			while (DSGRaKhac_ChoXL1_v72.Count != 0) {
				if (DSGVaoKhac_ChoXL1_v72 == null || DSGVaoKhac_ChoXL1_v72.Count == 0) break;
				while (DSGVaoKhac_ChoXL1_v72.Count != 0) {
					if (DSGRaKhac_ChoXL1_v72 == null || DSGRaKhac_ChoXL1_v72.Count == 0) break;
					if ((DSGRaKhac_ChoXL1_v72[0].TimeStr - DSGVaoKhac_ChoXL1_v72[0].TimeStr > new TimeSpan(1, 0, 0, 0))
						/*|| DSVao[0].ThuocCa == null || DSVao[0].ThuocCa.Count == 0#1#) {
						DSGVaoBatThuong.Add(DSGVaoKhac_ChoXL1_v72[0]);
						DSGVaoKhac_ChoXL1_v72.RemoveAt(0);
						continue;
					}

					if ((DSGVaoKhac_ChoXL1_v72[0].TimeStr > DSGRaKhac_ChoXL1_v72[0].TimeStr)
						/*|| DSRa[0].ThuocCa == null || DSRa[0].ThuocCa.Count == 0#1#) {
						DSGRaBatThuong.Add(DSGRaKhac_ChoXL1_v72[0]);
						DSGRaKhac_ChoXL1_v72.RemoveAt(0);
						continue;
					}

					TimeSpan tempTGLam = DSGRaKhac_ChoXL1_v72[0].TimeStr - DSGVaoKhac_ChoXL1_v72[0].TimeStr;

					if (tempTGLam < _30phut) {
						DSGVaoBatThuong.Add(DSGVaoKhac_ChoXL1_v72[0]);
						DSGRaBatThuong.Add(DSGRaKhac_ChoXL1_v72[0]);
						continue;
					}

					cChk tempVao = DSGVaoKhac_ChoXL1_v72[0];
					cChk tempRa = DSGRaKhac_ChoXL1_v72[0];
					cChkInOut tempVaoRa = new cChkInOut() { Vao = tempVao, Raa = tempRa };

					DSVaoRaKhacChuaXL.Add(tempVaoRa);

					if (tempTGLam > new TimeSpan(3, 45, 0) && tempTGLam < new TimeSpan(7, 45, 0)) DSGRaKhac_ChoXL1_v72[0].Cong = 0.5f;
					else if (tempTGLam > new TimeSpan(7, 45, 1) && tempTGLam < new TimeSpan(11, 45, 0)) DSGRaKhac_ChoXL1_v72[0].Cong = 1f;
					else if (tempTGLam > new TimeSpan(11, 45, 1) && tempTGLam < new TimeSpan(15, 45, 0)) DSGRaKhac_ChoXL1_v72[0].Cong = 1.5f;
					else if (tempTGLam > new TimeSpan(15, 45, 1) && tempTGLam < new TimeSpan(18, 0, 0)) DSGRaKhac_ChoXL1_v72[0].Cong = 2f;

					DSVaoKhacChuaXL2_v72.Add(DSGVaoKhac_ChoXL1_v72[0]);
					DSRaKhacChuaXL2_v72.Add(DSGRaKhac_ChoXL1_v72[0]);

					//DSVaoRaKhacChuaXL

					DSGVaoKhac_ChoXL1_v72.RemoveAt(0);
					DSGRaKhac_ChoXL1_v72.RemoveAt(0);
				}
			}
		}

		public void XetGioCaDai_v11() {
			if (DSVaoRaKhacChuaXL == null) DSVaoRaKhacChuaXL = new List<cChkInOut>(); //[TBD] tìm chỗ đặt khởi tạo cho hợp lý
			if ((DSGVaoKhac_ChoXL1_v72 == null || DSGVaoKhac_ChoXL1_v72.Count == 0)
				|| (DSGRaKhac_ChoXL1_v72 == null || DSGRaKhac_ChoXL1_v72.Count == 0)) return;

			while (DSGRaKhac_ChoXL1_v72.Count != 0) {
				if (DSGVaoKhac_ChoXL1_v72 == null || DSGVaoKhac_ChoXL1_v72.Count == 0) break;
				while (DSGVaoKhac_ChoXL1_v72.Count != 0) {
					if (DSGRaKhac_ChoXL1_v72 == null || DSGRaKhac_ChoXL1_v72.Count == 0) break;

					cChk tempVao = DSGVaoKhac_ChoXL1_v72[0];
					cChk tempRa = DSGRaKhac_ChoXL1_v72[0];
					TimeSpan tempGioLamThuc = tempRa.TimeStr - tempVao.TimeStr;

					if (tempGioLamThuc > new TimeSpan(1, 0, 0, 0)) {
						DSGVaoBatThuong.Add(tempVao);
						DSGVaoKhac_ChoXL1_v72.RemoveAt(0);
						continue;
					}

					if (tempVao.TimeStr > tempRa.TimeStr) {
						DSGRaBatThuong.Add(tempRa);
						DSGRaKhac_ChoXL1_v72.RemoveAt(0);
						continue;
					}

					if (tempGioLamThuc < _30phut) {
						DSGVaoBatThuong.Add(tempVao);
						DSGRaBatThuong.Add(tempRa);
						DSGVaoKhac_ChoXL1_v72.RemoveAt(0);
						DSGRaKhac_ChoXL1_v72.RemoveAt(0);
						continue;
					}

					//double a = Math.Floor(temp.TotalMinutes/30f);
					//DSRaKhacChuaXL1_v72[0].Cong = (float)(a/16d);
					//if (temp > new TimeSpan(3, 45, 0) && temp < new TimeSpan(7, 45, 0)) DSRaKhacChuaXL1_v72[0].Cong = (float)(a/16d);
					//else if (temp > new TimeSpan(7, 45, 1) && temp < new TimeSpan(11, 45, 0)) DSRaKhacChuaXL1_v72[0].Cong = 1f;
					//else if (temp > new TimeSpan(11, 45, 1) && temp < new TimeSpan(15, 45, 0)) DSRaKhacChuaXL1_v72[0].Cong = 1.5f;
					//else if (temp > new TimeSpan(15, 45, 1) && temp < new TimeSpan(18, 0, 0)) DSRaKhacChuaXL1_v72[0].Cong = 2f;
					//DSVaoKhacChuaXL2_v72.Add(DSVaoKhacChuaXL1_v72[0]);
					//DSRaKhacChuaXL2_v72.Add(DSRaKhacChuaXL1_v72[0]);


					float tempHeSoPC = 0f;
					TimeSpan tempTGTinhPC = _0gio;
					TimeSpan tempGioLamTinhCong = _0gio;

					if (tempGioLamThuc < new TimeSpan(7, 45, 1)) {
						tempGioLamTinhCong = tempGioLamThuc;
					}
					if (tempGioLamThuc > new TimeSpan(7, 45, 1)) {
						tempGioLamTinhCong = _8gio;
					}

					if (tempVao.TimeStr.TimeOfDay > tempRa.TimeStr.TimeOfDay) {
						tempHeSoPC = 0.3f;
						DateTime ktTinhPC = tempRa.TimeStr;
						DateTime bdTinhPC = tempVao.TimeStr;
						if (ktTinhPC.TimeOfDay > _05h45) ktTinhPC = ktTinhPC.Subtract(ktTinhPC.TimeOfDay - _05h45);
						if (bdTinhPC.TimeOfDay < _21h45) bdTinhPC = bdTinhPC.Add(_21h45 - bdTinhPC.TimeOfDay);
						tempTGTinhPC = ktTinhPC - bdTinhPC;
					}

					cChkInOut tempVaoRa = new cChkInOut() {
						Loai = 0, Vao = tempVao, Raa = tempRa, ThuocCa = null, ThuocNgayCong = tempVao.TimeDate,
						VaoTre = _0gio, RaaSom = _0gio,
						TGLamTinhCong = tempGioLamTinhCong,
						TongGioThuc = tempGioLamThuc,
						Cong = (float)(tempGioLamTinhCong.TotalHours / 8.0f),
						HeSoPC = tempHeSoPC, TGTinhPC = tempTGTinhPC,
						PhuCap = (float)((tempTGTinhPC.TotalHours / 8.0f) * tempHeSoPC)
					};
					//XacDinhCaVaoRa(tempVaoRa);

					DSVaoRaKhacChuaXL.Add(tempVaoRa);

					/*					cChkInOut tempvaora = new cChkInOut() { Vao = tempVao, Raa = tempRa, ThuocNgayCong = tempVao.TimeDate };
										XacDinhCaVaoNULL(tempvaora);
										XacDinhCaRaNULL(tempvaora);
										XacDinhCaVaoRaNULL(tempvaora);
										if (tempVao.ThuocCa.Count != 0 && tempRa.ThuocCa.Count != 0 ) {
											string tempstr = " Vao: " + tempVao.TimeStr.ToString("h:mm") + " Raa: " + tempRa.TimeStr.ToString("h:mm");
											tempstr += " Tong: " + (tempRa.TimeStr - tempVao.TimeStr).TotalHours;
											tempstr += "CaVao: ";

											foreach (cShift clsShiftse in tempVao.ThuocCa) {
												tempstr += " " + clsShiftse.ShiftCode;
											}
											tempstr += " CaRa: ";
											foreach (cShift clsShiftse in tempRa.ThuocCa) {
												tempstr += " " + clsShiftse.ShiftCode;
											}
											Debug.WriteLine(tempstr+"\n-------------------------------------\n");
										}#1#
					//XacDinhCaVaoRa(tempvaora);

					/*					TimeSpan temp1 = TimGioVaoGanNhat(tempVao, tempVao.ThuocCa);
										if (temp1 == new TimeSpan(0, 0, 0)) Debug.WriteLine(UserEnrollNumber + " Vao: " + tempVao.TimeStr.ToString("h:mm") + " KO CA ");
										else {
											Debug.WriteLine(UserEnrollNumber + " Vao: " + tempVao.TimeStr.ToString("h:mm") + " gan voi " + temp1.ToString());
										}
										TimeSpan temp2 = TimGioRaGanNhat(tempRa, tempRa.ThuocCa);
										if (temp2 == new TimeSpan(0, 0, 0)) Debug.WriteLine(UserEnrollNumber + " Raa: " + tempRa.TimeStr.ToString("h:mm") + " KO CA ");
										else {
											Debug.WriteLine(UserEnrollNumber + " Raa: " + tempRa.TimeStr.ToString("h:mm") + " gan voi " + temp2.ToString());
										}
										Debug.WriteLine("--------------------------\n\n ");#1#

					//DSVaoRaKhacChuaXL.Add(tempvaora);

					DSGVaoKhac_ChoXL1_v72.RemoveAt(0);
					DSGRaKhac_ChoXL1_v72.RemoveAt(0);
				}
			}
		}

		void XacDinhCaVaoNULL(cChkInOut Gio) {
			List<cShift> tempCaVao = new List<cShift>(Gio.Vao.ThuocCa);
			List<cShift> tempCaRa = new List<cShift>(Gio.Raa.ThuocCa);

			if ((tempCaVao == null || tempCaVao.Count == 0) && (tempCaRa != null && tempCaRa.Count != 0)) {
				Debug.WriteLine("CA VAO NULL----------------------------------");
				TimeSpan tempTongGio;
				if (Gio.Raa.TimeStr.TimeOfDay < Gio.Vao.TimeStr.TimeOfDay) tempTongGio = Gio.Raa.TimeStr.TimeOfDay + new TimeSpan(1, 0, 0, 0) - Gio.Vao.TimeStr.TimeOfDay;
				else tempTongGio = Gio.Raa.TimeStr.TimeOfDay - Gio.Vao.TimeStr.TimeOfDay;
				string temp = "Xem Gio Raa " + Gio.Vao.UserEnrollNumber + "\t BOVao: " + Gio.Vao.TimeStr.ToString("h:mm tt") + "\t Raa: " + Gio.Raa.TimeStr.ToString("h:mm tt") + " Tong: " + tempTongGio.TotalHours;
				temp += "\tCa: ";
				for (int i = 0; i < tempCaRa.Count; i++) {
					temp += tempCaRa[i].ShiftCode;
				}
				Debug.WriteLine(temp);
				Debug.WriteLine("---------------------------------------------\n");
			}
		}

		void XacDinhCaRaNULL(cChkInOut Gio) {
			List<cShift> tempCaVao = new List<cShift>(Gio.Vao.ThuocCa);
			List<cShift> tempCaRa = new List<cShift>(Gio.Raa.ThuocCa);

			if ((tempCaRa == null || tempCaRa.Count == 0) && (tempCaVao != null && tempCaVao.Count != 0)) {
				Debug.WriteLine("CA Raa NULL----------------------------------");
				TimeSpan tempTongGio;
				if (Gio.Raa.TimeStr.TimeOfDay < Gio.Vao.TimeStr.TimeOfDay) tempTongGio = Gio.Raa.TimeStr.TimeOfDay + new TimeSpan(1, 0, 0, 0) - Gio.Vao.TimeStr.TimeOfDay;
				else tempTongGio = Gio.Raa.TimeStr.TimeOfDay - Gio.Vao.TimeStr.TimeOfDay;
				string temp = "Xem Gio Vao " + Gio.Vao.UserEnrollNumber + "\t Vao: " + Gio.Vao.TimeStr.ToString("h:mm tt") + "\t BORa: " + Gio.Raa.TimeStr.ToString("h:mm tt") + " Tong: " + tempTongGio.TotalHours;
				temp += "\tCa: ";
				for (int i = 0; i < tempCaVao.Count; i++) {
					temp += tempCaVao[i].ShiftCode;
				}
				Debug.WriteLine(temp);
				Debug.WriteLine("---------------------------------------------\n");
			}
		}

		void XacDinhCaVaoRaNULL(cChkInOut Gio) {
			List<cShift> tempCaVao = new List<cShift>(Gio.Vao.ThuocCa);
			List<cShift> tempCaRa = new List<cShift>(Gio.Raa.ThuocCa);

			if ((tempCaVao == null || tempCaVao.Count == 0) && (tempCaRa == null || tempCaRa.Count == 0)) {
				Debug.WriteLine("CA VAO/RA NULL----------------------------------");
				TimeSpan tempTongGio;
				if (Gio.Raa.TimeStr.TimeOfDay < Gio.Vao.TimeStr.TimeOfDay) tempTongGio = Gio.Raa.TimeStr.TimeOfDay + new TimeSpan(1, 0, 0, 0) - Gio.Vao.TimeStr.TimeOfDay;
				else tempTongGio = Gio.Raa.TimeStr.TimeOfDay - Gio.Vao.TimeStr.TimeOfDay;
				string temp = Gio.Vao.UserEnrollNumber + "\t BOVao: " + Gio.Vao.TimeStr.ToString("h:mm tt") + "\t BORa: " + Gio.Raa.TimeStr.ToString("h:mm tt") + " Tong: " + tempTongGio.TotalHours;
				temp += "\tCa: ";

				Debug.WriteLine(temp);
				Debug.WriteLine("---------------------------------------------\n\n\n");
			}
		}

		void XacDinhCaVaoRa(cChkInOut Gio) {
			List<cShift> tempCaVao = new List<cShift>(Gio.Vao.ThuocCa);
			List<cShift> tempCaRa = new List<cShift>(Gio.Raa.ThuocCa);
			List<cChkInOut> dsVaoRaCoThe = new List<cChkInOut>();


			if ((tempCaVao.Count != 0) && (tempCaRa.Count != 0)) {
				Debug.WriteLine("CA VAO/RA Khac NULL----------------------------------");
				TimeSpan tempTongGio = Gio.Raa.TimeStr - Gio.Vao.TimeStr;
				string temp = Gio.Vao.UserEnrollNumber + "\t Vao: " + Gio.Vao.TimeStr.ToString("h:mm tt") + "\t Raa: " + Gio.Raa.TimeStr.ToString("h:mm tt") + "\t Tong: " + tempTongGio.TotalHours.ToString(".#");
				temp += "\nCaVao: \n";

				for (int i = 0; i < tempCaVao.Count; i++) {
					temp += tempCaVao[i].ShiftCode + "   ";
					TimeSpan tempRSLT = KiemTraRaSomLamThem(Gio.Raa, Gio.Vao.TimeDate, tempCaVao[i]);
					TimeSpan tempvaotre = TinhVaoTre(Gio.Vao, tempCaVao[i]);
					TimeSpan tempTongGioLam = TinhTongGioLam(tempvaotre, tempRSLT, tempCaVao[i]);
					temp += "\t TongGio:" + tempTongGioLam.TotalHours.ToString(".#") + "\n";
					Debug.WriteLineIf(tempRSLT < _0gio && tempRSLT > _4gio.Negate(), tempCaVao[i].ShiftCode + "\t vao tre: " + tempvaotre.ToString() + " Raa som: " + tempRSLT.ToString());
					Debug.WriteLineIf(tempRSLT > _0gio && tempRSLT < _4gio, tempCaVao[i].ShiftCode + "\t vao tre: " + tempvaotre.ToString() + "\t lam them sau: " + tempRSLT.ToString());
					/*
										if (Gio.Raa.TimeStr.TimeOfDay < tempCaVao[i].Onduty)
											tempCaVao[i].TGLam = Gio.Raa.TimeStr.TimeOfDay.Add(new TimeSpan(1, 0, 0, 0)) - tempCaVao[i].Onduty - tempCaVao[i].tre;
										tempCaVao[i].TGLam = Gio.Raa.TimeStr.TimeOfDay - tempCaVao[i].Onduty - tempCaVao[i].tre;
					#1#

				}
				temp += "\nCaRa: \n";
				for (int i = 0; i < tempCaRa.Count; i++) {
					temp += tempCaRa[i].ShiftCode + "   ";
					TimeSpan tempVTLT = KiemTraVaoTreLamThem(Gio.Vao, Gio.Raa.TimeDate, tempCaRa[i]);
					TimeSpan temprasom = TinhRaSom(Gio.Raa, tempCaRa[i]);
					TimeSpan tempTongGioLam = TinhTongGioLam(temprasom, tempVTLT, tempCaRa[i]);
					temp += "\t TongGio:" + tempTongGioLam.TotalHours.ToString(".#") + "\n";
					Debug.WriteLineIf(tempVTLT < _0gio && tempVTLT > _4gio.Negate(), tempCaRa[i].ShiftCode + "\t ra som : " + temprasom.ToString() + "\t Vao tre : " + tempVTLT.ToString());
					Debug.WriteLineIf(tempVTLT > _0gio && tempVTLT < _4gio, tempCaRa[i].ShiftCode + "\t ra som : " + temprasom.ToString() + "\t lam them truoc: " + tempVTLT.ToString());
				}
				Debug.WriteLine(temp);
				Debug.WriteLine("---------------------------------------------\n\n\n");
			}
		}

		private TimeSpan TinhTongGioLam(TimeSpan tempvaotre, TimeSpan tempRSLT, cShift cShift) {
			TimeSpan kq = _0gio;
			kq = cShift.WorkingTime - tempvaotre;
			if (tempRSLT < _0gio) kq -= tempRSLT.Negate();
			//if (tempRSLT > _0gio) 
			return kq;
		}

		/// <summary>
		/// Vào ca-Giờ vào: dương if Làm thêm , âm if vào trễ
		/// </summary>
		/// <param name="pGioVao"></param>
		/// <param name="pNgay"></param>
		/// <param name="pCa"></param>
		/// <returns>Vào ca-Giờ vào: Làm thêm nếu dương. vào trễ nếu âm</returns>
		private TimeSpan KiemTraVaoTreLamThem(cChk pGioVao, DateTime pNgay, cShift pCa) {
			TimeSpan kq = _0gio;
			DateTime VaoCa = pNgay - new TimeSpan(pCa.DayCount, 0, 0, 0) + new TimeSpan(pCa.Onduty.Hours, pCa.Onduty.Minutes, pCa.Onduty.Seconds);

			kq = VaoCa - pGioVao.TimeStr;
			return kq;
		}

		/// <summary>
		/// giờ ra - ra ca: TimeSpan dương if làm thêm,  âm nếu ra sớm
		/// </summary>
		/// <param name="pGioRa"></param>
		/// <param name="pNgay"></param>
		/// <param name="pCa"></param>
		/// <returns> TimeSpan âm nếu ra sớm, dương if làm thêm  </returns>
		private TimeSpan KiemTraRaSomLamThem(cChk pGioRa, DateTime pNgay, cShift pCa) {
			TimeSpan kq = _0gio;
			DateTime RaCa = pNgay + new TimeSpan(pCa.DayCount, pCa.Offduty.Hours, pCa.Offduty.Minutes, pCa.Offduty.Seconds);

			kq = pGioRa.TimeStr - RaCa;
			return kq;
		}

		TimeSpan TimGioVaoGanNhat(cChk GioKT, List<cShift> DSCa) {
			TimeSpan kq = _0gio;
			TimeSpan kqduration;
			TimeSpan tempDuration;
			if (DSCa == null || DSCa.Count == 0) return kq;
			kq = DSCa[0].Onduty;
			kqduration = (GioKT.TimeStr.TimeOfDay - DSCa[0].Onduty).Duration();

			for (int i = 1; i < DSCa.Count; i++) {
				tempDuration = (GioKT.TimeStr.TimeOfDay - DSCa[i].Onduty).Duration();
				if (tempDuration < kqduration) {
					kq = DSCa[i].Onduty;
					kqduration = tempDuration;
				}
			}
			return kq;
		}

		TimeSpan TimGioRaGanNhat(cChk GioKT, List<cShift> DSCa) {
			TimeSpan kq = _0gio;
			TimeSpan kqduration;
			TimeSpan tempDuration;
			if (DSCa == null || DSCa.Count == 0) return kq;
			kq = DSCa[0].Offduty;
			kqduration = (GioKT.TimeStr.TimeOfDay - DSCa[0].Offduty).Duration();

			for (int i = 1; i < DSCa.Count; i++) {
				tempDuration = (GioKT.TimeStr.TimeOfDay - DSCa[i].Offduty).Duration();
				if (tempDuration < kqduration) {
					kq = DSCa[i].Offduty;
					kqduration = tempDuration;
				}
			}
			return kq;
		}
		internal void KhoiTaoDSNgayCong(DateTime StartTime, DateTime EndTime) {
			DSNgayCong_v71 = new List<clsNgayCong_v71>();
			for (DateTime s = StartTime.Date; s <= EndTime.Date; s = s.AddDays(1d)) {
				clsNgayCong_v71 ngaycong = new clsNgayCong_v71 {
					NgayCong = s.Date,
					Tren2Ca = false,
					//DSVaoKhac_ChuaXL_v72 = new List<cChk>(), DSRaKhac_ChuaXL_v72 = new List<cChk>(),
					DSVaoKhac_DaXL_v10 = new List<cChk>(), DSRaKhac_DaXL_v10 = new List<cChk>(),
					//DSVaoRaHL_v11 = new List<cChkInOut>(), 
					DSVaoRaTH_v11 = new List<cChkInOut>(),
					TongGioLam = _0gio, TongCongNgay = 0f, TongPhuCap = 0f
				};
				DSNgayCong_v71.Add(ngaycong);
			}
		}
		void TinhCongDSGKhac_v10(cChk pIn, cChk pOut) {
			TimeSpan tongGioVaoRa = pOut.TimeStr - pIn.TimeStr;
			switch (pIn.Type) {
				case 1: // ca dài
					DateTime giovaolam = new DateTime(), gioralam = new DateTime();
					giovaolam = pIn.TimeStr < pIn.Gio ? pIn.Gio : pIn.TimeStr;
					gioralam = pOut.TimeStr > pOut.Gio ? pOut.Gio : pOut.TimeStr;
					tongGioVaoRa = gioralam - giovaolam;

					if (tongGioVaoRa.TotalHours < 11.75f) {
						pOut.Cong = (float)(tongGioVaoRa.TotalHours / 8f);
					}
					else if (tongGioVaoRa.TotalHours >= 11.75f) {
						pOut.Cong = 1.5f;
					}
					//[TBD]
					if (pIn.TimeStr.TimeOfDay > _16gio && tongGioVaoRa.TotalHours >= 8.0f)
						pOut.PhuCap = 0.3f;

					else pOut.PhuCap = 0.3f * (float)(tongGioVaoRa.TotalHours / 8.0f);
					break;
				case 2: // có tăng ca
					if (tongGioVaoRa.TotalHours > 7.75f) {
						XetThuocCa_v3(pIn, LichTrinhLV);
						XetThuocCa_v3(pOut, LichTrinhLV);
						// hiểu ca
						if (pOut.ThuocCa != null && pOut.ThuocCa.Count == 1) {
							TimeSpan vaotre = _0gio, rasom = _0gio, tempGioLamThem = _0gio;
							TinhVaoTreRaSom(pIn, pOut, pOut.ThuocCa[0], ref vaotre, ref rasom);
							if (pOut.TimeStr.TimeOfDay > pOut.ThuocCa[0].Offduty)
								tempGioLamThem = pOut.TimeStr.TimeOfDay - pOut.ThuocCa[0].Offduty;
							pOut.Cong = (float)(pOut.ThuocCa[0].WorkingTime - (vaotre + rasom) + tempGioLamThem).TotalHours / 8.0f;
							switch (pOut.ThuocCa[0].ShiftID) {
								case 8: // ca 3
									pOut.PhuCap = 0.3f * (float)(pOut.ThuocCa[0].WorkingTime - (vaotre + rasom)).TotalHours / 8.0f;
									pOut.PhuCap += 0.6f * (float)(tempGioLamThem.TotalHours / 8.0f);
									break;
								case 6:
								case 7:
								case 3:
								case 14:
								case 18: //6:ca1, 7:ca2, 3:HC, 14:HCx , 18 : ca1Dung
									pOut.PhuCap = 0.5f * (float)(tempGioLamThem.TotalHours / 8.0f);
									break;
							}
						}
						else {
							// ko hiểu ca
							pOut.Cong = (float)(tongGioVaoRa.TotalHours / 8.0f);
							if (pIn.TimeStr.TimeOfDay > new TimeSpan(21, 0, 0)) {
								pOut.PhuCap = 0.6f * (pOut.Cong - 1.0f);
								Debug.WriteLine("ko hieu ca (sau 21h) cua " + UserEnrollNumber + " lúc " + pIn.TimeStr + ". Cong=" + pOut.Cong + ". Phu cap=" + pOut.PhuCap);
							}
							else {
								pOut.PhuCap = 0.5f * (pOut.Cong - 1.0f);
								Debug.WriteLine("ko hieu ca (truoc 21h) cua " + UserEnrollNumber + " lúc " + pIn.TimeStr + ". Cong=" + pOut.Cong + ". Phu cap=" + pOut.PhuCap);
							}
						}
					}
					break;
				case 3: // bỏ giờ việc riêng
					pOut.Cong = (float)(tongGioVaoRa.TotalHours / 8.0f);
					if (pIn.TimeStr.TimeOfDay > new TimeSpan(21, 0, 0)) pOut.PhuCap = 0.3f * pOut.Cong;
					break;
				case 5: // giờ tự do
					pOut.Cong = (float)(tongGioVaoRa.TotalHours / 8.0f);
					if (pIn.TimeStr.TimeOfDay > new TimeSpan(21, 0, 0)) pOut.PhuCap = 0.3f * pOut.Cong;

					break;
			}
		}
		public void DuaVaoNgayCong() {
			int dem = 0;
			while (DSVaoHopLe != null && dem < DSVaoHopLe.Count) {
				clsNgayCong_v71 NgayCong = DSNgayCong_v71.Find(item => item.NgayCong.Date == DSVaoHopLe[dem].TimeDate);
				if (NgayCong.DSRaHL_v71 != null && NgayCong.DSRaHL_v71.Count == 1
					&& DSRaHopLe[dem].ThuocCa[0].Onduty - NgayCong.DSRaHL_v71[0].ThuocCa[0].Offduty < _30phut) { // 2 ca liên tiếp nhau,
					NgayCong.TongCongNgay -= NgayCong.DSRaHL_v71[0].Cong;
					NgayCong.DSRaHL_v71[0].Cong = (float)((NgayCong.DSRaHL_v71[0].ThuocCa[0].WorkingTime - NgayCong.DSVaoHL_v71[0].VaoTre).TotalHours / 8f);
					NgayCong.TongCongNgay += NgayCong.DSRaHL_v71[0].Cong;
					NgayCong.TongSom -= NgayCong.DSRaHL_v71[0].RaaSom;// ca đầu ko tính ra sớm
					NgayCong.TongTre -= DSVaoHopLe[dem].VaoTre;//ca sau ko tính vào trễ
					if (DSRaHopLe[dem].ThuocCa[0].ShiftID == 8 || DSRaHopLe[dem].ThuocCa[0].ShiftID == 30) { // ca sau là ca 3 hoặc ca 3A
						DSRaHopLe[dem].PhuCap = 0.6f * (float)((DSRaHopLe[dem].ThuocCa[0].WorkingTime - DSRaHopLe[dem].RaaSom).TotalHours / 8.0f);
					}
					else {
						DSRaHopLe[dem].PhuCap = 0.5f * (float)((DSRaHopLe[dem].ThuocCa[0].WorkingTime - DSRaHopLe[dem].RaaSom).TotalHours / 8.0f);
					}
				}
				NgayCong.DSVaoHL_v71.Add(DSVaoHopLe[dem]);
				NgayCong.DSRaHL_v71.Add(DSRaHopLe[dem]);
				NgayCong.TongCongNgay += DSRaHopLe[dem].Cong;
				NgayCong.TongPhuCap += DSRaHopLe[dem].PhuCap;
				NgayCong.TongTre += DSVaoHopLe[dem].VaoTre;
				NgayCong.TongSom += DSRaHopLe[dem].RaaSom;
				dem++;
			}

			dem = 0;
			while (DSVaoKhacChuaXL2_v72 != null && dem < DSVaoKhacChuaXL2_v72.Count) {
				clsNgayCong_v71 NgayCong = DSNgayCong_v71.Find(item => item.NgayCong.Date == DSVaoKhacChuaXL2_v72[dem].TimeDate);
				NgayCong.DSVaoKhac_ChuaXL_v72.Add(DSVaoKhacChuaXL2_v72[dem]);
				NgayCong.DSRaKhac_ChuaXL_v72.Add(DSRaKhacChuaXL2_v72[dem]);
				NgayCong.TongCongNgay += DSRaKhacChuaXL2_v72[dem].Cong;

				dem++;
			}

			dem = 0;
			//if (DSG_Khac_DaXL_v10 == null) Debug.WriteLine(this.UserFullName+": DSG_Khac_DaXL_v10 NULL");
			while (DSG_DaXL_v10 != null && dem < DSG_DaXL_v10.Count) {
				Debug.Write("NV: " + UserFullName + " co ca dai tu:  ");
				clsNgayCong_v71 NgayCong = DSNgayCong_v71.Find(item => item.NgayCong.Date == DSG_DaXL_v10[dem].TimeDate);
				TimeSpan temp = DSG_DaXL_v10[dem + 1].TimeStr - DSG_DaXL_v10[dem].TimeStr;
				Debug.Write(" " + DSG_DaXL_v10[dem].TimeStr + " -> " + DSG_DaXL_v10[dem + 1].TimeStr);
				Debug.WriteLine(" Tong Gio: " + temp.TotalHours);
				//if (temp.TotalHours > 8f) DSG_Khac_DaXL_v10[dem + 1].Cong = (float)(temp.TotalHours / 8.0f);
				TinhCongDSGKhac_v10(DSG_DaXL_v10[dem], DSG_DaXL_v10[dem + 1]);
				Debug.WriteLine(" DSG_Khac_DaXL_v10[dem + 1].Cong " + DSG_DaXL_v10[dem + 1].Cong);
				NgayCong.TongCongNgay += DSG_DaXL_v10[dem + 1].Cong;
				NgayCong.TongPhuCap += DSG_DaXL_v10[dem + 1].PhuCap;
				NgayCong.DSVaoKhac_DaXL_v10.Add(DSG_DaXL_v10[dem]);
				NgayCong.DSRaKhac_DaXL_v10.Add(DSG_DaXL_v10[dem + 1]);
				//Debug.WriteLine("nhan vien: " + UserFullName + " có ca dài: từ " + DSG_Khac_DaXL_v10[dem].TimeStr + " -> " + DSG_Khac_DaXL_v10[dem + 1].TimeStr + " Cong: " + DSG_Khac_DaXL_v10[dem + 1].Cong);
				dem = dem + 2;
			}
			DSGVaoBatThuong.AddRange(DSGRaBatThuong);

			DSGVaoBatThuong = DSGVaoBatThuong.OrderBy(@out => @out.TimeStr).ToList();
		}
		public void DuaVaoNgayCong_v11() {
			int dem = 0;
			List<cChkInOut> dstemp = new List<cChkInOut>();
			if (DSVaoRaHopLe != null) dstemp.AddRange(DSVaoRaHopLe);
			if (DSVaoRaKhacChuaXL != null) dstemp.AddRange(DSVaoRaKhacChuaXL);
			dstemp.Sort(new cChkInOutComparer());
			/*			foreach (cChkInOut inOut in dstemp) {
							string tempca = string.Empty;
							if (inOut.ThuocCa == null) tempca = "NULL";
							else tempca = inOut.ThuocCa.ShiftCode;
							Debug.WriteLine("UserEnrollNumber: " + inOut.Vao.UserEnrollNumber + "; DATE: " + inOut.Vao.TimeDate.ToString("d/M") + "; IN: " + inOut.Vao.TimeStr.ToString("h:mm tt") + "; OUT: " + inOut.Raa.TimeStr.ToString("h:mm tt") + "; CA: " + tempca);
						}#1#
			//return;
			//dstemp.AddRange(DSG_Khac_DaXL_v10);
			while (dstemp != null && dem < dstemp.Count) {
				clsNgayCong_v71 NgayCong = DSNgayCong_v71.Find(item => item.NgayCong.Date == dstemp[dem].ThuocNgayCong);
				if (NgayCong.DSVaoRaTH_v11 == null) NgayCong.DSVaoRaTH_v11 = new List<cChkInOut>();
				if (NgayCong.DSVaoRaTH_v11.Count == 0) {
					NgayCong.DSVaoRaTH_v11.Add(dstemp[dem]);

					NgayCong.TongCongNgay = dstemp[dem].Cong;
					NgayCong.TongPhuCap = dstemp[dem].PhuCap;
					NgayCong.TongTre = dstemp[dem].VaoTre;
					NgayCong.TongSom = dstemp[dem].RaaSom;
					NgayCong.TongGioLam = dstemp[dem].TGLamTinhCong;
					NgayCong.TongGioThuc = dstemp[dem].TongGioThuc;
				}
				else {
					/*
										string tempstrCa = string.Empty, tempstrCa1 = string.Empty;
										if (NgayCong.DSVaoRaTH_v11[0].Vao.ThuocCa != null && NgayCong.DSVaoRaTH_v11[0].Vao.ThuocCa.Count != 0) tempstrCa += NgayCong.DSVaoRaTH_v11[0].Vao.ThuocCa[0].ShiftCode;
										Debug.WriteLine(UserEnrollNumber + " CO NGAY CONG: " + NgayCong.NgayCong.ToString("d/M") + " CO 2 CA TRO LEN. vào: " + NgayCong.DSVaoRaTH_v11[0].Vao.TimeStr.ToString("h:mm tt d/M") + " ; ra: " + NgayCong.DSVaoRaTH_v11[0].Raa.TimeStr.ToString("h:mm tt d/M") + ". CA: " + tempstrCa);
										NgayCong.DSVaoRaTH_v11.Add(dstemp[dem]);
										if (NgayCong.DSVaoRaTH_v11[1].Vao.ThuocCa != null && NgayCong.DSVaoRaTH_v11[1].Vao.ThuocCa.Count != 0) tempstrCa1 += NgayCong.DSVaoRaTH_v11[1].Vao.ThuocCa[0].ShiftCode;
										Debug.WriteLine(UserEnrollNumber + " CO NGAY CONG: " + NgayCong.NgayCong.ToString("d/M") + " CO 2 CA TRO LEN. vào: " + NgayCong.DSVaoRaTH_v11[1].Vao.TimeStr.ToString("h:mm tt d/M") + " ; ra: " + NgayCong.DSVaoRaTH_v11[1].Raa.TimeStr.ToString("h:mm tt d/M") + ". CA: " + tempstrCa1);
					#1#
					NgayCong.DSVaoRaTH_v11.Add(dstemp[dem]);
					NgayCong.Tren2Ca = true;
					NgayCong.TongCongNgay += dstemp[dem].Cong;
					NgayCong.TongPhuCap += dstemp[dem].PhuCap;
					NgayCong.TongTre += dstemp[dem].VaoTre;
					NgayCong.TongSom += dstemp[dem].RaaSom;
					NgayCong.TongGioLam += dstemp[dem].TGLamTinhCong;
					NgayCong.TongGioThuc += dstemp[dem].TongGioThuc;
				}

				dem++;
			}

		}

		bool TinhVaoTreRaSom(cChk pIn, cChk pOut, cShift ca, ref TimeSpan VaoTre, ref TimeSpan RaaSom) {
			bool kq = false;
			VaoTre = RaaSom = _0gio;
			if (pIn.TimeStr.TimeOfDay > ca.Onduty + ca.LateGrace) {
				kq = true;
				VaoTre = pIn.TimeStr.TimeOfDay - ca.Onduty;
			}
			if (pOut.TimeStr.TimeOfDay < ca.Offduty - ca.EarlyGrace) {
				kq = true;
				RaaSom = ca.Offduty - pOut.TimeStr.TimeOfDay;
			}
			return kq;
		}

		bool IsHoliday(DateTime pDate) {
			string query = " SELECT ID,HDate,Holiday FROM Holiday where HDate = @date ";
			DataTable dt = SqlDataAccessHelper.ExecuteQueryString(query, new[] { "@date" }, new object[] { pDate.Date });
			if (dt.Rows.Count != 0) return true;
			return false;
		}

		public void ClearAll() {
			if (DSNgayCong_v71 != null) DSNgayCong_v71.Clear();
			if (DSVaoHopLe != null) DSVaoHopLe.Clear();
			if (DSRaHopLe != null) DSRaHopLe.Clear();
			if (DSGVaoBatThuong != null) DSGVaoBatThuong.Clear();
			if (DSGRaBatThuong != null) DSGRaBatThuong.Clear();
			if (DSGVaoKhac_ChoXL1_v72 != null) DSGVaoKhac_ChoXL1_v72.Clear();
			if (DSGRaKhac_ChoXL1_v72 != null) DSGRaKhac_ChoXL1_v72.Clear();
			if (DSVaoKhacChuaXL2_v72 != null) DSVaoKhacChuaXL2_v72.Clear();
			if (DSRaKhacChuaXL2_v72 != null) DSRaKhacChuaXL2_v72.Clear();
			if (DSG_DaXL_v10 != null) DSG_DaXL_v10.Clear();
			if (DSG_ChuaXL_v10 != null) DSG_ChuaXL_v10.Clear();
			if (DSVaoRaHopLe != null) DSVaoRaHopLe.Clear();
			if (DSVaoRaKhacChuaXL != null) DSVaoRaKhacChuaXL.Clear();

			GC.Collect();
			QuyGioTL = new cQuyGioTichLuy() { SumMinutes = 0, Used = 0, Thang = new DateTime() };
		}

		#endregion


		internal void TinhLaiNgayCongTren2Ca() {
			List<clsNgayCong_v71> kq = DSNgayCong_v71.FindAll(v71 => v71.Tren2Ca);
			if (kq == null || kq.Count == 0) return;
			foreach (clsNgayCong_v71 ngayCongV71 in kq) {
				if (ngayCongV71.TongGioLam < _8gio) {
					//Debug.WriteLine(ngayCongV71.DSVaoRaTH_v11[0].Vao.UserEnrollNumber + " " + ngayCongV71.NgayCong.ToString("d/MM") + " co tren 1 ca nhung tong gio lam ko du 8 tieng \n");
					continue;
				}
				//Debug.WriteLine(ngayCongV71.DSVaoRaTH_v11[0].Vao.UserEnrollNumber + " co ngay " + ngayCongV71.NgayCong.ToString("d/MM") + " co " + ngayCongV71.DSVaoRaTH_v11.Count + " ca");

				if (ngayCongV71.DSVaoRaTH_v11.Count == 2) {
					foreach (cChkInOut temp in ngayCongV71.DSVaoRaTH_v11) {
						//Debug.Write("vao: " + temp.Vao.TimeStr.ToString("h:mm") + "\t ra: " + temp.Raa.TimeStr.ToString("h:mm") + "\t tong gio: " + temp.TGLamTinhCong.TotalHours.ToString("0.##") +"\t ca: ");
						if (temp.ThuocCa != null){
							//Debug.Write(temp.ThuocCa.ShiftCode);
						}
						//Debug.WriteLine(" ");
					}

					if (ngayCongV71.DSVaoRaTH_v11[1].Vao.TimeStr.TimeOfDay > ngayCongV71.DSVaoRaTH_v11[1].Raa.TimeStr.TimeOfDay) {
						ngayCongV71.DSVaoRaTH_v11[1].HeSoPC = 0.6f;
						ngayCongV71.DSVaoRaTH_v11[1].TGTinhPC = ngayCongV71.TongGioLam - _8gio;
						ngayCongV71.DSVaoRaTH_v11[1].PhuCap = (float)(ngayCongV71.DSVaoRaTH_v11[1].HeSoPC * (ngayCongV71.DSVaoRaTH_v11[1].TGTinhPC.TotalHours / 8f));
						//Debug.WriteLine("phu cap dem: " + ngayCongV71.DSVaoRaTH_v11[1].TGTinhPC.TotalHours.ToString("0.##") + " \t = " + ngayCongV71.DSVaoRaTH_v11[1].PhuCap.ToString("0.##"));
					}
					else {
						ngayCongV71.DSVaoRaTH_v11[1].HeSoPC = 0.5f;
						ngayCongV71.DSVaoRaTH_v11[1].TGTinhPC = ngayCongV71.TongGioLam - _8gio;
						ngayCongV71.DSVaoRaTH_v11[1].PhuCap = (float)(ngayCongV71.DSVaoRaTH_v11[1].HeSoPC * (ngayCongV71.DSVaoRaTH_v11[1].TGTinhPC.TotalHours / 8f));
						//Debug.WriteLine("phu cap ngay: " + ngayCongV71.DSVaoRaTH_v11[1].TGTinhPC.TotalHours.ToString("0.##") + " \t = " + ngayCongV71.DSVaoRaTH_v11[1].PhuCap.ToString("0.##"));						
					}
					ngayCongV71.TongPhuCap = ngayCongV71.DSVaoRaTH_v11[1].PhuCap;
					//ngayCongV71.
					//Debug.WriteLine(" ");
				}
				else {
					//Debug.WriteLine("ALERT!");
				}
			}
		}
	}


	public class cChk : IComparable<cChk> {
		public cChk() { }

		#region Public Properties

		public int UserEnrollNumber { get; set; }

		public DateTime TimeDate { get; set; }

		public DateTime TimeStr { get; set; }

		public string OriginType { get; set; }

		public string NewType { get; set; }

		public string Source { get; set; }

		public int MachineNo { get; set; }

		/// <summary>
		/// -1 if NULL; >= 0 if NOT NULL
		/// </summary>
		public int WorkCode { get; set; }

		public int Type { get; set; }

		public bool KieuChamTay { get; set; }

		#endregion

		#region custom

		public DateTime Gio { get; set; }

		public TimeSpan VaoTre { get; set; }

		public TimeSpan RaaSom { get; set; }

		public TimeSpan GioLamThem { get; set; }

		public float Cong { get; set; }

		public float PhuCap { get; set; }

		public float HeSoPC { get; set; }
		public float TGTinhPC { get; set; }

		public List<cShift> ThuocCa { get; set; }

		public List<cChk> ListGioLQ_v3 { get; set; }
		#region ko còn áp dụng những hàm constructor này, xoá khi làm xong
		public cChk(int pUserEnrollNumber, DateTime pTimeDate, DateTime pTimeStr, string pOriginType, string pNewType, string pSource, int pMachineNo, int pWorkCode) {
			UserEnrollNumber = pUserEnrollNumber;
			TimeDate = pTimeDate;
			TimeStr = pTimeStr;
			OriginType = pOriginType;
			NewType = pNewType;
			Source = pSource;
			MachineNo = pMachineNo;
			WorkCode = pWorkCode;
			ListGioLQ_v3 = new List<cChk>();
			VaoTre = new TimeSpan(0, 0, 0);
			RaaSom = new TimeSpan(0, 0, 0);
			Cong = PhuCap = 0f;

		}
		public cChk(int pUserEnrollNumber, DateTime pTimeDate, DateTime pTimeStr, string pOriginType, string pNewType, string pSource, int pMachineNo, int pWorkCode, List<cChk> pGioLQ_v3) {
			UserEnrollNumber = pUserEnrollNumber;
			TimeDate = pTimeDate;
			TimeStr = pTimeStr;
			OriginType = pOriginType;
			NewType = pNewType;
			Source = pSource;
			MachineNo = pMachineNo;
			WorkCode = pWorkCode;
			ListGioLQ_v3 = pGioLQ_v3;
			VaoTre = new TimeSpan(0, 0, 0);
			RaaSom = new TimeSpan(0, 0, 0);
			Cong = PhuCap = 0f;
		}
		#endregion

		public int CompareTo(cChk b) {
			// Alphabetic sort name[A to Z]
			return this.TimeStr.CompareTo(b.TimeStr);
		}
		#endregion

	}

	public class cChkInOut {
		public cChk Vao { get; set; }
		public cChk Raa { get; set; }
		public TimeSpan TongGioThuc { get; set; }
		/// <summary>
		/// 0 : vào/ra khác(mặc định) ; 1 : vào/ra hợp lệ theo ca ; 2 : vào/ra đã xử lý
		/// </summary>
		public int Loai { get; set; }

		/// <summary>
		/// NULL if chấm thủ công
		/// </summary>
		public cShift ThuocCa { get; set; }
		public TimeSpan VaoTre { get; set; }
		public TimeSpan RaaSom { get; set; }
		public float HeSoPC { get; set; }
		public TimeSpan TGTinhPC { get; set; }
		public float PhuCap { get; set; }
		public TimeSpan TGLamTinhCong { get; set; }
		public float Cong { get; set; }
		public DateTime ThuocNgayCong { get; set; }
	}

	public class cChkInOutComparer : IComparer<cChkInOut> {
		public int Compare(cChkInOut x, cChkInOut y) {
			return x.Vao.CompareTo(y.Vao);
		}
	}

	public class clsUserAccount {
		#region Constructor

		public clsUserAccount() { }

		#endregion

		#region Private Variables

		//clsUserAccount objclsUserAccount;

		#endregion

		#region Public Properties

		public int UserID { get; set; }

		public string UserAccount { get; set; }

		public string Pass { get; set; }

		public int Privilege { get; set; }

		public int AreaID { get; set; }

		#endregion
	}


	public class cShiftSchedule {
		#region Constructor

		#endregion

		#region Private Variables

		//cShiftSchedule objclsShiftSch;

		#endregion

		#region Public Properties

		public int ID { get; set; }

		public int SchID { get; set; }

		public int T1 { get; set; }

		public int T2 { get; set; }

		public int T3 { get; set; }

		public int T4 { get; set; }

		public int T5 { get; set; }

		public int T6 { get; set; }

		public int T7 { get; set; }

		public cShiftSchedule() {
			ListT1 = new List<cShift>();
			ListT2 = new List<cShift>();
			ListT3 = new List<cShift>();
			ListT4 = new List<cShift>();
			ListT5 = new List<cShift>();
			ListT6 = new List<cShift>();
			ListT7 = new List<cShift>();
		}

		public cShiftSchedule(int pSchID, cShift pListTChungCho1Hang) {
			SchID = pSchID;
			if (ListT1 == null || ListT1.Count == 0) ListT1 = new List<cShift>() { pListTChungCho1Hang };
			else ListT1.Add(pListTChungCho1Hang);
			if (ListT2 == null || ListT2.Count == 0) ListT2 = new List<cShift>() { pListTChungCho1Hang };
			else ListT2.Add(pListTChungCho1Hang);
			if (ListT3 == null || ListT3.Count == 0) ListT3 = new List<cShift>() { pListTChungCho1Hang };
			else ListT3.Add(pListTChungCho1Hang);
			if (ListT4 == null || ListT4.Count == 0) ListT4 = new List<cShift>() { pListTChungCho1Hang };
			else ListT4.Add(pListTChungCho1Hang);
			if (ListT5 == null || ListT5.Count == 0) ListT5 = new List<cShift>() { pListTChungCho1Hang };
			else ListT5.Add(pListTChungCho1Hang);
			if (ListT6 == null || ListT6.Count == 0) ListT6 = new List<cShift>() { pListTChungCho1Hang };
			else ListT6.Add(pListTChungCho1Hang);
			if (ListT7 == null || ListT7.Count == 0) ListT7 = new List<cShift>() { pListTChungCho1Hang };
			else ListT7.Add(pListTChungCho1Hang);
		}

		#endregion

		#region custom properties

		public List<cShift> ListT1 { get; set; }

		public List<cShift> ListT2 { get; set; }

		public List<cShift> ListT3 { get; set; }

		public List<cShift> ListT4 { get; set; }

		public List<cShift> ListT5 { get; set; }

		public List<cShift> ListT6 { get; set; }

		public List<cShift> ListT7 { get; set; }

		#endregion
	}

	public class cShift {
		#region Constructor

		public cShift() { }

		#endregion

		#region Private Variables

		//private TimeSpan _OnLunch;
		//private TimeSpan _OffLunch;
		//private float _WorkingTime;
		//private float _Workingday;
		//private bool _IsNightTime;
		//private string _StartNT;
		//private string _EndNT;
		//private float _NoOutWT;
		//private float _NoInWT;
		//private int _LateGrace;
		//private bool _IsLateGrace;
		//private int _EarlyGrace;
		//private bool _IsEarlyGrace;
		//private bool _IsOT;
		//private int _OTlevel;
		//private bool _IsSunOT;
		//private int _SunOTLevel;
		//private bool _IsBeforeOT;
		//private int _BeforeOT;
		//private bool _IsAfterOT;
		//private int _AfterOT;
		//private float _AfterOTTotal;
		//private int _AfterOTDeduce;
		//private float _BeforeOTTotal;
		//private float _BeforeOTDeduce;
		//private int _LevelOT1;
		//private int _LevelOT2;
		//private bool _IsOTPoint;
		//private int _OTPoint;
		//private bool _IsHolidayOT;
		//private int _HolidayOTLevel;
		//private bool _IsLate;
		//private bool _IsEarly;
		//private int _WKOTLevel;
		//private bool _IsWKOTLevel;
		//private int _ShowPosition;
		//cShift objclsShifts;

		#endregion

		#region Public Properties

		public bool IsLate { get; set; }
		public TimeSpan tre { get; set; }
		public bool IsEarly { get; set; }
		public TimeSpan som { get; set; }

		public TimeSpan TGLam { get; set; }
		public TimeSpan WorkingTime { get; set; }

		public int ShiftID { get; set; }

		public string ShiftCode { get; set; }
		/// <summary>
		/// time of day
		/// </summary>
		public TimeSpan Onduty { get; set; }
		/// <summary>
		/// time of day
		/// </summary>
		public TimeSpan Offduty { get; set; }

		public int DayCount { get; set; }

		/// <summary>
		/// số phút
		/// </summary>
		public TimeSpan OnTimeIn { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
		public TimeSpan OnTimeOut { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
		public TimeSpan CutIn { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
		public TimeSpan CutOut { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
		public TimeSpan AfterOT { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
		public TimeSpan LateGrace { get; set; }
		/// <summary>
		/// số phút
		/// </summary>
		public TimeSpan EarlyGrace { get; set; }

		public float Workingday { get; set; }

		public int ShowPosition { get; set; }

		//public TimeSpan OnLunch
		//{
		//    get { return _OnLunch; }
		//    set { _OnLunch = value; }
		//}

		//public TimeSpan OffLunch
		//{
		//    get { return _OffLunch; }
		//    set { _OffLunch = value; }
		//}

		//public float WorkingTime
		//{
		//    get { return _WorkingTime; }
		//    set { _WorkingTime = value; }
		//}

		//public bool IsNightTime
		//{
		//    get { return _IsNightTime; }
		//    set { _IsNightTime = value; }
		//}

		//public string StartNT
		//{
		//    get { return _StartNT; }
		//    set { _StartNT = value; }
		//}

		//public string EndNT
		//{
		//    get { return _EndNT; }
		//    set { _EndNT = value; }
		//}

		//public float NoOutWT
		//{
		//    get { return _NoOutWT; }
		//    set { _NoOutWT = value; }
		//}

		//public float NoInWT
		//{
		//    get { return _NoInWT; }
		//    set { _NoInWT = value; }
		//}

		//public int LateGrace
		//{
		//    get { return _LateGrace; }
		//    set { _LateGrace = value; }
		//}

		//public bool IsLateGrace
		//{
		//    get { return _IsLateGrace; }
		//    set { _IsLateGrace = value; }
		//}

		//public int EarlyGrace
		//{
		//    get { return _EarlyGrace; }
		//    set { _EarlyGrace = value; }
		//}

		//public bool IsEarlyGrace
		//{
		//    get { return _IsEarlyGrace; }
		//    set { _IsEarlyGrace = value; }
		//}

		//public bool IsOT
		//{
		//    get { return _IsOT; }
		//    set { _IsOT = value; }
		//}

		//public int OTlevel
		//{
		//    get { return _OTlevel; }
		//    set { _OTlevel = value; }
		//}

		//public bool IsSunOT
		//{
		//    get { return _IsSunOT; }
		//    set { _IsSunOT = value; }
		//}

		//public int SunOTLevel
		//{
		//    get { return _SunOTLevel; }
		//    set { _SunOTLevel = value; }
		//}

		//public bool IsBeforeOT
		//{
		//    get { return _IsBeforeOT; }
		//    set { _IsBeforeOT = value; }
		//}

		//public int BeforeOT
		//{
		//    get { return _BeforeOT; }
		//    set { _BeforeOT = value; }
		//}

		//public bool IsAfterOT
		//{
		//    get { return _IsAfterOT; }
		//    set { _IsAfterOT = value; }
		//}

		//public float AfterOTTotal
		//{
		//    get { return _AfterOTTotal; }
		//    set { _AfterOTTotal = value; }
		//}

		//public int AfterOTDeduce
		//{
		//    get { return _AfterOTDeduce; }
		//    set { _AfterOTDeduce = value; }
		//}

		//public float BeforeOTTotal
		//{
		//    get { return _BeforeOTTotal; }
		//    set { _BeforeOTTotal = value; }
		//}

		//public float BeforeOTDeduce
		//{
		//    get { return _BeforeOTDeduce; }
		//    set { _BeforeOTDeduce = value; }
		//}

		//public int LevelOT1
		//{
		//    get { return _LevelOT1; }
		//    set { _LevelOT1 = value; }
		//}

		//public int LevelOT2
		//{
		//    get { return _LevelOT2; }
		//    set { _LevelOT2 = value; }
		//}

		//public bool IsOTPoint
		//{
		//    get { return _IsOTPoint; }
		//    set { _IsOTPoint = value; }
		//}

		//public int OTPoint
		//{
		//    get { return _OTPoint; }
		//    set { _OTPoint = value; }
		//}

		//public bool IsHolidayOT
		//{
		//    get { return _IsHolidayOT; }
		//    set { _IsHolidayOT = value; }
		//}

		//public int HolidayOTLevel
		//{
		//    get { return _HolidayOTLevel; }
		//    set { _HolidayOTLevel = value; }
		//}

		//public bool IsLate
		//{
		//    get { return _IsLate; }
		//    set { _IsLate = value; }
		//}

		//public bool IsEarly
		//{
		//    get { return _IsEarly; }
		//    set { _IsEarly = value; }
		//}

		//public int WKOTLevel
		//{
		//    get { return _WKOTLevel; }
		//    set { _WKOTLevel = value; }
		//}

		//public bool IsWKOTLevel
		//{
		//    get { return _IsWKOTLevel; }
		//    set { _IsWKOTLevel = value; }
		//}

		//public int ShowPosition
		//{
		//    get { return _ShowPosition; }
		//    set { _ShowPosition = value; }
		//}

		#endregion

	}

	public class clsSchedule {
		#region Constructor

		public clsSchedule() { }

		#endregion

		#region Private Variables

		//clsSchedule objclsSchedule;

		#endregion

		#region Public Properties

		public int SchID { get; set; }

		public string SchName { get; set; }

		public int InOutID { get; set; }

		public bool IsWeekend { get; set; }

		#endregion
	}

	public class clsRelationDept {
		#region Constructor

		public clsRelationDept() { }

		#endregion

		#region Private Variables

		//clsRelationDept objclsRelationDept;

		#endregion

		#region Public Properties

		public int ID { get; set; }

		public string Description { get; set; }

		public int RelationID { get; set; }

		public int LevelID { get; set; }

		#endregion
	}

	public class clsMenuPrivilege {
		#region Constructor

		public clsMenuPrivilege() { }

		#endregion

		#region Private Variables

		//clsMenuPrivilege objclsMenuPrivilege;

		#endregion

		#region Public Properties

		public int UserID { get; set; }

		public int MenuID { get; set; }

		public bool IsYes { get; set; }

		#endregion
	}

	public class clsMachinePrivilege {
		#region Constructor

		public clsMachinePrivilege() { }

		#endregion

		#region Private Variables

		//clsMachinePrivilege objclsMachinePrivilege;

		#endregion

		#region Public Properties

		public int UserID { get; set; }

		public int FunID { get; set; }

		public bool IsYes { get; set; }

		#endregion
	}

	public class clsSymReport {
		#region Public Properties

		public int ID { get; set; }

		public string SymName { get; set; }

		public string Symbol { get; set; }

		#endregion
	}

	public class clsAbsentSymbol {
		#region Constructor

		public clsAbsentSymbol() { }

		#endregion

		#region Private Variables

		private string _AbsentCode;
		private string _AbsentDescription;
		private string _AbsentSymbol;
		private int _IsYes;
		private bool _IsCount;
		//clsAbsentSymbol objclsAbsentSymbol;

		#endregion

		#region Public Properties

		public string AbsentCode {
			get { return _AbsentCode; }
			set { _AbsentCode = value; }
		}

		public string AbsentDescription {
			get { return _AbsentDescription; }
			set { _AbsentDescription = value; }
		}

		public string AbsentSymbol {
			get { return _AbsentSymbol; }
			set { _AbsentSymbol = value; }
		}

		public int IsYes {
			get { return _IsYes; }
			set { _IsYes = value; }
		}

		public bool IsCount {
			get { return _IsCount; }
			set { _IsCount = value; }
		}

		#endregion
	}

	public class clsAbsent {
		#region Constructor

		public clsAbsent() { }

		#endregion

		#region Private Variables

		private int _ID;
		private int _UserEnrollNumber;
		private string _UserFullCode;
		private System.DateTime _TimeDate;
		private string _AbsentCode;
		private int _Thang;
		private int _Nam;
		private float _Workingday;
		private float _WorkingTime;
		//clsAbsent objclsAbsent;

		#endregion

		#region Public Properties

		public int ID {
			get { return _ID; }
			set { _ID = value; }
		}

		public int UserEnrollNumber {
			get { return _UserEnrollNumber; }
			set { _UserEnrollNumber = value; }
		}

		public string UserFullCode {
			get { return _UserFullCode; }
			set { _UserFullCode = value; }
		}

		public System.DateTime TimeDate {
			get { return _TimeDate; }
			set { _TimeDate = value; }
		}

		public string AbsentCode {
			get { return _AbsentCode; }
			set { _AbsentCode = value; }
		}

		public int Thang {
			get { return _Thang; }
			set { _Thang = value; }
		}

		public int Nam {
			get { return _Nam; }
			set { _Nam = value; }
		}

		public float Workingday {
			get { return _Workingday; }
			set { _Workingday = value; }
		}

		public float WorkingTime {
			get { return _WorkingTime; }
			set { _WorkingTime = value; }
		}

		#endregion
	}

*/

}