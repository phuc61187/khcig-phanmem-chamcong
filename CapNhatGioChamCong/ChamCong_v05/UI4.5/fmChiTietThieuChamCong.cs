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
using ChamCong_v05.UI4._5;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ChamCong_v05.zMisc {
	public partial class fmChiTietThieuChamCong : Form {
		public List<cUserInfo> m_DSNV;
		public DateTime m_NgayBD;
		public DateTime m_NgayKT;
		public DataTable m_TableGioThieuChamCong;

		public fmChiTietThieuChamCong() {
			InitializeComponent();
			//SqlDataAccessHelper.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=WiseEyeV5Express;Integrated Security=true";
			m_TableGioThieuChamCong = this.CreateDataTable_CIO_ThieuChamCong();
			GridLocalizer.Active = new VietGridLocalizer();
			Localizer.Active = new Localizer();
		}

		private void Form1_Load(object sender, EventArgs e) {
			XacDinhGioThieuChamCong(this.m_DSNV);
			gridControl1.DataSource = this.m_TableGioThieuChamCong;
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
			kq.Columns.Add("cNgayCong", typeof(cNgayCong));
			kq.Columns.Add("cUserInfo", typeof(cUserInfo));
			return kq;
		}

		public void XacDinhGioThieuChamCong(List<cUserInfo> listNhanVien) {
			if (listNhanVien == null) return;
			foreach (cUserInfo nhanvien in listNhanVien) {
				foreach (cNgayCong ngayCong in nhanvien.DSNgayCong.Where(item => item.Ngay >= m_NgayBD && item.Ngay <= m_NgayKT).ToList()) {
					foreach (cCheckInOut CIO in ngayCong.DSVaoRa.Where(item => item.HaveINOUT < 0)) {
						DataRow newRow = this.CreateDataRow_CIO_ThieuChamCong(this.m_TableGioThieuChamCong, nhanvien, ngayCong, CIO);
						m_TableGioThieuChamCong.Rows.InsertAt(newRow, 0);
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
			kq["cNgayCong"] = NgayCong;

			return kq;
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
			DataTable table = gridControl1.DataSource as DataTable;
			bool checkBoSungVao = checkBoxBoSungVao.Checked, checkBoSungRaa = checkBoxBoSungRaa.Checked;

			int[] arraySelectedRow = gridView1.GetSelectedRows();
			List<DataRow> listSelectedRow = arraySelectedRow.Select(index => gridView1.GetDataRow(index)).ToList();

			TimeSpan gioVaoMoi = ((DateTime)timeEditBoSungVao.EditValue).TimeOfDay;
			TimeSpan gioRaaMoi = ((DateTime)timeEditBoSungRaa.EditValue).TimeOfDay;

			// tbd kiểm tra kết nối trước khi thực hiện
			List<cUserInfo> DSNVReload = new List<cUserInfo>();

			this.ThucHienThemGioXuongCSDL(ref DSNVReload, ref listSelectedRow, checkBoSungVao, checkBoSungRaa, gioVaoMoi, gioRaaMoi);
			this.RemoveDSNVCanReload(ref DSNVReload);
			XL.XemCongThoiGianChuaKetLuong(DSNVReload, m_NgayBD, m_NgayKT);
			m_DSNV.AddRange(DSNVReload);

			//reload GUI
			RemoveDataRowHaveDSNVReload(ref table, DSNVReload);
			XacDinhGioThieuChamCong(DSNVReload);

			MyUtility.CheckedCheckBox(false, checkBoxBoSungVao, checkBoxBoSungRaa);

		}

		private void btnDaoGioChamCong_Click(object sender, EventArgs e) {
			/* 1. lấy datasource để cập nhật lại sau khi thực hiện thêm xong hàng loạt
			 * 2. kiểm tra kết nối và thực hiện đảo giờ xuống csdl
			 * 3. lưu lại ds tạm NV bị thay đổi giờ -> DSNV CầnReload
			 * thực hiện remove khỏi danh sách nhân viên các NV bị thay đổi đó 
			 * 4. remove khỏi datasource các dòng có nhân viên bị thay đổi giờ
			 * reload lại chấm công của dsnvCầnReload đó
			 * 5. add lại các nhân viên bị thay đổi giờ vào listNhânviên
			 * 6. tạo các dòng dữ liệu thỏa điều kiện của riêng các nhân viên cần reload 
			 * insert vào đầu datasource các dòng dữ liệu thỏa điều kiện đó
			 */
			DataTable table = gridControl1.DataSource as DataTable;

			int[] arraySelectedRow = gridView1.GetSelectedRows();
			List<DataRow> listSelectedRow = arraySelectedRow.Select(index => gridView1.GetDataRow(index)).ToList();

			// tbd kiểm tra kết nối trước khi thực hiện
			List<cUserInfo> DSNVReload = new List<cUserInfo>();

			this.ThucHienDaoGioCCXuongCSDL(ref DSNVReload, ref listSelectedRow);
			this.RemoveDSNVCanReload(ref DSNVReload);
			XL.XemCongThoiGianChuaKetLuong(DSNVReload, m_NgayBD, m_NgayKT);
			m_DSNV.AddRange(DSNVReload);
			RemoveDataRowHaveDSNVReload(ref table, DSNVReload);
			XacDinhGioThieuChamCong(DSNVReload);

		}

		private void btnSuaGio_Click(object sender, EventArgs e) {
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
			DataTable table = gridControl1.DataSource as DataTable;
			bool checkSuaVao = checkBoxSuaVao.Checked, checkSuaRaa = checkBoxSuaRaa.Checked;

			int[] arraySelectedRow = gridView1.GetSelectedRows();
			List<DataRow> listSelectedRow = arraySelectedRow.Select(index => gridView1.GetDataRow(index)).ToList();

			TimeSpan gioVaoMoi = ((DateTime)timeEditSuaVao.EditValue).TimeOfDay;
			TimeSpan gioRaaMoi = ((DateTime)timeEditSuaRaa.EditValue).TimeOfDay;

			// tbd kiểm tra kết nối trước khi thực hiện
			List<cUserInfo> DSNVReload = new List<cUserInfo>();

			this.ThucHienSuaGioXuongCSDL(ref DSNVReload, ref listSelectedRow, checkSuaVao, checkSuaRaa, gioVaoMoi, gioRaaMoi);
			this.RemoveDSNVCanReload(ref DSNVReload);
			XL.XemCongThoiGianChuaKetLuong(DSNVReload, m_NgayBD, m_NgayKT);
			m_DSNV.AddRange(DSNVReload);

			//reload GUI
			RemoveDataRowHaveDSNVReload(ref table, DSNVReload);
			XacDinhGioThieuChamCong(DSNVReload);

			MyUtility.CheckedCheckBox(false, checkBoxSuaVao, checkBoxSuaRaa);
		}

		private void btnXoaGio_Click(object sender, EventArgs e) {
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
			DataTable table = gridControl1.DataSource as DataTable;
			bool checkXoaVao = checkBoxXoaVao.Checked, checkXoaRaa = checkBoxXoaRaa.Checked;

			int[] arraySelectedRow = gridView1.GetSelectedRows();
			List<DataRow> listSelectedRow = arraySelectedRow.Select(index => gridView1.GetDataRow(index)).ToList();

			// tbd kiểm tra kết nối trước khi thực hiện
			List<cUserInfo> DSNVReload = new List<cUserInfo>();

			this.ThucHienXoaGioXuongCSDL(ref DSNVReload, ref listSelectedRow, checkXoaVao, checkXoaRaa);
			this.RemoveDSNVCanReload(ref DSNVReload);
			XL.XemCongThoiGianChuaKetLuong(DSNVReload, m_NgayBD, m_NgayKT);
			m_DSNV.AddRange(DSNVReload);

			//reload GUI
			RemoveDataRowHaveDSNVReload(ref table, DSNVReload);
			XacDinhGioThieuChamCong(DSNVReload);

			MyUtility.CheckedCheckBox(false, checkBoxXoaVao, checkBoxXoaRaa);
		}

		private void RemoveDataRowHaveDSNVReload(ref DataTable table, List<cUserInfo> DSNVReload) {
			List<DataRow> rowMustRemove = (from DataRow row in table.Rows
										   let nhanvien = (cUserInfo)row["cUserInfo"]
										   where nhanvien != null && DSNVReload.Exists(item => item.MaCC == nhanvien.MaCC)
										   select row).ToList();
			foreach (DataRow row in rowMustRemove) {
				table.Rows.Remove(row);
			}
		}

		private void RemoveDSNVCanReload(ref List<cUserInfo> DSNVReload) {
			for (int i = 0; i < DSNVReload.Count; i++) {
				cUserInfo nhanvien = DSNVReload[i];
				this.m_DSNV.Remove(nhanvien);
			}
		}

		private void ThucHienThemGioXuongCSDL(ref List<cUserInfo> DSNVReload, ref List<DataRow> listSelectedRow,
			bool checkBoSungVao, bool checkBoSungRaa, TimeSpan gioVaoMoi, TimeSpan gioRaaMoi) {
			foreach (DataRow dataRow in listSelectedRow) {
				cCheckInOut currentCIO = (cCheckInOut)dataRow["cCheckInOut"];
				cUserInfo currentNhanVien = (cUserInfo)dataRow["cUserInfo"];
				if (DSNVReload.Any(item => item.MaCC == currentNhanVien.MaCC) == false) DSNVReload.Add(currentNhanVien);

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

		private void ThucHienDaoGioCCXuongCSDL(ref List<cUserInfo> DSNVReload, ref List<DataRow> listSelectedRow) {
			foreach (DataRow dataRow in listSelectedRow) {
				cCheckInOut currentCIO = (cCheckInOut)dataRow["cCheckInOut"];
				cUserInfo currentNhanVien = (cUserInfo)dataRow["cUserInfo"];
				if (DSNVReload.Any(item => item.MaCC == currentNhanVien.MaCC) == false) DSNVReload.Add(currentNhanVien);

				int UEN = (int)dataRow["UserEnrollNumber"];
				if (currentCIO == null || currentNhanVien == null) continue;
				DateTime timeString;
				int machineNoOld;
				string sourceOld;
				MayCheck mayCheckMoi;
				if (currentCIO.HaveINOUT == -1) {
					timeString = currentCIO.Vao.Time;
					machineNoOld = currentCIO.Vao.MachineNo;
					sourceOld = currentCIO.Vao.Source;
					mayCheckMoi = MayCheck.O; // đảo vào thành ra
				}
				else if (currentCIO.HaveINOUT == -2) {
					timeString = currentCIO.Raa.Time;
					machineNoOld = currentCIO.Raa.MachineNo;
					sourceOld = currentCIO.Raa.Source;
					mayCheckMoi = MayCheck.I; // đảo ra thành vào
				}
				else continue;

				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName.CheckInOut_InvertKieuChamCong.ToString(),
					new SqlParameter("@UserEnrollNumber", UEN),
					new SqlParameter("@TimeStr", timeString),
					new SqlParameter("@MachineNoOld", machineNoOld),
					new SqlParameter("@SourceOld", sourceOld),
					new SqlParameter("@TimeDateNew", timeString.Date),
					new SqlParameter("@MachineNoNew", (int)mayCheckMoi),
					new SqlParameter("@SourceNew", "PC"));


			}
			//tbd thông báo lỗi các trường hợp không thể thêm giờ
		}

		private void ThucHienSuaGioXuongCSDL(ref List<cUserInfo> DSNVReload, ref List<DataRow> listSelectedRow, bool checkSuaVao, bool checkSuaRaa, TimeSpan gioVaoMoi, TimeSpan gioRaaMoi) {
			foreach (DataRow dataRow in listSelectedRow) {
				cCheckInOut currentCIO = (cCheckInOut)dataRow["cCheckInOut"];
				cUserInfo currentNhanVien = (cUserInfo)dataRow["cUserInfo"];
				if (DSNVReload.Any(item => item.MaCC == currentNhanVien.MaCC) == false) DSNVReload.Add(currentNhanVien);

				int UEN = (int)dataRow["UserEnrollNumber"];
				if (currentCIO == null || currentNhanVien == null) continue;

				if (checkSuaVao) {
					this.SuaGio(currentCIO, MayCheck.I, gioVaoMoi, UEN);
				}
				if (checkSuaRaa) {
					this.SuaGio(currentCIO, MayCheck.O, gioRaaMoi, UEN);
				}
			}
			//tbd thông báo lỗi các trường hợp không thể thêm giờ
		}

		private void ThucHienXoaGioXuongCSDL(ref List<cUserInfo> DSNVReload, ref List<DataRow> listSelectedRow, bool checkXoaVao, bool checkXoaRaa) {
			foreach (DataRow dataRow in listSelectedRow) {
				cCheckInOut currentCIO = (cCheckInOut)dataRow["cCheckInOut"];
				cUserInfo currentNhanVien = (cUserInfo)dataRow["cUserInfo"];
				if (DSNVReload.Any(item => item.MaCC == currentNhanVien.MaCC) == false) DSNVReload.Add(currentNhanVien);

				int UEN = (int)dataRow["UserEnrollNumber"];
				if (currentCIO == null || currentNhanVien == null) continue;

				if (checkXoaVao) {
					this.XoaGio(currentCIO, UEN);
				}
				if (checkXoaRaa) {
					this.XoaGio(currentCIO, UEN);
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
				DateTime ngayGioVaoMoi = CIO.Raa.Time.Date;//ngày 
				gioBoSung = new TimeSpan(gioBoSung.Hours, gioBoSung.Minutes, 0);
				ngayGioVaoMoi = (gioBoSung >= gioRa)
					? ngayGioVaoMoi.AddDays(-1d).Add(gioBoSung).Add(XL2._01giay) // giây = 1 --> hh:mm:01 vào ca sau phải sau ra ca trước
					: ngayGioVaoMoi.Add(gioBoSung).Add(XL2._01giay); // thêm 1 ngày nếu vào sau ra

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
				DateTime ngayGioRaaMoi = CIO.Vao.Time.Date;//ngày
				gioBoSung = new TimeSpan(gioBoSung.Hours, gioBoSung.Minutes, 0);
				ngayGioRaaMoi = (gioVao >= gioBoSung)
					? ngayGioRaaMoi.AddDays(1d).Add(gioBoSung) // giây = 0--> hh:mm:00 ko cần add thêm 1 giây
					: ngayGioRaaMoi.Add(gioBoSung); // thêm 1 ngày nếu ra trước vào

				#region

				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName.CheckInOut_Ins.ToString(),
															 new SqlParameter("@UserEnrollNumber", UEN),
															 new SqlParameter("@TimeDate", ngayGioRaaMoi.Date),
															 new SqlParameter("@TimeStr", ngayGioRaaMoi),
															 new SqlParameter("@OriginType", LoaiCheck.ToString()),
															 new SqlParameter("@MachineNo", (int)LoaiCheck),
															 new SqlParameter("@Source", "PC"));

				#endregion
			}
			else {
			}
		}

		private void SuaGio(cCheckInOut CIO, MayCheck LoaiCheck, TimeSpan gioMoi, int UEN) {
			if (CIO.HaveINOUT == -1 && LoaiCheck.ToString() == MayCheck.O.ToString()) return; // sửa giờ cùng loại, khác loại bỏ qua
			if (CIO.HaveINOUT == -2 && LoaiCheck.ToString() == MayCheck.I.ToString()) return;// sửa giờ cùng loại, khác loại bỏ qua

			DateTime timeStringOld;
			int machineNoOld;
			string sourceOld;
			if (CIO.HaveINOUT == -1) {
				timeStringOld = CIO.Vao.Time;
				machineNoOld = CIO.Vao.MachineNo;
				sourceOld = CIO.Vao.Source;
			}
			else if (CIO.HaveINOUT == -2) {
				timeStringOld = CIO.Raa.Time;
				machineNoOld = CIO.Raa.MachineNo;
				sourceOld = CIO.Raa.Source;
			}
			else return;

			DateTime ngayGioMoi = timeStringOld.Date.Add(gioMoi);
			if (CIO.HaveINOUT == -1) ngayGioMoi = ngayGioMoi.Add(XL2._01giay); // nếu sửa giờ vào thì add thêm 1 giây. 


			#region

			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName.CheckInOut_Update.ToString(),
				new SqlParameter("@UserEnrollNumber", UEN),
				new SqlParameter("@TimeStrOld", timeStringOld),
				new SqlParameter("@MachineNoOld", machineNoOld),
				new SqlParameter("@SourceOld", sourceOld),
				new SqlParameter("@OriginTypeNew", LoaiCheck.ToString()),
				new SqlParameter("@TimeDateNew", ngayGioMoi.Date),
				new SqlParameter("@TimeStrNew", ngayGioMoi),
				new SqlParameter("@MachineNoNew", (int)LoaiCheck),
				new SqlParameter("@SourceNew", "PC"));

			#endregion

		}

		private void XoaGio(cCheckInOut CIO, int UEN) {
			DateTime timeStringOld;
			int machineNoOld;
			if (CIO.HaveINOUT == -1) {
				timeStringOld = CIO.Vao.Time;
				machineNoOld = CIO.Vao.MachineNo;
			}
			else if (CIO.HaveINOUT == -2) {
				timeStringOld = CIO.Raa.Time;
				machineNoOld = CIO.Raa.MachineNo;
			}
			else return;

			#region

			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName.CheckInOut_Delete.ToString(),
				new SqlParameter("@UserEnrollNumber", UEN),
				new SqlParameter("@TimeStr", timeStringOld),
				new SqlParameter("@MachineNo", machineNoOld));

			#endregion
		}

		private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e) {
			if (e.SelectedControl != gridControl1) return;

			ToolTipControlInfo info = null;
			//Get the view at the current mouse position
			GridView view = gridControl1.GetViewAt(e.ControlMousePosition) as GridView;
			if (view == null) return;
			//Get the view's element information that resides at the current position
			GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
			//Display a hint for row indicator cells
			if (hi.HitTest == GridHitTest.RowCell) {
				//An object that uniquely identifies a row indicator cell
				DataRow dataRow = view.GetDataRow(hi.RowHandle);
				cNgayCong ngayCong = (cNgayCong)dataRow["cNgayCong"];
				if (ngayCong == null) return;
				string text = XL.TaoTooltip5(ngayCong);
				object o = hi.HitTest.ToString() + hi.RowHandle.ToString();
				//string text = "Row " + hi.RowHandle.ToString();
				info = new ToolTipControlInfo(o, text);
			}
			//Supply tooltip information if applicable, otherwise preserve default tooltip (if any)
			if (info != null)
				e.Info = info;

		}

		private void timeEditBoSungVao_Properties_ButtonClick(object sender, ButtonPressedEventArgs e) {
			/* 1. xác định CIO nào đang chọn để lấy thời gian vào hoặc ra
			 * 2. trường hợp đặc biệt thỏa các điều kiện : 
			 *    - đang chọn 1 row, chọn ca tự do, chế độ nhập bổ sung thì tự động thêm
			 */
			if (e.Button.Kind == ButtonPredefines.Search) {
				fmDSCa formDSCa = new fmDSCa();
				formDSCa.ShowDialog();
				if (formDSCa.m_YesNoCancel == YesNoCancel.Yes) {
					cCa selectedCa = formDSCa.selectedCa;
					TimeSpan timeVao, timeRaa;
					this.GetThoigianVaoraCa(selectedCa, out timeVao, out timeRaa);
					int[] selectingRowHandle = gridView1.GetSelectedRows();
					if ((TimeEdit)sender == timeEditBoSungVao || (TimeEdit)sender == timeEditBoSungRaa) {
						if (selectingRowHandle.Count() == 1 && selectedCa.ID < 0) {
							cCheckInOut selectingCheckInOut = (gridView1.GetDataRow(selectingRowHandle[0]) != null)
												? (cCheckInOut)(gridView1.GetDataRow(selectingRowHandle[0])["cCheckInOut"]) : null;
							if (selectingCheckInOut == null) goto point1;
							if (selectingCheckInOut.HaveINOUT == -1) {
								timeVao = TimeSpan.Zero;
								DateTime dateTimeVao = (selectingCheckInOut.Vao.Time.Add(selectedCa.WorkingTimeTS));
								timeRaa = dateTimeVao.TimeOfDay;
							}
							else if (selectingCheckInOut.HaveINOUT == -2) {
								timeRaa = TimeSpan.Zero;
								DateTime dateTimeRaa = (selectingCheckInOut.Raa.Time.Add(-selectedCa.WorkingTimeTS));
								timeVao = dateTimeRaa.TimeOfDay;
							}
						}
					point1:
						timeEditBoSungVao.Time = DateTime.Today.Date.Add(timeVao);
						timeEditBoSungRaa.Time = DateTime.Today.Date.Add(timeRaa);
					}
					else if ((TimeEdit)sender == timeEditSuaVao || (TimeEdit)sender == timeEditSuaRaa) {

						timeEditSuaVao.Time = DateTime.Today.Date.Add(timeVao);
						timeEditSuaRaa.Time = DateTime.Today.Date.Add(timeRaa);
					}
				}
			}
		}

		private void GetThoigianVaoraCa(cCa selectedCa, out TimeSpan timeVao, out TimeSpan timeRaa) {
			if (selectedCa.ID < 0) {//ca tự do
				timeVao = TimeSpan.Zero;
				timeRaa = TimeSpan.Zero;
			}
			else {//ca chuẩn
				cCa ca = selectedCa;
				timeVao = ca.TOD_Duty.Onn;
				timeRaa = ca.TOD_Duty.Off;
			}
		}


	}
}
