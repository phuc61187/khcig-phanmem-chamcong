using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ChamCong_v05.Helper;

namespace ChamCong_v05.UI {
	public partial class frmError : Form
	{
		public List<Error> listError;
		#region hàm ko quan trọng

		public frmError()
		{
			InitializeComponent();
			dgrdLoi.AutoGenerateColumns = false;
		}

		private void btnThoat_Click(object sender, EventArgs e)
		{
			Close();
		}

		#endregion

		private void frmError_Load(object sender, EventArgs e) {
			DataTable table = new DataTable();
			table.Columns.Add("Loi", typeof (string));
			table.Columns.Add("NoiDungLoi", typeof (string));
			foreach (Error error in listError)
			{
				DataRow row = table.NewRow();
				row["Loi"] = error.L;
				row["NoiDungLoi"] = error.ND;
				table.Rows.Add(row);//
			}
			dgrdLoi.DataSource = table;
		}

	}
}
