using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;
using ChamCong_v03.Properties;
using log4net;

namespace ChamCong_v03
{
    public partial class frm_21_KhaiBaoVang : Form
    {
        public readonly ILog lg = LogManager.GetLogger("frm_21_KhaiBaoVang");

		public struct Working
		{
			public float cong;
			public float hour;
			public float Cong { get { return cong; } }
			public float Hour { get { return hour; } }
		}
        public List<int> m_listIDPhongBan;
        public List<cUserInfo> m_DSNV = new List<cUserInfo>();
        public DataTable m_Bang_DSNV;
        public DataTable TaoBang_DSNV()
        {
            var kq = XL.TaoCauTrucDataTable(
                new[] { "check", "cUserInfo", "UserFullCode", "UserEnrollNumber", "UserFullName", "SchID", "SchName", "TitleName", "IDD_1", "Description_1", "HeSoLuongCB", "HeSoLuongSP", "HSBHCongThem" },
                new[] { typeof(bool), typeof(cUserInfo), typeof(string), typeof(int), typeof(string), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(double), typeof(double), typeof(double) });
            return kq;
        }

        public DateTime currMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        #region biến checkbox check all: 1 của DSNV cần xem công. 2 DS Công của NV
        readonly CheckBox checkAll_GridDSNV = new CheckBox();
        void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            //1. xác định datagrid nào đang check all
            DataGridView tempGrid = dgrdDSNVTrgPhg;

            // 2. kiểm tra nếu ko có dòng dữ liệu nào thì thoát
            if (tempGrid.Rows.Count == 0) return;

            //3. có dữ liệu, duyệt qua hết và gán giá trị check (or uncheck), sau đó endEDIT lại lưới để dòng đầu tiên có giá trị
            bool tmpCheckAll = ((CheckBox)sender).Checked;

            tempGrid.BeginEdit(true);
            var dt = tempGrid.DataSource as DataView;

            if (dt != null && dt.Count != 0)
            {
                foreach (DataRowView row in dt)
                {
                    row["check"] = tmpCheckAll;
                }
            }

            tempGrid.EndEdit();
            tempGrid.Update();
            tempGrid.RefreshEdit();
        }
        #endregion


        // hàm xử lý -----------------------------------------------------------------------------
        #region load treeview new
        public TreeView loadTreePhgBan(TreeView tvDSPhongBan, DataTable pDataTable)
        {
            if (pDataTable.Rows.Count > 0)
            {
                foreach (var dataRow in pDataTable.Select("RelationID = 0", "ViTri asc"))
                {
                    var ParentNode = new TreeNode { Text = dataRow["Description"].ToString(), Tag = (int)dataRow["ID"] };
                    tvDSPhongBan.Nodes.Add(ParentNode);
                    loadTreeSubNode(ref ParentNode, (int)(dataRow["ID"]), pDataTable);
                }
            }
            return tvDSPhongBan;
        }

        private void loadTreeSubNode(ref TreeNode ParentNode, int ParentId, DataTable dtMenu)
        {
            var childs = dtMenu.Select("RelationID = " + ParentId, "ViTri asc");
            foreach (var dRow in childs)
            {
                var child = new TreeNode { Text = dRow["Description"].ToString(), Tag = (int)dRow["ID"], ToolTipText = dRow["Description"].ToString() };
                ParentNode.Nodes.Add(child);
                //Recursion Call
                loadTreeSubNode(ref child, (int)dRow["ID"], dtMenu);
            }
        }

        void GetIDLeafNodeExceptParent(TreeNode root, List<int> listID)
        {
            if (root == null) return;

            listID.Add((int)root.Tag);

            if (root.Nodes.Count > 0)
            {
                foreach (TreeNode node in root.Nodes)
                {
                    GetIDLeafNodeExceptParent(node, listID);
                }
            }
            // xuốn đến đây tương đương root.Nodes.Count== 0; return
        }

        private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false)
            {
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

        public frm_21_KhaiBaoVang()
        {
            InitializeComponent();
            dgrdDSNVTrgPhg.AutoGenerateColumns = dgrdNgayVang.AutoGenerateColumns = false;
            m_listIDPhongBan = new List<int>();
            m_DSNV = new List<cUserInfo>();
            m_Bang_DSNV = TaoBang_DSNV();
            DataView dataView = new DataView(m_Bang_DSNV);
            dgrdDSNVTrgPhg.DataSource = dataView;

            XL2.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));
        }

        private void frm_KhaiBaoVang_Load(object sender, EventArgs e)
        {
            if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false)
            {
                AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 4000);
                this.Close();
                return;
            }

            // 1. khởi tạo các biến cục bộ
            var tablePhong = DAL.LayDSPhong(XL2.currUserID);
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


            // lấy tháng hiện tại cho dtpThang
            dtpThang.Value = currMonth;

            var tableLV = SqlDataAccessHelper.ExecuteQueryString("Select * from LoaiVang");
            cbLoaiVang.ValueMember = "AbsentCode";
            cbLoaiVang.DisplayMember = "AbsentDescription";
            cbLoaiVang.DataSource = tableLV;
            cbLoaiVang.SelectedIndex = 0;

			List<Working> list = new List<Working>
				{
					new Working{cong = 0.5f, hour = 4f},
					new Working{cong = 1f, hour = 8f},
					new Working{cong = 2f, hour = 16f}
				};
	        cbSoBuoi.DataSource = list;
	        cbSoBuoi.ValueMember = "Hour";
	        cbSoBuoi.DisplayMember = "Cong";
            cbSoBuoi.SelectedIndex = 1; // mặc định cho số buổi là 1
        }


        private void dtpThang_ValueChanged(object sender, EventArgs e)
        {
            currMonth = dtpThang.Value;
            var startDay = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);
            var endddDay = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, DateTime.DaysInMonth(dtpThang.Value.Year, dtpThang.Value.Month));

            var indexDay = startDay;
            var lstNgay = new List<DateTime>();
            while (indexDay <= endddDay)
            {
                lstNgay.Add(indexDay);
                indexDay = indexDay.AddDays(1d);
            }
            checklistNgay.FormatString = "d - ddd";

            checklistNgay.DataSource = lstNgay;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false)
            {
                AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
                return;
            }

            #region hỏi lại trước khi thực hiện
            if (MessageBox.Show("Bạn muốn thêm các khai báo vắng cho nhân viên?", Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            #endregion
            //1. lấy dữ liệu từ form
            dtpThang.Update();
            currMonth = dtpThang.Value;

            dgrdDSNVTrgPhg.EndEdit();
            dgrdDSNVTrgPhg.Update();

            BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();
            //2. lấy danh sách nhân viên check, nếu chưa có nv nào check thì thông báo
            IEnumerable<DataGridViewRow> lstGridViewRow = dgrdDSNVTrgPhg.Rows.Cast<DataGridViewRow>();
            var listDataRow = from row in (lstGridViewRow) select row.DataBoundItem as DataRowView;
            var listNV = (from rowView in listDataRow
                          where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
                          select ((cUserInfo)rowView["cUserInfo"]).MaCC).ToArray();

            if (listNV.Length == 0)
            {
                AutoClosingMessageBox.Show("Bạn chưa chọn Nhân viên", "Thông báo", 2000);
                return;
            }

            // lấy ngày check
            var DSNgayCheck = new List<DateTime>();
            if (rdChonNgayTrongThang.Checked)
            {
                DSNgayCheck = (from item in checklistNgay.CheckedItems.Cast<object>() select (DateTime)item).ToList();
            }
            else
            {
                for (var indexNgay = dtpNgayBD.Value.Date; indexNgay <= dtpNgayKT.Value.Date; indexNgay = indexNgay.AddDays(1d))
                {
					if (chk1ExceptSat.Checked && indexNgay.DayOfWeek == DayOfWeek.Saturday) continue;
					if (chk1ExceptSun.Checked && indexNgay.DayOfWeek == DayOfWeek.Sunday) continue;
	                DSNgayCheck.Add(indexNgay.Date);
                }
            }
            if (DSNgayCheck.Count == 0)
            {
                AutoClosingMessageBox.Show("Bạn chưa chọn ngày vắng", "Thông báo", 2000);
                return;
            }

            // lấy loại vắng
            if (cbLoaiVang.SelectedItem == null)
            {
                AutoClosingMessageBox.Show("Bạn chưa chọn loại vắng", "Thông báo", 2000);
                return;
            }
            var rowLV = cbLoaiVang.SelectedItem as DataRowView;

            var absentCode = rowLV["AbsentCode"].ToString();
            var workingDay = (double)((Working)cbSoBuoi.SelectedItem).Cong;
            var workingTime = 0d;

            #region set working time tùy theo workingDay

            if (workingDay == 0d) workingTime = 0d;
            else if (workingDay == 0.5d) workingTime = 4d;
            else if (workingDay == 1d) workingTime = 8d;
            else if (workingDay == 1.5d) workingTime = 12d;
            else if (workingDay == 2d) workingTime = 16d;

            #endregion

            //var kqThaotac = DAL.ThemNgayVang(listNV, DSNgayCheck, rowLV, XL2.currUserID);
            var kqThaotac = DAL.ThemNgayVang(listNV, DSNgayCheck, workingDay, workingTime, absentCode, XL2.currUserID);
            if (kqThaotac == false)
                MessageBox.Show("Có lỗi trong quá trình thực hiện. Vui lòng kiểm tra lại dữ liệu.", "Lỗi");

            // sau khi thao tác xong thì liệt kê lại
            Thread.Sleep(20);
            btnLietKe.PerformClick();
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false)
            {
                AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
                return;
            }

            #region hỏi lại trước khi thực hiện

            if (MessageBox.Show("Bạn muốn xoá các khai báo vắng đang chọn?", Resources.capXacNhan, MessageBoxButtons.YesNo) ==
                DialogResult.No)
            {
                return;
            }

            #endregion

            dgrdNgayVang.EndEdit();
            dgrdNgayVang.Update();

            var arrRecord = (from row in dgrdNgayVang.SelectedRows.Cast<DataGridViewRow>()
                             select (int)((DataRowView)row.DataBoundItem)["ID"]).ToArray();
            if (arrRecord.Length == 0)
            {
                return;
            }

            var kqThaotac = DAL.XoaNgayVangNV(arrRecord);
            if (kqThaotac == false)
                MessageBox.Show("Có lỗi trong quá trình thực hiện. Vui lòng kiểm tra lại dữ liệu.", "Lỗi");
            GC.Collect();

            Thread.Sleep(20);
            btnLietKe.PerformClick();
        }

        private void btnLietKe_Click(object sender, EventArgs e)
        {
            if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false)
            {
                AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
                return;
            }

            //1. lấy dữ liệu từ form
            dtpThang.Update();
            currMonth = dtpThang.Value;
            var ngayBD = DateTime.MinValue;
            var ngayKT = DateTime.MinValue;
            if (rdChonNgayTrongKhoang.Checked)
            {
                ngayBD = dtpNgayBD.Value.Date;
                ngayKT = dtpNgayKT.Value.Date;
            }
            else
            {
                ngayBD = new DateTime(currMonth.Year, currMonth.Month, 1);
                ngayKT = new DateTime(currMonth.Year, currMonth.Month, DateTime.DaysInMonth(currMonth.Year, currMonth.Month));
            }
            //-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

            dgrdDSNVTrgPhg.EndEdit();
            dgrdDSNVTrgPhg.Update();

            //2. lấy danh sách nhân viên check
            BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();
            //2. lấy danh sách nhân viên check, nếu chưa có nv nào check thì thông báo
            IEnumerable<DataGridViewRow> lstGridViewRow = dgrdDSNVTrgPhg.Rows.Cast<DataGridViewRow>();
            var listDataRow = from row in (lstGridViewRow) select row.DataBoundItem as DataRowView;
            var listNV = (from rowView in listDataRow
                          where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
                          select ((cUserInfo)rowView["cUserInfo"]).MaCC).ToArray();
            if (listNV.Length == 0)
            {
                AutoClosingMessageBox.Show("Bạn chưa chọn Nhân viên.", "Thông báo", 2000);
                return;
            }

            var table = DAL.LietKeNgayVangChoNV(listNV, ngayBD, ngayKT);
            dgrdNgayVang.DataSource = table;
            if (table.Rows.Count == 0) AutoClosingMessageBox.Show("Các nhân viên đang chọn không vắng ngày nào trong tháng.", "Thông báo", 1500);
            GC.Collect();
        }

        private void rdChonNgayTrongKhoang_CheckedChanged(object sender, EventArgs e)
        {
            if (rdChonNgayTrongKhoang.Checked)
            {
                //dtpNgayBD.Enabled = dtpNgayKT.Enabled = true;
	            chk2ExceptSat.Checked = false;
	            chk2ExceptSun.Checked = false;
	            chk2AllWKDays.Checked = false;
				MyUtility.EnableDisableControl(true, new Control[] { dtpNgayBD, dtpNgayKT, chk1ExceptSat, chk1ExceptSun  });
				MyUtility.EnableDisableControl(false, new Control[]{dtpThang, checklistNgay, chk2AllWKDays, chk2ExceptSat, chk2ExceptSun});
                if (checklistNgay.Items.Count > 0)
                {
                    for (var i = 0; i < checklistNgay.Items.Count; i++)
                    {
                        checklistNgay.SetItemChecked(i, false);
                    }
                }
            }
            else
            {
				chk1ExceptSat.Checked = false;
	            chk1ExceptSun.Checked = false;
                //dtpNgayBD.Enabled = dtpNgayKT.Enabled = false;
                //dtpThang.Enabled = checklistNgay.Enabled = true;
				MyUtility.EnableDisableControl(false, new Control[] { dtpNgayBD, dtpNgayKT, chk1ExceptSat, chk1ExceptSun });
				MyUtility.EnableDisableControl(true, new Control[]{dtpThang, checklistNgay, chk2AllWKDays, chk2ExceptSat, chk2ExceptSun});
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (dgrdDSNVTrgPhg.DataSource == null) return;

            var searchStr1 = tbSearch.Text;
            var searchStr = string.Format("UserFullName like '%{0}%' or UserFullCode like '%{0}%'", searchStr1);
            var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
            dataView.RowFilter = searchStr;


        }

        private void linkHienThiTatCaNV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
            dataView.RowFilter = string.Empty;
        }



		private void chk2AllDays_CheckedChanged(object sender, EventArgs e) {
			bool checkT7 = chk2ExceptSat.Checked;
			bool checkCN = chk2ExceptSun.Checked;
			bool checkAll = chk2AllWKDays.Checked;
			for (int i = 0; i < checklistNgay.Items.Count; i++) {
				var obj = checklistNgay.Items[i];
				DateTime ngay = (DateTime)obj;
				checklistNgay.SetItemChecked(i, checkAll);
				if (ngay.DayOfWeek != DayOfWeek.Saturday && ngay.DayOfWeek != DayOfWeek.Sunday) checklistNgay.SetItemChecked(i, checkAll);
				if (ngay.DayOfWeek == DayOfWeek.Saturday) checklistNgay.SetItemChecked(i, checkT7);
				if (ngay.DayOfWeek == DayOfWeek.Sunday) checklistNgay.SetItemChecked(i, checkCN);
			}

		}



    }
}
