using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CapNhatGioChamCong.BUS;
using CapNhatGioChamCong.DTO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CapNhatGioChamCong {
	public partial class frm_XemCong : Form {
		#region local variable

		public List<int> flstIDPhongBan { get; set; }
		public List<int> flstMaCC { get; set; }
		public DataTable fTablePhongBan { get; set; }
		public DataTable fTableDSNV { get; set; }
		public DataTable fTableCheckIO { get; set; }
		public List<cUserInfo> flstDSNV { get; set; }
		public List<cUserInfo> flstDSNVChkXemCong { get; set; }
		#region 3 biến checkbox check all: 1 của DSNV cần xem công. 2 DS Công của NV
		readonly CheckBox checkAll_GridDSNV = new CheckBox();
		readonly CheckBox checkAllGridTH = new CheckBox();
		readonly CheckBox checkAllGridKDQD = new CheckBox();
		#endregion

		#endregion

		// hàm xử lý -----------------------------------------------------------------------------
		#region load treeview
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

		void GetNodeID(TreeNode root) {
			if (root == null) return;
			if (root.FirstNode == null) {
				flstIDPhongBan.Add((int)root.Tag);
				root = root.NextNode;
				return;
			}
			if (root.FirstNode != null) {
				foreach (TreeNode treeNode in root.Nodes)
					GetNodeID(treeNode);
			}
		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			if (flstIDPhongBan == null) flstIDPhongBan = new List<int>();
			else 	flstIDPhongBan.Clear();
			if (e.Node.FirstNode != null) GetNodeID(e.Node);
			else flstIDPhongBan.Add((int)e.Node.Tag);
			e.Node.Expand();

			string temp = "UserIDD = {0}";
			temp = String.Format(temp, String.Join(" or UserIDD = ", flstIDPhongBan.ToArray()));

			DataTable table;
			if (dgrdDSNVTrgPhg.DataSource == null)
				table = fTableDSNV.Clone();
			else {
				table = dgrdDSNVTrgPhg.DataSource as DataTable;
				table.Rows.Clear();
			}

			foreach (DataRow row in fTableDSNV.Select(temp, "UserIDD asc, UserEnrollNumber asc", DataViewRowState.CurrentRows))
				table.ImportRow(row);

			dgrdDSNVTrgPhg.DataSource = table;
			checkAll_GridDSNV.Checked = false;
		}

		#endregion
		#region tạo cấu trúc 2 table hiển thị, và 1 cấu trúc table Chi tiết để hiển thị bên form thêm xóa sửa
		private DataTable TaoCauTrucDataTableTongHop() {
			DataTable kq = new DataTable();
			kq.Columns.Add("check", typeof(bool)); //0
			kq.Columns.Add("UserEnrollNumber", typeof(int)); //0
			kq.Columns.Add("UserFullName", typeof(string)); //1
			kq.Columns.Add("TimeStrNgay", typeof(DateTime)); //2
			kq.Columns.Add("TimeStrThu", typeof(DateTime)); //3
			kq.Columns.Add("TimeStrVao1", typeof(DateTime)); //4
			kq.Columns.Add("TimeStrRa1", typeof(DateTime)); //5
			kq.Columns.Add("TimeStrVao2", typeof(DateTime)); //6
			kq.Columns.Add("TimeStrRa2", typeof(DateTime)); //7
			kq.Columns.Add("TimeStrVao3", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRa3", typeof(DateTime)); //9
			kq.Columns.Add("TimeStrVao4", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRa4", typeof(DateTime)); //9
			kq.Columns.Add("TimeStrVao5", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRa5", typeof(DateTime)); //9
			kq.Columns.Add("ShiftID1", typeof(int)); //8
			kq.Columns.Add("ShiftID2", typeof(int)); //9
			kq.Columns.Add("ShiftID3", typeof(int)); //9
			kq.Columns.Add("ShiftID4", typeof(int)); //9
			kq.Columns.Add("ShiftID5", typeof(int)); //9
			kq.Columns.Add("TimeStrTre", typeof(int)); //18
			kq.Columns.Add("TimeStrSom", typeof(int)); //19
			kq.Columns.Add("Cong", typeof(Double)); //20
			kq.Columns.Add("ShiftCode", typeof(string)); //21
			kq.Columns.Add("PhuCap", typeof(Double));//22
			kq.Columns.Add("TongGioLam", typeof(Double));
			kq.Columns.Add("TongGioThuc", typeof(Double));

			return kq;
		}

		private DataTable TaoCauTrucDataTableKDQD() {
			DataTable kq = new DataTable();
			kq.Columns.Add("check", typeof(bool)); //0
			kq.Columns.Add("UserEnrollNumber", typeof(int)); //0
			kq.Columns.Add("UserFullName", typeof(string)); //1
			kq.Columns.Add("TimeStrNgay", typeof(DateTime)); //2
			kq.Columns.Add("TimeStrThu", typeof(DateTime)); //3
			kq.Columns.Add("TimeStrVao1", typeof(DateTime)); //4
			kq.Columns.Add("TimeStrRa1", typeof(DateTime)); //5
			kq.Columns.Add("TimeStrVao2", typeof(DateTime)); //6
			kq.Columns.Add("TimeStrRa2", typeof(DateTime)); //7
			kq.Columns.Add("TimeStrVao3", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRa3", typeof(DateTime)); //9
			kq.Columns.Add("TimeStrVao4", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRa4", typeof(DateTime)); //9
			kq.Columns.Add("TimeStrVao5", typeof(DateTime)); //8
			kq.Columns.Add("TimeStrRa5", typeof(DateTime)); //9
			kq.Columns.Add("ShiftID1", typeof(int)); //8
			kq.Columns.Add("ShiftID2", typeof(int)); //9
			kq.Columns.Add("ShiftID3", typeof(int)); //9
			kq.Columns.Add("ShiftID4", typeof(int)); //9
			kq.Columns.Add("ShiftID5", typeof(int)); //9
			kq.Columns.Add("TimeStrTre", typeof(int)); //18
			kq.Columns.Add("TimeStrSom", typeof(int)); //19
			kq.Columns.Add("Cong", typeof(Double)); //20
			kq.Columns.Add("ShiftCode", typeof(string)); //21
			kq.Columns.Add("PhuCap", typeof(Double));//22
			kq.Columns.Add("TongGioLam", typeof(Double));
			kq.Columns.Add("TongGioThuc", typeof(Double));

			return kq;
		}

		#endregion

		#region vẽ checkBox check all và xử lý sự kiện check

		void checkAll_CheckedChanged(object sender, EventArgs e) {
			//1. xác định datagrid nào đang check all
			DataGridView tempGrid;
			if (sender == checkAll_GridDSNV) tempGrid = dgrdDSNVTrgPhg;
			else if (sender == checkAllGridTH) tempGrid = dgrdTongHop;
			else tempGrid = dgrdGioKDQD;
			
			// 2. kiểm tra nếu ko có dòng dữ liệu nào thì thoát
			if (tempGrid.Rows.Count == 0) return;

			//3. có dữ liệu, duyệt qua hết và gán giá trị check (or uncheck), sau đó endEDIT lại lưới để dòng đầu tiên có giá trị
			bool tmpCheckAll = ((CheckBox)sender).Checked;

			tempGrid.BeginEdit(true);
			DataTable dt = tempGrid.DataSource as DataTable;

			if (dt != null && dt.Rows.Count != 0) {
				foreach (DataRow row in dt.Rows) {
					row["check"] = tmpCheckAll;
				}
			}

			tempGrid.EndEdit();
			tempGrid.Update();
			//tempGrid.Refresh();
		}
		#endregion


		public frm_XemCong() {
			InitializeComponent();
			//1. không cho autogen các column khi bind dữ liệu: 4 cái
			dgrdDSNVTrgPhg.AutoGenerateColumns = dgrdTongHop.AutoGenerateColumns = dgrdGioKDQD.AutoGenerateColumns = false;

			//2. đặt lại tên các cột của các datagrid theo format: grid<Số thứ tự><DataPropertyName>
			foreach (DataGridViewColumn column in dgrdTongHop.Columns)
				column.Name = ThamSo.prefixColNameGrid1 + column.DataPropertyName;

			foreach (DataGridViewColumn column in dgrdGioKDQD.Columns)
				column.Name = ThamSo.prefixColNameGrid2 + column.DataPropertyName;

			//3. vẽ 3 checkbox checkall cho DSNV trong phòng
			ThamSo.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));
			ThamSo.VeCheckBox_CheckAll(dgrdTongHop, checkAllGridTH, checkAll_CheckedChanged, new Point(7, 10));
			ThamSo.VeCheckBox_CheckAll(dgrdGioKDQD, checkAllGridKDQD, checkAll_CheckedChanged, new Point(7, 10));
		}

		private void frm_XemCong_Load(object sender, EventArgs e) {

			// 1. khởi tạo các biến cục bộ
			flstIDPhongBan = new List<int>();

			//2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan : xoá dữ liệu trước và load
			fTablePhongBan = ThamSo.TablePhongBan.Copy();
			treePhongBan.Nodes.Clear();
			loadTreePhgBan(treePhongBan, fTablePhongBan);

			// 

			// 3. Duyệt  dữ liệu toàn bộ danh sách nhân viên được phép thao tác(và thêm cột check) 			
			// và  khởi tạo các giá trị mặc định cho từng nhân viên  
			fTableDSNV = ThamSo.DataTableDSNV.Copy();
			fTableDSNV.Columns.Add("check", typeof(bool));
			flstDSNV = new List<cUserInfo>();
			KhoitaoDSNV(fTableDSNV, flstDSNV);

			// đăng ký sự kiện cho tree và chọn topNode
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;
			treePhongBan.SelectedNode = treePhongBan.TopNode;

		}

		private void KhoitaoDSNV(DataTable TableDSNV, List<cUserInfo> dsnv) {
			if (TableDSNV == null || TableDSNV.Rows.Count == 0) return;

			foreach (DataRow row in TableDSNV.Rows) {
				cShiftSchedule tmpLichTrinh = ThamSo.DSLichTrinh.Find(item => item.SchID == (int)row["SchID"]);
				List<cShift> tmpDSCa = tmpLichTrinh.ListT1;
				List<cShift> tmpDSCaMoRong = ThamSo.TaoDSCaMoRong(tmpDSCa); // đã bao gồm Khác(int.Minvalue)
				//List<cShift> tmpDSCaChonGio = new List<cShift>(ThamSo.DSCa);
				cUserInfo nhanvien = new cUserInfo() {
					UserEnrollNumber = (int)row["UserEnrollNumber"], UserFullName = row["UserFullName"].ToString(),
					LichTrinhLV = tmpLichTrinh, DSCa = tmpDSCa, DSCaMoRong = tmpDSCaMoRong,
					BoPhan = new cPhongBan() { ID = (int)row["UserIDD"], TenPhongBan = row["Description"].ToString() },
					MacDinhTinhPC150 = (bool)row["TinhPC150"]
				};
				nhanvien.HeSoLuongCB = (row["HeSoLuongCB"]!= DBNull.Value) ? (Single) row["HeSoLuongCB"]:0f;
				nhanvien.HeSoLuongSP = (row["HeSoLuongSP"] != DBNull.Value) ? (Single) row["HeSoLuongSP"]:0f;

				dsnv.Add(nhanvien);
			}
		}

		private void btnXem_Click(object sender, EventArgs e) {
			//1. lấy dữ liệu từ form
			#region lấy ngày BD và kết thúc, và update lại Ngày BD = 1 ngày trước 31/08 12:00 AM, ngày KT là 1 ngày sau ngay 1 23:59:59
			dtpNgayBD.Update(); dtpNgayKT.Update();
			DateTime ngayBD = dtpNgayBD.Value.Date;
			ngayBD = ngayBD.AddDays(-1d);
			DateTime ngayKT = dtpNgayKT.Value.Date;
			ngayKT = ngayKT.AddDays(2d).Subtract(new TimeSpan(0, 0, 1));
			#endregion
			//-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

			dgrdDSNVTrgPhg.EndEdit();
			dgrdDSNVTrgPhg.Update();
			//2. lấy danh sách nhân viên check
			if (flstDSNVChkXemCong == null) flstDSNVChkXemCong = new List<cUserInfo>();
			else flstDSNVChkXemCong.Clear();
			LayDSNVXemCong(dgrdDSNVTrgPhg.DataSource as DataTable, flstDSNVChkXemCong, flstDSNV);
			//3. lấy dữ liệu chấm công của các nhân viên
			if (flstDSNVChkXemCong.Count == 0) {
				AutoClosingMessageBox.Show("Chưa chọn Nhân viên", "Thông báo", 2000);
				return;
			}
			//[CHÚ Ý] ngày bắt đầu và kết thúc đã cộng trừ thêm 1 ngày trước sau ở bở trên
			try {
				WaitWindow.Show(this.abc, "Đang xử lý...", new object[] {flstDSNVChkXemCong, ngayBD, ngayKT});
				//XL.XemCong(flstDSNVChkXemCong, ngayBD, ngayKT);

			//4. xử lý dữ liệu để đưa lên lưới tổng hợp

			DataTable dataTableTongHop = dgrdTongHop.DataSource as DataTable;
			if (dataTableTongHop == null) dataTableTongHop = TaoCauTrucDataTableTongHop();
			else dataTableTongHop.Rows.Clear();

			DataTable dataTableKDQD = dgrdGioKDQD.DataSource as DataTable;
			if (dataTableKDQD == null) dataTableKDQD = TaoCauTrucDataTableKDQD();
			else dataTableKDQD.Rows.Clear();

			const string shiftid = "ShiftID";
			int i1 = 1;
			#region fill vào dataTable
			foreach (cUserInfo nhanvien in flstDSNVChkXemCong) {
				for (int i = 1; i < nhanvien.DSNgayCong.Count - 1; i++) { //[CHÚ Ý] ở đây chỉ chạy từ 1-> kế cuối
					//Debug.WriteLine(nhanvien.DSNgayCong[0]);
					//Debug.WriteLine(nhanvien.DSNgayCong[nhanvien.DSNgayCong.Count - 1]);
					cNgayCong ngayCong = nhanvien.DSNgayCong[i];
					//Debug.WriteLine(cChkInOut1.ToString());
					DataRow rowTH = dataTableTongHop.NewRow();
					rowTH["check"] = false;
					rowTH["UserEnrollNumber"] = nhanvien.UserEnrollNumber; rowTH["UserFullName"] = nhanvien.UserFullName;
					rowTH["TimeStrNgay"] = rowTH["TimeStrThu"] = ngayCong.NgayCong;
					rowTH["Cong"] = ngayCong.TongCong;
					rowTH["PhuCap"] = ngayCong.TongPhuCap;
					rowTH["TimeStrTre"] = Math.Floor(ngayCong.TongTre.TotalMinutes);
					rowTH["TimeStrSom"] = Math.Floor(ngayCong.TongSom.TotalMinutes);
					rowTH["TongGioLam"] = ngayCong.TongGioLam.TotalHours;
					rowTH["TongGioThuc"] = ngayCong.TongGioThuc.TotalHours;
					if (ngayCong.HasCheck) {
						i1 = 1;
						foreach (cChkInOut cChkInOut1 in ngayCong.DSVaoRa) {
							if (i1 > 3) break;
							rowTH[ThamSo.nameVao + i1] = cChkInOut1.Vao != null ? (object)cChkInOut1.Vao.TimeStr : DBNull.Value;
							rowTH[ThamSo.nameRa + i1] = cChkInOut1.Raa != null ? (object)cChkInOut1.Raa.TimeStr : DBNull.Value;
							rowTH[shiftid + i1] = (cChkInOut1.HaveINOUT > 0) ? (object)cChkInOut1.ThuocCa.ShiftID : DBNull.Value;
							rowTH["ShiftCode"] += (cChkInOut1.HaveINOUT > 0) ? cChkInOut1.ThuocCa.ShiftCode + "; " : ((cChkInOut1.HaveINOUT == -1) ? "KV; " : "KR; ");
							i1++;
						}
						rowTH["ShiftCode"] += ngayCong.XuatChuoiVang();
					}
					else {
						if (ngayCong.DSVang != null && ngayCong.DSVang.Count != 0) {
							rowTH["ShiftCode"] += ngayCong.XuatChuoiVang();
						}
						else rowTH["ShiftCode"] = "--";
					}
					dataTableTongHop.Rows.Add(rowTH);
				}
			}
			#endregion

			dgrdTongHop.DataSource = dataTableTongHop;

			foreach (DataRow row in dataTableTongHop.Select("(ShiftID1 is NULL and (TimeStrVao1 is not null or TimeStrRa1 is not null)) " +
															" or (ShiftID2 is NULL and (TimeStrVao2 is not null or TimeStrRa2 is not null))" +
															" or (ShiftID3 is NULL and (TimeStrVao3 is not null or TimeStrRa3 is not null))" +
															" OR (ShiftID1 is NOT NULL and ShiftID1 = " + int.MinValue + ")" +
															" OR (ShiftID2 is NOT NULL and ShiftID2 = " + int.MinValue + ")" +
															" OR (ShiftID3 is NOT NULL and ShiftID3 = " + int.MinValue + ")", string.Empty, DataViewRowState.CurrentRows).ToList()) {
				dataTableKDQD.ImportRow(row);
			}
			dgrdGioKDQD.DataSource = dataTableKDQD;
			checkAllGridTH.CheckedChanged -= checkAll_CheckedChanged;
			checkAllGridKDQD.CheckedChanged -= checkAll_CheckedChanged;
			checkAllGridTH.Checked = false;
			checkAllGridKDQD.Checked = false;
			checkAllGridTH.CheckedChanged += checkAll_CheckedChanged;
			checkAllGridKDQD.CheckedChanged += checkAll_CheckedChanged;
			GC.Collect();
			} catch {
				MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại sau.", "Lỗi");
				GC.Collect();
				return;
			}

		}

		private void abc(object sender, WaitWindowEventArgs e) {
			List<cUserInfo> lstDSNVChkXemCong = e.Arguments[0] as List<cUserInfo>;
			DateTime ngayBD = e.Arguments[1] is DateTime ? (DateTime) e.Arguments[1] : new DateTime();
			DateTime ngayKT = e.Arguments[2] is DateTime ? (DateTime) e.Arguments[2] : new DateTime();
			XL.XemCong(lstDSNVChkXemCong, ngayBD, ngayKT);

		}

		public void LayDSNVXemCong(DataTable pdataTableDSNVCheck, List<cUserInfo> plstDSNVChkXemCong, List<cUserInfo> pDSNVDuocPhepThaoTac) {

			DataRow[] arrRecord = pdataTableDSNVCheck.Select("check = true", "UserEnrollNumber asc", DataViewRowState.CurrentRows);
			if (arrRecord.Length == 0) { plstDSNVChkXemCong.Clear(); return; }
			foreach (DataRow row in arrRecord) {
				cUserInfo nhanvien = pDSNVDuocPhepThaoTac.Find(info => info.UserEnrollNumber == (int)row["UserEnrollNumber"]);
				nhanvien.ClearAll();
				plstDSNVChkXemCong.Add(nhanvien);
			}

		}

		private void frm_XemCong_FormClosing(object sender, FormClosingEventArgs e) {
			flstIDPhongBan.Clear();
			flstDSNV.Clear();
			if (flstDSNVChkXemCong != null) flstDSNVChkXemCong.Clear();
			if (fTableDSNV != null) fTableDSNV.Clear();
			this.Dispose();
		}


		private void btnTangCa_Click(object sender, EventArgs e) {
			DateTime t2 = dtpNgayKT.Value.Date;
			DateTime t1 = dtpNgayBD.Value.Date;
			t1 = t1.AddDays(-1d);
			t2 = t2.AddDays(2d).Subtract(new TimeSpan(0, 0, 1));
			frm_XacNhanTangCa frmXacNhanTangCa = new frm_XacNhanTangCa { fListNVChk = this.flstDSNVChkXemCong, fNgayBD = t1, fNgayKT = t2 };
			frmXacNhanTangCa.ShowDialog();
		}





		private void btnXemCT_Click(object sender, EventArgs e) {
			DateTime t1 = dtpNgayBD.Value.Date;
			DateTime t2 = dtpNgayKT.Value.Date;
			TimeSpan tmp = t2 - t1;
			int songay = tmp.Days + 3; // ngày 1->5 là trừ ra = 4, + 1 để ra số ngày, + thêm 2 để ra cột(UserEnrollNumber và số lượng)
			DataGridView currDataGrid;
			if (tabControlChiTiet.SelectedTab == tabTongHop) currDataGrid = dgrdTongHop;
			else if (tabControlChiTiet.SelectedTab == tabGioKDQD) currDataGrid = dgrdGioKDQD;
			else {
				currDataGrid = null;
				return;
			}
			currDataGrid.EndEdit();
			currDataGrid.Update();
			DataTable table = currDataGrid.DataSource as DataTable;
			if (table == null) return;
			DataRow[] arrRecord = table.Select("check = true", "UserEnrollNumber asc");
			object[,] xemCT = new object[flstDSNVChkXemCong.Count + 1, songay];

			if (arrRecord.Length == 0) return;
			frm_ChiTietVaoRa frm = new frm_ChiTietVaoRa();

			int tmpUserEnrollNumber = -1;
			int i = 1, j = 1;

			foreach (DataRow row in arrRecord) {
				if (tmpUserEnrollNumber == -1) {
					tmpUserEnrollNumber = (int)row["UserEnrollNumber"];
					xemCT[i, j] = tmpUserEnrollNumber; //11
					j++;
					xemCT[i, j] = (DateTime)row["TimeStrNgay"];//12
					j++;
				}
				else {
					int tmpNewUserEnrollNumber = (int)row["UserEnrollNumber"];
					if (tmpNewUserEnrollNumber == tmpUserEnrollNumber) {
						xemCT[i, j] = (DateTime)row["TimeStrNgay"];//13
						j++;
					}
					else {
						xemCT[i, 0] = j; //10
						i++; //23
						j = 1; //21
						tmpUserEnrollNumber = tmpNewUserEnrollNumber;
						xemCT[i, 1] = tmpUserEnrollNumber;//21
						j++;//22
						xemCT[i, j] = (DateTime)row["TimeStrNgay"];//22
						j++;
					}
				}

			}
			// ra khỏi vòng lặp còn NV cuối chưa cập nhật số lượng ngày check
			xemCT[i, 0] = j;
			i++;
			xemCT[0, 0] = i;
			frm.xemCT = xemCT;
			frm.flstDSNVChk = flstDSNVChkXemCong;
			frm.fNgayBD = t1.Date;
			frm.fNgayKT = t2.Date.AddDays(2d).Subtract(new TimeSpan(0, 0, 1));
			frm.ShowDialog();

		}


		private void Fill_NV_Cong_cell(List<cUserInfo> lstNV, ExcelWorksheet sheet, int startRow, int startCol, ref int iLastRow, ref int iLastCol) {
			int iNV, iRow, iCol, iNgay;
			string content = string.Empty;


			sheet.Cells[startRow - 2, startCol].Value = "STT";
			sheet.Cells[startRow - 2, startCol].Style.WrapText = true;
			sheet.Cells[startRow - 2, startCol, startRow - 1, startCol].Merge = true;
			sheet.Cells[startRow - 2, startCol, startRow - 1, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

			sheet.Cells[startRow - 2, startCol + 1].Value = "Họ tên";
			sheet.Cells[startRow - 2, startCol + 1].Style.WrapText = true;
			sheet.Cells[startRow - 2, startCol + 1, startRow - 1, startCol + 1].Merge = true;
			sheet.Cells[startRow - 2, startCol + 1, startRow - 1, startCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

			sheet.Cells[startRow - 2, startCol + 2].Value = "Mã CC";
			sheet.Cells[startRow - 2, startCol + 2].Style.WrapText = true;
			sheet.Cells[startRow - 2, startCol + 2, startRow - 1, startCol + 2].Merge = true;
			sheet.Cells[startRow - 2, startCol + 2, startRow - 1, startCol + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);


			for (iNV = 0, iRow = startRow; iNV < lstNV.Count; iNV++, iRow++) {
				cUserInfo nhanvien = lstNV[iNV];
				XL.ThongKe(nhanvien);
				#region stt, ten, ma
				sheet.Cells[iRow, startCol].Value = iNV + 1;
				sheet.Cells[iRow, startCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				sheet.Cells[iRow, startCol + 1].Value = nhanvien.UserFullName; //Ten
				sheet.Cells[iRow, startCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //Ten
				sheet.Cells[iRow, startCol + 2].Value = nhanvien.UserEnrollNumber;//MaCC
				sheet.Cells[iRow, startCol + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //MaCC

				#endregion
				for (iNgay = 1, iCol = startCol + 3; iNgay < nhanvien.DSNgayCong.Count - 1; iNgay++, iCol += 2) {
					cNgayCong ngayCong = nhanvien.DSNgayCong[iNgay];
					if (iNV == 0) {
						sheet.Cells[startRow - 2, iCol].Value = ngayCong.NgayCong.Date.ToString("d ");
						sheet.Cells[startRow - 2, iCol, startRow - 2, iCol + 1].Merge = true;
						sheet.Cells[startRow - 2, iCol, startRow - 2, iCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

						sheet.Cells[startRow - 1, iCol].Value = ngayCong.NgayCong.ToString("ddd");
						sheet.Cells[startRow - 1, iCol, startRow - 1, iCol + 1].Merge = true;
						sheet.Cells[startRow - 1, iCol, startRow - 1, iCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
					}

					content = (ngayCong.TongCong == 0d) ? "--" : ngayCong.TongCong.ToString("#0.##");
					sheet.Cells[iRow, iCol].Value = content;
					sheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
					content = (ngayCong.TongPhuCap == 0d) ? "--" : ngayCong.TongPhuCap.ToString("#0.##");
					sheet.Cells[iRow, iCol + 1].Value = content;
					sheet.Cells[iRow, iCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				}
				#region thống kê
				sheet.Cells[iRow, iCol].Value = nhanvien.TongCongThang.ToString("#0.##");// tổng công tháng
				sheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

				sheet.Cells[iRow, iCol + 1].Value = nhanvien.TongPCapThang.ToString("#0.##");// tổng PC tháng
				sheet.Cells[iRow, iCol + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

				sheet.Cells[iRow, iCol + 2].Value = nhanvien.TongCongH_CT_PT;// tổng HHPT
				sheet.Cells[iRow, iCol + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

				sheet.Cells[iRow, iCol + 3].Value = nhanvien.TongCongPhep;// tổng phép
				sheet.Cells[iRow, iCol + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

				sheet.Cells[iRow, iCol + 4].Value = nhanvien.TongCongBH;// tổng BH
				sheet.Cells[iRow, iCol + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

				sheet.Cells[iRow, iCol + 5].Value = nhanvien.TongCongRo;// tổng RO
				sheet.Cells[iRow, iCol + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

				sheet.Cells[iRow, iCol + 6].Value = nhanvien.TongCongCV;// tổng CV
				sheet.Cells[iRow, iCol + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
				#endregion
				iLastRow = iRow;
				iLastCol = iCol + 7;
			}
			#region các cột thống kê
			sheet.Cells[startRow - 2, iLastCol - 7, startRow - 1, iLastCol - 7].Value = "T.công";
			sheet.Cells[startRow - 2, iLastCol - 7, startRow - 1, iLastCol - 7].Merge = true;
			sheet.Cells[startRow - 2, iLastCol - 7, startRow - 1, iLastCol - 7].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

			sheet.Cells[startRow - 2, iLastCol - 6, startRow - 1, iLastCol - 6].Value = "T.PC";
			sheet.Cells[startRow - 2, iLastCol - 6, startRow - 1, iLastCol - 6].Merge = true;
			sheet.Cells[startRow - 2, iLastCol - 6, startRow - 1, iLastCol - 6].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

			sheet.Cells[startRow - 2, iLastCol - 5, startRow - 1, iLastCol - 5].Value = "H,PT";
			sheet.Cells[startRow - 2, iLastCol - 5, startRow - 1, iLastCol - 5].Merge = true;
			sheet.Cells[startRow - 2, iLastCol - 5, startRow - 1, iLastCol - 5].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);


			sheet.Cells[startRow - 2, iLastCol - 4, startRow - 1, iLastCol - 4].Value = "P";
			sheet.Cells[startRow - 2, iLastCol - 4, startRow - 1, iLastCol - 4].Merge = true;
			sheet.Cells[startRow - 2, iLastCol - 4, startRow - 1, iLastCol - 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);


			sheet.Cells[startRow - 2, iLastCol - 3, startRow - 1, iLastCol - 3].Value = "BH";
			sheet.Cells[startRow - 2, iLastCol - 3, startRow - 1, iLastCol - 3].Merge = true;
			sheet.Cells[startRow - 2, iLastCol - 3, startRow - 1, iLastCol - 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

			sheet.Cells[startRow - 2, iLastCol - 2, startRow - 1, iLastCol - 2].Value = "Ro";
			sheet.Cells[startRow - 2, iLastCol - 2, startRow - 1, iLastCol - 2].Merge = true;
			sheet.Cells[startRow - 2, iLastCol - 2, startRow - 1, iLastCol - 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

			sheet.Cells[startRow - 2, iLastCol - 1, startRow - 1, iLastCol - 1].Value = "CV";
			sheet.Cells[startRow - 2, iLastCol - 1, startRow - 1, iLastCol - 1].Merge = true;
			sheet.Cells[startRow - 2, iLastCol - 1, startRow - 1, iLastCol - 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
			#endregion

			ExcelColumn excelColumn;
			sheet.Column(1).Width = 4;
			sheet.Column(2).Width = 18;
			sheet.Column(3).Width = 5;
			for (int i = startCol + 3; i < iLastCol - 7; i++) {
				excelColumn = sheet.Column(i);
				excelColumn.Width = 3;
			}
			for (int i = iLastCol - 7; i < iLastCol; i++) {
				excelColumn = sheet.Column(i);
				excelColumn.Width = 4.75;
			}
		}

        
		private void XuatBBCong_PC(string file_path) {

			DateTime t1 = dtpNgayBD.Value.Date;
			DateTime t2 = dtpNgayKT.Value.Date;
			TimeSpan tmp = t2 - t1;
			int songay = tmp.Days + 1; // ngày 1->5 là trừ ra = 4, + 1 để ra số ngày, + thêm 2 để ra cột(UserEnrollNumber và số lượng)
			int soNV = flstDSNVChkXemCong.Count;
			object misValue = System.Reflection.Missing.Value;
            
			using (ExcelPackage p = new ExcelPackage()) {
				//Create a sheet
				p.Workbook.Worksheets.Add("Bang Cham Cong");
				ExcelWorksheet ws = p.Workbook.Worksheets[1];
				ws.Name = "Bang Cham Cong"; //Setting Sheet's name
				ws.Cells.Style.Font.Size = 8; //Default font size for whole sheet
				ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

				ws.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				//Merging cells and create a center heading for out table

				int iLastRow = 0, iLastCol = 0;
				int startRow = 2, startCol = 1;
				//VeCOT(ws, startRow, startCol, songay, t1, t2);
				Fill_NV_Cong_cell(flstDSNVChkXemCong, ws, 4, 1, ref iLastRow, ref iLastCol);
				

				ws.Cells[1, 1].Value = "Bảng chấm công từ ngày " + t1.ToString("dd/MM/yyyy") + " đến ngày " + t2.ToString("dd/MM/yyyy");
				ws.Cells[1, 1].Style.Font.Size = 14;
				ws.Cells[1, 1, 1, iLastCol - 1].Merge = true;
				ws.Cells[1, 1, 1, iLastCol - 1].Style.Font.Bold = true;
				ws.Cells[1, 1, 1, iLastCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				ws.Cells[2, 1, 3, iLastCol - 1].Style.Font.Bold = true;

				Byte[] bin = p.GetAsByteArray();
				try {
					File.WriteAllBytes(file_path, bin);
					AutoClosingMessageBox.Show("Xuất báo biểu thành công.","Thông báo", 2000);
				}
				catch (Exception exception) {
					if (exception is UnauthorizedAccessException) MessageBox.Show("Bạn chưa được cấp quyền ghi file vào folder.", "Lỗi");
					else if (exception is DirectoryNotFoundException) MessageBox.Show("Không tìm thấy folder lưu trữ.", "Lỗi");
					else if (exception is IOException) MessageBox.Show("File đang mở bởi ứng dụng khác.","Lỗi");
					else MessageBox.Show("Không thể ghi được file báo biểu.", "Lỗi");
				}

			}
		}

		private void button1_Click(object sender, EventArgs e) {
			saveFileDialog.Filter = "Excel File|*.xlsx";
			saveFileDialog.ShowDialog();
			if (saveFileDialog.FileName != string.Empty) {
				XuatBBCong_PC(saveFileDialog.FileName);
			}
		}



	}
}
