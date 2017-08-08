using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tweener : MonoBehaviour {

	public bool playOnEnable = false;
	public float duration = 1f;

	public bool tweenPosition = false;
	public bool separateAxisPos = false;
	public AnimationCurve positionCurveAll = AnimationCurve.EaseInOut(0f,0f,1f,1f);
	public AnimationCurve positionCurveX = AnimationCurve.EaseInOut(0f,0f,1f,1f);
	public AnimationCurve positionCurveY = AnimationCurve.EaseInOut(0f,0f,1f,1f);
	public AnimationCurve positionCurveZ = AnimationCurve.EaseInOut(0f,0f,1f,1f);
	public Vector3 startPosition;
	public Vector3 endPosition;

	public bool tweenScale = false;
	public bool separateAxisScale = false;
	public AnimationCurve scaleCurveAll = AnimationCurve.EaseInOut(0f,0f,1f,1f);
	public AnimationCurve scaleCurveX = AnimationCurve.EaseInOut(0f,0f,1f,1f);
	public AnimationCurve scaleCurveY = AnimationCurve.EaseInOut(0f,0f,1f,1f);
	public AnimationCurve scaleCurveZ = AnimationCurve.EaseInOut(0f,0f,1f,1f);
	public Vector3 startScale;
	public Vector3 endScale;


	bool reverse = false;
	float totalTime = 0f;
	float lerpTime = 0f;


	/*==========================================================================================
	* Initialization
	*========================================================================================*/

	void OnEnable() {
		if ( playOnEnable && Application.isPlaying ) {
			DoTween();
		}
	}

	[ContextMenu("Do Tween")]
	public void DoTween() {
		reverse = false;
		StopAllCoroutines();
		StartCoroutine("TweenRoutine");
	}

	[ContextMenu("Do Reverse Tween")]
	public void DoReverseTween() {
		reverse = true;
		StopAllCoroutines();
		StartCoroutine("TweenRoutine");
	}

	/*==========================================================================================
	* Tweening
	*========================================================================================*/


	IEnumerator TweenRoutine() {

		lerpTime = 0f;
		totalTime = 0f;

		while ( true ) {

			CalculateTime();
			TweenPosition();
			TweenScale();

			if ( lerpTime == 1f ) {
				StopAllCoroutines();
			}

			yield return null;
		}
	}


	void CalculateTime() {
		totalTime += RealTime.deltaTime;
		lerpTime = Mathf.Clamp01(totalTime/duration);
	}

	void TweenPosition() {
		if (tweenPosition ) {
			if ( separateAxisPos ) {
				Vector3 newPos = Vector3.zero;
				Vector3 origin = startPosition;
				Vector3 target = endPosition;

				if ( reverse ) {
					origin = endPosition;
					target = startPosition;
				}
					
				float maxPosMultiplierX = Mathf.Clamp(positionCurveX.Evaluate(lerpTime),-100f,100f);
				float maxPosMultiplierY = Mathf.Clamp(positionCurveY.Evaluate(lerpTime),-100f,100f);
				float maxPosMultiplierZ = Mathf.Clamp(positionCurveZ.Evaluate(lerpTime),-100f,100f);

				newPos.x = Mathf.Lerp( origin.x, target.x * maxPosMultiplierX, positionCurveX.Evaluate(lerpTime));
				newPos.y = Mathf.Lerp( origin.y, target.y * maxPosMultiplierY, positionCurveY.Evaluate(lerpTime));
				newPos.z = Mathf.Lerp( origin.z, target.z * maxPosMultiplierZ, positionCurveZ.Evaluate(lerpTime));

				this.transform.position = newPos;
			}
			else {
				float maxPosMultiplier = Mathf.Clamp(positionCurveAll.Evaluate(lerpTime),-100f,100f);
				if ( !reverse ) this.transform.position = Vector3.Lerp( startPosition,endPosition * maxPosMultiplier, positionCurveAll.Evaluate(lerpTime) ); 
				else 			this.transform.position = Vector3.Lerp( endPosition,startPosition * maxPosMultiplier, positionCurveAll.Evaluate(lerpTime) ); 
			}
		}
	}

	void TweenScale() {
		if (tweenScale ) {
			if ( separateAxisScale ) {
				Vector3 newScale = Vector3.zero;
				Vector3 origin = startScale;
				Vector3 target = endScale;

				if ( reverse ) {
					origin = endScale;
					target = startScale;
				}

				float maxSizeMultiplierX = Mathf.Clamp(scaleCurveX.Evaluate(lerpTime),-100f,100f);
				float maxSizeMultiplierY = Mathf.Clamp(scaleCurveY.Evaluate(lerpTime),-100f,100f);
				float maxSizeMultiplierZ = Mathf.Clamp(scaleCurveZ.Evaluate(lerpTime),-100f,100f);

				newScale.x = Mathf.Lerp( origin.x, target.x * maxSizeMultiplierX, scaleCurveX.Evaluate(lerpTime));
				newScale.y = Mathf.Lerp( origin.y, target.y * maxSizeMultiplierY, scaleCurveY.Evaluate(lerpTime));
				newScale.z = Mathf.Lerp( origin.z, target.z * maxSizeMultiplierZ, scaleCurveZ.Evaluate(lerpTime));

				this.transform.localScale = newScale;
			}
			else {
				float maxSizeMultiplier = Mathf.Clamp(scaleCurveAll.Evaluate(lerpTime),-100f,100f);
				if ( !reverse ) this.transform.localScale = Vector3.Lerp( startScale,endScale * maxSizeMultiplier, scaleCurveAll.Evaluate(lerpTime) ); 
				else 			this.transform.localScale = Vector3.Lerp( endScale,startScale * maxSizeMultiplier, scaleCurveAll.Evaluate(lerpTime) ); 
			}
		}
	}

}
