using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour {


	public static CoinManager instance = null;
	public Text coinNumber;

	private int totalCoins;


	void Awake () {
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);    

		totalCoins = 0;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		coinNumber.text = totalCoins.ToString ();
	}


	public void AddCoin (int added) {
		totalCoins += added;
	}
}
