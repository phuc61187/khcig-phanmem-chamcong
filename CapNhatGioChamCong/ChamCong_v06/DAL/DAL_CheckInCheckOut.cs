using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
				Insert_CheckInOutData(UEN, CIO);
			}
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
														 new SqlParameter("@VaoTuDo", true), // lần đầu chưa xác nhận nên = false
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
	}
}
