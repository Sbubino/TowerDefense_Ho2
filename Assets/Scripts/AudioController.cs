using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	private static AudioController instance;

	void Awakw (){
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public static AudioController GetInstance()
	{
		return instance;
	}

}
