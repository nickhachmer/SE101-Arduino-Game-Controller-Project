using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public Transform player;

	// Use this for initialization
	void Start () {
		gameObject.transform.position = new Vector3 (player.position.x, player.position.y+3, -1);
	}
	
	// Update is called once per frame
	void Update () {

		if (player.position.x <= -14.6f) {
			gameObject.transform.position = new Vector3 (-14.6f, player.position.y + 3, -1);
		} else {
			gameObject.transform.position = new Vector3 (player.position.x, player.position.y + 3, -1);
		}
	}
}
