using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSlickPowerUp : PowerUp {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	
	public override void PowerUpEffect() {

		GameObject oilSlickPrefab = Resources.Load ("OilSlick") as GameObject;
		Object.Instantiate (oilSlickPrefab, player.transform.position + new Vector3 (0.0f, 2.0f, 0.0f), Quaternion.identity);
		
	}

}
