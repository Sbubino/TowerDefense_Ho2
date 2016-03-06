using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Transform spawnPoint;

	int indexEnemy = 0;
	public GameObject[] Wave1;
	public GameObject[] Wave2;
	public GameObject[] Wave3;
	bool wave1SetActive = true;
	bool wave2SetActive = false;
	bool wave3SetActive = false;

	public float nextWaveIn;

	public float wave1spawnTime;
	public float wave2spawnTime;
	public float wave3spawnTime;
	float waveTimer;

	//GameObject[] enemyA;
	//GameObject[] enemyB;

	void Start () {
		//enemyA = GameObject.FindGameObjectsWithTag ("EnemyA");
		//enemyB = GameObject.FindGameObjectsWithTag ("EnemyB");
	}
	
	// Update is called once per frame
	void Update () {
		SpawnMinionWave1 ();
		SpawnMinionWave2 ();
		SpawnMinionWave3 ();

		//Debug.Log (" wave1SetActive" + wave1SetActive);
		//Debug.Log (" wave2SetActive" + wave2SetActive);
		//Debug.Log (" wave3SetActive" + wave3SetActive);
	}


	public void SpawnMinionWave1(){
		if (wave1SetActive){
		    waveTimer += Time.deltaTime;

			if (waveTimer >= wave1spawnTime) {

				Wave1 [indexEnemy].transform.position = new Vector2 (spawnPoint.position.x, spawnPoint.position.y);	
				Wave1 [indexEnemy].SetActive (true);

				indexEnemy++;
				waveTimer = 0;

				if (indexEnemy == Wave1.Length) {
					wave1SetActive = false;
					StartCoroutine (StartWave2 ());
				}
			}
	    }
	}

	public IEnumerator StartWave2(){
		yield return new WaitForSeconds (nextWaveIn);
		wave2SetActive = true;
		indexEnemy = 0;
	}

	public void SpawnMinionWave2(){
		if (wave2SetActive){
			waveTimer += Time.deltaTime;

			if (waveTimer >= wave2spawnTime) {

				Wave2 [indexEnemy].transform.position = new Vector2 (spawnPoint.position.x, spawnPoint.position.y);	
				Wave2 [indexEnemy].SetActive (true);

				indexEnemy++;
				waveTimer = 0;

				if (indexEnemy == Wave2.Length) {
					wave2SetActive = false;
					StartCoroutine (StartWave3 ());
				}
			}
		}
	}

	public IEnumerator StartWave3(){
		yield return new WaitForSeconds (nextWaveIn);
		wave3SetActive = true;
		indexEnemy = 0;
	}

	public void SpawnMinionWave3(){
		if (wave3SetActive){
			waveTimer += Time.deltaTime;

			if (waveTimer >= wave3spawnTime) {

				Wave3 [indexEnemy].transform.position = new Vector2 (spawnPoint.position.x, spawnPoint.position.y);	
				Wave3 [indexEnemy].SetActive (true);

				indexEnemy++;
				waveTimer = 0;

				if (indexEnemy == Wave3.Length) {
					wave3SetActive = false;
				}
			}
		}
	}
}
