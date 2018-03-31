using UnityEngine;
using System.Collections;

// Applies an inverse explosion force to all nearby rigidbodies
public class GravityVortexPowerUp : PowerUp
{

	public GravityVortexPowerUp()
	{

	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	public override void PowerUpEffect(){
		GameObject vortexPrefab = Resources.Load ("Gravity Vortex") as GameObject;
		Object.Instantiate (vortexPrefab, player.transform.position + new Vector3 (0.0f, 2.0f, 0.0f), Quaternion.identity);
	}
}