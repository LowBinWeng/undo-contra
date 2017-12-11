using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuParallax : MonoBehaviour {

	public float lerpSpeed = 10f;
	public Vector3 parallaxLimit;

	Vector3 origin;
	Vector3 mousePos;
	Vector3 normalizedPos;
	Vector3 newPosition;
	Vector3 lerpTime = new Vector3(0.5f,0.5f,0.5f);

	void Start() {
		origin = this.transform.position;
	}

	// Update is called once per frame
	void Update () {

		if (Screen.fullScreen == false) {
			mousePos = Input.mousePosition;
			normalizedPos.x = mousePos.x / Screen.width;
			normalizedPos.y = mousePos.y / Screen.height;

			lerpTime.x = Mathf.MoveTowards (lerpTime.x, normalizedPos.x, RealTime.deltaTime * lerpSpeed);
			lerpTime.y = Mathf.MoveTowards (lerpTime.y, normalizedPos.y, RealTime.deltaTime * lerpSpeed);

			newPosition.x = origin.x + Mathf.Lerp (-parallaxLimit.x, parallaxLimit.x, lerpTime.x); 
			newPosition.y = origin.y + Mathf.Lerp (-parallaxLimit.y, parallaxLimit.y, lerpTime.y); 
			newPosition.z = origin.z;

			this.transform.position = newPosition;
		}
	}
}
