//Warning: Not guaranteed to work if multiple players are moving at once
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevelController : MonoBehaviour {

	private Coroutine stayDetect;
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
		if (!pPoints.TurnManager.PlayerHasWon())
		{
			pPoints.IncrementScore(5);
		}
		pPoints.IncrementScore(5);
		other.gameObject.SetActive(false);
		//Add the points
		//Remove the player object, activate a flag for that player having finished
		//Destroy (this.gameObject);
	}
}
