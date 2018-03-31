using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    // Put on powerups!
	// Use this for initialization
	// Add new Powerups Here (pt. 1 of 2)
    public enum PowerUpList { RANDOM = -1, RocketBoost, Brake, MoonJump, Bomb }
	void Start () {
		
	}
    public PowerUpList powerUpID;
	// Update is called once per frame
	void Update () {
		
	}

    public void Respawn()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
    }

    public PowerUp GetPowerUp()
    {
        if(powerUpID == PowerUpList.RANDOM)
        {
            Array values = Enum.GetValues(typeof(PowerUpList));
            powerUpID = (PowerUpList)UnityEngine.Random.Range(0, values.Length-1);
        }
        PowerUp chosenPowerUp = null;
	    // Add new Powerups here (pt 2 of 2)
        switch (powerUpID)
        {
            case PowerUpList.RocketBoost:
                //Debug.Log("Chose the Debug PowerUp!");
                chosenPowerUp = new RocketBoost();
                //print("Chosen Power null? : " + (chosenPowerUp == null));
                break;
			case PowerUpList.Brake:
				chosenPowerUp = new BrakePowerUp ();
				break;
            case PowerUpList.MoonJump:
                chosenPowerUp = new MoonJump();
                break;
			case PowerUpList.Bomb:
				chosenPowerUp = new BombPowerUp ();
				break;
        }
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        return chosenPowerUp;
    }
}
