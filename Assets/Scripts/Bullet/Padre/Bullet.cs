using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float m_Speed;

	[HideInInspector]
	public float damage;
	[HideInInspector]
	public GameObject target;
    protected float disableTime=0.5f;


    protected virtual void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Deactivate());
    }

    public IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }



	public virtual void FixedUpdate()
	{
		if(target != null)
			transform.position = Vector2.Lerp(transform.position, target.transform.position, m_Speed * Time.deltaTime);
	}

	public void SetDamage(float amount)
	{
		damage = amount;
	}

	public void SetTarget(GameObject tar)
	{
		target = tar;
	}
}
