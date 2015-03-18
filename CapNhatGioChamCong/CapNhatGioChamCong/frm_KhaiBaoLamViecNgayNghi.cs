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
using CapNhatGioChamCong.BUS;
using CapNhatGioChamCong.DTO;

namespace CapNhatGioChamCong {
	public partial class frm_KhaiBaoLamViecNgayNghi : Form {
		#region local variable

		public List<int> flstIDPhongBan { get; set; }
		public DataTable fTablePhongBan { get; set; }
		public DataTable fTableDSNV { get; set; }
		public List<cUserInfo> flstDSNVPhong { get; set; }
		public DateTime NgayKhaiBao = DateTime.Today;
		#region 3 biến checkbox check all: 1 của DSNV cần xem công. 2 DS Công của NV
		readonly CheckBox checkAll_GridDSNV = new CheckBox();
		readonly CheckBox checkAll_GridNgayVang = new CheckBox();
		#endregion

		string SelStr_GetDSGioVaoRa(List<cUserInfo> plstDSNVChkXemCong) {
			List<string> ds = new List<string>();
			if (plstDSNVChkXemCong.Count == 0) return string.Empty;
			foreach (cUserInfo nhanvien in plstDSNVChkXemCong)
				ds.Add(nhanvien.UserEnrollNumber.ToString());

			string selectQueryString = @" SELECT CheckInOut.UserEnrollNumber,TimeStr,MachineNo,Source,IDXacNhanCaVaLamThem,UserInfo.UserFullName,
													ID,  ShiftID,  Onduty,  Offduty,
													  DayCount,  WorkingTime,  Workingday,
													  TimeStrIn,  TimeStrOut,
													  LateMin,EarlyMin,  OTMin ";
			selectQueryString += @" from CheckInOut, UserInfo, XacNhanCaVaLamThem ";
			selectQueryString += @" where (((TimeStr between @BatDauVao and @KetThucVao) and MachineNo % 2 = 1)
                                        or ((TimeStr between @BatDauRa and @KetThucRa) and MachineNo % 2 = 0))
                                        and (UserInfo.UserEnrollNumber = CheckInOut.UserEnrollNumber)
		                                and (CheckInOut.UserEnrollNumber = {0} ";
			selectQueryString = String.Format(selectQueryString, String.Join(" or CheckInOut.UserEnrollNumber = ", ds.ToArray()));
			selectQueryString += " ) ";
			selectQueryString += " and IDXacNhanCaVaLamThem = ID " +
								 " group by CheckInOut.UserEnrollNumber,TimeStr,MachineNo,IDXacNhanCaVaLamThem,Source,UserInfo.UserFullName";
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

			DataTable table = dgrdDSNVTrgPhg.DataSource as DataTable;
			if (table == null) table = fTableDSNV.Clone();
			else table.Rows.Clear();

			foreach (DataRow row in fTableDSNV.Select(temp, "UserIDD asc, UserEnrollNumber asc", DataViewRowState.CurrentRows))
				table.ImportRow(row);

			dgrdDSNVTrgPhg.DataSource = table;
			checkAll_GridDSNV.Checked = false;
		}

		#endregion

		#region vẽ checkBox check all và xử lý sự kiện check

		void checkAll_CheckedChanged(object sender, EventArgs e) {
			//1. xác định datagrid nào đang check all
			DataGridView tempGrid;
			if (sender == checkAll_GridDSNV) tempGrid = dgrdDSNVTrgPhg;
			else tempGrid = dgrdNgayNghi;

			// 2. kiểm tra nếu ko có dòng dữ liệu nào thì thoát
			if (tempGrid.Rows.Count == 0) return;

			//3. có dữ liệu, duyệt qua hết và gán giá trị check (or uncheck), sau đó endEDIT lại lưới để dòng đầu tiên có giá trị
			bool tmpCheckAll = ((CheckBox)sender).Checked;

			tempGrid.BeginEdit(true);
			DataTable dt = tempGrid.DataSource as DataTable;

			if (dt != null && dt.Rows.Count != 0) {
				foreach (DataRow row in dt.Rows)
					row["check"] = tmpCheckAll;
			}

			tempGrid.EndEdit();
			tempGrid.Update();
		}
		#endregion


		public frm_KhaiBaoLamViecNgayNghi() {
			InitializeComponent();
			dgrdDSNVTrgPhg.AutoGenerateColumns = dgrdNgayNghi.AutoGenerateColumns = false;
			ThamSo.VeCheckBox_CheckAll(dgrdNgayNghi, checkAll_GridNgayVang, checkAll_CheckedChanged, new Point(7, 3));
			ThamSo.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));
		}

		private void frm_KhaiBaoVang_Load(object sender, EventArgs e) {
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

			// đăng ký sự kiện cho tree và chọn topNode
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;
			treePhongBan.SelectedNode = treePhongBan.TopNode;

			// lấy tháng hiện tại cho dtpThang
			dtpThang.Value = NgayKhaiBao;

		}




		private void btnThem_Click(object sender, EventArgs e) {
			//1. lấy dữ liệu từ form
			dtpThang.Update();
			NgayKhaiBao = dtpThang.Value.Date;
			//-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

			dgrdDSNVTrgPhg.EndEdit();
			dgrdDSNVTrgPhg.Update();

			//2. lấy danh sách nhân viên check
			object[,] arrDSNVCheck = new object[dgrdDSNVTrgPhg.Rows.Count + 1, 2];
			LayDSMaCC_MaNV_Checked(dgrdDSNVTrgPhg.DataSource as DataTable, arrDSNVCheck);
			if ((int)arrDSNVCheck[0, 0] == 0) {
				AutoClosingMessageBox.Show("Chưa chọn Nhân viên", "Thông báo", 2000);
				return;
			}

			Single hesoPC = 1f;
			// tính phụ cấp thêm 100% công : tổng cộng 1 công , 100%phụ cấp

			// lấy công

			bool kqThaotac = XL.KhaiBaoLVNgayNghiChoNV(arrDSNVCheck, NgayKhaiBao, hesoPC);
			if (kqThaotac == false)
				MessageBox.Show("Có lỗi trong quá trình thực hiện. Vui lòng kiểm tra lại dữ liệu.", "Lỗi");

			// sau khi thao tác xong thì liệt kê lại
			btnLietKe.PerformClick();
		}



		public void LayDSMaCC_MaNV_Checked(DataTable pdataTableDSNVCheck, object[,] arrDSNVCheck) {
			DataRow[] arrRecord = pdataTableDSNVCheck.Select("check = true", "UserEnrollNumber asc", DataViewRowState.CurrentRows);
			if (arrRecord.Length == 0) {
				arrDSNVCheck[0, 0] = 0;
				return;
			}
			else arrDSNVCheck[0, 0] = arrRecord.Length;

			for (int i = 0; i < arrRecord.Length; i++) {
				DataRow row = arrRecord[i];
				arrDSNVCheck[i + 1, 0] = ((int)row["UserEnrollNumber"]);
				arrDSNVCheck[i + 1, 1] = row["UserFullCode"].ToString();
			}
		}

		public List<DateTime> LayNgayCheck(CheckedListBox checkedList) {
			List<DateTime> kq = new List<DateTime>();
			if (checkedList.CheckedItems.Count == 0) return kq;
			foreach (object item in checkedList.CheckedItems) {
				kq.Add((DateTime)item);
			}
			return kq;
		}

		private void btnXoa_Click(object sender, EventArgs e) {
			//-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

			dgrdNgayNghi.EndEdit();
			dgrdNgayNghi.Update();

			//2. lấy danh sách row cần xóa
			DataTable table = dgrdNgayNghi.DataSource as DataTable;
			if (table == null) return;

			DataRow[] arrRecord = table.Select("check = true", "UserEnrollNumber asc");
			if (arrRecord.Length == 0) return;

			bool kqThaotac = XL.XoaLamViecNgayNghiDaKhaiBao(arrRecord);
			if (kqThaotac == false)
				MessageBox.Show("Có lỗi trong quá trình thực hiện. Vui lòng kiểm tra lại dữ liệu.", "Lỗi");
			btnLietKe_Click(btnLietKe, null);
		}

		private void btnLietKe_Click(object sender, EventArgs e) {
			//1. lấy dữ liệu từ form
			dtpThang.Update();
			NgayKhaiBao = dtpThang.Value;
			DateTime ngayBD = new DateTime(NgayKhaiBao.Year, NgayKhaiBao.Month, 1);
			DateTime ngayKT = new DateTime(NgayKhaiBao.Year, NgayKhaiBao.Month, DateTime.DaysInMonth(NgayKhaiBao.Year, NgayKhaiBao.Month));
			//-----------BUG [KHÔNG PHẢI BUG] CHỈ TÔ MÀU ĐỂ CHÚ Ý] bắt buộc EndEdit thao tác trên Grid rồi mới thực hiện xử lý tác vụ

			dgrdDSNVTrgPhg.EndEdit();
			dgrdDSNVTrgPhg.Update();

			//2. lấy danh sách nhân viên check
			DataTable table = dgrdDSNVTrgPhg.DataSource as DataTable;
			if (table == null) return;
			DataRow[] arrRecord = table.Select("check = true", "UserEnrollNumber asc", DataViewRowState.CurrentRows);
			if (arrRecord.Length == 0) {
				AutoClosingMessageBox.Show("Chưa chọn Nhân viên", "Thông báo", 2000);
				return;
			}
			object[] arrDSNVCheck = new object[arrRecord.Length];

			for (int i = 0; i < arrRecord.Length; i++) {
				DataRow row = arrRecord[i];
				arrDSNVCheck[i] = ((int)row["UserEnrollNumber"]);
			}

			DataTable dataTable = dgrdNgayNghi.DataSource as DataTable;
			if (dataTable != null) dataTable.Rows.Clear();
			dataTable = XL.LietKeLamViecNgayNghiDaKhaiBao(arrDSNVCheck, ngayBD, ngayKT);

			dataTable.Columns.Add("check", typeof(bool));
			dgrdNgayNghi.DataSource = dataTable;

			GC.Collect();
		}

		private void btnLietKe_MouseHover(object sender, EventArgs e) {
			toolTip1.Show("Liệt kê các ngày đi làm vào ngày nghỉ tính PC 100% lương đã khai báo.", btnLietKe, 3000);
		}
	}
}
