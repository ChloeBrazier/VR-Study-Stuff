using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class Vacuum : MonoBehaviour
{
    //object for vacuum end point
    public Transform vacuumHead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //cast out 
        if(Physics.SphereCast(vacuumHead.position, 3f, -transform.up, out RaycastHit hit, Mathf.Infinity))
        {
            if(hit.transform.gameObject.GetComponent<Interactable>() != null)
            {
                //pull object into the vacuum after getting force vector
                Vector3 forceVector = (vacuumHead.position - hit.transform.position).normalized * 50;
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(forceVector);
            }
        }
    }
}
