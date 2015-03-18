using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ChamCong_v03.DTO;

namespace ChamCong_v03 {
	public class ThamSo {

		public static int currUserID = int.MinValue;
		public static string currUserAccount = string.Empty;

		public static readonly TimeSpan _0gio = TimeSpan.Zero;
		public static readonly TimeSpan _01giay = new TimeSpan(0, 0, 1);
		public static readonly TimeSpan _05phut = new TimeSpan(0, 5, 0);
		public static readonly TimeSpan _10phut = new TimeSpan(0, 10, 0);
		public static readonly TimeSpan _30phut = new TimeSpan(0, 30, 0);
		public static readonly TimeSpan _02gio = new TimeSpan(2, 0, 0);
		public static readonly TimeSpan _04gio = new TimeSpan(4, 0, 0);
		public static readonly TimeSpan _07gio45ph = new TimeSpan(7, 45, 0);
		public static readonly TimeSpan _08gio = new TimeSpan(8, 0, 0);
		public static readonly TimeSpan _08gio1giay = new TimeSpan(8, 0, 1);
		public static readonly TimeSpan _12gio = new TimeSpan(12, 0, 0);
		public static readonly TimeSpan _16gio = new TimeSpan(16, 0, 0);
		public static readonly TimeSpan _1ngay = new TimeSpan(1, 0, 0, 0);
		public static readonly TimeSpan _04h30 = new TimeSpan(4, 30, 0);
		public static readonly TimeSpan _05h45 = new TimeSpan(5, 45, 0);
		public static readonly TimeSpan _07h00 = new TimeSpan(7, 0, 0);
		public static readonly TimeSpan _07h30 = new TimeSpan(7, 30, 0);
		public static readonly TimeSpan _13h45 = new TimeSpan(13, 45, 0);
		public static readonly TimeSpan _18h00 = new TimeSpan(18, 0, 0);
		public static readonly TimeSpan _20h00 = new TimeSpan(20, 0, 0);
		public static readonly TimeSpan _21h45 = new TimeSpan(21, 45, 0);
		public static readonly TimeSpan _24h00 = new TimeSpan(24, 0, 0);


		public const string nameVao = "TimeStrVao";
		public const string nameRaa = "TimeStrRaa";
		public const string nameNgay = "TimeStrNgay";
		public const string nameThu = "TimeStrThu";

		public static void VeCheckBox_CheckAll(DataGridView grid, CheckBox checkBox, EventHandler checkAll_CheckedChanged, Point location) {
			Rectangle rect = grid.GetCellDisplayRectangle(0, -1, true);
			checkBox.Size = new Size(18, 18); checkBox.Location = new Point(rect.Location.X + location.X, rect.Location.Y + location.Y);
			checkBox.CheckedChanged += checkAll_CheckedChanged;
			grid.Controls.Add(checkBox);
		}


	}
}
