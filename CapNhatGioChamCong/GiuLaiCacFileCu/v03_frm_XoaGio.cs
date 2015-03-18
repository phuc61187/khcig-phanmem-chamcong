using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using GiuLaiCacFileCu.BUS;
using GiuLaiCacFileCu.DAO;
using GiuLaiCacFileCu.DTO;
using log4net;

namespace GiuLaiCacFileCu {
	public partial class v03_frm_XoaGio : Form {
		private readonly ILog lg = LogManager.GetLogger("v03_frm_XoaGio");

		public DataRow[] DSNgayCongChked { get; set; }
		public bool IsReload { get; set; }
		readonly CheckBox checkAllGridTH = new CheckBox();
		void checkAll_CheckedChanged(object sender, EventArgs e) {
			//1. xác định datagrid nào đang check all
			DataGridView tempGrid;
			tempGrid = dgrdGioKDQD;

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

		public v03_frm_XoaGio() {
			InitializeComponent();
			dgrdGioKDQD.AutoGenerateColumns = false;
			log4net.Config.XmlConfigurator.Configure();
			ThamSo.VeCheckBox_CheckAll(dgrdGioKDQD, checkAllGridTH, checkAll_CheckedChanged, new Point(7, 10));

		}

		private void frm_XoaGio_Load(object sender, EventArgs e) {
			// mặc định ko reload lại xem công nếu ko thực hiện thao tác
			IsReload = false;

			// tạo cấu trúc table load datagrid
			DataTable tableChiTiet = XL.TaoCauTrucDataTable(
				new string[] { "check", "UserEnrollNumber", "UserFullCode", "UserFullName", "TimeStrNgay", "Kieu", "TimeStr", "MachineNo", "Source", "objCIO", "objCheck" }
				, new Type[] { typeof(bool), typeof(int), typeof(string), typeof(string), typeof(DateTime), typeof(string), typeof(DateTime), typeof(int), typeof(string), typeof(cChkInOut), typeof(cChk) });
			//load datagrid
			// duyệt qua từng ngày công đã chọn
			foreach (DataRow row in DSNgayCongChked) {
				int iUserEnrollNumber = (int)row["UserEnrollNumber"];
				string name = row["UserFullName"].ToString();
				string manv = row["UserFullCode"].ToString();
				DateTime ngay = (DateTime)row["TimeStrNgay"];
				cNgayCong ngayCong = (cNgayCong)row["cNgayCong"];
				// duyệt qua các giờ vào-ra, bỏ qua các CIO_V
				if (ngayCong.DSVaoRa != null && ngayCong.DSVaoRa.Count > 0) {
					foreach (cChkInOut CIO in ngayCong.DSVaoRa) {
						if (CIO is cChkInOut_V) continue; //bỏ qua các CIO_V
						if (CIO.Vao != null) {
							#region gán giá trị cho các row
							DataRow row1 = tableChiTiet.NewRow();
							row1["check"] = false;
							row1["UserEnrollNumber"] = iUserEnrollNumber;
							row1["UserFullName"] = name;
							row1["UserFullCode"] = manv;
							row1["TimeStrNgay"] = ngay;
							row1["Kieu"] = (CIO.Vao.MachineNo % 2 == 1) ? "Vào" : "Ra";
							row1["TimeStr"] = CIO.Vao.Time;
							row1["MachineNo"] = CIO.Vao.MachineNo;
							row1["Source"] = CIO.Vao.Source;
							row1["objCIO"] = CIO;
							row1["objCheck"] = CIO.Vao;
							tableChiTiet.Rows.Add(row1);
							#endregion
						}
						if (CIO.Raa != null) {
							#region gán giá trị cho các row
							DataRow row2 = tableChiTiet.NewRow();
							row2["check"] = false;
							row2["UserEnrollNumber"] = iUserEnrollNumber;
							row2["UserFullName"] = name;
							row2["UserFullCode"] = manv;
							row2["TimeStrNgay"] = ngay;
							row2["Kieu"] = (CIO.Raa.MachineNo % 2 == 1) ? "Vào" : "Ra";
							row2["TimeStr"] = CIO.Raa.Time;
							row2["MachineNo"] = CIO.Raa.MachineNo;
							row2["Source"] = CIO.Raa.Source;
							row2["objCIO"] = CIO;
							row2["objCheck"] = CIO.Raa;
							tableChiTiet.Rows.Add(row2);
							#endregion
						}
					}
				}
			}
			dgrdGioKDQD.DataSource = tableChiTiet;

		}


		private void btnXoaGio_Click(object sender, EventArgs e) {
            // xác nhận trước khi làm, nếu chọn cancel thì return
            if (MessageBox.Show("Xóa giờ chấm công tự động của nhân viên?", "Xác nhận",MessageBoxButtons.OKCancel)  == DialogResult.Cancel)
                return;

			// lấy thông tin ly do, ghi chu
			string lydo = (cbLyDo.SelectedItem != null) ? cbLyDo.SelectedItem.ToString() : cbLyDo.Text;
			string ghichu = tbGhiChu.Text;

			// nếu thực hiện thì bật yêu cầu reload lại form xem công
			IsReload = true;

			// biến kq thực hiện thành công hay thất bại
			bool flag = true;
			int iUserEnrollNumber = -1;
			string name = string.Empty;
			DateTime ngay = DateTime.MinValue;

			//update datagrid 
			this.BindingContext[dgrdGioKDQD.DataSource].EndCurrentEdit();

			DataTable table = dgrdGioKDQD.DataSource as DataTable; // lấy datatable của datagrid để lọc ra các dòng check = true
			if (table == null) return;
			DataRow[] arrRows = table.Select("check = true");// lấy ds record có check, kiểm tra nếu ko check thì thoát
			if (arrRows.Length == 0) return;

			DateTime TimeStr = DateTime.MinValue;
			// duyệt qua từng ngày để xóa giờ, trong quá trình xóa nếu xảy ra lỗi thì thoát ra.
			for (int i = 0; i < arrRows.Length; i++) {
				DataRow row = arrRows[i];
				cChk check = row["objCheck"] as cChk;
				if (check == null) continue;

				iUserEnrollNumber = (int)row["UserEnrollNumber"];
				TimeStr = check.Time;
				bool kq = DAL.XoaGioChoNV(iUserEnrollNumber, check.Time, check.Source, check.MachineNo, ThamSo.currUserID, lydo, ghichu);
				if (kq == false) {
					name = row["UserFullName"].ToString();
					flag = false;
					break;
				}
			}
			// nếu xảy ra lỗi thì báo, ko thì thông báo thành công
			if (flag == false) {
				MessageBox.Show("Xảy ra lỗi trong quá trình xoá giờ kể từ Nhân viên: " + name + " vào giờ " + TimeStr.ToString(), "Lỗi");
			}
			else {
				AutoClosingMessageBox.Show("Thực hiện thành công.", "Thông báo", 2000);
				this.Close();
			}
		}


		private void btnThoat_Click(object sender, EventArgs e) {
			this.Close();
		}

	}
}
