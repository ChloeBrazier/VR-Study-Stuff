using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackerController : MonoBehaviour
{
    //public list of render textures
    public List<RenderTexture> securityCamList;

    //lists for room-specific buttons
    public List<GameObject> roomOneButtons;
    public List<GameObject> roomTwoButtons;
    public List<GameObject> roomThreeButtons;

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

    public void OpenDoor(GameObject door)
    {
        Destroy(door);
    }

    //ugly-ass method for switching rooms p l e as e forgive me
    public void SwitchRoom(int index)
    {
        switch(index)
        {
            case 0:
                //set room one buttons active and room two and three buttons inactive
                foreach(GameObject button in roomTwoButtons)
                {
                    button.SetActive(false);
                }

                foreach (GameObject button in roomThreeButtons)
                {
                    button.SetActive(false);
                }

                foreach (GameObject button in roomOneButtons)
                {
                    button.SetActive(true);
                }
                break;
            case 1:
                //set room two buttons active and room one and three buttons inactive
                foreach (GameObject button in roomOneButtons)
                {
                    button.SetActive(false);
                }

                foreach (GameObject button in roomThreeButtons)
                {
                    button.SetActive(false);
                }

                foreach (GameObject button in roomTwoButtons)
                {
                    button.SetActive(true);
                }
                break;
            case 2:
                //set room three buttons active and room one and two buttons inactive
                foreach (GameObject button in roomOneButtons)
                {
                    button.SetActive(false);
                }

                foreach (GameObject button in roomTwoButtons)
                {
                    button.SetActive(false);
                }

                foreach (GameObject button in roomThreeButtons)
                {
                    button.SetActive(true);
                }
                break;
        }
    }
}
