using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GrappleGun : MonoBehaviour
{
    //steam vr action boolean
    public SteamVR_Action_Boolean activateGrapple;

    //bool for grapple interaction
    private bool grappleActive = false;
    private bool canLaunch = false;

    //object's line renderer
    private LineRenderer lineRenderer;

    //launch force
    private int launchForce = 30;

    private Rigidbody playerBody;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        playerBody = Player.instance.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Hand hand in Player.instance.hands)
        {
            SteamVR_Input_Sources source = hand.handType;

            //check if player is holding down trigger
            if (activateGrapple[source].stateDown)
            {
                //activate linerenderer and other grapple stuff
                lineRenderer.enabled = true;
                grappleActive = true;
            }

            //check if player is releasing trigger
            if (activateGrapple[source].stateUp)
            {
                //deactivate linerenderer

                lineRenderer.enabled = false;

                //launch player towards the grapple point if they hit it
                if (Physics.SphereCast(transform.position, 2f, -transform.up, out RaycastHit hit, Mathf.Infinity))
                {

                    if (hit.transform.gameObject.tag == "GrapplePoint")
                    {
                        playerBody.velocity = Vector3.zero;
                        LaunchPlayer(hit.point);
                    }
                }

                //deactivate grapple
                grappleActive = false;
            }
        }

        //do grapple stuff
        if(grappleActive == true)
        {
            //set linerenderer positons
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + -transform.up * 10);

            //launch player towards the grapple point if they hit it
            if (Physics.SphereCast(transform.position, 2f,-transform.up, out RaycastHit hit, Mathf.Infinity))
            {

                if (hit.transform.gameObject.tag == "GrapplePoint")
                {
                    lineRenderer.SetPosition(1, hit.point);
                    lineRenderer.endColor = Color.green;
                    lineRenderer.startColor = Color.green;
                }
                else
                {
                    lineRenderer.endColor = Color.red;
                    lineRenderer.startColor = Color.red;
                }
            }
        }
    }

    public void LaunchPlayer(Vector3 hitPos)
    {
        playerBody.isKinematic = false;
        playerBody.AddForce((hitPos - transform.position) * launchForce);
    }
}
