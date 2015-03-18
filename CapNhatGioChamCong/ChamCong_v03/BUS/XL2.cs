using System;
using System.Drawing;
using System.Windows.Forms;
using ChamCong_v03.Properties;
namespace ChamCong_v03.BUS {
	public class XL2 {

		public static int currUserID = int.MinValue;
		public static string currUserAccount = string.Empty;

		public static readonly TimeSpan _0gio = TimeSpan.Zero;
		public static readonly TimeSpan _01giay = new TimeSpan(0, 0, 1);
		public static readonly TimeSpan _01phut = new TimeSpan(0, 1, 0);
		public static readonly TimeSpan _05phut = new TimeSpan(0, 5, 0);
		public static readonly TimeSpan _10phut = new TimeSpan(0, 10, 0);
		public static readonly TimeSpan _30phut = new TimeSpan(0, 30, 0);
		public static readonly TimeSpan _02gio = new TimeSpan(2, 0, 0);
		public static readonly TimeSpan _04gio = new TimeSpan(4, 0, 0);
		public static readonly TimeSpan _07gio45ph = new TimeSpan(7, 45, 0);
		public static readonly TimeSpan _08gio = new TimeSpan(8, 0, 0);
		public static readonly TimeSpan _08gio1giay = new TimeSpan(8, 0, 1);
		public static readonly TimeSpan _12gio = new TimeSpan(12, 0, 0);
		public static readonly TimeSpan _16gio = new TimeSpan(16, 0, 0);
		public static readonly TimeSpan _1ngay = new TimeSpan(1, 0, 0, 0);
		public static readonly TimeSpan _04h30 = new TimeSpan(4, 30, 0);
		public static readonly TimeSpan _05h45 = new TimeSpan(5, 45, 0);
		public static readonly TimeSpan _07h00 = new TimeSpan(7, 0, 0);
		public static readonly TimeSpan _07h30 = new TimeSpan(7, 30, 0);
		public static readonly TimeSpan _13h45 = new TimeSpan(13, 45, 0);
		public static readonly TimeSpan _18h00 = new TimeSpan(18, 0, 0);
		public static readonly TimeSpan _20h00 = new TimeSpan(20, 0, 0);
		public static readonly TimeSpan _21h45 = new TimeSpan(21, 45, 0);
		public static readonly TimeSpan _24h00 = new TimeSpan(24, 0, 0);
		public static int PC30;
		public static int PC50;
		public static int PCTCC3;
		public static int PC100;
		public static int PC160;
		public static int PC200;
		public static int PC290;
		public static TimeSpan TGLamDemToiThieu;
		public static TimeSpan ChoPhepTre;
		public static TimeSpan ChoPhepSom;
		public static TimeSpan LamThemAfterOT;
		public static DateTime ThangKetCong;


		public static void VeCheckBox_CheckAll(DataGridView grid, CheckBox checkBox, EventHandler checkAll_CheckedChanged, Point location) {
			var rect = grid.GetCellDisplayRectangle(0, -1, true);
			checkBox.Size = new Size(18, 18); checkBox.Location = new Point(rect.Location.X + location.X, rect.Location.Y + location.Y);
			checkBox.CheckedChanged += checkAll_CheckedChanged;
			grid.Controls.Add(checkBox);
		}

		public static void VeLogo(string name, OfficeOpenXml.ExcelWorksheet ws) {

			var p = ws.Drawings.AddPicture(name, Resources.CNS);
			p.SetPosition(0, 0, 0, 0);
			p.SetSize(100);
		}

		public static void GhiThongTinTongcty(OfficeOpenXml.ExcelWorksheet ws, string address, 
			int sR1, int sC1, int sizeTCty, int sR2, int sC2, int sizeDiaChi, int sR3, int sC3) {
			XL.FormatCells(ws.Cells[sR1 + 0, sC1], "TỔNG CÔNG TY CÔNG NGHIỆP SÀI GÒN", VeBorder: false, size:sizeTCty);
			XL.FormatCells(ws.Cells[sR1 + 1, sC1], "TRÁCH NHIỆM HỮU HẠN MỘT THÀNH VIÊN", VeBorder: false, size:sizeTCty);
			XL.FormatCells(ws.Cells[sR1 + 2, sC1], "NHÀ MÁY THUỐC LÁ KHÁNH HỘI", VeBorder: false, bold: true, size:sizeTCty);

			XL.FormatCells(ws.Cells[sR2 + 0, sC2], "Lô 26, đường số 3, KCN Tân Tạo, Quận Bình Tân, Thành phố Hồ Chí Minh", VeBorder: false, size: sizeDiaChi);
			XL.FormatCells(ws.Cells[sR2 + 1, sC2], "Tel: +848 37507282 - FAX: 37507784", VeBorder: false, size:sizeDiaChi);
			XL.FormatCells(ws.Cells[sR2 + 2, sC2], "Email: khanhhoi@khcig.com    Website: http://khcig.com", VeBorder: false,size:sizeDiaChi);

			var tphcm = "Thành phố Hồ Chí Minh, ngày {0} tháng {1} năm {2}";
			tphcm = string.Format(tphcm, DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);
			XL.FormatCells(ws.Cells[sR3, sC3], tphcm, VeBorder: false,size:sizeDiaChi);

		}
	}
}
