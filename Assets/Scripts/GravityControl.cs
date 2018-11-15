using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour {

    public float lightScale;
    public float heavyScale;

    public Sprite beachBall;
    public Sprite bowlingBall;
    private Sprite original;

    public AudioClip transformSFX;

    public enum WeightState { NORMAL, HEAVY, LIGHT };
    public WeightState currentState;


	// Use this for initialization
	void Start () {
		original = this.GetComponent<SpriteRenderer> ().sprite;
        currentState = WeightState.NORMAL;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.S)) {
			this.GetComponent<AudioSource> ().PlayOneShot (transformSFX);
		} else if (Input.GetKeyDown (KeyCode.W)) {
			this.GetComponent<AudioSource> ().PlayOneShot (transformSFX);
		}

		if (Input.GetKey (KeyCode.W)) {
			currentState = WeightState.LIGHT;
			GetComponent<Rigidbody2D> ().gravityScale = lightScale;
			this.GetComponent<SpriteRenderer> ().sprite = beachBall;
		} else if (Input.GetKey (KeyCode.S)) {
			currentState = WeightState.HEAVY;
			GetComponent<Rigidbody2D> ().gravityScale = heavyScale;
			this.GetComponent<SpriteRenderer> ().sprite = bowlingBall;
		} else {
			currentState = WeightState.NORMAL;
			GetComponent<Rigidbody2D> ().gravityScale = 1;
			this.GetComponent<SpriteRenderer> ().sprite = original;
			//this.GetComponent<AudioSource> ().PlayOneShot (transformSFX);
		}
	}
}
