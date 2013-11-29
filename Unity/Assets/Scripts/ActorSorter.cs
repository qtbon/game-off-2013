using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Sorts list of actors to give sprite renderers appearance of depth
/// </summary>
public class ActorSorter : MonoBehaviour {

	public List<Transform> actors;

	void Start() {

		StartCoroutine(Sort ());
	}

	void OnEnable() {
		Events.ActorCreated += HandleCharacterCreated;
	}

	void OnDisable() {
		Events.ActorCreated -= HandleCharacterCreated;
	}

	void HandleCharacterCreated (object sender, System.EventArgs e) {
		actors.Add((sender as MonoBehaviour).transform);
	}

	// Update is called once per frame
	IEnumerator Sort () {
		while(true) {
			yield return new WaitForSeconds(0.2f);
			// Remove any items that have been destroyed
			actors.RemoveAll(c => c == null);
			// Sort
			Transform[] sorted = actors.OrderByDescending(c => c.position.y).ToArray();
			for(int i=0; i<sorted.Length; ++i) {
				(sorted[i].renderer as SpriteRenderer).sortingOrder = i;
			}
			sorted = null;
		}
	}
}
