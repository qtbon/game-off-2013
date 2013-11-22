using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Player player;
	public SpriteRenderer level;

	public float speed = 10;

	public float halfHeight;
	public float halfWidth;

	public bool IsLocked { get; private set; }

	// Use this for initialization
	void Start () {
		IsLocked = false;
		halfHeight = camera.orthographicSize;
		halfWidth = halfHeight * camera.aspect;
	}
	
	// Update is called once per frame
	void Update () {

		if(IsLocked) {
			return;
		}

		var pos = camera.transform.position;

		var targetX = player.transform.position.x - 0.1f;
		// clamp to level end
		targetX = Mathf.Clamp(targetX, 0f, (level.bounds.center.x + level.bounds.extents.x - halfWidth));
	
		pos.x += (targetX - pos.x) * Time.deltaTime * speed;

		camera.transform.position = pos;
	}

	public void Lock(){
		IsLocked = true;
	}

	public void Unlock(){
		IsLocked = false;
	}
}
