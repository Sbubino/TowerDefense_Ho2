using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    public LayerMask walkableTileMask;


    [HideInInspector]
    public bool checkDistance;
    //Variabili per SetNextTile
    [HideInInspector]
    public GameObject[] positionNextTile;
    [HideInInspector]
    public RaycastHit2D[] nearTile;
    [HideInInspector]
    public GameObject nextTile;
    
    public int distanceToCore = -1;
    [HideInInspector]
    public bool lastTile;




    public virtual void Awake()
    {
        SetNextTilePosition();     
        checkDistance = false;
    }


   public void SetNextTilePosition()
    {
        nearTile = Physics2D.BoxCastAll(transform.position, new Vector2(transform.localScale.x + 0.5f, transform.localScale.y + 0.5f), 0f, Vector3.zero, 0f, walkableTileMask);

        positionNextTile = new GameObject[nearTile.Length];

        for (int i = 0; i < nearTile.Length; i++)
        {
            if (nearTile[i].collider.gameObject != this.gameObject)
            {
                if (Vector3.Distance(transform.position, nearTile[i].transform.position) < 1.2f)
                {
                    for (int j = 0; j < positionNextTile.Length; j++)
                    {

                        if (positionNextTile[j] == null)
                        {

                            positionNextTile[j] = nearTile[i].transform.gameObject;
                            break;
                        }

                    }
                }
            }
        }
    }


   public void SetNext()
    {
        GameObject temp = nextTile;
        for (int i = 0; i < positionNextTile.Length; i++)
        {
            if (temp == null && !positionNextTile[i].gameObject.CompareTag("Switch"))
            {
                temp = positionNextTile[i];
                // Debug.Log("Tile :  " + transform + " NearTIle : " + positionNextTile[i].transform + "Distance to core : " + positionNextTile[i].GetComponent<Tile>().GetDistanceToCore());
            }
            else if (positionNextTile[i] != null && !positionNextTile[i].gameObject.CompareTag("Switch"))
            {
                if (positionNextTile[i] != null && positionNextTile[i].GetComponent<Tile>().GetDistanceToCore() < temp.GetComponent<Tile>().GetDistanceToCore())
                    temp = positionNextTile[i];


                //Debug.Log("Tile :  " + transform + " NearTIle : " + positionNextTile[i].transform + "Distance to core : " + positionNextTile[i].GetComponent<Tile>().GetDistanceToCore());
            }
            else if (positionNextTile[i] != null && positionNextTile[i].GetComponent<Tile>().checkDistance)
            {
                temp = positionNextTile[i];
            }
        }
        nextTile = temp;
        //  Debug.Log("Tile :  " + transform + " NextTile : " + nextTile);
    }


    void SetDistanceCore(int distance)
    {
        distanceToCore = distance + 1;

        checkDistance = true;
        lastTile = false;
        //StartCoroutine("RevertCheckDistance");

        for (int i = 0; i < positionNextTile.Length; i++)
        {
            if (positionNextTile[i] != null)
            {

                if (positionNextTile[i].CompareTag("Tile"))
                {
                    if (!positionNextTile[i].GetComponent<Tile>().CheckDistanceToCore())
                    {
                        lastTile = true;
                        positionNextTile[i].SendMessage("SetDistanceCore", distanceToCore);
                    }

                }
                else if (positionNextTile[i].CompareTag("Switch"))
                {

                    if (!positionNextTile[i].GetComponent<Tile>().CheckDistanceToCore())
                        
                        positionNextTile[i].SendMessage("SetDistanceCoreSwitch", this.gameObject);
                }
            }
        }

        if (!lastTile)
        {
            SetNext();
           //  Debug.Log("lasTile " + transform + " next  " + nextTile );
            StartCoroutine("RevertCheckDistance");
        }
    }


    public int GetDistanceToCore()
    {
        return distanceToCore;

    }

    public GameObject GetNextTile()
    {
        return nextTile;
    }

    public bool CheckDistanceToCore()
    {
        return checkDistance;
    }


    void RevertCheck()
    {
            checkDistance = false;
            if (nextTile == null)
                SetNext();
            if(!nextTile.CompareTag("Core"))
                 nextTile.SendMessage("RevertCheck");      
    }

    IEnumerator RevertCheckDistance()
    {
        yield return new WaitForEndOfFrame();
        checkDistance = false;
        nextTile.SendMessage("RevertCheck");
    }


    public virtual void QueryNextTile(GameObject target)
    {
       if (nextTile != null)
            target.SendMessage("SetNextTile", nextTile);
    }
}
