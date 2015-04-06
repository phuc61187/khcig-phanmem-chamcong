using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.DAL;
using ChamCong_v05.DTO;
using ChamCong_v05.Helper;
using ChamCong_v05.Properties;

namespace ChamCong_v05.UI.ChamCong {
	public partial class frm_XemCT_GioCC : Form {
		#region log tooltip và hàm ko quan trọng
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
			this.Close();
		}

		#endregion


		public bool IsReload;
		public DataRow selectedRow; // row đang chọn từ form xem công lưu giữ thông tin ngày công của nhân viên đang chọn
		public cUserInfo nhanvien_goc;// nhân viên đang chọn, để update lại thông tin các ds check , cio , ngày công sau khi thêm giờ
		public cNgayCong NgayCong_goc;// ngày công đang chọn, để update lại thông tin các danh sách và công, phụ cấp
		public List<cCa> dsCa; // danh sách các Ca thuộc lịch trình của nhân viên, hàm load sẽ thêm item "--" ở vị trí 0
		public DataTable m_Bang_ChiTiet;
		public DataTable TaoTable_ChiTiet() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "cUserInfo", "cNgayCong", "cCheckInOut", "cCheck", "TimeStrThu", "TimeStrNgay", "TimeStr", "Loai", "MachineNo", "Source", "ShiftCode", "IsEdited", "IsEnableEdit" },
				new[] { typeof(cUserInfo), typeof(cNgayCong), typeof(cCheckInOut), typeof(cCheck), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(string), typeof(int), typeof(string), typeof(string), typeof(bool), typeof(bool) }
				);
			return kq;
		}

		public frm_XemCT_GioCC() {
			InitializeComponent();

			//tạo table chi tiết để gán dataSource
			m_Bang_ChiTiet = TaoTable_ChiTiet();
			// ko cho autogenerate các cột
			dgrdGioKDQD.AutoGenerateColumns = false;
			dgrdGioKDQD.DataSource = m_Bang_ChiTiet;
		}

		private void frm_ThongTinThemGio_Load(object sender, EventArgs e) {

			// 1. lấy thông tin nhân viên, fill thông tin vào 2 tb mã và tên nv
			nhanvien_goc = selectedRow["cUserInfo"] as cUserInfo;
			NgayCong_goc = selectedRow["cNgayCong"] as cNgayCong;
			tbMaCC.Text = nhanvien_goc.MaNV;
			tbTenNV.Text = nhanvien_goc.TenNV;

			// 
			// .triển khai load datagrid, trong khi load datagrid cũng đồng thời triển khai load dataSource cho các combobox
			loadGrid();
		}

		public void loadGrid() {
			m_Bang_ChiTiet.Rows.Clear();
			//load datagrid từ dataTable
			// kiểm  tra nếu ngày trước bị edited hoặc ngày sau bị edited thì load các giờ của ngày hôm trước, ngày hôm sau
			if (NgayCong_goc.prev != null) {
				loadRow(NgayCong_goc.prev, false);
			}
			loadRow(NgayCong_goc, true);
			if (NgayCong_goc.next != null) {
				loadRow(NgayCong_goc.next, false);
			}

			

			#region kiểm tra ngày công gốc có CIO nào bị mất check ko? nếu có thì chọn mặc định check bị khuyết, ko thì chọn check vào đầu tiên của CIO đầu tiên

			var dataGridViewRows = (from DataGridViewRow dataGridViewRow in dgrdGioKDQD.Rows
			                        let rowView = (DataRowView) dataGridViewRow.DataBoundItem
			                        where ((cNgayCong) rowView["cNgayCong"]).Ngay == NgayCong_goc.Ngay
			                              && rowView["cCheck"] == DBNull.Value
			                        select dataGridViewRow).FirstOrDefault();
			if (dataGridViewRows == null)
			{
				dataGridViewRows = (from DataGridViewRow dataGridViewRow in dgrdGioKDQD.Rows
				                    let rowView = (DataRowView) dataGridViewRow.DataBoundItem
				                    where ((cNgayCong) rowView["cNgayCong"]).Ngay == NgayCong_goc.Ngay
				                    select dataGridViewRow).FirstOrDefault();
			}
			if (dataGridViewRows != null) dataGridViewRows.Selected = true;

			#endregion

			VeLaiCacGioCoThayDoi();
		}

		private void loadRow(cNgayCong ngayCong, bool isEnableEdit) {

			if (ngayCong.DSVaoRa.Count == 0) {
				var row1 = m_Bang_ChiTiet.NewRow();
				var row2 = m_Bang_ChiTiet.NewRow();
				row1["cNgayCong"] = row2["cNgayCong"] = ngayCong;
				row1["cUserInfo"] = row2["cUserInfo"] = nhanvien_goc;
				row1["TimeStrThu"] = row1["TimeStrNgay"] = ngayCong.Ngay.Date;
				row2["TimeStrThu"] = row2["TimeStrNgay"] = ngayCong.Ngay.Date;
				row1["Loai"] = "Vào";
				row2["Loai"] = "Ra";
				row1["IsEdited"] = false;
				row2["IsEdited"] = false;
				row1["IsEnableEdit"] = isEnableEdit;
				row2["IsEnableEdit"] = isEnableEdit;

				m_Bang_ChiTiet.Rows.Add(row1);
				m_Bang_ChiTiet.Rows.Add(row2);
				return;
			}

			foreach (var CIO in ngayCong.DSVaoRa) {
				var row1 = m_Bang_ChiTiet.NewRow();
				var row2 = m_Bang_ChiTiet.NewRow();
				row1["Loai"] = "Vào";
				row2["Loai"] = "Ra";
				row1["cNgayCong"] = row2["cNgayCong"] = ngayCong;
				row1["cUserInfo"] = row2["cUserInfo"] = nhanvien_goc;
				row1["TimeStrThu"] = row1["TimeStrNgay"] = ngayCong.Ngay.Date;
				row2["TimeStrThu"] = row2["TimeStrNgay"] = ngayCong.Ngay.Date;
				row1["cCheckInOut"] = row2["cCheckInOut"] = CIO;
				var temp = CIO.CIOCodeFull();
				if (CIO.HaveINOUT == -1) {
					row1["cCheck"] = CIO.Vao;
					row1["TimeStr"] = CIO.Vao.Time;
					row1["MachineNo"] = CIO.Vao.MachineNo;
					row1["Source"] = CIO.Vao.Source;
					row1["ShiftCode"] = temp;
				}
				else if (CIO.HaveINOUT == -2) {
					row2["cCheck"] = CIO.Raa;
					row2["TimeStr"] = CIO.Raa.Time;
					row2["MachineNo"] = CIO.Raa.MachineNo;
					row2["Source"] = CIO.Raa.Source;
					row2["ShiftCode"] = temp;
				}
				else {
					row1["cCheck"] = CIO.Vao;
					row2["cCheck"] = CIO.Raa;
					row1["TimeStr"] = CIO.Vao.Time;
					row2["TimeStr"] = CIO.Raa.Time;
					row1["MachineNo"] = CIO.Vao.MachineNo;
					row2["MachineNo"] = CIO.Raa.MachineNo;
					row1["Source"] = CIO.Vao.Source;
					row2["Source"] = CIO.Raa.Source;
					row1["ShiftCode"] = row2["ShiftCode"] = temp;
				}

				row1["IsEdited"] = false;//(CIO.IsEdited > 0);
				row2["IsEdited"] = false; //(CIO.IsEdited > 0);

				row1["IsEnableEdit"] = isEnableEdit;
				row2["IsEnableEdit"] = isEnableEdit;

				m_Bang_ChiTiet.Rows.Add(row1);
				m_Bang_ChiTiet.Rows.Add(row2);
			}
		}

		private void VeLaiCacGioCoThayDoi() {
			foreach (DataGridViewRow dataGridViewRow in dgrdGioKDQD.Rows) {
				var row = (dataGridViewRow.DataBoundItem as DataRowView).Row;
				if (row["IsEnableEdit"] == DBNull.Value || (bool)row["IsEnableEdit"] == false) {
					dataGridViewRow.DefaultCellStyle.BackColor = Color.LightGray;
				}
				if (row["IsEdited"] != DBNull.Value && (bool)row["IsEdited"]) {
					dataGridViewRow.DefaultCellStyle.BackColor = Color.LightGreen;
				}
			}
		}

		private void dgrdGioKDQD_SelectionChanged(object sender, EventArgs e) {
			// mỗi lần chọn 1 row thì load dữ liệu vào các group thêm xóa sửa
			if (dgrdGioKDQD.SelectedRows.Count == 0) { // trường hợp này xảy ra sau khi gán dataSource tức load lần đầu hay reload
				//reset từng  group

				chkGioVao.Checked = false;
				chkGioRaa.Checked = false;
				MyUtility.ClearControlText(tbCa_Them, tbGioCu_Suaa, tbCa_Suaa, tbGioCu_Xoaa, tbGioCuu_ChuyenDoi);
				tbCa_Them.Tag = null;
				tbCa_Suaa.Tag = null;
				//không row nào được chọn thì disable button, ko cho thực hiện thao tác
				MyUtility.EnableDisableControl(false, btnThem, btnXoaa, btnSuaa, btnChuyenDoi, btnChonCa_Them, btnChonCa_Suaa);
				return;
			}

			//chuyển từ dataGridViewRow sang DataRowView sang DataRow
			var row = (DataRowView)(dgrdGioKDQD.SelectedRows[0]).DataBoundItem;
			var CIO = (row["cCheckInOut"] != DBNull.Value) ? (cCheckInOut)row["cCheckInOut"] : null;

			//2. lấy dữ liệu và load group
			var ngayCong = (row["cNgayCong"] != DBNull.Value) ? (cNgayCong)row["cNgayCong"] : null;
			var chk = (row["cCheck"] != DBNull.Value) ? (cCheck)row["cCheck"] : null;

			LoadGroup_Them(ngayCong.Ngay, CIO);
			LoadGroup_Suaa(ngayCong, CIO, chk);
			LoadGroup_Xoaa(chk);
		}

		private void LoadGroup_Xoaa(cCheck check) {
			if (check == null) // check null thì reset group
			{
				MyUtility.ClearControlText(tbGioCuu_ChuyenDoi, tbGioCu_Xoaa, tbGhiChu_Xoaa);
				cbLyDo_Xoaa.SelectedIndex = 0;
				MyUtility.EnableDisableControl(false, btnChuyenDoi, btnXoaa);
			}
			else {// check ko null
				tbGioCu_Xoaa.Text = tbGioCuu_ChuyenDoi.Text = ((check.MachineNo % 2 == 1) ? "Vào" : "Ra") + " " + check.Time.ToString("H:mm ddd d/M", Application.CurrentCulture);
				cbLyDo_Xoaa.SelectedIndex = 0;
				tbGhiChu_Xoaa.Text = string.Empty;
				MyUtility.EnableDisableControl(true, btnChuyenDoi, btnXoaa);
			}
		}

		private void LoadGroup_Suaa(cNgayCong ngayCong, cCheckInOut cio, cCheck check) {
			if (check == null) {
				MyUtility.ClearControlText(tbGioCu_Suaa, tbCa_Suaa, tbGhiChu_Suaa);
				tbCa_Suaa.Tag = null;
				MyUtility.EnableDisableControl(false, btnChonCa_Suaa, btnSuaa);// ko tồn tại giờ cũ nên ko cho sửa, chuyển đổi
				cbLyDo_Suaa.SelectedIndex = 0;
			}
			else {
				// xem xét check là CIO_V hay CIO_A, CIO_A cho sửa, CIO_V ko cho sửa, trong CIO_A coi có phải là check đệm giữa ca ko
				tbGioCu_Suaa.Text = ((check.MachineNo % 2 == 1) ? "Vào" : "Ra") + " " + check.Time.ToString("H:mm ddd d/M");

				dtpGioMoi_Sua.Value = new DateTime(check.Time.Year, check.Time.Month, check.Time.Day, check.Time.Hour, check.Time.Minute, check.Time.Second);
				tbCa_Suaa.Tag = (cio == null || cio.HaveINOUT < 0) ? null : cio.ThuocCa;
				tbCa_Suaa.Text = (cio == null || cio.HaveINOUT < 0) ? string.Empty : cio.ThuocCa.Code;
				cbLyDo_Suaa.SelectedIndex = 0;
				tbGhiChu_Suaa.Text = string.Empty;

				//tồn tại giờ cũ nên cho phép sửa
				MyUtility.EnableDisableControl(true, btnChonCa_Suaa, btnSuaa);
			}
		}

		private void LoadGroup_Them(DateTime ngay, cCheckInOut CIO) {
			MyUtility.EnableDisableControl(true, btnChonCa_Them, btnThem);//vì thêm giờ ko cần xác định giờ cũ, chỉ insert data nên mặc định cho phép enable
			MyUtility.ClearControlText(tbCa_Them, tbGhichu_Them);
			tbCa_Them.Tag = (CIO == null || CIO.HaveINOUT < 0) ? null : CIO.ThuocCa;
			tbCa_Them.Text = (CIO == null || CIO.HaveINOUT < 0) ? string.Empty : CIO.ThuocCa.Code;
			chkGioVao.Checked = (CIO != null && CIO.HaveINOUT == -2);
			chkGioRaa.Checked = (CIO != null && CIO.HaveINOUT == -1);
			dtpVao_Them.Value = new DateTime(ngay.Year, ngay.Month, ngay.Day, 0, 0, 1);
			dtpRaa_Them.Value = new DateTime(ngay.Year, ngay.Month, ngay.Day, 0, 0, 1);
			cbLyDo_Them.SelectedIndex = 0;
		}


		private void btnThem_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			IsReload = true;

			#region lấy thông tin từ màn hình

			var giovao = dtpVao_Them.Value.Add(XL2._01giay);
			var gioraa = dtpRaa_Them.Value;
			var lydo = (cbLyDo_Them.SelectedItem != null) ? cbLyDo_Them.SelectedItem.ToString() : cbLyDo_Them.SelectedText;
			var ghichu = tbGhichu_Them.Text;
			var UserEnrollNumber = nhanvien_goc.MaCC;

			var themVao = chkGioVao.Checked;
			var themRaa = chkGioRaa.Checked;
			var currentRow = ((DataRowView)dgrdGioKDQD.SelectedRows[0].DataBoundItem);
			var currentCIO = (currentRow["cCheckInOut"] != DBNull.Value) ? (cCheckInOut)currentRow["cCheckInOut"] : null;
			#endregion
			// Xét mục đích trước
			var dungMucDich = false;
			if (currentCIO != null) {
				if (currentCIO.HaveINOUT == -1 && themRaa) {
					if (gioraa - currentCIO.Vao.Time < TimeSpan.Zero || gioraa - currentCIO.Vao.Time > XL2._21h45) dungMucDich = false;
					else dungMucDich = true;
				}
				if (currentCIO.HaveINOUT == -2 && themVao) {
					if (currentCIO.Raa.Time - giovao < TimeSpan.Zero || currentCIO.Raa.Time - giovao > XL2._21h45) dungMucDich = false;
					else dungMucDich = true;
				}
			}

			if (chkGioVao.Checked) {
				#region hỏi lại trước khi thêm
				if (dungMucDich) {
					if (MessageBox.Show(string.Format(Resources.Text_xacNhanThemGioDonGian_dungmucdich, "vào", giovao.ToString("H:mm dddd d/M"), nhanvien_goc.TenNV),
						Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
						return;
					}
				}
				else {
					if (MessageBox.Show(string.Format(Resources.Text_xacNhanThemGioDonGian_saimucdic, "vào", giovao.ToString("H:mm dddd d/M"), nhanvien_goc.TenNV),
						Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
						return;
					}
				}

				#endregion

				var checkinn = new cCheck { IsEdited = 1, Type = "I", MachineNo = 21, Source = "PC", MaCC = UserEnrollNumber, Time = giovao, PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false } };
				XL.ThemGioChoNV(nhanvien_goc.MaCC, checkinn, nhanvien_goc.DS_Check_A, lydo, ghichu);
			}

			if (chkGioRaa.Checked) {
				#region hỏi lại trước khi thêm

				if (dungMucDich) {
					if (MessageBox.Show(string.Format(Resources.Text_xacNhanThemGioDonGian_dungmucdich, "ra", gioraa.ToString("H:mm dddd d/M"), nhanvien_goc.TenNV),
						Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
						return;
					}
				}
				else {
					if (MessageBox.Show(string.Format(Resources.Text_xacNhanThemGioDonGian_saimucdic, "ra", gioraa.ToString("H:mm dddd d/M"), nhanvien_goc.TenNV),
						Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
						return;
					}
				}
				#endregion

				var checkout = new cCheck { IsEdited = 1, Type = "O", MachineNo = 22, Source = "PC", MaCC = UserEnrollNumber, Time = gioraa, PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false } };
				XL.ThemGioChoNV(nhanvien_goc.MaCC, checkout, nhanvien_goc.DS_Check_A, lydo, ghichu);
			}
			var ds_raa3_vao1 = new List<cCheck>();
			var DS_Check_KoHopLe = new List<cCheck>();

			XL.LoaiBoCheckKoHopLe1(nhanvien_goc.DS_Check_A, ref DS_Check_KoHopLe);
			XL.GhepCIO_A2(nhanvien_goc.DS_Check_A, out nhanvien_goc.DS_CIO_A);
			XL.XetCa_ListCIO_A3(nhanvien_goc.DS_CIO_A, nhanvien_goc.LichTrinhLV, ds_raa3_vao1, nhanvien_goc.DS_Check_A);
			if (DS_Check_KoHopLe.Count > 0) {
				DS_Check_KoHopLe.AddRange(DS_Check_KoHopLe);
			}
			XL.TronDS_CIO_A_V5(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_CIO_V, out nhanvien_goc.DSVaoRa);
			XL.PhanPhoi_DSVaoRa6(nhanvien_goc.DSVaoRa, nhanvien_goc.DSNgayCong);
			XL.PhanPhoi_DSVang7(nhanvien_goc.DSVang, nhanvien_goc.DSNgayCong);
			XL.TinhCong_ListNgayCong8(nhanvien_goc.DSNgayCong, nhanvien_goc.StartNT, nhanvien_goc.EndddNT);//ver 4.0.0.4
			XL.TinhPCTC_TrongListXNPCTC9(nhanvien_goc.DSXNPhuCap50, nhanvien_goc.DSNgayCong);
			XL.TinhPCDB_TrongListXNPCDB10(nhanvien_goc.DSXNPhuCapDB, nhanvien_goc.DSNgayCong);
			if (DS_Check_KoHopLe.Count > 0) DAO5.LoaiGioLienQuan(DS_Check_KoHopLe);
			if (ds_raa3_vao1.Count > 0) DAO5.ThemGio_ra3_vao1(ds_raa3_vao1);

			loadGrid();

		}


		private void btnXoaa_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			IsReload = true;

			#region lấy thông tin từ màn hình

			var row = (DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem);
			var check = (cCheck)row["cCheck"];
			var CIO = (cCheckInOut)row["cCheckInOut"];
			var lydo = (cbLyDo_Xoaa.SelectedItem != null) ? cbLyDo_Xoaa.SelectedItem.ToString() : cbLyDo_Xoaa.SelectedText;
			var ghichu = tbGhiChu_Xoaa.Text;

			#endregion

			#region thông báo ko cho sửa nếu đã xác nhận rồi

			if (CIO.DaXN) {
				MessageBox.Show(Resources.Text_GioDaXacnhanKhongTheThayDoi, Resources.Caption_ThongBao);
				return;
			}

			#endregion

			#region hỏi lại trước khi xoá

			if (MessageBox.Show((string.Format(Resources.Text_xacNhanXoaGioDonGian, check.Time.ToString("H:mm dddd d/M"))),
								Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}

			#endregion


			var ds_raa3_vao1 = new List<cCheck>();
			var DS_Check_KoHopLe = new List<cCheck>();

			// xóa khỏi ds nên ko cần sắp xếp lại
			if (XL.XoaGioChoNV(nhanvien_goc.MaCC, check, nhanvien_goc.DS_Check_A, lydo, ghichu)) {
				XL.LoaiBoCheckKoHopLe1(nhanvien_goc.DS_Check_A, ref DS_Check_KoHopLe);
				XL.GhepCIO_A2(nhanvien_goc.DS_Check_A, out nhanvien_goc.DS_CIO_A);
				XL.XetCa_ListCIO_A3(nhanvien_goc.DS_CIO_A, nhanvien_goc.LichTrinhLV, ds_raa3_vao1, nhanvien_goc.DS_Check_A);//nhanvien_goc.MacDinhTinhPC50, //[140615_4]
				XL.TronDS_CIO_A_V5(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_CIO_V, out nhanvien_goc.DSVaoRa);
				XL.PhanPhoi_DSVaoRa6(nhanvien_goc.DSVaoRa, nhanvien_goc.DSNgayCong);
				XL.PhanPhoi_DSVang7(nhanvien_goc.DSVang, nhanvien_goc.DSNgayCong);
				XL.TinhCong_ListNgayCong8(nhanvien_goc.DSNgayCong, nhanvien_goc.StartNT, nhanvien_goc.EndddNT);//ver 4.0.0.4
				XL.TinhPCTC_TrongListXNPCTC9(nhanvien_goc.DSXNPhuCap50, nhanvien_goc.DSNgayCong);
				XL.TinhPCDB_TrongListXNPCDB10(nhanvien_goc.DSXNPhuCapDB, nhanvien_goc.DSNgayCong);
			}
			if (DS_Check_KoHopLe.Count > 0) DAO5.LoaiGioLienQuan(DS_Check_KoHopLe);
			if (ds_raa3_vao1.Count > 0) DAO5.ThemGio_ra3_vao1(ds_raa3_vao1);

			// update lại table
			loadGrid();

		}

		private void btnSuaa_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			IsReload = true;

			#region lấy thông tin từ màn hình

			var row = ((DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem)).Row;
			var CIO = (cCheckInOut)row["cCheckInOut"];
			var checkold = (cCheck)row["cCheck"];
			var suagiovao = (checkold.MachineNo % 2 == 1);
			var gioMoi = dtpGioMoi_Sua.Value;
			var lydo = (cbLyDo_Suaa.SelectedItem != null) ? cbLyDo_Suaa.SelectedItem.ToString() : cbLyDo_Suaa.SelectedText;
			var ghichu = tbGhiChu_Suaa.Text;

			#endregion

			#region thông báo không cho sửa nếu là giờ đã xn

			if (CIO.DaXN) {
				MessageBox.Show(Resources.Text_GioDaXacnhanKhongTheThayDoi, Resources.Caption_ThongBao);
				return;
			}

			#endregion

			bool hoiLai = (gioMoi - checkold.Time).Duration() > XL2._04gio;

			#region hỏi lại trước khi sửa

			if (hoiLai == false && MessageBox.Show(string.Format(Resources.Text_xacNhanSuaGioDonGian_dungmucdich,
														suagiovao ? "vào" : "ra", checkold.Time.ToString("H:mm dddd d/M"), gioMoi.ToString("H:mm dddd d/M")),
														Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}
			if (hoiLai && MessageBox.Show(string.Format(Resources.Text_xacNhanSuaGioDonGian_saimucdich,
										suagiovao ? "vào" : "ra", checkold.Time.ToString("H:mm dddd d/M"), gioMoi.ToString("H:mm dddd d/M"), nhanvien_goc.TenNV),
										Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}


			#endregion

			var ds_raa3_vao1 = new List<cCheck>();
			var DS_Check_KoHopLe = new List<cCheck>();

			cCheck checknew = suagiovao
				? new cCheck { Type = "I", MaCC = nhanvien_goc.MaCC, IsEdited = 1, Time = gioMoi, Source = "PC", MachineNo = 21, PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = checkold.PhucHoi.Them, Xoaa = checkold.PhucHoi.Xoaa } }
				: new cCheck { Type = "O", MaCC = nhanvien_goc.MaCC, IsEdited = 1, Time = gioMoi, Source = "PC", MachineNo = 22, PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = checkold.PhucHoi.Them, Xoaa = checkold.PhucHoi.Xoaa } };

			XL.SuaGioChoNV(nhanvien_goc.MaCC, checkold, checknew, nhanvien_goc.DS_Check_A, lydo, ghichu);

			XL.LoaiBoCheckKoHopLe1(nhanvien_goc.DS_Check_A, ref DS_Check_KoHopLe);
			XL.GhepCIO_A2(nhanvien_goc.DS_Check_A, out nhanvien_goc.DS_CIO_A);
			XL.XetCa_ListCIO_A3(nhanvien_goc.DS_CIO_A, nhanvien_goc.LichTrinhLV, ds_raa3_vao1, nhanvien_goc.DS_Check_A);//nhanvien_goc.MacDinhTinhPC50, //[140615_4]
			XL.TronDS_CIO_A_V5(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_CIO_V, out  nhanvien_goc.DSVaoRa);
			XL.PhanPhoi_DSVaoRa6(nhanvien_goc.DSVaoRa, nhanvien_goc.DSNgayCong);
			XL.PhanPhoi_DSVang7(nhanvien_goc.DSVang, nhanvien_goc.DSNgayCong);
			XL.TinhCong_ListNgayCong8(nhanvien_goc.DSNgayCong, nhanvien_goc.StartNT, nhanvien_goc.EndddNT);//ver 4.0.0.4
			XL.TinhPCTC_TrongListXNPCTC9(nhanvien_goc.DSXNPhuCap50, nhanvien_goc.DSNgayCong);
			XL.TinhPCDB_TrongListXNPCDB10(nhanvien_goc.DSXNPhuCapDB, nhanvien_goc.DSNgayCong);
			if (DS_Check_KoHopLe.Count > 0) DAO5.LoaiGioLienQuan(DS_Check_KoHopLe);
			if (ds_raa3_vao1.Count > 0) DAO5.ThemGio_ra3_vao1(ds_raa3_vao1);


			loadGrid();

		}


		private void btnChuyenDoi_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			IsReload = true;
			var row = (DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem);
			var checkold = (cCheck)row["cCheck"];
			var CIO = (cCheckInOut)row["cCheckInOut"];
			var VAO_to_RAA = (checkold.MachineNo % 2 == 1);
			var giocuu = checkold.Time;
			var lydo = (cbLyDo_Suaa.SelectedItem != null) ? cbLyDo_Suaa.SelectedItem.ToString() : cbLyDo_Suaa.SelectedText;
			var ghichu = tbGhiChu_Suaa.Text;

			#region thông báo ko cho chuyển đõi nếu đã xn

			if (CIO.DaXN) {
				MessageBox.Show(Resources.Text_GioDaXacnhanKhongTheThayDoi, Resources.Caption_ThongBao);
				return;
			}

			#endregion

			#region hỏi lại trước khi sửa //tbd

			if (CIO.HaveINOUT < 0 && MessageBox.Show(string.Format(Resources.Text_xacNhanChuyenDoiGioDonGian,
				VAO_to_RAA ? "vào" : "ra", giocuu.ToString("H:mm dddd d/M"), VAO_to_RAA ? "ra" : "vào", giocuu.ToString("H:mm dddd d/M"), nhanvien_goc.TenNV),
								Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}

			if (CIO.HaveINOUT == 0 && MessageBox.Show(string.Format(Resources.Text_xacNhanChuyenDoiGioDonGian,
				VAO_to_RAA ? "vào" : "ra", giocuu.ToString("H:mm dddd d/M"), VAO_to_RAA ? "ra" : "vào", giocuu.ToString("H:mm dddd d/M"), nhanvien_goc.TenNV),
								Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}
			#endregion
			var ds_raa3_vao1 = new List<cCheck>();
			var DS_Check_KoHopLe = new List<cCheck>();

			cCheck checknew = !VAO_to_RAA
				? new cCheck { Type = "I", MaCC = nhanvien_goc.MaCC, IsEdited = 1, Time = giocuu, Source = "PC", MachineNo = 21, PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = checkold.PhucHoi.Them, Xoaa = checkold.PhucHoi.Xoaa } }
				: new cCheck { Type = "O", MaCC = nhanvien_goc.MaCC, IsEdited = 1, Time = giocuu, Source = "PC", MachineNo = 22, PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = checkold.PhucHoi.Them, Xoaa = checkold.PhucHoi.Xoaa } };
			XL.SuaGioChoNV(nhanvien_goc.MaCC, checkold, checknew, nhanvien_goc.DS_Check_A, lydo, ghichu);
			XL.LoaiBoCheckKoHopLe1(nhanvien_goc.DS_Check_A, ref DS_Check_KoHopLe);
			XL.GhepCIO_A2(nhanvien_goc.DS_Check_A, out nhanvien_goc.DS_CIO_A);
			XL.XetCa_ListCIO_A3(nhanvien_goc.DS_CIO_A, nhanvien_goc.LichTrinhLV, ds_raa3_vao1, nhanvien_goc.DS_Check_A);//nhanvien_goc.MacDinhTinhPC50, //[140615_4]
			XL.TronDS_CIO_A_V5(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_CIO_V, out  nhanvien_goc.DSVaoRa);
			XL.PhanPhoi_DSVaoRa6(nhanvien_goc.DSVaoRa, nhanvien_goc.DSNgayCong);
			XL.PhanPhoi_DSVang7(nhanvien_goc.DSVang, nhanvien_goc.DSNgayCong);
			XL.TinhCong_ListNgayCong8(nhanvien_goc.DSNgayCong, nhanvien_goc.StartNT, nhanvien_goc.EndddNT);//ver 4.0.0.4
			XL.TinhPCTC_TrongListXNPCTC9(nhanvien_goc.DSXNPhuCap50, nhanvien_goc.DSNgayCong);
			XL.TinhPCDB_TrongListXNPCDB10(nhanvien_goc.DSXNPhuCapDB, nhanvien_goc.DSNgayCong);
			if (DS_Check_KoHopLe.Count > 0) DAO5.LoaiGioLienQuan(DS_Check_KoHopLe);
			if (ds_raa3_vao1.Count > 0) DAO5.ThemGio_ra3_vao1(ds_raa3_vao1);

			loadGrid();


		}


		private void btnChonCa_Them_Click(object sender, EventArgs e) {
			// kiểm tra xem nếu tồn tại 1 ca nào đó rồi thì gửi ca đó đi, nếu chưa tồn tại ca ( chế độ hàng loạt, tbXNCa null thì gửi đi null
			var currShift = (cCa)tbCa_Them.Tag;
			frmDSCa frm = new frmDSCa {
				StartPosition = FormStartPosition.CenterParent,
				SelectedShift = currShift 
			};
			frm.ShowDialog();
			// sau khi showdialog và nhận được ca từ form xác nhận thì tiến hành fill và tính toán
			if (frm.SelectedShift == null) return;
			currShift = frm.SelectedShift;
			tbCa_Them.Tag = currShift;
			tbCa_Them.Text = (currShift != null) ? currShift.Code : string.Empty;
			DataRowView rowView = (DataRowView) dgrdGioKDQD.SelectedRows[0].DataBoundItem;
			var ngayCong = (cNgayCong)rowView["cNgayCong"];
			var CIO = (rowView["cCheckInOut"] == DBNull.Value) ? null : (cCheckInOut) rowView["cCheckInOut"];
			var ngay = ngayCong.Ngay;
			if (currShift == null || currShift.ID < int.MinValue + 100)//ver 4.0.0.4	// ko có ca hoặc ca tự do thì lấy mặc định 00:00
			{
				dtpVao_Them.Value = ngay;
				dtpRaa_Them.Value = ngay;
			}
			else
			{
				if (currShift.QuaDem && CIO != null && CIO.HaveINOUT == -2) // có vào ko có ra thì cứ add theo ngày công
				{
					ngay = ngay.AddDays(-1d);
				}
				DateTime vao = ngay.Add(currShift.Duty.Onn);
				DateTime raa = ngay.Add(currShift.Duty.Off);
				dtpVao_Them.Value = vao;
				dtpRaa_Them.Value = raa;
			}
		}

		private void btnChonCa_Suaa_Click(object sender, EventArgs e) {
			// kiểm tra xem nếu tồn tại 1 ca nào đó rồi thì gửi ca đó đi, nếu chưa tồn tại ca ( chế độ hàng loạt, tbXNCa null thì gửi đi null
			var currShift = (cCa)tbCa_Suaa.Tag;
			frmDSCa frm = new frmDSCa { 
				StartPosition = FormStartPosition.CenterParent,
				SelectedShift = currShift 
			};
			frm.ShowDialog();
			// sau khi showdialog và nhận được ca từ form xác nhận thì tiến hành fill và tính toán
			if (frm.SelectedShift == null) return;
			currShift = frm.SelectedShift;
			tbCa_Suaa.Tag = currShift;
			tbCa_Suaa.Text = (currShift != null) ? currShift.Code : string.Empty;
			DataRowView rowView = (DataRowView)dgrdGioKDQD.SelectedRows[0].DataBoundItem;
			var ngayCong = (cNgayCong)rowView["cNgayCong"];
			var CIO = (rowView["cCheckInOut"] == DBNull.Value) ? null : (cCheckInOut)rowView["cCheckInOut"];
			var check = (cCheck)rowView["cCheck"];
			var ngay = ngayCong.Ngay;
			//if (currShift == null || currShift.ID == int.MinValue) { 
			if (currShift == null || currShift.ID < int.MinValue +100) { //ver 4.0.0.4	// ko có ca hoặc ca tự do thì lấy mặc định 00:00
				dtpGioMoi_Sua.Value = ngay;
			}
			else {
				// nếu ko có check thì nút chọn ca đã bị disable nên nếu click được thì chắc chắn đang thuộc 1 check-> chắc chắn thuộc 1CIO
				dtpGioMoi_Sua.Value = (check.Type == "I") ? ngay.Add(currShift.Duty.Onn): ngay.Add(currShift.Duty.Off);
				if (check.Type == "O" && currShift.QuaDem && CIO != null && CIO.HaveINOUT == -2)
				{
					dtpGioMoi_Sua.Value = ngay.AddDays(-1d).Add(currShift.Duty.Off) ;
				}
			}

		}


	}
}
