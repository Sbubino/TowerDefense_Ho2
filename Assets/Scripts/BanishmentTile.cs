using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BanishmentTile : MonoBehaviour {
                    // numero max nemici banishabili, costo energia, posizionamento su piastrelle normali.
    public LayerMask m_EnemyMask;
    public bool IsCore;
 //   public LayerMask WalkableTile;
    public int Cost;
    public int life;
    public float speed;
    public Button banishmentTileButton;


    void Awake()
    {
    }
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}
    void FixedUpdate()
    {
        if(IsCore == false && life <= 0)
        {
            Destroy(this);
        }
        IsWalkingEnemy();
    }
    
    void IsWalkingEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(), 0f, m_EnemyMask, 0f, 2f);
        if(hit.collider != null)
        {
            hit.collider.GetComponent<Enemy>().Banishment();
            if(IsCore == false)
            {
                life -= 1;
            }
        }
    }


    void OnMouseDown()
    {
        Debug.Log("Jesooo è grande");
        StartCoroutine("ActivateInterface");
    }

    IEnumerator ActivateInterface()
    {
        int deltaPosition = 2;
        bool onPosition = false;
        Vector3 destination;

        while (!onPosition)
        {

          
                banishmentTileButton.gameObject.SetActive(true);
                destination = new Vector3(transform.position.x , transform.position.y + deltaPosition, 0);
                banishmentTileButton.gameObject.transform.position = Vector3.Lerp(banishmentTileButton.gameObject.transform.position, destination, speed / Vector3.Distance(banishmentTileButton.transform.position, destination));
                deltaPosition *= -1;
            if (Vector3.Distance(banishmentTileButton.transform.position, destination) < 0.5f) 
            {
                onPosition = true;
            }

            yield return null;
        }
        }
    
    }
