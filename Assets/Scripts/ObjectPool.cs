using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool S;
    public List<GameObject> pooledHeroProj;
    public GameObject heroProjToPool;
    public int amountHPToPool = 11;

    private void Awake()
    {
        S = this;
    }

    private void Start()
    {
        pooledHeroProj = new List<GameObject>();
        GameObject hero;
        for (int i = 0; i < amountHPToPool; i++)
        {
            hero = Instantiate(heroProjToPool,gameObject.transform);
            hero.SetActive(false);
            pooledHeroProj.Add(hero);
        }
    }

    public GameObject GetHeroProj()
    {
        for (int i = 0; i < amountHPToPool; i++)
        {
            if (!pooledHeroProj[i].activeInHierarchy)
                return pooledHeroProj[i];
        }
        return null;
    }
}
