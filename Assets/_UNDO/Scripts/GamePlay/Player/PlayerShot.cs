using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class PlayerShot : MonoBehaviour {

	public float speed = 10f;
	public float lifeTime = 2f;
	bool initialized = false;

	void OnEnable() {

		if (initialized) {
			PoolManager.Pools ["Attacks"].Despawn (this.transform, lifeTime);
		} else {
			initialized = true;
		}
	}

	// Update is called once per frame
	void Update () {
		this.transform.Translate (Vector3.forward * Time.deltaTime * speed, Space.Self);
	}

	void OnCollisionEnter(Collision other) {
		if (other.collider != null) {
			other.transform.GetComponent<Character> ().TakeHit (1, other.contacts[0].point);
		}
	}
}
