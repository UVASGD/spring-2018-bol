﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp {

    protected GameObject player;
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

    public GameObject Player
    {
        get
        {
            return player;
        }

        set
        {
            player = value;
        }
    }


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

    public virtual void UndoEffect() {
        Debug.Log("The Called Powerup does not have an undo effect.");
    }
}
