using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
public class PlayerController : MonoBehaviour {

	[Header("Character")]
	public Vector2 minBounds;
	public Vector2 maxBounds;
	public float moveSpeed = 1f;
	public float walkSpeed = 1f;
	public float jumpForce = 10f;
	public float dashForce = 10f;
	float netDashforce = 0f;
	public float dashDuration = 0.4f;
	float dashTime = 0f;
	public float dashCooldownDuration = 1f;
	float dashCooldownTime = 0f;
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
	public Transform crossHair;

	public float aimSpeed = 1f;
	Vector3 aimVelocity = Vector3.zero;
	Vector3 lastMousePos = Vector3.zero;
	Vector3 aimDelta = Vector3.zero;
	[SerializeField]float minX = -20f;
	[SerializeField]float maxX = 20f;
	[SerializeField]float minY = -20f;
	[SerializeField]float maxY = 20f;
	public float shotSpread = 1f;

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

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void OnEnable () {
		lastMousePos = new Vector2 ( Screen.width/2f, Screen.height/2f );
	}

	// Update is called once per frame
	void Update () {
		ControlMovement ();
		ControlCrossHair ();
		ControlAttack ();
		CountCooldown ();
	}

	void ControlMovement() {

		if ( !isDashing ) {
			velocity.x = Input.GetAxis ("Horizontal");
		}

		if ( Input.GetButtonDown("Dash") && (velocity.x != 0f) && !isDashing && dashCooldownTime == 0f) {
			isDashing = true;
			netDashforce = dashForce;
			dashTime = 0f;

			if 		( velocity.x < 0f ) {
				anim.Play("DodgeLeft");
				velocity.x -= dashForce;
			}
			else if ( velocity.x > 0f ) {
				anim.Play("DodgeRight");
				velocity.x += dashForce;
			}

		}

		if ( isDashing ) {
			if ( dashTime < dashDuration ) dashTime += Time.deltaTime;
			else { 
				// End Dash
				dashTime = dashDuration;
				dashCooldownTime = dashCooldownDuration;
				isDashing = false;
			}

			netDashforce = Mathf.Lerp( netDashforce, 0f, dashTime / dashDuration );
		}

		if ( dashCooldownTime > 0f ) dashCooldownTime -= Time.deltaTime;
		else dashCooldownTime = 0f;


		if ( Input.GetButtonDown( "Jump" ) && isJumping == false ) {
			anim.Play("Jump");
			isJumping = true;
			velocity.y = jumpForce;
		}

		if ( isJumping ) {
			if 	( !isDashing ) velocity.x *= 1.0f;
			else velocity.x *= 1.15f;

			if 		( !isAttacking ) velocity.y = Mathf.MoveTowards( velocity.y, -gravity, Time.deltaTime * 10f );
			else if ( isAttacking ) velocity.y = Mathf.MoveTowards( velocity.y, -gravity, Time.deltaTime * 2.5f );

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

		if ( crossHair.position.y <= minY && aimVelocity.y < 0f ) aimVelocity.y = 0f; // Min Y
		if ( crossHair.position.y >= maxY && aimVelocity.y > 0f ) aimVelocity.y = 0f; // Max Y
		if ( crossHair.position.x <= minX && aimVelocity.x < 0f ) aimVelocity.x = 0f; // Min X
		if ( crossHair.position.x >= maxX && aimVelocity.x > 0f ) aimVelocity.x = 0f; // Max X

		if ( crossHair.position.x < minX ) crossHair.position = new Vector3 ( minX, crossHair.position.y, crossHair.position.z );
		if ( crossHair.position.x > maxX ) crossHair.position = new Vector3 ( maxX, crossHair.position.y, crossHair.position.z );
		if ( crossHair.position.y < minY ) crossHair.position = new Vector3 ( crossHair.position.x, minY , crossHair.position.z );
		if ( crossHair.position.y > maxY ) crossHair.position = new Vector3 ( crossHair.position.x, maxY , crossHair.position.z );

		crossHair.Translate (aimVelocity * Time.deltaTime * aimSpeed);
		lastMousePos = Input.mousePosition;
	}



	void ControlAim() {

		anim.SetLayerWeight(1,1f);
		netSpeed = walkSpeed;

		Vector3 offset = crossHair.position - origin.position;
		offset.Normalize();

		anim.SetFloat("VerticalAim",offset.y);

		float angle = Mathf.Atan2( offset.x, offset.z ) * Mathf.Rad2Deg;

		girl.rotation = Quaternion.Lerp( girl.rotation, Quaternion.Euler(0f, angle , 0f ), Time.deltaTime * 10f );
	}

	void ControlAttack() {

		if (Input.GetMouseButtonDown (0) ) {
			windupTime = windupDuration;
		}

		if (Input.GetMouseButton (0) ) {

			isAttacking = true;

			if ( windupTime > 0f ) {
				windupTime -= Time.deltaTime;
			}
			else if ( cooldownTime <= 0f) {
				Transform t = PoolManager.Pools ["Attacks"].Spawn (bulletPrefab, spawnPoint.position, spawnPoint.rotation);
				t.LookAt (crossHair);
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
					if ( hit.collider.CompareTag("Enemy")) hit.collider.GetComponent<Character>().TakeHit(1, hit.point );
				}
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
