using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PowerBar : MonoBehaviour {

	public Image content;
	public TurnManager turn;

	// Use this for initialization
	void Start () {
		content.fillAmount = 0;
		if (!turn) {
			turn = (TurnManager)FindObjectOfType(typeof(TurnManager));
		}
	}
	
	// Update is called once per frame
	void Update () {
		content.fillAmount = turn.GetCurrentPlayerInput().curPower / turn.GetCurrentPlayerInput().maxPower;
	}
}
