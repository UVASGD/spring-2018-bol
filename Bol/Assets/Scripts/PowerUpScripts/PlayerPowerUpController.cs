using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpController : MonoBehaviour {

    PowerUp storedPowerUp = null;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UsePowerup()
    {
        if (storedPowerUp != null)
        {
            storedPowerUp.PowerUpEffect();
            storedPowerUp = null;
        }
    }

    public void AddPowerup()
    {
        if(storedPowerUp == null)
        {

        }
    }
}
