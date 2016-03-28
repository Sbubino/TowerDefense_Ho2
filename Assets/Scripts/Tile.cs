using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    public LayerMask walkableTileMask;
    public LayerMask m_enemyMask;
    public bool m_Core;
    public bool m_Switch;
    public bool m_Path1;
	public int switchEnergyCost;

    GameObject[] positionNextTile;
    RaycastHit2D[] nearTile;
    GameObject nextTile;

    public bool checkDistance;
    int distanceToCore = -1;

    int distanceToCore1 = -1;
    int distanceToCore2 = -1;
    GameObject path1;
    GameObject path2;

    GameObject sprite;
    float timer;
	BoxCollider2D col;
	float timerSwitch;

    bool lastTile;


    void Awake()
    {
        

		if (m_Core)
        {
            distanceToCore = 0;
            nextTile = gameObject;
        }
       

        SetNextTilePosition();
        if (m_Switch) {
			sprite = transform.GetChild (0).gameObject;
			col = gameObject.GetComponent<BoxCollider2D>();
		}

        checkDistance = false;
		timerSwitch = 0.5f;

    }

    void Start()
    {
        if (m_Core)
            StartPath();
		if (m_Switch)
			col.isTrigger = false;

       // StartCoroutine("CourSetNext");
        
        //Debug.Log("Tile : " + transform + "Next Tile" + nextTile);
    }

    void Update() {
       // if (m_Switch)
         //   Debug.Log("Switch : " + checkDistance  );
		//if (transform.name.Equals ("Switch") )
			//Debug.Log (path1 + "  " + path2);
		timerSwitch += Time.deltaTime;
       // if (transform.name.Equals("Tile (5)") || transform.name.Equals("Tile (88)") || transform.name.Equals("Tile (72)"))
         //   Debug.Log(transform + " " + distanceToCore );




    }

    


    void SetNextTilePosition()
    {
        nearTile = Physics2D.BoxCastAll(transform.position, new Vector2(transform.localScale.x + 0.5f, transform.localScale.y + 0.5f), 0f, Vector3.zero, 0f, walkableTileMask);

        positionNextTile = new GameObject[nearTile.Length];

		//if(transform.name.Equals ("Tile (81)"))
		//	Debug.Log(nearTile.Length);	


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

    void SetNext() {

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

    public GameObject GetNextTile()
    {
        return nextTile;
    }


	public int GetDistanceToCore(){
		return distanceToCore;

	}

	public GameObject[] GetPositionNextTile(){
			return positionNextTile;
	}

	public GameObject GetPositionNextTileSwitch(){

			if (m_Path1) 
				return path1;
			else
				return path2;
	}

	public bool CheckDistanceToCore()
	{
        return checkDistance;
	}


    public void DebugPositionTile()
    {
        
    }

    void RevertCheck()
    {
        if (!m_Core) {

            checkDistance = false;
            if(nextTile == null)
                SetNext();

            nextTile.SendMessage("RevertCheck");

           
        }
    }

    IEnumerator RevertCheckDistance()
    {
        yield return new WaitForEndOfFrame();
        checkDistance = false;
        nextTile.SendMessage("RevertCheck");
      
    }

    IEnumerator CourSetNext()
    {
       yield return new WaitForSeconds(0.2f);
        //SetNext();
    }


    public void SetDistanceCore(int distance)
	{
			distanceToCore = distance + 1;
           
            checkDistance = true;
            lastTile = false;
        //StartCoroutine("RevertCheckDistance");
       
        for (int i = 0; i < positionNextTile.Length; i++) {
				
				if (positionNextTile [i] != null) {
					
					if (positionNextTile [i].CompareTag ("Tile")) {
						if (!positionNextTile [i].GetComponent<Tile> ().CheckDistanceToCore ()) {
                        
                            lastTile = true;

                            positionNextTile [i].SendMessage ("SetDistanceCore", distanceToCore);
						}

					}else if (positionNextTile [i].CompareTag ("Switch")) {
                     if (!positionNextTile[i].GetComponent<Tile>().CheckDistanceToCore())

                        positionNextTile [i].SendMessage ("SetDistanceCoreSwitch", this.gameObject);
                    
					}
				}

			}

        if (!lastTile)
        {
            
            SetNext();
           // Debug.Log("lasTile " + transform + " next  " + nextTile );
            StartCoroutine("RevertCheckDistance");
        }

	}




	public IEnumerator SetDistanceCoreSwitch(GameObject tile)
	{
        yield return new WaitForEndOfFrame();
          if (distanceToCore1 == -1) {

			distanceToCore1 = tile.GetComponent<Tile>().GetDistanceToCore() + 1;
           
			path1 = tile;
		} else if(distanceToCore2 == -1) {

          
            distanceToCore2 = tile.GetComponent<Tile>().GetDistanceToCore() + 1;
           
			path2 = tile;
		}

        if (path1 != null && path2 != null)
        {
           

            if (m_Path1)
            {
                distanceToCore = distanceToCore1;
                nextTile = path1;
            }
            else {
                distanceToCore = distanceToCore2;
                nextTile = path2;
            }

           

            for (int i = 0; i < positionNextTile.Length; i++)
            {
                if (positionNextTile[i] != null && !positionNextTile[i].transform.name.Equals(path1.transform.name) && !positionNextTile[i].transform.name.Equals(path2.transform.name))
                {                   
                    checkDistance = true;
                    Debug.Log("tile problema " + positionNextTile[i] + "Path1 : " + path1 + "Path 2 : " + path2);

                    positionNextTile[i].SendMessage("SetDistanceCore", distanceToCore);
                }
                    
            }

        }
            RotateSprite();
	}

  


	void StartPath(){
		for (int i = 0; i < positionNextTile.Length; i++) 
		{
			if(positionNextTile[i] != null){
				positionNextTile[i].SendMessage("SetDistanceCore", distanceToCore);
				
			}
		}
		
	}

	public void Switch(){
		if (timerSwitch >= 0.5f) {
			GameController.instance.LoseEnergy (switchEnergyCost);
			m_Path1 = !m_Path1;

            if (m_Path1)
            {
                distanceToCore = distanceToCore1;
                nextTile = path1;
            }
            else {
                distanceToCore = distanceToCore2;
                nextTile = path2;
            }



            for (int i = 0; i < positionNextTile.Length; i++)
            {
                if (positionNextTile[i] != null && positionNextTile[i].transform.position != path1.transform.position && positionNextTile[i].transform.position != path2.transform.position)
                {
                    checkDistance = true;
                   
                        Debug.Log("tile problema Switch " + positionNextTile[i]);
                    positionNextTile[i].SendMessage("SetDistanceCore", distanceToCore);
                }

            }

            RotateSprite ();
			timerSwitch = 0;
		}
	}

	public void RotateSprite()
	{
		if (m_Switch) {
			if (m_Path1 && path1 != null) {
			
				Vector2 vectorToTarget = path1.transform.position - sprite.transform.position;
				float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
				sprite.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);

			} else if (path2 != null){

				Vector2 vectorToTarget = path2.transform.position - sprite.transform.position;
				float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
				sprite.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			
			}
		}
		
		
	}

    IEnumerator CorSetNext(GameObject tile)
    {
        yield return new WaitForSeconds(0.5f);

        tile.SendMessage("SetNextSwitch", gameObject);
    }

    void SetNextSwitch(GameObject next)
    {
        nextTile = next;
     }

	void QueryNextTile(GameObject target){
		if (m_Switch) {
			if (m_Path1) {
				Debug.Log(path1);
				target.SendMessage( "SetNextTile",path1);
			}
			else{
				target.SendMessage( "SetNextTile",path2);
			}


		}else if(nextTile != null)
			target.SendMessage ("SetNextTile", nextTile);

	}







}
