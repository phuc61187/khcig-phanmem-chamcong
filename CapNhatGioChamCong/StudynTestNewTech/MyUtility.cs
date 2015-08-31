using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraPrinting.Native;

namespace StudynTestNewTech {


	#region COMPARE

	public static class Compare {
		public static IEnumerable<T> DistinctBy<T, TIdentity>(this IEnumerable<T> source, Func<T, TIdentity> identitySelector) {
			return source.Distinct(Compare.By(identitySelector));
		}

		public static IEqualityComparer<TSource> By<TSource, TIdentity>(Func<TSource, TIdentity> identitySelector) {
			return new DelegateComparer<TSource, TIdentity>(identitySelector);
		}

		private class DelegateComparer<T, TIdentity> : IEqualityComparer<T> {
			private readonly Func<T, TIdentity> identitySelector;

			public DelegateComparer(Func<T, TIdentity> identitySelector) {
				this.identitySelector = identitySelector;
			}

			public bool Equals(T x, T y) {
				return Equals(identitySelector(x), identitySelector(y));
			}

			public int GetHashCode(T obj) {
				return identitySelector(obj).GetHashCode();
			}
		}
	}

	#region comment_ver5
	//public class cCheckComparer : IComparer<cCheck> {
	//	public int Compare(cCheck x, cCheck y) {
	//		return x.Time.CompareTo(y.Time);
	//		//return 1;
	//	}
	//}
	//public class cCheckInOutComparer : IComparer<cCheckInOut> {
	//	public int Compare(cCheckInOut x, cCheckInOut y) {
	//		return x.TimeDaiDien.CompareTo(y.TimeDaiDien);
	//		//return 1;
	//	}
	//}

	//public class cLoaiVangComparer : IComparer<cLoaiVang> {
	//	public int Compare(cLoaiVang x, cLoaiVang y) {
	//		return x.Ngay.CompareTo(y.Ngay);
	//		//return 1;
	//	}
	//}

	//public class cTemp1Comparer : IComparer<structPCTC>
	//{
	//	public int Compare(structPCTC x, structPCTC y)
	//	{
	//		return x.Ngay.CompareTo(y.Ngay);
	//	}
	//}

	//public class cTempComparer : IComparer<structPCDB>
	//{
	//	public int Compare(structPCDB x, structPCDB y)
	//	{
	//		return x.Ngay.CompareTo(y.Ngay);
	//	}
	//}
	#endregion

	#endregion

	public static partial class MyUtility {

		public static float Truncate(this float value, int digits) {
			double mult = Math.Pow(10.0, digits);
			double result = Math.Truncate(mult * value) / mult;
			return (float)result;
		}
		public static float Truncate(this double value, int digits) {
			double mult = Math.Pow(10.0, digits);
			double result = Math.Truncate(mult * value) / mult;
			return (float)result;
		}

		#region deep clone : copy object to new object with same data
		public static T DeepClone<T>(T obj) {
			using (var ms = new MemoryStream()) {
				var formatter = new BinaryFormatter();
				formatter.Serialize(ms, obj);
				ms.Position = 0;

				return (T)formatter.Deserialize(ms);
			}
		}
		#endregion


		#region //IsUseful // lấy giá trị các properties trong object, dùng để test

		public static string GetAllValueOfObject(object obj)
		{
			object propValue = null;
			string name = String.Empty, kq = String.Empty;

			foreach (PropertyInfo prop in obj.GetType().GetProperties())
			{
				name = prop.Name;
				propValue = prop.GetValue(obj, null);
				kq += String.Format("{0}:{1};\t", name, propValue);
			}
			return kq + "\n";
		}

		#endregion


		public static int GetIndexOf_DayInWeek(DateTime dateTime) {
			switch (dateTime.DayOfWeek) {
				case DayOfWeek.Sunday:
					return 0;
				case DayOfWeek.Monday:
					return 1;
				case DayOfWeek.Tuesday:
					return 2;
				case DayOfWeek.Wednesday:
					return 3;
				case DayOfWeek.Thursday:
					return 4;
				case DayOfWeek.Friday:
					return 5;
				case DayOfWeek.Saturday:
					return 6;

			}
			return 0;
		}

		public static void EnableDisableControl(bool isEnable, params  Control[] controls) {
			foreach (var control in controls)
				control.Enabled = isEnable;
		}

		public static void ClearControlText(params Control[] controls) {
			foreach (var control in controls)
				control.Text = String.Empty;
		}

		public static void ClearButtonEditText(params object[] controls)
		{
			foreach (var control in controls)
			{
				((DevExpress.XtraEditors.ButtonEdit)control).Text = string.Empty;
			}
		}

		public static void UpdateControl(params Control[] controls) {
			foreach (var control in controls) {
				control.Update();
			}
		}

		public static void CheckedCheckBox(bool IsChecked, params CheckBox[] controls)//IsUseful
		{
			foreach (var control in controls)
			{
				control.Checked = IsChecked;
			}
		}

		public static void Swap<T>(ref T lhs, ref T rhs) {
			T temp = lhs;
			lhs = rhs;
			rhs = temp;
		}

		public static DateTime LastDayOfMonth(DateTime dayInMonth)//IsUseful
		{
			return  new DateTime(dayInMonth.Year, dayInMonth.Month, DateTime.DaysInMonth(dayInMonth.Year, dayInMonth.Month));
		}
		public static DateTime FirstDayOfMonth(DateTime dayInMonth)//IsUseful
		{
			return  new DateTime(dayInMonth.Year, dayInMonth.Month, 1);
		}

		public static bool KiemtraNgayLaCN(DateTime ngayVang)//IsUseful
		{
			return (ngayVang.DayOfWeek == DayOfWeek.Sunday);
		}

		public static DataTable Array_To_DataTable(string tableName, IEnumerable<int> intArray) {
			DataTable tableIntArray = new DataTable(tableName);
			tableIntArray.Columns.Add("IntArr", typeof(int));
			intArray.ForEach(x => tableIntArray.Rows.Add(x));
			return tableIntArray;
		}

		#region IsUseful

		/// <summary>
		/// Chia đoạn thời gian thành các đoạn ngắn, mỗi đoạn tối đa là 1 tháng
		/// </summary>
		/// <param name="NgayBd"></param>
		/// <param name="NgayKt"></param>
		/// <param name="ArrDoanThoigian"></param>
		public static void ChiaDoanThoiGian(DateTime NgayBd, DateTime NgayKt, out List<List<DateTime>> ArrDoanThoigian)
		{
			ArrDoanThoigian = new List<List<DateTime>>();
			DateTime cursorDate = new DateTime(NgayBd.Year, NgayBd.Month, NgayBd.Day);

			while (cursorDate.Month < NgayKt.Month && cursorDate.Year <= NgayKt.Year) // vẫn chưa phải tháng cuối cùng trong khoảng thời gian
			{
				DateTime ngayBD_Doan, ngayKT_Doan;
				ngayBD_Doan = new DateTime(cursorDate.Year, cursorDate.Month, cursorDate.Day); // ngày đầu tháng bị dang dở hoặc ngày 1 đầu tháng
				ngayKT_Doan = LastDayOfMonth(ngayBD_Doan); // ngày cuối tháng
				ArrDoanThoigian.Add(new List<DateTime> {ngayBD_Doan, ngayKT_Doan});
				cursorDate = FirstDayOfMonth(cursorDate).AddMonths(1); // đưa ngày đầu dang dở về đầu tháng rồi mới add thêm 1 tháng mới. VD: 16 -> 1 rồi mới add monnth
			}
			// ra khỏi vòng lặp là tháng cuối có thể bị dang dở cursorDate.Month = NgayKT.Month . VD: 01/01/2015 -16/01/2015
			ArrDoanThoigian.Add(new List<DateTime> {cursorDate, NgayKt});
		}

		#endregion

	}






	#region IsUseful

	public struct WarningMessage
	{
		public int MaCC;
		public string MaNV;
		public string TenNV;
		public DateTime Ngay;
		public string NoiDung;
	}

	public struct structCotTong
	{
		public int ColIndex;
		public string NumberFormat;
	}

	public struct Error
	{
		public string L;
		public string ND;
	}

	public struct Warning
	{
		public string CB;
		public string ND;
	}

	public struct ADDCOL
	{
		public string ColName;
		public string address;
	}

	#endregion

}