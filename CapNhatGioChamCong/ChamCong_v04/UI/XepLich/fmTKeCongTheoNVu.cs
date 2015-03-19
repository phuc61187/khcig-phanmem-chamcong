using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v04.BUS;
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
			numQuy.Value = (DateTime.Today.Month - (DateTime.Today.Month%4) / 4);
			int thangDauQuy = (int) numQuy.Value;
			dtpQuyNam.Value = new DateTime(DateTime.Today.Year,thangDauQuy, 1);

			//load tree
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
			templateTable.Columns.Add(new DataColumn {ColumnName = "UserEnrollNumber", DataType = typeof (int)});
			templateTable.Columns.Add(new DataColumn {ColumnName = "UserFullName", DataType = typeof (string)});
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

		private void radioButton_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton checkedRad = (RadioButton) sender;
			if (checkedRad == radDate)
			{
				dtpNgayBD.Enabled = dtpNgayKT.Enabled = checkedRad.Checked;
				dtpThang.Enabled = !checkedRad.Enabled;
				dtpQuyNam.Enabled = !checkedRad.Checked;
			}
			else if (checkedRad == radMonth)
			{
				dtpNgayBD.Enabled = dtpNgayKT.Enabled = !checkedRad.Checked;
				dtpThang.Enabled = checkedRad.Enabled;
				dtpQuyNam.Enabled = !checkedRad.Checked;
			}
			else if (checkedRad == radQuarter)
			{
				dtpNgayBD.Enabled = dtpNgayKT.Enabled = !checkedRad.Checked;
				dtpThang.Enabled = !checkedRad.Enabled;
				dtpQuyNam.Enabled = checkedRad.Checked;
			}
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

			DataTable tableThongke = this.ThongKeCong_PC_Phep(ngayBD, ngayKT, m_listIDPhongBan, dsExcludeMaCC, dsNhiemVuDangChon);
			dgrdThongKe.DataSource = tableThongke;
		}

		private DataTable ThongKeCong_PC_Phep(DateTime ngayBD, DateTime ngayKT, List<int> m_listIDPhongBan, List<int> dsExcludeMaCC, List<int> dsNhiemVuDangChon) {
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
			return new DataTable();
		}

		public static void TachKC_ChuaKC(List<List<DateTime>> arrDoanThoigian,out List<List<DateTime>> arrDoanTGChuaKetCong, out List<List<DateTime>> arrDoanTGDaKetCong) {
			arrDoanTGDaKetCong = new List<List<DateTime>>();
			arrDoanTGChuaKetCong = new List<List<DateTime>>();
			for (int i = 0; i < arrDoanThoigian.Count; i++)
			{
				List<DateTime> x = arrDoanThoigian[i];
				DateTime ngaydauthang = MyUtility.FirstDayOfMonth(x[0]);
				SqlParameter paramNgayDauThang = new SqlParameter{ParameterName = "@NgayDauThang", Value = ngaydauthang};
				SqlParameter outParamKetQua = new SqlParameter{Direction = ParameterDirection.ReturnValue};
				SqlDataAccessHelper.ExecSPQuery(SPName.sp_KiemTraKetLuongThang.ToString(), paramNgayDauThang, outParamKetQua);
				int kq = (int)outParamKetQua.Value;
				if (kq == 1) arrDoanTGDaKetCong.Add(x);
				else arrDoanTGChuaKetCong.Add(x);
			}
		}

		public static void ChiaDoanThoiGian(DateTime NgayBd, DateTime NgayKt, out List<List<DateTime>> ArrDoanThoigian)
		{
			ArrDoanThoigian = new List<List<DateTime>>();
			DateTime cursorDate = new DateTime(NgayBd.Year,NgayBd.Month, NgayBd.Day);

			while (cursorDate.Month < NgayKt.Month && cursorDate.Year <= NgayKt.Year) // vẫn chưa phải tháng cuối cùng trong khoảng thời gian
			{
				DateTime ngayBD_Doan, ngayKT_Doan;
				ngayBD_Doan = new DateTime(cursorDate.Year,cursorDate.Month, cursorDate.Day); // ngày đầu tháng bị dang dở hoặc ngày 1 đầu tháng
				ngayKT_Doan = MyUtility.LastDayOfMonth(ngayBD_Doan);// ngày cuối tháng
				ArrDoanThoigian.Add(new List<DateTime>{ngayBD_Doan, ngayKT_Doan});
				cursorDate = MyUtility.FirstDayOfMonth(cursorDate).AddMonths(1); // đưa ngày đầu dang dở về đầu tháng rồi mới add thêm 1 tháng mới. VD: 16 -> 1 rồi mới add monnth
			}
			// ra khỏi vòng lặp là tháng cuối có thể bị dang dở cursorDate.Month = NgayKT.Month . VD: 01/01/2015 -16/01/2015
			ArrDoanThoigian.Add(new List<DateTime>{cursorDate, NgayKt});
		}

		private void LayDSExcludeNV(ref List<int> dsExcludeNv)
		{
			if (listExcludeNV.DataSource == null) return;
			DataTable tableExcludeNV = listExcludeNV.DataSource as DataTable;
			if (tableExcludeNV == null || tableExcludeNV.Columns.Count == 0 || tableExcludeNV.Rows.Count == 0) return;

			dsExcludeNv.AddRange(from DataRow row in tableExcludeNV.Rows select (int) row["UserEnrollNumber"]);
		}

		private void LayDSNhiemVuDangChon(ref List<int> dsNhiemVuDangChon)
		{
			if (checkedListNhiemVu.CheckedItems.Count == 0) return;

			dsNhiemVuDangChon.AddRange(from DataRowView rowView in checkedListNhiemVu.CheckedItems.Cast<DataRowView>() select (int)rowView["MaNhiemVu"]);
		}

		private void XacDinhKhoangThoiGianChon(out DateTime ngayBd, out DateTime ngayKt)
		{
			if (radDate.Checked)
			{
				ngayBd = dtpNgayBD.Value;
				ngayKt = dtpNgayKT.Value;
			}
			else if (radMonth.Checked)
			{
				ngayBd = MyUtility.FirstDayOfMonth(dtpThang.Value);
				ngayKt = MyUtility.LastDayOfMonth(dtpThang.Value);
			}
			else if (radQuarter.Checked)
			{
				int quy = (int) numQuy.Value;
				int nam = dtpQuyNam.Value.Year;
				int thangBD_Value = (3*(quy - 1)) + 1;
				int thangKT_Value = thangBD_Value + 2;// BD: 1;4;7;10; KT: 3;6;9;12
				ngayBd = new DateTime(nam, thangBD_Value,1);  // nam 2015 thang 10 ngay 1
				ngayKt = new DateTime(nam, thangKT_Value, DateTime.DaysInMonth(nam,thangKT_Value)); //2015 thang 12 ngay 31
			}
			else
			{
				ngayBd = DateTime.Today;
				ngayKt = DateTime.Today;
			}
		}
	}
}
