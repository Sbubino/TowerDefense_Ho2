using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GuiController : MonoBehaviour {
	
	public UILabel energyValue;
	public UISlider energyBar;

	public GameObject play;
	public GameObject pause;

	public GameObject x2;
	public GameObject xx2;

	bool gameStarted;


	void Awake(){
		Time.timeScale = 0;
	  
		gameStarted = false;

		play.SetActive (false);
		xx2.SetActive (false);
	}

	void Update () {
		EnergyBar ();

		Debug.Log (Time.timeScale);
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
		
}
