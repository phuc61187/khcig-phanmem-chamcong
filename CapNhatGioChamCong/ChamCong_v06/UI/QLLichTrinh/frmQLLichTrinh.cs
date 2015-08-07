using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v06.DAL;
using ChamCong_v06.Helper;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace ChamCong_v06.UI.QLLichTrinh {
	public partial class frmQLLichTrinh : Form {
		#region hàm ko quan trọng
		public frmQLLichTrinh() {
			InitializeComponent();
		}

		private void btnDong_Click(object sender, EventArgs e) {
			Close();
		}
		#endregion

		private void btnThemLichTrinh_Click(object sender, EventArgs e) {
			frmTTLichTrinh frm = new frmTTLichTrinh();
			frm.Mode = ModeType.Them;
			frm.ShowDialog();
			if (frm.Mode == ModeType.Cancel) return;

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.Schedule_ThemLichTrinh.ToString(), 
				new SqlParameter("@SchName", frm.m_TenLichTrinh),
				new SqlParameter("@InOutID", 2));
			if (kq == 0)
			{
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
				return;
			}
			LoadDSLichTrinh();
			
		}

		private void btnXoaLichTrinh_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Bạn muốn xóa lịch trình này?", Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No)
			{
				return;
			}
			int[] selRowsHandle = ((GridView) gridControlLichTrinh.MainView).GetSelectedRows();
			List<int> listIDLichTrinh = new List<int>();
			foreach (int rowHandle in selRowsHandle)
			{
				//DataRowView selectedDataRowView =(DataRowView) ((GridView) gridControlLichTrinh.MainView).GetRow(selRowsHandle[0]);
				DataRow selectedDataRow = gridViewCa.GetDataRow(rowHandle);
				var id = (int)selectedDataRow["SchID"];
				listIDLichTrinh.Add(id);
			}

			XuLyXoaLichTrinh(listIDLichTrinh);
			LoadDSLichTrinh();
		}

		private void XuLyXoaLichTrinh(List<int> ListIDLichTrinh) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			foreach (int id in ListIDLichTrinh)
			{
				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.Other_XoaLichTrinh.ToString(), new SqlParameter("@SchID", id));
				if (kq == 0) {
					ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
					return;
				}
			}
		}

		private void btnThemCa_Click(object sender, EventArgs e) {

			frmChonCa frm = new frmChonCa();
			frm.ShowDialog();
			if (frm.m_Mode == ModeType.Cancel) return;

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			int[] selRows = ((GridView)gridControlLichTrinh.MainView).GetSelectedRows();
			DataRowView selectedDataRowView = (DataRowView)((GridView)gridControlLichTrinh.MainView).GetRow(selRows[0]);
			DataRow selectedDataRow = selectedDataRowView.Row;
			var idLichTrinh = (int)selectedDataRow["SchID"];

			List<int> listID_SelectedShift = (from DataRow dataRow in frm.m_SelectedRows select (int) dataRow["ShiftID"]).ToList();

			foreach (int id in listID_SelectedShift)
			{
				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.ShiftSch_ThemDSCaVaoLichTrinh.ToString(),
				                                             new SqlParameter("@SchID", idLichTrinh),
				                                             new SqlParameter("@ShiftID", id));
				if (kq == 0)
				{
					ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
					break;
				}
			}

			LoadDSLichTrinh();
		}

		private void btnXoaCa_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Bạn muốn xóa ca này khỏi lịch trình?", Resources.Caption_XacNhan) == DialogResult.No)
			{
				return;
			}

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			int[] selRowsHandle_LichTrinh = ((GridView)gridControlLichTrinh.MainView).GetSelectedRows();
			DataRowView selectedDataRowView_LichTrinh = (DataRowView)((GridView)gridControlLichTrinh.MainView).GetRow(selRowsHandle_LichTrinh[0]);
			DataRow selectedDataRow_LichTrinh = selectedDataRowView_LichTrinh.Row;
			var idLichTrinh = (int)selectedDataRow_LichTrinh["SchID"];

			int[] selectedRowsHandle_Ca = ((GridView)gridControlLichTrinh.MainView).GetSelectedRows();

			List<int> listID_SelectedShift = new List<int>();
			foreach (int rowHandle in selectedRowsHandle_Ca)
			{
				DataRow dataRow_Ca = gridViewCa.GetDataRow(rowHandle);
				listID_SelectedShift.Add((int)dataRow_Ca["ShiftID"]);
			}

			foreach (int id in listID_SelectedShift) {
				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.ShiftSch_ThemDSCaVaoLichTrinh.ToString(),
															 new SqlParameter("@SchID", idLichTrinh),
															 new SqlParameter("@ShiftID", id));
				if (kq == 0) {
					ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
					break;
				}
			}

		}

		public void LoadDSLichTrinh()
		{
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			DataTable table = SqlDataAccessHelper.ExecSPQuery(SPName6.Schedule_DocLichTrinh.ToString());
			gridControlLichTrinh.DataSource = table;
			
		}

		private void GridView1OnFocusedRowChanged(object sender, FocusedRowChangedEventArgs focusedRowChangedEventArgs)
		{
			if (focusedRowChangedEventArgs.FocusedRowHandle == GridControl.InvalidRowHandle)
			{
				return;
			}
			DataRow dataRow = gridViewLichTrinh.GetDataRow(focusedRowChangedEventArgs.FocusedRowHandle);
			int schID = (int) dataRow["SchID"];
			DataTable tableDSCa = SqlDataAccessHelper.ExecSPQuery(SPName6.ShiftSch_DocDSCa.ToString(),
													  new SqlParameter("@SchID", schID));

			gridControlCa.DataSource = tableDSCa;
			

		}



		private void frmQLLichTrinh_Load(object sender, EventArgs e) {
			gridViewLichTrinh.FocusedRowChanged += GridView1OnFocusedRowChanged;

			LoadDSLichTrinh();
		}
	}
}
