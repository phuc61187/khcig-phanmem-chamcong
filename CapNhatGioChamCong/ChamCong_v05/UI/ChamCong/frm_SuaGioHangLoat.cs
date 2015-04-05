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
using log4net;

namespace ChamCong_v05.UI.ChamCong {
	public partial class frm_SuaGioHangLoat : Form {
		#region log tooltip và hàm ko quan trọng
		public readonly ILog lg = LogManager.GetLogger("frm_SuaGioHangLoat");

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

		private void btnThoat_Click(object sender, EventArgs e) {
			Close();
		}

		#endregion

		public List<int> m_listIDPhongBan;
		public List<cUserInfo> m_DSNV;
		public DataTable m_Bang_DSNV;
		public List<cPhongBan> m_DSPhg = new List<cPhongBan>();


		public DataTable TaoBang_DSNV() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "check", "cUserInfo", "UserEnrollNumber", "UserFullCode", "UserFullName", "SchID", "SchName", "ChucVu", "MaPhong", "TenPhong", "HeSoLuongCB", "HeSoLuongSP", "HSBHCongThem" },
				new[] { typeof(bool), typeof(cUserInfo), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(float), typeof(float), typeof(float) });
			return kq;
		}

		#region 3 biến checkbox check all: 1 của DSNV cần xem công. 2 DS Công của NV
		readonly CheckBox checkAll_GridDSNV = new CheckBox();
		readonly CheckBox checkAllGridCheckIn = new CheckBox();
		readonly CheckBox checkAllGridCheckOut = new CheckBox();
		#region vẽ check box check all và xử lý sự kiện check
		void checkAll_CheckedChanged(object sender, EventArgs e) {
			//1. xác định datagrid nào đang check all
			DataGridView tempGrid;
			if (sender == checkAll_GridDSNV) tempGrid = dgrdDSNVTrgPhg;
			else if (sender == checkAllGridCheckIn) tempGrid = dgrdCTGioVao;
			else tempGrid = dgrdCTGioRa;

			// 2. kiểm tra nếu ko có dòng dữ liệu nào thì thoát
			if (tempGrid.Rows.Count == 0) return;

			//3. có dữ liệu, duyệt qua hết và gán giá trị check (or uncheck), sau đó endEDIT lại lưới để dòng đầu tiên có giá trị
			bool tmpCheckAll = ((CheckBox)sender).Checked;

			tempGrid.BeginEdit(true);
			if (tempGrid.DataSource is DataTable) {
				var dt = tempGrid.DataSource as DataTable;
				if (dt.Rows.Count != 0) {
					foreach (DataRow row in dt.Rows) {
						row["check"] = tmpCheckAll;
					}
				}

			}
			else if (tempGrid.DataSource is DataView) {
				var dt = tempGrid.DataSource as DataView;
				if (dt.Count != 0) {
					foreach (DataRowView row in dt) {
						row["check"] = tmpCheckAll;
					}
				}
			}


			tempGrid.EndEdit();
			tempGrid.Update();
			tempGrid.RefreshEdit();
		}
		#endregion


		#endregion


		// hàm xử lý -----------------------------------------------------------------------------

		public frm_SuaGioHangLoat() {
			InitializeComponent();

			#region khởi tạo các biến cục bộ

			m_listIDPhongBan = new List<int>();
			m_DSNV = new List<cUserInfo>();
			m_Bang_DSNV = TaoBang_DSNV();

			#endregion

			#region không cho autogen các column khi bind dữ liệu

			dgrdDSNVTrgPhg.AutoGenerateColumns = dgrdCTGioVao.AutoGenerateColumns = dgrdCTGioRa.AutoGenerateColumns = false;

			#endregion

			#region gán template vào các dataSource, hoặc dataView vào các dataSource

			DataView dataView = new DataView(m_Bang_DSNV);
			dgrdDSNVTrgPhg.DataSource = dataView;

			#endregion

			#region vẽ 3 checkbox checkall cho DSNV trong phòng

			XL2.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));
			XL2.VeCheckBox_CheckAll(dgrdCTGioVao, checkAllGridCheckIn, checkAll_CheckedChanged, new Point(7, 3));
			XL2.VeCheckBox_CheckAll(dgrdCTGioRa, checkAllGridCheckOut, checkAll_CheckedChanged, new Point(7, 3));

			#endregion

			DateTime today = DateTime.Today;
			dtpBD.Value = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
			dtpKT.Value = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59, 0);
		}

		private void frm_SuaGioHangLoat2_Load(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				Close();
				return;
			}

			try //general try catch
			{
				// 1. khởi tạo các biến cục bộ
				m_listIDPhongBan = new List<int>();
				#region //2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan ,
				// trường hợp ko có phòng ban nào được phép thao tác thì báo và thoát form

				XL.KhoiTaoDSPhongBan(m_DSPhg, XL2.currUserID); // tạo danh sách phòng ban chỉ userID này được thao tác
				if (m_DSPhg.Count == 0) {
					ACMessageBox.Show(Resources.Text_ChuaCapQuyenPhongBanThaoTac, Resources.Caption_ThongBao, 5000);
					Close();
					return;
				}
				XL.loadTreePhgBan(treePhongBan, XL2.TatcaPhongban, m_DSPhg);

				#endregion
			} catch (Exception ex) //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
			// đăng ký sự kiện cho tree và chọn topNode
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;
			treePhongBan.SelectedNode = treePhongBan.TopNode;
		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			#region kiểm tra kết nối csdl , nếu mất kết nối thì đóng

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false)
			{
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				Close();
				return;
			}

			#endregion

			#region mỗi lần chọn node thì lấy ID node hiện tại và tất cả node con

			m_listIDPhongBan.Clear();
			if (e.Node.FirstNode != null) XL.GetIDNodeAndChildNode(e.Node, ref m_listIDPhongBan);
			else {
				var temp = ((cPhongBan)e.Node.Tag);
				if (temp.ChoPhep) m_listIDPhongBan.Add(temp.ID);
			}
			e.Node.Expand();

			#endregion

			XL.KhoiTaoDSNV_ChamCong(m_DSNV, m_listIDPhongBan, m_DSPhg);

			XL.TaoTableDSNV(m_DSNV, m_Bang_DSNV);

			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = string.Empty;

			checkAll_GridDSNV.Checked = false;
		}


		private void btnXem_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			//0. xoa dữ liệu cũ
			//1. lấy dữ liệu từ form
			dtpBD.Update(); dtpKT.Update();
			DateTime startTime = dtpBD.Value;
			DateTime endTime = dtpKT.Value;
			dgrdDSNVTrgPhg.EndEdit();
			BindingContext[dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();

			try //general try catch
			{
				#region //2. lấy danh sách nhân viên check, nếu chưa có nv nào check thì thông báo

				var listNV = (from DataGridViewRow dataGridViewRow in dgrdDSNVTrgPhg.Rows
				              let rowView = dataGridViewRow.DataBoundItem as DataRowView
				              where (rowView["check"] != DBNull.Value && (bool)rowView["check"])
				              select (cUserInfo)rowView["cUserInfo"])
					.ToList();

				if (listNV.Count == 0) {
					ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000); //
					GC.Collect();
					return;
				}

				#endregion
				//2. lấy dữ liệu chấm công của các nhân viên
				if (XL.KiemtraDulieuCapnhatTuServer(DateTime.Now) == false) {
					ACMessageBox.Show(Resources.Text_DuLieuChamCongChuaUpdate, Resources.Caption_ThongBao, 4000);
				}

				DataTable fDataTableChkInOut = new DataTable();
				try {
					var Arr_MaCC = (from nv in listNV select nv.MaCC).ToArray();
					fDataTableChkInOut = DAO5.LayTableCIO_A(Arr_MaCC, startTime, endTime);
					fDataTableChkInOut.Columns.Add("check", typeof(bool));
				} catch (Exception exception) {
					lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), exception);

					MessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi);
					GC.Collect();
				}

				DataTable dataTableChiTietVao = dgrdCTGioVao.DataSource as DataTable;
				DataTable dataTableChiTietRa = dgrdCTGioRa.DataSource as DataTable;
				if (dataTableChiTietVao == null) {
					dataTableChiTietVao = fDataTableChkInOut.Clone();
				}
				else dataTableChiTietVao.Rows.Clear();

				if (dataTableChiTietRa == null) {
					dataTableChiTietRa = fDataTableChkInOut.Clone();
				}
				else dataTableChiTietRa.Rows.Clear();


				//4. xử lý dữ liệu để đưa lên lưới tổng hợp
				foreach (DataRow row in fDataTableChkInOut.Rows) {
					if ((int)row["MachineNo"] % 2 == 1) dataTableChiTietVao.ImportRow(row);
					else dataTableChiTietRa.ImportRow(row);
				}
				dgrdCTGioVao.DataSource = dataTableChiTietVao;
				dgrdCTGioRa.DataSource = dataTableChiTietRa;
				checkAllGridCheckIn.CheckedChanged -= checkAll_CheckedChanged;
				checkAllGridCheckOut.CheckedChanged -= checkAll_CheckedChanged;
				checkAllGridCheckIn.Checked = false;
				checkAllGridCheckOut.Checked = false;
				checkAllGridCheckIn.CheckedChanged += checkAll_CheckedChanged;
				checkAllGridCheckOut.CheckedChanged += checkAll_CheckedChanged;
			} catch (Exception ex) //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}

		private void btnSuaGio_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			bool flag = false;
			bool tmpSuaGioVao = (tabControl1.SelectedIndex == 0);

			try
			{
				DataGridView dataGridThaoTac = tmpSuaGioVao ? dgrdCTGioVao : dgrdCTGioRa;
				DataTable table = dataGridThaoTac.DataSource as DataTable;
				if (table == null) return;
				DataRow[] tmpArrRow = (table).Select("check = true");
				if (tmpArrRow.Length == 0) {
					ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000);
					return;
				}

				frm_ThongTinSuaGioHangLoat frm311Thongtin = new frm_ThongTinSuaGioHangLoat() { fNgaySuaMacDinh = dtpBD.Value.Date, fCheckIn = tmpSuaGioVao };
				frm311Thongtin.ShowDialog();

				if (frm311Thongtin.fOK) {
					DateTime tmpGioMoi = frm311Thongtin.fGioMoi;
					bool tmpKieuGioMoi = frm311Thongtin.fCheckIn;
					foreach (DataRow row in tmpArrRow) {
						int tmpUserEnrollNumber = (int)row["UserEnrollNumber"];
						DateTime tmpGioCu = (DateTime)row["TimeStr"];
						int tmpMachineNo = (int)row["MachineNo"];
						string tmpSourceOld = row["Source"].ToString();
						string tmpLyDo = frm311Thongtin.fLydo;
						string tmpGhiChu = frm311Thongtin.fGhichu;
						flag = DAO5.SuaGioChoNV(tmpUserEnrollNumber, tmpGioCu, tmpGioMoi, tmpSourceOld, "PC", tmpMachineNo, (tmpKieuGioMoi) ? 21 : 22, -1, tmpLyDo, tmpGhiChu);
						if (flag == false) {
							lg.Error("Lỗi: sửa giờ cho " + tmpUserEnrollNumber + " giờ chưa sửa: " + tmpGioCu + " giờ sửa: " + tmpGioMoi);
							break;
						}

					}
					if (flag) {
						btnXem_Click(btnXem, null);
					}
					else {
						MessageBox.Show("Xảy ra lỗi trong quá trình sửa giờ cho Nhân viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK);
					}
				}
			}
			catch (Exception ex)
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}


		private void btnTim_Click(object sender, EventArgs e) {
			if (dgrdDSNVTrgPhg.DataSource == null) return;

			var searchStr1 = tbSearch.Text;
			var searchStr = string.Format("UserFullName like '%{0}%' or UserFullCode like '%{0}%'", searchStr1);
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = searchStr;


		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = string.Empty;

		}

		private void tbSearch_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter)
				btnTim.PerformClick();

		}



	}
}
