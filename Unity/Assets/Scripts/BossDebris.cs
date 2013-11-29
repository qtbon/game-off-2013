using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(CircleCollider2D))]
[RequireComponent (typeof(Projectile))]
[RequireComponent (typeof(CharacterAttacker))]
public class BossDebris : Actor {

	public float fallSpeed = 1f;
	public Vector3 GoalPosition { get; set; }
	public float lifeSpan = 5f;

	private Projectile projectile;
	private CharacterAttacker attacker;

	// Use this for initialization
	void Start () {
		StartCoroutine(MoveToGoal());
		projectile = GetComponent<Projectile>();
		attacker = GetComponent<CharacterAttacker>();

		attacker.targetLayerMask = 1 << LayerMask.NameToLayer("Player");

		Events.InvokeActorCreated(this, null);
	}

	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator MoveToGoal() {
		var baseVelocity = GoalPosition - transform.position;
		baseVelocity.Normalize();
		rigidbody2D.velocity = baseVelocity.normalized * fallSpeed;
		var toGoal = baseVelocity;
		while(Vector3.Dot(baseVelocity, toGoal) > 0f) {
			yield return new WaitForFixedUpdate();
			toGoal = (GoalPosition - transform.position).normalized;
		}

		attacker.targetLayerMask = 0;

		rigidbody2D.velocity = Vector2.zero;
		StartCoroutine(WaitAndDestroy());
	}

	IEnumerator WaitAndDestroy() {
		/// @todo add some flashing or some sort of indicator of almost gone

		yield return new WaitForSeconds(lifeSpan);
		Destroy(this.gameObject);
	}
}
