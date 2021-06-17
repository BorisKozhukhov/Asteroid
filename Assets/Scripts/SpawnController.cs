using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject ufo;

    private int numbersOfAsteroids = 2;
    private float asteroidTimer = 0;
    private float ufoTimer = 0;
    private float[] xUfoPos = { -73, 73 };
    private int delayUfo;

    private void Start()
    {
        for (int i = 0; i < numbersOfAsteroids; i++)
        {
            float xPosition = Random.Range(-74f, 74f);
            float yPosition = Random.Range(-35f, 35f);
            Vector3 spawnPosition = new Vector3(xPosition, yPosition);
            Instantiate(asteroid, spawnPosition, asteroid.transform.rotation);
        }
        numbersOfAsteroids++;
        delayUfo = Random.Range(20, 41);
    }

    private void Update()
    {
        SpawnAsteroids();
        SpawnUfo();
    }

    private void SpawnAsteroids()
    {
        if (GameObject.FindGameObjectWithTag("Asteroid") == null)
        {
            asteroidTimer += Time.deltaTime;
            if (asteroidTimer > 2)
            {
                Hero hero = GameObject.Find("Hero").GetComponent<Hero>();
                hero.invulnerability = true;
                for (int i = 0; i < numbersOfAsteroids; i++)
                {
                    float xPosition = Random.Range(-74f, 74f);
                    float yPosition = Random.Range(-35f, 35f);
                    Vector3 spawnPosition = new Vector3(xPosition, yPosition);
                    Instantiate(asteroid, spawnPosition, asteroid.transform.rotation);
                }
                numbersOfAsteroids++;
                asteroidTimer = 0;
            }
        }
    }

    private void SpawnUfo()
    {
        if (GameObject.FindGameObjectWithTag("Ufo") == null)
        {
            ufoTimer += Time.deltaTime;
            if (ufoTimer >= delayUfo)
            {
                int index = Random.Range(0, 2);
                float xPosition = xUfoPos[index];
                float yPosition = Random.Range(-21f, 21f);
                Vector3 spawnPosition = new Vector3(xPosition, yPosition);
                Instantiate(ufo, spawnPosition, asteroid.transform.rotation);
                ufoTimer = 0;
                delayUfo = Random.Range(20, 41);
            }
        }
    }
}
