using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.BUS;
using ChamCong_v06.DTO;
using ChamCong_v06.Helper;

namespace ChamCong_v06.UI {
	public partial class frmXemCong : Form {
		public frmXemCong() {
			InitializeComponent();
		}

		private void frmXemCong_Load(object sender, EventArgs e) {
			#region kiểm tra kết nối csdl , nếu mất kết nối thì đóng
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}
			#endregion

			LoadDSNV();
		}

		private void btnThucHien_CCThang_Click(object sender, EventArgs e) {

		}

		private void btnThucHien_XemCong_Click(object sender, EventArgs e) {

		}

		private void buttonEdit_TimKiemNV_Properties_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {

		}

		private void LoadDSNV() {
			DataTable tableDSPhong = SqlDataAccessHelper.ExecSPQuery(SPName6.DeptPrivilege_DocPhongBanThaoTacV6.ToString(),
	new SqlParameter("@UserID", GlobalVariables.CurrentUserID), new SqlParameter("@ChoPhepThaoTac", true),
	new SqlParameter("@RelationDeptEnable", true));
			//LayTableNhanVien(out tableNhanVien, this.m_SelectedPhong);
			//m_NhanVienDR = (from DataRow dataRow in tableNhanVien.Rows select dataRow).ToList();
			List<int> listIDPhong = (from DataRow dataRow in tableDSPhong.Rows select (int)dataRow["IDDepartment"]).ToList();
			DataTable tableArrayIDPhong = MyUtility.Array_To_DataTable("tableArrayIDD", listIDPhong);
			DataTable tableNhanVien = SqlDataAccessHelper.ExecSPQuery(SPName6.UserInfo_DocNhanVienChamCongV6.ToString(),
															new SqlParameter("@ArrayIDDepartment", SqlDbType.Structured) { Value = tableArrayIDPhong },
															new SqlParameter("@DepartmentEnable", true),
															new SqlParameter("@UserEnabled", true));

			gridControl_DSNV.DataSource = tableNhanVien;
		}

		private void gridControl_DSNV_Click(object sender, EventArgs e) {

		}
	}
}
