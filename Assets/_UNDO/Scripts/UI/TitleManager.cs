using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleManager : MonoBehaviour {

	private static TitleManager _instance;
	public static TitleManager Instance {get{return _instance;}}

	// Use this for initialization
	void Awake () {
		if ( _instance == null ) _instance = this;
		else if ( _instance != this ) Destroy(this);
	}

	public GameObject root;
	public GameObject startButton;
	public GameObject resumeButton;
	public GameObject menuButton;

	void Start() {
		if ( SceneManager.GetActiveScene().name == "Title" ) {
			startButton.SetActive(true);
			resumeButton.SetActive(false);
			menuButton.SetActive(false);
			root.SetActive(true);
		}
		else if ( SceneManager.GetActiveScene().name == "Game" ) {
			startButton.SetActive(false);
			resumeButton.SetActive(true);
			menuButton.SetActive(true);
			root.SetActive(false);
		}
	}

	void Update() {
		if ( Input.GetKeyDown(KeyCode.Escape ) ) {
			TogglePause();
		}
	}

	public void TogglePause() {
		if ( Time.timeScale == 1f ) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			Time.timeScale = 0f;
			root.SetActive(true);
		}
		else if ( Time.timeScale == 0f ) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Time.timeScale = 1f;
			root.SetActive(false);
		}
	}

	public void Exit() {
		Application.Quit();
	}

}
