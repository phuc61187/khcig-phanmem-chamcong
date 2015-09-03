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

		#endregion
		#region phần thông tin để xử lý các check vân tay
		public TrangThaiCheck CheckVT;
		public cCheck Vao;
		public cCheck Raa;
		public cCa ThuocCa;
		public List<cCa> CaNhanDien;

		public StructTGCa KhoangTGCa;
		public StructCongCa CongTheoCa;
		#endregion
		#region phần thông tin riêng cho đã xử lý
		public bool DuyetChoPhepVaoTre;
		public bool DuyetChoPhepRaSom;
		public bool VaoTreKoTruCong; //ver 4.0.0.4	
		public bool RaaSomKoTruCong; //ver 4.0.0.4	

		#endregion
		public int ShiftID;
		//public cCa ThuocCa;
		//public StructTGCa TG;
		public ThoiDiem TD;
		public bool QuaDem;
		public float Cong;
		public bool DaXN;


		public int OTMin;
		public override string ToString() {
			var temp = "HaveIO:{0} XN:{1} V:{2} R:{3} Ca:{4} Ngay:{5}";

			return string.Format(temp, CheckVT, DaXN,
								 (Vao != null) ? Vao.Time.ToString("H:mm") : "", (Raa != null) ? Raa.Time.ToString("H:mm") : "",
								 /*(ThuocCa != null) ? ThuocCa.Code : "",*/ ThuocNgayCong.ToString("d/M"));
		}
	}
}
