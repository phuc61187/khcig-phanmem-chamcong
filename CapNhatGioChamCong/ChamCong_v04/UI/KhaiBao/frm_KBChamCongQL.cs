using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DAL;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;
using log4net;

namespace ChamCong_v04.UI.KhaiBao {
	public partial class frm_KBChamCongQL : Form {
		#region log tooltip và hàm ko quan trọng
		public readonly ILog lg = LogManager.GetLogger("frm_KBChamCongQL");

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

		private void btnThoat_Click(object sender, EventArgs e)
		{
			Close();
		}

		#endregion

		public List<cUserInfo> m_DSNV = new List<cUserInfo>(); // danh sách các nhân viên thuộc phòng đang chọn
		public List<cPhongBan> m_DSPhg = new List<cPhongBan>();
		public List<int> m_listIDPhongBan;

		public List<cCa> m_DSCa;
		public DataTable m_Bang_DSNV;
		public DataTable TaoBang_DSNV() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "check", "cUserInfo", "UserEnrollNumber", "UserFullCode", "UserFullName", "SchID", "SchName", "ChucVu", "MaPhong", "TenPhong", "HeSoLuongCB", "HeSoLuongSP", "HSBHCongThem" },
				new[] { typeof(bool), typeof(cUserInfo), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(float), typeof(float), typeof(float) });
			return kq;
		}

		public DateTime m_currMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

		#region hàm ko quan trọng

		#region vẽ check box check all và xử lý sự kiện
		private readonly CheckBox checkAll_GridDSNV = new CheckBox();

		private void checkAll_CheckedChanged(object sender, EventArgs e) {
			//1. xác định datagrid nào đang check all
			DataGridView tempGrid = dgrdDSNVTrgPhg;

			// 2. kiểm tra nếu ko có dòng dữ liệu nào thì thoát
			if (tempGrid.Rows.Count == 0) return;

			//3. có dữ liệu, duyệt qua hết và gán giá trị check (or uncheck), sau đó endEDIT lại lưới để dòng đầu tiên có giá trị
			var tmpCheckAll = ((CheckBox)sender).Checked;

			tempGrid.BeginEdit(true);
			var dt = tempGrid.DataSource as DataView;

			if (dt != null && dt.Count != 0) {
				foreach (DataRowView row in dt) {
					row["check"] = tmpCheckAll;
				}
			}

			tempGrid.EndEdit();
			tempGrid.Update();
			tempGrid.RefreshEdit();
		}

		#endregion

		public frm_KBChamCongQL()
		{
			InitializeComponent();

			m_listIDPhongBan = new List<int>();
			m_DSNV = new List<cUserInfo>();
			m_Bang_DSNV = TaoBang_DSNV();
			dgrdDSNVTrgPhg.AutoGenerateColumns = false;
			DataView dataView = new DataView(m_Bang_DSNV);
			dgrdDSNVTrgPhg.DataSource = dataView;

			DateTime today = DateTime.Today;
			dtpThang.Value = new DateTime(today.Year, today.Month, today.Day);
			dtpBDLam.Value = new DateTime(today.Year, today.Month, today.Day);
			dtpKTLam.Value = new DateTime(today.Year, today.Month, today.Day);

			XL2.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));
		}

		#endregion

		#region search

		private void btnTim_Click(object sender, EventArgs e)
		{
			if (dgrdDSNVTrgPhg.DataSource == null) return;

			var searchStr1 = tbSearch.Text;
			var searchStr = string.Format("UserFullName like '%{0}%' or UserFullCode like '%{0}%'", searchStr1);
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = searchStr;
		}

		private void linkHienThiTatCaNV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = string.Empty;
		}

		#endregion

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {

			#region lấy id ds phòng ban và phòng ban con được chọn

			m_listIDPhongBan.Clear();
			if (e.Node.FirstNode != null) XL.GetIDNodeAndChildNode(e.Node, ref m_listIDPhongBan);
			else {
				var temp = ((cPhongBan)e.Node.Tag);
				if (temp.ChoPhep) m_listIDPhongBan.Add(temp.ID);
			}
			e.Node.Expand();

			#endregion

			#region kiểm tra kết nối csdl, mất kết nối thì thoát

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 2000);
				Close();
				return;
			}

			#endregion

			XL.KhoiTaoDSNV_ChamCong(m_DSNV, m_listIDPhongBan, m_DSPhg);

			XL.TaoTableDSNV(m_DSNV, m_Bang_DSNV);

			#region set datasource for autocomplete

			var Source = new AutoCompleteStringCollection();
			Source.AddRange((from nv in m_DSNV select nv.TenNV.ToUpperInvariant()).ToArray());
			tbSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tbSearch.AutoCompleteMode = AutoCompleteMode.Suggest;
			tbSearch.AutoCompleteCustomSource = Source;

			#endregion

			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = string.Empty;

			checkAll_GridDSNV.Checked = false;
		}

		private void frm_ChamCongTay_Load(object sender, EventArgs e) {
			#region kiểm tra kết nối csdl, mất kết nối thì thoát

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 2000);
				Close();
				return;
			}

			#endregion

			dtpBDLam.ValueChanged += dtp_ValueChanged;
			dtpKTLam.ValueChanged += dtp_ValueChanged;

			#region //2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan ,
			// trường hợp ko có phòng ban nào được phép thao tác thì báo và thoát form
			XL.KhoiTaoDSPhongBan(m_DSPhg, XL2.currUserID);
			if (m_DSPhg.Count == 0) {
				ACMessageBox.Show(Resources.Text_ChuaCapQuyenPhongBanThaoTac, Resources.Caption_ThongBao, 5000);
				Close();
				return;
			}
			XL.loadTreePhgBan(treePhongBan, XL2.TatcaPhongban, m_DSPhg);

			#endregion

			// đăng ký sự kiện cho tree và chọn topNode
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;
			treePhongBan.SelectedNode = treePhongBan.TopNode;

			#region chọn mặc định ca hành chánh cho cấp quản lý

			cCa defaultShift = XL.DSCa.Find(item => item.Duty.Onn == XL2._07h30 && item.Duty.Off == XL2._16gio && Math.Abs(item.Workingday - 1f) < 0.01f); //tbd
			tbCa.Tag = defaultShift;
			tbCa.Text = defaultShift.Code;
			dtpBDLam.Value = DateTime.Today.Date.Add(defaultShift.Duty.Onn);
			dtpKTLam.Value = DateTime.Today.Date.Add(defaultShift.Duty.Off);

			#endregion
		}

		private void dtpThang_ValueChanged(object sender, EventArgs e) {
			#region build danh sách ngày cho CheckList chọn ngày

			dtpThang.Update();
			m_currMonth = dtpThang.Value;
			var startDay = new DateTime(m_currMonth.Year, m_currMonth.Month, 1);
			var endddDay = new DateTime(m_currMonth.Year, m_currMonth.Month, DateTime.DaysInMonth(m_currMonth.Year, m_currMonth.Month));

			var indexDay = startDay;
			var lstNgay = new List<DateTime>();
			while (indexDay <= endddDay) {
				lstNgay.Add(indexDay);
				indexDay = indexDay.AddDays(1d);
			}
			checklistNgay.FormatString = "d - ddd";

			#endregion

			checklistNgay.DataSource = lstNgay;

			#region set các item về false hết

			chkAddSat.Checked = false;
			chkAddSun.Checked = false;
			chkAllWKDays.Checked = false;
			for (int i = 0; i < checklistNgay.Items.Count; i++) {
				checklistNgay.SetItemChecked(i, false);
			}

			#endregion

		}

		private void chkAllDays_CheckedChanged(object sender, EventArgs e) {
			bool checkT7 = chkAddSat.Checked;
			bool checkCN = chkAddSun.Checked;
			bool checkAll = chkAllWKDays.Checked;
			for (int i = 0; i < checklistNgay.Items.Count; i++) {
				var obj = checklistNgay.Items[i];
				DateTime ngay = (DateTime)obj;
				if (ngay.DayOfWeek != DayOfWeek.Saturday && ngay.DayOfWeek != DayOfWeek.Sunday) checklistNgay.SetItemChecked(i, checkAll);
				if (ngay.DayOfWeek == DayOfWeek.Saturday) checklistNgay.SetItemChecked(i, checkT7);
				if (ngay.DayOfWeek == DayOfWeek.Sunday) checklistNgay.SetItemChecked(i, checkCN);
			}
			checklistNgay.EndUpdate();
		}

		private void btnThucHien_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1),new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, DateTime.DaysInMonth(dtpThang.Value.Year, dtpThang.Value.Month)))) {
				MessageBox.Show(string.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "khai báo chấm công cho quản lý", "khai báo chấm công", ""),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}

			#endregion


			#region lấy và kiểm tra dsnv được chọn

			BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();
			//2. lấy danh sách nhân viên check, nếu chưa có nv nào check thì thông báo
			var listNV = (from DataGridViewRow dataGridViewRow in dgrdDSNVTrgPhg.Rows
						  let rowView = dataGridViewRow.DataBoundItem as DataRowView
						  where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
						  select ((cUserInfo)rowView["cUserInfo"]))
						  .ToArray();

			if (listNV.Length == 0) {
				ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000); //
				GC.Collect();
			}

			#endregion

			#region lấy ds các ngày đã check

			var DSNgayCheck = (from object item in checklistNgay.CheckedItems select (DateTime)item).ToList();
			if (DSNgayCheck.Count == 0) {
				ACMessageBox.Show("Bạn chưa chọn ngày làm việc.", "Thông báo", 2000);
				return;
			}

			#endregion

			#region lấy thông tin

			var ngay1 = DateTime.Today.Date;
			TimeSpan TimeSpanBD = dtpBDLam.Value.TimeOfDay;
			TimeSpan TimeSpanKT = dtpKTLam.Value.TimeOfDay;
			var timeBD = ngay1.Add(TimeSpanBD);
			var timeKT = ngay1.Add(TimeSpanKT);
			if (TimeSpanBD > TimeSpanKT) timeKT = timeKT.AddDays(1d);
			//var sophutOT = (checkXNLamThem.Checked && numSoPhutOT.Value > 0) ? (int)numSoPhutOT.Value : 0;
			TimeSpan OTCa = TimeSpan.Zero;//ver 4.0.0.4	
			if (checkXNLamThem.Checked && TimeSpan.TryParseExact(maskPhutTinhLamThem.Text, @"hh\:mm", CultureInfo.InvariantCulture, out OTCa) == false)
			{
				ACMessageBox.Show(Resources.Text_NhapThoiGianLamThemChuaHopLe, Resources.Caption_Loi, 2000);
				return;
			}
			var sophutOT = Convert.ToInt32(OTCa.TotalMinutes);//ver 4.0.0.4	
			var TinhPCTC = (checkTinhPC150.Checked);
			var bVaoTreLaCV = checkXNVaoTreTinhCV.Checked;//ver 4.0.0.4	
			var bRaaSomLaCV = checkXNRaaSomTinhCV.Checked;//ver 4.0.0.4	
			string lydo = (cbLyDo.SelectedItem != null) ? cbLyDo.SelectedItem.ToString() : cbLyDo.Text;
			string ghichu = tbGhiChu.Text;

			#endregion

			#region hỏi lại trước khi thực hiện

			if (MessageBox.Show(Resources.Text_XacNhanThemChamCongTayChoQL, Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}

			#endregion

			try
			{
				var ca = (tbCa.Tag == null) ? null : (cCa)tbCa.Tag;
				foreach (var nv in listNV) {
					foreach (var ngay in DSNgayCheck) {
						DateTime TimeStrInn = ngay.Date.Add(TimeSpanBD);
						DateTime TimeStrOut = ngay.Date.Add(TimeSpanKT);
						if (TimeSpanBD > TimeSpanKT) TimeStrOut = TimeStrOut.AddDays(1d);

						if (ca.QuaDem && TimeSpanBD < TimeSpanKT && TimeSpanBD < XL2._04h30) {
							TimeStrInn = TimeStrInn.AddDays(1d);
							TimeStrOut = TimeStrOut.AddDays(1d);
						}

/*
						if (ca.ID == int.MinValue)
							XL.TaoCaTuDo(ca, TimeStrInn, XL2._08gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, "8");
						else if (ca.ID == int.MinValue + 1)
							XL.TaoCaTuDo(ca, TimeStrInn, XL2._12gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1.5f, "D");
*/
						if (ca.ID < int.MinValue + 100) XL.TaoCaTuDo(ca, TimeStrInn);//ver 4.0.0.4	

						bool match = XL.KiemtraThuocCa(TimeStrInn, TimeStrOut, ngay, ca);
						// trường hợp chỉ chấm công bình thường thì chỉ thêm giờ cho đơn giản
						// các trường hợp xác nhận làm thêm x phút thì chuyển sang xác nhận
						if (ca.ID < 0) {
							DAO.ThemGioChoNV(nv.MaCC, TimeStrInn, "I", 21, lydo, ghichu);
							DAO.ThemGioChoNV(nv.MaCC, TimeStrOut, "O", 22, lydo, ghichu);
							XL.XacNhan_CIO_A(nv.MaCC, TimeStrInn, TimeStrOut, ca, false, false, sophutOT, TinhPCTC, lydo, ghichu, 
								bVaoTreLaCV, bRaaSomLaCV);
						}
						else if (ca.IsExtended || sophutOT > 0 || match == false) {
							// các trường hợp phải xác nhận là ca tự do, ca mở rộng, ca có làm thêm và giờ nhập ko nằm trong khoảng nhận diện
							if (ca.TachCaDem) {
								var timeOutCa3 = ngay.Add(ca.catruoc.Duty.Off);
								var timeInnCa1 = ngay.AddDays(1d).Add(ca.casauuu.Duty.Onn).Add(XL2._01giay);
								DAO.ThemGioChoNV(nv.MaCC, TimeStrInn, "I", 21, lydo, ghichu);
								DAO.ThemGioChoNV(nv.MaCC, timeOutCa3, "O", 22, "Hệ thống tự động thêm giờ đệm tách ca qua ngày", "Thực hiện tự động");
								DAO.ThemGioChoNV(nv.MaCC, timeInnCa1, "I", 21, "Hệ thống tự động thêm giờ đệm tách ca qua ngày", "Thực hiện tự động");
								DAO.ThemGioChoNV(nv.MaCC, TimeStrOut, "O", 22, lydo, ghichu);
								XL.XacNhan_CIO_A(nv.MaCC, TimeStrInn, timeOutCa3, ca.catruoc, false, false, 0, TinhPCTC, lydo, ghichu,
									bVaoTreLaCV, bRaaSomLaCV);//ver 4.0.0.4	
								XL.XacNhan_CIO_A(nv.MaCC, timeInnCa1, TimeStrOut, ca.casauuu, false, false, sophutOT, TinhPCTC, lydo, ghichu,
									bVaoTreLaCV, bRaaSomLaCV);//ver 4.0.0.4	
							}
							else {
								DAO.ThemGioChoNV(nv.MaCC, TimeStrInn, "I", 21, lydo, ghichu);
								DAO.ThemGioChoNV(nv.MaCC, TimeStrOut, "O", 22, lydo, ghichu);
								XL.XacNhan_CIO_A(nv.MaCC, TimeStrInn, TimeStrOut, ca, false, false, sophutOT, TinhPCTC, lydo, ghichu,
									bVaoTreLaCV, bRaaSomLaCV);//ver 4.0.0.4	
							}
						}
						else {
							DAO.ThemGioChoNV(nv.MaCC, TimeStrInn, "I", 21, lydo, ghichu);
							DAO.ThemGioChoNV(nv.MaCC, TimeStrOut, "O", 22, lydo, ghichu);
						}
					}
				}
				MyUtility.CheckedCheckBox(false, chkAllWKDays, chkAddSat, chkAddSun);//ver 4.0.0.4	
				ACMessageBox.Show("Đã thêm giờ chấm công cho quản lý.", Resources.Caption_ThongBao, 2000);
			}
			catch (Exception ex)
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}

		void dtp_ValueChanged(object sender, EventArgs e) {
			linkTinhLaiCong_LinkClicked(null, null);
		}
		
		private void linkTinhLaiCong_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			DateTime td_batdau_lv, td_ketthuc_lv_chuaOT, td_ketthuc_lv_daCoOT, TD_BD_LV_Ca3, TD_KT_LV_Ca3;
			TimeSpan tre, som, tongGiothuc = TimeSpan.Zero, TGGioLamViec = TimeSpan.Zero, TGLamBanDem, tgTinhPC30, tgTinhPC50, tgTinhPCTCC3;
			float Cong = 0f, PhuCap30, PhuCapTC, PhuCapTCC3, TongPhuCap = 0f;
			bool QuaDem;
			var ngay = DateTime.Today;
			TimeSpan timespanBD = dtpBDLam.Value.TimeOfDay;
			TimeSpan timeSpanKT = dtpKTLam.Value.TimeOfDay;
			var timeBD = ngay.Add(timespanBD);
			var timeKT = ngay.Add(timeSpanKT);
			if (timespanBD > timeSpanKT) timeKT = timeKT.AddDays(1d);

			cCa ca = tbCa.Tag as cCa;
			if (ca == null) {
				goto point;
			}

/*
			if (ca.ID < 0 )
			{
				ca.Duty = new TS {Onn = timespanBD, Off = timeSpanKT};
				XL.TaoCaTuDo(ca, timeBD, XL2._08gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, "8", false);//fix lỗi 
			}
*/
			if (ca.ID < int.MinValue+100)
			{
				//ca.Duty = new TS {Onn = timespanBD, Off = timeSpanKT};
				//XL.TaoCaTuDo(ca, timeBD, XL2._08gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, "8", false);//fix lỗi 
				XL.TaoCaTuDo(ca, timeBD);
			}
			else if (timeBD > ngay.Add(ca.Duty.Off) || timeKT < ngay.Add(ca.Duty.Onn)) {
				goto point;
			}
			//var sophutOT = (checkXNLamThem.Checked && numSoPhutOT.Value > 0) ? (int)numSoPhutOT.Value : 0;
			TimeSpan OTCa = TimeSpan.Zero;//ver 4.0.0.4	
			if (checkXNLamThem.Checked)
			{
				TimeSpan.TryParseExact(maskPhutTinhLamThem.Text, @"hh\:mm", CultureInfo.InvariantCulture, out OTCa);
			}
			var sophutOT = Convert.ToInt32(OTCa.TotalMinutes);//ver 4.0.0.4	

			var tinhPCTC = (checkTinhPC150.Checked);

			tongGiothuc = timeKT - timeBD;
			TimeSpan TGGioLamViecTrongCa;
			XL.Vao(timeBD, ngay.Add(ca.Duty.Onn), ngay.Add(ca.chophepTreTS), true, true, out td_batdau_lv, out tre);//todo 4.0.0.8
			XL.Raa(timeKT, ngay.Add(ca.Duty.Off), ngay.Add(ca.chophepSomTS), false, false, out td_ketthuc_lv_chuaOT, out som); //todo 4.0.0.8
			XL.LamThem(td_ketthuc_lv_chuaOT, new TimeSpan(0, sophutOT, 0), out td_ketthuc_lv_daCoOT);
			if (timeKT < td_ketthuc_lv_daCoOT)
			{
				timeKT = ngay.Add(ca.Duty.Off).Add(new TimeSpan(0, sophutOT, 0));
				dtpKTLam.Value = timeKT;
			}
			XL.Tinh_TGLamViecTrongCa(td_batdau_lv, td_ketthuc_lv_chuaOT, ca.LunchMin, out TGGioLamViecTrongCa);
			TGGioLamViec = TGGioLamViecTrongCa + OTCa;//ver 4.0.0.4	
			XL.Tinh_TGLamViec_Ca3(td_batdau_lv, td_ketthuc_lv_daCoOT, ngay.Add(XL2._22h00), ngay.AddDays(1d).Add(XL2._06h00), out TD_BD_LV_Ca3, out  TD_KT_LV_Ca3, out TGLamBanDem, out QuaDem);
			TimeSpan TGLamThem = XL.Tinh_TGLamThem(TGGioLamViec);//(TGGioLamViec - XL2._08gio) >= XL2._01phut ? (TGGioLamViec - XL2._08gio) : TimeSpan.Zero;
			float cong_trong_ca = Convert.ToSingle(Math.Round(((TGGioLamViecTrongCa.TotalHours / ca.WorkingTimeTS.TotalHours) * ca.Workingday), 2));
			float cong_ngoai_ca = Convert.ToSingle(Math.Round((OTCa.TotalHours / 8f), 2));
			Cong = cong_trong_ca + cong_ngoai_ca;

			XL.Tinh_PCTC(tinhPCTC, QuaDem, TGLamBanDem, TGLamThem, out tgTinhPC30, out tgTinhPC50, out tgTinhPCTCC3, out PhuCap30, out PhuCapTC, out PhuCapTCC3, out TongPhuCap);

		point:
			tbTongGio.Text = tongGiothuc.ToString(@"hh\gmm\p");
			tbGioLam.Text = TGGioLamViec.ToString(@"hh\gmm\p");
			tbCong.Text = (Math.Round(Cong, 2)).ToString();
			tbPC.Text = (TongPhuCap.ToString("0.0#"));
		}

		private void checkXNLamThem_CheckedChanged(object sender, EventArgs e) {
			linkTinhLaiCong_LinkClicked(null, null);
		}

		private void btnChonCa_Click(object sender, EventArgs e) {
			// kiểm tra xem nếu tồn tại 1 ca nào đó rồi thì gửi ca đó đi, nếu chưa tồn tại ca ( chế độ hàng loạt, tbXNCa null thì gửi đi null
			var currShift = (cCa)tbCa.Tag;
			frmDSCa frm = new frmDSCa { StartPosition = FormStartPosition.CenterParent,SelectedShift = currShift };
			frm.ShowDialog();
			// sau khi showdialog và nhận được ca từ form xác nhận thì tiến hành fill và tính toán
			if (frm.SelectedShift == null) return;
			currShift = frm.SelectedShift;
			tbCa.Tag = currShift;
			tbCa.Text = (currShift != null) ? currShift.Code : string.Empty;
			//if (currShift != null && currShift.ID != int.MinValue) {
			if (currShift != null && currShift.ID > int.MinValue + 100) {//ver 4.0.0.4	ko phải ca tự do thì điền giờ chuẩn theo quy định ca vào
				dtpBDLam.Value = DateTime.Today.Date.Add(currShift.Duty.Onn);
				dtpKTLam.Value = DateTime.Today.Date.Add(currShift.Duty.Off);
			}
			else if (currShift != null && currShift.ID < int.MinValue + 100) // ca tự do thì mặc định check VaoTreLaCV, RaSomLaCV
			{
				checkXNVaoTreTinhCV.Checked = true;
				checkXNRaaSomTinhCV.Checked = true;
			}
			linkTinhLaiCong_LinkClicked(null, null);
		}

		private void tbSearch_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter)
				btnTim.PerformClick();

		}


	}
}
