using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class Pockets : MonoBehaviour
{
    //bool to determine if pockets have been setup
    private bool setupComplete = false;

    //left pocket and right pocket gameobjects


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.instance.headsetOnHead.GetStateDown(SteamVR_Input_Sources.Head) && setupComplete != true)
        {
            //move pockets to player's approximate hip position

            //parent pockets to player
        }
    }
}
