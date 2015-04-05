using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ChamCong_v05.Helper;

namespace ChamCong_v05.UI {
	public partial class frmWarning : Form
	{
		public List<Warning> listWarning;
		public bool TiepTuc = false;
		#region hàm ko quan trọng

		public frmWarning()
		{
			InitializeComponent();
			dgrdCanhBao.AutoGenerateColumns = false;
		}

		private void btnThoat_Click(object sender, EventArgs e)
		{
			TiepTuc = false;
			Close();
		}

		#endregion

		private void frmWarning_Load(object sender, EventArgs e) {
			DataTable table = new DataTable();
			table.Columns.Add("CanhBao", typeof (string));
			table.Columns.Add("NoiDungCanhBao", typeof (string));
			foreach (Warning warning in listWarning)
			{
				DataRow row = table.NewRow();
				row["CanhBao"] = warning.CB;
				row["NoiDungCanhBao"] = warning.ND;
				table.Rows.Add(row);
			}
			dgrdCanhBao.DataSource = table;
		}

		private void btnTiepTuc_Click(object sender, EventArgs e)
		{
			TiepTuc = true;
			Close();
		}
	}
}
