using UnityEngine;
using System.Collections;


public class TurretBehaviour : MonoBehaviour {
	public LayerMask mask;
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

	GameObject mainTarget;



	void Awake(){

	}
	// Use this for initialization
	void Start () {
		damageRefer = turretDamage;
		isSl = slowT;
		isExp = explosionT;
		isFast=fastT;
		isHeavy=heavyT;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<CircleCollider2D> ().radius = range;
		timer += Time.deltaTime;

	
	}

	GameObject SetTarget(){
		Collider2D[] possibleTarget = Physics2D.OverlapCircleAll (transform.position, range , mask);
		//GameObject temptarget = possibleTarget [0].gameObject;
		//return temptarget;
		mainTarget = possibleTarget[0].gameObject;
		for (int i = 0; i < possibleTarget.Length; i++) {
			//inizialmente il primo target è il primo che entra
		
			//poi diventa quello con più switch passati e con la maggiore distanza percorsa nel range di una torretta(questa distanza viene resettata quando esce mentre il numero di switch no
			if (possibleTarget [i].gameObject.GetComponent<Enemy> ().ReturnSwitchValue () > mainTarget.GetComponent<Enemy> ().ReturnSwitchValue ()) {
				mainTarget = possibleTarget [i].gameObject;
				if (possibleTarget [i].gameObject.GetComponent<Enemy> ().ReturnTileValue () > mainTarget.GetComponent<Enemy> ().ReturnTileValue ()) {
					mainTarget = possibleTarget [i].gameObject;		
				}

				return mainTarget;
			}
		}
		return null;

	}


	void OnTriggerStay2D(Collider2D trig){
		if (trig.gameObject.tag == "Enemy" && fastT==true) {
			mainTarget = SetTarget ();
			Debug.Log (mainTarget.name);


			/*Vector2 vectorToTarget=mainTarget.transform.position-transform.position;
			float angle= Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x)*Mathf.Rad2Deg;
			Quaternion q=Quaternion.AngleAxis (angle,Vector3.forward);
			transform.rotation=Quaternion.Slerp (transform.rotation,q, Time.deltaTime*rotationSpeed);*/

			
			if (timer>=fireRate) {

				GameObject bulletInstance;
				bulletInstance = Instantiate (bulletPrefab, spawnPoint.transform.position, bulletPrefab.transform.rotation)as GameObject;
				bulletInstance.GetComponent<BulletScript>().SetDamage(turretDamage);
				bulletInstance.GetComponent<BulletScript>().target = trig.gameObject;
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

				GameObject bulletInstance;
				bulletInstance = Instantiate (bulletPrefab, spawnPoint.transform.position, bulletPrefab.transform.rotation)as GameObject;
				bulletInstance.GetComponent<Rigidbody2D>().AddForce(transform.right *forceToBullet);
				bulletInstance.GetComponent<BulletScript>().SetDamage(turretDamage);
				
				Destroy (bulletInstance, 3);
				timer=0;
			}
			
		}
		if (trig.gameObject.tag == "Enemy" && slowT==true) {
			
			
			Vector2 vectorToTarget=trig.transform.position-transform.position;
			float angle= Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x)*Mathf.Rad2Deg;
			Quaternion q=Quaternion.AngleAxis (angle,Vector3.forward);
			transform.rotation=Quaternion.Slerp (transform.rotation,q, Time.deltaTime*rotationSpeed);
			
			
			if (timer>=fireRate) {

				GameObject bulletInstance;
				bulletInstance = Instantiate (bulletPrefab, spawnPoint.transform.position, bulletPrefab.transform.rotation)as GameObject;
				bulletInstance.GetComponent<Rigidbody2D>().AddForce(transform.right *forceToBullet);
				bulletInstance.GetComponent<BulletScript>().SetDamage(turretDamage);
				bulletInstance.GetComponent<BulletScript>().SetSlow(slowAmmount);
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
