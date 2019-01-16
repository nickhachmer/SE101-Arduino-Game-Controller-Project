using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {

	public Rigidbody2D rb;
	public CircleCollider2D crcCol;

	private float speed = 15f;

	void Start() {
		gameObject.layer = 11;
	}

	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -25 || transform.position.x > 150) {
			Destroy (gameObject);
		} else if (colided()) {
			DestroyBomb ();
		}
	}

	void FixedUpdate() {
		rb.velocity = transform.right * speed; 
	}

	bool colided() {
		if (crcCol.IsTouchingLayers (1 << LayerMask.NameToLayer ("Ground"))) {
			return true;
		} else if (crcCol.IsTouchingLayers (1 << LayerMask.NameToLayer ("Wall"))) {
			return true;
		}
		return false;
	}

	public void DestroyBomb() {
		Destroy (gameObject);
	}
}
