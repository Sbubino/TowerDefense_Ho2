using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour {
    public GameObject m_BulletPrefab;
    public float rotationSpeed;
    public float fireRate;

    GameObject[] bulletPool;
    int bulletPoolIndex;
    GameObject target;

    GameObject[] targetsInRange;
    int indexOfTarget;
    float[] distanceOfTargets;

    GameObject sprite;
    GameObject spawnPoint;

    float timer;

    void Awake()
    {
        target = null;
        sprite = transform.GetChild(0).gameObject;
        spawnPoint = transform.GetChild(1).gameObject;
        targetsInRange = new GameObject[10];
        indexOfTarget = 0;
        distanceOfTargets = new float[10];
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
            indexOfTarget = CheckEnemy(trig.gameObject);
            if(indexOfTarget == -1)
            {
                AddEnemy(trig.gameObject);
            }

            if (target == null)
                SetTarget(trig.gameObject);
            else if (!target.activeInHierarchy)
                SetTarget(trig.gameObject);


            Vector2 vectorToTarget = target.transform.position - transform.position;
			float angle= Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x)*Mathf.Rad2Deg;
			Quaternion q=Quaternion.AngleAxis (angle,Vector3.forward);
			transform.rotation=Quaternion.Slerp (transform.rotation,q, Time.deltaTime*rotationSpeed);


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


    void SetTarget(GameObject enemy)
    {
        int tileValue = 0;
        int index = -1;
        if (target == null || !target.activeInHierarchy)
        {
            for(int i = 0; i < targetsInRange.Length; i++)
            {
                if(targetsInRange[i].GetComponent<Enemy>().ReturnTileValue() > tileValue)
                {
                    index = i;
                    tileValue = targetsInRange[i].GetComponent<Enemy>().ReturnTileValue();
                }
            }
        }

        if (index == -1)
            target = enemy;
        else
            target = targetsInRange[index].gameObject;
    }


    void AddEnemy(GameObject enemy)
    {
        for (int i = 0; i < targetsInRange.Length; i++)
            {
              if(targetsInRange[i] == null)
                {
                   targetsInRange[i] = enemy;
                }
            }    
        
    }



    int CheckEnemy(GameObject enemy)
    {
        for(int i = 0; i < targetsInRange.Length; i++)
        {
            if (targetsInRange[i] != null && targetsInRange[i].Equals(enemy))
                return i;
        }

        return -1;
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
