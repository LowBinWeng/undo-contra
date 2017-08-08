using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("LoadLevel", 0.5F);
	}
	
	void LoadLevel() {
		SceneManager.LoadScene(1);
	}
}
