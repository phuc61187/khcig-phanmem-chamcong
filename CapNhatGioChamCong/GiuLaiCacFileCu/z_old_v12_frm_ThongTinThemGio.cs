using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GiuLaiCacFileCu.DTO;

namespace GiuLaiCacFileCu {
	public partial class z_old_v12_frm_ThongTinThemGio : Form {
		public bool fOK = false;
		public bool fThemGioVao = false;
		public bool fThemGioRa = false;
		public DateTime fGioVao, fGioRa = DateTime.MinValue;
		public DateTime fNgay = DateTime.MinValue;
		public cUserInfo fNhanvien;
		public string fLydo, fGhichu = string.Empty;
		//public 

		public z_old_v12_frm_ThongTinThemGio() {
			InitializeComponent();
		}

		private void frm_ThongTinThemGio_Load(object sender, EventArgs e) {
			//1. show các thông tin
			tbMaCC.Text = fNhanvien.UserEnrollNumber.ToString();
			tbTenNV.Text = fNhanvien.UserFullName;
			dateTimeVaoMoi.Value = dateTimeRaMoi.Value = fNgay;
			List<cShift> tmpDSCa = new List<cShift>(fNhanvien.DSCa);
			tmpDSCa.Insert(0, new cShift() { ShiftID = int.MinValue, ShiftCode = "Tuỳ chỉnh" });
			cbVaoCa.DataSource = tmpDSCa;
			cbVaoCa.DisplayMember = "ShiftCode";
			cbVaoCa.ValueMember = "ShiftID";

			RegisterEvent();
		}

		private void RegisterEvent() {
			cbVaoCa.SelectedIndexChanged += cbVaoCa_SelectedIndexChanged;
			dateTimeVaoMoi.ValueChanged += dateTimeVaoMoi_ValueChanged;
			dateTimeRaMoi.ValueChanged += dateTimeRaMoi_ValueChanged;
		}

		void dateTimeRaMoi_ValueChanged(object sender, EventArgs e) {
			cShift tmpCa = (cShift)cbVaoCa.SelectedItem;
			if (dateTimeRaMoi.Value.TimeOfDay != tmpCa.OffDutyTS) {
				cbVaoCa.SelectedIndexChanged -= cbVaoCa_SelectedIndexChanged;
				cbVaoCa.SelectedIndex = 0;
				cbVaoCa.Refresh();
				cbVaoCa.SelectedIndexChanged += cbVaoCa_SelectedIndexChanged;
			}
		}

		void dateTimeVaoMoi_ValueChanged(object sender, EventArgs e) {
			cShift tmpCa = (cShift)cbVaoCa.SelectedItem;
			if (dateTimeVaoMoi.Value.TimeOfDay != tmpCa.OnnDutyTS) {
				cbVaoCa.SelectedIndexChanged -= cbVaoCa_SelectedIndexChanged;
				cbVaoCa.SelectedIndex = 0;
				cbVaoCa.Refresh();
				cbVaoCa.SelectedIndexChanged += cbVaoCa_SelectedIndexChanged;
			}
		}

		private void cbVaoCa_SelectedIndexChanged(object sender, EventArgs e) {
			cShift tmpCa = (cShift)cbVaoCa.SelectedItem;
			DateTime dateVao = dateTimeVaoMoi.Value.Date.Add(tmpCa.OnnDutyTS);
			DateTime dateRa = dateTimeRaMoi.Value.Date.Add(tmpCa.OffDutyTS);
			dateTimeVaoMoi.Value = new DateTime(dateVao.Year, dateVao.Month, dateVao.Day, dateVao.Hour, dateVao.Minute, 0);
			dateTimeRaMoi.Value = new DateTime(dateRa.Year, dateRa.Month, dateRa.Day, dateRa.Hour, dateRa.Minute, 0);
		}

		private void chkbThemGioRa_CheckedChanged(object sender, EventArgs e) {
			fThemGioRa = dateTimeRaMoi.Enabled = chkbGioRa.Checked;
		}

		private void chkbThemGioVao_CheckedChanged(object sender, EventArgs e) {
			fThemGioVao = dateTimeVaoMoi.Enabled = chkbGioVao.Checked;
		}

		private void btnDongY_Click(object sender, EventArgs e) {
			if (fThemGioVao == false && fThemGioRa == false) fOK = false;
			else {
				fOK = true;
				if (fThemGioVao) fGioVao = dateTimeVaoMoi.Value;
				if (fThemGioRa) fGioRa = dateTimeRaMoi.Value;
				fLydo = string.IsNullOrEmpty(cbLyDo.SelectedText) ? cbLyDo.SelectedItem.ToString() : cbLyDo.SelectedText;
				fGhichu = tbGhichu.Text;
			}
			Close();
		}

		private void btnHuyBo_Click(object sender, EventArgs e) {
			fOK = false;
			Close();
		}
	}
}
