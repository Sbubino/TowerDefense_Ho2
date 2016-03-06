using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public GameObject explosionRange;
	public float expDur;
	[HideInInspector]
	public int dam;
	[HideInInspector]
	public float raggio;



	// Use this for initialization
	void Start () {
		explosionRange.SetActive (false);


	}
	
	// Update is called once per frame
	void Update () {
		SetRange ();
	}
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag=="Enemy" && TurretBehaviour.isExp==true){
			Debug.Log ("collisione avvenuta");
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
			Destroy (gameObject);
		}
	}
	/*void OnTriggerEnter2D(Collider2D trig){
		if (trig.gameObject.tag == "Enemy") {
			trig.gameObject.GetComponent<Enemy>().TakeDamage(dam);

		}

	}*/
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
}
