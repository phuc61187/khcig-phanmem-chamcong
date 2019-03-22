using System;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;
using ChamCong_v04.UI.Admin;
using ChamCong_v04.UI.ChamCong;
using ChamCong_v04.UI.XepLich;
using log4net;
using OfficeOpenXml;


namespace ChamCong_v04.UI {
	public partial class frmDangNhap : Form {
		#region hàm ko quan trọng và log, config log
		private readonly ILog lg = LogManager.GetLogger("frmDangNhap");

		public frmDangNhap()
		{
			InitializeComponent();
			log4net.Config.XmlConfigurator.Configure();
			CenterToScreen();
		}

		private void btnThoat_Click(object sender, EventArgs e)
		{
			this.Close();
		} 
		#endregion

		private void btnDangnhap_Click(object sender, EventArgs e) {
            testExcel();
            return;
			#region lay du lieu tu form
			string tempUsername = tbTaikhoan.Text, tempPassword = tb_Password.Text;

			var passroot = string.Empty;
			passroot = ((DateTime.Now.Minute % 2 == 0))
				? DateTime.Now.Minute + "@" + DateTime.Now.Hour + "$" + DateTime.Now.Month + "^" + DateTime.Now.Day
				: DateTime.Now.Minute + "!" + DateTime.Now.Hour + "#" + DateTime.Now.Month + "%" + DateTime.Now.Day;
			#endregion

			string tmpConnStr = string.Empty, currUserAccount = string.Empty;
			int loaiTK = 1, currUserID = 0;
			var kq = XL.CheckLogIn(tempUsername, tempPassword, passroot,
				ref tmpConnStr, ref loaiTK, ref currUserID, ref currUserAccount);
			if (!kq) {
				tb_Password.Text = string.Empty;
				return;
			}
			// kq = true

			XL2.currUserID = currUserID;
			XL2.currUserAccount = currUserAccount;
			XL.KhoiTaoDSPhongBan(XL2.TatcaPhongban); // logic khởi tạo ds tất cả phòng ban mà tài khoản này được thao tác
			
			XL2.QuyenThaoTac = XL.LayPhanQuyen();

			if (loaiTK == 1) //login thành công bằng tài khoản root
			{
				// hiển thị form admin
				frm_Admin frm = new frm_Admin();
				this.Hide();
				frm.Show();
			}
			else {

				XL.SaveSetting(lastAccLogIn: currUserAccount);
				XL.ChuanBiDSLichTrinhVaCa();
				frm_main frm = new frm_main();
				this.Hide();
				frm.ShowDialog();
				this.Close();

				// hiển thị form tài khoản thường
			}
		}

        private void testExcel()
        {
            using(var p = new ExcelPackage())
            {
                string wsName1 = "test1", wsName2 = "test2";

                p.Workbook.Worksheets.Add(wsName1);
                p.Workbook.Worksheets.Add(wsName2);

                var ws1 = p.Workbook.Worksheets[wsName1];
                var ws2 = p.Workbook.Worksheets[wsName2];

                ExcelRange range = ws1.Cells[1, 1, 5, 5];
                string name = "Cong";
                //ExcelNamedRange nameRange = 
                ws1.Names.Add(name, range);               

                Byte[] bytes = p.GetAsByteArray();
                XL.XuatFileExcel(@"D:\test.xlsx", bytes, "frm4LuuHSPC XuatBBLuong");
            }

        }

        private void btnKetnoiCSDL_Click(object sender, EventArgs e) {

			frmKetNoiCSDL frm = new frmKetNoiCSDL();
			frm.ShowDialog();

		}

		private void frmDangNhap_Load(object sender, EventArgs e) {
			tbTaikhoan.Text = Settings.Default.LastAccLogIn;
		}

		private void tbTaikhoan_Or_tbPassword_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter)
				btnDangnhap.PerformClick();
		}




	}
}
