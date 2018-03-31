using UnityEngine;
using System.Collections;

// Applies an explosion force to all nearby rigidbodies
public class BombPowerUp : PowerUp
{

	public BombPowerUp()
	{

	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	public override void PowerUpEffect(){
		GameObject bombPrefab = Resources.Load ("Bomb") as GameObject;
		Object.Instantiate (bombPrefab, player.transform.position + new Vector3 (0.0f, 2.0f, 0.0f), Quaternion.identity);
	}
}