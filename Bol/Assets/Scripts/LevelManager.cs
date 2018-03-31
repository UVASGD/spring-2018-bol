using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public Button levelOne;

	// Use this for initialization
	void Awake () {
		levelOne.onClick.AddListener (() => GoLevelOne ());
	}

	void GoLevelOne(){
		SceneManager.LoadScene("JaredTesting");
	}

}
