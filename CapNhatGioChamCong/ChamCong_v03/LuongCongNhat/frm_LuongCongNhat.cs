using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;
using ChamCong_v03.DieuChinhLuong;
using ChamCong_v03.Properties;
using OfficeOpenXml;
using log4net;
using log4net.Config;

namespace ChamCong_v03.LuongCongNhat {
	public partial class frm_LuongCongNhat : Form {

		public readonly ILog lg = LogManager.GetLogger("LuongCongNhat");

		public DateTime m_thang = DateTime.MinValue;
		public frm_LuongCongNhat() {
			InitializeComponent();

			log4net.Config.XmlConfigurator.Configure();
			dgrdDSNVTrgPhg.AutoGenerateColumns = false;
		}

		private void frm_LuongCongNhat_Load(object sender, EventArgs e)
		{
			//dtpThang.Value = m_thang;

		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}


		private void btnXem_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			var thang = dtpThang.Value;
			var table = DAL.LayBangLuongCongNhat(thang.Month, thang.Year);
			if (table.Rows.Count == 0) {
				AutoClosingMessageBox.Show("Chưa có chi tiết lương công nhật nào trong tháng.", Resources.capThongBao, 2000);
			}
			dgrdDSNVTrgPhg.DataSource = table;
		}



		private void btnThemMoi_Click(object sender, EventArgs e) {
			frmThemLuongCongNhat frm = new frmThemLuongCongNhat();
			frm.m_thang = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);
			frm.m_mode = 1;// mở form thêm lương công nhật với chế độ thêm
			frm.Location = new Point((int)((this.Size.Width - frm.Size.Width) / 2f), (int)((this.Size.Height - frm.Size.Height) / 2f));
			frm.ShowDialog();
			if (frm.isReload) {
				Thread.Sleep(20);
				btnXem.PerformClick();
			}
		}

		private void btnCapNhat_Click(object sender, EventArgs e) {
			if (dgrdDSNVTrgPhg.SelectedRows.Count == 0) return;
			var row = ((DataRowView)dgrdDSNVTrgPhg.SelectedRows[0].DataBoundItem).Row;
			var id = (int)row["ID"];
			frmThemLuongCongNhat frm = new frmThemLuongCongNhat();
			frm.m_thang = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);
			frm.m_mode = 0;// mở form thêm lương công nhật với chế độ cập nhật

			#region truyền giá trị hiển thị cho form

			frm.m_id = id;
			frm.m_ten = row["Ten"].ToString();
			frm.m_chucvu = row["ChucVu"].ToString();
			frm.m_dongialuong = Convert.ToDouble(row["DonGiaLuong"]);
			frm.m_ngaycong = Convert.ToDouble(row["NgayCong"]);
			frm.m_thanhtien = Convert.ToDouble(row["ThanhTien"]);
			frm.m_tamung = Convert.ToDouble(row["TamUng"]);
			frm.m_thuclanh = Convert.ToDouble(row["ThucLanh"]);

			#endregion

			frm.Location = new Point((int)((this.Size.Width - frm.Size.Width) / 2f), (int)((this.Size.Height - frm.Size.Height) / 2f));
			frm.ShowDialog();
			if (frm.isReload) {
				Thread.Sleep(20);
				btnXem.PerformClick();
			}

		}

		private void btnXoa_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			if (MessageBox.Show("Xoá chi tiết lương công nhật của nhân viên?", Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}

			var row = ((DataRowView)dgrdDSNVTrgPhg.SelectedRows[0].DataBoundItem).Row;
			var id = (int)row["ID"];
			if (DAL.XoaLuongCongNhat(id) != 0) {
				AutoClosingMessageBox.Show("Thực hiện thành công.", Resources.capXacNhan, 2000);
				Thread.Sleep(20);
				btnXem.PerformClick();
				//sau khi thực hiện thành công thì giữ nguyên ko đóng form
			}
			else {
				AutoClosingMessageBox.Show("Vui lòng thử lại.", "Lỗi", 2000);
			}
		}


	}
}
