using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DTO;
using ChamCong_v04.Properties;
using log4net;

namespace ChamCong_v04.UI.XacNhan {
	public partial class frm_XN_PC100 : Form {
		#region log tooltip và hàm ko quan trọng
		private readonly ILog lg = LogManager.GetLogger("frm_XN_PC100");

		public frm_XN_PC100() {
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
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			var loai = 0;
			var pcngay = 0;
			var pcdem = 0;
			var temp = string.Empty;
			if (radKhongPC.Checked) loai = -1;
			else if (radPCNgayNghi.Checked) {
				loai = 200;
				pcngay = XL2.PC100;
				pcdem = XL2.PC160;
				temp = "ngày nghỉ {0}% (ban ngày), {1}% (ban đêm)";
				temp = string.Format(temp, XL2.PC100, XL2.PC160);
			}
			else if (radPCNgayLe.Checked) {
				loai = 300;
				pcngay = XL2.PC200;
				pcdem = XL2.PC290;
				temp = "ngày lễ {0}% (ban ngày), {1}% (ban đêm), chưa kể công lễ, tết";
				temp = string.Format(temp, XL2.PC200, XL2.PC290);
			}
			else if (radPCCus1.Checked)
			{
				loai = 1;
				pcngay = (int) (numPCCus1.Value);
				temp = "tuỳ chỉnh " + pcngay +"% cho giờ làm trên 8 tiếng";
			}
			else if (radPCCus2.Checked)
			{
				loai = 2;
				pcngay = (int) (numPCCus2.Value);
				temp = "tuỳ chỉnh " + pcngay +"% cho giờ làm";
			}

			#region hỏi lại trước khi thực hiện

			if (loai == -1) {
				if (MessageBox.Show(string.Format("Bạn muốn huỷ xác nhận tính phụ cấp {0}?", temp), Resources.Caption_XacNhan,
									MessageBoxButtons.OKCancel) == DialogResult.Cancel) {
					return;
				}
			}
			else {
				if (MessageBox.Show(string.Format("Bạn muốn xác nhận các ngày làm việc vừa chọn tính phụ cấp {0}?", temp),
									Resources.Caption_XacNhan, MessageBoxButtons.OKCancel) == DialogResult.Cancel) {
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
					XL.HuyBo_TinhPCDB(nhanvien, ngayCong, nhanvien.DSXNPhuCapDB);
					//XL.TinhPCNgayVang(ngayCong);
				}
				else {
					XL.TinhPCDB(nhanvien, ngayCong, ngay, loai, pcngay, pcdem, temp);
					XL.TinhPCNgayVang(ngayCong);
				}

			}
			this.Close();
		}

		private void frm_XN_PC100_Load(object sender, EventArgs e) {
			radPCNgayNghi.Text = string.Format(radPCNgayNghi.Text, XL2.PC100, XL2.PC160);
			radPCNgayLe.Text = string.Format(radPCNgayLe.Text, XL2.PC200, XL2.PC290);

		}





	}
}
