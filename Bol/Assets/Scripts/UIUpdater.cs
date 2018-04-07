using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour {

	public Image powerUpImage;

	// Use this for initialization
	void Start () {
        powerUpImage.sprite = null;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void UpdatePowerUpText(PowerUp powerUp)
    {
        if (powerUp == null)
        {
			powerUpImage.sprite = null;
        }
        else
        {
			var pwrSprite = Resources.Load<Sprite> ("" + powerUp.GetType ());
			if (pwrSprite != null) {
				powerUpImage.sprite = pwrSprite;
			} else {
				Debug.Log ("Sprite not found.");
			}
        }
    }
}
