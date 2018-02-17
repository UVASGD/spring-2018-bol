using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	public float horizontal_angle {
		get {
			return horizontal_angle;
		}
		private set {
			horizontal_angle = value;
		}
	}

	public float vertical_angle {
		get {
			return vertical_angle;
		}
		private set {
			vertical_angle = value;
		}
	}

	public float cur_power {
		get {
			return cur_power;
		}
		private set {
			cur_power = value;
		}
	}

	public bool jump {
		get {
			return jump;
		}
		private set {
			jump = value;
		}
	}

	public float axis_deadzone = 0.1f;
	public float power_increase = 0.1f;
	public float max_power = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float horiz = Input.GetAxis("Horizontal");
		float vert = Input.GetAxis("Vertical");
		if (Mathf.Abs(horiz) > axis_deadzone) {
			horizontal_angle += horiz;
		}
		if (Mathf.Abs(vert) > axis_deadzone) {
			vertical_angle += vert;
		}
		if (Input.GetButton("Jump")) {
			cur_power = Mathf.Min((cur_power + (Time.deltaTime * power_increase)), max_power);
		}
		if (Input.GetButtonUp("Jump")) {
			jump = true;
		}
	}

	public bool GetJump() {
		if (jump) {
			jump = false;
			return true;
		}
		return false;
	}
}
