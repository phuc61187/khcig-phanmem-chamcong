using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.BUS;
using ChamCong_v06.DAL;
using ChamCong_v06.Helper;
using DevExpress.XtraEditors;

namespace ChamCong_v06.UI {
	public partial class frmChangePassword : Form {
		public frmChangePassword() {
			InitializeComponent();
		}

		private void btnEdit_Clear_Click(object sender, EventArgs e)
		{
			ButtonEdit button = (ButtonEdit) sender;
			button.Text = string.Empty;
		}

		private void btnThaydoi_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			string oldPass = btnEditMatkhauCu.Text;
			string newPass = btnEditMatkhauMoi1.Text;
			string testPass = btnEditMatkhauMoi2.Text;
			string account = XL2.currUserAccount;
			if (newPass == testPass)
			{
				ACMessageBox.Show("Vui lòng nhập 2 mật khẩu mới giống nhau.", Resources.Caption_Loi, 2000);
				return;
			}
			if (DAO5.ChangePassword(oldPass, newPass, account))
			{
				ACMessageBox.Show(Resources.Text_DaThucHienXong, Resources.Caption_ThongBao, 2000);
				this.Close();
			}
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			Close();
		}
	}
}
