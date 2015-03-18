using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DTO;
using ChamCong_v04.Properties;
using MySetting = ChamCong_v04.Properties.Settings;

namespace ChamCong_v04.UI {
	public partial class frmDSCa2 : Form {
		public cCa SelectedShift;
		public frmDSCa2() {
			InitializeComponent();
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			Close();
		}

		private void frmDSCa_Load(object sender, EventArgs e) {
			#region gán datasource cho các datagrid

			dgrd4h.AutoGenerateColumns = dgrd8h.AutoGenerateColumns = dgrd12h.AutoGenerateColumns = dgrd16h.AutoGenerateColumns = dgrdCaTuDo.AutoGenerateColumns = false;
			var dsca4h = (from ca in XL.DSCa
						  where Math.Abs(ca.Workingday - 0.5f) < 0.01f
						  select ca).ToList();
			var dsca8h = (from ca in XL.DSCa
						  where Math.Abs(ca.Workingday - 1f) < 0.01f
						  select ca).ToList();
			var dsca12h = (from ca in XL.DSCa
						   where Math.Abs(ca.Workingday - 1.5f) < 0.01f
						   select ca).ToList();
			var dsca16h = (from ca in XL.DSCa
						   where Math.Abs(ca.Workingday - 2f) < 0.01f
						   select ca/*new { ca = ca, ID = ca.ID, Code = ca.Code, VaoCa = ca.Duty.Onn, RaaCa = ca.Duty.Off, QuaDem = ca.QuaDem, MoTa = ca.MoTa }*/).ToList();
			var dsCaTuDo = new List<dynamic>();
			//cCa ca4h = new cCa { ID = int.MinValue + 2, Code = Properties.Settings.Default.shiftCodeCa4h, };//ver 4.0.0.4
			cCa ca8h = new cCa { ID = int.MinValue + 0, Code = Properties.Settings.Default.shiftCodeCa8h, };
			//cCa ca12h = new cCa { ID = int.MinValue + 1, Code = Properties.Settings.Default.shiftCodeCa12h, };
			//cCa ca16h = new cCa { ID = int.MinValue + 3, Code = Properties.Settings.Default.shiftCodeCa16h, };//ver 4.0.0.4
			cCa caTD8h = new cCa { ID = int.MinValue + 10, Code = Properties.Settings.Default.shiftCodeCaTD8h, };//ver 4.0.0.4
			//XL.TaoCaTuDo(ca4h, DateTime.MinValue, XL2._04gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, MySetting.Default.kyHieuCCCa4h);//ver 4.0.0.4
			XL.TaoCaTuDo(ca8h, DateTime.MinValue, XL2._08gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, MySetting.Default.kyHieuCCCa8h);
			//XL.TaoCaTuDo(ca12h, DateTime.MinValue, XL2._12gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, MySetting.Default.kyHieuCCCa12h);
			//XL.TaoCaTuDo(ca16h, DateTime.MinValue, XL2._16gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, MySetting.Default.kyHieuCCCa16h);//ver 4.0.0.4
			XL.TaoCaTuDo(caTD8h, DateTime.MinValue, XL2._08gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, MySetting.Default.kyHieuCCCaTD);//ver 4.0.0.4
			dsCaTuDo.Add(ca8h/*new { ca = ca8h, ID = ca8h.ID, Code = ca8h.Code, VaoCa = DBNull.Value, RaaCa = DBNull.Value, QuaDem = false, MoTa = string.Format(Settings.Default.MoTaCaCoSo, 8) }*/);
			//dsCaTuDo.Add(new { ca = ca12h, ID = ca12h.ID, Code = ca12h.Code, VaoCa = DBNull.Value, RaaCa = DBNull.Value, QuaDem = false, MoTa = string.Format(Settings.Default.MoTaCaCoSo, 12) });
			//dsCaTuDo.Add(new { ca = ca4h, ID = ca4h.ID, Code = ca4h.Code, VaoCa = DBNull.Value, RaaCa = DBNull.Value, QuaDem = false, MoTa = string.Format(Settings.Default.MoTaCaCoSo, 4) });//ver 4.0.0.4
			//dsCaTuDo.Add(new { ca = ca16h, ID = ca16h.ID, Code = ca16h.Code, VaoCa = DBNull.Value, RaaCa = DBNull.Value, QuaDem = false, MoTa = string.Format(Settings.Default.MoTaCaCoSo, 16) });//ver 4.0.0.4
			//dsCaTuDo.Add(new { ca = caTD8h, ID = caTD8h.ID, Code = ca16h.Code, VaoCa = DBNull.Value, RaaCa = DBNull.Value, QuaDem = false, MoTa = string.Format(Settings.Default.MoTaCaCoSo, 16) });//ver 4.0.0.4
			dgrd4h.DataSource = dsca4h;
			dgrd8h.DataSource = dsca8h;
			dgrd12h.DataSource = dsca12h;
			dgrd16h.DataSource = dsca16h;
			dgrdCaTuDo.DataSource = dsCaTuDo;

			#endregion

			//set mặc định ca được chọn là ca từ form gọi truyền qua
			if (SelectedShift == null)// chế độ hàng loạt, các ca gồm nhiều loại khác nhau
			{
				// mặc định chọn tab 8h
				tabControl1.SelectTab(tab8h);
				tabControl1.Update();
			}
			else if (SelectedShift.ID == int.MinValue)// ca tự do
			{
				tabControl1.SelectTab(tabCaTuDo);
				tabControl1.Update();
				dgrdCaTuDo.Rows[0].Selected = true;
				dgrdCaTuDo.Update();
			}
			else// ca chuẩn
			{
				// 1. xác định ca chuẩn thuộc grid nào thì chọn mặc định grid đó, 
				// 2. tìm trong grid có ca đang chọn ko? nếu có thì mặc định chọn dòng đó,tab đó; ko thì chọn mặc định tab đầu tiên,
				//set selected tab
				tabControl1.SelectedTab = GetTabPage(SelectedShift.Workingday);
				tabControl1.Update();

				DataGridView dataGrid = GetDataGrid(SelectedShift.Workingday);
				var currRow = (from DataGridViewRow dataGridViewRow in dataGrid.Rows
							   let rowView = dataGridViewRow.DataBoundItem as dynamic
							   where rowView.ID == SelectedShift.ID
							   select dataGridViewRow).Single();
				
				// set selected row
				dataGrid.Rows[currRow.Index].Selected = true;
				dataGrid.FirstDisplayedScrollingRowIndex = currRow.Index;
				dataGrid.Refresh();
				dataGrid.Update();
			}
		}


		private void btnOK_Click(object sender, EventArgs e) {
			DataGridView dataGrid = GetDataGrid(tabControl1);
			// chưa chọn dòng nào thì thoát, ko gán selectedShift
			if (dataGrid.SelectedRows.Count == 0) {
				MessageBox.Show(Resources.Text_ChuaLuaChonCa, Resources.Caption_ThongBao);
			}
			else {
				var selectedRow = dataGrid.SelectedRows[0];
				dynamic d = (selectedRow.DataBoundItem);
				SelectedShift = d/*d.ca*/;
			}
			Close();

		}
		private void btnCancel_Click(object sender, EventArgs e) {
			// nếu ko chọn ca nào hết thì đóng form, ko gán selectedshift
			Close();
		}

		private DataGridView GetDataGrid(TabControl tabControl) {
			if (tabControl.SelectedTab == tab4h) return dgrd4h;
			if (tabControl.SelectedTab == tab8h) return dgrd8h;
			if (tabControl.SelectedTab == tab12h) return dgrd12h;
			return tabControl.SelectedTab == tab16h ? dgrd16h : dgrdCaTuDo;
		}

		private DataGridView GetDataGrid(float workingday) {
			if (Math.Abs(workingday - 0.5f) < 0.01f) return dgrd4h;
			if (Math.Abs(workingday - 1f) < 0.01f) return dgrd8h;
			if (Math.Abs(workingday - 1.5f) < 0.01f) return dgrd12h;
			return Math.Abs(workingday - 2f) < 0.01f ? dgrd16h : dgrdCaTuDo;
		}
		private TabPage GetTabPage(float workingday) {
			if (Math.Abs(workingday - 0.5f) < 0.01f) return tab4h;
			if (Math.Abs(workingday - 1f) < 0.01f) return tab8h;
			if (Math.Abs(workingday - 1.5f) < 0.01f) return tab12h;
			return Math.Abs(workingday - 2f) < 0.01f ? tab16h : tabCaTuDo;
		}

		private void tabControl1_Selected(object sender, TabControlEventArgs e) {

		}
	}
}
