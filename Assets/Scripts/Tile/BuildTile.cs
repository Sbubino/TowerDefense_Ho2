using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildTile : MonoBehaviour {
    public LayerMask m_TurretMask;
    DialogoController dialogo;

    Upgrade turretUp;
    public Turret curretTower;
    
    
    public bool builded;

    float timer;
        	
    void Awake()
    {
        dialogo = FindObjectOfType<DialogoController>();

    }

    void Update()
    {
  //      if (curretTower != null)
//            Debug.Log("Build + " + transform.name + "Torre + " + turretUp.transform.name);

       
    }


    public void OnClic()
    {
        if (builded)
        {
            GameController.instance.OpenMenu(gameObject);
            curretTower.UpInfo(true);
        }
        else
        {
            dialogo.SendMessage("GeneralInfo", "Empty slot, you can drop some food here");

        }

    }


    void SetCurrentTurret(GameObject current)
    {
        curretTower = current.GetComponent<Turret>();
        turretUp = current.GetComponent<Upgrade>();
        //builded = true; 

        StartCoroutine(SetBuilded());
    }

    

    public IEnumerator SetBuilded()
    {
        yield return new WaitForSeconds(0.4f);
        builded = true;
    }


    public void UpGrade(){
     
        if (turretUp.Liv < 2) {
            if (GameController.instance.currentEnergy >= curretTower.CostUpgrade)
            {
                Debug.Log("costo up " + curretTower.CostUpgrade);
                GameController.instance.LoseEnergy(curretTower.CostUpgrade);
                turretUp.LevelUp();

                GameController.instance.CloseMenu();
                dialogo.Reset();
            }
            else dialogo.GeneralInfo("You don't have enough energy to upgrade this turret\n\n\nCost: " + curretTower.CostUpgrade);
		}
        else dialogo.GeneralInfo("This turret has reached the maximum level!");


    }
    

	public void Sell(){
	GameController.instance.TakeEnergy (curretTower.value);
        curretTower.gameObject.SetActive(false);
        builded = false;
		GameController.instance.CloseMenu ();
	}


	
}
