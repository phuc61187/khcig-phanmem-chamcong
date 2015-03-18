using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;
using System.Linq;
using System.Linq.Expressions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using log4net;

namespace ChamCong_v03 {
	public partial class frm_12_DiemDanhNV : Form {
		public readonly ILog lg = LogManager.GetLogger("frm_12_DiemDanhNV");
		public List<cUserInfo> m_DSNV = new List<cUserInfo>(); // danh sách các nhân viên thuộc phòng đang chọn
		public List<int> m_listIDPhongBan;
		public DataTable m_CTDiemDanh; //[20140511_1]
		private DataTable TaoCauTrucTableDiemDanh() {//[20140511_1]
			DataTable kq = XL.TaoCauTrucDataTable(
					new[] { "UserEnrollNumber", "UserFullCode", "UserFullName", "TimeStrVao1", "TimeStrRaa1", "TimeStrVao2", "TimeStrRaa2", "TimeStrVao3", "TimeStrRaa3", "ShiftID1", "ShiftID2", "ShiftID3", "Ca", "TrangThai" },
					new[] { typeof(int), typeof(string), typeof(string), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(DateTime), typeof(int), typeof(int), typeof(int), typeof(string), typeof(string), });
			return kq;
		}

		// hàm xử lý -----------------------------------------------------------------------------
		#region load treeview new
		public TreeView loadTreePhgBan(TreeView tvDSPhongBan, DataTable pDataTable) {
			if (pDataTable.Rows.Count > 0) {
				foreach (DataRow dataRow in pDataTable.Select("RelationID = 0", "ViTri asc")) {
					TreeNode ParentNode = new TreeNode { Text = dataRow["Description"].ToString(), Tag = (int)dataRow["ID"] };
					tvDSPhongBan.Nodes.Add(ParentNode);
					loadTreeSubNode(ref ParentNode, (int)(dataRow["ID"]), pDataTable);
				}
			}
			return tvDSPhongBan;
		}

		private void loadTreeSubNode(ref TreeNode ParentNode, int ParentId, DataTable dtMenu) {
			DataRow[] childs = dtMenu.Select("RelationID = " + ParentId, "ViTri asc");
			foreach (DataRow dRow in childs) {
				TreeNode child = new TreeNode {
					Text = dRow["Description"].ToString(), Tag = (int)dRow["ID"], ToolTipText = dRow["Description"].ToString()
				};
				ParentNode.Nodes.Add(child);
				//Recursion Call
				loadTreeSubNode(ref child, (int)dRow["ID"], dtMenu);
			}
		}

		void GetIDLeafNodeExceptParent(TreeNode root, List<int> listID) {
			if (root == null) return;

			listID.Add((int)root.Tag);

			if (root.Nodes.Count > 0) {
				foreach (TreeNode node in root.Nodes) {
					GetIDLeafNodeExceptParent(node, listID);
				}
			}
			// xuốn đến đây tương đương root.Nodes.Count== 0; return
		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {

			m_listIDPhongBan.Clear();
			if (e.Node.FirstNode != null) GetIDLeafNodeExceptParent(e.Node, m_listIDPhongBan);
			else m_listIDPhongBan.Add((int)e.Node.Tag);
			e.Node.Expand();

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 4000);
				this.Close();
				return;
			}

			#region lấy ngày BD và kết thúc, và update lại Ngày BD = 1 ngày trước 31/08 12:00 AM, ngày KT là 1 ngày sau ngay 1 23:59:59
			dtpNgay.Update();
			DateTime ngayBD = dtpNgay.Value.Date;
			ngayBD = ngayBD.AddDays(-2d);
			DateTime ngayKT = ngayBD.AddDays(4d);
			#endregion

			// lấy dsnv tất cả nv thuộc các phòng ban được chọn
			DataTable table = DAL.LayDSNV(m_listIDPhongBan.ToArray());
			if (table.Rows.Count == 0) return;
			m_DSNV.Clear();
			XL.KhoiTaoDSNV(m_DSNV, table);

			//3. lấy dữ liệu chấm công của các nhân viên
			//[CHÚ Ý] ngày bắt đầu và kết thúc đã cộng trừ thêm 1 ngày trước sau ở bở trên

			int SoNVDangLamViec = 0, SoNVDaRaVe = 0, SoNVVang = 0;
			try {
				//dsnv = XL.XemCong(table, ArrDSMaCC_Checked, ngayBD, ngayKT);
				XL.DiemDanh(m_DSNV, ngayBD, ngayKT);
				m_CTDiemDanh.Rows.Clear();
				XL.TaoTableDiemDanh(m_DSNV, m_CTDiemDanh, out SoNVDangLamViec, out SoNVDaRaVe, out SoNVVang);
			} catch (Exception exception) {
				lg.Error("form diem danh", exception);
				MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại sau.", "Lỗi");
				GC.Collect();
				return;
			}

			//4. xử lý dữ liệu để đưa lên lưới tổng hợp

			tbTongSoNV.Text = m_DSNV.Count.ToString();
			tbSoNVDangLamViec.Text = SoNVDangLamViec.ToString();
			tbSoNVDaRaVe.Text = SoNVDaRaVe.ToString();
			tbSoNVVang.Text = SoNVVang.ToString();
			dgrdTongHop.DataSource = m_CTDiemDanh;

		}

		#endregion

		public frm_12_DiemDanhNV() {
			m_listIDPhongBan = new List<int>();
			m_DSNV = new List<cUserInfo>();
			m_CTDiemDanh = TaoCauTrucTableDiemDanh();

			InitializeComponent();
			log4net.Config.XmlConfigurator.Configure();
			dgrdTongHop.AutoGenerateColumns = false;
			DateTime today = DateTime.Today;
			dtpNgay.Value = new DateTime(today.Year, today.Month, today.Day);
		}




		private void frm_DiemDanhNV_Load(object sender, EventArgs e) {
			DataTable tablePhong = DAL.LayDSPhong(XL2.currUserID);
			if (tablePhong.Rows.Count == 0) {
				AutoClosingMessageBox.Show("Bạn chưa được phân quyền thao tác.", "Thông báo", 2000);
				return;
			}
			//2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan : xoá dữ liệu trước và load
			treePhongBan.Nodes.Clear();
			loadTreePhgBan(treePhongBan, tablePhong);

			if (XL.KiemtraDulieuCapnhatTuServer(DateTime.Now) == false) {
				AutoClosingMessageBox.Show("Dữ liệu chấm công chưa được cập nhật mới nhất từ các máy chấm công.\nCác thay đổi giờ chấm công có thể làm sai sót giờ chấm công thực tế khi dữ liệu được cập nhật.", "Thông báo", 4000);
			}

			// đăng ký sự kiện cho tree và chọn topNode
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;
			treePhongBan.SelectedNode = treePhongBan.TopNode;
			dtpNgay.ValueChanged += dtpNgay_ValueChanged;
		}

		private void dtpNgay_ValueChanged(object sender, EventArgs e) {
			treePhongBan_AfterSelect(treePhongBan, new TreeViewEventArgs(treePhongBan.SelectedNode, TreeViewAction.ByMouse));
		}

		private void btnXuatBB_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			//1. lấy dữ liệu từ form
			#region lấy ngày BD và kết thúc, và update lại Ngày BD = 1 ngày trước 31/08 12:00 AM, ngày KT là 1 ngày sau ngay 1 23:59:59
			dtpNgay.Update();
			DateTime ngayBD = dtpNgay.Value.Date;
			ngayBD = ngayBD.AddDays(-2d);
			DateTime ngayKT = ngayBD.AddDays(4d);
			#endregion

			// lấy dsnv tất cả nv thuộc các phòng ban được chọn
			DataTable table = DAL.LayDSNV(m_listIDPhongBan.ToArray());
			if (table.Rows.Count == 0) return;
			m_DSNV.Clear();
			XL.KhoiTaoDSNV(m_DSNV, table);

			//3. lấy dữ liệu chấm công của các nhân viên
			//[CHÚ Ý] ngày bắt đầu và kết thúc đã cộng trừ thêm 1 ngày trước sau ở bở trên
			if (XL.KiemtraDulieuCapnhatTuServer(DateTime.Now) == false) {
				AutoClosingMessageBox.Show("Dữ liệu chấm công chưa được cập nhật mới nhất từ các máy chấm công.\nCác thay đổi giờ chấm công có thể làm sai sót giờ chấm công thực tế khi dữ liệu được cập nhật.", "Thông báo", 4000);
			}

			int SoNVDangLamViec = 0, SoNVDaRaVe = 0, SoNVVang = 0;
			//dsnv = XL.XemCong(table, ArrDSMaCC_Checked, ngayBD, ngayKT);
			XL.DiemDanh(m_DSNV, ngayBD, ngayKT);
			m_CTDiemDanh.Rows.Clear();
			XL.TaoTableDiemDanh(m_DSNV, m_CTDiemDanh, out SoNVDangLamViec, out SoNVDaRaVe, out SoNVVang);

			saveFileDlgDiemDanh.Filter = "Excel File|*.xlsx";
			saveFileDlgDiemDanh.ShowDialog();
			if (saveFileDlgDiemDanh.FileName == String.Empty) {
				return;
			}
			var saveFileName = saveFileDlgDiemDanh.FileName;

			using (var p = new ExcelPackage()) {


				#region Ghi file , nếu xảy ra lỗi thì báo
				XuatBBDSNVDLV(p);
				XuatBBDSNVVang(p);
				Byte[] bin = p.GetAsByteArray();
				try {
					File.WriteAllBytes(saveFileName, bin);//(file_path, bin);
					AutoClosingMessageBox.Show("Xuất báo biểu thành công.", "Thông báo", 2000);
				} catch (Exception exception) {
					lg.Error("XuatBBDiemDanh", exception);
					if (exception is UnauthorizedAccessException) MessageBox.Show("Bạn chưa được cấp quyền ghi file vào folder.", "Lỗi");
					else if (exception is DirectoryNotFoundException) MessageBox.Show("Không tìm thấy folder lưu trữ.", "Lỗi");
					else if (exception is IOException) MessageBox.Show("File đang mở bởi ứng dụng khác.", "Lỗi");
					else MessageBox.Show("Không thể ghi được file báo biểu.", "Lỗi");
				}

				#endregion

			}


		}

		void XuatBBDSNVDLV(ExcelPackage p) {
			#region //Create a sheet
			p.Workbook.Worksheets.Add("DSNV Dang Lam Viec");
			var ws = p.Workbook.Worksheets[1];
			ws.Name = "DSNV Dang Lam Viec"; //Setting Sheet's name

			ws.Cells.Style.Font.Size = 10; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

			#endregion
			var sR = 1;
			XL2.VeLogo("bbdiemdanhdlv", ws);
			XL2.GhiThongTinTongcty(ws, "", 1, 4, 12, 1, 9, 12, 4, 9);
			int sttnv = 0, startColws = 1;
			#region 1.tieu de bang cham cong tu ngay den ngay 2. bỏ 1 dòng trống

			sR = sR + 7;// sR+1 là dòng trống
			XL.FormatCells(ws.Cells[sR, 6], "Danh sách nhân viên đang làm việc ngày " + dtpNgay.Value.ToString("dd/MM/yyyy"),
				size: 14, hAlign: ExcelHorizontalAlignment.CenterContinuous, bold: true, VeBorder: false);
			#endregion

			sR++;
			sR++;
			#region  write stt +0
			ws.Column(startColws).Width = (int)Z.STT;
			XL.FormatCells(ws.Cells[sR, startColws], "STT", bold: true);
			startColws++;
			#endregion
			#region  write ho ten +1
			ws.Column(startColws).Width = (int)Z.TEN;
			XL.FormatCells(ws.Cells[sR, startColws], "Họ tên", bold: true);
			startColws++;
			#endregion
			#region write manv +2
			ws.Column(startColws).Width = (int)Z.MANV;
			XL.FormatCells(ws.Cells[sR, startColws], "Mã NV", bold: true);
			startColws++;
			#endregion
			#region write trạng thái +3
			ws.Column(startColws).Width = (int)Z.TRANGTHAI;
			XL.FormatCells(ws.Cells[sR, startColws], "Trạng thái", bold: true);
			startColws++;
			#endregion
			#region write ca +4
			ws.Column(startColws).Width = (int)Z.CA;
			XL.FormatCells(ws.Cells[sR, startColws], "Ca", bold: true);
			startColws++;
			#endregion

			#region write vào 1 +5
			ws.Column(startColws).Width = (int)Z.VAO1;
			XL.FormatCells(ws.Cells[sR, startColws], "Vào 1", bold: true);
			ws.Cells[sR, startColws].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
			startColws++;
			#endregion
			#region write raa 1 +6
			ws.Column(startColws).Width = (int)Z.RAA1;
			XL.FormatCells(ws.Cells[sR, startColws], "Ra 1", bold: true);
			ws.Cells[sR, startColws].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
			startColws++;
			#endregion
			#region write vào 2 +7
			ws.Column(startColws).Width = (int)Z.VAO2;
			XL.FormatCells(ws.Cells[sR, startColws], "Vào 2", bold: true);
			ws.Cells[sR, startColws].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
			startColws++;
			#endregion
			#region write raa 2 +8
			ws.Column(startColws).Width = (int)Z.RAA2;
			XL.FormatCells(ws.Cells[sR, startColws], "Ra 2", bold: true);
			ws.Cells[sR, startColws].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
			startColws++;
			#endregion
			#region write vào 3 +9
			ws.Column(startColws).Width = (int)Z.VAO3;
			XL.FormatCells(ws.Cells[sR, startColws], "Vào 3", bold: true);
			ws.Cells[sR, startColws].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
			startColws++;
			#endregion
			#region write raa 3 +10
			ws.Column(startColws).Width = (int)Z.RAA3;
			XL.FormatCells(ws.Cells[sR, startColws], "Ra 3", bold: true);
			ws.Cells[sR, startColws].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
			startColws++;
			#endregion

			sR++;
			var pb_c2 = (from item in m_DSNV
						 select new { item.PBCap2.ID, item.PBCap2.TenPhongBan }).Distinct().ToList();
			try {
				foreach (var pb_i in pb_c2) {
					#region //write ten phong ban, tang 1 dong
					XL.FormatCells(ws.Cells[sR, 1], pb_i.TenPhongBan, bold: true, VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
					#endregion
					sR++;//chỉ số dòng mới, dòn 
					var pb_c1 = (from item in m_DSNV
								 where item.PBCap2.ID == pb_i.ID
								 select new { item.PBCap1.ID, item.PBCap1.TenPhongBan }).Distinct().ToList();

					foreach (var pb in pb_c1) {
						#region //write ten pb cap 1 tang 1 dong
						XL.FormatCells(ws.Cells[sR, 1], pb.TenPhongBan, bold: true, VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
						sR++;
						#endregion
						var dsnv = (from c in m_DSNV
									where c.PBCap1.ID == pb.ID
									select c).ToList();
						foreach (var nhanvien in dsnv) {
							// duyet tung nv, nhớ tăng sR
							startColws = 1;//reset startcol =1 mỗi lần ghi nv mới

							var ngayCong = nhanvien.DSNgayCong[2];
							if (ngayCong.DSVaoRa.Count == 0 || ngayCong.DSVaoRa[ngayCong.DSVaoRa.Count - 1].Raa != null) {
								continue;
							}

							#region  stt,hoten,macc
							sttnv++;
							XL.FormatCells(ws.Cells[sR, startColws + 0], sttnv);
							XL.FormatCells(ws.Cells[sR, startColws + 1], nhanvien.TenNV, hAlign: ExcelHorizontalAlignment.Left); //Ten
							XL.FormatCells(ws.Cells[sR, startColws + 2], nhanvien.MaNV, hAlign: ExcelHorizontalAlignment.Left);//MaNV
							startColws = startColws + 3; // + 4 là 4 cột, nếu thêm 1 cột thì +5, +6
							#endregion

							var xCol = startColws;

							#region trạng thái
							XL.FormatCells(ws.Cells[sR, xCol], "Đang làm việc");
							xCol++;

							#endregion

							#region ca
							XL.FormatCells(ws.Cells[sR, xCol], ngayCong.CIOs_Absents_Code_Comp());//
							xCol++;

							#endregion

							#region vào ra 1-->3

							for (int x = 0; x < ngayCong.DSVaoRa.Count || x < 3; x++) {
								if (x < 3 && x >= ngayCong.DSVaoRa.Count) {
									XL.FormatCells(ws.Cells[sR, xCol], null);
									xCol++;
									XL.FormatCells(ws.Cells[sR, xCol], null);
									xCol++;
								}
								else
								{
									XL.FormatCells(ws.Cells[sR, xCol], ngayCong.DSVaoRa[x].Vao != null ? ngayCong.DSVaoRa[x].Vao.Time : (object)null, numberFormat: "H:mm d/M");
									xCol++;
									XL.FormatCells(ws.Cells[sR, xCol], ngayCong.DSVaoRa[x].Raa != null ? ngayCong.DSVaoRa[x].Raa.Time : (object)null, numberFormat: "H:mm d/M");
									xCol++;
								}
							}


							#endregion

							sR++;
						}
					}
				}
				sR += 2;
			} catch (Exception exception) {
				throw exception;
			}
		}

		void XuatBBDSNVVang(ExcelPackage p) {
			#region //Create a sheet
			p.Workbook.Worksheets.Add("DSNV Vang");
			var ws = p.Workbook.Worksheets[2];
			ws.Name = "DSNV Vang"; //Setting Sheet's name

			ws.Cells.Style.Font.Size = 10; //Default font size for whole sheet
			ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

			#endregion
			var sR = 1;
			XL2.VeLogo("bbdiemdanhvang", ws);
			XL2.GhiThongTinTongcty(ws, "", 1, 4, 12, 1, 9, 12, 4, 9);
			int sttnv = 0, startColws = 1;
			#region 1.tieu de bang cham cong tu ngay den ngay 2. bỏ 1 dòng trống

			sR = sR + 7;// sR+1 là dòng trống
			XL.FormatCells(ws.Cells[sR, 6], "Danh sách nhân viên vắng ngày " + dtpNgay.Value.ToString("dd/MM/yyyy"),
				size: 14, hAlign: ExcelHorizontalAlignment.CenterContinuous, bold: true, VeBorder: false);
			#endregion

			sR++;
			sR++;
			#region  write stt +0
			ws.Column(startColws).Width = (int)Z.STT;
			XL.FormatCells(ws.Cells[sR, startColws], "STT", bold: true);
			startColws++;
			#endregion
			#region  write ho ten +1
			ws.Column(startColws).Width = (int)Z.TEN;
			XL.FormatCells(ws.Cells[sR, startColws], "Họ tên", bold: true);
			startColws++;
			#endregion
			#region write manv +2
			ws.Column(startColws).Width = (int)Z.MANV;
			XL.FormatCells(ws.Cells[sR, startColws], "Mã NV", bold: true);
			startColws++;
			#endregion
			#region write trạng thái +3
			ws.Column(startColws).Width = (int)Z.TRANGTHAI;
			XL.FormatCells(ws.Cells[sR, startColws], "Trạng thái", bold: true);
			startColws++;
			#endregion
			#region write ca +4
			ws.Column(startColws).Width = (int)Z.CA;
			XL.FormatCells(ws.Cells[sR, startColws], "Ca", bold: true);
			startColws++;
			#endregion

			//startCol = 4

			sR++;
			var pb_c2 = (from item in m_DSNV
						 select new { item.PBCap2.ID, item.PBCap2.TenPhongBan }).Distinct().ToList();
			try {
				foreach (var pb_i in pb_c2) {
					#region //write ten phong ban, tang 1 dong
					XL.FormatCells(ws.Cells[sR, 1], pb_i.TenPhongBan, bold: true, VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
					#endregion
					sR++;//chỉ số dòng mới, dòn 
					var pb_c1 = (from item in m_DSNV
								 where item.PBCap2.ID == pb_i.ID
								 select new { item.PBCap1.ID, item.PBCap1.TenPhongBan }).Distinct().ToList();

					foreach (var pb in pb_c1) {
						#region //write ten pb cap 1 tang 1 dong
						XL.FormatCells(ws.Cells[sR, 1], pb.TenPhongBan, bold: true, VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
						sR++;
						#endregion
						var dsnv = (from c in m_DSNV
									where c.PBCap1.ID == pb.ID
									select c).ToList();
						foreach (var nhanvien in dsnv) {
							// duyet tung nv, nhớ tăng sR
							startColws = 1;//reset startcol =1 mỗi lần ghi nv mới

							var ngayCong = nhanvien.DSNgayCong[2];
							if (ngayCong.DSVaoRa.Count > 0) {
								continue;
							}

							#region  stt,hoten,macc
							sttnv++;
							XL.FormatCells(ws.Cells[sR, startColws + 0], sttnv);
							XL.FormatCells(ws.Cells[sR, startColws + 1], nhanvien.TenNV, hAlign: ExcelHorizontalAlignment.Left); //Ten
							XL.FormatCells(ws.Cells[sR, startColws + 2], nhanvien.MaNV, hAlign: ExcelHorizontalAlignment.Left);//MaNV
							startColws = startColws + 3; // + 4 là 4 cột, nếu thêm 1 cột thì +5, +6
							#endregion

							var xCol = startColws;

							#region trạng thái
							XL.FormatCells(ws.Cells[sR, xCol], "Vắng");
							xCol++;

							#endregion

							#region ca
							XL.FormatCells(ws.Cells[sR, xCol], ngayCong.Absents_Code());//
							xCol++;

							#endregion

							sR++;
						}
					}
				}

				sR += 2;


			} catch (Exception exception) {
				throw exception;
			}
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}
