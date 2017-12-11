using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler {

	public virtual void OnPointerEnter( PointerEventData eventData ) {
		AudioManager.Instance.Play ("event:/Hover", Vector3.zero);
	}

	public virtual void OnPointerDown( PointerEventData eventData ) {
		AudioManager.Instance.Play ("event:/Confirm", Vector3.zero);
	}
}
