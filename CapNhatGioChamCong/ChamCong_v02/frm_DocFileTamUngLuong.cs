using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.AccessControl;
using System.Windows.Forms;
using ChamCong_v02.BUS;
using ChamCong_v02.DAO;
using ChamCong_v02.DTO;
using OfficeOpenXml;
using log4net;

namespace ChamCong_v02 {
	public partial class frm_DocFileTamUngLuong : Form {
		public List<cUserInfo> m_dsnv;
		public bool temp; // biến này để cho biết kết quả form này đọc file và gán giá trị có thành công ko. 
		// nếu ko đọc được thì báo false

		public frm_DocFileTamUngLuong() {
			InitializeComponent();
		}

		private void frm_DocFileTamUngLuong_Load(object sender, EventArgs e) {

		}

		private DataTable WorksheetToDataTable(ExcelWorksheet oSheet) {
			int totalRows = oSheet.Dimension.End.Row;
			int totalCols = oSheet.Dimension.End.Column;
			DataTable dt = new DataTable(oSheet.Name);
			dt.Columns.Add("UserFullCode", typeof(string));
			dt.Columns.Add("UserFullName", typeof(string));
			dt.Columns.Add("TamUng", typeof(Double));
			dt.Columns.Add("ThuChiKhac", typeof (Double));
			object[] arr = new object[4]; // tạo mảng 3 phần tử tương ứng 1 dòng dữ liệu để fill vào datatable

			int i = 0, j = 0;
			for (i = 2; i <= totalRows; i++) { //excel bắt đầu từ dòng 1 chhứ ko bắt đầu từ 0
				//i=2 , bỏ qua i=1 vì hàng đầu là tên cột
				DataRow row = dt.NewRow();

				for (j = 1; j <= totalCols; j++) {
					row[j - 1] = oSheet.Cells[i, j].Value;
				}
				dt.Rows.Add(row);
			}
			return dt;
		}

		private void btnBrowseTamUng_Click(object sender, EventArgs e) {

			string filePath = null;
			dialogOpenExcel.Filter = "Excel Files|*.xlsx";
			if (dialogOpenExcel.ShowDialog() == DialogResult.OK) {
				filePath = dialogOpenExcel.FileName;
				tbPathTamUngLuong.Text = filePath;
			}
			else return;

		}

		private void btnDocFile_Click(object sender, EventArgs e) {
			// đọc từ file tạm ứng, lưu biến đếm số lượng nhân viên tìm thấy được và gán vào biến tạm ứng trong danh sách nhân viên.
			bool flag = true;
			try {
				using (ExcelPackage excelPkg = new ExcelPackage()) {
					using (FileStream stream = new FileStream(dialogOpenExcel.FileName, FileMode.Open)) {
						excelPkg.Load(stream);

						ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets[1];
						DataTable table = WorksheetToDataTable(oSheet);
						cUserInfo nhanvien = null;
						int dem = 0;
						foreach (DataRow row in table.Rows) {
							nhanvien = m_dsnv.Find(o => o.UserFullCode == row["UserFullCode"].ToString());
							if (nhanvien == null) continue; // tìm ko thấy thì chuyển sang nhân viên tiếp theo
							// neu tim thay thi tang bien dem
							dem++;
							nhanvien.Luong.TamUng = Convert.ToDouble(row["TamUng"].ToString());
							nhanvien.Luong.ThuChiKhac = Convert.ToDouble(row["ThuChiKhac"].ToString());
						}
					}
				}
				temp = true; // cho biết form này đọc file thành công.
			} catch (Exception exception) {
				log4net.Config.XmlConfigurator.Configure();
				ILog lg = LogManager.GetLogger("frm_DocFileTamUng");
				string temp = "file name=" + dialogOpenExcel.FileName;
				lg.Error(temp, exception);
				flag = false;
			}
			// nếu trong qquá trình đọc file xảy ra lỗi thì báo
			// nếu đọc file thành công thì thoát
			if (flag == false) {
				temp = false; // cho biết form này đọc file bị lỗi.
				MessageBox.Show("Xảy ra lỗi trong quá trình đọc file tạm ứng lương. Vui lòng kiểm tra lại file.", "Lỗi");
			}
			else {
				temp = true;// cho biết form này đọc file thành công
				AutoClosingMessageBox.Show("Đọc file thành công. Hãy chọn thư mục lưu trữ bảng lương.", "Thông báo", 2000);
				this.Close();
			}
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			temp = false;
			this.Close();
		}


	}
}
