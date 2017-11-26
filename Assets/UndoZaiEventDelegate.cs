using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoZaiEventDelegate : MonoBehaviour {

	[SerializeField]UndoZai undoZai;

	public void Attack ( int index ) {
		undoZai.Attack(index);
	}
}
