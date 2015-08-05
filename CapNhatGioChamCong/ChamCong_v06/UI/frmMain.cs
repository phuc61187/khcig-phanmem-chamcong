using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.BUS;
using ChamCong_v06.Helper;
using ChamCong_v06.UI.QLLichTrinh;
using ChamCong_v06.UI.QLPhong;

namespace ChamCong_v06.UI {
	public partial class frmMain : Form
	{
		public bool m_LogInStatus;
		#region các hàm ko quan trọng
		public frmMain() {
			InitializeComponent();
		}
		#endregion

		private void frmMain_Load(object sender, EventArgs e) {
			//1. nếu chưa login thì hiển thị form login
			if (!this.m_LogInStatus)
			{
				frmLogIn frm = new frmLogIn();
				frm.ShowDialog();
				if (frm.m_LogInStatus)
				{
					//todo 
					frmQLLichTrinh frm1 = new frmQLLichTrinh();
					frm1.MdiParent = this;
					frm1.Show();
				}
			}
		}

		int LayVitriForm(Form mainForm, Type childType) {
			if (mainForm.MdiChildren.Length == 0) return -1;
			for (int i = 0; i < mainForm.MdiChildren.Length; i++) {
				if (mainForm.MdiChildren[i].GetType() == childType)
					return i;
			}
			return -1;
		}
		private void resetPassToolStripMenuItem_Click(object sender, EventArgs e) {
/*
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.DiemDanh) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}
*/
			frmChangePassword frm1 = new frmChangePassword();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = MdiChildren[indexForm] as frmChangePassword;
				if (frm1 != null) frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}

		}

		private void thoatToolStripMenuItem_Click(object sender, EventArgs e) {
			Application.Exit();
		}
	}
}
