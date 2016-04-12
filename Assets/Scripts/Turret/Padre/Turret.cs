using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	public float CostBuild;
	public float CostUpgrade;
	public float m_Damage;
	public float m_FireRate;
	public LayerMask m_EnemyLayer;
    public LayerMask m_BuildTileMask;
    public GameObject m_BulletPrefab;
    public float m_Range;
    protected string extraInfo;
    public int value;
   protected  string typeName;
    DialogoController dialogo;
    GameObject radius;

    RaycastHit2D hit;
    Upgrade up;

   public bool radiusVisible;
    [HideInInspector]
    public bool canShoot = true;
   

    //Variabili TorreInterne
	[HideInInspector]
	public float timer;
    CircleCollider2D range;


	//Variabili per SetTArget()
	[HideInInspector]
	public Collider2D[] enemyInRange;
	[HideInInspector]
	public GameObject target;
	

	//Variabili per Shoot();
	[HideInInspector]
	public GameObject[] bulletPool;
	[HideInInspector]
	public int bulletPoolIndex;


    protected virtual void Awake()
	{
        up = GetComponent<Upgrade>();
        dialogo = FindObjectOfType<DialogoController>();
        radius = transform.GetChild(2).gameObject;
        enemyInRange = new Collider2D[20];
        range = GetComponent<CircleCollider2D>();
        range.radius = m_Range;
		bulletPool = new GameObject[10];
		target = null;

		for(int i = 0; i< bulletPool.Length; i++)
		{
			bulletPool[i] = Instantiate(m_BulletPrefab, Vector3.zero, m_BulletPrefab.transform.rotation) as GameObject;
			bulletPool[i].SendMessage("SetDamage", m_Damage);
			bulletPool[i].SetActive(false);
		}
		bulletPoolIndex = 0;
	}

	void Update(){
		timer += Time.deltaTime;
        // canShoot = true;
        if (radiusVisible)
            radius.transform.localScale = new Vector3(m_Range * 1.7f, m_Range * 1.7f, 0);
        else
            radius.transform.localScale = new Vector3(0, 0, 0);
        if(!canShoot)
            radius.transform.localScale = new Vector3(m_Range * 1.7f, m_Range * 1.7f, 0);
    }

    protected virtual void Start()
    {
        SetBuildTile();
    }

    void OnTriggerStay2D(Collider2D trig)
	{
		if (trig.gameObject.tag == "Enemy")
		{
			SetTarget();

            if (timer >= m_FireRate)
            {
                if(canShoot)
                    Shoot();
            }
		}		
	}

	void OnTriggerExit2D(Collider2D trig)
	{
		if (trig.CompareTag ("Enemy")) {
			target = null;
			CancelEnemyArray(trig);
			SetTarget ();
		}
		
	}

	public void SetTarget()
	{

        Physics2D.OverlapCircleNonAlloc(transform.position, range.radius - 0.5f, enemyInRange,m_EnemyLayer);
		
		for(int i = 0; i < enemyInRange.Length; i++)
		{
			if (enemyInRange[i] != null)
			{
				if (target == null || !target.activeInHierarchy)
				
					target = enemyInRange[i].gameObject;

				else if (enemyInRange[i].gameObject.GetComponent<Enemy>().DistToCore() < target.GetComponent<Enemy>().DistToCore())
				
					target = enemyInRange[i].gameObject;

			}
		}		
	}


	public void CancelEnemyArray(Collider2D target)
	{
		for (int i = 0; i < enemyInRange.Length; i++) 
		{

			if(enemyInRange[i] != null)
			{	

				if( enemyInRange[i].Equals(target))

					enemyInRange[i] = null;
			}			
		}
	}


	public virtual void Shoot()
	{
		if (target != null && target.activeInHierarchy) 
		{
			
			if (bulletPoolIndex < bulletPool.Length - 1) 
			{

				if (!bulletPool [bulletPoolIndex].activeInHierarchy) 
				{
					bulletPool [bulletPoolIndex].transform.position = transform.position;
					bulletPool [bulletPoolIndex].SetActive (true);
					bulletPool [bulletPoolIndex].SendMessage ("SetTarget", target);
					timer = 0;
					
				} 
				else

					bulletPoolIndex++;
			} 
			else

				bulletPoolIndex = 0;
		} 
		else if (target != null) 
		{
			CancelEnemyArray(target.GetComponent<Collider2D>());
			SetTarget ();
		}
	}

    public void setRange(float ran)
    {
        range.radius = ran;
    }

    public void SetBuildTile()
    {
        hit = Physics2D.Raycast(transform.position, Vector3.back, 100, m_BuildTileMask);
        if (hit)
        {
            hit.transform.gameObject.SendMessage("SetCurrentTurret", gameObject);
        }
    }

    public string TurretDialogo()
  {
    return  typeName + " turret\n\n" + " Damage: " + m_Damage + "\n\nFire rate: " + m_FireRate + extraInfo+ "\n\n\nLeft click to build\n Right click to cancel" ;
    }

    public void UpInfo(bool builded)
    {

        string info = typeName + " turret\n\n" + "Upgrade cost: " + CostUpgrade + "\n\nDamage: " + m_Damage + " > " + (m_Damage + m_Damage*up.UpgradeDamage/100) + "\n\nFire rate: " + m_FireRate + " > " + (m_FireRate + m_FireRate*up.UpgradeFireRate/100) + "\n\n\nSell for " + 10;
        dialogo.TurretUpInfo(info);
    }
}
