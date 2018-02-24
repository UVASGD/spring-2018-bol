using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Camera eventCamera;
	public Button creditButton;
	public Button mainButton;
	public Button exitButton;

	void Awake()
	{
		creditButton.onClick.AddListener(() => OnClickCredit());
		mainButton.onClick.AddListener (() => OnClickMain ());
		exitButton.onClick.AddListener(() => OnClickExit());
	}

	void OnClickCredit()
	{
		// Seems to not rotate entirely if set at 90 degrees, 0.5s.
		// Works at 180, 0.5s though.
		// May need to change later, but not predicting specific issues. Yet.
		StartCoroutine (rotateCamera (-180.0f, 0.5f));
	}

	void OnClickMain()
	{
		StartCoroutine (rotateCamera (180.0f, 0.5f));
	}

	void OnClickExit(){
		Application.Quit ();
	}

	IEnumerator rotateCamera(float rotationAmt, float time) {
		Quaternion initialRotation = eventCamera.transform.rotation;
		for (float t = 0.0f; t <= time; t += Time.deltaTime) {
			eventCamera.transform.rotation = Quaternion.Lerp (initialRotation, initialRotation * Quaternion.AngleAxis (rotationAmt, Vector3.up), t);
			yield return null;
		}
	}
}