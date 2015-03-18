using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;
using ChamCong_v03.DieuChinhLuong;
using ChamCong_v03.LuongCongNhat;
using ChamCong_v03.Properties;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using log4net;


namespace ChamCong_v03 {
	public partial class frm_41_TinhLuongNV : Form {
		// vấn đề tồn đọng: 1. xuất báo biểu theo từng phòng ban cấp 2-1
		// 2.format các ô dữ liệu excel xuất
		// 3. save chồng lên file đang mở thì báo lỗi
		// 4. kiểm tra thêm cho hệ số cộng thêm vào bảo hiểm cho GD, PGD
		public readonly ILog lg = LogManager.GetLogger("frm_41_TinhLuongNV");
		public List<cUserInfo> m_DSNV = new List<cUserInfo>();
		public DataTable m_tableDSNV; // không cần new vì hàm DAL.LayDSTatCaNV() ở [20140510_7] đã gọi SQLdataaccesshelper tạo datatable mới


		public frm_41_TinhLuongNV() {
			InitializeComponent();
			DateTime today = DateTime.Today;
			dtpThang.Value = new DateTime(today.Year, today.Month, today.Day);
		}

		private void updateControl(Control[] controls) {
			foreach (Control control in controls) {
				control.Update();
			}
		}

		private void btnDieuChinhLuong_Click(object sender, EventArgs e) {
			frm_42_DieuChinhLuongThangTruoc frm = new frm_42_DieuChinhLuongThangTruoc(){m_thang = dtpThang.Value};
			frm.Location = new Point((int)((this.Size.Width - frm.Size.Width) / 2f), (int)((this.Size.Height - frm.Size.Height) / 2f));

			frm.ShowDialog();
		}

		private void btnThayDoiHSL_Click(object sender, EventArgs e) {
			frm_43_ThayDoiHeSoLuong_PC frm43 = new frm_43_ThayDoiHeSoLuong_PC();
			frm43.Location = new Point((int)((this.Size.Width - frm43.Size.Width) / 2f), (int)((this.Size.Height - frm43.Size.Height) / 2f));

			frm43.ShowDialog();
		}


		private void btnLuongCongNhat_Click(object sender, EventArgs e) {
			LuongCongNhat.frm_LuongCongNhat frm = new frm_LuongCongNhat();
			frm.m_thang = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);
			frm.Location = new Point((int)((this.Size.Width - frm.Size.Width) / 2f), (int)((this.Size.Height - frm.Size.Height) / 2f));
			frm.ShowDialog();

		}

		private void dtpThang_ValueChanged(object sender, EventArgs e) {
			dtpThang.Update();
			var ngaydauthang = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, DateTime.DaysInMonth(dtpThang.Value.Year, dtpThang.Value.Month));

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				MessageBox.Show("Mất kết nối với CSDL.", "Lỗi", MessageBoxButtons.OK);
				this.Close();
			}
			var kq = SqlDataAccessHelper.ExecuteQueryString(
					"select sum(ThanhTien) from LuongCongNhat where Thang = @Thang and Nam =@Nam",
					new string[] { "@Thang", "@Nam" }, new object[] { ngaydauthang.Month, ngaydauthang.Year });
			var luongcongnhat = kq.Rows[0][0] == DBNull.Value ? 0d : Convert.ToDouble(kq.Rows[0][0]);
			var tableThangKetCong = SqlDataAccessHelper.ExecuteQueryString(
				" select Khoa, SanLuong, DonGia, LuongToiThieu, BoiDuongCa3, ChiTienGiaCong " +
				" from ThangKetCong where Thang = @Thang ",
				new string[] { "@Thang" }, new object[] { ngaydauthang });
			var khoa = (tableThangKetCong.Rows.Count != 0 && Convert.ToBoolean(tableThangKetCong.Rows[0]["Khoa"]));

			decimal ChiTienGiaCong = 0;
			int SanLuong = 0, DonGia = 0, LuongToiThieu = 0, BoiDuongCa3 = 0;
			if (tableThangKetCong.Rows.Count != 0) {
				SanLuong = Convert.ToInt32(tableThangKetCong.Rows[0]["SanLuong"]);
				DonGia = Convert.ToInt32(tableThangKetCong.Rows[0]["DonGia"]);
				ChiTienGiaCong = Convert.ToDecimal(tableThangKetCong.Rows[0]["ChiTienGiaCong"]);
				LuongToiThieu = Convert.ToInt32(tableThangKetCong.Rows[0]["LuongToiThieu"]);
				BoiDuongCa3 = Convert.ToInt32(tableThangKetCong.Rows[0]["BoiDuongCa3"]);
			}
			if (tableThangKetCong.Rows.Count != 0 && khoa) {
				MyUtility.EnableDisableControl(false, new Control[] { numSanLuong, numDonGia, numChiTienGiaCong, numLuongTT, numBoiDuongCa3 });
			}
			else {
				MyUtility.EnableDisableControl(true, new Control[] { numSanLuong, numDonGia, numChiTienGiaCong, numLuongTT, numBoiDuongCa3 });
			}

			numSanLuong.Value = SanLuong;
			numSanLuong.Update();
			numDonGia.Value = DonGia;
			numDonGia.Update();
			numChiTienGiaCong.Value = ChiTienGiaCong;
			numChiTienGiaCong.Update();
			numLuongTT.Value = LuongToiThieu;
			numLuongTT.Update();
			numBoiDuongCa3.Value = BoiDuongCa3;
			numBoiDuongCa3.Update();
			numLuongCongNhat.Value = (decimal)luongcongnhat;
			numLuongCongNhat.Update();
		}


		private void btnTinhLuongVaXuatBB_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			updateControl(new Control[] { dtpThang, numSanLuong, numDonGia, numLuongTT, numBoiDuongCa3 });
			var thang = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);
			var ngaydauthang = new DateTime(thang.Year, thang.Month, 1);
			var ngaycuoithang = new DateTime(thang.Year, thang.Month, DateTime.DaysInMonth(thang.Year, thang.Month));
			var ngayBdBef2D = ngaydauthang.AddDays(-2d);
			var ngayKtAft2D = ngaycuoithang.AddDays(2d);

			m_tableDSNV = DAL.LayDSTatCaNV_TruCongNhat(); //[20140510_7]
			m_DSNV.Clear();
			XL.KhoiTaoDSNV(m_DSNV, m_tableDSNV);


			XL.XemCong(m_DSNV, ngayBdBef2D, ngayKtAft2D);
			var d01sanluong = (double)(numSanLuong.Value);
			var d02dongia = (double)numDonGia.Value;
			var d07ChiTienGiaCong = (double) numChiTienGiaCong.Value;
			var d03TongQuy100per_1 = d01sanluong * d02dongia;
			var d04_80perQuyA1_1 = d03TongQuy100per_1 * 0.8d;
			var d05_20perDuPhong_1 = (d03TongQuy100per_1 - d04_80perQuyA1_1);
			var tableLuongCN = SqlDataAccessHelper.ExecuteQueryString("select sum(ThanhTien) from LuongCongNhat where Thang = @Thang and Nam =@Nam",
					new string[] { "@Thang", "@Nam" }, new object[] { ngaydauthang.Month, ngaydauthang.Year });
			var d06_TongLuongCongNhat_AllNV_1 = tableLuongCN.Rows[0][0] == DBNull.Value ? 0d : Convert.ToDouble(tableLuongCN.Rows[0][0]);
			numLuongCongNhat.Value = (decimal)d06_TongLuongCongNhat_AllNV_1;

			var dluongtoithieu = (double)numLuongTT.Value;
			var dDonGiaBD_ca3 = (double)numBoiDuongCa3.Value;
			var dTongLuongThangTruoc_AllNV_1 = 0d;
			var dTongChiKhac_AllNV_1 = 0d;
			XL.DocLuongDieuChinh(thang, m_DSNV, out dTongLuongThangTruoc_AllNV_1, out dTongChiKhac_AllNV_1);
			// tính khấu trừ bảo hiểm
			XL.TinhKhauTruBHChoNV(m_DSNV, dluongtoithieu);
			#region tính lương

			var tong_qlcb_2 = 0d;
			var tong_spLamRa_B2_2 = 0d;
			var tongthuchikhac_2 = 0d;
			var tongluongthangtruoc_2 = 0d;
			foreach (var nv in m_DSNV) {
				XL.ThongKe(nv);

				XL.TinhCongChoViec(ngaydauthang, nv.Tong.CongThang, nv.Tong.Phep, nv.Tong.Le, nv.Tong.H_CT_PT, nv.Tong.BHXH, nv.Tong.NghiRo, nv.Tong.CongCV, nv.Tong.CongCVTreSom, out nv.Tong.CongCV);
				var luongcb_cong_pc_1NV = 0d;
				var boiduongca3_1NV = 0d;
				var spLamRa_1NV = 0d;
				XL.TinhLuongCoBan_CongVaPC_A202(nv.HeSo.LuongCB, dluongtoithieu,
					nv.Tong.CongThang, nv.Tong.PCapThang, nv.Tong.Phep, nv.Tong.H_CT_PT, nv.Tong.Le, nv.Tong.CongCV,
					out nv.Luong.LCB_TheoCongVaCV, out nv.Luong.LCB_TheoPCap, out luongcb_cong_pc_1NV);
				XL.TinhBoiDuongQuaDemA512(nv.Tong.NgayQuaDem, dDonGiaBD_ca3, out nv.Luong.BoiDuongQuaDem);
				tongthuchikhac_2 += nv.Luong.ThuChiKhac;
				tongluongthangtruoc_2 += nv.Luong.LuongThangTruoc;
				tong_qlcb_2 += luongcb_cong_pc_1NV + boiduongca3_1NV + nv.Luong.BoiDuongQuaDem + nv.Luong.LuongThangTruoc;//BUG tong_qlcb_2 bao gồm lương cb 1nv, bồi dưỡng ca 3 1nv, lương tháng trước 1 nv
				XL.TinhSPLamRa_CongVaPC_B102(nv.HeSo.LuongCV, nv.Tong.CongThang, nv.Tong.PCapThang, nv.Tong.Phep, nv.Tong.H_CT_PT, nv.Tong.Le,
					out nv.Luong.SP_LamRa_TheoCong, out nv.Luong.SP_LamRa_TheoPCap, out spLamRa_1NV);
				tong_spLamRa_B2_2 += spLamRa_1NV;
			}
			//var tong_qlSP_A71_1 = d04_80perQuyA1_1 - tong_qlcb_2 - d06_TongLuongCongNhat_AllNV_1;
			var tong_qlSP_A71_1_VaGiaCong = (d04_80perQuyA1_1 + d07ChiTienGiaCong) - tong_qlcb_2 - d06_TongLuongCongNhat_AllNV_1;
			double giaTri_1SP_B3_1 = tong_qlSP_A71_1_VaGiaCong / tong_spLamRa_B2_2; // tính ra được 1 đơn vị sản phẩm có giá bao nhiêu


			double kt_tongquyluongcb = 0d, kt_tongquycn = d06_TongLuongCongNhat_AllNV_1, kt_tongchikhac = 0d, kt_tongdieuchinh = 0d, kt_tongquyluongsp = 0d;
			foreach (var nv in m_DSNV) {
				nv.Luong.LSP_TheoCong = nv.Luong.SP_LamRa_TheoCong * giaTri_1SP_B3_1;
				nv.Luong.LSP_TheoPCap = nv.Luong.SP_LamRa_TheoPCap * giaTri_1SP_B3_1;
				nv.Luong.TongLuong_TheoCong_Choviec_ThangTruoc = nv.Luong.LCB_TheoCongVaCV + nv.Luong.LSP_TheoCong + nv.Luong.LuongThangTruoc;
				//nv.Luong.TongLuong_TheoPCap_BDCa3 = nv.Luong.TongLuongCB_TheoPCap + nv.Luong.LSP_TheoPCap + nv.Luong.BoiDuongQuaDem;
				nv.Luong.TongLuong_TheoPCap_BDCa3 = nv.Luong.LCB_TheoPCap + nv.Luong.LSP_TheoPCap + nv.Luong.BoiDuongQuaDem;
				nv.Luong.TongLuong = nv.Luong.TongLuong_TheoCong_Choviec_ThangTruoc + nv.Luong.TongLuong_TheoPCap_BDCa3;
				nv.Luong.ThucLanh = nv.Luong.TongLuong - nv.Luong.KhauTruBH - nv.Luong.TamUng - nv.Luong.ThuChiKhac;
				nv.Luong.TongLuong_NghiDinh205CP = nv.Luong.LCB_TheoCongVaCV + nv.Luong.LCB_TheoPCap;
				nv.Luong.TongLuong_TheoHSSP = nv.Luong.LSP_TheoCong + nv.Luong.LSP_TheoPCap;  //tbd chưa được sử dụng??
			}


			#endregion

			saveFileDialog.Filter = "Excel File|*.xlsx";
			saveFileDialog.ShowDialog();

			if (saveFileDialog.FileName == String.Empty) {
				return;
			}
			GC.Collect();
			try {
				XuatBBTinhLuong(m_DSNV, saveFileDialog.FileName, ngayBdBef2D, ngayKtAft2D,
					d01sanluong, d02dongia, d03TongQuy100per_1, d05_20perDuPhong_1, d04_80perQuyA1_1, d07ChiTienGiaCong,
					tong_qlcb_2, tong_qlSP_A71_1_VaGiaCong, giaTri_1SP_B3_1, d06_TongLuongCongNhat_AllNV_1, tongthuchikhac_2, tongluongthangtruoc_2,
					ref kt_tongquyluongcb, ref kt_tongquycn, ref kt_tongchikhac, ref kt_tongdieuchinh, ref kt_tongquyluongsp);
//				XuatBBTinhLuong(m_DSNV, saveFileDialog.FileName, ngayBdBef2D, ngayKtAft2D,
//					d01sanluong, d02dongia, d03TongQuy100per_1, d05_20perDuPhong_1, d04_80perQuyA1_1,
//					tong_qlcb_2, tong_qlSP_A71_1, giaTri_1SP_B3_1, d06_TongLuongCongNhat_AllNV_1, tongthuchikhac_2, tongluongthangtruoc_2,
//					ref kt_tongquyluongcb, ref kt_tongquycn, ref kt_tongchikhac, ref kt_tongdieuchinh, ref kt_tongquyluongsp);
			} catch (Exception exception) {
				lg.Error("Xuat BB", exception);
				MessageBox.Show("Xảy ra lỗi trong quá trình xuất. Vui lòng liên hệ bộ phận kỹ thuật để được trợ giúp", "Lỗi", MessageBoxButtons.OK);
			}
		}

		public void XuatBBTinhLuong(List<cUserInfo> m_DSNVXemCong, string file_path, DateTime ngayBD_Bef2D, DateTime ngayKT_Bef2D,
			double sanluong, double dongialuong1, double quyluong100per, double quyluongduphong20per, double quyluongchi80per, double ChiTienGiaCong,
			double quyluongCB, double quyluongSP, double tienluong1hesoSP, double quyluongcongnhat, double chikhac_ca3, double luongthangtruoc,
			ref double kt_tongquyluongcb, ref  double kt_tongquycn, ref  double kt_tongchikhac, ref double kt_tongdieuchinh, ref double kt_tongquyluongsp
			) {

			try {
				using (var p = new ExcelPackage()) {
					var dTongQuyLuongCB205_CongDon = 0d;
					var dTongQuyLuongSP_CongDon = 0d;
					var dTongChiKhac_Ca3_CongDon = 0d;
					var dTongLuongThangTruoc_CongDon = 0d;
					XuatBBBangLuong(p, m_DSNVXemCong, ngayBD_Bef2D, ngayKT_Bef2D, out dTongQuyLuongCB205_CongDon, out dTongChiKhac_Ca3_CongDon, out dTongLuongThangTruoc_CongDon, out dTongQuyLuongSP_CongDon,
						ref  kt_tongquyluongcb, ref   kt_tongquycn, ref   kt_tongchikhac, ref  kt_tongdieuchinh, ref  kt_tongquyluongsp);
					XuatBBBangThanhToanLuongCN(p, ngayBD_Bef2D, ngayKT_Bef2D);
					XuatBBBangTongHopChiLuong(p, m_DSNVXemCong, ngayBD_Bef2D, ngayKT_Bef2D, sanluong, dongialuong1, quyluong100per, quyluongduphong20per, quyluongchi80per, ChiTienGiaCong,
						dTongQuyLuongCB205_CongDon, dTongQuyLuongSP_CongDon, tienluong1hesoSP, quyluongcongnhat, dTongChiKhac_Ca3_CongDon, dTongLuongThangTruoc_CongDon);
					Byte[] bin = p.GetAsByteArray();
					try {
						File.WriteAllBytes(file_path, bin);
						AutoClosingMessageBox.Show("Xuất báo biểu thành công.", "Thông báo", 2000);
					} catch (Exception exception) {
						if (exception is UnauthorizedAccessException) MessageBox.Show("Bạn chưa được cấp quyền ghi file vào folder.", "Lỗi");
						else if (exception is DirectoryNotFoundException) MessageBox.Show("Không tìm thấy folder lưu trữ.", "Lỗi");
						else if (exception is IOException) MessageBox.Show("File đang mở bởi ứng dụng khác.", "Lỗi");
						else MessageBox.Show("Không thể ghi được file báo biểu.", "Lỗi");
					}

				}
			} catch (Exception exception) {
				lg.Error("XuatBBTinhLuong", exception);
				throw exception;
			}
		}
		public void XuatBBBangLuong(ExcelPackage p, List<cUserInfo> m_DSNVXemCong, DateTime ngayBD_Bef2D, DateTime ngayKT_Bef2D,
			out double QLCB205_CongDon, out double TongChiKhac_BDca3_CongDon, out double LuongThangTruoc_CongDon, out double QLSP_CongDon,
			ref double kt_tongquyluongcb, ref double kt_tongquycn, ref  double kt_tongchikhac, ref double kt_tongdieuchinh, ref double kt_tongquyluongsp) {
			try {
				#region //Create a sheet
				p.Workbook.Worksheets.Add("Bang Luong");
				ExcelWorksheet ws = p.Workbook.Worksheets[1];
				ws.Name = "Bang Luong"; //Setting Sheet's name
				ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
				ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

				ws.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				#endregion

				XL2.VeLogo("bbbangluong", ws);
				XL2.GhiThongTinTongcty(ws, "", 1, 10, 22, 1, 25, 22, 4, 25);
				var startRow = 5;
				var startCol = 1;
				QLCB205_CongDon = 0d;
				QLSP_CongDon = 0d;
				TongChiKhac_BDca3_CongDon = 0d;
				LuongThangTruoc_CongDon = 0d;
				SUMLUONG sum = new SUMLUONG();
				var sttnv = 0;

				var ir = startRow;
				var ic = startCol;

				#region 1.tieu de bang LƯƠNG

				var thang = ngayBD_Bef2D.AddDays(2);
				// vì ngày kết thúc là ngày đầu tiên của tháng sau nên -1 sẽ ra ngày đầu của tháng hiện tại
				XL.FormatCells(ws.Cells[ir, 17], "Bảng lương tháng " + thang.ToString("MM/yyyy"), size: 22, VeBorder: false, bold: true, hAlign:ExcelHorizontalAlignment.CenterContinuous);
				ir++;// bỏ 1 dòng trống
				ir++;// vị trí dòng tiếp theo

				#endregion

				var headerCol = ic;

				#region format và ghi tiêu đề cột
				// 2 dòng tiêu đề nên sr và sr + 1; chỉ mặc định bold 2 dòng tiêu đề, chưa ghi thông tin
				XL.FormatCells(ws.Cells[ir, 1, ir + 1, 34], null, VeBorder: false, bold: true);
				//sR++;sR++; chỉ mặc định bold 2 dòng tiêu đề, chưa ghi thông tin, ghi tiêu đề xong thì tăng lên

				#region stt, manv, ho ten +1 +2 +3--------------------------------------------------------

				#region  write stt +0

				ws.Column(headerCol).Width = (int)L.STT;
				ws.Cells[ir, headerCol].Value = "STT";
				ws.Cells[ir, headerCol].Style.WrapText = true; //sR vì chỉ có 1 dòng header
				ws.Cells[ir, headerCol, ir + 1, headerCol].Merge = true;
				ws.Cells[ir, headerCol, ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write manv +1

				ws.Column(headerCol).Width = (int)L.MANV;
				ws.Cells[ir, headerCol].Value = "Mã NV";
				ws.Cells[ir, headerCol].Style.WrapText = true;
				ws.Cells[ir, headerCol, ir + 1, headerCol].Merge = true;
				ws.Cells[ir, headerCol, ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region  write ho ten +2

				ws.Column(headerCol).Width = (int)L.HOTEN;
				ws.Cells[ir, headerCol].Value = "Họ tên";
				ws.Cells[ir, headerCol].Style.WrapText = true;
				ws.Cells[ir, headerCol, ir + 1, headerCol].Merge = true;
				ws.Cells[ir, headerCol, ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#endregion -----------------------------------------------------------------------

				#region HSLương +3 +4 ---------------------------------------------------------

				#region write HSLương ở trên

				ws.Cells[ir, headerCol].Value = "Hệ số lương";
				ws.Cells[ir, headerCol].Style.WrapText = true;
				ws.Cells[ir, headerCol, ir, headerCol + 1].Merge = true;
				ws.Cells[ir, headerCol, ir, headerCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

				#endregion

				#region write HeSoLCB +3

				ws.Column(headerCol).Width = (int)L.HSLUONGCB;
				ws.Cells[ir + 1, headerCol].Value = "CB";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write HeSoSP +4

				ws.Column(headerCol).Width = (int)L.HSLUONGSP;
				ws.Cells[ir + 1, headerCol].Value = "SP";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#endregion

				#region ngày công chuẩn +5 +6 +7 +8 +9---------------------------------------------

				#region write ngày công chuẩn ở trên

				ws.Cells[ir, headerCol].Value = "Ngày công chuẩn";
				ws.Cells[ir, headerCol].Style.WrapText = true;
				ws.Cells[ir, headerCol, ir, headerCol + 4].Merge = true;
				ws.Cells[ir, headerCol, ir, headerCol + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

				#endregion

				#region write Ban ngày,trực tết,pccc(PCCQ chi) +5

				ws.Column(headerCol).Width = (int)L.BANNGAY;
				ws.Cells[ir + 1, headerCol].Value = "Công";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write Phép +6

				ws.Column(headerCol).Width = (int)L.PHEP;
				ws.Cells[ir + 1, headerCol].Value = "Phép";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write Học, họp, PT, lễ +7

				ws.Column(headerCol).Width = (int)L.HOCHOPLE;
				ws.Cells[ir + 1, headerCol].Value = "Học, họp, PT, lễ";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write trực đêm CV +8

				ws.Column(headerCol).Width = (int)L.QUADEM;
				ws.Cells[ir + 1, headerCol].Value = "Qua đêm";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write Chờ việc +9

				ws.Column(headerCol).Width = (int)L.CV;
				ws.Cells[ir + 1, headerCol].Value = "Chờ việc";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#endregion  ----------------------------------------------------------------------

				#region làm thêm giờ +10 +11 +12 +13 ------------------------------------------------

				#region write làm thêm giờ ở trên

				ws.Cells[ir, headerCol].Value = "Phụ cấp";
				ws.Cells[ir, headerCol].Style.WrapText = true;
				ws.Cells[ir, headerCol, ir, headerCol + 6].Merge = true;
				ws.Cells[ir, headerCol, ir, headerCol + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

				#endregion

				#region write 130% +10

				ws.Column(headerCol).Width = (int)L.PC30;
				ws.Cells[ir + 1, headerCol].Value = "30%";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write 150% +11

				ws.Column(headerCol).Width = (int)L.PC50;
				ws.Cells[ir + 1, headerCol].Value = "50%";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write tcc3

				ws.Column(headerCol).Width = (int)L.PC50;
				ws.Cells[ir + 1, headerCol].Value = "TCC3 100%";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write 100% +12

				ws.Column(headerCol).Width = (int)L.PC100;
				ws.Cells[ir + 1, headerCol].Value = "LVNN 100%";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write 160% +13

				ws.Column(headerCol).Width = (int)L.PC160;
				ws.Cells[ir + 1, headerCol].Value = "LVNN 160%";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write 200% +13

				ws.Column(headerCol).Width = (int)L.PC160;
				ws.Cells[ir + 1, headerCol].Value = "Trực lễ 200%";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write 200% +13

				ws.Column(headerCol).Width = (int)L.PC160;
				ws.Cells[ir + 1, headerCol].Value = "Trực lễ ca3 290%";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#endregion ------------------------------------------------------------------------------------

				#region write tổng công tháng +14

				ws.Column(headerCol).Width = (int)L.TONGCONG;
				ws.Cells[ir, headerCol, ir + 1, headerCol].Value = "Tổng công";
				ws.Cells[ir, headerCol, ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir, headerCol, ir + 1, headerCol].Merge = true;
				ws.Cells[ir, headerCol, ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write tổng pc tháng +15

				ws.Column(headerCol).Width = (int)L.TONGPC;
				ws.Cells[ir, headerCol, ir + 1, headerCol].Value = "Tổng PC";
				ws.Cells[ir, headerCol, ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir, headerCol, ir + 1, headerCol].Merge = true;
				ws.Cells[ir, headerCol, ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region tiền lương +16 +17 +18 +19---------------------------------------------------------------------------------

				#region write tiền lương ở trên

				ws.Cells[ir, headerCol].Value = "Tiền lương";
				ws.Cells[ir, headerCol].Style.WrapText = true;
				ws.Cells[ir, headerCol, ir, headerCol + 3].Merge = true;
				ws.Cells[ir, headerCol, ir, headerCol + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

				#endregion

				#region write Luong CB +16

				ws.Column(headerCol).Width = (int)L.LUONGCB;
				ws.Cells[ir + 1, headerCol].Value = "Lương CB";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write Luong sp +17

				ws.Column(headerCol).Width = (int)L.LUONGSP;
				ws.Cells[ir + 1, headerCol].Value = "Lương SP";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write điều chỉnh lương tháng trước + 18

				ws.Column(headerCol).Width = (int)L.DIEUCHINH;
				ws.Cells[ir + 1, headerCol].Value = "Điều chỉnh lương tháng trước";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write tổng lương + 19

				ws.Column(headerCol).Width = (int)L.TONGLUONG;
				ws.Cells[ir + 1, headerCol].Value = "Tổng lương";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#endregion ----------------------------------------------------------------------------------------------

				#region phụ cấp lương +20 +21 +22 +23------------------------------------------------------------------------

				#region write phụ cấp lương ở trên

				ws.Cells[ir, headerCol].Value = "PC lương";
				ws.Cells[ir, headerCol].Style.WrapText = true;
				ws.Cells[ir, headerCol, ir, headerCol + 3].Merge = true;
				ws.Cells[ir, headerCol, ir, headerCol + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

				#endregion

				#region write phụ cấp theo Luong CB +20

				ws.Column(headerCol).Width = (int)L.PCLUONGCB;
				ws.Cells[ir + 1, headerCol].Value = "PC theo Lương CB";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write phụ cấp theo Luong SP +21

				ws.Column(headerCol).Width = (int)L.PCLUONGSP;
				ws.Cells[ir + 1, headerCol].Value = "PC theo Lương SP";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write bồi dưỡng ca 3 +22

				ws.Column(headerCol).Width = (int)L.BOIDUONGCA3;
				ws.Cells[ir + 1, headerCol].Value = "Bồi dưỡng ca 3";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write tổng pc +23

				ws.Column(headerCol).Width = (int)L.TONGPCLUONG;
				ws.Cells[ir + 1, headerCol].Value = "Tổng PC";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#endregion ------------------------------------------------------------------------

				#region  write tổng lương và phụ cấp +24

				ws.Column(headerCol).Width = (int)L.TONGLUONGPC;
				ws.Cells[ir, headerCol].Value = "Tổng lương và PC";
				ws.Cells[ir, headerCol].Style.WrapText = true; //sR vì chỉ có 1 dòng header
				ws.Cells[ir, headerCol, ir + 1, headerCol].Merge = true;
				ws.Cells[ir, headerCol, ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region khấu trừ  +25 +26 +27 ------------------------------------------------------------------------

				#region write khấu trừ ở trên

				ws.Cells[ir, headerCol].Value = "Khấu trừ";
				ws.Cells[ir, headerCol].Style.WrapText = true;
				ws.Cells[ir, headerCol, ir, headerCol + 2].Merge = true;
				ws.Cells[ir, headerCol, ir, headerCol + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

				#endregion

				#region write tạm ứng + 23

				ws.Column(headerCol).Width = (int)L.TAMUNG;
				ws.Cells[ir + 1, headerCol].Value = "Tạm ứng";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write khấu trừ y tế, bhxh + 24

				ws.Column(headerCol).Width = (int)L.BHXH;
				ws.Cells[ir + 1, headerCol].Value = "10,5% BHXH, YT, TN";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region write thu chi khac + 25

				ws.Column(headerCol).Width = (int)L.THUCHIKHAC;
				ws.Cells[ir + 1, headerCol].Value = "Thu chi khác";
				ws.Cells[ir + 1, headerCol].Style.WrapText = true;
				ws.Cells[ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#endregion

				#region  write thực lãnh +28

				ws.Column(headerCol).Width = (int)L.THUCLANH;
				ws.Cells[ir, headerCol].Value = "Thực lãnh";
				ws.Cells[ir, headerCol].Style.WrapText = true; //sR vì chỉ có 1 dòng header
				ws.Cells[ir, headerCol, ir + 1, headerCol].Merge = true;
				ws.Cells[ir, headerCol, ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#region  write ký nhận +29

				ws.Column(headerCol).Width = (int)L.KYNHAN;
				ws.Cells[ir, headerCol].Value = "Ký nhận";
				ws.Cells[ir, headerCol].Style.WrapText = true; //sR vì chỉ có 1 dòng header
				ws.Cells[ir, headerCol, ir + 1, headerCol].Merge = true;
				ws.Cells[ir, headerCol, ir + 1, headerCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				headerCol = headerCol + 1;

				#endregion

				#endregion

				ir++; ir++;// bỏ qua 2 dòng tiêu đề

				var pb_c1 = (from item in m_DSNVXemCong
							 select new { item.PBCap1.ID, item.PBCap1.TenPhongBan, item.PBCap1.ViTri }).Distinct().OrderBy(arg => arg.ViTri).ToList();

				foreach (var pb in pb_c1) {
					#region //write ten pb cap 1 tang 1 dong

					ic = startCol; // ghi từ cột bd, mỗi lần ghi 1 pb xong thì tăng dòng để ghi nv
					ws.Cells[ir, ic].Value = pb.TenPhongBan;
					ws.Cells[ir, ic].Style.Font.Bold = true;
					ws.Cells[ir, ic].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
					ir++;

					#endregion

					var dsnv = (from c in m_DSNVXemCong
								where c.PBCap1.ID == pb.ID
								select c).ToList();
					foreach (var nhanvien in dsnv) {
						// duyet tung nv, nhớ tăng sR
						ic = startCol; //reset startcol =1 mỗi lần ghi nv mới [140507_1]

						#region ghi thông tin

						#region stt. manv, ho ten, hsluong cb, heso luong sp

						sttnv++; // trước khi ghi thì tăng số tt nv vì init = 0, cộng rồi ghi thì init = 1
						XL.FormatCells(ws.Cells[ir, ic], sttnv, hAlign: ExcelHorizontalAlignment.Left);
						// stt start từ 1, nhớ +1 sau khi ghi vì đây là lặp for each
						ic++;

						XL.FormatCells(ws.Cells[ir, ic], nhanvien.MaNV, hAlign: ExcelHorizontalAlignment.Left); //MaNV
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.TenNV, hAlign: ExcelHorizontalAlignment.Left); //Ten
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.HeSo.LuongCB, numberFormat: "#0.00;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
						sum.hslcb1 += nhanvien.HeSo.LuongCB;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.HeSo.LuongCV, numberFormat: "#0.00;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
						sum.hslsp2 += nhanvien.HeSo.LuongCV;
						ic++;

						#endregion

						#region ngày công chuẩn  ban ngày +5, phép +6, học +7, trực đêm +8, chờ việc +9

						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Tong.CongThang, numberFormat: "#0.0#;-0;-?;",hAlign: ExcelHorizontalAlignment.Right);
						sum.cong3 += nhanvien.Tong.CongThang;
						//tổng công [tbd] xem lại tổng công tháng này phải bao gồm các công phép, h, lễ....
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Tong.Phep, numberFormat: "#0.0;-0;-?;",hAlign: ExcelHorizontalAlignment.Right); //
						sum.phep4 += nhanvien.Tong.Phep;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Tong.H_CT_PT + nhanvien.Tong.Le,numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
						sum.hop5 += (nhanvien.Tong.H_CT_PT + nhanvien.Tong.Le);
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Tong.NgayQuaDem, numberFormat: "#0;-0;-?;",hAlign: ExcelHorizontalAlignment.Right); // 
						sum.quadem6 += nhanvien.Tong.NgayQuaDem;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Tong.CongCV, numberFormat: "#0.0#;-0;-?;",hAlign: ExcelHorizontalAlignment.Right); // 
						sum.choviec7 += nhanvien.Tong.CongCV;
						ic++;

						#endregion

						#region CA 3 30%, làm thêm giờ 150% +10, 200% +11, 260% +12, tổng PC quy đổi ra công +13

						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Tong.PC30, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right); //pC30
						sum.pc30_8 += nhanvien.Tong.PC30;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Tong.PC50, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right); //pc50
						sum.pc50_9 += nhanvien.Tong.PC50;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Tong.PCTCC3, numberFormat: "#0.0#;-0;-?;",hAlign: ExcelHorizontalAlignment.Right); //pc200
						sum.pctcc3_10 += nhanvien.Tong.PCTCC3;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Tong.PC100, numberFormat: "#0.0#;-0;-?;",hAlign: ExcelHorizontalAlignment.Right); //pc200
						sum.pc100_11 += nhanvien.Tong.PC100;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Tong.PC160, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right); //pc200
						sum.pc160_12 += nhanvien.Tong.PC160;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Tong.PC200, numberFormat: "#0.0#;-0;-?;",hAlign: ExcelHorizontalAlignment.Right); //pc160
						sum.pc200_13 += nhanvien.Tong.PC200;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Tong.PC290, numberFormat: "#0.0#;-0;-?;",hAlign: ExcelHorizontalAlignment.Right); //pc160
						sum.pc290_14 += nhanvien.Tong.PC290;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic],
									   nhanvien.Tong.CongThang + nhanvien.Tong.Phep +
									   nhanvien.Tong.H_CT_PT + nhanvien.Tong.Le +
									   nhanvien.Tong.CongCV,
									   numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
						//tong cong m_thang //tbd
						sum.tongcong15 += nhanvien.Tong.CongThang + nhanvien.Tong.Phep + nhanvien.Tong.H_CT_PT + nhanvien.Tong.Le + nhanvien.Tong.CongCV;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic],
									   nhanvien.Tong.PC30 + nhanvien.Tong.PC50 + nhanvien.Tong.PCTCC3 +
									   nhanvien.Tong.PC100 + nhanvien.Tong.PC160 + nhanvien.Tong.PC200 + nhanvien.Tong.PC290,
									   numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right); //tbd chưa bao gồm pc khác
						sum.tongpc16 += nhanvien.Tong.PC30 + nhanvien.Tong.PC50 + nhanvien.Tong.PCTCC3 + nhanvien.Tong.PC100 + nhanvien.Tong.PC160 + nhanvien.Tong.PC200 + nhanvien.Tong.PC290;
						ic++;

						#endregion

						#region tiền lương +14 lương cb ; +15 lương sp; +16 lương tháng trước; +17 tổng lương theo công

						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Luong.LCB_TheoCongVaCV,numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right); //
						sum.luongcb17 += nhanvien.Luong.LCB_TheoCongVaCV;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Luong.LSP_TheoCong, numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right); //
						sum.luongsp18 += nhanvien.Luong.LSP_TheoCong;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Luong.LuongThangTruoc, numberFormat: "##,###,###,##0;-##,###,###,##0;-?;", hAlign: ExcelHorizontalAlignment.Right); //
						sum.dieuchinh19 += nhanvien.Luong.LuongThangTruoc;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Luong.TongLuong_TheoCong_Choviec_ThangTruoc, numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right); //tbd
						sum.tongluong20 += nhanvien.Luong.TongLuong_TheoCong_Choviec_ThangTruoc;
						ic++;

						#endregion

						#region tiền lương +18 lương cb ; +19 lương sp; +20 lương tháng trước; +21 tổng lương theo công

						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Luong.LCB_TheoPCap, numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right); //
						sum.pcluongcb21 += nhanvien.Luong.LCB_TheoPCap;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Luong.LSP_TheoPCap, numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right); //
						sum.pcluongsp22 += nhanvien.Luong.LSP_TheoPCap;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Luong.BoiDuongQuaDem, numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right); //
						sum.boiduongdem23 += nhanvien.Luong.BoiDuongQuaDem;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Luong.TongLuong_TheoPCap_BDCa3, numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right); //
						sum.tongpc24 += nhanvien.Luong.TongLuong_TheoPCap_BDCa3;
						ic++;

						#endregion

						#region  +24 tổng lương và pc

						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Luong.TongLuong, numberFormat: "##,###,###,##0;-0;-?;",hAlign: ExcelHorizontalAlignment.Right); //
						sum.tongluongpc25 += nhanvien.Luong.TongLuong;
						ic++;

						#endregion

						#region khấu trừ  +23 tạm ứng; +24 khấu trừ BH; +26 thu chi khác

						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Luong.TamUng, numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right); //
						sum.tamung26 += nhanvien.Luong.TamUng;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Luong.KhauTruBH, numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right); //
						sum.bh27 += nhanvien.Luong.KhauTruBH;
						ic++;
						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Luong.ThuChiKhac, numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right); //
						sum.thuchikhac28 += nhanvien.Luong.ThuChiKhac;
						ic++;

						#endregion

						#region  +28 thực lãnh

						XL.FormatCells(ws.Cells[ir, ic], nhanvien.Luong.ThucLanh, numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right); //
						sum.thuclanh29 += nhanvien.Luong.ThucLanh;
						ic++;

						#endregion

						#region +29 ký nhận

						XL.FormatCells(ws.Cells[ir, ic], null); //
						ic++;

						#endregion

						#endregion

						#region thống kê

						QLCB205_CongDon += nhanvien.Luong.LCB_TheoCongVaCV + nhanvien.Luong.LCB_TheoPCap;
						QLSP_CongDon += nhanvien.Luong.LSP_TheoCong + nhanvien.Luong.LSP_TheoPCap;
						LuongThangTruoc_CongDon += nhanvien.Luong.LuongThangTruoc;
						TongChiKhac_BDca3_CongDon += nhanvien.Luong.ThuChiKhac + nhanvien.Luong.BoiDuongQuaDem;

						#endregion

						ir++;

					}

				}
				#region write sum
				ic = 1; // bắt đầu từ cột hệ số lương 
				XL.FormatOneCell(ws, ref ir, ref ic, "", increaseCol: 1, bold: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatOneCell(ws, ref ir, ref ic, "", increaseCol: 1, bold: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatOneCell(ws, ref ir, ref ic, "Tổng cộng", increaseCol: 1, bold: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.hslcb1, increaseCol: 1, bold: true, numberFormat: "##,##0.00;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.hslsp2, increaseCol: 1, bold: true, numberFormat: "##,##0.00;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.cong3, increaseCol: 1, bold: true, numberFormat: "##,##0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.phep4, increaseCol: 1, bold: true, numberFormat: "##,##0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.hop5, increaseCol: 1, bold: true, numberFormat: "##,##0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.quadem6, increaseCol: 1, bold: true, numberFormat: "#,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.choviec7, increaseCol: 1, bold: true, numberFormat: "#,##0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.pc30_8, increaseCol: 1, bold: true, numberFormat: "##,##0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.pc50_9, increaseCol: 1, bold: true, numberFormat: "##,##0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.pctcc3_10, increaseCol: 1, bold: true, numberFormat: "##,##0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.pc100_11, increaseCol: 1, bold: true, numberFormat: "##,##0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.pc160_12, increaseCol: 1, bold: true, numberFormat: "##,##0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.pc200_13, increaseCol: 1, bold: true, numberFormat: "##,##0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.pc290_14, increaseCol: 1, bold: true, numberFormat: "##,##0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.tongcong15, increaseCol: 1, bold: true, numberFormat: "##,##0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.tongpc16, increaseCol: 1, bold: true, numberFormat: "##,##0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.luongcb17, increaseCol: 1, bold: true, numberFormat: "#,###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.luongsp18, increaseCol: 1, bold: true, numberFormat: "#,###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.dieuchinh19, increaseCol: 1, bold: true, numberFormat: "#,###,###,###,##0;-#,###,###,###,##0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.tongluong20, increaseCol: 1, bold: true, numberFormat: "#,###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.pcluongcb21, increaseCol: 1, bold: true, numberFormat: "#,###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.pcluongsp22, increaseCol: 1, bold: true, numberFormat: "#,###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.boiduongdem23, increaseCol: 1, bold: true, numberFormat: "#,###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.tongpc24, increaseCol: 1, bold: true, numberFormat: "#,###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.tongluongpc25, increaseCol: 1, bold: true, numberFormat: "#,###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.tamung26, increaseCol: 1, bold: true, numberFormat: "#,###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.bh27, increaseCol: 1, bold: true, numberFormat: "#,###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.thuchikhac28, increaseCol: 1, bold: true, numberFormat: "#,###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, sum.thuclanh29, increaseCol: 1, bold: true, numberFormat: "#,###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatOneCell(ws, ref ir, ref ic, "");
				
				// write số tiền bằng chữ
				ir++;
				var sotienbangchu = Math.Round(sum.thuclanh29, 3);
				var temp1 = "Số tiền bằng chữ: " + MyUtility.So_chu(sotienbangchu);
				ic = 17;
				XL.FormatOneCell(ws, ref ir, ref ic, temp1, VeBorder:false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);


				#endregion 

				ir += 2;

				var colGiamDoc = 5;
				var colKETOANT = 20;
				var colLapBieu = 27;

				XL.FormatCells(ws.Cells[ir, colGiamDoc], "GIÁM ĐỐC", VeBorder: false, size: 22, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
				XL.FormatCells(ws.Cells[ir, colKETOANT], "KẾ TOÁN TRƯỞNG", VeBorder: false, size: 22, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
				XL.FormatCells(ws.Cells[ir, colLapBieu], "LẬP BIỂU", VeBorder: false, size: 22, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);

				ir += 7;

				XL.FormatCells(ws.Cells[ir, colGiamDoc], "Lê Anh Tuấn", VeBorder: false, size: 22, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
				XL.FormatCells(ws.Cells[ir, colKETOANT], "Nguyễn Quốc Thái", VeBorder: false, size: 22, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
				XL.FormatCells(ws.Cells[ir, colLapBieu], "Nguyễn Đặng Xuân Tú", VeBorder: false, size: 22, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
			} catch (Exception exception) {
				lg.Error("XuatBBBangLuong", exception);
				throw exception;
			}

		}

		public void XuatBBBangThanhToanLuongCN(ExcelPackage p, DateTime ngayBD_Bef2D, DateTime ngayKT_Bef2D) {
			var ngaydauthang = ngayBD_Bef2D.AddDays(2d);

			try {
				p.Workbook.Worksheets.Add("Bang Thanh toan luong cong nhat");
				ExcelWorksheet ws = p.Workbook.Worksheets[2];
				ws.Name = "Bang Luong Cong Nhat"; //Setting Sheet's name
				ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
				ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

				ws.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

				XL2.VeLogo("bbluongcn", ws);
				XL2.GhiThongTinTongcty(ws, "", 1, 3,12, 1, 7,12, 4, 7);
				int startRow = 5, startCol = 1;
				var ir = startRow;

				#region 1.tieu de bang cham cong tu ngay den ngay 2. bỏ 1 dòng trống

				var headerCol = 4;
				var thang = ngayBD_Bef2D.AddDays(2);
				ir++;
				// vì ngày kết thúc là ngày đầu tiên của tháng sau nên -1 sẽ ra ngày đầu của tháng hiện tại
				XL.FormatCells(ws.Cells[ir + 0, headerCol], "BẢNG THANH TOÁN LƯƠNG THÁNG " + thang.ToString("MM/yyyy"), size: 14, bold: true, VeBorder: false, hAlign: ExcelHorizontalAlignment.CenterContinuous);
				XL.FormatCells(ws.Cells[ir + 1, headerCol], "CÔNG NHÂN CÔNG NHẬT, THỜI VỤ (chuyển khoản) " + thang.ToString("MM/yyyy"), size: 12, bold: true, VeBorder: false, hAlign: ExcelHorizontalAlignment.CenterContinuous);
				ir = ir + 3; // sR+2 là dòng trống

				#endregion

				var tableLuongCongNhat = SqlDataAccessHelper.ExecuteQueryString("select * from LuongCongNhat where Thang = @Thang and Nam =@Nam",
					new string[] { "@Thang", "@Nam" }, new object[] { ngaydauthang.Month, ngaydauthang.Year });

				var ic = startCol;
				#region write tiêu đề cột
				ws.Column(ic + 0).Width = (int)X.STT;
				XL.FormatCells(ws.Cells[ir, ic + 0], "STT", wraptext: true, bold: true);
				ws.Column(ic + 1).Width = (int)X.TEN;
				XL.FormatCells(ws.Cells[ir, ic + 1], "Họ và tên", wraptext: true, hAlign: ExcelHorizontalAlignment.Center, bold: true);
				ws.Column(ic + 2).Width = (int)X.CHUCVU;
				XL.FormatCells(ws.Cells[ir, ic + 2], "Chức danh nhiệm vụ", wraptext: true, hAlign: ExcelHorizontalAlignment.Center, bold: true);
				ws.Column(ic + 3).Width = (int)X.DONGIALUONG;
				XL.FormatCells(ws.Cells[ir, ic + 3], "Đơn giá lương", wraptext: true, hAlign: ExcelHorizontalAlignment.Center, bold: true);
				ws.Column(ic + 4).Width = (int)X.NGAYCONG;
				XL.FormatCells(ws.Cells[ir, ic + 4], "Số ngày công", wraptext: true, hAlign: ExcelHorizontalAlignment.Center, bold: true);
				ws.Column(ic + 5).Width = (int)X.THANHTIEN;
				XL.FormatCells(ws.Cells[ir, ic + 5], "Thành tiền", wraptext: true, hAlign: ExcelHorizontalAlignment.Center, bold: true);
				ws.Column(ic + 6).Width = (int)X.TAMUNG;
				XL.FormatCells(ws.Cells[ir, ic + 6], "Khấu trừ tạm ứng lương tháng " + ngaydauthang.ToString("M/yyyy"), wraptext: true, hAlign: ExcelHorizontalAlignment.Center, bold: true);
				ws.Column(ic + 7).Width = (int)X.THUCLANH;
				XL.FormatCells(ws.Cells[ir, ic + 7], "Thực lãnh", wraptext: true, hAlign: ExcelHorizontalAlignment.Center, bold: true);
				ws.Column(ic + 8).Width = (int)X.KYNHAN;
				XL.FormatCells(ws.Cells[ir, ic + 8], "Ký nhận", wraptext: true, hAlign: ExcelHorizontalAlignment.Center, bold: true);


				#endregion

				var stt = 0;
				var tongngaycong = 0d;
				var tongthanhtien = 0d;
				var tongtamung = 0d;
				var tongthuclanh = 0d;

				ir++;
				foreach (DataRow row in tableLuongCongNhat.Rows) {
					stt++;
					ic = startCol;
					var ten = row["Ten"].ToString();
					var chucvu = row["ChucVu"].ToString();
					var dongialuong = Convert.ToDouble(row["DonGiaLuong"]);
					var songaycong = Convert.ToDouble(row["NgayCong"]);
					var thanhtien = Convert.ToDouble(row["ThanhTien"]);
					var tamung = Convert.ToDouble(row["TamUng"]);
					var thuclanh = Convert.ToDouble(row["ThucLanh"]);

					tongngaycong += songaycong;
					tongthanhtien += thanhtien;
					tongtamung += tamung;
					tongthuclanh += thuclanh;

					XL.FormatCells(ws.Cells[ir, ic + 0], stt);
					XL.FormatCells(ws.Cells[ir, ic + 1], ten, hAlign: ExcelHorizontalAlignment.Left);
					XL.FormatCells(ws.Cells[ir, ic + 2], chucvu, hAlign: ExcelHorizontalAlignment.Left);
					XL.FormatCells(ws.Cells[ir, ic + 3], dongialuong, numberFormat: "###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatCells(ws.Cells[ir, ic + 4], songaycong, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatCells(ws.Cells[ir, ic + 5], thanhtien, numberFormat: "###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatCells(ws.Cells[ir, ic + 6], tamung, numberFormat: "###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatCells(ws.Cells[ir, ic + 7], thuclanh, numberFormat: "###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatCells(ws.Cells[ir, ic + 8], null, hAlign: ExcelHorizontalAlignment.Right);

					ir++;
				}

				#region CỘNG

				XL.FormatCells(ws.Cells[ir, ic + 0], null);
				XL.FormatCells(ws.Cells[ir, ic + 1], "CỘNG", bold: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatCells(ws.Cells[ir, ic + 2], null);
				XL.FormatCells(ws.Cells[ir, ic + 3], null);
				XL.FormatCells(ws.Cells[ir, ic + 4], tongngaycong, bold: true, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatCells(ws.Cells[ir, ic + 5], tongthanhtien, bold: true, numberFormat: "###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatCells(ws.Cells[ir, ic + 6], tongtamung, bold: true, numberFormat: "###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatCells(ws.Cells[ir, ic + 7], tongthuclanh, bold: true, numberFormat: "###,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatCells(ws.Cells[ir, ic + 8], null, hAlign: ExcelHorizontalAlignment.Right);
				ir++;
				ir++;
				var sotienbangchu = string.Format("Số tiền bằng chữ: {0}", MyUtility.So_chu(tongthuclanh));

				XL.FormatCells(ws.Cells[ir, 4], sotienbangchu, italic: true, VeBorder: false, hAlign: ExcelHorizontalAlignment.CenterContinuous);

				#endregion

				ir++;
				ir++;
				var colGiamDoc = 2;
				var colketoanT = 5;
				var colLapBieu = 8;

				XL.FormatCells(ws.Cells[ir, colGiamDoc], "GIÁM ĐỐC", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
				XL.FormatCells(ws.Cells[ir, colketoanT], "KẾ TOÁN TRƯỞNG", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
				XL.FormatCells(ws.Cells[ir, colLapBieu], "LẬP BIỂU", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);

				ir += 5;

				XL.FormatCells(ws.Cells[ir, colGiamDoc], "Lê Anh Tuấn", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
				XL.FormatCells(ws.Cells[ir, colketoanT], "Nguyễn Quốc Thái", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
				XL.FormatCells(ws.Cells[ir, colLapBieu], "Nguyễn Đặng Xuân Tú", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
			} catch (Exception exception) {
				lg.Error("XuatBBBangThanhToanLuongCN", exception);
				throw exception;
			}
		}
		public void XuatBBBangTongHopChiLuong(ExcelPackage p, List<cUserInfo> m_DSNVXemCong, DateTime ngayBD_Bef2D, DateTime ngayKT_Bef2D,
			double sanluong, double dongialuong1, double quyluong100per, double quyluongduphong20per, double quyluongchi80per, double ChiTienGiaCong,
			double quyluongCB, double quyluongSP, double tienluong1hesoSP, double quyluongcongnhat, double chikhac, double luongthangtruoc
			) {

			try {
				#region add sheet

				p.Workbook.Worksheets.Add("Bang Tong Hop Chi");
				ExcelWorksheet ws = p.Workbook.Worksheets[3];
				ws.Name = "Bang Tong Hop Chi"; //Setting Sheet's name

				#endregion

				#region font, format mac đinh cho toàn sheet

				ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
				ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

				ws.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

				#endregion

				#region vẽ logo và ghi thông tin tổng cty

				XL2.VeLogo("bbtonghopchi", ws);
				XL2.GhiThongTinTongcty(ws, "", 1, 5,12, 1, 11,12, 4, 11);

				#endregion

				int startRow = 5;
				var ir = startRow;

				#region 1.tieu de  2. bỏ 1 dòng trống

				var headerCol = 6;
				var ngaydauthang = ngayBD_Bef2D.AddDays(2d);
				var ngaycuoithang = ngayKT_Bef2D.AddDays(-2d);
				// vì ngày kết thúc là ngày đầu tiên của tháng sau nên -1 sẽ ra ngày đầu của tháng hiện tại
				ir++;
				XL.FormatCells(ws.Cells[ir + 0, headerCol], "BẢNG TỔNG HỢP CÁC NGUỒN CHI TỪ QUỸ LƯƠNG THÁNG " + ngaydauthang.ToString("MM/yyyy"), VeBorder: false, size: 14, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
				ir = ir + 2; // sR+1 là dòng trống

				#endregion

				var s21 = "1. Quỹ lương theo sản phẩm tiêu thụ gói (đồng)";
				var s22 = "Quỹ lương theo sản phẩm tiêu thụ thanh toán lương (80%)";
				var s23 = "Quỹ lương dự phòng (20%)";
				var s24 = "Sản lượng tiêu thụ (gói)";
				var s25 = "Đơn giá lương (đồng/gói) ";
				var s26 = "2. Quỹ lương gia công (đồng) ";
				var s27 = "Tổng quỹ lương thanh toán hàng tháng (đồng) ";
				var trongdo = "Trong đó ";
				var s9 = "Quỹ lương theo nghị định 205/CP ";
				var s10 = "Quỹ lương công nhật, thời vụ, khoán việc ";
				var s11 = "Chi khác từ quỹ lương ";
				var s12 = "Điều chỉnh lương tháng trước và truy lãnh lương ";
				var s13 = "Quỹ lương theo hệ số (sản phẩm) ";
				var s14 = "---> Tiền lương 1 hệ số (sản phẩm) ";

				#region write tiêu đề cột
				ws.Column(9).Width = 16;
				XL.FormatCells(ws.Cells[ir, 1, ir, 8], s21, VeBorder: false, merge: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatCells(ws.Cells[ir, 9], quyluong100per, numberFormat: "##,###,###,##0;-0;-?;", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.Right);
				ir++;
				XL.FormatCells(ws.Cells[ir, 1, ir, 8], s22, VeBorder: false, bold:true, merge: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatCells(ws.Cells[ir, 9], quyluongchi80per, numberFormat: "##,###,###,##0;-0;-?;", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.Right);
				ir++;
				XL.FormatCells(ws.Cells[ir, 1, ir, 8], s23, VeBorder: false, merge: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatCells(ws.Cells[ir, 9], quyluongduphong20per, numberFormat: "##,###,###,##0;-0;-?;", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.Right);
				ir++;
				XL.FormatCells(ws.Cells[ir, 1, ir, 8], s24, VeBorder: false, merge: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatCells(ws.Cells[ir, 9], sanluong, numberFormat: "##,###,###,##0;-0;-?;", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.Right);
				ir++;
				XL.FormatCells(ws.Cells[ir, 1, ir, 8], s25, VeBorder: false, merge: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatCells(ws.Cells[ir, 9], dongialuong1, numberFormat: "#,##0;-0;-?;", bold: true, VeBorder: false, hAlign: ExcelHorizontalAlignment.Right);
				ir++;
				XL.FormatCells(ws.Cells[ir, 1, ir, 8], s26, VeBorder: false, bold: true, merge: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatCells(ws.Cells[ir, 9], ChiTienGiaCong, numberFormat: "##,###,###,##0;-0;-?;", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.Right);
				ir++;
				XL.FormatCells(ws.Cells[ir, 1, ir, 8], s27, VeBorder: false, bold: true, merge: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatCells(ws.Cells[ir, 9], (quyluongchi80per + ChiTienGiaCong), numberFormat: "##,###,###,##0;-0;-?;", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.Right);
				ir++;
				XL.FormatCells(ws.Cells[ir, 1, ir, 8], trongdo, merge: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatCells(ws.Cells[ir, 9], null);
				ir++;
				XL.FormatCells(ws.Cells[ir, 1, ir, 8], s9, merge: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatCells(ws.Cells[ir, 9], quyluongCB, bold: true, numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				ir++;
				XL.FormatCells(ws.Cells[ir, 1, ir, 8], s10, merge: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatCells(ws.Cells[ir, 9], quyluongcongnhat, bold: true, numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				ir++;
				XL.FormatCells(ws.Cells[ir, 1, ir, 8], s11, merge: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatCells(ws.Cells[ir, 9], chikhac, bold: true, numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				ir++;
				XL.FormatCells(ws.Cells[ir, 1, ir, 8], s12, merge: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatCells(ws.Cells[ir, 9], luongthangtruoc, bold: true, numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				ir++;
				XL.FormatCells(ws.Cells[ir, 1, ir, 8], s13, merge: true, hAlign: ExcelHorizontalAlignment.Left);
				XL.FormatCells(ws.Cells[ir, 9], quyluongSP, bold: true, numberFormat: "##,###,###,##0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				ir++;
				XL.FormatCells(ws.Cells[ir, 1, ir, 8], s14, merge: true, hAlign: ExcelHorizontalAlignment.Right);
				XL.FormatCells(ws.Cells[ir, 9], tienluong1hesoSP, bold: true, numberFormat: "##,###,###,##0.000;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
				ir++;

				#endregion

				#region LẬP BIỂU, KÝ TÊN

				ir += 2;
				var colgiamdoc = 2;
				var collapbieu = 9;
				XL.FormatCells(ws.Cells[ir, colgiamdoc], "GIÁM ĐỐC", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
				XL.FormatCells(ws.Cells[ir, collapbieu], "LẬP BIỂU", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);

				ir += 7;

				XL.FormatCells(ws.Cells[ir, colgiamdoc], "Lê Anh Tuấn", VeBorder: false, bold: true,
					hAlign: ExcelHorizontalAlignment.CenterContinuous);
				XL.FormatCells(ws.Cells[ir, collapbieu], "Nguyễn Đặng Xuân Tú", VeBorder: false, bold: true,
					hAlign: ExcelHorizontalAlignment.CenterContinuous);

				#endregion
			} catch (Exception exception) {
				lg.Error("XuatBBBangTongHopChiLuong", exception);
				throw exception;
			}

		}

		private void btnKetCong_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			var tempThang = dtpThang.Value;
			var ngaycuoithang = new DateTime(tempThang.Year, tempThang.Month, DateTime.DaysInMonth(tempThang.Year, tempThang.Month));
			var chuoi = "Bạn muốn đóng kết công tháng {0}?\n Lưu ý: Sau khi đóng kết công sẽ không thể thay đổi hoặc xác nhận lại dữ liệu chấm công của tháng kết công.\nBấm Yes để tiếp tục. Bấm No để huỷ. ";
			chuoi = string.Format(chuoi, ngaycuoithang.ToString("MM/yyyy"));
			if (MessageBox.Show(chuoi, "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}
			var SanLuong = Convert.ToInt32(numSanLuong.Value);
			var DonGia = Convert.ToInt32(numDonGia.Value);
			var ChiTienGiaCong = Convert.ToDecimal(numChiTienGiaCong.Value);
			var LuongToiThieu = Convert.ToInt32(numLuongTT.Value);
			var BoiDuongCa3 = Convert.ToInt32(numBoiDuongCa3.Value);
			var query = @"	update	ThangKetCong 
							set		Khoa = @Khoa , SanLuong = @SanLuong, DonGia = @DonGia, ChiTienGiaCong = @ChiTienGiaCong, 
									LuongToiThieu = @LuongToiThieu, BoiDuongCa3 = @BoiDuongCa3
							where	Thang = @Thang 
							if (@@RowCount = 0) 
								insert into ThangKetCong (Thang, Khoa, SanLuong, DonGia, ChiTienGiaCong, LuongToiThieu, BoiDuongCa3) 
								values (@Thang, @Khoa,@SanLuong,@DonGia,@ChiTienGiaCong,@LuongToiThieu,@BoiDuongCa3) ";
			int kq = SqlDataAccessHelper.ExecNoneQueryString(query, 
				new string[] { "@Thang", "@Khoa", "@SanLuong", "@DonGia", "@ChiTienGiaCong", "@LuongToiThieu", "@BoiDuongCa3"}, 
				new object[] { ngaycuoithang, 1, SanLuong, DonGia, ChiTienGiaCong, LuongToiThieu, BoiDuongCa3});
			if (kq != 0) {
				XL2.ThangKetCong = ngaycuoithang;
				AutoClosingMessageBox.Show("Đã đóng kết công tháng.", "Thông báo", 2000);
				MyUtility.EnableDisableControl(false, new Control[] { numSanLuong, numDonGia, numChiTienGiaCong, numLuongTT, numBoiDuongCa3 });

			}
			else {
				MessageBox.Show("Đóng kết công tháng bị lỗi.\nVui lòng liên hệ bộ phận kỹ thuật để được trợ giúp.", "Lỗi", MessageBoxButtons.OK);
			}
		}

		private void btnMoKetCong_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			var tempThang = dtpThang.Value;
			var ngaycuoithang = new DateTime(tempThang.Year, tempThang.Month, DateTime.DaysInMonth(tempThang.Year, tempThang.Month));
			var chuoi = "Bạn muốn mở kết công tháng {0}?\n Lưu ý: Sau khi mở kết công có thể thay đổi hoặc xác nhận lại dữ liệu chấm công của nhân viên.\nBấm Yes để tiếp tục. Bấm No để huỷ. ";
			chuoi = string.Format(chuoi, ngaycuoithang.ToString("MM/yyyy"));
			if (MessageBox.Show(chuoi, "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}
			var query = @"	update ThangKetCong set Khoa = @Khoa where Thang = @Thang 
							if (@@RowCount = 0) 
								insert into ThangKetCong (Thang, Khoa) values (@Thang, @Khoa) ";
			int kq = SqlDataAccessHelper.ExecNoneQueryString(query, new string[] { "@Thang", "@Khoa" }, new object[] { ngaycuoithang, 0 });
			if (kq != 0) {
				AutoClosingMessageBox.Show("Đã mở kết công tháng.", "Thông báo", 2000);
				MyUtility.EnableDisableControl(true, new Control[] { numSanLuong, numDonGia, numChiTienGiaCong, numLuongTT, numBoiDuongCa3 });
				XL2.ThangKetCong = XL.DocThangKetCong();
			}
			else {
				MessageBox.Show("Mở kết công tháng bị lỗi.\nVui lòng liên hệ bộ phận kỹ thuật để được trợ giúp.", "Lỗi", MessageBoxButtons.OK);
			}
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void frm_41_TinhLuongNV_Load(object sender, EventArgs e) {

		}
	}
}
