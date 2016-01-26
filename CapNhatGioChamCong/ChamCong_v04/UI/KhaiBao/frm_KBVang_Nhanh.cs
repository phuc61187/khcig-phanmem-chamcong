using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DAL;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;

namespace ChamCong_v04.UI.KhaiBao {
	public partial class frm_KBVang_Nhanh : Form
	{
		public bool IsReload;
		public IEnumerable<dynamic> listMaCC_NgayVang;
		public frm_KBVang_Nhanh() {
			InitializeComponent();
		}

		private void btnThoat_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void frm_KBVang_Nhanh_Load(object sender, EventArgs e)
		{
			#region mất kết nối csdl thì báo

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 2000);
				Close();
				return;
			}

			#endregion

			var tableLoaiVang = DAO.LayDSLoaiVang();
			cbLoaiVang.DataSource = tableLoaiVang;
			cbLoaiVang.ValueMember = "AbsentCode";
			cbLoaiVang.DisplayMember = "AbsentDescription";

			List<frm_KBVang.Working> list = new List<frm_KBVang.Working>
				{
					new frm_KBVang.Working{cong = 0.5f, hour = 4f},
					new frm_KBVang.Working{cong = 1f, hour = 8f},
				};
			cbSoBuoi.DataSource = list;
			cbSoBuoi.ValueMember = "Hour";
			cbSoBuoi.DisplayMember = "Cong";
			cbSoBuoi.SelectedIndex = 1; // mặc định cho số buổi là 1

		}

		private void btnThem_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			// xác nhận thực hiện
			if (MessageBox.Show("Khai báo vắng cho các nhân viên?", Resources.Caption_XacNhan, MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
			#region //lấy loại vắng, workingDay, workingTime, absentCode, XL2.currUserID

			if (cbLoaiVang.SelectedItem == null)
			{
				ACMessageBox.Show("Bạn chưa chọn loại vắng", Resources.Caption_ThongBao, 2000);
				return;
			}
			var rowLV = cbLoaiVang.SelectedItem as DataRowView;
			var absentCode = rowLV["AbsentCode"].ToString();
			var workingDay = (float) ((frm_KBVang.Working) cbSoBuoi.SelectedItem).Cong;
			var phuCapString = maskedTextBox1.Text;
			var phuCapInt = 0;
			var phuCapFloat = 0f;
			if (int.TryParse(phuCapString, out phuCapInt)==false || phuCapString.Length < 3) { ACMessageBox.Show("Nhập phụ cấp chưa đúng định dạng.", Resources.Caption_Loi, 2000); return;}
			phuCapFloat = Convert.ToSingle(phuCapInt)/100f;

			#region set working time tùy theo workingDay

			var workingTime = 0f;

			if (Math.Abs(workingDay - 0f) < 0.01f) workingTime = 0f;
			else if (Math.Abs(workingDay - 0.5f) < 0.01f) workingTime = 4f;
			else if (Math.Abs(workingDay - 1f) < 0.01f) workingTime = 8f;

			#endregion

			#endregion

			IsReload = true;
			List<Error> listError = new List<Error>();
			XL.ThemNgayVang(listMaCC_NgayVang, workingDay, workingTime, phuCapFloat, absentCode, listError);
			if (listError.Count > 0)
			{
				frmError frm = new frmError { StartPosition = FormStartPosition.CenterParent,listError = listError };
				frm.ShowDialog();
			}
			Close();
		}
	}
}
