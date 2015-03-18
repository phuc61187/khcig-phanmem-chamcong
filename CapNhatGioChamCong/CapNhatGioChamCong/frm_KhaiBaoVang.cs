using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapNhatGioChamCong.BUS;
using CapNhatGioChamCong.DTO;

namespace CapNhatGioChamCong {
    public partial class frm_KhaiBaoVang : Form {
        #region local variable

        public List<int> flstIDPhongBan { get; set; }
        public DataTable fTablePhongBan { get; set; }
        public DataTable fTableDSNV { get; set; }
        public List<cUserInfo> flstDSNVPhong { get; set; }
        public DateTime currMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        #region 3 biến checkbox check all: 1 của DSNV cần xem công. 2 DS Công của NV
        readonly CheckBox checkAll_GridDSNV = new CheckBox();
        readonly CheckBox checkAll_GridNgayVang = new CheckBox();
        #endregion

        #endregion

        // hàm xử lý -----------------------------------------------------------------------------
        #region load treeview
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

        void GetNodeID(TreeNode root) {
            if (root == null) return;
            if (root.FirstNode == null) {
                flstIDPhongBan.Add((int)root.Tag);
                root = root.NextNode;
                return;
            }
            if (root.FirstNode != null) {
                foreach (TreeNode treeNode in root.Nodes)
                    GetNodeID(treeNode);
            }
        }

        private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
            flstIDPhongBan.Clear();
            if (e.Node.FirstNode != null) GetNodeID(e.Node);
            else flstIDPhongBan.Add((int)e.Node.Tag);
            e.Node.Expand();

            string temp = "UserIDD = {0}";
            temp = String.Format(temp, String.Join(" or UserIDD = ", flstIDPhongBan.ToArray()));

            DataTable table = dgrdDSNVTrgPhg.DataSource as DataTable;
            if (table == null) table = fTableDSNV.Clone();
            else table.Rows.Clear();

            foreach (DataRow row in fTableDSNV.Select(temp, "UserIDD asc, UserEnrollNumber asc", DataViewRowState.CurrentRows))
                table.ImportRow(row);

            dgrdDSNVTrgPhg.DataSource = table;
            checkAll_GridDSNV.Checked = false;
        }

        #endregion

        #region vẽ checkBox check all và xử lý sự kiện check

        void checkAll_CheckedChanged(object sender, EventArgs e) {
            //1. xác định datagrid nào đang check all
            DataGridView tempGrid;
            if (sender == checkAll_GridDSNV) tempGrid = dgrdDSNVTrgPhg;
            else tempGrid = dgrdNgayVang;

            // 2. kiểm tra nếu ko có dòng dữ liệu nào thì thoát
            if (tempGrid.Rows.Count == 0) return;

            //3. có dữ liệu, duyệt qua hết và gán giá trị check (or uncheck), sau đó endEDIT lại lưới để dòng đầu tiên có giá trị
            bool tmpCheckAll = ((CheckBox)sender).Checked;

            tempGrid.BeginEdit(true);
            DataTable dt = tempGrid.DataSource as DataTable;

            if (dt != null && dt.Rows.Count != 0) {
                foreach (DataRow row in dt.Rows)
                    row["check"] = tmpCheckAll;
            }

            tempGrid.EndEdit();
            tempGrid.Update();
        }
        #endregion


        public frm_KhaiBaoVang() {
            InitializeComponent();
            dgrdDSNVTrgPhg.AutoGenerateColumns = dgrdNgayVang.AutoGenerateColumns = false;
            ThamSo.VeCheckBox_CheckAll(dgrdNgayVang, checkAll_GridNgayVang, checkAll_CheckedChanged, new Point(7, 3));
            ThamSo.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7,3));
        }

        private void frm_KhaiBaoVang_Load(object sender, EventArgs e) {
            // 1. khởi tạo các biến cục bộ
            flstIDPhongBan = new List<int>();

            //2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan : xoá dữ liệu trước và load
            fTablePhongBan = ThamSo.TablePhongBan.Copy();
            treePhongBan.Nodes.Clear();
            loadTreePhgBan(treePhongBan, fTablePhongBan);

            // 

            // 3. Duyệt  dữ liệu toàn bộ danh sách nhân viên được phép thao tác(và thêm cột check) 			
            // và  khởi tạo các giá trị mặc định cho từng nhân viên  
            fTableDSNV = ThamSo.DataTableDSNV.Copy();
            fTableDSNV.Columns.Add("check", typeof(bool));

            // đăng ký sự kiện cho tree và chọn topNode
            treePhongBan.AfterSelect += treePhongBan_AfterSelect;
            treePhongBan.SelectedNode = treePhongBan.TopNode;

            // lấy tháng hiện tại cho dtpThang
            dtpThang.Value = currMonth;

            DataTable tableLV = DAO.SqlDataAccessHelper.ExecuteQueryString("Select * from LoaiVang");
            LoadComboLoaiVang(tableLV);

        }

        private void LoadComboLoaiVang(DataTable tableLV) {
            cbLoaiVang.DataSource = tableLV;
            cbLoaiVang.ValueMember = "AbsentCode";
            cbLoaiVang.DisplayMember = "AbsentDescription";
			cbLoaiVang.SelectedIndex = 0;
        }

        private void dtpThang_ValueChanged(object sender, EventArgs e) {
            currMonth = dtpThang.Value;
            DateTime startDay = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);
            DateTime endddDay = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, DateTime.DaysInMonth(dtpThang.Value.Year, dtpThang.Value.Month));

            DateTime indexDay = startDay;
            List<DateTime> lstNgay = new List<DateTime>();
            while (indexDay <= endddDay) {
                lstNgay.Add(indexDay);
                indexDay = indexDay.AddDays(1d);
            }
            checklistNgay.FormatString = "d - ddd";
            
            checklistNgay.DataSource = lstNgay;
        }

        private void btnThem_Click(object sender, EventArgs e) {
            //1. lấy dữ liệu từ form
            dtpThang.Update();
            currMonth = dtpThang.Value;
            //-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

            dgrdDSNVTrgPhg.EndEdit();
            dgrdDSNVTrgPhg.Update();

            //2. lấy danh sách nhân viên check
            object[,] arrDSNVCheck = new object[dgrdDSNVTrgPhg.Rows.Count + 1, 2];
            LayDSMaCC_MaNV_Checked(dgrdDSNVTrgPhg.DataSource as DataTable, arrDSNVCheck);
            if ((int)arrDSNVCheck[0, 0] == 0) {
                AutoClosingMessageBox.Show("Chưa chọn Nhân viên", "Thông báo", 2000);
                return;
            }

            // lấy ngày check
            List<DateTime> DSNgayCheck = LayNgayCheck(checklistNgay);

            // lấy loại vắng
            DataRowView row = cbLoaiVang.SelectedItem as DataRowView;

            bool kqThaotac = XL.KhaiBaoNgayVangChoNV(arrDSNVCheck, DSNgayCheck, row);
            if (kqThaotac == false)
                MessageBox.Show("Có lỗi trong quá trình thực hiện. Vui lòng kiểm tra lại dữ liệu.", "Lỗi");

            // sau khi thao tác xong thì liệt kê lại
            btnLietKe_Click(btnLietKe, null);
        }



        public void LayDSMaCC_MaNV_Checked(DataTable pdataTableDSNVCheck, object[,] arrDSNVCheck) {
            DataRow[] arrRecord = pdataTableDSNVCheck.Select("check = true", "UserEnrollNumber asc", DataViewRowState.CurrentRows);
            if (arrRecord.Length == 0) {
                arrDSNVCheck[0, 0] = 0;
                return;
            }
            else arrDSNVCheck[0, 0] = arrRecord.Length;

            for (int i = 0; i < arrRecord.Length; i++) {
                DataRow row = arrRecord[i];
                arrDSNVCheck[i + 1, 0] = ((int)row["UserEnrollNumber"]);
                arrDSNVCheck[i + 1, 1] = row["UserFullCode"].ToString();
            }
        }

        public List<DateTime> LayNgayCheck(CheckedListBox checkedList) {
            List<DateTime> kq = new List<DateTime>();
            if (checkedList.CheckedItems.Count == 0) return kq;
            foreach (object item in checkedList.CheckedItems) {
                kq.Add((DateTime)item);
            }
            return kq;
        }

        private void btnXoa_Click(object sender, EventArgs e) {
            //-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

            dgrdNgayVang.EndEdit();
            dgrdNgayVang.Update();

            //2. lấy danh sách row cần xóa
            DataTable table = dgrdNgayVang.DataSource as DataTable;
            if (table == null) return;

            DataRow[] arrRecord = table.Select("check = true" , "UserEnrollNumber asc");
            if (arrRecord.Length == 0) return;

            bool kqThaotac = XL.XoaNgayVangNV(arrRecord);
            if (kqThaotac == false)
                MessageBox.Show("Có lỗi trong quá trình thực hiện. Vui lòng kiểm tra lại dữ liệu.", "Lỗi");
            btnLietKe_Click(btnLietKe, null);
        }

        private void btnLietKe_Click(object sender, EventArgs e) {
            //1. lấy dữ liệu từ form
            dtpThang.Update();
            currMonth = dtpThang.Value;
            DateTime ngayBD = new DateTime(currMonth.Year, currMonth.Month, 1);
            DateTime ngayKT = new DateTime(currMonth.Year,currMonth.Month, DateTime.DaysInMonth(currMonth.Year, currMonth.Month));
            //-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

            dgrdDSNVTrgPhg.EndEdit();
            dgrdDSNVTrgPhg.Update();

            //2. lấy danh sách nhân viên check
            DataTable table = dgrdDSNVTrgPhg.DataSource as DataTable;
            if (table == null) return;
            DataRow[] arrRecord = table.Select("check = true", "UserEnrollNumber asc", DataViewRowState.CurrentRows);
            if (arrRecord.Length == 0) {
                AutoClosingMessageBox.Show("Chưa chọn Nhân viên", "Thông báo", 2000);
                return;
            }
            object[] arrDSNVCheck = new object[arrRecord.Length];

            for (int i = 0; i < arrRecord.Length; i++) {
                DataRow row = arrRecord[i];
                arrDSNVCheck[i] = ((int)row["UserEnrollNumber"]);
            }

            DataTable dataTable = dgrdNgayVang.DataSource as DataTable;
            if (dataTable != null) dataTable.Rows.Clear();
            dataTable = XL.LietKeNgayVangChoNV(arrDSNVCheck, ngayBD, ngayKT);
            dataTable.Columns.Add("check" , typeof (bool));
            dgrdNgayVang.DataSource = dataTable;

            GC.Collect();
        }
    }
}
