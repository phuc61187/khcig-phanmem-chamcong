using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;
using ChamCong_v03.Properties;
using log4net;

namespace ChamCong_v03 {
	public partial class frm_112_XacNhanPC100 : Form {
		private readonly ILog lg = LogManager.GetLogger("frm_112_XacNhanTangCa");

		public bool IsReload;
		public List<cUserInfo> m_DSNV; // từ form xem công chuyển qua
		public DataRow[] m_arrRecd;

		public frm_112_XacNhanPC100() {
			log4net.Config.XmlConfigurator.Configure();
			InitializeComponent();
			IsReload = false;
		}

		private void frmXacNhanTangCa_Load(object sender, EventArgs e) {

		}



		private void btnXacNhan_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			var loai = 0;
			var pcngay = 0;
			var pcdem = 0;
			var temp = string.Empty;
			if (radKhongPC.Checked) loai = -1;
			else if (radPCNgayNghi.Checked) {
				loai = 200;
				pcngay = XL2.PC100;
				pcdem = XL2.PC160;
				temp = "ngày nghỉ 100% (ban ngày), 150% (ban đêm)";
			}
			else if (radPCNgayLe.Checked) {
				loai = 300;
				pcngay = XL2.PC200;
				pcdem = XL2.PC290;
				temp = "ngày lễ 200% (ban ngày), 250% (ban đêm), chưa kể công lễ, tết";
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
				if (MessageBox.Show(string.Format("Bạn muốn huỷ xác nhận tính phụ cấp {0}?", temp), Resources.capXacNhan,
									MessageBoxButtons.OKCancel) == DialogResult.Cancel) {
					return;
				}
			}
			else {
				if (MessageBox.Show(string.Format("Bạn muốn xác nhận các ngày làm việc vừa chọn tính phụ cấp {0}?", temp),
									Resources.capXacNhan, MessageBoxButtons.OKCancel) == DialogResult.Cancel) {
					return;
				}
			}

			#endregion

			IsReload = true;

			foreach (var row in m_arrRecd) {
				var nhanvien = (cUserInfo)row["cUserInfo"];
				var ngay = (DateTime)row["TimeStrNgay"];
				if (ngay <= XL2.ThangKetCong && XL2.ThangKetCong != DateTime.MinValue) continue;// ko thực hiện đối với các ngày trong tháng đã kết công
				if (loai == -1) {
					XL.HuyBo_TinhPCDB(nhanvien, ngay);
				}
				else {
					XL.TinhPCDB(nhanvien, ngay, loai, pcngay, pcdem);
				}
			}
			this.Close();
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}





	}
}
