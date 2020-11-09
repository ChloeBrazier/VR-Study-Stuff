using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilProjectile : CannonProjectile
{
    //projectile blast prefab
    public GameObject blastPrefab;

    //parent object and volley bool
    public GameObject returnTo;
    public bool canVolley = false;
    public bool vollied = false;

    // Start is called before the first frame update
    void Start()
    {
        //set kill time to be longer and speed to be slower
        killTime = 20f;
        projectileSpeed = 3f;

        //start coroutine to destroy the projectile
        StartCoroutine(KillProjectile(killTime, this.gameObject));

        //unparent from blast location
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        //move forward
        transform.position += transform.forward * (projectileSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if the collision was with the arm cannon
        if(collision.gameObject.GetComponent<ShootGun>() != null)
        {
            Debug.Log("Projectile Deflected");

            //spawn a blast
            GameObject blast = Instantiate(blastPrefab, collision.GetContact(0).point, Quaternion.identity);
            blast.transform.SetParent(collision.transform);
            StartCoroutine(KillProjectile(0.5f, blast));

            //determine if the projectile is to be vollied or not
            if(returnTo != null && canVolley == true)
            {
                //set projectile's forward vector to go back to the enemy it spawned from
                StopCoroutine("KillProjectile");
                transform.forward = (returnTo.transform.position - transform.position).normalized;
                vollied = true;
            }
            else
            {
                //get the average velocity and set the forward transform to it then increase speed
                transform.forward = collision.GetContact(0).normal.normalized;
            }

            projectileSpeed = 7f;
        }
        else if(collision.gameObject.GetComponent<BasicEnemy>() != null && vollied)
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.GetComponent<BasicEnemy>() == null && collision.gameObject.GetComponent<ReflectProjectile>() == null)
        {
            Destroy(gameObject);
        }
    }

    public void Redirect(Vector3 newForward)
    {
        transform.forward = newForward;
        projectileSpeed = 7f;
    }
}
