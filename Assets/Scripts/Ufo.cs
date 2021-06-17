using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    public GameObject projectilePrefab;
    public AudioClip explosion;

    private float speed = 15;
    private Rigidbody rigid;
    public float fireTimer = 0;
    public float fireDelay;
    private AudioSource audioSource;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        if (transform.position.x < 0)
            rigid.velocity = Vector3.right * speed;
        else
            rigid.velocity = Vector3.left * speed;
        fireDelay = Random.Range(2f, 5f);
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }

    private void Update()
    {
        DestroyOnBounds();
        Fire();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            audioSource.PlayOneShot(explosion);
        }
    }

    private void DestroyOnBounds()
    {
        if (transform.position.x >= 73.5f || transform.position.x <= -73.5f)
            Destroy(gameObject);
    }

    private void Fire()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireDelay)
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            fireTimer = 0;
            fireDelay = Random.Range(2f, 5f);
        }
    }
}
