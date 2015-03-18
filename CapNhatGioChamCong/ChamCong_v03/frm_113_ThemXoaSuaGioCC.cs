using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;
using ChamCong_v03.Properties;

namespace ChamCong_v03 {
	public partial class frm_113_ThemXoaSuaGioCC : Form {
		public bool IsReload;
		public DataRow selectedRow; // row đang chọn từ form xem công lưu giữ thông tin ngày công của nhân viên đang chọn
		public cUserInfo nhanvien_goc;// nhân viên đang chọn, để update lại thông tin các ds check , cio , ngày công sau khi thêm giờ
		public cNgayCong NgayCong_goc;// ngày công đang chọn, để update lại thông tin các danh sách và công, phụ cấp
		public List<cCaAbs> dsCa; // danh sách các Ca thuộc lịch trình của nhân viên, hàm load sẽ thêm item "--" ở vị trí 0
		public DataTable m_Bang_ChiTiet;
		public DataTable TaoTable_ChiTiet() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "cUserInfo", "cNgayCong", "cChkInOut", "cChk", "TimeStrThu", "TimeStrNgay", "TimeStr", "Loai", "MachineNo", "Source", "ShiftCode", "IsEdited", "IsEnableEdit" },
				new[] { typeof(cUserInfo), typeof(cNgayCong), typeof(cChkInOut), typeof(cChk), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(string), typeof(int), typeof(string), typeof(string), typeof(bool), typeof(bool) }
				);
			return kq;
		}

		public frm_113_ThemXoaSuaGioCC() {
			InitializeComponent();

			//tạo table chi tiết để gán dataSource
			m_Bang_ChiTiet = TaoTable_ChiTiet();
			// ko cho autogenerate các cột
			dgrdGioKDQD.AutoGenerateColumns = false;
			dgrdGioKDQD.DataSource = m_Bang_ChiTiet;
			// gán trước các datamember, value member cho các comboBox
			cbCa_Them.ValueMember = cbCa_Suaa.ValueMember = "ID";
			cbCa_Them.DisplayMember = cbCa_Suaa.DisplayMember = "Code";
		}

		private void frm_ThongTinThemGio_Load(object sender, EventArgs e) {

			// 1. lấy thông tin nhân viên, fill thông tin vào 2 tb mã và tên nv
			nhanvien_goc = selectedRow["cUserInfo"] as cUserInfo;
			NgayCong_goc = selectedRow["cNgayCong"] as cNgayCong;
			tbMaCC.Text = nhanvien_goc.MaNV;
			tbTenNV.Text = nhanvien_goc.TenNV;

			//tạo dsCa gốc cho toàn form, combobox nào muốn sử dụng lại thì tạo mới từ cái gốc này và thay đổi tùy ý để giữ cái này làm gốc ko bị thay đổi
			dsCa = new List<cCaAbs>(nhanvien_goc.LichTrinhLV.DSCa); ;
			dsCa.Insert(0, new cCaChuan { ID = -1, Code = "--" });
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
				row1["IsEdited"] = (ngayCong.IsEdited > 0);
				row2["IsEdited"] = (ngayCong.IsEdited > 0);
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
				var temp = CIO.CIOCodeFull();
				if (CIO.HaveINOUT == -1) {
					row1["cChk"] = CIO.Vao;
					row1["TimeStr"] = CIO.Vao.Time;
					row1["MachineNo"] = CIO.Vao.MachineNo;
					row1["Source"] = CIO.Vao.Source;
					row1["ShiftCode"] = temp;
				}
				else if (CIO.HaveINOUT == -2) {
					row2["cChk"] = CIO.Raa;
					row2["TimeStr"] = CIO.Raa.Time;
					row2["MachineNo"] = CIO.Raa.MachineNo;
					row2["Source"] = CIO.Raa.Source;
					row2["ShiftCode"] = temp;
				}
				else {
					row1["cChk"] = CIO.Vao;
					row2["cChk"] = CIO.Raa;
					row1["TimeStr"] = CIO.Vao.Time;
					row2["TimeStr"] = CIO.Raa.Time;
					row1["MachineNo"] = CIO.Vao.MachineNo;
					row2["MachineNo"] = CIO.Raa.MachineNo;
					row1["Source"] = CIO.Vao.Source;
					row2["Source"] = CIO.Raa.Source;
					row1["ShiftCode"] = row2["ShiftCode"] = temp;
				}

				row1["IsEdited"] = (CIO.IsEdited > 0);
				row2["IsEdited"] = (CIO.IsEdited > 0);

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
				tbGioCu_Suaa.Text = tbGioCu_Xoaa.Text = string.Empty;

				//không row nào được chọn thì disable button, ko cho thực hiện thao tác
				BUS.MyUtility.EnableDisableControl(false, new Control[] { btnThem, btnXoaa, btnSuaa, btnChuyenDoi });
				return;
			}
			//chuyển từ dataGridViewRow sang DataRowView sang DataRow
			var row = ((DataRowView)(dgrdGioKDQD.SelectedRows[0]).DataBoundItem).Row;

			// lấy dữ liệu từ row đang chọn: 1. nếu đang chọn ngày trước hoặc sau thì disable các button và thoát; 2. lấy dữ liệu và load group
			var IsEnableEdit = (row["IsEnableEdit"] != DBNull.Value) ? (bool)row["IsEnableEdit"] : false;
			if (IsEnableEdit == false) {
				chkGioVao.Checked = false;
				chkGioRaa.Checked = false;
				tbGioCu_Suaa.Text = tbGioCu_Xoaa.Text = string.Empty;

				BUS.MyUtility.EnableDisableControl(false, new Control[] { btnThem, btnXoaa, btnSuaa, btnChuyenDoi });
				return;
			}

			//2. lấy dữ liệu và load group
			var ngayCong = (row["cNgayCong"] != DBNull.Value) ? (cNgayCong)row["cNgayCong"] : null;
			var chk = (row["cChk"] != DBNull.Value) ? (cChk)row["cChk"] : null;

			LoadGroup_Them(ngayCong.Ngay);
			LoadGroup_Suaa(ngayCong, chk);
			LoadGroup_Xoaa(chk);
		}

		private void LoadGroup_Xoaa(cChk check) {
			if (check == null) // check null thì reset group
			{
				tbGioCu_Xoaa.Text = string.Empty;
				cbLyDo_Xoaa.SelectedIndex = 0;
				tbGhiChu_Xoaa.Text = string.Empty;
				btnXoaa.Enabled = false;
			}
			else {
				var kieugio = (check.MachineNo % 2 == 1) ? "Vào" : "Ra";
				tbGioCu_Xoaa.Text = kieugio + " " + check.Time.ToString("H:mm ddd d/M", Application.CurrentCulture);
				btnXoaa.Enabled = true;
			}
		}

		private void LoadGroup_Suaa(cNgayCong ngayCong, cChk check) {
			if (check == null) {
				BUS.MyUtility.EnableDisableControl(false, new Control[] { btnSuaa, btnChuyenDoi, radVao, radRaa, dtpGioMoi_Sua });// ko tồn tại giờ cũ nên ko cho sửa, chuyển đổi
				BUS.MyUtility.ClearControlText(new Control[] { tbGioCu_Suaa, tbGhiChu_Suaa });
				cbCa_Suaa.DataSource = null;
			}
			else {
				// xem xét check là CIO_V hay CIO_A, CIO_A cho sửa, CIO_V ko cho sửa, trong CIO_A coi có phải là check đệm giữa ca ko
				tbGioCu_Suaa.Text = ((check.MachineNo % 2 == 1) ? "Vào" : "Ra") + " " + check.Time.ToString("H:mm ddd d/M");
				radVao.Checked = (check.MachineNo % 2 == 1);
				radRaa.Checked = (check.MachineNo % 2 == 0);

				var dataSource_DSCa = new List<cCaAbs>(dsCa);
				if (cbCa_Suaa.DataSource == null) {
					cbCa_Suaa.ValueMember = "ID";
					cbCa_Suaa.DisplayMember = "Code";
					cbCa_Suaa.DataSource = dataSource_DSCa;
				}

				dtpGioMoi_Sua.Value = new DateTime(ngayCong.Ngay.Year, ngayCong.Ngay.Month, ngayCong.Ngay.Day, 0, 0, 1);
				cbLyDo_Suaa.SelectedIndex = 0;
				tbGhiChu_Suaa.Text = string.Empty;

				//tồn tại giờ cũ nên cho phép sửa
				BUS.MyUtility.EnableDisableControl(true, new Control[] { btnSuaa, btnChuyenDoi, radVao, radRaa, dtpGioMoi_Sua });
			}
		}

		private void LoadGroup_Them(DateTime ngay) {

			var dataSource_DSCa = cbCa_Them.DataSource as List<cCaAbs>;
			if (dataSource_DSCa == null) dataSource_DSCa = new List<cCaAbs>(dsCa); // trường hợp này xảy ra khi loadgrid lần đầu, các lần sau dataSource ko còn bị null
			else dataSource_DSCa = dsCa;
			cbCa_Them.DataSource = dataSource_DSCa;

			cbCa_Them.SelectedIndex = 0;
			chkGioVao.Checked = false;
			chkGioRaa.Checked = false;
			dtpVao_Them.Value = new DateTime(ngay.Year, ngay.Month, ngay.Day, 0, 0, 1);
			dtpRaa_Them.Value = new DateTime(ngay.Year, ngay.Month, ngay.Day, 0, 0, 1);
			cbLyDo_Them.SelectedIndex = 0;
			tbGhichu_Them.Text = string.Empty;
			btnThem.Enabled = true;//vì thêm giờ ko cần xác định giờ cũ, chỉ insert data nên mặc định cho phép enable

		}


		private void cbCa_Them_SelectionChangeCommitted(object sender, EventArgs e) {

			//nếu dataSource null hoặc là index đầu tiên hoặc chưa có dòng nào được chọn thì ko làm gì cả
			if (cbCa_Them.DataSource == null || cbCa_Them.SelectedIndex == 0 || dgrdGioKDQD.SelectedRows.Count == 0) {
				return;
			}

			// tồn tại dataSource , xem item nào đang được chọn, nếu item đầu thì return như ở trên, các item khác thì fill
			var item = cbCa_Them.SelectedItem as cCaAbs;
			var row = ((DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem)).Row; // convert về datarow
			var ngayCong = (DateTime)row["TimeStrNgay"];
			// xác định được ca, ngày công, fill vào 2 datetimepicker
			dtpVao_Them.ValueChanged -= dtpVao_Them_ValueChanged;
			dtpRaa_Them.ValueChanged -= dtpRaa_Them_ValueChanged;
			dtpVao_Them.Value = ngayCong.Add(item.OnnTS);
			dtpRaa_Them.Value = ngayCong.Add(item.OffTS);
			dtpVao_Them.ValueChanged += dtpVao_Them_ValueChanged;
			dtpRaa_Them.ValueChanged += dtpRaa_Them_ValueChanged;

		}

		private void dtpVao_Them_ValueChanged(object sender, EventArgs e) {
			// nếu ko có dataSource hay chọn item đầu thì thoát
			if (cbCa_Them.DataSource == null || cbCa_Them.SelectedIndex == 0 || dgrdGioKDQD.SelectedRows.Count == 0) return;

			var row = ((DataRowView)dgrdGioKDQD.SelectedRows[0].DataBoundItem).Row;
			var ngayCong = (DateTime)row["TimeStrNgay"];
			var ca = cbCa_Them.SelectedItem as cCaAbs;
			var hieuvaoca = ngayCong.Add(ca.OnnInnTS);
			var hieuraaca = ngayCong.Add(ca.CutInnTS);
			var time = dtpVao_Them.Value;

			if (time < hieuvaoca || time > hieuraaca) {
				cbCa_Them.SelectionChangeCommitted -= cbCa_Them_SelectionChangeCommitted;
				cbCa_Them.SelectedIndex = 0;
				cbCa_Them.SelectionChangeCommitted += cbCa_Them_SelectionChangeCommitted;
			}

		}

		private void dtpRaa_Them_ValueChanged(object sender, EventArgs e) {
			// nếu ko có dataSource hay chọn item đầu thì thoát
			if (cbCa_Them.DataSource == null || cbCa_Them.SelectedIndex == 0 || dgrdGioKDQD.SelectedRows.Count == 0) return;

			var row = ((DataRowView)dgrdGioKDQD.SelectedRows[0].DataBoundItem).Row;
			var ngayCong = (DateTime)row["TimeStrNgay"];
			var ca = cbCa_Them.SelectedItem as cCaAbs;
			var hieuvaoca = ngayCong.Add(ca.OnnInnTS);
			var hieuraaca = ngayCong.Add(ca.CutInnTS);
			var time = dtpRaa_Them.Value;

			if (time < hieuvaoca || time > hieuraaca) {
				cbCa_Them.SelectionChangeCommitted -= cbCa_Them_SelectionChangeCommitted;
				cbCa_Them.SelectedIndex = 0;
				cbCa_Them.SelectionChangeCommitted += cbCa_Them_SelectionChangeCommitted;
			}

		}

		private void radVao_CheckedChanged(object sender, EventArgs e) {
			lbGioMoi.Text = "Giờ " + ((RadioButton)sender).Text + " mới";
			//lấy ngày công, ca
			if (cbCa_Suaa.DataSource == null || cbCa_Suaa.SelectedIndex == 0 || dgrdGioKDQD.SelectedRows.Count == 0) {
				return;
			}

			// tồn tại dataSource , xem item nào đang được chọn, nếu item đầu thì return như ở trên, các item khác thì fill
			var item = cbCa_Suaa.SelectedItem as cCaAbs;
			var row = ((DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem)).Row; // convert về datarow
			var ngayCong = (DateTime)row["TimeStrNgay"];

			if (radVao.Checked) {
				dtpGioMoi_Sua.ValueChanged -= dtpGioMoiSua_OnValueChanged;
				dtpGioMoi_Sua.Value = ngayCong.Add(item.OnnTS);
				dtpGioMoi_Sua.ValueChanged += dtpGioMoiSua_OnValueChanged;
			}
			else {
				dtpGioMoi_Sua.ValueChanged -= dtpGioMoiSua_OnValueChanged;
				dtpGioMoi_Sua.Value = ngayCong.Add(item.OffTS);
				dtpGioMoi_Sua.ValueChanged += dtpGioMoiSua_OnValueChanged;

			}
		}

		private void dtpGioMoiSua_OnValueChanged(object sender, EventArgs eventArgs) {
			//lấy ngày công, ca
			if (cbCa_Suaa.DataSource == null || cbCa_Suaa.SelectedIndex == 0 || dgrdGioKDQD.SelectedRows.Count == 0) {
				return;
			}

			// tồn tại dataSource , xem item nào đang được chọn, nếu item đầu thì return như ở trên, các item khác thì fill
			var item = cbCa_Suaa.SelectedItem as cCaAbs;
			var row = ((DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem)).Row; // convert về datarow
			var ngayCong = (DateTime)row["TimeStrNgay"];
			if (radVao.Checked) {
				if (dtpGioMoi_Sua.Value < ngayCong.Add(item.OnnInnTS) || dtpGioMoi_Sua.Value > ngayCong.Add(item.CutInnTS)) {
					cbCa_Suaa.SelectionChangeCommitted -= cbCaSuaa_OnSelectionChangeCommitted;
					cbCa_Suaa.SelectedIndex = 0;
					cbCa_Suaa.SelectionChangeCommitted += cbCaSuaa_OnSelectionChangeCommitted;
				}
			}
			else {
				if (dtpGioMoi_Sua.Value < ngayCong.Add(item.OnnOutTS) || dtpGioMoi_Sua.Value > ngayCong.Add(item.CutOutTS)) {
					cbCa_Suaa.SelectionChangeCommitted -= cbCaSuaa_OnSelectionChangeCommitted;
					cbCa_Suaa.SelectedIndex = 0;
					cbCa_Suaa.SelectionChangeCommitted += cbCaSuaa_OnSelectionChangeCommitted;
				}
			}
		}

		private void cbCaSuaa_OnSelectionChangeCommitted(object sender, EventArgs eventArgs) {
			//nếu dataSource null hoặc là index đầu tiên hoặc chưa có dòng nào được chọn thì ko làm gì cả
			if (cbCa_Suaa.DataSource == null || cbCa_Suaa.SelectedIndex == 0 || dgrdGioKDQD.SelectedRows.Count == 0) {
				return;
			}

			// tồn tại dataSource , xem item nào đang được chọn, nếu item đầu thì return như ở trên, các item khác thì fill
			var item = cbCa_Suaa.SelectedItem as cCaAbs;
			var row = ((DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem)).Row; // convert về datarow
			var ngayCong = (DateTime)row["TimeStrNgay"];
			// xác định được ca, ngày công, fill vào 2 datetimepicker

			if (radVao.Checked) {
				dtpGioMoi_Sua.ValueChanged -= dtpGioMoiSua_OnValueChanged;
				dtpGioMoi_Sua.Value = ngayCong.Add(item.OnnTS);
				dtpGioMoi_Sua.ValueChanged += dtpGioMoiSua_OnValueChanged;
			}
			else {
				dtpGioMoi_Sua.ValueChanged -= dtpGioMoiSua_OnValueChanged;
				dtpGioMoi_Sua.Value = ngayCong.Add(item.OffTS);
				dtpGioMoi_Sua.ValueChanged += dtpGioMoiSua_OnValueChanged;

			}
		}

		private void btnThem_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			IsReload = true;

			#region lấy thông tin từ màn hình

			var giovao = dtpVao_Them.Value.Add(XL2._01giay);
			var gioraa = dtpRaa_Them.Value;
			var lydo = (cbLyDo_Them.SelectedItem != null) ? cbLyDo_Them.SelectedItem.ToString() : cbLyDo_Them.SelectedText;
			var ghichu = tbGhichu_Them.Text;
			var UserEnrollNumber = nhanvien_goc.MaCC;

			#endregion

			if (chkGioVao.Checked) {
				#region hỏi lại trước khi thêm

				if (MessageBox.Show(string.Format(Resources.xacNhanThemGioVaoDonGian, giovao.ToString("H:mm dddd d/M"), nhanvien_goc.TenNV),
									Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
					return;
				}

				#endregion

				var checkinn = new cChkInn_A { IsEdited = 1, Type = "I", MachineNo = 21, Source = "PC", MaCC = UserEnrollNumber, Time = giovao, PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false } };
				XL.ThemGioChoNV(checkinn, nhanvien_goc, XL2.currUserID, lydo, ghichu);
			}

			if (chkGioRaa.Checked) {
				#region hỏi lại trước khi thêm

				if (MessageBox.Show(string.Format(Resources.xacNhanThemGioRaaDonGian, gioraa.ToString("H:mm dddd d/M"), nhanvien_goc.TenNV),
									Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
					return;
				}

				#endregion

				var checkout = new cChkOut_A { IsEdited = 1, Type = "O", MachineNo = 22, Source = "PC", MaCC = UserEnrollNumber, Time = gioraa, PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false } };
				XL.ThemGioChoNV(checkout, nhanvien_goc, XL2.currUserID, lydo, ghichu);
			}
			var ds_raa3_vao1 = new List<cChk>();
			var DS_Check_KoHopLe = new List<cChk>();

			XL.SapXepDS_Check(new[] { nhanvien_goc.DS_Check_A });
			XL.LoaiBoCheckCungLoaiTrong30phut(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_Check_KoHopLe);
			XL.GhepCIO_A(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CIO_A);
			XL.LoaiBoCIOKoHopLe(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_Check_A, nhanvien_goc.DS_Check_KoHopLe);
			if (nhanvien_goc.DS_Check_KoHopLe.Count > 0) {
				DS_Check_KoHopLe.AddRange(nhanvien_goc.DS_Check_KoHopLe);
				nhanvien_goc.DS_Check_KoHopLe.Clear();
			}
			XL.XetCa_CIO_A(nhanvien_goc.DS_CIO_A, nhanvien_goc.LichTrinhLV.DSCa, ds_raa3_vao1, nhanvien_goc.DS_Check_A);//nhanvien_goc.MacDinhTinhPC50, //[140615_4]
			XL.TronDS_CIO_A_V(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_CIO_V, nhanvien_goc.DSVaoRa);
			XL.TinhCongTheoNgay(nhanvien_goc.DSVaoRa, nhanvien_goc.NgayCongBD_Bef2D, nhanvien_goc.NgayCongKT_Aft2D, nhanvien_goc.DSVang, nhanvien_goc.DSNgayCong, nhanvien_goc.MacDinhTinhPC50);
			XL.TinhLaiPhuCapTC(nhanvien_goc.DSXNPhuCap50, nhanvien_goc.DSNgayCong);
			XL.TinhLaiPhuCapDB(nhanvien_goc.DSXNPhuCapDB, nhanvien_goc.DSNgayCong);
			NgayCong_goc = nhanvien_goc.DSNgayCong.Find(o => o.Ngay == NgayCong_goc.Ngay);
			if (DS_Check_KoHopLe.Count > 0) DAL.LoaiGioLienQuan(DS_Check_KoHopLe);
			if (ds_raa3_vao1.Count > 0) DAL.ThemGio_ra3_vao1(ds_raa3_vao1);

			m_Bang_ChiTiet.Rows.Clear();
			loadGrid();

		}


		private void btnXoaa_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			IsReload = true;

			#region lấy thông tin từ màn hình

			var row = ((DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem)).Row;
			var check = (cChk)row["cChk"];
			var lydo = (cbLyDo_Xoaa.SelectedItem != null) ? cbLyDo_Xoaa.SelectedItem.ToString() : cbLyDo_Xoaa.SelectedText;
			var ghichu = tbGhiChu_Xoaa.Text;

			#endregion

			#region thông báo ko cho sửa nếu đã xác nhận rồi

			if (check is cChkInn_V || check is cChkOut_V) {
				MessageBox.Show(Resources.GioDaXacnhanKhongTheThayDoi, Resources.capThongBao);
				return;
			}

			#endregion

			#region hỏi lại trước khi sửa

			if (MessageBox.Show((string.Format(Resources.xacNhanXoaGioDonGian, check.Time.ToString("H:mm dddd d/M"))),
								Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}

			#endregion


			var ds_raa3_vao1 = new List<cChk>();
			var DS_Check_KoHopLe = new List<cChk>();

			// xóa khỏi ds nên ko cần sắp xếp lại
			if (XL.XoaGioChoNV(check, nhanvien_goc, XL2.currUserID, lydo, ghichu)) {
				XL.LoaiBoCheckCungLoaiTrong30phut(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_Check_KoHopLe);
				XL.GhepCIO_A(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CIO_A);
				XL.LoaiBoCIOKoHopLe(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_Check_A, nhanvien_goc.DS_Check_KoHopLe);
				if (nhanvien_goc.DS_Check_KoHopLe.Count > 0) {
					DS_Check_KoHopLe.AddRange(nhanvien_goc.DS_Check_KoHopLe);
					nhanvien_goc.DS_Check_KoHopLe.Clear();
				}

				XL.XetCa_CIO_A(nhanvien_goc.DS_CIO_A, nhanvien_goc.LichTrinhLV.DSCa, ds_raa3_vao1, nhanvien_goc.DS_Check_A);//nhanvien_goc.MacDinhTinhPC50, //[140615_4]
				XL.TronDS_CIO_A_V(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_CIO_V, nhanvien_goc.DSVaoRa);
				XL.TinhCongTheoNgay(nhanvien_goc.DSVaoRa, nhanvien_goc.NgayCongBD_Bef2D, nhanvien_goc.NgayCongKT_Aft2D, nhanvien_goc.DSVang, nhanvien_goc.DSNgayCong, nhanvien_goc.MacDinhTinhPC50);
				XL.TinhLaiPhuCapTC(nhanvien_goc.DSXNPhuCap50, nhanvien_goc.DSNgayCong);
				XL.TinhLaiPhuCapDB(nhanvien_goc.DSXNPhuCapDB, nhanvien_goc.DSNgayCong);

				NgayCong_goc = nhanvien_goc.DSNgayCong.Find(o => o.Ngay == NgayCong_goc.Ngay);

			}
			if (DS_Check_KoHopLe.Count > 0) DAL.LoaiGioLienQuan(DS_Check_KoHopLe);
			if (ds_raa3_vao1.Count > 0) DAL.ThemGio_ra3_vao1(ds_raa3_vao1);

			// update lại table
			m_Bang_ChiTiet.Rows.Clear();
			loadGrid();

		}

		private void btnSuaa_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			IsReload = true;

			#region lấy thông tin từ màn hình

			var row = ((DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem)).Row;
			var checkold = (cChk)row["cChk"];
			var loaiMoi = radVao.Checked;
			var gioMoi = dtpGioMoi_Sua.Value;
			var lydo = (cbLyDo_Suaa.SelectedItem != null) ? cbLyDo_Suaa.SelectedItem.ToString() : cbLyDo_Suaa.SelectedText;
			var ghichu = tbGhiChu_Suaa.Text;

			#endregion

			#region thông báo không cho sửa nếu là giờ đã xn

			if (checkold is cChkInn_V || checkold is cChkOut_V) {
				MessageBox.Show(Resources.GioDaXacnhanKhongTheThayDoi, Resources.capThongBao);
				return;
			}

			#endregion

			#region hỏi lại trước khi sửa

			if (loaiMoi) {
				if (MessageBox.Show(string.Format(Resources.xacNhanSuaGioVaoDonGian, gioMoi.ToString("H:mm dddd d/M"), nhanvien_goc.TenNV),
									Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
					return;
				}
			}
			else {
				if (MessageBox.Show(string.Format(Resources.xacNhanSuaGioRaaDonGian, gioMoi.ToString("H:mm dddd d/M"), nhanvien_goc.TenNV),
									Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
					return;
				}
			}

			#endregion

			var ds_raa3_vao1 = new List<cChk>();
			var DS_Check_KoHopLe = new List<cChk>();

			cChk checknew;
			if (loaiMoi)
				checknew = new cChkInn_A { Type = "I", MaCC = nhanvien_goc.MaCC, IsEdited = 1, Time = gioMoi, Source = "PC", MachineNo = 21, PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = checkold.PhucHoi.Them, Xoaa = checkold.PhucHoi.Xoaa } }; // tbd trạng thái?? tạm thời lấy int.maxvalue tức sửa > 0
			else
				checknew = new cChkOut_A { Type = "O", MaCC = nhanvien_goc.MaCC, IsEdited = 1, Time = gioMoi, Source = "PC", MachineNo = 22, PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = checkold.PhucHoi.Them, Xoaa = checkold.PhucHoi.Xoaa } }; // tbd trạng thái?? tạm thời lấy int.maxvalue tức sửa > 0

			XL.SuaGioChoNV(checkold, checknew, nhanvien_goc, XL2.currUserID, lydo, ghichu);
			XL.SapXepDS_Check(new[] { nhanvien_goc.DS_Check_A });

			XL.LoaiBoCheckCungLoaiTrong30phut(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_Check_KoHopLe);
			XL.GhepCIO_A(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CIO_A);
			XL.LoaiBoCIOKoHopLe(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_Check_A, nhanvien_goc.DS_Check_KoHopLe);
			if (nhanvien_goc.DS_Check_KoHopLe.Count > 0) {
				DS_Check_KoHopLe.AddRange(nhanvien_goc.DS_Check_KoHopLe);
				nhanvien_goc.DS_Check_KoHopLe.Clear();
			}

			XL.XetCa_CIO_A(nhanvien_goc.DS_CIO_A, nhanvien_goc.LichTrinhLV.DSCa, ds_raa3_vao1, nhanvien_goc.DS_Check_A);//nhanvien_goc.MacDinhTinhPC50, //[140615_4]
			XL.TronDS_CIO_A_V(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_CIO_V, nhanvien_goc.DSVaoRa);
			XL.TinhCongTheoNgay(nhanvien_goc.DSVaoRa, nhanvien_goc.NgayCongBD_Bef2D, nhanvien_goc.NgayCongKT_Aft2D, nhanvien_goc.DSVang, nhanvien_goc.DSNgayCong, nhanvien_goc.MacDinhTinhPC50);
			XL.TinhLaiPhuCapTC(nhanvien_goc.DSXNPhuCap50, nhanvien_goc.DSNgayCong);
			XL.TinhLaiPhuCapDB(nhanvien_goc.DSXNPhuCapDB, nhanvien_goc.DSNgayCong);
			NgayCong_goc = nhanvien_goc.DSNgayCong.Find(o => o.Ngay == NgayCong_goc.Ngay);
			if (DS_Check_KoHopLe.Count > 0) DAL.LoaiGioLienQuan(DS_Check_KoHopLe);
			if (ds_raa3_vao1.Count > 0) DAL.ThemGio_ra3_vao1(ds_raa3_vao1);


			m_Bang_ChiTiet.Rows.Clear();
			loadGrid();

		}


		private void btnChuyenDoi_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			IsReload = true;
			var row = ((DataRowView)((dgrdGioKDQD.SelectedRows[0]).DataBoundItem)).Row;
			var checkold = (cChk)row["cChk"];
			var loaiCuu = (checkold.GetType() == typeof(cChkInn_A));
			var loaiMoi = !loaiCuu;
			var giocuu = checkold.Time;
			var lydo = (cbLyDo_Suaa.SelectedItem != null) ? cbLyDo_Suaa.SelectedItem.ToString() : cbLyDo_Suaa.SelectedText;
			var ghichu = tbGhiChu_Suaa.Text;

			#region thông báo ko cho chuyển đõi nếu đã xn

			if (checkold is cChkInn_V || checkold is cChkOut_V) {
				MessageBox.Show(Resources.GioDaXacnhanKhongTheThayDoi, Resources.capThongBao);
				return;
			}

			#endregion

			#region hỏi lại trước khi sửa

			if (loaiCuu) {
				if (MessageBox.Show(string.Format(Resources.xacNhanChuyenDoiGioDonGian, "vào", giocuu.ToString("H:mm dddd d/M"), "ra", nhanvien_goc.TenNV),
									Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
					return;
				}
			}
			else {
				if (MessageBox.Show(string.Format(Resources.xacNhanChuyenDoiGioDonGian, "ra", giocuu.ToString("H:mm dddd d/M"), "vào", nhanvien_goc.TenNV),
									Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
					return;
				}
			}

			#endregion
			var ds_raa3_vao1 = new List<cChk>();
			var DS_Check_KoHopLe = new List<cChk>();

			cChk checknew;
			if (loaiMoi)
				checknew = new cChkInn_A { Type = "I", MaCC = nhanvien_goc.MaCC, IsEdited = 1, Time = giocuu, Source = "PC", MachineNo = 21, PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = checkold.PhucHoi.Them, Xoaa = checkold.PhucHoi.Xoaa } };// tbd trạng thái?? tạm thời lấy int.maxvalue tức sửa > 0
			else
				checknew = new cChkOut_A { Type = "O", MaCC = nhanvien_goc.MaCC, IsEdited = 1, Time = giocuu, Source = "PC", MachineNo = 22, PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = checkold.PhucHoi.Them, Xoaa = checkold.PhucHoi.Xoaa } };// tbd trạng thái?? tạm thời lấy int.maxvalue tức sửa > 0
			XL.SuaGioChoNV(checkold, checknew, nhanvien_goc, XL2.currUserID, lydo, ghichu);
			XL.SapXepDS_Check(new[] { nhanvien_goc.DS_Check_A });
			XL.LoaiBoCheckCungLoaiTrong30phut(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_Check_KoHopLe);
			XL.GhepCIO_A(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CIO_A);
			XL.LoaiBoCIOKoHopLe(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_Check_A, nhanvien_goc.DS_Check_KoHopLe);
			if (nhanvien_goc.DS_Check_KoHopLe.Count > 0) {
				DS_Check_KoHopLe.AddRange(nhanvien_goc.DS_Check_KoHopLe);
				nhanvien_goc.DS_Check_KoHopLe.Clear();
			}

			XL.XetCa_CIO_A(nhanvien_goc.DS_CIO_A, nhanvien_goc.LichTrinhLV.DSCa, ds_raa3_vao1, nhanvien_goc.DS_Check_A);//nhanvien_goc.MacDinhTinhPC50, //[140615_4]
			XL.TronDS_CIO_A_V(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_CIO_V, nhanvien_goc.DSVaoRa);
			XL.TinhCongTheoNgay(nhanvien_goc.DSVaoRa, nhanvien_goc.NgayCongBD_Bef2D, nhanvien_goc.NgayCongKT_Aft2D, nhanvien_goc.DSVang, nhanvien_goc.DSNgayCong, nhanvien_goc.MacDinhTinhPC50);
			XL.TinhLaiPhuCapTC(nhanvien_goc.DSXNPhuCap50, nhanvien_goc.DSNgayCong);
			XL.TinhLaiPhuCapDB(nhanvien_goc.DSXNPhuCapDB, nhanvien_goc.DSNgayCong);
			NgayCong_goc = nhanvien_goc.DSNgayCong.Find(o => o.Ngay == NgayCong_goc.Ngay);
			if (DS_Check_KoHopLe.Count > 0) DAL.LoaiGioLienQuan(DS_Check_KoHopLe);
			if (ds_raa3_vao1.Count > 0) DAL.ThemGio_ra3_vao1(ds_raa3_vao1);

			m_Bang_ChiTiet.Rows.Clear();
			loadGrid();


		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}


	}
}
