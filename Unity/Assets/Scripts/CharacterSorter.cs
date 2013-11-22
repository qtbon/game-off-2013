using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Sorts list of characters to give sprite renderers appearance of depth
/// </summary>
public class CharacterSorter : MonoBehaviour {

	public List<Transform> characters;

	void Start() {

		StartCoroutine(Sort ());
	}

	void OnEnable() {
		Events.CharacterCreated += HandleCharacterCreated;
	}

	void OnDisable() {
		Events.CharacterCreated -= HandleCharacterCreated;
	}

	void HandleCharacterCreated (object sender, System.EventArgs e) {
		characters.Add((sender as MonoBehaviour).transform);
	}

	// Update is called once per frame
	IEnumerator Sort () {
		while(true) {
			yield return new WaitForSeconds(0.2f);
			// Remove any items that have been destroyed
			characters.RemoveAll(c => c == null);
			// Sort
			Transform[] sorted = characters.OrderByDescending(c => c.position.y).ToArray();
			for(int i=0; i<sorted.Length; ++i) {
				(sorted[i].renderer as SpriteRenderer).sortingOrder = i;
			}
			sorted = null;
		}
	}
}
