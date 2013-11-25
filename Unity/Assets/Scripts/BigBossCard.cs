using UnityEngine;
using System.Collections;

public class BigBossCard : Character {

	private float moverSpeed;

	private Transform player;
	private Transform target;

	// Use this for initialization
	protected override void Start () {
		base.Start();

		player = Player.Instance.transform;

		moverSpeed = mover.speed;
		mover.speed = 0f;
		mover.GoalBuffer = 0.0001f;

		var targetObj = new GameObject("Boss Target");
		target = targetObj.transform;

		StartCoroutine(JumpAttack());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		mover.Move();
	}

	IEnumerator JumpAttack() {
		// Choose target
		target.transform.position = player.position;
		mover.target = target;

		// Calculate trajectory and timing
		mover.speed = moverSpeed;
		var toPlayer = target.transform.position - transform.position;
		var jumpTime = Mathf.Clamp(toPlayer.magnitude * (1f/mover.speed), 1.5f, 3f);
		mover.SetJumpTime(jumpTime);

		// Jump
		mover.Jump();

		while(mover.IsJumping) {
			yield return null;
		}

		mover.target = null;

		StartCoroutine(Idle());
	}

	IEnumerator Idle() {
		yield return new WaitForSeconds(3f);
		StartCoroutine(JumpAttack());
	}
}
