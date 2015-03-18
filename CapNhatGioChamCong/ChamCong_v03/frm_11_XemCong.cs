using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using log4net;

namespace ChamCong_v03 {
	public partial class frm_11_XemCong : Form {
		public readonly ILog lg = LogManager.GetLogger("frm_11_XemCong");

		public List<int> m_listIDPhongBan = new List<int>();
		public List<cUserInfo> m_DSNV = new List<cUserInfo>();
		public DataTable m_Bang_DSNV;

		public DataTable TaoBang_DSNV() {
			var kq = XL.TaoCauTrucDataTable( 
				new[] { "check", "cUserInfo", "UserEnrollNumber", "UserFullCode", "UserFullName", "SchID", "SchName", "TitleName", "IDD_1", "Description_1", "HeSoLuongCB", "HeSoLuongSP", "HSBHCongThem" },
				new[] { typeof(bool), typeof(cUserInfo), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(double), typeof(double), typeof(double) });
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
			kq.Columns.Add("cChkInOut1", typeof(cChkInOut));
			kq.Columns.Add("cChkInOut2", typeof(cChkInOut));
			kq.Columns.Add("cChkInOut3", typeof(cChkInOut));
			kq.Columns.Add("TongTre", typeof(int)); //18
			kq.Columns.Add("TongSom", typeof(int)); //19
			kq.Columns.Add("TongCong", typeof(Single)); //20
			kq.Columns.Add("TongPhuCap", typeof(Single)); //22
			kq.Columns.Add("TongGioLam", typeof(Single));
			kq.Columns.Add("TongGioThuc", typeof(Single));
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
			kq.Columns.Add("cChkInOut1", typeof(cChkInOut));
			kq.Columns.Add("cChkInOut2", typeof(cChkInOut));
			kq.Columns.Add("cChkInOut3", typeof(cChkInOut));
			kq.Columns.Add("TongTre", typeof(int)); //18
			kq.Columns.Add("TongSom", typeof(int)); //19
			kq.Columns.Add("TongCong", typeof(Single)); //20
			kq.Columns.Add("TongPhuCap", typeof(Single)); //22
			kq.Columns.Add("TongGioLam", typeof(Single));
			kq.Columns.Add("TongGioThuc", typeof(Single));
			kq.Columns.Add("cNgayCong", typeof(cNgayCong));
			kq.Columns.Add("cUserInfo", typeof(cUserInfo));
			kq.Columns.Add("IsEdited", typeof(bool));
			kq.Columns.Add("TinhPCTC", typeof(bool));
			kq.Columns.Add("TreSomTinhCV1", typeof(bool));
			kq.Columns.Add("TreSomTinhCV2", typeof(bool));
			kq.Columns.Add("TreSomTinhCV3", typeof(bool));

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

		public frm_11_XemCong() {
			InitializeComponent();
			log4net.Config.XmlConfigurator.Configure();

			m_listIDPhongBan = new List<int>();
			m_DSNV = new List<cUserInfo>();
			m_Bang_DSNV 		  = TaoBang_DSNV();
			m_Bang_TongHopXemCong = TaoBang_TongHopXemCong();
			m_Bang_GioKDQD 		  = TaoBang_GioKDQD();
			m_Bang_GioThieuCheck  = TaoBang_GioThieuCheck();
			m_Bang_ThK_TreSom     = TaoBang_ThK_TreSom();

			//1. không cho autogen các column khi bind dữ liệu: 4 cái
			dgrdTongHop.AutoGenerateColumns = dgrdGioKDQD.AutoGenerateColumns = dgrdGioThieuCheck.AutoGenerateColumns = dgrdThKTreSom.AutoGenerateColumns
				= dgrdDSNVTrgPhg.AutoGenerateColumns = false;
			dgrdTongHop.DataSource 		 = m_Bang_TongHopXemCong;
			dgrdGioKDQD.DataSource 		 = m_Bang_GioKDQD;
			dgrdGioThieuCheck.DataSource = m_Bang_GioThieuCheck;
			dgrdThKTreSom.DataSource     = m_Bang_ThK_TreSom;
			DataView dataView = new DataView(m_Bang_DSNV);
			dgrdDSNVTrgPhg.DataSource = dataView;


			var today = DateTime.Today;
			dtpNgayBD.Value = new DateTime(today.Year, today.Month, 1);
			dtpNgayKT.Value = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));

			//3. vẽ 3 checkbox checkall cho DSNV trong phòng
			XL2.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));

		}

		private void frm_XemCong_Load(object sender, EventArgs e) {

			var tablePhong = DAL.LayDSPhong(XL2.currUserID);
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
			XL.TaoTableDSNV(m_DSNV, m_Bang_DSNV);
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			var Source = new AutoCompleteStringCollection();
			Source.AddRange((from nv in m_DSNV select nv.TenNV.ToUpperInvariant()).ToArray());
			tbSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tbSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			tbSearch.AutoCompleteCustomSource = Source;
			dataView.RowFilter = string.Empty;
			m_Bang_TongHopXemCong.Rows.Clear();
			m_Bang_GioKDQD.Rows.Clear();
			m_Bang_GioThieuCheck.Rows.Clear();
			m_Bang_ThK_TreSom.Rows.Clear();
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
			var ngayBD_Bef2D = ngayBD.AddDays(-2d);
			var ngayKT_Aft2D = ngayKT.AddDays(2d);
			// ngày 1->5 là trừ ra = 4, + 1 để ra số ngày, + thêm 2 để ra cột(UserEnrollNumber và số lượng)

			if ((ngayKT - ngayBD).Duration() > new TimeSpan(62,0,0,0))
			{
				if (MessageBox.Show("Khoảng thời gian xem công quá dài, dữ liệu chấm công từ server lớn có thể làm tràn bộ nhớ. \nBạn muốn tiếp tục?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
				{
					return;
				}
			}
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

			m_Bang_TongHopXemCong.Rows.Clear();
			m_Bang_GioKDQD.Rows.Clear();
			m_Bang_GioThieuCheck.Rows.Clear();
			m_Bang_ThK_TreSom.Rows.Clear();
			//3. lấy dữ liệu chấm công của các nhân viên
			try {
				if (XL.KiemtraDulieuCapnhatTuServer(DateTime.Now) == false) { 
					AutoClosingMessageBox.Show("Dữ liệu chấm công chưa được cập nhật mới nhất từ các máy chấm công.\nCác thay đổi giờ chấm công có thể làm sai sót giờ chấm công thực tế khi dữ liệu được cập nhật.", "Thông báo", 4000);
				}
				XL.XemCong(listNV, ngayBD_Bef2D, ngayKT_Aft2D);
				XL.TaoTableXemCong(listNV, m_Bang_TongHopXemCong);
				XL.TaoTableGioKDQD(listNV, m_Bang_GioKDQD);
				XL.TaoTableGioThieuCheck(listNV, m_Bang_GioThieuCheck);
				XL.TaoTableThK_TreSom(listNV, m_Bang_ThK_TreSom);
			} catch (Exception exception) {
				lg.Error("Xem Cong", exception);
				MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại sau.", "Lỗi");
				GC.Collect();
				return;
			}

		}

		private void btnXNCaVaLamThem_Click(object sender, EventArgs e) {
			// xác định đang chọn datagrid nào để lấy các datarow của các datagrid đó
			DataGridView currDataGrid;
			if (tabControlChiTiet.SelectedTab == tabTongHop) currDataGrid = dgrdTongHop;
			else if (tabControlChiTiet.SelectedTab == tabGioKDQD) currDataGrid = dgrdGioKDQD;
			else if (tabControlChiTiet.SelectedTab == tabGioThieuCheck) currDataGrid = dgrdGioThieuCheck;
			else if (tabControlChiTiet.SelectedTab == tabThK_TreSom) currDataGrid = dgrdThKTreSom;
			else {
				currDataGrid = null;
				return;
			}

			// lọc lấy các datarow được check, nếu không có row nào được check thì chế độ view all, ngược lại thì xem các ngày được check
			var table = currDataGrid.DataSource as DataTable;
			if (table == null) return;
			var arrRecord = table.Select("", "UserEnrollNumber asc");

			if (arrRecord.Length == 0) return;

			var frm112 = new frm_112_XacNhanTangCa();
			frm112.m_DSNV = m_DSNV;
			frm112.Location = new Point((int)((this.Size.Width - frm112.Size.Width) / 2f), (int)((this.Size.Height - frm112.Size.Height) / 2f));
			frm112.ShowDialog();

			// sau khi xác nhận thì reload lại và tô màu 2 dataGrid tổng hợp và giờ KDQD
			IEnumerable<DataGridViewRow> lstGridViewRow = dgrdDSNVTrgPhg.Rows.Cast<DataGridViewRow>();
			var listDataRow = from row in (lstGridViewRow) select row.DataBoundItem as DataRowView;
			var listNV = (from rowView in listDataRow
						  where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
						  select (cUserInfo)rowView["cUserInfo"]).ToList();

			if (frm112.IsReload) {
				m_Bang_TongHopXemCong.Rows.Clear();
				m_Bang_GioKDQD.Rows.Clear();
				m_Bang_GioThieuCheck.Rows.Clear();
				m_Bang_ThK_TreSom.Rows.Clear();
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

		}

		private void btnXuatBaoBieu_Click(object sender, EventArgs e) {
			#region lấy ngày BD và kết thúc, và update lại Ngày BD = một ngày trước 31/08 12:00 AM, ngày KT là 1 ngày sau ngay 1 23:59:59
			dtpNgayBD.Update(); dtpNgayKT.Update();
			DateTime ngayBD_Bef2D = dtpNgayBD.Value.Date;
			DateTime ngayKT_Aft2D = dtpNgayKT.Value.Date;
			ngayBD_Bef2D = ngayBD_Bef2D.AddDays(-2d);
			ngayKT_Aft2D = ngayKT_Aft2D.AddDays(2d);
			#endregion

			IEnumerable<DataGridViewRow> lstGridViewRow = dgrdDSNVTrgPhg.Rows.Cast<DataGridViewRow>();
			var listNV = (from row in (lstGridViewRow)
						  where ((DataRowView)row.DataBoundItem)["check"] != DBNull.Value && (bool)((DataRowView)row.DataBoundItem)["check"]
						  select (cUserInfo)((DataRowView)row.DataBoundItem).Row["cUserInfo"]).ToList();
			if (listNV.Count == 0) {
				AutoClosingMessageBox.Show("Bạn chưa chọn Nhân viên", "Thông báo", 2000);
				GC.Collect();
				return;
			}

			frm_XuatBBCongPC frm = new frm_XuatBBCongPC();
			frm.m_dsnv = listNV;
			frm.ngayBD_Bef2D = ngayBD_Bef2D;
			frm.ngayKT_Aft2D = ngayKT_Aft2D;
			frm.Location = new Point((int)((this.Size.Width - frm.Size.Width) / 2f), (int)((this.Size.Height - frm.Size.Height) / 2f));

			frm.ShowDialog();
		}

		private void btnThemXoaSuaDonGian_Click(object sender, EventArgs e) {

			#region xác định datagrid nào đang chọn thì lấy dòng SelectedRows[0] của datagrid đó

			DataGridView currDataGrid;
			if (tabControlChiTiet.SelectedTab == tabTongHop) currDataGrid = dgrdTongHop;
			else if (tabControlChiTiet.SelectedTab == tabGioKDQD) currDataGrid = dgrdGioKDQD;
			else if (tabControlChiTiet.SelectedTab == tabGioThieuCheck) currDataGrid = dgrdGioThieuCheck;
			else if (tabControlChiTiet.SelectedTab == tabThK_TreSom) currDataGrid = dgrdThKTreSom;
			else return;

			#endregion


			if (currDataGrid.SelectedRows.Count == 0) return;
			var arrRecord = ((DataRowView)((currDataGrid.SelectedRows[0]).DataBoundItem)).Row;

			// gọi form editTime và truyền dòng dữ liệu đang chọn sang cho form đó
			var frm1 = new frm_113_ThemXoaSuaGioCC();
			frm1.selectedRow = arrRecord;
			frm1.Location = new Point((int)((this.Size.Width - frm1.Size.Width) / 2f), (int)((this.Size.Height - frm1.Size.Height) / 2f));
			frm1.ShowDialog();

			#region // sau khi edit giờ chấm công xong thì reload lại và tô màu 2 dataGrid tổng hợp và giờ KDQD

			if (frm1.IsReload) {
				IEnumerable<DataGridViewRow> lstGridViewRow = dgrdDSNVTrgPhg.Rows.Cast<DataGridViewRow>();
				var listDataRow = from row in (lstGridViewRow) select row.DataBoundItem as DataRowView;
				var listNV = (from rowView in listDataRow
							  where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
							  select (cUserInfo)rowView["cUserInfo"]).ToList();

				m_Bang_TongHopXemCong.Rows.Clear();
				m_Bang_GioKDQD.Rows.Clear();
				m_Bang_GioThieuCheck.Rows.Clear();
				m_Bang_ThK_TreSom.Rows.Clear();
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

			#endregion

		}

		private void VeLaiDataGrid(DataGridView dataGridView) {
			IEnumerable<DataGridViewRow> lstGridViewRow = dataGridView.Rows.Cast<DataGridViewRow>();
			var datagridviewrow = from row1 in lstGridViewRow
								  where (
											(((DataRowView)row1.DataBoundItem).Row)["IsEdited"] != DBNull.Value &&
											(bool)(((DataRowView)row1.DataBoundItem).Row)["IsEdited"])
								  select row1;
			foreach (var dataGridViewRow in datagridviewrow) {
				dataGridViewRow.DefaultCellStyle.BackColor = Color.Aquamarine;
			}

		}


		private void btnThemXoaSuaHangLoat_Click(object sender, EventArgs e) {
			#region xác định datagrid nào đang chọn thì lấy dòng SelectedRows[0] của datagrid đó

			DataGridView currDataGrid;
			if (tabControlChiTiet.SelectedTab == tabTongHop) currDataGrid = dgrdTongHop;
			else if (tabControlChiTiet.SelectedTab == tabGioKDQD) currDataGrid = dgrdGioKDQD;
			else if (tabControlChiTiet.SelectedTab == tabGioThieuCheck) currDataGrid = dgrdGioThieuCheck;
			else if (tabControlChiTiet.SelectedTab == tabThK_TreSom) currDataGrid = dgrdThKTreSom;
			else return;

			#endregion

			IEnumerable<DataGridViewRow> lstGridViewRow = currDataGrid.SelectedRows.Cast<DataGridViewRow>();
			var arrRecord = (from row in lstGridViewRow
							 select ((DataRowView)row.DataBoundItem).Row).ToArray();

			if (arrRecord.Length == 0) return;

			var frm1 = new frm_113_ThemXoaSuaGioHangLoat();
			frm1.selectedRow = arrRecord;
			frm1.Location = new Point((int)((this.Size.Width - frm1.Size.Width) / 2f), (int)((this.Size.Height - frm1.Size.Height) / 2f));
			frm1.ShowDialog();
			// sau khi edit giờ chấm công xong thì reload lại và tô màu 2 dataGrid tổng hợp và giờ KDQD
			if (frm1.IsReload) {
				IEnumerable<DataGridViewRow> lstGridViewRow_DSNV_Reload = dgrdDSNVTrgPhg.Rows.Cast<DataGridViewRow>();
				var listDataRow = from row in (lstGridViewRow_DSNV_Reload) select row.DataBoundItem as DataRowView;
				var listNV = (from rowView in listDataRow
							  where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
							  select (cUserInfo)rowView["cUserInfo"]).ToList();

				m_Bang_TongHopXemCong.Rows.Clear();
				m_Bang_GioKDQD.Rows.Clear();
				m_Bang_GioThieuCheck.Rows.Clear();
				m_Bang_ThK_TreSom.Rows.Clear();
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


		}

		private void btnXacNhanPC100_Click(object sender, EventArgs e) {
			// xác định đang chọn datagrid nào để lấy các datarow của các datagrid đó
			DataGridView currDataGrid;
			if (tabControlChiTiet.SelectedTab == tabTongHop) currDataGrid = dgrdTongHop;
			else if (tabControlChiTiet.SelectedTab == tabGioKDQD) currDataGrid = dgrdGioKDQD;
			else if (tabControlChiTiet.SelectedTab == tabGioThieuCheck) currDataGrid = dgrdGioThieuCheck;
			else if (tabControlChiTiet.SelectedTab == tabThK_TreSom) currDataGrid = dgrdThKTreSom;
			else {
				currDataGrid = null;
				return;
			}

			// lọc lấy các datarow được check, nếu không có row nào được check thì chế độ view all, ngược lại thì xem các ngày được check
			if (currDataGrid.SelectedRows.Count != 0) {
				var arrRecord = (from row in currDataGrid.SelectedRows.Cast<DataGridViewRow>()
								 select (((DataRowView)row.DataBoundItem).Row)).ToArray();

				if (arrRecord.Length == 0) return;

				frm_112_XacNhanPC100 frm112 = new frm_112_XacNhanPC100();
				frm112.m_arrRecd = arrRecord;
				frm112.Location = new Point((int)((this.Size.Width - frm112.Size.Width) / 2f), (int)((this.Size.Height - frm112.Size.Height) / 2f));
				//frm112.m_DSNV = m_DSNV;
				frm112.ShowDialog();

				// sau khi xác nhận thì reload lại và tô màu 2 dataGrid tổng hợp và giờ KDQD
				IEnumerable<DataGridViewRow> lstGridViewRow = dgrdDSNVTrgPhg.Rows.Cast<DataGridViewRow>();
				var listDataRow = from row in (lstGridViewRow) select row.DataBoundItem as DataRowView;
				var listNV = (from rowView in listDataRow
							  where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
							  select (cUserInfo)rowView["cUserInfo"]).ToList();

				if (frm112.IsReload) {
					m_Bang_TongHopXemCong.Rows.Clear();
					m_Bang_GioKDQD.Rows.Clear();
					m_Bang_GioThieuCheck.Rows.Clear();
					m_Bang_ThK_TreSom.Rows.Clear();
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
			}
		}

		private void btnXNPCTC_Click(object sender, EventArgs e) {
			DataGridView currDataGrid;
			if (tabControlChiTiet.SelectedTab == tabTongHop) currDataGrid = dgrdTongHop;
			else if (tabControlChiTiet.SelectedTab == tabGioKDQD) currDataGrid = dgrdGioKDQD;
			else if (tabControlChiTiet.SelectedTab == tabGioThieuCheck) currDataGrid = dgrdGioThieuCheck;
			else if (tabControlChiTiet.SelectedTab == tabThK_TreSom) currDataGrid = dgrdThKTreSom;
			else {
				currDataGrid = null;
				return;
			}

			// lọc lấy các datarow được check, nếu không có row nào được check thì chế độ view all, ngược lại thì xem các ngày được check
			if (currDataGrid.SelectedRows.Count != 0) {
				var arrRecord = (from row in currDataGrid.SelectedRows.Cast<DataGridViewRow>()
								 select (((DataRowView)row.DataBoundItem).Row)).ToArray();

				if (arrRecord.Length == 0) return;

				frm_112_XacNhanPC50 frm112 = new frm_112_XacNhanPC50();
				frm112.m_arrRecd = arrRecord;
				frm112.Location = new Point((int)((this.Size.Width - frm112.Size.Width) / 2f), (int)((this.Size.Height - frm112.Size.Height) / 2f));
				//frm112.m_DSNV = m_DSNV;
				frm112.ShowDialog();

				// sau khi xác nhận thì reload lại và tô màu 2 dataGrid tổng hợp và giờ KDQD
				IEnumerable<DataGridViewRow> lstGridViewRow = dgrdDSNVTrgPhg.Rows.Cast<DataGridViewRow>();
				var listDataRow = from row in (lstGridViewRow) select row.DataBoundItem as DataRowView;
				var listNV = (from rowView in listDataRow
							  where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
							  select (cUserInfo)rowView["cUserInfo"]).ToList();

				if (frm112.IsReload)
				{
					var tempUserEnrollNumber = -1;
					var tempDateTime = DateTime.MinValue;
					GetCurrRow(dgrdTongHop, out tempUserEnrollNumber, out tempDateTime);
					m_Bang_TongHopXemCong.Rows.Clear();
					XL.TaoTableXemCong(listNV, m_Bang_TongHopXemCong);
					SetCurrRow(dgrdTongHop, tempUserEnrollNumber, tempDateTime);
					GetCurrRow(dgrdGioKDQD, out tempUserEnrollNumber, out tempDateTime);
					m_Bang_GioKDQD.Rows.Clear();
					XL.TaoTableGioKDQD(listNV, m_Bang_GioKDQD);
					SetCurrRow(dgrdGioKDQD, tempUserEnrollNumber, tempDateTime);
					GetCurrRow(dgrdGioThieuCheck, out tempUserEnrollNumber, out tempDateTime); 
					m_Bang_GioThieuCheck.Rows.Clear();
					XL.TaoTableGioThieuCheck(listNV, m_Bang_GioThieuCheck);
					SetCurrRow(dgrdGioThieuCheck, tempUserEnrollNumber, tempDateTime);
					m_Bang_ThK_TreSom.Rows.Clear();
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

		private bool GetCurrRow(DataGridView dgrdGioThieuCheck, out int UserEnrollNumber, out DateTime Ngay)
		{
			UserEnrollNumber = -1;
			Ngay = DateTime.MinValue;
			if (dgrdGioThieuCheck.SelectedRows.Count == 0) return false;
			var currRow = dgrdGioThieuCheck.SelectedRows[0];
			UserEnrollNumber =(int)(currRow.DataBoundItem as DataRowView)["UserEnrollNumber"] ;
			Ngay = (DateTime) (currRow.DataBoundItem as DataRowView)["TimeStrNgay"];
			return true;
		}

		private void SetCurrRow(DataGridView dataGrid, int UserEnrollNumber , DateTime ngay	)
		{
			if (dataGrid.Rows.Count == 0 || UserEnrollNumber == -1 || ngay == DateTime.MinValue) return;
			IEnumerable<DataGridViewRow> o = dataGrid.Rows.Cast<DataGridViewRow>();
			var currRow = (from row in o select row)
				.FirstOrDefault((item) => (int) (item.DataBoundItem as DataRowView).Row["UserEnrollNumber"] == UserEnrollNumber 
					&& (DateTime) (item.DataBoundItem as DataRowView).Row["TimeStrNgay"] == ngay);
			if (currRow == null) return;
			else
			{
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
			dataView.RowFilter = searchStr;
			
		}

		private void linkHienThiTatCaNV_Click(object sender, EventArgs e) {
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			dataView.RowFilter = string.Empty;
		}

		private void toolTip1_Draw(object sender, DrawToolTipEventArgs e) {
			Font f = new Font("Arial", 10.0f);
			e.DrawBackground();
			e.DrawBorder();
			e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, new PointF(2f, 2f));     
		}

		private void toolTip1_Popup(object sender, PopupEventArgs e) {
			Size temp = TextRenderer.MeasureText(toolTip1.GetToolTip(e.AssociatedControl), new Font("Arial", 10.0f));
			temp.Width += 4;
			temp.Height += 4;
			e.ToolTipSize = temp;
		}



	}
}
