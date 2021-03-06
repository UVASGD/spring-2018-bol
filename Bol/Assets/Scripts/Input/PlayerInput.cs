﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	public float horizontalAngle {
		get; private set;
	}

	public float verticalAngle {
		get; private set;
	}

	public float curPower {
		get; private set;
	}

	public bool jump {
		get; private set;
	}

    public bool powerUp;

	public float axisDeadzone = 0.1f;
	public float powerIncrease = 0.1f;
	public float angleIncrease = 1.0f;
	public float maxPower = 1.0f;

	bool powerIncreasing = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float horiz = Input.GetAxis("Horizontal");
		float vert = Input.GetAxis("Vertical");
		if (Mathf.Abs(horiz) > axisDeadzone) {
			horizontalAngle += horiz * angleIncrease;
		}
		if (Mathf.Abs(vert) > axisDeadzone) {
			verticalAngle += vert * angleIncrease;
			verticalAngle = Mathf.Clamp(verticalAngle, 0.0f, 89.0f);
		}
		if (Input.GetButtonDown("Jump")) {
			curPower = 0.0f;
		}
		if (Input.GetButton("Jump")) {
			if (curPower >= maxPower && powerIncreasing) {
				powerIncreasing = false;
			}
			if (curPower <= 0.0f && !powerIncreasing) {
				powerIncreasing = true;
			}
			curPower += (Time.deltaTime * (powerIncreasing ? powerIncrease : -1 * powerIncrease));
			curPower = Mathf.Clamp(curPower, 0.0f, maxPower);
		}
		if (Input.GetButtonUp("Jump")) {
			jump = true;
		}
        if (Input.GetButtonDown("UsePowerup"))
        {
            powerUp = true;
        }
	}

	public bool GetJump() {
		if (jump) {
			jump = false;
			return true;
		}
		return false;
	}

    public bool GetPowerUp()
    {
        if (powerUp)
        {
            powerUp = false;
            return true;
        }
        return false;
    }
}
