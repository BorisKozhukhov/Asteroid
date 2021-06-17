using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileUfo : MonoBehaviour
{
    private float speed = 40; 
    private float lifeTime = 3.6f;
    private Rigidbody rigid;
    private float timer = 0;
    private GameObject hero;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        hero = GameObject.Find("Hero");
        if (hero != null)
        {
            Vector3 lookDirection = (hero.transform.position - transform.position).normalized;
            rigid.velocity = lookDirection * speed;
        }
    }

    private void Update()
    {
        LifeTimer();
    }

    private void LifeTimer()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
            Destroy(gameObject);
    }
}
