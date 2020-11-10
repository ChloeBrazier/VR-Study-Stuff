using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHead : MonoBehaviour
{
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
        //slice up some food
        if(other.gameObject.tag == "Food")
        {
            //set food as sliced or something
            other.gameObject.GetComponent<Food>().Chop();
        }
    }
}
