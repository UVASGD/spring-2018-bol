using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour {
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
	public bool ballInFlight = false;

	public float verticalAngleLowerBound = 1.0f;
	public float verticalAngleUpperBound = 179.0f;

	// Use this for initialization
	void Start () {
        myTransform = gameObject.transform;
		StartCoroutine(CheckCanSeePlayer());
	}
	
	// Update is called once per frame
	void Update () {
        direction = myTransform.position - target.position;
		if(direction.magnitude >= followDistance && !moving && !rotating && !shifting)
        {
			Vector3 moveTowardsPoint = Vector3.MoveTowards(myTransform.position, target.position, (direction.magnitude - followDistance) * 1.25f);
			StartCoroutine(moveTowards(moveTowardsPoint, 1.0f));
        } else if (direction.magnitude < minimumDistance && !ballInFlight && !backingUp && !shifting) {
			StartCoroutine(moveBack(target.position, (direction.magnitude - minimumDistance)*1.5f, desiredAngle - Mathf.Asin(direction.y/direction.magnitude)*Mathf.Rad2Deg));
		}
		if (!ballInFlight) {
			float rotateH = Input.GetAxis("CameraHorizontal");
			float rotateV = Input.GetAxis("CameraVertical");
			shifting = Mathf.Abs(rotateH) > 0.05f || Mathf.Abs(rotateV) > 0.05f;
			myTransform.RotateAround(target.position, Vector3.up, rotateH);
			if ((Vector3.Angle(direction, Vector3.up) > verticalAngleLowerBound || rotateV < 0) && 
				(Vector3.Angle(direction, Vector3.up) < verticalAngleUpperBound || rotateV > 0)){
				myTransform.RotateAround(target.position, Vector3.Cross(direction, Vector3.up), rotateV);
			}
		}
        myTransform.LookAt(target);
	}

	public void ballEnterFlight() {
		Debug.Log("Ball entering flight");
		followDistance = 30.0f;
		ballInFlight = true;
	}

	public void ballLeaveFlight() {
		followDistance = 10.0f;
		ballInFlight = false;
	}

	IEnumerator CheckCanSeePlayer() {
		while (gameObject.activeInHierarchy) {
			if (!moving && !rotating && !shifting && ballInFlight) {
				if (Physics.Linecast(myTransform.position, target.position)) {
					Debug.Log("Finding a new position!");
					Vector3 newPosition = FindGoodPosition();
					if (newPosition != Vector3.zero) StartCoroutine(moveTowards(newPosition, 1.0f));
					else Debug.Log("Could not find a new position!");
				}
			}
			yield return new WaitForSeconds(3);
		}
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
		Destroy(finalGO);
	}

	Vector3 FindGoodPosition() {
		float angleIncrement = 360.0f/12; // Reduce the divisor (denominator) to increase performance
		GameObject rotatee = new GameObject();
		Vector3 initialCameraPosition = myTransform.position;
		rotatee.transform.position = myTransform.position;
		for (float vAngle = 0.0f; vAngle < 360.0f; vAngle += angleIncrement) {
			for (float hAngle = 0.0f; hAngle < 360.0f; hAngle += angleIncrement) {
				RotateObjectAroundTarget(initialCameraPosition, rotatee, vAngle, hAngle);
				if (!Physics.Linecast(rotatee.transform.position, target.transform.position)) {
					Vector3 goodPosition = rotatee.transform.position;
					Destroy(rotatee);
					return goodPosition;
				} else {
					Debug.Log("Position of " + rotatee.transform.position + " was not good");
					RaycastHit hit;
					Physics.Raycast(rotatee.transform.position, (target.position - rotatee.transform.position), out hit);
					Debug.Log(hit.collider.gameObject.name);
				}
			}
		}
		Destroy(rotatee);
		return Vector3.zero;
	}

	void RotateObjectAroundTarget(Vector3 initialCameraPosition, GameObject rotatee, float vAngle, float hAngle) {
		Vector3 rotateeToTarget = rotatee.transform.position - target.transform.position;
		rotatee.transform.position = initialCameraPosition;
		rotatee.transform.RotateAround(target.position, Vector3.up, hAngle);
		Vector3 vRotateAxis;
		if (Vector3.Angle(rotateeToTarget, Vector3.up) < 1.0f) {
			vRotateAxis = Vector3.Cross(Vector3.down, rotateeToTarget);
		} else {
			vRotateAxis = Vector3.Cross(rotateeToTarget, Vector3.up);
		}
		rotatee.transform.RotateAround(target.position, vRotateAxis, vAngle);
	}
}
