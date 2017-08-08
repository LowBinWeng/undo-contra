using UnityEngine;
using System.Collections;

// ResourcesManager
// Loads GameManager into scene if not found
// Loads MasterAudio into scene if not found
// Loads PlaylistController into scene if not found

public class ResourcesManager : MonoBehaviour {

	void Start () {

		LoadResource( "GameManager" );
		LoadResource( "MasterAudio" );
		LoadResource( "PlaylistController" );

	}

	void LoadResource( string resource ) {
		// If resource was not found, load a new resource from Resources folder
		if ( GameObject.Find ( resource ) == null ) {
			
			//Debug.Log("<color=yellow>" + resource + " not present in scene. </color> Loading " + resource + "...");
			GameObject r = Resources.Load(resource) as GameObject;
			
			// If loaded into memory, instantiate this GameObject into the scene
			if ( r != null ) {
				Transform t = Instantiate( r.transform, Vector3.zero, Quaternion.identity ) as Transform;
				t.name 		= resource;
				//Debug.Log("<color=lime>" + resource + " was successfully loaded.</color>");
			}
		}
	}

}
