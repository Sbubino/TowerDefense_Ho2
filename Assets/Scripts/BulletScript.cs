using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float expDur;

	[HideInInspector]
	public float raggio;
	public float slowAmount;

	public int dam;
	public float speed;
	GameObject target;
	bool explosion;
	float timer=0;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<CircleCollider2D> ().radius = 0.1f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Debug.Log ("timer: "+timer);
		transform.position = Vector2.Lerp(transform.position , target.transform.position , speed * Time.deltaTime);
		if(explosion==true){
			timer+=Time.deltaTime;
		}
		if(TurretBehaviour.isExp == true){
			ExplosionVanish ();
		}

	}

	void OnTriggerEnter2D(Collider2D trig){
		if (trig.gameObject.tag == "Enemy" && TurretBehaviour.isFast == true) {
			if (trig.gameObject.CompareTag("Enemy"))
			{
				Debug.Log("hit");
				trig.gameObject.SendMessage("TakeDamage", dam);
				gameObject.SetActive(false);
			}
		}
		if (trig.gameObject.tag == "Enemy" && TurretBehaviour.isHeavy == true) {
			if (trig.gameObject.CompareTag("Enemy"))
			{
				Debug.Log("hit");
				trig.gameObject.SendMessage("TakeDamage", dam);
				gameObject.SetActive(false);
			}
		}
		if (trig.gameObject.tag == "Enemy" && TurretBehaviour.isSl == true) {
			if (trig.gameObject.CompareTag("Enemy"))
			{
				Debug.Log("hit");
				trig.gameObject.SendMessage("TakeDamage", dam);
				trig.gameObject.SendMessage("GetSlow",slowAmount);
				gameObject.SetActive(false);
			}
		}
		if (trig.gameObject.tag == "Enemy" && TurretBehaviour.isExp == true) {
			if (trig.gameObject.CompareTag("Enemy"))
			{
				Debug.Log("hit");
				gameObject.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
				SetRange ();
				trig.gameObject.SendMessage("TakeDamage", dam);
			//	float timer=0;

				explosion=true;

				/*if(timer>=expDur){			
					explosion=false;
					timer=0;
					gameObject.SetActive(false);
				}*/

			}
		}
	}


    public void SetTarget(GameObject tar)
    {
        target = tar;
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
	void SetRange(){
		gameObject.GetComponent<CircleCollider2D> ().radius = raggio;
	}
	void ExplosionVanish(){
		if(timer>=expDur){			
			explosion=false;
			timer=0;
			gameObject.SetActive(false);
		}
	}

}
