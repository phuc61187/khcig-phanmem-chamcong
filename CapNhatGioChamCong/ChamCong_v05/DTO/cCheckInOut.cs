using System;
using System.Collections.Generic;
using System.Linq;
using ChamCong_v05.Helper;
using ChamCong_v05.Properties;

namespace ChamCong_v05.DTO {
	public class cPhucHoi {
		public bool Them;
		public int IDGioGoc;
		public bool Xoaa;
		public override string ToString() {
			string temp = "IDSua: {0}; {1}Them; {2}Sua";
			return string.Format(temp, IDGioGoc.ToString(), Convert.ToInt16(Them), Convert.ToInt16(Xoaa));
		}
	}

	public class ThoiGian {
		public TimeSpan GioThucTe5 = TimeSpan.Zero;
		public TimeSpan GioLamViec5 = TimeSpan.Zero; // tổng giờ làm việc đã bao gồm OT nếu có
		public TimeSpan GioLVTrongCa5 = TimeSpan.Zero; // chỉ tính giờ làm việc nằm trong ca, ko tính OT
		public TimeSpan SoPhutLamThem5 = TimeSpan.Zero;// tương đương giờ LV ngoài ca, tương đương OT đã xác nhận, làm ngoài ca chưa chắc OT ví dụ nửa ca
		public TimeSpan LamBanDem = TimeSpan.Zero;
		public TimeSpan LamTangCuong = TimeSpan.Zero; //trên 8 tiếng là tăng cường(ko xét ngày hay đêm)
		public TimeSpan VaoTre = TimeSpan.Zero;
		public TimeSpan RaaSom = TimeSpan.Zero;
		public TimeSpan OLai = TimeSpan.Zero; // 

		#region v4

		//public TimeSpan Tinh100 = TimeSpan.Zero;
		public TimeSpan Tinh130 = TimeSpan.Zero;
		public TimeSpan Tinh150 = TimeSpan.Zero;
		public TimeSpan TinhTCC3 = TimeSpan.Zero;
		public TimeSpan Tinh200 = TimeSpan.Zero;
		public TimeSpan Tinh260 = TimeSpan.Zero;
		public TimeSpan Tinh300 = TimeSpan.Zero;
		public TimeSpan Tinh390 = TimeSpan.Zero;
		public TimeSpan TinhPCCus = TimeSpan.Zero;

		#endregion

	}

	public class ThoiDiem {
		public DateTime BD_LV;// vào làm ca
		public DateTime KT_LV;// 
		public DateTime KT_LV_ChuaOT;
		//public DateTime KT_LV_DaCoOT;
		public DateTime BD_LV_Ca3;
		public DateTime KT_LV_Ca3;
	}

	[Serializable]
	public class cCheck {
		#region Public Properties

		public int ID;
		public ushort IsEdited;
		public int MaCC;
		public DateTime Time;
		public string Source;
		public int MachineNo;
		public string Type;
		public cPhucHoi PhucHoi = new cPhucHoi();

		public override string ToString() {
			var temp = "{0} ({1} {2}): {3}";
			return string.Format(temp, ((Type == "I") ? "INN" : "OUT"), Source, MachineNo, Time.ToString("d/M H:mm"));
		}
		#endregion


	}



	public class cCheckInOut {
		public DateTime TimeDaiDien;
		public cCheck Vao;
		public cCheck Raa;
		public int ShiftID;
		public cCa ThuocCa;
		public List<cCa> DSCa;
		public structThoiGianTheoCIO TG5;
		public structThoiDiem TD5;
		public bool QuaDem;
		public DateTime ThuocNgayCong;
		public bool DaXN;
		public int HaveINOUT;
		public structCong Cong5;
		public bool DuyetChoPhepVaoTre;
		public bool DuyetChoPhepRaSom;
		public bool VaoTreTinhCV; //ver 4.0.0.4	
		public bool RaaSomTinhCV; //ver 4.0.0.4	
		public int OTMin;

		#region v4

		/*public int ID;*/
		// info xem lại tình trạng sử dụng của ID này, suggest xóa nếu ko cần thiết
		public ThoiGian TG;
		public ThoiDiem TD;
		public float Cong;

		#endregion

		public override string ToString() {
			var temp = "HaveIO:{0} XN:{1} V:{2} R:{3} Ca:{4} Ngay:{5}";

			return string.Format(temp, HaveINOUT, DaXN,
								 (Vao != null) ? Vao.Time.ToString("H:mm") : "", (Raa != null) ? Raa.Time.ToString("H:mm") : "",
								 (ThuocCa != null) ? ThuocCa.Code : "", ThuocNgayCong.ToString("d/M"));
		}

		public string CIOCodeComp(string chuoiTruoc = null) {
			var kq = string.Empty;
			if (HaveINOUT == -2) kq = "KV";
			else if (HaveINOUT == -1) kq = "KR";
			else kq = ((DaXN) ? "XN-" : "") + ThuocCa.Code;
			return (chuoiTruoc == null)
					   ? kq
					   : chuoiTruoc + ";" + kq;
		}

		public string CIOCodeFull(string chuoiTruoc = null) {
			var kq = string.Empty;
			if (HaveINOUT == -2) {
				kq += "KV(";
				if (DSCa != null) {
					var result = string.Empty;
					foreach (var abs in DSCa)
						if (result == string.Empty)
							result += abs.Code;
						else result += ";" + abs.Code;
					kq += result;
				}
				kq += ")";
			}
			else if (HaveINOUT == -1) {
				kq += "KR(";
				if (DSCa != null) {
					var result = string.Empty;
					foreach (var abs in DSCa)
						if (result == string.Empty)
							result += abs.Code;
						else result += ";" + abs.Code;
					kq += result;
				}
				kq += ")";
			}
			else {
				kq += ((DaXN) ? "XN-" : "") + ThuocCa.Code;
			}
			return (chuoiTruoc == null)
								   ? kq
								   : chuoiTruoc + ";" + kq;

		}

		public string ExportKyHieuThuocCa1_5(bool ShowDSCa_KV_KR = false) {
			if (this.HaveINOUT == 0) return ";" + this.ThuocCa.Code;//@out empty | ;ca1  | ;ca2
			else if (HaveINOUT == -1) {
				string dsca = string.Empty;
				dsca = this.DSCa.Aggregate(dsca, (current, @out) => current + "," + @out.Code); 
				if (ShowDSCa_KV_KR) return "," + dsca;//@out empty | ,ca1 | ,2
				dsca = dsca.XoaKyTuPhanCachDauTien();
				return string.Format(";{0}({1})", Resources.SymKhongVao, dsca);//@out empty | ;KV(ca1,ca2) | ;2
			}
			else if (HaveINOUT == -2) {
				string dsca = string.Empty;
				dsca = this.DSCa.Aggregate(dsca, (current, @out) => current + ";" + @out.Code);
				if (ShowDSCa_KV_KR) return ";" + dsca;
				dsca = dsca.XoaKyTuPhanCachDauTien();
				return string.Format(";{0}({1})", Resources.SymKhongRa, dsca);//@out empty | ;KV(ca1,ca2) | ;2
			}
			else return ";#Name";
			//final result @out empty | ;KV(ca1,ca2) | ;2
		}

		public string ExportKyHieuTreSom5() {
			if (this.TG5.TongGioLamViec5 == TimeSpan.Zero) return string.Empty;

			string kq = string.Empty;
			if (this.TG5.VaoTre > TimeSpan.Zero) {//@out empty | ;BT | ;-T
				if (this.VaoTreTinhCV) kq += "," + Resources.SymTrePhaiLamBu;
				else kq += "," + Resources.SymTreBiTru;
			}

			if (this.TG5.RaaSom > TimeSpan.Zero) {//@out empty | ;BS | ;-S
				if (this.RaaSomTinhCV) kq += "," + Resources.SymSomPhaiLamBu;
				else kq += "," + Resources.SymSomBiTru;
			}
			return kq;//final result @out empty | ;BT;-S | ;-T;BS 
		}

		public string ExportKyHieu5_1(bool ShowDSCa_KV_KR = false) {
			string kq1 = string.Empty;
			kq1 += ExportKyHieuTreSom5(); //--> @out = empty hoặc ;-T;BT
			kq1 = kq1.XoaKyTuPhanCachDauTien(); // bỏ ; --> -T;BT
			if (kq1 != string.Empty) kq1 = string.Format("[{0}]", kq1); // @out empty [-T;BS]
			string kq2 = ExportKyHieuThuocCa1_5(ShowDSCa_KV_KR); //--> @out = empty | ;1;3 hoặc 

			return string.Format("{0}{1}", kq2, kq1);// @out empty | ;Ca1 [-T;BS]
		}

		public cCheckInOut() {
		}

	}


}
