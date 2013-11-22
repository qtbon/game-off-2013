using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public bool GameOver { get; private set; }
	public bool TempWin { get; private set; }

	void OnEnable() {
		Events.CharacterDied += HandleCharacterDied;
	}
	void OnDisable() {
		Events.CharacterDied -= HandleCharacterDied;
	}

	void HandleCharacterDied (object sender, System.EventArgs e) {
		var characterHealth = sender as CharacterHealth;
		var character = characterHealth.GetComponent<Character>();
		if(character.gameObject.layer == LayerMask.NameToLayer("Player")) {
			GameOver = true;
			Time.timeScale = 0f;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer != LayerMask.NameToLayer("Player")) {
			return;
		}

		TempWin = true;
	}

	void OnGUI() {
		if(GameOver) {
			int halfScreenWidth = Screen.width >> 1;
			int halfScreenHeight = Screen.height >> 1;

			GUI.BeginGroup(new Rect(halfScreenWidth >> 1, halfScreenHeight >> 1, halfScreenWidth, halfScreenHeight));
				GUI.Box(new Rect(0f, 0f, halfScreenWidth, halfScreenHeight), "GAME OVER");
				if(GUI.Button(new Rect((halfScreenWidth >> 1) - 50f, halfScreenHeight - 30f, 100f, 25f), "Try Again?")) {
					Application.LoadLevel(Application.loadedLevel);
				}
			GUI.EndGroup();
		}

		if(TempWin) {
			int halfScreenWidth = Screen.width >> 1;
			int halfScreenHeight = Screen.height >> 1;
			
			GUI.BeginGroup(new Rect(halfScreenWidth >> 1, halfScreenHeight >> 1, halfScreenWidth, halfScreenHeight));
			GUI.Box(new Rect(0f, 0f, halfScreenWidth, halfScreenHeight), "YOU WON!");
			if(GUI.Button(new Rect((halfScreenWidth >> 1) - 50f, halfScreenHeight - 30f, 100f, 25f), "Play Again?")) {
				Application.LoadLevel(Application.loadedLevel);
			}
			GUI.EndGroup();
		}
	}
}
