using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ScoreKeeper : MonoBehaviour {

	public Text scoreText;
	public TurnManager turn;

	// Use this for initialization
	void Start () {
		scoreText.text = "0";
		if (!turn) {
			turn = (TurnManager)FindObjectOfType(typeof(TurnManager));
		}
	}

	// Update is called once per frame
	void Update () {
		scoreText.text = turn.GetCurrentPlayerPoints ().PointTotal.ToString();
	}
}
