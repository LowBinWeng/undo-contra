using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {

	void OnEnable() {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
