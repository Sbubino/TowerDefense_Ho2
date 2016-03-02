using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public LayerMask m_TileMask;
    public LayerMask m_SwitchMask;


    public float Speed;
    public float life;
    public float banishmentCost;

    Vector3[] possibleTilePosition;
    Vector3 nextTile;
    Vector3 currentTile;
    Vector3 lastTile;

    void Awake()
    {
        possibleTilePosition = new Vector3[2];
        nextTile = transform.position;
        
    }


    void Start()
    {
        
        
        StartCoroutine("TakeTilePosition");
        
    }




    void  SetNextTile()
    {
        for(int i = 0; i<possibleTilePosition.Length; i++)
        {

            if (possibleTilePosition[i] != Vector3.zero && possibleTilePosition[i] != lastTile)
            {
                nextTile = possibleTilePosition[i];
            }
                
        }

       
    }

    IEnumerator TakeTilePosition()
    {
        
        while (Vector3.Distance(transform.position, nextTile) >= -0.5f && Vector3.Distance(transform.position, nextTile) < 0.2f)
        {
            RaycastHit2D hit1 = Physics2D.Raycast(transform.position, new Vector2(), 0f, m_SwitchMask, 0f, 2f);

            if (hit1.collider != null)
            {
                nextTile = hit1.collider.GetComponent<Switch>().GetNextTilePosition();


            }







            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(), 0f, m_TileMask, 0f, 2f);


            if (hit.collider != null && hit1.collider == null)
            {
               

                Tile temp = hit.transform.gameObject.GetComponent<Tile>();

                possibleTilePosition = temp.GetPositionNextTile();

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
        
        // transform.Translate(Vector3.zero);
        transform.position =  Vector2.Lerp(transform.position, nextTile, Speed * Time.deltaTime);  
        //transform.Translate(nextTile * Time.deltaTime * Speed);
    }

   
}
