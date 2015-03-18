using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;

namespace ChamCong_v04.UI {
	public partial class frm_32_XemLichSu : Form {

		public DataTable tableLSu;

		public frm_32_XemLichSu() {
			InitializeComponent();
			dgrdLichSu.AutoGenerateColumns = false;
		}


		private void btnXem_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			MyUtility.UpdateControl(dtpStartThoiDiemSua, dtpEndddThoiDiemSua);

			DateTime StartThoiDiemSua = DateTime.Today;
			DateTime EndddThoiDiemSua = DateTime.Today;
			int UserID = 0;
			var query = @"	select	NhatKyThaoTac.*, NewUserAccount.UserAccount, UserInfo.UserFullName	, RelationDept.Description as TenPhong										
							from	NewUserAccount, 
									NhatKyThaoTac	left outer join UserInfo on NhatKyThaoTac.UserEnrollNumber = UserInfo.UserEnrollNumber
													left outer join RelationDept on NhatKyThaoTac.MaPhong = RelationDept.ID
							where	NhatKyThaoTac.UserID = NewUserAccount.UserID ";
			if (checkThoiDiemSua.Checked) {
				StartThoiDiemSua = dtpStartThoiDiemSua.Value;
				EndddThoiDiemSua = dtpEndddThoiDiemSua.Value;
				var temp = " and   (ThoiDiem between @StartThoiDiemSua and @EndddThoiDiemSua) ";
				query += temp;
			}
			if (radThucHienBoi1TaiKhoan.Checked) {
				UserID = (int)cbTaiKhoanThucHien.SelectedValue;
				var temp = " and  NhatKyThaoTac.UserID = @UserID ";
				query += temp;
			}
			query += " order by ThoiDiem desc ";

			var table = SqlDataAccessHelper.ExecuteQueryString(query,
				new string[] { "@StartThoiDiemSua", "@EndddThoiDiemSua", "@UserID" },
				new object[] { StartThoiDiemSua, EndddThoiDiemSua, UserID });

			var temp1 = (from DataRow row in table.Rows.Cast<DataRow>()
			           select new {loai = row["Loai"]}).DistinctBy(o=>o.loai).ToList();

			cbLoaiThaoTac.SelectionChangeCommitted -= cbLoaiThaoTac_OnSelectionChangeCommitted;
			cbLoaiThaoTac.DataSource = null;
			cbLoaiThaoTac.DataSource = temp1;
			cbLoaiThaoTac.DisplayMember = "loai";
			cbLoaiThaoTac.SelectionChangeCommitted += cbLoaiThaoTac_OnSelectionChangeCommitted;
			DataView view = new DataView(table);
			dgrdLichSu.DataSource = view;
		}

		private void cbLoaiThaoTac_OnSelectionChangeCommitted(object sender, EventArgs eventArgs)
		{
			if (cbLoaiThaoTac.SelectedValue == null) return;

			var dataView = dgrdLichSu.DataSource as DataView;
			dynamic temp = (dynamic)cbLoaiThaoTac.SelectedValue;
			if (dataView != null) dataView.RowFilter = string.Format( "Loai like '%{0}%'" ,temp.loai);

		}

		private void frm_32_XemLichSu_Load(object sender, EventArgs e)
		{
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000); 
				this.Close();
				return;
			}

			var table = SqlDataAccessHelper.ExecuteQueryString("select UserID,UserAccount from NewUserAccount");
			cbTaiKhoanThucHien.ValueMember = "UserID";
			cbTaiKhoanThucHien.DisplayMember = "UserAccount";
			cbTaiKhoanThucHien.DataSource = table;

			dtpStartThoiDiemSua.Value = DateTime.Today;
			dtpEndddThoiDiemSua.Value = DateTime.Now;
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void btnTim_Click(object sender, EventArgs e) {
			if (dgrdLichSu.DataSource == null) return;

			var searchStr1 = tbSearch.Text;
			var searchStr = string.Format("NoiDung like '%{0}%' or Loai like '%{0}%' or UserFullName like '%{0}%'", searchStr1);
			var dataView = dgrdLichSu.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = searchStr;

		}

		private void linkHienThiTatCaNV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			var dataView = dgrdLichSu.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = string.Empty;

		}

		private void tbSearch_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter)
				btnTim.PerformClick();

		}



	}
}
