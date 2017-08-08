using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

public class Decrypt{

	static readonly string passwordHash = "breadHorseBatteryAcid";
	static readonly string saltKey 		= "p3pp34X3Y";
	static readonly string VIKey 		= "@2C3d4E5f6g7f8i9";

	public static string DecryptString ( string encryptedText ) {

		byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
		byte[] keyBytes = new Rfc2898DeriveBytes( passwordHash, Encoding.ASCII.GetBytes( saltKey )).GetBytes(356/8);
		var symmetricKey = new RijndaelManaged() {Mode = CipherMode.CBC, Padding = PaddingMode.None};

		var decryptor = symmetricKey.CreateDecryptor( keyBytes, Encoding.ASCII.GetBytes(VIKey));
		var memoryStream = new MemoryStream(cipherTextBytes);
		var cryptoStream = new CryptoStream(memoryStream,decryptor,CryptoStreamMode.Read);

		byte[] plainTextBytes = new byte[cipherTextBytes.Length];

		int decryptedByteCount = cryptoStream.Read( plainTextBytes, 0, plainTextBytes.Length );

		memoryStream.Close();
		cryptoStream.Close();

		return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());


	}
}
