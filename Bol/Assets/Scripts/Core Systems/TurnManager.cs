using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	public GameObject[] players;
	public UnifiedInput inputController;

	int curPlayerIndex = 0;

	public float minimumVelocity = 0.1f;

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
		PlayerControl curPlayerControl = players[curPlayerIndex].GetComponent<PlayerControl>();
		Rigidbody curPlayerRB = players[curPlayerIndex].GetComponent<Rigidbody>();
		PlayerInput curPlayerInput = players[curPlayerIndex].GetComponent<PlayerInput>();
        PlayerPowerUpController curPlayerPowerUp = players[curPlayerIndex].GetComponent<PlayerPowerUpController>();

        // If the current player has been in flight long enough AND has less velocity than the minimum velocity
        if (curPlayerControl.getPossibleTurnOver() && curPlayerRB.velocity.magnitude < minimumVelocity) {
			curPlayerRB.velocity = Vector3.zero;

			curPlayerInput.enabled = false;
			curPlayerControl.enabled = false;
			curPlayerControl.endTurn();

			curPlayerIndex = (curPlayerIndex + 1) % players.Length ;

			curPlayerControl = players[curPlayerIndex].GetComponent<PlayerControl>();
			curPlayerRB = players[curPlayerIndex].GetComponent<Rigidbody>();
			curPlayerInput = players[curPlayerIndex].GetComponent<PlayerInput>();

			Camera.main.GetComponent<CameraFollowPlayer>().target = players[curPlayerIndex].transform;
			Camera.main.GetComponent<CameraFollowPlayer>().ballLeaveFlight();

			curPlayerInput.enabled = true;
			curPlayerControl.enabled = true;

		curPlayerInput.enabled = true;
		curPlayerControl.enabled = true;
        curPlayerPowerUp.EndTurn();

		inputController.curInput = curPlayerInput;
		inputController.curPlayer = curPlayerControl;
		inputController.curPowerup = curPlayerPowerUp;
		switching = false;
	}
}
