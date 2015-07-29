using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.Helper;

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
					ACMessageBox.Show("ok", " ", 1000);

				}
			}
		}
	}
}
