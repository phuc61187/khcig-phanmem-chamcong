using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DAL;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;
using log4net;
using OfficeOpenXml;

namespace ChamCong_v04.UI.ChamCong {
	public partial class frm_KetCongBoPhan : Form {
		#region log tooltip và hàm ko quan trọng
		public static readonly ILog lg = LogManager.GetLogger("frm_KetCongBoPhan");

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

		public frm_KetCongBoPhan() {
			InitializeComponent();
			if (Settings.Default.LastThangKetCong == null || Settings.Default.LastThangKetCong == DateTime.MinValue) dtpThang.Value = MyUtility.FirstDayOfMonth(DateTime.Today);
			else dtpThang.Value = Settings.Default.LastThangKetCong;
		}

		private void btnDong_Click(object sender, EventArgs e) {
			Close();
		}

		private void treePhongBan_AfterSelect(object sender, TreeViewEventArgs e) {
			e.Node.Expand();
		}

		#endregion

		public List<cPhongBan> m_DSPhg = new List<cPhongBan>();
		public List<cUserInfo> m_DSNV = new List<cUserInfo>();
		public List<int> m_listIDPhongBan = new List<int>();

		private void frm_KetCongBoPhan_Load(object sender, EventArgs e) {
			#region kiểm tra kết nối csdl , nếu mất kết nối thì đóng

			if (SqlDataAccessHelper.TestConnection(SqlDataAccessHelper.ConnectionString) == false) {
				ACMessageBox.Show(Resources.Text_MatKetNoiCSDL, Resources.Caption_Loi, 4000);
				Close();
				return;
			}

			#endregion

			try //general try catch
			{
				#region //2. lấy dữ liệu phòng ban được phép thao tác  và load treePhongBan ,
				// trường hợp ko có phòng ban nào được phép thao tác thì báo và thoát form

				XL.KhoiTaoDSPhongBan(m_DSPhg, XL2.currUserID);
				XL.loadTreePhgBan(treePhongBan, XL2.TatcaPhongban, m_DSPhg);

				#endregion
				dtpThang_ValueChanged(null, null);
				// đăng ký sự kiện cho tree và chọn topNode
				treePhongBan.AfterSelect += treePhongBan_AfterSelect;
				treePhongBan.SelectedNode = treePhongBan.TopNode;

				tbTenNVLapbieu.Text = Settings.Default.LastTenNVLapBieuChamCong;
				tbTenTrgBP.Text = Settings.Default.LastTenTruongBP;
			} catch (Exception ex) //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}

		private void dtpThang_ValueChanged(object sender, EventArgs e) {
			try //general try catch
			{
				var ngaydauthang = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);
				Settings.Default.LastThangKetCong = ngaydauthang;
				Settings.Default.Save();
				#region lấy ds phòng đã kết công và check sẵn
				DataTable table = DAO.LayDSPhongDaKetcong(ngaydauthang);//tbd
				treePhongBan.AfterCheck -= treePhongBan_AfterCheck;
				//duyệt từng rows trong table và check node có id phòng, 
				//chỉ check node có id tương ứng ko thực hiện check đệ quy
				var root = treePhongBan.Nodes[0];
				//clear các check cũ
				ClearCheckedNode(false, root);
				for (int i = 0; i < table.Rows.Count; i++) {
					int idPhong = (int)table.Rows[i]["IDPhong"];
					setCheckedNode_PhongDaKetcong(idPhong, root);//suyệt đệ quy chứ ko check đệ quy
				}
				treePhongBan.AfterCheck += treePhongBan_AfterCheck;
				#endregion
			} catch (Exception ex) //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}

		private void setCheckedNode_PhongDaKetcong(int idPhong, TreeNode root) {
			// đã vô hiệu hoá afterCheck
			// hàm này tìm các node có idPhong để set true, ko thực hiện set true toàn bộ các node con
			var phong = (cPhongBan)root.Tag;
			if (phong == null) return;
			if (phong.ID == idPhong) root.Checked = true;
			if (root.Nodes.Count > 0)
				for (int i = 0; i < root.Nodes.Count; i++) {
					TreeNode node = root.Nodes[i];
					setCheckedNode_PhongDaKetcong(idPhong, node);
				}
		}
		private void ClearCheckedNode(bool IsCheck, TreeNode root) {
			// đã vô hiệu hoá afterCheck
			// hàm này tìm các node có idPhong để set true, ko thực hiện set true toàn bộ các node con
			if (root == null) return;
			root.Checked = IsCheck;
			if (root.Nodes.Count > 0)
				for (int i = 0; i < root.Nodes.Count; i++) {
					TreeNode node = root.Nodes[i];
					ClearCheckedNode(false, node);
				}
		}

		public void GetTopLevelNode(ref TreeNode currentNode) {
/*
			if (currentNode.Parent != null) {
				currentNode = currentNode.Parent;
				GetTopLevelNode(ref currentNode);
			}
*/

			while (currentNode.Parent != null) {
				currentNode = currentNode.Parent;
			}
			//return currentNode;
		}

		private void btnThucHien_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			var thang = dtpThang.Value;

			#region confirm trước khi kết công
			if (MessageBox.Show(string.Format(Resources.Text_ConfirmKetCongBoPhan, thang.ToString("MM/yyyy")), Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) {
				return;
			}
			#endregion

			string filePath = string.Empty;
			saveFileDialog.ShowDialog();
			if (saveFileDialog.FileName != string.Empty) {
				filePath = saveFileDialog.FileName;
			}
			else {
				ACMessageBox.Show("Vui lòng nhập tên file.", Resources.Caption_ThongBao, 2000);
				return;
			}
			string tenNVLapBieu = tbTenNVLapbieu.Text;
			string tenTrgBP = tbTenTrgBP.Text;
			XL.SaveSetting(lastTenNVLapBieuChamCong: tenNVLapBieu, lastTenTruongBP: tenTrgBP);

			try //general try catch
			{
				#region lấy ds phòng ban 1. được thao tác, 2.check kết công
				List<cPhongBan> dsphongban = new List<cPhongBan>();
				// đưa về root node trước khi thực hiện
				var root = treePhongBan.TopNode;
				GetTopLevelNode(ref root);// mỗi lần duyệt node sẽ làm root node chuyển về parent của node cuối nên phải trả về node gốc để duyệt từ đầu
				while (root.PrevNode != null)
					root = root.PrevNode;
				GetNode_DuocThaotac_CheckKetcong(root, dsphongban);
				#endregion

				WaitWindow.Show(this.ThucHienKetCong, "Đang kết công, bạn vui lòng đợi trong giây lát...", new object[] { thang, dsphongban });
				WaitWindow.Show(this.XuatBBChamCong, "Đang xuất báo biểu, bạn vui lòng đợi trong giây lát...", new object[] { filePath, tenNVLapBieu, tenTrgBP, thang, dsphongban });
				//kết công xong thì đóng form
				Close();
			} catch (Exception ex) //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}

		public void ThucHienKetCong(object sender, WaitWindowEventArgs e) {

			#region

			string TemplateShiftParams = @"Ca: [{0}]; ID: [{1}]; Onn: [{2}]; Off: [{3}]; 
BD_NDVao: [{4}]; KT_NDVao: [{5}]; BD_NDRa: [{6}]; KT_NDRa: [{7}];
CPTre: [{8}]ph: [{9}]; CPSom: [{10}]ph: [{11}]; AfterOT: [{12}]ph: [{13}]; LunchMin: [{14}];
WKTime: [{15}]; WKDay: [{16}]; DayCount: [{17}]; QuaDem: [{18}];
CaMoRong: [{19}]; CaTuDo: [{20}]; TachCaDem: [{21}];";
			string templateThongTinCa = @"Thông tin ca [{10}] (ký hiệu [{11}]):  Vào ca lúc [{0}], Ra ca lúc [{1}], Ca [{2}], 
Ca làm việc [{3}] tiếng, được nghỉ trưa [{4}] phút, tính [{5}] công, 
cho phép trễ [{6}] phút, ra sớm [{7}] phút, thời gian làm thêm tối thiểu [{8}] phút, [{9}] qua xác nhận quản lý";
			string shiftParams = string.Empty, thongtinCa = string.Empty;

			#endregion

			DateTime thang = (DateTime)e.Arguments[0];
			List<cPhongBan> dsphongban = (List<cPhongBan>)e.Arguments[1];
			#region  lấy ngày BD và kết thúc

			DateTime ngaydauthang = MyUtility.FirstDayOfMonth(thang), ngaycuoithang = MyUtility.LastDayOfMonth(thang);
			var ngayBD_Bef2D = ngaydauthang.AddDays(-2d);
			var ngayKT_Aft2D = ngaycuoithang.AddDays(2d);

			#endregion

			try //general try catch
			{
				// khởi tạo các nhân viên chấm công thuộc phòng ban check kết công (được thao tác) và tính toán công, phụ cấp
				XL.KhoiTaoDSNV_ChamCong(m_DSNV, (from cPhongBan item in dsphongban select item.ID).ToList(), dsphongban);
				XL.XemCong_v08(m_DSNV, ngayBD_Bef2D, ngayKT_Aft2D);
				// sau khi tính công xong thì cập nhật lại xử lý các trường hợp công nhật
				var tableCongNhat = DAO.LayTableCongNhat(thang);
				foreach (DataRow row in tableCongNhat.Rows) {
					int macc = (int)row["UserEnrollNumber"];
					DateTime ngayBD = (DateTime)row["NgayBatDau"];
					DateTime ngayKT = (DateTime)row["NgayKetThuc"];
					var nv = m_DSNV.Find(o => o.MaCC == macc); //tìm nv , ko tìm thấy thì tiếp tục
					if (nv == null) continue;
					//duyệt qua các ngày công để set lại phụ cấp, xoá hết các ds vắng
					float CongCongnhat = 0f;
					foreach (var ngayCong in nv.DSNgayCong.Where(o => o.Ngay >= ngayBD && o.Ngay <= ngayKT)) {
						ngayCong.PhuCaps = new PhuCap();
						ngayCong.QuaDem = false;
						ngayCong.DSVang.Clear();
						CongCongnhat += ngayCong.TongCong_4008;
					}
					// cập nhật số ngày công công nhật xuống csdl
					int kq1 = SqlDataAccessHelper.ExecNoneQueryString(" update DSNVChiCongNhatThang set SoNgayCong = @SoNgayCong where UserEnrollNumber= @UserEnrollNumber", new string[] { "@SoNgayCong", "@UserEnrollNumber" }, new object[] { CongCongnhat, macc });//info ko cần log
					if (kq1 == 0) {
						MessageBox.Show(Resources.Text_CoLoi);
					}
				}

				// xoá bỏ các kết công cũ của tháng (nếu có) để ghi kết công mới cho tháng
				int kq = DAO.DelKetCongCa_Ngay(ngaydauthang, ngaycuoithang, (from nv in m_DSNV select nv.MaCC).ToList());
				//bool flagError = false;
				foreach (var nv in m_DSNV) {
					foreach (var ngayCong in nv.DSNgayCong.Where(item => item.Ngay >= ngaydauthang && item.Ngay <= ngaycuoithang)) {
						foreach (var CIO in ngayCong.DSVaoRa) {
							int kq1 = 0;
							if (CIO.HaveINOUT < 0) {
								kq1 = DAO.InsKetCongCa(nv.MaCC, ngayCong.Ngay,
													   (CIO.Vao != null) ? (DateTime?)CIO.Vao.Time : null,
													   (CIO.Raa != null) ? (DateTime?)CIO.Raa.Time : null,
													   null, string.Empty, null, null, null,
													   null, null,//ver 4.0.0.4	VaoTreLaCV, RaSomLaCV
													   null, null, null, null, null, null,//ver 4.0.0.8@BuGioTre, @BuGioSom, @BuPhepTre, @CongBuPhepTre, @BuPhepSom, @CongBuPhepSom,
													   null, null, string.Empty,
													   null, null, null, null, null, null, null, null, string.Empty, string.Empty, CIO.HaveINOUT, null);

							}
							else {
								#region tạo shiftParams

								var sp = CIO.ThuocCa;
								shiftParams = string.Format(TemplateShiftParams, sp.Code, sp.ID, sp.Duty.Onn.ToString(@"d\ hh\:mm"), sp.Duty.Off.ToString(@"d\ hh\:mm"),
															sp.NhanDienVao.Onn.ToString(@"d\ hh\:mm"), sp.NhanDienVao.Off.ToString(@"d\ hh\:mm"),
															sp.NhanDienRaa.Onn.ToString(@"d\ hh\:mm"), sp.NhanDienRaa.Off.ToString(@"d\ hh\:mm"),
															sp.LateeMin.Minutes, sp.chophepTreTS.ToString(@"d\ hh\:mm"),
															sp.EarlyMin.Minutes, sp.chophepSomTS.ToString(@"d\ hh\:mm"),
															sp.AfterOTMin.Minutes, sp.batdaulamthemTS.ToString(@"d\ hh\:mm"),
															sp.LunchMin.Minutes, sp.WorkingTimeTS.TotalMinutes.ToString("#####"),
															sp.Workingday.ToString("0.0"), sp.DayCount, Convert.ToInt32(sp.QuaDem),
															Convert.ToInt32(sp.IsExtended), Convert.ToInt32(sp.Is_CaTuDo), Convert.ToInt32(sp.TachCaDem));

								thongtinCa = string.Format(templateThongTinCa,
														   sp.Duty.Onn.ToString(@"d\ hh\:mm"), sp.Duty.Off.ToString(@"d\ hh\:mm"), sp.Code, sp.WorkingTimeTS.TotalMinutes.ToString("#####"),
														   sp.LunchMin.Minutes, sp.Workingday.ToString("0.0#"),
														   sp.LateeMin.Minutes, sp.EarlyMin.Minutes, Convert.ToInt32(sp.AfterOTMin.TotalMinutes), CIO.DaXN ? "đã" : "chưa",
														   sp.Code, sp.KyHieuCC);

								#endregion

								kq1 = DAO.InsKetCongCa(nv.MaCC, ngayCong.Ngay, CIO.Vao.Time, CIO.Raa.Time,
													   CIO.ThuocCa.ID, CIO.ThuocCa.Code, CIO.DaXN, CIO.DuyetChoPhepVaoTre, CIO.DuyetChoPhepRaSom,
													   CIO.VaoTreTinhCV, CIO.RaaSomTinhCV,//ver 4.0.0.4	
													   CIO.ChoBuGioTre, CIO.ChoBuGioSom, CIO.ChoBuPhepTre, CIO.BuCongPhepTre, CIO.ChoBuPhepSom, CIO.BuCongPhepSom,//ver 4.0.0.8
													   CIO.OTMin, CIO.QuaDem, CIO.ThuocCa.KyHieuCC, CIO.TG.GioLamViec, CIO.TG.LamThem, CIO.TG.LamBanDem, CIO.TG.GioThuc,
													   CIO.TG.VaoTre, CIO.TG.RaaSom,
													   CIO.TD.BD_LV, CIO.TD.KT_LV, thongtinCa, shiftParams,
													   CIO.HaveINOUT, CIO.Cong);
							}
						}
						if (ngayCong.DSVaoRa.Count != 0) {
							int kq2 = DAO.InsKetCongNgay(nv.MaCC, ngayCong.Ngay, ngayCong.TongCong_4008, /*ngayCong.TongNgayLVtest*/0f, ngayCong.PhuCaps._TongPC, //ver4.0.0.1
														 ngayCong.PhuCaps._30_dem, ngayCong.PhuCaps._50_TC, ngayCong.PhuCaps._100_TCC3,
														 ngayCong.PhuCaps._100_LVNN_Ngay, ngayCong.PhuCaps._150_LVNN_Dem,
														 ngayCong.PhuCaps._200_LeTet_Ngay, ngayCong.PhuCaps._250_LeTet_Dem,
														 ngayCong.PhuCaps._Cus, ngayCong.TG.GioLamViec, ngayCong.TG.LamThem,
														 ngayCong.TG.LamBanDem, ngayCong.TG.GioThuc, ngayCong.QuaDem//,
														 //ngayCong.TruCongTreVR, ngayCong.TruCongSomVR, ngayCong.TruCongTreBu, ngayCong.TruCongSomBu,
														 //ngayCong.CongDinhMucDuoi8Tieng, ngayCong.CongTichLuy,
														 );
						}
					}
				}
				// sau khi ghi kết công thì cập nhật tình trạng kết công (đã kết công), đồng thời ghi log đã kết công
				DAO.UpdInsKetCongBoPhan(ngaydauthang, (from cPhongBan item in dsphongban select item).ToList());
			} catch (Exception ex) //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}

		private void XuatBBChamCong(object sender, WaitWindowEventArgs e) {
			string filePath = e.Arguments[0].ToString();
			string tenNVLapBieu = e.Arguments[1].ToString();
			string tenTrgBP = e.Arguments[2].ToString();
			DateTime thang = (DateTime)e.Arguments[3];
			List<cPhongBan> dsphongban = (List<cPhongBan>)e.Arguments[4];

			try  //general try catch
			{
				List<WarningMessage> warningMessages = new List<WarningMessage>();
				#region lấy thông tin từ csdl và khỏi tạo  nv

				DateTime ngaydauthang = MyUtility.FirstDayOfMonth(dtpThang.Value), ngaycuoithang = MyUtility.LastDayOfMonth(dtpThang.Value);
				var tableKetcongNgay = DAO.LayKetcongNgay(ngaydauthang, ngaycuoithang);
				var tableKetcongCa = DAO.LayKetcongCa(ngaydauthang, ngaycuoithang);
				var tableXPVang = DAO.LayTableXPVang(ngaydauthang, ngaycuoithang);
				var tableNgayLe = DAO.DocNgayLe(ngaydauthang, ngaycuoithang);
				var tableDSNVChiCongnhatThang = DAO.LayTableCongNhat(ngaydauthang);

				var dsnv = new List<cUserInfo>();
				XL.KhoiTaoDSNV_ChamCong(dsnv, (from p in dsphongban select p.ID).ToList(), dsphongban); // khởi tạo tất cả nhân viên tính công, bao gồm cả công nhật ngày(nv chính thức) và công nhật tháng

				/*
							List<cPhongBan> dsphongban = new List<cPhongBan>();
							// đưa về root node trước khi thực hiện
							var root = treePhongBan.TopNode;
							GetTopLevelNode(ref root);// mỗi lần duyệt node sẽ làm root node chuyển về parent của node cuối nên phải trả về node gốc để duyệt từ đầu
							while (root.PrevNode != null) root = root.PrevNode;
							GetNode_DuocThaotac_CheckKetcong(root, dsphongban);
				*/

				#endregion
				// xác định công chuẩn của tháng
				var congChuanThang = XL.TinhCongChuanCuaThang(ngaydauthang);

				#region //load cong phu cap tung ngay cho tat ca nv, ke ca cong nhat, rieng truong hop cong nhat se xu ly ngay ben duoi

				try {
					foreach (var nv in dsnv) {
						nv.ThongKeThang = new ThongKeCong_PC();
						nv.DSNgayCong = new List<cNgayCong>();
						nv.DSVang = new List<cLoaiVang>();
						for (DateTime indexNgay = ngaydauthang; indexNgay <= ngaycuoithang; indexNgay = indexNgay.AddDays(1d)) {
							XL.LoadNgayCong(nv.MaCC, nv.DSNgayCong, indexNgay, tableKetcongNgay, tableKetcongCa);
						}
						//load ds xp vắng
						//XL.LoadDSXPVang(nv.MaCC, nv.DSNgayCong, tableXPVang);
						//XL.LoadNgayLe(nv.DSNgayCong, tableNgayLe); 
						XL.LoadDSXPVang_Le(nv.MaCC, tableXPVang, tableNgayLe, nv.DSVang);//info trường hợp nhân viên công nhật sẽ được xử lý bên dưới
						XL.PhanPhoi_DSVang7(nv.DSVang, nv.DSNgayCong);
						XL.LoadThongtinLamViecCongNhat(nv.MaCC, ref nv.NgayBDCongnhat, ref nv.NgayKTCongnhat, ref nv.LoaiCN, nv.DSNgayCong, tableDSNVChiCongnhatThang);
					}

					var soNgayChuNhat = XL.DemSoNgayNghiChunhat(ngaydauthang, true, false);
					var soNgayThu7 = XL.DemSoNgayNghiChunhat(ngaydauthang, false, true);//v 4.0.0.1
					foreach (var nv in dsnv) {
						XL.ThongKeThang(ref nv.ThongKeThang, nv.DSNgayCong, nv.NgayBDCongnhat, nv.NgayKTCongnhat, nv.LoaiCN);
						// tính công chờ việc: 1.nv công nhật ko cv. 2. nv mới chính thức thì chỉ giữ công cv khai báo
						if (nv.LoaiCN == LoaiCongNhat.NVCongNhat)// nhân viên làm công nhật, công cv tự động, khai báo = 0
						{
							nv.ThongKeThang.CongCV_Auto = 0f;
							nv.ThongKeThang.CongCV_KB = 0f;
						}
						else {
							if (nv.LoaiCN == LoaiCongNhat.NVChinhThuc)// nhân viên chính thức
							{
								nv.ThongKeThang.CongCV_Auto = congChuanThang -
															  /*(nv.ThongKeThang.Cong + nv.ThongKeThang.Le + nv.ThongKeThang.Phep +//ver4.0.0.0*/
															  /*(nv.ThongKeThang.TongNgayLV + nv.ThongKeThang.Le + nv.ThongKeThang.Phep +//ver4.0.0.1*/
															  (nv.ThongKeThang.TongNgayLV4008 + nv.ThongKeThang.Le + nv.ThongKeThang.Phep +//ver4.0.0.8
															   nv.ThongKeThang.BHXH + nv.ThongKeThang.H_CT_PT
															   + nv.ThongKeThang.PTDT + nv.ThongKeThang.NghiRo + nv.ThongKeThang.CongCV_KB);//DANGLAM
								if (nv.ThongKeThang.CongCV_Auto < 0f) nv.ThongKeThang.CongCV_Auto = 0f;
							}
							else// nhân viên chính thức vừa công nhật thì công cv_auto =0, công cv khai báo ko đổi
							{
								nv.ThongKeThang.CongCV_Auto = 0f;
							}
						}
						nv.ThongKeThang.CongCV = nv.ThongKeThang.CongCV_Auto + nv.ThongKeThang.CongCV_KB;
						if (XL.KiemTraDieuKienCongCVAuto_VuotNguong(nv.ThongKeThang.CongCV_Auto, soNgayThu7, soNgayChuNhat)) {
							warningMessages.Add(
								new WarningMessage {
									MaCC = nv.MaCC, MaNV = nv.MaNV, TenNV = nv.TenNV,
									NoiDung = string.Format(
												"Có số công chờ việc được tính tự động [{0}] công theo quy định vượt quá [{1}] ngày chủ nhật trong tháng.",
												nv.ThongKeThang.CongCV_Auto.ToString("#0.0#"), soNgayChuNhat)
								});
						}
					}


				} catch (Exception ex) {
					lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				}

				#endregion


				using (var p = new ExcelPackage()) {
					//2. xuat bb bang ket cong thang
					#region ghi sheet bang ket cong thang trinh ky

					p.Workbook.Worksheets.Add("BangKetCong");
					var ws = p.Workbook.Worksheets["BangKetCong"];
					ws.Name = "BangKetCong"; //Setting Sheet's name
					XL.ExportSheetBangKetcongThang(ws, MyUtility.FirstDayOfMonth(dtpThang.Value), MyUtility.LastDayOfMonth(dtpThang.Value),
												   dsnv, tenNVLapBieu, tenTrgBP, XL2.PC30, XL2.PC50, XL2.PCTCC3, XL2.PC100, XL2.PC160, XL2.PC200, XL2.PC290); //info dsnv kết công bộ phận gồm cả nv công nhật, chính thức, vừa chính thức vừa công nhật  khác với bảng lương

					#endregion
					//3. xuat bb chi tiết kết công
					#region ghi sheet chi tiết kết công

					p.Workbook.Worksheets.Add("ChiTietKetCong");
					ws = p.Workbook.Worksheets["ChiTietKetCong"];
					ws.Name = "ChiTietKetCong"; //Setting Sheet's name
					XL.ExportSheetBangChiTietKetCong(ws, MyUtility.FirstDayOfMonth(dtpThang.Value), MyUtility.LastDayOfMonth(dtpThang.Value),
													 dsnv, XL2.PC30, XL2.PC50, XL2.PCTCC3, XL2.PC100, XL2.PC160, XL2.PC200, XL2.PC290);

					#endregion
					//4. xuất bb lưu ý nếu có
					if (warningMessages.Count > 0) {
						p.Workbook.Worksheets.Add("LUUY");
						ws = p.Workbook.Worksheets["LUUY"];
						ws.Name = "LuuY"; //Setting Sheet's name
						ws.Cells.Style.Font.Name = "Times New Roman";
						ws.Cells.Style.Font.Size = 12;
						int top = 1, left = 1;
						int ir = top, ic = left;
						XL.FormatCell_T(ws, ref ir, ref ic, "Mã NV", plusCol: 1);
						XL.FormatCell_T(ws, ref ir, ref ic, "Tên NV", plusCol: 1);
						XL.FormatCell_T(ws, ref ir, ref ic, "Nội dung lưu ý", plusCol: 1, plusRow: 1);
						foreach (WarningMessage message in warningMessages) {
							ic = left;
							XL.FormatCell_W(ws, ref ir, ref ic, message.MaNV, colWidth: (int)L.MANV, plusCol: 1);
							XL.FormatCell_W(ws, ref ir, ref ic, message.TenNV, colWidth: (int)L.HOTEN, plusCol: 1);
							XL.FormatCell_W(ws, ref ir, ref ic, message.NoiDung, colWidth: 60, plusCol: 1, plusRow: 1);
						}
					}
					Byte[] bytes = p.GetAsByteArray();
					XL.XuatFileExcel(filePath, bytes, "frm_KetCongBoPhan XuatBBChamCong ");
				}

				if (warningMessages.Count > 0) {
					MessageBox.Show("Vui lòng xem lại các cảnh báo của quá trình kết công trong sheet lưu ý.", Resources.Caption_ThongBao);
				}
			} catch (Exception ex)  //general try catch
			{
				lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}
		}

		private void GetNode_DuocThaotac_CheckKetcong(TreeNode root, List<cPhongBan> dsphongban) {
			if (root == null) return;
			var phong = (cPhongBan)root.Tag;
			if (phong.ChoPhep && root.Checked) dsphongban.Add(phong);
			if (root.Nodes.Count > 0)
				for (int index = 0; index < root.Nodes.Count; index++) {
					TreeNode node = root.Nodes[index];
					GetNode_DuocThaotac_CheckKetcong(node, dsphongban);
				}
		}

		private void treePhongBan_AfterCheck(object sender, TreeViewEventArgs e) {
			TreeNode root = e.Node;
			treePhongBan.AfterCheck -= treePhongBan_AfterCheck;
			var @checked = root.Checked;
			setCheckedNode(root, @checked);
			treePhongBan.AfterCheck += treePhongBan_AfterCheck;

		}

		private void setCheckedNode(TreeNode root, bool @checked) {
			// set check đệ quy trạng thái tương ứng cho toàn bộ node con
			if (root == null) return;
			root.Checked = @checked;
			root.Expand();

			if (root.Nodes.Count > 0)
				for (int i = 0; i < root.Nodes.Count; i++) {
					TreeNode node = root.Nodes[i];
					setCheckedNode(node, @checked);
				}
		}

	}
}
