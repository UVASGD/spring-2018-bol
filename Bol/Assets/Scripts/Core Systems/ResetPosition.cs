using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ResetPosition : MonoBehaviour {

	public GameObject[] players;
	public Vector3[] playerPositions;
	public TurnManager checkSwitch;

	// Use this for initialization
	void Start () {
		if (players == null || players.Length == 0) players = GameObject.FindGameObjectsWithTag("Player");

		playerPositions = new Vector3[players.Length];
		// Initialize player initial position.
		for(int x = 0; x < players.Length; x++){
			playerPositions [x] = players [x].transform.position;
		}

		checkSwitch = FindObjectOfType<TurnManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (checkSwitch.GetCurrentPlayerRigidbody ().transform.position.y <= -50.0f) {
			checkSwitch.GetCurrentPlayerRigidbody ().transform.position = playerPositions [checkSwitch.GetCurrentPlayerIndex ()];
			checkSwitch.GetCurrentPlayerRigidbody ().velocity = new Vector3(0.0f, 0.0f, 0.0f);
		}
		if (checkSwitch.GetConfirmStatus()) {
			playerPositions [checkSwitch.GetCurrentPlayerIndex ()] = checkSwitch.GetCurrentPlayerRigidbody ().transform.position;
		}
	}
}
