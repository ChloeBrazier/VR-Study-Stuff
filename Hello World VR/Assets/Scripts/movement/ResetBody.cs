using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ResetBody : MonoBehaviour
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
        Debug.Log("colliding");

        if (other.gameObject.tag == "Floor")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            Debug.Log("made player stop");
        }
    }
}
