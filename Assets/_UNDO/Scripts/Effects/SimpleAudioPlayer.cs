using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAudioPlayer : MonoBehaviour {

	public string audio;

	void OnEnable() {
		AudioManager.Instance.Play (audio, this.transform.position);
	}
}
