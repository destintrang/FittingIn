using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D obj) {
		if (obj.gameObject.tag == "Player") {
			GameObject.Find ("Player Manager").GetComponent<PlayerManager> ().death ();
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
