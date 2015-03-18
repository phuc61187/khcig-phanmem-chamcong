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
	public partial class frm_112_XacNhanTangCa : Form {
		private readonly ILog lg = LogManager.GetLogger("frm_112_XacNhanTangCa");

		public List<cUserInfo> m_DSNV; // từ form xem công chuyển qua
		public bool IsReload;
		public DataRow[] m_arrRecd; // từ form xem công chuyển qua
		public DataTable m_tableDS_CIO;
		public DataTable TaoCauTrucTable_DS_CIO() {
			var kq = new DataTable();
			kq.Columns.Add("UserEnrollNumber", typeof(int)); //0
			kq.Columns.Add("UserFullCode", typeof(string)); //1
			kq.Columns.Add("UserFullName", typeof(string)); //1
			kq.Columns.Add("TimeStrNgay", typeof(DateTime)); //2
			kq.Columns.Add("TimeStrVao", typeof(DateTime)); //4
			kq.Columns.Add("TimeStrRaa", typeof(DateTime)); //5
			kq.Columns.Add("ShiftCode", typeof(string)); //8
			kq.Columns.Add("ShiftID", typeof(int)); //9
			kq.Columns.Add("Cong", typeof(Double)); //20
			kq.Columns.Add("TongGioLam", typeof(Double));
			kq.Columns.Add("TongGioThuc", typeof(Double));
			kq.Columns.Add("cUserInfo", typeof(cUserInfo));
			kq.Columns.Add("cNgayCong", typeof(cNgayCong));
			kq.Columns.Add("cChkInOut", typeof(cChkInOut));
			kq.Columns.Add("tieuchi1", typeof(bool));
			kq.Columns.Add("tieuchi2", typeof(bool));
			kq.Columns.Add("tieuchi3", typeof(bool));
			kq.Columns.Add("IsEdited", typeof(bool));

			return kq;
		}
		private void VeLaiCacGioCoThayDoi() {
			foreach (DataGridViewRow dataGridViewRow in dgrdGioCoLamThem.Rows) {
				var row = (dataGridViewRow.DataBoundItem as DataRowView).Row;
				if (row["IsEdited"] != DBNull.Value && (bool)row["IsEdited"]) {
					dataGridViewRow.DefaultCellStyle.BackColor = Color.LightGreen;
				}
			}
			GC.Collect();
		}

		public frm_112_XacNhanTangCa() {
			log4net.Config.XmlConfigurator.Configure();
			InitializeComponent();

			dgrdGioCoLamThem.AutoGenerateColumns = false;
			m_tableDS_CIO = TaoCauTrucTable_DS_CIO();
			dgrdGioCoLamThem.DataSource = m_tableDS_CIO;

			cbXNChonCa.ValueMember = "ID";
			cbXNChonCa.DisplayMember = "Code";

			dgrdGioCoLamThem.SelectionChanged += dgrdGioCoLamThem_SelectionChanged;
			cbXNChonCa.SelectionChangeCommitted += cbChonCa_SelectionChangeCommitted;

		}

		private void frmXacNhanTangCa_Load(object sender, EventArgs e) {
			IsReload = false;

			XL.TaoTableXacNhanTangCa(m_DSNV, m_tableDS_CIO);
			cbTieuChi.SelectedIndex = 0;
			cbTieuChi_SelectionChangeCommitted(cbTieuChi, new EventArgs());
		}


		private void LoadGrid_TheoTieuChi(int tieuchi) {

			if (tieuchi == 0) {
				var view = new DataView(m_tableDS_CIO, "", "", DataViewRowState.CurrentRows);
				dgrdGioCoLamThem.DataSource = view;
			}
			else {
				var tieuchiSort = "tieuchi" + tieuchi + "= 1";
				var view = new DataView(m_tableDS_CIO, tieuchiSort, "", DataViewRowState.CurrentRows);
				dgrdGioCoLamThem.DataSource = view;
			}
			VeLaiCacGioCoThayDoi();

		}

		private void dgrdGioCoLamThem_SelectionChanged(object sender, EventArgs e) {

			if (dgrdGioCoLamThem.SelectedRows.Count == 0) {
				#region reset layout

				tbTenNV.Text = string.Empty;
				tbGioVao.Text = string.Empty;
				tbGioRaa.Text = string.Empty;
				tbThuocCa.Text = string.Empty;
				tbGioLam.Text = string.Empty;
				tbTongGio.Text = string.Empty;
				tbTreSom.Text = string.Empty;
				tbOLaiThem_ThongTin.Text = string.Empty;
				tbLamThem.Text = string.Empty;
				checkThongTinChoPhepTinhPC50.Checked = false;
				checkThongTinTreSomTinhCV.Checked = false;

				cbXNChonCa.DataSource=null;
				cbXNChonCa.ValueMember="ID";// do mỗi lần set null dataSource thì value member và code sẽ bị xóa
				cbXNChonCa.DisplayMember="Code";
				tbXNGioLam.Text = string.Empty;
				tbXNTreSom.Text = string.Empty;
				tbXN_OLaiThem.Text = string.Empty;
				checkXacNhanLamThem.Checked = false;
				checkXacNhanLamThem.Enabled = false;
				numPhutTinhLamThem.Value = 0;
				numPhutTinhLamThem.Enabled = false;
				#endregion
				// ngoài reset layout thì disable nút xác nhận để tránh ấn nhầm gây lỗi
				btnXacNhan.Enabled = false;
				return;
			}
			else if (dgrdGioCoLamThem.SelectedRows.Count == 1) {
				btnXacNhan.Enabled = true;
				var dataRowView = dgrdGioCoLamThem.SelectedRows[0].DataBoundItem as DataRowView;
				var nhanvien = dataRowView["cUserInfo"] as cUserInfo;
				var CIO = dataRowView["cChkInOut"] as cChkInOut;
				var ngaycong = dataRowView["cNgayCong"] as cNgayCong;

				#region fill thông tin CIO
				DateTime TimeStrVao = CIO.Vao.Time;
				DateTime TimeStrRaa = CIO.Raa.Time;
				DateTime ngaydangchon = CIO.ThuocNgayCong;
				tbTenNV.Text = nhanvien.TenNV;
				tbGioVao.Text = TimeStrVao.ToString("H:mm d/M");
				tbGioRaa.Text = TimeStrRaa.ToString("H:mm d/M");
				tbThuocCa.Text = CIO.ThuocCa.Code;
				tbGioLam.Text = CIO.TG.GioLamTrongNgay.TotalHours.ToString("#0.0#");
				tbTongGio.Text = CIO.TG.GioThuc.TotalHours.ToString("#0.0#");
				tbTreSom.Text = (CIO.TG.VaoTre + CIO.TG.RaaSom).TotalMinutes.ToString("###0");
				tbOLaiThem_ThongTin.Text = (CIO.GetType() == typeof(cChkInOut_A)) ? CIO.TG.OLai.TotalMinutes.ToString("###0") : "0";
				tbLamThem.Text = (CIO.GetType() == typeof(cChkInOut_V)) ? CIO.TG.OTCa.TotalMinutes.ToString("###0") : "0";
				checkThongTinChoPhepTinhPC50.Checked = ngaycong.TinhPC50;
				checkThongTinTreSomTinhCV.Checked = CIO.TreSomTinhCV;//tbd
			    //checkBox1.Checked = CIO.TreSomTinhCV;
				#endregion

				// xét xem nếu chọn đang chọn nhiều row là chế độ hàng loạt

				// 4. load ds chọn ca, tạo thêm 2 ca tự do và ca dài
				var dsCaChon = new List<cCaAbs>(nhanvien.LichTrinhLV.DSCaMoRong);
				var caKDQD = new cCaTuDo { ID = int.MinValue, Code = "Ca8h" };
				var CaDaiA = new cCaTuDo { ID = int.MinValue + 1, Code = "CaDài 12h" };
				XL.TaoCaTuDo(caKDQD, TimeStrVao, XL2._08gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, "8");
				XL.TaoCaTuDo(CaDaiA, TimeStrVao, XL2._12gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1.5f, "D");

				// loại bỏ những ca chắc chắn ko xảy ra: ra ca < check vào 30ph. check ra < vào ca 30ph
				dsCaChon.RemoveAll(item => (
					CIO.TG.GioThuc.TotalHours < (item.WorkingTimeTS.TotalHours / 3d)
					|| TimeStrVao.TimeOfDay < XL2._04h30 // xem [140515_3] 
					|| TimeStrRaa < ngaydangchon.Add(item.OnnTS).Add(XL2._30phut)
					|| TimeStrVao > ngaydangchon.Add(item.OffTS).Subtract(XL2._30phut)));
				// sau khi loại bỏ mới thêm 2 ca tự do vào
				dsCaChon.Insert(0, caKDQD);
				dsCaChon.Insert(1, CaDaiA);

				cbXNChonCa.DataSource = dsCaChon;
				cbXNChonCa.Update();
				cbXNChonCa.SelectedItem = (from a in dsCaChon where a.ID == CIO.ThuocCa.ID select a).FirstOrDefault();
				cbChonCa_SelectionChangeCommitted(cbXNChonCa, new EventArgs());

			}
			else { // chế độ xác nhận hàng loạt
				btnXacNhan.Enabled = true;

				var dataRowView = dgrdGioCoLamThem.SelectedRows[0].DataBoundItem as DataRowView;
				var nhanvien = dataRowView["cUserInfo"] as cUserInfo;
				var CIO = dataRowView["cChkInOut"] as cChkInOut;
				var ngaycong = dataRowView["cNgayCong"] as cNgayCong;

				#region fill thông tin CIO
				DateTime TimeStrVao = CIO.Vao.Time;
				DateTime TimeStrRaa = CIO.Raa.Time;
				DateTime ngaydangchon = CIO.ThuocNgayCong;
				tbTenNV.Text = nhanvien.TenNV;
				tbGioVao.Text = TimeStrVao.ToString("H:mm d/M");
				tbGioRaa.Text = TimeStrRaa.ToString("H:mm d/M");
				tbThuocCa.Text = CIO.ThuocCa.Code;
				tbGioLam.Text = CIO.TG.GioLamTrongNgay.TotalHours.ToString("#0.0#");
				tbTongGio.Text = CIO.TG.GioThuc.TotalHours.ToString("#0.0#");
				tbTreSom.Text = (CIO.TG.VaoTre + CIO.TG.RaaSom).TotalMinutes.ToString("###0");
				tbOLaiThem_ThongTin.Text = (CIO.GetType() == typeof(cChkInOut_A)) ? CIO.TG.OLai.TotalMinutes.ToString("###0") : "0";
				tbLamThem.Text = (CIO.GetType() == typeof(cChkInOut_V)) ? CIO.TG.OTCa.TotalMinutes.ToString("###0") : "0";
				checkThongTinChoPhepTinhPC50.Checked = ngaycong.TinhPC50;
				checkThongTinTreSomTinhCV.Checked = CIO.TreSomTinhCV;
				#endregion


				tbXNGioLam.Text = string.Empty;
				tbXNTreSom.Text = string.Empty;
				tbXN_OLaiThem.Text = string.Empty;
				numPhutTinhLamThem.Value = 0;
				numPhutTinhLamThem.Maximum = 840;

				IEnumerable<DataGridViewRow> dataGridViewRows = dgrdGioCoLamThem.SelectedRows.Cast<DataGridViewRow>();
				var dataRows = from x in dataGridViewRows select ((DataRowView)x.DataBoundItem).Row;
				var dsCa = ((from row in dataRows
							 from ca in ((cUserInfo)row["cUserInfo"]).LichTrinhLV.DSCa
							 select ca)).Distinct().ToList();
				var caKDQD = new cCaTuDo { ID = int.MinValue, Code = "Ca8h" };
				var CaDaiA = new cCaTuDo { ID = int.MinValue + 1, Code = "CaDài 12h" };
				dsCa.Insert(0, caKDQD);
				dsCa.Insert(1, CaDaiA);

				cbXNChonCa.DataSource = dsCa;
				checkTinhPC50.Checked = true; // mặc định check tính pc 50% theo nhân viên phòng nào
				checkXacNhanTreSomTinhCV.Checked = false;// mặc định ko check tresomtinhCV
				cbXNChonCa.Update();


			}


		}

		private void cbChonCa_SelectionChangeCommitted(object sender, EventArgs e) {
			if (dgrdGioCoLamThem.SelectedRows.Count == 0) return;
			else if (dgrdGioCoLamThem.SelectedRows.Count == 1) {
				//1. lấy dòng đang chọn và dữ liệu được chọn. gán currView để có thể lấy được dữ liệu
				var dataRowView = dgrdGioCoLamThem.SelectedRows[0].DataBoundItem as DataRowView;

				//2. tìm user đang chọn để load lại ds ca mở rộng
				var currNV = dataRowView["cUserInfo"] as cUserInfo;
				var CIO = dataRowView["cChkInOut"] as cChkInOut;

				// đang chọn 1 ca  nào đó, lấy ca đang chọn và fill thông tin
				var ca = cbXNChonCa.SelectedItem as cCaAbs;
				if (ca == null) return;

				var ngaycong = CIO.ThuocNgayCong;
				DateTime vaolam, raalam;
				TimeSpan vaotre, raasom, olaithem;
				XL.Vao(CIO.Vao.Time, ngaycong.Add(ca.OnnTS),  ngaycong.Add(ca.chophepTreTS), out vaolam, out vaotre);
				XL.Raa(CIO.Raa.Time, ngaycong.Add(ca.OffTS), ngaycong.Add(ca.chophepSomTS), out raalam, out raasom);
				XL.OLai(CIO.Raa.Time, ngaycong.Add(ca.OffTS), ngaycong.Add(ca.batdaulamthemTS), out olaithem);

				tbXNTreSom.Text = Math.Round((vaotre + raasom).TotalMinutes, 0).ToString("###0");
				tbXNGioLam.Text = Math.Round((raalam - vaolam - ca.LunchMin).TotalHours, 2).ToString("###0.0#");

				checkXacNhanLamThem.Checked = false;
				if (olaithem < new TimeSpan(0, 1, 0)) {
					tbXN_OLaiThem.Text = "0";
					checkXacNhanLamThem.Enabled = false;
					numPhutTinhLamThem.Enabled = false;
					numPhutTinhLamThem.Value = 0;
				}
				else {
					var sophutOLaiThem = Math.Floor(olaithem.TotalMinutes);
					tbXN_OLaiThem.Text = sophutOLaiThem.ToString("###0");
					checkXacNhanLamThem.Enabled = true;
					numPhutTinhLamThem.Enabled = true;
					numPhutTinhLamThem.Maximum = Convert.ToDecimal(Math.Floor(olaithem.TotalMinutes));
					numPhutTinhLamThem.Value = Convert.ToDecimal(sophutOLaiThem);
				}
			}
			else {
				tbXNGioLam.Text = string.Empty;
				tbXNTreSom.Text = string.Empty;
				tbXN_OLaiThem.Text = string.Empty;
				checkXacNhanLamThem.Enabled = true;
				numPhutTinhLamThem.Enabled = true;
				numPhutTinhLamThem.Value = 0;
				numPhutTinhLamThem.Maximum = 840;
			}


		}

		private void btnXacNhan_Click(object sender, EventArgs e) {
			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				AutoClosingMessageBox.Show("Mất kết nối với CSDL. Vui lòng thử lại sau.", "Lỗi", 2000);
				return;
			}

			#region hỏi trước khi xác nhận

			if (MessageBox.Show("Vui lòng kiểm tra kỹ thời gian vào ra các nhân viên trước khi xác nhận để không làm sai lệch công của nhân viên. \nĐồng ý xác nhận các giờ đã chọn?", Resources.capXacNhan, MessageBoxButtons.OKCancel) == DialogResult.Cancel) {
				return;
			}

			#endregion

			IsReload = true;
			numPhutTinhLamThem.Update();
			var soPhutLamThem = (checkXacNhanLamThem.Checked) ? Convert.ToInt32(numPhutTinhLamThem.Value) : 0;
			var choPhepTinhPc50 = checkTinhPC50.Checked;
			var TreSomTinhCV = (checkXacNhanTreSomTinhCV.Checked);

			var CaMoi = cbXNChonCa.SelectedItem as cCaAbs;
			if (CaMoi == null) return;

			if (dgrdGioCoLamThem.SelectedRows.Count == 0) {
				return;
			}
			else if (dgrdGioCoLamThem.SelectedRows.Count == 1) {
				var dataRowView = dgrdGioCoLamThem.SelectedRows[0].DataBoundItem as DataRowView;
				var nv = dataRowView["cUserInfo"] as cUserInfo;
				var CIO = dataRowView["cChkInOut"] as cChkInOut;

				if (CIO is cChkInOut_A) {
					if (CaMoi.TachCa == false) {
						XL.XacNhanKoTachCa(nv, CIO, CaMoi, soPhutLamThem, choPhepTinhPc50, TreSomTinhCV);////[140615_2]
					}
					else {
						if (DuDieuKienTach(CIO, CaMoi)) {
							XL.XacNhanCoTachCa(nv, CIO, CaMoi, soPhutLamThem, choPhepTinhPc50, TreSomTinhCV);////[140615_2]
						}
					}
				}
				else {
					if (CaMoi.TachCa == false) {
						XL.XacNhanLaiKoTachCa(nv, ref CIO, CaMoi, soPhutLamThem, choPhepTinhPc50, TreSomTinhCV);////[140615_2]
					}
					else {
						MessageBox.Show("Không thể thực hiện xác nhận này! Vui lòng liên hệ bộ phận kỹ thuật để được trợ giúp.", "Thông báo", MessageBoxButtons.OK);
					}
				}
			}
			else {
				IEnumerable<DataGridViewRow> dataGridViewRows = dgrdGioCoLamThem.SelectedRows.Cast<DataGridViewRow>();
				var arrRecord = (from row in dataGridViewRows
								 let nv = (cUserInfo)(((DataRowView)row.DataBoundItem).Row["cUserInfo"])
								 let cChkInOut = (cChkInOut)(((DataRowView)row.DataBoundItem).Row["cChkInOut"])
								 let ngay = (DateTime)(((DataRowView)row.DataBoundItem).Row["TimeStrNgay"])
								 select new { nhanvien = nv, CIO = cChkInOut, Ngay = ngay }).GroupBy(o => o.nhanvien).ToList();

				foreach (var group in arrRecord) {
					var nhanvien_goc = group.Key;
					foreach (var item in group) {
						if (CaMoi is cCaTuDo && CaMoi.ID == int.MinValue)
							XL.TaoCaTuDo((cCaTuDo)CaMoi, item.CIO.Vao.Time, XL2._08gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, "8");
						if (CaMoi is cCaTuDo && CaMoi.ID == int.MinValue + 1)
							XL.TaoCaTuDo((cCaTuDo)CaMoi, item.CIO.Vao.Time, XL2._12gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1.5f, "D");

						if (item.CIO.Vao.Time > item.CIO.ThuocNgayCong.Add(CaMoi.OffTS) || item.CIO.Raa.Time < item.CIO.ThuocNgayCong.Add(CaMoi.OnnTS)) {
							AutoClosingMessageBox.Show(
								string.Format("Không thể xác nhận ca {0} cho Nhân viên {1} vì có vào sau khi ca kết thúc hoặc ra trước khi ca bắt đầu.", CaMoi.Code, nhanvien_goc.TenNV),
								"Lỗi", 3000);
							continue;
						}
						TimeSpan olaithem;
						XL.OLai(item.CIO.Raa.Time, item.CIO.ThuocNgayCong.Add(CaMoi.OffTS), item.CIO.ThuocNgayCong.Add(CaMoi.batdaulamthemTS), out olaithem);

						#region tính số phút làm thêm
						if ((olaithem.TotalMinutes < soPhutLamThem && soPhutLamThem > 0)) continue; //tbd
						#endregion
						if (item.CIO is cChkInOut_A) {
							if (CaMoi.TachCa == false) {
								XL.XacNhanKoTachCa(nhanvien_goc, item.CIO, CaMoi, soPhutLamThem, choPhepTinhPc50, TreSomTinhCV);////[140615_2]
							}
							else {
								if (DuDieuKienTach(item.CIO, CaMoi))
									XL.XacNhanCoTachCa(nhanvien_goc, item.CIO, CaMoi, soPhutLamThem, choPhepTinhPc50, TreSomTinhCV);//
							}
						}
						else {// giờ đã xác nhận --> update lại
							var CIO_V = item.CIO;
							if (CaMoi.TachCa == false) {
								XL.XacNhanLaiKoTachCa(nhanvien_goc, ref CIO_V, CaMoi, soPhutLamThem, choPhepTinhPc50, TreSomTinhCV);//
							}
							else {
								AutoClosingMessageBox.Show("Không xác nhận được ca. Vui lòng liên hệ bộ phận kỹ thuật để được trợ giúp.", "Lỗi", 2000);
							}
						}

					}
				}

			}


			// sau khi xác nhận thì reload lại lưới
			m_tableDS_CIO.Rows.Clear();
			XL.TaoTableXacNhanTangCa(m_DSNV, m_tableDS_CIO);
			var tieuchi = 1;
			switch (cbTieuChi.SelectedIndex) {
				case 0:
					tieuchi = 1;
					break;
				case 1:
					tieuchi = 0;
					break;
				case 2:
					tieuchi = 2;
					break;
				case 3:
					tieuchi = 3;
					break;
			}
			LoadGrid_TheoTieuChi(tieuchi);
		}

		private bool DuDieuKienTach(cChkInOut CIO, cCaAbs caMoi) {
			var ca3 = ((cCaChuan)caMoi).catruoc;
			var ca1 = ((cCaChuan)caMoi).casauuu;
			var ngay = CIO.ThuocNgayCong;

			if (ngay.Add(ca3.OffTS).Subtract(XL2._30phut) < CIO.Vao.Time
				|| CIO.Raa.Time < ngay.Add(ca1.OnnTS).Add(XL2._30phut)) {
				return false;
			}
			return true;
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			this.Close();
		}




		private void cbTieuChi_SelectionChangeCommitted(object sender, EventArgs e) {
			var tieuchi = 1;
			switch (cbTieuChi.SelectedIndex) {
				case 0:
					tieuchi = 1;
					break;
				case 1:
					tieuchi = 0;
					break;
				case 2:
					tieuchi = 2;
					break;
				case 3:
					tieuchi = 3;
					break;
			}
			LoadGrid_TheoTieuChi(tieuchi);
		}





	}
}
