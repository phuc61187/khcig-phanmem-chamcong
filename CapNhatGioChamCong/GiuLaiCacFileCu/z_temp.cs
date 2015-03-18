using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiuLaiCacFileCu {
	//class temp {

	//	public void ThemGio_v3(cChkInOut pGio) {
	//		if (pGio.MachineNo % 2 == 1) {
	//			// thêm giờ check in
	//			if (DSVao == null || DSVao.Count == 0) {
	//				XetCa_v3(pGio, LichTrinhLV);
	//				DSVao = new List<cChkInOut>() { pGio };
	//			}
	//			else {
	//				if (pGio.TimeStr - DSVao[DSVao.Count - 1].TimeStr < new TimeSpan(0, 30, 0)) {
	//					if (DSVao[DSVao.Count - 1].ListGioLQ_v3 == null || DSVao[DSVao.Count - 1].ListGioLQ_v3.Count == 0) DSVao[DSVao.Count - 1].ListGioLQ_v3 = new List<cChkInOut>() { pGio };
	//					else DSVao[DSVao.Count - 1].ListGioLQ_v3.Add(pGio);
	//				}
	//				else {
	//					XetCa_v3(pGio, LichTrinhLV);
	//					DSVao.Add(pGio);
	//				}
	//			}
	//		}
	//		else {
	//			if (DSRa == null || DSRa.Count == 0) {
	//				XetCa_v3(pGio, LichTrinhLV);
	//				DSRa = new List<cChkInOut>() { pGio };
	//			}
	//			else {
	//				if (pGio.TimeStr - DSRa[DSRa.Count - 1].TimeStr < new TimeSpan(0, 30, 0)) {
	//					if (DSRa[DSRa.Count - 1].ListGioLQ_v3 == null || DSRa[DSRa.Count - 1].ListGioLQ_v3.Count == 0) DSRa[DSRa.Count - 1].ListGioLQ_v3 = new List<cChkInOut>() { pGio };
	//					else DSRa[DSRa.Count - 1].ListGioLQ_v3.Add(pGio);
	//				}
	//				else {
	//					XetCa_v3(pGio, LichTrinhLV);
	//					DSRa.Add(pGio);
	//				}
	//			}
	//		}
	//	}

	//	public void ThemGioKoXetCa_v3(cChkInOut pGio) {
	//		if (pGio.MachineNo % 2 == 1) {
	//			// thêm giờ check in
	//			if (DSVao == null || DSVao.Count == 0) {
	//				DSVao = new List<cChkInOut>() { pGio };
	//				return;
	//			}

	//			if (pGio.TimeStr - DSVao[DSVao.Count - 1].TimeStr < new TimeSpan(0, 30, 0)) {
	//				if (DSVao[DSVao.Count - 1].ListGioLQ_v3 == null) DSVao[DSVao.Count - 1].ListGioLQ_v3 = new List<cChkInOut>();

	//				DSVao[DSVao.Count - 1].ListGioLQ_v3.Add(pGio);
	//			}
	//			else DSVao.Add(pGio);
	//		}
	//		else {
	//			if (DSRa == null || DSRa.Count == 0) {
	//				DSRa = new List<cChkInOut>() { pGio };
	//				return;
	//			}
	//			if (pGio.TimeStr - DSRa[DSRa.Count - 1].TimeStr < new TimeSpan(0, 30, 0)) {
	//				if (DSRa[DSRa.Count - 1].ListGioLQ_v3 == null) DSRa[DSRa.Count - 1].ListGioLQ_v3 = new List<cChkInOut>();

	//				DSRa[DSRa.Count - 1].ListGioLQ_v3.Add(pGio);
	//			}
	//			else DSRa.Add(pGio);
	//		}
	//	}


	//	public void XetGioHopLe_v71() {

	//		if (DSVao == null && DSRa == null) return;
	//		if (DSVao != null && DSRa == null) {
	//			DSVaoBatThuong = DSVao;
	//			return;
	//		} // thường áp dụng cho trường hợp chọn 1 ngày và không có giờ vào giờ ra
	//		if (DSVao == null && DSRa != null) {
	//			DSRaBatThuong = DSRa;
	//			return;
	//		} // thường áp dụng cho trường hợp chọn 1 ngày và không có giờ vào giờ ra

	//		// ReSharper disable PossibleNullReferenceException
	//		// xét những trường hợp check vào nhầm
	//		/*            for (int ifVao = 0; ifVao < DSVao.Count; ifVao++) {
	//						int kq = DSRa.FindIndex(item => (DSVao[ifVao].TimeStr < item.TimeStr)
	//													  && item.TimeStr - DSVao[ifVao].TimeStr < new TimeSpan(0, 10, 0));
	//						// ReSharper restore PossibleNullReferenceException
	//						/*if (kq != -1) {
	//							DSGioVaoSai_v4.Add(DSVao[ifVao]);
	//							DSVao.RemoveAt(ifVao);
	//							ifVao--;
	//						}#1#
	//						if (kq != -1) {
	//							int temp = DSRa[kq].MachineNo;
	//							DSRa[kq].MachineNo = DSVao[ifVao].MachineNo;
	//							DSVao[ifVao].MachineNo = temp;

	//							DSVao.Insert(ifVao, DSRa[kq]);
	//							DSRa.Insert(kq, DSVao[ifVao]);

	//							DSVao.RemoveAt(ifVao+1);
	//							DSRa.RemoveAt(kq+1);
	//						}
	//					}*/

	//		while (DSRa.Count != 0) {
	//			if (DSVao == null || DSVao.Count == 0) break;
	//			while (DSVao.Count != 0) {
	//				if (DSRa == null || DSRa.Count == 0) break;
	//				// những trường hợp giờ ra >> giờ vào => giờ vào ko tìm được giờ ra. MOVE DSVao TO DSVaoBatThuong
	//				if ((DSRa[0].TimeStr - DSVao[0].TimeStr > new TimeSpan(1, 0, 0, 0))
	//					|| DSVao[0].ThuocCa == null || DSVao[0].ThuocCa.Count == 0) {
	//					DSVaoBatThuong.Add(DSVao[0]);
	//					DSVao.RemoveAt(0);
	//					continue;
	//				}

	//				// những trường hợp giờ vào > giờ ra => giờ vào ko tìm được giờ ra. MOVE DSRa TO DSRaBatThuong
	//				if ((DSVao[0].TimeStr > DSRa[0].TimeStr)
	//					|| DSRa[0].ThuocCa == null || DSRa[0].ThuocCa.Count == 0) {
	//					DSRaBatThuong.Add(DSRa[0]);
	//					DSRa.RemoveAt(0);
	//					continue;
	//				}

	//				int i = 0;
	//				int tempCountRa = DSRa[0].ThuocCa.Count;
	//				for (i = 0; i < tempCountRa; i++) {
	//					//int index = DsGioVao[0].Ca.FindIndex(item => item.ShiftID == DsGioRa[0].Ca[i].ShiftID);
	//					// nếu so khớp được ca của giờ vào với ca của giờ ra
	//					cShift ca = DSVao[0].ThuocCa.Find(item => item.ShiftID == DSRa[0].ThuocCa[i].ShiftID);
	//					if (ca != null) {
	//						DSVao[0].ThuocCa = new List<cShift>() { ca };
	//						DSRa[0].ThuocCa = new List<cShift>() { ca };
	//						// xét giờ đó vào trễ ra sớm  
	//						TimeSpan temp = DSVao[0].ThuocCa[0].Onduty + DSVao[0].ThuocCa[0].LateGrace;
	//						if (DSVao[0].TimeStr.TimeOfDay > temp) DSVao[0].VaoTre = DSVao[0].TimeStr.TimeOfDay - DSVao[0].ThuocCa[0].Onduty;

	//						TimeSpan temp2 = DSRa[0].ThuocCa[0].Offduty - DSRa[0].ThuocCa[0].EarlyGrace;
	//						if (DSRa[0].TimeStr.TimeOfDay < temp2) DSRa[0].RaaSom = DSRa[0].ThuocCa[0].Offduty - DSRa[0].TimeStr.TimeOfDay;

	//						// tính giờ làm thêm cho nhân viên
	//						TimeSpan temp3 = DSRa[0].ThuocCa[0].Offduty + DSRa[0].ThuocCa[0].AfterOT;
	//						if (DSRa[0].TimeStr.TimeOfDay > temp3) DSRa[0].GioLamThem = DSRa[0].TimeStr.TimeOfDay - DSRa[0].ThuocCa[0].Offduty;

	//						TimeSpan temp1 = (ca.Offduty + new TimeSpan(ca.DayCount, 0, 0, 0) - ca.Onduty);
	//						TimeSpan tempGioLamThuc = temp1 - (DSVao[0].VaoTre + DSRa[0].RaaSom);
	//						DSRa[0].Cong = (float)(tempGioLamThuc.TotalHours / temp1.TotalHours) * ca.Workingday;
	//						switch (ca.ShiftID) {
	//							case 8:
	//								DSRa[0].PhuCap = 0.3f;
	//								break; // ca3
	//							case 30:
	//								DSRa[0].PhuCap = 0.3f;
	//								break; //ca3A
	//							case 19:
	//								DSRa[0].PhuCap = 0.5f;
	//								break; // ca1&2
	//							case 20:
	//								DSRa[0].PhuCap = 0.5f;
	//								break; // ca2&3
	//						}
	//						//DSRa[0].Cong = 
	//						//DSVao[0].Cong = 0f;
	//						DSVaoHopLe.Add(DSVao[0]);
	//						DSRaHopLe.Add(DSRa[0]);
	//						DSVao.RemoveAt(0);
	//						DSRa.RemoveAt(0);
	//						break; // break xét ca
	//					}
	//				}
	//				// i== tempCountRa => duyệt hết mà ko tìm được
	//				// i<tempCountRa ==> tìm được
	//				if (i < tempCountRa) continue;
	//				else {
	//					DSVaoBatThuong.Add(DSVao[0]);
	//					DSRaBatThuong.Add(DSRa[0]);
	//					DSVao.RemoveAt(0);
	//					DSRa.RemoveAt(0);
	//				}
	//			}
	//		}
	//		while (DSVao.Count != 0) {
	//			DSVaoBatThuong.Add(DSVao[0]);
	//			DSVao.RemoveAt(0);
	//		}
	//		while (DSRa.Count != 0) {
	//			DSRaBatThuong.Add(DSRa[0]);
	//			DSRa.RemoveAt(0);
	//		}

	//		// xét những trường hợp check ra nhầm
	//		/*            for (int iRa = 0; iRa < DSRaBatThuong.Count; iRa++) {
	//						//int kq1 = DSVaoHopLe.FindIndex(item => (item.TimeStr > DSRaBatThuong[iRa].TimeStr
	//						//                                            && item.TimeStr - DSRaBatThuong[iRa].TimeStr < new TimeSpan(0, 10, 0)));
	//						int kq2 = DSVaoBatThuong.FindIndex(item => (item.TimeStr > DSRaBatThuong[iRa].TimeStr
	//																	&& item.TimeStr - DSRaBatThuong[iRa].TimeStr < new TimeSpan(0, 10, 0)));
	//						if (kq2 != -1) {
	//							DSGioRaSai_v4.Add(DSRaBatThuong[iRa]);
	//							DSRaBatThuong.RemoveAt(iRa);
	//							iRa--;
	//						}
	//					}*/
	//		// xét những trường hợp check ra nhầm VER 8 MỚI
	//		for (int iRa = 0; iRa < DSRaBatThuong.Count; iRa++) {
	//			//int kq1 = DSVaoHopLe.FindIndex(item => (item.TimeStr > DSRaBatThuong[iRa].TimeStr
	//			//                                            && item.TimeStr - DSRaBatThuong[iRa].TimeStr < new TimeSpan(0, 10, 0)));
	//			int kq2 = DSVaoHopLe.FindIndex(item => (item.TimeStr > DSRaBatThuong[iRa].TimeStr
	//													&& item.TimeStr - DSRaBatThuong[iRa].TimeStr < new TimeSpan(0, 10, 0)));
	//			if (kq2 != -1) {
	//				DSGioRaSai_v4.Add(DSRaBatThuong[iRa]);
	//				DSRaBatThuong.RemoveAt(iRa);
	//				iRa--;
	//			}
	//		}
	//		for (int iVao = 0; iVao < DSVaoBatThuong.Count; iVao++) {
	//			//int kq1 = DSVaoHopLe.FindIndex(item => (item.TimeStr > DSRaBatThuong[iRa].TimeStr
	//			//                                            && item.TimeStr - DSRaBatThuong[iRa].TimeStr < new TimeSpan(0, 10, 0)));
	//			int kq2 = DSRaHopLe.FindIndex(item => (item.TimeStr > DSVaoBatThuong[iVao].TimeStr
	//												   && item.TimeStr - DSVaoBatThuong[iVao].TimeStr < new TimeSpan(0, 10, 0)));
	//			if (kq2 != -1) {
	//				DSGioVaoSai_v4.Add(DSVaoBatThuong[iVao]);
	//				DSVaoBatThuong.RemoveAt(iVao);
	//				iVao--;
	//			}
	//		}
	//	}

	//	public void XetGioHopLe_v72() {

	//		if (DSVao == null && DSRa == null) return;
	//		if (DSVao != null && DSRa == null) {
	//			DSVaoKhac_Raw_v72 = DSVao;
	//			return;
	//		} // thường áp dụng cho trường hợp chọn 1 ngày và không có giờ vào giờ ra
	//		if (DSVao == null && DSRa != null) {
	//			DSRaKhac_Raw_v72 = DSRa;
	//			return;
	//		} // thường áp dụng cho trường hợp chọn 1 ngày và không có giờ vào giờ ra

	//		#region bỏ so với ver 71

	//		// ReSharper disable PossibleNullReferenceException
	//		// xét những trường hợp check vào nhầm
	//		/*            for (int ifVao = 0; ifVao < DSVao.Count; ifVao++) {
	//						int kq = DSRa.FindIndex(item => (DSVao[ifVao].TimeStr < item.TimeStr)
	//													  && item.TimeStr - DSVao[ifVao].TimeStr < new TimeSpan(0, 10, 0));
	//						// ReSharper restore PossibleNullReferenceException
	//						/*if (kq != -1) {
	//							DSGioVaoSai_v4.Add(DSVao[ifVao]);
	//							DSVao.RemoveAt(ifVao);
	//							ifVao--;
	//						}#1#
	//						if (kq != -1) {
	//							int temp = DSRa[kq].MachineNo;
	//							DSRa[kq].MachineNo = DSVao[ifVao].MachineNo;
	//							DSVao[ifVao].MachineNo = temp;

	//							DSVao.Insert(ifVao, DSRa[kq]);
	//							DSRa.Insert(kq, DSVao[ifVao]);

	//							DSVao.RemoveAt(ifVao+1);
	//							DSRa.RemoveAt(kq+1);
	//						}
	//					}*/

	//		#endregion

	//		while (DSRa.Count != 0) {
	//			if (DSVao == null || DSVao.Count == 0) break;
	//			while (DSVao.Count != 0) {
	//				if (DSRa == null || DSRa.Count == 0) break;
	//				// những trường hợp giờ ra >> giờ vào => giờ vào ko tìm được giờ ra. MOVE DSVao TO DSVaoBatThuong
	//				if ((DSRa[0].TimeStr - DSVao[0].TimeStr > new TimeSpan(1, 0, 0, 0))
	//					/*|| DSVao[0].ThuocCa == null || DSVao[0].ThuocCa.Count == 0*/) {
	//					DSVaoKhac_Raw_v72.Add(DSVao[0]);
	//					DSVao.RemoveAt(0);
	//					continue;
	//				}

	//				// những trường hợp giờ vào > giờ ra => giờ vào ko tìm được giờ ra. MOVE DSRa TO DSRaBatThuong
	//				if ((DSVao[0].TimeStr > DSRa[0].TimeStr)
	//					/*|| DSRa[0].ThuocCa == null || DSRa[0].ThuocCa.Count == 0*/) {
	//					DSRaKhac_Raw_v72.Add(DSRa[0]);
	//					DSRa.RemoveAt(0);
	//					continue;
	//				}

	//				int i = 0;
	//				int tempCountRa = DSRa[0].ThuocCa.Count;
	//				for (i = 0; i < tempCountRa; i++) {
	//					//int index = DsGioVao[0].Ca.FindIndex(item => item.ShiftID == DsGioRa[0].Ca[i].ShiftID);
	//					// nếu so khớp được ca của giờ vào với ca của giờ ra
	//					cShift ca = DSVao[0].ThuocCa.Find(item => item.ShiftID == DSRa[0].ThuocCa[i].ShiftID);
	//					if (ca != null) {
	//						DSVao[0].ThuocCa = new List<cShift>() { ca };
	//						DSRa[0].ThuocCa = new List<cShift>() { ca };
	//						// xét giờ đó vào trễ ra sớm  
	//						TimeSpan temp = DSVao[0].ThuocCa[0].Onduty + DSVao[0].ThuocCa[0].LateGrace;
	//						if (DSVao[0].TimeStr.TimeOfDay > temp) DSVao[0].VaoTre = DSVao[0].TimeStr.TimeOfDay - DSVao[0].ThuocCa[0].Onduty;

	//						TimeSpan temp2 = DSRa[0].ThuocCa[0].Offduty - DSRa[0].ThuocCa[0].EarlyGrace;
	//						if (DSRa[0].TimeStr.TimeOfDay < temp2) DSRa[0].RaaSom = DSRa[0].ThuocCa[0].Offduty - DSRa[0].TimeStr.TimeOfDay;

	//						// tính giờ làm thêm cho nhân viên
	//						TimeSpan temp3 = DSRa[0].ThuocCa[0].Offduty + DSRa[0].ThuocCa[0].AfterOT;
	//						if (DSRa[0].TimeStr.TimeOfDay > temp3) DSRa[0].GioLamThem = DSRa[0].TimeStr.TimeOfDay - DSRa[0].ThuocCa[0].Offduty;

	//						TimeSpan temp1 = (ca.Offduty + new TimeSpan(ca.DayCount, 0, 0, 0) - ca.Onduty);
	//						TimeSpan tempGioLamThuc = temp1 - (DSVao[0].VaoTre + DSRa[0].RaaSom);
	//						DSRa[0].Cong = (float)(tempGioLamThuc.TotalHours / temp1.TotalHours) * ca.Workingday;
	//						DSVaoHopLe.Add(DSVao[0]);
	//						DSRaHopLe.Add(DSRa[0]);
	//						DSVao.RemoveAt(0);
	//						DSRa.RemoveAt(0);
	//						break; // break xét ca
	//					}
	//				}
	//				// i== tempCountRa => duyệt hết mà ko tìm được
	//				// i<tempCountRa ==> tìm được
	//				if (i < tempCountRa) {
	//					continue;
	//				}
	//				else {
	//					DSVaoKhac_Raw_v72.Add(DSVao[0]);
	//					DSRaKhac_Raw_v72.Add(DSRa[0]);
	//					DSVao.RemoveAt(0);
	//					DSRa.RemoveAt(0);
	//				}
	//			}
	//		}
	//		while (DSVao.Count != 0) {
	//			DSVaoKhac_Raw_v72.Add(DSVao[0]);
	//			DSVao.RemoveAt(0);
	//		}
	//		while (DSRa.Count != 0) {
	//			DSVaoKhac_Raw_v72.Add(DSRa[0]);
	//			DSRa.RemoveAt(0);
	//		}

	//		// xét những trường hợp check ra nhầm VER 8 MỚI
	//		if (DSRaKhac_Raw_v72 != null)
	//			for (int iRa = 0; iRa < DSRaKhac_Raw_v72.Count; iRa++) {
	//				//int kq1 = DSVaoHopLe.FindIndex(item => (item.TimeStr > DSRaBatThuong[iRa].TimeStr
	//				//                                            && item.TimeStr - DSRaBatThuong[iRa].TimeStr < new TimeSpan(0, 10, 0)));
	//				int kq2 = DSVaoHopLe.FindIndex(item => (item.TimeStr > DSRaKhac_Raw_v72[iRa].TimeStr
	//														&& item.TimeStr - DSRaKhac_Raw_v72[iRa].TimeStr < new TimeSpan(0, 10, 0)));
	//				if (kq2 != -1) {
	//					DSGioRaSai_v4.Add(DSRaKhac_Raw_v72[iRa]);
	//					DSRaKhac_Raw_v72.RemoveAt(iRa);
	//					iRa--;
	//				}
	//			}
	//		if (DSVaoKhac_Raw_v72 != null)
	//			for (int iVao = 0; iVao < DSVaoKhac_Raw_v72.Count; iVao++) {
	//				//int kq1 = DSVaoHopLe.FindIndex(item => (item.TimeStr > DSRaBatThuong[iRa].TimeStr
	//				//                                            && item.TimeStr - DSRaBatThuong[iRa].TimeStr < new TimeSpan(0, 10, 0)));
	//				int kq2 = DSRaHopLe.FindIndex(item => (item.TimeStr > DSVaoKhac_Raw_v72[iVao].TimeStr
	//													   && item.TimeStr - DSVaoKhac_Raw_v72[iVao].TimeStr < new TimeSpan(0, 10, 0)));
	//				if (kq2 != -1) {
	//					DSGioVaoSai_v4.Add(DSVaoKhac_Raw_v72[iVao]);
	//					DSVaoKhac_Raw_v72.RemoveAt(iVao);
	//					iVao--;
	//				}
	//			}
	//	}

	//	public void XetGioHopLe_v9() {
	//		List<cChkInOut> DSGioTH = new List<cChkInOut>();
	//		if (DSVaoBatThuong == null) DSVaoBatThuong = new List<cChkInOut>();
	//		if (DSRaBatThuong == null) DSRaBatThuong = new List<cChkInOut>();
	//		if (DSVaoKhac_Raw_v72 == null) DSVaoKhac_Raw_v72 = new List<cChkInOut>();
	//		if (DSRaKhac_Raw_v72 == null) DSRaKhac_Raw_v72 = new List<cChkInOut>();
	//		DSGioTH.AddRange(DSVao);
	//		DSGioTH.AddRange(DSRa);
	//		DSGioTH.Sort((@out, inOut) => {
	//			if (@out.TimeStr > inOut.TimeStr) return 1;
	//			return -1;
	//		});
	//		foreach (var cChkInOut in DSGioTH) {
	//			Debug.WriteLine("\ndate:" + cChkInOut.TimeDate.ToString("d/M") + " giờ: " + cChkInOut.TimeStr);
	//		}
	//		int i = 0, tempCountRa = 0;
	//		DateTime t = new DateTime();
	//		while (DSGioTH != null && DSGioTH.Count != 0) {
	//			if (DSGioTH[0].MachineNo % 2 == 0) {
	//				// đầu ds là Raa thì add bất thường
	//				DSRaBatThuong.Add(DSGioTH[0]);
	//				DSGioTH.RemoveAt(0);
	//				continue;
	//			}
	//			else {
	//				// đầu ds chắc chắn là vào, xét xem đó có phải phần tử cuối ds không? đi đến 
	//				if (DSGioTH.Count > 1) {
	//					// chưa phải cuối ds. xét xem cặp với nó có phải giờ ra ko
	//					if (DSGioTH[1].MachineNo % 2 == 1) {
	//						// vào - vào => add cái vào đầu tiên vào bất thường và loại bỏ cái đầu tiên đó và tiếp túc vòng lặp
	//						DSVaoBatThuong.Add(DSGioTH[0]);
	//						DSVaoBatThuong.RemoveAt(0);
	//						continue;
	//					}
	//					else {
	//						// cặp với giờ vào là giờ ra. xét xem có nằm trong 24 tiếng ko, nếu trong 24 tiếng thì mới xét ca được
	//						if (DSGioTH[1].TimeStr - DSGioTH[0].TimeStr > new TimeSpan(23, 0, 0)) {
	//							DSVaoBatThuong.Add(DSGioTH[0]);
	//							DSGioTH.RemoveAt(0);
	//							continue;
	//						}
	//						else {
	//							#region so khớp ca

	//							tempCountRa = DSGioTH[1].ThuocCa.Count;
	//							for (i = 0; i < tempCountRa; i++) {
	//								// nếu so khớp được ca của giờ vào với ca của giờ ra
	//								cShift ca = DSGioTH[0].ThuocCa.Find(item => item.ShiftID == DSGioTH[1].ThuocCa[i].ShiftID);
	//								if (ca != null) {
	//									DSGioTH[0].ThuocCa = new List<cShift>() { ca };
	//									DSGioTH[1].ThuocCa = new List<cShift>() { ca };

	//									#region xét vào sớm ra trễ và tính công

	//									// xét giờ đó vào trễ ra sớm  
	//									TimeSpan temp = DSGioTH[0].ThuocCa[0].Onduty + DSGioTH[0].ThuocCa[0].LateGrace;
	//									if (DSGioTH[0].TimeStr.TimeOfDay > temp) DSGioTH[0].VaoTre = DSGioTH[0].TimeStr.TimeOfDay - DSGioTH[0].ThuocCa[0].Onduty;

	//									TimeSpan temp2 = DSGioTH[1].ThuocCa[0].Offduty - DSGioTH[1].ThuocCa[0].EarlyGrace;
	//									if (DSGioTH[1].TimeStr.TimeOfDay < temp2) DSGioTH[1].RaaSom = DSGioTH[1].ThuocCa[0].Offduty - DSGioTH[1].TimeStr.TimeOfDay;

	//									// tính giờ làm thêm cho nhân viên
	//									TimeSpan temp3 = DSGioTH[1].ThuocCa[0].Offduty + DSGioTH[1].ThuocCa[0].AfterOT;
	//									if (DSGioTH[1].TimeStr.TimeOfDay > temp3) DSGioTH[1].GioLamThem = DSGioTH[1].TimeStr.TimeOfDay - DSGioTH[1].ThuocCa[0].Offduty;

	//									TimeSpan temp1 = (ca.Offduty + new TimeSpan(ca.DayCount, 0, 0, 0) - ca.Onduty);
	//									TimeSpan tempGioLamThuc = temp1 - (DSGioTH[0].VaoTre + DSGioTH[1].RaaSom);
	//									DSGioTH[1].Cong = (float)(tempGioLamThuc.TotalHours / temp1.TotalHours) * ca.Workingday;

	//									#endregion

	//									DSVaoHopLe.Add(DSGioTH[0]);
	//									DSRaHopLe.Add(DSGioTH[1]);
	//									DSGioTH.RemoveRange(0, 2);
	//									break; // break xét ca
	//								}
	//							}

	//							#endregion

	//							if (i < tempCountRa) {
	//								continue;
	//							} // tìm được 2 ca khớp nhau. tiếp tục vòng lặp
	//							DSVaoKhac_Raw_v72.Add(DSGioTH[0]); // không tìm được 2 ca khớp nhau. đưa vào danh sách vào khác ra khác
	//							DSRaKhac_Raw_v72.Add(DSGioTH[1]);
	//							DSGioTH.RemoveRange(0, 2);
	//						}
	//					}
	//				} // cuối danh sách
	//				else {
	//					DSVaoBatThuong.Add(DSGioTH[0]);
	//					DSGioTH.RemoveAt(0);
	//				}
	//			}
	//		}
	//		Debug.WriteLine("nhan vien: " + UserEnrollNumber + " ten : " + UserFullName);
	//		Debug.WriteLine("HOP LE: ");
	//		for (i = 0; i < DSVaoHopLe.Count; i++) Debug.WriteLine("Ngay: " + DSVaoHopLe[i].TimeDate.ToString("d/M") + " " + DSVaoHopLe[i].TimeStr.TimeOfDay + " -> " + DSRaHopLe[i].TimeStr.TimeOfDay);

	//		Debug.WriteLine("KHAC: ");
	//		for (i = 0; i < DSVaoKhac_Raw_v72.Count; i++) Debug.WriteLine("Ngay: " + DSVaoKhac_Raw_v72[i].TimeDate.ToString("d/M") + " " + DSVaoKhac_Raw_v72[i].TimeStr.TimeOfDay + " -> " + DSRaKhac_Raw_v72[i].TimeStr.TimeOfDay);

	//		Debug.WriteLine("VAO BAT THUONG: ");
	//		for (i = 0; i < DSVaoBatThuong.Count; i++) Debug.WriteLine("Ngay: " + DSVaoBatThuong[i].TimeDate.ToString("d/M") + " " + DSVaoBatThuong[i].TimeStr.TimeOfDay);

	//		Debug.WriteLine("RA BAT THUONG: ");
	//		for (i = 0; i < DSRaBatThuong.Count; i++) Debug.WriteLine("Ngay: " + DSRaBatThuong[i].TimeDate.ToString("d/M") + " " + DSRaBatThuong[i].TimeStr.TimeOfDay);
	//	}

	//	#region ver11

	//	public List<cChkInOut> DSGio_Raw_v11;
	//	public List<cChkInOut> DSVaoRa_v11;
	//	public List<cChkInOut> DSVaoRaBT_v11;

	//	public void ThemGio_v11(cChkInOut pGio) {
	//		if (DSGio_Raw_v11 == null) DSGio_Raw_v11 = new List<cChkInOut>();
	//		if (DSVaoRa_v11 == null) DSVaoRa_v11 = new List<cChkInOut>();
	//		if (DSVaoRaBT_v11 == null) DSVaoRaBT_v11 = new List<cChkInOut>();

	//		switch (pGio.Type) {
	//			case 0:
	//				if (DSGio_Raw_v11.Count == 0) {
	//					XetCa_v3(pGio, LichTrinhLV);
	//					DSGio_Raw_v11.Add(pGio);
	//					return;
	//				}
	//				// dsgio.Count > 0
	//				if (((pGio.MachineNo % 2) == (DSGio_Raw_v11[DSGio_Raw_v11.Count - 1].MachineNo % 2))
	//					&& pGio.TimeStr - DSGio_Raw_v11[DSGio_Raw_v11.Count - 1].TimeStr < new TimeSpan(0, 30, 0)) {
	//					if (DSGio_Raw_v11[DSGio_Raw_v11.Count - 1].ListGioLQ_v3 == null) DSGio_Raw_v11[DSGio_Raw_v11.Count - 1].ListGioLQ_v3 = new List<cChkInOut>();
	//					DSGio_Raw_v11[DSGio_Raw_v11.Count - 1].ListGioLQ_v3.Add(pGio);
	//				}
	//				else {
	//					XetCa_v3(pGio, LichTrinhLV);
	//					DSGio_Raw_v11.Add(pGio);
	//				}
	//				break;
	//			case 1: break;
	//			case 2: break;
	//			default: break;
	//		}
	//		if (DSGio_Raw_v11.Count == 0) {
	//			XetCa_v3(pGio, LichTrinhLV);
	//			DSGio_Raw_v11.Add(pGio);
	//			return;
	//		}
	//		// dsgio.Count > 0
	//		if (((pGio.MachineNo % 2) == (DSGio_Raw_v11[DSGio_Raw_v11.Count - 1].MachineNo % 2))
	//			&& pGio.TimeStr - DSGio_Raw_v11[DSGio_Raw_v11.Count - 1].TimeStr < new TimeSpan(0, 30, 0)) {
	//			if (DSGio_Raw_v11[DSGio_Raw_v11.Count - 1].ListGioLQ_v3 == null) DSGio_Raw_v11[DSGio_Raw_v11.Count - 1].ListGioLQ_v3 = new List<cChkInOut>();
	//			DSGio_Raw_v11[DSGio_Raw_v11.Count - 1].ListGioLQ_v3.Add(pGio);
	//		}
	//		else {
	//			XetCa_v3(pGio, LichTrinhLV);
	//			DSGio_Raw_v11.Add(pGio);
	//		}

	//	}

	//	private void ThemGioTheoLoai(cChkInOut pGio, List<cChkInOut> pDSVaoRa_v11, bool pCoXetCa, cShiftSchedule pLichTrinh) {

	//	}

	//	#endregion
	//	//-----------------------version cũ không còn sử dụng
	//	#region version cũ không còn sử dụng
	//	public List<cChkInOut> DSGioVaoKoRa_v4;
	//	public List<cChkInOut> DSGioRaKoVao_v4;

	//	public void ThemGio_v4(cChkInOut pGio) {
	//		if (pGio.MachineNo % 2 == 1) { // thêm giờ check in
	//			if (DSVao == null || DSVao.Count == 0)
	//				if (XetCa_v4(ref pGio, LichTrinhLV)) DSVao = new List<cChkInOut>() { pGio };
	//				else {
	//					if (pGio.TimeStr - DSVao[DSVao.Count - 1].TimeStr < new TimeSpan(0, 30, 0)) {
	//						if (DSVao[DSVao.Count - 1].ListGioLQ_v3 == null || DSVao[DSVao.Count - 1].ListGioLQ_v3.Count == 0)
	//							DSVao[DSVao.Count - 1].ListGioLQ_v3 = new List<cChkInOut>() { pGio };
	//						else
	//							DSVao[DSVao.Count - 1].ListGioLQ_v3.Add(pGio);
	//					}
	//					else {
	//						if (XetCa_v4(ref pGio, LichTrinhLV)) DSVao.Add(pGio);
	//					}
	//				}
	//		}
	//		else {
	//			if (DSRa == null || DSRa.Count == 0)
	//				if (XetCa_v4(ref pGio, LichTrinhLV)) DSRa = new List<cChkInOut>() { pGio };

	//				else {
	//					if (pGio.TimeStr - DSRa[DSRa.Count - 1].TimeStr < new TimeSpan(0, 30, 0)) {
	//						if (DSRa[DSRa.Count - 1].ListGioLQ_v3 == null || DSRa[DSRa.Count - 1].ListGioLQ_v3.Count == 0)
	//							DSRa[DSRa.Count - 1].ListGioLQ_v3 = new List<cChkInOut>() { pGio };
	//						else
	//							DSRa[DSRa.Count - 1].ListGioLQ_v3.Add(pGio);
	//					}
	//					else {
	//						if (XetCa_v4(ref pGio, LichTrinhLV)) DSRa.Add(pGio);
	//					}
	//				}
	//		}
	//	}

	//	public bool XetCa_v4(ref cChkInOut pGio, cShiftSchedule pLichTrinh) {
	//		cChkInOut kq = pGio;
	//		if (kq.MachineNo % 2 == 1) {//check in
	//			for (int i = 0; i < pLichTrinh.ListT1.Count; i++) {
	//				if (kq.TimeStr.TimeOfDay < pLichTrinh.ListT1[i].OnTimeIn || kq.TimeStr.TimeOfDay > pLichTrinh.ListT1[i].CutIn) continue;
	//				if (kq.ThuocCa == null || kq.ThuocCa.Count == 0) kq.ThuocCa = new List<cShift>() { pLichTrinh.ListT1[i] };
	//				else kq.ThuocCa.Add(pLichTrinh.ListT1[i]);
	//			}
	//		}
	//		else { //check out
	//			for (int i = 0; i < pLichTrinh.ListT1.Count; i++) {
	//				TimeSpan tempTimeStr = kq.TimeStr.TimeOfDay;
	//				// nếu lúc getLịch trình có add thêm 1 day timespan thì ở đây phải trừ lại 1 day, còn ko lấy thì ko trừ
	//				//tempTimeStr = tempTimeStr + new TimeSpan(pLichTrinh.ListT1[i].DayCount , 0 , 0 , 0);
	//				if (tempTimeStr < pLichTrinh.ListT1[i].OnTimeOut || tempTimeStr > pLichTrinh.ListT1[i].CutOut) continue;
	//				if (kq.ThuocCa == null || kq.ThuocCa.Count == 0) kq.ThuocCa = new List<cShift>() { pLichTrinh.ListT1[i] };
	//				else kq.ThuocCa.Add(pLichTrinh.ListT1[i]);
	//			}
	//		}
	//		//v 4----------- nếu ko có ca => giờ bất thường
	//		if (kq.ThuocCa == null || kq.ThuocCa.Count == 0) {
	//			if (kq.MachineNo % 2 == 1) // check in bất thường
	//			{
	//				if (DSVaoBatThuong == null || DSVaoBatThuong.Count == 0) DSVaoBatThuong = new List<cChkInOut>() { pGio };
	//				else DSVaoBatThuong.Add(pGio);
	//			}
	//			else {
	//				if (DSRaBatThuong == null || DSRaBatThuong.Count == 0) DSRaBatThuong = new List<cChkInOut>() { pGio };
	//				else DSRaBatThuong.Add(pGio);
	//			}
	//			return false;
	//		}
	//		//end-------------------
	//		return true;
	//	}

	//	public void XetGioHopLe_v3() {
	//		if (DSVao == null && DSRa == null) return;
	//		if (DSVao != null && DSRa == null) { DSVaoBatThuong = DSVao; return; }
	//		if (DSVao == null && DSRa != null) { DSRaBatThuong = DSRa; return; }

	//		cChkInOut[] arrDSGioVao = new cChkInOut[DSVao.Count];
	//		cChkInOut[] arrDSGioRa = new cChkInOut[DSRa.Count];
	//		bool[] arrFlagDSGioVao = new bool[DSVao.Count];
	//		bool[] arrFlagDSGioRa = new bool[DSRa.Count];
	//		arrFlagDSGioVao.Initialize();
	//		arrFlagDSGioRa.Initialize();

	//		DSVao.CopyTo(arrDSGioVao);
	//		DSRa.CopyTo(arrDSGioRa);

	//		DSVaoHopLe = new List<cChkInOut>();
	//		DSRaHopLe = new List<cChkInOut>();
	//		DSVaoBatThuong = new List<cChkInOut>();
	//		DSRaBatThuong = new List<cChkInOut>();

	//		for (int iRa = 0; iRa < arrDSGioRa.Length; iRa++) {
	//			for (int iVao = 0; iVao < arrDSGioVao.Length; iVao++) {
	//				if (arrFlagDSGioRa[iRa]) break;
	//				if (arrFlagDSGioVao[iVao]) continue;

	//				if (arrDSGioRa[iRa].ThuocCa == null) {
	//					DSRaBatThuong.Add(arrDSGioRa[iRa]);
	//					arrFlagDSGioRa[iRa] = true;
	//					break;
	//				}

	//				if (arrDSGioRa[iRa].TimeStr < arrDSGioVao[iVao].TimeStr) break;

	//				if (arrDSGioVao[iVao].ThuocCa == null) {
	//					DSVaoBatThuong.Add(arrDSGioVao[iVao]);
	//					arrFlagDSGioVao[iVao] = true;
	//					continue;
	//				}


	//				if ((arrDSGioRa[iRa].TimeStr - arrDSGioVao[iVao].TimeStr) > new TimeSpan(1, 0, 0, 0)) continue;

	//				for (int iCaRa = 0; iCaRa < arrDSGioRa[iRa].ThuocCa.Count; iCaRa++) {
	//					if (arrFlagDSGioVao[iVao]) break;

	//					for (int iCaVao = 0; iCaVao < arrDSGioVao[iVao].ThuocCa.Count; iCaVao++) {

	//						if (arrDSGioRa[iRa].ThuocCa[iCaRa].ShiftID == arrDSGioVao[iVao].ThuocCa[iCaVao].ShiftID) {
	//							arrFlagDSGioVao[iVao] = true;
	//							arrFlagDSGioRa[iRa] = true;
	//							DSVaoHopLe.Add(arrDSGioVao[iVao]);
	//							DSRaHopLe.Add(arrDSGioRa[iRa]);
	//							//add ds gio hop le
	//							break;
	//						}
	//					}

	//				}
	//			}
	//		}



	//		for (int i = 0; i < arrDSGioRa.Length; i++)
	//			if (arrFlagDSGioRa[i] == false) DSRaBatThuong.Add(arrDSGioRa[i]);

	//		for (int j = 0; j < arrDSGioVao.Length; j++)
	//			if (arrFlagDSGioVao[j] == false) DSVaoBatThuong.Add(arrDSGioVao[j]);
	//	}

	//	public void XetGioHopLe_v5() {
	//		if (DSVao == null && DSRa == null) return;
	//		if (DSVao != null && DSRa == null) { DSVaoBatThuong = DSVao; return; }
	//		if (DSVao == null && DSRa != null) { DSRaBatThuong = DSRa; return; }

	//		cChkInOut[] arrDSGioVao = new cChkInOut[DSVao.Count];
	//		cChkInOut[] arrDSGioRa = new cChkInOut[DSRa.Count];
	//		bool[] arrFlagDSGioVao = new bool[DSVao.Count];
	//		bool[] arrFlagDSGioRa = new bool[DSRa.Count];
	//		arrFlagDSGioVao.Initialize();
	//		arrFlagDSGioRa.Initialize();

	//		DSVao.CopyTo(arrDSGioVao);
	//		DSRa.CopyTo(arrDSGioRa);

	//		DSVaoHopLe = new List<cChkInOut>();
	//		DSRaHopLe = new List<cChkInOut>();
	//		DSVaoBatThuong = new List<cChkInOut>();
	//		DSRaBatThuong = new List<cChkInOut>();
	//		DSGioVaoKoRa_v4 = new List<cChkInOut>();
	//		DSGioRaKoVao_v4 = new List<cChkInOut>();


	//		while (DSRa.Count != 0) {
	//			if (DSVao == null || DSVao.Count == 0) break;
	//			while (DSVao.Count != 0) {
	//				if (DSRa == null || DSRa.Count == 0) break;
	//				// những trường hợp giờ ra >> giờ vào => giờ vào ko tìm được giờ ra
	//				if (DSRa[0].TimeStr - DSVao[0].TimeStr > new TimeSpan(1, 0, 0, 0)) {
	//					DSGioVaoKoRa_v4.Add(DSVao[0]);
	//					DSVao.RemoveAt(0);
	//					continue;
	//				}

	//				if (DSVao[0].TimeStr > DSRa[0].TimeStr) {
	//					DSGioRaKoVao_v4.Add(DSRa[0]);
	//					DSRa.RemoveAt(0);
	//					continue;
	//				}

	//				if (DSVao[0].ThuocCa == null || DSVao[0].ThuocCa.Count == 0) {
	//					DSVaoBatThuong.Add(DSVao[0]);
	//					DSVao.RemoveAt(0);
	//					continue;
	//				}

	//				if (DSRa[0].ThuocCa == null || DSRa[0].ThuocCa.Count == 0) {
	//					DSRaBatThuong.Add(DSRa[0]);
	//					DSRa.RemoveAt(0);
	//					continue;
	//				}

	//				int i = 0;
	//				int tempCountRa = DSRa[0].ThuocCa.Count;
	//				for (i = 0; i < tempCountRa; i++) {
	//					int index = DSVao[0].ThuocCa.FindIndex(item => item.ShiftID == DSRa[0].ThuocCa[i].ShiftID);
	//					if (index != -1) {
	//						DSVaoHopLe.Add(DSVao[0]);
	//						DSRaHopLe.Add(DSRa[0]);
	//						DSVao.RemoveAt(0);
	//						DSRa.RemoveAt(0);
	//						break;// break xét ca
	//					}
	//				}
	//				// i== tempCountRa => duyệt hết mà ko tìm được
	//				// i<tempCountRa ==> tìm được
	//				if (i < tempCountRa) { continue; }
	//				else {
	//					DSVaoBatThuong.Add(DSVao[0]);
	//					DSRaBatThuong.Add(DSRa[0]);
	//					DSVao.RemoveAt(0);
	//					DSRa.RemoveAt(0);
	//				}
	//			}
	//		}
	//		while (DSVao.Count != 0) {
	//			if (DSVao[0].ThuocCa == null || DSVao[0].ThuocCa.Count == 0) {
	//				DSVaoBatThuong.Add(DSVao[0]);
	//				DSVao.RemoveAt(0);
	//			}
	//			else {
	//				DSGioVaoKoRa_v4.Add(DSVao[0]);
	//				DSVao.RemoveAt(0);
	//			}
	//		}
	//		while (DSRa.Count != 0) {
	//			if (DSRa[0].ThuocCa == null || DSRa[0].ThuocCa.Count == 0) {
	//				DSRaBatThuong.Add(DSRa[0]);
	//				DSRa.RemoveAt(0);
	//			}
	//			else {
	//				DSGioRaKoVao_v4.Add(DSRa[0]);
	//				DSRa.RemoveAt(0);
	//			}
	//		}
	//		#region
	//		/*for (int iRa = 0; iRa < arrDSGioRa.Length; iRa++) {
	//			for (int iVao = 0; iVao < arrDSGioVao.Length; iVao++) {
	//				if (arrFlagDSGioRa[iRa]) break;
	//				if (arrFlagDSGioVao[iVao]) continue;

	//				if (arrDSGioRa[iRa].Ca == null) {
	//					DSGioRaBatThuong.Add(arrDSGioRa[iRa]);
	//					arrFlagDSGioRa[iRa] = true;
	//					break;
	//				}

	//				if (arrDSGioRa[iRa].TimeStr < arrDSGioVao[iVao].TimeStr) break;

	//				if (arrDSGioVao[iVao].Ca == null) {
	//					DSGioVaoBatThuong.Add(arrDSGioVao[iVao]);
	//					arrFlagDSGioVao[iVao] = true;
	//					continue;
	//				}


	//				if ((arrDSGioRa[iRa].TimeStr - arrDSGioVao[iVao].TimeStr) > new TimeSpan(1, 0, 0, 0)) continue;

	//				for (int iCaRa = 0; iCaRa < arrDSGioRa[iRa].Ca.Count; iCaRa++) {
	//					if (arrFlagDSGioVao[iVao]) break;

	//					for (int iCaVao = 0; iCaVao < arrDSGioVao[iVao].Ca.Count; iCaVao++) {

	//						if (arrDSGioRa[iRa].Ca[iCaRa].ShiftID == arrDSGioVao[iVao].Ca[iCaVao].ShiftID) {
	//							arrFlagDSGioVao[iVao] = true;
	//							arrFlagDSGioRa[iRa] = true;
	//							DSGioVaoHopLe.Add(arrDSGioVao[iVao]);
	//							DSGioRaHopLe.Add(arrDSGioRa[iRa]);
	//							//add ds gio hop le
	//							break;
	//						}
	//					}

	//				}
	//			}
	//		}



	//		for (int i = 0; i < arrDSGioRa.Length; i++)
	//			if (arrFlagDSGioRa[i] == false) DSGioRaBatThuong.Add(arrDSGioRa[i]);

	//		for (int j = 0; j < arrDSGioVao.Length; j++)
	//			if (arrFlagDSGioVao[j] == false) DSGioVaoBatThuong.Add(arrDSGioVao[j]);*/
	//		#endregion

	//	}

	//	public void XetGioHopLe_v6() {
	//		if (DSVao == null && DSRa == null) return;
	//		if (DSVao != null && DSRa == null) { DSVaoBatThuong = DSVao; return; }
	//		if (DSVao == null && DSRa != null) { DSRaBatThuong = DSRa; return; }

	//		cChkInOut[] arrDSGioVao = new cChkInOut[DSVao.Count];
	//		cChkInOut[] arrDSGioRa = new cChkInOut[DSRa.Count];
	//		bool[] arrFlagDSGioVao = new bool[DSVao.Count];
	//		bool[] arrFlagDSGioRa = new bool[DSRa.Count];
	//		arrFlagDSGioVao.Initialize();
	//		arrFlagDSGioRa.Initialize();

	//		DSVao.CopyTo(arrDSGioVao);
	//		DSRa.CopyTo(arrDSGioRa);

	//		DSVaoHopLe = new List<cChkInOut>();
	//		DSRaHopLe = new List<cChkInOut>();
	//		DSVaoBatThuong = new List<cChkInOut>();
	//		DSRaBatThuong = new List<cChkInOut>();
	//		DSGioVaoKoRa_v4 = new List<cChkInOut>();
	//		DSGioRaKoVao_v4 = new List<cChkInOut>();
	//		DSGioVaoSai_v4 = new List<cChkInOut>();
	//		DSGioRaSai_v4 = new List<cChkInOut>();

	//		int iwRa = 0;
	//		int iwVao = 0;

	//		// xét những trường hợp check vào nhầm
	//		for (int ifVao = 0; ifVao < DSVao.Count; ifVao++) {
	//			int kq = DSRa.FindIndex(item => (item.TimeStr > DSVao[ifVao].TimeStr)
	//										  && item.TimeStr - DSVao[ifVao].TimeStr < new TimeSpan(0, 10, 0));
	//			if (kq != -1) {
	//				DSGioVaoSai_v4.Add(DSVao[ifVao]);
	//				DSVao.RemoveAt(ifVao);
	//				ifVao--;
	//			}
	//		}

	//		while (DSRa.Count != 0) {
	//			if (DSVao == null || DSVao.Count == 0) break;
	//			while (DSVao.Count != 0) {
	//				if (DSRa == null || DSRa.Count == 0) break;
	//				// những trường hợp giờ ra >> giờ vào => giờ vào ko tìm được giờ ra
	//				if ((DSRa[0].TimeStr - DSVao[0].TimeStr > new TimeSpan(1, 0, 0, 0))
	//				|| DSVao[0].ThuocCa == null || DSVao[0].ThuocCa.Count == 0) {
	//					DSVaoBatThuong.Add(DSVao[0]);
	//					DSVao.RemoveAt(0);
	//					continue;
	//				}

	//				if ((DSVao[0].TimeStr > DSRa[0].TimeStr)
	//				|| DSRa[0].ThuocCa == null || DSRa[0].ThuocCa.Count == 0) {
	//					DSRaBatThuong.Add(DSRa[0]);
	//					DSRa.RemoveAt(0);
	//					continue;
	//				}

	//				int i = 0;
	//				int tempCountRa = DSRa[0].ThuocCa.Count;
	//				for (i = 0; i < tempCountRa; i++) {
	//					//int index = DsGioVao[0].Ca.FindIndex(item => item.ShiftID == DsGioRa[0].Ca[i].ShiftID);
	//					cShift ca = DSVao[0].ThuocCa.Find(item => item.ShiftID == DSRa[0].ThuocCa[i].ShiftID);
	//					if (ca != null) {
	//						DSVao[0].ThuocCa = new List<cShift>() { ca };
	//						DSRa[0].ThuocCa = new List<cShift>() { ca };
	//						// xét giờ đó vào sớm ra trễ

	//						DSVaoHopLe.Add(DSVao[0]);
	//						DSRaHopLe.Add(DSRa[0]);
	//						DSVao.RemoveAt(0);
	//						DSRa.RemoveAt(0);
	//						break;// break xét ca
	//					}
	//				}
	//				// i== tempCountRa => duyệt hết mà ko tìm được
	//				// i<tempCountRa ==> tìm được
	//				if (i < tempCountRa) { continue; }
	//				else {
	//					DSVaoBatThuong.Add(DSVao[0]);
	//					DSRaBatThuong.Add(DSRa[0]);
	//					DSVao.RemoveAt(0);
	//					DSRa.RemoveAt(0);
	//				}
	//			}
	//		}
	//		while (DSVao.Count != 0) {
	//			DSVaoBatThuong.Add(DSVao[0]);
	//			DSVao.RemoveAt(0);
	//		}
	//		while (DSRa.Count != 0) {
	//			DSRaBatThuong.Add(DSRa[0]);
	//			DSRa.RemoveAt(0);
	//		}

	//		// xét những trường hợp check ra nhầm
	//		for (int iRa = 0; iRa < DSRaBatThuong.Count; iRa++) {
	//			int kq = DSVaoHopLe.FindIndex(item => (item.TimeStr > DSRaBatThuong[iRa].TimeStr
	//														&& item.TimeStr - DSRaBatThuong[iRa].TimeStr < new TimeSpan(0, 10, 0)));
	//			int kq2 = DSVaoBatThuong.FindIndex(item => (item.TimeStr > DSRaBatThuong[iRa].TimeStr
	//														&& item.TimeStr - DSRaBatThuong[iRa].TimeStr < new TimeSpan(0, 10, 0)));
	//			if (kq != -1 || kq2 != -1) {
	//				DSGioRaSai_v4.Add(DSRaBatThuong[iRa]);
	//				DSRaBatThuong.RemoveAt(iRa);
	//				iRa--;
	//			}
	//		}

	//	}

	//	public void ThemGio_v5(cChkInOut pGio) {
	//		clsNgayCong_v71 temp = DSNgayCong_v71.Find(item => item.NgayCong == pGio.TimeDate);

	//		if (pGio.MachineNo % 2 == 1) { // thêm giờ check in
	//			if (temp.DSVaoHL_v71 == null || temp.DSVaoHL_v71.Count == 0) {
	//				XetCa_v3(pGio, LichTrinhLV);
	//				temp.DSVaoHL_v71 = new List<cChkInOut>() { pGio };
	//			}
	//			else {
	//				if (pGio.TimeStr - temp.DSVaoHL_v71[temp.DSVaoHL_v71.Count - 1].TimeStr < new TimeSpan(0, 30, 0)) {
	//					if (temp.DSVaoHL_v71[temp.DSVaoHL_v71.Count - 1].ListGioLQ_v3 == null || temp.DSVaoHL_v71[temp.DSVaoHL_v71.Count - 1].ListGioLQ_v3.Count == 0)
	//						temp.DSVaoHL_v71[temp.DSVaoHL_v71.Count - 1].ListGioLQ_v3 = new List<cChkInOut>() { pGio };
	//					else
	//						temp.DSVaoHL_v71[temp.DSVaoHL_v71.Count - 1].ListGioLQ_v3.Add(pGio);
	//				}
	//				else {
	//					XetCa_v3(pGio, LichTrinhLV);
	//					temp.DSVaoHL_v71.Add(pGio);
	//				}
	//			}
	//		}
	//		else {
	//			if (temp.DSRaHL_v71 == null || temp.DSRaHL_v71.Count == 0) {
	//				XetCa_v3(pGio, LichTrinhLV);
	//				DSRa = new List<cChkInOut>() { pGio };
	//			}
	//			else {
	//				if (pGio.TimeStr - DSRa[DSRa.Count - 1].TimeStr < new TimeSpan(0, 30, 0)) {
	//					if (DSRa[DSRa.Count - 1].ListGioLQ_v3 == null || DSRa[DSRa.Count - 1].ListGioLQ_v3.Count == 0)
	//						DSRa[DSRa.Count - 1].ListGioLQ_v3 = new List<cChkInOut>() { pGio };
	//					else
	//						DSRa[DSRa.Count - 1].ListGioLQ_v3.Add(pGio);
	//				}
	//				else {
	//					XetCa_v3(pGio, LichTrinhLV);
	//					DSRa.Add(pGio);
	//				}
	//			}
	//		}

	//	}

	//	public cChkInOut XetCa_v5(cChkInOut pGio, cShiftSchedule pLichTrinh) {
	//		cChkInOut kq = pGio;
	//		if (kq.MachineNo % 2 == 1) {//check in
	//			for (int i = 0; i < pLichTrinh.ListT1.Count; i++) {
	//				if (kq.TimeStr.TimeOfDay < pLichTrinh.ListT1[i].OnTimeIn || kq.TimeStr.TimeOfDay > pLichTrinh.ListT1[i].CutIn) continue;
	//				if (kq.ThuocCa == null || kq.ThuocCa.Count == 0) kq.ThuocCa = new List<cShift>() { pLichTrinh.ListT1[i] };
	//				else kq.ThuocCa.Add(pLichTrinh.ListT1[i]);
	//			}
	//		}
	//		else { //check out
	//			for (int i = 0; i < pLichTrinh.ListT1.Count; i++) {
	//				TimeSpan tempTimeStr = kq.TimeStr.TimeOfDay;
	//				// nếu lúc getLịch trình có add thêm 1 day timespan thì ở đây phải trừ lại 1 day, còn ko lấy thì ko trừ
	//				//tempTimeStr = tempTimeStr + new TimeSpan(pLichTrinh.ListT1[i].DayCount , 0 , 0 , 0);
	//				if (tempTimeStr < pLichTrinh.ListT1[i].OnTimeOut || tempTimeStr > pLichTrinh.ListT1[i].CutOut) continue;
	//				if (kq.ThuocCa == null || kq.ThuocCa.Count == 0) kq.ThuocCa = new List<cShift>() { pLichTrinh.ListT1[i] };
	//				else kq.ThuocCa.Add(pLichTrinh.ListT1[i]);
	//			}
	//		}
	//		return kq;
	//	}
	//	#endregion
	//}
}
