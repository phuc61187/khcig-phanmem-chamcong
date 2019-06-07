using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DAL;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;
using log4net;

namespace ChamCong_v04.UI.ChamCong {

	public partial class frm_ThemXoaSuaGioHangLoat : Form {
		#region log tooltip và hàm ko quan trọng
		private readonly ILog lg = LogManager.GetLogger("frm_ThemXoaSuaGioHangLoat");
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
			this.Close();
		}

		#endregion

		public bool IsReload;
		public List<cUserInfo> m_DSNV;
		public List<cCa> dsCa;
		public DataRow[] selectedRow;
		public DataTable m_Bang_ChiTiet;
		public DataTable TaoTable_ChiTiet() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "UserFullCode", "UserFullName", "UserEnrollNumber", "cUserInfo", "cNgayCong", "cCheckInOut", "TimeStrNgay", "TimeStrVao", "TimeStrRaa", "ShiftCode", },
				new[] { typeof(string), typeof(string), typeof(string), typeof(cUserInfo), typeof(cNgayCong), typeof(cCheckInOut), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(string),  }
				);
			return kq;
		}


		public frm_ThemXoaSuaGioHangLoat() {
			InitializeComponent();
			m_Bang_ChiTiet = TaoTable_ChiTiet();

			dgrdDSGioVaoRa.AutoGenerateColumns = false;
			dgrdDSGioVaoRa.DataSource = m_Bang_ChiTiet;
		}

		private void frm_ChiTietVaoRa_Load(object sender, EventArgs e) {
			IsReload = false; // mặc định ko có gì thay đổi, nếu có sử dụng chức năng thêm, xoá sửa, đảo thì this.DialogResult = Yes;

			try //general try catch
			{
				loadGrid();
				cbCa.DataSource = XL.DSCa;
				cbCa.ValueMember = "ID";
				cbCa.DisplayMember = "Code";
			} catch (Exception ex) //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}

		public void loadGrid() {
			m_Bang_ChiTiet.Rows.Clear();
			//load datagrid từ dataTable
			foreach (var row in selectedRow) {
				var nhanvien = (cUserInfo)row["cUserInfo"];
				var ngayCong = (cNgayCong)row["cNgayCong"];
				loadRow(nhanvien, ngayCong);
			}

			// reset lại các control trong Thêm, Xóa Sửa
		}
		private void loadRow(cUserInfo nhanvien_goc, cNgayCong ngayCong)
		{
			if (ngayCong 				== null || ngayCong.DSVaoRa.Count == 0) return;
			foreach (var CIO in ngayCong.DSVaoRa) {
				var row 				= m_Bang_ChiTiet.NewRow();
				row["UserEnrollNumber"] = nhanvien_goc.MaCC;
				row["UserFullName"] 	= nhanvien_goc.TenNV;
				row["UserFullCode"] 	= nhanvien_goc.MaNV;
				row["TimeStrNgay"] 		= ngayCong.Ngay;
				row["TimeStrVao"] 		= (CIO.Vao != null) ? CIO.Vao.Time : (object)DBNull.Value;
				row["TimeStrRaa"] 		= (CIO.Raa != null) ? CIO.Raa.Time : (object)DBNull.Value;
				row["ShiftCode"] 		= CIO.CIOCodeFull();
				row["cUserInfo"] 		= nhanvien_goc;
				row["cCheckInOut"] 		= CIO;
				row["cNgayCong"] 		= ngayCong;
				m_Bang_ChiTiet.Rows.Add(row);
			}

		}


		private void dgrdTongHop_SelectionChanged(object sender, EventArgs e) {
			MyUtility.EnableDisableControl((dgrdDSGioVaoRa.SelectedRows.Count != 0), new Control[] { btnThem, btnSuaa, btnXoaGioVao, btnXoaGioRaa });

		}

		private void btnThem_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			IsReload = true;

			#region lấy thông tin

			var timespanGioVao = dtpVao.Value.TimeOfDay;
			var timespanGioRaa = dtpRaa.Value.TimeOfDay;
			var lydo = (cbLyDo.SelectedItem != null) ? cbLyDo.SelectedItem.ToString() : cbLyDo.Text;
			var ghichu = tbGhiChu.Text;

			#endregion

			var tempstring = radGioVao.Checked ? "vào" : "ra";
			var temptime = (radGioVao.Checked) ? dtpVao.Value : dtpRaa.Value;
			if (dgrdDSGioVaoRa.SelectedRows.Count == 0) return;

			#region hỏi lại trước khi thêm

			if (MessageBox.Show(string.Format(Resources.Text_xacNhanThemGioHangLoat, tempstring, temptime.ToString("H:mm")),
								Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}

			#endregion

			try //general try catch
			{
				#region lấy ds các giờ vào ra, group lại theo từng nhân viên để thực hiện thêm 1 loạt rồi sau đó xử lý tính toán lại checkinout và ngày công

				var arrRecord = (from DataGridViewRow dataGridViewRow in dgrdDSGioVaoRa.SelectedRows
				                 let row = (DataRowView)dataGridViewRow.DataBoundItem
				                 let nv = (cUserInfo)(row["cUserInfo"])
				                 let cCheckInOut = (cCheckInOut)(row["cCheckInOut"])
				                 let ngay = (DateTime)(row["TimeStrNgay"])
				                 select new { nhanvien = nv, CIO = cCheckInOut, Ngay = ngay }).GroupBy(o => o.nhanvien).ToList();

				#endregion
				var ds_raa3_vao1 = new List<cCheck>();
				var DS_Check_KoHopLe = new List<cCheck>();
				foreach (var group in arrRecord) {
					var nhanvien = group.Key;
					foreach (var item in @group.Where(item => item.CIO.HaveINOUT != 0))
					{
						if (radGioVao.Checked) {//2. thêm I cho O kv
							if (item.CIO.HaveINOUT == -2) {
								var giovao = item.Ngay.Add(timespanGioVao);
								if (giovao > item.CIO.Raa.Time) giovao = giovao.AddDays(-1d);// trừ đi 1 ngày nếu (giờ vào thêm) > giờ ra
								var checkinn = new cCheck { MaCC = item.nhanvien.MaCC, Type = "I", Time = giovao, Source = "PC", MachineNo = 21, PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false } };
								XL.ThemGioChoNV(item.nhanvien.MaCC, checkinn, item.nhanvien.DS_Check_A, lydo, ghichu);
							}
						}
						else {
							if (item.CIO.HaveINOUT == -1) {//3. thêm O cho I kr
								var gioraa = item.Ngay.Add(timespanGioRaa);
								if (gioraa < item.CIO.Vao.Time) gioraa = gioraa.AddDays(1d);// cộng thêm 1 ngày nếu (giờ vào thêm) > giờ ra
								var checkout = new cCheck { MaCC = item.nhanvien.MaCC, Type = "O", Time = gioraa, Source = "PC", MachineNo = 22, PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false } };
								XL.ThemGioChoNV(item.nhanvien.MaCC, checkout, item.nhanvien.DS_Check_A, lydo, ghichu);
							}
						}
					}

					XL.LoaiBoCheckKoHopLe1(nhanvien.DS_Check_A, ref DS_Check_KoHopLe);
					XL.GhepCIO_A2(nhanvien.DS_Check_A, nhanvien.DS_CIO_A);
					XL.XetCa_ListCIO_A3(nhanvien.DS_CIO_A, nhanvien.LichTrinhLV, ds_raa3_vao1, nhanvien.DS_Check_A);
					XL.TronDS_CIO_A_V5(nhanvien.DS_CIO_A, nhanvien.DS_CIO_V, nhanvien.DSVaoRa);
					XL.PhanPhoi_DSVaoRa6(nhanvien.DSVaoRa, nhanvien.DSNgayCong);
					XL.PhanPhoi_DSVang7(nhanvien.DSVang, nhanvien.DSNgayCong);
					XL.TinhCong_ListNgayCong8(nhanvien.DSNgayCong, nhanvien.StartNT, nhanvien.EndddNT);//ver 4.0.0.4
					XL.TinhPCTC_TrongListXNPCTC9(nhanvien.DSXNPhuCap50, nhanvien.DSNgayCong, nhanvien.NVNhanKiet);
					XL.TinhPCDB_TrongListXNPCDB10(nhanvien.DSXNPhuCapDB, nhanvien.DSNgayCong);
					XL.TinhPCNgayVang(nhanvien.DSVang, nhanvien.DSNgayCong);
				}
				if (DS_Check_KoHopLe.Count > 0) DAO.LoaiGioLienQuan(DS_Check_KoHopLe);
				if (ds_raa3_vao1.Count > 0) DAO.ThemGio_ra3_vao1(ds_raa3_vao1);

				loadGrid();
			} catch (Exception ex) //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}

		private void btnSuaa_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			IsReload = true;
			var timespanGioVao = dtpVao.Value.TimeOfDay;
			var timespanGioRaa = dtpRaa.Value.TimeOfDay;
			var lydo = (cbLyDo.SelectedItem != null) ? cbLyDo.SelectedItem.ToString() : cbLyDo.Text;
			var ghichu = tbGhiChu.Text;

			if (dgrdDSGioVaoRa.SelectedRows.Count == 0) return;

			#region hỏi lại trước khi sửa
			if (MessageBox.Show(string.Format(Resources.Text_xacNhanSuaGioHangLoat, 
												(radGioVao.Checked)?"vào":"ra", 
												(radGioVao.Checked)?dtpVao.Value.ToString("H:mm"): dtpRaa.Value.ToString("H:mm")),
												Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
								return;
							}

			#endregion

			try //general try catch
			{
				var flagThongBao = false; // flag thông báo không cho phép sửa giờ đã CN
				var arrRecord = (from DataGridViewRow dataGridViewRow in dgrdDSGioVaoRa.SelectedRows
				                 let row = (DataRowView)dataGridViewRow.DataBoundItem
				                 let nv = (cUserInfo)row["cUserInfo"]
				                 let cCheckInOut = (cCheckInOut)row["cCheckInOut"]
				                 let ngay = (DateTime)row["TimeStrNgay"]
				                 select new { nhanvien = nv, CIO = cCheckInOut, Ngay = ngay }).GroupBy(o => o.nhanvien).ToList();
				var ds_raa3_vao1 = new List<cCheck>();
				var DS_Check_KoHopLe = new List<cCheck>();
				foreach (var group in arrRecord) {
					var nhanvien = group.Key;
					foreach (var item in group) {
						if (item.CIO.DaXN) { // ko cho sửa các giờ đã xác nhận
							flagThongBao = true;
							continue;
						}
						if (radGioVao.Checked) {
							if (item.CIO.HaveINOUT == -2) continue;
							var giovao 			   = item.Ngay.Add(timespanGioVao);
							var checkinnold 	   = item.CIO.Vao;
							var checkinnnew 	   = new cCheck { MaCC = nhanvien.MaCC, Type = "I", Time = giovao, Source = "PC", MachineNo = 21, 
								PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = checkinnold.PhucHoi.Them, Xoaa = checkinnold.PhucHoi.Xoaa } };
							XL.SuaGioChoNV(nhanvien.MaCC, checkinnold, checkinnnew, nhanvien.DS_Check_A, lydo, ghichu);
						}
						else {
							if (item.CIO.HaveINOUT == -1) continue;
							var gioraa 			   = item.Ngay.Add(timespanGioRaa);
							var checkoutold 	   = item.CIO.Raa;
							var checkoutnew 	   = new cCheck { MaCC = nhanvien.MaCC, Type = "O", Time = gioraa, Source = "PC", MachineNo = 22, 
								PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = checkoutold.PhucHoi.Them, Xoaa = checkoutold.PhucHoi.Xoaa } };
							XL.SuaGioChoNV(nhanvien.MaCC, checkoutold, checkoutnew, nhanvien.DS_Check_A, lydo, ghichu);
						}
					}
					XL.LoaiBoCheckKoHopLe1(nhanvien.DS_Check_A, ref DS_Check_KoHopLe);
					XL.GhepCIO_A2(nhanvien.DS_Check_A, nhanvien.DS_CIO_A);
					XL.XetCa_ListCIO_A3(nhanvien.DS_CIO_A, nhanvien.LichTrinhLV, ds_raa3_vao1, nhanvien.DS_Check_A);
					XL.TronDS_CIO_A_V5(nhanvien.DS_CIO_A, nhanvien.DS_CIO_V, nhanvien.DSVaoRa);
					XL.PhanPhoi_DSVaoRa6(nhanvien.DSVaoRa, nhanvien.DSNgayCong);
					XL.PhanPhoi_DSVang7(nhanvien.DSVang, nhanvien.DSNgayCong);
					XL.TinhCong_ListNgayCong8(nhanvien.DSNgayCong, nhanvien.StartNT, nhanvien.EndddNT);//ver 4.0.0.4
					XL.TinhPCTC_TrongListXNPCTC9(nhanvien.DSXNPhuCap50, nhanvien.DSNgayCong, nhanvien.NVNhanKiet);
					XL.TinhPCDB_TrongListXNPCDB10(nhanvien.DSXNPhuCapDB, nhanvien.DSNgayCong);
					XL.TinhPCNgayVang(nhanvien.DSVang, nhanvien.DSNgayCong);

				}
				if (DS_Check_KoHopLe.Count > 0) DAO.LoaiGioLienQuan(DS_Check_KoHopLe);
				if (ds_raa3_vao1.Count > 0) DAO.ThemGio_ra3_vao1(ds_raa3_vao1);

				loadGrid();
				if (flagThongBao)
				{
					ACMessageBox.Show(Resources.Text_KoTheSuaXoaGioDaXN, Resources.Caption_ThongBao, 2000);
				}
			} catch (Exception ex) //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}

		private void btnXoaGioVao_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			IsReload = true;
			var lydo = (cbLyDo.SelectedItem != null) ? cbLyDo.SelectedItem.ToString() : cbLyDo.Text;
			var ghichu = tbGhiChu.Text;

			if (dgrdDSGioVaoRa.SelectedRows.Count == 0) return;

			#region hỏi lại trước khi xoá

			if (MessageBox.Show(string.Format(Resources.Text_xacNhanXoaGioHangLoat, "vào"), Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}

			#endregion

			try //general try catch
			{
				var flagThongBao = false;// thông báo ko được xoá giờ đãn XN
				var arrRecord = (from DataGridViewRow dataGridViewRow in dgrdDSGioVaoRa.SelectedRows
				                 let row = (DataRowView)dataGridViewRow.DataBoundItem
				                 let nv = (cUserInfo)(row["cUserInfo"])
				                 let cCheckInOut = (cCheckInOut)(row["cCheckInOut"])
				                 let ngay = (DateTime)(row["TimeStrNgay"])
				                 select new { nhanvien = nv, CIO = cCheckInOut, Ngay = ngay }).GroupBy(o => o.nhanvien).ToList();
				var ds_raa3_vao1 = new List<cCheck>();
				var DS_Check_KoHopLe = new List<cCheck>();
				foreach (var groupByNV in arrRecord) {
					var nhanvien = groupByNV.Key;
					foreach (var row in groupByNV) {
						if (row.CIO.DaXN)
						{
							flagThongBao = true;
							continue;
						}
						if (row.CIO.HaveINOUT == -2) continue;
						var checkinn = row.CIO.Vao;
						XL.XoaGioChoNV(nhanvien.MaCC, checkinn, row.nhanvien.DS_Check_A, lydo, ghichu);
					}
					XL.LoaiBoCheckKoHopLe1(nhanvien.DS_Check_A, ref DS_Check_KoHopLe);
					XL.GhepCIO_A2(nhanvien.DS_Check_A, nhanvien.DS_CIO_A);
					XL.XetCa_ListCIO_A3(nhanvien.DS_CIO_A, nhanvien.LichTrinhLV, ds_raa3_vao1, nhanvien.DS_Check_A);
					XL.TronDS_CIO_A_V5(nhanvien.DS_CIO_A, nhanvien.DS_CIO_V, nhanvien.DSVaoRa);
					XL.PhanPhoi_DSVaoRa6(nhanvien.DSVaoRa, nhanvien.DSNgayCong);
					XL.PhanPhoi_DSVang7(nhanvien.DSVang, nhanvien.DSNgayCong);
					XL.TinhCong_ListNgayCong8(nhanvien.DSNgayCong, nhanvien.StartNT, nhanvien.EndddNT);//ver 4.0.0.4
					XL.TinhPCTC_TrongListXNPCTC9(nhanvien.DSXNPhuCap50, nhanvien.DSNgayCong, nhanvien.NVNhanKiet);
					XL.TinhPCDB_TrongListXNPCDB10(nhanvien.DSXNPhuCapDB, nhanvien.DSNgayCong);
					XL.TinhPCNgayVang(nhanvien.DSVang, nhanvien.DSNgayCong);

				}
				if (DS_Check_KoHopLe.Count > 0) DAO.LoaiGioLienQuan(DS_Check_KoHopLe);
				if (ds_raa3_vao1.Count > 0) DAO.ThemGio_ra3_vao1(ds_raa3_vao1);

				loadGrid();

				if (flagThongBao) {
					ACMessageBox.Show(Resources.Text_KoTheSuaXoaGioDaXN, Resources.Caption_ThongBao, 2000);
				}
			} catch (Exception ex) //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}

		private void btnXoaGioRaa_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			IsReload = true;
			var lydo = (cbLyDo.SelectedItem != null) ? cbLyDo.SelectedItem.ToString() : cbLyDo.Text;
			var ghichu = tbGhiChu.Text;

			#region hỏi lại ttrước khi xoá

			if (MessageBox.Show(string.Format(Resources.Text_xacNhanXoaGioHangLoat, "ra"), Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}

			#endregion


			if (dgrdDSGioVaoRa.SelectedRows.Count == 0) return;
			var flagThongBao = false;// thông báo ko được xoá giờ đãn XN

			try //general try catch
			{
				var arrRecord = (from DataGridViewRow dataGridViewRow in dgrdDSGioVaoRa.SelectedRows
				                 let row = (DataRowView)dataGridViewRow.DataBoundItem
				                 let nv = (cUserInfo)(row["cUserInfo"])
				                 let cCheckInOut = (cCheckInOut)(row["cCheckInOut"])
				                 let ngay = (DateTime)(row["TimeStrNgay"])
				                 select new { nhanvien = nv, CIO = cCheckInOut, Ngay = ngay }).GroupBy(o => o.nhanvien).ToList();

				var ds_raa3_vao1 = new List<cCheck>();
				var DS_Check_KoHopLe = new List<cCheck>();
				foreach (var groupByNV in arrRecord) {
					var nhanvien = groupByNV.Key;
					foreach (var row in groupByNV) {
						if (row.CIO.DaXN) {
							flagThongBao = true;
							continue;
						}

						if (row.CIO.HaveINOUT == -1) continue;
						var checkout = row.CIO.Raa;
						XL.XoaGioChoNV(nhanvien.MaCC, checkout, row.nhanvien.DS_Check_A, lydo, ghichu);
					}
					XL.LoaiBoCheckKoHopLe1(nhanvien.DS_Check_A, ref DS_Check_KoHopLe);
					XL.GhepCIO_A2(nhanvien.DS_Check_A, nhanvien.DS_CIO_A);
					XL.XetCa_ListCIO_A3(nhanvien.DS_CIO_A, nhanvien.LichTrinhLV, ds_raa3_vao1, nhanvien.DS_Check_A);
					XL.TronDS_CIO_A_V5(nhanvien.DS_CIO_A, nhanvien.DS_CIO_V, nhanvien.DSVaoRa);
					XL.PhanPhoi_DSVaoRa6(nhanvien.DSVaoRa, nhanvien.DSNgayCong);
					XL.PhanPhoi_DSVang7(nhanvien.DSVang, nhanvien.DSNgayCong);
					XL.TinhCong_ListNgayCong8(nhanvien.DSNgayCong, nhanvien.StartNT, nhanvien.EndddNT);//ver 4.0.0.4
					XL.TinhPCTC_TrongListXNPCTC9(nhanvien.DSXNPhuCap50, nhanvien.DSNgayCong, nhanvien.NVNhanKiet);
					XL.TinhPCDB_TrongListXNPCDB10(nhanvien.DSXNPhuCapDB, nhanvien.DSNgayCong);
					XL.TinhPCNgayVang(nhanvien.DSVang, nhanvien.DSNgayCong);

				}
				if (DS_Check_KoHopLe.Count > 0) DAO.LoaiGioLienQuan(DS_Check_KoHopLe);
				if (ds_raa3_vao1.Count > 0) DAO.ThemGio_ra3_vao1(ds_raa3_vao1);

				loadGrid();


				if (flagThongBao) {
					ACMessageBox.Show(Resources.Text_KoTheSuaXoaGioDaXN, Resources.Caption_ThongBao, 2000);
				}
			} catch (Exception ex) //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}

		}



		private void cbCa_SelectionChangeCommitted(object sender, EventArgs e)
		{
			try //general try catch
			{
				if (cbCa.SelectedItem == null)return;
				var item = (cCa) cbCa.SelectedItem;
				dtpVao.Value = DateTime.Today.Date.Add(item.Duty.Onn);
				dtpRaa.Value = DateTime.Today.Date.Add(item.Duty.Off);
			} catch (Exception ex) //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}


	}
}
