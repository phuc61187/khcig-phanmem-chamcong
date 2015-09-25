using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.DTO;
using ChamCong_v06.Helper;

namespace ChamCong_v06.UI {
	public partial class frmChiTietVaoRa : Form {
		public Mode_Xem_CT_VaoRa CurrentMode;
		public List<cUserInfo> DSNV;
		public enum Mode_Xem_CT_VaoRa {
			XemThieuChamCong = 1,
			XemVaoTreRaSom = 2,
			Xem_KoNhanDienCa = 3,
			Xem_OLaiChuaXN = 4,
			Xem_GioBiChinhSua = 5,
			Xem_DaXNCa = 6,
			Xem_DaXN_LamThem = 7,
			Xem_ChoPhepTre_Som = 8,
			Xem_VaoRa_TuDo = 9,
		}

		public frmChiTietVaoRa() {
			InitializeComponent();
		}

		private void frmChiTietVaoRa_Load(object sender, EventArgs e) {
			Load_DSVaoRa(CurrentMode);
		}

		private void Load_DSVaoRa(Mode_Xem_CT_VaoRa Mode) {
			DataTable tableChiTietVaoRa = new DataTable();
			tableChiTietVaoRa.Columns.Add(f3.UserEnrollNumber.ToString(), typeof(int));
			tableChiTietVaoRa.Columns.Add(f3.UserFullCode.ToString(), typeof(string));
			tableChiTietVaoRa.Columns.Add(f3.UserFullName.ToString(), typeof(string));
			tableChiTietVaoRa.Columns.Add(f3.Ngay.ToString(), typeof(DateTime));
			tableChiTietVaoRa.Columns.Add(f3.GioVao.ToString(), typeof(DateTime));
			tableChiTietVaoRa.Columns.Add(f3.GioRa.ToString(), typeof(DateTime));
			tableChiTietVaoRa.Columns.Add(f3.KyHieuCa.ToString(), typeof(string));
			tableChiTietVaoRa.Columns.Add(f3.Tre.ToString(), typeof(TimeSpan));
			tableChiTietVaoRa.Columns.Add(f3.Som.ToString(), typeof(TimeSpan));
			tableChiTietVaoRa.Columns.Add(f3.TongCong.ToString(), typeof(float));
			tableChiTietVaoRa.Columns.Add(f3.GioLamViec.ToString(), typeof(TimeSpan));
			tableChiTietVaoRa.Columns.Add(f3.GioLamDem.ToString(), typeof(TimeSpan));
			tableChiTietVaoRa.Columns.Add(f3.GioHienDien.ToString(), typeof(TimeSpan));
			tableChiTietVaoRa.Columns.Add(f3.LamNgoaiGio.ToString(), typeof(TimeSpan));
			tableChiTietVaoRa.Columns.Add(f3.ChoPhepTre.ToString(), typeof(bool));
			tableChiTietVaoRa.Columns.Add(f3.ChoPhepSom.ToString(), typeof(bool));
			List<cCheckInOut_DaCC> kq;

			foreach (cUserInfo NhanVien in DSNV) {
				foreach (cNgayCong ngayCong in NhanVien.DSNgayDaCC) {
					foreach (cCheckInOut_DaCC CIO in ngayCong.DSVaoRa) {
						if (KiemTraThoaDieuKien(Mode, CIO)) {
							//DataRow newRow = 
						}
					}
				}
			}

		}

		private bool KiemTraThoaDieuKien(Mode_Xem_CT_VaoRa mode, cCheckInOut_DaCC CIO) {
			switch (mode) {
				case Mode_Xem_CT_VaoRa.XemThieuChamCong:
					if (CIO.CheckVT != TrangThaiCheck.CheckDayDu) 
						return true;
					break;
				case Mode_Xem_CT_VaoRa.XemVaoTreRaSom:
					if (CIO.CheckVT == TrangThaiCheck.CheckDayDu
					&& ((CIO.ChoPhepTre == false && CIO.Tre > TimeSpan.Zero) ||
						(CIO.ChoPhepSom == false && CIO.Som > TimeSpan.Zero))
					)
						return true;
					break;
				case Mode_Xem_CT_VaoRa.Xem_KoNhanDienCa:
					return false;
				case Mode_Xem_CT_VaoRa.Xem_OLaiChuaXN:
					return false;
				case Mode_Xem_CT_VaoRa.Xem_GioBiChinhSua:
					return false;
				case Mode_Xem_CT_VaoRa.Xem_DaXNCa:
					return false;
				case Mode_Xem_CT_VaoRa.Xem_DaXN_LamThem:
					return false;
				case Mode_Xem_CT_VaoRa.Xem_ChoPhepTre_Som:
					if (CIO.CheckVT == TrangThaiCheck.CheckDayDu
					&& (CIO.ChoPhepTre || CIO.ChoPhepSom))
						return true;
					break;
				case Mode_Xem_CT_VaoRa.Xem_VaoRa_TuDo:
					if (CIO.CheckVT == TrangThaiCheck.CheckDayDu 
						&& (CIO.VaoTuDo || CIO.RaaTuDo))
					return true;
					break;
				default:
					break;
			}
			return false;
		}
	}
}
