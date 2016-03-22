using UnityEngine;
using System.Collections;

public class BulletAle : MonoBehaviour {
    public float m_Speed;
    public float m_Damage;
    GameObject target;


    void FixedUpdate()
    {
        if(target != null)
        transform.position = Vector2.Lerp(transform.position, target.transform.position, m_Speed * Time.deltaTime);
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            col.gameObject.SendMessage("TakeDamage", m_Damage);
            gameObject.SetActive(false);
        }
    }

    public void SetTarget(GameObject tar)
    {
        target = tar;
    }

    
	
}
