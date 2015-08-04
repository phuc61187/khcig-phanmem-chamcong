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
using DevExpress.XtraGrid.Views.Grid;

namespace ChamCong_v06.UI.QLLichTrinh {
	public partial class frmQLCa : Form {
		#region các hàm ko quan trọng

		public frmQLCa() {
			InitializeComponent();
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			Close();
		}

		#endregion

		private void frmQLCa_Load(object sender, EventArgs e) {
			LoadGrid();
		}

		public void LoadGrid() {
			//kiem tra ket noi csdl

			DataTable tableShift = DAO5.LoadDataSourceShift();
			gridControl.DataSource = tableShift;
		}

		private void btnThem_Click(object sender, EventArgs e) {
			frmTTCaLamViec frm = new frmTTCaLamViec();
			frm.Mode = ModeType.Them;
			frm.ShowDialog();
			if (frm.Mode == ModeType.Cancel) return;

			#region

			string tempplate = @"ShiftCode:{0};
ShiftID:{1};
Enable:{2};
OnDuty:{3};
OffDuty:{4};
WorkingTime:{5};
Workingday:{6};
LateGrace:{7};
EarlyGrace:{8};
AfterOT:{9};
OnTimeIn:{10};
CutIn:{11};
OnTimeOut:{12};
CutOut:{13};
KyHieuCC:{14};
OnLunch:{15};
OffLunch:{16};
";
			MessageBox.Show(string.Format(tempplate,
										  frm.m_ShiftCode,
										  frm.m_ShiftID,
										  frm.m_Enable,
										  frm.m_OnDuty.ToString(@"hh\:mm"),
										  frm.m_OffDuty.ToString(@"hh\:mm"),
										  (frm.m_OffDuty - frm.m_OnDuty).ToString(@"hh\:mm"),
										  frm.m_ChamCong,
										  frm.m_LateGraceMin,
										  frm.m_EarlyGraceMin,
										  frm.m_AfterOTMin,
										  frm.m_OnTimeInMin,
										  frm.m_CutInMin,
										  frm.m_OnTimeOutMin,
										  frm.m_CutOutMin,
										  frm.m_KyHieuCC,
										  frm.m_OnLunch.ToString(@"hh\:mm"),
										  frm.m_OffLunch.ToString(@"hh\:mm")
								));

			#endregion

			//sử dụng kết quả trả về thêm vào CSDL

			XuLyInsert(frm);

			LoadGrid();
		}

		private void btnSua_Click(object sender, EventArgs e) {
			int[] selRows = ((GridView)gridControl.MainView).GetSelectedRows();
			DataRowView selRow = (DataRowView)(((GridView)gridControl.MainView).GetRow(selRows[0]));
			DataRow selectedRow = selRow.Row;
			frmTTCaLamViec frm = new frmTTCaLamViec();
			frm.m_CurrentRow = selectedRow;
			frm.Mode = ModeType.Sua;
			frm.ShowDialog();
			if (frm.Mode == ModeType.Cancel) return;

			#region

			string tempplate = @"ShiftCode:{0};
ShiftID:{1};
Enable:{2};
OnDuty:{3};
OffDuty:{4};
WorkingTime:{5};
Workingday:{6};
LateGrace:{7};
EarlyGrace:{8};
AfterOT:{9};
OnTimeIn:{10};
CutIn:{11};
OnTimeOut:{12};
CutOut:{13};
KyHieuCC:{14};
OnLunch:{15};
OffLunch:{16};
";
			MessageBox.Show(string.Format(tempplate,
										  frm.m_ShiftCode,
										  frm.m_ShiftID,
										  frm.m_Enable,
										  frm.m_OnDuty.ToString(@"hh\:mm"),
										  frm.m_OffDuty.ToString(@"hh\:mm"),
										  (frm.m_OffDuty - frm.m_OnDuty).ToString(@"hh\:mm"),
										  frm.m_ChamCong,
										  frm.m_LateGraceMin,
										  frm.m_EarlyGraceMin,
										  frm.m_AfterOTMin,
										  frm.m_OnTimeInMin,
										  frm.m_CutInMin,
										  frm.m_OnTimeOutMin,
										  frm.m_CutOutMin,
										  frm.m_KyHieuCC,
										  frm.m_OnLunch.ToString(@"hh\:mm"),
										  frm.m_OffLunch.ToString(@"hh\:mm")
								));

			#endregion

			//todo hỏi lại và thực hiện xuống csdl. làm phần tính toán lại toàn bộ dữ liệu hoặc chuyển sang nhân bản mới

			XuLyUpdate(frm);
		}

		private void btnXoa_Click(object sender, EventArgs e) {
			if (MessageBox.Show("Bạn muốn XÓA ca làm việc? " +
								"Xóa ca làm việc sẽ xóa toàn bộ dữ liệu chấm công liên quan đến ca làm việc này." +
								"Cân nhắc sử dụng Enable/Disable ca làm việc." +
								"Bấm YES để tiếp tục, No để hủy thao tác.", Resources.Caption_XacNhan) == DialogResult.No)
				return;

			int[] selRows = ((GridView)gridControl.MainView).GetSelectedRows();
			DataRowView selRow = (DataRowView)(((GridView)gridControl.MainView).GetRow(selRows[0]));
			DataRow selectedRow = selRow.Row;

			var shiftID = (int)selectedRow["ShiftID"];
			//sử dụng kết quả trả về thêm vào CSDL
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			//todo thực hiện kiểm tra tất cả các bảng có sử dụng shiftID, nếu còn thì ko cho xóa
			// nếu ko còn mới cho xóa
			if (TODOKiemTra() == false) {
			}
			else {
				int kq = SqlDataAccessHelper.ExecSPNoneQuery(SPName6.Shift_DeleteShift.ToString(),
															 new SqlParameter(@"ShiftID", shiftID));
				if (kq == 0) {
					ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
					return;
				}
				LoadGrid();
			}

		}

		private void XuLyInsert(frmTTCaLamViec frm) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 3000);
				return;
			}

			bool disableCSDL = !frm.m_Enable;
			TimeSpan workingTime = (frm.m_OffDuty - frm.m_OnDuty);
			int workingTimeMin = Convert.ToInt32(workingTime.TotalMinutes);
			SqlParameter[] param = new SqlParameter[]
				{
					new SqlParameter("@ShiftCode", frm.m_ShiftCode),
			                              new SqlParameter("@Disable", disableCSDL),
			                              new SqlParameter("@OnDuty", frm.m_OnDuty.ToString(@"hh\:mm")),
			                              new SqlParameter("@OffDuty", frm.m_OffDuty.ToString(@"hh\:mm")),
			                              new SqlParameter("@DayCount", frm.m_DayCount),
			                              new SqlParameter("@WorkingTimeMin", workingTimeMin),
			                              new SqlParameter("@Workingday", frm.m_ChamCong),
			                              new SqlParameter("@LateGrace", frm.m_LateGraceMin),
			                              new SqlParameter("@EarlyGrace", frm.m_EarlyGraceMin),
			                              new SqlParameter("@AfterOT", frm.m_AfterOTMin),
			                              new SqlParameter("@OnTimeIn", frm.m_OnTimeInMin),
			                              new SqlParameter("@CutIn", frm.m_CutInMin),
			                              new SqlParameter("@OnTimeOut", frm.m_OnTimeOutMin),
			                              new SqlParameter("@CutOut", frm.m_CutOutMin),
			                              new SqlParameter("@KyHieuCC", frm.m_KyHieuCC),
			                              new SqlParameter("@OnLunch", frm.m_OnLunch.ToString(@"hh\:mm")),
			                              new SqlParameter("@OffLunch", frm.m_OffLunch.ToString(@"hh\:mm"))
				};
			int kq = (SqlDataAccessHelper.ExecSPNoneQuery(SPName6.Shift_InsertNewShift.ToString(), param));
			if (kq == 0) {
				ACMessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, 2000);
			}
		}

		private bool TODOKiemTra()
		{
			return true;
		}
		
		private void XuLyUpdate(frmTTCaLamViec frm) {
			/*todo thực hiện 1 . nếu thay đổi các thông số liên quan đến chấm công thì tạo ca mới, 
			 * cập nhật các chấm công của tháng chưa áp dụng, các tháng trước vẫn giữ nguyên
			 * 
			 */
		
		}




	}
}
