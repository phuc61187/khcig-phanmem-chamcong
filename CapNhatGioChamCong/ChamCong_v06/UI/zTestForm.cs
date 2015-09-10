using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.DTO;
using ChamCong_v06.Helper;

namespace ChamCong_v06.UI {
	public partial class zTestForm : Form
	{
		public List<cUserInfo> m_DSNV;
		public zTestForm() {
			InitializeComponent();
		}

		private void zTestForm_Load(object sender, EventArgs e)
		{
			DataTable tableCheck = SqlDataAccessHelper.ExecuteQueryString("select * from CheckInOut ");
			gridControl1.DataSource = tableCheck;

			DataTable tableCIO = SqlDataAccessHelper.ExecuteQueryString("Select * from CIO");
			gridControl2.DataSource = tableCIO;
		}
	}
}
