using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CapNhatGioChamCong.DAO;

namespace CapNhatGioChamCong {
    public partial class frm_DoiMatKhau : Form {
        public string CurrentAccount { get; set; }

        public frm_DoiMatKhau() {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e) {
            if (tbPass1.Text == string.Empty || tbPass2.Text == string.Empty) {
                MessageBox.Show("Mật khẩu không được trống." , Properties.Resources.MessBoxTitle_ThongBao , MessageBoxButtons.OK);
                return;
            }
            if(tbPass1.Text != tbPass2.Text) {
                MessageBox.Show("Mật khẩu không khớp." , Properties.Resources.MessBoxTitle_ThongBao , MessageBoxButtons.OK);
                tbPass1.Text = tbPass2.Text = string.Empty;
                return;
            }
            string tempTKDangnhap = tbTaiKhoan.Text;
            string tempPass = tbPass1.Text;
            string tempPassEncrypt = MyUtility.Mahoa(tempPass);

            int kq = SqlDataAccessHelper.ExecNoneQueryString(" update NewUserAccount set Password = @Password  where UserAccount = @UserAccount"
                                                            , new string[] {"@Password" , "@UserAccount"}
                                                            , new object[] {tempPassEncrypt , tempTKDangnhap});
            if (kq == 0)
                MessageBox.Show("Không thể thay đổi password. Vui lòng thử lại" , Properties.Resources.MessBoxCap_Error , MessageBoxButtons.OK);
            else {
                MessageBox.Show("Thay đổi mật khẩu thành công." , Properties.Resources.MessBoxTitle_ThongBao , MessageBoxButtons.OK);
                tbPass1.Text = tbPass2.Text = string.Empty;
            }
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e) { tbTaiKhoan.Text = CurrentAccount; }
    }
}
