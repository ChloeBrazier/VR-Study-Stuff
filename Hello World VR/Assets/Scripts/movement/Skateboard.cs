using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public enum LeanStatus
{ 
    Still,
    Left,
    Right,
    Forward,
    Backward
}


public class Skateboard : MonoBehaviour
{
    //VR camera that acts as player's head position
    [SerializeField]
    private GameObject vrCam;

    [SerializeField]
    private GameObject origin;

    //steam vr action boolean
    public SteamVR_Action_Boolean recenterHead;

    //Debug Objects
    public List<GameObject> debugTextList;

    //bool to start tracking head against skateboard
    private bool trackHead = false;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //set up inital head origin
        SetOrigin();
        rigidbody = GetComponent<Rigidbody>();

        //cancel teleport hints
        Teleport.instance.CancelTeleportHint();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Hand hand in Player.instance.hands)
        {
            SteamVR_Input_Sources source = hand.handType;

            if (recenterHead[source].stateDown)
            {
                //readjust head locational data for leaning
                SetOrigin();
            }
        }

            //check head orientation based on head origin
            Vector3 headFromOrigin = vrCam.transform.position - transform.position;

            //get distance between origin object and headset
            float originDistance = Vector3.Distance(vrCam.transform.position, origin.transform.position);

            //get dot products for head position
            float forwardProduct = Vector3.Dot(transform.forward, headFromOrigin.normalized);
            float rightProduct = Vector3.Dot(transform.right, headFromOrigin.normalized);

            //check if the player's leanng forward or backward
            if (forwardProduct < 0)
            {
                debugTextList[3].SetActive(true);
            }
            else if (forwardProduct > 0 && originDistance > 0.2)
            {
                debugTextList[4].SetActive(true);
                //move skateboard forward
                rigidbody.AddForce(transform.forward * 30);
                Debug.Log("Moving forward");
            }

            //check if the player's leaning left or right
            if (rightProduct < 0 && originDistance > 0.2)
            {
                debugTextList[2].SetActive(true);
                //rotate the skateboard right
                transform.Rotate(0, -200 * Time.deltaTime, 0);
            }
            else if (rightProduct > 0 && originDistance > 0.2)
            {
                debugTextList[1].SetActive(true);
                //rotate the skateboard left
                transform.Rotate(0, 200 * Time.deltaTime, 0);
            }
    }

    public void SetOrigin()
    {
        origin.transform.position = vrCam.transform.position;
        ResetText();
    }

    public void ResetText()
    {
        foreach (GameObject text in debugTextList)
        {
            text.SetActive(false);
        }
    }
}
