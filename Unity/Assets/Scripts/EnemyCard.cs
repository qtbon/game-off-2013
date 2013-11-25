using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class EnemyCard : Character {

	public bool HasAggro { get; set; }

	void Awake() {
		HasAggro = false;
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();
		Events.InvokeCharacterCreated(this, null);
		mover.target = Player.Instance.transform;
		StartCoroutine(IdleBlockAndAttack());
		mover.GoalBuffer = 0.12f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!HasAggro) {
			return;
		}

		if(!IsBlocking) {
			mover.Move();
		}
		else {
			mover.Move(Vector2.zero);
		}
	}

	IEnumerator IdleBlockAndAttack() {
		while(true) {
			yield return new WaitForSeconds(Random.Range(2f, 4f));

			// Maybe I'll block
			while(true) {
				if(Random.value < 0.25f) {
					break;
				}
				animator.SetBool("shouldBlock", true);
				IsBlocking = true;
				yield return new WaitForSeconds(Random.Range(0.5f, 1f));
			}
			animator.SetBool("shouldBlock", false);
			IsBlocking = false;

			animator.SetTrigger("shouldAttack");

			// Wait a frame
			yield return null;

			// Wait for transition to end
			while(animator.IsInTransition(0)) {
				yield return null;
			}

			// Wait for end of attack
			int attackState = Animator.StringToHash("Base.attack");
			while(animator.GetCurrentAnimatorStateInfo(0).nameHash == attackState) {
				yield return null;
			}
		}
	}
}
