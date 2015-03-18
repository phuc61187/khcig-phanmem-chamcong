using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CapNhatGioChamCong;
using CapNhatGioChamCong.DAO;
using CapNhatGioChamCong.Properties;

namespace CapNhatGioChamCong {
    public partial class frm_KetNoiCSDL : Form {
        private string _fConnectionString = string.Empty;
        public string fConnectionString { get { return _fConnectionString; }  }

        public frm_KetNoiCSDL() {
            InitializeComponent();
            this.DialogResult = DialogResult.No;// mặc định là ko kết nối được CSDL, nếu kết nối OK thì this.DialogResult = DialogResult.Yes
            this._fConnectionString = string.Empty;
        }


        private void btnKetNoiCSDL_Click(object sender, EventArgs e) {
            string tempConnectionString = string.Empty;

            #region lấy dữ liệu từ form
            tempConnectionString += @"Data Source=" + tbServer.Text + ";";
                tempConnectionString += "Initial Catalog=" + tbDatabase.Text + ";";
            if (tbUser.Text == string.Empty && tbPassword.Text == string.Empty) {
                tempConnectionString += "Integrated Security=true";
            }
            else {
                tempConnectionString += "Initial Catalog=" + tbDatabase.Text + ";";
                tempConnectionString += "User ID=" + tbUser.Text + ";";
                tempConnectionString += "Password=" + tbPassword.Text + ";";
            }
            #endregion

            #region mở kết nối và ghi xuống file
            SqlConnection cnn = new SqlConnection(tempConnectionString);
            try {
                cnn.Open();
                cnn.Close();
                StreamWriter writer = new StreamWriter(Resources.ConnectionStringPath);
                string encryptConnectionString = MyUtility.Mahoa(tempConnectionString);
                writer.Write(encryptConnectionString);
                writer.Close();
            } catch (Exception ex) {
                if (ex is InvalidOperationException || ex is SqlException || ex is ConfigurationException) {
                    if (MessageBox.Show("Kết nối CSDL không thành công. Bấm Retry để thử lại. Bấm Cancel để thoát ứng dụng.", "Lỗi", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                        return;
                    else {
                        Application.Exit();
                        return;
                    }
                }
                else if (ex is UnauthorizedAccessException) {
                    MessageBox.Show("Không có quyền ghi chuỗi kết nối vào Folder. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK);
                    return;
                }
                else {
                    MessageBox.Show("Không thể thực hiện ghi chuỗi kết nối vào Folder lưu trữ. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK);
                    return;
                }
                
            }
            
            AutoClosingMessageBox.Show("Kết nối CSDL thành công !", "Thông báo", 1000);
            this._fConnectionString = tempConnectionString;
            this.DialogResult = DialogResult.Yes;
            this.Close();
            #endregion

        }


        private void btnThoat_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.No;

            this.Close();
        }

        private void frm_KetNoiCSDL_Load(object sender, EventArgs e) {
            //[TBD] hiển thị mặc định tên csdl trong file, chức năng này làm sau [TBD]
            tbDatabase.Text = tbPassword.Text = tbServer.Text = tbUser.Text = string.Empty;
        }








    }
}
