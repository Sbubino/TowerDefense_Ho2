using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildTile : MonoBehaviour {

    public GameObject[] m_TurretPreab;

    GameObject[] turretPool;
	Upgrade turretUp;
    Turret curretTower;

    public bool fast;
    public bool area;
    public bool slow;
    public bool heavy;

    [HideInInspector]
    public bool builded;	


    void Awake()
    {
        
        turretPool = new GameObject[m_TurretPreab.Length];

        for(int i = 0; i < turretPool.Length; i++)
        {
            turretPool[i] = Instantiate(m_TurretPreab[i], transform.position, m_TurretPreab[i].transform.rotation) as GameObject;
			turretPool[i].SetActive(false);
        }

		for (int i =0; i < turretPool.Length; i++) {
			if(fast)
				BuildTurret("FastT(Clone)");
			else if(area)
				BuildTurret("AreaT(Clone)");
			else if(slow)
				BuildTurret("SlowT(Clone)");
			else if(heavy)
				BuildTurret("PowerT(Clone)");
		}
    }


	public void BuildTurret(string tipeTurret){
		string name = tipeTurret;
			
		
		if (name.Equals("FastT(Clone)") )
		{
			builded = true;
			turretPool[1].SetActive(true);
			turretUp = turretPool[1].GetComponent<Upgrade>();
            curretTower = turretPool[1].GetComponent<Turret>();

        }
		if (name.Equals("AreaT(Clone)") )
		{
			builded = true;
			turretPool[0].SetActive(true);
			turretUp = turretPool[0].GetComponent<Upgrade>();
            curretTower = turretPool[0].GetComponent<Turret>();

        }
		if (name.Equals("SlowT(Clone)") )
		{
			builded = true;
			turretPool[3].SetActive(true);
			turretUp = turretPool[3].GetComponent<Upgrade>();
            curretTower = turretPool[3].GetComponent<Turret>();


        }
		if (name.Equals("PowerT(Clone)") )
		{
			builded = true;
			turretPool[2].SetActive(true);
			turretUp = turretPool[2].GetComponent<Upgrade>();
            curretTower = turretPool[2].GetComponent<Turret>();


        }
	}


	public void UpGrade(){
     
        if (turretUp.Liv < 2 && GameController.instance.currentEnergy >= curretTower.CostUpgrade ) {
            turretUp.LevelUp();
			GameController.instance.LoseEnergy(curretTower.CostUpgrade);
			GameController.instance.CloseMenu();            	
		}
	}
    

	public void Sell(){
	//	GameController.instance.TakeEnergy (turretUp.SellValue);


		for (int i = 0; i < turretPool.Length; i++) {
			if(turretPool[i].activeInHierarchy)
				turretPool[i].SetActive(false);
        }

		builded = false;
		GameController.instance.CloseMenu ();
	}


	
}
