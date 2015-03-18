using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;

namespace ChamCong_v03.DieuChinhLuong {
	public partial class frm_CapNhat : Form {
		public bool IsReload = false;
		public DataRow[] Row;
		public DateTime thang;
		public frm_CapNhat() {
			InitializeComponent();
		}

		private void Form3_Load(object sender, EventArgs e)
		{
			IsReload = false;
			dtpThang.Value = thang;
		}

		private void btnThucHien_Click(object sender, EventArgs e)
		{
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			IsReload = true;
			var luongdieuchinh = (Double)numLuongDieuChinh.Value;
			var tamung = (Double)numTamUngThang.Value;
			var thuchikhac = (Double)numThuChiKhac.Value;
			var mucdongbhxh = 0f;
			if (Single.TryParse(tbMucDongBH.Text, out mucdongbhxh) == false)
			{
				AutoClosingMessageBox.Show("Mức đóng BHXH không hợp lệ. Vui lòng nhập lại.","Thông báo", 4000);
				return;
			}
			var thang = dtpThang.Value;

			var flagError = false;
			foreach (var row in Row) {
				var UserEnrollNumber = (int)row["UserEnrollNumber"];
				var UserFullCode = row["UserFullCode"].ToString();
				var tennv = (row["UserFullName"].ToString());
				var kq = DAO.DAL.CapNhatLuongDieuChinh_TamUng_ThuChiKhac(UserEnrollNumber, thang.Month, thang.Year, luongdieuchinh,tamung, thuchikhac, mucdongbhxh);
				if (kq == 0) {
					flagError = true;
					AutoClosingMessageBox.Show(
						string.Format("Xảy ra lỗi trong quá trình cập nhật tại vị trí nhân viên {0}, mã {1}.\nVui lòng thử lại.", tennv,
									  UserFullCode), "Lỗi", 3000);
					break;
				}
			}
			if (flagError == false) {
				AutoClosingMessageBox.Show("Thực hiện thành công.", "Thông báo", 2000);
				this.Close();
			}
			
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}
