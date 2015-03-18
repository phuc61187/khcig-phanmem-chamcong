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

			#region kiểm tra kết nối csdl , nếu mất kết nối thì đóng

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}

			#endregion

		}


	}
}
