using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace StudynTestNewTech {
	public partial class DemoCheckComboBoxEdit : Form {
		public DemoCheckComboBoxEdit() {
			InitializeComponent();
		}

		private void btnLoadDataSource_Click(object sender, EventArgs e)
		{
			DataTable tableNV = SqlDataAccessHelper.ExecuteQueryString("select * from UserInfo");
			checkedComboBoxEdit1.Properties.DataSource = tableNV;
			checkedComboBoxEdit1.Properties.DisplayMember = "UserFullName";
			checkedComboBoxEdit1.Properties.ValueMember = "UserEnrollNumber";
		}

		private void DemoCheckComboBoxEdit_Load(object sender, EventArgs e) {
			SqlDataAccessHelper.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=WiseEyeV5Express;Integrated Security=true";
		}

		private void btnGetItem_Click(object sender, EventArgs e)
		{
			richTextBox1.Clear();
			var item = checkedComboBoxEdit1.Properties.GetCheckedItems();
			richTextBox1.Text= item.ToString(); 
		}
		
	}
}
