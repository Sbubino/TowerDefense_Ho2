﻿using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	public float CostBuild;
	public float CostUpgrade;
	public float m_Damage;
	public float m_FireRate;
	public LayerMask m_EnemyLayer;
	public GameObject m_BulletPrefab;
    public float m_Range;

    [HideInInspector]
    public bool canShoot = true;

    //Variabili TorreInterne
    float timer;
    CircleCollider2D range;

	//Variabili per SetTArget()
	Collider2D[] enemyInRange;
	GameObject target;
	

	//Variabili per Shoot();
	[HideInInspector]
	public GameObject[] bulletPool;
	int bulletPoolIndex;


	void Awake()
	{
		enemyInRange = new Collider2D[20];
        range = GetComponent<CircleCollider2D>();
        range.radius = m_Range;
		bulletPool = new GameObject[10];

		target = null;

		for(int i = 0; i< bulletPool.Length; i++)
		{
			bulletPool[i] = Instantiate(m_BulletPrefab, Vector3.zero, m_BulletPrefab.transform.rotation) as GameObject;
			bulletPool[i].SendMessage("SetDamage", m_Damage);
			bulletPool[i].SetActive(false);
		}
		bulletPoolIndex = 0;
	}

	void Update(){
		timer += Time.deltaTime;
	}


	void OnTriggerStay2D(Collider2D trig)
	{
		if (trig.gameObject.tag == "Enemy")
		{
			SetTarget();

            if (timer >= m_FireRate)
            {
                if(canShoot)
                    Shoot();
            }
		}		
	}

	void OnTriggerExit2D(Collider2D trig)
	{
		if (trig.CompareTag ("Enemy")) {
			target = null;
			CancelEnemyArray(trig);
			SetTarget ();
		}
		
	}

	void SetTarget()
	{
		Physics2D.OverlapCircleNonAlloc(transform.position, range.radius - 0.5f, enemyInRange,m_EnemyLayer);
		
		for(int i = 0; i < enemyInRange.Length; i++)
		{
			if (enemyInRange[i] != null)
			{
				if (target == null || !target.activeInHierarchy)
				
					target = enemyInRange[i].gameObject;

				else if (enemyInRange[i].gameObject.GetComponent<Enemy>().DistToCore() < target.GetComponent<Enemy>().DistToCore())
				
					target = enemyInRange[i].gameObject;

			}
		}		
	}


	void CancelEnemyArray(Collider2D target)
	{
		for (int i = 0; i < enemyInRange.Length; i++) 
		{

			if(enemyInRange[i] != null)
			{	

				if( enemyInRange[i].Equals(target))

					enemyInRange[i] = null;
			}			
		}
	}


	void Shoot()
	{
		if (target != null && target.activeInHierarchy) 
		{
			
			if (bulletPoolIndex < bulletPool.Length - 1) 
			{

				if (!bulletPool [bulletPoolIndex].activeInHierarchy) 
				{
					bulletPool [bulletPoolIndex].transform.position = transform.position;
					bulletPool [bulletPoolIndex].SetActive (true);
					bulletPool [bulletPoolIndex].SendMessage ("SetTarget", target);
					timer = 0;
					
				} 
				else

					bulletPoolIndex++;
			} 
			else

				bulletPoolIndex = 0;
		} 
		else if (target != null) 
		{
			CancelEnemyArray(target.GetComponent<Collider2D>());
			SetTarget ();
		}
	}

    public void setRange(float ran)
    {
        range.radius = ran;
    }
}