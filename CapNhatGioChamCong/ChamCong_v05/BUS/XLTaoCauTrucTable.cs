using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ChamCong_v05.DTO;
using ChamCong_v05.Helper;

namespace ChamCong_v05.BUS
{
	public static partial class XL
	{
		#region tao cau truc datatable
		public static DataTable TaoCauTrucDataTable(string[] colName, Type[] colType)
		{
			var kq = new DataTable();
			for (var i = 0; i < colName.Length; i++)
			{
				kq.Columns.Add(colName[i], colType[i]);
			}
			return kq;
		}

		#endregion
		#region tạo table dsnv, table xem công, điểm danh, giờ thiếu chấm công, giờ ko nhận diện được, xác nhận ca và tăng ca

		public static void TaoTableDSNV(List<cUserInfo> m_DSNV, DataTable m_tableDSNV)
		{
			m_tableDSNV.Rows.Clear();

			foreach (cUserInfo nhanvien in m_DSNV)
			{
				var row = m_tableDSNV.NewRow();
				row["check"] = false;
				row["cUserInfo"] = nhanvien;
				row["UserEnrollNumber"] = nhanvien.MaCC;
				row["UserFullCode"] = nhanvien.MaNV;
				row["UserFullName"] = nhanvien.TenNV;
				row["SchID"] = nhanvien.LichTrinhLV.SchID;
				row["SchName"] = nhanvien.LichTrinhLV.TenLichTrinh;
				row["cUserInfo"] = nhanvien;

				m_tableDSNV.Rows.Add(row);
			}
		}

		public static void TaoTableDSNV_ThayDoiThongTin(List<cUserInfo> m_DSNV, DataTable m_tableDSNV)
		{
			if (m_DSNV == null || m_DSNV.Count == 0) return;

			foreach (cUserInfo nhanvien in m_DSNV)
			{
				var row = m_tableDSNV.NewRow();
				row["check"] = false;
				row["cUserInfo"] = nhanvien;
				row["UserEnrollNumber"] = nhanvien.MaCC;
				row["UserFullCode"] = nhanvien.MaNV;
				row["UserFullName"] = nhanvien.TenNV;
				row["SchID"] = nhanvien.LichTrinhLV.SchID;
				row["SchName"] = nhanvien.LichTrinhLV.TenLichTrinh;
				row["ChucVu"] = nhanvien.ChucVu;
				row["MaPhong"] = nhanvien.PhongBan.ID;
				row["TenPhong"] = nhanvien.PhongBan.Ten;
				row["HeSoLuongCB"] = nhanvien.HeSo.LuongCB;
				row["HeSoLuongSP"] = nhanvien.HeSo.LuongCV;
				row["HSBHCongThem"] = nhanvien.HeSo.BHCongThem_ChoGD_PGD;
				row["UserEnabled"] = nhanvien.IsUserEnabled;
				m_tableDSNV.Rows.Add(row);
			}
		}

		public static void TaoTableDSNV_PhucHoi(List<cUserInfo> m_DSNV, DataTable m_tableDSNV)
		{
			if (m_DSNV == null || m_DSNV.Count == 0) return;

			foreach (cUserInfo nhanvien in m_DSNV)
			{
				var row = m_tableDSNV.NewRow();
				row["check"] = false;
				row["cUserInfo"] = nhanvien;
				row["UserEnrollNumber"] = nhanvien.MaCC;
				row["UserFullCode"] = nhanvien.MaNV;
				row["UserFullName"] = nhanvien.TenNV;
				row["ChucVu"] = nhanvien.ChucVu;
				row["MaPhong"] = nhanvien.PhongBan.ID;
				row["TenPhong"] = nhanvien.PhongBan.Ten;
				row["HeSoLuongCB"] = nhanvien.HeSo.LuongCB;
				row["HeSoLuongSP"] = nhanvien.HeSo.LuongCV;
				row["HSBHCongThem"] = nhanvien.HeSo.BHCongThem_ChoGD_PGD;
				m_tableDSNV.Rows.Add(row);
			}
		}

		public static void TaoTableDiemDanh(List<cUserInfo> dsnv, DataTable kq, DateTime dateDiemDanh, out int SoNVDangLamViec, out int SoNVDaRaVe, out int SoNV_KoHienDien, out int SoNVVang_LyDo, out int SoNVNghi)
		{
			SoNVDangLamViec = 0;
			SoNVDaRaVe = 0;
			SoNV_KoHienDien = 0;
			SoNVNghi = 0;
			SoNVVang_LyDo = 0;

			foreach (var nhanvien in dsnv)
			{
				var row = kq.NewRow();
				row["UserEnrollNumber"] = nhanvien.MaCC;
				row["UserFullCode"] = nhanvien.MaNV;
				row["UserFullName"] = nhanvien.TenNV;
				var ngayCong = nhanvien.DSNgayCong[2];
				//nếu có check thì khỏi ghi vắng
				var ChuoiTrangThai = String.Empty;
				for (var i = 0; i < ngayCong.DSVaoRa.Count; i++)
				{
					if (i >= 3) break;
					var CIO = ngayCong.DSVaoRa[i];
					row["TimeStrVao" + (i + 1)] = (CIO.Vao != null) ? CIO.Vao.Time : (object)DBNull.Value;
					row["TimeStrRaa" + (i + 1)] = (CIO.Raa != null) ? CIO.Raa.Time : (object)DBNull.Value;
				}
				if (ngayCong.TrangThaiDiemDanh == TrangThaiDiemDanh.DANGLAMVIEC)
				{
					ChuoiTrangThai = "Đang làm việc";
					SoNVDangLamViec++;
				}
				else if (ngayCong.TrangThaiDiemDanh == TrangThaiDiemDanh.VANG_LYDO)
				{
					ChuoiTrangThai = "Vắng (" + ngayCong.Absents_Code() + ")";
					SoNV_KoHienDien++;
					SoNVVang_LyDo++;
				}
				else if (ngayCong.TrangThaiDiemDanh == TrangThaiDiemDanh.VANG_NGHI)
				{
					ChuoiTrangThai = "Nghỉ";
					SoNV_KoHienDien++;
					SoNVNghi++;
				}
				else if (ngayCong.TrangThaiDiemDanh == TrangThaiDiemDanh.DARAVE_THIEUCC)
				{
					ChuoiTrangThai = "Đã về (thiếu chấm công)";
					SoNVDaRaVe++;
				}
				else if (ngayCong.TrangThaiDiemDanh == TrangThaiDiemDanh.DARAVE)
				{
					ChuoiTrangThai = "Đã về";
					SoNVDaRaVe++;
				}
				row["Ca"] = ngayCong.CIOs_Absents_Code_Full();
				row["TrangThai"] = ChuoiTrangThai;
				kq.Rows.Add(row);
			}
		}

		public static void TaoTableXemCong(List<cUserInfo> dsnv, DataTable kq)
		{
			kq.Rows.Clear();
			foreach (var nhanvien in dsnv)
			{
				//duyệt qua từng ngày công của nhân viên để fill datarow
				for (var x = 2; x < nhanvien.DSNgayCong.Count - 2; x++)
				{
					//[CHÚ Ý] ở đây chỉ chạy từ 1-> kế cuối
					var ngayCong = nhanvien.DSNgayCong[x];
					var row = kq.NewRow();
					row["cNgayCong"] = ngayCong;
					row["cUserInfo"] = nhanvien;
					row["UserEnrollNumber"] = nhanvien.MaCC;
					row["UserFullName"] = nhanvien.TenNV;
					row["UserFullCode"] = nhanvien.MaNV;
					row["TimeStrNgay"] = row["TimeStrThu"] = ngayCong.Ngay;
					row["TongGioThucTe"] = ngayCong.TG5.TongGioThucTe;
					row["TongGioLamViec"] = ngayCong.TG5.TongGioLamViec;
					row["TongGioLamDem"] = ngayCong.TG5.TongGioLamDem;
					row["TongGioLamNgay"] = ngayCong.TG5.TongGioLamNgay;
					row["TongGioTangCuong"] = ngayCong.TG5.TongGioTangCuong;
					row["GioLamNgay_KoTC"] = ngayCong.TG5.GioLamNgay_KoTC;
					row["TongGioTangCuong"] = ngayCong.TG5.TongGioTangCuong;
					row["GioLamNgay_KoTC"] = ngayCong.TG5.GioLamNgay_KoTC;
					row["HuongPC_TangCuongNgay"] = ngayCong.TG5.HuongPC_TangCuongNgay;
					row["HuongPC_TangCuongDem"] = ngayCong.TG5.HuongPC_TangCuongDem;
					row["HuongPC_Dem"] = ngayCong.TG5.HuongPC_Dem;
					row["VaoTre"] = ngayCong.TG5.VaoTre;
					row["RaaSom"] = ngayCong.TG5.RaaSom;

					if (ngayCong.DSVaoRa.Count == 0 && ngayCong.DSVang.Count == 0)
						row["ShiftCode"] = "-";
					else
					{
						if (ngayCong.DSVaoRa.Count > 0)
						{
							var i = 1;
							foreach (var CIO in ngayCong.DSVaoRa.TakeWhile(CIO => i <= 3))
							{
								row["cCheckInOut" + i] = CIO;
								switch (CIO.HaveINOUT)
								{
									case -1:
										row["TimeStrVao" + i] = CIO.Vao.Time;
										break;
									case -2:
										row["TimeStrRaa" + i] = CIO.Raa.Time;
										break;
									case 0:
									case 30:
										row["TimeStrVao" + i] = CIO.Vao.Time;
										row["TimeStrRaa" + i] = CIO.Raa.Time;
										break;
								}
								i++;
							}
						}
						row["ShiftCode"] = ngayCong.CIOs_Absents_Code_Comp();
					}
					kq.Rows.Add(row);
				}
			}
		}


		public static void TaoTableGioKDQD(List<cUserInfo> dsnv, DataTable kq)
		{
			kq.Rows.Clear();
			foreach (var nhanvien in dsnv)
			{
				//duyệt qua từng ngày công của nhân viên để fill datarow
				for (var x = 2; x < nhanvien.DSNgayCong.Count - 2; x++)
				{
					//[CHÚ Ý] ở đây chỉ chạy từ 1-> kế cuối
					var ngayCong = nhanvien.DSNgayCong[x];
					// điều kiện: 1. đủ IO, 2. chưa xác nhận, 3. ca tự do
					if (ngayCong.DSVaoRa.Exists(o => o.HaveINOUT == 0 && o.DaXN == false && o.ThuocCa.ID < 0))
					{
						var row = kq.NewRow();
						row["cNgayCong"] = ngayCong;
						row["cUserInfo"] = nhanvien;
						row["UserEnrollNumber"] = nhanvien.MaCC;
						row["UserFullName"] = nhanvien.TenNV;
						row["UserFullCode"] = nhanvien.MaNV;
						row["TimeStrNgay"] = row["TimeStrThu"] = ngayCong.Ngay;
						row["TongGioLam"] = ngayCong.TG.GioLamViec5;//Danglam giophut
						row["TongGioThuc"] = ngayCong.TG.GioThucTe5;//Danglam giophut
						row["TongCong"] = ngayCong.TongCong;
						row["TongPhuCap"] = ngayCong.PhuCaps._TongPC;
						row["IsEdited"] = false;
						var i = 1;
						foreach (var CIO in ngayCong.DSVaoRa)
						{
							if (i > 3) break;
							row["cCheckInOut" + i] = CIO;
							switch (CIO.HaveINOUT)
							{
								case -1:
									row["TimeStrVao" + i] = CIO.Vao.Time;
									break;
								case -2:
									row["TimeStrRaa" + i] = CIO.Raa.Time;
									break;
								case 0:
								case 30:
									row["TimeStrVao" + i] = CIO.Vao.Time;
									row["TimeStrRaa" + i] = CIO.Raa.Time;
									break;
							}
							i++;
						}
						row["ShiftCode"] = ngayCong.CIOs_Absents_Code_Comp();
						kq.Rows.Add(row);
					}
				}
			}
		}


		public static void TaoTableGioThieuCheck(List<cUserInfo> dsnv, DataTable kq)
		{
			kq.Rows.Clear();

			foreach (var nhanvien in dsnv)
			{
				//duyệt qua từng ngày công của nhân viên để fill datarow
				for (var x = 2; x < nhanvien.DSNgayCong.Count - 2; x++)
				{
					//[CHÚ Ý] ở đây chỉ chạy từ 1-> kế cuối
					var ngayCong = nhanvien.DSNgayCong[x];
					if (ngayCong.DSVaoRa.Count == 0 || ngayCong.DSVaoRa.Any(o=>o.HaveINOUT < 0) == false) continue; // nếu ko có giờ check KDQD thì bỏ qua
					// tồn tại 
					var row = kq.NewRow();
					row["cNgayCong"] = ngayCong;
					row["cUserInfo"] = nhanvien;
					row["UserEnrollNumber"] = nhanvien.MaCC;
					row["UserFullName"] = nhanvien.TenNV;
					row["UserFullCode"] = nhanvien.MaNV;
					row["TimeStrNgay"] = row["TimeStrThu"] = ngayCong.Ngay;
					row["TongGioLam"] = ngayCong.TG.GioLamViec5;//Danglam giophut
					row["TongGioThuc"] = ngayCong.TG.GioThucTe5;//Danglam giophut
					row["TongCong"] = ngayCong.TongCong;
					row["TongPhuCap"] = ngayCong.PhuCaps._TongPC;
					row["IsEdited"] = false;
					var i = 1;
					foreach (var CIO in ngayCong.DSVaoRa)
					{
						if (i > 3) break;
						row["cCheckInOut" + i] = CIO;
						switch (CIO.HaveINOUT)
						{
							case -1:
								row["TimeStrVao" + i] = CIO.Vao.Time;
								break;
							case -2:
								row["TimeStrRaa" + i] = CIO.Raa.Time;
								break;
							case 0:
							case 30:
								row["TimeStrVao" + i] = CIO.Vao.Time;
								row["TimeStrRaa" + i] = CIO.Raa.Time;
								break;
						}
						i++;
					}
					row["ShiftCode"] = ngayCong.CIOs_Absents_Code_Comp();
					kq.Rows.Add(row);
				}
			}

		}

		public static void TaoTableThK_TreSom(List<cUserInfo> dsnvDiemDanh, DataTable kq)
		{
			kq.Rows.Clear();

			foreach (cUserInfo nhanvien in dsnvDiemDanh)
			{
				//duyệt qua từng ngày công của nhân viên để fill datarow
				for (var x = 2; x < nhanvien.DSNgayCong.Count - 2; x++)
				{
					//[CHÚ Ý] ở đây chỉ chạy từ 1-> kế cuối
					var ngayCong = nhanvien.DSNgayCong[x];
					// đi trễ, về sớm, dù có cho phép vẫn báo
					if (ngayCong.TG.VaoTre.TotalMinutes < 1f && ngayCong.TG.RaaSom.TotalMinutes < 1f) continue; // nếu ko có giờ check KDQD thì bỏ qua
					// tồn tại 
					var row = kq.NewRow();
					row["cNgayCong"] = ngayCong;
					row["cUserInfo"] = nhanvien;
					row["UserEnrollNumber"] = nhanvien.MaCC;
					row["UserFullName"] = nhanvien.TenNV;
					row["UserFullCode"] = nhanvien.MaNV;
					row["TimeStrNgay"] = row["TimeStrThu"] = ngayCong.Ngay;
					row["TongGioLam"] = ngayCong.TG.GioLamViec5;
					row["TongGioThuc"] = ngayCong.TG.GioThucTe5;
					row["TongCong"] = ngayCong.TongCong;
					row["TongPhuCap"] = ngayCong.PhuCaps._TongPC;
					row["TongTre"] = ngayCong.TG.VaoTre.TotalMinutes;
					row["TongSom"] = ngayCong.TG.RaaSom.TotalMinutes;
					row["IsEdited"] = false;
					var i = 1;
					var flag_IsShow = false;
					foreach (var CIO in ngayCong.DSVaoRa)
					{
						if (i > 3) break;
						if (CIO.HaveINOUT >= 0)
							flag_IsShow = true;
						row["cCheckInOut" + i] = CIO;
						switch (CIO.HaveINOUT)
						{
							case -1:
								row["TimeStrVao" + i] = CIO.Vao.Time;
								break;
							case -2:
								row["TimeStrRaa" + i] = CIO.Raa.Time;
								break;
							case 0:
							case 30:
								row["TimeStrVao" + i] = CIO.Vao.Time;
								row["TimeStrRaa" + i] = CIO.Raa.Time;
								break;
						}
						i++;
					}
					row["ShiftCode"] = ngayCong.CIOs_Absents_Code_Comp();

					if (flag_IsShow)
						kq.Rows.Add(row);
				}
			}

		}



		public static void TaoTableXacNhanCa(List<cUserInfo> dsnv, DataRow[] arrRecs, DataTable table)
		{
			table.Rows.Clear();
			foreach (var dataRow in arrRecs)
			{
				var nv = (cUserInfo)dataRow["cUserInfo"];
				var ngay = (DateTime)dataRow["TimeStrNgay"];
				var ngayCong = nv.DSNgayCong.Find(item => item.Ngay == ngay);
				foreach (var CIO in ngayCong.DSVaoRa)
				{
					if (CIO.HaveINOUT < 0) continue;
					var row = table.NewRow();
					row["UserEnrollNumber"] = nv.MaCC; //0
					row["UserFullCode"] = nv.MaNV; //1
					row["UserFullName"] = nv.TenNV; //1
					row["TimeStrNgay"] = ngay; //2
					row["TimeStrVao"] = CIO.Vao.Time; //4
					row["TimeStrRaa"] = CIO.Raa.Time; //5
					row["ShiftCode"] = CIO.CIOCodeFull();
					row["ShiftID"] = CIO.ThuocCa.ID; //9
					row["Cong"] = CIO.Cong; //20
					row["TongGioLam"] = CIO.TG.GioLamViec5;//Danglam giophut
					row["TongGioThuc"] = CIO.TG.GioThucTe5;//Danglam giophut
					row["cUserInfo"] = nv;
					row["cNgayCong"] = ngayCong;
					row["cCheckInOut"] = CIO;
					row["IsEdited"] = false;//CIO.IsEdited;
					table.Rows.Add(row);
				}
			}
		}
		public static void TaoTableXacNhanLamThem(List<cUserInfo> dsnv, DataTable table)
		{/*( ngaycong.TG.GioLamViec > XL2._08gio 
						 || ngaycong.DSVaoRa.Exists(item => item.HaveINOUT==0 
															&& item.DaXN == false 
															&& item.TG.OLai > TimeSpan.Zero))*/
			table.Rows.Clear();
			foreach (var nv in dsnv)
			{
				var min = 2;
				var max = nv.DSNgayCong.Count - 3;
				List<cNgayCong> list3 = nv.DSNgayCong
					.Where(item => item.TG.GioLamViec5 > XL2._08gio 
									|| item.DSVaoRa.Exists(subitem=>subitem.HaveINOUT ==0 && subitem.DaXN == false 
																	&& subitem.TG.OLai > TimeSpan.Zero))
					.TakeWhile((item1, i) => (i >= min || i <= max)).ToList();
				

				foreach (var ngayCong in list3)
				{
					foreach (var CIO in ngayCong.DSVaoRa)
					{
						if (CIO.HaveINOUT < 0) continue;
						var row = table.NewRow();
						row["UserEnrollNumber"] = nv.MaCC; //0
						row["UserFullCode"] = nv.MaNV; //1
						row["UserFullName"] = nv.TenNV; //1
						row["TimeStrNgay"] = ngayCong.Ngay; //2
						row["TimeStrVao"] = CIO.Vao.Time; //4
						row["TimeStrRaa"] = CIO.Raa.Time; //5
						row["ShiftCode"] = CIO.CIOCodeFull();
						row["ShiftID"] = CIO.ThuocCa.ID; //9
						row["Cong"] = CIO.Cong; //20
						row["TongGioLam"] = CIO.TG.GioLamViec5;//Danglam giophut
						row["TongGioThuc"] = CIO.TG.GioThucTe5;//Danglam giophut
						row["cUserInfo"] = nv;
						row["cNgayCong"] = ngayCong;
						row["cCheckInOut"] = CIO;
						row["IsEdited"] = false;// CIO.IsEdited;
						table.Rows.Add(row);
					}
					
				}
			}
		}

		#endregion

	}

}
