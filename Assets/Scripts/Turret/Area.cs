using UnityEngine;
using System.Collections;

public class Area : Turret {
	public float m_AreaExplosion;


	AreaBullet[] pool;

    protected override void Awake()
    {
        base.Awake();
        typeName = "Explosive Chili";
        extraInfo = "\n\nBullets explode and hit more enemies";
    }

    protected override void Start()
	{
        base.Start();
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