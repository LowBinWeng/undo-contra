using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class Effect : MonoBehaviour {

	bool isInitialized = false;
	public float lifeTime = 1f;
	public bool screenShake = false;

	void OnEnable() {

		if ( !isInitialized ) {
			isInitialized = true;
		}
		else {
			Invoke("Despawn", lifeTime );
			if ( screenShake ) CameraShakeManager.Instance.SmallCameraShake();
		}

	}

	void Despawn() {
		PoolManager.Pools["Attacks"].Despawn( this.transform );
	}


}
