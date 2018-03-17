using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakePowerUp : PowerUp {

	// Use this for initialization
	void Start () {
		Debug.Log ("PowerUpEffect activated or some shit");
	}

	public BrakePowerUp(){
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void PowerUpEffect(){
		Debug.Log ("PowerUpEffect activated or some shit");
	}
}
