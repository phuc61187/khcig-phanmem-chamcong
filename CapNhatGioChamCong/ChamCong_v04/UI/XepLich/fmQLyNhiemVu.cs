using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;

namespace ChamCong_v04.UI.XepLich {
	public partial class fmQLyNhiemVu : Form {
		public fmQLyNhiemVu() {
			InitializeComponent();
		}

		private void fmQLyNhiemVu_Load(object sender, EventArgs e) {
			DataTable tableNhiemVu = SqlDataAccessHelper.ExecSPQuery(SPName.sp_NhiemVu_DocBang.ToString(), new SqlParameter("@LoaiNhiemVu", 1));//1 là nvụ chính
			listDSNhiemVu.DataSource = tableNhiemVu;
			listDSNhiemVu.ValueMember = "MaNhiemVu";
			listDSNhiemVu.DisplayMember = "TenNhiemVu";
			listDSNhiemVu.ClearSelected();
			listDSNhiemVu.SelectedIndexChanged += listDSNhiemVu_SelectedIndexChanged;

		}

		private void btnTaoNhiemVu_Click(object sender, EventArgs e) {
			if (btnTaoNhiemVu.Enabled) {
				tbTenNhiemVu.Clear();
				lbMaNhiemVu.Tag = null;
				btnTaoNhiemVu.Enabled = false; // ấn xuống
				tbTenNhiemVu.Focus();
			}
		}


		private void btnLuuNhiemVu_Click(object sender, EventArgs e)
		{
			string tenNhiemVu = tbTenNhiemVu.Text;
			//1. validate     hiện tại ko chọn item nào thì thoát, tên nhiệm vụ trống thì báo
			
			if (tenNhiemVu.Trim() == string.Empty)
			{
				ACMessageBox.Show("Tên nhiệm vụ không được để trống.", Resources.Caption_ThongBao, 2000);
				return;
			}

			#region kiểm tra kết nối csdl trước
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}
			#endregion

			//2. save to DB
			if (btnTaoNhiemVu.Enabled) // đang chế độ cập nhật
			{
				if (lbMaNhiemVu.Tag == null) return; // đang cập nhật mà ko chọn item nào thì thoát
				int maNvu = (int)lbMaNhiemVu.Tag;
				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName.sp_NhiemVu_InsUpd.ToString(),
															 new SqlParameter("@MaNhiemVu", maNvu),
															 new SqlParameter("@TenNhiemVu", tbTenNhiemVu.Text));
				if (kq == 0) MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, MessageBoxButtons.OK);
			}
			else // đang chế độ thêm mới
			{
				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName.sp_NhiemVu_InsUpd.ToString(),
															 new SqlParameter("@MaNhiemVu", DBNull.Value),
															 new SqlParameter("@TenNhiemVu", tbTenNhiemVu.Text));
				if (kq == 0) MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, MessageBoxButtons.OK);
			}
			
			//3. reload GUI
			DataTable tableNhiemVu = SqlDataAccessHelper.ExecSPQuery(SPName.sp_NhiemVu_DocBang.ToString());
			listDSNhiemVu.SelectedIndexChanged -= listDSNhiemVu_SelectedIndexChanged;
			listDSNhiemVu.DataSource = tableNhiemVu;
			listDSNhiemVu.SelectedIndexChanged += listDSNhiemVu_SelectedIndexChanged;
			listDSNhiemVu.ValueMember = "MaNhiemVu";
			listDSNhiemVu.DisplayMember = "TenNhiemVu";
			listDSNhiemVu.ClearSelected();

			btnTaoNhiemVu.Enabled = true;
			tbTenNhiemVu.Clear();
		}


		private void btnHuy_Click(object sender, EventArgs e)
		{
			btnTaoNhiemVu.Enabled = true;
			tbTenNhiemVu.Clear();
			lbMaNhiemVu.Tag = null;
			listDSNhiemVu.ClearSelected();
		}


		private void btnXoaNhiemVu_Click(object sender, EventArgs e) {
			if (btnTaoNhiemVu.Enabled == false) return;// đang ở chế độ tạo mới thì ko cho xoá
			if (lbMaNhiemVu.Tag == null || listDSNhiemVu.SelectedItems.Count == 0)	 return;// ko chọn item thì ko cho xoá

			//xác nhận thực hiện
			if (MessageBox.Show(Resources.Text_ConfirmXoaNhiemVu, Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) return;

			//đã đồng ý thực hiện
			int maNhiemVu = (int) lbMaNhiemVu.Tag;
			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName.sp_NhiemVu_Del.ToString(), new SqlParameter("@MaNhiemVu", maNhiemVu));
			if (kq == 0) MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_ThongBao);
			else ACMessageBox.Show(Resources.Text_DaThucHienXong, Resources.Caption_ThongBao, 2000);
		}


		private void listDSNhiemVu_SelectedIndexChanged(object sender, EventArgs e) {
			if (listDSNhiemVu.SelectedItems.Count == 0) return; // chưa chọn item nào thì ko fill dữ liệu

			var selectedItem = listDSNhiemVu.SelectedItem;
			DataRowView rowView = selectedItem as DataRowView;
			if (rowView == null) return;
			lbMaNhiemVu.Tag = (int)rowView["MaNhiemVu"];
			tbTenNhiemVu.Text = rowView["TenNhiemVu"].ToString();
		}

	}
}
