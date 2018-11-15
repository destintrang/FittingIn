using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour {

	public float interpVelocity;
	public float minDistance;
	public float followDistance;
	public GameObject target;
	public Vector3 offset;
	Vector3 targetPos;
	
	void Start ()
	{
		this.transform.position = target.transform.position - new Vector3(0, 0, 5);
		targetPos = transform.position;
	}


	void Update () 
	{
		if (target) 
		{
			Vector3 posNoZ = transform.position;
			posNoZ.z = target.transform.position.z;
			Vector3 targetDirection = (target.transform.position - posNoZ);
			interpVelocity = targetDirection.magnitude * 5f;
			targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);
			transform.position = Vector3.Lerp (transform.position, targetPos + offset, 5);
		}
	}
}
