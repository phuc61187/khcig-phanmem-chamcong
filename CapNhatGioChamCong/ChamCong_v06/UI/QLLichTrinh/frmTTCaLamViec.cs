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
			if (this.Mode == ModeType.Them)
			{
			}
			else if (this.Mode == ModeType.Sua)
			{
			}
		}

		private void btnLuu_Click(object sender, EventArgs e)
		{
			string shiftCode;
			TimeSpan onDuty, offDuty;
			int shiftID, onTimeInMin, onTimeOutMin, cutInMin, cutOutMin, lateGraceMin, earlyGraceMin,afterOTMin;
			
			float chamCong;

			if (ValidateValueForm(out shiftCode, out OnTimeIn, out  OnTimeOut) == false)
			{

			}
			//string shiftCode = textBoxKyhieu.Text == string.Empty ? re
		}

	}
}
