using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour {

	#region Singleton
	private static CombatManager _instance;
	public static CombatManager Instance {
		get {return _instance; }
	}
	#endregion

	double timer = 0;
	[SerializeField] Text timerLabel;
	[SerializeField] Text finalTimerLabel;

	/*==========================================================================================
	 * Initialization
	 *========================================================================================*/

	void Awake() {
		if(_instance == null) _instance = this;
		if(_instance != this) Destroy(gameObject);
	}

	void OnEnable() {
		StartCoroutine( TimeCounterRoutine());
	}

	IEnumerator TimeCounterRoutine() {
		while(true) {
			timer += (double)Time.deltaTime;
			timerLabel.text = TimeTools.ConvertSecondsToPrecisionStringFormat( timer, "{0}:{1}:{2}","00");
			yield return null;
		}
	}

	public void EndGame() {
		StopAllCoroutines();
		finalTimerLabel.text = timerLabel.text;
		UndoZaiSpawner.Instance.StopSpawning();
		CutsceneManager.Instance.PlayCutscene( Cutscene.Victory );
		AudioManager.Instance.StopBGM(true);
	}

}
