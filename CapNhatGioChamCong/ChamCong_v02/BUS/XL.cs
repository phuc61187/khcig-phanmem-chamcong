using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v02.DTO;
using ChamCong_v02.DAO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using log4net;

namespace ChamCong_v02.BUS {
	public static class XL {
		private static readonly ILog log = LogManager.GetLogger("XL");

		public static DataTable tablePHONG;

		public static List<cUserInfo> XemCong2(DataTable tableUserInfo, int[] ArrDSMaCC_Checked, DateTime ngayBD, DateTime ngayKT) {
			log4net.Config.XmlConfigurator.Configure();
			List<cUserInfo> dsnv = new List<cUserInfo>();

			DataTable tableCIO_A = DAL.LayTableCIO_A(ArrDSMaCC_Checked, ngayBD, ngayKT);
			DataTable tableCIO_V = DAL.LayTableCIO_V(ArrDSMaCC_Checked, ngayBD, ngayKT);
			DataTable tableVang = DAL.LayTableVang(ArrDSMaCC_Checked, ngayBD, ngayKT);
			DataTable tableLamViecNgayNghi = DAL.LayTableLamViecNgayNghi(ArrDSMaCC_Checked, ngayBD, ngayKT);
			DataTable tableNgayLe = DAL.DocNgayLe(ngayBD, ngayKT);
			bool HaveHoliday = (tableNgayLe.Rows.Count > 0);

			for (int x = 0; x < ArrDSMaCC_Checked.Length; x++) {
				int tempMaCC = ArrDSMaCC_Checked[x];
				DataRow[] arrCIO_A = tableCIO_A.Select("UserEnrollNumber = " + tempMaCC, "TimeStr asc");
				DataRow[] arrCIO_V = tableCIO_V.Select("UserEnrollNumber = " + tempMaCC, "ID asc, TimeStr asc");
				DataRow[] arrVangg = tableVang.Select("UserEnrollNumber = " + tempMaCC, "TimeDate asc");
				DataRow[] arrLVNN = tableLamViecNgayNghi.Select("UserEnrollNumber = " + tempMaCC, "Ngay asc");
				DataRow[] userinfo = tableUserInfo.Select("UserEnrollNumber = " + tempMaCC);

				cUserInfo nhanvien = new cUserInfo();
				nhanvien.UpdateUserInfo(userinfo);
				nhanvien.ArrayRowsToDSCheck_A(arrCIO_A);
				nhanvien.DS_CIO_A = nhanvien.GhepCIO_A(nhanvien.ds_CheckInn_A, nhanvien.ds_CheckOut_A);
				#region comment
				//                for (int i = 0; i < nhanvien.DS_CIO_A.Count; i++) {
				//					cChkInOut_A cio = nhanvien.DS_CIO_A[i];
				//					string temp = nhanvien.ToString();
				//					if (cio.HaveINOUT == -1) {
				//						if (i - 1 >= 0) temp += nhanvien.DS_CIO_A[i - 1].ToString() + ";\n";
				//						else temp += "null;\n";
				//						temp += cio.ToString() + "\t; Next: " + cio.next.ToString() + "\n";
				//						if (i + 1 < nhanvien.DS_CIO_A.Count) temp += nhanvien.DS_CIO_A[i + 1].ToString() + ";\n";
				//						else temp += "null;\n";
				//					}
				//					else if (cio.HaveINOUT == -2) {
				//						if (i - 1 >= 0) temp += nhanvien.DS_CIO_A[i - 1].ToString() + ";\n";
				//						else temp += "null;\n";
				//						temp += cio.ToString() + "\t; Prev: " + cio.prev.ToString() + "\n";
				//						if (i + 1 < nhanvien.DS_CIO_A.Count) temp += nhanvien.DS_CIO_A[i + 1].ToString() + ";\n";
				//						else temp += "null;\n";
				//					}
				//					else {
				//						continue;
				//					}
				//					temp += "\n";
				//					log.Info(temp);
				//				}
				#endregion
				nhanvien.ArrayRowsToDS_CIO_V(arrCIO_V);
				nhanvien.XetCa(nhanvien.DS_CIO_A, nhanvien.DSCa, nhanvien.MacDinhTinhPC150);
				nhanvien.DSVaoRa = nhanvien.TronDS_CIO_A_V(nhanvien.DS_CIO_A, nhanvien.DS_CIO_V);
				nhanvien.DSNgayCong = nhanvien.TinhCongTheoNgay(nhanvien.DSVaoRa, ngayBD, ngayKT, nhanvien.MacDinhTinhPC150);
				nhanvien.ArrayRowsToDSVang(arrVangg);
				if (HaveHoliday) { nhanvien.KBNgayLe(tableNgayLe); }
				nhanvien.ArrayRowsToDSLamViecNgayNghi(arrLVNN);

				//sau khi thực hiện xong thì add vào danh sách nhân viên
				dsnv.Add(nhanvien);
			}

			return dsnv;
		}

		public static void XemCong(cUserInfo nhanvien, DateTime ngayBD, DateTime ngayKT) {
			log4net.Config.XmlConfigurator.Configure();

			DataTable tableCIO_A = DAL.LayTableCIO_A(new int[] { nhanvien.UserEnrollNumber }, ngayBD, ngayKT);
			DataTable tableCIO_V = DAL.LayTableCIO_V(new int[] { nhanvien.UserEnrollNumber }, ngayBD, ngayKT);
			DataTable tableVang = DAL.LayTableVang(new int[] { nhanvien.UserEnrollNumber }, ngayBD, ngayKT);
			DataTable tableLamViecNgayNghi = DAL.LayTableLamViecNgayNghi(new int[] { nhanvien.UserEnrollNumber }, ngayBD, ngayKT);
			DataTable tableNgayLe = DAL.DocNgayLe(ngayBD, ngayKT);
			bool HaveHoliday = (tableNgayLe.Rows.Count > 0);

			DataRow[] arrCIO_A, arrCIO_V, arrVangg, arrLVNN;
			if (tableCIO_A.Rows.Count > 0) { arrCIO_A = new DataRow[tableCIO_A.Rows.Count]; tableCIO_A.Rows.CopyTo(arrCIO_A, 0); }
			else arrCIO_A = new DataRow[] { };
			if (tableCIO_V.Rows.Count > 0) { arrCIO_V = new DataRow[tableCIO_V.Rows.Count]; tableCIO_V.Rows.CopyTo(arrCIO_V, 0); }
			else arrCIO_V = new DataRow[] { };
			if (tableVang.Rows.Count > 0) { arrVangg = new DataRow[tableVang.Rows.Count]; tableVang.Rows.CopyTo(arrVangg, 0); }
			else arrVangg = new DataRow[] { };
			if (tableLamViecNgayNghi.Rows.Count > 0) { arrLVNN = new DataRow[tableLamViecNgayNghi.Rows.Count]; tableLamViecNgayNghi.Rows.CopyTo(arrLVNN, 0); }
			else arrLVNN = new DataRow[] { };

			nhanvien.ArrayRowsToDSCheck_A(arrCIO_A);
			nhanvien.DS_CIO_A = nhanvien.GhepCIO_A(nhanvien.ds_CheckInn_A, nhanvien.ds_CheckOut_A);
			nhanvien.ArrayRowsToDS_CIO_V(arrCIO_V);
			nhanvien.XetCa(nhanvien.DS_CIO_A, nhanvien.DSCa, nhanvien.MacDinhTinhPC150);
			nhanvien.DSVaoRa = nhanvien.TronDS_CIO_A_V(nhanvien.DS_CIO_A, nhanvien.DS_CIO_V);
			nhanvien.DSNgayCong = nhanvien.TinhCongTheoNgay(nhanvien.DSVaoRa, ngayBD, ngayKT, nhanvien.MacDinhTinhPC150);
			nhanvien.ArrayRowsToDSVang(arrVangg);
			if (HaveHoliday) { nhanvien.KBNgayLe(tableNgayLe); }
			nhanvien.ArrayRowsToDSLamViecNgayNghi(arrLVNN);

		}

		public static DataTable TaoTableXemCong(List<cUserInfo> DSNVXemCong) {
			DataTable kq = TaoCauTrucDataTableTongHop();

			foreach (cUserInfo nhanvien in DSNVXemCong) {
				//duyệt qua từng ngày công của nhân viên để fill datarow
				for (int i = 1; i < nhanvien.DSNgayCong.Count - 1; i++) { //[CHÚ Ý] ở đây chỉ chạy từ 1-> kế cuối
					cNgayCong ngayCong = nhanvien.DSNgayCong[i];
					DataRow rowTH = kq.NewRow();
					rowTH["check"] = false;
					rowTH["obj"] = ngayCong;
					rowTH["UserEnrollNumber"] = nhanvien.UserEnrollNumber;
					rowTH["UserFullName"] = nhanvien.UserFullName;
					rowTH["UserFullCode"] = nhanvien.UserFullCode;
					rowTH["IDD_1"] = nhanvien.PBCap1.ID;
					rowTH["Description_1"] = nhanvien.PBCap1.TenPhongBan;
					rowTH["IDD_2"] = nhanvien.PBCap2.ID;
					rowTH["Description_2"] = nhanvien.PBCap2.TenPhongBan;
					rowTH["TimeStrNgay"] = rowTH["TimeStrThu"] = ngayCong.NgayCong;
					rowTH["Cong"] = ngayCong.TongCong;
					rowTH["PhuCap"] = ngayCong.TongPhuCap;
					rowTH["TimeStrTre"] = Math.Floor(ngayCong.TongTre.TotalMinutes);
					rowTH["TimeStrSom"] = Math.Floor(ngayCong.TongSom.TotalMinutes);
					rowTH["TongGioLam"] = ngayCong.TG.LamTinhCong.TotalHours;
					rowTH["TongGioThuc"] = ngayCong.TongGioThuc.TotalHours;
					if (ngayCong.HasCheck) {
						int i1 = 1;
						foreach (cChkInOut cChkInOut1 in ngayCong.DSVaoRa) {

							if (i1 > 3) break;
							rowTH[ThamSo.nameVao + i1] = cChkInOut1.Vao != null ? (object)cChkInOut1.Vao.TimeStr : DBNull.Value;
							rowTH[ThamSo.nameRa + i1] = cChkInOut1.Raa != null ? (object)cChkInOut1.Raa.TimeStr : DBNull.Value;
							rowTH["ShiftID" + i1] = (cChkInOut1.HaveINOUT > 0) ? (object)cChkInOut1.ThuocCa.ShiftID : DBNull.Value;
							rowTH["ShiftCode"] += (cChkInOut1.HaveINOUT > 0) ? cChkInOut1.ThuocCa.ShiftCode + "; " : ((cChkInOut1.HaveINOUT == -2) ? "KV; " : "KR; ");
							i1++;
						}
						if (ngayCong.DSVang != null && ngayCong.DSVang.Count != 0) {
							foreach (cLoaiVang o in ngayCong.DSVang)
								rowTH["ShiftCode"] += o.KyHieu + "; ";
						}
					}
					else {
						if (ngayCong.DSVang != null && ngayCong.DSVang.Count != 0) {
							foreach (cLoaiVang o in ngayCong.DSVang)
								rowTH["ShiftCode"] += o.KyHieu + "; ";
						}
						else rowTH["ShiftCode"] = "--";
					}
					kq.Rows.Add(rowTH);
				}
			}

			return kq;
		}

		public static DataTable TaoTableDiemDanh(List<cUserInfo> flstDSNVDiemDanh, out int SoNVDangLamViec, out int SoNVDaRaVe, out int SoNVVang) {
			DataTable kq = XL.TaoCauTrucDataTable(
					new[] { "UserEnrollNumber", "UserFullCode", "UserFullName", "TimeStrVao1", "TimeStrRa1", "TimeStrVao2", "TimeStrRa2", "TimeStrVao3", "TimeStrRa3", "ShiftID1", "ShiftID2", "ShiftID3", "Ca", "TrangThai" },
					new[] { typeof(int), typeof(string), typeof(string), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(int), typeof(int), typeof(int), typeof(string), typeof(string), });
			SoNVDangLamViec = 0;
			SoNVDaRaVe = 0;
			SoNVVang = 0;

			foreach (var nhanvien in flstDSNVDiemDanh) {
				DataRow row = kq.NewRow();
				row["UserEnrollNumber"] = nhanvien.UserEnrollNumber;
				row["UserFullCode"] = nhanvien.UserFullCode;
				row["UserFullName"] = nhanvien.UserFullName;
				cNgayCong ngayCong = nhanvien.DSNgayCong[1];
				//nếu có check thì khỏi ghi vắng
				string ChuoiCa = string.Empty;
				string ChuoiTrangThai = string.Empty;
				if (ngayCong.HasCheck) {
					for (int i = 0; i < ngayCong.DSVaoRa.Count; i++) {
						if (i >= 3) break;
						row["TimeStrVao" + (i + 1)] = (ngayCong.DSVaoRa[i].Vao != null) ? ngayCong.DSVaoRa[i].Vao.TimeStr : (object)DBNull.Value;
						row["TimeStrRa" + (i + 1)] = (ngayCong.DSVaoRa[i].Raa != null) ? ngayCong.DSVaoRa[i].Raa.TimeStr : (object)DBNull.Value;
						if (ngayCong.DSVaoRa[i].HaveINOUT > 0)
							ChuoiCa += ngayCong.DSVaoRa[i].ThuocCa.ShiftCode + "; ";
						else if (ngayCong.DSVaoRa[i].HaveINOUT == -2)
							ChuoiCa += "KV; ";
						else if (ngayCong.DSVaoRa[i].HaveINOUT == -1)
							ChuoiCa += "KR; ";
					}
					cChkInOut lastCIO1 = ngayCong.DSVaoRa[ngayCong.DSVaoRa.Count - 1];
					// xét vào ra cuối để ghi trạng thái
					if (lastCIO1.HaveINOUT == -1) {
						ChuoiTrangThai = "Đang làm việc; ";
						SoNVDangLamViec++;
					}
					else if (lastCIO1.HaveINOUT > 0 || lastCIO1.HaveINOUT == -2) {
						ChuoiTrangThai = "Đã ra về; ";
						SoNVDaRaVe++;
					}

				}
				else { // không có check, kiểm tra có khai báo vắng ko, nếu có thì ghi
					SoNVVang++;
					ChuoiCa = string.Empty;
					if (ngayCong.DSVang != null && ngayCong.DSVang.Count != 0) {
						ChuoiTrangThai = ngayCong.XuatChuoiVang();
					}
					else {
						ChuoiTrangThai = "Vắng";
					}
				}
				row["Ca"] = ChuoiCa;
				row["TrangThai"] = ChuoiTrangThai;
				kq.Rows.Add(row);
			}
			return kq;
		}


		#region tao cau truc datatable
		public static DataTable TaoCauTrucDataTable(string[] colName, Type[] colType) {
			DataTable kq = new DataTable();
			for (int i = 0; i < colName.Length; i++) {
				kq.Columns.Add(colName[i], colType[i]);
			}
			return kq;
		}

		private static DataTable TaoCauTrucDataTableTongHop() {
			DataTable kq = new DataTable();
			kq.Columns.Add("check", typeof(bool)); //0
			kq.Columns.Add("UserEnrollNumber", typeof(int)); //0
			kq.Columns.Add("UserFullName", typeof(string)); //1
			kq.Columns.Add("UserFullCode", typeof(string));
			kq.Columns.Add("TimeStrNgay", typeof(DateTime)); //2
			kq.Columns.Add("TimeStrThu", typeof(DateTime)); //3
			kq.Columns.Add("TimeStrVao1", typeof(DateTime)); //4
			kq.Columns.Add("TimeStrRa1", typeof(DateTime)); //5
			kq.Columns.Add("TimeStrVao2", typeof(DateTime)); //6
			kq.Columns.Add("TimeStrRa2", typeof(DateTime)); //7
			kq.Columns.Add("TimeStrVao3", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRa3", typeof(DateTime)); //9
			kq.Columns.Add("TimeStrVao4", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRa4", typeof(DateTime)); //9
			kq.Columns.Add("TimeStrVao5", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRa5", typeof(DateTime)); //9
			kq.Columns.Add("ShiftID1", typeof(int)); //8
			kq.Columns.Add("ShiftID2", typeof(int)); //9
			kq.Columns.Add("ShiftID3", typeof(int)); //9
			kq.Columns.Add("ShiftID4", typeof(int)); //9
			kq.Columns.Add("ShiftID5", typeof(int)); //9
			kq.Columns.Add("TimeStrTre", typeof(int)); //18
			kq.Columns.Add("TimeStrSom", typeof(int)); //19
			kq.Columns.Add("Cong", typeof(float)); //20
			kq.Columns.Add("ShiftCode", typeof(string)); //21
			kq.Columns.Add("PhuCap", typeof(float));//22
			kq.Columns.Add("TongGioLam", typeof(float));
			kq.Columns.Add("TongGioThuc", typeof(float));
			kq.Columns.Add("obj", typeof(cNgayCong));
			kq.Columns.Add("IDD_1", typeof(int));
			kq.Columns.Add("Description_1", typeof(string));
			kq.Columns.Add("IDD_2", typeof(int));
			kq.Columns.Add("Description_2", typeof(string));
			return kq;
		}
		#endregion


		public static cShift XetThuocCa(cChk chkIn, cChk chkOut, List<cShift> pDSCa) {
			if (pDSCa != null)
				return pDSCa.FirstOrDefault(ca => chkIn.TimeStr >= chkIn.TimeStr.Date.Add(ca.OnTimeInTS) && chkIn.TimeStr <= chkIn.TimeStr.Date.Add(ca.CutInTS)
					&& chkOut.TimeStr >= chkIn.TimeStr.Date.Add(ca.OnTimeOutTS) && chkOut.TimeStr <= chkIn.TimeStr.Date.Add(ca.CutOutTS));

			return null;
		}

		#region thong ke va tinh luong
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
							case "TS":
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
							case "L":
								nhanvien.TongCongLe = nhanvien.TongCongLe + loaiVang.Cong;
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
				nv.Luong.LuongCB_TheoCong = TinhLuongCoBan_CongVaPC_A20(nv, luongtoithieu);
				nv.Luong.BoiDuongQuaDem = TinhBoiDuongQuaDemA51(nv, boiduongca3);
				nv.Luong.SP_LamRa_TheoCong = TinhHSLuongSPQuyDoiB10(nv);
				tong_qlcb += nv.Luong.LuongCB_TheoCong + nv.Luong.BoiDuongQuaDem + nv.Luong.LuongThangTruoc;
				tong_HSLuongSPQuyDoi_B2 += nv.Luong.SP_LamRa_TheoCong;
			}
			double tong_qlSP_A71 = _80per_LuongA1 - tong_qlcb - luongcongnhat;
			double B3 = tong_qlSP_A71 / tong_HSLuongSPQuyDoi_B2;

			foreach (var nv in dsnv) {
				double luongSP_B41 = nv.Luong.SP_LamRa_TheoCong * B3;
				nv.Luong.LuongSP_TheoCong = luongSP_B41;
				nv.Luong.TongLuong = nv.Luong.LuongCB_TheoCong + nv.Luong.BoiDuongQuaDem + luongSP_B41 + nv.Luong.LuongThangTruoc;
				nv.Luong.ThucLanh = nv.Luong.TongLuong - nv.Luong.KhauTruBH - nv.Luong.TamUng;
			}
		}

		private static double TinhHSLuongSPQuyDoiB10(cUserInfo nv) {
			double b11 = nv.HeSo.LuongCV * (nv.TongCongThang + nv.TongCongPhep + nv.TongCongH_CT_PT + nv.TongCongLe);
			double b12 = nv.TongPCapThang * nv.HeSo.LuongCV;
			return b11 + b12;
		}

		private static double TinhBoiDuongQuaDemA51(cUserInfo nv, double boiduongca3) {
			return (nv.TongNgayQuaDem * boiduongca3);
		}

		private static double TinhLuongCoBan_CongVaPC_A20(cUserInfo nv, double luongtoithieu) {
			double a21_1 = (luongtoithieu * nv.HeSo.LuongCB) / 26d;
			double a21_2 = a21_1 * (nv.TongCongThang + nv.TongCongPhep + nv.TongCongH_CT_PT + nv.TongCongLe);
			double a21_3 = a21_1 * (nv.TongCongCV);
			double a21_4 = a21_1 * (nv.TongPCapThang);
			double kq = a21_2 + a21_3 + a21_4;
			return kq;
		}

		private static void TinhCongChoViec(cUserInfo nv, DateTime ngayDauThang) {
			//nếu tổng công tháng = 0 tức ko có đi làm thì set tổng công cv = 0
			if (nv.TongCongThang == 0) {
				nv.TongCongCV = 0;
				return;
			}
			int soNgayCN = DemSoNgayCN(ngayDauThang);
			Double CongChuan = Convert.ToDouble((DateTime.DaysInMonth(ngayDauThang.Year, ngayDauThang.Month) - soNgayCN));
			Double tong = Math.Ceiling(nv.TongCongThang) + nv.TongCongPhep + nv.TongCongLe + nv.TongCongH_CT_PT + nv.TongCongBH + nv.TongCongRo + nv.TongCongCV;
			if (tong >= CongChuan) nv.TongCongCV = nv.TongCongCV;
			else nv.TongCongCV += CongChuan - tong;
		}

		#region tính lương 2
		public static void TinhLuong2(DateTime ngayDauThang, List<cUserInfo> dsnv, double dongia, double sanluong, double luongtoithieu, double luongcongnhat, double boiduongca3) {
			double _80per_LuongA1 = dongia * sanluong * 0.8d;
			double tong_qlcb = 0d, luongcb_cong_pc_1NV = 0d, boiduongca3_1NV = 0d, spLamRa_cong_pc_1NV = 0d;
			double tong_spLamRa_B2 = 0d;
			foreach (var nv in dsnv) {
				ThongKe(nv);

				TinhCongChoViec(nv, ngayDauThang);
				luongcb_cong_pc_1NV = TinhLuongCoBan_CongVaPC_A202(nv, luongtoithieu);
				nv.Luong.BoiDuongQuaDem = TinhBoiDuongQuaDemA512(nv, boiduongca3);
				tong_qlcb += luongcb_cong_pc_1NV + nv.Luong.BoiDuongQuaDem + nv.Luong.LuongThangTruoc;
				spLamRa_cong_pc_1NV = TinhSPLamRa_CongVaPC_B102(nv);
				tong_spLamRa_B2 += spLamRa_cong_pc_1NV;
			}
			double tong_qlSP_A71 = _80per_LuongA1 - tong_qlcb - luongcongnhat;
			double giaTri_1SP_B3 = tong_qlSP_A71 / tong_spLamRa_B2; // tính ra được 1 đơn vị sản phẩm có giá bao nhiêu

			foreach (var nv in dsnv) {
				nv.Luong.LuongSP_TheoCong = nv.Luong.SP_LamRa_TheoCong * giaTri_1SP_B3;
				nv.Luong.LuongSP_TheoPCap = nv.Luong.SP_LamRa_TheoPCap * giaTri_1SP_B3;
				nv.Luong.TongLuong_TheoCong = nv.Luong.LuongCB_TheoCong + nv.Luong.LuongSP_TheoCong;
				nv.Luong.TongLuong_TheoPCap = nv.Luong.TongLuongCB_TheoPCap + nv.Luong.LuongSP_TheoPCap;
				nv.Luong.TongLuong = nv.Luong.TongLuong_TheoCong + nv.Luong.TongLuong_TheoPCap + nv.Luong.LuongThangTruoc + nv.Luong.BoiDuongQuaDem;
				nv.Luong.ThucLanh = nv.Luong.TongLuong - nv.Luong.KhauTruBH - nv.Luong.TamUng - nv.Luong.ThuChiKhac;
			}
		}

		private static double TinhSPLamRa_CongVaPC_B102(cUserInfo nv) {
			double b11 = nv.HeSo.LuongCV * (nv.TongCongThang + nv.TongCongPhep + nv.TongCongH_CT_PT + nv.TongCongLe);
			nv.Luong.SP_LamRa_TheoCong = b11;
			double b12 = nv.TongPCapThang * nv.HeSo.LuongCV;
			nv.Luong.SP_LamRa_TheoPCap = b12;
			return b11 + b12;
		}

		private static double TinhBoiDuongQuaDemA512(cUserInfo nv, double boiduongca3) {
			return (nv.TongNgayQuaDem * boiduongca3);
		}

		private static double TinhLuongCoBan_CongVaPC_A202(cUserInfo nv, double luongtoithieu) {
			double a21_1 = (luongtoithieu * nv.HeSo.LuongCB) / 26d;
			double a21_2 = a21_1 * (nv.TongCongThang + nv.TongCongPhep + nv.TongCongH_CT_PT + nv.TongCongLe);
			double a21_3 = a21_1 * (nv.TongCongCV);
			nv.Luong.LuongCB_TheoCong = a21_2 + a21_3;
			double a21_4 = a21_1 * (nv.TongPCapThang);
			nv.Luong.LuongCB_TheoPCap = a21_4;
			double kq = a21_2 + a21_3 + a21_4;
			return kq;
		}

		private static void TinhCongChoViec2(cUserInfo nv, DateTime ngayDauThang) {
			//nếu tổng công tháng = 0 tức ko có đi làm thì set tổng công cv = 0
			if (nv.TongCongThang == 0) {
				nv.TongCongCV = 0;
				return;
			}
			int soNgayCN = DemSoNgayCN(ngayDauThang);
			Double CongChuan = Convert.ToDouble((DateTime.DaysInMonth(ngayDauThang.Year, ngayDauThang.Month) - soNgayCN));
			Double tong = Math.Ceiling(nv.TongCongThang) + nv.TongCongPhep + nv.TongCongLe + nv.TongCongH_CT_PT + nv.TongCongBH + nv.TongCongRo + nv.TongCongCV;
			if (tong >= CongChuan) nv.TongCongCV = nv.TongCongCV;
			else nv.TongCongCV += CongChuan - tong;
		}

		#endregion

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
		#endregion

		public static void TinhCong(List<cChkInOut> pDSVaoRa) {
			if (pDSVaoRa == null || pDSVaoRa.Count == 0) return;
			foreach (cChkInOut CIO in pDSVaoRa) {
				CIO.TG.LamTinhCong = ThamSo._0gio;
				CIO.TG.LamTinhPC30 = ThamSo._0gio;
				CIO.VaoTre = ThamSo._0gio;
				CIO.RaaSom = ThamSo._0gio;
				CIO.OLaiThem = ThamSo._0gio;
				CIO.Cong = 0d;
				CIO.PhuCapDem = 0d;

				if (CIO.HaveINOUT < 0) continue;
				else TinhCongTheoCa(CIO, CIO.ThuocCa);//BUG không phải bug, chỉ là chú ý hàm này sẽ cập nhật thông tin onduty, offduty cho Ca KĐQĐ, on = vào, off = vào + 8tiếng
			}
		}

		/// <summary>
		/// thay đổi: Vao/RaaLam, VaoTre,RaaSom, OLaiThem,  TGlamTinhCong, Cong,BD/KTLamDem, TGLamDem,PhuCapDem\n
		/// ko thay đổi: Vao,Raa,TimeStrDaiDien, DaTinhCong,ThuocCa,Loai,ThuocNgayCong,haveinout
		/// </summary>
		/// <param name="pCheckINOUT"></param>
		/// <param name="ca"></param>
		public static void TinhCongTheoCa(cChkInOut pCheckINOUT, cShift ca) {
			try {
				if (pCheckINOUT.HaveINOUT < 0) return;
				cChkInOut kq = pCheckINOUT;
				DateTime tmpDate = kq.Vao.TimeStr.Date;

				DateTime vaoCa = tmpDate.Add(ca.OnnDutyTS);
				DateTime raaCa = tmpDate.Add(ca.OffDutyTS);//off duty này đã bao gồm daycount được công bên trong
				DateTime chophepvaotre = tmpDate.Add(ca.chophepvaotreTS);
				DateTime chophepraasom = tmpDate.Add(ca.chopheprasomTS);
				DateTime batdaulamthem = tmpDate.Add(ca.batdaulamthemTS);
				DateTime tmpBDLamDem = tmpDate.Add(ThamSo._21h45);
				DateTime BDLamDem, KTLamDem;

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

				if (pCheckINOUT.GetType() == typeof(cChkInOut_V)) kq.OLaiThem = ThamSo._0gio;
				else {
					kq.LamThem = ThamSo._0gio;
					if (kq.Raa.TimeStr > batdaulamthem)
						kq.OLaiThem = kq.Raa.TimeStr - raaCa;
					else
						kq.OLaiThem = ThamSo._0gio;
				}
				kq.RaaLam = kq.RaaLam + kq.LamThem;

				kq.TG.LamTinhCong = kq.RaaLam - kq.VaoLam - ca.LunchMinute;
				kq.Cong = Math.Round(((kq.TG.LamTinhCong.TotalHours / ca.WorkingTimeTS.TotalHours) * ca.Workingday), 2); // chia cho workingtime vì hcb làm 3 tiếng nhưng vẫn tính 1 công

				if (kq.RaaLam > tmpBDLamDem) {
					DateTime tmpKTLamDem = tmpDate.AddDays(1d).Add(ThamSo._05h45);
					BDLamDem = kq.VaoLam >= (tmpBDLamDem + ThamSo._05phut) ? kq.VaoLam : tmpBDLamDem; //[Chú ý] suggest nên thay ThamSo._05phut = cho phép vào trễ
					KTLamDem = kq.RaaLam <= (tmpKTLamDem - ThamSo._10phut) ? kq.RaaLam : tmpKTLamDem;//[Chú ý] suggest nên thay ThamSo._05phut = cho phép ra sớm
					kq.TG.LamTinhPC30 = KTLamDem - BDLamDem;
				}
				else kq.TG.LamTinhPC30 = ThamSo._0gio;
				if (kq.TG.LamTinhPC30 < Properties.Settings.Default.TGLamDemToiThieu) {
					kq.TG.LamTinhPC30 = ThamSo._0gio;
					kq.QuaDem = false;
				}
				else {
					kq.QuaDem = true;
				}
				double pcdem = Convert.ToDouble(Properties.Settings.Default.PCDem / 100);
				kq.PhuCapDem = Math.Round(((kq.TG.LamTinhPC30.TotalHours / 8d) * pcdem), 2);

				// tính trọn công, phụ cấp
				if (kq.ThuocCa.ShiftID > 0) {
					kq.LamTron.TongCong = kq.ThuocCa.Workingday;
					kq.LamTron.TongPC30 = Math.Round((kq.LamTron.TongCong * pcdem), 2);
				}
				else {
					kq.LamTron.TongCong = kq.Cong;
					kq.LamTron.TongPC30 = kq.PhuCapDem;
				}
			} catch (Exception exception) {
				log4net.Config.XmlConfigurator.Configure();
				log.Fatal("TinhCongTheoCa \nStackTrace:" + exception.StackTrace + "--end StartTrace");
				throw exception;
			}

		}

		public static TimeSpan TinhVaoTre(DateTime Vao, DateTime OnnDuty, DateTime chophepvaotre) {
			return (Vao > chophepvaotre) ? Vao.Subtract(OnnDuty) : ThamSo._0gio;
		}
		public static TimeSpan TinhRaaSom(DateTime Raa, DateTime OffDuty, DateTime chophepraasom) {
			return (Raa < chophepraasom) ? OffDuty.Subtract(Raa) : ThamSo._0gio;
		}
		public static DateTime TinhVaoLam(DateTime Vao, DateTime OnnDuty, TimeSpan tre) {
			return (tre == ThamSo._0gio) ? OnnDuty : Vao;
		}
		public static DateTime TinhRaaLam(DateTime Raa, DateTime OffDuty, TimeSpan som, TimeSpan OT) {
			return (som == ThamSo._0gio) ? OffDuty.Add(OT) : Raa;
		}
		public static TimeSpan TinhOLaiThem(DateTime Raa, DateTime OffDuty, DateTime batdaulamthem) {
			return (Raa > batdaulamthem) ? Raa - OffDuty : ThamSo._0gio;
		}
		public static DateTime TinhBDLamDem(DateTime VaoLam, DateTime BDLamDem) {
			return VaoLam >= (BDLamDem + ThamSo._05phut) ? VaoLam : BDLamDem; //[Chú ý] suggest nên thay ThamSo._05phut = cho phép vào trễ
		}
		public static DateTime TinhKTLamDem(DateTime RaaLam, DateTime KTLamDem) {
			return RaaLam >= (KTLamDem + ThamSo._10phut) ? RaaLam : KTLamDem; //[Chú ý] suggest nên thay ThamSo._05phut = cho phép vào trễ
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

		internal static cChkInOut[] TachGio2Ca3Va1(List<cShift> pDSCa, cChkInOut ca3va1, cShift ThuocCa) {
			cChkInOut[] kq = new cChkInOut[2];

			cChkInOut_V CIO_Ca1 = new cChkInOut_V(), CIO_Ca3 = new cChkInOut_V();

			// tách ca chỉ có ca 3 và 1, cả và 1a
			cShift ca3 = pDSCa.Find(item => item.OnnDutyTS == ThuocCa.OnnDutyTS && item.Workingday == 1f);
			cShift ca1 = pDSCa.Find(item => item.OffDutyTS == ThuocCa.OffDutyTS.Subtract(ThamSo._1ngay));

			cChkOut_V tempRaCa3 = new cChkOut_V() {
				TimeStr = ca3va1.Vao.TimeStr.Date + ThuocCa.OnnDutyTS + ca3.WorkingTimeTS,
				MachineNo = 22, Source = "PC"
			};
			cChkInn_V tempVaoCa1 = new cChkInn_V() {
				TimeStr = ca3va1.Vao.TimeStr.Date + ThuocCa.OnnDutyTS + ca3.WorkingTimeTS.Add(new TimeSpan(0, 0, 1)),
				MachineNo = 21, Source = "PC"
			};

			CIO_Ca3.Vao = ca3va1.Vao; CIO_Ca3.Raa = tempRaCa3;
			CIO_Ca3.TimeStrDaiDien = ca3va1.TimeStrDaiDien;
			CIO_Ca3.ThuocNgayCong = ca3va1.TimeStrDaiDien.Date;
			CIO_Ca3.HaveINOUT = 1;
			CIO_Ca3.TinhPC150 = ca3va1.TinhPC150;
			CIO_Ca3.ThuocCa = ca3;
			CIO_Ca3.QuaDem = true; // ca 3 qua đêm
			CIO_Ca3.TongGioThuc = TimeSpan.MinValue;//reset lại tổng giờ thực để tự động tính lại dữa vào HaveINOUT

			CIO_Ca1.Vao = tempVaoCa1; CIO_Ca1.Raa = ca3va1.Raa;
			CIO_Ca1.TimeStrDaiDien = tempVaoCa1.TimeStr;
			CIO_Ca1.ThuocNgayCong = tempVaoCa1.TimeStr.Date;
			CIO_Ca1.HaveINOUT = 1;
			CIO_Ca1.TinhPC150 = ca3va1.TinhPC150;
			CIO_Ca1.ThuocCa = ca1;
			CIO_Ca1.QuaDem = false; // ca 1 bình thường ko qua đêm
			CIO_Ca1.TongGioThuc = TimeSpan.MinValue;//reset lại tổng giờ thực để tự động tính lại dữa vào HaveINOUT

			kq[0] = CIO_Ca3; kq[1] = CIO_Ca1;

			return kq;
		}

		public static void XemCong(cUserInfo nhanvien, List<cUserInfo> dsnv, DateTime ngayBD, DateTime ngayKT) {
			//List<cUserInfo> dsnv = new List<cUserInfo>();
			int iUserEnrollNumber = nhanvien.UserEnrollNumber;
			DataTable tableCIO_A = DAL.LayTableCIO_A(iUserEnrollNumber, ngayBD, ngayKT);
			DataTable tableCIO_V = DAL.LayTableCIO_V(iUserEnrollNumber, ngayBD, ngayKT);
			DataTable tableVang = DAL.LayTableVang(iUserEnrollNumber, ngayBD, ngayKT);
			DataTable tableLamViecNgayNghi = DAL.LayTableLamViecNgayNghi(iUserEnrollNumber, ngayBD, ngayKT);
			DataTable tableNgayLe = DAL.DocNgayLe(ngayBD, ngayKT);
			bool HaveHoliday = (tableNgayLe.Rows.Count > 0);


			DataRow[] arrCIO_A = new DataRow[tableCIO_A.Rows.Count]; tableCIO_A.Rows.CopyTo(arrCIO_A, 0);
			DataRow[] arrCIO_V = new DataRow[tableCIO_V.Rows.Count]; tableCIO_V.Rows.CopyTo(arrCIO_V, 0);
			DataRow[] arrVangg = new DataRow[tableVang.Rows.Count]; tableVang.Rows.CopyTo(arrVangg, 0);
			DataRow[] arrLVNN = new DataRow[tableLamViecNgayNghi.Rows.Count]; tableLamViecNgayNghi.Rows.CopyTo(arrLVNN, 0);

			//nhanvien.ClearAll();
			nhanvien.ArrayRowsToDSCheck_A(arrCIO_A);
			nhanvien.DS_CIO_A = nhanvien.GhepCIO_A(nhanvien.ds_CheckInn_A, nhanvien.ds_CheckOut_A);
			nhanvien.ArrayRowsToDS_CIO_V(arrCIO_V);
			nhanvien.XetCa(nhanvien.DS_CIO_A, nhanvien.DSCa, nhanvien.MacDinhTinhPC150);
			nhanvien.DSVaoRa = nhanvien.TronDS_CIO_A_V(nhanvien.DS_CIO_A, nhanvien.DS_CIO_V);
			nhanvien.DSNgayCong = nhanvien.TinhCongTheoNgay(nhanvien.DSVaoRa, ngayBD, ngayKT, nhanvien.MacDinhTinhPC150);
			nhanvien.ArrayRowsToDSVang(arrVangg);
			if (HaveHoliday) { nhanvien.KBNgayLe(tableNgayLe); }
			nhanvien.ArrayRowsToDSLamViecNgayNghi(arrLVNN);



		}

		public static void BUS_XacNhan(int iUserEnrollNumber, cShift newShift, bool pTinhPC150, int pSoPhutLamThem, cChkInOut CIO) {

			DateTime dOldTimeStrVao = CIO.Vao.TimeStr; DateTime dOldTimeStrRaa = CIO.Raa.TimeStr;
			string SourceInn = CIO.Vao.Source; string SourceOut = CIO.Raa.Source;
			int MachineNoInn = CIO.Vao.MachineNo;
			int MachineNoOut = CIO.Raa.MachineNo;
			int LateGraceTS = Convert.ToInt32(newShift.LateGraceTS.TotalMinutes);
			int EarlyGraceTS = Convert.ToInt32(newShift.EarlyGraceTS.TotalMinutes);
			int AfterOTTS = Convert.ToInt32(newShift.AfterOTTS.TotalMinutes);
			Single WorkingTimeTS = Convert.ToSingle(newShift.WorkingTimeTS.TotalMinutes);
			// nếu có làm thêm: lấy giờ vào - số phút trễ => TimeStrIn
			// TimeStr out = giờ ra + làm thêm

			DAL.XacNhanGio(iUserEnrollNumber, newShift.ShiftID, newShift.ShiftCode, newShift.OnnDutyTS.ToString(@"hh\:mm"), newShift.OffDutyTS.ToString(@"hh\:mm")
				, LateGraceTS, EarlyGraceTS, AfterOTTS, newShift.DayCount, WorkingTimeTS, newShift.Workingday, pSoPhutLamThem, pTinhPC150
				 , dOldTimeStrVao, dOldTimeStrRaa, SourceInn, SourceOut, MachineNoInn, MachineNoOut);


		}


		internal static void XuatBBCongVaPC(List<cUserInfo> m_DSNVXemCong, string file_path, DateTime ngayBD, DateTime ngayKT) {

			DateTime t1 = ngayBD.Date;
			DateTime t2 = ngayKT.Date;
			TimeSpan tmp = t2 - t1;
			int songay = tmp.Days - 1; // ngày 1->5 là trừ ra = 4, + 1 để ra số ngày, + thêm 2 để ra cột(UserEnrollNumber và số lượng)
			object misValue = System.Reflection.Missing.Value;

			using (ExcelPackage p = new ExcelPackage()) {
				#region //Create a sheet
				p.Workbook.Worksheets.Add("Bang Cham Cong");
				ExcelWorksheet ws = p.Workbook.Worksheets[1];
				ws.Name = "Bang Cham Cong"; //Setting Sheet's name
				ws.Cells.Style.Font.Size = 8; //Default font size for whole sheet
				ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

				ws.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				#endregion

				int sR = 1;
				//const int socotTruoc = 3; // chua 3 cot dau, neu vi tri bd thì phải cộng thêm 1. vd: 4
				const int socotTruoc = 4; // them cot chuc vu
				//const int socotSauuu = 7; // chua 7 cot dau, neu vi tri bd thì phải cộng thêm 1. vd: 4
				const int socotSauuu = 8; //them cot lễ

				int sttnv = 1, startCol = 1;
				#region 1.tieu de bang cham cong tu ngay den ngay 2. bỏ 1 dòng trống

				ws.Cells[sR, 1].Value = "Bảng chấm công từ ngày " + t1.AddDays(1d).ToString("dd/MM/yyyy") + " đến ngày " + t2.AddDays(-1d).ToString("dd/MM/yyyy");
				ws.Cells[sR, 1].Style.Font.Size = 14;
				ws.Cells[sR, 1, sR, songay + songay + socotTruoc + socotSauuu].Merge = true;
				ws.Cells[sR, 1, sR, songay + songay + socotTruoc + socotSauuu].Style.Font.Bold = true;
				ws.Cells[sR, 1, sR, songay + songay + socotTruoc + socotSauuu].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				sR = sR + 2;// sR+1 là dòng trống
				#endregion

				#region format 2 dòng 1.ngày , dòng dưới thứ là BOLD, rồi ghi STT, họ tên, ..., TĂNG COLUMN
				ws.Cells[sR, 1, sR + 1, songay + songay + socotTruoc + socotSauuu].Style.Font.Bold = true; // bold header cell 2 ô; //bỏ 2 ô sr+1,
				#endregion
				#region  write stt
				ws.Column(startCol).Width = 4;
				ws.Cells[sR, startCol].Value = "STT";
				ws.Cells[sR, startCol].Style.WrapText = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Merge = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				#endregion
				#region  write ho ten
				ws.Column(startCol + 1).Width = 18;
				ws.Cells[sR, startCol + 1].Value = "Họ tên";
				ws.Cells[sR, startCol + 1].Style.WrapText = true;
				ws.Cells[sR, startCol + 1, sR + 1, startCol + 1].Merge = true;
				ws.Cells[sR, startCol + 1, sR + 1, startCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				#endregion
				#region write macc
				ws.Column(startCol + 2).Width = 5;
				ws.Cells[sR, startCol + 2].Value = "Mã NV";
				ws.Cells[sR, startCol + 2].Style.WrapText = true;
				ws.Cells[sR, startCol + 2, sR + 1, startCol + 2].Merge = true;
				ws.Cells[sR, startCol + 2, sR + 1, startCol + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				#endregion
				#region write chức vụ
				ws.Column(startCol + 3).Width = 14;
				ws.Cells[sR, startCol + 3].Value = "Chức vụ";
				ws.Cells[sR, startCol + 3].Style.WrapText = true;
				ws.Cells[sR, startCol + 3, sR + 1, startCol + 3].Merge = true;
				ws.Cells[sR, startCol + 3, sR + 1, startCol + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				#endregion

				#region ghi TIEU DE ngay thang, TIEU DE cac cot thong ke, sau khi ghi thì nhớ tăng 2 sau dòng tiêu đề, xem cuối region này
				startCol = startCol + 4;// 4 LÀ SỐ CỘT STT, HO TEN, MACC, NẾU THÊM 1 CỘT THÌ +5, +6...

				for (DateTime dem = ngayBD.Date.AddDays(1d); dem <= ngayKT.Date.AddDays(-1d); dem = dem.AddDays(1d), startCol = startCol + 2) {
					ws.Cells[sR, startCol].Value = dem.ToString("d ");
					ws.Column(startCol).Width = 4;
					ws.Cells[sR, startCol, sR, startCol + 1].Merge = true;
					ws.Cells[sR, startCol, sR, startCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

					ws.Cells[sR + 1, startCol].Value = dem.ToString("ddd");
					ws.Column(startCol + 1).Width = 4;
					ws.Cells[sR + 1, startCol, sR + 1, startCol + 1].Merge = true;
					ws.Cells[sR + 1, startCol, sR + 1, startCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

				}
				// RA KHỎI VÒNG LẶP, START COL LÀ VỊ TRÍ CỘT TỔNG CÔNG, TIẾP TỤC TĂNG CỘT SAU MỖI LẦN GHI
				ws.Cells[sR, startCol, sR + 1, startCol].Value = "H,CT,PT";
				ws.Column(startCol).Width = 5;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.WrapText = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Merge = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				startCol++;

				ws.Cells[sR, startCol, sR + 1, startCol].Value = "P";
				ws.Column(startCol).Width = 5;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.WrapText = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Merge = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				startCol++;

				ws.Cells[sR, startCol, sR + 1, startCol].Value = "BH,TS";
				ws.Column(startCol).Width = 5;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.WrapText = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Merge = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				startCol++;

				ws.Cells[sR, startCol, sR + 1, startCol].Value = "Ro";
				ws.Column(startCol).Width = 5;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.WrapText = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Merge = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				startCol++;

				ws.Cells[sR, startCol, sR + 1, startCol].Value = "CV";
				ws.Column(startCol).Width = 5;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.WrapText = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Merge = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				startCol++;

				ws.Cells[sR, startCol, sR + 1, startCol].Value = "Lễ";
				ws.Column(startCol).Width = 5;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.WrapText = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Merge = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				startCol++;

				ws.Cells[sR, startCol, sR + 1, startCol].Value = "T.công";
				ws.Column(startCol).Width = 8;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.WrapText = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Merge = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				startCol++;

				ws.Cells[sR, startCol, sR + 1, startCol].Value = "T.PC";
				ws.Column(startCol).Width = 8;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.WrapText = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Merge = true;
				ws.Cells[sR, startCol, sR + 1, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				startCol++;

				//goto ab;

				sR = sR + 2; // 2 dòng tiêu đề stt, ngày, tổng công....
				#endregion
				var pb_c2 = (from item in m_DSNVXemCong
							 select new { item.PBCap2.ID, item.PBCap2.TenPhongBan }).Distinct().ToList();
				try {
					foreach (var pb_i in pb_c2) {
						#region //write ten phong ban, tang 1 dong
						ws.Cells[sR, 1, sR, songay + songay + socotTruoc + socotSauuu].Value = pb_i.TenPhongBan;
						ws.Cells[sR, 1, sR, songay + songay + socotTruoc + socotSauuu].Merge = true;
						ws.Cells[sR, 1, sR, songay + songay + socotTruoc + socotSauuu].Style.Font.Bold = true;
						ws.Cells[sR, 1, sR, songay + songay + socotTruoc + socotSauuu].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left; ;
						ws.Cells[sR, 1, sR, songay + songay + socotTruoc + socotSauuu].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //Ten
						#endregion
						sR = sR + 1;//chỉ số dòng mới, dòn 
						var pb_c1 = (from item in m_DSNVXemCong
									 where item.PBCap2.ID == pb_i.ID
									 select new { item.PBCap1.ID, item.PBCap1.TenPhongBan }).Distinct().ToList();

						foreach (var pb in pb_c1) {
							#region //write ten pb cap 1 tang 1 dong
							ws.Cells[sR, 1, sR, songay + songay + socotTruoc + socotSauuu].Value = pb.TenPhongBan;
							ws.Cells[sR, 1, sR, songay + songay + socotTruoc + socotSauuu].Merge = true;
							ws.Cells[sR, 1, sR, songay + songay + socotTruoc + socotSauuu].Style.Font.Bold = true;
							ws.Cells[sR, 1, sR, songay + songay + socotTruoc + socotSauuu].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
							ws.Cells[sR, 1, sR, songay + songay + socotTruoc + socotSauuu].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
							sR++;
							#endregion
							var dsnv = (from c in m_DSNVXemCong
										where c.PBCap1.ID == pb.ID
										select c).ToList();
							int iNgay = 0;
							foreach (cUserInfo nhanvien in dsnv) {
								// duyet tung nv, nhớ tăng sR
								startCol = 1;//reset startcol =1 mỗi lần ghi nv mới
								#region  stt,hoten,macc
								ws.Cells[sR, startCol].Value = sttnv; // stt start từ 1, nhớ +1 sau khi ghi vì đây là lặp for each
								sttnv++;
								ws.Cells[sR, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
								ws.Cells[sR, startCol + 1].Value = nhanvien.UserFullName; //Ten
								ws.Cells[sR, startCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //Ten
								ws.Cells[sR, startCol + 2].Value = nhanvien.UserFullCode;//MaNV
								ws.Cells[sR, startCol + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //MaNV
								ws.Cells[sR, startCol + 3].Value = nhanvien.TitleName;//MaNV
								ws.Cells[sR, startCol + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //chức vụ
								startCol = startCol + 4; // + 4 là 4 cột, nếu thêm 1 cột thì +5, +6
								#endregion

								#region write cong, pc

								Double tongcongthang = 0d, tongpc = 0d, tong_h_pt_ct = 0d, tongP = 0d, tongBH = 0d, tongRo = 0d, tongCV = 0d, tongLe = 0d;
								for (iNgay = 0; iNgay < nhanvien.DSNgayCong.Count; iNgay++) {// startCol += 2 chuyên vào trong hàm vì ngay đầu, ngày cuối bỏ qua nhưng nếu để đây thì vẫn tăng 2 --> 2 ô đầu + 2 ô cuối
									if (iNgay == 0 || iNgay == nhanvien.DSNgayCong.Count - 1) continue;

									cNgayCong ngayCong = nhanvien.DSNgayCong[iNgay];
									tongcongthang = tongcongthang + ngayCong.TongCong; // chỉ + công ngày nào có check auto hoặc veri, chưa cộng công vắng phép, học,họp,nên phải cộng
									tongpc = tongpc + ngayCong.TongPhuCap;
									if (ngayCong.DSVang != null) {
										foreach (var loaiVang in ngayCong.DSVang) {
											#region thống kê lại các loại vắng, chưa cộng vào công vì để cuối cùng rồi cộng
											switch (loaiVang.KyHieu) {
												case "P":
												case "NP":
													tongP = tongP + loaiVang.Cong;
													break;
												case "CV":
												case "NCV":
													tongCV = tongCV + loaiVang.Cong;
													break;
												case "BH":
												case "TS":
													tongBH = tongBH + loaiVang.Cong;
													break;
												case "PT":
												case "NPT":
												case "H":
												case "NH":
												case "CT":
												case "NCT":
													tong_h_pt_ct = tong_h_pt_ct + loaiVang.Cong;
													break;
												case "RO":
												case "NRO":
													tongRo = tongRo + loaiVang.Cong;
													break;
												case "L":
													tongLe = tongLe + loaiVang.Cong;
													break;
											}

											#endregion
										}
									}

									string content = (ngayCong.TongCong == 0d) ? "--" : ngayCong.TongCong.ToString("#0.##");
									ws.Cells[sR, startCol].Value = content;
									ws.Cells[sR, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
									content = (ngayCong.TongPhuCap == 0d) ? "--" : ngayCong.TongPhuCap.ToString("#0.##");
									ws.Cells[sR, startCol + 1].Value = content;
									ws.Cells[sR, startCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

									startCol += 2;
								}
								#endregion

								tongcongthang = tongcongthang + tong_h_pt_ct + tongP + tongLe;

								#region write thong ke
								ws.Cells[sR, startCol].Value = tong_h_pt_ct;// tổng HHPT
								ws.Cells[sR, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
								ws.Cells[sR, startCol].Style.Numberformat.Format = "#0.0#";

								ws.Cells[sR, startCol + 1].Value = tongP;// tổng phép
								ws.Cells[sR, startCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
								ws.Cells[sR, startCol + 1].Style.Numberformat.Format = "#0.0#";

								ws.Cells[sR, startCol + 2].Value = tongBH;// tổng BH,TS
								ws.Cells[sR, startCol + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
								ws.Cells[sR, startCol + 2].Style.Numberformat.Format = "#0.0#";

								ws.Cells[sR, startCol + 3].Value = tongRo;// tổng RO
								ws.Cells[sR, startCol + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
								ws.Cells[sR, startCol + 3].Style.Numberformat.Format = "#0.0#";

								ws.Cells[sR, startCol + 4].Value = tongCV;// tổng CV
								ws.Cells[sR, startCol + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
								ws.Cells[sR, startCol + 4].Style.Numberformat.Format = "#0.0#";

								ws.Cells[sR, startCol + 5].Value = tongLe;// tổng Lễ
								ws.Cells[sR, startCol + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
								ws.Cells[sR, startCol + 5].Style.Numberformat.Format = "#0.0#";

								ws.Cells[sR, startCol + 6].Value = tongcongthang;// tổng công tháng
								ws.Cells[sR, startCol + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
								ws.Cells[sR, startCol + 6].Style.Numberformat.Format = "#0.00";// tổng công tháng

								ws.Cells[sR, startCol + 7].Value = tongpc;// tổng PC tháng
								ws.Cells[sR, startCol + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
								ws.Cells[sR, startCol + 7].Style.Numberformat.Format = "#0.00";

								#endregion

								sR++;
							}
						}
					}
				} catch (Exception exception) {
					throw exception;
				}
				Byte[] bin = p.GetAsByteArray();
				try {
					File.WriteAllBytes(file_path, bin);
					AutoClosingMessageBox.Show("Xuất báo biểu thành công.", "Thông báo", 2000);
				} catch (Exception exception) {
					if (exception is UnauthorizedAccessException) MessageBox.Show("Bạn chưa được cấp quyền ghi file vào folder.", "Lỗi");
					else if (exception is DirectoryNotFoundException) MessageBox.Show("Không tìm thấy folder lưu trữ.", "Lỗi");
					else if (exception is IOException) MessageBox.Show("File đang mở bởi ứng dụng khác.", "Lỗi");
					else MessageBox.Show("Không thể ghi được file báo biểu.", "Lỗi");
				}

			}

		}

		public static void XuatBBTinhLuong(List<cUserInfo> m_DSNVXemCong, string file_path, DateTime ngayBD, DateTime ngayKT) {
			DateTime t1 = ngayBD.Date;
			DateTime t2 = ngayKT.Date;
			TimeSpan tmp = t2 - t1;
			int songay = tmp.Days - 1; // ngày 1->5 là trừ ra = 4, + 1 để ra số ngày, + thêm 2 để ra cột(UserEnrollNumber và số lượng)
			object misValue = System.Reflection.Missing.Value;

			using (ExcelPackage p = new ExcelPackage()) {
				#region //Create a sheet
				p.Workbook.Worksheets.Add("Bang Luong");
				ExcelWorksheet ws = p.Workbook.Worksheets[1];
				ws.Name = "Bang Luong"; //Setting Sheet's name
				ws.Cells.Style.Font.Size = 8; //Default font size for whole sheet
				ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

				ws.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				#endregion

				int sR = 1;
				//const int socotTruoc = 3; // chua 3 cot dau, neu vi tri bd thì phải cộng thêm 1. vd: 4
				const int socotTruoc = 4; // them cot chuc vu
				//const int socotSauuu = 7; // chua 7 cot dau, neu vi tri bd thì phải cộng thêm 1. vd: 4
				const int socotSauuu = 27; //them cot lễ
				//const int socotSauuu = 28;

				int sttnv = 1, startCol = 1;
				#region 1.tieu de bang cham cong tu ngay den ngay 2. bỏ 1 dòng trống

				DateTime thang = t2.AddMonths(-1); // vì ngày kết thúc là ngày đầu tiên của tháng sau nên -1 sẽ ra ngày đầu của tháng hiện tại
				ws.Cells[sR, 1].Value = "Bảng lương tháng " + thang.ToString("M/yyyy");
				ws.Cells[sR, 1].Style.Font.Size = 14;
				ws.Cells[sR, 1, sR, socotSauuu].Merge = true;//[tbd] tính trước xem có bao nhiêu cột
				ws.Cells[sR, 1, sR, socotSauuu].Style.Font.Bold = true;
				ws.Cells[sR, 1, sR, socotSauuu].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				sR = sR + 2;// sR+1 là dòng trống
				#endregion

				#region format 2 dòng BOLD, rồi ghi STT, họ tên, ..., TĂNG COLUMN
				ws.Cells[sR, 1, sR + 1, socotSauuu].Style.Font.Bold = true; // bold header cell 2 ô; //bỏ 2 ô sr+1,				
				#endregion

				int headerCol = startCol;
				#region stt, manv, ho ten +1 +2 +3--------------------------------------------------------
				#region  write stt +0
				ws.Column(headerCol).Width = 4;
				ws.Cells[sR, headerCol].Value = "STT";
				ws.Cells[sR, headerCol].Style.WrapText = true;//sR vì chỉ có 1 dòng header
				ws.Cells[sR, headerCol, sR + 1, headerCol].Merge = true;
				ws.Cells[sR, headerCol, sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region write manv +1
				ws.Column(headerCol).Width = 5;
				ws.Cells[sR, headerCol].Value = "Mã NV";
				ws.Cells[sR, headerCol].Style.WrapText = true;
				ws.Cells[sR, headerCol, sR + 1, headerCol].Merge = true;
				ws.Cells[sR, headerCol, sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region  write ho ten +2
				ws.Column(headerCol).Width = 18;
				ws.Cells[sR, headerCol].Value = "Họ tên";
				ws.Cells[sR, headerCol].Style.WrapText = true;
				ws.Cells[sR, headerCol, sR + 1, headerCol].Merge = true;
				ws.Cells[sR, headerCol, sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#endregion -----------------------------------------------------------------------
				#region HSLương +3 +4 ---------------------------------------------------------
				#region write HSLương ở trên
				ws.Cells[sR, headerCol].Value = "Hệ số lương";
				ws.Cells[sR, headerCol].Style.WrapText = true;
				ws.Cells[sR, headerCol, sR, headerCol + 1].Merge = true;
				ws.Cells[sR, headerCol, sR, headerCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				#endregion
				#region write HeSoLCB +3
				ws.Column(headerCol).Width = 5;
				ws.Cells[sR + 1, headerCol].Value = "CB";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region write HeSoSP +4
				ws.Column(headerCol).Width = 5;
				ws.Cells[sR + 1, headerCol].Value = "SP";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#endregion
				#region ngày công chuẩn +5 +6 +7 +8 +9---------------------------------------------
				#region write ngày công chuẩn ở trên
				ws.Cells[sR, headerCol].Value = "Ngày công chuẩn";
				ws.Cells[sR, headerCol].Style.WrapText = true;
				ws.Cells[sR, headerCol, sR, headerCol + 4].Merge = true;
				ws.Cells[sR, headerCol, sR, headerCol + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				#endregion

				#region write Ban ngày,trực tết,pccc(PCCQ chi) +5
				ws.Column(headerCol).Width = 5;
				ws.Cells[sR + 1, headerCol].Value = "Ban ngày,trực tết,pccc(PCCQ chi)";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion

				#region write Phép +6
				ws.Column(headerCol).Width = 5;
				ws.Cells[sR + 1, headerCol].Value = "Phép";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion

				#region write Học, họp, PT, lễ +7
				ws.Column(headerCol).Width = 5;
				ws.Cells[sR + 1, headerCol].Value = "Học, họp, PT, lễ";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region write trực đêm CV +8
				ws.Column(headerCol).Width = 5;
				ws.Cells[sR + 1, headerCol].Value = "Trực đêm";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region write Chờ việc +9
				ws.Column(headerCol).Width = 5;
				ws.Cells[sR + 1, headerCol].Value = "Chờ việc";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#endregion  ----------------------------------------------------------------------
				#region làm thêm giờ +10 +11 +12 +13------------------------------------------------
				#region write làm thêm giờ ở trên
				ws.Cells[sR, headerCol].Value = "Làm thêm giờ";
				ws.Cells[sR, headerCol].Style.WrapText = true;
				ws.Cells[sR, headerCol, sR, headerCol + 3].Merge = true;
				ws.Cells[sR, headerCol, sR, headerCol + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				#endregion
				#region write 150% +10
				ws.Column(headerCol).Width = 5;
				ws.Cells[sR + 1, headerCol].Value = "150%";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region write 200% +11
				ws.Column(headerCol).Width = 5;
				ws.Cells[sR + 1, headerCol].Value = "200%,Lễ,CN";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region write 260% +12
				ws.Column(headerCol).Width = 5;
				ws.Cells[sR + 1, headerCol].Value = "260%";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region write tổng công tháng +13
				ws.Column(headerCol).Width = 5;
				ws.Cells[sR + 1, headerCol].Value = "Tổng số công trong tháng";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#endregion ------------------------------------------------------------------------------------
				#region tiền lương +14 +15 +16 +17 ---------------------------------------------------------------------------------
				#region write tiền lương ở trên
				ws.Cells[sR, headerCol].Value = "Tiền lương";
				ws.Cells[sR, headerCol].Style.WrapText = true;
				ws.Cells[sR, headerCol, sR, headerCol + 3].Merge = true;
				ws.Cells[sR, headerCol, sR, headerCol + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				#endregion

				#region write Luong CB +14
				ws.Column(headerCol).Width = 12;
				ws.Cells[sR + 1, headerCol].Value = "Lương CB";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region write Luong CV +15
				ws.Column(headerCol).Width = 12;
				ws.Cells[sR + 1, headerCol].Value = "Lương SP";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region write điều chỉnh lương tháng trước + 16
				ws.Column(headerCol).Width = 12;
				ws.Cells[sR + 1, headerCol].Value = "Điều chỉnh lương tháng trước";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region write tổng lương + 17
				ws.Column(headerCol).Width = 12;
				ws.Cells[sR + 1, headerCol].Value = "Tổng lương";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#endregion ----------------------------------------------------------------------------------------------
				#region phụ cấp lương +18 +19 +20 +21------------------------------------------------------------------------
				#region write phụ cấp lương ở trên
				ws.Cells[sR, headerCol].Value = "Phụ cấp lương";
				ws.Cells[sR, headerCol].Style.WrapText = true;
				ws.Cells[sR, headerCol, sR, headerCol + 3].Merge = true;
				ws.Cells[sR, headerCol, sR, headerCol + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				#endregion
				#region write phụ cấp theo Luong CB +18
				ws.Column(headerCol).Width = 12;
				ws.Cells[sR + 1, headerCol].Value = "PC theo Lương CB";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region write phụ cấp theo Luong SP +19
				ws.Column(headerCol).Width = 12;
				ws.Cells[sR + 1, headerCol].Value = "PC thep Lương SP";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region write bồi dưỡng ca 3 +20
				ws.Column(headerCol).Width = 12;
				ws.Cells[sR + 1, headerCol].Value = "Bồi dưỡng ca 3";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region write tổng lương +21
				ws.Column(headerCol).Width = 12;
				ws.Cells[sR + 1, headerCol].Value = "Tổng PC";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#endregion ------------------------------------------------------------------------
				#region  write tổng lương và phụ cấp +22
				ws.Column(headerCol).Width = 12;
				ws.Cells[sR, headerCol].Value = "Tổng số lương và PC";
				ws.Cells[sR, headerCol].Style.WrapText = true;//sR vì chỉ có 1 dòng header
				ws.Cells[sR, headerCol, sR + 1, headerCol].Merge = true;
				ws.Cells[sR, headerCol, sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region khấu trừ  +23 +24 +25 ------------------------------------------------------------------------
				#region write khấu trừ ở trên
				ws.Cells[sR, headerCol].Value = "Khấu trừ";
				ws.Cells[sR, headerCol].Style.WrapText = true;
				ws.Cells[sR, headerCol, sR, headerCol + 2].Merge = true;
				ws.Cells[sR, headerCol, sR, headerCol + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				#endregion
				#region write tạm ứng + 23
				ws.Column(headerCol).Width = 12;
				ws.Cells[sR + 1, headerCol].Value = "Tạm ứng";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion

				#region write khấu trừ y tế, bhxh + 24
				ws.Column(headerCol).Width = 12;
				ws.Cells[sR + 1, headerCol].Value = "Khấu trừ BHXH, YT, TN";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region write khấu trừ y tế, bhxh + 25
				ws.Column(headerCol).Width = 12;
				ws.Cells[sR + 1, headerCol].Value = "Thu chi khác";
				ws.Cells[sR + 1, headerCol].Style.WrapText = true;
				ws.Cells[sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#endregion
				#region  write thực lãnh +26
				ws.Column(headerCol).Width = 12;
				ws.Cells[sR, headerCol].Value = "Thực lãnh";
				ws.Cells[sR, headerCol].Style.WrapText = true;//sR vì chỉ có 1 dòng header
				ws.Cells[sR, headerCol, sR + 1, headerCol].Merge = true;
				ws.Cells[sR, headerCol, sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion
				#region  write ký nhận +27
				ws.Column(headerCol).Width = 4;
				ws.Cells[sR, headerCol].Value = "Ký nhận";
				ws.Cells[sR, headerCol].Style.WrapText = true;//sR vì chỉ có 1 dòng header
				ws.Cells[sR, headerCol, sR + 1, headerCol].Merge = true;
				ws.Cells[sR, headerCol, sR + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;
				#endregion




				#region ghi TIEU DE ngay thang, TIEU DE cac cot thong ke, sau khi ghi thì nhớ tăng 2 sau dòng tiêu đề, xem cuối region này

				startCol = startCol + 17; // xem [140507_1]
				//goto ab;

				sR = sR + 2; // 2 dòng tiêu đề stt, ngày, tổng công....
				#endregion
				try {
					var pb_c1 = (from item in m_DSNVXemCong
								 select new { item.PBCap1.ID, item.PBCap1.TenPhongBan }).Distinct().ToList();

					foreach (var pb in pb_c1) {
						#region //write ten pb cap 1 tang 1 dong
						ws.Cells[sR, 1, sR, startCol - 1].Value = pb.TenPhongBan;
						ws.Cells[sR, 1, sR, startCol - 1].Merge = true;
						ws.Cells[sR, 1, sR, startCol - 1].Style.Font.Bold = true;
						ws.Cells[sR, 1, sR, startCol - 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
						ws.Cells[sR, 1, sR, startCol - 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
						sR++;
						#endregion
						var dsnv = (from c in m_DSNVXemCong
									where c.PBCap1.ID == pb.ID
									select c).ToList();
						foreach (cUserInfo nhanvien in dsnv) {
							// duyet tung nv, nhớ tăng sR
							startCol = 1;//reset startcol =1 mỗi lần ghi nv mới [140507_1]
							#region stt. manv, ho ten, hsluong cb, heso luong sp
							ws.Cells[sR, startCol].Value = sttnv; // stt start từ 1, nhớ +1 sau khi ghi vì đây là lặp for each
							sttnv++;
							ws.Cells[sR, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
							ws.Cells[sR, startCol + 1].Value = nhanvien.UserFullCode;//MaNV
							ws.Cells[sR, startCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //MaNV
							ws.Cells[sR, startCol + 2].Value = nhanvien.UserFullName; //Ten
							ws.Cells[sR, startCol + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //Ten
							ws.Cells[sR, startCol + 3].Value = nhanvien.HeSo.LuongCB;//hslcb
							ws.Cells[sR, startCol + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 4].Value = nhanvien.HeSo.LuongCV;//hslcv
							ws.Cells[sR, startCol + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							#endregion
							#region ngày công chuẩn  ban ngày +5, phép +6, học +7, trực đêm +8, chờ việc +9
							ws.Cells[sR, startCol + 5].Value = nhanvien.TongCongThang;//tổng công [tbd] xem lại tổng công tháng này phải bao gồm các công phép, h, lễ....
							ws.Cells[sR, startCol + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 5].Style.Numberformat.Format = "#0.0#";
							ws.Cells[sR, startCol + 6].Value = nhanvien.TongCongPhep;//
							ws.Cells[sR, startCol + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 6].Style.Numberformat.Format = "#0.0";
							ws.Cells[sR, startCol + 7].Value = nhanvien.TongCongH_CT_PT;//
							ws.Cells[sR, startCol + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 7].Style.Numberformat.Format = "#0.0";
							ws.Cells[sR, startCol + 8].Value = nhanvien.TongNgayQuaDem;// 
							ws.Cells[sR, startCol + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 8].Style.Numberformat.Format = "#0";
							ws.Cells[sR, startCol + 9].Value = nhanvien.TongCongCV;// 
							ws.Cells[sR, startCol + 9].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 9].Style.Numberformat.Format = "#0.0#";
							#endregion 

							#region làm thêm giờ 150% +10, 200% +11, 260% +12, tổng PC quy đổi ra công +13
							ws.Cells[sR, startCol + 10].Value = "--";
							ws.Cells[sR, startCol + 10].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 10].Style.Numberformat.Format = "##,###,###,##0.000";
							ws.Cells[sR, startCol + 11].Value = "--";
							ws.Cells[sR, startCol + 11].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 11].Style.Numberformat.Format = "##,###,###,##0.000";
							ws.Cells[sR, startCol + 12].Value = "--";
							ws.Cells[sR, startCol + 12].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 12].Style.Numberformat.Format = "##,###,###,##0.000";
							ws.Cells[sR, startCol + 13].Value = nhanvien.TongPCapThang;//
							ws.Cells[sR, startCol + 13].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 13].Style.Numberformat.Format = "#0.0#";
							#endregion 

							#region tiền lương +14 lương cb ; +15 lương sp; +16 lương tháng trước; +17 tổng lương theo công
							ws.Cells[sR, startCol + 14].Value = nhanvien.Luong.LuongCB_TheoCong;//
							ws.Cells[sR, startCol + 14].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 14].Style.Numberformat.Format = "##,###,###,##0.000";
							ws.Cells[sR, startCol + 15].Value = nhanvien.Luong.LuongSP_TheoCong;//
							ws.Cells[sR, startCol + 15].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 15].Style.Numberformat.Format = "##,###,###,##0.000";
							ws.Cells[sR, startCol + 16].Value = nhanvien.Luong.LuongThangTruoc;//
							ws.Cells[sR, startCol + 16].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 16].Style.Numberformat.Format = "##,###,###,##0.000";
							ws.Cells[sR, startCol + 17].Value = nhanvien.Luong.TongLuong_TheoCong;//
							ws.Cells[sR, startCol + 17].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 17].Style.Numberformat.Format = "##,###,###,##0.000";
							#endregion 
							#region tiền lương +18 lương cb ; +19 lương sp; +20 lương tháng trước; +21 tổng lương theo công
							ws.Cells[sR, startCol + 18].Value = nhanvien.Luong.LuongCB_TheoPCap;//
							ws.Cells[sR, startCol + 18].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 18].Style.Numberformat.Format = "##,###,###,##0.000";
							ws.Cells[sR, startCol + 19].Value = nhanvien.Luong.LuongSP_TheoPCap;//
							ws.Cells[sR, startCol + 19].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 19].Style.Numberformat.Format = "##,###,###,##0.000";
							ws.Cells[sR, startCol + 20].Value = nhanvien.Luong.BoiDuongQuaDem;//
							ws.Cells[sR, startCol + 20].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 20].Style.Numberformat.Format = "##,###,###,##0.000";
							ws.Cells[sR, startCol + 21].Value = nhanvien.Luong.TongLuong_TheoPCap;//
							ws.Cells[sR, startCol + 21].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 21].Style.Numberformat.Format = "##,###,###,##0.000";
							#endregion 
							#region  +22 tổng lương và pc 
							ws.Cells[sR, startCol + 22].Value = nhanvien.Luong.TongLuong;//
							ws.Cells[sR, startCol + 22].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 22].Style.Numberformat.Format = "##,###,###,##0.000";
							#endregion 
							#region khấu trừ  +23 tạm ứng; +24 khấu trừ BH; +25 thu chi khác
							ws.Cells[sR, startCol + 23].Value = nhanvien.Luong.TamUng;//
							ws.Cells[sR, startCol + 23].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 23].Style.Numberformat.Format = "##,###,###,##0.000";
							ws.Cells[sR, startCol + 24].Value = nhanvien.Luong.KhauTruBH;//
							ws.Cells[sR, startCol + 24].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 24].Style.Numberformat.Format = "##,###,###,##0.000";
							ws.Cells[sR, startCol + 25].Value = "--";//
							ws.Cells[sR, startCol + 25].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 25].Style.Numberformat.Format = "##,###,###,##0.000";
							#endregion 
							#region  +26 thực lãnh
							ws.Cells[sR, startCol + 26].Value = nhanvien.Luong.ThucLanh;//
							ws.Cells[sR, startCol + 26].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							ws.Cells[sR, startCol + 26].Style.Numberformat.Format = "##,###,###,##0.000";
							#endregion 
							#region +27 ký nhận
							ws.Cells[sR, startCol + 27].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //
							#endregion 
							//tổng cộng 17 cột
							startCol = startCol + 27; // + 4 là 4 cột, nếu thêm 1 cột thì +5, +6

							sR++;

						}

					}
				} catch (Exception exception) {
					throw exception;
				}
				Byte[] bin = p.GetAsByteArray();
				try {
					File.WriteAllBytes(file_path, bin);
					AutoClosingMessageBox.Show("Xuất báo biểu thành công.", "Thông báo", 2000);
				} catch (Exception exception) {
					if (exception is UnauthorizedAccessException) MessageBox.Show("Bạn chưa được cấp quyền ghi file vào folder.", "Lỗi");
					else if (exception is DirectoryNotFoundException) MessageBox.Show("Không tìm thấy folder lưu trữ.", "Lỗi");
					else if (exception is IOException) MessageBox.Show("File đang mở bởi ứng dụng khác.", "Lỗi");
					else MessageBox.Show("Không thể ghi được file báo biểu.", "Lỗi");
				}

			}

		}

		private static DataTable TaoCauTrucDataTableBaoBieu(int socot) {
			DataTable kq = new DataTable();
			kq.Columns.Add("UserFullName", typeof(string)); //1
			kq.Columns.Add("UserEnrollNumber", typeof(int)); //0
			kq.Columns.Add("UserIDTitle", typeof(int));


			for (int i = 0; i < socot; i++) {
				kq.Columns.Add("cong" + (i + 1), typeof(string));
				kq.Columns.Add("phucap" + (i + 1), typeof(string));
			}
			kq.Columns.Add("Cong", typeof(float)); //20
			kq.Columns.Add("ShiftCode", typeof(string)); //21
			kq.Columns.Add("PhuCap", typeof(float));//22
			kq.Columns.Add("TongGioLam", typeof(float));
			kq.Columns.Add("TongGioThuc", typeof(float));
			kq.Columns.Add("obj", typeof(cNgayCong));
			kq.Columns.Add("IDD_1", typeof(int));
			kq.Columns.Add("Description_1", typeof(string));
			kq.Columns.Add("IDD_2", typeof(int));
			kq.Columns.Add("Description_2", typeof(string));
			return kq;
		}


		internal static void DocHSBHCongThem(List<cUserInfo> dsnv) {
			DataTable table = DAL.DocHSBHCongThem();
			// neu ko co du lieu thi thoat
			if (table == null || table.Rows.Count == 0) return;
			foreach (DataRow row in table.Rows) {
				int iUserEnrollNumber = (int)row["UserEnrollNumber"];
				Single hesocongthem = (row["HSBHCongThem"] != DBNull.Value) ? (Single)row["HSBHCongThem"] : 0f;
				cUserInfo nhanvien = dsnv.Find(o => o.UserEnrollNumber == iUserEnrollNumber);
				if (nhanvien == null) continue; // ko tìm thay nv thi tiep tuc
				// tim thay thi them vao
				nhanvien.HeSo.BHXH_YT_TN = nhanvien.HeSo.BHXH_YT_TN + hesocongthem;
			}
		}

		internal static void TinhKhauTruBHChoNV(List<cUserInfo> dsnv, Double mucluongtoithieu, Double mucdong) {
			foreach (cUserInfo nhanvien in dsnv) {
				nhanvien.Luong.KhauTruBH = nhanvien.HeSo.BHXH_YT_TN * mucluongtoithieu * (mucdong / 100d);
			}
		}
	}
}
