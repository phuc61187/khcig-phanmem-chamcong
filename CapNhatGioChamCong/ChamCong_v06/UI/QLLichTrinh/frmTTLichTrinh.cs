using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.Helper;

namespace ChamCong_v06.UI.QLLichTrinh {
	public partial class frmTTLichTrinh : Form {
		public ModeType Mode = ModeType.Cancel;
		public string m_TenLichTrinh = string.Empty;
		public frmTTLichTrinh() {
			InitializeComponent();
		}

		private void btnHuy_Click(object sender, EventArgs e) {
			Mode = ModeType.Cancel;
			Close();
		}

		private void btnLuu_Click(object sender, EventArgs e) {
			Mode = ModeType.Them;
			if (string.IsNullOrEmpty(textBox1.Text))
			{
				ACMessageBox.Show("Chưa nhập tên lịch trình.", Resources.Caption_ThongBao, 2000);
				return;
			}
			this.m_TenLichTrinh = textBox1.Text;

			Close();
		}
	}
}
