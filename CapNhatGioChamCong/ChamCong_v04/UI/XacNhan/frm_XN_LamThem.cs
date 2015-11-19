using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;
using log4net;

namespace ChamCong_v04.UI.XacNhan {
	public partial class frm_XN_LamThem : Form {
		#region log tooltip và hàm ko quan trọng
		private readonly ILog lg = LogManager.GetLogger("frm_XN_LamThem");

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
		private void btnCancel_Click(object sender, EventArgs e) {
			this.Close();
		}


		#endregion

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
			kq.Columns.Add("Cong", typeof(float)); //20
			kq.Columns.Add("TongGioLam", typeof(TimeSpan));
			kq.Columns.Add("TongGioThuc", typeof(TimeSpan));
			kq.Columns.Add("DuyetCPVaoTre", typeof(bool));//ver 4.0.0.8
			kq.Columns.Add("DuyetCPRaSom", typeof(bool));//ver 4.0.0.8
			kq.Columns.Add("VaoTreTuDo", typeof(bool));//ver 4.0.0.8
			kq.Columns.Add("RaSomTuDo", typeof(bool));//ver 4.0.0.8
			kq.Columns.Add("ChoBuGioTre", typeof(bool));//ver 4.0.0.8
			kq.Columns.Add("ChoBuGioSom", typeof(bool));//ver 4.0.0.8
			kq.Columns.Add("ChoBuPhepTre", typeof(bool));//ver 4.0.0.8
			kq.Columns.Add("TGBuPhepTre", typeof(string));//ver 4.0.0.8
			kq.Columns.Add("ChoBuPhepSom", typeof(bool));//ver 4.0.0.8
			kq.Columns.Add("TGBuPhepSom", typeof(string));//ver 4.0.0.8
			kq.Columns.Add("cUserInfo", typeof(cUserInfo));
			kq.Columns.Add("cNgayCong", typeof(cNgayCong));
			kq.Columns.Add("cCheckInOut", typeof(cCheckInOut));

			return kq;
		}

		public frm_XN_LamThem() {
			log4net.Config.XmlConfigurator.Configure();
			InitializeComponent();

			dgrdGioCoLamThem.AutoGenerateColumns = false;
			m_tableDS_CIO = TaoCauTrucTable_DS_CIO();
			dgrdGioCoLamThem.DataSource = m_tableDS_CIO;

			dgrdGioCoLamThem.SelectionChanged += dgrdGioCoLamThem_SelectionChanged;
			cbCongPhepTre.SelectedIndex = 0;
			cbCongPhepSom.SelectedIndex = 0;
			lbTTCongBuPhepTre.Text = string.Empty;
			lbTTCongBuPhepSom.Text = string.Empty;
			lbTTCongBuPhepTre.Tag = null;
			lbTTCongBuPhepSom.Tag = null;
		}

		private void frmXacNhanTangCa_Load(object sender, EventArgs e) {
			IsReload = false;

			XL.TaoTableXacNhanLamThem(m_DSNV, m_tableDS_CIO);
		}

		private void dgrdGioCoLamThem_SelectionChanged(object sender, EventArgs e) {

			if (dgrdGioCoLamThem.SelectedRows.Count == 0) {
				#region reset layout

				MyUtility.ClearControlText(tbTTTenNV, tbTTGioVao, tbTTGioRaa,
				tbTTThuocCa, tbTTGioLam, tbTTTongGio, tbTTTreSom,
				tbTTOLaiThem, tbTTLamThem,
				lbTTCongBuPhepTre, lbTTCongBuPhepSom,//ver 4.0.0.8
				tbXNCa, tbXNGioLam,
				tbXNTre, tbXNSom, tbXN_OLaiThem, tbXNGhiChu);

				MyUtility.CheckedCheckBox(false, checkTTChoPhepTre, checkTTChoPhepSom, checkTTTinhPC50,
					checkXNChoPhepTre, checkXNChoPhepSom, checkXNLamThem, checkXNTinhPC50,
					checkXNBuGioTre, checkXNBuGioSom, checkXNBuPhepTre, checkXNBuPhepSom,//ver 4.0.0.8
					checkTTBuGioTre, checkTTBuGioSom, checkTTBuPhepTre, checkTTBuPhepSom,//ver 4.0.0.8
					checkTTVaoTreTinhCV, checkTTRaaSomTinhCV, checkXNVaoTreTinhCV, checkXNRaaSomTinhCV);//ver 4.0.0.4	

				//numPhutTinhLamThem.Value = 0;
				maskPhutTinhLamThem.Text = "00:00";//ver 4.0.0.4	
				maskPhutTinhLamThem.Tag = TimeSpan.Zero;//ver 4.0.0.4	
				cbXNLyDo.SelectedIndex = 0;
				#endregion
				MyUtility.EnableDisableControl(false,
						btnXacNhan, btnDoiCa,				// ngoài reset layout thì disable nút xác nhận, nút sửa để tránh ấn nhầm gây lỗi
						checkXNChoPhepTre, checkXNChoPhepSom, checkXNLamThem, checkXNTinhPC50, numPhutTinhLamThem, maskPhutTinhLamThem,
						checkXNBuGioTre, checkXNBuGioSom, checkXNBuPhepTre, checkXNBuPhepSom, //ver 4.0.0.8
						checkXNVaoTreTinhCV, checkXNRaaSomTinhCV);//ver 4.0.0.4	
			}
			else if (dgrdGioCoLamThem.SelectedRows.Count == 1) {
				MyUtility.EnableDisableControl(true,
						btnXacNhan, btnDoiCa,
						checkXNChoPhepTre, checkXNChoPhepSom, checkXNLamThem, checkXNTinhPC50, numPhutTinhLamThem, maskPhutTinhLamThem,
						checkXNBuGioTre, checkXNBuGioSom, checkXNBuPhepTre, checkXNBuPhepSom, //ver 4.0.0.8
						checkXNVaoTreTinhCV, checkXNRaaSomTinhCV);//ver 4.0.0.4	
				var dataRowView = dgrdGioCoLamThem.SelectedRows[0].DataBoundItem as DataRowView;
				var nhanvien = dataRowView["cUserInfo"] as cUserInfo;
				var CIO = dataRowView["cCheckInOut"] as cCheckInOut;
				var ngaycong = dataRowView["cNgayCong"] as cNgayCong;

				#region fill phần thông tin CIO
				//DateTime ngaydangchon = CIO.ThuocNgayCong;
				tbTTTenNV.Text = nhanvien.TenNV;
				tbTTGioVao.Text = (CIO.Vao != null) ? CIO.Vao.Time.ToString("H:mm d/M") : string.Empty;
				tbTTGioRaa.Text = (CIO.Raa != null) ? CIO.Raa.Time.ToString("H:mm d/M") : string.Empty;
				tbTTThuocCa.Text = CIO.CIOCodeFull();
				tbTTGioLam.Text = (CIO.HaveINOUT == 0) ? CIO.TG.GioLamViec.ToString(@"h\gmm\p") : string.Empty;
				tbTTTongGio.Text = (CIO.HaveINOUT == 0) ? CIO.TG.GioThuc.ToString(@"h\gmm\p") : string.Empty;
				tbTTTreSom.Text = (CIO.HaveINOUT == 0) ? (CIO.TG.VaoTre + CIO.TG.RaaSom).ToString(@"h\gmm\p") : string.Empty;
				tbTTOLaiThem.Text = (CIO.HaveINOUT == 0) ? CIO.TG.OLai.ToString(@"h\gmm\p") : string.Empty;
				tbTTLamThem.Text = (CIO.DaXN) ? CIO.TG.OTCa.ToString(@"h\gmm\p") : string.Empty;
				lbTTCongBuPhepTre.Text = this.ChuyenDoiCongThanhChu(CIO.BuCongPhepTreCongDon);//ver 4.0.0.8
				lbTTCongBuPhepSom.Text = this.ChuyenDoiCongThanhChu(CIO.BuCongPhepSomCongDon);//ver 4.0.0.8
				checkTTChoPhepTre.Checked = CIO.DuyetChoPhepVaoTre;
				checkTTChoPhepSom.Checked = CIO.DuyetChoPhepRaSom;
				checkTTTinhPC50.Checked = ngaycong.TinhPC50;
				checkTTVaoTreTinhCV.Checked = CIO.VaoTreTinhCV;//ver 4.0.0.4	
				checkTTRaaSomTinhCV.Checked = CIO.RaaSomTinhCV;//ver 4.0.0.4	
				checkTTBuGioTre.Checked = CIO.ChoBuGioTre;//ver 4.0.0.8
				checkTTBuGioSom.Checked = CIO.ChoBuGioSom;//ver 4.0.0.8
				checkTTBuPhepTre.Checked = CIO.ChoBuPhepTre;//ver 4.0.0.8
				checkTTBuPhepSom.Checked = CIO.ChoBuPhepSom;//ver 4.0.0.8
				#endregion

				#region fill phần xác nhận CIO

				tbXNCa.Text = CIO.CIOCodeFull();
				tbXNCa.Tag = (CIO.HaveINOUT == 0) ? CIO.ThuocCa : null;
				tbXNGioLam.Text = (CIO.HaveINOUT == 0) ? CIO.TG.GioLamViec.ToString(@"h\gmm\p") : string.Empty;
				tbXNTre.Text = (CIO.HaveINOUT == 0) ? CIO.TG.VaoTre.ToString(@"h\gmm\p") : string.Empty;
				tbXNSom.Text = (CIO.HaveINOUT == 0) ? CIO.TG.RaaSom.ToString(@"h\gmm\p") : string.Empty;
				tbXN_OLaiThem.Text = (CIO.HaveINOUT == 0) ? CIO.TG.OLai.ToString(@"h\gmm\p") : string.Empty;
				//numPhutTinhLamThem.Maximum = (CIO.HaveINOUT == 0) ? Convert.ToInt32(CIO.TG.OLai.TotalMinutes) : 0;
				//numPhutTinhLamThem.Value = (CIO.HaveINOUT == 0) ? (Convert.ToInt32(CIO.TG.OLai.TotalMinutes) / 10) * 10 : 0;
				maskPhutTinhLamThem.Tag = (CIO.TG.OLai == null) ? TimeSpan.Zero : CIO.TG.OLai;//ver 4.0.0.4	
				maskPhutTinhLamThem.Text = (CIO.TG.OLai == null) ? "00:00" : CIO.TG.OLai.ToString(@"hh\:mm");//ver 4.0.0.4	
				checkXNChoPhepTre.Checked = CIO.DuyetChoPhepVaoTre;
				checkXNChoPhepSom.Checked = CIO.DuyetChoPhepRaSom;
				checkXNBuGioTre.Checked = CIO.ChoBuGioTre;//ver 4.0.0.8
				checkXNBuGioSom.Checked = CIO.ChoBuGioSom;//ver 4.0.0.8
				checkXNBuPhepTre.Checked = CIO.ChoBuPhepTre;//ver 4.0.0.8
				checkXNBuPhepSom.Checked = CIO.ChoBuPhepSom;//ver 4.0.0.8
				cbCongPhepTre.SelectedIndexChanged -= this.checkXNChoPhepTre_CheckedChanged;//ver 4.0.0.8
				cbCongPhepSom.SelectedIndexChanged -= this.checkXNChoPhepTre_CheckedChanged;//ver 4.0.0.8
				cbCongPhepTre.SelectedIndex = this.ChuyenDoiSangIndexCongBuPhep(CIO.ChoBuPhepTre, CIO.BuCongPhepTreCongDon);//ver 4.0.0.8
				cbCongPhepSom.SelectedIndex = this.ChuyenDoiSangIndexCongBuPhep(CIO.ChoBuPhepSom, CIO.BuCongPhepSomCongDon);//ver 4.0.0.8
				cbCongPhepTre.SelectedIndexChanged += this.checkXNChoPhepTre_CheckedChanged;//ver 4.0.0.8
				cbCongPhepSom.SelectedIndexChanged += this.checkXNChoPhepTre_CheckedChanged;//ver 4.0.0.8
				checkXNLamThem.Checked = false;
				checkXNTinhPC50.Checked = true;
				checkXNVaoTreTinhCV.Checked = CIO.VaoTreTinhCV;//ver 4.0.0.4	
				checkXNRaaSomTinhCV.Checked = CIO.RaaSomTinhCV;//ver 4.0.0.4	
				cbXNLyDo.SelectedIndex = 0;
				tbXNGhiChu.Text = string.Empty;

				#endregion

			}
			else { // chế độ xác nhận hàng loạt
				MyUtility.EnableDisableControl(true,
						btnXacNhan, btnDoiCa,
						checkXNChoPhepTre, checkXNChoPhepSom, checkXNTinhPC50, checkXNLamThem, numPhutTinhLamThem,
						checkXNBuGioTre, checkXNBuGioSom, checkXNBuPhepTre, checkXNBuPhepSom, //ver 4.0.0.8
						maskPhutTinhLamThem, checkXNVaoTreTinhCV, checkXNRaaSomTinhCV);//ver 4.0.0.4	
				var dataRowView = dgrdGioCoLamThem.SelectedRows[0].DataBoundItem as DataRowView;
				var nhanvien = dataRowView["cUserInfo"] as cUserInfo;
				var CIO = dataRowView["cCheckInOut"] as cCheckInOut;
				var ngaycong = dataRowView["cNgayCong"] as cNgayCong;

				#region fill thông tin CIO
				tbTTTenNV.Text = nhanvien.TenNV;
				tbTTGioVao.Text = (CIO.Vao != null) ? CIO.Vao.Time.ToString("H:mm d/M") : string.Empty;
				tbTTGioRaa.Text = (CIO.Raa != null) ? CIO.Raa.Time.ToString("H:mm d/M") : string.Empty;
				tbTTThuocCa.Text = CIO.CIOCodeFull();
				tbTTGioLam.Text = (CIO.HaveINOUT == 0) ? CIO.TG.GioLamViec.ToString(@"h\gmm\p") : string.Empty;
				tbTTTongGio.Text = (CIO.HaveINOUT == 0) ? CIO.TG.GioThuc.ToString(@"h\gmm\p") : string.Empty;
				tbTTTreSom.Text = (CIO.HaveINOUT == 0) ? (CIO.TG.VaoTre + CIO.TG.RaaSom).ToString(@"h\gmm\p") : string.Empty;
				tbTTOLaiThem.Text = (CIO.HaveINOUT == 0) ? CIO.TG.OLai.ToString(@"h\gmm\p") : string.Empty;
				tbTTLamThem.Text = (CIO.DaXN) ? CIO.TG.OTCa.ToString(@"h\gmm\p") : string.Empty;
				lbTTCongBuPhepTre.Text = this.ChuyenDoiCongThanhChu(CIO.BuCongPhepTreCongDon);//ver 4.0.0.8
				lbTTCongBuPhepSom.Text = this.ChuyenDoiCongThanhChu(CIO.BuCongPhepSomCongDon);//ver 4.0.0.8
				checkTTChoPhepTre.Checked = CIO.DuyetChoPhepVaoTre;
				checkTTChoPhepSom.Checked = CIO.DuyetChoPhepRaSom;
				checkTTTinhPC50.Checked = ngaycong.TinhPC50;
				checkTTVaoTreTinhCV.Checked = CIO.VaoTreTinhCV;//ver 4.0.0.4	
				checkTTRaaSomTinhCV.Checked = CIO.RaaSomTinhCV;//ver 4.0.0.4	
				checkTTBuGioTre.Checked = CIO.ChoBuGioTre;//ver 4.0.0.8
				checkTTBuGioSom.Checked = CIO.ChoBuGioSom;//ver 4.0.0.8
				checkTTBuPhepTre.Checked = CIO.ChoBuPhepTre;//ver 4.0.0.8
				checkTTBuPhepSom.Checked = CIO.ChoBuPhepSom;//ver 4.0.0.8

				#endregion

				#region fill phần xác nhận CIO
				var listCIO = (from DataGridViewRow dataGridViewRow in dgrdGioCoLamThem.SelectedRows
							   let row = (DataRowView)dataGridViewRow.DataBoundItem
							   select (cCheckInOut)row["cCheckInOut"]).ToList();

				var distinctShift = (from cio in listCIO where cio.HaveINOUT == 0 select cio.ThuocCa).Distinct().ToList();
				var distinctList_KR = listCIO.Where(item => item.HaveINOUT == -1).ToList();
				var distinctList_KV = listCIO.Where(item => item.HaveINOUT == -2).ToList();

				// phần ca làm việc
				if (distinctShift.Count == 1) {
					tbXNCa.Tag = distinctShift[0];
					tbXNCa.Text = distinctShift[0].Code;
				}
				else {
					tbXNCa.Tag = null;
					tbXNCa.Text = string.Empty;
				}

				// phần giờ làm, ra trễ, về sớm, ở lại cần xn
				MyUtility.ClearControlText(tbXNGioLam, tbXNTre, tbXNSom, tbXN_OLaiThem);
				//numPhutTinhLamThem.Value = 0;
				//numPhutTinhLamThem.Maximum = 840;
				maskPhutTinhLamThem.Text = "00:00";//ver 4.0.0.4	
				maskPhutTinhLamThem.Tag = XL2._16gio;//ver 4.0.0.4	

				MyUtility.CheckedCheckBox(false, checkXNChoPhepTre, checkXNChoPhepSom, checkXNLamThem,
					checkXNBuGioTre, checkXNBuGioSom, checkXNBuPhepTre, checkXNBuPhepSom, //ver 4.0.0.8
					checkXNVaoTreTinhCV, checkXNRaaSomTinhCV);//ver 4.0.0.4	
				checkXNTinhPC50.Checked = true;
				cbXNLyDo.SelectedIndex = 0;
				cbCongPhepTre.SelectedIndexChanged -= this.checkXNChoPhepTre_CheckedChanged;//ver 4.0.0.8
				cbCongPhepSom.SelectedIndexChanged -= this.checkXNChoPhepTre_CheckedChanged;//ver 4.0.0.8
				cbCongPhepTre.SelectedIndex = 0;//ver 4.0.0.8
				cbCongPhepSom.SelectedIndex = 0;//ver 4.0.0.8
				cbCongPhepTre.SelectedIndexChanged += this.checkXNChoPhepTre_CheckedChanged;//ver 4.0.0.8
				cbCongPhepSom.SelectedIndexChanged += this.checkXNChoPhepTre_CheckedChanged;//ver 4.0.0.8
				tbXNGhiChu.Text = string.Empty;

				#endregion

			}

		}

		private int ChuyenDoiSangIndexCongBuPhep(bool ChoBuPhep, float p) {
			if (ChoBuPhep == false) return 0;
			if (Math.Abs(p - 0f) < 0.05f) return 0;
			else if (Math.Abs(p - 0.25f) < 0.05f) return 1;
			else if (Math.Abs(p - 0.5f) < 0.05f) return 2;
			else if (Math.Abs(p - 0.75f) < 0.05f) return 3;
			return 0;
		}

		private string ChuyenDoiCongThanhChu(float p) {
			if (Math.Abs(p - 0f) < 0.05f) return "";
			else if (Math.Abs(p - 0.25f) < 0.05f) return "2 giờ";
			else if (Math.Abs(p - 0.5f) < 0.05f) return "4 giờ";
			else if (Math.Abs(p - 0.75f) < 0.05f) return "6 giờ";
			return "LH bp IT";
		}

		private void btnXacNhan_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			// đọc tháng kết công để ko cho phép xác nhận nếu đã kết công
			//XL2.NgayCuoiThangKetCong = XL.DocThangKetCong();//tbd temp patch

			#region hỏi trước khi xác nhận

			if (MessageBox.Show(Resources.Text_ConfirmDongY_XacNhan, Resources.Caption_XacNhan, MessageBoxButtons.OKCancel) == DialogResult.Cancel) {
				return;
			}

			#endregion

			#region lấy trước các thông số
			//numPhutTinhLamThem.Update();

			#region //ver 4.0.0.4 kiểm tra ko nếu nhập giờ làm thêm ko hợp lệ thì báo, vượt ngưỡng max thì thoát

			TimeSpan OTCa = TimeSpan.Zero;
			if ((checkXNLamThem.Checked) && TimeSpan.TryParseExact(maskPhutTinhLamThem.Text, @"hh\:mm", CultureInfo.InvariantCulture, out OTCa) == false) {
				ACMessageBox.Show(Resources.Text_NhapThoiGianLamThemChuaHopLe, Resources.Caption_Loi, 3000);
				return;
			}
			var maxOTCa = TimeSpan.Zero;
			if (maskPhutTinhLamThem.Tag != null) maxOTCa = (TimeSpan)maskPhutTinhLamThem.Tag;
			if (OTCa > maxOTCa) {
				ACMessageBox.Show(Resources.Text_GioLamThemLonHonTGOLai, Resources.Caption_ThongBao, 3000);
				return;
			}
			#endregion

			var currShift = (tbXNCa.Tag != null) ? (cCa)tbXNCa.Tag : null;
			var bDuyetCPTre = checkXNChoPhepTre.Checked;
			var bDuyetCPSom = checkXNChoPhepSom.Checked;
			var bBuGioTre = checkXNBuGioTre.Checked;//ver 4.0.0.8
			var bBuGioSom = checkXNBuGioSom.Checked;//ver 4.0.0.8
			var bBuPhepTre = checkXNBuPhepTre.Checked;//ver 4.0.0.8
			var bBuPhepSom = checkXNBuPhepSom.Checked;//ver 4.0.0.8
			var fCongPhepTre = ((cbCongPhepTre.SelectedIndex) * 0.25f);
			var fCongPhepSom = ((cbCongPhepSom.SelectedIndex) * 0.25f);
			if (Math.Abs(fCongPhepTre - 0f) < 0.05f) bBuPhepTre = false;//ver 4.0.0.8
			if (Math.Abs(fCongPhepSom - 0f) < 0.05f) bBuPhepSom = false;//ver 4.0.0.8
			var bXacNhanLamThem = checkXNLamThem.Checked;
			//var soPhutLamThem = (checkXNLamThem.Checked) ? Convert.ToInt32(numPhutTinhLamThem.Value) : 0;
			var soPhutLamThem = Convert.ToInt32(OTCa.TotalMinutes);//ver 4.0.0.4	
			var choPhepTinhPc50 = checkXNTinhPC50.Checked;
			var bVaoTreLaCV = checkXNVaoTreTinhCV.Checked;//ver 4.0.0.4	
			var bRaaSomLaCV = checkXNRaaSomTinhCV.Checked;//ver 4.0.0.4	
			var fCongPhepTreCongDon = 0f;
			var fCongPhepSomCongDon = 0f;

			var lydo = (cbXNLyDo.SelectedItem != null) ? cbXNLyDo.SelectedItem.ToString() : cbXNLyDo.Text;
			var ghichu = tbXNGhiChu.Text;
			#endregion
			// chế độ xác nhận single
			if (dgrdGioCoLamThem.SelectedRows.Count == 1) {
				// kiểm tra các điều kiện trước khi xác nhận 
				IsReload = true;
				var dataRow = (DataRowView)dgrdGioCoLamThem.SelectedRows[0].DataBoundItem;
				XuLyDon(dataRow, currShift, bDuyetCPTre, bDuyetCPSom, soPhutLamThem, choPhepTinhPc50, lydo, ghichu,
					bVaoTreLaCV, bRaaSomLaCV,//ver 4.0.0.4	
					bBuGioTre, bBuGioSom, bBuPhepTre, fCongPhepTre, bBuPhepSom, fCongPhepSom//ver 4.0.0.8
					);
			}
			else// chế độ xác nhận multiple
			{
				IsReload = true;
				foreach (var dataRow in (from DataGridViewRow dataGridViewRow in dgrdGioCoLamThem.SelectedRows
										 select (DataRowView)dataGridViewRow.DataBoundItem)) {
					XuLyDon(dataRow, currShift, bDuyetCPTre, bDuyetCPSom, soPhutLamThem, choPhepTinhPc50, lydo, ghichu,
						bVaoTreLaCV, bRaaSomLaCV,//ver 4.0.0.4	
						bBuGioTre, bBuGioSom, bBuPhepTre, fCongPhepTre, bBuPhepSom, fCongPhepSom//ver 4.0.0.8
						);
				}
			}
			XL.TaoTableXacNhanLamThem(m_DSNV, m_tableDS_CIO);
		}

		private void XuLyDon(DataRowView dataRow, cCa currShift, bool bDuyetCPTre, bool bDuyetCPSom, int soPhutLamThem, bool choPhepTinhPc50, string lydo, string ghichu,
			bool bVaoTreLaCV, bool bRaaSomLaCV,//ver 4.0.0.4
			bool bBuGioTre, bool bBuGioSom, bool bBuPhepTre, float fCongPhepTre, bool bBuPhepSom, float fCongPhepSom//ver 4.0.0.8
			) {
			var nv = (cUserInfo)dataRow["cUserInfo"];
			var CIO = (cCheckInOut)dataRow["cCheckInOut"];
			var timevao = CIO.Vao.Time;
			var timeraa = CIO.Raa.Time;
			float tempRef_CongPhepTreCongDon = 0f, tempRef_CongPhepSomCongDon = 0f;//todo 4.0.0.8
			if (currShift.TachCaDem) {
				MessageBox.Show("Vui lòng thêm giờ ra cuối ca 3 và giờ vào đầu ca 1 và thực hiện xác nhận lại!",
					Resources.Caption_ThongBao, MessageBoxButtons.OK);
			}
			else {
				DateTime td_bd_lv, td_kt_lv_chuaOT;
				TimeSpan tempTre, tempSom, tempOLai;
				var onnduty = CIO.ThuocNgayCong.Add(currShift.Duty.Onn);
				var offduty = CIO.ThuocNgayCong.Add(currShift.Duty.Off);

				XL.Vao(timevao, onnduty, CIO.ThuocNgayCong.Add(currShift.chophepTreTS),
					bDuyetCPTre, bVaoTreLaCV, out td_bd_lv, out tempTre); //ver 4.0.0.8
				XL.XetBuGioTre(bBuGioTre, onnduty, ref td_bd_lv, ref tempTre);//ver 4.0.0.8
				XL.XetBuPhepTre(bBuPhepTre, fCongPhepTre, onnduty, ref tempRef_CongPhepTreCongDon, ref td_bd_lv, ref tempTre);//ver 4.0.0.8

				XL.Raa(timeraa, offduty, CIO.ThuocNgayCong.Add(currShift.chophepSomTS),
					bDuyetCPSom, bRaaSomLaCV, out td_kt_lv_chuaOT, out tempSom); //ver 4.0.0.8
				XL.XetBuGioSom(bBuGioSom, offduty, ref td_kt_lv_chuaOT, ref tempSom);
				XL.XetBuPhepSom(bBuPhepSom, fCongPhepSom, offduty, ref tempRef_CongPhepSomCongDon, ref td_kt_lv_chuaOT, ref  tempSom);

				XL.OLai(timeraa, CIO.ThuocNgayCong.Add(currShift.Duty.Off), CIO.ThuocNgayCong.Add(currShift.AfterOTMin), out tempOLai);
				if (soPhutLamThem > tempOLai.TotalMinutes)
					soPhutLamThem = Convert.ToInt32(tempOLai.TotalMinutes);

				XL.XacNhanCa(nv, CIO, currShift, bDuyetCPTre, bDuyetCPSom, soPhutLamThem, choPhepTinhPc50, lydo, ghichu,
					bVaoTreLaCV, bRaaSomLaCV, nv.StartNT, nv.EndddNT,//ver 4.0.0.4
					bBuGioTre, bBuGioSom, bBuPhepTre, tempRef_CongPhepTreCongDon, bBuPhepSom, tempRef_CongPhepSomCongDon//ver 4.0.0.8
					);
			}

		}

		private void btnDoiCa_Click(object sender, EventArgs e) {
			// kiểm tra xem nếu tồn tại 1 ca nào đó rồi thì gửi ca đó đi, nếu chưa tồn tại ca ( chế độ hàng loạt, tbXNCa null thì gửi đi null
			var currShift = (cCa)tbXNCa.Tag;
			frmDSCa frm = new frmDSCa { StartPosition = FormStartPosition.CenterParent, SelectedShift = currShift };
			frm.ShowDialog();
			// sau khi showdialog và nhận được ca từ form xác nhận thì tiến hành fill và tính toán
			if (frm.SelectedShift == null) return;
			currShift = frm.SelectedShift;
			tbXNCa.Tag = currShift;
			tbXNCa.Text = (currShift != null) ? currShift.Code : string.Empty;
			//nếu ca tự do thì mặc định check VaoTreLaCV, RaSomLaCV
			if (currShift != null && currShift.ID < int.MinValue + 100) {
				checkXNVaoTreTinhCV.Checked = true;
				checkXNRaaSomTinhCV.Checked = true;
			}
			else {
				checkXNVaoTreTinhCV.Checked = false;
				checkXNRaaSomTinhCV.Checked = false;
			}
			linkLabelTinhToan_LinkClicked(null, null);
		}

		private void linkLabelTinhToan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			#region lấy thông tin ca

			var selectedShift = tbXNCa.Tag as cCa;
			if (selectedShift == null) // chưa chọn ca nào hết thì ko tính toán
			{
				checkKiemTraDKNhap.Checked = false;
				return;
			}

			#endregion

			#region lấy thông tin từ datarow, CIO, NV, giờ vào, ra,

			if (dgrdGioCoLamThem.SelectedRows.Count == 0) {
				checkKiemTraDKNhap.Checked = false;
				return;
			}
			if (dgrdGioCoLamThem.SelectedRows.Count > 1) {
				return;
			}
			var dataRow = (DataRowView)dgrdGioCoLamThem.SelectedRows[0].DataBoundItem;
			var nv = (cUserInfo)dataRow["cUserInfo"];
			var CIO = (cCheckInOut)dataRow["cCheckInOut"];
			var timevao = CIO.Vao.Time;
			var timeraa = CIO.Raa.Time;

			// kiểm tra nếu thời gian vào ra có hợp lệ ko, ko hợp lệ thì thoát khỏi hàm
			if (timeraa - timevao <= TimeSpan.Zero || timeraa - timevao > XL2._24h00) {
				checkKiemTraDKNhap.Checked = false;
				return;
			}

			#endregion
			if (selectedShift.ID == int.MinValue + 0 || selectedShift.ID == int.MinValue + 1 || selectedShift.ID == int.MinValue + 2 || selectedShift.ID == int.MinValue + 3)
				//XL.TaoCaTuDo(selectedShift, timevao, XL2._08gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, "8");
				XL.TaoCaTuDo(selectedShift, timevao);

			var Ngay = XL.ThuocNgayCong(timevao);
			var bDuyetVaoTre = checkXNChoPhepTre.Checked;
			var bDuyetRaaSom = checkXNChoPhepSom.Checked;
			var bVaoTreLaCV = checkXNVaoTreTinhCV.Checked;
			var bRaaSomLaCV = checkXNRaaSomTinhCV.Checked;
			var bBuGioTre = checkXNBuGioTre.Checked;//ver 4.0.0.8
			var bBuGioSom = checkXNBuGioSom.Checked;//ver 4.0.0.8
			var bBuPhepTre = checkXNBuPhepTre.Checked;//ver 4.0.0.8
			var bBuPhepSom = checkXNBuPhepSom.Checked;//ver 4.0.0.8
			var fCongPhepTre = ((cbCongPhepTre.SelectedIndex) * 0.25f);
			var fCongPhepSom = ((cbCongPhepSom.SelectedIndex) * 0.25f);
			var fCongPhepTreCongDon = 0f;
			var fCongPhepSomCongDon = 0f;
			var bXacNhanOT = checkXNLamThem.Checked;
			var bDuocTinhPC50 = checkXNTinhPC50.Checked;
			//var iSoPhutOT = Convert.ToInt32(numPhutTinhLamThem.Value);
			//if (bXacNhanOT == false) iSoPhutOT = 0;

			#region //ver 4.0.0.4

			TimeSpan OTCa = TimeSpan.Zero;
			if (TimeSpan.TryParseExact(maskPhutTinhLamThem.Text, @"hh\:mm", CultureInfo.InvariantCulture, out OTCa) == false)
				OTCa = TimeSpan.Zero;
			var maxOTCa = (maskPhutTinhLamThem.Tag == null) ? TimeSpan.Zero : (TimeSpan)maskPhutTinhLamThem.Tag;
			if (OTCa > maxOTCa) OTCa = maxOTCa;
			if (bXacNhanOT == false) OTCa = TimeSpan.Zero;

			#endregion

			// tính giờ làm việc
			DateTime TD_BD_LV, TD_KT_LV, TD_KT_LV_ChuaOT, TD_KT_LV_DaCoOT, TD_BD_LV_Ca3, TD_KT_LV_Ca3;
			TimeSpan TGThucTe, TGGioLamViec, TGVaoTre, TGRaaSom, TGOLai, TGLamThem, TGLamBanDem;
			TimeSpan TGLamViecTrongCa;//ver 4.0.0.4	
			bool QuaDem;
			XL.TinhTG_LV_LVCa3_LamThem1Ca(Ngay, 0, true, bDuyetVaoTre, bDuyetRaaSom,
				bVaoTreLaCV, bRaaSomLaCV,//ver 4.0.0.4	
				bBuGioTre, bBuGioSom, bBuPhepTre, fCongPhepTre, ref fCongPhepTreCongDon, bBuPhepSom, fCongPhepSom, ref fCongPhepSomCongDon, //todo 4.0.0.8	
				timevao, timeraa, selectedShift.Duty.Onn, selectedShift.Duty.Off,
				selectedShift.chophepTreTS, selectedShift.chophepSomTS, selectedShift.batdaulamthemTS, selectedShift.LunchMin, OTCa,/*new TimeSpan(0, iSoPhutOT, 0),*/
				nv.StartNT, nv.EndddNT,
				out TD_BD_LV, out TD_KT_LV, out TD_KT_LV_ChuaOT, out TD_KT_LV_DaCoOT, out TD_BD_LV_Ca3, out TD_KT_LV_Ca3,
				out TGThucTe, out TGGioLamViec, out TGVaoTre, out TGRaaSom,
				out TGLamViecTrongCa,
				out TGOLai, out TGLamThem, out QuaDem, out TGLamBanDem);
			tbXNGioLam.Text = TGGioLamViec.ToString(@"h\gmm\p");
			tbXNTre.Text = TGVaoTre.ToString(@"h\gmm\p");
			tbXNSom.Text = TGRaaSom.ToString(@"h\gmm\p");
			tbXN_OLaiThem.Text = TGOLai.ToString(@"h\gmm\p");
			//numPhutTinhLamThem.Maximum = (CIO.HaveINOUT == 0) ? Convert.ToInt32(TGOLai.TotalMinutes) : 0;
			//numPhutTinhLamThem.Value = (CIO.HaveINOUT == 0) ? (Convert.ToInt32(TGOLai.TotalMinutes) / 10) * 10 : 0;

			maskPhutTinhLamThem.Tag = (CIO.HaveINOUT == 0) ? (TGOLai) : TimeSpan.Zero;
			maskPhutTinhLamThem.Text = TGOLai.ToString(@"hh\:mm");
		}

		private void checkXNChoPhepTre_CheckedChanged(object sender, EventArgs e) {
			if (checkXNBuPhepTre.Checked && sender == checkXNBuPhepTre) {
				checkXNBuGioTre.CheckedChanged -= checkXNChoPhepTre_CheckedChanged;
				checkXNBuGioTre.Checked = false;
				checkXNBuGioTre.CheckedChanged += checkXNChoPhepTre_CheckedChanged;
			}
			if (checkXNBuGioTre.Checked && sender == checkXNBuGioTre) {
				checkXNBuPhepTre.CheckedChanged -= checkXNChoPhepTre_CheckedChanged;
				checkXNBuPhepTre.Checked = false;
				checkXNBuPhepTre.CheckedChanged += checkXNChoPhepTre_CheckedChanged;
			}
			if (checkXNBuPhepSom.Checked && sender == checkXNBuPhepSom) {
				checkXNBuGioSom.CheckedChanged -= checkXNChoPhepTre_CheckedChanged;
				checkXNBuGioSom.Checked = false;
				checkXNBuGioSom.CheckedChanged += checkXNChoPhepTre_CheckedChanged;
			}
			if (checkXNBuGioSom.Checked && sender == checkXNBuGioSom) {
				checkXNBuPhepSom.CheckedChanged -= checkXNChoPhepTre_CheckedChanged;
				checkXNBuPhepSom.Checked = false;
				checkXNBuPhepSom.CheckedChanged += checkXNChoPhepTre_CheckedChanged;
			}
			linkLabelTinhToan_LinkClicked(null, null);
		}


		/*
				private void dgrdGioCoLamThem_SelectionChanged(object sender, EventArgs e) {
					#region old

					if (dgrdGioCoLamThem.SelectedRows.Count == 0) {
						#region reset layout

						MyUtility.ClearControlText(new Control[]{tbTTTenNV,tbTTGioVao,tbTTGioRaa,
						tbTTThuocCa,tbTTGioLam,tbTTTongGio,tbTTTreSom,
						tbTTOLaiThem_ThongTin,tbTTLamThem,
						tbXNCa,lbXNLoaiThieuCC,tbXNGioLam,
						tbXNTre,tbXNSom,tbXN_OLaiThem,				
						});

						checkTTChoPhepTre.Checked = false;
						checkTTChoPhepSom.Checked = false;
						checkTTChoPhepTinhPC50.Checked = false;


						checkXNChoPhepTre.Checked = false;
						checkXNChoPhepTre.Enabled = false;
						checkXNChoPhepSom.Checked = false;
						checkXNChoPhepSom.Enabled = false;
						checkXNLamThem.Checked = false;
						checkXNLamThem.Enabled = false;
						checkXNTinhPC50.Checked = false;
						checkXNTinhPC50.Enabled = false;
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
						var CIO = dataRowView["cCheckInOut"] as cCheckInOut;
						var ngaycong = dataRowView["cNgayCong"] as cNgayCong;

						#region fill thông tin CIO
						DateTime TimeStrVao = CIO.Vao.Time;
						DateTime TimeStrRaa = CIO.Raa.Time;
						//DateTime ngaydangchon = CIO.ThuocNgayCong;
						tbTTTenNV.Text = nhanvien.TenNV;
						tbTTGioVao.Text = TimeStrVao.ToString("H:mm d/M");
						tbTTGioRaa.Text = TimeStrRaa.ToString("H:mm d/M");
						tbTTThuocCa.Text = CIO.ThuocCa.Code;
						tbTTGioLam.Text = CIO.TG.GioLamViec.ToString(@"h\gmm\p");
						tbTTTongGio.Text = CIO.TG.GioThuc.ToString(@"h\gmm\p");
						tbTTTreSom.Text = (CIO.TG.VaoTre + CIO.TG.RaaSom).ToString(@"h\gmm\p");
						tbTTOLaiThem_ThongTin.Text = CIO.TG.OLai.ToString(@"h\gmm\p");
						tbTTLamThem.Text = (CIO.DaXN) ? CIO.TG.OTCa.ToString(@"h\gmm\p") : "0g";
						checkTTChoPhepTre.Checked = CIO.KoTruVaoTre;
						checkTTChoPhepSom.Checked = CIO.KoTruRaaSom;
						checkTTChoPhepTinhPC50.Checked = ngaycong.TinhPC50;

						tbXNCa.Text = CIO.ThuocCa.Code;
						tbXNCa.Tag = CIO.ThuocCa;
						dtpXNThemCC.Enabled = (CIO.HaveINOUT < 0);
						dtpXNThemCC.Value = ngaycong.Ngay;
						dtpXNThemCC.Update();
						tbXNGioLam.Text = CIO.TG.GioLamViec.ToString(@"h\gmm\p");
						tbXNTre.Text = CIO.TG.VaoTre.ToString(@"h\gmm\p");
						tbXNSom.Text = CIO.TG.RaaSom.ToString(@"h\gmm\p");
						tbXN_OLaiThem.Text = CIO.TG.OLai.ToString(@"h\gmm\p");
						numPhutTinhLamThem.Maximum = Convert.ToInt32(CIO.TG.OLai.TotalMinutes);
						checkXNChoPhepTre.Checked = CIO.KoTruVaoTre;
						checkTTChoPhepSom.Checked = CIO.KoTruRaaSom;
						checkXNLamThem.Checked = false;
						checkTTChoPhepTinhPC50.Checked = false;

						#endregion

						// xét xem nếu chọn đang chọn nhiều row là chế độ hàng loạt

						/*
										// 4. load ds chọn ca, tạo thêm 2 ca tự do và ca dài
										var dsCaChon = new List<cCa>(nhanvien.LichTrinhLV.DSCaMoRong);
										var caKDQD = new cCa { ID = int.MinValue, Code = "Ca8h", Is_CaTuDo = true};
										var CaDaiA = new cCa { ID = int.MinValue + 1, Code = "CaDài 12h", Is_CaTuDo = true};
										XL.TaoCaTuDo(caKDQD, TimeStrVao, XL2._08gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, "8");
										XL.TaoCaTuDo(CaDaiA, TimeStrVao, XL2._12gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1.5f, "D");

										// loại bỏ những ca chắc chắn ko xảy ra: ra ca < check vào 30ph. check ra < vào ca 30ph
										dsCaChon.RemoveAll(item => (
											CIO.TG.GioThuc.TotalHours < (item.WorkingTimeTS.TotalHours / 3d)
											|| TimeStrVao.TimeOfDay < XL2._04h30 // xem [140515_3] 
											|| TimeStrRaa < ngaydangchon.Add(item.Duty.Onn).Add(XL2._30phut)
											|| TimeStrVao > ngaydangchon.Add(item.Duty.Off).Subtract(XL2._30phut)));
										// sau khi loại bỏ mới thêm 2 ca tự do vào
										dsCaChon.Insert(0, caKDQD);
										dsCaChon.Insert(1, CaDaiA);

										cbXNChonCa.DataSource = dsCaChon;
										cbXNChonCa.Update();
										cbXNChonCa.SelectedItem = (from a in dsCaChon where a.ID == CIO.ThuocCa.ID select a).FirstOrDefault();
										cbChonCa_SelectionChangeCommitted(cbXNChonCa, new EventArgs());
						#1#

					}
					else { // chế độ xác nhận hàng loạt
						btnXacNhan.Enabled = true;

						var dataRowView = dgrdGioCoLamThem.SelectedRows[0].DataBoundItem as DataRowView;
						var nhanvien = dataRowView["cUserInfo"] as cUserInfo;
						var CIO = dataRowView["cCheckInOut"] as cCheckInOut;
						var ngaycong = dataRowView["cNgayCong"] as cNgayCong;

						#region fill thông tin CIO
						DateTime TimeStrVao = CIO.Vao.Time;
						DateTime TimeStrRaa = CIO.Raa.Time;
						//DateTime ngaydangchon = CIO.ThuocNgayCong;
						tbTTTenNV.Text = nhanvien.TenNV;
						tbTTGioVao.Text = TimeStrVao.ToString("H:mm d/M");
						tbTTGioRaa.Text = TimeStrRaa.ToString("H:mm d/M");
						tbTTThuocCa.Text = CIO.ThuocCa.Code;
						tbTTGioLam.Text = CIO.TG.GioLamViec.ToString(@"h\gmm\p");
						tbTTTongGio.Text = CIO.TG.GioThuc.ToString(@"h\gmm\p");
						tbTTTreSom.Text = (CIO.TG.VaoTre + CIO.TG.RaaSom).ToString(@"h\gmm\p");
						tbTTOLaiThem_ThongTin.Text = (CIO.DaXN == false) ? CIO.TG.OLai.ToString(@"h\gmm\p") : "0";
						tbTTLamThem.Text = (CIO.DaXN) ? CIO.TG.OTCa.ToString(@"h\gmm\p") : "0";
						checkTTChoPhepTre.Checked = CIO.KoTruVaoTre;
						checkTTChoPhepSom.Checked = CIO.KoTruRaaSom;
						checkTTChoPhepTinhPC50.Checked = ngaycong.TinhPC50;
						#endregion

						MyUtility.ClearControlText(tbXNCa, tbTTGioLam, tbXNTre, tbXNSom);
						tbXNGioLam.Text = string.Empty;
						tbXNTre.Text = string.Empty;
						tbXN_OLaiThem.Text = string.Empty;
						numPhutTinhLamThem.Value = 0;
						numPhutTinhLamThem.Maximum = 840;

						/*
										IEnumerable<DataGridViewRow> dataGridViewRows = dgrdGioCoLamThem.SelectedRows.Cast<DataGridViewRow>();
										var dataRows = from x in dataGridViewRows select ((DataRowView)x.DataBoundItem).Row;
										var dsCa = ((from row in dataRows
													 from ca in ((cUserInfo)row["cUserInfo"]).LichTrinhLV.DSCa
													 select ca)).Distinct().ToList();
										var caKDQD = new cCa { ID = int.MinValue, Code = "Ca8h", Is_CaTuDo = true};
										var CaDaiA = new cCa { ID = int.MinValue + 1, Code = "CaDài 12h",Is_CaTuDo = true};
										dsCa.Insert(0, caKDQD);
										dsCa.Insert(1, CaDaiA);
						#1#



					}
					#endregion

				}
		*/
		#region old


		/*			

			else if (dgrdGioCoLamThem.SelectedRows.Count == 1) {
				var dataRowView = dgrdGioCoLamThem.SelectedRows[0].DataBoundItem as DataRowView;
				var nv = dataRowView["cUserInfo"] as cUserInfo;
				var CIO = dataRowView["cCheckInOut"] as cCheckInOut;

				if (CIO.DaXN == false){//if (CIO is cCheckInOut_A) {
					if(CaMoi.TachCaDem == false){//if (CaMoi == false) {
						//XL.XacNhanKoTachCa(nv, CIO, CaMoi, soPhutLamThem, choPhepTinhPc50);////[140615_2]
					}
					else {
						if (DuDieuKienTach(CIO, CaMoi)) {
							//XL.XacNhanCoTachCa(nv, CIO, CaMoi, soPhutLamThem, choPhepTinhPc50);////[140615_2]
						}
					}
				}
				else {
					if (CaMoi.TachCaDem == false) {
						//XL.XacNhanLaiKoTachCa(nv, ref CIO, CaMoi, soPhutLamThem, choPhepTinhPc50);////[140615_2]
					}
					else {
						MessageBox.Show(Resources.Text_KoTheThucHienXN, Resources.Caption_ThongBao, MessageBoxButtons.OK);
					}
				}
			}
			else {
				IEnumerable<DataGridViewRow> dataGridViewRows = dgrdGioCoLamThem.SelectedRows.Cast<DataGridViewRow>();
				var arrRecord = (from row in dataGridViewRows
								 let nv = (cUserInfo)(((DataRowView)row.DataBoundItem).Row["cUserInfo"])
								 let cCheckInOut = (cCheckInOut)(((DataRowView)row.DataBoundItem).Row["cCheckInOut"])
								 let ngay = (DateTime)(((DataRowView)row.DataBoundItem).Row["TimeStrNgay"])
								 select new { nhanvien = nv, CIO = cCheckInOut, Ngay = ngay }).GroupBy(o => o.nhanvien).ToList();

				foreach (var group in arrRecord) {
					var nhanvien_goc = group.Key;
					foreach (var item in group) {
						if (CaMoi.Is_CaTuDo && CaMoi.ID == int.MinValue)
							XL.TaoCaTuDo(CaMoi, item.CIO.Vao.Time, XL2._08gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1f, "8");
						if (CaMoi.Is_CaTuDo && CaMoi.ID == int.MinValue + 1)
							XL.TaoCaTuDo(CaMoi, item.CIO.Vao.Time, XL2._12gio, XL2.ChoPhepTre, XL2.ChoPhepSom, XL2.LamThemAfterOT, 1.5f, "D");

						if (item.CIO.Vao.Time > item.CIO.ThuocNgayCong.Add(CaMoi.Duty.Off) || item.CIO.Raa.Time < item.CIO.ThuocNgayCong.Add(CaMoi.Duty.Onn)) {
							AutoClosingMessageBox.Show(
								string.Format("Không thể xác nhận ca {0} cho Nhân viên {1} vì có vào sau khi ca kết thúc hoặc ra trước khi ca bắt đầu.", CaMoi.Code, nhanvien_goc.TenNV),
								"Lỗi", 3000);
							continue;
						}
						TimeSpan olaithem;
						XL.OLai(item.CIO.Raa.Time, item.CIO.ThuocNgayCong.Add(CaMoi.Duty.Off), item.CIO.ThuocNgayCong.Add(CaMoi.batdaulamthemTS), out olaithem);

						#region tính số phút làm thêm
						if ((olaithem.TotalMinutes < soPhutLamThem && soPhutLamThem > 0)) continue; //tbd
						#endregion
						if (item.CIO.DaXN == false) {
							if (CaMoi.TachCaDem == false) {
								//XL.XacNhanKoTachCa(nhanvien_goc, item.CIO, CaMoi, soPhutLamThem, choPhepTinhPc50);////[140615_2]
							}
							else {
								if (DuDieuKienTach(item.CIO, CaMoi))
								{
									//XL.XacNhanCoTachCa(nhanvien_goc, item.CIO, CaMoi, soPhutLamThem, choPhepTinhPc50);//
}
							}
						}
						else {// giờ đã xác nhận --> update lại
							var CIO_V = item.CIO;
							if (CaMoi.TachCaDem == false) {
								//XL.XacNhanLaiKoTachCa(nhanvien_goc, ref CIO_V, CaMoi, soPhutLamThem, choPhepTinhPc50);//
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
			XL.TaoTableXacNhanTangCa(m_DSNV, m_tableDS_CIO);*/
		#endregion



	}
}
