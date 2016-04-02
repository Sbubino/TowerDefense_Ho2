using UnityEngine;
using System.Collections;

public class Area : Turret {
	public float m_AreaExplosion;


	AreaBullet[] pool;

	void Start()
	{
		pool = new AreaBullet[bulletPool.Length];
		
		for (int i = 0; i< pool.Length; i++) 
		{
			pool[i] = bulletPool[i].GetComponent<AreaBullet>();
			pool[i].SetValue(m_AreaExplosion, m_EnemyLayer);
		}
	}	

	void SetBulletValue(){
		for (int i = 0; i< pool.Length; i++) 
		{
			pool[i].SetValue(m_AreaExplosion, m_EnemyLayer);
		}
	}

}