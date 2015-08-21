using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;

namespace ChamCong_v04.UI.XepLich {
	public partial class fmTKeCongTheoNVu : Form {

		public List<int> m_listIDPhongBan = new List<int>();

		public fmTKeCongTheoNVu() {
			InitializeComponent();

			log4net.Config.XmlConfigurator.Configure();
			//không cho autogen các column khi bind dữ liệu
			dgrdThongKe.AutoGenerateColumns = false;
			//test git
		}

		private void fmTKeCongTheoNVu_Load(object sender, EventArgs e) {
			//load mặc định các dateTimePicker
			DateTime ngayBD = MyUtility.FirstDayOfMonth(DateTime.Today);
			DateTime ngayKT = MyUtility.LastDayOfMonth(DateTime.Today);
			dtpNgayBD.Value = ngayBD;
			dtpNgayKT.Value = ngayKT;
			dtpThang.Value = ngayBD;
			numQuy.Value = ((DateTime.Today.Month - (DateTime.Today.Month % 3)) / 3)+1;
			int thangDauQuy = (int)numQuy.Value;
			dtpQuyNam.Value = new DateTime(DateTime.Today.Year, thangDauQuy, 1);

			//load tree
			XL.loadTreePhgBan(treePhongBan);
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;

			//load danh sách nhiệm vụ
			DataTable tableNhiemVu = SqlDataAccessHelper.ExecSPQuery(SPName.sp_NhiemVu_DocBang.ToString());
			checkedListNhiemVu.DataSource = tableNhiemVu;
			checkedListNhiemVu.ValueMember = "MaNhiemVu";
			checkedListNhiemVu.DisplayMember = "TenNhiemVu";

			/*tạo sẵn cấu trúc table exclude các nhân viên để mỗi lần thêm mới thì chỉ cần lấy cấu trúc datarow
			 * của template table này
			 */
			DataTable templateTable = new DataTable("ExcludeNhanVien");
			templateTable.Columns.Add(new DataColumn { ColumnName = "UserEnrollNumber", DataType = typeof(int) });
			templateTable.Columns.Add(new DataColumn { ColumnName = "UserFullName", DataType = typeof(string) });
			listExcludeNV.DataSource = templateTable;
			listExcludeNV.ValueMember = "UserEnrollNumber";
			listExcludeNV.DisplayMember = "UserFullName";
		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			#region mỗi lần chọn node thì lấy ID node hiện tại và tất cả node con

			m_listIDPhongBan.Clear();
			e.Node.Expand();
			TreeNode topnode = XL.TopNode(e.Node); //đưa về root để thực hiện từ trên xuống
			if (topnode != null) XL.GetIDNodeAndChildNode1(e.Node, ref m_listIDPhongBan); // chỉ lấy các phòng ban được phép, 
			else {
				var temp = ((DataRow)e.Node.Tag);
				if ((int)temp["IsYes"] == 1) m_listIDPhongBan.Add((int)temp["ID"]);
			}

			#endregion
		}

		private void btnXem_Click(object sender, EventArgs e) {
			/*1. lấy danh sách phòng ban(và con) được phép thao tác để lọc nhân viên trong phòng ngày
			 * 2. lấy lựa chọn để xác định khoảng thời gian chọn
			 * 3. lấy danh sách các nhiệm vụ được chọn để lọc các nhân viên có nhiệm vụ này
			 * 4. lấy danh sách exclude nhân viên để loại ra các nhân viên này
			 * 5. thực hiện
			 */

			DataTable tableUserIDD = MyUtility.Array_To_DataTable("ArrayUserIDD", m_listIDPhongBan);

			DateTime ngayBD, ngayKT;
			this.XacDinhKhoangThoiGianChon(out ngayBD, out ngayKT);

			List<int> dsNhiemVuDangChon = new List<int>();
			this.LayDSNhiemVuDangChon(ref dsNhiemVuDangChon);

			List<int> dsExcludeMaCC = new List<int>();
			this.LayDSExcludeNV(ref dsExcludeMaCC);

			DataTable tableThongke = this.ThongKeCong_PC_Phep(ngayBD, ngayKT, tableUserIDD, dsExcludeMaCC, dsNhiemVuDangChon);
			dgrdThongKe.DataSource = tableThongke;
		}

		private DataTable ThongKeCong_PC_Phep(DateTime ngayBD, DateTime ngayKT, DataTable TableUserIDD, List<int> dsExcludeMaCC, List<int> dsNhiemVuDangChon) {
			/* 1. chia thành các đoạn thời gian, xác định rõ đoạn nào đã kết công, đoạn nào chưa kết công
			 * 2. thống kê các đoạn đã kết công
			 * 3. thống kê các đoạn chưa kết công
			 * 4. tổng hợp
			 */
			//tbd nhớ kiểm tra khoảng thời gian tìm kiếm quá xa nhau thì báo vì dữ liệu quá nhiều sẽ gây lỗi
			List<List<DateTime>> arrDoanThoigian;
			ChiaDoanThoiGian(ngayBD, ngayKT, out arrDoanThoigian);
			List<List<DateTime>> arrDoanTGDaKetCong;
			List<List<DateTime>> arrDoanTGChuaKetCong;
			TachKC_ChuaKC(arrDoanThoigian, out arrDoanTGChuaKetCong, out arrDoanTGDaKetCong);

			DataTable tableNhanVien = this.LayDSNhanVien(TableUserIDD, dsExcludeMaCC, dsNhiemVuDangChon);
			List<cUserInfo> listNhanVien = new List<cUserInfo>();
			foreach (DataRow row in tableNhanVien.Rows) {
				cUserInfo nhanvien = new cUserInfo {
					MaCC = (int)row["UserEnrollNumber"],
					MaNV = row["UserFullCode"].ToString(),
					TenNV = row["UserFullName"].ToString(),
				};
				XL.GetLichTrinhNV(nhanvien, row["SchID"] != DBNull.Value ? (int)row["SchID"] : (int?)null);
				listNhanVien.Add(nhanvien);
			}
			DataTable tableThongKe = this.ThongKe(listNhanVien, arrDoanTGChuaKetCong, arrDoanTGDaKetCong);
			return tableThongKe;
		}

		private DataTable ThongKe(List<cUserInfo> listNhanVien, List<List<DateTime>> arrDoanTGChuaKetCong, List<List<DateTime>> arrDoanTGDaKetCong) {
			#region tạo DataTable result

			DataTable result = new DataTable();
			result.Columns.Add(new DataColumn { ColumnName = "UserEnrollNumber", DataType = typeof(int) });
			result.Columns.Add(new DataColumn { ColumnName = "UserFullCode", DataType = typeof(string) });
			result.Columns.Add(new DataColumn { ColumnName = "UserFullName", DataType = typeof(string) });
			result.Columns.Add(new DataColumn { ColumnName = "TongCong1", DataType = typeof(float), DefaultValue = 0f });
			result.Columns.Add(new DataColumn { ColumnName = "TongPC1", DataType = typeof(float), DefaultValue = 0f });
			result.Columns.Add(new DataColumn { ColumnName = "TongPhep1", DataType = typeof(float), DefaultValue = 0f });
			result.Columns.Add(new DataColumn { ColumnName = "TongCong2", DataType = typeof(float), DefaultValue = 0f });
			result.Columns.Add(new DataColumn { ColumnName = "TongPC2", DataType = typeof(float), DefaultValue = 0f });
			result.Columns.Add(new DataColumn { ColumnName = "TongPhep2", DataType = typeof(float), DefaultValue = 0f });
			result.Columns.Add(new DataColumn { ColumnName = "TongCong3", DataType = typeof(float), DefaultValue = 0f });
			result.Columns.Add(new DataColumn { ColumnName = "TongPC3", DataType = typeof(float), DefaultValue = 0f });
			result.Columns.Add(new DataColumn { ColumnName = "TongPhep3", DataType = typeof(float), DefaultValue = 0f });
			//result.Columns.Add(new DataColumn {ColumnName = "", DataType = typeof ()});

			#endregion

			#region tạo các row tương ứng từng nhân viên, mặc định tổng công, pc = 0 (đã set sẵn do trong lúc tạo datatable)

			for (int i = 0; i < listNhanVien.Count; i++) {
				DataRow row = result.NewRow();
				row["UserEnrollNumber"] = listNhanVien[i].MaCC;
				row["UserFullCode"] = listNhanVien[i].MaNV;
				row["UserFullName"] = listNhanVien[i].TenNV;
				result.Rows.Add(row);
			}

			#endregion

			#region nếu có khoảng thời gian chưa kết công thì xem công khoảng thời gian này và cập nhật phần tổng công 1...

			if (arrDoanTGChuaKetCong.Count != 0) {
				var ngayBD = arrDoanTGChuaKetCong[0][0];
				var ngayKT = arrDoanTGChuaKetCong[arrDoanTGChuaKetCong.Count - 1][1];
				XL.XemCong_v08(listNhanVien, ngayBD.AddDays(-2d), ngayKT.AddDays(2d));
				foreach (DataRow rowNhanVien in result.Rows) // tìm và cập nhật phần tổng công 1 tương ứng cho nhân viên đó
				{
					var uen = (int)rowNhanVien["UserEnrollNumber"];
					cUserInfo nhanvien = listNhanVien.Find(item => item.MaCC == uen);
					var tongCong1 = 0f; //tbd
					var tongPC1 = 0f; //tbd
					var tongPhep1 = 0f; //tbd
					foreach (cNgayCong ngayCong in nhanvien.DSNgayCong) {
						if (ngayCong.Ngay < ngayBD || ngayCong.Ngay > ngayKT) continue;
						tongCong1 += ngayCong.TongCong;
						tongPC1 += ngayCong.PhuCaps._TongPC;
						var listPhep = (ngayCong.DSVang.Where(item => item.MaLV_Code.ToUpper() == "P").ToList());
						if (listPhep.Count > 0) tongPhep1 += listPhep.Sum(item1 => item1.WorkingDay);
					}
					rowNhanVien["TongCong1"] = tongCong1;
					rowNhanVien["TongPC1"] = tongPC1;
					rowNhanVien["TongPhep1"] = tongPhep1;
				}
			}

			#endregion

			#region nếu có khoảng thời gian đã kết công thì cập nhật phần tổng cong 2

			if (arrDoanTGDaKetCong.Count != 0) {
				DataTable tableArrUEN = MyUtility.Array_To_DataTable("TableArrUserEnrollNumber", (from cUserInfo item in listNhanVien select item.MaCC).ToList());
				DataTable tableTKeDaKetCong = SqlDataAccessHelper.ExecSPQuery(SPName.sp_ThongKeCongVaPhuCap.ToString(),
																			  new SqlParameter("@ArrUserEnrollNumber", SqlDbType.Structured) { Value = tableArrUEN },
																			  new SqlParameter("@NgayBatDau", arrDoanTGDaKetCong[0][0]),
																			  new SqlParameter("@NgayKetThuc", arrDoanTGDaKetCong[arrDoanTGDaKetCong.Count - 1][1]));
				foreach (DataRow rowCongDaKet in tableTKeDaKetCong.Rows) //tìm và cập nhật phần tổng công 2 tương ứng cho nhân viên đó
				{
					var uen = (int)rowCongDaKet["UserEnrollNumber"];
					DataRow[] rowNhanVien = result.Select("UserEnrollNumber=" + uen);
					rowNhanVien[0]["TongCong2"] = Convert.ToSingle(rowCongDaKet["TongCong"]);
					rowNhanVien[0]["TongPC2"] = Convert.ToSingle(rowCongDaKet["TongPC"]);
					rowNhanVien[0]["TongPhep2"] = rowCongDaKet["TongPhep"] != DBNull.Value ? Convert.ToSingle(rowCongDaKet["TongPhep"]) : 0f;
				}
			}

			#endregion
			foreach (DataRow row in result.Rows) {
				row["TongCong3"] = (float)row["TongCong1"] + (float)row["TongCong2"];
				row["TongPC3"] = (float)row["TongPC1"] + (float)row["TongPC2"];
				row["TongPhep3"] = (float)row["TongPhep1"] + (float)row["TongPhep2"];
			}

			return result;
		}

		private DataTable LayDSNhanVien(DataTable TableUserIDD, List<int> dsExcludeMaCC, List<int> dsNhiemVuDangChon) {
			SqlParameter sqlParamArrUserIDD = new SqlParameter("@ArrUserIDD", SqlDbType.Structured) { Value = TableUserIDD };
			DataTable tableExcludeNV = MyUtility.Array_To_DataTable("TableExcludeNV", dsExcludeMaCC);
			DataTable tableArrMaNhiemVu = MyUtility.Array_To_DataTable("TableMaNhiemVu", dsNhiemVuDangChon);
			DataTable tableNhanVien = SqlDataAccessHelper.ExecSPQuery(SPName.sp_UserInfo_DocDSNVThongKeCongVaPC.ToString(),
				sqlParamArrUserIDD,
				new SqlParameter("@ArrExcludeMaCC", SqlDbType.Structured) { Value = tableExcludeNV },
				new SqlParameter("@ArrMaNhiemVu", SqlDbType.Structured) { Value = tableArrMaNhiemVu });
			return tableNhanVien;
		}

		public static void TachKC_ChuaKC(List<List<DateTime>> arrDoanThoigian, out List<List<DateTime>> arrDoanTGChuaKetCong, out List<List<DateTime>> arrDoanTGDaKetCong) {
			arrDoanTGDaKetCong = new List<List<DateTime>>();
			arrDoanTGChuaKetCong = new List<List<DateTime>>();
			for (int i = 0; i < arrDoanThoigian.Count; i++) {
				List<DateTime> x = arrDoanThoigian[i];
				DateTime ngaydauthang = MyUtility.FirstDayOfMonth(x[0]);
				SqlParameter paramNgayDauThang = new SqlParameter { ParameterName = "@NgayDauThang", Value = ngaydauthang };
				SqlParameter outParamKetQua = new SqlParameter { Direction = ParameterDirection.ReturnValue };
				SqlDataAccessHelper.ExecSPQuery(SPName.sp_KiemTraKetLuongThang.ToString(), paramNgayDauThang, outParamKetQua);
				int kq = (int)outParamKetQua.Value;
				if (kq == 1) arrDoanTGDaKetCong.Add(x);
				else arrDoanTGChuaKetCong.Add(x);
			}
		}

		public static void ChiaDoanThoiGian(DateTime NgayBd, DateTime NgayKt, out List<List<DateTime>> ArrDoanThoigian) {
			ArrDoanThoigian = new List<List<DateTime>>();
			DateTime cursorDate = new DateTime(NgayBd.Year, NgayBd.Month, NgayBd.Day);

			while (cursorDate.Month < NgayKt.Month && cursorDate.Year <= NgayKt.Year) // vẫn chưa phải tháng cuối cùng trong khoảng thời gian
			{
				DateTime ngayBD_Doan, ngayKT_Doan;
				ngayBD_Doan = new DateTime(cursorDate.Year, cursorDate.Month, cursorDate.Day); // ngày đầu tháng bị dang dở hoặc ngày 1 đầu tháng
				ngayKT_Doan = MyUtility.LastDayOfMonth(ngayBD_Doan);// ngày cuối tháng
				ArrDoanThoigian.Add(new List<DateTime> { ngayBD_Doan, ngayKT_Doan });
				cursorDate = MyUtility.FirstDayOfMonth(cursorDate).AddMonths(1); // đưa ngày đầu dang dở về đầu tháng rồi mới add thêm 1 tháng mới. VD: 16 -> 1 rồi mới add monnth
			}
			// ra khỏi vòng lặp là tháng cuối có thể bị dang dở cursorDate.Month = NgayKT.Month . VD: 01/01/2015 -16/01/2015
			ArrDoanThoigian.Add(new List<DateTime> { cursorDate, NgayKt });
		}

		private void LayDSExcludeNV(ref List<int> dsExcludeNv) {
			if (listExcludeNV.DataSource == null) return;
			DataTable tableExcludeNV = listExcludeNV.DataSource as DataTable;
			if (tableExcludeNV == null || tableExcludeNV.Columns.Count == 0 || tableExcludeNV.Rows.Count == 0) return;

			dsExcludeNv.AddRange(from DataRow row in tableExcludeNV.Rows select (int)row["UserEnrollNumber"]);
		}

		private void LayDSNhiemVuDangChon(ref List<int> dsNhiemVuDangChon) {
			if (checkedListNhiemVu.CheckedItems.Count == 0) return;

			dsNhiemVuDangChon.AddRange(from DataRowView rowView in checkedListNhiemVu.CheckedItems.Cast<DataRowView>() select (int)rowView["MaNhiemVu"]);
		}

		private void XacDinhKhoangThoiGianChon(out DateTime ngayBd, out DateTime ngayKt) {
			if (radDate.Checked) {
				ngayBd = dtpNgayBD.Value;
				ngayKt = dtpNgayKT.Value;
			}
			else if (radMonth.Checked) {
				ngayBd = MyUtility.FirstDayOfMonth(dtpThang.Value);
				ngayKt = MyUtility.LastDayOfMonth(dtpThang.Value);
			}
			else if (radQuarter.Checked) {
				int quy = (int)numQuy.Value;
				int nam = dtpQuyNam.Value.Year;
				int thangBD_Value = (3 * (quy - 1)) + 1;
				int thangKT_Value = thangBD_Value + 2;// BD: 1;4;7;10; KT: 3;6;9;12
				ngayBd = new DateTime(nam, thangBD_Value, 1);  // nam 2015 thang 10 ngay 1
				ngayKt = new DateTime(nam, thangKT_Value, DateTime.DaysInMonth(nam, thangKT_Value)); //2015 thang 12 ngay 31
			}
			else {
				ngayBd = DateTime.Today;
				ngayKt = DateTime.Today;
			}
		}

		private void btnAddExcludeNV_Click(object sender, EventArgs e) {
			fmExcludeNV frm = new fmExcludeNV();
			frm.mListMaPhong = m_listIDPhongBan;
			frm.ShowDialog();
			if (frm.kq == YesNoCancel.Cancel || frm.kq == YesNoCancel.No) return;
			else if (frm.kq == YesNoCancel.Yes) {
				List<DataRow> checkedDataRow = frm.mCheckedRow; // luôn có phần tử
				//kiểm tra cái nào trùng thì bỏ qua, cái nào chưa có thì add thêm, vì mỗi lần tạo form mới nên ko check obj với obj mà check data với data
				//1. get datasource của listbox, nếu null thì tạo mới, nếu đã có thì get datasource đó để kiểm tra
				//datasource của listBox là listdatarow chứ ko phải datatable
				var source = listExcludeNV.DataSource as DataTable; //source ko cần check null vì ở hàm formload đã gán sẵn datasource
				//đã có, check từng thông tin
				foreach (DataRow row in checkedDataRow) {
					if (source.Rows.Cast<DataRow>().Any(item => (int)item["UserEnrollNumber"] == (int)row["UserEnrollNumber"]) == false) {
						var tempRow = source.NewRow();
						tempRow["UserEnrollNumber"] = (int)row["UserEnrollNumber"];
						tempRow["UserFullName"] = row["UserFullName"].ToString();
						source.Rows.Add(tempRow);
					}
				}

			}
		}

		private void radDate_CheckedChanged(object sender, EventArgs e) {
			MyUtility.EnableDisableControl(radDate.Checked, dtpNgayBD, dtpNgayKT);
			MyUtility.EnableDisableControl(!radDate.Checked, dtpThang, dtpQuyNam, numQuy);

		}

		private void radMonth_CheckedChanged(object sender, EventArgs e) {
			MyUtility.EnableDisableControl(radMonth.Checked, dtpThang);
			MyUtility.EnableDisableControl(!radMonth.Checked, dtpNgayBD, dtpNgayKT, dtpQuyNam, numQuy);

		}

		private void radQuarter_CheckedChanged(object sender, EventArgs e) {
			MyUtility.EnableDisableControl(radQuarter.Checked, dtpQuyNam, numQuy);
			MyUtility.EnableDisableControl(!radQuarter.Checked, dtpNgayBD, dtpNgayKT, dtpThang);

		}

		private void btnRemoveExcludeNV_Click(object sender, EventArgs e) {
			DataTable source = listExcludeNV.DataSource as DataTable;
			if (source == null) return;
			List<DataRowView> removeRow = (from DataRowView rowView in listExcludeNV.SelectedItems.Cast<DataRowView>() select rowView).ToList();
			foreach (DataRowView rowView in removeRow) {
				source.Rows.Remove(rowView.Row);
			}
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			Close();
		}

	}
}
