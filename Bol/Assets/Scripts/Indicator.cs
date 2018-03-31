using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour {

	public GameObject indicatorObj;

	public PlayerInput curInput;

	public float minScale = 0.25f;
	public float maxScale = 1.0f;

	// Use this for initialization
	void Start () {
		if (!curInput) {
			curInput = GetComponent<PlayerInput>();
		}
	}

	Vector3 calculateDirectionVector() {
		float horizontalAngle = curInput.horizontalAngle;
		float verticalAngle = curInput.verticalAngle;

		Quaternion directionVertRot = Quaternion.AngleAxis(verticalAngle, Vector3.left);
		Quaternion directionHorizRot = Quaternion.AngleAxis(horizontalAngle, Vector3.up);
		return directionHorizRot*(directionVertRot*Vector3.forward);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 indicatorScale = indicatorObj.transform.localScale;
		Vector3 newScale = new Vector3(indicatorObj.transform.localScale.x, 
			Mathf.Lerp(minScale, maxScale, curInput.curPower/curInput.maxPower), 
			indicatorObj.transform.localScale.z);
		indicatorObj.transform.localScale = newScale;
		indicatorObj.transform.position = gameObject.transform.position + calculateDirectionVector().normalized*indicatorObj.transform.localScale.y;
		indicatorObj.transform.rotation = Quaternion.LookRotation(calculateDirectionVector()*Mathf.Rad2Deg) * Quaternion.Euler(90.0f, 0.0f, 0.0f);
	}

	public void toggleActive() {
		indicatorObj.SetActive(false);
	}
}
