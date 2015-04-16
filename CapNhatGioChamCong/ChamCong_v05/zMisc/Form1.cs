using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.DTO;
using ChamCong_v05.Helper;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;

namespace ChamCong_v05.zMisc {
	public partial class Form1 : Form {
		public List<cUserInfo> m_DSNV;
		public DateTime m_NgayBD;
		public DateTime m_NgayKT;
		public DataTable m_TableGioThieuChamCong;

		public Form1() {
			InitializeComponent();
			//SqlDataAccessHelper.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=WiseEyeV5Express;Integrated Security=true";
			m_TableGioThieuChamCong = this.CreateDataTable_CIO_ThieuChamCong();
			GridLocalizer.Active = new VietGridLocalizer();
			Localizer.Active = new Localizer();
		}

		private DataTable CreateDataTable_CIO_ThieuChamCong() {
			DataTable kq = new DataTable();
			kq.Columns.Add("UserEnrollNumber", typeof(int));
			kq.Columns.Add("UserFullName", typeof(string));
			kq.Columns.Add("UserFullCode", typeof(string));
			kq.Columns.Add("Ngay", typeof(DateTime));
			kq.Columns.Add("GioVao", typeof(DateTime));
			kq.Columns.Add("GioRa", typeof(DateTime));
			kq.Columns.Add("DSCa", typeof(string));
			kq.Columns.Add("cCheckInOut", typeof(cCheckInOut));
			kq.Columns.Add("cUserInfo", typeof (cUserInfo));
			return kq;
		}

		private void Form1_Load(object sender, EventArgs e) {
			XacDinhGioThieuChamCong(this.m_DSNV);
			gridControl1.DataSource = this.m_TableGioThieuChamCong;
		}

		public void XacDinhGioThieuChamCong(List<cUserInfo> listNhanVien) {
			if (listNhanVien == null) return;
			foreach (cUserInfo nhanvien in listNhanVien) {
				foreach (cNgayCong ngayCong in nhanvien.DSNgayCong.Where(item => item.Ngay >= m_NgayBD && item.Ngay <= m_NgayKT).ToList()) {
					foreach (cCheckInOut CIO in ngayCong.DSVaoRa.Where(item => item.HaveINOUT < 0)) {
						DataRow newRow = this.CreateDataRow_CIO_ThieuChamCong(this.m_TableGioThieuChamCong, nhanvien, ngayCong, CIO);
						m_TableGioThieuChamCong.Rows.Add(newRow);
					}
				}
			}
		}

		private DataRow CreateDataRow_CIO_ThieuChamCong(DataTable dataTable, cUserInfo Nhanvien, cNgayCong NgayCong, cCheckInOut CIO) {
			DataRow kq = dataTable.NewRow();
			kq["UserEnrollNumber"] = Nhanvien.MaCC;
			kq["UserFullName"] = Nhanvien.TenNV;
			kq["UserFullCode"] = Nhanvien.MaNV;
			kq["Ngay"] = NgayCong.Ngay;
			kq["GioVao"] = CIO.Vao != null ? CIO.Vao.Time : (object)DBNull.Value;
			kq["GioRa"] = CIO.Raa != null ? CIO.Raa.Time : (object)DBNull.Value;
			kq["DSCa"] = CIO.ExportKyHieuThuocCa1_5(true).XoaKyTuPhanCachDauTien();
			kq["cCheckInOut"] = CIO;
			kq["cUserInfo"] = Nhanvien;

			return kq;
		}

		private void button1_Click(object sender, EventArgs e) {

		}

		private void btnBoSung_Click(object sender, EventArgs e) {
			/* 1. lấy datasource để cập nhật lại sau khi thực hiện thêm xong hàng loạt
			 * 2. lấy thông tin để thực hiện thêm giờ xuống csdl
			 * 2. kiểm tra kết nối và thực hiện thêm giờ xuống csdl
			 * 3. lưu lại ds tạm NV bị thay đổi giờ -> DSNV CầnReload
			 * thực hiện remove khỏi danh sách nhân viên các NV bị thay đổi đó 
			 * 4. remove khỏi datasource các dòng có nhân viên bị thay đổi giờ
			 * reload lại chấm công của dsnvCầnReload đó
			 * 5. add lại các nhân viên bị thay đổi giờ vào listNhânviên
			 * 6. tạo các dòng dữ liệu thỏa điều kiện của riêng các nhân viên cần reload 
			 * insert vào đầu datasource các dòng dữ liệu thỏa điều kiện đó
			 */
			DataTable table = gridView1.DataSource as DataTable;
			bool checkBoSungVao = checkBoxBoSungVao.Checked, checkBoSungRaa = checkBoxBoSungRaa.Checked;

			int[] arraySelectedRow = gridView1.GetSelectedRows();
			List<DataRow> listSelectedRow = arraySelectedRow.Select(index => gridView1.GetDataRow(index)).ToList();

			TimeSpan gioVaoMoi = ((DateTime)timeEditBoSungVao.EditValue).TimeOfDay;
			TimeSpan gioRaaMoi = ((DateTime)timeEditBoSungRaa.EditValue).TimeOfDay;

			// tbd kiểm tra kết nối trước khi thực hiện
			List<cUserInfo> DSNVReload = new List<cUserInfo>();

			this.ThucHienThemGioXuongCSDL(ref DSNVReload, ref listSelectedRow, checkBoSungVao, checkBoSungRaa, gioVaoMoi, gioRaaMoi );
			this.RemoveDSNVCanReload(ref DSNVReload);
			XL.XemCongThoiGianChuaKetLuong(DSNVReload, m_NgayBD, m_NgayKT);
			m_DSNV.AddRange(DSNVReload);
			RemoveDataRowHaveDSNVReload(ref table, DSNVReload);
			XacDinhGioThieuChamCong(DSNVReload);
		}

		private void RemoveDataRowHaveDSNVReload(ref DataTable table, List<cUserInfo> DSNVReload) {
			//table.Rows.Cast<DataRow>().Where(item => item["cUserIn"])
		}

		private void RemoveDSNVCanReload(ref List<cUserInfo> DSNVReload)
		{
			for (int i = 0; i < DSNVReload.Count; i++)
			{
				cUserInfo nhanvien = DSNVReload[i];
				this.m_DSNV.Remove(nhanvien);
			}
		}

		private void ThucHienThemGioXuongCSDL(ref List<cUserInfo> DSNVReload, ref List<DataRow> listSelectedRow, 
			bool checkBoSungVao, bool checkBoSungRaa, TimeSpan gioVaoMoi, TimeSpan gioRaaMoi) {
			foreach (DataRow dataRow in listSelectedRow) {
				cCheckInOut currentCIO = (cCheckInOut)dataRow["cCheckInOut"];
				cUserInfo currentNhanVien = (cUserInfo) dataRow["cUserInfo"];
				if (DSNVReload.Any(item =>item.MaCC == currentNhanVien.MaCC) == false) DSNVReload.Add(currentNhanVien);

				int UEN = (int)dataRow["UserEnrollNumber"];
				if (currentCIO == null || currentNhanVien == null) continue;

				if (checkBoSungVao) {
					this.BoSungGio(currentCIO, MayCheck.I, gioVaoMoi, UEN);
				}
				if (checkBoSungRaa) {
					this.BoSungGio(currentCIO, MayCheck.O, gioRaaMoi, UEN);
				}
			}
			//tbd thông báo lỗi các trường hợp không thể thêm giờ
		}

		private void BoSungGio(cCheckInOut CIO, MayCheck LoaiCheck, TimeSpan gioBoSung, int UEN) {
			if (CIO.HaveINOUT == -1 && LoaiCheck.ToString() == MayCheck.I.ToString()) return; // có vào thiếu ra , bổ sung ra thì trùng bỏ qua
			if (CIO.HaveINOUT == -2 && LoaiCheck.ToString() == MayCheck.O.ToString()) return;// có ra thiếu vào, bổ sung vào thì trùng bỏ qua

			if (CIO.HaveINOUT == -2) // bổ sung giờ vào cho CIO thiếu vào --> ok
			{
				//if (LoaiCheck.ToString() == MayCheck.I.ToString())
				/* xác định giờ ra đã có, ngày ra đã có, bỏ phần dư giây giờ bổ sung (vào = 0, ra =1) (hh:mm:00)
				 * tạo NGÀY giờ vào mới để thêm vào CSDL, (nếu vào sau ra thì có qua đêm phải add thêm 1 ngày) */
				TimeSpan gioRa = CIO.Raa.Time.TimeOfDay;// xác định giờ ra
				DateTime ngayGioVaoMoi = CIO.Raa.Time.Date;//ngày ra
				gioBoSung = new TimeSpan(gioBoSung.Hours, gioBoSung.Minutes, 0); // giây = 0--> hh:mm:00
				ngayGioVaoMoi = (gioBoSung > gioRa) ? ngayGioVaoMoi.AddDays(-1d).Add(gioBoSung) : ngayGioVaoMoi.Add(gioBoSung); // thêm 1 ngày nếu vào sau ra
				
				#region

				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName.CheckInOut_Ins.ToString(),
															 new SqlParameter("@UserEnrollNumber", UEN),
															 new SqlParameter("@TimeDate", ngayGioVaoMoi.Date),
															 new SqlParameter("@TimeStr", ngayGioVaoMoi),
															 new SqlParameter("@OriginType", LoaiCheck.ToString()),
															 new SqlParameter("@MachineNo", (int)LoaiCheck),
															 new SqlParameter("@Source", "PC"));

				#endregion

				if (kq == 0) { }
			}
			else if (CIO.HaveINOUT == -1) {//bổ sung giờ vào cho CIO đã có vào
				/* xác định giờ vào đã có, ngày ra đã có, bỏ phần dư giây giờ bổ sung (vào = 0, ra =1) (hh:mm:00)
				 * tạo NGÀY giờ vào mới để thêm vào CSDL, () */
				TimeSpan gioVao = CIO.Vao.Time.TimeOfDay;// xác định giờ vào
				DateTime ngayGioRaaMoi = CIO.Vao.Time.Date;//ngày ra
				gioBoSung = new TimeSpan(gioBoSung.Hours, gioBoSung.Minutes, 1); // giây = 1 --> hh:mm:01 ra luôn sau vào
				ngayGioRaaMoi = (gioVao > gioBoSung) ? ngayGioRaaMoi.AddDays(1d).Add(gioBoSung) : ngayGioRaaMoi.Add(gioBoSung); // thêm 1 ngày nếu ra trước vào

				#region 

				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName.CheckInOut_Ins.ToString(),
				                                             new SqlParameter("@UserEnrollNumber", UEN),
				                                             new SqlParameter("@TimeDate", ngayGioRaaMoi.Date),
				                                             new SqlParameter("@TimeStr", ngayGioRaaMoi),
				                                             new SqlParameter("@OriginType", LoaiCheck.ToString()),
				                                             new SqlParameter("@MachineNo", (int) LoaiCheck),
				                                             new SqlParameter("@Source", "PC"));

				#endregion
			}
			else {
			}
		}
	}
}
