using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;

namespace ChamCong_v04.UI.TinhLuong {
	public partial class frmHuyKetLuongThang : Form {
		public List<cUserInfo> m_DSNV = new List<cUserInfo>();
		public List<int> m_listIDPhongBan = new List<int>();

		#region hàm ko quan trong

		public frmHuyKetLuongThang()
		{
			InitializeComponent();
		}

		private void btnDong_Click(object sender, EventArgs e)
		{
			Close();
		}

		#endregion


		private void frm_KetCongBoPhan_Load(object sender, EventArgs e) {
			//set default tháng đang chọn là trước 1 tháng so với hiện tại vì qua tháng mới kết công
			var firstDay_CurrMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
			var firstDay_PrevMonth = firstDay_CurrMonth.AddMonths(-1);
			dtpThang.Value = new DateTime(firstDay_PrevMonth.Year, firstDay_PrevMonth.Month, firstDay_PrevMonth.Day);
		}


		private void btnThucHien_Click(object sender, EventArgs e)
		{
			#region kiểm tra kết nối csdl , nếu mất kết nối thì đóng

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}

			#endregion

			if (XL.Kiemtra(MyUtility.FirstDayOfMonth(dtpThang.Value), MyUtility.LastDayOfMonth(dtpThang.Value)) == false)
			{
				ACMessageBox.Show(string.Format("Tháng {0} chưa thực hiện kết lương.", dtpThang.Value.ToString("MM/yyyy")), Resources.Caption_ThongBao, 3000);
				return;
			}

			if (MessageBox.Show("Bạn muốn huỷ kết lương tháng " + dtpThang.Value.ToString("MM/yyyy"), Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No)
			{
				return;
			}

			if (XL.HuyKetLuongThang(MyUtility.FirstDayOfMonth(dtpThang.Value))) {
				ACMessageBox.Show("Đã huỷ kết lương tháng " + dtpThang.Value.ToString("MM/yyyy"), Resources.Caption_ThongBao, 3000);
			}
			else
			{
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
			
			Close();
		}


	}
}
