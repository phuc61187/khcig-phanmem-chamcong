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
	public partial class z_old_v12_frm_ThongTinSuaGio : Form {
		public bool fOK = false;
		public bool fIsChkIn = false;
		public DateTime fGioMoi = DateTime.MinValue;
		public cChk fCheck = null;
		public cUserInfo fNhanvien;
		public string fLydo, fGhichu = string.Empty;


		public z_old_v12_frm_ThongTinSuaGio() {
			InitializeComponent();
		}

		private void frm_ThongTinSuaGio_Load(object sender, EventArgs e) {
			//1. show các thông tin
			tbMaCC.Text = fNhanvien.UserEnrollNumber.ToString();
			tbTenNV.Text = fNhanvien.UserFullName;
			tbGioCu.Text = fCheck.TimeStr.ToString("dddd d/M/yyyy h:mm:ss");
			List<cShift> tmpDSCa = new List<cShift>(fNhanvien.DSCa);
			tmpDSCa.Insert(0, new cShift() { ShiftID = int.MinValue, ShiftCode = "Tuỳ chỉnh" });
			cbCa.DataSource = tmpDSCa;
			cbCa.DisplayMember = "ShiftCode";
			cbCa.ValueMember = "ShiftID";
			dateTimeGioMoi.Value = fCheck.TimeStr;
			rdbtnVao.Checked =  fIsChkIn;
		    rdbtnRa.Checked = !fIsChkIn;
			RegisterEvent();
		}

		private void RegisterEvent() {
			cbCa.SelectedIndexChanged += cbCa_SelectedIndexChanged;
			dateTimeGioMoi.ValueChanged += dateTimeGioMoi_ValueChanged;
			rdbtnVao.CheckedChanged += rdbtnVaoRa_CheckedChanged;
			rdbtnRa.CheckedChanged += rdbtnVaoRa_CheckedChanged;	
		}


		void dateTimeGioMoi_ValueChanged(object sender, EventArgs e) {
			cShift tmpCa = (cShift)cbCa.SelectedItem;
			bool tmpCheckIn = rdbtnVao.Checked;
			if (!((tmpCheckIn && dateTimeGioMoi.Value.TimeOfDay == tmpCa.OnnDutyTS) || (tmpCheckIn == false && dateTimeGioMoi.Value.TimeOfDay == tmpCa.OffDutyTS))) {
				cbCa.SelectedIndexChanged -= cbCa_SelectedIndexChanged;
				cbCa.SelectedIndex = 0;
				cbCa.Refresh();
				cbCa.SelectedIndexChanged += cbCa_SelectedIndexChanged;
			}
		}

		private void cbCa_SelectedIndexChanged(object sender, EventArgs e) {
			cShift tmpCa = (cShift)cbCa.SelectedItem;
			DateTime gio = (rdbtnVao.Checked) ? dateTimeGioMoi.Value.Date.Add(tmpCa.OnnDutyTS) : dateTimeGioMoi.Value.Date.Add(tmpCa.OffDutyTS);

			dateTimeGioMoi.Value = new DateTime(gio.Year, gio.Month, gio.Day, gio.Hour, gio.Minute, 0);

		}

		private void rdbtnVaoRa_CheckedChanged(object sender, EventArgs e) {
			cShift tmpCa = (cShift)cbCa.SelectedItem;
			bool tmpKieuGioVao = rdbtnVao.Checked;
			if (!((tmpKieuGioVao && dateTimeGioMoi.Value.TimeOfDay == tmpCa.OnnDutyTS) || (tmpKieuGioVao == false && dateTimeGioMoi.Value.TimeOfDay == tmpCa.OffDutyTS))) {
				cbCa.SelectedIndexChanged -= cbCa_SelectedIndexChanged;
				cbCa.SelectedIndex = 0;
				cbCa.Refresh();
				cbCa.SelectedIndexChanged += cbCa_SelectedIndexChanged;
			}
		}

		private void btnDongY_Click(object sender, EventArgs e) {
			fOK = true;
			fGioMoi = dateTimeGioMoi.Value;
			fIsChkIn = rdbtnVao.Checked;
			fLydo = string.IsNullOrEmpty(cbLyDo.SelectedText) ? cbLyDo.SelectedText : cbLyDo.SelectedItem.ToString();
			fGhichu = tbGhichu.Text;

			Close();
		}

		private void btnHuyBo_Click(object sender, EventArgs e) {
			fOK = false;
			Close();
		}

	}
}
