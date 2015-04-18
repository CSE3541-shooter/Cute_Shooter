using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;

	void Awake()
	{
		floorMask = LayerMask.GetMask("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		//get the input
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		Move (h, v);
		Turing ();
		Animating (h, v);
	}

	void Move(float h, float v)
	{
		//use the input to move the character
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turing()
	{
		//control the character to turn around following the mouse
		//cast the ray from the camera to the mouse (should be on the floor)
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			//get the rotation from the current position to the position the mouse point 
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);

		}
	}

	void Animating(float h, float v)
	{
		//if player press horizontal or vertical aixs then the character will walk
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);


	}
}
