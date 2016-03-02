using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
   

    Vector3 nucleus;
    Vector3[] positionNextTile;
    RaycastHit2D[] nextTile;
    LayerMask walkableTileMask;


    void Awake()
    {
        walkableTileMask = 1 << 8;
        SetNextTilePosition();


    }

   

    
    void SetNextTilePosition()
    {
        nextTile = Physics2D.BoxCastAll(transform.position, new Vector2(transform.localScale.x, transform.localScale.y), 0f, new Vector2(), 0f, walkableTileMask);

        positionNextTile = new Vector3[nextTile.Length];
        
       

        for (int i =  0; i< nextTile.Length; i++)
        {
            if (nextTile[i].collider.gameObject != this.gameObject)
            {
                if (Vector3.Distance(transform.position, nextTile[i].transform.position) < 1.2)
                {
                    for (int j = 0; j < positionNextTile.Length; j++)
                    {

                        if (positionNextTile[j] == Vector3.zero)
                        {
                            positionNextTile[j] = nextTile[i].transform.position;
                            break;
                        }

                    }
                }
            }
         }
     }



    public void DebugPositionTile()
    {
        for(int i = 0; i< positionNextTile.Length; i++)
        {
            if(positionNextTile[i] != null)
            {
                //Debug.Log("Piastrella n : " + i + "posizione : " + GetPositionNextTile()[i]);
            }
        }
    }

   
    public Vector3[] GetPositionNextTile()
    {
            
        return positionNextTile;
    }



}
