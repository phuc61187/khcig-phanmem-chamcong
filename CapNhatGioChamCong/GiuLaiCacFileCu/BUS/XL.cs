using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiuLaiCacFileCu.DTO;
using GiuLaiCacFileCu.DAO;
using log4net;

namespace GiuLaiCacFileCu.BUS {
	public static class XL {
		private static readonly ILog log = LogManager.GetLogger("XL");

		static string SelStr_GetDSCheckCacNV(object[] ds) {
			string selectQueryString = @" SELECT distinct CheckInOut.UserEnrollNumber,TimeStr,MachineNo,Source,UserInfo.UserFullName,
                                                          IDXacNhanCaVaLamThem, ShiftID , ShiftCode , Onduty , Offduty , LateGrace, EarlyGrace, AfterOT, TinhPC150, DayCount , WorkingTime , Workingday, 
                                                          TimeStrIn , TimeStrOut , OTMin , Note ";
			selectQueryString += @" from UserInfo , CheckInOut left join XacNhanCaVaLamThem on IDXacNhanCaVaLamThem = ID";
			selectQueryString += @" where  ";
			selectQueryString += @"     (       ( (TimeStr between @BatDauVao and @KetThucVao) and MachineNo % 2 = 1)
                                        or      ( (TimeStr between @BatDauRa  and @KetThucRa ) and MachineNo % 2 = 0)       )
                                        and (UserInfo.UserEnrollNumber = CheckInOut.UserEnrollNumber) ";
			selectQueryString += @"     and (   ";
			selectQueryString += @"             CheckInOut.UserEnrollNumber = {0} ";
			selectQueryString = String.Format(selectQueryString, String.Join(" or CheckInOut.UserEnrollNumber = ", ds));
			selectQueryString += @"         ) ";
			selectQueryString += @" group by CheckInOut.UserEnrollNumber,TimeStr,MachineNo,Source,UserInfo.UserFullName,
                                             IDXacNhanCaVaLamThem, ShiftID  , Onduty , Offduty , LateGrace, EarlyGrace, AfterOT, TinhPC150, DayCount , WorkingTime , Workingday, 
                                             TimeStrIn , TimeStrOut , OTMin , Note, ShiftCode ";
			selectQueryString += " order by CheckInOut.UserEnrollNumber asc, TimeStr asc";
			return selectQueryString;
		}

		/// <summary>
		/// throw exception
		/// </summary>
		/// <param name="pDSNV"></param>
		/// <param name="pNgayBD"></param>
		/// <param name="pNgayKT"></param>
		public static void XemCong(List<cUserInfo> pDSNV, DateTime pNgayBD, DateTime pNgayKT) {
			if (pDSNV == null || pDSNV.Count == 0) return;

			object[] ds = new object[pDSNV.Count];
			int i = 0;
			foreach (cUserInfo nhanvien in pDSNV) {
				ds[i] = nhanvien.UserEnrollNumber;
				nhanvien.ClearAll();
				i++;
			}
			//1. check giờ chưa xử lý : input: nhân viên, output: nhân viên có lst(đã lọc giờ gần trong 30ph)
			try {
				DataTable table = SqlDataAccessHelper.ExecuteQueryString(SelStr_GetDSCheckCacNV(ds),
					new string[] { "@BatDauVao", "@KetThucVao", "@BatDauRa", "@KetThucRa" },
					new object[] { pNgayBD, pNgayKT, pNgayBD, pNgayKT });

				DataTable tableNgayVang = XL.LietKeNgayVangChoNV(ds, pNgayBD, pNgayKT);
				DataTable tableChamCongTay = XL.LietKeChamCongTay(ds, pNgayBD, pNgayKT);
				DataTable tableLamViecNgayNghi = XL.LietKeLamViecNgayNghiDaKhaiBao(ds, pNgayBD, pNgayKT);

				foreach (cUserInfo nhanvien in pDSNV) {
					int iUserEnrollNumber = nhanvien.UserEnrollNumber;
					DataRow[] subTable = table.Select("UserEnrollNumber = " + iUserEnrollNumber, "UserEnrollNumber asc");
					DataRow[] subTableNgayNghi = tableNgayVang.Select("UserEnrollNumber = " + iUserEnrollNumber, "TimeDate asc");
					DataRow[] subTableNgayChamCongTay = tableChamCongTay.Select("UserEnrollNumber = " + iUserEnrollNumber);
					DataRow[] subTableLamViecNgayNghi = tableLamViecNgayNghi.Select("UserEnrollNumber = " + iUserEnrollNumber);

					TableCIO_To_DSCheck(subTable, nhanvien, pNgayBD, pNgayKT);

					LayDSCheckDaiDien(nhanvien.ds_CheckAuto);
					XetCaCheckAuto(nhanvien.ds_CheckAuto, nhanvien.DSCa, nhanvien.DSVaoRa, nhanvien.MacDinhTinhPC150);
					nhanvien.DSVaoRa.Sort(new cChkInOutComparer());
					TinhCong(nhanvien.DSVaoRa);
					DuaVaoNgayCong(nhanvien.DSVaoRa, pNgayBD.Date, pNgayKT.Date, nhanvien.DSNgayCong);
					if (subTableNgayNghi.Length != 0) GhiCongVang(nhanvien.DSNgayCong, subTableNgayNghi);
					if (subTableNgayChamCongTay.Length != 0) GhiCongChamTay(nhanvien.DSNgayCong, subTableNgayChamCongTay);
					if (subTableLamViecNgayNghi.Length != 0) GhiCongLamViecNgayNghi(nhanvien.DSNgayCong, subTableLamViecNgayNghi);
					//if ()
				}
			} catch (Exception exception) {
				log.Fatal(exception.ToString());
				throw;
			}

		}

		private static void GhiCongLamViecNgayNghi(List<cNgayCong> list, DataRow[] subTableLamViecNgayNghi) {
			foreach (var row in subTableLamViecNgayNghi) {
				DateTime ngay = (DateTime)row["Ngay"];
				float HeSoPhucap = (float)row["HeSoPC"];
				cNgayCong ngayCong = list.Find(o => o.NgayCong.Date == ngay);
				ngayCong.PhuCapDem = HeSoPhucap * (((float)(ngayCong.TongGioLamDem.TotalHours / 8f)) * 0.3f);
				ngayCong.PhuCapThem = HeSoPhucap * ngayCong.TongCong;
				ngayCong.TongPhuCap = ngayCong.PhuCapDem + ngayCong.PhuCapThem; // reset lại từ đầu = 0
			}
		}

		private static void GhiCongChamTay(List<cNgayCong> DSNgayCong, DataRow[] subTableNgayChamCongTay) {
			foreach (var row in subTableNgayChamCongTay) {
				DateTime ngay = (DateTime)row["Ngay"];
				float Cong = (float)row["Cong"];
				float Phucap = (float)row["PhuCap"];
				cNgayCong ngayCong = DSNgayCong.Find(o => o.NgayCong.Date == (DateTime)row["Ngay"]);
				ngayCong.TongCong += Cong;
				ngayCong.TongPhuCap += Phucap;
			}
		}

		private static DataTable LietKeChamCongTay(object[] arrDSNVCheck, DateTime pNgayBD, DateTime pNgayKT) {
			string query = ThamSo.SelStrLayDSChamCongTay();
			query = String.Format(query, String.Join(" or UserEnrollNumber = ", arrDSNVCheck));
			DataTable table = SqlDataAccessHelper.ExecuteQueryString(query, new[] { "@NgayBD", "@NgayKT" }, new object[] { pNgayBD, pNgayKT });
			return table;
		}

		private static void GhiCongVang(List<cNgayCong> DSNgayCong, DataRow[] arrRowNgayVang) {
			foreach (DataRow row in arrRowNgayVang) {
				cLoaiVang loaiVang = new cLoaiVang() {
					MaLV = row["AbsentCode"].ToString(),
					Cong = (float)row["Workingday"],
					MoTa = row["AbsentDescription"].ToString(),
					KyHieu = row["AbsentSymbol"].ToString(),
					Ngay = (DateTime)row["TimeDate"]
				};
				//HoanDoi(loaiVang);
				cNgayCong ngay = DSNgayCong.Find(o => o.NgayCong.Date == loaiVang.Ngay);
				switch (loaiVang.KyHieu) {
					case "P":
					case "NP":
					case "H":
					case "NH":
					case "CT":
					case "NCT":
					case "PT":
					case "NPT":
					case "L":
						ngay.TongCong += loaiVang.Cong;
						break;
				}
				if (ngay.DSVang == null) ngay.DSVang = new List<cLoaiVang>();
				ngay.DSVang.Add(loaiVang);
			}
		}

		public static void LayDSCheckDaiDien(List<cChk> DScheck) {
			if (DScheck == null) DScheck = new List<cChk>();
			int i = 0;
			while (i + 1 < DScheck.Count) {
				// nếu cùng loại CheckIN hoặc cùng loại CHECkOUT và trong vòng 30ph thì hấp thụ và loại khỏi DS
				cChk truoc = DScheck[i];
				cChk sau = DScheck[i + 1];
				if (truoc.GetType() == sau.GetType() && sau.TimeStr - truoc.TimeStr <= ThamSo._30phut) {
					if (truoc.GioLienQuan == null) truoc.GioLienQuan = new List<cChk>();
					truoc.GioLienQuan.Add(sau);
					DScheck.Remove(sau);
				}
				else i++;
			}
		}

		public static void XetCaCheckAuto(List<cChk> DSCheck_auto, List<cShift> pDSCa, List<cChkInOut> DSVaoRa, bool pMacDinhTinhPC150) {
			if (DSCheck_auto == null || DSCheck_auto.Count == 0) return;
			TimeSpan tongGioThuc = ThamSo._0gio;
			cChkInOut CIO_1, CIO_2;
			cChk lastChk = null;
			foreach (cChk curChk in DSCheck_auto) {
				if (lastChk == null) lastChk = curChk;
				else if (lastChk.GetType() == typeof(cChkIn)) {
					tongGioThuc = curChk.TimeStr - lastChk.TimeStr;
					if (curChk.GetType() == typeof(cChkOut)) {//in-out
						if (tongGioThuc <= ThamSo._21h45) {
							// trường hợp in-out: nếu thuộc ca: loại 0 (đúng quy định ca), nếu ko thuộc ca loại 3 (không đúng quy định nhưng vào ra trong ngày)
							cShift tmpThuocCa = XetThuocCa(lastChk, curChk, pDSCa);

							// trường hợp ca 3&1 tách, BUG [ko phải bug chỉ là chú ý] dkiện làm 2 ca có qua đêm sẽ làm trùng ca3&1 với ca 2&3 nên ở đây dùng điều kiện sau 20h
							if (tmpThuocCa != null && tmpThuocCa.Workingday == 2f && tmpThuocCa.OnnDutyTS > ThamSo._20h00) {
								CIO_1 = new cChkInOut() { Vao = lastChk, Raa = curChk, ThuocCa = tmpThuocCa, HaveINOUT = 1, TinhPC150 = pMacDinhTinhPC150 };
								cChkInOut[] tmpArr = TachGio2Ca3Va1(pDSCa, CIO_1, CIO_1.ThuocCa); // có update qua đêm = true cho ca 3 rồi
								tmpArr[0].TinhPC150 = pMacDinhTinhPC150;
								tmpArr[1].TinhPC150 = pMacDinhTinhPC150;
								DSVaoRa.Add(tmpArr[0]); DSVaoRa.Add(tmpArr[1]);
							}
							else if (tmpThuocCa != null) { // thuộc ca bình thường
								CIO_1 = new cChkInOut() { Vao = lastChk, Raa = curChk, TongGioThuc = tongGioThuc, ThuocCa = tmpThuocCa, HaveINOUT = 1, TinhPC150 = pMacDinhTinhPC150 };
								CIO_1.QuaDem = (tmpThuocCa.DayCount == 1) ? true : false;
								DSVaoRa.Add(CIO_1);
							}
							else { // ko thuộc ca nhưng vẫn đủ check in out
								//BUG [ko phải bug chỉ là chú ý] xem lại trường hợp nếu vào ra liền (vào 7h ra 7h 01ph) trong vòng 30ph có nên tách ra ko 
								//BUG => suggest là nên tách, còn các trường hợp khác > 30ph thì ko cần
								if (tongGioThuc < ThamSo._30phut) {
									CIO_1 = new cChkInOut() { Vao = lastChk, Raa = null, ThuocCa = null, HaveINOUT = -2 };
									CIO_2 = new cChkInOut() { Vao = null, Raa = curChk, ThuocCa = null, HaveINOUT = -1 };
									DSVaoRa.Add(CIO_1);
									DSVaoRa.Add(CIO_2);
								}
								else {
									cShift tmpCa_8tieng = new cShift() { ShiftID = int.MinValue, ShiftCode = "Ca 8 tiếng", LoaiCa = 1 };
									XL.TaoCaTuDo(tmpCa_8tieng, lastChk.TimeStr, ThamSo._8gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1f);
									CIO_1 = new cChkInOut() { Vao = lastChk, Raa = curChk, TongGioThuc = tongGioThuc, ThuocCa = tmpCa_8tieng, HaveINOUT = 1, TinhPC150 = pMacDinhTinhPC150 };
									CIO_1.QuaDem = (tmpCa_8tieng.QuaDem == true) ? true : false;
									DSVaoRa.Add(CIO_1);
								}
							}
							lastChk = null;
						}
						// trường hợp in-out trên 21 tiếng : do quên check nên vào ko ra và ra ko vào
						else if (tongGioThuc > ThamSo._21h45) {
							CIO_1 = new cChkInOut() { Vao = lastChk, Raa = null, ThuocCa = null, HaveINOUT = -2 };
							CIO_2 = new cChkInOut() { Vao = null, Raa = curChk, ThuocCa = null, HaveINOUT = -1 };
							DSVaoRa.Add(CIO_1);
							DSVaoRa.Add(CIO_2);
							lastChk = null;
						}
					}
					else { //in-in
						CIO_1 = new cChkInOut() { Vao = lastChk, Raa = null, ThuocCa = null, HaveINOUT = -2 };
						DSVaoRa.Add(CIO_1);
						lastChk = curChk;
					}
				}
				else if (lastChk.GetType() == typeof(cChkOut)) {//out-
					CIO_2 = new cChkInOut() { Vao = null, Raa = lastChk, ThuocCa = null, HaveINOUT = -1 };
					DSVaoRa.Add(CIO_2);
					lastChk = curChk;
				}
			}
			if (lastChk != null) {
				if (lastChk.GetType() == typeof(cChkIn)) {
					CIO_1 = new cChkInOut() { Vao = lastChk, Raa = null, ThuocCa = null, HaveINOUT = -2 };
					DSVaoRa.Add(CIO_1);
				}
				else {
					CIO_2 = new cChkInOut() { Vao = null, Raa = lastChk, ThuocCa = null, HaveINOUT = -1 };
					DSVaoRa.Add(CIO_2);
				}
			}

		}

		public static cShift XetThuocCa(cChk chkIn, cChk chkOut, List<cShift> pDSCa) {
			if (pDSCa != null)
				return pDSCa.FirstOrDefault(ca => chkIn.TimeStr >= chkIn.TimeStr.Date.Add(ca.OnTimeInTS) && chkIn.TimeStr <= chkIn.TimeStr.Date.Add(ca.CutInTS)
					&& chkOut.TimeStr >= chkIn.TimeStr.Date.Add(ca.OnTimeOutTS) && chkOut.TimeStr <= chkIn.TimeStr.Date.Add(ca.CutOutTS));

			return null;
		}

		internal static cChkInOut[] TachGio2Ca3Va1(List<cShift> pDSCa, cChkInOut ca3va1, cShift ThuocCa) {
			cChkInOut[] kq = new cChkInOut[2];
			cChkInOut CIO_Ca1 = new cChkInOut(), CIO_Ca3 = new cChkInOut();

			cShift ca3 = pDSCa.Find(item => item.OnnDutyTS == ThuocCa.OnnDutyTS && item.Workingday == 1f);
			cShift ca1 = pDSCa.Find(item => item.OffDutyTS == ThuocCa.OffDutyTS.Subtract(ThamSo._1ngay));

			cChkOut tempRaCa3 = new cChkOut() {
				TimeStr = ca3va1.Vao.TimeStr.Date + ThuocCa.OnnDutyTS + ca3.WorkingTimeTS,
				MachineNo = 22, Source = "PC"
			};
			cChkIn tempVaoCa1 = new cChkIn() {
				TimeStr = ca3va1.Vao.TimeStr.Date + ThuocCa.OnnDutyTS + ca3.WorkingTimeTS.Add(new TimeSpan(0, 0, 1)),
				MachineNo = 21, Source = "PC"
			};

			CIO_Ca3.Vao = ca3va1.Vao; CIO_Ca3.Raa = tempRaCa3;
			CIO_Ca3.HaveINOUT = 1;
			CIO_Ca3.ThuocCa = ca3;
			CIO_Ca3.QuaDem = true; // ca 3 qua đêm
			CIO_Ca3.TongGioThuc = TimeSpan.MinValue;//reset lại tổng giờ thực để tự động tính lại dữa vào HaveINOUT

			CIO_Ca1.Vao = tempVaoCa1; CIO_Ca1.Raa = ca3va1.Raa;
			CIO_Ca1.HaveINOUT = 1;
			CIO_Ca1.ThuocCa = ca1;
			CIO_Ca1.QuaDem = false; // ca 1 bình thường ko qua đêm
			CIO_Ca1.TongGioThuc = TimeSpan.MinValue;//reset lại tổng giờ thực để tự động tính lại dữa vào HaveINOUT

			kq[0] = CIO_Ca3; kq[1] = CIO_Ca1;
			return kq;
		}

		public static void ThongKe(cUserInfo nhanvien) {
			for (int i = 1; i < nhanvien.DSNgayCong.Count - 1; i++) {
				cNgayCong ngayCong = nhanvien.DSNgayCong[i];
				nhanvien.TongCongThang += ngayCong.TongCong;
				nhanvien.TongPCapThang += ngayCong.TongPhuCap;
				if (ngayCong.QuaDem) nhanvien.TongNgayQuaDem++;
				if (ngayCong.DSVang != null) {
					foreach (var loaiVang in ngayCong.DSVang) {
						switch (loaiVang.KyHieu) {
							case "P":
							case "NP":
								nhanvien.TongCongPhep = nhanvien.TongCongPhep + loaiVang.Cong;
								break;
							case "CV":
							case "NCV":
								nhanvien.TongCongCV = nhanvien.TongCongCV + loaiVang.Cong;
								break;
							case "BH":
								nhanvien.TongCongBH = nhanvien.TongCongBH + loaiVang.Cong;
								break;
							case "PT":
							case "NPT":
							case "H":
							case "NH":
							case "CT":
							case "NCT":
								nhanvien.TongCongH_CT_PT = nhanvien.TongCongH_CT_PT + loaiVang.Cong;
								break;
							case "RO":
							case "NRO":
								nhanvien.TongCongRo = nhanvien.TongCongRo + loaiVang.Cong;
								break;
						}
					}
				}

			}
		}

		public static void TinhLuong(DateTime ngayDauThang, List<cUserInfo> dsnv, double dongia, double sanluong, double luongtoithieu, double luongcongnhat, double boiduongca3) {
			double _80per_LuongA1 = dongia * sanluong * 0.8d;
			double tong_qlcb = 0d;
			double tong_HSLuongSPQuyDoi_B2 = 0d;
			foreach (var nv in dsnv) {
				ThongKe(nv);

				TinhCongChoViec(nv, ngayDauThang);
				nv.Luong.LuongCB = TinhLuongCoBanA20(nv, luongtoithieu);
				nv.Luong.BoiDuongQuaDem = TinhBoiDuongQuaDemA51(nv, boiduongca3);
				nv.Luong.HSLuongSPQuyDoi = TinhHSLuongSPQuyDoiB10(nv);
				tong_qlcb += nv.Luong.LuongCB + nv.Luong.BoiDuongQuaDem + nv.Luong.LuongThangTruoc;
				tong_HSLuongSPQuyDoi_B2 += nv.Luong.HSLuongSPQuyDoi;
			}
			double tong_qlSP_A71 = _80per_LuongA1 - tong_qlcb - luongcongnhat;
			double B3 = tong_qlSP_A71 / tong_HSLuongSPQuyDoi_B2;

			foreach (var nv in dsnv) {
				double luongSP_B41 = nv.Luong.HSLuongSPQuyDoi * B3;
				nv.Luong.LuongSP = luongSP_B41;
				nv.Luong.TongLuong = nv.Luong.LuongCB + nv.Luong.BoiDuongQuaDem + luongSP_B41 + nv.Luong.LuongThangTruoc;

			}
		}

		private static double TinhHSLuongSPQuyDoiB10(cUserInfo nv) {
			double b11 = nv.HeSoLuongSP * (nv.TongCongThang);
			double b12 = nv.TongPCapThang * nv.HeSoLuongSP;
			return b11 + b12;
		}

		private static double TinhBoiDuongQuaDemA51(cUserInfo nv, double boiduongca3) {
			return (nv.TongNgayQuaDem * boiduongca3);
		}

		private static double TinhLuongCoBanA20(cUserInfo nv, double luongtoithieu) {
			double a21_1 = (luongtoithieu * nv.HeSoLuongCB) / 26d;
			double a21_2 = a21_1 * (nv.TongCongThang);
			double a21_3 = a21_1 * (nv.TongCongCV);
			double a21_4 = a21_1 * (nv.TongPCapThang);
			double kq = a21_2 + a21_3 + a21_4;
			return kq;
		}

		private static void TinhCongChoViec(cUserInfo nv, DateTime ngayDauThang) {
			if (nv.TongCongThang == 0) {
				nv.TongCongCV = 0;
				return;
			}
			int soNgayCN = DemSoNgayCN(ngayDauThang);
			float CongChuan = (float)(DateTime.DaysInMonth(ngayDauThang.Year, ngayDauThang.Month) - soNgayCN);
			float tong = (int)(Math.Ceiling(nv.TongCongThang)) + nv.TongCongBH + nv.TongCongRo;
			if (tong >= CongChuan) nv.TongCongCV = nv.TongCongCV;
			else nv.TongCongCV += CongChuan - tong;
		}


		private static int DemSoNgayCN(DateTime ngayDauThang) {
			DateTime ngayBD = ngayDauThang;
			DateTime ngayKT = new DateTime(ngayDauThang.Year, ngayDauThang.Month, DateTime.DaysInMonth(ngayDauThang.Year, ngayDauThang.Month));
			int kq = 0;
			for (DateTime ngaydem = ngayBD; ngaydem.Date <= ngayKT.Date; ngaydem = ngaydem.AddDays(1d)) {
				if (ngaydem.DayOfWeek != DayOfWeek.Sunday) continue;
				kq++;
			}
			return kq;
		}

		public static void TinhCong(List<cChkInOut> pDSVaoRa) {
			if (pDSVaoRa == null || pDSVaoRa.Count == 0) return;
			foreach (cChkInOut CIO in pDSVaoRa) {
				CIO.TGLamTinhCong = ThamSo._0gio;
				CIO.TGLamDem = ThamSo._0gio;
				CIO.VaoTre = ThamSo._0gio;
				CIO.RaaSom = ThamSo._0gio;
				CIO.OLaiThem = ThamSo._0gio;
				CIO.Cong = 0f;
				CIO.PhuCapDem = 0f;
				#region old
				/*
                if (CIO.HaveINOUT < 0) continue;
				else if (CIO.ThuocCa.ShiftID == int.MinValue)
					TinhCongKoTheoCa(CIO, CIO.ThuocCa); //BUG không phải bug, chỉ là chú ý hàm này sẽ cập nhật thông tin onduty, offduty cho Ca KĐQĐ, on = vào, off = vào + 8tiếng
				else TinhCongTheoCa(CIO, CIO.ThuocCa);
*/
				#endregion
				#region new
				if (CIO.HaveINOUT < 0) continue;
				else TinhCongTheoCa(CIO, CIO.ThuocCa);
				#endregion
			}
		}

		/// <summary>
		/// thay đổi: Vao/RaaLam, VaoTre,RaaSom, OLaiThem,  TGlamTinhCong, Cong,BD/KTLamDem, TGLamDem,PhuCapDem\n
		/// ko thay đổi: Vao,Raa,TimeStrDaiDien, DaTinhCong,ThuocCa,Loai,ThuocNgayCong,haveinout
		/// </summary>
		/// <param name="pCheckINOUT"></param>
		/// <param name="ca"></param>
		public static void TinhCongTheoCa(cChkInOut pCheckINOUT, cShift ca) {
			cChkInOut kq = pCheckINOUT;

			DateTime tmpDate = ThamSo.GetDate(kq.Vao.TimeStr);
			DateTime vaoCa = tmpDate.Add(ca.OnnDutyTS);
			DateTime raaCa = tmpDate.Add(ca.OffDutyTS);
			DateTime chophepvaotre = tmpDate.Add(ca.chophepvaotreTS);
			DateTime chophepraasom = tmpDate.Add(ca.chopheprasomTS);
			DateTime batdaulamthem = tmpDate.Add(ca.batdaulamthemTS);
			DateTime tmpBDLamDem = tmpDate.Add(ThamSo._21h45);

			if (kq.Vao.TimeStr > chophepvaotre) {
				kq.VaoTre = kq.Vao.TimeStr.Subtract(vaoCa);
				kq.VaoLam = kq.Vao.TimeStr;
			}
			else {
				kq.VaoLam = vaoCa;	                //kq.VaoTre = 0
				kq.VaoTre = ThamSo._0gio;
			}

			if (kq.Raa.TimeStr < chophepraasom) {
				kq.RaaSom = raaCa.Subtract(kq.Raa.TimeStr);
				kq.RaaLam = kq.Raa.TimeStr;
			}
			else {
				kq.RaaLam = raaCa;
				kq.RaaSom = ThamSo._0gio;
			}

			if (pCheckINOUT.DaXN) kq.OLaiThem = ThamSo._0gio;
			else {
				kq.LamThem = ThamSo._0gio;
				if (kq.Raa.TimeStr > batdaulamthem)
					kq.OLaiThem = kq.Raa.TimeStr - raaCa;
				else
					kq.OLaiThem = ThamSo._0gio;
			}
			kq.RaaLam = kq.RaaLam + kq.LamThem;

			kq.TGLamTinhCong = kq.RaaLam - kq.VaoLam - ca.LunchMinute;
			kq.Cong = (float)((kq.TGLamTinhCong.TotalHours / ca.WorkingTimeTS.TotalHours) * ca.Workingday); // chia cho workingtime vì hcb làm 3 tiếng nhưng vẫn tính 1 công
			if (kq.RaaLam > tmpBDLamDem) {
				DateTime tmpKTLamDem = tmpDate.AddDays(1d).Add(ThamSo._05h45);
				kq.BDLamDem = kq.VaoLam >= (tmpBDLamDem + ThamSo._05phut) ? kq.VaoLam : tmpBDLamDem; //[Chú ý] suggest nên thay ThamSo._05phut = cho phép vào trễ
				kq.KTLamDem = kq.RaaLam <= (tmpKTLamDem - ThamSo._10phut) ? kq.RaaLam : tmpKTLamDem;//[Chú ý] suggest nên thay ThamSo._05phut = cho phép ra sớm
				kq.TGLamDem = kq.KTLamDem - kq.BDLamDem;
			}
			else kq.TGLamDem = ThamSo._0gio;
			if (kq.TGLamDem < ThamSo._02tieng) {
				kq.TGLamDem = ThamSo._0gio;
			}

			kq.PhuCapDem = (float)((kq.TGLamDem.TotalHours / 8d) * 0.3d);
		}

		public static void TinhCongKoTheoCa(cChkInOut pCheckINOUT, cShift ca) {
			cChkInOut kq = pCheckINOUT;

			DateTime tmpDate = ThamSo.GetDate(kq.Vao.TimeStr);
			DateTime tmpOnnDutyDT = kq.Vao.TimeStr;
			ca.OnnDutyTS = kq.Vao.TimeStr.TimeOfDay;
			DateTime tmpOffDutyDT = kq.Vao.TimeStr.Add(ThamSo._8gio);
			ca.OffDutyTS = kq.Vao.TimeStr.TimeOfDay.Add(ThamSo._8gio);
			// onduty 21h (timeofday), off = 21h+8h = 29h(tức 5h sáng hôm sau), lấy tmpDate là 0 giờ + 29h -> off duty
			// nói chung là offduty = onduty + 8 tiếng
			DateTime chophepvaotre = tmpOnnDutyDT.Add(ThamSo._05phut);//[TBD] đưa vào file config
			DateTime chophepraasom = tmpOffDutyDT.Subtract(ThamSo._10phut);//[TBD] đưa vào file config
			DateTime batdaulamthem = tmpOffDutyDT.Add(ThamSo._30phut);//[TBD] đưa vào file config
			DateTime tmpBDLamDem = tmpDate.Add(ThamSo._21h45);

			if (kq.Vao.TimeStr > chophepvaotre) {
				kq.VaoTre = kq.Vao.TimeStr.Subtract(tmpOnnDutyDT);
				kq.VaoLam = kq.Vao.TimeStr;
			}
			else {
				kq.VaoLam = tmpOnnDutyDT;	                //kq.VaoTre = 0
				kq.VaoTre = ThamSo._0gio;
			}

			if (kq.Raa.TimeStr < chophepraasom) {
				kq.RaaSom = tmpOffDutyDT.Subtract(kq.Raa.TimeStr);
				kq.RaaLam = kq.Raa.TimeStr;
			}
			else {
				kq.RaaLam = tmpOffDutyDT;
				kq.RaaSom = ThamSo._0gio;
			}

			kq.OLaiThem = ThamSo._0gio;
			if (kq.LamThem == ThamSo._0gio) { if (kq.Raa.TimeStr > batdaulamthem) kq.OLaiThem = kq.Raa.TimeStr - tmpOffDutyDT; }

			else kq.RaaLam = kq.RaaLam + kq.LamThem;

			kq.TGLamTinhCong = kq.RaaLam - kq.VaoLam; // không có trừ ca.LunchMinute vì tính công mặc định 8 tiếng
			kq.Cong = (float)(kq.TGLamTinhCong.TotalHours / 8f); // chia 8 tiếng
			if (kq.RaaLam > tmpBDLamDem) {
				DateTime tmpKTLamDem = tmpDate.AddDays(1d).Add(ThamSo._05h45);
				kq.BDLamDem = (kq.VaoLam > (tmpBDLamDem + ThamSo._05phut)) ? kq.VaoLam : tmpBDLamDem;
				kq.KTLamDem = (kq.RaaLam < (tmpKTLamDem - ThamSo._10phut)) ? kq.RaaLam : tmpKTLamDem;
				kq.TGLamDem = (kq.KTLamDem - kq.BDLamDem);
			}
			else kq.TGLamDem = ThamSo._0gio;

			kq.PhuCapDem = (float)((kq.TGLamDem.TotalHours / 8f) * 0.3f);
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

		public static void DuaVaoNgayCong(List<cChkInOut> pDSVaoRa, DateTime ngayBD, DateTime ngayKT, List<cNgayCong> pDSNgayCong) {
			DateTime ngaydem;
			if (pDSVaoRa.Count == 0) {
				ngaydem = ngayBD.Date;
				while (ngaydem <= ngayKT) { // <= vì lấy luôn ngày KT : vắng mặt
					cNgayCong ngayKOcheck = new cNgayCong() { NgayCong = ngaydem, HasCheck = false, DSVaoRa = new List<cChkInOut>(), };
					pDSNgayCong.Add(ngayKOcheck);
					ngaydem = ngaydem.AddDays(1.0d);
				}
				return;
			}

			int vtriBD = 0;
			ngaydem = ngayBD.Date;

			while (ngaydem <= ngayKT.Date) {

				if (vtriBD >= pDSVaoRa.Count) { // hết DS nhưng ngaydem <= ngày KT ==> ghi lại những ngày sau là vắng mặt
					while (ngaydem <= ngayKT.Date) {
						cNgayCong ngayKOcheck = new cNgayCong() { NgayCong = ngaydem, HasCheck = false, DSVaoRa = new List<cChkInOut>(), };
						pDSNgayCong.Add(ngayKOcheck);
						ngaydem = ngaydem.AddDays(1d);
					}
					continue;
				}

				// chưa hết DS, bắt đầu từ ngày của DSVaoRa tại vtbd, đó là ngày có mặt
				cChkInOut CIO = pDSVaoRa[vtriBD];
				DateTime ngayCoMat = ThamSo.GetDate(CIO.TimeStrDaiDien);

				// ghi lại những ngày vắng mặt trước ngày có mặt
				while (ngaydem < ngayCoMat) { // ko có = vì chỉ chạy đến trước ngày của tmpVaoRa thôi
					cNgayCong ngayKOcheck = new cNgayCong() { NgayCong = ngaydem, HasCheck = false, DSVaoRa = new List<cChkInOut>(), };
					pDSNgayCong.Add(ngayKOcheck);
					ngaydem = ngaydem.AddDays(1.0d);
				}
				//ngaydem = ngày có mặt. stop, tmpVaoRa là vào ra đầu tiên trong ngày
				#region collapse
				cNgayCong ngayCOcheck = new cNgayCong() {
					HasCheck = true, NgayCong = ngayCoMat, TinhPC150 = true, TinhPC200 = false,// mặc định khởi tạo ngày có tính PC 50%, nếu tồn tại ít nhất 1 ca nào ko tính pc 50% thì set cả ngày ko tính pc 50%
					TongGioLam = CIO.TGLamTinhCong, TongGioLamDem = CIO.TGLamDem, TongGioThuc = CIO.TongGioThuc,
					TongTre = CIO.VaoTre, TongSom = CIO.RaaSom,
					TongCong = CIO.Cong,
					PhuCapDem = ((CIO.TGLamDem.TotalHours > 0f)
					? ((float)(CIO.TGLamDem.TotalHours / 8f) * 0.3f)
					: 0f)
					// công thức cũ (tính phụ cấp tăng ca theo công) là : + (((tmpVaoRa.Cong > 1f) ? (tmpVaoRa.Cong - 1f) * 0.5f : 0f))
					// đổi lại công thức mới (tính phụ cấp theo giờ) là : + (((tmpVaoRa.TGLamTinhCong.TotalHours > 8f) ? (((float)tmpVaoRa.TGLamTinhCong.TotalHours - 8f) / 8f) * 0.5f : 0f))
				};
				if (CIO.QuaDem == true) ngayCOcheck.QuaDem = true; // set qua đêm nếu có
				if (ngayCOcheck.TinhPC150) {
					if (CIO.TinhPC150 == false) ngayCOcheck.TinhPC150 = false;

					if (ngayCOcheck.TinhPC150)
						ngayCOcheck.PhuCapThem = (((CIO.TGLamTinhCong.TotalHours > 8f)
														? ((((float)CIO.TGLamTinhCong.TotalHours - 8f) / 8f) * 0.5f)
														: 0f));
					else ngayCOcheck.PhuCapThem = 0f;
				}
				else ngayCOcheck.PhuCapThem = 0f;

				ngayCOcheck.TongPhuCap = ngayCOcheck.PhuCapDem + ngayCOcheck.PhuCapThem;

				ngayCOcheck.them(CIO);
				#endregion


				// sau khi tạo ngày công mới vào ra đó là vào ra đầu tiên thì chuyển sang VAORA next
				vtriBD++;
				// nếu hết ds thì ngưng, add tmpNgayCong1 vào danh sách ngày công (do chưa add)
				if (vtriBD >= pDSVaoRa.Count) {
					pDSNgayCong.Add(ngayCOcheck);
					ngaydem = ngayCOcheck.NgayCong.AddDays(1d);
					while (ngaydem <= ngayKT.Date) {
						cNgayCong ngayKOcheck = new cNgayCong() { NgayCong = ngaydem, HasCheck = false, DSVaoRa = new List<cChkInOut>(), };
						pDSNgayCong.Add(ngayKOcheck);
						ngaydem = ngaydem.AddDays(1.0d);
					}
					continue;
				}
				// chưa hết DSVàoRa, xem các vào ra kế tiếp, nếu cùng ngày công thì add thêm vào tmpNgayCong1
				while (vtriBD < pDSVaoRa.Count && ThamSo.GetDate(pDSVaoRa[vtriBD].TimeStrDaiDien) == ngayCOcheck.NgayCong) {
					cChkInOut CIO_1 = pDSVaoRa[vtriBD];
					#region collapse
					ngayCOcheck.TongGioThuc += CIO_1.TongGioThuc;
					ngayCOcheck.TongGioLam += CIO_1.TGLamTinhCong;
					ngayCOcheck.TongGioLamDem += CIO_1.TGLamDem;
					ngayCOcheck.TongTre += CIO_1.VaoTre;
					ngayCOcheck.TongSom += CIO_1.RaaSom;
					ngayCOcheck.TongCong += CIO_1.Cong;
					ngayCOcheck.PhuCapDem = ((ngayCOcheck.TongGioLamDem.TotalHours > 0f)
												   ? ((float)(ngayCOcheck.TongGioLamDem.TotalHours / 8f) * 0.3f)
												   : 0f);
					#endregion
					if (CIO_1.QuaDem == true) ngayCOcheck.QuaDem = true; // set qua đêm nếu có

					if (ngayCOcheck.TinhPC150) {
						if (CIO.TinhPC150 == false) ngayCOcheck.TinhPC150 = false;

						if (ngayCOcheck.TinhPC150)
							ngayCOcheck.PhuCapThem = (((ngayCOcheck.TongGioLam.TotalHours > 8f)
															? ((((float)ngayCOcheck.TongGioLam.TotalHours - 8f) / 8f) * 0.5f)
															: 0f));
						else ngayCOcheck.PhuCapThem = 0f;
					}
					else ngayCOcheck.PhuCapThem = 0f;

					ngayCOcheck.TongPhuCap = ngayCOcheck.PhuCapDem + ngayCOcheck.PhuCapThem;

					ngayCOcheck.them(CIO_1);

					vtriBD++;
				}
				//thoát khỏi vòng lặp: hoặc hết ds hoặc chuyển sang ngày mới
				if (vtriBD >= pDSVaoRa.Count) {
					pDSNgayCong.Add(ngayCOcheck);
					ngaydem = ngayCOcheck.NgayCong.AddDays(1d);
					while (ngaydem <= ngayKT.Date) {
						cNgayCong tmpNgayCong = new cNgayCong() { NgayCong = ngaydem, HasCheck = false, DSVaoRa = new List<cChkInOut>(), };
						pDSNgayCong.Add(tmpNgayCong);
						ngaydem = ngaydem.AddDays(1.0d);
					}
				}
				else {
					pDSNgayCong.Add(ngayCOcheck);
					ngaydem = ngaydem.AddDays(1.0d);
				}
			}
		}


		public static bool ThemGioChoNV(cUserInfo tmpNhanvien, DateTime pGio, int pMachineNo, string pLydo, string pGhichu) {
			int kq = 0;
			string OriginType = (pMachineNo%2 != 0) ? "I" : "O";
			kq = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.InsStrThemGioVaoRa()
				, new string[] { "@UserEnrollNumber", "@TimeDate", "@TimeStr", "@OriginType", "@Source", "@MachineNo", "@WorkCode", "@UserID", "@Explain", "@Note", "@CommandType" }
				, new object[] { tmpNhanvien.UserEnrollNumber, pGio.Date, pGio, OriginType, "PC", pMachineNo, 0, ThamSo.currUserID, pLydo, pGhichu, 1});
			if (kq == 0) return false;
			return true;
		}
		public static bool SuaGioChoNV(int pUserEnrollNumber, cChk pCheckOld, DateTime pGioMoi, bool pIsChkInNew, string pLydo, string pGhichu) {
			int kq = 0;
			bool tmpIsChkInOld = (pCheckOld.GetType() == typeof(cChkIn));
			int pMachineNoNew;
			if (tmpIsChkInOld == pIsChkInNew) pMachineNoNew = pCheckOld.MachineNo;
			else {
				if (pIsChkInNew) pMachineNoNew = 21;
				else pMachineNoNew = 22;
			}


			kq = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.UpdStrSuaGioVaoRa(), new string[] { "@UserEnrollNumber", "@TimeStrOld", "@TimeDateNew", "@TimeStrNew", "@SourceNew", "@MachineNoNew" },
														 new object[] { pUserEnrollNumber, pCheckOld.TimeStr, pGioMoi.Date, pGioMoi, "PC", pMachineNoNew });
			if (kq == 0) return false;
			kq = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.InsStrBackupThemGioVaoRa(),
														 new string[] { "@UserEnrollNumber", "@TimeStrOld", "@TimeStrNew", "@SourceOld", "@SourceNew", "@MachineNoOld", "@MachineNoNew", "@UserID", "@Explain", "@Note", "@CommandType" },
														 new object[] { pUserEnrollNumber, pCheckOld.TimeStr, pGioMoi, pCheckOld.Source, "PC", pCheckOld.MachineNo, pMachineNoNew, ThamSo.currUserID, pLydo, pGhichu, 0 });
			if (kq == 0) return false;

			if (pCheckOld.GioLienQuan != null && pCheckOld.GioLienQuan.Count != 0) {
				foreach (cChk tmpGioLQ in pCheckOld.GioLienQuan) {
					tmpIsChkInOld = (tmpGioLQ.GetType() == typeof(cChkIn));
					if (tmpIsChkInOld == pIsChkInNew) pMachineNoNew = pCheckOld.MachineNo;
					else {
						if (pIsChkInNew) pMachineNoNew = 21;
						else pMachineNoNew = 22;
					}

					kq = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.UpdStrSuaGioVaoRa(), new string[] { "@UserEnrollNumber", "@TimeStrOld", "@TimeDateNew", "@TimeStrNew", "@SourceNew", "@MachineNoNew" },
																 new object[] { pUserEnrollNumber, tmpGioLQ.TimeStr, pGioMoi.Date, pGioMoi, "PC", pMachineNoNew });
					if (kq == 0) return false;
					kq = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.InsStrBackupThemGioVaoRa(),
																 new string[] {
                                                                         "@UserEnrollNumber" , "@TimeStrOld" , "@TimeStrNew" , "@SourceOld" , "@SourceNew" , "@MachineNoOld" , "@MachineNoNew" , "@UserID" , "@Explain" ,
                                                                         "@Note" , "@CommandType"
                                                                 },
																 new object[] {
                                                                         pUserEnrollNumber , tmpGioLQ.TimeStr , pGioMoi , tmpGioLQ.Source , "PC" , tmpGioLQ.MachineNo , pMachineNoNew , ThamSo.currUserID ,
                                                                         pLydo , pGhichu , 0
                                                                 });
					if (kq == 0) return false;
				}
			}
			return true;
		}
		public static bool XoaGioChoNV(cUserInfo tmpNhanvien, cChk pCheck, string pLydo, string pGhichu) {
			int kq = 0;

			kq = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.DelStrXoaGioVaoRa(), new string[] { "@UserEnrollNumber", "@TimeStrOld", "@MachineNoOld" },
														 new object[] { tmpNhanvien.UserEnrollNumber, pCheck.TimeStr, pCheck.MachineNo });
			if (kq == 0) return false;
			kq = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.InsStrBackupThemGioVaoRa(),
														 new string[] { "@UserEnrollNumber", "@TimeStrOld", "@TimeStrNew", "@SourceOld", "@SourceNew", "@MachineNoOld", "@MachineNoNew", "@UserID", "@Explain", "@Note", "@CommandType" },
														 new object[] { tmpNhanvien.UserEnrollNumber, pCheck.TimeStr, pCheck.TimeStr, pCheck.Source, pCheck.Source, pCheck.MachineNo, pCheck.MachineNo, ThamSo.currUserID, pLydo, pGhichu, -1 });
			if (kq == 0) return false;

			if (pCheck.GioLienQuan != null) {
				foreach (cChk tmpGioLQ in pCheck.GioLienQuan) {
					kq = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.DelStrXoaGioVaoRa(), new string[] { "@UserEnrollNumber", "@TimeStrOld", "@MachineNoOld" },
																 new object[] { tmpNhanvien.UserEnrollNumber, pCheck.TimeStr, pCheck.MachineNo });
					if (kq == 0) return false;
					kq = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.InsStrBackupThemGioVaoRa(),
														 new string[] { "@UserEnrollNumber", "@TimeStrOld", "@TimeStrNew", "@SourceOld", "@SourceNew", "@MachineNoOld", "@MachineNoNew", "@UserID", "@Explain", "@Note", "@CommandType" },
														 new object[] { tmpNhanvien.UserEnrollNumber, tmpGioLQ.TimeStr, tmpGioLQ.TimeStr, tmpGioLQ.Source, tmpGioLQ.Source, tmpGioLQ.MachineNo, tmpGioLQ.MachineNo, ThamSo.currUserID, pLydo, pGhichu, -1 });
					if (kq == 0) return false;
				}
			}
			return true;
		}


		public static void BUS_XacNhan(cUserInfo pNV, cChkInOut pOldCIO, cChkInOut NewCIO, bool pIsOT, int pSoPhutLamThem, TimeSpan pSoPhutLamThemTS, bool pTinhPC150) {
			//int kq1, kq2, kq3;
			TimeSpan tmpOnduty = NewCIO.ThuocCa.OnnDutyTS;
			TimeSpan tmpOffduty = NewCIO.ThuocCa.OffDutyTS;
			int tmpDayCount = NewCIO.ThuocCa.DayCount;
			TimeSpan tmpWKT = NewCIO.ThuocCa.WorkingTimeTS;
			float tmpWKDay = NewCIO.ThuocCa.Workingday;
			DateTime tmpOldTimeStrIn = pOldCIO.Vao.TimeStr, tmpOldTimeStrOut = pOldCIO.Raa.TimeStr;

			/*
			"@ShiftID", "@Onduty", "@Offduty", "@DayCount", "@WorkingTime", "@Workingday", 
														"@TimeStrIn", "@TimeStrOut", "@LateMin", "@EarlyMin", "@OTMin", "@Note"
*/
			int iNewShiftID = NewCIO.ThuocCa.ShiftID;
			DateTime dOldTimeStrVao = pOldCIO.Vao.TimeStr;
			DateTime dOldTimeStrRa = pOldCIO.Raa.TimeStr;
			//DateTime dTimeDateNew = tmpNewChkINOUT.VaoLam.Date;
			cShift pNewShift = NewCIO.ThuocCa;
			int pSoPhutTre = (int)Math.Floor(NewCIO.VaoTre.TotalMinutes);
			int pSoPhutSom = (int)Math.Floor(NewCIO.RaaSom.TotalMinutes);

			// nếu có làm thêm: lấy giờ vào - số phút trễ => TimeStrIn
			// TimeStr out = giờ ra + làm thêm
			SqlDataAccessHelper.ExecNoneQueryString(ThamSo.InsStrXacNhanCa(),
													new string[] { "@UserEnrollNumber",  "@ShiftID",  "@ShiftCode",  "@Onduty", "@Offduty"
                                                                 , "@DayCount", "@WorkingTime", "@Workingday"
														         , "@LateGrace", "@EarlyGrace", "@AfterOT", "@TinhPC150"
                                                                 , "@TimeStrIn", "@TimeStrOut", "@OTMin", "@Note" },
													new object[] { pNV.UserEnrollNumber, iNewShiftID, pNewShift.ShiftCode, pNewShift.OnnDutyTS.ToString(@"hh\:mm"), pNewShift.OffDutyTS.ToString(@"hh\:mm")
                                                                 , pNewShift.DayCount, pNewShift.WorkingTimeTS.TotalMinutes, pNewShift.Workingday
													             , pNewShift.LateGraceTS.TotalMinutes, pNewShift.EarlyGraceTS.TotalMinutes, pNewShift.AfterOTTS.TotalMinutes, pTinhPC150
                                                                 , dOldTimeStrVao, dOldTimeStrRa, pSoPhutLamThem, ""});

			DataTable dt2 = SqlDataAccessHelper.ExecuteQueryString(ThamSo.SelIDXacNhanCa(),
				new string[] { "@UserEnrollNumber", "@TimeStrIn", "@TimeStrOut" },
				new object[] { pNV.UserEnrollNumber, dOldTimeStrVao, dOldTimeStrRa });

			int tmpNewID = (int)dt2.Rows[0]["ID"];

			SqlDataAccessHelper.ExecNoneQueryString(ThamSo.UpdStrXacNhanCa(),
												   new string[] { "@UserEnrollNumber", "@TimeStrOld", "@MachineNoOld", 
                                                      "@SourceNew", "@MachineNoNew", "@IDXacNhanCaVaLamThem" },
												   new object[] {  pNV.UserEnrollNumber, dOldTimeStrVao, pOldCIO.Vao.MachineNo,  // [Chú ý] "@TimeStrNew" =dOldTimeStrVao vì sử dụng giải pháp giữ nguyên giờ cũ, lấy thông tin Vào ca ra ca để set ngược lại giờ vào làm ra làm
													   "PC",  "21", tmpNewID});

			SqlDataAccessHelper.ExecNoneQueryString(ThamSo.InsStrBackupThemGioVaoRa()
				, new string[] { "@UserEnrollNumber", "@TimeStrOld", "@TimeStrNew"
                               , "@SourceOld", "@SourceNew", "@MachineNoOld", "@MachineNoNew"
                               , "@UserID", "@Explain", "@Note", "@CommandType" }
				, new object[] { pNV.UserEnrollNumber, dOldTimeStrVao, dOldTimeStrVao
                               , pOldCIO.Vao.Source, "PC", pOldCIO.Vao.MachineNo, "21"
                               , ThamSo.currUserID, "Xác nhận giờ", "", 0 });
			// [Chú ý] "@TimeStrNew" =dOldTimeStrVao vì sử dụng giải pháp giữ nguyên giờ cũ, lấy thông tin Vào ca ra ca để set ngược lại giờ vào làm ra làm

			SqlDataAccessHelper.ExecNoneQueryString(ThamSo.UpdStrXacNhanCa(),
												   new string[] { "@UserEnrollNumber", "@TimeStrOld", "@MachineNoOld", 
													  "@SourceNew", "@MachineNoNew", "@IDXacNhanCaVaLamThem" },
												   new object[] {  pNV.UserEnrollNumber, dOldTimeStrRa, pOldCIO.Raa.MachineNo,
													   "PC",  "22", tmpNewID});

			SqlDataAccessHelper.ExecNoneQueryString(ThamSo.InsStrBackupThemGioVaoRa()
				, new string[] { "@UserEnrollNumber", "@TimeStrOld", "@TimeStrNew"
                               , "@SourceOld", "@SourceNew", "@MachineNoOld", "@MachineNoNew"
                               , "@UserID", "@Explain", "@Note", "@CommandType" }
				, new object[] { pNV.UserEnrollNumber, dOldTimeStrRa, dOldTimeStrRa
                               , pOldCIO.Raa.Source, "PC", pOldCIO.Raa.MachineNo, "22"
                               , ThamSo.currUserID, "Explain", "Note", 0 });


		}

		public static void BUS_TachCaVaXacNhan(cUserInfo pNV, cChkInOut pOldCIO, cChkInOut[] pNewCIO, bool pIsOT, int pSoPhutLamThem, TimeSpan pSoPhutLamThemTS, bool pTinhPC150) {
			//int kq1, kq2, kq3;
			cChkInOut CIO1 = pNewCIO[0];
			cChkInOut CIO2 = pNewCIO[1];
			// insert ra ca3 trước, vào ca 1 sau
			ThemGioChoNV(pNV, CIO1.Raa.TimeStr, 22, "tách giờ", "");
			ThemGioChoNV(pNV, CIO2.Vao.TimeStr, 21, "tách giờ", "");

			BUS_XacNhan(pNV, CIO1, CIO1, false, 0, ThamSo._0gio, pTinhPC150);
			BUS_XacNhan(pNV, CIO2, CIO2, false, pSoPhutLamThem, pSoPhutLamThemTS, pTinhPC150);

		}

		public static void TableCIO_To_DSCheck(DataRow[] arrRow, cUserInfo nhanvien, DateTime pNgayBD, DateTime pNgayKT) {
			nhanvien.ClearAll();
			if (arrRow.Length == 0) return;
			foreach (DataRow row in arrRow) {
				DateTime tmpTimeStr = (DateTime)row["TimeStr"];
				int tmpMachineNo = (int)row["MachineNo"];
				string tmpSource = row["Source"].ToString();

				if (row["IDXacNhanCaVaLamThem"] == DBNull.Value)
					nhanvien.Check_GioChuaXN(tmpTimeStr, tmpSource, tmpMachineNo);
				else {
					//  [chú ý] check đã xác nhận, đồng thời Add trước vào DSVào ra
					//  -> sau này sắp xếp lại, TÍNH CÔNG LUÔN CHO GIỜ NÀY

					#region lấy dữ liệu giờ đã xác nhận và fill vào, đồng thời add luôn vào DSG_VaoRa (tính trước, sau này add thêm DSG_ChuaXN rồi sắp xếp lại
					int tmpIDXacNhanCaVaLamThem = (int)row["IDXacNhanCaVaLamThem"];
					int tmpShiftID = (int)row["ShiftID"];
					string tmpShiftCode = row["ShiftCode"].ToString();
					int tmpDayCount = (int)row["DayCount"];
					float tmpWKDay = (float)row["Workingday"];
					TimeSpan tmpOn, tmpOff;
					TimeSpan.TryParse(row["Onduty"].ToString(), out tmpOn);
					TimeSpan.TryParse(row["Offduty"].ToString(), out tmpOff);

					TimeSpan tempAfterOT = new TimeSpan(0, (int)row["AfterOT"], 0);
					TimeSpan tempLateGrace = new TimeSpan(0, (int)row["LateGrace"], 0);
					TimeSpan tempEarlyGrace = new TimeSpan(0, (int)row["EarlyGrace"], 0);

					tempLateGrace = tmpOn.Add(tempLateGrace);
					tmpOff = tmpOff.Add(new TimeSpan(tmpDayCount, 0, 0, 0));
					tempEarlyGrace = tmpOff.Subtract(tempEarlyGrace);
					tempAfterOT = tmpOff.Add(tempAfterOT);
					bool tempTinhPC150, tempTinhPC200 = false;
					if (row["TinhPC150"] == DBNull.Value) tempTinhPC150 = false;
					else tempTinhPC150 = (bool)row["TinhPC150"];
					#region tính PC200 temp
					//if (row["TinhPC200"] == DBNull.Value) tempTinhPC200 = false;
					//else tempTinhPC200 = (bool)row["TinhPC200"];
					#endregion

					int tmpWKTime = int.Parse(row["WorkingTime"].ToString());
					#endregion

					nhanvien.Check_GioDaXN(tmpTimeStr, tmpSource, tmpMachineNo,
										   tmpIDXacNhanCaVaLamThem, tmpShiftID, tmpShiftCode,
										   tmpOn, tmpOff, tempLateGrace, tempEarlyGrace, tempAfterOT, tempTinhPC150, tempTinhPC200
										   , tmpDayCount, new TimeSpan(0, tmpWKTime, 0), tmpWKDay
										   , (DateTime)row["TimeStrIn"], (DateTime)row["TimeStrOut"]
										   , (int)row["OTMin"], row["Note"].ToString());

				}
			}
			/*
						XL.LayDSCheckDaiDien(nhanvien.DSG_ChuaXN);
						XL.XetGioHopLe(nhanvien.DSG_ChuaXN, nhanvien.DSCa, nhanvien.DSVaoRa);
						nhanvien.DSVaoRa.Sort(new cChkInOutComparer());
						XL.TinhCong(nhanvien.DSVaoRa);
						XL.DuaVaoNgayCong(nhanvien.DSVaoRa, pBDVao.Date, pKTVao.Date, nhanvien.DSNgayCong);
			*/
		}

		public static bool KhaiBaoNgayVangChoNV(object[,] DSMaCC_MaNV_Checked, List<DateTime> DSNgayCheck, DataRowView row) {
			int kqThaotac = 0;
			bool flagError = false;
			float cong = (float)row["WorkingDay"];
			float workingtime = (float)row["WorkingTime"];
			log.Info("KhaiBaoNgayLeChoNV: so luong nv check: " + DSMaCC_MaNV_Checked[0, 0] + "\tSo ngay check:" + DSNgayCheck.Count + "\n row view: " + row.Row.ItemArray + "\tCong:" + cong.ToString());
			for (int i = 1; i <= (int)DSMaCC_MaNV_Checked[0, 0]; i++) {
				int maCC = (int)DSMaCC_MaNV_Checked[i, 0];
				string maNV = DSMaCC_MaNV_Checked[i, 1].ToString();
				foreach (DateTime ngay in DSNgayCheck) {
					kqThaotac = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.InsStrThemNgayVang()
						, new string[] { "@UserEnrollNumber", "@UserFullCode", "@TimeDate", "@AbsentCode", "@Thang", "@Nam", "@Workingday", "@WorkingTime" }
						, new object[] { maCC, maNV, ngay.Date, row["AbsentCode"].ToString(), ngay.Month, ngay.Year, cong, workingtime });
					if (kqThaotac == 0) {
						log.Info("KhaiBaoNgayLeChoNV: Loi: " + DSMaCC_MaNV_Checked[i, 0] + "\ngay check:" + ngay.ToString() + "\n row view: " + row.Row.ItemArray + "\tCong:" + cong.ToString());
						flagError = true;
						break;
					}
				}
				if (flagError) break;
			}
			if (flagError) return false;
			return true;
		}

		public static DataTable LietKeNgayVangChoNV(object[] arrDSNVCheck, DateTime ngayBD, DateTime ngayKT) {
			string query = ThamSo.SelStrLayDSNgayVang();
			query = String.Format(query, String.Join(" or UserInfo.UserEnrollNumber = ", arrDSNVCheck));
			DataTable table = SqlDataAccessHelper.ExecuteQueryString(query
				, new string[] { "@NgayBD", "@NgayKT" }
				, new object[] { ngayBD, ngayKT });
			return table;
		}

		public static bool XoaNgayVangNV(DataRow[] arrRecord) {
			int kqThaotac = 0;
			bool flagError = false;
			foreach (DataRow row in arrRecord) {
				int ID = (int)row["ID"];
				kqThaotac = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.DelStrXoaNgayVang(), new string[] { "@ID" }, new object[] { ID });
				if (kqThaotac == 0) {
					flagError = true;
					break;
				}
			}
			if (flagError == true) return false;
			return true;
		}

		public static DataTable TaoCauTrucDataTable(string[] colName, Type[] colType) {
			DataTable kq = new DataTable();
			for (int i = 0; i < colName.Length; i++) {
				kq.Columns.Add(colName[i], colType[i]);
			}
			return kq;
		}

		public static int ChamCongTay(int iMaCC, DateTime ngay, float fCong, float fPhucap) {
			int kqThuchien = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.InsStrThemChamCongTay(),
																	 new string[] { "@UserEnrollNumber", "@Ngay", "@Cong", "@PhuCap" },
																	 new object[] { iMaCC, ngay, fCong, fPhucap });
			return kqThuchien;
		}

		public static bool KhaiBaoLVNgayNghiChoNV(object[,] arrDSNVCheck, DateTime NgayKhaiBao, float hesoPC) {
			for (int i = 1; i <= (int)arrDSNVCheck[0, 0]; i++) {

				int UserEnrollNumber = (int)arrDSNVCheck[i, 0];
				int kqThuchien = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.InsStrThemLVNgayNghi(),
																		 new string[] { "@UserEnrollNumber", "@Ngay", @"HeSoPC" },
																		 new object[] { UserEnrollNumber, NgayKhaiBao, hesoPC });
				if (kqThuchien == 0)
					return false;
			}

			return true;
		}

		public static DataTable LietKeLamViecNgayNghiDaKhaiBao(object[] arrDSNVCheck, DateTime ngayBD, DateTime ngayKT) {
			string query = ThamSo.SelStrLayDSNgayNghiDaKhaiBao();
			query = String.Format(query, String.Join(" or LamViecNgayNghi.UserEnrollNumber = ", arrDSNVCheck));
			DataTable table = SqlDataAccessHelper.ExecuteQueryString(query
				, new string[] { "@NgayBD", "@NgayKT" }
				, new object[] { ngayBD, ngayKT });
			return table;
		}

		public static bool XoaLamViecNgayNghiDaKhaiBao(DataRow[] arrRecord) {
			int kqThaotac = 0;
			bool flagError = false;
			foreach (DataRow row in arrRecord) {
				int ID = (int)row["ID"];
				kqThaotac = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.DelStrXoaLamViecNgayNghi(), new string[] { "@ID" }, new object[] { ID });
				if (kqThaotac == 0) {
					flagError = true;
					break;
				}
			}
			if (flagError == true) return false;
			return true;
		}

		internal static bool GhiNhanDieuChinhLuong(int iMaCC, DateTime thang, decimal luong) {
			int kqThaotac = 0;
			bool flagError = false;
			kqThaotac = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.InsStrDieuChinhLuong(), new string[] { "@UserEnrollNumber", "@Thang", "@Nam", "@LuongDieuChinh" }
					, new object[] { iMaCC, thang.Month, thang.Year, luong });
			if (kqThaotac == 0) flagError = true;
			if (flagError == true) return false;
			return true;
		}

		internal static void DocLuongDieuChinh(DateTime thang, List<cUserInfo> dsnv) {
			DateTime ngayCuoiThangTruoc = thang.AddMonths(-1);
			DataTable dieuchinhluong = SqlDataAccessHelper.ExecuteQueryString("select * from DieuChinhLuongThangTruoc where Thang=@Thang and Nam=@Nam order by UserEnrollNumber"
				, new string[] { "@Thang", "@Nam" }, new object[] { ngayCuoiThangTruoc.Month, ngayCuoiThangTruoc.Year });
			if (dieuchinhluong.Rows.Count == 0) return;
			foreach (DataRow row in dieuchinhluong.Rows) {
				int iUserEnrollNumber = (int)row["UserEnrollNumber"];
				cUserInfo nv = dsnv.Find(o => o.UserEnrollNumber == iUserEnrollNumber);
				decimal luong = (decimal)row["LuongDieuChinh"];
				nv.Luong.LuongThangTruoc = (double)luong;
			}
		}


		#region new
		public static List<cChk> TableToDSCheck(DataTable table) {
			List<cChk> kq = new List<cChk>();
			foreach (DataRow row in table.Rows) {
				int iMachineNo = (int)row["MachineNo"];
				DateTime time = (DateTime)row["TimeStr"];
				int id = (row["IDXacNhanCaVaLamThem"] == DBNull.Value) ? -1 : (int)row["IDXacNhanCaVaLamThem"];
				if (iMachineNo % 2 == 1) {
					if (id == -1) {
						cChkInn_A chk = new cChkInn_A() { GioLienQuan = null, MachineNo = iMachineNo, Source = (string)row["Source"], TimeStr = time };
						kq.Add(chk);
					}
					else {
						cChkInn_V chk = new cChkInn_V() { GioLienQuan = null, MachineNo = iMachineNo, Source = (string)row["Source"], TimeStr = time, ID = id };
						kq.Add(chk);
					}
				}
				else {
					if (id == -1) {
						cChkOut_A chk = new cChkOut_A() { GioLienQuan = null, MachineNo = iMachineNo, Source = (string)row["Source"], TimeStr = time };
						kq.Add(chk);
					}
					else {
						cChkOut_V chk = new cChkOut_V() { GioLienQuan = null, MachineNo = iMachineNo, Source = (string)row["Source"], TimeStr = time, ID = id };
						kq.Add(chk);
					}
				}
			}
			return kq;
		}

		public static List<cChkInOut> GhepCIO_A(List<cChk> DSchkInn, List<cChk> DSchkOut) {
			List<cChkInOut> kq = new List<cChkInOut>();
			int x1 = 0, x2 = 0;
			if (DSchkInn.Count == 0 && DSchkOut.Count != 0) {
				while (x2 < DSchkOut.Count) {
					kq.Add(new cChkInOut() { Vao = null, Raa = DSchkOut[x2], HaveINOUT = -1, TongGioThuc = ThamSo._0gio });
				}
			}
			else if (DSchkInn.Count != 0 && DSchkOut.Count == 0) {
				while (x1 < DSchkInn.Count) {
					kq.Add(new cChkInOut() { Vao = DSchkInn[x1], Raa = null, HaveINOUT = -2, TongGioThuc = ThamSo._0gio });
				}
			}
			else if (DSchkInn.Count != 0 && DSchkOut.Count != 0) {
				while (x1 < DSchkInn.Count && x2 < DSchkOut.Count) {
					cChk chkinn = DSchkInn[x1];
					cChk chkout = DSchkOut[x2];
					DateTime time1 = chkinn.TimeStr;
					DateTime time2 = chkout.TimeStr;
					if (time2 < time1)//ra ko vào
					{
						kq.Add(new cChkInOut() { Vao = null, Raa = DSchkOut[x2], HaveINOUT = -1, TongGioThuc = ThamSo._0gio });
						x2++;
					}
					else {
						TimeSpan duration = time2 - time1;
						if (duration <= ThamSo._30phut || duration > ThamSo._21h45) {
							kq.Add(new cChkInOut() { Vao = DSchkInn[x1], Raa = null, HaveINOUT = -2, TongGioThuc = ThamSo._0gio });
							kq.Add(new cChkInOut() { Vao = null, Raa = DSchkOut[x2], HaveINOUT = -1, TongGioThuc = ThamSo._0gio });
						}
						else {
							kq.Add(new cChkInOut() { Vao = DSchkInn[x1], Raa = DSchkOut[x2], HaveINOUT = 1, TongGioThuc = duration });
						}
						x1++;
						x2++;
					}
				}
				if (x2 < DSchkOut.Count) {
					while (x2 < DSchkOut.Count) {
						kq.Add(new cChkInOut() { Vao = null, Raa = DSchkOut[x2], HaveINOUT = -1, TongGioThuc = ThamSo._0gio });
					}
				}
				else if (x1 < DSchkInn.Count) {
					while (x1 < DSchkInn.Count) {
						kq.Add(new cChkInOut() { Vao = DSchkInn[x1], Raa = null, HaveINOUT = -2, TongGioThuc = ThamSo._0gio });
					}
				}

			}

			return kq;
		}

		public static cShift XetCa(cChkInOut CIO, List<cShift> DSCa) {
			DateTime t_vao = CIO.Vao.TimeStr;
			DateTime t_raa = CIO.Raa.TimeStr;
			return DSCa.FirstOrDefault(ca => t_vao >= t_vao.Date.Add(ca.OnTimeInTS) && t_vao <= t_vao.Date.Add(ca.CutInTS)
									&& t_raa >= t_vao.Date.Add(ca.OnTimeOutTS) && t_raa <= t_vao.Date.Add(ca.CutOutTS));
		}

		public static void XetCa(List<cChkInOut> dsCIO, List<cShift> dsca) {
			for (int i = 0; i < dsCIO.Count; i++) {
				cChkInOut CIO = dsCIO[i];
				cShift ca = XetCa(CIO, dsca);
				if (ca != null) {
					if (ca.Workingday == 2f && ca.OnnDutyTS > ThamSo._20h00) {
						cChk raaca3 = new cChkOut() { GioLienQuan = null, MachineNo = 22, Source = "O", TimeStr = CIO.Vao.TimeStr.Date.Add(ca.OnnDutyTS).Add(ThamSo._8gio) };
						cChk vaoca1 = new cChkIn() { GioLienQuan = null, MachineNo = 21, Source = "I", TimeStr = CIO.Vao.TimeStr.Date.Add(ca.OnnDutyTS).Add(ThamSo._8gio1giay) };
						CIO.Raa = raaca3;
						i = i - 1;
						cChkInOut newCIO = new cChkInOut() { Vao = vaoca1, Raa = CIO.Raa, HaveINOUT = 1, };
						dsCIO.Insert(i, newCIO);
					}
					else {
						CIO.ThuocCa = ca;
					}
				}
				else {
					ca = new cShift();
					TaoCaTuDo(ca, CIO.Vao.TimeStr, ThamSo._8gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1f);
					CIO.ThuocCa = ca;
				}
			}

		}

		public static void TinhCongSetNgay(List<cChkInOut> dsCIO) {
			foreach (cChkInOut CIO in dsCIO) {
				TinhCongTheoCa(CIO, CIO.ThuocCa);

			}
		}
		#endregion
	}
}
