using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{

    public GameObject peel;
    //y 0.04831seven9
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Floor")
        {
            Vector3 spawn = transform.position;
            spawn.y = 0;
            Instantiate(peel, spawn, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    //y0 = 
    //yf =
}
