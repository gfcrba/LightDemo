using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleFollow : MonoBehaviour {

	public Transform target;
	private Vector3 offsetToTarget;

	void Start() {
		offsetToTarget = transform.position - target.position;
	}

	public void UpdateOffsetToTarget() {
		offsetToTarget = transform.position - target.position;
	}

	void LateUpdate () {
		transform.position = target.position + offsetToTarget;
	}
}
