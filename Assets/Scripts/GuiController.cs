using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GuiController : MonoBehaviour {

	public static GuiController instance;

	public UILabel energyValue;
	public UISlider energyBar;

	public GameObject play;
	public GameObject pause;

	public GameObject x2;
	public GameObject xx2;

	public GameObject NextWaveFlash;

	public UILabel waveNuber;
	public UILabel waveTimer;
	public GameObject bossIcon;
	public GameObject normalIcon;

	[HideInInspector]
	public int waveIndex = 0;


	int waveLenght;
	bool gameStarted;
	float timer = 0;


	void Awake(){
		instance = this;
		Time.timeScale = 0;
	  
		gameStarted = false;
		bossIcon.SetActive (false);

		play.SetActive (false);
		xx2.SetActive (false);


	}

	void Update () {
		EnergyBar ();


		FlashWave ();

		TakeNextWave ();	
	}

	void TakeNextWave(){
		
	/*	if (GameObject.FindWithTag ("Boss") != null) {
			normalIcon.SetActive (false);
			bossIcon.SetActive (true);
		}*/

		waveLenght = Spawnpoint.instance.wave.Length - waveIndex;
		waveNuber.text =  waveLenght.ToString();
	

	}

	void EnergyBar(){
		energyValue.text = "" + GameController.instance.currentEnergy + " / " + GameController.instance.maxEnergy;
		energyBar.value = GameController.instance.currentEnergy / GameController.instance.maxEnergy;
	}

	public void Pause(){
		if (gameStarted) {
			Time.timeScale = 0;		

			play.SetActive (true);
			pause.SetActive (false);
		}
	}

	public void Play(){
		if (gameStarted) {
			Time.timeScale = 1;		

			play.SetActive (false);
			pause.SetActive (true);

			x2.SetActive (true);
			xx2.SetActive (false);
		}
	}


	public void X2 (){
		if (gameStarted) {
			Time.timeScale = 2;
			x2.SetActive (false);
			xx2.SetActive (true);

			pause.SetActive (false);
			play.SetActive (true);
		}
	}


	public void NextWavebutton(){
		GameController.instance.waveTimer += 100; 
		if (!gameStarted) {
			gameStarted = true;
			Time.timeScale = 1;
		}
		
	}
		

	void FlashWave(){
		if (!gameStarted) {
			timer += Time.unscaledDeltaTime;

			if (timer >= 0.5) {
			
				NextWaveFlash.SetActive (false);

				if (timer >= 1) {
				
					NextWaveFlash.SetActive (true);
	
					timer = 0;
				}
			}
		}
	}
 

}
