using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour {
    
    public UISlider ProgressBar;

	private int LevelToLoad;

	public float MoveSpeed;
	public GameObject[] Sheet;

	public float Timer;

	void Awake()
	{
        Time.timeScale = 1;
		Timer = Time.time + Timer;
		LevelToLoad = MenuController.NumberOfLevel;
		Destroy (GameObject.Find("MenuController"));
		//Debug.Log (LevelToLoad);
	}



  /*  IEnumerator Loading()
    {
        AsyncOperation async = Application.LoadLevelAsync(LevelToLoad);
        while(!async.isDone)
        {
      //      ProgressBar.value = async.progress;

            yield return null;
        }
        yield return async;

    }
*/

    void Update () 
	{
		Move();
		if(Time.time >= Timer)
		{
			loadLevel();
		}

    //    StartCoroutine(Loading());
    }
	void Move()
	{
		Sheet[0].transform.Translate(Sheet[0].transform.right * MoveSpeed * Time.deltaTime);
		Sheet[1].transform.Translate(Sheet[1].transform.right * MoveSpeed * Time.deltaTime);
		Sheet[2].transform.Translate(Sheet[2].transform.right * MoveSpeed * Time.deltaTime);
		Sheet[3].transform.Translate(Sheet[3].transform.right * MoveSpeed * Time.deltaTime);
	}


	void loadLevel()
	{
		Application.LoadLevel(LevelToLoad);
	}
    /*IEnumerator Loading()
    {
        AsyncOperation myasync = Application.LoadLevelAsync("ProvaTexture");
            Debug.Log(myasync.progress);
            yield return myasync;
        }
    }*/
}
