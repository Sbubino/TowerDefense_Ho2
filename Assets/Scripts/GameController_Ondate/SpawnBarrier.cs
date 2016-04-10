using UnityEngine;
using System.Collections;

public class SpawnBarrier : MonoBehaviour {

	public GameObject spawnPoint;
	public int minionForNextSpawnPoint;
	[HideInInspector]
	public int currentMinionPassed;
    Animator piede;


	void Awake () {
        //spawnPoint.SetActive (false);	
        piede = transform.GetChild(0).GetComponent<Animator>() ;
	}

	void Update(){
		if (currentMinionPassed >= minionForNextSpawnPoint) {
            //spawnPoint.SetActive (true);
            spawnPoint.GetComponent<Spawnpoint>().Open();
			this.gameObject.SetActive (false);
		}		
	}

	void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Enemy")
        { currentMinionPassed++;
            if (!piede.GetBool("Crossed"))
               { piede.SetBool("Crossed", true);
                Invoke("StopAnim", 0.5f); }
        }
	}

    void StopAnim()
    {
        piede.SetBool("Crossed", false);

    }
}
