using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
public class Attack : Damageable {

	public float speed = 10f;
	public float lifeTime = 2f;
	public Transform despawnEffect;

	bool initialized = false;
	
	public virtual void OnEnable() {
		
		if (initialized) {
			PoolManager.Pools ["Attacks"].Despawn (this.transform, lifeTime);
		} else {
			initialized = true;
		}
	}

	public override void OnCollisionEnter(Collision other) {
		base.OnCollisionEnter( other );
	}

	public override void Despawn() {
		if ( despawnEffect ) PoolManager.Pools["Attacks"].Spawn( despawnEffect, this.transform.position, Quaternion.identity );
		PoolManager.Pools["Attacks"].Despawn(this.transform );
	}
}
