using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

	public GameObject bomb;
	public Transform firePoint;

	private float fireRate = 0.75f;
	private bool canShoot = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)) && canShoot) {
			StartCoroutine (shoot ());
		}
	}


	IEnumerator shoot() {
		Debug.Log("shot");
		canShoot = false;
		Instantiate (bomb, firePoint.position, firePoint.rotation); 
		yield return new WaitForSeconds (fireRate);
		canShoot = true;
	}
}


