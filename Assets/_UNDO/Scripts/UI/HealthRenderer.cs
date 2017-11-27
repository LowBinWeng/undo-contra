using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HealthBarOwner {PLAYER,BOSS}

public class HealthRenderer : MonoBehaviour {

	public HealthBarOwner character;
	public Image hpFillBar;
	public float actualMin = 0f;
	public float actualMax = 1f;

	public void UpdateHPRenderer(float hpPercentage) {
		// Remap
		//output = output_start + ((output_end - output_start) / (input_end - input_start)) * (input - input_start)

		float actualValue = actualMin + (( actualMax - actualMin) / (1f-0f)) * ( hpPercentage - 0f);
		hpFillBar.fillAmount = actualValue;

		if ( hpFillBar.fillAmount == actualMin ) {
			GameOverManager.Instance.ShowGameOver();

			if ( character == HealthBarOwner.PLAYER ) {
				PlayerController.Instance.Death();
			}
		}
	}
}
