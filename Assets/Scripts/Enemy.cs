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
    RaycastHit2D[] possibleTile;
    Vector2 nextTile;
    GameObject currentTile;
    Vector3 spawn;
    GameObject lastTurret;
    float timer;


    void Awake()
    {
        nextTile = Vector2.zero;



    }

    void Update()
    {
        timer += Time.deltaTime;
        
        SetCurrentTiles();
        Move();
       //   Debug.Log(currentTile.GetComponent <Tile>().GetDistanceToCore());
        //Debug.Log(nextTile);
    }



    void OnEnable()
    {

        currentLife = m_MaxLife;
        spawn = transform.position;

    }

    //Setta i parametri possible time, next tile e current tile
    void SetCurrentTiles()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 5, m_TileMask);
        if (hit.collider != null)
        {
			if(hit.collider.CompareTag("Core")){
				GameController.instance.LoseEnergy(banishmentCost);
				transform.position = spawn;
				Debug.Log ("Fanculo");
			}

			else if (!hit.collider.CompareTag("Switch"))
            {
                currentTile = hit.collider.gameObject;
                nextTile = currentTile.GetComponent<Tile>().GetNextTile().transform.position;
            }
			else
            {
                currentTile = hit.collider.gameObject;
                nextTile = currentTile.GetComponent<Tile>().GetPositionNextTileSwitch().transform.position;
            }
        }
	

    }


        


    

    

    void SetNextTile(GameObject tile)
    {
        nextTile = tile.transform.position;
        Move();

    }


    void Move()
    {

        // transform.Translate(Vector3.zero);
        if (nextTile != Vector2.zero)
        transform.position =  Vector2.Lerp(transform.position, nextTile,(Speed * Time.deltaTime) / Vector2.Distance(nextTile, transform.position)) ;  
        //transform.Translate(nextTile * Time.deltaTime * Speed);
    }

    public int DistToCore()
    {
        return currentTile.GetComponent<Tile>().GetDistanceToCore();
    }

    public void TakeDamage(int amount)
    {
        currentLife -= amount;
		if(currentLife <= 0){ 
			this.gameObject.SetActive(false);
			GameController.instance.TakeEnergy(AddEnergy);
		}
    }

    public void Banishment()
    {        
        transform.position = spawn;
        nextTile = spawn;
//        GameController.instance.LoseEnergy(banishmentCost);
        
    }

    void Slow(float[] slow){
		Debug.Log ("kesooo");
		StartCoroutine ("GetSlow", slow);

	}

	IEnumerator GetSlow(float[] slow)
    {
		Speed /= slow [0];
		yield return new WaitForSeconds (slow [1]);
		Speed *= slow [0];
    }

    void OnDisable()
    {
   //     GameController.instance.TakeEnergy(AddEnergy);
    }


		





	public int ReturnTileValue(){
		return Mathf.RoundToInt (tileWalked);
	}
	public int ReturnSwitchValue(){
		return switchPriority;
	}
}
