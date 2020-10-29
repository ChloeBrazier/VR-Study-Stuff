using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class EnemyReflect : MonoBehaviour
{
    //number of volleys the enemy can do
    public int volleys = 10;
    public int currentVolleys = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentVolleys = volleys;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //send the projectile back if the enemy still has volleys left
        if (currentVolleys > 0 && other.gameObject.GetComponent<EvilProjectile>().vollied == true)
        {
            other.gameObject.transform.forward = (Player.instance.hmdTransform.position - other.gameObject.transform.position).normalized;
            currentVolleys--;
        }
        else if (volleys == 0 && other.gameObject.GetComponent<EvilProjectile>() != null)
        {
            //destroy this gameobject
            Destroy(this.gameObject);
        }
    }
}
