using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public int maxHp;
	public int curHp;
	public HealthRenderer hpRenderer;

	void Awake() {
	}

	public void TakeHit( int damage, Vector3 point ) {
	
		curHp -= Mathf.Clamp (damage, 0, 100000);
		hpRenderer.UpdateHPRenderer ((float)curHp / (float)maxHp);

	}



}
