//Warning: Not guaranteed to work if multiple players are moving at once
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevelController : MonoBehaviour {

	private Coroutine stayDetect;

	private int[] pointsForWinning = {10, 5, 3, 0};

	public TurnManager turnManager;
	// Use this for initialization
	void Start () {
		

	}

	// Update is called once per frame
	void Update () {



	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			print ("Info: Goal trigger enter");
			stayDetect = StartCoroutine(CheckStay(other));
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			print ("Info: Goal trigger cancel");
			StopCoroutine(stayDetect);
		}
	}
	private IEnumerator CheckStay(Collider other) {
		Debug.Log("Checking if the player stayed");
		yield return new WaitForSeconds(3);
		Debug.Log("The player stayed!");
		PlayerStayed(other);

	}
	private void PlayerStayed(Collider other)
	{
		PlayerPoints pPoints = other.gameObject.GetComponent<PlayerPoints>();
		pPoints.PlayerPlaying = false;
		pPoints.IncrementScore(pointsForWinning[pPoints.TurnManager.NumberOfPlayersWon()]);
		other.gameObject.SetActive(false);
		turnManager.PlayerWon(turnManager.IndexOfPlayer(other.gameObject));
		//Add the points
		//Remove the player object, activate a flag for that player having finished
		//Destroy (this.gameObject);
	}
}
