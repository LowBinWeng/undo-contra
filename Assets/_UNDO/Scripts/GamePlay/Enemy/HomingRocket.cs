using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : Attack {

	Transform target;
	public float homingDuration = 1.5f;
	public float homingSpeed = 360f;
	float homingTime = 0f;
	[SerializeField]Transform rotRef;

	public override void OnEnable() {
		base.OnEnable();
		rotRef.SetParent(null);
	}

	public void StartHoming( Transform t ) {
		target = t;
		homingTime = homingDuration;
	}

	// Update is called once per frame
	void Update () {

		if ( homingTime > 0f ) {
			homingTime -= Time.deltaTime;
			rotRef.position = this.transform.position;
			rotRef.LookAt( target.root, Vector3.forward );
			this.transform.rotation = Quaternion.RotateTowards ( this.transform.rotation, rotRef.rotation, Time.deltaTime * homingSpeed );
		}

		this.transform.Translate (Vector3.forward * Time.deltaTime * speed, Space.Self);

		if ( this.transform.position.y < 0f ) Despawn();
	}

	public override void OnCollisionEnter(Collision other ) {
		base.OnCollisionEnter( other );
	}

}
