using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v04.DTO;

namespace ChamCong_v04.UI.ChamCong {
	public partial class frmTest : Form
	{
		public ThongKeCong_PC thongKeThang;
		public List<cNgayCong> dsNgayCong;
		public frmTest() {
			InitializeComponent();
			dataGridView1.AutoGenerateColumns = true;
		}

		private void frmTest_Load(object sender, EventArgs e)
		{
			dataGridView1.DataSource = dsNgayCong;

		}
	}
}
