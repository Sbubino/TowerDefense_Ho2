using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController instance;

	public Transform spawnPoint;
	public Transform spawnPoint2;
	public float nextWaveIn;
	public int minionForNextSpawnPoint;
	[HideInInspector]
	public int maxWaveNumber;
	[HideInInspector]
	public int currentMinionPassed;

	public float EnergyUpTime;
	public int EnergyUpValue;
	public float nextEnergyTimeDecrease;
	public float nextEnergyValueDecrease;
	public float moltiplicatoreEnergy;
	public int maxEnergy;
	[HideInInspector]
	public float currentEnergy;

	bool NextSpawnPoint = false;
	GameObject waveHolder;
	GameObject[] wave;
	int nextWaveControl = 0;
	int indexWave = 0;
	int localWaveIndex = 0;
	float waveTimer;
	float wave2Timer;

	float nextEnergyDecreaseTimer = 0;
	float energyTimer = 0;


	void Awake() {
		instance = this;	
		//imposto i valori dell'energy iniziale
		moltiplicatoreEnergy = 1f;
		//currentEnergy = maxEnergy;
		currentEnergy = 50;

		WaveBuild ();	
	}
	

	void Update () {
		WaveControl ();
		EnergyControl ();

		Debug.Log (currentEnergy);
		Debug.Log ("Time" + EnergyUpTime);
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




	void StartNextWave(bool OneOrTwo){	
		//all'aumentare dell'indice dell'ondata attivo quella successiva
		if(OneOrTwo){ 
		   if (localWaveIndex < wave.Length) {
			   wave [localWaveIndex].GetComponent<Wave> ().activator++;
			   localWaveIndex++;
		   }
		}

		if(!OneOrTwo){ 
			if (localWaveIndex < wave.Length) {
				wave [localWaveIndex].GetComponent<Wave> ().activatorSecondPoint++;
				localWaveIndex++;
			}
		}
	}

	void WaveBuild(){
		//riempio l'array wave con tutte le ondate in scena e imposto un valore wavetimer alto per far partire subuto la prima ondata al play

		waveTimer = 100;
		wave2Timer = 100;
		waveHolder = GameObject.FindGameObjectWithTag ("Wave");
		wave = new GameObject[waveHolder.transform.childCount];
		for (int i = 0; i < waveHolder.transform.childCount; i++) {
			wave [i] = waveHolder.transform.GetChild (i).gameObject;
		}
		maxWaveNumber = wave.Length;
	}

	void SetNextWave(){
		waveTimer += Time.deltaTime;
		//periodicamente si attiva l'ondata successiva

		if (waveTimer >= nextWaveIn) {			
			indexWave++;
			waveTimer = 0;
		}
	}

	void WaveControl(){
		//gestione dei due metodi precedenti
		SetNextWave();
		SetSecondPoint();
		SecondSpawnPoint ();

		if (nextWaveControl < indexWave) {
			StartNextWave (true);
			nextWaveControl = indexWave;	
		}

		//stabilisco la fine della partita 
		//if (indexWave >= maxWaveNumber)
			//carica la fine
			
	}

	void SetSecondPoint(){ 
		if (currentMinionPassed >= minionForNextSpawnPoint)
			NextSpawnPoint = true;
	}

	void SecondSpawnPoint(){
		//se il secondo spawn point è attivo spawnano da la
		if(NextSpawnPoint){
			wave2Timer += Time.deltaTime;

			if (wave2Timer >= nextWaveIn){ 
				StartNextWave (false);
			    wave2Timer = 0;
			}
		}
	}
}
