using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

public class Encrypt {

	static readonly string passwordHash = "breadHorseBatteryAcid";
	static readonly string saltKey 		= "p3pp34X3Y";
	static readonly string VIKey 		= "@2C3d4E5f6g7f8i9";

	public static string EncryptString ( string plainText ) {
		byte[] plainTextBytes 	= Encoding.UTF8.GetBytes( plainText );
		byte[] keyBytes			= new Rfc2898DeriveBytes(passwordHash, Encoding.ASCII.GetBytes(saltKey)).GetBytes(256/8);

		var symmetricKeys = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros};
		var encryptor = symmetricKeys.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

		byte[] cipherTextBytes;

		using (var memoryStream = new MemoryStream() ) {
			using ( var cryptoStream = new CryptoStream( memoryStream, encryptor, CryptoStreamMode.Write ) ) {

				cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length );
				cryptoStream.FlushFinalBlock();
				cipherTextBytes = memoryStream.ToArray();
				cryptoStream.Close();

			}
			memoryStream.Close();
		}

		return Convert.ToBase64String(cipherTextBytes);
	}
}
