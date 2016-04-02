using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildTile : MonoBehaviour {

    public GameObject[] m_TurretPreab;

    GameObject[] turretPool;
	Upgrade turretUp;
    Turret curretTower;
    public bool builded;

	public bool fast;
	public bool area;
	public bool slow;
	public bool heavy;


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
				BuildTurret("Fast");
			else if(area)
				BuildTurret("Area");
			else if(slow)
				BuildTurret("Slow");
			else if(heavy)
				BuildTurret("Heavy");

		}

    }


   public void OnClick() {


        if (!builded && !GameController.instance.openMenu) {
			GameController.instance.currentTile = gameObject;
			GameController.instance.turretMenu.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z -1);
			GameController.instance.turretMenu.PlayForward ();
			GameController.instance.openMenu = true;
		} else if (builded && !GameController.instance.openMenu) {
			GameController.instance.openMenu = true;
			GameController.instance.currentTile = gameObject;
			GameController.instance.turretUpgrade.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z -1);
			GameController.instance.turretUpgrade.PlayForward ();


		}
   }

	public void BuildTurret(GameObject tipeTurret){
        string name = tipeTurret.name;

        Debug.Log(name);


        if (name.Equals("Fast") && GameController.instance.currentEnergy >= 80)
        {
            builded = true;
            turretPool[1].SetActive(true);
			turretUp = turretPool[1].GetComponent<Upgrade>();
            curretTower = turretPool[1].GetComponent<Turret>();
            GameController.instance.LoseEnergy(80);
            GameController.instance.CloseMenu();
        }
		if (name.Equals("Area") && GameController.instance.currentEnergy >= 250)
        {
            builded = true;
            turretPool[0].SetActive(true);
			turretUp = turretPool[0].GetComponent<Upgrade>();
            curretTower = turretPool[0].GetComponent<Turret>();
            GameController.instance.LoseEnergy(250);
            GameController.instance.CloseMenu();
        }
		if (name.Equals("Slow") && GameController.instance.currentEnergy >= 200)
        {
            builded = true;
            turretPool[3].SetActive(true);
			turretUp = turretPool[3].GetComponent<Upgrade>();
            curretTower = turretPool[3].GetComponent<Turret>();
            GameController.instance.LoseEnergy(200);
            GameController.instance.CloseMenu();
        }
		if (name.Equals("Heavy") && GameController.instance.currentEnergy >= 100)
        {
            builded = true;
            turretPool[2].SetActive(true);
			turretUp = turretPool[2].GetComponent<Upgrade>();
            curretTower = turretPool[2].GetComponent<Turret>();
            GameController.instance.LoseEnergy(100);
            GameController.instance.CloseMenu();
        }
    }

	public void BuildTurret(string tipeTurret){
		string name = tipeTurret;
		
		Debug.Log(name);
		
		
		if (name.Equals("Fast") )
		{
			builded = true;
			turretPool[1].SetActive(true);
			turretUp = turretPool[1].GetComponent<Upgrade>();
            curretTower = turretPool[1].GetComponent<Turret>();

        }
		if (name.Equals("Area") )
		{
			builded = true;
			turretPool[0].SetActive(true);
			turretUp = turretPool[0].GetComponent<Upgrade>();
            curretTower = turretPool[0].GetComponent<Turret>();

        }
		if (name.Equals("Slow") )
		{
			builded = true;
			turretPool[3].SetActive(true);
			turretUp = turretPool[3].GetComponent<Upgrade>();
            curretTower = turretPool[3].GetComponent<Turret>();


        }
		if (name.Equals("Heavy") )
		{
			builded = true;
			turretPool[2].SetActive(true);
			turretUp = turretPool[2].GetComponent<Upgrade>();
            curretTower = turretPool[2].GetComponent<Turret>();


        }
	}


	public void UpGrade(){
        Debug.Log("qui");
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
