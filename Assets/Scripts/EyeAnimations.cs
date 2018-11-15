using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAnimations : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float hor = Input.GetAxis ("Horizontal");
		GetComponent<Animator> ().SetFloat ("Direction", hor);
	}
}
