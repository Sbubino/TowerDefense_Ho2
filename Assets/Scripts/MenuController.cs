﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MenuController : MonoBehaviour {
	public int nextLevel;
	[HideInInspector]
    public static int NumberOfLevel;
	public UIPanel start;
	public UIPanel Credits;
	public float TimerCredits;
	private bool StartTimer = false;
	private float BaseTimer = 0.00f;


	void Awake()
	{
        Time.timeScale = 1;

        DontDestroyOnLoad(this);

	}

	void Update()
	{
		if (StartTimer == true) 
		{

			if(Time.time >= BaseTimer)
			{
				start.gameObject.SetActive(true);
				StartTimer = false;
			}
		}
	}

    public void Level1()
    {
        Application.LoadLevel("Loading");
		NumberOfLevel = 1;
    }
    public void Level2()
    {
		Application.LoadLevel("Loading");
		NumberOfLevel = 2;
    }
    public void Level3()
    {
		Application.LoadLevel("Loading");
		NumberOfLevel = 3;
    }
    public void Quit()
    {
        Application.Quit();
    }
	public void setActive()
	{
		start.gameObject.SetActive (false);

	}
	public void EndCredit()
	{
		StartTimer = true;
		BaseTimer = Time.time + TimerCredits;
	}
	public void NextLevel()
	{
		NumberOfLevel = nextLevel;
		Application.LoadLevel ("Loading");
	}
	public void MenuLevel()
	{
		NumberOfLevel = 0;
		Application.LoadLevel("Loading");
	}
}
