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
			List<cPhongBan> List_Phong, List<cNhomCa> List_NhomCa, out List<cUserInfo> List_NhanVien)
		{
			List_NhanVien = new List<cUserInfo>();
			foreach (int UEN in List_UEN)
			{
				DataRow row = (from DataRow dataRow in ListDataRow_All_NV where (int)dataRow[Field.UserEnrollNumber.ToString()] == UEN select dataRow).Single();
				cUserInfo user = new cUserInfo {
					MaCC = (int)row[Field.UserEnrollNumber.ToString()], MaNV = row[Field.UserFullCode.ToString()].ToString(),
					TenNV = row[Field.UserFullName.ToString()].ToString(),
					ChucVu = new ID_Description() { ID = (int)row[Field.IDChucVu.ToString()], Description = row[Field.ChucVu.ToString()].ToString() },
					//LichTrinh = new ID_Description() { ID = (int)row[Field.SchID.ToString()], Description = row[Field.SchName.ToString()].ToString()},

					//todo còn các trường khác
				};
				user.PhongBan = XacDinhPhongBan((int)row[Field.UserIDDepartment.ToString()], List_Phong);
				user.NhomCa = XacDinhNhomCa((int)row["SchID"], List_NhomCa);
				//todo trường hợp nhóm ca null thì sao??
				List_NhanVien.Add(user);
			}
		}

		private cNhomCa XacDinhNhomCa(int SchID, IEnumerable<cNhomCa> List_NhomCa)
		{
			cNhomCa kq = (from cNhomCa item in List_NhomCa where item.IDDescription.ID == SchID select item).SingleOrDefault();
			if (kq == null)
			{
				kq = GlobalVariables.NhomCaMacDinh;
			}
			return kq;
		}

		private static cPhongBan XacDinhPhongBan(int UserIDDepartment, IEnumerable<cPhongBan> List_All_Phong)
		{
			return (from cPhongBan phong in List_All_Phong
					where phong.Phong.ID == UserIDDepartment
					select phong).SingleOrDefault();
		}
	}
}
