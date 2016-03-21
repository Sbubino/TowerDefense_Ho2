using UnityEngine;
using System.Collections;


public class TurretBehaviour : MonoBehaviour {

	public float rotationSpeed= 5.0f;
	public GameObject bulletPrefab;
	public float fireRate=1;
	public float range=1;

	public int turretDamage=1;

	public int turretCost;
	public int upgradeCost;
	public int sellEarn;
	public float slowAmmount;
	public float explosionRange;
	public float expDuration=0.5f;

	public bool fastT, heavyT, slowT, explosionT;
	public static bool isFast,isHeavy,isSl, isExp;
	public bool upgrade1=false, upgrade2=false;
	GameObject[] bulletPool;
    int bulletPoolIndex;
    GameObject target;

    GameObject[] targetsInRange;
    int indexOfTarget;
    float[] distanceOfTargets;

    GameObject sprite;

	float timer;
	GameObject mainTarget;
	GameObject spawnPoint;


	void Awake(){
		target = null;
		sprite = transform.GetChild(0).gameObject;
		spawnPoint = transform.GetChild(1).gameObject;
		targetsInRange = new GameObject[10];
		indexOfTarget = 0;
		distanceOfTargets = new float[10];
		bulletPool = new GameObject[20];
		for(int i = 0; i< bulletPool.Length; i++)
		{
			bulletPool[i] = Instantiate(bulletPrefab, Vector3.zero, bulletPrefab.transform.rotation) as GameObject;
			bulletPool[i].SetActive(false);
		}
		bulletPoolIndex = 0;

		isSl = slowT;
		isExp = explosionT;
		isFast=fastT;
		isHeavy=heavyT;

	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<CircleCollider2D> ().radius = range;
		timer += Time.deltaTime;	
	}


/*	GameObject SetTarget(){
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
    */

	void OnTriggerStay2D(Collider2D trig){
	/*+	if (trig.gameObject.tag == "Enemy" && fastT==true) {
			mainTarget = SetTarget ();
			Debug.Log (mainTarget.name);

        
			/*Vector2 vectorToTarget=mainTarget.transform.position-transform.position;
=======



	void OnTriggerStay2D(Collider2D trig){
		if (trig.gameObject.tag == "Enemy" && fastT==true) {
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
>>>>>>> a51b91032281b253eeabe47b4c0274f2fcacf581
			float angle= Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x)*Mathf.Rad2Deg;
			Quaternion q=Quaternion.AngleAxis (angle,Vector3.forward);
			transform.rotation=Quaternion.Slerp (transform.rotation,q, Time.deltaTime*rotationSpeed);

			
			if (timer>=fireRate) {

				ShootFast ();
				timer=0;
			}

		}
		if (trig.gameObject.tag == "Enemy" && heavyT==true) {
			
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
			
			
			if (timer>=fireRate) {
				
				ShootFast ();
				timer=0;
			}
			
		}
		if (trig.gameObject.tag == "Enemy" && slowT==true) {

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
			
			
			if (timer>=fireRate) {
				
				ShootSlow ();
				timer=0;
			}
			
		}
		if (trig.gameObject.tag == "Enemy" && explosionT==true) {

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
			
			
			if (timer>=fireRate) {
				
				ShootExplosive ();
				timer=0;
			}
			
		}*/
		
	}
	void OnTriggerExit2D(Collider2D trig)
	{
		if (trig.gameObject.tag == "Enemy" && trig.gameObject.Equals(target))
			target = null;
	}

	void IsFast(){

	}
	void IsHeavy(){
		
	}
	void IsSlow(){
		
	}
	void IsExplosion(){
		
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

	void ShootFast()
	{
		if (bulletPoolIndex < bulletPool.Length - 1)
		{
			if (!bulletPool[bulletPoolIndex].activeInHierarchy)
			{
				bulletPool[bulletPoolIndex].transform.position = spawnPoint.transform.position;
				bulletPool[bulletPoolIndex].SetActive(true);
				bulletPool[bulletPoolIndex].SendMessage("SetTarget", target);
				bulletPool[bulletPoolIndex].GetComponent<BulletScript>().SetDamage(turretDamage);
				
			}
			else
				bulletPoolIndex++;
		}
		else
			bulletPoolIndex = 0;
		
	}
	void ShootExplosive()
	{
		if (bulletPoolIndex < bulletPool.Length - 1)
		{
			if (!bulletPool[bulletPoolIndex].activeInHierarchy)
			{
				bulletPool[bulletPoolIndex].transform.position = spawnPoint.transform.position;
				bulletPool[bulletPoolIndex].SetActive(true);
				bulletPool[bulletPoolIndex].SendMessage("SetTarget", target);
				bulletPool[bulletPoolIndex].GetComponent<BulletScript>().SetDamage(turretDamage);
				bulletPool[bulletPoolIndex].GetComponent<BulletScript>().GetRange (explosionRange);
				bulletPool[bulletPoolIndex].GetComponent<BulletScript>().GetExpDur(expDuration);
				
			}
			else
				bulletPoolIndex++;
		}
		else
			bulletPoolIndex = 0;
		
	}
	void ShootSlow()
	{
		if (bulletPoolIndex < bulletPool.Length - 1)
		{
			if (!bulletPool[bulletPoolIndex].activeInHierarchy)
			{
				bulletPool[bulletPoolIndex].transform.position = spawnPoint.transform.position;
				bulletPool[bulletPoolIndex].SetActive(true);
				bulletPool[bulletPoolIndex].SendMessage("SetTarget", target);
				bulletPool[bulletPoolIndex].GetComponent<BulletScript>().SetDamage(turretDamage);
				bulletPool[bulletPoolIndex].GetComponent<BulletScript>().SetSlow(slowAmmount);

				
			}
			else
				bulletPoolIndex++;
		}
		else
			bulletPoolIndex = 0;
		
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
}
