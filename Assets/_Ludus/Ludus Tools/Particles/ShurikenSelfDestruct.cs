using UnityEngine;
using System.Collections;
using PathologicalGames;
public class ShurikenSelfDestruct : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		if (!GetComponent<ParticleSystem>().IsAlive()){
			PoolManager.Pools["Effects"].Despawn(this.transform);
		}
	}
}
