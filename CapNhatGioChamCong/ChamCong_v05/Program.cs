using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v05.UI;
using ChamCong_v05.UI4._5;
using ChamCong_v05.zMisc;

namespace ChamCong_v05 {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmDangNhap());
		}
	}
}
