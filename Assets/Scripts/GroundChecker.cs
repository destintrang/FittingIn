using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {


	public PlayerMovement p;
	public List<Collider2D> colliders;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (IsGrounded ()) {
			p.grounded = true;
		} else {
			p.grounded = false;
		}
	}


	bool IsGrounded () {
		bool returnedBool = false;
		foreach (Collider2D c in colliders) {
			if (c.gameObject.tag == "Floor") {
				returnedBool = true;
			}
		}
		return returnedBool;
	}

	void OnTriggerEnter2D (Collider2D obj) {
		colliders.Add (obj);
	}

	void OnTriggerExit2D (Collider2D obj) {
		print (obj.gameObject.name);
		if (obj.gameObject.tag == "Floor") {
			p.grounded = false;
		}
		colliders.Remove (obj);
	}
}
