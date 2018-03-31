using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePoint : MonoBehaviour {

    float distanceMoved;
    const float maxHeightToMove = 5;
    bool movingUp = true;

	// Use this for initialization
	void Start () {
        distanceMoved = 0;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
        transform.Translate(new Vector3(0, 0.0625f, 0) * Time.deltaTime * (movingUp ? 1 : -1));
        if(distanceMoved >= maxHeightToMove && movingUp)
        {
            movingUp = false;
        }
        if(distanceMoved <= 0 && !movingUp)
        {
            movingUp = true;
        }
        distanceMoved += 10 * Time.deltaTime * (movingUp ? 1 : -1);
    }
}
