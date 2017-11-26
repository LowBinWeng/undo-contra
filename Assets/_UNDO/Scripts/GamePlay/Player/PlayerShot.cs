using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class PlayerShot : Attack {
	
	public override void OnEnable() {
		base.OnEnable();
	}

	// Update is called once per frame
	void Update () {
		this.transform.Translate (Vector3.forward * Time.deltaTime * speed, Space.Self);
	}

	public override void OnCollisionEnter(Collision other ) {
		base.OnCollisionEnter( other );
	}


}
