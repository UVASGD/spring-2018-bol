using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour {

    public Text powerUpText;

	// Use this for initialization
	void Start () {
        powerUpText.text = "No PowerUp";
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void UpdatePowerUpText(PowerUp powerUp)
    {
        if (powerUp == null)
        {
            powerUpText.text = "No PowerUp";
        }
        else
        {
            powerUpText.text = "" + powerUp.GetType();
        }
    }
}
