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

			int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.Schedule_ThemLichTrinhV6.ToString(),
				new SqlParameter("@SchName", frm.m_TenLichTrinh),
				new SqlParameter("@InOutID", 2));
			if (kq == 0) {
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
				return;
			}
			LoadDSLichTrinh();

		}

		private void btnXoaLichTrinh_Click(object sender, EventArgs e) {
			if (MessageBox.Show("Bạn muốn xóa các lịch trình đã chọn?", Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}
			//nếu có check lịch trình thì xóa
			int[] selRowsHandle = ((GridView)gridControlLichTrinh.MainView).GetSelectedRows();
			if (selRowsHandle.Count() == 0)
			{
				ACMessageBox.Show("Bạn chưa chọn lịch trình cần xóa.", Resources.Caption_ThongBao, 2000);
				return;
			}
			List<int> listIDLichTrinh = new List<int>();
			foreach (int rowHandle in selRowsHandle) {
				//DataRowView selectedDataRowView =(DataRowView) ((GridView) gridControlLichTrinh.MainView).GetRow(selRowsHandle[0]);
				DataRow selectedDataRow = gridViewLichTrinh.GetDataRow(rowHandle);
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
			// thực hiện xóa lịch trình => LOGIC xóa lịch trình cũng đồng nghĩa xóa luôn các ca làm việc theo lịch trình này.
			foreach (int id in ListIDLichTrinh) {
				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.Other_XoaLichTrinhV6.ToString(), new SqlParameter("@SchID", id));
				if (kq == 0) {
					ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
					return;
				}
			}
		}

		private void btnThemCa_Click(object sender, EventArgs e) {
			// hiển thị form chọn ca, nếu chế độ cancel thì thoát
			frmChonCa frm = new frmChonCa();
			frm.ShowDialog();
			if (frm.m_Mode == ModeType.Cancel) return;

			// chế độ thêm, kiểm tra kết nối csdl trước
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			// xác định ScheduleID của Row đang FOCUS
			DataRowView selectedDataRowView = (DataRowView)((GridView)gridControlLichTrinh.MainView).GetFocusedRow();
			if (selectedDataRowView == null) return; // ko có focus vào row nào thì thoát
			DataRow selectedDataRow = selectedDataRowView.Row;
			var idLichTrinh = (int)selectedDataRow["SchID"];

			List<int> listID_SelectedShift = (from DataRow dataRow in frm.m_SelectedRows select (int)dataRow["ShiftID"]).ToList();
			if (listID_SelectedShift.Count == 0) return; // ko chọn item nào thì thoát

			//hỏi và Thực hiện thêm shiftID vào lịch trình
			string templateString = "Bạn muốn thêm danh sách ca cho lịch trình [{0}]?";
			string resultString = string.Format(templateString, selectedDataRow["SchName"]);
			if (MessageBox.Show(resultString, Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No)
			{
				return;
			}

			foreach (int shiftID in listID_SelectedShift) {
				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.ShiftSch_ThemDSCaVaoLichTrinhV6.ToString(),
										 new SqlParameter("@SchID", idLichTrinh),
										 new SqlParameter("@ShiftID", shiftID));
				if (kq == 0) {
					ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
					break;
				}
			}

			//reload lịch trình
			LoadDSLichTrinh();
		}

		private void btnXoaCa_Click(object sender, EventArgs e) {
			// LOGIC xóa ca thì xóa danh sách ca đang hiển thị của FOCUS_ROW chứ ko xóa của CHECK_ROW
			//kiểm tra kết nối trước khi xóa
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			//  Xác định lịch trình sẽ xóa ca và hỏi lại trước khi xóa
			DataRowView focusRowHandle = (DataRowView)((GridView)gridControlLichTrinh.MainView).GetFocusedRow();
			if (focusRowHandle == null) return; //ko có dòng focus, ko có dòng check => dữ liệu trống => thoát


			string scheduleName = focusRowHandle.Row["SchName"].ToString();
			int scheduleID = (int)focusRowHandle.Row["SchID"];
			string templateString = "Bạn muốn xóa các ca này khỏi lịch trình [{0}]?";
			string noidungThongbao = string.Format(templateString, scheduleName);
			if (MessageBox.Show(noidungThongbao, Resources.Caption_XacNhan) == DialogResult.No) {
				return;
			}

			// xác định các ca được check phải xóa
			int[] checkedRowsHandle_Ca = ((GridView)gridControlCa.MainView).GetSelectedRows();

			List<int> listID_SelectedShift = new List<int>();
			foreach (int rowHandle in checkedRowsHandle_Ca) {
				DataRow dataRow_Ca = gridViewCa.GetDataRow(rowHandle);
				listID_SelectedShift.Add((int)dataRow_Ca["T1"]); //T1 tương đương shiftID
			}

			// thực hiện xóa ca
			foreach (int id in listID_SelectedShift) {
				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.ShiftSch_XoaDSCaKhoiLichTrinhV6.ToString(),
															 new SqlParameter("@SchID", scheduleID),
															 new SqlParameter("@ShiftID", id));
				if (kq == 0) {
					ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
					break;
				}
			}
			//reload lại grid
			LoadDSLichTrinh();
		}

		public void LoadDSLichTrinh() {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			DataTable table = SqlDataAccessHelper.ExecSPQuery(SPName6.Schedule_DocLichTrinhV6.ToString());
			gridControlLichTrinh.DataSource = table;

		}

		private void GridView1OnFocusedRowChanged(object sender, FocusedRowChangedEventArgs focusedRowChangedEventArgs) {
			DataRow dataRow = gridViewLichTrinh.GetDataRow(focusedRowChangedEventArgs.FocusedRowHandle);
			if (dataRow == null) return;
			int schID = (int)dataRow["SchID"];
			DataTable tableDSCa = SqlDataAccessHelper.ExecSPQuery(SPName6.ShiftSch_DocDSCaV6.ToString(),
													  new SqlParameter("@SchID", schID));

			gridControlCa.DataSource = tableDSCa;


		}



		private void frmQLLichTrinh_Load(object sender, EventArgs e) {
			gridViewLichTrinh.FocusedRowChanged += GridView1OnFocusedRowChanged;

			LoadDSLichTrinh();
		}
	}
}
