using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {

	private Vector2 bounce;

	public AudioClip bounceSFX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D obj) {
		bounce = obj.relativeVelocity * -1;
		bounce.x = 0;
		if (bounce.y > 1) {
			obj.gameObject.GetComponent<PlayerMovement> ().grounded = false;
			obj.gameObject.GetComponent<Rigidbody2D> ().AddForce (bounce, ForceMode2D.Impulse);
			this.GetComponent<AudioSource> ().PlayOneShot (bounceSFX);
		} else {
			obj.gameObject.GetComponent<PlayerMovement> ().grounded = true;
		}
	}
}
