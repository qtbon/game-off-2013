using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterAttacker))]
public class BigBossCard : Enemy {

	private float moverSpeed;

	private Transform player;
	private Transform target;

	public GameObject debrisPrefab;

	private int attackCount = 0;
	public int debrisInterval = 3;

	private CharacterAttacker jumpAttacker;

	protected void Awake() {
		jumpAttacker = GetComponent<CharacterAttacker>();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();

		player = Player.Instance.transform;

		moverSpeed = mover.speed;
		mover.speed = 0f;
		mover.GoalBuffer = 0.0001f;

		var targetObj = new GameObject("Boss Target");
		target = targetObj.transform;

		StartCoroutine(WaitForAggro());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		mover.Move();
	}

	IEnumerator WaitForAggro() {
		while(!HasAggro) {
			yield return null;
		}

		StartCoroutine(JumpAttack());
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
			jumpAttacker.Attack();
			yield return null;
		}

		// Randomly spawn debris
		if(++attackCount % debrisInterval == 0) {
			// Send debris somewhere between boss and player
			var debrisPosition = Vector3.Lerp(transform.position, player.position, Random.Range(0.25f, 0.75f));
			var debrisObj = Instantiate(debrisPrefab, debrisPosition + Vector3.up * 2f, Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f))) ) as GameObject;
			var debris = debrisObj.GetComponent<BossDebris>();

			debris.GoalPosition = debrisPosition;
		}

		mover.target = null;

		StartCoroutine(Idle());
	}

	IEnumerator Idle() {
		yield return new WaitForSeconds(3f);
		StartCoroutine(JumpAttack());
	}
}
