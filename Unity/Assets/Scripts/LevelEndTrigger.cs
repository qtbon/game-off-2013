using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider2D))]
public class LevelEndTrigger : MonoBehaviour {

	public int levelToLoad;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer != LayerMask.NameToLayer("Player")) {
			return;
		}

		Application.LoadLevel(levelToLoad);
	}
}
