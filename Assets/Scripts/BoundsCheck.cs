using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    public Camera cam;

    private void Start()
    {
        if (cam == null)
        {
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
    }

    private void Update()
    {
        Vector3 point = cam.WorldToViewportPoint(transform.position);
        if (point.y < 0)
            transform.position = new Vector3(transform.position.x, -transform.position.y - 0.5f);
        if (point.y > 1)
            transform.position = new Vector3(transform.position.x, -transform.position.y + 0.5f);
        if (point.x - 0.001f < 0)
            transform.position = new Vector3(-transform.position.x - 0.5f, transform.position.y);
        if (point.x + 0.001f  > 1)
            transform.position = new Vector3(-transform.position.x + 0.5f, transform.position.y);
    }
}
