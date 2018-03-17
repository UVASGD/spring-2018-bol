using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    // Put on powerups!
	// Use this for initialization
	// Add new Powerups Here (pt. 1 of 2)
    public enum PowerUpList { RANDOM = -1, RocketBoost, Brake }
	void Start () {
		
	}
    public PowerUpList powerUpID;
	// Update is called once per frame
	void Update () {
		
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
                chosenPowerUp = new RocketBoost(gameObject);
                //print("Chosen Power null? : " + (chosenPowerUp == null));
                break;
			case PowerUpList.Brake:
				chosenPowerUp = new BrakePowerUp (gameObject);
				break;
        }
        
        return chosenPowerUp;
    }
}
