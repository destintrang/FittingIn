using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	public bool last = false;
	public AudioClip finishSFX;

	void OnTriggerEnter2D (Collider2D obj) {
		if (obj.gameObject.tag == "Player") {

			this.GetComponent<AudioSource> ().PlayOneShot (finishSFX);

			if (last) {
				GameObject.Find ("Player Manager").GetComponent<PlayerManager> ().realVictory ();
			} else {
				GameObject.Find ("Player Manager").GetComponent<PlayerManager> ().victory ();
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
