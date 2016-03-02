using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {

	public Transform spawnPoint;

	int indexEnemy = 0;
	public GameObject[] Wave1;
	public GameObject[] Wave2;
	public GameObject[] Wave3;
	bool wave1SetActive = true;
	bool wave2SetActive = false;
	bool wave3SetActive = false;

	public float NextWaveTimer;

	public float wave1spawnTime;
	public float wave2spawnTime;
	public float wave3spawnTime;
	float waveMinionTimer;


	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		SpawnMinionWave1 ();
		SpawnMinionWave2 ();
		SpawnMinionWave3 ();


		Debug.Log ("Wave1 Active " + wave1SetActive);
		Debug.Log ("Wave2 Active " + wave2SetActive);
		Debug.Log ("Wave3 Active " + wave3SetActive);
	}


	public void SpawnMinionWave1(){
		if (wave1SetActive){
			waveMinionTimer += Time.deltaTime;

	     	if (waveMinionTimer >= wave1spawnTime) {

			     Wave1 [indexEnemy].transform.position = new Vector2 (spawnPoint.position.x, spawnPoint.position.y);	
			     Wave1 [indexEnemy].SetActive (true);

		      	indexEnemy++;
			    waveMinionTimer = 0;

				if (indexEnemy == Wave1.Length){
				    wave1SetActive = false;	
					StartCoroutine (NextWaveStart2 ());	
				}
		    }
	    }
	}

	public IEnumerator NextWaveStart2(){
		yield return new WaitForSeconds (NextWaveTimer);
		wave2SetActive = true;		
		indexEnemy = 0;
	}


	public void SpawnMinionWave2(){
		if (wave2SetActive){
			waveMinionTimer += Time.deltaTime;

			if (waveMinionTimer >= wave2spawnTime) {

				Wave2 [indexEnemy].transform.position = new Vector2 (spawnPoint.position.x, spawnPoint.position.y);	
				Wave2 [indexEnemy].SetActive (true);

				indexEnemy++;
				waveMinionTimer = 0;

				if (indexEnemy == Wave2.Length){
					wave2SetActive = false;	
					StartCoroutine (NextWaveStart3 ());	
				}
			}
		}
	}

	public IEnumerator NextWaveStart3(){
		yield return new WaitForSeconds (NextWaveTimer);
		wave3SetActive = true;	
		indexEnemy = 0;
	}

	public void SpawnMinionWave3(){
		if (wave3SetActive){
			waveMinionTimer += Time.deltaTime;

			if (waveMinionTimer >= wave3spawnTime) {

				Wave3 [indexEnemy].transform.position = new Vector2 (spawnPoint.position.x, spawnPoint.position.y);	
				Wave3 [indexEnemy].SetActive (true);

				indexEnemy++;
				waveMinionTimer = 0;

				if (indexEnemy == Wave3.Length)
					wave3SetActive = false;					
			}
		}
	}
}
