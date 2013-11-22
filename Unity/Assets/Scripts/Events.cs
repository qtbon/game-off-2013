using System;
using UnityEngine;
using System.Collections;

public class Events {
	public delegate void EventHandler(object sender, EventArgs e);

	public static event EventHandler CharacterDied = delegate {};
	public static void InvokeCharacterDied(object sender, EventArgs e) {
		CharacterDied(sender, e);
	}  

	public static event EventHandler CharacterCreated = delegate {};
	public static void InvokeCharacterCreated(object sender, EventArgs e) {
		CharacterCreated(sender, e);
	}
}
