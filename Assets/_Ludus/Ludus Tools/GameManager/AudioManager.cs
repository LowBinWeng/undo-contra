using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour {

	#region Singleton
	private static AudioManager _instance;
	public static AudioManager Instance {
		get {return _instance; }
	}
	#endregion

	EventInstance bgm;

	/*==========================================================================================
	 * Initialization
	 *========================================================================================*/

	void Awake() {
		if(_instance == null) _instance = this;
		if(_instance != this) Destroy(gameObject);
	}

	/*==========================================================================================
	 * One Shot
	 *========================================================================================*/

	public void Play( string eventName, Vector3 pos = default(Vector3) ) {
		FMODUnity.RuntimeManager.PlayOneShot( eventName, pos );
	}

	/*==========================================================================================
	 * BGM
	 *========================================================================================*/

	public void PlayBGM( string eventName ) {
		if ( bgm != null ) bgm.stop(STOP_MODE.IMMEDIATE);

		bgm = FMODUnity.RuntimeManager.CreateInstance( eventName );

		if ( bgm != null ) bgm.start();
		else Debug.LogError("BGM instance not found");
	}

	public void StopBGM( bool fade ) {
		if ( bgm != null ) {
			if ( fade == true ) bgm.stop(STOP_MODE.ALLOWFADEOUT);
			else 				bgm.stop(STOP_MODE.IMMEDIATE);
		}
		else Debug.LogError("BGM instance not found");
	}

	public void SetBGM ( float value ) {
		string masterBusString = "bus:/BGM";
		FMOD.Studio.Bus masterBus;

		masterBus = FMODUnity.RuntimeManager.GetBus (masterBusString);

		masterBus.setVolume (value);
	}

	public void SetSFX ( float value ) {
		string masterBusString = "bus:/SFX";
		FMOD.Studio.Bus masterBus;

		masterBus = FMODUnity.RuntimeManager.GetBus (masterBusString);

		masterBus.setVolume (value);


	}

}
