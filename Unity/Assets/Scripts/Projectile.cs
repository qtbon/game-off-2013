using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(CharacterAttacker))]
public class Projectile : MonoBehaviour {

	protected CharacterAttacker attacker;

	public bool AttackActive { get; set; }

	void Awake () {
		attacker = GetComponent<CharacterAttacker>();
		AttackActive = true;
	}

	void OnTriggerEnter2D() {
		attacker.Attack();
	}
}
