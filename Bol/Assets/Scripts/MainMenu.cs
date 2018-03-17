using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Camera eventCamera;

	public Button playButton;

	public Button helpButton;
	public Button mainButtonFromHelp;

	public Button creditButton;
	public Button mainButtonFromCredit;

	public Button exitButton;

	bool moving = false;

	void Awake()
	{
		helpButton.onClick.AddListener (() => OnClickMain ());
		mainButtonFromHelp.onClick.AddListener (() => OnClickCredit ());

		creditButton.onClick.AddListener(() => OnClickCredit());
		mainButtonFromCredit.onClick.AddListener (() => OnClickMain ());

		exitButton.onClick.AddListener(() => OnClickExit());
	}

	void OnClickCredit()
	{
		if (!moving) StartCoroutine (rotateCamera (90.0f, 0.25f));
	}

	void OnClickMain()
	{
		if (!moving) StartCoroutine (rotateCamera (-90.0f, 0.25f));
	}

	void OnClickExit(){
		Application.Quit ();
	}

	IEnumerator rotateCamera(float rotationAmt, float time) {
		moving = true;
		Quaternion initialRotation = eventCamera.transform.rotation;
		for (float t = 0.0f; t <= time; t += Time.deltaTime) {
			eventCamera.transform.rotation = Quaternion.Lerp (initialRotation, initialRotation * Quaternion.AngleAxis (rotationAmt, Vector3.up), t/time);
			yield return null;
		}
		moving = false;
	}
}