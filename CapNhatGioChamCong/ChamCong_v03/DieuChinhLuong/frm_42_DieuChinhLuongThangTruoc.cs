using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using log4net;

namespace ChamCong_v03.DieuChinhLuong {
	public partial class frm_42_DieuChinhLuongThangTruoc : Form {

		public readonly ILog lg = LogManager.GetLogger("frm_42_DieuChinhLuongThangTruoc");
		public DateTime m_thang = DateTime.MinValue;

		public frm_42_DieuChinhLuongThangTruoc() {  
			InitializeComponent();

			log4net.Config.XmlConfigurator.Configure();
			dgrdDSNVTrgPhg.AutoGenerateColumns = false;
		}

		private void frm_DieuChinhLuongThangTruoc_Load(object sender, EventArgs e)
		{
            dtpThang.Value = new DateTime(m_thang.Year, m_thang.Month, 1);
		}

		private void btnXem_Click(object sender, EventArgs e)
		{
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

            // set datasource for dgrd
			var thang = dtpThang.Value;
			var table = DAL.LayBangDieuChinhLuong(thang);
            var view = new DataView(table);

			dgrdDSNVTrgPhg.DataSource = view;

            // set autocomplete text for tbsearch;
            AutoCompleteStringCollection list = new AutoCompleteStringCollection();
            list.AddRange((from row in table.Rows.Cast<DataRow>()select row["UserFullName"].ToString().ToUpperInvariant()).ToArray());
            tbSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		    tbSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbSearch.AutoCompleteCustomSource = list;
		}
		
        private void btnThemMoi_Click(object sender, EventArgs e) {

			frmThemDieuChinhLuong frm = new frmThemDieuChinhLuong{m_thang = dtpThang.Value};
			frm.Location = new Point((int)((this.Size.Width - frm.Size.Width) / 2f), (int)((this.Size.Height - frm.Size.Height) / 2f));
			frm.ShowDialog();
			if (frm.IsReload)
			{
				Thread.Sleep(20);
				btnXem.PerformClick();
			}
		}

		private void btnCapNhat_Click(object sender, EventArgs e)
		{
			if (dgrdDSNVTrgPhg.SelectedRows.Count == 0) return;
			IEnumerable<DataGridViewRow> dataGridViewRows = dgrdDSNVTrgPhg.SelectedRows.Cast<DataGridViewRow>();
			var arrRecord = (from row in dataGridViewRows
							select ((DataRowView)row.DataBoundItem).Row).ToArray();

			var thang = dtpThang.Value;
			frm_CapNhat frm = new frm_CapNhat();
			frm.Row = arrRecord;
			frm.thang = thang;
			frm.Location = new Point((int)((this.Size.Width - frm.Size.Width) / 2f), (int)((this.Size.Height - frm.Size.Height) / 2f));
			frm.ShowDialog();
			if (frm.IsReload)
			{
				Thread.Sleep(20);
				btnXem.PerformClick();
			}
		}

		private void btnDocTuExcel_Click(object sender, EventArgs e)
		{
			frmDocTuFileExcel frm = new frmDocTuFileExcel{m_Thang = dtpThang.Value};
			frm.Location = new Point((int)((this.Size.Width - frm.Size.Width) / 2f), (int)((this.Size.Height - frm.Size.Height) / 2f));
			frm.ShowDialog();
			if (frm.IsReload)
			{
				Thread.Sleep(20);
				btnXem.PerformClick();
			}

		}

		private void btnTim_Click(object sender, EventArgs e) {
            if (dgrdDSNVTrgPhg.DataSource == null) return;
            var view = dgrdDSNVTrgPhg.DataSource as DataView;
			var keyword = tbSearch.Text;
		    var filter = "UserFullCode like '%" + keyword + "%' or UserFullName like '%" + keyword + "%'";
		    view.RowFilter = filter;
		}

        private void linkHienThiTatCaNV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dgrdDSNVTrgPhg.DataSource == null) return;
            var view = dgrdDSNVTrgPhg.DataSource as DataView;
            view.RowFilter = string.Empty;
        }

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}


	}
}
