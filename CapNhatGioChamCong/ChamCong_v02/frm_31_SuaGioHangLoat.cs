using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ChamCong_v02.BUS;
using ChamCong_v02.DAO;
using ChamCong_v02.DTO;
using log4net;

namespace ChamCong_v02
{
	public partial class frm_31_SuaGioHangLoat : Form {

        public readonly ILog lg = LogManager.GetLogger("frm_31_SuaGioHangLoat");

        public List<int> m_listIDPhongBan { get; set; }
        public List<cUserInfo> m_DSNVXemCong { get; set; }

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
			DataTable dt = tempGrid.DataSource as DataTable;

			if (dt.Rows.Count != 0) {
				foreach (DataRow row in dt.Rows) {
					row["check"] = tmpCheckAll;
				}
			}

			tempGrid.EndEdit();
			tempGrid.Update();
			//tempGrid.Refresh();
		}
		#endregion


        #endregion


		// hàm xử lý -----------------------------------------------------------------------------
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

        private List<int> LayDSMaCC_Checked(DataGridView dataGridView)
        {
            List<int> kq = new List<int>();
            if (dataGridView.Rows.Count == 0) return kq;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DataRowView rowView = row.DataBoundItem as DataRowView;
                if (rowView == null) continue;
                bool IsChecked = (rowView["check"] != DBNull.Value) ? (bool)rowView["check"] : false;
                if (IsChecked) kq.Add((int)rowView["UserEnrollNumber"]);
            }
            return kq;
        }
		
		private void btnXem_Click(object sender, EventArgs e) {
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
            if (dsMaCC_Checked.Count == 0)
            {
                AutoClosingMessageBox.Show("Chưa chọn Nhân viên", "Thông báo", 2000);
                return;
            }
            //2. lấy danh sách nhân viên check
			DataTable fDataTableChkInOut = new DataTable(); 
            try
			{
			    fDataTableChkInOut = DAL.LayTableCIO_A(ArrDSMaCC_Checked, startTime, endTime);
			    fDataTableChkInOut.Columns.Add("check", typeof (bool));
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
			bool flag = false;
			bool tmpSuaGioVao = (tabControl1.SelectedTabIndex == 0);

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
                                            , tmpGioCu, tmpGioMoi, tmpKieuGioMoi, tmpSourceOld
                                            , tmpMachineNo, ThamSo.currUserID, tmpLyDo, tmpGhiChu);
					if (flag == false) {
						lg.Error("Lỗi: sửa giờ cho " + tmpUserEnrollNumber + " giờ chưa sửa: "+ tmpGioCu + " giờ sửa: " + tmpGioMoi);
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
			dgrdDSNVTrgPhg.AutoGenerateColumns = dgrdCTGioVao.AutoGenerateColumns = dgrdCTGioRa.AutoGenerateColumns = false;

			ThamSo.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));
			ThamSo.VeCheckBox_CheckAll(dgrdCTGioVao, checkAllGridCheckIn, checkAll_CheckedChanged, new Point(7, 3));
			ThamSo.VeCheckBox_CheckAll(dgrdCTGioRa, checkAllGridCheckOut, checkAll_CheckedChanged, new Point(7, 3));

			DateTime today = DateTime.Today;
			dtpBD.Value = new DateTime(today.Year,today.Month,today.Day,0,0,0);
			dtpKT.Value = new DateTime(today.Year,today.Month,today.Day, 23,59,59,0);
		}

		private void frm_SuaGioHangLoat2_Load(object sender, EventArgs e) {

			// 1. khởi tạo các biến cục bộ
            m_listIDPhongBan = new List<int>();
            DataTable tablePhong = DAL.LayDSPhong(ThamSo.currUserID);
            if (tablePhong.Rows.Count == 0)
            {
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


	}
}
