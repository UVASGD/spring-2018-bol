using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterTime : MonoBehaviour {

    public float secondsToWait = 120;
    float timewaited;
	// Use this for initialization
	void Start () {
        timewaited = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timewaited += Time.deltaTime;
        //Debug.Log(timewaited);
        if(timewaited >= secondsToWait)
        {
            Destroy(gameObject);
        }
	}
}
