using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


	public GameObject target;
	public float speed;

	private float staticY;  // The camera shouldn't move up and down


	// Use this for initialization
	void Start () {
		staticY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = new Vector3 (target.transform.position.x, transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp (transform.position, temp, speed);
	}
}
