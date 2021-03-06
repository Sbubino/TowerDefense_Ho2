﻿using UnityEngine;
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

    public GameObject lastWave;
	public UILabel waveNuber;
	public UILabel waveTimer;
    public GameObject nextwavein;
    public GameObject wavenumbert;   
    public UILabel startGamet;

    public GameObject nextWave;

	public GameObject pauseScreen;
	public GameObject winScreen;
	public GameObject loseScreen;

	public GameObject sureQuit;
	public GameObject sureRetry;

	[HideInInspector]
	public int waveIndex = 0;
	[HideInInspector]
	public bool win = false;
	[HideInInspector]
	public bool lose = false;

	int waveLenght;
    public bool gameStarted;
	float timer = 0;

    float nextWaveTimer;

    DialogoController dialogo;


    void Awake(){

		instance = this;
        dialogo = FindObjectOfType<DialogoController>();

        Time.timeScale = 1;

        nextWaveTimer = 10;

        gameStarted = false;		

		play.SetActive (false);
		xx2.SetActive (false);

		pauseScreen.SetActive (false);
		winScreen.SetActive (false);
		loseScreen.SetActive (false);


        nextwavein.SetActive(false);
        wavenumbert.SetActive(false);
        lastWave.SetActive(false);
}

	void Update () {

		EnergyBar ();
		FlashWave ();
		TakeNextWave ();
		EndLevel ();

    }

    void TakeNextWave() {


        waveLenght = Spawnpoint.instance.wave.Length - waveIndex;

        nextWaveTimer += Time.deltaTime;

        if (waveLenght == 0)
        {
            waveNuber.text = null;
            waveTimer.text = null;

            nextwavein.SetActive(false);
            wavenumbert.SetActive(false);

            lastWave.SetActive(true);
        }

        else {
            waveNuber.text = waveLenght.ToString();
            waveTimer.text = (Mathf.RoundToInt(GameController.instance.nextWaveIn) - Mathf.RoundToInt(GameController.instance.waveTimer)).ToString();
        }

        if (!gameStarted) { 
            waveTimer.text = null;
            waveNuber.text = null;
         }

	}

    public void NextWavebutton()
    {
       
            if (nextWaveTimer >= 5)
            {

                float amount = (Mathf.RoundToInt(GameController.instance.nextWaveIn) - Mathf.RoundToInt(GameController.instance.waveTimer));

                if (GameController.instance.indexWave <= GameController.instance.maxWaveNumber)
                {
                    GameController.instance.waveTimer += GameController.instance.nextWaveIn;
                    GameController.instance.currentEnergy += amount * 2;
                }

                nextWaveTimer = 0;
            }
        

         if (!gameStarted)
            {
                gameStarted = true;

                nextwavein.SetActive(true);
                wavenumbert.SetActive(true);
                startGamet.text = null;

                //Time.timeScale = 1;
            }
        

    }

    void EnergyBar(){
		energyValue.text = "" + GameController.instance.currentEnergy + " / " + GameController.instance.maxEnergy;
		energyBar.value = GameController.instance.currentEnergy / GameController.instance.maxEnergy;
	}

	public void Pause(){
		if (gameStarted) {
			Time.timeScale = 0;		

			//play.SetActive (true);
			//pause.SetActive (false);

		
			pauseScreen.SetActive (true);

		}

        if (!gameStarted)
            pauseScreen.SetActive(true);

    }

    public void Play()
    {
        if (gameStarted)
        {
            Time.timeScale = 1;

            play.SetActive(false);
            pause.SetActive(true);

            if (xx2.activeSelf)
            {
                x2.SetActive(true);
                xx2.SetActive(false);
                dialogo.SendMessage("GeneralInfo", "Ok then.\n Let's go back to regular speed");
            }


            pauseScreen.SetActive(false);


        }

        if (!gameStarted)
            pauseScreen.SetActive(false);
    }



	public void X2 (){
		if (gameStarted) {
			Time.timeScale = 2;
			x2.SetActive (false);
			xx2.SetActive (true);
            dialogo.SendMessage("GeneralInfo", "The game was too slow for you?\n Speed is  now doubled!");
			//pause.SetActive (false);
			//play.SetActive (true);
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

	void EndLevel(){

		
		if (win) {
			winScreen.SetActive (true);

			Time.timeScale = 0;

		}
		if (lose) {
			loseScreen.SetActive (true);


			Time.timeScale = 0;
		}

	}




	public void ToMainMenu(){

		Application.LoadLevel ("Menù");
	}

	public void Retry(){

		Application.LoadLevel (Application.loadedLevel);
	}


	public void SureQuit(){

		sureQuit.SetActive (true);

	}

	public void SureQuit1(){

		sureQuit.SetActive (false);

	}

	public void SureRetry(){

		sureRetry.SetActive (true);

	}

	public void SureRetry1(){

		sureRetry.SetActive (false);

	}
}