using System;
using System.Data;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.DAL;
using ChamCong_v05.Helper;
using ChamCong_v05.Properties;

namespace ChamCong_v05.UI.Admin {
	public partial class frmSetupShift : Form {
		bool themmoi = false;
		public frmSetupShift() {
			InitializeComponent();
		}

		void clearAllField() {
			MyUtility.ClearControlText(new Control[]
				{
					textBoxShiftcode,maskedTextBoxVao, maskedTextBoxRaa,textBoxWKTime, maskedTextBoxWKDay,
					textBoxMoTa, textBoxKyhieu,
					maskedTextBoxBDLunch, maskedTextBoxKTLunch
				});
			numOnnInn.Value = 0;
			numCutInn.Value = 0;
			numOnnOut.Value = 0;
			numCutOut.Value = 0;
			numLateGrace.Value = 0;
			numEarlyGrace.Value = 0;
			numAfterOT.Value = 0;
			lbShiftID.Tag = null;
			checkBoxTachCaDem.Checked = false;
			checkBoxEnable.Checked = false;
			checkBoxCaMR.Checked = false;
			// 2 combobox
			comboBoxCaTruoc.DataSource = null;
			comboBoxCaSau.DataSource = null;
		}

		private void buttonThem_Click(object sender, EventArgs e) {
			themmoi = true;
			dgrdDSCa.ClearSelection();
			clearAllField();
			//mặc định khi thêm mới thì check enable
			checkBoxEnable.Checked = true;
		}

		private void buttonXoa_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			var shiftid = (lbShiftID.Tag != null) ? (int)(lbShiftID.Tag) : -1;
			if (shiftid != -1)
			{
				int n = DAO5.DelCa(shiftid);
				if (n == 0) MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			LoadGrid();// thêm hoặc sửa xong thì load lại grid

		}
		private void buttonLuu_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			#region lấy dữ liệu từ form và xử lý trước khi lưu

			var shiftid = (lbShiftID.Tag != null) ? (int) (lbShiftID.Tag) : -1;
			var shiftcode = textBoxShiftcode.Text;
			var mota = textBoxMoTa.Text;
			var kyhieucc = textBoxKyhieu.Text;
			bool tachcadem = checkBoxTachCaDem.Checked;
			bool isenable = checkBoxEnable.Checked;
			bool isextended = checkBoxCaMR.Checked;
			TimeSpan OnnDuty = TimeSpan.Zero, OffDuty = TimeSpan.Zero;
			TimeSpan OnnInn = TimeSpan.Zero, CutInn = TimeSpan.Zero;
			TimeSpan OnnOut = TimeSpan.Zero, CutOut = TimeSpan.Zero;
			TimeSpan OnnLun = TimeSpan.Zero, OffLun = TimeSpan.Zero;
			float wkday = 0f;
			int daycount = 0, wktime = 0,  shiftid1 = -1, shiftid2 = -1;

			#region validate giờ
			if (TimeSpan.TryParse(maskedTextBoxVao.Text, out OnnDuty) == false)
			{
				ACMessageBox.Show(Resources.Text_GioVaoKoHL, Resources.Caption_Loi, 2000);
				return;
			}
			if (TimeSpan.TryParse(maskedTextBoxRaa.Text, out OffDuty) == false)
			{
				ACMessageBox.Show(Resources.Text_GioRaaKoHL, Resources.Caption_Loi, 2000);
				return;
			}
			if (TimeSpan.TryParse(maskedTextBoxBDLunch.Text, out OnnLun) == false)
			{
				ACMessageBox.Show(Resources.Text_OnnLunKoHL, Resources.Caption_Loi, 2000);
				return;
			}
			if (TimeSpan.TryParse(maskedTextBoxKTLunch.Text, out OffLun) == false)
			{
				ACMessageBox.Show(Resources.Text_OffLunKoHL, Resources.Caption_Loi, 2000);
				return;
			}
			if (float.TryParse(maskedTextBoxWKDay.Text, out wkday) == false)
			{
				ACMessageBox.Show(Resources.Text_WKDayKoHL, Resources.Caption_Loi, 2000);
				return;
			}
			#endregion 

			//tính daycount , wktime
			if (OffDuty < OnnDuty) daycount = 1;
			OffDuty = OffDuty.Add(new TimeSpan(daycount, 0, 0, 0));

			wktime = Convert.ToInt32((OffDuty - OnnDuty - (OffLun - OnnLun)).TotalMinutes);

			int iOnnInn = Convert.ToInt32(numOnnInn.Value);// bug onn in qua đêm
			int iCutInn = Convert.ToInt32(numCutInn.Value);// bug cut in qua đêm
			int iOnnOut = Convert.ToInt32(numOnnOut.Value);// bug onn out qua đêm
			int iCutOut = Convert.ToInt32(numCutOut.Value);// bug cut out qua đêm
			int iCPTre = Convert.ToInt32(numLateGrace.Value);
			int iCPSom = Convert.ToInt32(numEarlyGrace.Value);
			int afterot = Convert.ToInt32(numAfterOT.Value);
			if (tachcadem)
			{
				shiftid1 = (int) comboBoxCaTruoc.SelectedValue;
				shiftid2 = (int) comboBoxCaSau.SelectedValue;
			}

			#endregion

			//MessageBox.Show(shiftid + " " + shiftcode + " " + mota + " " + kyhieucc + " " + OnnDuty + " " + OffDuty + " " + wktime);// fortesting
			if (themmoi) // đang thêm
			{
				int n = DAO5.InsCa(shiftcode, OnnDuty, OffDuty, daycount, wktime, wkday, iOnnInn, iCutInn, iOnnOut, iCutOut, iCPTre, iCPSom, afterot,
						   mota, kyhieucc, OnnLun, OffLun, tachcadem, shiftid1, shiftid2, isenable, isextended);
				if (n == 0) MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// sau khi lưu xong thì set về false
				themmoi = false;
			}
			else // đang sửa
			{
				int n = DAO5.UpdCa(shiftid, shiftcode, OnnDuty, OffDuty, daycount, wktime, wkday, iOnnInn, iCutInn, iOnnOut, iCutOut, iCPTre, iCPSom, afterot,
						   mota, kyhieucc, OnnLun, OffLun, tachcadem, shiftid1, shiftid2, isenable, isextended);
				if (n == 0) MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			LoadGrid();// thêm hoặc sửa xong thì load lại grid
		}

		private void buttonHuy_Click(object sender, EventArgs e) {
			themmoi = false;
			// xóa trắng nếu thêm
			clearAllField();
			dgrdDSCa.ClearSelection();
			
			// fill lại row đang chọn nếu đang sửa
		}

		private void buttonDong_Click(object sender, EventArgs e) {
			Close();
		}

		public void LoadGrid() {
			var tableDSCa = DAO5.LayDSCa();
			dgrdDSCa.DataSource = tableDSCa;
		}

		private void dgrdDSCa_SelectionChanged(object sender, EventArgs e) {
			#region ko chọn dòng nào hết thì xóa trắng
			if (dgrdDSCa.SelectedRows.Count == 0) {
				clearAllField();
				return;
			}
			#endregion
			// đang chọn 1 dòng
			themmoi = false;
			DataRowView row = dgrdDSCa.SelectedRows[0].DataBoundItem as DataRowView;
			if (row == null) {
				MessageBox.Show(Resources.Text_Vuilongthulai, Resources.Caption_Loi, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			#region xử lý thông tin lấy từ csdl
			TimeSpan tsOnDuty;
			TimeSpan.TryParse(row["Onduty"].ToString(), out tsOnDuty);
			TimeSpan tOffDuty;
			TimeSpan.TryParse(row["Offduty"].ToString(), out tOffDuty);
			var iDayCount = (int)row["DayCount"];
			tOffDuty = tOffDuty.Add(new TimeSpan(iDayCount, 0, 0, 0));
			var tOnTimeIn =  (int)row["OnTimeIn"];
			var tCutIn = (int)row["CutIn"];

			// phải add thêm 1 ngày daycount vì trong dữ liệu chỉ có chuỗi giờ thô : 05:45 không có ngày
			var tOnTimeOut = (int) row["OnTimeOut"];
			var tCutOut = (int) row["CutOut"];

			var tAfterOT = (int) row["AfterOT"];
			var tLateGrace = (int) row["LateGrace"];
			var tEarlyGrace = (int) row["EarlyGrace"];

			var tOnLunch = XL2._0gio;
			var tOffLunch = XL2._0gio;
			if (row["OnLunch"] != DBNull.Value && row["OffLunch"] != DBNull.Value) {
				TimeSpan.TryParse(row["OnLunch"].ToString(), out tOnLunch);
				TimeSpan.TryParse(row["OffLunch"].ToString(), out tOffLunch);
			}

			var tempWorkingTime = int.Parse(row["WorkingTime"].ToString());
			var Workingday = (float)row["Workingday"];
			var kyhieucc = row["KyHieuCC"].ToString();
			bool tachcadem = (row["IsSplited"] != DBNull.Value) && (bool) row["IsSplited"];
			var shiftid_catruoc = (row["ShiftID1"] != DBNull.Value) ? (int) row["ShiftID1"] : -1;
			var shiftid_casauuu = (row["ShiftID2"] != DBNull.Value) ? (int) row["ShiftID2"] : -1;
			#endregion

			#region fill thông tin

			lbShiftID.Tag = (int)row["ShiftID"];
			textBoxShiftcode.Text = row["ShiftCode"].ToString();
			maskedTextBoxVao.Text = tsOnDuty.ToString(@"hh\:mm");
			maskedTextBoxRaa.Text = tOffDuty.ToString(@"hh\:mm");
			textBoxWKTime.Text = tempWorkingTime.ToString();
			maskedTextBoxWKDay.Text = Workingday.ToString("0.0");
			numOnnInn.Value = tOnTimeIn;
			numCutInn.Value = tCutIn;
			numOnnOut.Value = tOnTimeOut;
			numCutOut.Value = tCutOut;
			numLateGrace.Value = tLateGrace;
			numEarlyGrace.Value = tEarlyGrace;
			numAfterOT.Value = tAfterOT;
			//mô tả
			textBoxMoTa.Text = row["Description"].ToString();
			textBoxKyhieu.Text = kyhieucc;
			maskedTextBoxBDLunch.Text = tOnLunch.ToString(@"hh\:mm");
			maskedTextBoxKTLunch.Text = tOffLunch.ToString(@"hh\:mm");
			// tách ca qua đêm
			checkBoxTachCaDem.Checked = tachcadem;
			if (shiftid_catruoc != -1 && shiftid_casauuu != -1)
			{
				comboBoxCaTruoc.SelectedValue = shiftid_catruoc;
				comboBoxCaSau.SelectedValue = shiftid_casauuu;
				comboBoxCaTruoc.Update();
				comboBoxCaSau.Update();
			}
			
			// đang sử dụng
			checkBoxEnable.Checked = (row["IsEnabled"] != DBNull.Value) && (bool) row["IsEnabled"];
			// ca mở rộng
			checkBoxCaMR.Checked = (row["IsExtended"] != DBNull.Value) && (bool) row["IsExtended"];

			#endregion


			#region

			/*            foreach (DataRow row in tableDSCa.Rows)
			{
				#region transfer dữ liệu từ row sang đối tượng
				var iShiftID = (int)row["ShiftID"];
				var sShiftCode = row["ShiftCode"].ToString();

				TimeSpan tsOnDuty;
				TimeSpan.TryParse(row["Onduty"].ToString(), out tsOnDuty);
				TimeSpan tOffDuty;
				TimeSpan.TryParse(row["Offduty"].ToString(), out tOffDuty);
				var iDayCount = (int)row["DayCount"];
				tOffDuty = tOffDuty.Add(new TimeSpan(iDayCount, 0, 0, 0));

				var tOnTimeIn = tsOnDuty.Subtract(new TimeSpan(0, (int)row["OnTimeIn"], 0));
				var tCutIn = tsOnDuty.Add(new TimeSpan(0, (int)row["CutIn"], 0));

				// phải add thêm 1 ngày daycount vì trong dữ liệu chỉ có chuỗi giờ thô : 05:45 không có ngày
				var tOnTimeOut = tOffDuty.Subtract(new TimeSpan(0, (int)row["OnTimeOut"], 0));
				var tCutOut = tOffDuty.Add(new TimeSpan(0, (int)row["CutOut"], 0));

				var tAfterOT = new TimeSpan(0, (int)row["AfterOT"], 0);
				var tLateGrace = new TimeSpan(0, (int)row["LateGrace"], 0);
				var tEarlyGrace = new TimeSpan(0, (int)row["EarlyGrace"], 0);

				var tOnLunch = XL2._0gio;
				var tOffLunch = XL2._0gio;
				if (row["OnLunch"] != DBNull.Value && row["OffLunch"] != DBNull.Value)
				{
					TimeSpan.TryParse(row["OnLunch"].ToString(), out tOnLunch);
					TimeSpan.TryParse(row["OffLunch"].ToString(), out tOffLunch);
				}

				var iShowPosition = (int)row["ShowPosition"];
				var tempWorkingTime = int.Parse(row["WorkingTime"].ToString());
				var kyhieucc = row["KyHieuCC"].ToString();
				var tempShift = new cCaChuan
				{
					ID = iShiftID,
					Code = sShiftCode,
					DayCount = iDayCount,
					QuaDem = (iDayCount == 1),
					OnnTS = tsOnDuty,
					OffTS = tOffDuty,
					OnnInnTS = tOnTimeIn,
					CutInnTS = tCutIn,
					OnnOutTS = tOnTimeOut,
					CutOutTS = tCutOut,
					AfterOTMin = tAfterOT,
					LateeMin = tLateGrace,
					EarlyMin = tEarlyGrace,
					Workingday = (Single)row["Workingday"],
					ShowPosition = iShowPosition,
					WorkingTimeTS = new TimeSpan(0, tempWorkingTime, 0),
					chophepTreTS = tsOnDuty.Add(tLateGrace),
					chophepSomTS = tOffDuty - tEarlyGrace,
					TOD_batdaulamthem = tOffDuty + tAfterOT,//[TBD] thay tOffDuty + XL2._30phut thành tempOffDuty + tempAfterOT --> đã thay
					LunchMin = tOffLunch.Subtract(tOnLunch),
					KyHieuCC = kyhieucc,
				};
				#endregion

				XL.DSCa.Add(tempShift);

			}*/

			#endregion

		}

		private void frmSetupShift_Load(object sender, EventArgs e) {
			dgrdDSCa.AutoGenerateColumns = false;

			#region kiểm tra kết nối csdl trước
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000); 
				this.Close();
				return;
			}
			#endregion

			LoadGrid();
			dgrdDSCa.ClearSelection();
			dgrdDSCa.SelectionChanged += dgrdDSCa_SelectionChanged;
		}

		private void checkBoxTachCaDem_CheckedChanged(object sender, EventArgs e) {
			if (checkBoxTachCaDem.Checked == false)
			{
				comboBoxCaTruoc.DataSource = null;
				comboBoxCaSau.DataSource = null;
			}
			else {
				if (XL2.KiemtraKetnoiCSDL() == false) return;

				var tableDSCaTruoc = DAO5.LayDSCa_FillCaTruocCaSau();
				var tableDSCaSau = tableDSCaTruoc.Copy();
				comboBoxCaTruoc.DataSource = tableDSCaTruoc;
				comboBoxCaTruoc.ValueMember = "ShiftID";
				comboBoxCaTruoc.DisplayMember = "ShiftCode";
				comboBoxCaSau.DataSource = tableDSCaSau;
				comboBoxCaSau.ValueMember = "ShiftID";
				comboBoxCaSau.DisplayMember = "ShiftCode";
			}
		}

		private void maskedTextBoxVao_TextChanged(object sender, EventArgs e)
		{
			TimeSpan OnnDuty = TimeSpan.Zero, OffDuty = TimeSpan.Zero;
			int daycount = 0, wktime= 0;
			if (TimeSpan.TryParse(maskedTextBoxVao.Text, out OnnDuty) == false)
			{
				textBoxWKTime.Text = "0";
				return;
			}
			if (TimeSpan.TryParse(maskedTextBoxRaa.Text, out OffDuty) == false) {
				textBoxWKTime.Text = "0";
				return;
			}
			if (OffDuty < OnnDuty) daycount = 1;
			OffDuty = OffDuty.Add(new TimeSpan(daycount, 0, 0, 0));
			wktime = Convert.ToInt32((OffDuty - OnnDuty).TotalMinutes);
			textBoxWKTime.Text = wktime.ToString();

		}

	}
}
