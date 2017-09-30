using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeDilationManager : MonoBehaviour {

	#region Singleton
	private static TimeDilationManager _instance;
	public static TimeDilationManager Instance {
		get {return _instance; }
	}
	#endregion

	void Awake() {
		if(_instance == null) _instance = this;
		if(_instance != this) Destroy(gameObject);
	}

	[ContextMenu("ToggleTime")]
	public void ToggleTime() {
		if 		( Time.timeScale == 1f ) Time.timeScale = 0f;
		else if ( Time.timeScale == 0f ) Time.timeScale = 1f;
	}



	public void StartSlowMotion() {
		StopCoroutine("SlowMotion");
		StartCoroutine("SlowMotion");
	}

	IEnumerator SlowMotion() {

		Time.timeScale = 0.02F;

		while ( true ) {

			Time.timeScale = Mathf.MoveTowards( Time.timeScale, 1.0F, Time.deltaTime );

			if ( Time.timeScale >= 0.5F ) {
				Time.timeScale	=	1.0F;
				StopCoroutine("SlowMotion");
			}

			yield return null;
		}
	}

	public GameObject debugCanvas;
	public Text debugText; 
	#if UNITY_EDITOR
	// Debug speed up
	void Update() {
//		if ( Input.GetKeyDown(KeyCode.Space )) {
//			if 		( Time.timeScale == 1f ) Time.timeScale = 2f;
//			else if ( Time.timeScale == 2f ) Time.timeScale = 3f;
//			else if ( Time.timeScale == 3f ) Time.timeScale = 1f;
//
//			if ( Time.timeScale == 1f ) {
//				debugCanvas.SetActive(false);
//			}
//			else {
//				debugCanvas.SetActive(true);
//				debugText.text = Time.timeScale + "X";
//			}
//		}
	}
	#endif
}
