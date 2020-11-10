using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{ 
    Fruit,
    Vegetable,
    Meat
}

public class Food : MonoBehaviour
{
    //this food's type
    public FoodType type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method to replace food with chopped variety
    public void Chop()
    {
        Debug.Log("food has been chopped");
    }

    //method to replace food with mixed variety
    public void Mix()
    {
        Debug.Log("food has been mixed");
    }
}
