using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectProjectile : MonoBehaviour
{
    //gameobject to spawn at the contact point
    public GameObject evilProjectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<CannonProjectile>() != null && collision.gameObject.GetComponent<EvilProjectile>() == null)
        {
            Debug.Log("reflecting");

            //spawn a projectile at the point of collision
            float size = collision.gameObject.transform.localScale.x;
            ContactPoint contact = collision.GetContact(0);
            GameObject newProjectile = Instantiate(evilProjectilePrefab, contact.point, Quaternion.identity);
            newProjectile.transform.localScale = new Vector3(size, size, size);
            newProjectile.transform.forward = transform.forward;
        }
        else if(collision.gameObject.GetComponent<EvilProjectile>() != null)
        {
            collision.gameObject.transform.forward = transform.forward;
        }
    }
}
