using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using OfficeOpenXml;
using log4net;

namespace ChamCong_v03.DieuChinhLuong {
	public partial class frmDocTuFileExcel : Form
	{
		public readonly ILog lg = LogManager.GetLogger("frm_11_XemCong");
		public bool IsReload = false;
		public DateTime m_Thang;
		public frmDocTuFileExcel()
		{
			InitializeComponent();
			log4net.Config.XmlConfigurator.Configure();

		}

		private void WorksheetToDataTable(ExcelWorksheet oSheet, out List<int> InvalidRows, out bool flag, out DataTable dt)
		{
			int totalRows = oSheet.Dimension.End.Row;
			int totalCols = oSheet.Dimension.End.Column;
			InvalidRows = new List<int>();
			object[] arr = new object[5]; // tạo mảng 3 phần tử tương ứng 1 dòng dữ liệu để fill vào datatable

			dt = new DataTable(oSheet.Name);
			dt.Columns.Add("UserFullCode", typeof (string));
			dt.Columns.Add("UserFullName", typeof (string));
			dt.Columns.Add("LuongDieuChinh", typeof (Double));
			dt.Columns.Add("TamUng", typeof (Double));
			dt.Columns.Add("ThuChiKhac", typeof (Double));
			dt.Columns.Add("MucDongBHXH", typeof (Single));

			int i = 0, j = 0;
			flag = false;
			for (i = 2; i <= totalRows; i++)
			{
				//excel bắt đầu từ dòng 1 chhứ ko bắt đầu từ 0
				//i=2 , bỏ qua i=1 vì hàng đầu là tên cột
				DataRow row = dt.NewRow();

				for (j = 1; j <= totalCols; j++)
				{
					if (j < 3) row[j - 1] = oSheet.Cells[i, j].Value; // 2 cột tên và mã ko cần Parse
					else
					{
						#region thử Parse giá trị, nếu Parse thất bại thì bật cờ dừng để báo

						var giatri = 0d;
						if (Double.TryParse(oSheet.Cells[i, j].Value.ToString(), out giatri))
							row[j - 1] = oSheet.Cells[i, j].Value;
						else
						{
							flag = true;
							InvalidRows.Add(i);
						}

						#endregion
					}
				}
				dt.Rows.Add(row);
			}

		}

		private void button1_Click(object sender, EventArgs e)
		{

			dialogOpenExcel.Filter = "Excel Files|*.xlsx";
			if (dialogOpenExcel.ShowDialog() == DialogResult.OK)
			{
				try
				{
					var flagError = false;
					var InvalidRows = new List<int>();
					var table = new DataTable();
					using (var excelPkg = new ExcelPackage())
					{
						using (var stream = new FileStream(dialogOpenExcel.FileName, FileMode.Open))
						{
							excelPkg.Load(stream);

							ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets[1];
							WorksheetToDataTable(oSheet, out InvalidRows, out flagError, out table);
						}
					}
					if (flagError)
					{
						var chuoi = "Không thể đọc giá trị tại các dòng \n{0} ";
						var chuoi1 = string.Format(chuoi, InvalidRows.ToArray());
						MessageBox.Show(chuoi1, "Lỗi", MessageBoxButtons.OK);
						return;
					}
					else
					{
						dgrdDSNVTrgPhg.DataSource = table;
					}
				}
				catch (Exception exception)
				{
					XuLyException(exception);
				}
			}
		}

		private void XuLyException(Exception exception)
		{
			lg.Error("Tinh luong", exception);
			MessageBox.Show("Đọc file dữ liệu từ excel thất bại.\nVui lòng thử lại hoặc liên hệ bộ phận kỹ thuật để được trợ giúp.",
				"Thông báo", MessageBoxButtons.OK);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			IsReload = true;
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			var table = dgrdDSNVTrgPhg.DataSource as DataTable;
			var thang = dtpThang.Value;
			var flagError = false;

			#region xác nhận trước khi thực hiện

			if (
				MessageBox.Show(string.Format("Bạn muốn cập nhật cho tháng {0}?", thang.ToString("M/yyyy")), "Xác nhận",
				                MessageBoxButtons.YesNo) == DialogResult.No)
				return;

			#endregion

			if (table != null)
			{
				var tableTatCaNV = DAL.LayDSTatCaNV();
				foreach (DataRow row in table.Rows)
				{
					#region lấy thông tin 

					var userFullcode = row["UserFullCode"].ToString();
					var arrRows = tableTatCaNV.Select("UserFullCode = '" +userFullcode + "'","", DataViewRowState.CurrentRows);
					if (!arrRows.Any()) continue;
					var tenNV = arrRows[0]["UserFullName"].ToString();
					var UserEnrollNumber = (int) arrRows[0]["UserEnrollNumber"];
					var luongdieuchinh = (Double) row["LuongDieuChinh"];
					var tamung = (Double) row["TamUng"];
					var thuchikhac = (Double) row["ThuChiKhac"];
					var mucdongbhxh = (Single) row["MucDongBHXH"];

					#endregion

					var kq = DAL.CapNhatLuongDieuChinh_TamUng_ThuChiKhac(UserEnrollNumber, thang.Month, thang.Year, luongdieuchinh, tamung, thuchikhac, mucdongbhxh);

					#region báo lỗi nếu ko cập nhật được

					if (kq == 0)
					{
						flagError = true;
						AutoClosingMessageBox.Show(string.Format("Xảy ra lỗi trong quá trình cập nhật tại vị trí nhân viên {0}, mã {1}.\nVui lòng thử lại.", tenNV,
							              userFullcode), "Lỗi", 3000);
						break;
					}

					#endregion
				}
				if (flagError == false)
				{
					AutoClosingMessageBox.Show("Thực hiện thành công.", "Thông báo", 2000);
				}
			}
		}

		private void button3_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void frmDocTuFileExcel_Load(object sender, EventArgs e)
		{
			IsReload = false;
			dtpThang.Value = m_Thang;
		}
	}
}
