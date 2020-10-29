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
    //or maybe pocket prefab?
    public GameObject pocket;

    // Start is called before the first frame update
    void Start()
    {
        //pockets.Add(leftPocket);
        //pockets.Add(rightPocket);
    }

    // Update is called once per frame
    void Update()
    {
        pocket.transform.position = Player.instance.hmdTransform.position;
        pocket.transform.forward = Player.instance.bodyDirectionGuess;
        Vector3 tempVector = pocket.transform.position;
        tempVector.y = Player.instance.eyeHeight/1.5f;
        //tempVector.x = Player.instance.hmdTransform.position.x;
        tempVector = tempVector + (Player.instance.bodyDirectionGuess.normalized * -0.35f);
        pocket.transform.position = tempVector;
        //Debug.Log("eye height: " + Player.instance.eyeHeight);
    }
}
