// Smooth Follow from Standard Assets
// Converted to C# because I fucking hate UnityScript and it's inexistant C# interoperability
// If you have C# code and you want to edit SmoothFollow's vars ingame, use this instead.
using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
	
	// The target we are following
	public Transform target;
	// The distance in the x-z plane to the target
	public float distanceDefault = 4.0f;
	// the height we want the camera to be above the target
	public float heightDefault = 6.0f;
	// The distance in the x-z plane to the target
	private float distance = 4.0f;
	// the height we want the camera to be above the target
	private float height = 6.0f;
	// How much we 
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;

	private Vector3 offsetToTarget;

	public void UpdateOffsetOnStart() 
	{
		offsetToTarget = transform.position - target.position;
	}

	void LateUpdate () 
	{
		if (!target) return;

		if(Input.GetMouseButton(2))
		{

			distance = distanceDefault;
			height = heightDefault;

			float wantedRotationAngle = target.eulerAngles.y;
			float wantedHeight = target.position.y + height;
			
			float currentRotationAngle = transform.eulerAngles.y;
			float currentHeight = transform.position.y;
			
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

	        transform.position = target.position;

	        transform.position -= currentRotation * Vector3.forward * distance;

	        transform.position = new Vector3(transform.position.x,currentHeight,transform.position.z);
			
			transform.LookAt(target);

			offsetToTarget = transform.position - target.position;

			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			return;
		}

		if (Input.GetMouseButtonUp (2)) 
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}

		transform.position = target.position + offsetToTarget;
	}
}