﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
    public Transform target;
    private Transform myTransform;
    private Vector3 direction;
	bool moving = false;
	bool rotating = false;
	bool shifting = false;
	bool backingUp = false;
	float followDistance = 10.0f;
	public float minimumDistance = 5.0f;
	public float desiredAngle = 30.0f; // in degrees
	public bool canRotate = true;
	// Use this for initialization
	void Start () {
        myTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
        direction = myTransform.position - target.position;
		if(direction.magnitude >= followDistance && !moving && !rotating && !shifting)
        {
			StartCoroutine(moveTowards(Vector3.MoveTowards(myTransform.position, new Vector3(target.position.x, myTransform.position.y, target.position.z), direction.magnitude - followDistance), 1.0f));
            //myTransform.position = Vector3.MoveTowards(myTransform.position, new Vector3(target.position.x, myTransform.position.y, target.position.z), 1);
		} else if (direction.magnitude < minimumDistance && !backingUp && !shifting) {
			Debug.Log("Less than minimum distance and not backing up or shifting!");
			Vector3 ballToCam = myTransform.position - target.position;
			StartCoroutine(moveBack(target.position, (ballToCam.magnitude - minimumDistance)*1.5f, desiredAngle - Mathf.Asin(ballToCam.y/ballToCam.magnitude)*Mathf.Rad2Deg));
		}
		if (canRotate) {
			float rotatey = Input.GetAxis("CameraRotate");
			if (Mathf.Abs(rotatey) > 0.05f) shifting = true;
			Debug.Log(target.position);
			myTransform.RotateAround(target.position, Vector3.up, rotatey);
		}
        myTransform.LookAt(target);
	}

	public void ballEnterFlight() {
		Debug.Log("Ball entering flight");
		followDistance = 30.0f;
		canRotate = false;
	}

	public void ballLeaveFlight() {
		followDistance = 10.0f;
		canRotate = true;
	}

	IEnumerator moveBack(Vector3 fromPos, float dist, float angle) {
		backingUp = true;
		StartCoroutine(moveTowards(Vector3.MoveTowards(myTransform.position, fromPos, dist), 1.0f));
		while (moving) { yield return null; }
		StartCoroutine(rotateTowards(fromPos, Vector3.Cross((myTransform.position - fromPos), Vector3.up), angle, 1.0f)); 
		backingUp = false;
	}

	IEnumerator moveTowards(Vector3 position, float time) {
		moving = true;
		Vector3 initialPosition = myTransform.position;
		for (float t = 0.0f; t <= time; t += Time.deltaTime) {
			myTransform.position = Vector3.Lerp(initialPosition, position, t);
			yield return null;
		}
		moving = false;
	}

	IEnumerator rotateTowards(Vector3 point, Vector3 axis, float amt, float time) {
		rotating = true;
		Vector3 initialPosition = myTransform.position;
		Quaternion initialRotation = myTransform.rotation;

		GameObject finalGO = new GameObject();
		finalGO.transform.position = myTransform.position;
		finalGO.transform.rotation = myTransform.rotation;

		finalGO.transform.RotateAround(point, axis, amt);
		Transform finalTransform = finalGO.transform;
		for (float t = 0.0f; t <= time; t += Time.deltaTime) {
			myTransform.rotation = Quaternion.Lerp(initialRotation, finalTransform.rotation, t);
			myTransform.position = Vector3.Lerp(initialPosition, finalTransform.position, t);
			yield return null;
		}
		rotating = false;
	}
}
