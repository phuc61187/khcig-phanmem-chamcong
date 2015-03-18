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
	public partial class z_old_v12_frm_ThongTinXoaGio : Form {
		public bool fOK = false;
		public cChk fCheck = null;
		public cUserInfo fNhanvien;
		public string fLydo, fGhichu = string.Empty;
		
		public z_old_v12_frm_ThongTinXoaGio() {
			InitializeComponent();
		}

		private void frm_ThongTinSuaGio_Load(object sender, EventArgs e) {
			//1. show các thông tin
			tbMaCC.Text = fNhanvien.UserEnrollNumber.ToString();
			tbTenNV.Text = fNhanvien.UserFullName;
			tbGioCu.Text = fCheck.TimeStr.ToString("dddd d/M/yyyy H:mm:ss");
			List<cShift> tmpDSCa = new List<cShift>(fNhanvien.DSCa);
			tmpDSCa.Insert(0, new cShift() { ShiftID = int.MinValue, ShiftCode = "Tuỳ chỉnh" });
		}



		private void btnDongY_Click(object sender, EventArgs e) {
			fOK = true;
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
