using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DAL;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;
using log4net;
using OfficeOpenXml;

namespace ChamCong_v04.UI.TinhLuong {
	public partial class frmDocTuFileExcel2 : Form
	{
		public readonly ILog lg = LogManager.GetLogger("frm_11_XemCong");
		public bool IsReload = false;
		public DateTime m_Thang;
		public frmDocTuFileExcel2()
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
            dt.Columns.Add("UserFullCode", typeof(string));
            dt.Columns.Add("HSLCBTT17", typeof(float));
            dt.Columns.Add("HSPCCV", typeof(float));
            dt.Columns.Add("HSPCDH", typeof(float));
            dt.Columns.Add("HSPCTN", typeof(float));

            int i;
            flag = false;
            try
            {
                for (i = 2; i <= totalRows; i++)
                {
                    //excel bắt đầu từ dòng 1 chhứ ko bắt đầu từ 0
                    //i=2 , bỏ qua i=1 vì hàng đầu là tên cột
                    DataRow row = dt.NewRow();
                    int j;
                    for (j = 1; j <= totalCols; j++)
                    {
                        if (j < 2) row[j - 1] = oSheet.Cells[i, j].Value; // 2 cột tên và mã ko cần Parse

                        else
                        {
                            #region thử Parse giá trị, nếu Parse thất bại thì bật cờ dừng để báo

                            float giatri;
                            if (float.TryParse(oSheet.Cells[i, j].Value.ToString(), out giatri))
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
            catch (Exception ex)
            {
                Console.WriteLine("");
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
				float tongHSPCTN, tongHSLCBTT17, tongHSPCCV, tongHSPCDH;
				using (var excelPkg = new ExcelPackage())
				{
					using (var stream = new FileStream(dialogOpenExcel.FileName, FileMode.Open))
					{
						excelPkg.Load(stream);

						ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets[1];
						WorksheetToDataTable(oSheet, out InvalidRows, out flagError, out table);
						var distinctRows = (from DataRow row in table.Rows select row).DistinctBy(item => item["UserFullCode"].ToString().TrimStart().TrimEnd().ToLower()).ToList();
						tongNV = distinctRows.Count;
                        tongHSLCBTT17 = (from DataRow row in distinctRows select (float)row["HSLCBTT17"]).Sum();
                        tongHSPCCV = (from DataRow row in distinctRows select (float)row["HSPCCV"]).Sum();
                        tongHSPCDH = (from DataRow row in distinctRows select (float)row["HSPCDH"]).Sum();
                        tongHSPCTN = (from DataRow row in distinctRows select (float)row["HSPCTN"]).Sum();
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
					lbTongTamung.Text = string.Format("Tổng HSLCBTT17: {0:###,###,###,##0.000}", tongHSLCBTT17);
					lbTongLuongdieuchinh.Text = string.Format("Tổng HSPCCV: {0:###,###,###,##0.000}", tongHSPCCV);
					lbTongThuchikhac.Text = string.Format("Tổng HSPCDH: {0:###,###,###,##0.000}", tongHSPCDH);
					lbTongMucDongBHXH.Text = string.Format("Tổng HSPCTN: {0:###,##0.00}", tongHSPCTN);
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

			var table = dgrdDThuchiExcel.DataSource as DataTable;
			DateTime thang = m_Thang;
			var flagError = false;
            var kq2 = 0;
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
                    var userfc = row["UserFullCode"].ToString();
                    var HSLCBTT17 = (float)row["HSLCBTT17"];
					var HSPCCV = (float)row["HSPCCV"];
					var HSPCDH = (float)row["HSPCDH"];
					var HSPCTN = (float)row["HSPCTN"];

                    #endregion
                    var kq = SqlDataAccessHelper.ExecNoneQueryString(@"update UserInfo set HSLCBTT17 = @HSLCBTT17, HSPCCV=@HSPCCV, HSPCDH=@HSPCDH, HSPCTN=@HSPCTN where UserFullCode=@UserFullCode",
                         new string[] { "@HSLCBTT17", "@HSPCCV", "@HSPCDH", "@HSPCTN", "@UserFullCode" }, new object[] { HSLCBTT17, HSPCCV, HSPCDH, HSPCTN, userFullcode });

					#region báo lỗi nếu ko cập nhật được

					if (kq == 0)
					{
						flagError = true;
						ACMessageBox.Show(string.Format("Xảy ra lỗi trong quá trình cập nhật tại vị trí mã nhân viên {0} .\nVui lòng thử lại.", userFullcode), "Lỗi", 3000);
						break;
					}
                    kq2 += kq;
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
