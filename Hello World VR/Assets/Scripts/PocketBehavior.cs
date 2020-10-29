using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class PocketBehavior : MonoBehaviour
{
    //action bool for pulling something from your pockets
    public SteamVR_Action_Boolean itemPull;

    //bool to check if the player can pull out an item
    private bool canPull = false;

    //list of items the player can pull (would be replaced with inventory system in future)
    public List<GameObject> sampleItems;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //pull out a random object if the player is allowed to 
        foreach (Hand hand in Player.instance.hands)
        {

            //save the input source of the hand
            SteamVR_Input_Sources source = hand.handType;

            //check if the player can pull an item
            if (canPull == true)
            {
                if (itemPull[source].stateDown)
                {
                    //randomly pull a sample item
                    int randInt = Random.Range(0, sampleItems.Count);

                    //spawn item and attach it to player's hand
                    GameObject newItem = Instantiate(sampleItems[randInt]);
                    Interactable itemInteract = newItem.GetComponent<Interactable>();
                    hand.AttachObject(newItem, GrabTypes.Pinch);
                    hand.HoverLock(itemInteract);
                }
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        //check if the collider belongs to a hand
        if (other.gameObject.GetComponentInParent<Hand>() != null)
        {
            //do not allow the player to pull out an object
            canPull = false;

            Debug.Log("cannot pull object");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //check if the collider belongs to a hand
        if (other.gameObject.GetComponentInParent<Hand>() != null)
        {
            //allow the player to pull out an object
            canPull = true;

            Debug.Log("can pull object");
        }
    }
}
