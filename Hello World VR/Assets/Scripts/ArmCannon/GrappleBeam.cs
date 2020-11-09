using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class GrappleBeam : MonoBehaviour
{
    //steam vr action boolean
    public SteamVR_Action_Boolean activateGrapple;

    //bool for grapple interaction
    private bool grappleActive = false;
    private bool canLaunch = false;

    //grapple range
    private float grappleRange = 10f;

    //object's line renderer
    private LineRenderer lineRenderer;

    // active object
    private GameObject activeObject;

    //velocity check parameters
    private Vector3 lastPos;
    private Vector3 currentPos;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //draw a line
        Debug.DrawLine(transform.position, transform.position + transform.forward);

        foreach (Hand hand in Player.instance.hands)
        {
            SteamVR_Input_Sources source = hand.handType;

            //check if player is holding down trigger
            if (activateGrapple[source].stateDown)
            {
                //activate linerenderer and other grapple stuff
                if(activeObject != null)
                {
                    canLaunch = true;
                }
            }

            //check if player is releasing trigger
            if (activateGrapple[source].stateUp)
            {
                StopGrappple();
            }
        }

        //lock on to object if it's hit
        if (Physics.SphereCast(transform.position, 1.5f, transform.forward, out RaycastHit hit, grappleRange))
        {

            if (hit.transform.gameObject.tag == "GrapplePoint")
            {
                //enable line renderer
                lineRenderer.enabled = true;

                //set line renderer
                SetGrappleBeam();

                //allow launchability of the object
                grappleActive = true;

                //set active object
                activeObject = hit.transform.gameObject;
            }
            else
            {
                if(activeObject == null)
                {
                    //stop the grapple
                    StopGrappple();
                }
            }
        }

        //do this when you're locked on to an object
        if(canLaunch)
        {
            //set linerenderer positions:
            //get vector between hand and active object
            Vector3 renderVector = activeObject.transform.position - transform.position;
            float pointDistance = renderVector.magnitude / lineRenderer.positionCount;
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                if (i == 0)
                {
                    lineRenderer.SetPosition(i, transform.position);
                }
                else if(i == lineRenderer.positionCount - 1)
                {
                    lineRenderer.SetPosition(i, activeObject.transform.position);
                }
                else
                {
                    Vector3 baseVector = transform.position + (renderVector * pointDistance * i);
                    Vector3 randVector = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));
                    Vector3 newPos = baseVector + randVector;
                    lineRenderer.SetPosition(i, newPos);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //calculate velocity and debug it
        currentPos = transform.position;
        velocity = currentPos - lastPos;
        Debug.Log("velocity of grabble: " + velocity);

        //if you can launch, launch the object based on velocity
        if (canLaunch && velocity.magnitude > 0.03f && activeObject.GetComponent<EvilProjectile>() == null)
        {
            Debug.Log("launching");
            LaunchObject(activeObject);
        }
        else if(canLaunch && velocity.magnitude > 0.03f && activeObject.GetComponent<EvilProjectile>() != null)
        {
            //redirect object with grapple beam
            activeObject.GetComponent<EvilProjectile>().Redirect(velocity.normalized);
        }

        lastPos = currentPos;
    }

    public void LaunchObject(GameObject launchable)
    {
        //deactivate linerenderer
        lineRenderer.enabled = false;

        //deactivate grapple
        grappleActive = false;

        //deactive launch
        canLaunch = false;

        Rigidbody launchBody = launchable.GetComponent<Rigidbody>();
        launchBody.isKinematic = false;
        launchBody.useGravity = true;
        //get the vector to launch from
        Vector3 launchVector = velocity.normalized;
        launchBody.AddForceAtPosition((launchVector * 70),activeObject.transform.position);
    }

    public void SetGrappleBeam()
    {
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            if (i == 0)
            {
                lineRenderer.SetPosition(i, transform.position);
            }
            else
            {
                Vector3 randVector = new Vector3(transform.position.x + Random.Range(-0.1f, 0.1f), transform.position.y + Random.Range(-0.1f, 0.1f), transform.position.z + Random.Range(-0.1f, 0.1f));
                lineRenderer.SetPosition(i, randVector);
            }
        }
    }

    public void StopGrappple()
    {
        //deactivate linerenderer
        lineRenderer.enabled = false;

        //deactivate grapple
        grappleActive = false;

        //deactive launch
        canLaunch = false;

        //reset active object
        activeObject = null;
    }
}
