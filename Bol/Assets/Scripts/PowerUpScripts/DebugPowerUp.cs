using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPowerUp : PowerUp {

	// Use this for initialization
	void Start () {
        //Debug.Log("DebugPowerUp Start() Called!");
	}
    // I dunno. This may only be necessary for the trivial do-nothing one.
    public DebugPowerUp()
    {
        //Debug.Log("Debug Constructor Called!");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void PowerUpEffect()
    {
        Debug.Log("DEBUG POWER UP! WOO!");
    }
}
