using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController instance;


	public float nextWaveIn;
	[HideInInspector]
	public int maxWaveNumber;

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
	float waveTimer;


	float nextEnergyDecreaseTimer = 0;
	float energyTimer = 0;


	void Awake() {
		instance = this;	
		//imposto i valori dell'energy iniziale
		moltiplicatoreEnergy = 1f;
		//currentEnergy = maxEnergy;
		currentEnergy = 50;

		WaveBuild ();	
		SpawnpointBuild ();
	}
	

	void Update () {
		WaveControl ();
		EnergyControl ();
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

		if (energyTimer >= EnergyUpTime) {
			currentEnergy += EnergyUpValue;
			energyTimer = 0;
		}

		if (EnergyUpTime <= 0.5f)
			EnergyUpTime = 0.5f;

		NextEnergyTime ();

		if (currentEnergy > maxEnergy)
			currentEnergy = maxEnergy;
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



	void WaveControl(){
		//gestione dei due metodi precedenti
		SetNextWave();
		if (indexWave <= wave.Length) {
			if (nextWaveControl < indexWave) {
				StartNextWave ();
				nextWaveControl = indexWave;	
			}
		}

		//stabilisco la fine della partita 
		//if (indexWave >= maxWaveNumber)
		//carica la fine

	}

	void StartNextWave(){	
		if (localWaveIndex < wave.Length) {
			for (int i = 0; i < spawnPoint.Length; i++) {
				if (spawnPoint [i].activeSelf) {
					if (wave [localWaveIndex].GetComponent<Wave> ().activator != 0) {
						break;
					}
				
				    wave [localWaveIndex].GetComponent<Wave> ().StartWave (spawnPoint [i]);
					if (localWaveIndex < wave.Length - 1)
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
}
