using UnityEngine;
using System.Collections;

public class AreaBullet : Bullet {

	float areaExplosion;
	LayerMask enemyLayer;
	GameObject sprite;
	GameObject spriteExplosion;
	Collider2D col;
	//Variabili per Explode
	bool explose;


	void Awake(){
		sprite = transform.GetChild (0).gameObject;
		spriteExplosion = transform.GetChild (1).gameObject;
		col = GetComponent<CircleCollider2D> ();
	}

	protected override void OnEnable()
	{
        base.OnEnable();
		spriteExplosion.SetActive(false);
		sprite.SetActive (true);
		col.enabled = true;
		explose = false;
	}

	public override void FixedUpdate()
	{
		if(target != null && !explose)
			transform.position = Vector2.Lerp(transform.position, target.transform.position, m_Speed * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.CompareTag ("Enemy")) 
		{
            StopAllCoroutines();
			Explode ();		
			AudioController.instance.ChillyEfx();
		}
	}

	void Explode(){
		explose = true;
		Collider2D[] enemyHit = Physics2D.OverlapCircleAll (transform.position, areaExplosion, enemyLayer );
		col.enabled = false;
		sprite.SetActive (false);
		spriteExplosion.transform.position = target.transform.position;
		spriteExplosion.SetActive (true);
		
		
		for (int i = 0; i < enemyHit.Length; i++) {
			if (enemyHit [i] != null) {
				enemyHit [i].gameObject.SendMessage ("TakeDamage", damage);
			}
		}
	}


	public void SetValue(float areaExplosionTemp, LayerMask enemyLayerTemp){
		areaExplosion = areaExplosionTemp;
		enemyLayer = enemyLayerTemp;
	}

	public void Disable()
	{
		gameObject.SetActive (false);	
	}
	



}
