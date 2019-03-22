using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ChamCong_v04.Properties;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ChamCong_v04.BUS {
	public static partial class XL {




		public static void FormatCell(ExcelWorksheet ws, ref int row, ref int col, object value = null,
			int plusRow = 0, int plusCol = 0, int? colWidth = null,
			bool wrapText = false,
			int size = 12, bool bold = false, bool italic = false,
			bool VeBorder = true,
			ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin,
			ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Center,
			ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center,
			string numFormat = null, string congthuc = null) {

			if (value != null) ws.Cells[row, col].Value = value;
			if (colWidth != null) ws.Column(col).Width = (int)colWidth;
			ws.Cells[row, col].Style.WrapText = wrapText;
			ws.Cells[row, col].Style.Font.Size = size;
			ws.Cells[row, col].Style.Font.Bold = bold;
			ws.Cells[row, col].Style.Font.Italic = italic;
			ws.Cells[row, col].Style.HorizontalAlignment = hAlign;
			ws.Cells[row, col].Style.VerticalAlignment = vAlign;
			if (VeBorder) ws.Cells[row, col].Style.Border.BorderAround(viendamnhat, Color.Black);
			if (congthuc != null) ws.Cells[row, col].Formula = congthuc;
			if (numFormat != null) ws.Cells[row, col].Style.Numberformat.Format = numFormat;
			if (plusCol > 0) col += plusCol;
			if (plusRow > 0) row += plusRow;
		}


		public static void FormatCell_T_Merge(ExcelWorksheet ws, ref int currRow, ref int currCol, object value = null,
			int plusRow = 0, int plusCol = 0, int? colWidth = null,
			bool wrapText = true,
			int size = 12, bool bold = true, bool italic = false,
			bool merge = true, int? fromRow = null, int? fromCol = null, int? toRow = null, int? toCol = null,
			bool VeBorder = true, ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin, ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Center, ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center
			) {
			if (colWidth != null) ws.Column(currCol).Width = (int)colWidth;

			ExcelRange Cells = (merge && fromRow != null && fromCol != null && toCol != null && toRow != null)
				? ws.Cells[(int)fromRow, (int)fromCol, (int)toRow, (int)toCol]
				: ws.Cells[currRow, currCol];
			if (merge) Cells.Merge = true;
			Cells.Style.WrapText = wrapText;
			if (value != null) Cells.Value = value;
			Cells.Style.Font.Size = size;
			if (bold) Cells.Style.Font.Bold = true;
			if (italic) Cells.Style.Font.Italic = true;
			Cells.Style.HorizontalAlignment = hAlign;
			Cells.Style.VerticalAlignment = vAlign;

			if (VeBorder) Cells.Style.Border.BorderAround(viendamnhat, Color.Black);

			if (plusCol > 0) currCol += plusCol;
			if (plusRow > 0) currRow += plusRow;
		}
        public static void FormatCell_TCAS201903(ExcelWorksheet ws, ref int currRow, ref int currCol, object value = null,
    int plusRow = 0, int plusCol = 0, int? colWidth = null,
    bool wrapText = true,
    int size = 12, bool bold = true, bool italic = false,
    int? fromCol = null, int? toCol = null,
    bool VeBorder = true, ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin, ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.CenterContinuous, ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center
)
        {
            if (colWidth != null) ws.Column(currCol).Width = (int)colWidth;

            ExcelRange CurrentCell = ws.Cells[currRow, currCol];
            if (value != null) CurrentCell.Value = value;
            CurrentCell.Style.WrapText = wrapText;
            CurrentCell.Style.Font.Size = size;
            if (bold) CurrentCell.Style.Font.Bold = true;
            if (italic) CurrentCell.Style.Font.Italic = true;

            ExcelRange RangeCell = ws.Cells[currRow, (int)fromCol, currRow, (int)toCol];
            RangeCell.Style.HorizontalAlignment = hAlign;
            RangeCell.Style.VerticalAlignment = vAlign;

            if (VeBorder) RangeCell.Style.Border.BorderAround(viendamnhat, Color.Black);

            if (plusCol > 0) currCol += plusCol;
            if (plusRow > 0) currRow += plusRow;
        }
        public static void FormatCell_T201903(ExcelWorksheet ws, ref int currRow, ref int currCol, object value = null,
            int fromRow = 0, int fromCol = 0,
            int plusRow = 0, int plusCol = 0, int? colWidth = null,
            bool wrapText = true,
            int size = 12, bool bold = true, bool italic = false,
            bool VeBorder = true, ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin, ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Center, ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center
            )
        {
            if (colWidth != null) ws.Column(currCol).Width = (int)colWidth;

            ExcelRange Cells = ws.Cells[fromRow, fromCol];

            Cells.Style.WrapText = wrapText;
            if (value != null) Cells.Value = value;
            Cells.Style.Font.Size = size;
            if (bold) Cells.Style.Font.Bold = true;
            if (italic) Cells.Style.Font.Italic = true;
            Cells.Style.HorizontalAlignment = hAlign;
            Cells.Style.VerticalAlignment = vAlign;

            if (VeBorder) Cells.Style.Border.BorderAround(viendamnhat, Color.Black);

            if (plusCol > 0) currCol += plusCol;
            if (plusRow > 0) currRow += plusRow;
        }
        public static void FormatCell_TCenterVertical201903(ExcelWorksheet ws, ref int currRow, ref int currCol, object value = null,
    int plusRow = 0, int plusCol = 0, int? colWidth = null,
    bool wrapText = true,
    int size = 12, bool bold = true, bool italic = false,
    int? rowContainValue = null, int? fromRow = null, int? toRow = null,
    bool VeBorder = true, ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin, ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Center, ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center
)
        {
            if (colWidth != null) ws.Column(currCol).Width = (int)colWidth;

            ExcelRange CellFillValue = ws.Cells[(int)rowContainValue, currCol];
            if (value != null) CellFillValue.Value = value;
            CellFillValue.Style.WrapText = wrapText;
            CellFillValue.Style.Font.Size = size;
            if (bold) CellFillValue.Style.Font.Bold = true;
            if (italic) CellFillValue.Style.Font.Italic = true;
            CellFillValue.Style.HorizontalAlignment = hAlign;
            CellFillValue.Style.VerticalAlignment = vAlign;

            if (VeBorder) ws.Cells[(int)fromRow, currCol, (int)toRow, currCol].Style.Border.BorderAround(viendamnhat, Color.Black);

            if (plusCol > 0) currCol += plusCol;
            if (plusRow > 0) currRow += plusRow;
        }

        public static void FormatCell_T_Merge(ExcelWorksheet ws, int currRow, int currCol, object value = null,
			/*int plusRow = 0, int plusCol = 0, */int? colWidth = null,
			bool wrapText = true,
			int size = 12, bool bold = true, bool italic = false,
			bool merge = true, int? fromRow = null, int? fromCol = null, int? toRow = null, int? toCol = null,
			int? entireColIndexFormatNum = null, string numFormatEntireCol = null,
			bool VeBorder = true, ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin, ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Center, ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center
			) {

			FormatCell_T_Merge(ws, ref currRow, ref currCol, value: value,
		colWidth: colWidth,
		wrapText: wrapText,
		size: size, bold: bold, italic: italic,
		merge: merge, fromRow: fromRow, fromCol: fromCol, toRow: toRow, toCol: toCol,
		VeBorder: VeBorder, viendamnhat: viendamnhat, hAlign: hAlign, vAlign: vAlign);
		}
		/// <summary>
		/// format mac dinh Number , align right
		/// </summary>
		public static void FormatCell_N(ExcelWorksheet ws, ref int row, ref int col, object value = null,
			int plusRow = 0, int plusCol = 0, int? colWidth = null,
			bool wrapText = false,
			int size = 12, bool bold = false, bool italic = false,
			string numFormat = null, string congthuc = null,
			bool VeBorder = true, ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin, ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Right, ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center) {
			FormatCell(ws, ref row, ref col, value: value,
				plusRow: plusRow, plusCol: plusCol, colWidth: colWidth,//merge default false, wrap false
				size: size, bold: bold, italic: italic,
				numFormat: numFormat, congthuc: congthuc,
				VeBorder: VeBorder, viendamnhat: viendamnhat, hAlign: hAlign, vAlign: vAlign);
		}
		public static void FormatCell_N(ExcelWorksheet ws, int row, int col, object value = null,
			int? colWidth = null,
			bool wrapText = false,
			int size = 12, bool bold = false, bool italic = false,
			string numFormat = null, string congthuc = null,
			bool VeBorder = true, ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin, ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Right, ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center) {
			FormatCell(ws, ref row, ref col, value: value,
				colWidth: colWidth,//merge default false, wrap false
				size: size, bold: bold, italic: italic,
				numFormat: numFormat, congthuc: congthuc,
				VeBorder: VeBorder, viendamnhat: viendamnhat, hAlign: hAlign, vAlign: vAlign);
		}


		/// <summary>
		/// format mac dinh TitleColumn , align center, bold
		/// </summary>
		public static void FormatCell_T(ExcelWorksheet ws, ref int currRow, ref int currCol, object value = null,
			int plusRow = 0, int plusCol = 0, int? colWidth = null,
			bool wrapText = true,
			int size = 12, bool Bold = true, bool Italic = false,
			string numFormat = null,
			bool VeBorder = true, ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin, ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Center, ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center) {
			FormatCell(ws, ref currRow, ref currCol, value: value,
				plusRow: plusRow, plusCol: plusCol, colWidth: colWidth,
				wrapText: wrapText,
				size: size, bold: Bold, italic: Italic,
				numFormat: numFormat,
				VeBorder: VeBorder, viendamnhat: viendamnhat, hAlign: hAlign, vAlign: vAlign);
		}
		/// <summary>
		/// format mac dinh word noi dung , align left
		/// </summary>
		public static void FormatCell_W(ExcelWorksheet ws, ref int row, ref int col, object value = null,
			int plusRow = 0, int plusCol = 0, int? colWidth = null,
			bool wrapText = false,
			int size = 12, bool Bold = false, bool Italic = false,
			string numFormat = null, string congthuc = null,
			bool VeBorder = true, ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin, ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Left, ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center) {
			FormatCell(ws, ref row, ref col, value: value,
				plusRow: plusRow, plusCol: plusCol, colWidth: colWidth,
				 wrapText: wrapText,
				 size: size, bold: Bold, italic: Italic,
				 VeBorder: VeBorder, viendamnhat: viendamnhat, hAlign: hAlign, vAlign: vAlign);
		}
		public static void FormatCell_W(ExcelWorksheet ws, int row, int col, object value = null,
			/*int plusRow = 0, int plusCol = 0, */int? colWidth = null,
			bool wrapText = false,
			int size = 12, bool Bold = false, bool Italic = false,
			string numFormat = null, string congthuc = null,
			bool VeBorder = true, ExcelBorderStyle viendamnhat = ExcelBorderStyle.Thin, ExcelHorizontalAlignment hAlign = ExcelHorizontalAlignment.Left, ExcelVerticalAlignment vAlign = ExcelVerticalAlignment.Center) {
			FormatCell(ws, ref row, ref col, value: value,
				/*plusRow: plusRow, plusCol: plusCol, */colWidth: colWidth,
				 wrapText: wrapText,
				 size: size, bold: Bold, italic: Italic,
				 VeBorder: VeBorder, viendamnhat: viendamnhat, hAlign: hAlign, vAlign: vAlign);
		}

		public static void FormatNumber(ExcelWorksheet ws,ref int currRow,ref int currCol, int fromRow, int fromCol, int toRow, int toCol, 
			string numberFormat = null, int plusCol = 0, int plusRow = 0)
		{
			ws.Cells[fromRow, fromCol, toRow, toCol].Style.Numberformat.Format = numberFormat;
			if (plusCol > 0) currCol += plusCol;
			if (plusRow > 0) currRow += plusRow;
		}

		public static void FillCell(ExcelWorksheet ws, ref int currRow, ref int currCol, object value=null, int plusCol = 0, int plusRow = 0, string numberFormat = null)
		{
			if (value != null) ws.Cells[currRow, currCol].Value = value;
			if (plusCol > 0) currCol += plusCol;
			if (plusRow > 0) currRow += plusRow;
			if (numberFormat != null) ws.Cells[currRow, currCol].Style.Numberformat.Format = numberFormat;
		}
		public static void FillCell(ExcelWorksheet ws, int currRow, int currCol, object value=null, int plusCol = 0, int plusRow = 0, string numberFormat = null)
		{
			FillCell(ws, ref currRow, ref currCol, value, plusCol, plusRow, numberFormat);
		}


		public static bool XuatFileExcel(string saveFileName, byte[] bin, string logFunction)
		{
			bool @continue = true;
			bool error = false;
			while (@continue) {
				try {
					File.WriteAllBytes(saveFileName, bin);
					@continue = false;
					error = false;
				} catch (Exception ex) {
					lg.Error(string.Format("[{0}]_[{1}]\n", "XLFormatCell", System.Reflection.MethodBase.GetCurrentMethod().Name), ex);

					if (ex is UnauthorizedAccessException)
					{
						MessageBox.Show(Resources.Text_AccessDenied, Resources.Caption_Loi);
						@continue = false; // ko được quyền ghi thì thoát
						error = true;
					}
					else if (ex is DirectoryNotFoundException)
					{
						MessageBox.Show(Resources.Text_FolderNotFound, Resources.Caption_Loi);
						@continue = false; // ko được quyền ghi thì thoát
						error = true;
					}
					else if (ex is IOException)
					{
						MessageBox.Show(Resources.Text_FileDangMoBoiUngDungKhac, Resources.Caption_Loi);
						@continue = true; // cho phép thực hiện lại
					}
					else {
						MessageBox.Show(Resources.Text_KhongTheGhiFile, Resources.Caption_Loi);
						@continue = false;
						error = true;
					}
				}
			}
			return error;
		}


	}

}
