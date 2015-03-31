using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;

namespace ChamCong_v04.BUS {

	public class XL2 {

		public static int currUserID = Int32.MinValue;
		public static string currUserAccount = String.Empty;

		public static readonly TimeSpan _0gio = TimeSpan.Zero;
		public static readonly TimeSpan _01giay = new TimeSpan(0, 0, 1);
		public static readonly TimeSpan _01phut = new TimeSpan(0, 1, 0);
		public static readonly TimeSpan _05phut = new TimeSpan(0, 5, 0);
		public static readonly TimeSpan _10phut = new TimeSpan(0, 10, 0);
		public static readonly TimeSpan _30phut = new TimeSpan(0, 30, 0);
		public static readonly TimeSpan _02gio = new TimeSpan(2, 0, 0);
		public static readonly TimeSpan _03gio = new TimeSpan(3, 0, 0);
		public static readonly TimeSpan _04gio = new TimeSpan(4, 0, 0);
		public static readonly TimeSpan _07gio45ph = new TimeSpan(7, 45, 0);
		public static readonly TimeSpan _08gio = new TimeSpan(8, 0, 0);
		public static readonly TimeSpan _08gio1giay = new TimeSpan(8, 0, 1);
		public static readonly TimeSpan _12gio = new TimeSpan(12, 0, 0);
		public static readonly TimeSpan _16gio = new TimeSpan(16, 0, 0);
		public static readonly TimeSpan _1ngay = new TimeSpan(1, 0, 0, 0);
		public static readonly TimeSpan _04h30 = new TimeSpan(4, 30, 0);
		public static readonly TimeSpan _05h45 = new TimeSpan(5, 45, 0);
		public static readonly TimeSpan _06h00 = new TimeSpan(6, 0, 0);
		public static readonly TimeSpan _07h00 = new TimeSpan(7, 0, 0);
		public static readonly TimeSpan _07h30 = new TimeSpan(7, 30, 0);
		public static readonly TimeSpan _13h45 = new TimeSpan(13, 45, 0);
		public static readonly TimeSpan _18h00 = new TimeSpan(18, 0, 0);
		public static readonly TimeSpan _20h00 = new TimeSpan(20, 0, 0);
		public static readonly TimeSpan _21h45 = new TimeSpan(21, 45, 0);
		public static readonly TimeSpan _22h00 = new TimeSpan(22, 0, 0);
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
		public static List<cPhongBan> TatcaPhongban = new List<cPhongBan>();
		public static List<int> QuyenThaoTac;

		public class cChucNang {
			public int ID { get; set; }
			public string MoTa { get; set; }
			public bool IsYes { get; set; }
			public override string ToString() {
				return "ID= " + ID + " IsYes= " + IsYes + "\t" + MoTa;
			}
		}


		public static void VeCheckBox_CheckAll(DataGridView grid, CheckBox checkBox, EventHandler checkAll_CheckedChanged, Point location) {
			var rect = grid.GetCellDisplayRectangle(0, -1, true);
			checkBox.Size = new Size(18, 18); checkBox.Location = new Point(rect.Location.X + location.X, rect.Location.Y + location.Y);
			checkBox.CheckedChanged += checkAll_CheckedChanged;
			grid.Controls.Add(checkBox);
		}



		public static void TachThang(DateTime ngayBd, DateTime ngayKt, out List<DateTime> arrNgayBd, out List<DateTime> arrNgayKt)
		{
			arrNgayBd=new List<DateTime>();
			arrNgayKt= new List<DateTime>();
			var indexNgay = ngayBd;
			var indexThang = new DateTime(indexNgay.Year, indexNgay.Month, 1);
			var ThangKT = new DateTime(ngayKt.Year, ngayKt.Month, 1);
			while (indexThang < ThangKT)
			{
				var ngayktthang = new DateTime(indexNgay.Year, indexNgay.Month, DateTime.DaysInMonth(indexNgay.Year, indexNgay.Month));
				arrNgayBd.Add(indexNgay);
				arrNgayKt.Add(ngayktthang);
				indexNgay = new DateTime(indexNgay.Year, indexNgay.Month,1); // trả về ngày đầu tháng
				indexNgay = indexNgay.AddMonths(1);// index ngày giữ ngày đầu tiên của tháng (trừ lần đầu tiên giữ ngày bd)
				indexThang = indexThang.AddMonths(1); // thực tế là tăng tháng
			}
			//tháng của index = tháng của ngày kết thúc
			arrNgayBd.Add(indexNgay);
			arrNgayKt.Add(ngayKt);
		}

		public static bool KiemtraKetnoiCSDL()
		{
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return false;
			}
			return true;
		}

		public static Point GetCenterLocation(int MdiParentWidth, int MdiParentHeight, int formWidth, int formHeight)
		{
			return new Point((int)((MdiParentWidth - formWidth) / 2f),(int)((MdiParentHeight - formHeight) / 2f));
		}

		internal static List<XL2.cChucNang> TaoChucNang() {
			List<cChucNang> lstChucNang = new List<cChucNang>();
			//lstChucNang.Add(new cChucNang() { ID = 10000, MoTa = "Kết nối CSDL" });
			lstChucNang.Add(new cChucNang() { ID = 10011, MoTa = "Xem Công" });
			lstChucNang.Add(new cChucNang() { ID = 10012, MoTa = "Thêm xoá sửa giờ chấm công" });
			lstChucNang.Add(new cChucNang() { ID = 10033, MoTa = "Xác nhận ca và làm thêm" });
			lstChucNang.Add(new cChucNang() { ID = 10014, MoTa = "Xác nhận phụ cấp tăng cường" });
			lstChucNang.Add(new cChucNang() { ID = 10015, MoTa = "Xác nhận phụ cấp làm việc ngày nghỉ, trực lễ, tết" });
			lstChucNang.Add(new cChucNang() { ID = 10016, MoTa = "Kết công tháng" });
			lstChucNang.Add(new cChucNang() { ID = 10021, MoTa = "Điểm danh Nhân viên" });
			lstChucNang.Add(new cChucNang() { ID = 10031, MoTa = "Chấm công tay cho Quản lý" });

			lstChucNang.Add(new cChucNang() { ID = 20011, MoTa = "Khai báo vắng cho Nhân viên" });

			lstChucNang.Add(new cChucNang() { ID = 30011, MoTa = "Sửa giờ hàng loạt" });
			lstChucNang.Add(new cChucNang() { ID = 30012, MoTa = "Xem lịch sử thao tác" });
			lstChucNang.Add(new cChucNang() { ID = 30013, MoTa = "Quản lý nhiệm vụ của nhân viên"});
			lstChucNang.Add(new cChucNang() { ID = 30014, MoTa = "Xem thống kê công, PC, phép theo nhiệm vụ"});
			lstChucNang.Add(new cChucNang() { ID = 30015, MoTa = "Xem danh sách nhiệm vụ"});

			lstChucNang.Add(new cChucNang() { ID = 40011, MoTa = "Quản lý Nhân viên" });

			lstChucNang.Add(new cChucNang() { ID = 50011, MoTa = "Kết lương và huỷ kết lương tháng" });

			//lstChucNang.Add(new cChucNang() { ID = 60011, MoTa = "Đổi mật khẩu tài khoản" });// xem [2703_1]
			lstChucNang.Add(new cChucNang() { ID = 70011, MoTa = "Phân quyền" });
			lstChucNang.Add(new cChucNang() { ID = 70012, MoTa = "Cài đặt thông số" });
			lstChucNang.Add(new cChucNang() { ID = 70013, MoTa = "Tạo tài khoản đăng nhập" });

			return lstChucNang;


		}
	}
}
