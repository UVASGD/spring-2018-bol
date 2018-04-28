using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    public int PointTotal;

    public bool PlayerPlaying;

    public static int[] Points;

    public TurnManager TurnManager;

    private int playerIndex;
    
    // Use this for initialization
    void Start () {
        PointTotal = 0;
        PlayerPlaying = true;
        if (TurnManager == null) TurnManager = FindObjectOfType<TurnManager>();
        Points = new int[TurnManager.GetNumPlayers()];
        playerIndex = TurnManager.IndexOfPlayer(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            PointTotal += 1;
            Points[playerIndex] = PointTotal;
            Destroy(other.gameObject);
        }
    }

    public void IncrementScore(int inc)
    {
        PointTotal += inc;
        Points[playerIndex] = PointTotal;
    }

    public static void ResetPoints()
    {
        for (var index = 0; index < Points.Length; index++)
        {
            Points[index] = 0;
        }
    }
}
