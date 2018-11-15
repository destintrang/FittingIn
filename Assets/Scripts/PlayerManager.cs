using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			if (Time.timeScale == 0) {
				Time.timeScale = 1;
				GameObject.Find("Canvas").transform.Find ("Paused").gameObject.SetActive (false);
			} else {
				Time.timeScale = 0;
				GameObject.Find("Canvas").transform.Find ("Paused").gameObject.SetActive (true);
			}
		}
	}

	public void death () {
		Time.timeScale = 0;
		GameObject.Find("Canvas").transform.Find ("Lose").gameObject.SetActive (true);
	}

	public void victory () {
		Time.timeScale = 0;
		GameObject.Find("Canvas").transform.Find ("Cleared").gameObject.SetActive (true);
	}
	public void realVictory () {
		Time.timeScale = 0;
		GameObject.Find("Canvas").transform.Find ("Win").gameObject.SetActive (true);
	}

	public void returnToMenu () {
		SceneManager.LoadScene (1);
		Time.timeScale = 1;
	}

	public void restartLevel () {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		Time.timeScale = 1;
	}

	public void nextLevel () {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
		Time.timeScale = 1;
	}

}
