using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GiuLaiCacFileCu.DAO;
using GiuLaiCacFileCu.DTO;
using GiuLaiCacFileCu.Properties;
using System.Linq;

namespace GiuLaiCacFileCu {
	public partial class z_old_v11_SuaGioChamCong : Form {
		//------------------ khai báo biến , đăng ký sự kiện ------------------
		public bool SuaGioVaoRa { get; set; }
		public bool ThemGioVaoRa { get; set; }
		public bool XoaGioVaoRa { get; set; }

		private int _activeTab = -1;
		private int _mCurrentUserID = -1;
		private bool checkAllCTGioVao;
		private bool checkAllCTGioRa;
		private bool checkAllGioBT;
		private bool hasCheckDSNV = false;
/*
		public List<cUserInfo> _dsNVXemChk { get; set; }
		public List<cUserInfo> _dsNVXemUnChk { get; set; }
*/
		#region các biến tạm thời chưa sử dụng
		//private List<clsUserInfo> _dsNVThemGioVao_Checked;
		//private List<clsUserInfo> _dsNVThemGioVao_Unchecked;
		//private List<clsUserInfo> _dsNVThemGioRa_Checked;
		//private List<clsUserInfo> _dsNVThemGioRa_Unchecked;
		#endregion

		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public bool timTheoNgay = false;
		private List<cShift> _dsCaLamViec;
		private List<TimeSpan> _dsGioVaoCa = new List<TimeSpan>();
		private List<TimeSpan> _dsGioRaCa = new List<TimeSpan>();


		//[TBD]
		string UpdateStringSuaGioVaoRa() {
			//[TBD]
			string kq = @"update CheckInOut ";
			kq += @"      set ";
			kq += @"            TimeStr = @TimeStr_New
                              , TimeDate = @TimeDate_New";
			kq += @"      where ";
			kq += @"            UserEnrollNumber = @UserEnrollNumber
                             and TimeStr = @TimeStr_Old";
			return kq;
		}

		//[TBD]
		string InsertStringBackupThemGioVaoRa() {
			string kq = @"INSERT INTO LichSuSuaGioVaoRa (UserEnrollNumber
                               ,TimeDate,TimeStrOld,TimeStrNew
                               ,Source,MachineNo,WorkCode
                               ,UserID
                               ,Explain,Note
                               ,CommandType
                               ,ExecuteTime
                          ) VALUES (";
			kq += @"            @UserEnrollNumber
                               ,@TimeDate,@TimeStrOld,@TimeStrNew
                               ,@Source,@MachineNo,@WorkCode
                               ,@UserID
                               ,@Explain,@Note
                               ,@CommandType
                               ,GetDate()"; //lấy giờ của sql server chứ ko lấy giờ client
			kq += @")";
			return kq;
		}

		string insertStringThemGioVaoRa() {
			string kq = @"INSERT INTO CheckInOut(UserEnrollNumber
                               ,TimeDate,TimeStr
                               ,Source,MachineNo,WorkCode)
                         VALUES
                               (@UserEnrollNumber
                               ,@TimeDate,@TimeStr
                               ,@Source,@MachineNo,@WorkCode)";
			return kq;
		}

		//------------------ các hàm xử lý ------------------------------------


		public void LoadDSGioTheoCa() {
			// 1. tạm thời disable comboBoxDSCa.SelectedIndexChanged, mở lên lại ở bước [TBD]
			comboBoxDSCa.SelectedIndexChanged -= comboBoxDSCa_SelectedIndexChanged;

			//2. lấy dữ liệu ca làm việc
			DataTable dt = null;
			_dsCaLamViec = new List<cShift>();
			comboBoxDSCa.DataSource = dt;
			//DataRow dr = dt.NewRow();


			//3. duyệt từng dòng dữ liệu, bind vào comboBox Ca làm việc
			for (int i = 0; i < dt.Rows.Count; i++) {
				if ((int)dt.Rows[i]["ShiftID"] == -1) continue;
				string tempShiftCode = dt.Rows[i]["ShiftCode"].ToString();
				string tempOnduty = dt.Rows[i]["Onduty"].ToString();
				string tempOffduty = dt.Rows[i]["Offduty"].ToString();
				int tempDayCount = (int)dt.Rows[i]["DayCount"];
				int tempWorkingTime = (int) dt.Rows[i]["WorkingTime"];
				string tempCustomShiftCode = string.Empty;//GetDescriptionShiftCode(tempShiftCode, tempOnduty, tempOffduty, tempDayCount);
				cShift CaLVObj = new cShift() {
					ShiftID = (int)dt.Rows[i]["ShiftID"], ShiftCode = tempCustomShiftCode,
					OnnDutyTS = TimeSpan.Parse(tempOnduty), OffDutyTS = TimeSpan.Parse(tempOffduty), DayCount = tempDayCount,
					WorkingTimeTS = new TimeSpan(0, tempWorkingTime, 0)
				};
				_dsCaLamViec.Add(CaLVObj);

				// tạo danh sách giờ chuẩn để xử lý hiển thị dateTimeGioSua
				string giolamviec = dt.Rows[i]["Onduty"].ToString();
				TimeSpan tsp = TimeSpan.Parse(giolamviec);
				_dsGioVaoCa.Add(tsp);
				giolamviec = dt.Rows[i]["Offduty"].ToString();
				tsp = TimeSpan.Parse(giolamviec);
				_dsGioRaCa.Add(tsp);
			}


			// 5. mở lại sự kiện comboBoxDSCa.SelectedIndexChanged
			comboBoxDSCa.SelectedIndexChanged += comboBoxDSCa_SelectedIndexChanged;
		}

		private void TransferDataToObj_v3(DataTable pDataTableOriginal) {
			throw new NotImplementedException();
/*
			cUserInfo nhanvien = new cUserInfo();

			// 1. duyệt qua từng dòng dữ liệu
			foreach (DataRow dr in pDataTableOriginal.Rows) {
				int tempEnrollNumber = (int)dr["UserEnrollNumber"];
				int tempMachineNo = (int)dr["MachineNo"];
				DateTime tempTimeStr = (DateTime)dr["TimeStr"];
				// 1.1 tìm nhân viên tương ứng trong ds nhân viên đã chọn xem giờ công
				if (nhanvien.UserEnrollNumber != tempEnrollNumber)
					nhanvien = _dsNVXemChk.Find(item => item.UserEnrollNumber == tempEnrollNumber);

				// giờ vào ra của cùng 1 người, xét có nằm trong khoảng 5ph hay ko, nếu trong 5ph thì add vào giờ liên quan, ko thì add giờ chính mới
				/*                cCheckInOut tempGio = new cCheckInOut(tempEnrollNumber
																		 , tempTimeStr.Date
																		 , tempTimeStr
																		 , dr["OriginType"].ToString()
																		 , (dr["NewType"] != DBNull.Value)
																			 ? dr["NewType"].ToString()
																			 : null//: string.Empty
																		 , dr["Source"].ToString()
																		 , tempMachineNo
																		 , (dr["WorkCode"] != DBNull.Value) ? (int)dr["WorkCode"] : -1);#3#
				GiuLaiCacFileCu.cChk tempGio = new GiuLaiCacFileCu.cChk() {
					UserEnrollNumber = tempEnrollNumber, Cong = 0f, GioLamThem = new TimeSpan(0, 0, 0), KieuChamTay = false, ListGioLQ_v3 = new List<GiuLaiCacFileCu.cChk>(),
					MachineNo = tempMachineNo, PhuCap = 0f, NewType = (dr["NewType"] != DBNull.Value) ? dr["NewType"].ToString() : null,
					Source = dr["Source"].ToString(), WorkCode = (dr["WorkCode"] != DBNull.Value) ? (int)dr["WorkCode"] : -1,
					OriginType = dr["OriginType"].ToString(), RaaSom = new TimeSpan(0, 0, 0), VaoTre = new TimeSpan(0, 0, 0), ThuocCa = new List<cShift>(),
					TimeDate = tempTimeStr.Date, TimeStr = tempTimeStr, Type = 0
				};
				//nhanvien.ThemGio_v3(tempGio);
			}
*/
		}

		//------------------ xử lý các sự kiện phát sinh ----------------------
		public z_old_v11_SuaGioChamCong() {
			InitializeComponent();
			GC.Collect();
		}


		private void SuaGioChamCong_Load(object sender, EventArgs e) {
			throw new NotImplementedException();
/*
			btnThem.Enabled = ThemGioVaoRa;
			btnSua.Enabled = SuaGioVaoRa;
			btnXoa.Enabled = XoaGioVaoRa;
			//1. load ngày tháng mặc định cho các datetimepicker
			dateTimeGioSua.ValueChanged -= dateTimeGioSua_ValueChanged;
			dateTimeGioSua.Value = StartTime;
			dateTimeGioSua.ValueChanged += dateTimeGioSua_ValueChanged;

			//2. xử lý hiển thị combobox ca làm việc
			LoadDSGioTheoCa();

			#region  3. lấy dữ liệu chấm công theo ngày hay theo gio cu the
			if (timTheoNgay) {
				mTableOrigin = SqlDataAccessHelper.ExecuteQueryString(SelStr_GetDSGioVaoRa()
																	  , new[] { "@BatDauVao", "@KetThucVao", "@BatDauRa", "@KetThucRa" }
																	  , new object[]{StartTime.Date.AddHours(4.5d)
                                                                                , EndTime.Date.AddHours(23.5d)
                                                                                , StartTime.Date.AddHours(7d)
                                                                                , EndTime.Date.Add(new TimeSpan(1, 7, 0, 0))});
			}
			else {
				mTableOrigin = SqlDataAccessHelper.ExecuteQueryString(SelStr_GetDSGioVaoRa()
													  , new[] { "@BatDauVao", "@KetThucVao", "@BatDauRa", "@KetThucRa" }
													  , new object[] { StartTime, EndTime, StartTime, EndTime });
			}
			#endregion
			#region 4. hiển thị chi tiết giờ vào, giờ ra ở 2 lưới khác nhau
			DataView dtv1 = new DataView(mTableOrigin, "MachineNo%2 = 1", "UserEnrollNumber ASC", DataViewRowState.CurrentRows);
			DataView dtv2 = new DataView(mTableOrigin, "MachineNo%2 = 0", "UserEnrollNumber ASC", DataViewRowState.CurrentRows);
			/*
						dataGridCTGioVao.DataSource = dtv1;
						dataGridCTGioRa.DataSource = dtv2;
			#2#
			dataGridCTGioVao.DataSource = mTableOrigin;
			#endregion

			#region clear dữ liệu chấm công của nhân viên để load lại

			Parallel.ForEach(_dsNVXemChk, info => info.ClearAll());
			dataGridGioBT.Rows.Clear();
			dataGridTongHop.Rows.Clear();
			#endregion
			TransferDataToObj_v3(mTableOrigin);
			//TransferDataToObj_v4(mTableOrigin);


			#region // 3. [TBD] hiển thị dữ liệu cho lưới tổng hợp

			foreach (cUserInfo nv in _dsNVXemChk) {
				//nv.XetGioHopLe_v3();
				//nv.XetGioHopLe_v5();
				nv.KhoiTaoDSNgayCong(StartTime, EndTime);
				nv.XetGioHopLe_v10();
				nv.DuaVaoNgayCong();
				nv.XetGioCaDai_v72();
				#region load vào datagridTongHop
				int i = 0;

				foreach (clsNgayCong_v71 ngayCongV71 in nv.DSNgayCong_v71) {
					if (ngayCongV71.DSVaoHL_v71.Count == 0) {
						DataGridViewRow dataGridViewRow = new DataGridViewRow();
						dataGridViewRow.CreateCells(dataGridTongHop, new object[]
                                                                      {
                                                                          nv.UserEnrollNumber, nv.UserFullName
                                                                          , ngayCongV71.NgayCong
                                                                          , null, null, null, null, null, null
                                                                      });
						dataGridTongHop.Rows.Add(dataGridViewRow);
					}
					else {
						for (int j = 0; j < ngayCongV71.DSVaoHL_v71.Count; j++) {
							DataGridViewRow dataGridViewRow = new DataGridViewRow();
							dataGridViewRow.CreateCells(dataGridTongHop
								, new object[]{nv.UserEnrollNumber
                                                                          , nv.UserFullName
                                                                          , ngayCongV71.NgayCong
                                                                          , ngayCongV71.DSVaoHL_v71[j].TimeStr, ngayCongV71.DSRaHL_v71[j].TimeStr
                                                                          , (ngayCongV71.DSVaoHL_v71[j].VaoTre == new TimeSpan(0,0,0)) 
                                                                          ? string.Empty
                                                                          : ngayCongV71.DSVaoHL_v71[j].VaoTre.ToString()
                                                                          , (ngayCongV71.DSRaHL_v71[j].RaaSom == new TimeSpan(0,0,0))
                                                                          ? string.Empty
                                                                          : ngayCongV71.DSRaHL_v71[j].RaaSom.ToString()
                                                                          , ngayCongV71.DSVaoHL_v71[j].ThuocCa[0].ShiftCode
                                                                          , string.Empty
                                                                          , ngayCongV71.DSRaHL_v71[j].Cong
                                                                      });
							dataGridTongHop.Rows.Add(dataGridViewRow);
						}
					}

				}
				#endregion

				List<cChk> DSVaoBatThuong1 = new List<cChk>();
				DSVaoBatThuong1.AddRange(nv.DSGVaoBatThuong);
				DSVaoBatThuong1.AddRange(nv.DSGRaBatThuong);

				DSVaoBatThuong1.Sort(new Comparison<cChk>((@out, inOut) => {
					if (@out.TimeStr > inOut.TimeStr) return 1;
					return -1;
				}));

				// load vào datagridGioBatThuong
				if (DSVaoBatThuong1 != null) {
					for (i = 0; i < DSVaoBatThuong1.Count; i++) {
						DataGridViewRow dr = new DataGridViewRow();
						dr.CreateCells(dataGridGioBT, new object[]{
                            false
                            , nv.UserEnrollNumber
                            , nv.UserFullName
                            , DSVaoBatThuong1[i].TimeDate
                            , (DSVaoBatThuong1[i].MachineNo % 2 == 1) ? (object)DSVaoBatThuong1[i].TimeStr : null
                            , (DSVaoBatThuong1[i].MachineNo % 2 == 0) ? (object)DSVaoBatThuong1[i].TimeStr : null
                            , DSVaoBatThuong1[i].MachineNo
                            , DSVaoBatThuong1[i].Source
                            , DSVaoBatThuong1[i].OriginType
                            , DSVaoBatThuong1[i].NewType
                            , DSVaoBatThuong1[i].WorkCode                            
                            , (DSVaoBatThuong1[i].MachineNo % 2 == 1) ? "Không Raa" : "Không Vào"});
						dataGridGioBT.Rows.Add(dr);
					}
				}
				if (nv.DSVaoKhacChuaXL2_v72 != null) {
					for (i = 0; i < nv.DSVaoKhacChuaXL2_v72.Count; i++) {
						DataGridViewRow dr = new DataGridViewRow();
						TimeSpan tonggio = nv.DSRaKhacChuaXL2_v72[i].TimeStr - nv.DSVaoKhacChuaXL2_v72[i].TimeStr;
						dr.CreateCells(dataGridCTGioRa, new object[]{
                            false
                            , nv.UserEnrollNumber
                            , nv.UserFullName
                            , nv.DSVaoKhacChuaXL2_v72[i].TimeDate
                            , nv.DSVaoKhacChuaXL2_v72[i].TimeStr
                            , nv.DSRaKhacChuaXL2_v72[i].TimeStr
                            , tonggio.TotalHours
                        });
						dataGridCTGioRa.Rows.Add(dr);
					}
				}
				/*
				if (nv.DSVaoBatThuong != null) {
					for (i = 0; i < nv.DSVaoBatThuong.Count; i++) {
						DataGridViewRow dr = new DataGridViewRow();
						dr.CreateCells(dataGridGioBT, new object[]{
							false
							, nv.UserEnrollNumber
							, nv.UserFullName
							, nv.DSVaoBatThuong[i].TimeDate
							, nv.DSVaoBatThuong[i].TimeStr
							, null
							, nv.DSVaoBatThuong[i].MachineNo
							, nv.DSVaoBatThuong[i].Source
							, nv.DSVaoBatThuong[i].OriginType
							, nv.DSVaoBatThuong[i].NewType
							, nv.DSVaoBatThuong[i].WorkCode                            
							, "Không Raa"});
						dataGridGioBT.Rows.Add(dr);
					}
				}
				if (nv.DSRaBatThuong != null)
					for (i = 0; i < nv.DSRaBatThuong.Count; i++) {
						DataGridViewRow dr = new DataGridViewRow();
						dr.CreateCells(dataGridGioBT, new object[]{
							false
							, nv.UserEnrollNumber
							, nv.UserFullName
							, nv.DSRaBatThuong[i].TimeDate
							, null
							, nv.DSRaBatThuong[i].TimeStr
							, nv.DSRaBatThuong[i].MachineNo
							, nv.DSRaBatThuong[i].Source
							, nv.DSRaBatThuong[i].OriginType
							, nv.DSRaBatThuong[i].NewType
							, nv.DSRaBatThuong[i].WorkCode
							, "Không Vào"});
						dataGridGioBT.Rows.Add(dr);
					}
#2#

				if (nv.DSGioVaoSai_v4 != null)
					for (i = 0; i < nv.DSGioVaoSai_v4.Count; i++) {
						DataGridViewRow dr = new DataGridViewRow();
						dr.CreateCells(dataGridGioBT, new object[]{
                            false
                            , nv.UserEnrollNumber
                            , nv.UserFullName
                            , nv.DSGioVaoSai_v4[i].TimeDate
                            , nv.DSGioVaoSai_v4[i].TimeStr
                            , null
                            , nv.DSGioVaoSai_v4[i].MachineNo
                            , nv.DSGioVaoSai_v4[i].Source
                            , nv.DSGioVaoSai_v4[i].OriginType
                            , nv.DSGioVaoSai_v4[i].NewType
                            , nv.DSGioVaoSai_v4[i].WorkCode
                            , "Check nhầm giờ Raa"});
						dataGridGioBT.Rows.Add(dr);
					}

				if (nv.DSGioRaSai_v4 != null)
					for (i = 0; i < nv.DSGioRaSai_v4.Count; i++) {
						DataGridViewRow dr = new DataGridViewRow();
						dr.CreateCells(dataGridGioBT, new object[]{
                            false
                            , nv.UserEnrollNumber
                            , nv.UserFullName
                            , nv.DSGioRaSai_v4[i].TimeDate
                            , null
                            , nv.DSGioRaSai_v4[i].TimeStr
                            , nv.DSGioRaSai_v4[i].MachineNo
                            , nv.DSGioRaSai_v4[i].Source
                            , nv.DSGioRaSai_v4[i].OriginType
                            , nv.DSGioRaSai_v4[i].NewType
                            , nv.DSGioRaSai_v4[i].WorkCode
                            , "Check nhầm giờ vào"});
						dataGridGioBT.Rows.Add(dr);
					}
			}
			#endregion

#1#
            */

		}



		private void radioButtonChonKieuSua_CheckedChanged(object sender, EventArgs e) {
			// đổi kiểu sửa giờ vào ra thì load lại thời gian trên dateTimeGioSua nếu đang chọn theo ca
			// 1. tạm thời disable 
			comboBoxDSCa.SelectedIndexChanged -= comboBoxDSCa_SelectedIndexChanged;
			dateTimeGioSua.ValueChanged -= dateTimeGioSua_ValueChanged;

			// 2. nếu đang theo ca thì load lại dateTime
			if (comboBoxDSCa.SelectedIndex != 0) {
				dateTimeGioSua.Value = new DateTime(dateTimeGioSua.Value.Year
					, dateTimeGioSua.Value.Month
					, 1
					, (radioButtonSuaGioVao.Checked) ? _dsGioVaoCa[comboBoxDSCa.SelectedIndex - 1].Hours : _dsGioRaCa[comboBoxDSCa.SelectedIndex - 1].Hours // (index - 1) vì không tính item tùy chỉnh ko có giờ vào ra
					, (radioButtonSuaGioVao.Checked) ? _dsGioVaoCa[comboBoxDSCa.SelectedIndex - 1].Minutes : _dsGioRaCa[comboBoxDSCa.SelectedIndex - 1].Minutes
					, (radioButtonSuaGioVao.Checked) ? _dsGioVaoCa[comboBoxDSCa.SelectedIndex - 1].Seconds : _dsGioRaCa[comboBoxDSCa.SelectedIndex - 1].Seconds);
			}

			comboBoxDSCa.SelectedIndexChanged += comboBoxDSCa_SelectedIndexChanged;
			dateTimeGioSua.ValueChanged += dateTimeGioSua_ValueChanged;
		}

		private void dateTimeGioSua_ValueChanged(object sender, EventArgs e) {
			// mỗi lần thay đổi giờ thì cập nhật lại default của comboBox là Tùy chỉnh
			if (comboBoxDSCa.SelectedIndex != 0 &&
				((dateTimeGioSua.Value.Hour != _dsGioVaoCa[comboBoxDSCa.SelectedIndex - 1].Hours)
				|| (dateTimeGioSua.Value.Minute != _dsGioVaoCa[comboBoxDSCa.SelectedIndex - 1].Minutes))) {
				comboBoxDSCa.SelectedIndex = 0;
			}
		}

		private void buttonThucHienHangLoat_Click(object sender, EventArgs e) {
			if (MessageBox.Show("Thực hiện thay đổi giờ chấm công hàng loạt cho Nhân viên. Nhấn OK để thực hiện. Nhấn Cancel để huỷ.", "Hỏi lại", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
				return;
			if (_activeTab == 1)
				sua(dataGridCTGioVao);
			else if (_activeTab == 2)
				sua(dataGridCTGioRa);
			else if (_activeTab == 3)
				sua2(dataGridGioBT);

			SuaGioChamCong_Load(null, null);
		}

		private void sua(DataGridView dgv) {
			dgv.EndEdit();
			int dem;
			int kq_updateGio = 0, kq_insertLichSuSuaGio = 0;
			int temp_UserEnrollNumber = 0, temp_MachineNo = 0, temp_WorkCode = -1;
			string temp_Source = string.Empty;
			DateTime temp_TimeStrOld = new DateTime();
			DateTime temp_TimeStrNew = dateTimeGioSua.Value;

			List<DataGridViewRow> gridRow_Checked = new List<DataGridViewRow>();
			List<DataGridViewRow> gridRow_Unchecked = new List<DataGridViewRow>();
			//1. duyệt từng dòng trong dataGrid, dòng nào check thì add vào list gridRow_checked
			for (dem = 0; dem < dgv.Rows.Count; dem++) {
				object formattedValue = dgv.Rows[dem].Cells[0].Value;
				bool checkedRows = formattedValue != null && (bool)formattedValue;
				if (checkedRows) gridRow_Checked.Add(dgv.Rows[dem]);
				else gridRow_Unchecked.Add(dgv.Rows[dem]);
			}
			//2. duyệt qua từng gridRow_checked , update xuống csdl
			for (dem = 0; dem < gridRow_Checked.Count; dem++) {
				object formattedValue1 = gridRow_Checked[dem].Cells[1].Value;//cell 1 UserEnrollNumber  ,
				object formattedValue4 = gridRow_Checked[dem].Cells[4].Value;// cell 4 TimeStrOld 
				object formattedValue5 = gridRow_Checked[dem].Cells[5].Value;// cell 5 machineNo
				object formattedValue6 = gridRow_Checked[dem].Cells[6].Value;// cell 6 source
				object formattedValue7 = gridRow_Checked[dem].Cells[7].Value;// cell 7 origintype
				object formattedValueNewType = gridRow_Checked[dem].Cells[8].Value;// cell 8 newtype
				object formattedValue9 = gridRow_Checked[dem].Cells[9].Value;// cell 9 workcode

				if (formattedValue1 != null && formattedValue4 != null && formattedValue5 != null && formattedValue6 != null && formattedValue7 != null && formattedValue9 != null) {
					temp_UserEnrollNumber = int.Parse(formattedValue1.ToString());   // cell 1 UserEnrollNumber  , cell 2 tên nv
					temp_TimeStrOld = DateTime.Parse(formattedValue4.ToString());           // cell 4 TimeStrOld  , cell 3 timedateold
					temp_MachineNo = int.Parse(formattedValue5.ToString());
					temp_Source = formattedValue6.ToString();
					temp_WorkCode = (int)formattedValue9;
				}

				#region update vào bảng checkinout

				kq_updateGio = ExecuteUpdateGio(temp_UserEnrollNumber, temp_TimeStrOld, temp_TimeStrNew);

				#endregion
				if (kq_updateGio == 0) break;
				#region insert vào bảng lịch sử

				kq_insertLichSuSuaGio = ExecuteInsertLichSuSua(temp_UserEnrollNumber
															   , temp_TimeStrOld
															   , temp_TimeStrNew
															   , temp_Source
															   , temp_MachineNo
															   , temp_WorkCode
															   , _mCurrentUserID
															   , textBoxLyDo.Text
															   , textBoxGhiChu.Text
															   , 0);

				#endregion
				if (kq_insertLichSuSuaGio == 0) break;
			}
			if (kq_insertLichSuSuaGio == 0 || kq_updateGio == 0)
				MessageBox.Show("Có lỗi trong quá trình thực hiện.", "Thông báo", MessageBoxButtons.OK);
			else MessageBox.Show("Thực hiện thành công.", "Thông báo", MessageBoxButtons.OK);
		}
		private void sua2(DataGridView dgv) {
			dgv.EndEdit();
			int dem;
			int kq_updateGio = 0, kq_insertLichSuSuaGio = 0;
			int temp_UserEnrollNumber = 0, temp_MachineNo = 0, temp_WorkCode = -1;
			string temp_Source = string.Empty, temp_OriginType = string.Empty;
			DateTime temp_TimeStrOld = new DateTime();
			DateTime temp_TimeStrNew = dateTimeGioSua.Value;

			//Type tp1 = dgv.Rows[0].Cells.GetType();
			List<DataGridViewRow> gridRow_Checked = new List<DataGridViewRow>();
			List<DataGridViewRow> gridRow_Unchecked = new List<DataGridViewRow>();
			//1. duyệt từng dòng trong dataGrid, dòng nào check thì add vào list gridRow_checked
			for (dem = 0; dem < dgv.Rows.Count; dem++) {
				object formattedValue = dgv.Rows[dem].Cells[0].Value;
				bool checkedRows = formattedValue != null && (bool)formattedValue;
				if (checkedRows) gridRow_Checked.Add(dgv.Rows[dem]);
				else gridRow_Unchecked.Add(dgv.Rows[dem]);
			}
			//2. duyệt qua từng gridRow_checked , update xuống csdl
			for (dem = 0; dem < gridRow_Checked.Count; dem++) {
				object formattedValue1 = gridRow_Checked[dem].Cells[1].Value;//cell 1 UserEnrollNumber  ,
				object formattedValue4 = gridRow_Checked[dem].Cells[4].Value;// cell 4 TimeStrInOld 
				object formattedValue5 = gridRow_Checked[dem].Cells[5].Value;// cell 5 TimeStrOutOld 
				object formattedValue6 = gridRow_Checked[dem].Cells[6].Value;// cell 6 machineNo
				object formattedValue7 = gridRow_Checked[dem].Cells[7].Value;// cell 7 source 
				object formattedValue8 = gridRow_Checked[dem].Cells[8].Value;// cell 8 origintype
				object formattedValueNewType = gridRow_Checked[dem].Cells[9].Value;// cell 9 newtype
				object formattedValue10 = gridRow_Checked[dem].Cells[10].Value;// cell 10 workcode
				if (formattedValue4 == null || formattedValue4.ToString() == string.Empty)
					temp_TimeStrOld = (DateTime)formattedValue5;
				else if (formattedValue5 == null || formattedValue5.ToString() == string.Empty)
					temp_TimeStrOld = (DateTime)formattedValue4;

				if (formattedValue1 != null && formattedValue6 != null && formattedValue7 != null && formattedValue8 != null) {
					temp_UserEnrollNumber = int.Parse(formattedValue1.ToString());   // cell 1 UserEnrollNumber  , cell 2 tên nv
					temp_MachineNo = int.Parse(formattedValue6.ToString());
					temp_Source = formattedValue7.ToString();
					temp_OriginType = formattedValue8.ToString();
					if (formattedValue10 != null) temp_WorkCode = (int)formattedValue10;
				}

				#region update vào bảng checkinout

				kq_updateGio = ExecuteUpdateGio(temp_UserEnrollNumber, temp_TimeStrOld, temp_TimeStrNew);

				#endregion
				if (kq_updateGio == 0) break;
				#region insert vào bảng lịch sử

				kq_insertLichSuSuaGio = ExecuteInsertLichSuSua(temp_UserEnrollNumber
															   , temp_TimeStrOld
															   , temp_TimeStrNew
															   , temp_Source
															   , temp_MachineNo
															   , temp_WorkCode
															   , _mCurrentUserID
															   , textBoxLyDo.Text
															   , textBoxGhiChu.Text
															   , 0);

				#endregion
				if (kq_insertLichSuSuaGio == 0) break;
			}
			if (kq_insertLichSuSuaGio == 0 || kq_updateGio == 0)
				MessageBox.Show("Có lỗi trong quá trình thực hiện.",  "Thông báo", MessageBoxButtons.OK);
			else MessageBox.Show("Thực hiện thành công.",  "Thông báo", MessageBoxButtons.OK);
		}

		private int ExecuteInsertLichSuSua(int temp_UserEnrollNumber, DateTime temp_TimeStrOld, DateTime temp_TimeStrNew, string temp_Source, int temp_MachineNo, int tempWorkCode, int pCurrentUserID, string pLydo, string pGhiChu, int pCommandType) {
			object pWorkCode = new object();
			if (tempWorkCode == -1) pWorkCode = DBNull.Value;
			else pWorkCode = tempWorkCode;

			return SqlDataAccessHelper.ExecNoneQueryString(InsertStringBackupThemGioVaoRa()
																	, new[] { "@UserEnrollNumber"
                                                        ,"@TimeDate","@TimeStrOld","@TimeStrNew"
                                                        ,"@Source","@MachineNo","@WorkCode"
                                                        ,"@UserID"
                                                        ,"@Explain","@Note"
                                                        //, Execute time lấy giờ của sqlserver
                                                        ,"@CommandType"}
					, new object[] {  temp_UserEnrollNumber
                                                        , temp_TimeStrOld.Date, temp_TimeStrOld, temp_TimeStrNew
                                                        , temp_Source/* cell 6 "@Source"#1#, temp_MachineNo/*cell "@MachineNo"#1#, pWorkCode // cell 9 [TBD] giá trị này chưa biết nên để là 0"@WorkCode"
                                                        , pCurrentUserID //[TBD] lấy current user id"@UserID"
                                                        , pLydo , pGhiChu 
                                                        , pCommandType // [TBD] @CommandType +1 nếu là thêm, 0 nếu là sửa, -1 nếu là xóa
*/
                                    });
		}
		private int ExecuteUpdateGio(int pUserEnrollNumber, DateTime pTimeStrOld, DateTime temp_TimeStrNew) {
			return SqlDataAccessHelper.ExecNoneQueryString(
					UpdateStringSuaGioVaoRa()
					, new[] { 
                                                        "@UserEnrollNumber"
                                                        ,"@TimeStr_Old"
                                                        ,"@TimeDate_New"
                                                        ,"@TimeStr_New"}
					, new object[] { 
                                                            pUserEnrollNumber
                                                        , pTimeStrOld //[TBD]@TimeStr_Old
                                                        , temp_TimeStrNew.Date //[TBD]@TimeDate_New
                                                        , temp_TimeStrNew // [TBD]@TimeStr_New
                                    });
		}


		private void tabControlChiTiet_Selected(object sender, TabControlEventArgs e) {
			_activeTab = e.TabPageIndex;
		}

		private void comboBoxDSCa_SelectedIndexChanged(object sender, EventArgs e) {
			// mỗi lần thay đổi ca thì cập nhật lại giờ cho dateTimeGioSua
			if (comboBoxDSCa.SelectedIndex != 0) {
				dateTimeGioSua.ValueChanged -= dateTimeGioSua_ValueChanged;
				dateTimeGioSua.Value = new DateTime(dateTimeGioSua.Value.Year
					, dateTimeGioSua.Value.Month
					, dateTimeGioSua.Value.Day
					, (radioButtonSuaGioVao.Checked) ? _dsGioVaoCa[comboBoxDSCa.SelectedIndex - 1].Hours : _dsGioRaCa[comboBoxDSCa.SelectedIndex - 1].Hours // (index - 1) vì không tính item tùy chỉnh ko có giờ vào ra
					, (radioButtonSuaGioVao.Checked) ? _dsGioVaoCa[comboBoxDSCa.SelectedIndex - 1].Minutes : _dsGioRaCa[comboBoxDSCa.SelectedIndex - 1].Minutes
					, 0);
				dateTimeGioSua.ValueChanged += (dateTimeGioSua_ValueChanged);
			}

		}


		private void buttonThoat_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void dataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
			if (e.ColumnIndex == 0) {
				DataGridView dataGridView = sender as DataGridView;
				if (dataGridView != null) {
					switch (dataGridView.Name) {
						case "dataGridCTGioVao":
							checkAllCTGioVao = !checkAllCTGioVao;
							hasCheckDSNV = checkAllCTGioVao;
							for (int dem = 0; dem < dataGridCTGioVao.Rows.Count; dem++)
								dataGridCTGioVao.Rows[dem].Cells[0].Value = checkAllCTGioVao;

							dataGridCTGioVao.EndEdit();
							break;
						case "dataGridCTGioRa":
							checkAllCTGioRa = !checkAllCTGioRa;
							hasCheckDSNV = checkAllCTGioRa;
							for (int dem = 0; dem < dataGridCTGioRa.Rows.Count; dem++)
								dataGridCTGioRa.Rows[dem].Cells[0].Value = checkAllCTGioRa;

							dataGridCTGioRa.EndEdit();
							break;
						case "dataGridGioBT":
							checkAllGioBT = !checkAllGioBT;
							hasCheckDSNV = checkAllGioBT;
							for (int dem = 0; dem < dataGridGioBT.Rows.Count; dem++)
								dataGridGioBT.Rows[dem].Cells[0].Value = checkAllGioBT;

							dataGridGioBT.EndEdit();
							break;
					}
				}
			}
		}

		private void button1_Click(object sender, EventArgs e) {


		}

		private void btnThem_Click(object sender, EventArgs e) {
			if (MessageBox.Show("Thực hiện thay đổi giờ chấm công hàng loạt cho Nhân viên. Nhấn OK để thực hiện. Nhấn Cancel để huỷ.", "Hỏi lại", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
				return;

			int temp_UserEnrollNumber = 0, tempMachineNo = radioButtonSuaGioVao.Checked ? 1 : 2;
			int dem;
			int kq_insertGio = 0, kq_insertLichSuSuaGio = 0;
			DateTime temp_TimeStrNew = dateTimeGioSua.Value;

			//Type tp1 = dgv.Rows[0].Cells.GetType();
			List<DataGridViewRow> gridRow_Checked = new List<DataGridViewRow>();
			List<DataGridViewRow> gridRow_Unchecked = new List<DataGridViewRow>();
			//1. duyệt từng dòng trong dataGrid, dòng nào check thì add vào list gridRow_checked
			DataGridView dgv = new DataGridView();
			if (_activeTab == 1) dgv = dataGridCTGioVao;
			else if (_activeTab == 2) dgv = dataGridCTGioRa;
			else if (_activeTab == 3) dgv = dataGridGioBT;

			for (dem = 0; dem < dgv.Rows.Count; dem++) {
				object formattedValue = dgv.Rows[dem].Cells[0].Value;
				bool checkedRows = formattedValue != null && (bool)formattedValue;
				if (checkedRows) gridRow_Checked.Add(dgv.Rows[dem]);
				else gridRow_Unchecked.Add(dgv.Rows[dem]);
			}
			//2. duyệt qua từng gridRow_checked , update xuống csdl
			List<int> DSNVThemGio = new List<int>();
			for (dem = 0; dem < gridRow_Checked.Count; dem++) {
				object formattedValue1 = gridRow_Checked[dem].Cells[1].Value; //cell 1 UserEnrollNumber  ,

				if (formattedValue1 != null)
					temp_UserEnrollNumber = int.Parse(formattedValue1.ToString()); // cell 1 UserEnrollNumber 
				if (DSNVThemGio.Count != 0 && DSNVThemGio.IndexOf(temp_UserEnrollNumber) != -1) continue;

				DSNVThemGio.Add(temp_UserEnrollNumber);

				#region insert vào bảng checkinout

				kq_insertGio = SqlDataAccessHelper.ExecNoneQueryString(insertStringThemGioVaoRa()
																			, new string[]
                                                                            {
                                                                                "@UserEnrollNumber"
                                                                                , "@TimeDate"
                                                                                , "@TimeStr"
                                                                                , "@Source"
                                                                                , "@MachineNo"
                                                                                , "@WorkCode"
                                                                            }
																			, new object[]
                                                                            {
                                                                                temp_UserEnrollNumber
                                                                                , temp_TimeStrNew.Date
                                                                                , temp_TimeStrNew
                                                                                , "PC"
                                                                                , tempMachineNo
                                                                                , 0
                                                                            });

				#endregion

				if (kq_insertGio == 0) break;

				#region insert vào bảng lịch sử

				kq_insertLichSuSuaGio = ExecuteInsertLichSuSua(temp_UserEnrollNumber
																, temp_TimeStrNew.Date
																, temp_TimeStrNew
																, "PC"
																, tempMachineNo
																, 0
																, _mCurrentUserID
																, textBoxLyDo.Text
																, textBoxGhiChu.Text
																, 1);

				#endregion

				if (kq_insertLichSuSuaGio == 0) break;
			}
			if (kq_insertLichSuSuaGio == 0 || kq_insertGio == 0)
				MessageBox.Show("Có lỗi trong quá trình thực hiện.", "Thông báo", MessageBoxButtons.OK);
			else MessageBox.Show("Thực hiện thành công.", "Thông báo", MessageBoxButtons.OK);

			SuaGioChamCong_Load(null, null);
		}

	}
}
