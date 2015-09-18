using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamCong_v06.DTO {
	public class cKhaiBaoVang
	{
		public int MaCC;
		public DateTime Ngay;
		public float Workingday;
		public float WorkingTime;
		public string AbsentCode;
		public string KyHieuVang
		{
			get
			{
				string kq = AbsentCode;
				if (Math.Abs(Workingday - 0.5f) < 0.01) kq = "N" + AbsentCode;
				return kq;
			}
		}
	}
}
