using UnityEngine;
using System.Collections;


public class Tilemap : MonoBehaviour {

	public class Level {
		public uint Width { get; set; }
		public uint Height { get; set; }

		public class World {
			string Tileset { get; set; }
			uint[][] Tiles { get; set; }
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
