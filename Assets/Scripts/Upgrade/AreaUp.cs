using UnityEngine;
using System.Collections;

public class AreaUp : Upgrade {
    Area turretA;
    public float UpgradeRangeExplosion;

    float rangeExpPerc;


    void Start()
    {
        turretA = GetComponent<Area>();

        rangeExpPerc = UpgradeRangeExplosion / 100;

    }

    public override void LevelUp()
    {
        base.LevelUp();
        turretA.m_AreaExplosion += turretA.m_AreaExplosion * rangeExpPerc;

    }
}
