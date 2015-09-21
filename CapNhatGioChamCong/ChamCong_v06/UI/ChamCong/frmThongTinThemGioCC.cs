using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.DTO;

namespace ChamCong_v06.UI.ChamCong {
	public partial class frmThongTinThemGioCC : Form {
		public List<cCheckInOut_DaCC> m_DSVaoRa = new List<cCheckInOut_DaCC>();

		public frmThongTinThemGioCC() {
			InitializeComponent();
		}
	}
}
