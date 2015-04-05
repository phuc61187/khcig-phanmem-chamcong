using System;
using System.Data;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.DAL;
using ChamCong_v05.Helper;
using ChamCong_v05.Properties;

namespace ChamCong_v05.UI.QLNV {
	public partial class frmCapNhatCongNhat : Form {
		private DateTime m_thang = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
		public DataRowView m_currRowNV;
		public bool IsReload = false;

		#region hàm ko quan trọng

		public frmCapNhatCongNhat() {
			InitializeComponent();
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			Close();
		}

		private void checkLamCongnhat_CheckedChanged(object sender, EventArgs e) {
			dtpNgayBDCongnhat.Enabled = checkLamCongnhat.Checked;
			dtpNgayKTCongnhat.Enabled = checkLamCongnhat.Checked;
		}

		#endregion

		private void dtpThang_ValueChanged(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			// kiểm tra nếu tháng đó đã kết công thì ko cho nhập
			m_thang = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);
			DataTable tableThangketcong = DAO5.KiemtraTinhtrangKetcongThang(m_thang, (int)m_currRowNV["UserEnrollNumber"]);
			DataTable tableKhoangThoigianCongnhat = DAO5.LayKhoangThoigianCongnhat(m_thang, (int)m_currRowNV["UserEnrollNumber"]);
			if (tableThangketcong.Rows.Count > 0) // đã kết công --> ko cho nhập
			{
				checkLamCongnhat.Enabled = false;
				checkNVChinhThuc.Enabled = false;
				if (tableKhoangThoigianCongnhat.Rows.Count == 0) {// chưa khai báo công nhật tháng này 
					checkLamCongnhat.Checked = false;
					dtpNgayBDCongnhat.Value = m_thang;
					dtpNgayKTCongnhat.Value = m_thang;
					checkNVChinhThuc.Checked = false;
				}
				else {// đã khai báo công nhật tháng này 
					checkLamCongnhat.Checked = true;
					dtpNgayBDCongnhat.Value = (DateTime)tableKhoangThoigianCongnhat.Rows[0]["NgayBatDau"];
					dtpNgayKTCongnhat.Value = (DateTime)tableKhoangThoigianCongnhat.Rows[0]["NgayKetThuc"];
					checkNVChinhThuc.Checked = (bool)tableKhoangThoigianCongnhat.Rows[0]["NVChinhThuc"];
				}
			}
			else // chưa kết công cho phép nhập, kiểm tra nv đó có nhập ngày công nhật chưa, có thì fill, ko thì check false
			{
				checkLamCongnhat.Enabled = true;
				checkNVChinhThuc.Enabled = true;
				if (tableKhoangThoigianCongnhat.Rows.Count == 0) {// chưa khai báo công nhật tháng này -> cho phép nhập
					checkLamCongnhat.Checked = false;
					dtpNgayBDCongnhat.Value = m_thang;// cập nhật mặc định ngày đầu tháng
					dtpNgayKTCongnhat.Value = m_thang;
					checkNVChinhThuc.Checked = false;
				}
				else // đã khai báo công nhật --> fill
				{
					checkLamCongnhat.Checked = true;
					dtpNgayBDCongnhat.Value = (DateTime)tableKhoangThoigianCongnhat.Rows[0]["NgayBatDau"];
					dtpNgayKTCongnhat.Value = (DateTime)tableKhoangThoigianCongnhat.Rows[0]["NgayKetThuc"];
					checkNVChinhThuc.Checked = (bool)tableKhoangThoigianCongnhat.Rows[0]["NVChinhThuc"];
				}
			}
		}

		private void btnCapNhat_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			DateTime ngaydauthang = dtpThang.Value;
			int uen = (int)m_currRowNV["UserEnrollNumber"];
			bool LaNVChinhThuc = checkNVChinhThuc.Checked;
			DateTime ngayBD = dtpNgayBDCongnhat.Value;
			DateTime ngayKT = dtpNgayKTCongnhat.Value;
			if (ngayBD > ngayKT) MyUtility.Swap(ref ngayBD, ref ngayKT);
			if (checkLamCongnhat.Checked && checkLamCongnhat.Enabled) {
				#region query string

				var query = @"	
		UPDATE	DSNVChiCongNhatThang
		SET		IDPhong = @IDPhong ,TenPhong = @TenPhong
				,NgayBatDau = @NgayBatDau ,NgayKetThuc = @NgayKetThuc ,SoNgayCong = 0
				,NVChinhThuc = @NVChinhThuc
		WHERE	Thang = @Thang and UserEnrollNumber = @UserEnrollNumber
		if @@ROWCOUNT = 0
		INSERT INTO		DSNVChiCongNhatThang
				(Thang ,UserEnrollNumber ,IDPhong ,TenPhong
				,NgayBatDau ,NgayKetThuc ,SoNgayCong
				,DonGiaLuong ,TamUng ,NVChinhThuc)
		VALUES
				(@Thang ,@UserEnrollNumber ,@IDPhong ,@TenPhong
				,@NgayBatDau ,@NgayKetThuc ,0 
				,0 ,0 ,@NVChinhThuc) ";

				#endregion

				int idPhong = (int)m_currRowNV["MaPhong"];
				var tenPhong = m_currRowNV["TenPhong"].ToString();
				#region update or insert xuống csdl

				int kq = SqlDataAccessHelper.ExecNoneQueryString(
					query,
					new string[] {"@Thang", "@UserEnrollNumber", "@IDPhong", "@TenPhong", "@NgayBatDau", "@NgayKetThuc", "@NVChinhThuc"},
					new object[] {ngaydauthang, uen,idPhong,tenPhong,ngayBD,ngayKT,LaNVChinhThuc });
				DAO5.GhiNhatKyThaotac("Lưu ngày làm việc công nhật",
					string.Format("Lưu thời gian làm việc công nhật bắt đầu từ ngày [{1}] đến hết ngày [{2}], [{3}] cho NV có mã chấm công [{0}]",
					uen, ngayBD.ToString("dd/MM/yyyy"), ngayKT.ToString("dd/MM/yyyy"), LaNVChinhThuc ? "tính lương các ngày còn lại như NV chính thức" : ""), maCC:uen);
				if (kq == 0)// báo lỗi
				{
					MessageBox.Show(Resources.Text_CoLoi);
					return;
				}
				#endregion

				// sau khi update thì đóng form và reload 
				IsReload = true;
				Close();
			}
		}

		private void frmCapNhatCongNhat_Load(object sender, EventArgs e) {
			tbMaNV.Text = m_currRowNV["UserFullCode"].ToString();
			tbTenNV.Text = m_currRowNV["UserFullName"].ToString();
			tbMaNV.Tag = (int)m_currRowNV["UserEnrollNumber"];
			dtpThang.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
		}

	}
}
