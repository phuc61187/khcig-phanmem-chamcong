using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.DAL;
using ChamCong_v05.Helper;
using ChamCong_v05.Properties;
using log4net;
using OfficeOpenXml;

namespace ChamCong_v05.UI.TinhLuong {
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
			//object[] arr = new object[5]; // tạo mảng 5 phần tử tương ứng 1 dòng dữ liệu để fill vào datatable

			dt = new DataTable(oSheet.Name);
			dt.Columns.Add("UserFullCode", typeof (string));
			dt.Columns.Add("UserFullName", typeof (string));
			dt.Columns.Add("LuongDieuChinh", typeof(double));
			dt.Columns.Add("TamUng", typeof(double));
			dt.Columns.Add("ThuChiKhac", typeof(double));
			dt.Columns.Add("MucDongBHXH", typeof(double));

			int i;
			flag = false;
			for (i = 2; i <= totalRows; i++)
			{
				//excel bắt đầu từ dòng 1 chhứ ko bắt đầu từ 0
				//i=2 , bỏ qua i=1 vì hàng đầu là tên cột
				DataRow row = dt.NewRow();

				int j;
				for (j = 1; j <= totalCols; j++)
				{
					if (j < 3) {
						row[j - 1] = oSheet.Cells[i, j].Value; // 2 cột tên và mã ko cần Parse
}
					else
					{
						#region thử Parse giá trị, nếu Parse thất bại thì bật cờ dừng để báo

						double giatri;
						if (double.TryParse(oSheet.Cells[i, j].Value.ToString(), out giatri))
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

		private void btnBrowse_Click(object sender, EventArgs e)
		{

			dialogOpenExcel.Filter = @"Excel Files|*.xlsx";
			if (dialogOpenExcel.ShowDialog() != DialogResult.OK) return;
			try
			{
				bool flagError;
				List<int> InvalidRows;
				DataTable table;
				int tongNV;
				double tongTamUng, tongLuongDieuchinh, tongThuchikhac, tongMucdongBHXH;
				using (var excelPkg = new ExcelPackage())
				{
					using (var stream = new FileStream(dialogOpenExcel.FileName, FileMode.Open))
					{
						excelPkg.Load(stream);

						ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets[1];
						WorksheetToDataTable(oSheet, out InvalidRows, out flagError, out table);
						var distinctRows = (from DataRow row in table.Rows select row).DistinctBy(item => item["UserFullCode"].ToString().TrimStart().TrimEnd().ToLower()).ToList();
						tongNV = distinctRows.Count;
						tongTamUng = (from DataRow row in distinctRows select (double)row["TamUng"]).Sum();
						tongLuongDieuchinh = (from DataRow row in distinctRows select (double)row["LuongDieuChinh"]).Sum();
						tongThuchikhac = (from DataRow row in distinctRows select (double)row["ThuChiKhac"]).Sum();
						tongMucdongBHXH = (from DataRow row in distinctRows select (double)row["MucDongBHXH"]).Sum();
					}
				}
				if (flagError)
				{
					var chuoi = "Không thể đọc giá trị tại các dòng \n{0} ";
					var chuoi1 = string.Format(chuoi, InvalidRows.ToArray());
					MessageBox.Show(chuoi1, Resources.Caption_Loi, MessageBoxButtons.OK);
				}
				else
				{
					dgrdDThuchiExcel.DataSource = table;
					lbTongNV.Text = string.Format("Tổng số NV: {0}", tongNV);
					lbTongTamung.Text = string.Format("Tổng tạm ứng: {0:###,###,###,##0.000}", tongTamUng);
					lbTongLuongdieuchinh.Text = string.Format("Tổng lương điều chỉnh: {0:###,###,###,##0.000}", tongLuongDieuchinh);
					lbTongThuchikhac.Text = string.Format("Tổng thu chi khác: {0:###,###,###,##0.000}", tongThuchikhac);
					lbTongMucDongBHXH.Text = string.Format("Tổng các mức đóng BHXH: {0:###,##0.00}", tongMucdongBHXH);
				}
			}
			catch (Exception ex)
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_LoiDocFileExcel, Resources.Caption_ThongBao, MessageBoxButtons.OK);
			}
		}


		private void btnCapNhatVaoCSDL_Click(object sender, EventArgs e)
		{
			IsReload = true;
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(m_Thang.Date, MyUtility.LastDayOfMonth(m_Thang))) {
				MessageBox.Show(string.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "chỉnh sửa thu chi tháng", "thực hiện thao tác", ""),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}

			#endregion


			var table = dgrdDThuchiExcel.DataSource as DataTable;
			DateTime thang = m_Thang;
			var flagError = false;

			#region xác nhận trước khi thực hiện

			if (
				MessageBox.Show(string.Format("Bạn muốn cập nhật cho tháng {0}?", thang.ToString("M/yyyy")), Resources.Caption_XacNhan,
				                MessageBoxButtons.YesNo) == DialogResult.No)
				return;

			#endregion

			if (table != null)
			{
				var tableTatCaNV = XL.LayDSNV(true);//tbd
				foreach (DataRow row in table.Rows)
				{
					#region lấy thông tin 

					var userFullcode = row["UserFullCode"].ToString().TrimStart().TrimEnd().ToLower();
					//var arrRows = tableTatCaNV.Select("UserFullCode = '" +userFullcode + "'","", DataViewRowState.CurrentRows);
					var resultRow = (tableTatCaNV.Rows.Cast<DataRow>().Where(
						dataRow => dataRow["UserFullCode"].ToString().TrimStart().TrimEnd().ToLower() == userFullcode)).SingleOrDefault();
					if (resultRow == null) continue;
					var tenNV = resultRow["UserFullName"].ToString();
					var UserEnrollNumber = (int)resultRow["UserEnrollNumber"];
					var luongdieuchinh = (double)row["LuongDieuChinh"];
					var tamung = (double)row["TamUng"];
					var thuchikhac = (double)row["ThuChiKhac"];
					var mucdongbhxhDouble = (double)row["MucDongBHXH"];
					var mucdongbhxhFloat = Convert.ToSingle(mucdongbhxhDouble);

					#endregion

					var kq = DAO5.CapnhatThuchiThang(UserEnrollNumber, thang, luongdieuchinh, tamung, thuchikhac, mucdongbhxhFloat);

					#region báo lỗi nếu ko cập nhật được

					if (kq == 0)
					{
						flagError = true;
						ACMessageBox.Show(string.Format("Xảy ra lỗi trong quá trình cập nhật tại vị trí nhân viên {0}, mã {1}.\nVui lòng thử lại.", tenNV,
							              userFullcode), "Lỗi", 3000);
						break;
					}

					#endregion
				}
				if (flagError == false)
				{
					ACMessageBox.Show("Thực hiện thành công.", "Thông báo", 2000);
				}
			}
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			Close();
		}

		private void frmDocTuFileExcel_Load(object sender, EventArgs e)
		{
			IsReload = false;
		}
	}
}
