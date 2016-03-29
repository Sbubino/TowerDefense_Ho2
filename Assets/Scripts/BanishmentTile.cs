using UnityEngine;
using System.Collections;

public class BanishmentTile : MonoBehaviour {

    public LayerMask m_EnemyMask;
	public bool isCore;
	public int life;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (life <= 0 && isCore == false) {
			gameObject.SetActive(false);
		}

	}
    void FixedUpdate()
    {
        IsWalkingEnemy();
    }
    
    void IsWalkingEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(), 0f, m_EnemyMask, 0f, 2f);
        if(hit.collider != null)
        {
            hit.collider.GetComponent<Enemy>().Banishment();
			Debug.Log ("super Jesoo è immenso");
        }
    }
}
