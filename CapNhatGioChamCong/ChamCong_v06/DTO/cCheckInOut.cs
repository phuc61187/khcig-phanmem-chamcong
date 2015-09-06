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
		public ThoiDiem TD;

		#endregion
		#region phần thông tin để xử lý các check vân tay
		public cCheck Vao;
		public cCheck Raa;
		public cCa ThuocCa;
		public List<cCa> DSCaNhanDien;

		public StructTGCa KhoangTGCa;
		public StructCongCa CongTheoCa;
		#endregion
		#region phần thông tin riêng cho đã xử lý
		public bool ChoPhepTre;
		public bool ChoPhepSom;
		public bool VaoTuDo; //ver 4.0.0.4	
		public bool RaaTuDo; //ver 4.0.0.4	

		#endregion
		public int ShiftID;
		//public cCa ThuocCa;
		//public StructTGCa TG;
		public bool QuaDem;

		public override string ToString() {
			var temp = "HaveIO:{0} XN:{1} V:{2} R:{3} Ca:{4} Ngay:{5}";

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
