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


    bool inTurretRange;
	float tileWalked = 0f;
	int switchPriority = 0;
    int currentLife;
    Vector3[] possibleTilePosition;
    Vector3 nextTile;
    Vector3 currentTile;
    Vector3 lastTile;
    Vector3 spawn;
	Vector3 lastPosition;
    GameObject lastTurret;



    void Awake()
    {
        possibleTilePosition = new Vector3[2];
        nextTile = transform.position;

		lastPosition = transform.position;        
    }


    void OnEnable()
    {
       StartCoroutine("TakeTilePosition");
		currentLife = m_MaxLife;
        spawn = transform.position;

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
        
        while (Vector3.Distance(transform.position, nextTile) < 0.5)
        {
            RaycastHit2D hit1 = Physics2D.Raycast(transform.position, new Vector2(), 0f, m_SwitchMask, 0f, 2f);

            if (hit1.collider != null)
            {
				switchPriority++;

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

        transform.position =  Vector2.Lerp(transform.position, nextTile, (Speed * Time.deltaTime) / Vector2.Distance(transform.position, nextTile)) ;  
        //transform.Translate(nextTile * Time.deltaTime * Speed);
    }

    public void TakeDamage(int amount)
    {
        currentLife -= amount;
		if(currentLife <= 0){ 
			this.gameObject.SetActive(false);
		}
    }

    public void Banishment()
    {        
        transform.position = spawn;
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

	public void OnTriggerStay2D(Collider2D col){
		if(col.gameObject.tag == "SecondWaveactivator")
			GameController.instance.currentMinionPassed ++;
        if (col.gameObject.tag == "Turret" && lastTurret != col.gameObject)
            tileWalked = 0;


        if (col.gameObject.tag == "Turret")
        {
            
            lastTurret = col.gameObject;
            MovementDetection();
        }



	}

	

	void MovementDetection(){
		//se il nemico è nel range di una torretta aumenta un contatore di distanza percorsa che viene poi resettato all'uscita
			tileWalked += Vector2.Distance (transform.position, lastPosition);
			lastPosition = transform.position;
	
	}



	public int ReturnTileValue(){
		return Mathf.RoundToInt (tileWalked);
	}
	public int ReturnSwitchValue(){
		return switchPriority;
	}
}
