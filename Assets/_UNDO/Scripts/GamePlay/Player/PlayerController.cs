﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
public class PlayerController : MonoBehaviour {

	[Header("Character")]
	public float moveSpeed = 1f;
	public Animator anim;
	public Transform spawnPoint;
	public GameObject bulletPrefab;
	public float cooldownDuration = 0.1f;
	float cooldownTime = 0f;


	[Header("CrossHair")]
	public Transform crossHair;
	public Vector2 minBounds;
	public Vector2 maxBounds;
	public float aimSpeed = 1f;
	Vector3 aimVelocity = Vector3.zero;
	Vector3 lastMousePos = Vector3.zero;
	Vector3 aimDelta = Vector3.zero;

	Vector3 velocity = Vector3.zero;

	void Awake() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Update is called once per frame
	void Update () {
		ControlMovement ();
		ControlCrossHair ();
		ControlAttack ();
		CountCooldown ();
	}

	void ControlMovement() {

		velocity.x = Input.GetAxis ("Horizontal");

		transform.Translate (velocity * moveSpeed * Time.deltaTime);

		anim.SetFloat ("Direction", velocity.x);
		anim.SetFloat ("Speed", Mathf.Abs(velocity.x));

	}

	void ControlCrossHair() {
		aimDelta = Input.mousePosition - lastMousePos;
		aimDelta.Normalize ();

		aimVelocity.x = aimDelta.x;
		aimVelocity.y = aimDelta.y;

		aimVelocity.x = Input.GetAxis ("Mouse X");
		aimVelocity.y = Input.GetAxis ("Mouse Y");

		crossHair.Translate (aimVelocity * Time.deltaTime * aimSpeed);

		lastMousePos = Input.mousePosition;
	}

	void ControlAttack() {
		if (Input.GetMouseButton (0) && cooldownTime <= 0f) {
			Transform t = PoolManager.Pools ["Attacks"].Spawn (bulletPrefab, spawnPoint.position, spawnPoint.rotation);
			t.LookAt (crossHair);
			cooldownTime = cooldownDuration;
		}
	}

	void CountCooldown() {
		if (cooldownTime > 0f) cooldownTime -= Time.deltaTime;
	}
}
