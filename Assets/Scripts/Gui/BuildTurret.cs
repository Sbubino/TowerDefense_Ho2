using UnityEngine;
using System.Collections;

public class BuildTurret : MonoBehaviour {


    public GameObject[] allTurret;
    public GameObject[] buttons = new GameObject[4];
 
	public LayerMask m_BuildTileLayer;

    GameObject[] buildTile;
    GameObject[] buttonTurret;
    GameObject currentTurret;
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

        if (!positioned && currentTurret != null)
        {
            currentTurret.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
            currentTurret.GetComponent<Turret>().canShoot = false;
        }
        

        //se ho la torretta e clicco mouse destro la elimino
        if (currentTurret != null && !positioned && Input.GetMouseButtonDown(1))
        {
            currentTurret = null;

            for (int i = 0; i < 4; i++)
            {
                buttonTurret[i].SetActive(false);
            }
        }

        if (currentTurret != null && !positioned) { 
            for (int i = 0; i < buildTile.Length; i++)
            {
                if (buildTile[i].GetComponent<BuildTile>().builded == false)
                    buildTile[i].GetComponent<SpriteRenderer>().color = Color.green;              
            }
        }

        else if (currentTurret == null)
        {
            for (int i = 0; i < buildTile.Length; i++)
            {
                buildTile[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }


        hit = Physics2D.Raycast (mousePos, Vector3.forward, 100, m_BuildTileLayer);

        if (hit != false && currentTurret != null && !positioned )
        {
            //la torretta segue il mouse
            currentTurret.transform.position = hit.transform.gameObject.transform.position;         

            //posiziono la torretta nella build tile
            if (Input.GetMouseButtonDown(0) && hit.collider.gameObject.GetComponent<BuildTile>().builded == false)
            {
                currentTurret.transform.position = hit.transform.position;
                Turret temp =  currentTurret.gameObject.GetComponent<Turret>();
                temp.SetBuildTile();
                GameController.instance.currentEnergy -= currentTurret.GetComponent<Turret>().CostBuild;

                currentTurret.GetComponent<Turret>().canShoot = true;

                currentTurret = null;
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
            currentTurret = buttonTurret[0];
        }
        else if (names == "Area" && GameController.instance.currentEnergy >= buttonTurret[1].GetComponent<Turret>().CostBuild)
        {
            buttonTurret[1].SetActive(true);
            currentTurret = buttonTurret[1];
        }
       

        else if (names == "Heavy" && GameController.instance.currentEnergy >= buttonTurret[2].GetComponent<Turret>().CostBuild)
        {
            buttonTurret[2].SetActive(true);
            currentTurret = buttonTurret[2];
        }

        else if (names == "Slow" && GameController.instance.currentEnergy >= buttonTurret[3].GetComponent<Turret>().CostBuild)
        {
            buttonTurret[3].SetActive(true);
            currentTurret = buttonTurret[3];
        }       
    }



    void ButtonColor()
    {

        for (int i = 0; i < buttons.Length; i++)
        {

            if (GameController.instance.currentEnergy < buttonTurret[i].GetComponent<Turret>().CostBuild)
            {
                buttons[i].transform.GetChild(1).transform.GetComponent<UISprite>().color = Color.white;
                buttons[i].GetComponentInChildren<UISprite>().color = Color.grey;

            }


            else
            {
                buttons[i].transform.GetChild(1).transform.GetComponent<UISprite>().color = Color.green;
                buttons[i].GetComponentInChildren<UISprite>().color = Color.white;
            }


            buttons[i].GetComponentInChildren<UILabel>().text = buttonTurret[i].GetComponent<Turret>().CostBuild.ToString();
        }
    }
}