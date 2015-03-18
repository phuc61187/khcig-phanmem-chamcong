using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v02.BUS;
using ChamCong_v02.DAO;

namespace ChamCong_v02 {
	public partial class frm_XacNhanNgayLVTinhPC100 : Form {
		readonly CheckBox checkAll_GridNgayVang = new CheckBox();

		void checkAll_CheckedChanged(object sender, EventArgs e) {
			//1. xác định datagrid nào đang check all
			DataGridView tempGrid;
			tempGrid = dgrdNgayNghi;

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

		public frm_XacNhanNgayLVTinhPC100() {
			InitializeComponent();
			dgrdNgayNghi.AutoGenerateColumns = false;
			ThamSo.VeCheckBox_CheckAll(dgrdNgayNghi, checkAll_GridNgayVang, checkAll_CheckedChanged, new Point(7, 3));

		}

		private void frm_XacNhanNgayLVTinhPC100_Load(object sender, EventArgs e) {
			DataTable table = DAL.LayDSLamViecNgayNghiChoDuyet();
			table.Columns.Add("check", typeof (bool));
			dgrdNgayNghi.DataSource = table;
		}

		private void btnDuyet_Click(object sender, EventArgs e) {
			DataTable table = dgrdNgayNghi.DataSource as DataTable;
			if (table == null || table.Rows.Count == 0) return;

			BindingContext[this.dgrdNgayNghi.DataSource].EndCurrentEdit();

			DataRow[] arrRows = table.Select("check = true");

			if (arrRows.Length == 0) return;

			bool flag = false; // true if error
			int id = -1;
			int kq = 0;
			foreach (DataRow row in arrRows) {
				id = (int)row["ID"];
				kq = DAL.DuyetLamViecNgayNghi(id);
				// kiểm tra nếu kq = 0 --> ko update được báo lỗi
				if (kq == 0) {
					flag = true;
					break;
				}
			}
			if (flag) {
				MessageBox.Show("Xảy ra lỗi trong quá trình thao tác. Vui lòng thử lại.", "Lỗi");
			}
			else {
				AutoClosingMessageBox.Show("Đã duyệt xong.", "Thông báo", 2000);
				// sau khi duyệt xong thì load lại
				table = DAL.LayDSLamViecNgayNghiChoDuyet();
				table.Columns.Add("check", typeof(bool));
				dgrdNgayNghi.DataSource = table;

			}
		}

		private void btnXoa_Click(object sender, EventArgs e) {
			DataTable table = dgrdNgayNghi.DataSource as DataTable;
			if (table == null || table.Rows.Count == 0) return;

			BindingContext[this.dgrdNgayNghi.DataSource].EndCurrentEdit();

			DataRow[] arrRows = table.Select("check = true");

			if (arrRows.Length == 0) return;
			if (MessageBox.Show("Bạn muốn xoá các ngày làm việc tính PC 100% của Nhân viên?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK) {
				bool flag = DAL.XoaLamViecNgayNghiDaKhaiBao(arrRows);
				if (flag == false) {
					MessageBox.Show("Xảy ra lỗi trong quá trình thao tác. Vui lòng thử lại.", "Lỗi");
				}
				else {
					// sau khi xoá thì load lại table
					table = DAL.LayDSLamViecNgayNghiChoDuyet();
					table.Columns.Add("check", typeof(bool));

					dgrdNgayNghi.DataSource = table;
				}
			}

		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}
