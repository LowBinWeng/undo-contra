using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public int maxHp;
	public int curHp;
	public HealthRenderer hpRenderer;
	public Renderer[] _renderers;
	public GameObject[] _flashers;

	public virtual void TakeHit( int damage, Vector3 point ) {
	
		curHp -= Mathf.Clamp (damage, 0, 100000);
		hpRenderer.UpdateHPRenderer ((float)curHp / (float)maxHp);

	}

	public void FlashCharacter() {

		if ( _renderers.Length > 0 ) {
			StartCoroutine( "FlashRoutine");
		}
	}

	IEnumerator FlashRoutine() {
		for ( int i =0; i< _renderers.Length; i++ ) {
			_renderers[i].materials[0].SetColor ("_EmissionColor", Color.white);
		}  

		for ( int i = 0; i < _flashers.Length;i++ ) {
			_flashers[i].SetActive(true);
		}

		yield return new WaitForSeconds(0.02f);

		for ( int i =0; i< _renderers.Length; i++ ) {
			_renderers[i].materials[0].SetColor ("_EmissionColor", Color.black);
		}  

		for ( int i = 0; i < _flashers.Length;i++ ) {
			_flashers[i].SetActive(false);
		}

		StopCoroutine("FlashRoutine");

	}

}
