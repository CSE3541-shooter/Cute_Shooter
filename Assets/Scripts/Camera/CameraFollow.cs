using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothing = 5f;

	Vector3 offset;

	void Start()
	{
		//get the offset between target and camera
		offset = transform.position - target.position;
	}

	void FixedUpdate()
	{
		//make the camera track the character
		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
