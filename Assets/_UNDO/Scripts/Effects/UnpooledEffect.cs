using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class UnpooledEffect : Effect {

	void OnEnable() {


			Invoke("Despawn", lifeTime );
			if ( screenShake ) CameraShakeManager.Instance.SmallCameraShake();
			if ( sfxName != string.Empty ) 
			{
				AudioManager.Instance.Play("event:/"+sfxName, this.transform.position );
			}


	}

	void Despawn() {
		Destroy (this.gameObject);
	}


}
