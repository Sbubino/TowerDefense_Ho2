using UnityEngine;
using System.Collections;

public class PowerBullet : Bullet {
	public float RangeBounce = 1;
	public int NumBounce  = 2;
	public LayerMask m_EnemyLayer;

	GameObject[] targets;
	int bounce = 0;


	protected override void OnEnable ()
	{
		base.OnEnable();
		bounce = 0;
	}


	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.CompareTag ("Enemy")) 
		{
			col.gameObject.SendMessage ("TakeDamage", damage);
			if(bounce < NumBounce)
				Bounce();
			else
				gameObject.SetActive (false);			
			AudioController.instance.MeatballsEfx();
		}
	}

	/*public void SetTargets(GameObject[] tar)
	{

		targets = new GameObject[tar.Length];
		for (int i = 0; i < tar.Length; i++) {
			targets[i] = tar[i];
		}
	}*/


	void Bounce()
	{
		bounce ++;

		Collider2D[] temp =  Physics2D.OverlapCircleAll (transform.position, RangeBounce, m_EnemyLayer);
		targets = new GameObject[temp.Length];
		for (int i = 0; i < temp.Length; i++) {
			if(temp[i].gameObject.activeInHierarchy)
				targets[i] = temp[i].gameObject;
		}
		if (targets.Length > 1) {
			for (int i = 0; i < targets.Length; i++) {	
				if (targets [i].activeInHierarchy && targets [i] != target)
					target = targets [i];
				else
					gameObject.SetActive (false);
			}
		} else
			gameObject.SetActive (false);
	}
}
