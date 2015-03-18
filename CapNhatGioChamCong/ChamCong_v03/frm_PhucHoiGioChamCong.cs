using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;

namespace ChamCong_v03 {
	public partial class frm_PhucHoiGioChamCong : Form {
		public List<int> m_listIDPhongBan = new List<int>();
		public List<cUserInfo> m_DSNV = new List<cUserInfo>();
		public DataTable m_Bang_DSNV;

		public DataTable TaoBang_DSNV_PhucHoi() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "check", "cUserInfo", "UserEnrollNumber", "UserFullCode", "UserFullName", "TitleName", "IDD_1", "Description_1", "HeSoLuongCB", "HeSoLuongSP", "HSBHCongThem" },
				new[] { typeof(bool), typeof(cUserInfo), typeof(int), typeof(string), typeof(string), typeof(string), typeof(int), typeof(string), typeof(double), typeof(double), typeof(double) });
			return kq;
		}

		public DataTable m_Bang_PH_Them;

		public DataTable TaoBang_PH_Them() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "UserEnrollNumber", "UserFullCode", "UserFullName", 
					"Type","TimeStr",  "MachineNo","Source", 
					"IDXacNhanCaVaLamThem", "CoXN", "Them", "DaXoa"},
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
					"IDXacNhanCaVaLamThem", "IDGioGoc", "CoXN", "Them"},
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
					"IDXacNhanCaVaLamThem", "TimeStrInn", "TimeStrOut",  "ShiftID",  "ShiftCode","OTMin"},
				new[] { typeof(int), typeof(string), typeof(string), 
					typeof(int),typeof(DateTime), typeof(DateTime), typeof(int), typeof(string), typeof(int), });
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
			var tablePhong = DAL.LayDSTatCaPhongBan();
			if (tablePhong.Rows.Count == 0) {
				AutoClosingMessageBox.Show("Bạn chưa được phân quyền thao tác.", "Thông báo", 2000);
				return;
			}
			//2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan : xoá dữ liệu trước và load
			treePhongBan.Nodes.Clear();
			loadTreePhgBan(treePhongBan, tablePhong);

			// đăng ký sự kiện cho tree và chọn topNode
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;
			treePhongBan.SelectedNode = treePhongBan.TopNode;

		}

		#region load treeview new

		public TreeView loadTreePhgBan(TreeView tvDSPhongBan, DataTable pDataTable) {
			if (pDataTable.Rows.Count > 0) {
				foreach (var dataRow in pDataTable.Select("RelationID = 0", "ViTri asc")) {
					var ParentNode = new TreeNode { Text = dataRow["Description"].ToString(), Tag = (int)dataRow["ID"] };
					tvDSPhongBan.Nodes.Add(ParentNode);
					loadTreeSubNode(ref ParentNode, (int)(dataRow["ID"]), pDataTable);
				}
			}
			return tvDSPhongBan;
		}

		private void loadTreeSubNode(ref TreeNode ParentNode, int ParentId, DataTable dtMenu) {
			var childs = dtMenu.Select("RelationID = " + ParentId, "ViTri asc");
			foreach (var dRow in childs) {
				var child = new TreeNode { Text = dRow["Description"].ToString(), Tag = (int)dRow["ID"], ToolTipText = dRow["Description"].ToString() };
				ParentNode.Nodes.Add(child);
				//Recursion Call
				loadTreeSubNode(ref child, (int)dRow["ID"], dtMenu);
			}
		}

		private void GetIDLeafNodeExceptParent(TreeNode root, List<int> listID) {
			if (root == null) return;

			listID.Add((int)root.Tag);

			if (root.Nodes.Count > 0) {
				foreach (TreeNode node in root.Nodes) {
					GetIDLeafNodeExceptParent(node, listID);
				}
			}
			// xuốn đến đây tương đương root.Nodes.Count== 0; return
		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {

			m_listIDPhongBan.Clear();
			if (e.Node.FirstNode != null) GetIDLeafNodeExceptParent(e.Node, m_listIDPhongBan);
			else m_listIDPhongBan.Add((int)e.Node.Tag);
			e.Node.Expand();

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 4000);
				this.Close();
				return;
			}
			var table = DAL.LayDSNV(m_listIDPhongBan.ToArray());
			if (table.Rows.Count == 0) return;
			m_DSNV.Clear();
			XL.KhoiTaoDSNV(m_DSNV, table);
			m_Bang_DSNV.Rows.Clear();
			XL.TaoTableDSNV_PhucHoi(m_DSNV, m_Bang_DSNV);
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			var Source = new AutoCompleteStringCollection();
			Source.AddRange((from nv in m_DSNV select nv.TenNV.ToUpperInvariant()).ToArray());
			tbSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tbSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			tbSearch.AutoCompleteCustomSource = Source;
			dataView.RowFilter = string.Empty;
			checkAll_GridDSNV.Checked = false;
		}

		#endregion

		private void btnXem_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}
			//1. lấy dữ liệu từ form

			#region lấy ngày BD và kết thúc nếu quá 62 ngày thì báo

			dtpNgayBD.Update();
			dtpNgayKT.Update();
			var ngayBD = dtpNgayBD.Value.Date;
			var ngayKT = dtpNgayKT.Value.Date;
			#endregion


			BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();
			//2. lấy danh sách nhân viên check, nếu chưa có nv nào check thì thông báo
			IEnumerable<DataGridViewRow> lstGridViewRow = dgrdDSNVTrgPhg.Rows.Cast<DataGridViewRow>();
			var listNV = (from row in (lstGridViewRow)
						  where ((DataRowView)row.DataBoundItem)["check"] != DBNull.Value && (bool)((DataRowView)row.DataBoundItem)["check"]
						  select (cUserInfo)((DataRowView)row.DataBoundItem).Row["cUserInfo"]).ToList();

			if (listNV.Count == 0) {
				AutoClosingMessageBox.Show("Bạn chưa chọn Nhân viên", "Thông báo", 2000);
				GC.Collect();
				return;
			}
			/*m_Bang_TongHopXemCong.Rows.Clear();
			m_Bang_GioKDQD.Rows.Clear();
			m_Bang_GioThieuCheck.Rows.Clear();*/
			m_Bang_PH_Them.Rows.Clear();
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
			#region kiểm tra kết nối csdl trước khi thực hiện
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}
			#endregion 
			DataGridView currGrid;
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
				var kq = DAL.PhucHoiGioThem(UserEnrollNumber, MachineNo, TimeStr);
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
				var kq = DAL.PhucHoiGioXoa(UserEnrollNumber, MachineNo, TimeStr) ;
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
				var kq = DAL.PhucHoiGioGoc(UserEnrollNumber, CurrMachineNo, CurrTimeStr, CurrSource, OrinMachineNo, OrinTimeStr, OrinSource, IDGioGoc);
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
			#region kiểm tra kết nối csdl trước khi thực hiện
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}
			#endregion
			DataGridView currGrid;
			if (tabControl1.SelectedTab == tabDSG_DaXN) {
				IEnumerable<DataGridViewRow> lstGridViewRow = dgrdGioDaXN.SelectedRows.Cast<DataGridViewRow>();
				var listRowView = (from row in (lstGridViewRow)
								   select ((DataRowView)row.DataBoundItem)).ToList();
				foreach (var rowView in listRowView)
				{
					var ID = (int) rowView["IDXacNhanCaVaLamThem"];
					XL.HuyXN_GioChamCong(ID);
				}
				Thread.Sleep(20);
				btnXem.PerformClick();
			}

		}


	}
}
