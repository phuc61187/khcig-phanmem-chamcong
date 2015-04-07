using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamCong_v05.Helper {
	#region STRUCT
	public struct TS {
		public TimeSpan Onn;
		public TimeSpan Off;
		public override string ToString() {
			string temp = "Onn:{0}; Off:{1}";
			return string.Format(temp, Onn.ToString(@"d\ hh:\mm"), Off.ToString(@"d\ hh:\mm"));
		}
	}

	public struct DT {
		public DateTime BD;
		public DateTime KT;
		public override string ToString() {
			string temp = "BD:{0}; KT:{1}";
			return string.Format(temp, BD.ToString("H:mm d/M"), KT.ToString("H:mm d/M"));
		}
	}
	public struct structThoiGian
	{
		public TimeSpan GioThucTe5;
		public TimeSpan GioLamViec5; // tổng giờ làm việc đã bao gồm OT nếu có
		public TimeSpan GioLVTrongCa5; // chỉ tính giờ làm việc nằm trong ca, ko tính OT
		public TimeSpan SoPhutLamThem5;// tương đương giờ LV ngoài ca, tương đương OT đã xác nhận, làm ngoài ca chưa chắc OT ví dụ nửa ca
		public TimeSpan LamBanDem;
		public TimeSpan LamTangCuong; //trên 8 tiếng là tăng cường(ko xét ngày hay đêm)
		//public TimeSpan Tinh100;
		public TimeSpan Tinh130;
		public TimeSpan Tinh150;
		public TimeSpan TinhTCC3;
		public TimeSpan Tinh200;
		public TimeSpan Tinh260;
		public TimeSpan Tinh300;
		public TimeSpan Tinh390;
		public TimeSpan TinhPCCus;

		public TimeSpan VaoTre;
		public TimeSpan RaaSom;
		public TimeSpan OLai; // 
	}

	public struct structThoiDiem
	{
		public DateTime BD_LV;// vào làm ca
		public DateTime KT_LV;// 
		public DateTime KT_LV_ChuaOT;
		//public DateTime KT_LV_DaCoOT;
		public DateTime BD_LV_Ca3;
		public DateTime KT_LV_Ca3;
	}


	public struct structCong
	{
		public float CaQuyDinh;
		public float ThucTe;// thực tế được hưởng
		public float TTTrongCa; // thực tế trong ca được hưởng
		public float TTNgoaiCa; //thực tế làm thêm sau giờ chính được hưởng
		//public float CongBu; //chờ làm bù không bị trừ công
		public float TTCongTre; // phần công thực tế do trễ 
		public float TTCongSom; // phần công thực tế do sớm 
		//public float TongCongTru; // bị trừ công do trễ sớm việc riêng
		public float TongCongBu;
		public float TongCongTru;
		public float DinhMuc; // định mức công được hưởng bao gồm công do ca quy định và phần công ngoài ca
	}

	public struct PhuCap {
		public float _30_dem;
		public float _50_TC;
		public float _100_TCC3;
		public float _100_LVNN_Ngay;
		public float _150_LVNN_Dem;
		public float _200_LeTet_Ngay;
		public float _250_LeTet_Dem;
		public float _Cus;
		public float _TongPC;
		public override string ToString() {
			string temp = "Tong={8}; 30%={0}; 50%={1}; TCC3={2}; 100%CN={3}; 150%DemCN={4}; 200%Le={5}; 250%DemLe={6}; Cus={7};";
			return string.Format(temp, _30_dem, _50_TC, _100_TCC3, _100_LVNN_Ngay, _150_LVNN_Dem, _200_LeTet_Ngay, _250_LeTet_Dem, _Cus, _TongPC);
		}
	}

	public struct SUMLUONG {
		public float hslcb4;
		public float hslsp5;
		public float cong6;
		public float phep7;
		public float hop8;
		public float ptdt9;
		public float viecrieng10;
		public int quadem11;
		public float choviec12;
		public float pc30_13;
		public float pc50_14;
		public float pctcc3_15;
		public float pc100_16;
		public float pc160_17;
		public float pc200_18;
		public float pc290_19;
		public float tongcong20;
		public float tongpc21;
		public double luongcb22;
		public double luongsp23;
		public double dieuchinh24;
		public double tongluong25;
		public double pcluongcb26;
		public double pcluongsp27;
		public double boiduongdem28;
		public double tongpcLuong29;
		public double tongluongpc30;
		public double tamung31;
		public double bh32;
		public double thuchikhac33;
		public double tienComTrua34;
		public double thuclanh35;

	}

	public struct SUMCC {
		public float cong1;
		public float le2;
		public float phep3;
		public float cv4;
		public float bhxh5;
		public float hoc6;
		public float tongcong7;
		public float pc30_8;
		public float pc50_9;
		public float pctcc3_10;
		public float pc100_11;
		public float pc160_12;
		public float pc200_13;
		public float pc290_14;
		public float pckhac_15;
		public float nghiRO_16;
		public float ptdt_17;

	}


	#endregion


}
