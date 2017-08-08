using UnityEditor;
using UnityEngine;
using System.Collections;
//using Google2u;

[CustomEditor(typeof(TextLocalizer))]
 public class TextLocalizerEditor : Editor
 {
 	
	 public override void OnInspectorGUI()
	 {
	 	TextLocalizer target = (TextLocalizer)this.target;
		 
		 target.textType = (TextLocalizer.TextType)EditorGUILayout.EnumPopup( "Type", target.textType );
		 
		 // Expose the appropriate enum list
//		switch ( target.textType ) {
//		 case TextLocalizer.TextType.UI: {
//		 	target.uiLocalizationSelection			= (UILocalizationDB.rowIds)EditorGUILayout.EnumPopup( "UI Localization", target.uiLocalizationSelection );
//		}break;
//		 case TextLocalizer.TextType.CharacterNames: {
//		 	target.characterNamesSelection			= (CharacterNamesDB.rowIds)EditorGUILayout.EnumPopup( "Character Names Localization", target.characterNamesSelection );
//		}break;
//		 case TextLocalizer.TextType.CharacterDescriptions: {
//		 	target.characterDescriptionSelection	= (CharacterDescriptionDB.rowIds)EditorGUILayout.EnumPopup( "Character Description Localization", target.characterDescriptionSelection );
//		 }break;
//		}
		 
	 }
 }