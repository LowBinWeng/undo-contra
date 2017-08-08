using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

	private static PauseManager _instance;
	public static PauseManager Instance {
		get {return _instance;}
	}

	void Awake() {
		if 		(_instance == null ) _instance = this;
		else if (_instance != this ) Destroy( this.gameObject );
	}

	public void TogglePause () {
		if 		( Time.timeScale == 0f) Time.timeScale = 1f;
		else if ( Time.timeScale == 1f) Time.timeScale = 0f;
	}
}
