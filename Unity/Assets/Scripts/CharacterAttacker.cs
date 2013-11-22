using UnityEngine;
using System.Collections;

public class CharacterAttacker : MonoBehaviour {

	public LayerMask targetLayerMask;

	public float punchHitRadius = 0.1f;
	public Vector3 punchHitOffset = new Vector3(0.1f, 0.4f, 0f);
	
	public void Attack() {
		var directedPunchOffset = new Vector3(punchHitOffset.x * Mathf.Sign(transform.localScale.x), punchHitOffset.y);
		//Debug.DrawLine(transform.position + directedPunchOffset - Vector3.right * punchHitRadius, transform.position + directedPunchOffset + Vector3.right * punchHitRadius);
		//Debug.DrawLine(transform.position + directedPunchOffset - Vector3.up * punchHitRadius, transform.position + directedPunchOffset + Vector3.up * punchHitRadius);
		//Debug.Break();
		var colliders = Physics2D.OverlapCircleAll(transform.position + directedPunchOffset, punchHitRadius, targetLayerMask.value);
		foreach(var collider in colliders) {
			var damageable = collider.GetComponentInChildren(typeof(IDamageable)) as IDamageable;
			if(damageable == null) {
				continue;
			}

			var character = collider.GetComponent<Character>();
			if(character != null && character.IsBlocking) {
				continue;
			}

			damageable.TakeDamage(5f);
		}
	}
}
