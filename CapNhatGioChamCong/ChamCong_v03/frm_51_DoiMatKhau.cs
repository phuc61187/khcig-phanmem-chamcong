using System;
using System.Windows.Forms;
using ChamCong_v03.DAO;
using ChamCong_v03.BUS;


namespace ChamCong_v03 {
	public partial class frm_51_DoiMatKhau : Form {

		public frm_51_DoiMatKhau() {
			InitializeComponent();
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void btnDongY_Click(object sender, EventArgs e) {
			if (tbPass1.Text == string.Empty || tbPass2.Text == string.Empty) {
				MessageBox.Show("Mật khẩu không được trống.", "Thông báo", MessageBoxButtons.OK);
				return;
			}
			if (tbPass1.Text != tbPass2.Text) {
				MessageBox.Show("Mật khẩu không khớp.", "Thông báo", MessageBoxButtons.OK);
				tbPass1.Text = tbPass2.Text = string.Empty;
				return;
			}
			if (MessageBox.Show("Bấm Yes để đổi mật khẩu. Bấm No để xem lại.","Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
			{
                return;			    
			}
			string tempTKDangnhap = lbAccount.Tag.ToString();
			string tempPass = tbPass1.Text;
			string tempPassEncrypt = MyUtility.Mahoa(tempPass);

			int kq = SqlDataAccessHelper.ExecNoneQueryString(" update NewUserAccount set Password = @Password  where UserAccount = @UserAccount"
															, new string[] { "@Password", "@UserAccount" }
															, new object[] { tempPassEncrypt, tempTKDangnhap });
			if (kq == 0)
				MessageBox.Show("Không thể thay đổi password. Vui lòng thử lại", "Lỗi", MessageBoxButtons.OK);
			else {
				MessageBox.Show("Thay đổi mật khẩu thành công.", "Thông báo", MessageBoxButtons.OK);
				tbPass1.Text = tbPass2.Text = string.Empty;
			}
		}

		private void frmDoiMatKhau_Load(object sender, EventArgs e)
		{
			lbAccount.Tag = XL2.currUserAccount;
		}
	}
}
