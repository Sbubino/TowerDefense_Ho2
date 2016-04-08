﻿using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {

	public static AudioController instance;
	public AudioClip[] efx;
	public AudioClip[] soundTrack;
	public AudioSource Efx;
	public AudioSource SoundTrack;

	void Awake (){
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
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			WinTrack();
		}

	}
	public static AudioController GetInstance()
	{
		return instance;
	
	}


	public void ChillyEfx()
	{
		Efx.PlayOneShot(efx[0]);
	}

	public void PanCakeEfx()
	{
		Efx.PlayOneShot(efx[1]);
	}

	public void IceCreamEfx()
	{
		Efx.PlayOneShot(efx[2]);
	}

	public void MeatballsEfx()
	{
		Efx.PlayOneShot(efx[3]);
	}

	public void MenuTrack()
	{
		SoundTrack.loop = true;
		SoundTrack.clip = soundTrack[0];
		SoundTrack.Play();
		//SoundTrack.PlayOneShot(soundTrack[0]);
	}

	public void GameTrack()
	{
		SoundTrack.loop = true;
		SoundTrack.PlayOneShot(soundTrack[1]);
	}

	public void WinTrack()
	{
		StartCoroutine(WinCoroutine());
		/*SoundTrack.loop = false;
		SoundTrack.clip = soundTrack[2];
		SoundTrack.Play();
		if(SoundTrack. >= 0)
		{
			SoundTrack.clip = null;
		}
		if(SoundTrack.clip == null)
		{
			SoundTrack.loop = true;
			SoundTrack.clip = soundTrack[0];
			SoundTrack.Play();
		}*/
	}

	public void LoseTrack()
	{
		StartCoroutine(LoseCoroutine());
	}
	IEnumerator WinCoroutine()
	{
		SoundTrack.clip = soundTrack[2];
		SoundTrack.Play ();
		yield return new WaitForSeconds(SoundTrack.clip.length);
		SoundTrack.loop = true;
		SoundTrack.clip = soundTrack[0];
		SoundTrack.Play ();

	}

	IEnumerator LoseCoroutine()
	{
		SoundTrack.clip = soundTrack[3];
		SoundTrack.Play ();
		yield return new WaitForSeconds(SoundTrack.clip.length);
		SoundTrack.loop = true;
		SoundTrack.clip = soundTrack[0];
		SoundTrack.Play ();
		
	}


}
