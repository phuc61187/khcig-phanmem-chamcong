using HTQLTTKH.Helper;
using HTQLTTKH.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using userSetting=HTQLTTKH.Properties.Settings;

namespace HTQLTTKH {
	public partial class fmKetNoiCSDL : Form {
		public fmKetNoiCSDL() {
			InitializeComponent();
		}

		private void simpleButtonThoat_Click(object sender, EventArgs e) {
			Close();
		}

		private void simpleButtonKetNoi_Click(object sender, EventArgs e)
		{
			string server = tbServer.Text, pass = tbPass.Text, user = tbUser.Text, database = tbDatabase.Text;
			string sConnection = string.Empty;
			if (checkBox1.Checked)
			{
				var template = @"Data Source={0};Initial Catalog={1};Integrated Security=true;";
				sConnection = string.Format(template, server, database);
			}
			else
			{
				var template = @"Data Source={0};Initial Catalog={1};User ID={2};Password={3};";
				sConnection = string.Format(template, server, database, user, pass);
			}
			System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(sConnection);
			try {
				cnn.Open();
				cnn.Close();
				string template = "{0}\\Setting.txt";
				int lastIndex = Application.CommonAppDataPath.LastIndexOf(Path.DirectorySeparatorChar);
				int count = Application.CommonAppDataPath.Length - lastIndex;
				var temp = Application.CommonAppDataPath.Remove(lastIndex, count);
				var FileName = string.Format(template, temp);
				System.IO.StreamWriter writer = new System.IO.StreamWriter( FileName);
				string encryptConnectionString = MyUtility.Mahoa(sConnection);
				writer.Write(encryptConnectionString);
				writer.Close();
				userSetting.Default.LastVar_Server = server;
				userSetting.Default.LastVar_Database = database;
				userSetting.Default.LastVar_UserWE = user;
				ACMessageBox.Show("Kết nối CSDL thành công.", "Thông báo", 2000);
			} catch (Exception ex) {
				//lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				
				if (ex is InvalidOperationException || ex is System.Data.SqlClient.SqlException || ex is System.Configuration.ConfigurationException) {
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
				else if (ex is FileNotFoundException)
				{
					MessageBox.Show("Không tìm thấy file Setting.txt kết nối CSDL", Resources.Caption_Loi, MessageBoxButtons.OK);
					return;
				}
				else {
					MessageBox.Show(Resources.Text_KoGhiDuocChuoiKetNoiCSDL, Resources.Caption_Loi, MessageBoxButtons.OK);
					return;
				}
			//// test kết nối và ghi file
			//WE_LinqToSQLDataContext context = new WE_LinqToSQLDataContext();
			//context.Connection
			//SqlConnection cnn = new SqlConnection(sConnection);
			//try {
			//	cnn.Open();
			//	cnn.Close();
			//	var temp = Application.UserAppDataPath + "\\Setting.txt";
			//	StreamWriter writer = new StreamWriter( temp);
			//	string encryptConnectionString = MyUtility.Mahoa(sConnection);
			//	writer.Write(encryptConnectionString);
			//	writer.Close();
			//	XL.SaveSetting(connectionstring:temp, lastUserName:user, lastServerName:server, lastDatabase:database);
			//	ACMessageBox.Show("Kết nối CSDL thành công.", "Thông báo",2000);
			//} catch (Exception ex) {
			//	lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				
			//	if (ex is InvalidOperationException || ex is SqlException || ex is ConfigurationException) {
			//		if (MessageBox.Show(Resources.Text_KetNoiCSDLKoThanhCong, Resources.Caption_Loi, MessageBoxButtons.RetryCancel) == DialogResult.Retry)
			//			return;
			//		else {
			//			Application.Exit();
			//			return;
			//		}
			//	}
			//	else if (ex is UnauthorizedAccessException) {
			//		MessageBox.Show(Resources.Text_KoCoQuyenGhiChuoiKetNoiCSDL, Resources.Caption_Loi, MessageBoxButtons.OK);
			//		return;
			//	}
			//	else {
			//		MessageBox.Show(Resources.Text_KoGhiDuocChuoiKetNoiCSDL, Resources.Caption_Loi, MessageBoxButtons.OK);
			//		return;
			//	}
			}
		}
	}
}
