using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	public void Start () {
		SceneManager.LoadScene (3, UnityEngine.SceneManagement.LoadSceneMode.Single);
	}

	public void controls () {
		SceneManager.LoadScene (2, UnityEngine.SceneManagement.LoadSceneMode.Single);
	}

}
