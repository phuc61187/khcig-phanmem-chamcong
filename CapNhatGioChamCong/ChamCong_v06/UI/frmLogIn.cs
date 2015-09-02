using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ChamCong_v06.BUS;
using ChamCong_v06.Helper;
using ChamCong_v06.Properties;
using ChamCong_v06.UI.QLPhong;

namespace ChamCong_v06.UI {
	public partial class frmLogIn : Form
	{
		public bool m_LogInStatus;
		#region khu vực hàm ko quan trọng
		public frmLogIn() {
			InitializeComponent();
		}

		private void btnEdit_Clear_Click(object sender, EventArgs e) {
			ButtonEdit button = (ButtonEdit)sender;
			button.Text = string.Empty;
		}

		private void btnKetnoiCSDL_Click(object sender, EventArgs e) {
			frmKetNoiCSDL frm = new frmKetNoiCSDL();
			frm.ShowDialog();
		}

		private void btnThoat_Click(object sender, EventArgs e)
		{
			// lưu lại username lần đăng nhập cuối và thoát ứng dụng
			Settings.Default.LastAccLogIn = btnEditTaikhoan.Text;
			Settings.Default.Save();
			Application.Exit();
		}
		#endregion

		private void btnDangnhap_Click(object sender, EventArgs e) {
			#region lay du lieu tu form
			string tempUsername = btnEditTaikhoan.Text, tempPassword = btnEditMatkhau.Text;

			var passroot = string.Empty;
			passroot = ((DateTime.Now.Minute % 2 == 0))
				? DateTime.Now.Minute + "@" + DateTime.Now.Hour + "$" + DateTime.Now.Month + "^" + DateTime.Now.Day
				: DateTime.Now.Minute + "!" + DateTime.Now.Hour + "#" + DateTime.Now.Month + "%" + DateTime.Now.Day;
			#endregion


			//1.  kiểm tra kết nối csdl trước rồi kiểm tra tài khoản đăng nhập
			//1.1 ko có kết nối hoặc tài khoản ko đúng thì ko bật flag login
			//1.2 đăng nhập thành công thì bật cờ,đóng form này lại //todo 
			string tmpConnStr = string.Empty, currUserAccount = string.Empty;
			int currUserID = 0;
            LoaiTK loaiTK = LoaiTK.None;
			if (Settings.Default.EncryptConnectionString == string.Empty)
			{
				ACMessageBox.Show("Chưa có kết nối CSDL.", Resources.Caption_Loi, 2000);
				return;
			}
			tmpConnStr = MyUtility.giaima(Settings.Default.EncryptConnectionString);
			var kq = XL.CheckLogIn(tempUsername, tempPassword, passroot,
				ref tmpConnStr, out loaiTK, out currUserID, out currUserAccount);
			if (!kq) {
				btnEditMatkhau.Text = string.Empty;
				return;
			}

			//1.2 đang nhập thành công thì chuẩn bị các dữ liệu toàn cục
            // nếu tài khoản LocalRoot thì được mở các form...//todo hiển thị các form nào?

            // nếu tài khoản thường thì chuẩn bị các biến toàn cục: 
            // a. danh sách phòng ban được phép thao tác, 
            // b. thông tin ca tự do phần mềm thiết lập, áp dụng cho runtime
			GlobalVariables.CurrentUserID = currUserID;
			GlobalVariables.CurrentUserAccount = currUserAccount;
            GlobalVariables.DocServerSetting5();
            GlobalVariables.SettingCaTuDo();
			this.m_LogInStatus = true;
			this.Close();
			return;
			//XL.KhoiTaoDSPhongBan(XL2.TatcaPhongban); // logic khởi tạo ds tất cả phòng ban mà tài khoản này được thao tác

			//XL2.QuyenThaoTac = XL.LayPhanQuyen();

			//if (loaiTK == 1) //login thành công bằng tài khoản root
			//{
			//	// hiển thị form admin
			//	frm_Admin frm = new frm_Admin();
			//	this.Hide();
			//	frm.Show();
			//}
			//else {

			//	XL.SaveSetting(lastAccLogIn: currUserAccount);
			//	XL.ChuanBiDSLichTrinhVaCa();
			//	/*
			//					SqlDataAccessHelper.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=WiseEyeV5Express;Integrated Security=true";
			//					fmXemCong4 fm = new fmXemCong4();
			//					fm.Show();
			//	*/
			//	/*
			//					fmTKeCongTheoNVu frm = new fmTKeCongTheoNVu();
			//					this.Hide();
			//					frm.ShowDialog();
			//					this.Close();
			//	*/

			//	frm_main frm = new frm_main();
			//	this.Hide();
			//	frm.ShowDialog();
			//	this.Close();

				// hiển thị form tài khoản thường//}

		}


	}
}
