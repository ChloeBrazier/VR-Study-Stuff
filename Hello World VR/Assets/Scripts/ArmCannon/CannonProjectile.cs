using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour
{
    public float killTime = 1f;
    protected float projectileSpeed = 12f;

    // Start is called before the first frame update
    void Start()
    {
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

    public IEnumerator KillProjectile(float killTime, GameObject toBeKilled)
    {
        Debug.Log("projectile killed");
        yield return new WaitForSecondsRealtime(killTime);
        Destroy(toBeKilled);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if the collision was with the arm cannon
        if (collision.gameObject.GetComponent<ShootGun>() == null)
        {
            //destroy this if it collides with another object
            Destroy(this.gameObject);
        }
    }
}
