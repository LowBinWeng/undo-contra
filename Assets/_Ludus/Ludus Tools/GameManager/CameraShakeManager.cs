using UnityEngine;
using System.Collections;

public enum CameraShakeStrength  {none, small, medium, big}

public class CameraShakeManager : MonoBehaviour {

	#region Singleton
	private static CameraShakeManager _instance;
	public static CameraShakeManager Instance {
		get{
			if ( _instance == null ) _instance = GameObject.FindObjectOfType<CameraShakeManager>() as CameraShakeManager;
			return _instance;
		}
	}
	#endregion
	
	#region Debug
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Keypad1)) {
			SmallCameraShake ();
			Debug.Log("Shake");
		}
		else if ( Input.GetKeyDown( KeyCode.Keypad2 ) ) MediumCameraShake();
		else if ( Input.GetKeyDown( KeyCode.Keypad3 ) ) BigCameraShake();
		else if ( Input.GetKeyDown( KeyCode.Keypad4 ) ) MassiveCameraShake();
	}
	#endregion

	[Header("Small Camera Shake")]
	public int 		numberOfShakes_Small;
	public Vector3 	shakeAmount_Small;
	public Vector3 	rotationAmount_Small;
	public float 	distance_Small;
	public float 	speed_Small;
	public float 	decay_Small;
	public float 	uiShakeModifier_Small;
	public bool 	multiplyByTimeScale_Small;

	[Header("Medium Camera Shake")]
	public int 		numberOfShakes_Medium;
	public Vector3 	shakeAmount_Medium;
	public Vector3 	rotationAmount_Medium;
	public float 	distance_Medium;
	public float 	speed_Medium;
	public float 	decay_Medium;
	public float 	uiShakeModifier_Medium;
	public bool 	multiplyByTimeScale_Medium;

	[Header("Big Camera Shake")]
	public int 		numberOfShakes_Big;
	public Vector3 	shakeAmount_Big;
	public Vector3 	rotationAmount_Big;
	public float 	distance_Big;
	public float 	speed_Big;
	public float 	decay_Big;
	public float 	uiShakeModifier_Big;
	public bool 	multiplyByTimeScale_Big;

	[Header("Massive Camera Shake")]
	public int 		numberOfShakes_Massive;
	public Vector3 	shakeAmount_Massive;
	public Vector3 	rotationAmount_Massive;
	public float 	distance_Massive;
	public float 	speed_Massive;
	public float 	decay_Massive;
	public float 	uiShakeModifier_Massive;
	public bool 	multiplyByTimeScale_Massive;

	#region Camera Shakes
    public void SmallCameraShake() {
		Thinksquirrel.Utilities.CameraShake.ShakeAll( Thinksquirrel.Utilities.CameraShake.ShakeType.CameraMatrix, 	// Shake Type
		                                             numberOfShakes_Small,											// # of Shakes
		                                             shakeAmount_Small, 											// ShakeAmount
		                                             rotationAmount_Small, 											// Rotation Amount
		                                             distance_Small, 												// Distance
		                                             speed_Small,													// Speed
		                                             decay_Small,													// Decay
		                                             uiShakeModifier_Small, 										// UI Shake Modifier			
		                                             multiplyByTimeScale_Small );									// Multiply by TimeScale
	}

	public void MediumCameraShake() {
		Thinksquirrel.Utilities.CameraShake.ShakeAll( Thinksquirrel.Utilities.CameraShake.ShakeType.CameraMatrix, 	// Shake Type
		                                             numberOfShakes_Medium,											// # of Shakes
		                                             shakeAmount_Medium, 												// ShakeAmount
		                                             rotationAmount_Medium, 											// Rotation Amount
		                                             distance_Medium, 													// Distance
		                                             speed_Medium,														// Speed
		                                             decay_Medium,														// Decay
		                                             uiShakeModifier_Medium, 											// UI Shake Modifier			
		                                             multiplyByTimeScale_Medium );										// Multiply by TimeScale
	}

	public void BigCameraShake() {
		Thinksquirrel.Utilities.CameraShake.ShakeAll( Thinksquirrel.Utilities.CameraShake.ShakeType.CameraMatrix, 	// Shake Type
		                                             numberOfShakes_Big,											// # of Shakes
		                                             shakeAmount_Big, 												// ShakeAmount
		                                             rotationAmount_Big, 											// Rotation Amount
		                                             distance_Big, 													// Distance
		                                             speed_Big,														// Speed
		                                             decay_Big,														// Decay
		                                             uiShakeModifier_Big, 											// UI Shake Modifier			
		                                             multiplyByTimeScale_Big );										// Multiply by TimeScale
	}

	public void MassiveCameraShake() {
		Thinksquirrel.Utilities.CameraShake.ShakeAll( Thinksquirrel.Utilities.CameraShake.ShakeType.CameraMatrix, 	// Shake Type
		                                             numberOfShakes_Massive,										// # of Shakes
		                                             shakeAmount_Massive, 										// ShakeAmount
		                                             rotationAmount_Massive, 									// Rotation Amount
		                                             distance_Massive, 											// Distance
		                                             speed_Massive,												// Speed
		                                             decay_Massive,												// Decay
		                                             uiShakeModifier_Massive, 									// UI Shake Modifier			
		                                             multiplyByTimeScale_Massive );								// Multiply by TimeScale
	}

	#endregion

}
