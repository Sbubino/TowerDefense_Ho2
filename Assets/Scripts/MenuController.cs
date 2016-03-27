using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MenuController : MonoBehaviour {
	
    public int NumberOfLevel;
	public UIPanel start;
	public UIPanel Credits;
	public float TimerCredits;
	private bool StartTimer = false;
	private float BaseTimer = 0.00f;

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
        Application.LoadLevel(1);
    }
    public void Level2()
    {
        Application.LoadLevel(2);
    }
    public void Level3()
    {
        Application.LoadLevel(3);
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
   
}
