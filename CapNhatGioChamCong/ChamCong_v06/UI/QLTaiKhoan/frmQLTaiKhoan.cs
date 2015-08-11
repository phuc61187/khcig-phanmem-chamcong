using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.Helper;

namespace ChamCong_v06.UI.QLTaiKhoan {
	public partial class frmQLTaiKhoan : Form {
		#region các hàm ko quan trọng
		public frmQLTaiKhoan() {
			InitializeComponent();
		}

		#endregion

		#region cách làm có store procedure
		public static TreeView loadTreePhgBan(TreeView tvDSPhongBan) {
			tvDSPhongBan.Nodes.Clear();
			DataTable tableDSPhong = SqlDataAccessHelper.ExecSPQuery(SPName.RelationDept_DocTatCaPhongBan.ToString());

			var rowsPhong = (from DataRow row in tableDSPhong.Rows select row).OrderBy(s => (int)s["ViTri"]);
			var relationID_0 = rowsPhong.Where(o => (int)o["RelationID"] == 0).ToList().OrderBy(s => (int)s["ViTri"]);

			foreach (var dataRowView in relationID_0) {
				var enable = (bool)(dataRowView["Enable"]);
				if (enable == false) continue; // ko load những phòng ban bị disable
				var string2 = dataRowView["Description"].ToString();
				TreeNode parentNode = new TreeNode { Text = string2, Tag = dataRowView };
				tvDSPhongBan.Nodes.Add(parentNode);
				loadTreeSubNode(ref parentNode, (int)dataRowView["ID"], rowsPhong/*TatcaPhongban*/);
			}


			return tvDSPhongBan;
		}

		public static void loadTreeSubNode(ref TreeNode ParentNode, int idPhongBanTrucThuoc, IOrderedEnumerable<DataRow> dsphongban /*List<cPhongBan> dsphongban*/) {
			IOrderedEnumerable<DataRow> childs = dsphongban.Where(item => (int)item["RelationID"] == idPhongBanTrucThuoc).ToList().OrderBy(item => (int)item["ViTri"]);
			foreach (DataRow phong in childs) {
				var enable = (bool)(phong["Enable"]);
				if (enable == false) continue; // ko load những phòng ban bị disable
				var string2 = phong["Description"].ToString();
				TreeNode child = new TreeNode { Text = string2, Tag = phong };
				ParentNode.Nodes.Add(child);
				loadTreeSubNode(ref child, (int)phong["ID"], dsphongban);
			}
		}

		#endregion

		private void frmQLTaiKhoan_Load(object sender, EventArgs e)
		{
			LoadDSTaiKhoan();
			loadTreePhgBan(treePhongBan);

		}

		private void LoadDSTaiKhoan()
		{
			DataTable tableTaiKhoan = SqlDataAccessHelper.ExecSPQuery(SPName6.NewAccount_DocTatCaTaiKhoan.ToString());
			gridControlTaiKhoan.DataSource = tableTaiKhoan;
		}

		private void LoadPhanQuyenPhongBan(int UserID)
		{
			DataTable tablePhongBanThaoTac = SqlDataAccessHelper.ExecSPQuery(SPName6.DeptPrivilege_DocPhongBanThaoTac.ToString(),
				new SqlParameter("@UserID", UserID),
				new SqlParameter("@IsYes", true),
				new SqlParameter("@RelationDeptEnable", true));
			treePhongBan.Nodes.
		}
	}
}
