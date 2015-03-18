using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;
using log4net;

namespace ChamCong_v03 {

	public partial class frm_111_ChiTietVaoRa : Form {
		private readonly ILog lg = LogManager.GetLogger("frm_111_ChiTietVaoRa");
		public bool IsReload;
		public List<cUserInfo> m_DSNV;
		public DataRow[] DSNgayCongChecked;


		public frm_111_ChiTietVaoRa() {
			InitializeComponent();
			dgrdTongHop.AutoGenerateColumns = false;
			tabControl1.SelectedIndex = 0;
		}

		private void frm_ChiTietVaoRa_Load(object sender, EventArgs e) {
			IsReload = false; // mặc định ko có gì thay đổi, nếu có sử dụng chức năng thêm, xoá sửa, đảo thì this.DialogResult = Yes;

		}






		private void dgrdTongHop_SelectionChanged(object sender, EventArgs e) {
		}



	}
}
