using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
public class Attack : MonoBehaviour {

	public float speed = 10f;
	public float lifeTime = 2f;
	public string targetTag = "Enemy";

	bool initialized = false;
	
	public virtual void OnEnable() {
		
		if (initialized) {
			PoolManager.Pools ["Attacks"].Despawn (this.transform, lifeTime);
		} else {
			initialized = true;
		}
	}

	public virtual void OnCollisionEnter(Collision other) {
		if (other.collider != null) {
			if ( other.collider.CompareTag(targetTag)) {
				other.transform.GetComponent<Character> ().TakeHit (1, other.contacts[0].point);
			}
		}
	}

	protected void Despawn() {
		PoolManager.Pools["Attacks"].Despawn(this.transform );
	}
}
