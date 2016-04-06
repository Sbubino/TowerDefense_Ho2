using UnityEngine;
using System.Collections;

public class Spawnpoint : MonoBehaviour {
	public static Spawnpoint instance;

	public GameObject waveHolder;

	[HideInInspector]
	public GameObject[] wave;

	int maxWaveNumber;


	void Awake(){
		instance = this;


		WaveBuild ();
	}

	void Update(){
		GameController.instance.WaveControl (wave.Length);
	}


	public void StartWave(int index){
		
		wave[index].GetComponent<Wave> ().StartWave (this.gameObject);
	}


	void WaveBuild(){
		//riempio l'array wave con tutte le ondate in scena e imposto un valore wavetimer alto per far partire subuto la prima ondata al play
		wave = new GameObject[waveHolder.transform.childCount];
		for (int i = 0; i < waveHolder.transform.childCount; i++) {
			wave [i] = waveHolder.transform.GetChild (i).gameObject;
		}
	}
}