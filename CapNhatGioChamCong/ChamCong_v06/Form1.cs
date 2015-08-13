using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.Helper;

namespace ChamCong_v06 {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
			ACMessageBox.Show("Test sourcetere", Resources.Caption_ThongBao, 2000);
		}
	}
}
