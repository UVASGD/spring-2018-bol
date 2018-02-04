using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
    public Transform target;
    private Transform myTransform;
    private Vector3 direction;
	// Use this for initialization
	void Start () {
        myTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
        direction = myTransform.position - target.position;
        if(direction.magnitude >= 30)
        {
            myTransform.position = Vector3.MoveTowards(myTransform.position, new Vector3(target.position.x, myTransform.position.y, target.position.z), 1);
        }
        myTransform.LookAt(target);
	}
}
