using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenFlashManager : MonoBehaviour {

	public Image	whiteFlash;
	public Image	redFlash;

	public Color	transparentWhite;
	public Color	transparentRed;

	#region Singleton
	private static ScreenFlashManager _instance;
	public static ScreenFlashManager Instance {
		get{
			if ( _instance == null ) _instance = GameObject.FindObjectOfType<ScreenFlashManager>() as ScreenFlashManager;
			return _instance;
		}
	}
	#endregion
	
	#region Debug
	// Update is called once per frame
	void Update () {
		if 		( Input.GetKeyDown( KeyCode.Keypad1 ) ) FlashWhite();
		else if ( Input.GetKeyDown( KeyCode.Keypad2 ) ) FlashRed();

	}
	#endregion

	void Start() {
		StartCoroutine ( "FadeOutFlash" );
	}

	public void FlashWhite() {
		redFlash.color		=	transparentRed;
		whiteFlash.color	=	Color.white;
	}

	public void FlashRed() {
		whiteFlash.color	=	transparentWhite;
		redFlash.color		=	Color.red;
	}

	IEnumerator FadeOutFlash() {
		while ( true ) {

			whiteFlash.color 	= Color.Lerp ( whiteFlash.color, transparentWhite, Time.deltaTime * 10F );
			redFlash.color 		= Color.Lerp ( redFlash.color, transparentRed, Time.deltaTime * 10F );

			yield return null;
		}
	}



}
