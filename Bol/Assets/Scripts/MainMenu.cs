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
		StartCoroutine (rotateCamera (90.0f, 2.0f));
		eventCamera.transform.Rotate (new Vector3 (0, 90, 0));
	}

	void OnClickMain()
	{
		StartCoroutine (rotateCamera (-90.0f, 2.0f));
		//eventCamera.transform.Rotate (new Vector3 (0, -90, 0));
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