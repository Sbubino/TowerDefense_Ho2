using UnityEngine;
using System.Collections;

public class SlowUp : Upgrade {
    Slow turretS;
    public int UgradeSlowAmount;
    public float UpgradeSlowTIme;

    float slowTimePerc;

    void Start()
    {
        turretS = GetComponent<Slow>();
        slowTimePerc = UpgradeSlowTIme / 100;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        turretS.m_SlowAmount = UgradeSlowAmount;
        turretS.m_SlowTime += turretS.m_SlowTime * slowTimePerc;
    }

}
