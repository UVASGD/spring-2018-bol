using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EndScreen : MonoBehaviour {

	public Text playerOne;
	public Text playerTwo;
	public Text playerThree;
	public Text playerFour;

	public int scoreOne;
	public int scoreTwo;
	public int scoreThree;
	public int scoreFour;

	private bool scoresLoaded;

	// Use this for initialization
	void Start () {
		scoreOne = 5;
		scoreTwo = 18;
		scoreThree = 4;
		scoreFour = 12;
		scoresLoaded = false;
	}

	// Update is called once per frame
	void Update () {
		if (!scoresLoaded) {
			StartCoroutine (writeScore (playerOne, scoreOne));
			StartCoroutine (writeScore (playerTwo, scoreTwo));
			StartCoroutine (writeScore (playerThree, scoreThree));
			StartCoroutine (writeScore (playerFour, scoreFour));
			scoresLoaded = true;
		}
	}

	IEnumerator writeScore(Text scoreWrite, int scoreValue){
		while (scoreValue > 0) {
			scoreWrite.text += "\u00F6";
			scoreValue--;
			if (scoreValue == 0) {
				scoreWrite.text += "L";
			}
			yield return new WaitForSeconds (0.125f);
		}
	}
}
