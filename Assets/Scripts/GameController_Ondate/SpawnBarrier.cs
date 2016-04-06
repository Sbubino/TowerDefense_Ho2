using UnityEngine;
using System.Collections;

public class SpawnBarrier : MonoBehaviour {

	public GameObject spawnPoint;
	public int minionForNextSpawnPoint;
	[HideInInspector]
	public int currentMinionPassed;


	void Awake () {
		spawnPoint.SetActive (false);	
	}

	void Update(){
		if (currentMinionPassed >= minionForNextSpawnPoint) {
			spawnPoint.SetActive (true);
			this.gameObject.SetActive (false);
		}		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Enemy")
			currentMinionPassed ++;
	}
}
