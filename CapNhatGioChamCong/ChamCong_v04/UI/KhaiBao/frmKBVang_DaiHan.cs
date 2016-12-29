using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DAL;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;

namespace ChamCong_v04.UI.KhaiBao {
	public partial class frmKBVang_DaiHan : Form
	{
		public bool IsReload;
		public List<int> listMaCC_NV;
		public frmKBVang_DaiHan() {
			InitializeComponent();
		}
		private void btnDong_Click(object sender, EventArgs e) {
			Close();
		}

		private void frmKBVang_DaiHan_Load(object sender, EventArgs e)
		{
			dtpNgayBD.Value = MyUtility.FirstDayOfMonth(DateTime.Today);
			dtpNgayKT.Value = MyUtility.LastDayOfMonth(DateTime.Today);

			var tableLV = SqlDataAccessHelper.ExecuteQueryString("Select * from LoaiVang");
			var dataRows = (from DataRow row in tableLV.Rows
			                let absentCode = row["AbsentCode"].ToString()
			                where absentCode != "BD" && absentCode != "TS"
			                select row).ToList();
			for (int i = 0; i < dataRows.Count; i++)
			{
				tableLV.Rows.Remove(dataRows[i]);
			}
			
			cbLoaiVang.ValueMember = "AbsentCode";
			cbLoaiVang.DisplayMember = "AbsentDescription";
			cbLoaiVang.DataSource = tableLV;
			cbLoaiVang.SelectedIndex = 0;
		}

		private void btnThem_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			// lấy ngày check
			var ngayBD = dtpNgayBD.Value.Date;
			var ngayKT = dtpNgayKT.Value.Date;
			if (ngayBD > ngayKT) MyUtility.Swap(ref ngayBD, ref ngayKT);
			List<DateTime> DSNgayCheck = new List<DateTime>();
			for (DateTime indexNgay = ngayBD; indexNgay <= ngayKT; indexNgay = indexNgay.AddDays(1d))
				DSNgayCheck.Add(indexNgay);


			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(DSNgayCheck[0], DSNgayCheck[DSNgayCheck.Count - 1])) {
				MessageBox.Show(String.Format(Resources.Text_KhoangTGDaKetCong_KoChinhSuaGioCC, "khai báo vắng", "khai báo vắng", "khai báo vắng"),
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
				return;
			}

			#endregion
			
			#region hỏi lại trước khi thực hiện
			if (MessageBox.Show(Resources.Text_XacNhanThemKhaiVang, Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}
			#endregion

			// lấy loại vắng
			if (cbLoaiVang.SelectedItem == null) {
				ACMessageBox.Show("Bạn chưa chọn loại vắng", "Thông báo", 2000);
				return;
			}
			var rowLV = cbLoaiVang.SelectedItem as DataRowView;

			var absentCode = rowLV["AbsentCode"].ToString();
			var workingDay = 1f;
			var workingTime = 8f;

			var formatString = "[{0}] đã xin phép vắng [{1}] [{2}] ngày ngày [{3}]";
			var tableVang = DAO.LietKeNgayVangChoNV(listMaCC_NV, DSNgayCheck.Min(), DSNgayCheck.Max());

			if (tableVang.Rows.Count > 0) {
				List<Warning> listWarning = new List<Warning>();
				foreach (var nv in listMaCC_NV) {//duyệt từng nhân viên
					foreach (var ngay in DSNgayCheck) {// duyệt từng ngày check vắng của nhân viên
						DateTime ngay1 = ngay;
						int nv1 = nv;
						var result = (from DataRow item in tableVang.Rows
									  where (int)item["UserEnrollNumber"] == nv1 && (DateTime)item["TimeDate"] == ngay1
									  select item).ToList(); // lấy danh sách các xin phép vắng trong ngày xác định
						if (result.Any())
						{
							// nếu có xin phép vắng thì ghi lại chuỗi các xin phép vắng đó
							var userfullname = result[0]["UserFullName"].ToString();
							listWarning.AddRange(result.Select(row123 => new Warning()
								{
									CB = "Đã có xin phép vắng", 
									ND = string.Format(formatString, userfullname, row123["AbsentCode"], 
															((float) row123["Workingday"]).ToString("0.0#"),
															((DateTime)row123["TimeDate"]).ToString("dd/MM/yyyy"))
								}));
						}
					}
				}

				// hiện form cảnh báo, nếu xác nhận tiếp tục thì thực hiện , ko thì dừng
				frmWarning frm = new frmWarning {listWarning = listWarning, WindowState = FormWindowState.Normal, StartPosition = FormStartPosition.CenterParent};
				//Point temp = XL2.GetCenterLocation(this.MdiParent.Size.Width, this.MdiParent.Size.Height, frm.Size.Width, frm.Size.Height);
				//frm.Location = new Point(this.MdiParent.Location.X + temp.X, this.MdiParent.Location.Y + temp.Y);
				frm.ShowDialog();
				if (frm.TiepTuc == false) return;
			}

			IsReload = true; 
			IEnumerable<dynamic> listMaCC_NgayVang = (from macc in listMaCC_NV
											 from ngay in DSNgayCheck
											 select new { MaCC = macc, NgayVang = ngay });

			List<Error> listError = new List<Error>();
			XL.ThemNgayVang(listMaCC_NgayVang, workingDay, workingTime, 0f, absentCode, listError);
			if (listError.Count > 0) {
				frmError frm = new frmError {StartPosition=FormStartPosition.CenterParent, listError = listError };
				frm.ShowDialog();
			}

			Close();
		}

	}
}
