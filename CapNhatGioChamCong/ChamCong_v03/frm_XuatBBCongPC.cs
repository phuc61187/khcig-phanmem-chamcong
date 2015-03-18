using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChamCong_v03.BUS;
using ChamCong_v03.DAO;
using ChamCong_v03.DTO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using log4net;

namespace ChamCong_v03 {
	public partial class frm_XuatBBCongPC : Form {
		private readonly ILog lg = LogManager.GetLogger("frm_XuatBBCongPC");

		public List<cUserInfo> m_dsnv;
		public DateTime ngayBD_Bef2D;
		public DateTime ngayKT_Aft2D;
		public frm_XuatBBCongPC() {
			log4net.Config.XmlConfigurator.Configure();

			InitializeComponent();
		}

		public void XuatBBCongVaPCThang(List<cUserInfo> m_DSNVXemCong, string file_path, DateTime ngaydauthang, DateTime ngaycuoithang, string TenNVNhapLieu, string TenTruongBP) {
			var songay = DateTime.DaysInMonth(ngaydauthang.Year, ngaydauthang.Month);
			var soNgayCN = XL.DemSoNgayCN(ngaydauthang);
			var CongChuan = Convert.ToDouble(songay - soNgayCN);

			using (var p = new ExcelPackage()) {
				#region //Create a sheet
				p.Workbook.Worksheets.Add("Bang Cham Cong Thang");
				var ws = p.Workbook.Worksheets[1];
				ws.Name = "Bang Cham Cong Thang"; //Setting Sheet's name
				ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
				ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

				#endregion
				var sR = 1;
				XL2.VeLogo("bbcongpcthang", ws);
				XL2.GhiThongTinTongcty(ws, "", 1, 11,12, 1, 33,12, 4, 33);
				int sttnv = 0, startCol = 1;
				SUMCC sum = new SUMCC();
				#region 1.tieu de bang cham cong tu ngay den ngay 2. bỏ 1 dòng trống

				sR = sR + 7;// sR+1 là dòng trống
				XL.FormatCells(ws.Cells[sR, 20], "BẢNG CHẤM CÔNG THÁNG " + ngaydauthang.ToString("MM/yyyy"),
					size: 12, hAlign: ExcelHorizontalAlignment.CenterContinuous, bold: true, VeBorder: false);
				#endregion

				sR++;
				sR++;
				#region  write stt +0
				ws.Column(startCol).Width = (int)CC.STT;
				XL.FormatCells(ws.Cells[sR, startCol, sR + 1, startCol], "STT", merge: true, wraptext: true, textRotation: 90, bold: true);
				startCol++;
				#endregion
				#region  write ho ten +1
				ws.Column(startCol).Width = (int)CC.ten;
				XL.FormatCells(ws.Cells[sR, startCol, sR + 1, startCol], "Họ tên", merge: true, bold: true);
				startCol++;
				#endregion
				#region write manv +2
				ws.Column(startCol).Width = (int)CC.manv;
				XL.FormatCells(ws.Cells[sR, startCol, sR + 1, startCol], "Mã NV", merge: true, wraptext: true, bold: true);
				ws.Cells[sR, startCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				startCol++;
				#endregion
				//startCol = 4

				#region ghi TIEU DE ngay m_thang, TIEU DE cac cot thong ke, sau khi ghi thì nhớ tăng 2 sau dòng tiêu đề, xem cuối region này
				XL.FormatCells(ws.Cells[sR, startCol, sR, startCol + songay - 1], "Ngày", merge: true, bold: true);

				for (var dem = ngaydauthang; dem <= ngaycuoithang; dem = dem.AddDays(1d), startCol = startCol + 1) {
					ws.Column(startCol).Width = (int)CC.kyhieuchamcong;

					XL.FormatCells(ws.Cells[sR + 1, startCol], dem, numberFormat: "d", bold: true);
				}
				// RA KHỎI VÒNG LẶP, START COL LÀ VỊ TRÍ CỘT TỔNG CÔNG, TIẾP TỤC TĂNG CỘT SAU MỖI LẦN GHI

				XL.FormatCells(ws.Cells[sR, startCol, sR, startCol + 5], "Ngày công chuẩn", merge: true, bold: true);
				XL.FormatCells(ws.Cells[sR, startCol + 6], (songay - soNgayCN));
				XL.FormatCells(ws.Cells[sR, startCol + 7, sR, startCol + 14], "Phụ cấp", merge: true, bold: true);

				#region tiêu đề các cột thống kê

				ws.Column(startCol).Width = (int)CC.cong;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "Công", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.Le;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "Lễ 100%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.Phep;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "Phép CĐ", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.cv;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "CV", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.bh;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "BHXH", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.hoc;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "Học, họp, PT", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.tongcong;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "TC", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.ca130;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PC Ca3 30%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.ca150;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PCTC 50%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.tcc3_100;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PCTC Ca 3 100%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.LVNN;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PC LVNN 100%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.pcCa3LVNN;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PC LVNN Ca3 160%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.pcle_tet;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PC Lễ, tết 200%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.pcCa3_le;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PC lễ, tết ca 3 290%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.pckhac;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PC khác", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.nghiRo;
				XL.FormatCells(ws.Cells[sR, startCol, sR + 1, startCol], "Nghỉ Ro", wraptext: true, merge: true, bold: true);
				startCol++;

				#endregion


				//goto ab;

				sR = sR + 1; // 2 dòng tiêu đề stt, ngày, tổng công....
				#endregion

				sR++;
				var pb_c1 = (from item in m_DSNVXemCong
							 select new { item.PBCap1.ID, item.PBCap1.TenPhongBan, item.PBCap1.ViTri }).Distinct().ToList().OrderBy(item => item.ViTri);

				try
				{
					var startColThongKe = 0;
					foreach (var pb in pb_c1) {
						#region //write ten pb cap 1 tang 1 dong
						XL.FormatCells(ws.Cells[sR, 1], pb.TenPhongBan, bold: true, VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
						sR++;
						#endregion
						var dsnv = (from c in m_DSNVXemCong
									where c.PBCap1.ID == pb.ID
									select c).ToList();
						var iNgay = 0;
						foreach (var nhanvien in dsnv) {
							// duyet tung nv, nhớ tăng sR
							startCol = 1;//reset startcol =1 mỗi lần ghi nv mới
							#region  stt,hoten,macc
							sttnv++;
							XL.FormatCells(ws.Cells[sR, startCol], sttnv);
							XL.FormatCells(ws.Cells[sR, startCol + 1], nhanvien.TenNV, hAlign: ExcelHorizontalAlignment.Left); //Ten
							XL.FormatCells(ws.Cells[sR, startCol + 2], nhanvien.MaNV);//MaNV
							startCol = startCol + 3; // + 4 là 4 cột, nếu thêm 1 cột thì +5, +6
							#endregion

							#region write cong, pc

							Double tongcongvantay = 0d, tongTreSomTruCV = 0d, tongpc30 = 0d, tongpc50 = 0d, tongpctcc3 = 0d,
								tongpc100 = 0d, tongpc160 = 0d, tongpc200 = 0d, tongpc290 = 0d, tongpckhac = 0d,
								tong_h_pt_ct = 0d, tongP = 0d, tongBH = 0d, tongRo = 0d, tongCV_khaibao = 0d, tongCV = 0d, tongLe = 0d;
							for (iNgay = 2; iNgay < nhanvien.DSNgayCong.Count - 2; iNgay++) {// startCol += 2 chuyên vào trong hàm vì ngay đầu, ngày cuối bỏ qua nhưng nếu để đây thì vẫn tăng 2 --> 2 ô đầu + 2 ô cuối

								var ngayCong = nhanvien.DSNgayCong[iNgay];
								tongcongvantay = tongcongvantay + ngayCong.TongCong; // chỉ + công ngày nào có check auto hoặc veri, chưa cộng công vắng phép, học,họp,nên phải cộng
								tongTreSomTruCV += ngayCong.TruCongCV;
								if (ngayCong.TinhPCDB) {
									if (ngayCong.LoaiPCDB == 200) {
										tongpc100 += ngayCong.PhuCap100;
										tongpc160 += ngayCong.PhuCap160;
									}
									if (ngayCong.LoaiPCDB == 300) {
										tongpc200 += ngayCong.PhuCap200;
										tongpc290 += ngayCong.PhuCap290;
									}
									if (ngayCong.LoaiPCDB == 1 || ngayCong.LoaiPCDB == 2) {
										tongpckhac += ngayCong.PhuCapCus;
									}
								}
								else {
									if (ngayCong.QuaDem)
										tongpc30 += ngayCong.PhuCap30;
									if (ngayCong.TinhPC50)
										tongpc50 += ngayCong.PhuCap50;
									if (ngayCong.TinhPC50 && ngayCong.QuaDem)
										tongpctcc3 += ngayCong.PhuCapTCC3;
								}


								if (ngayCong.DSVang != null) {
									foreach (var loaiVang in ngayCong.DSVang) {
										#region thống kê lại các loại vắng, chưa cộng vào công vì để cuối cùng rồi cộng
										switch (loaiVang.KyHieu) {
											case "P":
												tongP = tongP + loaiVang.Cong;
												break;
											case "CV":
												tongCV_khaibao = tongCV_khaibao + loaiVang.Cong;
												break;
											case "BH":
											case "TS":
												tongBH = tongBH + loaiVang.Cong;
												break;
											case "PT":
											case "H":
											case "CT":
												tong_h_pt_ct = tong_h_pt_ct + loaiVang.Cong;
												break;
											case "RO":
												tongRo = tongRo + loaiVang.Cong;
												break;
											case "L":
												tongLe = tongLe + loaiVang.Cong;
												break;
										}

										#endregion
									}
								}

								var content1 = string.Empty;
								ngayCong.XuatChuoiKyHieuChamCong(ref content1);
								var color = 0;
								if (ngayCong.Ngay.DayOfWeek == DayOfWeek.Saturday) color = 7;
								else if (ngayCong.Ngay.DayOfWeek == DayOfWeek.Sunday) color = 8;
								XL.FormatCells(ws.Cells[sR, startCol], content1, color:color, hAlign: ExcelHorizontalAlignment.Left);

								startCol++;
							}
							#endregion

							tongcongvantay = Math.Round(tongcongvantay, 2);
							var tong = tongcongvantay + tongTreSomTruCV + tongP + tongLe + tong_h_pt_ct + tongCV_khaibao
								+ tongBH + tongRo;
							if (tong < CongChuan) //tong >= CongChuan thì nv.TkThang.TongCongCV giữ nguyên = nv.TkThang.TongCongCV;
								tongCV = tongCV_khaibao + (CongChuan - tong);
							else
								tongCV = tongCV_khaibao;
							var tongCongthehien = tongcongvantay + tongLe + tongP + tongCV + tongBH + tong_h_pt_ct;

							#region write thong ke

							startColThongKe = startCol;

							XL.FormatCells(ws.Cells[sR, startCol], tongcongvantay, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng công 1
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongLe, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng Lễ 3
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongP, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng phép 4
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongCV, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng CV 5
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongBH, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng BH,TS 6
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tong_h_pt_ct, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng HHPT 7
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongCongthehien, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng công 8
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpc30, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng PC 30 2
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpc50, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng pc150 9
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpctcc3, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng pctcc3 10
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpc100, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng pc100 10
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpc160, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng pc160 10
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpc200, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng pc200 10
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpc290, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng pc260 11
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpckhac, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng pc260 11
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongRo, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng RO 12
							startCol++;

							sum.cong1 += tongcongvantay;
							sum.le2 += tongLe;
							sum.phep3 += tongP;
							sum.cv4 += sum.cv4;
							sum.bhxh5 += tongBH;
							sum.hoc6 += tong_h_pt_ct;
							sum.tongcong7 += tongCongthehien;
							sum.pc30_8 += tongpc30;
							sum.pc50_9 += tongpc50;
							sum.pctcc3_10 += tongpctcc3;
							sum.pc100_11 += tongpc100;
							sum.pc160_12 += tongpc160;
							sum.pc200_13 += tongpc200;
							sum.pc290_14 += tongpc290;
							sum.pckhac_15 += tongpckhac;
							sum.nghiRO_16 += tongRo;

							#endregion

							sR++;
						}
					}
					var temp = startColThongKe - 2;
					XL.FormatOneCell(ws, ref sR, ref temp, "Tổng cộng", increaseCol:1, VeBorder:false, bold:true, hAlign:ExcelHorizontalAlignment.Left);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.cong1, increaseCol:1, numberFormat:"###,##0.0#;-0;-?;", bold:true, hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.le2, increaseCol: 1, numberFormat: "###,##0.0;-0;-?;", bold: true, hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.phep3, increaseCol: 1, numberFormat: "###,##0.0;-0;-?;", bold: true, hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.cv4, increaseCol: 1, numberFormat: "###,##0.0;-0;-?;", bold: true, hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.bhxh5, increaseCol: 1, numberFormat: "###,##0.0;-0;-?;", bold: true, hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.hoc6, increaseCol: 1, numberFormat: "###,##0.0;-0;-?;", bold: true, hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.tongcong7, increaseCol: 1, numberFormat: "###,##0.0#;-0;-?;", bold: true, hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.pc30_8, increaseCol: 1, numberFormat: "###,##0.0#;-0;-?;", bold: true, hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.pc50_9, increaseCol: 1, numberFormat: "###,##0.0#;-0;-?;", bold: true, hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.pctcc3_10, increaseCol: 1, numberFormat: "###,##0.0#;-0;-?;", bold: true, hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.pc100_11, increaseCol: 1, numberFormat: "###,##0.0#;-0;-?;", bold: true, hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.pc160_12, increaseCol: 1, numberFormat: "###,##0.0#;-0;-?;", bold: true, hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.pc200_13, increaseCol: 1, numberFormat: "###,##0.0#;-0;-?;", bold: true, hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.pc290_14, increaseCol: 1, numberFormat: "###,##0.0#;-0;-?;", bold: true, hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.pckhac_15, increaseCol: 1, numberFormat: "###,##0.0#;-0;-?;", bold: true, hAlign: ExcelHorizontalAlignment.Right);
					XL.FormatOneCell(ws, ref sR, ref startColThongKe, sum.nghiRO_16, increaseCol: 1, numberFormat: "###,##0.0;-0;-?;", bold: true, hAlign: ExcelHorizontalAlignment.Right);

					sR++;
					XL.FormatCells(ws.Cells[sR, 2], "Ghi chú ký hiệu: ", VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);

					var tempsR = 0;
					foreach (var caChuan in XL.DSCa) {
						var tempstr = "{0} : {1}";
						tempstr = string.Format(tempstr, caChuan.KyHieuCC, caChuan.Code);
						XL.FormatCells(ws.Cells[sR + tempsR, 4], tempstr, VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
						tempsR++;
					}
					XL.FormatCells(ws.Cells[sR + tempsR, 4], "8 : Ca tự do 8 tiếng", VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
					tempsR++;
					XL.FormatCells(ws.Cells[sR + tempsR, 4], "D : Ca dài 12 tiếng", VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
					tempsR++;
					XL.FormatCells(ws.Cells[sR + tempsR, 4], "XN- : Ca đã qua xác nhận", VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
					tempsR++;

					var tableLoaiVang = DAL.LayDSLoaiVang();
					var tempsR2 = 0;
					foreach (DataRow row in tableLoaiVang.Rows) {
						var tempstr = "{0}";
						tempstr = string.Format(tempstr, row["AbsentDescription"]);
						XL.FormatCells(ws.Cells[sR + tempsR2, 11], tempstr, VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
						tempsR2++;
					}
					XL.FormatCells(ws.Cells[sR + tempsR2, 11], "(+) : Có Phụ cấp tăng cường 50%", VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
					tempsR2++;
					XL.FormatCells(ws.Cells[sR + tempsR2, 11], "(+1) : Có Phụ cấp tăng cường ca 3 100%", VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
					tempsR2++;
					XL.FormatCells(ws.Cells[sR + tempsR2, 11], "(X2N) : Có Phụ cấp làm việc ngày nghỉ ban ngày 100%", VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
					tempsR2++;
					XL.FormatCells(ws.Cells[sR + tempsR2, 11], "(X2D) : Có Phụ cấp làm việc ngày nghỉ ban đêm 150%", VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
					tempsR2++;
					XL.FormatCells(ws.Cells[sR + tempsR2, 11], "(X3N) : Có Phụ cấp trực lễ, tết ban ngày 200% (chưa kể công lễ, tết)", VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
					tempsR2++;
					XL.FormatCells(ws.Cells[sR + tempsR2, 11], "(X3D) : Có Phụ cấp trực lễ, tết ban đêm 250% (chưa kể công lễ, tết)", VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
					tempsR2++;

					sR = sR + tempsR;

					sR += 2;

					var colPGiamDoc = 3;
					var colTRUONG = songay;
					var colLapBieu = songay + 10;

					XL.FormatCells(ws.Cells[sR, colPGiamDoc], "PHÓ GIÁM ĐỐC", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
					XL.FormatCells(ws.Cells[sR, colTRUONG], "TRƯỞNG BỘ PHẬN", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
					XL.FormatCells(ws.Cells[sR, colLapBieu], "LẬP BIỂU", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);

					sR += 7;

					XL.FormatCells(ws.Cells[sR, colPGiamDoc], "Trần Hồng Sơn", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
					XL.FormatCells(ws.Cells[sR, colTRUONG], TenTruongBP, VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
					XL.FormatCells(ws.Cells[sR, colLapBieu], TenNVNhapLieu, VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
				} catch (Exception exception) {
					lg.Error("XuatBBCongVaPCThang", exception);
					throw exception;
				}

				#region Ghi file , nếu xảy ra lỗi thì báo

				Byte[] bin = p.GetAsByteArray();
				try {
					File.WriteAllBytes(file_path, bin);
					AutoClosingMessageBox.Show("Xuất báo biểu thành công.", "Thông báo", 2000);
				} catch (Exception exception) {
					lg.Error("XuatBBCongVaPCThang", exception);
					if (exception is UnauthorizedAccessException) MessageBox.Show("Bạn chưa được cấp quyền ghi file vào folder.", "Lỗi");
					else if (exception is DirectoryNotFoundException) MessageBox.Show("Không tìm thấy folder lưu trữ.", "Lỗi");
					else if (exception is IOException) MessageBox.Show("File đang mở bởi ứng dụng khác.", "Lỗi");
					else MessageBox.Show("Không thể ghi được file báo biểu.", "Lỗi");
				}

				#endregion

			}

		}
		public void XuatBBCongVaPCTuNgayDenNgay(List<cUserInfo> m_DSNVXemCong, string file_path, DateTime ngaybd, DateTime ngaykt, string TenNVNhapLieu, string TenTruongBP) {
			var songay = (ngaykt - ngaybd).Days;

			using (var p = new ExcelPackage()) {
				#region //Create a sheet
				p.Workbook.Worksheets.Add("Bang Cham Cong Chi Tiet");
				var ws = p.Workbook.Worksheets[1];
				ws.Name = "Bang Cham Cong Chi Tiet"; //Setting Sheet's name
				ws.Cells.Style.Font.Size = 10; //Default font size for whole sheet
				ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

				#endregion
				var sR = 1;
				XL2.VeLogo("cong tu ngay den ngay", ws);
				XL2.GhiThongTinTongcty(ws, "", 1, 11,12, 1, 33,12, 4, 33);
				int sttnv = 0, startCol = 1;
				#region 1.tieu de bang cham cong tu ngay den ngay 2. bỏ 1 dòng trống

				sR = sR + 7;// sR+1 là dòng trống
				XL.FormatCells(ws.Cells[sR, 20], string.Format("BẢNG CHẤM CÔNG TỪ NGÀY {0} ĐẾN NGÀY {1} ", ngaybd.ToString("dd/MM/yyyy"), ngaykt.ToString("dd/MM/yyyy")),
					size: 14, hAlign: ExcelHorizontalAlignment.CenterContinuous, bold: true, VeBorder: false);
				#endregion

				sR++;
				sR++;
				#region  write stt +0
				ws.Column(startCol).Width = (int)CC.STT;
				XL.FormatCells(ws.Cells[sR, startCol, sR + 1, startCol], "STT", merge: true, wraptext: true, textRotation: 90, bold: true);
				startCol++;
				#endregion
				#region  write ho ten +1
				ws.Column(startCol).Width = (int)CC.ten;
				XL.FormatCells(ws.Cells[sR, startCol, sR + 1, startCol], "Họ tên", merge: true, bold: true);
				startCol++;
				#endregion
				#region write manv +2
				ws.Column(startCol).Width = (int)CC.manv;
				XL.FormatCells(ws.Cells[sR, startCol, sR + 1, startCol], "Mã NV", merge: true, wraptext: true, bold: true);
				ws.Cells[sR, startCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				startCol++;
				#endregion
				//startCol = 4

				#region ghi TIEU DE ngay m_thang, TIEU DE cac cot thong ke, sau khi ghi thì nhớ tăng 2 sau dòng tiêu đề, xem cuối region này
				XL.FormatCells(ws.Cells[sR, startCol, sR, startCol + (songay * 3) + 2], "Ngày", merge: true, bold: true); // cộng 2 vì số ngày * 3 thì nó merger tới cột đầu tiên của ngày cuối mà ngày cuối 3 cột nên phải cộng thêm khoảng 2

				for (var dem = ngaybd; dem <= ngaykt; dem = dem.AddDays(1d), startCol = startCol + 3) {
					ws.Column(startCol + 0).Width = (int)CC.congtrongngay;
					ws.Column(startCol + 1).Width = (int)CC.pctrongngay;
					ws.Column(startCol + 2).Width = (int)CC.kyhieuvang;

					XL.FormatCells(ws.Cells[sR + 1, startCol, sR + 1, startCol + 2], dem, numberFormat: "d/M ddd", merge: true, bold: true);
				}
				// RA KHỎI VÒNG LẶP, START COL LÀ VỊ TRÍ CỘT TỔNG CÔNG, TIẾP TỤC TĂNG CỘT SAU MỖI LẦN GHI

				XL.FormatCells(ws.Cells[sR, startCol, sR, startCol + 6], "Tổng kết", merge: true, bold: true);
				XL.FormatCells(ws.Cells[sR, startCol + 7, sR, startCol + 14], "Phụ cấp", merge: true, bold: true);

				#region tiêu đề các cột thống kê

				ws.Column(startCol).Width = (int)CC.cong;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "Công", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.Le;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "Lễ 100%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.Phep;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "Phép CĐ", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.cv;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "CV", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.bh;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "BHXH", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.hoc;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "Học, họp, PT", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.tongcong;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "TC", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.ca130;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PC Ca3 30%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.ca150;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PCTC 50%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.tcc3_100;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PCTC Ca3 100%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.LVNN;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PC LVNN 100%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.pcCa3LVNN;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PC LVNN Ca 3 160%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.pcle_tet;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PC trực lễ, tết 200%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.pcCa3_le;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PC trực lễ, tết ca 3 290%", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.pckhac;
				XL.FormatCells(ws.Cells[sR + 1, startCol], "PC khác", wraptext: true, bold: true);
				startCol++;

				ws.Column(startCol).Width = (int)CC.nghiRo;
				XL.FormatCells(ws.Cells[sR, startCol, sR + 1, startCol], "Nghỉ Ro", wraptext: true, bold: true, merge: true);
				startCol++;

				#endregion


				//goto ab;

				sR = sR + 1; // 2 dòng tiêu đề stt, ngày, tổng công....
				#endregion

				sR++;

				var pb_c1 = (from item in m_DSNVXemCong
							 select new { item.PBCap1.ID, item.PBCap1.TenPhongBan, item.PBCap1.ViTri }).Distinct().ToList().OrderBy(item => item.ViTri);


				try {

					foreach (var pb in pb_c1) {
						#region //write ten pb cap 1 tang 1 dong
						XL.FormatCells(ws.Cells[sR, 1], pb.TenPhongBan, bold: true, VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
						sR++;
						#endregion
						var dsnv = (from c in m_DSNVXemCong
									where c.PBCap1.ID == pb.ID
									select c).ToList();
						var iNgay = 0;
						foreach (var nhanvien in dsnv) {
							// duyet tung nv, nhớ tăng sR
							startCol = 1;//reset startcol =1 mỗi lần ghi nv mới
							#region  stt,hoten,macc
							sttnv++;
							XL.FormatCells(ws.Cells[sR, startCol], sttnv);
							XL.FormatCells(ws.Cells[sR, startCol + 1], nhanvien.TenNV, hAlign: ExcelHorizontalAlignment.Left); //Ten
							XL.FormatCells(ws.Cells[sR, startCol + 2], nhanvien.MaNV);//MaNV
							startCol = startCol + 3; // + 4 là 4 cột, nếu thêm 1 cột thì +5, +6
							#endregion

							#region write cong, pc

							Double tongcongvantay = 0d, tongpc30 = 0d, tongpc50 = 0d, tongpctcc3 = 0d,
								tongpc100 = 0d, tongpc160 = 0d, tongpc200 = 0d, tongpc290 = 0d, tongpckhac = 0d,
								tong_h_pt_ct = 0d, tongP = 0d, tongBH = 0d, tongRo = 0d, tongCV_khaibao = 0d, tongLe = 0d;

							for (iNgay = 2; iNgay < nhanvien.DSNgayCong.Count - 2; iNgay++) {// startCol += 2 chuyên vào trong hàm vì ngay đầu, ngày cuối bỏ qua nhưng nếu để đây thì vẫn tăng 2 --> 2 ô đầu + 2 ô cuối

								var ngayCong = nhanvien.DSNgayCong[iNgay];
								tongcongvantay = tongcongvantay + ngayCong.TongCong; // chỉ + công ngày nào có check auto hoặc veri, chưa cộng công vắng phép, học,họp,nên phải cộng

								#region thống kê phụ cấp

								if (ngayCong.TinhPCDB) {
									if (ngayCong.LoaiPCDB == 200) {
										tongpc100 += ngayCong.PhuCap100;
										tongpc160 += ngayCong.PhuCap160;
									}
									if (ngayCong.LoaiPCDB == 300) {
										tongpc200 += ngayCong.PhuCap200;
										tongpc290 += ngayCong.PhuCap290;
									}
									if (ngayCong.LoaiPCDB == 1 || ngayCong.LoaiPCDB == 2) {
										tongpckhac += ngayCong.PhuCapCus;
									}
								}
								else {
									if (ngayCong.QuaDem)
										tongpc30 += ngayCong.PhuCap30;
									if (ngayCong.TinhPC50)
										tongpc50 += ngayCong.PhuCap50;
									if (ngayCong.TinhPC50 && ngayCong.QuaDem)
										tongpctcc3 += ngayCong.PhuCapTCC3;
								}


								#endregion

								if (ngayCong.DSVang != null) {
									foreach (var loaiVang in ngayCong.DSVang) {
										#region thống kê lại các loại vắng, chưa cộng vào công vì để cuối cùng rồi cộng
										switch (loaiVang.KyHieu) {
											case "P":
												tongP = tongP + loaiVang.Cong;
												break;
											case "CV":
												tongCV_khaibao = tongCV_khaibao + loaiVang.Cong;
												break;
											case "BH":
											case "TS":
												tongBH = tongBH + loaiVang.Cong;
												break;
											case "PT":
											case "H":
											case "CT":
												tong_h_pt_ct = tong_h_pt_ct + loaiVang.Cong;
												break;
											case "RO":
												tongRo = tongRo + loaiVang.Cong;
												break;
											case "L":
												tongLe = tongLe + loaiVang.Cong;
												break;
										}

										#endregion
									}
								}

								var content1 = ngayCong.TongCong;
								var content2 = ngayCong.TongPhuCap;
								var content3 = ngayCong.Absents_Code();
								XL.FormatCells(ws.Cells[sR, startCol + 0], content1, numberFormat: "0.0?;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
								XL.FormatCells(ws.Cells[sR, startCol + 1], content2, numberFormat: "0.0?;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);
								XL.FormatCells(ws.Cells[sR, startCol + 2], content3, hAlign: ExcelHorizontalAlignment.Right);

								startCol = startCol + 3;
							}
							#endregion

							tongcongvantay = Math.Round(tongcongvantay, 2);
							var tong = tongcongvantay + tongP + tongLe + tong_h_pt_ct + tongCV_khaibao
								+ tongBH + tongRo;

							#region write thong ke
							XL.FormatCells(ws.Cells[sR, startCol], tongcongvantay, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng công 1
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongLe, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng Lễ 3
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongP, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng phép 4
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongCV_khaibao, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng CV 5
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongBH, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng BH,TS 6
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tong_h_pt_ct, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng HHPT 7
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tong, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng công 8
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpc30, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng PC 30 2
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpc50, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng pc150 9
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpctcc3, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng pc200 10
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpc100, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng pc200 10
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpc160, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng pc200 10
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpc200, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng pc200 10
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpc290, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng pc260 11
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpckhac, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng pc260 11
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongRo, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng RO 12
							startCol++;
							#endregion

							sR++;
						}
					}

					sR += 2;

					var colLapBieu = 3;
					var colTRUONG = songay;
					var colPGiamDoc = songay + 10;

					XL.FormatCells(ws.Cells[sR, colLapBieu], "LẬP BIỂU", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
					XL.FormatCells(ws.Cells[sR, colTRUONG], "TRƯỞNG BỘ PHẬN", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
					XL.FormatCells(ws.Cells[sR, colPGiamDoc], "PHÓ GIÁM ĐỐC", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);

					sR += 7;

					XL.FormatCells(ws.Cells[sR, colLapBieu], TenNVNhapLieu, VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
					XL.FormatCells(ws.Cells[sR, colTRUONG], TenTruongBP, VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);
					XL.FormatCells(ws.Cells[sR, colPGiamDoc], "Trần Hồng Sơn", VeBorder: false, bold: true, hAlign: ExcelHorizontalAlignment.CenterContinuous);

				} catch (Exception exception) {
					lg.Error("XuatBBCongVaPCTuNgayDenNgay", exception);
					throw exception;
				}

				#region Ghi file , nếu xảy ra lỗi thì báo

				Byte[] bin = p.GetAsByteArray();
				try {
					File.WriteAllBytes(file_path, bin);
					AutoClosingMessageBox.Show("Xuất báo biểu thành công.", "Thông báo", 2000);
				} catch (Exception exception) {
					lg.Error("XuatBBCongVaPCTuNgayDenNgay", exception);
					if (exception is UnauthorizedAccessException) MessageBox.Show("Bạn chưa được cấp quyền ghi file vào folder.", "Lỗi");
					else if (exception is DirectoryNotFoundException) MessageBox.Show("Không tìm thấy folder lưu trữ.", "Lỗi");
					else if (exception is IOException) MessageBox.Show("File đang mở bởi ứng dụng khác.", "Lỗi");
					else MessageBox.Show("Không thể ghi được file báo biểu.", "Lỗi");
				}

				#endregion

			}

		}

		public void XuatBBCongVaPC(List<cUserInfo> m_DSNVXemCong, string file_path, DateTime thang) {
			var songay = DateTime.DaysInMonth(thang.Year, thang.Month);
			var t1 = new DateTime(thang.Year, thang.Month, 1);
			var t2 = new DateTime(thang.Year, thang.Month, songay);

			using (var p = new ExcelPackage()) {
				#region //Create a sheet
				p.Workbook.Worksheets.Add("Bang Cham Cong");
				var ws = p.Workbook.Worksheets[1];
				ws.Name = "Bang Cham Cong"; //Setting Sheet's name
				ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
				ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

				#endregion
				var sR = 1;

				int sttnv = 1, startCol = 1;
				#region 1.tieu de bang cham cong tu ngay den ngay 2. bỏ 1 dòng trống

				ws.Cells[sR, 20].Value = "Bảng chấm công từ tháng " + thang.ToString("MM/yyyy");
				ws.Cells[sR, 20].Style.Font.Size = 14;
				ws.Cells[sR, 20].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
				sR = sR + 2;// sR+1 là dòng trống
				#endregion

				#region format 2 dòng 1.ngày , dòng dưới thứ là BOLD, rồi ghi STT, họ tên, ..., TĂNG COLUMN
				ws.Cells[sR, 1, sR, songay + songay].Style.Font.Bold = true; // bold header cell 2 ô; //bỏ 2 ô sr+1,
				#endregion
				#region  write stt +0
				ws.Column(startCol).Width = (int)CC.STT;
				XL.FormatCells(ws.Cells[sR, startCol], "STT", wraptext: true, textRotation: 90);
				startCol++;
				#endregion
				#region  write ho ten +1
				ws.Column(startCol).Width = (int)CC.ten;
				XL.FormatCells(ws.Cells[sR, startCol], "Họ tên");
				startCol++;
				#endregion
				#region write manv +2
				ws.Column(startCol).Width = (int)CC.manv;
				XL.FormatCells(ws.Cells[sR, startCol], "Mã NV", wraptext: true);
				ws.Cells[sR, startCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				startCol++;
				#endregion
				//startCol = 4

				#region ghi TIEU DE ngay m_thang, TIEU DE cac cot thong ke, sau khi ghi thì nhớ tăng 2 sau dòng tiêu đề, xem cuối region này

				for (var dem = ngayBD_Bef2D.Date.AddDays(2d); dem <= ngayKT_Aft2D.Date.AddDays(-2d); dem = dem.AddDays(1d), startCol = startCol + 1) {
					ws.Column(startCol).Width = (int)CC.kyhieuchamcong;

					XL.FormatCells(ws.Cells[sR, startCol], dem, numberFormat: "d");
				}
				// RA KHỎI VÒNG LẶP, START COL LÀ VỊ TRÍ CỘT TỔNG CÔNG, TIẾP TỤC TĂNG CỘT SAU MỖI LẦN GHI
				ws.Column(startCol).Width = (int)CC.cong;
				XL.FormatCells(ws.Cells[sR, startCol], "Ca 100%");
				startCol++;

				ws.Column(startCol).Width = (int)CC.ca130;
				XL.FormatCells(ws.Cells[sR, startCol], "Ca 130%");
				startCol++;

				ws.Column(startCol).Width = (int)CC.Le;
				XL.FormatCells(ws.Cells[sR, startCol], "Lễ 100%");
				startCol++;

				ws.Column(startCol).Width = (int)CC.Phep;
				XL.FormatCells(ws.Cells[sR, startCol], "Phép CĐ");
				startCol++;

				ws.Column(startCol).Width = (int)CC.cv;
				XL.FormatCells(ws.Cells[sR, startCol], "CV");
				startCol++;

				ws.Column(startCol).Width = (int)CC.bh;
				XL.FormatCells(ws.Cells[sR, startCol], "BHXH");
				startCol++;

				ws.Column(startCol).Width = (int)CC.hoc;
				XL.FormatCells(ws.Cells[sR, startCol], "Học, họp, PT");
				startCol++;


				//goto ab;

				sR = sR + 2; // 2 dòng tiêu đề stt, ngày, tổng công....
				#endregion
				var pb_c1 = (from item in m_DSNVXemCong
							 select new { item.PBCap1.ID, item.PBCap1.TenPhongBan, item.PBCap1.ViTri }).Distinct().ToList().OrderBy(item => item.ViTri);

				try {

					foreach (var pb in pb_c1) {
						#region //write ten pb cap 1 tang 1 dong

						XL.FormatCells(ws.Cells[sR, 1], pb.TenPhongBan, bold: true, hAlign: ExcelHorizontalAlignment.Left);
						sR++;
						#endregion
						var dsnv = (from c in m_DSNVXemCong
									where c.PBCap1.ID == pb.ID
									select c).ToList();
						int iNgay = 0;
						foreach (cUserInfo nhanvien in dsnv) {
							// duyet tung nv, nhớ tăng sR
							startCol = 1;//reset startcol =1 mỗi lần ghi nv mới
							#region  stt,hoten,macc
							ws.Cells[sR, startCol].Value = sttnv; // stt start từ 1, nhớ +1 sau khi ghi vì đây là lặp for each
							sttnv++;
							XL.FormatCells(ws.Cells[sR, startCol], sttnv);
							XL.FormatCells(ws.Cells[sR, startCol + 1], nhanvien.TenNV, hAlign: ExcelHorizontalAlignment.Left); //Ten
							XL.FormatCells(ws.Cells[sR, startCol + 2], nhanvien.MaNV);//MaNV
							startCol = startCol + 3; // + 4 là 4 cột, nếu thêm 1 cột thì +5, +6
							#endregion

							#region write cong, pc

							Double tongcongthang = 0d, tongpc = 0d, tong_h_pt_ct = 0d, tongP = 0d, tongBH = 0d, tongRo = 0d, tongCV = 0d, tongLe = 0d;
							for (iNgay = 2; iNgay < nhanvien.DSNgayCong.Count - 2; iNgay++) {// startCol += 2 chuyên vào trong hàm vì ngay đầu, ngày cuối bỏ qua nhưng nếu để đây thì vẫn tăng 2 --> 2 ô đầu + 2 ô cuối

								var ngayCong = nhanvien.DSNgayCong[iNgay];
								tongcongthang = tongcongthang + ngayCong.TongCong; // chỉ + công ngày nào có check auto hoặc veri, chưa cộng công vắng phép, học,họp,nên phải cộng
								tongpc = tongpc + ngayCong.TongPhuCap;
								if (ngayCong.DSVang != null) {
									foreach (var loaiVang in ngayCong.DSVang) {
										#region thống kê lại các loại vắng, chưa cộng vào công vì để cuối cùng rồi cộng
										switch (loaiVang.KyHieu) {
											case "P":
												tongP = tongP + loaiVang.Cong;
												break;
											case "CV":
												tongCV = tongCV + loaiVang.Cong;
												break;
											case "BH":
											case "TS":
												tongBH = tongBH + loaiVang.Cong;
												break;
											case "PT":
											case "H":
											case "CT":
												tong_h_pt_ct = tong_h_pt_ct + loaiVang.Cong;
												break;
											case "RO":
												tongRo = tongRo + loaiVang.Cong;
												break;
											case "L":
												tongLe = tongLe + loaiVang.Cong;
												break;
										}

										#endregion
									}
								}

								var content1 = string.Empty;
								ngayCong.XuatChuoiKyHieuChamCong(ref content1);
								XL.FormatCells(ws.Cells[sR, startCol], content1, hAlign: ExcelHorizontalAlignment.Left);

								startCol++;
							}
							#endregion

							tongcongthang = tongcongthang + tong_h_pt_ct + tongP + tongLe;

							#region write thong ke
							XL.FormatCells(ws.Cells[sR, startCol], tong_h_pt_ct, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng HHPT
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongP, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng phép
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongBH, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng BH,TS
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongRo, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng RO
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongCV, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng CV
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongLe, numberFormat: "#0.0;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng Lễ
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongcongthang, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng công tháng
							startCol++;
							XL.FormatCells(ws.Cells[sR, startCol], tongpc, numberFormat: "#0.0#;-0;-?;", hAlign: ExcelHorizontalAlignment.Right);// tổng PC tháng
							startCol++;
							#endregion

							sR++;
						}
					}

				} catch (Exception exception) {
					throw exception;
				}
				Byte[] bin = p.GetAsByteArray();
				try {
					File.WriteAllBytes(file_path, bin);
					AutoClosingMessageBox.Show("Xuất báo biểu thành công.", "Thông báo", 2000);
				} catch (Exception exception) {
					lg.Error("XuatBBCongVaPC", exception);
					if (exception is UnauthorizedAccessException) MessageBox.Show("Bạn chưa được cấp quyền ghi file vào folder.", "Lỗi");
					else if (exception is DirectoryNotFoundException) MessageBox.Show("Không tìm thấy folder lưu trữ.", "Lỗi");
					else if (exception is IOException) MessageBox.Show("File đang mở bởi ứng dụng khác.", "Lỗi");
					else MessageBox.Show("Không thể ghi được file báo biểu.", "Lỗi");
				}

			}

		}


		private void XuatBBThKDitreVesom(List<cUserInfo> m_DSNVXemCong, string file_path, DateTime dateTime1, DateTime dateTime2) {

			using (var p = new ExcelPackage()) {
				#region //Create a sheet
				p.Workbook.Worksheets.Add("Bang thong ke tre som chi tiet");
				var ws1 = p.Workbook.Worksheets[1];
				ws1.Name = "Bang thong ke tre som chi tiet"; //Setting Sheet's name
				ws1.Cells.Style.Font.Size = 12; //Default font size for whole sheet
				ws1.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet
				p.Workbook.Worksheets.Add("Bang thong ke tre som tong hop");
				var ws2 = p.Workbook.Worksheets[2];
				ws2.Name = "Bang thong ke tre som tong hop"; //Setting Sheet's name
				ws2.Cells.Style.Font.Size = 12; //Default font size for whole sheet
				ws2.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet

				#endregion
				int sRWS1 = 1, sRWS2 = 1;

				int sttnv = 1, startColWS1 = 1, startColWS2 = 1;
				#region 1.tieu de bang cham cong tu ngay den ngay 2. bỏ 1 dòng trống

				var tempStr1 = "Bảng thống kê đi trễ, về sớm từ ngày {0} đến ngày {1}";
				tempStr1 = string.Format(tempStr1, dateTime1.ToString("d/M/yyyy"), dateTime2.ToString("d/M/yyyy"));
				XL.FormatCellsTitle3(ws1.Cells[sRWS1, 4], tempStr1, size: 14, Bold: true, VeBorder: false, hAlign: ExcelHorizontalAlignment.CenterContinuous);
				XL.FormatCellsTitle3(ws2.Cells[sRWS2, 4], tempStr1, size: 14, Bold: true, VeBorder: false, hAlign: ExcelHorizontalAlignment.CenterContinuous);
				sRWS1 = sRWS1 + 2;// sR+1 là dòng trống
				sRWS2 = sRWS2 + 2;// sR+1 là dòng trống
				#endregion

				#region  write ho ten +1
				ws1.Column(startColWS1).Width = (int)U.TEN;
				XL.FormatCellsTitle3(ws1.Cells[sRWS1, startColWS1], "Họ tên");
				startColWS1++;
				ws2.Column(startColWS2).Width = (int)U.STT;
				XL.FormatCellsTitle3(ws2.Cells[sRWS2, startColWS2], "STT");
				startColWS2++;
				#endregion
				#region write manv +2
				ws1.Column(startColWS1).Width = (int)U.MANV;
				XL.FormatCellsTitle3(ws1.Cells[sRWS1, startColWS1], "Mã NV");
				startColWS1++;
				ws2.Column(startColWS2).Width = (int)U.TEN;
				XL.FormatCellsTitle3(ws2.Cells[sRWS2, startColWS2], "Họ tên");
				startColWS2++;
				#endregion
				#region write ngày +2
				ws1.Column(startColWS1).Width = (int)U.NGAY;
				XL.FormatCellsTitle3(ws1.Cells[sRWS1, startColWS1], "Ngày");
				startColWS1++;
				ws2.Column(startColWS2).Width = (int)U.MANV;
				XL.FormatCellsTitle3(ws2.Cells[sRWS2, startColWS2], "Mã NV");
				startColWS2++;
				#endregion
				#region write manv +2
				ws1.Column(startColWS1).Width = (int)U.CA;
				XL.FormatCellsTitle3(ws1.Cells[sRWS1, startColWS1], "Ca");
				startColWS1++;
				ws2.Column(startColWS2).Width = (int)U.TONGTRE;
				XL.FormatCellsTitle3(ws2.Cells[sRWS2, startColWS2], "Tổng vào trễ");
				startColWS2++;
				#endregion
				#region write manv +2
				ws1.Column(startColWS1).Width = (int)U.VAOTRE;
				XL.FormatCellsTitle3(ws1.Cells[sRWS1, startColWS1], "Vào trễ");
				startColWS1++;
				ws2.Column(startColWS2).Width = (int)U.TONGSOM;
				XL.FormatCellsTitle3(ws2.Cells[sRWS2, startColWS2], "Tổng ra sớm");
				startColWS2++;
				#endregion
				#region write manv +2
				ws1.Column(startColWS1).Width = (int)U.RAASOM;
				XL.FormatCellsTitle3(ws1.Cells[sRWS1, startColWS1], "Ra sớm");
				startColWS1++;
				ws2.Column(startColWS2).Width = (int)U.TONGTRESOM;
				XL.FormatCellsTitle3(ws2.Cells[sRWS2, startColWS2], "Tổng");
				startColWS2++;
				#endregion
				#region write manv +2
				ws1.Column(startColWS1).Width = (int)U.VAO1;
				XL.FormatCellsTitle3(ws1.Cells[sRWS1, startColWS1], "Vào1");
				startColWS1++;
				#endregion
				#region write manv +2
				ws1.Column(startColWS1).Width = (int)U.RAA1;
				XL.FormatCellsTitle3(ws1.Cells[sRWS1, startColWS1], "Ra1");
				startColWS1++;
				#endregion
				#region write manv +2
				ws1.Column(startColWS1).Width = (int)U.VAO2;
				XL.FormatCellsTitle3(ws1.Cells[sRWS1, startColWS1], "Vào2");
				startColWS1++;
				#endregion
				#region write manv +2
				ws1.Column(startColWS1).Width = (int)U.RAA2;
				XL.FormatCellsTitle3(ws1.Cells[sRWS1, startColWS1], "Ra2");
				startColWS1++;
				#endregion
				#region write manv +2
				ws1.Column(startColWS1).Width = (int)U.VAO3;
				XL.FormatCellsTitle3(ws1.Cells[sRWS1, startColWS1], "Vào3");
				startColWS1++;
				#endregion
				#region write manv +2
				ws1.Column(startColWS1).Width = (int)U.RAA3;
				XL.FormatCellsTitle3(ws1.Cells[sRWS1, startColWS1], "Ra3");
				startColWS1++;
				#endregion
				//startCol = 4

				#region ghi TIEU DE ngay m_thang, TIEU DE cac cot thong ke, sau khi ghi thì nhớ tăng 2 sau dòng tiêu đề, xem cuối region này

				//goto ab;

				sRWS1++; // 2 dòng tiêu đề stt, ngày, tổng công....
				sRWS2++; // 2 dòng tiêu đề stt, ngày, tổng công....
				#endregion
				var pb_c1 = (from item in m_DSNVXemCong
							 select new { item.PBCap1.ID, item.PBCap1.TenPhongBan, item.PBCap1.ViTri }).Distinct().ToList().OrderBy(item => item.ViTri);

				try {

					foreach (var pb in pb_c1) {
						#region //write ten pb cap 1 tang 1 dong

						XL.FormatCellsTitle3(ws1.Cells[sRWS1, 1], pb.TenPhongBan, VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
						XL.FormatCellsTitle3(ws2.Cells[sRWS2, 1], pb.TenPhongBan, VeBorder: false, hAlign: ExcelHorizontalAlignment.Left);
						sRWS1++;
						sRWS2++;
						#endregion
						var dsnv = (from c in m_DSNVXemCong
									where c.PBCap1.ID == pb.ID
									select c).ToList();
						int iNgay = 0;
						var stt = 0;

						foreach (cUserInfo nhanvien in dsnv) {
							// duyet tung nv, nhớ tăng sR
							startColWS1 = 1;//reset startcol =1 mỗi lần ghi nv mới
							startColWS2 = 1;//reset startcol =1 mỗi lần ghi nv mới

							#region write cong, pc

							double tongtre = 0d, tongsom = 0d, tong = 0d;
							for (iNgay = 2; iNgay < nhanvien.DSNgayCong.Count - 2; iNgay++) {// startCol += 2 chuyên vào trong hàm vì ngay đầu, ngày cuối bỏ qua nhưng nếu để đây thì vẫn tăng 2 --> 2 ô đầu + 2 ô cuối

								var ngayCong = nhanvien.DSNgayCong[iNgay];
								if (ngayCong.TG.VaoTre.TotalMinutes < 1f && ngayCong.TG.RaaSom.TotalMinutes < 1f) continue;

								#region  stt,hoten,macc
								XL.FormatCells3(ws1.Cells[sRWS1, startColWS1 + 0], nhanvien.TenNV, hAlign: ExcelHorizontalAlignment.Left); //Ten
								XL.FormatCells3(ws1.Cells[sRWS1, startColWS1 + 1], nhanvien.MaNV);//MaNV
								XL.FormatCells3(ws1.Cells[sRWS1, startColWS1 + 2], ngayCong.Ngay, numberFormat: "dd/MM/yyyy", hAlign: ExcelHorizontalAlignment.Left);
								XL.FormatCells3(ws1.Cells[sRWS1, startColWS1 + 3], ngayCong.CIOs_Absents_Code_Comp(), hAlign: ExcelHorizontalAlignment.Left);
								XL.FormatCells3(ws1.Cells[sRWS1, startColWS1 + 4], ngayCong.TG.VaoTre.TotalMinutes);
								XL.FormatCells3(ws1.Cells[sRWS1, startColWS1 + 5], ngayCong.TG.RaaSom.TotalMinutes);
								tongtre += ngayCong.TG.VaoTre.TotalMinutes;
								tongsom += ngayCong.TG.RaaSom.TotalMinutes;
								for (int i = 0; i < 3; i++) {
									if (i >= ngayCong.DSVaoRa.Count && i < 3) {
										XL.FormatCells3(ws1.Cells[sRWS1, startColWS1 + 6 + (i * 2)], null, numberFormat: "H:mm");
										XL.FormatCells3(ws1.Cells[sRWS1, startColWS1 + 6 + (i * 2) + 1], null, numberFormat: "H:mm");
										continue;
									}
									var CIO = ngayCong.DSVaoRa[i];
									switch (CIO.HaveINOUT) {
										case -1:
											XL.FormatCells3(ws1.Cells[sRWS1, startColWS1 + 6 + (i * 2)], CIO.Vao.Time, numberFormat: "H:mm");
											XL.FormatCells3(ws1.Cells[sRWS1, startColWS1 + 6 + (i * 2) + 1], null, numberFormat: "H:mm");
											break;
										case -2:
											XL.FormatCells3(ws1.Cells[sRWS1, startColWS1 + 6 + (i * 2)], null, numberFormat: "H:mm");
											XL.FormatCells3(ws1.Cells[sRWS1, startColWS1 + 6 + (i * 2) + 1], CIO.Raa.Time, numberFormat: "H:mm");
											break;
										case 0:
										case 30:
											XL.FormatCells3(ws1.Cells[sRWS1, startColWS1 + 6 + (i * 2)], CIO.Vao.Time, numberFormat: "H:mm");
											XL.FormatCells3(ws1.Cells[sRWS1, startColWS1 + 6 + (i * 2) + 1], CIO.Raa.Time, numberFormat: "H:mm");
											break;
									}
								}

								#endregion
								sRWS1++;
							}
							tong = tongtre + tongsom;
							if (tong == 0d) {
							}
							else {
								stt++;
								XL.FormatCells3(ws2.Cells[sRWS2, startColWS1 + 0], stt, hAlign: ExcelHorizontalAlignment.Left); //Ten
								XL.FormatCells3(ws2.Cells[sRWS2, startColWS1 + 1], nhanvien.TenNV, hAlign: ExcelHorizontalAlignment.Left); //Ten
								XL.FormatCells3(ws2.Cells[sRWS2, startColWS1 + 2], nhanvien.MaNV);//MaNV
								XL.FormatCells3(ws2.Cells[sRWS2, startColWS2 + 3], tongtre, numberFormat: "####0;-0;-?;");
								XL.FormatCells3(ws2.Cells[sRWS2, startColWS2 + 4], tongsom, numberFormat: "####0;-0;-?;");
								XL.FormatCells3(ws2.Cells[sRWS2, startColWS2 + 5], tong, numberFormat: "####0;-0;-?;");
								sRWS2++;
							}

							#endregion



						}
					}

				} catch (Exception exception) {
					throw exception;
				}
				Byte[] bin = p.GetAsByteArray();
				try {
					File.WriteAllBytes(file_path, bin);
					AutoClosingMessageBox.Show("Xuất báo biểu thành công.", "Thông báo", 2000);
				} catch (Exception exception) {
					lg.Error("XuatBBCongVaPC", exception);
					if (exception is UnauthorizedAccessException) MessageBox.Show("Bạn chưa được cấp quyền ghi file vào folder.", "Lỗi");
					else if (exception is DirectoryNotFoundException) MessageBox.Show("Không tìm thấy folder lưu trữ.", "Lỗi");
					else if (exception is IOException) MessageBox.Show("File đang mở bởi ứng dụng khác.", "Lỗi");
					else MessageBox.Show("Không thể ghi được file báo biểu.", "Lỗi");
				}

			}

		}

		private void ReportChiTietChamCong(cUserInfo nv, string file_path, DateTime ngayBD, DateTime ngayKT)
		{
			using (var p = new ExcelPackage())
			{
				#region //Create a sheet

				p.Workbook.Worksheets.Add("Bang Thong ke chi tiet");
				var ws = p.Workbook.Worksheets[1];
				ws.Name = "Bang Thong ke chi tiet"; //Setting Sheet's name
				ws.Cells.Style.Font.Size = 12; //Default font size for whole sheet
				ws.Cells.Style.Font.Name = "Times News Roman"; //Default Font name for whole sheet
				p.Workbook.Worksheets.Add("Bang thong ke chi tiet");

				#endregion

				int marginL = 1, marginR = 1, marginB = 1, marginT = 1;
				int iR = marginT, iC = marginL;
				var temp = 0;
				XL.FormatCell(ws, ref iR, ref temp, "BẢNG CHẤM CÔNG CHI TIẾT THÁNG", Bold:true, size:16, hAlign:ExcelHorizontalAlignment.CenterContinuous, VeBorder:false, increaseRow:1);
				XL.FormatCell(ws, ref iR, ref iC, "Ngày", Bold:true, hAlign:ExcelHorizontalAlignment.CenterContinuous, VeBorder:false, increaseRow:1);


					double Le = 0d, Phep = 0d, H_CT_PT = 0d, BHXH = 0d, NghiRo = 0d, CongCV=0d, pc30 = 0d, pc50 = 0d, pctcc3 = 0d, pc100 = 0d, pc160 = 0d, pc200 = 0d, pc290 =0d, pckhac = 0d;
				for (int i = 2; i < nv.DSNgayCong.Count - 2; i++)
				{
					cNgayCong ngayCong = nv.DSNgayCong[i];
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.Ngay, increaseCol:1, numberFormat:"ddd dd/MM/yyyy", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.CIOs_Absents_Code_Comp(), increaseCol:1, hAlign:ExcelHorizontalAlignment.Left);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.TG.GioLamTrongNgay, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.TG.GioLamTrongNgay, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.TG.GioThuc, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.TG.VaoTre, increaseCol:1, numberFormat:"###,##0;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.TG.RaaSom, increaseCol:1, numberFormat:"###,##0;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.TongCong, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.TongPhuCap, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					foreach (var loaiVang in ngayCong.DSVang)
					{
						switch (loaiVang.KyHieu)
						{
							case "P":
								Phep = Phep + loaiVang.Cong;
								break;
							case "CV":
								CongCV = CongCV + loaiVang.Cong;
								break;
							case "BH":
							case "TS":
								BHXH = BHXH + loaiVang.Cong;
								break;
							case "PT":
							case "H":
							case "CT":
								H_CT_PT = H_CT_PT + loaiVang.Cong;
								break;
							case "RO":
								NghiRo = NghiRo + loaiVang.Cong;
								break;
							case "L":
								Le = Le + loaiVang.Cong;
								break;
						}
					}
					XL.FormatCell(ws, ref iR, ref iC, Le, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, Phep, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, H_CT_PT, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, BHXH, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, NghiRo, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.PhuCap30, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.PhuCap50, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.PhuCapTCC3, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.PhuCap100, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.PhuCap160, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.PhuCap200, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.PhuCap290, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);
					XL.FormatCell(ws, ref iR, ref iC, ngayCong.PhuCapCus, increaseCol:1, numberFormat:"#0.0#;-0;-?;", hAlign:ExcelHorizontalAlignment.Right);


				}
			}

		}

		private void Form2_Load(object sender, EventArgs e) {
			var ngayBDXemCong = ngayBD_Bef2D.AddDays(2d);
			var thang = new DateTime(ngayBDXemCong.Year, ngayBDXemCong.Month, 1);
			var ngaycuoithang = new DateTime(thang.Year, thang.Month, DateTime.DaysInMonth(thang.Year, thang.Month));


			//update xuất báo biểu tháng{0}
			var ngaydauuthang01 = new DateTime(ngayBDXemCong.Year, ngayBDXemCong.Month, 1);
			radBB_ChamCongThang.Text = string.Format(radBB_ChamCongThang.Text, ngaydauuthang01.ToString("MM/yyyy"));

		}

		private void btnXuata_Click(object sender, EventArgs e) {
			#region lấy save file name

			saveFileDlg.Filter = "Excel File|*.xlsx";
			saveFileDlg.ShowDialog();
			if (saveFileDlg.FileName == String.Empty) {
				return;
			}
			var saveFileName = saveFileDlg.FileName;

			#endregion

			try {
				//lấy ngày đầu tiên xem công
				var ngayBDXemCong = ngayBD_Bef2D.AddDays(2d);
				if (radBB_ChamCongThang.Checked) {
					var ngaydauuthang01 = new DateTime(ngayBDXemCong.Year, ngayBDXemCong.Month, 1);
					var ngaycuoithang01 = new DateTime(ngayBDXemCong.Year, ngayBDXemCong.Month, DateTime.DaysInMonth(ngayBDXemCong.Year, ngayBDXemCong.Month));

					XL.XemCong(m_dsnv, ngaydauuthang01.AddDays(-2d), ngaycuoithang01.AddDays(2d));
					XuatBBCongVaPCThang(m_dsnv, saveFileName, ngaydauuthang01, ngaycuoithang01, tbTenNVNhapLieu.Text, tbTenTruongBP.Text); //tbd chính thức
				}
				else if (radioButton1.Checked) {
					var ngayKTXemCong = ngayKT_Aft2D.AddDays(-2d);
					XL.XemCong(m_dsnv, ngayBD_Bef2D, ngayKT_Aft2D);
					XuatBBCongVaPCTuNgayDenNgay(m_dsnv, saveFileName, ngayBD_Bef2D.AddDays(2d), ngayKT_Aft2D.AddDays(-2d), tbTenNVNhapLieu.Text, tbTenTruongBP.Text);//tbd test

				}
				else if (radioButton2.Checked) {
					XL.XemCong(m_dsnv, ngayBD_Bef2D, ngayKT_Aft2D);
					XuatBBThKDitreVesom(m_dsnv, saveFileName, ngayBD_Bef2D.AddDays(2d), ngayKT_Aft2D.AddDays(-2d));
				}
			} catch (Exception exception) {
				lg.Error("btnXuata_Click", exception);
				MessageBox.Show("Xảy ra lỗi trong quá trình xuất. Vui lòng liên hệ bộ phận kỹ thuật để được trợ giúp.", "Lỗi", MessageBoxButtons.OK);
			}
		}

		private void button2_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}
