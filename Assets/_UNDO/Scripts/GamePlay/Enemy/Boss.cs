using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : Enemy {

	[Header("Data")]
	public PlayerController target;
	public enum BossBehaviour {Idle, Shuffle}
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
				}

			break;

			// FAR
			case TargetDistance.Far :
			
				switch ( bossBehaviour ) {
				case BossBehaviour.Idle: StartIdle(); break;
				case BossBehaviour.Shuffle: StartShuffle(); break;
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
		yield return new WaitForSeconds( 3f );
		CheckTarget();
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

		CheckTarget();

	}

	void EndAIRoutine() {
		CheckTarget();
	}

	public override void TakeHit( int damage, Vector3 point ) {
		FlashCharacter();
		base.TakeHit( damage,point);

	}


}
