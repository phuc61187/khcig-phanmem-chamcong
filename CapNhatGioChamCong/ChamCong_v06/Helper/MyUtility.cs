using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraPrinting.Native;

namespace ChamCong_v06.Helper {


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

		#region format chuoi string sao cho cung so luong ky tu
		public static String FormatEx(String format, params Object[] varArgs) {
			if (String.IsNullOrEmpty(format)) {
				throw new ArgumentNullException(
						"format",
						"The 'format' string may not be null or empty.");
			}

			return FormatEx(CultureInfo.CurrentUICulture, format, varArgs);
		}

		public static String FormatEx(CultureInfo uiCulture, String format, params Object[] varArgs) {
			if (String.IsNullOrEmpty(format)) {
				throw new ArgumentNullException(
						"format",
						"The 'format' string may not be null or empty.");
			}

			if (null == uiCulture) {
				uiCulture = CultureInfo.CurrentUICulture;
			}

			Regex reTotal = new Regex(@"{(\d+)(,(c)?(-)?(({(\d+)})|(\d+)))?(:(({(\d+)})|([^}]+)))?}");
			MatchCollection matches;

			// retrieve _all_ matches of this RE on the format string.
			//
			matches = reTotal.Matches(format);

			// Only have work to do if there are field place holders
			//
			if ((null != matches) && (0 < matches.Count)) {
				// place holders specified, but none provided?
				//  Probably just a typo, but tell them about it.
				//
				if ((null == varArgs) || (0 == varArgs.Length)) {
					throw new ArgumentNullException(
							"varArgs",
							String.Format(
									CultureInfo.InvariantCulture,
									"You specified {0} formatting placeholder{1} but varArgs is null or empty.",
									matches.Count,
									1 == matches.Count ? "" : "s"));
				}

				// Clone the arguments,
				//  as we will need to extend it for the specially formatted values.
				//
				List<object> extArgs = new List<object>(varArgs);

				// walk matches in reverse order so indexes for early ones don't change before I use them.
				//
				for (int m = matches.Count; --m >= 0; ) {
					// original field format, with possible extensions
					//
					String fieldFormat = matches[m].Groups[0].Value;

					// get the index to the value to be formatted
					//  int.Parse() may throw an Exception if the index string is not an integer.
					//
					int argV = Int32.Parse(matches[m].Groups[1].Value);
					if ((argV < 0) || (varArgs.Length <= argV)) {
						throw new IndexOutOfRangeException(
								String.Format(
										CultureInfo.InvariantCulture,
										"You specified formatting for argument [{0}]"
										+ " but the legal index range is [0 .. {1}] inclusive.",
										argV,
										varArgs.Length - 1));
					}

					// Nothing unusual unless [3], [6] or [11]
					//
					if (String.IsNullOrEmpty(matches[m].Groups[3].Value)        // did they ask for extension: center alignment?
						&& String.IsNullOrEmpty(matches[m].Groups[6].Value)        // did they ask for extension: indirect width?
						&& String.IsNullOrEmpty(matches[m].Groups[11].Value))      // did they ask for extension: indirect format?
					{
						// Nope!  No extensions asked for.
						//  we can leave this format placeholder and the varArgs list alone.
						//
						continue;
					}

					// if they asked for an indirect formatting code,
					//  then we need to calculate what it is.
					// We will need if they are centering,
					//  and we will want to de-indirect it if they are not.
					//
					String formatPart = matches[m].Groups[9].Value;
					if (!String.IsNullOrEmpty(formatPart)) {
						if (!String.IsNullOrEmpty(matches[m].Groups[11].Value)) {
							// get the index to the formatString to be used
							//  int.Parse() may throw an Exception if the index string is not an integer.
							//
							int argF = Int32.Parse(matches[m].Groups[12].Value);
							if ((argF < 0) || (varArgs.Length <= argF)) {
								throw new IndexOutOfRangeException(
										String.Format(
												CultureInfo.InvariantCulture,
												"You specified Indirect formatString for argument [{0}]"
												+ " from [{1}] but the legal index range is [0 .. {2}] inclusive.",
												argV,
												argF,
												varArgs.Length - 1));
							}

							formatPart = String.Format(uiCulture, ":{0}", varArgs[argF]);
						}
					}
					// assert: formatPart is either empty
					//                    or the original ":..." with no indirection
					//                    or the de-indirected ":..." formatting code

					// if we are aligning special
					//
					bool centered = !String.IsNullOrEmpty(matches[m].Groups[3].Value);
					if (centered                                                // did they ask for extension: center
						|| !String.IsNullOrEmpty(matches[m].Groups[6].Value))      // did they ask for extension: indirect width
					{
						// whether direct or indirect, get the non-indirect width
						//
						int width;
						if (!String.IsNullOrEmpty(matches[m].Groups[6].Value)) {
							//  int.Parse() may throw an Exception if the index string is not an integer.
							//
							int argW = Int32.Parse(matches[m].Groups[7].Value);   // index for width

							if ((argW < 0) || (varArgs.Length <= argW)) {
								throw new IndexOutOfRangeException(
										String.Format(
												CultureInfo.InvariantCulture,
												"You specified Indirect Alignment for argument [{0}]"
												+ " from argument {1} but the legal index range is [0 .. {2}] inclusive.",
												argV,
												argW,
												varArgs.Length - 1));
							}

							String indirectWidth = String.Format("{0}", varArgs[argW]);
							if (indirectWidth.StartsWith("c", StringComparison.OrdinalIgnoreCase)) {
								// indirect centering
								//
								centered = true;
								indirectWidth = indirectWidth.Substring(1);
							}
							width = Int32.Parse(indirectWidth);
						}
						else {
							//  int.Parse() may throw an Exception if the alignment string is not an integer.
							//
							width = Int32.Parse(matches[m].Groups[8].Value);
						}
						if (!String.IsNullOrEmpty(matches[m].Groups[4].Value)) {
							width = -width;
						}

						// if centering
						//
						if (centered) {
							// format the final value without alignment padding
							//  but with the optional formatting code string
							//
							String argValue = String.Format(uiCulture, "{0" + formatPart + "}", varArgs[argV]);

							// then pad left and right to center the value representation.
							//
							if (width < 0) {
								width = -width;

								if (argValue.Length < width) {
									// round down for left alignment
									//
									int padding = argValue.Length + ((width - argValue.Length) / 2);
									argValue = argValue.PadLeft(padding).PadRight(width);
								}
							}
							else {
								if (argValue.Length < width) {
									// round up for right alignment
									//
									int padding = argValue.Length + (((width - argValue.Length) + 1) / 2);
									argValue = argValue.PadLeft(padding).PadRight(width);
								}
							}
							// replace the varArgs value to be printed with the new value.
							//  but append the new value on the array, so original remains available.
							//
							argV = extArgs.Count;
							extArgs.Add(argValue);

							// replace original formatting area with the simplest field specifier
							//  pointing to our modified argument value.
							//
							fieldFormat = "{" + argV.ToString() + "," + width.ToString() + "}";
							format = format.Substring(0, matches[m].Groups[0].Index)
									 + fieldFormat
									 + format.Substring(matches[m].Groups[0].Index + matches[m].Groups[0].Length);
							continue;
						}
						else {
							// replace original formatting area with the simplified field specifier
							//  pointing to the original value, as we didn't need to modify it.
							//
							fieldFormat = "{" + matches[m].Groups[1].Value + "," + width.ToString() + formatPart + "}";
							format = format.Substring(0, matches[m].Groups[0].Index)
									 + fieldFormat
									 + format.Substring(matches[m].Groups[0].Index + matches[m].Groups[0].Length);
							continue;
						}
					}
					// assert: formatPart is either empty
					//                    or the original ":..." with no indirection
					//                    or the de-indirected ":..." formatting code
					// assert: alignment is not extended

					// replace original formatting area with the simplified field specifier
					//  pointing to the original value, as we didn't need to modify it.
					//
					fieldFormat = "{" + matches[m].Groups[1].Value + matches[m].Groups[2].Value + formatPart + "}";
					format = format.Substring(0, matches[m].Groups[0].Index)
							 + fieldFormat
							 + format.Substring(matches[m].Groups[0].Index + matches[m].Groups[0].Length);
				}
				// assert: all field place holders are non-indirect

				// put our extended list of arguments in place for the standard formatting call.
				//
				varArgs = extArgs.ToArray();
			}

			return String.Format(uiCulture, format, varArgs);
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