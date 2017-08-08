using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour {

	public Transform target;
	public Transform spriteRoot;

	// Update is called once per frame
	void LateUpdate () {
		spriteRoot.LookAt (target);
	}
}
