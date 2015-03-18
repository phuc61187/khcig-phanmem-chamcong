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
	public partial class frm_23_ChamCongTay : Form {
		public readonly ILog lg = LogManager.GetLogger("frm_23_ChamCongTay");
		
		public List<int> m_listIDPhongBan { get; set; }
		public List<cShift> m_DSCa;
		readonly CheckBox checkAll_GridDSNV = new CheckBox();

		public frm_23_ChamCongTay() {
			InitializeComponent();

			dgrdDSNVTrgPhg.AutoGenerateColumns = false;
			DateTime today = DateTime.Today;
			dtpNgay.Value = new DateTime(today.Year,today.Month,today.Day);
			dtpBDLam.Value = new DateTime(today.Year,today.Month,today.Day);
			dtpKTLam.Value = new DateTime(today.Year,today.Month,today.Day);

		}

        #region load treeview new
        public TreeView loadTreePhgBan(TreeView tvDSPhongBan, DataTable pDataTable)
        {
            if (pDataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in pDataTable.Select("RelationID = 0"))
                {
                    TreeNode ParentNode = new TreeNode { Text = dataRow["Description"].ToString(), Tag = (int)dataRow["ID"] };
                    tvDSPhongBan.Nodes.Add(ParentNode);
                    loadTreeSubNode(ref ParentNode, (int)(dataRow["ID"]), pDataTable);
                }
            }
            return tvDSPhongBan;
        }

        private void loadTreeSubNode(ref TreeNode ParentNode, int ParentId, DataTable dtMenu)
        {
            DataRow[] childs = dtMenu.Select("RelationID = " + ParentId);
            foreach (DataRow dRow in childs)
            {
                TreeNode child = new TreeNode { Text = dRow["Description"].ToString(), Tag = (int)dRow["ID"], ToolTipText = dRow["Description"].ToString() };
                ParentNode.Nodes.Add(child);
                //Recursion Call
                loadTreeSubNode(ref child, (int)dRow["ID"], dtMenu);
            }
        }

        void GetIDLeafNodeExceptParent(TreeNode root, List<int> listID)
        {
            if (root == null) return;
            if (root.FirstNode == null)
            {
                listID.Add((int)root.Tag);
                //lg.Debug(root.Tag + " " + root.Text);
                GetIDLeafNodeExceptParent(root.NextNode, listID);
            }
            if (root.FirstNode != null)
            {
                foreach (TreeNode node in root.Nodes)
                {
                    GetIDLeafNodeExceptParent(node, listID);
                }
            }
        }

        private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (m_listIDPhongBan == null) m_listIDPhongBan = new List<int>();
            else m_listIDPhongBan.Clear();
            if (e.Node.FirstNode != null) GetIDLeafNodeExceptParent(e.Node, m_listIDPhongBan);
            else m_listIDPhongBan.Add((int)e.Node.Tag);
            e.Node.Expand();

            DataTable table = DAL.LayDSNV(m_listIDPhongBan.ToArray());
            table.Columns.Add("check", typeof(bool));
            table.Columns["check"].DefaultValue = false;

            dgrdDSNVTrgPhg.DataSource = table;
            checkAll_GridDSNV.Checked = false;
        }

        #endregion
		#region vẽ check box check all và xử lý sự kiện
		void checkAll_CheckedChanged(object sender, EventArgs e) {
			//1. xác định datagrid nào đang check all
			DataGridView tempGrid = dgrdDSNVTrgPhg;

			// 2. kiểm tra nếu ko có dòng dữ liệu nào thì thoát
			if (tempGrid.Rows.Count == 0) return;

			//3. có dữ liệu, duyệt qua hết và gán giá trị check (or uncheck), sau đó endEDIT lại lưới để dòng đầu tiên có giá trị
			bool tmpCheckAll = ((CheckBox)sender).Checked;

			tempGrid.BeginEdit(true);
			DataTable dt = tempGrid.DataSource as DataTable;

			if (dt != null && dt.Rows.Count != 0) {
				foreach (DataRow row in dt.Rows) {
					row["check"] = tmpCheckAll;
				}
			}

			if (tempGrid.DataSource != null) {
				BindingContext[tempGrid.DataSource].EndCurrentEdit();
			}
		}
		#endregion

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void btnThucHien_Click(object sender, EventArgs e) {

			// kiểm tra chọn ca làm việc chưa? chưa thì báo
			cShift ca = cbCa.SelectedItem as cShift;
			if (ca == null || cbCa.SelectedIndex == 0) {
				AutoClosingMessageBox.Show("Bạn chưa chọn ca làm việc.", "Thông báo", 1500);
				return;
			}

			//đã chọn ca làm việc
			DateTime ngay = dtpNgay.Value.Date;
			DateTime TimeStrInn = dtpBDLam.Value;
			DateTime TimeStrOut = dtpKTLam.Value;
			TimeSpan giolam = TimeStrOut - TimeStrInn;
			// kiểm tra giờ làm < 0 thì báo lỗi
			if (giolam < ThamSo._0gio) {
				AutoClosingMessageBox.Show("Giờ ra phải lớn hơn giờ vào.", "Lỗi", 1500);
				return;
			}

			// kiểm tra chọn NV chưa, chưa thì báo
			DataTable tableCheckNV = dgrdDSNVTrgPhg.DataSource as DataTable;
			if (tableCheckNV == null) return;
			DataRow[] arrRows = tableCheckNV.Select("check = true", "UserEnrollNumber asc");
			if (arrRows.Length == 0) {
				AutoClosingMessageBox.Show("Bạn chưa chọn nhân viên.", "Thông báo", 1500);
				return;
			}

			// đã chọn NV, lấy ds mã CC đã chọn
			string lydo = string.Empty;
			if (string.IsNullOrWhiteSpace(cbLyDo.Text)) {
				lydo = cbLyDo.Text;
			}
			string ghichu = tbGhiChu.Text;

			bool flag = true;

			foreach (DataRow row in arrRows) {
				//dsNVcheck.Add((int)row["UserEnrollNumber"]);
				int iUserEnrollNumber = (int)row["UserEnrollNumber"];
				if (ca.ShiftID == int.MinValue)
					XL.TaoCaTuDo(ca, TimeStrInn, ThamSo._8gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1f);
				else if (ca.ShiftID == int.MinValue + 1)
					XL.TaoCaTuDo(ca, TimeStrInn, ThamSo._12gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1.5f);

				string onnduty = ca.OnnDutyTS.ToString(@"hh\:mm");
				string offduty = ca.OffDutyTS.ToString(@"hh\:mm");
				int lategrace = Convert.ToInt32(ca.LateGraceTS.TotalMinutes);
				int earlygrace = Convert.ToInt32(ca.EarlyGraceTS.TotalMinutes);
				int afterot = Convert.ToInt32(ca.AfterOTTS.TotalMinutes);
				int daycount = ca.DayCount;
				Single wktime = Convert.ToSingle(ca.WorkingTimeTS.TotalMinutes);
				Single wkdayy = Convert.ToSingle(ca.Workingday);
				int sophutOT = (int)numSoPhutOT.Value;
				bool tinhPC150 = checkTinhPC150.Checked;
				int n;

				if (ca.OnnDutyTS == ThamSo._21h45 && ca.Workingday == 2f) { // ca 3 và 1 => tách
					cShift ca3 = ThamSo.DSCa.Find(o => o.OnnDutyTS == ThamSo._21h45 && o.Workingday == 1f);
					cShift ca1 = ThamSo.DSCa.Find(o => o.OnnDutyTS == ThamSo._05h45 && o.Workingday == 1f);
					n = DAL.ThemChamCongTay(iUserEnrollNumber, TimeStrInn, ngay.Add(ca3.OffDutyTS), ca3.ShiftID, ca3.ShiftCode
						, ca3.OnnDutyTS.ToString(@"hh\:mm"), ca3.OffDutyTS.ToString(@"hh\:mm")
						, Convert.ToInt32(ca3.LateGraceTS.TotalMinutes), Convert.ToInt32(ca3.EarlyGraceTS.TotalMinutes), Convert.ToInt32(ca3.AfterOTTS.TotalMinutes)
						, ca3.DayCount, Convert.ToSingle(ca3.WorkingTimeTS.TotalMinutes), ca3.Workingday, 0, tinhPC150, lydo,ghichu);
					// add thêm 1 ngày ở thời gian bd vì ca 1 của ngày hôm sau
					n = DAL.ThemChamCongTay(iUserEnrollNumber, ngay.AddDays(1d).Add(ca1.OnnDutyTS).Add(new TimeSpan(0, 0, 1)), TimeStrOut, ca1.ShiftID, ca1.ShiftCode
						, ca1.OnnDutyTS.ToString(@"hh\:mm"), ca1.OffDutyTS.ToString(@"hh\:mm")
						, Convert.ToInt32(ca1.LateGraceTS.TotalMinutes), Convert.ToInt32(ca1.EarlyGraceTS.TotalMinutes), Convert.ToInt32(ca1.AfterOTTS.TotalMinutes)
						, ca1.DayCount, Convert.ToSingle(ca1.WorkingTimeTS.TotalMinutes), ca1.Workingday, sophutOT, tinhPC150, lydo, ghichu);
				}
				else {
					// BUG chú ý thêm 1 giây để tránh tình trạng giờ vào trùng giờ ra
					n = DAL.ThemChamCongTay(iUserEnrollNumber, TimeStrInn.Add(new TimeSpan(0, 0, 1)), TimeStrOut, ca.ShiftID, ca.ShiftCode
						, onnduty, offduty, lategrace, earlygrace, afterot, daycount, wktime, wkdayy, sophutOT, tinhPC150, lydo, ghichu);
				}
				if (n == 0) {
					MessageBox.Show("Có lỗi trong quá trình thực hiện.");
					flag = false;
					break;
				}
			}
			if (flag) {
				AutoClosingMessageBox.Show("Thêm giờ chấm công tay cho các nhân viên thành công.", "Thông báo", 2000);
			}
		}


		private void frm_ChamCongTay_Load(object sender, EventArgs e) {
			ThamSo.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));
			m_listIDPhongBan = new List<int>();

			dtpBDLam.ValueChanged += dtp_ValueChanged;
			dtpKTLam.ValueChanged += dtp_ValueChanged;
			dtpNgay.ValueChanged += dtpNgay_ValueChanged;
			m_DSCa = new List<cShift>(ThamSo.DSCa);
			cShift tmp = new cShift() { ShiftID = 0, ShiftCode = "--" };
			cShift ca8tieng = new cShift() { ShiftID = int.MinValue, ShiftCode = "Ca 8 tiếng" };
			cShift cadai = new cShift() { ShiftID = int.MinValue + 1, ShiftCode = "Ca dài 12 tiếng" };
			m_DSCa.Insert(0, cadai);
			m_DSCa.Insert(0, ca8tieng);
			m_DSCa.Insert(0, tmp);

			cbCa.ValueMember = "ShiftID";
			cbCa.DisplayMember = "ShiftCode";
			cbCa.DataSource = m_DSCa;
			cbCa.SelectionChangeCommitted += cbCa_SelectionChangeCommitted;
			tbGioLam.TextChanged += tbGioLam_TextChanged;


			DataTable tablePhong = DAL.LayDSPhong(ThamSo.currUserID);
			if (tablePhong.Rows.Count == 0) {
				AutoClosingMessageBox.Show("Bạn chưa được phân quyền thao tác.", "Thông báo", 2000);
				return;
			}
			//2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan : xoá dữ liệu trước và load
			treePhongBan.Nodes.Clear();
			loadTreePhgBan(treePhongBan, tablePhong);

			// đăng ký sự kiện cho tree và chọn topNode
			treePhongBan.AfterSelect += treePhongBan_AfterSelect;
			treePhongBan.SelectedNode = treePhongBan.TopNode;

		}

		void tbGioLam_TextChanged(object sender, EventArgs e) {
			if (string.IsNullOrEmpty(tbGioLam.Text) || tbGioLam.Text == "0") {
				tbCong.Text = "0";
				return;
			}
			//int cong = int.Parse(tbGioLam.Text);
			cShift ca = cbCa.SelectedItem as cShift;
			if (ca == null) {
				AutoClosingMessageBox.Show("ca null", "", 1000);
				return;
			}
			TimeSpan sophutlamthem = new TimeSpan(0, (int)numSoPhutOT.Value, 0);
			Double giolam = (string.IsNullOrEmpty(tbGioLam.Text)) ? 0d : Double.Parse(tbGioLam.Text);
			giolam = giolam + sophutlamthem.TotalHours;
			Double cong = 0d;

			if (ca.ShiftID == 0 || ca.ShiftID == int.MinValue || ca.ShiftID == int.MinValue + 1) cong = giolam / 8d;
			else {
				cong = (giolam / ca.WorkingTimeTS.TotalHours) * ca.Workingday;
			}
			cong = Math.Round(cong, 2);
			tbCong.Text = cong.ToString();// ("#0.##");
		}

		void cbCa_SelectionChangeCommitted(object sender, EventArgs e) {
			cShift ca = (cbCa.SelectedItem) as cShift;
			if (ca == null) {
				AutoClosingMessageBox.Show("ca null", "", 1000);
				return;
			}
			DateTime ngay = dtpNgay.Value.Date;
			if (ca.ShiftID == 0 || ca.ShiftID == int.MinValue || ca.ShiftID == int.MinValue + 1) {
				dtpBDLam.Value = new DateTime(ngay.Year, ngay.Month, ngay.Day, 0, 0, 0);
				dtpKTLam.Value = new DateTime(ngay.Year, ngay.Month, ngay.Day, 0, 0, 0);
				dtpBDLam.Update();
				dtpKTLam.Update();
				return;
			}

			// chọn ca 1, 2 ,....
			DateTime timeBD = ngay.Add(ca.OnnDutyTS);
			DateTime timeKT = ngay.Add(ca.OffDutyTS);
			dtpBDLam.Value = timeBD;
			dtpKTLam.Value = timeKT;

		}

		void dtpNgay_ValueChanged(object sender, EventArgs e) {
			DateTime timeBD = dtpBDLam.Value;
			DateTime timeKT = dtpKTLam.Value;
			DateTime ngay = dtpNgay.Value;
			dtpBDLam.Value = new DateTime(ngay.Year, ngay.Month, ngay.Day, timeBD.Hour, timeBD.Minute, timeBD.Second);
			dtpKTLam.Value = new DateTime(ngay.Year, ngay.Month, ngay.Day, timeKT.Hour, timeKT.Minute, timeKT.Second);
			dtpBDLam.Update();
			dtpKTLam.Update();
		}

		void dtp_ValueChanged(object sender, EventArgs e) {
			DateTime timeBD = dtpBDLam.Value;
			DateTime timeKT = dtpKTLam.Value;
			TimeSpan giolam = timeKT - timeBD;
			if (giolam < ThamSo._0gio || giolam > ThamSo._24h00) {
				giolam = ThamSo._0gio;
				tbGioLam.Text = giolam.TotalHours.ToString("#0.##");
				return;
			}

			cShift ca = cbCa.SelectedItem as cShift;
			if (ca == null) {
				AutoClosingMessageBox.Show("ca null", "", 1000);
				return;
			}

			if (ca.ShiftID == 0)
				tbGioLam.Text = giolam.TotalHours.ToString("#0.##");
			else if (ca.ShiftID == int.MinValue) {
				if (giolam > ThamSo._8gio) giolam = ThamSo._8gio;
			}
			else if (ca.ShiftID == int.MinValue + 1) {
				if (giolam > ThamSo._12gio) giolam = ThamSo._12gio;
			}
			else { // ca 1,2,3, hc..
				if (giolam > ca.WorkingTimeTS) giolam = ca.WorkingTimeTS;
			}

			tbGioLam.Text = giolam.TotalHours.ToString("#0.##");

		}
	}
}
