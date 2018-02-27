using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public Rigidbody rb;

	public float maxTimerBeforeTurnOver = 1.0f;
	float curTimer = 0.0f;

	bool inFlight = false;

	// Use this for initialization
	void Start () {
		if (rb == null) {
			rb = GetComponent<Rigidbody>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (inFlight) {
			curTimer += Time.deltaTime;
		}
	}
		
	public void LaunchInDirection(Vector3 dir, float pow) {
		if (!inFlight) {
			Vector3 direction = dir.normalized;

			rb.AddForce(direction * pow);

			inFlight = true;
		}
	}

	public bool getPossibleTurnOver() {
		return curTimer > maxTimerBeforeTurnOver;
	}

	public void endTurn() {
		inFlight = false;
		curTimer = 0.0f;
	}
}
