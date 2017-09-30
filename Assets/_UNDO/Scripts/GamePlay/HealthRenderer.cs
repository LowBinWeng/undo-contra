using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthRenderer : MonoBehaviour {

	public Image hpFillBar;

	public void UpdateHPRenderer(float hpPercentage) {
		hpFillBar.fillAmount = hpPercentage;
	}
}
