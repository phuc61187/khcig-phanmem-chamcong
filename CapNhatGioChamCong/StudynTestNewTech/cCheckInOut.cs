using System;
using System.Collections.Generic;

namespace ChamCong_v05.DTO {
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

	public class ThoiDiem
	{
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

		public override string ToString()
		{
			var temp = "{0} ({1} {2}): {3}";
			return string.Format(temp, ((Type == "I") ? "INN" : "OUT"), Source, MachineNo, Time.ToString("d/M H:mm"));
		}
		#endregion


	}



	public class cCheckInOut
	{
		public DateTime TimeDaiDien;
		public cCheck Vao;
		public cCheck Raa;
		public int ShiftID;
		public bool QuaDem;
		public DateTime ThuocNgayCong;
		public bool DaXN;
		public int HaveINOUT;
		public bool DuyetChoPhepVaoTre;
		public bool DuyetChoPhepRaSom;
		public bool VaoTreTinhCV; //ver 4.0.0.4	
		public bool RaaSomTinhCV; //ver 4.0.0.4	
		public int OTMin;

		#region v4

		/*public int ID;*/ // info xem lại tình trạng sử dụng của ID này, suggest xóa nếu ko cần thiết
		public ThoiGian TG;
		public ThoiDiem TD;
		public float Cong;

		#endregion

		public cCheckInOut() {
		}

	}


}
