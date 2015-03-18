using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DAL;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;

namespace ChamCong_v04.UI.TinhLuong {
	public partial class frm2QLLuongCongNhat : Form {
		public DateTime m_thang;
		public DataTable m_OriginTableCongNhat;
		public frm2QLLuongCongNhat() {
			InitializeComponent();

			dgrdDSLuongCongnhat.AutoGenerateColumns = false;
		}

		private void frmQLLuongCongNhat_Load(object sender, EventArgs e)
		{
			groupBox1.Text = string.Format(groupBox1.Text, m_thang.ToString("MM/yyyy"));// danh sách nhân viên chi công nhật tháng {0}
			#region kiểm tra kết nối csdl, mất kết nối thì thoát

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				return;
			}

			#endregion
			// load danh sách nv tính lương công nhật trong tháng
			m_OriginTableCongNhat = DAO.LayTableCongNhat(m_thang);
			dgrdDSLuongCongnhat.DataSource = m_OriginTableCongNhat;
			dgrdDSLuongCongnhat.ClearSelection();
		}

		private void dgrdDSLuongCongnhat_SelectionChanged(object sender, EventArgs e) {
			if (dgrdDSLuongCongnhat.SelectedRows.Count == 0)
			{
				MyUtility.ClearControlText(tbTenNV, tbMaNV, tbPhongBan, tbChucvu, tbCong);
				lbUserEnrollNumber.Tag = null;
				numDonGiaLuong.Value = 0;
				numTamUng.Value = 0;
				btnCapNhat.Enabled = false;// ko chọn dòng nào--> ko fill thông tin, disable btnCapnhat
				return;
			}

			btnCapNhat.Enabled = true;// có chọn dòng, enable btnCapnhat
			// lấy dòng được chọn
			DataRowView selectedRow = (DataRowView)(dgrdDSLuongCongnhat.SelectedRows[0]).DataBoundItem;
			if (selectedRow == null) return;

			//fill thông tin nhân viên công nhật của dòng được chọn
			tbTenNV.Text = selectedRow["UserFullName"].ToString();
			tbMaNV.Text = selectedRow["UserFullCode"].ToString();
			lbUserEnrollNumber.Tag = selectedRow["UserEnrollNumber"];
			lbUserEnrollNumber.Text = ((int)selectedRow["UserEnrollNumber"]).ToString();
			tbPhongBan.Text = selectedRow["TenPhong"].ToString();
			tbPhongBan.Tag = (selectedRow["IDPhong"] != DBNull.Value) ? (int) selectedRow["IDPhong"] : -1;
			tbChucvu.Text = selectedRow["ChucVu"].ToString();
			tbCong.Text = ((float)selectedRow["SoNgayCong"]).ToString("00.00");
			var dongiaLuong = (selectedRow["DonGiaLuong"] == DBNull.Value) ? 0 : (int)selectedRow["DonGiaLuong"];
			var tamung = (selectedRow["TamUng"] == DBNull.Value) ? 0d : Convert.ToDouble(selectedRow["TamUng"]);
			numDonGiaLuong.Value = dongiaLuong;
			numTamUng.Value = Convert.ToDecimal(tamung);
		}

		private void btnCapNhat_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			#region ko cho cập nhật lương công nhật nếu tháng này đã đã kết lương

			if (XL.Kiemtra(m_thang, MyUtility.LastDayOfMonth(m_thang))) {
				MessageBox.Show(string.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "cập nhật thông tin làm việc công nhật", "cập nhật thông tin làm việc công nhật", ""),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}
			
			#endregion

			// lấy thông tin từ form
			var UserEnrollNumber = (lbUserEnrollNumber.Tag == null) ? -1 : (int)lbUserEnrollNumber.Tag;
			var DonGiaLuong = (int)numDonGiaLuong.Value;
			var TamUng = Convert.ToDouble(numTamUng.Value);

			#region update đơn giá lương , tạm ứng xuống csdl
			//info ko cần lưu thành tiền vì lúc lưu xuống đã tính thành tiền để có cơ sở tính lương, lúc lấy lên thì tính ra số tổng rồi mới xuất bb
			string query = @" 
update DSNVChiCongNhatThang 
set DonGiaLuong=@DonGiaLuong, TamUng=@TamUng 
where UserEnrollNumber=@UserEnrollNumber and Thang=@Thang ";
			int kq2 = SqlDataAccessHelper.ExecNoneQueryString(query,
				new string[]{"@DonGiaLuong", "@TamUng", "@UserEnrollNumber", "@Thang"},
				new object[]{DonGiaLuong, TamUng, UserEnrollNumber, m_thang});
			DAO.GhiNhatKyThaotac("Cập nhật lương công nhật và tạm ứng", 
				string.Format("Cập nhật đơn giá lương công nhật [{1}], tạm ứng [{2}] cho NV có mã chấm công [{0}]", 
				UserEnrollNumber, DonGiaLuong.ToString(Settings.Default.numFormatMoney), TamUng.ToString(Settings.Default.numFormatMoney)), maCC:UserEnrollNumber);
			#endregion

			// sau khi cập nhật thì reload lại
			var table = DAO.LayTableCongNhat(m_thang);
			dgrdDSLuongCongnhat.DataSource = table;
			dgrdDSLuongCongnhat.ClearSelection();
		}


		private void btnTiepTuc_Click(object sender, EventArgs e) {
			frm3NhapThuChiThang frm = new frm3NhapThuChiThang {m_thang = m_thang};
			frm.WindowState = FormWindowState.Normal;
			frm.StartPosition = FormStartPosition.Manual;
			frm.MdiParent = this.MdiParent;
			//frm.Location = XL2.GetCenterLocation(frm.MdiParent.Size.Width, frm.MdiParent.Size.Height, frm.Size.Width, frm.Size.Height);
			frm.Location = new Point(0, 0);//XL2.GetCenterLocation(frm.MdiParent.ClientRectangle.Width, frm.MdiParent.ClientRectangle.Height, frm.Size.Width, frm.Size.Height);
			frm.Show();
			Close();
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			Close();
		}


	}
}
