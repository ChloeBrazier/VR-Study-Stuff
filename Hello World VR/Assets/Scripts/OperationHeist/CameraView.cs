using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Camera> CamList;
    void Start()
    {
        foreach(Camera cam in CamList)
        {
            cam.cullingMask = 8 << 0; ; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
