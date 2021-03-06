﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnifiedInput : MonoBehaviour {

	public PlayerInput curInput;
	public PlayerControl curPlayer;
    public PlayerPowerUpController curPowerup;

	// Use this for initialization
	void Start () {
		if (!curInput || !curPlayer || !curPowerup)
		{
			GameObject player = FindObjectOfType<TurnManager>().GetCurrentPlayer();
			curInput = player.GetComponent<PlayerInput>();
			curPlayer = player.GetComponent<PlayerControl>();
			curPowerup = player.GetComponent<PlayerPowerUpController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (curInput.GetJump()) {
			Camera.main.GetComponent<CameraMan>().ballEnterFlight();
			curPlayer.LaunchInDirection(calculateDirectionVector(), curInput.curPower);
			curPlayer.GetComponent<Indicator>().setActive(false);
		}
        if (curInput.GetPowerUp())
        {
            curPowerup.UsePowerUp();
        }
	}

	Vector3 calculateDirectionVector() {
		float horizontalAngle = curInput.horizontalAngle;
		float verticalAngle = curInput.verticalAngle;

		Quaternion directionVertRot = Quaternion.AngleAxis(verticalAngle, Vector3.left);
		Quaternion directionHorizRot = Quaternion.AngleAxis(horizontalAngle, Vector3.up);
		return directionHorizRot*(directionVertRot*Vector3.forward);
	}

	/*void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Vector3 playerPosition = curPlayer.gameObject.transform.position;

		Gizmos.DrawLine(playerPosition, playerPosition + calculateDirectionVector());
		Gizmos.DrawSphere(playerPosition + (calculateDirectionVector() * (curInput.curPower/curInput.maxPower)), 0.1f);
	}*/
}
