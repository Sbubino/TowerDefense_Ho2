using UnityEngine;
using System.Collections;

public class Core : Tile {

    public override void Awake()
    {
        distanceToCore = 0;
        nextTile = gameObject;
        base.Awake();
    }

    void Start()
    {       
        StartPath();      
    }

    void StartPath()
    {
        checkDistance = true;
        for (int i = 0; i < positionNextTile.Length; i++)
        {
            if (positionNextTile[i] != null)
            {
                positionNextTile[i].SendMessage("SetDistanceCore", distanceToCore);
            }
        }

    }
}
