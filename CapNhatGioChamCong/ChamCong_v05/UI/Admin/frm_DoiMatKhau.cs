using System;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.Helper;
using ChamCong_v05.Properties;

namespace ChamCong_v05.UI.Admin {
	public partial class frm_DoiMatKhau : Form {

		public frm_DoiMatKhau() {
			InitializeComponent();
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			Close();
		}

		private void btnDongY_Click(object sender, EventArgs e) {
			if (tbPass1.Text == string.Empty || tbPass2.Text == string.Empty) {
				MessageBox.Show("Mật khẩu không được trống.", Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}
			if (tbPass1.Text != tbPass2.Text) {
				MessageBox.Show("Mật khẩu không khớp.", Resources.Caption_ThongBao, MessageBoxButtons.OK);
				tbPass1.Text = tbPass2.Text = string.Empty;
				return;
			}
			if (MessageBox.Show("Bấm Yes để đổi mật khẩu. Bấm No để xem lại.",Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No)
			{
				return;			    
			}
			string tempTKDangnhap = lbAccount.Tag.ToString();
			string tempPass = tbPass1.Text;
			string tempPassEncrypt = MyUtility.Mahoa(tempPass);

			int kq = SqlDataAccessHelper.ExecNoneQueryString(" update NewUserAccount set Password = @Password  where UserAccount = @UserAccount"
															, new string[] { "@Password", "@UserAccount" }
															, new object[] { tempPassEncrypt, tempTKDangnhap }, CanLog: false); // info ko cho phép log acc, pass
			if (kq == 0)
				MessageBox.Show("Không thể thay đổi password. Vui lòng thử lại", Resources.Caption_Loi, MessageBoxButtons.OK);
			else {
				MessageBox.Show("Thay đổi mật khẩu thành công.", Resources.Caption_ThongBao, MessageBoxButtons.OK);
				tbPass1.Text = tbPass2.Text = string.Empty;
			}
		}

		private void frmDoiMatKhau_Load(object sender, EventArgs e)
		{
			lbAccount.Tag = XL2.currUserAccount;
		}

		private void tbPass1_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter)
				btnThucHien.PerformClick();

		}
	}
}
