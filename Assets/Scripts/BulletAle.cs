using UnityEngine;
using System.Collections;

public class BulletAle : MonoBehaviour {
    public float m_Speed;
    public float m_Damage;
	public bool m_SlowBullet;
	public float m_SlowAmount;
	public float m_SlowTime;
	public bool m_AreaT;
	public float m_AreaExplosion;
	public LayerMask m_Enemy;
    
	GameObject target;
	GameObject spriteExplosion;
	GameObject sprite;

	float[] slow;

	void Awake(){
		slow = new float[2];
		sprite = transform.GetChild (0).gameObject;
		spriteExplosion = transform.GetChild (1).gameObject;

		if (m_SlowBullet) {
			slow[0] = m_SlowAmount;
			slow[1] = m_SlowTime;


		}



	}


	void OnEnable(){
		if (!m_AreaT)
			StartCoroutine ("Disable");
		else {
			spriteExplosion.SetActive (false);
			sprite.SetActive (true);
		}
	}

	void OnDisable(){
		spriteExplosion.SetActive (false);
		sprite.SetActive (true);

	}

    void FixedUpdate()
    {

		if(target != null)
        	transform.position = Vector2.Lerp(transform.position, target.transform.position, m_Speed * Time.deltaTime);
    }

	IEnumerator Disable(){
		yield return new WaitForSeconds (2);
		gameObject.SetActive (false);

	}

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            if(!m_AreaT)
           	 col.gameObject.SendMessage("TakeDamage", m_Damage);

			if(m_SlowBullet)
				col.gameObject.SendMessage("Slow", slow);

			if(m_AreaT){

				Collider2D[] enemyHit =  Physics2D.OverlapCircleAll(transform.position , m_AreaExplosion, m_Enemy);
				sprite.SetActive(false);
				spriteExplosion.SetActive(true);
				for(int i = 0; i < enemyHit.Length ; i++){
					if(enemyHit[i] != null){
							enemyHit[i].gameObject.SendMessage("TakeDamage", m_Damage);
					}

				}

			}
			if(!m_AreaT)
            	gameObject.SetActive(false);
			else
				StartCoroutine ("Disable");
        }
    }

    public void SetTarget(GameObject tar)
    {
        target = tar;
    }

    
	
}
