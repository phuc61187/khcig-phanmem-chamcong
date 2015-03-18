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
using ChamCong_v03.DTO;
using ChamCong_v03.Properties;

namespace ChamCong_v03.LuongCongNhat {
	public partial class frmThemLuongCongNhat : Form {
		public bool isReload;
		public DateTime m_thang;
		public int m_mode;
		public int m_id;
		public string m_ten = string.Empty;
		public string m_chucvu = string.Empty;
		public double m_dongialuong = 0d;
		public double m_ngaycong = 0d;
		public double m_thanhtien = 0d;
		public double m_tamung = 0d;
		public double m_thuclanh = 0d;

		public frmThemLuongCongNhat() {
			InitializeComponent();

		}

		private void Form1_Load(object sender, EventArgs e) {
			isReload = false;
			dtpThang.Value = m_thang;
			if (m_mode == 1) // chế độ thêm
			{
			}
			else// chế độ cập nhật
			{
				lbID.Text            = m_id.ToString();
				tbTen.Text           = m_ten;
				tbChucVu.Text        = m_chucvu;
				numDonGiaLuong.Value = (decimal)m_dongialuong;
				tbNgayCong.Text      = m_ngaycong.ToString("##.#");
				tbThanhTien.Text     = m_thanhtien.ToString("###,###,###,##0.000");
				numTamUng.Value      = (decimal)m_tamung;
				tbThucLanh.Text      = m_thuclanh.ToString("###,###,###,##0.000");
			}
		}

		private void btnThucHien_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			#region lấy dữ liệu

			numDonGiaLuong.Update();
			numTamUng.Update();
			var hoten = tbTen.Text;
			var chucdanh = tbChucVu.Text;
			var dongialuong = (double)numDonGiaLuong.Value;
			var ngayCong = (tbNgayCong.Tag == null) ? 0d : (double)tbNgayCong.Tag;
			var dthantien = (tbThanhTien.Tag == null) ? 0d : (double)tbThanhTien.Tag;
			var dtamung = (double)numTamUng.Value;
			var dthuclanh = (tbThucLanh.Tag == null) ? 0d : (double)tbThucLanh.Tag;
			var thang = dtpThang.Value.Month;
			var nam = dtpThang.Value.Year;

			#endregion

			if (m_mode == 1) // chế độ thêm
			{
				if (MessageBox.Show("Thêm lương công nhật cho nhân viên của tháng " + dtpThang.Value.ToString("M/yyyy"), Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
					return;
				}
				isReload = true;
				if (DAL.ThemLuongCongNhat(thang, nam, hoten, chucdanh, dongialuong, ngayCong, dthantien, dtamung, dthuclanh) != 0) {
					AutoClosingMessageBox.Show("Thực hiện thành công.", Resources.capXacNhan, 2000);
					//sau khi thực hiện thành công thì giữ nguyên ko đóng form
				}
				else {
					AutoClosingMessageBox.Show("Vui lòng thử lại.", "Lỗi", 2000);
				}
			}
			else // chế độ cập nhật
			{
				if (MessageBox.Show("Cập nhật lương công nhật cho nhân viên của tháng " + dtpThang.Value.ToString("M/yyyy"), Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
					return;
				}
				var id = int.Parse(lbID.Text);
				isReload = true;
				if (DAL.CapNhatLuongCongNhat(id, thang, nam, hoten, chucdanh, dongialuong, ngayCong, dthantien, dtamung, dthuclanh) != 0) {
					AutoClosingMessageBox.Show("Thực hiện thành công.", Resources.capXacNhan, 2000);
					this.Close();// sau kh thực hiện thành công thì đóng form
				}
				else {
					AutoClosingMessageBox.Show("Vui lòng thử lại.", "Lỗi", 2000);
				}

			}
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}


		private void numTamUng_ValueChanged(object sender, EventArgs e) {
			numTamUng.Update();
			numDonGiaLuong.Update();
			string sngaycong = tbNgayCong.Text;
			var dongia = (double)numDonGiaLuong.Value;
			var dNgayCong = 0d;
			var dthanhtien = 0d;
			if (Double.TryParse(sngaycong, out dNgayCong)) {
				dthanhtien = dNgayCong * dongia;
				tbNgayCong.Tag = dNgayCong;
			}
			else {
				dthanhtien = 0d;
				tbNgayCong.Tag = 0d;
			}
			tbThanhTien.Text = dthanhtien.ToString("###,###,###,##0.000");
			tbThanhTien.Tag = dthanhtien;
			var dtamung = (double)numTamUng.Value;
			var dthuclanh = dthanhtien - dtamung;
			tbThucLanh.Text = dthuclanh.ToString("###,###,###,##0.000");
			tbThucLanh.Tag = dthuclanh;
		}

		private void maskedTextBox1_TextChanged(object sender, EventArgs e) {
			numTamUng.Update();
			numDonGiaLuong.Update();
			string sngaycong = tbNgayCong.Text;
			var dongia = (double)numDonGiaLuong.Value;
			var dNgayCong = 0d;
			var dthanhtien = 0d;
			if (Double.TryParse(sngaycong, out dNgayCong)) {
				dthanhtien = dNgayCong * dongia;
				tbNgayCong.Tag = dNgayCong;
			}
			else {
				dthanhtien = 0d;
				tbNgayCong.Tag = 0d;
			}
			tbThanhTien.Text = dthanhtien.ToString("###,###,###,##0.000");
			tbThanhTien.Tag = dthanhtien;
			var dtamung = (double)numTamUng.Value;
			var dthuclanh = dthanhtien - dtamung;
			tbThucLanh.Text = dthuclanh.ToString("###,###,###,##0.000");
			tbThucLanh.Tag = dthuclanh;

		}


	}
}
