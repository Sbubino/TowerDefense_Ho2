using UnityEngine;
using System.Collections;

public class Spawnpoint : MonoBehaviour {
	public static Spawnpoint instance;

	public GameObject waveHolder;

	[HideInInspector]
	public GameObject[] wave;

	int maxWaveNumber;
    public bool open;
    public Sprite sprites;
  

	void Awake(){
		instance = this;
        if (open)
            GetComponent<SpriteRenderer>().sprite = sprites;

        GetComponent<BoxCollider2D>().isTrigger = true;
        WaveBuild();
	}

	void Update(){
        if (GuiController.instance.gameStarted)
        {
            if (open)            
                GameController.instance.WaveControl(wave.Length);
            
        }
	}


    public void Open()
    {
        open = true;
        GetComponent<SpriteRenderer>().sprite = sprites;


    }
    public void StartWave(int index){
        if (open)
		wave[index].GetComponent<Wave> ().StartWave (this.gameObject);
	}


	void WaveBuild(){       

        wave = new GameObject[waveHolder.transform.childCount];
		for (int i = 0; i < waveHolder.transform.childCount; i++) {
			wave [i] = waveHolder.transform.GetChild (i).gameObject;
		}
	}
}