using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public enum Cutscene {Intro, Victory, GameOver}

public class CutsceneManager : MonoBehaviour {

	[SerializeField] PlayableDirector director;
	[SerializeField] TimelineAsset introTimeline;
	[SerializeField] TimelineAsset gameOverTimeline;


	private static CutsceneManager _instance;
	public static CutsceneManager Instance {
		get {return _instance;}
	}

	void Awake() {
		if ( _instance == null ) _instance = this;
		else if ( _instance != this ) Destroy(this);
	}

	void OnEnable() {
		PlayCutscene (Cutscene.Intro);
	}

	public void PlayCutscene( Cutscene _cutscene ) {
		switch( _cutscene ) {
		case Cutscene.Intro: director.Play( introTimeline, DirectorWrapMode.None ); break;
		case Cutscene.GameOver: director.Play( gameOverTimeline, DirectorWrapMode.Hold ); break;
		}
	}
}
