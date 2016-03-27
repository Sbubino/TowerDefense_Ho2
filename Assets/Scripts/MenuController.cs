using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MenuController : MonoBehaviour {
	
    public int NumberOfLevel;
	public UIPanel start;
	public UIPanel Credits;

	// Update is called once per frame
	void Update () {

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
		start.gameObject.SetActive (true);

	}
   
}
