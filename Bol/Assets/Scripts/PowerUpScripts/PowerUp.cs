using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp {

    protected GameObject player;
    protected bool endsOnTurn;
    protected bool used;

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

    public bool WasUsed
    {
        get
        {
            return used;
        }

        set
        {
            used = value;
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

    public virtual bool UndoEffect() { //Returns whether or not to delete the powerup (true = delete)
        Debug.Log("The Called Powerup does not have an undo effect.");
        return true;
    }
}
