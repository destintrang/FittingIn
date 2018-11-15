using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {


	public GameObject coinObj;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D (Collider2D obj) {
		if (obj.tag == "Player") {
			//coinObj.SetActive (false);
			GetComponent<CircleCollider2D>().enabled = false;
			CoinManager.instance.AddCoin (1);
			coinObj.GetComponent<Animator> ().SetTrigger ("Collect");
		}
	}
}
