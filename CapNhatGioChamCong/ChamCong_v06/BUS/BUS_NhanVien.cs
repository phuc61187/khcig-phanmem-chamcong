using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ChamCong_v06.DTO;
using ChamCong_v06.Helper;

namespace ChamCong_v06.BUS {
	public partial class BUS_NhanVien {
		public void KhoiTaoDSNV_DuocChon(List<int> List_UEN, List<DataRow> ListDataRow_All_NV,
			List<cPhongBan> List_Phong, List<cNhomCa> List_NhomCa, out List<cUserInfo> List_NhanVien) {
			List_NhanVien = new List<cUserInfo>();
			foreach (int UEN in List_UEN) {
				DataRow row = (from DataRow dataRow in ListDataRow_All_NV where (int)dataRow[f1.UserEnrollNumber.ToString()] == UEN select dataRow).Single();
				cUserInfo user = new cUserInfo {
					MaCC = (int)row[f1.UserEnrollNumber.ToString()], MaNV = row[f1.UserFullCode.ToString()].ToString(),
					TenNV = row[f1.UserFullName.ToString()].ToString(),
					ChucVu = new ID_Description() { ID = (int)row[f1.IDChucVu.ToString()], Description = row[f1.ChucVu.ToString()].ToString() },
					//LichTrinh = new ID_Description() { ID = (int)row[Field.SchID.ToString()], Description = row[Field.SchName.ToString()].ToString()},

					//todo còn các trường khác
				};
				user.PhongBan = XacDinhPhongBan((int)row[f1.UserIDDepartment.ToString()], List_Phong);
				user.NhomCa = XacDinhNhomCa((int)row[f1.SchID.ToString()], List_NhomCa);
				//todo trường hợp nhóm ca null thì sao??
				List_NhanVien.Add(user);
			}
		}

		internal void KhoiTaoNV(DataRow dataRow, IEnumerable<cNhomCa> List_NhomCa, out cUserInfo nhanvien) {
			nhanvien = new cUserInfo {
				MaCC = (int)dataRow[f1.UserEnrollNumber.ToString()], MaNV = dataRow[f1.UserFullCode.ToString()].ToString(),
				TenNV = dataRow[f1.UserFullName.ToString()].ToString(),
				ChucVu = new ID_Description() { ID = (int)dataRow[f1.IDChucVu.ToString()], Description = dataRow[f1.ChucVu.ToString()].ToString() },
				//LichTrinh = new ID_Description() { ID = (int)row[Field.SchID.ToString()], Description = row[Field.SchName.ToString()].ToString()},
				PhongBan_ID_Des = new ID_Description { ID = (int)dataRow["UserIDDepartment"], Description = dataRow["DepartmentDescription"].ToString() }

				//todo còn các trường khác
			};
			//user.PhongBan = XacDinhPhongBan((int)dataRow[Field.UserIDDepartment.ToString()], List_Phong);
			nhanvien.NhomCa = XacDinhNhomCa((int)dataRow["SchID"], List_NhomCa);
			//todo trường hợp nhóm ca null thì sao??

		}


		private cNhomCa XacDinhNhomCa(int SchID, IEnumerable<cNhomCa> List_NhomCa) {
			cNhomCa kq = (from cNhomCa item in List_NhomCa where item.IDDescription.ID == SchID select item).SingleOrDefault();
			if (kq == null) {
				kq = GlobalVariables.NhomCaMacDinh;
			}
			return kq;
		}

		private cPhongBan XacDinhPhongBan(int UserIDDepartment, IEnumerable<cPhongBan> List_All_Phong) {
			return (from cPhongBan phong in List_All_Phong
					where phong.Phong.ID == UserIDDepartment
					select phong).SingleOrDefault();
		}


		public void XuatDataTableXemCong(List<cUserInfo> dsnv_DuocChon, out DataTable table_KQ_XemCong) {
			table_KQ_XemCong = new DataTable();
			table_KQ_XemCong.Columns.Add(new DataColumn(f2.UserEnrollNumber.ToString(), typeof(int)));
			table_KQ_XemCong.Columns.Add(new DataColumn(f2.UserFullName.ToString(), typeof(string)));
			table_KQ_XemCong.Columns.Add(new DataColumn(f2.UserFullCode.ToString(), typeof(string)));
			table_KQ_XemCong.Columns.Add(new DataColumn(f2.Ngay.ToString(), typeof(DateTime)));
			table_KQ_XemCong.Columns.Add(new DataColumn(f2.KyHieuCa.ToString(), typeof(string)));
			table_KQ_XemCong.Columns.Add(new DataColumn(f2.KyHieuVang.ToString(), typeof(string)));
			table_KQ_XemCong.Columns.Add(new DataColumn(f2.TongCong.ToString(), typeof(float)));
			table_KQ_XemCong.Columns.Add(new DataColumn(f2.TongPhuCap.ToString(), typeof(float)));
			table_KQ_XemCong.Columns.Add(new DataColumn(f2.TongGioLamViec.ToString(), typeof(TimeSpan)));
			table_KQ_XemCong.Columns.Add(new DataColumn(f2.TongGioHienDien.ToString(), typeof(TimeSpan)));
			table_KQ_XemCong.Columns.Add(new DataColumn(f2.TongPhutTreSom.ToString(), typeof(TimeSpan)));
			table_KQ_XemCong.Columns.Add(new DataColumn(f2.TongPhutTre.ToString(), typeof(TimeSpan)));
			table_KQ_XemCong.Columns.Add(new DataColumn(f2.TongPhutSom.ToString(), typeof(TimeSpan)));
			table_KQ_XemCong.Columns.Add(new DataColumn(f2.TongGioLamDem.ToString(), typeof(TimeSpan)));
			table_KQ_XemCong.Columns.Add(new DataColumn(f2.TongGioLamThem.ToString(), typeof(TimeSpan)));

			foreach (cUserInfo nhanVien in dsnv_DuocChon) {
				foreach (cNgayCong ngayCong in nhanVien.DSNgayDaCC) {
					DataRow dataRow = table_KQ_XemCong.NewRow();
					table_KQ_XemCong.Rows.Add(dataRow);
				}
			}
		}

		public void XuatTableXemCong_AddNewRow(DataRow dataRow, cUserInfo nhanVien, cNgayCong ngayCong,
			ref int SoTH_ThieuCC, ref int SoTH_TreSom, ref int SoTH_ChoPhepTreSom) {
			foreach (var checkInOutDaCc in ngayCong.DSVaoRa)
			{
				if (checkInOutDaCc.CheckVT != TrangThaiCheck.CheckDayDu) SoTH_ThieuCC++;
				if ((checkInOutDaCc.ChoPhepTre == false && checkInOutDaCc.Tre > TimeSpan.Zero)
					|| (checkInOutDaCc.ChoPhepSom == false && checkInOutDaCc.Som > TimeSpan.Zero))
					SoTH_TreSom++;
				if (checkInOutDaCc.ChoPhepTre || checkInOutDaCc.ChoPhepSom) SoTH_ChoPhepTreSom++;
			}

			dataRow[f2.UserEnrollNumber.ToString()] = nhanVien.MaCC;
			dataRow[f2.UserFullName.ToString()] = nhanVien.TenNV;
			dataRow[f2.UserFullCode.ToString()] = nhanVien.MaNV;
			dataRow[f2.Ngay.ToString()] = ngayCong.Ngay;
			dataRow[f2.KyHieuCa.ToString()] = ngayCong.KyHieuCa_1Ngay;//todo
			dataRow[f2.KyHieuVang.ToString()] = ngayCong.KyHieuVang_1Ngay;//todo
			dataRow[f2.TongCong.ToString()] = ngayCong.TongCong;//todo
			dataRow[f2.TongPhuCap.ToString()] = ngayCong.TongPhuCap;
			dataRow[f2.TongGioLamViec.ToString()] = ngayCong.LamViec;
			dataRow[f2.TongGioHienDien.ToString()] = ngayCong.HienDien;//todo
			dataRow[f2.TongPhutTreSom.ToString()] = ngayCong.Tre + ngayCong.Som;
			dataRow[f2.TongPhutTre.ToString()] = ngayCong.Tre;
			dataRow[f2.TongPhutSom.ToString()] = ngayCong.Som;
			dataRow[f2.TongGioLamDem.ToString()] = ngayCong.LamDem;
			dataRow[f2.TongGioLamThem.ToString()] = ngayCong.LamThem;
		}



	}
}
