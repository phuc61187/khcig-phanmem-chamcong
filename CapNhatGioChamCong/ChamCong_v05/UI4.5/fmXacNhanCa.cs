using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v05.BUS;
using ChamCong_v05.DTO;
using ChamCong_v05.Helper;
using ChamCong_v05.UI4._5;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ChamCong_v05.zMisc {
	public partial class fmXacNhanCa : Form {
		public List<cUserInfo> m_DSNV;
		public DateTime m_NgayBD;
		public DateTime m_NgayKT;
		public DataTable m_TableGioThieuChamCong;

		public fmXacNhanCa() {
			InitializeComponent();
			//SqlDataAccessHelper.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=WiseEyeV5Express;Integrated Security=true";
			m_TableGioThieuChamCong = this.CreateDataTable_CIO_ThieuChamCong();
			GridLocalizer.Active = new VietGridLocalizer();
			Localizer.Active = new Localizer();
		}

		private void Form1_Load(object sender, EventArgs e) {
			XacDinhGioThieuChamCong(this.m_DSNV);
			gridControl1.DataSource = this.m_TableGioThieuChamCong;
		}

		private DataTable CreateDataTable_CIO_ThieuChamCong() {
			DataTable kq = new DataTable();
			kq.Columns.Add("UserEnrollNumber", typeof(int));
			kq.Columns.Add("UserFullName", typeof(string));
			kq.Columns.Add("UserFullCode", typeof(string));
			kq.Columns.Add("Ngay", typeof(DateTime));
			kq.Columns.Add("GioVao", typeof(DateTime));
			kq.Columns.Add("GioRa", typeof(DateTime));
			kq.Columns.Add("DSCa", typeof(string));
			kq.Columns.Add("cCheckInOut", typeof(cCheckInOut));
			kq.Columns.Add("cNgayCong", typeof(cNgayCong));
			kq.Columns.Add("cUserInfo", typeof(cUserInfo));
			return kq;
		}

		public void XacDinhGioThieuChamCong(List<cUserInfo> listNhanVien) {
			if (listNhanVien == null) return;
			foreach (cUserInfo nhanvien in listNhanVien) {
				foreach (cNgayCong ngayCong in nhanvien.DSNgayCong.Where(item => item.Ngay >= m_NgayBD && item.Ngay <= m_NgayKT).ToList()) {
					foreach (cCheckInOut CIO in ngayCong.DSVaoRa.Where(item => item.HaveINOUT < 0)) {
						DataRow newRow = this.CreateDataRow_CIO_ThieuChamCong(this.m_TableGioThieuChamCong, nhanvien, ngayCong, CIO);
						m_TableGioThieuChamCong.Rows.InsertAt(newRow, 0);
					}
				}
			}
		}

		private DataRow CreateDataRow_CIO_ThieuChamCong(DataTable dataTable, cUserInfo Nhanvien, cNgayCong NgayCong, cCheckInOut CIO) {
			DataRow kq = dataTable.NewRow();
			kq["UserEnrollNumber"] = Nhanvien.MaCC;
			kq["UserFullName"] = Nhanvien.TenNV;
			kq["UserFullCode"] = Nhanvien.MaNV;
			kq["Ngay"] = NgayCong.Ngay;
			kq["GioVao"] = CIO.Vao != null ? CIO.Vao.Time : (object)DBNull.Value;
			kq["GioRa"] = CIO.Raa != null ? CIO.Raa.Time : (object)DBNull.Value;
			kq["DSCa"] = CIO.ExportKyHieuThuocCa1_5(true).XoaKyTuPhanCachDauTien();
			kq["cCheckInOut"] = CIO;
			kq["cUserInfo"] = Nhanvien;
			kq["cNgayCong"] = NgayCong;

			return kq;
		}


		private void RemoveDataRowHaveDSNVReload(ref DataTable table, List<cUserInfo> DSNVReload) {
			List<DataRow> rowMustRemove = (from DataRow row in table.Rows
										   let nhanvien = (cUserInfo)row["cUserInfo"]
										   where nhanvien != null && DSNVReload.Exists(item => item.MaCC == nhanvien.MaCC)
										   select row).ToList();
			foreach (DataRow row in rowMustRemove) {
				table.Rows.Remove(row);
			}
		}

		private void RemoveDSNVCanReload(ref List<cUserInfo> DSNVReload) {
			for (int i = 0; i < DSNVReload.Count; i++) {
				cUserInfo nhanvien = DSNVReload[i];
				this.m_DSNV.Remove(nhanvien);
			}
		}

		private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e) {
			if (e.SelectedControl != gridControl1) return;

			ToolTipControlInfo info = null;
			//Get the view at the current mouse position
			GridView view = gridControl1.GetViewAt(e.ControlMousePosition) as GridView;
			if (view == null) return;
			//Get the view's element information that resides at the current position
			GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
			//Display a hint for row indicator cells
			if (hi.HitTest == GridHitTest.RowCell) {
				//An object that uniquely identifies a row indicator cell
				DataRow dataRow = view.GetDataRow(hi.RowHandle);
				cNgayCong ngayCong = (cNgayCong)dataRow["cNgayCong"];
				if (ngayCong == null) return;
				string text = XL.TaoTooltip5(ngayCong);
				object o = hi.HitTest.ToString() + hi.RowHandle.ToString();
				//string text = "Row " + hi.RowHandle.ToString();
				info = new ToolTipControlInfo(o, text);
			}
			//Supply tooltip information if applicable, otherwise preserve default tooltip (if any)
			if (info != null)
				e.Info = info;

		}


		private void buttonEdit1_Properties_ButtonClick(object sender, ButtonPressedEventArgs e) {
			if (e.Button.Kind == ButtonPredefines.Search) {
				fmDSCa formDSCa = new fmDSCa();
				formDSCa.ShowDialog();
				if (formDSCa.m_YesNoCancel == YesNoCancel.Yes) {
					dynamic selectedCa = formDSCa.selectedCa;
				}
			}

		}


	}
}
