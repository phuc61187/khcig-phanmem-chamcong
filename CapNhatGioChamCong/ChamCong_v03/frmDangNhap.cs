using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ChamCong_v03.DAO;
using ChamCong_v03.BUS;
using ChamCong_v03.Properties;
using log4net;

//using DevComponents;

namespace ChamCong_v03 {
	public partial class frmDangNhap : Form {
		private readonly ILog lg = LogManager.GetLogger("frmDangNhap");

		public int LoggedInResult { get; set; }
		public frmDangNhap() {
			InitializeComponent();
			log4net.Config.XmlConfigurator.Configure();
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			LoggedInResult = 0;
			this.Close();
		}

		private void btnDangnhap_Click(object sender, EventArgs e) {
			#region lay du lieu tu form
			string tempUsername = tbTaikhoan.Text, tempPassword = tb_Password.Text;

			string passroot = string.Empty;
			passroot = ((DateTime.Now.Minute % 2 == 0))
				? DateTime.Now.Minute + "@" + DateTime.Now.Hour + "$" + DateTime.Now.Month + "^" + DateTime.Now.Day
				: DateTime.Now.Minute + "!" + DateTime.Now.Hour + "#" + DateTime.Now.Month + "%" + DateTime.Now.Day;
			#endregion

			string tmpConnStr = string.Empty;
			if (tempUsername == "root" && tempPassword == passroot) { //log in bằng tài khoản root
				tmpConnStr = KiemtraDocFileKetnoiDL(Properties.Settings.Default.ConnectionStringPath);

				if (string.IsNullOrWhiteSpace(tmpConnStr) == false) { // kết nối được csdl, ghi thông tin id và currAcc, báo kq sau đó tắt form này đi
					if (SqlDataAccessHelper.TestConnection(tmpConnStr) == false)
					{
						AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
						return;
					}
					SqlDataAccessHelper.ConnectionString = tmpConnStr;
					XL2.currUserID = int.MaxValue;
					XL2.currUserAccount = tempUsername;
					LoggedInResult = int.MaxValue;
					this.Close();
				}
				else { // không kết nối được csdl thì báo yêu cầu kết nối csdl trước, vẫn để nguyên form vì chưa login thành công
					MessageBox.Show("Chưa có kết nối đến CSDL. Vui lòng kết nối đến CSDL trước.", "Thông báo");
				}
			}
			else {// kiểm tra login bằng tài khoản thường

				tmpConnStr = KiemtraDocFileKetnoiDL(Properties.Settings.Default.ConnectionStringPath);

				if (string.IsNullOrWhiteSpace(tmpConnStr) == false) { // kết nối csdl thành công  thì xác thực tài khoản, nếu ok thì đóng form , ghi lại trạng thái login
					SqlDataAccessHelper.ConnectionString = tmpConnStr;

					if (SqlDataAccessHelper.TestConnection(tmpConnStr) == false) {
						AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
						return;
					}

					try {
						string passEncrypt = MyUtility.Mahoa(tempPassword);
						DataTable dt = DAL.LogIn(tempUsername, passEncrypt);

						if (dt.Rows.Count != 0) { // tài khoản thường -> 
							XL2.currUserID = (int)dt.Rows[0]["UserID"];
							XL2.currUserAccount = dt.Rows[0]["UserAccount"].ToString();
							XL.DocSetting();
							XL2.ThangKetCong = XL.DocThangKetCong();

							LoggedInResult = 1;
							this.Close();
						}
						else if (MessageBox.Show("Tài khoản hoặc mật khẩu chưa đúng. Vui lòng điền lại.", "Thông báo", MessageBoxButtons.OK) == DialogResult.OK) {
							tb_Password.Text = string.Empty;
						}
					} catch (Exception exception) {
						string temp = "btnDangnhap_Click param:";
						temp += "username=" + tempUsername;
						lg.Error(temp, exception);
						AutoClosingMessageBox.Show("Mất kết nối đến Máy chủ hoặc CSDL không đúng. Vui lòng thử lại.", "Lỗi", 2000);
					}
				}
				else {// kết nối csdl khong thành công. thông báo để chọn kết nối csdl, không đóng form để tiếp tục nhập
					MessageBox.Show("Chưa có kết nối đến CSDL. Vui lòng kết nối đến CSDL trước.", "Thông báo");
				}
			}
		}

		private string KiemtraDocFileKetnoiDL(string FileName) {
			string kq = string.Empty;
			// Open the file into a StreamReader
			try {
				//FileStream file = File.Open(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
				//StreamReader reader = new StreamReader(file);
				//string s = reader.ReadToEnd();
				StreamReader file = File.OpenText(FileName);
				string s = file.ReadToEnd();
				kq = MyUtility.giaima(s);

				file.Close();
			} catch (Exception ex) {
				if (ex is FileNotFoundException || ex is DirectoryNotFoundException)
                    MessageBox.Show("Không tìm thấy file kết nối CSDL.", "Thông báo", MessageBoxButtons.OK);
				else if (ex is NotSupportedException || ex is PathTooLongException)
                    MessageBox.Show("File kết nối CSDL bị lỗi.", "Thông báo", MessageBoxButtons.OK);
				else if (ex is UnauthorizedAccessException)
					MessageBox.Show("Không có quyền truy cập file kết nối CSDL.", "Thông báo", MessageBoxButtons.OK);
				else {
					lg.Error("kiemtraketnoicsdl",ex);
					MessageBox.Show("Không thể kết nối CSDL. \nLỗi:\n" + ex.Message, "Thông báo", MessageBoxButtons.OK);
				}
				return string.Empty;
			}
			return kq;
		}
		private void btnKetnoiCSDL_Click(object sender, EventArgs e) {

			frmKetNoiCSDL frm = new frmKetNoiCSDL();
			frm.ShowDialog();

		}

		private void frmDangNhap_Load(object sender, EventArgs e) {
			LoggedInResult = 0;
		}

		private void tbTaikhoan_Or_tbPassword_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter)
				btnDangnhap.PerformClick();
		}
	}
}
