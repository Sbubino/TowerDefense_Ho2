using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

	GameObject[] childArray;
	public int minionSpawnIn;
	[HideInInspector]
	public int activator = 0;

	int index = 0;
	float spawnTimer;

	void Awake(){
		GetChildInArray ();
	}

	void Update(){
		if(activator != 0)
		   MinionSpawn ();
	}

	void GetChildInArray (){
		childArray = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			childArray [i] = transform.GetChild (i).gameObject;
		}
	}

	void MinionSpawn(){
		spawnTimer += Time.deltaTime;

		if (spawnTimer >= minionSpawnIn && index <= childArray.Length -1) {
			childArray [index].transform.position = GameController.instance.spawnPoint.position;
			childArray [index].SetActive (true);

			index++;
			spawnTimer = 0;
		}
	}
}
