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

		public static void EXP_Header(ExcelWorksheet ws, ref int top1,int leftTCT = 1, int rightTCT = 10,
            int leftVietNam = 2, int rightVietNam = 20,
            int leftTitle = 3, int rightTitle = 20,
			int sizeTCT = 12, int sizeVietNam = 12, int sizeTitle = 12,
			DateTime? ngayLap = null,
			string headerString = null,
			string logoName = null
			) {
			int ir, ic;

			ir = top1;
			ic = leftTCT;
			XL.FormatCell_TCAS201903(ws, ref ir, ref ic, size: sizeTCT, fromCol:leftTCT, toCol:rightTCT, bold: false, VeBorder: false, wrapText: false, plusRow: 1, value: Settings.Default.headerTCty);
			XL.FormatCell_TCAS201903(ws, ref ir, ref ic, size: sizeTCT, fromCol:leftTCT, toCol:rightTCT, bold: false, VeBorder: false, wrapText: false, plusRow: 1, value: Settings.Default.headerTNHH);
			XL.FormatCell_TCAS201903(ws, ref ir, ref ic, size: sizeTCT, fromCol:leftTCT, toCol:rightTCT, bold: false, VeBorder: false, wrapText: false, plusRow: 1, value: Settings.Default.headerCNS);
			XL.FormatCell_TCAS201903(ws, ref ir, ref ic, size: sizeTCT, fromCol:leftTCT, toCol:rightTCT, bold: true, VeBorder: false, wrapText: false, plusRow: 1, value: Settings.Default.headerNMTLKH);

			ir = top1;
			ic = leftVietNam;
			XL.FormatCell_TCAS201903(ws, ref ir, ref ic, size: sizeVietNam, fromCol: leftVietNam, toCol: rightVietNam, bold: true, VeBorder: false, wrapText: false, plusRow: 1, value: Settings.Default.headerCHXHCNVN, hAlign: ExcelHorizontalAlignment.CenterContinuous);
			XL.FormatCell_TCAS201903(ws, ref ir, ref ic, size: sizeVietNam, fromCol: leftVietNam, toCol: rightVietNam, bold: true, VeBorder: false, wrapText: false, plusRow: 1, value: Settings.Default.headerDL_TD_HP, hAlign: ExcelHorizontalAlignment.CenterContinuous);
			ir = ir + 2;// bỏ 2 row rồi bắt đầu ghi title
            ic = leftVietNam;
            XL.FormatCell_TCAS201903(ws, ref ir, ref ic, size: sizeVietNam, fromCol: leftVietNam, toCol:rightVietNam, bold: false, italic:true, VeBorder: false, wrapText: false, 
				value: string.Format(Settings.Default.headerTPHCMNgaythangnam, ((DateTime)ngayLap).Day, ((DateTime)ngayLap).Month, ((DateTime)ngayLap).Year), hAlign: ExcelHorizontalAlignment.CenterContinuous);

			ir = ir + 1;// bỏ 2 row rồi bắt đầu ghi title
			ic = leftTitle;
			XL.FormatCell_TCAS201903(ws, ref  ir, ref ic, size: sizeTitle, fromCol:leftTitle, toCol:rightTitle, bold: true, VeBorder: false, wrapText: false, value: headerString);
			ir = ir + 2;// bỏ 2 row rồi bắt đầu ghi nội dung
			top1 = ir;

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
			t2.Add("(x): có phụ cấp chỉnh tay");
			t2.Add("(bt): sử dụng quỹ tăng cường cho thời gian vào trễ");
			t2.Add("(bs): sử dụng quỹ tăng cường cho thời gian ra sớm");
			t2.Add("2tP/(p2): nghỉ phép 2 giờ");
			t2.Add("(nP): nghỉ phép 4 giờ");
			t2.Add("(p6): nghỉ phép 6 giờ");
			t2.Add("2tRo: nghỉ việc riêng 2 giờ");
			
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

		public static void ExportSheetBangLuongCongNhat(ExcelWorksheet ws, DateTime m_thang, DataTable tableLuongCongNhat, int DinhMucComTrua, out string FullAddressTongLuongCongNhat) {
            FullAddressTongLuongCongNhat = string.Empty;
            if (tableLuongCongNhat.Rows.Count == 0) return;// nếu ko có nhân viên làm việc công nhật thì ko ghi sheet này

			ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet
			ws.Workbook.CalcMode = ExcelCalcMode.Automatic;

			int stt = 1, top = 1, left = 1, bottom = 1, ir, ic;
			bottom = top;

			#region // 1. ghi header

			string headerString = string.Format(Settings.Default.titleBangLuongCongNhat, m_thang.ToString("MM/yyyy"));
            int left1 = 1, left2 = 5, rightVietNam = 10, leftT = 1;
			ir = top;

            EXP_Header(ws, ref ir, left1, rightTCT: 3, leftVietNam: left2, rightVietNam: rightVietNam, leftTitle: leftT, rightTitle: 10, sizeTCT: 12, sizeVietNam: 12, sizeTitle: 14,
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
			XL.FormatCell_T(ws, ref ir, ref ic, colWidth: (int)X.TAMUNG, value: "Tiền ăn giữa ca tháng " + m_thang.ToString("M/yyyy"), plusCol: 1);
			XL.FormatCell_T(ws, ref ir, ref ic, colWidth: (int)X.THUCLANH, value: "Thực lãnh", plusCol: 1);
			XL.FormatCell_T(ws, ref ir, ref ic, colWidth: (int)X.KYNHAN, value: "Ký nhận", plusCol: 1);
			ir++;
			#endregion

			#region //3. ghi noi dung

            int colDonGiaLuong = 0, colSoNgayCong = 0, colTamUng = 0, colThanhTien = 0, colTienComTrua, colThucLanh = 0;
            int rowStartRecord = ir, rowLastestRecord = ir;
			foreach (DataRow row in tableLuongCongNhat.Rows) {
				ic = left;
				FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: stt);
				FormatCell_W(ws, ref ir, ref ic, plusCol: 1, value: row["UserFullName"].ToString());
				FormatCell_W(ws, ref ir, ref ic, plusCol: 1, wrapText:true, value: row["ChucVu"].ToString());

                colDonGiaLuong = ic;
				FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: (int)row["DonGiaLuong"], numFormat: Settings.Default.numFormatInt);

                colSoNgayCong = ic;
				FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(Convert.ToSingle(row["SoNgayCong"]), 2), numFormat: Settings.Default.numFormatFloat101F);

                colThanhTien = ic;
				FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("{0}*{1}", ws.Cells[ir, colDonGiaLuong].Address, ws.Cells[ir, colSoNgayCong].Address),
                    numFormat: Settings.Default.numFormatMoney);

                colTamUng = ic;
				FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(Convert.ToDouble(row["TamUng"]), 2), numFormat: Settings.Default.numFormatMoney);

                colTienComTrua = ic;
				FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: DinhMucComTrua, numFormat: Settings.Default.numFormatMoney);

                colThucLanh = ic;
				FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("{0}-{1}+{2}", ws.Cells[ir, colThanhTien].Address, ws.Cells[ir, colTamUng].Address, ws.Cells[ir, colTienComTrua].Address),
                    numFormat: Settings.Default.numFormatMoney);

				FormatCell(ws, ref ir, ref ic, plusCol: 1, plusRow: 1); //ký nhận
				stt++;
			}
            rowLastestRecord = ir - 1;
            ic = left;

            //chắc chắn luôn có ít nhất 1 record
            int rowTongCong = ir;
            FormatCell_TCAS201903(ws, ref ir, ref ic, plusCol: 1, value: "CỘNG", fromCol: left, toCol: colDonGiaLuong, bold: true);
            ic = colDonGiaLuong;
            func3(ws, ref ir, ref ic, rowStartRecord, rowLastestRecord, numFormat: Settings.Default.numFormatMoney); //dongialuong
            func3(ws, ref ir, ref ic, rowStartRecord, rowLastestRecord, numFormat: Settings.Default.numFormatFloat101F); //songaycong
			func3(ws, ref ir, ref ic, rowStartRecord, rowLastestRecord, numFormat: Settings.Default.numFormatMoney); //thanhtien
			func3(ws, ref ir, ref ic, rowStartRecord, rowLastestRecord, numFormat: Settings.Default.numFormatMoney); //tamung
			func3(ws, ref ir, ref ic, rowStartRecord, rowLastestRecord, numFormat: Settings.Default.numFormatMoney); //tiencomtrua
			func3(ws, ref ir, ref ic, rowStartRecord, rowLastestRecord, numFormat: Settings.Default.numFormatMoney); //thuclanh
			FormatCell_W(ws, ref ir, ref ic, VeBorder:true, plusCol: 1);//ký nhận

			#endregion

			#region //4. ghi footer

			ir++;
            ws.Cells[ir, 1].Value = "Số tiền bằng chữ: ";
            ws.Cells[ir, 1].Style.Font.Color.SetColor(Color.White);
            ws.Cells[ir, 1].Style.Border.BorderAround(ExcelBorderStyle.None);
            ic = 2;
            ws.Cells[ir, 2].Formula = string.Format("{0}&VND({1})", ws.Cells[ir, 1].Address, ws.Cells[rowTongCong, colThucLanh].Address);
            int lastColumn = ws.Dimension.End.Column;
            ws.Cells[ir, 2, ir, lastColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;

            FullAddressTongLuongCongNhat = ws.Cells[rowTongCong, colThanhTien].FullAddress;
			ir = ir + 2;
			ic = left;
			EXP_Footer(ws, ref ir,
					   new int[] { 2, 5, 9 },
					   new int[] { 13, 13, 13 },
					   new string[] { "GIÁM ĐỐC", "KTT", "LẬP BIỂU" },
					   new string[] { Settings.Default.LastTenGD, Settings.Default.LastTenKTT, Settings.Default.LastTenNVLapBieuLuong });

			#endregion


		}

		public static void ExportSheetBangLuong(ExcelWorksheet ws, DateTime m_thang, List<cUserInfo> dsnv, string tenNVLapBieu,
            int MucLuongToiThieuND205, int MucLuongToiThieuTT17MoiNhat, int MucLuongTT172135, int DinhMucComTrua,
            out int rowTong,
            out int colTongLuongCB, out int colTongLuongCV, 
            out int colTongDieuChinhLuong, 
            out int colTongPCTheoHSLCB, 
            out int colTongHSLuongCBQuyDoi, 
            out int colTongHSPhuCapQuyDoi,
            out int topRow_TinhLuongSP, out int colLuongSP,
            out int endRow_TinhLuongSP, out int colPCTheoHSLSP
            ) {
			ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet
			ws.Workbook.CalcMode = ExcelCalcMode.Automatic;

			int ir = 1, ic = 1, stt = 1, left = 1, top = 1, index = 0;

			#region // 1. ghi header

			string headerString = string.Format(Settings.Default.titleBangLuongThang, m_thang.ToString("MM/yyyy"));
			int left1 = 1, left2 = 22, rightVietNam = 42, leftT = 1;
			ir = top;
			EXP_Header(ws, ref ir, left1, rightTCT:20, leftVietNam:left2, rightVietNam:rightVietNam, leftTitle: leftT, rightTitle:42, sizeTCT: 22, sizeVietNam: 22, sizeTitle: 22,
					   ngayLap: DateTime.Today, headerString: headerString);

			#endregion

			#region // 2. ghi colTitle

			int top2 = ir;// giữ lại row bắt đầu ghi Title
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)L.STT, value: "TT", fromRow: ir, toRow: ir + 1, rowContainValue:ir+1, plusCol: 1);//col 1
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)L.MANV, value: "Mã NV", fromRow: ir, toRow: ir + 1, rowContainValue:ir+1, plusCol: 1);
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)L.HOTEN, value: "Họ tên", fromRow: ir, toRow: ir + 1, rowContainValue:ir+1, plusCol: 1);//col 3

            XL.FormatCell_TCAS201903(ws, ref ir, ref ic, colWidth: null, size:10, value: "Hệ số lương (NĐ205)", fromCol: ic, toCol: ic + 1);//write HSLương ở trên -- ko tinh col
            XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.HSLUONGCB, value: "Cơ bản", fromRow: ir + 1, fromCol: ic, plusCol: 1);//col 4
            XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.HSLUONGSP, value: "Sản phẩm", fromRow: ir + 1, fromCol: ic, plusCol: 1);//col 5

			XL.FormatCell_TCAS201903(ws, ref ir, ref ic, colWidth: null, value: "Hệ số lương TT 17", fromCol: ic, toCol: ic + 3);//write HSLương ở trên -- ko tinh col
            XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.HSLUONGSP, value: "Cơ bản", fromRow: ir + 1, fromCol: ic, plusCol: 1);//col 6
            XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.HSLUONGSP, value: "Phụ cấp trách nhiệm", fromRow: ir + 1, fromCol: ic, plusCol: 1);
            XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.HSLUONGSP, value: "Phụ cấp độc hại", fromRow: ir + 1, fromCol: ic, plusCol: 1);
            XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.HSLUONGSP, value: "Phụ cấp chức vụ", fromRow: ir + 1, fromCol: ic, plusCol: 1);//col 9

            XL.FormatCell_TCAS201903(ws, ref ir, ref ic, colWidth: null, value: "Thống kê", fromCol: ic, toCol: ic + 7);//write ngày công chuẩn ở trên-- ko tinh col
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.BANNGAY, value: "Công", fromRow: ir + 1, fromCol: ic, plusCol: 1);//col 10
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.HOCHOPLE, size: 10, value: "Học, họp, CT, PT NM chi", fromRow: ir + 1, fromCol: ic, plusCol: 1);
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.HOCHOPLE, value: "Lễ, Tết", fromRow: ir + 1, fromCol: ic, plusCol: 1);
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.PTDOANTHE, size:10, value: "PT (Đoàn thể chi)", fromRow: ir + 1, fromCol: ic, plusCol: 1);//bỏ
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.PHEP, value: "Phép", fromRow: ir + 1, fromCol: ic, plusCol: 1);
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.CV, value: "Chờ việc", fromRow: ir + 1, fromCol: ic, plusCol: 1);
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.VIECRIENG, value: "Việc riêng", fromRow: ir + 1, fromCol: ic, plusCol: 1);//col15
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.VIECRIENG, value: "Nghỉ BHXH", fromRow: ir + 1, fromCol: ic, plusCol: 1);//col16

			XL.FormatCell_TCAS201903(ws, ref ir, ref ic, colWidth: null, value: "Phụ cấp", fromCol: ic, toCol: ic + 7);//write làm thêm giờ ở trên-- ko tinh col
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.PC30, value: "Ca 3", fromRow: ir + 1, fromCol: ic, plusCol: 1);//ic+0 
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.PC50, value: "Tăng cường", fromRow: ir + 1, fromCol: ic, plusCol: 1);//ic+1
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.PCTCC3, value: "TC Ca 3", fromRow: ir + 1, fromCol: ic, plusCol: 1);//ic+2//col20
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.PC100, value: "LVNN 100%", fromRow: ir + 1, fromCol: ic, plusCol: 1);//ic+3
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.PC160, value: "LVNN 150%", fromRow: ir + 1, fromCol: ic, plusCol: 1);//ic+4
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.PC200, value: "Trực lễ 200%", fromRow: ir + 1, fromCol: ic, plusCol: 1);//ic+5
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.PC290, value: "Trực lễ ca 3 290%", fromRow: ir + 1, fromCol: ic, plusCol: 1);//ic+6
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.PCTCC3, value: "Khác", fromRow: ir + 1, fromCol: ic, plusCol: 1);//ic+7 //col25

			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)L.TONGCONG, value: "Tổng công", fromRow: ir, toRow: ir + 1, rowContainValue: ir +1, plusCol: 1);//col26
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)L.TONGPC, value: "Tổng PC", fromRow: ir, toRow: ir + 1, rowContainValue: ir +1, plusCol: 1);//

			XL.FormatCell_TCAS201903(ws, ref ir, ref ic, colWidth: null, value: "Tiền lương theo hệ số (NĐ205)", fromCol: ic, toCol: ic + 4/* ic + 3*/);//write tiền lương ở trên-- ko tinh col
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.LUONGCB, value: "Theo HSLCB", fromRow: ir + 1, fromCol: ic, plusCol: 1);
            colLuongSP = ic; // cột lương sp phải tính lại sau khi tính được tiền 1 sản phẩm
            XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.LUONGSP, value: "Theo HSLSP", fromRow: ir + 1, fromCol: ic, plusCol: 1);
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.LUONGSP, value: "Theo Công Chờ việc", fromRow: ir + 1, fromCol: ic, plusCol: 1);//col30
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.DIEUCHINH, value: "Điều chỉnh lương tháng trước", fromRow: ir + 1, fromCol: ic, plusCol: 1);
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.TONGLUONG, value: "Tổng lương", fromRow: ir + 1, fromCol: ic, plusCol: 1);

			XL.FormatCell_TCAS201903(ws, ref ir, ref ic, colWidth: null, value: "Tiền Phụ cấp theo hệ số (NĐ205)", fromCol: ic, toCol: ic + 2);//write phu cap ở trên-- ko tinh col
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.PCLUONGCB, value: "Theo HSLCB", fromRow: ir + 1, fromCol: ic, plusCol: 1);//ol33
            colPCTheoHSLSP = ic;
            XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.PCLUONGSP, value: "Theo HSLSP", fromRow: ir + 1, fromCol: ic, plusCol: 1);
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.TONGPCLUONG, value: "Tổng PC", fromRow: ir + 1, fromCol: ic, plusCol: 1);//col35

			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)L.TONGLUONGPC, value: "Tổng lương và PC", fromRow: ir, toRow: ir + 1,  rowContainValue: ir +1, plusCol: 1);

			XL.FormatCell_TCAS201903(ws, ref ir, ref ic, colWidth: null, value: "Khấu trừ", fromCol: ic, toCol: ic + 2); // write khau tru o tren-- ko tinh col
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.TAMUNG, value: "Tạm ứng", fromRow: ir + 1, fromCol: ic, plusCol: 1);
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.BHXH, value: "BHXH, YT, TN", fromRow: ir + 1, fromCol: ic, plusCol: 1);//col 38
			XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.THUCHIKHAC, value: "Thu chi khác", fromRow: ir + 1, fromCol: ic, plusCol: 1);

			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)L.TIENCOMTRUA, value: "Tiền ăn giữa ca", fromRow: ir, toRow: ir + 1, rowContainValue: ir + 1, plusCol: 1);///col 40
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)L.THUCLANH, value: "Thực lãnh", fromRow: ir, toRow: ir + 1, rowContainValue: ir + 1, plusCol: 1);
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)L.KYNHAN, value: "Ký nhận", fromRow: ir, toRow: ir + 1, rowContainValue: ir + 1, plusCol: 1);

            XL.FormatCell_TCAS201903(ws, ref ir, ref ic, colWidth: null, size: 10, value: "Hệ số quy đổi", fromCol: ic, toCol: ic + 1);//write HSLương ở trên -- ko tinh col
            XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.HSLUONGCB, value: "HS Lương SP", fromRow: ir + 1, fromCol: ic, plusCol: 1);//col 4
            XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)L.HSLUONGSP, value: "HS Phụ cấp", fromRow: ir + 1, fromCol: ic, plusCol: 1);//col 5

            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)L.KYNHAN, value: "Nghỉ Ro Cả ngày", fromRow: ir, toRow: ir + 1, rowContainValue: ir + 1, plusCol: 1);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)L.KYNHAN, value: "Nhóm", fromRow: ir, toRow: ir + 1, rowContainValue: ir + 1, plusCol: 1);// tách các BP, this column must hidden

            ws.Row(ir + 1).Height = 65d;
            #region ghi số thứ tự cột để dễ phục vụ công tác excel
            int iRowSTTCot = ir - 1;
            int iStartSection = 2;
            int iEndSection = ic - 1;
            index = 0;
            for (index = 0; (iStartSection + index) <= iEndSection; index++) //iTemp = 2 vì bỏ qua cột STT đầu
            {
                ws.Cells[iRowSTTCot, (iStartSection + index)].Style.Font.Color.SetColor(Color.White);
                ws.Cells[iRowSTTCot, (iStartSection + index)].Value = (index + 1);                
            }
            #endregion

            ir = ir + 2;// tăng 2 dòng để bắt đầu ghi
			ic = left;
			#endregion -----------------------------------------------------------------------

			#region // 3. ghi noi dung

			int top3 = ir; // giữ lại dòng bắt đầu ghi nội dung
            topRow_TinhLuongSP = ir; //giữ lại dòng bắt đầu ghi nội dung để tính lại phần lương theo HSL Sản phẩm
			SUMLUONG sumLuong = new SUMLUONG();
			var dsphong = (from nv in dsnv
						   group nv by nv.PhongBan into @group
						   orderby @group.Key.ViTri
						   select @group).ToList();

			foreach (var item in dsphong) {
				var phong = item.Key;
				List<cUserInfo> dsnv1 = dsnv.Where(o => o.PhongBan.ID == phong.ID).ToList();
				EXP_group_LuongNV(ws, ref stt, ref ir, ref ic, MucLuongToiThieuND205, MucLuongToiThieuTT17MoiNhat, MucLuongTT172135, DinhMucComTrua, dsnv1, phong, ref sumLuong);
				ic = left;
				//ir++; //exp record cuối  cùng của group tự động tăng ir
			}
            endRow_TinhLuongSP = ir; //giữ lại dòng kết thúc ghi nội dung để tính lại phần lương theo HSL Sản phẩm
            int rowStartSectionNameRange = top3, colStartSectionNameRange = 2;
            int rowEndSectionNameRange = endRow_TinhLuongSP, colEndSectionNameRange = iEndSection;

            ws.Workbook.Names.Add("BangLuongRange", ws.Cells[rowStartSectionNameRange, colStartSectionNameRange, rowEndSectionNameRange, colEndSectionNameRange]);

            #region // ghi dòng tổng cộng, FORMAT TỪ DÒNG GHI PHÒNG BAN ĐẾN DÒNG CUỐI NUMBERFORMAT

            int bottom3 = ir - 1; //top3 là dòng phòng ban, sum từ dòng kế tiếp; bottom -1 vì bottom = ir là index dòng kế
			var temp = 3;////ver 4.0.0.4	
			FormatCell_TCAS201903(ws, ref ir, ref temp, value: "Tổng cộng: ", bold:true, wrapText: false, fromCol: left, toCol: 3, hAlign: ExcelHorizontalAlignment.Right); //ver 4.0.0.4	
			int ic5 = 4;// bat dau ghi tong cong tu cot 4 hslcb

            #region ghi dòng sum cuối cùng
            rowTong = ir;

			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")"); // vì top là dòng phòng ban, sum từ dòng kế dòng phòng ban//  "CB"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "SP"//col5
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "CBTT17"
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "PCTNTT17"
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "PCCDTT17"
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "PCCVTT17"
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Công"//col10
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Học, họp, CT, PT"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "lễ"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "PTĐT"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Phép"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Việc riêng"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "BHXH"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Chờ việc"//col17
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "30%"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "50%"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "TCC3 100%"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "LVNN 100%"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "LVNN 150%"//col20
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Trực lễ 200%"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Trực lễ ca3 290%"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "PCKhác"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Tổng công"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Tổng PC"//col27
            colTongLuongCB = ic5;
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Lương CB"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Lương SP"
            colTongLuongCV = ic5;
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Lương CV"
            colTongDieuChinhLuong = ic5;
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Điều chỉnh lương tháng trước"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Tổng lương"//col32
            colTongPCTheoHSLCB = ic5;
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "PC theo Lương CB"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "PC theo Lương SP"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Tổng PC"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Tổng lương và PC"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Tạm ứng"//col37
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "10,5% BHXH, YT, TN"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Thu chi khác"
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Tiền cơm trưa"
            int colThucLanh = ic5;
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// "Thực lãnh"//col41
			FormatCell_W(ws, ref ir, ref ic5, plusCol: 1); //ký nhận// đáng lẽ sau dòng SUM này thì tăng ir để trỏ sang dòng mới// "Ký nhận"
            colTongHSLuongCBQuyDoi = ic5;
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// he so quy doi luong cb
            colTongHSPhuCapQuyDoi = ic5;
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// he so quy doi phu cap
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[top3, ic5, bottom3, ic5].Address + ")");// nghi Ro Nguyen Ngay

			#endregion

			#region chỉnh format number từng cột

			ic5 = 4;// bat dau ghi tong cong tu cot 4 hslcb
			//ws.Cells[t1, ic5, bottom, ic5].Style.Numberformat.Format = Settings.Default.numFormatFloat101F; // vì top là dòng phòng ban, sum từ dòng kế dòng phòng ban
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat20); //hslcb// t1, ic5, bottom + 1 - 1 vì t1, ic5, bottom3 + 1 là do format luôn dòng tổng cộng
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat20); //col 5 hslcv
            XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat20); //cb tt17
            XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat20); //tn tt17
            XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat20); // dh tt17
            XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat20); //cv tt17
            XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); //cong//col10 
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat10); //hoc
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatInt); //le
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat10); //ptdt //DANGLAM
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); //phep
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); //cho viec//col15
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); //viecrieng //DANGLAM
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); //bhxh 
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); //30per
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); //50%
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); //tcc3
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); // CN 100%
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); // CN C3 150%
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); // truc le 200%
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); //truc le c3 290%
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); //pc khac
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);// tong cong
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F); //tong pc//col25
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney); // luong cb
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney); //luong sp
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney); //luong CV
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney); // dieu chinh luong
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney);//Tong Luong ko phu cap
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney); //Phu Cap Theo HSLCB
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney); //Phu Cap Theo HSL SP
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney); //Tong PC
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney); //Tong Luong va PC
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney);//TamUng
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney); //BHXH
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney); //ThuChiKhac
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney); //ComTrua
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatMoney);//Thuc Lanh
			FormatCell_W(ws, ref ir, ref ic5, plusCol: 1); //ký nhận
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat30); //He so quy doi Cong ra san pham
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat30);//He so quy doi Phu Cap ra San Pham
			XL.FormatNumber(ws, ref top3, ref ic5, top3, ic5, bottom3 + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatInt);// Nghi Ro Nguyen ngay

            #endregion

			#endregion
            int icBoPhan = ic5, SoCotCuoiPhaiHidden = 4;

            for (index = 0; index < SoCotCuoiPhaiHidden; index++)
            { ws.Column(ic5-index).Hidden = true; }
			#region // set colWidth = 0 các cột ko cần thiết

			//if (Math.Abs(sumLuong.pc100_16 - 0f) < 0.01f) ws.Column(19).Width = 0;
			//if (Math.Abs(sumLuong.pc160_17 - 0f) < 0.01f) ws.Column(20).Width = 0;
			//if (Math.Abs(sumLuong.pc200_18 - 0f) < 0.01f) ws.Column(21).Width = 0;
			//if (Math.Abs(sumLuong.pc290_19 - 0f) < 0.01f) ws.Column(22).Width = 0;
			//if (Math.Abs(sumLuong.dieuchinh25 - 0d) < 0.01d) ws.Column(29).Width = 0;
			////v4.0.0.7 boBDC3 if (Math.Abs(sumLuong.thuchikhac34 - 0d) < 0.01d) ws.Column(34).Width = 0;
			//if (Math.Abs(sumLuong.thuchikhac34 - 0d) < 0.01d) ws.Column(37).Width = 0;

			#endregion

			#endregion

			#region //4. ghi footer

			ir++;
            ws.Cells[ir, 1].Formula = string.Format("CONCATENATE({0},VND(ROUND({1},0)))", ws.Cells[ir, ic5 - 1].Address, ws.Cells[ir - 1, colThucLanh].Address);
            ws.Cells[ir, 1].Style.Border.BorderAround(ExcelBorderStyle.None);
            ws.Cells[ir, 1].Style.Font.Bold = true;
            ws.Cells[ir, 1, ir, ic5 - 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;

            FormatCell_W(ws, ir, ic5 - 1, value: "Số tiền bằng chữ: ");
            ws.Cells[ir, ic5 - 1].Style.Font.Color.SetColor(Color.White);
            ws.Cells[ir, ic5 - 1].Style.Border.BorderAround(ExcelBorderStyle.None);

            //FormatCell_T(ws, ref ir, ref temp2, congthuc: , VeBorder: false, Bold: true, hAlign: );//ký nhận

            ir = ir + 2; // cách 2 dòng
			EXP_Footer(ws, ref ir,
					   new int[] { 6, 24, 38 },
					   new int[] { 24, 24, 24 },
					   new string[] { "GIÁM ĐỐC", "KTT", "LẬP BIỂU" },
					   new string[] { Settings.Default.LastTenGD, Settings.Default.LastTenKTT, tenNVLapBieu },
					   khoangCachTen_ChucVu:8);

			#endregion

		}

        internal static void TinhNgayCNBu(DataTable tableNgayLeThangTruoc, DataTable tableNgayLe, DateTime ngaydauthang, DateTime ngaycuoithang, out int truNgayCNTrungLe, out int ngayCNnghiBu)
        {
            ngayCNnghiBu = 0;
            truNgayCNTrungLe = 0;
            if (tableNgayLe.Rows.Count == 0) truNgayCNTrungLe = 0;
            else
            {
                var dsNgayLe = (from DataRow row in tableNgayLe.Select(string.Empty, "HDate ASC")
                                let ngayle = (DateTime)row["HDate"]
                                select ngayle).ToList();
                for(DateTime temp = ngaycuoithang; temp > ngaydauthang; temp = temp.AddDays(-1))
                {
                    var found = false;
                    for (int i=0; i< dsNgayLe.Count; i++)
                        if (temp == dsNgayLe[i]) found = true;
                    if (found == true)
                    {
                        if (temp.DayOfWeek == DayOfWeek.Sunday)
                            truNgayCNTrungLe = 1;
                    }
                    else break;                    
                }
            }
            if (tableNgayLeThangTruoc.Rows.Count == 0) ngayCNnghiBu = 0;
            else
            {
                var dsNgayLe = (from DataRow row in tableNgayLeThangTruoc.Select(string.Empty, "HDate ASC")
                                let ngayle = (DateTime)row["HDate"]
                                select ngayle).ToList();
                var ngaycuoithangTruoc = ngaydauthang.AddDays(-1);
                var ngaydauthangTruoc = ngaycuoithangTruoc.AddMonths(-1);
                var truNgayCNTrungLe2 = 0;
                for (DateTime temp = ngaycuoithangTruoc; temp > ngaydauthangTruoc; temp = temp.AddDays(-1))
                {
                    var found = false;
                    for (int i = 0; i < dsNgayLe.Count; i++)
                        if (temp == dsNgayLe[i]) found = true;
                    if (found == true)
                    {
                        if (temp.DayOfWeek == DayOfWeek.Sunday)
                            truNgayCNTrungLe2 = 1;
                    }
                    else break;
                }
                if (truNgayCNTrungLe2 == 1)
                {
                    ngayCNnghiBu = -1;
                }
            }
        }

        public static void EXP_group_LuongNV(ExcelWorksheet ws, ref int stt, ref int top, ref int left, int MucLuongToiThieuND205, int MucLuongToiThieu_TT17MoiNhat, int MucLuongTT172135, int DinhMucComTrua,
            List<cUserInfo> dsnv, cPhongBan cPhongBan, ref SUMLUONG sumluong) {
			// giữ lại vị trí ô để tính tổng
			var ir = top;
			var ic = left;
			int bottom, right;

			ir = ir + 1; // bỏ 1 dòng ghi phòng ban
			foreach (var nv in dsnv.Where(o => o.PhongBan.ID == cPhongBan.ID)) {
				ic = left; //reset ve cot dau tien sau moi lan lap
				XL.EXP_record_LuongNVChinhThuc(ws, ref stt, ref ir, ref ic, MucLuongToiThieuND205, MucLuongToiThieu_TT17MoiNhat, MucLuongTT172135, DinhMucComTrua, nv, nv.PhongBan.Ten, ref sumluong);

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
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true); //hslcb// bottom - 1 vì bottom là index của dòng mới
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true); //col 5 hslcv
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true); 
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true); 
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);//col10 Cong
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true); 
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true); 
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);//col15 VR
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true); 
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true); //col20 TCC3 100% 
            func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);//col25 PC Khac
            func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true); 
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);//col30  Theo Cong CV
            func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true); 
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);//col35 Tong PC tạm ứng
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);//col39 thu chi khac
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);//col 40 com trua
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);// col 41 thuc lanh
            FormatCell_W(ws, ref ir, ref ic, plusCol: 1); //ký nhận
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);//He so quy doi
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);
			func3(ws, ref ir, ref ic, ir + 1, bottom - 1, bold: true, italic: true);//nghỉ Ro nguyên ngày
            FormatCell_W(ws, ref ir, ref ic, plusCol: 1, value: cPhongBan.Ten); // tên phòng ban để lọc dữ liệu


			#endregion

			top = bottom; // cập nhật lại giá trị dòng tiếp theo

		}
		public static void func3(ExcelWorksheet ws, ref int ir, ref int ic, int fromRow, int toRow, string numFormat = null, bool bold = false, bool italic = false) {

			string congthuc = "SUBTOTAL(9,{0})";

			string addStartRow = ws.Cells[fromRow, ic, toRow, ic].Address; // bottom - 1 vì bottom là index của dòng mới
			congthuc = string.Format(congthuc, addStartRow);

			XL.FormatCell_N(ws, ref ir, ref ic, bold: bold, italic:italic, congthuc: congthuc, plusCol: 1, numFormat: numFormat);
		}

		public static void EXP_record_LuongNVChinhThuc(ExcelWorksheet ws, ref int stt, ref int ir, ref int ic, int MucLuongToiThieu_ND205, int MucLuongToiThieu_TT17MoiNhat, int MucLuongTT172135, int DinhMucComTrua, 
            cUserInfo nv, string TenPhong, ref SUMLUONG sumBKL) {
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: stt); //col 1
            XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, value: nv.MaNV);
            XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, value: nv.TenNV);//col3
            int colHSLCBND205 = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.HeSo.LuongCB, 2));
            int colHSLSPND205 = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.HeSo.LuongCV, 2));//col 5
            int colHSLCBTT17 = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.HeSo.LCBTT17, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.HeSo.PCTNTT17, 2));//col 7
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.HeSo.PCDHTT17, 2));
            int colHSPCCV_TT17 = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.HeSo.PCCVTT17, 2));//col 9
            int colCong = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.Cong, 2));//col10
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.H_CT_PT, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.Le, 2));
            int colPTDT = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PTDT, 2));//col13
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.Phep, 2));
            int colCongCV = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.CongCV, 2));
            int colNghiRo = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.NghiRo, 2));//col15
            int colNghiBHXH = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.BHXH, 2));//col16
            int colPCC3_30 = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._30_dem, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._50_TC, 2));//col19
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._100_TCC3, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._100_LVNN_Ngay, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._150_LVNN_Dem, 2));//col22
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._200_LeTet_Ngay, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._250_LeTet_Dem, 2));
            int colPhuCapKhac = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._Cus, 2));//col25

            int colTongCong = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("SUM({0})-{1}-SUM({2})", 
                ws.Cells[ir, colCong, ir, colNghiBHXH].Address, ws.Cells[ir, colPTDT].Address, ws.Cells[ir, colNghiRo, ir, colNghiBHXH].Address), numFormat: Settings.Default.numFormatFloat101F);// col26TongCong

            int colTongPhuCap = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("SUM({0})", ws.Cells[ir, colPCC3_30, ir, colPhuCapKhac].Address));//col27 Tong PC

            int colLuongTheoHSLSBND205 = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, 
                congthuc: string.Format("({0}/26)*{1}*({2}-{3})", MucLuongToiThieu_ND205, ws.Cells[ir, colHSLCBND205].Address, ws.Cells[ir, colTongCong].Address, ws.Cells[ir, colCongCV].Address), 
                numFormat: Settings.Default.numFormatMoney);//col28LuongCB
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0, numFormat: Settings.Default.numFormatMoney);//col29 Luong Theo HSSP mặc định = 0, write công thức sau
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("({0}/26)*(SUM({1}))*{2}",
               MucLuongTT172135, ws.Cells[ir, colHSLCBTT17, ir, colHSPCCV_TT17].Address, ws.Cells[ir, colCongCV].Address),
                numFormat: Settings.Default.numFormatMoney);//col30 Luong Theo CongCV

            int colLuongDieuChinh = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.chiTietLuong.LuongDieuChinh, 2), numFormat: Settings.Default.numFormatMoney);//col 31 luong dieu chinh 

            int colTongLuongKhongPhuCap = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, 
                congthuc: string.Format("SUM({0})", ws.Cells[ir, colLuongTheoHSLSBND205, ir, colLuongDieuChinh].Address), numFormat: Settings.Default.numFormatMoney);// col32TongLuong

            int colTienPhuCapTheoHSLCB_ND205 = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, 
                congthuc: string.Format("({0}/26)*{1}*{2}", MucLuongToiThieu_ND205, ws.Cells[ir, colHSLCBND205].Address, ws.Cells[ir, colTongPhuCap].Address), 
                numFormat: Settings.Default.numFormatMoney);//col33 PC Theo LuongCB

            int colTienPhuCapTheoHSLSP = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0, numFormat: Settings.Default.numFormatMoney); //col34 Tien Phu Cap theo HSSP, mặc định = 0, write công thức sau

            int colTongTienPhuCap = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("SUM({0})", ws.Cells[ir, colTienPhuCapTheoHSLCB_ND205, ir, colTienPhuCapTheoHSLSP].Address), numFormat: Settings.Default.numFormatMoney);//col 35 tong tien PC

            int colTongTienLuongVaTienPhuCap = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("{0}+{1}", ws.Cells[ir, colTongLuongKhongPhuCap].Address, ws.Cells[ir, colTongTienPhuCap].Address), 
                numFormat: Settings.Default.numFormatMoney);//col36 Tong Luong va PC

            int colTamUng = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.chiTietLuong.KhauTru.TamUng), numFormat: Settings.Default.numFormatMoney);//COL37 tam ung

            int colBHXHYTTN = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("IF(SUM({0})>=14,0,SUM({1})*{2}*(10.5%))", 
                ws.Cells[ir, colNghiRo, ir, colNghiBHXH].Address, ws.Cells[ir, colHSLCBTT17, ir, colHSPCCV_TT17].Address, MucLuongToiThieu_TT17MoiNhat), numFormat: Settings.Default.numFormatMoney); //COL38 BHXH

            int colThuChiKhac = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.chiTietLuong.KhauTru.ThuChiKhac), numFormat: Settings.Default.numFormatMoney);//col39 thu chi khac

            int colTienComTrua = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("IF({0}-({0}/26*{1})<0, 0,{0}-({0}/26*{1}))",
                DinhMucComTrua, ws.Cells[ir, colTienComTrua+5].Address), //số ngày nghỉ RO cách 5 cột nên = cột cơm trưa + 5
                numFormat: Settings.Default.numFormatMoney);//col40
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("{0}-(SUM({1}))+{2}",
                ws.Cells[ir, colTongTienLuongVaTienPhuCap].Address, ws.Cells[ir, colTamUng, ir, colThuChiKhac].Address, ws.Cells[ir, colTienComTrua].Address), 
                numFormat: Settings.Default.numFormatMoney);//col41

            XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1);//ký nhận//col42

            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("{0}*({1}-{2})",
                ws.Cells[ir, colHSLSPND205].Address, ws.Cells[ir, colTongCong].Address, ws.Cells[ir, colCongCV].Address), numFormat: Settings.Default.numFormatFloat30);//col43
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("{0}*{1}",
                ws.Cells[ir, colHSLSPND205].Address, ws.Cells[ir, colTongPhuCap].Address), numFormat: Settings.Default.numFormatFloat30);//col44
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.SoNgayNghiRO_NguyenNgay, numFormat: Settings.Default.numFormatInt);//col44
            XL.FormatCell_W(ws, ref ir, ref ic, /*chu y day la dong cuoi cung phai plusRow*/plusRow: 1, plusCol: 1, value: TenPhong);//col44

                                                                                                                 // phần sum để set colWidth
            sumBKL.ptdt9 += nv.ThongKeThang.PTDT;
			sumBKL.viecrieng10 += nv.ThongKeThang.NghiRo;
			sumBKL.pc100_16 += nv.ThongKeThang.PhuCaps._100_LVNN_Ngay;
			sumBKL.pc160_17 += nv.ThongKeThang.PhuCaps._150_LVNN_Dem;
			sumBKL.pc200_18 += nv.ThongKeThang.PhuCaps._200_LeTet_Ngay;
			sumBKL.pc290_19 += nv.ThongKeThang.PhuCaps._250_LeTet_Dem;
			sumBKL.pcKhac += nv.ThongKeThang.PhuCaps._Cus;
			sumBKL.dieuchinh25 += nv.chiTietLuong.LuongDieuChinh;
			sumBKL.thuchikhac34 += nv.chiTietLuong.KhauTru.ThuChiKhac;
			sumBKL.thuclanh36 += nv.chiTietLuong.ThucLanh;//col39
		}

		public static void ExportSheetTongHopChi(ExcelWorksheet ws, ExcelWorksheet wsBangLuong, DateTime ngaydauthang, DataTable tableThongSo, 
            int rowTong, 
            int colTongLuongCB, int colTongLuongCV,
            int colTongDieuChinhLuong,
            int colTongPCTheoHSLCB,
            int colTongHSLuongCBQuyDoi,
            int colTongHSPhuCapQuyDoi,
            string fullAddressTongLuongCongNhat,
            out int rowTienLuong1SP, out int colTienLuong1SP
            ) {
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
            var luongPTT = tableThongSo.Rows[0]["LuongPTT"] == DBNull.Value ? 0f : (float)tableThongSo.Rows[0]["LuongPTT"];
            var luongTrucLeTetBV = tableThongSo.Rows[0]["LuongTrucLeTetBV"] == DBNull.Value ? 0f : (float)tableThongSo.Rows[0]["LuongTrucLeTetBV"];
            var phuCapTrachNhiem = tableThongSo.Rows[0]["PhuCapTrachNhiem"] == DBNull.Value ? 0f : (float)tableThongSo.Rows[0]["PhuCapTrachNhiem"];
            var quyluongThanhtoan = _80perThanhTien + thanhtienGiaCongNoiBo + thanhtienGiaCongNgoai;
			var quyluongChoviec = (double)tableThongSo.Rows[0]["QuyLuongCV"];
			var quyluongNghidinh = (double)tableThongSo.Rows[0]["QuyLuongNghiDinhCP"];
			var chiKhacTuQuyLuong = (double)tableThongSo.Rows[0]["ChiKhacTuQuyLuong"];
			var quyLuongTheoHeSoSanPham = (double)tableThongSo.Rows[0]["QuyLuongTheoHeSoSanPham"];
			var tienLuong1HeSoSP = (double)tableThongSo.Rows[0]["TienLuong1HeSoSP"];

			// 1. ghi header
			ir = top;
			EXP_Header(ws, ref ir, leftTCT: 2, rightTCT: 6, leftVietNam: 7, rightVietNam:13, leftTitle:2, rightTitle:13,
                ngayLap: DateTime.Today, headerString: string.Format(Settings.Default.titleBangTongHopChiThang, ngaydauthang.ToString("MM/yyyy")));

			ic = left;
            // 2. ghi colTitle ( sản lương đơn giá)
            //XL.FormatCell_T_Merge(ws, ir, ic, bold:);
            // 3. ghi noi dung
			XL.FormatCell_W(ws, ref ir, ref ic, plusRow: 1, value: "Căn cứ để tạm trích lương tháng: ", Bold: true, VeBorder: false);
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Quỹ lương sản phẩm trong tháng(100%): ", VeBorder: false);
            int rowQL_100per = ir, colSL = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, plusCol: 1, value: sanLuong, VeBorder: false, numFormat: Settings.Default.numFormatInt);
			XL.FormatCell_W(ws, ref ir, ref ic, colWidth: 5, plusCol: 1, value: "x", VeBorder: false);
            int colDG = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, plusCol: 1, value: donGia, VeBorder: false, numFormat: Settings.Default.numFormatInt);
			XL.FormatCell_W(ws, ref ir, ref ic, colWidth: 5, plusCol: 1, value: "=", VeBorder: false);
            int colQL_100per = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, plusRow: 1, colWidth: 18, congthuc: string.Format("{0}*{1}", ws.Cells[rowQL_100per, colSL].Address, ws.Cells[rowQL_100per, colDG].Address), VeBorder: false, numFormat: Settings.Default.numFormatMoney);

            ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Quỹ lương sản phẩm để thanh toán lương(" + perTrichQuyLuong + "%): ", Bold: true, VeBorder: false);
            int rowQL_80per = ir, colQL_80per = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, plusRow: 1, colWidth: 18, congthuc: string.Format("{0}*({1}/100)", ws.Cells[rowQL_100per, colQL_100per].Address, perTrichQuyLuong), VeBorder: false, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Quỹ lương gia công nội bộ: ", VeBorder: false);
            int rowQLGCNB = ir, colSLGCNB = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, plusCol: 1, value: sanLuongGiaCongNoiBo, VeBorder: false, numFormat: Settings.Default.numFormatInt);
			XL.FormatCell_W(ws, ref ir, ref ic, colWidth: 5, plusCol: 1, value: "x", VeBorder: false);
            int colDGGCNB = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, plusCol: 1, value: donGiaGiaCongNoiBo, VeBorder: false, numFormat: Settings.Default.numFormatInt);
			XL.FormatCell_W(ws, ref ir, ref ic, colWidth: 5, plusCol: 1, value: "=", VeBorder: false);
            int colQLGCNB = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, plusRow: 1, colWidth: 18, congthuc: string.Format("{0}*{1}", ws.Cells[rowQLGCNB, colSLGCNB].Address, ws.Cells[rowQLGCNB, colDGGCNB].Address), VeBorder: false, numFormat: Settings.Default.numFormatMoney);

            ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Quỹ lương gia công bên ngoài: ", VeBorder: false);
            int rowQLGCNgoai = ir, colSLGCNgoai = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, plusCol: 1, value: sanLuongGiaCongNgoai, VeBorder: false, numFormat: Settings.Default.numFormatInt);
			XL.FormatCell_W(ws, ref ir, ref ic, colWidth: 5, plusCol: 1, value: "x", VeBorder: false);
            int colDGGCNgoai = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, plusCol: 1, value: donGiaGiaCongNgoai, VeBorder: false, numFormat: Settings.Default.numFormatInt);
			XL.FormatCell_W(ws, ref ir, ref ic, colWidth: 5, plusCol: 1, value: "=", VeBorder: false);
            int colQLGCNgoai = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, plusRow: 1, colWidth: 18, congthuc: string.Format("{0}*{1}", 
                ws.Cells[rowQLGCNgoai, colSLGCNgoai].Address, ws.Cells[rowQLGCNgoai, colDGGCNgoai].Address), 
                VeBorder: false, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Tổng quỹ lương thanh toán trong tháng: ", Bold: true, VeBorder: false);
            int rowQLThanhToan = ir, colQLThanhToan = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, congthuc: string.Format("{0}+{1}+{2}", 
                ws.Cells[rowQL_80per, colQL_80per].Address, ws.Cells[rowQLGCNB, colQLGCNB].Address, ws.Cells[rowQLGCNgoai, colQLGCNgoai].Address),
                VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "QUỸ LƯƠNG TRẢ CHO NGƯỜI LAO ĐỘNG ", Bold: true, VeBorder: false);
			XL.FormatCell_N(ws, ref ir, ref ic, plusRow: 1, colWidth: 18, congthuc: string.Format("{0}", ws.Cells[rowQLThanhToan, colQLThanhToan].Address), 
                VeBorder: false, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusRow: 1, value: "TRONG ĐÓ: ", VeBorder: false);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Quỹ lương theo nghị định 205/CP ", Bold: true, VeBorder: false);
            int rowQLND205 = ir, colQLND205 = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18,congthuc: string.Format("{0}+{1}",
                wsBangLuong.Cells[rowTong, colTongLuongCB].FullAddress, wsBangLuong.Cells[rowTong, colTongPCTheoHSLCB].FullAddress),
                VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Quỹ lương công nhật, thời vụ, khoán việc ", VeBorder: false);
            int rowLuongCongNhat = ir, colLuongCongNhat = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);
            if (fullAddressTongLuongCongNhat == string.Empty) { ws.Cells[ir - 1, ic].Value = 0; } // vì format cell đã làm ir tăng lên 1 nên phải - 1 để trả lại dòng đúng
            else ws.Cells[ir - 1, ic].Formula = fullAddressTongLuongCongNhat;

            ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Chi khác từ quỹ lương ", Bold:true, VeBorder: false);
            int rowChiKhac = ir, colChiKhac = ic;
            XL.FormatCell_N(ws, ref ir, ref ic, plusRow: 1, colWidth: 18,
                congthuc: string.Format("SUM({0})", ws.Cells[rowChiKhac + 1, colChiKhac, rowChiKhac + 5, colChiKhac].Address), // chi khác = tổng các khoản chi bên dưới dòng này, hiện tại là 5 khoản
                VeBorder: false, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Lương chờ việc: ", VeBorder: false);
            int rowQLChoViec = ir, colQLChoViec = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, congthuc: string.Format("{0}", wsBangLuong.Cells[rowTong, colTongLuongCV].FullAddress),
                VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Lương phụ cấp trách nhiệm: ", VeBorder: false);
            int rowLuongPCTN = ir, colLuongPCTN = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: Math.Round(phuCapTrachNhiem), VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Lương cho phòng Thị trường: ", VeBorder: false);
            int rowLuongPTT = ir, colLuongPTT = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: Math.Round(luongPTT), VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Lương trực lễ bảo vệ: ", VeBorder: false);
            int rowLuongTrucLeBV = ir, colLuongTrucLeBV = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, value: Math.Round(luongTrucLeTetBV), VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Điều chỉnh lương tháng trước và truy lãnh lương ", VeBorder: false);
            int rowDieuChinhLuong = ir, colDieuChinhLuong = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, colWidth: 18, congthuc: string.Format("{0}", wsBangLuong.Cells[rowTong, colTongDieuChinhLuong].FullAddress),
                VeBorder: false, plusRow: 1, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "Quỹ lương theo hệ số (sản phẩm) ", Bold: true, VeBorder: false);
            int rowQLSP = ir, colQLSP = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, plusRow: 1, colWidth: 18, congthuc: string.Format("{0}-{1}-{2}-{3}",
                ws.Cells[rowQLThanhToan, colQLThanhToan].Address, ws.Cells[rowQLND205, colQLND205].Address, ws.Cells[rowChiKhac, colChiKhac].Address, ws.Cells[rowLuongCongNhat, colLuongCongNhat].Address),
                VeBorder: false, numFormat: Settings.Default.numFormatMoney);

			ic = left;
			XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 7, value: "---> Tiền lương 1 hệ số (sản phẩm) ", Bold: true, VeBorder: false);
            rowTienLuong1SP = ir; colTienLuong1SP = ic;
			XL.FormatCell_N(ws, ref ir, ref ic, plusRow: 1, colWidth: 18, congthuc: string.Format("{0}/({1}+{2})",
                ws.Cells[rowQLSP, colQLSP].Address, 
                wsBangLuong.Cells[rowTong, colTongHSLuongCBQuyDoi].FullAddress, 
                wsBangLuong.Cells[rowTong, colTongHSPhuCapQuyDoi].FullAddress),
                VeBorder: false, numFormat: Settings.Default.numFormatMoney);

			//4. ghi footer
			ir = ir + 2;
			EXP_Footer(ws, ref ir,
	new int[] { 3, 10 },
	new int[] { 13, 13 },
	new string[] { "GIÁM ĐỐC", "LẬP BIỂU" },
	new string[] { Settings.Default.LastTenGD, Settings.Default.LastTenNVLapBieuLuong }, khoangCachTen_ChucVu:9);

		}

		public static void ExportSheetBangKetcongThang(ExcelWorksheet ws, DateTime ngaybd, DateTime ngaykt, List<cUserInfo> dsnv, string tenNVLapBieu, string tenTrgBP, params int[] p) {

			int stt = 1, left = 1, top = 1, ir, ic, index = 0;

			// 1. ghi header
			//string headerString = string.Format(Settings.Default.titleBangCC_THANG, ngaybd.ToString("MM/yyyy"));
			string headerString = string.Format(Settings.Default.titleBangCC_THANG, ngaykt.ToString("MM/yyyy"));//v4.7
			
			ir = top;
            EXP_Header(ws, ref ir, leftTCT: 1, rightTCT: 15, leftVietNam: 30, rightVietNam: 48, leftTitle: 1, rightTitle: 48, sizeTCT: 22, sizeVietNam: 18, sizeTitle: 22,
                       ngayLap: DateTime.Today, headerString: headerString);

            #region // 2. ghi colTitle
            ic = left;
            FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.STT, value: "STT", fromRow: ir, toRow: ir + 1, rowContainValue:ir+1, plusCol: 1); //col 1
            FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.manv, value: "Mã NV", fromRow: ir, toRow: ir + 1, rowContainValue:ir+1, plusCol: 1);
			FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.ten, value: "Họ tên", fromRow: ir, toRow: ir + 1, rowContainValue:ir+1, plusCol: 1);
			var songay = Convert.ToInt32((ngaykt - ngaybd).TotalDays) + 1; // 1->31: songay = 30; 1-> 30: songay = 29 ==> phải +1 phía sau Convert.ToInt32((ngaykt - ngaybd).TotalDays)
            int irBelow = ir + 1;
            FormatCell_TCAS201903(ws, ref ir, ref ic, value: "Ngày", fromCol: ic, toCol: (ic + songay - 1)); //vì ic + songay = vị trí ô kế tiếp  
			for (DateTime indexNgay = ngaybd; indexNgay <= ngaykt; indexNgay = indexNgay.AddDays(1d)) {
				FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.congtrongngay, value: indexNgay, plusCol: 1, numFormat: "d");
			}
			//ic mội lần lặp đều tăng nên ic hiện tại là vị trí của cột sau ngày cuối tháng
			int icStartSection = ic; // giữ lại vị trí cột đầu để format merge dòng trên , ic lúc này là cột kế bên cột ngày cuối tháng
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.cong, value: "Công", plusCol: 1); //col 1
			FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.hoc, value: "Học, họp, CT, PT", plusCol: 1);//DANGLAM PTDT CHUYỂN RA SAU RO
			FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.Le, value: "Lễ", plusCol: 1);
			FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.Phep, value: "Phép Chế độ", plusCol: 1);
			FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.cv, value: "Chờ việc", plusCol: 1);
            // write ngày công chuẩn ở trên
            FormatCell_TCAS201903(ws, ref ir, ref icStartSection, colWidth: (int)CC.cong, value: "Chấm công", fromCol: icStartSection, toCol: ic-1); //5 cột trừ cột hiện tại đang đứng

			icStartSection = ic;
            //write số công chuẫn ở trên
            //var socongchuan = DateTime.DaysInMonth(ngaybd.Year, ngaybd.Month) - DemSoNgayNghiChunhat(ngaybd, true, false);
            var temp1 = ngaykt - ngaybd;
            //var socongchuan = (float)temp1.TotalDays + 1f - (float)XL.DemSoNgayNghiChunhat(ngaybd, ngaykt, true, false); //v4.7

            FormatCell_T(ws, ref ir, ref ic, plusCol:0, colWidth: (int)CC.tongcong, value: "");// không tăng column
            //irtemp = ir + 1; //dòng dưới của dòng Ngày - Công chuẩn
			FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.tongcong, value: "Tổng công", plusCol: 1);

			icStartSection = ic; //giữ lại vị trí bắt đầu merge
			FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.ca130, value: string.Format("Ca 3", p[0]), plusCol: 1);
			FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.ca150, value: string.Format("Tăng cường", p[1]), plusCol: 1);
			FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.tcc3_100, value: string.Format("Tăng cường ca 3", p[2]), plusCol: 1, size: 10);
			FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.LVNN, value: string.Format("Ng.Nghỉ hàng tuần", p[3]), plusCol: 1, size: 10);
			FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pcCa3LVNN, value: string.Format("Ca 3 Ng.Nghỉ h.tuần", p[4]), plusCol: 1, size:10);
			FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pcle_tet, value: string.Format("Lễ, tết", p[5]), plusCol: 1);
			FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pcCa3_le, value: string.Format("Ca 3 Lễ, tết", p[6]), plusCol: 1);
			FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pckhac, value: "Phụ cấp", plusCol: 1);
            //write phụ cấp ở trên
            FormatCell_TCAS201903(ws, ref ir, ref icStartSection, colWidth: (int)CC.cong, value: "Phụ cấp", fromCol: icStartSection, toCol: ic - 1);
            //write tổng PC
			FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.pckhac, value: "Tổng Phụ cấp", fromRow:ir, toRow:irBelow, rowContainValue:irBelow, plusCol: 1);

            icStartSection = ic; //giữ lại vị trí bắt đầu merge
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.bh, value: "BHXH", plusCol: 1);
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.nghiRo, value: "Việc riêng", plusCol: 1);
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.nghiRo, value: "PT Đoàn thể chi", plusCol: 1);
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: 0, value: "TrS_VR", plusCol: 1); //width=0: ẩn cột này
            FormatCell_TCAS201903(ws, ref ir, ref icStartSection, colWidth: (int)CC.cong, value: "Vắng không tính công", fromCol: icStartSection, toCol: ic - 1, size: 10);
            int indexLastColumn = ic - 1;
			ws.Row(ir + 1).Height = 50d;

            #endregion
            #region ghi cột STT cột để dễ tính excel
            int iRowSTTCot = ir - 1;
            int iStartSection = 2;
            int iEndSection = ic - 1;
            index = 0;
            for (index = 0; (iStartSection + index) <= iEndSection; index++) //iTemp = 2 vì bỏ qua cột STT đầu
            {
                ws.Cells[iRowSTTCot, (iStartSection + index)].Style.Font.Color.SetColor(Color.White);
                ws.Cells[iRowSTTCot, (iStartSection + index)].Value = (index + 1);
            }
            #endregion


            //3. ghi noi dung
            SUMCC sumCC = new SUMCC();
			ir = ir + 2;// 2 dòng title
			int irSum = ir;
			ic = left;

			var dsphong = (from nv in dsnv.Where(o => o.NVNhanKiet == false && o.LoaiCN != LoaiCongNhat.NVCongNhat)
						   group nv by nv.PhongBan into @group
						   orderby @group.Key.ViTri
						   select @group).ToList();
			int sumCol_Pos = 0;
            if (dsphong.Count == 0) return;
            int rowStartSectionNameRange = ir, colStartSectionNameRange = 2;
			foreach (var item in dsphong) {
				var phong = item.Key;
				List<cUserInfo> dsnv1 = dsnv.Where(o => o.NVNhanKiet == false && o.PhongBan.ID == phong.ID).ToList();
				XL.EXP_group_KetcongThang(ws, ref stt, ref ir, ref ic, dsnv1, phong, true, ref sumCC, out sumCol_Pos);
				ic = left;
				//ir++;
			}
			// ghi dòng tổng cộng
			int t1 = irSum, bottom = ir - 1; //top + 1 vì top là dòng phòng ban, sum từ dòng kế tiếp; bottom -1 vì bottom = ir là index dòng kế
			//XL.FormatCell_W(ws, ir, 30, "Tổng cộng: ", VeBorder: false);
			//FormatCell_T_Merge(ws, ir, ic, value: "Tổng cộng", fromRow: ir, fromCol: left, toRow: ir, toCol: sumCol_Pos - 1, wrapText: false, hAlign: ExcelHorizontalAlignment.Right);//ver 4.0.0.4	
            int currCol = sumCol_Pos - 1;
            FormatCell_TCAS201903(ws, ref ir, ref currCol, wrapText:false, value: "Tổng cộng: ", fromCol: left, toCol: sumCol_Pos - 1, hAlign: ExcelHorizontalAlignment.Right);

			int ic5 = sumCol_Pos;
			// ghi phần tổng kết
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //Cong);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //H_CT_PT);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //Le);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //Phep);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //CongCV);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //Tổng công;
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._30_dem);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._50_TC);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._100_TCC3);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._100_LVNN_Ngay);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._150_LVNN_Dem);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._200_LeTet_Ngay);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._250_LeTet_Dem);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._Cus);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //Tổng PC;
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //BHXH);//DANGLAM
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //NghiRo);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PTDT);
			FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //TreSomVR);

			//formatNumber
			ic5 = sumCol_Pos;
			//XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat20); //hslcb// t1, ic5, bottom + 1 - 1 vì t1, ic5, bottom + 1 là index của dòng mới
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//Cong);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat10);//H_CT_PT);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat10);//Le);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//Phep);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//CongCV);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//Tổng công
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._30_dem);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._50_TC);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._100_TCC3);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._100_LVNN_Ngay);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._150_LVNN_Dem);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._200_LeTet_Ngay);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._250_LeTet_Dem);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._Cus);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//Tổng PC;
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat10);//BHXH);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//NghiRo);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PTDT);
			XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//TrS_VR);


			#region // set ColWidth = 0 các cột ko cần thiết
            /*test 201903
			//if (Math.Abs(sumCC.le2 - 0f) < 0.01f) ws.Column(3 + songay + 2).Width = 0; // 3 cột stt manv tennv
			//if (Math.Abs(sumCC.bhxh5 - 0f) < 0.01f) ws.Column(3 + songay + 5).Width = 0;
			//if (Math.Abs(sumCC.pc100_11 - 0f) < 0.01f) ws.Column(3 + songay + 11).Width = 0;
			//if (Math.Abs(sumCC.pc160_12 - 0f) < 0.01f) ws.Column(3 + songay + 12).Width = 0;
			//if (Math.Abs(sumCC.pc200_13 - 0f) < 0.01f) ws.Column(3 + songay + 13).Width = 0;
			//if (Math.Abs(sumCC.pc290_14 - 0f) < 0.01f) ws.Column(3 + songay + 14).Width = 0;
			//if (Math.Abs(sumCC.pckhac_15 - 0f) < 0.01f) ws.Column(3 + songay + 15).Width = 0;
			//if (Math.Abs(sumCC.nghiRO_16 - 0f) < 0.01f) ws.Column(3 + songay + 16).Width = 0;
			//if (Math.Abs(sumCC.ptdt_17 - 0f) < 0.01f) ws.Column(3 + songay + 17).Width = 0;
            201903*/
            #endregion
            // ghi các phần công nhật
            ir++;
			XL.FormatCell_W(ws, ref ir, ref ic, value: "Nhân viên làm việc công nhật và hợp đồng", plusRow: 1, Bold: true, VeBorder: false); // ghi dòng phòng ban 
			foreach (var nv in dsnv.Where(o => (o.NVNhanKiet == false && (o.LoaiCN == LoaiCongNhat.NVCongNhat || o.LoaiCN == LoaiCongNhat.NVCongNhatVaChinhThuc)) ) ) {
				ic5 = left;
				EXP_record_KetcongThang(ws, ref stt, ref ir, ref ic5, nv, false, ref sumCC, out sumCol_Pos);
				stt++;
			}
            //func3(ws, ref ir, ref ic5);
            int rowEndSectionNameRange = ir, colEndSectionNameRange = ic5;

            ws.Workbook.Names.Add("BangKetCongRange", ws.Cells[rowStartSectionNameRange, colStartSectionNameRange, rowEndSectionNameRange, indexLastColumn]);
            //4. ghi footer
            var tableShift = DAO.LayDSCa();
			var tableAbsent = DAO.LayDSLoaiVang();
			ir = ir + 2;// cách 2 dòng
			EXP_Footer(ws, ref ir,
	new int[] { 4, 22, 39 },
	new int[] { 16, 16, 16 },
	new string[] { "LẬP BIỂU", "TRƯỞNG BỘ PHẬN", "PHÓ GIÁM ĐỐC" },
	new string[] { tenNVLapBieu, tenTrgBP, Settings.Default.LastTenPhoGD }, khoangCachTen_ChucVu: Settings.Default.cachDongBKC_LapBieu_ten);

			ir = ir + Settings.Default.cachDongBKC_LapBieu_ten + 2;// cách 2 dòng
			EXP_KyHieuCC(ws, ref ir, new int[] { 3, 4, 22, 39 }, out ir, tableShift, tableAbsent);

		}

        public static void ExportSheetBangKetcongThangNhanKiet(ExcelWorksheet ws, DateTime ngaybd, DateTime ngaykt, List<cUserInfo> dsnv, string tenNVLapBieu, string tenTrgBP, params int[] p)
        {
            int stt = 1, left = 1, top = 1, ir, ic;

            // 1. ghi header
            string headerString = string.Format(Settings.Default.titleBangCC_THANG, ngaybd.ToString("MM/yyyy"));
            headerString += " CỦA CÔNG TY NHÂN KIỆT";
            ir = top;
            EXP_Header(ws, ref ir, leftTCT: 1, rightTCT: 15, leftVietNam: 30, rightVietNam: 48, leftTitle: 1, rightTitle: 48, sizeTCT: 22, sizeVietNam: 18, sizeTitle: 22,
                       ngayLap: DateTime.Today, headerString: headerString);


            #region // 2. ghi colTitle
            ic = left;
            FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.STT, value: "STT", fromRow: ir, toRow: ir + 1, rowContainValue: ir + 1, plusCol: 1); //col 1
            FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.manv, value: "Mã NV", fromRow: ir, toRow: ir + 1, rowContainValue: ir + 1, plusCol: 1);
            FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.ten, value: "Họ tên", fromRow: ir, toRow: ir + 1, rowContainValue: ir + 1, plusCol: 1);
            var songay = Convert.ToInt32((ngaykt - ngaybd).TotalDays) + 1; // 1->31: songay = 30; 1-> 30: songay = 29 ==> phải +1 phía sau Convert.ToInt32((ngaykt - ngaybd).TotalDays)
            int irBelow = ir + 1;
            FormatCell_TCAS201903(ws, ref ir, ref ic, value: "Ngày", fromCol: ic, toCol: (ic + songay - 1)); //vì ic + songay = vị trí ô kế tiếp  
            for (DateTime indexNgay = ngaybd; indexNgay <= ngaykt; indexNgay = indexNgay.AddDays(1d))
            {
                FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.congtrongngay, value: indexNgay, plusCol: 1, numFormat: "d");
            }
            //ic mội lần lặp đều tăng nên ic hiện tại là vị trí của cột sau ngày cuối tháng
            int icStartSection = ic; // giữ lại vị trí cột đầu để format merge dòng trên , ic lúc này là cột kế bên cột ngày cuối tháng
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.cong, value: "Công", plusCol: 1); //col 1
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.cv, value: "Lễ", plusCol: 1);
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.cv, value: "Phép", plusCol: 1);
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.cv, value: "Chờ việc", plusCol: 1);
            // write ngày công chuẩn ở trên
            FormatCell_TCAS201903(ws, ref ir, ref icStartSection, colWidth: (int)CC.cong, value: "Chấm công", fromCol: icStartSection, toCol: ic - 1); //5 cột trừ cột hiện tại đang đứng

            icStartSection = ic;
            //write số công chuẫn ở trên
            //var socongchuan = DateTime.DaysInMonth(ngaybd.Year, ngaybd.Month) - DemSoNgayNghiChunhat(ngaybd, true, false);
            var socongchuan = (float)(ngaykt - ngaybd).TotalDays + 1f - XL.DemSoNgayNghiChunhat(ngaybd, ngaykt, true, false); //v4.7

            FormatCell_T(ws, ref ir, ref ic, plusCol: 0, colWidth: (int)CC.tongcong, value: "");// không tăng column
                                                                                                         //irtemp = ir + 1; //dòng dưới của dòng Ngày - Công chuẩn
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.tongcong, value: "Tổng công", plusCol: 1);

            icStartSection = ic; //giữ lại vị trí bắt đầu merge
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.ca130, value: string.Format("Ca 3", p[0]), plusCol: 1);
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.ca150, value: string.Format("Tăng cường", p[1]), plusCol: 1);
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.tcc3_100, value: string.Format("Tăng cường ca 3", p[2]), plusCol: 1);
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.LVNN, value: string.Format("Ng.Nghỉ hàng tuần", p[3]), size: 10, plusCol: 1);
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pcCa3LVNN, value: string.Format("Ca 3 Ng.Nghỉ h.tuần", p[4]), size: 10, plusCol: 1);
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pcle_tet, value: string.Format("Lễ, tết", p[5]), plusCol: 1);
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pcCa3_le, value: string.Format("Ca 3 Lễ, tết", p[6]), plusCol: 1);
            FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pckhac, value: "Phụ cấp", plusCol: 1);
            //write phụ cấp ở trên
            FormatCell_TCAS201903(ws, ref ir, ref icStartSection, colWidth: (int)CC.cong, value: "Phụ cấp", fromCol: icStartSection, toCol: ic - 1);
            //write tổng PC
            FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.pckhac, value: "Tổng Phụ cấp", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);

            FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.cong, value: "Vắng không tính công", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1, size: 10);
            FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: 0, value: "TrS_VR", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);//colWidth=0: ẩn cột này
            ws.Row(ir + 1).Height = 50d;

            #endregion


            //3. ghi noi dung
            SUMCC sumCC = new SUMCC();
            ir = ir + 2;// 2 dòng title
            int irSum = ir;
            ic = left;

            var dsphong = (from nv in dsnv.Where(o => o.NVNhanKiet == true)
                           group nv by nv.PhongBan into @group
                           orderby @group.Key.ViTri
                           select @group).ToList();
            int sumCol_Pos = 0;
            foreach (var item in dsphong) {
                var phong = item.Key;
                List<cUserInfo> dsnv1 = dsnv.Where(o => o.NVNhanKiet == true && o.PhongBan.ID == phong.ID).ToList();
                XL.EXP_group_KetcongThangNhanKiet(ws, ref stt, ref ir, ref ic, dsnv1, phong, ref sumCC, out sumCol_Pos);
                ic = left;
                //ir++;
            }
            // ghi dòng tổng cộng
            int t1 = irSum, bottom = ir - 1; //top + 1 vì top là dòng phòng ban, sum từ dòng kế tiếp; bottom -1 vì bottom = ir là index dòng kế
                                             //XL.FormatCell_W(ws, ir, 30, "Tổng cộng: ", VeBorder: false);
            int currCol = sumCol_Pos - 1;
            FormatCell_TCAS201903(ws, ref ir, ref currCol, wrapText: false, value: "Tổng cộng: ", fromCol: left, toCol: sumCol_Pos - 1, hAlign: ExcelHorizontalAlignment.Right);

            int ic5 = sumCol_Pos;
            // ghi phần tổng kết
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //Cong);
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //Lễ);
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //Vắng phép
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //CongCV);
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //Tổng công;
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._30_dem);
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._50_TC);
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._100_TCC3);
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._100_LVNN_Ngay);
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._150_LVNN_Dem);
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._200_LeTet_Ngay);
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._250_LeTet_Dem);
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //PhuCaps._Cus);
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //Tổng PC;
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //Tổng ngày nghỉ ro
            FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, bold:true, italic:true, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //TreSomVR

            //formatNumber
            ic5 = sumCol_Pos;
            //XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat20); //hslcb// t1, ic5, bottom + 1 - 1 vì t1, ic5, bottom + 1 là index của dòng mới
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//Cong);
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//Lễ);
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//Vắng Phép);
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//CongCV);
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//Tổng công;
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._30_dem);
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._50_TC);
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._100_TCC3);
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._100_LVNN_Ngay);
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._150_LVNN_Dem);
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._200_LeTet_Ngay);
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._250_LeTet_Dem);
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//PhuCaps._Cus);
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//Tổng PC
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//NghiRo);
            XL.FormatNumber(ws, ref t1, ref ic5, t1, ic5, bottom + 1, ic5, plusCol: 1, numberFormat: Settings.Default.numFormatFloat101F);//TreSomVR

            //4. ghi footer
            var tableShift = DAO.LayDSCa();
            var tableAbsent = DAO.LayDSLoaiVang();
            ir = ir + 2;// cách 2 dòng
            EXP_Footer(ws, ref ir,
    new int[] { 4, 22, 39 },
    new int[] { 16, 16, 16 },
    new string[] { "LẬP BIỂU", "TRƯỞNG BỘ PHẬN", "PHÓ GIÁM ĐỐC" },
    new string[] { tenNVLapBieu, tenTrgBP, Settings.Default.LastTenPhoGD }, khoangCachTen_ChucVu: Settings.Default.cachDongBKC_LapBieu_ten);

            ir = ir + Settings.Default.cachDongBKC_LapBieu_ten + 2;// cách 2 dòng
            EXP_KyHieuCC(ws, ref ir, new int[] { 3, 4, 22, 39 }, out ir, tableShift, tableAbsent);

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
			func3(ws, ref irSum, ref ic, t1, bottom); //H_CT_PT);
			func3(ws, ref irSum, ref ic, t1, bottom); //Le);
			func3(ws, ref irSum, ref ic, t1, bottom); //Phep);
			func3(ws, ref irSum, ref ic, t1, bottom); //CongCV);
			func3(ws, ref irSum, ref ic, t1, bottom); //Tổng công
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._30_dem);
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._50_TC);
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._100_TCC3);
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._100_LVNN_Ngay);
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._150_LVNN_Dem);
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._200_LeTet_Ngay);
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._250_LeTet_Dem);
			func3(ws, ref irSum, ref ic, t1, bottom); //PhuCaps._Cus);
			func3(ws, ref irSum, ref ic, t1, bottom); //Tổng PC
			func3(ws, ref irSum, ref ic, t1, bottom); //BHXH);
			func3(ws, ref irSum, ref ic, t1, bottom); //Nghiro);
			func3(ws, ref irSum, ref ic, t1, bottom); //PTDT
			func3(ws, ref irSum, ref ic, t1, bottom); //TreSomVR

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
                switch (ngay.Ngay.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                        ws.Cells[ir, ic].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[ir, ic].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        break;
                    case DayOfWeek.Sunday:
                        ws.Cells[ir, ic].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[ir, ic].Style.Fill.BackgroundColor.SetColor(Color.DarkGray);
                        break;
                    default:
                        break;
                }
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
            int icStartSection = 0, icEndSection;
			if (XuatPhanKetCongChinhThuc) {
                icStartSection = ic;
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.Cong, 2));
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.H_CT_PT, 2));
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.Le, 2));
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.Phep, 2));
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.CongCV, 2));
                icEndSection = ic - 1;
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("SUM({0})", ws.Cells[ir, icStartSection, ir, icEndSection].Address));

                icStartSection = ic;
                XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._30_dem, 2));
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._50_TC, 2));
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._100_TCC3, 2));
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._100_LVNN_Ngay, 2));
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._150_LVNN_Dem, 2));
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._200_LeTet_Ngay, 2));
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._250_LeTet_Dem, 2));
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._Cus, 2));
                icEndSection = ic - 1;
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("SUM({0})", ws.Cells[ir, icStartSection, ir, icEndSection].Address));

				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.BHXH, 2));
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.NghiRo, 2));
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PTDT, 2));
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.TongTruCongTreVR + nv.ThongKeThang.TongTruCongSomVR + nv.ThongKeThang.TreSom_KoDuBuCong, 2));

            }
            else {
                icStartSection = ic;
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.Cong_Congnhat,2)); //nv.ThongKeThang.Cong);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.H_CT_PT, 2)); //nv.ThongKeThang.H_CT_PT);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.Le);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.Phep, 2)); //nv.ThongKeThang.Phep);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.CongCV);
                icEndSection = ic - 1;
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("SUM({0})", ws.Cells[ir, icStartSection, ir, icEndSection].Address)); //Tổng công

                icStartSection = ic;
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._30_dem);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._50_TC);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._100_TCC3);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._100_LVNN_Ngay);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._150_LVNN_Dem);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._200_LeTet_Ngay);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._250_LeTet_Dem);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PhuCaps._Cus);
                icEndSection = ic - 1;
                XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("SUM({0})", ws.Cells[ir, icStartSection, ir, icEndSection].Address));//Tổng PC

				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.BHXH);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.NghiRo);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //nv.ThongKeThang.PTDT);
				XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: 0f); //TreSomVR);

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

        public static void EXP_group_KetcongThangNhanKiet(ExcelWorksheet ws, ref int stt, ref int top, ref int left, List<cUserInfo> dsnv, cPhongBan phong, ref SUMCC sumCC, out int sumCol_Pos)
        {
            int ir = top, ic = left;
            sumCol_Pos = 0;
            XL.FormatCell_W(ws, ref ir, ref ic, value: phong.Ten, plusRow: 1, Bold: true, VeBorder: false); // ghi dòng phòng ban 
            if (dsnv.Count == 0) return; //v9 neu 1 phong ko co nhan vien thi ko ghi sum thi dong tong cong cuoi thi sao?
            foreach (cUserInfo nv in dsnv) {
                ic = left;//reset về cột đầu tiên  dòng tiếp theo
                EXP_record_KetcongThangNhanKiet(ws, ref stt, ref ir, ref ic, nv, ref sumCC, out sumCol_Pos);
                stt++;
            }

            int t1 = top + 1, bottom = ir - 1; //top + 1 vì top là dòng phòng ban, sum từ dòng kế tiếp; bottom -1 vì bottom = ir là index dòng kế
                                               // ghi phần sum tổng kết
            int irSum = top; // top group
            ic = sumCol_Pos;
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //Cong);
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //Lễ);
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //Vắng Phép
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //CongCV);
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //Tổng Công
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //PhuCaps._30_dem);
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //PhuCaps._50_TC);
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //PhuCaps._100_TCC3);
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //PhuCaps._100_LVNN_Ngay);
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //PhuCaps._150_LVNN_Dem);
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //PhuCaps._200_LeTet_Ngay);
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //PhuCaps._250_LeTet_Dem);
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //PhuCaps._Cus);
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //Tổng PC
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //Nghỉ RO
            func3(ws, ref irSum, ref ic, t1, bottom, bold: true, italic: true); //TrS_VR

            top = ir;// xuất xong 1 group rồi thì cập nhật lại top là con trỏ dòng kế tiếp
        }
        public static void EXP_record_KetcongThangNhanKiet(ExcelWorksheet ws, ref int stt, ref int top, ref int left, cUserInfo nv, ref SUMCC sumCC, out int sumCol_Position)
        {
            int ir = top, ic = left;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: stt);
            XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, value: nv.MaNV);
            XL.FormatCell_W(ws, ref ir, ref ic, plusCol: 1, value: nv.TenNV);

            // ghi ký hiệu từng ngày công
            foreach (cNgayCong ngay in nv.DSNgayCong) {
                var temp = string.Empty;
                switch (ngay.Ngay.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                        ws.Cells[ir, ic].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[ir, ic].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        break;
                    case DayOfWeek.Sunday:
                        ws.Cells[ir, ic].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[ir, ic].Style.Fill.BackgroundColor.SetColor(Color.DarkGray);
                        break;
                    default:
                        break;
                }

                ngay.XuatChuoiKyHieuChamCong(ref temp);
                XL.FormatCell_W(ws, ir, ic, value: temp, wrapText: true);
                ic++;// cập nhật lại vị trí cho col kế tiếp
            }
            // ghi phần thống kê tháng các cột cuối
            sumCol_Position = ic;
            int icStartSection = 0, icEndSection;
            icStartSection = ic; 
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.Cong, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.Le, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.Phep, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.CongCV, 2));
            icEndSection = ic - 1;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("SUM({0})", ws.Cells[ir, icStartSection, ir, icEndSection].Address));

            icStartSection = ic; 
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._30_dem, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._50_TC, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._100_TCC3, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._100_LVNN_Ngay, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._150_LVNN_Dem, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._200_LeTet_Ngay, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._250_LeTet_Dem, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.PhuCaps._Cus, 2));
            icEndSection = ic - 1;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, congthuc: string.Format("SUM({0})", ws.Cells[ir,icStartSection,ir,icEndSection].Address));//tổng PC

            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.NghiRo, 2));
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, value: Math.Round(nv.ThongKeThang.TongTruCongTreVR + nv.ThongKeThang.TongTruCongSomVR + nv.ThongKeThang.TreSom_KoDuBuCong, 2));
                        
 
            //sum để set colWidth
            //sumCC.le2 += nv.ThongKeThang.Le;
            sumCC.pc100_11 += nv.ThongKeThang.PhuCaps._100_LVNN_Ngay;
            sumCC.pc160_12 += nv.ThongKeThang.PhuCaps._150_LVNN_Dem;
            sumCC.pc200_13 += nv.ThongKeThang.PhuCaps._200_LeTet_Ngay;
            sumCC.pc290_14 += nv.ThongKeThang.PhuCaps._250_LeTet_Dem;
            sumCC.pckhac_15 += nv.ThongKeThang.PhuCaps._Cus;

            ir++;
            top = ir; // cập nhật lại index cho row kế tiếp
        }


        public static void ExportSheetBangThongKeSua(ExcelWorksheet ws, DateTime ngaybd, DateTime ngaykt, List<cUserInfo> dsnv) {

			int stt = 1, left = 1, top = 1, ir, ic;
			ir = top;
			ic = left;
			FormatCell_T_Merge(ws, ref ir, ref ic, value: string.Format("BẢNG THỐNG KÊ CHI CHẾ ĐỘ BỒI DƯỠNG ĐỘC HẠI BẰNG HIỆN VẬT THÁNG {0}/{1}", ngaybd.Month, ngaybd.Year), wrapText: false, size: 16, bold: true, VeBorder: false, merge: false, plusRow: 1, hAlign: ExcelHorizontalAlignment.Left);

			#region // 2. ghi colTitle
			ic = left;
            int irBelow = ir + 1;
			FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.STT, value: "STT", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1); //col 1
            FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.manv, value: "Mã NV", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);
            FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.ten, value: "Họ tên", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);
			var songay = Convert.ToInt32((ngaykt - ngaybd).TotalDays) + 1; // 1->31: songay = 30; 1-> 30: songay = 29 ==> phải +1 phía sau Convert.ToInt32((ngaykt - ngaybd).TotalDays)                                                                              
            FormatCell_TCAS201903(ws, ref ir, ref ic, value: "Ngày", fromCol: ic, toCol: ic + songay - 1);//vì ic + songay = vị trí ô kế tiếp
			for (DateTime indexNgay = ngaybd; indexNgay <= ngaykt; indexNgay = indexNgay.AddDays(1d)) {
				FormatCell_T(ws, ref irBelow, ref ic, colWidth: 0, value: indexNgay, plusCol: 1, numFormat: "d");
			}
			//ir = ir - 1;
            FormatCell_TCAS201903(ws, ref ir, ref ic, value: "Ngày", fromCol: ic, toCol: ic + songay - 1); //vì ic + songay = vị trí ô kế tiếp  
			for (DateTime indexNgay = ngaybd; indexNgay <= ngaykt; indexNgay = indexNgay.AddDays(1d)) {
				FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.congtrongngay, value: indexNgay, plusCol: 1, numFormat: "d");
			}
			//ir = ir - 1; // vì "Ngày" đã làm tăng ir lên 1
			//ic mội lần lặp đều tăng nên ic hiện tại là vị trí của cột sau ngày cuối tháng

			#endregion


			//3. ghi noi dung
			SUMCC sumCC = new SUMCC();
			ir = ir + 2;// 2 dòng title
			int irSum = ir;
			ic = left;

			var dsphong = (from nv in dsnv//.Where(o => o.LoaiCN != LoaiCongNhat.NVCongNhat || o.NVNhanKiet == true)
						   group nv by nv.PhongBan into @group
						   orderby @group.Key.ViTri
						   select @group).ToList();
			int sumCol_Pos = 0;
			foreach (var item in dsphong) {
				var phong = item.Key;
				List<cUserInfo> dsnv1 = dsnv.Where(o => o.PhongBan.ID == phong.ID).ToList();
				//List<cUserInfo> dsnv1 = dsnv.Where(o => (o.LoaiCN != LoaiCongNhat.NVCongNhat || o.NVNhanKiet == true) && o.PhongBan.ID == phong.ID).ToList();
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
				FormatCell_N(ws, ref ir, ref ic5, plusCol: 1, congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5, bottom, ic5].Address + ")"); //Cong);
				FormatCell_N(ws, ref ir, ref ic5_2, /*plusCol: 1, */congthuc: "SUBTOTAL(9," + ws.Cells[t1, ic5_2, bottom, ic5_2].Address + ")"); //ko can plusCol vì ic5 đã plusCol rồi
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
				var temp = ws.Cells[ir, colGio, ir, colGio].Address;
				if (nv.NgayBDCongnhat != DateTime.MinValue && ngay.Ngay >= nv.NgayBDCongnhat && ngay.Ngay <= nv.NgayKTCongnhat)
					XL.FormatCell_N(ws, ir, ic);
				else
					XL.FormatCell_N(ws, ir, ic, value: ngay.TG.GioLamViec.TotalHours);//info ko bỏ totalHours vì phải là số mới thực hiện hàm if được
				XL.FormatCell_N(ws, ir, ic + songay, congthuc: string.Format("IF({0}<2,0,IF({0}<12,1,IF({0}<20,2,3)))", temp));//=IF(O14<2,0,IF(O14<10,1,IF(O14<18,2,3)))
				ic++;// cập nhật lại vị trí cho col kế tiếp
			}
			// ghi phần thống kê tháng các cột cuối
			ir++;
			top = ir; // cập nhật lại index cho row kế tiếp
		}



		public static void ExportSheetBangChiTietKetCong(ExcelWorksheet ws, DateTime ngaybd, DateTime ngaykt, List<cUserInfo> dsnv, params int[] p) {

			int stt = 1, left = 1, top = 1, ir, ic;

			// 1. ghi header
			string headerString = string.Format(Settings.Default.titleBangCC_THANG, ngaybd.ToString("MM/yyyy"));
			ir = top;
            ic = left;
            XL.FormatCell_T201903(ws, ref ir, ref ic, colWidth: (int)CC.STT, value: headerString, fromRow:ir, fromCol:ic, plusRow: 1,
                VeBorder: false, bold: true, wrapText: false, size: 18, hAlign: ExcelHorizontalAlignment.Left);//col 1

            // 2. ghi colTitle
            ic = left;
            int irBelow = ir + 1;
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.STT, value: "STT", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);//col 1
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.manv, value: "Mã NV", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.ten, value: "Họ tên", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);

            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.STT, fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);

			var chisoCotNgay1 = ic;
			var songay = Convert.ToInt32((ngaykt - ngaybd).TotalDays) + 1;
            FormatCell_TCAS201903(ws, ref ir, ref ic, value: "Ngày", fromCol: ic, toCol: (ic + songay - 1)); //vì ic + songay = vị trí ô kế tiếp  
            for (DateTime indexNgay = ngaybd; indexNgay <= ngaykt; indexNgay = indexNgay.AddDays(1d)) {
				XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.congtrongngay, value: indexNgay, plusCol: 1, numFormat: "d");
			}
			//ir = ir - 1; // vì "Ngày" đã plusRow làm tăng ir lên 1
			//ic mội lần lặp đều tăng nên ic hiện tại là vị trí của cột sau ngày cuối tháng
			int icStartSection = ic; // giữ lại vị trí cột đầu để format merge

            XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.tongcong, value: "Công", plusCol: 1);//col 1
			XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.hoc, value: "Học, họp, PT", plusCol: 1);//DANGLAM PTDT CHUYỂN XUỐNG SAU RO
			XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.Le, value: "Lễ", plusCol: 1);
			XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.Phep, value: "Phép CĐ", plusCol: 1);
			XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.cv, value: "Chờ việc", plusCol: 1);
			// write ngày công chuẩn ở trên
			XL.FormatCell_TCAS201903(ws, ref ir, ref icStartSection, colWidth: (int)CC.tongcong, value: "Ngày công chuẩn", fromCol: icStartSection, toCol: ic - 1);

			icStartSection = ic;
			XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.tongcong, value: "TC");
            //write số công chuẫn ở trên
            //var socongchuan = DateTime.DaysInMonth(ngaybd.Year, ngaybd.Month) - XL.DemSoNgayNghiChunhat(ngaybd, true, false);
            //var socongchuan = (float)(ngaykt - ngaybd).TotalDays + 1f - XL.DemSoNgayNghiChunhat(ngaybd, ngaykt, true, false); //v4.7

            XL.FormatCell_T(ws, ref ir, ref ic, colWidth: (int)CC.cong, value: "", plusCol: 1);

			icStartSection = ic;
			XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.ca130, value: string.Format("Ca 3", p[0]), plusCol: 1);
			XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.ca150, value: string.Format("Tăng cường", p[1]), plusCol: 1);
			XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.tcc3_100, value: string.Format("Tăng cường ca 3", p[2]), plusCol: 1);
			XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.LVNN, value: string.Format("Ng.Nghỉ hàng tuần", p[3]), plusCol: 1, size: 10);
			XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pcCa3LVNN, value: string.Format("Ca 3 Ng.Nghỉ h.tuần", p[4]), plusCol: 1, size: 10);
			XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pcle_tet, value: string.Format("Lễ, tết", p[5]), plusCol: 1);
			XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pcCa3_le, value: string.Format("Ca 3 Lễ, tết", p[6]), plusCol: 1);
			XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pckhac, value: "Phụ cấp", plusCol: 1);
			//write phụ cấp ở trên
			XL.FormatCell_TCAS201903(ws, ref ir, ref icStartSection, colWidth: (int)CC.cong, value: "Phụ cấp", fromCol: icStartSection, toCol: ic - 1);

            icStartSection = ic;
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.nghiRo, value: "Nghỉ Ro", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.bh, value: "BHXH", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.ptdt, value: "PT Đoàn thể", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.ptdt, value: "TrS_VR", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);// v4.7

            ws.Row(ir + 1).Height = 50d;

            
            //3. ghi noi dung
            SUMCC sumCC = new SUMCC();
			ir = ir + 2;// 2 dòng title
			ic = left;

			var dsphong = (from nv in dsnv.Where(o => o.NVNhanKiet == false)
                           group nv by nv.PhongBan into @group
						   orderby @group.Key.ViTri
						   select @group).ToList();
            if (dsphong.Count == 0) return; // ko được phân quyền phòng nào hết thì không export gì hết
			foreach (var item in dsphong) {
				var phong = item.Key;
				List<cUserInfo> dsnv1 = dsnv.Where(o => o.PhongBan.ID == phong.ID).ToList();
				XL.EXP_group_ChitietCong_PC(ws, ref stt, ref ir, ref ic, dsnv1, phong, ref sumCC);
				ic = left;
				//ir++;
			}


			//set colWidth =0 các cột ko cần thiết

			//if (Math.Abs(sumCC.le2 - 0f) < 0.01f) ws.Column(4 + songay + 2).Width = 0;
			//if (Math.Abs(sumCC.bhxh5 - 0f) < 0.01f) ws.Column(4 + songay + 5).Width = 0;
			//if (Math.Abs(sumCC.pc100_11 - 0f) < 0.01f) ws.Column(4 + songay + 11).Width = 0;
			//if (Math.Abs(sumCC.pc160_12 - 0f) < 0.01f) ws.Column(4 + songay + 12).Width = 0;
			//if (Math.Abs(sumCC.pc200_13 - 0f) < 0.01f) ws.Column(4 + songay + 13).Width = 0;
			//if (Math.Abs(sumCC.pc290_14 - 0f) < 0.01f) ws.Column(4 + songay + 14).Width = 0;
			//if (Math.Abs(sumCC.pckhac_15 - 0f) < 0.01f) ws.Column(4 + songay + 15).Width = 0;
			//if (Math.Abs(sumCC.nghiRO_16 - 0f) < 0.01f) ws.Column(4 + songay + 16).Width = 0;
			//if (Math.Abs(sumCC.ptdt_17 - 0f) < 0.01f) ws.Column(4 + songay + 17).Width = 0;//DANGLAM


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
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, value: stt, fromRow: ir, toRow: ir + 2, rowContainValue: ir, bold: false);
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, value: nv.MaNV, fromRow: ir, toRow: ir + 2, rowContainValue: ir, bold: false, hAlign: ExcelHorizontalAlignment.Left);
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, value: nv.TenNV, fromRow: ir, toRow: ir + 2, rowContainValue: ir, bold: false, hAlign: ExcelHorizontalAlignment.Left);
			XL.FormatCell_W(ws, ir + 0, ic, value: "Công");
			XL.FormatCell_W(ws, ir + 1, ic, value: "PC");
			XL.FormatCell_W(ws, ir + 2, ic, value: "KH");
			ic++;

			foreach (cNgayCong ngay in nv.DSNgayCong) {
                switch (ngay.Ngay.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                        ws.Cells[ir, ic, ir + 2, ic].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[ir, ic, ir + 2, ic].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        break;
                    case DayOfWeek.Sunday:
                        ws.Cells[ir, ic, ir + 2, ic].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[ir, ic, ir + 2, ic].Style.Fill.BackgroundColor.SetColor(Color.DarkGray);
                        break;
                    default:
                        break;
                }

                XL.FormatCell_N(ws, ir + 0, ic, value: Math.Round(ngay.TongCong_4008, 3), numFormat: Settings.Default.numFormatFloat101F);
				XL.FormatCell_N(ws, ir + 1, ic, value: Math.Round(ngay.PhuCaps._TongPC, 3), numFormat: Settings.Default.numFormatFloat101F);
				var temp = string.Empty;
				ngay.XuatChuoiKyHieuChamCong(ref temp);
				// xuất ký hiệu ở dòng dưới,  vì đã cộng rồi
				XL.FormatCell_W(ws, ir + 2, ic, value: temp, wrapText: true);

				ic++;// cập nhật lại vị trí cho col kế tiếp
			}

			// ghi phần tổng kết
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.Cong, 3) + Math.Round(nv.ThongKeThang.Cong_Congnhat, 3));
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.H_CT_PT, 3));
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.Le, 3));
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.Phep, 3));
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.CongCV, 3));
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.Cong, 3) + Math.Round(nv.ThongKeThang.Le, 3) + Math.Round(nv.ThongKeThang.Phep, 3) + Math.Round(nv.ThongKeThang.CongCV, 3) + Math.Round(nv.ThongKeThang.H_CT_PT, 3));//DANGLAM
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.PhuCaps._30_dem, 3));
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.PhuCaps._50_TC, 3));
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.PhuCaps._100_TCC3, 3));
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.PhuCaps._100_LVNN_Ngay, 3));
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.PhuCaps._150_LVNN_Dem, 3));
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.PhuCaps._200_LeTet_Ngay, 3));
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.PhuCaps._250_LeTet_Dem, 3));
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.PhuCaps._Cus, 3));
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.NghiRo, 3));
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.BHXH, 3));
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.PTDT, 3));//DANGLAM
			XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 2,rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.TongTruCongTreVR + nv.ThongKeThang.TongTruCongSomVR + nv.ThongKeThang.TreSom_KoDuBuCong, 2));//v4.7

			////sum để set colWidth
			//sumCC.le2 += nv.ThongKeThang.Le;
			//sumCC.bhxh5 += nv.ThongKeThang.BHXH;
			//sumCC.pc100_11 += nv.ThongKeThang.PhuCaps._100_LVNN_Ngay;
			//sumCC.pc160_12 += nv.ThongKeThang.PhuCaps._150_LVNN_Dem;
			//sumCC.pc200_13 += nv.ThongKeThang.PhuCaps._200_LeTet_Ngay;
			//sumCC.pc290_14 += nv.ThongKeThang.PhuCaps._250_LeTet_Dem;
			//sumCC.pckhac_15 += nv.ThongKeThang.PhuCaps._Cus;
			//sumCC.nghiRO_16 += nv.ThongKeThang.NghiRo;


			// xuất xong các cột trong 1 dòng, cập nhật lại index cho dòng kế
			top = ir + 3; //(vì xuất 3 cột)
		}

        public static void ExportSheetBangChiTietKetCongNhanKiet(ExcelWorksheet ws, DateTime ngaybd, DateTime ngaykt, List<cUserInfo> dsnv, params int[] p)
        {
            int stt = 1, left = 1, top = 1, ir, ic;

            // 1. ghi header
            string headerString = string.Format(Settings.Default.titleBangCC_THANG, ngaybd.ToString("MM/yyyy"));
            headerString += " CỦA CÔNG TY NHÂN KIỆT";
            ir = top;
            ic = left;
            XL.FormatCell_T201903(ws, ref ir, ref ic, value: headerString, fromRow: ir, fromCol: ic, plusRow: 1,
                VeBorder: false, bold: true, wrapText: false, size: 18, hAlign: ExcelHorizontalAlignment.Left);

            // 2. ghi colTitle
            ic = left;
            int irBelow = ir + 1;
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.STT, value: "STT", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);//col 1
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.manv, value: "Mã NV", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.ten, value: "Họ tên", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);

            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.STT, fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);
            var chisoCotNgay1 = ic;
            var songay = Convert.ToInt32((ngaykt - ngaybd).TotalDays) + 1;
            FormatCell_TCAS201903(ws, ref ir, ref ic, value: "Ngày", fromCol: ic, toCol: (ic + songay - 1)); //vì ic + songay = vị trí ô kế tiếp  
            for (DateTime indexNgay = ngaybd; indexNgay <= ngaykt; indexNgay = indexNgay.AddDays(1d)) {
                XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.congtrongngay, value: indexNgay, plusCol: 1, numFormat: "d");
            }
                         //ic mội lần lặp đều tăng nên ic hiện tại là vị trí của cột sau ngày cuối tháng
            int icStartSection = ic; // giữ lại vị trí cột đầu để format merge

            XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.tongcong, value: "Công", plusCol: 1);//col 1
            XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.Le, value: "Lễ, tết", plusCol: 1);
            XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.Phep, value: "Phép (ma chay, hiếu hỉ)", size:10, plusCol: 1);
            XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.cv, value: "Chờ việc", plusCol: 1);
            //3XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.bh, value: "BHXH", plusCol: 1);
            //4XL.FormatCell_T(ws, ref irtemp, ref ic, colWidth: (int)CC.hoc, value: "Học, họp, PT", plusCol: 1);//DANGLAM PTDT CHUYỂN XUỐNG SAU RO
                                                                                                              // write ngày công chuẩn ở trên
            XL.FormatCell_TCAS201903(ws, ref ir, ref icStartSection, colWidth: (int)CC.tongcong, value: "Ngày công chuẩn", fromCol: icStartSection, toCol: ic - 1);

            icStartSection = ic;
            XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.tongcong, value: "TC");

            //write số công chuẫn ở trên
            //var socongchuan = DateTime.DaysInMonth(ngaybd.Year, ngaybd.Month) - XL.DemSoNgayNghiChunhat(ngaybd, true, false);
            //var socongchuan = (float)(ngaykt - ngaybd).TotalDays + 1f - XL.DemSoNgayNghiChunhat(ngaybd, ngaykt, true, false); //v4.7

            XL.FormatCell_T(ws, ref ir, ref ic, colWidth: (int)CC.cong, value: "", plusCol: 1);//công chuẩn ẩn

            icStartSection = ic;
            XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.ca130, value: string.Format("Ca 3", p[0]), plusCol: 1);
            XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.ca150, value: string.Format("Tăng Cường", p[1]), plusCol: 1);
            XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.tcc3_100, value: "Tăng cường ca 3", plusCol: 1);
            XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.LVNN, value: string.Format("Ng.Nghỉ hàng tuần", p[3]), size: 10, plusCol: 1);
            XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pcCa3LVNN, value: string.Format("Ca 3 Ng.Nghỉ h.tuần", p[4]), size: 10, plusCol: 1);
            XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pcle_tet, value: string.Format("Lễ, tết", p[5]), plusCol: 1);
            XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pcCa3_le, value: string.Format("Ca 3 lễ, tết", p[6]), size: 10, plusCol: 1);
            XL.FormatCell_T(ws, ref irBelow, ref ic, colWidth: (int)CC.pckhac, value: "Phụ cấp", plusCol: 1);
            //write phụ cấp ở trên
            XL.FormatCell_TCAS201903(ws, ref ir, ref icStartSection, colWidth: (int)CC.cong, value: "Phụ cấp", fromCol: icStartSection, toCol: ic - 1);

            icStartSection = ic;
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: (int)CC.nghiRo, value: "Nghỉ Ro", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, colWidth: 0, value: "TrS_VR", fromRow: ir, toRow: irBelow, rowContainValue: irBelow, plusCol: 1);

            ws.Row(ir + 1).Height = 50d;


            //3. ghi noi dung
            SUMCC sumCC = new SUMCC();
            ir = ir + 2;// 2 dòng title
            ic = left;

            var dsphong = (from nv in dsnv
                           where nv.NVNhanKiet == true
                           group nv by nv.PhongBan into @group
                           orderby @group.Key.ViTri
                           select @group).ToList();
            foreach (var item in dsphong) {
                var phong = item.Key;
                List<cUserInfo> dsnv1 = dsnv.Where(o => o.NVNhanKiet == true && o.PhongBan.ID == phong.ID).ToList();
                XL.EXP_group_ChitietCong_PCNhanKiet(ws, ref stt, ref ir, ref ic, dsnv1, phong, ref sumCC);
                ic = left;
                //ir++;
            }


            //set colWidth =0 các cột ko cần thiết

            //if (Math.Abs(sumCC.le2 - 0f) < 0.01f) ws.Column(4 + songay + 2).Width = 0;
            //if (Math.Abs(sumCC.pc100_11 - 0f) < 0.01f) ws.Column(4 + songay + 7).Width = 0;
            //if (Math.Abs(sumCC.pc160_12 - 0f) < 0.01f) ws.Column(4 + songay + 8).Width = 0;
            //if (Math.Abs(sumCC.pc200_13 - 0f) < 0.01f) ws.Column(4 + songay + 9).Width = 0;
            //if (Math.Abs(sumCC.pc290_14 - 0f) < 0.01f) ws.Column(4 + songay + 10).Width = 0;
            //if (Math.Abs(sumCC.pckhac_15 - 0f) < 0.01f) ws.Column(4 + songay + 11).Width = 0;


            ws.Workbook.CalcMode = ExcelCalcMode.Automatic;

            //4. ghi footer
        }
        public static void EXP_group_ChitietCong_PCNhanKiet(ExcelWorksheet ws, ref int stt, ref int top, ref int left, List<cUserInfo> dsnv, cPhongBan phong, ref SUMCC sumCC)
        {
            int ir = top, ic = left;
            XL.FormatCell_W(ws, ref ir, ref ic, value: phong.Ten, plusRow: 1, Bold: true, VeBorder: false); // ghi dòng phòng ban
            foreach (cUserInfo nv in dsnv) {
                ic = left;//reset về cột đầu tiên  dòng tiếp theo
                EXP_record_ChitietCong_PCNhanKiet(ws, ref stt, ref ir, ref ic, nv, ref sumCC); // sau khi xuất xong 1 dòng, ir sẽ cập nhật là ir của dòng kế
                stt++;
            }
            top = ir;// xuất xong 1 group rồi thì cập nhật lại index cho dòng kế
        }
        public static void EXP_record_ChitietCong_PCNhanKiet(ExcelWorksheet ws, ref int stt, ref int top, ref int left, cUserInfo nv, ref SUMCC sumCC)
        {
            int ir = top, ic = left;
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, value: stt, fromRow: ir, toRow: ir + 3, rowContainValue:ir, bold: false);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, value: nv.MaNV, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, hAlign: ExcelHorizontalAlignment.Left);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, value: nv.TenNV, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, hAlign: ExcelHorizontalAlignment.Left);
            XL.FormatCell_W(ws, ir + 0, ic, value: "Công");
            XL.FormatCell_W(ws, ir + 1, ic, value: "PC");
            XL.FormatCell_W(ws, ir + 2, ic, value: "KH");
            XL.FormatCell_W(ws, ir + 3, ic, value: "BDĐH");
            ic++;

            var chuoiKyHieu = string.Empty;
            foreach (cNgayCong ngay in nv.DSNgayCong) {
                switch (ngay.Ngay.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                        ws.Cells[ir, ic, ir+3, ic].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[ir, ic, ir+3, ic].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        break;
                    case DayOfWeek.Sunday:
                        ws.Cells[ir, ic, ir+3, ic].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[ir, ic, ir+3, ic].Style.Fill.BackgroundColor.SetColor(Color.DarkGray);
                        break;
                    default:
                        break;
                }

                XL.FormatCell_N(ws, ir + 0, ic, value: Math.Round(ngay.TongCong_4008,2), numFormat: Settings.Default.numFormatFloat101F);
                XL.FormatCell_N(ws, ir + 1, ic, value: Math.Round(ngay.PhuCaps._TongPC,2), numFormat: Settings.Default.numFormatFloat101F);
                chuoiKyHieu = string.Empty;
                ngay.XuatChuoiKyHieuChamCong(ref chuoiKyHieu);
                // xuất ký hiệu ở dòng dưới,  vì đã cộng rồi
                XL.FormatCell_W(ws, ir + 2, ic, value: chuoiKyHieu, wrapText: true);
                XL.FormatCell_N(ws, ir + 3, ic, numFormat: Settings.Default.numFormatInt);
                ws.Cells[ir + 3, ic].Formula = string.Format("IF({0}<0.35,0,IF({0}<1.5,1,IF({0}<2.5,2,3)))", ws.Cells[ir, ic].Address);


                ic++;// cập nhật lại vị trí cho col kế tiếp
            }

            // ghi phần tổng kết
            int icStartSection, icEndSection;
            icStartSection = ic;
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.Cong);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: 0f);//Lễ
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.Phep);//Phép
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.CongCV);
            icEndSection = ic-1;
            XL.FormatCell_N(ws, ref ir, ref ic, plusCol: 1, bold: true, VeBorder: false, vAlign: ExcelVerticalAlignment.Top, congthuc: string.Format("SUM({0})", ws.Cells[ir, icStartSection, ir, icEndSection].Address));
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._30_dem);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._50_TC);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._100_TCC3);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._100_LVNN_Ngay);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._150_LVNN_Dem);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._200_LeTet_Ngay);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._250_LeTet_Dem);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.PhuCaps._Cus);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: nv.ThongKeThang.NghiRo);
            XL.FormatCell_TCenterVertical201903(ws, ref ir, ref ic, plusCol: 1, fromRow: ir, toRow: ir + 3, rowContainValue: ir, bold: false, wrapText: false, hAlign: ExcelHorizontalAlignment.Right, vAlign: ExcelVerticalAlignment.Top, value: Math.Round(nv.ThongKeThang.TongTruCongTreVR + nv.ThongKeThang.TongTruCongSomVR + nv.ThongKeThang.TreSom_KoDuBuCong, 2));

            //sum để set colWidth
            //sumCC.le2 += nv.ThongKeThang.Le;
            //1sumCC.bhxh5 += nv.ThongKeThang.BHXH;
            //sumCC.pc100_11 += nv.ThongKeThang.PhuCaps._100_LVNN_Ngay;
            //sumCC.pc160_12 += nv.ThongKeThang.PhuCaps._150_LVNN_Dem;
            //sumCC.pc200_13 += nv.ThongKeThang.PhuCaps._200_LeTet_Ngay;
            //sumCC.pc290_14 += nv.ThongKeThang.PhuCaps._250_LeTet_Dem;
            //sumCC.pckhac_15 += nv.ThongKeThang.PhuCaps._Cus;
            //sumCC.nghiRO_16 += nv.ThongKeThang.NghiRo;


            // xuất xong các cột trong 1 dòng, cập nhật lại index cho dòng kế
            top = ir + 4; //(vì xuất 4 dòng), dòng kế tiếp là dòng 5
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
		public static void ExportSheetThongSo1(ExcelWorksheet ws, DateTime ngaydauthang, DateTime ngaycuoithang, DataTable tableThongSo) {
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
            XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: Convert.ToInt32(XL.TinhCongChuanCuaThang(ngaydauthang, ngaycuoithang)), numberFormat:Settings.Default.numFormatInt);//, numberFormat:"##");
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)tableThongSo.Rows[0]["MucLuongTTTT17"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (float)tableThongSo.Rows[0]["LuongPTT"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (float)tableThongSo.Rows[0]["LuongTrucLeTetBV"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (float)tableThongSo.Rows[0]["PhuCapTrachNhiem"]);
			XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: (int)Settings.Default.MucLuongTT172135);

			//XL.FillCell(ws, ref ir, ref ic, plusRow: 1, value: ()tableThongSo.Rows[0][""]);

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

			int left = 1, top = 4, ir, ic;
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

			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.PhongBan.ID);//maphong//"Mã phòng", // col 1
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.PhongBan.Ten);//tenphong//"Tên phòng",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.MaNV);//manv//"Mã NV",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.TenNV);//tennv//"Họ tên",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.HeSo.LuongCB);//hslcb 5//"HSLCB",//col 5
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.HeSo.LuongCV);//
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.HeSo.LCBTT17);//
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.HeSo.PCCVTT17);//
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.HeSo.PCDHTT17);//
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.HeSo.PCTNTT17);// col 10
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.HeSo.BHCongThem_ChoGD_PGD);//"HSBH Cộng thêm lãnh đạo",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.Cong);//cong//"Công",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.Phep);////"Phép",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.H_CT_PT);////"Học, họp, CT, PT",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.Le);//Le col 15
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.BHXH);////"BH",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.SoNgayNghiRO_NguyenNgay);////"SoNgayNghiRO_NguyenNgay",//ver 4.0.0.8
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.NghiRo);////"Việc riêng",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PTDT);////""PT Đoàn thể"",//DANGLAM
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.CongCV);////"Chờ việc", col 20
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.NgayQuaDem);//qua đêm//"Qua đêm",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._30_dem);////"PC Đêm",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._50_TC);//"PC tăng cường",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._100_TCC3);//"PC TC Ca3",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._100_LVNN_Ngay);//"PC LVNN Ngày", col 25
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._150_LVNN_Dem);//"PC LVNN Ca 3",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._200_LeTet_Ngay);//"Trực lễ ngày",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._250_LeTet_Dem);//"Trực lễ ca3",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.ThongKeThang.PhuCaps._Cus);//"Phụ cấp tùy chỉnh",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//"Tổng công", col 30
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1); //"Tổng PC",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//, value: nv.chiTietLuong.LCB_Theo.Cong_CDNghi//"Lương CB theo ngày công chuẩn",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//, value: nv.chiTietLuong.LCB_Theo.CongCV//"Lương CB Theo Công CV",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);////, value: nv.chiTietLuong.LSP_Theo.Cong_CDNghi"Lương SP",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.chiTietLuong.LuongDieuChinh);////"Điều chỉnh lương tháng trước", //col 35
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//, value: nv.chiTietLuong.TongLuong_KoTinhCacLoaiPhuCap//"Tổng lương",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//"PC theo Lương CB",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//, value: nv.chiTietLuong.LSP_Theo.PhuCap"PC theo Lương SP",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//"Tổng PC",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//"Tổng lương và PC", col 40
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.chiTietLuong.KhauTru.TamUng);//"Tạm ứng",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.chiTietLuong.MucDongBHXH);//"Mức đóng BHXH",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//"Khấu trừ BHXH, YT, TN",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.chiTietLuong.KhauTru.ThuChiKhac);//"Thu chi khác",
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//"Tiền cơm trưa", col 45
			XL.FillCell(ws, ref ir, ref ic, plusCol: 1);//"Thực lãnh",
			//XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.chiTietLuong.LSP_Theo.Cong_CDNghi);////"Lương SP",
			//XL.FillCell(ws, ref ir, ref ic, plusCol: 1, value: nv.chiTietLuong.LSP_Theo.PhuCap);//"PC theo Lương SP",

			ir++; //sau khi xuất hết 3 cái vào ra thì tăng ir, ko tăng bên trong vì nếu tăng bên trong thì
			top = ir;
		}

	}
}
