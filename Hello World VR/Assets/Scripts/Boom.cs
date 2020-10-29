using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public GameObject particles;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            Vector3 spawn = transform.position;
            spawn.y = 1;
            Instantiate(particles, spawn, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
