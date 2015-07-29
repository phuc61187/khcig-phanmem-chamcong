using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.IO;
using ChamCong_v06.Helper;
using System.Configuration;
using ChamCong_v06.Properties;
using log4net;

namespace ChamCong_v06.UI {
	public partial class frmKetNoiCSDL : Form {

		#region khu vực hàm ko quan trọng
		private readonly ILog lg = LogManager.GetLogger("frmKetNoiCSDL");

		public frmKetNoiCSDL()
		{
			InitializeComponent();
		}

		private void btnEdit_Clear_Click(object sender, EventArgs e)
		{
			ButtonEdit button = (ButtonEdit) sender;
			button.Text = string.Empty;
		}

		private void btnThoat_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		#endregion

		private void btnKetnoiCSDL_Click(object sender, EventArgs e) {
			#region tạo chuỗi kết nối
			string server = btnEditServer.Text;
			string User = btnEditUser.Text;
			string pass = btnEditPassword.Text;
			string database = btnEditDatabase.Text;

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
				string encryptConnectionString = MyUtility.Mahoa(sConnection);
				Settings.Default.EncryptConnectionString = encryptConnectionString;
				Settings.Default.Save();
				ACMessageBox.Show("Kết nối CSDL thành công.", "Thông báo", 2000);
			}
			catch (Exception ex) {
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
