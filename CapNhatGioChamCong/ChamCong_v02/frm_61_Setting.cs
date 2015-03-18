using System;
using System.Windows.Forms;
using ChamCong_v02.BUS;

namespace ChamCong_v02 {
	public partial class frm_61_Setting : Form {
		public frm_61_Setting() {
			InitializeComponent();
		}

		private void frm_Setting_Load(object sender, EventArgs e) {
			//dateTimePicker1.Value = new DateTime( Properties.Settings.Default.TGLamDemToiThieu 
			DateTime temp = new DateTime(2000, 1, 1, Properties.Settings.Default.TGLamDemToiThieu.Hours, Properties.Settings.Default.TGLamDemToiThieu.Minutes, Properties.Settings.Default.TGLamDemToiThieu.Seconds);
			dateTimePicker1.Value = temp;
			numericUpDown1.Value = Properties.Settings.Default.PCDem;
			numericUpDown2.Value = Properties.Settings.Default.PCTangCuong;
		}

		private void btnSave_Click(object sender, EventArgs e) {
			TimeSpan TGLamDemToiThieu = dateTimePicker1.Value - (dateTimePicker1.Value.Date);
			Properties.Settings.Default.TGLamDemToiThieu = TGLamDemToiThieu;
			Properties.Settings.Default.PCDem = numericUpDown1.Value;
			Properties.Settings.Default.PCTangCuong = numericUpDown2.Value;
			Properties.Settings.Default.Save();
			AutoClosingMessageBox.Show("Lưu thông số thành công.", "Thông báo", 2000);
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}
