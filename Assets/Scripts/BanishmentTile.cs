using UnityEngine;
using System.Collections;

public class BanishmentTile : MonoBehaviour {

    public LayerMask m_EnemyMask;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
        }
    }
}
