using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour {
    
    public UISlider ProgressBar;

	private int LevelToLoad;

	void Awake()
	{
		LevelToLoad = MenuController.NumberOfLevel;
		Destroy (GameObject.Find("MenuController"));
		Debug.Log (LevelToLoad);
	}



    IEnumerator Loading()
    {
        AsyncOperation async = Application.LoadLevelAsync(LevelToLoad);
        while(!async.isDone)
        {
      //      ProgressBar.value = async.progress;

            yield return null;
        }
        yield return async;

    }


    void Update () 
	{
	
        StartCoroutine(Loading());
    }

    /*IEnumerator Loading()
    {
        AsyncOperation myasync = Application.LoadLevelAsync("ProvaTexture");
            Debug.Log(myasync.progress);
            yield return myasync;
        }
    }*/
}
