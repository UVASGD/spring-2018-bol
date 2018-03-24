using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp {

    protected GameObject player;
    protected int duration;
    protected bool endsOnTurn;

    public bool EndsOnTurn
    {
        get
        {
            return endsOnTurn;
        }

        set
        {
            endsOnTurn = value;
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    abstract public void PowerUpEffect();
}
