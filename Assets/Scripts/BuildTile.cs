using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildTile : MonoBehaviour {

    public GameObject[] m_TurretPreab;

    GameObject[] turretPool;
    bool builded;

    void Awake()
    {
        builded = false;
        turretPool = new GameObject[m_TurretPreab.Length];

        for(int i = 0; i < turretPool.Length; i++)
        {
            turretPool[i] = Instantiate(m_TurretPreab[i], transform.position, m_TurretPreab[i].transform.rotation) as GameObject;
            turretPool[i].SetActive(false);
        }

    }


   public void OnClick() {


        if (!builded && !GameController.instance.openMenu) {
			GameController.instance.currentTile = gameObject;
			GameController.instance.turretMenu.gameObject.transform.position = gameObject.transform.position;
			GameController.instance.turretMenu.PlayForward ();
			GameController.instance.openMenu = true;
		} /*else if (builded && !GameController.instance.openMenu) {
			GameController.instance.currentTile = gameObject;
			GameController.instance.upgradeMenu.gameObject.transform.position = gameObject.transform.position;
			GameController.instance.upgradeMenu.PlayForward ();
			GameController.instance.openMenu = true;

		}*/
   }

	public void BuildTurret(GameObject tipeTurret){
        string name = tipeTurret.name;

        Debug.Log(name);


        if (name.Equals("Fast"))
        {
            builded = true;
            turretPool[1].SetActive(true);
            GameController.instance.CloseMenu();
        }
        if (name.Equals("Area"))
        {
            builded = true;
            turretPool[0].SetActive(true);
            GameController.instance.CloseMenu();
        }
        if (name.Equals("Slow"))
        {
            builded = true;
            turretPool[3].SetActive(true);
            GameController.instance.CloseMenu();
        }
        if (name.Equals("Heavy"))
        {
            builded = true;
            turretPool[2].SetActive(true);
            GameController.instance.CloseMenu();
        }
    }

	public void UpGrade(){
		Debug.Log ("upgrade");

	}

	public void Sell(){
		Debug.Log("sell");

	}
	
}
