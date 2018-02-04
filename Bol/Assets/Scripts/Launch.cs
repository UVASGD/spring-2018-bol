using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour {

    private Transform trans;
    private Rigidbody rb;

    public Transform guideTransform;
    public float horizontalAngle;
    public float verticalAngle;
    public float launchPower = 10;
    
    private Vector3 forward;
    private Vector3 rotAxis;
    private Vector3 launchVector;
    private Quaternion finalRotation;
    private bool increasing;

	// Use this for initialization
	void Start () {
        trans = gameObject.transform;
        rb = gameObject.GetComponent<Rigidbody>();
        // Change this later
        forward = Vector3.forward;
        rotAxis = Vector3.right;
        launchVector = Vector3.forward;
        increasing = true;
	}
	
	// Update is called once per frame
	void Update () {
        forward = Quaternion.AngleAxis(horizontalAngle, Vector3.up) * Vector3.forward;
        rotAxis = Vector3.Cross(Vector3.up, forward);
        finalRotation = Quaternion.AngleAxis(verticalAngle, rotAxis);
        trans.rotation = finalRotation;
        launchVector = finalRotation * Vector3.up;
        launchVector = launchVector.normalized;

        if (Input.GetKey(KeyCode.Q))
        {
            horizontalAngle += 5;
            
        }

        if (Input.GetKey(KeyCode.E))
        {
            horizontalAngle -= 5;
           
        }

        if (Input.GetKey(KeyCode.W))
        {
            verticalAngle += 2.5f;
            if (verticalAngle >= 90) verticalAngle = 90;
        }

        if (Input.GetKey(KeyCode.S))
        {
            verticalAngle -= 2.5f;
            if (verticalAngle <= 0) verticalAngle = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (increasing)
            {
                launchPower += 10;
            }
            else
            {
                launchPower -= 10;
            }
            if(launchPower >= 500 && increasing)
            {
                increasing = false;
            }
            if(launchPower <= 10 && !increasing)
            {
                increasing = true;
            }
            guideTransform.localScale = new Vector3(guideTransform.localScale.x, launchPower / 500, guideTransform.localScale.z);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            print("LAUNCHING AT " + launchPower);
            rb.AddForce(launchVector * launchPower);
        }

        if (Input.GetKey(KeyCode.B))
        {
            if(rb.velocity.magnitude > 0)
            {
                rb.AddForce(-rb.velocity.normalized * 5);
            }
        }
	}
}
