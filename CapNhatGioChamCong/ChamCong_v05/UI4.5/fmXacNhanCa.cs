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
using ChamCong_v05.Properties;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ChamCong_v05.zMisc {
	public partial class fmXacNhanCa : Form {
		public List<cUserInfo> m_DSNV;
		public DateTime m_NgayBD;
		public DateTime m_NgayKT;
		public DataTable m_TableGioThieuChamCong;
		public bool m_DaChonCaKhac = false;

		public fmXacNhanCa() {
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
			kq.Columns.Add("TongGioLamViec", typeof(TimeSpan));
			kq.Columns.Add("TongGioCheck", typeof(TimeSpan));
			kq.Columns.Add("Cong", typeof(float));
			kq.Columns.Add("cCheckInOut", typeof(cCheckInOut));
			kq.Columns.Add("cNgayCong", typeof(cNgayCong));
			kq.Columns.Add("cUserInfo", typeof(cUserInfo));
			return kq;
		}

		public void XacDinhGioThieuChamCong(List<cUserInfo> listNhanVien) {
			if (listNhanVien == null) return;
			foreach (cUserInfo nhanvien in listNhanVien) {
				foreach (cNgayCong ngayCong in nhanvien.DSNgayCong.Where(item => item.Ngay >= m_NgayBD && item.Ngay <= m_NgayKT).ToList()) {
					foreach (cCheckInOut CIO in ngayCong.DSVaoRa.Where(item => item.HaveINOUT == 0)) {
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
			kq["TongGioLamViec"] = CIO.TG5.TongGioLamViec5;
			kq["TongGioCheck"] = CIO.TG5.GioThucTe5;
			kq["Cong"] = CIO.Cong5.ThucTe;
			kq["cCheckInOut"] = CIO;
			kq["cUserInfo"] = Nhanvien;
			kq["cNgayCong"] = NgayCong;

			return kq;
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


		private void btnChonCa_Properties_ButtonClick(object sender, ButtonPressedEventArgs e) {
			if (e.Button.Kind != ButtonPredefines.Search) return; // không phải bấm button chọn ca thì thoát

			/* thực hiện xử lý
			 1. xác định ca được
			 */
			fmDSCa formDSCa = new fmDSCa();
			formDSCa.ShowDialog();
			if (formDSCa.m_YesNoCancel == YesNoCancel.Yes) {
				cCa selectedCa = formDSCa.selectedCa;
				btnChonCa.Tag = selectedCa;
				btnChonCa.Text = selectedCa.Code;
				m_DaChonCaKhac = true;
			}


		}

		private void timeEditBoSungVao_Properties_ButtonClick(object sender, ButtonPressedEventArgs e) {

		}

		private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e) {
			tbXNGhiChu.Text += string.Format("{0} {1} [action {2}] [controlerRow {3}]", "\n\n", "selection change ",
											 e.Action.ToString(), e.ControllerRow.ToString());


			int[] arrayRowHandle = gridView1.GetSelectedRows();
			if (arrayRowHandle.Count() == 0) {
				btnChonCa.Tag = null;
				btnChonCa.Text = string.Empty;
				this.m_DaChonCaKhac = false;
				this.ResetDataOfControl();
			}
			else if (arrayRowHandle.Count() == 1) {
				if (this.m_DaChonCaKhac == false) // chưa có fill thông tin cũ
				{
					DataRow dataRow = gridView1.GetDataRow(arrayRowHandle[0]);
					cCheckInOut currentCIO = (cCheckInOut)dataRow["cCheckInOut"];//luôn luôn là CIO đủ vào ra
					btnChonCa.Tag = currentCIO.ThuocCa;
					btnChonCa.Text = currentCIO.ThuocCa.Code;
					lbGioLV.Tag = currentCIO.TG5.TongGioLamViec5;
					lbGioLV.Text = currentCIO.TG5.TongGioLamViec5.ToString(@"h\:mm");
					lbGioCheckVT.Tag = currentCIO.TG5.GioThucTe5;
					lbGioCheckVT.Text = currentCIO.TG5.GioThucTe5.ToString(@"h\:mm");
					lbVaoTre.Tag = currentCIO.TG5.VaoTre;
					lbVaoTre.Text = currentCIO.TG5.VaoTre.ToString(@"h\:mm");
					lbRaaSom.Tag = currentCIO.TG5.RaaSom;
					lbRaaSom.Text = currentCIO.TG5.RaaSom.ToString(@"h\:mm");
					checkChoPhepTre.Checked = currentCIO.DuyetChoPhepVaoTre;
					checkChoPhepSom.Checked = currentCIO.DuyetChoPhepRaSom;
					checkVaoTreTinhCV.Checked = currentCIO.VaoTreTinhCV;
					checkRaaSomTinhCV.Checked = currentCIO.RaaSomTinhCV;
					lbOLaiChuaXN.Tag = currentCIO.TG5.OLai;
					lbOLaiChuaXN.Text = currentCIO.TG5.OLai.ToString(@"h\:mm");
					if (currentCIO.DaXN && currentCIO.TG5.SoPhutLamThem5 > TimeSpan.Zero) {
						checkXNLamThem.Checked = true;
						timeEditXacNhanOT.Tag = currentCIO.TG5.OLai;// giữ object số phút ở lại như là max thời gian làm thêm
						timeEditXacNhanOT.Time = DateTime.Today.Date.Add(currentCIO.TG5.SoPhutLamThem5); //hiển thị OTMin nếu đã xác nhận, chưa xác nhận thì lấy max ở lại
					}
					else {
						checkXNLamThem.Checked = false;
						timeEditXacNhanOT.Tag = currentCIO.TG5.OLai;// giữ object số phút ở lại như là max thời gian làm thêm
						timeEditXacNhanOT.Time = DateTime.Today.Date.Add(currentCIO.TG5.OLai); //hiển thị OTMin nếu đã xác nhận, chưa xác nhận thì lấy max ở lại
					}
					tbThongTinKhac.Clear(); //tbd ghi thông khác như công trong ngày.....
				}
				else {// đã có dùng thông tin ca mới để tính toán
					dynamic CaDuocChon = btnChonCa.Tag;
					float congCaQuyDinh, congTre, congSom, congThucTeTrongCa, congThucTeNgoaiCa, congThucTe, tongCongBu, tongCongTru, dinhMucCong;
					TimeSpan soPhutLamThemDaXN = (checkXNLamThem.Checked) ? timeEditXacNhanOT.Time.TimeOfDay : TimeSpan.Zero;
					XL.TinhCong_1_CIO_5(CaDuocChon.Workingday, CaDuocChon.WorkingTime, XL2.GioiHanChoPhepTreSom.Onn, XL2.GioiHanChoPhepTreSom.Off,
						checkVaoTreTinhCV.Checked, checkRaaSomTinhCV.Checked, soPhutLamThemDaXN, out congCaQuyDinh, out congTre, out congSom,
						out congThucTeTrongCa, out congThucTeNgoaiCa, out congThucTe, out tongCongBu, out tongCongTru, out dinhMucCong);
				}
			}
			else {// chọn multirow, khó tính toán tại chỗ, nên reset hết data và cho phép xem trước trước khi xác nhận
				ResetDataOfControl();
				if (m_DaChonCaKhac) {
					return;
				}
				else {
					btnChonCa.Tag = null;
					btnChonCa.Text = string.Empty;
				}
			}

		}
		private void ResetDataOfControl() {
			//chú ý ko xóa tag của button Chọn ca
			lbGioLV.Tag = null;
			lbGioCheckVT.Tag = null;
			lbVaoTre.Tag = null;
			lbRaaSom.Tag = null;
			lbOLaiChuaXN.Tag = null;
			lbCurrentCIO.Tag = null;// ẩn ko hiển thị text
			lbCurrentNgayCong.Tag = null; // ẩn ko hiển thị text
			timeEditXacNhanOT.Tag = null;

			MyUtility.CheckedCheckBox(false, checkChoPhepTre, checkChoPhepSom, checkVaoTreTinhCV, checkRaaSomTinhCV, checkXNLamThem);
			MyUtility.ClearControlText(lbGioLV, lbGioCheckVT, lbVaoTre, lbRaaSom, lbOLaiChuaXN, tbThongTinKhac, cbXNLyDo, tbXNGhiChu);

			timeEditXacNhanOT.Time = DateTime.Today.Date; //reset về timezero 0:00
		}

		private void button1_Click(object sender, EventArgs e) {
			int[] arrayRowHandle = gridView1.GetSelectedRows();

			if (arrayRowHandle.Count() == 0) return;
			else if (arrayRowHandle.Count() == 1) { 				
				fmPreviewXNCa frm = new fmPreviewXNCa();
				frm.m_Ca = (cCa)btnChonCa.Tag;
				frm.m_CheckVaoTreTinhCV = checkVaoTreTinhCV.Checked;
				frm.m_CheckRaaSomTinhCV = checkRaaSomTinhCV.Checked;
				frm.m_ChoPhepVaoTre = checkChoPhepTre.Checked;
				frm.m_ChoPhepRaaSom = checkChoPhepSom.Checked;
				frm.m_SoPhutLamThem = timeEditXacNhanOT.Time.TimeOfDay;
				int kq = frm.ValidateCIO(gridView1.GetDataRow(arrayRowHandle[0]), frm.m_Ca, frm.m_CheckVaoTreTinhCV, frm.m_CheckRaaSomTinhCV, frm.m_ChoPhepVaoTre, frm.m_ChoPhepRaaSom, frm.m_SoPhutLamThem);
				MessageBox.Show(kq.ToString());
			}
			else
			{

				fmPreviewXNCa frm = new fmPreviewXNCa();
				frm.m_Ca = (cCa)btnChonCa.Tag;
				frm.m_CheckVaoTreTinhCV = checkVaoTreTinhCV.Checked;
				frm.m_CheckRaaSomTinhCV = checkRaaSomTinhCV.Checked;
				frm.m_ChoPhepVaoTre = checkChoPhepTre.Checked;
				frm.m_ChoPhepRaaSom = checkChoPhepSom.Checked;
				frm.m_SoPhutLamThem = timeEditXacNhanOT.Time.TimeOfDay;
				//frm.ValidateCIO()
			}
		}





	}
}
