using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMusicPlayer : MonoBehaviour {

	public string musicName = "Menu";

	// Use this for initialization
	void Start () {
		Invoke( "PlayMusic",0.1f);
	}

	void PlayMusic() {
		AudioManager.Instance.PlayBGM("event:/"+musicName);		
	}
}
