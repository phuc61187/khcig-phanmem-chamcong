using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.Helper;

namespace ChamCong_v06.UI.QLTaiKhoan {
	public partial class frmNhapTTTaiKhoan : Form {
		public string m_TenTaiKhoan;
		public string m_Password;
		public bool m_Enable;
		public ModeType m_Mode;

		#region hàm ko quan trọng
		public frmNhapTTTaiKhoan() {
			InitializeComponent();
		}

		private void btnTenPhong_Properties_ClearButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
			MyUtility.ClearButtonEditText(sender);
		}

		private void btnEdit_Clear_Click(object sender, EventArgs e) {
			MyUtility.ClearButtonEditText(sender);
		}

		private void btnEdit_Clear_Click(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
			MyUtility.ClearButtonEditText(sender);
		}

		private void btnHuy_Click(object sender, EventArgs e) {
			this.m_Mode = ModeType.Cancel;
			Close();
		}
		#endregion

		private void btnLuu_Click(object sender, EventArgs e) {
			string tenTaiKhoan = btnTenTaiKhoan.Text;
			tenTaiKhoan = tenTaiKhoan.Trim();
			string password1 = btnEditMatkhau.Text;
			string password2 = btnEditMatkhau2.Text;
			bool IsEnable = checkEnable.Checked;

			if (m_Mode == ModeType.Them || m_Mode == ModeType.Sua) { // chế độ thêm hoặc sửa thì kiểm tra xem tên tài khoản có trống ko? các chế độ khác ko cần
				if (tenTaiKhoan == string.Empty) {
					ACMessageBox.Show("Tên tài khoản không được để trống.", Resources.Caption_ThongBao, 2000);
					return;
				}
			}
			if (m_Mode == ModeType.Them || m_Mode == ModeType.Other) {// chế độ đổi pass thì kiểm tra password
				if (password1 != password2) {
					ACMessageBox.Show("Mật khẩu chưa khớp nhau. Vui lòng nhập lại.", Resources.Caption_ThongBao, 2000);
					btnEditMatkhau.Text = string.Empty;
					btnEditMatkhau2.Text = string.Empty;
					return;
				}
			}

			this.m_TenTaiKhoan = tenTaiKhoan;
			this.m_Password = password1;
			this.m_Enable = IsEnable;

			this.Close();
		}

		private void frmNhapTTTaiKhoan_Load(object sender, EventArgs e) {
			if (this.m_Mode == ModeType.Them) {// check sẵn enable = true và ko cho sửa
				checkEnable.Checked = true;
				checkEnable.Enabled = false;
			}
			else if (this.m_Mode == ModeType.Sua) {
				//chỉ cho sửa tên tài khoản và check Enable , ko cho sửa mật khẩu
				btnTenTaiKhoan.Text = this.m_TenTaiKhoan;
				checkEnable.Checked = this.m_Enable;
				btnEditMatkhau.Enabled = false;
				btnEditMatkhau2.Enabled = false;
			}
			else if (this.m_Mode == ModeType.Other) {//reset password
				//ko cho sửa tên tài khoản và check enable
				btnTenTaiKhoan.Text = this.m_TenTaiKhoan;
				btnTenTaiKhoan.Enabled = false;
				checkEnable.Checked = m_Enable;
				checkEnable.Enabled = false;
			}
		}
	}

}
