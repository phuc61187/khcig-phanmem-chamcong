using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ChamCong_v06.DAL;
using ChamCong_v06.DTO;
using ChamCong_v06.Helper;

namespace ChamCong_v06.BUS {
	public partial class BUS_ChamCong {
		public void ChamCong1()
		{
			List<int> arrayUEN = new List<int>();
			FromToTime KhoangTG = new FromToTime();
			List<cCheck> DSCheckInCheckOut;
			List<cCheck> DSCheck_BiLoai_All;
			List<cUserInfo> DSNVChamCong;
			LapDSNVChamCong(arrayUEN, out DSNVChamCong);
			DAL_CheckInCheckOut dal = new DAL_CheckInCheckOut();
			dal.GetCheckInCheckOutData(KhoangTG, arrayUEN, out DSCheckInCheckOut);

			XuLy_Loai_CheckTrong30ph(arrayUEN, DSCheckInCheckOut, out DSCheck_BiLoai_All);

		}

		private void LapDSNVChamCong(List<int> arrayUEN, out List<cUserInfo> DSNVChamCong) {
			DSNVChamCong = new List<cUserInfo>();
			
		}

		private void XuLy_Loai_CheckTrong30ph(List<int> ArrayUEN, List<cCheck> DSCheckInCheckOut, out List<cCheck> DSCheck_BiLoai_All) {
			DSCheck_BiLoai_All = new List<cCheck>();
			foreach (int uen in ArrayUEN) {
				List<cCheck> DS_Check_By_UEN = (from cCheck check in DSCheckInCheckOut where check.MaCC == uen select check).ToList();
				List<cCheck> DSCheck_BiLoai_By_UEN;
				LoaiBoCheckKoHopLe1_V6(DS_Check_By_UEN, out DSCheck_BiLoai_By_UEN);
				DSCheck_BiLoai_All.AddRange(DSCheck_BiLoai_By_UEN);
			}
			DAL.DAL_CheckInCheckOut dal = new DAL_CheckInCheckOut();
			dal.LoaiCheckTrong30ph(DSCheck_BiLoai_All);
		}

		internal void ChamCong(List<DTO.cUserInfo> listDSNV, DateTime Thang)
		{
			DataTable tableArrUEN = MyUtility.Array_To_DataTable("tableName",
				(from cUserInfo user in listDSNV select user.MaCC).ToList());
			// xác định tháng đã kết công chưa, nếu đã kết công thì lấy danh sách các ngày đã kết công
			// chưa kết công thì tính công
			FromToTime khoangTG_Check = new FromToTime();
			DataTable tableCheck = SqlDataAccessHelper.ExecSPQuery(SPName6.CheckInOut_DocCheckChuaXuLyV6.ToString(),
				new SqlParameter("@StartTime", khoangTG_Check.From),
				new SqlParameter("@EndTime", khoangTG_Check.To),
				new SqlParameter("@ArrUserEnrollNumber", tableArrUEN));
			XuLyCheck(listDSNV, tableCheck);

			// lấy các check mới chưa ghép cặp được để ghép cặp lại
		}

		private void XuLyCheck(List<cUserInfo> listDSNV, DataTable tableCheck) {
			if (tableCheck.Rows.Count == 0) return;

			List<cCheck> dsCheck_Disable_All_NV = new List<cCheck>();
			foreach (cUserInfo user in listDSNV)
			{
				LoadDSCheck_A(user.MaCC, tableCheck, user.DS_Check_A);
				var dsCheck_Disable = new List<cCheck>();
				LoaiBoCheckKoHopLe1(user.DS_Check_A, ref dsCheck_Disable);
				dsCheck_Disable_All_NV.AddRange(dsCheck_Disable);
				GhepCIO_A2(user.DS_Check_A, user.DS_CIO_A);
			}
		}

		private void LoadDSCheck_A(int tempMaCC, DataTable tableCheck_A, List<cCheck> ds_Check_A) {
			var arrRows = tableCheck_A.Select("UserEnrollNumber = " + tempMaCC, "TimeStr asc");
			//reset lại các danh sách
			ds_Check_A.Clear();

			if (arrRows.Length == 0) return;

			foreach (var row in arrRows) {
				var UserEnrollNumber = (int)row["UserEnrollNumber"];
				var MachineNo = (int)row["MachineNo"];
				var Source = (string)row["Source"];
				var TimeStr = (DateTime)row["TimeStr"];

				cCheck check;
				if (MachineNo % 2 == 1) {
					var checkInn = new cCheck { MaCC = UserEnrollNumber, Time = TimeStr, Source = Source, MachineNo = MachineNo, Type = "I",};
					check = checkInn;
				}
				else {
					var checkOut = new cCheck { MaCC = UserEnrollNumber, Time = TimeStr, Source = Source, MachineNo = MachineNo, Type = "O",};
					check = checkOut;
				}
				ds_Check_A.Add(check);
			}

		}

		private void LoaiBoCheckKoHopLe1(List<cCheck> ds_Check_A, ref List<cCheck> ds_Check_Trong30ph) {
			//clear ds_Check_Trong30ph trước vì nó còn giữ các check ko hl của nv trước
			ds_Check_Trong30ph.Clear();
			// lọc này phải dảm bảo sort trước
			if (ds_Check_A == null || ds_Check_A.Count == 0 || ds_Check_A.Count == 1) return;
			var i = 0;
			while (i + 1 < ds_Check_A.Count) {
				var before = ds_Check_A[i];
				var afterr = ds_Check_A[i + 1];
				if ((before.Type == afterr.Type) && ((afterr.Time - before.Time) < XL2._10phut)) {
					//info ver 4.0.0.1 // giữ vào đầu tiên, ra cuối cùng
					if (before.Type == "I") {
						ds_Check_Trong30ph.Add(afterr);
						ds_Check_A.Remove(afterr);
					}
					else //out
					{
						ds_Check_Trong30ph.Add(before);
						ds_Check_A.Remove(before);
					}
				}
				else if (before.Type == "I" && afterr.Type == "O"
					&& (afterr.Time - before.Time) < XL2._10phut) {
					//IO trong 30 phút thì chỉ giữ O
					ds_Check_Trong30ph.Add(before);
					ds_Check_A.Remove(before);
				}
				else i++;
			}
		}
		private void LoaiBoCheckKoHopLe1_V6(List<cCheck> ds_Check_A, out List<cCheck> ds_Check_Trong30ph) {
			ds_Check_Trong30ph = new List<cCheck>();
			// lọc này phải dảm bảo sort trước
			if (ds_Check_A == null || ds_Check_A.Count == 0 || ds_Check_A.Count == 1) return;
			var i = 0;
			while (i + 1 < ds_Check_A.Count) {
				var before = ds_Check_A[i];
				var afterr = ds_Check_A[i + 1];
				if ((before.Type == afterr.Type) && ((afterr.Time - before.Time) < XL2._10phut)) {
					//info ver 4.0.0.1 // giữ vào đầu tiên, ra cuối cùng
					if (before.Type == "I") {
						ds_Check_Trong30ph.Add(afterr);
						ds_Check_A.Remove(afterr);
					}
					else //out
					{
						ds_Check_Trong30ph.Add(before);
						ds_Check_A.Remove(before);
					}
				}
				else if (before.Type == "I" && afterr.Type == "O"
					&& (afterr.Time - before.Time) < XL2._10phut) {
					//IO trong 30 phút thì chỉ giữ O
					ds_Check_Trong30ph.Add(before);
					ds_Check_A.Remove(before);
				}
				else i++;
			}
		}


		private void GhepCIO_A2(List<cCheck> ds_Check_A, List<cCheckInOut> ds_CIO_A) {
			ds_CIO_A.Clear();
			var x = 0;
			while (x + 1 < ds_Check_A.Count) {
				var chk_1 = ds_Check_A[x];
				var chk_2 = ds_Check_A[x + 1];
				if (chk_1.Type == "O") {
					// đầu ds là checkOut --> ra ko vào
					var CIO = new cCheckInOut { Vao = null, Raa = chk_1, HaveINOUT = -2, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
					ds_CIO_A.Add(CIO);
					x++;
				}
				else {
					//đầu ds là checkInn-> kiểm tra kế nếu cũng là check In thì checkInn trước là vào ko ra
					if (chk_2.Type == "I") {
						var CIO = new cCheckInOut { Vao = chk_1, Raa = null, HaveINOUT = -1, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
						ds_CIO_A.Add(CIO);
						x++;
					}
					else {
						// kế là checkOut --> kiểm tra nằm trong khoảng >30ph và dưới 21h45 thì ghép, ngược lại thì giờ vào ko ra, ra ko vào
						var duration = chk_2.Time - chk_1.Time;
						if (duration > XL2._22h00) {//ver 4.0.0.4	old:(duration > XL2._21h45)
							var CIO1 = new cCheckInOut { Vao = chk_1, Raa = null, HaveINOUT = -1, TimeDaiDien = chk_1.Time, TD = new ThoiDiem(), }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
							var CIO2 = new cCheckInOut { Vao = null, Raa = chk_2, HaveINOUT = -2, TimeDaiDien = chk_2.Time, TD = new ThoiDiem(), }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
							ds_CIO_A.Add(CIO1);
							x++;
							ds_CIO_A.Add(CIO2);
							x++;
						}
						else {
							var CIO = new cCheckInOut { Vao = chk_1, Raa = chk_2, HaveINOUT = 0, TimeDaiDien = chk_1.Time, TG = new StructTGCa(), TD = new ThoiDiem(), };
							ds_CIO_A.Add(CIO);
							x++;
							x++;
						}
					}
				}
			}
			// xảy ra 2 TH, 1 là hết ds--> ko làm gì hết, 2 là còn lại 1 pt-> them vào
			if (x < ds_Check_A.Count) {
				var chk_1 = ds_Check_A[x];
				if (chk_1.Type == "I") {
					var CIO1 = new cCheckInOut { Vao = chk_1, Raa = null, HaveINOUT = -1, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
					ds_CIO_A.Add(CIO1);
				}
				else {
					var CIO2 = new cCheckInOut { Vao = null, Raa = chk_1, HaveINOUT = -2, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
					ds_CIO_A.Add(CIO2);
				}
			}
		}

/*
		public static void XetCa_ListCIO_A3(List<cCheckInOut> ds_CIO_A, cShiftSchedule lichtrinh, List<cCheck> ds_raa3_vao1, List<cCheck> ds_check_A) {
			//bool macdinh_tinhPC50, //[140615_4]
			try {
				var i = 0;
				while (i < ds_CIO_A.Count) {
					var CIO = ds_CIO_A[i];

					#region nếu giờ quên check thì chỉ kiểm tra khoảng hiểu ca

					int index;
					if (CIO.HaveINOUT < 0) {
						CIO.ThuocNgayCong = CIO.TimeDaiDien.Date;
						index = GetIndex(CIO.ThuocNgayCong);
						CIO.DSCa = Tim_DSCa_NhanDienDuoc(CIO.TimeDaiDien, CIO.ThuocNgayCong, CIO.HaveINOUT, lichtrinh.DSCaThu[index]);
						i++;
						continue;
					}

					#endregion

					var ngay = ThuocNgayCong(CIO.TimeDaiDien);
					CIO.ThuocNgayCong = ngay;
					index = GetIndex(CIO.ThuocNgayCong);
					var ca = KiemtraThuocCa(CIO.Vao.Time, CIO.Raa.Time, CIO.ThuocNgayCong, lichtrinh.DSCaThu[index]);

					#region nếu thuộc khoảng hiểu ca thì set ca

					if (ca != null) {
						if (ca.TachCaDem) {
							//tbd thêm điều kiện ca.QuaDem
							var ca3 = ca.catruoc;
							var ca1 = ca.casauuu;

							#region check inn, check out vao 3 ra 3, vao 1 ra 1

							var vaoca3 = CIO.Vao;
							var raaca3 = new cCheck {
								ID = int.MinValue, Type = "O", MachineNo = 22, Source = "PC", Time = ngay.Add(ca3.Duty.Off), MaCC = CIO.Vao.MaCC,
								PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false },
							}; // đáng lẽ IDgiờgốc là -1 vì giờ này mới thêm chưa bị sửa nhưng do đây là giờ đệm ko có trong csdl nên để max, ko cho update
							var vaoca1 = new cCheck {
								ID = int.MinValue, Type = "I", MachineNo = 21, Source = "PC", Time = ngay.AddDays(1d).Date.Add(ca1.Duty.Onn).Add(XL2._01giay), MaCC = CIO.Raa.MaCC,
								PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false },
							}; // đáng lẽ IDgiờgốc là -1 vì giờ này mới thêm chưa bị sửa nhưng do đây là giờ đệm ko có trong csdl nên để max, ko cho update
							var raaca1 = CIO.Raa;

							#endregion

							ds_raa3_vao1.Add(raaca3);
							ds_raa3_vao1.Add(vaoca1);
							ds_check_A.Add(raaca3);
							ds_check_A.Add(vaoca1);
							ds_check_A.Sort(new cCheckComparer());
							// do tách ra thành 2 CIO mới nên phải gán lại IsEdited cho từng cái
							ds_CIO_A[i] = new cCheckInOut { IsEdited = vaoca3.IsEdited, TG = new ThoiGian(), TD = new ThoiDiem(), ThuocCa = ca3, Vao = vaoca3, Raa = raaca3, ThuocNgayCong = ngay, TimeDaiDien = vaoca3.Time, }; //TinhPC150 = macdinh_tinhPC50, //[140615_2]

							var newCIO = new cCheckInOut { IsEdited = raaca1.IsEdited, TG = new ThoiGian(), TD = new ThoiDiem(), ThuocCa = ca1, Vao = vaoca1, Raa = raaca1, ThuocNgayCong = ngay.AddDays(1d), TimeDaiDien = vaoca1.Time, }; //TinhPC150 = macdinh_tinhPC50, //[140615_2]

							// vì hàm insert ko cho phép chèn ở vị trí > số lượng phần tử
							// => nên nếu i là phần tử cuối thì add vào cuối danh sách, ngược lại thì insert vào vị trí i+1
							if (i == (ds_CIO_A.Count - 1)) ds_CIO_A.Add(newCIO);
							else ds_CIO_A.Insert(i + 1, newCIO);
							i = i + 2; // +2 vì i là ca3, i+1 là ca 1
						}
						else {
							CIO.ThuocCa = ca;
							i++;
						}
					}
					#endregion
					#region nếu ko thuộc khoảng hiểu thì đó tạo ca tự do, tính công theo ca tự do này

					else {
						var catudo = new cCa { ID = Int32.MinValue, Code = Properties.Settings.Default.shiftCodeCa8h, Is_CaTuDo = true };
						//TaoCaTuDo(catudo, CIO.Vao.Time, XL2._08gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, "8");
						TaoCaTuDo(catudo, CIO.Vao.Time);//ver 4.0.0.4	
						CIO.ThuocCa = catudo;

						i++;
					}

					#endregion
				}

			}
			catch (Exception e) {
				lg.Error(string.Format("[{0}]_[{1}]\n", "XLChamCong", System.Reflection.MethodBase.GetCurrentMethod().Name), e);
			}
		}
*/
	}
}
