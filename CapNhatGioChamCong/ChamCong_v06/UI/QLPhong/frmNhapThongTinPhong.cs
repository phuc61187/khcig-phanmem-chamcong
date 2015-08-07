using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v06.Helper;

namespace ChamCong_v06.UI.QLPhong {
	public partial class frmNhapThongTinPhong : Form {
		public DataRow m_CurrentDataRow = null; // nếu chế độ sửa: dùng CurrentDataRow
		public DataRow m_ParentDataRow = null; // nếu chế độ thêm: dùng ParentDataRow 
		public string m_TenPhong = string.Empty;
		public int m_VitriPhong = 99999;
		public bool m_Enable = false;
		public ModeType m_Mode = ModeType.Cancel;

		#region hàm ko quan trọng
		public frmNhapThongTinPhong() {
			InitializeComponent();
		}
		private void btnTenPhong_Properties_ClearButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
			MyUtility.ClearButtonEditText(sender);
		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			e.Node.Expand();
		}

		private void btnHuy_Click(object sender, EventArgs e) {
			m_Mode = Helper.ModeType.Cancel;
			Close();
		}

		#endregion

		#region cách làm có store procedure
		public TreeView loadTreePhgBan(TreeView tvDSPhongBan) {
			// chỉ chọn các node được enable
			tvDSPhongBan.Nodes.Clear();
			DataTable tableDSPhong = SqlDataAccessHelper.ExecSPQuery(SPName.RelationDept_DocTatCaPhongBan.ToString());

			var rowsPhong = (from DataRow row in tableDSPhong.Rows
							 //where //todo chọn các phòng được enable
							 select row).OrderBy(s => (int)s["ViTri"]);
			var relationID_0 = rowsPhong.Where(o => (int)o["RelationID"] == 0).ToList().OrderBy(s => (int)s["ViTri"]);

			foreach (var dataRow in relationID_0) {
				TreeNode parentNode = new TreeNode { Text = dataRow["Description"].ToString(), Tag = dataRow };
				tvDSPhongBan.Nodes.Add(parentNode);
				loadTreeSubNode(ref parentNode, (int)dataRow["ID"], rowsPhong/*TatcaPhongban*/);
			}
			return tvDSPhongBan;
		}

		public void loadTreeSubNode(ref TreeNode ParentNode, int idPhongBanTrucThuoc, IOrderedEnumerable<DataRow> dsphongban /*List<cPhongBan> dsphongban*/) {
			IOrderedEnumerable<DataRow> childs = dsphongban.Where(item => (int)item["RelationID"] == idPhongBanTrucThuoc).ToList().OrderBy(item => (int)item["ViTri"]);
			foreach (DataRow phong in childs) {
				TreeNode child = new TreeNode { Text = phong["Description"].ToString(), Tag = phong };
				ParentNode.Nodes.Add(child);
				loadTreeSubNode(ref child, (int)phong["ID"], dsphongban);
			}
		}

		#endregion

		private void frmThemPhongBan_Load(object sender, EventArgs e) {
			loadTreePhgBan(treePhongBan);

			//nếu chế độ sửa thì tự fill thông tin 
			if (this.m_Mode == ModeType.Sua) {
				string tenPhong = (this.m_CurrentDataRow["Description"] == DBNull.Value) ? string.Empty : this.m_CurrentDataRow["Description"].ToString();
				var vitriPhong = (this.m_CurrentDataRow["ViTri"] == DBNull.Value) ? 0 : (int)this.m_CurrentDataRow["ViTri"];
				var enable = (bool) m_CurrentDataRow["Enable"];
				//var idPhong = ((int) this.CurrentDataRow["ID"]);
				var idRelation = ((int)this.m_CurrentDataRow["RelationID"]);
				TreeNode topNode = TopNode(treePhongBan.TopNode);
				TreeNode node = FindNode(idRelation, topNode);
				treePhongBan.SelectedNode = node;

				btnTenPhong.Text = tenPhong;
				tbVitriPhong.Text = vitriPhong.ToString();
				checkEnable.Checked = enable;
			}
			else if (this.m_Mode == ModeType.Them) { // chọn sẵn node cha, checked enable
				var id = ((int)this.m_CurrentDataRow["ID"]);
				TreeNode topNode = TopNode(treePhongBan.TopNode);
				TreeNode node = FindNode(id, topNode);
				treePhongBan.SelectedNode = node;

				checkEnable.Checked = true;
			}
		}

		private TreeNode FindNode(int IDPhong, TreeNode root) {
			DataRow row = (DataRow)root.Tag;
			if (IDPhong == (int)row["ID"]) return root;
			if (root.Nodes.Count > 0) {
				for (int i = 0; i < root.Nodes.Count; i++) {
					TreeNode node = FindNode(IDPhong, root.Nodes[i]);
					if (node != null) return node;
				}
			}
			return null;
		}

		public TreeNode TopNode(TreeNode root) {
			if (root.Parent != null) return root.Parent;
			if (root.PrevNode != null) return root.PrevNode;
			return root;
		}

		private void btnLuu_Click(object sender, EventArgs e) {
			#region kiểm tra nhập liệu hợp lệ

			if (btnTenPhong.Text == string.Empty) {
				ACMessageBox.Show("Tên phòng trống", Resources.Caption_Loi, 2000);
				return;
			}
			if (int.TryParse(tbVitriPhong.Text, out m_VitriPhong) == false) {
				ACMessageBox.Show("Vị trí phòng không hợp lệ", Resources.Caption_Loi, 2000);
				return;
			}

			#endregion

			this.m_TenPhong = btnTenPhong.Text;
			this.m_ParentDataRow = (DataRow)treePhongBan.SelectedNode.Tag;
			this.m_Enable = checkEnable.Checked;
			//this.VitriPhong = VitriPhong;
			this.Close();
		}

	}
}
