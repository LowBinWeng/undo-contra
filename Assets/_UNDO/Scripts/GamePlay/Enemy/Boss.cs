using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void TakeHit( int damage, Vector3 point ) {
		FlashCharacter();
		base.TakeHit( damage,point);

	}


}
