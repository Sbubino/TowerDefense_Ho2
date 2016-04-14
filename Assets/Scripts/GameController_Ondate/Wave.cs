using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

	GameObject[] childArray;
	public float minionSpawnIn;
	[HideInInspector]
	public int activator = 0;

	GameObject spawnPoint;
	int index = 0;
	float spawnTimer;


	void Awake(){
		spawnTimer = 10;
		//inserisco i minion nell'oggetto ondata
		GetChildInArray ();
	}
		
	void Update(){
		if (activator != 0) {
			MinionSpawn (spawnPoint);
		}
	}

	public void StartWave(GameObject spawn){
		activator++;
		spawnPoint = spawn;

		GuiController.instance.waveIndex++;
	}

	void GetChildInArray (){
		childArray = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			childArray [i] = transform.GetChild (i).gameObject;
		}
	}

	void MinionSpawn(GameObject spawn){
		//spawno i minion in ogni certo periodo di tempo
		spawnTimer += Time.deltaTime;

		if (spawnTimer >= minionSpawnIn && index <= childArray.Length -1) {
			childArray [index].transform.position = spawn.transform.position;
			childArray [index].SetActive (true);

			index++;
			spawnTimer = 0;
		}
	}
}
