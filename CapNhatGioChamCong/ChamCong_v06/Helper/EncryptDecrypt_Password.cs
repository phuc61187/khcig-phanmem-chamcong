using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ChamCong_v06.Helper {
	public static partial class MyUtility {
		#region mã hóa giải mã
		private const string passPhrase = "PaS5pR@s3";
		private const string saltValue = "s@1TValue";
		private const string hashAlgorithm = "MD5";
		private const int passwordIterations = 2;
		private const string initVector = "@1M2b3D4e5F6g7h8";
		private const int keySize = 256;

		public static string Mahoa(string plainText) //Mã hóa 
		{
			byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
			byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			var password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
			byte[] keyBytes = password.GetBytes(keySize / 8);
			var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
			ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
			var memoryStream = new MemoryStream();
			var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
			cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
			cryptoStream.FlushFinalBlock();
			byte[] cipherTextBytes = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();
			string cipherText = Convert.ToBase64String(cipherTextBytes);
			return cipherText;
		}

		public static string giaima(string cipherText) //Gi?i mã
		{
			byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
			byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
			byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
			var password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
			byte[] keyBytes = password.GetBytes(keySize / 8);
			var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
			ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
			var memoryStream = new MemoryStream(cipherTextBytes);
			var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			var plainTextBytes = new byte[cipherTextBytes.Length];
			int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
			memoryStream.Close();
			cryptoStream.Close();
			string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
			return plainText;
		}
		#endregion
	}
}
