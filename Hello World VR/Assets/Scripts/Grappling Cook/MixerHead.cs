using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MixerHead : MonoBehaviour
{
    //steam vr action boolean
    public SteamVR_Action_Boolean startMix;

    //mixing variables
    private bool spin = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //go through active hands
        for (int i = 0; i < 2; i++)
        {
            SteamVR_Input_Sources source = Player.instance.hands[i].handType;

            //check if player is holding the trigger
            if (startMix[source].stateDown)
            {
                spin = true;
            }

            if (startMix[source].stateUp)
            {
                spin = false;
            }
        }

        //if the mixer's spinning, rotate it
        if(spin)
        {
            transform.Rotate(0, 0, 40);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if the mixer is spinning, check collision
        if(spin)
        {
            Debug.Log("Mixing Stuff Up");
        }
    }
}
