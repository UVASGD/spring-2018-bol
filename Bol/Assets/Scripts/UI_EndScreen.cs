using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EndScreen : MonoBehaviour {
	
	public Text[] PointDisplay;

	private bool scoresLoaded;

	// Use this for initialization
	void Start ()
	{
		scoresLoaded = false;
	}

	// Update is called once per frame
	void Update () {
		if (!scoresLoaded) {
			for (var index = 0; index < PlayerPoints.Points.Length; index++)
			{
				if (index > PointDisplay.Length)
				{
					Debug.LogError("Number of Textboxes in End Screen is not enough! " + 
					               PlayerPoints.Points.Length + " needed, but only " + 
					               PointDisplay.Length + " textboxes available!");
					break;
				}
				StartCoroutine(writeScore(PointDisplay[index], PlayerPoints.Points[index]));
			}
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
			yield return new WaitForSeconds (0.5f);
		}
	}
}
