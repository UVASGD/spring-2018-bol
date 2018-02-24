using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		if (rb == null) {
			rb = GetComponent<Rigidbody>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void LaunchInDirection(Vector3 dir, float pow) {
		Vector3 direction = dir.normalized;

		rb.AddForce(direction * pow);
	}
}
