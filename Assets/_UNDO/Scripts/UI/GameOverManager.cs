using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

	private static GameOverManager _instance;
	public static GameOverManager Instance {
		get { return _instance; }
	}

	void Awake() {
		if ( _instance == null ) _instance = this;
		else if ( _instance != this ) Destroy( gameObject );
	}

	public GameObject canvas;

	public void ShowGameOver() {
		canvas.SetActive(true);
	}
}
