using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController instance;

	public LayerMask m_Bottoniui;
	public float nextWaveIn;
	[HideInInspector]
	public int maxWaveNumber;

	public TweenScale turretMenu;
	public TweenScale turretUpgrade;
	[HideInInspector]
	public GameObject currentTile;

	[HideInInspector]
	public bool openMenu;

	public float EnergyUpTime;
	public int EnergyUpValue;
	public float nextEnergyTimeDecrease;
	public float nextEnergyValueDecrease;
	public float moltiplicatoreEnergy;
	public int maxEnergy;
	//[HideInInspector]
	public float currentEnergy;
	[HideInInspector]
	public float waveTimer;

    DialogoController dialogo;


    GameObject SpawnHolder;
	GameObject[] spawnPoint;
	GameObject waveHolder;
	GameObject[] wave;
	int nextWaveControl = 0;
	int indexWave = 0;
	int localWaveIndex = 0;

    bool multiplierSelected = false;



	float nextEnergyDecreaseTimer = 0;
	float energyTimer = 0;

	public GameObject FatMan;
	private SpriteRenderer fatMan;
	public Sprite[] CiccioneSprite;

    public float energyMultCost = 0.5f;

	void Awake() {
		instance = this;	
		//imposto i valori dell'energy iniziale
		moltiplicatoreEnergy = 1f;
        dialogo = FindObjectOfType<DialogoController>();

        //currentEnergy = maxEnergy;
        //currentEnergy = 300;
        waveTimer = nextWaveIn - 2;

		//WaveBuild ();	
		SpawnpointBuild ();
	}
	

	void Update () {
		//WaveControl ();
		EnergyControl ();
            ClickSelect ();
        if (Input.GetMouseButtonDown(0))

            ClickInfo();
	
		if (openMenu && Input.GetMouseButtonDown (0)) {
           
            if (ClickSelect() == null || !ClickSelect().CompareTag("BottoniUI"))
            {
                CloseMenu();
            }
		}




	}


	//gestione dell'energy
	public void LoseEnergy (float costo){
		currentEnergy -= costo;
	}
	public void TakeEnergy (float incremento){
		currentEnergy = currentEnergy + (Mathf.Round(incremento) * moltiplicatoreEnergy);
	}

    public void IncreaseMultiplier()
    {
        if (currentEnergy >= energyMultCost * maxEnergy)
        {
            if (!multiplierSelected)
                StartCoroutine(ClickMultiplier(false));
            else
            {
                moltiplicatoreEnergy *= 1.1f;
                maxEnergy = (int)Mathf.Round(maxEnergy * 1.2f);
                currentEnergy = currentEnergy - energyMultCost * maxEnergy;
                dialogo.Reset();
                StopAllCoroutines();
                Debug.Log("ene");
            }

        }

        else
            dialogo.GeneralInfo("You don't have enough energy to buy a multiplier" + "\n\n Cost: " + (energyMultCost * maxEnergy) + " energy");


    }

    public IEnumerator ClickMultiplier(bool selected)
    {
        if (!selected)
        {
            yield return new WaitForEndOfFrame();
            multiplierSelected = true;
            dialogo.GeneralInfo("Click now to buy an energy multipier. \n\n Energy will grow faster and maximum energy will be increased" + "\n\n Cost: " + (energyMultCost * maxEnergy) + " energy");
            StartCoroutine(ClickMultiplier(true));
        }

            else
        { yield return new WaitForSeconds(5 * Time.timeScale);
            multiplierSelected = false;
            StopAllCoroutines();
        }

    }

   public void CloseMenu()
    {
       
        turretMenu.ResetToBeginning();
		turretUpgrade.ResetToBeginning ();
		openMenu = false;
        Debug.Log("CloseMenu" + openMenu);
    }



	void EnergyControl (){
		//aumenta con il tempo
		energyTimer += Time.deltaTime;

		if (energyTimer >= EnergyUpTime && currentEnergy > 0) {
			currentEnergy += EnergyUpValue;
			energyTimer = 0;
		}

		if (EnergyUpTime <= 0.5f)
			EnergyUpTime = 0.5f;

		NextEnergyTime ();

		if (currentEnergy > maxEnergy)
			currentEnergy = maxEnergy;

		if (currentEnergy < 0) {
			GuiController.instance.lose = true;
		}
	}	  

	void NextEnergyTime(){
		nextEnergyDecreaseTimer += Time.deltaTime;

		if (nextEnergyDecreaseTimer >= nextEnergyTimeDecrease) {
			EnergyUpTime -= nextEnergyValueDecrease ;
			nextEnergyDecreaseTimer = 0;
		}

	}

    public void OpenMenu(GameObject target)
    {
        currentTile = target;
        turretUpgrade.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -2);
        turretUpgrade.PlayForward();
        openMenu = true;
    }


	void WaveBuild(){
		//riempio l'array wave con tutte le ondate in scena e imposto un valore wavetimer alto per far partire subuto la prima ondata al play

		waveTimer = 100;
		waveHolder = GameObject.FindGameObjectWithTag ("Wave");
		wave = new GameObject[waveHolder.transform.childCount];
		for (int i = 0; i < waveHolder.transform.childCount; i++) {
			wave [i] = waveHolder.transform.GetChild (i).gameObject;
		}

	}


	void SpawnpointBuild(){
    SpawnHolder = GameObject.FindGameObjectWithTag ("Spawnpoint");
		spawnPoint = new GameObject[SpawnHolder.transform.childCount];
		for (int i = 0; i < SpawnHolder.transform.childCount; i++) {
			spawnPoint [i] = SpawnHolder.transform.GetChild (i).gameObject;
		}
	}

    public void BuildTurret(GameObject name)
    {

        currentTile.SendMessage("BuildTurret", name);
    }

	public void Upgrade(){
        Debug.Log("UpgradePerJesoo");
        currentTile.SendMessage("UpGrade");

	}

	public void Sell(){
		currentTile.SendMessage("Sell");
	}

    public void EnergyBarInfo()
    {
        dialogo.GeneralInfo("This is my energy, use it wisely! \n Please, keep me fat, don't let it go to 0! \n\n If you tap on the burger, you can buy an energy multiplier");
    }


    public void WaveControl(int waveLenght){
		//gestione dei due metodi precedenti
		maxWaveNumber = waveLenght;

		SetNextWave();

		if (indexWave <= waveLenght) {
			if (nextWaveControl < indexWave) {
				StartNextWave (waveLenght);
				nextWaveControl = indexWave;	
			}
		}

		//stabilisco la fine della partita 
		if (indexWave >= maxWaveNumber) {
			if(GameObject.FindWithTag ("Enemy") == null)
				GuiController.instance.win = true;
		}
	}

	void StartNextWave(int waveLenght){			


		if (localWaveIndex < waveLenght) {
			for (int i = 1; i < spawnPoint.Length; i++) {
				if (spawnPoint [0].activeSelf) {
					spawnPoint [0].GetComponent<Spawnpoint> ().StartWave (localWaveIndex);


				if (spawnPoint [i].activeSelf) {
					spawnPoint [i].GetComponent<Spawnpoint> ().StartWave (localWaveIndex);
				}	

					if (localWaveIndex < waveLenght - 1)
					localWaveIndex++;
				}
			}			
		}
	}

	void SetNextWave(){
		waveTimer += Time.deltaTime;
		//periodicamente si attiva l'ondata successiva

		if (waveTimer >= nextWaveIn) {					
			indexWave++;
			waveTimer = 0;
		}

	}

    GameObject ClickSelect()
    {
        //Converting Mouse Pos to 2D (vector2) World Pos
        Vector2 rayPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 5, m_Bottoniui);

        if (hit.transform != null)
        {
            return hit.transform.gameObject;


        }
        else
            return null;


    }


	void changeSprite()
	{
		if(currentEnergy >= 150)
		{
			fatMan.sprite = CiccioneSprite[0];
		}
		if(currentEnergy <= 149)
		{
			fatMan.sprite = CiccioneSprite[1];
		}
		if(currentEnergy <= 75)
		{
			fatMan.sprite = CiccioneSprite[2];
		}
	}



    void ClickInfo()
    {
        //Converting Mouse Pos to 2D (vector2) World Pos
        Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
        if (hit)
        {
            if (hit.transform.tag == "Core")
            {
                Debug.Log(hit.transform.name);
                dialogo.GeneralInfo("I'm so hungry, but i hate veggies. \nHelp me!");
                //return hit.transform.gameObject;
            }

            if (hit.transform.tag == "Enemy")
            {
                dialogo.GeneralInfo(hit.transform.gameObject.GetComponent<Enemy>().SendInfo());
            }

        }
       // else return null;
    }

}
