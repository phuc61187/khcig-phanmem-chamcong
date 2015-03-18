using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapNhatGioChamCong.BUS;
using CapNhatGioChamCong.DAO;
using CapNhatGioChamCong.DTO;
using log4net;

namespace CapNhatGioChamCong {

	public partial class frm_ChiTietVaoRa : Form {
		private readonly ILog log = LogManager.GetLogger("ChiTietVaoRa");
		public List<cUserInfo> flstDSNVChk { get; set; }
		public DataTable tableShow { get; set; }
		private readonly CheckBox chkBoxAllDSNV = new CheckBox();
		public object[,] xemCT { get; set; }
		public DateTime fNgayBD { get; set; }
		public DateTime fNgayKT { get; set; }

		void checkAll_CheckedChanged(object sender, EventArgs e) {
			DataGridView tempGrid = dgrdTongHop;
			if (tempGrid.Rows.Count == 0) return;

			//3. có dữ liệu, duyệt qua hết và gán giá trị check (or uncheck), sau đó endEDIT lại lưới để dòng đầu tiên có giá trị
			bool tmpCheckAll = ((CheckBox)sender).Checked;

			tempGrid.BeginEdit(true);
			DataTable dt = tempGrid.DataSource as DataTable;

			if (dt.Rows.Count != 0) {
				foreach (DataRow row in dt.Rows) {
					row["check"] = tmpCheckAll;
				}
			}

			tempGrid.EndEdit();
			tempGrid.Update();
		}

		public frm_ChiTietVaoRa() {
			InitializeComponent();

			ThamSo.VeCheckBox_CheckAll(dgrdTongHop, chkBoxAllDSNV, checkAll_CheckedChanged, new Point(7, 3));
			tabControl1.SelectedIndex = 0;
		}

		private void frm_ChiTietVaoRa_Load(object sender, EventArgs e) {
			loadTable();
		}

		private void loadTable() {
			DataTable table = (dgrdTongHop.DataSource) as DataTable;
			if (table == null || table.Rows.Count != 0)
				table = TaoCauTrucTableCTGioVaoRa();
			else table.Rows.Clear();
			//[TBD] nhớ check null
			int lastRowIndex = (int)xemCT[0, 0];
			for (int i = 1; i < lastRowIndex; i++) {
				int UserEnrollNumber = (int)xemCT[i, 1];
				cUserInfo tmpNV = flstDSNVChk.Find(o => o.UserEnrollNumber == UserEnrollNumber);

				int slcot = (int)xemCT[i, 0];
				int j = 2;
				while (j < slcot && xemCT[i, j] != null) {
					DateTime ngay = (DateTime)xemCT[i, j];
					cNgayCong tmpNgayCong = tmpNV.DSNgayCong.Find(o => o.NgayCong.Date == ngay.Date);
					//DataRow row = table.NewRow();
					#region neu ngày đó vắng => các giá trị null
					if (tmpNgayCong.HasCheck == false) {
						DataRow subrow = table.NewRow();
						subrow["check"] = false;
						subrow["UserEnrollNumber"] = tmpNV.UserEnrollNumber;
						subrow["UserFullName"] = tmpNV.UserFullName;
						subrow["TimeStrNgay"] = subrow["TimeStrThu"] = tmpNgayCong.NgayCong;
						subrow["TimeStrVao"] = subrow["TimeStrRa"] = subrow["ShiftCode"] = subrow["ShiftID"] = DBNull.Value;
						table.Rows.Add(subrow);
						j++;
						continue;
					}
					#endregion
					#region nếu có check thì gán giá trị
					foreach (cChkInOut chkInOut in tmpNgayCong.DSVaoRa) {
						DataRow subrow = table.NewRow();
						subrow["check"] = false;
						subrow["UserEnrollNumber"] = tmpNV.UserEnrollNumber;
						subrow["UserFullName"] = tmpNV.UserFullName;
						subrow["TimeStrNgay"] = subrow["TimeStrThu"] = tmpNgayCong.NgayCong;
						subrow["TimeStrVao"] = (chkInOut.Vao != null) ? chkInOut.Vao.TimeStr : (object)DBNull.Value;
						subrow["TimeStrRa"] = (chkInOut.Raa != null) ? chkInOut.Raa.TimeStr : (object)DBNull.Value;
						subrow["ShiftCode"] = (chkInOut.ThuocCa != null) ? chkInOut.ThuocCa.ShiftCode : (object)DBNull.Value;
						subrow["ShiftID"] = (chkInOut.ThuocCa != null) ? chkInOut.ThuocCa.ShiftID : (object)DBNull.Value;
						table.Rows.Add(subrow);
					}
					#endregion
					j++;
				}
			}
			dgrdTongHop.DataSource = table;
			dgrdTongHop.Select();
		}

		private DataTable TaoCauTrucTableCTGioVaoRa() {
			DataTable kq = new DataTable();
			kq.Columns.Add("check", typeof(bool)); //0
			kq.Columns.Add("UserEnrollNumber", typeof(int)); //0
			kq.Columns.Add("UserFullName", typeof(string)); //1
			kq.Columns.Add("TimeStrNgay", typeof(DateTime)); //2
			kq.Columns.Add("TimeStrThu", typeof(DateTime)); //3
			kq.Columns.Add("TimeStrVao", typeof(DateTime)); //4
			kq.Columns.Add("TimeStrRa", typeof(DateTime)); //5
			kq.Columns.Add("ShiftCode", typeof(string)); //8
			kq.Columns.Add("ShiftID", typeof(int)); //9

			return kq;
		}

		private void dgrdTongHop_RowEnter(object sender, DataGridViewCellEventArgs e) {
			DataTable tmpTable = (dgrdTongHop.DataSource as DataTable);
			if (tmpTable == null || tmpTable.Rows.Count == 0) return;
			if (e.RowIndex == -1) return;
			DataRowView dataRowView = dgrdTongHop.Rows[e.RowIndex].DataBoundItem as DataRowView;
			dgrdTongHop.Tag = dataRowView;
			if (dataRowView == null) return;

			int tmpUserEnrollNumber = (int)dataRowView["UserEnrollNumber"];
			cUserInfo tmpNV = flstDSNVChk.Find(o => o.UserEnrollNumber == tmpUserEnrollNumber);
			DateTime tmpNgayCong = (DateTime)dataRowView["TimeStrNgay"];
			//test += "\nTimeStrNgay:" + tmpNgayCong.ToShortDateString() + " ";
			DateTime tmpVao = (dataRowView["TimeStrVao"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dataRowView["TimeStrVao"];
			DateTime tmpRa = (dataRowView["TimeStrRa"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dataRowView["TimeStrRa"];
			//test += "\ttmpVao:" + tmpVao.ToShortTimeString() + " \ttmpRa" + tmpRa.ToShortTimeString();

			cNgayCong ngayCong = tmpNV.DSNgayCong.Find(o => o.NgayCong == tmpNgayCong);
			//test += tmpNV + "\n " + ngayCong;
			//log.Info(test);
			cChkInOut old_CIO = new cChkInOut();
			try {
				if (ngayCong.HasCheck == false || (tmpVao == DateTime.MinValue && tmpRa == DateTime.MinValue)) old_CIO = null;
				else if (tmpVao == DateTime.MinValue && tmpRa != DateTime.MinValue) old_CIO = ngayCong.DSVaoRa.Find(o => o.HaveINOUT == -1 && o.Raa.TimeStr == tmpRa);
				else if (tmpRa == DateTime.MinValue && tmpVao != DateTime.MinValue) old_CIO = ngayCong.DSVaoRa.Find(o => o.HaveINOUT == -2 && o.Vao.TimeStr == tmpVao);
				else old_CIO = ngayCong.DSVaoRa.Find(o => o.HaveINOUT > 0 && o.Vao.TimeStr == tmpVao && o.Raa.TimeStr == tmpRa);

				LoadTabThemGio(tmpNV, tmpNgayCong, old_CIO);
				LoadTabSuaGio(tmpNV, tmpNgayCong, old_CIO);
				cbLyDo_Xoa.SelectedIndex = 0;
				cbLyDo_Xoa.Text = string.Empty;
				tbGhiChu_Xoa.Text = string.Empty;

			} catch (Exception ex) {
				string temp = "\n--start \n NV: " + tmpNV + "\nCIO: ";
				temp += (old_CIO != null) ? old_CIO.ToString() : "null";
				temp += "\n--end";
				log.Fatal(ex.StackTrace + temp);
				AutoClosingMessageBox.Show("Có lỗi trong quá trình thao tác.", "Lỗi", 1500);
			}
		}

		private void LoadTabSuaGio(cUserInfo tmpNV, DateTime tmpNgayCong, cChkInOut old_CIO) {
			DateTime tmpVao, tmpRa;
			if (old_CIO == null) { tmpVao = tmpRa = DateTime.MinValue; }
			else {
				tmpVao = old_CIO.Vao == null ? DateTime.MinValue : old_CIO.Vao.TimeStr;
				tmpRa = old_CIO.Raa == null ? DateTime.MinValue : old_CIO.Raa.TimeStr;
			}
			tbTenNV_Sua.Text = tmpNV.UserFullName;
			cbLyDo_Sua.SelectedIndex = 0;
			cbLyDo_Sua.Text = string.Empty;
			tbGhiChu_Sua.Text = string.Empty;
			if (tmpVao != DateTime.MinValue) { // có giờ vào, cho phép input giờ vào
				checkVao_Sua.Enabled = true;
				checkVao_Sua.Checked = false;
				dtpVao_Sua.Enabled = false;
				dtpVao_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
				dtpVao_Sua.Value = tmpVao;
				dtpVao_Sua.ValueChanged += dtp_Sua_OnValueChanged;
				cbCaVao_Sua.Enabled = false;
				LoadComboBoxSua(cbCaVao_Sua, tmpNV.DSCa);
			}
			else { // ko cho phép input
				checkVao_Sua.Enabled = false;
				checkVao_Sua.Checked = false;
				dtpVao_Sua.Enabled = false;
				dtpVao_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
				dtpVao_Sua.Value = tmpNgayCong;
				cbCaVao_Sua.Enabled = false;
				LoadComboBoxSua(cbCaVao_Sua, null);
				//dtpVao_Sua.Value = tmpVao;
			}

			if (tmpRa != DateTime.MinValue) { // có giờ ra, cho phép input giờ ra
				checkRa_Sua.Enabled = true;
				checkRa_Sua.Checked = false;
				dtpRa_Sua.Enabled = false;
				dtpRa_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
				dtpRa_Sua.Value = tmpRa;
				dtpRa_Sua.ValueChanged += dtp_Sua_OnValueChanged;
				cbCaRa_Sua.Enabled = false;
				LoadComboBoxSua(cbCaRa_Sua, tmpNV.DSCa);
			}
			else {
				checkRa_Sua.Enabled = false;
				checkRa_Sua.Checked = false;
				dtpRa_Sua.Enabled = false;
				dtpRa_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
				dtpRa_Sua.Value = tmpNgayCong;
				cbCaRa_Sua.Enabled = false;
				LoadComboBoxSua(cbCaRa_Sua, null);
				//dtpRa_Sua.Value = tmpRa;
			}
		}

		private void LoadTabThemGio(cUserInfo tmpNV, DateTime tmpNgayCong, cChkInOut old_CIO) {
			DateTime tmpVao, tmpRa;
			if (old_CIO == null) { tmpVao = tmpRa = DateTime.MinValue; }
			else {
				tmpVao = old_CIO.Vao == null ? DateTime.MinValue : old_CIO.Vao.TimeStr;
				tmpRa = old_CIO.Raa == null ? DateTime.MinValue : old_CIO.Raa.TimeStr;
			}
			tbTenNV_Them.Text = tmpNV.UserFullName;
			cbLyDo_Them.SelectedIndex = 0;
			cbLyDo_Them.Text = string.Empty;
			tbGhiChu_Them.Text = string.Empty;
			if (tmpVao == DateTime.MinValue) { // ko có giờ vào, cho phép input giờ vào
				checkVao_Them.Enabled = true;
				checkVao_Them.Checked = true;
				dtpVao_Them.Enabled = true;
				dtpVao_Them.ValueChanged -= dtp_Them_OnValueChanged;
				dtpVao_Them.Value = tmpNgayCong;
				dtpVao_Them.ValueChanged += dtp_Them_OnValueChanged;
				cbCaVao_Them.Enabled = true;
				LoadComboBoxThem(cbCaVao_Them, tmpNV.DSCa);
			}
			else { // ko cho phép input
				//checkVao_Them.Enabled = false; //new 3/3/2014
				checkVao_Them.Checked = false;
				//dtpVao_Them.Enabled = false;//new 3/3/2014
				dtpVao_Them.ValueChanged -= dtp_Them_OnValueChanged;
				dtpVao_Them.Value = tmpVao;
				cbCaVao_Them.Enabled = false;
				LoadComboBoxThem(cbCaVao_Them, tmpNV.DSCa); //new 3/3/2014->old://LoadComboBoxThem(cbCaVao_Them, null);
			}

			if (tmpRa == DateTime.MinValue) { // ko có giờ ra, cho phép input giờ ra
				checkRa_Them.Enabled = true;
				checkRa_Them.Checked = true;
				dtpRa_Them.Enabled = true;
				dtpRa_Them.ValueChanged -= dtp_Them_OnValueChanged;
				dtpRa_Them.Value = tmpNgayCong;
				dtpRa_Them.ValueChanged += dtp_Them_OnValueChanged;
				cbCaRa_Them.Enabled = true;
				LoadComboBoxThem(cbCaRa_Them, tmpNV.DSCa);
			}
			else {
				//checkRa_Them.Enabled = false;//new 3/3/2014
				checkRa_Them.Checked = false;
				//dtpRa_Them.Enabled = false;//new 3/3/2014
				dtpRa_Them.ValueChanged -= dtp_Them_OnValueChanged;
				dtpRa_Them.Value = tmpRa;
				cbCaRa_Them.Enabled = false;
				LoadComboBoxThem(cbCaRa_Them, tmpNV.DSCa); //new 3/3/2014 ->old://LoadComboBoxThem(cbCaRa_Them, null);
			}
		}

		private void dgrdTongHop_TabIndexChanged(object sender, EventArgs e) {
			if (tabControl1.SelectedTab == tabXoaGio || tabControl1.SelectedTab == tabDaoGio) {
				dgrdTongHop.Columns[0].Visible = true;
				chkBoxAllDSNV.Show();
			}
			else {
				dgrdTongHop.Columns[0].Visible = false;
				chkBoxAllDSNV.Hide();
			}
			dgrdTongHop.Update();
		}

		#region them
		private void checkBox_Them_CheckedChanged(object sender, EventArgs e) {
			if (sender == checkVao_Them) {
				if (checkVao_Them.Checked) {
					DataRowView row = (DataRowView)dgrdTongHop.Tag;
					cUserInfo tmpNV = flstDSNVChk.Find(o => o.UserEnrollNumber == (int)row["UserEnrollNumber"]);
					dtpVao_Them.ValueChanged -= dtp_Them_OnValueChanged;
					dtpVao_Them.Value = ((DateTime)row["TimeStrNgay"]).Date;
					LoadComboBoxThem(cbCaVao_Them, tmpNV.DSCa);
					dtpVao_Them.ValueChanged += dtp_Them_OnValueChanged;
					dtpVao_Them.Enabled = true;
					cbCaVao_Them.Enabled = true;
				}
				else {
					dtpVao_Them.ValueChanged -= dtp_Them_OnValueChanged;
					LoadComboBoxThem(cbCaVao_Them, null);
					DataRowView row = (DataRowView)dgrdTongHop.Tag;
					dtpVao_Them.Value = ((DateTime)row["TimeStrNgay"]).Date;
					dtpVao_Them.Enabled = false;
					cbCaVao_Them.Enabled = false;
				}
			}
			else if (sender == checkRa_Them) {
				if (checkRa_Them.Checked) {
					DataRowView row = (DataRowView)dgrdTongHop.Tag;
					cUserInfo tmpNV = flstDSNVChk.Find(o => o.UserEnrollNumber == (int)row["UserEnrollNumber"]);
					dtpRa_Them.ValueChanged -= dtp_Them_OnValueChanged;
					dtpRa_Them.Value = ((DateTime)row["TimeStrNgay"]).Date;
					dtpRa_Them.ValueChanged += dtp_Them_OnValueChanged;
					LoadComboBoxThem(cbCaRa_Them, tmpNV.DSCa);
					dtpRa_Them.Enabled = true;
					cbCaRa_Them.Enabled = true;
				}
				else {
					dtpRa_Them.ValueChanged -= dtp_Them_OnValueChanged;
					LoadComboBoxThem(cbCaRa_Them, null);
					DataRowView row = (DataRowView)dgrdTongHop.Tag;
					dtpRa_Them.Value = ((DateTime)row["TimeStrNgay"]).Date;
					dtpRa_Them.Enabled = false;
					cbCaRa_Them.Enabled = false;
				}
			}
		}

		private void cbCa_Them_OnSelectedIndexChanged(object sender, EventArgs eventArgs) {
			if (sender == cbCaVao_Them) {
				cShift tmpshift = (sender as ComboBox).SelectedItem as cShift;
				DataRowView row = dgrdTongHop.Tag as DataRowView;
				DateTime tmpVao = (DateTime)row["TimeStrNgay"];
				tmpVao = tmpVao.Date.Add(tmpshift.OnnDutyTS);
				//[1]dtpVao_Them.ValueChanged -= dtp_Them_OnValueChanged;
				dtpVao_Them.Value = tmpVao;
				//[1]dtpVao_Them.ValueChanged += dtp_Them_OnValueChanged;
				dtpVao_Them.Update();
			}
			else {
				cShift tmpshift = (sender as ComboBox).SelectedItem as cShift;
				DataRowView row = dgrdTongHop.Tag as DataRowView;
				DateTime tmpRa = (DateTime)row["TimeStrNgay"];
				tmpRa = tmpRa.Date.Add(tmpshift.OffDutyTS);
				//[1]dtpRa_Them.ValueChanged -= dtp_Them_OnValueChanged;
				dtpRa_Them.Value = tmpRa;
				//[1]dtpRa_Them.ValueChanged += dtp_Them_OnValueChanged;
				dtpRa_Them.Update();
			}

		}

		private void dtp_Them_OnValueChanged(object sender, EventArgs eventArgs) {
			if (sender == dtpVao_Them) {
				cShift tmpshift = cbCaVao_Them.SelectedItem as cShift;
				DataRowView row = dgrdTongHop.Tag as DataRowView;
				DateTime tmpVao = (DateTime)row["TimeStrNgay"];
				tmpVao = tmpVao.Date.Add(tmpshift.OnnDutyTS);
				DateTime inputTime = dtpVao_Them.Value;
				if (inputTime != tmpVao) {
					cbCaVao_Them.SelectedIndexChanged -= cbCa_Them_OnSelectedIndexChanged;
					cbCaVao_Them.SelectedIndex = 0;
					cbCaVao_Them.SelectedIndexChanged += cbCa_Them_OnSelectedIndexChanged;
				}
				dtpVao_Them.Update();
			}
			else {
				cShift tmpshift = cbCaRa_Them.SelectedItem as cShift;
				DataRowView row = dgrdTongHop.Tag as DataRowView;
				DateTime tmpRa = (DateTime)row["TimeStrNgay"];
				tmpRa = tmpRa.Date.Add(tmpshift.OffDutyTS);
				DateTime inputTime = dtpRa_Them.Value;
				if (inputTime != tmpRa) {
					cbCaRa_Them.SelectedIndexChanged -= cbCa_Them_OnSelectedIndexChanged;
					cbCaRa_Them.SelectedIndex = 0;
					cbCaRa_Them.SelectedIndexChanged += cbCa_Them_OnSelectedIndexChanged;
				}
				dtpRa_Them.Update();
			}
		}

		private void LoadComboBoxThem(ComboBox comboBox, List<cShift> pDSCa) {
			if (comboBox == cbCaVao_Them) {
				if (pDSCa == null) {
					cbCaVao_Them.SelectedIndexChanged -= cbCa_Them_OnSelectedIndexChanged;
					cbCaVao_Them.DataSource = null;
					cbCaVao_Them.Update();
				}
				else {
					List<cShift> tmpDSCaCopy = new List<cShift>(pDSCa);
					tmpDSCaCopy.Insert(0, new cShift() { ShiftCode = "Tuỳ chỉnh", ShiftID = int.MinValue, LoaiCa = 1 });
					cbCaVao_Them.SelectedIndexChanged -= cbCa_Them_OnSelectedIndexChanged;
					cbCaVao_Them.DataSource = tmpDSCaCopy;
					cbCaVao_Them.DisplayMember = "ShiftCode";
					cbCaVao_Them.ValueMember = "ShiftID";
					cbCaVao_Them.SelectedIndex = 0;
					cbCaVao_Them.Update();
					cbCaVao_Them.SelectedIndexChanged += cbCa_Them_OnSelectedIndexChanged;
				}
			}
			else {
				if (pDSCa == null) {
					cbCaRa_Them.SelectedIndexChanged -= cbCa_Them_OnSelectedIndexChanged;
					cbCaRa_Them.DataSource = null;
					cbCaRa_Them.Update();
				}
				else {
					List<cShift> tmpDSCaCopy = new List<cShift>(pDSCa);
					tmpDSCaCopy.Insert(0, new cShift() { ShiftCode = "Tuỳ chỉnh", ShiftID = int.MinValue, LoaiCa = 1 });
					cbCaRa_Them.SelectedIndexChanged -= cbCa_Them_OnSelectedIndexChanged;
					cbCaRa_Them.DataSource = tmpDSCaCopy;
					cbCaRa_Them.DisplayMember = "ShiftCode";
					cbCaRa_Them.ValueMember = "ShiftID";
					cbCaRa_Them.SelectedIndex = 0;
					cbCaRa_Them.Update();
					cbCaRa_Them.SelectedIndexChanged += cbCa_Them_OnSelectedIndexChanged;
				}
			}
		}

		#endregion

		#region sua
		private void checkBox_Sua_CheckedChanged(object sender, EventArgs e) {
			if (sender == checkVao_Sua) {
				if (checkVao_Sua.Checked) {
					DataRowView row = (DataRowView)dgrdTongHop.Tag;
					cUserInfo tmpNV = flstDSNVChk.Find(o => o.UserEnrollNumber == (int)row["UserEnrollNumber"]);
					dtpVao_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
					dtpVao_Sua.Value = ((DateTime)row["TimeStrNgay"]);
					LoadComboBoxSua(cbCaVao_Sua, tmpNV.DSCa);
					dtpVao_Sua.ValueChanged += dtp_Sua_OnValueChanged;
					dtpVao_Sua.Enabled = true;
					cbCaVao_Sua.Enabled = true;
				}
				else {
					DataRowView row = (DataRowView)dgrdTongHop.Tag;
					dtpVao_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
					dtpVao_Sua.Value = ((DateTime)row["TimeStrNgay"]);
					LoadComboBoxSua(cbCaVao_Sua, null);
					dtpVao_Sua.Enabled = false;
					cbCaVao_Sua.Enabled = false;
				}
			}
			else {
				if (checkRa_Sua.Checked) {
					DataRowView row = (DataRowView)dgrdTongHop.Tag;
					cUserInfo tmpNV = flstDSNVChk.Find(o => o.UserEnrollNumber == (int)row["UserEnrollNumber"]);
					dtpRa_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
					dtpRa_Sua.Value = ((DateTime)row["TimeStrNgay"]);
					LoadComboBoxSua(cbCaRa_Sua, tmpNV.DSCa);
					dtpRa_Sua.ValueChanged += dtp_Sua_OnValueChanged;
					dtpRa_Sua.Enabled = true;
					cbCaRa_Sua.Enabled = true;
				}
				else {
					DataRowView row = (DataRowView)dgrdTongHop.Tag;
					dtpRa_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
					dtpRa_Sua.Value = ((DateTime)row["TimeStrNgay"]);
					LoadComboBoxSua(cbCaRa_Sua, null);
					dtpRa_Sua.Enabled = false;
					cbCaRa_Sua.Enabled = false;
				}
			}
		}

		private void cbCa_Sua_OnSelectedIndexChanged(object sender, EventArgs e) {
			if (sender == cbCaVao_Sua) {
				cShift tmpshift = (sender as ComboBox).SelectedItem as cShift;
				DataRowView row = dgrdTongHop.Tag as DataRowView;
				DateTime tmpVao = (DateTime)row["TimeStrNgay"];
				tmpVao = tmpVao.Date.Add(tmpshift.OnnDutyTS);
				//[1]dtpVao_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
				dtpVao_Sua.Value = tmpVao;
				//[1]dtpVao_Sua.ValueChanged += dtp_Sua_OnValueChanged;
				dtpVao_Sua.Update();
			}
			else {
				cShift tmpshift = (sender as ComboBox).SelectedItem as cShift;
				DataRowView row = dgrdTongHop.Tag as DataRowView;
				DateTime tmpRa = (DateTime)row["TimeStrNgay"];
				tmpRa = tmpRa.Date.Add(tmpshift.OffDutyTS);
				//[1]dtpRa_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
				dtpRa_Sua.Value = tmpRa;
				//[1]dtpRa_Sua.ValueChanged += dtp_Sua_OnValueChanged;
				dtpRa_Sua.Update();
			}

		}

		private void dtp_Sua_OnValueChanged(object sender, EventArgs e) {
			if (sender == dtpVao_Sua) {
				cShift tmpshift = cbCaVao_Sua.SelectedItem as cShift;
				DataRowView row = dgrdTongHop.Tag as DataRowView;
				DateTime tmpVao = (DateTime)row["TimeStrNgay"];
				tmpVao = tmpVao.Date.Add(tmpshift.OnnDutyTS);
				DateTime inputTime = dtpVao_Sua.Value;
				if (inputTime != tmpVao) {
					cbCaVao_Sua.SelectedIndexChanged -= cbCa_Sua_OnSelectedIndexChanged;
					cbCaVao_Sua.SelectedIndex = 0;
					cbCaVao_Sua.SelectedIndexChanged += cbCa_Sua_OnSelectedIndexChanged;
				}
				dtpVao_Sua.Update();
			}
			else {
				cShift tmpshift = cbCaRa_Sua.SelectedItem as cShift;
				DataRowView row = dgrdTongHop.Tag as DataRowView;
				DateTime tmpRa = (DateTime)row["TimeStrNgay"];
				tmpRa = tmpRa.Date.Add(tmpshift.OffDutyTS);
				DateTime inputTime = dtpRa_Sua.Value;
				if (inputTime != tmpRa) {
					cbCaRa_Sua.SelectedIndexChanged -= cbCa_Sua_OnSelectedIndexChanged;
					cbCaRa_Sua.SelectedIndex = 0;
					cbCaRa_Sua.SelectedIndexChanged += cbCa_Sua_OnSelectedIndexChanged;
				}
				dtpRa_Sua.Update();
			}
		}

		private void LoadComboBoxSua(ComboBox comboBox, List<cShift> pDSCa) {
			if (comboBox == cbCaVao_Sua) {
				if (pDSCa == null) {
					cbCaVao_Sua.SelectedIndexChanged -= cbCa_Sua_OnSelectedIndexChanged;
					cbCaVao_Sua.DataSource = null;
				}
				else {
					List<cShift> tmpDSCaCopy = new List<cShift>(pDSCa);
					tmpDSCaCopy.Insert(0, new cShift() { ShiftCode = "Tuỳ chỉnh", ShiftID = int.MinValue, LoaiCa = 1 });
					cbCaVao_Sua.SelectedIndexChanged -= cbCa_Sua_OnSelectedIndexChanged;
					cbCaVao_Sua.DataSource = tmpDSCaCopy;
					cbCaVao_Sua.DisplayMember = "ShiftCode";
					cbCaVao_Sua.ValueMember = "ShiftID";
					cbCaVao_Sua.SelectedIndex = 0;
					cbCaVao_Sua.SelectedIndexChanged += cbCa_Sua_OnSelectedIndexChanged;
				}
			}
			else {
				if (pDSCa == null) {
					cbCaRa_Sua.SelectedIndexChanged -= cbCa_Sua_OnSelectedIndexChanged;
					cbCaRa_Sua.DataSource = null;
				}
				else {
					List<cShift> tmpDSCaCopy = new List<cShift>(pDSCa);
					tmpDSCaCopy.Insert(0, new cShift() { ShiftCode = "Tuỳ chỉnh", ShiftID = int.MinValue, LoaiCa = 1 });
					cbCaRa_Sua.SelectedIndexChanged -= cbCa_Sua_OnSelectedIndexChanged;
					cbCaRa_Sua.DataSource = tmpDSCaCopy;
					cbCaRa_Sua.DisplayMember = "ShiftCode";
					cbCaRa_Sua.ValueMember = "ShiftID";
					cbCaRa_Sua.SelectedIndex = 0;
					cbCaRa_Sua.SelectedIndexChanged += cbCa_Sua_OnSelectedIndexChanged;
				}
			}
		}

		#endregion

		private void btnDaoKieuChamCong_Click(object sender, EventArgs e) {
			dgrdTongHop.EndEdit();
			DataTable table = (dgrdTongHop.DataSource) as DataTable;
			if (table == null) return;
			DataRow[] arrRecord = table.Select("check = true", "UserEnrollNumber asc");
			if (arrRecord.Length == 0) {
				AutoClosingMessageBox.Show("Chưa chọn dòng dữ liệu thao tác.", "Thông báo", 2000);
				return;
			}

			bool flag = false;
			foreach (DataRow row in arrRecord) {
				// kiểm tra trước khi thực hiện
				if (row["TimeStrVao"] != DBNull.Value && row["TimeStrRa"] != DBNull.Value) {
					flag = true;
					continue;
				}
				cUserInfo tmpNV = flstDSNVChk.Find(item => item.UserEnrollNumber == (int)row["UserEnrollNumber"]);
				cNgayCong tmpNgay = tmpNV.DSNgayCong.Find(item => item.NgayCong == (DateTime)row["TimeStrNgay"]);
				DateTime tmpOldTime;
				cChk tmpChkOld;
				bool tmpIsCheckInOld;
				if (row["TimeStrVao"] != DBNull.Value) {
					tmpOldTime = (DateTime)row["TimeStrVao"];
					tmpChkOld = tmpNV.ds_CheckAuto.Find(item => item.TimeStr == tmpOldTime && item.GetType() == typeof(cChkIn));
					tmpIsCheckInOld = true;
				}
				else {
					tmpOldTime = (DateTime)row["TimeStrRa"];
					tmpChkOld = tmpNV.ds_CheckAuto.Find(item => item.TimeStr == tmpOldTime && item.GetType() == typeof(cChkOut));
					tmpIsCheckInOld = false;
				}
				if (tmpChkOld == null) {
					MessageBox.Show("Không thể sửa giờ đã được xác nhận.", "Thông báo");
					continue;
				}

				if (XL.SuaGioChoNV(tmpNV.UserEnrollNumber, tmpChkOld, tmpChkOld.TimeStr, !tmpIsCheckInOld, "Chấm nhầm máy", "Thay đổi Số máy check") == false) {
					MessageBox.Show("Không sửa được giờ cho nhân viên. Vui lòng thử lại.", "Lỗi");
					break;
				}
			}

			if (flag)
				AutoClosingMessageBox.Show("Không thể đảo ngược giờ có đủ giờ vào và giờ ra", "Thông báo", 2000);

			try {
				XL.XemCong(flstDSNVChk, fNgayBD, fNgayKT);
				loadTable();
			} catch (Exception) {
				MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại sau.", "Lỗi");
			}
			GC.Collect();
		}

		private void btnThemGio_Click(object sender, EventArgs e) {
			#region lấy thông tin
			DataRowView row = dgrdTongHop.Tag as DataRowView;
			if (row == null) return;
			cUserInfo tmpNV = flstDSNVChk.Find(o => o.UserEnrollNumber == (int)row["UserEnrollNumber"]);
			string Lydo = string.Empty;
			if (string.IsNullOrEmpty(cbLyDo_Them.Text) == false) {
				Lydo = cbLyDo_Them.Text;
			} else if (cbLyDo_Them.SelectedItem != null) {
				Lydo = (string)cbLyDo_Them.SelectedItem;
			}

			if (checkVao_Them.Checked) {
				dtpVao_Them.Update();
				if (XL.ThemGioChoNV(tmpNV, dtpVao_Them.Value.Add(new TimeSpan(0, 0, 1)), 21, Lydo, tbGhiChu_Them.Text) == false)
					MessageBox.Show("Không thêm được giờ vào cho nhân viên. Vui lòng thử lại.", "Lỗi");
			}
			if (checkRa_Them.Checked) {
				dtpRa_Them.Update();
				if (XL.ThemGioChoNV(tmpNV, dtpRa_Them.Value, 22, Lydo, tbGhiChu_Them.Text) == false)
					MessageBox.Show("Không thêm được giờ ra cho nhân viên. Vui lòng thử lại.", "Lỗi");

			}
			try {
				XL.XemCong(flstDSNVChk, fNgayBD, fNgayKT);
				loadTable();
				
				GC.Collect();

			} catch (Exception) {
				MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK);
			}
			#endregion
		}

		private void btnSuaGio_Click(object sender, EventArgs e) {
			DataRowView row = dgrdTongHop.Tag as DataRowView;
			if (row == null) return;
			cUserInfo tmpNV = flstDSNVChk.Find(o => o.UserEnrollNumber == (int)row["UserEnrollNumber"]);

			DateTime tmpOldVao = (row["TimeStrVao"] != DBNull.Value) ? (DateTime)row["TimeStrVao"] : DateTime.MinValue;
			DateTime tmpOldRaa = (row["TimeStrRa"] != DBNull.Value) ? (DateTime)row["TimeStrRa"] : DateTime.MinValue;
			DateTime tmpNgayCong = (DateTime)row["TimeStrNgay"];

			cNgayCong ngayCong = tmpNV.DSNgayCong.Find(o => o.NgayCong == tmpNgayCong);
			cChkInOut old_CIO;
			if (ngayCong.HasCheck == false || (tmpOldVao == DateTime.MinValue && tmpOldRaa == DateTime.MinValue)) old_CIO = null;
			else if (tmpOldVao == DateTime.MinValue) old_CIO = ngayCong.DSVaoRa.Find(o => o.HaveINOUT < 0 && o.Raa.TimeStr == tmpOldRaa);
			else if (tmpOldRaa == DateTime.MinValue) old_CIO = ngayCong.DSVaoRa.Find(o => o.HaveINOUT < 0 && o.Vao.TimeStr == tmpOldVao);
			else old_CIO = ngayCong.DSVaoRa.Find(o => o.HaveINOUT > 0 && o.Vao.TimeStr == tmpOldVao && o.Raa.TimeStr == tmpOldRaa);
			if (old_CIO != null && old_CIO.DaXN == true) {
				MessageBox.Show("Không thể sửa giờ đã được xác nhận.", "Thông báo");
				return;
			}

			string Lydo = string.Empty;
			if (string.IsNullOrEmpty(cbLyDo_Sua.Text) == false) {
				Lydo = cbLyDo_Sua.Text;
			}
			else if (cbLyDo_Sua.SelectedItem != null) {
				Lydo = (string)cbLyDo_Sua.SelectedItem;
			}

			if (checkVao_Sua.Checked) {
				dtpVao_Sua.Update();
				cChk tmpCheck = tmpNV.ds_CheckAuto.Find(o => o.TimeStr == tmpOldVao && o.MachineNo % 2 == 1);
				if (XL.SuaGioChoNV(tmpNV.UserEnrollNumber, tmpCheck, dtpVao_Sua.Value, true, Lydo, tbGhiChu_Sua.Text) == false) {
					MessageBox.Show("Không sửa được giờ cho nhân viên. Vui lòng thử lại.", "Lỗi");
				}

			}
			if (checkRa_Sua.Checked) {
				dtpRa_Sua.Update();
				cChk tmpCheck = tmpNV.ds_CheckAuto.Find(o => o.TimeStr == tmpOldRaa && o.MachineNo % 2 == 0);
				if (XL.SuaGioChoNV(tmpNV.UserEnrollNumber, tmpCheck, dtpRa_Sua.Value, false, Lydo, tbGhiChu_Sua.Text) == false) {
					MessageBox.Show("Không sửa được giờ cho nhân viên. Vui lòng thử lại.", "Lỗi");
				}
			}
			try {
				XL.XemCong(flstDSNVChk, fNgayBD, fNgayKT);
				loadTable();
			} catch (Exception) {
				MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại sau.", "Lỗi");
				return;
			}
			GC.Collect();

		}

		private void btnXoaGio_Click(object sender, EventArgs e) {
			DataTable table = dgrdTongHop.DataSource as DataTable;
			if (table == null) return;
			DataRow[] arrRows = table.Select("check = true", "UserEnrollNumber asc");
			//1 nếu chưa chọn ai thì thông báo
			if (arrRows.Length == 0) {
				AutoClosingMessageBox.Show("Bạn chưa chọn Nhân viên.", "Thông báo", 1000);
				return;
			}

			if (MessageBox.Show("Xoá giờ chấm công của các nhân viên?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
				return;
			bool flagThongBao = false, flagLoiThaoTacDL = false;
			string Lydo = string.Empty;
			if (string.IsNullOrEmpty(cbLyDo_Xoa.Text) == false) {
				Lydo = cbLyDo_Xoa.Text;
			}
			else if (cbLyDo_Xoa.SelectedItem != null) {
				Lydo = (string)cbLyDo_Xoa.SelectedItem;
			}

			foreach (DataRow row in arrRows) {
				cUserInfo tmpNV = flstDSNVChk.Find(o => o.UserEnrollNumber == (int)row["UserEnrollNumber"]);
				DateTime t_Vao = (row["TimeStrVao"] != DBNull.Value) ? (DateTime)row["TimeStrVao"] : DateTime.MinValue;
				DateTime t_Raa = (row["TimeStrRa"] != DBNull.Value) ? (DateTime)row["TimeStrRa"] : DateTime.MinValue;

				if (rdXoaVao.Checked) {
					if (t_Vao == DateTime.MinValue) continue;
					cChk tmpCheck = tmpNV.ds_CheckAuto.Find(o => o.TimeStr == t_Vao && o.GetType() == typeof(cChkIn));
					if (tmpCheck == null) flagThongBao = true;// giờ chưa xl thì xóa, giờ xác nhận thì bỏ qua
					else {
						if (XL.XoaGioChoNV(tmpNV, tmpCheck, Lydo, tbGhiChu_Xoa.Text) == false) {
							flagLoiThaoTacDL = true;
							break;
						}
					}
				}
				else if (rdXoaRa.Checked) {
					if (t_Raa == DateTime.MinValue) continue;
					cChk tmpCheck = tmpNV.ds_CheckAuto.Find(o => o.TimeStr == t_Raa && o.GetType() == typeof(cChkOut));
					if (tmpCheck == null) flagThongBao = true;
					else {
						if (XL.XoaGioChoNV(tmpNV, tmpCheck, Lydo, tbGhiChu_Xoa.Text) == false) {
							flagLoiThaoTacDL = true;
							break;
						}
					}

				}
				else {
					if (t_Vao != DateTime.MinValue && t_Raa != DateTime.MinValue) {
						cChk tmpCheckIn = tmpNV.ds_CheckAuto.Find(o => o.TimeStr == t_Vao && o.GetType() == typeof(cChkIn));
						cChk tmpCheckOut = tmpNV.ds_CheckAuto.Find(o => o.TimeStr == t_Raa && o.GetType() == typeof(cChkOut));
						if (tmpCheckIn != null && tmpCheckOut != null) {
							if (XL.XoaGioChoNV(tmpNV, tmpCheckIn, Lydo, tbGhiChu_Xoa.Text) == false) {
								flagLoiThaoTacDL = true; break;
							}
							if (XL.XoaGioChoNV(tmpNV, tmpCheckOut, Lydo, tbGhiChu_Xoa.Text) == false) {
								flagLoiThaoTacDL = true; break;
							}
						}
						else flagThongBao = true;
					}
				}
			}
			if (flagThongBao) MessageBox.Show("Không thể sửa giờ đã được xác nhận.", "Thông báo");
			if (flagLoiThaoTacDL) MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK);
			try {
				XL.XemCong(flstDSNVChk, fNgayBD, fNgayKT);
				loadTable();
			} catch (Exception) {
				MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK);
			}
			GC.Collect();
		}



	}
}
