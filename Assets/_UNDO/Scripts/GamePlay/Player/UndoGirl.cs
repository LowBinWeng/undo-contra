using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoGirl : Character {

	public Transform meshRoot;

	public override void Death() {
		SetCursor(true);
		_isDead = true;
		meshRoot.rotation = Quaternion.identity;
		CutsceneManager.Instance.PlayCutscene( Cutscene.GameOver );
	}

	void SetCursor ( bool active ) {
		if ( active == false ) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
