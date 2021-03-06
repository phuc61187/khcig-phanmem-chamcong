﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamCong_v06.Helper;

namespace ChamCong_v06.DTO {
	public partial class cCheckInOut_DaCC {
		#region dữ liệu
		public int ID;
		public int MaCC;

		public string KyHieuCa;
		public TrangThaiCheck CheckVT;
		public DateTime Ngay;
		public DateTime GioVao;
		public DateTime GioRaa;

		public int VaoLamTron_Min;
		public int RaaLamTron_Min;
		public int BDCa_Min;
		public int KTCa_Min;
		public int BD_LV_Min;// vào làm ca
		public int KT_LV_TrongCa_Min;
		public int KT_LV_Min;// 
		public int LunchMin;
		public int BD_LV_Ca3_Min;
		public int KT_LV_Ca3_Min;

		public int Tre_Min;
		public int Som_Min;

		public DateTime VaoLamTron { get { return Ngay.Add(new TimeSpan(0, VaoLamTron_Min, 0)); } }
		public DateTime RaaLamTron { get { return Ngay.Add(new TimeSpan(0, RaaLamTron_Min, 0)); } }

		public DateTime BDCa { get { return Ngay.Add(new TimeSpan(0, BDCa_Min, 0)); } }
		public DateTime KTCa { get { return Ngay.Add(new TimeSpan(0, KTCa_Min, 0)); } }
		public DateTime BD_LV { get { return Ngay.Add(new TimeSpan(0, BD_LV_Min, 0)); } }// vào làm ca
		public DateTime KT_LV_TrongCa { get { return Ngay.Add(new TimeSpan(0, KT_LV_TrongCa_Min, 0)); } }
		public DateTime KT_LV { get { return Ngay.Add(new TimeSpan(0, KT_LV_Min, 0)); } }// 
		public TimeSpan TG_NghiTrua { get { return new TimeSpan(0, LunchMin, 0); } }
		public DateTime BD_LV_Ca3 { get { return Ngay.Add(new TimeSpan(0, BD_LV_Ca3_Min, 0)); } }
		public DateTime KT_LV_Ca3 { get { return Ngay.Add(new TimeSpan(0, KT_LV_Ca3_Min, 0)); } }

		public bool QuaDem;

		//public TimeSpan HienDien;
		public TimeSpan Tre { get { return (ChoPhepTre || VaoTuDo) ? TimeSpan.Zero : new TimeSpan(0, Tre_Min, 0); } }
		public TimeSpan Som { get { return (ChoPhepSom || RaaTuDo) ? TimeSpan.Zero : new TimeSpan(0, Som_Min, 0); } }

		public TimeSpan VaoSauCa = TimeSpan.Zero;
		public TimeSpan RaTruocCa = TimeSpan.Zero;
		//public TimeSpan LamViec;

		public bool ChoPhepTre;
		public bool ChoPhepSom;
		public bool VaoTuDo;
		public bool RaaTuDo;
		public bool TinhCongThuCong;

		public float TruCongTre;
		public float TruCongSom;
		public float CongTrongGio;
		public float CongNgoaiGio;
		public float ChamCongTay;

		#endregion

		public TimeSpan LamDem { get { return KT_LV_Ca3 - BD_LV_Ca3; } }
		public TimeSpan LamTrongGio { get { return ((KT_LV_TrongCa - BD_LV) - TG_NghiTrua); } }
		public TimeSpan LamNgoaiGio { get { return KT_LV - KT_LV_TrongCa; } }
		public TimeSpan LamViec { get { return ((KT_LV - BD_LV) - TG_NghiTrua); } }
		public TimeSpan HienDien { get { return (CheckVT == TrangThaiCheck.CheckDayDu) ? (RaaLamTron - VaoLamTron) : TimeSpan.Zero; } }


		public float DinhMuc;
		public float Tong;
		internal void CapNhatDinhMucCong() {
			DinhMuc = CongTrongGio + CongNgoaiGio + TruCongTre + TruCongSom + ChamCongTay;
		}

		public void CapNhatDinhTongCong() {
			Tong = CongTrongGio + CongNgoaiGio + ChamCongTay;
		}
	}
}
