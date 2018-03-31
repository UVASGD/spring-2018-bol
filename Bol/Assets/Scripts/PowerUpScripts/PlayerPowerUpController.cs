using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpController : MonoBehaviour {

    PowerUp storedPowerUp = null;

    [SerializeField]
    bool hasPowerUp = false;

    float timeLeft;

	// Use this for initialization
	void Start () {

	}

    private void Timer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0.05)
        {
            Debug.Log("Removing power up!");
            storedPowerUp.UndoEffect();
            storedPowerUp = null;
            hasPowerUp = false;
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
            Debug.Log(gameObject.name + "Using PowerUp");
            storedPowerUp.PowerUpEffect();
            if (!storedPowerUp.EndsOnTurn)
            {
                storedPowerUp = null;
                hasPowerUp = false;
            }
        }
        else
        {
            //Debug.Log("NO POWERUP WTF U DOIN");
        }
    }

    public void AddPowerup(PowerUp newPowerUp)
    {
        if(newPowerUp == null)
        {
            print("That Powerup you gave me is null wtf.");
        }
        if(storedPowerUp == null && newPowerUp != null)
        {
            Debug.Log("ADDED NEW POWERUP " + newPowerUp.GetType());
            storedPowerUp = newPowerUp;
            storedPowerUp.Player = gameObject;
            hasPowerUp = true;
            Debug.Log(gameObject.name + ", " + storedPowerUp == null);
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

    public PowerUp GetStoredPowerUp()
    {
        Debug.Log("Getting powerup from " + gameObject.name + ", " + (storedPowerUp == null).ToString());
        return storedPowerUp;
    }
}
