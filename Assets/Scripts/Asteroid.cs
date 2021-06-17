using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public float speed;
    private Vector3[] directions = { Vector3.left, Vector3.up, Vector3.right, Vector3.down };
    private int index = -1;
    private Vector3 particlePos = new Vector3(4, 0, 0);

    private void Start()
    {
        speed = Random.Range(10.0f, 20.0f);
        if (index < 0)
            index = Random.Range(0, directions.Length);
    }

    private void Update()
    {
        Move();
        ScaleLook();
    }

    private void Move()
    {
        transform.Translate(directions[index] * speed * Time.deltaTime);
    }

    private void ScaleLook()
    {
        if (transform.localScale == new Vector3(0, 0, transform.localScale.z))
            Destroy(gameObject);
    }

    public void Split()
    {
        transform.localScale -= new Vector3(3, 3);
        Vector3 pos = transform.position;
        pos += particlePos;
        transform.position -= particlePos; 
        Instantiate(gameObject, pos, gameObject.transform.rotation);
    }
}
