using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DAL;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;

namespace ChamCong_v04.UI.Admin {
	public partial class frm_PhucHoiGioChamCong : Form {
		private List<int> m_listIDPhongBan = new List<int>();
		private List<cUserInfo> m_DSNV = new List<cUserInfo>();
		private List<cPhongBan> m_DSPhg = new List<cPhongBan>();
		public DataTable m_Bang_DSNV;


		public DataTable TaoBang_DSNV_PhucHoi() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "check", "cUserInfo", "UserEnrollNumber", "UserFullCode", "UserFullName", "SchID", "SchName", "ChucVu", "MaPhong", "TenPhong", "HeSoLuongCB", "HeSoLuongSP", "HSBHCongThem" },
				new[] { typeof(bool), typeof(cUserInfo), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(float), typeof(float), typeof(float) });
			return kq;
		}

		public DataTable m_Bang_PH_Them;

		public DataTable TaoBang_PH_Them() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "UserEnrollNumber", "UserFullCode", "UserFullName", 
					"Type","TimeStr",  "MachineNo","Source", 
					"IDXNCa_LamThem", "CoXN", "Them", "DaXoa"},
				new[] { typeof(int), typeof(string), typeof(string), 
					typeof(string), typeof(DateTime), typeof(int), typeof(string), 
					typeof(int),typeof(bool), typeof(bool), typeof(bool) });
			return kq;
		}

		public DataTable m_Bang_PH_GioGoc;

		public DataTable TaoBang_PH_GioGoc() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "UserEnrollNumber", "UserFullCode", "UserFullName", 
					"CurrType", "CurrTimeStr",  "CurrMachineNo","CurrSource", 
					"OrinType", "OrinTimeStr",  "OrinMachineNo","OrinSource", 
					"IDXNCa_LamThem", "IDGioGoc", "CoXN", "Them"},
				new[] { typeof(int), typeof(string), typeof(string), 
					typeof(string), typeof(DateTime), typeof(int), typeof(string),
					typeof(string), typeof(DateTime), typeof(int), typeof(string),
					typeof(int), typeof(int), typeof(bool), typeof(bool) });
			return kq;
		}

		public DataTable m_Bang_PH_Xoaa;

		public DataTable TaoBang_PH_Xoaa() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "UserEnrollNumber", "UserFullCode", "UserFullName", 
					"Type", "TimeStr",  "MachineNo","Source", "Xoa",},
				new[] { typeof(int), typeof(string), typeof(string), 
					typeof(string), typeof(DateTime), typeof(int), typeof(string), typeof(bool), });
			return kq;
		}

		public DataTable m_Bang_GioDaXN;

		public DataTable TaoBang_GioDaXN() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "UserEnrollNumber", "UserFullCode", "UserFullName", 
					"IDXNCa_LamThem", "TimeStrInn", "TimeStrOut",  "ShiftID",  "ShiftCode","OTMin", "DuyetChoPhepVaoTre","DuyetChoPhepRaSom"},
				new[] { typeof(int), typeof(string), typeof(string), 
					typeof(int),typeof(DateTime), typeof(DateTime), typeof(int), typeof(string), typeof(int), typeof(bool), typeof(bool)});
			return kq;
		}

		#region biến checkbox check all: 1 của DSNV cần xem công. 2 DS Công của NV

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

		public frm_PhucHoiGioChamCong() {
			InitializeComponent();
			log4net.Config.XmlConfigurator.Configure();
			dtpNgayBD.Value = MyUtility.FirstDayOfMonth(DateTime.Today);
			dtpNgayKT.Value = MyUtility.LastDayOfMonth(DateTime.Today);

			m_listIDPhongBan = new List<int>();
			m_DSNV = new List<cUserInfo>();
			m_Bang_DSNV = TaoBang_DSNV_PhucHoi();
			m_Bang_PH_Them = TaoBang_PH_Them();
			m_Bang_GioDaXN = TaoBang_GioDaXN();
			dgrdDSNVTrgPhg.AutoGenerateColumns = dgrdPH_Them.AutoGenerateColumns = dgrdPH_Xoaa.AutoGenerateColumns = dgrdPH_GioGoc.AutoGenerateColumns = dgrdGioDaXN.AutoGenerateColumns = false;

			DataView dataView = new DataView(m_Bang_DSNV);
			dgrdDSNVTrgPhg.DataSource = dataView;

			dgrdPH_Them.DataSource = m_Bang_PH_Them;
			//3. vẽ 3 checkbox checkall cho DSNV trong phòng
			XL2.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));

		}

		private void frm_PhucHoiGioChamCong_Load(object sender, EventArgs e) {
			#region kiểm tra kết nối csdl , nếu mất kết nối thì đóng

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}

			#endregion

			XL.ChuanBiDSLichTrinhVaCa();

			#region //2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan ,
			// trường hợp ko có phòng ban nào được phép thao tác thì báo và thoát form

			XL.KhoiTaoDSPhongBan(m_DSPhg); // tạo danh sách phòng ban chỉ userID này được thao tác
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

		}

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

			checkAll_GridDSNV.Checked = false;
		}


		private void btnXem_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;
			//1. lấy dữ liệu từ form

			#region lấy ngày BD và kết thúc nếu quá 62 ngày thì báo

			dtpNgayBD.Update();
			dtpNgayKT.Update();
			var ngayBD = dtpNgayBD.Value.Date;
			var ngayKT = dtpNgayKT.Value.Date;
			#endregion


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
			XL.LayBang_PH_Them(listNV, ngayBD, ngayKT, ref m_Bang_PH_Them);
			XL.LayBang_PH_Xoaa(listNV, ngayBD, ngayKT, ref m_Bang_PH_Xoaa);
			XL.LayBang_PH_GioGoc(listNV, ngayBD, ngayKT, ref m_Bang_PH_GioGoc);
			XL.LayBang_GioDaXN(listNV, ngayBD, ngayKT, ref m_Bang_GioDaXN);
			dgrdPH_Them.DataSource = m_Bang_PH_Them;
			dgrdPH_Xoaa.DataSource = m_Bang_PH_Xoaa;
			dgrdPH_GioGoc.DataSource = m_Bang_PH_GioGoc;
			dgrdGioDaXN.DataSource = m_Bang_GioDaXN;
		}

		private void btnPhucHoi_Click(object sender, EventArgs e) {
			#region hỏi lại trước khi thực hiện
			#endregion 
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			if (tabControl1.SelectedTab == tabDSGThem) {
				IEnumerable<DataGridViewRow> lstGridViewRow = dgrdPH_Them.SelectedRows.Cast<DataGridViewRow>();
				var listRowView = (from row in (lstGridViewRow)
								   select ((DataRowView)row.DataBoundItem)).ToList();
				PhucHoiGioThem(listRowView);
			}
			else if (tabControl1.SelectedTab == tabDSGXoaa) {
				IEnumerable<DataGridViewRow> lstGridViewRow = dgrdPH_Xoaa.SelectedRows.Cast<DataGridViewRow>();
				var listRowView = (from row in (lstGridViewRow)
								   select ((DataRowView)row.DataBoundItem)).ToList();
				PhucHoiGioXoa(listRowView);
			}
			else if (tabControl1.SelectedTab == tabDSGSuaa) {
				IEnumerable<DataGridViewRow> lstGridViewRow = dgrdPH_GioGoc.SelectedRows.Cast<DataGridViewRow>();
				var listRowView = (from row in (lstGridViewRow)
								   select ((DataRowView)row.DataBoundItem)).ToList();
				PhucHoiGioGoc(listRowView);
			}
			Thread.Sleep(20);
			btnXem.PerformClick();
		}

		private void PhucHoiGioThem(List<DataRowView> listRowView) {
			if (listRowView.Count == 0) return; 
			foreach (DataRowView rowView in listRowView)
			{
				var UserEnrollNumber = (int)rowView["UserEnrollNumber"];
				var MachineNo = (int)rowView["MachineNo"];
				var TimeStr = (DateTime) rowView["TimeStr"];
				var kq = DAO.PhucHoiGioThem(UserEnrollNumber, MachineNo, TimeStr);
				if (kq == 0)
				{
					MessageBox.Show("Xảy ra lỗi trong quá trình thao tác. Vui lòng thử lại sau.");
					break;
				}
			}
		}

		private void PhucHoiGioXoa(List<DataRowView> listRowView) {
			if (listRowView.Count == 0) return; 
			foreach (DataRowView rowView in listRowView)
			{
				var UserEnrollNumber = (int)rowView["UserEnrollNumber"];
				var MachineNo = (int)rowView["MachineNo"];
				var TimeStr = (DateTime) rowView["TimeStr"];
				var kq = DAO.PhucHoiGioXoa(UserEnrollNumber, MachineNo, TimeStr) ;
				if (kq == 0)
				{
					MessageBox.Show("Xảy ra lỗi trong quá trình thao tác. Vui lòng thử lại sau.");
					break;
				}
			}
		}

		private void PhucHoiGioGoc(List<DataRowView> listRowView) {
			if (listRowView.Count == 0) return;
			foreach (DataRowView rowView in listRowView)
			{
				var UserEnrollNumber = (int)rowView["UserEnrollNumber"];
				var CurrMachineNo = (int)rowView["CurrMachineNo"];
				var CurrTimeStr = (DateTime) rowView["CurrTimeStr"];
				var CurrSource = rowView["CurrSource"].ToString();
				var OrinMachineNo = (int) rowView["OrinMachineNo"];
				var OrinTimeStr = (DateTime) rowView["OrinTimeStr"];
				var OrinSource = rowView["OrinSource"].ToString();
				var IDGioGoc = (int) rowView["IDGioGoc"];
				var kq = DAO.PhucHoiGioGoc(UserEnrollNumber, CurrMachineNo, CurrTimeStr, CurrSource, OrinMachineNo, OrinTimeStr, OrinSource, IDGioGoc);
				if (kq == 0)
				{
					MessageBox.Show("Xảy ra lỗi trong quá trình thao tác. Vui lòng thử lại sau.");
					break;
				}
			}
		}

		private void btnHuyXN_Click(object sender, EventArgs e) {
			#region hỏi lại trước khi thực hiện
			#endregion
			if (XL2.KiemtraKetnoiCSDL() == false) return;
			if (tabControl1.SelectedTab == tabDSG_DaXN) {
				IEnumerable<DataGridViewRow> lstGridViewRow = dgrdGioDaXN.SelectedRows.Cast<DataGridViewRow>();
				var listRowView = (from row in (lstGridViewRow)
								   select ((DataRowView)row.DataBoundItem)).ToList();
				foreach (var rowView in listRowView)
				{
					var ID = (int) rowView["IDXNCa_LamThem"];
					XL.HuyXN_GioChamCong(ID);
				}
				Thread.Sleep(20);
				btnXem.PerformClick();
			}

		}

		private void tbSearch_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter)
				btnTim.PerformClick();

		}


	}
}
