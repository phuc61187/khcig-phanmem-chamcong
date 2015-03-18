using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ChamCong_v02.BUS;
using ChamCong_v02.DAO;
using log4net;

namespace ChamCong_v02.DTO {

	public class cPhongBan {
		public int ID;
		public string TenPhongBan;
	}

	public class cChkComparer : IComparer<cChk> {
		public int Compare(cChk x, cChk y) {
			return x.TimeStr.CompareTo(y.TimeStr);
			//return 1;
		}
	}

	public class cChkInOutComparer : IComparer<cChkInOut> {
		public int Compare(cChkInOut x, cChkInOut y) {
			return x.TimeStrDaiDien.CompareTo(y.TimeStrDaiDien);
			//return 1;
		}
	}

	public struct HeSo {
		public Single LuongCB;
		public Single LuongCV;
		public Single BHXH_YT_TN;
	}

	public class cUserInfo {
		public ILog log = LogManager.GetLogger("cUserInfo");

		#region Public Properties

		public string UserFullCode;
		public string UserFullName;
		public int UserEnrollNumber;
		public int UserIDTitle;
		public string TitleName;
		public cPhongBan PBCap1;
		public cPhongBan PBCap2;

		public bool MacDinhTinhPC150;
		public cShiftSchedule LichTrinhLV;
		public List<cShift> DSCa = new List<cShift>();
		public List<cShift> DSCaMoRong = new List<cShift>();
		public List<cChk> ds_CheckInn_A = new List<cChk>();
		public List<cChk> ds_CheckOut_A = new List<cChk>();
		public List<cChk> ds_Check_A = new List<cChk>();
		public List<cChkInOut_A> DS_CIO_A = new List<cChkInOut_A>();
		public List<cChkInOut_V> DS_CIO_V = new List<cChkInOut_V>();
		public List<cChkInOut> DSVaoRa = new List<cChkInOut>();
		public List<cNgayCong> DSNgayCong = new List<cNgayCong>();


		public HeSo HeSo;
		#endregion


		public void UpdateUserInfo(DataRow[] userinfo) {
			DataRow info = userinfo[0];
			UserEnrollNumber = (int)info["UserEnrollNumber"];
			UserFullName = (string)info["UserFullName"];
			UserFullCode = (string)info["UserFullCode"];
			UserIDTitle = (int)info["UserIDTitle"];
			TitleName = (string)info["TitleName"];
			if (info["IDD_1"] != DBNull.Value)
				PBCap1 = new cPhongBan() { ID = (int)info["IDD_1"], TenPhongBan = info["Description_1"].ToString() };
			if (info["IDD_2"] != DBNull.Value)
				PBCap2 = new cPhongBan() { ID = (int)info["IDD_2"], TenPhongBan = info["Description_2"].ToString() };
			HeSo.LuongCB = (info["HeSoLuongCB"] != DBNull.Value) ? (Single)info["HeSoLuongCB"] : 0f;
			HeSo.LuongCV = (info["HeSoLuongSP"] != DBNull.Value) ? (Single)info["HeSoLuongSP"] : 0f;
			HeSo.BHXH_YT_TN = HeSo.LuongCB;// hệ số bảo hiểm, bình thường = hệ số lương cb. riêng gd, pgd thì + 0.5, 0.6
			MacDinhTinhPC150 = (info["TinhPC150"] != DBNull.Value) ? (bool)info["TinhPC150"] : false; //trong query là d1.TinhPC150
			int schid = (int)info["SchID"];
			cShiftSchedule temp = ThamSo.DSLichTrinh.Find(o => o.SchID == schid);
			DSCa = temp.ListT1;
			DSCaMoRong = ThamSo.TaoDSCaMoRong(DSCa);
		}

		public void ArrayRowsToDSCheck_A(DataRow[] arrRows) { //cấu trúc datatble là cấu trúc bảng checkinout
			#region reset lại các danh sách
			ds_CheckInn_A.Clear();
			ds_CheckOut_A.Clear();
			ds_Check_A.Clear();
			#endregion
			if (arrRows.Length != 0) {
				foreach (DataRow row in arrRows) {
					int MachineNo = (int)row["MachineNo"];
					string Source = (string)row["Source"];
					DateTime TimeStr = (DateTime)row["TimeStr"];
					cChk check;
					if (MachineNo % 2 == 1) {
						cChkInn_A checkInn = new cChkInn_A() { TimeStr = TimeStr, Source = Source, GioLienQuan = null, MachineNo = MachineNo };
						ds_CheckInn_A.Add(checkInn);
						check = checkInn;
					}
					else {
						cChkOut_A checkOut = new cChkOut_A() { TimeStr = TimeStr, Source = Source, GioLienQuan = null, MachineNo = MachineNo };
						ds_CheckOut_A.Add(checkOut);
						check = checkOut;
					}
					ds_Check_A.Add(check);
				}
				LoaiBoCheck30phut(ds_CheckInn_A); //dataTable lấy lên đã sort sẵn nên dùng hàm này ko tích hợp sort
				LoaiBoCheck30phut(ds_CheckOut_A);
			}
		}

		public void LoaiBoCheck30phut(List<cChk> dscheck) { // lọc này phải dảm bảo sort trước
			if (dscheck == null || dscheck.Count == 0 || dscheck.Count == 1) return;
			int i = 0;
			while (i + 1 < dscheck.Count) {
				cChk before = dscheck[i];
				cChk afterr = dscheck[i + 1];
				if (afterr.TimeStr - before.TimeStr <= ThamSo._30phut) {
					if (before.GioLienQuan == null) before.GioLienQuan = new List<cChk>();
					before.GioLienQuan.Add(afterr);
					dscheck.Remove(afterr);
				}
				else i++;
			}
		}

		public void ArrayRowsToDS_CIO_V(DataRow[] arrRows) { // cấu trúc là cấu trúc ghép 2 bảng checkinout và xác nhận

			DS_CIO_V.Clear();

			if (arrRows.Length != 0) {

				for (int i = 0, j = 1; i < arrRows.Length; ) { //i là index rowInn, j là index row out

					DataRow rowInn = arrRows[i];
					if (j >= arrRows.Length) break; // ko có phần tử kế tiếp để xét
					DataRow rowOut = arrRows[j];
					int IDinn = (int)rowInn["ID"];
					int IDout = (int)rowOut["ID"];
					string SourceInn = (string)rowInn["Source"];
					string SourceOut = (string)rowOut["Source"];
					int MachineNoInn = (int)rowInn["MachineNo"];
					int MachineNoOut = (int)rowOut["MachineNo"];
					DateTime timeInn = (DateTime)rowInn["TimeStr"];
					DateTime timeOut = (DateTime)rowOut["TimeStr"];
					if (IDinn != IDout)// bị mất cặp -> bỏ qua đến cặp kế tiếp, phải tăng i, j vì i+1=j ==> j+1
					{
						i = i + 1;
						j = j + 1;
						continue;
					}
					// cùng id --> xét có gần nhau ko
					if (MachineNoInn % 2 == MachineNoOut % 2) {
						j = j + 1;
						continue;
					}
					// đủ điều kiện ghép  cặp
					#region lấy thông tin
					int shiftID = (int)rowInn["ShiftID"];
					string shiftCode = rowInn["ShiftCode"].ToString();
					int dayCount = (int)rowInn["DayCount"];
					Single workingday = (Single)rowInn["Workingday"];
					TimeSpan onnDutyTs, offDutyTs;
					TimeSpan.TryParse(rowInn["Onduty"].ToString(), out onnDutyTs);
					TimeSpan.TryParse(rowInn["Offduty"].ToString(), out offDutyTs);

					TimeSpan afterOTTs = new TimeSpan(0, (int)rowInn["AfterOT"], 0);
					TimeSpan lateGraceTSS = new TimeSpan(0, (int)rowInn["LateGrace"], 0);
					TimeSpan earlyGraceTS = new TimeSpan(0, (int)rowInn["EarlyGrace"], 0);

					lateGraceTSS = onnDutyTs.Add(lateGraceTSS);
					// add thêm 1 ngày daycount nếu có
					offDutyTs = offDutyTs.Add(new TimeSpan(dayCount, 0, 0, 0));
					earlyGraceTS = offDutyTs.Subtract(earlyGraceTS);
					afterOTTs = offDutyTs.Add(afterOTTs);
					int pOTMin = (int)rowInn["OTMin"];
					bool tempTinhPC150 = false;
					if (rowInn["TinhPC150"] == DBNull.Value) tempTinhPC150 = false;
					else tempTinhPC150 = (bool)rowInn["TinhPC150"];

					int wkt = int.Parse(rowInn["WorkingTime"].ToString());
					TimeSpan pWorkingTime = new TimeSpan(0, wkt, 0);

					#endregion

					cChkInn_V chkInnV = new cChkInn_V() { GioLienQuan = null, ID = IDinn, MachineNo = MachineNoInn, Source = SourceInn, TimeStr = timeInn };
					cChkOut_V chkOutV = new cChkOut_V() { GioLienQuan = null, ID = IDout, MachineNo = MachineNoOut, Source = SourceOut, TimeStr = timeOut };
					cChkInOut_V chkInOutV = new cChkInOut_V() {
						Vao = chkInnV,
						Raa = chkOutV,
						TG = new ThoiGian(),
						HaveINOUT = 1,
						TongGioThuc = chkOutV.TimeStr - chkInnV.TimeStr,
						TimeStrDaiDien = chkInnV.TimeStr,
						TinhPC150 = tempTinhPC150,
						LamThem = new TimeSpan(0, 0, pOTMin, 0),
						ThuocNgayCong = chkInnV.TimeStr.Date,
					}; // [TBD] xem lại thuộc ngày công

					cShift tmpThuocCa;
					if (shiftID > 0) {
						tmpThuocCa = DSCa.Find(item => item.ShiftID == shiftID);
					}
					else if (shiftID > int.MinValue + 100 && shiftID < 0) // ca tách và ca kết hợp  [Chú ý] + 100 vì chừa khoảng này cho các loại khác
						tmpThuocCa = new cShift() { ShiftID = shiftID, ShiftCode = shiftCode, OnnDutyTS = onnDutyTs, OffDutyTS = offDutyTs, chophepvaotreTS = lateGraceTSS, chopheprasomTS = earlyGraceTS, batdaulamthemTS = afterOTTs, WorkingTimeTS = pWorkingTime, Workingday = workingday, DayCount = dayCount, QuaDem = (dayCount == 1), LunchMinute = ThamSo._0gio, LoaiCa = 0 }; //[TBD]
					else if (shiftID < int.MinValue + 100) // ca tự do 8 tiếng
						tmpThuocCa = new cShift() { ShiftID = shiftID, ShiftCode = shiftCode, OnnDutyTS = onnDutyTs, OffDutyTS = offDutyTs, chophepvaotreTS = lateGraceTSS, chopheprasomTS = earlyGraceTS, batdaulamthemTS = afterOTTs, WorkingTimeTS = pWorkingTime, Workingday = workingday, DayCount = dayCount, QuaDem = (dayCount == 1), LunchMinute = ThamSo._0gio, LoaiCa = 1 };
					else {
						tmpThuocCa = new cShift() { ShiftID = shiftID, ShiftCode = "XacNhan8h", OnnDutyTS = onnDutyTs, OffDutyTS = offDutyTs, chophepvaotreTS = lateGraceTSS, chopheprasomTS = earlyGraceTS, batdaulamthemTS = afterOTTs, WorkingTimeTS = pWorkingTime, Workingday = workingday, DayCount = dayCount, QuaDem = (dayCount == 1), LunchMinute = ThamSo._0gio, LoaiCa = 1 }; //[TBD]
					}
					if (tmpThuocCa == null) {
						log.Fatal("ERROR function Check_GioDaXN");
					}
					chkInOutV.ThuocCa = tmpThuocCa;
					chkInOutV.QuaDem = tmpThuocCa.QuaDem;
					XL.TinhCongTheoCa(chkInOutV, chkInOutV.ThuocCa);//[TBD]
					DS_CIO_V.Add(chkInOutV);

					// sau khi thực hiện xong thì tăng 
					i = i + 2;
					j = j + 2;
				}
			}
		}

		public List<cChkInOut_A> GhepCIO_A(List<cChk> DSchkInn, List<cChk> DSchkOut) {
			List<cChkInOut_A> kq = new List<cChkInOut_A>();
			try {
				int x1 = 0, x2 = 0;
				if (DSchkInn.Count == 0 && DSchkOut.Count != 0) {
					while (x2 < DSchkOut.Count) {
						kq.Add(new cChkInOut_A() { Vao = null, Raa = DSchkOut[x2], TG = new ThoiGian(), HaveINOUT = -2, TongGioThuc = ThamSo._0gio, ThuocNgayCong = DSchkOut[x2].TimeStr.Date, TimeStrDaiDien = DSchkOut[x2].TimeStr, });
						x2++;
					}
				}
				else if (DSchkInn.Count != 0 && DSchkOut.Count == 0) {
					while (x1 < DSchkInn.Count) {
						kq.Add(new cChkInOut_A() { Vao = DSchkInn[x1], Raa = null, TG = new ThoiGian(), HaveINOUT = -1, TongGioThuc = ThamSo._0gio, ThuocNgayCong = DSchkInn[x1].TimeStr.Date, TimeStrDaiDien = DSchkInn[x1].TimeStr, });
						x1++;
					}
				}
				else if (DSchkInn.Count != 0 && DSchkOut.Count != 0) {
					while (x1 < DSchkInn.Count && x2 < DSchkOut.Count) {
						cChk chkinn = DSchkInn[x1];
						cChk chkout = DSchkOut[x2];
						DateTime timeInn = chkinn.TimeStr;
						DateTime timeOut = chkout.TimeStr;
						if (timeOut <= timeInn)//ra ko vào
                        {
							cChkInOut_A CIO = new cChkInOut_A { Vao = null, Raa = DSchkOut[x2], TG = new ThoiGian(), HaveINOUT = -2, TongGioThuc = ThamSo._0gio, ThuocNgayCong = DSchkOut[x2].TimeStr.Date, TimeStrDaiDien = DSchkOut[x2].TimeStr };
							// lưu prev
							kq.Add(CIO);
							x2++;
						}
						else {
							TimeSpan duration = timeOut - timeInn;
							if (duration <= ThamSo._30phut) {
								kq.Add(new cChkInOut_A() { Vao = DSchkInn[x1], Raa = null, TG = new ThoiGian(), HaveINOUT = -1, TongGioThuc = ThamSo._0gio, ThuocNgayCong = DSchkInn[x1].TimeStr.Date, TimeStrDaiDien = DSchkInn[x1].TimeStr, });
								kq.Add(new cChkInOut_A() { Vao = null, Raa = DSchkOut[x2], TG = new ThoiGian(), HaveINOUT = -2, TongGioThuc = ThamSo._0gio, ThuocNgayCong = DSchkOut[x2].TimeStr.Date, TimeStrDaiDien = DSchkOut[x2].TimeStr, });
								x1++;
								x2++;
							}
							else if (duration > ThamSo._21h45) {
								cChkInOut_A CIO = new cChkInOut_A() { Vao = DSchkInn[x1], Raa = null, TG = new ThoiGian(), HaveINOUT = -1, TongGioThuc = ThamSo._0gio, ThuocNgayCong = DSchkInn[x1].TimeStr.Date, TimeStrDaiDien = DSchkInn[x1].TimeStr };
								kq.Add(CIO);
								x1++;
							}
							else {
								kq.Add(new cChkInOut_A() { Vao = DSchkInn[x1], Raa = DSchkOut[x2], TG = new ThoiGian(), HaveINOUT = 1, TongGioThuc = duration, ThuocNgayCong = ThamSo.GetDate(DSchkInn[x1].TimeStr), TimeStrDaiDien = DSchkInn[x1].TimeStr });
								x1++;
								x2++;
							}
						}
					}
					if (x2 < DSchkOut.Count) {
						while (x2 < DSchkOut.Count) {
							cChkInOut_A CIO = new cChkInOut_A() { Vao = null, Raa = DSchkOut[x2], TG = new ThoiGian(), HaveINOUT = -2, TongGioThuc = ThamSo._0gio, ThuocNgayCong = DSchkOut[x2].TimeStr.Date, TimeStrDaiDien = DSchkOut[x2].TimeStr };
							kq.Add(CIO);
							x2++;
						}
					}
					else if (x1 < DSchkInn.Count) {
						while (x1 < DSchkInn.Count) {
							cChkInOut_A CIO = new cChkInOut_A() { Vao = DSchkInn[x1], Raa = null, TG = new ThoiGian(), HaveINOUT = -1, TongGioThuc = ThamSo._0gio, ThuocNgayCong = DSchkInn[x1].TimeStr.Date, TimeStrDaiDien = DSchkInn[x1].TimeStr };
							kq.Add(CIO);
							x1++;
						}
					}

				}

			} catch (Exception ex) {
				log4net.Config.XmlConfigurator.Configure();
				ILog lg = LogManager.GetLogger("GhepCaCIO_A");
				lg.Error(null, ex);
				throw ex;
			}

			return kq;

		}

		public void AddNewCheck_A(cChk check) {
			if (check.GetType() == typeof(cChkInn_A)) {
				ds_CheckInn_A.Add(check);
			}
			else if (check.GetType() == typeof(cChkOut_A)) {
				ds_CheckOut_A.Add(check);
			}
		}

		public List<cChkInOut> TronDS_CIO_A_V(List<cChkInOut_A> dsCIO_A, List<cChkInOut_V> dsCIO_V) {
			List<cChkInOut> kq = new List<cChkInOut>();
			kq.AddRange(dsCIO_A);
			kq.AddRange(dsCIO_V);
			kq.Sort(new cChkInOutComparer());
			return kq;
		}

		public cShift KiemtraThuocKhoangHieuCa(cChkInOut_A CIO, List<cShift> dsCa) {
			DateTime t_vao = CIO.Vao.TimeStr;
			DateTime t_raa = CIO.Raa.TimeStr;
			return dsCa.FirstOrDefault(ca => t_vao >= t_vao.Date.Add(ca.OnTimeInTS) && t_vao <= t_vao.Date.Add(ca.CutInTS)
									&& t_raa >= t_vao.Date.Add(ca.OnTimeOutTS) && t_raa <= t_vao.Date.Add(ca.CutOutTS));
		}
		public List<cShift> KiemtraThuocKhoangHieuVaoCa(cChkInOut_A CIO, List<cShift> dsCa) {
			DateTime t_vao = CIO.Vao.TimeStr;
			return dsCa.FindAll(ca => t_vao >= t_vao.Date.Add(ca.OnTimeInTS) && t_vao <= t_vao.Date.Add(ca.CutInTS));
		}
		public List<cShift> KiemtraThuocKhoangHieuRaaCa(cChkInOut_A CIO, List<cShift> dsCa) {
			TimeSpan t_raa = CIO.Raa.TimeStr.TimeOfDay;
			return dsCa.FindAll(ca => ((t_raa >= ca.OnTimeOutTS.Subtract(new TimeSpan(ca.DayCount, 0, 0, 0))) && (t_raa <= ca.CutOutTS.Subtract(new TimeSpan(ca.DayCount, 0, 0, 0)))));
		}
		public void XetCa(List<cChkInOut_A> dsCIO, List<cShift> dsca, bool macdinhtinhPC150) {
			int i = 0;
			while (i < dsCIO.Count) {
				cChkInOut_A CIO = dsCIO[i];
				if (CIO.HaveINOUT < 0) {
					if (CIO.HaveINOUT == -1) {
						//CIO.kiem
					}
					else {
					}
					i++;
					continue;
				}
				cShift ca = KiemtraThuocKhoangHieuCa(CIO, dsca);
				if (ca != null) {
					if (ca.Workingday == 2f && ca.OnnDutyTS > ThamSo._20h00) {
						List<cChk> gioLQvaoca3 = (CIO.Vao.GioLienQuan != null)
													 ? new List<cChk>(CIO.Vao.GioLienQuan)
													 : new List<cChk>();
						List<cChk> gioLQraaca1 = (CIO.Raa.GioLienQuan != null)
													? new List<cChk>(CIO.Raa.GioLienQuan)
													 : new List<cChk>();

						cChk raaca3 = new cChkOut_A() { GioLienQuan = null, MachineNo = 22, Source = "PC", TimeStr = CIO.Vao.TimeStr.Date.Add(ca.OnnDutyTS).Add(ThamSo._8gio) };
						cChk vaoca1 = new cChkInn_A() { GioLienQuan = null, MachineNo = 21, Source = "PC", TimeStr = CIO.Vao.TimeStr.Date.Add(ca.OnnDutyTS).Add(ThamSo._8gio1giay) };
						cChk vaoca3 = new cChkInn_A() { GioLienQuan = gioLQvaoca3, MachineNo = CIO.Vao.MachineNo, Source = CIO.Vao.Source, TimeStr = CIO.Vao.TimeStr };
						cChk raaca1 = new cChkInn_A() { GioLienQuan = gioLQraaca1, MachineNo = CIO.Raa.MachineNo, Source = CIO.Raa.Source, TimeStr = CIO.Raa.TimeStr };
						dsCIO[i] = new cChkInOut_A() { TinhPC150 = macdinhtinhPC150, Vao = vaoca3, Raa = raaca3, TG = new ThoiGian(), HaveINOUT = 1, TongGioThuc = raaca3.TimeStr - vaoca3.TimeStr, ThuocNgayCong = vaoca3.TimeStr.Date, TimeStrDaiDien = vaoca3.TimeStr };
						cShift ca3 = dsca.Find(o => o.OnnDutyTS > ThamSo._20h00 && o.Workingday == 1f);
						dsCIO[i].ThuocCa = ca3;
						dsCIO[i].QuaDem = true;

						cChkInOut_A newCIO = new cChkInOut_A() { TinhPC150 = macdinhtinhPC150, Vao = vaoca1, Raa = raaca1, TG = new ThoiGian(), HaveINOUT = 1, TongGioThuc = raaca1.TimeStr - vaoca1.TimeStr, ThuocNgayCong = vaoca1.TimeStr.Date, TimeStrDaiDien = vaoca1.TimeStr };
						cShift ca1 = dsca.Find(o => o.OnnDutyTS <= ThamSo._05h45 && o.Workingday == 1f);
						newCIO.ThuocCa = ca1;
						newCIO.QuaDem = false;

						// vì hàm insert ko cho phép chèn ở vị trí > số lượng phần tử
						// => nên nếu i là phần tử cuối thì add vào cuối danh sách, ngược lại thì insert vào vị trí i+1
						if (i == (dsCIO.Count - 1)) dsCIO.Add(newCIO);
						else dsCIO.Insert(i + 1, newCIO);
						i = i + 2; // +2 vì i là ca3, i+1 là ca 1
					}
					else {
						CIO.TinhPC150 = macdinhtinhPC150;
						CIO.ThuocCa = ca;
						i++;
					}
				}
				else {
					ca = new cShift() { ShiftID = int.MinValue, ShiftCode = "Ca 8 tiếng" };
					TaoCaTuDo(ca, CIO.Vao.TimeStr, ThamSo._8gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1f);
					CIO.TinhPC150 = macdinhtinhPC150;
					CIO.ThuocCa = ca;
					i++;
				}
			}


		}

		public static void TaoCaTuDo(cShift pCa, DateTime pCheckInTime, TimeSpan pWorkingTime, TimeSpan pchophepvaotreTS, TimeSpan pchopheprasomTS, TimeSpan pbatdaulamthemTS, float pWorkingDay) {
			pCa.OnnDutyTS = pCheckInTime.TimeOfDay;
			pCa.OffDutyTS = pCheckInTime.TimeOfDay + pWorkingTime;
			pCa.LateGraceTS = pchophepvaotreTS;
			pCa.EarlyGraceTS = pchopheprasomTS;
			pCa.AfterOTTS = pbatdaulamthemTS;
			pCa.chophepvaotreTS = pCa.OnnDutyTS.Add(pchophepvaotreTS);
			pCa.chopheprasomTS = pCa.OffDutyTS.Subtract(pchopheprasomTS);
			pCa.batdaulamthemTS = pCa.OffDutyTS.Add(pbatdaulamthemTS);
			pCa.DayCount = pCa.OffDutyTS.Days;
			pCa.QuaDem = (pCa.OffDutyTS.Days == 1);
			pCa.WorkingTimeTS = pWorkingTime;
			pCa.Workingday = pWorkingDay;
			pCa.LunchMinute = ThamSo._0gio;
		}




		public List<cNgayCong> TinhCongTheoNgay(List<cChkInOut> pDSVaoRa, DateTime ngayBD, DateTime ngayKT, bool macdinhtinhpc50) {

			List<cNgayCong> kq = new List<cNgayCong>();
			DateTime ngaydem;
			if (pDSVaoRa.Count == 0) {
				ngaydem = ngayBD.Date;
				while (ngaydem <= ngayKT) { // <= vì lấy luôn ngày KT : vắng mặt
					cNgayCong ngayKOcheck = new cNgayCong() { NgayCong = ngaydem, HasCheck = false, DSVaoRa = new List<cChkInOut>(), };
					kq.Add(ngayKOcheck);
					ngaydem = ngaydem.AddDays(1d);
				}
				return kq;
			}

			int vtriBD = 0;
			ngaydem = ngayBD.Date;

			while (ngaydem <= ngayKT.Date) {

				if (vtriBD >= pDSVaoRa.Count) { // hết DS nhưng ngaydem <= ngày KT ==> ghi lại những ngày sau là vắng mặt
					while (ngaydem <= ngayKT.Date) {
						cNgayCong ngayKOcheck = new cNgayCong() { NgayCong = ngaydem, HasCheck = false, DSVaoRa = new List<cChkInOut>(), };
						kq.Add(ngayKOcheck);
						ngaydem = ngaydem.AddDays(1d);
					}
					continue;
				}

				// chưa hết DS, bắt đầu từ ngày của DSVaoRa tại vtbd, đó là ngày có mặt
				cChkInOut CIO = pDSVaoRa[vtriBD];
				XL.TinhCongTheoCa(CIO, CIO.ThuocCa);
				DateTime ngayCoMat = CIO.ThuocNgayCong;

				// ghi lại những ngày vắng mặt trước ngày có mặt
				while (ngaydem < ngayCoMat) { // ko có = vì chỉ chạy đến trước ngày của tmpVaoRa thôi
					cNgayCong ngayKOcheck = new cNgayCong() { NgayCong = ngaydem, HasCheck = false, DSVaoRa = new List<cChkInOut>(), };
					kq.Add(ngayKOcheck);
					ngaydem = ngaydem.AddDays(1d);
				}
				//ngaydem = ngày có mặt. stop, tmpVaoRa là vào ra đầu tiên trong ngày
				#region collapse

				double pcdem = Convert.ToDouble(Properties.Settings.Default.PCDem / 100);
				double pctc = Convert.ToDouble(Properties.Settings.Default.PCTangCuong / 100);
				cNgayCong ngayCOcheck = new cNgayCong() {
					HasCheck = true,
					NgayCong = ngayCoMat,
					TinhPC150 = macdinhtinhpc50,// mặc định khởi tạo ngày có tính PC 50%, nếu tồn tại ít nhất 1 ca nào ko tính pc 50% thì set cả ngày ko tính pc 50%
					TG = new ThoiGian() { LamTinhCong = CIO.TG.LamTinhCong, LamTinhPC30 = CIO.TG.LamTinhPC30 },
					TongGioThuc = CIO.TongGioThuc,
					TongTre = CIO.VaoTre,
					TongSom = CIO.RaaSom,
					TongCong = Math.Round(CIO.Cong, 2),
					PhuCap30 = Math.Round(((CIO.TG.LamTinhPC30.TotalHours > 0d)
					? ((CIO.TG.LamTinhPC30.TotalHours / 8d) * pcdem)
					: 0d), 2)
					// công thức cũ (tính phụ cấp tăng ca theo công) là : + (((tmpVaoRa.Cong > 1f) ? (tmpVaoRa.Cong - 1f) * 0.5f : 0f))
					// đổi lại công thức mới (tính phụ cấp theo giờ) là : + (((tmpVaoRa.TGLamTinhCong.TotalHours > 8f) ? (((float)tmpVaoRa.TGLamTinhCong.TotalHours - 8f) / 8f) * 0.5f : 0f))
				};

				if (CIO.QuaDem == true) ngayCOcheck.QuaDem = true; // set qua đêm nếu có
				if (CIO.TinhPC150 != macdinhtinhpc50) ngayCOcheck.TinhPC150 = CIO.TinhPC150;
				if (ngayCOcheck.TinhPC150) {
					ngayCOcheck.PhuCap50 = Math.Round((((CIO.TG.LamTinhCong.TotalHours > 8d)
														   ? (((CIO.TG.LamTinhCong.TotalHours - 8d) / 8d) * pctc)
														   : 0d)), 2);
				}
				else ngayCOcheck.PhuCap50 = 0d;

				ngayCOcheck.TongPhuCap = Math.Round((ngayCOcheck.PhuCap30 + ngayCOcheck.PhuCap50), 2);
				ngayCOcheck.them(CIO);
				#endregion


				// sau khi tạo ngày công mới vào ra đó là vào ra đầu tiên thì chuyển sang VAORA next
				vtriBD++;
				// nếu hết ds thì ngưng, add tmpNgayCong1 vào danh sách ngày công (do chưa add)
				if (vtriBD >= pDSVaoRa.Count) {
					kq.Add(ngayCOcheck);
					ngaydem = ngayCOcheck.NgayCong.AddDays(1d);
					while (ngaydem <= ngayKT.Date) {
						cNgayCong ngayKOcheck = new cNgayCong() { NgayCong = ngaydem, HasCheck = false, DSVaoRa = new List<cChkInOut>(), };
						kq.Add(ngayKOcheck);
						ngaydem = ngaydem.AddDays(1.0d);
					}
					continue;
				}
				// chưa hết DSVàoRa, xem các vào ra kế tiếp, nếu cùng ngày công thì add thêm vào tmpNgayCong1
				while (vtriBD < pDSVaoRa.Count && ThamSo.GetDate(pDSVaoRa[vtriBD].TimeStrDaiDien) == ngayCOcheck.NgayCong) {
					cChkInOut CIO_1 = pDSVaoRa[vtriBD];
					XL.TinhCongTheoCa(CIO_1, CIO_1.ThuocCa);

					#region collapse
					ngayCOcheck.TongGioThuc += CIO_1.TongGioThuc;
					ngayCOcheck.TG.LamTinhCong += CIO_1.TG.LamTinhCong;
					ngayCOcheck.TG.LamTinhPC30 += CIO_1.TG.LamTinhPC30;
					ngayCOcheck.TongTre += CIO_1.VaoTre;
					ngayCOcheck.TongSom += CIO_1.RaaSom;
					ngayCOcheck.TongCong += Math.Round(CIO_1.Cong, 2);

					ngayCOcheck.PhuCap30 = Math.Round(((ngayCOcheck.TG.LamTinhPC30.TotalHours > 0d)
												   ? ((ngayCOcheck.TG.LamTinhPC30.TotalHours / 8d) * pcdem)
												   : 0d), 2);
					#endregion
					if (CIO_1.QuaDem == true) ngayCOcheck.QuaDem = true; // set qua đêm nếu có

					if (CIO_1.TinhPC150 != macdinhtinhpc50) ngayCOcheck.TinhPC150 = CIO_1.TinhPC150;
					if (ngayCOcheck.TinhPC150) {
						ngayCOcheck.PhuCap50 = Math.Round((((ngayCOcheck.TG.LamTinhCong.TotalHours > 8d)
															   ? (((ngayCOcheck.TG.LamTinhCong.TotalHours - 8d) / 8d) * pctc)
															   : 0d)), 2);
					}
					else ngayCOcheck.PhuCap50 = 0d;

					ngayCOcheck.TongPhuCap = Math.Round((ngayCOcheck.PhuCap30 + ngayCOcheck.PhuCap50), 2);
					ngayCOcheck.them(CIO_1);

					vtriBD++;
				}
				//thoát khỏi vòng lặp: hoặc hết ds hoặc chuyển sang ngày mới
				if (vtriBD >= pDSVaoRa.Count) {
					kq.Add(ngayCOcheck);
					ngaydem = ngayCOcheck.NgayCong.AddDays(1d);
					while (ngaydem <= ngayKT.Date) {
						cNgayCong tmpNgayCong = new cNgayCong() { NgayCong = ngaydem, HasCheck = false, DSVaoRa = new List<cChkInOut>(), };
						kq.Add(tmpNgayCong);
						ngaydem = ngaydem.AddDays(1d);
					}
				}
				else {
					kq.Add(ngayCOcheck);
					ngaydem = ngaydem.AddDays(1d);
				}
			}

			return kq;
		}

		public void ClearAll() {
			ds_Check_A.Clear();

			DSVaoRa.Clear();
			DSNgayCong.Clear();

		}

		public void ArrayRowsToDSVang(DataRow[] arrRow) {
			if (arrRow.Length == 0) return;
			List<cLoaiVang> dsvang = new List<cLoaiVang>();
			foreach (DataRow row in arrRow) {
				int iuen = (int)row["UserEnrollNumber"];
				DateTime TimeDate = (DateTime)row["TimeDate"];
				string absentCode = (string)row["AbsentCode"];
				string absentsymbol = (string)row["AbsentSymbol"];
				string absentDesc = (string)row["AbsentDescription"];
				Single wkdayy = (Single)row["Workingday"];
				Single wktime = (Single)row["WorkingTime"];
				cLoaiVang loaiVang = new cLoaiVang() { KyHieu = absentsymbol, Cong = wkdayy, MaLV = absentCode, MoTa = absentDesc, Ngay = TimeDate };
				// xác định ngày vắng, nếu ko tìm thấy thì continue, nếu tìm thấy thì add
				cNgayCong ngayCong = DSNgayCong.Find(o => o.NgayCong == loaiVang.Ngay);
				if (ngayCong == null) continue;// ko tìm thấy
				// tìm thấy: add
				if (ngayCong.DSVang == null) ngayCong.DSVang = new List<cLoaiVang>();
				ngayCong.DSVang.Add(loaiVang);
			}
		}

		public void ArrayRowsToDSChamCongTay(DataRow[] arrRow) {
			if (arrRow.Length == 0) return;
			object[,] ds = new object[arrRow.Length, 4];
			int i1 = 0, i2 = 0;
			foreach (DataRow row in arrRow) {
				ds[i2, 0] = row["UserEnrollNumber"];
				ds[i2, 1] = row["Ngay"];
				ds[i2, 2] = row["Cong"];
				ds[i2, 3] = row["PhuCap"];
				i2++;
			}
			for (i1 = 0; i1 < DSNgayCong.Count; i1++) {
				cNgayCong ngayCong = DSNgayCong[i1];
				if ((DateTime)ds[i2, 1] == ngayCong.NgayCong) {
					ngayCong.TongCong += (Single)ds[i2, 2];
					ngayCong.TongPhuCap += (Single)ds[i2, 3];
					i2++;
					if (i2 >= arrRow.Length) break;
				}
			}

		}

		public void ArrayRowsToDSLamViecNgayNghi(DataRow[] arrRow_DSLamViecNgayNghi) {
			if (arrRow_DSLamViecNgayNghi.Length == 0) return;
			foreach (DataRow row in arrRow_DSLamViecNgayNghi) {
				DateTime ngaynghi = (DateTime)row["Ngay"];
				Single PCThem = (Single)row["PCThem"];
				Single PCDem = (Single)row["PCDem"];
				cNgayCong ngayCong = DSNgayCong.Find(o => o.NgayCong.Date == ngaynghi);
				ngayCong.PhuCap50 = ngayCong.TongCong * PCThem + ngayCong.PhuCap30 * PCDem;
				ngayCong.TongPhuCap = ngayCong.PhuCap30 + ngayCong.PhuCap50;
			}
		}

		internal void KBNgayLe(DataTable tableNgayLe) {
			// duyệt từng ngày lễ nếu có, 1. thêm vào danh sách loại vắng  2. tính công nếu trưởng phó thì ko tính pc lễ, 3. nếu  bộ phận khác thì tính double
			foreach (DataRow row in tableNgayLe.Rows) {
				DateTime ngayle = (DateTime)row["HDate"];
				string mota = row["Holiday"].ToString();
				// xác định ngày công nào là ngày lễ
				cNgayCong ngayCong = DSNgayCong.Find(o => o.NgayCong.Date == ngayle);
				//thêm vào danh sách vắng
				if (ngayCong.DSVang == null) ngayCong.DSVang = new List<cLoaiVang>();
				cLoaiVang loaiVang = new cLoaiVang { Cong = 1f, KyHieu = "L", MaLV = "L", MoTa = mota, Ngay = ngayle };
				ngayCong.DSVang.Add(loaiVang);

			}
		}

		public override string ToString() {
			return " UEN=" + UserEnrollNumber + "; Ten=" + UserFullName + "__\n";
		}



		// [BackupFunction06]

		public cUserInfo() {
		}


		public Double TongCongThang;

		public int TongNgayQuaDem;
		public Double TongCongLe;
		public Double TongCongPhep;
		public Double TongCongCV;

		public Double TongCongBH;

		public Double TongCongH_CT_PT;

		public Double TongCongRo;

		public Double TongPCapThang;

		public Double TienLuong;

		public cLuongThang Luong = new cLuongThang();

		public struct NgayCongChuan {
			public Double ChamCong;
			public Double NgayCong;
			public Double Phep;
			public Double H_PT_Le;
			public Double QuaDem;
			public Double ChoViec;
		}

	}

	public class cLuongThang {
		//-----lương cb, sp theo công, phụ cấp
		public Double LuongCB_TheoCong;
		public Double LuongCB_TheoPCap;
		public Double TongLuongCB_TheoCong;
		public Double TongLuongCB_TheoPCap;
		public Double LuongSP_TheoCong;
		public Double LuongSP_TheoPCap;
		public Double TongLuong_TheoCong;
		public Double TongLuong_TheoPCap;

		public Double SP_LamRa_TheoCong;
		public Double SP_LamRa_TheoPCap;
		public Double LuongThangTruoc;
		public Double BoiDuongQuaDem;
		//------khấu trừ
		public Double TamUng;
		public Double KhauTruBH;
		public Double ThuChiKhac;
		public Double TongLuong;
		public Double ThucLanh;

	}
}
