using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v05.DTO;
using ChamCong_v05.Helper;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;

namespace ChamCong_v05.zMisc {
	public partial class Form1 : Form
	{
		public List<cUserInfo> m_DSNV;
		public DateTime m_NgayBD;
		public DateTime m_NgayKT;
		public DataTable m_TableGioThieuChamCong;

		public Form1() {
			InitializeComponent();
			//SqlDataAccessHelper.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=WiseEyeV5Express;Integrated Security=true";
			m_TableGioThieuChamCong = this.CreateDataTable_CIO_ThieuChamCong();
			GridLocalizer.Active = new VietGridLocalizer();
			Localizer.Active = new Localizer();
		}

		private DataTable CreateDataTable_CIO_ThieuChamCong() {
			DataTable kq = new DataTable();
			kq.Columns.Add("UserEnrollNumber", typeof (int));
			kq.Columns.Add("UserFullName", typeof (string));
			kq.Columns.Add("UserFullCode", typeof (string));
			kq.Columns.Add("Ngay", typeof (DateTime));
			kq.Columns.Add("GioVao", typeof (DateTime));
			kq.Columns.Add("GioRa", typeof (DateTime));
			kq.Columns.Add("cCheckInOut", typeof (cCheckInOut));
			return kq;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			XacDinhGioThieuChamCong(this.m_DSNV);
			gridControl1.DataSource = this.m_TableGioThieuChamCong;
		}

		public void XacDinhGioThieuChamCong(List<cUserInfo> listNhanVien)
		{
			if(listNhanVien == null) return;
			foreach (cUserInfo nhanvien in listNhanVien)
			{
				foreach (cNgayCong ngayCong in nhanvien.DSNgayCong.Where(item => item.Ngay >=m_NgayBD && item.Ngay <= m_NgayKT).ToList())
				{
					foreach (cCheckInOut CIO in ngayCong.DSVaoRa.Where(item => item.HaveINOUT < 0))
					{
						DataRow newRow = this.CreateDataRow_CIO_ThieuChamCong(this.m_TableGioThieuChamCong, nhanvien, ngayCong, CIO);
						m_TableGioThieuChamCong.Rows.Add(newRow);
					}
				}
			}
		}

		private DataRow CreateDataRow_CIO_ThieuChamCong(DataTable dataTable, cUserInfo Nhanvien, cNgayCong NgayCong, cCheckInOut CIO)
		{
			DataRow kq = dataTable.NewRow();
			kq["UserEnrollNumber"] = Nhanvien.MaCC ;
			kq["UserFullName"] = Nhanvien.TenNV ;
			kq["UserFullCode"] = Nhanvien.MaNV ;
			kq["Ngay"] = NgayCong.Ngay ;
			kq["GioVao"] = CIO.Vao != null ? CIO.Vao.Time : (object)DBNull.Value ;
			kq["GioRa"] = CIO.Raa != null ? CIO.Raa.Time : (object)DBNull.Value ;

			return kq;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DataTable table = gridView1.DataSource as DataTable;
			int[] arraySelectedRow = gridView1.GetSelectedRows();
			List<DataRow> listSelectedRow = arraySelectedRow.Select(index => gridView1.GetDataRow(index)).ToList();
			string temp = string.Empty;
			foreach (DataRow dataRow in listSelectedRow)
			{
				temp += string.Format("{0}; ", dataRow["UserEnrollNumber"]);
			}
			ACMessageBox.Show(temp,"",8000);
		}
	}
}
