using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {

	public void GoToMenu() {
		Debug.Log("Return to Menu");
		AudioManager.Instance.StopBGM(true);
		Time.timeScale = 1f;
		TransitionManager.Instance.TransitionOut( 1, "Title" ); 
	}
}
