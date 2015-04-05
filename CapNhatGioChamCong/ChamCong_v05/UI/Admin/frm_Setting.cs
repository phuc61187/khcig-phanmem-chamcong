using System;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.DAL;
using ChamCong_v05.Helper;

namespace ChamCong_v05.UI.Admin {
	public partial class frm_Setting : Form {
		public frm_Setting() {
			InitializeComponent();
		}

		private void frm_Setting_Load(object sender, EventArgs e)
		{
			var table = SqlDataAccessHelper.ExecuteQueryString("select * from Setting", null, null);
			for (int i = 0; i < table.Rows.Count; i++) {
				var row = table.Rows[i];
				var id = (int)row["ID"];
				var code = row["Code"].ToString();
				var value = row["Value"].ToString();
				switch (code) {
					#region phụ cấp

					case "PC30":
						var val30 = 0;
						if (int.TryParse(value, out val30)) {
							numPC30.Value = Convert.ToDecimal(val30);
							numPC30.Tag = id;
						}
						break;
					case "PC50":
						var val50 = 0;
						if (int.TryParse(value, out val50)) {
							numPC50.Value = Convert.ToDecimal(val50);
							numPC50.Tag = id;
						}
						break;
					case "PCTCC3":
						var valPCTCC3 = 0;
						if (int.TryParse(value, out valPCTCC3)) {
							numPCTCC3.Value = Convert.ToDecimal(valPCTCC3);
							numPCTCC3.Tag = id;
						}
						break;
					case "PC100":
						var val100 = 0;
						if (int.TryParse(value, out val100)) {
							numPC100.Value = Convert.ToDecimal(val100);
							numPC100.Tag = id;
						}
						break;
					case "PC160":
						var val160 = 0;
						if (int.TryParse(value, out val160)) {
							numPC160.Value = Convert.ToDecimal(val160);
							numPC160.Tag = id;
						}
						break;

					case "PC200":
						var val200 = 0;
						if (int.TryParse(value, out val200)) {
							numPC200.Value = Convert.ToDecimal(val200);
							numPC200.Tag = id;
						}
						break;
					case "PC290":
						var val290 = 0;
						if (int.TryParse(value, out val290)) {
							numPC290.Value = Convert.ToDecimal(val290);
							numPC290.Tag = id;
						}
						break;
					#endregion
					case "TGLamDemToiThieu":
						var valTGLamDemToiThieu = new TimeSpan(0, 0, 0);
						if (TimeSpan.TryParse(value, out valTGLamDemToiThieu)) {
							DateTime d = DateTime.Today.Date;
							dateTimePicker1.Value = new DateTime(d.Year, d.Month, d.Day, valTGLamDemToiThieu.Hours, valTGLamDemToiThieu.Minutes, 0);
							dateTimePicker1.Tag = id;
						}
						break;

					#region số phút cho phép trễ sớm afterot ca tự do

					case "ChoPhepTre":
						var valChoPhepTre = 0;
						if (int.TryParse(value, out valChoPhepTre)) {
							numPhutTre.Value = Convert.ToDecimal(valChoPhepTre);
							numPhutTre.Tag = id;
						}
						break;
					case "ChoPhepSom":
						var valChoPhepSom = 0;
						if (int.TryParse(value, out valChoPhepSom)) {
							numPhutSom.Value = Convert.ToDecimal(valChoPhepSom);
							numPhutSom.Tag = id;
						}
						break;
					case "LamThemAfterOT":
						var valLamThemAfterOT = 0;
						if (int.TryParse(value, out valLamThemAfterOT)) {
							numPhutAfterOT.Value = Convert.ToDecimal(valLamThemAfterOT);
							numPhutAfterOT.Tag = id;
						}
						break;

					#endregion
				}
			}
		}

		private void btnSave_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			var val30 = (int)numPC30.Value;
			DAO5.UpdSetting((int)numPC30.Tag, val30.ToString());
			var val50 = (int)numPC50.Value;
			DAO5.UpdSetting((int)numPC50.Tag, val50.ToString());
			var valPCTCC3 = (int)numPCTCC3.Value;
			DAO5.UpdSetting((int)numPCTCC3.Tag, valPCTCC3.ToString());
			var val100 = (int)numPC100.Value;
			DAO5.UpdSetting((int)numPC100.Tag, val100.ToString());
			var val160 = (int)numPC160.Value;
			DAO5.UpdSetting((int)numPC160.Tag, val160.ToString());
			var val200 = (int)numPC200.Value;
			DAO5.UpdSetting((int)numPC200.Tag, val200.ToString());
			var val290 = (int)numPC290.Value;
			DAO5.UpdSetting((int)numPC290.Tag, val290.ToString());
			var valTGLamDemToiThieu = dateTimePicker1.Value.TimeOfDay;
			DAO5.UpdSetting((int)dateTimePicker1.Tag, valTGLamDemToiThieu.ToString(@"hh\:mm\:") + "00");
			var valChoPhepTre = (int)numPhutTre.Value;
			DAO5.UpdSetting((int)numPhutTre.Tag, valChoPhepTre.ToString());
			var valChoPhepSom = (int)numPhutSom.Value;
			DAO5.UpdSetting((int)numPhutSom.Tag, valChoPhepSom.ToString());
			var valLamThemAfterOT = (int)numPhutAfterOT.Value;
			DAO5.UpdSetting((int)numPhutAfterOT.Tag, valLamThemAfterOT.ToString());
			ACMessageBox.Show("Lưu thông số thành công.", "Thông báo", 2000);
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}
