using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	[SerializeField]protected UndoGirl undoGirl;

	[Header("Character")]
	public Vector2 minBounds;
	public Vector2 maxBounds;
	public float moveSpeed = 1f;
	public float walkSpeed = 1f;
	public float jumpForce = 10f;
	public float jumpLateralSpeedMultiply = 3f;
	public float dashForce = 10f;
	float netDashforce = 0f;
	public float dashDuration = 0.4f;
	float dashTime = 0f;
	public float dashCooldownDuration = 1f;
	float dashCooldownTime = 0f;
	public Image staminaFill;
	public GameObject staminaFlash; 

	public float gravity = 10f;

	float netSpeed = 0f;
	public Animator anim;
	public Transform spawnPoint;
	public GameObject bulletPrefab;
	public LayerMask targetMask;
	public float cooldownDuration = 0.1f;
	public float windupDuration = 0.1f;
	float windupTime = 0f;
	float cooldownTime = 0f;
	public Transform girl;
	public Transform root;
	public Transform center;
	public Transform leftRotReference;
	public Transform rightRotReference;

	bool isAttacking = false;
	bool isJumping = false;
	bool isDashing = false;

	[Header("CrossHair")]
	public Transform aiming3D;
	public RectTransform crossHairUI;
	public Animator crossHairAnim;

	public float aimSpeed = 1f;
	Vector3 aimVelocity = Vector3.zero;
	Vector3 lastMousePos = Vector3.zero;
	Vector3 aimDelta = Vector3.zero;
	[SerializeField]float minX = -20f;
	[SerializeField]float maxX = 20f;
	[SerializeField]float minY = -20f;
	[SerializeField]float maxY = 20f;
	public float shotSpread = 1f;
	bool attackLock = false;

	Vector3 velocity = Vector3.zero;

	private static PlayerController _instance;
	public static PlayerController Instance {
		get {
			return _instance;
		}
	}

	[Header("Aiming")]
	public Transform origin;

	void Awake() {

		if ( _instance == null ) _instance = this;
		else if ( _instance != this ) Destroy( this );

		SetCursor(false);
	}

	void OnEnable () {
		lastMousePos = new Vector2 ( Screen.width/2f, Screen.height/2f );
	}

	void SetCursor ( bool active ) {
		if ( active == false ) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	// Update is called once per frame
	void Update () {
		if ( undoGirl.isDead == false ) {
			ControlMovement ();
			ControlCrossHair ();
			ControlAttack ();
			CountCooldown ();
		}
		else {
			if ( isJumping ) {
				if ( this.transform.position.y < 0f ) {
					Vector3 resetPos = this.transform.position;
					resetPos.y = 0f;
					this.transform.position = resetPos;

					velocity.y = 0f;
					isJumping = false;
				}
				else {
					velocity.x = 0f;
					velocity.y = -gravity;
					transform.Translate (velocity * netSpeed * Time.deltaTime);
				}
			}
		}


	}

	void ControlMovement() {

		if (!isDashing) {
			velocity.x = Input.GetAxis ("Horizontal");
		}

		if (Input.GetButtonDown ("Dash") && (velocity.x != 0f) && !isDashing && dashCooldownTime == 0f) {
			isDashing = true;
			netDashforce = dashForce;
			dashTime = 0f;
			attackLock = true;

			if (velocity.x < 0f) {
				anim.Play ("DodgeLeft");
				velocity.x -= dashForce;
			} else if (velocity.x > 0f) {
				anim.Play ("DodgeRight");
				velocity.x += dashForce;
			}

			AudioManager.Instance.Play ("event:/Dash", this.transform.position);
		}

		if (isDashing) {
			if (dashTime < dashDuration)
				dashTime += Time.deltaTime;
			else { 
				// End Dash
				dashTime = dashDuration;
				dashCooldownTime = dashCooldownDuration;
				isDashing = false;
				attackLock = false;
			}

			netDashforce = Mathf.Lerp (netDashforce, 0f, dashTime / dashDuration);
		}

		if (dashCooldownTime > 0f) {
			dashCooldownTime -= Time.deltaTime;
			staminaFill.fillAmount = (dashCooldownDuration - dashCooldownTime) / dashCooldownDuration;
			staminaFlash.SetActive (false);
		}
		else {
			dashCooldownTime = 0f;
			staminaFill.fillAmount = 1f;
			staminaFlash.SetActive (true);
		}


		if ( (Input.GetButtonDown( "Jump" ) || Input.GetKeyDown(KeyCode.Space) ) && isJumping == false ) {
			anim.Play("Jump");
			isJumping = true;
			velocity.y = jumpForce;

		}

		if ( isJumping ) {
			if 	( !isDashing ) velocity.x *= jumpLateralSpeedMultiply;
			else velocity.x *= 1.0f;

			if 		( !isAttacking ) velocity.y = Mathf.MoveTowards( velocity.y, -gravity, Time.deltaTime * 10f );
			else if ( isAttacking) velocity.y = Mathf.MoveTowards( velocity.y, -gravity, Time.deltaTime * 2.5f );

			if ( this.transform.position.y < 0f ) {
				Vector3 resetPos = this.transform.position;
				resetPos.y = 0f;
				this.transform.position = resetPos;

				velocity.y = 0f;
				isJumping = false;
			}
		}

		anim.SetBool( "IsJumping", isJumping );
		anim.SetBool( "IsDashing", isDashing );
		anim.SetFloat( "Direction", velocity.x );
		anim.SetFloat( "Speed", Mathf.Abs(velocity.x) );


		if ( this.transform.position.x <= minBounds.x && velocity.x < 0f) velocity.x = 0f; 
		if ( this.transform.position.x >= maxBounds.x && velocity.x > 0f ) velocity.x = 0f; 
//		if ( this.transform.position.y <= minBounds.y && velocity.y < 0f ) velocity.y = 0f; 
//		if ( this.transform.position.y >= maxBounds.y && velocity.y > 0f ) velocity.y = 0f; 

		transform.Translate (velocity * netSpeed * Time.deltaTime);

		if ( this.transform.position.x < minBounds.x ) this.transform.position = new Vector3( minBounds.x, transform.position.y, transform.position.z );
		if ( this.transform.position.x > maxBounds.x ) this.transform.position = new Vector3( maxBounds.x, transform.position.y, transform.position.z );
//		if ( this.transform.position.y < minBounds.y ) this.transform.position = new Vector3( transform.position.x, minBounds.y, transform.position.z );
//		if ( this.transform.position.y > maxBounds.y ) this.transform.position = new Vector3( transform.position.x, maxBounds.y, transform.position.z );

		// Rotation Control
		if ( Mathf.Abs(velocity.x) > 0f ) {
			anim.SetLayerWeight(1,0f); // Cancel Aiming
			// Run
			if ( !isAttacking && !isDashing ) {
				
				netSpeed = moveSpeed;

				if ( velocity.x > 0f ) {
					girl.rotation = rightRotReference.rotation;
				}
				else if ( velocity.x < 0f ) {
					girl.rotation = leftRotReference.rotation;
				}
			}
			// Walk / Attacking
			else if ( !isDashing ) {
				ControlAim();
			}
			// Dashing defaults
			else {
				anim.SetLayerWeight(1,0f);
				girl.rotation = Quaternion.identity;
			}
		}
		else if (isAttacking) {
			ControlAim();
		}
		// Stationary
		else {
			netSpeed = moveSpeed;
			anim.SetLayerWeight(1,0f);
			girl.rotation = Quaternion.identity;
		}
	}


	void ControlCrossHair() {
		aimDelta = Input.mousePosition - lastMousePos;
		aimDelta.Normalize ();

		aimVelocity.x = aimDelta.x;
		aimVelocity.y = aimDelta.y;

		aimVelocity.x = Input.GetAxis ("Mouse X");
		aimVelocity.y = Input.GetAxis ("Mouse Y");

		if ( aiming3D.position.y <= minY && aimVelocity.y < 0f ) aimVelocity.y = 0f; // Min Y
		if ( aiming3D.position.y >= maxY && aimVelocity.y > 0f ) aimVelocity.y = 0f; // Max Y
		if ( aiming3D.position.x <= minX && aimVelocity.x < 0f ) aimVelocity.x = 0f; // Min X
		if ( aiming3D.position.x >= maxX && aimVelocity.x > 0f ) aimVelocity.x = 0f; // Max X

		if ( aiming3D.position.x < minX ) aiming3D.position = new Vector3 ( minX, aiming3D.position.y, aiming3D.position.z );
		if ( aiming3D.position.x > maxX ) aiming3D.position = new Vector3 ( maxX, aiming3D.position.y, aiming3D.position.z );
		if ( aiming3D.position.y < minY ) aiming3D.position = new Vector3 ( aiming3D.position.x, minY , aiming3D.position.z );
		if ( aiming3D.position.y > maxY ) aiming3D.position = new Vector3 ( aiming3D.position.x, maxY , aiming3D.position.z );

		aiming3D.Translate (aimVelocity * Time.deltaTime * aimSpeed);
		lastMousePos = Input.mousePosition;

		if (isAttacking) crossHairAnim.Play ("Fire");
		else crossHairAnim.Play ("Idle");

		if ( undoGirl.isDead == false ) crossHairUI.position = Vector3.MoveTowards( crossHairUI.position, Camera.main.WorldToScreenPoint( aiming3D.position), Time.deltaTime * 120f );

	}



	void ControlAim() {

		anim.SetLayerWeight(1,1f);
		netSpeed = walkSpeed;

		Vector3 offset = aiming3D.position - origin.position;
		offset.Normalize();

		anim.SetFloat("VerticalAim",offset.y);

		float angle = Mathf.Atan2( offset.x, offset.z ) * Mathf.Rad2Deg;

		girl.rotation = Quaternion.Lerp( girl.rotation, Quaternion.Euler(0f, angle , 0f ), Time.deltaTime * 10f );
	}

	void ControlAttack() {

		if (Input.GetMouseButtonUp(0)) {
			attackLock = false;
		}

		if (Input.GetMouseButtonDown (0) && attackLock == false ) {
			windupTime = windupDuration;
		}

		if (Input.GetMouseButton (0) && attackLock == false ) {

			isAttacking = true;

			if ( windupTime > 0f ) {
				windupTime -= Time.deltaTime;
			}
			else if ( cooldownTime <= 0f) {
				Transform t = PoolManager.Pools ["Attacks"].Spawn (bulletPrefab, spawnPoint.position, spawnPoint.rotation);
				t.LookAt (aiming3D);
				Vector3 shotDirection = Vector3.forward;
				shotDirection.x = Random.Range(-shotSpread,shotSpread);
				shotDirection.y = Random.Range(-shotSpread,shotSpread);
				t.Translate( shotDirection  * 20f );
				LineRenderer lr = t.GetComponent<LineRenderer>();
				lr.SetPosition(0, spawnPoint.position );
				lr.SetPosition(1, t.position );

				cooldownTime = cooldownDuration;

				// Hit detection
				RaycastHit hit;
				if ( Physics.Raycast( spawnPoint.position, (t.position - spawnPoint.position)*200f, out hit ) ) {
					if (hit.collider.CompareTag ("Enemy")) {
						hit.collider.GetComponent<Character> ().TakeHit (1, hit.point);
						AudioManager.Instance.Play ("event:/Hit", this.center.position);
						PoolManager.Pools["Attacks"].Spawn("HitEffect", hit.point, Quaternion.identity);
					}
				}

				// Effect
				Transform e = PoolManager.Pools["Attacks"].Spawn("LaserMuzzle", spawnPoint.position, spawnPoint.rotation );

				AudioManager.Instance.Play( "event:/LaserShot", this.center.position );
			}

		}
		else {
			isAttacking = false;
		}

		anim.SetBool( "IsAttacking", isAttacking );
	}

	void CountCooldown() {
		if (cooldownTime > 0f) cooldownTime -= Time.deltaTime;
	}

}
