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
				if (CIO.CheckVT == TrangThaiCheck.CheckDayDu && CIO.ThuocCa.ID == int.MinValue) continue;//todo test
				if (KiemTraThoaDieuKienThemCIO(CIO))
					Insert_CheckInOutData(UEN, CIO);
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
			if (CIO.CheckVT == TrangThaiCheck.CheckDayDu) {
				phut_Vao = MyUtility.QuyDoiPhut((CIO.TD.BD_LV - CIO.ThuocNgayCong));
				phut_Ra = MyUtility.QuyDoiPhut((CIO.TD.KT_LV - CIO.ThuocNgayCong));
				phut_BD_LV = MyUtility.QuyDoiPhut((CIO.TD.BD_LV - CIO.ThuocNgayCong));
				phut_KT_LV = MyUtility.QuyDoiPhut((CIO.TD.KT_LV - CIO.ThuocNgayCong));
				phut_KT_LV_TrongCa = MyUtility.QuyDoiPhut((CIO.TD.KT_LV_TrongCa - CIO.ThuocNgayCong));
				phut_BD_LV_Ca3 = (CIO.TD.BD_LV_Ca3 == DateTime.MinValue) ? 0 : MyUtility.QuyDoiPhut((CIO.TD.BD_LV_Ca3 - CIO.ThuocNgayCong));
				phut_KT_LV_Ca3 = (CIO.TD.KT_LV_Ca3 == DateTime.MinValue) ? 0 : MyUtility.QuyDoiPhut((CIO.TD.KT_LV_Ca3 - CIO.ThuocNgayCong));
				phut_TreVR = MyUtility.QuyDoiPhut(CIO.KhoangTGCa.Tre);
				phut_SomVR = MyUtility.QuyDoiPhut(CIO.KhoangTGCa.Som);
				phut_VaoSauCa = MyUtility.QuyDoiPhut(CIO.KhoangTGCa.VaoSauCa);
				phut_RaTruocCa = MyUtility.QuyDoiPhut(CIO.KhoangTGCa.RaTruocCa);
				//phut_LamNgoaiGio = 0; //chua co xac nhan lam ngoai gio
				//[ChoPhepSom][ChoPhepTre]=false

				//phut_LamDem = (CIO.KhoangTGCa.LamDem == TimeSpan.Zero) ? 0 : Convert.ToInt32(CIO.KhoangTGCa.LamDem.TotalMinutes);
			}
			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.CIO_ThemCIOChuaChamCongV6.ToString(),
														 new SqlParameter("@UserEnrollNumber", UEN),
														 new SqlParameter("@NgayCong", CIO.ThuocNgayCong),
														 new SqlParameter("@HaveINOUT", (int)CIO.CheckVT),
														 new SqlParameter("@GioVao", CIO.Vao == null ? (object)DBNull.Value : CIO.Vao.Time),
														 new SqlParameter("@GioRa", CIO.Raa == null ? (object)DBNull.Value : CIO.Raa.Time),
														 new SqlParameter("@Vao", CIO.Vao == null ? (object)DBNull.Value : phut_Vao),
														 new SqlParameter("@Ra", CIO.Raa == null ? (object)DBNull.Value : phut_Ra),
														 new SqlParameter("@MayVao", CIO.Vao == null ? (object)DBNull.Value : CIO.Vao.MachineNo),
														 new SqlParameter("@MayRa", CIO.Raa == null ? (object)DBNull.Value : CIO.Raa.MachineNo),
														 new SqlParameter("@BDLV", phut_BD_LV),
														 new SqlParameter("@KTLVTrongCa", phut_KT_LV_TrongCa),
														 new SqlParameter("@KTLV", phut_KT_LV),
														 new SqlParameter("@BDLVCa3", phut_BD_LV_Ca3),
														 new SqlParameter("@KTLVCa3", phut_KT_LV_Ca3),
														 new SqlParameter("@Tre", phut_TreVR),
														 new SqlParameter("@Som", phut_SomVR),
														 new SqlParameter("@VaoSauCa", phut_VaoSauCa),
														 new SqlParameter("@RaTruocCa", phut_RaTruocCa),
														 new SqlParameter { ParameterName = "@SoPhutXacNhanNgoaiGio", Value = 0 }, // lần đầu chưa xác nhận nên = 0
														 new SqlParameter("@ChoPhepTre", CIO.ChoPhepTre), // lần đầu chưa xác nhận nên = false
														 new SqlParameter("@ChoPhepSom", CIO.ChoPhepSom), // lần đầu chưa xác nhận nên = false
														 new SqlParameter("@VaoTuDo", CIO.VaoTuDo), // lần đầu chưa xác nhận nên = false
														 new SqlParameter("@RaTuDo", CIO.RaaTuDo), // lần đầu chưa xác nhận nên = false
														 new SqlParameter("@CongTrongGio", CIO.CongTheoCa.TrongGio),
														 new SqlParameter("@CongNgoaiGio", CIO.CongTheoCa.NgoaiGio),
														 new SqlParameter("@TruCongTre", CIO.CongTheoCa.TruCongTre),
														 new SqlParameter("@TruCongSom", CIO.CongTheoCa.TruCongSom),
														 new SqlParameter { ParameterName = "@ChamCongTay", Value = false, },
														 new SqlParameter("@DinhMucCong", CIO.CongTheoCa.DinhMuc),
														 new SqlParameter("@TongCong", CIO.CongTheoCa.Tong),
														 new SqlParameter("@TheoDoiGioGocMayCC", string.Empty)//todo ghi phần theo dõi
				// ko có GhiChu, LyDo
				);
			if (kq == 0) { ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000); }
		}

		//public void

		internal void GetCIOData(DataTable tableArrayUEN, FromToDateTime KhoangTG, out List<cCheckInOut_DaCC> DS_CIO_DaCC) {
			DataTable tableCIO = SqlDataAccessHelper.ExecSPQuery(SPName6.CIO_Lay.ToString(),
				new SqlParameter { ParameterName = "@Array_UserEnrollNumber", Value = tableArrayUEN, SqlDbType = SqlDbType.Structured },
													 new SqlParameter("@From", KhoangTG.From),
													 new SqlParameter("@To", KhoangTG.To));
			DS_CIO_DaCC = new List<cCheckInOut_DaCC>();
			foreach (DataRow row in tableCIO.Rows) {
				cCheckInOut_DaCC cio = new cCheckInOut_DaCC();
				cio.ID = (int)row["ID"];
				cio.MaCC = (int)row["UserEnrollNumber"];
				cio.Ngay = (DateTime)row["Ngay"];
				if (row["GioVao"] != DBNull.Value) cio.GioVao = (DateTime)row["GioVao"];
				if (row["GioRa"] != DBNull.Value) cio.GioRaa = (DateTime)row["GioRa"];
				cio.TD = new ThoiDiem();
				cio.KhoangTG = new StructTGCa();
				cio.CheckVT = TrangThaiCheckVT((int)row["HaveINOUT"]);
				if (row["Vao"] != DBNull.Value) cio.GioVao_LamTron = cio.Ngay.Add(new TimeSpan(0, (int)row["Vao"], 0));
				if (row["Ra"] != DBNull.Value) cio.GioRaa_LamTron = cio.Ngay.Add(new TimeSpan(0, (int)row["Ra"], 0));
				if (cio.CheckVT == TrangThaiCheck.CheckDayDu)
				{
					if (row["BDLV"] != DBNull.Value) cio.TD.BD_LV = cio.Ngay.Add(new TimeSpan(0, (int) row["BDLV"], 0));
					if (row["KTLVTrongCa"] != DBNull.Value) cio.TD.KT_LV_TrongCa = cio.Ngay.Add(new TimeSpan(0, (int) row["KTLVTrongCa"], 0));
					if (row["KTLV"] != DBNull.Value) cio.TD.KT_LV = cio.Ngay.Add(new TimeSpan(0, (int) row["KTLV"], 0));
					if (row["BDLVCa3"] != DBNull.Value) cio.TD.BD_LV_Ca3 = cio.Ngay.Add(new TimeSpan(0, (int) row["BDLVCa3"], 0));
					if (row["KTLVCa3"] != DBNull.Value) cio.TD.BD_LV_Ca3 = cio.Ngay.Add(new TimeSpan(0, (int) row["KTLVCa3"], 0));
					//cio.KhoangTG.LamTrongGio = cio.
				}
				if (row["Tre"] != DBNull.Value) cio.KhoangTG.Tre = new TimeSpan(0, (int)row["Tre"], 0);
				if (row["Som"] != DBNull.Value) cio.KhoangTG.Som = new TimeSpan(0, (int)row["Som"], 0);
				if (row["VaoSauCa"] != DBNull.Value) cio.KhoangTG.VaoSauCa = new TimeSpan(0, (int)row["VaoSauCa"], 0);
				if (row["RaTruocCa"] != DBNull.Value) cio.KhoangTG.RaTruocCa = new TimeSpan(0, (int)row["RaTruocCa"], 0);
				if (row["SoPhutXacNhanNgoaiGio"] != DBNull.Value) cio.KhoangTG.LamNgoaiGio = new TimeSpan(0, (int)row["SoPhutXacNhanNgoaiGio"], 0);
				cio.ChoPhepTre = (bool)row["ChoPhepTre"];
				cio.ChoPhepSom = (bool)row["ChoPhepSom"];
				cio.VaoTuDo = (bool)row["VaoTuDo"];
				cio.RaaTuDo = (bool)row["RaTuDo"];
				cio.ChamCongTay = (bool)row["ChamCongTay"];
				if (cio.ChamCongTay) cio.Cong.Tong = (row["TongCong"] != DBNull.Value) ? (float)row["TongCong"] : 0f;

				DS_CIO_DaCC.Add(cio);
			}
		}

		private TrangThaiCheck TrangThaiCheckVT(int p) {
			if (p == (int)TrangThaiCheck.ThieuVao) return TrangThaiCheck.ThieuVao;
			else if (p == (int)TrangThaiCheck.ThieuRa) return TrangThaiCheck.ThieuRa;
			return TrangThaiCheck.CheckDayDu;
		}

		public void GetNgayCongData(DataTable tableArrayUEN, FromToDateTime KhoangTG, out DataTable tableNgayCong) {
			tableNgayCong = SqlDataAccessHelper.ExecSPQuery(SPName6.NgayCong_Lay.ToString(),
															new SqlParameter { ParameterName = "@Array_UserEnrollNumber", Value = tableArrayUEN, SqlDbType = SqlDbType.Structured },
															new SqlParameter("@From", KhoangTG.From),
															new SqlParameter("@To", KhoangTG.To));
		}

		public void GetNgayVangData(DataTable tableArrayUEN, FromToDateTime KhoangTG, out DataTable tableKhaiBaoVang, out List<cKhaiBaoVang> DS_KhaiBaoVang) {
			tableKhaiBaoVang = SqlDataAccessHelper.ExecSPQuery(SPName6.Absent_GetData.ToString(),
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

		internal void GetNgayLeData(FromToDateTime KhoangTG, out DataTable tableNgayCong) {
			throw new NotImplementedException();
		}
	}
}
