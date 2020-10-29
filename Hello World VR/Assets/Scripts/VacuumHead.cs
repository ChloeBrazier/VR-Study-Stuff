using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class VacuumHead : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //delete the object if it's an interactable
        if(other.gameObject.GetComponent<Interactable>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
