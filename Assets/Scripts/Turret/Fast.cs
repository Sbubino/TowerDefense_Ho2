using UnityEngine;
using System.Collections;

public class Fast : Turret{

    FastBullet[] pool;


    protected override void Awake()
    {
        base.Awake();
        typeName = "Pancake gatling";


    }

    //void Start()
    //{
    //    pool = new FastBullet[bulletPool.Length];

    //    for (int i = 0; i < pool.Length; i++)
    //    {
    //        pool[i] = bulletPool[i].GetComponent<FastBullet>();

    //    }

    //}


    //public override string TurretDialogo()
    //{
    //    return "Damage: " + m_Damage + "\n" +"FireRate:"+ m_FireRate;
    //}
}
