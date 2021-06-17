using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Material mat;
    public GameObject collar;
    public bool invulnerability = true;
    public bool keyboardControl = true;
    public AudioClip acceleration;
    public AudioClip fire;
    public AudioClip explosion;
    public GameObject audioS;

    private float invulnerabilityTimer = 0;
    private float speed = 25;
    private float rotationSpeed = 180;
    private bool colorAlphaZero = false;
    private bool colorAlphaOne = true;
    private float projectlieDelay = 0.33f;
    private float timeProjectileDelay = 0;
    private AudioSource audioSource;

    private void Awake()
    {
        Color col = mat.color;
        col.a = 1;
        mat.color = col;
        audioSource = audioS.GetComponent<AudioSource>();
    }

    private void Update()
    {
        ChangeControl();
        Blink();
    }

    private void OnTriggerEnter(Collider other)
    {
        UiController ui = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UiController>();
        if (invulnerability || other.gameObject.CompareTag("ProjectileHero"))
            return;
        audioSource.PlayOneShot(explosion);
        if (ui.heroLives > 0)
        {
            Destroy(other.gameObject);
            ui.heroLives--;
            transform.position = Vector3.zero;
            invulnerability = true;
        }
        else
        {
            audioSource.Stop();
            Destroy(other.gameObject);
            Destroy(gameObject);
            ui.EndGame();
        }
    }

    private void ChangeControl()
    {
        keyboardControl = GameObject.Find("Main Camera").GetComponent<UiController>().settingsText.text == "Управление: клавиатура" ? true : false;
        if (Time.timeScale == 1)
        {
            if (keyboardControl)
                KeyboardControl();
            else
                MouseControl();
        }
    }

    private void KeyboardControl()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            audioSource.clip = acceleration;
            audioSource.Play();
            audioSource.loop = true;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            audioSource.Stop();
            audioSource.clip = null;
            audioSource.loop = false;
        }

        float yAxis = Input.GetAxis("Vertical");
        transform.Translate(0, yAxis * speed * Time.deltaTime, 0);

        float xAxis = Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, -xAxis * rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= timeProjectileDelay)
        {
            //Instantiate(projectilePrefab, collar.transform.position, transform.rotation);
            GameObject bullet = ObjectPool.S.GetHeroProj();
            if (bullet != null)
            {
                bullet.transform.position = collar.transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }
            timeProjectileDelay = Time.time + projectlieDelay;
            audioSource.PlayOneShot(fire);
        }
    }

    private void MouseControl()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(1))
        {
            audioSource.clip = acceleration;
            audioSource.Play();
            audioSource.loop = true;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetMouseButtonUp(1))
        {
            audioSource.Stop();
            audioSource.clip = null;
            audioSource.loop = false;
        }

        float yAxis = Input.GetMouseButton(1) ? Input.GetAxis("Vertical with mouse") : Input.GetAxis("Vertical");
        transform.Translate(0, yAxis * speed * Time.deltaTime, 0);
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = mousePos - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z - 90);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && Time.time >= timeProjectileDelay)
        {
            //Instantiate(projectilePrefab, collar.transform.position, transform.rotation);
            GameObject bullet = ObjectPool.S.GetHeroProj();
            if (bullet != null)
            {
                bullet.transform.position = collar.transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }
            timeProjectileDelay = Time.time + projectlieDelay;
            audioSource.PlayOneShot(fire);
        }
    }

    private void Blink()
    {
        if (invulnerability)
        {
            Color col = mat.color;
            invulnerabilityTimer += Time.deltaTime;
            if (!colorAlphaZero)
            {
                col.a -= Time.deltaTime * 4;
                mat.color = col;
                if (mat.color.a <= 0)
                {
                    colorAlphaZero = true;
                    colorAlphaOne = false;
                }
            }
            if (!colorAlphaOne)
            {
                col.a += Time.deltaTime * 4;
                mat.color = col;
                if (mat.color.a >= 1)
                {
                    colorAlphaZero = false;
                    colorAlphaOne = true;
                }
            }
            if (invulnerabilityTimer >= 3)
            {
                col.a = 1;
                mat.color = col;
                invulnerability = false;
                invulnerabilityTimer = 0;
            }
        }
    }
}
