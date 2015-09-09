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

		public void XemCong(List<cUserInfo> DSNV, FromToDateTime KhoangTG)
		{
			ChamCong2(DSNV, KhoangTG);
			XemCong2(DSNV, KhoangTG);
		}

		private void XemCong2(List<cUserInfo> DSNV, FromToDateTime KhoangTG) {
			List<int> arrayUEN = (from cUserInfo item in DSNV select item.MaCC).ToList();
			DataTable tableArrayUEN = MyUtility.Array_To_DataTable("tableName", arrayUEN);

			List<cCheckInOut_DaCC> DS_CIO_DaCC;
			List<cKhaiBaoVang> DS_KhaiBaoVang;
			List<DateTime> DS_NgayLe;
			DAL_CheckInCheckOut dal = new DAL_CheckInCheckOut();
			DataTable tableCIO, tableNgayCong, tableNgayLe;
			dal.GetCIOData(tableArrayUEN, KhoangTG, out DS_CIO_DaCC);
			//List<cCheckInOut_DaCC>  = 
			dal.GetNgayCongData(tableArrayUEN, KhoangTG, out tableNgayCong);
			dal.GetNgayVangData(tableArrayUEN, KhoangTG, out tableNgayLe, out DS_KhaiBaoVang);
			dal.GetNgayLeData(KhoangTG, out DS_NgayLe);
			//string template = "UserEnrollNumber = {0} and Ngay"
			foreach (cUserInfo nhanvien in DSNV)
			{
				LapDSNgayCongDeXuLy(KhoangTG, out nhanvien.DSNgayDaCC);
				Load_DS_CIO(nhanvien.DSNgayDaCC, DS_CIO_DaCC);
			}
		}

		private void Load_DS_CIO(List<cNgayCong> list, List<cCheckInOut_DaCC> DS_CIO_DaCC, DataTable TableNgayCong, List<cKhaiBaoVang> DS_KhaiBaoVang, List<DateTime> DSNgayLe ) {
			foreach (cNgayCong ngayCong in list)
			{
				ngayCong.DSVaoRa = (from cCheckInOut_DaCC item in DS_CIO_DaCC where item.Ngay == ngayCong.Ngay select item).ToList();

			}
		}

		private void LapDSNgayCongDeXuLy(FromToDateTime KhoangTG, out List<cNgayCong> DSNgayDaCC) {
			DSNgayDaCC = new List<cNgayCong>();
			for (DateTime i = KhoangTG.From; i <= KhoangTG.To; i=i.Add(GlobalVariables._1ngay))
			{
				cNgayCong ngayCong = new cNgayCong {Ngay = i, DSVang = new List<cKhaiBaoVang>()};
				DSNgayDaCC.Add(ngayCong);
			}
		}

		public void ChamCong2(List<cUserInfo> DSNV, FromToDateTime KhoangTG) {
			//ở cấp trên luôn kiểm tra có nhân viên mới chấm công
			List<int> arrayUEN = (from cUserInfo item in DSNV select item.MaCC).ToList();


			List<cCheck> DSCheckInCheckOut;
			List<cCheck> DSCheck_BiLoai_All;
			DAL_CheckInCheckOut dal = new DAL_CheckInCheckOut();
			dal.GetCheckInCheckOutData(KhoangTG, arrayUEN, out DSCheckInCheckOut);
			XuLy_Loai_CheckTrong30ph(arrayUEN, DSCheckInCheckOut, out DSCheck_BiLoai_All);// giữ check hợp lệ, loại check cùng loại, gần nhau, hoặc check nhầm IO trong 10ph

			// ghép check in- check out
			List<cCheck> DS_Check_A = new List<cCheck>();
			List<cCheckInOut> DS_CIO_A = new List<cCheckInOut>();
			foreach (cUserInfo nhanVien in DSNV) {
				DS_Check_A.Clear();
				DS_CIO_A.Clear();
				int ma_ChamCong = nhanVien.MaCC;
				DS_Check_A.AddRange(from item in DSCheckInCheckOut where item.MaCC == ma_ChamCong select item);
				DS_Check_A.Sort(new cCheckComparer());
				GhepCIO_A2(DS_Check_A, DS_CIO_A);
				XetCa_ListCIO_A3_V6(DS_CIO_A, nhanVien.NhomCa.DSCa);
				TinhTGLV(DS_CIO_A);
				//LapDSNgayCongDeXuLy(nhanVien.DS_CIO_A, out nhanVien.DSNgayDangCC);
				dal.Insert_CheckInOutData(ma_ChamCong, DS_CIO_A);
			}
		}

		private void LapDSNgayCongDeXuLy(List<cCheckInOut> DS_CIO, out List<cNgayCong> DSNgayCong) {
			DSNgayCong = new List<cNgayCong>();
			DS_CIO.Sort(new cCheckInOutComparer());

			//TinhTGLV(nhanVien.DS_CIO_A);
			var Groups = (from cCheckInOut item in DS_CIO
						  group item by item.ThuocNgayCong into _1group
						  select _1group);
			foreach (IGrouping<DateTime, cCheckInOut> _1group in Groups) {
				cNgayCong ngayCong = new cNgayCong {
					Ngay = _1group.Key, TGNgay = new StructTGNgay(), TongCongCa = new StructCongCa()
				};
				foreach (cCheckInOut item in _1group) {
					TinhTGLV_Cong(item);
					if (item.QuaDem) ngayCong.QuaDem = true;
					TinhTongThoiGianTrongNgay(ngayCong.TGNgay, item.KhoangTGCa);
					TinhTongCongTrongNgay(ngayCong.TongCongCa, item.CongTheoCa);
				}
				
			}
			//Groups
		}

		private void TinhTongCongTrongNgay(StructCongCa CongNgay, StructCongCa CongCa)
		{
			CongNgay.TrongGio += CongCa.TrongGio;
			CongNgay.NgoaiGio += CongCa.NgoaiGio;
			CongNgay.TruCongTre += CongCa.TruCongTre;
			CongNgay.TruCongSom += CongCa.TruCongSom;
			CongNgay.DinhMuc += CongCa.DinhMuc;
			CongNgay.Tong = CongCa.Tong;
		}

		private void TinhTongThoiGianTrongNgay(StructTGNgay TGNgay, StructTGCa TGCa) {
			TGNgay.HienDien += TGCa.HienDien;
			TGNgay.Tre += TGCa.Tre;
			TGNgay.Som += TGCa.Som;
			TGNgay.VaoSauCa += TGCa.VaoSauCa;
			TGNgay.RaTruocCa += TGCa.RaTruocCa;
			TGNgay.LamViec += TGCa.LamTrongGio + TGCa.LamNgoaiGio;
			TGNgay.LamDem += TGCa.LamDem;
		}

		private void XuLy_Loai_CheckTrong30ph(IEnumerable<int> ArrayUEN, List<cCheck> DSCheckInCheckOut, out List<cCheck> DSCheck_BiLoai_All) {
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

		private void LoaiBoCheckKoHopLe1_V6(List<cCheck> ds_Check_A, out List<cCheck> ds_Check_Trong30ph) {
			ds_Check_Trong30ph = new List<cCheck>();
			// lọc này phải dảm bảo sort trước
			if (ds_Check_A == null || ds_Check_A.Count == 0 || ds_Check_A.Count == 1) return;
			var i = 0;
			while (i + 1 < ds_Check_A.Count) {
				var before = ds_Check_A[i];
				var afterr = ds_Check_A[i + 1];
				if ((before.Type == afterr.Type) && ((afterr.Time - before.Time) < GlobalVariables._10phut)) {
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
					&& (afterr.Time - before.Time) < GlobalVariables._10phut) {
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
					var CIO = new cCheckInOut { Vao = null, Raa = chk_1, CheckVT = TrangThaiCheck.ThieuVao, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
					ds_CIO_A.Add(CIO);
					x++;
				}
				else {
					//đầu ds là checkInn-> kiểm tra kế nếu cũng là check In thì checkInn trước là vào ko ra
					if (chk_2.Type == "I") {
						var CIO = new cCheckInOut { Vao = chk_1, Raa = null, CheckVT = TrangThaiCheck.ThieuRa, TimeDaiDien = chk_1.Time, }; //tbd bỏ  TG = new ThoiGian() xem lại có cần thiết thì giữ lại
						ds_CIO_A.Add(CIO);
						x++;
					}
					else {
						// kế là checkOut --> kiểm tra nằm trong khoảng >30ph và dưới 21h45 thì ghép, ngược lại thì giờ vào ko ra, ra ko vào
						var duration = chk_2.Time - chk_1.Time;
						if (duration > GlobalVariables._22h00) {//ver 4.0.0.4	old:(duration > XL2._21h45)
							var CIO1 = new cCheckInOut { Vao = chk_1, Raa = null, CheckVT = TrangThaiCheck.ThieuRa, TimeDaiDien = chk_1.Time, };
							var CIO2 = new cCheckInOut { Vao = null, Raa = chk_2, CheckVT = TrangThaiCheck.ThieuVao, TimeDaiDien = chk_2.Time, };
							ds_CIO_A.Add(CIO1);
							x++;
							ds_CIO_A.Add(CIO2);
							x++;
						}
						else {
							var CIO = new cCheckInOut { Vao = chk_1, Raa = chk_2, CheckVT = TrangThaiCheck.CheckDayDu, TimeDaiDien = chk_1.Time, };
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
					var CIO1 = new cCheckInOut { Vao = chk_1, Raa = null, CheckVT = TrangThaiCheck.ThieuRa, TimeDaiDien = chk_1.Time, };
					ds_CIO_A.Add(CIO1);
				}
				else {
					var CIO2 = new cCheckInOut { Vao = null, Raa = chk_1, CheckVT = TrangThaiCheck.ThieuVao, TimeDaiDien = chk_1.Time, };
					ds_CIO_A.Add(CIO2);
				}
			}
		}

		public void XetCa_ListCIO_A3_V6(List<cCheckInOut> ds_CIO_A, List<cCa> DSCa) {
			try {
				var i = 0;
				while (i < ds_CIO_A.Count) {
					var CIO = ds_CIO_A[i];

					#region nếu giờ quên check thì chỉ kiểm tra khoảng hiểu ca

					if (CIO.CheckVT != TrangThaiCheck.CheckDayDu) {
						CIO.ThuocNgayCong = ThuocNgayCong(CIO.TimeDaiDien);
						Tim_DSCa_NhanDienDuoc(CIO.TimeDaiDien, CIO.ThuocNgayCong, CIO.CheckVT, DSCa, out CIO.DSCaNhanDien);
						i++;
						continue;
					}

					#endregion

					var ngay = ThuocNgayCong(CIO.TimeDaiDien);
					CIO.ThuocNgayCong = ngay;
					bool namTrongDSCa;
					cCa caNhanDien;
					KiemtraThuocCa(CIO.Vao.Time, CIO.Raa.Time, CIO.ThuocNgayCong, DSCa, out namTrongDSCa, out caNhanDien);

					#region nếu thuộc khoảng hiểu ca thì set ca

					if (namTrongDSCa && caNhanDien.TachCaDem == false) {// ko phải ca 3 và 1
						CIO.ThuocCa = caNhanDien;
						i++;
					}
					else if (namTrongDSCa == false) { // ko thuộc ca nào -> ca tự do
						cCa caTuDo;
						TaoCaTuDo(int.MinValue, CIO.Vao.Time, out caTuDo);
						CIO.ThuocCa = caTuDo;
						i++;
					}
					else { // trúng ca 3 và 1 thì tách ra 2 ca
						#region check inn, check out vao 3 ra 3, vao 1 ra 1

						var vaoca3 = CIO.Vao;
						var raaca3 = new cCheck {
							Type = "O",
							MachineNo = 22,
							Source = "PC",
							MaCC = CIO.Vao.MaCC,
							TypeColumn = "O",
							Time = ngay.Add(GlobalVariables._22h00)
						};
						var vaoca1 = new cCheck {
							Type = "I",
							MachineNo = 21,
							Source = "PC",
							MaCC = CIO.Raa.MaCC,
							TypeColumn = "I",
							Time = ngay.Date.Add(GlobalVariables._6gHomSau).Add(GlobalVariables._01giay), //todo lưu ý ở đây cộng thêm 1 ngày, 1 giây để ko bị trùng check cùng giờ 2 máy
						};
						var raaca1 = CIO.Raa;

						#endregion

						// do tách ra thành 2 CIO mới nên phải gán lại IsEdited cho từng cái
						bool thuocDSCa3 = false, thuocDSCa1 = false;
						cCa thuocCa3, thuocCa1, caTuDo3, caTuDo1;

						ds_CIO_A[i] = new cCheckInOut { Vao = vaoca3, Raa = raaca3, ThuocNgayCong = ngay, TimeDaiDien = vaoca3.Time, };
						KiemtraThuocCa(vaoca3.Time, raaca3.Time, ds_CIO_A[i].ThuocNgayCong, DSCa, out thuocDSCa3, out thuocCa3);
						if (thuocDSCa3) ds_CIO_A[i].ThuocCa = thuocCa3;
						else {
							TaoCaTuDo(int.MinValue, ds_CIO_A[i].Vao.Time, out caTuDo3);
							ds_CIO_A[i].ThuocCa = caTuDo3;
						}

						var newCIO = new cCheckInOut { Vao = vaoca1, Raa = raaca1, ThuocNgayCong = ngay.AddDays(1d), TimeDaiDien = vaoca1.Time, };
						KiemtraThuocCa(vaoca1.Time, raaca1.Time, newCIO.ThuocNgayCong, DSCa, out thuocDSCa1, out thuocCa1);
						if (thuocDSCa1) newCIO.ThuocCa = thuocCa1;
						else {
							TaoCaTuDo(int.MinValue, newCIO.Vao.Time, out caTuDo1);
							newCIO.ThuocCa = caTuDo1;
						}

						// vì hàm insert ko cho phép chèn ở vị trí > số lượng phần tử
						// => nên nếu i là phần tử cuối thì add vào cuối danh sách, ngược lại thì insert vào vị trí i+1
						if (i == (ds_CIO_A.Count - 1)) ds_CIO_A.Add(newCIO);
						else ds_CIO_A.Insert(i + 1, newCIO);
						i = i + 2; // +2 vì i là ca3, i+1 là ca 1
					}
					#endregion

				}

			}
			catch (Exception e) {
				//lg.Error(string.Format("[{0}]_[{1}]\n", "XLChamCong", System.Reflection.MethodBase.GetCurrentMethod().Name), e);
			}
		}

		public void TinhTGLV(IEnumerable<cCheckInOut> DS_CIO) {
			foreach (cCheckInOut CIO in DS_CIO) {
				TinhTGLV_Cong(CIO);
			}
		}

		public void TinhTGLV_Cong(cCheckInOut CIO) {
			CIO.TD = new ThoiDiem();
			CIO.KhoangTGCa = new StructTGCa();
			CIO.CongTheoCa = new StructCongCa();
			if (CIO.CheckVT == TrangThaiCheck.ThieuVao || CIO.CheckVT == TrangThaiCheck.ThieuRa || CIO.ThuocCa.ID == int.MinValue) return;//todo test
			TinhTG_LV_LVCa3_LamThem1Ca(CIO, CIO.ThuocCa, CIO.ThuocCa.NightTime);
			TinhCong(CIO, CIO.ThuocCa);
		}

		private void TinhCong(cCheckInOut CIO, cCa Ca) {
			TinhCong(CIO.KhoangTGCa.Tre, CIO.KhoangTGCa.Som, CIO.ChoPhepTre, CIO.ChoPhepSom, true, CIO.RaaTuDo,
				CIO.KhoangTGCa.LamTrongGio, CIO.KhoangTGCa.LamNgoaiGio, Ca.WorkingTimeTS, Ca.Workingday,
				out CIO.CongTheoCa.TruCongTre, out CIO.CongTheoCa.TruCongSom, out CIO.CongTheoCa.TrongGio, out CIO.CongTheoCa.NgoaiGio,
				out CIO.CongTheoCa.DinhMuc, out CIO.CongTheoCa.Tong);
		}

		private void TinhCong(TimeSpan TreVR, TimeSpan SomVR, bool ChoPhepTre, bool ChoPhepSom, bool VaoTuDo, bool RaaTuDo,
			TimeSpan LamTrongGio, TimeSpan LamNgoaiGio, TimeSpan WorkingTimeTS, float Workingday,
			out float TruCongTre, out float TruCongSom, out float TrongGio, out float NgoaiGio, out float DinhMuc, out float TongCong) {
			TruCongTre = TinhToan_CongTre_Som(TreVR, Workingday, WorkingTimeTS);
			TruCongSom = TinhToan_CongTre_Som(SomVR, Workingday, WorkingTimeTS);
			NgoaiGio = TinhToan_CongNgoaiGio(LamNgoaiGio);
			TrongGio = TinhCongTrongGio(Workingday, TruCongTre, ChoPhepTre, TruCongSom, ChoPhepSom);
			DinhMuc = TinhDinhMucCong(Workingday, NgoaiGio, TruCongTre, ChoPhepTre, VaoTuDo, TruCongSom, ChoPhepSom, RaaTuDo);
			TongCong = TrongGio + NgoaiGio;
		}

		public void TinhTG_LV_LVCa3_LamThem1Ca(cCheckInOut CIO, cCa ca, FromToTimeSpan NightTime) {
			bool tempQuaDem;
			TinhTG_LV_LVCa3_LamThem1Ca(CIO.ThuocNgayCong, CIO.CheckVT,
				CIO.Vao.Time, CIO.Raa.Time, ca.Duty.From, ca.Duty.To, ca.ChoPhepTre_TimeOfDay, ca.ChophepSom_TimeOfDay, ca.BatdauOT_TimeOfDay, ca.LunchMin, NightTime,
				out CIO.TD.VaoLamTron, out CIO.TD.RaaLamTron,
				out CIO.TD.BD_LV, out CIO.TD.KT_LV_TrongCa, out CIO.TD.KT_LV, out CIO.TD.BD_LV_Ca3, out CIO.TD.KT_LV_Ca3,
				out CIO.KhoangTGCa.HienDien, out CIO.KhoangTGCa.VaoSauCa, out CIO.KhoangTGCa.RaTruocCa,
				out CIO.KhoangTGCa.Tre, out CIO.KhoangTGCa.Som,
				out CIO.KhoangTGCa.OLaiVR, out CIO.KhoangTGCa.LamTrongGio, out tempQuaDem, out CIO.KhoangTGCa.LamDem);
			CIO.QuaDem = tempQuaDem;// ko cho phép out CIO.QuaDem nên fix tạm bằng cách dùng biến trung gian cục bộ và gán lại
		}

		public void TinhTG_LV_LVCa3_LamThem1Ca(DateTime ThuocNgayCong, TrangThaiCheck CheckVT,
			DateTime TD_Vao, DateTime TD_Raa,
			TimeSpan BDCa_TOD, TimeSpan KTCa_TOD, TimeSpan ChoPhepTre_TimeOfDay, TimeSpan ChophepSom_TimeOfDay, TimeSpan BatdauOT_TimeOfDay,
			TimeSpan Phut_NghiTrua, FromToTimeSpan NightTime,
			out DateTime TD_Vao_lamTron, out DateTime TD_Raa_lamTron,
			out DateTime TD_BD_LV, out DateTime TD_KT_LV_TrongCa, out DateTime TD_KT_LV, out DateTime TD_BD_LV_Ca3, out DateTime TD_KT_LV_Ca3,
			out TimeSpan TGHienDien, out TimeSpan TGVaoSauCa, out TimeSpan TGRaTruocCa,
			out TimeSpan TGVaoTreVR, out TimeSpan TGRaaSomVR, out TimeSpan TG_OLai_VR,
			out TimeSpan TGLamViecTrongCa, /*out TimeSpan TGLamViecNgoaiGio, ko có TGLamViecNgoaiGio vì chưa xử lý*/
			out bool QuaDem, out TimeSpan TGLamDem) {

			TD_Vao_lamTron = MyUtility.LamTronPhut(TD_Vao);
			TD_Raa_lamTron = MyUtility.LamTronPhut(TD_Raa);
			TD_BD_LV = ThuocNgayCong;
			TD_KT_LV_TrongCa = ThuocNgayCong;
			TD_KT_LV = ThuocNgayCong;
			TD_BD_LV_Ca3 = ThuocNgayCong;
			TD_KT_LV_Ca3 = ThuocNgayCong;
			TGHienDien = TimeSpan.Zero;
			TGVaoSauCa = TimeSpan.Zero;
			TGRaTruocCa = TimeSpan.Zero;
			TGVaoTreVR = TimeSpan.Zero;
			TGRaaSomVR = TimeSpan.Zero;
			TG_OLai_VR = TimeSpan.Zero;
			TGLamViecTrongCa = TimeSpan.Zero; //ko có TG làm việc ngoài giờ
			TGLamDem = TimeSpan.Zero;
			QuaDem = false;

			if (CheckVT != TrangThaiCheck.CheckDayDu) return;

			var TD_BD_Ca = ThuocNgayCong.Add(BDCa_TOD);
			var TD_KT_Ca = ThuocNgayCong.Add(KTCa_TOD);//off duty này đã bao gồm daycount được công bên trong
			var thoidiem_BD_tinhtre = ThuocNgayCong.Add(ChoPhepTre_TimeOfDay);
			var thoidiem_BD_tinhsom = ThuocNgayCong.Add(ChophepSom_TimeOfDay);
			var thoidiem_BD_tinhOLai = ThuocNgayCong.Add(BatdauOT_TimeOfDay);
			var thoidiem_QuyDinhBDLamDem = ThuocNgayCong.Add(NightTime.From);//ver 4.0.0.4
			var thoidiem_QuyDinhKTLamDem = ThuocNgayCong.Add(NightTime.To);//ver 4.0.0.4 //todo lưu ý đã có 1 ngày vì Global Variable =6gHôm sau

			bool quadem;

			TGHienDien = TD_Raa_lamTron - TD_Vao_lamTron;
			// kiểm tra giờ ra ko được nhỏ hơn vào ca, giờ vào ko được nhỏ hơn ra ca
			if (TD_Raa_lamTron <= TD_BD_Ca || TD_Vao_lamTron >= TD_KT_Ca) {
				return;
			}

			XacDinh_TD_BDLV(TD_Vao_lamTron, TD_BD_Ca, thoidiem_BD_tinhtre, out TD_BD_LV, out TGVaoSauCa);
			TGVaoTreVR = (thoidiem_BD_tinhtre < TD_Vao_lamTron) ? (TD_Vao_lamTron - TD_BD_Ca) : TimeSpan.Zero;
			XacDinh_TD_KTLV_TrongCa(TD_Raa_lamTron, TD_KT_Ca, thoidiem_BD_tinhsom, out TD_KT_LV_TrongCa, out TGRaTruocCa);
			TGRaaSomVR = (TD_Raa_lamTron < thoidiem_BD_tinhsom) ? (TD_KT_Ca - TD_Raa_lamTron) : TimeSpan.Zero;
			XacDinh_KTG_OLai(TD_Raa_lamTron, TD_KT_Ca, thoidiem_BD_tinhOLai, out TG_OLai_VR);
			Tinh_TGLamViecTrongCa(TD_BD_LV, TD_KT_LV_TrongCa, Phut_NghiTrua, out TGLamViecTrongCa);
			TD_KT_LV = TD_KT_LV_TrongCa;			//Do chưa xác nhận nên giờ làm thêm nên TD_KT_LV = TD_KT_LV_TrongCa
			Tinh_TGLamViec_Ca3(TD_BD_LV, TD_KT_LV, thoidiem_QuyDinhBDLamDem, thoidiem_QuyDinhKTLamDem, out TD_BD_LV_Ca3, out TD_KT_LV_Ca3, out TGLamDem, out quadem);
			QuaDem = quadem;
		}

		#region các hàm tính thời điểm bắt đầu và kết thúc làm việc
		public float TinhToan_CongTre_Som(TimeSpan TGTreSom, float Workingday, TimeSpan WorkingTimeTS) {
			var DoubleTruTre_Som = (TGTreSom.TotalHours * Convert.ToDouble(Workingday)) / WorkingTimeTS.TotalHours;
			return DoubleTruTre_Som.Truncate(2);
		}
		public float TinhToan_CongNgoaiGio(TimeSpan LamNgoaiGio) {
			var DoubleNgoaiGio = (LamNgoaiGio.TotalHours) / 8d;
			return DoubleNgoaiGio.Truncate(2);
		}
		public float TinhCongTrongGio(float CongCaQuyDinh, float TruCongTre, bool ChoPhepTre, float TruCongSom, bool ChoPhepSom) {
			var CongTrongGio = CongCaQuyDinh;
			if (ChoPhepTre == false) CongTrongGio -= TruCongTre;
			if (ChoPhepSom == false) CongTrongGio -= TruCongSom;
			return CongTrongGio;
		}
		public float TinhDinhMucCong(float CongCaQuyDinh, float CongNgoaiGio, float TruCongTre, bool ChoPhepTre, bool VaoTuDo, float TruCongSom, bool ChoPhepSom, bool RaaTuDo) {
			var DinhMucCong = CongCaQuyDinh + CongNgoaiGio;
			if (ChoPhepTre == false && VaoTuDo) DinhMucCong -= TruCongTre;
			if (ChoPhepSom == false && RaaTuDo) DinhMucCong -= TruCongSom;
			return DinhMucCong;
		}

		public void XacDinh_TD_BDLV(DateTime TD_VaoLamTron, DateTime TD_BDCa, DateTime TD_Chopheptre, out DateTime TD_BDLV, out TimeSpan VaoSauCa) {
			if (TD_VaoLamTron - TD_BDCa > TimeSpan.Zero) {
				VaoSauCa = TD_VaoLamTron - TD_BDCa;
				VaoSauCa = MyUtility.LamTronPhut(VaoSauCa);
			}
			else VaoSauCa = TimeSpan.Zero;
			//cho trễ ... vào --> trễ , lấy vào làm tròn;
			// vào ... cho trễ --> ko trễ, lấy bđ ca
			TD_BDLV = (TD_Chopheptre < TD_VaoLamTron /*&& TD_VaoLamTron - TD_Chopheptre > TimeSpan.Zero*/) ? TD_VaoLamTron : TD_BDCa;
		}

		public void XacDinh_TD_KTLV_TrongCa(DateTime TD_Raa_LamTron, DateTime TD_KTCa, DateTime TD_chophepsom, out DateTime TD_KTLVTrongCa, out TimeSpan RaTruocCa) {
			if (TD_KTCa - TD_Raa_LamTron > TimeSpan.Zero) {
				RaTruocCa = TD_KTCa - TD_Raa_LamTron;
				RaTruocCa = MyUtility.LamTronPhut(RaTruocCa);
			}
			else RaTruocCa = TimeSpan.Zero;
			// ra ... choPhép sớm --> ra sớm, lấy giờ ra làm tròn
			// cho phép sớm .. ra --> ko sớm, lấy giờ kt ca
			TD_KTLVTrongCa = (TD_Raa_LamTron < TD_chophepsom /*&& TD_chophepsom - TD_Raa_LamTron > TimeSpan.Zero*/) ? TD_Raa_LamTron : TD_KTCa;
		}

		public static void XacDinh_KTG_OLai(DateTime TD_Raa_LamTron, DateTime TD_KTCa, DateTime TD_BD_TinhOT, out TimeSpan OLaiVR) {
			// ra ... BĐ OT --> ko đủ thời gian tối thiểu OT --> lấy ra làm tròn
			// BĐOT ... ra --> có OT, lấy Ra trừ BĐCa
			OLaiVR = (TD_Raa_LamTron < TD_BD_TinhOT) ? TimeSpan.Zero : (TD_Raa_LamTron - TD_KTCa);
		}

		public static void Tinh_TGLamViecTrongCa(DateTime tdBdLv, DateTime tdKtLvTrongCa, TimeSpan lunchMin, out TimeSpan tgGioLamViecTrongCa) {//ver 4.0.0.4	
			tgGioLamViecTrongCa = (tdKtLvTrongCa - tdBdLv).Subtract(lunchMin);
		}

		public static void Tinh_TGLamViec_Ca3(DateTime TD_BDLV, DateTime TD_KTLV, DateTime startNT, DateTime endddNT,
			out DateTime TD_BTLVCa3, out DateTime TD_KTLV_Ca3, out TimeSpan TGLamDem, out bool QuaDem) {
			TimeSpan tempTGLamDem;
			var BDLamDem = DateTime.MinValue;
			var KTLamDem = DateTime.MinValue;
			if (TD_KTLV > startNT) { // bắt đầu làm việc phải sau 22h mới tính qua đêm
				BDLamDem = TD_BDLV > startNT ? TD_BDLV : startNT; // vào sau 22h lấy giờ vào, vào trước 22h lấy 22h
				KTLamDem = TD_KTLV < endddNT ? TD_KTLV : endddNT;// ra trước 6g lấy giờ ra, ra sau 6g lấy 6g
				tempTGLamDem = KTLamDem - BDLamDem;
			}
			else tempTGLamDem = TimeSpan.Zero;
			if (tempTGLamDem < GlobalVariables.default_PhutLamDemToiThieu) { // ko đủ làm đêm tối thiểu thì trả về 0
				TD_BTLVCa3 = DateTime.MinValue;
				TD_KTLV_Ca3 = DateTime.MinValue;
				TGLamDem = TimeSpan.Zero;
				QuaDem = false;
			}
			else {
				TD_BTLVCa3 = BDLamDem;
				TD_KTLV_Ca3 = KTLamDem;
				TGLamDem = tempTGLamDem;
				QuaDem = true;
			}
		}

		public static TimeSpan Tinh_TGLamThem(TimeSpan TGGioLamViec) {
			return (TGGioLamViec - GlobalVariables._08gio) >= GlobalVariables._01phut ? (TGGioLamViec - GlobalVariables._08gio) : TimeSpan.Zero;
		}

		#endregion



		public void Tim_DSCa_NhanDienDuoc(DateTime time, DateTime ngay, TrangThaiCheck CheckVT, List<cCa> DSCa, out List<cCa> Result) {
			Result = new List<cCa>();
			Result = (CheckVT == TrangThaiCheck.ThieuRa)
						 ? DSCa.FindAll(ca => time >= ngay.Add(ca.NhanDienVao.From) && time <= ngay.Add(ca.NhanDienVao.To))
						 : DSCa.FindAll(ca => time >= ngay.Add(ca.NhanDienRaa.From) && time <= ngay.Add(ca.NhanDienRaa.To));

		}

		public static DateTime ThuocNgayCong(DateTime chkinn)// lưu ý không dùng cho các giờ ra, giờ ra thiếu vào, giờ vào thiếu ra
		{
			return chkinn.TimeOfDay < GlobalVariables._03gio ? chkinn.Date.AddDays(-1) : chkinn.Date;
		}

		public static void KiemtraThuocCa(DateTime t_vao, DateTime t_raa, DateTime ngay, List<cCa> DSCa, out bool ThuocDSCa, out cCa Result) {
			ThuocDSCa = true;
			Result = DSCa.FirstOrDefault(ca => t_vao >= ngay.Add(ca.NhanDienVao.From) && t_vao <= ngay.Add(ca.NhanDienVao.To)
											 && t_raa >= ngay.Add(ca.NhanDienRaa.From) && t_raa <= ngay.Add(ca.NhanDienRaa.To));
			if (Result == null) {
				ThuocDSCa = false;
			}


		}

		public void TaoCaTuDo(int ID, DateTime CheckInTime, out cCa Ca) {
			//var temp = CheckInTime.TimeOfDay;//ver 4.0.0.0//tbd xem lại ngày công
			Ca = new cCa();
			var gioVaoLamTron = MyUtility.LamTronPhut(CheckInTime.TimeOfDay);//ver 4.0.0.1 bỏ phần giây, chỉ giữ phần giờ, phút
			//if (CheckInTime.TimeOfDay < GlobalVariables._03gio) temp = Ca.Duty.From.Add(GlobalVariables._1ngay); //ca 3 , ca 3 va 1 vẫn giữ nguyên vì 21h > 4h//tbd xem lại ngày công
			if (Ca.ID == int.MinValue + 0) {
				Ca.Duty = new FromToTimeSpan { From = gioVaoLamTron, To = gioVaoLamTron.Add(GlobalVariables._08gio) };
				Ca.WorkingTimeTS = GlobalVariables._08gio;
				Ca.Workingday = 1f;
				Ca.Code = Properties.Settings.Default.shiftCodeCa8h;//todo xem lại
				Ca.MoTa = string.Format(Properties.Settings.Default.MoTaCaTuDo, 8); //todo xem lauị
				Ca.KyHieuCC = Properties.Settings.Default.kyHieuCCCa8h;
			}
			Ca.Is_CaTuDo = true;
			Ca.TachCaDem = false;
			Ca.NightTime = GlobalVariables.NightTime22h;
			Ca.LateeMin = GlobalVariables.default_LateMin;
			Ca.EarlyMin = GlobalVariables.default_EarlyMin;
			Ca.AfterOTMin = GlobalVariables.default_AfterOTMin;
			Ca.LunchMin = GlobalVariables.default_LunchMin;
			Ca.DayCount = Ca.Duty.To.Days;
			Ca.QuaDem = (Ca.Duty.To.Days == 1);
			Ca.OnTimeInMin = 5;//setting nhưng ko ý nghĩa
			Ca.CutInMin = 5;//setting nhưng ko ý nghĩa
			Ca.OnTimeOutMin = 5;//setting nhưng ko ý nghĩa
			Ca.CutOutMin = 5;//setting nhưng ko ý nghĩa
		}

	}
}
