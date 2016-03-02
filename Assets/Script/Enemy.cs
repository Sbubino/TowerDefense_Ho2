using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public LayerMask m_Mask;
   
    
    public float Speed;
    public float life;
    public float banishmentCost;

    Vector3[] tilePosition;
    Vector3 nextTile;
    Vector3 currentTile;
    Vector3 lastTile;

    void Awake()
    {
        tilePosition = new Vector3[2];
        nextTile = transform.position;
        
    }


    void Start()
    {
        
        
        StartCoroutine("TakeTilePosition");
        
    }




    void  SetNextTile()
    {
        for(int i = 0; i<tilePosition.Length; i++)
        {
            if(tilePosition[i] != Vector3.zero && tilePosition[i] != lastTile)
            {
                nextTile = tilePosition[i];
            }
                
        }

       
    }

    IEnumerator TakeTilePosition()
    {
        
        while (Vector3.Distance(transform.position, nextTile) >= 0 && Vector3.Distance(transform.position, nextTile) < 0.2f)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(), 0f, m_Mask, 0f, 2f);

            if (hit.collider != null)
            {
               

                Tile temp = hit.transform.gameObject.GetComponent<Tile>();

                tilePosition = temp.GetPositionNextTile();

                lastTile = currentTile;

                currentTile = nextTile;

               

                SetNextTile();

               

              

                Move();

                yield return null;
            }


            while (Vector3.Distance(transform.position, nextTile) >= 0.2f)
            {
            
                Move();
               
                yield return null;
            }

        }

     

        
    }



    void Move()
    {
        

        transform.position =  Vector2.Lerp(transform.position, nextTile, Speed * Time.deltaTime);
    }

    public void DebugPositionTile()
    {
        for (int i = 0; i < tilePosition.Length; i++)
        {
            if (tilePosition[i] != null)
            {
                Debug.Log("Piastrella n : " + i + "  posizione : " + tilePosition[i]);
            }
        }
    }
}
