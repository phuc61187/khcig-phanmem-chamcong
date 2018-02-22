using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DAL;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;
using log4net;
using log4net.Config;

namespace ChamCong_v04.UI.TinhLuong {
	public partial class frm3NhapThuChiThang : Form {
		#region log tooltip và hàm ko quan trọng
		public readonly ILog lg = LogManager.GetLogger("frm4NhapThuChiThang");

		public frm3NhapThuChiThang() {
			InitializeComponent();
			XmlConfigurator.Configure();

			dgrdDSNV.AutoGenerateColumns = dgrdDSThuChiThangTungNV.AutoGenerateColumns = false;
		}

		private void toolTipHint_Draw(object sender, DrawToolTipEventArgs e) {
			Font f = new Font("Arial", 10.0f);
			e.DrawBackground();
			e.DrawBorder();
			e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, new PointF(2f, 2f));
		}

		private void toolTipHint_Popup(object sender, PopupEventArgs e) {
			Size temp = TextRenderer.MeasureText(toolTipHint.GetToolTip(e.AssociatedControl), new Font("Arial", 10.0f));
			temp.Width += 4;
			temp.Height += 4;
			e.ToolTipSize = temp;
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			Close();
		}


		#endregion

		public DateTime m_thang;



		private void frm4NhapThuChiThang_Load(object sender, EventArgs e) {
			// load danh sách nhân viên
			var tableNV = XL.LayDSNV(true);
			var dataViewDSNV = new DataView(tableNV, "", "UserFullCode asc", DataViewRowState.CurrentRows);
			dgrdDSNV.DataSource = dataViewDSNV;

			//load danh sách thu chi tháng nv
			var tableDSThuChi = XL.LayDSThuchiThang(m_thang);
			var dataViewDSThuchiThang = new DataView(tableDSThuChi, "", "UserFullCode asc", DataViewRowState.CurrentRows);
			dgrdDSThuChiThangTungNV.DataSource = dataViewDSThuchiThang;

			#region set dataSource autocomplete

			var SourceNV = new AutoCompleteStringCollection();
			SourceNV.AddRange((from DataRow row in tableNV.Rows select row["UserFullName"].ToString().ToUpper()).ToArray());
			tbSearchNV.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tbSearchNV.AutoCompleteMode = AutoCompleteMode.Suggest;
			tbSearchNV.AutoCompleteCustomSource = SourceNV;

			#endregion
			reloadGridDSThuChi();
			/*
						var SourceDSThuchiThang = new AutoCompleteStringCollection();
						SourceDSThuchiThang.AddRange((from DataRow row in tableDSThuChi.Rows select row["UserFullName"].ToString().ToUpper()).ToArray());
						tbSearchThuchiThang.AutoCompleteSource = AutoCompleteSource.CustomSource;
						tbSearchThuchiThang.AutoCompleteMode = AutoCompleteMode.Suggest;
						tbSearchThuchiThang.AutoCompleteCustomSource = SourceDSThuchiThang;


						#region tính tổng các trường và thể hiện qua các label

						var distinctRows = (from DataRow row in tableDSThuChi.Rows select row).DistinctBy(item => item["UserFullCode"].ToString().TrimStart().TrimEnd().ToLower()).ToList();
						int tongNV = distinctRows.Count;
						double tongTamUng = (from DataRow row in distinctRows select (double)row["TamUng"]).Sum();
						double tongLuongDieuchinh = (from DataRow row in distinctRows select (double)row["LuongDieuChinh"]).Sum();
						double tongThuchikhac = (from DataRow row in distinctRows select (double)row["ThuChiKhac"]).Sum();
						float tongMucdongBHXH = (from DataRow row in distinctRows select (float)row["MucDongBHXH"]).Sum();

						lbTongNV.Text = string.Format("Tổng số NV: {0}", tongNV);
						lbTongTamung.Text = string.Format("Tổng tạm ứng: {0:###,###,###,##0.000}", tongTamUng);
						lbTongLuongdieuchinh.Text = string.Format("Tổng lương điều chỉnh: {0:###,###,###,##0.000}", tongLuongDieuchinh);
						lbTongThuchikhac.Text = string.Format("Tổng thu chi khác: {0:###,###,###,##0.000}", tongThuchikhac);
						lbTongMucDongBHXH.Text = string.Format("Tổng các mức đóng BHXH: {0:###,##0.00}", tongMucdongBHXH);

						#endregion
			*/


		}

		void reloadGridDSThuChi() {
			//  giữ filter cũ
			string oldFilter = string.Empty;
			var dataView = dgrdDSThuChiThangTungNV.DataSource as DataView;
			if (dataView != null) oldFilter = dataView.RowFilter;
			// lấy ds thu chi
			var tableDSThuChi = XL.LayDSThuchiThang(m_thang);

			//set autocomplete source là của datatable
			var SourceDSThuchiThang = new AutoCompleteStringCollection();
			SourceDSThuchiThang.AddRange((from DataRow row in tableDSThuChi.Rows select row["UserFullName"].ToString().ToUpper()).ToArray());
			tbSearchThuchiThang.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tbSearchThuchiThang.AutoCompleteMode = AutoCompleteMode.Suggest;
			tbSearchThuchiThang.AutoCompleteCustomSource = SourceDSThuchiThang;

			// filter table mới với old string filter, gán lại datasource 
			dataView = new DataView(tableDSThuChi) { RowFilter = oldFilter };
			dgrdDSThuChiThangTungNV.DataSource = dataView;

			TinhToanTong();
		}

		void TinhToanTong() {
			// tính toán các thông số với dataView hiện tại
			int tongNV = 0;
			double tongTamUng = 0d, tongThuchikhac = 0d, tongLuongDieuchinh = 0d;
			float tongMucDongBHXH = 0f;
			foreach (DataGridViewRow dataGridViewRow in dgrdDSThuChiThangTungNV.Rows) {
				DataRowView rowView = dataGridViewRow.DataBoundItem as DataRowView;
				tongNV++;
				tongTamUng += (double)rowView["TamUng"];
				tongThuchikhac += (double)rowView["ThuChiKhac"];
				tongMucDongBHXH += (float)rowView["MucDongBHXH"];
				tongLuongDieuchinh += (double)rowView["LuongDieuChinh"];
			}
			lbTongNV.Text = string.Format("Tổng số NV: {0}", tongNV);
			lbTongTamung.Text = string.Format("Tổng tạm ứng: {0:###,###,###,##0.000}", tongTamUng);
			lbTongLuongdieuchinh.Text = string.Format("Tổng lương điều chỉnh: {0:###,###,###,##0.000}", tongLuongDieuchinh);
			lbTongThuchikhac.Text = string.Format("Tổng thu chi khác: {0:###,###,###,##0.000}", tongThuchikhac);
			lbTongMucDongBHXH.Text = string.Format("Tổng các mức đóng BHXH: {0:###,##0.00}", tongMucDongBHXH);
		}

		private void btnDocTuExcel_Click(object sender, EventArgs e) {
			frmDocTuFileExcel2 frm = new frmDocTuFileExcel2 {StartPosition=FormStartPosition.CenterParent, m_Thang = m_thang };
			frm.ShowDialog();
			if (!frm.IsReload) return;// tương đương nếu có reload thì bấm btnXem

			reloadGridDSThuChi();

			/*
						var tableThuchiThang = XL.LayDSThuchiThang(m_thang);
						var dataViewDSThuchiThang = new DataView(tableThuchiThang, "", "UserFullCode asc", DataViewRowState.CurrentRows);
						var keyword = tbSearchThuchiThang.Text;
						var filter = "UserFullCode like '%" + keyword + "%' or UserFullName like '%" + keyword + "%'";
						dataViewDSThuchiThang.RowFilter = filter;
						dgrdDSThuChiThangTungNV.DataSource = dataViewDSThuchiThang;

						#region tính tổng các trường và thể hiện qua các label

						var distinctRows = (from DataRow row in tableThuchiThang.Rows select row).DistinctBy(item => item["UserFullCode"].ToString().TrimStart().TrimEnd().ToLower()).ToList();
						int tongNV = distinctRows.Count;
						double tongTamUng = (from DataRow row in distinctRows select (double)row["TamUng"]).Sum();
						double tongLuongDieuchinh = (from DataRow row in distinctRows select (double)row["LuongDieuChinh"]).Sum();
						double tongThuchikhac = (from DataRow row in distinctRows select (double)row["ThuChiKhac"]).Sum();
						float tongMucdongBHXH = (from DataRow row in distinctRows select (float)row["MucDongBHXH"]).Sum();

						lbTongNV.Text = string.Format("Tổng số NV: {0}", tongNV);
						lbTongTamung.Text = string.Format("Tổng tạm ứng: {0:###,###,###,##0.000}", tongTamUng);
						lbTongLuongdieuchinh.Text = string.Format("Tổng lương điều chỉnh: {0:###,###,###,##0.000}", tongLuongDieuchinh);
						lbTongThuchikhac.Text = string.Format("Tổng thu chi khác: {0:###,###,###,##0.000}", tongThuchikhac);
						lbTongMucDongBHXH.Text = string.Format("Tổng các mức đóng BHXH: {0:###,##0.00}", tongMucdongBHXH);

						#endregion

						#region set lại autocomplete cho ds thu chi

						var SourceDSThuchiThang = new AutoCompleteStringCollection();
						SourceDSThuchiThang.AddRange((from DataRow row in tableThuchiThang.Rows select row["UserFullName"].ToString().ToUpper()).ToArray());
						tbSearchThuchiThang.AutoCompleteSource = AutoCompleteSource.CustomSource;
						tbSearchThuchiThang.AutoCompleteMode = AutoCompleteMode.Suggest;
						tbSearchThuchiThang.AutoCompleteCustomSource = SourceDSThuchiThang;

						#endregion

			*/

		}

		private void dgrdDSNV_SelectionChanged(object sender, EventArgs e) {
			if (dgrdDSNV.SelectedRows.Count == 0) return;

			DataRowView selectedRow = (DataRowView)(dgrdDSNV.SelectedRows[0]).DataBoundItem;
			if (selectedRow == null) return;

			dgrdDSThuChiThangTungNV.ClearSelection();

			// chọn dsnv --> tức muốn thêm --> enable thêm; disable cập nhật xóa
			MyUtility.EnableDisableControl(false, btnCapNhat, btnXoa);
			MyUtility.EnableDisableControl(true, btnThem);

			tbTenNV.Text = selectedRow["UserFullName"].ToString();
			tbMaNV.Text = selectedRow["UserFullCode"].ToString();
			lbUserEnrollNumber.Tag = selectedRow["UserEnrollNumber"];
			lbUserEnrollNumber.Text = ((int)selectedRow["UserEnrollNumber"]).ToString();
			numTamUng.Value = 0;
			numThuchiKhac.Value = 0;
			numLuongdieuchinh.Value = 0;
			tbMucDongBHXH.Text = "1050";

		}

		private void dgrdDSThuChiThangTungNV_SelectionChanged(object sender, EventArgs e) {
			if (dgrdDSThuChiThangTungNV.SelectedRows.Count == 0) return;

			DataRowView selectedRow = (DataRowView)(dgrdDSThuChiThangTungNV.SelectedRows[0]).DataBoundItem;
			if (selectedRow == null) return;

			dgrdDSNV.ClearSelection();
			// chọn thu chi --> tức muốn cập nhật xóa --> enable cập nhật xóa; disable thêm
			MyUtility.EnableDisableControl(true, btnCapNhat, btnXoa);
			MyUtility.EnableDisableControl(false, btnThem);

			tbTenNV.Text = selectedRow["UserFullName"].ToString();
			tbMaNV.Text = selectedRow["UserFullCode"].ToString();
			lbUserEnrollNumber.Tag = selectedRow["UserEnrollNumber"];
			lbUserEnrollNumber.Text = ((int)selectedRow["UserEnrollNumber"]).ToString();
			numTamUng.Value = Convert.ToDecimal(selectedRow["TamUng"]);
			numLuongdieuchinh.Value = Convert.ToDecimal(selectedRow["LuongDieuChinh"]);
			numThuchiKhac.Value = Convert.ToDecimal(selectedRow["ThuChiKhac"]);
			tbMucDongBHXH.Text = ((float)selectedRow["MucDongBHXH"]).ToString("00.00");

		}

		#region thêm xoa sửa tạm ứng, thu chi...

		private void btnThem_Capnhat_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;
			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(m_thang.Date, MyUtility.LastDayOfMonth(m_thang))) {
				MessageBox.Show(string.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "chỉnh sửa thu chi tháng", "thực hiện thao tác", ""),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}

			#endregion


			#region lấy thông tin từ form

			int? macc = (int?)lbUserEnrollNumber.Tag;
			if (macc == null) {
				ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000);
				return;
			}
			double tamung = 0d, thuchiKhac = 0d, luongDieuchinh = 0d;
			float mucDongBHXH = 0f;
			if (float.TryParse(tbMucDongBHXH.Text, out mucDongBHXH) == false) {
				ACMessageBox.Show("Mức đóng BHXH chưa hợp lệ. Vui lòng nhập lại.", Resources.Caption_Loi, 2000);
				return;
			}

			tamung = Convert.ToDouble(numTamUng.Value);
			thuchiKhac = Convert.ToDouble(numThuchiKhac.Value);
			luongDieuchinh = Convert.ToDouble(numLuongdieuchinh.Value);

			#endregion

			// thực hiện query
			int kq = DAO.CapnhatThuchiThang((int)macc, m_thang, luongDieuchinh, tamung, thuchiKhac, mucDongBHXH);
			DAO.GhiNhatKyThaotac("Thêm khoản thu chi tháng", 
				string.Format("Thêm tạm ứng [{2}], lương điều chỉnh [{3}], mức đóng BHXH [{4}], thu chi khác [{5}] tháng [{0}]... cho 1 NV có mã chấm công [{1}]", 
				m_thang.ToString("dd/MM/yyyy"), macc, 
				tamung.ToString("###,###,###,###,###,##0"),
				luongDieuchinh.ToString("###,###,###,###,###,##0"), 
				mucDongBHXH.ToString("#0.00"),
				thuchiKhac.ToString("###,###,###,###,###,##0")), maCC:macc);

			if (kq == 0) {
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
			reloadGridDSThuChi();
		}

		private void btnXoa_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;
			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(m_thang.Date, MyUtility.LastDayOfMonth(m_thang))) {
				MessageBox.Show(string.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "chỉnh sửa thu chi tháng", "thực hiện thao tác", ""),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}

			#endregion

			#region lấy thông tin từ form

			int? macc = (int?)lbUserEnrollNumber.Tag;
			if (macc == null) {
				ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000);
				return;
			}

			#endregion

			// thực hiện query
			int kq = SqlDataAccessHelper.ExecNoneQueryString(
					" delete from DSThuChiThang where Thang=@Thang and UserEnrollNumber=@UserEnrollNumber ",
					new string[] { "@Thang", "@UserEnrollNumber" },
					new object[] { m_thang, macc });
			DAO.GhiNhatKyThaotac("Xoá khoản thu chi tháng", string.Format("Xoá tạm ứng, lương điều chỉnh, mức đóng BHXH, thu chi khác tháng [{0}]... của 1 NV có mã chấm công [{1}]", m_thang.ToString("dd/MM/yyyy"), macc), maCC:macc);
			if (kq == 0) {
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}

			reloadGridDSThuChi();

		}

		#endregion

		#region search nhân viên, thu chi...

		private void btnTimNV_Click(object sender, EventArgs e) {
			if (dgrdDSNV.DataSource == null) return;

			var searchStr1 = tbSearchNV.Text;
			var searchStr = string.Format("UserFullName like '%{0}%' or UserFullCode like '%{0}%'", searchStr1);
			var dataView = dgrdDSNV.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = searchStr;
		}

		private void linkHienthiTatcaNV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			var dataView = dgrdDSNV.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = string.Empty;

		}

		private void btnTimThuchiThang_Click(object sender, EventArgs e) {
			if (dgrdDSThuChiThangTungNV.DataSource == null) return;
			var view = dgrdDSThuChiThangTungNV.DataSource as DataView;
			var keyword = tbSearchThuchiThang.Text;
			var filter = "UserFullCode like '%" + keyword + "%' or UserFullName like '%" + keyword + "%'";
			if (view != null) view.RowFilter = filter;

			TinhToanTong();
		}

		private void linkHienThiTatCaThuchiThang_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			var view = dgrdDSThuChiThangTungNV.DataSource as DataView;
			if (view != null) view.RowFilter = string.Empty;

			TinhToanTong();
		}

		#endregion

		private void btnTiepTuc_Click(object sender, EventArgs e) {

			frm4LuuHSPC frm = new frm4LuuHSPC { m_Thang = m_thang };
			frm.WindowState = FormWindowState.Normal;
			frm.StartPosition = FormStartPosition.Manual;
			frm.MdiParent = this.MdiParent;
			//frm.Location = XL2.GetCenterLocation(frm.MdiParent.Size.Width, frm.MdiParent.Size.Height, frm.Size.Width, frm.Size.Height);
			frm.Location = new Point(0, 0);//XL2.GetCenterLocation(frm.MdiParent.ClientRectangle.Width, frm.MdiParent.ClientRectangle.Height, frm.Size.Width, frm.Size.Height);
			frm.Show();
			Close();


		}

		private void btnXoaTatcaDSThuchiThang_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;
			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(m_thang.Date, MyUtility.LastDayOfMonth(m_thang))) {
				MessageBox.Show(string.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "chỉnh sửa thu chi tháng", "thực hiện thao tác", ""),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}

			#endregion

			//xác nhận trước khi xóa
			string messContent = "Bạn muốn xóa tất cả danh sách thu chi tháng {0}?.";
			messContent = string.Format(messContent, m_thang.ToString("MM/yyyy"));
			if (MessageBox.Show(messContent, Resources.Caption_XacNhan) == DialogResult.No) return;

			// thực hiện query 
			int kq = SqlDataAccessHelper.ExecNoneQueryString("delete from DSThuChiThang where thang=@Thang ",new string[] { "@Thang" }, new object[] { m_thang });
			DAO.GhiNhatKyThaotac("Xoá tất cả khoản thu chi tháng", string.Format("Xoá tạm ứng, lương điều chỉnh, mức đóng BHXH, thu chi khác của TẤT CẢ NV tháng [{0}]", m_thang.ToString("dd/MM/yyyy")));

			if (kq == 0) MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);

			reloadGridDSThuChi();

		}

		private void btnQuayLai_Click(object sender, EventArgs e)
		{
			frm2QLLuongCongNhat frm = new frm2QLLuongCongNhat { m_thang = m_thang };
			frm.WindowState = FormWindowState.Normal;
			frm.StartPosition = FormStartPosition.Manual;
			frm.MdiParent = this.MdiParent;
			//frm.Location = XL2.GetCenterLocation(frm.MdiParent.Size.Width, frm.MdiParent.Size.Height, frm.Size.Width, frm.Size.Height);
			frm.Location = new Point(0, 0);//XL2.GetCenterLocation(frm.MdiParent.ClientRectangle.Width, frm.MdiParent.ClientRectangle.Height, frm.Size.Width, frm.Size.Height);
			frm.Show();
			Close();

		}

		private void tbSearchNV_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter)
				btnTimNV.PerformClick();

		}

		private void tbSearchThuchiThang_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter)
				btnTimThuChiThang.PerformClick();

		}



	}
}
