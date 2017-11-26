using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class Boss : Enemy {

	[Header("Data")]
	public PlayerController target;
	public enum BossBehaviour {Idle, Shuffle, Shoot, Rocket}
	public BossBehaviour bossBehaviour = BossBehaviour.Idle;
	BossBehaviour lastBossBehaviour = BossBehaviour.Idle;

	[Header("Awareness")]
	public float closeDistance = 10f;
	public float farDistance = 100f;
	enum TargetDistance {Close, Far}
	TargetDistance targetDistance = TargetDistance.Close;

	[Header("Shuffling")]
	public Transform[] shufflingPoints;
	int lastShufflePointIndex = 0;
	public int shuffleCount = 10;
	public float shuffleSpeed = 10f;
	public float shuffleDelay = 0.2f;

	[Header("Shooting")]
	public Transform enemyShot;
	public Transform[] attackSpawnPoints;
	public int shots = 10;
	public float shotInterval = 0.2f;
	public float shotSpread = 0.2f;

	[Header("Rocket Swarm")]
	public Transform[] rocketSpawnPoints;
	public Transform homingRocket;
	public float rocketShotInterval = 0.2f;
	public Transform rocketSwarmPosition;

	void OnEnable() {
		CheckTarget();	
	}

	/* ==============================================================================================================
	 * AI MAIN
	 * ============================================================================================================ */

	void CheckTarget() {
		if ( Vector3.Distance( this.transform.position, target.root.position) < closeDistance ) targetDistance = TargetDistance.Close;
		else targetDistance = TargetDistance.Far;

		// Set new boss behaviour
		do { bossBehaviour = (BossBehaviour) Random.Range(0,System.Enum.GetValues(typeof(BossBehaviour)).Length); }
		while ( lastBossBehaviour == bossBehaviour );

		lastBossBehaviour = bossBehaviour;

		switch( targetDistance ) {
			// CLOSE
			case TargetDistance.Close :
			
				switch ( bossBehaviour ) {
				case BossBehaviour.Idle: StartIdle(); break;
				case BossBehaviour.Shuffle: StartShuffle(); break;
				case BossBehaviour.Shoot: StartShoot(); break;
				case BossBehaviour.Rocket: StartRocketSwarm(); break;
				}

			break;

			// FAR
			case TargetDistance.Far :
			
				switch ( bossBehaviour ) {
				case BossBehaviour.Idle: StartIdle(); break;
				case BossBehaviour.Shuffle: StartShuffle(); break;
				case BossBehaviour.Shoot: StartShoot(); break;
				case BossBehaviour.Rocket: StartRocketSwarm(); break;
				}

			break;
		}

	}

	/* ==============================================================================================================
	 * Idling
	 * ============================================================================================================ */

	void StartIdle() {
		StartCoroutine(IdleRoutine());
	}

	IEnumerator IdleRoutine() {
		yield return new WaitForSeconds( 1f );
		EndAIRoutine();
	}


	/* ==============================================================================================================
	 * Shuffling
	 * ============================================================================================================ */

	void StartShuffle() {
		StartCoroutine(ShuffleRoutine());
	}

	IEnumerator ShuffleRoutine() {
		int _shuffleCount = shuffleCount;
		int _shufflePointIndex = 0;

		// Get new shuffle point
		do { _shufflePointIndex = Random.Range(0,shufflingPoints.Length); }
		while ( _shufflePointIndex == lastShufflePointIndex );

		while ( _shuffleCount > 0 ) {

			this.transform.position = Vector3.MoveTowards( this.transform.position, shufflingPoints[_shufflePointIndex].position, Time.deltaTime * shuffleSpeed );
			this.transform.LookAt( target.root );

			yield return null;

			if ( this.transform.position == shufflingPoints[_shufflePointIndex].position ) {
				// Get new shuffle point
				do { _shufflePointIndex = Random.Range(0,shufflingPoints.Length); }
				while ( _shufflePointIndex == lastShufflePointIndex );
				_shuffleCount--;

				yield return new WaitForSeconds( shuffleDelay );
			}
		}

		EndAIRoutine();

	}

	/* ==============================================================================================================
	 * Shoot
	 * ============================================================================================================ */

	void StartShoot() {
		StartCoroutine( ShootRoutine());
	}

	IEnumerator ShootRoutine() {

		this.transform.LookAt( target.center );

		for ( int i = 0; i < shots; i++ ) {
			Transform t1 = PoolManager.Pools["Attacks"].Spawn( enemyShot, attackSpawnPoints[0].position, attackSpawnPoints[0].rotation );
			Transform t2 = PoolManager.Pools["Attacks"].Spawn( enemyShot, attackSpawnPoints[1].position, attackSpawnPoints[1].rotation );

			t1.LookAt( target.center );
			t2.LookAt( target.center );

			t1.Rotate( new Vector3 (Random.Range(-shotSpread,shotSpread),Random.Range(-shotSpread,shotSpread),0f) );
			t2.Rotate( new Vector3 (Random.Range(-shotSpread,shotSpread),Random.Range(-shotSpread,shotSpread),0f) );

			yield return new WaitForSeconds( shotInterval );

			this.transform.LookAt( target.center );

		}

		EndAIRoutine();
	}

	/* ==============================================================================================================
	 * Rocket Swarm
	 * ============================================================================================================ */

	void StartRocketSwarm() {
		StartCoroutine( RocketSwarmRoutine());
	}

	IEnumerator RocketSwarmRoutine() {

		this.transform.LookAt( target.center );

		while ( this.transform.position != rocketSwarmPosition.position ) {

			this.transform.position = Vector3.MoveTowards( this.transform.position, rocketSwarmPosition.position, Time.deltaTime * shuffleSpeed );
			yield return null;
		}

		this.transform.LookAt( target.center );
		yield return new WaitForSeconds( 1.3f);

		for ( int i = 0; i < rocketSpawnPoints.Length; i++ ) {
			Transform t = PoolManager.Pools["Attacks"].Spawn( homingRocket, rocketSpawnPoints[i].position, rocketSpawnPoints[i].rotation );
			t.GetComponent<HomingRocket>().StartHoming( target.root );

			yield return new WaitForSeconds( rocketShotInterval );

			this.transform.LookAt( target.center );

		}

		yield return new WaitForSeconds( 2f);

		EndAIRoutine();
	}

	/* ==============================================================================================================
	 * End Routine
	 * ============================================================================================================ */

	void EndAIRoutine() {
		CheckTarget();
	}

	public override void TakeHit( int damage, Vector3 point ) {
		FlashCharacter();
		base.TakeHit( damage,point);

	}


}
