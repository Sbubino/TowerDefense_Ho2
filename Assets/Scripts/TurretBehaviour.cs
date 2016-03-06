using UnityEngine;
using System.Collections;


public class TurretBehaviour : MonoBehaviour {
	public float rotationSpeed= 5.0f;
	public GameObject bulletPrefab;
	public Transform spawnPoint;

	public float forceToBullet;

	public float fireRate=1;
	public float range=1;

	public int turretDamage=1;
	public static int damageRefer;
	public int turretCost;
	public int upgradeCost;
	public int sellEarn;
	public float slowAmmount;
	public float explosionRange;
	public float expDuration=0.5f;

	public bool fastT, heavyT, slowT, explosionT;
	public static bool isFast,isHeavy,isSl, isExp;
	public bool upgrade1=false, upgrade2=false;
	private float timer;




	// Use this for initialization
	void Start () {
		damageRefer = turretDamage;
		isSl = slowT;
		isExp = explosionT;
	


	}
	
	// Update is called once per frame
	void Update () {
	
		timer += Time.deltaTime;
		//Debug.Log ("timer: " + timer);
	
	}
	void OnTriggerStay2D(Collider2D trig){
		if (trig.gameObject.tag == "Enemy" && fastT==true) {


			Vector2 vectorToTarget=trig.transform.position-transform.position;
			float angle= Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x)*Mathf.Rad2Deg;
			Quaternion q=Quaternion.AngleAxis (angle,Vector3.forward);
			transform.rotation=Quaternion.Slerp (transform.rotation,q, Time.deltaTime*rotationSpeed);

			
			if (timer>=fireRate) {

				Debug.Log ("stai sparando!");
				GameObject bulletInstance;
				bulletInstance = Instantiate (bulletPrefab, spawnPoint.transform.position, bulletPrefab.transform.rotation)as GameObject;
				bulletInstance.GetComponent<Rigidbody2D>().AddForce(transform.right *forceToBullet);
				bulletInstance.GetComponent<BulletScript>().SetDamage(turretDamage);
				
				Destroy (bulletInstance, 3);
				timer=0;
			}

		}
		if (trig.gameObject.tag == "Enemy" && heavyT==true) {
			
			
			Vector2 vectorToTarget=trig.transform.position-transform.position;
			float angle= Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x)*Mathf.Rad2Deg;
			Quaternion q=Quaternion.AngleAxis (angle,Vector3.forward);
			transform.rotation=Quaternion.Slerp (transform.rotation,q, Time.deltaTime*rotationSpeed);
			
			
			if (timer>=fireRate) {
				
				Debug.Log ("stai sparando!");
				GameObject bulletInstance;
				bulletInstance = Instantiate (bulletPrefab, spawnPoint.transform.position, bulletPrefab.transform.rotation)as GameObject;
				bulletInstance.GetComponent<Rigidbody2D>().AddForce(transform.right *forceToBullet);
				bulletInstance.GetComponent<BulletScript>().SetDamage(turretDamage);
				
				Destroy (bulletInstance, 3);
				timer=0;
			}
			
		}
		if (trig.gameObject.tag == "Enemy" && explosionT==true) {

			Vector2 vectorToTarget=trig.transform.position-transform.position;
			float angle= Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x)*Mathf.Rad2Deg;
			Quaternion q=Quaternion.AngleAxis (angle,Vector3.forward);
			transform.rotation=Quaternion.Slerp (transform.rotation,q, Time.deltaTime*rotationSpeed);
			
			
			if (timer>=fireRate) {
				
				Debug.Log ("stai sparando!");
				GameObject bulletInstance;
				bulletInstance = Instantiate (bulletPrefab, spawnPoint.transform.position, bulletPrefab.transform.rotation)as GameObject;
				bulletInstance.GetComponent<Rigidbody2D>().AddForce(transform.right *forceToBullet);
				bulletInstance.GetComponent<BulletScript>().SetDamage(turretDamage);
				bulletInstance.GetComponent<BulletScript>().GetRange (explosionRange);
				bulletInstance.GetComponent<BulletScript>().GetExpDur(expDuration);
				Destroy (bulletInstance, 3);
				timer=0;
			}
			
		}
		
	}
	void IsFast(){

	}
	void IsHeavy(){
		
	}
	void IsSlow(){
		
	}
	void IsExplosion(){
		
	}


		



}
