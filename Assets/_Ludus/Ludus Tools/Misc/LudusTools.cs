using UnityEngine;
using System.Collections;

public class LudusTools : MonoBehaviour {

	// Set layers to GameObject recursively
	public static void SetLayerRecursively( GameObject obj, int layer) {
		if ( obj == null ) return;
		obj.layer = layer;
		
		foreach (Transform child in obj.transform) {
			SetLayerRecursively( child.gameObject, layer);
		}
	}
}

