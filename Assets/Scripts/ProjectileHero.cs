using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileHero : MonoBehaviour
{
    public AudioClip explosion;

    private float speed = 40;
    private Rigidbody rigid;
    private float lifeTime = 3.6f;
    private float timer = 0;
    private UiController uiController;
    public AudioSource audioSource;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        uiController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UiController>();
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }

    private void Update()
    {
        rigid.velocity = transform.up * speed;
        LifeTimer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hero"))
            return;
        audioSource.PlayOneShot(explosion);
        if (other.gameObject.CompareTag("Asteroid"))
        {
            if (other.gameObject.transform.localScale.x == 9)
                uiController.Score(20);
            if (other.gameObject.transform.localScale.x == 6)
                uiController.Score(50);
            if (other.gameObject.transform.localScale.x == 3)
                uiController.Score(100);
            Asteroid ast = other.gameObject.GetComponent<Asteroid>();
            ast.Split();
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Ufo"))
        {
            uiController.Score(200);
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }

    private void LifeTimer()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            gameObject.SetActive(false);
            timer = 0;
        }
    }
}
