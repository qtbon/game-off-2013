using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class CharacterMover : MonoBehaviour
{
	public float speed = 2f;
	public float jumpHeight = 0.3f;
	// Length of time in air, in seconds
	public float jumpTime = 0.25f;
	public Transform target = null;
	public float GoalBuffer { get; set; }

	private float xScale;
	private Animator animator;
	
	private bool shouldJump = false;
	public bool IsJumping { get;private set; }

	private Vector3 jumpRoot;

	void Awake () {
		animator = GetComponent<Animator>();
		xScale = transform.localScale.x;

		SetJumpTime(jumpTime);
	}

	// Converts seconds to time.deltatime multiplier
	public void SetJumpTime(float jumpTime) {
		this.jumpTime = jumpTime;
	}
	
	public void Move() {
		if(target) {
			var baseVelocity = !IsJumping ? target.position - transform.position : target.position - jumpRoot;

			if(baseVelocity.sqrMagnitude > GoalBuffer) {
				Move(baseVelocity.normalized);
			}
			else {
				Move(Vector2.zero);
			}
		}
		else {
			Move(Vector2.zero);
		}
	}

	public void Move(Vector2 baseVelocity) {
		var velocity = baseVelocity * speed;
		
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
		IsJumping = shouldJump = true;
		animator.SetBool("isJumping", true);

		jumpRoot = transform.position;
		var jumpOffset = Vector3.zero;

		var t = 0f;
		// Calculate period based off of jump time

		while((t += Time.deltaTime) < jumpTime) {
			DoJump(ref jumpRoot, ref jumpOffset, t);
			yield return new WaitForFixedUpdate();
		}
		DoJump(ref jumpRoot, ref jumpOffset, jumpTime);

		IsJumping = shouldJump = false;
		animator.SetBool("isJumping", false);
	}

	void DoJump(ref Vector3 jumpRoot, ref Vector3 jumpOffset, float t) {
		// velocityOffset.y = Mathf.Sign(Mathf.Lerp(1, -1, t)) * Time.deltaTime * jumpForce;
		jumpRoot += new Vector3(rigidbody2D.velocity.x, rigidbody2D.velocity.y) * Time.fixedDeltaTime;
		Debug.DrawLine(jumpRoot - Vector3.right * 0.25f, jumpRoot + Vector3.right * 0.25f);
		jumpOffset.y = Mathf.Sin(t * (1/jumpTime) * Mathf.PI) * jumpHeight;
		transform.position = jumpRoot + jumpOffset;
	
	}
}

