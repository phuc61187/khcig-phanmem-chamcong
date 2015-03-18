using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapNhatGioChamCong.DAO;
using CapNhatGioChamCong.DTO;

namespace CapNhatGioChamCong {
	public partial class frm_SuaGioHangLoat2 : Form {

		#region local variable
		public List<int> flstIDPhongBan { get; set; }
		public List<int> lstMaCC { get; set; }
		public DataTable fTablePhgBanDuocThaoTac { get; set; }
		public DataTable fTableDSNVDuocThaotac { get; set; }
		public DataTable fDataTableChkInOut { get; set; }
		public List<cUserInfo> flstDSNVPhong { get; set; }
		public List<cUserInfo> flstDSNVChkXemGioVaoRa { get; set; }
		readonly CheckBox checkAll_GridDSNV = new CheckBox();
		readonly CheckBox checkAllGridCheckIn = new CheckBox();
		readonly CheckBox checkAllGridCheckOut = new CheckBox();

		//[Chú ý] chỉ lọc những giờ vào ra chưa xác nhận vì ko cho sửa những giờ vào ra đã xác nhận
		string SelStr_GetDSGioVaoRa() {
			List<string> ds = new List<string>();
			if (flstDSNVChkXemGioVaoRa.Count == 0) return string.Empty;
			foreach (cUserInfo nhanvien in flstDSNVChkXemGioVaoRa)
				ds.Add(nhanvien.UserEnrollNumber.ToString());

			string selectQueryString = @" SELECT CheckInOut.UserEnrollNumber,TimeStr,MachineNo,Source,UserInfo.UserFullName";
			selectQueryString += @" from CheckInOut, UserInfo ";
			selectQueryString += @" where (((TimeStr between @BatDauVao and @KetThucVao) and MachineNo % 2 = 1)
                                        or ((TimeStr between @BatDauRa and @KetThucRa) and MachineNo % 2 = 0))
                                        and (UserInfo.UserEnrollNumber = CheckInOut.UserEnrollNumber)
										and IDXacNhanCaVaLamThem is null
		                                and (CheckInOut.UserEnrollNumber = {0} ";
			selectQueryString = String.Format(selectQueryString, String.Join(" or CheckInOut.UserEnrollNumber = ", ds.ToArray()));
			selectQueryString += " ) ";
			selectQueryString += " group by CheckInOut.UserEnrollNumber,TimeStr,MachineNo,Source,UserInfo.UserFullName";
			selectQueryString += " order by CheckInOut.UserEnrollNumber asc, TimeStr asc";
			ds.Clear();
			return selectQueryString;
		}

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
			flstIDPhongBan.Clear();
			if (e.Node.FirstNode != null) GetNodeID(e.Node);
			else flstIDPhongBan.Add((int)e.Node.Tag);
			e.Node.Expand();

			string temp = "UserIDD = {0}";
			temp = String.Format(temp, String.Join(" or UserIDD = ", flstIDPhongBan.ToArray()));

			DataTable table;
			if (dgrdDSNVTrgPhg.DataSource == null)
				table = fTableDSNVDuocThaotac.Clone();
			else {
				table = dgrdDSNVTrgPhg.DataSource as DataTable;
				table.Rows.Clear();
			}

			foreach (DataRow row in fTableDSNVDuocThaotac.Select(temp, "UserIDD asc, UserEnrollNumber asc", DataViewRowState.CurrentRows))
				table.ImportRow(row);

			dgrdDSNVTrgPhg.DataSource = table;
			checkAll_GridDSNV.Checked = false;
		}

		#endregion

		private void KhoitaoDSNV(DataTable fTableDSNV, List<cUserInfo> dsnv) {
			if (fTableDSNV == null || fTableDSNV.Rows.Count == 0) return;

			foreach (DataRow row in fTableDSNV.Rows) {
				cShiftSchedule tmpLichTrinh = ThamSo.DSLichTrinh.Find(item => item.SchID == (int)row["SchID"]);
				List<cShift> tmpDSCa = tmpLichTrinh.ListT1;
				List<cShift> tmpDSCaMoRong = ThamSo.TaoDSCaMoRong(tmpDSCa); // đã bao gồm Khác(int.Minvalue)
				cUserInfo nhanvien = new cUserInfo() {
					UserEnrollNumber = (int)row["UserEnrollNumber"], UserFullName = row["UserFullName"].ToString(),
					HeSoLuongCB = (Single)row["HeSoLuongCB"], HeSoLuongSP = (Single)row["HeSoLuongSP"],
					LichTrinhLV = tmpLichTrinh, DSCa = tmpDSCa, DSCaMoRong = tmpDSCaMoRong
				};
				dsnv.Add(nhanvien);
			}
		}

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
			DataTable dt = tempGrid.DataSource as DataTable;

			if (dt.Rows.Count != 0) {
				foreach (DataRow row in dt.Rows) {
					row["check"] = tmpCheckAll;
				}
			}

			tempGrid.EndEdit();
			tempGrid.Update();
			//tempGrid.Refresh();
		}
		#endregion
		private void btnXem_Click(object sender, EventArgs e) {
			//0. xoa dữ liệu cũ
			//1. lấy dữ liệu từ form
			dtpBD.Update(); dtpKT.Update();
			DateTime startTime = dtpBD.Value;
			DateTime endTime = dtpKT.Value;
			dgrdDSNVTrgPhg.EndEdit();
			this.BindingContext[this.dgrdDSNVTrgPhg.DataSource].EndCurrentEdit();
			//2. lấy danh sách nhân viên check
			LayDSNVXemCong(dgrdDSNVTrgPhg.DataSource as DataTable, flstDSNVChkXemGioVaoRa, flstDSNVPhong);

			//3. lấy dữ liệu chấm công của các nhân viên
			if (flstDSNVChkXemGioVaoRa.Count == 0) {
				AutoClosingMessageBox.Show("Chưa chọn Nhân viên", "Thông báo", 2000);
				return;
			}

			try {
				fDataTableChkInOut = SqlDataAccessHelper.ExecuteQueryString(SelStr_GetDSGioVaoRa()
																		  , new[] { "@BatDauVao", "@KetThucVao", "@BatDauRa", "@KetThucRa" }
																		  , new object[] { startTime, endTime, startTime, endTime });
			} catch (Exception) {
				MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại sau.", "Lỗi");
				return;
			}

			DataTable dataTableChiTietVao = dgrdCTGioVao.DataSource as DataTable;
			DataTable dataTableChiTietRa = dgrdCTGioRa.DataSource as DataTable;
			if (dataTableChiTietVao == null) {
				dataTableChiTietVao = fDataTableChkInOut.Clone();
				dataTableChiTietVao.Columns.Add("check", typeof(bool));
			}
			else dataTableChiTietVao.Rows.Clear();

			if (dataTableChiTietRa == null) {
				dataTableChiTietRa = fDataTableChkInOut.Clone();
				dataTableChiTietRa.Columns.Add("check", typeof(bool));
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

		}


		public void LayDSNVXemCong(DataTable pdataTableDSNVCheck, List<cUserInfo> plstDSNVChkXemCong, List<cUserInfo> pDSNVDuocPhepThaoTac) {
			if (plstDSNVChkXemCong != null) plstDSNVChkXemCong.Clear();
			DataRow[] arrRecord = pdataTableDSNVCheck.Select("check = true", "UserEnrollNumber asc", DataViewRowState.CurrentRows);
			if (arrRecord.Length == 0) { plstDSNVChkXemCong.Clear(); return; }
			foreach (DataRow row in arrRecord) {
				cUserInfo nhanvien = pDSNVDuocPhepThaoTac.Find(info => info.UserEnrollNumber == (int)row["UserEnrollNumber"]);
				nhanvien.ClearAll();
				plstDSNVChkXemCong.Add(nhanvien);
			}

		}


		private void btnSuaGio_Click(object sender, EventArgs e) {
			bool flag = false;
			bool tmpSuaGioVao = (tabControl1.SelectedIndex == 0);

			DataGridView dataGridThaoTac = tmpSuaGioVao ? dgrdCTGioVao : dgrdCTGioRa;
			DataTable table = dataGridThaoTac.DataSource as DataTable;
			if (table == null) return;
			DataRow[] tmpArrRow = (table).Select("check = true");
			if (tmpArrRow.Length == 0) {
				AutoClosingMessageBox.Show("Chưa chọn nhân viên sửa giờ chấm công.", "Thông báo", 1000);
				return;
			}

			frm_ThongTinSuaGioHangLoat frm_ThongTinSuaGioHangLoat = new frm_ThongTinSuaGioHangLoat() { fNgaySuaMacDinh = dtpBD.Value.Date, fCheckIn = tmpSuaGioVao };
			frm_ThongTinSuaGioHangLoat.ShowDialog();

			if (frm_ThongTinSuaGioHangLoat.fOK) {
				DateTime tmpGioMoi = frm_ThongTinSuaGioHangLoat.fGioMoi;
				bool tmpKieuGioMoi = frm_ThongTinSuaGioHangLoat.fCheckIn;
				foreach (DataRow row in tmpArrRow) {
					int tmpUserEnrollNumber = (int)row["UserEnrollNumber"];
					DateTime tmpGioCu = (DateTime)row["TimeStr"];
					int tmpMachineNo = (int)row["MachineNo"];
					string tmpSourceOld = row["Source"].ToString();
					string tmpLyDo = frm_ThongTinSuaGioHangLoat.fLydo;
					string tmpGhiChu = frm_ThongTinSuaGioHangLoat.fGhichu;
					if (SuaGioChoNV(tmpUserEnrollNumber, tmpGioCu, tmpGioMoi, tmpSuaGioVao, tmpKieuGioMoi, tmpSourceOld, tmpMachineNo, tmpLyDo, tmpGhiChu) == false) {
						flag = true;
					}

				}
				if (flag == false) {
					btnXem_Click(btnXem, null);
				}
				else {
					MessageBox.Show("Xảy ra lỗi trong quá trình sửa giờ cho Nhân viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK);
				}
			}

		}

		private bool SuaGioChoNV(int pUserEnrollNumber, DateTime pGioCu, DateTime pGioMoi, bool pKieuGioCu, bool pKieuGioMoi, string pSourceOld, int pMachineNoOld, string pLydo, string pGhichu) {
			int kq = 0;
			int pMachineNoNew = (pKieuGioMoi) ? 21 : 22;

			kq = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.UpdStrSuaGioVaoRa(), new string[] { "@UserEnrollNumber", "@TimeStrOld", "@TimeDateNew", "@TimeStrNew", "@SourceNew", "@MachineNoNew" },
														 new object[] { pUserEnrollNumber, pGioCu, pGioMoi.Date, pGioMoi, "PC", pMachineNoNew });
			if (kq == 0) return false;
			kq = SqlDataAccessHelper.ExecNoneQueryString(ThamSo.InsStrBackupThemGioVaoRa(),
													 new string[] { "@UserEnrollNumber", "@TimeStrOld", "@TimeStrNew", "@SourceOld", "@SourceNew", "@MachineNoOld", "@MachineNoNew", "@UserID", "@Explain", "@Note", "@CommandType" },
													 new object[] { pUserEnrollNumber, pGioCu, pGioMoi, pSourceOld, "PC", pMachineNoOld, pMachineNoNew, ThamSo.currUserID, pLydo, pGhichu, 0 });
			return true;
		}

		public frm_SuaGioHangLoat2() {
			InitializeComponent();
			dgrdCTGioVao.AutoGenerateColumns = dgrdCTGioRa.AutoGenerateColumns = false;

			foreach (DataGridViewColumn column in dgrdCTGioVao.Columns) column.Name = ThamSo.prefixColNameGrid1 + column.DataPropertyName;

			foreach (DataGridViewColumn column in dgrdCTGioRa.Columns) column.Name = ThamSo.prefixColNameGrid2 + column.DataPropertyName;

			ThamSo.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));
			ThamSo.VeCheckBox_CheckAll(dgrdCTGioVao, checkAllGridCheckIn, checkAll_CheckedChanged, new Point(7, 3));
			ThamSo.VeCheckBox_CheckAll(dgrdCTGioRa, checkAllGridCheckOut, checkAll_CheckedChanged, new Point(7, 3));

		}

		private void frm_SuaGioHangLoat2_Load(object sender, EventArgs e) {

			// 1. khởi tạo các biến cục bộ
			flstIDPhongBan = new List<int>();

			//2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan : xoá dữ liệu trước và load
			fTablePhgBanDuocThaoTac = ThamSo.TablePhongBan.Copy();
			treePhongBan.Nodes.Clear();
			loadTreePhgBan(treePhongBan, fTablePhgBanDuocThaoTac);

			// 3. Duyệt  dữ liệu toàn bộ danh sách nhân viên được phép thao tác(và thêm cột check) 			
			// và  khởi tạo các giá trị mặc định cho từng nhân viên  
			fTableDSNVDuocThaotac = ThamSo.DataTableDSNV.Copy();
			fTableDSNVDuocThaotac.Columns.Add("check", typeof(bool));
			flstDSNVPhong = new List<cUserInfo>();
			KhoitaoDSNV(fTableDSNVDuocThaotac, flstDSNVPhong);

            flstDSNVChkXemGioVaoRa = new List<cUserInfo>();

			// đăng ký sự kiện cho tree và chọn topNode
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;
			treePhongBan.SelectedNode = treePhongBan.TopNode;
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}
