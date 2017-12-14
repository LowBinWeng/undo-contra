using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AudioCategory {BGM,SFX}

public class AudioController : MonoBehaviour {

	public AudioCategory audioCategory = AudioCategory.BGM;
	public Slider slider;

	void OnEnable() {

		switch (audioCategory) {
		case AudioCategory.BGM:
			if (PlayerPrefs.HasKey ("BGM") == false) {
				PlayerPrefs.SetFloat ("BGM", 0.5f);
				AudioManager.Instance.SetBGM (0.5f);
				slider.value = 0.5f;
			} else {
				float value = PlayerPrefs.GetFloat ("BGM");
				AudioManager.Instance.SetBGM (value);
				slider.value = value;	
			}
			break;

		case AudioCategory.SFX:
			if (PlayerPrefs.HasKey ("SFX") == false) {
				PlayerPrefs.SetFloat ("SFX", 0.5f);
				AudioManager.Instance.SetSFX (0.5f);
				slider.value = 0.5f;
			} else {
				float value = PlayerPrefs.GetFloat ("SFX");
				AudioManager.Instance.SetSFX (value);
				slider.value = value;	
			}
			break;
		}

		slider.onValueChanged.AddListener ( 
			delegate { UpdateAudioManager ();}
		);
	}


	void OnDisable() { slider.onValueChanged.RemoveListener (delegate {
			UpdateAudioManager (); }
		);
	}

	
	void UpdateAudioManager() {
		switch (audioCategory) {
		case AudioCategory.BGM:
			PlayerPrefs.SetFloat ("BGM", slider.value);
			AudioManager.Instance.SetBGM (slider.value);
			break;
		case AudioCategory.SFX:
			PlayerPrefs.SetFloat ("SFX", slider.value);
			AudioManager.Instance.SetSFX (slider.value);
			break;
		}
	}
}
