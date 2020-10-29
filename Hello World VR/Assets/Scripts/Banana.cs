using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{

    public GameObject peel;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Floor")
        {
            Instantiate(peel, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
