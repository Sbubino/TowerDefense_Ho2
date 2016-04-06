using UnityEngine;
using System.Collections;

public class BuildTurret : MonoBehaviour {


    public GameObject[] allTurret;
    public GameObject[] buttons = new GameObject[4];
 
	public LayerMask m_BuildTileLayer;

    GameObject[] buildTile;
    GameObject[] buttonTurret;
    GameObject turret;
    RaycastHit2D hit;

    bool positioned = true;
	

    void Awake()
    {
        buttonTurret = new GameObject[allTurret.Length];

        for (int i = 0; i < allTurret.Length; i++)
        {
           buttonTurret[i] = Instantiate(allTurret[i], transform.position, allTurret[i].transform.rotation) as GameObject;
           buttonTurret[i].SetActive(false);
        }

        buildTile = GameObject.FindGameObjectsWithTag("BuildTile");
    }


	void Update(){

        TurretPositioning ();
        ButtonColor();

	}


    void TurretPositioning() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!positioned && turret != null)
        {
            turret.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
            turret.GetComponent<Turret>().canShoot = false;
        }
        

        //se ho la torretta e clicco mouse destro la elimino
        if (turret != null && !positioned && Input.GetMouseButtonDown(1))
        {
            turret = null;

            for (int i = 0; i < 4; i++)
            {
                buttonTurret[i].SetActive(false);
            }
        }

        if (turret != null && !positioned) { 
            for (int i = 0; i < buildTile.Length; i++)
            {
                if (buildTile[i].GetComponent<BuildTile>().builded == false)
                    buildTile[i].GetComponent<SpriteRenderer>().color = Color.green;              
            }
        }

        else if (turret == null)
        {
            for (int i = 0; i < buildTile.Length; i++)
            {
                buildTile[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }


        hit = Physics2D.Raycast (mousePos, Vector3.forward, 100, m_BuildTileLayer);

        if (hit != null && turret != null && !positioned )
        {
            //la torretta segue il mouse
            turret.transform.position = hit.transform.position;         

            //posiziono la torretta nella build tile
            if (Input.GetMouseButtonDown(0) && hit.collider.gameObject.GetComponent<BuildTile>().builded == false)
            {
                hit.collider.gameObject.GetComponent<BuildTile>().BuildTurret(turret.name);
                GameController.instance.currentEnergy -= turret.GetComponent<Turret>().CostBuild;
               
                turret = null;
                positioned = true;
            }
        }
    }


	public void BuildTower(GameObject button){
		string names = button.name;
		Vector3 mousePos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);

		positioned = false;

        if (names == "Fast" && GameController.instance.currentEnergy >= buttonTurret[0].GetComponent<Turret>().CostBuild)
        {
            buttonTurret[0].SetActive(true);
            turret = buttonTurret[0];
        }
        else if (names == "Area" && GameController.instance.currentEnergy >= buttonTurret[1].GetComponent<Turret>().CostBuild)
        {
            buttonTurret[1].SetActive(true);
            turret = buttonTurret[1];
        }
       

        else if (names == "Heavy" && GameController.instance.currentEnergy >= buttonTurret[2].GetComponent<Turret>().CostBuild)
        {
            buttonTurret[2].SetActive(true);
            turret = buttonTurret[2];
        }

        else if (names == "Slow" && GameController.instance.currentEnergy >= buttonTurret[3].GetComponent<Turret>().CostBuild)
        {
            buttonTurret[3].SetActive(true);
            turret = buttonTurret[3];
        }       
    }



    void ButtonColor()
    {

        for (int i = 0; i < buttons.Length; i++)
        {
             if (GameController.instance.currentEnergy < buttonTurret[i].GetComponent<Turret>().CostBuild)
                 buttons[i].transform.GetChild(1).transform.GetComponent<UISprite>().color = Color.white;


             else
                buttons[i].transform.GetChild(1).transform.GetComponent<UISprite>().color = Color.green;


            buttons[i].GetComponentInChildren<UILabel>().text = buttonTurret[i].GetComponent<Turret>().CostBuild.ToString();
        }
    }
}