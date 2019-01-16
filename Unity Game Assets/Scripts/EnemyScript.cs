using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public Rigidbody2D rb;
	public BoxCollider2D bxCol;
	public bool horizontal = true;
	public bool vertical = false;
	public float loop = 1.5f;
	public float speed = 10;

	private int direction = 1;
	private bool change = true;

	// Use this for initialization
	void Start () {
	}
		
	void FixedUpdate() {

		if (horizontal) {
			rb.velocity = new Vector2(direction * speed,0f);
		} else if (vertical) {
			rb.velocity = new Vector2(0f,direction * speed);
		}

		if (change) {
			StartCoroutine (changeDirections ());
		}
	}

	IEnumerator changeDirections() {
		direction *= -1;
		change = false;
		yield return new WaitForSeconds (loop);
		change = true;
	}

	void OnTriggerEnter2D(Collider2D col) {
		//Debug.Log ("should des");
		if (col.gameObject.tag == "Bomb") {
			col.gameObject.GetComponent<BombScript> ().DestroyBomb ();
			Destroy (gameObject);
		}
	}

}
