using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
    public Transform target;
    private Transform myTransform;
    private Vector3 direction;
	bool moving = false;
	float followDistance = 10.0f;
	// Use this for initialization
	void Start () {
        myTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
        direction = myTransform.position - target.position;
		if(direction.magnitude >= followDistance && !moving)
        {
			StartCoroutine(moveTowards(Vector3.MoveTowards(myTransform.position, new Vector3(target.position.x, myTransform.position.y, target.position.z), direction.magnitude - followDistance), 1.0f));
            //myTransform.position = Vector3.MoveTowards(myTransform.position, new Vector3(target.position.x, myTransform.position.y, target.position.z), 1);
        }
        myTransform.LookAt(target);
	}

	public void ballEnterFlight() {
		Debug.Log("Ball entering flight");
		followDistance = 30.0f;
	}

	public void ballLeaveFlight() {
		followDistance = 10.0f;
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
}
