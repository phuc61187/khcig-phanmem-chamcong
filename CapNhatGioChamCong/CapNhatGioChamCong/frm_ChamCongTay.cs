using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapNhatGioChamCong.BUS;
using CapNhatGioChamCong.DAO;
using CapNhatGioChamCong.DTO;

namespace CapNhatGioChamCong {
	public partial class frm_ChamCongTay : Form {
		public List<cShift> m_DSCa;
		readonly CheckBox checkAll_GridDSNV = new CheckBox();

		public frm_ChamCongTay() {
			InitializeComponent();

			dgrdDSNVTrgPhg.AutoGenerateColumns = false;
		}

		#region load treeview
		public List<int> flstIDPhongBan { get; set; }
		public DataTable fTableDSNV { get; set; }
		public DataTable fTablePhongBan { get; set; }

		public TreeView loadTreePhgBan(TreeView tvDSPhongBan, DataTable pDataTable) {
			if (pDataTable.Rows.Count > 0) {
				foreach (DataRow dataRow in pDataTable.Select("RelationID = 0")) {
					TreeNode ParentNode = new TreeNode { Text = dataRow["Description"].ToString(), Tag = (int)dataRow["ID"] };
					tvDSPhongBan.Nodes.Add(ParentNode);
					loadTreeSubNode(ref ParentNode, (int)(dataRow["ID"]), pDataTable);
				}
			}
			return tvDSPhongBan;
		}

		private void loadTreeSubNode(ref TreeNode ParentNode, int ParentId, DataTable dtMenu) {
			DataRow[] childs = dtMenu.Select("RelationID = " + ParentId);
			foreach (DataRow dRow in childs) {
				TreeNode child = new TreeNode { Text = dRow["Description"].ToString(), Tag = (int)dRow["ID"], ToolTipText = dRow["Description"].ToString() };
				ParentNode.Nodes.Add(child);
				//Recursion Call
				loadTreeSubNode(ref child, (int)dRow["ID"], dtMenu);
			}
		}

		void GetNodeID(TreeNode root) {
			if (root == null) return;
			if (root.FirstNode == null) {
				flstIDPhongBan.Add((int)root.Tag);
				root = root.NextNode;
				return;
			}
			if (root.FirstNode != null) {
				foreach (TreeNode treeNode in root.Nodes)
					GetNodeID(treeNode);
			}
		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			flstIDPhongBan.Clear();
			if (e.Node.FirstNode != null) GetNodeID(e.Node);
			else flstIDPhongBan.Add((int)e.Node.Tag);
			e.Node.Expand();

			string temp = "UserIDD = {0}";
			temp = String.Format(temp, String.Join(" or UserIDD = ", flstIDPhongBan.ToArray()));

			DataTable table;
			if (dgrdDSNVTrgPhg.DataSource == null)
				table = fTableDSNV.Clone();
			else {
				table = dgrdDSNVTrgPhg.DataSource as DataTable;
				table.Rows.Clear();
			}

			foreach (DataRow row in fTableDSNV.Select(temp, "UserIDD asc, UserEnrollNumber asc", DataViewRowState.CurrentRows))
				table.ImportRow(row);

			dgrdDSNVTrgPhg.DataSource = table;
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

			tempGrid.EndEdit();
			tempGrid.Update();
			//tempGrid.Refresh();
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
			DateTime timeBD = dtpBDLam.Value;
			DateTime timeKT = dtpKTLam.Value;
			TimeSpan giolam = timeKT - timeBD;
			// kiểm tra giờ làm < 0 thì báo lỗi
			if (giolam < ThamSo._0gio) {
				giolam = ThamSo._0gio;
				AutoClosingMessageBox.Show("Giờ ra phải lớn hơn giờ vào.", "Lỗi", 1500);
				return;
			}

			string lydo = string.Empty;
			if (string.IsNullOrWhiteSpace(cbLyDo.Text)) {
				lydo = cbLyDo.Text;
			}
			string ghichu = tbGhiChu.Text;

			// kiểm tra chọn NV chưa, chưa thì báo
			DataTable tableCheckNV = dgrdDSNVTrgPhg.DataSource as DataTable;
			if (tableCheckNV == null) return;
			DataRow[] arrRows = tableCheckNV.Select("check = true", "UserEnrollNumber asc");
			if (arrRows.Length == 0) {
				AutoClosingMessageBox.Show("Bạn chưa chọn nhân viên.", "Thông báo", 1500);
				return;
			}

			// đã chọn NV, lấy ds mã CC đã chọn
			bool flag = true;
			foreach (DataRow row in arrRows) {
				//dsNVcheck.Add((int)row["UserEnrollNumber"]);
				int iUserEnrollNumber = (int)row["UserEnrollNumber"];
				if (ca.ShiftID == int.MinValue)
					XL.TaoCaTuDo(ca, timeBD, ThamSo._8gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1f);
				else if (ca.ShiftID == int.MinValue + 1)
					XL.TaoCaTuDo(ca, timeBD, ThamSo._12gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1.5f);

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
					n = DAL.ThemChamCongTay(iUserEnrollNumber, timeBD, ngay.Add(ca3.OffDutyTS), ca3.ShiftID, ca3.ShiftCode
						, ca3.OnnDutyTS.ToString(@"hh\:mm"), ca3.OffDutyTS.ToString(@"hh\:mm")
						, Convert.ToInt32(ca3.LateGraceTS.TotalMinutes), Convert.ToInt32(ca3.EarlyGraceTS.TotalMinutes), Convert.ToInt32(ca3.AfterOTTS.TotalMinutes)
						, ca3.DayCount, Convert.ToSingle(ca3.WorkingTimeTS.TotalMinutes), ca3.Workingday, 0, tinhPC150, lydo,ghichu);
					// add thêm 1 ngày ở thời gian bd vì ca 1 của ngày hôm sau
					n = DAL.ThemChamCongTay(iUserEnrollNumber, ngay.AddDays(1d).Add(ca1.OnnDutyTS).Add(new TimeSpan(0, 0, 1)), timeKT, ca1.ShiftID, ca1.ShiftCode
						, ca1.OnnDutyTS.ToString(@"hh\:mm"), ca1.OffDutyTS.ToString(@"hh\:mm")
						, Convert.ToInt32(ca1.LateGraceTS.TotalMinutes), Convert.ToInt32(ca1.EarlyGraceTS.TotalMinutes), Convert.ToInt32(ca1.AfterOTTS.TotalMinutes)
						, ca1.DayCount, Convert.ToSingle(ca1.WorkingTimeTS.TotalMinutes), ca1.Workingday, sophutOT, tinhPC150, lydo, ghichu);
				}
				else {
					// BUG chú ý thêm 1 giây để tránh tình trạng giờ vào trùng giờ ra
					n = DAL.ThemChamCongTay(iUserEnrollNumber, timeBD.Add(new TimeSpan(0, 0, 1)), timeKT, ca.ShiftID, ca.ShiftCode
						, onnduty, offduty, lategrace, earlygrace, afterot, daycount, wktime, wkdayy, sophutOT, tinhPC150, lydo,ghichu);
				}
				if (n == 0) {
					MessageBox.Show("Có lỗi trong quá trình thực hiện.");
					flag = false;
					break;
				}
			}
			if (flag) {
				AutoClosingMessageBox.Show("Thêm giờ chấm tay cho các nhân viên thành công.", "Thông báo", 2000);
			}
		}



		private void frm_ChamCongTay_Load(object sender, EventArgs e) {
			ThamSo.VeCheckBox_CheckAll(dgrdDSNVTrgPhg, checkAll_GridDSNV, checkAll_CheckedChanged, new Point(7, 3));

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

			// 1. khởi tạo các biến cục bộ
			flstIDPhongBan = new List<int>();

			//2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan : xoá dữ liệu trước và load
			fTablePhongBan = ThamSo.TablePhongBan.Copy();
			treePhongBan.Nodes.Clear();
			loadTreePhgBan(treePhongBan, fTablePhongBan);

			// 3. Duyệt  dữ liệu toàn bộ danh sách nhân viên được phép thao tác(và thêm cột check) 			
			// và  khởi tạo các giá trị mặc định cho từng nhân viên  
			fTableDSNV = ThamSo.DataTableDSNV.Copy();
			fTableDSNV.Columns.Add("check", typeof(bool));

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
			Double giolam = (string.IsNullOrEmpty(tbGioLam.Text)) ? 0f : Double.Parse(tbGioLam.Text);
			giolam = giolam + sophutlamthem.TotalHours;
			Double cong = 0f;

			if (ca.ShiftID == 0 || ca.ShiftID == int.MinValue || ca.ShiftID == int.MinValue + 1) cong = giolam / 8f;
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
