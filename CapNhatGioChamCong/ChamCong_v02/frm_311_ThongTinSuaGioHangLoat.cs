using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ChamCong_v02.DTO;

namespace ChamCong_v02 {
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
			List<cShift> tmpDSCa = new List<cShift>(ThamSo.DSCa);
			tmpDSCa.Insert(0, new cShift() { ShiftID = int.MinValue, ShiftCode = "Tuỳ chỉnh" });
			cbCa.DisplayMember = "ShiftCode";
			cbCa.ValueMember = "ShiftID";
			cbCa.DataSource = tmpDSCa;
			dateTimeGioMoi.Value = fNgaySuaMacDinh.Date;
			switchButton1.Value = fCheckIn;
			RegisterEvent();
		}

		private void RegisterEvent() {
			cbCa.SelectedIndexChanged += cbCa_SelectedIndexChanged;
			dateTimeGioMoi.ValueChanged += dateTimeGioMoi_ValueChanged;
			switchButton1.ValueChanged += switchButton1_ValueChanged;
		}


		void dateTimeGioMoi_ValueChanged(object sender, EventArgs e) {
			cShift tmpCa = (cShift)cbCa.SelectedItem;
			bool tmpCheckIn = switchButton1.Value;
			if (!((tmpCheckIn && dateTimeGioMoi.Value.TimeOfDay == tmpCa.OnnDutyTS) || (tmpCheckIn == false && dateTimeGioMoi.Value.TimeOfDay == tmpCa.OffDutyTS))) {
				cbCa.SelectedIndexChanged -= cbCa_SelectedIndexChanged;
				cbCa.SelectedIndex = 0;
				cbCa.Refresh();
				cbCa.SelectedIndexChanged += cbCa_SelectedIndexChanged;
			}
		}

		private void cbCa_SelectedIndexChanged(object sender, EventArgs e) {
			cShift tmpCa = (cShift)cbCa.SelectedItem;
			DateTime gio = (switchButton1.Value) ? dateTimeGioMoi.Value.Date.Add(tmpCa.OnnDutyTS) : dateTimeGioMoi.Value.Date.Add(tmpCa.OffDutyTS);

			dateTimeGioMoi.Value = new DateTime(gio.Year, gio.Month, gio.Day, gio.Hour, gio.Minute, 0);

		}


		private void btnDongY_Click(object sender, EventArgs e) {
			fOK = true;
			fGioMoi = dateTimeGioMoi.Value;
			fCheckIn = switchButton1.Value;
			fLydo = cbLyDo.SelectedItem == null ? cbLyDo.Text : cbLyDo.SelectedItem.ToString();
			fGhichu = tbGhichu.Text;

			Close();
		}

		private void btnHuyBo_Click(object sender, EventArgs e) {
			fOK = false;
			Close();
		}

		private void switchButton1_ValueChanged(object sender, EventArgs e) {
			cShift tmpCa = (cShift)cbCa.SelectedItem;
			bool tmpKieuGioVao = switchButton1.Value;
			if (!((tmpKieuGioVao && dateTimeGioMoi.Value.TimeOfDay == tmpCa.OnnDutyTS) || (tmpKieuGioVao == false && dateTimeGioMoi.Value.TimeOfDay == tmpCa.OffDutyTS))) {
				cbCa.SelectedIndexChanged -= cbCa_SelectedIndexChanged;
				cbCa.SelectedIndex = 0;
				cbCa.Refresh();
				cbCa.SelectedIndexChanged += cbCa_SelectedIndexChanged;
			}
		}

	}
}
