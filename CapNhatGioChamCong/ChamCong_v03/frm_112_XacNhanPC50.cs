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
	public partial class frm_112_XacNhanPC50 : Form {
		private readonly ILog lg = LogManager.GetLogger("frm_112_XacNhanTangCa");

		public bool IsReload;
		public List<cUserInfo> m_DSNV; // từ form xem công chuyển qua
		public DataRow[] m_arrRecd;

		public frm_112_XacNhanPC50() {
			log4net.Config.XmlConfigurator.Configure();
			InitializeComponent();
			IsReload = false;
		}


		private void btnXacNhan_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
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
				if (
					MessageBox.Show("Bạn muốn huỷ xác nhận tính phụ cấp tăng cường 50%?", Resources.capXacNhan,
									MessageBoxButtons.OKCancel) == DialogResult.Cancel) {
					return;
				}
			}
			else {
				if (
					MessageBox.Show("Bạn muốn xác nhận các ngày làm việc vừa chọn tính phụ cấp tăng cường 50% nếu làm làm việc trên 8 tiếng?",
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
					XL.CheckTinhPC50(nhanvien, ngay, false);
				}
				else {
					XL.CheckTinhPC50(nhanvien, ngay, true);
				}
				XL.TinhLaiPhuCapTC(nhanvien.DSXNPhuCap50, nhanvien.DSNgayCong);
				XL.TinhLaiPhuCapDB(nhanvien.DSXNPhuCapDB, nhanvien.DSNgayCong);
			}
			this.Close();
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}





	}
}
