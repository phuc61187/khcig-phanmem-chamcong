using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamCong_v06.DTO {
	public class cCheck {

		public int MaCC;
		public DateTime Time;
		public string Source;
		public int MachineNo;
		public string Type; //I nếu vào, O nếu ra
		public string TypeColumn; // giá trị trong CSDL quy ước

		public override string ToString() {
			var temp = "{0} ({1} {2}): {3}";
			return string.Format(temp, ((Type == "I") ? "INN" : "OUT"), Source, MachineNo, Time.ToString("d/M H:mm"));
		}
	}
	public class cCheckComparer : IComparer<cCheck> {
		public int Compare(cCheck x, cCheck y) {
			return x.Time.CompareTo(y.Time);
			//return 1;
		}
	}
}
