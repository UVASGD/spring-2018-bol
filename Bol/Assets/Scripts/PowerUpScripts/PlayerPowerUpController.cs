using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpController : MonoBehaviour {

    PowerUp storedPowerUp = null;
    float timeLeft;

	// Use this for initialization
	void Start () {

	}

    private void Timer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0.05)
        {
            storedPowerUp.UndoEffect();
            storedPowerUp = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(storedPowerUp != null && storedPowerUp.EndsOnTurn)
        {
            Timer();
        }
	}
    public void UsePowerUp()
    {
        if (storedPowerUp != null)
        {
            storedPowerUp.PowerUpEffect();
            if (!storedPowerUp.EndsOnTurn)
            {
                storedPowerUp = null;
            }
        }
        else
        {
            Debug.Log("NO POWERUP WTF U DOIN");
        }
    }

    public void AddPowerup(PowerUp newPowerUp)
    {
        if(storedPowerUp == null && newPowerUp != null)
        {
            // Debug.Log("ADDED NEW POWERUP");
            storedPowerUp = newPowerUp;
            storedPowerUp.Player = gameObject;
        }
    }

    public void EndTurn()
    {
        if (storedPowerUp != null && storedPowerUp.EndsOnTurn)
        {
            storedPowerUp.UndoEffect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ObjectIsPowerUp(other.gameObject))
        {
            //Debug.Log("Collided with a powerup!");
            PowerUpManager powerUpManager = other.GetComponent<PowerUpManager>();
            AddPowerup(powerUpManager.GetPowerUp());
            Destroy(other.gameObject);
        }
    }

    bool ObjectIsPowerUp(GameObject obj)
    {
        // Check if the other object allows picking up of power ups.
        if (obj.GetComponent<PowerUpManager>() == null)
        {
            return false;
        }

        // Allow object to take powerup
        return true;
    }
}
