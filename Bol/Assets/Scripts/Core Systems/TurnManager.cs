using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	public GameObject[] players;
	public UnifiedInput inputController;

    public UIUpdater uiUpdater;

	int curPlayerIndex = 0;

	public float minimumVelocity = 0.1f;

	bool switching = false;
	bool confirming = false;

    private GameObject[] powerUps;

	const int WAIT_TIME = 3;

	int turnsSinceWin = 0;
	int firstWinningPlayerIndex = -1;
	bool[] playersWon;

	bool firstLoop = true;

    // Use this for initialization
	void Start () {
		IComparer playerOrderer = new PlayerSorter();
		players = GameObject.FindGameObjectsWithTag("Player");
		Array.Sort(players, playerOrderer);
		foreach (GameObject player in players) {
			if (player.GetComponent<PlayerInput>() == null || player.GetComponent<PlayerControl>() == null) {
				Debug.LogError("Player Input or Player Control on " + player + " is null");
				Debug.Break();
			}
		}
        powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
		playersWon = new bool[players.Length];
		for (int i = 0; i < playersWon.Length; i++) {
			playersWon[i] = false;
		}
		StartCoroutine(checkTurnSwitch());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	bool AllPlayersWon() {
		foreach (bool b in playersWon) {
			if (!b) return false;
		}
		return true;
	}

	IEnumerator checkTurnSwitch() {
		while (gameObject.activeInHierarchy) {
			PlayerControl curPlayerControl = players[curPlayerIndex].GetComponent<PlayerControl>();
			Rigidbody curPlayerRB = players[curPlayerIndex].GetComponent<Rigidbody>();
			PlayerInput curPlayerInput = players[curPlayerIndex].GetComponent<PlayerInput>();
			PlayerPowerUpController curPlayerPowerUp = players[curPlayerIndex].GetComponent<PlayerPowerUpController>();
			PlayerPoints curPlayerPoints = players[curPlayerIndex].GetComponent<PlayerPoints>();
            if (firstLoop)
            {
                players[curPlayerIndex].GetComponent<Indicator>().indicatorObj.SetActive(true);
                firstLoop = false;
            }
            uiUpdater.UpdatePowerUpText(curPlayerPowerUp.GetStoredPowerUp());
            if (curPlayerControl.getPossibleTurnOver() && (curPlayerRB.velocity.magnitude < minimumVelocity || !curPlayerPoints.PlayerPlaying) && !switching) {
				if (curPlayerIndex == firstWinningPlayerIndex) turnsSinceWin++;
				if (!curPlayerPoints.PlayerPlaying) {
					if (firstWinningPlayerIndex == -1) firstWinningPlayerIndex = curPlayerIndex;
					playersWon[curPlayerIndex] = true;
				}
				if (turnsSinceWin > 5 || AllPlayersWon()) {
					// End the game!
				}
				StartCoroutine(switchTurn(curPlayerControl, curPlayerRB, curPlayerInput, curPlayerPowerUp, curPlayerPoints));
			} else {
				confirming = false; // The player is moving, so we are no longer confirmed.
			}
			yield return new WaitForSeconds(WAIT_TIME);
		}
	}

	IEnumerator switchTurn(PlayerControl curPlayerControl, Rigidbody curPlayerRB, PlayerInput curPlayerInput, PlayerPowerUpController curPlayerPowerUp, PlayerPoints curPlayerPoints) {
		if (!confirming && curPlayerPoints.PlayerPlaying) {
			confirming = true; // We are currently confirming!
			Debug.Log("Confirming!");
			yield break;
		}
		// If we make it here, the player has been stationary for WAIT_TIME seconds!
		Debug.Log("Confirmed!");
		switching = true;
		curPlayerRB.velocity = Vector3.zero;

		curPlayerPowerUp.EndTurn ();
		curPlayerInput.enabled = false;
		curPlayerControl.enabled = false;
		curPlayerControl.endTurn();

		players[curPlayerIndex].GetComponent<Indicator>().indicatorObj.SetActive(false);
		curPlayerIndex = (curPlayerIndex + 1) % players.Length ;
		Camera.main.GetComponent<CameraMan>().ballLeaveFlight();
		// There might be a better way to do this...
		foreach(GameObject powerUp in powerUps)
		{
			powerUp.GetComponent<PowerUpManager>().Respawn(players.Length);
		}
		if (players[curPlayerIndex].GetComponent<PlayerPoints>().PlayerPlaying) {
			players[curPlayerIndex].GetComponent<Indicator>().indicatorObj.SetActive(true);

			curPlayerControl = players[curPlayerIndex].GetComponent<PlayerControl>();
			curPlayerRB = players[curPlayerIndex].GetComponent<Rigidbody>();
			curPlayerInput = players[curPlayerIndex].GetComponent<PlayerInput>();
            curPlayerPowerUp = players[curPlayerIndex].GetComponent<PlayerPowerUpController>();


			Camera.main.GetComponent<CameraMan>().target = players[curPlayerIndex].transform;

			curPlayerInput.enabled = true;
			curPlayerControl.enabled = true;
	        curPlayerPowerUp.EndTurn();

			inputController.curInput = curPlayerInput;
			inputController.curPlayer = curPlayerControl;
			inputController.curPowerup = curPlayerPowerUp;
		}
		switching = false;
		confirming = false;
	}

	public class PlayerSorter : IComparer  {

		// Calls CaseInsensitiveComparer.Compare on the monster name string.
		int IComparer.Compare( System.Object x, System.Object y )  {
			return( (new CaseInsensitiveComparer()).Compare( ((GameObject)x).name, ((GameObject)y).name) );
		}

	}
}
