using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp {

    // This is a superclass. All powerups inherit from this class
    public PowerUp()
    {

    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    abstract public void PowerUpEffect();
}
