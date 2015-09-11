using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamCong_v06.BUS;
using ChamCong_v06.Helper;

namespace ChamCong_v06.DTO {
	public class cCheckInOut {
		#region phần thông tin chung cho đã xử lý và chưa xử lý
		public DateTime TimeDaiDien;
		public DateTime ThuocNgayCong;
		public TrangThaiCheck CheckVT;
		//public ThoiDiem TD;
		public DateTime VaoLamTron;
		public DateTime RaaLamTron;
		public DateTime BD_LV;// vào làm ca
		public DateTime KT_LV_TrongCa;
		public DateTime KT_LV;// 
		public DateTime BD_LV_Ca3;
		public DateTime KT_LV_Ca3;

		#endregion
		#region phần thông tin để xử lý các check vân tay
		public cCheck Vao;
		public cCheck Raa;
		public cCa ThuocCa;
		public List<cCa> DSCaNhanDien;

		//public StructTGCa KhoangTGCa;
		public TimeSpan HienDien = TimeSpan.Zero;//todo IsUseful?
		public TimeSpan Tre = TimeSpan.Zero;
		public TimeSpan Som = TimeSpan.Zero;
		public TimeSpan VaoSauCa = TimeSpan.Zero;
		public TimeSpan RaTruocCa = TimeSpan.Zero;
		public TimeSpan OLaiVR = TimeSpan.Zero;//todo IsUseful?
		//public TimeSpan LamViec;
		public TimeSpan LamDem = TimeSpan.Zero;
		public TimeSpan LamTrongGio = TimeSpan.Zero;
		public TimeSpan LamNgoaiGio = TimeSpan.Zero;

		//public StructCongCa CongTheoCa;
		public float TruCongTre = 0f;
		public float TruCongSom = 0f;
		public float TrongGio = 0f;
		public float NgoaiGio = 0f;//todo IsUseful?
		public float DinhMuc = 0f;//todo IsUseful?
		public float Tong = 0f;

		#endregion
		#region phần thông tin riêng cho đã xử lý
		public bool ChoPhepTre = false;
		public bool ChoPhepSom= false;
		public bool VaoTuDo= false; //ver 4.0.0.4	
		public bool RaaTuDo= false; //ver 4.0.0.4	

		#endregion
		public int ShiftID;
		//public cCa ThuocCa;
		//public StructTGCa TG;
		public bool QuaDem;

		public override string ToString() {
			var temp = "HaveIO:{0} V:{1} R:{2} Ngay:{3}";

			return string.Format(temp, CheckVT, (Vao != null) ? Vao.Time.ToString("H:mm") : "", (Raa != null) ? Raa.Time.ToString("H:mm") : "",
								 /*(ThuocCa != null) ? ThuocCa.Code : "",*/ ThuocNgayCong.ToString("d/M"));
		}


	}
	public class cCheckInOutComparer : IComparer<cCheckInOut> {
		public int Compare(cCheckInOut x, cCheckInOut y) {
			return x.TimeDaiDien.CompareTo(y.TimeDaiDien);
			//return 1;
		}
	}




}
