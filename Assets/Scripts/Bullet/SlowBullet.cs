using UnityEngine;
using System.Collections;

public class SlowBullet : Bullet {
	float[] slow = new float[2];

	void OnEnable(){
		StartCoroutine ("Disables");
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.CompareTag ("Enemy")) {

            col.gameObject.SendMessage("Slow", slow);

            col.gameObject.SendMessage ("TakeDamage", damage);
	

				gameObject.SetActive (false);
			AudioController.instance.IceCreamEfx();
		}
	}

	IEnumerator Disables(){
		yield return new WaitForSeconds (2);
		gameObject.SetActive (false);
	}

	public void SetValueSlow(float slowAmount, float slowTime){
		slow [0] = slowAmount;
		slow [1] = slowTime;
	}
}
