using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

	GameObject[] childArray;
	public int minionSpawnIn;
	[HideInInspector]
	public int activator = 0;
	[HideInInspector]
	public int activatorSecondPoint = 0;


	int index = 0;
	float spawnTimer;

	void Awake(){
		spawnTimer = 10;
		//inserisco i minion nell'oggetto ondata
		GetChildInArray ();

	}

	void Update(){
		//tramite il game controller attivo l'ondata
		if(activator != 0)
		   MinionSpawn ();

		if(activatorSecondPoint != 0)
			MinionSpawn2nd ();
	}

	void GetChildInArray (){
		childArray = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			childArray [i] = transform.GetChild (i).gameObject;
		}
	}

	void MinionSpawn(){
		//spawno i minion in ogni certo periodo di tempo
		spawnTimer += Time.deltaTime;

		if (spawnTimer >= minionSpawnIn && index <= childArray.Length -1) {
			childArray [index].transform.position = GameController.instance.spawnPoint.position;
			childArray [index].SetActive (true);

			index++;
			spawnTimer = 0;
		}
	}
	void MinionSpawn2nd(){
		//spawno i minion in ogni certo periodo di tempo nel secondo spawn point
		spawnTimer += Time.deltaTime;
		
		if (spawnTimer >= minionSpawnIn && index <= childArray.Length -1) {
			childArray [index].transform.position = GameController.instance.spawnPoint2.position;
			childArray [index].SetActive (true);
			
			index++;
			spawnTimer = 0;
		}
	}
}
