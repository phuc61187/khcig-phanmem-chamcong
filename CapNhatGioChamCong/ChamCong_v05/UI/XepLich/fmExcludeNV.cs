using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v05.Helper;

namespace ChamCong_v05.UI.XepLich {
	public partial class fmExcludeNV : Form {
		public List<int> mListMaPhong = new List<int>();
		public YesNoCancel kq = YesNoCancel.Cancel;
		public List<DataRow> mCheckedRow = new List<DataRow>();

		public fmExcludeNV() {
			InitializeComponent();

			dgrdDSNVTrgPhg.AutoGenerateColumns = false;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			kq = YesNoCancel.Cancel;
			Close();
		}

		private void fmExcludeNV_Load(object sender, EventArgs e)
		{
			DataTable tableArrarMaPhong = MyUtility.Array_To_DataTable("TableArrayMaPhong", mListMaPhong);
			DataTable tableNhanVien = SqlDataAccessHelper.ExecSPQuery(SPName.sp_UserInfo_DocDSNVThaoTac.ToString(),
			                                                          new SqlParameter("@ArrUserIDD", SqlDbType.Structured) {Value = tableArrarMaPhong});

			#region tạo datasourcr cho autocomplete

			var Source = new AutoCompleteStringCollection();
			Source.AddRange((from DataRow row in tableNhanVien.Rows select row["UserLastName"].ToString()).ToArray());
			tbSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tbSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			tbSearch.AutoCompleteCustomSource = Source;

			#endregion

			dgrdDSNVTrgPhg.DataSource = new DataView(tableNhanVien);
		}

		#region tìm kiếm

		private void btnTim_Click(object sender, EventArgs e)
		{
			if (dgrdDSNVTrgPhg.DataSource == null) return;

			var searchStr1 = tbSearch.Text;
			var searchStr = string.Format("UserFullName like '%{0}%' or UserFullCode like '%{0}%'", searchStr1);
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = searchStr;
		}

		private void linkHienThiTatCaNV_Click(object sender, EventArgs e)
		{
			var dataView = dgrdDSNVTrgPhg.DataSource as DataView;
			if (dataView != null) dataView.RowFilter = string.Empty;
		}

		private void tbSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				btnTim.PerformClick();
		}
		
		#endregion

		private void btnDongY_Click(object sender, EventArgs e)
		{
			//validate view
			DataView view = dgrdDSNVTrgPhg.DataSource as DataView;
			if (view == null)
			{
				kq = YesNoCancel.Cancel;
				Close();
				return;
			}

			var checkedRowView = (from DataRow row in view.Table.Rows where row["check"] != DBNull.Value && (bool) row["check"] select row).ToList();
			if (!checkedRowView.Any()) // nếu ko check dấu nào thì đóng, chế độ cancel
			{
				kq = YesNoCancel.Cancel;
				Close();
				return;
			}
			
			//add các checkedRow, đóng form chế độ đồng ý
			mCheckedRow.AddRange(checkedRowView);
			kq = YesNoCancel.Yes;
			this.Close();
		}


	}
}
