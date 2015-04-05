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
using ChamCong_v05.UI.KhaiBao;
using log4net;
using OfficeOpenXml;

namespace ChamCong_v05.UI.ChamCong {
	public partial class frm_DiemDanhNV : Form {
		#region log tooltip và hàm ko quan trọng
		public readonly ILog lg = LogManager.GetLogger("frm_DiemDanhNV");

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

		private void frm_DiemDanhNV_Activated(object sender, EventArgs e) {
			dtpNgay_ValueChanged(null, null);
		}

		#endregion

		public List<cUserInfo> m_DSNV = new List<cUserInfo>(); // danh sách các nhân viên thuộc phòng đang chọn
		public List<cPhongBan> m_DSPhg = new List<cPhongBan>();
		public List<int> m_listIDPhongBan;
		public DataTable m_BangCTDiemDanh; //[20140511_1]
		private DataTable TaoCauTrucTableDiemDanh() {//[20140511_1]
			DataTable kq = XL.TaoCauTrucDataTable(
					new[] { "UserEnrollNumber", "UserFullCode", "UserFullName", "TimeStrVao1", "TimeStrRaa1", "TimeStrVao2", "TimeStrRaa2", "TimeStrVao3", "TimeStrRaa3", "ShiftID1", "ShiftID2", "ShiftID3", "Ca", "TrangThai" },
					new[] { typeof(int), typeof(string), typeof(string), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(int), typeof(int), typeof(int), typeof(string), typeof(string), });
			return kq;
		}

		// hàm xử lý -----------------------------------------------------------------------------
		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			try  //general try catch
			{
				#region get ID node hiện tại và các node con
				m_listIDPhongBan.Clear();
				if (e.Node.FirstNode != null) XL.GetIDNodeAndChildNode(e.Node, ref m_listIDPhongBan);
				else {
					var temp = ((cPhongBan)e.Node.Tag);
					if (temp.ChoPhep) m_listIDPhongBan.Add(temp.ID);
				}
				e.Node.Expand();
				#endregion

				#region mất kết nối csdl thì thoát form
				if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
					ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
					Close();
					return;
				}
				#endregion

				#region lấy ngày BD và kết thúc, ngày điểm danh và update lại Ngày BD = 1 ngày trước 31/08 12:00 AM, ngày KT là 1 ngày sau ngay 1 23:59:59
				dtpNgay.Update();
				DateTime ngaydiemdanh = dtpNgay.Value.Date;
				DateTime ngayBD_bef2D = ngaydiemdanh.AddDays(-2d);
				DateTime ngayKT_Aft2D = ngaydiemdanh.AddDays(2d);
				#endregion

				// lấy dsnv tất cả nv thuộc các phòng ban được chọn
				XL.KhoiTaoDSNV_ChamCong(m_DSNV, m_listIDPhongBan, m_DSPhg);

				//3. lấy dữ liệu chấm công của các nhân viên
				//[CHÚ Ý] ngày bắt đầu và kết thúc đã cộng trừ thêm 1 ngày trước sau ở bở trên

				int SoNVDangLamViec, SoNVDaRaVe, SoNVKoHienDien, SoNVVang_LyDo, SoNVNghi;
				try {
					//logic điểm danh ngày được input, truyền thêm datetime.Now để xác định nếu trong vòng 24h kể từ hiện tại trở về trước, nếu vào ko ra thì vẫn còn làm việc
					// nếu vào ko ra trên 24h nghĩa là ngày đó quên chấm công
					XL.DiemDanh_v08(m_DSNV, ngayBD_bef2D, ngayKT_Aft2D, ngaydiemdanh, DateTime.Now);
					m_BangCTDiemDanh.Rows.Clear();
					XL.TaoTableDiemDanh(m_DSNV, m_BangCTDiemDanh, ngaydiemdanh, out SoNVDangLamViec, out SoNVDaRaVe, out SoNVKoHienDien, out SoNVVang_LyDo, out SoNVNghi);
				} catch (Exception exception) {
					lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), exception);
					MessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi);
					GC.Collect();
					return;
				}

				//4. xử lý dữ liệu để đưa lên lưới tổng hợp
				tbTongSoNV.Text = m_DSNV.Count.ToString();
				tbSoNVDangLamViec.Text = SoNVDangLamViec.ToString();
				tbSoNVDaRaVe.Text = SoNVDaRaVe.ToString();
				tbSoNVVang.Text = SoNVKoHienDien.ToString();
				tbVangLyDo.Text = SoNVVang_LyDo.ToString();
				tbVangNghi.Text = SoNVNghi.ToString();
				dgrdTongHop.DataSource = m_BangCTDiemDanh;

			} catch (Exception ex)  //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}

		public frm_DiemDanhNV() {
			m_listIDPhongBan = new List<int>();
			m_DSNV = new List<cUserInfo>();
			m_DSPhg = new List<cPhongBan>();
			m_BangCTDiemDanh = TaoCauTrucTableDiemDanh();

			InitializeComponent();
			log4net.Config.XmlConfigurator.Configure();
			dgrdTongHop.AutoGenerateColumns = false;
			DateTime today = DateTime.Today;
			dtpNgay.Value = new DateTime(today.Year, today.Month, today.Day);
		}

		private void frm_DiemDanhNV_Load(object sender, EventArgs e) {
			#region kiểm tra kết nối csdl trước
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}
			#endregion

			try //general try catch
			{
				#region //2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan ,
				// trường hợp ko có phòng ban nào được phép thao tác thì báo và thoát form

				XL.KhoiTaoDSPhongBan(m_DSPhg, XL2.currUserID);
				if (m_DSPhg.Count == 0) {
					ACMessageBox.Show(Resources.Text_ChuaCapQuyenPhongBanThaoTac, Resources.Caption_ThongBao, 5000);
					Close();
					return;
				}
				XL.loadTreePhgBan(treePhongBan, XL2.TatcaPhongban, m_DSPhg);

				#endregion

				// đăng ký sự kiện cho tree và chọn topNode
				treePhongBan.AfterSelect += treePhongBan_AfterSelect;
				treePhongBan.SelectedNode = treePhongBan.TopNode;
				dtpNgay.ValueChanged += dtpNgay_ValueChanged;

				//vô hiệu hoá các button ko có phân quyền
				btnKBVangNhanh.Enabled = XL2.QuyenThaoTac.Any(o => o == (int)Quyen.KhaiBaoVang);
				btnXoaKBVang.Enabled = XL2.QuyenThaoTac.Any(o => o == (int)Quyen.KhaiBaoVang);
			} catch (Exception ex)  //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}

		private void dtpNgay_ValueChanged(object sender, EventArgs e) {
			try //general try catch
			{
				if (XL.KiemtraDulieuCapnhatTuServer(DateTime.Now) == false)
				{
					ACMessageBox.Show(Resources.Text_DuLieuChamCongChuaUpdate, Resources.Caption_ThongBao, 4000);
				}

				treePhongBan_AfterSelect(treePhongBan, new TreeViewEventArgs(treePhongBan.SelectedNode, TreeViewAction.ByMouse));
			} catch (Exception ex) //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}

		private void btnKBVangNhanh_Click(object sender, EventArgs e) {
			try //general try catch
			{
				//info khai báo vắng nhanh sẽ báo lỗi và log lỗi
				var listMaCC_NV = (from DataGridViewRow dataGridViewRow in dgrdTongHop.SelectedRows
				                   let row = (DataRowView)dataGridViewRow.DataBoundItem
				                   select (int)row["UserEnrollNumber"]).ToList();
				if (listMaCC_NV.Count == 0) {
					ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000);
					return;
				}
				IEnumerable<dynamic> templist = (from macc in listMaCC_NV
				                                 select new { MaCC = macc, NgayVang = dtpNgay.Value.Date });
				frm_KBVang_Nhanh frm = new frm_KBVang_Nhanh { listMaCC_NgayVang = templist, StartPosition = FormStartPosition.CenterParent};

				frm.ShowDialog();
				if (frm.IsReload) {
					dtpNgay_ValueChanged(null, null);
				}
			} catch (Exception ex)  //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}

		private void btnXoaKBVang_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			#region chưa chọn nhân viên thì báo
			var listMaCC_NV = (from DataGridViewRow dataGridViewRow in dgrdTongHop.SelectedRows
							   let row = (DataRowView)dataGridViewRow.DataBoundItem
							   select (int)row["UserEnrollNumber"]).ToList();
			if (listMaCC_NV.Count == 0)
			{
				ACMessageBox.Show(Resources.Text_ChuaChonNV, Resources.Caption_ThongBao, 2000);
				return;
			}

			#endregion

			if (MessageBox.Show(string.Format("Xoá tất cả khai báo vắng ngày {0} của các nhân viên được chọn?", dtpNgay.Value.Date.ToString("dddd dd/MM/yyyy")), 
				Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) return;
			IEnumerable<dynamic> templist = (from macc in listMaCC_NV
											 select new { MaCC = macc, NgayVang = dtpNgay.Value.Date });
			string query = " delete from Absent where UserEnrollNumber = @UserEnrollNumber and TimeDate = @TimeDate ";
			try
			{
				foreach (dynamic obj in templist) {
					SqlDataAccessHelper.ExecNoneQueryString(
						query,
						new string[] { "@UserEnrollNumber", "@TimeDate" },
						new object[] { obj.MaCC, obj.NgayVang });
					DAO5.GhiNhatKyThaotac("Xoá các khai báo vắng trong ngày",
										 string.Format("Xoá tất cả khai báo vắng của NV có mã chấm công [{0}] trong ngày [{1}]", (int)obj.MaCC, ((DateTime)obj.NgayVang).ToString("dd/MM/yyyy")), maCC: (int)obj.MaCC);
				}
			}
			catch (Exception ex)
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
			dtpNgay_ValueChanged(null, null);
		}

		private void btnXuatBB_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			//1. lấy dữ liệu từ form
			#region lấy ngày BD và kết thúc, và update lại Ngày BD = 1 ngày trước 31/08 12:00 AM, ngày KT là 1 ngày sau ngay 1 23:59:59
			dtpNgay.Update();
			DateTime ngayBD = dtpNgay.Value.Date;
			ngayBD = ngayBD.AddDays(-2d);
			DateTime ngayKT = ngayBD.AddDays(4d);
			#endregion


			try //general try catch
			{
				if (saveFileDlgDiemDanh.ShowDialog() == DialogResult.Cancel || saveFileDlgDiemDanh.FileName == String.Empty) {
					return;
				}
				var saveFileName = saveFileDlgDiemDanh.FileName;


				using (var p = new ExcelPackage()) {
					#region ghi sheet diemdanh

					p.Workbook.Worksheets.Add("DiemDanhNV");
					var ws = p.Workbook.Worksheets["DiemDanhNV"];
					ws.Name = "DiemDanhNV"; //Setting Sheet's name
					XL.ExportSheetDiemDanh(ws, m_DSNV); 

					#endregion

					#region Ghi file , nếu xảy ra lỗi thì báo
					Byte[] bin = p.GetAsByteArray();
					XL.XuatFileExcel(saveFileName, bin, "btnXuatBB_Click XuatBBDiemDanh");
					#endregion

				}
			} catch (Exception ex) //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}


	}
}
