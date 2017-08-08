#pragma warning disable 0414
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour {
	
	public string gameDataFileName = "data.json";
	private GameData gameData;

	[ContextMenu("Load Game Data")]
	void LoadGameData() {

		string path = Path.Combine ( Application.streamingAssetsPath, gameDataFileName );	// Get Data Path


		if ( File.Exists(path)) {
			string jsonData = File.ReadAllText( path ); 									// Read encrypted string data from file

			// Decryption
			string decryptedText = Decrypt.DecryptString(jsonData);							// Decrypt string

			gameData = JsonUtility.FromJson<GameData>(decryptedText); 						// Populate GameData with decrypted data
			Debug.Log("Game Data Loaded");
		}
		else {
			Debug.LogError("File Does Not Exist: " + path ); 								// File does not exist
		}
	}

	[ContextMenu("Save Game Data")]
	void SaveGameData() {
		
		GameData gameData = new GameData("Default", 1, 2f, Vector3.one);					// Construct default value GameData
		Debug.LogWarning("Saving default values");

		string jsonData = JsonUtility.ToJson(gameData);										// Convert GameData to Json
			
		// Encryption
		string encryptedText = Encrypt.EncryptString(jsonData);								// Encrypt string

		string path = Path.Combine( Application.streamingAssetsPath, gameDataFileName );	// Get Data Path
		File.WriteAllText( path, encryptedText);											// Write encrypted string to file
		Debug.Log("Game Data Saved");
	}
}
