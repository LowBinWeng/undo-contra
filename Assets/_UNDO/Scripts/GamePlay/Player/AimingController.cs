using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingController : MonoBehaviour {

	public Animator anim;
	public Transform origin;
	public Transform crossHair;

	void Update() {

		Vector3 offset = crossHair.position - origin.position;
		offset.Normalize();

		anim.SetFloat("VerticalAim",offset.y);
//		anim.SetFloat("HorizontalAim",offset.x);
	}


}
