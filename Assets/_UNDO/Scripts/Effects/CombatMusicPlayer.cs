using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMusicPlayer : MonoBehaviour {

	public string combatMusicName = "CombatMusic 1";

	// Use this for initialization
	void Start () {
		Invoke( "PlayMusic",0.1f);
	}

	void PlayMusic() {
		AudioManager.Instance.PlayBGM("event:/"+combatMusicName);		
	}

}
