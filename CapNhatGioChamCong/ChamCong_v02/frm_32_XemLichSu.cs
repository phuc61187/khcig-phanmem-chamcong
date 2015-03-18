using System;
using System.Data;
using System.Windows.Forms;
using ChamCong_v02.DAO;

namespace ChamCong_v02 {
	public partial class frm_32_XemLichSu : Form {

		public frm_32_XemLichSu() {
			InitializeComponent();
			dataGridView1.AutoGenerateColumns = false;
		}


		private void XemLichSuSuaGioChamCong_Load(object sender, EventArgs e)
		{
			DataTable table = DAL.SelLichSuSuaGioChamCong(ThamSo.currUserID);
			table.Columns.Add("TenThaoTac", typeof(string));
			table.Columns.Add("KieuChamCu", typeof(string));
			table.Columns.Add("KieuChamMoi", typeof(string));
			int loai = 0;
			foreach (DataRow row in table.Rows) {
				loai = (int)row["CommandType"];

				if (loai == -1) { // xoá
					row["TenThaoTac"] = "Xóa";
					#region set null các cột liên quan đến giờ mới
					row["TimeStrNew"] = DBNull.Value;
					row["MachineNoNew"] = DBNull.Value;
					row["KieuChamMoi"] = DBNull.Value;
					#endregion
				}
				else if (loai == 0) { // sửa
					row["TenThaoTac"] = "Sửa";
				}
				else {// thêm
					row["TenThaoTac"] = "Thêm";
					#region set null các cột liên quan đến giờ cũ
					row["TimeStrOld"] = DBNull.Value;
					row["MachineNoOld"] = DBNull.Value;
					row["KieuChamCu"] = DBNull.Value;
					#endregion
				}

				if (row["MachineNoOld"] != DBNull.Value) {
					int maycu = (int)row["MachineNoOld"];
					if (maycu % 2 == 1) row["KieuChamCu"] = "Vào";
					else row["KieuChamCu"] = "Ra";
				}
				if (row["MachineNoNew"] != DBNull.Value) {
					int maymoi = (int)row["MachineNoNew"];
					if (maymoi % 2 == 1) row["KieuChamMoi"] = "Vào";
					else row["KieuChamMoi"] = "Ra";
				}
				
			}

			dataGridView1.DataSource = table;
		}

	}
}
