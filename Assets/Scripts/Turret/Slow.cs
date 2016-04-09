using UnityEngine;
using System.Collections;

public class Slow : Turret {
	public float m_SlowAmount;
	public float m_SlowTime;
	SlowBullet[] pool;

    protected override void Awake()
    {
        base.Awake();
        typeName = "Freezing icecream";
        extraInfo = "\n\nSlows down enemies when hit";

    }

    void Start()
	{
		pool = new SlowBullet[bulletPool.Length];
		
		for (int i = 0; i< pool.Length; i++) 
		{
			pool[i] = bulletPool[i].GetComponent<SlowBullet>();
			pool[i].SetValueSlow(m_SlowAmount, m_SlowTime);
		}
		
	}	

	void SetBulletValue(){
		for (int i = 0; i< pool.Length; i++) 
		{
			pool[i].SetValueSlow(m_SlowAmount, m_SlowTime);
		}
	}


}
