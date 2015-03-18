using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using ChamCong_v02.BUS;
using log4net;

namespace ChamCong_v02 {
    public partial class frmKetNoiCSDL : Form {
        private readonly ILog lg = LogManager.GetLogger("frmKetNoiCSDL");

        public frmKetNoiCSDL() {
            InitializeComponent();
        }

        private void frmKetNoiCSDL_Load(object sender, EventArgs e) {

        }

        private void btnThoat_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnKetnoiCSDL_Click(object sender, EventArgs e) {
            #region tạo chuỗi kết nối
            string server = tbServer.Text;
            string User = tbUser.Text;
            string pass = tbPass.Text;
            string database = tbDatabase.Text;

			string sConnection = @"Data Source=" + server + ";";
			sConnection += @"Initial Catalog=" + database + ";";
            if (User == string.Empty && pass == string.Empty) {
                sConnection += @"Integrated Security=true";
            }
            else {
                sConnection += @"User ID=" + User + ";";
                sConnection += @"Password=" + pass + ";";
            }
            #endregion 
            // test kết nối và ghi file
            SqlConnection cnn = new SqlConnection(sConnection);
            try {
                cnn.Open();
                cnn.Close();
                StreamWriter writer = new StreamWriter(Properties.Settings.Default.ConnectionStringPath);
                string encryptConnectionString = MyUtility.Mahoa(sConnection);
                writer.Write(encryptConnectionString);
                writer.Close();
                AutoClosingMessageBox.Show("Kết nối CSDL thành công.", "Thông báo",2000);
            } catch (Exception ex) {
	            string temp = "btnKetnoiCSDL_Click; param:ConnectionString=" + sConnection;
	            temp += "ConnectionStringPath=" + Properties.Settings.Default.ConnectionStringPath;
	            temp += "UserID " + User;
				lg.Error(temp, ex);
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

        }
    }
}
