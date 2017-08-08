using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 1f;
	public Animator anim;

	Vector3 velocity = Vector3.zero;

	// Update is called once per frame
	void Update () {
		ControlMovement ();
	}

	void ControlMovement() {

		velocity.x = Input.GetAxis ("Horizontal");

		transform.Translate (velocity * moveSpeed * Time.deltaTime);

		anim.SetFloat ("Direction", velocity.x);
		anim.SetFloat ("Speed", Mathf.Abs(velocity.x));

	}

}
