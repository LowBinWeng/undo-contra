using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
public class PlayerController : MonoBehaviour {

	[Header("Character")]
	public float moveSpeed = 1f;
	public float walkSpeed = 1f;
	public float jumpForce = 10f;
	public float gravity = 10f;

	float netSpeed = 0f;
	public Animator anim;
	public Transform spawnPoint;
	public GameObject bulletPrefab;
	public float cooldownDuration = 0.1f;
	float cooldownTime = 0f;
	public Transform girl;
	public Transform leftRotReference;
	public Transform rightRotReference;

	bool isAttacking = false;
	bool isJumping = false;

	[Header("CrossHair")]
	public Transform crossHair;
	public Vector2 minBounds;
	public Vector2 maxBounds;
	public float aimSpeed = 1f;
	Vector3 aimVelocity = Vector3.zero;
	Vector3 lastMousePos = Vector3.zero;
	Vector3 aimDelta = Vector3.zero;
	[SerializeField]float minX = -20f;
	[SerializeField]float maxX = 20f;
	[SerializeField]float minY = -20f;
	[SerializeField]float maxY = 20f;

	Vector3 velocity = Vector3.zero;

	[Header("Aiming")]
	public Transform origin;

	void Awake() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Update is called once per frame
	void Update () {
		ControlMovement ();
		ControlCrossHair ();
		ControlAttack ();
		CountCooldown ();
	}

	void ControlMovement() {

		velocity.x = Input.GetAxis ("Horizontal");

		if ( Input.GetButtonDown( "Jump" ) && isJumping == false ) {
			anim.Play("Jump");
			isJumping = true;
			velocity.y = jumpForce;
		}

		if ( isJumping ) {
			velocity.x *= 2.5f;

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
		anim.SetFloat( "Direction", velocity.x );
		anim.SetFloat( "Speed", Mathf.Abs(velocity.x) );


		transform.Translate (velocity * netSpeed * Time.deltaTime);


		// Rotation Control
		if ( Mathf.Abs(velocity.x) > 0f ) {
			anim.SetLayerWeight(1,0f); // Cancel Aiming
			// Run
			if ( !isAttacking ) {
				
				netSpeed = moveSpeed;

				if ( velocity.x > 0f ) {
					girl.rotation = rightRotReference.rotation;
				}
				else if ( velocity.x < 0f ) {
					girl.rotation = leftRotReference.rotation;
				}
			}
			// Walk / Attacking
			else{
				ControlAim();
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
		if (Input.GetMouseButton (0) ) {
			isAttacking = true;
			if ( cooldownTime <= 0f) {
				Transform t = PoolManager.Pools ["Attacks"].Spawn (bulletPrefab, spawnPoint.position, spawnPoint.rotation);
				t.LookAt (crossHair);
				cooldownTime = cooldownDuration;
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
