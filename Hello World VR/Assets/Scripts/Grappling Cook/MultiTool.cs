using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MultiTool : MonoBehaviour
{
    //steam vr action boolean(s)
    public SteamVR_Action_Boolean launchGrapple;
    public SteamVR_Action_Boolean switchHead;

    //multitool head variables
    public List<GameObject> toolHeads;
    private GameObject activeHead;
    private int toolIndex = 0;

    //multitool bools (multibools, even)
    private bool grappleHeadActive = true;
    private bool grappleLaunched = false;
    private bool resetGrapple = false;

    //grapple variables
    public Transform grappleReturn;

    // Start is called before the first frame update
    void Start()
    {
        //set active head
        activeHead = toolHeads[toolIndex];
    }

    // Update is called once per frame
    void Update()
    {
        //go through active hands
        for (int i = 0; i < 2; i++)
        {
            SteamVR_Input_Sources source = Player.instance.hands[i].handType;

            //check if player pressed down on trigger
            if (launchGrapple[source].stateDown && grappleHeadActive == true)
            {
                if(grappleLaunched == false)
                {
                    //launch the grapple head if it's active
                    activeHead.GetComponent<Rigidbody>().isKinematic = false;
                    activeHead.GetComponent<Rigidbody>().AddForce(activeHead.transform.forward * 2000);
                    grappleLaunched = true;
                    StartCoroutine(GrappleResetCooldown());
                }
                else
                {
                    //increase spring force to bring back the hook
                    GetComponent<SpringJoint>().spring = 100;
                }
                
            }

            //check if player pressed the grip
            if (switchHead[source].stateDown)
            {
                //disable current active toolhead
                activeHead.SetActive(false);

                //switch heads
                toolIndex++;
                if(toolIndex == toolHeads.Count)
                {
                    toolIndex = 0;
                }

                //set the next toolhead as active
                toolHeads[toolIndex].SetActive(true);
                activeHead = toolHeads[toolIndex];

                if(activeHead.GetComponent<GrappleHead>() != null)
                {
                    grappleHeadActive = true;
                }
                else
                {
                    grappleHeadActive = false;
                }
            }
        }

        //grapple stuff for when it's active
        if(grappleHeadActive)
        {
            //return grapple head to starting position once it returns
            if (Vector3.Distance(transform.position, activeHead.transform.position) < 1 && resetGrapple == true)
            {
                //reset grappling hook and drop any held items
                if(activeHead.GetComponent<GrappleHead>().heldItem != null)
                {
                    activeHead.GetComponent<GrappleHead>().DropItem();
                }

                activeHead.GetComponent<Rigidbody>().isKinematic = true;
                activeHead.transform.position = grappleReturn.position;
                activeHead.transform.forward = grappleReturn.forward;
                grappleLaunched = false;
                resetGrapple = false;
            }
        }
    }

    public IEnumerator GrappleResetCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        resetGrapple = true;
    }
}
