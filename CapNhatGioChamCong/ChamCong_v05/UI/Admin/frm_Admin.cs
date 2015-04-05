using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.Helper;
using log4net;

namespace ChamCong_v05.UI.Admin {
	public partial class frm_Admin : Form {
		//1. mỗi lần thêm 1 chức năng gì phải update lại bên menuid mặc định của tài khoản

		private readonly ILog lg = LogManager.GetLogger("frm01admin");

		public frm_Admin() {
			log4net.Config.XmlConfigurator.Configure();

			InitializeComponent();
		}

		private void btnChonCSDL_Click(object sender, EventArgs e) {
			frmKetNoiCSDL frm = new frmKetNoiCSDL();
			frm.ShowDialog();
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			Application.Exit();
		}


		private void frmTaoTaiKhoan_Load(object sender, EventArgs e) {
			try {
				Load_dataGridNewUserAccount();
				Load_cbTaikhoanWE();

			} catch (Exception) {
				MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK);
			}
		}


		private void Load_cbTaikhoanWE() {
			try {
				DataTable dataTable = SqlDataAccessHelper.ExecuteQueryString(
						@"  select UserID, UserAccount from UserAccount 
						where UserAccount not in (select NewUserAccount.UserAccount from NewUserAccount)", null, null);
				cbTaikhoanWE.DisplayMember = "UserAccount";
				cbTaikhoanWE.ValueMember = "UserID";
				cbTaikhoanWE.DataSource = dataTable;
				tbPass1.Text = tbPass2.Text = string.Empty;
				if (dataTable == null || dataTable.Rows.Count == 0) btnTaoTK.Enabled = false;
			} catch (Exception exception) {
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), exception);
				throw exception;
			}
		}

		private void Load_dataGridNewUserAccount() {
			try {
				DataTable dataTable = SqlDataAccessHelper.ExecuteQueryString("Select UserID, UserAccount from NewUserAccount", null, null);
				dataGridTK.DataSource = dataTable;
			} catch (Exception exception) {
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), exception);
				throw exception;
			}

		}

		private void btnTaoTK_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			if (tbPass1.Text != tbPass2.Text) {
				MessageBox.Show("Mật khẩu không khớp.");
				return;
			}
			if (tbPass1.Text == string.Empty || tbPass2.Text == string.Empty) {
				MessageBox.Show("Mật khẩu không được để trống.");
				return;
			}

			string insertString = @" insert into NewUserAccount(UserID, UserAccount, Password) values (@UserID, @UserAccount, @password) ";

			// xem các chức năng để tạo mặc định 
			List<XL2.cChucNang> listChucNang = XL2.TaoChucNang();
			string templateString = " insert into MenuPrivilege (UserID, MenuID, IsYes) values (@UserID, {0}, 0) ";
			string chuoi = string.Empty;
			for (int i = 0; i < listChucNang.Count; i++)
			{
				chuoi = string.Format(templateString, listChucNang[i].ID);
				insertString += chuoi;
			}
			int tempUserID = (int)cbTaikhoanWE.SelectedValue;

			string tempUserAccount = tbTenTaiKhoan.Text;
			string tempPass = tbPass1.Text;
			string tempPassEncrypt = MyUtility.Mahoa(tempPass);
			try {
				#region 

				/* insertString = @" insert into NewUserAccount(UserID, UserAccount, Password) values (@UserID, @UserAccount, @password) 
									insert into MenuPrivilege (UserID, MenuID, IsYes) values (@UserID, 10001, 0) 
									insert into MenuPrivilege (UserID, MenuID, IsYes) values (@UserID, 10002, 0) 
									insert into MenuPrivilege (UserID, MenuID, IsYes) values (@UserID, 20001, 0) 
									insert into MenuPrivilege (UserID, MenuID, IsYes) values (@UserID, 20002, 0) 
									insert into MenuPrivilege (UserID, MenuID, IsYes) values (@UserID, 20003, 0) 
									insert into MenuPrivilege (UserID, MenuID, IsYes) values (@UserID, 30001, 0) 
									insert into MenuPrivilege (UserID, MenuID, IsYes) values (@UserID, 30002, 0) 
									insert into MenuPrivilege (UserID, MenuID, IsYes) values (@UserID, 40001, 0) 
									insert into MenuPrivilege (UserID, MenuID, IsYes) values (@UserID, 40002, 0) 
									insert into MenuPrivilege (UserID, MenuID, IsYes) values (@UserID, 60001, 0) "
				*/

				#endregion

				int kq = SqlDataAccessHelper.ExecNoneQueryString(insertString
																, new[] { "@UserID", "@UserAccount", "@password" }
																, new object[] { tempUserID, tempUserAccount, tempPassEncrypt }, CanLog:false);
				if (kq != 0) ACMessageBox.Show("Thêm tài khoản thành công.", "Thông báo", 2000);
				else MessageBox.Show("Không thêm được tài khoản.", "Lỗi", MessageBoxButtons.OK);
				Load_dataGridNewUserAccount();
				Load_cbTaikhoanWE();

			} catch (Exception exception) {
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), exception);
				MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK);
			}
		}



		private void btnReset_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			if (tbPass1.Text != tbPass2.Text) {
				MessageBox.Show("Mật khẩu không khớp.");
				return;
			}
			if (tbPass1.Text == string.Empty || tbPass2.Text == string.Empty) {
				MessageBox.Show("Mật khẩu không được để trống.");
				return;
			}

			string tempTenTK = tbTenTaiKhoan.Text;
			string tempPass = tbPass1.Text;
			string tempPassEncrypt = MyUtility.Mahoa(tempPass);
			string updateString = @" update NewUserAccount set Password=@Password where UserAccount = @UserAccount ";
			int kq = SqlDataAccessHelper.ExecNoneQueryString(updateString, new[] { "@UserAccount", "@Password" }
															, new object[] { tempTenTK, tempPassEncrypt }, CanLog:false);
			if (kq != 0) ACMessageBox.Show("Reset mật khẩu tài khoản thành công.", "Thông báo", 2000);
			else ACMessageBox.Show("Không Reset mật khẩu tài khoản được.", "Lỗi", 2000);
			tbPass1.Text = tbPass2.Text = string.Empty;
		}

		private void cbTaikhoanWE_SelectedIndexChanged(object sender, EventArgs e) {
			string tempTenTK = cbTaikhoanWE.Text;
			tbTenTaiKhoan.Text = tempTenTK;
			tbPass1.Text = tbPass2.Text = string.Empty;
			btnTaoTK.Enabled = true;
			btnXoaTK.Enabled = btnReset.Enabled = false;

		}

		private void btnXoaTK_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			string tempTenTK = tbTenTaiKhoan.Text;
			string deleteString = @" delete NewUserAccount where UserAccount = @UserAccount ";
			try {
				int kq = SqlDataAccessHelper.ExecNoneQueryString(deleteString, new[] { "@UserAccount" }, new object[] { tempTenTK });
				if (kq != 0) MessageBox.Show("Xóa tài khoản thành công.", "Thông báo", MessageBoxButtons.OK);
				Load_cbTaikhoanWE();
				Load_dataGridNewUserAccount();
			} catch (Exception ex) {
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show("Không xóa tài khoản được.", "Lỗi", MessageBoxButtons.OK);
			}
		}


		private void dataGridTK_CellClick(object sender, DataGridViewCellEventArgs e) {
			if (e.RowIndex == -1) return;
			btnTaoTK.Enabled = false;
			btnXoaTK.Enabled = btnReset.Enabled = true;

			string tempTenTK = dataGridTK.Rows[e.RowIndex].Cells[1].Value.ToString();
			tbTenTaiKhoan.Text = tempTenTK;
			int tempUserID = (int)dataGridTK.Rows[e.RowIndex].Cells[0].Value;
			tbPass1.Text = tbPass2.Text = string.Empty;
		}

		private void btnPhanQuyen_Click(object sender, EventArgs e) {
			frm_PhanQuyen frm52 = new frm_PhanQuyen();
			frm52.ShowDialog();
		}

		private void btnCaiDatThongSo_Click(object sender, EventArgs e) {
			frm_Setting frm1 = new frm_Setting();
			frm1.ShowDialog();
		}

		private void btnPhucHoi_HuyXN_GioCC_Click(object sender, EventArgs e) {
			frm_PhucHoiGioChamCong frm = new frm_PhucHoiGioChamCong();
			frm.ShowDialog();
		}

		private void btnTaoCaLV_Click(object sender, EventArgs e) {
			frmSetupShift frm = new frmSetupShift();
			frm.ShowDialog();
		}

	}
}
