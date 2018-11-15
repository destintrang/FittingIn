using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMovement : MonoBehaviour {

	public float width;
	public float speed;

	private bool right;
	private Vector3 maxLeft;
	private Vector3 maxRight;

	// Use this for initialization
	void Start () {
		maxLeft = this.transform.position - new Vector3 ((width / 2), 0, 0);
		maxRight = this.transform.position + new Vector3 ((width / 2), 0, 0);
		right = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (right) {
			this.transform.position += new Vector3 (speed, 0, 0);
			if (this.transform.position.x > maxRight.x) {
				right = false;
			}
		} else {
			this.transform.position -= new Vector3 (speed, 0, 0);
			if (this.transform.position.x < maxLeft.x) {
				right = true;
			}
		}
	}
}
