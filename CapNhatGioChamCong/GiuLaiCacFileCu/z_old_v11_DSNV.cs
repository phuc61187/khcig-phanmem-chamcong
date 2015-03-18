using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GiuLaiCacFileCu.DAO;
using GiuLaiCacFileCu.DTO;
using GiuLaiCacFileCu.Properties;

namespace GiuLaiCacFileCu {
	public partial class z_old_v11_DSNV : Form {
		//------------------ khai báo biến và hằng số ------------------------------
		public bool XemGioVaoRa { get; set; }
		public bool SuaGioVaoRa { get; set; }
		public bool ThemGioVaoRa { get; set; }
		public bool XoaGioVaoRa { get; set; }

		public int CurrentUserID { get; set; }

		private DataTable mDataTableDSPhongBan;
		private DataTable mTableOrigin = null;
		//private List<cUserInfo> _dsnv;
		//public List<cUserInfo> dsnv_checked { get; set; }
		//public List<cUserInfo> dsnv_unchecked { get; set; }
		private bool checkAllDSNV = false;
		private bool hasCheckDSNV = false;
		private int _mTongDSNV = 0;
		private int _mTongNVChecked = 0;
		private List<cShift> _dsCaLamViec;
		private List<TimeSpan> _dsGioVaoCa;
		private List<TimeSpan> _dsGioRaCa;


		private string SelStr_GetPhongBanThaoTac() {
			return @"select distinct UserInfo.UserIDD, RelationDept.[Description] 
                                    from UserInfo , RelationDept
                                    where UserInfo.UserIDD in 
		                                                    (  select DeptPrivilege.IDD from DeptPrivilege
                                                                where DeptPrivilege.UserID = @UserID and IsYes = @IsYes)
			                                and RelationDept.ID = UserInfo.UserIDD";
		}




		//------------------- hàm xử lý chính -----------------------------

		/// <summary>
		/// [TBD] ok có dùng
		/// </summary>
		/// <param name="pUserID"></param>
		/// <param name="pIsYes"></param>
		/// <returns></returns>
		private DataTable GetDataDSPhongBan(int pUserID, bool pIsYes) {
			DataTable kq = null;
			string selectQueryString = SelStr_GetPhongBanThaoTac();

			kq = SqlDataAccessHelper.ExecuteQueryString(selectQueryString, new string[] { "@UserID", "@IsYes" }, new object[] { pUserID, pIsYes });
			return kq;
		}

		/// <summary>
		/// Danh sách tất cả nhân viên các phòng ban mà user được phép thao tác
		/// </summary>
		/// <param name="pUserID"></param>
		/// <returns></returns>
		private DataTable GetDataDSNV(int pUserID) {
			DataTable kq = null;
			string selectQueryString = "";

			try {
				kq = SqlDataAccessHelper.ExecuteQueryString(selectQueryString, new[] { "@UserID", "@IsYes" }, new object[] { pUserID, true });
			} catch (Exception ex) {
				if (ex is InvalidOperationException) {
					MessageBox.Show(
							@"Có lỗi xảy ra trong quá trình load Danh sách nhân viên.\nVui lòng liên hệ phòng Kỹ thuật để được trợ giúp!",
							"Lỗi", MessageBoxButtons.OK);
				}
				else if (ex is System.Data.SqlClient.SqlException) {
					MessageBox.Show(
							@"Có lỗi xảy ra trong quá trình kết nối với CSDL.\nVui lòng liên hệ phòng Kỹ thuật để được trợ giúp!",
							"Lỗi", MessageBoxButtons.OK);
				}
				else MessageBox.Show(@"Lỗi: \n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK);
			}
			return kq;
		}

		/// <summary>
		/// [TBD] ok có dùng : Load danh sách phòng ban được quyền thao tác, hàm này làm 1 lần khi form được gọi
		/// </summary>
		/// <param name="pUserID"></param>
		private void LoadPhongBan(int pUserID) {
			mDataTableDSPhongBan = GetDataDSPhongBan(pUserID, true);

			// 1.thêm item chọn tất cả
			DataRow dr = mDataTableDSPhongBan.NewRow();
			dr["UserIDD"] = -1;
			dr["Description"] = "Tất cả";

			// 2. binding data 
			//   2.1 tắt Event SelectedIndexChanged trước khi bind dữ liệu
			comboBoxPhongBan.SelectedIndexChanged -= comboBoxPhongBan_SelectedIndexChanged;
			//   2.2 bind dữ liệu nếu có           
			//   2.3 thêm và chọn item tất cả (ID = -1)
			mDataTableDSPhongBan.Rows.InsertAt(dr, 0);
			comboBoxPhongBan.DataSource = mDataTableDSPhongBan;
			comboBoxPhongBan.DisplayMember = "Description";
			comboBoxPhongBan.ValueMember = "UserIDD";
			comboBoxPhongBan.SelectedIndex = 0;
			//   2.4 mở lại Event SelectedIndexChanged
			comboBoxPhongBan.SelectedIndexChanged += comboBoxPhongBan_SelectedIndexChanged;
		}

		public void LoadDSKyHieuVang() {

		}

		public void LoadDSGioTheoCa() {
			// 1. tạm thời disable comboBoxDSCa.SelectedIndexChanged, mở lên lại ở bước [TBD]
			comboBoxDSCa.SelectedIndexChanged -= comboBoxDSCa_SelectedIndexChanged;

			//2. lấy dữ liệu ca làm việc
			DataTable dt = null;
			_dsCaLamViec = new List<cShift>();
			_dsGioVaoCa = new List<TimeSpan>();
			_dsGioRaCa = new List<TimeSpan>();
			comboBoxDSCa.DataSource = dt;

			//3. duyệt từng dòng dữ liệu, bind vào comboBox Ca làm việc
			for (int i = 0; i < dt.Rows.Count; i++) {
				if ((int)dt.Rows[i]["ShiftID"] == -1) continue;

				string tempShiftCode = dt.Rows[i]["ShiftCode"].ToString();
				string tempOnduty = dt.Rows[i]["Onduty"].ToString();
				string tempOffduty = dt.Rows[i]["Offduty"].ToString();
				int tempWorkingTime = (int)dt.Rows[i]["WorkingTime"];
				int tempDayCount = (int)dt.Rows[i]["DayCount"];
				int tempShowPosition = (int)dt.Rows[i]["ShowPosition"];
                string tempCustomShiftCode = string.Empty;//GetDescriptionShiftCode(tempShiftCode, tempOnduty, tempOffduty, tempDayCount);
				cShift CaLVObj = new cShift() {
					ShiftID = (int)dt.Rows[i]["ShiftID"], ShiftCode = dt.Rows[i]["ShiftCode"].ToString(),
					OnnDutyTS = TimeSpan.Parse(tempOnduty), OffDutyTS = TimeSpan.Parse(tempOffduty), DayCount = tempDayCount,
					ShowPosition = tempShowPosition,
					WorkingTimeTS = new TimeSpan(0, tempWorkingTime, 0)
				};
				_dsCaLamViec.Add(CaLVObj);

				string giolamviec = dt.Rows[i]["Onduty"].ToString();
				TimeSpan tsp = TimeSpan.Parse(giolamviec);
				_dsGioVaoCa.Add(tsp);
				giolamviec = dt.Rows[i]["Offduty"].ToString();
				tsp = TimeSpan.Parse(giolamviec);
				_dsGioRaCa.Add(tsp);
			}

			// [TBD] đổi tên item cho dễ hiểu, phát triển sau

			// 4. thêm item Tùy chỉnh
			            DataRow newrow = dt.NewRow();
						newrow["ShiftID"] = -1;
						newrow["ShiftCode"] = "Tùy chỉnh";
						newrow["Onduty"] = string.Empty;
						newrow["Offduty"] = string.Empty;
						newrow["DayCount"] = 0;
						dt.Rows.InsertAt(newrow, 0);
			comboBoxDSCa.ValueMember = "ShiftID";
			comboBoxDSCa.DisplayMember = "ShiftCode";
			comboBoxDSCa.SelectedIndex = 0; //hiển thị mặc định item tùy chỉnh

			// 5. mở lại sự kiện comboBoxDSCa.SelectedIndexChanged
			comboBoxDSCa.SelectedIndexChanged += comboBoxDSCa_SelectedIndexChanged;
		}

		//[BackupFunction05]


		//------------------- xử lý sự kiện phát sinh ----------------------
		public z_old_v11_DSNV() {
			InitializeComponent();
			// đăng ký sự kiện
		}

		private void DSNV_Load(object sender, EventArgs e) {
            throw new NotImplementedException();
			// 1. load các phòng ban được phép thao tác
/*
            LoadPhongBan(ThamSo.currUserID);
			LoadDSGioTheoCa();

			// 2. load thời gian mặc định cho các date time là ngày đầu và cuối tháng hiện tại
			DateTime today = DateTime.Today;
			dateTimeNgayBD.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
			dateTimeNgayKT.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
			dateTimeNgayBD.Update();
			dateTimeNgayKT.Update();

			// 3. lấy dữ liệu ds nhân viên được phép thao tác
            mTableOrigin = GetDataDSNV(ThamSo.currUserID);
			_dsnv = new List<cUserInfo>();
			_dsnv = TransferDataToObj(mTableOrigin);

			_mTongDSNV = _dsnv.Count;
			_mTongNVChecked = 0;
			lbTongSoNV.Text = "Tổng: " + _mTongDSNV.ToString() + " NV";

			// 4. hiển thị dữ liệu lên luới DSNV
			dataGridDSNVThaoTac.DataSource = mTableOrigin;

			//[TBD] dòng này test, khi deploy nhớ xóa 
			/*            foreach (DataGridViewRow dataGridViewRow in dataGridDSNVThaoTac.Rows) {
							if (dataGridViewRow.Cells[1].Value.Equals(1000)) {
								dataGridViewRow.Cells[0].Value = true;
							}
						}#2#
			dataGridDSNVThaoTac.EndEdit();
*/
		}

		private void comboBoxPhongBan_SelectedIndexChanged(object sender, EventArgs e) {
			if (comboBoxPhongBan.SelectedIndex == 0)
				dataGridDSNVThaoTac.DataSource = mTableOrigin;
			else {
				DataRowView drv = comboBoxPhongBan.SelectedValue as DataRowView;
				int userIDD_filter;
				bool a = int.TryParse(comboBoxPhongBan.SelectedValue.ToString(), out userIDD_filter);
				DataView dtv_FilterUserIDD = new DataView(mTableOrigin, "UserIDD = " + userIDD_filter, String.Empty, DataViewRowState.CurrentRows);
				dataGridDSNVThaoTac.DataSource = dtv_FilterUserIDD.Count == 0 ? null : dtv_FilterUserIDD;
			}
			hasCheckDSNV = checkAllDSNV = false;
			_mTongDSNV = dataGridDSNVThaoTac.Rows.Count;
			lbTongSoNV.Text = "Tổng: " + _mTongDSNV + " NV";
		}

		private void dataGridDSNVThaoTac_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
			if (e.ColumnIndex != 0) return;

			if (dataGridDSNVThaoTac.RowCount == 0) { checkAllDSNV = hasCheckDSNV = false; return; }

			checkAllDSNV = !checkAllDSNV;
			hasCheckDSNV = checkAllDSNV;
			Parallel.For(0, dataGridDSNVThaoTac.Rows.Count, (dem) => { dataGridDSNVThaoTac.Rows[dem].Cells[0].Value = checkAllDSNV; });

			dataGridDSNVThaoTac.EndEdit();
			_mTongNVChecked = checkAllDSNV ? _mTongDSNV : 0;
		}

		private void buttonXemGioVaoRa_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
/*
			// 1. kiểm tra giờ hợp lệ
			dateTimeNgayBD.Update();
			dateTimeNgayKT.Update();
			dateTimeKetThuc.Update();
			dateTimeBatDau.Update();
			dataGridDSNVThaoTac.EndEdit();
			if (radioTheoGio.Checked && dateTimeKetThuc.Value < dateTimeBatDau.Value) {
				if (MessageBox.Show("Giờ kết thúc trước giờ bắt đầu. "
					//+ dateTimeKetThuc.Value.ToString("dddd dd/MM/yyyy hh:mm:ss tt")
					//+ " đến " + dateTimeBatDau.Value.ToString("dddd dd/MM/yyyy hh:mm:ss tt") + "? "
					, "Hỏi lại", MessageBoxButtons.OK) == DialogResult.OK)
					return;
			}

			// mỗi lần bấm Button này là tạo ra list mới, tạo form mới
			GetDSNVCheck();

			z_old_v11_SuaGioChamCong frmSuaGioChamCong;
			if (radioTheoNgay.Checked) {
				frmSuaGioChamCong = new z_old_v11_SuaGioChamCong {
					timTheoNgay = true,
					StartTime = dateTimeNgayBD.Value, EndTime = dateTimeNgayKT.Value,
					SuaGioVaoRa = this.SuaGioVaoRa, ThemGioVaoRa = this.ThemGioVaoRa, XoaGioVaoRa = this.XoaGioVaoRa,
					_dsNVXemChk = dsnv_checked, _dsNVXemUnChk = dsnv_unchecked
				};
			}
			else //if (radioTheoGio.Checked)
            {
				frmSuaGioChamCong = new z_old_v11_SuaGioChamCong {
					timTheoNgay = false,
					StartTime = dateTimeBatDau.Value, EndTime = dateTimeKetThuc.Value,
					SuaGioVaoRa = this.SuaGioVaoRa, ThemGioVaoRa = this.ThemGioVaoRa, XoaGioVaoRa = this.XoaGioVaoRa,
					_dsNVXemChk = dsnv_checked, _dsNVXemUnChk = dsnv_unchecked
				};
			}
			//ThemGioChamCong frmThemGioChamCong = new ThemGioChamCong();

			//frmThemGioChamCong.ShowDialog();

			frmSuaGioChamCong.ShowDialog();
*/
		}

		private void buttonThoat_Click(object sender, EventArgs e) {
			this.Close();
			GC.Collect();
		}

		private void radioTheoNgayTheoGio_CheckedChanged(object sender, EventArgs e) {
			dateTimeNgayBD.Enabled = dateTimeNgayKT.Enabled = radioTheoNgay.Checked;
			comboBoxDSCa.Enabled = dateTimeBatDau.Enabled = dateTimeKetThuc.Enabled = radioTheoGio.Checked;
		}

		private void comboBoxDSCa_SelectedIndexChanged(object sender, EventArgs e) {
			// mỗi lần thay đổi ca thì cập nhật lại giờ cho dateTimeGioSua
			dateTimeBatDau.ValueChanged -= dateTimeBatDau_ValueChanged;
			dateTimeKetThuc.ValueChanged -= dateTimeKetThuc_ValueChanged;
			if (comboBoxDSCa.SelectedIndex != 0) {
				dateTimeBatDau.Value = new DateTime(dateTimeBatDau.Value.Year
									, dateTimeBatDau.Value.Month
									, dateTimeBatDau.Value.Day
									, _dsGioVaoCa[comboBoxDSCa.SelectedIndex - 1].Hours// (index - 1) vì không tính item tùy chỉnh ko có giờ vào ra
									, _dsGioVaoCa[comboBoxDSCa.SelectedIndex - 1].Minutes
									, 0);
				dateTimeKetThuc.Value = new DateTime(dateTimeKetThuc.Value.Year
									, dateTimeKetThuc.Value.Month
									, dateTimeKetThuc.Value.Day
									, _dsGioRaCa[comboBoxDSCa.SelectedIndex - 1].Hours// (index - 1) vì không tính item tùy chỉnh ko có giờ vào ra
									, _dsGioRaCa[comboBoxDSCa.SelectedIndex - 1].Minutes
									, 0);
			}
			dateTimeBatDau.Update();
			dateTimeKetThuc.Update();
			dateTimeBatDau.ValueChanged += dateTimeBatDau_ValueChanged;
			dateTimeKetThuc.ValueChanged += dateTimeKetThuc_ValueChanged;
		}

		private void dateTimeBatDau_ValueChanged(object sender, EventArgs e) {
			if (comboBoxDSCa.SelectedIndex != 0 &&
					((dateTimeBatDau.Value.Hour != _dsGioVaoCa[comboBoxDSCa.SelectedIndex - 1].Hours)
					|| (dateTimeBatDau.Value.Minute != _dsGioVaoCa[comboBoxDSCa.SelectedIndex - 1].Minutes))) {
				comboBoxDSCa.SelectedIndex = 0;
			}
		}

		private void dateTimeKetThuc_ValueChanged(object sender, EventArgs e) {
			if (comboBoxDSCa.SelectedIndex != 0 &&
					((dateTimeKetThuc.Value.Hour != _dsGioVaoCa[comboBoxDSCa.SelectedIndex - 1].Hours)
					|| (dateTimeKetThuc.Value.Minute != _dsGioVaoCa[comboBoxDSCa.SelectedIndex - 1].Minutes))) {
				comboBoxDSCa.SelectedIndex = 0;
			}
		}

		private void button1_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
/*
			GetDSNVCheck();
			z_old_v11_frmTichluyBugio frm1 = new z_old_v11_frmTichluyBugio { DSNV = dsnv_checked };
			frm1.ShowDialog();
*/
		}

		void GetDSNVCheck() {
            throw new NotImplementedException();
/*
			dsnv_checked = new List<cUserInfo>();
			dsnv_unchecked = new List<cUserInfo>();
			dataGridDSNVThaoTac.EndEdit();
			foreach (DataGridViewRow dgvr in dataGridDSNVThaoTac.Rows) {
				object checkedValue = dgvr.Cells[0].FormattedValue;
				object MaCCValue = dgvr.Cells[1].FormattedValue;
				cUserInfo tempNhanvien = _dsnv.Find(item => MaCCValue != null && item.UserEnrollNumber == int.Parse(MaCCValue.ToString()));
				if (checkedValue != null && (bool)checkedValue)
					dsnv_checked.Add(tempNhanvien);
				else if (checkedValue != null && (bool)checkedValue == false)
					dsnv_unchecked.Add(_dsnv.Find(item => MaCCValue != null && item.UserEnrollNumber == int.Parse(MaCCValue.ToString())));
				else {
					MessageBox.Show("Null Object", "Lỗi", MessageBoxButtons.OK);
					return;
				}
			}
			if (dsnv_checked.Count == 0) {
				MessageBox.Show("Vui lòng chọn nhân viên để thao tác.", Resources.MessBoxCap_NhacNho, MessageBoxButtons.OK); //[TBD]
				return;
			}
*/
		}

	}
}