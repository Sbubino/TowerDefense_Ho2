﻿using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public GameObject explosionRange;
	public float expDur;
	[HideInInspector]
	public int dam;
	[HideInInspector]
	public float raggio;
	public float slowAmount;
	public GameObject target;
	public float speed;



	// Use this for initialization
	void Start () {
		explosionRange.SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		SetRange ();
		transform.position = Vector2.Lerp(transform.position , target.transform.position , speed * Time.deltaTime);

	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag=="Enemy" && TurretBehaviour.isExp==true){

			explosionRange.SetActive(true);
			gameObject.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
			float timer=0;
			timer+=Time.deltaTime;
			if(timer>=expDur){

				explosionRange.SetActive (false);
				timer=0;
			}
		}
		if (col.gameObject.tag == "Enemy" && TurretBehaviour.isFast == true) {
			col.gameObject.GetComponent<Enemy>().TakeDamage(dam);
			Destroy (this.gameObject);
		}
		if (col.gameObject.tag == "Enemy" && TurretBehaviour.isHeavy == true) {
			col.gameObject.GetComponent<Enemy>().TakeDamage(dam);
			Destroy (this.gameObject);
		}
		if (col.gameObject.tag == "Enemy" && TurretBehaviour.isSl == true) {
			col.gameObject.GetComponent<Enemy>().TakeDamage(dam);
			col.gameObject.GetComponent<Enemy>().GetSlow(slowAmount);
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D trig){
		if (trig.gameObject.tag == "Enemy") {
			trig.gameObject.GetComponent<Enemy>().TakeDamage(dam);
		}
	}

    public void SetTarget(GameObject tar)
    {
        target = tar;
    }

	void SetRange(){
		explosionRange.GetComponent<CircleCollider2D> ().radius = raggio;
	}
	public void GetExpDur(float t){
		expDur = t;
	}
	public void GetRange(float r){
		raggio = r;
	}
	public void SetDamage(int amount){
		dam=amount;
	}
	public void SetSlow(float slowVal){
		slowAmount=slowVal;
	}
}