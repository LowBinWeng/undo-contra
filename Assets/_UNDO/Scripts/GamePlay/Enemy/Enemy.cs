using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float frequency = 1f;
	public float distance = 1f;

	Vector3 newPos = Vector3.zero;
	
	// Update is called once per frame
	void Update () {
		newPos = transform.position;
		newPos.x = (Mathf.Sin (Time.time * frequency) * distance);
		transform.position = newPos;
	}
}
