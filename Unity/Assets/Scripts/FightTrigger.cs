using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightTrigger : MonoBehaviour {

	public bool HasTriggered { get; private set; }
	public bool FightOver { get; private set; }
	public List<EnemyCard> enemies = new List<EnemyCard>();

	private uint numDeadEnemies = 0;

	void Start() {
		HasTriggered = false;
		FightOver = false;
	}

	void Update() {
		if(!FightOver && numDeadEnemies == enemies.Count) {
			Camera.main.GetComponent<CameraController>().Unlock();
			Events.CharacterDied -= HandleCharacterDied;
			FightOver = true;
		}
	}

	void OnDisable() {
		if(HasTriggered && !FightOver) {
			Events.CharacterDied -= HandleCharacterDied;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(HasTriggered) {
			return;
		}
		
		if(other.gameObject.layer != LayerMask.NameToLayer("Player"))
			return;
		
		// Aggrevate enemies
		enemies.ForEach(e => e.HasAggro = true);

		// Lock screen
		Camera.main.GetComponent<CameraController>().Lock();
		
		HasTriggered = true;

		Events.CharacterDied += HandleCharacterDied;
	}

	void HandleCharacterDied (object sender, System.EventArgs e) {
		var characterHealth = sender as CharacterHealth;
		var character = characterHealth.GetComponent<EnemyCard>();
		if(!enemies.Contains(character)) {
			return;
		}

		++numDeadEnemies;
	}
}
