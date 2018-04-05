using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaOnRaycast : MonoBehaviour {

	public Renderer alpha_renderer;

	public float raycastedAlpha = 0.5f;
	public float fullAlpha = 1.0f;

	public bool inRaycast = false;

	// Use this for initialization
	void Start () {
		if (!alpha_renderer) alpha_renderer = GetComponent<Renderer>();
	}

	public void OnEnterRaycast() {
		Color oldColor = alpha_renderer.material.color;

		alpha_renderer.material.color = new Color(oldColor.r, oldColor.g, oldColor.b, raycastedAlpha);

		Debug.Log(gameObject.name + " entering Raycast");
		inRaycast = true;
	}

	public void OnExitRaycast() {
		Color oldColor = alpha_renderer.material.color;

		alpha_renderer.material.color = new Color(oldColor.r, oldColor.g, oldColor.b, fullAlpha);
		Debug.Log(gameObject.name + " leaving Raycast");
		inRaycast = false;
	}
}
