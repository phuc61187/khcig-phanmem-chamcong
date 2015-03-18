using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;
using log4net;

namespace ChamCong_v03 {
	public partial class frm_31_SuaGioHangLoat : Form {

		public readonly ILog lg = LogManager.GetLogger("frm_31_SuaGioHangLoat");

		public List<int> m_listIDPhongBan { get; set; }
		public List<cUserInfo> m_DSNV { get; set; }
		public DataTable m_Bang_DSNV;

		public DataTable TaoBang_DSNV() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "check", "cUserInfo", "UserEnrollNumber", "UserFullCode", "UserFullName", "SchID", "SchName", "TitleName", "IDD_1", "Description_1", "HeSoLuongCB", "HeSoLuongSP", "HSBHCongThem" },
				new[] { typeof(bool), typeof(cUserInfo), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(double), typeof(double), typeof(double) });
			return kq;
		}

		#region 3 biến checkbox check all: 1 của DSNV cần xem công. 2 DS Công của NV
		readonly CheckBox checkAll_GridDSNV = new CheckBox();
		readonly CheckBox checkAllGridCheckIn = new CheckBox();
		readonly CheckBox checkAllGridCheckOut = new CheckBox();
		#region vẽ check box check all và xử lý sự kiện check
		void checkAll_CheckedChanged(object sender, EventArgs e) {
			//1. xác định datagrid nào đang check all
			DataGridView tempGrid;
			if (sender == checkAll_GridDSNV) tempGrid = dgrdDSNVTrgPhg;
			else if (sender == checkAllGridCheckIn) tempGrid = dgrdCTGioVao;
			else tempGrid = dgrdCTGioRa;

			// 2. kiểm tra nếu ko có dòng dữ liệu nào thì thoát
			if (tempGrid.Rows.Count == 0) return;

			//3. có dữ liệu, duyệt qua hết và gán giá trị check (or uncheck), sau đó endEDIT lại lưới để dòng đầu tiên có giá trị
			bool tmpCheckAll = ((CheckBox)sender).Checked;

			tempGrid.BeginEdit(true);
			if (tempGrid.DataSource is DataTable) {
				var dt = tempGrid.DataSource as DataTable;
				if (dt.Rows.Count != 0) {
					foreach (DataRow row in dt.Rows) {
						row["check"] = tmpCheckAll;
					}
				}

			}
			else if (tempGrid.DataSource is DataView) {
				var dt = tempGrid.DataSource as DataView;
				if (dt.Count != 0) {
					foreach (DataRowView row in dt) {
						row["check"] = tmpCheckAll;
					}
				}
			}


			tempGrid.EndEdit();
			tempGrid.Update();
			tempGrid.RefreshEdit();
		}
		#endregion


		#endregion


		// hàm xử lý -----------------------------------------------------------------------------
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
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 4000);
				this.Close();
				return;
			}

			m_listIDPhongBan.Clear();
			if (e.Node.FirstNode != null) GetIDLeafNodeExceptParent(e.Node, m_listIDPhongBan);
			else m_listIDPhongBan.Add((int)e.Node.Tag);
			e.Node.Expand();


			var table = DAL.LayDSNV(m_listIDPhongBan.ToArray());
			if (table.Rows.Count == 0) return;
			m_DSNV.Clear();
			XL.KhoiTaoDSNV(m_DSNV, table);
			m_Bang_DSNV.Rows.Clear();
			XL.TaoTableDSNV(m_DSNV, m_Bang_DSNV);
			checkAll_GridDSNV.Checked = false;
		}

		#endregion

		private List<int> LayDSMaCC_Checked(DataGridView dataGridView) {
			List<int> kq = new List<int>();
			if (dataGridView.Rows.Count == 0) return kq;
			kq.AddRange(from rowView in
							(from DataGridViewRow row in dataGridView.Rows
							 select row.DataBoundItem).OfType<DataRowView>()
						let IsChecked = (rowView["check"] != DBNull.Value) ? (bool)rowView["check"] : false
						where IsChecked
						select (int)rowView["UserEnrollNumber"]);
			return kq;
		}

		private void btnXem_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			//0. xoa dữ liệu cũ
			//1. lấy dữ liệu từ form
			dtpBD.Update(); dtpKT.Update();
			DateTime startTime = dtpBD.Value;
			DateTime endTime = dtpKT.Value;
			dgrdDSNVTrgPhg.EndEdit();
			if (dgrdDSNVTrgPhg.DataSource != null)
				BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();
			//2. lấy danh sách nhân viên check, nếu chưa có nv nào check thì thông báo
			List<int> dsMaCC_Checked = LayDSMaCC_Checked(dgrdDSNVTrgPhg);
			int[] ArrDSMaCC_Checked = dsMaCC_Checked.ToArray();
			if (dsMaCC_Checked.Count == 0) {
				AutoClosingMessageBox.Show("Chưa chọn Nhân viên", "Thông báo", 2000);
				return;
			}
			//2. lấy danh sách nhân viên check
			DataTable fDataTableChkInOut = new DataTable();
			try {
				fDataTableChkInOut = DAL.LayTableCIO_A_SuaHangLoat(ArrDSMaCC_Checked, startTime, endTime);
				fDataTableChkInOut.Columns.Add("check", typeof(bool));
			} catch (Exception) {
				MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại sau.", "Lỗi");
				return;
			}

			DataTable dataTableChiTietVao = dgrdCTGioVao.DataSource as DataTable;
			DataTable dataTableChiTietRa = dgrdCTGioRa.DataSource as DataTable;
			if (dataTableChiTietVao == null) {
				dataTableChiTietVao = fDataTableChkInOut.Clone();
			}
			else dataTableChiTietVao.Rows.Clear();

			if (dataTableChiTietRa == null) {
				dataTableChiTietRa = fDataTableChkInOut.Clone();
			}
			else dataTableChiTietRa.Rows.Clear();


			//4. xử lý dữ liệu để đưa lên lưới tổng hợp
			foreach (DataRow row in fDataTableChkInOut.Rows) {
				if ((int)row["MachineNo"] % 2 == 1) dataTableChiTietVao.ImportRow(row);
				else dataTableChiTietRa.ImportRow(row);
			}
			dgrdCTGioVao.DataSource = dataTableChiTietVao;
			dgrdCTGioRa.DataSource = dataTableChiTietRa;
			checkAllGridCheckIn.CheckedChanged -= checkAll_CheckedChanged;
			checkAllGridCheckOut.CheckedChanged -= checkAll_CheckedChanged;
			checkAllGridCheckIn.Checked = false;
			checkAllGridCheckOut.Checked = false;
			checkAllGridCheckIn.CheckedChanged += checkAll_CheckedChanged;
			checkAllGridCheckOut.CheckedChanged += checkAll_CheckedChanged;

		}

		private void btnSuaGio_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			bool flag = false;
			bool tmpSuaGioVao = (tabControl1.SelectedIndex == 0);

			DataGridView dataGridThaoTac = tmpSuaGioVao ? dgrdCTGioVao : dgrdCTGioRa;
			DataTable table = dataGridThaoTac.DataSource as DataTable;
			if (table == null) return;
			DataRow[] tmpArrRow = (table).Select("check = true");
			if (tmpArrRow.Length == 0) {
				AutoClosingMessageBox.Show("Chưa chọn nhân viên sửa giờ chấm công.", "Thông báo", 2000);
				return;
			}

			frm_311_ThongTinSuaGioHangLoat frm311Thongtin = new frm_311_ThongTinSuaGioHangLoat() { fNgaySuaMacDinh = dtpBD.Value.Date, fCheckIn = tmpSuaGioVao };
			frm311Thongtin.ShowDialog();

			if (frm311Thongtin.fOK) {
				DateTime tmpGioMoi = frm311Thongtin.fGioMoi;
				bool tmpKieuGioMoi = frm311Thongtin.fCheckIn;
				foreach (DataRow row in tmpArrRow) {
					int tmpUserEnrollNumber = (int)row["UserEnrollNumber"];
					DateTime tmpGioCu = (DateTime)row["TimeStr"];
					int tmpMachineNo = (int)row["MachineNo"];
					string tmpSourceOld = row["Source"].ToString();
					string tmpLyDo = frm311Thongtin.fLydo;
					string tmpGhiChu = frm311Thongtin.fGhichu;
					flag = DAL.SuaGioChoNV(tmpUserEnrollNumber
						, tmpGioCu, tmpGioMoi, tmpSourceOld, "PC", tmpMachineNo, (tmpKieuGioMoi) ? 21 : 22, -1, XL2.currUserID, tmpLyDo, tmpGhiChu);
					if (flag == false) {
						lg.Error("Lỗi: sửa giờ cho " + tmpUserEnrollNumber + " giờ chưa sửa: " + tmpGioCu + " giờ sửa: " + tmpGioMoi);
						break;
					}

				}
				if (flag) {
					btnXem_Click(btnXem, null);
				}
				else {
					MessageBox.Show("Xảy ra lỗi trong quá trình sửa giờ cho Nhân viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK);
				}
			}

		}



		public frm_31_SuaGioHangLoat() {
			InitializeComponent();
			m_listIDPhongBan = new List<int>();
			m_DSNV = new List<cUserInfo>();
			m_Bang_DSNV = TaoBang_DSNV();

			dgrdDSNVTrgPhg.AutoGenerateColumns = dgrdCTGioVao.AutoGenerateColumns = dgrdCTGioRa.AutoGenerateColumns = false;
			DataView dataView = new DataView(m_Bang_DSNV);
			dgrdDSNVTrgPhg.DataSource = dataView;

			XL2.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));
			XL2.VeCheckBox_CheckAll(dgrdCTGioVao, checkAllGridCheckIn, checkAll_CheckedChanged, new Point(7, 3));
			XL2.VeCheckBox_CheckAll(dgrdCTGioRa, checkAllGridCheckOut, checkAll_CheckedChanged, new Point(7, 3));

			DateTime today = DateTime.Today;
			dtpBD.Value = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
			dtpKT.Value = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59, 0);
		}

		private void frm_SuaGioHangLoat2_Load(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				this.Close();
				return;
			}

			// 1. khởi tạo các biến cục bộ
			m_listIDPhongBan = new List<int>();
			DataTable tablePhong = DAL.LayDSPhong(XL2.currUserID);
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

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void button4_Click(object sender, EventArgs e) {
			if (dgrdDSNVTrgPhg.DataSource == null) return;

			var searchStr1 = tbSearch.Text;
			var searchStr = string.Format("UserFullName like '%{0}%' or UserFullCode like '%{0}%'", searchStr1);
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			dataView.RowFilter = searchStr;


		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			dataView.RowFilter = string.Empty;

		}


	}
}
