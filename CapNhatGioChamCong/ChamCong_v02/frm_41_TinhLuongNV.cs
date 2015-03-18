using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v02.BUS;
using ChamCong_v02.DAO;
using ChamCong_v02.DTO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using log4net;


namespace ChamCong_v02 {
	public partial class frm_41_TinhLuongNV : Form {
		// vấn đề tồn đọng: 1. xuất báo biểu theo từng phòng ban cấp 2-1
		// 2.format các ô dữ liệu excel xuất
		// 3. save chồng lên file đang mở thì báo lỗi
		// 4. kiểm tra thêm cho hệ số cộng thêm vào bảo hiểm cho GD, PGD
		public readonly ILog lg = LogManager.GetLogger("frm_41_TinhLuongNV");

		public frm_41_TinhLuongNV() {
			InitializeComponent();
			DateTime today = DateTime.Today;
			dtpThang.Value = new DateTime(today.Year, today.Month, today.Day);
		}

		private void btnTinhLuongVaXuatBB_Click(object sender, EventArgs e) {
			DateTime thang = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);
			DateTime ngayBD = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);
			DateTime ngayKT = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, DateTime.DaysInMonth(dtpThang.Value.Year, dtpThang.Value.Month));
			ngayBD = ngayBD.AddDays(-1d);
			ngayKT = ngayKT.AddDays(2d).Subtract(new TimeSpan(0, 0, 1));
			DataTable tableAllDSNV = DAL.LayDSTatCaNV();
			List<cUserInfo> dsnv = new List<cUserInfo>();
			int[] arrUserEnrollNumber = (from DataRow row in tableAllDSNV.Rows select (int) row["UserEnrollNumber"]).ToArray();

			// lấy thông tin mức đóng bảo hiểm
			//int sMucdong = int.Parse(string.IsNullOrWhiteSpace(tbMucDongBH.Text) ? "0" : tbMucDongBH.Text);
			// nếu <100 thì 3 con số nếu > 100 thì 3 con số, nếu > 1000 thì 4 con số

			Double dMucdong = Double.Parse((string.IsNullOrWhiteSpace(tbMucDongBH.Text) ? "0" : tbMucDongBH.Text));

			dsnv = XL.XemCong2(tableAllDSNV, arrUserEnrollNumber, ngayBD, ngayKT);
			var dsanluong = (double)(numSanLuong.Value);
			double ddongia = (double)numDonGia.Value;
			double dluongtoithieu = (double)numLuongTT.Value;
			double dcongnhat = (double)numLuongCongNhat.Value;
			double dboiduongca3 = (double)numBoiDuongCa3.Value;
			XL.DocLuongDieuChinh(thang, dsnv);
			XL.DocHSBHCongThem(dsnv);
			#region đọc file tạm ứng lương
			frm_DocFileTamUngLuong frm = new frm_DocFileTamUngLuong { m_dsnv = dsnv, temp = false };
			frm.ShowDialog();
			if (frm.temp == false) // xảy ra lỗi trong quá trình đọc file tạm ứng lương thì 1.(không cần báo lỗi vì form bên kia đã báo) 2.thoát ko làm nửa
				return;
			#endregion
			// tính khấu trừ bảo hiểm
			XL.TinhKhauTruBHChoNV(dsnv, dluongtoithieu, dMucdong);

			XL.TinhLuong2(thang, dsnv, ddongia, dsanluong, dluongtoithieu, dcongnhat, dboiduongca3);

			saveFileDialog.Filter = "Excel File|*.xlsx";
			saveFileDialog.ShowDialog();
			if (saveFileDialog.FileName == string.Empty) {
				return;
			}
			XL.XuatBBTinhLuong(dsnv, saveFileDialog.FileName, ngayBD, ngayKT);

		}

		private void btnDieuChinhLuong_Click(object sender, EventArgs e) {
			frm_42_DieuChinhLuongThangTruoc frm = new frm_42_DieuChinhLuongThangTruoc() { thang = dtpThang.Value.AddMonths(-1) };
			frm.ShowDialog();
		}

		private void btnThayDoiHSL_Click(object sender, EventArgs e) {
			frm_43_ThayDoiHeSoLuong_PC frm43 = new frm_43_ThayDoiHeSoLuong_PC();
			frm43.ShowDialog();
		}


	}
}
