using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ChamCong_v02.BUS;
using ChamCong_v02.DAO;
using ChamCong_v02.DTO;
using log4net;

namespace ChamCong_v02 {
	public partial class frm_12_DiemDanhNV : Form {
		public readonly ILog lg = LogManager.GetLogger("frm_12_DiemDanhNV");


		public List<int> m_listIDPhongBan { get; set; }

		// hàm xử lý -----------------------------------------------------------------------------
		#region load treeview new
		public TreeView loadTreePhgBan(TreeView tvDSPhongBan, DataTable pDataTable) {
			if (pDataTable.Rows.Count > 0) {
				foreach (DataRow dataRow in pDataTable.Select("RelationID = 0")) {
					TreeNode ParentNode = new TreeNode { Text = dataRow["Description"].ToString(), Tag = (int)dataRow["ID"] };
					tvDSPhongBan.Nodes.Add(ParentNode);
					loadTreeSubNode(ref ParentNode, (int)(dataRow["ID"]), pDataTable);
				}
			}
			return tvDSPhongBan;
		}

		private void loadTreeSubNode(ref TreeNode ParentNode, int ParentId, DataTable dtMenu) {
			DataRow[] childs = dtMenu.Select("RelationID = " + ParentId);
			foreach (DataRow dRow in childs) {
				TreeNode child = new TreeNode { Text = dRow["Description"].ToString(), Tag = (int)dRow["ID"], ToolTipText = dRow["Description"].ToString() };
				ParentNode.Nodes.Add(child);
				//Recursion Call
				loadTreeSubNode(ref child, (int)dRow["ID"], dtMenu);
			}
		}

		void GetIDLeafNodeExceptParent(TreeNode root, List<int> listID) {
			if (root == null) return;
			if (root.FirstNode == null) {
				listID.Add((int)root.Tag);
				//lg.Debug(root.Tag + " " + root.Text);
				GetIDLeafNodeExceptParent(root.NextNode, listID);
			}
			if (root.FirstNode != null) {
				foreach (TreeNode node in root.Nodes) {
					GetIDLeafNodeExceptParent(node, listID);
				}
			}
		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {

			if (m_listIDPhongBan == null) m_listIDPhongBan = new List<int>();
			else m_listIDPhongBan.Clear();
			if (e.Node.FirstNode != null) GetIDLeafNodeExceptParent(e.Node, m_listIDPhongBan);
			else m_listIDPhongBan.Add((int)e.Node.Tag);
			e.Node.Expand();

		}

		#endregion

		public frm_12_DiemDanhNV() {
			InitializeComponent();
			log4net.Config.XmlConfigurator.Configure();
			dgrdTongHop.AutoGenerateColumns = false;
			DateTime today = DateTime.Today;
			dtpNgay.Value = new DateTime(today.Year, today.Month, today.Day);
		}

		private void btnDiemDanh_Click(object sender, EventArgs e) {
			//1. lấy dữ liệu từ form
			#region lấy ngày BD và kết thúc, và update lại Ngày BD = 1 ngày trước 31/08 12:00 AM, ngày KT là 1 ngày sau ngay 1 23:59:59
			dtpNgay.Update();
			DateTime ngayBD = dtpNgay.Value.Date;
			ngayBD = ngayBD.AddDays(-1d);
			DateTime ngayKT = ngayBD.AddDays(2d).Subtract(new TimeSpan(0, 0, 1));
			#endregion
			//-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

			DataTable table = DAL.LayDSNV(m_listIDPhongBan.ToArray());
			List<int> dsMaCC_Checked = new List<int>();
			if (table.Rows.Count == 0) return;
			foreach (DataRow row in table.Rows) {
				dsMaCC_Checked.Add((int)row["UserEnrollNumber"]);
			}
			int[] ArrDSMaCC_Checked = dsMaCC_Checked.ToArray();
			//3. lấy dữ liệu chấm công của các nhân viên
			//[CHÚ Ý] ngày bắt đầu và kết thúc đã cộng trừ thêm 1 ngày trước sau ở bở trên
			DataTable tableCTDiemDanh = new DataTable();

			List<cUserInfo> dsnv = new List<cUserInfo>();
			int SoNVDangLamViec = 0, SoNVDaRaVe = 0, SoNVVang = 0;
			try {
				dsnv = XL.XemCong2(table, ArrDSMaCC_Checked, ngayBD, ngayKT);
				tableCTDiemDanh = XL.TaoTableDiemDanh(dsnv, out SoNVDangLamViec, out SoNVDaRaVe, out SoNVVang);
			} catch (Exception exception) {
				lg.Error("form diem danh", exception);
				MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại sau.", "Lỗi");
				GC.Collect();
				return;
			}

			//4. xử lý dữ liệu để đưa lên lưới tổng hợp

			tbTongSoNV.Text = dsnv.Count.ToString();
			tbSoNVDangLamViec.Text = SoNVDangLamViec.ToString();
			tbSoNVDaRaVe.Text = SoNVDaRaVe.ToString();
			tbSoNVVang.Text = SoNVVang.ToString();
			dgrdTongHop.DataSource = tableCTDiemDanh;
		}


		private void frm_DiemDanhNV_Load(object sender, EventArgs e) {
			// 1. khởi tạo các biến cục bộ
			m_listIDPhongBan = new List<int>();
			DataTable tablePhong = DAL.LayDSPhong(ThamSo.currUserID);
			if (tablePhong.Rows.Count == 0) {
				AutoClosingMessageBox.Show("Bạn chưa được phân quyền thao tác.", "Thông báo", 2000);
				return;
			}
			//2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan : xoá dữ liệu trước và load
			treePhongBan.Nodes.Clear();
			loadTreePhgBan(treePhongBan, tablePhong);

			// đăng ký sự kiện cho tree và chọn topNode
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;
			treePhongBan.SelectedNode = treePhongBan.TopNode;

		}



	}
}
