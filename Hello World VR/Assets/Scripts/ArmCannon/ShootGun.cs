using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ShootGun : MonoBehaviour
{
    //transform for projectile spawn
    [SerializeField]
    private Transform blastSpawn;
    public GameObject projectilePrefab;

    //steam vr action boolean
    public SteamVR_Action_Boolean shootGun;

    //current projectile variables
    private GameObject currentProjectile;
    private bool chargeProjectile = false;
    private Vector3 projectileScale = new Vector3(0.3f, 0.3f, 0.3f);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            SteamVR_Input_Sources source = Player.instance.hands[i].handType;

            //check if player pressed down on trigger
            if (shootGun[source].stateDown)
            {
                //spawn projectile at the spawnpoint
                currentProjectile = Instantiate(projectilePrefab, blastSpawn);
                chargeProjectile = true;
            }

            //check if player is releasing trigger
            if (shootGun[source].stateUp)
            {
                //release projectile
                currentProjectile.GetComponent<CannonProjectile>().enabled = true;
                chargeProjectile = false;
            }

            if (chargeProjectile == true)
            {
                currentProjectile.transform.localScale += projectileScale * Time.deltaTime;
                if (currentProjectile.transform.localScale.x > 0.5f)
                {
                   currentProjectile.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
            }
        }
    }
}
