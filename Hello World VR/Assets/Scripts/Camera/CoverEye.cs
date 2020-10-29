using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class CoverEye : MonoBehaviour
{
    //steam vr action boolean for eye covering
    public SteamVR_Action_Boolean coverEye;
    private bool canSee = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 2; i++)
        {
            SteamVR_Input_Sources source = Player.instance.hands[i].handType;

            if (coverEye[source].stateDown)
            {
                //change eye status
                canSee = !canSee;
                Debug.Log(canSee);
                if (canSee == true)
                {
                    GetComponent<Camera>().cullingMask = LayerMask.GetMask("Default");
                }
                else
                {
                    //blind that eye
                    GetComponent<Camera>().cullingMask = LayerMask.GetMask("Nothing");
                }
            }
        }
    }
}
