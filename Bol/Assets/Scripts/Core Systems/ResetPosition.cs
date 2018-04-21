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
		foreach (GameObject player in players) {
			playerPositions
		}
		checkSwitch = FindObjectOfType<TurnManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (checkSwitch.GetSwitchStatus()) {
			foreach(GameObject player in players){
				Vector3 playerPos = player.transform.position;
				print (playerPos);
			}
		}
	}
}
