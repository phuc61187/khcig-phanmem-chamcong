using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.Helper;
using ChamCong_v05.Properties;

namespace ChamCong_v05.UI4._5 {
	public partial class fmXemCongChiTiet : Form {
		public List<int> m_listCurrentIDPhg = new List<int>();

		public fmXemCongChiTiet() {
			InitializeComponent();
		}

		private void fmXemCong4_Load(object sender, EventArgs e) {
			#region kiểm tra kết nối csdl , nếu mất kết nối thì đóng
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}
			#endregion

			/* 1. load tree phòng ( datasource cho checkComboEdit chọn nv do hàm treeAfterselect tạo
			 * 2. 
			 * 3. set tháng khi load lần đầu
			 */
			XL.loadTreePhgBan(treePhongBan);
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;

		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			#region mỗi lần chọn node thì lấy ID node hiện tại và tất cả node con

			m_listCurrentIDPhg.Clear();
			e.Node.Expand();
			TreeNode topnode = XL.TopNode(e.Node); //đưa về root để thực hiện từ trên xuống
			if (topnode != null) XL.GetIDNodeAndChildNode1(e.Node, ref m_listCurrentIDPhg); // chỉ lấy các phòng ban được phép, 
			else {
				var temp = ((DataRow)e.Node.Tag);
				if ((int)temp["IsYes"] == 1) m_listCurrentIDPhg.Add((int)temp["ID"]);
			}

			#endregion

			/* 1. clear select chọn nhân viên trước
			 * 2. load datasource cho check chọn nhân viên 
			 * 3. gán lại tên cho các column ngày
			 */
//			checkedComboBoxEdit1.Properties.Items.Clear();
			DataTable tableMaPhong = MyUtility.Array_To_DataTable("ArrUserIDD", m_listCurrentIDPhg);
			DataTable tableNhanVien = SqlDataAccessHelper.ExecSPQuery(SPName.sp_UserInfo_DocDSNVThaoTac.ToString(), new SqlParameter("@ArrUserIDD", SqlDbType.Structured){Value = tableMaPhong});
			checkedDSNV.Properties.DataSource = tableNhanVien;
			checkedDSNV.Properties.DisplayMember = "DisplayItem";
			checkedDSNV.Properties.ValueMember = "UserEnrollNumber";

			for (int i = 3; i < 34; i++) // bắt đầu từ 3 vì 1 mã cc, 2 mã nv, 3 tên nv
			{
				dgrdTongHop.Columns[i].HeaderText = string.Empty;
			}
		}

		private void btnChamCong_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			/* 1. lấy dsnv check, lấy tháng
			 * 
			 */
			string strChecked_ArrMaCC = checkedDSNV.EditValue.ToString();
			List<int> arrMaCC = new List<int>();
			strChecked_ArrMaCC.Split(new char[]{','}).ToList().ForEach(item=>arrMaCC.Add(int.Parse(item)));

		}

		private void label3_Click(object sender, EventArgs e) {

		}

	}
}
