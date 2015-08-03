using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.DAL;
using ChamCong_v06.Helper;
using DevExpress.XtraGrid.Views.Grid;

namespace ChamCong_v06.UI.QLLichTrinh {
	public partial class frmQLCa : Form {
		#region các hàm ko quan trọng

		public frmQLCa()
		{
			InitializeComponent();
		}

		private void btnThoat_Click(object sender, EventArgs e)
		{
			Close();
		}

		#endregion

		private void btnThem_Click(object sender, EventArgs e) {
			frmTTCaLamViec frm = new frmTTCaLamViec();
			frm.Mode = ModeType.Them;
			frm.ShowDialog();
			LoadGrid();
		}

		private void btnSua_Click(object sender, EventArgs e) {
			int[] selRows = ((GridView)gridControl.MainView).GetSelectedRows();
			DataRowView selRow = (DataRowView)(((GridView)gridControl.MainView).GetRow(selRows[0]));
			DataRow selectedRow = selRow.Row;
			frmTTCaLamViec frm = new frmTTCaLamViec();
			frm.m_CurrentRow = selectedRow;
			frm.Mode = ModeType.Sua;
			frm.ShowDialog();

		}

		private void btnXoa_Click(object sender, EventArgs e) {

		}

		private void frmQLCa_Load(object sender, EventArgs e) {
			LoadGrid();
		}

		public void LoadGrid() {
			//kiem tra ket noi csdl

			DataTable tableShift = DAO5.LoadDataSourceShift();
			gridControl.DataSource = tableShift;
		}


	}
}
