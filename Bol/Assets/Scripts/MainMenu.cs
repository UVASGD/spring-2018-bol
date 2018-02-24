using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Button creditButton;
	public Button exitButton;

	void Awake()
	{
		creditButton.onClick.AddListener(() => OnClickCredit());
		exitButton.onClick.AddListener(() => OnClickExit());
	}

	void OnClickCredit()
	{
		SceneManager.LoadScene("CreditsMenu");
	}

	void OnClickExit(){
		Application.Quit ();
	}
}