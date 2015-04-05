using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.DAL;
using ChamCong_v05.DTO;
using ChamCong_v05.Helper;
using ChamCong_v05.Properties;
using log4net;
using log4net.Config;
using OfficeOpenXml;

namespace ChamCong_v05.UI.ChamCong {
	public partial class frm_XuatBBCongPC : Form {
		private readonly ILog lg = LogManager.GetLogger("frm_XuatBBCongPC");
		public List<cUserInfo> m_dsnv;
		public DateTime m_Thang;
		#region hàm ko quan trọng

		public frm_XuatBBCongPC() {
			XmlConfigurator.Configure();

			InitializeComponent();
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}

		#endregion

		private void Form2_Load(object sender, EventArgs e) {
			tbTenNVNhapLieu.Text = Settings.Default.LastTenNVLapBieuChamCong;
			tbTenTruongBP.Text = Settings.Default.LastTenTruongBP;
			dtpThang.Value = m_Thang;
		}

		private void btnXuatBB_Click(object sender, EventArgs e) {
			string tenTrgBP = tbTenTruongBP.Text, tenNVLapBieu = tbTenNVNhapLieu.Text;
			XL.SaveSetting(lastTenNVLapBieuChamCong: tenNVLapBieu, lastTenTruongBP: tenTrgBP);

			if (XL2.KiemtraKetnoiCSDL() == false) return;

			#region lấy save file name

			saveFileDlg.ShowDialog();
			if (saveFileDlg.FileName == String.Empty) {
				return;
			}
			var saveFileName = saveFileDlg.FileName;

			#endregion

			if (radBB_ChamCongThang.Checked)
			{
				WaitWindow.Show(this.XuatBBKetCongThang, "Đang xử lý. Bạn vui lòng đợi trong giây lát...", new object[] {saveFileName, tenNVLapBieu, tenTrgBP});
			}
			else if (radioButton2.Checked)
			{
				WaitWindow.Show(this.XuatBBGioKoDungQuyDinh_Va_DiTreVesom, "Đang xử lý. Bạn vui lòng đợi trong giây lát...", new object[] {saveFileName});
			}

		}

		private void XuatBBGioKoDungQuyDinh_Va_DiTreVesom(object sender, WaitWindowEventArgs e) {
			string saveFileName = (string)e.Arguments[0];
			using (var p = new ExcelPackage()) {
				//2. xuat bb bang ChiTietVaoTreRaSom

				#region ghi sheet bang ChiTietVaoTreRaSom

				p.Workbook.Worksheets.Add("ChiTietVaoTreRaSom");
				var ws = p.Workbook.Worksheets["ChiTietVaoTreRaSom"];
				ws.Name = "ChiTietVaoTreRaSom"; //Setting Sheet's name
				XL.ExportSheetTreSom(ws, m_dsnv);
				#endregion

				#region ghi sheet bang ChiTietVaoTreRaSom

				p.Workbook.Worksheets.Add("ThieuChamCong_KhongNhanDienCa");
				ws = p.Workbook.Worksheets["ThieuChamCong_KhongNhanDienCa"];
				ws.Name = "ThieuChamCong_KhongNhanDienCa"; //Setting Sheet's name
				XL.ExportSheetThieuChamCong_KhongHieuCa(ws, m_dsnv);
				#endregion
				Byte[] bytes = p.GetAsByteArray();
				XL.XuatFileExcel(saveFileName, bytes, "frmXuatBBCongPC_XuatBBGioKoDungQuyDinh_Va_DiTreVesom");
			}

		}

		private void XuatBBKetCongThang(object sender, WaitWindowEventArgs e) {
			string saveFileName = (string)e.Arguments[0]; string tenNVLapBieu = (string)e.Arguments[1]; string tenTrgBP = (string)e.Arguments[2];
			#region lấy thông tin từ csdl và khỏi tạo  nv

			int[] listMaCC = (from nv in m_dsnv select nv.MaCC).ToArray();
			var ngaydauthang = MyUtility.FirstDayOfMonth(dtpThang.Value);
			var ngaycuoithang = MyUtility.LastDayOfMonth(dtpThang.Value);
			var tableKetcongNgay = DAO5.LayKetcongNgay(ngaydauthang, ngaycuoithang, DSNV: listMaCC);
			var tableKetcongCa = DAO5.LayKetcongCa(ngaydauthang, ngaycuoithang, DSNV: listMaCC);
			var tableXPVang = DAO5.LayTableXPVang(ngaydauthang, ngaycuoithang, listMaCC);
			var tableNgayLe = DAO5.DocNgayLe(ngaydauthang, ngaycuoithang);
			var tableDSNVChiCongnhatThang = DAO5.LayTableCongNhat(ngaydauthang, DSNV: listMaCC);

			#endregion

			// xác định công chuẩn của tháng
			var soNgayChuNhat = XL.DemSoNgayNghiChunhat(ngaydauthang, true, false);
			var congChuanThang = DateTime.DaysInMonth(ngaydauthang.Year, ngaydauthang.Month) - soNgayChuNhat;

			#region //load cong phu cap tung ngay cho tat ca nv, ke ca cong nhat, rieng truong hop cong nhat se xu ly ngay ben duoi
			List<cUserInfo> dsnv9 = new List<cUserInfo>();
			foreach (var nv in m_dsnv) { // duyệt qua các nhân viên được check
				cUserInfo nv9 = new cUserInfo {
					MaCC = nv.MaCC, MaNV = nv.MaNV, TenNV = nv.TenNV, ChucVu = nv.ChucVu, IDChucVu = nv.IDChucVu,
					HeSo = nv.HeSo, PhongBan = nv.PhongBan, IsUserEnabled = nv.IsUserEnabled, LichTrinhLV = nv.LichTrinhLV,
					ThongKeThang = new ThongKeCong_PC(), DSNgayCong = new List<cNgayCong>(), DSVang = new List<cLoaiVang>()
				};
				for (DateTime indexNgay = ngaydauthang; indexNgay <= ngaycuoithang; indexNgay = indexNgay.AddDays(1d)) {
					XL.LoadNgayCong(nv9.MaCC, nv9.DSNgayCong, indexNgay, tableKetcongNgay, tableKetcongCa);
				}

				XL.LoadDSXPVang_Le(nv.MaCC, tableXPVang, tableNgayLe, nv.DSVang);
				XL.PhanPhoi_DSVang7(nv.DSVang, nv.DSNgayCong);

				XL.LoadThongtinLamViecCongNhat(nv9.MaCC, ref nv9.NgayBDCongnhat, ref nv9.NgayKTCongnhat, ref nv9.LoaiCN, nv9.DSNgayCong, tableDSNVChiCongnhatThang);
				dsnv9.Add(nv9);
			}

			foreach (var nv9 in dsnv9) {
				XL.ThongKeThang(ref nv9.ThongKeThang, nv9.DSNgayCong, nv9.NgayBDCongnhat, nv9.NgayKTCongnhat, nv9.LoaiCN);

				// tính công chờ việc: 1.nv công nhật ko cv. 2. nv mới chính thức thì chỉ giữ công cv khai báo
				if (nv9.NgayBDCongnhat != DateTime.MinValue) // nhân viên vừa làm chính thức vừa làm công nhật, công cv tự động = 0
					{
					nv9.ThongKeThang.CongCV_Auto = 0f;
				}
				else // nhân viên chính thức
					{
					nv9.ThongKeThang.CongCV_Auto = congChuanThang -
												  /*(nv9.ThongKeThang.Cong + nv9.ThongKeThang.Le + nv9.ThongKeThang.Phep +//ver4.0.0.0*/
												  (nv9.ThongKeThang.TongNgayLV + nv9.ThongKeThang.Le + nv9.ThongKeThang.Phep +//ver4.0.0.1
												   nv9.ThongKeThang.BHXH + nv9.ThongKeThang.H_CT_PT
												   + nv9.ThongKeThang.PTDT + nv9.ThongKeThang.NghiRo + nv9.ThongKeThang.CongCV_KB);//DANGLAM
					if (nv9.ThongKeThang.CongCV_Auto < 0f) nv9.ThongKeThang.CongCV_Auto = 0f;
				}
				nv9.ThongKeThang.CongCV = nv9.ThongKeThang.CongCV_Auto + nv9.ThongKeThang.CongCV_KB;
			}

			#endregion


			using (var p = new ExcelPackage()) {
				//2. xuat bb bang ket cong thang

				#region ghi sheet bang ket cong thang trinh ky

				p.Workbook.Worksheets.Add("BangKetCong");
				var ws = p.Workbook.Worksheets["BangKetCong"];
				ws.Name = "BangKetCong"; //Setting Sheet's name
				XL.ExportSheetBangKetcongThang(ws, MyUtility.FirstDayOfMonth(dtpThang.Value), MyUtility.LastDayOfMonth(dtpThang.Value),
											   dsnv9, tenNVLapBieu, tenTrgBP, XL2.PC30, XL2.PC50, XL2.PCTCC3, XL2.PC100, XL2.PC160, XL2.PC200, XL2.PC290);

				#endregion

				//3. xuat bb chi tiết kết công

				#region ghi sheet chi tiết kết công

				p.Workbook.Worksheets.Add("ChiTietKetCong");
				ws = p.Workbook.Worksheets["ChiTietKetCong"];
				ws.Name = "ChiTietKetCong"; //Setting Sheet's name
				XL.ExportSheetBangChiTietKetCong(ws, MyUtility.FirstDayOfMonth(dtpThang.Value), MyUtility.LastDayOfMonth(dtpThang.Value),
												 dsnv9, XL2.PC30, XL2.PC50, XL2.PCTCC3, XL2.PC100, XL2.PC160, XL2.PC200, XL2.PC290);

				#endregion

				Byte[] bytes = p.GetAsByteArray();
				XL.XuatFileExcel(saveFileName, bytes, "frmXuatBBCongPC XuatBBKetCongThang");
			}
		}
	}
}

