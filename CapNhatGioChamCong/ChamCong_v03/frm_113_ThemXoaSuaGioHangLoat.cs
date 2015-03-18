using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;
using ChamCong_v03.Properties;

using log4net;

namespace ChamCong_v03 {

	public partial class frm_113_ThemXoaSuaGioHangLoat : Form {
		private readonly ILog lg = LogManager.GetLogger("frm_111_ChiTietVaoRa");
		public bool IsReload;
		public List<cUserInfo> m_DSNV;
		public List<cCaAbs> dsCa;
		public DataRow[] selectedRow;
		public DataTable m_Bang_ChiTiet;
		public DataTable TaoTable_ChiTiet() {
			var kq = XL.TaoCauTrucDataTable(
				new[] { "UserFullCode", "UserFullName", "UserEnrollNumber", "cUserInfo", "cNgayCong", "cChkInOut", "TimeStrNgay", "TimeStrVao", "TimeStrRaa", "ShiftCode", "IsEdited", },
				new[] { typeof(string), typeof(string), typeof(string), typeof(cUserInfo), typeof(cNgayCong), typeof(cChkInOut), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(string), typeof(bool), }
				);
			return kq;
		}


		public frm_113_ThemXoaSuaGioHangLoat() {
			InitializeComponent();
			m_Bang_ChiTiet = TaoTable_ChiTiet();

			dgrdDSGioVaoRa.AutoGenerateColumns = false;
			dgrdDSGioVaoRa.DataSource = m_Bang_ChiTiet;
		}

		private void frm_ChiTietVaoRa_Load(object sender, EventArgs e) {
			IsReload = false; // mặc định ko có gì thay đổi, nếu có sử dụng chức năng thêm, xoá sửa, đảo thì this.DialogResult = Yes;
			dsCa = new List<cCaAbs>(XL.DSCa);
			dsCa.Insert(0, new cCaChuan { ID = -1, Code = "--" });
			cbCa.ValueMember = "ID";
			cbCa.DisplayMember = "Code";
			cbCa.DataSource = dsCa;

			loadGrid();
		}

		public void loadGrid() {
			m_Bang_ChiTiet.Rows.Clear();
			//load datagrid từ dataTable
			foreach (var row in selectedRow) {
				var nhanvien = (cUserInfo)row["cUserInfo"];
				var ngayCong = (DateTime)row["TimeStrNgay"];
				loadRow(nhanvien, ngayCong);
			}

			VeLaiCacGioCoThayDoi();
			// reset lại các control trong Thêm, Xóa Sửa
		}

		private void loadRow(cUserInfo nhanvien_goc, DateTime ngayCong) {
			var cNgayCong 				= nhanvien_goc.DSNgayCong.FirstOrDefault(item => item.Ngay == ngayCong);
			if (cNgayCong 				== null || cNgayCong.HasCheck == false) return;
			foreach (var CIO in cNgayCong.DSVaoRa) {
				var row 				= m_Bang_ChiTiet.NewRow();
				row["UserEnrollNumber"] = nhanvien_goc.MaCC;
				row["UserFullName"] 	= nhanvien_goc.TenNV;
				row["UserFullCode"] 	= nhanvien_goc.MaNV;
				row["TimeStrNgay"] 		= cNgayCong.Ngay;
				row["TimeStrVao"] 		= (CIO.Vao != null) ? CIO.Vao.Time : (object)DBNull.Value;
				row["TimeStrRaa"] 		= (CIO.Raa != null) ? CIO.Raa.Time : (object)DBNull.Value;
				row["ShiftCode"] 		= CIO.CIOCodeFull();
				row["cUserInfo"] 		= nhanvien_goc;
				row["cChkInOut"] 		= CIO;
				row["cNgayCong"] 		= cNgayCong;
				row["IsEdited"] 		= CIO.IsEdited;
				m_Bang_ChiTiet.Rows.Add(row);
			}

		}


		private void dgrdTongHop_SelectionChanged(object sender, EventArgs e) {
			BUS.MyUtility.EnableDisableControl((dgrdDSGioVaoRa.SelectedRows.Count != 0), new Control[] { btnThem, btnSuaa, btnXoaGioVao, btnXoaGioRaa });

		}

		private void cbCa_SelectionChangeCommitted(object sender, EventArgs e) {
			// ko làm gì nếu ko có item hoặc ko item chọn là tuỳ chỉnh
			if (cbCa.Items.Count == 0 || cbCa.SelectedIndex == 0) return;

			var ca = (cCaChuan)(cbCa.SelectedItem);
			var now = DateTime.Now;
			dtpVao.Value = new DateTime(now.Year, now.Month, now.Day, ca.OnnTS.Hours, ca.OnnTS.Minutes, 1);
			dtpRaa.Value = new DateTime(now.Year, now.Month, now.Day, ca.OffTS.Hours, ca.OffTS.Minutes, 0);
		}

		private void btnThem_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			IsReload = true;

			#region lấy thông tin

			var timespanGioVao = dtpVao.Value.TimeOfDay;
			var timespanGioRaa = dtpRaa.Value.TimeOfDay;
			var lydo = (cbLyDo.SelectedItem != null) ? cbLyDo.SelectedItem.ToString() : cbLyDo.SelectedText;
			var ghichu = tbGhiChu.Text;

			#endregion

			var tempstring = radGioVao.Checked ? "vào" : "ra";
			var temptime = (radGioVao.Checked) ? dtpVao.Value : dtpRaa.Value;
			if (dgrdDSGioVaoRa.SelectedRows.Count == 0) return;

			#region hỏi lại trước khi thêm

			if (MessageBox.Show(string.Format(Resources.xacNhanThemGioHangLoat, tempstring, temptime.ToString("H:mm")),
								Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}

			#endregion

			#region lấy ds các giờ vào ra, group lại theo từng nhân viên để thực hiện thêm 1 loạt rồi sau đó xử lý tính toán lại checkinout và ngày công

			IEnumerable<DataGridViewRow> dataGridViewRows = dgrdDSGioVaoRa.SelectedRows.Cast<DataGridViewRow>();
			var arrRecord = (from row in dataGridViewRows
							 let nv = (cUserInfo)(((DataRowView)row.DataBoundItem).Row["cUserInfo"])
							 let cChkInOut = (cChkInOut)(((DataRowView)row.DataBoundItem).Row["cChkInOut"])
							 let ngay = (DateTime)(((DataRowView)row.DataBoundItem).Row["TimeStrNgay"])
							 select new { nhanvien = nv, CIO = cChkInOut, Ngay = ngay }).GroupBy(o => o.nhanvien).ToList();

			#endregion

			var ds_raa3_vao1 = new List<cChk>();
			var DS_Check_KoHopLe = new List<cChk>();
			foreach (var group in arrRecord) {
				var nhanvien = group.Key;
				foreach (var item in group) {
					if (item.CIO.HaveINOUT == 1) continue;// 1. bỏ qua các giờ đủ IO
					if (radGioVao.Checked) {//2. thêm I cho O kv
						if (item.CIO.HaveINOUT == -2) {
							var giovao = item.Ngay.Add(timespanGioVao);
							if (giovao > item.CIO.Raa.Time) giovao = giovao.AddDays(-1d);// trừ đi 1 ngày nếu (giờ vào thêm) > giờ ra
							var checkinn = new cChkInn_A { IsEdited = 1, MaCC = item.nhanvien.MaCC, Type = "I", Time = giovao, Source = "PC", MachineNo = 21, PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false } };
							XL.ThemGioChoNV(checkinn, item.nhanvien, XL2.currUserID, lydo, ghichu);
						}
					}
					else {
						if (item.CIO.HaveINOUT == -1) {//3. thêm O cho I kr
							var gioraa = item.Ngay.Add(timespanGioRaa);
							if (gioraa < item.CIO.Vao.Time) gioraa = gioraa.AddDays(1d);// cộng thêm 1 ngày nếu (giờ vào thêm) > giờ ra
							var checkout = new cChkOut_A() { IsEdited = 1, MaCC = item.nhanvien.MaCC, Type = "O", Time = gioraa, Source = "PC", MachineNo = 22, PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false } };
							XL.ThemGioChoNV(checkout, item.nhanvien, XL2.currUserID, lydo, ghichu);
						}
					}
				}

				XL.SapXepDS_Check(new[] { nhanvien.DS_Check_A });

				XL.LoaiBoCheckCungLoaiTrong30phut(nhanvien.DS_Check_A, nhanvien.DS_Check_KoHopLe);
				XL.GhepCIO_A(nhanvien.DS_Check_A, nhanvien.DS_CIO_A);
				XL.LoaiBoCIOKoHopLe(nhanvien.DS_CIO_A, nhanvien.DS_Check_A, nhanvien.DS_Check_KoHopLe);
				if (nhanvien.DS_Check_KoHopLe.Count > 0) {
					DS_Check_KoHopLe.AddRange(nhanvien.DS_Check_KoHopLe);
					nhanvien.DS_Check_KoHopLe.Clear();
				}

				XL.XetCa_CIO_A(nhanvien.DS_CIO_A, nhanvien.LichTrinhLV.DSCa, ds_raa3_vao1, nhanvien.DS_Check_A);
				XL.TronDS_CIO_A_V(nhanvien.DS_CIO_A, nhanvien.DS_CIO_V, nhanvien.DSVaoRa);
				XL.TinhCongTheoNgay(nhanvien.DSVaoRa, nhanvien.NgayCongBD_Bef2D, nhanvien.NgayCongKT_Aft2D, nhanvien.DSVang, nhanvien.DSNgayCong, nhanvien.MacDinhTinhPC50);
				XL.TinhLaiPhuCapTC(nhanvien.DSXNPhuCap50,nhanvien.DSNgayCong);
				XL.TinhLaiPhuCapDB(nhanvien.DSXNPhuCapDB, nhanvien.DSNgayCong);
			}
			if (DS_Check_KoHopLe.Count > 0) DAL.LoaiGioLienQuan(DS_Check_KoHopLe);
			if (ds_raa3_vao1.Count > 0) DAL.ThemGio_ra3_vao1(ds_raa3_vao1);

			loadGrid();

		}

		private void btnSuaa_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			IsReload = true;
			var timespanGioVao = dtpVao.Value.TimeOfDay;
			var timespanGioRaa = dtpRaa.Value.TimeOfDay;
			var lydo = (cbLyDo.SelectedItem != null) ? cbLyDo.SelectedItem.ToString() : cbLyDo.SelectedText;
			var ghichu = tbGhiChu.Text;

			if (dgrdDSGioVaoRa.SelectedRows.Count == 0) return;

			#region hỏi lại trước khi sửa

			if (radGioVao.Checked) {
				if (MessageBox.Show(string.Format(Resources.xacNhanSuaGioHangLoat, "vào", dtpVao.Value.ToString("H:mm")),
									Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
					return;
				}
			}
			else {
				if (MessageBox.Show(string.Format(Resources.xacNhanSuaGioHangLoat, "ra", dtpRaa.Value.ToString("H:mm")),
									Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
					return;
				}
			}

			#endregion

			var flagThongBao = false; // flag thông báo không cho phép sửa giờ đã CN
			IEnumerable<DataGridViewRow> dataGridViewRows = dgrdDSGioVaoRa.SelectedRows.Cast<DataGridViewRow>();
			var arrRecord = (from row in dataGridViewRows
							 let nv = (cUserInfo)(((DataRowView)row.DataBoundItem).Row["cUserInfo"])
							 let cChkInOut = (cChkInOut)(((DataRowView)row.DataBoundItem).Row["cChkInOut"])
							 let ngay = (DateTime)(((DataRowView)row.DataBoundItem).Row["TimeStrNgay"])
							 select new { nhanvien = nv, CIO = cChkInOut, Ngay = ngay }).GroupBy(o => o.nhanvien).ToList();
			var ds_raa3_vao1 = new List<cChk>();
			var DS_Check_KoHopLe = new List<cChk>();
			foreach (var group in arrRecord) {
				var nhanvien = group.Key;
				foreach (var item in group) {
					if (item.CIO.GetType() == typeof(cChkInOut_V)) {
						flagThongBao = true;
						continue;
					}
					if (radGioVao.Checked) {
						if (item.CIO.HaveINOUT == -2) continue;
						var giovao 			   = item.Ngay.Add(timespanGioVao);
						var checkinnold 	   = item.CIO.Vao;
						var checkinnnew 	   = new cChkInn_A { MaCC = nhanvien.MaCC, Type = "I", Time = giovao, Source = "PC", MachineNo = 21, IsEdited = 1, 
																PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = checkinnold.PhucHoi.Them, Xoaa = checkinnold.PhucHoi.Xoaa } };
						XL.SuaGioChoNV(checkinnold, checkinnnew, nhanvien, XL2.currUserID, lydo, ghichu);
					}
					else {
						if (item.CIO.HaveINOUT == -1) continue;
						var gioraa 			   = item.Ngay.Add(timespanGioRaa);
						var checkoutold 	   = item.CIO.Raa;
						var checkoutnew 	   = new cChkOut_A { MaCC = nhanvien.MaCC, Type = "O", Time = gioraa, Source = "PC", MachineNo = 22, IsEdited = 1, 
																PhucHoi = new cPhucHoi { IDGioGoc = int.MaxValue, Them = checkoutold.PhucHoi.Them, Xoaa = checkoutold.PhucHoi.Xoaa } };
						XL.SuaGioChoNV(checkoutold, checkoutnew, nhanvien, XL2.currUserID, lydo, ghichu);
					}
				}
				XL.SapXepDS_Check(new[] { nhanvien.DS_Check_A });
				XL.LoaiBoCheckCungLoaiTrong30phut(nhanvien.DS_Check_A, nhanvien.DS_Check_KoHopLe);
				if (nhanvien.DS_Check_KoHopLe.Count > 0) {
					DS_Check_KoHopLe.AddRange(nhanvien.DS_Check_KoHopLe);
					nhanvien.DS_Check_KoHopLe.Clear();
				}

				XL.GhepCIO_A(nhanvien.DS_Check_A, nhanvien.DS_CIO_A);
				XL.LoaiBoCIOKoHopLe(nhanvien.DS_CIO_A, nhanvien.DS_Check_A, nhanvien.DS_Check_KoHopLe);
				XL.XetCa_CIO_A(nhanvien.DS_CIO_A, nhanvien.LichTrinhLV.DSCa, ds_raa3_vao1, nhanvien.DS_Check_A);
				XL.TronDS_CIO_A_V(nhanvien.DS_CIO_A, nhanvien.DS_CIO_V, nhanvien.DSVaoRa);
				XL.TinhCongTheoNgay(nhanvien.DSVaoRa, nhanvien.NgayCongBD_Bef2D, nhanvien.NgayCongKT_Aft2D, nhanvien.DSVang, nhanvien.DSNgayCong, nhanvien.MacDinhTinhPC50);
				XL.TinhLaiPhuCapTC(nhanvien.DSXNPhuCap50, nhanvien.DSNgayCong);
				XL.TinhLaiPhuCapDB(nhanvien.DSXNPhuCapDB, nhanvien.DSNgayCong);

			}
			if (DS_Check_KoHopLe.Count > 0) DAL.LoaiGioLienQuan(DS_Check_KoHopLe);
			if (ds_raa3_vao1.Count > 0) DAL.ThemGio_ra3_vao1(ds_raa3_vao1);

			loadGrid();
			if (flagThongBao)
			{
				AutoClosingMessageBox.Show("Không thể sửa các giờ đã xác nhận.", "Thông báo", 2000);
			}

		}

		private void btnXoaGioVao_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			IsReload = true;
			var lydo = (cbLyDo.SelectedItem != null) ? cbLyDo.SelectedItem.ToString() : cbLyDo.SelectedText;
			var ghichu = tbGhiChu.Text;

			if (dgrdDSGioVaoRa.SelectedRows.Count == 0) return;

			#region hỏi lại trước khi xoá

			if (MessageBox.Show(string.Format(Resources.xacNhanXoaGioHangLoat, "vào"), Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}

			#endregion

			var flagThongBao = false;// thông báo ko được xoá giờ đãn XN
			IEnumerable<DataGridViewRow> dataGridViewRows = dgrdDSGioVaoRa.SelectedRows.Cast<DataGridViewRow>();
			var arrRecord = (from row in dataGridViewRows
							 let nv = (cUserInfo)(((DataRowView)row.DataBoundItem).Row["cUserInfo"])
							 let cChkInOut = (cChkInOut)(((DataRowView)row.DataBoundItem).Row["cChkInOut"])
							 let ngay = (DateTime)(((DataRowView)row.DataBoundItem).Row["TimeStrNgay"])
							 select new { nhanvien = nv, CIO = cChkInOut, Ngay = ngay }).GroupBy(o => o.nhanvien).ToList();
			var ds_raa3_vao1 = new List<cChk>();
			var DS_Check_KoHopLe = new List<cChk>();
			foreach (var groupByNV in arrRecord) {
				var nhanvien = groupByNV.Key;
				foreach (var row in groupByNV) {
					if (row.CIO.GetType() == typeof(cChkInOut_V))
					{
						flagThongBao = true;
						continue;
					}
					if (row.CIO.HaveINOUT == -2) continue;
					var checkinn = row.CIO.Vao;
					XL.XoaGioChoNV(checkinn, row.nhanvien, XL2.currUserID, lydo, ghichu);
				}
				XL.LoaiBoCheckCungLoaiTrong30phut(nhanvien.DS_Check_A, nhanvien.DS_Check_KoHopLe);
				XL.GhepCIO_A(nhanvien.DS_Check_A, nhanvien.DS_CIO_A);
				XL.LoaiBoCIOKoHopLe(nhanvien.DS_CIO_A, nhanvien.DS_Check_A, nhanvien.DS_Check_KoHopLe);
				if (nhanvien.DS_Check_KoHopLe.Count > 0) {
					DS_Check_KoHopLe.AddRange(nhanvien.DS_Check_KoHopLe);
					nhanvien.DS_Check_KoHopLe.Clear();
				}

				XL.XetCa_CIO_A(nhanvien.DS_CIO_A, nhanvien.LichTrinhLV.DSCa, ds_raa3_vao1, nhanvien.DS_Check_A);
				XL.TronDS_CIO_A_V(nhanvien.DS_CIO_A, nhanvien.DS_CIO_V, nhanvien.DSVaoRa);
				XL.TinhCongTheoNgay(nhanvien.DSVaoRa, nhanvien.NgayCongBD_Bef2D, nhanvien.NgayCongKT_Aft2D, nhanvien.DSVang, nhanvien.DSNgayCong, nhanvien.MacDinhTinhPC50);
				XL.TinhLaiPhuCapTC(nhanvien.DSXNPhuCap50, nhanvien.DSNgayCong);
				XL.TinhLaiPhuCapDB(nhanvien.DSXNPhuCapDB, nhanvien.DSNgayCong);

			}
			if (DS_Check_KoHopLe.Count > 0) DAL.LoaiGioLienQuan(DS_Check_KoHopLe);
			if (ds_raa3_vao1.Count > 0) DAL.ThemGio_ra3_vao1(ds_raa3_vao1);

			loadGrid();

			if (flagThongBao) {
				AutoClosingMessageBox.Show("Không thể xoá các giờ đã xác nhận.", "Thông báo", 2000);
			}
		}

		private void btnXoaGioRaa_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			IsReload = true;
			var lydo = cbLyDo.SelectedText;
			var ghichu = tbGhiChu.Text;

			#region hỏi lại ttrước khi xoá

			if (MessageBox.Show(string.Format(Resources.xacNhanXoaGioHangLoat, "ra"), Resources.capXacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}

			#endregion


			if (dgrdDSGioVaoRa.SelectedRows.Count == 0) return;
			var flagThongBao = false;// thông báo ko được xoá giờ đãn XN

			IEnumerable<DataGridViewRow> dataGridViewRows = dgrdDSGioVaoRa.SelectedRows.Cast<DataGridViewRow>();
			var arrRecord = (from row in dataGridViewRows
							 let nv = (cUserInfo)(((DataRowView)row.DataBoundItem).Row["cUserInfo"])
							 let cChkInOut = (cChkInOut)(((DataRowView)row.DataBoundItem).Row["cChkInOut"])
							 let ngay = (DateTime)(((DataRowView)row.DataBoundItem).Row["TimeStrNgay"])
							 select new { nhanvien = nv, CIO = cChkInOut, Ngay = ngay }).GroupBy(o => o.nhanvien).ToList();

			var ds_raa3_vao1 = new List<cChk>();
			var DS_Check_KoHopLe = new List<cChk>();
			foreach (var groupByNV in arrRecord) {
				var nhanvien = groupByNV.Key;
				foreach (var row in groupByNV) {
					if (row.CIO.GetType() == typeof(cChkInOut_V)) {
						flagThongBao = true;
						continue;
					}

					if (row.CIO.HaveINOUT == -1) continue;
					var checkout = row.CIO.Raa;
					XL.XoaGioChoNV(checkout, row.nhanvien, XL2.currUserID, lydo, ghichu);
				}
				XL.LoaiBoCheckCungLoaiTrong30phut(nhanvien.DS_Check_A, nhanvien.DS_Check_KoHopLe);
				XL.GhepCIO_A(nhanvien.DS_Check_A, nhanvien.DS_CIO_A);
				XL.LoaiBoCIOKoHopLe(nhanvien.DS_CIO_A, nhanvien.DS_Check_A, nhanvien.DS_Check_KoHopLe);
				if (nhanvien.DS_Check_KoHopLe.Count > 0) {
					DS_Check_KoHopLe.AddRange(nhanvien.DS_Check_KoHopLe);
					nhanvien.DS_Check_KoHopLe.Clear();
				}

				XL.XetCa_CIO_A(nhanvien.DS_CIO_A, nhanvien.LichTrinhLV.DSCa, ds_raa3_vao1, nhanvien.DS_Check_A);
				XL.TronDS_CIO_A_V(nhanvien.DS_CIO_A, nhanvien.DS_CIO_V, nhanvien.DSVaoRa);
				XL.TinhCongTheoNgay(nhanvien.DSVaoRa, nhanvien.NgayCongBD_Bef2D, nhanvien.NgayCongKT_Aft2D, nhanvien.DSVang, nhanvien.DSNgayCong, nhanvien.MacDinhTinhPC50);
				XL.TinhLaiPhuCapTC(nhanvien.DSXNPhuCap50, nhanvien.DSNgayCong);
				XL.TinhLaiPhuCapDB(nhanvien.DSXNPhuCapDB, nhanvien.DSNgayCong);

			}
			if (DS_Check_KoHopLe.Count > 0) DAL.LoaiGioLienQuan(DS_Check_KoHopLe);
			if (ds_raa3_vao1.Count > 0) DAL.ThemGio_ra3_vao1(ds_raa3_vao1);

			loadGrid();


			if (flagThongBao) {
				AutoClosingMessageBox.Show("Không thể xoá các giờ đã xác nhận.", "Thông báo", 2000);
			}


		}

		private void VeLaiCacGioCoThayDoi() {
			foreach (DataGridViewRow dataGridViewRow in dgrdDSGioVaoRa.Rows) {
				var row = (dataGridViewRow.DataBoundItem as DataRowView).Row;
				if (row["IsEdited"] != DBNull.Value && (bool)row["IsEdited"]) {
					dataGridViewRow.DefaultCellStyle.BackColor = Color.LightGreen;
				}
			}
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}


	}
}
