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
using DevExpress.XtraEditors;

namespace ChamCong_v06.UI {
	public partial class frmXemCong : Form {
		public List<cNhomCa> m_AllNhomCa = new List<cNhomCa>();
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

			BUS_LichTrinh_Ca busLichTrinhCa = new BUS_LichTrinh_Ca();
			busLichTrinhCa.LayTatCaLichTrinhVaCa(ref m_AllNhomCa);

			LoadDSNV();
		}

		private void btnThucHien_CCThang_Click(object sender, EventArgs e) {
			//1. kiểm tra tháng chấm công hợp lệ, lấy dsnv ( chưa chọn thì báo)
			DateTime thangChamCong;
			if (Validate_ThangChamCong(dateEdit_ThangCC, out thangChamCong) == false) return;

			List<cUserInfo> dsnv_duocChon = new List<cUserInfo>();
			LayDSNV_DuocChon(gridView_DSNV, dsnv_duocChon);
			
			BUS_ChamCong busChamCong = new BUS_ChamCong();
			FromToDateTime khoangTg = new FromToDateTime{From = MyUtility.FirstDayOfMonth(thangChamCong), To = MyUtility.LastDayOfMonth(thangChamCong)};
			busChamCong.ChamCong2(dsnv_duocChon, khoangTg);
		}

		private void btnThucHien_XemCong_Click(object sender, EventArgs e)
		{
			//1. validate ngày bắt đầu, kết thúc
			//2. chỉ cho xem các ngày chưa kết lương, các ngày đã kết lương thì xem bên chỗ khác
			DateTime NgayBD_XemCong, NgayKT_XemCong;
			if (Validate_NgayBD_NgayKT(dateEdit_NgayBDCC, dateEdit_NgayKTCC, out NgayBD_XemCong, out NgayKT_XemCong) == false) return;

			if (Validate_NgayXemCong(NgayBD_XemCong, NgayKT_XemCong) == false) return;

			List<cUserInfo> dsnv_DuocChon = new List<cUserInfo>();
			LayDSNV_DuocChon(gridView_DSNV, dsnv_DuocChon);
			BUS_ChamCong busChamCong = new BUS_ChamCong();
			FromToDateTime khoangTg = new FromToDateTime { From = NgayBD_XemCong, To = NgayKT_XemCong }; //đã bỏ phần giờ, lấy phần ngày
			busChamCong.XemCong(dsnv_DuocChon, khoangTg);

			DataTable table_KQ_XemCong;
			BUS_NhanVien busNhanVien = new BUS_NhanVien();
			busNhanVien.XuatDataTableXemCong(dsnv_DuocChon, out table_KQ_XemCong);
			gridControl_BangCC.DataSource = table_KQ_XemCong;

			//ReloadThongKe();
		}

		private void ReloadThongKe(List<cUserInfo> DSNV_DuocChon, int SoTH_ThieuCC=0, int SoTH_KoNhanDienCa=0, int SoTH_OLaiChuaXN=0 ) {
			
		}

		private bool Validate_NgayXemCong(DateTime NgayBD_XemCong, DateTime NgayKT_XemCong) {
			//todo//validate chỉ xem công trong khoảng thời gian chưa kết lương
			return true;
		}

		private void buttonEdit_TimKiemNV_Properties_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {

		}

		private void gridControl_DSNV_Click(object sender, EventArgs e) {

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

		private void LayDSNV_DuocChon(DevExpress.XtraGrid.Views.Grid.GridView grid_DSNV, List<cUserInfo> dsnv_duocChon)
		{
			int[] selectedRowHandle = grid_DSNV.GetSelectedRows();
			BUS_NhanVien busNhanVien = new BUS_NhanVien();
			foreach (int rowHandle in selectedRowHandle)
			{
				DataRow dataRow = grid_DSNV.GetDataRow(rowHandle);
				cUserInfo nhanvien;
				busNhanVien.KhoiTaoNV(dataRow, m_AllNhomCa, out nhanvien);
				dsnv_duocChon.Add(nhanvien);
			}
		}

		private bool Validate_ThangChamCong(DateEdit DateEdit, out DateTime ThangChamCong) {
			ThangChamCong = DateEdit.DateTime;
			if (DateEdit.DateTime < DateTime.Now.AddYears(-2) || DateEdit.DateTime > DateTime.Now.AddMonths(2))
			{
				ACMessageBox.Show("Nhập tháng chấm công chưa hợp lệ.", Resources.Caption_Loi, 2000);
				return false;
			}

			return true;
		}

		private bool Validate_NgayBD_NgayKT(DateEdit dateEditNgayBdcc, DateEdit dateEditNgayKtcc, out DateTime ngayBdXemCong, out DateTime ngayKtXemCong)
		{
			ngayBdXemCong = new DateTime();
			ngayKtXemCong = new DateTime();

			if (dateEditNgayBdcc.DateTime < DateTime.Now.AddYears(-2) || dateEditNgayBdcc.DateTime > DateTime.Now.AddMonths(2)
				|| dateEditNgayKtcc.DateTime < DateTime.Now.AddYears(-2) || dateEditNgayKtcc.DateTime > DateTime.Now.AddMonths(2)) {
				ACMessageBox.Show("Nhập ngày chấm công chưa hợp lệ.", Resources.Caption_Loi, 2000);
				return false;
			}

			ngayBdXemCong = dateEditNgayBdcc.DateTime.Date; //bỏ phần giờ, lấy phần ngày
			ngayKtXemCong = dateEditNgayKtcc.DateTime.Date;//bỏ phần giờ, lấy phần ngày
			if (ngayKtXemCong < ngayBdXemCong)
			{
				DateTime temp = ngayBdXemCong;
				ngayBdXemCong = ngayKtXemCong;
				ngayKtXemCong = temp;
			}
			return true;
		}


	}
}
