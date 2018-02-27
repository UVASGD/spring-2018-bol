using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	public GameObject[] players;
	public UnifiedInput inputController;

	int curPlayerIndex = 0;

	// Use this for initialization
	void Start () {
		foreach (GameObject player in players) {
			if (player.GetComponent<PlayerInput>() == null || player.GetComponent<PlayerControl>() == null) {
				Debug.LogError("Player Input or Player Control on " + player + " is null");
				Debug.Break();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.T)) {
			players[curPlayerIndex].GetComponent<PlayerInput>().enabled = false;
			players[curPlayerIndex].GetComponent<PlayerControl>().enabled = false;
			curPlayerIndex = (curPlayerIndex + 1) % players.Length ;
			Camera.main.GetComponent<CameraFollowPlayer>().target = players[curPlayerIndex].transform;
			players[curPlayerIndex].GetComponent<PlayerInput>().enabled = true;
			players[curPlayerIndex].GetComponent<PlayerControl>().enabled = true;
			inputController.curInput = players[curPlayerIndex].GetComponent<PlayerInput>();
			inputController.curPlayer = players[curPlayerIndex].GetComponent<PlayerControl>();
            inputController.curPowerup = players[curPlayerIndex].GetComponent<PlayerPowerUpController>();
		}
	}
}
