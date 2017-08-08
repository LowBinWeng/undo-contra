#pragma warning disable 0219
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

using System;
using System.Text;
using System.Security.Cryptography;

public class GameDataEditor : EditorWindow {

	public GameData gameData;
	public bool encryptData = true;

	[MenuItem("Ludus/GameData")]
	public static void  ShowWindow () {
		EditorWindow.GetWindow(typeof(GameDataEditor));
	}

	void OnGUI() {

		encryptData = EditorGUILayout.Toggle( "Encrypt Data", encryptData );			// Show option to Encrypt Data

		if ( gameData != null ) {
			// Serialize GameData and make modifiable
			SerializedObject serializedObject = new SerializedObject( this );
			SerializedProperty property = serializedObject.FindProperty( "gameData" );
			EditorGUILayout.PropertyField( property, true );
			serializedObject.ApplyModifiedProperties();
		}
		else {
			LoadGameData();																// Load game data to editor
		}

		if ( GUILayout.Button("Save")) {
			SaveGameData();
		}
	}

	// Load game data to editor
	void LoadGameData() {

		string path = Path.Combine(Application.streamingAssetsPath, "data.json");				// Get data path

		if ( File.Exists( path ) ) {
			string jsonData = File.ReadAllText( path );											// Read string from text file

			// Decryption
			string decryptedData = Decrypt.DecryptString(jsonData);								// Decrypt string from text file

			if ( encryptData ) 	gameData = JsonUtility.FromJson<GameData>( decryptedData );		// Convert string to GameData (Decrypted JSON)
			else 				gameData = JsonUtility.FromJson<GameData>( jsonData );			// Convert string to GameData (JSON)
		}
		else {
			GameData _gameData = new GameData("Default", 10, 10f, Vector3.left );				// Construct Default Value Game Data if none exists
			SaveGameData();																		// Save new GameData to disk
		}

	}

	// Save GameData to Disk
	void SaveGameData() {
		string path = Path.Combine(Application.streamingAssetsPath, "data.json");				// Get Data Path

		string jsonData = JsonUtility.ToJson(gameData);											// Convert GameData to JSON string

		// Encryption
		string encryptedData = Encrypt.EncryptString(jsonData);									// Encrypt JSON

		if ( encryptData )	File.WriteAllText(path, encryptedData);								// Save encrypted string to text file
		else  				File.WriteAllText(path, jsonData);									// Save JSON string to text file

	}
}
