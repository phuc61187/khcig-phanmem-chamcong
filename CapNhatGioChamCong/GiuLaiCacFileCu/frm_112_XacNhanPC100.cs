using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using log4net;

namespace GiuLaiCacFileCu {
	public partial class frm_112_XacNhanPC100 : Form {
		public frm_112_XacNhanPC100() {
		}

/*
		private readonly ILog lg = LogManager.GetLogger("frm_112_XacNhanTangCa");

		public bool IsReload;
		public List<cUserInfo> m_DSNV; // từ form xem công chuyển qua
		public DataRow[] m_arrRecd; // từ form xem công chuyển qua
		//public int mode;// từ form chuyển qua, nếu check all thì duye
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
			kq.Columns.Add("PhuCap", typeof(Double)); //20
			kq.Columns.Add("TongGioLam", typeof(Double));
			kq.Columns.Add("TongGioThuc", typeof(Double));
			kq.Columns.Add("cUserInfo", typeof(cUserInfo));
			kq.Columns.Add("cNgayCong", typeof(cNgayCong));
			kq.Columns.Add("cChkInOut", typeof(cChkInOut));
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

		public frm_112_XacNhanPC100() {
			log4net.Config.XmlConfigurator.Configure();
			InitializeComponent();
			IsReload = false;
			dgrdGioCoLamThem.AutoGenerateColumns = false;
			m_tableDS_CIO = TaoCauTrucTable_DS_CIO();
			dgrdGioCoLamThem.DataSource = m_tableDS_CIO;

			dgrdGioCoLamThem.SelectionChanged += dgrdGioCoLamThem_SelectionChanged;
			cbXNChonCa.SelectionChangeCommitted += cbChonCa_SelectionChangeCommitted;

		}

		private void frmXacNhanTangCa_Load(object sender, EventArgs e) {
			loadGrid();
		}

		private void loadGrid() {
			m_tableDS_CIO.Clear();
			XL.TaoTableXacNhanPC100(m_arrRecd, m_tableDS_CIO);
			VeLaiCacGioCoThayDoi();

		}


		private void dgrdGioCoLamThem_SelectionChanged(object sender, EventArgs e) {

			if (dgrdGioCoLamThem.SelectedRows.Count == 0) {
				#region reset layout
				MyUtility.ClearControlText(new Control[]{tbTenNV,tbGioVao,tbGioRaa,tbThuocCa,tbGioLam,tbTongGio,tbTreSom,tbOLaiThem_ThongTin,tbLamThem,
					tbXNGioLam,tbXNTreSom,tbXN_OLaiThem});
				checkBox1.Checked = false;

				cbXNChonCa.DataSource = null;
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

				#region fill thông tin CIO
				var TimeStrVao = CIO.Vao.Time;
				var TimeStrRaa = CIO.Raa.Time;
				var ngaydangchon = CIO.ThuocNgayCong;
				tbTenNV.Text = nhanvien.TenNV;
				tbGioVao.Text = TimeStrVao.ToString("H:mm d/M");
				tbGioRaa.Text = TimeStrRaa.ToString("H:mm d/M");
				tbThuocCa.Text = CIO.ThuocCa.Code;
				tbGioLam.Text = CIO.TG.LamTinhCong.TotalHours.ToString("#0.0#");
				tbTongGio.Text = CIO.TG.GioThuc.TotalHours.ToString("#0.0#");
				tbTreSom.Text = (CIO.TG.VaoTre + CIO.TG.RaaSom).TotalMinutes.ToString("###0");
				tbOLaiThem_ThongTin.Text = (CIO.GetType() == typeof(cChkInOut_A)) ? ((cChkInOut_A)CIO).OLaiThem.TotalMinutes.ToString("###0") : "0";
				tbLamThem.Text = (CIO.GetType() == typeof(cChkInOut_V)) ? ((cChkInOut_V)CIO).LamThem.TotalMinutes.ToString("###0") : "0";
				//checkBox1.Checked = CIO.TinhPC150;//[140615_2]
				#endregion

				// xét xem nếu chọn đang chọn nhiều row là chế độ hàng loạt

				// 4. load ds chọn ca, tạo thêm 2 ca tự do và ca dài
				var dsCaChon = new List<cCaAbs>(nhanvien.LichTrinhLV.DSCaMoRong);
				var caKDQD = new cCaTuDo { ID = int.MinValue, Code = "Ca 8h" };
				var CaDaiA = new cCaTuDo { ID = int.MinValue + 1, Code = "CaDài 12h" };
				XL.TaoCaTuDo(caKDQD, TimeStrVao, ThamSo._08gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1f);
				XL.TaoCaTuDo(CaDaiA, TimeStrVao, ThamSo._12gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1.5f);

				// loại bỏ những ca chắc chắn ko xảy ra: ra ca < check vào 30ph. check ra < vào ca 30ph
				dsCaChon.RemoveAll(item => (
					CIO.TG.GioThuc.TotalHours < (item.WorkingTimeTS.TotalHours / 3d)
					|| TimeStrVao.TimeOfDay < ThamSo._04h30 // xem [140515_3] 
					|| TimeStrRaa < ngaydangchon.Add(item.OnnTS).Add(ThamSo._30phut)
					|| TimeStrVao > ngaydangchon.Add(item.OffTS).Subtract(ThamSo._30phut)));
				// sau khi loại bỏ mới thêm 2 ca tự do vào
				dsCaChon.Insert(0, caKDQD);
				dsCaChon.Insert(1, CaDaiA);

				cbXNChonCa.ValueMember = "ID";
				cbXNChonCa.DisplayMember = "Code";
				cbXNChonCa.DataSource = dsCaChon;
				cbXNChonCa.Update();
			}
			else { // chế độ xác nhận hàng loạt
				btnXacNhan.Enabled = true;

				var dataRowView = dgrdGioCoLamThem.SelectedRows[0].DataBoundItem as DataRowView;
				var nhanvien = dataRowView["cUserInfo"] as cUserInfo;
				var CIO = dataRowView["cChkInOut"] as cChkInOut;

				#region fill thông tin CIO
				var TimeStrVao = CIO.Vao.Time;
				var TimeStrRaa = CIO.Raa.Time;
				var ngaydangchon = CIO.ThuocNgayCong;
				tbTenNV.Text = nhanvien.TenNV;
				tbGioVao.Text = TimeStrVao.ToString("H:mm d/M");
				tbGioRaa.Text = TimeStrRaa.ToString("H:mm d/M");
				tbThuocCa.Text = CIO.ThuocCa.Code;
				tbGioLam.Text = CIO.TG.LamTinhCong.TotalHours.ToString("#0.0#");
				tbTongGio.Text = CIO.TG.GioThuc.TotalHours.ToString("#0.0#");
				tbTreSom.Text = (CIO.TG.VaoTre + CIO.TG.RaaSom).TotalMinutes.ToString("###0");
				tbOLaiThem_ThongTin.Text = (CIO.GetType() == typeof(cChkInOut_A)) ? ((cChkInOut_A)CIO).OLaiThem.TotalMinutes.ToString("###0") : "0";
				tbLamThem.Text = (CIO.GetType() == typeof(cChkInOut_V)) ? ((cChkInOut_V)CIO).LamThem.TotalMinutes.ToString("###0") : "0";
				//checkBox1.Checked = CIO.TinhPC150;//[140615_2]
				#endregion


				tbXNGioLam.Text = string.Empty;
				tbXNTreSom.Text = string.Empty;
				tbXN_OLaiThem.Text = string.Empty;

				numPhutTinhLamThem.Maximum = 840;

				IEnumerable<DataGridViewRow> dataGridViewRows = dgrdGioCoLamThem.SelectedRows.Cast<DataGridViewRow>();
				var dataRows = from x in dataGridViewRows select ((DataRowView)x.DataBoundItem).Row;
				var dsCa = ((from row in dataRows
							 from ca in ((cUserInfo)row["cUserInfo"]).LichTrinhLV.DSCa
							 select ca)).Distinct().ToList();
				var caKDQD = new cCaTuDo { ID = int.MinValue, Code = "Ca 8h" };
				var CaDaiA = new cCaTuDo { ID = int.MinValue + 1, Code = "Ca Dài 12 tiếng" };
				dsCa.Insert(0, caKDQD);
				dsCa.Insert(1, CaDaiA);

				cbXNChonCa.ValueMember = "ID";
				cbXNChonCa.DisplayMember = "Code";
				cbXNChonCa.DataSource = dsCa;
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

				if (cbXNChonCa.DataSource == null || cbXNChonCa.Items.Count == 0) {
					#region reset layout
					MyUtility.ClearControlText(new Control[] { tbTenNV, tbGioVao, tbGioRaa, tbThuocCa, tbGioLam, tbTongGio, tbTreSom, tbLamThem, tbXNGioLam, tbXNTreSom, tbXN_OLaiThem });

					cbXNChonCa.DataSource = null;
					checkXacNhanLamThem.Checked = false;
					checkXacNhanLamThem.Enabled = false;
					//checkBox1.Checked = CIO.TinhPC150;//[140615_2]
					numPhutTinhLamThem.Value = 0;
					numPhutTinhLamThem.Enabled = false;

					#endregion

					return;
				}

				// đang chọn 1 ca  nào đó, lấy ca đang chọn và fill thông tin
				var ca = cbXNChonCa.SelectedItem as cCaAbs;
				if (ca == null) return;

				numPhutTinhLamThem.Value = 0;
				var lamthem = new TimeSpan(0, 0, (int)numPhutTinhLamThem.Value, 0);
				var ngaycong = CIO.ThuocNgayCong;

				var vaotre = XL.TinhVaoTre(CIO.Vao.Time, ngaycong.Add(ca.OnnTS), ngaycong.Add(ca.chophepTreTS));
				var raasom = XL.TinhRaaSom(CIO.Raa.Time, ngaycong.Add(ca.OffTS), ngaycong.Add(ca.chophepSomTS));
				var vaolam = XL.TinhVaoLam(CIO.Vao.Time, ngaycong.Add(ca.OnnTS), vaotre);
				var raalam = XL.TinhRaaLam(CIO.Raa.Time, ngaycong.Add(ca.OffTS), raasom, lamthem);
				tbXNTreSom.Text = Math.Round((vaotre + raasom).TotalMinutes, 2).ToString("###0");
				tbXNGioLam.Text = Math.Round((raalam - vaolam - ca.LunchMin).TotalHours, 2).ToString("###0.0#");
				var olaithem = XL.TinhOLaiThem(CIO.Raa.Time, ngaycong.Add(ca.OffTS), ngaycong.Add(ca.batdaulamthemTS));

				checkXacNhanLamThem.Checked = false;
				if (olaithem == ThamSo._0gio) {
					tbXN_OLaiThem.Text = "0";
					checkXacNhanLamThem.Enabled = false;
					numPhutTinhLamThem.Enabled = false;
					numPhutTinhLamThem.Value = 0;
				}
				else {
					tbXN_OLaiThem.Text = Math.Floor(olaithem.TotalMinutes).ToString();
					checkXacNhanLamThem.Enabled = true;
					numPhutTinhLamThem.Enabled = true;
					numPhutTinhLamThem.Value = 0;
					numPhutTinhLamThem.Maximum = Convert.ToDecimal(Math.Floor(olaithem.TotalMinutes));
				}
			}
			else {
				tbXNGioLam.Text = string.Empty;
				tbXNTreSom.Text = string.Empty;
				tbXN_OLaiThem.Text = string.Empty;
				checkXacNhanLamThem.Enabled = true;
				numPhutTinhLamThem.Enabled = true;
				numPhutTinhLamThem.Value = 0;
			}
		}

		private void btnXacNhan_Click(object sender, EventArgs e) {
			IsReload = true;
			numPhutTinhLamThem.Update();
			var soPhutLamThem = (checkXacNhanLamThem.Checked) ? Convert.ToInt32(numPhutTinhLamThem.Value) : 0;
			var loai = 0;
			var giatri = false;
			if (radPCNgayNghi.Checked) {
				loai = 2;
				giatri = radPCNgayNghi.Checked;
			}
			else if (radPCNgayLe.Checked) {
				loai = 3;
				giatri = radPCNgayLe.Checked;
			}
			else if (radPCCa3Le.Checked) {
				loai = 4;
				giatri = radPCCa3Le.Checked;
			}

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
						XL.abc(nv, CIO, loai, giatri);
						XL.XacNhanKoTachCa(nv, CIO, CaMoi, soPhutLamThem);////[140615_2]
					}
					else {
						if (DuDieuKienTach(CIO, CaMoi))
							XL.XacNhanCoTachCa(nv, CIO, CaMoi, soPhutLamThem);//, choPhepTinhPc50//[140615_2]
					}
				}
				else {
					if (CaMoi.TachCa == false) {
						XL.XacNhanLaiKoTachCa(nv, ref CIO, CaMoi, soPhutLamThem);//, choPhepTinhPc50 //[140615_2]
					}
					else {
						throw new NotImplementedException();
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
							XL.TaoCaTuDo((cCaTuDo)CaMoi, item.CIO.Vao.Time, ThamSo._08gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1f);
						if (CaMoi is cCaTuDo && CaMoi.ID == int.MinValue + 1)
							XL.TaoCaTuDo((cCaTuDo)CaMoi, item.CIO.Vao.Time, ThamSo._12gio, ThamSo._05phut, ThamSo._10phut, ThamSo._30phut, 1.5f);
						var olaithem = XL.TinhOLaiThem(item.CIO.Raa.Time, item.CIO.ThuocNgayCong.Add(CaMoi.OffTS), item.CIO.ThuocNgayCong.Add(CaMoi.batdaulamthemTS));
						#region tính số phút làm thêm
						if ((olaithem.TotalMinutes < soPhutLamThem && soPhutLamThem > 0)) continue; //tbd
						#endregion
						if (item.CIO is cChkInOut_A) {
							if (CaMoi.TachCa == false) {
								XL.abc(nhanvien_goc, item.CIO, loai, giatri);
								XL.XacNhanKoTachCa(nhanvien_goc, item.CIO, CaMoi, soPhutLamThem);//, choPhepTinhPc50//[140615_2]
							}
							else {
								if (DuDieuKienTach(item.CIO, CaMoi))
									XL.XacNhanCoTachCa(nhanvien_goc, item.CIO, CaMoi, soPhutLamThem);//, choPhepTinhPc50//[140615_2]
							}
						}
						else {// giờ đã xác nhận --> update lại
							var a = item.CIO;
							if (CaMoi.TachCa == false) {
								XL.XacNhanLaiKoTachCa(nhanvien_goc, ref a, CaMoi, soPhutLamThem);//, choPhepTinhPc50//[140615_2]
							}
							else {
								throw new NotImplementedException();
							}
						}

					}
				}

			}

			loadGrid();
			
		}

		private bool DuDieuKienTach(cChkInOut CIO, cCaAbs caMoi) {
			var ca3 = ((cCaChuan)caMoi).catruoc;
			var ca1 = ((cCaChuan)caMoi).casauuu;
			var ngay = CIO.ThuocNgayCong;

			if (ngay.Add(ca3.OffTS).Subtract(ThamSo._30phut) < CIO.Vao.Time
				|| CIO.Raa.Time < ngay.Add(ca1.OnnTS).Add(ThamSo._30phut)) {
				return false;
			}
			return true;
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void checkTinhPC150_MouseHover(object sender, EventArgs e) {
			//toolTip1.Show("Check mục này để tính phụ cấp 50% lương trường hợp ngày làm trên 8 tiếng.", checkTinhPC150, 5000);//[140615_2]
		}

		private void numPhutTinhLamThem_ValueChanged(object sender, EventArgs e) {
			if (numPhutTinhLamThem.Value > 0) checkXacNhanLamThem.Checked = true;
			else checkXacNhanLamThem.Checked = false;

		}
*/






	}
}
