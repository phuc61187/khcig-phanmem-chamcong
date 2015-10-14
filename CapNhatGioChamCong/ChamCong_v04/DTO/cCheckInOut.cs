using System;
using System.Collections.Generic;

namespace ChamCong_v04.DTO {
	public class cPhucHoi {
		public bool Them;
		public int IDGioGoc;
		public bool Xoaa;
		public override string ToString()
		{
			string temp = "IDSua: {0}; {1}Them; {2}Sua";
			return string.Format(temp, IDGioGoc.ToString(), Convert.ToInt16(Them), Convert.ToInt16(Xoaa));
		}
	}

	public class ThoiGian {
		public TimeSpan GioThuc = TimeSpan.Zero;
		public TimeSpan GioLamViec = TimeSpan.Zero;
		public TimeSpan GioLVTrongCa = TimeSpan.Zero;
		public TimeSpan LamBanDem = TimeSpan.Zero;
		//public TimeSpan LamBanNgay = TimeSpan.Zero;
		public TimeSpan LamThem = TimeSpan.Zero;
		//public TimeSpan Tinh100 = TimeSpan.Zero;
		public TimeSpan Tinh130 = TimeSpan.Zero;
		public TimeSpan Tinh150 = TimeSpan.Zero;
		public TimeSpan TinhTCC3 = TimeSpan.Zero;
		public TimeSpan Tinh200 = TimeSpan.Zero;
		public TimeSpan Tinh260 = TimeSpan.Zero;
		public TimeSpan Tinh300 = TimeSpan.Zero;
		public TimeSpan Tinh390 = TimeSpan.Zero;
		public TimeSpan TinhPCCus = TimeSpan.Zero;

		public TimeSpan VaoTre = TimeSpan.Zero;
		public TimeSpan RaaSom = TimeSpan.Zero;
		public TimeSpan OLai = TimeSpan.Zero;
		public TimeSpan OTCa = TimeSpan.Zero;
	}

	public class ThoiDiem
	{
		public DateTime BD_LV;// vào làm ca
		public DateTime KT_LV;// 
		public DateTime KT_LV_ChuaOT;
		public DateTime KT_LV_DaCoOT;
	    public DateTime BD_LV_Ca3;
	    public DateTime KT_LV_Ca3;
	}

	[Serializable]
	public class cCheck {
		#region Public Properties

		public int ID;
		public int MaCC;
		public DateTime Time;
		public string Source;
		public int MachineNo;
		public string Type;
		public cPhucHoi PhucHoi = new cPhucHoi();

		public override string ToString()
		{
			var temp = "{0} ({1} {2}): {3}";
			return string.Format(temp, ((Type == "I") ? "INN" : "OUT"), Source, MachineNo, Time.ToString("d/M H:mm"));
		}
		#endregion


	}



	public class cCheckInOut
	{
		/*public int ID;*/ // info xem lại tình trạng sử dụng của ID này, suggest xóa nếu ko cần thiết
		public DateTime TimeDaiDien;
		public cCheck Vao;
		public cCheck Raa;
		public int ShiftID;
		public cCa ThuocCa;
		public ThoiGian TG;
		public ThoiDiem TD;
		public bool QuaDem;
		public DateTime ThuocNgayCong;
		public float Cong; 
		public bool DaXN;
		public int HaveINOUT;

		public bool DuyetChoPhepVaoTre;
		public bool DuyetChoPhepRaSom;
		public bool VaoTreTinhCV; //ver 4.0.0.4	
		public bool RaaSomTinhCV; //ver 4.0.0.4	
		public float CongTrongCa;//ver 4.0.0.8
		public float CongNgoaiCa;//ver 4.0.0.8
		public float TruCongTre;//ver 4.0.0.8
		public float TruCongSom;//ver 4.0.0.8
		public bool ChoBuGioTre;//ver 4.0.0.8
		public bool ChoBuGioSom;//ver 4.0.0.8
		public float TruCongTreVR;//ver 4.0.0.8
		public float TruCongSomVR;//ver 4.0.0.8
		public float TruCongTreBu;//ver 4.0.0.8
		public float TruCongSomBu;//ver 4.0.0.8

		public int OTMin;
		public override string ToString()
		{
			var temp = "HaveIO:{0} XN:{1} V:{2} R:{3} Ca:{4} Ngay:{5}";

			return string.Format(temp, HaveINOUT, DaXN,
			                     (Vao != null) ? Vao.Time.ToString("H:mm") : "", (Raa != null) ? Raa.Time.ToString("H:mm") : "",
			                     (ThuocCa != null) ? ThuocCa.Code : "", ThuocNgayCong.ToString("d/M"));
		}

		public string CIOCodeComp(string chuoiTruoc = null) {
			var kq = string.Empty;
			if (HaveINOUT == -2) kq = "KV";
			else if (HaveINOUT == -1) kq = "KR";
			else kq = ((DaXN) ? "XN-":"")+ThuocCa.Code;
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

		public List<cCa> DSCa;

		public cCheckInOut() {
		}

	}


}
