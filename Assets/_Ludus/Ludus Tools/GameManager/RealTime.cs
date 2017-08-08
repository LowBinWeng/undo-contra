using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTime : MonoBehaviour {

	#region Singleton
	private static RealTime _instance;
	public static RealTime Instance {
		get {return _instance; }
	}
	#endregion

	void Awake() {
		if(_instance == null) _instance = this;
		if(_instance != this) Destroy(gameObject);
	}

	private float prevRealTime;
	private float thisRealTime;

	void Update () {
		prevRealTime = thisRealTime;
		thisRealTime = Time.realtimeSinceStartup;
	}

	public static float deltaTime {
		get {
			if (Time.timeScale > 0f)  return  Time.deltaTime / Time.timeScale;
			return Time.realtimeSinceStartup - RealTime.Instance.prevRealTime; // Checks realtimeSinceStartup again because it may have changed since Update was called
		}
	}
}
