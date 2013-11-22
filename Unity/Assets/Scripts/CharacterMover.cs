using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Rigidbody2D))]
public class CharacterMover : MonoBehaviour
{
	public float speed = 2f;
	public float jumpForce = 75f;
	public Transform target = null;

	private float xScale;
	private Animator animator;

	// Used for jumping, knockdown, etc.
	private Vector2 velocityOffset;
	
	private bool shouldJump = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		xScale = transform.localScale.x;
	}
	
	public void Move() {
		if(target) {
			var baseVelocity = target.position - transform.position;
			
			if(baseVelocity.sqrMagnitude > 0.12f) {
				Move(baseVelocity.normalized);
			}
			else {
				Move(Vector2.zero);
			}
		}
	}

	public void Move(Vector2 baseVelocity) {
		var velocity = baseVelocity * speed;
		velocity += velocityOffset;
		
		rigidbody2D.velocity = velocity;

		if(velocity.x != 0f) {
			var scale = transform.localScale;
			scale.x = velocity.x < 0 ? -xScale : xScale;
			transform.localScale = scale;
		}

		/// @todo Maybe only set every so often in coroutine
		animator.SetFloat("speed", Mathf.Abs(rigidbody2D.velocity.magnitude));
	}

	public void Jump() {
		if(!shouldJump) {
			StartCoroutine(Jumping());
		}
	}

	IEnumerator Jumping() {
		shouldJump = true;
		animator.SetBool("isJumping", true);
		var t = 0f;
		velocityOffset = Vector2.zero;
		while((t += Time.deltaTime * 2f) < 1f) {
			velocityOffset.y = Mathf.Sign(Mathf.Lerp(1, -1, t)) * Time.deltaTime * jumpForce;
			yield return null;
		}
		shouldJump = false;
		velocityOffset = Vector2.zero;
		animator.SetBool("isJumping", false);
	}
}

