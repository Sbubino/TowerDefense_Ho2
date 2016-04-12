using UnityEngine;
using System.Collections;

public class Upgrade : MonoBehaviour
{
    public float UpgradeDamage;
    public float UpgradeFireRate;
    public float UpgradeRange;
    public float UpgradeCost;
    public int Liv = 0;

    Turret turret;
    float damagePercent;
    float fireRatePercent;
    float rangePercent;
    float costPercent;
   

    //Variabili Cambio Sprite
    GameObject sprite;
    GameObject[] sprites;


    void Awake()
    {
        sprite = transform.FindChild("Sprite").gameObject;
        sprites = new GameObject[3];
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i] = sprite.transform.GetChild(i).gameObject;
            sprites[i].SetActive(false);
        }

        ChangeSprite();

        turret = GetComponent<Turret>();

        damagePercent = UpgradeDamage / 100;
        fireRatePercent = UpgradeFireRate / 100;
        rangePercent = UpgradeRange / 100;
        costPercent = UpgradeCost / 100;
    }

    public virtual void LevelUp()
    {
        if (Liv <= 2)
        {
            turret.m_Damage +=  turret.m_Damage * damagePercent;
            turret.m_FireRate += turret.m_FireRate * fireRatePercent;
			turret.m_Range += turret.m_Range * rangePercent;
            turret.setRange(turret.m_Range);
            turret.CostUpgrade += turret.CostUpgrade * costPercent;

            Liv++;

            ChangeSprite();

        }
    }

    void ChangeSprite()
    {
        if (Liv == 0)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if (i == 0)
                {
                    sprites[i].SetActive(true);
                }
                else
                    sprites[i].SetActive(false);
            }
        }
        else if (Liv == 1)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if (i == 1)
                {
                    sprites[i].SetActive(true);
                }
                else
                    sprites[i].SetActive(false);
            }
        }
        if (Liv == 2)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if (i == 2)
                {
                    sprites[i].SetActive(true);
                }
                else
                    sprites[i].SetActive(false);
            }

        }
    }
}
