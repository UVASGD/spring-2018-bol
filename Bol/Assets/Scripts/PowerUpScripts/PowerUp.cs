using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp {

    protected GameObject player;
    protected int duration;
    protected bool hasTimer;

    public bool HasTimer
    {
        get
        {
            return hasTimer;
        }

        set
        {
            hasTimer = value;
        }
    }

    public int Duration
    {
        get
        {
            return duration;
        }

        set
        {
            duration = value;
        }
    }

    // This is a superclass. All powerups inherit from this class
    public PowerUp(GameObject player, int duration = 0)
    {
        this.player = player;
        this.Duration = duration;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    abstract public void PowerUpEffect();

    public void UndoEffect() {

    }
}
