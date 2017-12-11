using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : ButtonBehaviour, IPointerEnterHandler, IPointerDownHandler {

	public override void OnPointerEnter( PointerEventData eventData ) {
		base.OnPointerEnter (eventData);
	}

	public override void OnPointerDown( PointerEventData eventData ) {
		base.OnPointerDown(eventData);
	}

	public void StartGame() {

		Debug.Log("StartGame");
		AudioManager.Instance.StopBGM(true);
		Time.timeScale = 1f;
		TransitionManager.Instance.TransitionOut( 2, "Game" ); 

	}
}
