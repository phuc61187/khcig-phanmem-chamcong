using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamCong_v06.Helper;

namespace ChamCong_v06.DTO {
	public partial class cCheckInOut_DaCC {
		#region dữ liệu
		public int ID;
		public int MaCC;
		public TrangThaiCheck CheckVT;
		public DateTime Ngay;
		public DateTime GioVao;
		public DateTime GioRaa;

		public int VaoLamTron_Min;
		public int RaaLamTron_Min;
		public int BD_LV_Min;// vào làm ca
		public int KT_LV_TrongCa_Min;
		public int KT_LV_Min;// 
		public int BD_LV_Ca3_Min;
		public int KT_LV_Ca3_Min;

		public int Tre_Min;
		public int Som_Min;

		public DateTime VaoLamTron { get { return Ngay.Add(new TimeSpan(0, VaoLamTron_Min, 0)); } }
		public DateTime RaaLamTron { get { return Ngay.Add(new TimeSpan(0, RaaLamTron_Min, 0)); } }

		public DateTime BD_LV { get { return Ngay.Add(new TimeSpan(0, BD_LV_Min, 0)); } }// vào làm ca
		public DateTime KT_LV_TrongCa { get { return Ngay.Add(new TimeSpan(0, KT_LV_TrongCa_Min, 0)); } }
		public DateTime KT_LV { get { return Ngay.Add(new TimeSpan(0, KT_LV_Min, 0)); } }// 
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

		public float TruCongTre = 0f;
		public float TruCongSom = 0f;
		public float CongTrongGio = 0f;
		public float CongNgoaiGio = 0f;

		public bool TinhCongThuCong;
		public float ChamCongTay = 0f;

		public float DinhMuc = 0f;
		public float Tong = 0f;
		#endregion

		public TimeSpan LamDem { get { return KT_LV_Ca3 - BD_LV_Ca3; } }
		public TimeSpan LamTrongGio { get { return KT_LV_TrongCa - BD_LV; } }
		public TimeSpan LamNgoaiGio { get { return KT_LV - KT_LV_TrongCa; } }
		public TimeSpan LamViec { get { return KT_LV - BD_LV; } }




	}
}
