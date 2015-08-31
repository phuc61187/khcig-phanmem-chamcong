using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ChamCong_v06.DTO;
using ChamCong_v06.Helper;

namespace ChamCong_v06.BUS {
	public static partial class BUS_NhanVien {
		public static void KhoiTaoDSNV_DuocChon(List<int> List_UEN, List<DataRow> ListDataRow_All_NV, 
			List<DataRow> ListDataRow_All_Phong, out List<cUserInfo> List_NhanVien)
		{
			List_NhanVien = new List<cUserInfo>();
			foreach (int UEN in List_UEN)
			{
				DataRow row = (from DataRow dataRow in ListDataRow_All_NV where (int)dataRow[Field.UserEnrollNumber.ToString()] == UEN select dataRow).Single();
				cUserInfo user = new cUserInfo {
					MaCC = (int)row[Field.UserEnrollNumber.ToString()], MaNV = row[Field.UserFullCode.ToString()].ToString(),
					TenNV = row[Field.UserFullName.ToString()].ToString(),
					ChucVu = new ID_Description() { ID = (int)row[Field.IDChucVu.ToString()], Description = row[Field.ChucVu.ToString()].ToString() },
					LichTrinh = new ID_Description() { ID = (int)row[Field.SchID.ToString()], Description = row[Field.SchName.ToString()].ToString()},

					//todo còn các trường khác
				};
				user.PhongBan = XacDinhPhongBan((int)row[Field.UserIDDepartment.ToString()], ListDataRow_All_Phong);
				

				List_NhanVien.Add(user);
			}
		}

		private static DataRow XacDinhPhongBan(int UserIDDepartment, List<DataRow> ListDataRow_All_Phong)
		{
			return (from DataRow dataRow in ListDataRow_All_Phong where (int) dataRow[Field.IDDepartment.ToString()] == UserIDDepartment select dataRow).SingleOrDefault();
		}
	}
}
