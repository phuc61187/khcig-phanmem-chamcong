using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamCong_v04.DAL;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using System.Data;
using log4net.Config;

namespace ChamCong_v04.BUS
{
	//new function for ver 4.0.0.5
	public static partial class XL
	{
		public static void GetLichTrinhNV(cUserInfo nhanvien, int? schID) {
			#region xét lịch trình cho nv
			// chưa có lịch trình thì tạo lịch trình ko có ca
			if (schID == null) {
				nhanvien.LichTrinhLV = new cShiftSchedule {
					SchID = int.MinValue,
					TenLichTrinh = "Chưa SX",
					DSCaThu = new List<List<cCa>>(7),
					DSCaMRThu = new List<List<cCa>>(7)
				};
				for (int i = 0; i < 7; i++) {
					nhanvien.LichTrinhLV.DSCaThu.Add(new List<cCa>());
					nhanvien.LichTrinhLV.DSCaMRThu.Add(new List<cCa>());
				}
				nhanvien.LichTrinhLV.TGLamDemTheoQuyDinh = TGLamDemTheoQuyDinh._22h00;//ver 4.0.0.4
			}
			else {
				// tìm lịch trình có ID, nếu ko tìm thấy thì mặc định là chưa sắp xếp
				nhanvien.LichTrinhLV = XL.DSLichTrinh.FirstOrDefault(o => o.SchID == schID);
				if (nhanvien.LichTrinhLV == null) {
					nhanvien.LichTrinhLV = new cShiftSchedule {
						SchID = int.MinValue,
						TenLichTrinh = "Chưa SX",
						DSCaThu = new List<List<cCa>>(7),
						DSCaMRThu = new List<List<cCa>>(7)
					};
					for (int i = 0; i < 7; i++) {
						nhanvien.LichTrinhLV.DSCaThu.Add(new List<cCa>());
						nhanvien.LichTrinhLV.DSCaMRThu.Add(new List<cCa>());
					}
					nhanvien.LichTrinhLV.TGLamDemTheoQuyDinh = TGLamDemTheoQuyDinh._22h00;//ver 4.0.0.4
				}
			}
			if (nhanvien.LichTrinhLV.TGLamDemTheoQuyDinh == TGLamDemTheoQuyDinh._22h00) { nhanvien.StartNT = XL2._22h00; nhanvien.EndddNT = XL2._06h00; }
			else { nhanvien.StartNT = XL2._21h45; nhanvien.EndddNT = XL2._05h45; }

			#endregion

		}




	}
}
