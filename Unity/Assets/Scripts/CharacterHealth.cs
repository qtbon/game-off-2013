using UnityEngine;
using System.Collections;

public class CharacterHealth : MonoBehaviour, IDamageable {

	public float maxHealth = 10f;
	private float health;

	protected bool isVulnerable = true;

	public bool shouldDestroyOnDeath = true;

	void Awake() {
		health = maxHealth;
	}

	public void TakeDamage(float damage) {
		if(!isVulnerable) {
			return;
		}
		
		health -= damage;
		if(health <= 0f) {
			Events.InvokeCharacterDied(this, null);
			if(shouldDestroyOnDeath) {
				Destroy(this.gameObject);
				return;
			}
		}

		StartCoroutine(Invulnerable());
	}
	
	IEnumerator Invulnerable() {
		isVulnerable = false;
		var spriteRenderer = renderer as SpriteRenderer;
		var color = spriteRenderer.color;
		spriteRenderer.color = Color.red;
		yield return new WaitForSeconds(0.5f);
		spriteRenderer.color = color;
		isVulnerable = true;
	}
}
