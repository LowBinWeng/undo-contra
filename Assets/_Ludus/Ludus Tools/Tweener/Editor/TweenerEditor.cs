using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tweener))]
public class TweenerEditor : Editor {

	public override void OnInspectorGUI() {
		serializedObject.Update();

		EditorGUILayout.PropertyField( serializedObject.FindProperty("playOnEnable") );
		EditorGUILayout.PropertyField( serializedObject.FindProperty("duration") );

		DrawPositionTween();
		DrawScaleTween();



		serializedObject.ApplyModifiedProperties();
	}

	void DrawPositionTween() {
		// Pos Tween
		SerializedProperty tweenPos = serializedObject.FindProperty("tweenPosition");
		EditorGUILayout.PropertyField(tweenPos);

		if ( tweenPos.boolValue == true ) {
			EditorGUI.indentLevel++;

			SerializedProperty separateAxisPos = serializedObject.FindProperty("separateAxisPos");
			EditorGUILayout.PropertyField(separateAxisPos);

			if ( separateAxisPos.boolValue == true ) {
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.CurveField( serializedObject.FindProperty("positionCurveX"),Color.red, Rect.zero, GUIContent.none, GUILayout.MaxWidth(100f) );
				EditorGUILayout.CurveField( serializedObject.FindProperty("positionCurveY"),Color.green, Rect.zero, GUIContent.none, GUILayout.MaxWidth(100f) );
				EditorGUILayout.CurveField( serializedObject.FindProperty("positionCurveZ"),Color.blue, Rect.zero, GUIContent.none, GUILayout.MaxWidth(100f) );
				EditorGUILayout.EndHorizontal();
			}
			else {
				EditorGUILayout.CurveField( serializedObject.FindProperty("positionCurveAll"),Color.yellow, Rect.zero, GUIContent.none, GUILayout.MaxWidth(100f) );
			}

			EditorGUILayout.PropertyField( serializedObject.FindProperty("startPosition") );
			EditorGUILayout.PropertyField( serializedObject.FindProperty("endPosition") );
			EditorGUI.indentLevel--;

			EditorGUILayout.LabelField( "", GUI.skin.horizontalSlider);
		}	
	}

	void DrawScaleTween() {
		// Scale Tween
		SerializedProperty tweenScale = serializedObject.FindProperty("tweenScale");
		EditorGUILayout.PropertyField(tweenScale);

		if ( tweenScale.boolValue == true ) {
			EditorGUI.indentLevel++;

			SerializedProperty separateAxisScale = serializedObject.FindProperty("separateAxisScale");
			EditorGUILayout.PropertyField(separateAxisScale);

			if ( separateAxisScale.boolValue == true ) {
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.CurveField( serializedObject.FindProperty("scaleCurveX"),Color.red, Rect.zero, GUIContent.none, GUILayout.MaxWidth(100f) );
				EditorGUILayout.CurveField( serializedObject.FindProperty("scaleCurveY"),Color.green, Rect.zero, GUIContent.none, GUILayout.MaxWidth(100f) );
				EditorGUILayout.CurveField( serializedObject.FindProperty("scaleCurveZ"),Color.blue, Rect.zero, GUIContent.none, GUILayout.MaxWidth(100f) );
				EditorGUILayout.EndHorizontal();
			}
			else {
				EditorGUILayout.CurveField( serializedObject.FindProperty("scaleCurveAll"),Color.yellow, Rect.zero, GUIContent.none, GUILayout.MaxWidth(100f) );
			}

			EditorGUILayout.PropertyField( serializedObject.FindProperty("startScale") );
			EditorGUILayout.PropertyField( serializedObject.FindProperty("endScale") );
			EditorGUI.indentLevel--;

			EditorGUILayout.LabelField( "", GUI.skin.horizontalSlider);
		}
	}

}
