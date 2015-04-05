using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.DAL;
using ChamCong_v05.DTO;
using ChamCong_v05.Helper;
using ChamCong_v05.Properties;

namespace ChamCong_v05.UI.TinhLuong {
	public partial class frm_QLKetCongBoPhan : Form {
		public List<cUserInfo> m_DSNV = new List<cUserInfo>();
		public List<int> m_listIDPhongBan = new List<int>();

		#region hàm ko quan trong

		public frm_QLKetCongBoPhan() {
			InitializeComponent();
		}

		private void btnDong_Click(object sender, EventArgs e) {
			Close();
		}

		#endregion


		private void frm_KetCongBoPhan_Load(object sender, EventArgs e) {
			#region kiểm tra kết nối csdl , nếu mất kết nối thì đóng

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}

			#endregion

			//set default tháng đang chọn là trước 1 tháng so với hiện tại vì qua tháng mới kết công
			var firstDay_CurrMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
			var firstDay_PrevMonth = firstDay_CurrMonth.AddMonths(-1);
			dtpThang.Value = new DateTime(firstDay_PrevMonth.Year, firstDay_PrevMonth.Month, firstDay_PrevMonth.Day);
			// gọi hàm để load danh sách phòng ban đã kết công
			dtpThang_ValueChanged(dtpThang, null);
		}

		private void dtpThang_ValueChanged(object sender, EventArgs e) {
			var ngaydauthang = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);

			XL.loadTreePhgBan(treePhongBan, XL2.TatcaPhongban, XL2.TatcaPhongban);
			treePhongBan.ExpandAll();
			#region lấy ds phòng đã kết công và check sẵn
			DataTable table = DAO5.LayDSPhongDaKetcong(ngaydauthang);//tbd
			//duyệt từng rows trong table và check node có id phòng, 
			//chỉ check node có id tương ứng ko thực hiện check đệ quy
			for (int i = 0; i < table.Rows.Count; i++) {
				int idPhong = (int)table.Rows[i]["IDPhong"];
				setCheckedNode_PhongDaKetcong(idPhong, treePhongBan.Nodes[0]);
			}
			#endregion

		}

		private void setCheckedNode_PhongDaKetcong(int idPhong, TreeNode root) {
			// đã vô hiệu hoá afterCheck
			// hàm này tìm các node có idPhong để set true, ko thực hiện set true toàn bộ các node con
			var phong = (cPhongBan)root.Tag;
			if (phong.ID == idPhong) root.Checked = true;
			if (root.Nodes.Count > 0)
				foreach (TreeNode node in root.Nodes)
					setCheckedNode_PhongDaKetcong(idPhong, node);
		}

		private void btnThucHien_Click(object sender, EventArgs e) {
			frm2QLLuongCongNhat frm = new frm2QLLuongCongNhat() {
				m_thang = dtpThang.Value,
			};
			frm.WindowState = FormWindowState.Normal;
			frm.StartPosition = FormStartPosition.Manual;
			frm.MdiParent = this.MdiParent;
			//frm.Location = XL2.GetCenterLocation(frm.MdiParent.Size.Width, frm.MdiParent.Size.Height, frm.Size.Width, frm.Size.Height);
			frm.Location = new Point(0, 0);//XL2.GetCenterLocation(frm.MdiParent.ClientRectangle.Width, frm.MdiParent.ClientRectangle.Height, frm.Size.Width, frm.Size.Height);
			frm.Show();
			Close();
		}


	}
}
