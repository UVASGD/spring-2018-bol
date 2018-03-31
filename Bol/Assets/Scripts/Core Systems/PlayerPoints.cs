using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour {

    int pointTotal;
    bool playerPlaying;


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

    public bool PlayerPlaying
    {
        get
        {
            return playerPlaying;
        }

        set
        {
            playerPlaying = value;
        }
    }


    // Use this for initialization
    void Start () {
        PointTotal = 0;
        PlayerPlaying = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    
}
