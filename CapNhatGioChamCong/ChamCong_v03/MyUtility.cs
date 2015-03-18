using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ChamCong_v03 {
	public static class MyUtility {
		public static T DeepClone<T>(T obj) {
			using (var ms = new MemoryStream()) {
				var formatter = new BinaryFormatter();
				formatter.Serialize(ms, obj);
				ms.Position = 0;

				return (T)formatter.Deserialize(ms);
			}
		}

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
				List<Object> extArgs = new List<object>(varArgs);

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

		public static string GetAllValueOfObject(object obj) {
			object propValue = null;
			string name = string.Empty, kq = string.Empty;

			foreach (PropertyInfo prop in obj.GetType().GetProperties()) {
				name = prop.Name;
				propValue = prop.GetValue(obj, null);
				kq += string.Format("{0}:{1};\t", name, propValue);
			}
			return kq + "\n";
		}

		static string passPhrase = "PaS5pR@s3";
		static string saltValue = "s@1TValue";
		static string hashAlgorithm = "MD5";
		static int passwordIterations = 2;
		static string initVector = "@1M2b3D4e5F6g7h8";
		static int keySize = 256;

		public static string Mahoa(string plainText) //Mã hóa 
		{
			byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
			byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
			byte[] keyBytes = password.GetBytes(keySize / 8);
			RijndaelManaged symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
			ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
			cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
			cryptoStream.FlushFinalBlock();
			byte[] cipherTextBytes = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();
			string cipherText = Convert.ToBase64String(cipherTextBytes);
			return cipherText;
		}

		public static string giaima(string cipherText) //Giải mã
		{
			byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
			byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
			byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
			PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
			byte[] keyBytes = password.GetBytes(keySize / 8);
			RijndaelManaged symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
			ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
			MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
			CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			byte[] plainTextBytes = new byte[cipherTextBytes.Length];
			int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
			memoryStream.Close();
			cryptoStream.Close();
			string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
			return plainText;
		}



	}





}