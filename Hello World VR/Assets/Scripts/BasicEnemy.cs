using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class BasicEnemy : MonoBehaviour
{
    //bool for shooting
    private bool canShoot = true;

    //projectile prefab and current projectile
    public GameObject projectilePrefab;
    private GameObject currentProjectile = null;

    //transform for spawning projectile
    public GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        //start the cooldown
        StartCoroutine(CoolDown());
    }

    // Update is called once per frame
    void Update()
    {
        //shoot at the player 
        if(canShoot && currentProjectile == null)
        {
            //spawn projectile and aim it at the player
            float size = 0.4f;
            GameObject newProjectile = Instantiate(projectilePrefab, spawner.transform.position, Quaternion.identity);
            currentProjectile = newProjectile;
            newProjectile.transform.localScale = new Vector3(size, size, size);
            newProjectile.GetComponent<EvilProjectile>().returnTo = gameObject;
            newProjectile.GetComponent<EvilProjectile>().canVolley = true;
            newProjectile.transform.forward = Player.instance.hmdTransform.position - newProjectile.transform.position;
            spawner.GetComponent<EnemyReflect>().currentVolleys = spawner.GetComponent<EnemyReflect>().volleys;

            //start the cooldown
            StartCoroutine(CoolDown());
        }
    }

    public IEnumerator CoolDown()
    {
        canShoot = false;
        yield return new WaitForSecondsRealtime(3);
        canShoot = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //die if colliding with an evil projectile
        if(collision.gameObject.GetComponent<EvilProjectile>() != null && collision.gameObject.GetComponent<EvilProjectile>().vollied == true)
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.GetComponent<CannonProjectile>() != null && collision.gameObject.GetComponent<EvilProjectile>() == null)
        {
            Destroy(gameObject);
        }
    }
}
