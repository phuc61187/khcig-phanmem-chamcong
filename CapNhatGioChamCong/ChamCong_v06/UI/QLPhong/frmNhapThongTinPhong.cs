using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v06.Helper;

namespace ChamCong_v06.UI.QLPhong {
	public partial class frmNhapThongTinPhong : Form {
		public DataRow CurrentDataRow = null; // nếu chế độ sửa: dùng CurrentDataRow
		public DataRow ParentDataRow = null; // nếu chế độ thêm: dùng ParentDataRow 
		public string TenPhong = string.Empty;
		public int VitriPhong = 99999;
		public bool Disable = false;
		public ModeType Mode = ModeType.Cancel;

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
			Mode = Helper.ModeType.Cancel;
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
			if (this.Mode == ModeType.Sua) {
				string tenPhong = (this.CurrentDataRow["Description"] == DBNull.Value) ? string.Empty : this.CurrentDataRow["Description"].ToString();
				var vitriPhong = (this.CurrentDataRow["ViTri"] == DBNull.Value) ? 0 : (int)this.CurrentDataRow["ViTri"];
				var disable = (CurrentDataRow["Disable"] == DBNull.Value) ? false : ((bool)CurrentDataRow["Disable"]);
				var enable = !disable;
				//var idPhong = ((int) this.CurrentDataRow["ID"]);
				var idRelation = ((int)this.CurrentDataRow["RelationID"]);
				TreeNode topNode = TopNode(treePhongBan.TopNode);
				TreeNode node = FindNode(idRelation, topNode);
				treePhongBan.SelectedNode = node;

				btnTenPhong.Text = tenPhong;
				tbVitriPhong.Text = vitriPhong.ToString();
				checkEnable.Checked = enable;// chú ý ngược giá trị của disable
			}
			else if (this.Mode == ModeType.Them) { // chọn sẵn node cha, checked enable
				var id = ((int)this.CurrentDataRow["ID"]);
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
			if (int.TryParse(tbVitriPhong.Text, out VitriPhong) == false) {
				ACMessageBox.Show("Vị trí phòng không hợp lệ", Resources.Caption_Loi, 2000);
				return;
			}

			#endregion

			this.TenPhong = btnTenPhong.Text;
			this.ParentDataRow = (DataRow)treePhongBan.SelectedNode.Tag;
			this.Disable = !checkEnable.Checked;//chú ý vì CSDL là disable nên ở đây là dấu !
			//this.VitriPhong = VitriPhong;
			this.Close();
		}

	}
}
