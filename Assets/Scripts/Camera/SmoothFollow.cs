﻿// Smooth Follow from Standard Assets
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

    private float maxDistance = 4.0f;
    private float maxHeight = 6.0f;

    private float rotationAngle = 0.0f;

	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;

	private Vector3 offsetToTarget;
    private CharacterMovement characterMovementScript;

	public void UpdateOffsetOnStart() 
	{
		offsetToTarget = transform.position - target.position;
        characterMovementScript = target.GetComponent<CharacterMovement>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            FlushCamera();
        }
    }

	void LateUpdate () 
	{
		if (!target) return;

        if (characterMovementScript.backCamera)
		{
            UpdateCameraHeight();

            UpdateCameraDistance();

            UpdateCameraRotation();

            distance = distanceDefault;
			height = heightDefault;

            float wantedRotationAngle = 0.0f;

            wantedRotationAngle = transform.eulerAngles.y + rotationAngle;
			
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

			return;
		}

        transform.position = target.position + offsetToTarget;
	}

    void UpdateCameraHeight()
    {
        float mouseInput = Input.GetAxis("Mouse Y") / 10.0f;
        if (Mathf.Abs(mouseInput) < 0.2f) mouseInput = 0.0f;
        heightDefault += mouseInput;

        if (heightDefault > maxHeight)
        {
            heightDefault = maxHeight;
        }
        else if (heightDefault < 2.0f)
        {
            heightDefault = 2.0f;
        }
    }

    void UpdateCameraRotation()
    {
        float mouseInput = Input.GetAxis("Mouse X") * rotationDamping;
        rotationAngle = mouseInput;
    }

    void UpdateCameraDistance()
    {
        float mouseInput = Input.mouseScrollDelta.y / 10.0f;

        distanceDefault -= mouseInput;

        if (distanceDefault > maxDistance)
        {
            distanceDefault = maxDistance;
        }
        else if (distanceDefault < 1.0f)
        {
            distanceDefault = 1.0f;
        }
    }

    void FlushCamera()
    {
        distanceDefault = maxDistance;
        heightDefault = maxHeight;
    }
}