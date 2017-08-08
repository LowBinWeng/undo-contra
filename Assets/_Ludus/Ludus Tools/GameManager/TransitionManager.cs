using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {

	public 		Animator 	animator;
	private 	int			sceneToLoad = 0;
	private 	string		sceneToLoadName = string.Empty;
	#region Singleton
	private static TransitionManager _instance;
	public static TransitionManager Instance {
		get {return _instance; }
	}
	#endregion

	void Awake() {
		if(_instance == null) _instance = this;
		if(_instance != this) Destroy(gameObject);
	}

	void Start() {
		SceneManager.sceneLoaded += OnSceneLoaded;
		animator.Play ("TransitionIn");		// Transition in on first load
	}

	// Transition in when level has loaded
	void OnSceneLoaded ( Scene scene, LoadSceneMode loadSceneMode ) {
		sceneToLoadName = default(string);
		Time.timeScale = 1F;				// 	UnPause game
		Invoke("PlayTransitionIn",0.03f);
	}

	void PlayTransitionIn() {
		animator.Play ("TransitionIn");		//	Transition in
	}

	public void TransitionOut( int scene, string sceneName=default(string) ) {
		sceneToLoad = scene;				// 	Set target scene
		sceneToLoadName = sceneName;
		Time.timeScale = 0F;				//	Pause game
		animator.Play ("TransitionOut");	// 	Transition out
	}

	protected void TriggerLoadScene() {
		if ( sceneToLoadName == default(string) ) SceneManager.LoadScene( sceneToLoad );
		else 									SceneManager.LoadScene( sceneToLoadName );
	}
}
