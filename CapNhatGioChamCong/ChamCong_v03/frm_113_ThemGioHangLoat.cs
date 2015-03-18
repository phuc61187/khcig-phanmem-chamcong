using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;

namespace ChamCong_v03 {
	public partial class frm_113_ThemGioHangLoat : Form {
		public bool IsReload;
		public DataRow[] selectedRow; // row đang chọn từ form xem công lưu giữ thông tin ngày công của nhân viên đang chọn
		public List<cCaAbs> dsCa; // danh sách các Ca thuộc lịch trình của nhân viên, hàm load sẽ thêm item "--" ở vị trí 0

		public frm_113_ThemGioHangLoat() {
			InitializeComponent();

			// gán trước các datamember, value member cho các comboBox
			cbCa_Them.ValueMember = "ID";
			cbCa_Them.DisplayMember = "Code";
		}
/*
		private void frm_ThongTinThemGio_Load(object sender, EventArgs e) {

			// 1. lấy thông tin nhân viên, fill thông tin vào 2 tb mã và tên nv

			//tạo dsCa gốc cho toàn form, combobox nào muốn sử dụng lại thì tạo mới từ cái gốc này và thay đổi tùy ý để giữ cái này làm gốc ko bị thay đổi
			dsCa = (from row in selectedRow
					let dsca1 = ((cUserInfo)row["cUserInfo"]).LichTrinhLV.DSCaMoRong
					from ca in dsca1
					select ca).Distinct().ToList();

			dsCa.Insert(0, new cCaChuan { ShiftID = -1, ShiftCode = "--" });
			cbCa_Them.DataSource = dsCa;
			// 
			// .triển khai load datagrid, trong khi load datagrid cũng đồng thời triển khai load dataSource cho các combobox
		}

		private void cbCa_Them_SelectionChangeCommitted(object sender, EventArgs e) {
			//nếu dataSource null hoặc là index đầu tiên hoặc chưa có dòng nào được chọn thì ko làm gì cả
			if (cbCa_Them.DataSource == null || cbCa_Them.SelectedIndex == 0) {
				return;
			}

			// tồn tại dataSource , xem item nào đang được chọn, nếu item đầu thì return như ở trên, các item khác thì fill
			var item = cbCa_Them.SelectedItem as cCaAbs;
			dtpVao_Them.ValueChanged -= dtpVao_Them_ValueChanged;
			dtpRaa_Them.ValueChanged -= dtpRaa_Them_ValueChanged;
			dtpVao_Them.Value = DateTime.Now.Date.Add(item.OnnDutyTS);
			dtpRaa_Them.Value = DateTime.Now.Date.Add(item.OffDutyTS);
			dtpVao_Them.ValueChanged += dtpVao_Them_ValueChanged;
			dtpRaa_Them.ValueChanged += dtpRaa_Them_ValueChanged;

		}

		private void dtpVao_Them_ValueChanged(object sender, EventArgs e) {
			// nếu ko có dataSource hay chọn item đầu thì thoát
			if (cbCa_Them.DataSource == null || cbCa_Them.SelectedIndex == 0) return;

			var ca = cbCa_Them.SelectedItem as cCaAbs;
			var hieuvaoca = DateTime.Now.Date.Add(ca.OnTimeInTS);
			var hieuraaca = DateTime.Now.Date.Add(ca.CutInTS);
			var time = dtpVao_Them.Value;

			if (time < hieuvaoca || time > hieuraaca) {
				cbCa_Them.SelectionChangeCommitted -= cbCa_Them_SelectionChangeCommitted;
				cbCa_Them.SelectedIndex = 0;
				cbCa_Them.SelectionChangeCommitted += cbCa_Them_SelectionChangeCommitted;
			}

		}

		private void dtpRaa_Them_ValueChanged(object sender, EventArgs e) {
			// nếu ko có dataSource hay chọn item đầu thì thoát
			if (cbCa_Them.DataSource == null || cbCa_Them.SelectedIndex == 0) return;

			var ca = cbCa_Them.SelectedItem as cCaAbs;
			var hieuvaoca = DateTime.Now.Date.Add(ca.OnTimeInTS);
			var hieuraaca = DateTime.Now.Date.Add(ca.CutInTS);
			var time = dtpRaa_Them.Value;

			if (time < hieuvaoca || time > hieuraaca) {
				cbCa_Them.SelectionChangeCommitted -= cbCa_Them_SelectionChangeCommitted;
				cbCa_Them.SelectedIndex = 0;
				cbCa_Them.SelectionChangeCommitted += cbCa_Them_SelectionChangeCommitted;
			}

		}

		private void btnThem_Click(object sender, EventArgs e) {
			IsReload = true;
			// lấy thông tin
			var giovao_temp = dtpVao_Them.Value.Add(ThamSo._01giay);
			var gioraa_temp = dtpRaa_Them.Value;
			var lydo = cbLyDo_Them.SelectedText;
			var ghichu = tbGhichu_Them.Text;
			var timespanvao = giovao_temp - DateTime.Now.Date; // vì datetimepicker đã gán ngày cố định là ngày hiện tại và ko hiển thị ngày để sửa
			var timespanraa = gioraa_temp - DateTime.Now.Date;
			// group dsnv
			var dsnv = (from row in selectedRow select (cUserInfo)row["cUserInfo"]).Distinct().ToList();


			foreach (var row in selectedRow) {
				var nhanvien_goc = (cUserInfo)row["cUserInfo"];
				var ngayCong = (DateTime)row["TimeStrNgay"];
				var cNgayCong = (cNgayCong) row["cNgayCong"];
				var ngayBD = nhanvien_goc.DSNgayCong[0].Ngay.Date;
				var ngayKT = nhanvien_goc.DSNgayCong[nhanvien_goc.DSNgayCong.Count - 1].Ngay.Date;
				if (chkGioVao.Checked) {
					// kiểm tra trước khi thêm giờ
					var kq = false;
					var kq1 = 0;
					var gioAnhHuong = new List<cChkInOut>();
					var chuoi = string.Empty;
					var giovao = ngayCong.Add(timespanvao);

					XL.Kiemtra_GioRaaNhoHonGioVao(giovao, cNgayCong.DSVaoRa, ref kq1, ref gioAnhHuong, ref chuoi); //tbd NgayCong_goc hay lấy từ row đang chọn?
					if (kq1 == 1) {
						var giovaoTruoc1Ngay = giovao - ThamSo._1ngay;
						var tempstring = "Bạn muốn thêm giờ vào cho " + chuoi
										 + "?\nBấm Yes nếu đồng ý thêm giờ vào " + giovaoTruoc1Ngay.ToString("H:mm dddd d/M") 
										 + ".\nBấm No nếu bạn vẫn muốn thêm giờ vào " + giovao.ToString("H:mm dddd d/M") 
										 + ".\nBấm Cancel để hủy bỏ và xem lại thời gian nhập cho ngày này.";
						switch (MessageBox.Show(tempstring, "Thông báo", MessageBoxButtons.YesNoCancel)) {
							case DialogResult.Yes:
								giovao = giovaoTruoc1Ngay;
								break;
							case DialogResult.Cancel:
								goto abc;
						}

					}
					else if (kq1 == 2)
					{
						var giovaoSau1Ngay = giovao + ThamSo._1ngay;
						var tempstring = "Bạn muốn thêm giờ vào cho " + chuoi
										 + "?\nBấm Yes nếu đồng ý thêm giờ vào " + giovaoSau1Ngay.ToString("H:mm dddd d/M") 
										 + ".\nBấm No nếu bạn vẫn muốn thêm giờ vào " + giovao.ToString("H:mm dddd d/M") 
										 + ".\nBấm Cancel để hủy bỏ và xem lại thời gian nhập cho ngày này.";
						switch (MessageBox.Show(tempstring, "Thông báo", MessageBoxButtons.YesNoCancel)) {
							case DialogResult.Yes:
								giovao = giovaoSau1Ngay;
								break;
							case DialogResult.Cancel:
								goto abc;
						}
						
					}

					gioAnhHuong.Clear();
					chuoi = string.Empty;
					KiemTra_TruocKhiThemGio(giovao, true, nhanvien_goc.DSVaoRa, ref kq, ref gioAnhHuong, ref chuoi);
					if (kq) {
						MessageBox.Show("Không thể thêm giờ vào " + giovao.ToString("H:mm d/M") + ".\nVì giờ cần thêm gần hoặc giữa cặp giờ" + chuoi, "Thông báo");
						goto abc;
					}

					if (DAL.ThemGioChoNV(nhanvien_goc.UserEnrollNumber, giovao, "I", 21, ThamSo.currUserID, lydo, ghichu)) {
						var checkinn = new cChkInn_A() { IsEdited = 1, Type = "I", MachineNo = 21, Source = "PC", UserEnrollNumber = nhanvien_goc.UserEnrollNumber, TimeStr = giovao, PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false } };
						nhanvien_goc.DS_Check_A.Add(checkinn);
					}
				}
			abc:
				if (chkGioRaa.Checked) {
					// kiểm tra trước khi thêm giờ
					var kq = false;
					var gioAnhHuong = new List<cChkInOut>();
					var loai = string.Empty;
					var gioraa1 = ngayCong.Add(timespanraa);
					KiemTra_TruocKhiThemGio(gioraa1, false, nhanvien_goc.DSVaoRa, ref kq, ref gioAnhHuong, ref loai);
					if (kq) {
						MessageBox.Show("Không thể thêm giờ ra " + gioraa1.ToString("H:mm d/M") + ".\nVì giờ cần thêm gần hoặc giữa cặp giờ" + loai, "Thông báo");
						return;
					}
					if (DAL.ThemGioChoNV(nhanvien_goc.UserEnrollNumber, gioraa1, "O", 22, ThamSo.currUserID, lydo, ghichu)) {
						var checkout = new cChkOut_A() { IsEdited = 1, Type = "O", MachineNo = 22, Source = "PC", UserEnrollNumber = nhanvien_goc.UserEnrollNumber, TimeStr = gioraa1, PhucHoi = new cPhucHoi { Them = true, IDGioGoc = -1, Xoaa = false } };
						nhanvien_goc.DS_Check_A.Add(checkout);
					}
				}
				nhanvien_goc.DS_Check_A.Sort(new cChkComparer());
				XL.LoaiBoCheckCungLoaiTrong30phut(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_Check_KoHopLe);
				XL.GhepCIO_A(nhanvien_goc.DS_Check_A, nhanvien_goc.DS_CIO_A);
				XL.LoaiBoCIOKoHopLe(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_Check_A, nhanvien_goc.DS_Check_KoHopLe);
				XL.XetCa_CIO_A(nhanvien_goc.DS_CIO_A, nhanvien_goc.LichTrinhLV.DSCa, nhanvien_goc.MacDinhTinhPC150);
				XL.TronDS_CIO_A_V(nhanvien_goc.DS_CIO_A, nhanvien_goc.DS_CIO_V, nhanvien_goc.DSVaoRa);
				XL.TinhCongTheoNgay(nhanvien_goc.DSVaoRa, ngayBD, ngayKT, nhanvien_goc.DSVang, nhanvien_goc.DSNgayCong);
			}

			this.Close();
		}

		private void KiemTra_TruocKhiThemGio(DateTime gio, bool kieugio, List<cChkInOut> dsVaoRaa, ref bool kq, ref List<cChkInOut> gioAnhHuong, ref string chuoi) {
			if (kieugio) // kiểu giờ vào
			{
				var vao2 = gio;
				gioAnhHuong = (from CIO in dsVaoRaa
							   where ((CIO.HaveINOUT == -1 || CIO.HaveINOUT == 0) && (vao2 < CIO.Vao.TimeStr) && (CIO.Vao.TimeStr - vao2 < ThamSo._30phut))
								   || ((CIO.HaveINOUT == -1) && (CIO.Vao.TimeStr < vao2) && (vao2 - CIO.Vao.TimeStr < ThamSo._30phut))
								   || (CIO.HaveINOUT == 0 && CIO.Vao.TimeStr < vao2 && vao2 < CIO.Raa.TimeStr) // I2 nam giua IO
								   || ((CIO.HaveINOUT == -2) && (vao2 < CIO.Raa.TimeStr) && (CIO.Raa.TimeStr - vao2 < ThamSo._30phut)) // IO30
							   select CIO).ToList();
			}
			else {
				var raa2 = gio;
				gioAnhHuong = (from CIO in dsVaoRaa
							   where ((CIO.HaveINOUT == -2 || CIO.HaveINOUT == 0) && (CIO.Raa.TimeStr < raa2) && (raa2 - CIO.Raa.TimeStr < ThamSo._30phut))
								   || ((CIO.HaveINOUT == -2) && (raa2 < CIO.Raa.TimeStr) && (CIO.Raa.TimeStr - raa2 < ThamSo._30phut))
								   || (CIO.HaveINOUT == 0 && CIO.Vao.TimeStr < raa2 && raa2 < CIO.Raa.TimeStr) // O2 nam giua IO
								   || ((CIO.HaveINOUT == -1) && (CIO.Vao.TimeStr < raa2) && (raa2 - CIO.Vao.TimeStr < ThamSo._30phut)) // IO30
							   select CIO).ToList();
			}
			if (gioAnhHuong.Count > 0) {
				kq = true;
				var tempCIO = gioAnhHuong[0];
				chuoi += (tempCIO.Vao != null) ? " Vào " + tempCIO.Vao.TimeStr.ToString("H:mm d/M") : "";
				chuoi += (tempCIO.Raa != null) ? " Ra " + tempCIO.Raa.TimeStr.ToString("H:mm d/M") : "";
			}

		}

		private void button1_Click(object sender, EventArgs e) {
			this.Close();
		}*/



	}
}
