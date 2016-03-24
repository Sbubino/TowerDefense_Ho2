using UnityEngine;
using System.Collections;

public class BulletAle : MonoBehaviour {
	public float m_TimeBeforeDisable;
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
	SpriteRenderer spriteExplosionAlpha;
	GameObject sprite;
	Collider2D col;

	float[] slow;

	void Awake(){
		col = GetComponent<CircleCollider2D> ();
		slow = new float[2];
		if (m_AreaT) {
			sprite = transform.GetChild (0).gameObject;
			spriteExplosion = transform.GetChild (1).gameObject;
			spriteExplosionAlpha = transform.GetChild (1).gameObject.GetComponent<SpriteRenderer>();

		}

		if (m_SlowBullet) {
			slow[0] = m_SlowAmount;
			slow[1] = m_SlowTime;


		}



	}

	void Update(){


	}


	void OnEnable(){
		if (!m_AreaT)
			StartCoroutine ("Disables");
		else {
			spriteExplosion.SetActive(false);
			sprite.SetActive (true);
			col.enabled = true;
			//spriteExplosionAlpha.color = jesoo;

		}
	}
	


    void FixedUpdate()
    {
//		Debug.Log (spriteExplosionAlpha.color.a);
		if(target != null)
        	transform.position = Vector2.Lerp(transform.position, target.transform.position, m_Speed * Time.deltaTime);
    }

	public void Disable(){
//		yield return new WaitForSeconds (m_TimeBeforeDisable);
		gameObject.SetActive (false);

	}

	IEnumerator Disables(){
		yield return new WaitForSeconds (m_TimeBeforeDisable);
		gameObject.SetActive (false);
	}


    void OnTriggerEnter2D (Collider2D col)
    {
		if (col.gameObject.CompareTag ("Enemy")) {
			if (!m_AreaT)
				col.gameObject.SendMessage ("TakeDamage", m_Damage);

			if (m_SlowBullet)
				col.gameObject.SendMessage ("Slow", slow);

			if (m_AreaT)
				Explode ();


			if(!m_AreaT)
				gameObject.SetActive (false);

        
		}
	}

    public void SetTarget(GameObject tar)
    {
        target = tar;
    }

	void Explode(){
		
		Collider2D[] enemyHit = Physics2D.OverlapCircleAll (transform.position, m_AreaExplosion, m_Enemy);
		col.enabled = false;
		sprite.SetActive (false);
		spriteExplosion.SetActive (true);

	
		for (int i = 0; i < enemyHit.Length; i++) {
			if (enemyHit [i] != null) {
				enemyHit [i].gameObject.SendMessage ("TakeDamage", m_Damage);
			}
		}
	}

    
	
}
