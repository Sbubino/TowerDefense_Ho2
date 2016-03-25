﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController instance;

	public LayerMask m_Bottoniui;
	public float nextWaveIn;
	[HideInInspector]
	public int maxWaveNumber;

	public TweenScale turretMenu;
	public GameObject currentTile;

	[HideInInspector]
	public bool openMenu;

	public float EnergyUpTime;
	public int EnergyUpValue;
	public float nextEnergyTimeDecrease;
	public float nextEnergyValueDecrease;
	public float moltiplicatoreEnergy;
	public int maxEnergy;
	[HideInInspector]
	public float currentEnergy;

	GameObject SpawnHolder;
	GameObject[] spawnPoint;
	GameObject waveHolder;
	GameObject[] wave;
	int nextWaveControl = 0;
	int indexWave = 0;
	int localWaveIndex = 0;
	float waveTimer = 100;


	float nextEnergyDecreaseTimer = 0;
	float energyTimer = 0;


	void Awake() {
		instance = this;	
		//imposto i valori dell'energy iniziale
		moltiplicatoreEnergy = 1f;
		//currentEnergy = maxEnergy;
		currentEnergy = 150;

		//WaveBuild ();	
		SpawnpointBuild ();
	}
	

	void Update () {
		//WaveControl ();
		EnergyControl ();
		ClickSelect ();
		if (Input.GetKeyDown(KeyCode.S))
			Time.timeScale = 2;

		if (Input.GetKey(KeyCode.R))
			Time.timeScale = 1;


		if (openMenu && Input.GetMouseButtonDown (0)) {
			if ( ClickSelect() != null &&  ClickSelect ().Equals(turretMenu.gameObject))
				Debug.Log (ClickSelect().transform);
			else	
				turretMenu.ResetToBeginning ();
		}




	}


	//gestione dell'energy
	public void LoseEnergy (int costo){
		currentEnergy -= costo;
	}
	public void TakeEnergy (int incremento){
		currentEnergy = currentEnergy + (Mathf.Round(incremento) * moltiplicatoreEnergy);
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
			Debug.Log ("Hai perso,sei una sega!");
		}
	}	  

	void NextEnergyTime(){
		nextEnergyDecreaseTimer += Time.deltaTime;

		if (nextEnergyDecreaseTimer >= nextEnergyTimeDecrease) {
			EnergyUpTime -= nextEnergyValueDecrease ;
			nextEnergyDecreaseTimer = 0;
		}

	}


	void WaveBuild(){
		//riempio l'array wave con tutte le ondate in scena e imposto un valore wavetimer alto per far partire subuto la prima ondata al play

		waveTimer = 100;
		waveHolder = GameObject.FindGameObjectWithTag ("Wave");
		wave = new GameObject[waveHolder.transform.childCount];
		for (int i = 0; i < waveHolder.transform.childCount; i++) {
			wave [i] = waveHolder.transform.GetChild (i).gameObject;
		}
		maxWaveNumber = wave.Length;
	}


	void SpawnpointBuild(){
    SpawnHolder = GameObject.FindGameObjectWithTag ("Spawnpoint");
		spawnPoint = new GameObject[SpawnHolder.transform.childCount];
		for (int i = 0; i < SpawnHolder.transform.childCount; i++) {
			spawnPoint [i] = SpawnHolder.transform.GetChild (i).gameObject;
		}
	}



	public void WaveControl(int waveLenght){
		//gestione dei due metodi precedenti
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
				Debug.Log ("LivelloFinito");
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
		Vector3 rayPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x , Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -0.5f);
		RaycastHit hit;

		if (Physics.Raycast (rayPos, Vector3.forward, out hit, m_Bottoniui)) {

			if (hit.transform.gameObject != null) {

				return hit.transform.gameObject;
			}
			else return null;
		}
		else return null;
	}
	
	

}
