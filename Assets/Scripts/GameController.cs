using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController instance;

	public Transform spawnPoint;
	public float nextWaveIn;
	public int maxWaveNumber;

	public float moltiplicatoreEnergy;
	public int maxEnergy;

	GameObject[] wave;
	int nextWaveControl = 0;
	int indexWave = 0;
	int localWaveIndex = 0;
	float waveTimer;

	float currentEnergy;


	void Awake() {
		instance = this;	

		moltiplicatoreEnergy = 1f;
		currentEnergy = maxEnergy;
		waveTimer = 100;
		wave = GameObject.FindGameObjectsWithTag ("Enemy");
	}
	

	void Update () {
		WaveControl ();
	}



	public void LoseEnergy (int costo){
		currentEnergy -= costo;
	}
	public void TakeEnergy (int incremento){
		currentEnergy = currentEnergy + (Mathf.Round(incremento) * moltiplicatoreEnergy);
	}

	public void EnergyControl (){
		if (currentEnergy > maxEnergy)
			currentEnergy = maxEnergy;
		//aumenta con il tempo
	}
	  


	void StartNextWave(){		
		if (localWaveIndex < wave.Length) {
			wave [localWaveIndex].GetComponent<Wave> ().activator++;
			localWaveIndex++;
		}
	}
	void SetNextWave(){
		waveTimer += Time.deltaTime;

		if (waveTimer >= nextWaveIn) {			
			indexWave++;
			waveTimer = 0;
		}
	}
	void WaveControl(){
		SetNextWave();

		if (nextWaveControl < indexWave) {
			StartNextWave ();
			nextWaveControl = indexWave;	
		}

		if (indexWave >= maxWaveNumber)
			//carica la fine
			Debug.Log ("Ciao");
	}
}
