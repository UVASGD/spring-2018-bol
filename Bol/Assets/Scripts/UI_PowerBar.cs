﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PowerBar : MonoBehaviour {

	public Image content;
	public PlayerInput powVal;

	// Use this for initialization
	void Start () {
		content.fillAmount = 0;
		//if (!powVal) {
		//	powVal = GetComponent<PlayerInput>();
		//}
	}
	
	// Update is called once per frame
	void Update () {
		//print (powVal.horizontalAngle);
		content.fillAmount = Random.value;
	}
}
