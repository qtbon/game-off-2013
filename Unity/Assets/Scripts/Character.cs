using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterMover))]
public class Character : Actor {
	
	protected  Animator animator;
	protected  CharacterMover mover;
	
	public bool IsBlocking { get; protected set; }

	protected virtual void Start() {
		animator = GetComponent<Animator>();
		mover = GetComponent<CharacterMover>();
	}
}