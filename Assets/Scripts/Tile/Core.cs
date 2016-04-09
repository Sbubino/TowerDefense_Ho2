using UnityEngine;
using System.Collections;

public class Core : Tile {

    DialogoController dialogo;


    public override void Awake()
    {
        distanceToCore = 0;
        nextTile = gameObject;
        base.Awake();
        dialogo = FindObjectOfType<DialogoController>();

    }

    void Start()
    {       
        StartPath();      
    }

    void Update()
    {
       //if(Input.GetMouseButtonDown(0)) ClickSelect();
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


    //GameObject ClickSelect()
    //{
    //    //Converting Mouse Pos to 2D (vector2) World Pos
    //    Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    //    RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

    //    if (hit)
    //    {
    //        Debug.Log(hit.transform.name);
    //        return hit.transform.gameObject;
    //    }
    //    else return null;
    //}


}
