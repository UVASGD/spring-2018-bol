﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour {
	
	public int turnsAfterWinUntilEndGame = 5;

	private GameObject[] players = null;
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

	void Awake()
	{
		players = null;
		GetPlayers();
	}
	
    // Use this for initialization
	void Start ()
	{
		IComparer playerOrderer = new PlayerSorter();
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
		players[curPlayerIndex].GetComponent<PlayerInput>().enabled = true;
		StartCoroutine(CheckTurnSwitch());
	}

	private bool AllPlayersWon() {
		foreach (bool b in playersWon) {
			if (!b) return false;
		}
		return true;
	}

	IEnumerator CheckTurnSwitch() {
		while (true)
		{
			if (gameObject.activeInHierarchy)
			{
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
				
				if (turnsSinceWin > turnsAfterWinUntilEndGame || AllPlayersWon())
				{
					// End the game!
					Debug.Log("GAME HAS ENDED!");
					SceneManager.LoadScene("GameEndScreen");
				}
				
				if (curPlayerControl.getPossibleTurnOver() &&
				    (curPlayerRB.velocity.magnitude < minimumVelocity || !curPlayerPoints.PlayerPlaying))
				{
					if (!curPlayerPoints.PlayerPlaying)
					{
						Debug.Log("Player has won!");
						if (firstWinningPlayerIndex == -1)
						{
							firstWinningPlayerIndex = curPlayerIndex;
							Debug.Log("First player has won!");
						}
						playersWon[curPlayerIndex] = true;
					}

					StartCoroutine(switchTurn(curPlayerControl, curPlayerRB, curPlayerInput, curPlayerPowerUp, curPlayerPoints));
				}
				else
				{
					confirming = false; // The player is moving, so we are no longer confirmed.
				}
				
				if (AllPlayersWon())
				{
					// End the game!
					Debug.Log("GAME HAS ENDED!");
					SceneManager.LoadScene("GameEndScreen");
				}

				yield return new WaitForSeconds(WAIT_TIME);
			}
		}
	}

	IEnumerator switchTurn(PlayerControl curPlayerControl, Rigidbody curPlayerRB, PlayerInput curPlayerInput, PlayerPowerUpController curPlayerPowerUp, PlayerPoints curPlayerPoints) {
		if (!confirming && curPlayerPoints.PlayerPlaying) {
			confirming = true; // We are currently confirming!
			Debug.Log("Confirming!");
			yield break;
		}
		// If we make it here, the player has been stationary for WAIT_TIME seconds or they've won!
		Debug.Log("Confirmed!");
		curPlayerRB.velocity = Vector3.zero;

		curPlayerPowerUp.EndTurn ();
		curPlayerInput.enabled = false;
		curPlayerControl.enabled = false;
		curPlayerControl.endTurn();

		players[curPlayerIndex].GetComponent<Indicator>().indicatorObj.SetActive(false);
		curPlayerIndex = (curPlayerIndex + 1) % players.Length;
		Camera.main.GetComponent<CameraMan>().ballLeaveFlight();
		// There might be a better way to do this...
		foreach(GameObject powerUp in powerUps)
		{
			powerUp.GetComponentInChildren<PowerUpManager>().Respawn(players.Length);
		}

		int playersWonCounter = 0;
		
		while (!players[curPlayerIndex].GetComponent<PlayerPoints>().PlayerPlaying)
		{
			if (curPlayerIndex == firstWinningPlayerIndex) turnsSinceWin++;
			playersWon[curPlayerIndex] = true;
			playersWonCounter++;
			curPlayerIndex = (curPlayerIndex + 1) % players.Length;

			if (playersWonCounter >= players.Length)
			{
				// End the game!
				yield break;
			}
		}
		
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
		confirming = false;
	}

	public int GetCurrentPlayerIndex(){
		return curPlayerIndex;
	}

	public bool GetConfirmStatus(){
		return confirming;
	}

	public PlayerInput GetCurrentPlayerInput() {
		return players[curPlayerIndex].GetComponent<PlayerInput>();
	}

	public Rigidbody GetCurrentPlayerRigidbody() {
		return players[curPlayerIndex].GetComponent<Rigidbody>();
	}

	public PlayerPowerUpController GetCurrentPlayerPowerUpController() {
		return players[curPlayerIndex].GetComponent<PlayerPowerUpController>();
	}

	public PlayerPoints GetCurrentPlayerPoints() {
		return players[curPlayerIndex].GetComponent<PlayerPoints>();
	}

	public PlayerControl GetCurrentPlayerControl() {
		return players[curPlayerIndex].GetComponent<PlayerControl>();
	}

	private class PlayerSorter : IComparer  {

		// Calls CaseInsensitiveComparer.Compare on the player name string.
		int IComparer.Compare( System.Object x, System.Object y )  {
			if (x == null) return 1;
			if (y == null) return -1;
			return( (new CaseInsensitiveComparer()).Compare( ((GameObject)x).name, ((GameObject)y).name) );
		}

	}

	private void GetPlayers()
	{
		
		if (players == null || players.Length == 0)
		{
			players = GameObject.FindGameObjectsWithTag("Player");
		}
	}

	public int GetNumPlayers()
	{
		GetPlayers();

		return players.Length;
	}

	public int IndexOfPlayer(GameObject player)
	{
		int playersLength = GetNumPlayers();
		for (int playerIndex = 0; playerIndex < playersLength; playerIndex++)
		{
			if (player.name == players[playerIndex].name)
			{
				return playerIndex;
			}
		}

		return -1;
	}
}
