//Warning: Not guaranteed to work if multiple players are moving at once
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevelController : MonoBehaviour {

	private Coroutine stayDetect;

	private int[] pointsForWinning = {5, 3, 1, 0};

	public TurnManager turnManager;
	// Use this for initialization
	void Start () {
        if(turnManager == null) turnManager = FindObjectOfType<TurnManager>();

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
		pPoints.playerPlaying = false;
		pPoints.IncrementScore(pointsForWinning[pPoints.turnManager.NumberOfPlayersWon()]);
		other.gameObject.SetActive(false);
		turnManager.PlayerWon(turnManager.IndexOfPlayer(other.gameObject));
		//Add the points
		//Remove the player object, activate a flag for that player having finished
		//Destroy (this.gameObject);
	}
}
