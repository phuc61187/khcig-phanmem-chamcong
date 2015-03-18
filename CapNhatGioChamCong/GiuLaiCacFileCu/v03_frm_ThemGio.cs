using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using GiuLaiCacFileCu.BUS;
using GiuLaiCacFileCu.DAO;
using GiuLaiCacFileCu.DTO;

namespace ChamCong_v03 {
    public partial class v03_frm_ThemGio : Form {
		// sau khi thực hiện thêm giờ xong thì đóng form để form chính load lại giờ chấm công

        public DataRow[] DSNgayCongChked { get; set; }
        public bool IsReload { get; set; }
        public v03_frm_ThemGio() {
            InitializeComponent();
        }

        private void frm_ThemGio2_Load(object sender, EventArgs e) {
            // mặc định ko reload lại xem công nếu ko thực hiện thao tác
            IsReload = false;

            //1 load 2 combobox danh sach ca
            List<cCaChuan> dataCa_Vao = new List<cCaChuan>(XL.DSCa);
            dataCa_Vao.Insert(0, new cCaChuan() { ID = 0, Code = "Tuỳ chỉnh",});
            cbCa_Vao.DisplayMember = "ShiftCode";
            cbCa_Vao.DataSource = dataCa_Vao;
            List<cCaChuan> dataCa_Raa = new List<cCaChuan>(XL.DSCa);
            dataCa_Raa.Insert(0, new cCaChuan() { ID = 0, Code = "Tuỳ chỉnh",});
            cbCa_Raa.DisplayMember = "ShiftCode";
            cbCa_Raa.DataSource = dataCa_Raa;
            // gio đe mac dinh 0:00
            dtpGio_Vao.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            dtpGio_Raa.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);

            // dang ky su kien check vao ra, combo ca , datetime gio
            cbCa_Vao.SelectionChangeCommitted += cbCa_Vao_SelectionChangeCommitted;
            dtpGio_Vao.ValueChanged += dtpGio_Vao_ValueChanged;

            cbCa_Raa.SelectionChangeCommitted += cbCa_Raa_SelectionChangeCommitted;
            dtpGio_Raa.ValueChanged += dtpGio_Raa_ValueChanged;

        }

        void dtpGio_Vao_ValueChanged(object sender, EventArgs e) {
            if (cbCa_Vao.SelectedIndex == 0) return; // neu chon tuỳ chinh thi thoat ko lam gi het

            cCaChuan ca = cbCa_Vao.SelectedItem as cCaChuan; // lay ca duoc chon và kiem tra xem gio vao có nằm trong khoảng hiểu ca ko.
            // neu ko hieu ca chuyen combo ve tùy chỉnh

            if (ca == null) return;
            DateTime x = dtpGio_Vao.Value;
            if (x.TimeOfDay >= ca.OnnInnTS.Subtract(new TimeSpan(ca.DayCount, 0, 0, 0))
                && x.TimeOfDay <= ca.CutInnTS.Subtract(new TimeSpan(ca.DayCount, 0, 0, 0))) {
                // nằm trong khoảng hiểu thì ko làm gì hết
            }
            else {// nằm ngoài khoảng hiểu, chuyển về tùy chọn tùy chỉnh
                cbCa_Vao.SelectedIndex = 0;
            }
        }
        void dtpGio_Raa_ValueChanged(object sender, EventArgs e) {
            if (cbCa_Raa.SelectedIndex == 0) return; // neu chon tuỳ chinh thi thoat ko lam gi het

            cCaChuan ca = cbCa_Raa.SelectedItem as cCaChuan; // lay ca duoc chon và kiem tra xem gio Raa có nằm trong khoảng hiểu ca ko.

            if (ca == null) return;
            DateTime x = dtpGio_Raa.Value;

            if (x.TimeOfDay >= ca.OnnOutTS.Subtract(new TimeSpan(ca.DayCount, 0, 0, 0))
                && x.TimeOfDay <= ca.CutOutTS.Subtract(new TimeSpan(ca.DayCount, 0, 0, 0))) {
                // nằm trong khoảng hiểu thì ko làm gì hết
            }
            else {// nằm ngoài khoảng hiểu, chuyển về tùy chọn tùy chỉnh
                cbCa_Raa.SelectedIndex = 0;
            }
        }




        private void cbCa_Vao_SelectionChangeCommitted(object sender, EventArgs e) {
            if (cbCa_Vao.SelectedIndex == 0) return; // neu chon tuỳ chinh thi thoat ko lam gi het

            cCaChuan ca = cbCa_Vao.SelectedItem as cCaChuan; // lay ca chon

            if (ca == null) return;
            // gán giờ theo ca, huỷ rồi bật lại sự kiện datetimepicker change
            dtpGio_Vao.ValueChanged -= dtpGio_Vao_ValueChanged;
            DateTime x = DateTime.Today.Add(ca.OnnTS);

            dtpGio_Vao.Value = x;
            dtpGio_Vao.Update();
            dtpGio_Vao.ValueChanged += dtpGio_Vao_ValueChanged;

        }
        private void cbCa_Raa_SelectionChangeCommitted(object sender, EventArgs e) {
            if (cbCa_Raa.SelectedIndex == 0) return; // neu chon tuỳ chinh thi thoat ko lam gi het

            cCaChuan ca = cbCa_Raa.SelectedItem as cCaChuan; // lay ca chon

            if (ca == null) return;
            // gán giờ theo ca, huỷ rồi bật lại sự kiện datetimepicker change
            dtpGio_Raa.ValueChanged -= dtpGio_Raa_ValueChanged;
            DateTime x = DateTime.Today.Add(ca.OffTS);

            dtpGio_Raa.Value = x;
            dtpGio_Raa.Update();
            dtpGio_Raa.ValueChanged += dtpGio_Raa_ValueChanged;

        }
        #region enable/disable datetimepicker ngày
        private void checkNgayVao_CheckedChanged(object sender, EventArgs e) {
            dtpNgay_Vao.Enabled = checkNgay_Vao.Checked;
        }
        private void checkNgay_Raa_CheckedChanged(object sender, EventArgs e) {
            dtpNgay_Raa.Enabled = checkNgay_Raa.Checked;
        }

        #endregion

        private void btnThemGio_Vao_Click(object sender, EventArgs e) {
            // lấy thông tin 1. ca, 2. gio vao, 3. ngay co dinh if check, ly do, ghi chu
            TimeSpan giovao = dtpGio_Vao.Value.TimeOfDay;
			string lydo_vao = (cbLyDo_Vao.SelectedItem != null) ? cbLyDo_Vao.SelectedItem.ToString() : cbLyDo_Vao.Text;
            string ghichu_vao = tbGhiChu_Vao.Text;

            // nếu thực hiện thì bật yêu cầu reload lại form xem công
            IsReload = true;

            // biến kq thực hiện thành công hay thất bại
            bool flag = true;
            int iUserEnrollNumber = -1;
            string name = string.Empty;
            DateTime ngay = DateTime.MinValue;
            // duyệt qua từng ngày để thêm giờ, trong quá trình thêm nếu xảy ra lỗi thì thoát ra.
            for (int i = 0; i < DSNgayCongChked.Length; i++) {
                DataRow row = DSNgayCongChked[i];
                iUserEnrollNumber = (int)row["UserEnrollNumber"];
                ngay = (checkNgay_Vao.Checked) ? dtpNgay_Vao.Value.Date : (DateTime)row["TimeStrNgay"];
                ngay = ngay.Add(giovao);
                bool kq = DAL.ThemGioChoNV(iUserEnrollNumber, ngay, "I", 21, ThamSo.currUserID, lydo_vao, ghichu_vao);
                if (kq == false) {
                    name = row["UserFullName"].ToString();
                    flag = false;
                    break;
                }
            }
            // nếu xảy ra lỗi thì báo, ko thì thông báo thành công
            if (flag == false) {
                MessageBox.Show("Xảy ra lỗi trong quá trình thêm giờ kể từ Nhân viên: " + name + " vào ngày " + ngay.ToString("dd/MM/yyyy"), "Lỗi");
            }
            else {
                AutoClosingMessageBox.Show("Thực hiện thành công.", "Thông báo", 2000);
            }
        }

        private void btnThemGio_Raa_Click(object sender, EventArgs e) {
            TimeSpan gioRaa = dtpGio_Raa.Value.TimeOfDay;
			string lydo_Raa = (cbLyDo_Raa.SelectedItem != null) ? cbLyDo_Raa.SelectedItem.ToString() : cbLyDo_Raa.Text;
            string ghichu_Raa = tbGhiChu_Raa.Text;

            // nếu thực hiện thì bật yêu cầu reload lại form xem công
            IsReload = true;
            bool flag = true;
            int iUserEnrollNumber = -1;
            string name = string.Empty;
            DateTime ngay = DateTime.MinValue;

            // duyệt qua từng ngày để thêm giờ, trong quá trình thêm nếu xảy ra lỗi thì thoát ra.
            for (int i = 0; i < DSNgayCongChked.Length; i++) {
                DataRow row = DSNgayCongChked[i];
                iUserEnrollNumber = (int)row["UserEnrollNumber"];
                ngay = (checkNgay_Raa.Checked) ? dtpNgay_Raa.Value.Date : (DateTime)row["TimeStrNgay"];
                ngay = ngay.Add(gioRaa);
                bool kq = DAL.ThemGioChoNV(iUserEnrollNumber, ngay, "O", 22, ThamSo.currUserID, lydo_Raa, ghichu_Raa);
                if (kq == false) {
                    name = row["UserFullName"].ToString();
                    flag = false;
                    break;
                }
            }
            // nếu xảy ra lỗi thì báo, ko thì thông báo thành công
            if (flag == false) {
                MessageBox.Show("Xảy ra lỗi trong quá trình thêm giờ kể từ Nhân viên: " + name + " vào ngày " + ngay.ToString("dd/MM/yyyy"), "Lỗi");
            }
            else {
                AutoClosingMessageBox.Show("Thực hiện thành công.", "Thông báo", 2000);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e) {
            this.Close();
        }

    }
}
