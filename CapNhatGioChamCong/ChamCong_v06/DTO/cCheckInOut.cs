using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamCong_v06.Helper;

namespace ChamCong_v06.DTO {
	public class cCheckInOut {
		public ushort IsEdited;
		public DateTime TimeDaiDien;
		public cCheck Vao;
		public cCheck Raa;
		public int ShiftID;
		//public cCa ThuocCa;
		public StructTGCa TG;
		public ThoiDiem TD;
		public bool QuaDem;
		public DateTime ThuocNgayCong;
		public float Cong;
		public bool DaXN;
		public int HaveINOUT;

		public bool DuyetChoPhepVaoTre;
		public bool DuyetChoPhepRaSom;
		public bool VaoTreKoTruCong; //ver 4.0.0.4	
		public bool RaaSomKoTruCong; //ver 4.0.0.4	

		public int OTMin;
		public override string ToString() {
			var temp = "HaveIO:{0} XN:{1} V:{2} R:{3} Ca:{4} Ngay:{5}";

			return string.Format(temp, HaveINOUT, DaXN,
								 (Vao != null) ? Vao.Time.ToString("H:mm") : "", (Raa != null) ? Raa.Time.ToString("H:mm") : "",
								 /*(ThuocCa != null) ? ThuocCa.Code : "",*/ ThuocNgayCong.ToString("d/M"));
		}
	}
}
