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
	public partial class MainForm : Form {
		public MainForm() {
			InitializeComponent();
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
			Application.Exit();
		}

		private void thoatToolStripMenuItem_Click(object sender, EventArgs e) {
			while (this.MdiChildren.Any())
			{
				this.MdiChildren[0].Close();
			}
			frmLogin frmLogin = new frmLogin();
			frmLogin.ShowDialog();
		}

		private void MainForm_Load(object sender, EventArgs e) {
			frmLogin frmLogin = new frmLogin();
			frmLogin.ShowDialog();
			//frmLogin.MdiParent = this;
		}


	}
}
