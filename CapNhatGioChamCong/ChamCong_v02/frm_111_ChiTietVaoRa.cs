using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ChamCong_v02.BUS;
using ChamCong_v02.DAO;
using ChamCong_v02.DTO;
using log4net;

namespace ChamCong_v02 {

	public partial class frm_111_ChiTietVaoRa : Form {
		private readonly ILog lg = LogManager.GetLogger("frm_111_ChiTietVaoRa");
		public bool IsReload;
		public List<cUserInfo> flstDSNVChk;
		private readonly CheckBox chkBoxAllDSNV = new CheckBox();
		public List<cNgayCong> dsngaycong;
		public DateTime fNgayBD;
		public DateTime fNgayKT;

		public DataRow[] DSNgayCongChked;


		public frm_111_ChiTietVaoRa() {
			InitializeComponent();
			dgrdTongHop.AutoGenerateColumns = false;
			tabControl1.SelectedIndex = 0;
		}

		private void frm_ChiTietVaoRa_Load(object sender, EventArgs e) {
			IsReload = false; // mặc định ko có gì thay đổi, nếu có sử dụng chức năng thêm, xoá sửa, đảo thì this.DialogResult = Yes;
			loadTable();

		}

		private void loadTable() {
			DataTable table = (dgrdTongHop.DataSource) as DataTable;
			if (table == null || table.Rows.Count != 0)
				table = TaoCauTrucTableCTGioVaoRa();
			else table.Rows.Clear();
			//[TBD] nhớ check null --> có check xem mới được chuyển qua --> khỏi check null
			for (int i = 0; i < DSNgayCongChked.Length; i++) {
				DataRow row = DSNgayCongChked[i];
				int UserEnrollNumber = (int)row["UserEnrollNumber"];
				string UserFullCode = row["UserFullCode"].ToString();
				string UserFullName = row["UserFullName"].ToString();
				DateTime ngay = (DateTime)row["TimeStrNgay"];
				cUserInfo nv = flstDSNVChk.Find(o => o.UserEnrollNumber == UserEnrollNumber);

				cNgayCong tmpNgayCong = nv.DSNgayCong.Find(o => o.NgayCong == ngay);
				if (tmpNgayCong == null) continue;

				#region neu ngày đó vắng => các giá trị null
				if (tmpNgayCong.HasCheck == false) {
					DataRow subrow = table.NewRow();
					subrow["check"] = false;
					subrow["UserEnrollNumber"] = UserEnrollNumber;
					subrow["UserFullCode"] = UserFullCode;
					subrow["UserFullName"] = UserFullName;
					subrow["TimeStrNgay"] = subrow["TimeStrThu"] = tmpNgayCong.NgayCong;
					subrow["TimeStrVao"] = subrow["TimeStrRa"] = subrow["ShiftCode"] = subrow["ShiftID"] = DBNull.Value;
					subrow["obj"] = DBNull.Value;
					table.Rows.Add(subrow);
				}
				#endregion
				#region nếu có check thì gán giá trị
				foreach (cChkInOut chkInOut in tmpNgayCong.DSVaoRa) {
					DataRow subrow = table.NewRow();
					subrow["check"] = false;
					subrow["UserEnrollNumber"] = UserEnrollNumber;
					subrow["UserFullCode"] = UserFullCode;
					subrow["UserFullName"] = UserFullName;
					subrow["TimeStrNgay"] = subrow["TimeStrThu"] = tmpNgayCong.NgayCong;
					subrow["TimeStrVao"] = (chkInOut.Vao != null) ? chkInOut.Vao.TimeStr : (object)DBNull.Value;
					subrow["TimeStrRa"] = (chkInOut.Raa != null) ? chkInOut.Raa.TimeStr : (object)DBNull.Value;
					subrow["ShiftCode"] = (chkInOut.ThuocCa != null) ? chkInOut.ThuocCa.ShiftCode : (object)DBNull.Value;
					subrow["ShiftID"] = (chkInOut.ThuocCa != null) ? chkInOut.ThuocCa.ShiftID : (object)DBNull.Value;
					subrow["obj"] = chkInOut;
					table.Rows.Add(subrow);
				}
				#endregion

			}
			dgrdTongHop.DataSource = table;
			dgrdTongHop.Select();
		}

		private DataTable TaoCauTrucTableCTGioVaoRa() {
			DataTable kq = new DataTable();
			kq.Columns.Add("check", typeof(bool)); //0
			kq.Columns.Add("UserEnrollNumber", typeof(int)); //0
			kq.Columns.Add("UserFullCode", typeof(string)); //0
			kq.Columns.Add("UserFullName", typeof(string)); //1
			kq.Columns.Add("TimeStrNgay", typeof(DateTime)); //2
			kq.Columns.Add("TimeStrThu", typeof(DateTime)); //3
			kq.Columns.Add("TimeStrVao", typeof(DateTime)); //4
			kq.Columns.Add("TimeStrRa", typeof(DateTime)); //5
			kq.Columns.Add("ShiftCode", typeof(string)); //8
			kq.Columns.Add("ShiftID", typeof(int)); //9
			kq.Columns.Add("obj", typeof(cChkInOut));
			return kq;
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
				checkVao_Sua.Enabled = true; // có giờ, cho phép input
				dtpVao_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
				cbCaVao_Sua.SelectedIndexChanged -= cbCa_Sua_OnSelectedIndexChanged;

				dtpVao_Sua.Value = tmpVao;
				checkVao_Sua.Checked = false;

				dtpVao_Sua.ValueChanged += dtp_Sua_OnValueChanged;
				cbCaVao_Sua.SelectedIndexChanged += cbCa_Sua_OnSelectedIndexChanged;
			}
			else { // ko cho phép input
				dtpVao_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
				cbCaVao_Sua.SelectedIndexChanged -= cbCa_Sua_OnSelectedIndexChanged;

				checkVao_Sua.Checked = false;
				checkVao_Sua.Enabled = false;// ko có giờ vào nên ko thể input
				dtpVao_Sua.Value = tmpNgayCong;

				dtpVao_Sua.ValueChanged += dtp_Sua_OnValueChanged;
				cbCaVao_Sua.SelectedIndexChanged += cbCa_Sua_OnSelectedIndexChanged;
			}

			if (tmpRa != DateTime.MinValue) { // có giờ ra, cho phép input giờ ra
				checkRa_Sua.Enabled = true; // có giờ , cho phép input
				dtpRa_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
				cbCaRa_Sua.SelectedIndexChanged -= cbCa_Sua_OnSelectedIndexChanged;

				dtpRa_Sua.Value = tmpRa;
				checkRa_Sua.Checked = false;

				dtpRa_Sua.ValueChanged += dtp_Sua_OnValueChanged;
				cbCaRa_Sua.SelectedIndexChanged += cbCa_Sua_OnSelectedIndexChanged;
			}
			else {
				dtpRa_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
				cbCaRa_Sua.SelectedIndexChanged -= cbCa_Sua_OnSelectedIndexChanged;

				checkRa_Sua.Checked = false;
				checkRa_Sua.Enabled = false;// ko co gio ra, ko cho phep sua
				dtpRa_Sua.Value = tmpNgayCong;

				dtpRa_Sua.ValueChanged += dtp_Sua_OnValueChanged;
				cbCaRa_Sua.SelectedIndexChanged += cbCa_Sua_OnSelectedIndexChanged;
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
				dtpVao_Them.ValueChanged -= dtp_Them_OnValueChanged;
				cbCaVao_Them.SelectedIndexChanged -= cbCa_Them_OnSelectedIndexChanged;

				dtpVao_Them.Value = tmpNgayCong;
				checkVao_Them.Checked = true;

				dtpVao_Them.ValueChanged += dtp_Them_OnValueChanged;
				cbCaVao_Them.SelectedIndexChanged += cbCa_Them_OnSelectedIndexChanged;
			}
			else { // ko cho phép input
				dtpVao_Them.ValueChanged -= dtp_Them_OnValueChanged;
				cbCaVao_Them.SelectedIndexChanged -= cbCa_Them_OnSelectedIndexChanged;

				checkVao_Them.Checked = false;
				dtpVao_Them.Value = tmpVao;

				dtpVao_Them.ValueChanged += dtp_Them_OnValueChanged;
				cbCaVao_Them.SelectedIndexChanged += cbCa_Them_OnSelectedIndexChanged;

			}

			if (tmpRa == DateTime.MinValue) { // ko có giờ ra, cho phép input giờ ra
				dtpRa_Them.ValueChanged -= dtp_Them_OnValueChanged;
				cbCaRa_Them.SelectedIndexChanged -= cbCa_Them_OnSelectedIndexChanged;

				dtpRa_Them.Value = tmpNgayCong;
				checkRa_Them.Checked = true;

				dtpRa_Them.ValueChanged += dtp_Them_OnValueChanged;
				cbCaRa_Them.SelectedIndexChanged += cbCa_Them_OnSelectedIndexChanged;
			}
			else {
				dtpRa_Them.ValueChanged -= dtp_Them_OnValueChanged;
				cbCaRa_Them.SelectedIndexChanged -= cbCa_Them_OnSelectedIndexChanged;

				checkRa_Them.Checked = false;
				dtpRa_Them.Value = tmpRa;

				dtpRa_Them.ValueChanged += dtp_Them_OnValueChanged;
				cbCaRa_Them.SelectedIndexChanged += cbCa_Them_OnSelectedIndexChanged;
			}
		}

		private void dgrdTongHop_TabIndexChanged(object sender, EventArgs e) {
			dgrdTongHop.Columns[0].Visible = false;
			chkBoxAllDSNV.Hide();
			dgrdTongHop.Update();
		}

		#region them
		private void checkBox_Them_CheckedChanged(object sender, EventArgs e) {
			if (dgrdTongHop.SelectedRows.Count == 0) return;

			DataRowView row = dgrdTongHop.SelectedRows[0].DataBoundItem as DataRowView;

			if (sender == checkVao_Them) {
				if (checkVao_Them.Checked) {
					lg.Debug("checkVao_Them.Checked start");
					cUserInfo tmpNV = flstDSNVChk.Find(o => o.UserEnrollNumber == (int)row["UserEnrollNumber"]);
					dtpVao_Them.ValueChanged -= dtp_Them_OnValueChanged;
					cbCaVao_Them.SelectedIndexChanged -= cbCa_Them_OnSelectedIndexChanged;

					dtpVao_Them.Value = ((DateTime)row["TimeStrNgay"]).Date;

					List<cShift> tmpDSCaCopy = new List<cShift>(tmpNV.DSCa);
					tmpDSCaCopy.Insert(0, new cShift() { ShiftCode = "Tuỳ chỉnh", ShiftID = int.MinValue, LoaiCa = 1 });
					cbCaVao_Them.DisplayMember = "ShiftCode";
					cbCaVao_Them.ValueMember = "ShiftID";
					cbCaVao_Them.DataSource = tmpDSCaCopy;
					cbCaVao_Them.SelectedIndex = 0;
					cbCaVao_Them.Update();

					cbCaVao_Them.SelectedIndexChanged += cbCa_Them_OnSelectedIndexChanged;
					dtpVao_Them.ValueChanged += dtp_Them_OnValueChanged;

					dtpVao_Them.Enabled = true;
					cbCaVao_Them.Enabled = true;
					lg.Debug("checkVao_Them.Checked end");
				}
				else {
					lg.Debug("checkVao_Them.UnChecked start");
					dtpVao_Them.ValueChanged -= dtp_Them_OnValueChanged;
					cbCaVao_Them.SelectedIndexChanged -= cbCa_Them_OnSelectedIndexChanged;

					cbCaVao_Them.DataSource = null;
					dtpVao_Them.Value = ((DateTime)row["TimeStrNgay"]).Date;

					dtpVao_Them.Enabled = false;
					cbCaVao_Them.Enabled = false;
					lg.Debug("checkVao_Them.UnChecked end");
				}
			}
			else if (sender == checkRa_Them) {
				if (checkRa_Them.Checked) {
					cUserInfo tmpNV = flstDSNVChk.Find(o => o.UserEnrollNumber == (int)row["UserEnrollNumber"]);
					dtpRa_Them.ValueChanged -= dtp_Them_OnValueChanged;
					cbCaRa_Them.SelectedIndexChanged -= cbCa_Them_OnSelectedIndexChanged;

					dtpRa_Them.Value = ((DateTime)row["TimeStrNgay"]).Date;

					List<cShift> tmpDSCaCopy = new List<cShift>(tmpNV.DSCa);
					tmpDSCaCopy.Insert(0, new cShift() { ShiftCode = "Tuỳ chỉnh", ShiftID = int.MinValue, LoaiCa = 1 });
					cbCaRa_Them.DisplayMember = "ShiftCode";
					cbCaRa_Them.ValueMember = "ShiftID";
					cbCaRa_Them.DataSource = tmpDSCaCopy;
					cbCaRa_Them.SelectedIndex = 0;
					cbCaRa_Them.Update();

					dtpRa_Them.ValueChanged += dtp_Them_OnValueChanged;
					cbCaRa_Them.SelectedIndexChanged += cbCa_Them_OnSelectedIndexChanged;

					dtpRa_Them.Enabled = true;
					cbCaRa_Them.Enabled = true;
				}
				else {
					dtpRa_Them.ValueChanged -= dtp_Them_OnValueChanged;
					cbCaRa_Them.SelectedIndexChanged -= cbCa_Them_OnSelectedIndexChanged;

					cbCaRa_Them.DataSource = null;
					dtpRa_Them.Value = ((DateTime)row["TimeStrNgay"]).Date;

					dtpRa_Them.Enabled = false;
					cbCaRa_Them.Enabled = false;
				}
			}
		}

		private void cbCa_Them_OnSelectedIndexChanged(object sender, EventArgs eventArgs) {
			if (dgrdTongHop.SelectedRows.Count == 0) return;
			DataRowView row = dgrdTongHop.SelectedRows[0].DataBoundItem as DataRowView;

			if (sender == cbCaVao_Them) {
				if (cbCaVao_Them.SelectedIndex == 0) return;
				cShift tmpshift = (sender as ComboBox).SelectedItem as cShift;
				DateTime tmpVao = (DateTime)row["TimeStrNgay"];
				tmpVao = tmpVao.Date.Add(tmpshift.OnnDutyTS);
				dtpVao_Them.ValueChanged -= dtp_Them_OnValueChanged;
				dtpVao_Them.Value = tmpVao;
				dtpVao_Them.Update();
				dtpVao_Them.ValueChanged += dtp_Them_OnValueChanged;
			}
			else {
				cShift tmpshift = (sender as ComboBox).SelectedItem as cShift;
				DateTime tmpRa = (DateTime)row["TimeStrNgay"];
				tmpRa = tmpRa.Date.Add(tmpshift.OffDutyTS);
				dtpRa_Them.ValueChanged -= dtp_Them_OnValueChanged;
				dtpRa_Them.Value = tmpRa;
				dtpRa_Them.Update();
				dtpRa_Them.ValueChanged += dtp_Them_OnValueChanged;
			}

		}

		private void dtp_Them_OnValueChanged(object sender, EventArgs eventArgs) {
			if (dgrdTongHop.SelectedRows.Count == 0) return;

			DataRowView row = dgrdTongHop.SelectedRows[0].DataBoundItem as DataRowView;

			if (sender == dtpVao_Them) {
				if (cbCaVao_Them.SelectedIndex == 0) return;
				cShift tmpshift = cbCaVao_Them.SelectedItem as cShift;
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
				if (cbCaRa_Them.SelectedIndex == 0) return;
				cShift tmpshift = cbCaRa_Them.SelectedItem as cShift;
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


		#endregion

		#region sua
		private void checkBox_Sua_CheckedChanged(object sender, EventArgs e) {
			if (dgrdTongHop.SelectedRows.Count == 0) return;

			DataRowView row = dgrdTongHop.SelectedRows[0].DataBoundItem as DataRowView;
			if (sender == checkVao_Sua) {
				if (checkVao_Sua.Checked) {
					cUserInfo tmpNV = flstDSNVChk.Find(o => o.UserEnrollNumber == (int)row["UserEnrollNumber"]);
					dtpVao_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
					cbCaVao_Sua.SelectedIndexChanged -= cbCa_Sua_OnSelectedIndexChanged;

					dtpVao_Sua.Value = ((DateTime)row["TimeStrNgay"]);

					List<cShift> tmpDSCaCopy = new List<cShift>(tmpNV.DSCa);
					tmpDSCaCopy.Insert(0, new cShift() { ShiftCode = "Tuỳ chỉnh", ShiftID = int.MinValue, LoaiCa = 1 });
					cbCaVao_Sua.DisplayMember = "ShiftCode";
					cbCaVao_Sua.ValueMember = "ShiftID";
					cbCaVao_Sua.DataSource = tmpDSCaCopy;
					cbCaVao_Sua.SelectedIndex = 0;

					cbCaVao_Sua.SelectedIndexChanged += cbCa_Sua_OnSelectedIndexChanged;
					dtpVao_Sua.ValueChanged += dtp_Sua_OnValueChanged;
					dtpVao_Sua.Enabled = true;
					cbCaVao_Sua.Enabled = true;
				}
				else {
					dtpVao_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
					cbCaVao_Sua.SelectedIndexChanged -= cbCa_Sua_OnSelectedIndexChanged;

					cbCaVao_Sua.DataSource = null;
					dtpVao_Sua.Value = ((DateTime)row["TimeStrNgay"]);

					dtpVao_Sua.Enabled = false;
					cbCaVao_Sua.Enabled = false;
				}
			}
			else {
				if (checkRa_Sua.Checked) {
					cUserInfo tmpNV = flstDSNVChk.Find(o => o.UserEnrollNumber == (int)row["UserEnrollNumber"]);
					dtpRa_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
					cbCaRa_Sua.SelectedIndexChanged -= cbCa_Sua_OnSelectedIndexChanged;

					dtpRa_Sua.Value = ((DateTime)row["TimeStrNgay"]);

					List<cShift> tmpDSCaCopy = new List<cShift>(tmpNV.DSCa);
					tmpDSCaCopy.Insert(0, new cShift() { ShiftCode = "Tuỳ chỉnh", ShiftID = int.MinValue, LoaiCa = 1 });
					cbCaRa_Sua.DisplayMember = "ShiftCode";
					cbCaRa_Sua.ValueMember = "ShiftID";
					cbCaRa_Sua.DataSource = tmpDSCaCopy;
					cbCaRa_Sua.SelectedIndex = 0;
					cbCaRa_Sua.Update();

					dtpRa_Sua.ValueChanged += dtp_Sua_OnValueChanged;
					cbCaRa_Sua.SelectedIndexChanged += cbCa_Sua_OnSelectedIndexChanged;
					dtpRa_Sua.Enabled = true;
					cbCaRa_Sua.Enabled = true;
				}
				else {
					dtpRa_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
					cbCaRa_Sua.SelectedIndexChanged -= cbCa_Sua_OnSelectedIndexChanged;

					cbCaRa_Sua.DataSource = null;
					dtpRa_Sua.Value = ((DateTime)row["TimeStrNgay"]);

					dtpRa_Sua.Enabled = false;
					cbCaRa_Sua.Enabled = false;
				}
			}
		}

		private void cbCa_Sua_OnSelectedIndexChanged(object sender, EventArgs e) {
			if (dgrdTongHop.SelectedRows.Count == 0) return;

			DataRowView row = dgrdTongHop.SelectedRows[0].DataBoundItem as DataRowView;

			if (sender == cbCaVao_Sua) {
				cShift tmpshift = (sender as ComboBox).SelectedItem as cShift;
				DateTime tmpVao = (DateTime)row["TimeStrNgay"];
				tmpVao = tmpVao.Date.Add(tmpshift.OnnDutyTS);
				dtpVao_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
				dtpVao_Sua.Value = tmpVao;
				dtpVao_Sua.Update();
				dtpVao_Sua.ValueChanged += dtp_Sua_OnValueChanged;
			}
			else {
				cShift tmpshift = (sender as ComboBox).SelectedItem as cShift;
				DateTime tmpRa = (DateTime)row["TimeStrNgay"];
				tmpRa = tmpRa.Date.Add(tmpshift.OffDutyTS);
				dtpRa_Sua.ValueChanged -= dtp_Sua_OnValueChanged;
				dtpRa_Sua.Value = tmpRa;
				dtpRa_Sua.Update();
				dtpRa_Sua.ValueChanged += dtp_Sua_OnValueChanged;
			}

		}

		private void dtp_Sua_OnValueChanged(object sender, EventArgs e) {
			if (dgrdTongHop.SelectedRows.Count == 0) return;

			DataRowView row = dgrdTongHop.SelectedRows[0].DataBoundItem as DataRowView;

			if (sender == dtpVao_Sua) {
				if (cbCaVao_Them.SelectedIndex == 0) return;
				cShift tmpshift = cbCaVao_Sua.SelectedItem as cShift;
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
				if (cbCaRa_Them.SelectedIndex == 0) return;
				cShift tmpshift = cbCaRa_Sua.SelectedItem as cShift;
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

		//[tbd] kiểm tra xem còn sử dụng ko
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
					cbCaVao_Sua.DisplayMember = "ShiftCode";
					cbCaVao_Sua.ValueMember = "ShiftID";
					cbCaVao_Sua.DataSource = tmpDSCaCopy;
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
					cbCaRa_Sua.DisplayMember = "ShiftCode";
					cbCaRa_Sua.ValueMember = "ShiftID";
					cbCaRa_Sua.DataSource = tmpDSCaCopy;
					cbCaRa_Sua.SelectedIndex = 0;
					cbCaRa_Sua.SelectedIndexChanged += cbCa_Sua_OnSelectedIndexChanged;
				}
			}
		}

		#endregion


		private void btnThemGio_Click(object sender, EventArgs e) {
			IsReload = true;

			#region lấy thông tin
			DataRowView row = dgrdTongHop.SelectedRows[0].DataBoundItem as DataRowView;
			if (row == null) return;
			int iUserEnrollNumber = (int)row["UserEnrollNumber"];
			cUserInfo tmpNV = flstDSNVChk.Find(o => o.UserEnrollNumber == iUserEnrollNumber);
			string Lydo = string.Empty;
			if (cbLyDo_Them.SelectedItem == null) {
				Lydo = cbLyDo_Them.Text;
			}
			else if (cbLyDo_Them.SelectedItem != null) {
				Lydo = (string)cbLyDo_Them.SelectedItem;
			}

			if (checkVao_Them.Checked) {
				dtpVao_Them.Update();

				if (DAL.ThemGioChoNV(iUserEnrollNumber, dtpVao_Them.Value.Add(new TimeSpan(0, 0, 1)), true, 21, ThamSo.currUserID, Lydo, tbGhiChu_Them.Text) == false)
					MessageBox.Show("Không thêm được giờ vào cho nhân viên. Vui lòng thử lại.", "Lỗi");
			}
			if (checkRa_Them.Checked) {
				dtpRa_Them.Update();
				if (DAL.ThemGioChoNV(iUserEnrollNumber, dtpRa_Them.Value, false, 22, ThamSo.currUserID, Lydo, tbGhiChu_Them.Text) == false)
					MessageBox.Show("Không thêm được giờ ra cho nhân viên. Vui lòng thử lại.", "Lỗi");

			}
			try {
				XL.XemCong(tmpNV, fNgayBD, fNgayKT);
				loadTable();

				GC.Collect();

			} catch (Exception) {
				MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK);
			}
			#endregion

		}

		private void btnSuaGio_Click(object sender, EventArgs e) {
			IsReload = true;

			DataRowView row = dgrdTongHop.SelectedRows[0].DataBoundItem as DataRowView;
			if (row == null) return;
			int iUserEnrollNumber = (int)row["UserEnrollNumber"];
			cUserInfo tmpNV = flstDSNVChk.Find(o => o.UserEnrollNumber == iUserEnrollNumber);

			DateTime tmpOldVao = (row["TimeStrVao"] != DBNull.Value) ? (DateTime)row["TimeStrVao"] : DateTime.MinValue;
			DateTime tmpOldRaa = (row["TimeStrRa"] != DBNull.Value) ? (DateTime)row["TimeStrRa"] : DateTime.MinValue;
			DateTime tmpNgayCong = (DateTime)row["TimeStrNgay"];


			cNgayCong ngayCong = tmpNV.DSNgayCong.Find(o => o.NgayCong == tmpNgayCong);
			cChkInOut old_CIO = row["obj"] as cChkInOut;
			if (ngayCong.HasCheck == false || (tmpOldVao == DateTime.MinValue && tmpOldRaa == DateTime.MinValue)) old_CIO = null;
			else if (tmpOldVao == DateTime.MinValue) old_CIO = ngayCong.DSVaoRa.Find(o => o.HaveINOUT < 0 && o.Raa.TimeStr == tmpOldRaa);
			else if (tmpOldRaa == DateTime.MinValue) old_CIO = ngayCong.DSVaoRa.Find(o => o.HaveINOUT < 0 && o.Vao.TimeStr == tmpOldVao);
			else old_CIO = ngayCong.DSVaoRa.Find(o => o.HaveINOUT > 0 && o.Vao.TimeStr == tmpOldVao && o.Raa.TimeStr == tmpOldRaa);
			if (old_CIO != null && old_CIO.GetType() == typeof(cChkInOut_V)) {
				MessageBox.Show("Không thể sửa giờ đã được xác nhận.", "Thông báo");
				return;
			}

			string Lydo = string.Empty;
			if (cbLyDo_Sua.SelectedItem == null) {
				Lydo = cbLyDo_Sua.Text;
			}
			else if (cbLyDo_Sua.SelectedItem != null) {
				Lydo = (string)cbLyDo_Sua.SelectedItem;
			}

			if (checkVao_Sua.Checked) {
				dtpVao_Sua.Update();
				cChk tmpCheck = old_CIO.Vao;
				if (DAL.SuaGioChoNV(iUserEnrollNumber, tmpCheck.TimeStr, dtpVao_Sua.Value.Add(new TimeSpan(0, 0, 1)), true, tmpCheck.Source, tmpCheck.MachineNo, ThamSo.currUserID, Lydo, tbGhiChu_Sua.Text) == false) {
					MessageBox.Show("Không sửa được giờ cho nhân viên. Vui lòng thử lại.", "Lỗi");
				}

			}
			if (checkRa_Sua.Checked) {
				dtpRa_Sua.Update();
				cChk tmpCheck = old_CIO.Raa;
				if (DAL.SuaGioChoNV(iUserEnrollNumber, tmpCheck.TimeStr, dtpRa_Sua.Value, false, tmpCheck.Source, tmpCheck.MachineNo, ThamSo.currUserID, Lydo, tbGhiChu_Sua.Text) == false) {
					MessageBox.Show("Không sửa được giờ cho nhân viên. Vui lòng thử lại.", "Lỗi");
				}
			}
			try {
				XL.XemCong(tmpNV, fNgayBD, fNgayKT);
				loadTable();
			} catch (Exception) {
				MessageBox.Show("Mất kết nối đến máy chủ. Vui lòng thử lại sau.", "Lỗi");
				return;
			}
			GC.Collect();


		}


		private void dgrdTongHop_SelectionChanged(object sender, EventArgs e) {
			if (dgrdTongHop.SelectedRows.Count == 0) return;
			DataTable tmpTable = (dgrdTongHop.DataSource as DataTable);
			if (tmpTable == null || tmpTable.Rows.Count == 0) return;
			DataRowView dataRowView = dgrdTongHop.SelectedRows[0].DataBoundItem as DataRowView;
			//dgrdTongHop.Tag = dataRowView;
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
			//lg.Info(test);
			cChkInOut old_CIO = new cChkInOut_A(){ TG = new ThoiGian()};
			try {
				if (ngayCong.HasCheck == false || (tmpVao == DateTime.MinValue && tmpRa == DateTime.MinValue)) old_CIO = null;
				else if (tmpVao == DateTime.MinValue && tmpRa != DateTime.MinValue) old_CIO = ngayCong.DSVaoRa.Find(o => o.HaveINOUT == -2 && o.Raa.TimeStr == tmpRa);
				else if (tmpRa == DateTime.MinValue && tmpVao != DateTime.MinValue) old_CIO = ngayCong.DSVaoRa.Find(o => o.HaveINOUT == -1 && o.Vao.TimeStr == tmpVao);
				else old_CIO = ngayCong.DSVaoRa.Find(o => o.HaveINOUT > 0 && o.Vao.TimeStr == tmpVao && o.Raa.TimeStr == tmpRa);

				LoadTabThemGio(tmpNV, tmpNgayCong, old_CIO);
				LoadTabSuaGio(tmpNV, tmpNgayCong, old_CIO);

			} catch (Exception ex) {
				string temp = "\n--start \n NV: " + tmpNV + "\nCIO: ";
				temp += (old_CIO != null) ? old_CIO.ToString() : "null";
				temp += "\n--end";
				lg.Fatal(ex.StackTrace + temp);
				AutoClosingMessageBox.Show("Có lỗi trong quá trình thao tác.", "Lỗi", 1500);
			}
		}



	}
}
