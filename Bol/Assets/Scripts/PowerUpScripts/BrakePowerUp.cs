using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakePowerUp : PowerUp {



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void PowerUpEffect(GameObject player){
		Rigidbody playerAtt = player.GetComponent<Rigidbody>();
		// Hard stops horizontal-scale movement. Vertical velocity is unaffected.
		Vector3 zeroMomentum = new Vector3 (0, playerAtt.velocity.y, 0);
		playerAtt.velocity = zeroMomentum;
	}
}
