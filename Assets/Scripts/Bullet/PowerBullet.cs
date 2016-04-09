﻿using UnityEngine;
using System.Collections;

public class PowerBullet : Bullet {

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.CompareTag ("Enemy")) 
		{
			col.gameObject.SendMessage ("TakeDamage", damage);
			gameObject.SetActive (false);			
			AudioController.instance.MeatballsEfx();
		}
	}
}
