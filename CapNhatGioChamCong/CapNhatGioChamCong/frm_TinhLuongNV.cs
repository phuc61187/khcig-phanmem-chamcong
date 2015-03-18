using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapNhatGioChamCong.BUS;
using CapNhatGioChamCong.DAO;
using CapNhatGioChamCong.DTO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CapNhatGioChamCong {
	public partial class frm_TinhLuongNV : Form {
		public frm_TinhLuongNV() {
			InitializeComponent();

		}

		private void btnTinhLuongVaXuatBB_Click(object sender, EventArgs e) {
			DateTime thang = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);
			DateTime ngayBD = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);
			DateTime ngayKT = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, DateTime.DaysInMonth(dtpThang.Value.Year, dtpThang.Value.Month));
			ngayBD = ngayBD.AddDays(-1d);
			ngayKT = ngayKT.AddDays(2d).Subtract(new TimeSpan(0, 0, 1));
			DataTable tableAllDSNV = SqlDataAccessHelper.ExecuteQueryString(ThamSo.SelStrLayDSNV(), null, null);
			List<cUserInfo> dsnv = new List<cUserInfo>();
			KhoitaoDSNV(tableAllDSNV, dsnv);

			XL.XemCong(dsnv, ngayBD, ngayKT);
			double dsanluong = (double)(numSanLuong.Value);
			double ddongia = (double)numDonGia.Value;
			double dluongtoithieu = (double)numLuongTT.Value;
			double dcongnhat = (double)numLuongCongNhat.Value;
			double dboiduongca3 = (double)numBoiDuongCa3.Value;
			XL.DocLuongDieuChinh(thang, dsnv);
			XL.TinhLuong(thang, dsnv, ddongia, dsanluong, dluongtoithieu, dcongnhat, dboiduongca3);

			saveFileDialog.Filter = "Excel File|*.xlsx";
			saveFileDialog.ShowDialog();
			if (saveFileDialog.FileName == string.Empty) {
				return;
			}

			using (ExcelPackage p = new ExcelPackage()) {
				//Create a sheet
				p.Workbook.Worksheets.Add("Bang Luong");
				ExcelWorksheet ws = p.Workbook.Worksheets[1];
				ws.Name = "Bang Luong"; //Setting Sheet's name
				ws.Cells.Style.Font.Size = 8; //Default font size for whole sheet
				ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

				ws.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				//Merging cells and create a center heading for out table

				int iLastRow = 0, iLastCol = 0;
				ws.Cells[2, 1].Value = "STT";
				ws.Cells[2, 2].Value = "Tên NV";
				ws.Cells[2, 3].Value = "Mã CC";
				ws.Cells[2, 4].Value = "T.Công";
				ws.Cells[2, 5].Value = "T.PC";
				ws.Cells[2, 6].Value = "Công CV";
				ws.Cells[2, 7].Value = "Lương CB";
				ws.Cells[2, 8].Value = "Làm qua đêm";
				ws.Cells[2, 9].Value = "Bồi dưỡng ca 3";
				ws.Cells[2, 10].Value = "Lương SP";
				ws.Cells[2, 11].Value = "Điều chỉnh lương tháng trước";
				ws.Cells[2, 12].Value = "Tổng lương";
				ws.Cells[2, 1].Style.WrapText = true;
				ws.Cells[2, 2].Style.WrapText = true;
				ws.Cells[2, 3].Style.WrapText = true;
				ws.Cells[2, 4].Style.WrapText = true;
				ws.Cells[2, 5].Style.WrapText = true;
				ws.Cells[2, 6].Style.WrapText = true;
				ws.Cells[2, 7].Style.WrapText = true;
				ws.Cells[2, 8].Style.WrapText = true;
				ws.Cells[2, 9].Style.WrapText = true;
				ws.Cells[2, 10].Style.WrapText = true;
				ws.Cells[2, 11].Style.WrapText = true;
				ws.Cells[2, 12].Style.WrapText = true;


				for (int iRow = 3, index = 0; index < dsnv.Count; iRow++, index++) {
					cUserInfo nv = dsnv[index];
					ws.Cells[iRow, 1].Value = index + 1;
					ws.Cells[iRow, 2].Value = nv.UserFullName;
					ws.Cells[iRow, 3].Value = nv.UserEnrollNumber;
					ws.Cells[iRow, 4].Value = nv.TongCongThang.ToString();//;
					ws.Cells[iRow, 5].Value = nv.TongPCapThang.ToString();//;
					ws.Cells[iRow, 6].Value = nv.TongCongCV.ToString("#0.#");//;
					ws.Cells[iRow, 7].Value = nv.Luong.LuongCB.ToString("###,###,##0.###");//;
					ws.Cells[iRow, 8].Value = nv.TongNgayQuaDem.ToString("#0");//;
					ws.Cells[iRow, 9].Value = nv.Luong.BoiDuongQuaDem.ToString("###,##0");//;
					ws.Cells[iRow, 10].Value = nv.Luong.LuongSP.ToString("###,###,##0.###");//;
					ws.Cells[iRow, 11].Value = nv.Luong.LuongThangTruoc.ToString("###,###,##0.###");//;
					ws.Cells[iRow, 12].Value = nv.Luong.TongLuong.ToString("###,###,##0.###");//;
					ws.Cells[iRow, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
					ws.Cells[iRow, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
					ws.Cells[iRow, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
					ws.Cells[iRow, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
					ws.Cells[iRow, 5].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
					ws.Cells[iRow, 6].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
					ws.Cells[iRow, 7].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
					ws.Cells[iRow, 8].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
					ws.Cells[iRow, 9].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
					ws.Cells[iRow, 10].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
					ws.Cells[iRow, 11].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
					ws.Cells[iRow, 12].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				}
				ws.Column(1).Width = 4;
				ws.Column(2).Width = 18;
				ws.Column(3).Width = 6;
				ws.Column(4).Width = 8;
				ws.Column(5).Width = 8;
				ws.Column(6).Width = 8;
				ws.Column(7).Width = 11.5;
				ws.Column(8).Width = 8;
				ws.Column(9).Width = 10;
				ws.Column(10).Width = 11.5;
				ws.Column(11).Width = 11.5;
				ws.Column(12).Width = 15;
				ws.Cells[1, 1, 1, 12].Value = "Bảng lương tháng " + dtpThang.Value.ToString("MM/yyyy");
				ws.Cells[1, 1, 1, 12].Style.Font.Bold = true;
				ws.Cells[1, 1, 1, 12].Style.Font.Size = 14;
				ws.Cells[1, 1, 1, 12].Merge = true;
				ws.Cells[2, 1, 2, 12].Style.Font.Bold = true;


				Byte[] bin = p.GetAsByteArray();

				string file_path = saveFileDialog.FileName;

				File.WriteAllBytes(file_path, bin);

			}
			saveFileDialog.FileName = string.Empty;
		}

		private void KhoitaoDSNV(DataTable TableDSNV, List<cUserInfo> dsnv) {
			if (TableDSNV == null || TableDSNV.Rows.Count == 0) return;

			foreach (DataRow row in TableDSNV.Rows) {
				cShiftSchedule tmpLichTrinh = ThamSo.DSLichTrinh.Find(item => item.SchID == (int)row["SchID"]);
				List<cShift> tmpDSCa = tmpLichTrinh.ListT1;
				List<cShift> tmpDSCaMoRong = ThamSo.TaoDSCaMoRong(tmpDSCa); // đã bao gồm Khác(int.Minvalue)
				//List<cShift> tmpDSCaChonGio = new List<cShift>(ThamSo.DSCa);
				cUserInfo nhanvien = new cUserInfo() {
					UserEnrollNumber = (int)row["UserEnrollNumber"], UserFullName = row["UserFullName"].ToString(),
					LichTrinhLV = tmpLichTrinh, DSCa = tmpDSCa, DSCaMoRong = tmpDSCaMoRong,
					HeSoLuongCB = (Single)row["HeSoLuongCB"], HeSoLuongSP = (Single)row["HeSoLuongSP"],
					//BoPhan = new cPhongBan() { ID = (int)row["UserIDD"], TenPhongBan = row["Description"].ToString() },
					MacDinhTinhPC150 = (bool)row["TinhPC150"],
					Luong = new cLuongThang(),
				};
				nhanvien.ClearAll();
				dsnv.Add(nhanvien);
			}
		}

		int DemSoNgayCN(DateTime ngayDauThang) {
			DateTime ngayBD = ngayDauThang;
			DateTime ngayKT = new DateTime(ngayDauThang.Year, ngayDauThang.Month, DateTime.DaysInMonth(ngayDauThang.Year, ngayDauThang.Month));
			int kq = 0;
			for (DateTime ngaydem = ngayBD; ngaydem.Date <= ngayKT.Date; ngaydem = ngaydem.AddDays(1d)) {
				if (ngaydem.DayOfWeek != DayOfWeek.Sunday) continue;
				kq++;
			}
			return kq;
		}

		private void btnDieuChinhLuong_Click(object sender, EventArgs e) {
			frm_DieuChinhLuongThangTruoc frm = new frm_DieuChinhLuongThangTruoc(){ thang = dtpThang.Value.AddMonths(-1)};
			frm.ShowDialog();
		}


	}
}
