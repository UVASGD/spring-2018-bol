using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySmallRandomMovement : MonoBehaviour {

    // Use this for initialization
    public float amount = 0.5f;
	void Start () {
        gameObject.transform.Translate(new Vector3(Random.Range(-amount, amount), Random.Range(-amount, amount), Random.Range(-amount, amount)));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
