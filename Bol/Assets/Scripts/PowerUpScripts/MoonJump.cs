﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonJump : PowerUp 
{
    // Use this for initialization
    void Start() {
        Debug.Log("MoonJump activated");
    }

    public MoonJump(GameObject player) : base(player){

    }

    // Update is called once per frame
    void Update() { }

    public override void PowerUpEffect()
    {
        Debug.Log("MoonJump PowerUpEffect activated");
        Rigidbody playerAtt = player.GetComponent<Rigidbody>();
        Debug.Log(playerAtt.velocity);

        //x and z velocity stay the same, y velocity is incremented
        //not really sure what kind of units these are so idk how much to increment
        Vector3 newVelocity = playerAtt.velocity;
        newVelocity.y += 10;
        playerAtt.velocity = newVelocity;
        Debug.Log(playerAtt.velocity);

        //need to figure out how to change gravity on this one object...
        //maybe apply upward force or increase drag
    }
}
