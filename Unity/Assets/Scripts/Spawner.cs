using System;
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject objToSpawn;
	public Vector2 position;

	void OnEnable() {
		Events.CharacterDied += HandleCharacterDied;
	}

	void OnDisable() {
		Events.CharacterDied -= HandleCharacterDied;
	}
	
	void HandleCharacterDied (object sender, EventArgs e) {
		if( (sender as MonoBehaviour).gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			Spawn();
		}
	}

	void Spawn() {
		GameObject.Instantiate(objToSpawn, position, objToSpawn.transform.rotation);
	}

	void OnDrawGizmos(){
		Gizmos.DrawSphere(position, 0.1f);
	}
}

