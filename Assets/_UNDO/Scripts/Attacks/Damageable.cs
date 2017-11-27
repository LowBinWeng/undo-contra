using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	public string targetTag = "Enemy";
	public int damage = 5;

	public virtual void OnCollisionEnter(Collision other) {
		if (other.collider != null) {

			if ( targetTag != string.Empty ) {
				if ( other.collider.CompareTag(targetTag)) {
					other.transform.GetComponent<Character> ().TakeHit (damage, other.contacts[0].point);
					Despawn();
				}
			}
			else 
			{
				if ( other.transform.CompareTag("Enemy") || other.transform.CompareTag("Player") ) other.transform.GetComponent<Character> ().TakeHit (damage, other.contacts[0].point);
				Despawn();
			}
		}
	}

	public virtual void Despawn() {
		// This is implemented in attack
	}
}
