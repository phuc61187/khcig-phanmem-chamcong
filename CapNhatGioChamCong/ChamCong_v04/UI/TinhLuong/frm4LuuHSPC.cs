using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ChamCong_v04.BUS;
using ChamCong_v04.DAL;
using ChamCong_v04.DTO;
using ChamCong_v04.Helper;
using ChamCong_v04.Properties;
using log4net;
using OfficeOpenXml;

namespace ChamCong_v04.UI.TinhLuong {
	public partial class frm4LuuHSPC : Form {
		public DateTime m_Thang;

		#region hàm ko quan trọng

		public static readonly ILog lg = LogManager.GetLogger("XuatBB");

		public frm4LuuHSPC() {
			InitializeComponent();
		}

		private void btnThoat_Click(object sender, EventArgs e) {
			Close();
		}

		#endregion

		private void frm4LuuHSPC_Load(object sender, EventArgs e) {
			var ngayDauThang = m_Thang;
			groupBox2.Text = string.Format(groupBox2.Text, ngayDauThang.ToString("MM/yyyy"));
			tbTenNVLapbieuLuong.Text = Settings.Default.LastTenNVLapBieuLuong;
			var tableThongsoKetluongThang = DAO.LayThongsoKetluongThang(ngayDauThang);

            if (tableThongsoKetluongThang.Rows.Count != 0)
            {
                numSanLuong.Value = tableThongsoKetluongThang.Rows[0]["SanLuong"] == DBNull.Value ? 0 : (int)tableThongsoKetluongThang.Rows[0]["SanLuong"];
                numDonGia.Value = tableThongsoKetluongThang.Rows[0]["DonGia"] == DBNull.Value ? 0 : (int)tableThongsoKetluongThang.Rows[0]["DonGia"];
                numSanluongGiacongNoibo.Value = tableThongsoKetluongThang.Rows[0]["SanLuongGiaCongNoiBo"] == DBNull.Value ? 0 : (int)tableThongsoKetluongThang.Rows[0]["SanLuongGiaCongNoiBo"];
                numDongiaGiacongNoibo.Value = tableThongsoKetluongThang.Rows[0]["DonGiaGiaCongNoiBo"]  == DBNull.Value ? 0 : (int)tableThongsoKetluongThang.Rows[0]["DonGiaGiaCongNoiBo"];
                numSanluongGiacongNgoai.Value = tableThongsoKetluongThang.Rows[0]["SanLuongGiaCongNgoai"] == DBNull.Value ? 0 : (int)tableThongsoKetluongThang.Rows[0]["SanLuongGiaCongNgoai"];
			    numDongiaGiacongNgoai.Value = tableThongsoKetluongThang.Rows[0]["DonGiaGiaCongNgoai"] == DBNull.Value ? 0 : (int)tableThongsoKetluongThang.Rows[0]["DonGiaGiaCongNgoai"];

                numTrichQuyLuong.Value = tableThongsoKetluongThang.Rows[0]["TrichQuyLuong"] == DBNull.Value ? 0 : (int)tableThongsoKetluongThang.Rows[0]["TrichQuyLuong"];
                numLuongToiThieu.Value = tableThongsoKetluongThang.Rows[0]["MucLuongToiThieu"]== DBNull.Value ? 0 : Convert.ToDecimal(tableThongsoKetluongThang.Rows[0]["MucLuongToiThieu"]);
                numDinhMucComTrua.Value = tableThongsoKetluongThang.Rows[0]["DinhMucComTrua"] == DBNull.Value ? 0 : Convert.ToDecimal(tableThongsoKetluongThang.Rows[0]["DinhMucComTrua"]);                

                numMucLuongTT17.Value = tableThongsoKetluongThang.Rows[0]["MucLuongTTTT17"] == DBNull.Value ? 0 : Convert.ToDecimal(tableThongsoKetluongThang.Rows[0]["MucLuongTTTT17"]);
                num11LuongPTT.Value = tableThongsoKetluongThang.Rows[0]["LuongPTT"] == DBNull.Value ? 0 : Convert.ToDecimal(tableThongsoKetluongThang.Rows[0]["LuongPTT"]);
                num12LuongLeTetBV.Value = tableThongsoKetluongThang.Rows[0]["LuongTrucLeTetBV"] == DBNull.Value ? 0 : Convert.ToDecimal(tableThongsoKetluongThang.Rows[0]["LuongTrucLeTetBV"]);
                num10PCTrachNhiem.Value = tableThongsoKetluongThang.Rows[0]["PhuCapTrachNhiem"] == DBNull.Value ? 0 : Convert.ToDecimal(tableThongsoKetluongThang.Rows[0]["PhuCapTrachNhiem"]);
            }

        }


        private void btnKetLuong_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;

			XL.SaveSetting(lastTenNVLapBieuLuong: tbTenNVLapbieuLuong.Text);

			//nếu đã kết lương tháng này rồi thì ko cho kết lương, phải hủy kết lương trước
			#region ko cho chỉnh sửa giờ nếu khoảng thời gian xem công tồn tại các ngày đã kết lương

			if (XL.Kiemtra(m_Thang.Date, MyUtility.LastDayOfMonth(m_Thang)))
			{
				MessageBox.Show(string.Format("Tháng {0} đã kết lương! Trường hợp muốn kết lương lại tháng này yêu cầu hủy kết lương tháng và thực hiện kết lương lại.", m_Thang.ToString("MM/yyyy")),
				                Resources.Caption_ThongBao);
					return;
			}

			#endregion
			WaitWindow.Show(this.KetLuong, "Đang kết lương tháng. Bạn vui lòng đợi trong giây lát..."); 

			saveFileDialog.ShowDialog();
			if (saveFileDialog.FileName != string.Empty) {
				string saveFileName = saveFileDialog.FileName;
				WaitWindow.Show(this.XuatBBLuong, "Đang xuất bảng kết lương. Bạn vui lòng đợi...",
					new object[] { tbTenNVLapbieuLuong.Text, saveFileName });
			}

		}

		private void KetLuong(object sender, WaitWindowEventArgs e) {
			#region lấy thông tin từ csdl và khỏi tạo  nv

			var ngaydauthang = MyUtility.FirstDayOfMonth(m_Thang);
			var ngaycuoithang = MyUtility.LastDayOfMonth(m_Thang);
			var tableDSNVChiCongnhatThang = DAO.LayTableCongNhat(ngaydauthang);
			var tableDSThuchiThang = DAO.LayDSThuchiThang(ngaydauthang);
			var tableKetcongNgay = DAO.LayKetcongNgay(ngaydauthang, ngaycuoithang);
			var tableKetcongCa = DAO.LayKetcongCa(ngaydauthang, ngaycuoithang);
			var tableXPVang = DAO.LayTableXPVang(ngaydauthang, ngaycuoithang);
			var tableNgayLe = DAO.DocNgayLe(ngaydauthang, ngaycuoithang);

			var dsnv = new List<cUserInfo>();
			var dsphongban = new List<cPhongBan>();
			XL.KhoiTaoDSPhongBan(dsphongban); // khởi tạo các phòng ban để cập nhật thông tin phòng ban cho nhân viên tính công
			XL.KhoiTaoDSNV_TinhLuong(dsnv, dsphongban); // khởi tạo tất cả nhân viên tính công, bao gồm cả công nhật ngày(nv chính thức) và công nhật tháng

			#endregion

			#region //load cong phu cap tung ngay cho tat ca nv, ke ca cong nhat, rieng truong hop cong nhat se xu ly ngay ben duoi

			foreach (var nv in dsnv) {
				nv.DSNgayCong = new List<cNgayCong>();
				nv.DSVang = new List<cLoaiVang>();
				for (DateTime indexNgay = ngaydauthang; indexNgay <= ngaycuoithang; indexNgay = indexNgay.AddDays(1d)) {
					XL.LoadNgayCong(nv.MaCC, nv.DSNgayCong, indexNgay, tableKetcongNgay, tableKetcongCa);
				}
				XL.LoadDSXPVang_Le(nv.MaCC, tableXPVang, tableNgayLe, nv.DSVang);//info trường hợp nhân viên công nhật sẽ được xử lý bên dưới
				XL.PhanPhoi_DSVang7(nv.DSVang, nv.DSNgayCong);

				XL.LoadThongtinLamViecCongNhat(nv.MaCC, ref  nv.NgayBDCongnhat, ref nv.NgayKTCongnhat, ref nv.LoaiCN, nv.DSNgayCong, tableDSNVChiCongnhatThang);
			}


			#endregion

			//có được ds nhân viên với công, phụ cấp, các loại vắng, công ngày lễ --> bắt đầu tính lương
			#region lấy thông số để tính lương
			var sanluong01 = (int)(numSanLuong.Value);
			var dongia02 = (int)numDonGia.Value;
			var perTrichQuyLuong = (int)numTrichQuyLuong.Value;
			var sanluongGiacongNoiBo = (int)numSanluongGiacongNoibo.Value;
			var dongiaGiacongNoiBo = (int)numDongiaGiacongNoibo.Value;
			var sanluongGiacongNgoai = (int)numSanluongGiacongNgoai.Value;
			var dongiaGiacongNgoai = (int)numDongiaGiacongNgoai.Value;
			var mucLuongToithieu = (int)numLuongToiThieu.Value;
            var mucLuongTT17 = (int)numMucLuongTT17.Value;
			var DinhMuccomtrua = (int)numDinhMucComTrua.Value;
            var luongPTT = (float)num11LuongPTT.Value;
            var luongTrucBV = (float)num12LuongLeTetBV.Value;
            var phuCapTN = (float)num10PCTrachNhiem.Value;

			double tongQuy100Per03 = Convert.ToDouble(sanluong01) * Convert.ToDouble(dongia02);
			double _80perQuy100_04 = tongQuy100Per03 * (perTrichQuyLuong / 100d); // ko cần round vì 220*0.8=176 chẵn

			#endregion
			// xác định công chuẩn của tháng
			var congChuanThang = XL.TinhCongChuanCuaThang(ngaydauthang);
			int temp = 0;
			#region // thống kê công, phụ cấp hàng ngày của từng nhân viên chính thức//trường hợp nhân viên vừa công nhật vừa chính thức thì chỉ thống kê ngày công sau ngày kết thúc công nhật
			foreach (var nv in dsnv) {
				// thống kê công và phụ cấp từng nv
				XL.ThongKeThang(ref nv.ThongKeThang, nv.DSNgayCong, nv.NgayBDCongnhat, nv.NgayKTCongnhat, nv.LoaiCN, out temp, out temp);
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
														  /*(nv.ThongKeThang.Cong + nv.ThongKeThang.Le + nv.ThongKeThang.Phep + //ver4.0.0.0*/
														  /*(nv.ThongKeThang.TongNgayLV + nv.ThongKeThang.Le + nv.ThongKeThang.Phep + //ver4.0.0.1*/
														  (nv.ThongKeThang.TongNgayLV4008
														  + nv.ThongKeThang.TongTruCongTreVR + nv.ThongKeThang.TongTruCongSomVR + nv.ThongKeThang.TreSom_KoDuBuCong //ver 4.0.0.8
														  + nv.ThongKeThang.Phep + nv.ThongKeThang.Le //ver4.0.0.8
														  + nv.ThongKeThang.BHXH + nv.ThongKeThang.H_CT_PT 
														  + nv.ThongKeThang.PTDT + nv.ThongKeThang.NghiRo + nv.ThongKeThang.CongCV_KB);//DANGLAM
						if (nv.ThongKeThang.CongCV_Auto < 0f) nv.ThongKeThang.CongCV_Auto = 0f;
					}
					else// nhân viên chính thức vừa công nhật thì công cv_auto =0, công cv khai báo ko đổi
					{
						nv.ThongKeThang.CongCV_Auto = 0f;
					}
				}
				nv.ThongKeThang.CongCV = nv.ThongKeThang.CongCV_Auto + nv.ThongKeThang.CongCV_KB;
			}

			#endregion

			#region // tính lương công nhật cho các nhân viên làm công nhật và tổng lương công nhật

			double TongLuongCongNhat_AllNV = 0d;

			foreach (DataRow row in tableDSNVChiCongnhatThang.Rows.Cast<DataRow>()) {
				// lấy thông tin
				var macc = (int)row["UserEnrollNumber"];
				var dongiaLuong = (int)row["DonGiaLuong"];
				var soNgayCong = (float)row["SoNgayCong"];
				var TamUng = (double)row["TamUng"];
				// xác định nhân viên
				var nv = dsnv.Find(o => o.MaCC == macc);
				if (nv == null) continue;
				nv.ThongKeThang.Cong_Congnhat = soNgayCong;
				nv.chiTietLuong.CongNhat = soNgayCong * dongiaLuong; //ko trừ tạm ứng, tạm ứng chỉ ở phần thực lãnh mới ghi
				TongLuongCongNhat_AllNV += nv.chiTietLuong.CongNhat;
			}

			#endregion

			#region //đọc danh sách thu chi cho từng nhân viên (lưu ý ko đọc phụ cấp của nhân viên công nhật

			foreach (DataRow row in tableDSThuchiThang.Rows.Cast<DataRow>()) {
				var macc = (int)row["UserEnrollNumber"];
				// xác định nhân viên
				var nv = dsnv.Find(o => o.MaCC == macc);
				if (nv == null) continue;
				nv.chiTietLuong.KhauTru.TamUng = (double)row["TamUng"];
				nv.chiTietLuong.LuongDieuChinh = (double)row["LuongDieuChinh"];
				nv.chiTietLuong.MucDongBHXH = (float)row["MucDongBHXH"];
				nv.chiTietLuong.KhauTru.ThuChiKhac = (double)row["ThuChiKhac"];
			}

			#endregion

			#region // tính lương cho nv chính thức

			double tong_qlcb_2 = 0d, tong_SPLamRa_B2_2 = 0d, tongQuyLuongCV = 0d, tongQuyLuongNghiDinhCP = 0d, tongChiKhacTuQuyLuong = 0d, tongQuyLuongSP = 0d, tong_luong_dieuchinh = 0d, tongKhauTruThuChiKhac = 0d;
			foreach (var nv in dsnv.Where(o => o.LoaiCN != LoaiCongNhat.NVCongNhat)) {
				//if (Math.Abs(nv.ThongKeThang.Cong - 0f) < 0.01f) nv.ThongKeThang.CongCV = 0f; //fortesting nếu ko chấm công thì cũng ko có công cv
				XL.TinhLuongCoBan_CongVaPC_A202(nv.HeSo.LuongCB, mucLuongToithieu, nv.ThongKeThang.Cong, nv.ThongKeThang.PhuCaps._TongPC,
												nv.ThongKeThang.Phep, nv.ThongKeThang.H_CT_PT, nv.ThongKeThang.PTDT, nv.ThongKeThang.Le, nv.ThongKeThang.CongCV,//DANGLAM
												out nv.chiTietLuong.LCB_Theo.CongThucTe, out nv.chiTietLuong.LCB_Theo.CheDoNghi, out nv.chiTietLuong.LCB_Theo.CongCV,
												out nv.chiTietLuong.LCB_Theo.PhuCap);
				tongQuyLuongCV += nv.chiTietLuong.LCB_Theo.CongCV;
				//tongQuyLuongNghiDinhCP += nv.chiTietLuong.LCB_Theo.TongCong_CD_CV_PC; // yêu cầu cũ (T1/2015) là có lương chờ việc
				tongQuyLuongNghiDinhCP += nv.chiTietLuong.LCB_Theo.Cong_CD_PC;// yêu cầu mới (T8/2015) là tách lương chờ việc ra

				//ver4.0.0.7 tong_qlcb_2 += nv.chiTietLuong.LCB_Theo.TongCong_CD_CV_PC + nv.chiTietLuong.LuongDieuChinh; //yêu cầu mới (T8/2015) là tách lương chờ việc ra khỏi quỹ lương cơ bản đưa vào quỹ lương sản phẩm
				tong_qlcb_2 += nv.chiTietLuong.LCB_Theo.Cong_CD_PC; //info tong_qlcb_2 bao gồm lương cb 1nv, bồi dưỡng ca 3 1nv, lương tháng trước 1 nv
                tong_luong_dieuchinh += nv.chiTietLuong.LuongDieuChinh;
                tongKhauTruThuChiKhac += nv.chiTietLuong.KhauTru.ThuChiKhac;

				XL.TinhSPLamRa_CongVaPC_B102(nv.HeSo.LuongCV, nv.ThongKeThang.Cong, nv.ThongKeThang.PhuCaps._TongPC, nv.ThongKeThang.Phep, nv.ThongKeThang.H_CT_PT, nv.ThongKeThang.PTDT, nv.ThongKeThang.Le,//DANGLAM
											 out nv.chiTietLuong.SPLamRa_Theo.CongThucTe, out nv.chiTietLuong.SPLamRa_Theo.CheDoNghi, out nv.chiTietLuong.SPLamRa_Theo.PhuCap);
				tong_SPLamRa_B2_2 += nv.chiTietLuong.SPLamRa_Theo.TongSPLamRa;
                // tính khấu trừ BHXH
                //nv.chiTietLuong.KhauTru.BHXH = Convert.ToDouble(nv.HeSo.BHXH_YT_TN * mucLuongToithieu * (nv.chiTietLuong.MucDongBHXH / 100f));
                nv.chiTietLuong.KhauTru.BHXH = Convert.ToDouble( (nv.HeSo.LCBTT17 + nv.HeSo.PCTNTT17 + nv.HeSo.PCDHTT17 + nv.HeSo.PCCVTT17) * mucLuongTT17 * (nv.chiTietLuong.MucDongBHXH / 100f));// tính BHXH theo TT17 [update T1/2018]
                // tính tiền cơm trưa
                var temp1 = DinhMuccomtrua - ((DinhMuccomtrua / congChuanThang) * nv.ThongKeThang.SoNgayNghiRO_NguyenNgay);
				nv.chiTietLuong.TienComTrua = (temp1 >= 0d) ? temp1 : 0d;

			}
			var chitienGiacongNoibo = Convert.ToDouble(sanluongGiacongNoiBo) * Convert.ToDouble(dongiaGiacongNoiBo);
			var chitienGiacongNgoai = Convert.ToDouble(sanluongGiacongNgoai) * Convert.ToDouble(dongiaGiacongNgoai);
			//var tong_qlSP_A71_1_VaGiaCong = (_80perQuy100_04 + chitienGiacongNoibo + chitienGiacongNgoai) - tong_qlcb_2 - TongLuongCongNhat_AllNV; // yêu cầu mới (T8/2015) là tách lương chờ việc ra
            var tong_qlSP_A71_1_VaGiaCong = (_80perQuy100_04 + chitienGiacongNoibo + chitienGiacongNgoai) - tong_qlcb_2 - tongQuyLuongCV - TongLuongCongNhat_AllNV - tong_luong_dieuchinh - phuCapTN - luongPTT - luongTrucBV - tongKhauTruThuChiKhac; // yêu cầu mới (T1/2018)
            tongChiKhacTuQuyLuong = tongQuyLuongCV + phuCapTN + luongPTT + luongTrucBV + tongKhauTruThuChiKhac;
            double giaTri_1SP_B3_1 = (Math.Abs(tong_SPLamRa_B2_2 - 0d) > 0.001d) ? (tong_qlSP_A71_1_VaGiaCong / tong_SPLamRa_B2_2) : 0d; // tính ra được 1 đơn vị sản phẩm có giá bao nhiêu
			//double tong0 = 0d, tong1 = 0d, tong2 = 0d, tong3 = 0d, tong4 = 0d, tong5 = 0d, tong6 = 0d, tong7 = 0d, tong8 = 0d, tong9 = 0d, tong10 = 0d, tong11 = 0d, tong12 = 0d, tong13 = 0d, tong14 = 0d, tong15 = 0d, tong16 = 0d, tong17 = 0d, tong18 = 0d, tong19 = 0d, tong20 = 0d, tong21 = 0d, tong22 = 0d, tong23 = 0d, tong24 = 0d, tong25 = 0d, tong26 = 0d, tong27 = 0d, tong28 = 0d, tong29 = 0d, tong30 = 0d, tong31 = 0d, tong32 = 0d, tong33 = 0d, tong34 = 0d, tong35 = 0d, tong36 = 0d;

			foreach (var nv in dsnv.Where(o => o.LoaiCN != LoaiCongNhat.NVCongNhat)) {
				nv.chiTietLuong.LSP_Theo.CongThucTe = nv.chiTietLuong.SPLamRa_Theo.CongThucTe * giaTri_1SP_B3_1;
				nv.chiTietLuong.LSP_Theo.CheDoNghi = nv.chiTietLuong.SPLamRa_Theo.CheDoNghi * giaTri_1SP_B3_1;
				nv.chiTietLuong.LSP_Theo.PhuCap = nv.chiTietLuong.SPLamRa_Theo.PhuCap * giaTri_1SP_B3_1;
				tongQuyLuongSP += nv.chiTietLuong.LSP_Theo.TongCong_CD_PC;
				//fortesting region [03]
			}

			#endregion

			//fortesting testing region [01]
			#region // cập nhật lương xuống csdl , trước khi cập nhật thì xoá lương cũ

			// cập nhật lương xuống csdl , trước khi cập nhật thì xoá lương cũ
			int kq1 = SqlDataAccessHelper.ExecNoneQueryString(
				"delete from KetLuongThang where Thang = @Thang", new string[] { "@Thang" }, new object[] { m_Thang }); //INFO LOG DO KẾT LƯƠNG THÁNG THỰC HIỆN

			foreach (var nv in dsnv.Where(o => o.LoaiCN != LoaiCongNhat.NVCongNhat)) {
				int kq = DAO.InsKetLuongThang(m_Thang, nv);
				//if (kq == 0) MessageBox.Show("Xảy ra lỗi tại vị trí NV: " + nv.MaCC + nv.TenNV);//fortesting
			}

			// sau khi cập nhật lương từng nhân viên thì cập nhật thông số kết lương tháng
			int kq2 = DAO.UpdInsThongsoKetluongThang(m_Thang,
													 XL2.PC30, XL2.PC50, XL2.PCTCC3, XL2.PC100, XL2.PC160, XL2.PC200, XL2.PC290,
													 sanluong01, dongia02, perTrichQuyLuong,
													 sanluongGiacongNoiBo, dongiaGiacongNoiBo, sanluongGiacongNgoai, dongiaGiacongNgoai,
													 mucLuongToithieu, /*donGiaBdCa3,*/ DinhMuccomtrua,
                                                     mucLuongTT17, luongPTT, luongTrucBV, phuCapTN,
													 tongQuyLuongCV, tongQuyLuongNghiDinhCP,
													 tongChiKhacTuQuyLuong, tongQuyLuongSP, giaTri_1SP_B3_1);

			//ghi log
			string noidung = string.Format("Kết lương tháng [{0}]", m_Thang.ToString("MM/yyyy"));
			DAO.GhiNhatKyThaotac("Kết lương", noidung);

			#endregion

		}

		private void XuatBBLuong(object sender, WaitWindowEventArgs e) {
			string tenNVLapBieu = (string)e.Arguments[0];
			string saveFileName = (string)e.Arguments[1];
			var ngaydauthang = MyUtility.FirstDayOfMonth(m_Thang);
			var ngaycuoithang = MyUtility.LastDayOfMonth(m_Thang);
			//#region lấy dữ liệu kết lương để xuất

			var tableKetLuongThang = DAO.LayKetLuongThang(ngaydauthang);
			var tableThongsoKetluongThang = DAO.LayThongsoKetluongThang(ngaydauthang);
			var tableDSNVChiCongnhatThang = DAO.LayTableCongNhat(ngaydauthang);
			var tableTongLuongCongnhat = SqlDataAccessHelper.ExecuteQueryString(
				@"select  CAST(SUM (SoNgayCong*DonGiaLuong) as float)  from DSNVChiCongNhatThang where Thang=@Thang",
				new string[] { "@Thang" }, new object[] { ngaydauthang });
			var tongLuongCongnhat = (tableTongLuongCongnhat.Rows[0][0] != DBNull.Value) ? (double)tableTongLuongCongnhat.Rows[0][0] : 0d;
			var tongLuongDieuchinh = (from DataRow row in tableKetLuongThang.Rows
									  let luongdieuchinh = (row["LuongDieuChinh"] != DBNull.Value)
															   ? (double)row["LuongDieuChinh"]
															   : 0d
									  select luongdieuchinh).Sum();
			var tableKetcongNgay = DAO.LayKetcongNgay(ngaydauthang, ngaycuoithang);
			var tableKetcongCa = DAO.LayKetcongCa(ngaydauthang, ngaycuoithang);
			var tableXPVang = DAO.LayTableXPVang(ngaydauthang, ngaycuoithang);
			var tableNgayLe = DAO.DocNgayLe(ngaydauthang, ngaycuoithang);
			var dsnv = new List<cUserInfo>();
			ChuanBiDuLieuXuatLuong(dsnv, ngaydauthang, ngaycuoithang, tableKetLuongThang, tableKetcongNgay, tableKetcongCa, tableXPVang, tableNgayLe, tableDSNVChiCongnhatThang);

			//#endregion

			//fortesting testing region [02]
			using (var p = new ExcelPackage()) {
				//1. xuat bb bang thong so san luong, don gia, he so pc
				#region lay thong so cac loai pc de ghi cot tieu de
				int pc30 = (int)tableThongsoKetluongThang.Rows[0]["HSPCDem"];
				int pc50 = (int)tableThongsoKetluongThang.Rows[0]["HSPCTangCuong"];
				int pctcc3 = (int)tableThongsoKetluongThang.Rows[0]["HSPCTangCuong_Dem"];
				int pc100 = (int)tableThongsoKetluongThang.Rows[0]["HSPC200"];
				int pc160 = (int)tableThongsoKetluongThang.Rows[0]["HSPC260"];
				int pc200 = (int)tableThongsoKetluongThang.Rows[0]["HSPC300"];
				int pc290 = (int)tableThongsoKetluongThang.Rows[0]["HSPC390"];
				#endregion
				//2. xuat bb bang ket cong thang
				#region ghi sheet bang ket cong thang trinh ky

				p.Workbook.Worksheets.Add("BangKetCong");
				var ws = p.Workbook.Worksheets["BangKetCong"];
				ws.Name = "BangKetCong"; //Setting Sheet's name
				XL.ExportSheetBangKetcongThang(ws, ngaydauthang, ngaycuoithang, dsnv, string.Empty, string.Empty, pc30, pc50, pctcc3, pc100, pc160, pc200, pc290);

				#endregion
				//3. xuat bb chi tiết kết công
				#region ghi sheet chi tiết kết công

				p.Workbook.Worksheets.Add("ChiTietKetCong");
				ws = p.Workbook.Worksheets["ChiTietKetCong"];
				ws.Name = "ChiTietKetCong"; //Setting Sheet's name
				XL.ExportSheetBangChiTietKetCong(ws, ngaydauthang, ngaycuoithang, dsnv, pc30, pc50, pctcc3, pc100, pc160, pc200, pc290);

				#endregion
				//4. xuat bb bang luong cong nhat
				#region ghi sheet bang luong cong nhat

				p.Workbook.Worksheets.Add("BangLuongCongNhat");
				ws = p.Workbook.Worksheets["BangLuongCongNhat"];
				ws.Name = "BangLuongCongNhat";
				XL.ExportSheetBangLuongCongNhat(ws, ngaydauthang, tableDSNVChiCongnhatThang);

				#endregion
				//5. xuat bb bang luong chinh thuc
				#region ghi sheet bảng lương

				p.Workbook.Worksheets.Add("BangLuong");
				ws = p.Workbook.Worksheets["BangLuong"];
				ws.Name = "BangLuong";
				XL.ExportSheetBangLuong(ws, m_Thang, dsnv, tenNVLapBieu);

				#endregion
				//6. xuat bb bang tong hop so lieu giam doc ky duyet
				#region ghi sheet bảng tổng hợp số liệu

				p.Workbook.Worksheets.Add("BangTongHopChi");
				ws = p.Workbook.Worksheets["BangTongHopChi"];
				ws.Name = "BangTongHopChi";
				XL.ExportSheetTongHopChi(ws, m_Thang, tableThongsoKetluongThang, tongLuongCongnhat, tongLuongDieuchinh);

				#endregion
				//7. xuat sheet thong ke sữa
				#region ghi sheet thống kê BD ĐH

				p.Workbook.Worksheets.Add("BangThongKeBoiDuongDocHai");
				ws = p.Workbook.Worksheets["BangThongKeBoiDuongDocHai"];
				ws.Name = "BangThongKeBoiDuongDocHai";
				XL.ExportSheetBangThongKeSua(ws, ngaydauthang, ngaycuoithang, dsnv);

				#endregion

				
				Byte[] bytes = p.GetAsByteArray();
				XL.XuatFileExcel(saveFileName, bytes, "frm4LuuHSPC XuatBBLuong");
			}


		}

		private void btnBack_Click(object sender, EventArgs e) {
			frm3NhapThuChiThang frm = new frm3NhapThuChiThang { m_thang = m_Thang };
			frm.WindowState = FormWindowState.Normal;
			frm.StartPosition = FormStartPosition.Manual;
			frm.MdiParent = this.MdiParent;
			frm.Location = new Point(0, 0);//XL2.GetCenterLocation(frm.MdiParent.ClientRectangle.Width, frm.MdiParent.ClientRectangle.Height, frm.Size.Width, frm.Size.Height);
			frm.Show();
			Close();
		}

		private void btnHuyKetluong_Click(object sender, EventArgs e) {
			if (XL2.KiemtraKetnoiCSDL() == false) return;// kiểm tra kết nối csdl trước khi thực hiện

			//confirm trước khi thực hiện
			if (MessageBox.Show(string.Format("Bạn muốn hủy kết lương tháng {0}?", m_Thang.ToString("MM/yyyy")), Resources.Caption_XacNhan, MessageBoxButtons.YesNo) == DialogResult.No) return;

			if (XL.HuyKetLuongThang(MyUtility.FirstDayOfMonth(m_Thang))) {
				ACMessageBox.Show("Đã huỷ kết lương tháng " + m_Thang.ToString("MM/yyyy"), Resources.Caption_ThongBao, 3000);
			}
			else {
				MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
			}

		}

		private void btnXuatBangChiTiet_Click(object sender, EventArgs e) {
			if (XL.Kiemtra(m_Thang.Date, MyUtility.LastDayOfMonth(m_Thang)) == false) {
				MessageBox.Show(string.Format("Tháng {0} chưa kết lương!", m_Thang.ToString("MM/yyyy")), Resources.Caption_ThongBao);
				return;
			}

			if (saveFileDialog.ShowDialog() == DialogResult.Cancel) return;
			if (saveFileDialog.FileName == string.Empty) return;

			string saveFileName = saveFileDialog.FileName;

			WaitWindow.Show(this.XuatBangChiTiet, "Đang xuất bảng chi tiết. Bạn vui lòng đợi...", saveFileName);
		}
		private void XuatBangChiTiet(object sender, WaitWindowEventArgs e) {
			string saveFileName = e.Arguments[0].ToString();

			var dsnv = new List<cUserInfo>();
			var ngaydauthang = MyUtility.FirstDayOfMonth(m_Thang);
			var ngaycuoithang = MyUtility.LastDayOfMonth(m_Thang);
			#region lấy dữ liệu kết lương để xuất

			var tableKetLuongThang = DAO.LayKetLuongThang(ngaydauthang);
			var tableThongsoKetluongThang = DAO.LayThongsoKetluongThang(ngaydauthang);
			var tableDSNVChiCongnhatThang = DAO.LayTableCongNhat(ngaydauthang);
			var tableTongLuongCongnhat = SqlDataAccessHelper.ExecuteQueryString(
				@"select  CAST(SUM (SoNgayCong*DonGiaLuong) as float)  from DSNVChiCongNhatThang where Thang=@Thang",
				new string[] { "@Thang" }, new object[] { ngaydauthang });
			var tongLuongCongnhat = (tableTongLuongCongnhat.Rows[0][0] != DBNull.Value) ? (double)tableTongLuongCongnhat.Rows[0][0] : 0d;
			var tongLuongDieuchinh = (from DataRow row in tableKetLuongThang.Rows
									  let luongdieuchinh = (row["LuongDieuChinh"] != DBNull.Value)
															   ? (double)row["LuongDieuChinh"]
															   : 0d
									  select luongdieuchinh).Sum();
			var tableKetcongNgay = DAO.LayKetcongNgay(ngaydauthang, ngaycuoithang);
			var tableKetcongCa = DAO.LayKetcongCa(ngaydauthang, ngaycuoithang);
			var tableXPVang = DAO.LayTableXPVang(ngaydauthang, ngaycuoithang);
			var tableNgayLe = DAO.DocNgayLe(ngaydauthang, ngaycuoithang);

			#endregion
			ChuanBiDuLieuXuatLuong(dsnv, ngaydauthang, ngaycuoithang, tableKetLuongThang, tableKetcongNgay, tableKetcongCa, tableXPVang, tableNgayLe, tableDSNVChiCongnhatThang);


			var @continue = true;
			FileInfo template, fileResult;
			try {
				template = new FileInfo(Settings.Default.pathReportChiTietLuong);
			} catch (Exception ex) {
				if (ex is System.Security.SecurityException || ex is UnauthorizedAccessException) {
					MessageBox.Show(string.Format(Resources.Text_KoCoQuyenTruyCapFileX, "mẫu xuất báo biểu"), Resources.Caption_Loi); return;
				}
				else if (ex is NotSupportedException || ex is PathTooLongException) {
					MessageBox.Show(string.Format(Resources.Text_UnsupportedFile_PathTooLong, "mẫu xuất báo biểu"), Resources.Caption_Loi); return;
				}
				else {
					MessageBox.Show(string.Format(Resources.Text_CoLoi), Resources.Caption_Loi); return;
				}
			}
			try {
				fileResult = new FileInfo(saveFileName);

			} catch (Exception ex) {
				if (ex is System.Security.SecurityException || ex is UnauthorizedAccessException) {
					MessageBox.Show(string.Format(Resources.Text_KoCoQuyenTruyCapFileX, "mẫu xuất báo biểu"), Resources.Caption_Loi); return;
				}
				else if (ex is NotSupportedException || ex is PathTooLongException) {
					MessageBox.Show(string.Format(Resources.Text_UnsupportedFile_PathTooLong, "mẫu xuất báo biểu"), Resources.Caption_Loi); return;
				}
				else {
					MessageBox.Show(string.Format(Resources.Text_CoLoi), Resources.Caption_Loi); return;
				}
			}

			try {
				using (var packageResult = new ExcelPackage(template)) {

					var workbook = packageResult.Workbook;
					#region ghi sheet thong so

					var ws = workbook.Worksheets["ThongSo"];
					ws.Name = "ThongSo"; //Setting Sheet's name
					XL.ExportSheetThongSo1(ws, ngaydauthang, tableThongsoKetluongThang);
					#endregion

					#region ghi sheet chi tiết từng ngày công
					ws = workbook.Worksheets["BangChiTietNgayCong"];
					ws.Name = "BangChiTietNgayCong";
					//XL.ExportSheetChiTietNgayCong1(ws, dsnv);

					#endregion

					var ws_Template = workbook.Worksheets["BangChiTietLuong"];
					XL.ExportSheetChiTietLuong1(ws_Template, dsnv);

					while (@continue) {
						try {
							packageResult.SaveAs(fileResult);
							@continue = false;
						} catch (Exception ex) {
							lg.Error(string.Format("[{0}]_[{1}]\n", this.Name, System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
							if (ex is IOException) {
								MessageBox.Show(Resources.Text_FileDangMoBoiUngDungKhac, Resources.Caption_Loi);
								@continue = true;
							}
							else {
								MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
								@continue = false;
							}
						}
					}

				}


			} catch (Exception ex) {
				if (ex is System.Security.SecurityException || ex is UnauthorizedAccessException) {
					MessageBox.Show(string.Format(Resources.Text_KoCoQuyenTruyCapFileX, "mẫu xuất báo biểu"), Resources.Caption_Loi); return;
				}
				else if (ex is NotSupportedException || ex is PathTooLongException) {
					MessageBox.Show(string.Format(Resources.Text_UnsupportedFile_PathTooLong, "mẫu xuất báo biểu"), Resources.Caption_Loi); return;
				}
				else if (ex is IOException) {
					MessageBox.Show(Resources.Text_FileDangMoBoiUngDungKhac, Resources.Caption_Loi); return;
				}
				else
				{
					MessageBox.Show(Resources.Text_CoLoi, Resources.Caption_Loi);
					return;
				}


			}
		}


		private void ChuanBiDuLieuXuatLuong(List<cUserInfo> dsnv, DateTime ngaydauthang, DateTime ngaycuoithang,
			DataTable tableKetLuongThang, DataTable tableKetcongNgay, DataTable tableKetcongCa, DataTable tableXPVang, DataTable tableNgayLe,
			DataTable tableDSNVChiCongnhatThang) {
			#region lấy dữ liệu kết lương để xuất

			//var tableKetLuongThang = DAO.LayKetLuongThang(ngaydauthang);
			//var tableThongsoKetluongThang = DAO.LayThongsoKetluongThang(ngaydauthang);
			//var tableDSNVChiCongnhatThang = DAO.LayTableCongNhat(ngaydauthang);
			//var tableTongLuongCongnhat = SqlDataAccessHelper.ExecuteQueryString(
			//    @"select  CAST(SUM (SoNgayCong*DonGiaLuong) as float)  from DSNVChiCongNhatThang where Thang=@Thang",
			//    new string[] { "@Thang" }, new object[] { ngaydauthang });
			//var tongLuongCongnhat = (tableTongLuongCongnhat.Rows[0][0] != DBNull.Value) ? (double)tableTongLuongCongnhat.Rows[0][0] : 0d;
			//var tongLuongDieuchinh = (from DataRow row in tableKetLuongThang.Rows
			//                          let luongdieuchinh = (row["LuongDieuChinh"] != DBNull.Value)
			//                                                   ? (double)row["LuongDieuChinh"]
			//                                                   : 0d
			//                          select luongdieuchinh).Sum();
			//var tableKetcongNgay = DAO.LayKetcongNgay(ngaydauthang, ngaycuoithang);
			//var tableKetcongCa = DAO.LayKetcongCa(ngaydauthang, ngaycuoithang);
			//var tableXPVang = DAO.LayTableXPVang(ngaydauthang, ngaycuoithang);
			//var tableNgayLe = DAO.DocNgayLe(ngaydauthang, ngaycuoithang);

			#endregion

			#region tái tạo lại danh sách phòng ban của thời điểm kết lương để biết nv thuộc group phòng ban nào tại thời điểm kết lương

			var dsphongban = (from DataRow row in tableKetLuongThang.Rows
							  group row by new cPhongBan {
								  ID = (int)row["IDPhong"],
								  Ten = row["TenPhong"].ToString(),
								  ViTri = (int)row["ViTriPhong"],
								  idParent = (int)row["RelationIDDept"]
							  }
								  into @group
								  orderby @group.Key.ViTri
								  select @group.Key).ToList();

			foreach (var item in dsphongban) {
				// mỗi item tương ứng 1 phòng ban
				int? idParent = item.idParent;
				if (idParent != null) {
					cPhongBan parent = dsphongban.Find(o => o.ID == idParent); // info trường hợp idParent == 0 --> ko có phòng id=0 --> parent = null
					item.parent = parent;
				}
			}

			#endregion

			foreach (DataRow row in tableKetLuongThang.Rows) { //info chỉ lấy các nhân viên chính thức, vừa chính thức vừa công nhật, thiếu các nv công nhật

				cUserInfo nv = new cUserInfo();
				nv.MaCC = (int)row["UserEnrollNumber"];
				nv.DSNgayCong = new List<cNgayCong>();
				nv.DSVang = new List<cLoaiVang>();
				for (DateTime indexNgay = ngaydauthang; indexNgay <= ngaycuoithang; indexNgay = indexNgay.AddDays(1d)) {
					XL.LoadNgayCong(nv.MaCC, nv.DSNgayCong, indexNgay, tableKetcongNgay, tableKetcongCa);
				}
				XL.LoadDSXPVang_Le(nv.MaCC, tableXPVang, tableNgayLe, nv.DSVang);
				XL.PhanPhoi_DSVang7(nv.DSVang, nv.DSNgayCong);

				//kiểm tra nv đó có làm công nhật ko ? tìm nv chính thức trong dsnv công nhật
				// vì bảng kết lương chỉ bao gồm các nhân viên chính thức , các nhân viên công nhật ko chính thức sẽ ko có trong này
				XL.LoadThongtinLamViecCongNhat(nv.MaCC, ref nv.NgayBDCongnhat, ref nv.NgayKTCongnhat, ref nv.LoaiCN, nv.DSNgayCong, tableDSNVChiCongnhatThang);

				#region lấy thông tin cá nhân nv, phòng ban tổng công, phụ cấp, lương cơ bản, lương sản phẩm ... để xuất

				nv.MaNV = row["UserFullCode"].ToString();  //col 1
				nv.TenNV = row["UserFullName"].ToString();
				nv.HeSo = new HeSo { LuongCB = (float)row["HSLCB"], LuongCV = (float)row["HSLCV"], BHCongThem_ChoGD_PGD = (float)row["HSBHCongThem"],
                    LCBTT17 = (float)row["HSLCBTT17"], PCCVTT17 = (float)row["HSPCCV"], PCDHTT17 = (float)row["HSPCDH"], PCTNTT17 = (float)row["HSPCTN"]
                };
				nv.PhongBan = dsphongban.Find(o => o.ID == (int)row["IDPhong"]);
				nv.IDChucVu = (int)row["IDChucVu"];
				nv.ChucVu = row["ChucVu"].ToString();
				nv.ThongKeThang.Cong = (float)row["TongCong"];
				nv.ThongKeThang.Le = (float)row["TongLe"];
				nv.ThongKeThang.Phep = (float)row["TongPhep"];
				nv.ThongKeThang.H_CT_PT = (float)row["TongH_CT_PT"];
				nv.ThongKeThang.NgayQuaDem = (int)row["TongQuaDem"];
				nv.ThongKeThang.CongCV = (float)row["TongCongCV"];
				nv.ThongKeThang.BHXH = (float)row["TongBHXH"];
				nv.ThongKeThang.NghiRo = (float)row["TongRo"];
				nv.ThongKeThang.PTDT = (float)row["TongPTDT"];//DANGLAM
				nv.ThongKeThang.SoNgayNghiRO_NguyenNgay = (int)row["ViecRiengKoComTrua"];//ver 4.0.0.8
				nv.ThongKeThang.PhuCaps._30_dem = (float)row["TongPCDem"];
				nv.ThongKeThang.PhuCaps._50_TC = (float)row["TongPCTC"];
				nv.ThongKeThang.PhuCaps._100_TCC3 = (float)row["TongPCTC_Dem"];
				nv.ThongKeThang.PhuCaps._100_LVNN_Ngay = (float)row["TongPC200"];
				nv.ThongKeThang.PhuCaps._150_LVNN_Dem = (float)row["TongPC260"];
				nv.ThongKeThang.PhuCaps._200_LeTet_Ngay = (float)row["TongPC300"];
				nv.ThongKeThang.PhuCaps._250_LeTet_Dem = (float)row["TongPC390"];
				nv.ThongKeThang.PhuCaps._Cus = (float)row["TongPCCus"];
				nv.ThongKeThang.PhuCaps._TongPC = (float)row["TongPC"]; // tính toán dưới sql = tổng các loại phụ cấp, ko lưu dưới csdl
				nv.chiTietLuong.LCB_Theo.CongThucTe = (double)row["LuongCB_TheoCongThucTe"];
				nv.chiTietLuong.LCB_Theo.CheDoNghi = (double)row["LuongCB_TheoCheDoNghi"];
				nv.chiTietLuong.LCB_Theo.CongCV = (double)row["LuongCB_TheoCongCV"];
				nv.chiTietLuong.LCB_Theo.PhuCap = (double)row["PCLuongCB"]; // với 4 chi tiết lương này có thể tính được các tổng LCB
				nv.chiTietLuong.LSP_Theo.CongThucTe = (double)row["LuongSP_TheoCongThucTe"];
				nv.chiTietLuong.LSP_Theo.CheDoNghi = (double)row["LuongSP_TheoCheDoNghi"];
				nv.chiTietLuong.LSP_Theo.PhuCap = (double)row["PCLuongSP"]; // với 2 chi tiết lương này có thể tính được các tổng LSP
				nv.chiTietLuong.LuongDieuChinh = (double)row["LuongDieuChinh"];
				nv.chiTietLuong.KhauTru.TamUng = (double)row["TamUng"];
				nv.chiTietLuong.MucDongBHXH = (float)row["MucDongBHXH"];
				nv.chiTietLuong.KhauTru.BHXH = (double)row["KhauTruBHXH"];
				nv.chiTietLuong.KhauTru.ThuChiKhac = (double)row["ThuChiKhac"];
				nv.chiTietLuong.TienComTrua = (double)row["TienComTrua"];
				nv.chiTietLuong.ThucLanh = (double)row["ThucLanh"];

				#endregion

				dsnv.Add(nv);
			}
		}
	}





}
