using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HealthBarOwner {PLAYER,BOSS}

public class HealthRenderer : MonoBehaviour {

	public HealthBarOwner character;
	public Image hpFillBar;

	public void UpdateHPRenderer(float hpPercentage) {
		hpFillBar.fillAmount = hpPercentage;
		if ( hpFillBar.fillAmount == 0f ) {
			GameOverManager.Instance.ShowGameOver();
		}
	}
}
