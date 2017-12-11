using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : ButtonBehaviour, IPointerEnterHandler, IPointerDownHandler {

	public override void OnPointerEnter( PointerEventData eventData ) {
		base.OnPointerEnter (eventData);
	}

	public override void OnPointerDown( PointerEventData eventData ) {
		base.OnPointerDown(eventData);
	}

	public void GoToMenu() {
		Debug.Log("Return to Menu");
		AudioManager.Instance.StopBGM(true);
		Time.timeScale = 1f;
		TransitionManager.Instance.TransitionOut( 1, "Title" ); 
	}
}
