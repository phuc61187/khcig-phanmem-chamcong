using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DTO;

namespace ChamCong_v03 {
	public partial class frm_311_ThongTinSuaGioHangLoat : Form {
		public bool fOK = false;
		public bool fCheckIn = false;
		public DateTime fNgaySuaMacDinh = DateTime.MinValue;
		public DateTime fGioMoi = DateTime.MinValue;
		public string fLydo, fGhichu = string.Empty;


		public frm_311_ThongTinSuaGioHangLoat() {
			InitializeComponent();
		}

		private void frm_ThongTinSuaGio_Load(object sender, EventArgs e) {
			//1. show các thông tin
            List<cCaAbs> tmpDSCa = new List<cCaAbs>(XL.DSCa);
			tmpDSCa.Insert(0, new cCaTuDo() { ID = int.MinValue, Code = "Tuỳ chỉnh" });
			cbCa.DisplayMember = "Code";
			cbCa.ValueMember = "ID";
			cbCa.DataSource = tmpDSCa;
			dateTimeGioMoi.Value = fNgaySuaMacDinh.Date;
			radVao.Checked = fCheckIn;
			RegisterEvent();
		}

		private void RegisterEvent() {
			cbCa.SelectedIndexChanged += cbCa_SelectedIndexChanged;
			dateTimeGioMoi.ValueChanged += dateTimeGioMoi_ValueChanged;
			radVao.CheckedChanged += switchButton1_ValueChanged;
		}


		void dateTimeGioMoi_ValueChanged(object sender, EventArgs e) {
			cCaAbs tmpCa = (cCaAbs)cbCa.SelectedItem;
            bool tmpCheckIn = radVao.Checked;
			if (!((tmpCheckIn && dateTimeGioMoi.Value.TimeOfDay == tmpCa.OnnTS) || (tmpCheckIn == false && dateTimeGioMoi.Value.TimeOfDay == tmpCa.OffTS))) {
				cbCa.SelectedIndexChanged -= cbCa_SelectedIndexChanged;
				cbCa.SelectedIndex = 0;
				cbCa.Refresh();
				cbCa.SelectedIndexChanged += cbCa_SelectedIndexChanged;
			}
		}

		private void cbCa_SelectedIndexChanged(object sender, EventArgs e) {
			cCaAbs tmpCa = (cCaAbs)cbCa.SelectedItem;
            DateTime gio = (radVao.Checked) ? dateTimeGioMoi.Value.Date.Add(tmpCa.OnnTS) : dateTimeGioMoi.Value.Date.Add(tmpCa.OffTS);

			dateTimeGioMoi.Value = new DateTime(gio.Year, gio.Month, gio.Day, gio.Hour, gio.Minute, 0);

		}


		private void btnDongY_Click(object sender, EventArgs e) {
			fOK = true;
			fGioMoi = dateTimeGioMoi.Value;
		    fCheckIn = radVao.Checked;
			fLydo = cbLyDo.SelectedItem == null ? cbLyDo.Text : cbLyDo.SelectedItem.ToString();
			fGhichu = tbGhichu.Text;

			Close();
		}

		private void btnHuyBo_Click(object sender, EventArgs e) {
			fOK = false;
			Close();
		}

		private void switchButton1_ValueChanged(object sender, EventArgs e) {
			cCaAbs tmpCa = (cCaAbs)cbCa.SelectedItem;
		    bool tmpKieuGioVao = radVao.Checked;
			if (!((tmpKieuGioVao && dateTimeGioMoi.Value.TimeOfDay == tmpCa.OnnTS) || (tmpKieuGioVao == false && dateTimeGioMoi.Value.TimeOfDay == tmpCa.OffTS))) {
				cbCa.SelectedIndexChanged -= cbCa_SelectedIndexChanged;
				cbCa.SelectedIndex = 0;
				cbCa.Refresh();
				cbCa.SelectedIndexChanged += cbCa_SelectedIndexChanged;
			}
		    lbKieuGio.Text = string.Format("Giờ {0}", (radVao.Checked ? "vào" : "ra"));
		}



	}
}
