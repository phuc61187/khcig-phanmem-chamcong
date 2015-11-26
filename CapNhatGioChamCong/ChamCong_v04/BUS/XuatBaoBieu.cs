using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using ChamCong_v04.DAL;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;
using log4net;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ChamCong_v04.BUS {
	public static partial class XL {
		public static readonly ILog lg = LogManager.GetLogger("XuatBB");

		public static void EXP_Header(ExcelWorksheet ws, ref int top1,int left1 = 1, int left2 = 2, int leftT = 3,
			int size1 = 12, int size2 = 12, int sizeT = 12,
			DateTime? ngayLap = null,
			string headerString = null,
			string logoName = null
			) {
			int ir, ic;

			ir = top1;
			ic = left1;
			XL.FormatCell_T(ws, ref ir, ref ic, size: size1, Bold: false, VeBorder: false, wrapText: false, plusRow: 1, value: Settings.Default.headerTCty);
			XL.FormatCell_T(ws, ref ir, ref ic, size: size1, Bold: false, VeBorder: false, wrapText: false, plusRow: 1, value: Settings.Default.headerTNHH);
			XL.FormatCell_T(ws, ref ir, ref ic, size: size1, Bold: false, VeBorder: false, wrapText: false, plusRow: 1, value: Settings.Default.headerCNS);
			XL.FormatCell_T(ws, ref ir, ref ic, size: size1, Bold: true, VeBorder: false, wrapText: false, plusRow: 1, value: Settings.Default.headerNMTLKH);

			ir = top1;
			ic = left2;
			XL.FormatCell_T(ws, ref ir, ref ic, size: size2, Bold: true, VeBorder: false, wrapText: false, plusRow: 1, value: Settings.Default.headerCHXHCNVN, hAlign: ExcelHorizontalAlignment.CenterContinuous);
			XL.FormatCell_T(ws, ref ir, ref ic, size: size2, Bold: true, VeBorder: false, wrapText: false, plusRow: 1, value: Settings.Default.headerDL_TD_HP, hAlign: ExcelHorizontalAlignment.CenterContinuous);
			XL.FormatCell_T(ws, ref ir, ref ic, size: size2, Bold: false, VeBorder: false, wrapText: false, plusRow: 1, value: string.Empty, hAlign: ExcelHorizontalAlignment.CenterContinuous);
			XL.FormatCell_T(ws, ref ir, ref ic, size: size2, Bold: false, Italic:true, VeBorder: false, wrapText: false, 
				value: string.Format(Settings.Default.headerTPHCMNgaythangnam, ((DateTime)ngayLap).Day, ((DateTime)ngayLap).Month, ((DateTime)ngayLap).Year), hAlign: ExcelHorizontalAlignment.CenterContinuous);

			ir = ir + 2;// bỏ 2 row rồi bắt đầu ghi title
			ic = leftT;
			XL.FormatCell_T(ws, ref  ir, ref ic, size: sizeT, Bold: true, VeBorder: false, wrapText: false, value: headerString);
			ir = ir + 2;// bỏ 2 row rồi bắt đầu ghi nội dung
			top1 = ir;


/*
			var p = ws.Drawings.AddPicture(logoName, Resources.CNS);
			p.SetPosition(0, 0, 0, 0);
			p.SetSize(100);
*/
		}
		public static void EXP_Footer(ExcelWorksheet ws, ref int top1,
	int[] left, int[] size, string[] chucVu, string[] ten, int khoangCachTen_ChucVu = 5
	) {

			for (int i = 0; i < left.Length; i++) {
				int ir = top1, ic = left[i];
				int tempSize = size[i];
				XL.FormatCell_W(ws, ref ir, ref ic, size: tempSize, Bold: true, VeBorder: false, value: chucVu[i], hAlign: ExcelHorizontalAlignment.Center);
				ir = ir + khoangCachTen_ChucVu;
				XL.FormatCell_W(ws, ref ir, ref ic, size: tempSize, Bold: true, VeBorder: false, value: ten[i], hAlign: ExcelHorizontalAlignment.Center);

			}
		}
		public static void EXP_KyHieuCC(ExcelWorksheet ws, ref int top, int[] left, out int bottom, DataTable tableShift, DataTable tableAbsent) {
			var Groups = (from DataRow row in tableShift.Rows
						  group row by row["KyHieuCC"].ToString() into g
						  select g).ToList();
			List<string> t = new List<string>();
			for (int index = 0; index < Groups.Count; index++) {
				var @group = Groups[index];
				var temp = @group.Key;
				temp += " : " + string.Join(",", (from DataRow item in @group select item["ShiftCode"]).ToArray());
				t.Add(temp);
			}
			t.Add("4: Ca tự do tính công trên cơ sở 4h làm việc");
			t.Add("8: Ca tự do tính công trên cơ sở 8h làm việc");
			t.Add("D: Ca dài tính công trên cơ sở 12h làm việc");
			List<string> t2 = new List<string>();
			t2.Add("(tc): có phụ cấp tăng cường");
			t2.Add("(t3): có phụ cấp tăng cường làm việc ban đêm");
			t2.Add("(x2): có phụ cấp làm việc vào ngày nghỉ");
			t2.Add("(x2đ): có phụ cấp làm việc ban đêm vào ngày nghỉ");
			t2.Add("(x3): có phụ cấp làm việc, trực ngày lễ, tết");
			t2.Add("(x3đ): có phụ cấp làm việc, trực ban đêm ngày lễ, tết");
			t2.Add("(xYC): có phụ cấp theo yêu cầu quản lý");
			t2.Add("(bt): sử dụng quỹ tăng cường cho thời gian vào trễ");
			t2.Add("(bs): sử dụng quỹ tăng cường cho thời gian ra sớm");
			t2.Add("(p2): nghỉ phép 2 giờ");
			t2.Add("(nP): nghỉ phép 4 giờ");
			t2.Add("(p6): nghỉ phép 6 giờ");
			List<string> t3 = new List<string>();
			t3.Add("n: nửa công (hoặc nửa ngày)");
			t3.Add("L: 1 công lễ, tết");
			t3.AddRange((from DataRow row in tableAbsent.Rows select row["AbsentDescription"].ToString()).ToList());

			int ir, ic, bottomtemp;
			ir = top;
			ic = left[0];
			XL.FormatCell_W(ws, ref ir, ref ic, value: "Ghi chú ký hiệu:", VeBorder: false);
			ir = top;
			ic = left[1];
			bottomtemp = top;
			foreach (string @string in t) {
				XL.FormatCell_W(ws, ref ir, ref ic, value: @string, VeBorder: false, plusRow: 1);
			}
			if (ir > bottomtemp) bottomtemp = ir;

			ir = top;
			ic = left[2];
			foreach (string @string in t2) {
				XL.FormatCell_W(ws, ref ir, ref ic, value: @string, VeBorder: false, plusRow: 1);
			}
			if (ir > bottomtemp) bottomtemp = ir;

			ir = top;
			ic = left[3];
			foreach (string @string in t3) {
				XL.FormatCell_W(ws, ref ir, ref ic, value: @string, VeBorder: false, plusRow: 1);
			}
			if (ir > bottomtemp) bottomtemp = ir;
			bottom = new int();
			bottom = bottomtemp;
		}


		public static void ExportSheetThongSo(ExcelWorksheet ws, DateTime ngaydauthang, DataTable tableThongSo) {
			int ic = 1, left = 1, ir = 2, top = 2;
			string temp = string.Empty;

			ir = top;
			XL.FormatCell(ws, ref ir, ref ic, "Tháng", plusCol: 1, hAlign: ExcelHorizontalAlignment.Left);
			XL.FormatCell(ws, ref ir, ref ic, ngaydauthang, plusRow: 1, numFormat: "MM/yyyy");
			ic = left;

			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Sản lượng", "SanLuong", "gói", numberFormat: Settings.Default.numFormatInt);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Đơn giá", "DonGia", "đồng", numberFormat: Settings.Default.numFormatInt);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Sản lượng gia công nội bộ", "SanLuongGiaCongNoiBo", numberFormat: Settings.Default.numFormatInt);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Đơn giá gia công nội bộ", "DonGiaGiaCongNoiBo", "đồng", numberFormat: Settings.Default.numFormatInt);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Sản lượng gia công ngoài", "SanLuongGiaCongNgoai", numberFormat: Settings.Default.numFormatInt);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Đơn giá gia công ngoài", "DonGiaGiaCongNgoai", "đồng", numberFormat: Settings.Default.numFormatInt);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Trích quỹ lương", "TrichQuyLuong", "%", numberFormat: Settings.Default.numFormatInt);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Mức lương tối thiểu", "MucLuongToiThieu", "đồng", numberFormat: Settings.Default.numFormatMoney);
			//v4.0.0.7 boBDC3 func1(ws, ref ir, ref ic, ref left, tableThongSo, "Bồi dưỡng ca 3", "BoiDuongCa3", "đồng", numberFormat: Settings.Default.numFormatMoney);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Định mức tiền cơm trưa tháng", "DinhMucComTrua", "đồng", Settings.Default.numFormatMoney);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Phụ cấp ca đêm", "HSPCDem", "%", numberFormat: Settings.Default.numFormatInt);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Phụ cấp tăng cường", "HSPCTangCuong", "%", numberFormat: Settings.Default.numFormatInt);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Phụ cấp tăng cường ca 3", "HSPCTangCuong_Dem", "%", numberFormat: Settings.Default.numFormatInt);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Phụ cấp làm việc ngày nghỉ", "HSPC200", "%", numberFormat: Settings.Default.numFormatInt);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Phụ cấp làm việc ca 3 ngày nghỉ", "HSPC260", "%", numberFormat: Settings.Default.numFormatInt);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Phụ cấp làm việc ngày lễ, tết", "HSPC300", "%", numberFormat: Settings.Default.numFormatInt);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Phụ cấp trực lễ ca 3", "HSPC390", "%", numberFormat: Settings.Default.numFormatInt);
			func1(ws, ref ir, ref ic, ref left, tableThongSo, "Tiền lương 1 hệ số sản phẩm", "TienLuong1HeSoSP", "đồng", numberFormat: Settings.Default.numFormatFloat30);
		}


		public static void func1(ExcelWorksheet ws, ref int ir, ref int ic, ref int left, DataTable tableThongSo, string mota, string fieldname, string donvitinh = "", string numberFormat = null) {
			XL.FormatCell_W(ws, ref ir, ref ic, mota, plusCol: 1);
			ws.Column(ic).AutoFit();
			XL.FormatCell_N(ws, ref ir, ref ic, tableThongSo.Rows[0][fieldname], plusCol: 1, numFormat: numberFormat);
			ws.Column(ic).AutoFit();
			XL.FormatCell_W(ws, ref ir, ref ic, donvitinh, plusRow: 1);
			ws.Column(ic).AutoFit();

			ic = left;
		}

		public static void ExportSheetBangLuongCongNhat(ExcelWorksheet ws, DateTime m_thang, DataTable tableLuongCongNhat) {
			ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet
			ws.Workbook.CalcMode = ExcelCalcMode.Automatic;

			int stt = 1, top = 1, left = 1, bottom = 1, ir, ic;
			bottom = top;

			#region // 1. ghi header

			string headerString = string.Format(Settings.Default.titleBangLuongCongNhat, m_thang.ToString("MM/yyyy"));
			int left1 = 3, left2 = 7, leftT = 5;
			ir = top;
			EXP_Header(ws, ref ir, left1, left2, leftT, size1: 12, size2: 12, sizeT: 14,
					   ngayLap: DateTime.Today, headerString: headerString);

			#endregion
			// sau khi ghi header thì ir là vị trí row đầu tiên ghi title

			#region // 2. ghi colTitle
			ic = left;
			XL.FormatCell_T(ws, ref ir, ref ic, colWidth: (int)X.STT, value: "STT", plusCol: 1);//col 1
			XL.FormatCell_T(ws, ref ir, ref ic, colWidth: (int)X.TEN, value: "Họ tên", plusCol: 1);
			XL.FormatCell_T(ws, ref ir, ref ic, colWidth: (int)X.CHUCVU, value: "Chức vụ", plusCol: 1);
			XL.FormatCell_T(ws, ref ir, ref ic, colWidth: (int)X.DONGIALUONG, value: "Đơn giá lương", plusCol: 1);
			XL.FormatCell_T(ws, ref ir, ref ic, colWidth: (int)X.NGAYCONG, value: "Số ngày công", plusCol: 1);
			XL.FormatCell_T(ws, ref ir, ref ic, colWidth: (int)X.THANHTIEN, value: "Thành tiền", plusCol: 1);
			XL.FormatCell_T(ws, ref ir, ref ic, colWidth: (int)X.TAMUNG, value: "Khấu trừ tạm ứng lương tháng " + m_thang.ToString("M/yyyy"), plusCol: 1);
			XL.FormatCell_T(ws, ref ir, ref ic, colWidth: (int)X.THUCLANH, value: "Thực lãnh", plusCol: 1);
			XL.FormatCell_T(ws, ref ir, ref ic, colWidth: (int)X.KYNHAN, value: "Ký nhận", plusCol: 1);
			ir++;
			#endregion

			#region //3. ghi noi dung

			var tongThucLanh = 0d;
			foreach (DataRow row in tableLuongCongNhat.Rows) {
				ic = left;
				FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: stt);
				FormatCell_W(ws, ref ir, ref ic, plusCol: 1, value: row["UserFullName"].ToString());
				FormatCell_W(ws, ref ir, ref ic, plusCol: 1, value: row["ChucVu"].ToString());
				FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: (int)row["DonGiaLuong"], numFormat: Settings.Default.numFormatInt);
				FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Convert.ToSingle(row["SoNgayCong"]), numFormat: Settings.Default.numFormatFloat101F);
				var thanhtien = Convert.ToDouble((int)row["DonGiaLuong"] * Convert.ToSingle(row["SoNgayCong"]));
				FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: thanhtien, numFormat: Settings.Default.numFormatMoney);
				FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Convert.ToDouble(row["TamUng"]), numFormat: Settings.Default.numFormatMoney);
				var thuclanh = thanhtien - Convert.ToDouble(row["TamUng"]);
				tongThucLanh += thuclanh;
				FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: thuclanh, numFormat: Settings.Default.numFormatMoney);
				FormatCell(ws, ref ir, ref ic, plusCol: 1, plusRow: 1); //ký nhận
				stt++;
			}
			bottom = ir;

			ir = bottom;
			ic = left;
			var fromRow = top + 2;// bỏ qua 1 dòng tiêu đề, 1 dòng tên cột
			var toRow = bottom - 1;
			FormatCell_W(ws, ref ir, ref ic, plusCol: 1);
			FormatCell_W(ws, ref ir, ref ic, plusCol: 1);
			FormatCell_W(ws, ref ir, ref ic, plusCol: 1, value: "CỘNG", Bold: true);
			FormatCell_W(ws, ref ir, ref ic, plusCol: 1);
			func3(ws, ref ir, ref ic, fromRow, toRow, numFormat: Settings.Default.numFormatFloat101F); //songaycong
			func3(ws, ref ir, ref ic, fromRow, toRow, numFormat: Settings.Default.numFormatMoney); //thanhtien
			func3(ws, ref ir, ref ic, fromRow, toRow, numFormat: Settings.Default.numFormatMoney); //tamung
			func3(ws, ref ir, ref ic, fromRow, toRow, numFormat: Settings.Default.numFormatMoney); //thuclanh
			FormatCell_W(ws, ref ir, ref ic, plusCol: 1);//ký nhận

			#endregion

			#region //4. ghi footer

			ir++;
			FormatCell_W(ws, ir, 5, value: "Số tiền bằng chữ: " + MyUtility.So_chu(tongThucLanh), VeBorder: false, Bold: true, hAlign: ExcelHorizontalAlignment.Center);

			ir = ir + 2;
			ic = left;
			EXP_Footer(ws, ref ir,
					   new int[] { 2, 5, 9 },
					   new int[] { 13, 13, 13 },
					   new string[] { "GIÁM ĐỐC", "KTT", "LẬP BIỂU" },
					   new string[] { Settings.Default.LastTenGD, Settings.Default.LastTenKTT, Settings.Default.LastTenNVLapBieuLuong });

			#endregion


		}

		public static void ExportSheetBangLuong(ExcelWorksheet ws, DateTime m_thang, List<cUserInfo> dsnv, string tenNVLapBieu) {
			ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet
			ws.Workbook.CalcMode = ExcelCalcMode.Automatic;

			int ir = 1, ic = 1, stt = 1, left = 1, top = 1;

			#region // 1. ghi header

			string headerString = string.Format(Settings.Default.titleBangLuongThang, m_thang.ToString("MM/yyyy"));
			int left1 = 6, left2 = 30, leftT = 20;
			ir = top;
			EXP_Header(ws, ref ir, left1, left2, leftT, size1: 22, size2: 22, sizeT: 22,
					   ngayLap: DateTime.Today, headerString: headerString);

			#endregion

			#region // 2. ghi colTitle

			int top2 = ir;// giữ lại row bắt đầu ghi Title
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.STT, value: "STT", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);//col 1
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.MANV, value: "Mã NV", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.HOTEN, value: "Họ tên", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);

			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: null, value: "Hệ số lương", fromRow: ir, fromCol: ic, toRow: ir, toCol: ic + 1);//write HSLương ở trên -- ko tinh col
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.HSLUONGCB, value: "CB", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.HSLUONGSP, value: "SP", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);//col5

			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: null, value: "Ngày công chuẩn", fromRow: ir, fromCol: ic, toRow: ir, toCol: ic + 6);//write ngày công chuẩn ở trên-- ko tinh col
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.BANNGAY, value: "Công", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.PHEP, value: "Phép", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.HOCHOPLE, value: "Học, họp, CT, PT, lễ", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.PTDOANTHE, value: "PTĐT", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);//DANGLAM
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.VIECRIENG, value: "Việc riêng", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);//DANGLAM NEW THÊM VIỆC RIÊNG
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.QUADEM, value: "Qua đêm", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.CV, value: "Chờ việc", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);//col 10

			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: null, value: "Phụ cấp", fromRow: ir, fromCol: ic, toRow: ir, toCol: ic + 6);//write làm thêm giờ ở trên-- ko tinh col
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.PC30, value: "30%", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.PC50, value: "50%", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.PCTCC3, value: "TCC3 100%", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.PC100, value: "LVNN 100%", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.PC160, value: "LVNN 150%", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);//col15
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.PC200, value: "Trực lễ 200%", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.PC290, value: "Trực lễ ca3 290%", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);

			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.TONGCONG, value: "Tổng công", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.TONGPC, value: "Tổng PC", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);

			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: null, value: "Tiền lương", fromRow: ir, fromCol: ic, toRow: ir, toCol: ic + 4/* ic + 3*/);//write tiền lương ở trên-- ko tinh col
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.LUONGCB, value: "Lương CB", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);//col20
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.LUONGSP, value: "Lương SP", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.LUONGSP, value: "Lương CV", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.DIEUCHINH, value: "Điều chỉnh lương tháng trước", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.TONGLUONG, value: "Tổng lương", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);

			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: null, value: "PC lương", fromRow: ir, fromCol: ic, toRow: ir, toCol: ic + 2);//write phu cap ở trên-- ko tinh col
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.PCLUONGCB, value: "PC theo Lương CB", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);//col25
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.PCLUONGSP, value: "PC theo Lương SP", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			//v4.0.0.7 boBDC3 XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.BOIDUONGCA3, value: "Bồi dưỡng ca 3", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.TONGPCLUONG, value: "Tổng PC", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);

			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.TONGLUONGPC, value: "Tổng lương và PC", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);

			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: null, value: "Khấu trừ", fromRow: ir, fromCol: ic, toRow: ir, toCol: ic + 2); // write khau tru o tren-- ko tinh col
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.TAMUNG, value: "Tạm ứng", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);//col 30
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.BHXH, value: "BHXH, YT, TN", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.THUCHIKHAC, value: "Thu chi khác", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);

			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.TIENCOMTRUA, value: "Tiền cơm trưa", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.THUCLANH, value: "Thực lãnh", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);//col 34
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)L.KYNHAN, value: "Ký nhận", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			ws.Row(ir + 1).Height = 55d;


			ir = ir + 2;// tăng 2 dòng để bắt đầu ghi
			ic = left;
			#endregion -----------------------------------------------------------------------

			#region // 3. ghi noi dung

			int top3 = ir; // giữ lại dòng bắt đầu ghi nội dung
			SUMLUONG sumLuong = new SUMLUONG();
			var dsphong = (from nv in dsnv
						   group nv by nv.PhongBan into @group
						   orderby @group.Key.ViTri
						   select @group).ToList();

			foreach (var item in dsphong) {
				var phong = item.Key;
				List<cUserInfo> dsnv1 = dsnv.Where(o => o.PhongBan.ID == phong.ID).ToList();
				EXP_group_LuongNV(ws, ref stt, ref ir, ref ic, dsnv1, phong, ref sumLuong);
				ic = left;
				//ir++; //exp record cuối  cùng của group tự động tăng ir
			}


			#region // ghi dòng tổng cộng, FORMAT TỪ DÒNG GHI PHÒNG BAN ĐẾN DÒNG CUỐI NUMBERFORMAT

			int bottom3 = ir - 1; //top3 là dòng phòng ban, sum từ dòng kế tiếp; bottom -1 vì bottom = ir là index dòng kế
			var temp = 3;////ver 4.0.0.4	
			FormatCell_T_Merge(ws, ir, temp, value: "Tổng cộng: ", wrapText: false, fromRow: ir, fromCol: left, toRow: ir, toCol: 3, hAlign: ExcelHorizontalAlignment.Right); //ver 4.0.0.4	
			int ic5 = 4;// bat dau ghi tong cong tu cot 4 hslcb

			#region ghi dòng sum cuối cùng
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2"); // vì top là dòng phòng ban, sum từ dòng kế dòng phòng ban//                        "CB"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "SP"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Công"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Phép"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Học, họp, CT, PT, lễ"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "PTĐT"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Việc riêng"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Qua đêm"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Chờ việc"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "30%"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "50%"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "TCC3 100%"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "LVNN 100%"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "LVNN 150%"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Trực lễ 200%"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Trực lễ ca3 290%"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Tổng công"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Tổng PC"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Lương CB"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Lương SP"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Lương CV"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Điều chỉnh lương tháng trước"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Tổng lương"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "PC theo Lương CB"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "PC theo Lương SP"
			//v4.0.0.7 boBDC3 FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Bồi dưỡng ca 3"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Tổng PC"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Tổng lương và PC"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Tạm ứng"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "10,5% BHXH, YT, TN"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Thu chi khác"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Tiền cơm trưa"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[top3, ic5, bottom3, ic5].Address + ")/2");// "Thực lãnh"
			FormatCell_W(ws, ref ir, ref ic5, plusCol: 1); //ký nhận// đáng lẽ sau dòng SUM này thì tăng ir để trỏ sang dòng mới// "Ký nhận"

			#endregion

			#region chỉnh format number từng cột

			ic5 = 4;// bat dau ghi tong cong tu cot 4 hslcb
			//ws.Cells[t1, ic5, bottom, ic5].Style.Numberformat.Format = Settings.Default.numFormatFloat101F; // vì top là dòng phòng ban, sum từ dòng kế dòng phòng ban
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat20); //hslcb// t1, ic5, bottom + 1 - 1 vì t1, ic5, bottom3 + 1 là do format luôn dòng tổng cộng
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat20); //col 5 hslcv
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); //cong
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat10); //phep
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat10); //hoc
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat10); //ptdt //DANGLAM
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat10); //viecrieng //DANGLAM
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatInt); //qua dem
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); //col10 cho viec
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); //col15
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);// tong cong
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); //tong pc
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney); //col20 luong cb
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney);
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney);
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney);
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney);
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney);//col25
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney); 
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney);
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney);
			//v4.0.0.7 boBDC3 XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney);
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney);
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney); //col30
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney);
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney);
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney);
			//FormatCell_W(ws, ref ir, ref ic5, plusCol: 1); //ký nhận

			#endregion


			#endregion

			#region // set colWidth = 0 các cột ko cần thiết

			if (Math.Abs(sumLuong.pc100_16 - 0f) < 0.01f) ws.Column(16).Width = 0;
			if (Math.Abs(sumLuong.pc160_17 - 0f) < 0.01f) ws.Column(17).Width = 0;
			if (Math.Abs(sumLuong.pc200_18 - 0f) < 0.01f) ws.Column(18).Width = 0;
			if (Math.Abs(sumLuong.pc290_19 - 0f) < 0.01f) ws.Column(19).Width = 0;
			if (Math.Abs(sumLuong.dieuchinh25 - 0d) < 0.01d) ws.Column(25).Width = 0;
			//v4.0.0.7 boBDC3 if (Math.Abs(sumLuong.thuchikhac34 - 0d) < 0.01d) ws.Column(34).Width = 0;
			if (Math.Abs(sumLuong.thuchikhac34 - 0d) < 0.01d) ws.Column(33).Width = 0;

			#endregion

			#endregion

			#region //4. ghi footer

			ir++;
			FormatCell_W(ws, ir, 20, value: "Số tiền bằng chữ: " + MyUtility.So_chu(sumLuong.thuclanh36), VeBorder: false, Bold: true, hAlign: ExcelHorizontalAlignment.Center);//ký nhận

			ir = ir + 2; // cách 2 dòng
			EXP_Footer(ws, ref ir,
					   new int[] { 6, 20, 28 },
					   new int[] { 22, 22, 22 },
					   new string[] { "GIÁM ĐỐC", "KTT", "LẬP BIỂU" },
					   new string[] { Settings.Default.LastTenGD, Settings.Default.LastTenKTT, tenNVLapBieu },
					   khoangCachTen_ChucVu:8);

			#endregion

		}

		public static void EXP_group_LuongNV(ExcelWorksheet ws, ref int stt, ref int top, ref int left, List<cUserInfo> dsnv, cPhongBan cPhongBan, ref SUMLUONG sumluong) {
			// giữ lại vị trí ô để tính tổng
			var ir = top;
			var ic = left;
			int bottom, right;

			ir = ir + 1; // bỏ 1 dòng ghi phòng ban
			foreach (var nv in dsnv.Where(o => o.PhongBan.ID == cPhongBan.ID)) {
				ic = left; //reset ve cot dau tien sau moi lan lap
				XL.EXP_record_LuongNVChinhThuc(ws, ref stt, ref ir, ref ic, nv, ref sumluong);

				// sau moi lan lap thi row tang len, stt tang len, col reset ve dau
				stt++;
			}
			bottom = ir; // index ->
			right = ic;
			//sau khi xuat luong theo phong thi xuat tong cong

			#region ghi phần sum

			ir = top; //top của group tức dòng phòng ban
			ic = left;
			FormatCell_W(ws, ref ir, ref ic, cPhongBan.Ten, Bold: true, plusCol: 1); //col 1 stt --> manv
			ic++; //manv-> tennv
			ic++; //tennv --> hslcb
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1); //hslcb// bottom - 1 vì bottom là index của dòng mới
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1); //col 5 hslcv
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1); //cong
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1); //phep
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1); //qua dem
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1); //col10
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1); //col15
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1); //col20 luong cb
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1); //col25
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			//v4.0.0.7 boBDC3 func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1); 
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);//col30
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1);
			FormatCell_W(ws, ref ir, ref ic); //ký nhận

			#endregion

			top = bottom; // cập nhật lại giá trị dòng tiếp theo

		}
		public static void func3(ExcelWorksheet ws, ref int ir, ref int ic, int fromRow, int toRow, string numFormat = null) {

			string congthuc = "SUM({0})";

			string addStartRow = ws.Cells[fromRow, ic, toRow, ic].Address; // bottom - 1 vì bottom là index của dòng mới
			congthuc = string.Format(congthuc, addStartRow);

			XL.FormatCell_N(ws, ref ir, ref ic, congthuc: congthuc, plusCol: 1, numFormat: numFormat);
		}

		public static void EXP_record_LuongNVChinhThuc(ExcelWorksheet ws, ref int stt, ref int ir, ref int ic, cUserInfo nv, ref SUMLUONG sumBKL) {
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: stt); //col 1
			XL.FormatCell_W(ws, ref  ir, ref ic, plusCol: 1, value: nv.MaNV);
			XL.FormatCell_W(ws, ref  ir, ref ic, plusCol: 1, value: nv.TenNV);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.HeSo.LuongCB);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.HeSo.LuongCV);//col 5
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.Cong);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.Phep);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.H_CT_PT + nv.ThongKeThang.Le);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PTDT);//DANGLAM
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.NghiRo);//DANGLAM
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.NgayQuaDem);//col10
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.CongCV);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._30_dem);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._50_TC);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._100_TCC3);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._100_LVNN_Ngay);//col15
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._150_LVNN_Dem);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._200_LeTet_Ngay);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._250_LeTet_Dem);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.Cong + nv.ThongKeThang.Phep + nv.ThongKeThang.H_CT_PT + nv.ThongKeThang.Le + nv.ThongKeThang.CongCV);//DANGLAM ko tính ro,bh và ptdt
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._TongPC);//col20
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.chiTietLuong.LCB_Theo.Cong_CDNghi);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.chiTietLuong.LSP_Theo.Cong_CDNghi);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.chiTietLuong.LCB_Theo.CongCV);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.chiTietLuong.LuongDieuChinh);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.chiTietLuong.TongLuong_KoTinhCacLoaiPhuCap);//col25
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.chiTietLuong.LCB_Theo.PhuCap);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.chiTietLuong.LSP_Theo.PhuCap);
			//v4.0.0.7 boBDC3 XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.chiTietLuong.BoiDuongQuaDem);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.chiTietLuong.TongPhuCapLuong);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.chiTietLuong.TongLuong);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.chiTietLuong.KhauTru.TamUng);//col30
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.chiTietLuong.KhauTru.BHXH);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.chiTietLuong.KhauTru.ThuChiKhac);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.chiTietLuong.TienComTrua);
			XL.FormatCell_N(ws, ref  ir, ref ic, plusCol: 1, value: nv.chiTietLuong.ThucLanh);//col 34
			XL.FormatCell_W(ws, ref  ir, ref ic, plusCol: 1, plusRow: 1);//ký nhận //COL35

			// phần sum để set colWidth
			sumBKL.ptdt9 += nv.ThongKeThang.PTDT;
			sumBKL.viecrieng10 += nv.ThongKeThang.NghiRo;
			sumBKL.pc100_16 += nv.ThongKeThang.PhuCaps._100_LVNN_Ngay;
			sumBKL.pc160_17 += nv.ThongKeThang.PhuCaps._150_LVNN_Dem;//col15
			sumBKL.pc200_18 += nv.ThongKeThang.PhuCaps._200_LeTet_Ngay;
			sumBKL.pc290_19 += nv.ThongKeThang.PhuCaps._250_LeTet_Dem;
			sumBKL.dieuchinh25 += nv.chiTietLuong.LuongDieuChinh;
			sumBKL.thuchikhac34 += nv.chiTietLuong.KhauTru.ThuChiKhac;
			sumBKL.thuclanh36 += nv.chiTietLuong.ThucLanh;
		}

		public static void ExportSheetTongHopChi(ExcelWorksheet ws, DateTime ngaydauthang, DataTable tableThongSo, double tongLuongCongnhat, double tongLuongDieuchinh) {
			ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

			int ic = 1, left = 2, ir = 1, top = 1;
			string temp = string.Empty;

			var perTrichQuyLuong = (int)tableThongSo.Rows[0]["TrichQuyLuong"];
			var sanLuong = (int)tableThongSo.Rows[0]["SanLuong"];
			var donGia = (int)tableThongSo.Rows[0]["DonGia"];
			var thanhtien = ((double)sanLuong * (double)donGia);
			var _80perThanhTien = (thanhtien * perTrichQuyLuong) / 100d;
			var sanLuongGiaCongNoiBo = (int)tableThongSo.Rows[0]["SanLuongGiaCongNoiBo"];
			var donGiaGiaCongNoiBo = (int)tableThongSo.Rows[0]["DonGiaGiaCongNoiBo"];
			var thanhtienGiaCongNoiBo = ((sanLuongGiaCongNoiBo) * (donGiaGiaCongNoiBo));
			var sanLuongGiaCongNgoai = (int)tableThongSo.Rows[0]["SanLuongGiaCongNgoai"];
			var donGiaGiaCongNgoai = (int)tableThongSo.Rows[0]["DonGiaGiaCongNgoai"];
			var thanhtienGiaCongNgoai = ((double)sanLuongGiaCongNgoai * (double)donGiaGiaCongNgoai);
			var quyluongThanhtoan = _80perThanhTien + thanhtienGiaCongNoiBo + thanhtienGiaCongNgoai;
			var quyluongChoviec = (double)tableThongSo.Rows[0]["QuyLuongCV"];
			var quyluongNghidinh = (double)tableThongSo.Rows[0]["QuyLuongNghiDinhCP"];
			var chiKhacTuQuyLuong = (double)tableThongSo.Rows[0]["ChiKhacTuQuyLuong"];
			var quyLuongTheoHeSoSanPham = (double)tableThongSo.Rows[0]["QuyLuongTheoHeSoSanPham"];
			var tienLuong1HeSoSP = (double)tableThongSo.Rows[0]["TienLuong1HeSoSP"];

			// 1. ghi header
			ir = top;
			EXP_Header(ws, ref ir, 4, 12, 8, ngayLap: DateTime.Today, headerString: string.Format(Settings.Default.titleBangTongHopChiThang, ngaydauthang.ToString("MM/yyyy")));

			ic = left;
			// 2. ghi colTitle ( sản lương đơn giá)
			//XL.FormatCell_T_Merge(ws, ir, ic, bold:);
			// 3. ghi noi dung
			XL.FormatCell_W(ws, ref ir, ref ic, plusRow: 1, value: "Căn cứ để tạm trích lương tháng: ", Bold: true, VeBorder: false);
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Quỹ lương sản phẩm trong tháng(100%): ", VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, plusCol: 1, value: sanLuong, VeBorder: false, numFormat: Settings.Default.numFormatInt);
			XL.FormatCell_W(ws, ref ir, ref ic, colWidth: 5, plusCol: 1, value: "x", VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, plusCol: 1, value: donGia, VeBorder: false, numFormat: Settings.Default.numFormatInt);
			XL.FormatCell_W(ws, ref ir, ref ic, colWidth: 5, plusCol: 1, value: "=", VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: thanhtien, VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Quỹ lương sản phẩm để thanh toán lương(" + perTrichQuyLuong + "%): ", Bold: true, VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: _80perThanhTien, VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Quỹ lương gia công nội bộ: ", VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, plusCol: 1, value: sanLuongGiaCongNoiBo, VeBorder: false, numFormat: Settings.Default.numFormatInt);
			XL.FormatCell_W(ws, ref ir, ref ic, colWidth: 5, plusCol: 1, value: "x", VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, plusCol: 1, value: donGiaGiaCongNoiBo, VeBorder: false, numFormat: Settings.Default.numFormatInt);
			XL.FormatCell_W(ws, ref ir, ref ic, colWidth: 5, plusCol: 1, value: "=", VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: thanhtienGiaCongNoiBo, VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Quỹ lương gia công bên ngoài: ", VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, plusCol: 1, value: sanLuongGiaCongNgoai, VeBorder: false, numFormat: Settings.Default.numFormatInt);
			XL.FormatCell_W(ws, ref ir, ref ic, colWidth: 5, plusCol: 1, value: "x", VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, plusCol: 1, value: donGiaGiaCongNgoai, VeBorder: false, numFormat: Settings.Default.numFormatInt);
			XL.FormatCell_W(ws, ref ir, ref ic, colWidth: 5, plusCol: 1, value: "=", VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: thanhtienGiaCongNgoai, VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Tổng quỹ lương thanh toán trong tháng: ", Bold: true, VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: quyluongThanhtoan, VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Quỹ lương chờ việc (tính vào quỹ lương dự phòng cuối năm): ", VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: quyluongChoviec, VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ir = ir + 1; // cách 1 dòng 
			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "QUỸ LƯƠNG TRẢ CHO NGƯỜI LAO ĐỘNG ", Bold: true, VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: quyluongThanhtoan, VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusRow: 1, value: "TRONG ĐÓ: ", VeBorder: false);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Quỹ lương theo nghị định 205/CP ", Bold: true, VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: quyluongNghidinh, VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Quỹ lương công nhật, thời vụ, khoán việc ", VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: tongLuongCongnhat, VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Chi khác từ quỹ lương ", VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: chiKhacTuQuyLuong, VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Điều chỉnh lương tháng trước và truy lãnh lương ", VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: tongLuongDieuchinh, VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Quỹ lương theo hệ số (sản phẩm) ", Bold: true, VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: quyLuongTheoHeSoSanPham, VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "---> Tiền lương 1 hệ số (sản phẩm) ", Bold: true, VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: tienLuong1HeSoSP, VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			//4. ghi footer
			ir = ir + 2;
			EXP_Footer(ws, ref ir,
	new int[] { 3, 10 },
	new int[] { 13, 13 },
	new string[] { "GIÁM ĐỐC", "LẬP BIỂU" },
	new string[] { Settings.Default.LastTenGD, Settings.Default.LastTenNVLapBieuLuong }, khoangCachTen_ChucVu:9);

		}

		public static void ExportSheetBangKetcongThang(ExcelWorksheet ws, DateTime ngaybd, DateTime ngaykt, List<cUserInfo> dsnv, string tenNVLapBieu, string tenTrgBP, params int[] p) {
			ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet
			ws.Workbook.CalcMode = ExcelCalcMode.Automatic;

			int stt = 1, left = 1, top = 1, ir, ic;

			// 1. ghi header
			string headerString = string.Format(Settings.Default.titleBangCC_THANG, ngaybd.ToString("MM/yyyy"));
			int left1 = Settings.Default.viTriOHeaderBKC_TCTCNSG, left2 = Settings.Default.viTriOHeaderBKC_DiaChi, leftT = Settings.Default.viTriOHeaderTitle;
			ir = top;
			EXP_Header(ws, ref ir, left1, left2, leftT, size1: Settings.Default.sizeHeaderBKC_TCTCNSG, size2: Settings.Default.sizeHeaderBKC_TCTCNSG, sizeT: Settings.Default.sizeTitleBKC,
				ngayLap: DateTime.Today, headerString: headerString);


			#region // 2. ghi colTitle
			ic = left;
			FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.STT, value: "STT", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1); //col 1
			FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.manv, value: "Mã NV", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.ten, value: "Họ tên", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			var songay = Convert.ToInt32((ngaykt - ngaybd).TotalDays) + 1; // 1->31: songay = 30; 1-> 30: songay = 29 ==> phải +1 phía sau Convert.ToInt32((ngaykt - ngaybd).TotalDays)
			FormatCell_T_Merge(ws, ref ir, ref ic, value: "Ngày", plusRow: 1, fromRow: ir, toRow: ir, fromCol: ic, toCol: ic + songay - 1); //vì ic + songay = vị trí ô kế tiếp  
			for (DateTime indexNgay = ngaybd; indexNgay <= ngaykt; indexNgay = indexNgay.AddDays(1d)) {
				FormatCell_T(ws, ref ir, ref ic, colWidth: (int)CC.congtrongngay, value: indexNgay, plusCol: 1, numFormat: "d");
			}
			ir = ir - 1; // vì "Ngày" đã làm tăng ir lên 1
			//ic mội lần lặp đều tăng nên ic hiện tại là vị trí của cột sau ngày cuối tháng
			int icTemp = ic; // giữ lại vị trí cột đầu để format merge
			int irtemp = ir + 1;
			FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.cong, value: "Công", plusCol: 1); //col 1
			FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.Le, value: "Lễ 100%", plusCol: 1);
			FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.Phep, value: "Phép CĐ", plusCol: 1);
			FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.cv, value: "CV", plusCol: 1);
			FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.bh, value: "BHXH", plusCol: 1);
			FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.hoc, value: "Học, họp, CT, PT", plusCol: 1);//DANGLAM PTDT CHUYỂN RA SAU RO
			// write ngày công chuẩn ở trên
			FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.cong, value: "Ngày công chuẩn", fromRow: ir, fromCol: icTemp, toRow: ir, toCol: ic - 1);

			icTemp = ic;
			FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.tongcong, value: "TC", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			//write số công chuẫn ở trên
			var socongchuan = DateTime.DaysInMonth(ngaybd.Year, ngaybd.Month) - DemSoNgayNghiChunhat(ngaybd, true, false);
			FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.tongcong, value: socongchuan, fromRow: ir, fromCol: icTemp, toRow: ir, toCol: ic - 1);

			icTemp = ic;
			irtemp = ir + 1;
			FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.ca130, value: string.Format("PC Ca3 {0}%", p[0]), plusCol: 1);
			FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.ca150, value: string.Format("PCTC {0}%", p[1]), plusCol: 1);
			FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.tcc3_100, value: string.Format("PCTC Ca 3 {0}%", p[2]), plusCol: 1);
			FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.LVNN, value: string.Format("PC LVNN {0}%", p[3]), plusCol: 1);
			FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.pcCa3LVNN, value: string.Format("PC LVNN Ca3 {0}%", p[4]), plusCol: 1);
			FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.pcle_tet, value: string.Format("PC Lễ, tết {0}%", p[5]), plusCol: 1);
			FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.pcCa3_le, value: string.Format("PC lễ, tết ca 3 {0}%", p[6]), plusCol: 1);
			FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.pckhac, value: "PC khác", plusCol: 1);
			//write phụ cấp ở trên
			FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.cong, value: "Phụ cấp", fromRow: ir, fromCol: icTemp, toRow: ir, toCol: ic - 1);

			FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.nghiRo, value: "Nghỉ Ro", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.nghiRo, value: "PT Đoàn thể", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);//DANGLAM
			ws.Row(ir + 1).Height = 50d;

			#endregion


			//3. ghi noi dung
			SUMCC sumCC = new SUMCC();
			ir = ir + 2;// 2 dòng title
			int irSum = ir;
			ic = left;

			var dsphong = (from nv in dsnv.Where(o => o.LoaiCN != LoaiCongNhat.NVCongNhat)
						   group nv by nv.PhongBan into @group
						   orderby @group.Key.ViTri
						   select @group).ToList();
			int sumCol_Pos = 0;
			foreach (var item in dsphong) {
				var phong = item.Key;
				List<cUserInfo> dsnv1 = dsnv.Where(o => o.PhongBan.ID == phong.ID).ToList();
				XL.EXP_group_KetcongThang(ws, ref stt, ref ir, ref ic, dsnv1, phong, true, ref sumCC, out sumCol_Pos);
				ic = left;
				//ir++;
			}
			// ghi dòng tổng cộng
			int t1 = irSum, bottom = ir - 1; //top + 1 vì top là dòng phòng ban, sum từ dòng kế tiếp; bottom -1 vì bottom = ir là index dòng kế
			//XL.FormatCell_W(ws, ir, 30, "Tổng cộng: ", VeBorder: false);
			FormatCell_T_Merge(ws, ir, ic, value: "Tổng cộng", fromRow: ir, fromCol: left, toRow: ir, toCol: sumCol_Pos - 1, wrapText: false, hAlign: ExcelHorizontalAlignment.Right);//ver 4.0.0.4	

			int ic5 = sumCol_Pos;
			// ghi phần tổng kết
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //Cong);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //Le);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //Phep);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //CongCV);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //BHXH);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //H_CT_PT);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //Cong + nv.ThongKeThang.Le + nv.ThongKeThang.Phep + nv.ThongKeThang.CongCV + nv.ThongKeThang.H_CT_PT);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //PhuCaps._30_dem);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //PhuCaps._50_TC);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //PhuCaps._100_TCC3);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //PhuCaps._100_LVNN_Ngay);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //PhuCaps._150_LVNN_Dem);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //PhuCaps._200_LeTet_Ngay);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //PhuCaps._250_LeTet_Dem);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //PhuCaps._Cus);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //NghiRo);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //PTDT);//DANGLAM

			//formatNumber
			ic5 = sumCol_Pos;
			//XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat20); //hslcb// t1, ic5, bottom + 1 - 1 vì t1, ic5, bottom + 1 là index của dòng mới
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//Cong);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat10);//Le);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat10);//Phep);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//CongCV);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat10);//BHXH);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat10);//H_CT_PT);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//Cong + nv.ThongKeThang.Le + nv.ThongKeThang.Phep + nv.ThongKeThang.CongCV + nv.ThongKeThang.H_CT_PT + nv.ThongKeThang.PTDT); //DANGLAM
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._30_dem);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._50_TC);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._100_TCC3);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._100_LVNN_Ngay);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._150_LVNN_Dem);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._200_LeTet_Ngay);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._250_LeTet_Dem);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._Cus);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat10);//NghiRo);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat10);//PTDT);//DANGLAM



			#region // set ColWidth = 0 các cột ko cần thiết

			if (Math.Abs(sumCC.le2 - 0f) < 0.01f) ws.Column(3 + songay + 2).Width = 0; // 3 cột stt manv tennv
			if (Math.Abs(sumCC.bhxh5 - 0f) < 0.01f) ws.Column(3 + songay + 5).Width = 0;
			if (Math.Abs(sumCC.pc100_11 - 0f) < 0.01f) ws.Column(3 + songay + 11).Width = 0;
			if (Math.Abs(sumCC.pc160_12 - 0f) < 0.01f) ws.Column(3 + songay + 12).Width = 0;
			if (Math.Abs(sumCC.pc200_13 - 0f) < 0.01f) ws.Column(3 + songay + 13).Width = 0;
			if (Math.Abs(sumCC.pc290_14 - 0f) < 0.01f) ws.Column(3 + songay + 14).Width = 0;
			if (Math.Abs(sumCC.pckhac_15 - 0f) < 0.01f) ws.Column(3 + songay + 15).Width = 0;
			if (Math.Abs(sumCC.nghiRO_16 - 0f) < 0.01f) ws.Column(3 + songay + 16).Width = 0;
			if (Math.Abs(sumCC.ptdt_17 - 0f) < 0.01f) ws.Column(3 + songay + 17).Width = 0;

			#endregion

			// ghi các phần công nhật
			ir++;
			XL.FormatCell_W(ws, ref ir, ref ic, value: "Nhân viên làm việc công nhật", plusRow: 1, Bold: true, VeBorder: false); // ghi dòng phòng ban 
			foreach (var nv in dsnv.Where(o => o.LoaiCN == LoaiCongNhat.NVCongNhat || o.LoaiCN == LoaiCongNhat.NVCongNhatVaChinhThuc)) {
				ic5 = left;
				EXP_record_KetcongThang(ws, ref stt, ref ir, ref ic5, nv, false, ref sumCC, out sumCol_Pos);
				stt++;
			}
			//func3(ws, ref ir, ref ic5);


			//4. ghi footer
			var tableShift = DAO.LayDSCa();
			var tableAbsent = DAO.LayDSLoaiVang();
			ir = ir + 2;// cách 2 dòng
			EXP_KyHieuCC(ws, ref ir, new int[] { 3, 4, 24, 41 }, out ir, tableShift, tableAbsent);

			ir = ir + 2;// cách 2 dòng
			EXP_Footer(ws, ref ir,
	new int[] { 4, 24, 41 },
	new int[] { 16, 16, 16 },
	new string[] { "LẬP BIỂU", "TRƯỞNG BỘ PHẬN", "PHÓ GIÁM ĐỐC" },
	new string[] { tenNVLapBieu, tenTrgBP, Settings.Default.LastTenPhoGD }, khoangCachTen_ChucVu: Settings.Default.cachDongBKC_LapBieu_ten);

		}
		public static void EXP_group_KetcongThang(ExcelWorksheet ws, ref int stt, ref int top, ref int left, List<cUserInfo> dsnv, cPhongBan phong, bool XuatPhanChinhThuc, ref SUMCC sumCC, out int sumCol_Pos) {
			int ir = top, ic = left;
			sumCol_Pos = 0;
			XL.FormatCell_W(ws, ref ir, ref ic, value: phong.Ten, plusRow: 1, Bold: true, VeBorder: false); // ghi dòng phòng ban 
			if (dsnv.Count == 0) return; //v9 neu 1 phong ko co nhan vien thi ko ghi sum thi dong tong cong cuoi thi sao?
			foreach (cUserInfo nv in dsnv) {
				ic = left;//reset về cột đầu tiên  dòng tiếp theo
				EXP_record_KetcongThang(ws, ref stt, ref ir, ref ic, nv, XuatPhanChinhThuc, ref sumCC, out sumCol_Pos);
				stt++;
			}

			int t1 = top + 1, bottom = ir - 1; //top + 1 vì top là dòng phòng ban, sum từ dòng kế tiếp; bottom -1 vì bottom = ir là index dòng kế
			// ghi phần sum tổng kết
			int irSum = top; // top group
			ic = sumCol_Pos;
			func3(ws, ref irSum, ref ic, t1, bottom); //Cong);
			func3(ws, ref irSum, ref ic, t1, bottom); //Le);
			func3(ws, ref irSum, ref ic, t1, bottom); //Phep);
			func3(ws, ref irSum, ref ic, t1, bottom); //CongCV);
			func3(ws, ref irSum, ref ic, t1, bottom); //BHXH);
			func3(ws, ref irSum, ref ic, t1, bottom); //H_CT_PT);
			func3(ws, ref irSum, ref ic, t1, bottom); //Cong + nv.ThongKeThang.Le + nv.ThongKeThang.Phep + nv.ThongKeThang.CongCV + nv.ThongKeThang.H_CT_PT + nv.ThongKeThang.PTDT);//DANGLAM
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._30_dem);
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._50_TC);
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._100_TCC3);
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._100_LVNN_Ngay);
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._150_LVNN_Dem);
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._200_LeTet_Ngay);
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._250_LeTet_Dem);
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._Cus);
			func3(ws, ref irSum, ref ic, t1, bottom); //Nghiro);
			func3(ws, ref irSum, ref ic, t1, bottom); //PTDT);//DANGLAM

			top = ir;// xuất xong 1 group rồi thì cập nhật lại top là con trỏ dòng kế tiếp
		}
		public static void EXP_record_KetcongThang(ExcelWorksheet ws, ref int stt, ref int top, ref int left, cUserInfo nv, bool XuatPhanKetCongChinhThuc, ref SUMCC sumCC, out int sumCol_Position) {
			int ir = top, ic = left;
			XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: stt);
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, value: nv.MaNV);
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, value: nv.TenNV);

			#region //for testing

			/*
			cNgayCong ngayCong = new cNgayCong()
				{
					TongCong = 0.98f,
					PhuCaps = new PhuCap() {_TongPC = 0.3f},
					DSVang = new List<cLoaiVang>()
						{
							new cLoaiVang() {KyHieu = "P", WorkingDay = 0.5f}
						}
				};
			nv.DSNgayCong.AddRange(new cNgayCong[] {ngayCong, ngayCong, ngayCong, ngayCong, ngayCong});
*/

			#endregion

			// ghi ký hiệu từng ngày công
			foreach (cNgayCong ngay in nv.DSNgayCong) {
				var temp = string.Empty;
				if (XuatPhanKetCongChinhThuc) {
					if (nv.LoaiCN == LoaiCongNhat.NVChinhThuc) {
						ngay.XuatChuoiKyHieuChamCong(ref temp);
						XL.FormatCell_W(ws, ir, ic, value: temp, wrapText: true);
					}
					else if (nv.LoaiCN == LoaiCongNhat.NVCongNhatVaChinhThuc) {
						if (ngay.Ngay < nv.NgayBDCongnhat || ngay.Ngay > nv.NgayKTCongnhat) { //xuất phần chính thức
							ngay.XuatChuoiKyHieuChamCong(ref temp);
							XL.FormatCell_W(ws, ir, ic, value: temp, wrapText: true);
						}
						else XL.FormatCell_W(ws, ir, ic, value: string.Empty, wrapText: true); // phần công nhật xuất 0
					}
					else XL.FormatCell_W(ws, ir, ic, value: string.Empty, wrapText: true); // phần công nhật xuất 0
				}
				else {
					if (nv.LoaiCN == LoaiCongNhat.NVCongNhat) {
						ngay.XuatChuoiKyHieuChamCong(ref temp);
						XL.FormatCell_W(ws, ir, ic, value: temp, wrapText: true);
					}
					else if (nv.LoaiCN == LoaiCongNhat.NVCongNhatVaChinhThuc) {
						if (ngay.Ngay >= nv.NgayBDCongnhat && ngay.Ngay <= nv.NgayKTCongnhat) {
							ngay.XuatChuoiKyHieuChamCong(ref temp);
							XL.FormatCell_W(ws, ir, ic, value: temp, wrapText: true);
						}
						else XL.FormatCell_W(ws, ir, ic, value: string.Empty, wrapText: true); // phần công nhật xuất 0
					}
					else XL.FormatCell_W(ws, ir, ic, value: string.Empty, wrapText: true); // phần công nhật xuất 0
				}
				ic++;// cập nhật lại vị trí cho col kế tiếp
			}
			// ghi phần thống kê tháng các cột cuối
			sumCol_Position = ic;
			if (XuatPhanKetCongChinhThuc) {
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.Cong);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.Le);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.Phep);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.CongCV);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.BHXH);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.H_CT_PT);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.Cong + nv.ThongKeThang.Le + nv.ThongKeThang.Phep + nv.ThongKeThang.CongCV + nv.ThongKeThang.H_CT_PT + nv.ThongKeThang.PTDT);//DANGLAM
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._30_dem);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._50_TC);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._100_TCC3);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._100_LVNN_Ngay);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._150_LVNN_Dem);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._200_LeTet_Ngay);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._250_LeTet_Dem);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._Cus);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.NghiRo);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PTDT);//DANGLAM
			}
			else {
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.Cong_Congnhat); //nv.ThongKeThang.Cong);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.Le);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.Phep);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.CongCV);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.BHXH);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.H_CT_PT);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.Cong + nv.ThongKeThang.Le + nv.ThongKeThang.Phep + nv.ThongKeThang.CongCV + nv.ThongKeThang.H_CT_PT + nv.ThongKeThang.PTDT);//DANGLAM
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._30_dem);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._50_TC);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._100_TCC3);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._100_LVNN_Ngay);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._150_LVNN_Dem);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._200_LeTet_Ngay);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._250_LeTet_Dem);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._Cus);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.NghiRo);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PTDT);//DANGLAM

			}
			//sum để set colWidth
			sumCC.le2 += nv.ThongKeThang.Le;
			sumCC.bhxh5 += nv.ThongKeThang.BHXH;
			sumCC.pc100_11 += nv.ThongKeThang.PhuCaps._100_LVNN_Ngay;
			sumCC.pc160_12 += nv.ThongKeThang.PhuCaps._150_LVNN_Dem;
			sumCC.pc200_13 += nv.ThongKeThang.PhuCaps._200_LeTet_Ngay;
			sumCC.pc290_14 += nv.ThongKeThang.PhuCaps._250_LeTet_Dem;
			sumCC.pckhac_15 += nv.ThongKeThang.PhuCaps._Cus;
			sumCC.nghiRO_16 += nv.ThongKeThang.NghiRo;
			sumCC.ptdt_17 += nv.ThongKeThang.PTDT;//DANGLAM

			ir++;
			top = ir; // cập nhật lại index cho row kế tiếp
		}


		public static void ExportSheetBangThongKeSua(ExcelWorksheet ws, DateTime ngaybd, DateTime ngaykt, List<cUserInfo> dsnv) {
			ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet
			ws.Workbook.CalcMode = ExcelCalcMode.Automatic;

			int stt = 1, left = 1, top = 1, ir, ic;
			ir = top;
			ic = left;
			FormatCell_T_Merge(ws, ref ir, ref ic, value: "BẢNG THỐNG KÊ CHI CHẾ ĐỘ BỒI DƯỠNG ĐỘC HẠI BẰNG HIỆN VẬT", wrapText: false, size: 16, bold: true, VeBorder: false, merge: false, plusRow: 1, hAlign: ExcelHorizontalAlignment.Left);

			#region // 2. ghi colTitle
			ic = left;
			FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.STT, value: "STT", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1); //col 1
			FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.manv, value: "Mã NV", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.ten, value: "Họ tên", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			var songay = Convert.ToInt32((ngaykt - ngaybd).TotalDays) + 1; // 1->31: songay = 30; 1-> 30: songay = 29 ==> phải +1 phía sau Convert.ToInt32((ngaykt - ngaybd).TotalDays)
			FormatCell_T_Merge(ws, ref ir, ref ic, value: "Thời gian làm việc theo Ngày", plusRow: 1, fromRow: ir, toRow: ir, fromCol: ic, toCol: ic + songay - 1); //vì ic + songay = vị trí ô kế tiếp  
			for (DateTime indexNgay = ngaybd; indexNgay <= ngaykt; indexNgay = indexNgay.AddDays(1d)) {
				FormatCell_T(ws, ref ir, ref ic, colWidth: 0, value: indexNgay, plusCol: 1, numFormat: "d");
			}
			ir = ir - 1;
			FormatCell_T_Merge(ws, ref ir, ref ic, value: "Ngày", plusRow: 1, fromRow: ir, toRow: ir, fromCol: ic, toCol: ic + songay - 1); //vì ic + songay = vị trí ô kế tiếp  
			for (DateTime indexNgay = ngaybd; indexNgay <= ngaykt; indexNgay = indexNgay.AddDays(1d)) {
				FormatCell_T(ws, ref ir, ref ic, colWidth: (int)CC.congtrongngay, value: indexNgay, plusCol: 1, numFormat: "d");
			}
			ir = ir - 1; // vì "Ngày" đã làm tăng ir lên 1
			//ic mội lần lặp đều tăng nên ic hiện tại là vị trí của cột sau ngày cuối tháng

			#endregion


			//3. ghi noi dung
			SUMCC sumCC = new SUMCC();
			ir = ir + 2;// 2 dòng title
			int irSum = ir;
			ic = left;

			var dsphong = (from nv in dsnv.Where(o => o.LoaiCN != LoaiCongNhat.NVCongNhat)
						   group nv by nv.PhongBan into @group
						   orderby @group.Key.ViTri
						   select @group).ToList();
			int sumCol_Pos = 0;
			foreach (var item in dsphong) {
				var phong = item.Key;
				List<cUserInfo> dsnv1 = dsnv.Where(o => o.PhongBan.ID == phong.ID).ToList();
				XL.EXP_group_ThongKeSua(ws, ref stt, ref ir, ref ic, dsnv1, phong, songay, ref sumCC, out sumCol_Pos);
				ic = left;
				//ir++;
			}
			// ghi dòng tổng cộng
			int t1 = irSum, bottom = ir - 1; //top + 1 vì top là dòng phòng ban, sum từ dòng kế tiếp; bottom -1 vì bottom = ir là index dòng kế
			XL.FormatCell_W(ws, ir, 3, "Tổng cộng: ", VeBorder: false);
			int ic5 = sumCol_Pos;
			// ghi phần tổng kết
			for (int index = 0; index < songay; index++) {
				int ic5_2 = ic5 + songay;
				FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUM(" + ws.Cells[t1, ic5, bottom, ic5].Address + ")/2"); //Cong);
				FormatCell_N(ws, ref ir, ref ic5_2, /*plusCol: 1, */congthuc: "SUM(" + ws.Cells[t1, ic5_2, bottom, ic5_2].Address + ")/2"); //ko can plusCol vì ic5 đã plusCol rồi
			}

			//formatNumber
			ic5 = sumCol_Pos;
			for (int index = 0; index < songay; index++) {
				int ic5_2 = ic5 + songay;
				XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//thời gian
				XL.FormatNumber(ws, ref t1, ref ic5_2, t1, ic5_2, bottom + 1, ic5_2, /*plusCol: 1, */numberFormat: Settings.Default.numFormatInt);//kết sữa
			}






		}
		public static void EXP_group_ThongKeSua(ExcelWorksheet ws, ref int stt, ref int top, ref int left, List<cUserInfo> dsnv, cPhongBan phong, int songay, ref SUMCC sumCC, out int sumCol_Pos) {
			int ir = top, ic = left;
			sumCol_Pos = 0;
			XL.FormatCell_W(ws, ref ir, ref ic, value: phong.Ten, plusRow: 1, Bold: true, VeBorder: false); // ghi dòng phòng ban 
			if (dsnv.Count == 0) return; //v9 neu 1 phong ko co nhan vien thi ko ghi sum thi dong tong cong cuoi thi sao?
			foreach (cUserInfo nv in dsnv) {
				ic = left;//reset về cột đầu tiên  dòng tiếp theo
				EXP_record_ThongKeSua(ws, ref stt, ref ir, ref ic, nv, songay, ref sumCC, out sumCol_Pos);
				stt++;
			}

			int t1 = top + 1, bottom = ir - 1; //top + 1 vì top là dòng phòng ban, sum từ dòng kế tiếp; bottom -1 vì bottom = ir là index dòng kế
			// ghi phần sum tổng kết
			int irSum = top; // top group
			ic = sumCol_Pos;
			for (int index = 0; index < songay; index++) {// xuất các sum theo phòng ban phần thời gian làm việc
				func3(ws, ref irSum, ref ic, t1, bottom);
			}
			for (int index = 0; index < songay; index++) {// xuất các sum theo phòng ban phần kết chi BDĐH
				func3(ws, ref irSum, ref ic, t1, bottom);
			}

			top = ir;// xuất xong 1 group rồi thì cập nhật lại top là con trỏ dòng kế tiếp
		}
		public static void EXP_record_ThongKeSua(ExcelWorksheet ws, ref int stt, ref int top, ref int left, cUserInfo nv, int songay, ref SUMCC sumCC, out int sumCol_Position) {
			int ir = top, ic = left;
			XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: stt);
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, value: nv.MaNV);
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, value: nv.TenNV);

			sumCol_Position = ic;
			// ghi phan thời gian bị ẩn
			foreach (cNgayCong ngay in nv.DSNgayCong) {
				int colGio = ic;
				var temp = ws.Cells[ir, colGio, ir, colGio].FullAddress;
				if (nv.NgayBDCongnhat != DateTime.MinValue && ngay.Ngay >= nv.NgayBDCongnhat && ngay.Ngay <= nv.NgayKTCongnhat)
					XL.FormatCell_N(ws, ir, ic);
				else
					XL.FormatCell_N(ws, ir, ic, value: ngay.TG.GioLamViec.TotalHours);//info ko bỏ totalHours vì phải là số mới thực hiện hàm if được
				XL.FormatCell_N(ws, ir, ic + songay, congthuc: string.Format("IF({0}<2,0,IF({0}<10,1,IF({0}<18,2,3)))", temp));//=IF(O14<2,0,IF(O14<10,1,IF(O14<18,2,3)))
				ic++;// cập nhật lại vị trí cho col kế tiếp
			}
			// ghi phần thống kê tháng các cột cuối
			ir++;
			top = ir; // cập nhật lại index cho row kế tiếp
		}


		public static void ExportSheetBangChiTietKetCong(ExcelWorksheet ws, DateTime ngaybd, DateTime ngaykt, List<cUserInfo> dsnv, params int[] p) {
			ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

			int stt = 1, left = 1, top = 1, ir, ic;

			// 1. ghi header
			string headerString = string.Format(Settings.Default.titleBangCC_THANG, ngaybd.ToString("MM/yyyy"));
			int left1 = 6, left2 = 39, leftT = 24;
			ir = top;
			EXP_Header(ws, ref ir, left1, left2, leftT, size1: 24, size2: 24, sizeT: 24,
				logoName: "logo", ngayLap: DateTime.Today, headerString: headerString);

			// 2. ghi colTitle
			ic = left;
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.STT, value: "STT", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);//col 1
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.manv, value: "Mã NV", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.ten, value: "Họ tên", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.STT, fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			var chisoCotNgay1 = ic;
			var songay = Convert.ToInt32((ngaykt - ngaybd).TotalDays) + 1;
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, value: "Ngày", plusRow: 1, fromRow: ir, toRow: ir, fromCol: ic, toCol: ic + songay - 1);
			for (DateTime indexNgay = ngaybd; indexNgay <= ngaykt; indexNgay = indexNgay.AddDays(1d)) {
				XL.FormatCell_T(ws, ref ir, ref ic, colWidth: (int)CC.congtrongngay, value: indexNgay, plusCol: 1, numFormat: "d");
			}
			ir = ir - 1; // vì "Ngày" đã plusRow làm tăng ir lên 1
			//ic mội lần lặp đều tăng nên ic hiện tại là vị trí của cột sau ngày cuối tháng
			int icTemp = ic; // giữ lại vị trí cột đầu để format merge
			int irtemp = ir + 1;
			XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.tongcong, value: "Công", plusCol: 1);//col 1
			XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.Le, value: "Lễ 100%", plusCol: 1);
			XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.Phep, value: "Phép CĐ", plusCol: 1);
			XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.cv, value: "CV", plusCol: 1);
			XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.bh, value: "BHXH", plusCol: 1);
			XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.hoc, value: "Học, họp, PT", plusCol: 1);//DANGLAM PTDT CHUYỂN XUỐNG SAU RO
			// write ngày công chuẩn ở trên
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.tongcong, value: "Ngày công chuẩn", fromRow: ir, fromCol: icTemp, toRow: ir, toCol: ic - 1);

			icTemp = ic;
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.tongcong, value: "TC", fromRow: ir + 1, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			//write số công chuẫn ở trên
			var socongchuan = DateTime.DaysInMonth(ngaybd.Year, ngaybd.Month) - XL.DemSoNgayNghiChunhat(ngaybd, true, false);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.cong, value: socongchuan, fromRow: ir, fromCol: icTemp, toRow: ir, toCol: ic - 1);

			icTemp = ic;
			irtemp = ir + 1;
			XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.ca130, value: string.Format("PC Ca3 {0}%", p[0]), plusCol: 1);
			XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.ca150, value: string.Format("PCTC {0}%", p[1]), plusCol: 1);
			XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.tcc3_100, value: string.Format("PCTC Ca 3 {0}%", p[2]), plusCol: 1);
			XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.LVNN, value: string.Format("PC LVNN {0}%", p[3]), plusCol: 1);
			XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.pcCa3LVNN, value: string.Format("PC LVNN Ca3 {0}%", p[4]), plusCol: 1);
			XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.pcle_tet, value: string.Format("PC Lễ, tết {0}%", p[5]), plusCol: 1);
			XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.pcCa3_le, value: string.Format("PC lễ, tết ca 3 {0}%", p[6]), plusCol: 1);
			XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.pckhac, value: "PC khác", plusCol: 1);
			//write phụ cấp ở trên
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.cong, value: "Phụ cấp", fromRow: ir, fromCol: icTemp, toRow: ir, toCol: ic - 1);

			icTemp = ic;
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.nghiRo, value: "Nghỉ Ro", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, colWidth: (int)CC.ptdt, value: "PT Đoàn thể", fromRow: ir, fromCol: ic, toRow: ir + 1, toCol: ic, plusCol: 1);//DANGLAM

			ws.Row(ir + 1).Height = 50d;


			//3. ghi noi dung
			SUMCC sumCC = new SUMCC();
			ir = ir + 2;// 2 dòng title
			ic = left;

			var dsphong = (from nv in dsnv
						   group nv by nv.PhongBan into @group
						   orderby @group.Key.ViTri
						   select @group).ToList();
			foreach (var item in dsphong) {
				var phong = item.Key;
				List<cUserInfo> dsnv1 = dsnv.Where(o => o.PhongBan.ID == phong.ID).ToList();
				XL.EXP_group_ChitietCong_PC(ws, ref stt, ref ir, ref ic, dsnv1, phong, ref sumCC);
				ic = left;
				//ir++;
			}


			//set colWidth =0 các cột ko cần thiết

			if (Math.Abs(sumCC.le2 - 0f) < 0.01f) ws.Column(4 + songay + 2).Width = 0;
			if (Math.Abs(sumCC.bhxh5 - 0f) < 0.01f) ws.Column(4 + songay + 5).Width = 0;
			if (Math.Abs(sumCC.pc100_11 - 0f) < 0.01f) ws.Column(4 + songay + 11).Width = 0;
			if (Math.Abs(sumCC.pc160_12 - 0f) < 0.01f) ws.Column(4 + songay + 12).Width = 0;
			if (Math.Abs(sumCC.pc200_13 - 0f) < 0.01f) ws.Column(4 + songay + 13).Width = 0;
			if (Math.Abs(sumCC.pc290_14 - 0f) < 0.01f) ws.Column(4 + songay + 14).Width = 0;
			if (Math.Abs(sumCC.pckhac_15 - 0f) < 0.01f) ws.Column(4 + songay + 15).Width = 0;
			if (Math.Abs(sumCC.nghiRO_16 - 0f) < 0.01f) ws.Column(4 + songay + 16).Width = 0;
			if (Math.Abs(sumCC.ptdt_17 - 0f) < 0.01f) ws.Column(4 + songay + 17).Width = 0;//DANGLAM


			ws.Workbook.CalcMode = ExcelCalcMode.Automatic;

			//4. ghi footer
		}
		public static void EXP_group_ChitietCong_PC(ExcelWorksheet ws, ref int stt, ref int top, ref int left, List<cUserInfo> dsnv, cPhongBan phong, ref SUMCC sumCC) {
			int ir = top, ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, value: phong.Ten, plusRow: 1, Bold: true, VeBorder: false); // ghi dòng phòng ban
			foreach (cUserInfo nv in dsnv) {
				ic = left;//reset về cột đầu tiên  dòng tiếp theo
				EXP_record_ChitietCong_PC(ws, ref stt, ref ir, ref ic, nv, ref sumCC); // sau khi xuất xong 1 dòng, ir sẽ cập nhật là ir của dòng kế
				stt++;
			}
			top = ir;// xuất xong 1 group rồi thì cập nhật lại index cho dòng kế
		}
		public static void EXP_record_ChitietCong_PC(ExcelWorksheet ws, ref int stt, ref int top, ref int left, cUserInfo nv, ref SUMCC sumCC) {
			int ir = top, ic = left;
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, value: stt, fromRow: ir, fromCol: ic, toRow: ir + 2, toCol: ic, bold: false);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, value: nv.MaNV, fromRow: ir, fromCol: ic, toRow: ir + 2, toCol: ic, bold: false, hAlign: ExcelHorizontalAlignment.Left);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, value: nv.TenNV, fromRow: ir, fromCol: ic, toRow: ir + 2, toCol: ic, bold: false, hAlign: ExcelHorizontalAlignment.Left);
			XL.FormatCell_W(ws, ir + 0, ic, value: "Công");
			XL.FormatCell_W(ws, ir + 1, ic, value: "PC");
			XL.FormatCell_W(ws, ir + 2, ic, value: "KH");
			ic++;

			foreach (cNgayCong ngay in nv.DSNgayCong) {
				XL.FormatCell_N(ws, ir + 0, ic, value: ngay.TongCong_4008, numFormat: Settings.Default.numFormatFloat101F);
				XL.FormatCell_N(ws, ir + 1, ic, value: ngay.PhuCaps._TongPC, numFormat: Settings.Default.numFormatFloat101F);
				var temp = string.Empty;
				ngay.XuatChuoiKyHieuChamCong(ref temp);
				// xuất ký hiệu ở dòng dưới,  vì đã cộng rồi
				XL.FormatCell_W(ws, ir + 2, ic, value: temp, wrapText: true);

				ic++;// cập nhật lại vị trí cho col kế tiếp
			}

			// ghi phần tổng kết
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.Cong + nv.ThongKeThang.Cong_Congnhat);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.Le);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.Phep);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.CongCV);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.BHXH);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.H_CT_PT);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.Cong + nv.ThongKeThang.Le + nv.ThongKeThang.Phep + nv.ThongKeThang.CongCV + nv.ThongKeThang.H_CT_PT + nv.ThongKeThang.PTDT);//DANGLAM
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._30_dem);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._50_TC);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._100_TCC3);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._100_LVNN_Ngay);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._150_LVNN_Dem);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._200_LeTet_Ngay);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._250_LeTet_Dem);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._Cus);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.NghiRo);
			XL.FormatCell_T_Merge(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2, fromCol: ic, toCol: ic, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PTDT);//DANGLAM

			//sum để set colWidth
			sumCC.le2 += nv.ThongKeThang.Le;
			sumCC.bhxh5 += nv.ThongKeThang.BHXH;
			sumCC.pc100_11 += nv.ThongKeThang.PhuCaps._100_LVNN_Ngay;
			sumCC.pc160_12 += nv.ThongKeThang.PhuCaps._150_LVNN_Dem;
			sumCC.pc200_13 += nv.ThongKeThang.PhuCaps._200_LeTet_Ngay;
			sumCC.pc290_14 += nv.ThongKeThang.PhuCaps._250_LeTet_Dem;
			sumCC.pckhac_15 += nv.ThongKeThang.PhuCaps._Cus;
			sumCC.nghiRO_16 += nv.ThongKeThang.NghiRo;


			// xuất xong các cột trong 1 dòng, cập nhật lại index cho dòng kế
			top = ir + 3; //(vì xuất 3 cột)
		}


		public static void ExportSheetDiemDanh(ExcelWorksheet ws, List<cUserInfo> dsnv) {
			ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

			/*
						sR = sR + 7;// sR+1 là dòng trống
						XL.FormatCell(ws.Cells[sR, 6], "Danh sách nhân viên đang làm việc ngày " + dtpNgay.Value.ToString("dd/MM/yyyy"),
							size: 14, hAlign: ExcelHorizontalAlignment.CenterContinuous, bold: true, VeBorder: false);*/

			int stt = 0, left = 1, top = 1;
			int ir = top, ic = left;
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, plusRow: 1, value: "BẢNG ĐIỂM DANH NHÂN VIÊN TRONG NGÀY", wrapText: false, VeBorder: false, size: 16, hAlign: ExcelHorizontalAlignment.Left);//+0
			ic = left;
			int left1 = left;
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "STT");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Mã NV");//+1
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Họ tên");//+2
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Trạng thái");//+3
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Loại vắng");//+3
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Vào 1");//+5
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Ra 1");//+6
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Ca");//+4
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Vào 2");//+7
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Ra 2");//+8
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Ca");//+4
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Vào 3");//+9
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Ra 3");//+10						
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, plusRow: 1, value: "Ca");//+4
			ws.Cells[ir - 1, left1, ir - 1, ic - 1].AutoFilter = true;

			var dsphong = (from nv in dsnv
						   group nv by nv.PhongBan into @group
						   orderby @group.Key.ViTri
						   select @group).ToList();
			int SoNV_DangLV = 0, SoNV_DaVe = 0, SoNV_VangLyDo = 0, SoNV_VangNghi = 0;
			foreach (var item in dsphong) {
				var phong = item.Key;
				ic = left;
				List<cUserInfo> dsnv1 = dsnv.Where(o => o.PhongBan.ID == phong.ID).ToList();
				XL.EXP_group_DiemDanh(ws, ref stt, ref ir, ref ic, dsnv1, phong, ref SoNV_DangLV, ref SoNV_DaVe, ref SoNV_VangLyDo, ref SoNV_VangNghi);
				//ir++;
			}
			ic = left;
			int top2 = top + 2;// top +2 vì 1 dòng header title, 1dòng colTitle
			XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, VeBorder: false);//+0 
			XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, VeBorder: false);//+1
			XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, VeBorder: false, value: "Tổng cộng:");//+2
			XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, VeBorder: false, congthuc: string.Format("SUBTOTAL(3,{0})", ws.Cells[top2, ic, ir - 1, ic].FullAddress));//+3 trang thai
			XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, VeBorder: false, congthuc: string.Format("SUBTOTAL(3,{0})", ws.Cells[top2, ic, ir - 1, ic].FullAddress));//+4 loai vang
			XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, VeBorder: false);//vao
			XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, VeBorder: false);//ra
			XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, VeBorder: false, congthuc: string.Format("SUBTOTAL(3,{0})", ws.Cells[top2, ic, ir - 1, ic].FullAddress));
			XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, VeBorder: false);//vao
			XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, VeBorder: false);//ra
			XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, VeBorder: false, congthuc: string.Format("SUBTOTAL(3,{0})", ws.Cells[top2, ic, ir - 1, ic].FullAddress));
			XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, VeBorder: false);//vao
			XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, VeBorder: false);//ra
			XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, VeBorder: false, congthuc: string.Format("SUBTOTAL(3,{0})", ws.Cells[top2, ic, ir - 1, ic].FullAddress));

			ic = left;
			ic = left + 3;
			var condition1 = ws.Cells[top2, ic, ir - 1, ic].ConditionalFormatting.AddContainsText();
			condition1.Text = "Vắng có lý do";
			condition1.Style.Fill.BackgroundColor.Color = Color.LightBlue;
			var condition2 = ws.Cells[top2, ic, ir - 1, ic].ConditionalFormatting.AddContainsText();
			condition2.Text = "Đã ra về";
			condition2.Style.Fill.BackgroundColor.Color = Color.Green;
			var condition3 = ws.Cells[top2, ic, ir - 1, ic].ConditionalFormatting.AddContainsText();
			condition3.Text = "Đã về (thiếu chấm công)";
			condition3.Style.Fill.BackgroundColor.Color = Color.IndianRed;
			var condition4 = ws.Cells[top2, ic, ir - 1, ic].ConditionalFormatting.AddContainsText();
			condition4.Text = " - ";
			condition4.Style.Fill.BackgroundColor.Color = Color.Gray;

			ir = ir + 2; // cách 2 dòng
			ic = left + 2;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, VeBorder:false, value: "Tổng NV đang làm việc");
			XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, VeBorder:false, plusRow: 1, value: SoNV_DangLV);
			ic = left + 2;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, VeBorder:false, value: "Tổng NV đã ra về");
			XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, VeBorder:false, plusRow: 1, value: SoNV_DaVe);
			ic = left + 2;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, VeBorder:false, value: "Tổng NV không hiện diện");
			XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, VeBorder:false, plusRow: 1, value: SoNV_VangLyDo + SoNV_VangNghi);
			ic = left + 2;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, VeBorder:false, value: "Tổng NV vắng có lý do");
			XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, VeBorder:false, plusRow: 1, value: SoNV_VangLyDo);
			ic = left + 2;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, VeBorder:false, value: "Tổng NV chưa làm việc");
			XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, VeBorder:false, plusRow: 1, value: SoNV_VangNghi);


		}

		public static void EXP_group_DiemDanh(ExcelWorksheet ws, ref int stt, ref int top, ref int left,
			List<cUserInfo> dsnv, cPhongBan phong, ref int SoNV_DangLV, ref int SoNV_DaVe, ref int SoNV_VangLyDo, ref int SoNV_VangNghi) {
			int ir = top, ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, value: phong.Ten, plusRow: 1, Bold: true, VeBorder: false); // ghi dòng phòng ban 

			foreach (var nhanvien in dsnv) {
				ic = left; //reset startcol =1 mỗi lần ghi nv mới

				var ngayCong = nhanvien.DSNgayCong[2];
				//stt,hoten,macc
				stt++;
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)DD.STT, value: stt);
				XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)DD.MANV, value: nhanvien.MaNV); //MaNV
				XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)DD.TEN, value: nhanvien.TenNV); //Ten
				//thái
				switch (ngayCong.TrangThaiDiemDanh) {
					case TrangThaiDiemDanh.VANG_LYDO:
						XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)DD.TRANGTHAI, value: "Vắng có lý do");
						SoNV_VangLyDo++;
						break;
					case TrangThaiDiemDanh.VANG_NGHI:
						XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)DD.TRANGTHAI, value: " - ");
						SoNV_VangNghi++;
						break;
					case TrangThaiDiemDanh.DANGLAMVIEC:
						XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)DD.TRANGTHAI, value: "Đang làm việc");
						SoNV_DangLV++;
						break;
					case TrangThaiDiemDanh.DARAVE:
						XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)DD.TRANGTHAI, value: "Đã ra về");
						SoNV_DaVe++;
						break;
					case TrangThaiDiemDanh.DARAVE_THIEUCC:
						XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)DD.TRANGTHAI, value: "Đã về (thiếu chấm công)");
						SoNV_DaVe++;
						break;

				}
				XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)DD.LOAIVANG, value: ngayCong.DSVang.Aggregate(string.Empty, (current, loaiVang) => current + (loaiVang.LayKyHieu() + ";"))); //
				//1-->3

				for (int x = 0; x < ngayCong.DSVaoRa.Count || x < 3; x++) {
					if (x < 3 && x >= ngayCong.DSVaoRa.Count) {
						XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)DD.VAO1);
						XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)DD.RAA1);
						XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)DD.CA);

					}
					else {
						XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)DD.VAO1, value: ngayCong.DSVaoRa[x].Vao != null ? ngayCong.DSVaoRa[x].Vao.Time : (object)null, numFormat: "H:mm d/M");
						XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)DD.RAA1, value: ngayCong.DSVaoRa[x].Raa != null ? ngayCong.DSVaoRa[x].Raa.Time : (object)null, numFormat: "H:mm d/M");
						XL.FormatCell(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)DD.CA, value: ngayCong.DSVaoRa[x].CIOCodeComp());
					}
				}
				ir++;
			}
			top = ir;
		}


		public static void ExportSheetTreSom(ExcelWorksheet ws, List<cUserInfo> dsnv) {
			ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

			int top = 1, left = 1, ir, ic;
			ir = top;
			ic = left;
			// ghi header
			XL.FormatCell_T(ws, ref ir, ref ic, plusRow: 1, value: "BẢNG THỐNG KÊ VÀO TRỄ, RA SỚM", wrapText: false, VeBorder: false, size: 16, hAlign: ExcelHorizontalAlignment.Left);//+0

			int top2 = ir;
			//ghi col Title
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Họ tên");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Mã NV");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Ngày");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Ca");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Vào trễ");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Ra sớm");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Vào1");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Ra1");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Vào2");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Ra2");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: "Vào3");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, plusRow: 1, value: "Ra3");//+0

			//ghi nội dung
			int top3 = ir;
			ic = left;
			var dsphong = (from nv in dsnv
						   group nv by nv.PhongBan into @group
						   orderby @group.Key.ViTri
						   select @group).ToList();
			foreach (var item in dsphong) {
				var phong = item.Key;
				ic = left;
				List<cUserInfo> dsnv1 = dsnv.Where(o => o.PhongBan.ID == phong.ID).ToList();
				XL.EXP_group_TreSom(ws, ref ir, ref ic, dsnv1, phong);
				//ir++;
			}


		}

		public static void EXP_group_TreSom(ExcelWorksheet ws, ref int top, ref int left, List<cUserInfo> dsnv, cPhongBan phong) {
			int ir = top, ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, value: phong.Ten, plusRow: 1, Bold: true, VeBorder: false); // ghi dòng phòng ban
			foreach (cUserInfo nv in dsnv) {
				ic = left;//reset về cột đầu tiên  dòng tiếp theo
				EXP_record_ChitietTreSom(ws, ref ir, ref ic, nv); // sau khi xuất xong 1 dòng, ir sẽ cập nhật là ir của dòng kế
				//ir++;
			}
			top = ir;// xuất xong 1 group rồi thì cập nhật lại index cho dòng kế

		}

		public static void EXP_record_ChitietTreSom(ExcelWorksheet ws, ref int top, ref int left, cUserInfo nv) {
			int ir = top, ic = left;


			foreach (cNgayCong ngay in nv.DSNgayCong.Where(o => o.TG.VaoTre > TimeSpan.Zero || o.TG.RaaSom > TimeSpan.Zero)) {
				ic = left;
				XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.TEN, value: nv.TenNV);
				XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.MANV, value: nv.MaNV);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.NGAY, value: ngay.Ngay.ToString("dd/MM/yyyy"));
				XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.CA, value: ngay.CIOs_Absents_Code_Comp());
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: ngay.TG.VaoTre.TotalMinutes, numFormat: Settings.Default.numFormatInt);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: ngay.TG.RaaSom.TotalMinutes, numFormat: Settings.Default.numFormatInt);
				for (int i = 0; i < 3; i++) {
					if (i < ngay.DSVaoRa.Count && i < 3) {
						cCheckInOut CIO = ngay.DSVaoRa[i];
						if (ngay.DSVaoRa[i].HaveINOUT >= 0) {
							XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: CIO.TD.BD_LV, numFormat: "H:mm");
							XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: CIO.TD.KT_LV, numFormat: "H:mm"); //ko tăng ir ở đây mà xuống dưới
						}
						else if (CIO.HaveINOUT == -1) {
							XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: CIO.Vao.Time, numFormat: "H:mm");
							XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: null);
						}
						else if (CIO.HaveINOUT == -2) {
							XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: null);
							XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: CIO.Raa.Time, numFormat: "H:mm"); //ko tăng ir ở đây mà xuống dưới
						}
					}
					else if (i >= ngay.DSVaoRa.Count && i < 3) {
						XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: null);
						XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: null);
					}
				}
				ir++; //sau khi xuất hết 3 cái vào ra thì tăng ir, ko tăng bên trong vì nếu tăng bên trong thì
			}
			top = ir;
		}

		public static void ExportSheetThieuChamCong_KhongHieuCa(ExcelWorksheet ws, List<cUserInfo> dsnv) {
			ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

			int top = 1, left = 1, ir, ic;
			ir = top;
			ic = left;
			// ghi header
			XL.FormatCell_T(ws, ref ir, ref ic, plusRow: 1, value: "BẢNG THỐNG KÊ CÁC NGÀY CÓ GIỜ VÀO RA KHÔNG NHẬN DIỆN ĐƯỢC CA", wrapText: false, VeBorder: false, size: 16, hAlign: ExcelHorizontalAlignment.Left);//+0

			int top2 = ir;
			//ghi col Title
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.TEN, value: "Họ tên");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.MANV, value: "Mã NV");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.NGAY, value: "Ngày");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.CA, value: "Ca");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: "Vào trễ");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: "Ra sớm");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: "Vào1");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: "Ra1");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: "Vào2");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: "Ra2");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: "Vào3");//+0
			XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, plusRow: 1, value: "Ra3");//+0

			//ghi nội dung
			int top3 = ir;
			ic = left;
			var dsphong = (from nv in dsnv
						   group nv by nv.PhongBan into @group
						   orderby @group.Key.ViTri
						   select @group).ToList();
			foreach (var item in dsphong) {
				var phong = item.Key;
				ic = left;
				List<cUserInfo> dsnv1 = dsnv.Where(o => o.PhongBan.ID == phong.ID).ToList();
				XL.EXP_group_KhongHieuCa(ws, ref ir, ref ic, dsnv1, phong);
				//ir++;
			}


		}

		public static void EXP_group_KhongHieuCa(ExcelWorksheet ws, ref int top, ref int left, List<cUserInfo> dsnv, cPhongBan phong) {
			int ir = top, ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, value: phong.Ten, plusRow: 1, Bold: true, VeBorder: false); // ghi dòng phòng ban
			foreach (cUserInfo nv in dsnv) {
				ic = left;//reset về cột đầu tiên  dòng tiếp theo
				EXP_record_KoHieuCa(ws, ref ir, ref ic, nv); // sau khi xuất xong 1 dòng, ir sẽ cập nhật là ir của dòng kế
				//ir++;
			}
			top = ir;// xuất xong 1 group rồi thì cập nhật lại index cho dòng kế

		}

		public static void EXP_record_KoHieuCa(ExcelWorksheet ws, ref int top, ref int left, cUserInfo nv) {
			int ir = top, ic = left;


			foreach (cNgayCong ngay in nv.DSNgayCong.Where(o => o.DSVaoRa.Exists(item
																				=> (item.HaveINOUT < 0) // GIỜ THIẾU CHẤM CÔNG
																				|| (item.HaveINOUT == 0 && item.ThuocCa == null && item.DaXN == false)) // CA KO ĐÚNG QUY ĐỊNH , CHƯA QUA XÁC NHẬN

				)) {
				ic = left;
				XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.TEN, value: nv.TenNV);
				XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.MANV, value: nv.MaNV);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.NGAY, value: ngay.Ngay.ToString("dd/MM/yyyy"));
				XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.CA, value: ngay.CIOs_Absents_Code_Full());
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: ngay.TG.VaoTre.TotalMinutes, numFormat: Settings.Default.numFormatInt);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: ngay.TG.RaaSom.TotalMinutes, numFormat: Settings.Default.numFormatInt);
				for (int i = 0; i < 3; i++) {
					if (i < ngay.DSVaoRa.Count && i < 3) {
						cCheckInOut CIO = ngay.DSVaoRa[i];
						if (ngay.DSVaoRa[i].HaveINOUT >= 0) {
							XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: CIO.TD.BD_LV, numFormat: "H:mm");
							XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: CIO.TD.KT_LV, numFormat: "H:mm"); //ko tăng ir ở đây mà xuống dưới
						}
						else if (CIO.HaveINOUT == -1) {
							XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: CIO.Vao.Time, numFormat: "H:mm");
							XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1);
						}
						else if (CIO.HaveINOUT == -2) {
							XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1);
							XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1, value: CIO.Raa.Time, numFormat: "H:mm"); //ko tăng ir ở đây mà xuống dưới
						}
					}
					else if (i >= ngay.DSVaoRa.Count && i < 3) {
						XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1);
						XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.VAO1);
					}
				}
				ir++; //sau khi xuất hết 3 cái vào ra thì tăng ir, ko tăng bên trong vì nếu tăng bên trong thì
			}
			top = ir;
		}
		public static void ExportSheetThongSo1(ExcelWorksheet ws, DateTime ngaydauthang, DataTable tableThongSo) {
			int ic = 1, left = 1, ir = 2, top = 2;
			string temp = string.Empty;

			ir = top;
			left = 2;
			ic = left;
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: ngaydauthang);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["SanLuong"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["DonGia"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["SanLuongGiaCongNoiBo"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["DonGiaGiaCongNoiBo"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["SanLuongGiaCongNgoai"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["DonGiaGiaCongNgoai"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["TrichQuyLuong"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["MucLuongToiThieu"]);
			//v4.0.0.7 boBDC3 XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["BoiDuongCa3"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["DinhMucComTrua"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["HSPCDem"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["HSPCTangCuong"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["HSPCTangCuong_Dem"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["HSPC200"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["HSPC260"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["HSPC300"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["HSPC390"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (double)tableThongSo.Rows[0]["TienLuong1HeSoSP"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: Convert.ToInt32(XL.TinhCongChuanCuaThang(ngaydauthang)), numberFormat:"##");
		}


		public static void ExportSheetChiTietNgayCong(ExcelWorksheet ws, List<cUserInfo> dsnv) {
			ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

			int left = 1, top = 1, ir, ic;
			ir = top;
			ic = left;
			// ghi header
			XL.FormatCell_T(ws, ref ir, ref ic, plusRow: 1, value: "BẢNG THỐNG KÊ CHI TIẾT NGÀY CÔNG", wrapText: false, VeBorder: false, size: 16, hAlign: ExcelHorizontalAlignment.Left);//+0

			int top2 = ir;
			//ghi col Title
			string[,] header = new string[,]
				{
{ "Mã phòng","Mã phòng"}	, { "Phòng","Phòng"}	, { "Mã NV","Mã NV"}	, { "Họ tên","Họ tên"}	, { "Ngày công","Ngày công"}	, { "Ca LV 1","Ca làm việc 1"}	, { "BĐ LV 1","Bắt đầu làm việc 1"}	, { "KT LV1","Kết thúc làm việc 1"}	, { "Ca LV 2","Ca làm việc 2"}	, { "BĐ LV 2","Bắt đầu làm việc 2"}	, { "KT LV2","Kết thúc làm việc 2"}	, { "Ca LV 3","Ca làm việc 3"}	, { "BĐ LV 3","Bắt đầu làm việc 3"}	, { "KT LV3","Kết thúc làm việc 3"}	, { "Qua Đêm","Có làm việc qua đêm"}	, { "TG Check","Tổng thời gian Check IN OUT"}	, { "TGLV","Tổng thời gian làm việc"}	, { "TGLV TC","Tổng thời gian làm việc tăng cường"}	, { "TGLV Đêm","Tổng thời gian làm việc qua đêm"}	, { "Công","Công 1 Ngày làm việc"}	, { "Tổng PC","Tổng phụ cấp"}	, { "PC TC","Phụ cấp tăng cường"}	, { "PC Ca3","Phụ cấp qua đêm"}	, { "PC TC Ca3","Phụ cấp tăng cường qua đêm"}	, { "PC LVNN","Phụ cấp làm việc ban ngày ngày nghỉ"}	, { "PC LVNN Ca3","Phụ cấp làm việc qua đêm ngày nghỉ"}	, { "PC Lễ","Phụ cấp trực lễ tết ban ngày"}	, { "PC Lễ Ca3","Phụ cấp trực lễ tết ban đêm"}	, { "PCYC","Phụ cấp tùy chỉnh"}	, { "Học, họp","Học, họp"}	, { "CT","Công tác"}	, { "PT","Phong trào"}	, { "Lễ","Lễ"}	, { "Phép","Phép"}	, { "BHXH","Bảo hiểm"}	, { "RO","Việc riêng"}	, { "PD","Phong trào Đoàn thể"}	, { "CV","Chờ việc theo yêu cầu quản lý"}//DANGLAM
				};
			for (int col = 0; col < header.GetLength(1); col++) { //so cot
				ic = left;
				for (int row = 0; row < header.GetLength(0); row++) {
					//Console.WriteLine(string.Format("header[{0},{1}] = {2}", row, col, header[row, col]));
					XL.FormatCell_T(ws, ref ir, ref ic, plusCol: 1, value: header[row, col], wrapText: false);
				}
				ir++;
			}

			//ghi nội dung
			int top3 = ir;
			ic = left;
			var dsphong = (from nv in dsnv
						   group nv by nv.PhongBan into @group
						   orderby @group.Key.ViTri
						   select @group).ToList();
			List<string> kh = new List<string> { "H", "CT", "PT", "L", "P", "BH", "RO", "PD", "CV", };//DANGLAM
			Stopwatch stopwatch = new Stopwatch();
			foreach (var item in dsphong) {
				var phong = item.Key;
				ic = left;
				stopwatch.Reset();
				stopwatch.Start();
				List<cUserInfo> dsnv1 = dsnv.Where(o => o.PhongBan.ID == phong.ID).ToList();
				XL.EXP_group_ChiTietNgayCong(ws, ref ir, ref ic, dsnv1, phong, kh);
				stopwatch.Stop();
				Console.WriteLine(string.Format("Phong {0}, {1} NV, thuc hien trong {2}", phong.Ten, dsnv1.Count, Convert.ToSingle((stopwatch.ElapsedMilliseconds / 1000d)).ToString("###0.0#")));
				//ir++;
			}
		}
		public static void EXP_group_ChiTietNgayCong(ExcelWorksheet ws, ref int top, ref int left, List<cUserInfo> dsnv, cPhongBan phong, List<string> kh) {
			int ir = top, ic = left;
			//XL.FormatCell_W(ws, ref ir, ref ic, value: phong.Ten, plusRow: 1, Bold: true, VeBorder: false); // ko cần dòng phòng ban
			foreach (cUserInfo nv in dsnv) {
				ic = left;//reset về cột đầu tiên  dòng tiếp theo
				EXP_record_ChiTietNgayCong(ws, ref ir, ref ic, nv, kh); // sau khi xuất xong 1 dòng, ir sẽ cập nhật là ir của dòng kế

				//ir++;
			}
			top = ir;// xuất xong 1 group rồi thì cập nhật lại index cho dòng kế

		}
		public static void EXP_record_ChiTietNgayCong(ExcelWorksheet ws, ref int top, ref int left, cUserInfo nv, List<string> kh) {
			int ir = top, ic = left, NumberOfDSVaoRa, i = 0;

			foreach (cNgayCong ngay in nv.DSNgayCong) {
				ic = left;
				XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.MANV, value: nv.PhongBan.ID);//maphong
				XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.TEN, value: nv.PhongBan.Ten);//tenphong
				XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.MANV, value: nv.MaNV);//manv
				XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.TEN, value: nv.TenNV);//tennv
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, colWidth: (int)TRS.NGAY, value: ngay.Ngay.ToString("dd/MM/yyyy"));
				for (i = 0; i < 3; i++) {
					NumberOfDSVaoRa = ngay.DSVaoRa.Count;
					if (i < NumberOfDSVaoRa && i < 3) {
						cCheckInOut CIO = ngay.DSVaoRa[i];
						if (ngay.DSVaoRa[i].HaveINOUT >= 0) {
							XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: CIO.ThuocCa.Code);
							XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: CIO.TD.BD_LV, numFormat: "H:mm");
							XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: CIO.TD.KT_LV, numFormat: "H:mm"); //ko tăng ir ở đây mà xuống dưới
						}
						else {
							XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1);
							XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1);
							XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1); //ko tăng ir ở đây mà xuống dưới
						}
					}
					else if (i >= ngay.DSVaoRa.Count && i < 3) {
						XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1);
						XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1);
						XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1);
					}
				}
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Convert.ToInt32(ngay.QuaDem));//qua đêm
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: ngay.TG.GioThuc, numFormat: @"[h]:mm");// bỏ :ss
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: ngay.TG.GioLamViec, numFormat: @"[h]:mm");// bỏ :ss
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: ngay.TG.LamThem, numFormat: @"[h]:mm");// bỏ :ss
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: ngay.TG.LamBanDem, numFormat: @"[h]:mm");// bỏ :ss
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: ngay.TongCong_4008, numFormat: Settings.Default.numFormatFloat101F);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._TongPC, numFormat: Settings.Default.numFormatFloat101F);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._50_TC, numFormat: Settings.Default.numFormatFloat101F);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._30_dem, numFormat: Settings.Default.numFormatFloat101F);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._100_TCC3, numFormat: Settings.Default.numFormatFloat101F);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._100_LVNN_Ngay, numFormat: Settings.Default.numFormatFloat101F);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._150_LVNN_Dem, numFormat: Settings.Default.numFormatFloat101F);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._200_LeTet_Ngay, numFormat: Settings.Default.numFormatFloat101F);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._250_LeTet_Dem, numFormat: Settings.Default.numFormatFloat101F);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._Cus, numFormat: Settings.Default.numFormatFloat101F);
				for (i = 0; i < 9; i++) {
					ws.Cells[ir, ic, ir, ic + i].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
					ws.Cells[ir, ic, ir, ic + i].Style.Numberformat.Format = Settings.Default.numFormatFloat101F;
				}
				for (i = 0; i < nv.DSVang.Count; i++) {
					cLoaiVang xpvang = nv.DSVang[i];
					switch (xpvang.MaLV_Code) {
						case "BD": XL.FormatCell_N(ws, ir, ic + kh.IndexOf("BH"), value: xpvang.WorkingDay, numFormat: Settings.Default.numFormatFloat101F); break;
						default: XL.FormatCell_N(ws, ir, ic + kh.IndexOf(xpvang.MaLV_Code), value: xpvang.WorkingDay, numFormat: Settings.Default.numFormatFloat101F); break;
					}

				}
				//XL.FormatCell_W(ws, ir, ic, value:"start");
				ir++; //sau khi xuất hết 3 cái vào ra thì tăng ir, ko tăng bên trong vì nếu tăng bên trong thì
			}
			top = ir;
		}

		public static void ExportSheetChiTietNgayCong1(ExcelWorksheet ws, List<cUserInfo> dsnv) {
			ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

			int left = 1, top = 3, ir, ic;
			ir = top;
			ic = left;
			// ghi header
			//XL.FormatCell_T(ws, ref ir, ref ic, plusRow: 1, value: "BẢNG THỐNG KÊ CHI TIẾT NGÀY CÔNG", wrapText: false, VeBorder: false, size: 16, hAlign: ExcelHorizontalAlignment.Left);//+0

			int top2 = ir;

			//ghi nội dung
			int top3 = ir;
			ic = left;
			var dsphong = (from nv in dsnv
						   group nv by nv.PhongBan into @group
						   orderby @group.Key.ViTri
						   select @group).ToList();
			List<string> kh = new List<string> { "H", "CT", "PT", "PD", "L", "P", "BH", "RO", "CV", };//DANGLAM
			//Stopwatch stopwatch = new Stopwatch();
			foreach (var item in dsphong) {
				var phong = item.Key;
				ic = left;
				//stopwatch.Reset();
				//stopwatch.Start();
				List<cUserInfo> dsnv1 = dsnv.Where(o => o.PhongBan.ID == phong.ID).ToList();
				XL.EXP_group_ChiTietNgayCong1(ws, ref ir, ref ic, dsnv1, phong, kh);
				//stopwatch.Stop();
				//Console.WriteLine(string.Format("Phong {0}, {1} NV, thuc hien trong {2}", phong.Ten, dsnv1.Count, Convert.ToSingle((stopwatch.ElapsedMilliseconds / 1000d)).ToString("###0.0#")));
				//ir++;
			}
		}
		public static void EXP_group_ChiTietNgayCong1(ExcelWorksheet ws, ref int top, ref int left, List<cUserInfo> dsnv, cPhongBan phong, List<string> kh) {
			int ir = top, ic = left;
			//XL.FormatCell_W(ws, ref ir, ref ic, value: phong.Ten, plusRow: 1, Bold: true, VeBorder: false); // ko cần dòng phòng ban
			foreach (cUserInfo nv in dsnv) {
				ic = left;//reset về cột đầu tiên  dòng tiếp theo
				EXP_record_ChiTietNgayCong1(ws, ref ir, ref ic, nv, kh); // sau khi xuất xong 1 dòng, ir sẽ cập nhật là ir của dòng kế

				//ir++;
			}
			top = ir;// xuất xong 1 group rồi thì cập nhật lại index cho dòng kế

		}
		public static void EXP_record_ChiTietNgayCong1(ExcelWorksheet ws, ref int top, ref int left, cUserInfo nv, List<string> kh) {
			int ir = top, ic = left, NumberOfDSVaoRa, i = 0;

			foreach (cNgayCong ngay in nv.DSNgayCong) {
				ic = left;
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.PhongBan.ID);//maphong
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.PhongBan.Ten);//tenphong
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.MaNV);//manv
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.TenNV);//tennv
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: ngay.Ngay.ToString("dd/MM/yyyy"));
				for (i = 0; i < 3; i++) {
					NumberOfDSVaoRa = ngay.DSVaoRa.Count;
					if (i < NumberOfDSVaoRa && i < 3) {
						cCheckInOut CIO = ngay.DSVaoRa[i];
						if (ngay.DSVaoRa[i].HaveINOUT >= 0) {
							XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: CIO.ThuocCa.Code);
							XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: CIO.TD.BD_LV);
							XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: CIO.TD.KT_LV); //ko tăng ir ở đây mà xuống dưới
						}
						else {
							XL.FillCell(ws, ref ir, ref ic, plusCol: 1);
							XL.FillCell(ws, ref ir, ref ic, plusCol: 1);
							XL.FillCell(ws, ref ir, ref ic, plusCol: 1); //ko tăng ir ở đây mà xuống dưới
						}
					}
					else if (i >= NumberOfDSVaoRa && i < 3) {
						XL.FillCell(ws, ref ir, ref ic, plusCol: 1);
						XL.FillCell(ws, ref ir, ref ic, plusCol: 1);
						XL.FillCell(ws, ref ir, ref ic, plusCol: 1);
					}
				}
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: Convert.ToInt32(ngay.QuaDem));//qua đêm
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: ngay.TG.GioThuc);
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: ngay.TG.GioLamViec);
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: ngay.TG.LamThem);
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: ngay.TG.LamBanDem);
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: ngay.TongCong_4008);
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._TongPC);
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._50_TC);
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._30_dem);
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._100_TCC3);
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._100_LVNN_Ngay);
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._150_LVNN_Dem);
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._200_LeTet_Ngay);
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._250_LeTet_Dem);
				XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: ngay.PhuCaps._Cus);
				for (i = 0; i < 8; i++) {
					//ws.Cells[ir, ic, ir, ic + i].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
					ws.Cells[ir, ic, ir, ic + i].Value = 0f;
				}
				for (i = 0; i < ngay.DSVang.Count; i++) {
					cLoaiVang xpvang = ngay.DSVang[i];
					switch (xpvang.MaLV_Code) {
						case "BD": XL.FillCell(ws, ir, ic + kh.IndexOf("BH"), value: xpvang.WorkingDay); break;
						default: XL.FillCell(ws, ir, ic + kh.IndexOf(xpvang.MaLV_Code), value: xpvang.WorkingDay); break;
					}

				}
				//XL.FillCell(ws, ir, ic, value:"start");
				ir++; //sau khi xuất hết 3 cái vào ra thì tăng ir, ko tăng bên trong vì nếu tăng bên trong thì
			}
			top = ir;
		}

		public static void ExportSheetChiTietLuong1(ExcelWorksheet ws, List<cUserInfo> dsnv) {

			int left = 1, top = 3, ir, ic;
			ir = top;
			ic = left;
			var dsphong = (from nv in dsnv
						   group nv by nv.PhongBan into @group
						   orderby @group.Key.ViTri
						   select @group).ToList();
			foreach (var item in dsphong) {
				var phong = item.Key;
				ic = left;
				List<cUserInfo> dsnv1 = dsnv.Where(o => o.PhongBan.ID == phong.ID).ToList();
				XL.EXP_group_ChiTietLuong1(ws, ref ir, ref ic, dsnv1, phong);
				//ir++;
			}
		}
		public static void EXP_group_ChiTietLuong1(ExcelWorksheet ws, ref int top, ref int left, List<cUserInfo> dsnv, cPhongBan phong) {
			int ir = top, ic = left;
			foreach (cUserInfo nv in dsnv) {
				ic = left;//reset về cột đầu tiên  dòng tiếp theo
				EXP_record_ChiTietLuong1(ws, ref ir, ref ic, nv); // sau khi xuất xong 1 dòng, ir sẽ cập nhật là ir của dòng kế

				//ir++;
			}
			top = ir;// xuất xong 1 group rồi thì cập nhật lại index cho dòng kế

		}
		public static void EXP_record_ChiTietLuong1(ExcelWorksheet ws, ref int top, ref int left, cUserInfo nv) {
			int ir = top, ic = left;

			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.PhongBan.ID);//maphong//"Mã phòng",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.PhongBan.Ten);//tenphong//"Tên phòng",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.MaNV);//manv//"Mã NV",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.TenNV);//tennv//"Họ tên",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.HeSo.LuongCB);//hslcb 5//"HSLCB",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.HeSo.LuongCV);//hslcv//"HSLSP",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.HeSo.BHCongThem_ChoGD_PGD);//"HSBH Cộng thêm lãnh đạo",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.Cong);//cong//"Công",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.Phep);////"Phép",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.H_CT_PT);////"Học, họp, CT, PT",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.Le);//Le 10//"Lễ",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.BHXH);////"BH",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.SoNgayNghiRO_NguyenNgay);////"SoNgayNghiRO_NguyenNgay",//ver 4.0.0.8
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.NghiRo);////"Việc riêng",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PTDT);////""PT Đoàn thể"",//DANGLAM
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.CongCV);////"Chờ việc",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.NgayQuaDem);//qua đêm//"Qua đêm",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._30_dem);////"PC Đêm",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._50_TC);//"PC tăng cường",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._100_TCC3);//"PC TC Ca3",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._100_LVNN_Ngay);//"PC LVNN Ngày",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._150_LVNN_Dem);//"PC LVNN Ca 3",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._200_LeTet_Ngay);//"Trực lễ ngày",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._250_LeTet_Dem);//"Trực lễ ca3",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._Cus);//"Phụ cấp tùy chỉnh",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//"Tổng công",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1); //"Tổng PC",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//, value: nv.chiTietLuong.LCB_Theo.Cong_CDNghi//"Lương CB theo ngày công chuẩn",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//, value: nv.chiTietLuong.LCB_Theo.CongCV//"Lương CB Theo Công CV",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);////, value: nv.chiTietLuong.LSP_Theo.Cong_CDNghi"Lương SP",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.chiTietLuong.LuongDieuChinh);////"Điều chỉnh lương tháng trước",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//, value: nv.chiTietLuong.TongLuong_KoTinhCacLoaiPhuCap//"Tổng lương",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//"PC theo Lương CB",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//, value: nv.chiTietLuong.LSP_Theo.PhuCap"PC theo Lương SP",
			//v4.0.0.7 boBDC3 XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//"Bồi dưỡng ca 3",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//"Tổng PC",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//"Tổng lương và PC",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.chiTietLuong.KhauTru.TamUng);//"Tạm ứng",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.chiTietLuong.MucDongBHXH);//"Mức đóng BHXH",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//"Khấu trừ BHXH, YT, TN",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.chiTietLuong.KhauTru.ThuChiKhac);//"Thu chi khác",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//"Tiền cơm trưa",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//"Thực lãnh",
			//XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.chiTietLuong.LSP_Theo.Cong_CDNghi);////"Lương SP",
			//XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.chiTietLuong.LSP_Theo.PhuCap);//"PC theo Lương SP",

			ir++; //sau khi xuất hết 3 cái vào ra thì tăng ir, ko tăng bên trong vì nếu tăng bên trong thì
			top = ir;
		}

	}
}
