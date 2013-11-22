using UnityEngine;
using System.Collections;

public class Player : Character {

	private static Player instance = null;
	public static Player Instance { get { return instance; } }
	
	void Awake() {
		instance = this;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.X)){
			animator.SetTrigger("shouldAttack");
		}

		if(Input.GetKeyDown(KeyCode.C)) {
			animator.SetBool("shouldBlock", true);
			IsBlocking = true;
		}
		else if(Input.GetKeyUp(KeyCode.C)) {
			animator.SetBool("shouldBlock", false);
			IsBlocking = false;
		}

		var velocity = Vector2.zero;

		if(Input.GetKey(KeyCode.RightArrow)) {
			velocity.x = 1f;
		}

		if(Input.GetKey(KeyCode.LeftArrow)) {
			velocity.x = -1f;
		}

		if(Input.GetKey(KeyCode.UpArrow)) {
			velocity.y = 1f;
		}

		if(Input.GetKey(KeyCode.DownArrow)) {
			velocity.y = -1f;
		}

		if(IsBlocking) {
			velocity = Vector2.zero;
		}

		mover.Move(velocity.normalized);

		if(Input.GetKeyDown(KeyCode.Z)) {
			mover.Jump();
		}
	}

	void OnGUI() {
		GUI.Label(new Rect(10f, 10f, 200f, 100f), "Move: Arrow Keys\nPunch: X\nBlock: C\nJump: Z\n");
	}
}
