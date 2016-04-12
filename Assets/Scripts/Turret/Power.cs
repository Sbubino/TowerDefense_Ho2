using UnityEngine;
using System.Collections;

public class Power : Turret {
	PowerBullet[] bullet;

    protected override void Awake()
    {
        base.Awake();
        typeName = "Heavy meatball";
		bullet = new PowerBullet[bulletPool.Length];
		for (int i = 0; i < bullet.Length; i++) {
			bullet[i] =  bulletPool[i].GetComponent<PowerBullet>();


		}
    }

	/*public override void Shoot ()
	{
		if (target != null && target.activeInHierarchy) 
			{
				
				if (bulletPoolIndex < bulletPool.Length - 1) 
				{
					
					if (!bulletPool [bulletPoolIndex].activeInHierarchy) 
					{
						bulletPool [bulletPoolIndex].transform.position = transform.position;
						bulletPool [bulletPoolIndex].SetActive (true);
						bulletPool [bulletPoolIndex].SendMessage ("SetTarget", target);
						timer = 0;
						SetEnemyOnRange();
						
						
					} 
					else
						
						bulletPoolIndex++;
				} 
				else
					
					bulletPoolIndex = 0;
			} 
			else if (target != null) 
			{
				CancelEnemyArray(target.GetComponent<Collider2D>());
				SetTarget ();
			}
	}*/

	/*void SetEnemyOnRange(){
		int index = 0;
		for (int i = 0; i < enemyInRange.Length; i++) {
			if(enemyInRange[i] != null && enemyInRange[i].gameObject.activeInHierarchy){
				index ++;
			}
		}
		Debug.Log (index);
		GameObject[] temp = new GameObject [index];
		for (int i = 0; i < temp.Length; i++) {
			if(enemyInRange[i] != null)
				temp[i] = enemyInRange[i].gameObject;
		}

		bullet [bulletPoolIndex].SetTargets (temp);

	}*/


}
