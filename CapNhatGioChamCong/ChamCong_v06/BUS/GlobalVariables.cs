using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamCong_v06.DTO;
using ChamCong_v06.Helper;

namespace ChamCong_v06.BUS {
	public class GlobalVariables {
		public static List<cNhomCa> DSNhomCa = new List<cNhomCa>();
		public static readonly TimeSpan _0gio = TimeSpan.Zero;
		public static readonly TimeSpan _01giay = new TimeSpan(0, 0, 1);
		public static readonly TimeSpan _01phut = new TimeSpan(0, 1, 0);
		public static readonly TimeSpan _05phut = new TimeSpan(0, 5, 0);
		public static readonly TimeSpan _10phut = new TimeSpan(0, 10, 0);
		public static readonly TimeSpan _30phut = new TimeSpan(0, 30, 0);
		public static readonly TimeSpan _02gio = new TimeSpan(2, 0, 0);
		public static readonly TimeSpan _03gio = new TimeSpan(3, 0, 0);
		public static readonly TimeSpan _04gio = new TimeSpan(4, 0, 0);
		public static readonly TimeSpan _07gio45ph = new TimeSpan(7, 45, 0);
		public static readonly TimeSpan _08gio = new TimeSpan(8, 0, 0);
		public static readonly TimeSpan _08gio1giay = new TimeSpan(8, 0, 1);
		public static readonly TimeSpan _12gio = new TimeSpan(12, 0, 0);
		public static readonly TimeSpan _16gio = new TimeSpan(16, 0, 0);
		public static readonly TimeSpan _1ngay = new TimeSpan(1, 0, 0, 0);
		public static readonly TimeSpan _04h30 = new TimeSpan(4, 30, 0);
		public static readonly TimeSpan _05h45 = new TimeSpan(5, 45, 0);
		public static readonly TimeSpan _06h00 = new TimeSpan(6, 0, 0);
		public static readonly TimeSpan _07h00 = new TimeSpan(7, 0, 0);
		public static readonly TimeSpan _07h30 = new TimeSpan(7, 30, 0);
		public static readonly TimeSpan _13h45 = new TimeSpan(13, 45, 0);
		public static readonly TimeSpan _18h00 = new TimeSpan(18, 0, 0);
		public static readonly TimeSpan _20h00 = new TimeSpan(20, 0, 0);
		public static readonly TimeSpan _21h45 = new TimeSpan(21, 45, 0);
		public static readonly TimeSpan _22h00 = new TimeSpan(22, 0, 0);
		public static readonly TimeSpan _24h00 = new TimeSpan(24, 0, 0);
		public static readonly FromToTimeSpan NightTime22h = new FromToTimeSpan { From = XL2._22h00, To = XL2._06h00 };

	}
}
