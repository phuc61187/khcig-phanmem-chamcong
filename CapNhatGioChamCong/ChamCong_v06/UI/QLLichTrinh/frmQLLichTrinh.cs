using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.Helper;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace ChamCong_v06.UI.QLLichTrinh {
	public partial class frmQLLichTrinh : Form {
		#region hàm ko quan trọng
		public frmQLLichTrinh() {
			InitializeComponent();
		}

		private void btnDong_Click(object sender, EventArgs e) {
			Close();
		}
		#endregion

		private void btnThemLichTrinh_Click(object sender, EventArgs e) {
			frmTTLichTrinh frm = new frmTTLichTrinh();
			frm.Mode = ModeType.Them;
			frm.ShowDialog();
			if (frm.Mode == ModeType.Cancel) return;

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.Schedule_ThemLichTrinh.ToString(), 
				new SqlParameter("@SchName", frm.m_TenLichTrinh),
				new SqlParameter("@InOutID", 2));
			if (kq == 0)
			{
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
				return;
			}
			LoadDSLichTrinh();
			
		}

		private void btnXoaLichTrinh_Click(object sender, EventArgs e)
		{
			int[] selRows = ((GridView) gridControl1.MainView).GetSelectedRows();
			DataRowView selectedDataRowView =(DataRowView) ((GridView) gridControl1.MainView).GetRow(selRows[0]);
			DataRow selectedDataRow = selectedDataRowView.Row;
			var id = (int)selectedDataRow["SchID"];

			XuLyXoaLichTrinh(id);
			LoadDSLichTrinh();
		}

		private void XuLyXoaLichTrinh(int id) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.Other_XoaLichTrinh.ToString(), new SqlParameter("@SchID", id));
			if (kq == 0) {
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
			}
		}

		private void btnThemCa_Click(object sender, EventArgs e) {

		}

		private void btnXoaCa_Click(object sender, EventArgs e) {

		}

		public void LoadDSLichTrinh()
		{
			gridView1.SelectionChanged -= GridView1OnSelectionChanged;
			DataTable table = SqlDataAccessHelper.ExecSPQuery(SPName6.Schedule_DocLichTrinh.ToString());
			gridControl1.DataSource = table;
			gridView1.SelectionChanged +=GridView1OnSelectionChanged;
			
		}

		private void GridView1OnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
		{
			int[] selRows = (gridView1.GetSelectedRows());
			if (selRows.Count() == 0) gridControl2.DataSource = null;
			DataRow dataRow = gridView1.GetDataRow(selRows[0]);
			int schID = (int) dataRow["SchID"];
			DataTable tableDSCa = SqlDataAccessHelper.ExecSPQuery(SPName6.ShiftSch_DocDSCa.ToString(),
			                                                      new SqlParameter("@SchID", schID),
			                                                      new SqlParameter("@Enable", true));
			gridControl2.DataSource = tableDSCa;
		}

		private void frmQLLichTrinh_Load(object sender, EventArgs e) {
			LoadDSLichTrinh();
		}
	}
}
