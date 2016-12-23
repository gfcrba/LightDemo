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
	public float distanceAltDefault = 3.5f;
	// the height we want the camera to be above the target
	public float heightAltDefault = 1.5f;
	// The distance in the x-z plane to the target
	public float distance = 4.0f;
	// the height we want the camera to be above the target
	public float height = 6.0f;
	// How much we 
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;

	public bool frontView = false;
    //public Camera fakeCamera;

    // Place the script in the Camera-Control group in the component menu
	[AddComponentMenu("Camera-Control/Smooth Follow")]
	
	void LateUpdate () {
		// Early out if we don't have a target
		if (!target) return;
		Event e = Event.current;
		if (Input.GetKey(KeyCode.LeftAlt)) {
			distance = distanceAltDefault;
			height = heightAltDefault;
		} else {
			distance = distanceDefault;
			height = heightDefault;
		}
		// Calculate the current rotation angles
		float wantedRotationAngle = target.eulerAngles.y + (frontView? 180 : 0);
		float wantedHeight = target.position.y + height;
		
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		
		// Damp the height
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
		// Convert the angle into a rotation
		var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        //fakeCamera.transform.position = target.position;

        transform.position = target.position;

        //fakeCamera.transform.position = Vector3.forward * distance;
        transform.position -= currentRotation * Vector3.forward * distance;

        //fakeCamera.transform.position = new Vector3(transform.position.x, wantedHeight, transform.position.z);
        // Set the height of the camera
        transform.position = new Vector3(transform.position.x,currentHeight,transform.position.z);
		
		// Always look at the target
		transform.LookAt(target);
        //fakeCamera.transform.LookAt(target);
	}
}