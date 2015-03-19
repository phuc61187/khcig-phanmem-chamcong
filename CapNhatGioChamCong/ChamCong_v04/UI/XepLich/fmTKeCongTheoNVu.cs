using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;

namespace ChamCong_v04.UI.XepLich {
	public partial class Form2 : Form {

		public List<int> m_listIDPhongBan = new List<int>();

		public Form2() {
			InitializeComponent();

			log4net.Config.XmlConfigurator.Configure();
			//không cho autogen các column khi bind dữ liệu
			dgrdThongKe.AutoGenerateColumns = false;
			//test git
		}

		private void Form2_Load(object sender, EventArgs e) {
			//load mặc định các dateTimePicker
			DateTime ngayBD = MyUtility.FirstDayOfMonth(DateTime.Today);
			DateTime ngayKT = MyUtility.LastDayOfMonth(DateTime.Today);
			dtpNgayBD.Value = ngayBD;
			dtpNgayKT.Value = ngayKT;
			dtpThang.Value = ngayBD;
			numQuy.Value = (int) (DateTime.Today.Month - (DateTime.Today.Month%4) / 4);
			int thangDauQuy = (int)numQuy.Value
			dtpQuyNam = new DateTime(DateTime.Today.Year,thangDauQuy, 1);
		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			#region mỗi lần chọn node thì lấy ID node hiện tại và tất cả node con

			m_listIDPhongBan.Clear();
			e.Node.Expand();
			TreeNode topnode = XL.TopNode(e.Node); //đưa về root để thực hiện từ trên xuống
			if (topnode != null) XL.GetIDNodeAndChildNode1(e.Node, ref m_listIDPhongBan); // chỉ lấy các phòng ban được phép, 
			else {
				var temp = ((DataRow)e.Node.Tag);
				if ((int)temp["IsYes"] == 1) m_listIDPhongBan.Add((int)temp["ID"]);
			}

			#endregion
		}


	}
}
