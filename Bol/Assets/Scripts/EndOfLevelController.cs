using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		

	}

	// Update is called once per frame
	void Update () {



	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Player")
		{
			print ("Info: End of level trigger");
			//implement level transition here
			//Destroy (this.gameObject);
		}
	}
}
