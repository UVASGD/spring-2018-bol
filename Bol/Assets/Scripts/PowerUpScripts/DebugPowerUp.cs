using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPowerUp : PowerUp {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void PowerUpEffect()
    {
        Debug.Log("DEBUG POWER UP! WOO!");
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000, 0));
    }
}
