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

namespace ChamCong_v06.UI.QLLichTrinh {
	public partial class frmChonCa : Form {
		public ModeType m_Mode = ModeType.Cancel;
		public List<DataRow> m_SelectedRows = new List<DataRow>();
		public frmChonCa() {
			InitializeComponent();
		}

		private void btnHuy_Click(object sender, EventArgs e) {
			m_Mode = ModeType.Cancel;
			Close();
		}

		private void btnLuu_Click(object sender, EventArgs e) {
			this.m_Mode = ModeType.Them;
			int[] selectedRowsHandle = gridView2.GetSelectedRows();
			foreach (int rowHandle in selectedRowsHandle)
			{
				DataRow row = gridView2.GetDataRow(rowHandle);
				this.m_SelectedRows.Add(row);
			}
			Close();
		}

		private void frmChonCa_Load(object sender, EventArgs e)
		{
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				m_Mode = ModeType.Cancel;
				Close();
				return;
			}

			DataTable tableDSCa = SqlDataAccessHelper.ExecSPQuery(SPName6.Shift_DocDSCaEnableV6.ToString(), new SqlParameter("@Enable", true));
			gridControl2.DataSource = tableDSCa;
		}
	}
}
