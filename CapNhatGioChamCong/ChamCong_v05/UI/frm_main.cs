using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.Helper;
using ChamCong_v05.Properties;
using ChamCong_v05.UI.Admin;
using ChamCong_v05.UI.ChamCong;
using ChamCong_v05.UI.KhaiBao;
using ChamCong_v05.UI.QLNV;
using ChamCong_v05.UI.TinhLuong;
using ChamCong_v05.UI.XepLich;
using log4net;
using ChamCong_v05.UI4._5;


namespace ChamCong_v05.UI {
	public partial class frm_main : Form {
		public readonly ILog lg = LogManager.GetLogger("frm_main");

		public frm_main() {
			InitializeComponent();

			//config log
			log4net.Config.XmlConfigurator.Configure();

		}

		private void frm_main_Load(object sender, EventArgs e) {
			XL.ChuanBiDSLichTrinhVaCa();
			XL.DocServerSetting5();
		}



		int LayVitriForm(Form mainForm, Type childType) {
			if (mainForm.MdiChildren.Length == 0) return -1;
			for (int i = 0; i < mainForm.MdiChildren.Length; i++) {
				if (mainForm.MdiChildren[i].GetType() == childType)
					return i;
			}
			return -1;
		}

		private void SubMenu_DiemDanh_Click(object sender, EventArgs e) {
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.DiemDanh) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}
			frm_DiemDanhNV frm1 = new frm_DiemDanhNV();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = MdiChildren[indexForm] as frm_DiemDanhNV;
				if (frm1 != null) frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Maximized;
				frm1.Show();
			}
		}
		private void SubMenu_XemCongNV_Click(object sender, EventArgs e) {
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.XemCong) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}

			fmXemCong4 frm1 = new fmXemCong4();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = MdiChildren[indexForm] as fmXemCong4;
				if (frm1 != null) frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Maximized;
				frm1.Show();
			}
		}

		private void Menu_KhaiBaoVang_Click(object sender, EventArgs e) {
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.KhaiBaoVang) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}

			frm_KBVang frm1 = new frm_KBVang();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = MdiChildren[indexForm] as frm_KBVang;
				if (frm1 != null) frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Location = new Point((int)((Size.Width - frm1.Size.Width) / 2f),
										  (int)((Size.Height - frm1.Size.Height) / 2f));
				frm1.Show();
			}
		}
		private void SubMenu_ChamcongTayQL_Click(object sender, EventArgs e) {
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.ChamCongQL) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}

			frm_KBChamCongQL frm1 = new frm_KBChamCongQL();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = MdiChildren[indexForm] as frm_KBChamCongQL;
				if (frm1 != null) frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}

		}
		private void SubMenu_KetLuongThang_Click(object sender, EventArgs e) {
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.KetLuong) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}

			frm_QLKetCongBoPhan frm1 = new frm_QLKetCongBoPhan();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = MdiChildren[indexForm] as frm_QLKetCongBoPhan;
				if (frm1 != null) frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}

		}


		private void SubMenu_SuaGioHangLoat_Click(object sender, EventArgs e) {
			frm_SuaGioHangLoat frm1 = new frm_SuaGioHangLoat();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_SuaGioHangLoat;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Maximized;
				frm1.Show();
			}
		}


		private void SubMenu_xemHistory_Click(object sender, EventArgs e) {
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.XemNhatKyThaoTac) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}

			frm_32_XemLichSu frm1 = new frm_32_XemLichSu();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = this.MdiChildren[indexForm] as frm_32_XemLichSu;
				frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Maximized;
				frm1.Show();
			}
		}


		private void SubMenu_DoiMK_Click(object sender, EventArgs e) {
			frm_DoiMatKhau frm1 = new frm_DoiMatKhau {
				StartPosition = FormStartPosition.CenterParent
			};
			frm1.ShowDialog();
		}

		private void SubMenu_Setting_Click(object sender, EventArgs e) {
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.CaiDatThongSo) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}

			frm_Setting frm1 = new frm_Setting();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = MdiChildren[indexForm] as frm_Setting;
				if (frm1 != null) frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}
		}

		private void SubMenu_PhanQuyen_Click(object sender, EventArgs e) {
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.PhanQuyen) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}

			frm_PhanQuyen frm1 = new frm_PhanQuyen();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = MdiChildren[indexForm] as frm_PhanQuyen;
				if (frm1 != null) frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}
		}
		private void SubMenu_TaoTK_Click(object sender, EventArgs e) {
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.TaoTKDangNhap) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}

			frm_TaoTaiKhoan frm1 = new frm_TaoTaiKhoan();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = MdiChildren[indexForm] as frm_TaoTaiKhoan;
				if (frm1 != null) frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}
		}
		private void MenuThoat_Click(object sender, EventArgs e) {
			if (ActiveMdiChild == null) {
				Close();
			}
			while (ActiveMdiChild != null) {
				ActiveMdiChild.Close();
			}
		}

		private void MenuQLNV_Click(object sender, EventArgs e) {
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.QuanLyNV) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}

			frmQLNV frm1 = new frmQLNV();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = MdiChildren[indexForm] as frmQLNV;
				if (frm1 != null) frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}
		}


		private void SubMenu_HuyKetluong_Click(object sender, EventArgs e) {
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.KetLuong) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}

			frmHuyKetLuongThang frm1 = new frmHuyKetLuongThang();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = MdiChildren[indexForm] as frmHuyKetLuongThang;
				if (frm1 != null) frm1.BringToFront();
			}
			else {
				frm1.WindowState = FormWindowState.Normal;
				frm1.MdiParent = this;
				frm1.Show();
			}


		}

		private void SubMenu_QLNhiemVu_Click(object sender, EventArgs e) {
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.QuanLyNhiemVuNhanVien) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}

			fmDSNVNhanNhiemVu frm1 = new fmDSNVNhanNhiemVu();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = MdiChildren[indexForm] as fmDSNVNhanNhiemVu;
				if (frm1 != null) frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}

		}

		private void SubMenu_XemTKeCongVaPCTheoNVu_Click(object sender, EventArgs e) {
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.XemTKeCongVaPCTheoNhiemVu) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}

			fmTKeCongTheoNVu frm1 = new fmTKeCongTheoNVu();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = MdiChildren[indexForm] as fmTKeCongTheoNVu;
				if (frm1 != null) frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}


		}

		private void SubMenu_XemDSNhiemVu_Click(object sender, EventArgs e) {
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.XemDSNhiemVu) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}

			fmQLyNhiemVu frm1 = new fmQLyNhiemVu();
			int indexForm = LayVitriForm(this, frm1.GetType());
			if (indexForm != -1) {
				frm1 = MdiChildren[indexForm] as fmQLyNhiemVu;
				if (frm1 != null) frm1.BringToFront();
			}
			else {
				frm1.MdiParent = this;
				frm1.WindowState = FormWindowState.Normal;
				frm1.Show();
			}

		}

	}
}
