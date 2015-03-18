using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v02.BUS;
using ChamCong_v02.DAO;
using ChamCong_v02.DTO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using log4net;

namespace ChamCong_v02 {
	public partial class frm_11_XemCong : Form {
		public readonly ILog lg = LogManager.GetLogger("frm_11_XemCong");

		public List<int> m_listIDPhongBan { get; set; }
		public List<cUserInfo> m_DSNVXemCong { get; set; }

		#region 3 biến checkbox check all: 1 của DSNV cần xem công. 2 DS Công của NV
		readonly CheckBox checkAll_GridDSNV = new CheckBox();
		readonly CheckBox checkAllGridTH = new CheckBox();
		readonly CheckBox checkAllGridKDQD = new CheckBox();
		void checkAll_CheckedChanged(object sender, EventArgs e) {
			//1. xác định datagrid nào đang check all
			DataGridView tempGrid;
			if (sender == checkAll_GridDSNV) tempGrid = dgrdDSNVTrgPhg;
			else if (sender == checkAllGridTH) tempGrid = dgrdTongHop;
			else tempGrid = dgrdGioKDQD;

			// 2. kiểm tra nếu ko có dòng dữ liệu nào thì thoát
			if (tempGrid.Rows.Count == 0) return;

			//3. có dữ liệu, duyệt qua hết và gán giá trị check (or uncheck), sau đó endEDIT lại lưới để dòng đầu tiên có giá trị
			bool tmpCheckAll = ((CheckBox)sender).Checked;

			tempGrid.BeginEdit(true);
			DataTable dt = tempGrid.DataSource as DataTable;

			if (dt != null && dt.Rows.Count != 0) {
				foreach (DataRow row in dt.Rows) {
					row["check"] = tmpCheckAll;
				}
			}

			tempGrid.EndEdit();
			tempGrid.Update();
			//tempGrid.Refresh();
		}

		#endregion

		public frm_11_XemCong() {
			InitializeComponent();
			log4net.Config.XmlConfigurator.Configure();

			//1. không cho autogen các column khi bind dữ liệu: 4 cái
			dgrdDSNVTrgPhg.AutoGenerateColumns = dgrdTongHop.AutoGenerateColumns = dgrdGioKDQD.AutoGenerateColumns = false;

			DateTime today = DateTime.Today;
			dtpNgayBD.Value = new DateTime(today.Year, today.Month, 1);
			dtpNgayKT.Value = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));

			//3. vẽ 3 checkbox checkall cho DSNV trong phòng
			ThamSo.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));
			ThamSo.VeCheckBox_CheckAll(dgrdTongHop, checkAllGridTH, checkAll_CheckedChanged, new Point(7, 10));
			ThamSo.VeCheckBox_CheckAll(dgrdGioKDQD, checkAllGridKDQD, checkAll_CheckedChanged, new Point(7, 10));

		}

		private void frm_XemCong_Load(object sender, EventArgs e) {
			// 1. khởi tạo các biến cục bộ
			m_listIDPhongBan = new List<int>();
			DataTable tablePhong = DAL.LayDSPhong(ThamSo.currUserID);
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
				foreach (DataRow dataRow in pDataTable.Select("RelationID = 0")) {
					TreeNode ParentNode = new TreeNode { Text = dataRow["Description"].ToString(), Tag = (int)dataRow["ID"] };
					tvDSPhongBan.Nodes.Add(ParentNode);
					loadTreeSubNode(ref ParentNode, (int)(dataRow["ID"]), pDataTable);
				}
			}
			return tvDSPhongBan;
		}

		private void loadTreeSubNode(ref TreeNode ParentNode, int ParentId, DataTable dtMenu) {
			DataRow[] childs = dtMenu.Select("RelationID = " + ParentId);
			foreach (DataRow dRow in childs) {
				TreeNode child = new TreeNode { Text = dRow["Description"].ToString(), Tag = (int)dRow["ID"], ToolTipText = dRow["Description"].ToString() };
				ParentNode.Nodes.Add(child);
				//Recursion Call
				loadTreeSubNode(ref child, (int)dRow["ID"], dtMenu);
			}
		}

		void GetIDLeafNodeExceptParent(TreeNode root, List<int> listID) {
			if (root == null) return;
			if (root.FirstNode == null) {
				listID.Add((int)root.Tag);
				//lg.Debug(root.Tag + " " + root.Text);
				GetIDLeafNodeExceptParent(root.NextNode, listID);
			}
			if (root.FirstNode != null) {
				foreach (TreeNode node in root.Nodes) {
					GetIDLeafNodeExceptParent(node, listID);
				}
			}
		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {

			if (m_listIDPhongBan == null) m_listIDPhongBan = new List<int>();
			else m_listIDPhongBan.Clear();
			if (e.Node.FirstNode != null) GetIDLeafNodeExceptParent(e.Node, m_listIDPhongBan);
			else m_listIDPhongBan.Add((int)e.Node.Tag);
			e.Node.Expand();

			DataTable table = DAL.LayDSNV(m_listIDPhongBan.ToArray());
			table.Columns.Add("check", typeof(bool));
			table.Columns["check"].DefaultValue = false;

			dgrdDSNVTrgPhg.DataSource = table;
			checkAll_GridDSNV.Checked = false;
		}

		#endregion
		#region tạo cấu trúc 2 table hiển thị, và 1 cấu trúc table Chi tiết để hiển thị bên form thêm xóa sửa
		private DataTable TaoCauTrucDataTableTongHop() {
			DataTable kq = new DataTable();
			kq.Columns.Add("check", typeof(bool)); //0
			kq.Columns.Add("UserEnrollNumber", typeof(int)); //0
			kq.Columns.Add("UserFullCode", typeof(string)); //0
			kq.Columns.Add("UserFullName", typeof(string)); //1
			kq.Columns.Add("TimeStrNgay", typeof(DateTime)); //2
			kq.Columns.Add("TimeStrThu", typeof(DateTime)); //3
			kq.Columns.Add("TimeStrVao1", typeof(DateTime)); //4
			kq.Columns.Add("TimeStrRa1", typeof(DateTime)); //5
			kq.Columns.Add("TimeStrVao2", typeof(DateTime)); //6
			kq.Columns.Add("TimeStrRa2", typeof(DateTime)); //7
			kq.Columns.Add("TimeStrVao3", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRa3", typeof(DateTime)); //9
			kq.Columns.Add("TimeStrVao4", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRa4", typeof(DateTime)); //9
			kq.Columns.Add("TimeStrVao5", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRa5", typeof(DateTime)); //9
			kq.Columns.Add("ShiftID1", typeof(int)); //8
			kq.Columns.Add("ShiftID2", typeof(int)); //9
			kq.Columns.Add("ShiftID3", typeof(int)); //9
			kq.Columns.Add("ShiftID4", typeof(int)); //9
			kq.Columns.Add("ShiftID5", typeof(int)); //9
			kq.Columns.Add("TimeStrTre", typeof(int)); //18
			kq.Columns.Add("TimeStrSom", typeof(int)); //19
			kq.Columns.Add("Cong", typeof(float)); //20
			kq.Columns.Add("ShiftCode", typeof(string)); //21
			kq.Columns.Add("PhuCap", typeof(float));//22
			kq.Columns.Add("TongGioLam", typeof(float));
			kq.Columns.Add("TongGioThuc", typeof(float));
			kq.Columns.Add("IDD_1", typeof(int));
			kq.Columns.Add("Description_1", typeof(string));
			kq.Columns.Add("IDD_2", typeof(int));
			kq.Columns.Add("Description_2", typeof(string));

			return kq;
		}

		private DataTable TaoCauTrucDataTableKDQD() {
			DataTable kq = new DataTable();
			kq.Columns.Add("check", typeof(bool)); //0
			kq.Columns.Add("UserEnrollNumber", typeof(int)); //0
			kq.Columns.Add("UserFullCode", typeof(string)); //0
			kq.Columns.Add("UserFullName", typeof(string)); //1
			kq.Columns.Add("TimeStrNgay", typeof(DateTime)); //2
			kq.Columns.Add("TimeStrThu", typeof(DateTime)); //3
			kq.Columns.Add("TimeStrVao1", typeof(DateTime)); //4
			kq.Columns.Add("TimeStrRa1", typeof(DateTime)); //5
			kq.Columns.Add("TimeStrVao2", typeof(DateTime)); //6
			kq.Columns.Add("TimeStrRa2", typeof(DateTime)); //7
			kq.Columns.Add("TimeStrVao3", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRa3", typeof(DateTime)); //9
			kq.Columns.Add("TimeStrVao4", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRa4", typeof(DateTime)); //9
			kq.Columns.Add("TimeStrVao5", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRa5", typeof(DateTime)); //9
			kq.Columns.Add("ShiftID1", typeof(int)); //8
			kq.Columns.Add("ShiftID2", typeof(int)); //9
			kq.Columns.Add("ShiftID3", typeof(int)); //9
			kq.Columns.Add("ShiftID4", typeof(int)); //9
			kq.Columns.Add("ShiftID5", typeof(int)); //9
			kq.Columns.Add("TimeStrTre", typeof(int)); //18
			kq.Columns.Add("TimeStrSom", typeof(int)); //19
			kq.Columns.Add("Cong", typeof(float)); //20
			kq.Columns.Add("ShiftCode", typeof(string)); //21
			kq.Columns.Add("PhuCap", typeof(float));//22
			kq.Columns.Add("TongGioLam", typeof(float));
			kq.Columns.Add("TongGioThuc", typeof(float));
			kq.Columns.Add("obj", typeof(cNgayCong));
			kq.Columns.Add("IDD_1", typeof(int));
			kq.Columns.Add("Description_1", typeof(string));
			kq.Columns.Add("IDD_2", typeof(int));
			kq.Columns.Add("Description_2", typeof(string));

			return kq;
		}

		#endregion

		private void btnXem_Click(object sender, EventArgs e) {
			//1. lấy dữ liệu từ form
			#region lấy ngày BD và kết thúc, và update lại Ngày BD = một ngày trước 31/08 12:00 AM, ngày KT là 1 ngày sau ngay 1 23:59:59
			dtpNgayBD.Update(); dtpNgayKT.Update();
			DateTime ngayBD = dtpNgayBD.Value.Date;
			DateTime ngayKT = dtpNgayKT.Value.Date;
			ngayBD = ngayBD.AddDays(-1d);
			ngayKT = ngayKT.AddDays(2d).Subtract(new TimeSpan(0, 0, 1));
			#endregion
			//-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

			if (dgrdDSNVTrgPhg.DataSource != null)
				BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();
			//2. lấy danh sách nhân viên check, nếu chưa có nv nào check thì thông báo
			List<int> dsMaCC_Checked = LayDSMaCC_Checked(dgrdDSNVTrgPhg);
			int[] ArrDSMaCC_Checked = dsMaCC_Checked.ToArray();
			if (dsMaCC_Checked.Count == 0) {
				AutoClosingMessageBox.Show("Chưa chọn Nhân viên", "Thông báo", 2000);
				return;
			}

			#region get data thông tin cá nhân từ bảng dgrdDSNVTrgPhg.DataSource

			DataTable tableUserInfo = dgrdDSNVTrgPhg.DataSource as DataTable;
			if (tableUserInfo == null) return;
			#endregion


			DataTable dataTableTongHop = dgrdTongHop.DataSource as DataTable;
			if (dataTableTongHop == null) dataTableTongHop = TaoCauTrucDataTableTongHop();
			else dataTableTongHop.Rows.Clear();

			DataTable dataTableKDQD = dgrdGioKDQD.DataSource as DataTable;
			if (dataTableKDQD == null) dataTableKDQD = TaoCauTrucDataTableKDQD();
			else dataTableKDQD.Rows.Clear();

			//3. lấy dữ liệu chấm công của các nhân viên
			try {
				m_DSNVXemCong = XL.XemCong2(tableUserInfo, ArrDSMaCC_Checked, ngayBD, ngayKT);
				dataTableTongHop = XL.TaoTableXemCong(m_DSNVXemCong);
			} catch (Exception exception) {
				lg.Fatal(exception);
				MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại sau.", "Lỗi");
				GC.Collect();
				return;
			}

			dgrdTongHop.DataSource = dataTableTongHop;

			foreach (DataRow row in dataTableTongHop.Select("(ShiftID1 is NULL and (TimeStrVao1 is not null or TimeStrRa1 is not null)) " +
															" or (ShiftID2 is NULL and (TimeStrVao2 is not null or TimeStrRa2 is not null))" +
															" or (ShiftID3 is NULL and (TimeStrVao3 is not null or TimeStrRa3 is not null))" +
															" OR (ShiftID1 is NOT NULL and ShiftID1 = " + int.MinValue + ")" +
															" OR (ShiftID2 is NOT NULL and ShiftID2 = " + int.MinValue + ")" +
															" OR (ShiftID3 is NOT NULL and ShiftID3 = " + int.MinValue + ")", string.Empty, DataViewRowState.CurrentRows).ToList()) {
				dataTableKDQD.ImportRow(row);
			}
			dgrdGioKDQD.DataSource = dataTableKDQD;
			checkAllGridTH.CheckedChanged -= checkAll_CheckedChanged;
			checkAllGridKDQD.CheckedChanged -= checkAll_CheckedChanged;
			checkAllGridTH.Checked = false;
			checkAllGridKDQD.Checked = false;
			checkAllGridTH.CheckedChanged += checkAll_CheckedChanged;
			checkAllGridKDQD.CheckedChanged += checkAll_CheckedChanged;
			GC.Collect();
			/*
						//m_DSNVXemCong.Select(info => info.PBCap1.ID).Distinct().ToList();
						Stopwatch s = new Stopwatch();
						s.Start();
						var pb_c2 = (from c in m_DSNVXemCong
									 select new { c.PBCap2.ID, c.PBCap2.TenPhongBan }).Distinct().ToList();
						foreach (var pb_i in pb_c2) {
							//write ten phong ban

							var pb_c1 = (from c in m_DSNVXemCong
										 where c.PBCap2.ID == pb_i.ID
										 select new { c.PBCap1.ID, c.PBCap1.TenPhongBan }).Distinct().ToList();

							foreach (var pb in pb_c1) {
								//write ten pb cap 1

								var dsnv = (from c in m_DSNVXemCong
											where c.PBCap1.ID == pb.ID
											select c).ToList();
								foreach (cUserInfo nhanvien in dsnv) {

								}
							}
						}
						s.Stop();
						Console.WriteLine(s.ElapsedMilliseconds);
						lg.Debug("");*/
		}

		private List<int> LayDSMaCC_Checked(DataGridView dataGridView) {
			List<int> kq = new List<int>();
			if (dataGridView.Rows.Count == 0) return kq;
			foreach (DataGridViewRow row in dataGridView.Rows) {
				DataRowView rowView = row.DataBoundItem as DataRowView;
				if (rowView == null) continue;
				bool IsChecked = (rowView["check"] != DBNull.Value) ? (bool)rowView["check"] : false;
				if (IsChecked) kq.Add((int)rowView["UserEnrollNumber"]);
			}
			return kq;
		}

		private void btnTangCa_Click(object sender, EventArgs e) {
			dtpNgayBD.Update(); dtpNgayKT.Update();
			DateTime ngayBD = dtpNgayBD.Value.Date;
			DateTime ngayKT = dtpNgayKT.Value.Date;
			ngayBD = ngayBD.AddDays(-1d);
			ngayKT = ngayKT.AddDays(2d).Subtract(new TimeSpan(0, 0, 1));
			frm_112_XacNhanTangCa frm112 = new frm_112_XacNhanTangCa();
			frm112.m_DSNV = m_DSNVXemCong;
			frm112.fNgayBD = ngayBD;
			frm112.fNgayKT = ngayKT;
			frm112.ShowDialog();
		}

		private void btnXemCT_Click(object sender, EventArgs e) {

			DateTime t1 = dtpNgayBD.Value.Date;
			DateTime t2 = dtpNgayKT.Value.Date;
			TimeSpan tmp = t2 - t1;
			int songay = tmp.Days + 3; // ngày 1->5 là trừ ra = 4, + 1 để ra số ngày, + thêm 2 để ra cột(UserEnrollNumber và số lượng)
			DataGridView currDataGrid;
			if (tabControlChiTiet.SelectedTab == tabTongHop) currDataGrid = dgrdTongHop;
			else if (tabControlChiTiet.SelectedTab == tabGioKDQD) currDataGrid = dgrdGioKDQD;
			else {
				currDataGrid = null;
				return;
			}

			if (currDataGrid.DataSource == null) return; // ko có datasouce thì thoát

			BindingContext[currDataGrid.DataSource].EndCurrentEdit();
			DataTable table = currDataGrid.DataSource as DataTable;
			if (table == null) return;
			DataRow[] arrRecord = table.Select("check = true", "UserEnrollNumber asc");

			if (arrRecord.Length == 0) return;


			frm_111_ChiTietVaoRa frm111 = new frm_111_ChiTietVaoRa();

			frm111.DSNgayCongChked = arrRecord;
			frm111.fNgayBD = t1.AddDays(-1d);
			frm111.fNgayKT = t2.AddDays(2d).Subtract(new TimeSpan(0, 0, 1));
			frm111.flstDSNVChk = m_DSNVXemCong;
			frm111.ShowDialog(); // có thực hiện thay đổi giờ cho nv -> xem lại công
			if (frm111.IsReload) {
				btnXem.PerformClick();
			}
		}

		private void btnXuatBaoBieu_Click(object sender, EventArgs e) {
			#region lấy ngày BD và kết thúc, và update lại Ngày BD = một ngày trước 31/08 12:00 AM, ngày KT là 1 ngày sau ngay 1 23:59:59
			dtpNgayBD.Update(); dtpNgayKT.Update();
			DateTime ngayBD = dtpNgayBD.Value.Date;
			DateTime ngayKT = dtpNgayKT.Value.Date;
			ngayBD = ngayBD.AddDays(-1d);
			ngayKT = ngayKT.AddDays(2d).Subtract(new TimeSpan(0, 0, 1));
			#endregion
			//-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

			if (dgrdDSNVTrgPhg.DataSource != null)
				BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();
			//2. lấy danh sách nhân viên check, nếu chưa có nv nào check thì thông báo
			List<int> dsMaCC_Checked = LayDSMaCC_Checked(dgrdDSNVTrgPhg);
			int[] ArrDSMaCC_Checked = dsMaCC_Checked.ToArray();
			if (dsMaCC_Checked.Count == 0) {
				AutoClosingMessageBox.Show("Chưa chọn Nhân viên", "Thông báo", 2000);
				return;
			}

			#region get data thông tin cá nhân từ bảng dgrdDSNVTrgPhg.DataSource

			DataTable tableUserInfo = dgrdDSNVTrgPhg.DataSource as DataTable;
			if (tableUserInfo == null) return;
			#endregion


			DataTable dataTableTongHop = dgrdTongHop.DataSource as DataTable;
			if (dataTableTongHop == null) dataTableTongHop = TaoCauTrucDataTableTongHop();
			else dataTableTongHop.Rows.Clear();


			//3. lấy dữ liệu chấm công của các nhân viên
			try {
				m_DSNVXemCong = XL.XemCong2(tableUserInfo, ArrDSMaCC_Checked, ngayBD, ngayKT);
				dataTableTongHop = XL.TaoTableXemCong(m_DSNVXemCong);
				saveFileDialog.Filter = "Excel Files|*.xlsx";

				if (saveFileDialog.ShowDialog() == DialogResult.OK) {
					string filepath = saveFileDialog.FileName;
					XL.XuatBBCongVaPC(m_DSNVXemCong, filepath, ngayBD, ngayKT); //[TBD]
				}

			} catch (Exception exception) {
				lg.Error("btnXuatBaoBieu_Click", exception);
				MessageBox.Show("Lỗi trong quá trình xuất báo biểu. Xin thử lại.", "Lỗi");
				GC.Collect();
				return;
			}

		}


		private void btnThemGio_Click(object sender, EventArgs e) {
			DateTime t1 = dtpNgayBD.Value.Date;
			DateTime t2 = dtpNgayKT.Value.Date;
			TimeSpan tmp = t2 - t1;
			int songay = tmp.Days + 3; // ngày 1->5 là trừ ra = 4, + 1 để ra số ngày, + thêm 2 để ra cột(UserEnrollNumber và số lượng)
			DataGridView currDataGrid;
			if (tabControlChiTiet.SelectedTab == tabTongHop) currDataGrid = dgrdTongHop;
			else if (tabControlChiTiet.SelectedTab == tabGioKDQD) currDataGrid = dgrdGioKDQD;
			else {
				currDataGrid = null;
				return;
			}
			// kiểm tra nếu chưa có dataSource null thì thoát
			if (currDataGrid.DataSource == null) return;
			BindingContext[currDataGrid.DataSource].EndCurrentEdit();
			DataTable table = currDataGrid.DataSource as DataTable;
			if (table == null) return;
			DataRow[] arrRecord = table.Select("check = true", "UserEnrollNumber asc");

			if (arrRecord.Length == 0) return;

			v03_frm_ThemGio frm1 = new v03_frm_ThemGio();
			frm1.DSNgayCongChked = arrRecord;
			frm1.ShowDialog();
			if (frm1.IsReload) btnXem.PerformClick();
		}

		private void btnSuaGio_Click(object sender, EventArgs e) {
			DateTime t1 = dtpNgayBD.Value.Date;
			DateTime t2 = dtpNgayKT.Value.Date;
			TimeSpan tmp = t2 - t1;
			int songay = tmp.Days + 3; // ngày 1->5 là trừ ra = 4, + 1 để ra số ngày, + thêm 2 để ra cột(UserEnrollNumber và số lượng)
			DataGridView currDataGrid;
			if (tabControlChiTiet.SelectedTab == tabTongHop) currDataGrid = dgrdTongHop;
			else if (tabControlChiTiet.SelectedTab == tabGioKDQD) currDataGrid = dgrdGioKDQD;
			else {
				currDataGrid = null;
				return;
			}
			// kiểm tra nếu chưa có dataSource null thì thoát
			if (currDataGrid.DataSource == null) return;
			BindingContext[currDataGrid.DataSource].EndCurrentEdit();
			DataTable table = currDataGrid.DataSource as DataTable;
			if (table == null) return;
			DataRow[] arrRecord = table.Select("check = true", "UserEnrollNumber asc");

			if (arrRecord.Length == 0) return;

			v03_frm_SuaGio frm1 = new v03_frm_SuaGio();
			frm1.DSNgayCongChked = arrRecord;
			frm1.ShowDialog();
			if (frm1.IsReload) btnXem.PerformClick();
		}

		private void btnXoaGio_Click(object sender, EventArgs e) {
			DateTime t1 = dtpNgayBD.Value.Date;
			DateTime t2 = dtpNgayKT.Value.Date;
			TimeSpan tmp = t2 - t1;
			int songay = tmp.Days + 3; // ngày 1->5 là trừ ra = 4, + 1 để ra số ngày, + thêm 2 để ra cột(UserEnrollNumber và số lượng)
			DataGridView currDataGrid;
			if (tabControlChiTiet.SelectedTab == tabTongHop) currDataGrid = dgrdTongHop;
			else if (tabControlChiTiet.SelectedTab == tabGioKDQD) currDataGrid = dgrdGioKDQD;
			else {
				currDataGrid = null;
				return;
			}
			// kiểm tra nếu chưa có dataSource null thì thoát
			if (currDataGrid.DataSource == null) return;
			BindingContext[currDataGrid.DataSource].EndCurrentEdit();
			DataTable table = currDataGrid.DataSource as DataTable;
			if (table == null) return;
			DataRow[] arrRecord = table.Select("check = true", "UserEnrollNumber asc");

			if (arrRecord.Length == 0) return;

			v03_frm_XoaGio frm1 = new v03_frm_XoaGio();
			frm1.DSNgayCongChked = arrRecord;
			frm1.ShowDialog();
			if (frm1.IsReload) btnXem.PerformClick();
		}
	}
}
