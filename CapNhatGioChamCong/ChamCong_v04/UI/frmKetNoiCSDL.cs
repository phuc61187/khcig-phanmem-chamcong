using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;
using log4net;

namespace ChamCong_v04.UI {
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
				var temp = Application.CommonAppDataPath + "\\Setting.txt";
				StreamWriter writer = new StreamWriter( temp);
				string encryptConnectionString = MyUtility.Mahoa(sConnection);
				writer.Write(encryptConnectionString);
				writer.Close();
				XL.SaveSetting(connectionstring:temp, lastUserName:User, lastServerName:server, lastDatabase:database);
				ACMessageBox.Show("Kết nối CSDL thành công.", "Thông báo",2000);
			} catch (Exception ex) {
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				
				if (ex is InvalidOperationException || ex is SqlException || ex is ConfigurationException) {
					if (MessageBox.Show(Resources.Text_KetNoiCSDLKoThanhCong, Resources.Caption_Loi, MessageBoxButtons.RetryCancel) == DialogResult.Retry)
						return;
					else {
						Application.Exit();
						return;
					}
				}
				else if (ex is UnauthorizedAccessException) {
					MessageBox.Show(Resources.Text_KoCoQuyenGhiChuoiKetNoiCSDL, Resources.Caption_Loi, MessageBoxButtons.OK);
					return;
				}
				else {
					MessageBox.Show(Resources.Text_KoGhiDuocChuoiKetNoiCSDL, Resources.Caption_Loi, MessageBoxButtons.OK);
					return;
				}
			}

		}
	}
}
