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
        Debug.DrawLine(vacuumHead.position, transform.position - vacuumHead.up * 10, Color.red);
        //cast out 
        if(Physics.SphereCast(transform.position, 100f, -vacuumHead.up, out RaycastHit hit, Mathf.Infinity))
        {
            if(hit.transform.gameObject.GetComponent<Interactable>() != null)
            {
                Debug.Log("time to suck");
                //pull object into the vacuum after getting force vector
                Vector3 forceVector = (vacuumHead.position - hit.transform.position).normalized * 10000;
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(forceVector);
            }
        }
    }
}
