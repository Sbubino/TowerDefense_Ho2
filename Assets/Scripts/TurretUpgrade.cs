using UnityEngine;
using System.Collections;

public class TurretUpgrade : MonoBehaviour {

	public GameObject sprite1;
	public GameObject sprite2;
	public GameObject sprite3;

	public float multiplierDamage1;
	public float multiplierDamage2;
	public float multiplierRange1;
	public float multiplierRange2;
	public float multiplierCost1;
	public float multiplierCost2;
	public float multiplierSlow1;
	public float multiplierSlow2;
	public float multiplierExp1;
	public float multiplierExp2;

	public float fireRate=1;
	public float range=1;
	public static float rangeRef;
	public float turretDamage;
	public static float damageRefer;
	public int turretCost;
	public float upgradeCost;
	public int sellEarn;
	public float slowAmmount;
	public float explosionRange;
	public float multiplierPowerUp1;

	public bool upgrade1=false, upgrade2=false;

	void Awake (){
		sprite1 = transform.GetChild (0).gameObject;
		sprite2 = transform.GetChild (1).gameObject;
		sprite3 = transform.GetChild (2).gameObject;
		sprite1.SetActive (true);
		sprite2.SetActive (false);
		sprite3.SetActive (false);
	}

	 void PowerUp(){
        if (upgrade1)
        {
            sprite1.SetActive(false);
            sprite2.SetActive(true);
            sprite3.SetActive(false);

            turretDamage = turretDamage *= multiplierDamage1;
            range = range *= multiplierRange1;
            upgradeCost = upgradeCost *= multiplierCost1;
            slowAmmount = slowAmmount *= multiplierSlow1;
            explosionRange = explosionRange *= multiplierExp1;
        }

        else {
            sprite1.SetActive (false);
			sprite2.SetActive (false);
			sprite3.SetActive (true);

            turretDamage = turretDamage *= multiplierDamage2;
            range = range *= multiplierRange2;
            upgradeCost = upgradeCost *= multiplierCost2;
            slowAmmount = slowAmmount *= multiplierSlow2;
            explosionRange = explosionRange *= multiplierExp2;
            
        }
					
	}


    public void PowerUp1Active(){
		upgrade1 = true;
		upgrade2 = false;
		PowerUp ();
	}
	public void PowerUp2Active(){
		upgrade1 = false;
		upgrade2 = true;
		PowerUp ();
	}

}
