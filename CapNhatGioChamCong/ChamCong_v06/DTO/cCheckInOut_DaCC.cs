using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamCong_v06.Helper;

namespace ChamCong_v06.DTO {
	public partial class cCheckInOut_DaCC {
		public int ID;
		public int MaCC;
		public string MaNV;
		public string TenNV;
		public TrangThaiCheck CheckVT;
		public DateTime Ngay;

		public DateTime GioVao;
		public DateTime GioRaa;
		public DateTime VaoLamTron;
		public DateTime RaaLamTron;

		public DateTime BD_LV;// vào làm ca
		public DateTime KT_LV_TrongCa;
		public DateTime KT_LV;// 
		public DateTime BD_LV_Ca3;
		public DateTime KT_LV_Ca3;

		//public TimeSpan HienDien;
		public TimeSpan Tre;
		public TimeSpan Som;
		public TimeSpan VaoSauCa;
		public TimeSpan RaTruocCa;
		//public TimeSpan LamViec;
		public TimeSpan LamDem;
		public TimeSpan LamTrongGio;
		public TimeSpan LamNgoaiGio; 
		
		public bool ChoPhepTre;
		public bool ChoPhepSom;
		public bool VaoTuDo;
		public bool RaaTuDo;

		public float TruCongTre;
		public float TruCongSom;
		public float CongTrongGio;
		public float CongNgoaiGio;
		public bool ChamCongTay;
		public float DinhMuc;
		public float Tong;


	}
}
