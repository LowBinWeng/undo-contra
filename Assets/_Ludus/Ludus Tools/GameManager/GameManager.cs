using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	#region Singleton
	private static GameManager _instance;
	public static GameManager Instance {
		get{
			if ( _instance == null ) _instance = GameObject.FindObjectOfType<GameManager>() as GameManager;
			return _instance;
		}
	}
	#endregion


	private int frameRate	=	60;


	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad( this.gameObject );
		Application.targetFrameRate = frameRate;
	}

	// Quit Function
	void Update() {
		if (Input.GetKeyDown (KeyCode.Escape)) Application.Quit ();
	}


}
