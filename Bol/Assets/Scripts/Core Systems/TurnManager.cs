using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	public GameObject[] players;
	public UnifiedInput inputController;

	int curPlayerIndex = 0;
    int numPlayersPlaying;

	public float minimumVelocity = 0.1f;

	bool switching = false;
	bool confirming = false;

    private GameObject[] powerUps;

	const int WAIT_TIME = 3;

	int turnsSinceWin = 0;
	int firstWinningPlayerIndex = -1;
	bool[] playersWon;
    
    // Use this for initialization
	void Start () {
		foreach (GameObject player in players) {
			if (player.GetComponent<PlayerInput>() == null || player.GetComponent<PlayerControl>() == null) {
				Debug.LogError("Player Input or Player Control on " + player + " is null");
				Debug.Break();
			}
		}
        powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        numPlayersPlaying = players.Length;
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
			if (curPlayerControl.getPossibleTurnOver() && (curPlayerRB.velocity.magnitude < minimumVelocity || !curPlayerPoints.PlayerPlaying) && !switching) {
				if (curPlayerIndex == firstWinningPlayerIndex) turnsSinceWin++;
				if (!curPlayerPoints.PlayerPlaying) {
					if (firstWinningPlayerIndex == -1) firstWinningPlayerIndex = curPlayerIndex;
					playersWon[curPlayerIndex] = true;
				}
				if (turnsSinceWin > 5 || AllPlayersWon()) {
					// End the game!
				}
				StartCoroutine(switchTurn(curPlayerControl, curPlayerRB, curPlayerInput, curPlayerPowerUp));
			} else {
				confirming = false; // The player is moving, so we are no longer confirmed.
			}
			yield return new WaitForSeconds(WAIT_TIME);
		}
	}

	IEnumerator switchTurn(PlayerControl curPlayerControl, Rigidbody curPlayerRB, PlayerInput curPlayerInput, PlayerPowerUpController curPlayerPowerUp) {
		if (!confirming) {
			confirming = true; // We are currently confirming!
			Debug.Log("Confirming!");
			yield break;
		}
		// If we make it here, the player has been stationary for WAIT_TIME seconds!
		Debug.Log("Confirmed!");
		switching = true;
		curPlayerRB.velocity = Vector3.zero;

		curPlayerInput.enabled = false;
		curPlayerControl.enabled = false;
		curPlayerControl.endTurn();

		players[curPlayerIndex].GetComponent<Indicator>().indicatorObj.SetActive(false);
		curPlayerIndex = (curPlayerIndex + 1) % players.Length ;
		players[curPlayerIndex].GetComponent<Indicator>().indicatorObj.SetActive(true);

		curPlayerControl = players[curPlayerIndex].GetComponent<PlayerControl>();
		curPlayerRB = players[curPlayerIndex].GetComponent<Rigidbody>();
		curPlayerInput = players[curPlayerIndex].GetComponent<PlayerInput>();


		Camera.main.GetComponent<CameraFollowPlayer>().target = players[curPlayerIndex].transform;
		Camera.main.GetComponent<CameraFollowPlayer>().ballLeaveFlight();

		curPlayerInput.enabled = true;
		curPlayerControl.enabled = true;
        curPlayerPowerUp.EndTurn();

        // There might be a better way to do this...
        foreach(GameObject powerUp in powerUps)
        {
            powerUp.GetComponent<PowerUpManager>().Respawn();
        }

		inputController.curInput = curPlayerInput;
		inputController.curPlayer = curPlayerControl;
		inputController.curPowerup = curPlayerPowerUp;
		switching = false;
		confirming = false;
	}
}
