using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour {
    public GameObject m_BulletPrefab;
    public float rotationSpeed;
    public float fireRate;
    
    public LayerMask m_EnemtLayer;

    GameObject[] bulletPool;
    int bulletPoolIndex;
    GameObject target;
    float range;
    Collider2D[] enemyInRange;
    int indexOfTarget;
   
    GameObject sprite;
    GameObject spawnPoint;

    float timer;

    void Awake()
    {
      	enemyInRange = new Collider2D[20];
        range = GetComponent<CircleCollider2D>().radius - 0.5f;
       
        target = null;
        sprite = transform.GetChild(0).gameObject;
        spawnPoint = transform.GetChild(0).GetChild(0).gameObject;
        indexOfTarget = 0;
        bulletPool = new GameObject[20];
        for(int i = 0; i< bulletPool.Length; i++)
        {
            bulletPool[i] = Instantiate(m_BulletPrefab, Vector3.zero, m_BulletPrefab.transform.rotation) as GameObject;
            bulletPool[i].SetActive(false);
        }
        bulletPoolIndex = 0;
    }
        
    void Update()
    {
        timer += Time.deltaTime;

		//Debug.DrawRay (transform.position,target.transform.position - transform.position );

       
    }


    void OnTriggerStay2D(Collider2D trig)
    {


        if (trig.gameObject.tag == "Enemy")
        {
			SetTarget();


//			Vector2 vectorToTarget = target.transform.position - sprite.transform.position;
//			float angle= Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x)*Mathf.Rad2Deg;
//			Quaternion q=Quaternion.AngleAxis (angle,Vector3.forward);
//			sprite.transform.rotation = Quaternion.Slerp (sprite.transform.rotation,q, Time.deltaTime*rotationSpeed);
            

            if (timer >= fireRate)
            {
                Shoot();
               
            }

        }

    }

    void OnTriggerExit2D(Collider2D trig)
    {
		if (trig.CompareTag ("Enemy")) {
			target = null;
			CancelEnemyArray(trig);
			SetTarget ();
		}

    }




    void SetTarget()
    {
        Physics2D.OverlapCircleNonAlloc(transform.position, range, enemyInRange,m_EnemtLayer);

        for(int i = 0; i < enemyInRange.Length; i++)
        {
            if (enemyInRange[i] != null)
            {
                if (target == null || !target.activeInHierarchy)
                {
                    target = enemyInRange[i].gameObject;
                }
                else if (enemyInRange[i].gameObject.GetComponent<Enemy>().DistToCore() < target.GetComponent<Enemy>().DistToCore())
                {
                    target = enemyInRange[i].gameObject;
                }
            }
        }


    }

	void CancelEnemyArray(Collider2D target){
		for (int i = 0; i < enemyInRange.Length; i++) {
			if(enemyInRange[i] != null){	
				if( enemyInRange[i].Equals(target))
					enemyInRange[i] = null;
			}

		}
	}
    



   

    void Shoot()
    {
       if (target != null && target.activeInHierarchy) {

			if (bulletPoolIndex < bulletPool.Length - 1) {
				if (!bulletPool [bulletPoolIndex].activeInHierarchy) {
					bulletPool [bulletPoolIndex].transform.position = spawnPoint.transform.position;
					bulletPool [bulletPoolIndex].SetActive (true);
					bulletPool [bulletPoolIndex].SendMessage ("SetTarget", target);
					timer = 0;

				} else
					bulletPoolIndex++;
			} else
				bulletPoolIndex = 0;
		} else if (target != null) {
			CancelEnemyArray(target.GetComponent<Collider2D>());
			SetTarget ();

		}
    }


            
    

}
