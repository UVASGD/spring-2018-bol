using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour {

    int pointTotal;

    public int PointTotal
    {
        get
        {
            return pointTotal;
        }

        set
        {
            pointTotal = value;
        }
    }

    // Use this for initialization
    void Start () {
        PointTotal = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    
}
