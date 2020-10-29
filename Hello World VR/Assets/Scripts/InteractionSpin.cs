using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class InteractionSpin : MonoBehaviour
{
    //the amount of speed with which the object will spin
    public float spinSpeed = 1f;

    //steam vr action boolean
    public SteamVR_Action_Boolean activateSpin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Hand hand in Player.instance.hands)
        {
            SteamVR_Input_Sources source = hand.handType;

            if (activateSpin[source].stateDown)
            {
                StartCoroutine(SpinAround());
            }
        }
        
    }

    public IEnumerator SpinAround()
    {
        float currentRotation = transform.eulerAngles.y;

        for (float i = 0; i < 1; i += Time.deltaTime/2f)
        {
            float yRotation = Mathf.SmoothStep(currentRotation, 360, i);
            transform.eulerAngles = new Vector3(0, yRotation, 0);
            yield return null;
        }

        transform.eulerAngles = Vector3.zero;
    }
}
