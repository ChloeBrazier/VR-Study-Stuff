using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackerController : MonoBehaviour
{
    //public list of render textures
    public List<RenderTexture> securityCamList;

    //security cam feed for hacker
    public RawImage activeCam;
    private int camIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //set defualt active security cam
        activeCam.texture = securityCamList[0];
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Make this work with UI buttons
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCam();
        }
    }

    public void SwitchCam()
    {
        camIndex++;
        if(camIndex == securityCamList.Count)
        {
            camIndex = 0;
        }

        activeCam.texture = securityCamList[camIndex];
    }

    public void SetCam(int index)
    {
        activeCam.texture = securityCamList[index];
    }
}
