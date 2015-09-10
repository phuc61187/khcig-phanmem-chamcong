using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ChamCong_v06.BUS;
using ChamCong_v06.DTO;
using ChamCong_v06.Helper;

namespace ChamCong_v06.DAL {
	public partial class DAL_CheckInCheckOut {
		public void GetCheckInCheckOutData(FromToDateTime KhoangThoiGian, List<int> ArrayUEN, out List<cCheck> ResultListCheck) {
			DataTable tableArrayUEN = MyUtility.Array_To_DataTable("tableName", ArrayUEN);
			DataTable tableCheck = SqlDataAccessHelper.ExecSPQuery(SPName6.CheckInOut_DocCheckChuaXuLyV6.ToString(),
																   new SqlParameter("@From", KhoangThoiGian.From),
																   new SqlParameter("@To", KhoangThoiGian.To),
																   new SqlParameter { ParameterName = "@ArrayUserEnrollNumber", SqlDbType = SqlDbType.Structured, Value = tableArrayUEN },
																   new SqlParameter("@DaChamCong", false),
																   new SqlParameter("@Loai", false));
			ResultListCheck = new List<cCheck>();
			foreach (DataRow dataRow in tableCheck.Rows) {
				cCheck check = new cCheck {
					MaCC = (int)dataRow["UserEnrollNumber"],
					Source = dataRow["Source"].ToString(),
					Time = (DateTime)dataRow["TimeStr"],
					MachineNo = (int)dataRow["MachineNo"],
					TypeColumn = dataRow["OriginType"].ToString()
				};
				check.Type = (check.MachineNo % 2 == 1) ? "I" : "O";
				ResultListCheck.Add(check);
			}

		}

		internal void LoaiCheckTrong30ph(List<cCheck> DSCheck_BiLoai_All) {
			int kq = 0;
			bool flag = false;
			foreach (cCheck check in DSCheck_BiLoai_All) {
				kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.CheckInOut_LoaiCheck_KoHopLeV6.ToString(),
														 new SqlParameter("@UserEnrollNumber", check.MaCC),
														 new SqlParameter("@TimeStr", check.Time),
														 new SqlParameter("@MachineNo", check.MachineNo),
														 new SqlParameter("@Loai", true),
														 new SqlParameter("@DaChamCong", true));
				if (kq == 0) {
					flag = true;
				}
			}
			if (flag) {
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
			}
		}

		public void Insert_CheckInOutData(int UEN, IEnumerable<cCheckInOut> DS_CIO) {
			foreach (cCheckInOut CIO in DS_CIO) {
				if (KiemTraThoaDieuKienThemCIO(CIO)) {
					Insert_CheckInOutData(UEN, CIO);
					if (CIO.Vao != null) Update_Check_DaChamCong(UEN, CIO.Vao);
					if (CIO.Raa != null) Update_Check_DaChamCong(UEN, CIO.Raa);
				}
			}
		}

		private void Update_Check_DaChamCong(int UEN, cCheck check) {

			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.CheckInOut_Upd_Check_DaChamCongV6.ToString(),
														 new SqlParameter("@UserEnrollNumber", UEN),
														 new SqlParameter("@TimeStr", check.Time),
														 new SqlParameter("@MachineNo", check.MachineNo),
														 new SqlParameter("@DaChamCong", true));
			if (kq == 0) {
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
			}
		}


		private bool KiemTraThoaDieuKienThemCIO(cCheckInOut cio) {
			//logic: nếu check đủ vào ra, check ra thì thêm xuống csdl, 
			//nếu check thiếu ra thì kiểm tra trên 22 tiếng mới cho thêm, dưới 22 tiếng thì vẫn có thể là trường hợp đang ở trong nhà máy
			if (cio.CheckVT == TrangThaiCheck.CheckDayDu || cio.CheckVT == TrangThaiCheck.ThieuVao
				|| ((cio.CheckVT == TrangThaiCheck.ThieuRa) && (((DateTime.Now - cio.TimeDaiDien).Duration() > GlobalVariables._22h00)))) return true;
			return false;
		}

		public void Insert_CheckInOutData(int UEN, cCheckInOut CIO) {
			int phut_Vao = 0, phut_Ra = 0, phut_BD_LV = 0, phut_KT_LV_TrongCa = 0, phut_KT_LV = 0, phut_BD_LV_Ca3 = 0, phut_KT_LV_Ca3 = 0;
			int phut_TreVR = 0, phut_SomVR = 0, phut_LamViec = 0, phut_LamDem = 0, phut_OLaiVR = 0, phut_LamTrongGio = 0, phut_LamNgoaiGio = 0,
				phut_VaoSauCa = 0, phut_RaTruocCa = 0;
			float congTrongGio = 0f, congNgoaiGio = 0f, truCongTre = 0f, truCongSom = 0f;
			bool quaDem = false;
			if (CIO.CheckVT == TrangThaiCheck.CheckDayDu) {
				phut_Vao = MyUtility.QuyDoiPhut((CIO.BD_LV - CIO.ThuocNgayCong));
				phut_Ra = MyUtility.QuyDoiPhut((CIO.KT_LV - CIO.ThuocNgayCong));
				phut_BD_LV = MyUtility.QuyDoiPhut((CIO.BD_LV - CIO.ThuocNgayCong));
				phut_KT_LV = MyUtility.QuyDoiPhut((CIO.KT_LV - CIO.ThuocNgayCong));
				phut_KT_LV_TrongCa = MyUtility.QuyDoiPhut((CIO.KT_LV_TrongCa - CIO.ThuocNgayCong));
				phut_BD_LV_Ca3 = (CIO.BD_LV_Ca3 == DateTime.MinValue) ? 0 : MyUtility.QuyDoiPhut((CIO.BD_LV_Ca3 - CIO.ThuocNgayCong));
				phut_KT_LV_Ca3 = (CIO.KT_LV_Ca3 == DateTime.MinValue) ? 0 : MyUtility.QuyDoiPhut((CIO.KT_LV_Ca3 - CIO.ThuocNgayCong));
				phut_TreVR = MyUtility.QuyDoiPhut(CIO.Tre);
				phut_SomVR = MyUtility.QuyDoiPhut(CIO.Som);
				phut_VaoSauCa = MyUtility.QuyDoiPhut(CIO.VaoSauCa);
				phut_RaTruocCa = MyUtility.QuyDoiPhut(CIO.RaTruocCa);
				congTrongGio = CIO.TrongGio;
				congNgoaiGio = CIO.NgoaiGio;
				truCongTre = CIO.TruCongTre;
				truCongSom = CIO.TruCongSom;
				quaDem = CIO.QuaDem;
			}
			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.CIO_ThemCIOChuaChamCongV6.ToString(),
														 new SqlParameter("@UserEnrollNumber", UEN),
														 new SqlParameter("@NgayCong", CIO.ThuocNgayCong),
														 new SqlParameter("@HaveINOUT", (int)CIO.CheckVT),
														 new SqlParameter("@GioVao", CIO.Vao == null ? (object)DBNull.Value : CIO.Vao.Time),
														 new SqlParameter("@GioRa", CIO.Raa == null ? (object)DBNull.Value : CIO.Raa.Time),
														 new SqlParameter("@Vao", phut_Vao),
														 new SqlParameter("@Ra", phut_Ra),
														 new SqlParameter("@MayVao", CIO.Vao == null ? (object)DBNull.Value : CIO.Vao.MachineNo),
														 new SqlParameter("@MayRa", CIO.Raa == null ? (object)DBNull.Value : CIO.Raa.MachineNo),
														 new SqlParameter("@BDLV", phut_BD_LV),
														 new SqlParameter("@KTLVTrongCa", phut_KT_LV_TrongCa),
														 new SqlParameter("@KTLV", phut_KT_LV),
														 new SqlParameter("@BDLVCa3", phut_BD_LV_Ca3),
														 new SqlParameter("@KTLVCa3", phut_KT_LV_Ca3),
														 new SqlParameter("@QuaDem", quaDem),
														 new SqlParameter("@Tre", phut_TreVR),
														 new SqlParameter("@Som", phut_SomVR),
														 new SqlParameter("@VaoSauCa", phut_VaoSauCa),
														 new SqlParameter("@RaTruocCa", phut_RaTruocCa),
														 new SqlParameter { ParameterName = "@SoPhutXacNhanNgoaiGio", Value = 0 }, // lần đầu chưa xác nhận nên = 0
														 new SqlParameter("@ChoPhepTre", CIO.ChoPhepTre), // lần đầu chưa xác nhận nên = false
														 new SqlParameter("@ChoPhepSom", CIO.ChoPhepSom), // lần đầu chưa xác nhận nên = false
														 new SqlParameter("@VaoTuDo", CIO.VaoTuDo), // lần đầu chưa xác nhận nên = false
														 new SqlParameter("@RaTuDo", CIO.RaaTuDo), // lần đầu chưa xác nhận nên = false
														 new SqlParameter("@CongTrongGio", congTrongGio),
														 new SqlParameter("@CongNgoaiGio", congNgoaiGio),
														 new SqlParameter("@TruCongTre", truCongTre),
														 new SqlParameter("@TruCongSom", truCongSom),
														 new SqlParameter{ ParameterName = "@TinhCongThuCong", Value = false, },
														 new SqlParameter{ ParameterName = "@ChamCongTay", Value = 0f, },
														 new SqlParameter("@DinhMucCong", CIO.DinhMuc),
														 new SqlParameter("@TongCong", CIO.Tong),
														 new SqlParameter("@TheoDoiGioGocMayCC", string.Empty)//todo ghi phần theo dõi
				// ko có GhiChu, LyDo
				);
			if (kq == 0) { ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000); }
		}

		//public void

		internal void GetCIOData(DataTable tableArrayUEN, FromToDateTime KhoangTG, out List<cCheckInOut_DaCC> DS_CIO_DaCC) {
			DataTable tableCIO = SqlDataAccessHelper.ExecSPQuery(SPName6.CIO_GetData_V6.ToString(),
				new SqlParameter { ParameterName = "@Array_UserEnrollNumber", Value = tableArrayUEN, SqlDbType = SqlDbType.Structured },
													 new SqlParameter("@From", KhoangTG.From),
													 new SqlParameter("@To", KhoangTG.To));
			DS_CIO_DaCC = new List<cCheckInOut_DaCC>();
			foreach (DataRow row in tableCIO.Rows) {
				cCheckInOut_DaCC cio = new cCheckInOut_DaCC();
				cio.ID = (int)row["ID"];
				cio.MaCC = (int)row["UserEnrollNumber"];
				cio.Ngay = (DateTime)row["NgayCong"];
				if (row["GioVao"] != DBNull.Value) cio.GioVao = (DateTime)row["GioVao"];if (row["GioRa"] != DBNull.Value) cio.GioRaa = (DateTime)row["GioRa"];
				cio.CheckVT = TrangThaiCheckVT((int)row["HaveINOUT"]);
				cio.Tre_Min = (int)row["Tre"];
				cio.Som_Min = (int)row["Som"];
				if (row["VaoSauCa"] != DBNull.Value) cio.VaoSauCa = new TimeSpan(0, (int)row["VaoSauCa"], 0);
				if (row["RaTruocCa"] != DBNull.Value) cio.RaTruocCa = new TimeSpan(0, (int)row["RaTruocCa"], 0);

				cio.VaoLamTron_Min = (int)row["Vao"];
				cio.RaaLamTron_Min = (int)row["Ra"];
				cio.BD_LV_Min = (int)row["BDLV"];
				cio.KT_LV_TrongCa_Min = (int)row["KTLVTrongCa"];
				cio.KT_LV_Min = (int)row["KTLV"];
				cio.BD_LV_Ca3_Min = (int)row["BDLVCa3"];
				cio.KT_LV_Ca3_Min = (int)row["KTLVCa3"];
				cio.QuaDem = (bool) row["QuaDem"];
				cio.ChoPhepTre = (bool)row["ChoPhepTre"];
				cio.ChoPhepSom = (bool)row["ChoPhepSom"];
				cio.VaoTuDo = (bool)row["VaoTuDo"];
				cio.RaaTuDo = (bool)row["RaTuDo"];
				cio.CongTrongGio = (float)row["CongTrongGio"];
				cio.CongNgoaiGio = (float)row["CongNgoaiGio"];
				cio.TruCongTre = (float)row["TruCongTre"];
				cio.TruCongSom = (float)row["TruCongSom"];
				cio.TinhCongThuCong = (bool)row["TinhCongThuCong"];
				cio.ChamCongTay = (float)row["ChamCongTay"];
				cio.CapNhatDinhMucCong();
				cio.CapNhatDinhTongCong();
				DS_CIO_DaCC.Add(cio);
			}
		}

		private TrangThaiCheck TrangThaiCheckVT(int p) {
			if (p == (int)TrangThaiCheck.ThieuVao) return TrangThaiCheck.ThieuVao;
			else if (p == (int)TrangThaiCheck.ThieuRa) return TrangThaiCheck.ThieuRa;
			return TrangThaiCheck.CheckDayDu;
		}

		public void GetXacNhanPhuCapNgayData(DataTable tableArrayUEN, FromToDateTime KhoangTG, out List<cXacNhanPhuCapNgay> DS_XN_PC_Ngay) {
			DataTable tableNgayCong = SqlDataAccessHelper.ExecSPQuery(SPName6.NgayCong_LayV6.ToString(),
															new SqlParameter { ParameterName = "@Array_UserEnrollNumber", Value = tableArrayUEN, SqlDbType = SqlDbType.Structured },
															new SqlParameter("@From", KhoangTG.From),
															new SqlParameter("@To", KhoangTG.To));
			DS_XN_PC_Ngay = new List<cXacNhanPhuCapNgay>();
			foreach (DataRow dataRow in tableNgayCong.Rows) {
				cXacNhanPhuCapNgay item = new cXacNhanPhuCapNgay();
				item.MaCC = (int)dataRow["UserEnrollNumber"];
				item.Ngay = (DateTime)dataRow["Ngay"];
				item.TinhPCTC = (bool)dataRow["TinhPCTC"];
				item.TinhPCNgayNghi = (bool)dataRow["TinhPCNgayNghi"];
				item.TinhPCNgayLe = (bool)dataRow["TinhPCNgayLe"];
				item.TinhPCTay = (bool)dataRow["TinhPCThuCong"];
				item.TongPhuCap = (dataRow["TongPC"] != DBNull.Value) ? (float)dataRow["TongPC"] : 0f;
				if (item.TinhPCTay) item.PhuCapTay = item.TongPhuCap;
				DS_XN_PC_Ngay.Add(item);
			}
		}

		public void GetNgayVangData(DataTable tableArrayUEN, FromToDateTime KhoangTG, out List<cKhaiBaoVang> DS_KhaiBaoVang) {
			DataTable tableKhaiBaoVang = SqlDataAccessHelper.ExecSPQuery(SPName6.Absent_GetDataV6.ToString(),
															   new SqlParameter { ParameterName = "@Array_UserEnrollNumber", Value = tableArrayUEN, SqlDbType = SqlDbType.Structured },
															   new SqlParameter("@From", KhoangTG.From),
															   new SqlParameter("@To", KhoangTG.To));
			DS_KhaiBaoVang = new List<cKhaiBaoVang>();
			foreach (DataRow dataRow in tableKhaiBaoVang.Rows) {
				cKhaiBaoVang khaiBaoVang = new cKhaiBaoVang();
				khaiBaoVang.MaCC = (int)dataRow["UserEnrollNumber"];
				khaiBaoVang.Ngay = (DateTime)dataRow["TimeDate"];
				khaiBaoVang.Workingday = (float)dataRow["Workingday"];
				khaiBaoVang.WorkingTime = (float)dataRow["WorkingTime"];
				DS_KhaiBaoVang.Add(khaiBaoVang);
			}
		}

		internal void GetNgayLeData(FromToDateTime KhoangTG, out List<DateTime> listNgayLe) {
			DataTable tableNgayLe = SqlDataAccessHelper.ExecSPQuery(SPName6.Holiday_GetDataV6.ToString(),
																	new SqlParameter("@From", KhoangTG.From), new SqlParameter("@To", KhoangTG.To));
			listNgayLe = (from DataRow dataRow in tableNgayLe.Rows select (DateTime)dataRow["HDate"]).ToList();
		}
	}
}
