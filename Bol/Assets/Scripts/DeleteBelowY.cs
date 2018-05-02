using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBelowY : MonoBehaviour {


    public float yValue = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.y < yValue) Destroy(gameObject);
	}
}
