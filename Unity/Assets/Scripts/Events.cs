using System;
using UnityEngine;
using System.Collections;

public class Events {
	public delegate void EventHandler(object sender, EventArgs e);

	public static event EventHandler CharacterDied = delegate {};
	public static void InvokeCharacterDied(object sender, EventArgs e) {
		CharacterDied(sender, e);
	}  

	public static event EventHandler ActorCreated = delegate {};
	public static void InvokeActorCreated(object sender, EventArgs e) {
		ActorCreated(sender, e);
	}

	public static event EventHandler ActorDestroyed = delegate {};
	public static void InvokeActorDestroyed(object sender, EventArgs e) {
		ActorCreated(sender, e);
	}
}
