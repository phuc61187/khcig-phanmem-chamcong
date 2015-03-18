using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;
using log4net;

namespace ChamCong_v04.UI.XacNhan {
	public partial class frm_XN_PC50 : Form {
		#region log tooltip và hàm ko quan trọng
		private readonly ILog lg = LogManager.GetLogger("frm_XN_PC50");

		public frm_XN_PC50() {
			log4net.Config.XmlConfigurator.Configure();
			InitializeComponent();
			IsReload = false;
		}
		private void toolTipHint_Draw(object sender, DrawToolTipEventArgs e) {
			Font f = new Font("Arial", 10.0f);
			e.DrawBackground();
			e.DrawBorder();
			e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, new PointF(2f, 2f));
		}

		private void toolTipHint_Popup(object sender, PopupEventArgs e) {
			Size temp = TextRenderer.MeasureText(toolTipHint.GetToolTip(e.AssociatedControl), new Font("Arial", 10.0f));
			temp.Width += 4;
			temp.Height += 4;
			e.ToolTipSize = temp;
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}


		#endregion


		public bool IsReload;
		public List<cUserInfo> m_DSNV; // từ form xem công chuyển qua
		public DataRow[] m_arrRecd;


		private void btnXacNhan_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			var loai = 0;
			var temp = string.Empty;
			if (radKhongPC50.Checked) loai = -1;
			else if (radPC50.Checked) {
				loai = 1;
			}

			#region hỏi lại trước khi thực hiện

			if (loai == -1) {
				if (MessageBox.Show(Resources.Text_CancelXacNhan_PCTC50, Resources.Caption_XacNhan,MessageBoxButtons.OKCancel) == DialogResult.Cancel) {
					return;
				}
			}
			else {
				if (MessageBox.Show(Resources.Text_XNPCTC50, Resources.Caption_XacNhan, MessageBoxButtons.OKCancel) == DialogResult.Cancel) {
					return;
				}
			}

			#endregion

			IsReload = true;

			foreach (var row in m_arrRecd) {
				var nhanvien = (cUserInfo)row["cUserInfo"];
				var ngay = (DateTime)row["TimeStrNgay"];
				var ngayCong = (cNgayCong) row["cNgayCong"];
				//if (ngay <= XL2.NgayCuoiThangKetCong && XL2.NgayCuoiThangKetCong != DateTime.MinValue) continue;//tbd temp patch// ko thực hiện đối với các ngày trong tháng đã kết công
				if (loai == -1) {
					XL.CheckTinhPC50_UpdORInsNew_Sort(nhanvien, ngay, false);
					XL.TinhPCTC_CuaNgay(ngayCong, false);
					XL.TinhPCDB_CuaNgay(ngayCong, nhanvien.DSXNPhuCapDB);
				}
				else {
					XL.CheckTinhPC50_UpdORInsNew_Sort(nhanvien, ngay, true);
					XL.TinhPCTC_CuaNgay(ngayCong, true);
					XL.TinhPCDB_CuaNgay(ngayCong, nhanvien.DSXNPhuCapDB);
				}
				//XL.TinhPCTC_CuaNgay(ngayCong, nhanvien.DSXNPhuCap50, nhanvien.DSNgayCong);
				//XL.TinhLaiPhuCapDB(nhanvien.DSXNPhuCapDB, nhanvien.DSNgayCong);
			}
			this.Close();
		}



	}
}
