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
using ChamCong_v05.UI.KhaiBao;
using ChamCong_v05.UI.XacNhan;
using log4net;

namespace ChamCong_v05.UI.ChamCong {
	public partial class frm_XemCong : Form {
		#region log tooltip và hàm ko quan trọng

		public readonly ILog lg = LogManager.GetLogger("frm_XemCong");

		private void toolTipHint_Draw(object sender, DrawToolTipEventArgs e)
		{
			Font f = new Font("Arial", 10.0f);
			e.DrawBackground();
			e.DrawBorder();
			e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, new PointF(2f, 2f));
		}

		private void toolTipHint_Popup(object sender, PopupEventArgs e)
		{
			Size temp = TextRenderer.MeasureText(toolTipHint.GetToolTip(e.AssociatedControl), new Font("Arial", 10.0f));
			temp.Width += 4;
			temp.Height += 4;
			e.ToolTipSize = temp;
		}

		#endregion

		public List<int> m_listIDPhongBan = new List<int>();
		public List<cUserInfo> m_DSNV = new List<cUserInfo>();
		public List<cPhongBan> m_DSPhg = new List<cPhongBan>();

		#region các biến bảng cục bộ và hàm tạo bảng

		public DataTable m_Bang_DSNV;

		public DataTable TaoBang_DSNV() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "check", "cUserInfo", "UserEnrollNumber", "UserFullCode", "UserFullName", "SchID", "SchName", "ChucVu", "MaPhong", "TenPhong", "HeSoLuongCB", "HeSoLuongSP", "HSBHCongThem" },
				new[] { typeof(bool), typeof(cUserInfo), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(float), typeof(float), typeof(float) });
			return kq;
		}

		public DataTable m_Bang_TongHopXemCong; // sử dụng chung hàm tạo cấu trúc table tổng hợp

		public DataTable TaoBang_TongHopXemCong() {
			var kq = new DataTable();
			kq.Columns.Add("check", typeof(bool)); //0
			kq.Columns.Add("UserEnrollNumber", typeof(int)); //0
			kq.Columns.Add("UserFullName", typeof(string)); //1
			kq.Columns.Add("UserFullCode", typeof(string));
			kq.Columns.Add("TimeStrNgay", typeof(DateTime)); //2
			kq.Columns.Add("TimeStrThu", typeof(DateTime)); //3
			kq.Columns.Add("ShiftCode", typeof(string)); //21
			kq.Columns.Add("TimeStrVao1", typeof(DateTime)); //4
			kq.Columns.Add("TimeStrRaa1", typeof(DateTime)); //5
			kq.Columns.Add("TimeStrVao2", typeof(DateTime)); //6
			kq.Columns.Add("TimeStrRaa2", typeof(DateTime)); //7
			kq.Columns.Add("TimeStrVao3", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRaa3", typeof(DateTime)); //9
			kq.Columns.Add("cCheckInOut1", typeof(cCheckInOut));
			kq.Columns.Add("cCheckInOut2", typeof(cCheckInOut));
			kq.Columns.Add("cCheckInOut3", typeof(cCheckInOut));
			kq.Columns.Add("TongTre", typeof(int)); //18
			kq.Columns.Add("TongSom", typeof(int)); //19
			kq.Columns.Add("TongCong", typeof(Single)); //20
			kq.Columns.Add("TongPhuCap", typeof(Single)); //22
			kq.Columns.Add("TongGioLam", typeof(TimeSpan));
			kq.Columns.Add("TongGioThuc", typeof(TimeSpan));
			kq.Columns.Add("cNgayCong", typeof(cNgayCong));
			kq.Columns.Add("cUserInfo", typeof(cUserInfo));
			kq.Columns.Add("IsEdited", typeof(bool));
			kq.Columns.Add("TinhPCTC", typeof(bool));
			return kq;
		}

		public DataTable TaoBang_VaoTreRaaSom() {
			var kq = new DataTable();
			kq.Columns.Add("check", typeof(bool)); //0
			kq.Columns.Add("UserEnrollNumber", typeof(int)); //0
			kq.Columns.Add("UserFullName", typeof(string)); //1
			kq.Columns.Add("UserFullCode", typeof(string));
			kq.Columns.Add("TimeStrNgay", typeof(DateTime)); //2
			kq.Columns.Add("TimeStrThu", typeof(DateTime)); //3
			kq.Columns.Add("ShiftCode", typeof(string)); //21
			kq.Columns.Add("TimeStrVao1", typeof(DateTime)); //4
			kq.Columns.Add("TimeStrRaa1", typeof(DateTime)); //5
			kq.Columns.Add("TimeStrVao2", typeof(DateTime)); //6
			kq.Columns.Add("TimeStrRaa2", typeof(DateTime)); //7
			kq.Columns.Add("TimeStrVao3", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRaa3", typeof(DateTime)); //9
			kq.Columns.Add("cCheckInOut1", typeof(cCheckInOut));
			kq.Columns.Add("cCheckInOut2", typeof(cCheckInOut));
			kq.Columns.Add("cCheckInOut3", typeof(cCheckInOut));
			kq.Columns.Add("TongTre", typeof(int)); //18
			kq.Columns.Add("TongSom", typeof(int)); //19
			kq.Columns.Add("TongCong", typeof(Single)); //20
			kq.Columns.Add("TongPhuCap", typeof(Single)); //22
			kq.Columns.Add("TongGioLam", typeof(TimeSpan));
			kq.Columns.Add("TongGioThuc", typeof(TimeSpan));
			kq.Columns.Add("cNgayCong", typeof(cNgayCong));
			kq.Columns.Add("cUserInfo", typeof(cUserInfo));
			kq.Columns.Add("IsEdited", typeof(bool));
			kq.Columns.Add("TinhPCTC", typeof(bool));

			return kq;
		}

		public DataTable m_Bang_GioKDQD; // sử dụng chung hàm tạo cấu trúc table tổng hợp

		public DataTable TaoBang_GioKDQD() {
			return TaoBang_TongHopXemCong();
		}

		public DataTable m_Bang_GioThieuCheck; // sử dụng chung hàm tạo cấu trúc table tổng hợp

		public DataTable TaoBang_GioThieuCheck() {
			return TaoBang_TongHopXemCong();
		}

		public DataTable m_Bang_ThK_TreSom; // sử dụng chung hàm tạo cấu trúc table tổng hợp

		public DataTable TaoBang_ThK_TreSom() {
			return TaoBang_VaoTreRaaSom();
		}

		#endregion

		#region biến checkbox check all: 1 của DSNV cần xem công

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

		public frm_XemCong() {
			InitializeComponent();
			log4net.Config.XmlConfigurator.Configure();

			#region khởi tạo các biến cục bộ
			m_DSPhg = new List<cPhongBan>();
			m_listIDPhongBan = new List<int>();
			m_DSNV = new List<cUserInfo>();
			#endregion

			#region tạo template dataSource

			m_Bang_DSNV = TaoBang_DSNV();
			m_Bang_TongHopXemCong = TaoBang_TongHopXemCong();
			m_Bang_GioKDQD = TaoBang_GioKDQD();
			m_Bang_GioThieuCheck = TaoBang_GioThieuCheck();
			m_Bang_ThK_TreSom = TaoBang_ThK_TreSom();

			#endregion

			#region không cho autogen các column khi bind dữ liệu: 4 cái

			dgrdTongHop.AutoGenerateColumns = dgrdGioKDQD.AutoGenerateColumns
			= dgrdGioThieuCheck.AutoGenerateColumns = dgrdThKTreSom.AutoGenerateColumns
			= dgrdDSNVTrgPhg.AutoGenerateColumns = false;

			#endregion

			#region gán template vào các dataSource, hoặc dataView vào các dataSource

			dgrdTongHop.DataSource = m_Bang_TongHopXemCong;
			dgrdGioKDQD.DataSource = m_Bang_GioKDQD;
			dgrdGioThieuCheck.DataSource = m_Bang_GioThieuCheck;
			dgrdThKTreSom.DataSource = m_Bang_ThK_TreSom;
			DataView dataView = new DataView(m_Bang_DSNV);
			dgrdDSNVTrgPhg.DataSource = dataView;

			#endregion

			//3. vẽ 3 checkbox checkall cho DSNV trong phòng
			XL2.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));

			#region khởi tạo giá trị mặc định ngày từ thứ 2 tuần trước đến ngày hôm nay

			var today = DateTime.Today;
			DateTime mondayOfLastWeek = today.AddDays(-(int)today.DayOfWeek - 6);
			var ngaybd = mondayOfLastWeek;
			var ngaykt = today;
			if (Settings.Default.LastStartDate != null && Settings.Default.LastStartDate != DateTime.MinValue) {
				ngaybd = Settings.Default.LastStartDate;ngaykt = Settings.Default.LastEndDate;
			}
			dtpNgayBD.Value = ngaybd;
			dtpNgayKT.Value = ngaykt;

			#endregion


		}

		private void frm_XemCong_Load(object sender, EventArgs e) {
			#region kiểm tra kết nối csdl , nếu mất kết nối thì đóng

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}

			#endregion

			#region //2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan ,
			// trường hợp ko có phòng ban nào được phép thao tác thì báo và thoát form

			XL.KhoiTaoDSPhongBan(m_DSPhg, XL2.currUserID); // tạo danh sách phòng ban chỉ userID này được thao tác
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

			//vô hiệu hoá các nút chức năng ko có phân quyền
			btnThemXoaSuaHangLoat.Enabled = XL2.QuyenThaoTac.Any(o => o == (int)Quyen.ThemXoaSuaGioCC);
			btnThemXoaSuaDonGian.Enabled = XL2.QuyenThaoTac.Any(o => o == (int)Quyen.ThemXoaSuaGioCC);
			btnKetCongThang.Enabled = XL2.QuyenThaoTac.Any(o => o == (int)Quyen.KetCongThang);
			btnThemKBVang.Enabled = XL2.QuyenThaoTac.Any(o => o == (int)Quyen.KhaiBaoVang);
			btnXoaKBVang.Enabled = XL2.QuyenThaoTac.Any(o => o == (int)Quyen.KhaiBaoVang);
			btnXNCa.Enabled = XL2.QuyenThaoTac.Any(o => o == (int)Quyen.XNCa_LamThem);
			btnXNCaVaLamThem.Enabled = XL2.QuyenThaoTac.Any(o => o == (int)Quyen.XNCa_LamThem);
			btnXNPCTC.Enabled = XL2.QuyenThaoTac.Any(o => o == (int)Quyen.XN_PCTC);
			btnXacNhanPC100.Enabled = XL2.QuyenThaoTac.Any(o => o == (int)Quyen.XN_PCDB);
		}

		#region select node in tree

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {

			#region mỗi lần chọn node thì lấy ID node hiện tại và tất cả node con

			m_listIDPhongBan.Clear();
			if (e.Node.FirstNode != null) XL.GetIDNodeAndChildNode(e.Node, ref m_listIDPhongBan);
			else {
				var temp = ((cPhongBan)e.Node.Tag);
				if (temp.ChoPhep) m_listIDPhongBan.Add(temp.ID);
			}
			e.Node.Expand();

			#endregion

			#region kiểm tra kết nối csdl , nếu mất kết nối thì đóng

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}

			#endregion


			XL.KhoiTaoDSNV_ChamCong(m_DSNV, m_listIDPhongBan, this.m_DSPhg);

			XL.TaoTableDSNV(m_DSNV, m_Bang_DSNV);

			#region tạo datasourcr cho autocomplete

			var Source = new AutoCompleteStringCollection();
			Source.AddRange((from nv in m_DSNV select nv.TenNV.ToUpperInvariant()).ToArray());
			tbSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tbSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			tbSearch.AutoCompleteCustomSource = Source;

			#endregion

			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = string.Empty;

			m_Bang_TongHopXemCong.Rows.Clear();
			m_Bang_GioKDQD.Rows.Clear();
			m_Bang_GioThieuCheck.Rows.Clear();
			m_Bang_ThK_TreSom.Rows.Clear();

			checkAll_GridDSNV.Checked = false;
		}

		#endregion

		private void btnXem_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;


			#region //1. lấy dữ liệu từ form lấy ngày BD và kết thúc nếu quá 62 ngày thì báo

			dtpNgayBD.Update();
			dtpNgayKT.Update();
			var ngayBD = dtpNgayBD.Value.Date;
			var ngayKT = dtpNgayKT.Value.Date;
			if (ngayBD > ngayKT) MyUtility.Swap(ref ngayBD, ref ngayKT);
			var ngayBD_Bef2D = ngayBD.AddDays(-2d);
			var ngayKT_Aft2D = ngayKT.AddDays(2d);
			// ngày 1->5 là trừ ra = 4, + 1 để ra số ngày, + thêm 2 để ra cột(UserEnrollNumber và số lượng)

			if ((ngayKT - ngayBD).Duration() > new TimeSpan(62, 0, 0, 0)) {
				if (MessageBox.Show(Resources.Text_KhoangThoiGianXemCongQuaDai, Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
					return;
				}
			}
			#endregion

			XL.SaveSetting(lastStartDate: ngayBD, lastEndDate: ngayKT);
			BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();

			#region //2. lấy danh sách nhân viên check, nếu chưa có nv nào check thì thông báo

			var listNV = (from DataGridViewRow dataGridViewRow in dgrdDSNVTrgPhg.Rows
						  let rowView = dataGridViewRow.DataBoundItem as DataRowView
						  where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
						  select (cUserInfo)rowView["cUserInfo"])
						  .ToList();

			if (listNV.Count == 0) {
				ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000); //
				GC.Collect();
				return;
			}

			#endregion

			//3. lấy dữ liệu chấm công của các nhân viên
			try {
				if (XL.KiemtraDulieuCapnhatTuServer(DateTime.Now) == false) {
					ACMessageBox.Show(Resources.Text_DuLieuChamCongChuaUpdate, Resources.Caption_ThongBao, 4000);
				}
				WaitWindow.Show(this.XuLyXemCong, "Đang xử lý, vui lòng đợi trong giây lát...", new object[] { listNV, ngayBD_Bef2D, ngayKT_Aft2D });
				//XL.XemCong_v08(listNV, ngayBD_Bef2D, ngayKT_Aft2D);
				Reload4DataGrid(listNV);
			} catch (Exception exception) {
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), exception);
				MessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi);
				GC.Collect();
			}

		}

		public void XuLyXemCong(object sender, WaitWindowEventArgs e) {
			List<cUserInfo> listNV = (List<cUserInfo>)e.Arguments[0];
			DateTime ngayBD_Bef2D = (DateTime)e.Arguments[1];
			DateTime ngayKT_Aft2D = (DateTime)e.Arguments[2];
			XL.XemCong_v08(listNV, ngayBD_Bef2D, ngayKT_Aft2D);
			//Reload4DataGrid(listNV);

		}

		private void Reload4DataGrid(List<cUserInfo> listNV) {
			XL.TaoTableXemCong(listNV, m_Bang_TongHopXemCong);
			XL.TaoTableGioKDQD(listNV, m_Bang_GioKDQD);
			XL.TaoTableGioThieuCheck(listNV, m_Bang_GioThieuCheck);
			XL.TaoTableThK_TreSom(listNV, m_Bang_ThK_TreSom);
			VeLaiDataGrid(dgrdTongHop);
			VeLaiDataGrid(dgrdGioKDQD);
			VeLaiDataGrid(dgrdGioThieuCheck);
			VeLaiDataGrid(dgrdThKTreSom);

			dgrdTongHop.Invalidate();
			dgrdGioKDQD.Invalidate();
			dgrdGioThieuCheck.Invalidate();
			dgrdThKTreSom.Invalidate();

		}

		private void btnXNCaVaLamThem_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;


			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(dtpNgayBD.Value.Date, dtpNgayKT.Value.Date)) {
				MessageBox.Show(string.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "xem công", "xác nhận ca, làm thêm", "xem công"),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}

			#endregion

			// xác định đang chọn datagrid nào để lấy các datarow của các datagrid đó
			DataGridView currDataGrid = XacDinhDataGridViewDangChon();

			// lọc lấy các datarow được check, nếu không có row nào được check thì chế độ view all, ngược lại thì xem các ngày được check
			var table = currDataGrid.DataSource as DataTable;
			if (table == null) return;
			var arrRecord = table.Select("", "UserEnrollNumber asc");

			if (arrRecord.Length == 0) return;

			var frm112 = new frm_XN_LamThem { m_DSNV = m_DSNV, StartPosition = FormStartPosition.CenterParent };
			//frm112.m_DSNV = m_DSNV;
			//frm112.Location = new Point((int)((Size.Width - frm112.Size.Width) / 2f), (int)((Size.Height - frm112.Size.Height) / 2f));
			frm112.ShowDialog();

			// lấy dsnv đang chọn
			var listNV = (from DataGridViewRow dataGridViewRow in dgrdDSNVTrgPhg.Rows
						  let rowView = dataGridViewRow.DataBoundItem as DataRowView
						  where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
						  select (cUserInfo)rowView["cUserInfo"])
						  .ToList();

			if (frm112.IsReload) {
				Reload4DataGrid(listNV);
			}


		}

		private void btnXuatBaoBieu_Click(object sender, EventArgs e) {

			#region lấy ngày BD và kết thúc, và update lại Ngày BD = một ngày trước 31/08 12:00 AM, ngày KT là 1 ngày sau ngay 1 23:59:59
			dtpNgayBD.Update(); dtpNgayKT.Update();
			DateTime ngayBD_Bef2D = dtpNgayBD.Value.Date;
			DateTime ngayKT_Aft2D = dtpNgayKT.Value.Date;
			if (ngayBD_Bef2D > ngayKT_Aft2D) MyUtility.Swap(ref ngayBD_Bef2D, ref ngayKT_Aft2D);
			ngayBD_Bef2D = ngayBD_Bef2D.AddDays(-2d);
			ngayKT_Aft2D = ngayKT_Aft2D.AddDays(2d);
			#endregion

			IEnumerable<DataGridViewRow> lstGridViewRow = dgrdDSNVTrgPhg.Rows.Cast<DataGridViewRow>();
			var listNV = (from row in (lstGridViewRow)
						  where ((DataRowView)row.DataBoundItem)["check"] != DBNull.Value && (bool)((DataRowView)row.DataBoundItem)["check"]
						  select (cUserInfo)((DataRowView)row.DataBoundItem).Row["cUserInfo"]).ToList();
			if (listNV.Count == 0) {
				ACMessageBox.Show("Bạn chưa chọn Nhân viên", "Thông báo", 2000);
				GC.Collect();
				return;
			}

			frm_XuatBBCongPC frm = new frm_XuatBBCongPC {
				StartPosition = FormStartPosition.CenterParent,
				m_dsnv = listNV, m_Thang = dtpNgayBD.Value
			};
			//frm.Location = new Point((int)((Size.Width - frm.Size.Width) / 2f), (int)((Size.Height - frm.Size.Height) / 2f));

			frm.ShowDialog();

		}

		private void btnThemXoaSuaDonGian_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(dtpNgayBD.Value.Date, dtpNgayKT.Value.Date)) {
				MessageBox.Show(string.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "xem công", "chỉnh sửa giờ chấm công", "xem công"),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}

			#endregion

			// xác định datagrid nào đang chọn thì lấy dòng SelectedRows[0] của datagrid đó
			DataGridView currDataGrid = XacDinhDataGridViewDangChon();

			if (currDataGrid.SelectedRows.Count == 0) return;
			var arrRecord = ((DataRowView)((currDataGrid.SelectedRows[0]).DataBoundItem)).Row;

			// gọi form editTime và truyền dòng dữ liệu đang chọn sang cho form đó
			var frm1 = new frm_XemCT_GioCC {
				StartPosition = FormStartPosition.CenterParent,
				selectedRow = arrRecord
			};
			frm1.ShowDialog();

			#region // sau khi edit giờ chấm công xong thì reload lại và tô màu 2 dataGrid tổng hợp và giờ KDQD

			if (frm1.IsReload) {
				// lấy dsnv đang chọn
				var listNV = (from DataGridViewRow dataGridViewRow in dgrdDSNVTrgPhg.Rows
							  let rowView = dataGridViewRow.DataBoundItem as DataRowView
							  where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
							  select (cUserInfo)rowView["cUserInfo"])
							  .ToList();

				Reload4DataGrid(listNV);
			}

			#endregion

		}

		private DataGridView XacDinhDataGridViewDangChon() {
			DataGridView currDataGrid;
			if (tabControlChiTiet.SelectedTab == tabTongHop) currDataGrid = dgrdTongHop;
			else if (tabControlChiTiet.SelectedTab == tabGioKDQD) currDataGrid = dgrdGioKDQD;
			else if (tabControlChiTiet.SelectedTab == tabGioThieuCheck) currDataGrid = dgrdGioThieuCheck;
			else if (tabControlChiTiet.SelectedTab == tabThK_TreSom) currDataGrid = dgrdThKTreSom;
			else return null;
			return currDataGrid;
		}

		private void VeLaiDataGrid(DataGridView dataGridView) {
			var listDataGridViewRow = (from DataGridViewRow dataGridViewRow in dataGridView.Rows
									   let rowView = dataGridViewRow.DataBoundItem as DataRowView
									   where (rowView["IsEdited"] != DBNull.Value && (bool)rowView["IsEdited"])
									   select dataGridViewRow)
						  .ToList();

			foreach (var dataGridViewRow in listDataGridViewRow) {
				dataGridViewRow.DefaultCellStyle.BackColor = Color.Aquamarine;
			}


			foreach (var dataGridViewRow in (from DataGridViewRow rowSat_Sun in dataGridView.Rows
											 let rowView = rowSat_Sun.DataBoundItem as DataRowView
											 where (((DateTime)rowView["TimeStrNgay"]).DayOfWeek == DayOfWeek.Saturday
												   || ((DateTime)rowView["TimeStrNgay"]).DayOfWeek == DayOfWeek.Sunday)
											 select rowSat_Sun)) {
				dataGridViewRow.DefaultCellStyle.BackColor =
					((DateTime)((DataRowView)dataGridViewRow.DataBoundItem)["TimeStrNgay"]).DayOfWeek == DayOfWeek.Saturday
					? Color.Gainsboro
					: Color.Silver;
			}

		}


		private void btnThemXoaSuaHangLoat_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(dtpNgayBD.Value.Date, dtpNgayKT.Value.Date)) {
				MessageBox.Show(string.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "xem công", "chỉnh sửa giờ chấm công", "xem công"),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}

			#endregion

			//xác định datagrid nào đang chọn thì lấy dòng SelectedRows[0] của datagrid đó
			DataGridView currDataGrid = XacDinhDataGridViewDangChon();

			var arrRecord = (from DataGridViewRow row in currDataGrid.SelectedRows
							 select ((DataRowView)row.DataBoundItem).Row).ToArray();

			if (arrRecord.Length == 0) return;

			var frm1 = new frm_ThemXoaSuaGioHangLoat {
				StartPosition = FormStartPosition.CenterParent,
				selectedRow = arrRecord
			};
			//frm1.Location = new Point((int)((Size.Width - frm1.Size.Width) / 2f), (int)((Size.Height - frm1.Size.Height) / 2f));
			frm1.ShowDialog();
			// sau khi edit giờ chấm công xong thì reload lại và tô màu 2 dataGrid tổng hợp và giờ KDQD
			if (frm1.IsReload) {
				// lấy dsnv đang chọn
				var listNV = (from DataGridViewRow dataGridViewRow in dgrdDSNVTrgPhg.Rows
							  let rowView = dataGridViewRow.DataBoundItem as DataRowView
							  where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
							  select (cUserInfo)rowView["cUserInfo"])
							  .ToList();

				Reload4DataGrid(listNV);

			}



		}

		private void btnXacNhanPC100_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(dtpNgayBD.Value.Date, dtpNgayKT.Value.Date)) {
				MessageBox.Show(string.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "xem công", "xác nhận phụ cấp", "xem công"),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}

			#endregion


			// xác định đang chọn datagrid nào để lấy các datarow của các datagrid đó
			DataGridView currDataGrid = XacDinhDataGridViewDangChon();

			// lọc lấy các datarow được check, nếu không có row nào được check thì chế độ view all, ngược lại thì xem các ngày được check
			if (currDataGrid.SelectedRows.Count != 0) {
				var arrRecord = (from DataGridViewRow row in currDataGrid.SelectedRows
								 select (((DataRowView)row.DataBoundItem).Row)).ToArray();

				if (arrRecord.Length == 0) return;

				frm_XN_PC100 frm112 = new frm_XN_PC100 {
					StartPosition = FormStartPosition.CenterParent,
					m_arrRecd = arrRecord
				};
				//frm112.Location = new Point((int)((Size.Width - frm112.Size.Width) / 2f), (int)((Size.Height - frm112.Size.Height) / 2f));
				//frm112.m_DSNV = m_DSNV;
				frm112.ShowDialog();

				// lấy dsnv đang chọn
				var listNV = (from DataGridViewRow dataGridViewRow in dgrdDSNVTrgPhg.Rows
							  let rowView = dataGridViewRow.DataBoundItem as DataRowView
							  where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
							  select (cUserInfo)rowView["cUserInfo"])
							  .ToList();

				if (frm112.IsReload) {
					Reload4DataGrid(listNV);
				}
			}

		}

		private void btnXNPCTC_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(dtpNgayBD.Value.Date, dtpNgayKT.Value.Date)) {
				MessageBox.Show(string.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "xem công", "xác nhận phụ cấp", "xem công"),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}

			#endregion

			// xác định đang chọn datagrid nào để lấy các datarow của các datagrid đó
			DataGridView currDataGrid = XacDinhDataGridViewDangChon();

			// lọc lấy các datarow được check, nếu không có row nào được check thì chế độ view all, ngược lại thì xem các ngày được check
			if (currDataGrid.SelectedRows.Count != 0) {
				var arrRecord = (from DataGridViewRow row in currDataGrid.SelectedRows
								 select (((DataRowView)row.DataBoundItem).Row)).ToArray();

				if (arrRecord.Length == 0) return;

				frm_XN_PC50 frm112 = new frm_XN_PC50 {
					StartPosition = FormStartPosition.CenterParent,
					m_arrRecd = arrRecord
				};
				//frm112.Location = new Point((int)((Size.Width - frm112.Size.Width) / 2f), (int)((Size.Height - frm112.Size.Height) / 2f));
				//frm112.m_DSNV = m_DSNV;
				frm112.ShowDialog();

				// lấy dsnv đang chọn
				var listNV = (from DataGridViewRow dataGridViewRow in dgrdDSNVTrgPhg.Rows
							  let rowView = dataGridViewRow.DataBoundItem as DataRowView
							  where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
							  select (cUserInfo)rowView["cUserInfo"])
							  .ToList();

				if (frm112.IsReload) {
					int tempUserEnrollNumber;
					DateTime tempDateTime;
					GetCurrRow(dgrdTongHop, out tempUserEnrollNumber, out tempDateTime);
					XL.TaoTableXemCong(listNV, m_Bang_TongHopXemCong);
					SetCurrRow(dgrdTongHop, tempUserEnrollNumber, tempDateTime);
					GetCurrRow(dgrdGioKDQD, out tempUserEnrollNumber, out tempDateTime);
					XL.TaoTableGioKDQD(listNV, m_Bang_GioKDQD);
					SetCurrRow(dgrdGioKDQD, tempUserEnrollNumber, tempDateTime);
					GetCurrRow(dgrdGioThieuCheck, out tempUserEnrollNumber, out tempDateTime);
					XL.TaoTableGioThieuCheck(listNV, m_Bang_GioThieuCheck);
					SetCurrRow(dgrdGioThieuCheck, tempUserEnrollNumber, tempDateTime);
					XL.TaoTableThK_TreSom(listNV, m_Bang_ThK_TreSom);
					VeLaiDataGrid(dgrdTongHop);
					VeLaiDataGrid(dgrdGioKDQD);
					VeLaiDataGrid(dgrdGioThieuCheck);
					VeLaiDataGrid(dgrdThKTreSom);
					dgrdTongHop.Invalidate();
					dgrdGioKDQD.Invalidate();
					dgrdGioThieuCheck.Invalidate();
					dgrdThKTreSom.Invalidate();
				}
			}
		}

		private bool GetCurrRow(DataGridView dataGrid, out int UserEnrollNumber, out DateTime Ngay) {
			UserEnrollNumber = -1;
			Ngay = DateTime.MinValue;
			if (dataGrid.SelectedRows.Count == 0) return false;
			var currRow = dataGrid.SelectedRows[0].DataBoundItem as DataRowView;
			if (currRow != null) {
				UserEnrollNumber = (int)(currRow)["UserEnrollNumber"];
				Ngay = (DateTime)(currRow)["TimeStrNgay"];
			}
			return true;
		}

		private void SetCurrRow(DataGridView dataGrid, int UserEnrollNumber, DateTime ngay) {
			if (dataGrid.Rows.Count == 0 || UserEnrollNumber == -1 || ngay == DateTime.MinValue) return;

			var currRow = (from DataGridViewRow row in dataGrid.Rows select row)
				.FirstOrDefault((item) => (int)(item.DataBoundItem as DataRowView).Row["UserEnrollNumber"] == UserEnrollNumber
					&& (DateTime)(item.DataBoundItem as DataRowView).Row["TimeStrNgay"] == ngay);
			if (currRow == null) return;
			else {
				dataGrid.Rows[currRow.Index].Selected = true;
				dataGrid.FirstDisplayedScrollingRowIndex = currRow.Index;
				dataGrid.Refresh();
			}
		}

		private void btnTim_Click(object sender, EventArgs e) {
			if (dgrdDSNVTrgPhg.DataSource == null) return;

			var searchStr1 = tbSearch.Text;
			var searchStr = string.Format("UserFullName like '%{0}%' or UserFullCode like '%{0}%'", searchStr1);
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = searchStr;
		}

		private void linkHienThiTatCaNV_Click(object sender, EventArgs e) {
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = string.Empty;
		}


		private void btnXNCa_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(dtpNgayBD.Value.Date, dtpNgayKT.Value.Date)) {
				MessageBox.Show(string.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "xem công", "xác nhận ca", "xem công"),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}

			#endregion

			// xác định đang chọn datagrid nào để lấy các datarow của các datagrid đó
			DataGridView currDataGrid = XacDinhDataGridViewDangChon();

			// lọc lấy các datarow được check, nếu không có row nào được check thì chế độ view all, ngược lại thì xem các ngày được check
			if (currDataGrid.SelectedRows.Count != 0) {
				var arrRecord = (from DataGridViewRow row in currDataGrid.SelectedRows
								 select (((DataRowView)row.DataBoundItem).Row)).ToArray();

				if (arrRecord.Length == 0) return;

				frm_XN_Ca frm112 = new frm_XN_Ca {
					StartPosition = FormStartPosition.CenterParent,
					m_arrRecd = arrRecord
				};
				//frm112.Location = new Point((int)((Size.Width - frm112.Size.Width) / 2f), (int)((Size.Height - frm112.Size.Height) / 2f));
				//frm112.m_DSNV = m_DSNV;
				frm112.ShowDialog();

				// lấy dsnv đang chọn
				var listNV = (from DataGridViewRow dataGridViewRow in dgrdDSNVTrgPhg.Rows
							  let rowView = dataGridViewRow.DataBoundItem as DataRowView
							  where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
							  select (cUserInfo)rowView["cUserInfo"])
							  .ToList();

				if (frm112.IsReload) {
					Reload4DataGrid(listNV);
				}
			}

		}

		private void dgrd_ColumnHeaderClick_ToSort(object sender, DataGridViewCellMouseEventArgs e) {
			VeLaiDataGrid(dgrdTongHop);
			VeLaiDataGrid(dgrdGioKDQD);
			VeLaiDataGrid(dgrdGioThieuCheck);
			VeLaiDataGrid(dgrdThKTreSom);
		}

		private void btnThemKBVang_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(dtpNgayBD.Value.Date, dtpNgayKT.Value.Date)) {
				MessageBox.Show(string.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "xem công", "khai báo vắng", "xem công"),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}
			#endregion

			// xác định đang chọn datagrid nào để lấy các datarow của các datagrid đó
			DataGridView currDataGrid = XacDinhDataGridViewDangChon();

			if (currDataGrid.SelectedRows.Count != 0) {
				IEnumerable<dynamic> arrRecord = (from DataGridViewRow row in currDataGrid.SelectedRows
												  let rowViews = (DataRowView)row.DataBoundItem
												  select new { MaCC = (int)rowViews["UserEnrollNumber"], NgayVang = (DateTime)rowViews["TimeStrNgay"] }).ToList();

				if (arrRecord.Any() == false) return;

				frm_KBVang_Nhanh frm = new frm_KBVang_Nhanh {
					StartPosition = FormStartPosition.CenterParent,
					listMaCC_NgayVang = arrRecord
				};
				frm.ShowDialog();
				if (frm.IsReload) {
					btnXem.PerformClick();
				}


			}
		}

		private void btnXoaKBVang_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(dtpNgayBD.Value.Date, dtpNgayKT.Value.Date)) {
				MessageBox.Show(string.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "xem công", "xoá khai báo vắng", "xem công"),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}

			#endregion

			DataGridView currDataGrid = XacDinhDataGridViewDangChon();

			if (MessageBox.Show("Xoá các khai báo vắng của nhân viên với các ngày được chọn?", Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) return;
			if (currDataGrid.SelectedRows.Count != 0) {
				IEnumerable<dynamic> arrRecord = (from DataGridViewRow row in currDataGrid.SelectedRows
												  let rowViews = (DataRowView)row.DataBoundItem
												  select new { MaCC = (int)rowViews["UserEnrollNumber"], NgayVang = (DateTime)rowViews["TimeStrNgay"] }).ToList();

				if (arrRecord.Any() == false) return;

				string query = " delete from Absent where UserEnrollNumber = @UserEnrollNumber and TimeDate = @TimeDate ";
				foreach (dynamic obj in arrRecord) {
					int kq = SqlDataAccessHelper.ExecNoneQueryString(
						query,
						new string[] { "@UserEnrollNumber", "@TimeDate" },
						new object[] { obj.MaCC, obj.NgayVang });
					DAO5.GhiNhatKyThaotac("Xoá các khai báo vắng trong ngày",
							string.Format("Xoá tất cả khai báo vắng của NV có mã chấm công [{0}] trong ngày [{1}]", (int)obj.MaCC, ((DateTime)obj.NgayVang).ToString("dd/MM/yyyy")), maCC: (int)obj.MaCC);

				}
				btnXem.PerformClick();
			}
		}

		private void btnKetCongThang_Click(object sender, EventArgs e) {
			if (XL2.QuyenThaoTac.Any(o => o == (int)Quyen.KetCongThang) == false) {
				ACMessageBox.Show(Resources.Text_KoCoQuyen, Resources.Caption_ThongBao, 3000);
				return;
			}

			//muốn kết công bộ phận thì phải kiểm tra kết công tháng(thông số kết lương tháng)
			frm_KetCongBoPhan frm = new frm_KetCongBoPhan { StartPosition = FormStartPosition.CenterParent };
			frm.ShowDialog();
		}

		private void tbSearch_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter)
				btnTim.PerformClick();
		}


	}
}
