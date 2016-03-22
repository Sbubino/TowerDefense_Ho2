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
       enemyInRange = new Collider2D[10];
        range = GetComponent<CircleCollider2D>().radius;
       
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
       
    }

  

    void OnTriggerStay2D(Collider2D trig)
    {


        if (trig.gameObject.tag == "Enemy")
        {
            Debug.Log("qui");
            SetTarget();           


            Vector2 vectorToTarget = target.transform.position - sprite.transform.position;
			float angle= Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x)*Mathf.Rad2Deg;
			Quaternion q=Quaternion.AngleAxis (angle,Vector3.forward);
			sprite.transform.rotation = Quaternion.Slerp (sprite.transform.rotation,q, Time.deltaTime*rotationSpeed);
            

            if (timer >= fireRate)
            {
                Shoot();
                timer = 0;
            }

        }

    }

    void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Enemy" && trig.gameObject.Equals(target))
            target = null;
    }




    void SetTarget()
    {
         Physics2D.OverlapCircleNonAlloc(transform.position, range, enemyInRange,m_EnemtLayer);

        for(int i = 0; i < enemyInRange.Length; i++)
        {
            if (enemyInRange[i] != null)
            {
                if (target == null)
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


    



   

    void Shoot()
    {
        if (bulletPoolIndex < bulletPool.Length - 1)
        {
            if (!bulletPool[bulletPoolIndex].activeInHierarchy)
            {
                bulletPool[bulletPoolIndex].transform.position = spawnPoint.transform.position;
                bulletPool[bulletPoolIndex].SetActive(true);
                bulletPool[bulletPoolIndex].SendMessage("SetTarget", target);

            }
            else
                bulletPoolIndex++;
        }
        else
            bulletPoolIndex = 0;

    }


            
    

}
