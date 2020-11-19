using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollide : MonoBehaviour
{
    [SerializeField] GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hand")
        {
            Debug.Log("Hand dandy collision!");
            player.transform.position = Vector3.zero;
        }
    }
}
