using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForRaycast : MonoBehaviour {

	public CameraMan manager;

	List<GameObject> lastHit;

	// Use this for initialization
	void Start () {
		if (!manager) manager = GetComponent<CameraMan>();
		lastHit = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = (manager.target.transform.position - manager.transform.position);

		RaycastHit[] hits = Physics.RaycastAll(manager.transform.position, direction, direction.magnitude);

		List<GameObject> newHit = new List<GameObject>();

		foreach (RaycastHit hit in hits) {
			GameObject hitObj = hit.collider.gameObject;

			AlphaOnRaycast alphaOnRaycast = hitObj.GetComponent<AlphaOnRaycast>();

			if (!alphaOnRaycast) continue;

			if (!lastHit.Contains(hitObj)) {
				alphaOnRaycast.OnEnterRaycast();
			} else {
				lastHit.Remove(hitObj);
			}
			newHit.Add(hitObj);
		}

		foreach (GameObject obj in lastHit) {
			AlphaOnRaycast alphaOnRaycast = obj.GetComponent<AlphaOnRaycast>();

			if (!alphaOnRaycast) continue;

			alphaOnRaycast.OnExitRaycast();
		}

		lastHit.Clear();

		lastHit = newHit;
	}
}
