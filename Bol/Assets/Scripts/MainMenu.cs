using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Camera eventCamera;

	public Button playButton;
	public Button mainButtonFromPlay;

	public Button helpButton;
	public Button mainButtonFromHelp;

	public Button creditButton;
	public Button mainButtonFromCredit;

	public Button exitButton;

	bool moving = false;

	void Awake()
	{
		playButton.onClick.AddListener (() => OnClickPlay ());
		mainButtonFromPlay.onClick.AddListener (() => OnClickPlayMain ());

		helpButton.onClick.AddListener (() => OnClickHelp ());
		mainButtonFromHelp.onClick.AddListener (() => OnClickHelpMain ());

		creditButton.onClick.AddListener(() => OnClickCredit());
		mainButtonFromCredit.onClick.AddListener (() => OnClickMain ());

		exitButton.onClick.AddListener(() => OnClickExit());
	}

	void OnClickPlay(){
		if (!moving) StartCoroutine (rotateCamera (180.0f, 180.0f, 0.25f));
	}

	void OnClickPlayMain()
	{
		if (!moving) StartCoroutine (rotateCamera (180.0f, 0.0f, 0.25f));
	}

	void OnClickHelp(){
		if (!moving) StartCoroutine (rotateCamera (-90.0f, -90.0f, 0.25f));
	}

	void OnClickHelpMain(){
		if (!moving) StartCoroutine (rotateCamera (90.0f, 0.0f, 0.25f));
	}

	void OnClickCredit()
	{
		if (!moving) StartCoroutine (rotateCamera (90.0f, 90.0f, 0.25f));
	}

	void OnClickMain()
	{
		if (!moving) StartCoroutine (rotateCamera (-90.0f, 0.0f, 0.5f));
	}

	void OnClickExit(){
		Application.Quit ();
	}

	IEnumerator rotateCamera(float rotationAmt, float fixLerp, float time) {
		moving = true;
		Quaternion initialRotation = eventCamera.transform.rotation;
		for (float t = 0.0f; t <= time; t += Time.deltaTime) {
			eventCamera.transform.rotation = Quaternion.Lerp (initialRotation, initialRotation * Quaternion.AngleAxis (rotationAmt, Vector3.up), t/time);
			yield return null;
		}
		eventCamera.transform.rotation = Quaternion.AngleAxis (fixLerp, Vector3.up);
		moving = false;
	}
}