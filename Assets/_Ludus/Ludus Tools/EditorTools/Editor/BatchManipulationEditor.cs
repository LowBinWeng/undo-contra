using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class BatchManipulationEditor : EditorWindow {
	
	[MenuItem("Ludus/Batch Manipulation Editor")]
	public static void GetMenuEditorWindow() {
		EditorWindow.GetWindow<BatchManipulationEditor>( false, "Batch Manipulation Editor" );
	}

	public string gameObjectName;
	public Vector3 translateValue;


	void OnGUI() {

		TranslateTool();
		SelectionTool();
	}

	/*====================================================================================================
	// SELECTION
	====================================================================================================*/

	void SelectionTool() {
		EditorGUILayout.LabelField( "Batch Selection Editor" );
		gameObjectName = EditorGUILayout.TextField( "GameObject Name", gameObjectName );

		// Transform
		if (GUILayout.Button( "Batch Selection By Name" ) ){

			if ( Selection.activeGameObject != null ) {
				
				Transform[] childTransforms = Selection.activeGameObject.transform.GetComponentsInChildren<Transform>(true);

				List<GameObject> filteredChildObjectsList = new List<GameObject>();

				// Filted by name
				for ( int i = 0; i < childTransforms.Length; i++ ) {
					if ( childTransforms[i].name == gameObjectName ) {
						filteredChildObjectsList.Add( childTransforms[i].gameObject );
					}
				}

				// Convert list to array
				GameObject[] filteredChildObjectsArray =  filteredChildObjectsList.ToArray();

				Selection.objects = filteredChildObjectsArray;
			}
			else {
				Debug.LogError("No GameObjects were selected");
			}

		}
	}

	/*====================================================================================================
	// TRANSLATION
	====================================================================================================*/

	void TranslateTool() {

		EditorGUILayout.LabelField( "Batch Manipulation Editor" );
		translateValue = EditorGUILayout.Vector3Field( "Translate Value", translateValue );

		// Transform
		if (GUILayout.Button( "Batch Translate Transform" ) ){
			GameObject[] go = Selection.gameObjects;
			for ( int i = 0; i < go.Length; i++ ) {
				go[i].GetComponent<Transform>().position += translateValue;
			}
		}

		// Rect
		if (GUILayout.Button( "Batch Translate Rect" ) ){
			GameObject[] go = Selection.gameObjects;
			Vector2	translate2D = Vector2.zero;
			translate2D.x = translateValue.x;
			translate2D.y = translateValue.y;
			for ( int i = 0; i < go.Length; i++ ) {
				
				go[i].GetComponent<RectTransform>().anchoredPosition += translate2D;
			}
		}
	}


}
