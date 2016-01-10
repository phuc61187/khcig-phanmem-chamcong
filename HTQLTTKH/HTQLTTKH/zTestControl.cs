using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTQLTTKH {
	public partial class zTestControl : Form {
		public zTestControl() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			//using (WE_LinqToSQLDataContext context = new WE_LinqToSQLDataContext())
			//{
			//	relationDeptBindingSource.DataSource = context.RelationDepts;
			//}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var tempList = (List<object>) checkedComboBoxEdit1.EditValue; // ko dùng List<int>
			var chuoi1 = tempList.Aggregate(string.Empty, (current, i) => current + i.ToString());
			richTextBox1.Text += string.Format("EditValueType: {0} \nValue: {1}\n", checkedComboBoxEdit1.EditValue.GetType(), chuoi1);
		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			richTextBox1.Text += string.Format("Type:{0} Value:{1}", timeEdit1.EditValue.GetType(), timeEdit1.EditValue);
		}
	}
}
