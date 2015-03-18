using System;
using System.Data;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;

namespace ChamCong_v03 {
	public partial class frm_32_XemLichSu : Form {

		public DataTable tableLSu;

		public frm_32_XemLichSu() {
			InitializeComponent();
			dataGridView1.AutoGenerateColumns = false;
		}


		private void btnXem_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			MyUtility.UpdateControl(dtpStartGioTruocSua, dtpStartGioSauuuSua, dtpStartThoiDiemSua,
				dtpEndddGioTruocSua, dtpEndddGioSauuuSua, dtpEndddThoiDiemSua);

			DateTime StartGioTruocSua = DateTime.Today;
			DateTime EndddGioTruocSua = DateTime.Today;
			DateTime StartGioSauuuSua = DateTime.Today;
			DateTime EndddGioSauuuSua = DateTime.Today;
			DateTime StartThoiDiemSua = DateTime.Today;
			DateTime EndddThoiDiemSua = DateTime.Today;
			int UserID = 0;
			var query = @"	select	  UserInfo.UserEnrollNumber,UserInfo.UserFullName, UserInfo.UserFullCode,									
									  case when CommandType = 1 then null else TimeStrOld end as TimeStrOld,
									  case when CommandType = 1 then null
										   else 
												case when MachineNoOld%2 = 1 then N'Vào' else N'Ra' end
									  end as OriginTypeOld,
									  case when CommandType = 1 then null else SourceOld end as SourceOld,
									  case when CommandType = 1 then null else MachineNoOld end as MachineNoOld,									  
									  
									  case when CommandType = -1 then null else TimeStrNew end as TimeStrNew,									  
									  case when CommandType = -1 then null
										   else 
												case when MachineNoNew%2 = 1 then N'Vào' else N'Ra' end
									  end as OriginTypeNew,
									  case when CommandType = -1 then null else SourceNew end as SourceNew,									  
									  case when CommandType = -1 then null else MachineNoNew end as MachineNoNew,
									  
									  LichSuSuaGioVaoRa.UserID, NewUserAccount.UserAccount ,
									  Explain,Note,
									  ExecuteTime,
									  case when CommandType = 0 then N'Sửa'
									  when CommandType = -1 then N'Xóa'
									  when CommandType = 1 then N'Thêm' 
									  end as CommandType
										
							from	LichSuSuaGioVaoRa,  NewUserAccount, UserInfo
							where	LichSuSuaGioVaoRa.UserID = NewUserAccount.UserID
							and UserInfo.UserEnrollNumber = LichSuSuaGioVaoRa.UserEnrollNumber ";
			if (checkGioTruocSua.Checked) {
				StartGioTruocSua = dtpStartGioTruocSua.Value;
				EndddGioTruocSua = dtpEndddGioTruocSua.Value;
				var temp = " and  (TimeStrOld between @StartGioTruocSua and @EndddGioTruocSua) ";
				query += temp;
			}
			if (checkGioSauuuSua.Checked) {
				StartGioSauuuSua = dtpStartGioSauuuSua.Value;
				EndddGioSauuuSua = dtpEndddGioSauuuSua.Value;
				var temp = " and   (TimeStrNew between @StartGioSauuuSua and @EndddGioSauuuSua) ";
				query += temp;
			}
			if (checkThoiDiemSua.Checked) {
				StartThoiDiemSua = dtpStartThoiDiemSua.Value;
				EndddThoiDiemSua = dtpEndddThoiDiemSua.Value;
				var temp = " and   (ExecuteTime between @StartThoiDiemSua and @EndddThoiDiemSua) ";
				query += temp;
			}
			if (checkThucHienBoi.Checked && cbTaiKhoanThucHien.SelectedIndex != 0) {
				UserID = (int)cbTaiKhoanThucHien.SelectedValue;
				var temp = " and  LichSuSuaGioVaoRa.UserID = @UserID ";
				query += temp;
			}

			var table = SqlDataAccessHelper.ExecuteQueryString(query,
				new string[] { "@StartGioTruocSua", "@EndddGioTruocSua", "@StartGioSauuuSua", "@EndddGioSauuuSua", "@StartThoiDiemSua", "@EndddThoiDiemSua", "@UserID" },
				new object[] { StartGioTruocSua, EndddGioTruocSua, StartGioSauuuSua, EndddGioSauuuSua, StartThoiDiemSua, EndddThoiDiemSua, UserID });
			dataGridView1.DataSource = table;
		}

		private void frm_32_XemLichSu_Load(object sender, EventArgs e)
		{
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 4000);
				this.Close();
				return;
			}

			var table = DAL.SelAllDSTaikhoan();
			cbTaiKhoanThucHien.ValueMember = "UserID";
			cbTaiKhoanThucHien.DisplayMember = "UserAccount";
			cbTaiKhoanThucHien.DataSource = table;
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}



	}
}
