using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBoost : PowerUp {

	// Use this for initialization
	void Start () {
		
	}
	
    public RocketBoost()
    {

    }

	// Update is called once per frame
	void Update () {
		
	}

    public override void PowerUpEffect(GameObject player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        Vector3 newVelocity = rb.velocity;
        newVelocity.y = 0;
        newVelocity *= 50.0f;
        rb.velocity = newVelocity;
        Debug.Log("ZOOOOOM");
    }
}
