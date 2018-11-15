using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private GravityControl g;
	private float jumpTimer;

	public float acceleration;
	public float jumpPower;
	public float airSpeed;
	public float lightModifier;
	private Vector3 move;
	public bool grounded = true;

	public float shortHopModifier;
	public float shortHop;

	public AudioClip jumpSFX;

	// Use this for initialization
	void Start () {
		g = GetComponent<GravityControl> ();
	}
	
	// Update is called once per frame
	void Update () {


		//Jumping stuff
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (grounded) {
				jumpTimer = shortHop;
			}
		} 

		if (jumpTimer > 0) {
			if (Input.GetKeyUp (KeyCode.Space)) {
				jumpTimer = 0;
				move = new Vector3 (0.0f, jumpPower * shortHopModifier, 0.0f);
				GetComponent<Rigidbody2D> ().AddForce (move, ForceMode2D.Impulse);
				grounded = false;
				this.GetComponent<AudioSource> ().PlayOneShot (jumpSFX);
			} else {
				jumpTimer -= Time.deltaTime;
				if (jumpTimer <= 0) {
					jumpTimer = 0;
					move = new Vector3 (0.0f, jumpPower, 0.0f);
					GetComponent<Rigidbody2D> ().AddForce (move, ForceMode2D.Impulse);
					grounded = false;
					this.GetComponent<AudioSource> ().PlayOneShot (jumpSFX);
				}
			}
		}


		float hor = Input.GetAxis ("Horizontal");

		if (grounded) move = new Vector3 (hor * acceleration, 0.0f, 0.0f);
		else move = new Vector3 (hor * acceleration * airSpeed, 0.0f, 0.0f);

		GetComponent<Rigidbody2D> ().AddForce (move);

		/*
		if (Input.GetKey (KeyCode.A)) {
			if (grounded)
				move = new Vector3 (-acceleration, 0.0f, 0.0f);
			else
				move = new Vector3 (-acceleration * airSpeed, 0.0f, 0.0f);
			if (g.currentState == GravityControl.WeightState.LIGHT)
				move = Vector3.Scale (move, new Vector3 (lightModifier, 0.0f, 0.0f));
			GetComponent<Rigidbody2D> ().AddForce (move);
		} else if (Input.GetKey (KeyCode.D)) {
			if (grounded)
				move = new Vector3 (acceleration, 0.0f, 0.0f);
			else
				move = new Vector3 (acceleration * airSpeed, 0.0f, 0.0f);
			if (g.currentState == GravityControl.WeightState.LIGHT)
				move = Vector3.Scale (move, new Vector3 (lightModifier, 0.0f, 0.0f));
			GetComponent<Rigidbody2D> ().AddForce (move);
		} else {
			if (grounded) {
				GetComponent<Rigidbody2D> ().velocity = Vector2.Lerp (GetComponent<Rigidbody2D> ().velocity, new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y), 0.05f);
			}
		}
		*/
	}

	void OnCollisionEnter2D (Collision2D obj) {
		if (obj.gameObject.tag == "Floor") {
			grounded = true;
		}
	}
/*
	void OnCollisionExit2D (Collision2D obj) {
		if (obj.gameObject.tag == "Floor") {
			grounded = false;
		}
	}
*/
}
