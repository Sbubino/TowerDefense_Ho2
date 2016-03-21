using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    public LayerMask walkableTileMask;
    public LayerMask m_enemyMask;
    public bool m_Core;
    public bool m_Switch;
    public bool m_Path1;

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


    void Awake()
    {
        if (m_Core)
        {
            distanceToCore = 0;
            nextTile = gameObject;
        }
        walkableTileMask = 1 << 8;

        SetNextTilePosition();
        if (m_Switch)
            sprite = transform.GetChild(0).gameObject;

        checkDistance = false;

    }

    void Start()
    {
        if (m_Core)
            StartPath();

        StartCoroutine("CourSetNext");
        
        //Debug.Log("Tile : " + transform + "Next Tile" + nextTile);
    }

    void Update() {
        //if (m_Switch)
          //  Debug.Log("Switch : " + gameObject.transform + " Path1 " + path1.transform + " Path2 " + path2.transform  );
            
       
      

        
           
        
    }

    void OnMouseDown() {
        Switch();
    }


    void SetNextTilePosition()
    {
        nearTile = Physics2D.BoxCastAll(transform.position, new Vector2(transform.localScale.x + 0.5f, transform.localScale.y + 0.5f), 0f, new Vector2(), 0f, walkableTileMask);

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

    void SetNext() {

        GameObject temp = nextTile;
        for (int i = 0; i < positionNextTile.Length; i++)
        {

            if (temp == null)
            {
                temp = positionNextTile[i];
               // Debug.Log("Tile :  " + transform + " NearTIle : " + positionNextTile[i].transform + "Distance to core : " + positionNextTile[i].GetComponent<Tile>().GetDistanceToCore());
            }
            else if (positionNextTile[i] != null && !positionNextTile[i].CompareTag("Switch"))
            {
                if (positionNextTile[i] != null && positionNextTile[i].GetComponent<Tile>().GetDistanceToCore() < temp.GetComponent<Tile>().GetDistanceToCore())
                   temp = positionNextTile[i];
                 //Debug.Log("Tile :  " + transform + " NearTIle : " + positionNextTile[i].transform + "Distance to core : " + positionNextTile[i].GetComponent<Tile>().GetDistanceToCore());
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

    IEnumerator RevertCheckDistance()
    {
        yield return new WaitForSeconds(0.5f);
        checkDistance = false;
      
    }

    IEnumerator CourSetNext()
    {
        yield return new WaitForSeconds(0.2f);
        SetNext();
    }


    public void SetDistanceCore(int distance)
	{
			distanceToCore = distance + 1;
            checkDistance = true;
             StartCoroutine("RevertCheckDistance");


			for (int i = 0; i < positionNextTile.Length; i++) {
				if (positionNextTile [i] != null) {

					if (positionNextTile [i].CompareTag ("Tile")) {
						if (!positionNextTile [i].GetComponent<Tile> ().CheckDistanceToCore ()) {

							positionNextTile [i].SendMessage ("SetDistanceCore", distanceToCore);
						}
					}else if (positionNextTile [i].CompareTag ("Switch")) {
                    if (!positionNextTile[i].GetComponent<Tile>().CheckDistanceToCore())
                         positionNextTile [i].SendMessage ("SetDistanceCoreSwitch", this.gameObject);

					}
				}

			}
	}



	public void SetDistanceCoreSwitch(GameObject tile)
	{
        
        if (distanceToCore1 == -1) {
			distanceToCore1 = tile.GetComponent<Tile>().GetDistanceToCore() + 1;
			path1 = tile;
		} else {

            checkDistance = true;
            distanceToCore2 = tile.GetComponent<Tile>().GetDistanceToCore() + 1;
			path2 = tile;
		}



        if (path1 != null && path2 != null)
        {
            if (m_Path1)
                distanceToCore = distanceToCore1;
            else
                distanceToCore = distanceToCore2;

            for (int i = 0; i < positionNextTile.Length; i++)
            {
                if (positionNextTile[i].transform.position != path1.transform.position && positionNextTile[i].transform.position != path2.transform.position && positionNextTile[i] != null)
                {

                    StartCoroutine("CorSetNext", positionNextTile[i]);

                    if (m_Path1)
                        positionNextTile[i].SendMessage("SetDistanceCore", distanceToCore1);
                    else
                        positionNextTile[i].SendMessage("SetDistanceCore", distanceToCore2);
                    break;
                }
            }
        }
     
	    RotateSprite ();
	}

  


	void StartPath(){
		for (int i = 0; i < positionNextTile.Length; i++) 
		{
			if(positionNextTile[i] != null){
				positionNextTile[i].SendMessage("SetDistanceCore", distanceToCore);
				break;
			}
		}
		
	}

	void Switch(){
		m_Path1 = !m_Path1;

        if (distanceToCore == distanceToCore1)
            distanceToCore = distanceToCore2;
        else
            distanceToCore = distanceToCore1;


        if (path1 != null && path2 != null)
        {
           

            for (int i = 0; i < positionNextTile.Length; i++)
            {
                if (positionNextTile[i].transform.position != path1.transform.position && positionNextTile[i].transform.position != path2.transform.position && positionNextTile[i] != null)
                {
                   

                    if (m_Path1)
                        positionNextTile[i].SendMessage("SetDistanceCore", distanceToCore1);
                    else
                        positionNextTile[i].SendMessage("SetDistanceCore", distanceToCore2);
                    break;
                }
            }
        }
        
        RotateSprite ();
	}

	public void RotateSprite()
	{
		if (m_Switch) {
			if (m_Path1) {
			
				Vector2 vectorToTarget = path1.transform.position - sprite.transform.position;
				float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
				sprite.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);

			} else {

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







}
