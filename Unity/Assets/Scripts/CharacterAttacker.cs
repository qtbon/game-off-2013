using UnityEngine;
using System.Collections;

public class CharacterAttacker : MonoBehaviour {

	public LayerMask targetLayerMask;

	public float hitRadius = 0.1f;
	public Vector3 hitOffset = new Vector3(0.1f, 0.4f, 0f);
	
#if TRACE_HITBOX
	public void Update() {
		var directedPunchOffset = new Vector3(hitOffset.x * Mathf.Sign(transform.localScale.x), hitOffset.y);
		Debug.DrawLine(transform.position + directedPunchOffset - Vector3.right * hitRadius, transform.position + directedPunchOffset + Vector3.right * hitRadius);
		Debug.DrawLine(transform.position + directedPunchOffset - Vector3.up * hitRadius, transform.position + directedPunchOffset + Vector3.up * hitRadius);
	}
#endif
	
	public void Attack() {
		var directedPunchOffset = new Vector3(hitOffset.x * Mathf.Sign(transform.localScale.x), hitOffset.y);
		var colliders = Physics2D.OverlapCircleAll(transform.position + directedPunchOffset, hitRadius, targetLayerMask.value);
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
