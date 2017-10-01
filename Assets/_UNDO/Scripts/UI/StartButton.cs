using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StartButton : MonoBehaviour {

	public void StartGame() {
		Debug.Log("StartGame");
		TransitionManager.Instance.TransitionOut( 2, "Game" ); 
	}
}
