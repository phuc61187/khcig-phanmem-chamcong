using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.DAL;
using ChamCong_v05.DTO;
using ChamCong_v05.Helper;
using ChamCong_v05.Properties;

namespace ChamCong_v05.UI.QLNV {
	public partial class frmQLNV : Form {
		#region log tooltip và hàm ko quan trọng
		private void toolTipHint_Draw(object sender, DrawToolTipEventArgs e) {
			Font f = new Font("Arial", 10.0f);
			e.DrawBackground();
			e.DrawBorder();
			e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, new PointF(2f, 2f));
		}

		private void toolTipHint_Popup(object sender, PopupEventArgs e) {
			Size temp = TextRenderer.MeasureText(toolTipHint.GetToolTip(e.AssociatedControl), new Font("Arial", 10.0f));
			temp.Width += 4;
			temp.Height += 4;
			e.ToolTipSize = temp;
		}

		#endregion

		public List<int> m_listIDPhongBan = new List<int>();
		public List<cUserInfo> m_DSNV = new List<cUserInfo>();
		public List<cPhongBan> m_DSPhg = new List<cPhongBan>();

		public DataTable m_Bang_DSNV;


		private readonly CheckBox checkAll_GridDSNV = new CheckBox();

		private void checkAll_CheckedChanged(object sender, EventArgs e) {
			//1. xác định datagrid nào đang check all
			DataGridView tempGrid = dgrdDSNVTrgPhg;

			// 2. kiểm tra nếu ko có dòng dữ liệu nào thì thoát
			if (tempGrid.Rows.Count == 0) return;

			//3. có dữ liệu, duyệt qua hết và gán giá trị check (or uncheck), sau đó endEDIT lại lưới để dòng đầu tiên có giá trị
			var tmpCheckAll = ((CheckBox)sender).Checked;

			tempGrid.BeginEdit(true);
			var dt = tempGrid.DataSource as DataView;

			foreach (DataGridViewRow row in dgrdDSNVTrgPhg.Rows) {
				row.Cells["check"].Value = tmpCheckAll;
			}

			tempGrid.EndEdit();
			tempGrid.Update();
			tempGrid.RefreshEdit();
		}

		public frmQLNV() {
			InitializeComponent();
			log4net.Config.XmlConfigurator.Configure();

			#region kh?i t?o các bi?n c?c b?
			m_DSPhg = new List<cPhongBan>();
			m_listIDPhongBan = new List<int>();
			m_DSNV = new List<cUserInfo>();
			#endregion

			// không cho autogen các column khi bind d? li?u: 4 cái
			dgrdDSNVTrgPhg.AutoGenerateColumns = false;


			#region gán template vào các dataSource, ho?c dataView vào các dataSource

			DataView dataView = new DataView(m_Bang_DSNV);
			dgrdDSNVTrgPhg.DataSource = dataView;

			#endregion

			//3. v? 3 checkbox checkall cho DSNV trong pḥng
			XL2.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));

		}

		private void frmQLNV_Load(object sender, EventArgs e) {

			#region kiểm tra kết nối csdl , nếu mất kết nối thì đóng

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}

			#endregion

			//2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan : xoá dữ liệu trước và load
			XL.KhoiTaoDSPhongBan(m_DSPhg, XL2.currUserID);
			if (m_DSPhg.Count == 0) {
				ACMessageBox.Show(Resources.Text_ChuaCapQuyenPhongBanThaoTac, Resources.Caption_ThongBao, 5000);
				Close();
				return;
			}

			//2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan : xoá dữ liệu trước và load
			XL.loadTreePhgBan(treePhongBan, XL2.TatcaPhongban, m_DSPhg);
			TreeNode parentNode0 = new TreeNode { Text = "Nhân viên chưa sắp xếp", Tag = null };
			treePhongBan.Nodes.Insert(0,parentNode0);


			// đăng ký sự kiện cho tree và chọn topNode
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;
			treePhongBan.SelectedNode = treePhongBan.TopNode;

		}
		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			#region kiểm tra kết nối csdl , nếu mất kết nối thì đóng

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}

			#endregion
			// if topnode thì lấy tất cả nhân viên, nếu  1 node khác thì lấy theo ID
			DataTable table;
			if (e.Node == treePhongBan.TopNode) {
				table = XL.LayDSNVChuaSX(null);
			}
			else {
				#region mỗi lần chọn node thì lấy ID node hiện tại và tất cả node con
				m_listIDPhongBan.Clear();
				if (e.Node.FirstNode != null) XL.GetIDNodeAndChildNode(e.Node, ref m_listIDPhongBan);
				else {
					var temp = ((cPhongBan)e.Node.Tag);
					if (temp.ChoPhep) m_listIDPhongBan.Add(temp.ID);
				}
				e.Node.Expand();
				#endregion
				table = XL.LayDSNV(null,m_listIDPhongBan.ToArray());
			}
			table.Columns.Add("check", typeof (bool));// thêm cột check để chọn nv
			m_Bang_DSNV = table.Copy();
			var dataView = new DataView(m_Bang_DSNV);
			dgrdDSNVTrgPhg.DataSource = dataView;
			dataView.RowFilter = string.Empty;

			#region tạo datasource cho autocomplete

			var Source = new AutoCompleteStringCollection();
			Source.AddRange((from DataRow row in m_Bang_DSNV.Rows select row["UserFullName"].ToString().ToUpperInvariant()).ToArray());
			tbSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tbSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			tbSearch.AutoCompleteCustomSource = Source;

			#endregion


			checkAll_GridDSNV.Checked = false;
		}

		private void btnTim_Click(object sender, EventArgs e) {
			if (dgrdDSNVTrgPhg.DataSource == null) return;

			var searchStr1 = tbSearch.Text;
			var searchStr = string.Format("UserFullName like '%{0}%' or UserFullCode like '%{0}%'", searchStr1);
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = searchStr;
		}

		private void btnCapNhat_Click(object sender, EventArgs e) {
			#region lấy dsnv được chọn

			BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();
			var checkRows = (from DataGridViewRow dataGridViewRow in dgrdDSNVTrgPhg.Rows
			                 let row = (DataRowView) dataGridViewRow.DataBoundItem
			                 where dataGridViewRow.Cells["check"].FormattedValue != null && (bool) dataGridViewRow.Cells["check"].FormattedValue
			                 select row).ToList();

			#endregion

			#region chưa chọn nv thì báo và thoát

			if (checkRows.Count == 0)
			{
				ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000);
				return;
			}

			#endregion

			TreeNode node = treePhongBan.SelectedNode;
			string oldFilter = string.Empty;

			if (checkRows.Count == 1) {
				frmThem_Capnhat_1NV frm = new frmThem_Capnhat_1NV { StartPosition = FormStartPosition.CenterParent };
				frm.mode = 0;
				frm.RowView = checkRows[0];
				frm.ShowDialog();
				if (frm.IsReload) {
					var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
					if (dataView != null) oldFilter = dataView.RowFilter;
					treePhongBan_AfterSelect(treePhongBan, new TreeViewEventArgs(node));
					dataView = dgrdDSNVTrgPhg.DataSource as DataView;
					if (dataView != null) dataView.RowFilter = oldFilter;
				}
			}
			else {
				frmCapNhatNVHangLoat frm = new frmCapNhatNVHangLoat { StartPosition = FormStartPosition.CenterParent };
				frm.RowViews = checkRows;
				frm.ShowDialog();
				if (frm.IsReload) {
					var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
					if (dataView != null) oldFilter = dataView.RowFilter;
					treePhongBan_AfterSelect(treePhongBan, new TreeViewEventArgs(node));
					dataView = dgrdDSNVTrgPhg.DataSource as DataView;
					if (dataView != null) dataView.RowFilter = oldFilter;
				}
			}
		}

		private void btnThem_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			TreeNode node = treePhongBan.SelectedNode;
			frmThem_Capnhat_1NV frm = new frmThem_Capnhat_1NV { StartPosition = FormStartPosition.CenterParent };
			frm.mode = 1;
			frm.ShowDialog();
			if (frm.IsReload) {
				string oldFilter = string.Empty;
				var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
				if (dataView != null) oldFilter = dataView.RowFilter;
				treePhongBan_AfterSelect(treePhongBan, new TreeViewEventArgs(node));
				dataView = dgrdDSNVTrgPhg.DataSource as DataView;
				if (dataView != null) dataView.RowFilter = oldFilter;
			}
		}

		private void btnXoa_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			#region lấy ds UserEnrollNumber được chọn

			BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();

			var checkRows = (from DataGridViewRow dataGridViewRow in dgrdDSNVTrgPhg.Rows
							 let row = (DataRowView)dataGridViewRow.DataBoundItem
							 where dataGridViewRow.Cells["check"].FormattedValue != null && (bool)dataGridViewRow.Cells["check"].FormattedValue
							 select (int)row["UserEnrollNumber"]).ToList();

			#endregion

			#region chưa chọn NV thì báo và thoát

			if (checkRows.Count == 0) {
				ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000);
				return;
			}

			#endregion

			#region xác nhận trước khi xoá
			if (MessageBox.Show("Bạn muốn xoá Nhân viên này khỏi CSDL? \nKhi xoá Nhân viên sẽ không thể phục hồi lại nhân viên này.\nCân nhắc sử dụng chức năng vô hiệu hoá trạng thái hoạt động (UserEnabled).\nBấm yes để xoá Nhân viên. Bấm No để quay trở lại.", Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) return;
			#endregion
			TreeNode node = treePhongBan.SelectedNode;// sử dụng để reload dgrid

			var query = "	delete from UserInfo where ( UserEnrollNumber = {0} ) ";
			string temp = string.Join(" or UserEnrollNumber = ", checkRows.ToArray());
			query = string.Format(query, temp);
			int kq = SqlDataAccessHelper.ExecNoneQueryString(query, null, null);
			foreach (int checkedUEN in checkRows)
			{
				DAO5.GhiNhatKyThaotac("Xoá NV khỏi CSDL", string.Format("Xoá NV có mã chấm công [{0}] khỏi CSDL", checkedUEN), maCC:checkedUEN);
			}
			if (kq != 0) {
				ACMessageBox.Show("Xoá Nhân viên thành công.", Resources.Caption_ThongBao, 2000);
			}
			else {
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
			}

			#region reload grid

			string oldFilter = string.Empty;
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) oldFilter = dataView.RowFilter;
			treePhongBan_AfterSelect(treePhongBan, new TreeViewEventArgs(node));
			dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = oldFilter;

			#endregion

		}

		private void linkHienThiTatCaNV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = string.Empty;

		}

		private void btnCapnhatCongnhat_Click(object sender, EventArgs e) {
			#region lấy ds UserEnrollNumber được chọn

			BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();

			var checkRow = (from DataGridViewRow dataGridViewRow in dgrdDSNVTrgPhg.Rows
							 let row = (DataRowView)dataGridViewRow.DataBoundItem
							 where dataGridViewRow.Cells["check"].FormattedValue != null && (bool)dataGridViewRow.Cells["check"].FormattedValue
							 select row).SingleOrDefault();
			if (checkRow == null)
			{
				ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000);
				return;
			}

			TreeNode node = treePhongBan.SelectedNode;
			string oldFilter = string.Empty;

			frmCapNhatCongNhat frm = new frmCapNhatCongNhat { StartPosition = FormStartPosition.CenterParent,m_currRowNV = checkRow };
			frm.ShowDialog();


				if (frm.IsReload) {
					var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
					if (dataView != null) oldFilter = dataView.RowFilter;
					treePhongBan_AfterSelect(treePhongBan, new TreeViewEventArgs(node));
					dataView = dgrdDSNVTrgPhg.DataSource as DataView;
					if (dataView != null) dataView.RowFilter = oldFilter;
				}


			#endregion

		}

		private void tbSearch_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter)
				btnTim.PerformClick();

		}



	}
}
