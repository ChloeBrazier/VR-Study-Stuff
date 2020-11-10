using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GrappleHead : MonoBehaviour
{
    //grabbed object
    public GameObject heldItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if an item is being held, keep it at collider's center
        if(heldItem != null)
        {
            heldItem.transform.position = GetComponent<SphereCollider>().transform.position;
        }
    }

    public void DropItem()
    {
        heldItem.GetComponent<Rigidbody>().velocity = Vector3.zero;
        heldItem = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Food" && heldItem == null)
        {
            Debug.Log("grab");
            //parent object to the hook
            heldItem = other.gameObject;
        }
        
    }
}
