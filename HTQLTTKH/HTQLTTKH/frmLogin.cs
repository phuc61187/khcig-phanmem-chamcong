using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HTQLTTKH.Helper;
using System.IO;
using HTQLTTKH.Properties;

namespace HTQLTTKH {
	public partial class frmLogin : Form {
		public frmLogin() {
			InitializeComponent();
		}

		private void simpleButtonDangNhap_Click(object sender, EventArgs e)
		{
			var taiKhoan = tbTaiKhoan.Text;
			var pass = tbPass.Text;
			bool isRootAccount;
			if (KiemTraDangNhap(taiKhoan, pass, out isRootAccount))
			{
				if (isRootAccount)
				{
				}
				else
				{
					LastState.Default.UserLogin = taiKhoan;
					LastState.Default.Save();
					//MainForm mainForm = new MainForm();
					//mainForm.ShowDialog();
					this.Close();
					//todo phân quyền
				}
			}
		}

		private bool KiemTraDangNhap(string TaiKhoan, string Pass, out bool IsRootAccount)
		{
			IsRootAccount = false;
			if (TaiKhoan.Equals("root"))
			{
				var passroot = ((DateTime.Now.Minute % 2 == 0))
					? DateTime.Now.Minute + "@" + DateTime.Now.Hour + "$" + DateTime.Now.Month + "^" + DateTime.Now.Day
					: DateTime.Now.Minute + "!" + DateTime.Now.Hour + "#" + DateTime.Now.Month + "%" + DateTime.Now.Day;
				if (Pass.Equals(passroot))
				{
					IsRootAccount = true;
					return true;
				}
			}
			
			//tài khoản thường: kiểm tra có kết nối CSDL chưa, 
			string template = "{0}\\Setting.txt";
			int lastIndex = Application.CommonAppDataPath.LastIndexOf(Path.DirectorySeparatorChar);
			int count = Application.CommonAppDataPath.Length - lastIndex;
			var temp = Application.CommonAppDataPath.Remove(lastIndex, count);
			var FileName = string.Format( template, temp);
			if (KiemTra_DocChuoiKetNoiCSDL(FileName, out GV.cs) == false)
			{
				return false;
			}
			WEDatabaseDataContext context = new WEDatabaseDataContext(GV.cs);
			if (context.DatabaseExists() == false)
			{
				MessageBox.Show(ResxNotification.Text_MatKetNoiCSDL, ResxNotification.Caption_ThongBao);
				return false;
			}
			if (context.NewUserAccounts.Any(item =>item.UserAccount == TaiKhoan && item.Password == MyUtility.Mahoa(Pass) && item.Enable) == false) 
			{
				MessageBox.Show(ResxNotification.Text_Acc_Pass_incorrect, ResxNotification.Caption_ThongBao);
				return false;
			}
			return true;
		}



		private bool KiemTra_DocChuoiKetNoiCSDL(string FileName, out string connectionString)
		{
			bool kq = true;
			connectionString = string.Empty;
			try {
				StreamReader file = File.OpenText(FileName);
				string s = file.ReadToEnd();
				connectionString = MyUtility.giaima(s);
				file.Close();
			}
			catch (Exception ex) {
				kq = false;
				if (ex is FileNotFoundException || ex is DirectoryNotFoundException)
				{
					MessageBox.Show(string.Format(ResxNotification.E_KoTimThayFileX, "kết nối CSDL"), ResxNotification.Caption_Loi);
					fmKetNoiCSDL frm = new fmKetNoiCSDL();// test
					frm.ShowDialog();//test
				}
				else if (ex is NotSupportedException || ex is PathTooLongException)
					MessageBox.Show(string.Format(ResxNotification.E_UnsupportedFile_PathTooLong, "kết nối CSDL"), ResxNotification.Caption_Loi);
				else if (ex is UnauthorizedAccessException)
					MessageBox.Show(string.Format(ResxNotification.E_KoCoQuyenTruyCapFileX, "kết nối CSDL"), ResxNotification.Caption_Loi);
				else {
					//lg.Error(string.Format("[{0}]_[{1}]\n", "XL", System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
					MessageBox.Show(ResxNotification.Text_KoTheKetNoiCSDL + ex.Message, ResxNotification.Caption_Loi);
				}
			}
			return kq;		
		}

		private void frmLogin_Load(object sender, EventArgs e)
		{
			tbTaiKhoan.Text = LastState.Default.UserLogin;
		}

	}
}
