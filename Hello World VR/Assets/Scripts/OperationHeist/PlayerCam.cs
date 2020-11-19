using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Camera playerCam;
    void Start()
    {
        playerCam.cullingMask = 1 << 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
