using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.DAL;
using ChamCong_v06.Helper;

namespace ChamCong_v06.UI.QLLichTrinh {
	public partial class frmTTCaLamViec : Form {
		public ModeType Mode = ModeType.Cancel;
		public DataRow m_CurrentRow = null;
		public int m_ShiftID = -1;
		public string m_ShiftCode = string.Empty;
		public TimeSpan m_OnDuty = TimeSpan.Zero;
		public TimeSpan m_OffDuty = TimeSpan.Zero;
		public int m_DayCount = 0;
		public float m_ChamCong = 0;
		public int m_OnTimeInMin = 0;
		public int m_CutInMin = 0;
		public int m_OnTimeOutMin = 0;
		public int m_CutOutMin = 0;
		public int m_LateGraceMin = 0;
		public int m_EarlyGraceMin = 0;
		public int m_AfterOTMin = 0;
		public string m_KyHieuCC = string.Empty;
		public TimeSpan m_OnLunch = TimeSpan.Zero;
		public TimeSpan m_OffLunch = TimeSpan.Zero;
		public bool m_Enable = true;
		//public int m_ = 0;


		#region hàm ko quan trọng
		public frmTTCaLamViec() {
			InitializeComponent();
		}

		private void btnHuy_Click(object sender, EventArgs e) {
			Mode = ModeType.Cancel;
			Close();
		}

		#endregion
		private void frmTTCaLamViec_Load(object sender, EventArgs e) {
			if (this.Mode == ModeType.Them) {
			}
			else if (this.Mode == ModeType.Sua) {
				lbShiftID.Tag = (int)m_CurrentRow["ShiftID"];
				textBoxShiftcode.Text = m_CurrentRow["ShiftCode"].ToString();
				maskedTextBoxVao.Text = ((TimeSpan)m_CurrentRow["OnDuty"]).ToString("H:mm");
				maskedTextBoxRaa.Text = ((TimeSpan)m_CurrentRow["OffDuty"]).ToString("H:mm");
				textBoxWKTime.Text = ((TimeSpan)m_CurrentRow["WorkingTime"]).TotalMinutes.ToString("###0");
				maskedTextBoxWKDay.Text = ((float)m_CurrentRow["Workingday"]).ToString("0.0");
				numOnnInn.Value = (int)m_CurrentRow["OnTimeInMin"];
				numCutInn.Value = (int)m_CurrentRow["CutInMin"];
				numOnnOut.Value = (int)m_CurrentRow["OnTimeOutMin"];
				numCutOut.Value = (int)m_CurrentRow["CutOutMin"];
				numLateGrace.Value = (int)m_CurrentRow["LateGrace"];
				numEarlyGrace.Value = (int)m_CurrentRow["EarlyGrace"];
				numAfterOT.Value = (int)m_CurrentRow["AfterOT"];
				textBoxKyhieu.Text = m_CurrentRow["KyHieuCC"].ToString();
				checkBoxEnable.Checked = (bool)m_CurrentRow["Enable"];
				TimeSpan OnLunch = (TimeSpan)m_CurrentRow["OnLunch"];
				TimeSpan OffLunch = (TimeSpan)m_CurrentRow["OffLunch"];
				if ((OnLunch == TimeSpan.Zero && OffLunch == TimeSpan.Zero)
					|| (OffLunch - OnLunch).Duration() < new TimeSpan(0, 0, 1, 0))
					checkTinhThoigianNghiTrua.Checked = false;
				else checkTinhThoigianNghiTrua.Checked = true;
				maskedTextBoxBDLunch.Text = OnLunch.ToString("H:mm");
				maskedTextBoxKTLunch.Text = OffLunch.ToString("H:mm");
			}
		}

		private void btnLuu_Click(object sender, EventArgs e) {
			//1. kiểm tra dữ liệu nhập ko hợp lệ thì báo
			//2. thực hiện
			string shiftCode;
			TimeSpan onDuty, offDuty, onLunch, offLunch;
			int dayCount;
			int shiftID, onTimeInMin, onTimeOutMin, cutInMin, cutOutMin, lateGraceMin, earlyGraceMin, afterOTMin;

			float chamCong;

			//1.
			if (ValidateValueForm(out shiftCode, out onDuty, out offDuty, out dayCount, out chamCong, out onTimeInMin, out cutInMin, out onTimeOutMin, out cutOutMin,
				out lateGraceMin, out earlyGraceMin, out afterOTMin, out onLunch, out offLunch) == false) {
				ACMessageBox.Show("Dữ liệu nhập chưa hợp lệ. Vui lòng kiểm tra lại!", Resources.Caption_Loi, 3000);
				return;
			}

			//2.
			m_ShiftID = (lbShiftID.Tag != null) ? (int)lbShiftID.Tag : -1;
			m_ShiftCode = shiftCode;
			m_OnDuty = onDuty;
			m_OffDuty = offDuty;
			m_DayCount = dayCount;
			//tongiolam
			m_ChamCong = chamCong;
			m_OnTimeInMin = onTimeInMin;
			m_CutInMin = cutInMin;
			m_OnTimeOutMin = onTimeOutMin;
			m_CutOutMin = cutOutMin;
			m_EarlyGraceMin = earlyGraceMin;
			m_LateGraceMin = lateGraceMin;
			m_AfterOTMin = afterOTMin;
			m_KyHieuCC = textBoxKyhieu.Text;
			m_OnLunch = onLunch;
			m_OffLunch = offLunch;
			m_Enable = checkBoxEnable.Checked;

		}

		private bool ValidateValueForm(out string shiftCode, out TimeSpan onDuty, out TimeSpan offDuty, out int DayCount, out float chamCong,
			out int onTimeInMin, out int cutInMin, out int onTimeOutMin, out int cutOutMin, out int lateGraceMin, out int earlyGraceMin, out int afterOTMin,
			out TimeSpan onLunch, out TimeSpan offLunch) {
			shiftCode = string.Empty;
			onDuty = new TimeSpan();
			offDuty = new TimeSpan();
			onLunch = new TimeSpan();
			offLunch = new TimeSpan();
			DayCount = 0;
			chamCong = 0f;
			onTimeInMin = 0;
			cutInMin = 0;
			onTimeOutMin = 0;
			cutOutMin = 0;
			lateGraceMin = 0;
			earlyGraceMin = 0;
			afterOTMin = 0;
			if (string.IsNullOrEmpty(textBoxKyhieu.Text)) return false;
			else shiftCode = textBoxKyhieu.Text;
			if (TimeSpan.TryParse(maskedTextBoxVao.Text, out onDuty) == false)
				return false;
			if (TimeSpan.TryParse(maskedTextBoxRaa.Text, out offDuty) == false)
				return false;
			if (offDuty < onDuty) DayCount = 1;
			offDuty = offDuty.Add(new TimeSpan(DayCount, 0, 0, 0));
			var tongGioLam = offDuty - onDuty;
			if (tongGioLam < TimeSpan.Zero) return false;
			if (float.TryParse(maskedTextBoxWKDay.ToString(), out chamCong) == false)
				return false;
			onTimeInMin = (int)numOnnInn.Value;
			cutInMin = (int)numCutInn.Value;
			onTimeOutMin = (int)numOnnOut.Value;
			cutOutMin = (int)numCutOut.Value;
			var bien1 = onDuty.Subtract(new TimeSpan(0, onTimeInMin, 0));// nếu ON - ONIN mà trước 0g thì ko cho phép
			var bien2 = offDuty.Add(new TimeSpan(0, cutOutMin, 0));// nếu OFF + cut mà sau 24 giờ hôm sau thì ko cho phép
			if (bien1 < TimeSpan.Zero) return false;
			if (bien2.TotalDays > 2f) return false;

			if (checkTinhThoigianNghiTrua.Checked) {
				if (TimeSpan.TryParse(maskedTextBoxBDLunch.Text, out onLunch) == false) return false;
				if (TimeSpan.TryParse(maskedTextBoxKTLunch.Text, out offLunch) == false) return false;
				if (onLunch > offLunch) return false;
			}
			else {
				onLunch = TimeSpan.Zero;
				offLunch = TimeSpan.Zero;
			}

			return true;
		}

	}
}
