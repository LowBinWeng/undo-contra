using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using Google2u;


public class TextLocalizer : Localizer {
	
	private Text					text;
	public enum TextType {UI, CharacterNames, CharacterDescriptions}
	public TextType	textType;
	
//	public UILocalizationDB.rowIds			uiLocalizationSelection;
//	public CharacterNamesDB.rowIds			characterNamesSelection;
//	public CharacterDescriptionDB.rowIds	characterDescriptionSelection;
	
	// Localize text on awake
	void OnEnable() {
		if ( text ) LocalizeText();
		else {
			text = GetComponent<Text>();
			LocalizeText();
		}
	}
	
	// Localize the text based on UI_Localization
	protected override void LocalizeText() {

//		switch ( textType ) {
//		case TextType.UI: {
//			text.text	 = UILocalizationDB.Instance.GetRow( uiLocalizationSelection.ToString() ).GetStringDataByIndex(PlayerPrefs.GetInt("Language"));
//		}break;
//			
//		case TextType.CharacterNames: {
//			text.text	 = CharacterNamesDB.Instance.GetRow( characterNamesSelection.ToString() ).GetStringDataByIndex(PlayerPrefs.GetInt("Language"));
//		}break;
//			
//		case TextType.CharacterDescriptions: {
//			text.text	 = CharacterDescriptionDB.Instance.GetRow( characterDescriptionSelection.ToString() ).GetStringDataByIndex(PlayerPrefs.GetInt("Language"));
//		}break;		
//		}

	}
	

	
}
