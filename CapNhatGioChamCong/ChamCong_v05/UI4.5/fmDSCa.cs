using ChamCong_v05.BUS;
using ChamCong_v05.DTO;
using ChamCong_v05.Helper;
using ChamCong_v05.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChamCong_v05.UI4._5 {
	public partial class fmDSCa : Form {
		public List<dynamic> DSTatCaCacCa;
		public YesNoCancel m_YesNoCancel = YesNoCancel.Cancel;
		public dynamic selectedCa = null;
		public fmDSCa() {
			InitializeComponent();
		}

		private void fmDSCa_Load(object sender, EventArgs e) {
			#region gán datasource cho các datagrid

			var dscaThuong = (from ca in XL.DSCa
							  select new { ca = ca, ID = ca.ID, Code = ca.Code, VaoCa = ca.Duty.Onn, RaaCa = ca.Duty.Off, QuaDem = ca.QuaDem, MoTa = ca.MoTa, WorkingTime = ca.WorkingTimeTS }).ToList();
			var dsCaTuDo = new List<dynamic>();
			cCa ca8h = new cCa { ID = int.MinValue + 0, Code = Properties.Settings.Default.shiftCodeCa8h, MoTa = string.Format(Settings.Default.MoTaCaTuDo, 8), Workingday = 1f };
			cCa ca12h = new cCa { ID = int.MinValue + 1, Code = Properties.Settings.Default.shiftCodeCa12h, MoTa = string.Format(Settings.Default.MoTaCaTuDo, 12), Workingday = 1.5f };
			cCa ca4h = new cCa { ID = int.MinValue + 2, Code = Properties.Settings.Default.shiftCodeCa4h, MoTa = string.Format(Settings.Default.MoTaCaTuDo, 4), Workingday = 0.5f };//ver 4.0.0.4
			cCa ca16h = new cCa { ID = int.MinValue + 3, Code = Properties.Settings.Default.shiftCodeCa16h, MoTa = string.Format(Settings.Default.MoTaCaTuDo, 16), Workingday = 2f };//ver 4.0.0.4
			dynamic dCa8h = new { ca = ca8h, ID = ca8h.ID, Code = ca8h.Code, VaoCa = TimeSpan.Zero, RaaCa = TimeSpan.Zero, QuaDem = false, MoTa = ca8h.MoTa, WorkingTime = XL2._08gio };
			dynamic dCa12h = new { ca = ca12h, ID = ca12h.ID, Code = ca12h.Code, VaoCa = TimeSpan.Zero, RaaCa = TimeSpan.Zero, QuaDem = false, MoTa = ca12h.MoTa, WorkingTime = XL2._12gio };
			dynamic dCa4h = new { ca = ca4h, ID = ca4h.ID, Code = ca4h.Code, VaoCa = TimeSpan.Zero, RaaCa = TimeSpan.Zero, QuaDem = false, MoTa = ca4h.MoTa, WorkingTime = XL2._04gio };
			dynamic dCa16h = new { ca = ca16h, ID = ca16h.ID, Code = ca16h.Code, VaoCa = TimeSpan.Zero, RaaCa = TimeSpan.Zero, QuaDem = false, MoTa = ca16h.MoTa, WorkingTime = XL2._16gio };
			dsCaTuDo.Add(dCa8h);
			dsCaTuDo.Add(dCa12h);
			dsCaTuDo.Add(dCa4h);
			dsCaTuDo.Add(dCa16h);
			DSTatCaCacCa = new List<dynamic>();
			DSTatCaCacCa.AddRange(dsCaTuDo);
			DSTatCaCacCa.AddRange(dscaThuong);
			#endregion
			radioChonTGLV.SelectedIndex = 0;
			radioChonTGLV_SelectedIndexChanged(null, null);
			radioChonTGLV.Update();
		}

		private void radioChonTGLV_SelectedIndexChanged(object sender, EventArgs e) {
			if ((int)radioChonTGLV.EditValue == 8) {
				gridControl1.DataSource = this.GetDataSourceDSCa(1f);
			}
			else if ((int)radioChonTGLV.EditValue == 4) {
				gridControl1.DataSource = this.GetDataSourceDSCa(0.5f);
			}
			else if ((int)radioChonTGLV.EditValue == 12) {
				gridControl1.DataSource = this.GetDataSourceDSCa(1.5f);
			}
			else {
				gridControl1.DataSource = this.GetDataSourceDSCa(2f);
			}

		}

		private List<dynamic> GetDataSourceDSCa(float WorkingDay) {
			return (from dynamic item in DSTatCaCacCa
					let wkday = (float)item.ca.Workingday - WorkingDay
					where wkday < 0.01f && wkday > -0.01f
					select item).ToList();
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.m_YesNoCancel = YesNoCancel.Cancel;
			
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			this.m_YesNoCancel = YesNoCancel.Cancel;
			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			int[] selectedRowHandle = gridView1.GetSelectedRows();
			if (selectedRowHandle.Count() > 1)
			{
				MessageBox.Show("chọn quá nhiều row");
				return;
			}

			dynamic objectCa = gridView1.GetRow(selectedRowHandle[0]);
			if (objectCa == null || objectCa.ca == null) return;
			selectedCa = objectCa;
			m_YesNoCancel = YesNoCancel.Yes;
			Close();
		}
	}
}
