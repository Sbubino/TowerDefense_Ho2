using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public LayerMask m_TileMask;
    public LayerMask m_SwitchMask;
	public int m_MaxLife;
    public int banishmentCost;
    public float Speed;
    public float distance = 1.8f;
    public int AddEnergy;
    int currentLife;
    Vector3[] possibleTilePosition;
    Vector3 nextTile;
    Vector3 currentTile;
    Vector3 lastTile;
    Vector3 spawn;


    void Awake()
    {
        possibleTilePosition = new Vector3[2];
        nextTile = transform.position;

        
    }


    void OnEnable()
    {
       StartCoroutine("TakeTilePosition");
		currentLife = m_MaxLife;
        spawn = transform.position;
        Debug.Log(spawn);
    }




    void  SetNextTile()
    {

        for (int i = 0; i<possibleTilePosition.Length; i++)
        {

            if (possibleTilePosition[i] != Vector3.zero && possibleTilePosition[i] != lastTile)
            {
                nextTile = possibleTilePosition[i];
                break;
            }
                
        }

       
    }

    IEnumerator TakeTilePosition()
    {
        
        while (Vector3.Distance(transform.position, nextTile) < 1)
        {
            RaycastHit2D hit1 = Physics2D.Raycast(transform.position, new Vector2(), 0f, m_SwitchMask, 0f, 2f);

            if (hit1.collider != null)
            {
                

                currentTile = hit1.transform.position;

                nextTile = hit1.collider.GetComponent<Switch>().GetNextTilePosition();

            }


            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(), 0f, m_TileMask, 0f, 2f);


            if (hit.collider != null && hit1.collider == null)
            {
               

				Move();

                Tile temp = hit.transform.gameObject.GetComponent<Tile>();

                possibleTilePosition = temp.GetPositionNextTile();

                lastTile = currentTile;

                currentTile = nextTile;
             
                SetNextTile();
  
                

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
        transform.position =  Vector2.Lerp(transform.position, nextTile, (Speed * Time.fixedDeltaTime) / Vector2.Distance(transform.position, nextTile)) ;  
        //transform.Translate(nextTile * Time.deltaTime * Speed);
    }

    public void TakeDamage(int amount)
    {
        currentLife -= amount;
    }

    public void Banishment()
    {
        
        transform.position = spawn;
        Debug.Log("Jesoos is great");
        nextTile = spawn;
//        GameController.instance.LoseEnergy(banishmentCost);
        
    }

    public void GetSlow(float slow)
    {
        Speed /= slow;
    }

    void OnDisable()
    {
   //     GameController.instance.TakeEnergy(AddEnergy);
    }
}
